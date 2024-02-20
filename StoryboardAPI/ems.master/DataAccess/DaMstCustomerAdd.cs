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
using ems.master.Models;
using System.Configuration;

using System.Drawing;

namespace ems.master.DataAccess
{
    public class DaMstCustomerAdd
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        HttpPostedFile httpPostedFile;
        OdbcDataReader objODBCDatareader, objreader;
        DataTable dt_datatable, dt_table;
        string msSQL, msGetGid, msGetGid1, msGetGidREF, lspath, lsemployeeGID;
        int mnResult;
        string lscustomer_name, lscustomer2userdtl_gid, lsmobile_no, lspersonalemail_address,lspan_no,lsgst_no, lscontact_person;
        string lscustomer2usertype_gid, lscustomer_type;
        //----------- Submit Address Type----------//
        public bool Dapostaddresstype(string employee_gid, MdlMstaddresstype values)
        {
            msSQL = "select primary_address from ocs_mst_tcustomer2address where primary_address='Yes' and customer2usertype_gid='" + employee_gid+"'";
            string lsprimary_address = objdbconn.GetExecuteScalar(msSQL);
            if (lsprimary_address == (values.primary_address))
            {
                values.status = false;
                values.message = "Already Primary Address added";
                return false;
            }
            msSQL = "select customer2address_gid from ocs_mst_tcustomer2address where address_type='" + values.address_type + "' and customer2usertype_gid='" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Already Address Type added";
                return false;
            }

            msGetGid = objcmnfunctions.GetMasterGID("CU2A");
            msSQL = " insert into ocs_mst_tcustomer2address(" +
                    " customer2address_gid," +
                    " customer2usertype_gid," +
                    " address_type,"+
                    " addressline1," +
                    " addressline2," +
                    " city," +
                    " postal_code," +
                    " taluka," +
                    " district," +
                    " state," +
                    " state_gid,"+
                    " country," +
                    " primary_address," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                     "'" + values.address_type + "'," +
                    "'" + values.addressline1 + "'," +
                    "'" + values.addressline2 + "'," +
                    "'" + values.city + "'," +
                    "'" + values.postal_code + "'," +
                    "'" + values.taluka + "'," +
                    "'" + values.district + "'," +
                    "'" + values.state + "'," +
                    "'" + values.state_gid + "'," +
                    "'" + values.country + "'," +
                     "'" + values.primary_address + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                values.status = true;
                values.message = "Address details added sucessfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured while adding Address";
                return false;
            }

        }

        //----------- Submit ID Proof----------//
        public bool Dapostidproof(string employee_gid, MdlID_proof values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("CU2I");
            msSQL = " insert into ocs_mst_tcustomer2identityproof(" +
                    " customer2identityproof_gid," +
                    " customer2usertype_gid," +
                    " idproof_type," +
                    " idproof_number," +              
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + values.idproof_type + "'," +
                    "'" + values.idproof_no + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                values.status = true;
                values.message = "Identity Proof added sucessfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured while adding Identity Proof";
                return false;
            }
        }

        //----------- Submit Mobile No----------//
        public bool Dapostmobileno(string employee_gid, Mdlmobile_no values)
        {
            msSQL = "select primary_mobileno from ocs_mst_tcustomer2mobileno where primary_mobileno='Yes' and customer2usertype_gid='" + employee_gid + "'";
      string lsprimary_mobileno = objdbconn.GetExecuteScalar(msSQL);
            if (lsprimary_mobileno == (values.primary_mobileno))
            {
             
                values.status = false;
                values.message = "Already Primary Mobile No added";
                return false;
            }
            msSQL = "select customer2mobileno_gid from ocs_mst_tcustomer2mobileno where mobile_no='"+ values.mobile_no + "' and customer2usertype_gid='" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Already This Mobile No added";
                return false;
            }
            msGetGid = objcmnfunctions.GetMasterGID("CUML");
            msSQL = " insert into ocs_mst_tcustomer2mobileno(" +
                    " customer2mobileno_gid," +
                    " customer2usertype_gid," +
                    " mobile_no," +
                    " primary_mobileno,"+
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + values.mobile_no + "'," +
                    "'" + values.primary_mobileno + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                values.status = true;
                values.message = "Mobile No added sucessfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured while adding Mobile No";
                return false;
            }
        }
        //----------- Submit Member----------//
        public bool Dapostmember(string employee_gid, MdlMember values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("CMEM");
            msSQL = " insert into ocs_mst_tcustomer2member(" +
                    " customer2member_gid," +
                    " customer2usertype_gid," +
                    " member_name," +
                    " member_designation," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + values.member_name + "'," +
                    "'" + values.member_designation + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                values.status = true;
                values.message = "Member added sucessfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured while adding Member";
                return false;
            }
        }
        public void DaGetMobileNo(string employee_gid, Mdlmobile_no values)
            {
              msSQL = "select mobile_no,customer2mobileno_gid,primary_mobileno from ocs_mst_tcustomer2mobileno where " +
                " customer2usertype_gid='" + employee_gid + "'";
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
                        customer2mobileno_gid = (dr_datarow["customer2mobileno_gid"].ToString()),
                    });
                }
            values.mobileno_list = getmobileno_list;
            }
            dt_datatable.Dispose();
            }
        public void DaGetAddress(string employee_gid, MdlMstaddresstype values)
        {
            msSQL = "select address_type,addressline1,addressline2, city,state,taluka,country,district,postal_code,customer2address_gid,primary_address"+
                " from ocs_mst_tcustomer2address where " +
              " customer2usertype_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getaddress_list = new List<address_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getaddress_list.Add(new address_list
                    {
                        primary_address = (dr_datarow["primary_address"].ToString()),
                        address_type = (dr_datarow["address_type"].ToString()),
                        addressline1 = (dr_datarow["addressline1"].ToString()),
                        addressline2 = (dr_datarow["addressline2"].ToString()),
                        city = (dr_datarow["city"].ToString()),
                        state = (dr_datarow["state"].ToString()),
                        taluka = (dr_datarow["taluka"].ToString()),
                        district = (dr_datarow["district"].ToString()),
                        postal_code = (dr_datarow["postal_code"].ToString()),
                        country = (dr_datarow["country"].ToString()),
                        customer2address_gid = (dr_datarow["customer2address_gid"].ToString()),
                    });
                }
                values.address_list = getaddress_list;
            }
            dt_datatable.Dispose();
        }

        public void DaGetidproofList(string employee_gid, MdlID_proof values)
        {
            msSQL = "select customer2identityproof_gid,idproof_type,idproof_number from ocs_mst_tcustomer2identityproof where " +
              " customer2usertype_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getidprooflist = new List<idproof_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getidprooflist.Add(new idproof_list
                    {
                       idproof_type = (dr_datarow["idproof_type"].ToString()),
                       idproof_no = (dr_datarow["idproof_number"].ToString()),
                       customer2identityproof_gid = (dr_datarow["customer2identityproof_gid"].ToString()),
                    });
                }
                values.idproof_list = getidprooflist;
            }
            dt_datatable.Dispose();

            
        }
        public void DaGetMemberList(string employee_gid, MdlMember values)
        {
            msSQL = "select customer2member_gid,member_name,member_designation from ocs_mst_tcustomer2member where " +
              " customer2usertype_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmember_list = new List<member_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmember_list.Add(new member_list
                    {
                        member_designation = (dr_datarow["member_designation"].ToString()),
                        member_name = (dr_datarow["member_name"].ToString()),
                        customer2member_gid = (dr_datarow["customer2member_gid"].ToString()),
                    });
                }
                values.member_list = getmember_list;
            }
            dt_datatable.Dispose();
        }
        //----------Upload Photo------//
        public bool DaPostUploadPhoto(HttpRequest httpRequest, UploadDocumentname objfilename, string employee_gid, string user_gid)
        {
           // UploadDocumentList objdocumentmodel = new UploadDocumentList();
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


            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";

            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "Master/photos/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
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
            if ((FileExtension == ".jpg") || (FileExtension == ".jpeg") || (FileExtension == ".png") || (FileExtension == ".tif") || (FileExtension == ".tiff") || (FileExtension == ".jfif") || (FileExtension == ".gif"))
            {
                ls_readStream = httpPostedFile.InputStream;
                ls_readStream.CopyTo(ms);
                lspath = path;
                objcmnfunctions.uploadFile(lspath, lsfile_gid);
                lspath = "../../erp_documents" + "/" + lscompany_code + "/" + "Master/photos/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension;

                msGetGid = objcmnfunctions.GetMasterGID("UPPD");
                msSQL = " insert into ocs_tmp_tphotoupload( " +
                             " photo_name," +
                             " photo_path, " +
                             " created_by ," +
                             " created_date " +
                             " )values(" +
                             "'" + httpPostedFile.FileName.Replace("'", " ") + "'," +
                             "'" + lspath.Replace("'", " ") + "'," +
                             "'" + user_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else
            {
                objfilename.status = false;
                objfilename.message = "File format is not supported";
                return false;
            }
            if (mnResult != 0)
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
        //---------Submit - individual Information--------//
        public void DaPostIndividualSubmit(string employee_gid, mdlcustomer2userdtl values)
        {
            msSQL = "select customer2mobileno_gid from ocs_mst_tcustomer2mobileno where customer2usertype_gid='" + employee_gid + "' and primary_mobileno='Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Kindly Add Mobile No with Primary Status";
                return;
            }
            msSQL = "select customer2address_gid from ocs_mst_tcustomer2address where customer2usertype_gid='" + employee_gid + "' and primary_address='Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Kindly Add Address with Primary Status";
                return;
            }
            msSQL = "select user_type from ocs_mst_tcustomer2userdtl where customer_gid='" + employee_gid + "' and user_type='Applicant'";
            string lsuser_type = objdbconn.GetExecuteScalar(msSQL);

           if(lsuser_type== values.user_type)
            {
               
                values.status = false;
                values.message = "Applicant Information already added";
                return;
            }
            msSQL = "select customer2usertype_gid from ocs_mst_tcustomer2userdtl where aadhar_no='" + values.aadhar_no + "' and user_type='" + values.user_type + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == false)
            {

            msGetGid = objcmnfunctions.GetMasterGID("C2UD");
            msSQL = " insert into ocs_mst_tcustomer2userdtl(" +
                    " customer2usertype_gid ," +
                    " customer_gid," +
                    " customer2user_name," +
                    " customer2user_dob," +
                    " customer2user_age," +
                    " customer2user_gender," +
                    " personalemail_address ," +
                    " officialemail_address," +
                    " telephone_no," +
                    " contact_person," +
                    " contactperson_designation," +
                    " aadhar_no ," +
                    " pan_no," +
                    " user_type," +
                    " usertype_gid," +
                    " customer_type ," +
                    " guarantor_id ," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + values.name + "',";
            if ((values.dob == null)|| (values.dob == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.dob).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }

           
                 msSQL+= "'" + values.age + "'," +
                    "'" + values.gender + "'," +
                    "'" + values.personalemail_address + "'," +
                    "'" + values.officailemail_address + "'," +
                    "'" + values.telephone_no + "'," +
                    "'" + values.contact_person + "'," +
                    "'" + values.contactperson_designation + "'," +
                    "'" + values.aadhar_no + "'," +
                    "'" + values.pan_no + "'," +
                    "'" + values.user_type + "'," +
                    "'" + values.usertype_gid + "'," +
                    "'Individual'," +
                     "'" + values.guarantor_id + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            }
            else
            {
                msGetGid = objcmnfunctions.GetMasterGID("C2UD");
                msSQL = " insert into ocs_mst_tcustomer2userdtl(" +
                        " customer2usertype_gid ," +
                        " customer_gid," +
                        " customer2user_name," +
                        " customer2user_dob," +
                        " customer2user_age," +
                        " customer2user_gender," +
                        " personalemail_address ," +
                        " officialemail_address," +
                        " telephone_no," +
                        " contact_person," +
                        " contactperson_designation," +
                        " aadhar_no ," +
                        " pan_no," +
                        " user_type," +
                        " usertype_gid," +
                        " customer_type ," +
                        " guarantor_id ," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + employee_gid + "'," +
                        "'" + values.name + "',";
                if ((values.dob == null) || (values.dob == ""))
                {
                    msSQL += "null,";
                }
                else
                {
                    msSQL += "'" + Convert.ToDateTime(values.dob).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                }


                msSQL += "'" + values.age + "'," +
                   "'" + values.gender + "'," +
                   "'" + values.personalemail_address + "'," +
                   "'" + values.officailemail_address + "'," +
                   "'" + values.telephone_no + "'," +
                   "'" + values.contact_person + "'," +
                   "'" + values.contactperson_designation + "'," +
                   "'" + values.aadhar_no + "'," +
                   "'" + values.pan_no + "'," +
                   "'" + values.user_type + "'," +
                   "'" + values.usertype_gid + "'," +
                   "'Individual'," +
                    "'" + values.guarantor_id + "'," +
                   "'" + employee_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update ocs_mst_tcustomer2userdtl set " +
                     " customer2user_name='" + values.name + "',";
                if ((values.dob == null) || (values.dob == ""))
                {
                    msSQL += "customer2user_dob=null,";
                }
                else
                {
                    msSQL += "customer2user_dob= '" + Convert.ToDateTime(values.dob).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                }
                msSQL += " customer2user_age='" + values.age + "'," +
                        " customer2user_gender='" + values.gender + "'," +
                        " personalemail_address ='" + values.personalemail_address + "'," +
                        " officialemail_address='" + values.officailemail_address + "'," +
                        " telephone_no='" + values.telephone_no + "'," +
                        " contact_person='" + values.contact_person + "'," +
                        " contactperson_designation='" + values.contactperson_designation + "'," +
                        " aadhar_no='" + values.aadhar_no + "'," +
                        " pan_no='" + values.pan_no + "'," +
                        " user_type='" + values.user_type + "'," +
                        " usertype_gid='" + values.usertype_gid + "'," +
                        " guarantor_id='" + values.guarantor_id + "'," +
                        " customer_type ='Individual'," +
                        " updated_by='" + employee_gid + "'," +
                        " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where aadhar_no='" + values.aadhar_no + "'";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            objODBCDatareader.Close();
            if (mnResult != 0)
            {
                //--------Mobile No updation--//
                msSQL = "update ocs_mst_tcustomer2mobileno set customer2usertype_gid ='" + msGetGid + "' where customer2usertype_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                //---------Address updation------//
                msSQL = "update ocs_mst_tcustomer2address set customer2usertype_gid ='" + msGetGid + "' where customer2usertype_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                //---------ID Proof updation------//
                msSQL = "update ocs_mst_tcustomer2identityproof set customer2usertype_gid ='" + msGetGid + "' where customer2usertype_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                //---------Photo updation------//

                msSQL = "select photo_name,photo_path from ocs_tmp_tphotoupload where created_by='" + employee_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if(objODBCDatareader.HasRows==true)
                {
                    msSQL = "update ocs_mst_tcustomer2userdtl set photo_name ='" + objODBCDatareader["photo_name"].ToString() + "',"+
                        " photo_path='"+ objcmnstorage.EncryptData(objODBCDatareader["photo_path"].ToString()) + "' where customer2usertype_gid='" + msGetGid + "'";
                    objODBCDatareader.Close();
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                values.status = true;
                values.message = "Individual Information added sucessfully";
               
            }
            else
            {
                values.status = true;
                values.message = "Error Occured while adding Individual";
               
            }
        }
        //---------Submit - individual Information--------//
        public void DaEditIndividualSubmit(string employee_gid, mdlcustomer2userdtl values)
        {
            msSQL = "select customer2mobileno_gid from ocs_mst_tcustomer2mobileno where customer2usertype_gid='" + employee_gid + "' and primary_mobileno='Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Kindly Add Mobile No with Primary Status";
                return;
            }
            msSQL = "select customer2address_gid from ocs_mst_tcustomer2address where customer2usertype_gid='" + employee_gid + "' and primary_address='Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Kindly Add Address with Primary Status";
                return;
            }
            msSQL = "select user_type from ocs_mst_tcustomer2userdtl where (customer_gid='" + employee_gid + "'or customer_gid='" + values.customer_gid + "')"+
                " and user_type='Applicant'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Applicant Information already added";
                return;
            }
            msSQL = "select customer2usertype_gid from ocs_mst_tcustomer2userdtl where aadhar_no='" + values.aadhar_no + "' and user_type='" + values.user_type + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == false)
            {
                msGetGid = objcmnfunctions.GetMasterGID("C2UD");
            msSQL = " insert into ocs_mst_tcustomer2userdtl(" +
                    " customer2usertype_gid ," +
                    " customer_gid," +
                    " customer2user_name," +
                    " customer2user_dob," +
                    " customer2user_age," +
                    " customer2user_gender," +
                    " personalemail_address ," +
                    " officialemail_address," +
                    " telephone_no," +
                    " contact_person," +
                    " contactperson_designation," +
                    " aadhar_no ," +
                    " pan_no," +
                    " user_type," +
                    " usertype_gid," +
                    " customer_type ," +
                    " guarantor_id ," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + values.name + "',";
            if ((values.dob == null) || (values.dob == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.dob).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }


            msSQL += "'" + values.age + "'," +
               "'" + values.gender + "'," +
               "'" + values.personalemail_address + "'," +
               "'" + values.officailemail_address + "'," +
               "'" + values.telephone_no + "'," +
               "'" + values.contact_person + "'," +
               "'" + values.contactperson_designation + "'," +
               "'" + values.aadhar_no + "'," +
               "'" + values.pan_no + "'," +
               "'" + values.user_type + "'," +
               "'" + values.usertype_gid + "'," +
               "'Individual'," +
               "'" + values.guarantor_id + "'," +
               "'" + employee_gid + "'," +
               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
        }
            else
            {
                msGetGid = objcmnfunctions.GetMasterGID("C2UD");
                msSQL = " insert into ocs_mst_tcustomer2userdtl(" +
                        " customer2usertype_gid ," +
                        " customer_gid," +
                        " customer2user_name," +
                        " customer2user_dob," +
                        " customer2user_age," +
                        " customer2user_gender," +
                        " personalemail_address ," +
                        " officialemail_address," +
                        " telephone_no," +
                        " contact_person," +
                        " contactperson_designation," +
                        " aadhar_no ," +
                        " pan_no," +
                        " user_type," +
                        " usertype_gid," +
                        " customer_type ," +
                        " guarantor_id ," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + employee_gid + "'," +
                        "'" + values.name + "',";
                if ((values.dob == null) || (values.dob == ""))
                {
                    msSQL += "null,";
                }
                else
                {
                    msSQL += "'" + Convert.ToDateTime(values.dob).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                }


                msSQL += "'" + values.age + "'," +
                   "'" + values.gender + "'," +
                   "'" + values.personalemail_address + "'," +
                   "'" + values.officailemail_address + "'," +
                   "'" + values.telephone_no + "'," +
                   "'" + values.contact_person + "'," +
                   "'" + values.contactperson_designation + "'," +
                   "'" + values.aadhar_no + "'," +
                   "'" + values.pan_no + "'," +
                   "'" + values.user_type + "'," +
                   "'" + values.usertype_gid + "'," +
                   "'Individual'," +
                   "'" + values.guarantor_id + "'," +
                   "'" + employee_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                msSQL = " update ocs_mst_tcustomer2userdtl set " +
                     " customer2user_name='" + values.name + "',";
                if ((values.dob == null) || (values.dob == ""))
                {
                    msSQL += "customer2user_dob=null,";
                }
                else
                {
                    msSQL += "customer2user_dob= '" + Convert.ToDateTime(values.dob).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                }
               msSQL += " customer2user_age='" + values.age + "'," +
                        " customer2user_gender='" + values.gender + "'," +
                        " personalemail_address ='" + values.personalemail_address + "'," +
                        " officialemail_address='" + values.officailemail_address + "'," +
                        " telephone_no='" + values.telephone_no + "'," +
                        " contact_person='" + values.contact_person + "'," +
                        " contactperson_designation='" + values.contactperson_designation + "'," +
                        " aadhar_no='" + values.aadhar_no + "'," +
                        " pan_no='" + values.pan_no + "'," +
                        " user_type='" + values.user_type + "'," +
                        " usertype_gid='" + values.usertype_gid + "'," +
                        " customer_type ='Individual'," +
                        " guarantor_id='" + values.guarantor_id + "'," +
                        " updated_by='" + employee_gid + "'," +
                        " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where aadhar_no='" + values.aadhar_no + "'";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            objODBCDatareader.Close();
            if (mnResult != 0)
            {
                //--------Mobile No updation--//
                msSQL = "update ocs_mst_tcustomer2mobileno set customer2usertype_gid ='" + msGetGid + "' where customer2usertype_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                //---------Address updation------//
                msSQL = "update ocs_mst_tcustomer2address set customer2usertype_gid ='" + msGetGid + "' where customer2usertype_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                //---------ID Proof updation------//
                msSQL = "update ocs_mst_tcustomer2identityproof set customer2usertype_gid ='" + msGetGid + "' where customer2usertype_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                //---------Photo updation------//

                msSQL = "select photo_name,photo_path from ocs_tmp_tphotoupload where created_by='" + employee_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    msSQL = "update ocs_mst_tcustomer2userdtl set photo_name ='" + objODBCDatareader["photo_name"].ToString() + "'," +
                        " photo_path='" + objcmnstorage.EncryptData(objODBCDatareader["photo_path"].ToString()) + "' where customer2usertype_gid='" + msGetGid + "'";
                    objODBCDatareader.Close();
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                values.status = true;
                values.message = "Individual Information added sucessfully";

            }
            else
            {
                values.status = true;
                values.message = "Error Occured while adding Individual";

            }
        }
        //---------Submit - institution Information--------//
        public void DaPostInstitutionSubmit(string employee_gid, mdlcustomer2userdtl values)
        {
            msSQL = "select customer2mobileno_gid from ocs_mst_tcustomer2mobileno where customer2usertype_gid='" + employee_gid + "' and primary_mobileno='Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Kindly Add Mobile No with Primary Status";
                return;
            }
            msSQL = "select customer2address_gid from ocs_mst_tcustomer2address where customer2usertype_gid='" + employee_gid + "' and primary_address='Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Kindly Add Address with Primary Status";
                return;
            }
            msSQL = "select user_type from ocs_mst_tcustomer2userdtl where customer_gid='" + employee_gid + "' and user_type='Applicant'";
            string lsuser_type = objdbconn.GetExecuteScalar(msSQL);

            if (lsuser_type == values.user_type)
            {

                values.status = false;
                values.message = "Applicant Information already added";
                return;
            }
            msSQL = "select customer2usertype_gid from ocs_mst_tcustomer2userdtl where gst_no='" + values.gst_no + "' and user_type='" + values.user_type + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == false)
            {
                msGetGid = objcmnfunctions.GetMasterGID("C2UD");
                msSQL = " insert into ocs_mst_tcustomer2userdtl(" +
                        " customer2usertype_gid ," +
                        " customer_gid," +
                        " customer2user_name," +
                        " cin_date," +
                        " personalemail_address ," +
                        " telephone_no," +
                        " contact_person," +
                        " contactperson_designation," +
                        " company_type," +
                        " year_business ," +
                        " month_business," +
                        " landmark," +
                        " credit_rating," +
                        " escrow," +
                        " cin_no," +
                        " pan_no," +
                        " gst_no," +
                        " user_type," +
                        " usertype_gid," +
                        " customer_type ," +
                         " guarantor_id ," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + employee_gid + "'," +
                        "'" + values.name + "',";
                if (values.cin_date == null)
                {
                    msSQL += "null,";
                }
                else
                {
                    msSQL += "'" + Convert.ToDateTime(values.cin_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                }
                msSQL += "'" + values.personalemail_address + "'," +
                   "'" + values.telephone_no + "'," +
                   "'" + values.contact_person + "'," +
                   "'" + values.contactperson_designation + "'," +
                   "'" + values.company_type + "'," +
                   "'" + values.year_business + "'," +
                   "'" + values.month_business + "'," +
                   "'" + values.landmark + "'," +
                   "'" + values.credit_rating + "'," +
                   "'" + values.escrow + "'," +
                   "'" + values.cin_no + "'," +
                   "'" + values.pan_no + "'," +
                   "'" + values.gst_no + "'," +
                   "'" + values.user_type + "'," +
                   "'" + values.usertype_gid + "'," +
                   "'Institution'," +
                   "'" + values.guarantor_id + "'," +
                   "'" + employee_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else {
                msGetGid = objcmnfunctions.GetMasterGID("C2UD");
                msSQL = " insert into ocs_mst_tcustomer2userdtl(" +
                        " customer2usertype_gid ," +
                        " customer_gid," +
                        " customer2user_name," +
                        " cin_date," +
                        " personalemail_address ," +
                        " telephone_no," +
                        " contact_person," +
                        " contactperson_designation," +
                        " company_type," +
                        " year_business ," +
                        " month_business," +
                        " landmark," +
                        " credit_rating," +
                        " escrow," +
                        " cin_no," +
                        " pan_no," +
                        " gst_no," +
                        " user_type," +
                        " usertype_gid," +
                        " customer_type ," +
                         " guarantor_id ," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + employee_gid + "'," +
                        "'" + values.name + "',";
                if (values.cin_date == null)
                {
                    msSQL += "null,";
                }
                else
                {
                    msSQL += "'" + Convert.ToDateTime(values.cin_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                }
                msSQL += "'" + values.personalemail_address + "'," +
                   "'" + values.telephone_no + "'," +
                   "'" + values.contact_person + "'," +
                   "'" + values.contactperson_designation + "'," +
                   "'" + values.company_type + "'," +
                   "'" + values.year_business + "'," +
                   "'" + values.month_business + "'," +
                   "'" + values.landmark + "'," +
                   "'" + values.credit_rating + "'," +
                   "'" + values.escrow + "'," +
                   "'" + values.cin_no + "'," +
                   "'" + values.pan_no + "'," +
                   "'" + values.gst_no + "'," +
                   "'" + values.user_type + "'," +
                   "'" + values.usertype_gid + "'," +
                   "'Institution'," +
                   "'" + values.guarantor_id + "'," +
                   "'" + employee_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update ocs_mst_tcustomer2userdtl set" +
                    " customer2user_name='" + values.name + "',";
            if (values.cin_date == null)
            {
                msSQL += "cin_date=null,";
            }
            else
            {
                msSQL += "cin_date='" + Convert.ToDateTime(values.cin_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            msSQL += " personalemail_address='" + values.personalemail_address + "'," +
                     " telephone_no='" + values.telephone_no + "'," +
                     " contact_person='" + values.contact_person + "'," +
                     " contactperson_designation='" + values.contactperson_designation + "'," +
                     " company_type='" + values.company_type + "'," +
                     " year_business ='" + values.year_business + "'," +
                     " month_business='" + values.month_business + "'," +
                     " landmark='" + values.landmark + "'," +
                     " credit_rating='" + values.credit_rating + "'," +
                     " escrow='" + values.escrow + "'," +
                     " cin_no='" + values.cin_no + "'," +
                     " pan_no='" + values.pan_no + "'," +
                     " gst_no='" + values.gst_no + "'," +
                     " user_type='" + values.user_type + "'," +
                     " usertype_gid='" + values.usertype_gid + "'," +
                     " customer_type ='Institution'," +
                     " guarantor_id='" + values.guarantor_id + "'," +
                     " updated_by='" + employee_gid + "'," +
                     " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where gst_no='" + values.gst_no + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            objODBCDatareader.Close();
            if (mnResult != 0)
            {
                //--------Mobile No updation--//
                msSQL = "update ocs_mst_tcustomer2mobileno set customer2usertype_gid ='" + msGetGid + "' where customer2usertype_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                //---------Address updation------//
                msSQL = "update ocs_mst_tcustomer2address set customer2usertype_gid ='" + msGetGid + "' where customer2usertype_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                //---------ID Proof updation------//
                msSQL = "update ocs_mst_tcustomer2identityproof set customer2usertype_gid ='" + msGetGid + "' where customer2usertype_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                //-----------Member Updation-------//
                msSQL = "update ocs_mst_tcustomer2member set customer2usertype_gid ='" + msGetGid + "' where customer2usertype_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Individual Information added sucessfully";

            }
            else
            {
                values.status = true;
                values.message = "Error Occured while adding Individual";

            }
        }
        //---------Submit - institution Information--------//
        public void DaEditInstitutionSubmit(string employee_gid, mdlcustomer2userdtl values)
        {
            msSQL = "select customer2mobileno_gid from ocs_mst_tcustomer2mobileno where customer2usertype_gid='" + employee_gid + "' and primary_mobileno='Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Kindly Add Mobile No with Primary Status";
                return;
            }
            msSQL = "select customer2address_gid from ocs_mst_tcustomer2address where customer2usertype_gid='" + employee_gid + "' and primary_address='Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Kindly Add Address with Primary Status";
                return;
            }
            msSQL = "select user_type from ocs_mst_tcustomer2userdtl where ( customer_gid='" + employee_gid + "' or customer_gid='" + values.customer_gid + "')"+
                " and user_type='Applicant'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Applicant Information already added";
                return;
            }
            msSQL = "select customer2usertype_gid from ocs_mst_tcustomer2userdtl where gst_no='" + values.gst_no + "' and user_type='" + values.user_type + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == false)
            {
                msGetGid = objcmnfunctions.GetMasterGID("C2UD");
                msSQL = " insert into ocs_mst_tcustomer2userdtl(" +
                        " customer2usertype_gid ," +
                        " customer_gid," +
                        " customer2user_name," +
                        " cin_date," +
                        " personalemail_address ," +
                        " telephone_no," +
                        " contact_person," +
                        " contactperson_designation," +
                        " company_type," +
                        " year_business ," +
                        " month_business," +
                        " landmark," +
                        " credit_rating," +
                        " escrow," +
                        " cin_no," +
                        " pan_no," +
                        " gst_no," +
                        " user_type," +
                        " usertype_gid," +
                        " customer_type ," +
                        " guarantor_id," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + employee_gid + "'," +
                        "'" + values.name + "',";
                if (values.cin_date == null)
                {
                    msSQL += "null,";
                }
                else
                {
                    msSQL += "'" + Convert.ToDateTime(values.cin_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                }
                msSQL += "'" + values.personalemail_address + "'," +
                   "'" + values.telephone_no + "'," +
                   "'" + values.contact_person + "'," +
                   "'" + values.contactperson_designation + "'," +
                   "'" + values.company_type + "'," +
                   "'" + values.year_business + "'," +
                   "'" + values.month_business + "'," +
                   "'" + values.landmark + "'," +
                   "'" + values.credit_rating + "'," +
                   "'" + values.escrow + "'," +
                   "'" + values.cin_no + "'," +
                   "'" + values.pan_no + "'," +
                   "'" + values.gst_no + "'," +
                    "'" + values.user_type + "'," +
                     "'" + values.usertype_gid + "'," +
                   "'Institution'," +
                   "'" + values.guarantor_id + "'," +
                   "'" + employee_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else
            {
                msGetGid = objcmnfunctions.GetMasterGID("C2UD");
                msSQL = " insert into ocs_mst_tcustomer2userdtl(" +
                        " customer2usertype_gid ," +
                        " customer_gid," +
                        " customer2user_name," +
                        " cin_date," +
                        " personalemail_address ," +
                        " telephone_no," +
                        " contact_person," +
                        " contactperson_designation," +
                        " company_type," +
                        " year_business ," +
                        " month_business," +
                        " landmark," +
                        " credit_rating," +
                        " escrow," +
                        " cin_no," +
                        " pan_no," +
                        " gst_no," +
                        " user_type," +
                        " usertype_gid," +
                        " customer_type ," +
                         " guarantor_id ," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + employee_gid + "'," +
                        "'" + values.name + "',";
                if (values.cin_date == null)
                {
                    msSQL += "null,";
                }
                else
                {
                    msSQL += "'" + Convert.ToDateTime(values.cin_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                }
                msSQL += "'" + values.personalemail_address + "'," +
                   "'" + values.telephone_no + "'," +
                   "'" + values.contact_person + "'," +
                   "'" + values.contactperson_designation + "'," +
                   "'" + values.company_type + "'," +
                   "'" + values.year_business + "'," +
                   "'" + values.month_business + "'," +
                   "'" + values.landmark + "'," +
                   "'" + values.credit_rating + "'," +
                   "'" + values.escrow + "'," +
                   "'" + values.cin_no + "'," +
                   "'" + values.pan_no + "'," +
                   "'" + values.gst_no + "'," +
                   "'" + values.user_type + "'," +
                   "'" + values.usertype_gid + "'," +
                   "'Institution'," +
                   "'" + values.guarantor_id + "'," +
                   "'" + employee_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                msSQL = " update ocs_mst_tcustomer2userdtl set" +
                        " customer2user_name='" + values.name + "',";
                if (values.cin_date == null)
                {
                    msSQL += "cin_date=null,";
                }
                else
                {
                    msSQL += "cin_date='" + Convert.ToDateTime(values.cin_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                }
                msSQL += " personalemail_address='" + values.personalemail_address + "'," +
                         " telephone_no='" + values.telephone_no + "'," +
                         " contact_person='" + values.contact_person + "'," +
                         " contactperson_designation='" + values.contactperson_designation + "'," +
                         " company_type='" + values.company_type + "'," +
                         " year_business ='" + values.year_business + "'," +
                         " month_business='" + values.month_business + "'," +
                         " landmark='" + values.landmark + "'," +
                         " credit_rating='" + values.credit_rating + "'," +
                         " escrow='" + values.escrow + "'," +
                         " cin_no='" + values.cin_no + "'," +
                         " pan_no='" + values.pan_no + "'," +
                         " gst_no='" + values.gst_no + "'," +
                         " user_type='" + values.user_type + "'," +
                         " usertype_gid='" + values.usertype_gid + "'," +
                         " customer_type ='Institution'," +
                         " guarantor_id='" + values.guarantor_id + "'," +
                         " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where gst_no='" + values.gst_no + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            objODBCDatareader.Close();
            if (mnResult != 0)
            {
                //--------Mobile No updation--//
                msSQL = "update ocs_mst_tcustomer2mobileno set customer2usertype_gid ='" + msGetGid + "' where customer2usertype_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                //---------Address updation------//
                msSQL = "update ocs_mst_tcustomer2address set customer2usertype_gid ='" + msGetGid + "' where customer2usertype_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                //---------ID Proof updation------//
                msSQL = "update ocs_mst_tcustomer2identityproof set customer2usertype_gid ='" + msGetGid + "' where customer2usertype_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                //-----------Member Updation-------//
                msSQL = "update ocs_mst_tcustomer2member set customer2usertype_gid ='" + msGetGid + "' where customer2usertype_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Individual Information added sucessfully";

            }
            else
            {
                values.status = true;
                values.message = "Error Occured while adding Individual";

            }
        }
        //----------Get Customer 2 User Information------------//
        public void DaGetCustomer2UserDtl(string employee_gid, mdlcustomer2userdtl values)
        {
            msSQL = " select a.customer2user_name,date_format(a.created_date,'%d-%m-%Y') as created_date,user_type,customer2usertype_gid," +
                    " concat(c.user_firstname, ' ', c.user_lastname, '/', c.user_code) as created_by,customer_type from ocs_mst_tcustomer2userdtl a" +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                    " left  join adm_mst_tuser c on b.user_gid = c.user_gid where " +
                    " customer_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcustomer2userdtl_list =new List<customer2userdtl_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcustomer2userdtl_list.Add(new customer2userdtl_list
                    {
                        name = (dr_datarow["customer2user_name"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        user_type = (dr_datarow["user_type"].ToString()),
                        customer2usertype_gid = (dr_datarow["customer2usertype_gid"].ToString()),
                        customer_type = (dr_datarow["customer_type"].ToString()),
                    });
                }
                values.customer2userdtl_list = getcustomer2userdtl_list;
            }
            dt_datatable.Dispose();
        }

        //---------Submit - Customer Information--------//
        public void DaPostCustomerSubmit(string employee_gid, mdlcreatecustomer values)
        {
            
            if (values.customer_urn == null||values.customer_urn=="")
            {
               
            }
            else
            {
                msSQL = "select customer_gid from ocs_mst_tcustomer where customer_urn='" + values.customer_urn + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    objODBCDatareader.Close();
                    values.status = false;
                    values.message = "Customer URN Already Exists";
                    return;
                }
                objODBCDatareader.Close();
            }
            msGetGid = objcmnfunctions.GetMasterGID("CRMS");
            msGetGidREF = objcmnfunctions.GetMasterGID("CC");
            msSQL = "select customer2user_name ,customer2usertype_gid,gst_no,pan_no,personalemail_address,contact_person,customer_type "+
                " from ocs_mst_tcustomer2userdtl where customer_gid='" + employee_gid + "' and user_type='Applicant'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lscustomer_name = objODBCDatareader["customer2user_name"].ToString();
                lsgst_no = objODBCDatareader["gst_no"].ToString();
                lspan_no = objODBCDatareader["pan_no"].ToString();
                lspersonalemail_address = objODBCDatareader["personalemail_address"].ToString();            
                lscontact_person = objODBCDatareader["contact_person"].ToString();
                lscustomer2userdtl_gid = objODBCDatareader["customer2usertype_gid"].ToString();
                lscustomer_type = objODBCDatareader["customer_type"].ToString();
                objODBCDatareader.Close();
            }
            else
            {
                values.message = "Kindly Add Applicant";
                values.status = false;
                objODBCDatareader.Close();
            }

           
            msSQL = "select mobile_no from ocs_mst_tcustomer2mobileno where customer2usertype_gid='" + lscustomer2userdtl_gid + "' and primary_mobileno='Yes'";
            lsmobile_no = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select addressline1,addressline2,state,state_gid,taluka,district,city,postal_code,country from ocs_mst_tcustomer2address where customer2usertype_gid='" + lscustomer2userdtl_gid + "' and primary_address='Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if(objODBCDatareader.HasRows==true)
            {

                msSQL = " insert into ocs_mst_tcustomer(" +
                        " customer_gid," +
                        " customer_code," +
                        " customername," +
                        " contactperson," +
                        " customer_urn," +
                        " gst_number," +
                        " pan_number," +
                        " mobileno," +
                        " email," +
                        " address," +
                        " address2," +
                        " state," +
                        " state_gid," +
                        " taluka," +
                        " district," +
                        " city," +
                        " vertical_gid," +
                        " vertical_code," +
                        " postalcode," +
                        " country," +
                        " constitution_name," +
                        " constitution_gid," +
                        " SA_status," +
                        " sa_id_gid," +
                        " sa_idname," +
                        " sa_payout," +
                        " zonal_head," +
                        " business_head," +
                        " relationship_manager," +
                        " cluster_manager_gid," +
                        " creditmanager_gid," +
                        " zonal_name," +
                        " businesshead_name," +
                        " relationshipmgmt_name," +
                        " creditmgmt_name," +
                        " cluster_manager_name," +
                        " businessunit_name," +
                        " businessunit_gid," +
                        " customer_type," +
                        " zonal_riskmanager," +
                        " zonal_riskmanagerName," +
                        " assigned_RM," +
                        " assigned_RMName," +
                        " riskMonitoring_GID," +
                        " riskMonitoring_Name," +
                        " major_corporate," +
                        " ccmail_text," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + msGetGidREF + "'," +
                        "'" + lscustomer_name + "'," +
                         "'" + lscontact_person + "'," +
                         "'" + values.customer_urn + "'," +
                         "'" + lsgst_no + "'," +
                         "'" + lspan_no + "'," +
                         "'" + lsmobile_no + "'," +
                          "'" + lspersonalemail_address + "'," +
                         "'" + objODBCDatareader["addressline1"].ToString() + "'," +
                         "'" + objODBCDatareader["addressline2"].ToString() + "'," +
                         "'" + objODBCDatareader["state"].ToString() + "'," +
                         "'" + objODBCDatareader["state_gid"].ToString() + "'," +
                         "'" + objODBCDatareader["taluka"].ToString() + "'," +
                         "'" + objODBCDatareader["district"].ToString() + "'," +
                         "'" + objODBCDatareader["city"].ToString() + "'," +
                         "'" + values.vertical_gid + "'," +
                         "'" + values.vertical_code.Replace("'", "").Replace("\n", "") + "'," +
                         "'" + objODBCDatareader["postal_code"].ToString() + "'," +
                         "'" + objODBCDatareader["country"].ToString() + "'," +
                         "'" + values.constitution_name.Replace("   ", "").Replace("\n", "") + "'," +
                         "'" + values.constitution_gid + "'," +
                         "'" + values.sa_status + "'," +
                         "'" + values.sa_id_gid + "'," +
                         "'" + values.sa_idname + "'," +
                         "'" + values.sa_payout + "'," +
                         "'" + values.zonalGid + "'," +
                         "'" + values.businessHeadGid + "'," +
                         "'" + values.relationshipMgmtGid + "'," +
                         "'" + values.clustermanagerGid + "'," +
                         "'" + values.creditmanagerGid + "'," +
                         "'" + values.zonal_name.Replace("    ", "").Replace("\n", "") + "'," +
                         "'" + values.businesshead_name.Replace("    ", "").Replace("\n", "") + "'," +
                         "'" + values.relationshipmgmt_name.Replace("    ", "").Replace("\n", "") + "'," +
                         "'" + values.creditmanager_name.Replace("    ", "").Replace("\n", "") + "'," +
                         "'" + values.cluster_manager_name.Replace("    ", "").Replace("\n", "") + "'," +
                         "'" + values.businessunit_name.Replace("    ", "").Replace("\n", "") + "'," +
                         "'" + values.businessunit_gid.Replace("    ", "").Replace("\n", "") + "'," +
                         "'" + lscustomer_type + "'," +
                         "'" + values.zonal_riskmanagerGID + "'," +
                         "'" + values.zonal_riskmanagerName.Replace("    ", "").Replace("\n", "") + "'," +
                         "'" + values.risk_managerGID + "'," +
                         "'" + values.riskmanager_name.Replace("    ", "").Replace("\n", "") + "'," +
                         "'" + values.riskMonitoring_GID + "'," +
                         "'" + values.riskMonitoring_Name.Replace("    ", "").Replace("\n", "") + "',";
                if (values.major_corporate == null)
                {
                    msSQL += "'',";

                }
                else
                {
                    msSQL += "'" + values.major_corporate.Replace("'", "") + "',";
                }
                msSQL+=   "'" + values.ccmail + "'," +
                         "'" + employee_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                    for (var i = 0; i < values.primaryvaluechain_list.Count; i++)
                    {

                        msGetGid1 = objcmnfunctions.GetMasterGID("CPRI");
                        msSQL = " insert into ocs_mst_tcustomer2primaryvaluechain(" +
                                " customer2primaryvaluechain_gid," +
                                " customer_gid," +
                                " primaryvaluechain_name," +
                                " primaryvaluechain_gid," +
                                " created_by," +
                                " created_date)" +
                                " values(" +
                                "'" + msGetGid1 + "'," +
                                "'" + msGetGid + "'," +
                                "'" + values.primaryvaluechain_list[i].valuechain_name + "'," +
                                "'" + values.primaryvaluechain_list[i].valuechain_gid + "'," +
                                "'" + employee_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    for (var i = 0; i < values.secondaryvaluechain_list.Count; i++)
                    {

                        msGetGid1 = objcmnfunctions.GetMasterGID("CSEC");
                        msSQL = " insert into ocs_mst_tcustomer2secondaryvaluechain(" +
                                " customer2secondaryvaluechain_gid," +
                                " customer_gid," +
                                " secondaryvaluechain_name," +
                                " secondaryvaluechain_gid," +
                                " created_by," +
                                " created_date)" +
                                " values(" +
                                "'" + msGetGid1 + "'," +
                                "'" + msGetGid + "'," +
                                "'" + values.secondaryvaluechain_list[i].valuechain_name + "'," +
                                "'" + values.secondaryvaluechain_list[i].valuechain_gid + "'," +
                                "'" + employee_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    //--------Customer 2 User info updation--//

                    msSQL = "update ocs_mst_tcustomer2userdtl set customer_gid ='" + msGetGid + "' where customer_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "select group_concat(primaryvaluechain_name) as primaryvaluechain from ocs_mst_tcustomer2primaryvaluechain where customer_gid ='" + msGetGid + "'";
                    string lsprimaryvaluechain = objdbconn.GetExecuteScalar(msSQL);
                    msSQL= "update ocs_mst_tcustomer set primaryvaluechain_name='"+ lsprimaryvaluechain  + "'where customer_gid ='" + msGetGid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "select group_concat(secondaryvaluechain_name) as secondaryvaluechain from ocs_mst_tcustomer2secondaryvaluechain where customer_gid ='" + msGetGid + "'";
                    string lssecondaryvaluechain = objdbconn.GetExecuteScalar(msSQL);
                    msSQL = "update ocs_mst_tcustomer set secondaryvaluechain_name='" + lssecondaryvaluechain + "'where customer_gid ='" + msGetGid + "'";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    values.message = "Customer Added Successfully";
                    values.status = true;
            }
            else
            {
                values.message = "Error Occured While Creating the Customer";
                values.status = false;

            }
            }
            objODBCDatareader.Close();
        }
        public void DaDeleteMobileNo(string customer2mobileno_gid ,string employee_gid, mdlcreatecustomer values)
        {
            msSQL = "delete from ocs_mst_tcustomer2mobileno where customer2mobileno_gid='" + customer2mobileno_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
              
                values.message = "Mobile No delete successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured While Creating the mobile no";
                values.status = false;

            }
        }

        public void DaDeleteAddress(string customer2address_gid,string employee_gid, MdlMstaddresstype values)
        {
            msSQL = "delete from ocs_mst_tcustomer2address where customer2address_gid='" + customer2address_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "Address delete successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured While Creating the Address";
                values.status = false;

            }
        }

        public void DeleteIDProof(string customer2identityproof_gid,string employee_gid, MdlID_proof values)
        {
            msSQL = "delete from ocs_mst_tcustomer2identityproof where customer2identityproof_gid='" + customer2identityproof_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "ID Proof delete successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured While Creating the ID Proof";
                values.status = false;

            }
        }
        public void DeleteMember(string customer2member_gid,string employee_gid, MdlMember values)
        {
            msSQL = "delete from ocs_mst_tcustomer2member where customer2member_gid='" + customer2member_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "Member delete successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured While Creating the Member";
                values.status = false;

            }
        }
        public void DaGetAge(string dob,string employee_gid, Mdlmobile_no values)
        {
            try
            {
                if (dob == "" || dob == null)
                {

                }
                else
                {
                    var date = DateTime.Parse(new string(dob.Take(24).ToArray()));
                    var dobdate = date.ToString("yyyy/MM/dd");

                    msSQL = "select (year(now())-year('" + dobdate + "'))";
                    values.age = objdbconn.GetExecuteScalar(msSQL);
                    values.status = true;
                }
            }
            catch
            {
                DateTime date2 = Convert.ToDateTime(dob, System.Globalization.CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat);
                var dobdate = date2.ToString("yyyy/MM/dd");

                msSQL = "select (year(now())-year('" + dobdate + "'))";
                values.age = objdbconn.GetExecuteScalar(msSQL);
                values.status = true;
            }
        }
        public void DaGetTempClear(string employee_gid, result values)
        {
            msSQL = "delete from ocs_mst_tcustomer2address where customer2usertype_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from ocs_mst_tcustomer2mobileno where customer2usertype_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from ocs_mst_tcustomer2identityproof where customer2usertype_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from ocs_mst_tcustomer2member where customer2usertype_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            values.status= true;
        }
        public void DaOverallTempClear(string employee_gid, result values)
        {
            msSQL = "delete from ocs_mst_tcustomer2userdtl where customer_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            values.status = true;
        }
        public void DaGetCustomer2UserInfo(string customer2usertype_gid, string employee_gid, mdlcustomer2userdtl values)
        {
            msSQL = "select customer2user_name,date_format(customer2user_dob,'%d-%m-%Y') as customer2user_dob,customer2user_gender,personalemail_address,"+
                   " officialemail_address,telephone_no,contact_person,aadhar_no,pan_no,user_type,photo_path,photo_name,customer2user_age,escrow,credit_rating,"+
                   " month_business,landmark,date_format(cin_date, '%d-%m-%Y') as cin_date,cin_no,contactperson_designation,company_type,year_business,gst_no,"+
                   " customer_type,usertype_gid from  ocs_mst_tcustomer2userdtl where customer2usertype_gid='" + customer2usertype_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if(objODBCDatareader.HasRows==true)
            {
                values.name = objODBCDatareader["customer2user_name"].ToString();
                values.dob = objODBCDatareader["customer2user_dob"].ToString();
                values.gender = objODBCDatareader["customer2user_gender"].ToString();
                values.personalemail_address = objODBCDatareader["personalemail_address"].ToString();
                values.officailemail_address = objODBCDatareader["officialemail_address"].ToString();
                values.telephone_no = objODBCDatareader["telephone_no"].ToString();
                values.contact_person = objODBCDatareader["contact_person"].ToString();
                values.aadhar_no = objODBCDatareader["aadhar_no"].ToString();
                values.pan_no = objODBCDatareader["pan_no"].ToString();
                values.photo_name = objODBCDatareader["photo_name"].ToString();
                values.photo_path = objcmnstorage.EncryptData(HttpContext.Current.Server.MapPath(objODBCDatareader["photo_path"].ToString())); 
                values.age = objODBCDatareader["customer2user_age"].ToString();
                values.escrow = objODBCDatareader["escrow"].ToString();
                values.credit_rating = objODBCDatareader["credit_rating"].ToString();
                values.month_business = objODBCDatareader["month_business"].ToString();
                values.landmark = objODBCDatareader["landmark"].ToString();
                values.cin_date = objODBCDatareader["cin_date"].ToString();
                values.cin_no = objODBCDatareader["cin_no"].ToString();
                values.contactperson_designation = objODBCDatareader["contactperson_designation"].ToString();
                values.company_type = objODBCDatareader["company_type"].ToString();
                values.year_business = objODBCDatareader["year_business"].ToString();
                values.gst_no = objODBCDatareader["gst_no"].ToString();
                values.customer_type = objODBCDatareader["customer_type"].ToString();
                values.user_type = objODBCDatareader["user_type"].ToString();
                values.usertype_gid = objODBCDatareader["usertype_gid"].ToString();
                objODBCDatareader.Close();
            }

            msSQL = "select address_type,addressline1,addressline2, city,state,taluka,country,district,postal_code,customer2address_gid,primary_address" +
                " from ocs_mst_tcustomer2address where " +
              " customer2usertype_gid='" + customer2usertype_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getaddress_list = new List<address_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getaddress_list.Add(new address_list
                    {
                        primary_address = (dr_datarow["primary_address"].ToString()),
                        address_type = (dr_datarow["address_type"].ToString()),
                        addressline1 = (dr_datarow["addressline1"].ToString()),
                        addressline2 = (dr_datarow["addressline2"].ToString()),
                        city = (dr_datarow["city"].ToString()),
                        state = (dr_datarow["state"].ToString()),
                        taluka = (dr_datarow["taluka"].ToString()),
                        district = (dr_datarow["district"].ToString()),
                        postal_code = (dr_datarow["postal_code"].ToString()),
                        country = (dr_datarow["country"].ToString()),
                        customer2address_gid = (dr_datarow["customer2address_gid"].ToString()),
                    });
                }
                values.address_list = getaddress_list;
            }
            dt_datatable.Dispose();

            msSQL = "select customer2identityproof_gid,idproof_type,idproof_number from ocs_mst_tcustomer2identityproof where " +
              " customer2usertype_gid='" + customer2usertype_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getidprooflist = new List<idproof_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getidprooflist.Add(new idproof_list
                    {
                        idproof_type = (dr_datarow["idproof_type"].ToString()),
                        idproof_no = (dr_datarow["idproof_number"].ToString()),
                        customer2identityproof_gid = (dr_datarow["customer2identityproof_gid"].ToString()),
                    });
                }
                values.idproof_list = getidprooflist;
            }
            dt_datatable.Dispose();
            msSQL = "select mobile_no,customer2mobileno_gid,primary_mobileno from ocs_mst_tcustomer2mobileno where " +
               " customer2usertype_gid='" + customer2usertype_gid + "'";
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
                        customer2mobileno_gid = (dr_datarow["customer2mobileno_gid"].ToString()),
                    });
                }
                values.mobileno_list = getmobileno_list;
            }
            dt_datatable.Dispose();
            msSQL = "select customer2member_gid,member_name,member_designation from ocs_mst_tcustomer2member where " +
              " customer2usertype_gid='" + customer2usertype_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmember_list = new List<member_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmember_list.Add(new member_list
                    {
                        member_designation = (dr_datarow["member_designation"].ToString()),
                        member_name = (dr_datarow["member_name"].ToString()),
                        customer2member_gid = (dr_datarow["customer2member_gid"].ToString()),
                    });
                }
                values.member_list = getmember_list;
            }
            dt_datatable.Dispose();
            values.status = true;
        }

        public void DaGetViewCustomer2UserDtl(string customer_gid,string employee_gid, mdlcustomer2userdtl values)
        {
            msSQL = "select customer_urn,vertical_code,zonal_name,businesshead_name,relationshipmgmt_name,cluster_manager_name,creditmgmt_name,constitution_name,customername," +
                " sa_payout,sa_idname,secondaryvaluechain_name,primaryvaluechain_name,SA_status,businessunit_name from ocs_mst_tcustomer where customer_gid='" + customer_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if(objODBCDatareader.HasRows==true)
            {
                values.customer_urn = objODBCDatareader["customer_urn"].ToString();
                values.vertical = objODBCDatareader["vertical_code"].ToString();
                values.zonal_head = objODBCDatareader["zonal_name"].ToString();
                values.business_head = objODBCDatareader["businesshead_name"].ToString();
                values.rm_name = objODBCDatareader["relationshipmgmt_name"].ToString();
                values.cluster_manager = objODBCDatareader["cluster_manager_name"].ToString();
                values.credit_manager = objODBCDatareader["creditmgmt_name"].ToString();
                values.constitution = objODBCDatareader["constitution_name"].ToString();
                values.sa_payout = objODBCDatareader["sa_payout"].ToString();
                values.sa_idname = objODBCDatareader["sa_idname"].ToString();
                values.secondaryvalue_chain = objODBCDatareader["secondaryvaluechain_name"].ToString();
                values.primaryvalue_chain = objODBCDatareader["primaryvaluechain_name"].ToString();
                values.sa_status = objODBCDatareader["SA_status"].ToString();
                values.business_unit = objODBCDatareader["businessunit_name"].ToString();
                values.customername = objODBCDatareader["customername"].ToString();
            }
            objODBCDatareader.Close();
            msSQL = " select a.customer2user_name,date_format(a.created_date,'%d-%m-%Y') as created_date,user_type,customer2usertype_gid," +
                    " concat(c.user_firstname, ' ', c.user_lastname, '/', c.user_code) as created_by,customer_type from ocs_mst_tcustomer2userdtl a" +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                    " left  join adm_mst_tuser c on b.user_gid = c.user_gid where " +
                    " customer_gid='" + customer_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcustomer2userdtl_list = new List<customer2userdtl_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcustomer2userdtl_list.Add(new customer2userdtl_list
                    {
                        name = (dr_datarow["customer2user_name"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        user_type = (dr_datarow["user_type"].ToString()),
                        customer2usertype_gid = (dr_datarow["customer2usertype_gid"].ToString()),
                        customer_type = (dr_datarow["customer_type"].ToString()),
                    });
                }
                values.customer2userdtl_list = getcustomer2userdtl_list;
            }
            dt_datatable.Dispose();
        }

        public void DaGetIndividualInformation(string aadhar_no, string employee_gid, mdlcustomer2userdtl values)
        {
            msSQL = "select customer2user_name,customer2user_gender,personalemail_address,officialemail_address,telephone_no,contact_person,aadhar_no, " +
                    "pan_no,photo_name,photo_path,customer2user_age,escrow,credit_rating,month_business,landmark, cin_no,contactperson_designation," +
                    "company_type,year_business,gst_no,customer_type,user_type,customer2usertype_gid,customer2user_dob,cin_date,guarantor_id" +
                   " from ocs_mst_tcustomer2userdtl where aadhar_no='" + aadhar_no + "' group by aadhar_no";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.name = objODBCDatareader["customer2user_name"].ToString();
                //  values.dob = objODBCDatareader["customer2user_dob"].ToString();
                values.gender = objODBCDatareader["customer2user_gender"].ToString();
                values.personalemail_address = objODBCDatareader["personalemail_address"].ToString();
                values.officailemail_address = objODBCDatareader["officialemail_address"].ToString();
                values.telephone_no = objODBCDatareader["telephone_no"].ToString();
                values.contact_person = objODBCDatareader["contact_person"].ToString();
                values.aadhar_no = objODBCDatareader["aadhar_no"].ToString();
                values.pan_no = objODBCDatareader["pan_no"].ToString();
                values.photo_name = objODBCDatareader["photo_name"].ToString();
                values.photo_path = objcmnstorage.EncryptData(HttpContext.Current.Server.MapPath(objODBCDatareader["photo_path"].ToString()));
                values.age = objODBCDatareader["customer2user_age"].ToString();
                values.escrow = objODBCDatareader["escrow"].ToString();
                values.credit_rating = objODBCDatareader["credit_rating"].ToString();
                values.month_business = objODBCDatareader["month_business"].ToString();
                values.landmark = objODBCDatareader["landmark"].ToString();
                //values.cin_date = objODBCDatareader["cin_date"].ToString();
                values.cin_no = objODBCDatareader["cin_no"].ToString();
                values.contactperson_designation = objODBCDatareader["contactperson_designation"].ToString();
                values.company_type = objODBCDatareader["company_type"].ToString();
                values.year_business = objODBCDatareader["year_business"].ToString();
                values.gst_no = objODBCDatareader["gst_no"].ToString();
                values.customer_type = objODBCDatareader["customer_type"].ToString();
                values.user_type = objODBCDatareader["user_type"].ToString();
                lscustomer2usertype_gid = objODBCDatareader["customer2usertype_gid"].ToString();
                if (objODBCDatareader["customer2user_dob"].ToString() == "")
                {
                }
                else
                {
                    values.dob = Convert.ToDateTime(objODBCDatareader["customer2user_dob"]).ToString("MM-dd-yyyy");
                }
                if (objODBCDatareader["cin_date"].ToString() == "")
                {
                }
                else
                {
                    values.cin_date = Convert.ToDateTime(objODBCDatareader["cin_date"]).ToString("MM-dd-yyyy");
                }
                values.guarantor_id = objODBCDatareader["guarantor_id"].ToString();
                objODBCDatareader.Close();
            }
           if(lscustomer2usertype_gid ==null||lscustomer2usertype_gid =="")
            {

            }
            else
            {

           
            msSQL = "select address_type,addressline1,addressline2,state_gid, city,state,taluka,country,district,postal_code,customer2address_gid,primary_address" +
              " from ocs_mst_tcustomer2address where " +
            " customer2usertype_gid='" + lscustomer2usertype_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getaddress_list = new List<address_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getaddress_list.Add(new address_list
                    {
                        primary_address = (dr_datarow["primary_address"].ToString()),
                        address_type = (dr_datarow["address_type"].ToString()),
                        addressline1 = (dr_datarow["addressline1"].ToString()),
                        addressline2 = (dr_datarow["addressline2"].ToString()),
                        city = (dr_datarow["city"].ToString()),
                        state = (dr_datarow["state"].ToString()),
                        taluka = (dr_datarow["taluka"].ToString()),
                        district = (dr_datarow["district"].ToString()),
                        postal_code = (dr_datarow["postal_code"].ToString()),
                        country = (dr_datarow["country"].ToString()),
                        customer2address_gid = (dr_datarow["customer2address_gid"].ToString()),
                    });
                    msGetGid = objcmnfunctions.GetMasterGID("CU2A");
                    msSQL = " insert into ocs_mst_tcustomer2address(" +
                            " customer2address_gid," +
                            " customer2usertype_gid," +
                            " address_type," +
                            " addressline1," +
                            " addressline2," +
                            " city," +
                            " postal_code," +
                            " taluka," +
                            " district," +
                            " state," +
                            " state_gid," +
                            " country," +
                            " primary_address," +
                            " created_by," +
                            " created_date)" +
                            " values(" +
                            "'" + msGetGid + "'," +
                            "'" + employee_gid + "'," +
                             "'" + dr_datarow["address_type"].ToString() + "'," +
                            "'" + dr_datarow["addressline1"].ToString() + "'," +
                            "'" + dr_datarow["addressline2"].ToString() + "'," +
                            "'" + dr_datarow["city"].ToString() + "'," +
                            "'" + dr_datarow["postal_code"].ToString() + "'," +
                            "'" + dr_datarow["taluka"].ToString() + "'," +
                            "'" + dr_datarow["district"].ToString() + "'," +
                            "'" + dr_datarow["state"].ToString() + "'," +
                            "'" + dr_datarow["state_gid"].ToString() + "'," +
                            "'" + dr_datarow["country"].ToString() + "'," +
                             "'" + dr_datarow["primary_address"].ToString() + "'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                values.address_list = getaddress_list;
            }
            dt_datatable.Dispose();

            msSQL = "select customer2identityproof_gid,idproof_type,idproof_number from ocs_mst_tcustomer2identityproof where " +
              " customer2usertype_gid='" + lscustomer2usertype_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getidprooflist = new List<idproof_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getidprooflist.Add(new idproof_list
                    {
                        idproof_type = (dr_datarow["idproof_type"].ToString()),
                        idproof_no = (dr_datarow["idproof_number"].ToString()),
                        customer2identityproof_gid = (dr_datarow["customer2identityproof_gid"].ToString()),
                    });
                    msGetGid = objcmnfunctions.GetMasterGID("CU2I");
                    msSQL = " insert into ocs_mst_tcustomer2identityproof(" +
                            " customer2identityproof_gid," +
                            " customer2usertype_gid," +
                            " idproof_type," +
                            " idproof_number," +
                            " created_by," +
                            " created_date)" +
                            " values(" +
                            "'" + msGetGid + "'," +
                            "'" + employee_gid + "'," +
                            "'" + dr_datarow["idproof_type"].ToString() + "'," +
                            "'" + dr_datarow["idproof_number"].ToString() + "'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                values.idproof_list = getidprooflist;
            }
            dt_datatable.Dispose();
            msSQL = "select mobile_no,customer2mobileno_gid,primary_mobileno from ocs_mst_tcustomer2mobileno where " +
               " customer2usertype_gid='" + lscustomer2usertype_gid + "'";
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
                        customer2mobileno_gid = (dr_datarow["customer2mobileno_gid"].ToString()),
                    });
                    msGetGid = objcmnfunctions.GetMasterGID("CUML");
                    msSQL = " insert into ocs_mst_tcustomer2mobileno(" +
                            " customer2mobileno_gid," +
                            " customer2usertype_gid," +
                            " mobile_no," +
                            " primary_mobileno," +
                            " created_by," +
                            " created_date)" +
                            " values(" +
                            "'" + msGetGid + "'," +
                            "'" + employee_gid + "'," +
                            "'" + dr_datarow["mobile_no"].ToString() + "'," +
                            "'" + dr_datarow["primary_mobileno"].ToString() + "'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                values.mobileno_list = getmobileno_list;
                dt_datatable.Dispose();
                values.status = true;
            }
                values.status = true;
            }
        }

        public void DaGetInstitutionInformation(string gst_no, string employee_gid, mdlcustomer2userdtl values)
        {
            msSQL = "select customer2user_name,customer2user_gender,personalemail_address,officialemail_address,telephone_no,contact_person,aadhar_no, " +
                    "pan_no,photo_name,photo_path,customer2user_age,escrow,credit_rating,month_business,landmark, cin_no,contactperson_designation," +
                    "company_type,year_business,gst_no,customer_type,user_type,customer2usertype_gid,customer2user_dob,cin_date,guarantor_id" +
                    " from ocs_mst_tcustomer2userdtl where gst_no='" + gst_no + "'group by gst_no";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.name = objODBCDatareader["customer2user_name"].ToString();
                //  values.dob = objODBCDatareader["customer2user_dob"].ToString();
                values.gender = objODBCDatareader["customer2user_gender"].ToString();
                values.personalemail_address = objODBCDatareader["personalemail_address"].ToString();
                values.officailemail_address = objODBCDatareader["officialemail_address"].ToString();
                values.telephone_no = objODBCDatareader["telephone_no"].ToString();
                values.contact_person = objODBCDatareader["contact_person"].ToString();
                values.aadhar_no = objODBCDatareader["aadhar_no"].ToString();
                values.pan_no = objODBCDatareader["pan_no"].ToString();
                values.photo_name = objODBCDatareader["photo_name"].ToString();
                values.photo_path = objcmnstorage.EncryptData(HttpContext.Current.Server.MapPath(objODBCDatareader["photo_path"].ToString()));
                values.age = objODBCDatareader["customer2user_age"].ToString();
                values.escrow = objODBCDatareader["escrow"].ToString();
                values.credit_rating = objODBCDatareader["credit_rating"].ToString();
                values.month_business = objODBCDatareader["month_business"].ToString();
                values.landmark = objODBCDatareader["landmark"].ToString();
                //values.cin_date = objODBCDatareader["cin_date"].ToString();
                values.cin_no = objODBCDatareader["cin_no"].ToString();
                values.contactperson_designation = objODBCDatareader["contactperson_designation"].ToString();
                values.company_type = objODBCDatareader["company_type"].ToString();
                values.year_business = objODBCDatareader["year_business"].ToString();
                values.gst_no = objODBCDatareader["gst_no"].ToString();
                values.customer_type = objODBCDatareader["customer_type"].ToString();
                values.user_type = objODBCDatareader["user_type"].ToString();
                lscustomer2usertype_gid = objODBCDatareader["customer2usertype_gid"].ToString();
                if (objODBCDatareader["customer2user_dob"].ToString() == "")
                {
                }
                else
                {
                    values.dob = Convert.ToDateTime(objODBCDatareader["customer2user_dob"]).ToString("MM-dd-yyyy");
                }
                if (objODBCDatareader["cin_date"].ToString() == "")
                {
                }
                else
                {
                    values.cin_date = Convert.ToDateTime(objODBCDatareader["cin_date"]).ToString("MM-dd-yyyy");
                }
                values.guarantor_id = objODBCDatareader["guarantor_id"].ToString();
                objODBCDatareader.Close();
            }
            if (lscustomer2usertype_gid == null || lscustomer2usertype_gid == "")
            {

            }
            else
            {

                msSQL = "select address_type,addressline1,addressline2,state_gid, city,state,taluka,country,district,postal_code,customer2address_gid,primary_address" +
                " from ocs_mst_tcustomer2address where " +
              " customer2usertype_gid='" + lscustomer2usertype_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getaddress_list = new List<address_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getaddress_list.Add(new address_list
                        {
                            primary_address = (dr_datarow["primary_address"].ToString()),
                            address_type = (dr_datarow["address_type"].ToString()),
                            addressline1 = (dr_datarow["addressline1"].ToString()),
                            addressline2 = (dr_datarow["addressline2"].ToString()),
                            city = (dr_datarow["city"].ToString()),
                            state = (dr_datarow["state"].ToString()),
                            taluka = (dr_datarow["taluka"].ToString()),
                            district = (dr_datarow["district"].ToString()),
                            postal_code = (dr_datarow["postal_code"].ToString()),
                            country = (dr_datarow["country"].ToString()),
                            customer2address_gid = (dr_datarow["customer2address_gid"].ToString()),
                        });
                        msGetGid = objcmnfunctions.GetMasterGID("CU2A");
                        msSQL = " insert into ocs_mst_tcustomer2address(" +
                                " customer2address_gid," +
                                " customer2usertype_gid," +
                                " address_type," +
                                " addressline1," +
                                " addressline2," +
                                " city," +
                                " postal_code," +
                                " taluka," +
                                " district," +
                                " state," +
                                " state_gid," +
                                " country," +
                                " primary_address," +
                                " created_by," +
                                " created_date)" +
                                " values(" +
                                "'" + msGetGid + "'," +
                                "'" + employee_gid + "'," +
                                 "'" + dr_datarow["address_type"].ToString() + "'," +
                                "'" + dr_datarow["addressline1"].ToString() + "'," +
                                "'" + dr_datarow["addressline2"].ToString() + "'," +
                                "'" + dr_datarow["city"].ToString() + "'," +
                                "'" + dr_datarow["postal_code"].ToString() + "'," +
                                "'" + dr_datarow["taluka"].ToString() + "'," +
                                "'" + dr_datarow["district"].ToString() + "'," +
                                "'" + dr_datarow["state"].ToString() + "'," +
                                "'" + dr_datarow["state_gid"].ToString() + "'," +
                                "'" + dr_datarow["country"].ToString() + "'," +
                                 "'" + dr_datarow["primary_address"].ToString() + "'," +
                                "'" + employee_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    values.address_list = getaddress_list;
                }
                dt_datatable.Dispose();

                msSQL = "select customer2identityproof_gid,idproof_type,idproof_number from ocs_mst_tcustomer2identityproof where " +
                  " customer2usertype_gid='" + lscustomer2usertype_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getidprooflist = new List<idproof_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getidprooflist.Add(new idproof_list
                        {
                            idproof_type = (dr_datarow["idproof_type"].ToString()),
                            idproof_no = (dr_datarow["idproof_number"].ToString()),
                            customer2identityproof_gid = (dr_datarow["customer2identityproof_gid"].ToString()),
                        });
                        msGetGid = objcmnfunctions.GetMasterGID("CU2I");
                        msSQL = " insert into ocs_mst_tcustomer2identityproof(" +
                                " customer2identityproof_gid," +
                                " customer2usertype_gid," +
                                " idproof_type," +
                                " idproof_number," +
                                " created_by," +
                                " created_date)" +
                                " values(" +
                                "'" + msGetGid + "'," +
                                "'" + employee_gid + "'," +
                                "'" + dr_datarow["idproof_type"].ToString() + "'," +
                                "'" + dr_datarow["idproof_number"].ToString() + "'," +
                                "'" + employee_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    values.idproof_list = getidprooflist;
                }
                dt_datatable.Dispose();
                msSQL = "select mobile_no,customer2mobileno_gid,primary_mobileno from ocs_mst_tcustomer2mobileno where " +
                   " customer2usertype_gid='" + lscustomer2usertype_gid + "'";
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
                            customer2mobileno_gid = (dr_datarow["customer2mobileno_gid"].ToString()),
                        });
                        msGetGid = objcmnfunctions.GetMasterGID("CUML");
                        msSQL = " insert into ocs_mst_tcustomer2mobileno(" +
                                " customer2mobileno_gid," +
                                " customer2usertype_gid," +
                                " mobile_no," +
                                " primary_mobileno," +
                                " created_by," +
                                " created_date)" +
                                " values(" +
                                "'" + msGetGid + "'," +
                                "'" + employee_gid + "'," +
                                "'" + dr_datarow["mobile_no"].ToString() + "'," +
                                "'" + dr_datarow["primary_mobileno"].ToString() + "'," +
                                "'" + employee_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    values.mobileno_list = getmobileno_list;
                }
                dt_datatable.Dispose();

            }
            values.status = true;
        }

        public void DaGetEditCustomer2UserDtl(string customer_gid, string employee_gid, mdlcreatecustomer values)
        {
            msSQL = "select customer_urn,vertical_code,zonal_name,businesshead_name,relationshipmgmt_name,cluster_manager_name,creditmgmt_name,constitution_name," +
                " sa_payout,sa_idname,secondaryvaluechain_name,primaryvaluechain_name,SA_status,businessunit_name,businessunit_gid,"+
                " vertical_gid,zonal_head,business_head, relationship_manager,cluster_manager_gid,creditmanager_gid,"+
                " constitution_gid,sa_id_gid,customer_gid,zonal_riskmanager,zonal_riskmanagerName,assigned_RM,assigned_RMName, " +
                " riskMonitoring_GID,riskMonitoring_Name,major_corporate,ccmail_text from ocs_mst_tcustomer where customer_gid='" + customer_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.customer_urn = objODBCDatareader["customer_urn"].ToString();
                values.vertical_code = objODBCDatareader["vertical_gid"].ToString();
                values.zonalGid = objODBCDatareader["zonal_head"].ToString();
                values.businessHeadGid = objODBCDatareader["business_head"].ToString();
                values.relationshipMgmtGid = objODBCDatareader["relationship_manager"].ToString();
                values.clustermanagerGid = objODBCDatareader["cluster_manager_gid"].ToString();
                values.creditmanagerGid = objODBCDatareader["creditmanager_gid"].ToString();
                values.constitution_gid = objODBCDatareader["constitution_gid"].ToString();
                values.sa_payout = objODBCDatareader["sa_payout"].ToString();
                values.sa_id_gid = objODBCDatareader["sa_id_gid"].ToString();
                values.sa_status = objODBCDatareader["SA_status"].ToString();
                values.businessunit_gid = objODBCDatareader["businessunit_gid"].ToString();
                values.customer_gid = objODBCDatareader["customer_gid"].ToString();
                values.major_corporate = objODBCDatareader["major_corporate"].ToString();
                values.ccmail = objODBCDatareader["ccmail_text"].ToString();
                values.major_corporate = objODBCDatareader["major_corporate"].ToString();
                values.zonal_riskmanagerGID = objODBCDatareader["zonal_riskmanager"].ToString();
                values.zonal_riskmanagerName = objODBCDatareader["zonal_riskmanagerName"].ToString();
                values.risk_managerGID = objODBCDatareader["assigned_RM"].ToString();
                values.riskmanager_name = objODBCDatareader["assigned_RMName"].ToString();
                values.riskMonitoring_GID = objODBCDatareader["riskMonitoring_GID"].ToString();
                values.riskMonitoring_Name = objODBCDatareader["riskMonitoring_Name"].ToString();
            }
            objODBCDatareader.Close();
            msSQL = " select a.customer2user_name,date_format(a.created_date,'%d-%m-%Y') as created_date,user_type,customer2usertype_gid," +
                    " concat(c.user_firstname, ' ', c.user_lastname, '/', c.user_code) as created_by,customer_type,customer_gid from ocs_mst_tcustomer2userdtl a" +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                    " left  join adm_mst_tuser c on b.user_gid = c.user_gid where " +
                    " customer_gid='" + customer_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcustomer2userdtl_list = new List<customer2userdtl_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcustomer2userdtl_list.Add(new customer2userdtl_list
                    {
                        name = (dr_datarow["customer2user_name"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        user_type = (dr_datarow["user_type"].ToString()),
                        customer2usertype_gid = (dr_datarow["customer2usertype_gid"].ToString()),
                        customer_type = (dr_datarow["customer_type"].ToString()),
                        customer_gid = (dr_datarow["customer_gid"].ToString())
                    });
                }
                values.customer2userdtl_list = getcustomer2userdtl_list;
            }
            dt_datatable.Dispose();
            msSQL = "select primaryvaluechain_gid,primaryvaluechain_name from ocs_mst_tcustomer2primaryvaluechain where customer_gid ='" + customer_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);

            values.primaryvaluechain_list = dt_datatable.AsEnumerable().Select(row =>
            new primaryvaluechain_list { valuechain_gid = row["primaryvaluechain_gid"].ToString(),
                valuechain_name = row["primaryvaluechain_name"].ToString()
            }).ToList();
            dt_datatable.Dispose();
           
            msSQL = "select secondaryvaluechain_gid,secondaryvaluechain_name from ocs_mst_tcustomer2secondaryvaluechain where customer_gid ='" + customer_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);

            values.secondaryvaluechain_list = dt_datatable.AsEnumerable().Select(row =>
         new secondaryvaluechain_list { valuechain_gid = row["secondaryvaluechain_gid"].ToString(),
          valuechain_name = row["secondaryvaluechain_name"].ToString()
         }).ToList();
            dt_datatable.Dispose(); 
            
        }
        //---------Submit - individual Information--------//
        public void DaPostIndividualUpdate(string employee_gid, mdlcustomer2userdtl values)
        {
            msSQL = "select customer2mobileno_gid from ocs_mst_tcustomer2mobileno where (customer2usertype_gid='" + employee_gid + "' or customer2usertype_gid='" + values.customer2usertype_gid + "') and primary_mobileno='Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Kindly Add Mobile No with Primary Status";
                return;
            }
            msSQL = "select customer2address_gid from ocs_mst_tcustomer2address where (customer2usertype_gid='" + employee_gid + "' or customer2usertype_gid='" + values.customer2usertype_gid + "') and primary_address='Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Kindly Add Address with Primary Status";
                return;
            }
            
            msSQL = " update ocs_mst_tcustomer2userdtl set " +
                    " customer2user_name='" + values.name + "',";
            if ((values.dob == null) || (values.dob == ""))
            {
                msSQL += "customer2user_dob=null,";
            }
            else
            {
                msSQL += "customer2user_dob= '" + Convert.ToDateTime(values.dob).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            msSQL+= " customer2user_age='" + values.age + "'," +
                    " customer2user_gender='" + values.gender + "'," +
                    " personalemail_address ='" + values.personalemail_address + "'," +
                    " officialemail_address='" + values.officailemail_address + "'," +
                    " telephone_no='" + values.telephone_no + "'," +
                    " contact_person='" + values.contact_person + "'," +
                    " contactperson_designation='" + values.contactperson_designation + "'," +
                    " aadhar_no='" + values.aadhar_no + "'," +
                    " pan_no='" + values.pan_no + "'," +
                    " user_type='" + values.user_type + "'," +
                    " usertype_gid='" + values.usertype_gid + "'," +
                    " customer_type ='Individual'," +
                    " guarantor_id='" + values.guarantor_id + "'," +
                    " updated_by='" + employee_gid + "'," +
                    " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where customer2usertype_gid='"+ values.customer2usertype_gid + "'";
                   
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                //--------Mobile No updation--//
                msSQL = "update ocs_mst_tcustomer2mobileno set customer2usertype_gid ='" + values.customer2usertype_gid + "' where customer2usertype_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                //---------Address updation------//
                msSQL = "update ocs_mst_tcustomer2address set customer2usertype_gid ='" + values.customer2usertype_gid + "' where customer2usertype_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                //---------ID Proof updation------//
                msSQL = "update ocs_mst_tcustomer2identityproof set customer2usertype_gid ='" + values.customer2usertype_gid + "' where customer2usertype_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                //---------Photo updation------//

                msSQL = "select photo_name,photo_path from ocs_tmp_tphotoupload where created_by='" + employee_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    msSQL = "update ocs_mst_tcustomer2userdtl set photo_name ='" + objODBCDatareader["photo_name"].ToString() + "'," +
                        " photo_path='" + objcmnstorage.EncryptData(objODBCDatareader["photo_path"].ToString()) + "' where customer2usertype_gid='" + values.customer2usertype_gid + "'";
                    objODBCDatareader.Close();
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                values.status = true;
                values.message = "Individual Information updated sucessfully";

            }
            else
            {
                values.status = true;
                values.message = "Error Occured while updating Individual";

            }
        }
        //---------Submit - institution Information--------//
        public void DaPostInstitutionUpdate(string employee_gid, mdlcustomer2userdtl values)
        {
            msSQL = "select customer2mobileno_gid from ocs_mst_tcustomer2mobileno where (customer2usertype_gid='" + employee_gid + "' or customer2usertype_gid='" + values.customer2usertype_gid + "') and primary_mobileno='Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Kindly Add Mobile No with Primary Status";
                return;
            }
            msSQL = "select customer2address_gid from ocs_mst_tcustomer2address where (customer2usertype_gid='" + employee_gid + "' or customer2usertype_gid='" + values.customer2usertype_gid + "') and primary_address='Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Kindly Add Address with Primary Status";
                return;
            }
            //msSQL = "select user_type from ocs_mst_tcustomer2userdtl where (customer2usertype_gid='" + employee_gid + "' or customer2usertype_gid='" + values.customer2usertype_gid + "')" +
            //    " and user_type='Applicant'";
            //objODBCDatareader = objdbconn.GetDataReader(msSQL);
            //if (objODBCDatareader.HasRows == true)
            //{
            //    objODBCDatareader.Close();
            //    values.status = false;
            //    values.message = "Applicant Information already added";
            //    return;
            //}
            msSQL = " update ocs_mst_tcustomer2userdtl set" +
                     " customer2user_name='" + values.name + "',";
            if (values.cin_date == null)
            {
                msSQL += "cin_date=null,";
            }
            else
            {
                msSQL += "cin_date='" + Convert.ToDateTime(values.cin_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
           msSQL+=  " personalemail_address='" + values.personalemail_address + "'," +
                    " telephone_no='"+ values.telephone_no + "'," +
                    " contact_person='"+ values.contact_person + "'," +
                    " contactperson_designation='"+ values.contactperson_designation + "'," +
                    " company_type='" + values.company_type + "'," +
                    " year_business ='"+ values.year_business + "'," +
                    " month_business='"+ values.month_business + "'," +
                    " landmark='"+ values.landmark +"'," +
                    " credit_rating='"+ values.credit_rating+"'," +
                    " escrow='"+ values.escrow + "'," +
                    " cin_no='"+ values.cin_no + "'," +
                    " pan_no='" + values.pan_no + "'," +
                    " gst_no='"+ values.gst_no + "'," +
                    " user_type='"+ values.user_type + "'," +
                    " usertype_gid='" + values.usertype_gid + "'," +
                    " customer_type ='Institution'," +
                    " guarantor_id='" + values.guarantor_id + "'," +
                    " updated_by='" + employee_gid + "'," +
                    " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where customer2usertype_gid='" + values.customer2usertype_gid + "'";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                //--------Mobile No updation--//
                msSQL = "update ocs_mst_tcustomer2mobileno set customer2usertype_gid ='" + values.customer2usertype_gid + "' where customer2usertype_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                //---------Address updation------//
                msSQL = "update ocs_mst_tcustomer2address set customer2usertype_gid ='" + values.customer2usertype_gid + "' where customer2usertype_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                //---------ID Proof updation------//
                msSQL = "update ocs_mst_tcustomer2identityproof set customer2usertype_gid ='" + values.customer2usertype_gid + "' where customer2usertype_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                //-----------Member Updation-------//
                msSQL = "update ocs_mst_tcustomer2member set customer2usertype_gid ='" + values.customer2usertype_gid + "' where customer2usertype_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Individual Information added sucessfully";

            }
            else
            {
                values.status = true;
                values.message = "Error Occured while adding Individual";

            }
        }
        //---------Update - Customer Information--------//
        public void DaPostCustomerUpdate(string employee_gid, mdlcreatecustomer values)
        {

            if (values.customer_urn == null||values.customer_urn=="")
            {
                
            }
            else
            {
                msSQL = "select customer_gid from ocs_mst_tcustomer where customer_urn='" + values.customer_urn + "' and customer_gid <>'" + values.customer_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    objODBCDatareader.Close();
                    values.status = false;
                    values.message = "Customer URN Already Exists";
                    return;
                }
                objODBCDatareader.Close();
            }

            
            msSQL = "select customer2user_name ,customer2usertype_gid,gst_no,pan_no,personalemail_address,contact_person,customer_type " +
                " from ocs_mst_tcustomer2userdtl where (customer_gid='" + employee_gid + "'or customer_gid='" + values.customer_gid + "') and user_type='Applicant'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lscustomer_name = objODBCDatareader["customer2user_name"].ToString();
                lsgst_no = objODBCDatareader["gst_no"].ToString();
                lspan_no = objODBCDatareader["pan_no"].ToString();
                lspersonalemail_address = objODBCDatareader["personalemail_address"].ToString();
                lscontact_person = objODBCDatareader["contact_person"].ToString();
                lscustomer2userdtl_gid = objODBCDatareader["customer2usertype_gid"].ToString();
                lscustomer_type = objODBCDatareader["customer_type"].ToString();
                objODBCDatareader.Close();
            }
           

            msSQL = "select mobile_no from ocs_mst_tcustomer2mobileno where customer2usertype_gid='" + lscustomer2userdtl_gid + "' and primary_mobileno='Yes'";
            lsmobile_no = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select addressline1,addressline2,state,state_gid,taluka,district,city,postal_code,country from ocs_mst_tcustomer2address where customer2usertype_gid='" + lscustomer2userdtl_gid + "' and primary_address='Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

           

            if (objODBCDatareader.HasRows == true)
            {

                msSQL = " update ocs_mst_tcustomer set" +
                       " customername='" + lscustomer_name + "'," +
                        " contactperson='" + lscontact_person + "'," +
                        " customer_urn='" + values.customer_urn + "'," +
                        " gst_number='" + lsgst_no + "'," +
                        " pan_number='" + lspan_no + "'," +
                        " mobileno='" + lsmobile_no + "'," +
                        " email='" + lspersonalemail_address + "'," +
                        " address='" + objODBCDatareader["addressline1"].ToString() + "'," +
                        " address2='" + objODBCDatareader["addressline2"].ToString() + "'," +
                        " state='" + objODBCDatareader["state"].ToString() + "'," +
                        " state_gid='" + objODBCDatareader["state_gid"].ToString() + "'," +
                        " taluka='" + objODBCDatareader["taluka"].ToString() + "'," +
                        " district='" + objODBCDatareader["district"].ToString() + "'," +
                        " city='" + objODBCDatareader["city"].ToString() + "'," +
                        " vertical_gid='" + values.vertical_gid + "'," +
                        " vertical_code='" + values.vertical_code.Replace("'", "").Replace("\n", "") + "'," +
                        " postalcode='" + objODBCDatareader["postal_code"].ToString() + "'," +
                        " country='" + objODBCDatareader["country"].ToString() + "'," +
                        " constitution_name='" + values.constitution_name.Replace("   ", "").Replace("\n", "") + "'," +
                        " constitution_gid='" + values.constitution_gid + "'," +
                        " SA_status='" + values.sa_status + "'," +
                        " sa_id_gid='" + values.sa_id_gid + "'," +
                        " sa_idname='" + values.sa_idname + "'," +
                        " sa_payout='" + values.sa_payout + "'," +
                        " zonal_head='" + values.zonalGid + "'," +
                        " business_head='" + values.businessHeadGid + "'," +
                        " relationship_manager='" + values.relationshipMgmtGid + "'," +
                        " cluster_manager_gid='" + values.clustermanagerGid + "'," +
                        " creditmanager_gid='" + values.creditmanagerGid + "'," +
                        " zonal_name='" + values.zonal_name.Replace("    ", "").Replace("\n", "") + "'," +
                        " businesshead_name='" + values.businesshead_name.Replace("    ", "").Replace("\n", "") + "'," +
                        " relationshipmgmt_name='" + values.relationshipmgmt_name.Replace("    ", "").Replace("\n", "") + "'," +
                        " creditmgmt_name='" + values.creditmanager_name.Replace("    ", "").Replace("\n", "") + "'," +
                        " cluster_manager_name='" + values.cluster_manager_name.Replace("    ", "").Replace("\n", "") + "'," +
                        " businessunit_name='" + values.businessunit_name.Replace("    ", "").Replace("\n", "") + "'," +
                        " businessunit_gid='" + values.businessunit_gid.Replace("    ", "").Replace("\n", "") + "'," +
                        " customer_type='" + lscustomer_type + "'," +
                        " zonal_riskmanager='" + values.zonal_riskmanagerGID + "'," +
                        " zonal_riskmanagerName='" + values.zonal_riskmanagerName + "'," +
                        " assigned_RM='" + values.risk_managerGID + "'," +
                        " assigned_RMName='" + values.riskmanager_name + "'," +
                        " riskMonitoring_GID='" + values.riskMonitoring_GID + "'," +
                        " riskMonitoring_Name='" + values.riskMonitoring_Name + "'," +
                        " ccmail_text='" + values.ccmail + "',";
                if (values.major_corporate==null || values.major_corporate == "")
                {
                    msSQL += " major_corporate='',";
                }
                else
                {
                    msSQL += " major_corporate='" + values.major_corporate.Replace("'", "") + "',";
                }

                msSQL += " updated_by='" + employee_gid + "'," +
                        " updated_date='"+ DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+"' where customer_gid='"+ values.customer_gid +"'";
                      
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    msSQL = "delete from ocs_mst_tcustomer2primaryvaluechain where customer_gid='" + values.customer_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "delete from ocs_mst_tcustomer2secondaryvaluechain where customer_gid='" + values.customer_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    for (var i = 0; i < values.primaryvaluechain_list.Count; i++)
                    {

                        msGetGid1 = objcmnfunctions.GetMasterGID("CPRI");
                        msSQL = " insert into ocs_mst_tcustomer2primaryvaluechain(" +
                                " customer2primaryvaluechain_gid," +
                                " customer_gid," +
                                " primaryvaluechain_name," +
                                " primaryvaluechain_gid," +
                                " created_by," +
                                " created_date)" +
                                " values(" +
                                "'" + msGetGid1 + "'," +
                                "'" + values.customer_gid + "'," +
                                "'" + values.primaryvaluechain_list[i].valuechain_name + "'," +
                                "'" + values.primaryvaluechain_list[i].valuechain_gid + "'," +
                                "'" + employee_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    for (var i = 0; i < values.secondaryvaluechain_list.Count; i++)
                    {

                        msGetGid1 = objcmnfunctions.GetMasterGID("CSEC");
                        msSQL = " insert into ocs_mst_tcustomer2secondaryvaluechain(" +
                                " customer2secondaryvaluechain_gid," +
                                " customer_gid," +
                                " secondaryvaluechain_name," +
                                " secondaryvaluechain_gid," +
                                " created_by," +
                                " created_date)" +
                                " values(" +
                                "'" + msGetGid1 + "'," +
                                "'" + values.customer_gid + "'," +
                                "'" + values.secondaryvaluechain_list[i].valuechain_name + "'," +
                                "'" + values.secondaryvaluechain_list[i].valuechain_gid + "'," +
                                "'" + employee_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    //--------Customer 2 User info updation--//

                    msSQL = "update ocs_mst_tcustomer2userdtl set customer_gid ='" + values.customer_gid + "' where customer_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "select group_concat(primaryvaluechain_name) as primaryvaluechain from ocs_mst_tcustomer2primaryvaluechain where customer_gid ='" + values.customer_gid + "'";
                    string lsprimaryvaluechain = objdbconn.GetExecuteScalar(msSQL);
                    msSQL = "update ocs_mst_tcustomer set primaryvaluechain_name='" + lsprimaryvaluechain + "'where customer_gid ='" + msGetGid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "select group_concat(secondaryvaluechain_name) as secondaryvaluechain from ocs_mst_tcustomer2secondaryvaluechain where customer_gid ='" + values.customer_gid + "'";
                    string lssecondaryvaluechain = objdbconn.GetExecuteScalar(msSQL);
                    msSQL = "update ocs_mst_tcustomer set secondaryvaluechain_name='" + lssecondaryvaluechain + "'where customer_gid ='" + values.customer_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    //msSQL = " update osd_trn_tbankalert2allocated set " +
                    //                   " transferto_gid='" + values.relationshipMgmtGid + "'," +
                    //                   " transferto_name='" + values.relationshipmgmt_name + "'," +
                    //                   " where customer_gid='" + values.customer_gid + "'";
                    //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    values.message = "Customer Updated Successfully";
                    values.status = true;
                }
                else
                {
                    values.message = "Error Occured While Creating the Customer";
                    values.status = false;

                }
            }
            objODBCDatareader.Close();
        }
        //----------Get Customer 2 User Information------------//
        public void DaEdit2UserDtl(string employee_gid, mdlcustomer2userdtl values,string customer_gid)
        {
            msSQL = " select a.customer2user_name,date_format(a.created_date,'%d-%m-%Y') as created_date,user_type,customer2usertype_gid," +
                    " concat(c.user_firstname, ' ', c.user_lastname, '/', c.user_code) as created_by,customer_type from ocs_mst_tcustomer2userdtl a" +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                    " left  join adm_mst_tuser c on b.user_gid = c.user_gid where " +
                    " (customer_gid='" + employee_gid + "' or customer_gid='" + customer_gid + "')";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcustomer2userdtl_list = new List<customer2userdtl_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcustomer2userdtl_list.Add(new customer2userdtl_list
                    {
                        name = (dr_datarow["customer2user_name"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        user_type = (dr_datarow["user_type"].ToString()),
                        customer2usertype_gid = (dr_datarow["customer2usertype_gid"].ToString()),
                        customer_type = (dr_datarow["customer_type"].ToString()),
                    });
                }
                values.customer2userdtl_list = getcustomer2userdtl_list;
            }
            dt_datatable.Dispose();
        }
        public void DaEditCustomer2UserDtl(string customer_gid,string employee_gid, mdlcustomer2userdtl values)
        {
            msSQL = " select a.customer2user_name,date_format(a.created_date,'%d-%m-%Y') as created_date,user_type,customer2usertype_gid," +
                    " concat(c.user_firstname, ' ', c.user_lastname, '/', c.user_code) as created_by,customer_type from ocs_mst_tcustomer2userdtl a" +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                    " left  join adm_mst_tuser c on b.user_gid = c.user_gid where " +
                    " ( customer_gid='" + employee_gid + "' or  customer_gid='" + customer_gid + "') ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcustomer2userdtl_list = new List<customer2userdtl_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcustomer2userdtl_list.Add(new customer2userdtl_list
                    {
                        name = (dr_datarow["customer2user_name"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        user_type = (dr_datarow["user_type"].ToString()),
                        customer2usertype_gid = (dr_datarow["customer2usertype_gid"].ToString()),
                        customer_type = (dr_datarow["customer_type"].ToString()),
                    });
                }
                values.customer2userdtl_list = getcustomer2userdtl_list;
            }
            dt_datatable.Dispose();
        }
        public void DaGetTempCustomerdetail(MdlCustomer objCustomer)
        {
            try
            {
                msSQL = " select a.tmpcustomer_gid,a.vertical_code,a.customer_code,a.customer_urn,a.customername,a.contactperson, " +
                  " case when a.zonal_head = '' then 'NA' else a.zonal_name end as zonal_head," +
                  " case when a.business_head = '' then 'NA' else a.businesshead_name end as business_head, " +
                  " case when a.cluster_manager_gid = '' then 'NA' else a.cluster_manager_name end as cluster_manager, " +
                  " case when a.relationship_manager = '' then 'NA' else a.relationshipmgmt_name end as relationship_manager, " +
                  " case when a.creditmanager_gid = '' then 'NA' else a.creditmgmt_name end as creditmgmt_name, " +
                  " concat(c.user_firstname,' ',c.user_lastname,'/',c.user_code) as created_by,date_format(a.created_date,'%d-%m-%Y') as created_date" +
                  " from ocs_tmp_tcustomer a " +
                  " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                  " left join adm_mst_tuser c on b.user_gid = c.user_gid order by a.tmpcustomer_gid desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    objCustomer.customer_list = dt_datatable.AsEnumerable().Select(row =>
                    new customer_list
                    {
                        customer_gid = (row["tmpcustomer_gid"].ToString()),
                        customercode = (row["customer_code"].ToString()),
                        customername = (row["customername"].ToString()),
                        contactperson = (row["contactperson"].ToString()),
                        vertical_code = (row["vertical_code"].ToString()),
                        zonalGid = (row["zonal_head"].ToString()),
                        businessHeadGid = (row["business_head"].ToString()),
                        relationshipMgmtGid = (row["relationship_manager"].ToString()),
                        clustermanagerGid = (row["cluster_manager"].ToString()),
                        customer_urn = (row["customer_urn"].ToString()),
                        creditmanagerName = (row["creditmgmt_name"].ToString()),
                        created_by = (row["created_by"].ToString()),
                        created_date = (row["created_date"].ToString())
                    }).ToList();
                    dt_datatable.Dispose();

                }
                objCustomer.status = true;
            }
            catch
            {
                objCustomer.status = false;
            }



        }
        public void DaPostTagCustomer(mdlcreatecustomer objCustomer)
        {
            msSQL = "insert into ocs_mst_tcustomer () (select *from ocs_tmp_tcustomer where tmpcustomer_gid='" + objCustomer.customer_gid + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if(mnResult!=0)
            {
                msSQL = "insert into ocs_mst_tcustomer2userdtl()(select *from ocs_tmp_tcustomer2userdtl where tmpcustomer_gid='" + objCustomer.customer_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "select customer2usertype_gid from ocs_tmp_tcustomer2userdtl  where tmpcustomer_gid='" + objCustomer.customer_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                 if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        msSQL = "insert into ocs_mst_tcustomer2mobileno ()(select* from ocs_tmp_tcustomer2mobileno where"+
                            " customer2usertype_gid='"+ dr_datarow["customer2usertype_gid"].ToString() + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = "insert into ocs_mst_tcustomer2address ()(select* from ocs_tmp_tcustomer2address where" +
                           " customer2usertype_gid='" + dr_datarow["customer2usertype_gid"].ToString() + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = "insert into ocs_mst_tcustomer2identityproof ()(select* from ocs_tmp_tcustomer2identityproof where" +
                           " customer2usertype_gid='" + dr_datarow["customer2usertype_gid"].ToString() + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = "insert into ocs_mst_tcustomer2member ()(select* from ocs_mst_tcustomer2member where" +
                           " customer2usertype_gid='" + dr_datarow["customer2usertype_gid"].ToString() + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                dt_datatable.Dispose();

                msSQL= "delete from ocs_tmp_tcustomer where tmpcustomer_gid='" + objCustomer.customer_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                objCustomer.message = "Customer Added Successfully";
                objCustomer.status = true;
            }
            else
            {
                objCustomer.message = "Error Occured while adding";
                objCustomer.status = false;
            }

        }
        public void DaGetURNInfo(string urn, string employee_gid, mdlcustomer2userdtl values)
        {
            msSQL = "select tmpcustomer_gid from ocs_tmp_tcustomer where customer_urn='" + urn+"'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if(objODBCDatareader .HasRows ==true )
            {
                values.status = true;
            }
            else
            {
                values.status = false;
            }
            objODBCDatareader.Close();
        }
        public void DaGetCustomerdetail(MdlCustomer objCustomer)
        {
            try
            {
                msSQL = "( select a.customer_gid,a.vertical_code,a.customer_code,a.customer_urn,a.customername,a.contactperson,a.customer_type,  " +
                  " case when a.zonal_head = '' then 'NA' else a.zonal_name end as zonal_head," +
                  " case when a.business_head = '' then 'NA' else a.businesshead_name end as business_head, " +
                  " case when a.cluster_manager_gid = '' then 'NA' else a.cluster_manager_name end as cluster_manager, " +
                  " case when a.relationship_manager = '' then 'NA' else a.relationshipmgmt_name end as relationship_manager, " +
                  " case when a.creditmanager_gid = '' then 'NA' else a.creditmgmt_name end as creditmgmt_name, " +
                  " concat(c.user_firstname,' ',c.user_lastname,'/',c.user_code) as created_by,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                  " case when a.legaltag_flag is null then 'N' else a.legaltag_flag end as legaltag_flag, " +
                   " case when a.legaltag_flag is null then 'NA' when a.legaltag_flag='N' then 'UnTagged' else 'Tagged' end as legal_tagging " +
                  " from ocs_mst_tcustomer a " +
                  " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                  " left join adm_mst_tuser c on b.user_gid = c.user_gid" +
                  "  order by a.customer_gid desc ) UNION (" +
                   " select a.tmpcustomer_gid as customer_gid,a.vertical_code,a.customer_code,a.customer_urn,a.customername,a.contactperson,d.customer_type, " +
                  " case when a.zonal_head = '' then 'NA' else a.zonal_name end as zonal_head," +
                  " case when a.business_head = '' then 'NA' else a.businesshead_name end as business_head, " +
                  " case when a.cluster_manager_gid = '' then 'NA' else a.cluster_manager_name end as cluster_manager, " +
                  " case when a.relationship_manager = '' then 'NA' else a.relationshipmgmt_name end as relationship_manager, " +
                  " case when a.creditmanager_gid = '' then 'NA' else a.creditmgmt_name end as creditmgmt_name, " +
                  " concat(c.user_firstname,' ',c.user_lastname,'/',c.user_code) as created_by,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                  " case when a.legaltag_flag is null then 'N' else a.legaltag_flag end as legaltag_flag, " +
                   " case when a.legaltag_flag is null then 'NA' when a.legaltag_flag='N' then 'UnTagged' else 'Tagged' end as legal_tagging " +
                  " from ocs_tmp_tcustomer a " +
                  " left join ocs_mst_tcustomer2userdtl d on a.tmpcustomer_gid = d.customer_gid" +
                  " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                  " left join adm_mst_tuser c on b.user_gid = c.user_gid order by a.tmpcustomer_gid desc) ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    objCustomer.customer_list = dt_datatable.AsEnumerable().Select(row =>
                    new customer_list
                    {
                        customer_gid = (row["customer_gid"].ToString()),
                        customercode = (row["customer_code"].ToString()),
                        customername = (row["customername"].ToString()),
                        contactperson = (row["contactperson"].ToString()),
                        vertical_code = (row["vertical_code"].ToString()),
                        zonalGid = (row["zonal_head"].ToString()),                      
                        businessHeadGid = (row["business_head"].ToString()),
                        relationshipMgmtGid = (row["relationship_manager"].ToString()),
                        clustermanagerGid = (row["cluster_manager"].ToString()),
                        customer_urn = (row["customer_urn"].ToString()),
                        creditmanagerName = (row["creditmgmt_name"].ToString()),
                        created_by = (row["created_by"].ToString()),
                        created_date = (row["created_date"].ToString()),
                        customer_type = (row["customer_type"].ToString())
                    }).ToList();
                    dt_datatable.Dispose();

                }
                objCustomer.status = true;
            }
            catch
            {
                objCustomer.status = false;
            }



        }
        public string childloop(string employee)
        {
            
        msSQL = " select a.employee_gid, concat(b.user_firstname, '-', b.user_code) as user  from adm_mst_tmodule2employee a  " +
                " inner join hrm_mst_temployee c on a.employee_gid = c.employee_gid  " +
                " inner join adm_mst_tuser b on c.user_gid = b.user_gid  " +
                " where a.module_gid in  (select module_gid_parent from adm_mst_tmodule where module_gid in  " +
                " (select modulereportingto_gid from adm_mst_tcompany))  " +
                " and a.employeereporting_to = '" + employee + "'  group by a.employee_gid";
            dt_table = objdbconn.GetDataTable(msSQL);
            foreach (DataRow dr_datarow in dt_table.Rows)
            {
               

                msSQL = " select a.employee_gid, b.user_gid  from adm_mst_tmodule2employee a  " +
                    " inner join hrm_mst_temployee c on a.employee_gid = c.employee_gid  " +
                    " inner join adm_mst_tuser b on c.user_gid = b.user_gid  " +
                    " where a.module_gid in  (select module_gid_parent from adm_mst_tmodule where module_gid in  " +
                    " (select modulereportingto_gid from adm_mst_tcompany)) " +
                    " and a.employee_gid = '" + dr_datarow["employee_gid"].ToString() + "'  group by a.employee_gid";
                objreader = objdbconn.GetDataReader(msSQL);
                if (objreader.HasRows == true)
                {
                    objreader.Read();
                    lsemployeeGID = lsemployeeGID + "'" + objreader["employee_gid"].ToString() + "',";
                }
                objreader.Close();
                childloop(dr_datarow["employee_gid"].ToString());
            }

            dt_table.Dispose();
            return lsemployeeGID;
        }
        public void DaGetMyTeamCustomer(MdlCustomer objCustomer,string employee_gid)
        {
            try
            {
               string lsemployee_gid= childloop(employee_gid);
                if (lsemployee_gid == "" || lsemployee_gid == null)
                {
                    lsemployee_gid = "''";
                }
                else
                { 
                lsemployee_gid = lsemployee_gid.Remove(lsemployee_gid.LastIndexOf(",")) ;
                }
                msSQL = "( select a.customer_gid,a.vertical_code,a.customer_code,a.customer_urn,a.customername,a.contactperson,a.customer_type,  " +
                  " case when a.zonal_head = '' then 'NA' else a.zonal_name end as zonal_head," +
                  " case when a.business_head = '' then 'NA' else a.businesshead_name end as business_head, " +
                  " case when a.cluster_manager_gid = '' then 'NA' else a.cluster_manager_name end as cluster_manager, " +
                  " case when a.relationship_manager = '' then 'NA' else a.relationshipmgmt_name end as relationship_manager, " +
                  " case when a.creditmanager_gid = '' then 'NA' else a.creditmgmt_name end as creditmgmt_name, " +
                  " concat(c.user_firstname,' ',c.user_lastname,'/',c.user_code) as created_by,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                  " case when a.legaltag_flag is null then 'N' else a.legaltag_flag end as legaltag_flag, " +
                  " case when a.legaltag_flag is null then 'NA' when a.legaltag_flag='N' then 'UnTagged' else 'Tagged' end as legal_tagging " +
                  " from ocs_mst_tcustomer a " +
                  " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                  " left join adm_mst_tuser c on b.user_gid = c.user_gid where relationship_manager in ("+ lsemployee_gid + ") " +
                  "  order by a.customer_gid desc ) UNION (" +
                  " select a.tmpcustomer_gid as customer_gid,a.vertical_code,a.customer_code,a.customer_urn,a.customername,a.contactperson,d.customer_type, " +
                  " case when a.zonal_head = '' then 'NA' else a.zonal_name end as zonal_head," +
                  " case when a.business_head = '' then 'NA' else a.businesshead_name end as business_head, " +
                  " case when a.cluster_manager_gid = '' then 'NA' else a.cluster_manager_name end as cluster_manager, " +
                  " case when a.relationship_manager = '' then 'NA' else a.relationshipmgmt_name end as relationship_manager, " +
                  " case when a.creditmanager_gid = '' then 'NA' else a.creditmgmt_name end as creditmgmt_name, " +
                  " concat(c.user_firstname,' ',c.user_lastname,'/',c.user_code) as created_by,date_format(a.created_date,'%d-%m-%Y  %h:%i %p') as created_date," +
                  " case when a.legaltag_flag is null then 'N' else a.legaltag_flag end as legaltag_flag, " +
                  " case when a.legaltag_flag is null then 'NA' when a.legaltag_flag='N' then 'UnTagged' else 'Tagged' end as legal_tagging " +
                  " from ocs_tmp_tcustomer a " +
                  " left join ocs_mst_tcustomer2userdtl d on a.tmpcustomer_gid = d.customer_gid" +
                  " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                  " left join adm_mst_tuser c on b.user_gid = c.user_gid where relationship_manager in (" + lsemployee_gid + ")"+
                  " order by a.tmpcustomer_gid desc) ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    objCustomer.customer_list = dt_datatable.AsEnumerable().Select(row =>
                    new customer_list
                    {
                        customer_gid = (row["customer_gid"].ToString()),
                        customercode = (row["customer_code"].ToString()),
                        customername = (row["customername"].ToString()),
                        contactperson = (row["contactperson"].ToString()),
                        vertical_code = (row["vertical_code"].ToString()),
                        zonalGid = (row["zonal_head"].ToString()),
                        businessHeadGid = (row["business_head"].ToString()),
                        relationshipMgmtGid = (row["relationship_manager"].ToString()),
                        clustermanagerGid = (row["cluster_manager"].ToString()),
                        customer_urn = (row["customer_urn"].ToString()),
                        creditmanagerName = (row["creditmgmt_name"].ToString()),
                        created_by = (row["created_by"].ToString()),
                        created_date = (row["created_date"].ToString()),
                        customer_type = (row["customer_type"].ToString())
                    }).ToList();
                    dt_datatable.Dispose();

                }
                objCustomer.status = true;
            }
            catch(Exception e)
            {
                objCustomer.status = false;
            }
        }
        public void DaGetMyCustomer(MdlCustomer objCustomer, string employee_gid)
        {
            try
            {
                
                msSQL = "( select a.customer_gid,a.vertical_code,a.customer_code,a.customer_urn,a.customername,a.contactperson,a.customer_type,  " +
                  " case when a.zonal_head = '' then 'NA' else a.zonal_name end as zonal_head," +
                  " case when a.business_head = '' then 'NA' else a.businesshead_name end as business_head, " +
                  " case when a.cluster_manager_gid = '' then 'NA' else a.cluster_manager_name end as cluster_manager, " +
                  " case when a.relationship_manager = '' then 'NA' else a.relationshipmgmt_name end as relationship_manager, " +
                  " case when a.creditmanager_gid = '' then 'NA' else a.creditmgmt_name end as creditmgmt_name, " +
                  " concat(c.user_firstname,' ',c.user_lastname,'/',c.user_code) as created_by,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                  " case when a.legaltag_flag is null then 'N' else a.legaltag_flag end as legaltag_flag, " +
                  " case when a.legaltag_flag is null then 'NA' when a.legaltag_flag='N' then 'UnTagged' else 'Tagged' end as legal_tagging " +
                  " from ocs_mst_tcustomer a " +
                  " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                  " left join adm_mst_tuser c on b.user_gid = c.user_gid where  relationship_manager='" + employee_gid + "'" +
                  "  order by a.customer_gid desc ) UNION (" +
                  " select a.tmpcustomer_gid as customer_gid,a.vertical_code,a.customer_code,a.customer_urn,a.customername,a.contactperson,d.customer_type, " +
                  " case when a.zonal_head = '' then 'NA' else a.zonal_name end as zonal_head," +
                  " case when a.business_head = '' then 'NA' else a.businesshead_name end as business_head, " +
                  " case when a.cluster_manager_gid = '' then 'NA' else a.cluster_manager_name end as cluster_manager, " +
                  " case when a.relationship_manager = '' then 'NA' else a.relationshipmgmt_name end as relationship_manager, " +
                  " case when a.creditmanager_gid = '' then 'NA' else a.creditmgmt_name end as creditmgmt_name, " +
                  " concat(c.user_firstname,' ',c.user_lastname,'/',c.user_code) as created_by,date_format(a.created_date,'%d-%m-%Y  %h:%i %p') as created_date," +
                  " case when a.legaltag_flag is null then 'N' else a.legaltag_flag end as legaltag_flag, " +
                  " case when a.legaltag_flag is null then 'NA' when a.legaltag_flag='N' then 'UnTagged' else 'Tagged' end as legal_tagging " +
                  " from ocs_tmp_tcustomer a " +
                  " left join ocs_mst_tcustomer2userdtl d on a.tmpcustomer_gid = d.customer_gid" +
                  " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                  " left join adm_mst_tuser c on b.user_gid = c.user_gid where relationship_manager='" + employee_gid + "'" +
                  " order by a.tmpcustomer_gid desc) ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    objCustomer.customer_list = dt_datatable.AsEnumerable().Select(row =>
                    new customer_list
                    {
                        customer_gid = (row["customer_gid"].ToString()),
                        customercode = (row["customer_code"].ToString()),
                        customername = (row["customername"].ToString()),
                        contactperson = (row["contactperson"].ToString()),
                        vertical_code = (row["vertical_code"].ToString()),
                        zonalGid = (row["zonal_head"].ToString()),
                        businessHeadGid = (row["business_head"].ToString()),
                        relationshipMgmtGid = (row["relationship_manager"].ToString()),
                        clustermanagerGid = (row["cluster_manager"].ToString()),
                        customer_urn = (row["customer_urn"].ToString()),
                        creditmanagerName = (row["creditmgmt_name"].ToString()),
                        created_by = (row["created_by"].ToString()),
                        created_date = (row["created_date"].ToString()),
                        customer_type = (row["customer_type"].ToString())
                    }).ToList();
                    dt_datatable.Dispose();

                }
                objCustomer.status = true;
            }
            catch (Exception e)
            {
                objCustomer.status = false;
            }
        }
    }
}