using ems.master.Models;
using ems.storage.Functions;
using ems.utilities.Functions;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;

namespace ems.master.DataAccess
{
    public class DaMstCreditOpsApplication
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        OdbcDataReader objODBCDataReader, objODBCDatareader;
        HttpPostedFile httpPostedFile;
        DataTable dt_datatable, dt_datatable6, dt_datatable7, dt_datatable8, dt_datatable9;
        string msSQL, msGetGid, msGetdisbreqcode, msGetDocumentGid;
        int mnResult;
        int rowCount, columnCount;
        string lspath;
        string institution_urn = string.Empty;
        string individual_urn = string.Empty;
        string group_urn = string.Empty;
        string lsrmupdated_person;
        string lsapplication_gid, lsapplication2sanction_gid, lssanction_refno, lsapplication2loan_gid, lsproduct_type, lsprocessing_fees;
        string lsgst, lsfinance_charges, lsod_amount, lsescrow_payment, lsnach_status, lsremarks, lsupdated_person, lsamounttobe_disbursed, lsloandisbursement_date;
        string lsrmdisbursementrequest_gid, lsdisbursementassign_status, lsdisbursementcode, lsverticaldisbursementdocument_gid, lsdisbursementdocumentapprovalconfig_gid, lsdisbursementbankaccountapprovalconfig_gid, lsdisbursementbankaccount_gid, lsdisbursementodbelow30approvalconfig_gid, lsdisbursementodbelow30_gid, lsdisbursementodbelow90approvalconfig_gid, lsdisbursementodbelow90_gid, lspenalwaiver_gid, lspenalwaiverapprovalconfig_gid;
        string lsdisbdocdeferralapprovalconfig_gid, lsdisbursementdocdeferral_gid, lsvertical_gid, lsgroup_gid, lsgroup_name, lssubgroup_gid, lssubgroup_name, lsmanager_gid, lsmanager_name, lsmember_gid, lsmember_name;
        string lswefdate, lswefdatetime, lswef_date, lswef_datetime;
        string lscompany_code;
        string msGetGidMobile, endRange;
        string msGetGidEmail, msGetGidpan;
        string excelRange;
        string contactimportlog_message = "";
        string lsapplication_no, lsurn_status, lsurn, lsinsitution_name, lspan_status, lspan_no, lspanstatusvalue,
               lsaadhar_no, lsfirst_name, lsmiddle_name, lslast_name, lsindividual_dob, lsgender_name, lsgender_gid,
               lsdesignation_type, lsdesignation_gid, lspep_status, lspepverified_date, lsuser_type, lsusertype_gid,
               lsmaritalstatus_name, lsmaritalstatus_gid, lsfather_firstname, lsfather_middlename, lsfather_lastname,
               lsfathernominee_status, lsfather_dob, lsmother_firstname, lsmother_middlename, lsmother_lastname, lsmothernominee_status,
               lsmother_dob, lsspouse_firstname, lsspouse_middlename, lsspouse_lastname, lsspousenominee_status, lsspouse_dob,
               lseducationalqualification_name, lseducationalqualification_gid, lsmain_occupation, lsannual_income,
               lsmonthly_income, lsincometype_name, lsincometype_gid, lsyearscurrentresidece, lsdistancebranch;
        string lsstate_gid, lsstate, lscountry, lslatitude, lslongitude, lsinstitution2address_gid, lsinstitution_gid, lsgststate_gid, lsgst_state, lsgst_no, lsgst_registered;
        string lsmobile_no, lsprimary_status, lswhatsapp_no, lsinstitution2mobileno_gid, lsemail_address, lsinstitution2email_gid;
        string msGetGidAddress, lsaddresstype_name, lsaddresstype_gid, lscustomer_urn;
        string lsaddress_typegid, lsaddress_type, lsaddressline1, lsaddressline2, lslandmark, lstaluka, lspostal_code, lscity, lsdistrict, lsinstitution2branch_gid;
        int logCount = 0, mnResultMobile, mnResultEmail, mnResultAddress, mnResultGST, mnResultBank;
        string lsifsccode, lsbankname, lsbranchname, lsbranchaddress, lsmicrcode, lsbankaccountnumber, lsaccountholdername, lsaccounttype_gid, lsaccounttype, lsjointaccount, lsjointaccountholdername, lsischequebookfacilityavailable, lsaccountopendate, ldaccountopendate, lsdisbursementamount;
        string lsareference_gid, lsareference_number, lsdispgstprocessing_fees, lsadditionalcharges_gst, lsdispgstadditionfees_charges, lsmaker_remarks, lsdisbursement_to;
        string lsurnpan_number, lsindividual_name, lsdisbursement_amount;
        string coapplicantimportlog_message, lsfarmerpan_status, lsfarmerpan_number, lsfarmerindividual_name, lsfarmercontact_gid, lsage, lsfathers_age, lsmothers_age, lsspouse_age, lsmobileprimary_status, lsemailprimary_status, lsaddressprimary_status, lsconfirmbankaccountnumber;

        public void DaGetSanctionRefnoDropDown(string employee_gid, string customer_urn, MdlSanctionDropDown values)
        {
            //Loan Product
            msSQL = " Select a.application2sanction_gid,sanction_refno,c.generatelsa_gid,a.application_gid " +
                    " from ocs_trn_tapplication2sanction a " +
                    " left join ocs_trn_tcadapplication b on b.application_gid = a.application_gid " +
                    " left join ocs_trn_tgeneratelsa c on c.application2sanction_gid = a.application2sanction_gid " +
                    " where b.customer_urn='" + customer_urn + "' and date_format(sanctiontill_date, '%Y-%m-%d')>= CURDATE() " +
                    " group by a.application2sanction_gid order by a.application2sanction_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getsanctionrefnolist = new List<sanctionrefnolist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getsanctionrefnolist.Add(new sanctionrefnolist
                    {
                        application2sanction_gid = (dr_datarow["application2sanction_gid"].ToString()),
                        sanction_refno = (dr_datarow["sanction_refno"].ToString()),
                        generatelsa_gid = (dr_datarow["generatelsa_gid"].ToString()),
                        application_gid = (dr_datarow["application_gid"].ToString())
                    });
                }
                values.sanctionrefnolist = getsanctionrefnolist;
            }
            dt_datatable.Dispose();

        }

        public void DaGetSanctionDtls(string application2sanction_gid, string application_gid, RMsanctiondetails values)
        {
            try
            {
                msSQL = " select application2sanction_gid, sanction_refno,  date_format(a.sanction_date, '%d-%m-%Y %h:%i %p') as sanction_date, sanction_amount,  entity, paycard, " +
                        " entity_gid, application_gid , ccapproved_date, applicationtype_gid, application_type,esdeclaration_status, " +
                        " sanctionto_gid, sanctionto_name, date_format(a.sanctionfrom_date, '%d-%m-%Y') as sanctionfrom_date, " +
                        " date_format(a.sanctiontill_date, '%d-%m-%Y') as sanctiontill_date, contactpersonaddress_gid, " +
                        " contactperson_address, contactperson_name, contactperson_number, contactpersonmobileno_gid, " +
                        " contactpersonemail_gid, contactpersonemail_address, sanction_type, natureof_proposal " +
                        " from ocs_trn_tapplication2sanction a" +
                        " WHERE application2sanction_gid ='" + application2sanction_gid + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows)
                {
                    values.application2sanction_gid = objODBCDataReader["application2sanction_gid"].ToString();
                    values.sanction_refno = objODBCDataReader["sanction_refno"].ToString();
                    values.sanction_amount = objODBCDataReader["sanction_amount"].ToString();
                    values.entity = objODBCDataReader["entity"].ToString();
                    values.entity_gid = objODBCDataReader["entity_gid"].ToString();
                    values.application_gid = objODBCDataReader["application_gid"].ToString();
                    values.sanctionfrom_date = objODBCDataReader["sanctionfrom_date"].ToString();
                    values.sanctiontill_date = objODBCDataReader["sanctiontill_date"].ToString();
                    values.sanction_date = objODBCDataReader["sanction_date"].ToString();

                }
                objODBCDataReader.Close();


            }
            catch (Exception ex)
            {
                values.message = ex.ToString();
                values.status = false;
            }
        }

        public bool DaRMDisbursementDocumentUpload(HttpRequest httpRequest, disbursementuploaddocument objfilename, string employee_gid)
        {

            upload_list objdocumentmodel = new upload_list();
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string lsdocument_title = httpRequest.Form["document_title"].ToString();
            string lsrmdisbursementrequest_gid = httpRequest.Form["rmdisbursementrequest_gid"].ToString();
            string project_flag = httpRequest.Form["project_flag"].ToString();
            // string lsdocument_id = httpRequest.Form["document_id"].ToString();
            String path = lspath;

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/RMDisbursementDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
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
                        // Check Document validation;

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.message = "File format is not supported";
                            return false;
                        }
                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Master/RMDisbursementDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "Master/RMDisbursementDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        msGetGid = objcmnfunctions.GetMasterGID("RMDD");
                        msSQL = " insert into ocs_trn_trmdisbursementdocument( " +
                                    " rmdisbursementdocument_gid," +
                                    " rmdisbursementrequest_gid," +
                                    " application_gid," +
                                    " document_title," +
                                    " document_name," +
                                    " document_path," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + msGetGid + "',";
                        if (lsrmdisbursementrequest_gid == null || lsrmdisbursementrequest_gid == "")
                        {
                            msSQL += "'" + employee_gid + "',";
                        }
                        else
                        {
                            msSQL += "'" + lsrmdisbursementrequest_gid + "',";

                        }
                        msSQL += "'" + employee_gid + "'," +
                                 "'" + lsdocument_title.Replace("'", "") + "'," +
                                 "'" + httpPostedFile.FileName.Replace("'", "") + "'," +
                                 "'" + lspath + msdocument_gid + FileExtension.Replace("'", "") + "'," +
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

                        msSQL = " select rmdisbursementdocument_gid,application_gid,document_name,document_path,document_title, " +
                                 " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date, " +
                                 " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as created_by " +
                                 " from ocs_trn_trmdisbursementdocument a" +
                                 " left join hrm_mst_temployee b on b.employee_gid = a.created_by " +
                                 " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                                 " where rmdisbursementrequest_gid='" + employee_gid + "' or " +
                                 " rmdisbursementrequest_gid = '" + lsrmdisbursementrequest_gid + "'";
                        dt_datatable = objdbconn.GetDataTable(msSQL);
                        var getdisbursementuploaddocument_list = new List<disbursementuploaddocument_list>();
                        if (dt_datatable.Rows.Count != 0)
                        {
                            foreach (DataRow dt in dt_datatable.Rows)
                            {
                                getdisbursementuploaddocument_list.Add(new disbursementuploaddocument_list
                                {
                                    document_name = dt["document_name"].ToString(),
                                    document_title = dt["document_title"].ToString(),
                                    document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                                    application_gid = dt["application_gid"].ToString(),
                                    rmdisbursementdocument_gid = dt["rmdisbursementdocument_gid"].ToString(),
                                    created_by = dt["created_by"].ToString(),
                                    created_date = dt["created_date"].ToString(),

                                });
                                objfilename.disbursementuploaddocument_list = getdisbursementuploaddocument_list;
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

        public void DaDeleteRMDisbursementDocument(string rmdisbursementdocument_gid, string rmdisbursementrequest_gid, disbursementuploaddocument values, string employee_gid)
        {
            msSQL = "delete from ocs_trn_trmdisbursementdocument where rmdisbursementdocument_gid='" + rmdisbursementdocument_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Document Deleted successfully";

                msSQL = " select rmdisbursementdocument_gid,application_gid,document_name,document_path,document_title from " +
                        " ocs_trn_trmdisbursementdocument where rmdisbursementrequest_gid='" + employee_gid + "' or " +
                        " rmdisbursementrequest_gid='" + rmdisbursementrequest_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getdisbursementuploaddocument_list = new List<disbursementuploaddocument_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getdisbursementuploaddocument_list.Add(new disbursementuploaddocument_list
                        {
                            document_name = dt["document_name"].ToString(),
                            document_title = dt["document_title"].ToString(),
                            document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                            application_gid = dt["application_gid"].ToString(),
                            rmdisbursementdocument_gid = dt["rmdisbursementdocument_gid"].ToString(),

                        });
                        values.disbursementuploaddocument_list = getdisbursementuploaddocument_list;
                    }
                }
                dt_datatable.Dispose();

            }
            else
            {
                values.status = false;
                values.message = "Error Occurred while deleting Document";
            }
        }

        public void DaGetRMDisbursementTempClear(string employee_gid, result values)
        {
            msSQL = "delete from ocs_trn_trmdisbursementdocument where application_gid ='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            values.status = true;
        }

        public void DaGetProductChargesDtl(string application2loan_gid, MdlMstProductChargesView values)
        {
            try
            {
                msSQL = " select a.application2loan_gid, a.enduse_purpose,a.facility_mode,a.rate_interest,a.loanfacility_amount,a.tenureoverall_limit,b.product_type, " +
                        " a.application_gid,b.processing_fees,b.processing_gst,b.finance_charges,b.od_amount,b.escrow_payment,b.nach_status,b.remarks from ocs_trn_tcadapplication2loan a" +
                        " left join ocs_trn_trmdisbursementrequest b on b.application_gid = a.application_gid " +
                        " where a.application2loan_gid='" + application2loan_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.application2loan_gid = objODBCDatareader["application2loan_gid"].ToString();
                    values.application_gid = objODBCDatareader["application_gid"].ToString();
                    values.enduse_purpose = objODBCDatareader["enduse_purpose"].ToString();
                    values.facility_mode = objODBCDatareader["facility_mode"].ToString();
                    values.rate_interest = objODBCDatareader["rate_interest"].ToString();
                    values.tenureoverall_limit = objODBCDatareader["tenureoverall_limit"].ToString();
                    values.product_type = objODBCDatareader["product_type"].ToString();
                    values.processing_fees = objODBCDatareader["processing_fees"].ToString();
                    values.gst = objODBCDatareader["processing_gst"].ToString();
                    values.finance_charges = objODBCDatareader["finance_charges"].ToString();
                    values.od_amount = objODBCDatareader["od_amount"].ToString();
                    values.escrow_payment = objODBCDatareader["escrow_payment"].ToString();
                    values.nach_status = objODBCDatareader["nach_status"].ToString();
                    values.remarks = objODBCDatareader["remarks"].ToString();
                    values.loanfacility_amount = objODBCDatareader["loanfacility_amount"].ToString();
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

        public void DaGetProductView(string application_gid, MdlMstProductView values)
        {
            msSQL = " select group_concat(ifnull(c.urn,''))   from ocs_trn_tcadapplication a " +
                    " left join ocs_trn_tcadinstitution c on c.application_gid = a.application_gid " +
                    " where a.application_gid = '" + application_gid + "'";
            institution_urn = objdbconn.GetExecuteScalar(msSQL);
            institution_urn = institution_urn.Replace(",,", ",");
            institution_urn = institution_urn.Replace(",,,", ",");
            institution_urn = institution_urn.TrimEnd(',');

            msSQL = " select group_concat(ifnull(c.urn,''))   from ocs_trn_tcadapplication a " +
                    " left join ocs_trn_tcadcontact c on c.application_gid = a.application_gid " +
                    " where a.application_gid = '" + application_gid + "'";
            individual_urn = objdbconn.GetExecuteScalar(msSQL);
            individual_urn = individual_urn.Replace(",,", ",");
            individual_urn = individual_urn.Replace(",,,", ",");
            individual_urn = individual_urn.TrimEnd(',');

            msSQL = " select group_concat(ifnull(c.group_urn,''))   from ocs_trn_tcadapplication a " +
                    " left join ocs_trn_tcadgroup c on c.application_gid = a.application_gid " +
                    " where a.application_gid = '" + application_gid + "'";
            group_urn = objdbconn.GetExecuteScalar(msSQL);
            group_urn = group_urn.Replace(",,", ",");
            group_urn = group_urn.Replace(",,,", ",");
            group_urn = group_urn.TrimEnd(',');

            try
            {
                msSQL = " select a.customer_name, b.stakeholder_type, 'Institution' as applicant_type, a.urn,a.account_no, " +
                        " a.max_dpd,a.Vertical,a.lastdemandrundate,a.current_principal from lgl_tmp_tmisdata a " +
                        " left join ocs_trn_tcadinstitution b on a.urn = b.urn " +
                        " where a.urn in ('" + institution_urn.Replace(",", "','") + "') and  a.ac_status = '0'" +
                        " union " +
                        " select a.customer_name, b.stakeholder_type, 'Individual' as applicant_type, a.urn,a.account_no,a.rbiold_oddays, a.late_charge, " +
                        " a.max_dpd,a.Vertical,a.lastdemandrundate,a.current_principal from lgl_tmp_tmisdata a " +
                        " left join ocs_trn_tcadcontact b on a.urn = b.urn " +
                        " where a.urn in ('" + individual_urn.Replace(",", "','") + "') and  a.ac_status = '0'" +
                        " union " +
                        " select a.customer_name, '-' as stakeholder_type, 'Group' as applicant_type, a.urn,a.account_no,a.rbiold_oddays,a.late_charge, " +
                        " a.max_dpd,a.Vertical,a.lastdemandrundate,a.current_principal from lgl_tmp_tmisdata a " +
                        " left join ocs_trn_tcadgroup b on a.urn = b.group_urn " +
                        " where a.urn in ('" + group_urn.Replace(",", "','") + "') and  a.ac_status = '0'";

                values.status = true;
                values.message = "success";
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "failure";
            }
        }

        public void DaGetLSABankDocumentUpload(string lsabankaccdtl_gid, MdlLSAuploadeddocument values)
        {
            msSQL = " select lsachequeleafdocument_gid,lsabankaccdtl_gid,document_name,document_path,document_title from " +
                   " ocs_trn_tlsachequeleafdocument where lsabankaccdtl_gid='" + lsabankaccdtl_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getlsauploadeddocument_list = new List<lsauploadeddocument_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getlsauploadeddocument_list.Add(new lsauploadeddocument_list
                    {
                        document_name = dt["document_name"].ToString(),
                        document_title = dt["document_title"].ToString(),
                        document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                        lsachequeleafdocument_gid = dt["lsachequeleafdocument_gid"].ToString(),
                        lsabankaccdtl_gid = dt["lsabankaccdtl_gid"].ToString(),
                    });
                    values.lsauploadeddocument_list = getlsauploadeddocument_list;
                }
            }
            dt_datatable.Dispose();

        }

        public void DaPostDisbursementRequestAdd(MdlDisbursementRequestAdd values, string employee_gid)
        {

            if (values.disbursement_to == "Applicant(B2B)")
            {
                msSQL = " select bankaccountstatus_gid from ocs_trn_tbankaccountstatus  " +
                   " where  rmdisbursementrequest_gid='" + employee_gid + "' and disbursementaccount_status='Yes'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == false)
                {
                    values.status = false;
                    values.message = "Kindly Update the Bank Account Status";
                    objODBCDatareader.Close();
                    return;
                }
            }

            if (values.disbursement_to == "Applicant(B2B)")
            {
                msSQL = " select  disbursementamount_gid from ocs_trn_tdisbursementamount  " +
                   " where application_gid = '" + values.application_gid + "' and  " +
                   " rmdisbursementrequest_gid='" + employee_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == false)
                {
                    values.status = false;
                    values.message = "Kindly Add the Disbursement Amount";
                    objODBCDatareader.Close();
                    return;
                }
            }






            msSQL = " select a.application_gid" +
                   " from ocs_trn_tcadapplication a " +
                   " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by " +
                   " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                   " left join ocs_trn_tprocesstype_assign d on d.application_gid = a.application_gid " +
                   " left join ocs_trn_tapplication2sanction e on e.application_gid = a.application_gid " +
                   " where a.process_type = 'Accept' and d.menu_gid = '" + getMenuClass.ScannedDocument + "' " +
                   " and d.maker_approvalflag='Y' and d.checker_approvalflag = 'Y' and d.approver_approvalflag='Y' " +
                   " and d.overall_approvalstatus='Approved' and d.completed_flag='Y'  " +

                   " and e.application2sanction_gid ='" + values.application2sanction_gid + "' order by d.created_date desc";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Deferral Document status is pending. So, can't able to initiate disbursement request";
                objODBCDatareader.Close();
                return;
            }

            //msSQL = " select a.application_gid " +
            //        " from ocs_trn_tcadapplication a " +
            //        " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by " +
            //        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
            //        " left join ocs_trn_tprocesstype_assign d on d.application_gid = a.application_gid " +
            //        " left join ocs_trn_tapplication2sanction e on e.application_gid = a.application_gid " +
            //        " where a.process_type = 'Accept' and d.menu_gid = '" + getMenuClass.PhysicalDocument + "' " +
            //        " and d.maker_approvalflag='Y' and d.checker_approvalflag = 'Y' and d.approver_approvalflag='Y' " +
            //        " and d.overall_approvalstatus='Approved' and d.completed_flag='Y' and " +
            //        " (maker_gid='" + employee_gid + "' or checker_gid='" + employee_gid + "' or approver_gid='" + employee_gid + "') " +
            //        "  and e.application2sanction_gid ='" + values.application2sanction_gid + "' order by d.created_date desc";
            //objODBCDatareader = objdbconn.GetDataReader(msSQL);
            //if (objODBCDatareader.HasRows == false)
            //{
            //    values.status = false;
            //    values.message = "Deferral Document status is pending. So, can't able to initiate disbursement request";
            //    objODBCDatareader.Close();
            //    return;
            //}


            msGetGid = objcmnfunctions.GetMasterGID("RMDR");
            msGetdisbreqcode = objcmnfunctions.GetMasterGID("DISB");

            lsdisbursementcode = msGetdisbreqcode + "/" + values.sanction_refno;

            msSQL = " insert into ocs_trn_trmdisbursementrequest(" +
                    " rmdisbursementrequest_gid ," +
                    " disbursementrequest_code," +
                    " application_gid," +
                    " application2sanction_gid," +
                    " sanction_refno," +
                    " application2loan_gid," +
                    " product_type," +
                    " processing_fees," +
                    " processing_gst," +
                    " finance_charges," +
                    " od_amount," +
                    " escrow_payment," +
                    " nach_status," +
                    " remarks," +
                    " updated_person," +
                    " loandisbursement_date," +
                    " amounttobe_disbursed," +
                    " disbursement_to, " +
                    " lsareference_gid," +
                    " lsareference_number," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + lsdisbursementcode + "'," +
                    "'" + values.application_gid + "'," +
                    "'" + values.application2sanction_gid + "'," +
                    "'" + values.sanction_refno + "'," +
                    "'" + values.application2loan_gid + "'," +
                    "'" + values.product_type + "'," +
                    "'" + values.processing_fees + "'," +
                    "'" + values.gst + "'," +
                    "'" + values.finance_charges + "'," +
                    "'" + values.od_amount + "'," +
                    "'" + values.escrow_payment + "'," +
                    "'" + values.nach_status + "'," +
                    "'" + values.remarks.Replace("'", "") + "'," +
                    "'" + values.updated_person + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                    "'" + values.amounttobe_disbursed + "'," +
                    "'" + values.disbursement_to + "'," +
                    "'" + values.lsareference_gid + "'," +
                    "'" + values.lsareference_number + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            //msSQL = " update ocs_trn_trmdisbursementaccount set rmdisbursementrequest_gid='" + msGetGid + "'," +
            //        " application2sanction_gid='" + values.application2sanction_gid + "'," +
            //        " sanction_refno='" + values.sanction_refno + "'" +
            //        " where application_gid ='" + values.application_gid + "'";
            //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " update ocs_trn_trmdisbursementdocument set application_gid='" + values.application_gid + "'," +
                    " rmdisbursementrequest_gid='" + msGetGid + "'" +
                    " where rmdisbursementrequest_gid ='" + employee_gid + "' and application_gid='" + employee_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " update ocs_trn_tfarmercontact set rmdisbursementrequest_gid='" + msGetGid + "'" +
                    " where rmdisbursementrequest_gid ='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " update ocs_trn_tdisbursementsupplier set rmdisbursementrequest_gid='" + msGetGid + "'" +
                    " where rmdisbursementrequest_gid ='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " update ocs_trn_tcontactcoapplicant set rmdisbursementrequest_gid='" + msGetGid + "'" +
                    " where rmdisbursementrequest_gid ='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            msSQL = " update ocs_trn_tdisbapplicantbankdtl set rmdisbursementrequest_gid='" + msGetGid + "'" +
                  " where rmdisbursementrequest_gid ='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " update ocs_trn_tdisbursementamount set rmdisbursementrequest_gid='" + msGetGid + "'" +
                    " where rmdisbursementrequest_gid ='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " update ocs_trn_tbankaccountstatus set rmdisbursementrequest_gid='" + msGetGid + "'" +
                    " where rmdisbursementrequest_gid ='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Disbursement Request Added successfully";
            }
            else
            {
                values.message = "Error Occured while Adding";
                values.status = false;
            }

        }

        public void DaGetDisbursementRequestSummary(string employee_gid, string customer_urn, string application_gid, MdlDisbursementRequest values)
        {
            try
            {
                msSQL = " select a.application_gid,e.application_no,a.disbursementrequest_code,e.customer_name as customer_name,e.customer_urn,a.sanction_refno, " +
                        " d.generatelsa_gid,date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as created_by, " +
                        " a.application2sanction_gid, a.application2loan_gid, a.rmdisbursementrequest_gid,a.disbursementassign_status," +
                        " lsareference_number,product_type,disbursement_to " +
                        " from ocs_trn_trmdisbursementrequest a " +
                        " left join ocs_trn_tcadapplication e on e.application_gid = a.application_gid " +
                        " left join hrm_mst_temployee b on b.employee_gid = e.created_by " +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " left join ocs_trn_tgeneratelsa d on d.application2sanction_gid = a.application2sanction_gid " +
                        " where e.customer_urn = '" + customer_urn + "' and a.application_gid=e.application_gid" +
                        " group by a.created_date order by a.updated_date desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getdisbursementrequest_list = new List<disbursementrequest_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getdisbursementrequest_list.Add(new disbursementrequest_list
                        {
                            application_no = dt["application_no"].ToString(),
                            customer_name = dt["customer_name"].ToString(),
                            application_gid = dt["application_gid"].ToString(),
                            application2sanction_gid = dt["application2sanction_gid"].ToString(),
                            application2loan_gid = dt["application2loan_gid"].ToString(),
                            sanction_refno = dt["sanction_refno"].ToString(),
                            created_by = dt["created_by"].ToString(),
                            created_date = dt["created_date"].ToString(),
                            rmdisbursementrequest_gid = dt["rmdisbursementrequest_gid"].ToString(),
                            disbursementassign_status = dt["disbursementassign_status"].ToString(),
                            generatelsa_gid = dt["generatelsa_gid"].ToString(),
                            disbursementrequest_code = dt["disbursementrequest_code"].ToString(),
                            lsareference_number = dt["lsareference_number"].ToString(),
                            product_type = dt["product_type"].ToString(),
                            disbursement_to = dt["disbursement_to"].ToString()
                        });

                    }
                }
                values.disbursementrequest_list = getdisbursementrequest_list;
                dt_datatable.Dispose();

                msSQL = " select rmdisbursementdocument_gid,application_gid,document_name,document_path,document_title, " +
                          " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as created_by, " +
                          " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date from ocs_trn_trmdisbursementdocument a" +
                          " left join hrm_mst_temployee b on b.employee_gid = a.created_by " +
                          " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                          "where application_gid='" + application_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getdisbursementuploaddocument_list = new List<disbursementuploaddocument_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getdisbursementuploaddocument_list.Add(new disbursementuploaddocument_list
                        {
                            document_name = dt["document_name"].ToString(),
                            document_title = dt["document_title"].ToString(),
                            document_path = dt["document_path"].ToString(),
                            application_gid = dt["application_gid"].ToString(),
                            rmdisbursementdocument_gid = dt["rmdisbursementdocument_gid"].ToString(),
                            created_by = dt["created_by"].ToString(),
                            created_date = dt["created_date"].ToString(),

                        });
                        values.disbursementuploaddocument_list = getdisbursementuploaddocument_list;
                    }
                }
                dt_datatable.Dispose();

                msSQL = " select rmdisbursementdocument_gid,application_gid,document_name,document_path,document_title, " +
                          " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as created_by, " +
                          " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date from ocs_trn_trmdisbursementdocument a" +
                          " left join hrm_mst_temployee b on b.employee_gid = a.created_by " +
                          " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                          "where application_gid='" + application_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getdisbursementuploadeddocument_list = new List<disbursementuploadeddocument_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getdisbursementuploadeddocument_list.Add(new disbursementuploadeddocument_list
                        {
                            document_name = dt["document_name"].ToString(),
                            document_title = dt["document_title"].ToString(),
                            document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                            application_gid = dt["application_gid"].ToString(),
                            rmdisbursementdocument_gid = dt["rmdisbursementdocument_gid"].ToString(),
                            created_by = dt["created_by"].ToString(),
                            created_date = dt["created_date"].ToString(),

                        });
                        values.disbursementuploadeddocument_list = getdisbursementuploadeddocument_list;
                    }
                }
                dt_datatable.Dispose();

                values.status = true;
            }
            catch (Exception ex)
            {
                values.status = false;
            }
        }

        public void DaPostConfirmDisbursementAcct(MdlConfirmDisbursementAcct values, string employee_gid)
        {
            msGetGid = objcmnfunctions.GetMasterGID("RMDA");
            msSQL = " insert into ocs_trn_trmdisbursementaccount(" +
                    " rmdisbursementaccount_gid ," +
                    " rmdisbursementrequest_gid ," +
                    " application_gid," +
                    " application2sanction_gid," +
                    " sanction_refno," +
                    " bankdtl_gid," +
                    " disbursement_status," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + values.application_gid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + values.bankdtl_gid + "'," +
                    "'" + values.disbursement_status + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Disbursement Account Added successfully";
            }
            else
            {
                values.message = "Error Occured while Adding";
                values.status = false;
            }

        }

        public void DaGetCreditDisbursementBankDtls(string application_gid, RMsanctiondetails values)
        {
            try
            {
                msSQL = " select creditbankdtl_gid,credit_gid, a.application_gid, bank_name,branch_name,bank_address,micr_code,ifsc_code,bankaccount_number,chequebookfacility_available, " +
                  "  bankaccount_name,bankaccounttype_gid,bankaccounttype_name,confirmbankaccountnumber,accountopen_date,disbursement_accountstatus,accountholder_name, " +
                  " joint_account,jointaccountholder_name,chequebook_status , date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                  " date_format(a.updated_date, '%d-%m-%Y %h:%i %p') as updated_date,f.disbursement_status as rmdisbursement_status, " +
                  " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, " +
                  " concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as updated_by " +
                  " from ocs_mst_tcreditbankdtl a " +
                  " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                  " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                  " left join hrm_mst_temployee d on a.updated_by = d.employee_gid " +
                  " left join adm_mst_tuser e on e.user_gid = d.user_gid " +
                  " left join ocs_trn_trmdisbursementaccount f on f.bankdtl_gid = a.creditbankdtl_gid " +
                  "  where a.application_gid='" + application_gid + "' and a.disbursement_accountstatus = 'Yes'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcreditbankaccount_list = new List<creditbankaccount_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcreditbankaccount_list.Add(new creditbankaccount_list
                        {
                            creditbankdtl_gid = (dr_datarow["creditbankdtl_gid"].ToString()),
                            bank_name = (dr_datarow["bank_name"].ToString()),
                            branch_name = (dr_datarow["branch_name"].ToString()),
                            bank_address = (dr_datarow["bank_address"].ToString()),
                            micr_code = (dr_datarow["micr_code"].ToString()),
                            ifsc_code = (dr_datarow["ifsc_code"].ToString()),
                            accountholder_name = (dr_datarow["accountholder_name"].ToString()),
                            bankaccount_number = (dr_datarow["bankaccount_number"].ToString()),
                            confirmbankaccountnumber = (dr_datarow["confirmbankaccountnumber"].ToString()),
                            joint_account = (dr_datarow["joint_account"].ToString()),
                            jointaccountholder_name = (dr_datarow["jointaccountholder_name"].ToString()),
                            chequebookfacility_available = (dr_datarow["chequebookfacility_available"].ToString()),
                            accountopen_date = (dr_datarow["accountopen_date"].ToString()),
                            disbursement_accountstatus = (dr_datarow["disbursement_accountstatus"].ToString()),
                            bankaccounttype_name = (dr_datarow["bankaccounttype_name"].ToString()),
                            rmdisbursement_status = (dr_datarow["rmdisbursement_status"].ToString()),
                        });
                    }
                    values.creditbankaccount_list = getcreditbankaccount_list;
                }
                dt_datatable.Dispose();

            }
            catch (Exception ex)
            {
                values.message = ex.ToString();
                values.status = false;
            }
        }

        public void DaGetLSADisbursementBankDtls(string application_gid, RMsanctiondetails values)
        {
            try
            {
                msSQL = " select lsabankaccdtl_gid, name, stakeholder_type, bank_name, branch_name, branch_address, micr_code, ifsc_code, accountholder_name, " +
                   " accounttype_gid, accounttype_name, bankaccount_number, confirmbankaccount_number, joint_account,lsabankaccdtl_gid, " +
                   " jointaccountholder_name, chequebookfacility_available, accountopen_date, disbursement_accountstatus,b.disbursement_status as rmdisbursement_status " +
                   " from ocs_trn_tlsabankaccountdtl a" +
                    " left join ocs_trn_trmdisbursementaccount b on b.bankdtl_gid = a.lsabankaccdtl_gid " +
                   " where a.application_gid='" + application_gid + "' and disbursement_accountstatus = 'Yes'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getlsabankaccount_list = new List<lsabankaccount_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getlsabankaccount_list.Add(new lsabankaccount_list
                        {
                            lsabankaccdtl_gid = (dr_datarow["lsabankaccdtl_gid"].ToString()),
                            name = (dr_datarow["name"].ToString()),
                            stakeholder_type = (dr_datarow["stakeholder_type"].ToString()),
                            bank_name = (dr_datarow["bank_name"].ToString()),
                            branch_name = (dr_datarow["branch_name"].ToString()),
                            branch_address = (dr_datarow["branch_address"].ToString()),
                            micr_code = (dr_datarow["micr_code"].ToString()),
                            ifsc_code = (dr_datarow["ifsc_code"].ToString()),
                            accountholder_name = (dr_datarow["accountholder_name"].ToString()),
                            bankaccount_number = (dr_datarow["bankaccount_number"].ToString()),
                            confirmbankaccountnumber = (dr_datarow["confirmbankaccount_number"].ToString()),
                            joint_account = (dr_datarow["joint_account"].ToString()),
                            jointaccountholder_name = (dr_datarow["jointaccountholder_name"].ToString()),
                            chequebookfacility_available = (dr_datarow["chequebookfacility_available"].ToString()),
                            accountopen_date = (dr_datarow["accountopen_date"].ToString()),
                            disbursement_accountstatus = (dr_datarow["disbursement_accountstatus"].ToString()),
                            accounttype_name = (dr_datarow["accounttype_name"].ToString()),
                            rmdisbursement_status = (dr_datarow["rmdisbursement_status"].ToString()),
                        });
                    }
                    values.lsabankaccount_list = getlsabankaccount_list;
                }
                dt_datatable.Dispose();

            }
            catch (Exception ex)
            {
                values.message = ex.ToString();
                values.status = false;
            }
        }

        public void DaGetDisbursementPendingSummary(string employee_gid, MdlDisbursementRequest values)
        {
            try
            {
                msSQL = " select a.application_gid,e.application_no,e.customer_name as customer_name,e.customer_urn,a.sanction_refno, " +
                        " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date, concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as created_by, " +
                        " a.application2sanction_gid, a.application2loan_gid,e.vertical_name,e.region,a.rmdisbursementrequest_gid, " +
                        " a.disbursementrequest_code,a.sanction_refno,a.lsareference_number,a.product_type,a.disbursement_to " +
                        " from ocs_trn_trmdisbursementrequest a " +
                        " left join ocs_trn_tcadapplication e on e.application_gid = a.application_gid" +
                        " left join hrm_mst_temployee b on b.employee_gid = a.created_by " +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where a.disbursementassign_status = 'Pending' order by a.created_date desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getdisbursementrequest_list = new List<disbursementrequest_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getdisbursementrequest_list.Add(new disbursementrequest_list
                        {
                            application_no = dt["application_no"].ToString(),
                            customer_name = dt["customer_name"].ToString(),
                            application_gid = dt["application_gid"].ToString(),
                            application2sanction_gid = dt["application2sanction_gid"].ToString(),
                            application2loan_gid = dt["application2loan_gid"].ToString(),
                            created_by = dt["created_by"].ToString(),
                            created_date = dt["created_date"].ToString(),
                            vertical_name = dt["vertical_name"].ToString(),
                            region = dt["region"].ToString(),
                            rmdisbursementrequest_gid = dt["rmdisbursementrequest_gid"].ToString(),
                            disbursementrequest_code = dt["disbursementrequest_code"].ToString(),
                            sanction_refno = dt["sanction_refno"].ToString(),
                            lsareference_number = dt["lsareference_number"].ToString(),
                            product_type = dt["product_type"].ToString(),
                            disbursement_to = dt["disbursement_to"].ToString()
                        });

                    }
                }
                values.disbursementrequest_list = getdisbursementrequest_list;
                dt_datatable.Dispose();

                values.status = true;
            }
            catch (Exception ex)
            {
                values.status = false;
            }
        }

        public void DaGetCreditOpsGroupDropDown(string employee_gid, string application_gid, MdlCreditOpsGroupDropDown values)
        {
            //Loan Product
            msSQL = " SELECT creditopsgroupmapping_gid,creditopsgroup_name FROM ocs_mst_tcreditopsgroupmapping a" +
                    " left join ocs_trn_tcadapplication b on b.vertical_gid = a.vertical_gid" +
                    " where b.application_gid='" + application_gid + "' order by a.created_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcreditOpsGrouplist = new List<creditOpsGrouplist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcreditOpsGrouplist.Add(new creditOpsGrouplist
                    {
                        creditopsgroupmapping_gid = (dr_datarow["creditopsgroupmapping_gid"].ToString()),
                        creditopsgroup_name = (dr_datarow["creditopsgroup_name"].ToString()),
                    });
                }
                values.creditOpsGrouplist = getcreditOpsGrouplist;
            }
            dt_datatable.Dispose();

        }

        public void DaGetCreditOps2Heads(string creditopsgroupmapping_gid, MdlCreditOps2Heads objmaster)
        {
            msSQL = " select creditops2maker_gid,employee_gid,employee_name, creditopsgroupmapping_gid from ocs_mst_tcreditops2maker " +
                  " where creditopsgroupmapping_gid='" + creditopsgroupmapping_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getCreditops_maker = new List<Creditops_maker>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getCreditops_maker.Add(new Creditops_maker
                    {
                        creditops2maker_gid = dt["creditops2maker_gid"].ToString(),
                        employee_gid = dt["employee_gid"].ToString(),
                        employee_name = dt["employee_name"].ToString(),
                    });
                    objmaster.Creditops_maker = getCreditops_maker;
                }
            }
            dt_datatable.Dispose();

            msSQL = " select creditops2checker_gid,employee_gid,employee_name, creditopsgroupmapping_gid from ocs_mst_tcreditops2checker " +
                " where creditopsgroupmapping_gid='" + creditopsgroupmapping_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getCreditops_checker = new List<Creditops_checker>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getCreditops_checker.Add(new Creditops_checker
                    {
                        creditops2checker_gid = dt["creditops2checker_gid"].ToString(),
                        employee_gid = dt["employee_gid"].ToString(),
                        employee_name = dt["employee_name"].ToString(),
                    });
                    objmaster.Creditops_checker = getCreditops_checker;
                }
            }
            dt_datatable.Dispose();

        }

        public void DaPostDisbursementAssignment(MdlDisbursementAssignment values, string employee_gid)
        {
            msGetGid = objcmnfunctions.GetMasterGID("DIAS");
            msSQL = " insert into ocs_trn_tdisbursementassignment(" +
                    " disbursementassignment_gid ," +
                    " application_gid," +
                    " creditopsgroup_gid," +
                    " creditopsgroup_name," +
                    " creditopsmaker_gid," +
                    " creditopsmaker_name," +
                    " creditopschecker_gid," +
                    " creditopschecker_name," +
                    //" creditopsapprover_gid," +
                    //" creditopsapprover_name," +
                    " remarks," +
                    " disbursementassign_status," +
                    " rmdisbursementrequest_gid," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.application_gid + "'," +
                    "'" + values.creditopsgroup_gid + "'," +
                    "'" + values.creditopsgroup_name + "'," +
                    "'" + values.creditopsmaker_gid + "'," +
                    "'" + values.creditopsmaker_name + "'," +
                    "'" + values.creditopschecker_gid + "'," +
                    "'" + values.creditopschecker_name + "'," +
                    //"'" + values.creditopsapprover_gid + "'," +
                    //"'" + values.creditopsapprover_name + "'," +
                    "'" + values.remarks.Replace("'", "") + "'," +
                    "'Assigned'," +
                    "'" + values.rmdisbursementrequest_gid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " update ocs_trn_trmdisbursementrequest set disbursementassign_status='Assigned'" +
                    " where rmdisbursementrequest_gid ='" + values.rmdisbursementrequest_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Disbursement Assigned successfully";
            }
            else
            {
                values.message = "Error Occured while Adding";
                values.status = false;
            }

        }

        public void DaGetDisbursementAssignedSummary(string employee_gid, MdlDisbursementRequest values)
        {
            try
            {
                msSQL = " select a.application_gid,e.application_no,e.customer_name as customer_name,e.customer_urn,a.sanction_refno, " +
                        " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date, concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as created_by, " +
                        " concat(g.user_firstname, ' ', g.user_lastname, ' / ', g.user_code) as checker_name,concat(k.user_firstname, ' ', k.user_lastname, ' / ', k.user_code) as assigned_by, " +
                        " date_format(d.created_date, '%d-%m-%Y %h:%i %p') as assigned_date," +
                        " concat(i.user_firstname, ' ', i.user_lastname, ' / ', i.user_code) as maker_name, " +
                        " a.application2sanction_gid, a.application2loan_gid,e.vertical_name,e.region, " +
                        " a.rmdisbursementrequest_gid, a.disbursementrequest_code,a.sanction_refno,a.lsareference_number," +
                        " a.product_type,a.disbursement_to " +
                        " from ocs_trn_trmdisbursementrequest a " +
                        " left join ocs_trn_tcadapplication e on e.application_gid = a.application_gid" +
                        " left join hrm_mst_temployee b on b.employee_gid = a.created_by " +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +

                        " left join ocs_trn_tdisbursementassignment d on d.rmdisbursementrequest_gid = a.rmdisbursementrequest_gid " +
                          " left join hrm_mst_temployee j on j.employee_gid = d.created_by " +
                        " left join adm_mst_tuser k on k.user_gid = j.user_gid " +
                        " left join hrm_mst_temployee f on f.employee_gid = d.creditopschecker_gid " +
                        " left join adm_mst_tuser g on g.user_gid = f.user_gid " +
                        " left join hrm_mst_temployee h on h.employee_gid = d.creditopsmaker_gid " +
                        " left join adm_mst_tuser i on i.user_gid = h.user_gid " +
                        " where d.disbursementassign_status = 'Assigned' and d.approval_status != 'Rejected'" +
                        " order by a.created_date desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getdisbursementassigned_list = new List<disbursementassigned_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getdisbursementassigned_list.Add(new disbursementassigned_list
                        {
                            application_no = dt["application_no"].ToString(),
                            customer_name = dt["customer_name"].ToString(),
                            application_gid = dt["application_gid"].ToString(),
                            application2sanction_gid = dt["application2sanction_gid"].ToString(),
                            application2loan_gid = dt["application2loan_gid"].ToString(),
                            sanction_refno = dt["sanction_refno"].ToString(),
                            created_by = dt["created_by"].ToString(),
                            created_date = dt["created_date"].ToString(),
                            vertical_name = dt["vertical_name"].ToString(),
                            region = dt["region"].ToString(),
                            maker_name = dt["maker_name"].ToString(),
                            checker_name = dt["checker_name"].ToString(),
                            rmdisbursementrequest_gid = dt["rmdisbursementrequest_gid"].ToString(),
                            disbursementrequest_code = dt["disbursementrequest_code"].ToString(),
                            lsareference_number = dt["lsareference_number"].ToString(),
                            product_type = dt["product_type"].ToString(),
                            disbursement_to = dt["disbursement_to"].ToString(),
                            assigned_by = dt["assigned_by"].ToString(),
                            assigned_date = dt["assigned_date"].ToString()
                        });

                    }
                }
                values.disbursementassigned_list = getdisbursementassigned_list;
                dt_datatable.Dispose();

                values.status = true;
            }
            catch (Exception ex)
            {
                values.status = false;
            }
        }

        public void DaDisbursementAssignCount(string user_gid, string employee_gid, DisbursementAssignCount values)
        {
            msSQL = " select count(a.rmdisbursementrequest_gid) as pending_count from ocs_trn_trmdisbursementrequest a" +
                    " where a.disbursementassign_status = 'Pending'";
            values.pending_count = objdbconn.GetExecuteScalar(msSQL);
            int pending_count = Convert.ToInt16(values.pending_count);

            msSQL = " select count(d.rmdisbursementrequest_gid) as assigned_count from ocs_trn_trmdisbursementrequest a" +
                    " left join ocs_trn_tdisbursementassignment d on d.rmdisbursementrequest_gid = a.rmdisbursementrequest_gid " +
                    " where a.disbursementassign_status = 'Assigned' and d.approval_status != 'Rejected' ";
            values.assigned_count = objdbconn.GetExecuteScalar(msSQL);
            int assigned_count = Convert.ToInt16(values.assigned_count);

            msSQL = " select count(d.rmdisbursementrequest_gid) as assigned_count from ocs_trn_trmdisbursementrequest a" +
                    " left join ocs_trn_tdisbursementassignment d on d.rmdisbursementrequest_gid = a.rmdisbursementrequest_gid " +
                    " where a.disbursementassign_status = 'Assigned' and d.approval_status = 'Rejected' ";
            values.rejected_count = objdbconn.GetExecuteScalar(msSQL);
            int rejected_count = Convert.ToInt16(values.rejected_count);

            int totalcount = pending_count + assigned_count + rejected_count;
            values.lstotalcount = Convert.ToInt16(totalcount);
        }

        public void DaGetDisbursementMakerSummary(string employee_gid, MdlDisbursementRequest values)
        {
            try
            {
                msSQL = " select d.application_gid,e.application_no,e.customer_name as customer_name,e.customer_urn,d.sanction_refno, " +
                        " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as created_by, " +
                         " d.application2sanction_gid, d.application2loan_gid,e.vertical_name,e.region,d.rmdisbursementrequest_gid, " +
                         " d.lsareference_gid,d.lsareference_number,disbursement_to,d.disbursementrequest_code,d.product_type,a.approval_status " +
                         " from ocs_trn_tdisbursementassignment a " +
                         " left join ocs_trn_tcadapplication e on e.application_gid = a.application_gid " +
                         "  left join hrm_mst_temployee b on b.employee_gid = a.created_by " +
                         " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                         " left join ocs_trn_trmdisbursementrequest d on d.rmdisbursementrequest_gid = a.rmdisbursementrequest_gid " +
                         " where a.disbursementassign_status = 'Assigned' and a.creditopsmaker_gid = '" + employee_gid + "'" +
                         "  and a.maker_approvalflag = 'N' and a.approval_status != 'Rejected'" +
                         "  group by d.rmdisbursementrequest_gid order by a.created_date desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getdisbursementmeker_list = new List<disbursementmeker_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getdisbursementmeker_list.Add(new disbursementmeker_list
                        {
                            application_no = dt["application_no"].ToString(),
                            customer_name = dt["customer_name"].ToString(),
                            application_gid = dt["application_gid"].ToString(),
                            application2sanction_gid = dt["application2sanction_gid"].ToString(),
                            application2loan_gid = dt["application2loan_gid"].ToString(),
                            sanction_refno = dt["sanction_refno"].ToString(),
                            created_by = dt["created_by"].ToString(),
                            created_date = dt["created_date"].ToString(),
                            vertical_name = dt["vertical_name"].ToString(),
                            region = dt["region"].ToString(),
                            customer_urn = dt["customer_urn"].ToString(),
                            rmdisbursementrequest_gid = dt["rmdisbursementrequest_gid"].ToString(),
                            lsareference_gid = dt["lsareference_gid"].ToString(),
                            lsareference_number = dt["lsareference_number"].ToString(),
                            disbursementrequest_code = dt["disbursementrequest_code"].ToString(),
                            product_type = dt["product_type"].ToString(),
                            disbursement_to = dt["disbursement_to"].ToString()
                        });

                    }
                }
                values.disbursementmeker_list = getdisbursementmeker_list;
                dt_datatable.Dispose();

                values.status = true;
            }
            catch (Exception ex)
            {
                values.status = false;
            }
        }

        public void DaGetDisbursementFollowupMakerSummary(string employee_gid, MdlDisbursementRequest values)
        {
            try
            {
                msSQL = " select a.application_gid,e.application_no,e.customer_name as customer_name,e.customer_urn,a.sanction_refno, " +
                        " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date, concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as created_by, " +
                        " a.application2sanction_gid, a.application2loan_gid,e.vertical_name,e.region,d.rmdisbursementrequest_gid," +
                        " a.lsareference_gid,a.lsareference_number,a.disbursement_to,a.disbursementrequest_code,a.product_type,d.approval_status " +
                        " from ocs_trn_trmdisbursementrequest a " +
                        " left join ocs_trn_tcadapplication e on e.application_gid = a.application_gid" +
                        " left join hrm_mst_temployee b on b.employee_gid = a.created_by " +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " left join ocs_trn_tdisbursementassignment d on d.rmdisbursementrequest_gid = a.rmdisbursementrequest_gid " +
                        " where d.disbursementassign_status = 'Assigned' and d.creditopsmaker_gid = '" + employee_gid + "' and" +
                        " d.maker_approvalflag = 'Y' and d.checker_approvalflag = 'N' and d.approval_status != 'Rejected'" +
                        " order by a.created_date desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getdisbursementmekerfollowup_list = new List<disbursementmekerfollowup_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getdisbursementmekerfollowup_list.Add(new disbursementmekerfollowup_list
                        {
                            application_no = dt["application_no"].ToString(),
                            customer_name = dt["customer_name"].ToString(),
                            application_gid = dt["application_gid"].ToString(),
                            application2sanction_gid = dt["application2sanction_gid"].ToString(),
                            application2loan_gid = dt["application2loan_gid"].ToString(),
                            sanction_refno = dt["sanction_refno"].ToString(),
                            created_by = dt["created_by"].ToString(),
                            created_date = dt["created_date"].ToString(),
                            vertical_name = dt["vertical_name"].ToString(),
                            region = dt["region"].ToString(),
                            rmdisbursementrequest_gid = dt["rmdisbursementrequest_gid"].ToString(),
                            lsareference_gid = dt["lsareference_gid"].ToString(),
                            lsareference_number = dt["lsareference_number"].ToString(),
                            disbursementrequest_code = dt["disbursementrequest_code"].ToString(),
                            product_type = dt["product_type"].ToString(),
                            disbursement_to = dt["disbursement_to"].ToString()
                        });

                    }
                }
                values.disbursementmekerfollowup_list = getdisbursementmekerfollowup_list;
                dt_datatable.Dispose();

                values.status = true;
            }
            catch (Exception ex)
            {
                values.status = false;
            }
        }

        public void DaGetDisbursementCheckerSummary(string employee_gid, MdlDisbursementRequest values)
        {
            try
            {
                msSQL = " select a.application_gid,e.application_no,e.customer_name as customer_name,e.customer_urn,a.sanction_refno, " +
                        " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date, concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as created_by, " +
                        " a.application2sanction_gid, a.application2loan_gid,e.vertical_name,e.region,d.rmdisbursementrequest_gid," +
                        " a.lsareference_gid,a.lsareference_number,a.disbursement_to,a.disbursementrequest_code,a.product_type,d.approval_status " +
                        " from ocs_trn_trmdisbursementrequest a " +
                        " left join ocs_trn_tcadapplication e on e.application_gid = a.application_gid" +
                        " left join hrm_mst_temployee b on b.employee_gid = a.created_by " +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " left join ocs_trn_tdisbursementassignment d on d.rmdisbursementrequest_gid = a.rmdisbursementrequest_gid " +
                        " where d.disbursementassign_status = 'Assigned' and d.creditopschecker_gid = '" + employee_gid + "'" +
                        " and d.checker_approvalflag = 'N' and d.maker_approvalflag = 'Y' and d.approval_status != 'Rejected'" +
                        " order by a.created_date desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getdisbursementchecker_list = new List<disbursementchecker_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getdisbursementchecker_list.Add(new disbursementchecker_list
                        {
                            application_no = dt["application_no"].ToString(),
                            customer_urn = dt["customer_urn"].ToString(),
                            customer_name = dt["customer_name"].ToString(),
                            application_gid = dt["application_gid"].ToString(),
                            application2sanction_gid = dt["application2sanction_gid"].ToString(),
                            application2loan_gid = dt["application2loan_gid"].ToString(),
                            sanction_refno = dt["sanction_refno"].ToString(),
                            created_by = dt["created_by"].ToString(),
                            created_date = dt["created_date"].ToString(),
                            vertical_name = dt["vertical_name"].ToString(),
                            region = dt["region"].ToString(),
                            rmdisbursementrequest_gid = dt["rmdisbursementrequest_gid"].ToString(),
                            lsareference_gid = dt["lsareference_gid"].ToString(),
                            lsareference_number = dt["lsareference_number"].ToString(),
                            disbursementrequest_code = dt["disbursementrequest_code"].ToString(),
                            product_type = dt["product_type"].ToString(),
                            disbursement_to = dt["disbursement_to"].ToString()
                        });

                    }
                }
                values.disbursementchecker_list = getdisbursementchecker_list;
                dt_datatable.Dispose();

                values.status = true;
            }
            catch (Exception ex)
            {
                values.status = false;
            }
        }

        public void DaGetDisbursementCompletedSummary(string employee_gid, MdlDisbursementRequest values)
        {
            try
            {
                msSQL = " select a.application_gid,e.application_no,e.customer_name as customer_name,e.customer_urn,a.sanction_refno, " +
                        " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date, concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as created_by, " +
                        " a.application2sanction_gid, a.application2loan_gid,e.vertical_name,e.region, " +
                        " concat(g.user_firstname, ' ', g.user_lastname, ' / ', g.user_code) as checker_name, " +
                        " date_format(d.checker_approveddate, '%d-%m-%Y %h:%i %p') as checker_approveddate," +
                        " d.rmdisbursementrequest_gid,a.lsareference_gid,a.lsareference_number,a.disbursement_to," +
                        " a.disbursementrequest_code,a.product_type,d.approval_status " +
                        " from ocs_trn_trmdisbursementrequest a " +
                        " left join ocs_trn_tcadapplication e on e.application_gid = a.application_gid" +
                        " left join hrm_mst_temployee b on b.employee_gid = a.created_by " +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " left join ocs_trn_tdisbursementassignment d on d.rmdisbursementrequest_gid = a.rmdisbursementrequest_gid " +
                        " left join hrm_mst_temployee f on f.employee_gid = d.creditopschecker_gid " +
                        " left join adm_mst_tuser g on g.user_gid = f.user_gid " +
                        " where d.disbursementassign_status = 'Assigned' and d.checker_approvalflag = 'Y'" +
                        " order by d.checker_approveddate desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getdisbursementcompleted_list = new List<disbursementcompleted_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getdisbursementcompleted_list.Add(new disbursementcompleted_list
                        {
                            application_no = dt["application_no"].ToString(),
                            customer_urn = dt["customer_urn"].ToString(),
                            customer_name = dt["customer_name"].ToString(),
                            application_gid = dt["application_gid"].ToString(),
                            application2sanction_gid = dt["application2sanction_gid"].ToString(),
                            application2loan_gid = dt["application2loan_gid"].ToString(),
                            sanction_refno = dt["sanction_refno"].ToString(),
                            created_by = dt["created_by"].ToString(),
                            created_date = dt["created_date"].ToString(),
                            vertical_name = dt["vertical_name"].ToString(),
                            region = dt["region"].ToString(),
                            checker_name = dt["checker_name"].ToString(),
                            checker_approveddate = dt["checker_approveddate"].ToString(),
                            rmdisbursementrequest_gid = dt["rmdisbursementrequest_gid"].ToString(),
                            lsareference_gid = dt["lsareference_gid"].ToString(),
                            lsareference_number = dt["lsareference_number"].ToString(),
                            disbursementrequest_code = dt["disbursementrequest_code"].ToString(),
                            product_type = dt["product_type"].ToString(),
                            disbursement_to = dt["disbursement_to"].ToString()
                        });

                    }
                }
                values.disbursementcompleted_list = getdisbursementcompleted_list;
                dt_datatable.Dispose();

                values.status = true;
            }
            catch (Exception ex)
            {
                values.status = false;
            }
        }

        public void DaGetDisbursementEdit(string employee_gid, string application_gid, MdlDisbursementRequestAdd values)
        {
            msSQL = " select application_gid,application2sanction_gid,sanction_refno,application2loan_gid,product_type," +
                    " processing_fees,processing_gst,finance_charges,od_amount,escrow_payment,nach_status,remarks,amounttobe_disbursed, " +
                    " date_format(loandisbursement_date, '%d-%m-%Y %h:%i %p') as loandisbursement_date, date_format(loandisbursement_date, '%Y-%m-%d %h:%i:%s') as editloandisbursement_date " +
                    " from ocs_trn_trmdisbursementrequest a" +
                    " where application_gid='" + application_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.application_gid = objODBCDatareader["application_gid"].ToString();
                values.application2sanction_gid = objODBCDatareader["application2sanction_gid"].ToString();
                values.sanction_refno = objODBCDatareader["sanction_refno"].ToString();
                values.application2loan_gid = objODBCDatareader["application2loan_gid"].ToString();
                values.product_type = objODBCDatareader["product_type"].ToString();
                values.processing_fees = objODBCDatareader["processing_fees"].ToString();
                values.gst = objODBCDatareader["processing_gst"].ToString();
                values.finance_charges = objODBCDatareader["finance_charges"].ToString();
                values.od_amount = objODBCDatareader["od_amount"].ToString();
                values.escrow_payment = objODBCDatareader["escrow_payment"].ToString();
                values.nach_status = objODBCDatareader["nach_status"].ToString();
                values.remarks = objODBCDatareader["remarks"].ToString();
                values.loandisbursement_date = objODBCDatareader["loandisbursement_date"].ToString();
                values.amounttobe_disbursed = objODBCDatareader["amounttobe_disbursed"].ToString();
                values.editloandisbursement_date = objODBCDatareader["editloandisbursement_date"].ToString();
            }
            objODBCDatareader.Close();


        }

        public bool DaPostDisbursementUpdate(string employee_gid, MdlDisbursementRequestAdd values)
        {
            if (values.disbursement_to == "Applicant(B2B)")
            {
                msSQL = " select bankaccountstatus_gid from ocs_trn_tbankaccountstatus  " +
                   " where application_gid = '" + values.application_gid + "' and  " +
                   " rmdisbursementrequest_gid='" + values.rmdisbursementrequest_gid + "' and disbursementaccount_status='Yes'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == false)
                {
                    values.status = false;
                    values.message = "Kindly Update the Bank Account Status";
                    objODBCDatareader.Close();
                    return false;
                }
            }



            if (values.disbursement_to == "Applicant(B2B)")
            {
                if (values.updated_person == "RM")
                {
                    msSQL = " select disbursementamount_gid from ocs_trn_tdisbursementamount  " +
                            " where application_gid = '" + values.application_gid + "' and  " +
                            " rmdisbursementrequest_gid='" + values.rmdisbursementrequest_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == false)
                    {
                        values.status = false;
                        values.message = "Kindly Add the Disbursement Amount";
                        objODBCDatareader.Close();
                        return false;
                    }
                }
            }
            
                msSQL = " update ocs_trn_trmdisbursementrequest set " +
                        " remarks = '" + values.remarks.Replace("'", "") + "'," +
                        " updated_person = '" + values.updated_person + "'," +
                        " updated_by = '" + employee_gid + "'," +
                        " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                        " where rmdisbursementrequest_gid ='" + values.rmdisbursementrequest_gid + "' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult != 0)
                    {
                        msSQL = " update ocs_trn_trmdisbursementdocument set rmdisbursementrequest_gid='" + values.rmdisbursementrequest_gid + "'," +
                                " application_gid='" + values.application_gid + "'" +
                                " where application_gid ='" + employee_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " update ocs_trn_tfarmercontact set rmdisbursementrequest_gid='" + values.rmdisbursementrequest_gid + "'" +
                                " where rmdisbursementrequest_gid ='" + employee_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " update ocs_trn_tdisbursementsupplier set rmdisbursementrequest_gid='" + values.rmdisbursementrequest_gid + "'" +
                                " where rmdisbursementrequest_gid ='" + employee_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " update ocs_trn_tcontactcoapplicant set rmdisbursementrequest_gid='" + values.rmdisbursementrequest_gid + "'" +
                                " where rmdisbursementrequest_gid ='" + employee_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " update ocs_trn_tdisbapplicantbankdtl set rmdisbursementrequest_gid='" + values.rmdisbursementrequest_gid + "'" +
                              " where rmdisbursementrequest_gid ='" + employee_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " update ocs_trn_tdisbursementamount set rmdisbursementrequest_gid='" + values.rmdisbursementrequest_gid + "'" +
                                " where rmdisbursementrequest_gid ='" + employee_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " update ocs_trn_tbankaccountstatus set rmdisbursementrequest_gid='" + values.rmdisbursementrequest_gid + "'" +
                                " where rmdisbursementrequest_gid ='" + employee_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);                                          

                        values.status = true;
                        values.message = "Disbursement Request Updated Successfully";
                        return true;
                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error Occured while updating Disbursement";
                        return false;
                    }
            
        }

        public bool DaPostDisbursementApprove(string employee_gid, MdlDisbursementRequestAdd values)
        {
            if (values.disbursement_to == "Applicant(B2B)")
            {
                msSQL = " select  disbursementamount_gid from ocs_trn_tdisbursementamount  " +
                   " where application_gid = '" + values.application_gid + "' and  " +
                   " rmdisbursementrequest_gid='" + values.rmdisbursementrequest_gid + "'  and (checkerdisbursement_amount is not null or checkerdisbursement_amount <> '')";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == false)
                {
                    values.status = false;
                    values.message = "Kindly Add the Checker Disbursement Amount";
                    objODBCDatareader.Close();
                    return false;
                }

                msSQL = " select bankaccountstatus_gid from ocs_trn_tbankaccountstatus  " +
                      " where application_gid = '" + values.application_gid + "' and  " +
                      " rmdisbursementrequest_gid='" + values.rmdisbursementrequest_gid + "' and disbursementaccount_status='Yes'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == false)
                {
                    values.status = false;
                    values.message = "Kindly Update the Bank Account Status";
                    objODBCDatareader.Close();
                    return false;
                }
            }


            msSQL = " select application_gid,application2sanction_gid,sanction_refno,application2loan_gid,product_type," +
                    " processing_fees,processing_gst,finance_charges,od_amount,escrow_payment,nach_status,remarks," +
                    " updated_person,amounttobe_disbursed,disbursement_to,lsareference_gid,lsareference_number," +
                    " dispgstprocessing_fees," +
                    " additionalcharges_gst,dispgstadditionfees_charges,maker_remarks, " +
                    " date_format(loandisbursement_date, '%Y-%m-%d %h:%i:%s') as loandisbursement_date " +
                    " from ocs_trn_trmdisbursementrequest a" +
                    " where rmdisbursementrequest_gid='" + values.rmdisbursementrequest_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                lsapplication_gid = objODBCDatareader["application_gid"].ToString();
                lsapplication2sanction_gid = objODBCDatareader["application2sanction_gid"].ToString();
                lssanction_refno = objODBCDatareader["sanction_refno"].ToString();
                lsapplication2loan_gid = objODBCDatareader["application2loan_gid"].ToString();
                lsproduct_type = objODBCDatareader["product_type"].ToString();
                lsprocessing_fees = objODBCDatareader["processing_fees"].ToString();
                lsgst = objODBCDatareader["processing_gst"].ToString();
                lsfinance_charges = objODBCDatareader["finance_charges"].ToString();
                lsod_amount = objODBCDatareader["od_amount"].ToString();
                lsescrow_payment = objODBCDatareader["escrow_payment"].ToString();
                lsnach_status = objODBCDatareader["nach_status"].ToString();
                lsremarks = objODBCDatareader["remarks"].ToString();
                lsupdated_person = objODBCDatareader["updated_person"].ToString();
                lsamounttobe_disbursed = objODBCDatareader["amounttobe_disbursed"].ToString();
                lsareference_gid = objODBCDatareader["lsareference_gid"].ToString();
                lsareference_number = objODBCDatareader["lsareference_number"].ToString();
                lsdispgstprocessing_fees = objODBCDatareader["dispgstprocessing_fees"].ToString();
                lsadditionalcharges_gst = objODBCDatareader["additionalcharges_gst"].ToString();
                lsdispgstadditionfees_charges = objODBCDatareader["dispgstadditionfees_charges"].ToString();
                lsmaker_remarks = objODBCDatareader["maker_remarks"].ToString();
                lsdisbursement_to = objODBCDatareader["disbursement_to"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " update ocs_trn_trmdisbursementrequest set " +
                   " processing_fees='" + values.processing_fees + "'," +
                   " processing_gst = '" + values.processing_gst + "'," +
                   " dispgstprocessing_fees = '" + values.dispgstprocessing_fees + "'," +
                   " od_amount = '" + values.od_amount + "'," +
                   " finance_charges = '" + values.finance_charges + "'," +
                   " additionalcharges_gst = '" + values.additionalcharges_gst + "'," +
                   " dispgstadditionfees_charges = '" + values.dispgstadditionfees_charges + "'," +
                   " escrow_payment='" + values.escrow_payment + "'," +
                   " nach_status = '" + values.nach_status + "'," +
                   " checker_remarks = '" + values.checker_remarks.Replace("'", "") + "'," +
                   " updated_person = '" + values.updated_person + "'," +
                   " disbursementassign_status = 'Approved' ," +
                   " updated_by = '" + employee_gid + "'," +
                   " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                   " where rmdisbursementrequest_gid ='" + values.rmdisbursementrequest_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            msSQL = " update ocs_trn_tdisbursementassignment set checker_approvalflag='Y'," +
                    " checker_approveddate = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    " where rmdisbursementrequest_gid ='" + values.rmdisbursementrequest_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msSQL = " update ocs_trn_trmdisbursementdocument set rmdisbursementrequest_gid='" + values.rmdisbursementrequest_gid + "'," +
                   " application_gid='" + values.application_gid + "'" +
                   " where application_gid ='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update ocs_trn_tfarmercontact set rmdisbursementrequest_gid='" + values.rmdisbursementrequest_gid + "'" +
                        " where rmdisbursementrequest_gid ='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update ocs_trn_tdisbursementsupplier set rmdisbursementrequest_gid='" + values.rmdisbursementrequest_gid + "'" +
                        " where rmdisbursementrequest_gid ='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update ocs_trn_tcontactcoapplicant set rmdisbursementrequest_gid='" + values.rmdisbursementrequest_gid + "'" +
                        " where rmdisbursementrequest_gid ='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                msSQL = " update ocs_trn_tdisbapplicantbankdtl set rmdisbursementrequest_gid='" + values.rmdisbursementrequest_gid + "'" +
                      " where rmdisbursementrequest_gid ='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update ocs_trn_tdisbursementamount set rmdisbursementrequest_gid='" + values.rmdisbursementrequest_gid + "'" +
                        " where rmdisbursementrequest_gid ='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update ocs_trn_tbankaccountstatus set rmdisbursementrequest_gid='" + values.rmdisbursementrequest_gid + "'" +
                        " where rmdisbursementrequest_gid ='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msGetGid = objcmnfunctions.GetMasterGID("DRLO");
                msSQL = " insert into ocs_trn_trmdisbursementrequestlog(" +
                        " rmdisbursementrequestlog_gid ," +
                        " application_gid," +
                        " application2sanction_gid," +
                        " sanction_refno," +
                        " application2loan_gid," +
                        " product_type," +
                        " processing_fees," +
                        " processing_gst," +
                        " finance_charges," +
                        " od_amount," +
                        " escrow_payment," +
                        " nach_status," +
                        " remarks," +
                        " updated_person," +
                        " amounttobe_disbursed," +
                        " loandisbursement_date," +
                        " disbursement_to," +
                        " lsareference_gid," +
                        " lsareference_number," +
                        " dispgstprocessing_fees," +
                        " additionalcharges_gst," +
                        " maker_remarks," +
                        " rmdisbursementrequest_gid," +
                        " updated_by," +
                        " updated_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + lsapplication_gid + "'," +
                        "'" + lsapplication2sanction_gid + "'," +
                        "'" + lssanction_refno + "'," +
                        "'" + lsapplication2loan_gid + "'," +
                        "'" + lsproduct_type + "'," +
                        "'" + lsprocessing_fees + "'," +
                        "'" + lsgst + "'," +
                        "'" + lsfinance_charges + "'," +
                        "'" + lsod_amount + "'," +
                        "'" + lsescrow_payment + "'," +
                        "'" + lsnach_status + "'," +
                        "'" + lsremarks.Replace("'", "") + "'," +
                        "'" + lsupdated_person + "'," +
                        "'" + lsamounttobe_disbursed + "',";
                if ((lsloandisbursement_date == null) || (lsloandisbursement_date == ""))
                {
                    msSQL += "'" + null + "',";
                }
                else
                {
                    msSQL += "'" + lsloandisbursement_date + "',";
                }
                msSQL += "'" + lsdisbursement_to + "'," +
                        "'" + lsareference_gid + "'," +
                        "'" + lsareference_number + "'," +
                        "'" + lsdispgstprocessing_fees + "'," +
                        "'" + lsadditionalcharges_gst + "'," +
                        "'" + lsmaker_remarks + "'," +
                        "'" + values.rmdisbursementrequest_gid + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Disbursement Approved Successfully! ";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while updating Disbursement";
                return false;
            }
        }

        public void DaDisbursementCount(string user_gid, string employee_gid, DisbursementAssignCount values)
        {
            msSQL = " select count(a.rmdisbursementrequest_gid) as makerpending_count from ocs_trn_tdisbursementassignment a  where a.disbursementassign_status = 'Assigned' " +
                    " and a.creditopsmaker_gid = '" + employee_gid + "' and a.maker_approvalflag = 'N' and a.approval_status != 'Rejected'";
            values.makerpending_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select count(a.rmdisbursementrequest_gid) as makerfollowup_count from ocs_trn_tdisbursementassignment a  where a.disbursementassign_status = 'Assigned'" +
                    " and a.creditopsmaker_gid = '" + employee_gid + "' and a.maker_approvalflag = 'Y' and a.checker_approvalflag = 'N'  and a.approval_status != 'Rejected'";
            values.makerfollowup_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select count(a.rmdisbursementrequest_gid) as checker_count from ocs_trn_tdisbursementassignment a  where a.disbursementassign_status = 'Assigned' " +
                    " and a.creditopschecker_gid = '" + employee_gid + "' and a.checker_approvalflag = 'N' and a.maker_approvalflag = 'Y' and a.approval_status != 'Rejected'";
            values.checker_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select count(a.rmdisbursementrequest_gid) as approvedcompleted_count from ocs_trn_trmdisbursementrequest a " +
                    " left join ocs_trn_tdisbursementassignment d on d.rmdisbursementrequest_gid = a.rmdisbursementrequest_gid " +
                    " where d.disbursementassign_status = 'Assigned'" +
                    " and d.checker_approvalflag = 'Y' and d.approval_status != 'Rejected'";
            values.approvedcompleted_count = objdbconn.GetExecuteScalar(msSQL);
        }

        public void DaGetRMDisbursementDtlView(string application_gid, MdlDisbursementDtlView values)
        {
            try
            {
                msSQL = " select updated_person from ocs_trn_trmdisbursementrequest where application_gid='" + application_gid + "'";
                lsrmupdated_person = objdbconn.GetExecuteScalar(msSQL);

                if (lsrmupdated_person == "RM")
                {
                    msSQL = " select a.application_gid,a.application2sanction_gid,a.sanction_refno,a.application2loan_gid,a.product_type,a.processing_fees, " +
                            " a.processing_gst,a.finance_charges,a.od_amount,a.escrow_payment,a.nach_status,a.remarks,a.amounttobe_disbursed, " +
                            " date_format(a.loandisbursement_date, '%d-%m-%Y %h:%i %p') as loandisbursement_date " +
                            " from ocs_trn_trmdisbursementrequest a" +
                            " where a.application_gid='" + application_gid + "' and updated_person= 'RM' ";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        values.application_gid = objODBCDatareader["application_gid"].ToString();
                        values.application2sanction_gid = objODBCDatareader["application2sanction_gid"].ToString();
                        values.sanction_refno = objODBCDatareader["sanction_refno"].ToString();
                        values.application2loan_gid = objODBCDatareader["application2loan_gid"].ToString();
                        values.product_type = objODBCDatareader["product_type"].ToString();
                        values.processing_fees = objODBCDatareader["processing_fees"].ToString();
                        values.gst = objODBCDatareader["processing_gst"].ToString();
                        values.finance_charges = objODBCDatareader["finance_charges"].ToString();
                        values.od_amount = objODBCDatareader["od_amount"].ToString();
                        values.escrow_payment = objODBCDatareader["escrow_payment"].ToString();
                        values.nach_status = objODBCDatareader["nach_status"].ToString();
                        values.remarks = objODBCDatareader["remarks"].ToString();
                        values.amounttobe_disbursed = objODBCDatareader["amounttobe_disbursed"].ToString();
                        values.loandisbursement_date = objODBCDatareader["loandisbursement_date"].ToString();
                    }
                    values.status = true;
                    values.message = "success";
                    objODBCDatareader.Close();
                }
                else
                {
                    msSQL = " select a.application_gid,a.application2sanction_gid,a.sanction_refno,a.application2loan_gid,a.product_type,a.processing_fees, " +
                            " a.processing_gst,a.finance_charges,a.od_amount,a.escrow_payment,a.nach_status,a.remarks,a.amounttobe_disbursed, " +
                            " date_format(a.loandisbursement_date, '%d-%m-%Y %h:%i %p') as loandisbursement_date " +
                            " from ocs_trn_trmdisbursementrequestlog a" +
                            " where a.application_gid='" + application_gid + "' and updated_person= 'RM' ";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        values.application_gid = objODBCDatareader["application_gid"].ToString();
                        values.application2sanction_gid = objODBCDatareader["application2sanction_gid"].ToString();
                        values.sanction_refno = objODBCDatareader["sanction_refno"].ToString();
                        values.application2loan_gid = objODBCDatareader["application2loan_gid"].ToString();
                        values.product_type = objODBCDatareader["product_type"].ToString();
                        values.processing_fees = objODBCDatareader["processing_fees"].ToString();
                        values.gst = objODBCDatareader["processing_gst"].ToString();
                        values.finance_charges = objODBCDatareader["finance_charges"].ToString();
                        values.od_amount = objODBCDatareader["od_amount"].ToString();
                        values.escrow_payment = objODBCDatareader["escrow_payment"].ToString();
                        values.nach_status = objODBCDatareader["nach_status"].ToString();
                        values.remarks = objODBCDatareader["remarks"].ToString();
                        values.amounttobe_disbursed = objODBCDatareader["amounttobe_disbursed"].ToString();
                        values.loandisbursement_date = objODBCDatareader["loandisbursement_date"].ToString();
                    }
                    values.status = true;
                    values.message = "success";
                    objODBCDatareader.Close();
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "failure";
            }

        }

        public void DaGetMakerDisbursementDtlView(string application_gid, MdlDisbursementDtlView values)
        {
            try
            {
                msSQL = " select updated_person from ocs_trn_trmdisbursementrequest where application_gid='" + application_gid + "'";
                lsrmupdated_person = objdbconn.GetExecuteScalar(msSQL);

                if (lsrmupdated_person == "Maker")
                {
                    msSQL = " select a.application_gid,a.application2sanction_gid,a.sanction_refno,a.application2loan_gid,a.product_type,a.processing_fees, " +
                            " a.gst,a.finance_charges,a.od_amount,a.escrow_payment,a.nach_status,a.remarks,a.amounttobe_disbursed, " +
                            " date_format(a.loandisbursement_date, '%d-%m-%Y %h:%i %p') as loandisbursement_date " +
                            " from ocs_trn_trmdisbursementrequest a" +
                            " where a.application_gid='" + application_gid + "' and updated_person= 'Maker' ";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        values.application_gid = objODBCDatareader["application_gid"].ToString();
                        values.application2sanction_gid = objODBCDatareader["application2sanction_gid"].ToString();
                        values.sanction_refno = objODBCDatareader["sanction_refno"].ToString();
                        values.application2loan_gid = objODBCDatareader["application2loan_gid"].ToString();
                        values.product_type = objODBCDatareader["product_type"].ToString();
                        values.processing_fees = objODBCDatareader["processing_fees"].ToString();
                        values.gst = objODBCDatareader["gst"].ToString();
                        values.finance_charges = objODBCDatareader["finance_charges"].ToString();
                        values.od_amount = objODBCDatareader["od_amount"].ToString();
                        values.escrow_payment = objODBCDatareader["escrow_payment"].ToString();
                        values.nach_status = objODBCDatareader["nach_status"].ToString();
                        values.remarks = objODBCDatareader["remarks"].ToString();
                        values.amounttobe_disbursed = objODBCDatareader["amounttobe_disbursed"].ToString();
                        values.loandisbursement_date = objODBCDatareader["loandisbursement_date"].ToString();
                    }
                    values.status = true;
                    values.message = "success";
                    objODBCDatareader.Close();
                }
                else
                {
                    msSQL = " select a.application_gid,a.application2sanction_gid,a.sanction_refno,a.application2loan_gid,a.product_type,a.processing_fees, " +
                            " a.gst,a.finance_charges,a.od_amount,a.escrow_payment,a.nach_status,a.remarks,a.amounttobe_disbursed," +
                            " date_format(a.loandisbursement_date, '%d-%m-%Y %h:%i %p') as loandisbursement_date " +
                            " from ocs_trn_trmdisbursementrequestlog a" +
                            " where a.application_gid='" + application_gid + "' and updated_person= 'Maker' ";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        values.application_gid = objODBCDatareader["application_gid"].ToString();
                        values.application2sanction_gid = objODBCDatareader["application2sanction_gid"].ToString();
                        values.sanction_refno = objODBCDatareader["sanction_refno"].ToString();
                        values.application2loan_gid = objODBCDatareader["application2loan_gid"].ToString();
                        values.product_type = objODBCDatareader["product_type"].ToString();
                        values.processing_fees = objODBCDatareader["processing_fees"].ToString();
                        values.gst = objODBCDatareader["gst"].ToString();
                        values.finance_charges = objODBCDatareader["finance_charges"].ToString();
                        values.od_amount = objODBCDatareader["od_amount"].ToString();
                        values.escrow_payment = objODBCDatareader["escrow_payment"].ToString();
                        values.nach_status = objODBCDatareader["nach_status"].ToString();
                        values.remarks = objODBCDatareader["remarks"].ToString();
                        values.amounttobe_disbursed = objODBCDatareader["amounttobe_disbursed"].ToString();
                        values.loandisbursement_date = objODBCDatareader["loandisbursement_date"].ToString();
                    }
                    values.status = true;
                    values.message = "success";
                    objODBCDatareader.Close();
                }
            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }

        }

        public void DaGetCheckerDisbursementDtlView(string application_gid, MdlDisbursementDtlView values)
        {
            try
            {
                msSQL = " select updated_person from ocs_trn_trmdisbursementrequest where application_gid='" + application_gid + "'";
                lsrmupdated_person = objdbconn.GetExecuteScalar(msSQL);

                if (lsrmupdated_person == "Checker")
                {
                    msSQL = " select a.application_gid,a.application2sanction_gid,a.sanction_refno,a.application2loan_gid,a.product_type,a.processing_fees, " +
                            " a.gst,a.finance_charges,a.od_amount,a.escrow_payment,a.nach_status,a.remarks,a.amounttobe_disbursed, " +
                            " date_format(a.loandisbursement_date, '%d-%m-%Y %h:%i %p') as loandisbursement_date " +
                            " from ocs_trn_trmdisbursementrequest a" +
                            " where a.application_gid='" + application_gid + "' and updated_person= 'Checker' ";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        values.application_gid = objODBCDatareader["application_gid"].ToString();
                        values.application2sanction_gid = objODBCDatareader["application2sanction_gid"].ToString();
                        values.sanction_refno = objODBCDatareader["sanction_refno"].ToString();
                        values.application2loan_gid = objODBCDatareader["application2loan_gid"].ToString();
                        values.product_type = objODBCDatareader["product_type"].ToString();
                        values.processing_fees = objODBCDatareader["processing_fees"].ToString();
                        values.gst = objODBCDatareader["gst"].ToString();
                        values.finance_charges = objODBCDatareader["finance_charges"].ToString();
                        values.od_amount = objODBCDatareader["od_amount"].ToString();
                        values.escrow_payment = objODBCDatareader["escrow_payment"].ToString();
                        values.nach_status = objODBCDatareader["nach_status"].ToString();
                        values.remarks = objODBCDatareader["remarks"].ToString();
                        values.amounttobe_disbursed = objODBCDatareader["amounttobe_disbursed"].ToString();
                        values.loandisbursement_date = objODBCDatareader["loandisbursement_date"].ToString();
                    }
                    values.status = true;
                    values.message = "success";
                    objODBCDatareader.Close();
                }
                else
                {
                    msSQL = " select a.application_gid,a.application2sanction_gid,a.sanction_refno,a.application2loan_gid,a.product_type,a.processing_fees, " +
                            " a.gst,a.finance_charges,a.od_amount,a.escrow_payment,a.nach_status,a.remarks,a.amounttobe_disbursed, " +
                            " date_format(a.loandisbursement_date, '%d-%m-%Y %h:%i %p') as loandisbursement_date " +
                            " from ocs_trn_trmdisbursementrequestlog a " +
                            " where a.application_gid='" + application_gid + "' and updated_person= 'Checker' ";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        values.application_gid = objODBCDatareader["application_gid"].ToString();
                        values.application2sanction_gid = objODBCDatareader["application2sanction_gid"].ToString();
                        values.sanction_refno = objODBCDatareader["sanction_refno"].ToString();
                        values.application2loan_gid = objODBCDatareader["application2loan_gid"].ToString();
                        values.product_type = objODBCDatareader["product_type"].ToString();
                        values.processing_fees = objODBCDatareader["processing_fees"].ToString();
                        values.gst = objODBCDatareader["gst"].ToString();
                        values.finance_charges = objODBCDatareader["finance_charges"].ToString();
                        values.od_amount = objODBCDatareader["od_amount"].ToString();
                        values.escrow_payment = objODBCDatareader["escrow_payment"].ToString();
                        values.nach_status = objODBCDatareader["nach_status"].ToString();
                        values.remarks = objODBCDatareader["remarks"].ToString();
                        values.amounttobe_disbursed = objODBCDatareader["amounttobe_disbursed"].ToString();
                        values.loandisbursement_date = objODBCDatareader["loandisbursement_date"].ToString();
                    }
                    values.status = true;
                    values.message = "success";
                    objODBCDatareader.Close();
                }
            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }

        }
        public void DaGetSancLsApprovalFlag(string application_gid, MdlSancLsApprovalFlag values)
        {
            try
            {
                msSQL = " select application_gid, approver_approvalflag " +
                        " from ocs_trn_tprocesstype_assign a" +
                        " where application_gid ='" + application_gid + "' and menu_gid='CADMGTLSA'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows)
                {
                    values.application_gid = objODBCDataReader["application_gid"].ToString();
                    values.approver_approvalflag = objODBCDataReader["approver_approvalflag"].ToString();
                }
                objODBCDataReader.Close();
            }
            catch (Exception ex)
            {
                values.message = ex.ToString();
                values.status = false;
            }
        }
        public void DaGetDisbursementDocSummary(string employee_gid, string application_gid, MdlDisbursementRequest values)
        {
            try
            {
                msSQL = " select rmdisbursementdocument_gid,application_gid,document_name,document_path,document_title, " +
                          " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as created_by, " +
                          " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date from ocs_trn_trmdisbursementdocument a" +
                          " left join hrm_mst_temployee b on b.employee_gid = a.created_by " +
                          " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                          "where application_gid='" + application_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getdisbursementuploaddocument_list = new List<disbursementuploaddocument_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getdisbursementuploaddocument_list.Add(new disbursementuploaddocument_list
                        {
                            document_name = dt["document_name"].ToString(),
                            document_title = dt["document_title"].ToString(),
                            document_path = dt["document_path"].ToString(),
                            application_gid = dt["application_gid"].ToString(),
                            rmdisbursementdocument_gid = dt["rmdisbursementdocument_gid"].ToString(),
                            created_by = dt["created_by"].ToString(),
                            created_date = dt["created_date"].ToString(),

                        });
                        values.disbursementuploaddocument_list = getdisbursementuploaddocument_list;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = ex.ToString();
                values.status = false;
            }
        }

        public void DaGetDisbursementRequestEdit(string rmdisbursementrequest_gid, MdlDisbursementRequestAdd values)
        {
            try
            {
                msSQL = " select rmdisbursementrequest_gid, a.application_gid,application2sanction_gid,sanction_refno, " +
                        " application2loan_gid,product_type, processing_fees,processing_gst,finance_charges,od_amount,escrow_payment, " +
                        " nach_status,remarks,disbursementassign_status,updated_person,loandisbursement_date,amounttobe_disbursed " +
                        " from ocs_trn_trmdisbursementrequest a " +
                        " where a.rmdisbursementrequest_gid='" + rmdisbursementrequest_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);

                if (objODBCDatareader.HasRows == true)
                {
                    values.rmdisbursementrequest_gid = objODBCDatareader["rmdisbursementrequest_gid"].ToString();
                    values.application_gid = objODBCDatareader["application_gid"].ToString();
                    values.application2sanction_gid = objODBCDatareader["application2sanction_gid"].ToString();
                    values.sanction_refno = objODBCDatareader["sanction_refno"].ToString();
                    values.application2loan_gid = objODBCDatareader["application2loan_gid"].ToString();
                    values.product_type = objODBCDatareader["product_type"].ToString();
                    values.processing_fees = objODBCDatareader["processing_fees"].ToString();
                    values.gst = objODBCDatareader["processing_gst"].ToString();
                    values.finance_charges = objODBCDatareader["finance_charges"].ToString();
                    values.od_amount = objODBCDatareader["od_amount"].ToString();
                    values.escrow_payment = objODBCDatareader["escrow_payment"].ToString();
                    values.nach_status = objODBCDatareader["nach_status"].ToString();
                    values.remarks = objODBCDatareader["remarks"].ToString();
                    values.disbursementassign_status = objODBCDatareader["disbursementassign_status"].ToString();
                    values.updated_person = objODBCDatareader["updated_person"].ToString();
                    values.loandisbursement_date = objODBCDatareader["loandisbursement_date"].ToString();
                    values.amounttobe_disbursed = objODBCDatareader["amounttobe_disbursed"].ToString();
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
        public void DaGetDisbursementDocEditSummary(string employee_gid, string rmdisbursementrequest_gid, MdlDisbursementRequest values)
        {
            try
            {
                msSQL = " select rmdisbursementdocument_gid,application_gid,document_name,document_path,document_title, " +
                         " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as created_by, " +
                         " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date from ocs_trn_trmdisbursementdocument a" +
                         " left join hrm_mst_temployee b on b.employee_gid = a.created_by " +
                         " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                         " where rmdisbursementrequest_gid='" + rmdisbursementrequest_gid + "' " +
                         " or rmdisbursementrequest_gid='" + employee_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getdisbursementuploaddocument_list = new List<disbursementuploaddocument_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getdisbursementuploaddocument_list.Add(new disbursementuploaddocument_list
                        {
                            document_name = dt["document_name"].ToString(),
                            document_title = dt["document_title"].ToString(),
                            document_path = dt["document_path"].ToString(),
                            application_gid = dt["application_gid"].ToString(),
                            rmdisbursementdocument_gid = dt["rmdisbursementdocument_gid"].ToString(),
                            created_by = dt["created_by"].ToString(),
                            created_date = dt["created_date"].ToString(),

                        });
                        values.disbursementuploaddocument_list = getdisbursementuploaddocument_list;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = ex.ToString();
                values.status = false;
            }
        }
        public void DaPostDisbDocumentDeferral(MdlDisbDocumentDeferral values, string employee_gid)
        {
            msSQL = " select disbursementdocdeferral_gid from ocs_mst_tdisbursementdocdeferral a " +
                    " where a.vertical_gid = '" + values.vertical_gid + "' and " +
                    " a.customer_type = '" + values.customer_type + "' and a.disbursementdocdeferral_status='Y'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.message = "This Customer Type already Added.Kindly, Inactive that and again add the Customer Type";
                values.status = false;
                return;
            }
            else
            {
                lswefdate = values.wef_date;
                lswefdatetime = GetDateFormat(lswefdate);

                msGetGid = objcmnfunctions.GetMasterGID("DSDD");
                msSQL = " insert into ocs_mst_tdisbursementdocdeferral(" +
                        " disbursementdocdeferral_gid ," +
                        " vertical_gid ," +
                        " wef_date," +
                        " customer_type," +
                        " disbursementdocdeferral_status," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + values.vertical_gid + "'," +
                        "'" + lswefdatetime + "'," +
                        "'" + values.customer_type + "'," +
                        "'Y'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Document Deferral Details Added successfully";
                }
                else
                {
                    values.message = "Error Occured while Adding";
                    values.status = false;
                }
            }
        }

        public void DaGetDisbDocumentDeferralSummary(string employee_gid, string vertical_gid, MdlDisbDocumentDeferral values)
        {
            try
            {
                msSQL = " select disbursementdocdeferral_gid,vertical_gid, customer_type, disbursementdocdeferral_status," +
                        " date_format(a.wef_date, '%d-%m-%Y %h:%i %p') as wef_date, " +
                         " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as created_by, " +
                         " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date " +
                         " from ocs_mst_tdisbursementdocdeferral a" +
                         " left join hrm_mst_temployee b on b.employee_gid = a.created_by " +
                         " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                         " where vertical_gid='" + vertical_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getdisbdocumentdeferral_list = new List<disbdocumentdeferral_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getdisbdocumentdeferral_list.Add(new disbdocumentdeferral_list
                        {
                            disbursementdocdeferral_gid = dt["disbursementdocdeferral_gid"].ToString(),
                            vertical_gid = dt["vertical_gid"].ToString(),
                            customer_type = dt["customer_type"].ToString(),
                            disbursementdocdeferral_status = dt["disbursementdocdeferral_status"].ToString(),
                            wef_date = dt["wef_date"].ToString(),
                            created_by = dt["created_by"].ToString(),
                            created_date = dt["created_date"].ToString(),

                        });
                        values.disbdocumentdeferral_list = getdisbdocumentdeferral_list;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = ex.ToString();
                values.status = false;
            }
        }
        public void DaGetDeviationApprovalGroupName(string employee_gid, MdlDeviationApprovalDropDown values)
        {
            msSQL = " Select a.deviationapprovalgroup_gid,deviationapprovalgroup_name from ocs_mst_tdeviationapprovalgroup a " +
                    " where deviationapprovalgroup_status='Y' order by a.created_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdeviationgroup_list = new List<deviationgroup_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getdeviationgroup_list.Add(new deviationgroup_list
                    {
                        deviationapprovalgroup_gid = (dr_datarow["deviationapprovalgroup_gid"].ToString()),
                        deviationapprovalgroup_name = (dr_datarow["deviationapprovalgroup_name"].ToString())
                    });
                }
                values.deviationgroup_list = getdeviationgroup_list;
            }
            dt_datatable.Dispose();

        }
        public void DaGetDeviationApprovalSubGroupName(string employee_gid, string deviationapprovalgroup_gid, MdlDeviationApprovalDropDown values)
        {
            msSQL = " Select a.deviationapprovalgroup_gid,subgroup_name from ocs_mst_tdeviationapprovalgroup a " +
                    "  where deviationapprovalgroup_gid='" + deviationapprovalgroup_gid + "' and deviationapprovalgroup_status='Y' order by a.created_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdeviationsubgroup_list = new List<deviationsubgroup_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getdeviationsubgroup_list.Add(new deviationsubgroup_list
                    {
                        subgroup_gid = (dr_datarow["deviationapprovalgroup_gid"].ToString()),
                        subgroup_name = (dr_datarow["subgroup_name"].ToString())
                    });
                }
                values.deviationsubgroup_list = getdeviationsubgroup_list;
            }
            dt_datatable.Dispose();

        }
        public void DaGetDeviationApprovalMemberName(string employee_gid, string deviationapprovalgroup_gid, MdlDeviationApprovalDropDown values)
        {
            msSQL = " select deviationapprovalgroupmember_gid,employee_gid,employee_name, deviationapprovalgroup_gid " +
                    " from ocs_mst_tdeviationapprovalgroup_member " +
                    " where deviationapprovalgroup_gid='" + deviationapprovalgroup_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdeviationmember_list = new List<deviationmember_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getdeviationmember_list.Add(new deviationmember_list
                    {
                        deviationapprovalgroupmember_gid = dt["deviationapprovalgroupmember_gid"].ToString(),
                        employee_gid = dt["employee_gid"].ToString(),
                        employee_name = dt["employee_name"].ToString(),
                    });
                    values.deviationmember_list = getdeviationmember_list;
                }
            }
            dt_datatable.Dispose();

        }
        public void DaGetDeviationApprovalManagerName(string employee_gid, string deviationapprovalgroup_gid, MdlDeviationApprovalDropDown values)
        {
            msSQL = " select deviationapprovalgroupmanager_gid,employee_gid,employee_name, deviationapprovalgroup_gid " +
                    " from ocs_mst_tdeviationapprovalgroup_manager " +
                    " where deviationapprovalgroup_gid='" + deviationapprovalgroup_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdeviationmanager_list = new List<deviationmanager_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getdeviationmanager_list.Add(new deviationmanager_list
                    {
                        deviationapprovalgroupmanager_gid = dt["deviationapprovalgroupmanager_gid"].ToString(),
                        employee_gid = dt["employee_gid"].ToString(),
                        employee_name = dt["employee_name"].ToString(),
                    });
                    values.deviationmanager_list = getdeviationmanager_list;
                }
            }
            dt_datatable.Dispose();

        }
        public void DaPostDisbDocDefApprovalConfig(MdlDisbDocumentDeferral values, string employee_gid)
        {
            msGetGid = objcmnfunctions.GetMasterGID("DSAC");
            msSQL = " insert into ocs_mst_tdisbdocdeferralapprovalconfig(" +
                    " disbdocdeferralapprovalconfig_gid ," +
                    " disbursementdocdeferral_gid ," +
                    " vertical_gid," +
                    " group_gid," +
                    " group_name," +
                    " subgroup_gid ," +
                    " subgroup_name ," +
                    " manager_gid," +
                    " manager_name," +
                    " member_gid," +
                    " member_name," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.disbursementdocdeferral_gid + "'," +
                    "'" + values.vertical_gid + "'," +
                    "'" + values.group_gid + "'," +
                    "'" + values.group_name + "'," +
                    "'" + values.subgroup_gid + "'," +
                    "'" + values.subgroup_name + "'," +
                    "'" + values.manager_gid + "'," +
                    "'" + values.manager_name + "'," +
                    "'" + values.member_gid + "'," +
                    "'" + values.member_name + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Approval Config Added successfully";
            }
            else
            {
                values.message = "Error Occured while Adding";
                values.status = false;
            }

        }
        public void DaGetDisbDoDefApprovalConfigSummary(string employee_gid, string vertical_gid, MdlDisbDocumentDeferral values)
        {
            try
            {
                msSQL = " select a.disbdocdeferralapprovalconfig_gid,d.disbursementdocdeferral_gid, d.vertical_gid, group_gid," +
                        " group_name,subgroup_gid,subgroup_name,manager_gid,manager_name,member_gid,member_name, " +
                        " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as created_by, " +
                        " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date, " +
                        " disbursementdocdeferral_status,date_format(d.wef_date, '%d-%m-%Y %h:%i %p') as wef_date,d.customer_type " +
                        " from ocs_mst_tdisbdocdeferralapprovalconfig a" +
                        " left join hrm_mst_temployee b on b.employee_gid = a.created_by " +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " left join ocs_mst_tdisbursementdocdeferral d on d.disbursementdocdeferral_gid = a.disbursementdocdeferral_gid " +
                        " where d.vertical_gid='" + vertical_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getdisbdocdefapprovalconfig_list = new List<disbdocdefapprovalconfig_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getdisbdocdefapprovalconfig_list.Add(new disbdocdefapprovalconfig_list
                        {
                            disbdocdeferralapprovalconfig_gid = dt["disbdocdeferralapprovalconfig_gid"].ToString(),
                            disbursementdocdeferral_gid = dt["disbursementdocdeferral_gid"].ToString(),
                            vertical_gid = dt["vertical_gid"].ToString(),
                            group_gid = dt["group_gid"].ToString(),
                            group_name = dt["group_name"].ToString(),
                            subgroup_gid = dt["subgroup_gid"].ToString(),
                            subgroup_name = dt["subgroup_name"].ToString(),
                            manager_gid = dt["manager_gid"].ToString(),
                            manager_name = dt["manager_name"].ToString(),
                            member_gid = dt["member_gid"].ToString(),
                            member_name = dt["member_name"].ToString(),
                            created_by = dt["created_by"].ToString(),
                            created_date = dt["created_date"].ToString(),
                            wef_date = dt["wef_date"].ToString(),
                            customer_type = dt["customer_type"].ToString(),
                            disbursementdocdeferral_status = dt["disbursementdocdeferral_status"].ToString()
                        });
                        values.disbdocdefapprovalconfig_list = getdisbdocdefapprovalconfig_list;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = ex.ToString();
                values.status = false;
            }
        }
        public void DaGetDisbDeferralDocView(string disbursementdocdeferral_gid, MdlDisbDocumentDeferral values)
        {
            try
            {
                msSQL = " select disbursementdocdeferral_gid, wef_date,customer_type,disbursementdocdeferral_status " +
                        " from ocs_mst_tdisbursementdocdeferral a" +
                        " where disbursementdocdeferral_gid ='" + disbursementdocdeferral_gid + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows)
                {
                    values.disbursementdocdeferral_gid = objODBCDataReader["disbursementdocdeferral_gid"].ToString();
                    values.wef_date = objODBCDataReader["wef_date"].ToString();
                    values.customer_type = objODBCDataReader["customer_type"].ToString();
                    values.disbursementdocdeferral_status = objODBCDataReader["disbursementdocdeferral_status"].ToString();
                }
                objODBCDataReader.Close();
            }
            catch (Exception ex)
            {
                values.message = ex.ToString();
                values.status = false;
            }
        }
        public void DaPostDisbDeferralDocInactive(MdlDisbDocumentDeferral values, string employee_gid)
        {
            if (values.remarks == null || values.remarks == "")
            {
                lsremarks = "";
            }
            else
            {
                lsremarks = values.remarks.Replace("'", "\\'");
            }
            msSQL = " update ocs_mst_tdisbursementdocdeferral set disbursementdocdeferral_status ='" + values.disbursementdocdeferral_status + "'," +
                    " remarks='" + lsremarks + "'" +
                    " where disbursementdocdeferral_gid='" + values.disbursementdocdeferral_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("DSIL");

                msSQL = " insert into ocs_mst_tdisbursementdocdeferralinactivelog (" +
                      " disbursementdocdeferralinactivelog_gid, " +
                      " disbursementdocdeferral_gid," +
                      " wef_date ," +
                      " customer_type ," +
                      " vertical_gid," +
                      " disbursementdocdeferral_status," +
                      " remarks, " +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.disbursementdocdeferral_gid + "'," +
                      " '" + Convert.ToDateTime(values.wef_date).ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                      " '" + values.customer_type + "'," +
                      " '" + values.vertical_gid + "'," +
                      " '" + values.disbursementdocdeferral_status + "'," +
                      " '" + lsremarks + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.disbursementdocdeferral_status == "N")
                {
                    values.status = true;
                    values.message = "Document Deferral Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Document Deferral  Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Changing Status";
            }
        }

        public void DaGetDisbDeferralDocInactiveLogview(string disbursementdocdeferral_gid, MdlDisbDocumentDeferral values)
        {
            try
            {
                msSQL = " Select disbursementdocdeferralinactivelog_gid,disbursementdocdeferral_gid," +
                        " date_format(a.wef_date,'%d-%m-%Y %h:%i %p') as wef_date,customer_type,vertical_gid,a.remarks, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                        " date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                        " case when a.disbursementdocdeferral_status='N' then 'Inactive' else 'Active' end as disbursementdocdeferral_status" +
                        " from ocs_mst_tdisbursementdocdeferralinactivelog a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where disbursementdocdeferral_gid ='" + disbursementdocdeferral_gid + "'" +
                        " order by a.disbursementdocdeferralinactivelog_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getdisbdefdoclog_list = new List<disbdefdoclog_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getdisbdefdoclog_list.Add(new disbdefdoclog_list
                        {
                            disbursementdocdeferralinactivelog_gid = (dr_datarow["disbursementdocdeferralinactivelog_gid"].ToString()),
                            disbursementdocdeferral_gid = (dr_datarow["disbursementdocdeferral_gid"].ToString()),
                            wef_date = (dr_datarow["wef_date"].ToString()),
                            customer_type = (dr_datarow["customer_type"].ToString()),
                            vertical_gid = (dr_datarow["vertical_gid"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            disbursementdocdeferral_status = (dr_datarow["disbursementdocdeferral_status"].ToString()),
                        });
                    }
                    values.disbdefdoclog_list = getdisbdefdoclog_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }
        public void DaGetDisbDeferralDocEdit(string disbdocdeferralapprovalconfig_gid, MdlDisbDocumentDeferral values)
        {
            try
            {
                msSQL = " select disbdocdeferralapprovalconfig_gid,disbursementdocdeferral_gid,vertical_gid,group_gid,group_name, " +
                        " subgroup_gid,subgroup_name,manager_gid,manager_name,member_gid,member_name" +
                        " from ocs_mst_tdisbdocdeferralapprovalconfig a " +
                        " where a.disbdocdeferralapprovalconfig_gid='" + disbdocdeferralapprovalconfig_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.disbdocdeferralapprovalconfig_gid = objODBCDatareader["disbdocdeferralapprovalconfig_gid"].ToString();
                    values.disbursementdocdeferral_gid = objODBCDatareader["disbursementdocdeferral_gid"].ToString();
                    values.vertical_gid = objODBCDatareader["vertical_gid"].ToString();
                    values.group_gid = objODBCDatareader["group_gid"].ToString();
                    values.group_name = objODBCDatareader["group_name"].ToString();
                    values.subgroup_gid = objODBCDatareader["subgroup_gid"].ToString();
                    values.subgroup_name = objODBCDatareader["subgroup_name"].ToString();
                    values.manager_gid = objODBCDatareader["manager_gid"].ToString();
                    values.manager_name = objODBCDatareader["manager_name"].ToString();
                    values.member_gid = objODBCDatareader["member_gid"].ToString();
                    values.member_name = objODBCDatareader["member_name"].ToString();
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
        public bool DaPosDisbDeferralDocUpdate(string employee_gid, MdlDisbDocumentDeferral values)
        {
            msSQL = " select disbdocdeferralapprovalconfig_gid,disbursementdocdeferral_gid,vertical_gid,group_gid,group_name, " +
                        " subgroup_gid,subgroup_name,manager_gid,manager_name,member_gid,member_name" +
                        " from ocs_mst_tdisbdocdeferralapprovalconfig a " +
                        " where a.disbdocdeferralapprovalconfig_gid='" + values.disbdocdeferralapprovalconfig_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                lsdisbdocdeferralapprovalconfig_gid = objODBCDatareader["disbdocdeferralapprovalconfig_gid"].ToString();
                lsdisbursementdocdeferral_gid = objODBCDatareader["disbursementdocdeferral_gid"].ToString();
                lsvertical_gid = objODBCDatareader["vertical_gid"].ToString();
                lsgroup_gid = objODBCDatareader["group_gid"].ToString();
                lsgroup_name = objODBCDatareader["group_name"].ToString();
                lssubgroup_gid = objODBCDatareader["subgroup_gid"].ToString();
                lssubgroup_name = objODBCDatareader["subgroup_name"].ToString();
                lsmanager_gid = objODBCDatareader["manager_gid"].ToString();
                lsmanager_name = objODBCDatareader["manager_name"].ToString();
                lsmember_gid = objODBCDatareader["member_gid"].ToString();
                lsmember_name = objODBCDatareader["member_name"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " update ocs_mst_tdisbdocdeferralapprovalconfig set " +
                    " group_gid='" + values.group_gid + "'," +
                    " group_name = '" + values.group_name + "'," +
                    " subgroup_gid = '" + values.subgroup_gid + "'," +
                    " subgroup_name = '" + values.subgroup_name + "'," +
                    " manager_gid = '" + values.manager_gid + "'," +
                    " manager_name='" + values.manager_name + "'," +
                    " member_gid = '" + values.member_gid + "'," +
                    " member_name = '" + values.member_name + "'," +
                    " disbursementdocdeferral_gid = '" + lsdisbursementdocdeferral_gid + "'," +
                    " vertical_gid='" + values.vertical_gid + "'," +
                    " updated_by = '" + employee_gid + "'," +
                    " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    " where disbdocdeferralapprovalconfig_gid ='" + values.disbdocdeferralapprovalconfig_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("DDAL");
                msSQL = " insert into ocs_mst_tdisbdocdeferralapprovalconfiglog(" +
                        " disbdocdeferralapprovalconfiglog_gid ," +
                        " disbdocdeferralapprovalconfig_gid," +
                        " disbursementdocdeferral_gid," +
                        " vertical_gid," +
                        " group_gid," +
                        " group_name," +
                        " subgroup_gid," +
                        " subgroup_name," +
                        " manager_gid," +
                        " manager_name," +
                        " member_gid," +
                        " member_name," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + lsdisbdocdeferralapprovalconfig_gid + "'," +
                        "'" + lsdisbursementdocdeferral_gid + "'," +
                        "'" + lsvertical_gid + "'," +
                        "'" + lsgroup_gid + "'," +
                        "'" + lsgroup_name + "'," +
                        "'" + lssubgroup_gid + "'," +
                        "'" + lssubgroup_name + "'," +
                        "'" + lsmanager_gid + "'," +
                        "'" + lsmanager_name + "'," +
                        "'" + lsmember_gid + "'," +
                        "'" + lsmember_name + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Document Deferral Details Updated Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while updating Document Deferral Details";
                return false;
            }
        }
        public string GetDateFormat(string lsdate)
        {
            DateTime Date;
            string[] formats = { "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'" };
            DateTime.TryParseExact(lsdate, formats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out Date);

            lswef_date = Convert.ToDateTime(Date).ToString("yyyy-MM-dd");
            lswef_datetime = DateTime.Now.ToString("HH:mm:ss");

            string wefdate = lswef_date + " " + lswef_datetime;
            return wefdate;
        }
        public void DaGetProductBuyerList(string application_gid, MdlMstProductBuyer values)
        {
            try
            {
                msSQL = " select a.application2buyer_gid,a.buyer_name,a.buyer_gid,a.buyer_limit,a.availed_limit, " +
                       " a.balance_limit,a.margin,a.bill_tenure from ocs_trn_tcadapplication2buyer a " +
                       " left join ocs_trn_tcadapplication2loan b on a.application2loan_gid = b.application2loan_gid " +
                       "where b.application_gid='" + application_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmstproductbuyer_list = new List<mstproductbuyer_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmstproductbuyer_list.Add(new mstproductbuyer_list
                        {
                            application2buyer_gid = (dr_datarow["application2buyer_gid"].ToString()),
                            buyer_gid = (dr_datarow["buyer_gid"].ToString()),
                            buyer_name = (dr_datarow["buyer_name"].ToString()),
                            buyer_limit = (dr_datarow["buyer_limit"].ToString()),
                            availed_limit = (dr_datarow["availed_limit"].ToString()),
                            balance_limit = (dr_datarow["balance_limit"].ToString()),
                            bill_tenure = (dr_datarow["bill_tenure"].ToString()),
                            margin = (dr_datarow["margin"].ToString())
                        });
                    }
                    values.mstproductbuyer_list = getmstproductbuyer_list;
                }
                dt_datatable.Dispose();
                values.status = true;
                values.message = "success";

            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }

        }
        public void DaImportExcelDisbFarmerIndividual(HttpRequest httpRequest, string employee_gid, result objResult)
        {
            try
            {
                int insertCount = 0;
                HttpFileCollection httpFileCollection;
                DataTable dt = null;
                string lspath, lsfilePath;
                string application_gid = httpRequest.Form["application_gid"];

                msSQL = " select company_code from adm_mst_tcompany";
                lscompany_code = objdbconn.GetExecuteScalar(msSQL);

                // Create Directory
                lsfilePath = HttpContext.Current.Server.MapPath("../../../erpdocument" + "/" + lscompany_code + "/Master/DisbursementFarmerIndividualDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month);

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

                //path creation        
                lspath = lsfilePath + "/";
                FileStream file = new FileStream(lspath + lsfile_gid, FileMode.Create, FileAccess.Write);
                ms.WriteTo(file);

                using (ExcelPackage xlPackage = new ExcelPackage(ms))
                {
                    ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets[1];
                    rowCount = worksheet.Dimension.End.Row;
                    columnCount = worksheet.Dimension.End.Column;
                    endRange = worksheet.Dimension.End.Address;
                }

                bool status;
                status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Master/DisbursementFarmerIndividualDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);


                file.Close();
                ms.Close();

                objcmnfunctions.uploadFile(lspath, lsfile_gid);

                //Excel To DataTable

                lsfilePath = @"" + lsfilePath.Replace("/", "\\") + "\\" + lsfile_gid + "";



                excelRange = "A2:" + endRange + rowCount.ToString();
                dt = objcmnfunctions.ExcelToDataTable(lsfilePath, excelRange);
                try
                {
                    dt = dt.Rows.Cast<DataRow>().Where(r => string.Join("", r.ItemArray).Trim() != string.Empty).CopyToDataTable();
                }
                catch (Exception ex)
                {
                    objResult.status = false;
                    objResult.message = "No Records Found";
                    return;
                }

                Nullable<DateTime> ldpepverified_date, ldfather_dob, ldmother_dob, ldspouse_dob, ldaccountopendate;

                foreach (DataRow row in dt.Rows)
                {
                    contactimportlog_message = "";

                    lsapplication_no = row["* Application No"].ToString();
                    if (lsapplication_no == "")
                    {

                    }
                    else
                    {
                        lsurn_status = row["* Having URN (Yes/No)"].ToString();
                        lsurn = row["If Yes, URN"].ToString();

                        //lsgroup_name = row["If Group Yes, Group Name *"].ToString();
                        //msSQL = "select group_gid from ocs_mst_tgroup where group_name='" + lsgroup_name + "'";
                        //lsgroup_gid = objdbconn.GetExecuteScalar(msSQL);
                        //if (lsgroup_name == "NA" && lsgroup_gid == "")
                        //    lsgroup_gid = "NA";

                        lsinsitution_name = row["If company/Institution yes, Company Name *"].ToString();
                        msSQL = "select institution_gid from ocs_trn_tcadinstitution where company_name='" + lsinsitution_name + "'";
                        lsinstitution_gid = objdbconn.GetExecuteScalar(msSQL);
                        if (lsinsitution_name == "NA" && lsinstitution_gid == "")
                            lsinstitution_gid = "NA";

                        lspan_status = row["* PAN Status (Yes / No)"].ToString();
                        lspan_no = row["PAN Value (If PAN Status is Yes, PAN Value is mandatory)"].ToString();
                        lsaadhar_no = row["* Aadhar Number"].ToString();

                        lsfirst_name = row["* First Name"].ToString();
                        lsmiddle_name = row["Middle Name"].ToString();
                        lslast_name = row["* Last Name"].ToString();

                        lsindividual_dob = row["Date of Birth (DD-MM-YYYY)"].ToString();

                        if (lsindividual_dob.Length > 10)
                        {
                            lsindividual_dob = dateFormatStandardizer(lsindividual_dob);
                        }

                        lsgender_name = row["* Gender"].ToString();
                        msSQL = "select gender_gid from ocs_mst_tgender where gender_name='" + row["* Gender"].ToString() + "'";
                        lsgender_gid = objdbconn.GetExecuteScalar(msSQL);

                        lsdesignation_type = row["Designation"].ToString();
                        msSQL = "select designation_gid from ocs_mst_tdesignation where designation_type='" + row["Designation"].ToString() + "'";
                        lsdesignation_gid = objdbconn.GetExecuteScalar(msSQL);

                        lspep_status = row["* Politically Exposed person (PEP)(Yes/No)"].ToString();

                        lspepverified_date = row["* PEP Verified On (DD-MM-YYYY)"].ToString();
                        if (lspepverified_date.Length > 10)
                        {
                            lspepverified_date = dateFormatStandardizer(lspepverified_date);
                        }
                        lspepverified_date = lspepverified_date.Replace('-', '/');
                        ldpepverified_date = DateTime.ParseExact(lspepverified_date, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                        lsuser_type = row["* Stakeholder Type"].ToString();
                        msSQL = "select usertype_gid from ocs_mst_tusertype where user_type='" + row["* Stakeholder Type"].ToString() + "'";
                        lsusertype_gid = objdbconn.GetExecuteScalar(msSQL);

                        lsmaritalstatus_name = row["* Marital Status"].ToString();
                        msSQL = "select maritalstatus_gid from ocs_mst_tmaritalstatus where maritalstatus_name='" + row["* Marital Status"].ToString() + "'";
                        lsmaritalstatus_gid = objdbconn.GetExecuteScalar(msSQL);

                        lsfather_firstname = row["* Father's First Name"].ToString();
                        lsfather_middlename = row["Father's Middle Name"].ToString();
                        lsfather_lastname = row["* Father's Last Name"].ToString();
                        lsfathernominee_status = row["Father Nominee(Yes/No)"].ToString();

                        lsfather_dob = row["Father's Date of Birth(DD-MM-YYYY)"].ToString();
                        if (lsfather_dob.Length > 10)
                        {
                            lsfather_dob = dateFormatStandardizer(lsfather_dob);
                        }
                        if (lsfather_dob.Length > 0)
                        {
                            lsfather_dob = lsfather_dob.Replace('-', '/');
                            ldfather_dob = DateTime.ParseExact(lsfather_dob, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            ldfather_dob = null;
                        }


                        lsmother_firstname = row["Mother's First Name"].ToString();
                        lsmother_middlename = row["Mother's Middle Name"].ToString();
                        lsmother_lastname = row["Mother's Last Name"].ToString();
                        lsmothernominee_status = row["Mother Nominee(Yes/No)"].ToString();


                        lsmother_dob = row["Mother's Date of Birth(DD-MM-YYYY)"].ToString();
                        if (lsmother_dob.Length > 10)
                        {
                            lsmother_dob = dateFormatStandardizer(lsmother_dob);
                        }
                        if (lsmother_dob.Length > 0)
                        {
                            lsmother_dob = lsmother_dob.Replace('-', '/');
                            ldmother_dob = DateTime.ParseExact(lsmother_dob, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            ldmother_dob = null;
                        }

                        lsspouse_firstname = row["Spouse First Name"].ToString();
                        lsspouse_middlename = row["Spouse Middle Name"].ToString();
                        lsspouse_lastname = row["Spouse Last Name"].ToString();
                        lsspousenominee_status = row["Spouse Nominee(Yes/No)"].ToString();

                        lsspouse_dob = row["Spouse's Date of Birth(DD-MM-YYYY)"].ToString();
                        if (lsspouse_dob.Length > 10)
                        {
                            lsspouse_dob = dateFormatStandardizer(lsspouse_dob);
                        }
                        if (lsspouse_dob.Length > 0)
                        {
                            lsspouse_dob = lsspouse_dob.Replace('-', '/');
                            ldspouse_dob = DateTime.ParseExact(lsspouse_dob, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            ldspouse_dob = null;
                        }

                        lseducationalqualification_name = row["* Educational Qualification"].ToString();
                        msSQL = "select educationalqualification_gid from ocs_mst_teducationalqualification where educationalqualification_name='" + row["* Educational Qualification"].ToString() + "'";
                        lseducationalqualification_gid = objdbconn.GetExecuteScalar(msSQL);

                        lsmain_occupation = row["* Main Occupation"].ToString();
                        lsannual_income = row["* Annual Income"].ToString();
                        lsmonthly_income = row["Monthly Income"].ToString();

                        lsincometype_name = row["Income Type"].ToString();
                        msSQL = "select incometype_gid from ocs_mst_tincometype where incometype_name='" + row["Income Type"].ToString() + "'";
                        lsincometype_gid = objdbconn.GetExecuteScalar(msSQL);

                        lsyearscurrentresidece = row["* Years in Current Residence"].ToString();
                        lsdistancebranch = row["* Distance from Branch/Regional Office (in Kms)"].ToString();

                        lsmobile_no = row["* Mobile Number"].ToString();
                        lswhatsapp_no = row["* Whatsapp  (Yes/No)"].ToString();

                        lsemail_address = row["* Email Address"].ToString();

                        lsaddresstype_name = row["* Address Type"].ToString();
                        msSQL = "select address_gid from ocs_mst_taddresstype where address_type='" + row["* Address Type"].ToString() + "'";
                        lsaddresstype_gid = objdbconn.GetExecuteScalar(msSQL);

                        lsaddressline1 = row["* AddressLine1"].ToString();
                        lsaddressline2 = row["AddressLine2"].ToString();
                        lspostal_code = row["* Postal Code"].ToString();

                        msSQL = " select city,taluka,district,state from ocs_mst_tpostalcode where " +
                           "postalcode_value='" + lspostal_code + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lscity = objODBCDatareader["city"].ToString();
                            lstaluka = objODBCDatareader["taluka"].ToString();
                            lsdistrict = objODBCDatareader["district"].ToString();
                            lsstate = objODBCDatareader["state"].ToString();
                        }
                        objODBCDatareader.Close();
                        lscountry = row["* Country"].ToString();
                        lsifsccode = row["* IFSC Code"].ToString();
                        lsbankname = row["Bank Name"].ToString();
                        lsbranchname = row["Branch Name"].ToString();
                        lsbranchaddress = row["Branch Address"].ToString();
                        lsmicrcode = row["MICR Code"].ToString();
                        lsbankaccountnumber = row["* Bank Account Number "].ToString();
                        lsaccountholdername = row["* Account Holder Name"].ToString();

                        lsaccounttype = row["* Account Type"].ToString();
                        msSQL = "select bankaccounttype_gid from ocs_mst_tbankaccounttype where bankaccounttype_name='" + row["* Account Type"].ToString() + "'";
                        lsaccounttype_gid = objdbconn.GetExecuteScalar(msSQL);

                        lsjointaccount = row["* Joint Account"].ToString();
                        lsjointaccountholdername = row["Joint Account Holder Name"].ToString();
                        lsischequebookfacilityavailable = row["* Is Cheque Book Facility Available"].ToString();
                        lsaccountopendate = row["Account Open Date"].ToString();
                        lsdisbursementamount = row["* Disbursement Amount"].ToString();

                        if (lsaccountopendate.Length > 10)
                        {
                            lsaccountopendate = dateFormatStandardizer(lsaccountopendate);
                        }
                        if (lsaccountopendate.Length > 0)
                        {
                            lsaccountopendate = lsaccountopendate.Replace('-', '/');
                            ldaccountopendate = DateTime.ParseExact(lsaccountopendate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            ldaccountopendate = null;
                        }

                        //msSQL = "select stakeholder_type from ocs_trn_tcadcontact where application_gid='" + application_gid + "' and stakeholder_type in ('Borrower','Applicant')";
                        //string lsstakeholder_type = objdbconn.GetExecuteScalar(msSQL);

                        //if (lsstakeholder_type == lsuser_type)
                        //{
                        //    contactimportlog_message = "Applicant/Borrower Information Already Added in Individual";

                        //}

                        //msSQL = "select stakeholder_type from ocs_trn_tcadinstitution where application_gid='" + application_gid + "' and stakeholder_type in ('Borrower','Applicant')";
                        //lsstakeholder_type = objdbconn.GetExecuteScalar(msSQL);

                        //if (lsstakeholder_type == lsuser_type)
                        //{
                        //    contactimportlog_message = "Applicant/Borrower Information Already Added in Insitution";
                        //}

                        //msSQL = "select stakeholder_type from ocs_trn_tcadinstitution where application_gid='" + application_gid + "' and stakeholder_type in ('Borrower','Applicant')";

                        if (contactimportlog_message == "")
                        {
                            if (lspan_status == "Yes" && lspan_no == "")
                            {
                                lspanstatusvalue = "empty";
                            }

                            if ((lsurn_status == "") || (lsgroup_name == "") || (lsinsitution_name == "") || (lspan_status == "") || (lsaadhar_no == "") || (lsfirst_name == "")
                            || (lslast_name == "") || (lsgender_name == "") || (lspep_status == "") || (lspepverified_date == "") || (lsuser_type == "") || (lsmaritalstatus_name == "")
                            || (lsfather_firstname == "") || (lsfather_lastname == "") || (lseducationalqualification_name == "") || (lsmain_occupation == "") || (lsannual_income == "")
                            || (lsmobile_no == "") || (lswhatsapp_no == "") || (lsemail_address == "") || (lsaddresstype_name == "") || (lsaddressline1 == "") || (lsaddressline1 == "")
                            || (lspostal_code == "") || (lscountry == "") || (lspanstatusvalue == "empty")
                            || (lsifsccode == "") || (lsbankaccountnumber == "") || (lsaccountholdername == "") || (lsaccounttype == "")
                            || (lsjointaccount == "") || (lsischequebookfacilityavailable == "") || (lsdisbursementamount == ""))
                            {
                                contactimportlog_message = "Mandatory fields are empty";
                            }

                            //if (contactimportlog_message == "")
                            //{
                            //    msSQL = "select first_name, last_name, pan_no from ocs_trn_tcadcontact where application_gid='" + application_gid + "'";
                            //    dt_datatable = objdbconn.GetDataTable(msSQL);
                            //    if (dt_datatable.Rows.Count != 0)
                            //    {
                            //        foreach (DataRow dr_datarow in dt_datatable.Rows)
                            //        {
                            //            if ((lsfirst_name == dr_datarow["first_name"].ToString()) && (lslast_name == dr_datarow["last_name"].ToString()) && (lspan_no == dr_datarow["pan_no"].ToString()))
                            //            {
                            //                contactimportlog_message = "Record has many duplicate values";
                            //                break;
                            //            }
                            //        }
                            //    }
                            //    dt_datatable.Dispose();

                            //}

                            msSQL = "select sanction_amount from ocs_trn_tapplication2sanction where  application_gid='" + application_gid + "'";
                            string lssanction_amount = objdbconn.GetExecuteScalar(msSQL);
                            msSQL = " select disbursement_amount as sumdisbursement_amount from ocs_trn_tdisbursementamount  where application_gid='" + application_gid + "' and " +
                                   " (rmdisbursementrequest_gid !='" + employee_gid + "')" +
                                   " and rmdisbursementrequest_gid not in (select rmdisbursementrequest_gid from ocs_trn_tdisbursementassignment where approval_status ='Rejected') " +
                                   " and " +
                                   " ((makerdisbursement_amount is null ||makerdisbursement_amount ='') && " +
                                   " (checkerdisbursement_amount is null ||checkerdisbursement_amount ='')) " +
                                   " union all " +
                                   " select makerdisbursement_amount  as sumdisbursement_amount from ocs_trn_tdisbursementamount  where application_gid='" + application_gid + "' and " +
                                   " (rmdisbursementrequest_gid !='" + employee_gid + "')" +
                                   " and rmdisbursementrequest_gid not in (select rmdisbursementrequest_gid from ocs_trn_tdisbursementassignment where approval_status ='Rejected') " +
                                   " and " +
                                   " ((makerdisbursement_amount is not null ||makerdisbursement_amount !='') && " +
                                   " (checkerdisbursement_amount is  null ||checkerdisbursement_amount ='')) " +
                                   " union all " +
                                   " select checkerdisbursement_amount as sumdisbursement_amount from ocs_trn_tdisbursementamount  where application_gid='" + application_gid + "' and " +
                                   " (rmdisbursementrequest_gid !='" + employee_gid + "')" +
                                   " and rmdisbursementrequest_gid not in (select rmdisbursementrequest_gid from ocs_trn_tdisbursementassignment where approval_status ='Rejected') " +
                                   " and " +
                                   " ((checkerdisbursement_amount is not null ||checkerdisbursement_amount !=''))" +
                                    " union all" +
                                    " select case when (creditopscheckerdisbursement_amount is null and  creditopsdisbursement_amount is null) then disbursement_amount  " +
                                    " when (creditopscheckerdisbursement_amount is null ) then creditopsdisbursement_amount  " +
                                    " else creditopscheckerdisbursement_amount end as sumdisbursement_amount " +
                                    " from ocs_trn_tdisbursementsupplier  where application_gid='" + application_gid + "'  " +
                                    " and rmdisbursementrequest_gid not in (select rmdisbursementrequest_gid from ocs_trn_tdisbursementassignment where approval_status ='Rejected') " +
                                    " union all" +
                                    " select case when (creditopscheckerdisbursement_amount is null and  creditopsdisbursement_amount is null) then disbursement_amount  " +
                                    " when (creditopscheckerdisbursement_amount is null ) then creditopsdisbursement_amount  " +
                                    " else creditopscheckerdisbursement_amount end as sumdisbursement_amount " +
                                    " from ocs_trn_tfarmercontact  where application_gid='" + application_gid + "'  " +
                                    " and rmdisbursementrequest_gid not in (select rmdisbursementrequest_gid from ocs_trn_tdisbursementassignment where approval_status ='Rejected') ";
                            dt_datatable = objdbconn.GetDataTable(msSQL);
                            var getdisbursementamount_list = new List<disbursementamount_list>();
                            double disbursementamount_total = 0;
                            if (dt_datatable.Rows.Count != 0)
                            {
                                foreach (DataRow dr_datarow in dt_datatable.Rows)
                                {

                                    disbursementamount_total = disbursementamount_total + double.Parse(dr_datarow["sumdisbursement_amount"].ToString().Replace(",", ""));

                                }

                            }
                            dt_datatable.Dispose();


                            if (string.IsNullOrEmpty(lsdisbursementamount))
                            {

                            }
                            else
                            {
                                disbursementamount_total = disbursementamount_total + double.Parse(lsdisbursementamount.Replace(",", ""));
                            }

                            if (disbursementamount_total <= double.Parse(lssanction_amount))
                            {

                            }
                            else
                            {

                                contactimportlog_message = "Disbursement Amount is Greater than Sanction Amount";


                            }

                        }


                        if (contactimportlog_message != "")
                        {

                            msGetGid = objcmnfunctions.GetMasterGID("DFIL");

                            msSQL = " insert into ocs_trn_tfarmercontactimportlog(" +
                    " farmercontactlog_gid," +
                    " application_gid," +
                    " application_no," +

                    " urn_status," +
                    " urn," +
                    " group_name," +
                    " institution_name," +
                    " pan_status," +
                    " pan_no," +
                    " aadhar_no," +
                    " first_name," +
                    " middle_name," +
                    " last_name," +
                    " individual_dob," +

                    " gender_name," +
                    " designation_name," +
                    " pep_status," +
                    " pepverified_date," +

                    " stakeholder_type," +
                    " maritalstatus_name," +

                    " father_firstname," +
                    " father_middlename," +
                    " father_lastname," +
                    " fathernominee_status," +
                    " father_dob," +

                    " mother_firstname," +
                    " mother_middlename," +
                    " mother_lastname," +
                    " mothernominee_status," +
                    " mother_dob," +

                    " spouse_firstname," +
                    " spouse_middlename," +
                    " spouse_lastname," +
                    " spousenominee_status," +
                    " spouse_dob," +

                    " educationalqualification_name," +
                    " main_occupation," +
                    " annual_income," +
                    " monthly_income," +
                    " incometype_name," +

                    " mobile_no," +
                    " whatsapp_no," +
                    " email_address," +
                    " addresstype_name," +
                    " addressline1," +
                    " addressline2," +
                    " postal_code," +
                    " country," +

                    " contactimportlog_status," +
                    " ifsc_code," +
                    " bank_name," +
                    " branch_name," +
                    " branch_address," +
                    " micr_code," +
                    " bankaccount_number," +
                    " accountholder_name," +
                    " accounttype_gid," +
                    " account_type," +
                    " joint_account," +
                    " jointaccountholder_name," +
                    " chequebookfacility_available," +
                    " accountopen_date," +
                    " disbursement_amount," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + application_gid + "'," +
                    "'" + lsapplication_no + "'," +

                     "'" + lsurn_status + "'," +
                     "'" + lsurn + "',";
                            if (lsgroup_name == "" || lsgroup_name == null)
                            {
                                msSQL += "'',";
                            }
                            else
                            {
                                msSQL += "'" + lsgroup_name.Replace("'", "") + "',";
                            }

                            if (lsinsitution_name == "" || lsinsitution_name == null)
                            {
                                msSQL += "'',";
                            }
                            else
                            {
                                msSQL += "'" + lsinsitution_name.Replace("'", "") + "',";
                            }

                            msSQL += "'" + lspan_no + "'," +
                                     "'" + lspan_status + "'," +
                                     "'" + lsaadhar_no + "'," +
                                     "'" + lsfirst_name + "'," +
                                      "'" + lsmiddle_name + "'," +
                                      "'" + lslast_name + "'," +
                                      "'" + lsindividual_dob + "'," +

                                      "'" + lsgender_name + "'," +
                                      "'" + lsdesignation_type + "'," +
                                      "'" + lspep_status + "'," +
                                      "'" + Convert.ToDateTime(ldpepverified_date).ToString("yyyy-MM-dd HH:mm:ss") + "'," +

                                      "'" + lsuser_type + "'," +
                                      "'" + lsmaritalstatus_name + "'," +

                                      "'" + lsfather_firstname + "'," +
                                      "'" + lsfather_middlename + "'," +
                                      "'" + lsfather_lastname + "'," +
                                      "'" + lsfathernominee_status + "',";

                            if ((ldfather_dob == null))
                            {
                                msSQL += "null,";
                            }
                            else
                            {
                                msSQL += "'" + Convert.ToDateTime(ldfather_dob).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                            }



                            msSQL += "'" + lsmother_firstname + "'," +
                                      "'" + lsmother_middlename + "'," +
                                      "'" + lsmother_lastname + "'," +
                                      "'" + lsmothernominee_status + "',";

                            if ((ldmother_dob == null))
                            {
                                msSQL += "null,";
                            }
                            else
                            {
                                msSQL += "'" + Convert.ToDateTime(ldmother_dob).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                            }

                            msSQL += "'" + lsspouse_firstname + "'," +
                                      "'" + lsspouse_middlename + "'," +
                                      "'" + lsspouse_lastname + "'," +
                                      "'" + lsspousenominee_status + "',";

                            if ((ldspouse_dob == null))
                            {
                                msSQL += "null,";
                            }
                            else
                            {
                                msSQL += "'" + Convert.ToDateTime(ldspouse_dob).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                            }

                            msSQL += "'" + lseducationalqualification_name + "'," +
                                     "'" + lsmain_occupation + "'," +
                                     "'" + lsannual_income + "'," +
                                     "'" + lsmonthly_income + "'," +
                                     "'" + lsincometype_name + "'," +

                                     "'" + lsmobile_no + "'," +
                                     "'" + lswhatsapp_no + "'," +
                                     "'" + lsemail_address + "'," +
                                     "'" + lsaddresstype_name + "',";
                            if (lsaddressline1 == "" || lsaddressline1 == null)
                            {
                                msSQL += "'',";
                            }
                            else
                            {
                                msSQL += "'" + lsaddressline1.Replace("'", "") + "',";
                            }
                            if (lsaddressline2 == "" || lsaddressline2 == null)
                            {
                                msSQL += "'',";
                            }
                            else
                            {
                                msSQL += "'" + lsaddressline2.Replace("'", "") + "',";
                            }

                            msSQL += "'" + lspostal_code + "'," +
                                   "'" + lscountry + "'," +

                                   "'" + contactimportlog_message + "'," +
                                   "'" + lsifsccode + "'," +
                                   "'" + lsbankname + "'," +
                                   "'" + lsbranchname + "'," +
                                   "'" + lsbranchaddress + "'," +
                                   "'" + lsmicrcode + "'," +
                                   "'" + lsbankaccountnumber + "'," +
                                   "'" + lsaccountholdername + "'," +
                                   "'" + lsaccounttype_gid + "'," +
                                   "'" + lsaccounttype + "'," +
                                   "'" + lsjointaccount + "'," +
                                   "'" + lsjointaccountholdername + "'," +
                                   "'" + lsischequebookfacilityavailable + "',";
                            if ((ldaccountopendate == null))
                            {
                                msSQL += "null,";
                            }
                            else
                            {
                                msSQL += "'" + Convert.ToDateTime(ldaccountopendate).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                            }
                            msSQL += "'" + lsdisbursementamount + "'," +
                                     "'" + employee_gid + "'," +
                                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            logCount++;
                        }
                        else
                        {
                            if (lspan_status == "Yes")
                            {
                                lspan_status = "Customer Submitting PAN";
                            }
                            else if (lspan_status == "No")
                            {
                                lspan_status = "Customer Submitting Form 60";
                            }
                            else
                            {

                            }
                            msGetGid = objcmnfunctions.GetMasterGID("DSFC");

                            msSQL = " insert into ocs_trn_tfarmercontact(" +
                           " farmercontact_gid," +
                           " application_gid," +
                           " application_no," +

                           " urn_status," +
                           " urn," +
                           //" group_gid," +
                           //" group_name," +
                           " institution_gid," +
                           " institution_name," +
                           " pan_status," +
                           " pan_no," +
                           " aadhar_no," +
                           " first_name," +
                           " middle_name," +
                           " last_name," +
                           " individual_dob," +

                           " gender_gid," +
                           " gender_name," +
                           " designation_gid," +
                           " designation_name," +
                           " pep_status," +
                           " pepverified_date," +

                           " stakeholdertype_gid," +
                           " stakeholder_type," +
                           " maritalstatus_gid," +
                           " maritalstatus_name," +

                           " father_firstname," +
                           " father_middlename," +
                           " father_lastname," +
                           " fathernominee_status," +
                           " father_dob," +

                           " mother_firstname," +
                           " mother_middlename," +
                           " mother_lastname," +
                           " mothernominee_status," +
                           " mother_dob," +

                           " spouse_firstname," +
                           " spouse_middlename," +
                           " spouse_lastname," +
                           " spousenominee_status," +
                           " spouse_dob," +


                           " educationalqualification_gid," +
                           " educationalqualification_name," +
                           " main_occupation," +
                           " annual_income," +
                           " monthly_income," +
                           " incometype_gid," +
                           " incometype_name," +

                           " currentresidence_years," +
                           " branch_distance," +
                           " contact_status," +
                           " ifsc_code," +
                           " bank_name," +
                           " branch_name," +
                           " branch_address," +
                           " micr_code," +
                           " bankaccount_number," +
                           " accountholder_name," +
                           " accounttype_gid," +
                           " account_type," +
                           " joint_account," +
                           " jointaccountholder_name," +
                           " chequebookfacility_available," +
                           " accountopen_date," +
                           " rmdisbursementrequest_gid," +
                           " disbursement_amount," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetGid + "'," +
                           "'" + application_gid + "'," +
                           "'" + lsapplication_no + "'," +

                            "'" + lsurn_status + "'," +
                            "'" + lsurn + "'," +
                            //"'" + lsgroup_gid + "'," +
                            //"'" + lsgroup_name.Replace("'", "") + "'," +
                            "'" + lsinstitution_gid + "'," +
                            "'" + lsinsitution_name.Replace("'", "") + "'," +
                           "'" + lspan_status + "'," +
                           "'" + lspan_no + "'," +
                           "'" + lsaadhar_no + "'," +
                           "'" + lsfirst_name + "'," +
                            "'" + lsmiddle_name + "'," +
                            "'" + lslast_name + "'," +
                            "'" + lsindividual_dob + "'," +

                            "'" + lsgender_gid + "'," +
                            "'" + lsgender_name + "'," +
                            "'" + lsdesignation_gid + "'," +
                            "'" + lsdesignation_type + "'," +
                            "'" + lspep_status + "'," +
                            "'" + Convert.ToDateTime(ldpepverified_date).ToString("yyyy-MM-dd HH:mm:ss") + "'," +


                            "'" + lsusertype_gid + "'," +
                            "'" + lsuser_type + "'," +
                            "'" + lsmaritalstatus_gid + "'," +
                            "'" + lsmaritalstatus_name + "'," +

                            "'" + lsfather_firstname + "'," +
                            "'" + lsfather_middlename + "'," +
                            "'" + lsfather_lastname + "'," +
                            "'" + lsfathernominee_status + "',";

                            if ((ldfather_dob == null))
                            {
                                msSQL += "null,";
                            }
                            else
                            {
                                msSQL += "'" + Convert.ToDateTime(ldfather_dob).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                            }



                            msSQL += "'" + lsmother_firstname + "'," +
                                      "'" + lsmother_middlename + "'," +
                                      "'" + lsmother_lastname + "'," +
                                      "'" + lsmothernominee_status + "',";

                            if ((ldmother_dob == null))
                            {
                                msSQL += "null,";
                            }
                            else
                            {
                                msSQL += "'" + Convert.ToDateTime(ldmother_dob).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                            }

                            msSQL += "'" + lsspouse_firstname + "'," +
                                      "'" + lsspouse_middlename + "'," +
                                      "'" + lsspouse_lastname + "'," +
                                      "'" + lsspousenominee_status + "',";

                            if ((ldspouse_dob == null))
                            {
                                msSQL += "null,";
                            }
                            else
                            {
                                msSQL += "'" + Convert.ToDateTime(ldspouse_dob).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                            }

                            msSQL += "'" + lseducationalqualification_gid + "'," +
                                  "'" + lseducationalqualification_name + "'," +
                                  "'" + lsmain_occupation + "'," +
                                  "'" + lsannual_income + "'," +
                                  "'" + lsmonthly_income + "'," +
                                  "'" + lsincometype_gid + "'," +
                                  "'" + lsincometype_name + "'," +

                                  "'" + lsyearscurrentresidece + "'," +
                                  "'" + lsdistancebranch + "'," +
                                  "'Incomplete'," +
                                  "'" + lsifsccode + "'," +
                                  "'" + lsbankname + "'," +
                                  "'" + lsbranchname + "'," +
                                  "'" + lsbranchaddress + "'," +
                                  "'" + lsmicrcode + "'," +
                                  "'" + lsbankaccountnumber + "'," +
                                  "'" + lsaccountholdername + "'," +
                                  "'" + lsaccounttype_gid + "'," +
                                  "'" + lsaccounttype + "'," +
                                  "'" + lsjointaccount + "'," +
                                  "'" + lsjointaccountholdername + "'," +
                                  "'" + lsischequebookfacilityavailable + "',";
                            if ((ldaccountopendate == null))
                            {
                                msSQL += "null,";
                            }
                            else
                            {
                                msSQL += "'" + Convert.ToDateTime(ldaccountopendate).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                            }
                            msSQL += "'" + employee_gid + "'," +
                                     "'" + lsdisbursementamount + "'," +
                                     "'" + employee_gid + "'," +
                                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            msGetGidMobile = objcmnfunctions.GetMasterGID("DFCM");

                            msSQL = " insert into ocs_trn_tfarmercontact2mobileno(" +
                           " farmercontact2mobileno_gid," +
                           " farmercontact_gid," +
                           " mobile_no," +
                           " primary_status," +
                           " whatsapp_no," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetGidMobile + "'," +
                           "'" + msGetGid + "'," +
                           "'" + lsmobile_no + "'," +
                           "'" + "Yes" + "'," +
                           "'" + lswhatsapp_no + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                            mnResultMobile = objdbconn.ExecuteNonQuerySQL(msSQL);

                            msGetGidEmail = objcmnfunctions.GetMasterGID("DFCE");
                            msSQL = " insert into ocs_trn_tfarmercontact2email(" +
                                    " farmercontact2email_gid," +
                                    " farmercontact_gid," +
                                    " email_address," +
                                    " primary_status," +
                                    " created_by," +
                                    " created_date)" +
                                    " values(" +
                                    "'" + msGetGidEmail + "'," +
                                    "'" + msGetGid + "'," +
                                    "'" + lsemail_address + "'," +
                                    "'" + "Yes" + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                            mnResultEmail = objdbconn.ExecuteNonQuerySQL(msSQL);


                            msGetGidAddress = objcmnfunctions.GetMasterGID("DFCA");
                            msSQL = " insert into ocs_trn_tfarmercontact2address(" +
                                    " farmercontact2address_gid," +
                                    " farmercontact_gid," +
                                    " addresstype_gid," +
                                    " addresstype_name," +
                                    " primary_status," +
                                    " addressline1," +
                                    " addressline2," +
                                    " landmark," +
                                    " postal_code," +
                                    " city," +
                                    " taluka," +
                                    " district," +
                                    " state," +
                                    " country," +
                                    " created_by," +
                                    " created_date)" +
                                    " values(" +
                                    "'" + msGetGidAddress + "'," +
                                    "'" + msGetGid + "'," +
                                    "'" + lsaddresstype_gid + "'," +
                                    "'" + lsaddresstype_name + "'," +
                                    "'" + "Yes" + "'," +
                                    "'" + lsaddressline1.Replace("'", "") + "',";

                            if (lsaddressline2 == "" || lsaddressline2 == null)
                            {
                                msSQL += "'',";
                            }
                            else
                            {
                                msSQL += "'" + lsaddressline2.Replace("'", "") + "',";
                            }

                            msSQL += "''," +
                                       "'" + lspostal_code + "'," +
                                       "'" + lscity + "'," +
                                       "'" + lstaluka + "'," +
                                       "'" + lsdistrict + "'," +
                                       "'" + lsstate + "'," +
                                       "'" + lscountry + "'," +
                                       "'" + employee_gid + "'," +
                                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                            mnResultAddress = objdbconn.ExecuteNonQuerySQL(msSQL);

                            if (mnResult == 1 && mnResultMobile == 1 && mnResultEmail == 1 && mnResultAddress == 1)
                            {
                                insertCount++;
                            }
                            else
                            {
                                msSQL = "delete from ocs_trn_tfarmercontact where farmercontact_gid ='" + msGetGid + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                msSQL = "delete from ocs_trn_tfarmercontact2mobileno where farmercontact2mobileno_gid ='" + msGetGidMobile + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                msSQL = "delete from ocs_trn_tfarmercontact2email where farmercontact2email_gid ='" + msGetGidEmail + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                msSQL = "delete from ocs_trn_tfarmercontact2address where farmercontact2address_gid ='" + msGetGidAddress + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }
                        }
                    }

                }

                if (insertCount > 0)
                {
                    objResult.status = true;
                    objResult.message = insertCount.ToString() + " Of " + dt.Rows.Count.ToString() + " Records Uploaded Successfully";
                }
                else
                {
                    objResult.status = false;
                    objResult.message = "Error occured in uploading Excel Sheet Details";
                }

                dt.Dispose();



            }
            catch (Exception ex)
            {
                objResult.status = false;
                objResult.message = ex.ToString();
            }
        }

        public string dateFormatStandardizer(string sentDate)
        {
            string[] dateArr = sentDate.Split(' ');
            DateTime ldreturnDate = DateTime.ParseExact(dateArr[0], "M/d/yyyy", CultureInfo.InvariantCulture);
            string returnDate = ldreturnDate.ToString("dd-MM-yyyy");
            return returnDate;
        }

        public void DaGetDisbFarmerIndividualImportLog(string application_gid, MdlExcelImportApplication values)
        {
            try
            {
                msSQL = " select concat(first_name, ' ', last_name) as individual_name, contactimportlog_status as reason " +
                        " from ocs_trn_tfarmercontactimportlog" +
                        " where application_gid = '" + application_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getindividualimport_List = new List<individualimport_List>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getindividualimport_List.Add(new individualimport_List
                        {
                            individual_name = (dr_datarow["individual_name"].ToString()),
                            reason = (dr_datarow["reason"].ToString()),
                        });
                    }
                    values.individualimport_List = getindividualimport_List;
                }
                dt_datatable.Dispose();
            }
            catch
            {
                values.status = false;
            }
        }
        public void DaGetFarmerIndividualSummary(string employee_gid, string application_gid, MdlFarmerIndividualSummary values)
        {
            try
            {
                msSQL = " select a.farmercontact_gid,concat(a.first_name, ' ', a.middle_name, ' ', a.last_name) as individual_name, " +
                        " a.pan_no,a.aadhar_no,a.designation_name,a.main_occupation,a.disbursement_amount," +
                        " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as created_by, " +
                        " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date,d.contactcoapplicant_gid " +
                        " from ocs_trn_tfarmercontact a" +
                        " left join hrm_mst_temployee b on b.employee_gid = a.created_by " +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " left join ocs_trn_tcontactcoapplicant d on d.farmercontact_gid = a.farmercontact_gid " +
                        " where a.application_gid='" + application_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getfarmerindividualsummary_list = new List<farmerindividualsummary_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getfarmerindividualsummary_list.Add(new farmerindividualsummary_list
                        {
                            farmercontact_gid = dt["farmercontact_gid"].ToString(),
                            individual_name = dt["individual_name"].ToString(),
                            pan_no = dt["pan_no"].ToString(),
                            aadhar_no = dt["aadhar_no"].ToString(),
                            designation_name = dt["designation_name"].ToString(),
                            main_occupation = dt["main_occupation"].ToString(),
                            created_by = dt["created_by"].ToString(),
                            created_date = dt["created_date"].ToString(),
                            contactcoapplicant_gid = dt["contactcoapplicant_gid"].ToString(),
                            disbursement_amount = dt["disbursement_amount"].ToString(),
                        });
                        values.farmerindividualsummary_list = getfarmerindividualsummary_list;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = ex.ToString();
                values.status = false;
            }
        }

        public void DaGetDisbFarmerIndividualView(string farmercontact_gid, MdlFarmerIndividualDtlView values)
        {
            try
            {
                msSQL = " select a.farmercontact_gid,a.application_gid,a.application_no,a.pan_status,a.pan_no,a.aadhar_no, " +
                        " concat(a.first_name, ' ', a.middle_name, ' ', a.last_name) as individual_name," +
                        " date_format(a.individual_dob, '%d-%m-%Y %h:%i %p') as individual_dob,a.gender_name, " +
                        " a.designation_name,a.educationalqualification_name, " +
                        " a.main_occupation,a.annual_income,a.monthly_income,a.pep_status," +
                        " date_format(a.pepverified_date, '%d-%m-%Y %h:%i %p') as pepverified_date,a.stakeholder_type, " +
                        " a.maritalstatus_name,concat(a.father_firstname, ' ', a.father_middlename, ' ', a.father_lastname) as father_name, " +
                        " date_format(a.father_dob, '%d-%m-%Y %h:%i %p') as father_dob, " +
                        " concat(a.mother_firstname, ' ', a.mother_middlename, ' ', a.mother_lastname) as mother_name," +
                        " date_format(a.mother_dob, '%d-%m-%Y %h:%i %p') as mother_dob, " +
                        " concat(a.spouse_firstname, ' ', a.spouse_middlename, ' ', a.spouse_lastname) as spouse_name," +
                        " date_format(a.spouse_dob, '%d-%m-%Y %h:%i %p') as spouse_dob,a.currentresidence_years, " +
                        " a.branch_distance,a.urn_status,a.urn,a.fathernominee_status,a.mothernominee_status,a.spousenominee_status, " +
                        " a.ifsc_code,a.bank_name,a.branch_name,a.branch_address,a.micr_code,a.bankaccount_number,a.accountholder_name, " +
                        " a.account_type,a.joint_account,a.jointaccountholder_name,a.chequebookfacility_available," +
                        " date_format(a.accountopen_date, '%d-%m-%Y %h:%i %p') as accountopen_date,b.mobile_no,b.primary_status as mobileno_primarystatus, " +
                        " b.whatsapp_no,c.email_address,c.primary_status as emailprimary_status, " +
                        " d.addresstype_name,d.primary_status as addressprimary_status,d.addressline1,d.addressline2," +
                        " d.postal_code,d.city,d.taluka,d.district,d.state,d.country, " +
                        " concat(f.user_firstname, ' ', f.user_lastname, ' / ', f.user_code) as created_by, " +
                        " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date " +
                        " from ocs_trn_tfarmercontact a " +
                        " left join ocs_trn_tfarmercontact2mobileno b on b.farmercontact_gid = a.farmercontact_gid " +
                        " left join ocs_trn_tfarmercontact2email c on c.farmercontact_gid = a.farmercontact_gid " +
                        " left join ocs_trn_tfarmercontact2address d on d.farmercontact_gid = a.farmercontact_gid " +
                        " left join hrm_mst_temployee e on e.employee_gid = a.created_by " +
                        " left join adm_mst_tuser f on f.user_gid = e.user_gid " +
                        " where a.farmercontact_gid='" + farmercontact_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.farmercontact_gid = objODBCDatareader["farmercontact_gid"].ToString();
                    values.application_gid = objODBCDatareader["application_gid"].ToString();
                    values.application_no = objODBCDatareader["application_no"].ToString();
                    values.pan_status = objODBCDatareader["pan_status"].ToString();
                    values.pan_no = objODBCDatareader["pan_no"].ToString();
                    values.aadhar_no = objODBCDatareader["aadhar_no"].ToString();
                    values.individual_name = objODBCDatareader["individual_name"].ToString();
                    values.individual_dob = objODBCDatareader["individual_dob"].ToString();
                    values.gender_name = objODBCDatareader["gender_name"].ToString();
                    values.designation_name = objODBCDatareader["designation_name"].ToString();
                    values.educationalqualification_name = objODBCDatareader["educationalqualification_name"].ToString();
                    values.main_occupation = objODBCDatareader["main_occupation"].ToString();
                    values.annual_income = objODBCDatareader["annual_income"].ToString();
                    values.monthly_income = objODBCDatareader["monthly_income"].ToString();
                    values.pep_status = objODBCDatareader["pep_status"].ToString();
                    values.pepverified_date = objODBCDatareader["pepverified_date"].ToString();
                    values.stakeholder_type = objODBCDatareader["stakeholder_type"].ToString();
                    values.maritalstatus_name = objODBCDatareader["maritalstatus_name"].ToString();
                    values.father_name = objODBCDatareader["father_name"].ToString();
                    values.father_dob = objODBCDatareader["father_dob"].ToString();
                    values.mother_name = objODBCDatareader["mother_name"].ToString();
                    values.mother_dob = objODBCDatareader["mother_dob"].ToString();
                    values.spouse_name = objODBCDatareader["spouse_name"].ToString();
                    values.spouse_dob = objODBCDatareader["spouse_dob"].ToString();
                    values.currentresidence_years = objODBCDatareader["currentresidence_years"].ToString();
                    values.branch_distance = objODBCDatareader["branch_distance"].ToString();
                    values.urn_status = objODBCDatareader["urn_status"].ToString();
                    values.urn = objODBCDatareader["urn"].ToString();
                    values.fathernominee_status = objODBCDatareader["fathernominee_status"].ToString();
                    values.mothernominee_status = objODBCDatareader["mothernominee_status"].ToString();
                    values.spousenominee_status = objODBCDatareader["spousenominee_status"].ToString();
                    values.ifsc_code = objODBCDatareader["ifsc_code"].ToString();
                    values.bank_name = objODBCDatareader["bank_name"].ToString();
                    values.branch_name = objODBCDatareader["branch_name"].ToString();
                    values.branch_address = objODBCDatareader["branch_address"].ToString();
                    values.micr_code = objODBCDatareader["micr_code"].ToString();
                    values.bankaccount_number = objODBCDatareader["bankaccount_number"].ToString();
                    values.accountholder_name = objODBCDatareader["accountholder_name"].ToString();
                    values.account_type = objODBCDatareader["account_type"].ToString();
                    values.joint_account = objODBCDatareader["joint_account"].ToString();
                    values.jointaccountholder_name = objODBCDatareader["jointaccountholder_name"].ToString();
                    values.chequebookfacility_available = objODBCDatareader["chequebookfacility_available"].ToString();
                    values.accountopen_date = objODBCDatareader["accountopen_date"].ToString();
                    values.mobile_no = objODBCDatareader["mobile_no"].ToString();
                    values.mobileno_primarystatus = objODBCDatareader["mobileno_primarystatus"].ToString();
                    values.whatsapp_no = objODBCDatareader["whatsapp_no"].ToString();
                    values.email_address = objODBCDatareader["email_address"].ToString();
                    values.emailprimary_status = objODBCDatareader["emailprimary_status"].ToString();
                    values.addresstype_name = objODBCDatareader["addresstype_name"].ToString();
                    values.addressprimary_status = objODBCDatareader["addressprimary_status"].ToString();
                    values.addressline1 = objODBCDatareader["addressline1"].ToString();
                    values.addressline2 = objODBCDatareader["addressline2"].ToString();
                    values.postal_code = objODBCDatareader["postal_code"].ToString();
                    values.city = objODBCDatareader["city"].ToString();
                    values.taluka = objODBCDatareader["taluka"].ToString();
                    values.district = objODBCDatareader["district"].ToString();
                    values.state = objODBCDatareader["state"].ToString();
                    values.country = objODBCDatareader["country"].ToString();
                    values.created_by = objODBCDatareader["created_by"].ToString();
                    values.created_date = objODBCDatareader["created_date"].ToString();
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
        public bool DaDisbsupplierdocumentUpload(HttpRequest httpRequest, disbsupplieruploaddocument objfilename, string employee_gid)
        {

            upload_list objdocumentmodel = new upload_list();
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string lsdocument_title = httpRequest.Form["document_title"].ToString();
            string lsdisbursementsupplier_gid = httpRequest.Form["disbursementsupplier_gid"].ToString();
            // string lsdocument_id = httpRequest.Form["document_id"].ToString();
            string project_flag = httpRequest.Form["project_flag"].ToString();
            String path = lspath;

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/DisbursementSupplierDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
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
                        // Check Document validation;

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.message = "File format is not supported";
                            return false;
                        }
                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Master/DisbursementSupplierDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "Master/DisbursementSupplierDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        msGetGid = objcmnfunctions.GetMasterGID("DSDU");
                        msSQL = " insert into ocs_mst_tdisbsupplierbankdocument( " +
                                    " disbsupplierbankdocument_gid," +
                                    " disbursementsupplier_gid," +
                                    " document_title  ," +
                                    " document_name  ," +
                                    " document_path," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + employee_gid + "'," +
                                      "'" + lsdocument_title.Replace("'", "") + "'," +
                                    "'" + httpPostedFile.FileName.Replace("'", "") + "'," +
                                    "'" + lspath + msdocument_gid + FileExtension.Replace("'", "") + "'," +
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

                        msSQL = " select disbsupplierbankdocument_gid,disbursementsupplier_gid,document_title,document_name,document_path " +
                                " from ocs_mst_tdisbsupplierbankdocument where disbursementsupplier_gid='" + employee_gid + "' or  disbursementsupplier_gid='" + lsdisbursementsupplier_gid + "'";
                        dt_datatable = objdbconn.GetDataTable(msSQL);
                        var getdisbsupplieruploaddocument_list = new List<disbsupplieruploaddocument_list>();
                        if (dt_datatable.Rows.Count != 0)
                        {
                            foreach (DataRow dt in dt_datatable.Rows)
                            {
                                getdisbsupplieruploaddocument_list.Add(new disbsupplieruploaddocument_list
                                {
                                    document_name = dt["document_name"].ToString(),
                                    document_title = dt["document_title"].ToString(),
                                    document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                                    disbursementsupplier_gid = dt["disbursementsupplier_gid"].ToString(),
                                    disbsupplierbankdocument_gid = dt["disbsupplierbankdocument_gid"].ToString()
                                });
                                objfilename.disbsupplieruploaddocument_list = getdisbsupplieruploaddocument_list;
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

        public void DaDeleteDisbsupplierdocument(string disbsupplierbankdocument_gid, disbsupplieruploaddocument values, string employee_gid)
        {
            msSQL = "delete from ocs_mst_tdisbsupplierbankdocument where disbsupplierbankdocument_gid='" + disbsupplierbankdocument_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Document Deleted successfully";

                msSQL = " select disbsupplierbankdocument_gid,disbursementsupplier_gid,document_name,document_path,document_title " +
                        " from ocs_mst_tdisbsupplierbankdocument where disbursementsupplier_gid='" + employee_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getdisbsupplieruploaddocument_list = new List<disbsupplieruploaddocument_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getdisbsupplieruploaddocument_list.Add(new disbsupplieruploaddocument_list
                        {
                            document_name = dt["document_name"].ToString(),
                            document_title = dt["document_title"].ToString(),
                            document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                            disbursementsupplier_gid = dt["disbursementsupplier_gid"].ToString(),
                            disbsupplierbankdocument_gid = dt["disbsupplierbankdocument_gid"].ToString()
                        });
                        values.disbsupplieruploaddocument_list = getdisbsupplieruploaddocument_list;
                    }
                }
                dt_datatable.Dispose();

            }
            else
            {
                values.status = false;
                values.message = "Error Occurred while deleting Document";
            }
        }
        public bool DaPostDisbursementSupplier(string employee_gid, MdlDisbSupplierBankAcct values)
        {

            msSQL = "select sanction_amount from ocs_trn_tapplication2sanction where  application_gid='" + values.application_gid + "'";
            string lssanction_amount = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " select disbursement_amount as sumdisbursement_amount from ocs_trn_tdisbursementamount  where application_gid='" + values.application_gid + "' and " +
                   " (rmdisbursementrequest_gid !='" + employee_gid + "' and " +
                   " rmdisbursementrequest_gid !='" + values.rmdisbursementrequest_gid + "')" +
                   " and rmdisbursementrequest_gid not in (select rmdisbursementrequest_gid from ocs_trn_tdisbursementassignment where approval_status ='Rejected') " +
                   " and " +
                   " ((makerdisbursement_amount is null ||makerdisbursement_amount ='') && " +
                   " (checkerdisbursement_amount is null ||checkerdisbursement_amount ='')) " +
                   " union all" +
                   " select makerdisbursement_amount  as sumdisbursement_amount from ocs_trn_tdisbursementamount  where application_gid='" + values.application_gid + "' and " +
                   " (rmdisbursementrequest_gid !='" + employee_gid + "' and " +
                   " rmdisbursementrequest_gid !='" + values.rmdisbursementrequest_gid + "')" +
                   " and rmdisbursementrequest_gid not in (select rmdisbursementrequest_gid from ocs_trn_tdisbursementassignment where approval_status ='Rejected') " +
                   " and " +
                   " ((makerdisbursement_amount is not null ||makerdisbursement_amount !='') && " +
                   " (checkerdisbursement_amount is  null ||checkerdisbursement_amount ='')) " +
                   " union all " +
                   " select checkerdisbursement_amount as sumdisbursement_amount from ocs_trn_tdisbursementamount  where application_gid='" + values.application_gid + "' and " +
                   " (rmdisbursementrequest_gid !='" + employee_gid + "' and " +
                   " rmdisbursementrequest_gid  !='" + values.rmdisbursementrequest_gid + "')" +
                   " and rmdisbursementrequest_gid not in (select rmdisbursementrequest_gid from ocs_trn_tdisbursementassignment where approval_status ='Rejected') " +
                   " and " +
                   " ((checkerdisbursement_amount is not null ||checkerdisbursement_amount !=''))" +
                    " union all" +
                    " select case when (creditopscheckerdisbursement_amount is null and  creditopsdisbursement_amount is null) then disbursement_amount  " +
                    " when (creditopscheckerdisbursement_amount is null ) then creditopsdisbursement_amount  " +
                    " else creditopscheckerdisbursement_amount end as sumdisbursement_amount " +
                    " from ocs_trn_tdisbursementsupplier  where application_gid='" + values.application_gid + "' and " +
                    " disbursementsupplier_gid !='" + values.disbursementsupplier_gid + "' " +
                    " and rmdisbursementrequest_gid not in (select rmdisbursementrequest_gid from ocs_trn_tdisbursementassignment where approval_status ='Rejected') " +
                    " union all" +
                    " select case when (creditopscheckerdisbursement_amount is null and  creditopsdisbursement_amount is null) then disbursement_amount  " +
                    " when (creditopscheckerdisbursement_amount is null ) then creditopsdisbursement_amount  " +
                    " else creditopscheckerdisbursement_amount end as sumdisbursement_amount " +
                    " from ocs_trn_tfarmercontact  where application_gid='" + values.application_gid + "' and " +
                    " farmercontact_gid !='" + values.farmercontact_gid + "' " +
                    " and rmdisbursementrequest_gid not in (select rmdisbursementrequest_gid from ocs_trn_tdisbursementassignment where approval_status ='Rejected') ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdisbursementamount_list = new List<disbursementamount_list>();
            double disbursementamount_total = 0;
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {

                    disbursementamount_total = disbursementamount_total + double.Parse(dr_datarow["sumdisbursement_amount"].ToString().Replace(",", ""));

                }

            }
            dt_datatable.Dispose();


            if (string.IsNullOrEmpty(values.disbursement_amount))
            {

            }
            else
            {
                disbursementamount_total = disbursementamount_total + double.Parse(values.disbursement_amount.Replace(",", ""));
            }

            if (disbursementamount_total <= double.Parse(lssanction_amount))
            {

            }
            else
            {
                values.status = false;
                values.message = "Disbursement Amount is Greater than Sanction Amount";

                return false;
            }

            msGetGid = objcmnfunctions.GetMasterGID("DSSP");
            msSQL = " insert into ocs_trn_tdisbursementsupplier(" +
                    " disbursementsupplier_gid," +
                    " application_gid," +
                    " supplier_gid," +
                    " supplier_name," +
                    " supplier2bank_gid, " +
                    " ifsc_code," +
                    " micr_code," +
                    " branch_address," +
                    " bank_name," +
                    " branch_name," +
                    " bankaccount_number," +
                    " confirmbankaccount_number," +
                    " accountholder_name," +
                    " disbursement_amount," +
                    " rmdisbursementrequest_gid," +
                    " bankaccounttype_name," +
                    " jointaccount_status," +
                    " jointaccountholder_name," +
                    " chequebook_status," +
                    " accountopen_date," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.application_gid + "'," +
                    "'" + values.supplier_gid + "'," +
                    "'" + values.supplier_name.Replace("'", "") + "'," +
                    "'" + values.supplier2bank_gid + "'," +
                    "'" + values.ifsc_code + "'," +
                    "'" + values.micr_code + "'," +
                    "'" + values.branch_address + "'," +
                    "'" + values.bank_name + "'," +
                    "'" + values.branch_name + "'," +
                    "'" + values.bankaccount_number + "'," +
                    "'" + values.confirmbankaccount_number + "'," +
                    "'" + values.accountholder_name + "'," +
                    "'" + values.disbursement_amount + "'," +
                    "'" + employee_gid + "'," +
                    "'" + values.bankaccounttype_name + "'," +
                    "'" + values.jointaccount_status + "'," +
                    "'" + values.jointaccountholder_name + "'," +
                    "'" + values.chequebook_status + "',";
            if (values.accountopen_date == null || values.accountopen_date == "")
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.accountopen_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            msSQL += "'" + employee_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msSQL = "update ocs_mst_tdisbsupplierbankdocument set disbursementsupplier_gid='" + msGetGid + "' where disbursementsupplier_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " select disbursementsupplier_gid,supplier_name,bank_name,branch_name,ifsc_code, " +
                        " bankaccount_number,accountholder_name, " +
                        " concat(f.user_firstname, ' ', f.user_lastname, ' / ', f.user_code) as created_by, " +
                        " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date " +
                        " from ocs_trn_tdisbursementsupplier a " +
                        " left join hrm_mst_temployee e on e.employee_gid = a.created_by " +
                        " left join adm_mst_tuser f on f.user_gid = e.user_gid " +
                        " where a.rmdisbursementrequest_gid='" + employee_gid + "' or " +
                        " a.rmdisbursementrequest_gid='" + values.rmdisbursementrequest_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getdisbsupplierdtl_list = new List<disbsupplierdtl_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getdisbsupplierdtl_list.Add(new disbsupplierdtl_list
                        {
                            supplier_name = dt["supplier_name"].ToString(),
                            bank_name = dt["bank_name"].ToString(),
                            branch_name = dt["branch_name"].ToString(),
                            ifsc_code = dt["ifsc_code"].ToString(),
                            bankaccount_number = dt["bankaccount_number"].ToString(),
                            accountholder_name = dt["accountholder_name"].ToString(),
                            disbursementsupplier_gid = dt["disbursementsupplier_gid"].ToString(),
                            created_by = dt["created_by"].ToString(),
                            created_date = dt["created_date"].ToString(),
                        });
                        values.disbsupplierdtl_list = getdisbsupplierdtl_list;
                    }
                }
                dt_datatable.Dispose();
                values.status = true;
                values.message = "Supplier Details Added Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured While Adding Supplier Details";
                return false;
            }

        }
        public void DaGetDisbursementSupplierSummary(string rmdisbursementrequest_gid, MdlDisbSupplierBankAcct values)
        {
            try
            {
                msSQL = " select disbursementsupplier_gid,supplier_name,bank_name,branch_name,ifsc_code, " +
                        " bankaccount_number,accountholder_name,disbursement_amount, disbursement_amount as rmdisbursement_amount," +
                        " concat(f.user_firstname, ' ', f.user_lastname, ' / ', f.user_code) as created_by, " +
                        " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date,creditopsdisbursement_amount, " +
                        " creditopscheckerdisbursement_amount,a.disbursementbookingencore_status from ocs_trn_tdisbursementsupplier a " +
                        " left join hrm_mst_temployee e on e.employee_gid = a.created_by " +
                        " left join adm_mst_tuser f on f.user_gid = e.user_gid " +
                        " where rmdisbursementrequest_gid='" + rmdisbursementrequest_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getdisbsupplierdtl_list = new List<disbsupplierdtl_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getdisbsupplierdtl_list.Add(new disbsupplierdtl_list
                        {
                            supplier_name = dt["supplier_name"].ToString(),
                            bank_name = dt["bank_name"].ToString(),
                            branch_name = dt["branch_name"].ToString(),
                            ifsc_code = dt["ifsc_code"].ToString(),
                            bankaccount_number = dt["bankaccount_number"].ToString(),
                            accountholder_name = dt["accountholder_name"].ToString(),
                            disbursementsupplier_gid = dt["disbursementsupplier_gid"].ToString(),
                            disbursement_amount = dt["disbursement_amount"].ToString(),
                            rmdisbursement_amount = dt["rmdisbursement_amount"].ToString(),
                            created_by = dt["created_by"].ToString(),
                            created_date = dt["created_date"].ToString(),
                            creditopsdisbursement_amount = dt["creditopsdisbursement_amount"].ToString(),
                            creditopscheckerdisbursement_amount = dt["creditopscheckerdisbursement_amount"].ToString(),
                            disbursementbookingencore_status = dt["disbursementbookingencore_status"].ToString()
                        });
                        values.disbsupplierdtl_list = getdisbsupplierdtl_list;
                    }
                }
                dt_datatable.Dispose();
            }
            catch
            {
                values.status = false;
            }
        }
        public void DaGetDisbSupplierDocTempClear(string employee_gid, result values)
        {
            msSQL = "delete from ocs_mst_tdisbsupplierbankdocument where disbursementsupplier_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            values.status = true;
        }

        public void DaGetDisbSupplierDtlView(string disbursementsupplier_gid, MdlDisbSupplierBankAcct values)
        {
            try
            {
                msSQL = " select a.disbursementaccount_status,a.disbursementsupplier_gid,a.application_gid,a.supplier_name,a.ifsc_code,a.micr_code, " +
                        " a.branch_address,a.bank_name,a.branch_name,a.bankaccount_number,a.confirmbankaccount_number," +
                        " a.accountholder_name,a.disbursement_amount, " +
                        " concat(f.user_firstname, ' ', f.user_lastname, ' / ', f.user_code) as created_by, " +
                        " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date," +
                        " bankaccounttype_name,jointaccount_status,jointaccountholder_name,chequebook_status," +
                        " date_format(a.accountopen_date, '%d-%m-%Y %h:%i %p') as accountopen_date " +
                        " from ocs_trn_tdisbursementsupplier a " +
                        " left join hrm_mst_temployee e on e.employee_gid = a.created_by " +
                        " left join adm_mst_tuser f on f.user_gid = e.user_gid " +
                        " where a.disbursementsupplier_gid='" + disbursementsupplier_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.disbursementsupplier_gid = objODBCDatareader["disbursementsupplier_gid"].ToString();
                    values.application_gid = objODBCDatareader["application_gid"].ToString();
                    values.supplier_name = objODBCDatareader["supplier_name"].ToString();
                    values.ifsc_code = objODBCDatareader["ifsc_code"].ToString();
                    values.micr_code = objODBCDatareader["micr_code"].ToString();
                    values.branch_address = objODBCDatareader["branch_address"].ToString();
                    values.bank_name = objODBCDatareader["bank_name"].ToString();
                    values.branch_name = objODBCDatareader["branch_name"].ToString();
                    values.bankaccount_number = objODBCDatareader["bankaccount_number"].ToString();
                    values.confirmbankaccount_number = objODBCDatareader["confirmbankaccount_number"].ToString();
                    values.accountholder_name = objODBCDatareader["accountholder_name"].ToString();
                    values.disbursement_amount = objODBCDatareader["disbursement_amount"].ToString();
                    values.created_by = objODBCDatareader["created_by"].ToString();
                    values.created_date = objODBCDatareader["created_date"].ToString();
                    values.disbursementaccount_status = objODBCDatareader["disbursementaccount_status"].ToString();
                    values.bankaccounttype_name = objODBCDatareader["bankaccounttype_name"].ToString();
                    values.jointaccount_status = objODBCDatareader["jointaccount_status"].ToString();
                    values.jointaccountholder_name = objODBCDatareader["jointaccountholder_name"].ToString();
                    values.chequebook_status = objODBCDatareader["chequebook_status"].ToString();
                    values.accountopen_date = objODBCDatareader["accountopen_date"].ToString();
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
        public void DaDisbsupplierdocumentView(string disbursementsupplier_gid, disbsupplieruploaddocument values, string employee_gid)
        {
            msSQL = " select disbsupplierbankdocument_gid,disbursementsupplier_gid,document_name,document_path,document_title " +
                    " from ocs_mst_tdisbsupplierbankdocument where disbursementsupplier_gid='" + disbursementsupplier_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdisbsupplieruploaddocument_list = new List<disbsupplieruploaddocument_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getdisbsupplieruploaddocument_list.Add(new disbsupplieruploaddocument_list
                    {
                        document_name = dt["document_name"].ToString(),
                        document_title = dt["document_title"].ToString(),
                        document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                        disbursementsupplier_gid = dt["disbursementsupplier_gid"].ToString(),
                        disbsupplierbankdocument_gid = dt["disbsupplierbankdocument_gid"].ToString()
                    });
                    values.disbsupplieruploaddocument_list = getdisbsupplieruploaddocument_list;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaGetDisbursementDocTempClear(string employee_gid, result values)
        {
            msSQL = "delete from ocs_trn_trmdisbursementdocument where rmdisbursementrequest_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            values.status = true;
        }

        public void DaSubmitCoapplicantContactDtlAdd(string employee_gid, MdlMstCoApplicantContact values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("CCAG");
            msSQL = " insert into ocs_trn_tcontactcoapplicant(" +
                   " contactcoapplicant_gid," +
                   " farmercontact_gid," +
                   " application_gid," +
                   " pan_status," +
                   " pan_no," +
                   " aadhar_no," +
                   " first_name," +
                   " middle_name," +
                   " last_name," +
                   " individual_dob," +
                   " age," +
                   " gender_gid," +
                   " gender_name," +
                   " designation_gid," +
                   " designation_name," +
                   " educationalqualification_gid," +
                   " educationalqualification_name," +
                   " main_occupation," +
                   " annual_income," +
                   " monthly_income," +
                   " pep_status," +
                   " pepverified_date," +
                   " maritalstatus_gid," +
                   " maritalstatus_name," +
                   " father_firstname," +
                   " father_middlename," +
                   " father_lastname," +
                   " father_dob," +
                   " father_age," +
                   " mother_firstname," +
                   " mother_middlename," +
                   " mother_lastname," +
                   " mother_dob," +
                   " mother_age," +
                   " spouse_firstname," +
                   " spouse_middlename," +
                   " spouse_lastname," +
                   " spouse_dob," +
                   " spouse_age," +
                   " currentresidence_years," +
                   " branch_distance," +
                   " fathernominee_status," +
                   " mothernominee_status," +
                   " spousenominee_status," +
                   " mobile_no," +
                   " primary_status," +
                   " whatsapp_no," +
                   " email_address," +
                   " emailprimary_status," +
                   " addresstype_gid," +
                   " addresstype_name," +
                   " addressprimary_status," +
                   " addressline1," +
                   " addressline2," +
                   " landmark," +
                   " postal_code," +
                   " city," +
                   " taluka," +
                   " district," +
                   " state," +
                   " country," +
                   " latitude," +
                   " longitude," +
                   " ifsc_code," +
                   " bank_name," +
                   " branch_name," +
                   " branch_address," +
                   " micr_code," +
                   " bankaccount_number," +
                   " confirmbankaccount_number," +
                   " accountholder_name," +
                   " rmdisbursementrequest_gid," +
                   " created_by," +
                   " created_date)" +
                   " values(" +
                   "'" + msGetGid + "'," +
                   "'" + values.farmercontact_gid + "'," +
                   "'" + values.application_gid + "'," +
                   "'" + values.pan_status + "'," +
                   "'" + values.pan_no + "'," +
                   "'" + values.aadhar_no + "',";
            if (values.first_name == "" || values.first_name == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.first_name.Replace("'", "") + "',";
            }
            if (values.middle_name == "" || values.middle_name == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.middle_name.Replace("'", "") + "',";
            }
            if (values.last_name == "" || values.last_name == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.last_name.Replace("'", "") + "',";
            }
            msSQL += "'" + values.individual_dob + "'," +
                     "'" + values.age + "'," +
                     "'" + values.gender_gid + "'," +
                     "'" + values.gender_name + "'," +
                     "'" + values.designation_gid + "'," +
                     "'" + values.designation_name + "'," +
                     "'" + values.educationalqualification_gid + "'," +
                     "'" + values.educationalqualification_name + "'," +
                     "'" + values.main_occupation + "'," +
                     "'" + values.annual_income + "'," +
                     "'" + values.monthly_income + "'," +
                     "'" + values.pep_status + "',";

            if ((values.pepverified_date == null) || (values.pepverified_date == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.pepverified_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }



            msSQL += "'" + values.maritalstatus_gid + "'," +
                     "'" + values.maritalstatus_name + "',";
            if (values.father_firstname == "" || values.father_firstname == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.father_firstname.Replace("'", "") + "',";
            }
            if (values.father_middlename == "" || values.father_middlename == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.father_middlename.Replace("'", "") + "',";
            }
            if (values.father_lastname == "" || values.father_lastname == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.father_lastname.Replace("'", "") + "',";
            }
            msSQL += "'" + values.father_dob + "'," +
                     "'" + values.father_age + "',";
            if (values.mother_firstname == "" || values.mother_firstname == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.mother_firstname.Replace("'", "") + "',";
            }
            if (values.mother_middlename == "" || values.mother_middlename == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.mother_middlename.Replace("'", "") + "',";
            }
            if (values.mother_lastname == "" || values.mother_lastname == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.mother_lastname.Replace("'", "") + "',";
            }
            msSQL += "'" + values.mother_dob + "'," +
                     "'" + values.mother_age + "',";
            if (values.spouse_firstname == "" || values.spouse_firstname == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.spouse_firstname.Replace("'", "") + "',";
            }
            if (values.spouse_middlename == "" || values.spouse_middlename == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.spouse_middlename.Replace("'", "") + "',";
            }
            if (values.spouse_lastname == "" || values.spouse_lastname == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.spouse_lastname.Replace("'", "") + "',";
            }
            msSQL += "'" + values.spouse_dob + "'," +
                     "'" + values.spouse_age + "'," +
                     "'" + values.currentresidence_years + "'," +
                     "'" + values.branch_distance + "'," +
                     "'" + values.fathernominee_status + "'," +
                     "'" + values.mothernominee_status + "'," +
                     "'" + values.spousenominee_status + "'," +
                     "'" + values.mobile_no + "'," +
                     "'" + values.primary_status + "'," +
                     "'" + values.whatsapp_no + "'," +
                     "'" + values.email_address + "'," +
                     "'" + values.emailprimary_status + "'," +
                     "'" + values.addresstype_gid + "'," +
                     "'" + values.addresstype_name + "'," +
                     "'" + values.addressprimary_status + "'," +
                     "'" + values.addressline1 + "'," +
                     "'" + values.addressline2 + "'," +
                     "'" + values.landmark + "'," +
                     "'" + values.postal_code + "'," +
                     "'" + values.city + "'," +
                     "'" + values.taluka + "'," +
                     "'" + values.district + "'," +
                     "'" + values.state + "'," +
                     "'" + values.country + "'," +
                     "'" + values.latitude + "'," +
                     "'" + values.longitude + "'," +
                     "'" + values.ifsc_code + "'," +
                     "'" + values.bank_name + "'," +
                     "'" + values.branch_name + "'," +
                     "'" + values.branch_address + "'," +
                     "'" + values.micr_code + "'," +
                     "'" + values.bankaccount_number + "'," +
                     "'" + values.confirmbankaccount_number + "'," +
                     "'" + values.accountholder_name + "'," +
                     "'" + employee_gid + "'," +
                     "'" + employee_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                if (values.pan_status == "Customer Submitting Form 60")
                {
                    // PAN Update
                    foreach (string reason in values.panabsencereason_selectedlist)
                    {
                        msGetGidpan = objcmnfunctions.GetMasterGID("CCPR");
                        msSQL = " INSERT INTO ocs_mst_tcoapplicantcontact2panabsencereason(" +
                               " coapplicantcontact2panabsencereason_gid," +
                               " farmercontact_gid," +
                               " application_gid," +
                               " contactcoapplicant_gid," +
                               " panabsencereason," +
                               " created_date," +
                               " created_by)" +
                               " VALUES(" +
                               "'" + msGetGidpan + "'," +
                               "'" + values.farmercontact_gid + "'," +
                               "'" + values.application_gid + "'," +
                               "'" + employee_gid + "'," +
                               "'" + reason + "'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                               "'" + employee_gid + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }

                //Updates

                msSQL = "update ocs_mst_tcoapplicantcontact2document set contactcoapplicant_gid ='" + msGetGid + "' where contactcoapplicant_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_trn_tcadkycpanauthentication set function_gid ='" + values.application_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tkycpanaadhaarlink set function_gid ='" + values.application_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_trn_tcadkycifscauthentication set function_gid ='" + values.application_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tcoapplicantcontact2panform60 set contactcoapplicant_gid ='" + msGetGid + "' where contactcoapplicant_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tcoapplicantcontact2panabsencereason set contactcoapplicant_gid ='" + msGetGid + "' where contactcoapplicant_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Co-Applicant Details Added Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
            }
        }
        public bool DaCoapplicantContactDocumentUpload(HttpRequest httpRequest, uploaddocument objfilename, string employee_gid)
        {
            upload_list objdocumentmodel = new upload_list();
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string lsdocument_title = httpRequest.Form["document_title"].ToString();
            String path = lspath;
            string lsindividualdocument_gid = httpRequest.Form["individualdocument_gid"].ToString();
            string project_flag = httpRequest.Form["project_flag"].ToString();
            string lsfarmercontact_gid = httpRequest.Form["farmercontact_gid"].ToString();
            string lsapplication_gid = httpRequest.Form["application_gid"].ToString();

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/CoapplicantContactDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
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


                        // Check Document validation;

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.message = "File format is not supported";
                            return false;
                        }

                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Master/CoapplicantContactDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "Master/CoapplicantContactDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        msGetGid = objcmnfunctions.GetMasterGID("CACD");

                        msSQL = " insert into ocs_mst_tcoapplicantcontact2document( " +
                                    " coapplicantcontact2document_gid, " +
                                    " farmercontact_gid, " +
                                    " application_gid, " +
                                    " contactcoapplicant_gid, " +
                                    " document_title, " +
                                    " document_name, " +
                                    " document_path," +
                                    " individualdocument_gid, " +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + lsfarmercontact_gid + "'," +
                                    "'" + lsapplication_gid + "'," +
                                     "'" + employee_gid + "'," +
                                    "'" + lsdocument_title.Replace("'", "") + "'," +
                                    "'" + httpPostedFile.FileName.Replace("'", "") + "'," +
                                    "'" + lspath + msdocument_gid + FileExtension.Replace("'", "") + "'," +
                                    "'" + lsindividualdocument_gid + "'," +
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
                    }
                }
            }
            catch (Exception ex)
            {
                objfilename.message = ex.ToString();
            }
            return true;
        }
        public void DaGetCoapplicantContactDocList(string employee_gid, MdlCoapplicantContactDocument values)
        {
            msSQL = " select coapplicantcontact2document_gid,document_name,document_path,document_title,migration_flag " +
                    " from ocs_mst_tcoapplicantcontact2document " +
                    " where contactcoapplicant_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdocumentdtlList = new List<coapplicantuploadindividualdoc_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                // Create list
                var file_name = new List<string>();
                var file_path = string.Empty;

                foreach (DataRow dt in dt_datatable.Rows)
                {
                    file_name.Add(dt["document_name"].ToString());
                    file_path = objcmnstorage.EncryptData(dt["document_path"].ToString());
                }
                values.filename = file_name.ToArray();
                values.filepath = file_path.ToString();

                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getdocumentdtlList.Add(new coapplicantuploadindividualdoc_list
                    {
                        migration_flag = dt["migration_flag"].ToString(),
                        document_name = dt["document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                        coapplicantcontact2document_gid = dt["coapplicantcontact2document_gid"].ToString(),
                        document_title = dt["document_title"].ToString(),
                    });
                    values.coapplicantuploadindividualdoc_list = getdocumentdtlList;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaCoapplicantContactDocDelete(string coapplicantcontact2document_gid, MdlCoapplicantContactDocument values)
        {
            msSQL = "delete from ocs_mst_tcoapplicantcontact2document where coapplicantcontact2document_gid='" + coapplicantcontact2document_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.message = "Document Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }
        public bool DaCoapplicantPANForm60DocumentUpload(HttpRequest httpRequest, uploaddocument objfilename, string employee_gid)
        {
            upload_list objdocumentmodel = new upload_list();
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string project_flag = httpRequest.Form["project_flag"].ToString();
            string lsfarmercontact_gid = httpRequest.Form["farmercontact_gid"].ToString();
            string lsapplication_gid = httpRequest.Form["application_gid"].ToString();
            String path = lspath;

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/CoapplicantPANForm60Document/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
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


                        // Check Document validation;

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.message = "File format is not supported";
                            return false;
                        }


                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Master/CoapplicantPANForm60Document/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "Master/CoapplicantPANForm60Document/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        msGetGid = objcmnfunctions.GetMasterGID("CAPF");
                        msSQL = " insert into ocs_mst_tcoapplicantcontact2panform60(" +
                                " coapplicantcontact2panform60_gid," +
                                " farmercontact_gid," +
                                " application_gid, " +
                                " contactcoapplicant_gid, " +
                                " document_name," +
                                " document_path," +
                                " created_by," +
                                " created_date)" +
                                " values(" +
                                "'" + msGetGid + "'," +
                                "'" + lsfarmercontact_gid + "'," +
                                "'" + lsapplication_gid + "'," +
                                "'" + employee_gid + "'," +
                                "'" + httpPostedFile.FileName.Replace("'", "") + "'," +
                                "'" + lspath + msdocument_gid + FileExtension.Replace("'", "") + "'," +
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
                    }
                }
            }
            catch (Exception ex)
            {
                objfilename.message = ex.ToString();
            }
            return true;
        }
        public void DaGetCoapplicantPANForm60List(string employee_gid, MdlCoapplicantContactPANForm60 values)
        {
            msSQL = " select coapplicantcontact2panform60_gid,document_name, document_path " +
                    " from ocs_mst_tcoapplicantcontact2panform60  " +
                    " where contactcoapplicant_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getCoapplicantcontactpanform60_list = new List<Coapplicantcontactpanform60_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getCoapplicantcontactpanform60_list.Add(new Coapplicantcontactpanform60_list
                    {
                        coapplicantcontact2panform60_gid = (dr_datarow["coapplicantcontact2panform60_gid"].ToString()),
                        document_name = (dr_datarow["document_name"].ToString()),
                        document_path = objcmnstorage.EncryptData(dr_datarow["document_path"].ToString()),
                    });

                    values.Coapplicantcontactpanform60_list = getCoapplicantcontactpanform60_list;
                }
                dt_datatable.Dispose();
            }

        }

        public void DaCoapplicantPANForm60Delete(string coapplicantcontact2panform60_gid, MdlCoapplicantContactPANForm60 values)
        {
            msSQL = "delete from ocs_mst_tcoapplicantcontact2panform60 where coapplicantcontact2panform60_gid='" + coapplicantcontact2panform60_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "Form-60 Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }
        public void GetCoApplicantTempClear(string employee_gid, result values)
        {
            msSQL = "delete from ocs_mst_tcoapplicantcontact2panform60 where contactcoapplicant_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from ocs_mst_tcoapplicantcontact2document where contactcoapplicant_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            values.status = true;
        }
        public void DaGetDisbCoApplicantDtlView(string contactcoapplicant_gid, MdlCoApplicantDtlView values)
        {
            try
            {
                msSQL = " select a.contactcoapplicant_gid,a.farmercontact_gid,a.application_gid,a.application_no,a.pan_status,a.pan_no,a.aadhar_no, " +
                        " concat(a.first_name, ' ', a.middle_name, ' ', a.last_name) as individual_name," +
                        " a.individual_dob,a.gender_name, " +
                        " a.designation_name,a.educationalqualification_name, " +
                        " a.main_occupation,a.annual_income,a.monthly_income,a.pep_status," +
                        " date_format(a.pepverified_date, '%d-%m-%Y %h:%i %p') as pepverified_date,a.stakeholder_type, " +
                        " a.maritalstatus_name,concat(a.father_firstname, ' ', a.father_middlename, ' ', a.father_lastname) as father_name, " +
                        " a.father_dob, " +
                        " concat(a.mother_firstname, ' ', a.mother_middlename, ' ', a.mother_lastname) as mother_name," +
                        " a.mother_dob, " +
                        " concat(a.spouse_firstname, ' ', a.spouse_middlename, ' ', a.spouse_lastname) as spouse_name," +
                        " a.spouse_dob,a.currentresidence_years, " +
                        " a.branch_distance,a.fathernominee_status,a.mothernominee_status,a.spousenominee_status, " +
                        " a.ifsc_code,a.bank_name,a.branch_name,a.branch_address,a.micr_code,a.bankaccount_number,a.accountholder_name, " +
                        " a.mobile_no,a.primary_status as mobileno_primarystatus, " +
                        " a.whatsapp_no,a.email_address,a.emailprimary_status as emailprimary_status, " +
                        " a.addresstype_name,a.addressprimary_status as addressprimary_status,a.addressline1,a.addressline2," +
                        " a.postal_code,a.city,a.taluka,a.district,a.state,a.country, " +
                        " concat(f.user_firstname, ' ', f.user_lastname, ' / ', f.user_code) as created_by, " +
                        " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date " +
                        " from ocs_trn_tcontactcoapplicant a " +
                        " left join hrm_mst_temployee e on e.employee_gid = a.created_by " +
                        " left join adm_mst_tuser f on f.user_gid = e.user_gid " +
                        " where a.contactcoapplicant_gid='" + contactcoapplicant_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.farmercontact_gid = objODBCDatareader["farmercontact_gid"].ToString();
                    values.application_gid = objODBCDatareader["application_gid"].ToString();
                    values.application_no = objODBCDatareader["application_no"].ToString();
                    values.pan_status = objODBCDatareader["pan_status"].ToString();
                    values.pan_no = objODBCDatareader["pan_no"].ToString();
                    values.aadhar_no = objODBCDatareader["aadhar_no"].ToString();
                    values.individual_name = objODBCDatareader["individual_name"].ToString();
                    values.individual_dob = objODBCDatareader["individual_dob"].ToString();
                    values.gender_name = objODBCDatareader["gender_name"].ToString();
                    values.designation_name = objODBCDatareader["designation_name"].ToString();
                    values.educationalqualification_name = objODBCDatareader["educationalqualification_name"].ToString();
                    values.main_occupation = objODBCDatareader["main_occupation"].ToString();
                    values.annual_income = objODBCDatareader["annual_income"].ToString();
                    values.monthly_income = objODBCDatareader["monthly_income"].ToString();
                    values.pep_status = objODBCDatareader["pep_status"].ToString();
                    values.pepverified_date = objODBCDatareader["pepverified_date"].ToString();
                    values.stakeholder_type = objODBCDatareader["stakeholder_type"].ToString();
                    values.maritalstatus_name = objODBCDatareader["maritalstatus_name"].ToString();
                    values.father_name = objODBCDatareader["father_name"].ToString();
                    values.father_dob = objODBCDatareader["father_dob"].ToString();
                    values.mother_name = objODBCDatareader["mother_name"].ToString();
                    values.mother_dob = objODBCDatareader["mother_dob"].ToString();
                    values.spouse_name = objODBCDatareader["spouse_name"].ToString();
                    values.spouse_dob = objODBCDatareader["spouse_dob"].ToString();
                    values.currentresidence_years = objODBCDatareader["currentresidence_years"].ToString();
                    values.branch_distance = objODBCDatareader["branch_distance"].ToString();
                    values.fathernominee_status = objODBCDatareader["fathernominee_status"].ToString();
                    values.mothernominee_status = objODBCDatareader["mothernominee_status"].ToString();
                    values.spousenominee_status = objODBCDatareader["spousenominee_status"].ToString();
                    values.ifsc_code = objODBCDatareader["ifsc_code"].ToString();
                    values.bank_name = objODBCDatareader["bank_name"].ToString();
                    values.branch_name = objODBCDatareader["branch_name"].ToString();
                    values.branch_address = objODBCDatareader["branch_address"].ToString();
                    values.micr_code = objODBCDatareader["micr_code"].ToString();
                    values.bankaccount_number = objODBCDatareader["bankaccount_number"].ToString();
                    values.accountholder_name = objODBCDatareader["accountholder_name"].ToString();
                    values.mobile_no = objODBCDatareader["mobile_no"].ToString();
                    values.mobileno_primarystatus = objODBCDatareader["mobileno_primarystatus"].ToString();
                    values.whatsapp_no = objODBCDatareader["whatsapp_no"].ToString();
                    values.email_address = objODBCDatareader["email_address"].ToString();
                    values.emailprimary_status = objODBCDatareader["emailprimary_status"].ToString();
                    values.addresstype_name = objODBCDatareader["addresstype_name"].ToString();
                    values.addressprimary_status = objODBCDatareader["addressprimary_status"].ToString();
                    values.addressline1 = objODBCDatareader["addressline1"].ToString();
                    values.addressline2 = objODBCDatareader["addressline2"].ToString();
                    values.postal_code = objODBCDatareader["postal_code"].ToString();
                    values.city = objODBCDatareader["city"].ToString();
                    values.taluka = objODBCDatareader["taluka"].ToString();
                    values.district = objODBCDatareader["district"].ToString();
                    values.state = objODBCDatareader["state"].ToString();
                    values.country = objODBCDatareader["country"].ToString();
                    values.created_by = objODBCDatareader["created_by"].ToString();
                    values.created_date = objODBCDatareader["created_date"].ToString();
                }

                msSQL = " select coapplicantcontact2panform60_gid,document_name, document_path, " +
                        " concat(f.user_firstname, ' ', f.user_lastname, ' / ', f.user_code) as created_by, " +
                        " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date " +
                        " from ocs_mst_tcoapplicantcontact2panform60 a" +
                        " left join hrm_mst_temployee e on e.employee_gid = a.created_by " +
                        " left join adm_mst_tuser f on f.user_gid = e.user_gid " +
                        " where a.contactcoapplicant_gid='" + contactcoapplicant_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getCoapplicantcontactpanform60_list = new List<Coapplicantcontactpanform60_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getCoapplicantcontactpanform60_list.Add(new Coapplicantcontactpanform60_list
                        {
                            coapplicantcontact2panform60_gid = (dr_datarow["coapplicantcontact2panform60_gid"].ToString()),
                            document_name = (dr_datarow["document_name"].ToString()),
                            document_path = objcmnstorage.EncryptData(dr_datarow["document_path"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString())
                        });

                        values.Coapplicantcontactpanform60_list = getCoapplicantcontactpanform60_list;
                    }
                    dt_datatable.Dispose();
                }

                msSQL = " select coapplicantcontact2document_gid,document_name,document_path,document_title, " +
                        " migration_flag,concat(f.user_firstname, ' ', f.user_lastname, ' / ', f.user_code) as created_by, " +
                        " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date " +
                        " from ocs_mst_tcoapplicantcontact2document a" +
                        " left join hrm_mst_temployee e on e.employee_gid = a.created_by " +
                        " left join adm_mst_tuser f on f.user_gid = e.user_gid " +
                   " where a.contactcoapplicant_gid='" + contactcoapplicant_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getdocumentdtlList = new List<coapplicantuploadindividualdoc_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getdocumentdtlList.Add(new coapplicantuploadindividualdoc_list
                        {
                            migration_flag = dt["migration_flag"].ToString(),
                            document_name = dt["document_name"].ToString(),
                            document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                            coapplicantcontact2document_gid = dt["coapplicantcontact2document_gid"].ToString(),
                            document_title = dt["document_title"].ToString(),
                            created_by = dt["created_by"].ToString(),
                            created_date = dt["created_date"].ToString(),
                        });
                        values.coapplicantuploadindividualdoc_list = getdocumentdtlList;
                    }
                }

                msSQL = " SELECT panabsencereason" +
                        " from ocs_mst_tcoapplicantcontact2panabsencereason where contactcoapplicant_gid='" + contactcoapplicant_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcoapplicantpanabsencereasons_list = new List<coapplicantpanabsencereasons_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    values.coapplicantpanabsencereasons_list = dt_datatable.AsEnumerable().Select(row =>
                      new coapplicantpanabsencereasons_list
                      {
                          coapplicantpanabsencereason = row["panabsencereason"].ToString(),
                      }
                    ).ToList();
                }
                dt_datatable.Dispose();

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

        public void DaGetLSARefNoDropDown(string employee_gid, string application2sanction_gid, MdlMstLSARefNoDropDown values)
        {
            msSQL = " select generatelsa_gid, lsa_refno from ocs_trn_tgeneratelsa  " +
                    " where application2sanction_gid ='" + application2sanction_gid + "'" +
                    " order by application2sanction_gid desc ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getLSARefNoDropDown_list = new List<LSARefNoDropDown_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getLSARefNoDropDown_list.Add(new LSARefNoDropDown_list
                    {
                        generatelsa_gid = (dr_datarow["generatelsa_gid"].ToString()),
                        lsa_refno = (dr_datarow["lsa_refno"].ToString()),

                    });
                }
                values.LSARefNoDropDown_list = getLSARefNoDropDown_list;
            }
            dt_datatable.Dispose();
        }
        public void DaGetDisbFarmerIndividualSummary(string employee_gid, MdlFarmerIndividualSummary values)
        {
            try
            {
                msSQL = " select a.farmercontact_gid,concat(a.first_name, ' ', a.middle_name, ' ', a.last_name) as individual_name, " +
                        " a.pan_no,a.aadhar_no,a.designation_name,a.main_occupation,a.disbursement_amount," +
                        " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as created_by, " +
                        " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date,d.contactcoapplicant_gid " +
                        " from ocs_trn_tfarmercontact a" +
                        " left join hrm_mst_temployee b on b.employee_gid = a.created_by " +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " left join ocs_trn_tcontactcoapplicant d on d.farmercontact_gid = a.farmercontact_gid " +
                        " where a.rmdisbursementrequest_gid='" + employee_gid + "'" +
                        " or a.rmdisbursementrequest_gid='" + values.rmdisbursementrequest_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getfarmerindividualsummary_list = new List<farmerindividualsummary_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getfarmerindividualsummary_list.Add(new farmerindividualsummary_list
                        {
                            farmercontact_gid = dt["farmercontact_gid"].ToString(),
                            individual_name = dt["individual_name"].ToString(),
                            pan_no = dt["pan_no"].ToString(),
                            aadhar_no = dt["aadhar_no"].ToString(),
                            designation_name = dt["designation_name"].ToString(),
                            main_occupation = dt["main_occupation"].ToString(),
                            created_by = dt["created_by"].ToString(),
                            created_date = dt["created_date"].ToString(),
                            contactcoapplicant_gid = dt["contactcoapplicant_gid"].ToString(),
                            disbursement_amount = dt["disbursement_amount"].ToString(),
                        });
                        values.farmerindividualsummary_list = getfarmerindividualsummary_list;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = ex.ToString();
                values.status = false;
            }
        }
        public void DaGetDisbSupplierSummary(string employee_gid, MdlDisbSupplierBankAcct values)
        {
            try
            {
                msSQL = " select disbursementsupplier_gid,supplier_name,bank_name,branch_name,ifsc_code, " +
                        " bankaccount_number,accountholder_name,disbursement_amount, " +
                        " concat(f.user_firstname, ' ', f.user_lastname, ' / ', f.user_code) as created_by, " +
                        " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date " +
                        " from ocs_trn_tdisbursementsupplier a " +
                        " left join hrm_mst_temployee e on e.employee_gid = a.created_by " +
                        " left join adm_mst_tuser f on f.user_gid = e.user_gid " +
                        " where a.rmdisbursementrequest_gid='" + employee_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getdisbsupplierdtl_list = new List<disbsupplierdtl_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getdisbsupplierdtl_list.Add(new disbsupplierdtl_list
                        {
                            supplier_name = dt["supplier_name"].ToString(),
                            bank_name = dt["bank_name"].ToString(),
                            branch_name = dt["branch_name"].ToString(),
                            ifsc_code = dt["ifsc_code"].ToString(),
                            bankaccount_number = dt["bankaccount_number"].ToString(),
                            accountholder_name = dt["accountholder_name"].ToString(),
                            disbursementsupplier_gid = dt["disbursementsupplier_gid"].ToString(),
                            disbursement_amount = dt["disbursement_amount"].ToString(),
                            created_by = dt["created_by"].ToString(),
                            created_date = dt["created_date"].ToString(),
                        });
                        values.disbsupplierdtl_list = getdisbsupplierdtl_list;
                    }
                }
                dt_datatable.Dispose();
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetDisbFarmerSupplierTempClear(string employee_gid, result values)
        {
            msSQL = "delete from ocs_trn_tfarmercontact where rmdisbursementrequest_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from ocs_trn_tdisbursementsupplier where rmdisbursementrequest_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            values.status = true;
        }
        public void DaGetDisbursementCreditOpsView(string employee_gid, string rmdisbursementrequest_gid, MdlDisbursementRequestAdd values)
        {
            msSQL = " select application_gid,application2sanction_gid,sanction_refno,application2loan_gid,product_type," +
                    " processing_fees,processing_gst,finance_charges,od_amount,escrow_payment,nach_status,remarks,amounttobe_disbursed, " +
                    " date_format(loandisbursement_date, '%d-%m-%Y %h:%i %p') as loandisbursement_date, " +
                    " date_format(loandisbursement_date, '%Y-%m-%d %h:%i:%s') as editloandisbursement_date, " +
                    " lsareference_gid,lsareference_number,disbursement_to,additionalcharges_gst," +
                    " dispgstprocessing_fees,dispgstadditionfees_charges,maker_remarks,checker_remarks " +
                    " from ocs_trn_trmdisbursementrequest a" +
                    " where rmdisbursementrequest_gid='" + rmdisbursementrequest_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.application_gid = objODBCDatareader["application_gid"].ToString();
                values.application2sanction_gid = objODBCDatareader["application2sanction_gid"].ToString();
                values.sanction_refno = objODBCDatareader["sanction_refno"].ToString();
                values.application2loan_gid = objODBCDatareader["application2loan_gid"].ToString();
                values.product_type = objODBCDatareader["product_type"].ToString();
                values.processing_fees = objODBCDatareader["processing_fees"].ToString();
                values.gst = objODBCDatareader["processing_gst"].ToString();
                values.finance_charges = objODBCDatareader["finance_charges"].ToString();
                values.od_amount = objODBCDatareader["od_amount"].ToString();
                values.escrow_payment = objODBCDatareader["escrow_payment"].ToString();
                values.nach_status = objODBCDatareader["nach_status"].ToString();
                values.remarks = objODBCDatareader["remarks"].ToString();
                values.loandisbursement_date = objODBCDatareader["loandisbursement_date"].ToString();
                values.amounttobe_disbursed = objODBCDatareader["amounttobe_disbursed"].ToString();
                values.editloandisbursement_date = objODBCDatareader["editloandisbursement_date"].ToString();
                values.lsareference_gid = objODBCDatareader["lsareference_gid"].ToString();
                values.lsareference_number = objODBCDatareader["lsareference_number"].ToString();
                values.disbursement_to = objODBCDatareader["disbursement_to"].ToString();
                values.additionalcharges_gst = objODBCDatareader["additionalcharges_gst"].ToString();
                values.dispgstprocessing_fees = objODBCDatareader["dispgstprocessing_fees"].ToString();
                values.dispgstadditionfees_charges = objODBCDatareader["dispgstadditionfees_charges"].ToString();
                values.maker_remarks = objODBCDatareader["maker_remarks"].ToString();
                values.checker_remarks = objODBCDatareader["checker_remarks"].ToString();
            }
            objODBCDatareader.Close();


        }
        public void DaGetRmDisbursementDocumentDtl(string employee_gid, string rmdisbursementrequest_gid, MdlDisbursementRequest values)
        {
            try
            {
                msSQL = " select rmdisbursementdocument_gid,application_gid,document_name,document_path,document_title, " +
                          " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as created_by, " +
                          " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date from ocs_trn_trmdisbursementdocument a" +
                          " left join hrm_mst_temployee b on b.employee_gid = a.created_by " +
                          " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                          " where rmdisbursementrequest_gid='" + rmdisbursementrequest_gid + "' " +
                          " or rmdisbursementrequest_gid='" + employee_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getdisbursementuploadeddocument_list = new List<disbursementuploadeddocument_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getdisbursementuploadeddocument_list.Add(new disbursementuploadeddocument_list
                        {
                            document_name = dt["document_name"].ToString(),
                            document_title = dt["document_title"].ToString(),
                            document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                            application_gid = dt["application_gid"].ToString(),
                            rmdisbursementdocument_gid = dt["rmdisbursementdocument_gid"].ToString(),
                            created_by = dt["created_by"].ToString(),
                            created_date = dt["created_date"].ToString(),

                        });
                        values.disbursementuploadeddocument_list = getdisbursementuploadeddocument_list;
                    }
                }
                dt_datatable.Dispose();

                values.status = true;
            }
            catch (Exception ex)
            {
                values.status = false;
            }
        }
        public void DaGetDisbFarmerIndividualCreditOps(string employee_gid, string rmdisbursementrequest_gid, MdlFarmerIndividualSummary values)
        {
            try
            {
                msSQL = " select a.farmercontact_gid,concat(a.first_name, ' ', a.middle_name, ' ', a.last_name) as individual_name, " +
                        " a.pan_no,a.aadhar_no,a.designation_name,a.main_occupation,a.disbursement_amount," +
                        " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as created_by, " +
                        " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date,d.contactcoapplicant_gid, " +
                        " a.ifsc_code,a.bank_name,a.bankaccount_number,a.disbursement_amount as rmfarmerdisbursement_amount, " +
                        " a.disbursement_amount as farmerdisbursement_amount,a.creditopsdisbursement_amount," +
                        " creditopscheckerdisbursement_amount,a.encoreaccintegration_status,a.urn,a.urn_status,a.encore_accountid, " +
                        " a.disbursementbookingencore_status,a.encorefindcust_status,a.batchencorefindcust_status from ocs_trn_tfarmercontact a" +
                        " left join hrm_mst_temployee b on b.employee_gid = a.created_by " +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " left join ocs_trn_tcontactcoapplicant d on d.farmercontact_gid = a.farmercontact_gid " +
                        " where a.rmdisbursementrequest_gid='" + rmdisbursementrequest_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getfarmerindividualsummary_list = new List<farmerindividualsummary_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getfarmerindividualsummary_list.Add(new farmerindividualsummary_list
                        {
                            farmercontact_gid = dt["farmercontact_gid"].ToString(),
                            individual_name = dt["individual_name"].ToString(),
                            pan_no = dt["pan_no"].ToString(),
                            aadhar_no = dt["aadhar_no"].ToString(),
                            designation_name = dt["designation_name"].ToString(),
                            main_occupation = dt["main_occupation"].ToString(),
                            created_by = dt["created_by"].ToString(),
                            created_date = dt["created_date"].ToString(),
                            contactcoapplicant_gid = dt["contactcoapplicant_gid"].ToString(),
                            disbursement_amount = dt["disbursement_amount"].ToString(),
                            ifsc_code = dt["ifsc_code"].ToString(),
                            bank_name = dt["bank_name"].ToString(),
                            bankaccount_number = dt["bankaccount_number"].ToString(),
                            rmfarmerdisbursement_amount = dt["rmfarmerdisbursement_amount"].ToString(),
                            farmerdisbursement_amount = dt["farmerdisbursement_amount"].ToString(),
                            creditopsdisbursement_amount = dt["creditopsdisbursement_amount"].ToString(),
                            creditopscheckerdisbursement_amount = dt["creditopscheckerdisbursement_amount"].ToString(),
                            urn = dt["urn"].ToString(),
                            urn_status = dt["urn_status"].ToString(),
                            encoreaccintegration_status = dt["encoreaccintegration_status"].ToString(),
                            encore_accountid = dt["encore_accountid"].ToString(),
                            disbursementbookingencore_status = dt["disbursementbookingencore_status"].ToString(),
                            encorefindcust_status = dt["encorefindcust_status"].ToString(),                            
                        });
                        values.farmerindividualsummary_list = getfarmerindividualsummary_list;
                        values.batchencorefindcust_status = dt["batchencorefindcust_status"].ToString();
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = ex.ToString();
                values.status = false;
            }
        }
        public bool DaGetDidbScannedDocList(ScannnedDocTaggedDocumentList values, string application_gid)
        {
            msSQL = "select application_no, customer_name from ocs_trn_tcadapplication where application_gid='" + application_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                values.application_no = objODBCDataReader["application_no"].ToString();
                values.customer_name = objODBCDataReader["customer_name"].ToString();
            }
            objODBCDataReader.Close();

            List<PhysicalDocTaggedDocument> deferraltagged = new List<PhysicalDocTaggedDocument>();
            msSQL = " select  groupdocumentchecklist_gid, date_format(due_date,'%d-%m-%Y') as due_date, deferraltagdoc_gid, deferraltag_status, " +
                    " date_format(created_date, '%d-%m-%Y %h:%i %p') as taggeddate   from ocs_trn_tdeferraltagdoc " +
                    " where application_gid = '" + application_gid + "' and deferraltag_status in ('1','2','3') and fromphysical_document = 'N'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                deferraltagged = dt_datatable.AsEnumerable().Select(row => new PhysicalDocTaggedDocument
                {
                    groupdocumentchecklist_gid = row["groupdocumentchecklist_gid"].ToString(),
                    deferraltag_status = row["deferraltag_status"].ToString(),
                    taggeddate = row["taggeddate"].ToString(),
                    due_date = row["due_date"].ToString(),
                    deferraltagdoc_gid = row["deferraltagdoc_gid"].ToString(),
                }).ToList();
            }
            dt_datatable.Dispose();

            msSQL = " SELECT a.groupdocumentchecklist_gid,a.document_code, a.overall_docstatus,a.documentconfirmation_remarks, mstdocumenttype_gid as documenttype_gid,mstdocumenttype_name as documenttype_code, " +
                       " a.mstdocument_name as documenttype_name, a.tagged_by,date_format(a.extendeddue_date,'%d-%m-%Y') as extendeddue_date, " +
                       " a.mstcovenant_type,a.application_gid,a.softcopyquerystatus, " +
                       " (select count(*) from ocs_trn_tdeferralchecklist x where x.groupdocumentchecklist_gid = a.groupdocumentchecklist_gid and x.fromphysical_document='N' LIMIT 1) as checklistcount, " +
                       " (SELECT COUNT(*) FROM ocs_trn_tscanneddocument y " +
                       " WHERE y.groupdocumentchecklist_gid = a.groupdocumentchecklist_gid and signeddocument_flag = 'Y') as scanned_documentcount, " +
                       " (SELECT COUNT(*) from ocs_trn_tinitiateextendorwaiver z " +
                       " where z.groupdocumentchecklist_gid =  a.groupdocumentchecklist_gid  and z.fromphysical_document='N' " +
                       " and approval_status in ('Pending','Approved') and activity_type = 'Waiver') as waiverpendingcount, " +
                        " case  " +
                      " when(select contact_gid from ocs_trn_tcadcontact where contact_gid = a.credit_gid) is not null then 'Individual' " +
                      " when(select institution_gid from ocs_trn_tcadinstitution where institution_gid = a.credit_gid) is not null then 'Institution' " +
                      " when(select group_gid from ocs_trn_tcadgroup where group_gid = a.credit_gid) is not null then 'Group' " +
                      " else '' " +
                      " end " +
                      " as applicant_type," +
                      " case  " +
                      " when(select contact_gid from ocs_trn_tcadcontact where contact_gid = a.credit_gid and stakeholder_type = 'Applicant') is not null then 'Applicant' " +
                      " when(select contact_gid from ocs_trn_tcadcontact where contact_gid = a.credit_gid and stakeholder_type = 'Guarantor') is not null then 'Guarantor' " +
                      " when(select contact_gid from ocs_trn_tcadcontact where contact_gid = a.credit_gid and stakeholder_type = 'Member') is not null then 'Member' " +
                      " when(select institution_gid from ocs_trn_tcadinstitution where institution_gid = a.credit_gid and stakeholder_type = 'Applicant') is not null then 'Applicant' " +
                      " when(select institution_gid from ocs_trn_tcadinstitution where institution_gid = a.credit_gid and stakeholder_type = 'Guarantor') is not null then 'Guarantor' " +
                      " when(select institution_gid from ocs_trn_tcadinstitution where institution_gid = a.credit_gid and stakeholder_type = 'Member') is not null then 'Member' " +
                      " else '' " +
                      " end " +
                      " as stakeholder_type, " +
                      " case when s.company_name is null then concat(u.first_name,u.middle_name,u.last_name)   else s.company_name end as stakeholder_name " +
                       " FROM ocs_trn_tcadgroupdocumentchecklist a " +
                       " left join ocs_trn_tcadinstitution s on s.institution_gid = a.credit_gid  " +
                       " left join ocs_trn_tcadcontact u on u.contact_gid = a.credit_gid  " +
                       " WHERE a.application_gid = '" + application_gid + "' and (((a.extendeddue_date <= CURRENT_DATE())or a.extendeddue_date  is not null  or a.extendeddue_date is null) and (a.overall_docstatus not in ('Satisfied','Waived')" +
                       " or a.overall_docstatus ='' or a.overall_docstatus is null)) and a.untagged_type is null " +
                       " group by a.groupdocumentchecklist_gid order by documenttype_code asc,documenttype_name asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getScannnedDocTaggedDocument = new List<ScannnedDocTaggedDocument>();
            if (dt_datatable.Rows.Count != 0)
            {
                var i = 0;
                foreach (DataRow row in dt_datatable.Rows)
                {
                    var groupdocumentchecklist_gid = row["groupdocumentchecklist_gid"].ToString();
                    getScannnedDocTaggedDocument.Add(new ScannnedDocTaggedDocument
                    {
                        groupdocumentchecklist_gid = row["groupdocumentchecklist_gid"].ToString(),
                        documenttype_gid = row["documenttype_gid"].ToString(),
                        document_code = row["document_code"].ToString(),
                        documenttype_code = row["documenttype_code"].ToString(),
                        documenttype_name = row["documenttype_name"].ToString(),
                        covenant_type = row["mstcovenant_type"].ToString(),
                        overall_docstatus = row["overall_docstatus"].ToString(),
                        documentconfirmation_remarks = row["documentconfirmation_remarks"].ToString(),
                        extendeddue_date = row["extendeddue_date"].ToString(),
                        scanned_documentcount = row["scanned_documentcount"].ToString(),
                        waiverpendingcount = row["waiverpendingcount"].ToString(),
                        checklistcount = row["checklistcount"].ToString(),
                        taggedby = row["tagged_by"].ToString(),
                        applicant_type = row["applicant_type"].ToString(),
                        stakeholder_type = row["stakeholder_type"].ToString(),
                        stakeholder_name = row["stakeholder_name"].ToString(),
                        softcopyquerystatus = row["softcopyquerystatus"].ToString(),
                    });
                    if (deferraltagged != null)
                    {
                        var getdeferraldata = deferraltagged.Where(a => a.groupdocumentchecklist_gid == groupdocumentchecklist_gid).FirstOrDefault();
                        if (getdeferraldata != null)
                        {
                            getScannnedDocTaggedDocument[i].deferraltag_status = getdeferraldata.deferraltag_status;
                            getScannnedDocTaggedDocument[i].taggeddate = getdeferraldata.taggeddate;
                            getScannnedDocTaggedDocument[i].due_date = getdeferraldata.due_date;
                            getScannnedDocTaggedDocument[i].deferraltagdoc_gid = getdeferraldata.deferraltagdoc_gid;
                        }
                    }
                    i++;
                }
                values.ScannnedDocTaggedDocument = getScannnedDocTaggedDocument;
            }
            dt_datatable.Dispose();

            msSQL = " SELECT a.groupcovdocumentchecklist_gid,a.document_code, a.overall_docstatus,a.documentconfirmation_remarks, mstdocumenttype_gid as documenttype_gid,mstdocumenttype_name as documenttype_code, " +
                       " a.mstdocument_name as documenttype_name, a.tagged_by,date_format(a.extendeddue_date,'%d-%m-%Y') as extendeddue_date, " +
                       " a.mstcovenant_type,a.application_gid,a.softcopyquerystatus, " +
                       " (select count(*) from ocs_trn_tdeferralchecklist x where x.groupdocumentchecklist_gid = a.groupcovdocumentchecklist_gid and x.fromphysical_document='N' ) as checklistcount, " +
                       " (SELECT COUNT(*) FROM ocs_trn_tscanneddocument y " +
                       " WHERE y.groupdocumentchecklist_gid = a.groupcovdocumentchecklist_gid and signeddocument_flag = 'Y') as scanned_documentcount, " +
                       " (select covenant_periods from ocs_trn_tcadcovanantdocumentcheckdtls e " +
                       " where e.groupcovdocumentchecklist_gid = a.groupcovdocumentchecklist_gid  group by a.groupcovdocumentchecklist_gid ) as covenant_periods, " +
                       " (SELECT COUNT(*) from ocs_trn_tinitiateextendorwaiver z " +
                       " where z.groupdocumentchecklist_gid =  a.groupcovdocumentchecklist_gid and z.fromphysical_document='N' and approval_status = 'Pending' and activity_type = 'Waiver') as waiverpendingcount, " +
                      " case  " +
                      " when(select contact_gid from ocs_trn_tcadcontact where contact_gid = a.credit_gid) is not null then 'Individual' " +
                      " when(select institution_gid from ocs_trn_tcadinstitution where institution_gid = a.credit_gid) is not null then 'Institution' " +
                      " when(select group_gid from ocs_trn_tcadgroup where group_gid = a.credit_gid) is not null then 'Group' " +
                      " else '' " +
                      " end " +
                      " as applicant_type," +
                      " case  " +
                      " when(select contact_gid from ocs_trn_tcadcontact where contact_gid = a.credit_gid and stakeholder_type = 'Applicant') is not null then 'Applicant' " +
                      " when(select contact_gid from ocs_trn_tcadcontact where contact_gid = a.credit_gid and stakeholder_type = 'Guarantor') is not null then 'Guarantor' " +
                      " when(select contact_gid from ocs_trn_tcadcontact where contact_gid = a.credit_gid and stakeholder_type = 'Member') is not null then 'Member' " +
                      " when(select institution_gid from ocs_trn_tcadinstitution where institution_gid = a.credit_gid and stakeholder_type = 'Applicant') is not null then 'Applicant' " +
                      " when(select institution_gid from ocs_trn_tcadinstitution where institution_gid = a.credit_gid and stakeholder_type = 'Guarantor') is not null then 'Guarantor' " +
                      " when(select institution_gid from ocs_trn_tcadinstitution where institution_gid = a.credit_gid and stakeholder_type = 'Member') is not null then 'Member' " +
                      " else '' " +
                      " end " +
                      " as stakeholder_type ," +
                       " case when s.company_name is null then concat(u.first_name,u.middle_name,u.last_name)  else s.company_name end as stakeholder_name " +
                       " FROM ocs_trn_tcadgroupcovenantdocumentchecklist a " +
                       " left join ocs_trn_tcadinstitution s on s.institution_gid = a.credit_gid  " +
                       " left join ocs_trn_tcadcontact u on u.contact_gid = a.credit_gid  " +
                       " LEFT JOIN ocs_trn_tdeferraltagdoc b on a.groupcovdocumentchecklist_gid = b.groupdocumentchecklist_gid " +
                       " WHERE a.application_gid = '" + application_gid + "' and (((a.extendeddue_date <= CURRENT_DATE())or a.extendeddue_date  is not null  or a.extendeddue_date is null) and (a.overall_docstatus not in ('Satisfied','Waived') " +
                       "  or a.overall_docstatus ='' or a.overall_docstatus is null)) and a.untagged_type is null " +
                       " group by a.groupcovdocumentchecklist_gid order by documenttype_code asc,documenttype_name asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getScannnedCovenantDocTaggedDocument = new List<ScannnedCovenantDocTaggedDocument>();
            if (dt_datatable.Rows.Count != 0)
            {
                var i = 0;
                foreach (DataRow row in dt_datatable.Rows)
                {
                    var groupdocumentchecklist_gid = row["groupcovdocumentchecklist_gid"].ToString();
                    getScannnedCovenantDocTaggedDocument.Add(new ScannnedCovenantDocTaggedDocument
                    {
                        groupdocumentchecklist_gid = row["groupcovdocumentchecklist_gid"].ToString(),
                        documenttype_gid = row["documenttype_gid"].ToString(),
                        documenttype_code = row["documenttype_code"].ToString(),
                        documenttype_name = row["documenttype_name"].ToString(),
                        covenant_type = row["mstcovenant_type"].ToString(),
                        overall_docstatus = row["overall_docstatus"].ToString(),
                        extendeddue_date = row["extendeddue_date"].ToString(),
                        document_code = row["document_code"].ToString(),
                        scanned_documentcount = row["scanned_documentcount"].ToString(),
                        covenant_periods = row["covenant_periods"].ToString(),
                        waiverpendingcount = row["waiverpendingcount"].ToString(),
                        checklistcount = row["checklistcount"].ToString(),
                        applicant_type = row["applicant_type"].ToString(),
                        stakeholder_type = row["stakeholder_type"].ToString(),
                        stakeholder_name = row["stakeholder_name"].ToString(),
                        softcopyquerystatus = row["softcopyquerystatus"].ToString(),
                    });
                    if (deferraltagged != null)
                    {
                        var getdeferraldata = deferraltagged.Where(a => a.groupdocumentchecklist_gid == groupdocumentchecklist_gid).FirstOrDefault();
                        if (getdeferraldata != null)
                        {
                            getScannnedCovenantDocTaggedDocument[i].deferraltag_status = getdeferraldata.deferraltag_status;
                            getScannnedCovenantDocTaggedDocument[i].taggeddate = getdeferraldata.taggeddate;
                            getScannnedCovenantDocTaggedDocument[i].due_date = getdeferraldata.due_date;
                            getScannnedCovenantDocTaggedDocument[i].deferraltagdoc_gid = getdeferraldata.deferraltagdoc_gid;
                        }
                    }
                    i++;
                }
                values.ScannnedCovenantDocTaggedDocument = getScannnedCovenantDocTaggedDocument;
            }
            dt_datatable.Dispose();
            return true;
        }
        public bool DaGetDidbPhysicalDocList(PhysicalDocTaggedDocumentList values, string application_gid)
        {
            msSQL = "select application_no, customer_name from ocs_trn_tcadapplication where application_gid='" + application_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                values.application_no = objODBCDataReader["application_no"].ToString();
                values.customer_name = objODBCDataReader["customer_name"].ToString();
            }
            objODBCDataReader.Close();
            List<PhysicalDocTaggedDocument> deferraltagged = new List<PhysicalDocTaggedDocument>();
            msSQL = " select  groupdocumentchecklist_gid, date_format(due_date,'%d-%m-%Y') as due_date, deferraltagdoc_gid, deferraltag_status, " +
                    " date_format(created_date, '%d-%m-%Y %h:%i %p') as taggeddate   from ocs_trn_tdeferraltagdoc " +
                    " where application_gid = '" + application_gid + "' and deferraltag_status in ('1','2') and fromphysical_document = 'Y'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                deferraltagged = dt_datatable.AsEnumerable().Select(row => new PhysicalDocTaggedDocument
                {
                    groupdocumentchecklist_gid = row["groupdocumentchecklist_gid"].ToString(),
                    deferraltag_status = row["deferraltag_status"].ToString(),
                    taggeddate = row["taggeddate"].ToString(),
                    due_date = row["due_date"].ToString(),
                    deferraltagdoc_gid = row["deferraltagdoc_gid"].ToString(),
                }).ToList();
            }
            dt_datatable.Dispose();

            msSQL = " SELECT a.groupdocumentchecklist_gid,a.document_code,a.overall_docstatus,a.physicaloverall_docstatus, mstdocumenttype_gid as documenttype_gid,mstdocumenttype_name as documenttype_code, " +
                       " a.mstdocument_name as documenttype_name, a.tagged_by,date_format(a.physical_extendedduedate,'%d-%m-%Y') as extendeddue_date, " +
                       " a.mstcovenant_type,a.application_gid, a.physicalcopyquerystatus," +
                       " (select count(*) from ocs_trn_tdeferralchecklist x where x.groupdocumentchecklist_gid = a.groupdocumentchecklist_gid and x.fromphysical_document='Y' LIMIT 1) as checklistcount, " +
                       " (SELECT COUNT(*) FROM ocs_trn_tphysicaldocument y " +
                       " WHERE y.groupdocumentchecklist_gid = a.groupdocumentchecklist_gid) as physical_documentcount, " +
                       " (SELECT COUNT(*) from ocs_trn_tinitiateextendorwaiver z " +
                       " where z.groupdocumentchecklist_gid =  a.groupdocumentchecklist_gid and z.fromphysical_document='Y' and approval_status in ('Pending','Approved') and activity_type = 'Waiver') as waiverpendingcount, " +
                        " case  " +
                      " when(select contact_gid from ocs_trn_tcadcontact where contact_gid = a.credit_gid) is not null then 'Individual' " +
                      " when(select institution_gid from ocs_trn_tcadinstitution where institution_gid = a.credit_gid) is not null then 'Institution' " +
                      " when(select group_gid from ocs_trn_tcadgroup where group_gid = a.credit_gid) is not null then 'Group' " +
                      " else '' " +
                      " end " +
                      " as applicant_type," +
                      " case  " +
                      " when(select contact_gid from ocs_trn_tcadcontact where contact_gid = a.credit_gid and stakeholder_type = 'Applicant') is not null then 'Applicant' " +
                      " when(select contact_gid from ocs_trn_tcadcontact where contact_gid = a.credit_gid and stakeholder_type = 'Guarantor') is not null then 'Guarantor' " +
                      " when(select contact_gid from ocs_trn_tcadcontact where contact_gid = a.credit_gid and stakeholder_type = 'Member') is not null then 'Member' " +
                      " when(select institution_gid from ocs_trn_tcadinstitution where institution_gid = a.credit_gid and stakeholder_type = 'Applicant') is not null then 'Applicant' " +
                      " when(select institution_gid from ocs_trn_tcadinstitution where institution_gid = a.credit_gid and stakeholder_type = 'Guarantor') is not null then 'Guarantor' " +
                      " when(select institution_gid from ocs_trn_tcadinstitution where institution_gid = a.credit_gid and stakeholder_type = 'Member') is not null then 'Member' " +
                      " else '' " +
                      " end " +
                      " as stakeholder_type, " +
                       " case when s.company_name is null then concat(u.first_name,u.middle_name,u.last_name)   else s.company_name end as stakeholder_name " +
                       " FROM ocs_trn_tcadgroupdocumentchecklist a " +
                       " left join ocs_trn_tcadinstitution s on s.institution_gid = a.credit_gid  " +
                       " left join ocs_trn_tcadcontact u on u.contact_gid = a.credit_gid  " +
                       " WHERE a.application_gid = '" + application_gid + "' and (((a.extendeddue_date <= CURRENT_DATE())or a.extendeddue_date  is not null  or a.extendeddue_date is null) and (a.overall_docstatus not in ('Satisfied','Waived')" +
                       " or a.overall_docstatus ='' or a.overall_docstatus is null)) and a.untagged_type is null " +
                       " group by a.groupdocumentchecklist_gid order by documenttype_code asc,documenttype_name asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);

            if (dt_datatable.Rows.Count != 0)
            {
                var getPhysicalDocTaggedDocument = new List<PhysicalDocTaggedDocument>();
                var i = 0;
                foreach (DataRow row in dt_datatable.Rows)
                {
                    var groupdocumentchecklist_gid = row["groupdocumentchecklist_gid"].ToString();
                    getPhysicalDocTaggedDocument.Add(new PhysicalDocTaggedDocument
                    {
                        groupdocumentchecklist_gid = row["groupdocumentchecklist_gid"].ToString(),
                        documenttype_gid = row["documenttype_gid"].ToString(),
                        documenttype_code = row["documenttype_code"].ToString(),
                        document_code = row["document_code"].ToString(),
                        documenttype_name = row["documenttype_name"].ToString(),
                        covenant_type = row["mstcovenant_type"].ToString(),
                        overall_docstatus = row["overall_docstatus"].ToString(),
                        physical_documentcount = row["physical_documentcount"].ToString(),
                        waiverpendingcount = row["waiverpendingcount"].ToString(),
                        checklistcount = row["checklistcount"].ToString(),
                        physicaloverall_docstatus = row["physicaloverall_docstatus"].ToString(),
                        extendeddue_date = row["extendeddue_date"].ToString(),
                        taggedby = row["tagged_by"].ToString(),
                        applicant_type = row["applicant_type"].ToString(),
                        stakeholder_type = row["stakeholder_type"].ToString(),
                        physicalcopyquerystatus = row["physicalcopyquerystatus"].ToString(),
                    });
                    if (deferraltagged != null)
                    {
                        var getdeferraldata = deferraltagged.Where(a => a.groupdocumentchecklist_gid == groupdocumentchecklist_gid).FirstOrDefault();
                        if (getdeferraldata != null)
                        {
                            getPhysicalDocTaggedDocument[i].deferraltag_status = getdeferraldata.deferraltag_status;
                            getPhysicalDocTaggedDocument[i].taggeddate = getdeferraldata.taggeddate;
                            getPhysicalDocTaggedDocument[i].due_date = getdeferraldata.due_date;
                            getPhysicalDocTaggedDocument[i].deferraltagdoc_gid = getdeferraldata.deferraltagdoc_gid;
                        }
                    }
                    i++;
                }

                values.PhysicalDocTaggedDocument = getPhysicalDocTaggedDocument;
            }
            dt_datatable.Dispose();

            msSQL = " SELECT a.groupcovdocumentchecklist_gid,a.document_code, a.overall_docstatus ,a.physicaloverall_docstatus, mstdocumenttype_gid as documenttype_gid,mstdocumenttype_name as documenttype_code, " +
                       " a.mstdocument_name as documenttype_name, a.tagged_by,date_format(a.physical_extendedduedate,'%d-%m-%Y') as extendeddue_date, " +
                       " a.mstcovenant_type,a.application_gid,a.physicalcopyquerystatus, " +
                       " (select count(*) from ocs_trn_tdeferralchecklist x where x.groupdocumentchecklist_gid = a.groupcovdocumentchecklist_gid and x.fromphysical_document='Y' ) as checklistcount, " +
                       " (SELECT COUNT(*) FROM ocs_trn_tphysicaldocument y " +
                       " WHERE y.groupdocumentchecklist_gid = a.groupcovdocumentchecklist_gid) as physical_documentcount, " +
                       " CASE  WHEN physical_covenant_periods is null THEN  (select covenant_periods from ocs_trn_tcadcovanantdocumentcheckdtls e " +
                       " where e.groupcovdocumentchecklist_gid = a.groupcovdocumentchecklist_gid  group by a.groupcovdocumentchecklist_gid ) " +
                       " ELSE physical_covenant_periods END as 'covenant_periods', " +
                       " (SELECT COUNT(*) from ocs_trn_tinitiateextendorwaiver z " +
                       " where z.groupdocumentchecklist_gid =  a.groupcovdocumentchecklist_gid and z.fromphysical_document='Y' and approval_status = 'Pending' and activity_type = 'Waiver') as waiverpendingcount, " +
                        " case  " +
                      " when(select contact_gid from ocs_trn_tcadcontact where contact_gid = a.credit_gid) is not null then 'Individual' " +
                      " when(select institution_gid from ocs_trn_tcadinstitution where institution_gid = a.credit_gid) is not null then 'Institution' " +
                      " when(select group_gid from ocs_trn_tcadgroup where group_gid = a.credit_gid) is not null then 'Group' " +
                      " else '' " +
                      " end " +
                      " as applicant_type," +
                      " case  " +
                      " when(select contact_gid from ocs_trn_tcadcontact where contact_gid = a.credit_gid and stakeholder_type = 'Applicant') is not null then 'Applicant' " +
                      " when(select contact_gid from ocs_trn_tcadcontact where contact_gid = a.credit_gid and stakeholder_type = 'Guarantor') is not null then 'Guarantor' " +
                      " when(select contact_gid from ocs_trn_tcadcontact where contact_gid = a.credit_gid and stakeholder_type = 'Member') is not null then 'Member' " +
                      " when(select institution_gid from ocs_trn_tcadinstitution where institution_gid = a.credit_gid and stakeholder_type = 'Applicant') is not null then 'Applicant' " +
                      " when(select institution_gid from ocs_trn_tcadinstitution where institution_gid = a.credit_gid and stakeholder_type = 'Guarantor') is not null then 'Guarantor' " +
                      " when(select institution_gid from ocs_trn_tcadinstitution where institution_gid = a.credit_gid and stakeholder_type = 'Member') is not null then 'Member' " +
                      " else '' " +
                      " end " +
                      " as stakeholder_type ," +
                       " case when s.company_name is null then concat(u.first_name,u.middle_name,u.last_name)   else s.company_name end as stakeholder_name " +
                       " FROM ocs_trn_tcadgroupcovenantdocumentchecklist a " +
                       " left join ocs_trn_tcadinstitution s on s.institution_gid = a.credit_gid  " +
                       " left join ocs_trn_tcadcontact u on u.contact_gid = a.credit_gid  " +
                       " WHERE a.application_gid = '" + application_gid + "' and (((a.extendeddue_date <= CURRENT_DATE())or a.extendeddue_date  is not null  or a.extendeddue_date is null) " +
                       " and (a.overall_docstatus not in ('Satisfied','Waived') or a.overall_docstatus ='' or a.overall_docstatus is null)) and a.untagged_type is null " +
                       " group by a.groupcovdocumentchecklist_gid order by documenttype_code asc,documenttype_name asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);

            if (dt_datatable.Rows.Count != 0)
            {
                var getPhysicalCovenantDocTaggedDocument = new List<PhysicalCovenantDocTaggedDocument>();
                var i = 0;
                foreach (DataRow row in dt_datatable.Rows)
                {
                    var groupdocumentchecklist_gid = row["groupcovdocumentchecklist_gid"].ToString();
                    getPhysicalCovenantDocTaggedDocument.Add(new PhysicalCovenantDocTaggedDocument
                    {
                        groupdocumentchecklist_gid = row["groupcovdocumentchecklist_gid"].ToString(),
                        documenttype_gid = row["documenttype_gid"].ToString(),
                        documenttype_code = row["documenttype_code"].ToString(),
                        document_code = row["document_code"].ToString(),
                        documenttype_name = row["documenttype_name"].ToString(),
                        covenant_type = row["mstcovenant_type"].ToString(),
                        overall_docstatus = row["overall_docstatus"].ToString(),
                        extendeddue_date = row["extendeddue_date"].ToString(),
                        physical_documentcount = row["physical_documentcount"].ToString(),
                        covenant_periods = row["covenant_periods"].ToString(),
                        waiverpendingcount = row["waiverpendingcount"].ToString(),
                        checklistcount = row["checklistcount"].ToString(),
                        physicaloverall_docstatus = row["physicaloverall_docstatus"].ToString(),
                        taggedby = row["tagged_by"].ToString(),
                        applicant_type = row["applicant_type"].ToString(),
                        stakeholder_type = row["stakeholder_type"].ToString(),
                        physicalcopyquerystatus = row["physicalcopyquerystatus"].ToString(),
                    });
                    if (deferraltagged != null)
                    {
                        var getdeferraldata = deferraltagged.Where(a => a.groupdocumentchecklist_gid == groupdocumentchecklist_gid).FirstOrDefault();
                        if (getdeferraldata != null)
                        {
                            getPhysicalCovenantDocTaggedDocument[i].deferraltag_status = getdeferraldata.deferraltag_status;
                            getPhysicalCovenantDocTaggedDocument[i].taggeddate = getdeferraldata.taggeddate;
                            getPhysicalCovenantDocTaggedDocument[i].due_date = getdeferraldata.due_date;
                            getPhysicalCovenantDocTaggedDocument[i].deferraltagdoc_gid = getdeferraldata.deferraltagdoc_gid;
                        }
                    }
                    i++;
                }
                values.PhysicalCovenantDocTaggedDocument = getPhysicalCovenantDocTaggedDocument;
            }
            dt_datatable.Dispose();
            return true;
        }
        public void DaCreditOpsSupplierDisbAmountUpdate(string employee_gid, MdlDisbSupplierBankAcct values)
        {


            if (string.IsNullOrEmpty(values.creditopsdisbursement_amount))
            {
                values.status = false;
                values.message = "Kindly Add Maker Disbursement Amount";

                return;

            }


            msSQL = "select sanction_amount from ocs_trn_tapplication2sanction where  application_gid='" + values.application_gid + "'";
            string lssanction_amount = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " select disbursement_amount as sumdisbursement_amount from ocs_trn_tdisbursementamount  where application_gid='" + values.application_gid + "' and " +
                   " (rmdisbursementrequest_gid !='" + employee_gid + "' and " +
                   " rmdisbursementrequest_gid !='" + values.rmdisbursementrequest_gid + "')" +
                   " and rmdisbursementrequest_gid not in (select rmdisbursementrequest_gid from ocs_trn_tdisbursementassignment where approval_status ='Rejected') " +
                   " and " +
                   " ((makerdisbursement_amount is null ||makerdisbursement_amount ='') && " +
                   " (checkerdisbursement_amount is null ||checkerdisbursement_amount ='')) " +
                   " union all " +
                   " select makerdisbursement_amount  as sumdisbursement_amount from ocs_trn_tdisbursementamount  where application_gid='" + values.application_gid + "' and " +
                   " (rmdisbursementrequest_gid !='" + employee_gid + "' and " +
                   " rmdisbursementrequest_gid !='" + values.rmdisbursementrequest_gid + "')" +
                   " and rmdisbursementrequest_gid not in (select rmdisbursementrequest_gid from ocs_trn_tdisbursementassignment where approval_status ='Rejected') " +
                   " and " +
                   " ((makerdisbursement_amount is not null ||makerdisbursement_amount !='') && " +
                   " (checkerdisbursement_amount is  null ||checkerdisbursement_amount ='')) " +
                   " union all " +
                   " select checkerdisbursement_amount as sumdisbursement_amount from ocs_trn_tdisbursementamount  where application_gid='" + values.application_gid + "' and " +
                   " (rmdisbursementrequest_gid !='" + employee_gid + "' and " +
                   " rmdisbursementrequest_gid  !='" + values.rmdisbursementrequest_gid + "')" +
                   " and rmdisbursementrequest_gid not in (select rmdisbursementrequest_gid from ocs_trn_tdisbursementassignment where approval_status ='Rejected') " +
                   " and " +
                   " ((checkerdisbursement_amount is not null ||checkerdisbursement_amount !=''))" +
                    " union all" +
                    " select case when (creditopscheckerdisbursement_amount is null and  creditopsdisbursement_amount is null) then disbursement_amount  " +
                    " when (creditopscheckerdisbursement_amount is null ) then creditopsdisbursement_amount  " +
                    " else creditopscheckerdisbursement_amount end as sumdisbursement_amount " +
                    " from ocs_trn_tdisbursementsupplier  where application_gid='" + values.application_gid + "' and " +
                    " disbursementsupplier_gid !='" + values.disbursementsupplier_gid + "' " +
                    " and rmdisbursementrequest_gid not in (select rmdisbursementrequest_gid from ocs_trn_tdisbursementassignment where approval_status ='Rejected') " +
                    " union all" +
                    " select case when (creditopscheckerdisbursement_amount is null and  creditopsdisbursement_amount is null) then disbursement_amount  " +
                    " when (creditopscheckerdisbursement_amount is null ) then creditopsdisbursement_amount  " +
                    " else creditopscheckerdisbursement_amount end as sumdisbursement_amount " +
                    " from ocs_trn_tfarmercontact  where application_gid='" + values.application_gid + "' and " +
                    " farmercontact_gid !='" + values.farmercontact_gid + "' " +
                    " and rmdisbursementrequest_gid not in (select rmdisbursementrequest_gid from ocs_trn_tdisbursementassignment where approval_status ='Rejected') ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdisbursementamount_list = new List<disbursementamount_list>();
            double disbursementamount_total = 0;
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {

                    disbursementamount_total = disbursementamount_total + double.Parse(dr_datarow["sumdisbursement_amount"].ToString().Replace(",", ""));

                }

            }
            dt_datatable.Dispose();


            if (string.IsNullOrEmpty(values.creditopsdisbursement_amount))
            {

            }
            else
            {
                disbursementamount_total = disbursementamount_total + double.Parse(values.creditopsdisbursement_amount.Replace(",", ""));
            }

            if (disbursementamount_total <= double.Parse(lssanction_amount))
            {

            }
            else
            {
                values.status = false;
                values.message = "Maker Disbursement Amount is Greater than Sanction Amount";

                return;
            }


            msSQL = " update ocs_trn_tdisbursementsupplier set" +
                    " creditopsdisbursement_amount='" + values.creditopsdisbursement_amount + "'" +
                    " where disbursementsupplier_gid='" + values.disbursementsupplier_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                values.status = true;
                values.message = "Disbursement Amount Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
            }
        }

        public void DaGetCreditOpsSupplierDisbAmountView(string employee_gid, string disbursementsupplier_gid, MdlDisbSupplierBankAcct values)
        {
            msSQL = " select disbursementsupplier_gid,creditopsdisbursement_amount " +
                    " from ocs_trn_tdisbursementsupplier a" +
                    " where disbursementsupplier_gid='" + disbursementsupplier_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.disbursementsupplier_gid = objODBCDatareader["disbursementsupplier_gid"].ToString();
                values.creditopsdisbursement_amount = objODBCDatareader["creditopsdisbursement_amount"].ToString();
            }
            objODBCDatareader.Close();
        }
        public void DaCreditOpsFarmerDisbAmountUpdate(string employee_gid, MdlFarmerIndividualDtlView values)
        {
            if (string.IsNullOrEmpty(values.creditopsdisbursement_amount))
            {
                values.status = false;
                values.message = "Kindly Add Maker Disbursement Amount";

                return;

            }

            msSQL = "select sanction_amount from ocs_trn_tapplication2sanction where  application_gid='" + values.application_gid + "'";
            string lssanction_amount = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " select disbursement_amount as sumdisbursement_amount from ocs_trn_tdisbursementamount  where application_gid='" + values.application_gid + "' and " +
                   " (rmdisbursementrequest_gid !='" + employee_gid + "' and " +
                   " rmdisbursementrequest_gid !='" + values.rmdisbursementrequest_gid + "')" +
                   " and rmdisbursementrequest_gid not in (select rmdisbursementrequest_gid from ocs_trn_tdisbursementassignment where approval_status ='Rejected') " +
                   " and " +
                   " ((makerdisbursement_amount is null ||makerdisbursement_amount ='') && " +
                   " (checkerdisbursement_amount is null ||checkerdisbursement_amount ='')) " +
                   " union all " +
                   " select makerdisbursement_amount  as sumdisbursement_amount from ocs_trn_tdisbursementamount  where application_gid='" + values.application_gid + "' and " +
                   " (rmdisbursementrequest_gid !='" + employee_gid + "' and " +
                   " rmdisbursementrequest_gid !='" + values.rmdisbursementrequest_gid + "')" +
                   " and rmdisbursementrequest_gid not in (select rmdisbursementrequest_gid from ocs_trn_tdisbursementassignment where approval_status ='Rejected') " +
                   " and " +
                   " ((makerdisbursement_amount is not null ||makerdisbursement_amount !='') && " +
                   " (checkerdisbursement_amount is  null ||checkerdisbursement_amount ='')) " +
                   " union all " +
                   " select checkerdisbursement_amount as sumdisbursement_amount from ocs_trn_tdisbursementamount  where application_gid='" + values.application_gid + "' and " +
                   " (rmdisbursementrequest_gid !='" + employee_gid + "' and " +
                   " rmdisbursementrequest_gid  !='" + values.rmdisbursementrequest_gid + "')" +
                   " and rmdisbursementrequest_gid not in (select rmdisbursementrequest_gid from ocs_trn_tdisbursementassignment where approval_status ='Rejected') " +
                   " and " +
                   " ((checkerdisbursement_amount is not null ||checkerdisbursement_amount !=''))" +
                    " union all" +
                    " select case when (creditopscheckerdisbursement_amount is null and  creditopsdisbursement_amount is null) then disbursement_amount  " +
                    " when (creditopscheckerdisbursement_amount is null ) then creditopsdisbursement_amount  " +
                    " else creditopscheckerdisbursement_amount end as sumdisbursement_amount " +
                    " from ocs_trn_tdisbursementsupplier  where application_gid='" + values.application_gid + "' and " +
                    " disbursementsupplier_gid !='" + values.disbursementsupplier_gid + "' " +
                    " and rmdisbursementrequest_gid not in (select rmdisbursementrequest_gid from ocs_trn_tdisbursementassignment where approval_status ='Rejected') " +
                    " union all" +
                    " select case when (creditopscheckerdisbursement_amount is null and  creditopsdisbursement_amount is null) then disbursement_amount  " +
                    " when (creditopscheckerdisbursement_amount is null ) then creditopsdisbursement_amount  " +
                    " else creditopscheckerdisbursement_amount end as sumdisbursement_amount " +
                    " from ocs_trn_tfarmercontact  where application_gid='" + values.application_gid + "' and " +
                    " farmercontact_gid !='" + values.farmercontact_gid + "' " +
                    " and rmdisbursementrequest_gid not in (select rmdisbursementrequest_gid from ocs_trn_tdisbursementassignment where approval_status ='Rejected') ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdisbursementamount_list = new List<disbursementamount_list>();
            double disbursementamount_total = 0;
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {

                    disbursementamount_total = disbursementamount_total + double.Parse(dr_datarow["sumdisbursement_amount"].ToString().Replace(",", ""));

                }

            }
            dt_datatable.Dispose();


            if (string.IsNullOrEmpty(values.creditopsdisbursement_amount))
            {

            }
            else
            {
                disbursementamount_total = disbursementamount_total + double.Parse(values.creditopsdisbursement_amount.Replace(",", ""));
            }

            if (disbursementamount_total <= double.Parse(lssanction_amount))
            {

            }
            else
            {
                values.status = false;
                values.message = "Maker Disbursement Amount is Greater than Sanction Amount";

                return;
            }
            msSQL = " update ocs_trn_tfarmercontact set" +
                    " creditopsdisbursement_amount='" + values.creditopsdisbursement_amount + "'" +
                    " where farmercontact_gid='" + values.farmercontact_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                values.status = true;
                values.message = "Disbursement Amount Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
            }
        }
        public void DaGetCreditOpsFarmerDisbAmountView(string employee_gid, string farmercontact_gid, MdlFarmerIndividualDtlView values)
        {
            msSQL = " select farmercontact_gid,creditopsdisbursement_amount " +
                    " from ocs_trn_tfarmercontact a" +
                    " where farmercontact_gid='" + farmercontact_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.farmercontact_gid = objODBCDatareader["farmercontact_gid"].ToString();
                values.creditopsdisbursement_amount = objODBCDatareader["creditopsdisbursement_amount"].ToString();
            }
            objODBCDatareader.Close();
        }

        public void DaPostDisbursementRequestReject(string employee_gid, MdlDisbursementReject values)
        {

            msSQL = " update ocs_trn_trmdisbursementrequest set " +
                    " updated_person = '" + values.updated_person + "'," +
                    " disbursementassign_status = 'Rejected' ," +
                    " updated_by = '" + employee_gid + "'," +
                    " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    " where rmdisbursementrequest_gid ='" + values.rmdisbursementrequest_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " update ocs_trn_tdisbursementassignment set" +
                    " rejected_by='" + employee_gid + "'," +
                    " rejected_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                    " rejected_remarks='" + values.rejected_remarks + "'," +
                    " approval_status='" + values.approval_status + "'" +
                    " where rmdisbursementrequest_gid='" + values.rmdisbursementrequest_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                values.status = true;
                values.message = "Disbursement Rejected Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
            }
        }
        public void DaGetDisbursementRejectedSummary(string employee_gid, MdlDisbursementRequest values)
        {
            try
            {
                msSQL = " select a.application_gid,e.application_no,e.customer_name as customer_name,e.customer_urn,a.sanction_refno, " +
                        " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date, concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as created_by, " +
                        " date_format(d.rejected_date, '%d-%m-%Y %h:%i %p') as rejected_date, concat(k.user_firstname, ' ', k.user_lastname, ' / ', k.user_code) as rejected_by, " +
                        " concat(g.user_firstname, ' ', g.user_lastname, ' / ', g.user_code) as checker_name, " +
                        " concat(i.user_firstname, ' ', i.user_lastname, ' / ', i.user_code) as maker_name, " +
                        " a.application2sanction_gid, a.application2loan_gid,e.vertical_name,e.region, " +
                        " a.rmdisbursementrequest_gid, a.disbursementrequest_code,a.sanction_refno,a.lsareference_number," +
                        " a.product_type,a.disbursement_to " +
                        " from ocs_trn_trmdisbursementrequest a " +
                        " left join ocs_trn_tcadapplication e on e.application_gid = a.application_gid" +
                        " left join hrm_mst_temployee b on b.employee_gid = a.created_by " +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " left join ocs_trn_tdisbursementassignment d on d.rmdisbursementrequest_gid = a.rmdisbursementrequest_gid " +
                        " left join hrm_mst_temployee f on f.employee_gid = d.creditopschecker_gid " +
                        " left join adm_mst_tuser g on g.user_gid = f.user_gid " +
                        " left join hrm_mst_temployee h on h.employee_gid = d.creditopsmaker_gid " +
                        " left join adm_mst_tuser i on i.user_gid = h.user_gid " +
                          " left join hrm_mst_temployee j on j.employee_gid = d.rejected_by " +
                        " left join adm_mst_tuser k on k.user_gid = j.user_gid " +
                        " where d.disbursementassign_status = 'Assigned' and d.approval_status = 'Rejected'" +
                        " order by a.created_date desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getdisbursementrejected_list = new List<disbursementrejected_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getdisbursementrejected_list.Add(new disbursementrejected_list
                        {
                            application_no = dt["application_no"].ToString(),
                            customer_name = dt["customer_name"].ToString(),
                            application_gid = dt["application_gid"].ToString(),
                            application2sanction_gid = dt["application2sanction_gid"].ToString(),
                            application2loan_gid = dt["application2loan_gid"].ToString(),
                            sanction_refno = dt["sanction_refno"].ToString(),
                            created_by = dt["created_by"].ToString(),
                            created_date = dt["created_date"].ToString(),
                            vertical_name = dt["vertical_name"].ToString(),
                            region = dt["region"].ToString(),
                            maker_name = dt["maker_name"].ToString(),
                            checker_name = dt["checker_name"].ToString(),
                            rmdisbursementrequest_gid = dt["rmdisbursementrequest_gid"].ToString(),
                            disbursementrequest_code = dt["disbursementrequest_code"].ToString(),
                            lsareference_number = dt["lsareference_number"].ToString(),
                            product_type = dt["product_type"].ToString(),
                            disbursement_to = dt["disbursement_to"].ToString(),
                            rejected_by = dt["rejected_by"].ToString(),
                            rejected_date = dt["rejected_date"].ToString()
                        });

                    }
                }
                values.disbursementrejected_list = getdisbursementrejected_list;
                dt_datatable.Dispose();

                values.status = true;
            }
            catch (Exception ex)
            {
                values.status = false;
            }
        }
        public void DaCreditOpsCheckerFarmerDisbAmountUpdate(string employee_gid, MdlFarmerIndividualDtlView values)
        {

            if (string.IsNullOrEmpty(values.creditopscheckerdisbursement_amount))
            {
                values.status = false;
                values.message = "Kindly Add Checker Disbursement Amount";

                return;

            }


            msSQL = "select sanction_amount from ocs_trn_tapplication2sanction where  application_gid='" + values.application_gid + "'";
            string lssanction_amount = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " select disbursement_amount as sumdisbursement_amount from ocs_trn_tdisbursementamount  where application_gid='" + values.application_gid + "' and " +
                   " (rmdisbursementrequest_gid !='" + employee_gid + "' and " +
                   " rmdisbursementrequest_gid !='" + values.rmdisbursementrequest_gid + "')" +
                   " and rmdisbursementrequest_gid not in (select rmdisbursementrequest_gid from ocs_trn_tdisbursementassignment where approval_status ='Rejected') " +
                   " and " +
                   " ((makerdisbursement_amount is null ||makerdisbursement_amount ='') && " +
                   " (checkerdisbursement_amount is null ||checkerdisbursement_amount ='')) " +
                   " union all " +
                   " select makerdisbursement_amount  as sumdisbursement_amount from ocs_trn_tdisbursementamount  where application_gid='" + values.application_gid + "' and " +
                   " (rmdisbursementrequest_gid !='" + employee_gid + "' and " +
                   " rmdisbursementrequest_gid !='" + values.rmdisbursementrequest_gid + "')" +
                   " and rmdisbursementrequest_gid not in (select rmdisbursementrequest_gid from ocs_trn_tdisbursementassignment where approval_status ='Rejected') " +
                   " and " +
                   " ((makerdisbursement_amount is not null ||makerdisbursement_amount !='') && " +
                   " (checkerdisbursement_amount is  null ||checkerdisbursement_amount ='')) " +
                   " union all " +
                   " select checkerdisbursement_amount as sumdisbursement_amount from ocs_trn_tdisbursementamount  where application_gid='" + values.application_gid + "' and " +
                   " (rmdisbursementrequest_gid !='" + employee_gid + "' and " +
                   " rmdisbursementrequest_gid  !='" + values.rmdisbursementrequest_gid + "')" +
                   " and rmdisbursementrequest_gid not in (select rmdisbursementrequest_gid from ocs_trn_tdisbursementassignment where approval_status ='Rejected') " +
                   " and " +
                   " ((checkerdisbursement_amount is not null ||checkerdisbursement_amount !=''))" +
                    " union all" +
                    " select case when (creditopscheckerdisbursement_amount is null and  creditopsdisbursement_amount is null) then disbursement_amount  " +
                    " when (creditopscheckerdisbursement_amount is null ) then creditopsdisbursement_amount  " +
                    " else creditopscheckerdisbursement_amount end as sumdisbursement_amount " +
                    " from ocs_trn_tdisbursementsupplier  where application_gid='" + values.application_gid + "' and " +
                    " disbursementsupplier_gid !='" + values.disbursementsupplier_gid + "' " +
                    " and rmdisbursementrequest_gid not in (select rmdisbursementrequest_gid from ocs_trn_tdisbursementassignment where approval_status ='Rejected') " +
                    " union all" +
                    " select case when (creditopscheckerdisbursement_amount is null and  creditopsdisbursement_amount is null) then disbursement_amount  " +
                    " when (creditopscheckerdisbursement_amount is null ) then creditopsdisbursement_amount  " +
                    " else creditopscheckerdisbursement_amount end as sumdisbursement_amount " +
                    " from ocs_trn_tfarmercontact  where application_gid='" + values.application_gid + "' and " +
                    " farmercontact_gid !='" + values.farmercontact_gid + "' " +
                    " and rmdisbursementrequest_gid not in (select rmdisbursementrequest_gid from ocs_trn_tdisbursementassignment where approval_status ='Rejected') ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdisbursementamount_list = new List<disbursementamount_list>();
            double disbursementamount_total = 0;
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {

                    disbursementamount_total = disbursementamount_total + double.Parse(dr_datarow["sumdisbursement_amount"].ToString().Replace(",", ""));

                }

            }
            dt_datatable.Dispose();


            if (string.IsNullOrEmpty(values.creditopscheckerdisbursement_amount))
            {

            }
            else
            {
                disbursementamount_total = disbursementamount_total + double.Parse(values.creditopscheckerdisbursement_amount.Replace(",", ""));
            }

            if (disbursementamount_total <= double.Parse(lssanction_amount))
            {

            }
            else
            {
                values.status = false;
                values.message = "Checker Disbursement Amount is Greater than Sanction Amount";

                return;
            }

            msSQL = " update ocs_trn_tfarmercontact set" +
                    " creditopscheckerdisbursement_amount='" + values.creditopscheckerdisbursement_amount + "'" +
                    " where farmercontact_gid='" + values.farmercontact_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                values.status = true;
                values.message = "Disbursement Amount Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
            }
        }
        public void DaGetCreditOpsCheckerFarmerDisbAmountView(string employee_gid, string farmercontact_gid, MdlFarmerIndividualDtlView values)
        {
            msSQL = " select farmercontact_gid,creditopscheckerdisbursement_amount " +
                    " from ocs_trn_tfarmercontact a" +
                    " where farmercontact_gid='" + farmercontact_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.farmercontact_gid = objODBCDatareader["farmercontact_gid"].ToString();
                values.creditopscheckerdisbursement_amount = objODBCDatareader["creditopscheckerdisbursement_amount"].ToString();
            }
            objODBCDatareader.Close();
        }
        public void DaCreditOpsCheckerSupplierDisbAmountUpdate(string employee_gid, MdlDisbSupplierBankAcct values)
        {

            if (string.IsNullOrEmpty(values.creditopscheckerdisbursement_amount))
            {
                values.status = false;
                values.message = "Kindly Add Checker Disbursement Amount";

                return;

            }


            msSQL = "select sanction_amount from ocs_trn_tapplication2sanction where  application_gid='" + values.application_gid + "'";
            string lssanction_amount = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " select disbursement_amount as sumdisbursement_amount from ocs_trn_tdisbursementamount  where application_gid='" + values.application_gid + "' and " +
                   " (rmdisbursementrequest_gid !='" + employee_gid + "' and " +
                   " rmdisbursementrequest_gid !='" + values.rmdisbursementrequest_gid + "')" +
                   " and rmdisbursementrequest_gid not in (select rmdisbursementrequest_gid from ocs_trn_tdisbursementassignment where approval_status ='Rejected') " +
                   " and " +
                   " ((makerdisbursement_amount is null ||makerdisbursement_amount ='') && " +
                   " (checkerdisbursement_amount is null ||checkerdisbursement_amount ='')) " +
                   " union all " +
                   " select makerdisbursement_amount  as sumdisbursement_amount from ocs_trn_tdisbursementamount  where application_gid='" + values.application_gid + "' and " +
                   " (rmdisbursementrequest_gid !='" + employee_gid + "' and " +
                   " rmdisbursementrequest_gid !='" + values.rmdisbursementrequest_gid + "')" +
                   " and rmdisbursementrequest_gid not in (select rmdisbursementrequest_gid from ocs_trn_tdisbursementassignment where approval_status ='Rejected') " +
                   " and " +
                   " ((makerdisbursement_amount is not null ||makerdisbursement_amount !='') && " +
                   " (checkerdisbursement_amount is  null ||checkerdisbursement_amount ='')) " +
                   " union all " +
                   " select checkerdisbursement_amount as sumdisbursement_amount from ocs_trn_tdisbursementamount  where application_gid='" + values.application_gid + "' and " +
                   " (rmdisbursementrequest_gid !='" + employee_gid + "' and " +
                   " rmdisbursementrequest_gid  !='" + values.rmdisbursementrequest_gid + "')" +
                   " and rmdisbursementrequest_gid not in (select rmdisbursementrequest_gid from ocs_trn_tdisbursementassignment where approval_status ='Rejected') " +
                   " and " +
                   " ((checkerdisbursement_amount is not null ||checkerdisbursement_amount !=''))" +
                    " union all" +
                    " select case when (creditopscheckerdisbursement_amount is null and  creditopsdisbursement_amount is null) then disbursement_amount  " +
                    " when (creditopscheckerdisbursement_amount is null ) then creditopsdisbursement_amount  " +
                    " else creditopscheckerdisbursement_amount end as sumdisbursement_amount " +
                    " from ocs_trn_tdisbursementsupplier  where application_gid='" + values.application_gid + "' and " +
                    " disbursementsupplier_gid !='" + values.disbursementsupplier_gid + "' " +
                    " and rmdisbursementrequest_gid not in (select rmdisbursementrequest_gid from ocs_trn_tdisbursementassignment where approval_status ='Rejected') " +
                    " union all" +
                    " select case when (creditopscheckerdisbursement_amount is null and  creditopsdisbursement_amount is null) then disbursement_amount  " +
                    " when (creditopscheckerdisbursement_amount is null ) then creditopsdisbursement_amount  " +
                    " else creditopscheckerdisbursement_amount end as sumdisbursement_amount " +
                    " from ocs_trn_tfarmercontact  where application_gid='" + values.application_gid + "' and " +
                    " farmercontact_gid !='" + values.farmercontact_gid + "' " +
                    " and rmdisbursementrequest_gid not in (select rmdisbursementrequest_gid from ocs_trn_tdisbursementassignment where approval_status ='Rejected') ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdisbursementamount_list = new List<disbursementamount_list>();
            double disbursementamount_total = 0;
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {

                    disbursementamount_total = disbursementamount_total + double.Parse(dr_datarow["sumdisbursement_amount"].ToString().Replace(",", ""));

                }

            }
            dt_datatable.Dispose();


            if (string.IsNullOrEmpty(values.creditopscheckerdisbursement_amount))
            {

            }
            else
            {
                disbursementamount_total = disbursementamount_total + double.Parse(values.creditopscheckerdisbursement_amount.Replace(",", ""));
            }

            if (disbursementamount_total <= double.Parse(lssanction_amount))
            {

            }
            else
            {
                values.status = false;
                values.message = "Checker Disbursement Amount is Greater than Sanction Amount";

                return;
            }

            msSQL = " update ocs_trn_tdisbursementsupplier set" +
                    " creditopscheckerdisbursement_amount='" + values.creditopscheckerdisbursement_amount + "'" +
                    " where disbursementsupplier_gid='" + values.disbursementsupplier_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                values.status = true;
                values.message = "Disbursement Amount Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
            }
        }

        public void DaGetCreditOpsCheckerSupplierDisbAmountView(string employee_gid, string disbursementsupplier_gid, MdlDisbSupplierBankAcct values)
        {
            msSQL = " select disbursementsupplier_gid,creditopscheckerdisbursement_amount " +
                    " from ocs_trn_tdisbursementsupplier a" +
                    " where disbursementsupplier_gid='" + disbursementsupplier_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.disbursementsupplier_gid = objODBCDatareader["disbursementsupplier_gid"].ToString();
                values.creditopscheckerdisbursement_amount = objODBCDatareader["creditopscheckerdisbursement_amount"].ToString();
            }
            objODBCDatareader.Close();
        }
        public bool DaPostDisbApplicantBankDtl(string employee_gid, MdlDisbSupplierBankAcct values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("DABD");
            msSQL = " insert into ocs_trn_tdisbapplicantbankdtl(" +
                    " disbapplicantbankdtl_gid," +
                    " application_gid," +
                    " applicant_name," +
                    " ifsc_code," +
                    " micr_code," +
                    " branch_address," +
                    " bank_name," +
                    " branch_name," +
                    " bankaccount_number," +
                    " confirmbankaccount_number," +
                    " accountholder_name," +
                    " disbursement_amount," +
                    " rmdisbursementrequest_gid," +
                    " disbursementaccount_status," +
                    " initiated_by," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.application_gid + "',";

            if (values.applicant_name == null || values.applicant_name == "")
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.applicant_name.Replace("'", "") + "',";
            }
            msSQL += "'" + values.ifsc_code + "'," +
            "'" + values.micr_code + "'," +
            "'" + values.branch_address + "'," +
            "'" + values.bank_name + "'," +
            "'" + values.branch_name + "'," +
            "'" + values.bankaccount_number + "'," +
            "'" + values.confirmbankaccount_number + "'," +
            "'" + values.accountholder_name + "'," +
            "'" + values.disbursement_amount + "'," +
            "'" + employee_gid + "'," +
            "'" + values.disbursementaccount_status + "'," +
            "'" + values.initiated_by + "'," +
            "'" + employee_gid + "'," +
            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                string lsdisbapplicantbankdtl_gid = "";
                msSQL = " select disbapplicantbankdtl_gid from ocs_trn_tdisbapplicantbankdtl  where rmdisbursementrequest_gid='" + employee_gid + "' and '" + values.disbursementaccount_status + "' = 'Yes'";
                lsdisbapplicantbankdtl_gid = objdbconn.GetExecuteScalar(msSQL);
                if (lsdisbapplicantbankdtl_gid != "")
                {
                    msSQL = "delete from ocs_trn_tbankaccountstatus where  (rmdisbursementrequest_gid='" + values.rmdisbursementrequest_gid + "' or " +
                    " rmdisbursementrequest_gid='" + employee_gid + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                msSQL = "update ocs_mst_tdisbapplicantbankdocument set disbapplicantbankdtl_gid='" + msGetGid + "' where disbapplicantbankdtl_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " select disbapplicantbankdtl_gid,applicant_name,bank_name,branch_name,ifsc_code, " +
                        " bankaccount_number,accountholder_name, " +
                        " concat(f.user_firstname, ' ', f.user_lastname, ' / ', f.user_code) as created_by, " +
                        " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date, " +
                        " disbursementaccount_status,initiated_by" +
                        " from ocs_trn_tdisbapplicantbankdtl a " +
                        " left join hrm_mst_temployee e on e.employee_gid = a.created_by " +
                        " left join adm_mst_tuser f on f.user_gid = e.user_gid " +
                        " where a.rmdisbursementrequest_gid='" + employee_gid + "' or " +
                        " a.rmdisbursementrequest_gid='" + values.rmdisbursementrequest_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getdisbapplicantbankacctdtl_list = new List<disbapplicantbankacctdtl_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getdisbapplicantbankacctdtl_list.Add(new disbapplicantbankacctdtl_list
                        {
                            applicant_name = dt["applicant_name"].ToString(),
                            bank_name = dt["bank_name"].ToString(),
                            branch_name = dt["branch_name"].ToString(),
                            ifsc_code = dt["ifsc_code"].ToString(),
                            bankaccount_number = dt["bankaccount_number"].ToString(),
                            accountholder_name = dt["accountholder_name"].ToString(),
                            disbapplicantbankdtl_gid = dt["disbapplicantbankdtl_gid"].ToString(),
                            created_by = dt["created_by"].ToString(),
                            created_date = dt["created_date"].ToString(),
                            disbursementaccount_status = dt["disbursementaccount_status"].ToString(),
                            initiated_by = dt["initiated_by"].ToString()
                        });
                        values.disbapplicantbankacctdtl_list = getdisbapplicantbankacctdtl_list;
                    }
                }
                dt_datatable.Dispose();
                values.status = true;
                values.message = "Bank Account Added Successfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured While Adding Bank Account Details";
                return false;
            }

        }
        public bool DaDisbApplicantdocumentUpload(HttpRequest httpRequest, disbsupplieruploaddocument objfilename, string employee_gid)
        {

            upload_list objdocumentmodel = new upload_list();
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string lsdocument_title = httpRequest.Form["document_title"].ToString();
            string lsdisbapplicantbankdtl_gid = httpRequest.Form["disbapplicantbankdtl_gid"].ToString();
            // string lsdocument_id = httpRequest.Form["document_id"].ToString();
            string project_flag = httpRequest.Form["project_flag"].ToString();
            String path = lspath;

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/DisbursementApplicantBankAcctDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
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
                        // Check Document validation;

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.message = "File format is not supported";
                            return false;
                        }
                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Master/DisbursementApplicantBankAcctDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "Master/DisbursementApplicantBankAcctDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        msGetGid = objcmnfunctions.GetMasterGID("DADD");
                        msSQL = " insert into ocs_mst_tdisbapplicantbankdocument( " +
                                    " disbapplicantbankdocument_gid," +
                                    " disbapplicantbankdtl_gid," +
                                    " document_title  ," +
                                    " document_name  ," +
                                    " document_path," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + employee_gid + "'," +
                                      "'" + lsdocument_title.Replace("'", "") + "'," +
                                    "'" + httpPostedFile.FileName.Replace("'", "") + "'," +
                                    "'" + lspath + msdocument_gid + FileExtension.Replace("'", "") + "'," +
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

                        msSQL = " select disbapplicantbankdocument_gid,disbapplicantbankdtl_gid,document_title,document_name,document_path " +
                                " from ocs_mst_tdisbapplicantbankdocument where " +
                                " disbapplicantbankdtl_gid='" + employee_gid + "' or  disbapplicantbankdtl_gid='" + lsdisbapplicantbankdtl_gid + "'";
                        dt_datatable = objdbconn.GetDataTable(msSQL);
                        var getdisbapplicantuploaddocument_list = new List<disbapplicantuploaddocument_list>();
                        if (dt_datatable.Rows.Count != 0)
                        {
                            foreach (DataRow dt in dt_datatable.Rows)
                            {
                                getdisbapplicantuploaddocument_list.Add(new disbapplicantuploaddocument_list
                                {
                                    document_name = dt["document_name"].ToString(),
                                    document_title = dt["document_title"].ToString(),
                                    document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                                    disbapplicantbankdtl_gid = dt["disbapplicantbankdtl_gid"].ToString(),
                                    disbapplicantbankdocument_gid = dt["disbapplicantbankdocument_gid"].ToString()
                                });
                                objfilename.disbapplicantuploaddocument_list = getdisbapplicantuploaddocument_list;
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
        public void DaDeleteDisbApplicantdocument(string disbapplicantbankdocument_gid, string disbapplicantbankdtl_gid, disbsupplieruploaddocument values, string employee_gid)
        {
            msSQL = "delete from ocs_mst_tdisbapplicantbankdocument where disbapplicantbankdocument_gid='" + disbapplicantbankdocument_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Document Deleted successfully";

                msSQL = " select disbapplicantbankdocument_gid,disbapplicantbankdtl_gid,document_name,document_path,document_title " +
                        " from ocs_mst_tdisbapplicantbankdocument where disbapplicantbankdtl_gid='" + employee_gid + "'" +
                        " or disbapplicantbankdtl_gid='" + disbapplicantbankdtl_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getdisbapplicantuploaddocument_list = new List<disbapplicantuploaddocument_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getdisbapplicantuploaddocument_list.Add(new disbapplicantuploaddocument_list
                        {
                            document_name = dt["document_name"].ToString(),
                            document_title = dt["document_title"].ToString(),
                            document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                            disbapplicantbankdtl_gid = dt["disbapplicantbankdtl_gid"].ToString(),
                            disbapplicantbankdocument_gid = dt["disbapplicantbankdocument_gid"].ToString()
                        });
                        values.disbapplicantuploaddocument_list = getdisbapplicantuploaddocument_list;
                    }
                }
                dt_datatable.Dispose();


            }
            else
            {
                values.status = false;
                values.message = "Error Occurred while deleting Document";
            }
        }
        public void DaGetDisbApplicantSummary(string rmdisbursementrequest_gid, string employee_gid, MdlDisbSupplierBankAcct values)
        {
            try
            {
                msSQL = " select disbapplicantbankdtl_gid,applicant_name,bank_name,branch_name,ifsc_code, " +
                        " bankaccount_number,accountholder_name,disbursement_amount, " +
                        " concat(f.user_firstname, ' ', f.user_lastname, ' / ', f.user_code) as created_by, " +
                        " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date," +
                        " disbursementaccount_status,initiated_by " +
                        " from ocs_trn_tdisbapplicantbankdtl a " +
                        " left join hrm_mst_temployee e on e.employee_gid = a.created_by " +
                        " left join adm_mst_tuser f on f.user_gid = e.user_gid " +
                        " where a.rmdisbursementrequest_gid='" + employee_gid + "' or " +
                        " a.rmdisbursementrequest_gid='" + rmdisbursementrequest_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getdisbapplicantbankacctdtl_list = new List<disbapplicantbankacctdtl_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getdisbapplicantbankacctdtl_list.Add(new disbapplicantbankacctdtl_list
                        {
                            applicant_name = dt["applicant_name"].ToString(),
                            bank_name = dt["bank_name"].ToString(),
                            branch_name = dt["branch_name"].ToString(),
                            ifsc_code = dt["ifsc_code"].ToString(),
                            bankaccount_number = dt["bankaccount_number"].ToString(),
                            accountholder_name = dt["accountholder_name"].ToString(),
                            disbapplicantbankdtl_gid = dt["disbapplicantbankdtl_gid"].ToString(),
                            disbursement_amount = dt["disbursement_amount"].ToString(),
                            created_by = dt["created_by"].ToString(),
                            created_date = dt["created_date"].ToString(),
                            disbursementaccount_status = dt["disbursementaccount_status"].ToString(),
                            initiated_by = dt["initiated_by"].ToString()
                        });
                        values.disbapplicantbankacctdtl_list = getdisbapplicantbankacctdtl_list;
                    }
                }
                dt_datatable.Dispose();
            }
            catch
            {
                values.status = false;
            }
        }
        public void DaGetDisbApplicantDocTempClear(string employee_gid, result values)
        {
            msSQL = "delete from ocs_mst_tdisbapplicantbankdocument where disbapplicantbankdtl_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            values.status = true;
        }
        public void DaGetDisbApplicantBankDtlView(string disbapplicantbankdtl_gid, MdlDisbSupplierBankAcct values)
        {
            try
            {
                msSQL = " select a.disbapplicantbankdtl_gid,a.application_gid,a.applicant_name,a.ifsc_code,a.micr_code, " +
                        " a.branch_address,a.bank_name,a.branch_name,a.bankaccount_number,a.confirmbankaccount_number," +
                        " a.accountholder_name,a.disbursement_amount,a.disbursementaccount_status,a.initiated_by, " +
                        " concat(f.user_firstname, ' ', f.user_lastname, ' / ', f.user_code) as created_by, " +
                        " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date " +
                        " from ocs_trn_tdisbapplicantbankdtl a " +
                        " left join hrm_mst_temployee e on e.employee_gid = a.created_by " +
                        " left join adm_mst_tuser f on f.user_gid = e.user_gid " +
                        " where a.disbapplicantbankdtl_gid='" + disbapplicantbankdtl_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.disbapplicantbankdtl_gid = objODBCDatareader["disbapplicantbankdtl_gid"].ToString();
                    values.application_gid = objODBCDatareader["application_gid"].ToString();
                    values.applicant_name = objODBCDatareader["applicant_name"].ToString();
                    values.ifsc_code = objODBCDatareader["ifsc_code"].ToString();
                    values.micr_code = objODBCDatareader["micr_code"].ToString();
                    values.branch_address = objODBCDatareader["branch_address"].ToString();
                    values.bank_name = objODBCDatareader["bank_name"].ToString();
                    values.branch_name = objODBCDatareader["branch_name"].ToString();
                    values.bankaccount_number = objODBCDatareader["bankaccount_number"].ToString();
                    values.confirmbankaccount_number = objODBCDatareader["confirmbankaccount_number"].ToString();
                    values.accountholder_name = objODBCDatareader["accountholder_name"].ToString();
                    values.disbursement_amount = objODBCDatareader["disbursement_amount"].ToString();
                    values.created_by = objODBCDatareader["created_by"].ToString();
                    values.created_date = objODBCDatareader["created_date"].ToString();
                    values.disbursementaccount_status = objODBCDatareader["disbursementaccount_status"].ToString();
                    values.initiated_by = objODBCDatareader["initiated_by"].ToString();
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
        public void DaDisbApplicantBankAcctDocView(string disbapplicantbankdtl_gid, disbsupplieruploaddocument values, string employee_gid)
        {
            msSQL = " select disbapplicantbankdocument_gid,disbapplicantbankdtl_gid,document_name,document_path,document_title " +
                    " from ocs_mst_tdisbapplicantbankdocument where disbapplicantbankdtl_gid='" + disbapplicantbankdtl_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdisbapplicantuploaddocument_list = new List<disbapplicantuploaddocument_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getdisbapplicantuploaddocument_list.Add(new disbapplicantuploaddocument_list
                    {
                        document_name = dt["document_name"].ToString(),
                        document_title = dt["document_title"].ToString(),
                        document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                        disbapplicantbankdtl_gid = dt["disbapplicantbankdtl_gid"].ToString(),
                        disbapplicantbankdocument_gid = dt["disbapplicantbankdocument_gid"].ToString()
                    });
                    values.disbapplicantuploaddocument_list = getdisbapplicantuploaddocument_list;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaGetDisbursementApprovalDtlView(string rmdisbursementrequest_gid, MdlDisbursementDtlView values)
        {
            try
            {
                msSQL = " select a.creditopsgroup_gid,a.creditopsgroup_name,a.creditopsmaker_gid,a.creditopsmaker_name, " +
                        " date_format(a.maker_approveddate, '%d-%m-%Y %h:%i %p') as maker_approveddate," +
                        " creditopschecker_gid,creditopschecker_name," +
                        " date_format(a.checker_approveddate, '%d-%m-%Y %h:%i %p') as checker_approveddate " +
                        " from ocs_trn_tdisbursementassignment a" +
                        " where a.rmdisbursementrequest_gid='" + rmdisbursementrequest_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.creditopsgroup_gid = objODBCDatareader["creditopsgroup_gid"].ToString();
                    values.creditopsgroup_name = objODBCDatareader["creditopsgroup_name"].ToString();
                    values.creditopsmaker_gid = objODBCDatareader["creditopsmaker_gid"].ToString();
                    values.creditopsmaker_name = objODBCDatareader["creditopsmaker_name"].ToString();
                    values.creditopschecker_gid = objODBCDatareader["creditopschecker_gid"].ToString();
                    values.creditopschecker_name = objODBCDatareader["creditopschecker_name"].ToString();
                    values.maker_approveddate = objODBCDatareader["maker_approveddate"].ToString();
                    values.checker_approveddate = objODBCDatareader["checker_approveddate"].ToString();
                }
                values.status = true;
                values.message = "success";
                objODBCDatareader.Close();

            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "failure";
            }

        }
        public void DaGetDisbursementDocumentSummary(string employee_gid, string vertical_gid, MdlDisbursementDocument values)
        {
            try
            {
                msSQL = " select verticaldisbursementdocument_gid,vertical_gid, customer_type, verticaldisbursementdocument_status," +
                        " date_format(a.wef_date, '%d-%m-%Y %h:%i %p') as wef_date, " +
                         " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as created_by, " +
                         " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date " +
                         " from ocs_mst_tverticaldisbursementdocument a" +
                         " left join hrm_mst_temployee b on b.employee_gid = a.created_by " +
                         " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                         " where vertical_gid='" + vertical_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getdisbursementdocument_list = new List<disbursementdocument_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getdisbursementdocument_list.Add(new disbursementdocument_list
                        {
                            verticaldisbursementdocument_gid = dt["verticaldisbursementdocument_gid"].ToString(),
                            vertical_gid = dt["vertical_gid"].ToString(),
                            customer_type = dt["customer_type"].ToString(),
                            verticaldisbursementdocument_status = dt["verticaldisbursementdocument_status"].ToString(),
                            wef_date = dt["wef_date"].ToString(),
                            created_by = dt["created_by"].ToString(),
                            created_date = dt["created_date"].ToString(),

                        });
                        values.disbursementdocument_list = getdisbursementdocument_list;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = ex.ToString();
                values.status = false;
            }

        }
        public void DaGetDisbursementDocumentApprovalConfigSummary(string employee_gid, string vertical_gid, MdlDisbursementDocument values)
        {
            try
            {
                msSQL = " select a.disbursementdocumentapprovalconfig_gid,d.verticaldisbursementdocument_gid, d.vertical_gid, group_gid," +
                        " group_name,subgroup_gid,subgroup_name,manager_gid,manager_name,member_gid,member_name, " +
                        " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as created_by, " +
                        " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date, " +
                        " verticaldisbursementdocument_status,date_format(d.wef_date, '%d-%m-%Y %h:%i %p') as wef_date,d.customer_type " +
                        " from ocs_mst_tdisbursementdocumentapprovalconfig a" +
                        " left join hrm_mst_temployee b on b.employee_gid = a.created_by " +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " left join ocs_mst_tverticaldisbursementdocument d on d.verticaldisbursementdocument_gid = a.verticaldisbursementdocument_gid " +
                        " where d.vertical_gid='" + vertical_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getdisbursementdocumentapprovalconfig_list = new List<disbursementdocumentapprovalconfig_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getdisbursementdocumentapprovalconfig_list.Add(new disbursementdocumentapprovalconfig_list
                        {
                            disbursementdocumentapprovalconfig_gid = dt["disbursementdocumentapprovalconfig_gid"].ToString(),
                            verticaldisbursementdocument_gid = dt["verticaldisbursementdocument_gid"].ToString(),
                            vertical_gid = dt["vertical_gid"].ToString(),
                            group_gid = dt["group_gid"].ToString(),
                            group_name = dt["group_name"].ToString(),
                            subgroup_gid = dt["subgroup_gid"].ToString(),
                            subgroup_name = dt["subgroup_name"].ToString(),
                            manager_gid = dt["manager_gid"].ToString(),
                            manager_name = dt["manager_name"].ToString(),
                            member_gid = dt["member_gid"].ToString(),
                            member_name = dt["member_name"].ToString(),
                            created_by = dt["created_by"].ToString(),
                            created_date = dt["created_date"].ToString(),
                            wef_date = dt["wef_date"].ToString(),
                            customer_type = dt["customer_type"].ToString(),
                            verticaldisbursementdocument_status = dt["verticaldisbursementdocument_status"].ToString()
                        });
                        values.disbursementdocumentapprovalconfig_list = getdisbursementdocumentapprovalconfig_list;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = ex.ToString();
                values.status = false;
            }
        }
        public void DaPostDisbursementDocument(MdlDisbursementDocument values, string employee_gid)
        {
            msSQL = " select verticaldisbursementdocument_gid from ocs_mst_tverticaldisbursementdocument a " +
                    " where a.vertical_gid = '" + values.vertical_gid + "' and " +
                    " a.customer_type = '" + values.customer_type + "' and a.verticaldisbursementdocument_status='Y'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.message = "This Customer Type already Added.Kindly, Inactive that and again add the Customer Type";
                values.status = false;
                return;
            }
            else
            {
                lswefdate = values.wef_date;
                lswefdatetime = GetDateFormat(lswefdate);

                msGetGid = objcmnfunctions.GetMasterGID("VDID");
                msSQL = " insert into ocs_mst_tverticaldisbursementdocument(" +
                        " verticaldisbursementdocument_gid ," +
                        " vertical_gid ," +
                        " wef_date," +
                        " customer_type," +
                        " verticaldisbursementdocument_status," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + values.vertical_gid + "'," +
                        "'" + lswefdatetime + "'," +
                        "'" + values.customer_type + "'," +
                        "'Y'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Disbursement Document Details Added successfully";
                }
                else
                {
                    values.message = "Error Occured while Adding";
                    values.status = false;
                }
            }
        }
        public void DaPostDisbursementDocumentApprovalConfig(MdlDisbursementDocument values, string employee_gid)
        {
            msGetGid = objcmnfunctions.GetMasterGID("DDAC");
            msSQL = " insert into ocs_mst_tdisbursementdocumentapprovalconfig(" +
                    " disbursementdocumentapprovalconfig_gid ," +
                    " verticaldisbursementdocument_gid ," +
                    " vertical_gid," +
                    " group_gid," +
                    " group_name," +
                    " subgroup_gid ," +
                    " subgroup_name ," +
                    " manager_gid," +
                    " manager_name," +
                    " member_gid," +
                    " member_name," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.verticaldisbursementdocument_gid + "'," +
                    "'" + values.vertical_gid + "'," +
                    "'" + values.group_gid + "'," +
                    "'" + values.group_name + "'," +
                    "'" + values.subgroup_gid + "'," +
                    "'" + values.subgroup_name + "'," +
                    "'" + values.manager_gid + "'," +
                    "'" + values.manager_name + "'," +
                    "'" + values.member_gid + "'," +
                    "'" + values.member_name + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Approval Config Added successfully";
            }
            else
            {
                values.message = "Error Occured while Adding";
                values.status = false;
            }

        }
        public void DaGetDisbursementDocumentView(string verticaldisbursementdocument_gid, MdlDisbursementDocument values)
        {
            try
            {
                msSQL = " select verticaldisbursementdocument_gid, wef_date,customer_type,verticaldisbursementdocument_status " +
                        " from ocs_mst_tverticaldisbursementdocument a" +
                        " where verticaldisbursementdocument_gid ='" + verticaldisbursementdocument_gid + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows)
                {
                    values.verticaldisbursementdocument_gid = objODBCDataReader["verticaldisbursementdocument_gid"].ToString();
                    values.wef_date = objODBCDataReader["wef_date"].ToString();
                    values.customer_type = objODBCDataReader["customer_type"].ToString();
                    values.verticaldisbursementdocument_status = objODBCDataReader["verticaldisbursementdocument_status"].ToString();
                }
                objODBCDataReader.Close();
            }
            catch (Exception ex)
            {
                values.message = ex.ToString();
                values.status = false;
            }
        }
        public void DaPostDisbursementDocumentInactive(MdlDisbursementDocument values, string employee_gid)
        {
            if (values.remarks == null || values.remarks == "")
            {
                lsremarks = "";
            }
            else
            {
                lsremarks = values.remarks.Replace("'", "\\'");
            }
            msSQL = " update ocs_mst_tverticaldisbursementdocument set verticaldisbursementdocument_status ='" + values.verticaldisbursementdocument_status + "'," +
                    " remarks='" + lsremarks + "'" +
                    " where verticaldisbursementdocument_gid='" + values.verticaldisbursementdocument_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("DDIL");

                msSQL = " insert into ocs_mst_tverticaldisbursementdocumentinactivelog (" +
                      " verticaldisbursementdocumentinactivelog_gid, " +
                      " verticaldisbursementdocument_gid," +
                      " wef_date ," +
                      " customer_type ," +
                      " vertical_gid," +
                      " verticaldisbursementdocument_status," +
                      " remarks, " +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.verticaldisbursementdocument_gid + "'," +
                      " '" + Convert.ToDateTime(values.wef_date).ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                      " '" + values.customer_type + "'," +
                      " '" + values.vertical_gid + "'," +
                      " '" + values.verticaldisbursementdocument_status + "'," +
                      " '" + lsremarks + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.verticaldisbursementdocument_status == "N")
                {
                    values.status = true;
                    values.message = "Disbursement Document Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = " Disbursement Document  Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Changing Status";
            }
        }
        public void DaGetDisbursementDocumentInactiveLogview(string verticaldisbursementdocument_gid, MdlDisbursementDocument values)
        {
            try
            {
                msSQL = " Select verticaldisbursementdocumentinactivelog_gid,verticaldisbursementdocument_gid," +
                        " date_format(a.wef_date,'%d-%m-%Y %h:%i %p') as wef_date,customer_type,vertical_gid,a.remarks, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                        " date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                        " case when a.verticaldisbursementdocument_status='N' then 'Inactive' else 'Active' end as verticaldisbursementdocument_status" +
                        " from ocs_mst_tverticaldisbursementdocumentinactivelog a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where verticaldisbursementdocument_gid ='" + verticaldisbursementdocument_gid + "'" +
                        " order by a.verticaldisbursementdocumentinactivelog_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getdisbursementdocumentlog_list = new List<disbursementdocumentlog_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getdisbursementdocumentlog_list.Add(new disbursementdocumentlog_list
                        {
                            verticaldisbursementdocumentinactivelog_gid = (dr_datarow["verticaldisbursementdocumentinactivelog_gid"].ToString()),
                            verticaldisbursementdocument_gid = (dr_datarow["verticaldisbursementdocument_gid"].ToString()),
                            wef_date = (dr_datarow["wef_date"].ToString()),
                            customer_type = (dr_datarow["customer_type"].ToString()),
                            vertical_gid = (dr_datarow["vertical_gid"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            verticaldisbursementdocument_status = (dr_datarow["verticaldisbursementdocument_status"].ToString()),
                        });
                    }
                    values.disbursementdocumentlog_list = getdisbursementdocumentlog_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }
        public void DaGetDisbursementDocumentEdit(string disbursementdocumentapprovalconfig_gid, MdlDisbursementDocument values)
        {
            try
            {
                msSQL = " select disbursementdocumentapprovalconfig_gid,verticaldisbursementdocument_gid,vertical_gid,group_gid,group_name, " +
                        " subgroup_gid,subgroup_name,manager_gid,manager_name,member_gid,member_name" +
                        " from ocs_mst_tdisbursementdocumentapprovalconfig a " +
                        " where a.disbursementdocumentapprovalconfig_gid='" + disbursementdocumentapprovalconfig_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.disbursementdocumentapprovalconfig_gid = objODBCDatareader["disbursementdocumentapprovalconfig_gid"].ToString();
                    values.verticaldisbursementdocument_gid = objODBCDatareader["verticaldisbursementdocument_gid"].ToString();
                    values.vertical_gid = objODBCDatareader["vertical_gid"].ToString();
                    values.group_gid = objODBCDatareader["group_gid"].ToString();
                    values.group_name = objODBCDatareader["group_name"].ToString();
                    values.subgroup_gid = objODBCDatareader["subgroup_gid"].ToString();
                    values.subgroup_name = objODBCDatareader["subgroup_name"].ToString();
                    values.manager_gid = objODBCDatareader["manager_gid"].ToString();
                    values.manager_name = objODBCDatareader["manager_name"].ToString();
                    values.member_gid = objODBCDatareader["member_gid"].ToString();
                    values.member_name = objODBCDatareader["member_name"].ToString();
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
        public bool DaPostDisbursementDocumentUpdate(string employee_gid, MdlDisbursementDocument values)
        {
            msSQL = " select disbursementdocumentapprovalconfig_gid,verticaldisbursementdocument_gid,vertical_gid,group_gid,group_name, " +
                        " subgroup_gid,subgroup_name,manager_gid,manager_name,member_gid,member_name" +
                        " from ocs_mst_tdisbursementdocumentapprovalconfig a " +
                        " where a.disbursementdocumentapprovalconfig_gid='" + values.disbursementdocumentapprovalconfig_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                lsdisbursementdocumentapprovalconfig_gid = objODBCDatareader["disbursementdocumentapprovalconfig_gid"].ToString();
                lsverticaldisbursementdocument_gid = objODBCDatareader["verticaldisbursementdocument_gid"].ToString();
                lsvertical_gid = objODBCDatareader["vertical_gid"].ToString();
                lsgroup_gid = objODBCDatareader["group_gid"].ToString();
                lsgroup_name = objODBCDatareader["group_name"].ToString();
                lssubgroup_gid = objODBCDatareader["subgroup_gid"].ToString();
                lssubgroup_name = objODBCDatareader["subgroup_name"].ToString();
                lsmanager_gid = objODBCDatareader["manager_gid"].ToString();
                lsmanager_name = objODBCDatareader["manager_name"].ToString();
                lsmember_gid = objODBCDatareader["member_gid"].ToString();
                lsmember_name = objODBCDatareader["member_name"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " update ocs_mst_tdisbursementdocumentapprovalconfig set " +
                    " group_gid='" + values.group_gid + "'," +
                    " group_name = '" + values.group_name + "'," +
                    " subgroup_gid = '" + values.subgroup_gid + "'," +
                    " subgroup_name = '" + values.subgroup_name + "'," +
                    " manager_gid = '" + values.manager_gid + "'," +
                    " manager_name='" + values.manager_name + "'," +
                    " member_gid = '" + values.member_gid + "'," +
                    " member_name = '" + values.member_name + "'," +
                    " verticaldisbursementdocument_gid = '" + lsverticaldisbursementdocument_gid + "'," +
                    " vertical_gid='" + values.vertical_gid + "'," +
                    " updated_by = '" + employee_gid + "'," +
                    " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    " where disbursementdocumentapprovalconfig_gid ='" + values.disbursementdocumentapprovalconfig_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("VDAL");
                msSQL = " insert into ocs_mst_tdisbursementdocumentapprovalconfiglog(" +
                        " disbursementdocumentapprovalconfiglog_gid ," +
                        " disbursementdocumentapprovalconfig_gid," +
                        " verticaldisbursementdocument_gid," +
                        " vertical_gid," +
                        " group_gid," +
                        " group_name," +
                        " subgroup_gid," +
                        " subgroup_name," +
                        " manager_gid," +
                        " manager_name," +
                        " member_gid," +
                        " member_name," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + lsdisbursementdocumentapprovalconfig_gid + "'," +
                        "'" + lsverticaldisbursementdocument_gid + "'," +
                        "'" + lsvertical_gid + "'," +
                        "'" + lsgroup_gid + "'," +
                        "'" + lsgroup_name + "'," +
                        "'" + lssubgroup_gid + "'," +
                        "'" + lssubgroup_name + "'," +
                        "'" + lsmanager_gid + "'," +
                        "'" + lsmanager_name + "'," +
                        "'" + lsmember_gid + "'," +
                        "'" + lsmember_name + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Disbursement Document Details Updated Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while updating Disbursement Document Details";
                return false;
            }
        }
        public void DaGetDisbursementBankAccountSummary(string employee_gid, string vertical_gid, MdlDisbursementBankAccount values)
        {
            try
            {
                msSQL = " select disbursementbankaccount_gid,vertical_gid, customer_type, disbursementbankaccount_status," +
                        " date_format(a.wef_date, '%d-%m-%Y %h:%i %p') as wef_date, " +
                         " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as created_by, " +
                         " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date " +
                         " from ocs_mst_tdisbursementbankaccount a" +
                         " left join hrm_mst_temployee b on b.employee_gid = a.created_by " +
                         " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                         " where vertical_gid='" + vertical_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getdisbursementbankaccount_list = new List<disbursementbankaccount_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getdisbursementbankaccount_list.Add(new disbursementbankaccount_list
                        {
                            disbursementbankaccount_gid = dt["disbursementbankaccount_gid"].ToString(),
                            vertical_gid = dt["vertical_gid"].ToString(),
                            customer_type = dt["customer_type"].ToString(),
                            disbursementbankaccount_status = dt["disbursementbankaccount_status"].ToString(),
                            wef_date = dt["wef_date"].ToString(),
                            created_by = dt["created_by"].ToString(),
                            created_date = dt["created_date"].ToString(),

                        });
                        values.disbursementbankaccount_list = getdisbursementbankaccount_list;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = ex.ToString();
                values.status = false;
            }

        }
        public void DaGetDisbursementBankAccountApprovalConfigSummary(string employee_gid, string vertical_gid, MdlDisbursementBankAccount values)
        {
            try
            {
                msSQL = " select a.disbursementbankaccountapprovalconfig_gid,d.disbursementbankaccount_gid, d.vertical_gid, group_gid," +
                        " group_name,subgroup_gid,subgroup_name,manager_gid,manager_name,member_gid,member_name, " +
                        " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as created_by, " +
                        " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date, " +
                        " disbursementbankaccount_status,date_format(d.wef_date, '%d-%m-%Y %h:%i %p') as wef_date,d.customer_type " +
                        " from ocs_mst_tdisbursementbankaccountapprovalconfig a" +
                        " left join hrm_mst_temployee b on b.employee_gid = a.created_by " +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " left join ocs_mst_tdisbursementbankaccount d on d.disbursementbankaccount_gid = a.disbursementbankaccount_gid " +
                        " where d.vertical_gid='" + vertical_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getdisbursementbankaccountapprovalconfig_list = new List<disbursementbankaccountapprovalconfig_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getdisbursementbankaccountapprovalconfig_list.Add(new disbursementbankaccountapprovalconfig_list
                        {
                            disbursementbankaccountapprovalconfig_gid = dt["disbursementbankaccountapprovalconfig_gid"].ToString(),
                            disbursementbankaccount_gid = dt["disbursementbankaccount_gid"].ToString(),
                            vertical_gid = dt["vertical_gid"].ToString(),
                            group_gid = dt["group_gid"].ToString(),
                            group_name = dt["group_name"].ToString(),
                            subgroup_gid = dt["subgroup_gid"].ToString(),
                            subgroup_name = dt["subgroup_name"].ToString(),
                            manager_gid = dt["manager_gid"].ToString(),
                            manager_name = dt["manager_name"].ToString(),
                            member_gid = dt["member_gid"].ToString(),
                            member_name = dt["member_name"].ToString(),
                            created_by = dt["created_by"].ToString(),
                            created_date = dt["created_date"].ToString(),
                            wef_date = dt["wef_date"].ToString(),
                            customer_type = dt["customer_type"].ToString(),
                            disbursementbankaccount_status = dt["disbursementbankaccount_status"].ToString()
                        });
                        values.disbursementbankaccountapprovalconfig_list = getdisbursementbankaccountapprovalconfig_list;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = ex.ToString();
                values.status = false;
            }
        }
        public void DaPostDisbursementBankAccount(MdlDisbursementBankAccount values, string employee_gid)
        {
            msSQL = " select disbursementbankaccount_gid from ocs_mst_tdisbursementbankaccount a " +
                    " where a.vertical_gid = '" + values.vertical_gid + "' and " +
                    " a.customer_type = '" + values.customer_type + "' and a.disbursementbankaccount_status='Y'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.message = "This Customer Type already Added.Kindly, Inactive that and again add the Customer Type";
                values.status = false;
                return;
            }
            else
            {
                lswefdate = values.wef_date;
                lswefdatetime = GetDateFormat(lswefdate);

                msGetGid = objcmnfunctions.GetMasterGID("DIBA");
                msSQL = " insert into ocs_mst_tdisbursementbankaccount(" +
                        " disbursementbankaccount_gid ," +
                        " vertical_gid ," +
                        " wef_date," +
                        " customer_type," +
                        " disbursementbankaccount_status," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + values.vertical_gid + "'," +
                        "'" + lswefdatetime + "'," +
                        "'" + values.customer_type + "'," +
                        "'Y'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Disbursement Bank Account Details Added successfully";
                }
                else
                {
                    values.message = "Error Occured while Adding";
                    values.status = false;
                }
            }
        }
        public void DaPostDisbursementBankAccountApprovalConfig(MdlDisbursementBankAccount values, string employee_gid)
        {
            msGetGid = objcmnfunctions.GetMasterGID("DBAA");
            msSQL = " insert into ocs_mst_tdisbursementbankaccountapprovalconfig(" +
                    " disbursementbankaccountapprovalconfig_gid ," +
                    " disbursementbankaccount_gid ," +
                    " vertical_gid," +
                    " group_gid," +
                    " group_name," +
                    " subgroup_gid ," +
                    " subgroup_name ," +
                    " manager_gid," +
                    " manager_name," +
                    " member_gid," +
                    " member_name," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.disbursementbankaccount_gid + "'," +
                    "'" + values.vertical_gid + "'," +
                    "'" + values.group_gid + "'," +
                    "'" + values.group_name + "'," +
                    "'" + values.subgroup_gid + "'," +
                    "'" + values.subgroup_name + "'," +
                    "'" + values.manager_gid + "'," +
                    "'" + values.manager_name + "'," +
                    "'" + values.member_gid + "'," +
                    "'" + values.member_name + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Approval Config Added successfully";
            }
            else
            {
                values.message = "Error Occured while Adding";
                values.status = false;
            }

        }
        public void DaGetDisbursementBankAccountView(string disbursementbankaccount_gid, MdlDisbursementBankAccount values)
        {
            try
            {
                msSQL = " select disbursementbankaccount_gid, wef_date,customer_type,disbursementbankaccount_status " +
                        " from ocs_mst_tdisbursementbankaccount a" +
                        " where disbursementbankaccount_gid ='" + disbursementbankaccount_gid + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows)
                {
                    values.disbursementbankaccount_gid = objODBCDataReader["disbursementbankaccount_gid"].ToString();
                    values.wef_date = objODBCDataReader["wef_date"].ToString();
                    values.customer_type = objODBCDataReader["customer_type"].ToString();
                    values.disbursementbankaccount_status = objODBCDataReader["disbursementbankaccount_status"].ToString();
                }
                objODBCDataReader.Close();
            }
            catch (Exception ex)
            {
                values.message = ex.ToString();
                values.status = false;
            }
        }
        public void DaPostDisbursementBankAccountInactive(MdlDisbursementBankAccount values, string employee_gid)
        {
            if (values.remarks == null || values.remarks == "")
            {
                lsremarks = "";
            }
            else
            {
                lsremarks = values.remarks.Replace("'", "\\'");
            }
            msSQL = " update ocs_mst_tdisbursementbankaccount set disbursementbankaccount_status ='" + values.disbursementbankaccount_status + "'," +
                    " remarks='" + lsremarks + "'" +
                    " where disbursementbankaccount_gid='" + values.disbursementbankaccount_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("DBAI");

                msSQL = " insert into ocs_mst_tdisbursementbankaccountinactivelog (" +
                      " disbursementbankaccountinactivelog_gid, " +
                      " disbursementbankaccount_gid," +
                      " wef_date ," +
                      " customer_type ," +
                      " vertical_gid," +
                      " disbursementbankaccount_status," +
                      " remarks, " +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.disbursementbankaccount_gid + "'," +
                      " '" + Convert.ToDateTime(values.wef_date).ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                      " '" + values.customer_type + "'," +
                      " '" + values.vertical_gid + "'," +
                      " '" + values.disbursementbankaccount_status + "'," +
                      " '" + lsremarks + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.disbursementbankaccount_status == "N")
                {
                    values.status = true;
                    values.message = "Disbursement Bank Account Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = " Disbursement Bank Account  Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Changing Status";
            }
        }
        public void DaGetDisbursementbankAccountInactiveLogview(string disbursementbankaccount_gid, MdlDisbursementBankAccount values)
        {
            try
            {
                msSQL = " Select disbursementbankaccountinactivelog_gid,disbursementbankaccount_gid," +
                        " date_format(a.wef_date,'%d-%m-%Y %h:%i %p') as wef_date,customer_type,vertical_gid,a.remarks, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                        " date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                        " case when a.disbursementbankaccount_status='N' then 'Inactive' else 'Active' end as disbursementbankaccount_status" +
                        " from ocs_mst_tdisbursementbankaccountinactivelog a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where disbursementbankaccount_gid ='" + disbursementbankaccount_gid + "'" +
                        " order by a.disbursementbankaccountinactivelog_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getdisbursementbankaccountlog_list = new List<disbursementbankaccountlog_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getdisbursementbankaccountlog_list.Add(new disbursementbankaccountlog_list
                        {
                            disbursementbankaccountinactivelog_gid = (dr_datarow["disbursementbankaccountinactivelog_gid"].ToString()),
                            disbursementbankaccount_gid = (dr_datarow["disbursementbankaccount_gid"].ToString()),
                            wef_date = (dr_datarow["wef_date"].ToString()),
                            customer_type = (dr_datarow["customer_type"].ToString()),
                            vertical_gid = (dr_datarow["vertical_gid"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            disbursementbankaccount_status = (dr_datarow["disbursementbankaccount_status"].ToString()),
                        });
                    }
                    values.disbursementbankaccountlog_list = getdisbursementbankaccountlog_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }
        public void DaGetDisbursementBankAccountEdit(string disbursementbankaccountapprovalconfig_gid, MdlDisbursementBankAccount values)
        {
            try
            {
                msSQL = " select disbursementbankaccountapprovalconfig_gid,disbursementbankaccount_gid,vertical_gid,group_gid,group_name, " +
                        " subgroup_gid,subgroup_name,manager_gid,manager_name,member_gid,member_name" +
                        " from ocs_mst_tdisbursementbankaccountapprovalconfig a " +
                        " where a.disbursementbankaccountapprovalconfig_gid='" + disbursementbankaccountapprovalconfig_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.disbursementbankaccountapprovalconfig_gid = objODBCDatareader["disbursementbankaccountapprovalconfig_gid"].ToString();
                    values.disbursementbankaccount_gid = objODBCDatareader["disbursementbankaccount_gid"].ToString();
                    values.vertical_gid = objODBCDatareader["vertical_gid"].ToString();
                    values.group_gid = objODBCDatareader["group_gid"].ToString();
                    values.group_name = objODBCDatareader["group_name"].ToString();
                    values.subgroup_gid = objODBCDatareader["subgroup_gid"].ToString();
                    values.subgroup_name = objODBCDatareader["subgroup_name"].ToString();
                    values.manager_gid = objODBCDatareader["manager_gid"].ToString();
                    values.manager_name = objODBCDatareader["manager_name"].ToString();
                    values.member_gid = objODBCDatareader["member_gid"].ToString();
                    values.member_name = objODBCDatareader["member_name"].ToString();
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
        public bool DaPostDisbursementBankAccountUpdate(string employee_gid, MdlDisbursementBankAccount values)
        {
            msSQL = " select disbursementbankaccountapprovalconfig_gid,disbursementbankaccount_gid,vertical_gid,group_gid,group_name, " +
                        " subgroup_gid,subgroup_name,manager_gid,manager_name,member_gid,member_name" +
                        " from ocs_mst_tdisbursementbankaccountapprovalconfig a " +
                        " where a.disbursementbankaccountapprovalconfig_gid='" + values.disbursementbankaccountapprovalconfig_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                lsdisbursementbankaccountapprovalconfig_gid = objODBCDatareader["disbursementbankaccountapprovalconfig_gid"].ToString();
                lsdisbursementbankaccount_gid = objODBCDatareader["disbursementbankaccount_gid"].ToString();
                lsvertical_gid = objODBCDatareader["vertical_gid"].ToString();
                lsgroup_gid = objODBCDatareader["group_gid"].ToString();
                lsgroup_name = objODBCDatareader["group_name"].ToString();
                lssubgroup_gid = objODBCDatareader["subgroup_gid"].ToString();
                lssubgroup_name = objODBCDatareader["subgroup_name"].ToString();
                lsmanager_gid = objODBCDatareader["manager_gid"].ToString();
                lsmanager_name = objODBCDatareader["manager_name"].ToString();
                lsmember_gid = objODBCDatareader["member_gid"].ToString();
                lsmember_name = objODBCDatareader["member_name"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " update ocs_mst_tdisbursementbankaccountapprovalconfig set " +
                    " group_gid='" + values.group_gid + "'," +
                    " group_name = '" + values.group_name + "'," +
                    " subgroup_gid = '" + values.subgroup_gid + "'," +
                    " subgroup_name = '" + values.subgroup_name + "'," +
                    " manager_gid = '" + values.manager_gid + "'," +
                    " manager_name='" + values.manager_name + "'," +
                    " member_gid = '" + values.member_gid + "'," +
                    " member_name = '" + values.member_name + "'," +
                    " disbursementbankaccount_gid = '" + lsdisbursementbankaccount_gid + "'," +
                    " vertical_gid='" + values.vertical_gid + "'," +
                    " updated_by = '" + employee_gid + "'," +
                    " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    " where disbursementbankaccountapprovalconfig_gid ='" + values.disbursementbankaccountapprovalconfig_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("DBAL");
                msSQL = " insert into ocs_mst_tdisbursementbankaccountapprovalconfiglog(" +
                        " disbursementbankaccountapprovalconfiglog_gid ," +
                        " disbursementbankaccountapprovalconfig_gid," +
                        " disbursementbankaccount_gid," +
                        " vertical_gid," +
                        " group_gid," +
                        " group_name," +
                        " subgroup_gid," +
                        " subgroup_name," +
                        " manager_gid," +
                        " manager_name," +
                        " member_gid," +
                        " member_name," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + lsdisbursementbankaccountapprovalconfig_gid + "'," +
                        "'" + lsdisbursementbankaccount_gid + "'," +
                        "'" + lsvertical_gid + "'," +
                        "'" + lsgroup_gid + "'," +
                        "'" + lsgroup_name + "'," +
                        "'" + lssubgroup_gid + "'," +
                        "'" + lssubgroup_name + "'," +
                        "'" + lsmanager_gid + "'," +
                        "'" + lsmanager_name + "'," +
                        "'" + lsmember_gid + "'," +
                        "'" + lsmember_name + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Disbursement Bank Account Details Updated Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while updating Disbursement Bank Account Details";
                return false;
            }
        }
        public void DaGetDisbursementODBelow30Summary(string employee_gid, string vertical_gid, MdlDisbursementODBelow30 values)
        {
            try
            {
                msSQL = " select disbursementodbelow30_gid,vertical_gid, customer_type, disbursementodbelow30_status," +
                        " date_format(a.wef_date, '%d-%m-%Y %h:%i %p') as wef_date, " +
                         " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as created_by, " +
                         " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date " +
                         " from ocs_mst_tdisbursementodbelow30 a" +
                         " left join hrm_mst_temployee b on b.employee_gid = a.created_by " +
                         " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                         " where vertical_gid='" + vertical_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getdisbursementodbelow30_list = new List<disbursementodbelow30_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getdisbursementodbelow30_list.Add(new disbursementodbelow30_list
                        {
                            disbursementodbelow30_gid = dt["disbursementodbelow30_gid"].ToString(),
                            vertical_gid = dt["vertical_gid"].ToString(),
                            customer_type = dt["customer_type"].ToString(),
                            disbursementodbelow30_status = dt["disbursementodbelow30_status"].ToString(),
                            wef_date = dt["wef_date"].ToString(),
                            created_by = dt["created_by"].ToString(),
                            created_date = dt["created_date"].ToString(),

                        });
                        values.disbursementodbelow30_list = getdisbursementodbelow30_list;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = ex.ToString();
                values.status = false;
            }

        }
        public void DaGetDisbursementODBelow30ApprovalConfigSummary(string employee_gid, string vertical_gid, MdlDisbursementODBelow30 values)
        {
            try
            {
                msSQL = " select a.disbursementodbelow30approvalconfig_gid,d.disbursementodbelow30_gid, d.vertical_gid, group_gid," +
                        " group_name,subgroup_gid,subgroup_name,manager_gid,manager_name,member_gid,member_name, " +
                        " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as created_by, " +
                        " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date, " +
                        " disbursementodbelow30_status,date_format(d.wef_date, '%d-%m-%Y %h:%i %p') as wef_date,d.customer_type " +
                        " from ocs_mst_tdisbursementodbelow30approvalconfig a" +
                        " left join hrm_mst_temployee b on b.employee_gid = a.created_by " +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " left join ocs_mst_tdisbursementodbelow30 d on d.disbursementodbelow30_gid = a.disbursementodbelow30_gid " +
                        " where d.vertical_gid='" + vertical_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getdisbursementodbelow30approvalconfig_list = new List<disbursementodbelow30approvalconfig_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getdisbursementodbelow30approvalconfig_list.Add(new disbursementodbelow30approvalconfig_list
                        {
                            disbursementodbelow30approvalconfig_gid = dt["disbursementodbelow30approvalconfig_gid"].ToString(),
                            disbursementodbelow30_gid = dt["disbursementodbelow30_gid"].ToString(),
                            vertical_gid = dt["vertical_gid"].ToString(),
                            group_gid = dt["group_gid"].ToString(),
                            group_name = dt["group_name"].ToString(),
                            subgroup_gid = dt["subgroup_gid"].ToString(),
                            subgroup_name = dt["subgroup_name"].ToString(),
                            manager_gid = dt["manager_gid"].ToString(),
                            manager_name = dt["manager_name"].ToString(),
                            member_gid = dt["member_gid"].ToString(),
                            member_name = dt["member_name"].ToString(),
                            created_by = dt["created_by"].ToString(),
                            created_date = dt["created_date"].ToString(),
                            wef_date = dt["wef_date"].ToString(),
                            customer_type = dt["customer_type"].ToString(),
                            disbursementodbelow30_status = dt["disbursementodbelow30_status"].ToString()
                        });
                        values.disbursementodbelow30approvalconfig_list = getdisbursementodbelow30approvalconfig_list;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = ex.ToString();
                values.status = false;
            }
        }
        public void DaPostDisbursementODBelow30(MdlDisbursementODBelow30 values, string employee_gid)
        {
            msSQL = " select disbursementodbelow30_gid from ocs_mst_tdisbursementodbelow30 a " +
                    " where a.vertical_gid = '" + values.vertical_gid + "' and " +
                    " a.customer_type = '" + values.customer_type + "' and a.disbursementodbelow30_status='Y'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.message = "This Customer Type already Added.Kindly, Inactive that and again add the Customer Type";
                values.status = false;
                return;
            }
            else
            {
                lswefdate = values.wef_date;
                lswefdatetime = GetDateFormat(lswefdate);

                msGetGid = objcmnfunctions.GetMasterGID("DODB");
                msSQL = " insert into ocs_mst_tdisbursementodbelow30(" +
                        " disbursementodbelow30_gid ," +
                        " vertical_gid ," +
                        " wef_date," +
                        " customer_type," +
                        " disbursementodbelow30_status," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + values.vertical_gid + "'," +
                        "'" + lswefdatetime + "'," +
                        "'" + values.customer_type + "'," +
                        "'Y'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Disbursement OD Below30 Details Added successfully";
                }
                else
                {
                    values.message = "Error Occured while Adding";
                    values.status = false;
                }
            }
        }
        public void DaPostDisbursementODBelow30ApprovalConfig(MdlDisbursementODBelow30 values, string employee_gid)
        {
            msGetGid = objcmnfunctions.GetMasterGID("DODA");
            msSQL = " insert into ocs_mst_tdisbursementodbelow30approvalconfig(" +
                    " disbursementodbelow30approvalconfig_gid ," +
                    " disbursementodbelow30_gid ," +
                    " vertical_gid," +
                    " group_gid," +
                    " group_name," +
                    " subgroup_gid ," +
                    " subgroup_name ," +
                    " manager_gid," +
                    " manager_name," +
                    " member_gid," +
                    " member_name," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.disbursementodbelow30_gid + "'," +
                    "'" + values.vertical_gid + "'," +
                    "'" + values.group_gid + "'," +
                    "'" + values.group_name + "'," +
                    "'" + values.subgroup_gid + "'," +
                    "'" + values.subgroup_name + "'," +
                    "'" + values.manager_gid + "'," +
                    "'" + values.manager_name + "'," +
                    "'" + values.member_gid + "'," +
                    "'" + values.member_name + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Approval Config Added successfully";
            }
            else
            {
                values.message = "Error Occured while Adding";
                values.status = false;
            }

        }
        public void DaGetDisbursementODBelow30View(string disbursementodbelow30_gid, MdlDisbursementODBelow30 values)
        {
            try
            {
                msSQL = " select disbursementodbelow30_gid, wef_date,customer_type,disbursementodbelow30_status " +
                        " from ocs_mst_tdisbursementodbelow30 a" +
                        " where disbursementodbelow30_gid ='" + disbursementodbelow30_gid + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows)
                {
                    values.disbursementodbelow30_gid = objODBCDataReader["disbursementodbelow30_gid"].ToString();
                    values.wef_date = objODBCDataReader["wef_date"].ToString();
                    values.customer_type = objODBCDataReader["customer_type"].ToString();
                    values.disbursementodbelow30_status = objODBCDataReader["disbursementodbelow30_status"].ToString();
                }
                objODBCDataReader.Close();
            }
            catch (Exception ex)
            {
                values.message = ex.ToString();
                values.status = false;
            }
        }
        public void DaPostDisbursementODBelow30Inactive(MdlDisbursementODBelow30 values, string employee_gid)
        {
            if (values.remarks == null || values.remarks == "")
            {
                lsremarks = "";
            }
            else
            {
                lsremarks = values.remarks.Replace("'", "\\'");
            }
            msSQL = " update ocs_mst_tdisbursementodbelow30 set disbursementodbelow30_status ='" + values.disbursementodbelow30_status + "'," +
                    " remarks='" + lsremarks + "'" +
                    " where disbursementodbelow30_gid='" + values.disbursementodbelow30_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("DODI");

                msSQL = " insert into ocs_mst_tdisbursementodbelow30inactivelog (" +
                      " disbursementodbelow30inactivelog_gid, " +
                      " disbursementodbelow30_gid," +
                      " wef_date ," +
                      " customer_type ," +
                      " vertical_gid," +
                      " disbursementodbelow30_status," +
                      " remarks, " +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.disbursementodbelow30_gid + "'," +
                      " '" + Convert.ToDateTime(values.wef_date).ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                      " '" + values.customer_type + "'," +
                      " '" + values.vertical_gid + "'," +
                      " '" + values.disbursementodbelow30_status + "'," +
                      " '" + lsremarks + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.disbursementodbelow30_status == "N")
                {
                    values.status = true;
                    values.message = "Disbursement OD Below30 Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = " Disbursement OD Below30  Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Changing Status";
            }
        }
        public void DaGetDisbursementODBelow30InactiveLogview(string disbursementodbelow30_gid, MdlDisbursementODBelow30 values)
        {
            try
            {
                msSQL = " Select disbursementodbelow30inactivelog_gid,disbursementodbelow30_gid," +
                        " date_format(a.wef_date,'%d-%m-%Y %h:%i %p') as wef_date,customer_type,vertical_gid,a.remarks, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                        " date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                        " case when a.disbursementodbelow30_status='N' then 'Inactive' else 'Active' end as disbursementodbelow30_status" +
                        " from ocs_mst_tdisbursementodbelow30inactivelog a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where disbursementodbelow30_gid ='" + disbursementodbelow30_gid + "'" +
                        " order by a.disbursementodbelow30inactivelog_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getdisbursementodbelow30log_list = new List<disbursementodbelow30log_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getdisbursementodbelow30log_list.Add(new disbursementodbelow30log_list
                        {
                            disbursementodbelow30inactivelog_gid = (dr_datarow["disbursementodbelow30inactivelog_gid"].ToString()),
                            disbursementodbelow30_gid = (dr_datarow["disbursementodbelow30_gid"].ToString()),
                            wef_date = (dr_datarow["wef_date"].ToString()),
                            customer_type = (dr_datarow["customer_type"].ToString()),
                            vertical_gid = (dr_datarow["vertical_gid"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            disbursementodbelow30_status = (dr_datarow["disbursementodbelow30_status"].ToString()),
                        });
                    }
                    values.disbursementodbelow30log_list = getdisbursementodbelow30log_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }
        public void DaGetDisbursementODBelow30Edit(string disbursementodbelow30approvalconfig_gid, MdlDisbursementODBelow30 values)
        {
            try
            {
                msSQL = " select disbursementodbelow30approvalconfig_gid,disbursementodbelow30_gid,vertical_gid,group_gid,group_name, " +
                        " subgroup_gid,subgroup_name,manager_gid,manager_name,member_gid,member_name" +
                        " from ocs_mst_tdisbursementodbelow30approvalconfig a " +
                        " where a.disbursementodbelow30approvalconfig_gid='" + disbursementodbelow30approvalconfig_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.disbursementodbelow30approvalconfig_gid = objODBCDatareader["disbursementodbelow30approvalconfig_gid"].ToString();
                    values.disbursementodbelow30_gid = objODBCDatareader["disbursementodbelow30_gid"].ToString();
                    values.vertical_gid = objODBCDatareader["vertical_gid"].ToString();
                    values.group_gid = objODBCDatareader["group_gid"].ToString();
                    values.group_name = objODBCDatareader["group_name"].ToString();
                    values.subgroup_gid = objODBCDatareader["subgroup_gid"].ToString();
                    values.subgroup_name = objODBCDatareader["subgroup_name"].ToString();
                    values.manager_gid = objODBCDatareader["manager_gid"].ToString();
                    values.manager_name = objODBCDatareader["manager_name"].ToString();
                    values.member_gid = objODBCDatareader["member_gid"].ToString();
                    values.member_name = objODBCDatareader["member_name"].ToString();
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
        public bool DaPostDisbursementODBelow30Update(string employee_gid, MdlDisbursementODBelow30 values)
        {
            msSQL = " select disbursementodbelow30approvalconfig_gid,disbursementodbelow30_gid,vertical_gid,group_gid,group_name, " +
                        " subgroup_gid,subgroup_name,manager_gid,manager_name,member_gid,member_name" +
                        " from ocs_mst_tdisbursementodbelow30approvalconfig a " +
                        " where a.disbursementodbelow30approvalconfig_gid='" + values.disbursementodbelow30approvalconfig_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                lsdisbursementodbelow30approvalconfig_gid = objODBCDatareader["disbursementodbelow30approvalconfig_gid"].ToString();
                lsdisbursementodbelow30_gid = objODBCDatareader["disbursementodbelow30_gid"].ToString();
                lsvertical_gid = objODBCDatareader["vertical_gid"].ToString();
                lsgroup_gid = objODBCDatareader["group_gid"].ToString();
                lsgroup_name = objODBCDatareader["group_name"].ToString();
                lssubgroup_gid = objODBCDatareader["subgroup_gid"].ToString();
                lssubgroup_name = objODBCDatareader["subgroup_name"].ToString();
                lsmanager_gid = objODBCDatareader["manager_gid"].ToString();
                lsmanager_name = objODBCDatareader["manager_name"].ToString();
                lsmember_gid = objODBCDatareader["member_gid"].ToString();
                lsmember_name = objODBCDatareader["member_name"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " update ocs_mst_tdisbursementodbelow30approvalconfig set " +
                    " group_gid='" + values.group_gid + "'," +
                    " group_name = '" + values.group_name + "'," +
                    " subgroup_gid = '" + values.subgroup_gid + "'," +
                    " subgroup_name = '" + values.subgroup_name + "'," +
                    " manager_gid = '" + values.manager_gid + "'," +
                    " manager_name='" + values.manager_name + "'," +
                    " member_gid = '" + values.member_gid + "'," +
                    " member_name = '" + values.member_name + "'," +
                    " disbursementodbelow30_gid = '" + lsdisbursementodbelow30_gid + "'," +
                    " vertical_gid='" + values.vertical_gid + "'," +
                    " updated_by = '" + employee_gid + "'," +
                    " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    " where disbursementodbelow30approvalconfig_gid ='" + values.disbursementodbelow30approvalconfig_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("DODL");
                msSQL = " insert into ocs_mst_tdisbursementodbelow30approvalconfiglog(" +
                        " disbursementodbelow30approvalconfiglog_gid ," +
                        " disbursementodbelow30approvalconfig_gid," +
                        " disbursementodbelow30_gid," +
                        " vertical_gid," +
                        " group_gid," +
                        " group_name," +
                        " subgroup_gid," +
                        " subgroup_name," +
                        " manager_gid," +
                        " manager_name," +
                        " member_gid," +
                        " member_name," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + lsdisbursementodbelow30approvalconfig_gid + "'," +
                        "'" + lsdisbursementodbelow30_gid + "'," +
                        "'" + lsvertical_gid + "'," +
                        "'" + lsgroup_gid + "'," +
                        "'" + lsgroup_name + "'," +
                        "'" + lssubgroup_gid + "'," +
                        "'" + lssubgroup_name + "'," +
                        "'" + lsmanager_gid + "'," +
                        "'" + lsmanager_name + "'," +
                        "'" + lsmember_gid + "'," +
                        "'" + lsmember_name + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Disbursement OD Below30 Details Updated Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while updating Disbursement OD below30 Details";
                return false;
            }
        }
        public void DaGetDisbursementODBelow90Summary(string employee_gid, string vertical_gid, MdlDisbursementODBelow90 values)
        {
            try
            {
                msSQL = " select disbursementodbelow90_gid,vertical_gid, customer_type, disbursementodbelow90_status," +
                        " date_format(a.wef_date, '%d-%m-%Y %h:%i %p') as wef_date, " +
                         " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as created_by, " +
                         " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date " +
                         " from ocs_mst_tdisbursementodbelow90 a" +
                         " left join hrm_mst_temployee b on b.employee_gid = a.created_by " +
                         " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                         " where vertical_gid='" + vertical_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getdisbursementodbelow90_list = new List<disbursementodbelow90_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getdisbursementodbelow90_list.Add(new disbursementodbelow90_list
                        {
                            disbursementodbelow90_gid = dt["disbursementodbelow90_gid"].ToString(),
                            vertical_gid = dt["vertical_gid"].ToString(),
                            customer_type = dt["customer_type"].ToString(),
                            disbursementodbelow90_status = dt["disbursementodbelow90_status"].ToString(),
                            wef_date = dt["wef_date"].ToString(),
                            created_by = dt["created_by"].ToString(),
                            created_date = dt["created_date"].ToString(),

                        });
                        values.disbursementodbelow90_list = getdisbursementodbelow90_list;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = ex.ToString();
                values.status = false;
            }

        }
        public void DaGetDisbursementODBelow90ApprovalConfigSummary(string employee_gid, string vertical_gid, MdlDisbursementODBelow90 values)
        {
            try
            {
                msSQL = " select a.disbursementodbelow90approvalconfig_gid,d.disbursementodbelow90_gid, d.vertical_gid, group_gid," +
                        " group_name,subgroup_gid,subgroup_name,manager_gid,manager_name,member_gid,member_name, " +
                        " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as created_by, " +
                        " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date, " +
                        " disbursementodbelow90_status,date_format(d.wef_date, '%d-%m-%Y %h:%i %p') as wef_date,d.customer_type " +
                        " from ocs_mst_tdisbursementodbelow90approvalconfig a" +
                        " left join hrm_mst_temployee b on b.employee_gid = a.created_by " +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " left join ocs_mst_tdisbursementodbelow90 d on d.disbursementodbelow90_gid = a.disbursementodbelow90_gid " +
                        " where d.vertical_gid='" + vertical_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getdisbursementodbelow90approvalconfig_list = new List<disbursementodbelow90approvalconfig_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getdisbursementodbelow90approvalconfig_list.Add(new disbursementodbelow90approvalconfig_list
                        {
                            disbursementodbelow90approvalconfig_gid = dt["disbursementodbelow90approvalconfig_gid"].ToString(),
                            disbursementodbelow90_gid = dt["disbursementodbelow90_gid"].ToString(),
                            vertical_gid = dt["vertical_gid"].ToString(),
                            group_gid = dt["group_gid"].ToString(),
                            group_name = dt["group_name"].ToString(),
                            subgroup_gid = dt["subgroup_gid"].ToString(),
                            subgroup_name = dt["subgroup_name"].ToString(),
                            manager_gid = dt["manager_gid"].ToString(),
                            manager_name = dt["manager_name"].ToString(),
                            member_gid = dt["member_gid"].ToString(),
                            member_name = dt["member_name"].ToString(),
                            created_by = dt["created_by"].ToString(),
                            created_date = dt["created_date"].ToString(),
                            wef_date = dt["wef_date"].ToString(),
                            customer_type = dt["customer_type"].ToString(),
                            disbursementodbelow90_status = dt["disbursementodbelow90_status"].ToString()
                        });
                        values.disbursementodbelow90approvalconfig_list = getdisbursementodbelow90approvalconfig_list;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = ex.ToString();
                values.status = false;
            }
        }
        public void DaPostDisbursementODBelow90(MdlDisbursementODBelow90 values, string employee_gid)
        {
            msSQL = " select disbursementodbelow90_gid from ocs_mst_tdisbursementodbelow90 a " +
                    " where a.vertical_gid = '" + values.vertical_gid + "' and " +
                    " a.customer_type = '" + values.customer_type + "' and a.disbursementodbelow90_status='Y'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.message = "This Customer Type already Added.Kindly, Inactive that and again add the Customer Type";
                values.status = false;
                return;
            }
            else
            {
                lswefdate = values.wef_date;
                lswefdatetime = GetDateFormat(lswefdate);

                msGetGid = objcmnfunctions.GetMasterGID("DIOD");
                msSQL = " insert into ocs_mst_tdisbursementodbelow90(" +
                        " disbursementodbelow90_gid ," +
                        " vertical_gid ," +
                        " wef_date," +
                        " customer_type," +
                        " disbursementodbelow90_status," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + values.vertical_gid + "'," +
                        "'" + lswefdatetime + "'," +
                        "'" + values.customer_type + "'," +
                        "'Y'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Disbursement OD Below90 Details Added successfully";
                }
                else
                {
                    values.message = "Error Occured while Adding";
                    values.status = false;
                }
            }
        }
        public void DaPostDisbursementODBelow90ApprovalConfig(MdlDisbursementODBelow90 values, string employee_gid)
        {
            msGetGid = objcmnfunctions.GetMasterGID("DIOA");
            msSQL = " insert into ocs_mst_tdisbursementodbelow90approvalconfig(" +
                    " disbursementodbelow90approvalconfig_gid ," +
                    " disbursementodbelow90_gid ," +
                    " vertical_gid," +
                    " group_gid," +
                    " group_name," +
                    " subgroup_gid ," +
                    " subgroup_name ," +
                    " manager_gid," +
                    " manager_name," +
                    " member_gid," +
                    " member_name," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.disbursementodbelow90_gid + "'," +
                    "'" + values.vertical_gid + "'," +
                    "'" + values.group_gid + "'," +
                    "'" + values.group_name + "'," +
                    "'" + values.subgroup_gid + "'," +
                    "'" + values.subgroup_name + "'," +
                    "'" + values.manager_gid + "'," +
                    "'" + values.manager_name + "'," +
                    "'" + values.member_gid + "'," +
                    "'" + values.member_name + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Approval Config Added successfully";
            }
            else
            {
                values.message = "Error Occured while Adding";
                values.status = false;
            }

        }
        public void DaGetDisbursementODBelow90View(string disbursementodbelow90_gid, MdlDisbursementODBelow90 values)
        {
            try
            {
                msSQL = " select disbursementodbelow90_gid, wef_date,customer_type,disbursementodbelow90_status " +
                        " from ocs_mst_tdisbursementodbelow90 a" +
                        " where disbursementodbelow90_gid ='" + disbursementodbelow90_gid + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows)
                {
                    values.disbursementodbelow90_gid = objODBCDataReader["disbursementodbelow90_gid"].ToString();
                    values.wef_date = objODBCDataReader["wef_date"].ToString();
                    values.customer_type = objODBCDataReader["customer_type"].ToString();
                    values.disbursementodbelow90_status = objODBCDataReader["disbursementodbelow90_status"].ToString();
                }
                objODBCDataReader.Close();
            }
            catch (Exception ex)
            {
                values.message = ex.ToString();
                values.status = false;
            }
        }
        public void DaPostDisbursementODBelow90Inactive(MdlDisbursementODBelow90 values, string employee_gid)
        {
            if (values.remarks == null || values.remarks == "")
            {
                lsremarks = "";
            }
            else
            {
                lsremarks = values.remarks.Replace("'", "\\'");
            }
            msSQL = " update ocs_mst_tdisbursementodbelow90 set disbursementodbelow90_status ='" + values.disbursementodbelow90_status + "'," +
                    " remarks='" + lsremarks + "'" +
                    " where disbursementodbelow90_gid='" + values.disbursementodbelow90_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("DIOI");

                msSQL = " insert into ocs_mst_tdisbursementodbelow90inactivelog (" +
                      " disbursementodbelow90inactivelog_gid, " +
                      " disbursementodbelow90_gid," +
                      " wef_date ," +
                      " customer_type ," +
                      " vertical_gid," +
                      " disbursementodbelow90_status," +
                      " remarks, " +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.disbursementodbelow90_gid + "'," +
                      " '" + Convert.ToDateTime(values.wef_date).ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                      " '" + values.customer_type + "'," +
                      " '" + values.vertical_gid + "'," +
                      " '" + values.disbursementodbelow90_status + "'," +
                      " '" + lsremarks + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.disbursementodbelow90_status == "N")
                {
                    values.status = true;
                    values.message = "Disbursement OD Below90 Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = " Disbursement OD Below90  Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Changing Status";
            }
        }
        public void DaGetDisbursementODBelow90InactiveLogview(string disbursementodbelow90_gid, MdlDisbursementODBelow90 values)
        {
            try
            {
                msSQL = " Select disbursementodbelow90inactivelog_gid,disbursementodbelow90_gid," +
                        " date_format(a.wef_date,'%d-%m-%Y %h:%i %p') as wef_date,customer_type,vertical_gid,a.remarks, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                        " date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                        " case when a.disbursementodbelow90_status='N' then 'Inactive' else 'Active' end as disbursementodbelow90_status" +
                        " from ocs_mst_tdisbursementodbelow90inactivelog a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where disbursementodbelow90_gid ='" + disbursementodbelow90_gid + "'" +
                        " order by a.disbursementodbelow90inactivelog_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getdisbursementodbelow90log_list = new List<disbursementodbelow90log_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getdisbursementodbelow90log_list.Add(new disbursementodbelow90log_list
                        {
                            disbursementodbelow90inactivelog_gid = (dr_datarow["disbursementodbelow90inactivelog_gid"].ToString()),
                            disbursementodbelow90_gid = (dr_datarow["disbursementodbelow90_gid"].ToString()),
                            wef_date = (dr_datarow["wef_date"].ToString()),
                            customer_type = (dr_datarow["customer_type"].ToString()),
                            vertical_gid = (dr_datarow["vertical_gid"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            disbursementodbelow90_status = (dr_datarow["disbursementodbelow90_status"].ToString()),
                        });
                    }
                    values.disbursementodbelow90log_list = getdisbursementodbelow90log_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }
        public void DaGetDisbursementODBelow90Edit(string disbursementodbelow90approvalconfig_gid, MdlDisbursementODBelow90 values)
        {
            try
            {
                msSQL = " select disbursementodbelow90approvalconfig_gid,disbursementodbelow90_gid,vertical_gid,group_gid,group_name, " +
                        " subgroup_gid,subgroup_name,manager_gid,manager_name,member_gid,member_name" +
                        " from ocs_mst_tdisbursementodbelow90approvalconfig a " +
                        " where a.disbursementodbelow90approvalconfig_gid='" + disbursementodbelow90approvalconfig_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.disbursementodbelow90approvalconfig_gid = objODBCDatareader["disbursementodbelow90approvalconfig_gid"].ToString();
                    values.disbursementodbelow90_gid = objODBCDatareader["disbursementodbelow90_gid"].ToString();
                    values.vertical_gid = objODBCDatareader["vertical_gid"].ToString();
                    values.group_gid = objODBCDatareader["group_gid"].ToString();
                    values.group_name = objODBCDatareader["group_name"].ToString();
                    values.subgroup_gid = objODBCDatareader["subgroup_gid"].ToString();
                    values.subgroup_name = objODBCDatareader["subgroup_name"].ToString();
                    values.manager_gid = objODBCDatareader["manager_gid"].ToString();
                    values.manager_name = objODBCDatareader["manager_name"].ToString();
                    values.member_gid = objODBCDatareader["member_gid"].ToString();
                    values.member_name = objODBCDatareader["member_name"].ToString();
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
        public bool DaPostDisbursementODBelow90Update(string employee_gid, MdlDisbursementODBelow90 values)
        {
            msSQL = " select disbursementodbelow90approvalconfig_gid,disbursementodbelow90_gid,vertical_gid,group_gid,group_name, " +
                        " subgroup_gid,subgroup_name,manager_gid,manager_name,member_gid,member_name" +
                        " from ocs_mst_tdisbursementodbelow90approvalconfig a " +
                        " where a.disbursementodbelow90approvalconfig_gid='" + values.disbursementodbelow90approvalconfig_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                lsdisbursementodbelow90approvalconfig_gid = objODBCDatareader["disbursementodbelow90approvalconfig_gid"].ToString();
                lsdisbursementodbelow90_gid = objODBCDatareader["disbursementodbelow90_gid"].ToString();
                lsvertical_gid = objODBCDatareader["vertical_gid"].ToString();
                lsgroup_gid = objODBCDatareader["group_gid"].ToString();
                lsgroup_name = objODBCDatareader["group_name"].ToString();
                lssubgroup_gid = objODBCDatareader["subgroup_gid"].ToString();
                lssubgroup_name = objODBCDatareader["subgroup_name"].ToString();
                lsmanager_gid = objODBCDatareader["manager_gid"].ToString();
                lsmanager_name = objODBCDatareader["manager_name"].ToString();
                lsmember_gid = objODBCDatareader["member_gid"].ToString();
                lsmember_name = objODBCDatareader["member_name"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " update ocs_mst_tdisbursementodbelow90approvalconfig set " +
                    " group_gid='" + values.group_gid + "'," +
                    " group_name = '" + values.group_name + "'," +
                    " subgroup_gid = '" + values.subgroup_gid + "'," +
                    " subgroup_name = '" + values.subgroup_name + "'," +
                    " manager_gid = '" + values.manager_gid + "'," +
                    " manager_name='" + values.manager_name + "'," +
                    " member_gid = '" + values.member_gid + "'," +
                    " member_name = '" + values.member_name + "'," +
                    " disbursementodbelow90_gid = '" + lsdisbursementodbelow90_gid + "'," +
                    " vertical_gid='" + values.vertical_gid + "'," +
                    " updated_by = '" + employee_gid + "'," +
                    " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    " where disbursementodbelow90approvalconfig_gid ='" + values.disbursementodbelow90approvalconfig_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("DIOL");
                msSQL = " insert into ocs_mst_tdisbursementodbelow90approvalconfiglog(" +
                        " disbursementodbelow90approvalconfiglog_gid ," +
                        " disbursementodbelow90approvalconfig_gid," +
                        " disbursementodbelow90_gid," +
                        " vertical_gid," +
                        " group_gid," +
                        " group_name," +
                        " subgroup_gid," +
                        " subgroup_name," +
                        " manager_gid," +
                        " manager_name," +
                        " member_gid," +
                        " member_name," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + lsdisbursementodbelow90approvalconfig_gid + "'," +
                        "'" + lsdisbursementodbelow90_gid + "'," +
                        "'" + lsvertical_gid + "'," +
                        "'" + lsgroup_gid + "'," +
                        "'" + lsgroup_name + "'," +
                        "'" + lssubgroup_gid + "'," +
                        "'" + lssubgroup_name + "'," +
                        "'" + lsmanager_gid + "'," +
                        "'" + lsmanager_name + "'," +
                        "'" + lsmember_gid + "'," +
                        "'" + lsmember_name + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Disbursement OD Below90 Details Updated Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while updating Disbursement OD below90 Details";
                return false;
            }
        }
        public void DaGetPenalWaiverSummary(string employee_gid, string vertical_gid, MdlPenalWaiver values)
        {
            try
            {
                msSQL = " select penalwaiver_gid,vertical_gid, customer_type, penalwaiver_status," +
                        " date_format(a.wef_date, '%d-%m-%Y %h:%i %p') as wef_date, " +
                         " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as created_by, " +
                         " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date " +
                         " from ocs_mst_tpenalwaiver a" +
                         " left join hrm_mst_temployee b on b.employee_gid = a.created_by " +
                         " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                         " where vertical_gid='" + vertical_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getpenalwaiver_list = new List<penalwaiver_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getpenalwaiver_list.Add(new penalwaiver_list
                        {
                            penalwaiver_gid = dt["penalwaiver_gid"].ToString(),
                            vertical_gid = dt["vertical_gid"].ToString(),
                            customer_type = dt["customer_type"].ToString(),
                            penalwaiver_status = dt["penalwaiver_status"].ToString(),
                            wef_date = dt["wef_date"].ToString(),
                            created_by = dt["created_by"].ToString(),
                            created_date = dt["created_date"].ToString(),

                        });
                        values.penalwaiver_list = getpenalwaiver_list;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = ex.ToString();
                values.status = false;
            }

        }
        public void DaGetPenalWaiverApprovalConfigSummary(string employee_gid, string vertical_gid, MdlPenalWaiver values)
        {
            try
            {
                msSQL = " select a.penalwaiverapprovalconfig_gid,d.penalwaiver_gid, d.vertical_gid, group_gid," +
                        " group_name,subgroup_gid,subgroup_name,manager_gid,manager_name,member_gid,member_name, " +
                        " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as created_by, " +
                        " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date, " +
                        " penalwaiver_status,date_format(d.wef_date, '%d-%m-%Y %h:%i %p') as wef_date,d.customer_type " +
                        " from ocs_mst_tpenalwaiverapprovalconfig a" +
                        " left join hrm_mst_temployee b on b.employee_gid = a.created_by " +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " left join ocs_mst_tpenalwaiver d on d.penalwaiver_gid = a.penalwaiver_gid " +
                        " where d.vertical_gid='" + vertical_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getpenalwaiverapprovalconfig_list = new List<penalwaiverapprovalconfig_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getpenalwaiverapprovalconfig_list.Add(new penalwaiverapprovalconfig_list
                        {
                            penalwaiverapprovalconfig_gid = dt["penalwaiverapprovalconfig_gid"].ToString(),
                            penalwaiver_gid = dt["penalwaiver_gid"].ToString(),
                            vertical_gid = dt["vertical_gid"].ToString(),
                            group_gid = dt["group_gid"].ToString(),
                            group_name = dt["group_name"].ToString(),
                            subgroup_gid = dt["subgroup_gid"].ToString(),
                            subgroup_name = dt["subgroup_name"].ToString(),
                            manager_gid = dt["manager_gid"].ToString(),
                            manager_name = dt["manager_name"].ToString(),
                            member_gid = dt["member_gid"].ToString(),
                            member_name = dt["member_name"].ToString(),
                            created_by = dt["created_by"].ToString(),
                            created_date = dt["created_date"].ToString(),
                            wef_date = dt["wef_date"].ToString(),
                            customer_type = dt["customer_type"].ToString(),
                            penalwaiver_status = dt["penalwaiver_status"].ToString()
                        });
                        values.penalwaiverapprovalconfig_list = getpenalwaiverapprovalconfig_list;
                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = ex.ToString();
                values.status = false;
            }
        }
        public void DaPostPenalWaiver(MdlPenalWaiver values, string employee_gid)
        {
            msSQL = " select penalwaiver_gid from ocs_mst_tpenalwaiver a " +
                    " where a.vertical_gid = '" + values.vertical_gid + "' and " +
                    " a.customer_type = '" + values.customer_type + "' and a.penalwaiver_status='Y'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.message = "This Customer Type already Added.Kindly, Inactive that and again add the Customer Type";
                values.status = false;
                return;
            }
            else
            {
                lswefdate = values.wef_date;
                lswefdatetime = GetDateFormat(lswefdate);

                msGetGid = objcmnfunctions.GetMasterGID("PEWA");
                msSQL = " insert into ocs_mst_tpenalwaiver(" +
                        " penalwaiver_gid ," +
                        " vertical_gid ," +
                        " wef_date," +
                        " customer_type," +
                        " penalwaiver_status," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + values.vertical_gid + "'," +
                        "'" + lswefdatetime + "'," +
                        "'" + values.customer_type + "'," +
                        "'Y'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Penal Waiver Details Added successfully";
                }
                else
                {
                    values.message = "Error Occured while Adding";
                    values.status = false;
                }
            }
        }
        public void DaPostPenalWaiverApprovalConfig(MdlPenalWaiver values, string employee_gid)
        {
            msGetGid = objcmnfunctions.GetMasterGID("PENA");
            msSQL = " insert into ocs_mst_tpenalwaiverapprovalconfig(" +
                    " penalwaiverapprovalconfig_gid ," +
                    " penalwaiver_gid ," +
                    " vertical_gid," +
                    " group_gid," +
                    " group_name," +
                    " subgroup_gid ," +
                    " subgroup_name ," +
                    " manager_gid," +
                    " manager_name," +
                    " member_gid," +
                    " member_name," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.penalwaiver_gid + "'," +
                    "'" + values.vertical_gid + "'," +
                    "'" + values.group_gid + "'," +
                    "'" + values.group_name + "'," +
                    "'" + values.subgroup_gid + "'," +
                    "'" + values.subgroup_name + "'," +
                    "'" + values.manager_gid + "'," +
                    "'" + values.manager_name + "'," +
                    "'" + values.member_gid + "'," +
                    "'" + values.member_name + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Approval Config Added successfully";
            }
            else
            {
                values.message = "Error Occured while Adding";
                values.status = false;
            }

        }
        public void DaGetPenalWaiverView(string penalwaiver_gid, MdlPenalWaiver values)
        {
            try
            {
                msSQL = " select penalwaiver_gid, wef_date,customer_type,penalwaiver_status " +
                        " from ocs_mst_tpenalwaiver a" +
                        " where penalwaiver_gid ='" + penalwaiver_gid + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows)
                {
                    values.penalwaiver_gid = objODBCDataReader["penalwaiver_gid"].ToString();
                    values.wef_date = objODBCDataReader["wef_date"].ToString();
                    values.customer_type = objODBCDataReader["customer_type"].ToString();
                    values.penalwaiver_status = objODBCDataReader["penalwaiver_status"].ToString();
                }
                objODBCDataReader.Close();
            }
            catch (Exception ex)
            {
                values.message = ex.ToString();
                values.status = false;
            }
        }
        public void DaPostPenalWaiverInactive(MdlPenalWaiver values, string employee_gid)
        {
            if (values.remarks == null || values.remarks == "")
            {
                lsremarks = "";
            }
            else
            {
                lsremarks = values.remarks.Replace("'", "\\'");
            }
            msSQL = " update ocs_mst_tpenalwaiver set penalwaiver_status ='" + values.penalwaiver_status + "'," +
                    " remarks='" + lsremarks + "'" +
                    " where penalwaiver_gid='" + values.penalwaiver_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("PEWI");

                msSQL = " insert into ocs_mst_tpenalwaiverinactivelog (" +
                      " penalwaiverinactivelog_gid, " +
                      " penalwaiver_gid," +
                      " wef_date ," +
                      " customer_type ," +
                      " vertical_gid," +
                      " penalwaiver_status," +
                      " remarks, " +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.penalwaiver_gid + "'," +
                      " '" + Convert.ToDateTime(values.wef_date).ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                      " '" + values.customer_type + "'," +
                      " '" + values.vertical_gid + "'," +
                      " '" + values.penalwaiver_status + "'," +
                      " '" + lsremarks + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.penalwaiver_status == "N")
                {
                    values.status = true;
                    values.message = "Penal Waiver Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = " Penal Waiver Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Changing Status";
            }
        }
        public void DaGetPenalWaiverInactiveLogview(string penalwaiver_gid, MdlPenalWaiver values)
        {
            try
            {
                msSQL = " Select penalwaiverinactivelog_gid,penalwaiver_gid," +
                        " date_format(a.wef_date,'%d-%m-%Y %h:%i %p') as wef_date,customer_type,vertical_gid,a.remarks, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                        " date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                        " case when a.penalwaiver_status='N' then 'Inactive' else 'Active' end as penalwaiver_status" +
                        " from ocs_mst_tpenalwaiverinactivelog a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where penalwaiver_gid ='" + penalwaiver_gid + "'" +
                        " order by a.penalwaiverinactivelog_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getpenalwaiverlog_list = new List<penalwaiverlog_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getpenalwaiverlog_list.Add(new penalwaiverlog_list
                        {
                            penalwaiverinactivelog_gid = (dr_datarow["penalwaiverinactivelog_gid"].ToString()),
                            penalwaiver_gid = (dr_datarow["penalwaiver_gid"].ToString()),
                            wef_date = (dr_datarow["wef_date"].ToString()),
                            customer_type = (dr_datarow["customer_type"].ToString()),
                            vertical_gid = (dr_datarow["vertical_gid"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            penalwaiver_status = (dr_datarow["penalwaiver_status"].ToString()),
                        });
                    }
                    values.penalwaiverlog_list = getpenalwaiverlog_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }
        public void DaGetPenalWaiverEdit(string penalwaiverapprovalconfig_gid, MdlPenalWaiver values)
        {
            try
            {
                msSQL = " select penalwaiverapprovalconfig_gid,penalwaiver_gid,vertical_gid,group_gid,group_name, " +
                        " subgroup_gid,subgroup_name,manager_gid,manager_name,member_gid,member_name" +
                        " from ocs_mst_tpenalwaiverapprovalconfig a " +
                        " where a.penalwaiverapprovalconfig_gid='" + penalwaiverapprovalconfig_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.penalwaiverapprovalconfig_gid = objODBCDatareader["penalwaiverapprovalconfig_gid"].ToString();
                    values.penalwaiver_gid = objODBCDatareader["penalwaiver_gid"].ToString();
                    values.vertical_gid = objODBCDatareader["vertical_gid"].ToString();
                    values.group_gid = objODBCDatareader["group_gid"].ToString();
                    values.group_name = objODBCDatareader["group_name"].ToString();
                    values.subgroup_gid = objODBCDatareader["subgroup_gid"].ToString();
                    values.subgroup_name = objODBCDatareader["subgroup_name"].ToString();
                    values.manager_gid = objODBCDatareader["manager_gid"].ToString();
                    values.manager_name = objODBCDatareader["manager_name"].ToString();
                    values.member_gid = objODBCDatareader["member_gid"].ToString();
                    values.member_name = objODBCDatareader["member_name"].ToString();
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
        public bool DaPostPenalWaiverUpdate(string employee_gid, MdlPenalWaiver values)
        {
            msSQL = " select penalwaiverapprovalconfig_gid,penalwaiver_gid,vertical_gid,group_gid,group_name, " +
                        " subgroup_gid,subgroup_name,manager_gid,manager_name,member_gid,member_name" +
                        " from ocs_mst_tpenalwaiverapprovalconfig a " +
                        " where a.penalwaiverapprovalconfig_gid='" + values.penalwaiverapprovalconfig_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                lspenalwaiverapprovalconfig_gid = objODBCDatareader["penalwaiverapprovalconfig_gid"].ToString();
                lspenalwaiver_gid = objODBCDatareader["penalwaiver_gid"].ToString();
                lsvertical_gid = objODBCDatareader["vertical_gid"].ToString();
                lsgroup_gid = objODBCDatareader["group_gid"].ToString();
                lsgroup_name = objODBCDatareader["group_name"].ToString();
                lssubgroup_gid = objODBCDatareader["subgroup_gid"].ToString();
                lssubgroup_name = objODBCDatareader["subgroup_name"].ToString();
                lsmanager_gid = objODBCDatareader["manager_gid"].ToString();
                lsmanager_name = objODBCDatareader["manager_name"].ToString();
                lsmember_gid = objODBCDatareader["member_gid"].ToString();
                lsmember_name = objODBCDatareader["member_name"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " update ocs_mst_tpenalwaiverapprovalconfig set " +
                    " group_gid='" + values.group_gid + "'," +
                    " group_name = '" + values.group_name + "'," +
                    " subgroup_gid = '" + values.subgroup_gid + "'," +
                    " subgroup_name = '" + values.subgroup_name + "'," +
                    " manager_gid = '" + values.manager_gid + "'," +
                    " manager_name='" + values.manager_name + "'," +
                    " member_gid = '" + values.member_gid + "'," +
                    " member_name = '" + values.member_name + "'," +
                    " penalwaiver_gid = '" + lspenalwaiver_gid + "'," +
                    " vertical_gid='" + values.vertical_gid + "'," +
                    " updated_by = '" + employee_gid + "'," +
                    " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    " where penalwaiverapprovalconfig_gid ='" + values.penalwaiverapprovalconfig_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("PEWL");
                msSQL = " insert into ocs_mst_tpenalwaiverapprovalconfiglog(" +
                        " penalwaiverapprovalconfiglog_gid ," +
                        " penalwaiverapprovalconfig_gid," +
                        " penalwaiver_gid," +
                        " vertical_gid," +
                        " group_gid," +
                        " group_name," +
                        " subgroup_gid," +
                        " subgroup_name," +
                        " manager_gid," +
                        " manager_name," +
                        " member_gid," +
                        " member_name," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + lspenalwaiverapprovalconfig_gid + "'," +
                        "'" + lspenalwaiver_gid + "'," +
                        "'" + lsvertical_gid + "'," +
                        "'" + lsgroup_gid + "'," +
                        "'" + lsgroup_name + "'," +
                        "'" + lssubgroup_gid + "'," +
                        "'" + lssubgroup_name + "'," +
                        "'" + lsmanager_gid + "'," +
                        "'" + lsmanager_name + "'," +
                        "'" + lsmember_gid + "'," +
                        "'" + lsmember_name + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Penal Waiver Details Updated Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while updating Penal Waiver Details";
                return false;
            }
        }
        public void DaGetDisbursementApplicantView(string employee_gid, string application_gid, MdlDisbursementRequestAdd values)
        {
            msSQL = " select customer_urn,customer_name,vertical_name,program_name,mobile_no,email_address " +
                    " from ocs_trn_tcadapplication a " +
                    " where a.application_gid = '" + application_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.customer_urn = objODBCDatareader["customer_urn"].ToString();
                values.customer_name = objODBCDatareader["customer_name"].ToString();
                values.vertical_name = objODBCDatareader["vertical_name"].ToString();
                values.program_name = objODBCDatareader["program_name"].ToString();
                values.mobile_no = objODBCDatareader["mobile_no"].ToString();
                values.email_address = objODBCDatareader["email_address"].ToString();
            }
            objODBCDatareader.Close();
        }
        public void DaGetDisbursementRejectedView(string employee_gid, string rmdisbursementrequest_gid, MdlDisbursementRequestAdd values)
        {
            msSQL = " select disbursementassignment_gid, concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as rejected_by," +
                    " date_format(a.rejected_date,'%d-%m-%Y %h:%i %p') as rejected_date,rejected_remarks,approval_status " +
                    " from ocs_trn_tdisbursementassignment a " +
                    " left join hrm_mst_temployee b on b.employee_gid=a.rejected_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid" +
                    " where a.rmdisbursementrequest_gid = '" + rmdisbursementrequest_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.disbursementassignment_gid = objODBCDatareader["disbursementassignment_gid"].ToString();
                values.rejected_by = objODBCDatareader["rejected_by"].ToString();
                values.rejected_date = objODBCDatareader["rejected_date"].ToString();
                values.rejected_remarks = objODBCDatareader["rejected_remarks"].ToString();
                values.approval_status = objODBCDatareader["approval_status"].ToString();
            }
            objODBCDatareader.Close();
        }

        public void DaPostConfirmDisbursementRequest(MdlConfirmDisbursementAcct values, string employee_gid)
        {
            if (values.creditbankdtl_gid != "")
            {

                string lsbankaccountstatus_gid = "";
                string msGetTrnGid = objcmnfunctions.GetMasterGID("BASL");


                msSQL = " select bankaccountstatus_gid from ocs_trn_tbankaccountstatus c " +

                         " where credit_gid = '" + values.creditbankdtl_gid + "' and (rmdisbursementrequest_gid='" + values.rmdisbursementrequest_gid + "' or " +
                    " rmdisbursementrequest_gid='" + employee_gid + "') ";
                lsbankaccountstatus_gid = objdbconn.GetExecuteScalar(msSQL);
                if (lsbankaccountstatus_gid != "")
                {
                    //msSQL = "update ocs_trn_tbankaccountstatus set disbursementaccount_status='" + values.disbursementaccount_status + "'  where credit_gid = '" + values.creditbankdtl_gid + "' and  (rmdisbursementrequest_gid='" + values.rmdisbursementrequest_gid + "' or " +
                    //" rmdisbursementrequest_gid='" + employee_gid + "') ";
                    //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (values.rmdisbursementrequest_gid == null || values.rmdisbursementrequest_gid == "")
                    {
                        msSQL = " insert into ocs_trn_tbankaccountstatuslog (bankaccountstatuslog_gid,bankaccountstatus_gid,application_gid,created_by, " +
                       " created_date,disbursementaccount_status,rmdisbursementrequest_gid,credit_gid,initiated_by,initiatedupdated_by,updated_by,updated_date)" +
                       " (select '" + msGetTrnGid + "',bankaccountstatus_gid,application_gid,created_by,created_date,disbursementaccount_status,rmdisbursementrequest_gid,credit_gid," +
                       " initiated_by,initiatedupdated_by,updated_by,updated_date from ocs_trn_tbankaccountstatus c " +
                       " where credit_gid = '" + values.creditbankdtl_gid + "' and (rmdisbursementrequest_gid='" + values.rmdisbursementrequest_gid + "' or " +
                       " rmdisbursementrequest_gid='" + employee_gid + "')) ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = "update ocs_trn_tbankaccountstatus set disbursementaccount_status='" + values.disbursementaccount_status + "', " +
                            " initiatedupdated_by='" + values.initiated_by + "'," +
                            " updated_by='" + employee_gid + "'," +
                            " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                            " where credit_gid = '" + values.creditbankdtl_gid + "' and   rmdisbursementrequest_gid='" + employee_gid + "' ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    else
                    {

                        msSQL = " insert into ocs_trn_tbankaccountstatuslog (bankaccountstatuslog_gid,bankaccountstatus_gid,application_gid,created_by, " +
                       " created_date,disbursementaccount_status,rmdisbursementrequest_gid,credit_gid,initiated_by,initiatedupdated_by,updated_by,updated_date)" +
                       " (select '" + msGetTrnGid + "',bankaccountstatus_gid,application_gid,created_by,created_date,disbursementaccount_status,rmdisbursementrequest_gid," +
                       " credit_gid,initiated_by,initiatedupdated_by,updated_by,updated_date from ocs_trn_tbankaccountstatus c " +
                       " where credit_gid = '" + values.creditbankdtl_gid + "' and (rmdisbursementrequest_gid='" + values.rmdisbursementrequest_gid + "' or " +
                       " rmdisbursementrequest_gid='" + employee_gid + "')) ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = "update ocs_trn_tbankaccountstatus set disbursementaccount_status='" + values.disbursementaccount_status + "', " +
                             " initiatedupdated_by='" + values.initiated_by + "'," +
                            " updated_by='" + employee_gid + "'," +
                            " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                            " where credit_gid = '" + values.creditbankdtl_gid + "' and  rmdisbursementrequest_gid='" + values.rmdisbursementrequest_gid + "' ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                else
                {

                    msGetGid = objcmnfunctions.GetMasterGID("BAST");
                    msSQL = " insert into ocs_trn_tbankaccountstatus(" +
                            " bankaccountstatus_gid," +
                            " application_gid," +
                            " credit_gid," +
                            " disbursement_amount," +
                            " rmdisbursementrequest_gid," +
                            " disbursementaccount_status," +
                            " initiated_by," +
                            " created_by," +
                            " created_date)" +
                            " values(" +
                            "'" + msGetGid + "'," +
                            "'" + values.application_gid + "'," +
                             "'" + values.creditbankdtl_gid + "'," +
                            "'" + values.disbursement_amount + "',";
                    if (values.rmdisbursementrequest_gid == null || values.rmdisbursementrequest_gid == "")
                    {
                        msSQL += "'" + employee_gid + "',";
                    }
                    else
                    {
                        msSQL += "'" + values.rmdisbursementrequest_gid + "',";
                    }
                    msSQL += "'" + values.disbursementaccount_status + "'," +
                            "'" + values.initiated_by + "'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            else
            {
                string lsbankaccountstatus_gid = "";
                msSQL = " select bankaccountstatus_gid from ocs_trn_tbankaccountstatus  " +

                                  " where credit_gid = '" + values.lsabankaccdtl_gid + "' and  (rmdisbursementrequest_gid='" + values.rmdisbursementrequest_gid + "' or " +
                    " rmdisbursementrequest_gid='" + employee_gid + "') ";
                lsbankaccountstatus_gid = objdbconn.GetExecuteScalar(msSQL);
                if (lsbankaccountstatus_gid != "")
                {
                    if (values.rmdisbursementrequest_gid == null || values.rmdisbursementrequest_gid == "")
                    {
                        string msGetTrnGid = objcmnfunctions.GetMasterGID("BASL");
                        msSQL = " insert into ocs_trn_tbankaccountstatuslog (bankaccountstatuslog_gid,bankaccountstatus_gid,application_gid,created_by, " +
                           " created_date,disbursementaccount_status,rmdisbursementrequest_gid,credit_gid,initiated_by,initiatedupdated_by,updated_by,updated_date)" +
                           " (select '" + msGetTrnGid + "',bankaccountstatus_gid,application_gid,created_by,created_date,disbursementaccount_status,rmdisbursementrequest_gid,credit_gid," +
                           " initiated_by,initiatedupdated_by,updated_by,updated_date from ocs_trn_tbankaccountstatus c " +
                           " where credit_gid = '" + values.lsabankaccdtl_gid + "' and (rmdisbursementrequest_gid='" + values.rmdisbursementrequest_gid + "' or " +
                           " rmdisbursementrequest_gid='" + employee_gid + "')) ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = "update ocs_trn_tbankaccountstatus set disbursementaccount_status='" + values.disbursementaccount_status + "'," +
                                 " initiatedupdated_by='" + values.initiated_by + "'," +
                                " updated_by='" + employee_gid + "'," +
                                " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                                "  where credit_gid = '" + values.lsabankaccdtl_gid + "' and  rmdisbursementrequest_gid='" + employee_gid + "' ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    else
                    {
                        string msGetTrnGid = objcmnfunctions.GetMasterGID("BASL");
                        msSQL = " insert into ocs_trn_tbankaccountstatuslog (bankaccountstatuslog_gid,bankaccountstatus_gid,application_gid,created_by, " +
                           " created_date,disbursementaccount_status,rmdisbursementrequest_gid,credit_gid,initiated_by,initiatedupdated_by,updated_by,updated_date)" +
                           " (select '" + msGetTrnGid + "',bankaccountstatus_gid,application_gid,created_by,created_date,disbursementaccount_status,rmdisbursementrequest_gid,credit_gid," +
                           " initiated_by,initiatedupdated_by,updated_by,updated_date from ocs_trn_tbankaccountstatus c " +
                           " where credit_gid = '" + values.lsabankaccdtl_gid + "' and (rmdisbursementrequest_gid='" + values.rmdisbursementrequest_gid + "' or " +
                           " rmdisbursementrequest_gid='" + employee_gid + "')) ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = "update ocs_trn_tbankaccountstatus set disbursementaccount_status='" + values.disbursementaccount_status + "'," +
                                " initiatedupdated_by='" + values.initiated_by + "'," +
                                " updated_by='" + employee_gid + "'," +
                                " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                                "  where credit_gid = '" + values.lsabankaccdtl_gid + "' and  rmdisbursementrequest_gid='" + values.rmdisbursementrequest_gid + "' ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                else
                {
                    msGetGid = objcmnfunctions.GetMasterGID("BAST");
                    msSQL = " insert into ocs_trn_tbankaccountstatus(" +
                            " bankaccountstatus_gid," +
                            " application_gid," +
                            " credit_gid," +
                            " disbursement_amount," +
                            " rmdisbursementrequest_gid," +
                            " disbursementaccount_status," +
                            " created_by," +
                            " created_date)" +
                            " values(" +
                            "'" + msGetGid + "'," +
                            "'" + values.application_gid + "'," +
                             "'" + values.lsabankaccdtl_gid + "'," +
                            "'" + values.disbursement_amount + "',";
                    if (values.rmdisbursementrequest_gid == null || values.rmdisbursementrequest_gid == "")
                    {
                        msSQL += "'" + employee_gid + "',";
                    }
                    else
                    {
                        msSQL += "'" + values.rmdisbursementrequest_gid + "',";
                    }
                    msSQL += "'" + values.disbursementaccount_status + "'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Disbursement Status Added successfully";
            }
            else
            {
                values.message = "Error Occured while Adding";
                values.status = false;
            }

        }

        public void DaGetBankAccountTempClear(string employee_gid, result values)
        {
            msSQL = "delete from ocs_trn_tdisbapplicantbankdtl where rmdisbursementrequest_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from ocs_trn_tdisbursementamount where rmdisbursementrequest_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from ocs_trn_tbankaccountstatus where rmdisbursementrequest_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from ocs_mst_tdisbapplicantbankdocument where disbapplicantbankdtl_gid ='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            values.status = true;
        }

        public void DaGetApplicantSummary(string rmdisbursementrequest_gid, string employee_gid, string application_gid, MdlDisbursementDtlView values)
        {
            try
            {
                string lsapplicant_gid = "";

                msSQL = " select institution_gid from ocs_trn_tcadapplication a " +
                       " left join  ocs_trn_tcadinstitution e on e.application_gid = a.application_gid " +
                       " where stakeholder_type = 'Applicant' and a.application_gid ='" + application_gid + "'";
                lsapplicant_gid = objdbconn.GetExecuteScalar(msSQL);
                if (lsapplicant_gid != "")
                {
                    msSQL = " select a.application_gid,a.institution_gid as credit_gid,a.company_name as customer_name,b.mobile_no,c.email_address," +
                            " e.disbursement_amount,e.disbursementamount_gid,e.makerdisbursement_amount,e.checkerdisbursement_amount,f.encoreintegration_status,f.encore_accountid,f.disbursementbookingencore_status  " +
                            " from ocs_trn_tcadinstitution a " +
                             " left join ocs_mst_tinstitution2mobileno b on b.institution_gid = a.institution_gid  and b.primary_status ='Yes' " +
                             " left join ocs_mst_tinstitution2email c on c.institution_gid = a.institution_gid  and c.primary_status ='Yes' " +
                             " left join ocs_trn_trmdisbursementrequest f on f.application_gid = a.application_gid and (f.rmdisbursementrequest_gid='" + rmdisbursementrequest_gid + "' or  f.rmdisbursementrequest_gid='" + employee_gid + "') " +
                             " left join ocs_trn_tdisbursementamount e on e.application_gid = a.application_gid and (e.rmdisbursementrequest_gid='" + rmdisbursementrequest_gid + "' or  e.rmdisbursementrequest_gid='" + employee_gid + "') " +

                             " where stakeholder_type ='Applicant'  and  a.application_gid='" + application_gid + "' ";
                }
                else
                {
                    msSQL = " select a.application_gid,a.contact_gid as credit_gid,concat(first_name,middle_name,last_name) as customer_name,b.mobile_no,c.email_address," +
                            " e.disbursement_amount,e.disbursementamount_gid,e.makerdisbursement_amount,e.checkerdisbursement_amount,f.encoreintegration_status,f.encore_accountid,f.disbursementbookingencore_status  " +
                            " from ocs_trn_tcadcontact a " +
                            " left join ocs_mst_tcontact2mobileno b on b.contact_gid = a.contact_gid  and b.primary_status ='Yes' " +
                            " left join ocs_mst_tcontact2email c on c.contact_gid = a.contact_gid  and c.primary_status ='Yes' " +
                            " left join ocs_trn_trmdisbursementrequest f on f.application_gid = a.application_gid and (f.rmdisbursementrequest_gid='" + rmdisbursementrequest_gid + "' or  f.rmdisbursementrequest_gid='" + employee_gid + "') " +
                            " left join ocs_trn_tdisbursementamount e on e.application_gid = a.application_gid  and (e.rmdisbursementrequest_gid='" + rmdisbursementrequest_gid + "' or  e.rmdisbursementrequest_gid='" + employee_gid + "') " +
                            " where stakeholder_type ='Applicant'  and  a.application_gid='" + application_gid + "' ";
                }
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.application_gid = objODBCDatareader["application_gid"].ToString();
                    values.credit_gid = objODBCDatareader["credit_gid"].ToString();
                    values.customer_name = objODBCDatareader["customer_name"].ToString();
                    values.mobile_no = objODBCDatareader["mobile_no"].ToString();
                    values.email_address = objODBCDatareader["email_address"].ToString();
                    values.disbursement_amount = objODBCDatareader["disbursement_amount"].ToString();
                    values.disbursementamount_gid = objODBCDatareader["disbursementamount_gid"].ToString();
                    values.makerdisbursement_amount = objODBCDatareader["makerdisbursement_amount"].ToString();
                    values.checkerdisbursement_amount = objODBCDatareader["checkerdisbursement_amount"].ToString();
                    values.encoreintegration_status = objODBCDatareader["encoreintegration_status"].ToString();
                    values.encore_accountid = objODBCDatareader["encore_accountid"].ToString();
                    values.disbursementbookingencore_status = objODBCDatareader["disbursementbookingencore_status"].ToString();

                }
                values.status = true;
                values.message = "success";
                objODBCDatareader.Close();

            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "failure";
            }

        }
        public void DaPostDisbursementAmount(MdlConfirmDisbursementAcct values, string employee_gid)
        {
            if (string.IsNullOrEmpty(values.disbursement_amount))
            {
                values.status = false;
                values.message = "Kindly Add Disbursement Amount";

                return;

            }


            msSQL = "select sanction_amount from ocs_trn_tapplication2sanction where  application_gid='" + values.application_gid + "'";
            string lssanction_amount = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " select disbursement_amount as sumdisbursement_amount from ocs_trn_tdisbursementamount  where application_gid='" + values.application_gid + "' and " +
                   " (rmdisbursementrequest_gid !='" + employee_gid + "' and  " +
                   " rmdisbursementrequest_gid !='" + values.rmdisbursementrequest_gid + "')" +
                   " and rmdisbursementrequest_gid not in (select rmdisbursementrequest_gid from ocs_trn_tdisbursementassignment where approval_status ='Rejected') " +
                   " and " +
                   " ((makerdisbursement_amount is null ||makerdisbursement_amount ='') && " +
                   " (checkerdisbursement_amount is null ||checkerdisbursement_amount ='')) " +
                   " union all " +
                   " select makerdisbursement_amount  as sumdisbursement_amount from ocs_trn_tdisbursementamount  where application_gid='" + values.application_gid + "' and " +
                   " (rmdisbursementrequest_gid !='" + employee_gid + "' and " +
                   " rmdisbursementrequest_gid !='" + values.rmdisbursementrequest_gid + "')" +
                   " and rmdisbursementrequest_gid not in (select rmdisbursementrequest_gid from ocs_trn_tdisbursementassignment where approval_status ='Rejected') " +
                   " and " +
                   " ((makerdisbursement_amount is not null ||makerdisbursement_amount !='') && " +
                   " (checkerdisbursement_amount is  null ||checkerdisbursement_amount ='')) " +
                   " union all " +
                   " select checkerdisbursement_amount as sumdisbursement_amount from ocs_trn_tdisbursementamount  where application_gid='" + values.application_gid + "' and " +
                   " (rmdisbursementrequest_gid !='" + employee_gid + "' and " +
                   " rmdisbursementrequest_gid  !='" + values.rmdisbursementrequest_gid + "')" +
                   " and rmdisbursementrequest_gid not in (select rmdisbursementrequest_gid from ocs_trn_tdisbursementassignment where approval_status ='Rejected') " +
                   " and " +
                   " ((checkerdisbursement_amount is not null ||checkerdisbursement_amount !=''))" +
                    " union all" +
                    " select case when (creditopscheckerdisbursement_amount is null and  creditopsdisbursement_amount is null) then disbursement_amount  " +
                    " when (creditopscheckerdisbursement_amount is null ) then creditopsdisbursement_amount  " +
                    " else creditopscheckerdisbursement_amount end as sumdisbursement_amount " +
                    " from ocs_trn_tdisbursementsupplier  where application_gid='" + values.application_gid + "' and " +
                    " disbursementsupplier_gid !='" + values.disbursementsupplier_gid + "' " +
                    " and rmdisbursementrequest_gid not in (select rmdisbursementrequest_gid from ocs_trn_tdisbursementassignment where approval_status ='Rejected') " +
                    " union all" +
                    " select case when (creditopscheckerdisbursement_amount is null and  creditopsdisbursement_amount is null) then disbursement_amount  " +
                    " when (creditopscheckerdisbursement_amount is null ) then creditopsdisbursement_amount  " +
                    " else creditopscheckerdisbursement_amount end as sumdisbursement_amount " +
                    " from ocs_trn_tfarmercontact  where application_gid='" + values.application_gid + "' and " +
                    " farmercontact_gid !='" + values.farmercontact_gid + "' " +
                    " and rmdisbursementrequest_gid not in (select rmdisbursementrequest_gid from ocs_trn_tdisbursementassignment where approval_status ='Rejected') ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdisbursementamount_list = new List<disbursementamount_list>();
            double disbursementamount_total = 0;
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {

                    disbursementamount_total = disbursementamount_total + double.Parse(dr_datarow["sumdisbursement_amount"].ToString().Replace(",", ""));

                }

            }
            dt_datatable.Dispose();

            if (string.IsNullOrEmpty(values.disbursement_amount))
            {

            }
            else
            {
                disbursementamount_total = disbursementamount_total + double.Parse(values.disbursement_amount.Replace(",", ""));
            }



            if (disbursementamount_total <= double.Parse(lssanction_amount))
            {

            }
            else
            {
                values.status = false;
                values.message = "Disbursement Amount is Greater than Sanction Amount";

                return;
            }


            string lsbankaccountstatus_gid = "";

            msSQL = " select  disbursementamount_gid from ocs_trn_tdisbursementamount  " +
                    " where application_gid = '" + values.application_gid + "' and  " +
                    " (rmdisbursementrequest_gid='" + values.rmdisbursementrequest_gid + "' or " +
                    " rmdisbursementrequest_gid='" + employee_gid + "') ";
            lsbankaccountstatus_gid = objdbconn.GetExecuteScalar(msSQL);
            if (lsbankaccountstatus_gid != "")
            {
                msSQL = "update ocs_trn_tdisbursementamount set disbursement_amount='" + values.disbursement_amount + "'  where application_gid = '" + values.application_gid + "' and  (rmdisbursementrequest_gid='" + values.rmdisbursementrequest_gid + "' or " +
                    " rmdisbursementrequest_gid='" + employee_gid + "') ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else
            {

                msGetGid = objcmnfunctions.GetMasterGID("DBAM");
                msSQL = " insert into ocs_trn_tdisbursementamount(" +
                        " disbursementamount_gid," +
                        " application_gid," +
                        " credit_gid," +
                        " disbursement_amount," +
                        " rmdisbursementrequest_gid," +

                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + values.application_gid + "'," +
                         "'" + values.credit_gid + "'," +
                        "'" + values.disbursement_amount + "'," +
                        "'" + employee_gid + "'," +

                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }



            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Disbursement Amount Added successfully";
            }
            else
            {
                values.message = "Error Occured while Adding";
                values.status = false;
            }

        }

        public void DaGetDisbAppilicantDtlView(string employee_gid, string disbapplicantbankdtl_gid, MdlDisbSupplierBankAcct values)
        {
            try
            {
                msSQL = " select a.disbursementaccount_status,a.disbapplicantbankdtl_gid,a.application_gid,a.ifsc_code,a.micr_code, " +
                        " a.branch_address,a.bank_name,a.branch_name,a.bankaccount_number,a.confirmbankaccount_number," +
                        " a.accountholder_name,a.disbursement_amount, " +
                        " concat(f.user_firstname, ' ', f.user_lastname, ' / ', f.user_code) as created_by, " +
                        " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date " +
                        " from ocs_trn_tdisbapplicantbankdtl a " +
                        " left join hrm_mst_temployee e on e.employee_gid = a.created_by " +
                        " left join adm_mst_tuser f on f.user_gid = e.user_gid " +
                        " where a.disbapplicantbankdtl_gid='" + disbapplicantbankdtl_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.disbapplicantbankdtl_gid = objODBCDatareader["disbapplicantbankdtl_gid"].ToString();
                    values.application_gid = objODBCDatareader["application_gid"].ToString();
                    //values.supplier_name = objODBCDatareader["supplier_name"].ToString();
                    values.ifsc_code = objODBCDatareader["ifsc_code"].ToString();
                    values.micr_code = objODBCDatareader["micr_code"].ToString();
                    values.branch_address = objODBCDatareader["branch_address"].ToString();
                    values.bank_name = objODBCDatareader["bank_name"].ToString();
                    values.branch_name = objODBCDatareader["branch_name"].ToString();
                    values.bankaccount_number = objODBCDatareader["bankaccount_number"].ToString();
                    values.confirmbankaccount_number = objODBCDatareader["confirmbankaccount_number"].ToString();
                    values.accountholder_name = objODBCDatareader["accountholder_name"].ToString();
                    values.disbursement_amount = objODBCDatareader["disbursement_amount"].ToString();
                    values.created_by = objODBCDatareader["created_by"].ToString();
                    values.created_date = objODBCDatareader["created_date"].ToString();
                    values.disbursementaccount_status = objODBCDatareader["disbursementaccount_status"].ToString();

                }
                objODBCDatareader.Close();

                msSQL = " select disbapplicantbankdocument_gid,disbapplicantbankdtl_gid,document_title,document_name,document_path " +
                               " from ocs_mst_tdisbapplicantbankdocument where " +
                               " disbapplicantbankdtl_gid='" + employee_gid + "' or  disbapplicantbankdtl_gid='" + disbapplicantbankdtl_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getdisbapplicantuploaddocument_list = new List<disbapplicantuploaddocument_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getdisbapplicantuploaddocument_list.Add(new disbapplicantuploaddocument_list
                        {
                            document_name = dt["document_name"].ToString(),
                            document_title = dt["document_title"].ToString(),
                            document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                            disbapplicantbankdtl_gid = dt["disbapplicantbankdtl_gid"].ToString(),
                            disbapplicantbankdocument_gid = dt["disbapplicantbankdocument_gid"].ToString()
                        });
                        values.disbapplicantuploaddocument_list = getdisbapplicantuploaddocument_list;
                    }
                }
                dt_datatable.Dispose();

                values.status = true;
                values.message = "success";
            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }
        }
        public bool DaPostUpdateBankAccountDetails(string employee_gid, MdlBankAccount values)
        {


            if (values.bank_name == null)
                values.bank_name = "";
            if (values.branch_name == null)
                values.branch_name = "";
            if (values.bank_address == null)
                values.bank_address = "";
            if (values.micr_code == null)
                values.micr_code = "";

            if (values.branch_address == null)
                values.branch_address = "";
            if (values.bankaccounttype_name == null)
                values.bankaccounttype_name = "";

            string msGetTrnGid = objcmnfunctions.GetMasterGID("BASL");


            msSQL = " insert into ocs_trn_tbankaccountstatuslog (bankaccountstatuslog_gid,application_gid,created_by, " +
                                  " created_date,disbursementaccount_status,rmdisbursementrequest_gid,credit_gid,initiated_by," +
                                  " initiatedupdated_by,updated_by,updated_date,bank_name,branch_name,branch_address,micr_code,ifsc_code," +
                                  "accountholder_name,bankaccount_number,confirmbankaccount_number)" +
                                  " select '" + msGetTrnGid + "',application_gid,created_by,created_date," +
                                  " disbursementaccount_status,rmdisbursementrequest_gid,disbapplicantbankdtl_gid," +
                                  " initiated_by,initiatedupdated_by,updated_by,updated_date,bank_name,branch_name,branch_address,micr_code,ifsc_code," +
                                  "accountholder_name,bankaccount_number,confirmbankaccount_number from ocs_trn_tdisbapplicantbankdtl  " +
                                  " where disbapplicantbankdtl_gid = '" + values.disbapplicantbankdtl_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " update ocs_trn_tdisbapplicantbankdtl set " +
                    " bank_name='" + values.bank_name.Replace("'", "") + "'," +
                    " branch_name='" + values.branch_name.Replace("'", "") + "'," +
                    " branch_address='" + values.branch_address.Replace("'", "") + "'," +
                    " micr_code='" + values.micr_code.Replace("'", "") + "'," +
                    " ifsc_code='" + values.ifsc_code + "'," +
                    " accountholder_name='" + values.accountholder_name.Replace("'", "") + "'," +
                    " bankaccount_number='" + values.bankaccount_number + "'," +
                    " confirmbankaccount_number='" + values.confirmbankaccount_number + "'," +
                     " disbursementaccount_status='" + values.disbursementaccount_status + "'," +
                     " initiatedupdated_by='" + values.initiated_by + "'," +
                     " updated_by ='" + employee_gid + "'," +
                     " updated_date ='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                     " where disbapplicantbankdtl_gid='" + values.disbapplicantbankdtl_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                //msSQL = " update ocs_trn_tlsachequeleafdocument set lsabankaccdtl_gid='" + values.lsabankaccdtl_gid + "' " +
                //        " where lsabankaccdtl_gid='" + employee_gid + "' and generatelsa_gid='" + values.generatelsa_gid + "'";
                //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                string lsdisbapplicantbankdtl_gid = "";
                msSQL = " select disbapplicantbankdtl_gid from ocs_trn_tdisbapplicantbankdtl  where (rmdisbursementrequest_gid='" + values.rmdisbursementrequest_gid + "' or " +
                    " rmdisbursementrequest_gid='" + employee_gid + "') and '" + values.disbursementaccount_status + "' = 'Yes'";
                lsdisbapplicantbankdtl_gid = objdbconn.GetExecuteScalar(msSQL);
                if (lsdisbapplicantbankdtl_gid != "")
                {
                    msSQL = "delete from ocs_trn_tbankaccountstatus where  (rmdisbursementrequest_gid='" + values.rmdisbursementrequest_gid + "' or " +
                    " rmdisbursementrequest_gid='" + employee_gid + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                msSQL = "update ocs_mst_tdisbapplicantbankdocument set disbapplicantbankdtl_gid='" + values.disbapplicantbankdtl_gid + "' where disbapplicantbankdtl_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                msSQL = " select disbapplicantbankdtl_gid,applicant_name,bank_name,branch_name,ifsc_code, " +
                    " bankaccount_number,confirmbankaccount_number,accountholder_name, " +
                    " concat(f.user_firstname, ' ', f.user_lastname, ' / ', f.user_code) as created_by, " +
                    " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date, " +
                    " disbursementaccount_status,initiated_by" +
                    " from ocs_trn_tdisbapplicantbankdtl a " +
                    " left join hrm_mst_temployee e on e.employee_gid = a.created_by " +
                    " left join adm_mst_tuser f on f.user_gid = e.user_gid " +
                    " where a.rmdisbursementrequest_gid='" + employee_gid + "' or " +
                    " a.rmdisbursementrequest_gid='" + values.rmdisbursementrequest_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getdisbapplicantbankacctdtl_list = new List<disbapplicantbankacctdtl_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getdisbapplicantbankacctdtl_list.Add(new disbapplicantbankacctdtl_list
                        {
                            applicant_name = dt["applicant_name"].ToString(),
                            bank_name = dt["bank_name"].ToString(),
                            branch_name = dt["branch_name"].ToString(),
                            ifsc_code = dt["ifsc_code"].ToString(),
                            bankaccount_number = dt["bankaccount_number"].ToString(),
                            confirmbankaccount_number = dt["confirmbankaccount_number"].ToString(),
                            accountholder_name = dt["accountholder_name"].ToString(),
                            disbapplicantbankdtl_gid = dt["disbapplicantbankdtl_gid"].ToString(),
                            created_by = dt["created_by"].ToString(),
                            created_date = dt["created_date"].ToString(),
                            disbursementaccount_status = dt["disbursementaccount_status"].ToString(),
                            initiated_by = dt["initiated_by"].ToString()
                        });
                        values.disbapplicantbankacctdtl_list = getdisbapplicantbankacctdtl_list;
                    }
                }
                dt_datatable.Dispose();
                values.status = true;
                values.message = "Bank Account Details are Updated Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
                return false;
            }

        }
        public void DaCreditOpsApplicantDisbAmountUpdate(string employee_gid, MdlDisbCreditOpsApplicantBankAcct values)
        {
            if (string.IsNullOrEmpty(values.makerdisbursement_amount))
            {
                values.status = false;
                values.message = "Kindly Add Maker Disbursement Amount";

                return;

            }


            msSQL = "select sanction_amount from ocs_trn_tapplication2sanction where  application_gid='" + values.application_gid + "'";
            string lssanction_amount = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " select disbursement_amount as sumdisbursement_amount from ocs_trn_tdisbursementamount  where application_gid='" + values.application_gid + "' and " +
                   " (rmdisbursementrequest_gid !='" + employee_gid + "' and " +
                   " rmdisbursementrequest_gid !='" + values.rmdisbursementrequest_gid + "')" +
                   " and rmdisbursementrequest_gid not in (select rmdisbursementrequest_gid from ocs_trn_tdisbursementassignment where approval_status ='Rejected') " +
                   " and " +
                   " ((makerdisbursement_amount is null ||makerdisbursement_amount ='') && " +
                   " (checkerdisbursement_amount is null ||checkerdisbursement_amount ='')) " +
                   " union all " +
                   " select makerdisbursement_amount  as sumdisbursement_amount from ocs_trn_tdisbursementamount  where application_gid='" + values.application_gid + "' and " +
                   " (rmdisbursementrequest_gid !='" + employee_gid + "' and " +
                   " rmdisbursementrequest_gid !='" + values.rmdisbursementrequest_gid + "')" +
                   " and rmdisbursementrequest_gid not in (select rmdisbursementrequest_gid from ocs_trn_tdisbursementassignment where approval_status ='Rejected') " +
                   " and " +
                   " ((makerdisbursement_amount is not null ||makerdisbursement_amount !='') && " +
                   " (checkerdisbursement_amount is  null ||checkerdisbursement_amount ='')) " +
                   " union all " +
                   " select checkerdisbursement_amount as sumdisbursement_amount from ocs_trn_tdisbursementamount  where application_gid='" + values.application_gid + "' and " +
                   " (rmdisbursementrequest_gid !='" + employee_gid + "' and " +
                   " rmdisbursementrequest_gid  !='" + values.rmdisbursementrequest_gid + "')" +
                   " and rmdisbursementrequest_gid not in (select rmdisbursementrequest_gid from ocs_trn_tdisbursementassignment where approval_status ='Rejected') " +
                   " and " +
                   " ((checkerdisbursement_amount is not null ||checkerdisbursement_amount !=''))" +
                    " union all" +
                    " select case when (creditopscheckerdisbursement_amount is null and  creditopsdisbursement_amount is null) then disbursement_amount  " +
                    " when (creditopscheckerdisbursement_amount is null ) then creditopsdisbursement_amount  " +
                    " else creditopscheckerdisbursement_amount end as sumdisbursement_amount " +
                    " from ocs_trn_tdisbursementsupplier  where application_gid='" + values.application_gid + "' and " +
                    " disbursementsupplier_gid !='" + values.disbursementsupplier_gid + "' " +
                    " and rmdisbursementrequest_gid not in (select rmdisbursementrequest_gid from ocs_trn_tdisbursementassignment where approval_status ='Rejected') " +
                    " union all" +
                    " select case when (creditopscheckerdisbursement_amount is null and  creditopsdisbursement_amount is null) then disbursement_amount  " +
                    " when (creditopscheckerdisbursement_amount is null ) then creditopsdisbursement_amount  " +
                    " else creditopscheckerdisbursement_amount end as sumdisbursement_amount " +
                    " from ocs_trn_tfarmercontact  where application_gid='" + values.application_gid + "' and " +
                    " farmercontact_gid !='" + values.farmercontact_gid + "' " +
                    " and rmdisbursementrequest_gid not in (select rmdisbursementrequest_gid from ocs_trn_tdisbursementassignment where approval_status ='Rejected') ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdisbursementamount_list = new List<disbursementamount_list>();
            double disbursementamount_total = 0;
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {

                    disbursementamount_total = disbursementamount_total + double.Parse(dr_datarow["sumdisbursement_amount"].ToString().Replace(",", ""));

                }

            }
            dt_datatable.Dispose();


            if (string.IsNullOrEmpty(values.makerdisbursement_amount))
            {

            }
            else
            {
                disbursementamount_total = disbursementamount_total + double.Parse(values.makerdisbursement_amount.Replace(",", ""));
            }

            if (disbursementamount_total <= double.Parse(lssanction_amount))
            {

            }
            else
            {
                values.status = false;
                values.message = "Maker Disbursement Amount is Greater than Sanction Amount";

                return;
            }

            msSQL = " update ocs_trn_tdisbursementamount set" +
                    " makerdisbursement_amount ='" + values.makerdisbursement_amount + "'" +
                    " where disbursementamount_gid ='" + values.disbursementamount_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                values.status = true;
                values.message = "Disbursement Amount Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
            }
        }
        public void DaGetCreditOpsApplicantDisbAmountView(string employee_gid, string disbursementamount_gid, MdlDisbCreditOpsApplicantBankAcct values)
        {
            msSQL = " select disbursementamount_gid,makerdisbursement_amount,disbursement_amount " +
                    " from ocs_trn_tdisbursementamount a" +
                    " where disbursementamount_gid='" + disbursementamount_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.disbursementamount_gid = objODBCDatareader["disbursementamount_gid"].ToString();
                values.makerdisbursement_amount = objODBCDatareader["makerdisbursement_amount"].ToString();
                values.disbursement_amount = objODBCDatareader["disbursement_amount"].ToString();
            }
            objODBCDatareader.Close();
        }
        public void DaCheckerApplicantDisbAmountUpdate(string employee_gid, MdlDisbCreditOpsApplicantBankAcct values)
        {
            if (string.IsNullOrEmpty(values.checkerdisbursement_amount))
            {
                values.status = false;
                values.message = "Kindly Add Checker Disbursement Amount";

                return;

            }

            msSQL = "select sanction_amount from ocs_trn_tapplication2sanction where  application_gid='" + values.application_gid + "'";
            string lssanction_amount = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " select disbursement_amount as sumdisbursement_amount from ocs_trn_tdisbursementamount  where application_gid='" + values.application_gid + "' and " +
                   " (rmdisbursementrequest_gid !='" + employee_gid + "' and " +
                   " rmdisbursementrequest_gid !='" + values.rmdisbursementrequest_gid + "')" +
                   " and rmdisbursementrequest_gid not in (select rmdisbursementrequest_gid from ocs_trn_tdisbursementassignment where approval_status ='Rejected') " +
                   " and " +
                   " ((makerdisbursement_amount is null ||makerdisbursement_amount ='') && " +
                   " (checkerdisbursement_amount is null ||checkerdisbursement_amount ='')) " +
                   " union all " +
                   " select makerdisbursement_amount  as sumdisbursement_amount from ocs_trn_tdisbursementamount  where application_gid='" + values.application_gid + "' and " +
                   " (rmdisbursementrequest_gid !='" + employee_gid + "' and " +
                   " rmdisbursementrequest_gid !='" + values.rmdisbursementrequest_gid + "')" +
                   " and rmdisbursementrequest_gid not in (select rmdisbursementrequest_gid from ocs_trn_tdisbursementassignment where approval_status ='Rejected') " +
                   " and " +
                   " ((makerdisbursement_amount is not null ||makerdisbursement_amount !='') && " +
                   " (checkerdisbursement_amount is  null ||checkerdisbursement_amount ='')) " +
                   " union all " +
                   " select checkerdisbursement_amount as sumdisbursement_amount from ocs_trn_tdisbursementamount  where application_gid='" + values.application_gid + "' and " +
                   " (rmdisbursementrequest_gid !='" + employee_gid + "' and " +
                   " rmdisbursementrequest_gid  !='" + values.rmdisbursementrequest_gid + "')" +
                   " and rmdisbursementrequest_gid not in (select rmdisbursementrequest_gid from ocs_trn_tdisbursementassignment where approval_status ='Rejected') " +
                   " and " +
                   " ((checkerdisbursement_amount is not null ||checkerdisbursement_amount !=''))" +
                    " union all" +
                    " select case when (creditopscheckerdisbursement_amount is null and  creditopsdisbursement_amount is null) then disbursement_amount  " +
                    " when (creditopscheckerdisbursement_amount is null ) then creditopsdisbursement_amount  " +
                    " else creditopscheckerdisbursement_amount end as sumdisbursement_amount " +
                    " from ocs_trn_tdisbursementsupplier  where application_gid='" + values.application_gid + "' and " +
                    " disbursementsupplier_gid !='" + values.disbursementsupplier_gid + "' " +
                    " and rmdisbursementrequest_gid not in (select rmdisbursementrequest_gid from ocs_trn_tdisbursementassignment where approval_status ='Rejected') " +
                    " union all" +
                    " select case when (creditopscheckerdisbursement_amount is null and  creditopsdisbursement_amount is null) then disbursement_amount  " +
                    " when (creditopscheckerdisbursement_amount is null ) then creditopsdisbursement_amount  " +
                    " else creditopscheckerdisbursement_amount end as sumdisbursement_amount " +
                    " from ocs_trn_tfarmercontact  where application_gid='" + values.application_gid + "' and " +
                    " farmercontact_gid !='" + values.farmercontact_gid + "' " +
                    " and rmdisbursementrequest_gid not in (select rmdisbursementrequest_gid from ocs_trn_tdisbursementassignment where approval_status ='Rejected') ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdisbursementamount_list = new List<disbursementamount_list>();
            double disbursementamount_total = 0;
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {

                    disbursementamount_total = disbursementamount_total + double.Parse(dr_datarow["sumdisbursement_amount"].ToString().Replace(",", ""));

                }

            }
            dt_datatable.Dispose();




            if (string.IsNullOrEmpty(values.checkerdisbursement_amount))
            {

            }
            else
            {
                disbursementamount_total = disbursementamount_total + double.Parse(values.checkerdisbursement_amount.Replace(",", ""));
            }


            if (disbursementamount_total <= double.Parse(lssanction_amount))
            {

            }
            else
            {
                values.status = false;
                values.message = "Checker Disbursement Amount is Greater than Sanction Amount";

                return;
            }

            msSQL = " update ocs_trn_tdisbursementamount set" +
                    " checkerdisbursement_amount ='" + values.checkerdisbursement_amount + "'" +
                    " where disbursementamount_gid ='" + values.disbursementamount_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                values.status = true;
                values.message = "Disbursement Amount Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
            }
        }
        public void DaGetCheckerApplicantDisbAmountView(string employee_gid, string disbursementamount_gid, MdlDisbCreditOpsApplicantBankAcct values)
        {
            msSQL = " select disbursementamount_gid,checkerdisbursement_amount " +
                    " from ocs_trn_tdisbursementamount a" +
                    " where disbursementamount_gid='" + disbursementamount_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.disbursementamount_gid = objODBCDatareader["disbursementamount_gid"].ToString();
                values.checkerdisbursement_amount = objODBCDatareader["checkerdisbursement_amount"].ToString();
            }
            objODBCDatareader.Close();
        }

        public void DaGetNewApplicantSummary(string employee_gid, string application_gid, MdlDisbursementDtlView values)
        {
            try
            {
                string lsapplicant_gid = "";

                msSQL = " select institution_gid from ocs_trn_tcadapplication a " +
                       " left join  ocs_trn_tcadinstitution e on e.application_gid = a.application_gid " +
                       " where stakeholder_type = 'Applicant' and a.application_gid ='" + application_gid + "'";
                lsapplicant_gid = objdbconn.GetExecuteScalar(msSQL);
                if (lsapplicant_gid != "")
                {
                    msSQL = " select a.application_gid,a.institution_gid as credit_gid,a.company_name as customer_name,b.mobile_no,c.email_address," +
                            " e.disbursement_amount,e.disbursementamount_gid,e.makerdisbursement_amount,e.checkerdisbursement_amount " +
                            " from ocs_trn_tcadinstitution a " +
                             " left join ocs_mst_tinstitution2mobileno b on b.institution_gid = a.institution_gid  and b.primary_status ='Yes' " +
                             " left join ocs_mst_tinstitution2email c on c.institution_gid = a.institution_gid  and c.primary_status ='Yes' " +
                             " left join ocs_trn_tdisbursementamount e on e.application_gid = a.application_gid  and ( rmdisbursementrequest_gid is null or rmdisbursementrequest_gid='" + employee_gid + "') " +
                             " where stakeholder_type ='Applicant'  and  a.application_gid='" + application_gid + "'";
                }
                else
                {
                    msSQL = " select a.application_gid,a.contact_gid as credit_gid,concat(first_name,middle_name,last_name) as customer_name,b.mobile_no,c.email_address," +
                            " e.disbursement_amount,e.disbursementamount_gid,e.makerdisbursement_amount,e.checkerdisbursement_amount" +
                            " from ocs_trn_tcadcontact a " +
                            " left join ocs_mst_tcontact2mobileno b on b.contact_gid = a.contact_gid  and b.primary_status ='Yes' " +
                            " left join ocs_mst_tcontact2email c on c.contact_gid = a.contact_gid  and c.primary_status ='Yes' " +
                            " left join ocs_trn_tdisbursementamount e on e.application_gid = a.application_gid and ( rmdisbursementrequest_gid is null or rmdisbursementrequest_gid='" + employee_gid + "') " +
                            " where stakeholder_type ='Applicant'  and  a.application_gid='" + application_gid + "'";
                }
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.application_gid = objODBCDatareader["application_gid"].ToString();
                    values.credit_gid = objODBCDatareader["credit_gid"].ToString();
                    values.customer_name = objODBCDatareader["customer_name"].ToString();
                    values.mobile_no = objODBCDatareader["mobile_no"].ToString();
                    values.email_address = objODBCDatareader["email_address"].ToString();
                    values.disbursement_amount = objODBCDatareader["disbursement_amount"].ToString();
                    values.disbursementamount_gid = objODBCDatareader["disbursementamount_gid"].ToString();
                    values.makerdisbursement_amount = objODBCDatareader["makerdisbursement_amount"].ToString();
                    values.checkerdisbursement_amount = objODBCDatareader["checkerdisbursement_amount"].ToString();

                }
                values.status = true;
                values.message = "success";
                objODBCDatareader.Close();

            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "failure";
            }

        }

        public void DaDisbursementAmountCal(string employee_gid, string rmdisbursementrequest_gid, string application_gid, MdlConfirmDisbursementAcct values)
        {
            msSQL = "select sanction_amount from ocs_trn_tapplication2sanction where  application_gid='" + application_gid + "'";
            string lssanction_amount = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select disbursement_amount as sumdisbursement_amount from ocs_trn_tdisbursementamount  where application_gid='" + application_gid + "' and " +
                    "  rmdisbursementrequest_gid not in (select rmdisbursementrequest_gid from ocs_trn_tdisbursementassignment where approval_status ='Rejected') " +
                    " and " +
                    " ((makerdisbursement_amount is null ||makerdisbursement_amount ='') && " +
                    " (checkerdisbursement_amount is null ||checkerdisbursement_amount ='')) " +
                    " union all" +
                    " select makerdisbursement_amount  as sumdisbursement_amount from ocs_trn_tdisbursementamount  where application_gid='" + application_gid + "' and " +
                    " rmdisbursementrequest_gid not in (select rmdisbursementrequest_gid from ocs_trn_tdisbursementassignment where approval_status ='Rejected') " +
                    " and " +
                    " ((makerdisbursement_amount is not null ||makerdisbursement_amount !='') && " +
                    " (checkerdisbursement_amount is  null ||checkerdisbursement_amount ='')) " +
                    " union all" +
                    " select checkerdisbursement_amount as sumdisbursement_amount from ocs_trn_tdisbursementamount  where application_gid='" + application_gid + "' and " +
                    " rmdisbursementrequest_gid not in (select rmdisbursementrequest_gid from ocs_trn_tdisbursementassignment where approval_status ='Rejected') " +
                    " and " +
                    " ((checkerdisbursement_amount is not null ||checkerdisbursement_amount !=''))" +
                    " union all" +
                    " select case when (creditopscheckerdisbursement_amount is null and  creditopsdisbursement_amount is null) then disbursement_amount  " +
                    " when (creditopscheckerdisbursement_amount is null ) then creditopsdisbursement_amount  " +
                    " else creditopscheckerdisbursement_amount end as sumdisbursement_amount " +
                    " from ocs_trn_tdisbursementsupplier  where application_gid='" + application_gid + "' " +
                    " and rmdisbursementrequest_gid not in (select rmdisbursementrequest_gid from ocs_trn_tdisbursementassignment where approval_status ='Rejected') " +
                    " union all" +
                    " select case when (creditopscheckerdisbursement_amount is null and  creditopsdisbursement_amount is null) then disbursement_amount  " +
                    " when (creditopscheckerdisbursement_amount is null ) then creditopsdisbursement_amount  " +
                    " else creditopscheckerdisbursement_amount end as sumdisbursement_amount " +
                    " from ocs_trn_tfarmercontact  where application_gid='" + application_gid + "' " +
                    " and rmdisbursementrequest_gid not in (select rmdisbursementrequest_gid from ocs_trn_tdisbursementassignment where approval_status ='Rejected') ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdisbursementamount_list = new List<disbursementamount_list>();
            double disbursementamount_total = 0;
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {

                    disbursementamount_total = disbursementamount_total + double.Parse(dr_datarow["sumdisbursement_amount"].ToString().Replace(",", ""));

                }

            }
            dt_datatable.Dispose();

            if (disbursementamount_total <= double.Parse(lssanction_amount))
            {
                values.disbursementamount_status = "T";
            }
            else
            {
                values.disbursementamount_status = "F";
            }

        }

        public void DaDisbursementAmountCalValidation(string employee_gid, MdlConfirmDisbursementAcct values)
        {
            msSQL = "select sanction_amount from ocs_trn_tapplication2sanction where  application_gid='" + values.application_gid + "'";
            string lssanction_amount = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " select disbursement_amount as sumdisbursement_amount, disbursementamount_gid as primary_gid from ocs_trn_tdisbursementamount  where application_gid='" + values.application_gid + "' and " +
                   " (rmdisbursementrequest_gid !='" + employee_gid + "' and " +
                   " rmdisbursementrequest_gid !='" + values.rmdisbursementrequest_gid + "')" +
                   " and rmdisbursementrequest_gid not in (select rmdisbursementrequest_gid from ocs_trn_tdisbursementassignment where approval_status ='Rejected') " +
                   " and " +
                   " ((makerdisbursement_amount is null ||makerdisbursement_amount ='') && " +
                   " (checkerdisbursement_amount is null ||checkerdisbursement_amount ='')) " +
                   " union all" +
                   " select makerdisbursement_amount  as sumdisbursement_amount, disbursementamount_gid as primary_gid from ocs_trn_tdisbursementamount  where application_gid='" + values.application_gid + "' and " +
                   " (rmdisbursementrequest_gid !='" + employee_gid + "' and " +
                   " rmdisbursementrequest_gid !='" + values.rmdisbursementrequest_gid + "')" +
                   " and rmdisbursementrequest_gid not in (select rmdisbursementrequest_gid from ocs_trn_tdisbursementassignment where approval_status ='Rejected') " +
                   " and " +
                   " ((makerdisbursement_amount is not null ||makerdisbursement_amount !='') && " +
                   " (checkerdisbursement_amount is  null ||checkerdisbursement_amount ='')) " +
                   " union all" +
                   " select checkerdisbursement_amount as sumdisbursement_amount, disbursementamount_gid as primary_gid from ocs_trn_tdisbursementamount  where application_gid='" + values.application_gid + "' and " +
                   " (rmdisbursementrequest_gid !='" + employee_gid + "' and " +
                   " rmdisbursementrequest_gid  !='" + values.rmdisbursementrequest_gid + "')" +
                   " and rmdisbursementrequest_gid not in (select rmdisbursementrequest_gid from ocs_trn_tdisbursementassignment where approval_status ='Rejected') " +
                   " and " +
                   " ((checkerdisbursement_amount is not null ||checkerdisbursement_amount !='')) " +
                    " union all" +
                    " select case when (creditopscheckerdisbursement_amount is null and  creditopsdisbursement_amount is null) then disbursement_amount  " +
                    " when (creditopscheckerdisbursement_amount is null ) then creditopsdisbursement_amount  " +
                    " else creditopscheckerdisbursement_amount end as sumdisbursement_amount, disbursementsupplier_gid as primary_gid " +
                    " from ocs_trn_tdisbursementsupplier  where application_gid='" + values.application_gid + "' and " +
                    " disbursementsupplier_gid !='" + values.disbursementsupplier_gid + "' " +
                    " and rmdisbursementrequest_gid not in (select rmdisbursementrequest_gid from ocs_trn_tdisbursementassignment where approval_status ='Rejected') " +
                    " union all" +
                    " select case when (creditopscheckerdisbursement_amount is null and  creditopsdisbursement_amount is null) then disbursement_amount  " +
                    " when (creditopscheckerdisbursement_amount is null ) then creditopsdisbursement_amount  " +
                    " else creditopscheckerdisbursement_amount end as sumdisbursement_amount, farmercontact_gid as primary_gid " +
                    " from ocs_trn_tfarmercontact  where application_gid='" + values.application_gid + "' and " +
                    " farmercontact_gid !='" + values.farmercontact_gid + "' " +
                    " and rmdisbursementrequest_gid not in (select rmdisbursementrequest_gid from ocs_trn_tdisbursementassignment where approval_status ='Rejected') ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdisbursementamount_list = new List<disbursementamount_list>();
            double disbursementamount_total = 0;
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {

                    disbursementamount_total = disbursementamount_total + double.Parse(dr_datarow["sumdisbursement_amount"].ToString().Replace(",", ""));

                }

            }
            dt_datatable.Dispose();
            if (string.IsNullOrEmpty(values.validation_amount))
            {

            }
            else
            {
                disbursementamount_total = disbursementamount_total + double.Parse(values.validation_amount.Replace(",", ""));
            }


            if (disbursementamount_total <= double.Parse(lssanction_amount))
            {
                values.disbursementamount_status = "T";
            }
            else
            {
                values.disbursementamount_status = "F";
            }

        }

        //instrument Encore Integration - Payment Option Dropdown 
        public void DaGetinstrumentlist(mdlinstrument objmaster)
        {
            try
            {
                msSQL = " SELECT instrument_gid,instrument from ocs_mst_tencoreinstrumentmaster ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    objmaster.instrumentlist = dt_datatable.AsEnumerable().Select(row =>
                      new instrumentlist
                      {
                          instrument_gid = row["instrument_gid"].ToString(),
                          instrument = row["instrument"].ToString()
                      }
                    ).ToList();
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch (Exception ex)
            {
                objmaster.status = false;
            }
        }
        public void DaDeleteDisbFarmerDtl(string farmercontact_gid, string employee_gid, result values)
        {
            msSQL = " delete from ocs_trn_tcontactcoapplicant where farmercontact_gid='" + farmercontact_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " delete from ocs_trn_tfarmercontact where farmercontact_gid='" + farmercontact_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Farmer Details Deleted Successfully..!";
            }

        }

        // Maker Farmer Disbursement Amount Export
        public void DaGetExportFarmerDisbAmount(string rmdisbursementrequest_gid, string application_gid, MdlExcelImportApplication objMstExportFarmerDisbAmount)
        {
            msSQL = " select application_no as 'Application Number',pan_status as 'Pan Status',pan_no as 'Pan Number', " +
                    " concat(a.first_name, ' ', a.middle_name, ' / ', a.last_name) as 'Individual Name'," +
                    " creditopsdisbursement_amount as 'Disbursement Amount'" +
                    " from ocs_trn_tfarmercontact a " +
                    " where rmdisbursementrequest_gid='" + rmdisbursementrequest_gid + "' and " +
                    " application_gid='" + application_gid + "'" +
                    " order by a.created_date ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            string lscompany_code = string.Empty;
            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            var workSheet = excel.Workbook.Worksheets.Add("Individual Disbursement Amount Details");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                objMstExportFarmerDisbAmount.lsname = "Individual Disbursement Amount Details.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/Individual Disbursement Amount Details/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                objMstExportFarmerDisbAmount.lscloudpath = lscompany_code + "/" + "Master/Individual Disbursement Amount Details/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstExportFarmerDisbAmount.lsname;
                objMstExportFarmerDisbAmount.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/Individual Disbursement Amount Details/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstExportFarmerDisbAmount.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(objMstExportFarmerDisbAmount.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 22])  //Address "A1:A9"
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);
                }
                excel.SaveAs(ms);
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", objMstExportFarmerDisbAmount.lscloudpath, ms);
                status = objcmnstorage.UploadStream("erpdocument", objMstExportFarmerDisbAmount.lscloudpath, ms);
                ms.Close();

            }
            catch (Exception ex)
            {
                objMstExportFarmerDisbAmount.status = false;
                objMstExportFarmerDisbAmount.message = "Failure";
            }
            objMstExportFarmerDisbAmount.lscloudpath = objcmnstorage.EncryptData(objMstExportFarmerDisbAmount.lscloudpath);
            objMstExportFarmerDisbAmount.lspath = objcmnstorage.EncryptData(objMstExportFarmerDisbAmount.lspath);
            objMstExportFarmerDisbAmount.status = true;
            objMstExportFarmerDisbAmount.message = "Success";
        }

        // Maker Farmer Disbursement Amount Import
        public void DaImportExcelFarmerDisbAmount(HttpRequest httpRequest, string employee_gid, result objResult, MdlExcelImportApplication objMstExportFarmerDisbAmount)
        {
            try
            {
                int insertCount = 0;
                HttpFileCollection httpFileCollection;
                DataTable dt = null;
                string lspath, lsfilePath;
                string rmdisbursementrequest_gid = httpRequest.Form["rmdisbursementrequest_gid"];
                string application_gid = httpRequest.Form["application_gid"];

                msSQL = " select company_code from adm_mst_tcompany";
                lscompany_code = objdbconn.GetExecuteScalar(msSQL);

                // Create Directory
                lsfilePath = HttpContext.Current.Server.MapPath("../../../erpdocument" + "/" + lscompany_code + "/Master/IndividualDisbAmountDtl/" + DateTime.Now.Year + "/" + DateTime.Now.Month);

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

                //path creation        
                lspath = lsfilePath + "/";
                FileStream file = new FileStream(lspath + lsfile_gid, FileMode.Create, FileAccess.Write);
                ms.WriteTo(file);

                using (ExcelPackage xlPackage = new ExcelPackage(ms))
                {
                    ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets[1];
                    rowCount = worksheet.Dimension.End.Row;
                }

                bool status;
                status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Master/IndividualDisbAmountDtl/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);


                file.Close();
                ms.Close();

                objcmnfunctions.uploadFile(lspath, lsfile_gid);

                //Excel To DataTable

                lsfilePath = @"" + lsfilePath.Replace("/", "\\") + "\\" + lsfile_gid + "";
                excelRange = "A1:E" + rowCount.ToString();
                dt = objcmnfunctions.ExcelToDataTable(lsfilePath, excelRange);
                try
                {
                    dt = dt.Rows.Cast<DataRow>().Where(r => string.Join("", r.ItemArray).Trim() != string.Empty).CopyToDataTable();
                }
                catch (Exception ex)
                {
                    objResult.status = false;
                    objResult.message = "No Records Found";
                    return;
                }

                foreach (DataRow row in dt.Rows)
                {

                    lsapplication_no = row["Application Number"].ToString();

                    lspan_status = row["Pan Status"].ToString();

                    lsurnpan_number = row["Pan Number"].ToString();

                    lsindividual_name = row["Individual Name"].ToString();

                    lsdisbursement_amount = row["Disbursement Amount"].ToString();

                    msSQL = " update ocs_trn_tfarmercontact set creditopsdisbursement_amount ='" + lsdisbursement_amount + "'" +
                            " where rmdisbursementrequest_gid ='" + rmdisbursementrequest_gid + "' and " +
                            " application_gid ='" + application_gid + "' and " +
                            " concat(first_name, ' ', middle_name, ' / ', last_name) = '" + lsindividual_name + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                }

                if (mnResult != 0)
                {
                    objResult.status = true;
                    objResult.message = "Disbursement Amount Added Successfully";
                }
                else
                {
                    objResult.status = false;
                    objResult.message = "Error occured in uploading Disbursement Amount Details";
                }

                dt.Dispose();

            }
            catch (Exception ex)
            {
                objResult.status = false;
                objResult.message = ex.ToString();
            }
        }

        // Checker Farmer Disbursement Amount Export
        public void DaGetExportCheckerFarmerDisbAmount(string rmdisbursementrequest_gid, string application_gid, MdlExcelImportApplication objMstExportFarmerDisbAmount)
        {
            msSQL = " select application_no as 'Application Number',pan_status as 'Pan Status',pan_no as 'Pan Number', " +
                    " concat(a.first_name, ' ', a.middle_name, ' / ', a.last_name) as 'Individual Name'," +
                    " creditopscheckerdisbursement_amount as 'Disbursement Amount'" +
                    " from ocs_trn_tfarmercontact a " +
                    " where rmdisbursementrequest_gid='" + rmdisbursementrequest_gid + "' and " +
                    " application_gid='" + application_gid + "'" +
                    " order by a.created_date ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            string lscompany_code = string.Empty;
            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            var workSheet = excel.Workbook.Worksheets.Add("Checker Individual Disbursement Amount Details");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                objMstExportFarmerDisbAmount.lsname = "Checker Individual Disbursement Amount Details.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/Checker Individual Disbursement Amount Details/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                objMstExportFarmerDisbAmount.lscloudpath = lscompany_code + "/" + "Master/Checker Individual Disbursement Amount Details/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstExportFarmerDisbAmount.lsname;
                objMstExportFarmerDisbAmount.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/Checker Individual Disbursement Amount Details/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstExportFarmerDisbAmount.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(objMstExportFarmerDisbAmount.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 22])  //Address "A1:A9"
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);
                }
                excel.SaveAs(ms);
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", objMstExportFarmerDisbAmount.lscloudpath, ms);
                status = objcmnstorage.UploadStream("erpdocument", objMstExportFarmerDisbAmount.lscloudpath, ms);
                ms.Close();

            }
            catch (Exception ex)
            {
                objMstExportFarmerDisbAmount.status = false;
                objMstExportFarmerDisbAmount.message = "Failure";
            }
            objMstExportFarmerDisbAmount.lscloudpath = objcmnstorage.EncryptData(objMstExportFarmerDisbAmount.lscloudpath);
            objMstExportFarmerDisbAmount.lspath = objcmnstorage.EncryptData(objMstExportFarmerDisbAmount.lspath);
            objMstExportFarmerDisbAmount.status = true;
            objMstExportFarmerDisbAmount.message = "Success";
        }

        // Checker Farmer Disbursement Amount Import
        public void DaImportExcelCheckerFarmerDisbAmount(HttpRequest httpRequest, string employee_gid, result objResult, MdlExcelImportApplication objMstExportCheckerFarmerDisbAmount)
        {
            try
            {
                int insertCount = 0;
                HttpFileCollection httpFileCollection;
                DataTable dt = null;
                string lspath, lsfilePath;
                string rmdisbursementrequest_gid = httpRequest.Form["rmdisbursementrequest_gid"];
                string application_gid = httpRequest.Form["application_gid"];

                msSQL = " select company_code from adm_mst_tcompany";
                lscompany_code = objdbconn.GetExecuteScalar(msSQL);

                // Create Directory
                lsfilePath = HttpContext.Current.Server.MapPath("../../../erpdocument" + "/" + lscompany_code + "/Master/CheckerIndividualDisbAmountDtl/" + DateTime.Now.Year + "/" + DateTime.Now.Month);

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

                //path creation        
                lspath = lsfilePath + "/";
                FileStream file = new FileStream(lspath + lsfile_gid, FileMode.Create, FileAccess.Write);
                ms.WriteTo(file);

                using (ExcelPackage xlPackage = new ExcelPackage(ms))
                {
                    ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets[1];
                    rowCount = worksheet.Dimension.End.Row;
                }

                bool status;
                status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Master/CheckerIndividualDisbAmountDtl/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);


                file.Close();
                ms.Close();

                objcmnfunctions.uploadFile(lspath, lsfile_gid);

                //Excel To DataTable

                lsfilePath = @"" + lsfilePath.Replace("/", "\\") + "\\" + lsfile_gid + "";
                excelRange = "A1:E" + rowCount.ToString();
                dt = objcmnfunctions.ExcelToDataTable(lsfilePath, excelRange);
                try
                {
                    dt = dt.Rows.Cast<DataRow>().Where(r => string.Join("", r.ItemArray).Trim() != string.Empty).CopyToDataTable();
                }
                catch (Exception ex)
                {
                    objResult.status = false;
                    objResult.message = "No Records Found";
                    return;
                }

                foreach (DataRow row in dt.Rows)
                {

                    lsapplication_no = row["Application Number"].ToString();

                    lspan_status = row["Pan Status"].ToString();

                    lsurnpan_number = row["Pan Number"].ToString();

                    lsindividual_name = row["Individual Name"].ToString();

                    lsdisbursement_amount = row["Disbursement Amount"].ToString();

                    msSQL = " update ocs_trn_tfarmercontact set creditopscheckerdisbursement_amount ='" + lsdisbursement_amount + "'" +
                            " where rmdisbursementrequest_gid ='" + rmdisbursementrequest_gid + "' and " +
                            " application_gid ='" + application_gid + "' and " +
                            " concat(first_name, ' ', middle_name, ' / ', last_name) = '" + lsindividual_name + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                }

                if (mnResult != 0)
                {
                    objResult.status = true;
                    objResult.message = "Disbursement Amount Added Successfully";
                }
                else
                {
                    objResult.status = false;
                    objResult.message = "Error occured in uploading Disbursement Amount Details";
                }

                dt.Dispose();

            }
            catch (Exception ex)
            {
                objResult.status = false;
                objResult.message = ex.ToString();
            }
        }
        public void DaGetSupplierNameDropDown(string employee_gid, MdlMstSupplierName values)
        {
            msSQL = " select supplier_gid, supplier_name " +
                    " from ocs_mst_tsupplier  where status='Y' order by created_date desc ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdispsupplier_list = new List<dispsupplier_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getdispsupplier_list.Add(new dispsupplier_list
                    {
                        supplier_gid = (dr_datarow["supplier_gid"].ToString()),
                        supplier_name = (dr_datarow["supplier_name"].ToString()),

                    });
                }
                values.dispsupplier_list = getdispsupplier_list;
            }
            dt_datatable.Dispose();
        }
        public void DaGetSupplierIfscCodeDropDown(string supplier_gid, string employee_gid, MdlMstSupplierIfscCode values)
        {
            msSQL = " select supplier2bank_gid,ifsc_code " +
                    " from ocs_mst_tsupplier2bank  where supplier_gid ='" + supplier_gid + "' order by created_date desc ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdispsupplierifsc_list = new List<dispsupplierifsc_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getdispsupplierifsc_list.Add(new dispsupplierifsc_list
                    {
                        supplier2bank_gid = (dr_datarow["supplier2bank_gid"].ToString()),
                        ifsc_code = (dr_datarow["ifsc_code"].ToString()),

                    });
                }
                values.dispsupplierifsc_list = getdispsupplierifsc_list;
            }
            dt_datatable.Dispose();
        }
        public void DaGetDispSuplBankAcctDtlView(string employee_gid, string supplier2bank_gid, MdlDispSuplBankAcctDtl values)
        {
            msSQL = " select bank_name,branch_name,bank_address,bankaccount_name,micr_code," +
                    " bankaccount_number,confirmbankaccountnumber,bankaccounttype_gid," +
                    " bankaccounttype_name,joinaccount_status,joinaccount_name,chequebook_status," +
                    " date_format(accountopen_date, '%d-%m-%Y') " +
                    
                    " from ocs_mst_tsupplier2bank a" +
                    " where supplier2bank_gid='" + supplier2bank_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.bank_name = objODBCDatareader["bank_name"].ToString();
                values.branch_name = objODBCDatareader["branch_name"].ToString();
                values.bank_address = objODBCDatareader["bank_address"].ToString();
                values.bankaccount_name = objODBCDatareader["bankaccount_name"].ToString();
                values.micr_code = objODBCDatareader["micr_code"].ToString();
                values.bankaccount_number = objODBCDatareader["bankaccount_number"].ToString();
                values.confirmbankaccountnumber = objODBCDatareader["confirmbankaccountnumber"].ToString();
                values.bankaccounttype_gid = objODBCDatareader["bankaccounttype_gid"].ToString();
                values.bankaccounttype_name = objODBCDatareader["bankaccounttype_name"].ToString();
                values.joinaccount_status = objODBCDatareader["joinaccount_status"].ToString();
                values.joinaccount_name = objODBCDatareader["joinaccount_name"].ToString();
                values.chequebook_status = objODBCDatareader["chequebook_status"].ToString();
                values.accountopen_date = objODBCDatareader["accountopen_date"].ToString();

                //if (objODBCDatareader["accountopen_date"].ToString() == "")
                //{
                //}
                //else
                //{
                //    values.accountopen_date = Convert.ToDateTime(objODBCDatareader["accountopen_date"]).ToString("MM-dd-yyyy");
                //}
            }
            objODBCDatareader.Close();
        }
        public void DaGetDisbursementEditSupplierSummary(string employee_gid, string rmdisbursementrequest_gid, MdlDisbSupplierBankAcct values)
        {
            try
            {
                msSQL = " select disbursementsupplier_gid,supplier_name,bank_name,branch_name,ifsc_code, " +
                        " bankaccount_number,accountholder_name,disbursement_amount, disbursement_amount as rmdisbursement_amount," +
                        " concat(f.user_firstname, ' ', f.user_lastname, ' / ', f.user_code) as created_by, " +
                        " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date,creditopsdisbursement_amount, " +
                        " creditopscheckerdisbursement_amount,a.disbursementbookingencore_status from ocs_trn_tdisbursementsupplier a " +
                        " left join hrm_mst_temployee e on e.employee_gid = a.created_by " +
                        " left join adm_mst_tuser f on f.user_gid = e.user_gid " +
                        " where rmdisbursementrequest_gid='" + rmdisbursementrequest_gid + "' or " +
                        " rmdisbursementrequest_gid='" + employee_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getdisbsupplierdtl_list = new List<disbsupplierdtl_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getdisbsupplierdtl_list.Add(new disbsupplierdtl_list
                        {
                            supplier_name = dt["supplier_name"].ToString(),
                            bank_name = dt["bank_name"].ToString(),
                            branch_name = dt["branch_name"].ToString(),
                            ifsc_code = dt["ifsc_code"].ToString(),
                            bankaccount_number = dt["bankaccount_number"].ToString(),
                            accountholder_name = dt["accountholder_name"].ToString(),
                            disbursementsupplier_gid = dt["disbursementsupplier_gid"].ToString(),
                            disbursement_amount = dt["disbursement_amount"].ToString(),
                            rmdisbursement_amount = dt["rmdisbursement_amount"].ToString(),
                            created_by = dt["created_by"].ToString(),
                            created_date = dt["created_date"].ToString(),
                            creditopsdisbursement_amount = dt["creditopsdisbursement_amount"].ToString(),
                            creditopscheckerdisbursement_amount = dt["creditopscheckerdisbursement_amount"].ToString(),
                            disbursementbookingencore_status = dt["disbursementbookingencore_status"].ToString()
                        });
                        values.disbsupplierdtl_list = getdisbsupplierdtl_list;
                    }
                }
                dt_datatable.Dispose();
            }
            catch
            {
                values.status = false;
            }
        }
        // Co-Applicant Details Export Excel
        public void DaGetCoApplicantExportExcel(string employee_gid, string application_gid, MdlExcelImportApplication objMstCoApplicantExportExcel)
        {
            msSQL = " select a.application_no as 'Application Number',a.pan_status as 'Pan Status',a.pan_no as 'Pan Number', " +
                    " concat(a.first_name, ' ', a.middle_name, ' / ', a.last_name) as 'Individual Name',a.farmercontact_gid," +
                    " b.aadhar_no as '* Aadhar Number'," +
                    " b.pan_status as '* Pan Status(Yes / No)',b.pan_no as '* Pan Number(If PAN Status is Yes, PAN Value is mandatory)'," +
                    " b.first_name as '* First Name',b.middle_name as 'Middle Name',b.last_name as '* Last Name'," +
                    " b.individual_dob as '* Date of Birth(DD-MM-YYYY)',b.age as 'Age',b.gender_name as '* Gender'," +
                    " b.designation_name as '* Designation',b.pep_status as '* Politically Exposed person (PEP)(Yes/No)'," +
                    " b.pepverified_date as '* PEP Verified On(DD-MM-YYYY)',b.maritalstatus_name as '* Marital Status'," +
                    " b.educationalqualification_name as '* Educational Qualification',b.main_occupation as '* Main Occupation'," +
                    " b.annual_income as '* Annual Income',b.monthly_income as 'Monthly Income'," +
                    " b.currentresidence_years as '* Years in Current Residence'," +
                    " b.branch_distance as '* Distance from Branch/Regional Office (in Kms)'," +
                    " b.father_firstname as '* Father s First Name',b.father_middlename as 'Father s Middle Name'," +
                    " b.father_lastname as '* Father s Last Name',b.fathernominee_status as 'Father s Nominee Status(Yes/No)'," +
                    " b.father_dob as 'Father s Date of Birth(DD-MM-YYYY)',b.father_age as 'Father s Age'," +
                    " b.mother_firstname as 'Mother s First Name',b.mother_middlename as 'Mother s Middle Name'," +
                    " b.mother_lastname as 'Mother s Last Name',b.mothernominee_status as 'Mother s Nominee Status(Yes/No)'," +
                    " b.mother_dob as 'Mother s Date of Birth(DD-MM-YYYY)',b.mother_age as 'Mother s Age'," +
                    " b.spouse_firstname as 'Spouse First Name',b.spouse_middlename as 'Spouse Middle Name'," +
                    " b.spouse_lastname as 'Spouse Last Name', b.spousenominee_status as 'Spouse Nominee Status(Yes/No)'," +
                    " b.spouse_dob as 'Spouse Date of Birth(DD-MM-YYYY)',b.spouse_age as 'Spouse Age'," +
                    " b.mobile_no as '* Mobile Number',b.primary_status as '* Primary Status(Yes/No)'," +
                    " b.whatsapp_no as '* Whatsapp Number(Yes/No)'," +
                    " b.email_address as '* Email Address',b.emailprimary_status as '* Email Primary Status(Yes/No)'," +
                    " b.addresstype_name as '* Address Type',b.addressprimary_status as '* Address Primary Status'," +
                    " b.addressline1 as '* Address Line 1',b.addressline2 as 'Address Line 2',b.landmark as 'Land Mark'," +
                    " b.postal_code as '* Postal Code',b.city as 'City',b.taluka as 'Taluka'," +
                    " b. district as '* District',b.state as '* State',b.country as '* Country',b.latitude as 'Latitude'," +
                    " b.longitude as 'Longitude',b.ifsc_code as '* IFSC Code',b.bank_name as 'Bank Name'," +
                    " b.branch_name as 'Branch Name',b.branch_address as 'Branch Address',b.micr_code as 'MICR Code'," +
                    " b.bankaccount_number as '* Bank Account Number',b.confirmbankaccount_number as '* Confirm Account Number'," +
                    " b.accountholder_name as '* Account Holder Name'" +
                    " from ocs_trn_tfarmercontact a " +
                    " left join ocs_trn_tcontactcoapplicant b on b.farmercontact_gid = a.farmercontact_gid " +
                    " where a.rmdisbursementrequest_gid='" + employee_gid + "' and " +
                    " a.application_gid='" + application_gid + "' and  " +
                    " a.farmercontact_gid not in (select farmercontact_gid from ocs_trn_tcontactcoapplicant c where " +
                    " c.rmdisbursementrequest_gid='" + employee_gid + "') " +
                    " order by a.created_date ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            string lscompany_code = string.Empty;
            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            var workSheet = excel.Workbook.Worksheets.Add("Individual CoApplicant Details");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                objMstCoApplicantExportExcel.lsname = "Individual CoApplicant Details.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/Individual CoApplicant Details/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                objMstCoApplicantExportExcel.lscloudpath = lscompany_code + "/" + "Master/Individual CoApplicant Details/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstCoApplicantExportExcel.lsname;
                objMstCoApplicantExportExcel.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/Individual CoApplicant Details/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstCoApplicantExportExcel.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(objMstCoApplicantExportExcel.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 68])  //Address "A1:BP1"
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);
                }
               
                workSheet.Cells.Style.Locked = false;
                workSheet.Column(1).Style.Locked = true;
                workSheet.Column(2).Style.Locked = true;
                workSheet.Column(3).Style.Locked = true;
                workSheet.Column(4).Style.Locked = true;
               
                workSheet.Column(1).AutoFit();
                workSheet.Column(2).AutoFit();
                workSheet.Column(3).AutoFit();
                workSheet.Column(4).AutoFit();
                workSheet.Cells.Style.Hidden = false;
                workSheet.Column(5).Hidden = true;

                workSheet.Protection.AllowEditObject = true;
                workSheet.Protection.AllowEditScenarios = true;
                workSheet.Protection.AllowFormatCells = true;
                workSheet.Protection.AllowFormatColumns = true;
                workSheet.Protection.AllowFormatRows = true;
                workSheet.Protection.IsProtected = true;
                workSheet.Protection.SetPassword("Welcome@123");

                excel.SaveAs(ms);
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", objMstCoApplicantExportExcel.lscloudpath, ms);
                status = objcmnstorage.UploadStream("erpdocument", objMstCoApplicantExportExcel.lscloudpath, ms);
                ms.Close();

            }
            catch (Exception ex)
            {
                objMstCoApplicantExportExcel.status = false;
                objMstCoApplicantExportExcel.message = "Failure";
            }
            objMstCoApplicantExportExcel.lscloudpath = objcmnstorage.EncryptData(objMstCoApplicantExportExcel.lscloudpath);
            objMstCoApplicantExportExcel.lspath = objcmnstorage.EncryptData(objMstCoApplicantExportExcel.lspath);
            objMstCoApplicantExportExcel.status = true;
            objMstCoApplicantExportExcel.message = "Success";
        }
        // Co-Applicant Details Import Excel
        public void DaImportExcelDisbCoApplicant(HttpRequest httpRequest, string employee_gid, result objResult)
        {
            try
            {
                int insertCount = 0;
                HttpFileCollection httpFileCollection;
                DataTable dt = null;
                string lspath, lsfilePath;
                string application_gid = httpRequest.Form["application_gid"];

                msSQL = " select company_code from adm_mst_tcompany";
                lscompany_code = objdbconn.GetExecuteScalar(msSQL);

                // Create Directory
                lsfilePath = HttpContext.Current.Server.MapPath("../../../erpdocument" + "/" + lscompany_code + "/Master/DisbursementCoApplicantExportDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month);

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

                //path creation        
                lspath = lsfilePath + "/";
                FileStream file = new FileStream(lspath + lsfile_gid, FileMode.Create, FileAccess.Write);
                ms.WriteTo(file);

                using (ExcelPackage xlPackage = new ExcelPackage(ms))
                {
                    ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets[1];
                    rowCount = worksheet.Dimension.End.Row;
                    columnCount = worksheet.Dimension.End.Column;
                    endRange = worksheet.Dimension.End.Address;
                }

                bool status;
                status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Master/DisbursementCoApplicantExportDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);


                file.Close();
                ms.Close();

                objcmnfunctions.uploadFile(lspath, lsfile_gid);

                //Excel To DataTable

                lsfilePath = @"" + lsfilePath.Replace("/", "\\") + "\\" + lsfile_gid + "";

                excelRange = "A1:BP" + rowCount.ToString();
                dt = objcmnfunctions.ExcelToDataTable(lsfilePath, excelRange);
                try
                {
                    dt = dt.Rows.Cast<DataRow>().Where(r => string.Join("", r.ItemArray).Trim() != string.Empty).CopyToDataTable();
                }
                catch (Exception ex)
                {
                    objResult.status = false;
                    objResult.message = "No Records Found";
                    return;
                }

                Nullable<DateTime> ldpepverified_date, ldfather_dob, ldmother_dob, ldspouse_dob, ldaccountopendate;

                foreach (DataRow row in dt.Rows)
                {
                    coapplicantimportlog_message = "";

                    lsaadhar_no = row["* Aadhar Number"].ToString();
                    if (!string.IsNullOrEmpty(lsaadhar_no))
                    {

                    lsapplication_no = row["Application Number"].ToString();
                    if (lsapplication_no == "")
                    {

                    }
                    else
                    {
                        lsfarmerpan_status = row["Pan Status"].ToString();
                        lsfarmerpan_number = row["Pan Number"].ToString();
                        lsfarmerindividual_name = row["Individual Name"].ToString();
                        lsfarmercontact_gid = row["farmercontact_gid"].ToString();
                        lsaadhar_no = row["* Aadhar Number"].ToString();
                        lspan_status = row["* Pan Status(Yes / No)"].ToString();
                        lspan_no = row["* Pan Number(If PAN Status is Yes, PAN Value is mandatory)"].ToString();
                        lsfirst_name = row["* First Name"].ToString();
                        lsmiddle_name = row["Middle Name"].ToString();
                        lslast_name = row["* Last Name"].ToString();

                        lsindividual_dob = row["* Date of Birth(DD-MM-YYYY)"].ToString();

                        if (lsindividual_dob.Length > 10)
                        {
                            lsindividual_dob = dateFormatStandardizer(lsindividual_dob);
                        }

                        lsage = row["Age"].ToString();
                        lsgender_name = row["* Gender"].ToString();
                        msSQL = "select gender_gid from ocs_mst_tgender where gender_name='" + row["* Gender"].ToString() + "'";
                        lsgender_gid = objdbconn.GetExecuteScalar(msSQL);

                        lsdesignation_type = row["* Designation"].ToString();
                        msSQL = "select designation_gid from ocs_mst_tdesignation where designation_type='" + row["* Designation"].ToString() + "'";
                        lsdesignation_gid = objdbconn.GetExecuteScalar(msSQL);

                        lspep_status = row["* Politically Exposed person (PEP)(Yes/No)"].ToString();

                        lspepverified_date = row["* PEP Verified On(DD-MM-YYYY)"].ToString();
                        if (lspepverified_date.Length > 10)
                        {
                            lspepverified_date = dateFormatStandardizer(lspepverified_date);
                        }
                        lspepverified_date = lspepverified_date.Replace('-', '/');
                        ldpepverified_date = DateTime.ParseExact(lspepverified_date, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                        lsmaritalstatus_name = row["* Marital Status"].ToString();
                        msSQL = "select maritalstatus_gid from ocs_mst_tmaritalstatus where maritalstatus_name='" + row["* Marital Status"].ToString() + "'";
                        lsmaritalstatus_gid = objdbconn.GetExecuteScalar(msSQL);

                        lseducationalqualification_name = row["* Educational Qualification"].ToString();
                        msSQL = "select educationalqualification_gid from ocs_mst_teducationalqualification where educationalqualification_name='" + row["* Educational Qualification"].ToString() + "'";
                        lseducationalqualification_gid = objdbconn.GetExecuteScalar(msSQL);

                        lsmain_occupation = row["* Main Occupation"].ToString();
                        lsannual_income = row["* Annual Income"].ToString();
                        lsmonthly_income = row["Monthly Income"].ToString();
                        lsyearscurrentresidece = row["* Years in Current Residence"].ToString();
                        lsdistancebranch = row["* Distance from Branch/Regional Office (in Kms)"].ToString();

                        lsfather_firstname = row["* Father s First Name"].ToString();
                        lsfather_middlename = row["Father s Middle Name"].ToString();
                        lsfather_lastname = row["* Father s Last Name"].ToString();
                        lsfathernominee_status = row["Father s Nominee Status(Yes/No)"].ToString();

                        lsfather_dob = row["Father s Date of Birth(DD-MM-YYYY)"].ToString();
                        if (lsfather_dob.Length > 10)
                        {
                            lsfather_dob = dateFormatStandardizer(lsfather_dob);
                        }
                        if (lsfather_dob.Length > 0)
                        {
                            lsfather_dob = lsfather_dob.Replace('-', '/');
                            ldfather_dob = DateTime.ParseExact(lsfather_dob, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            ldfather_dob = null;
                        }
                        lsfathers_age = row["Father s Age"].ToString();

                        lsmother_firstname = row["Mother s First Name"].ToString();
                        lsmother_middlename = row["Mother s Middle Name"].ToString();
                        lsmother_lastname = row["Mother s Last Name"].ToString();
                        lsmothernominee_status = row["Mother s Nominee Status(Yes/No)"].ToString();

                        lsmother_dob = row["Mother s Date of Birth(DD-MM-YYYY)"].ToString();
                        if (lsmother_dob.Length > 10)
                        {
                            lsmother_dob = dateFormatStandardizer(lsmother_dob);
                        }
                        if (lsmother_dob.Length > 0)
                        {
                            lsmother_dob = lsmother_dob.Replace('-', '/');
                            ldmother_dob = DateTime.ParseExact(lsmother_dob, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            ldmother_dob = null;
                        }
                        lsmothers_age = row["Mother s Age"].ToString();

                        lsspouse_firstname = row["Spouse First Name"].ToString();
                        lsspouse_middlename = row["Spouse Middle Name"].ToString();
                        lsspouse_lastname = row["Spouse Last Name"].ToString();
                        lsspousenominee_status = row["Spouse Nominee Status(Yes/No)"].ToString();

                        lsspouse_dob = row["Spouse Date of Birth(DD-MM-YYYY)"].ToString();
                        if (lsspouse_dob.Length > 10)
                        {
                            lsspouse_dob = dateFormatStandardizer(lsspouse_dob);
                        }
                        if (lsspouse_dob.Length > 0)
                        {
                            lsspouse_dob = lsspouse_dob.Replace('-', '/');
                            ldspouse_dob = DateTime.ParseExact(lsspouse_dob, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            ldspouse_dob = null;
                        }
                        lsspouse_age = row["Spouse Age"].ToString();                        

                        lsmobile_no = row["* Mobile Number"].ToString();
                        lsmobileprimary_status = row["* Primary Status(Yes/No)"].ToString();
                        lswhatsapp_no = row["* Whatsapp Number(Yes/No)"].ToString();

                        lsemail_address = row["* Email Address"].ToString();
                        lsemailprimary_status = row["* Email Primary Status(Yes/No)"].ToString();

                        lsaddresstype_name = row["* Address Type"].ToString();
                        msSQL = "select address_gid from ocs_mst_taddresstype where address_type='" + row["* Address Type"].ToString() + "'";
                        lsaddresstype_gid = objdbconn.GetExecuteScalar(msSQL);

                        lsaddressprimary_status = row["* Address Primary Status"].ToString();
                        lsaddressline1 = row["* Address Line 1"].ToString();
                        lsaddressline2 = row["Address Line 2"].ToString();
                        lslandmark = row["Land Mark"].ToString();
                        lspostal_code = row["* Postal Code"].ToString();

                        msSQL = " select city,taluka,district,state from ocs_mst_tpostalcode where " +
                           "postalcode_value='" + lspostal_code + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lscity = objODBCDatareader["city"].ToString();
                            lstaluka = objODBCDatareader["taluka"].ToString();
                            lsdistrict = objODBCDatareader["district"].ToString();
                            lsstate = objODBCDatareader["state"].ToString();
                        }
                        objODBCDatareader.Close();
                        lscountry = row["* Country"].ToString();
                        lslatitude = row["Latitude"].ToString();
                        lslongitude = row["Longitude"].ToString();

                        lsifsccode = row["* IFSC Code"].ToString();
                        lsbankname = row["Bank Name"].ToString();
                        lsbranchname = row["Branch Name"].ToString();
                        lsbranchaddress = row["Branch Address"].ToString();
                        lsmicrcode = row["MICR Code"].ToString();
                        lsbankaccountnumber = row["* Bank Account Number"].ToString();
                        lsconfirmbankaccountnumber = row["* Confirm Account Number"].ToString();
                        lsaccountholdername = row["* Account Holder Name"].ToString();

                        if (coapplicantimportlog_message == "")
                        {
                            if (lspan_status == "Yes" && lspan_no == "")
                            {
                                lspanstatusvalue = "empty";
                            }

                            if ((lspan_status == "") || (lsaadhar_no == "") || (lsfirst_name == "") || (lsdesignation_type == "")
                            || (lslast_name == "") || (lsindividual_dob == "") || (lsgender_name == "") || (lspep_status == "")
                            || (lspepverified_date == "") || (lsuser_type == "") || (lsmaritalstatus_name == "") || (lsyearscurrentresidece == "")
                            || (lsfather_firstname == "") || (lsfather_lastname == "") || (lseducationalqualification_name == "")
                            || (lsmain_occupation == "") || (lsannual_income == "") || (lsmobileprimary_status == "") || (lsaddressprimary_status == "")
                            || (lsmobile_no == "") || (lswhatsapp_no == "") || (lsemail_address == "") || (lsaddresstype_name == "")
                            || (lsaddressline1 == "") || (lsaddressline1 == "") || (lsemailprimary_status == "")
                            || (lspostal_code == "") || (lscountry == "") || (lspanstatusvalue == "empty")
                            || (lsifsccode == "") || (lsbankaccountnumber == "") || (lsaccountholdername == "") || (lsaccountholdername == ""))
                            {
                                coapplicantimportlog_message = "Mandatory fields are empty";
                            }


                            if (coapplicantimportlog_message != "")
                            {

                                msGetGid = objcmnfunctions.GetMasterGID("COAL");

                                msSQL = " insert into ocs_trn_tcontactcoapplicantlog(" +
                                        " contactcoapplicantlog_gid," +
                                        " application_gid," +
                                        " application_no," +

                                        " pan_status," +
                                        " pan_no," +
                                        " aadhar_no," +
                                        " first_name," +
                                        " middle_name," +
                                        " last_name," +
                                        " individual_dob," +

                                        " gender_name," +
                                        " designation_name," +
                                        " pep_status," +
                                        " pepverified_date," +
                                        " maritalstatus_name," +

                                        " father_firstname," +
                                        " father_middlename," +
                                        " father_lastname," +
                                        " fathernominee_status," +
                                        " father_dob," +

                                        " mother_firstname," +
                                        " mother_middlename," +
                                        " mother_lastname," +
                                        " mothernominee_status," +
                                        " mother_dob," +

                                        " spouse_firstname," +
                                        " spouse_middlename," +
                                        " spouse_lastname," +
                                        " spousenominee_status," +
                                        " spouse_dob," +

                                        " educationalqualification_name," +
                                        " main_occupation," +
                                        " annual_income," +
                                        " monthly_income," +
                                        " incometype_name," +

                                        " mobile_no," +
                                        " whatsapp_no," +
                                        " email_address," +
                                        " addresstype_name," +
                                        " addressline1," +
                                        " addressline2," +
                                        " postal_code," +
                                        " country," +

                                        " contactimportlog_status," +
                                        " ifsc_code," +
                                        " bank_name," +
                                        " branch_name," +
                                        " branch_address," +
                                        " micr_code," +
                                        " bankaccount_number," +
                                        " accountholder_name," +
                                        " created_by," +
                                        " created_date)" +
                                        " values(" +
                                        "'" + msGetGid + "'," +
                                        "'" + application_gid + "'," +
                                        "'" + lsapplication_no + "'," +
                                        "'" + lspan_status + "'," +
                                        "'" + lspan_no + "'," +
                                                         "'" + lsaadhar_no + "'," +
                                                         "'" + lsfirst_name + "'," +
                                                          "'" + lsmiddle_name + "'," +
                                                          "'" + lslast_name + "'," +
                                                          "'" + lsindividual_dob + "'," +

                                                          "'" + lsgender_name + "'," +
                                                          "'" + lsdesignation_type + "'," +
                                                          "'" + lspep_status + "'," +
                                                          "'" + Convert.ToDateTime(ldpepverified_date).ToString("yyyy-MM-dd HH:mm:ss") + "'," +

                                                          "'" + lsmaritalstatus_name + "'," +

                                                          "'" + lsfather_firstname + "'," +
                                                          "'" + lsfather_middlename + "'," +
                                                          "'" + lsfather_lastname + "'," +
                                                          "'" + lsfathernominee_status + "',";

                                if ((ldfather_dob == null))
                                {
                                    msSQL += "null,";
                                }
                                else
                                {
                                    msSQL += "'" + Convert.ToDateTime(ldfather_dob).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                                }



                                msSQL += "'" + lsmother_firstname + "'," +
                                          "'" + lsmother_middlename + "'," +
                                          "'" + lsmother_lastname + "'," +
                                          "'" + lsmothernominee_status + "',";

                                if ((ldmother_dob == null))
                                {
                                    msSQL += "null,";
                                }
                                else
                                {
                                    msSQL += "'" + Convert.ToDateTime(ldmother_dob).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                                }

                                msSQL += "'" + lsspouse_firstname + "'," +
                                          "'" + lsspouse_middlename + "'," +
                                          "'" + lsspouse_lastname + "'," +
                                          "'" + lsspousenominee_status + "',";

                                if ((ldspouse_dob == null))
                                {
                                    msSQL += "null,";
                                }
                                else
                                {
                                    msSQL += "'" + Convert.ToDateTime(ldspouse_dob).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                                }

                                msSQL += "'" + lseducationalqualification_name + "'," +
                                         "'" + lsmain_occupation + "'," +
                                         "'" + lsannual_income + "'," +
                                         "'" + lsmonthly_income + "'," +
                                         "'" + lsincometype_name + "'," +

                                         "'" + lsmobile_no + "'," +
                                         "'" + lswhatsapp_no + "'," +
                                         "'" + lsemail_address + "'," +
                                         "'" + lsaddresstype_name + "',";
                                if (lsaddressline1 == "" || lsaddressline1 == null)
                                {
                                    msSQL += "'',";
                                }
                                else
                                {
                                    msSQL += "'" + lsaddressline1.Replace("'", "") + "',";
                                }
                                if (lsaddressline2 == "" || lsaddressline2 == null)
                                {
                                    msSQL += "'',";
                                }
                                else
                                {
                                    msSQL += "'" + lsaddressline2.Replace("'", "") + "',";
                                }

                                msSQL += "'" + lspostal_code + "'," +
                                       "'" + lscountry + "'," +

                                       "'" + coapplicantimportlog_message + "'," +
                                       "'" + lsifsccode + "'," +
                                       "'" + lsbankname + "'," +
                                       "'" + lsbranchname + "'," +
                                       "'" + lsbranchaddress + "'," +
                                       "'" + lsmicrcode + "'," +
                                       "'" + lsbankaccountnumber + "'," +
                                       "'" + lsaccountholdername + "'," +
                                         "'" + employee_gid + "'," +
                                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                logCount++;
                            }
                            else
                            {
                                if (lspan_status == "Yes")
                                {
                                    lspan_status = "Customer Submitting PAN";
                                }
                                else if (lspan_status == "No")
                                {
                                    lspan_status = "Customer Submitting Form 60";
                                }
                                else
                                {

                                }
                                msGetGid = objcmnfunctions.GetMasterGID("CCAG");

                               msSQL = " insert into ocs_trn_tcontactcoapplicant(" +
                                       " contactcoapplicant_gid," +
                                       " farmercontact_gid," +
                                       " application_gid," +
                                       " application_no," +
                                       " pan_status," +
                                       " pan_no," +
                                       " aadhar_no," +
                                       " first_name," +
                                       " middle_name," +
                                       " last_name," +
                                       " individual_dob," +
                                       " age, " +
                                       " gender_gid," +
                                       " gender_name," +
                                       " designation_gid," +
                                       " designation_name," +
                                       " pep_status," +
                                       " pepverified_date," +
                                       " maritalstatus_gid," +
                                       " maritalstatus_name," +

                                       " father_firstname," +
                                       " father_middlename," +
                                       " father_lastname," +
                                       " fathernominee_status," +
                                       " father_dob," +
                                       " father_age," +

                                       " mother_firstname," +
                                       " mother_middlename," +
                                       " mother_lastname," +
                                       " mothernominee_status," +
                                       " mother_dob," +
                                       " mother_age," +

                                       " spouse_firstname," +
                                       " spouse_middlename," +
                                       " spouse_lastname," +
                                       " spousenominee_status," +
                                       " spouse_dob," +
                                       " spouse_age," +

                                       " educationalqualification_gid," +
                                       " educationalqualification_name," +
                                       " main_occupation," +
                                       " annual_income," +
                                       " monthly_income," +                                      

                                       " currentresidence_years," +
                                       " branch_distance," +

                                       " ifsc_code," +
                                       " bank_name," +
                                       " branch_name," +
                                       " branch_address," +
                                       " micr_code," +
                                       " bankaccount_number," +
                                       " confirmbankaccount_number," +
                                       " accountholder_name," +

                                       " mobile_no," +
                                       " primary_status," +
                                       " whatsapp_no," +

                                       " email_address," +
                                       " emailprimary_status," +

                                       " addresstype_gid," +
                                       " addresstype_name," +
                                       " addressprimary_status," +
                                       " addressline1," +
                                       " addressline2," +
                                       " landmark," +
                                       " postal_code," +
                                       " city," +
                                       " taluka," +
                                       " district," +
                                       " state," +
                                       " country," +
                                       " latitude," +
                                       " longitude," +
                                       " rmdisbursementrequest_gid," +
                                       " created_by," +
                                       " created_date)" +
                                       " values(" +
                                       "'" + msGetGid + "'," +
                                       "'" + lsfarmercontact_gid + "'," +
                                       "'" + application_gid + "'," +
                                       "'" + lsapplication_no + "'," +

                                       "'" + lspan_status + "'," +
                                       "'" + lspan_no + "'," +
                                       "'" + lsaadhar_no + "'," +
                                       "'" + lsfirst_name + "'," +
                                        "'" + lsmiddle_name + "'," +
                                        "'" + lslast_name + "'," +
                                        "'" + lsindividual_dob + "'," +
                                        "'" + lsage + "'," +
                                        "'" + lsgender_gid + "'," +
                                        "'" + lsgender_name + "'," +
                                        "'" + lsdesignation_gid + "'," +
                                        "'" + lsdesignation_type + "'," +
                                        "'" + lspep_status + "'," +
                                        "'" + Convert.ToDateTime(ldpepverified_date).ToString("yyyy-MM-dd HH:mm:ss") + "'," +

                                        "'" + lsmaritalstatus_gid + "'," +
                                        "'" + lsmaritalstatus_name + "'," +

                                        "'" + lsfather_firstname + "'," +
                                        "'" + lsfather_middlename + "'," +
                                        "'" + lsfather_lastname + "'," +
                                        "'" + lsfathernominee_status + "',";

                                if ((ldfather_dob == null))
                                {
                                    msSQL += "null,";
                                }
                                else
                                {
                                    msSQL += "'" + Convert.ToDateTime(ldfather_dob).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                                }

                                msSQL += "'" + lsfathers_age + "'," +
                                         "'" + lsmother_firstname + "'," +
                                         "'" + lsmother_middlename + "'," +
                                         "'" + lsmother_lastname + "'," +
                                         "'" + lsmothernominee_status + "',";

                                if ((ldmother_dob == null))
                                {
                                    msSQL += "null,";
                                }
                                else
                                {
                                    msSQL += "'" + Convert.ToDateTime(ldmother_dob).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                                }

                                msSQL += "'" + lsmothers_age + "'," +
                                         "'" + lsspouse_firstname + "'," +
                                         "'" + lsspouse_middlename + "'," +
                                         "'" + lsspouse_lastname + "'," +
                                         "'" + lsspousenominee_status + "',";

                                if ((ldspouse_dob == null))
                                {
                                    msSQL += "null,";
                                }
                                else
                                {
                                    msSQL += "'" + Convert.ToDateTime(ldspouse_dob).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                                }

                                msSQL +=  "'" + lsspouse_age + "'," +
                                          "'" + lseducationalqualification_gid + "'," +
                                          "'" + lseducationalqualification_name + "'," +
                                          "'" + lsmain_occupation + "'," +
                                          "'" + lsannual_income + "'," +
                                          "'" + lsmonthly_income + "'," +
                                         
                                          "'" + lsyearscurrentresidece + "'," +
                                          "'" + lsdistancebranch + "'," +

                                          "'" + lsifsccode + "'," +
                                          "'" + lsbankname + "'," +
                                          "'" + lsbranchname + "'," +
                                          "'" + lsbranchaddress + "'," +
                                          "'" + lsmicrcode + "'," +
                                          "'" + lsbankaccountnumber + "'," +
                                          "'" + lsconfirmbankaccountnumber + "'," +
                                          "'" + lsaccountholdername + "'," +

                                         "'" + lsmobile_no + "'," +
                                         "'" + lsmobileprimary_status + "'," +
                                         "'" + lswhatsapp_no + "'," +

                                          "'" + lsemail_address + "'," +
                                          "'" + lsemailprimary_status + "'," +

                                          "'" + lsaddresstype_gid + "'," +
                                          "'" + lsaddresstype_name + "'," +
                                          "'" + lsaddressprimary_status + "'," +
                                          "'" + lsaddressline1 + "'," +
                                          "'" + lsaddressline2 + "'," +
                                          "'" + lslandmark + "'," +
                                          "'" + lspostal_code + "'," +
                                          "'" + lscity + "'," +
                                          "'" + lstaluka + "'," +
                                          "'" + lsdistrict + "'," +
                                          "'" + lsstate + "'," +
                                          "'" + lscountry + "'," +
                                          "'" + lslatitude + "'," +
                                          "'" + lslongitude + "'," +

                                         "'" + employee_gid + "'," +
                                         "'" + employee_gid + "'," +
                                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                if (mnResult == 1)
                                {
                                    insertCount++;
                                }
                                else
                                {
                                    msSQL = "delete from ocs_trn_tcontactcoapplicant where contactcoapplicant_gid ='" + msGetGid + "'";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                }
                            }
                        }
                    }
                    }
                    if (insertCount > 0)
                    {
                        objResult.status = true;
                        objResult.message = insertCount.ToString() + " Of " + dt.Rows.Count.ToString() + " Records Uploaded Successfully";
                    }
                    else
                    {
                        objResult.status = false;
                        objResult.message = "Error occured in uploading Excel Sheet Details";
                    }

                    dt.Dispose();

                }
            }
            catch (Exception ex)
            {
                objResult.status = false;
                objResult.message = ex.ToString();                                                                                                                                                          
            }
        }

        // Co-Applicant Details Export Excel
        public void DaGetCoApplicantEditExportExcel(string employee_gid, string application_gid, string rmdisbursementrequest_gid, MdlExcelImportApplication objMstCoApplicantExportExcel)
        {
            msSQL = " select a.application_no as 'Application Number',a.pan_status as 'Pan Status',a.pan_no as 'Pan Number', " +
                    " concat(a.first_name, ' ', a.middle_name, ' / ', a.last_name) as 'Individual Name',a.farmercontact_gid," +
                    " b.aadhar_no as '* Aadhar Number'," +
                    " b.pan_status as '* Pan Status(Yes / No)',b.pan_no as '* Pan Number(If PAN Status is Yes, PAN Value is mandatory)'," +
                    " b.first_name as '* First Name',b.middle_name as 'Middle Name',b.last_name as '* Last Name'," +
                    " b.individual_dob as '* Date of Birth(DD-MM-YYYY)',b.age as 'Age',b.gender_name as '* Gender'," +
                    " b.designation_name as '* Designation',b.pep_status as '* Politically Exposed person (PEP)(Yes/No)'," +
                    " b.pepverified_date as '* PEP Verified On(DD-MM-YYYY)',b.maritalstatus_name as '* Marital Status'," +
                    " b.educationalqualification_name as '* Educational Qualification',b.main_occupation as '* Main Occupation'," +
                    " b.annual_income as '* Annual Income',b.monthly_income as 'Monthly Income'," +
                    " b.currentresidence_years as '* Years in Current Residence'," +
                    " b.branch_distance as '* Distance from Branch/Regional Office (in Kms)'," +
                    " b.father_firstname as '* Father s First Name',b.father_middlename as 'Father s Middle Name'," +
                    " b.father_lastname as '* Father s Last Name',b.fathernominee_status as 'Father s Nominee Status(Yes/No)'," +
                    " b.father_dob as 'Father s Date of Birth(DD-MM-YYYY)',b.father_age as 'Father s Age'," +
                    " b.mother_firstname as 'Mother s First Name',b.mother_middlename as 'Mother s Middle Name'," +
                    " b.mother_lastname as 'Mother s Last Name',b.mothernominee_status as 'Mother s Nominee Status(Yes/No)'," +
                    " b.mother_dob as 'Mother s Date of Birth(DD-MM-YYYY)',b.mother_age as 'Mother s Age'," +
                    " b.spouse_firstname as 'Spouse First Name',b.spouse_middlename as 'Spouse Middle Name'," +
                    " b.spouse_lastname as 'Spouse Last Name', b.spousenominee_status as 'Spouse Nominee Status(Yes/No)'," +
                    " b.spouse_dob as 'Spouse Date of Birth(DD-MM-YYYY)',b.spouse_age as 'Spouse Age'," +
                    " b.mobile_no as '* Mobile Number',b.primary_status as '* Primary Status(Yes/No)'," +
                    " b.whatsapp_no as '* Whatsapp Number(Yes/No)'," +
                    " b.email_address as '* Email Address',b.emailprimary_status as '* Email Primary Status(Yes/No)'," +
                    " b.addresstype_name as '* Address Type',b.addressprimary_status as '* Address Primary Status'," +
                    " b.addressline1 as '* Address Line 1',b.addressline2 as 'Address Line 2',b.landmark as 'Land Mark'," +
                    " b.postal_code as '* Postal Code',b.city as 'City',b.taluka as 'Taluka'," +
                    " b. district as '* District',b.state as '* State',b.country as '* Country',b.latitude as 'Latitude'," +
                    " b.longitude as 'Longitude',b.ifsc_code as '* IFSC Code',b.bank_name as 'Bank Name'," +
                    " b.branch_name as 'Branch Name',b.branch_address as 'Branch Address',b.micr_code as 'MICR Code'," +
                    " b.bankaccount_number as '* Bank Account Number',b.confirmbankaccount_number as '* Confirm Account Number'," +
                    " b.accountholder_name as '* Account Holder Name'" +
                    " from ocs_trn_tfarmercontact a " +
                    " left join ocs_trn_tcontactcoapplicant b on b.farmercontact_gid = a.farmercontact_gid " +
                    " where a.rmdisbursementrequest_gid='" + rmdisbursementrequest_gid + "' and " +
                    " a.application_gid='" + application_gid + "' and  " +
                    " a.farmercontact_gid not in (select farmercontact_gid from ocs_trn_tcontactcoapplicant c where " +
                    " c.rmdisbursementrequest_gid='" + rmdisbursementrequest_gid + "') " +
                    " order by a.created_date ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            string lscompany_code = string.Empty;
            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            var workSheet = excel.Workbook.Worksheets.Add("Individual CoApplicant Details");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                objMstCoApplicantExportExcel.lsname = "Individual CoApplicant Details.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/Individual CoApplicant Details/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                objMstCoApplicantExportExcel.lscloudpath = lscompany_code + "/" + "Master/Individual CoApplicant Details/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstCoApplicantExportExcel.lsname;
                objMstCoApplicantExportExcel.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/Individual CoApplicant Details/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstCoApplicantExportExcel.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(objMstCoApplicantExportExcel.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 68])  //Address "A1:BP1"
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);
                }

                workSheet.Cells.Style.Locked = false;
                workSheet.Column(1).Style.Locked = true;
                workSheet.Column(2).Style.Locked = true;
                workSheet.Column(3).Style.Locked = true;
                workSheet.Column(4).Style.Locked = true;

                workSheet.Column(1).AutoFit();
                workSheet.Column(2).AutoFit();
                workSheet.Column(3).AutoFit();
                workSheet.Column(4).AutoFit();
                workSheet.Cells.Style.Hidden = false;
                workSheet.Column(5).Hidden = true;

                workSheet.Protection.AllowEditObject = true;
                workSheet.Protection.AllowEditScenarios = true;
                workSheet.Protection.AllowFormatCells = true;
                workSheet.Protection.AllowFormatColumns = true;
                workSheet.Protection.AllowFormatRows = true;
                workSheet.Protection.IsProtected = true;
                workSheet.Protection.SetPassword("Welcome@123");

                excel.SaveAs(ms);
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", objMstCoApplicantExportExcel.lscloudpath, ms);
                status = objcmnstorage.UploadStream("erpdocument", objMstCoApplicantExportExcel.lscloudpath, ms);
                ms.Close();

            }
            catch (Exception ex)
            {
                objMstCoApplicantExportExcel.status = false;
                objMstCoApplicantExportExcel.message = "Failure";
            }
            objMstCoApplicantExportExcel.lscloudpath = objcmnstorage.EncryptData(objMstCoApplicantExportExcel.lscloudpath);
            objMstCoApplicantExportExcel.lspath = objcmnstorage.EncryptData(objMstCoApplicantExportExcel.lspath);
            objMstCoApplicantExportExcel.status = true;
            objMstCoApplicantExportExcel.message = "Success";
        }
        public bool DaPostDisbursementMakerApprove(string employee_gid, MdlDisbursementRequestAdd values)
        {

            if (values.disbursement_to == "Applicant(B2B)")
            {
                if (values.updated_person == "Maker")
                {
                    msSQL = " select  disbursementamount_gid from ocs_trn_tdisbursementamount  " +
                       " where application_gid = '" + values.application_gid + "' and  " +
                       " rmdisbursementrequest_gid='" + values.rmdisbursementrequest_gid + "'  and (makerdisbursement_amount is not null or makerdisbursement_amount <> '')";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == false)
                    {
                        values.status = false;
                        values.message = "Kindly Add the Maker Disbursement Amount";
                        objODBCDatareader.Close();
                        return false;
                    }
                }

                
                    msSQL = " select bankaccountstatus_gid from ocs_trn_tbankaccountstatus  " +
                       " where application_gid = '" + values.application_gid + "' and  " +
                       " rmdisbursementrequest_gid='" + values.rmdisbursementrequest_gid + "' and disbursementaccount_status='Yes'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == false)
                    {
                        values.status = false;
                        values.message = "Kindly Update the Bank Account Status";
                        objODBCDatareader.Close();
                        return false;
                    }
                
            }

            if (values.disbursement_to == "Supplier")
            {
                if (values.updated_person == "Maker")
                {
                    msSQL = " select  rmdisbursementrequest_gid from ocs_trn_tdisbursementsupplier  " +
                       " where application_gid = '" + values.application_gid + "' and  " +
                       " rmdisbursementrequest_gid='" + values.rmdisbursementrequest_gid + "'  and (creditopsdisbursement_amount is not null or creditopsdisbursement_amount <> '' or creditopsdisbursement_amount <> '0')";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == false)
                    {
                        values.status = false;
                        values.message = "Kindly Add the Maker Disbursement Amount";
                        objODBCDatareader.Close();
                        return false;
                    }
                }
            }

            if (values.disbursement_to == "Farmer(B2B2C)")
            {
                if (values.updated_person == "Maker")
                {
                    msSQL = " select  rmdisbursementrequest_gid from ocs_trn_tfarmercontact  " +
                       " where application_gid = '" + values.application_gid + "' and  " +
                       " rmdisbursementrequest_gid='" + values.rmdisbursementrequest_gid + "'  and (creditopsdisbursement_amount is not null or creditopsdisbursement_amount <> '' or creditopsdisbursement_amount <> '0')";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == false)
                    {
                        values.status = false;
                        values.message = "Kindly Add the Maker Disbursement Amount";
                        objODBCDatareader.Close();
                        return false;
                    }
                }
            }

            msSQL = " select a.creditopsmaker_gid, creditopschecker_gid " +
                      " from ocs_trn_tdisbursementassignment a" +
                      " where a.rmdisbursementrequest_gid='" + values.rmdisbursementrequest_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.creditopsmaker_gid = objODBCDatareader["creditopsmaker_gid"].ToString();
                values.creditopschecker_gid = objODBCDatareader["creditopschecker_gid"].ToString();
            }
            objODBCDatareader.Close();


            if (values.creditopsmaker_gid == values.creditopschecker_gid)
            {
                msSQL = " update ocs_trn_trmdisbursementrequest set ";

                if (values.updated_person == "RM")
                {

                }
                else
                {
                    msSQL += " maker_remarks = '" + values.maker_remarks.Replace("'", "") + "'," +
                             " processing_fees='" + values.processing_fees + "'," +
                             " processing_gst = '" + values.processing_gst + "'," +
                             " dispgstprocessing_fees = '" + values.dispgstprocessing_fees + "'," +
                             " od_amount = '" + values.od_amount + "'," +
                             " finance_charges = '" + values.finance_charges + "'," +
                             " additionalcharges_gst = '" + values.additionalcharges_gst + "'," +
                             " dispgstadditionfees_charges = '" + values.dispgstadditionfees_charges + "'," +
                             " escrow_payment='" + values.escrow_payment + "'," +
                             " nach_status = '" + values.nach_status + "',";

                    msSQL += " updated_person = '" + values.updated_person + "'," +
                            " updated_by = '" + employee_gid + "'," +
                            " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                            " where rmdisbursementrequest_gid ='" + values.rmdisbursementrequest_gid + "' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult != 0)
                    {  
                        msSQL = " select application_gid,application2sanction_gid,sanction_refno,application2loan_gid,product_type," +
                               " processing_fees,processing_gst,finance_charges,od_amount,escrow_payment,nach_status,remarks," +
                               " updated_person,amounttobe_disbursed,disbursement_to,lsareference_gid,lsareference_number," +
                               " dispgstprocessing_fees," +
                               " additionalcharges_gst,dispgstadditionfees_charges,maker_remarks, " +
                               " date_format(loandisbursement_date, '%Y-%m-%d %h:%i:%s') as loandisbursement_date " +
                               " from ocs_trn_trmdisbursementrequest a" +
                               " where rmdisbursementrequest_gid='" + values.rmdisbursementrequest_gid + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);

                        if (objODBCDatareader.HasRows == true)
                        {
                            lsapplication_gid = objODBCDatareader["application_gid"].ToString();
                            lsapplication2sanction_gid = objODBCDatareader["application2sanction_gid"].ToString();
                            lssanction_refno = objODBCDatareader["sanction_refno"].ToString();
                            lsapplication2loan_gid = objODBCDatareader["application2loan_gid"].ToString();
                            lsproduct_type = objODBCDatareader["product_type"].ToString();
                            lsprocessing_fees = objODBCDatareader["processing_fees"].ToString();
                            lsgst = objODBCDatareader["processing_gst"].ToString();
                            lsfinance_charges = objODBCDatareader["finance_charges"].ToString();
                            lsod_amount = objODBCDatareader["od_amount"].ToString();
                            lsescrow_payment = objODBCDatareader["escrow_payment"].ToString();
                            lsnach_status = objODBCDatareader["nach_status"].ToString();
                            lsremarks = objODBCDatareader["remarks"].ToString();
                            lsupdated_person = objODBCDatareader["updated_person"].ToString();
                            lsamounttobe_disbursed = objODBCDatareader["amounttobe_disbursed"].ToString();
                            lsareference_gid = objODBCDatareader["lsareference_gid"].ToString();
                            lsareference_number = objODBCDatareader["lsareference_number"].ToString();
                            lsdispgstprocessing_fees = objODBCDatareader["dispgstprocessing_fees"].ToString();
                            lsadditionalcharges_gst = objODBCDatareader["additionalcharges_gst"].ToString();
                            lsdispgstadditionfees_charges = objODBCDatareader["dispgstadditionfees_charges"].ToString();
                            lsmaker_remarks = objODBCDatareader["maker_remarks"].ToString();
                            lsdisbursement_to = objODBCDatareader["disbursement_to"].ToString();
                        }
                        objODBCDatareader.Close();

                        msSQL = " update ocs_trn_trmdisbursementrequest set " +
                               " processing_fees='" + values.processing_fees + "'," +
                               " processing_gst = '" + values.processing_gst + "'," +
                               " dispgstprocessing_fees = '" + values.dispgstprocessing_fees + "'," +
                               " od_amount = '" + values.od_amount + "'," +
                               " finance_charges = '" + values.finance_charges + "'," +
                               " additionalcharges_gst = '" + values.additionalcharges_gst + "'," +
                               " dispgstadditionfees_charges = '" + values.dispgstadditionfees_charges + "'," +
                               " escrow_payment='" + values.escrow_payment + "'," +
                               " nach_status = '" + values.nach_status + "'," +
                               " checker_remarks = '" + values.maker_remarks.Replace("'", "") + "'," +
                               " updated_person = '" + values.updated_person + "'," +
                               " disbursementassign_status = 'Approved' ," +
                               " updated_by = '" + employee_gid + "'," +
                               " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                               " where rmdisbursementrequest_gid ='" + values.rmdisbursementrequest_gid + "' ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " update ocs_trn_tdisbursementassignment set maker_approvalflag='Y'," +
                               " maker_approveddate = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                               " where rmdisbursementrequest_gid ='" + values.rmdisbursementrequest_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " update ocs_trn_tdisbursementassignment set checker_approvalflag='Y'," +
                                " checker_approveddate = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                                " where rmdisbursementrequest_gid ='" + values.rmdisbursementrequest_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        if (mnResult != 0)
                        {
                            msSQL = " update ocs_trn_trmdisbursementdocument set rmdisbursementrequest_gid='" + values.rmdisbursementrequest_gid + "'," +
                               " application_gid='" + values.application_gid + "'" +
                               " where application_gid ='" + employee_gid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            msSQL = " update ocs_trn_tfarmercontact set rmdisbursementrequest_gid='" + values.rmdisbursementrequest_gid + "'" +
                                    " where rmdisbursementrequest_gid ='" + employee_gid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            msSQL = " update ocs_trn_tdisbursementsupplier set rmdisbursementrequest_gid='" + values.rmdisbursementrequest_gid + "'" +
                                    " where rmdisbursementrequest_gid ='" + employee_gid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            msSQL = " update ocs_trn_tcontactcoapplicant set rmdisbursementrequest_gid='" + values.rmdisbursementrequest_gid + "'" +
                                    " where rmdisbursementrequest_gid ='" + employee_gid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                            msSQL = " update ocs_trn_tdisbapplicantbankdtl set rmdisbursementrequest_gid='" + values.rmdisbursementrequest_gid + "'" +
                                  " where rmdisbursementrequest_gid ='" + employee_gid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            msSQL = " update ocs_trn_tdisbursementamount set rmdisbursementrequest_gid='" + values.rmdisbursementrequest_gid + "'" +
                                    " where rmdisbursementrequest_gid ='" + employee_gid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            msSQL = " update ocs_trn_tbankaccountstatus set rmdisbursementrequest_gid='" + values.rmdisbursementrequest_gid + "'" +
                                    " where rmdisbursementrequest_gid ='" + employee_gid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            msGetGid = objcmnfunctions.GetMasterGID("DRLO");
                            msSQL = " insert into ocs_trn_trmdisbursementrequestlog(" +
                                    " rmdisbursementrequestlog_gid ," +
                                    " application_gid," +
                                    " application2sanction_gid," +
                                    " sanction_refno," +
                                    " application2loan_gid," +
                                    " product_type," +
                                    " processing_fees," +
                                    " processing_gst," +
                                    " finance_charges," +
                                    " od_amount," +
                                    " escrow_payment," +
                                    " nach_status," +
                                    " remarks," +
                                    " updated_person," +
                                    " amounttobe_disbursed," +
                                    " loandisbursement_date," +
                                    " disbursement_to," +
                                    " lsareference_gid," +
                                    " lsareference_number," +
                                    " dispgstprocessing_fees," +
                                    " additionalcharges_gst," +
                                    " maker_remarks," +
                                    " rmdisbursementrequest_gid," +
                                    " updated_by," +
                                    " updated_date)" +
                                    " values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + lsapplication_gid + "'," +
                                    "'" + lsapplication2sanction_gid + "'," +
                                    "'" + lssanction_refno + "'," +
                                    "'" + lsapplication2loan_gid + "'," +
                                    "'" + lsproduct_type + "'," +
                                    "'" + lsprocessing_fees + "'," +
                                    "'" + lsgst + "'," +
                                    "'" + lsfinance_charges + "'," +
                                    "'" + lsod_amount + "'," +
                                    "'" + lsescrow_payment + "'," +
                                    "'" + lsnach_status + "'," +
                                    "'" + lsremarks.Replace("'", "") + "'," +
                                    "'" + lsupdated_person + "'," +
                                    "'" + lsamounttobe_disbursed + "',";
                            if ((lsloandisbursement_date == null) || (lsloandisbursement_date == ""))
                            {

                                msSQL += "null,";
                            }
                            else
                            {
                                msSQL += "'" + lsloandisbursement_date + "',";
                            }
                            msSQL += "'" + lsdisbursement_to + "'," +
                                    "'" + lsareference_gid + "'," +
                                    "'" + lsareference_number + "'," +
                                    "'" + lsdispgstprocessing_fees + "'," +
                                    "'" + lsadditionalcharges_gst + "'," +
                                    "'" + lsmaker_remarks + "'," +
                                    "'" + values.rmdisbursementrequest_gid + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }

                        if (mnResult != 0)
                        {
                            if (values.disbursement_to == "Applicant(B2B)")
                            {
                                msSQL = " select disbursementamount_gid,makerdisbursement_amount from ocs_trn_tdisbursementamount a" +
                                        " where rmdisbursementrequest_gid='" + values.rmdisbursementrequest_gid + "'";
                                dt_datatable9 = objdbconn.GetDataTable(msSQL);
                                if (dt_datatable9.Rows.Count != 0)
                                {
                                    foreach (DataRow dt9 in dt_datatable9.Rows)
                                    {
                                        msSQL = " update ocs_trn_tdisbursementamount  set" +
                                                " checkerdisbursement_amount='" + dt9["makerdisbursement_amount"].ToString() + "'" +
                                                " where disbursementamount_gid='" + dt9["disbursementamount_gid"].ToString() + "'";
                                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                    }
                                }
                            }
                            else if (values.disbursement_to == "Farmer(B2B2C)")
                            {
                                msSQL = " select farmercontact_gid,creditopsdisbursement_amount from ocs_trn_tfarmercontact a" +
                                        " where rmdisbursementrequest_gid='" + values.rmdisbursementrequest_gid + "'";
                                dt_datatable6 = objdbconn.GetDataTable(msSQL);
                                if (dt_datatable6.Rows.Count != 0)
                                {
                                    foreach (DataRow dt6 in dt_datatable6.Rows)
                                    {
                                        msSQL = " update ocs_trn_tfarmercontact set" +
                                                " creditopscheckerdisbursement_amount='" + dt6["creditopsdisbursement_amount"].ToString() + "'" +
                                                " where farmercontact_gid='" + dt6["farmercontact_gid"].ToString() + "'";
                                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                    }
                                }
                            }
                            else if (values.disbursement_to == "Supplier")
                            {
                                msSQL = " select disbursementsupplier_gid,creditopsdisbursement_amount from ocs_trn_tdisbursementsupplier a" +
                                        " where rmdisbursementrequest_gid='" + values.rmdisbursementrequest_gid + "'";
                                dt_datatable7 = objdbconn.GetDataTable(msSQL);
                                if (dt_datatable7.Rows.Count != 0)
                                {
                                    foreach (DataRow dt7 in dt_datatable7.Rows)
                                    {
                                        msSQL = " update ocs_trn_tdisbursementsupplier set" +
                                                " creditopscheckerdisbursement_amount='" + dt7["creditopsdisbursement_amount"].ToString() + "'" +
                                                " where disbursementsupplier_gid='" + dt7["disbursementsupplier_gid"].ToString() + "'";
                                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                    }
                                }
                            }
                            else
                            {

                            }
                        }
                    }
                   
                }
            }
            else
            {
                msSQL = " update ocs_trn_trmdisbursementrequest set " +
                        " maker_remarks = '" + values.maker_remarks.Replace("'", "") + "'," +
                        " processing_fees='" + values.processing_fees + "'," +
                        " processing_gst = '" + values.processing_gst + "'," +
                        " dispgstprocessing_fees = '" + values.dispgstprocessing_fees + "'," +
                        " od_amount = '" + values.od_amount + "'," +
                        " finance_charges = '" + values.finance_charges + "'," +
                        " additionalcharges_gst = '" + values.additionalcharges_gst + "'," +
                        " dispgstadditionfees_charges = '" + values.dispgstadditionfees_charges + "'," +
                        " escrow_payment='" + values.escrow_payment + "'," +
                        " nach_status = '" + values.nach_status + "'," +
                        " updated_person = '" + values.updated_person + "'," +
                        " updated_by = '" + employee_gid + "'," +
                        " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                        " where rmdisbursementrequest_gid ='" + values.rmdisbursementrequest_gid + "' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult != 0)
                    {
                        msSQL = " update ocs_trn_trmdisbursementdocument set rmdisbursementrequest_gid='" + values.rmdisbursementrequest_gid + "'," +
                                " application_gid='" + values.application_gid + "'" +
                                " where application_gid ='" + employee_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " update ocs_trn_tfarmercontact set rmdisbursementrequest_gid='" + values.rmdisbursementrequest_gid + "'" +
                                " where rmdisbursementrequest_gid ='" + employee_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " update ocs_trn_tdisbursementsupplier set rmdisbursementrequest_gid='" + values.rmdisbursementrequest_gid + "'" +
                                " where rmdisbursementrequest_gid ='" + employee_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " update ocs_trn_tcontactcoapplicant set rmdisbursementrequest_gid='" + values.rmdisbursementrequest_gid + "'" +
                                " where rmdisbursementrequest_gid ='" + employee_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                        msSQL = " update ocs_trn_tdisbapplicantbankdtl set rmdisbursementrequest_gid='" + values.rmdisbursementrequest_gid + "'" +
                                " where rmdisbursementrequest_gid ='" + employee_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " update ocs_trn_tdisbursementamount set rmdisbursementrequest_gid='" + values.rmdisbursementrequest_gid + "'" +
                                " where rmdisbursementrequest_gid ='" + employee_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " update ocs_trn_tbankaccountstatus set rmdisbursementrequest_gid='" + values.rmdisbursementrequest_gid + "'" +
                                " where rmdisbursementrequest_gid ='" + employee_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " update ocs_trn_tdisbursementassignment set maker_approvalflag='Y'," +
                                " maker_approveddate = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                                " where rmdisbursementrequest_gid ='" + values.rmdisbursementrequest_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        
                    }                
            }
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Disbursement Request Maker Approved Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while updating Disbursement";
                return false;
            }
        }

    }
}