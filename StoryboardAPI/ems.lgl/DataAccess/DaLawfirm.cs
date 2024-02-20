using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Odbc;
using System.Net;
using System.IO;
using ems.utilities.Functions;
using ems.storage.Functions;
using ems.lgl.Models;
using System.Configuration;
using System.Drawing;
namespace ems.lgl.DataAccess
{
    public class DaLawfirm
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable, dt_lawyer;
        string msSQL, msGetGid,msGetGid1,msgetGISREF, msGetGIDREF;
        string lspath,lsdocument_type;
        HttpPostedFile httpPostedFile;
        int mnResult;
        string lslawfirm_address, lslawfirm_remarks, lsfirmref_no, lsfirm_name;
        string lsemail_address;

        public bool PostLawfirm(MdlLawfirm values, string user_gid,string employee_gid)
        {
          
            msGetGid = objcmnfunctions.GetMasterGID("LAFR");
            msgetGISREF = objcmnfunctions.GetMasterGID("LFR_");

           
            if (values.firm_address!=null)
            {
                lslawfirm_address = (values.firm_address).Replace("'", "");
            }
            else
            {
                lslawfirm_address = "";
            }

            if (values.remarks!=null)
            {
                lslawfirm_remarks = (values.remarks).Replace("'", "");
            }
            else
            {
                lslawfirm_remarks = "";
            }
            msSQL = " insert into lgl_mst_tlawfirm(" +
                    " lawfirm_gid," +
                    " firm_refno," +
                    " firm_name," +
                    " contact_details," +
                    " mail_address," +
                    " firm_years," +
                    " firm_address," +  
                    " remarks ," +    
                    " website ,"+
                    " lawfirmuser_status,"+
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + msgetGISREF + "'," +
                    "'" + values.firm_name.Replace("'", "") + "'," +
                    "'" + values.contact_details.Replace("'", "") + "'," +
                    "'" + values.mail_address + "'," +
                    "'" + values.firm_years.Replace("'", "") + "'," +
                    "'" + lslawfirm_address + "'," +
                    "'" + lslawfirm_remarks  + "'," +
                    "'" + values.website +"',"+
                     "'Not Created'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            //for (var i = 0; i < values.lawfirm2lawyer_list.Count; i++)
            //{
                
            //    msGetGid1 = objcmnfunctions.GetMasterGID("LFLW");
            //    msSQL = " insert into lgl_trn_tlawfirm2lawyer(" +
            //            " lawfirm2lawyer_gid," +
            //            " lawfirm_gid," +
            //            " lawyerregister_gid," +
            //            " lawfirm_name," +
            //            " firm_refno," +
            //            " tagged_by," +
            //            " tagged_date)" +
            //            " values(" +
            //            "'" + msGetGid1 + "'," +
            //            "'" + msGetGid + "'," +
            //            "'" + values.lawfirm2lawyer_list[i].lawyerregister_gid + "'," +
            //            "'" + values.firm_name + "'," +
            //            "'" + values.firm_refno + "'," +
            //            "'" + user_gid + "'," +
            //            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            //    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            //}
            if (mnResult != 0)
            {
                msSQL = "update lgl_tmp_tuploadbankcertificate set lawfirm_gid='" + msGetGid + "' where lawfirm_gid='" + user_gid + "' and"+
                    "  created_by='" + user_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update lgl_trn_tlawfirm2member set lawfirm_gid='" + msGetGid + "' where lawfirm_gid='" + employee_gid + "' and" +
                    "  created_by='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "select * from lgl_trn_tlawfirm2member where lawfirm_gid='" + msGetGid + "'";
                 dt_datatable = objdbconn.GetDataTable(msSQL);
               
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        msSQL = "select lawyerregister_gid from lgl_mst_tregisterlawyer where enrolment_no='" + dr_datarow["enrolment_no"].ToString() + "'";
                        string lslawyerregister_gid = objdbconn.GetExecuteScalar(msSQL);
                        if(lslawyerregister_gid == "")
                        {
                            msSQL= "select * from lgl_trn_tlawfirm2member where lawfirm_gid='" + dr_datarow["lawfirm_gid"].ToString() + "' and"+
                            " enrolment_no='" + dr_datarow["enrolment_no"].ToString() + "'"; 
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if(objODBCDatareader.HasRows==true)
                            {
                            msGetGid1 = objcmnfunctions.GetMasterGID("RELR");
                            msGetGIDREF = objcmnfunctions.GetMasterGID("MEM_");
                            msSQL = " insert into lgl_mst_tregisterlawyer(" +
                                    " lawyerregister_gid," +
                                   " lawyerref_no ," +
                                    " lawyer_name ," +
                                    " mobile_no," +
                                    " email_address," +
                                    " enrolment_no," +
                                      " login_status," +
                                      " created_by," +
                                      " created_date)" +
                                    " values(" +
                                    "'" + msGetGid1 + "'," +
                                    "'" + msGetGIDREF + "'," +
                                    "'" + objODBCDatareader["member_name"].ToString() + "'," +
                                    "'" + objODBCDatareader["mobile_no"].ToString() + "'," +
                                    "'" + objODBCDatareader["email_address"].ToString() + "'," +
                                    "'" + objODBCDatareader["enrolment_no"].ToString() + "'," +
                                    "'Tagged in Lawfirm'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }
                            objODBCDatareader.Close();
                        }
                    }
                   
                }
                dt_datatable.Dispose();

                values.status = true;
                values.message = "Law Firm added sucessfully";
                return true;   
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while adding Law Firm";
                return false;
            }
        }

        public bool DaGetLawFirmDetail(mdllawfirmdtl objLawfirm)
        {

            msSQL = " select a.lawfirm_gid,a.firm_refno,a.firm_name,a.mail_address,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, a.firm_years,"+
                " a.lawfirmuser_status,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by  from lgl_mst_tlawfirm a"+
                "  left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                " left join adm_mst_tuser c on c.user_gid=b.user_gid order by a.created_date desc ";
            
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getlawfirm = new List<lawfirm_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getlawfirm.Add(new lawfirm_list
                    {
                        firm_refno = (dr_datarow["firm_refno"].ToString()),
                        firm_name = (dr_datarow["firm_name"].ToString()),
                        email_address = (dr_datarow["mail_address"].ToString()),
                        no_years = (dr_datarow["firm_years"].ToString()),
                        lawfirm_gid = (dr_datarow["lawfirm_gid"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        lawfirmuser_status = (dr_datarow["lawfirmuser_status"].ToString()),
                        created_by = dr_datarow["created_by"].ToString(),
                    });
                }
                objLawfirm.lawfirm_list = getlawfirm;
            }
            dt_datatable.Dispose();
            objLawfirm.status = true;
            return true;
        }

        public bool DaGetFirmDetails(string user_gid,string lawfirm_gid, lawfirmedit values)
        {
            msSQL = " select * from lgl_mst_tlawfirm where lawfirm_gid='" + lawfirm_gid + "' ";
            
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                values.txtfirm_refnoedit = objODBCDatareader["firm_refno"].ToString();
                values.txtfirm_nameedit = objODBCDatareader["firm_name"].ToString();
                values.txtcontact_noedit = objODBCDatareader["contact_details"].ToString();
                values.txtemailaddressedit = objODBCDatareader["mail_address"].ToString();
                values.txtexperienceedit = objODBCDatareader["firm_years"].ToString();
                values.txtaddress1edit = objODBCDatareader["firm_address"].ToString();
                values.txtremarksedit = objODBCDatareader["remarks"].ToString();
                values.txtwebsiteedit = objODBCDatareader["website"].ToString();
                values.lawfirm_gid = lawfirm_gid;

            }
            objODBCDatareader.Close();
            msSQL = " select document_gid,document_name,concat(date_format(a.created_date, '%d-%m-%Y %H:%i %p')) as uploaded_date,document_path,document_type," +
                        " concat(b.user_firstname, ' ', b.user_lastname, ' / ', b.user_code) as uploaded_by from lgl_tmp_tuploadbankcertificate a, adm_mst_tuser b" +
                        " where a.created_by = b.user_gid and ( lawfirm_gid='" + lawfirm_gid + "' or lawfirm_gid='" + user_gid + "') ";
            
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdocument = new List<UploadDocument>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getdocument.Add(new UploadDocument
                    {
                        document_gid = (dr_datarow["document_gid"].ToString()),
                        filename = (dr_datarow["document_name"].ToString()),
                        document_type=dr_datarow["document_type"].ToString(),
                        uploaded_by=dr_datarow["uploaded_by"].ToString(),
                        updated_date=dr_datarow["uploaded_date"].ToString()
                    });
                }
                values.UploadDocument = getdocument;
            }
            dt_datatable.Dispose();
            
            msSQL = " SELECT lawyerregister_gid FROM lgl_trn_tlawfirm2lawyer WHERE lawfirm_gid='" + lawfirm_gid + "'";
            dt_lawyer = objdbconn.GetDataTable(msSQL);
            
            values.lawyerlist_edit = dt_lawyer.AsEnumerable().Select(row => new lawyerlist_edit { lawyerregister_gid = row["lawyerregister_gid"].ToString() }).ToList();
            dt_lawyer.Dispose();
            values.status = true;
            return true;
        }

        public bool DaPostUpdateLawFirm(string employee_gid,string user_gid, lawfirmedit values)
        {
            
            msSQL = " update lgl_mst_tlawfirm set " +
                    " firm_refno='" + values.txtfirm_refnoedit + "'," +
                    " firm_name='" + values.txtfirm_nameedit + "'," +
                    " contact_details='" + values.txtcontact_noedit.Replace("'", "") + "'," +
                    " mail_address='" + values.txtemailaddressedit + "'," +
                    " firm_years='" + values.txtexperienceedit + "',"  +
                    " firm_address='" + values.txtaddress1edit.Replace("'", "") + "'," +
                    " remarks='"+values.txtremarksedit.Replace("'","")+"',"+
                    " website='"+values.txtwebsiteedit.Replace("'","")+"',"+
                    " updated_by='" + user_gid + "'," +
                    " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    " where lawfirm_gid='" + values.lawfirm_gid + "' ";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            //msSQL = "insert into  lgl_trn_tlawfirm2lawyerlog (select * from  lgl_trn_tlawfirm2lawyer where  lawfirm_gid='" + values.lawfirm_gid + "' ) ";
            //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            //msSQL = "delete from lgl_trn_tlawfirm2lawyer  where  lawfirm_gid='" + values.lawfirm_gid + "'";
            //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            //for (var i = 0; i < values.lawyerlist_edit.Count; i++)
            //{

            //        msGetGid1 = objcmnfunctions.GetMasterGID("LFLW");
            //        msSQL = " insert into lgl_trn_tlawfirm2lawyer(" +
            //                " lawfirm2lawyer_gid," +
            //                " lawfirm_gid," +
            //                " lawyerregister_gid," +
            //                " lawfirm_name," +
            //                " firm_refno," +
            //                " tagged_by," +
            //                " tagged_date)" +
            //                " values(" +
            //                "'" + msGetGid1 + "'," +
            //                "'" + values.lawfirm_gid + "'," +
            //                "'" + values.lawyerlist_edit[i].lawyerregister_gid + "'," +
            //                "'" + values.txtfirm_nameedit + "'," +
            //                "'" + values.txtfirm_refnoedit + "'," +
            //                "'" + user_gid + "'," +
            //                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            //        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            //    }

            msSQL = "update lgl_trn_tlawfirm2member set  lawfirm_gid='" + values.lawfirm_gid + "' where lawfirm_gid='" + employee_gid + "' and" +
                "  created_by='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            msSQL = "update lgl_tmp_tuploadbankcertificate set lawfirm_gid='" + values.lawfirm_gid + "' where lawfirm_gid='" + user_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "select * from lgl_trn_tlawfirm2member where lawfirm_gid='" + values.lawfirm_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);

            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    msSQL = "select lawyerregister_gid from lgl_mst_tregisterlawyer where enrolment_no='" + dr_datarow["enrolment_no"].ToString() + "'";
                    string lslawyerregister_gid = objdbconn.GetExecuteScalar(msSQL);
                    if (lslawyerregister_gid == "")
                    {
                        msSQL = "select * from lgl_trn_tlawfirm2member where lawfirm_gid='" + dr_datarow["lawfirm_gid"].ToString() + "' and" +
                        " enrolment_no='" + dr_datarow["enrolment_no"].ToString() + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            msGetGid1 = objcmnfunctions.GetMasterGID("RELR");
                            msGetGIDREF = objcmnfunctions.GetMasterGID("MEM_");
                            msSQL = " insert into lgl_mst_tregisterlawyer(" +
                                    " lawyerregister_gid," +
                                   " lawyerref_no ," +
                                    " lawyer_name ," +
                                    " mobile_no," +
                                    " email_address," +
                                    " enrolment_no," +
                                      " login_status," +
                                      " created_by," +
                                      " created_date)" +
                                    " values(" +
                                    "'" + msGetGid1 + "'," +
                                    "'" + msGetGIDREF + "'," +
                                    "'" + objODBCDatareader["member_name"].ToString() + "'," +
                                    "'" + objODBCDatareader["mobile_no"].ToString() + "'," +
                                    "'" + objODBCDatareader["email_address"].ToString() + "'," +
                                    "'" + objODBCDatareader["enrolment_no"].ToString() + "'," +
                                    "'Tagged in Lawfirm'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                        objODBCDatareader.Close();
                    }
                }

            }
            dt_datatable.Dispose();
            if (mnResult!=0)
            {
                values.status = true;
                values.message = "Law Firm updated successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while updating Law Firm";
                return false;
            }
          
        }

        public bool DaGetLawFirm(string lawfirm_gid, lawfirmedit values)
        {
            
            msSQL = " delete from lgl_mst_tlawfirm where lawfirm_gid='" + lawfirm_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            
            if (mnResult != 0)
            {
                
                msSQL = "delete from lgl_tmp_tuploadbankcertificate where lawfirm_gid='" + lawfirm_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "delete from lgl_trn_tlawfirm2member where lawfirm_gid='" + lawfirm_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Law Firm deleted successfully";
                return true;
            }
          
            else
            {
                values.message = "Error Occured while deleting Law Firm";
                return false;
            }
           
        }

        public bool DaGetLaw(string lawfirm_gid, MdlLawfirm values)
        {
            
            msSQL = " select * from lgl_mst_tlawfirm where lawfirm_gid='" + lawfirm_gid + "' ";            
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                values.firm_refno = objODBCDatareader["firm_refno"].ToString();
                values.firm_name = objODBCDatareader["firm_name"].ToString();
                values.lawfirm_gid = lawfirm_gid;

            }
            objODBCDatareader.Close();
            values.status = true;
            return true;
        }

        public bool DaGetLawfirmTag(string employee_gid,MdlLawfirm values)
        {
            
            msGetGid1 = objcmnfunctions.GetMasterGID("LFLW");
            msSQL = " insert into lgl_trn_tlawfirm2lawyer(" +
                    " lawfirm2lawyer_gid," +
                    " lawfirm_gid," +
                    " lawyerregister_gid," +
                    " lawyer_name," +
                    " lawfirm_name," +
                    " firm_refno," +
                    " tagged_by," +
                    " tagged_date)" +
                    " values(" +
                    "'" + msGetGid1 + "'," +
                    "'" + values.lawfirm_gid + "'," +
                    "'" + values.lawyerregister_gid + "'," +
                    "'" + values.lawyer.Replace("'", "") + "'," +
                    "'" + values.firm_name + "'," +
                    "'" + values.firm_refno + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Lawyer Tagged Sucessfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while tagged lawyer";
                return false;
            }
        }

        public bool DaGetLawFirmView(string lawfirm_gid, Lawfirmupload objfilename, string user_gid)
        {
            msSQL = " select lawfirm_gid,firm_refno,firm_name, contact_details,lawfirmuser_status,lawfirmuser_password,block_remarks," +
            " mail_address,firm_years,firm_address,remarks,website from lgl_mst_tlawfirm " +
            " where lawfirm_gid = '" + lawfirm_gid + "'";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                objfilename.lawfirm_gid = objODBCDatareader["lawfirm_gid"].ToString();
                objfilename.firm_refno = objODBCDatareader["firm_refno"].ToString();
                objfilename.firm_name = objODBCDatareader["firm_name"].ToString();
                objfilename.contact_details = objODBCDatareader["contact_details"].ToString();
                objfilename.mail_address = objODBCDatareader["mail_address"].ToString();
                objfilename.firm_years = objODBCDatareader["firm_years"].ToString();
                objfilename.firm_address = objODBCDatareader["firm_address"].ToString();
                objfilename.remarks = objODBCDatareader["remarks"].ToString();
                objfilename.website = objODBCDatareader["website"].ToString();
                objfilename.lawfirmuser_status = objODBCDatareader["lawfirmuser_status"].ToString();
                objfilename.lawfirmuser_password = objODBCDatareader["lawfirmuser_password"].ToString();
                objfilename.block_remarks = objODBCDatareader["block_remarks"].ToString();
            }
            objODBCDatareader.Close();
            msSQL = " select document_gid,document_name,concat(date_format(a.created_date, '%d-%m-%Y %H:%i %p')) as uploaded_date,document_path,document_type," +
                        " concat(b.user_firstname, ' ', b.user_lastname, ' / ', b.user_code) as uploaded_by from lgl_tmp_tuploadbankcertificate a, adm_mst_tuser b" +
                        " where a.created_by = b.user_gid and lawfirm_gid='" + lawfirm_gid + "'";
            
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdocument = new List<UploadDocument>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getdocument.Add(new UploadDocument
                    {
                        document_gid = (dr_datarow["document_gid"].ToString()),
                        filename = (dr_datarow["document_name"].ToString()),
                        path = objcmnstorage.EncryptData(dr_datarow["document_path"].ToString()),
                        document_type=dr_datarow["document_type"].ToString(),
                        uploaded_by = dr_datarow["uploaded_by"].ToString(),
                        updated_date = dr_datarow["uploaded_date"].ToString()

                    });
                }
                objfilename.UploadDocument = getdocument;
            }
            dt_datatable.Dispose();

            msSQL = " select a.lawfirm_gid,b.lawyerref_no,b.lawyer_name,b.mobile_no,date_format(b. date_enrolment, '%d-%m-%Y') as date_enrolment," +
          " experience,b.email_address from lgl_trn_tlawfirm2lawyer a " +
          " left join lgl_mst_tregisterlawyer b on a.lawyerregister_gid=b.lawyerregister_gid " +
          " where lawfirm_gid = '" + lawfirm_gid + "'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getlawfirmdtl = new List<lawfirm_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getlawfirmdtl.Add(new lawfirm_list
                    {
                        lawfirm_gid = (dr_datarow["lawfirm_gid"].ToString()),
                        lawyerref_no = (dr_datarow["lawyerref_no"].ToString()),
                        lawyer_name = (dr_datarow["lawyer_name"].ToString()),
                        mobile_no = (dr_datarow["mobile_no"].ToString()),
                        date_enrolment = (dr_datarow["date_enrolment"].ToString()),
                        experience=dr_datarow["experience"].ToString (),
                        email_address=dr_datarow["email_address"].ToString()

                    });
                }
                objfilename.lawfirm_list = getlawfirmdtl;
            }
            dt_datatable.Dispose();
            objfilename.status = true;
            return true;
        }

        public bool DaGetLawFirmDetails(Lawfirmupload objLawfirmdtl, string lawfirm_gid)
        {
        msSQL = " select a.lawfirm_gid,b.lawyerref_no,b.lawyer_name,b.mobile_no,date_format(b.date_enrolment, '%d-%m-%Y') as date_enrolment," +
            " experience,b.email_address from lgl_trn_tlawfirm2lawyer a " +
            " left join lgl_mst_tregisterlawyer b on a.lawyerregister_gid=b.lawyerregister_gid " +
            " where lawfirm_gid = '" + lawfirm_gid + "'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getlawfirmdtl = new List<lawfirm_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getlawfirmdtl.Add(new lawfirm_list
                    {
                        lawfirm_gid = (dr_datarow["lawfirm_gid"].ToString()),
                        lawyerref_no = (dr_datarow["lawyerref_no"].ToString()),
                        lawyer_name = (dr_datarow["lawyer_name"].ToString()),
                        mobile_no = (dr_datarow["mobile_no"].ToString()),
                        date_enrolment = (dr_datarow["date_enrolment"].ToString()),
                        experience=dr_datarow["experience"].ToString (),
                        email_address=dr_datarow["email_address"].ToString()
                    });
                }
                objLawfirmdtl.lawfirm_list = getlawfirmdtl;
            }
            dt_datatable.Dispose();
            objLawfirmdtl.status = true;
            return true;
        }
        public bool DaUploadEnrollCertificate(HttpRequest httpRequest, Lawfirmupload objfilename, string employee_gid, string user_gid)
        {
            UploadDocument objdocumentmodel = new UploadDocument();
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


            msSQL = "SELECT a.company_code FROM adm_mst_tcompany a";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);

            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "LGL/BankEmpanel/" + DateTime.Now.Year + "/" + DateTime.Now.Month+"/";
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
                        //string lsfile_gid = msdocument_gid + FileExtension;
                        string lsfile_gid = msdocument_gid;
                        FileExtension = Path.GetExtension(FileExtension).ToLower();
                        lsfile_gid = lsfile_gid + FileExtension;
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
                        //lspath = "../../erp_documents" + "/" + lscompany_code + "/" + "LGL/BankEmpanel/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension;
                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "LGL/BankEmpanel/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "LGL/BankEmpanel/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        if (document_type == "undefined")
                        {
                            lsdocument_type = "";
                        }
                        else
                        {
                            lsdocument_type = document_type;
                        }
                        msGetGid = objcmnfunctions.GetMasterGID("UECD");
                        msSQL = " insert into lgl_tmp_tuploadbankcertificate( " +
                                    " document_gid," +
                                    " document_name," +
                                    " document_path ," +
                                    " document_type ,"+
                                    " lawfirm_gid ,"+
                                    " created_by ,"+
                                    " created_date "+
                                    " )values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + httpPostedFile.FileName + "'," +
                                     "'" + lspath + msdocument_gid + FileExtension + "'," +
                                    "'" + lsdocument_type + "',"+
                                    "'" + user_gid+"',"+
                                    "'" + user_gid+"',"+
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        
                       
                    }
                }
                msSQL = " select document_gid,document_name,concat(date_format(a.created_date, '%d-%m-%Y %H:%i %p')) as uploaded_date,document_path,document_type,"+
                        " concat(b.user_firstname, ' ', b.user_lastname, ' / ', b.user_code) as uploaded_by from lgl_tmp_tuploadbankcertificate a, adm_mst_tuser b"+
                        " where a.created_by = b.user_gid and  lawfirm_gid ='" + user_gid + "'";
                
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_filename = new List<UploadDocument>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_filename.Add(new UploadDocument
                        {
                            filename = (dr_datarow["document_name"].ToString()),
                            document_gid = (dr_datarow["document_gid"].ToString()),
                            document_type = (dr_datarow["document_type"].ToString()),
                            uploaded_by = dr_datarow["uploaded_by"].ToString(),
                            updated_date = dr_datarow["uploaded_date"].ToString()
                        });
                    }
                    objfilename.UploadDocument = get_filename;
                }
                dt_datatable.Dispose();
                
            }
            catch
            {

            }
            if (mnResult != 0)
            {
                objfilename.status = true;
                objfilename.message = "Document Uploaded Successfully";
                return true;
            }
            else
            {
                objfilename.status = false;
                objfilename.message = "Error Occured while uploading document";
                return false;
            }
        }
        public bool DaEditUploadEnrollcertificate_da(HttpRequest httpRequest, Lawfirmupload objfilename, string employee_gid, string user_gid)
        {
            UploadDocument objdocumentmodel = new UploadDocument();
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
            string lawfirm_gid = httpRequest.Form["lawfirm_gid"].ToString();
            string project_flag = httpRequest.Form["project_flag"].ToString();


            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "LGL/BankEmpanel/" + DateTime.Now.Year + "/" + DateTime.Now.Month+ "/";
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
                        //string lsfile_gid = msdocument_gid + FileExtension;
                        string lsfile_gid = msdocument_gid;
                        FileExtension = Path.GetExtension(FileExtension).ToLower();
                        lsfile_gid = lsfile_gid + FileExtension;
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

                        //lspath = "../../erp_documents" + "/" + lscompany_code + "/" + "LGL/BankEmpanel/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension;
                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "LGL/BankEmpanel/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "LGL/BankEmpanel/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        if (document_type == "undefined")
                        {
                            lsdocument_type = "";
                        }
                        else
                        {
                            lsdocument_type = document_type;
                        }
                        msGetGid = objcmnfunctions.GetMasterGID("UECD");
                        msSQL = " insert into lgl_tmp_tuploadbankcertificate( " +
                                    " document_gid," +
                                    " document_name," +
                                    " document_path ," +
                                    " document_type ," +
                                    " lawfirm_gid ," +
                                    " created_by ," +
                                    " created_date " +
                                    " )values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + httpPostedFile.FileName + "'," +
                                    "'" + lspath + msdocument_gid + FileExtension + "'," +
                                    "'" + lsdocument_type + "'," +
                                    "'" + user_gid + "'," +
                                    "'" + user_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        
                        
                    }
                }
                msSQL = " select document_gid,document_name,concat(date_format(a.created_date, '%d-%m-%Y %H:%i %p')) as uploaded_date,document_path,document_type," +
                        " concat(b.user_firstname, ' ', b.user_lastname, ' / ', b.user_code) as uploaded_by from lgl_tmp_tuploadbankcertificate a, adm_mst_tuser b" +
                        " where a.created_by = b.user_gid and ( lawfirm_gid='" + user_gid + "' or lawfirm_gid='" + lawfirm_gid + "')";
                
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_filename = new List<UploadDocument>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_filename.Add(new UploadDocument
                        {
                            filename = (dr_datarow["document_name"].ToString()),
                            document_gid = (dr_datarow["document_gid"].ToString()),
                            document_type = dr_datarow["document_type"].ToString(),
                            uploaded_by = dr_datarow["uploaded_by"].ToString(),
                            updated_date = dr_datarow["uploaded_date"].ToString()
                        });
                    }
                    objfilename.UploadDocument = get_filename;
                }
                dt_datatable.Dispose();
                
            }
            catch
            {

            }
            if (mnResult != 0)
            {
                objfilename.status = true;
                objfilename.message = "Document Upload Successfully";
                return true;
            }
            else
            {
                objfilename.status = false;
                objfilename.message = "Error Occured while uploading document";
                return false;
            }
        }
        public bool DaGetDocumentCancel(string document_gid,resultvalue value)
        {
            
            msSQL = " delete from lgl_tmp_tuploadbankcertificate where document_gid= '" + document_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult!=0)
            {
                value.status = true;
                value.message = "Document Deleted Successfully";
                return true;
            }
            else
            {
                value.status = false;
                value.message = "Error Occured while deleting document";
                return false;
            }
        }
        public bool DaGetLawFirm2Lawyer(Lawfirm2lawyer values)
        {
            
            msSQL = " select lawyerregister_gid,lawyer_name " +
                    " from lgl_mst_tregisterlawyer where lawyeruser_status='Y' order by created_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_lawyer = new List<lawfirm2lawyer_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    get_lawyer.Add(new lawfirm2lawyer_list
                    {
                        lawyerregister_gid = dr_datarow["lawyerregister_gid"].ToString(),
                        lawyer_name = dr_datarow["lawyer_name"].ToString()
               
                    });
                }
                values.lawfirm2lawyer_list = get_lawyer;
                         
            }
            dt_datatable.Dispose();
            values.status = true;
            return true;
        }
        public bool DaGetTempDelete(string user_gid,resultvalue value)
        {
            
            msSQL = " delete from lgl_tmp_tuploadbankcertificate where (lawfirm_gid like 'SUS%' or lawfirm_gid='U1')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " delete from lgl_mst_tregisterlawyer where (lawfirm_gid like 'SER%' or lawfirm_gid='E1')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult !=0)
            {
                value.status = true;
                value.message = "Document deleted successfully";
                return true;
            }
            else
            {
                value.status = false;
                value.message = "Error Occured while deleting";
                return false;
            }
        }
        public bool Dapostpostmembers(mdlmembername values, string employee_gid)
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
           
                msGetGid = objcmnfunctions.GetMasterGID("LFLW");

                msSQL = " insert into lgl_trn_tlawfirm2member(" +
                        " lawfirmmember_gid," +
                        " lawfirm_gid," +
                        " member_name," +
                        " mobile_no," +
                        " email_address," +
                        " designation," +
                        " enrolment_no," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + employee_gid + "'," +
                        "'" + values.lawyer_name.Replace("  ", "") + "'," +
                        "'" + values.mobile_no + "'," +
                        "'" + values.email_address + "'," +
                        "'" + values.designation.Replace("'", "") + "'," +
                        "'" + values.enrolment_no + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if(mnResult!=0)
            { 
                values.status = true;
                values.message = "Member added sucessfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while adding Member";
                return false;
            }
        }
        public void DaGettempmember(string employee_gid, mdlmembername values)
        {
            msSQL = "select member_name,mobile_no,designation,email_address,enrolment_no,lawfirmmember_gid from lgl_trn_tlawfirm2member where lawfirm_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmember_list = new List<member_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmember_list.Add(new member_list
                    {
                        lawyer_name = (dr_datarow["member_name"].ToString()),
                        mobile_no = (dr_datarow["mobile_no"].ToString()),
                        designation = dr_datarow["designation"].ToString(),
                        email_address = dr_datarow["email_address"].ToString(),
                        enrolment_no = dr_datarow["enrolment_no"].ToString(),
                        lawfirmmember_gid = dr_datarow["lawfirmmember_gid"].ToString()

                    });
                }
                values.member_list = getmember_list;
            }
            dt_datatable.Dispose();
        }
        public void Damemberdelete(string lawfirmmember_gid, resultvalue values)
        {
            msSQL = "delete from lgl_trn_tlawfirm2member where lawfirmmember_gid='" + lawfirmmember_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                values.status = true;
                values.message = "Lawyer deleted sucessfully";
                
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while deleting Lawyer";
               
            }
        }
        public void Dagetlawfirm2member(string lawfirm_gid,mdlmembername values)
        {
            msSQL = "select member_name,mobile_no,designation,email_address,enrolment_no,lawfirmmember_gid from lgl_trn_tlawfirm2member where lawfirm_gid='" + lawfirm_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmember_list = new List<member_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmember_list.Add(new member_list
                    {
                        lawyer_name = (dr_datarow["member_name"].ToString()),
                        mobile_no = (dr_datarow["mobile_no"].ToString()),
                        designation = dr_datarow["designation"].ToString(),
                        email_address = dr_datarow["email_address"].ToString(),
                        enrolment_no = dr_datarow["enrolment_no"].ToString(),
                        lawfirmmember_gid = dr_datarow["lawfirmmember_gid"].ToString()

                    });
                }
                values.member_list = getmember_list;
            }
            dt_datatable.Dispose();
        }
        public void DagetEditlawfirm2member(string employee_gid,string lawfirm_gid, mdlmembername values)
        {
            msSQL = "select member_name,mobile_no,designation,email_address,enrolment_no,lawfirmmember_gid from lgl_trn_tlawfirm2member where " +
                " ( lawfirm_gid='" + lawfirm_gid + "' or lawfirm_gid='" + employee_gid + "')";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmember_list = new List<member_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmember_list.Add(new member_list
                    {
                        lawyer_name = (dr_datarow["member_name"].ToString()),
                        mobile_no = (dr_datarow["mobile_no"].ToString()),
                        designation = dr_datarow["designation"].ToString(),
                        email_address = dr_datarow["email_address"].ToString(),
                        enrolment_no = dr_datarow["enrolment_no"].ToString(),
                        lawfirmmember_gid = dr_datarow["lawfirmmember_gid"].ToString()

                    });
                }
                values.member_list = getmember_list;
            }
            dt_datatable.Dispose();
        }
        public bool Dapostlawfirmlogincreation(string employee_gid, lawfirmlogin values)
        {
            msSQL = "select firm_refno,firm_name,mail_address from lgl_mst_tlawfirm where lawfirm_gid='" + values.lawfirm_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsfirmref_no = objODBCDatareader["firm_refno"].ToString();
                lsfirm_name = objODBCDatareader["firm_name"].ToString();
                lsemail_address = objODBCDatareader["mail_address"].ToString();

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
                        " register_type,"+
                         " email_address," +
                        " created_by," +
                        " created_date" +
                        " )values(" +
                        "'" + msGetGid + "'," +
                        "'" + values.lawfirm_gid + "'," +
                        "'" + values.lawfirmuser_code + "'," +
                        "'" + lsfirm_name + "'," +
                        "'" + objcmnfunctions.ConvertToAscii(values.lawfirmuser_password) + "'," +
                        "'Y'," +
                       "'Law Firm'," +
                        "'" + lsemail_address + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " update lgl_mst_tlawfirm set lawfirmuser_status = 'Y',lawfirmuser_password='" + values.lawfirmuser_password + "' " +
                   " where lawfirm_gid='" + values.lawfirm_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Law Firm Login Activated Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
                return false;
            }
        }
        public bool DaPostlawfirmactivationstatus(string employee_gid, lawfirmlogin values)
        {
            if ((values.lawfirmuser_activation == "Inactive") || (values.lawfirmuser_activation == "Block"))
            {

              
                msSQL = "select c.lawyeruser_gid,b.lawyerregister_gid,status" +
                        " from lgl_mst_trequestcompliance a" +
                        " left join lgl_trn_trequestcompliance2lawyerdtl b on a.requestcompliance_gid = b.requestcompliance_gid" +
                        " left join lgl_mst_tlawyeruser c on b.lawyerregister_gid = c.lawyerregister_gid" +
                        " where b.lawyerregister_gid='" + values.lawfirm_gid + "' and a.status<>'Completed'";
              
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    
                    values.status = false;
                    values.message = "You can't in-activate this lawyer, Lawyer tagged to Compliance Request";
                    return false;
                }

                objODBCDatareader.Close();

                msSQL = " select a.SRassigned_lawyer" +
                        " from lgl_trn_traiselegalSR a" +
                        " left join lgl_mst_tlawyeruser b on b.lawyeruser_gid = a.SRassigned_lawyer" +
                        " where b.lawyerregister_gid='" + values.lawfirm_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    
                    values.status = false;
                    values.message = "You can't in-activate this lawyer, Lawyer tagged to Legal SR";
                    return false;
                }
                objODBCDatareader.Close();

            }


            msSQL = " update lgl_mst_tlawfirm set lawfirmuser_status = '" + values.lawfirmuser_activation + "'," +
                    " lawfirmuser_password='" + values.lawfirmuser_password + "'," +
                       " lawfirmactivation_updatedby='" + employee_gid + "'," +
                       " lawfirmactivation_updateddate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                       " where lawfirm_gid='" + values.lawfirm_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (values.lawfirmuser_activation == "Block")
            {
                msSQL = " update lgl_mst_tlawfirm set block_remarks='" + values.blockremarks + "'" +
                  " where lawfirm_gid='" + values.lawfirm_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            msSQL = " update lgl_mst_tlawyeruser set lawyeruser_status='" + values.lawfirmuser_activation + "'," +
                "lawyeruser_password='" + objcmnfunctions.ConvertToAscii(values.lawfirmuser_password) + "'" +
                    " where lawyerregister_gid='" + values.lawfirm_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            if (mnResult != 0)
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
        //-----------View Member---------------//
        public void DaGetviewmember(string employee_gid, string lawfirm_gid, mdlmembername values)
        {
            msSQL = "select member_name,mobile_no,designation,email_address,enrolment_no,lawfirmmember_gid from lgl_trn_tlawfirm2member where " +
                " lawfirm_gid='" + lawfirm_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmember_list = new List<member_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmember_list.Add(new member_list
                    {
                        lawyer_name = (dr_datarow["member_name"].ToString()),
                        mobile_no = (dr_datarow["mobile_no"].ToString()),
                        designation = dr_datarow["designation"].ToString(),
                        email_address = dr_datarow["email_address"].ToString(),
                        enrolment_no = dr_datarow["enrolment_no"].ToString(),
                        lawfirmmember_gid = dr_datarow["lawfirmmember_gid"].ToString()

                    });
                }
                values.member_list = getmember_list;
            }
            dt_datatable.Dispose();
        }
        public bool DaTagLawyer2lawfirm(mdlmembername values, string employee_gid)
        {
      

            msSQL = "update lgl_mst_tregisterlawyer set login_status='Individual and Tagged in Law Firm' where lawyerregister_gid='" + values.lawyerregister_gid + "'";
                  
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            
            if (mnResult != 0)
            {
               
                msGetGid = objcmnfunctions.GetMasterGID("LFLW");
                msSQL = "select * from lgl_mst_tregisterlawyer where lawyerregister_gid='" + values.lawyerregister_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    msSQL = " insert into lgl_trn_tlawfirm2member(" +
                       " lawfirmmember_gid," +
                       " lawfirm_gid," +
                       " member_name," +
                       " mobile_no," +
                       " email_address," +
                       " designation," +
                       " enrolment_no," +
                       " created_by," +
                       " created_date)" +
                       " values(" +
                       "'" + msGetGid + "'," +
                       "'" + employee_gid + "'," +
                       "'" + objODBCDatareader["lawyer_name"].ToString () + "'," +
                       "'" + objODBCDatareader["mobile_no"].ToString() + "'," +
                       "'" + objODBCDatareader["email_address"].ToString() + "'," +
                       "'" + values.designation.Replace("'", "") + "'," +
                       "'" + objODBCDatareader["enrolment_no"].ToString() + "'," +
                       "'" + employee_gid + "'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                objODBCDatareader.Close();
                values.status = true;
                values.message = "Lawyer Tagged sucessfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while tagging";
                return false;
            }
        }
        //-------Checking Enrolment Number Duplication-----------//
        public void DaGetLawfirmenrolment_validation(mdlmembername values, string enrolment_no)
        {
            msSQL = "select enrolment_no from lgl_trn_tlawfirm2member where enrolment_no='" + enrolment_no + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objODBCDatareader.Close();
                values.enrolment_no = "Y";
                values.status = true;
            }
            else
            {
                objODBCDatareader.Close();
                values.enrolment_no = "N";
                values.status = false;
            }
            
        }
    }
}