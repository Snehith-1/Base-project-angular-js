using System.Web;
using ems.utilities.Functions;
using ems.storage.Functions;
using ems.lgl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Odbc;
using System.IO;
using System.Configuration;
using System.Drawing;
namespace ems.lgl.DataAccess
{
    public class DaRegisterlawyer
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        OdbcDataReader objODBCDatareader;
        HttpPostedFile httpPostedFile;
        DataTable dt_datatable;
        string msSQL, msGetGid, msGetGIDREF;
        string lspath, lsdocument_type, lslawyer_name, lslawyer_email, lsmobile_no;
        int mnresult, mnResult;
        string lsaddress1, lsaddress2, lsaddress1edit, lsaddress2edit;

        //-----Submit - Register Lawyer-----//
        public bool DaPostRegisterLawyer(string employee_gid, string user_gid, mdlRegisterlawyer values)
        {
            msSQL = "select enrolment_no from lgl_mst_tregisterlawyer where enrolment_no='" + values.enrolment_no + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.message = "Enrolment Number Already Exists";
                return false;
            }
            objODBCDatareader.Close();

            msSQL = "select lawyerregister_gid from lgl_mst_tregisterlawyer where lawyer_name='" + values.lawyer_name + "' and mobile_no='" + values.mobile_no + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.message = "Lawyer Already Exists";
                return false;
            }
            objODBCDatareader.Close();
            if (values.address_line1 != null)
            {
                lsaddress1 = (values.address_line1).Replace("'", "");
            }
            else
            {
                lsaddress1 = "";
            }

            if (values.address_line2 != null)
            {
                lsaddress2 = (values.address_line2).Replace("'", "");
            }
            else
            {
                lsaddress2 = "";
            }


            msGetGid = objcmnfunctions.GetMasterGID("RELR");
            msGetGIDREF = objcmnfunctions.GetMasterGID("RL_");
            msSQL = "insert into lgl_mst_tregisterlawyer (" +
                      " lawyerregister_gid  ," +
                      " lawyerref_no ," +
                      " lawyer_name ," +
                      " dob ," +
                      " gender ," +
                      " mobile_no ," +
                      " telephone_no ," +
                      " email_address ," +
                      " educational_qualification ," +
                      " date_enrolment ," +
                      " pan_no," +
                      " experience ," +
                      " place_practice ," +
                      " address_line1 ," +
                      " address_line2 ," +
                      " state ," +
                      " country," +
                      " postal_code," +
                      " enrolment_no," +
                      " login_status," +
                      " aadhar_no," +
                      " bank_name," +
                      " account_no," +
                      " ifsc_code," +
                      " created_by," +
                      " created_date)" +
                      " values(" +
                      "'" + msGetGid + "'," +
                      "'" + msGetGIDREF + "'," +
                      "'" + values.lawyer_name + "',";
            if (values.dob == null)
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.dob).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }

            msSQL += "'" + values.gender + "'," +
                "'" + values.mobile_no + "'," +
                "'" + values.telephone_no + "'," +
                "'" + values.email_address + "'," +
                "'" + values.educational_qualification.Replace("'", "") + "'," +
                "'" + Convert.ToDateTime(values.date_enrolment).ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                "'" + values.pan_no + "'," +
                "'" + values.experience.Replace("'", "") + "'," +
                "'" + values.place_practice + "'," +
                "'" + lsaddress1 + "'," +
                "'" + lsaddress2 + "'," +
                "'" + values.state + "'," +
                "'" + values.country.Replace("'", "") + "'," +
                "'" + values.postal_code + "'," +
                "'" + values.enrolment_no + "'," +
                 "'Individual'," +
                 "'" + values.aadhar_no + "'," +
                "'" + values.bank_name + "'," +
                "'" + values.account_no + "'," +
                "'" + values.ifsc_code + "'," +
                "'" + employee_gid + "'," +
                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnresult != 0)
            {
                msSQL = "update lgl_tmp_tuploadcertificate set lawyerregister_gid='" + msGetGid + "' where lawyerregister_gid='" + user_gid + "' and created_by='" + user_gid + "'";
                mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "select * from lgl_tmp_tphotoupload";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objODBCDatareader.Read();
                    msSQL = "update lgl_mst_tregisterlawyer set lawyerphoto_name='" + objODBCDatareader["photo_name"].ToString() + "'," +
                        " lawyerphoto_path='" + objODBCDatareader["lawyerphoto_path"].ToString() + "' where lawyerregister_gid='" + msGetGid + "'";
                    mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnresult != 0)
                    {
                        msSQL = "delete from lgl_tmp_tphotoupload";
                        mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }

                }
                objODBCDatareader.Close();
                values.status = true;
                values.message = "Lawyer Registered Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while adding Lawyer";
                return false;
            }

        }
        //------------Summary------------------//
        public bool DaGetLawyerDetail(mdllawyer objLawyer)
        {
            msSQL = "select a.lawyerregister_gid,a.lawyer_name,a.lawyeruser_status,a.lawyerref_no,a.mobile_no,date_format(a.date_enrolment, '%d-%m-%Y') as 'date_enrolment'," +
                " a.experience,a.enrolment_no,a.login_status,date_format(a.created_date, '%d-%m-%Y %H:%i %p') as created_date," +
                " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by from lgl_mst_tregisterlawyer a" +
                " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                " left join adm_mst_tuser c on c.user_gid=b.user_gid order by a.created_date desc ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getlawyer = new List<lawyer_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getlawyer.Add(new lawyer_list
                    {
                        lawyer_refno = (dr_datarow["lawyerref_no"].ToString()),
                        lawyer_name = (dr_datarow["lawyer_name"].ToString()),
                        mobile_no = (dr_datarow["mobile_no"].ToString()),
                        date_enroll = (dr_datarow["date_enrolment"].ToString()),
                        lawyerregister_gid = (dr_datarow["lawyerregister_gid"].ToString()),
                        experience = dr_datarow["experience"].ToString(),
                        lawyeruser_status = dr_datarow["lawyeruser_status"].ToString(),
                        enrolment_no = dr_datarow["enrolment_no"].ToString(),
                        login_status = dr_datarow["login_status"].ToString(),
                        created_date = dr_datarow["created_date"].ToString(),
                        created_by = dr_datarow["created_by"].ToString(),
                    });
                }
                objLawyer.lawyer_list = getlawyer;
            }
            dt_datatable.Dispose();
            objLawyer.status = true;
            return true;
        }
        //-------------Edit and View Information-------------//
        public bool DaGetLawyerDetails(string user_gid, string lawyerregister_gid, lawyeredit values)
        {
            try
            {
                msSQL = " select lawyerregister_gid,lawyerref_no,lawyer_name,dob,gender,mobile_no,enrolment_no,telephone_no,email_address,educational_qualification," +
                          " date_enrolment,pan_no,experience,place_practice,address_line1,address_line2,state,country,postal_code,aadhar_no,account_no,bank_name,ifsc_code " +
                          " from lgl_mst_tregisterlawyer where lawyerregister_gid='" + lawyerregister_gid + "' ";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {

                    values.txtlawyerref_noedit = objODBCDatareader["lawyerref_no"].ToString();
                    values.txtlawyernameedit = objODBCDatareader["lawyer_name"].ToString();
                    if (objODBCDatareader["dob"].ToString() == "")
                    {
                    }
                    else
                    {
                        values.dobedit = Convert.ToDateTime(objODBCDatareader["dob"]).ToString("MM-dd-yyyy");
                    }
                    values.genderedit = objODBCDatareader["gender"].ToString();
                    if (objODBCDatareader["mobile_no"].ToString() != "")
                    {
                        values.txtmobilenoedit = Convert.ToDouble(objODBCDatareader["mobile_no"].ToString());
                    }
                    if (objODBCDatareader["telephone_no"].ToString() != "")
                    {
                        values.txttelephonenoedit = Convert.ToDouble(objODBCDatareader["telephone_no"].ToString());
                    }
                    values.txtemailaddressedit = objODBCDatareader["email_address"].ToString();
                    values.txtqualificationedit = objODBCDatareader["educational_qualification"].ToString();
                    values.txtdateenrol_counciledit = objODBCDatareader["date_enrolment"].ToString();
                    if (objODBCDatareader["date_enrolment"].ToString() == "")
                    {
                    }
                    else
                    {
                        values.txtdateenrol_counciledit = Convert.ToDateTime(objODBCDatareader["date_enrolment"]).ToString("MM-dd-yyyy");
                    }
                    values.txtpannoedit = objODBCDatareader["pan_no"].ToString();
                    values.txtexperienceedit = objODBCDatareader["experience"].ToString();
                    values.txtplace_practiceedit = objODBCDatareader["place_practice"].ToString();
                    values.txtaddress1edit = objODBCDatareader["address_line1"].ToString();
                    values.txtaddress2edit = objODBCDatareader["address_line2"].ToString();
                    values.txtstateedit = objODBCDatareader["state"].ToString();
                    values.txtcountryedit = objODBCDatareader["country"].ToString();
                    if (objODBCDatareader["postal_code"].ToString() != "")
                    {
                        values.txtpostalcodeedit = Convert.ToDouble(objODBCDatareader["postal_code"].ToString());
                    }

                    values.lawyerregister_gid = lawyerregister_gid;
                    values.txtenrolment_no = objODBCDatareader["enrolment_no"].ToString();
                    values.aadhar_no = objODBCDatareader["aadhar_no"].ToString();
                    values.account_no = objODBCDatareader["account_no"].ToString();
                    values.bank_name = objODBCDatareader["bank_name"].ToString();
                    values.ifsc_code = objODBCDatareader["ifsc_code"].ToString();


                }
                objODBCDatareader.Close();

                msSQL = " select document_gid,document_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,document_path,document_type, " +
                         " concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as uploaded_by" +
                         " from lgl_tmp_tuploadcertificate a, adm_mst_tuser b where a.created_by=b.user_gid and (lawyerregister_gid='" + lawyerregister_gid + "' or" +
                         " lawyerregister_gid='" + user_gid + "') ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getdocument = new List<UploadDocumentList>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getdocument.Add(new UploadDocumentList
                        {
                            document_gid = (dr_datarow["document_gid"].ToString()),
                            document_name = (dr_datarow["document_name"].ToString()),
                            document_type = dr_datarow["document_type"].ToString(),
                            uploaded_by = dr_datarow["uploaded_by"].ToString(),
                            updated_date = dr_datarow["uploaded_date"].ToString()
                        });
                    }
                    values.UploadDocumentList = getdocument;
                }
                dt_datatable.Dispose();

            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = ex.Message;
                return false;
            }
            values.status = true;
            return true;
        }
        //--------Update - Register Lawyer-----------//
        public bool DaUpdateLawyer(string user_gid, lawyeredit values)
        {

            if (values.txtaddress1edit != null)
            {
                lsaddress1edit = (values.txtaddress1edit).Replace("'", "");
            }
            else
            {
                lsaddress1edit = "";
            }

            if (values.txtaddress2edit != null)
            {
                lsaddress2edit = (values.txtaddress2edit).Replace("'", "");
            }
            else
            {
                lsaddress2edit = "";
            }

            msSQL = " update lgl_mst_tregisterlawyer set " +
                    " lawyerref_no='" + values.txtlawyerref_noedit + "'," +
                    " lawyer_name='" + values.txtlawyernameedit + "'," +
                    " gender='" + values.genderedit + "'," +
                    " mobile_no='" + Convert.ToDouble(values.txtmobilenoedit).ToString() + "'," +
                    " telephone_no='" + Convert.ToDouble(values.txttelephonenoedit).ToString() + "'," +
                    " email_address='" + values.txtemailaddressedit + "'," +
                    " educational_qualification='" + values.txtqualificationedit.Replace("'", "") + "'," +
                    " pan_no='" + values.txtpannoedit + "'," +
                    " experience='" + values.txtexperienceedit + "'," +
                    " place_practice='" + values.txtplace_practiceedit.Replace("'", "") + "'," +
                    " address_line1='" + lsaddress1edit + "'," +
                    " address_line2='" + lsaddress2edit + "'," +
                    " state='" + values.txtstateedit + "'," +
                    " country='" + values.txtcountryedit.Replace("'", "") + "'," +
                    " postal_code='" + Convert.ToDouble(values.txtpostalcodeedit).ToString() + "'," +
                    " enrolment_no='" + values.txtenrolment_no + "'," +
                    " aadhar_no='" + values.aadhar_no + "'," +
                    " account_no='" + values.account_no + "'," +
                    " bank_name='" + values.bank_name + "'," +
                    " ifsc_code='" + values.ifsc_code + "'," +
                    " updated_by='" + user_gid + "'," +
                    " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
               " where lawyerregister_gid='" + values.lawyerregister_gid + "' ";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (Convert.ToDateTime(values.dob_edit).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
            {

            }
            else
            {
                msSQL = "update lgl_mst_tregisterlawyer set dob='" + Convert.ToDateTime(values.dob_edit).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "'" +
                    "where lawyerregister_gid='" + values.lawyerregister_gid + "' ";
                mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            if (Convert.ToDateTime(values.txt_dateenrol_counciledit).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
            {

            }
            else
            {
                msSQL = "update lgl_mst_tregisterlawyer set date_enrolment='" + Convert.ToDateTime(values.txt_dateenrol_counciledit).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "'" +
                    "where lawyerregister_gid='" + values.lawyerregister_gid + "' ";
                mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            msSQL = "update lgl_tmp_tuploadcertificate set lawyerregister_gid='" + values.lawyerregister_gid + "' where lawyerregister_gid='" + user_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "select * from lgl_tmp_tphotoupload where created_by='" + user_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                msSQL = "update lgl_mst_tregisterlawyer set lawyerphoto_name='" + objODBCDatareader["photo_name"].ToString() + "'," +
                         " lawyerphoto_path='" + objODBCDatareader["lawyerphoto_path"].ToString() + "' where lawyerregister_gid='" + values.lawyerregister_gid + "'";

                mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnresult != 0)
                {
                    msSQL = "delete from lgl_tmp_tphotoupload  where created_by='" + user_gid + "'";
                    mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

            }
            objODBCDatareader.Close();


            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Lawyer Details Updated Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while updating Lawyer";
                return false;
            }

        }

        public bool DaLawyerDelete(string lawyerregister_gid, lawyeredit values)
        {


            msSQL = "select lawfirm2lawyer_gid from lgl_trn_tlawfirm2lawyer where lawyerregister_gid='" + lawyerregister_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count == 0)
            {
                msSQL = " delete from lgl_mst_tregisterlawyer where lawyerregister_gid='" + lawyerregister_gid + "'";
                mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

                dt_datatable.Dispose();

                if (mnresult != 0)
                {
                    msSQL = "delete from lgl_tmp_tuploadcertificate where lawyerregister_gid='" + lawyerregister_gid + "'";
                    mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                values.message = "Deleted Successfully";
                values.status = true;
                return true;




            }

            else
            {
                dt_datatable.Dispose();
                values.status = false;
                values.message = "You can't delete this Lawyer because Lawyer assigned to Lawfirm";
                return false;
            }
        }
        // ------View Page------------------//
        public bool DaGetLawyerView(string lawyerregister_gid, UploadDocumentname objfilename, string user_gid)
        {


            msSQL = " select lawyerregister_gid,lawyerref_no,lawyer_name,lawyeruser_password,date_format(dob,'%d-%m-%Y')as dob,gender,mobile_no,lawyeruser_status," +
            " telephone_no,email_address,educational_qualification,lawyerphoto_name,lawyerphoto_path,enrolment_no," +
            " date_format(date_enrolment,'%d-%m-%Y')as date_enrolment,pan_no,experience,place_practice,address_line1,address_line2,state,country,postal_code, " +
            " block_remarks,aadhar_no,bank_name,account_no,ifsc_code from lgl_mst_tregisterlawyer " +
            " where lawyerregister_gid = '" + lawyerregister_gid + "'";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                objfilename.lawyerregister_gid = objODBCDatareader["lawyerregister_gid"].ToString();
                objfilename.lawyerref_no = objODBCDatareader["lawyerref_no"].ToString();
                objfilename.lawyer_name = objODBCDatareader["lawyer_name"].ToString();
                objfilename.dob = objODBCDatareader["dob"].ToString();
                objfilename.gender = objODBCDatareader["gender"].ToString();
                if (objODBCDatareader["mobile_no"].ToString() != "")
                {
                    objfilename.mobile_no = Convert.ToDouble(objODBCDatareader["mobile_no"].ToString());
                }
                if (objODBCDatareader["telephone_no"].ToString() != "")
                {
                    objfilename.telephone_no = Convert.ToDouble(objODBCDatareader["telephone_no"].ToString());
                }
                objfilename.email_address = objODBCDatareader["email_address"].ToString();
                objfilename.educational_qualification = objODBCDatareader["educational_qualification"].ToString();
                objfilename.date_enrolment = objODBCDatareader["date_enrolment"].ToString();
                objfilename.pan_no = objODBCDatareader["pan_no"].ToString();
                objfilename.experience = objODBCDatareader["experience"].ToString();
                objfilename.place_practice = objODBCDatareader["place_practice"].ToString();
                objfilename.address_line1 = objODBCDatareader["address_line1"].ToString();
                objfilename.address_line2 = objODBCDatareader["address_line2"].ToString();
                objfilename.state = objODBCDatareader["state"].ToString();
                objfilename.country = objODBCDatareader["country"].ToString();
                if (objODBCDatareader["postal_code"].ToString() != "")
                {
                    objfilename.postal_code = Convert.ToDouble(objODBCDatareader["postal_code"].ToString());
                }
                objfilename.lawyeruser_status = objODBCDatareader["lawyeruser_status"].ToString();
                objfilename.lawyerphoto_name = objODBCDatareader["lawyerphoto_name"].ToString();
                if (objODBCDatareader["lawyerphoto_path"].ToString() == "")
                {
                    objfilename.lawyerphoto_path = "N";
                }
                else
                {
                    objfilename.lawyerphoto_path = objcmnstorage.EncryptData(objODBCDatareader["lawyerphoto_path"].ToString());
                }
                objfilename.lawyeruser_password = objODBCDatareader["lawyeruser_password"].ToString();
                objfilename.enrolment_no = objODBCDatareader["enrolment_no"].ToString();
                objfilename.block_remarks = objODBCDatareader["block_remarks"].ToString();
                objfilename.aadhar_no = objODBCDatareader["aadhar_no"].ToString();
                objfilename.account_no = objODBCDatareader["account_no"].ToString();
                objfilename.bank_name = objODBCDatareader["bank_name"].ToString();
                objfilename.ifsc_code = objODBCDatareader["ifsc_code"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " select document_gid,document_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,document_path,document_type, " +
                " concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as uploaded_by" +
                " from lgl_tmp_tuploadcertificate a, adm_mst_tuser b where a.created_by=b.user_gid and lawyerregister_gid='" + lawyerregister_gid + "'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdocument = new List<UploadDocumentList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getdocument.Add(new UploadDocumentList
                    {
                        document_gid = (dr_datarow["document_gid"].ToString()),
                        document_name = (dr_datarow["document_name"].ToString()),
                        document_path = objcmnstorage.EncryptData(dr_datarow["document_path"].ToString()),
                        document_type = dr_datarow["document_type"].ToString(),
                        uploaded_by = dr_datarow["uploaded_by"].ToString(),
                        updated_date = dr_datarow["uploaded_date"].ToString()


                    });
                }
                objfilename.UploadDocumentList = getdocument;
            }
            dt_datatable.Dispose();
            objfilename.status = true;
            return true;
        }

        public bool DaUploadEnrollCertificate(HttpRequest httpRequest, UploadDocumentname objfilename, string employee_gid, string user_gid)
        {
            UploadDocumentList objdocumentmodel = new UploadDocumentList();
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

            string document_type = httpRequest.Form["document_type"].ToString();
            string project_flag = httpRequest.Form["project_flag"].ToString();


            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";

            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            //path = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "LGL/Enrolcertificate/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
            //{
            //    if ((!System.IO.Directory.Exists(path)))
            //        System.IO.Directory.CreateDirectory(path);
            //}
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "LGL/Enrolcertificate/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
            {
                if ((!System.IO.Directory.Exists(path)))
                    System.IO.Directory.CreateDirectory(path);
            }
            string msdocument_gid = objcmnfunctions.GetMasterGID("UECD");
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
                        //string lsfile_gid = msdocument_gid + FileExtension;
                        string lsfile_gid = msdocument_gid;
                        FileExtension = Path.GetExtension(FileExtension).ToLower();
                        lsfile_gid = lsfile_gid + FileExtension;
                        ls_readStream = httpPostedFile.InputStream;
                        ls_readStream.CopyTo(ms);
                        //lspath = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "LGL/Enrolcertificate/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        //objcmnfunctions.uploadFile(lspath, lsfile_gid);
                        //lspath = "../../erp_documents" + "/" + lscompany_code + "/" + "LGL/Enrolcertificate/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension;

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.status = false;
                            objfilename.message = "File format is not supported";
                            return false;
                        }

                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "LGL/Enrolcertificate/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "LGL/Enrolcertificate/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";


                        if (document_type == "undefined")
                        {
                            lsdocument_type = "";
                        }
                        else
                        {
                            lsdocument_type = document_type;
                        }
                        msGetGid = objcmnfunctions.GetMasterGID("UECD");
                        msSQL = " insert into lgl_tmp_tuploadcertificate( " +
                                    " document_gid," +
                                    " lawyerregister_gid ," +
                                    " document_name," +
                                    " document_path," +
                                    " document_type ," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + msdocument_gid + "'," +
                                    "'" + user_gid + "'," +
                                    "'" + httpPostedFile.FileName + "'," +
                                    "'" + lspath + msdocument_gid + FileExtension + "'," +
                                    "'" + lsdocument_type.Replace("'", "") + "'," +
                                    "'" + user_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                        mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                msSQL = " select document_gid,document_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,document_path,document_type, " +
                     " concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as uploaded_by" +
                     " from lgl_tmp_tuploadcertificate a, adm_mst_tuser b where a.created_by=b.user_gid and lawyerregister_gid='" + user_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_filename = new List<UploadDocumentList>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_filename.Add(new UploadDocumentList
                        {
                            document_name = (dr_datarow["document_name"].ToString()),
                            document_gid = (dr_datarow["document_gid"].ToString()),
                            document_type = dr_datarow["document_type"].ToString(),
                            uploaded_by = dr_datarow["uploaded_by"].ToString(),
                            updated_date = dr_datarow["uploaded_date"].ToString()
                        });
                    }
                    objfilename.UploadDocumentList = get_filename;
                }
                dt_datatable.Dispose();
            }
            catch
            {

            }
            if (mnresult == 1)
            {
                objfilename.status = true;
                objfilename.message = "Document Uploaded Successfully";
                return true;
            }
            else
            {
                objfilename.status = false;
                objfilename.message = "Error Ocuured while uploading document";
                return false;
            }
        }
        public bool DaEdituploadEnrollCertificate(HttpRequest httpRequest, UploadDocumentname objfilename, string employee_gid, string user_gid)
        {
            UploadDocumentList objdocumentmodel = new UploadDocumentList();
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
            string document_type = httpRequest.Form["document_type"].ToString();
            string lawyerregister_gid = httpRequest.Form["lawyerregister_gid"].ToString();
            string project_flag = httpRequest.Form["project_flag"].ToString();


            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";

            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "LGL/Enrolcertificate/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
            {
                if ((!System.IO.Directory.Exists(path)))
                    System.IO.Directory.CreateDirectory(path);
            }
            string msdocument_gid = objcmnfunctions.GetMasterGID("UECD");
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
                        //string lsfile_gid = msdocument_gid + FileExtension;
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
                            return false;
                        }

                        lspath = path;
                        objcmnfunctions.uploadFile(lspath, lsfile_gid);
                        lspath = "../../erp_documents" + "/" + lscompany_code + "/" + "LGL/Enrolcertificate/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension;

                        if (document_type == "undefined")
                        {
                            lsdocument_type = "";
                        }
                        else
                        {
                            lsdocument_type = document_type;
                        }
                        msGetGid = objcmnfunctions.GetMasterGID("UECD");
                        msSQL = " insert into lgl_tmp_tuploadcertificate( " +
                                    " document_gid," +
                                    " lawyerregister_gid ," +
                                    " document_name," +
                                    " document_path," +
                                    " document_type ," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + user_gid + "'," +
                                    "'" + httpPostedFile.FileName + "'," +
                                    "'" + lspath + msdocument_gid + FileExtension + "'," +
                                    "'" + lsdocument_type + "'," +
                                    "'" + user_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                        mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    }
                }
                msSQL = " select document_gid,document_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,document_path,document_type, " +
                     " concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as uploaded_by" +
                     " from lgl_tmp_tuploadcertificate a, adm_mst_tuser b where a.created_by=b.user_gid and (a.lawyerregister_gid='" + user_gid + "' or a.lawyerregister_gid='" + lawyerregister_gid + "')";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_filename = new List<UploadDocumentList>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_filename.Add(new UploadDocumentList
                        {
                            document_name = (dr_datarow["document_name"].ToString()),
                            document_gid = (dr_datarow["document_gid"].ToString()),
                            document_type = dr_datarow["document_type"].ToString(),
                            uploaded_by = dr_datarow["uploaded_by"].ToString(),
                            updated_date = dr_datarow["uploaded_date"].ToString()
                        });
                    }
                    objfilename.UploadDocumentList = get_filename;
                }
                dt_datatable.Dispose();
            }
            catch
            {

            }
            if (mnresult == 1)
            {
                objfilename.status = true;
                objfilename.message = "Document upload Successfully";
                return true;
            }
            else
            {
                objfilename.status = false;
                objfilename.message = "Error Occured while uploading document";
                return false;
            }

        }
        public bool DaGetDocumentCancel(string document_gid, resultvalue values)
        {

            msSQL = " delete from lgl_tmp_tuploadcertificate where document_gid= '" + document_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnresult != 0)
            {
                values.status = true;
                values.message = "Document deleted Sucessfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while deleting document";
                return false;
            }
        }
        public bool DaGetTempDelete(string user_gid, resultvalue value)
        {

            msSQL = " delete from lgl_tmp_tuploadcertificate where (lawyerregister_gid like 'SUS%' or lawyerregister_gid='U1')";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " delete from lgl_tmp_tphotoupload where (created_by like 'SUS%' or created_by='U1')";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnresult != 0)
            {
                value.status = true;
                value.message = "Document deleted Successfully";
                return true;
            }
            else
            {
                value.status = false;
                value.message = "Error Occured while deleting document";
                return false;
            }
        }

        public bool DaPostUploadPhoto(HttpRequest httpRequest, UploadDocumentname objfilename, string employee_gid, string user_gid)
        {
            UploadDocumentList objdocumentmodel = new UploadDocumentList();
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
            string project_flag = httpRequest.Form["project_flag"].ToString();

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";

            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "LGL/photos/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
            {
                if ((!System.IO.Directory.Exists(path)))
                    System.IO.Directory.CreateDirectory(path);
            }
            string msdocument_gid = objcmnfunctions.GetMasterGID("UPPD");
            string lsfirstdocument_filepath = string.Empty;

            httpFileCollection = httpRequest.Files;

            httpPostedFile = httpFileCollection[0];
            string FileExtension = httpPostedFile.FileName;
            //string lsfile_gid = msdocument_gid + FileExtension;
            string lsfile_gid = msdocument_gid;
            FileExtension = Path.GetExtension(FileExtension).ToLower();
            lsfile_gid = lsfile_gid + FileExtension;
            if ((FileExtension == ".jpg") || (FileExtension == ".jpeg") || (FileExtension == ".png"))
            {
                ls_readStream = httpPostedFile.InputStream;
                ls_readStream.CopyTo(ms);
                lspath = path;

                byte[] bytes = ms.ToArray();
                if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                {
                    objfilename.status = false;
                    objfilename.message = "File format is not supported";
                    return false;
                }

                //objcmnfunctions.uploadFile(lspath, lsfile_gid);
                //lspath = "../../erp_documents" + "/" + lscompany_code + "/" + "LGL/photos/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension;
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "LGL/photos/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                ms.Close();
                lspath = "erpdocument" + "/" + lscompany_code + "/" + "LGL/photos/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";


                msGetGid = objcmnfunctions.GetMasterGID("UPPD");
                msSQL = " insert into lgl_tmp_tphotoupload( " +
                             " photo_name," +
                             " lawyerphoto_path, " +
                             " created_by ," +
                             " created_date " +
                             " )values(" +
                             "'" + httpPostedFile.FileName + "'," +
                             "'" + lspath + msdocument_gid + FileExtension + "'," +
                             "'" + user_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else
            {
                objfilename.status = false;
                objfilename.message = "File format is not supported";
                return false;
            }
            if (mnresult != 0)
            {
                objfilename.status = true;
                objfilename.message = "Photo uploaded successfully";
                return true;
            }
            else
            {
                objfilename.status = false;
                objfilename.message = "Error Occured while uploading photo";
                return false;
            }
        }

        public bool DaPostLawyerLoginCreation(string employee_gid, lawyerlogin values)
        {
            msSQL = "select lawyer_name,email_address,mobile_no from lgl_mst_tregisterlawyer where lawyerregister_gid='" + values.lawyerregister_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lslawyer_name = objODBCDatareader["lawyer_name"].ToString();
                lslawyer_email = objODBCDatareader["email_address"].ToString();
                lsmobile_no = objODBCDatareader["mobile_no"].ToString();
            }
            objODBCDatareader.Close();

            msGetGid = objcmnfunctions.GetMasterGID("LAUR");
            msSQL = " insert into lgl_mst_tlawyeruser( " +
                        " lawyeruser_gid," +
                        " lawyerregister_gid ," +
                        " lawyeruser_code," +
                        " lawyeruser_name," +
                        " lawyeruser_password ," +
                        " lawyeruser_status," +
                        " register_type," +
                        " email_address," +
                        " created_by," +
                        " created_date" +
                        " )values(" +
                        "'" + msGetGid + "'," +
                        "'" + values.lawyerregister_gid + "'," +
                        "'" + values.lawyeruser_code + "'," +
                        "'" + lslawyer_name + "'," +
                        "'" + objcmnfunctions.ConvertToAscii(values.lawyer_userpassword) + "'," +
                        "'Y'," +
                        "'Lawyer'," +
                         "'" + lslawyer_email + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " update lgl_mst_tregisterlawyer set lawyeruser_status = 'Y',lawyeruser_password='" + values.lawyer_userpassword + "' " +
                   " where lawyerregister_gid='" + values.lawyerregister_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnresult != 0)
            {
                values.status = true;
                values.message = "Lawyer Login Activated Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
                return false;
            }
        }
        public bool DaPostLawyerActivationStatus(string employee_gid, lawyerlogin values)
        {
            if ((values.lawyeruser_activation == "Inactive") || (values.lawyeruser_activation == "Block"))
            {

                msSQL = "select c.lawyeruser_gid,b.lawyerregister_gid,status" +
                        " from lgl_mst_trequestcompliance a" +
                        " left join lgl_trn_trequestcompliance2lawyerdtl b on a.requestcompliance_gid = b.requestcompliance_gid" +
                        " left join lgl_mst_tlawyeruser c on b.lawyerregister_gid = c.lawyerregister_gid" +
                        " where b.lawyerregister_gid='" + values.lawyerregister_gid + "' and a.status<>'Completed'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    
                    values.status = false;
                    values.message = "You can't In-activate this lawyer, Lawyer tagged to Legal Services";
                    return false;
                }

                objODBCDatareader.Close();

                msSQL = " select a.SRassigned_lawyer" +
                        " from lgl_trn_traiselegalSR a" +
                        " left join lgl_mst_tlawyeruser b on b.lawyeruser_gid = a.SRassigned_lawyer" +
                        " where b.lawyerregister_gid='" + values.lawyerregister_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    
                    values.status = false;
                    values.message = "You can't in-activate this lawyer, Lawyer tagged to Legal SR";
                    return false;
                }
                objODBCDatareader.Close();

            }


            msSQL = " update lgl_mst_tregisterlawyer set lawyeruser_status = '" + values.lawyeruser_activation + "'," +
                    " lawyeruser_password='" + values.lawyeruser_password + "'," +
                       " lawyeractivation_updatedby='" + employee_gid + "'," +
                       " lawyeractivation_updateddate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                       " where lawyerregister_gid='" + values.lawyerregister_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (values.lawyeruser_activation == "Block")
            {
                msSQL = " update lgl_mst_tregisterlawyer set block_remarks='" + values.blockremarks + "'" +
                  " where lawyerregister_gid='" + values.lawyerregister_gid + "'";
                mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            msSQL = " update lgl_mst_tlawyeruser set lawyeruser_status='" + values.lawyeruser_activation + "'," +
                "lawyeruser_password='" + objcmnfunctions.ConvertToAscii(values.lawyeruser_password) + "'" +
                    " where lawyerregister_gid='" + values.lawyerregister_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);


            if (mnresult != 0)
            {
                values.status = true;
                values.message = "Activation Status Updated Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
                return false;
            }
        }

        public bool DaGetTempDocument(string user_gid, UploadDocumentname value)
        {
            msSQL = " select document_gid,document_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,document_path,document_type, " +
                    " concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as uploaded_by" +
                    " from lgl_tmp_tuploadcertificate a, adm_mst_tuser b where a.created_by=b.user_gid and lawyerregister_gid='" + user_gid + "'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_filename = new List<UploadDocumentList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    get_filename.Add(new UploadDocumentList
                    {
                        document_name = (dr_datarow["document_name"].ToString()),
                        document_gid = (dr_datarow["document_gid"].ToString()),
                        document_type = dr_datarow["document_type"].ToString(),
                        uploaded_by = dr_datarow["uploaded_by"].ToString(),
                        updated_date = dr_datarow["uploaded_date"].ToString()
                    });
                }
                value.UploadDocumentList = get_filename;
            }
            dt_datatable.Dispose();
            value.status = true;
            return true;
        }
        //-------Checking Enrolment Number Duplication-----------//
        public void DaGetenrolment_validation(mdlRegisterlawyer values, string enrolment_no)
        {
            msSQL = "select enrolment_no from lgl_mst_tregisterlawyer where enrolment_no='" + enrolment_no + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.enrolment_no = "Y";
                values.status = true;
                objODBCDatareader.Close();
            }
            else
            {
                values.enrolment_no = "N";
                values.status = false;
                objODBCDatareader.Close();
            }

        }
    }

}