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
    public class DaExternalVendor
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        OdbcDataReader objODBCDatareader;
        HttpPostedFile httpPostedFile;
        DataTable dt_datatable;
        string msSQL, msGetGid, msGet_documentGid;
        int mnResult;
        double lscontactNumber;
        string lsexternal_vendorname;
        string lscompany_code, path, lspath;
        string lsfile_name, lsfile_path, lsexternalphoto_id;

        public bool DaPostExternalRegistration(string employee_gid, externalvendordtl values)
        {

            msSQL = "select file_name,file_path,externalphoto_id from rsk_tmp_texternalphoto where created_by='" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsfile_name = objODBCDatareader["file_name"].ToString();
                lsfile_path = objcmnstorage.EncryptData(objODBCDatareader["file_path"].ToString());
                lsexternalphoto_id = objODBCDatareader["externalphoto_id"].ToString();
            }
            objODBCDatareader.Close();
            if (lsfile_name == "" || lsfile_name == null)
            {
                lsfile_name = "";
            }
            else if (lsfile_path == "" || lsfile_path == null)
            {
                lsfile_path = "";
            }
            if (values.address_line2 == "" || values.address_line2 == null)
            {
                values.address_line2 = "";
            }
            else
            {
               
            }
            msGetGid = objcmnfunctions.GetMasterGID("EVRE");

            msSQL = "insert into rsk_mst_texternalregistration(" +
                    " externalregister_gid  ," +
                    " external_vendorcode ," +
                    " external_vendorname ," +
                    " contact_person ," +
                    " contact_emailid ," +
                    " contact_number ," +
                    " address_line1 ," +
                    " address_line2 ," +
                    " state_gid ," +
                    " district_gid ," +
                    " country_name ," +
                    " postal_code ," +
                    " photo_name, " +
                    " photo_path," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.external_vendorcode + "'," +
                    "'" + values.external_vendorname.Replace("'", "\\'") + "'," +
                    "'" + values.contact_person + "'," +
                    "'" + values.contact_emailid + "'," +
                    "'" + values.contact_number + "'," +
                    "'" + values.address_line1.Replace("'", "\\'") + "'," +
                    "'" + values.address_line2.Replace("'", "\\'") + "'," +
                    "'" + values.state_gid + "'," +
                    "'" + values.district_gid + "'," +
                    "'" + values.country_name + "'," +
                    "'" + values.postal_code + "'," +
                    "'" + lsfile_name + "'," +
                    "'" + lsfile_path + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msSQL = "delete from rsk_tmp_texternalphoto where externalphoto_id='" + lsexternalphoto_id + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.message = "External Vendor Registered Successfully..!";
                values.status = true;
                return true;
            }
            else
            {
                values.message = "Error Occured..!";
                values.status = false;
                return false;
            }
        }

        public bool DaPostUpdateExternalRegistration(string employee_gid, externalvendordtl values)
        {

            msSQL = "update rsk_mst_texternalregistration set" +
                    " external_vendorcode ='" + values.external_vendorcode + "'," +
                    " external_vendorname ='" + values.external_vendorname.Replace("'", "\\'") + "'," +
                    " contact_person='" + values.contact_person + "'," +
                    " contact_emailid='" + values.contact_emailid + "'," +
                    " contact_number ='" + values.contact_number + "'," +
                    " address_line1 ='" + values.address_line1.Replace("'", "\\'") + "'," +
                    " address_line2 ='" + values.address_line2.Replace("'", "\\'") + "'," +
                    " state_gid ='" + values.state_gid + "'," +
                    " district_gid='" + values.district_gid + "'," +
                    " country_name ='" + values.country_name + "'," +
                    " postal_code='" + values.postal_code + "'," +
                    " updated_by ='" + employee_gid + "'," +
                    " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    " where externalregister_gid='" + values.externalregister_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "External Vendor Details are Updated Successfully..!";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
                return false;
            }
        }

        public bool DaGetExternalRegisterdtl(string externalregister_gid, externalvendordtl values)
        {
            string lsphoto_name = "";
            msSQL = " select a.photo_path,a.photo_name,a.external_vendorcode,a.external_vendorname,a.contact_person,a.contact_emailid, " +
                    " a.contact_number,a.address_line1,a.address_line2,a.state_gid,a.district_gid, " +
                    " a.country_name,b.district_name,b.state_name, " +
                    " a.postal_code,a.external_status from rsk_mst_texternalregistration a " +
                    " left join rsk_mst_tstatedetails b on a.district_gid=b.district_gid" +
                    " where externalregister_gid = '" + externalregister_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.external_vendorcode = objODBCDatareader["external_vendorcode"].ToString();
                values.external_vendorname = objODBCDatareader["external_vendorname"].ToString();
                values.contact_person = objODBCDatareader["contact_person"].ToString();
                values.contact_emailid = objODBCDatareader["contact_emailid"].ToString();
                if (objODBCDatareader["contact_number"].ToString() != "")
                {
                    values.contact_number = Convert.ToDouble(objODBCDatareader["contact_number"].ToString());
                }
                values.address_line1 = objODBCDatareader["address_line1"].ToString();
                values.address_line2 = objODBCDatareader["address_line2"].ToString();
                values.country_name = objODBCDatareader["country_name"].ToString();
                values.district_name = objODBCDatareader["district_name"].ToString();
                values.state_name = objODBCDatareader["state_name"].ToString();
                values.state_gid = objODBCDatareader["state_gid"].ToString();
                values.district_gid = objODBCDatareader["district_gid"].ToString();
                values.postal_code = objODBCDatareader["postal_code"].ToString();
                values.external_status = objODBCDatareader["external_status"].ToString();
                lsphoto_name = objODBCDatareader["photo_name"].ToString(); 
                if (objODBCDatareader["photo_path"].ToString() != "")
                {
                    values.photo_path = HttpContext.Current.Server.MapPath("../../" + objcmnstorage.EncryptData(objODBCDatareader["photo_path"].ToString()));
                }
                else
                {
                    values.photo_path = "N";
                } 
            }
            objODBCDatareader.Close();
            
            return true;
        }

        public bool DaGetexternalRegisterSummary(externalvendorList values)
        {
            msSQL = " select a.externalregister_gid,a.external_vendorcode,a.external_vendorname,a.contact_person,a.contact_emailid, " +
                    " a.contact_number,a.address_line1,a.address_line2,a.country_name,b.district_name,b.state_name, " +
                    " a.postal_code,a.external_status from rsk_mst_texternalregistration a " +
                    " left join rsk_mst_tstatedetails b on a.district_gid=b.district_gid " +
                    " order by a.externalregister_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_externalvendordtl = new List<externalvendordtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    if (dt["contact_number"].ToString() != "")
                    {
                        lscontactNumber = Convert.ToDouble(dt["contact_number"].ToString());
                    }
                    get_externalvendordtl.Add(new externalvendordtl
                    {
                        state_name = dt["state_name"].ToString(),
                        district_name = dt["district_name"].ToString(),
                        externalregister_gid = dt["externalregister_gid"].ToString(),
                        external_vendorcode = dt["external_vendorcode"].ToString(),
                        external_vendorname = dt["external_vendorname"].ToString(),
                        contact_person = dt["contact_person"].ToString(),
                        contact_emailid = dt["contact_emailid"].ToString(),
                        contact_number = lscontactNumber,
                        address_line1 = dt["address_line1"].ToString(),
                        address_line2 = dt["address_line2"].ToString(),
                        country_name = dt["country_name"].ToString(),
                        postal_code = dt["postal_code"].ToString(),
                        external_status = dt["external_status"].ToString(),
                    });
                }
                values.externalvendordtl = get_externalvendordtl;
            }
            dt_datatable.Dispose();
            return true;
        }

        public bool DaGetExternalLoginDtl(string externalregister_gid, externalVendorlogin values)
        {

            msSQL = " select a.external_vendorcode,a.external_vendorname,a.external_userpassword,a.external_status " +
                    " from rsk_mst_texternalregistration a " +
                    " where externalregister_gid = '" + externalregister_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.external_vendorCode = objODBCDatareader["external_vendorcode"].ToString();
                values.external_Vendorname = objODBCDatareader["external_vendorname"].ToString();
                values.external_vendorPassword = objODBCDatareader["external_userpassword"].ToString();
                values.external_activeStatus = objODBCDatareader["external_status"].ToString();
            }
            objODBCDatareader.Close();
            return true;
        }

        public bool DaPostExternalLogin(string employee_gid, externalVendorlogin values)
        {

            msSQL = "select external_vendorname from rsk_mst_texternalregistration where externalregister_gid='" + values.externalregister_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsexternal_vendorname = objODBCDatareader["external_vendorname"].ToString();
            }
            objODBCDatareader.Close();

            msGetGid = objcmnfunctions.GetMasterGID("EVUR");
            msSQL = " insert into rsk_mst_texternaluser( " +
                        " external_usergid," +
                        " external_registerGid ," +
                        " external_usercode," +
                        " external_username," +
                        " external_userpassword ," +
                        " external_userstatus," +
                        " created_by," +
                        " created_date" +
                        " )values(" +
                        "'" + msGetGid + "'," +
                        "'" + values.externalregister_gid + "'," +
                        "'" + values.external_vendorCode + "'," +
                        "'" + lsexternal_vendorname + "'," +
                        "'" + objcmnfunctions.ConvertToAscii(values.external_vendorPassword) + "'," +
                        "'Y'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " update rsk_mst_texternalregistration set external_status = 'Y',external_usergid='" + msGetGid + "', " +
                    " external_userpassword='" + values.external_vendorPassword + "' " +
                    " where externalregister_gid='" + values.externalregister_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "External Vendor Login Activated Successfully..!";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
                return false;
            }
        }

        public bool DaPostExternalLoginStatus(string employee_gid, externalVendorlogin values)
        {

            if (values.external_activeStatus == "N")
            {
                msSQL = " select customer_gid from rsk_trn_tallocationdtl  a " +
                       " left join rsk_mst_texternalregistration b on a.allocate_externalGid = b.external_usergid " +
                       " where b.externalregister_gid = '" + values.externalregister_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.message = " You can't able to Inactive the External ( Customer Allocated For this External )";
                    return false;
                }
                objODBCDatareader.Close();
            }
            msSQL = " update rsk_mst_texternalregistration set external_status = '" + values.external_activeStatus + "'," +
                   " loginstatus_updatedby='" + employee_gid + "'," +
                   " loginstatus_updateddate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                   " where externalregister_gid='" + values.externalregister_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " update rsk_mst_texternaluser set external_userstatus='" + values.external_activeStatus + "' " +
                    " where external_registerGid='" + values.externalregister_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "External Vendor Login Status Updated Successfully..!";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
                return false;
            }
        }

        public bool DaGetTmpExternalPhotoClear(string employee_gid)
        {
            msSQL = "delete from rsk_tmp_texternalphoto where created_by='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            return true;
        }

        public bool DaPostUploadDocument(HttpRequest httpRequest, string employee_gid, externalphoto objfilename)
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

            msSQL = "SELECT company_code from adm_mst_tcompany where 1=1";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objODBCDatareader.Read();
                lscompany_code = objODBCDatareader["company_code"].ToString();
            }
            objODBCDatareader.Close();
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "RSK/ExternalPhoto/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
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
                        if ((FileExtension == ".jpg") || (FileExtension == ".jfif") || (FileExtension == ".jpeg") || (FileExtension == ".png"))
                        {
                            ls_readStream = httpPostedFile.InputStream;
                            MemoryStream ms = new MemoryStream();
                            ls_readStream.CopyTo(ms);
                            //CopyStream(ms, ls_readStream);
                            //lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "RSK/ExternalPhoto/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + lsfile_gid;
                            //FileStream file = new FileStream(lspath, FileMode.Create, FileAccess.Write);
                            //ms.WriteTo(file);
                            //file.Close(); 

                            byte[] bytes = ms.ToArray();
                            if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                            {
                                objfilename.message = "File format is not supported";
                                return false;
                            }

                            bool status;
                            status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "RSK/ExternalPhoto/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                            ms.Close();
                            lspath = "erpdocument" + "/" + lscompany_code + "/" + "RSK/ExternalPhoto/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension;


                            msSQL = " insert into rsk_tmp_texternalphoto( " +
                                    " file_name," +
                                    " file_path," +
                                    " created_by," +
                                    " created_date " +
                                    " )values(" +
                                    "'" + httpPostedFile.FileName + "'," +
                                    "'" + lspath + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            if (mnResult == 1)
                            {
                                objfilename.status = true;
                                msSQL = "select file_name, file_path from rsk_tmp_texternalphoto where created_by='" + employee_gid + "'";
                                dt_datatable = objdbconn.GetDataTable(msSQL);
                                var get_sanctiondocument = new List<Rskexternalvendordoc>();
                                if (dt_datatable.Rows.Count != 0)
                                {
                                    foreach (DataRow dt in dt_datatable.Rows)
                                    {
                                        get_sanctiondocument.Add(new Rskexternalvendordoc
                                        {
                                            file_name = dt["file_name"].ToString(),
                                            file_path = objcmnstorage.EncryptData((dt["file_path"].ToString())),
                                        });
                                        objfilename.Rskexternalvendordoc = get_sanctiondocument;
                                    }
                                }
                                dt_datatable.Dispose();
                            }
                            else
                            {
                                objfilename.status = false;
                                objfilename.message = "File Format Not Supported..!";
                            }
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                dt_datatable.Dispose();
                objfilename.status = false;
            }
            return true;
        }
    }
}