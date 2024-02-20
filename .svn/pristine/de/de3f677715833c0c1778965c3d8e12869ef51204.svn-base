using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Odbc;
using System.Net;
using System.IO;
using ems.utilities.Functions;
using ems.mastersamagro.Models;
using System.Configuration;
using System.Drawing;
using System.Net.Mail;
using ems.storage.Functions;

namespace ems.mastersamagro.DataAccess
{
    /// <summary>
    /// This DataAccess will provide access to various functions in visit report (Visit updates, address, document upload & download events)
    /// </summary>
    /// <remarks>Written by Abilash.A, Premchander.K </remarks>
    public class DaAgrMstApplicationVisitReport
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        OdbcDataReader objODBCDatareader;
        HttpPostedFile httpPostedFile;
        DataTable dt_datatable;
        string msSQL, msGetGid, msGetGid1, lspath;
        int mnresult;
        private string lsins_refno;

        public void DaGetVisitedplace(MdlMstVisitPerson values)
        {
            try
            {
                msSQL = " SELECT visitedplace_gid,visitedplace_name FROM ocs_mst_tvisitedplace order by visitedplace_gid asc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmdlvisitdone = new List<mdlvisitdone>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmdlvisitdone.Add(new mdlvisitdone
                        {
                            visitdone_gid = (dr_datarow["visitedplace_gid"].ToString()),
                            visitdone_name = (dr_datarow["visitedplace_name"].ToString())
                        });
                    }
                    values.mdlvisitdone = getmdlvisitdone;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public bool DaPostVisitContactNo(string employee_gid, mstVisitpersoncontact_list values)
        {
            msSQL = "select primary_status from agr_mst_tapplicationvisitperson2contactno where primary_status='Yes' and applicationvisit2person_gid='" + employee_gid + "'";
            string lsprimary_status = objdbconn.GetExecuteScalar(msSQL);
            if (lsprimary_status == (values.primary_status))
            {

                values.status = false;
                values.message = "Already Primary Mobile Number Added";
                return false;
            }

            msSQL = "select applicationvisitperson2contact_gid,primary_status from agr_mst_tapplicationvisitperson2contactno where mobile_no='" + values.mobile_no + "' " +
                " and applicationvisit2person_gid='" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Mobile Number Already Exist";
                return false;
            }
            else
            {
                objODBCDatareader.Close();
            }

            msGetGid = objcmnfunctions.GetMasterGID("VP2C");
            msSQL = " insert into agr_mst_tapplicationvisitperson2contactno(" +
                    " applicationvisitperson2contact_gid," +
                    " applicationvisit2person_gid," +
                    " mobile_no," +
                    " primary_status," +
                    " whatsapp_mobileno," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + values.mobile_no + "'," +
                    "'" + values.primary_status + "'," +
                    "'" + values.whatsapp_mobileno + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnresult != 0)
            {

                values.status = true;
                values.message = "Mobile Number Added Successfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured";
                return false;
            }
        }

        public bool DaPostPersonDetails(string employee_gid, mstVisitpersondtl_list values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("VR2P");
            msSQL = " insert into agr_mst_tapplicationvisit2person(" +
                    " applicationvisit2person_gid," +
                    " applicationvisit_gid," +
                    " clientrepresentative_name," +
                    " clientrepresentative_designationgid," +
                    " clientrepresentative_designationname," +
                    " personal_mail," +
                    " office_mail, " +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + values.clientrepresentative_name + "'," +
                    "'" + values.clientrepresentative_designationgid + "'," +
                     "'" + values.clientrepresentative_designationname + "'," +
                    "'" + values.clientrepresentative_personalmail + "'," +
                    "'" + values.clientrepresentative_officemail + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
            msSQL = "update agr_mst_tapplicationvisitperson2contactno set applicationvisit2person_gid= '" + msGetGid + "' where applicationvisit2person_gid='" + employee_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnresult != 0)
            {

                values.status = true;
                values.message = "Visited Person Details Added Successfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured";
                return false;
            }
        }

        public bool DaPostVisitAddress(string employee_gid, mstVisitpersonaddress_list values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("VR2A");
            msSQL = " insert into agr_mst_tapplicationvisit2address(" +
                    " applicationvisit2address_gid," +
                    " applicationvisit_gid," +
                    " addresstype_gid," +
                    " addresstype_name," +
                    " primary_status," +
                    " address_line1, " +
                    " address_line2, " +
                    " landmark, " +
                    " postal_code, " +
                    " city, " +
                    " taluk, " +
                    " district, " +
                    " state_name, " +
                    " country, " +
                    " latitude," +
                    " longitude," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + values.addresstype_gid + "'," +
                    "'" + values.addresstype_name + "'," +
                    "'" + values.primary_status + "'," +
                    "'" + values.address_line1 + "'," +
                    "'" + values.address_line2 + "'," +
                    "'" + values.landmark + "'," +
                    "'" + values.postal_code + "'," +
                    "'" + values.city + "'," +
                    "'" + values.taluk + "'," +
                    "'" + values.district + "'," +
                    "'" + values.state_name + "'," +
                    "'" + values.country + "'," +
                    "'" + values.latitude + "'," +
                    "'" + values.longitude + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnresult != 0)
            {

                values.status = true;
                values.message = " Address Details Added Successfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured ";
                return false;
            }
        }

        public void DaPostVisitDocumentUpload(HttpRequest httpRequest, UploadDocumentList objfilename, string user_gid, string employee_gid)
        {
            // upload_list objdocumentmodel = new upload_list();
            HttpFileCollection httpFileCollection;

            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;

            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string pdfFilName = string.Empty;

            string lsdocumenttype_gid = string.Empty;
            string document_name = httpRequest.Form["document_name"];
            String path = lspath;
            string project_flag = httpRequest.Form["project_flag"].ToString();



            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "CSTSamAgro/VisitDocumentUpload/" + DateTime.Now.Year + "/" + DateTime.Now.Month;

            if ((!System.IO.Directory.Exists(path)))
                System.IO.Directory.CreateDirectory(path);

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
                        //string lsfile_gid = msdocument_gid + FileExtension;
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
                            objfilename.status = false;
                            return;
                        }
                        
                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "CSTSamAgro/VisitDocumentUpload/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "CSTSamAgro/VisitDocumentUpload/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        msGetGid = objcmnfunctions.GetMasterGID("VR2D");
                        msSQL = " insert into agr_mst_tapplicationvisit2document( " +
                                    " applicationvisit2document_gid ," +
                                    " applicationvisit_gid," +
                                    " file_name ," +
                                    " document_path," +
                                    " document_name," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + httpPostedFile.FileName + "'," +
                                    "'" + lspath + msdocument_gid + FileExtension + "'," +
                                    "'" + document_name + "'," +
                                    "'" + user_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnresult != 0)
                        {
                            objfilename.status = true;
                            objfilename.message = "Document Uploaded Successfully";

                        }
                        else
                        {
                            objfilename.status = false;
                            objfilename.message = "Error Occured While Uploading the document";

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                objfilename.message = ex.ToString();
            }

        }


        public bool DaPostVisitUploadPhoto(HttpRequest httpRequest, UploadphotoList objfilename, string employee_gid, string user_gid)
        {
            // upload_list objdocumentmodel = new upload_list();
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;

            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string pdfFilName = string.Empty;

            string lsdocumenttype_gid = string.Empty;
            string photo_name = httpRequest.Form["photo_name"];
            String path = lspath;
            string project_flag = httpRequest.Form["project_flag"].ToString();

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            //path = HttpContext.Current.Server.MapPath("../../../erpdocument" + "/" + lscompany_code + "/" + "CSTSamAgro/Visitphotos/" + DateTime.Now.Year + "/" + DateTime.Now.Month);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "CSTSamAgro/Visitphotos/" + DateTime.Now.Year + "/" + DateTime.Now.Month;

            if ((!System.IO.Directory.Exists(path)))
                System.IO.Directory.CreateDirectory(path);

            try
            {
                if (httpRequest.Files.Count > 0)
                {
                    string lsfirstdocument_filepath = string.Empty;
                    httpFileCollection = httpRequest.Files;
                    for (int i = 0; i < httpFileCollection.Count; i++)
                    {
                        string msdocument_gid = objcmnfunctions.GetMasterGID("UPPD");
                        httpPostedFile = httpFileCollection[i];
                        string FileExtension = httpPostedFile.FileName;
                        FileExtension = Path.GetExtension(FileExtension).ToLower();

                        if ((FileExtension == ".jpg") || (FileExtension == ".jpeg") || (FileExtension == ".png") || (FileExtension == ".tif") || (FileExtension == ".tiff") || (FileExtension == ".jfif") || (FileExtension == ".gif"))
                        {

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
                            status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "CSTSamAgro/Visitphotos/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                            ms.Close();
                            lspath = "erpdocument" + "/" + lscompany_code + "/" + "CSTSamAgro/Visitphotos/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";


                            msGetGid = objcmnfunctions.GetMasterGID("VR2P");

                            msSQL = " insert into agr_mst_tapplicationvisit2photo( " +
                                  " applicationvisit2photo_gid ," +
                                  " applicationvisit_gid," +
                                  " file_name," +
                                  " visitphoto_name," +
                                  " visitphoto_path, " +
                                  " created_by ," +
                                  " created_date " +
                                  " )values(" +
                                  "'" + msGetGid + "'," +
                                  "'" + employee_gid + "'," +
                                  "'" + httpPostedFile.FileName + "'," +
                                  "'" + photo_name + "'," +
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
                        
                    }
                    if (mnresult != 0)
                    {
                        objfilename.status = true;
                        objfilename.message = "Photo Uploaded Successfully";
                        return true;
                    }
                    else
                    {
                        objfilename.status = false;
                        objfilename.message = "Error Occured while uploading photo";
                        return false;
                    }
                    //return true;

                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                objfilename.message = ex.ToString();
                return false;
            }

        }


        public void DaGetVisittmpContactList(string employee_gid, mstVisitpersondtl_list values)
        {
            msSQL = "select applicationvisitperson2contact_gid,mobile_no,primary_status,whatsapp_mobileno from agr_mst_tapplicationvisitperson2contactno where " +
              " applicationvisit2person_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstVisitpersoncontact_list = new List<mstVisitpersoncontact_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstVisitpersoncontact_list.Add(new mstVisitpersoncontact_list
                    {
                        applicationvisitperson2contact_gid = (dr_datarow["applicationvisitperson2contact_gid"].ToString()),
                        mobile_no = (dr_datarow["mobile_no"].ToString()),
                        primary_status = (dr_datarow["primary_status"].ToString()),
                        whatsapp_mobileno = (dr_datarow["whatsapp_mobileno"].ToString())
                    });
                }
                values.mstVisitpersoncontact_list = getmstVisitpersoncontact_list;
            }
            dt_datatable.Dispose();
            values.status = true;
        }

        public void DaGetVisitContactList(string employee_gid, string applicationvisit2person_gid, mstVisitpersondtl_list values)
        {
            msSQL = "select applicationvisitperson2contact_gid,mobile_no,primary_status,whatsapp_mobileno from agr_mst_tapplicationvisitperson2contactno where " +
              " applicationvisit2person_gid='" + applicationvisit2person_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstVisitpersoncontact_list = new List<mstVisitpersoncontact_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstVisitpersoncontact_list.Add(new mstVisitpersoncontact_list
                    {
                        applicationvisitperson2contact_gid = (dr_datarow["applicationvisitperson2contact_gid"].ToString()),
                        mobile_no = (dr_datarow["mobile_no"].ToString()),
                        primary_status = (dr_datarow["primary_status"].ToString()),
                        whatsapp_mobileno = (dr_datarow["whatsapp_mobileno"].ToString())
                    });
                }
                values.mstVisitpersoncontact_list = getmstVisitpersoncontact_list;
            }
            dt_datatable.Dispose();
            values.status = true;
        }

        public void DaGetEditVisitContactList(string employee_gid, string applicationvisit2person_gid, mstVisitpersondtl_list values)
        {
            msSQL = "select applicationvisitperson2contact_gid,mobile_no,primary_status,whatsapp_mobileno from agr_mst_tapplicationvisitperson2contactno where " +
              " applicationvisit2person_gid='" + applicationvisit2person_gid + "'  or applicationvisit2person_gid='" + employee_gid + "' order by applicationvisitperson2contact_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstVisitpersoncontact_list = new List<mstVisitpersoncontact_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstVisitpersoncontact_list.Add(new mstVisitpersoncontact_list
                    {
                        applicationvisitperson2contact_gid = (dr_datarow["applicationvisitperson2contact_gid"].ToString()),
                        mobile_no = (dr_datarow["mobile_no"].ToString()),
                        primary_status = (dr_datarow["primary_status"].ToString()),
                        whatsapp_mobileno = (dr_datarow["whatsapp_mobileno"].ToString())
                    });
                }
                values.mstVisitpersoncontact_list = getmstVisitpersoncontact_list;
            }
            dt_datatable.Dispose();
            values.status = true;
        }


        public void DaGetVisittmpPersondtlList(string employee_gid, MdlMstVisitPerson values)
        {
            msSQL = "select applicationvisit2person_gid,clientrepresentative_name,clientrepresentative_designationgid,clientrepresentative_designationname,personal_mail,office_mail from agr_mst_tapplicationvisit2person where " +
              " applicationvisit_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstVisitpersondtl_list = new List<mstVisitpersondtl_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstVisitpersondtl_list.Add(new mstVisitpersondtl_list
                    {
                        applicationvisit2person_gid = (dr_datarow["applicationvisit2person_gid"].ToString()),
                        clientrepresentative_name = (dr_datarow["clientrepresentative_name"].ToString()),
                        clientrepresentative_designationgid = (dr_datarow["clientrepresentative_designationgid"].ToString()),
                        clientrepresentative_designationname = (dr_datarow["clientrepresentative_designationname"].ToString()),
                        clientrepresentative_personalmail = (dr_datarow["personal_mail"].ToString()),
                        clientrepresentative_officemail = (dr_datarow["office_mail"].ToString())
                    });
                }
                values.mstVisitpersondtl_list = getmstVisitpersondtl_list;
            }
            dt_datatable.Dispose();
            values.status = true;
        }

        public void DaGetVisittmpAddressList(string employee_gid, MdlMstVisitPerson values)
        {
            msSQL = "select applicationvisit2address_gid,addresstype_gid,addresstype_name,primary_status,address_line1,address_line2,landmark,postal_code,city,taluk,district,state_name,country,latitude,longitude from agr_mst_tapplicationvisit2address where " +
              " applicationvisit_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstVisitpersonaddress_list = new List<mstVisitpersonaddress_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstVisitpersonaddress_list.Add(new mstVisitpersonaddress_list
                    {
                        applicationvisit2address_gid = (dr_datarow["applicationvisit2address_gid"].ToString()),
                        addresstype_gid = (dr_datarow["addresstype_gid"].ToString()),
                        addresstype_name = (dr_datarow["addresstype_name"].ToString()),
                        primary_status = (dr_datarow["primary_status"].ToString()),
                        address_line1 = (dr_datarow["address_line1"].ToString()),
                        address_line2 = (dr_datarow["address_line2"].ToString()),
                        landmark = (dr_datarow["landmark"].ToString()),
                        postal_code = (dr_datarow["postal_code"].ToString()),
                        city = (dr_datarow["city"].ToString()),
                        taluk = (dr_datarow["taluk"].ToString()),
                        district = (dr_datarow["district"].ToString()),
                        state_name = (dr_datarow["state_name"].ToString()),
                        country = (dr_datarow["country"].ToString()),
                        latitude = (dr_datarow["latitude"].ToString()),
                        longitude = (dr_datarow["longitude"].ToString()),
                    });
                }
                values.mstVisitpersonaddress_list = getmstVisitpersonaddress_list;
            }
            dt_datatable.Dispose();
            values.status = true;
        }

        public void DaGetVisitPersondtlList(string employee_gid, string applicationvisit_gid, MdlMstVisitPerson values)
        {
            msSQL = "select applicationvisit2person_gid,clientrepresentative_name,clientrepresentative_designationgid,clientrepresentative_designationname,personal_mail,office_mail from agr_mst_tapplicationvisit2person where " +
              " applicationvisit_gid='" + applicationvisit_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstVisitpersondtl_list = new List<mstVisitpersondtl_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstVisitpersondtl_list.Add(new mstVisitpersondtl_list
                    {
                        applicationvisit2person_gid = (dr_datarow["applicationvisit2person_gid"].ToString()),
                        clientrepresentative_name = (dr_datarow["clientrepresentative_name"].ToString()),
                        clientrepresentative_designationgid = (dr_datarow["clientrepresentative_designationgid"].ToString()),
                        clientrepresentative_designationname = (dr_datarow["clientrepresentative_designationname"].ToString()),
                        clientrepresentative_personalmail = (dr_datarow["personal_mail"].ToString()),
                        clientrepresentative_officemail = (dr_datarow["office_mail"].ToString())
                    });
                }
                values.mstVisitpersondtl_list = getmstVisitpersondtl_list;
            }
            dt_datatable.Dispose();
            values.status = true;
        }

        public void DaGetEditVisitPersondtlList(string employee_gid, string applicationvisit_gid, MdlMstVisitPerson values)
        {
            msSQL = "select applicationvisit2person_gid,clientrepresentative_name,clientrepresentative_designationgid,clientrepresentative_designationname,personal_mail,office_mail from agr_mst_tapplicationvisit2person where " +
              " applicationvisit_gid='" + applicationvisit_gid + "' or applicationvisit_gid='" + employee_gid + "' order by applicationvisit2person_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstVisitpersondtl_list = new List<mstVisitpersondtl_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstVisitpersondtl_list.Add(new mstVisitpersondtl_list
                    {
                        applicationvisit2person_gid = (dr_datarow["applicationvisit2person_gid"].ToString()),
                        clientrepresentative_name = (dr_datarow["clientrepresentative_name"].ToString()),
                        clientrepresentative_designationgid = (dr_datarow["clientrepresentative_designationgid"].ToString()),
                        clientrepresentative_designationname = (dr_datarow["clientrepresentative_designationname"].ToString()),
                        clientrepresentative_personalmail = (dr_datarow["personal_mail"].ToString()),
                        clientrepresentative_officemail = (dr_datarow["office_mail"].ToString())
                    });
                }
                values.mstVisitpersondtl_list = getmstVisitpersondtl_list;
            }
            dt_datatable.Dispose();
            values.status = true;
        }


        public void DaGetVisitAddressList(string employee_gid, string applicationvisit_gid, MdlMstVisitPerson values)
        {
            msSQL = "select applicationvisit2address_gid,addresstype_gid,addresstype_name,primary_status,address_line1,address_line2,landmark,postal_code,city,taluk,district,state_name,latitude,longitude,country from agr_mst_tapplicationvisit2address where " +
              " applicationvisit_gid='" + applicationvisit_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstVisitpersonaddress_list = new List<mstVisitpersonaddress_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstVisitpersonaddress_list.Add(new mstVisitpersonaddress_list
                    {
                        applicationvisit2address_gid = (dr_datarow["applicationvisit2address_gid"].ToString()),
                        addresstype_gid = (dr_datarow["addresstype_gid"].ToString()),
                        addresstype_name = (dr_datarow["addresstype_name"].ToString()),
                        primary_status = (dr_datarow["primary_status"].ToString()),
                        address_line1 = (dr_datarow["address_line1"].ToString()),
                        address_line2 = (dr_datarow["address_line2"].ToString()),
                        landmark = (dr_datarow["landmark"].ToString()),
                        postal_code = (dr_datarow["postal_code"].ToString()),
                        city = (dr_datarow["city"].ToString()),
                        taluk = (dr_datarow["taluk"].ToString()),
                        district = (dr_datarow["district"].ToString()),
                        state_name = (dr_datarow["state_name"].ToString()),
                        latitude = (dr_datarow["latitude"].ToString()),
                        longitude = (dr_datarow["longitude"].ToString()),
                    });
                }
                values.mstVisitpersonaddress_list = getmstVisitpersonaddress_list;
            }
            dt_datatable.Dispose();
            values.status = true;
        }

        public void DaGetEditVisitAddressList(string employee_gid, string applicationvisit_gid, MdlMstVisitPerson values)
        {
            msSQL = "select applicationvisit2address_gid,addresstype_gid,addresstype_name,primary_status,address_line1,address_line2,landmark,postal_code,city,taluk,district,state_name,latitude,longitude,country from agr_mst_tapplicationvisit2address where " +
              " applicationvisit_gid='" + applicationvisit_gid + "' or applicationvisit_gid='" + employee_gid + "' order by applicationvisit2address_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstVisitpersonaddress_list = new List<mstVisitpersonaddress_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstVisitpersonaddress_list.Add(new mstVisitpersonaddress_list
                    {
                        applicationvisit2address_gid = (dr_datarow["applicationvisit2address_gid"].ToString()),
                        addresstype_gid = (dr_datarow["addresstype_gid"].ToString()),
                        addresstype_name = (dr_datarow["addresstype_name"].ToString()),
                        primary_status = (dr_datarow["primary_status"].ToString()),
                        address_line1 = (dr_datarow["address_line1"].ToString()),
                        address_line2 = (dr_datarow["address_line2"].ToString()),
                        landmark = (dr_datarow["landmark"].ToString()),
                        postal_code = (dr_datarow["postal_code"].ToString()),
                        city = (dr_datarow["city"].ToString()),
                        taluk = (dr_datarow["taluk"].ToString()),
                        district = (dr_datarow["district"].ToString()),
                        state_name = (dr_datarow["state_name"].ToString()),
                        country = (dr_datarow["country"].ToString()),
                        latitude = (dr_datarow["latitude"].ToString()),
                        longitude = (dr_datarow["longitude"].ToString()),
                    });
                }
                values.mstVisitpersonaddress_list = getmstVisitpersonaddress_list;
            }
            dt_datatable.Dispose();
            values.status = true;
        }


        public void DaGetVisittmpDocumentList(string employee_gid, MdlMstVisitPerson values)
        {
            msSQL = "select applicationvisit2document_gid,document_name,document_path,file_name  from agr_mst_tapplicationvisit2document where " +
              " applicationvisit_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getUploadDocumentList = new List<UploadDocumentList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getUploadDocumentList.Add(new UploadDocumentList
                    {
                        applicationvisit2document_gid = (dr_datarow["applicationvisit2document_gid"].ToString()),
                        document_name = (dr_datarow["document_name"].ToString()),
                        document_path = objcmnstorage.EncryptData((dr_datarow["document_path"].ToString())),
                        filename = (dr_datarow["file_name"].ToString())
                    });
                }
                values.UploadDocumentList = getUploadDocumentList;
            }
            dt_datatable.Dispose();
            values.status = true;
        }


        public void DaGetVisitDocumentList(string employee_gid, string applicationvisit_gid, MdlMstVisitPerson values)
        {
            msSQL = "select applicationvisit2document_gid,document_name,document_path,file_name  from agr_mst_tapplicationvisit2document where " +
              " applicationvisit_gid='" + applicationvisit_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getUploadDocumentList = new List<UploadDocumentList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getUploadDocumentList.Add(new UploadDocumentList
                    {
                        applicationvisit2document_gid = (dr_datarow["applicationvisit2document_gid"].ToString()),
                        document_name = (dr_datarow["document_name"].ToString()),
                        document_path = objcmnstorage.EncryptData((dr_datarow["document_path"].ToString())),
                        filename = (dr_datarow["file_name"].ToString())
                    });
                }
                values.UploadDocumentList = getUploadDocumentList;
            }
            dt_datatable.Dispose();
            values.status = true;
        }

        public void DaGetEditVisitDocumentList(string employee_gid, string applicationvisit_gid, MdlMstVisitPerson values)
        {
            msSQL = "select applicationvisit2document_gid,document_name,document_path,file_name  from agr_mst_tapplicationvisit2document where " +
              " applicationvisit_gid='" + applicationvisit_gid + "'  or applicationvisit_gid='" + employee_gid + "' order by applicationvisit2document_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getUploadDocumentList = new List<UploadDocumentList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getUploadDocumentList.Add(new UploadDocumentList
                    {
                        applicationvisit2document_gid = (dr_datarow["applicationvisit2document_gid"].ToString()),
                        document_name = (dr_datarow["document_name"].ToString()),
                        document_path = objcmnstorage.EncryptData((dr_datarow["document_path"].ToString())),
                        filename = (dr_datarow["file_name"].ToString())
                    });
                }
                values.UploadDocumentList = getUploadDocumentList;
            }
            dt_datatable.Dispose();
            values.status = true;
        }

        public void DaGetVisittmpPhotoList(string employee_gid, MdlMstVisitPerson values)
        {
            msSQL = "select applicationvisit2photo_gid,visitphoto_name,visitphoto_path,file_name  from agr_mst_tapplicationvisit2photo where " +
              " applicationvisit_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getUploadphotoList = new List<UploadphotoList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getUploadphotoList.Add(new UploadphotoList
                    {
                        applicationvisit2photo_gid = (dr_datarow["applicationvisit2photo_gid"].ToString()),
                        photo_name = (dr_datarow["visitphoto_name"].ToString()),
                        document_path = objcmnstorage.EncryptData((dr_datarow["visitphoto_path"].ToString())),
                        filename = (dr_datarow["file_name"].ToString())
                    });
                }
                values.UploadphotoList = getUploadphotoList;
            }
            dt_datatable.Dispose();
            values.status = true;
        }


        public void DaGetVisitPhotoList(string employee_gid, string applicationvisit_gid, MdlMstVisitPerson values)
        {
            msSQL = "select applicationvisit2photo_gid,visitphoto_name,visitphoto_path,file_name  from agr_mst_tapplicationvisit2photo where " +
             " applicationvisit_gid='" + applicationvisit_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getUploadphotoList = new List<UploadphotoList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getUploadphotoList.Add(new UploadphotoList
                    {
                        applicationvisit2photo_gid = (dr_datarow["applicationvisit2photo_gid"].ToString()),
                        photo_name = (dr_datarow["visitphoto_name"].ToString()),
                        document_path = objcmnstorage.EncryptData((dr_datarow["visitphoto_path"].ToString())),
                        filename = (dr_datarow["file_name"].ToString())
                    });
                }
                values.UploadphotoList = getUploadphotoList;
            }
            dt_datatable.Dispose();
            values.status = true;
        }

        public void DaGetEditVisitPhotoList(string employee_gid, string applicationvisit_gid, MdlMstVisitPerson values)
        {
            msSQL = "select applicationvisit2photo_gid,visitphoto_name,visitphoto_path,file_name  from agr_mst_tapplicationvisit2photo where " +
             " applicationvisit_gid='" + applicationvisit_gid + "'  or applicationvisit_gid='" + employee_gid + "' order by applicationvisit2photo_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getUploadphotoList = new List<UploadphotoList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getUploadphotoList.Add(new UploadphotoList
                    {
                        applicationvisit2photo_gid = (dr_datarow["applicationvisit2photo_gid"].ToString()),
                        photo_name = (dr_datarow["visitphoto_name"].ToString()),
                        document_path = objcmnstorage.EncryptData((dr_datarow["visitphoto_path"].ToString())),
                        filename = (dr_datarow["file_name"].ToString())
                    });
                }
                values.UploadphotoList = getUploadphotoList;
            }
            dt_datatable.Dispose();
            values.status = true;
        }


        public void DaDeleteVisittmpContactList(string applicationvisitperson2contact_gid, mstVisitpersoncontact_list values)
        {
            msSQL = "delete from agr_mst_tapplicationvisitperson2contactno where applicationvisitperson2contact_gid='" + applicationvisitperson2contact_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnresult != 0)
            {

                values.message = "Visit Contact Details Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }

        public void DaDeleteVisittmppersondtlList(string applicationvisit2person_gid, mstVisitpersondtl_list values)
        {
            msSQL = "select applicationvisitperson2contact_gid from agr_mst_tapplicationvisitperson2contactno where " +
                    " applicationvisit2person_gid='" + applicationvisit2person_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    msSQL = "delete from agr_mst_tapplicationvisitperson2contactno where applicationvisitperson2contact_gid='" + dr_datarow["applicationvisitperson2contact_gid"].ToString() + "'";
                    mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

                }

            }
            dt_datatable.Dispose();
            msSQL = "delete from agr_mst_tapplicationvisit2person where applicationvisit2person_gid='" + applicationvisit2person_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnresult != 0)
            {

                values.message = " Visited Person Details Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured ";
                values.status = false;

            }
        }

        public void DaDeleteVisittmpAddressList(string applicationvisit2address_gid, mstVisitpersonaddress_list values)
        {
            msSQL = "delete from agr_mst_tapplicationvisit2address where applicationvisit2address_gid='" + applicationvisit2address_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnresult != 0)
            {

                values.message = "Address Details Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }

        public void DaDeleteVisittmpDocumentList(string applicationvisit2document_gid, UploadDocumentList values)
        {
            msSQL = "delete from agr_mst_tapplicationvisit2document where applicationvisit2document_gid='" + applicationvisit2document_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnresult != 0)
            {

                values.message = "Document Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured ";
                values.status = false;

            }
        }

        public void DaDeleteVisittmpPhotoList(string applicationvisit2photo_gid, UploadphotoList values)
        {
            msSQL = "delete from agr_mst_tapplicationvisit2photo where applicationvisit2photo_gid='" + applicationvisit2photo_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnresult != 0)
            {

                values.message = "Photo Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }

        public void DaDeleteVisittmpContact(string employee_gid, mstVisitpersoncontact_list values)
        {
            msSQL = "delete from agr_mst_tapplicationvisitperson2contactno where applicationvisit2person_gid='" + employee_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnresult != 0)
            {

                values.message = "Visit Contact Details Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }

        public void DaDeleteVisittmppersondtl(string employee_gid, mstVisitpersondtl_list values)
        {
            msSQL = "select applicationvisit2person_gid from agr_mst_tapplicationvisit2person where " +
                    " applicationvisit_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    msSQL = "delete from agr_mst_tapplicationvisitperson2contactno where applicationvisit2person_gid='" + dr_datarow["applicationvisit2person_gid"].ToString() + "'";
                    mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

                }

            }
            dt_datatable.Dispose();
            msSQL = "delete from agr_mst_tapplicationvisit2person where applicationvisit_gid ='" + employee_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnresult != 0)
            {
                values.message = "Visited Person Details Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }

        public void DaDeleteVisittmpAddress(string employee_gid, mstVisitpersonaddress_list values)
        {
            msSQL = "delete from agr_mst_tapplicationvisit2address where applicationvisit_gid='" + employee_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnresult != 0)
            {

                values.message = "Address Details Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }

        public void DaDeleteVisittmpDocument(string employee_gid, UploadDocumentList values)
        {
            msSQL = "delete from agr_mst_tapplicationvisit2document where applicationvisit_gid='" + employee_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnresult != 0)
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

        public void DaDeleteVisittmpPhoto(string employee_gid, UploadphotoList values)
        {
            msSQL = "delete from agr_mst_tapplicationvisit2photo where applicationvisit_gid='" + employee_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnresult != 0)
            {

                values.message = "Photo Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured ";
                values.status = false;

            }
        }

        public bool DaPostApplicationVisitReport(string employee_gid, string user_gid, MdlMstVisitPerson values)
        {

            if (values.statusupdated_by == "RM")
            {
                lsins_refno = "VSTBYBV" + DateTime.Now.ToString("yyyyMMdd");
                string msGETRef1 = objcmnfunctions.GetMasterGID("VRRM");
                msGETRef1 = msGETRef1.Replace("VRRM", "");
                lsins_refno = lsins_refno + msGETRef1;
            }
            else
            {

                lsins_refno = "VSTBYCV" + DateTime.Now.ToString("yyyyMMdd");
                string msGETRef1 = objcmnfunctions.GetMasterGID("VRCM");
                msGETRef1 = msGETRef1.Replace("VRCM", "");
                lsins_refno = lsins_refno + msGETRef1;
            }

            msGetGid = objcmnfunctions.GetMasterGID("APVR");
            msSQL = " insert into agr_mst_tapplicationvisitreport(" +
                    " applicationvisit_gid," +
                    " visitreport_id," +
                    " application_gid," +
                    " applicationvisit_date," +
                    " clientkmp_activities," +
                    " promoter_background," +
                    " overall_observations," +
                    " inspectingofficial_recommenation," +
                    " trading_relationship," +
                    " summary," +
                    " draft_flag," +
                    " statusupdated_by," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + lsins_refno + "'," +
                    "'" + values.application_gid + "',";
            if (values.applicationvisit_date == null)
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.applicationvisit_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            if (values.clientkmp_activities == null)
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + values.clientkmp_activities.Replace("'", "") + "',";
            }
            if (values.promoter_background == null)
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + values.promoter_background.Replace("'", "") + "',";
            }
            if (values.overall_observations == null)
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + values.overall_observations.Replace("'", "") + "',";
            }

            if (values.inspectingofficial_recommenation == null)
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + values.inspectingofficial_recommenation.Replace("'", "") + "',";
            }
            if (values.trading_relationship == null)
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + values.trading_relationship.Replace("'", "") + "',";
            }
            if (values.summary == null)
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + values.summary.Replace("'", "") + "',";
            }

            msSQL += "'Y'," +
                    "'" + values.statusupdated_by + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (values.mstinspectingofficials == null)
            {

            }
            else
            {
                for (var i = 0; i < values.mstinspectingofficials.Count; i++)
                {
                    msGetGid1 = objcmnfunctions.GetMasterGID("APIO");

                    msSQL = "Insert into agr_mst_tapplicationvisit2inspectingofficial( " +
                           " applicationvisit2inspectingofficial_gid, " +
                           " applicationvisit_gid," +
                           " inspectingofficials_gid," +
                           " inspectingofficials_name," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetGid1 + "'," +
                           "'" + msGetGid + "'," +
                           "'" + values.mstinspectingofficials[i].employee_gid + "'," +
                           "'" + values.mstinspectingofficials[i].employee_name + "'," +
                           "'" + user_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            if (values.mdlvisitdone == null)
            {

            }
            else
            {
                for (var i = 0; i < values.mdlvisitdone.Count; i++)
                {
                    msGetGid1 = objcmnfunctions.GetMasterGID("APVD");

                    msSQL = "Insert into agr_mst_tapplicationvisit2visitdone( " +
                           " applicationvisit2visitdone_gid, " +
                           " applicationvisit_gid," +
                           " visitdone_gid," +
                           " visitdone_name," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetGid1 + "'," +
                           "'" + msGetGid + "'," +
                           "'" + values.mdlvisitdone[i].visitdone_gid + "'," +
                           "'" + values.mdlvisitdone[i].visitdone_name + "'," +
                           "'" + user_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }

            msSQL = "update agr_mst_tapplicationvisit2person set applicationvisit_gid ='" + msGetGid + "' where applicationvisit_gid='" + employee_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update agr_mst_tapplicationvisit2address set applicationvisit_gid ='" + msGetGid + "' where applicationvisit_gid='" + employee_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update agr_mst_tapplicationvisit2document set applicationvisit_gid ='" + msGetGid + "' where applicationvisit_gid='" + employee_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update agr_mst_tapplicationvisit2photo set applicationvisit_gid ='" + msGetGid + "' where applicationvisit_gid='" + employee_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnresult != 0)
            {

                values.status = true;
                values.message = "Visit Report Saved Successfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured ";
                return false;
            }
        }


        public void DaEditApplicationVisitaddress(string applicationvisit2address_gid, mstVisitpersonaddress_list values)
        {
            try
            {
                msSQL = "select applicationvisit2address_gid,addresstype_gid,addresstype_name,primary_status,address_line1,address_line2,latitude, longitude,landmark,postal_code,city,taluk,district,state_name,country from agr_mst_tapplicationvisit2address where " +
                       " applicationvisit2address_gid='" + applicationvisit2address_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.applicationvisit2address_gid = objODBCDatareader["applicationvisit2address_gid"].ToString();
                    values.addresstype_gid = objODBCDatareader["addresstype_gid"].ToString();
                    values.addresstype_name = objODBCDatareader["addresstype_name"].ToString();
                    values.primary_status = objODBCDatareader["primary_status"].ToString();
                    values.address_line1 = objODBCDatareader["address_line1"].ToString();
                    values.address_line2 = objODBCDatareader["address_line2"].ToString();
                    values.landmark = objODBCDatareader["landmark"].ToString();
                    values.postal_code = objODBCDatareader["postal_code"].ToString();
                    values.latitude = objODBCDatareader["latitude"].ToString();
                    values.longitude = objODBCDatareader["longitude"].ToString();
                    values.city = objODBCDatareader["city"].ToString();
                    values.taluk = objODBCDatareader["taluk"].ToString();
                    values.district = objODBCDatareader["district"].ToString();
                    values.state_name = objODBCDatareader["state_name"].ToString();
                    values.country = objODBCDatareader["country"].ToString();
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

        public void DaEditApplicationVisitpersondtl(string applicationvisit2person_gid, mstVisitpersondtl_list values)
        {
            try
            {
                msSQL = "select applicationvisit2person_gid,clientrepresentative_name,clientrepresentative_designationgid,clientrepresentative_designationname," +
                       " personal_mail,office_mail from agr_mst_tapplicationvisit2person where " +
                       " applicationvisit2person_gid='" + applicationvisit2person_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.applicationvisit2person_gid = objODBCDatareader["applicationvisit2person_gid"].ToString();
                    values.clientrepresentative_name = objODBCDatareader["clientrepresentative_name"].ToString();
                    values.clientrepresentative_designationgid = objODBCDatareader["clientrepresentative_designationgid"].ToString();
                    values.clientrepresentative_designationname = objODBCDatareader["clientrepresentative_designationname"].ToString();
                    values.clientrepresentative_personalmail = objODBCDatareader["personal_mail"].ToString();
                    values.clientrepresentative_officemail = objODBCDatareader["office_mail"].ToString();

                    msSQL = "select applicationvisitperson2contact_gid,mobile_no,primary_status,whatsapp_mobileno from agr_mst_tapplicationvisitperson2contactno where " +
                           " applicationvisit2person_gid='" + values.applicationvisit2person_gid + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getmstVisitpersoncontact_list = new List<mstVisitpersoncontact_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dr_datarow in dt_datatable.Rows)
                        {
                            getmstVisitpersoncontact_list.Add(new mstVisitpersoncontact_list
                            {
                                applicationvisitperson2contact_gid = (dr_datarow["applicationvisitperson2contact_gid"].ToString()),
                                mobile_no = (dr_datarow["mobile_no"].ToString()),
                                primary_status = (dr_datarow["primary_status"].ToString()),
                                whatsapp_mobileno = (dr_datarow["whatsapp_mobileno"].ToString())
                            });
                        }
                        values.mstVisitpersoncontact_list = getmstVisitpersoncontact_list;
                    }
                    dt_datatable.Dispose();
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

        public bool DaPostPersonDetailsUpdate(string employee_gid, mstVisitpersondtl_list values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("A2CN");

            msSQL = " update agr_mst_tapplicationvisit2person set clientrepresentative_name='" + values.clientrepresentative_name + "'," +
                   "  clientrepresentative_designationgid='" + values.clientrepresentative_designationgid + "',clientrepresentative_designationname='" + values.clientrepresentative_designationname + "',personal_mail='" + values.clientrepresentative_personalmail + "'," +
                   " office_mail='" + values.clientrepresentative_officemail + "', updated_by='" + employee_gid + "',updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where applicationvisit2person_gid='" + values.applicationvisit2person_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
            msSQL = "update agr_mst_tapplicationvisitperson2contactno set applicationvisit2person_gid= '" + values.applicationvisit2person_gid + "' where applicationvisit2person_gid='" + employee_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnresult != 0)
            {
                values.status = true;
                values.message = "Visited Person Details Updated Successfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured";
                return false;
            }
        }

        public bool DaPostPersonaddressUpdate(string employee_gid, mstVisitpersonaddress_list values)
        {
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
            msSQL = " update agr_mst_tapplicationvisit2address set addresstype_gid='" + values.addresstype_gid + "'," +
                   "  addresstype_name='" + values.addresstype_name + "'," +
                   "  primary_status='" + values.primary_status + "'," +
                   "  address_line1='" + values.address_line1 + "'," +
                   "  address_line2='" + values.address_line2 + "'," +
                   "  landmark='" + values.landmark + "'," +
                   "  postal_code='" + values.postal_code + "'," +
                   "  city='" + values.city + "'," +
                   " district='" + values.district + "'," +
                   " taluk='" + values.taluk + "'," +
                   " state_name='" + values.state_name + "'," +
                   " country='" + values.country + "'," +
                   " latitude='" + values.latitude + "'," +
                   " longitude='" + values.longitude + "'," +
                   " updated_by='" + employee_gid + "',updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where applicationvisit2address_gid='" + values.applicationvisit2address_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnresult != 0)
            {
                values.status = true;
                values.message = "Address Details Updated Successfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured";
                return false;
            }
        }

        public bool DaPostSubmitApplicationVisitReportUpdate(string employee_gid, string user_gid, MdlMstVisitPerson values)
        {
            msSQL = "select applicationvisit_gid from agr_mst_tapplicationvisit2person where applicationvisit_gid ='" + employee_gid + "' or applicationvisit_gid='" + values.applicationvisit_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Person Details are Not Added";
                return false;

            }
            else
            {
                objODBCDatareader.Close();
            }

            msSQL = "select applicationvisit_gid from agr_mst_tapplicationvisit2address where applicationvisit_gid ='" + employee_gid + "' or applicationvisit_gid='" + values.applicationvisit_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Address Details are Not Added";
                return false;

            }
            else
            {
                objODBCDatareader.Close();
            }
            msSQL = "select applicationvisit_gid from agr_mst_tapplicationvisit2document where applicationvisit_gid ='" + employee_gid + "' or applicationvisit_gid='" + values.applicationvisit_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Documents are Not Uploaded";
                return false;

            }
            else
            {
                objODBCDatareader.Close();
            }
            msSQL = "select applicationvisit_gid from agr_mst_tapplicationvisit2photo where applicationvisit_gid ='" + employee_gid + "' or applicationvisit_gid='" + values.applicationvisit_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Photos are Not Uploaded";
                return false;

            }
            else
            {
                objODBCDatareader.Close();
            }
            msSQL = "select date_format(applicationvisit_date,'%d-%m-%Y') as applicationvisit_date from agr_mst_tapplicationvisitreport where applicationvisit_gid='" + values.applicationvisit_gid + "'";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                if (values.applicationvisit_date == (objODBCDatareader["applicationvisit_date"].ToString()))
                {
                    values.applicationvisit_date = "exist";
                }
                objODBCDatareader.Close();

            }
            else
            {
                objODBCDatareader.Close();
            }
            msSQL = "update agr_mst_tapplicationvisitreport set " +
                     " clientkmp_activities='" + values.clientkmp_activities.Replace("'", "") + "'," +
                     " promoter_background='" + values.promoter_background.Replace("'", "") + "'," +
                     " overall_observations='" + values.overall_observations.Replace("'", "") + "'," +
                     " inspectingofficial_recommenation='" + values.inspectingofficial_recommenation.Replace("'", "") + "'," +
                     " trading_relationship='" + values.trading_relationship.Replace("'", "") + "'," +
                     " summary='" + values.summary.Replace("'", "") + "'," +
                     " draft_flag='N'," +
                     " statusupdated_by = '" + values.statusupdated_by + "', ";
            if (values.applicationvisit_date == null)
            {
                msSQL += "applicationvisit_date=null,";
            }
            else if (values.applicationvisit_date == "exist")
            {

            }
            else if (Convert.ToDateTime(values.applicationvisitdate).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
            {

            }
            else
            {
                msSQL += "applicationvisit_date='" + Convert.ToDateTime(values.applicationvisit_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            msSQL += "updated_by='" + employee_gid + "'," +
                     "updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where applicationvisit_gid='" + values.applicationvisit_gid + "' ";

            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
            msSQL = " delete from agr_mst_tapplicationvisit2inspectingofficial where applicationvisit_gid ='" + values.applicationvisit_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnresult != 0)
            {

                for (var i = 0; i < values.mstinspectingofficials.Count; i++)
                {
                    msGetGid1 = objcmnfunctions.GetMasterGID("APIO");

                    msSQL = "Insert into agr_mst_tapplicationvisit2inspectingofficial( " +
                           " applicationvisit2inspectingofficial_gid, " +
                           " applicationvisit_gid," +
                           " inspectingofficials_gid," +
                           " inspectingofficials_name," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetGid1 + "'," +
                           "'" + values.applicationvisit_gid + "'," +
                           "'" + values.mstinspectingofficials[i].employee_gid + "'," +
                           "'" + values.mstinspectingofficials[i].employee_name + "'," +
                           "'" + user_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            msSQL = " delete from agr_mst_tapplicationvisit2visitdone where applicationvisit_gid ='" + values.applicationvisit_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnresult != 0)
            {
                for (var i = 0; i < values.mdlvisitdone.Count; i++)
                {
                    msGetGid1 = objcmnfunctions.GetMasterGID("APVD");

                    msSQL = "Insert into agr_mst_tapplicationvisit2visitdone( " +
                           " applicationvisit2visitdone_gid, " +
                           " applicationvisit_gid," +
                           " visitdone_gid," +
                           " visitdone_name," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetGid1 + "'," +
                           "'" + values.applicationvisit_gid + "'," +
                           "'" + values.mdlvisitdone[i].visitdone_gid + "'," +
                           "'" + values.mdlvisitdone[i].visitdone_name + "'," +
                           "'" + user_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }

            msSQL = "update agr_mst_tapplicationvisit2person set applicationvisit_gid ='" + values.applicationvisit_gid + "' where applicationvisit_gid='" + employee_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update agr_mst_tapplicationvisit2address set applicationvisit_gid ='" + values.applicationvisit_gid + "' where applicationvisit_gid='" + employee_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update agr_mst_tapplicationvisit2document set applicationvisit_gid ='" + values.applicationvisit_gid + "' where applicationvisit_gid='" + employee_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update agr_mst_tapplicationvisit2photo set applicationvisit_gid ='" + values.applicationvisit_gid + "' where applicationvisit_gid='" + employee_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnresult != 0)
            {

                values.status = true;
                values.message = "Visit Report Submitted Successfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured";
                return false;
            }
        }

        public bool DaPostUpdateApplicationVisitReportUpdate(string employee_gid, string user_gid, MdlMstVisitPerson values)
        {
            msSQL = "select applicationvisit_gid from agr_mst_tapplicationvisit2person where applicationvisit_gid ='" + employee_gid + "' or applicationvisit_gid='" + values.applicationvisit_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Person Details are Not Added";
                return false;

            }
            else
            {
                objODBCDatareader.Close();
            }
            msSQL = "select applicationvisit_gid from agr_mst_tapplicationvisit2address where applicationvisit_gid ='" + employee_gid + "' or applicationvisit_gid='" + values.applicationvisit_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Address Details are Not Added";
                return false;

            }
            else
            {
                objODBCDatareader.Close();
            }
            msSQL = "select applicationvisit_gid from agr_mst_tapplicationvisit2document where applicationvisit_gid ='" + employee_gid + "' or applicationvisit_gid='" + values.applicationvisit_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Documents are Not Uploaded";
                return false;

            }
            else
            {
                objODBCDatareader.Close();
            }
            msSQL = "select applicationvisit_gid from agr_mst_tapplicationvisit2photo where applicationvisit_gid ='" + employee_gid + "' or applicationvisit_gid='" + values.applicationvisit_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Photos are Not Uploaded";
                return false;

            }
            else
            {
                objODBCDatareader.Close();
            }
            msSQL = "select date_format(applicationvisit_date,'%d-%m-%Y') as applicationvisit_date from agr_mst_tapplicationvisitreport where applicationvisit_gid='" + values.applicationvisit_gid + "'";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                if (values.applicationvisit_date == (objODBCDatareader["applicationvisit_date"].ToString()))
                {
                    values.applicationvisit_date = "exist";
                }
                objODBCDatareader.Close();

            }
            else
            {
                objODBCDatareader.Close();
            }
            msSQL = "update agr_mst_tapplicationvisitreport set " +
                     " clientkmp_activities='" + values.clientkmp_activities.Replace("'", "") + "'," +
                     " promoter_background='" + values.promoter_background.Replace("'", "") + "'," +
                     " overall_observations='" + values.overall_observations.Replace("'", "") + "'," +
                     " inspectingofficial_recommenation='" + values.inspectingofficial_recommenation.Replace("'", "") + "'," +
                     " trading_relationship='" + values.trading_relationship.Replace("'", "") + "'," +
                     " summary='" + values.summary.Replace("'", "") + "'," +
                     " draft_flag='N'," +
                     " statusupdated_by = '" + values.statusupdated_by + "', ";
            if (values.applicationvisit_date == null)
            {
                msSQL += "applicationvisit_date=null,";
            }
            else if (values.applicationvisit_date == "exist")
            {

            }
            else if (Convert.ToDateTime(values.applicationvisitdate).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
            {

            }
            else
            {
                msSQL += "applicationvisit_date='" + Convert.ToDateTime(values.applicationvisit_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            msSQL += "updated_by='" + employee_gid + "'," +
                     "updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where applicationvisit_gid='" + values.applicationvisit_gid + "' ";

            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
            msSQL = " delete from agr_mst_tapplicationvisit2inspectingofficial where applicationvisit_gid ='" + values.applicationvisit_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnresult != 0)
            {

                for (var i = 0; i < values.mstinspectingofficials.Count; i++)
                {
                    msGetGid1 = objcmnfunctions.GetMasterGID("APIO");

                    msSQL = "Insert into agr_mst_tapplicationvisit2inspectingofficial( " +
                           " applicationvisit2inspectingofficial_gid, " +
                           " applicationvisit_gid," +
                           " inspectingofficials_gid," +
                           " inspectingofficials_name," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetGid1 + "'," +
                           "'" + values.applicationvisit_gid + "'," +
                           "'" + values.mstinspectingofficials[i].employee_gid + "'," +
                           "'" + values.mstinspectingofficials[i].employee_name + "'," +
                           "'" + user_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            msSQL = " delete from agr_mst_tapplicationvisit2visitdone where applicationvisit_gid ='" + values.applicationvisit_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnresult != 0)
            {
                for (var i = 0; i < values.mdlvisitdone.Count; i++)
                {
                    msGetGid1 = objcmnfunctions.GetMasterGID("APVD");

                    msSQL = "Insert into agr_mst_tapplicationvisit2visitdone( " +
                           " applicationvisit2visitdone_gid, " +
                           " applicationvisit_gid," +
                           " visitdone_gid," +
                           " visitdone_name," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetGid1 + "'," +
                           "'" + values.applicationvisit_gid + "'," +
                           "'" + values.mdlvisitdone[i].visitdone_gid + "'," +
                           "'" + values.mdlvisitdone[i].visitdone_name + "'," +
                           "'" + user_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }

            msSQL = "update agr_mst_tapplicationvisit2person set applicationvisit_gid ='" + values.applicationvisit_gid + "' where applicationvisit_gid='" + employee_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update agr_mst_tapplicationvisit2address set applicationvisit_gid ='" + values.applicationvisit_gid + "' where applicationvisit_gid='" + employee_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update agr_mst_tapplicationvisit2document set applicationvisit_gid ='" + values.applicationvisit_gid + "' where applicationvisit_gid='" + employee_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update agr_mst_tapplicationvisit2photo set applicationvisit_gid ='" + values.applicationvisit_gid + "' where applicationvisit_gid='" + employee_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnresult != 0)
            {

                values.status = true;
                values.message = "Visit Report Updated Successfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured";
                return false;
            }
        }


        public void DaGetVisitReportList(string application_gid, string statusupdated_by, MdlMstVisitPerson values)
        {
            msSQL = "select visitreport_id, concat(c.user_firstname,' ',c.user_lastname,'/',c.user_code) as created_by,draft_flag, date_format(a.created_date,'%d-%m-%Y %H:%m:%s') as created_date, date_format(a.applicationvisit_date,'%d-%m-%Y') as applicationvisit_date,a.applicationvisit_gid  from agr_mst_tapplicationvisitreport a " +
                " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                 " left join  adm_mst_tuser c on c.user_gid=b.user_gid " +
                " where application_gid='" + application_gid + "' and a.statusupdated_by='" + statusupdated_by + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getVisitReportList = new List<VisitReportList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getVisitReportList.Add(new VisitReportList
                    {
                        draft_flag = (dr_datarow["draft_flag"]).ToString(),
                        visitreport_gid = (dr_datarow["applicationvisit_gid"].ToString()),
                        visitreport_date = (dr_datarow["applicationvisit_date"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        visitreport_id = (dr_datarow["visitreport_id"].ToString()),
                    });
                }
                values.VisitReportList = getVisitReportList;
            }
            dt_datatable.Dispose();
            values.status = true;
        }


        public void DaEditApplicationVisitReport(string applicationvisit_gid, MdlMstVisitPerson values)
        {
            try
            {
                msSQL = "select applicationvisit_gid,visitreport_id,date_format(applicationvisit_date,'%d-%m-%Y') as applicationvisit_date,clientkmp_activities," +
                    " promoter_background,overall_observations,inspectingofficial_recommenation,trading_relationship," +
                    " summary,draft_flag from agr_mst_tapplicationvisitreport where " +
                       " applicationvisit_gid='" + applicationvisit_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.visitreport_id = objODBCDatareader["visitreport_id"].ToString();
                    values.applicationvisit_gid = objODBCDatareader["applicationvisit_gid"].ToString();
                    values.applicationvisit_date = objODBCDatareader["applicationvisit_date"].ToString();
                    values.clientkmp_activities = objODBCDatareader["clientkmp_activities"].ToString();
                    values.promoter_background = objODBCDatareader["promoter_background"].ToString();
                    values.overall_observations = objODBCDatareader["overall_observations"].ToString();
                    values.inspectingofficial_recommenation = objODBCDatareader["inspectingofficial_recommenation"].ToString();
                    values.trading_relationship = objODBCDatareader["trading_relationship"].ToString();
                    values.summary = objODBCDatareader["summary"].ToString();
                    values.draft_flag = objODBCDatareader["draft_flag"].ToString();
                    msSQL = " select inspectingofficials_gid,inspectingofficials_name,applicationvisit2inspectingofficial_gid from agr_mst_tapplicationvisit2inspectingofficial " +
                    " where applicationvisit_gid='" + applicationvisit_gid + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getmstinspectingofficials = new List<mstinspectingofficials>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            getmstinspectingofficials.Add(new mstinspectingofficials
                            {
                                employee_gid = dt["inspectingofficials_gid"].ToString(),
                                employee_name = dt["inspectingofficials_name"].ToString(),
                                applicationvisit2inspectingofficial_gid = dt["applicationvisit2inspectingofficial_gid"].ToString(),
                            });
                            values.mstinspectingofficials = getmstinspectingofficials;
                        }
                    }
                    dt_datatable.Dispose();


                    msSQL = " select visitdone_name,visitdone_gid,applicationvisit2visitdone_gid from agr_mst_tapplicationvisit2visitdone " +
                 " where applicationvisit_gid='" + applicationvisit_gid + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getvisitdone_list = new List<visitdone_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            getvisitdone_list.Add(new visitdone_list
                            {
                                visitdone_gid = dt["visitdone_gid"].ToString(),
                                visitdone_name = dt["visitdone_name"].ToString(),
                                applicationvisit2visitdone_gid = dt["applicationvisit2visitdone_gid"].ToString(),
                            });
                            values.visitdone_list = getvisitdone_list;
                        }
                    }
                    dt_datatable.Dispose();

                    msSQL = " SELECT a.user_firstname,a.user_gid ,concat(a.user_firstname,' ',a.user_lastname,' / ',a.user_code) as employee_name, " +
                               " b.employee_gid from adm_mst_tuser a " +
                            " LEFT JOIN hrm_mst_temployee b ON a.user_gid=b.user_gid " +
                            " where user_status<>'N' order by a.user_firstname asc";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getemployeeList = new List<inspectemployee_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            getemployeeList.Add(new inspectemployee_list
                            {
                                employee_gid = dt["employee_gid"].ToString(),
                                employee_name = dt["employee_name"].ToString(),
                            });
                            values.employeelist = getemployeeList;
                        }
                    }
                    dt_datatable.Dispose();

                    msSQL = " SELECT visitedplace_gid,visitedplace_name FROM ocs_mst_tvisitedplace order by visitedplace_gid asc ";

                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getmdlvisitdone = new List<mdlvisitdone>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dr_datarow in dt_datatable.Rows)
                        {
                            getmdlvisitdone.Add(new mdlvisitdone
                            {
                                visitdone_gid = (dr_datarow["visitedplace_gid"].ToString()),
                                visitdone_name = (dr_datarow["visitedplace_name"].ToString())
                            });
                        }
                        values.mdlvisitdone = getmdlvisitdone;
                    }
                    dt_datatable.Dispose();
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


        public bool DaPostSubmitApplicationVisitReport(string employee_gid, string user_gid, MdlMstVisitPerson values)
        {
            msSQL = "select applicationvisit_gid from agr_mst_tapplicationvisit2person where applicationvisit_gid ='" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Person Details are Not Added";
                return false;

            }
            else
            {
                objODBCDatareader.Close();
            }
            msSQL = "select applicationvisit_gid from agr_mst_tapplicationvisit2address where applicationvisit_gid ='" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Address Details are Not Added";
                return false;

            }
            else
            {
                objODBCDatareader.Close();
            }
            msSQL = "select applicationvisit_gid from agr_mst_tapplicationvisit2document where applicationvisit_gid ='" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Documents are Not Uploaded";
                return false;

            }
            else
            {
                objODBCDatareader.Close();
            }
            msSQL = "select applicationvisit_gid from agr_mst_tapplicationvisit2photo where applicationvisit_gid ='" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Photos are Not Uploaded";
                return false;

            }
            else
            {
                objODBCDatareader.Close();
            }

            if (values.statusupdated_by == "RM")
            {
                lsins_refno = "VSTBYBV" + DateTime.Now.ToString("yyyyMMdd");
                string msGETRef1 = objcmnfunctions.GetMasterGID("VRRM");
                msGETRef1 = msGETRef1.Replace("VRRM", "");
                lsins_refno = lsins_refno + msGETRef1;
            }
            else
            {

                lsins_refno = "VSTBYCV" + DateTime.Now.ToString("yyyyMMdd");
                string msGETRef1 = objcmnfunctions.GetMasterGID("VRCM");
                msGETRef1 = msGETRef1.Replace("VRCM", "");
                lsins_refno = lsins_refno + msGETRef1;
            }

            msGetGid = objcmnfunctions.GetMasterGID("APVR");
            msSQL = " insert into agr_mst_tapplicationvisitreport(" +
                    " applicationvisit_gid," +
                    " visitreport_id," +
                    " application_gid," +
                    " applicationvisit_date," +
                    " clientkmp_activities," +
                    " promoter_background," +
                    " overall_observations," +
                    " inspectingofficial_recommenation," +
                    " trading_relationship," +
                    " summary," +
                    " draft_flag," +
                    " statusupdated_by," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                     "'" + lsins_refno + "'," +
                    "'" + values.application_gid + "',";
            if (values.applicationvisit_date == null)
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.applicationvisit_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            msSQL += "'" + values.clientkmp_activities.Replace("'", "") + "'," +
                    "'" + values.promoter_background.Replace("'", "") + "'," +
                    "'" + values.overall_observations.Replace("'", "") + "'," +
                    "'" + values.inspectingofficial_recommenation.Replace("'", "") + "'," +
                    "'" + values.trading_relationship.Replace("'", "") + "'," +
                    "'" + values.summary.Replace("'", "") + "'," +
                    "'N'," +
                    "'" + values.statusupdated_by + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

            for (var i = 0; i < values.mstinspectingofficials.Count; i++)
            {
                msGetGid1 = objcmnfunctions.GetMasterGID("APIO");

                msSQL = "Insert into agr_mst_tapplicationvisit2inspectingofficial( " +
                       " applicationvisit2inspectingofficial_gid, " +
                       " applicationvisit_gid," +
                       " inspectingofficials_gid," +
                       " inspectingofficials_name," +
                       " created_by," +
                       " created_date)" +
                       " values(" +
                       "'" + msGetGid1 + "'," +
                       "'" + msGetGid + "'," +
                       "'" + values.mstinspectingofficials[i].employee_gid + "'," +
                       "'" + values.mstinspectingofficials[i].employee_name + "'," +
                       "'" + user_gid + "'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            for (var i = 0; i < values.mdlvisitdone.Count; i++)
            {
                msGetGid1 = objcmnfunctions.GetMasterGID("APVD");

                msSQL = "Insert into agr_mst_tapplicationvisit2visitdone( " +
                       " applicationvisit2visitdone_gid, " +
                       " applicationvisit_gid," +
                       " visitdone_gid," +
                       " visitdone_name," +
                       " created_by," +
                       " created_date)" +
                       " values(" +
                       "'" + msGetGid1 + "'," +
                       "'" + msGetGid + "'," +
                       "'" + values.mdlvisitdone[i].visitdone_gid + "'," +
                       "'" + values.mdlvisitdone[i].visitdone_name + "'," +
                       "'" + user_gid + "'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            msSQL = "update agr_mst_tapplicationvisit2person set applicationvisit_gid ='" + msGetGid + "' where applicationvisit_gid='" + employee_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update agr_mst_tapplicationvisit2address set applicationvisit_gid ='" + msGetGid + "' where applicationvisit_gid='" + employee_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update agr_mst_tapplicationvisit2document set applicationvisit_gid ='" + msGetGid + "' where applicationvisit_gid='" + employee_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update agr_mst_tapplicationvisit2photo set applicationvisit_gid ='" + msGetGid + "' where applicationvisit_gid='" + employee_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnresult != 0)
            {

                values.status = true;
                values.message = "Visit Report Added Successfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured ";
                return false;
            }
        }

        public bool DaPostSaveApplicationVisitReportUpdate(string employee_gid, string user_gid, MdlMstVisitPerson values)
        {
            msSQL = "select date_format(applicationvisit_date,'%d-%m-%Y') as applicationvisit_date from agr_mst_tapplicationvisitreport where applicationvisit_gid='" + values.applicationvisit_gid + "'";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                if (values.applicationvisit_date == (objODBCDatareader["applicationvisit_date"].ToString()))
                {
                    values.applicationvisit_date = "exist";
                }
                objODBCDatareader.Close();

            }
            else
            {
                objODBCDatareader.Close();
            }
            msSQL = "update agr_mst_tapplicationvisitreport set " +
                     " draft_flag='Y'," +
                     " statusupdated_by = '" + values.statusupdated_by + "', ";
            if (values.clientkmp_activities == null)
            {
                msSQL += "clientkmp_activities=null,";
            }
            else
            {
                msSQL += "clientkmp_activities='" + values.clientkmp_activities.Replace("'", "") + "',";
            }
            if (values.promoter_background == null)
            {
                msSQL += "promoter_background=null,";
            }
            else
            {
                msSQL += "promoter_background='" + values.promoter_background.Replace("'", "") + "',";
            }
            if (values.overall_observations == null)
            {
                msSQL += "overall_observations=null,";
            }
            else
            {
                msSQL += "overall_observations='" + values.overall_observations.Replace("'", "") + "',";
            }
            if (values.inspectingofficial_recommenation == null)
            {
                msSQL += "inspectingofficial_recommenation=null,";
            }
            else
            {
                msSQL += "inspectingofficial_recommenation='" + values.inspectingofficial_recommenation.Replace("'", "") + "',";
            }
            if (values.trading_relationship == null)
            {
                msSQL += "trading_relationship=null,";
            }
            else
            {
                msSQL += "trading_relationship='" + values.trading_relationship.Replace("'", "") + "',";
            }
            if (values.summary == null)
            {
                msSQL += "summary=null,";
            }
            else
            {
                msSQL += "summary='" + values.summary.Replace("'", "") + "',";
            }
            if (values.applicationvisit_date == null || values.applicationvisit_date == "")
            {
                msSQL += "applicationvisit_date=null,";
            }
            else if (values.applicationvisit_date == "exist")
            {

            }
            else if (Convert.ToDateTime(values.applicationvisitdate).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
            {

            }
            else
            {
                msSQL += "applicationvisit_date='" + Convert.ToDateTime(values.applicationvisit_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            msSQL += "updated_by='" + employee_gid + "'," +
                     "updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where applicationvisit_gid='" + values.applicationvisit_gid + "' ";

            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
            msSQL = " delete from agr_mst_tapplicationvisit2inspectingofficial where applicationvisit_gid ='" + values.applicationvisit_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnresult != 0)
            {

                for (var i = 0; i < values.mstinspectingofficials.Count; i++)
                {
                    msGetGid1 = objcmnfunctions.GetMasterGID("APIO");

                    msSQL = "Insert into agr_mst_tapplicationvisit2inspectingofficial( " +
                           " applicationvisit2inspectingofficial_gid, " +
                           " applicationvisit_gid," +
                           " inspectingofficials_gid," +
                           " inspectingofficials_name," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetGid1 + "'," +
                           "'" + values.applicationvisit_gid + "'," +
                           "'" + values.mstinspectingofficials[i].employee_gid + "'," +
                           "'" + values.mstinspectingofficials[i].employee_name + "'," +
                           "'" + user_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            msSQL = " delete from agr_mst_tapplicationvisit2visitdone where applicationvisit_gid ='" + values.applicationvisit_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnresult != 0)
            {
                for (var i = 0; i < values.mdlvisitdone.Count; i++)
                {
                    msGetGid1 = objcmnfunctions.GetMasterGID("APVD");

                    msSQL = "Insert into agr_mst_tapplicationvisit2visitdone( " +
                           " applicationvisit2visitdone_gid, " +
                           " applicationvisit_gid," +
                           " visitdone_gid," +
                           " visitdone_name," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetGid1 + "'," +
                           "'" + values.applicationvisit_gid + "'," +
                           "'" + values.mdlvisitdone[i].visitdone_gid + "'," +
                           "'" + values.mdlvisitdone[i].visitdone_name + "'," +
                           "'" + user_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }

            msSQL = "update agr_mst_tapplicationvisit2person set applicationvisit_gid ='" + values.applicationvisit_gid + "' where applicationvisit_gid='" + employee_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update agr_mst_tapplicationvisit2address set applicationvisit_gid ='" + values.applicationvisit_gid + "' where applicationvisit_gid='" + employee_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update agr_mst_tapplicationvisit2document set applicationvisit_gid ='" + values.applicationvisit_gid + "' where applicationvisit_gid='" + employee_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update agr_mst_tapplicationvisit2photo set applicationvisit_gid ='" + values.applicationvisit_gid + "' where applicationvisit_gid='" + employee_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnresult != 0)
            {

                values.status = true;
                values.message = "Visit Report Saved Successfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured";
                return false;
            }
        }


        public void DaGetVisitReportDtls(string visitreport_gid, MdlMstVisitPersonView values)
        {
            try
            {
                msSQL = " select date_format(a.applicationvisit_date,'%d-%m-%Y') as applicationvisit_date, a.clientkmp_activities, a.promoter_background, a.visitreport_id, " +
               " a.overall_observations, a.inspectingofficial_recommenation, a.trading_relationship, summary, GROUP_CONCAT(distinct(b.inspectingofficials_name) SEPARATOR ', ') as inspectingofficials_name, " +
               " GROUP_CONCAT(distinct(c.visitdone_name) SEPARATOR ', ') as visitdone_name " +
               " from agr_mst_tapplicationvisitreport a " +
               " left join agr_mst_tapplicationvisit2inspectingofficial b on a.applicationvisit_gid = b.applicationvisit_gid" +
               " left join agr_mst_tapplicationvisit2visitdone c on c.applicationvisit_gid = a.applicationvisit_gid " +
               " where a.applicationvisit_gid='" + visitreport_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.visitreport_id = objODBCDatareader["visitreport_id"].ToString();
                    values.applicationvisit_date = objODBCDatareader["applicationvisit_date"].ToString();
                    values.inspectingofficials_name = objODBCDatareader["inspectingofficials_name"].ToString();
                    values.visitdone_name = objODBCDatareader["visitdone_name"].ToString();
                    values.clientkmp_activities = objODBCDatareader["clientkmp_activities"].ToString();
                    values.promoter_background = objODBCDatareader["promoter_background"].ToString();
                    values.overall_observations = objODBCDatareader["overall_observations"].ToString();
                    values.inspectingofficial_recommenation = objODBCDatareader["inspectingofficial_recommenation"].ToString();
                    values.trading_relationship = objODBCDatareader["trading_relationship"].ToString();
                    values.summary = objODBCDatareader["summary"].ToString();

                }
                objODBCDatareader.Close();

                msSQL = "select applicationvisit2person_gid,clientrepresentative_name,clientrepresentative_designationname,personal_mail,office_mail from agr_mst_tapplicationvisit2person where " +
                        " applicationvisit_gid='" + visitreport_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmstVisitpersondtl_list = new List<mstVisitpersondtl_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmstVisitpersondtl_list.Add(new mstVisitpersondtl_list
                        {
                            applicationvisit2person_gid = (dr_datarow["applicationvisit2person_gid"].ToString()),
                            clientrepresentative_name = (dr_datarow["clientrepresentative_name"].ToString()),
                            clientrepresentative_designationname = (dr_datarow["clientrepresentative_designationname"].ToString()),
                            clientrepresentative_personalmail = (dr_datarow["personal_mail"].ToString()),
                            clientrepresentative_officemail = (dr_datarow["office_mail"].ToString())
                        });
                    }
                    values.mstVisitpersondtl_list = getmstVisitpersondtl_list;
                }
                dt_datatable.Dispose();


                msSQL = "select applicationvisit2address_gid,addresstype_gid,addresstype_name,primary_status,address_line1,address_line2,landmark,postal_code,city,taluk,district,state_gid,state_name,latitude,longitude,country from agr_mst_tapplicationvisit2address where " +
                        " applicationvisit_gid='" + visitreport_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmstVisitpersonaddress_list = new List<mstVisitpersonaddress_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmstVisitpersonaddress_list.Add(new mstVisitpersonaddress_list
                        {
                            applicationvisit2address_gid = (dr_datarow["applicationvisit2address_gid"].ToString()),
                            addresstype_gid = (dr_datarow["addresstype_gid"].ToString()),
                            addresstype_name = (dr_datarow["addresstype_name"].ToString()),
                            primary_status = (dr_datarow["primary_status"].ToString()),
                            address_line1 = (dr_datarow["address_line1"].ToString()),
                            address_line2 = (dr_datarow["address_line2"].ToString()),
                            landmark = (dr_datarow["landmark"].ToString()),
                            postal_code = (dr_datarow["postal_code"].ToString()),
                            city = (dr_datarow["city"].ToString()),
                            taluk = (dr_datarow["taluk"].ToString()),
                            district = (dr_datarow["district"].ToString()),
                            state_name = (dr_datarow["state_name"].ToString()),
                            country = (dr_datarow["country"].ToString()),
                            latitude = (dr_datarow["latitude"].ToString()),
                            longitude = (dr_datarow["longitude"].ToString()),
                        });
                    }
                    values.mstVisitpersonaddress_list = getmstVisitpersonaddress_list;
                }
                dt_datatable.Dispose();


                msSQL = "select applicationvisit2document_gid,document_name,document_path,file_name,date_format(created_date,'%d-%m-%Y') as created_date  from agr_mst_tapplicationvisit2document where " +
                        " applicationvisit_gid='" + visitreport_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getUploadDocumentList = new List<UploadDocumentList>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getUploadDocumentList.Add(new UploadDocumentList
                        {
                            applicationvisit2document_gid = (dr_datarow["applicationvisit2document_gid"].ToString()),
                            document_name = (dr_datarow["document_name"].ToString()),
                            document_path = objcmnstorage.EncryptData((dr_datarow["document_path"].ToString())),
                            filename = (dr_datarow["file_name"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString())
                        });
                    }
                    values.UploadDocumentList = getUploadDocumentList;
                }
                dt_datatable.Dispose();


                msSQL = "select applicationvisit2photo_gid,visitphoto_name,visitphoto_path,file_name,date_format(created_date,'%d-%m-%Y') as created_date  from agr_mst_tapplicationvisit2photo where " +
                        " applicationvisit_gid='" + visitreport_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getUploadphotoList = new List<UploadphotoList>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getUploadphotoList.Add(new UploadphotoList
                        {
                            applicationvisit2photo_gid = (dr_datarow["applicationvisit2photo_gid"].ToString()),
                            photo_name = (dr_datarow["visitphoto_name"].ToString()),
                            document_path = objcmnstorage.EncryptData((dr_datarow["visitphoto_path"].ToString())),
                            filename = (dr_datarow["file_name"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString())
                        });
                    }
                    values.UploadphotoList = getUploadphotoList;
                }
                dt_datatable.Dispose();

            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }

        }
    }
}