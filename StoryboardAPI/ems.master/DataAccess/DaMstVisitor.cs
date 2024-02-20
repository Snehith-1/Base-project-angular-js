using ems.master.Models;
using ems.utilities.Functions;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using ems.storage.Functions;

namespace ems.master.DataAccess
{
    public class DaMstVisitor
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msSQL, msGetGid,msGetGid1;
        int mnResult;
        HttpPostedFile httpPostedFile;
        private string lspath;
        private int mnresult;

        public void DaGetBranch(MdlBranchname objBranch)
        {
            msSQL = " SELECT branch_gid,branch_name FROM hrm_mst_tbranch order by branch_code asc ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getBranch = new List<branchname_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getBranch.Add(new branchname_list
                    {
                        branch_gid = (dr_datarow["branch_gid"].ToString()),
                        branch_name = (dr_datarow["branch_name"].ToString()),
                    });
                }
                objBranch.branchname_list = getBranch;
            }
            dt_datatable.Dispose();

        }

        public bool DaPostVisitorMobileNo(string employee_gid, VisitorMobileNo values)
        {
            msSQL = "select primary_mobileno from ocs_mst_tvisitor2contactno where primary_mobileno='Yes' and visitorname_gid='" + employee_gid + "'";
            string lsprimary_mobileno = objdbconn.GetExecuteScalar(msSQL);
            if (lsprimary_mobileno == (values.primary_mobileno))
            {

                values.status = false;
                values.message = "Already Primary Mobile Number Added";
                return false;
            }
            msSQL = "select visitor2contact_gid from ocs_mst_tvisitor2contactno where mobile_no='" + values.mobile_no + "' " +
                " and visitorname_gid='" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Already This Mobile Number Added";
                return false;
            }
            msGetGid = objcmnfunctions.GetMasterGID("V2CN");
            msSQL = " insert into ocs_mst_tvisitor2contactno(" +
                    " visitor2contact_gid," +
                    " visitorname_gid," +
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
                values.message = "Error Occured";
                return false;
            }
        }

        //Mobile Number List
        public void DaGetVisitMobileNo(string employee_gid, VisitorMobileNo values)
        {
            msSQL = "select mobile_no,visitor2contact_gid,primary_mobileno from ocs_mst_tvisitor2contactno where " +
                   " visitorname_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstmobileno_list = new List<VisitorMobileNo>();
            foreach (DataRow dr_datarow in dt_datatable.Rows)
            {
                getmstmobileno_list.Add(new VisitorMobileNo
                {
                    visitor2contact_gid = (dr_datarow["visitor2contact_gid"].ToString()),
                    mobile_no = (dr_datarow["mobile_no"].ToString()),
                    primary_mobileno = (dr_datarow["primary_mobileno"].ToString()),
                });
            }
            values.visitormobileno_list = getmstmobileno_list;
            dt_datatable.Dispose();
        }
        //Mobile Number Delete
        public void DaDeleteVisitMobileNo(string visitor2contact_gid, VisitorMobileNo values)
        {
            msSQL = "delete from ocs_mst_tvisitor2contactno where visitor2contact_gid='" + visitor2contact_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "Mobile Number Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }
        //Email Address Add
        public bool DaPostVisitorEmailAddress(string employee_gid, VisitorEmailAddress values)
        {
            msSQL = "select primary_emailaddress from ocs_mst_tvisitor2email where primary_emailaddress='Yes' and visitorname_gid='" + employee_gid + "'";
            string lsprimary_emailaddress = objdbconn.GetExecuteScalar(msSQL);
            if (lsprimary_emailaddress == (values.primary_emailaddress))
            {

                values.status = false;
                values.message = "Already Primary Email Address Added";
                return false;
            }
            msSQL = "select visitor2email_gid from ocs_mst_tvisitor2email where email_address='" + values.email_address + "' and visitorname_gid='" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Already This Email Address Added";
                return false;
            }
            msGetGid = objcmnfunctions.GetMasterGID("V2EA");
            msSQL = " insert into ocs_mst_tvisitor2email(" +
                    " visitor2email_gid," +
                    " visitorname_gid," +
                    " email_address," +
                    " primary_emailaddress," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + values.email_address.Replace("'", " ") + "'," +
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
                values.message = "Error Occured";
                return false;
            }
        }
        //Email Address List
        public void DaGetVisitorEmailAddressList(string employee_gid,string visitorname_gid, VisitorEmailAddress values)
        {
            msSQL = "select email_address,visitor2email_gid,primary_emailaddress from ocs_mst_tvisitor2email where " +
              " visitorname_gid='" + employee_gid + "' or visitorname_gid='" + visitorname_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstemailaddress_list = new List<VisitorEmailAddress>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstemailaddress_list.Add(new VisitorEmailAddress
                    {
                        visitor2email_gid = (dr_datarow["visitor2email_gid"].ToString()),
                        email_address = (dr_datarow["email_address"].ToString()),
                        primary_emailaddress = (dr_datarow["primary_emailaddress"].ToString())
                    });
                }
                values.visitoremailaddress_list = getmstemailaddress_list;
            }
            dt_datatable.Dispose();
            values.status = true;
        }
        //Email Address Delete
        public void DaDeleteVisitorEmailAddress(string visitor2email_gid, VisitorEmailAddress values)
        {
            msSQL = "delete from ocs_mst_tvisitor2email where visitor2email_gid='" + visitor2email_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "Email Address Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }
        //Visitor Name
        public bool DaPostVisitorName(string employee_gid, VisitorName values)
        {
            msSQL = "select visitorname_gid from ocs_mst_tvisitorname where visitor_name='" + values.visitor_name + "' and visitor_gid='" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Already This Visitor Name Added";
                return false;
            }
            objODBCDatareader.Close();
           
            msGetGid = objcmnfunctions.GetMasterGID("V2VN");
            msSQL = " insert into ocs_mst_tvisitorname(" +
                    " visitorname_gid," +
                    " visitor_gid," +
                    " visitor_name," +
                    " visitor_idproof," +
                    " visitor_idproofno," +
                    " temperature," +
                    " vaccination_status, " +
                    " visitorcompany_name, " +
                    " spo2, " +
                    " visitor_email, " +
                    " visitor_mobileno, " +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + values.visitor_name.Replace("'", " ") + "',";
            if (values.visitoridproof == null || values.visitoridproof == "" || values.visitoridproof == "undefind")
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.visitoridproof.Replace("'", " ") + "',";
            }    
            msSQL+= "'" + values.visitoridproof_no + "'," +
                    "'" + values.temperature + "'," +
                    "'" + values.vaccination_status + "'," +
                    "'" + values.visitorcompany_name + "'," +
                    "'" + values.spo2 + "'," +
                    "'" + values.visitor_email + "'," +
                    "'" + values.visitor_mobileno + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msSQL = "update ocs_mst_tvisitor2photo set visitorname_gid ='" + msGetGid + "' where visitorname_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                
                values.status = true;
                values.message = "Visitor Name Added Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
                return false;
            }
        }
        //Visitor Name List
        public void DaGetVisitorName(string employee_gid, VisitorName values)
        {
            msSQL = "select visitor_name,visitorname_gid,visitor_idproof,visitor_idproofno,generate_status from ocs_mst_tvisitorname where " +
              " visitor_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstvisitor_list = new List<VisitorName>();

            foreach (DataRow dr_datarow in dt_datatable.Rows)
            {
                getmstvisitor_list.Add(new VisitorName
                {
                    visitorname_gid = (dr_datarow["visitorname_gid"].ToString()),
                    visitor_name = (dr_datarow["visitor_name"].ToString()),
                    visitoridproof_no =(dr_datarow["visitor_idproofno"].ToString()),
                    visitoridproof =(dr_datarow["visitor_idproof"].ToString()),
                    generate_status =(dr_datarow["generate_status"].ToString())
                });
            }
            values.visitorname_list = getmstvisitor_list;

            dt_datatable.Dispose();
            values.status = true;
        }
        //VisitorName Delete
        public void DaDeleteVisitorName(string visitorname_gid, VisitorName values)
        {
            msSQL = "delete from ocs_mst_tvisitorname where visitorname_gid='" + visitorname_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "delete from ocs_mst_tvisitor2email where visitorname_gid='" + visitorname_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                msSQL = "delete from ocs_mst_tvisitor2contactno where visitorname_gid='" + visitorname_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                msSQL = "delete from ocs_mst_tvisitor2photo where visitorname_gid='" + visitorname_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                values.message = "Visitor Name Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }
        //Temp clear
        public void DaGetVisitorTempClear(string employee_gid, result values)
        {
            msSQL = "delete from ocs_mst_tvisitorname where visitor_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from ocs_mst_tvisitor2email where visitorname_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from ocs_mst_tvisitor2contactno where visitorname_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            msSQL = "delete from ocs_mst_tvisitor2photo where visitorname_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            values.status = true;
        }
        //Visitor add
        public void DaCreateVisitor(Visitor values, string employee_gid)
        {
            msSQL = "select visitorname_gid from ocs_mst_tvisitorname where visitor_gid = '" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Kindly Add Visitor Name";
                return;
            }
            objODBCDatareader.Close();
           
            DateTime date_value = DateTime.Now;
            string lsvisitid = objcmnfunctions.GetMasterGID("VSID");
            string lsseq = lsvisitid.Substring(lsvisitid.Length - 8);
            string lsbranch = values.branch_name.ToUpper();
            string lsbranch3 = lsbranch.Substring(0, 3);
            string lsid = lsbranch3 + date_value.ToString("ddMMyyyy") + lsseq;
            msGetGid = objcmnfunctions.GetMasterGID("VICT");
            msSQL = " insert into ocs_mst_tvisitor(" +
            " visitor_gid," +
            " visit_id,"+
            " branch_gid," +
            " branch_name," +
            " visiting_type," +
            " purpose_of_visit," +
            " in_time," +
            " tentative_out_time," +
            " visit_date," +
            " actual_out_time," +
            " created_by," +
            " created_date)" +
            " values(" +
            "'" + msGetGid + "'," +
            "'" + lsid + "'," +
            "'" + values.branch_gid + "'," +
            "'" + values.branch_name + "'," +           
            "'" + values.visiting_type + "'," +
            "'" + values.purpose_of_visit.Replace("'", " ") + "'," +
            "'" + Convert.ToDateTime(values.in_time).ToString("HH: mm:ss") + "'," +
            "'" + Convert.ToDateTime(values.tentative_out_time).ToString("HH: mm:ss") + "'," +
            "'" + values.visit_date + "'," +        
            "'" + Convert.ToDateTime(values.actual_out_time).ToString("HH: mm:ss") + "'," +
            "'" + employee_gid + "'," +
            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            for (var i = 0; i < values.visitingofficer_name.Count; i++)
            {
                msGetGid1 = objcmnfunctions.GetMasterGID("VSOF");

                msSQL = "Insert into ocs_mst_tvisitingofficer_name( " +
                       " visitingofficer_gid, " +
                       " visitor_gid," +
                       " employee_gid," +
                       " employee_name," +
                       " created_by," +
                       " created_date)" +
                       " values(" +
                       "'" + msGetGid1 + "'," +
                       "'" + msGetGid + "'," +
                       "'" + values.visitingofficer_name[i].employee_gid + "'," +
                       "'" + values.visitingofficer_name[i].employee_name + "'," +
                       "'" + employee_gid + "'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Visitor Added Successfully";


                msSQL = "update ocs_mst_tvisitorname set visitor_gid ='" + msGetGid + "' where visitor_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
             
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Adding";
            }

        }
        //Upload document
        public bool DaPostVisitorUploadPhoto(HttpRequest httpRequest, VisitorUploadphotoList objfilename, string employee_gid, string user_gid)
        {

            HttpFileCollection httpFileCollection;



            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;

            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string pdfFilName = string.Empty;

            string lsdocumenttype_gid = string.Empty;
            string photo_name = httpRequest.Form["photo_name"];
            string path = lspath;



            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/VisitorPhotoUpload/" + DateTime.Now.Year + "/" + DateTime.Now.Month;

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

                            bool status;
                            status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Master/VisitorPhotoUpload/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                            ms.Close();
                            lspath = "erpdocument" + "/" + lscompany_code + "/" + "Master/VisitorPhotoUpload/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                            msGetGid = objcmnfunctions.GetMasterGID("V2DC");

                            msSQL = " insert into ocs_mst_tvisitor2photo( " +
                                  " visitor2photo_gid ," +
                                  " visitorname_gid," +
                                  " file_name," +
                                  " visitphoto_name," +
                                  " visitphoto_path, " +
                                  " created_by ," +
                                  " created_date " +
                                  " )values(" +
                                  "'" + msGetGid + "'," +
                                  "'" + employee_gid + "'," +
                                  "'" + httpPostedFile.FileName.Replace("'", " ") + "'," +
                                  "'" + photo_name.Replace("'", " ") + "'," +
                                  "'" + lspath + msdocument_gid + FileExtension.Replace("'", " ") + "'," +
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
                            objfilename.message = "Photo Uploaded Successfully";
                            return true;
                        }
                        else
                        {
                            objfilename.status = false;
                            objfilename.message = "Error Occured while uploading photo";
                            return false;
                        }


                    }
                    return true;

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
        //Photo list
        public void DaGetVisitPhotos(string employee_gid, MdlVisitor values)
        {
            msSQL = "select visitor2photo_gid,visitphoto_name,visitphoto_path,file_name  from ocs_mst_tvisitor2photo where " +
             " visitorname_gid='" + employee_gid + "' order by visitor2photo_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getVisitorUploadphotoList = new List<VisitorUploadphotoList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getVisitorUploadphotoList.Add(new VisitorUploadphotoList
                    {
                        visitor2photo_gid = (dr_datarow["visitor2photo_gid"].ToString()),
                        photo_name = (dr_datarow["visitphoto_name"].ToString()),
                        document_path = objcmnstorage.EncryptData((dr_datarow["visitphoto_path"].ToString())),
                        filename = (dr_datarow["file_name"].ToString())
                    });
                }
                values.VisitorUploadphotoList = getVisitorUploadphotoList;
            }
            dt_datatable.Dispose();
            values.status = true;
        }
        //Delete photo
        public void DaDeleteVisittmpPhotoList(string visitor2photo_gid, VisitorUploadphotoList values)
        {
            msSQL = "delete from ocs_mst_tvisitor2photo where visitor2photo_gid='" + visitor2photo_gid + "'";
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
        //Visitor list
        public void DaGetVisitor(MdlVisitor objVisitor, string employee_gid)
        {
            try
            {
                msSQL = " select a.visitor_gid, branch_name, visiting_type, visit_date, visit_id," +
                        " date_format(a.created_date,'%d-%m-%Y  %h:%i %p') as created_date,concat(c.user_firstname,' ' ,c.user_lastname,'/',c.user_code) as created_by, " +
                        " date_format(now(),'%d-%m-%Y') as now_date " +
                        " from ocs_mst_tvisitor a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where a.created_date >= CURDATE() and a.created_by = '" + employee_gid + "'  order by a.visitor_gid desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getVisitor = new List<Visitor>();
                if (dt_datatable.Rows.Count != 0)
                {

                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        string lsgenerate_status;
                        string lsvisitor_name;
                        string lsvisitingofficer_name;
                        msSQL = "select visitor_gid from ocs_mst_tvisitorname where generate_status='Y' and visitor_gid='" + dr_datarow["visitor_gid"] + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsgenerate_status = "Y";                           

                        }
                        else
                        {
                            lsgenerate_status = "N";
                           
                        }
                        objODBCDatareader.Close();
                        msSQL = "select visitor_gid,group_concat(visitor_name) as visitor_name from ocs_mst_tvisitorname where visitor_gid='" + dr_datarow["visitor_gid"] + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsvisitor_name = objODBCDatareader["visitor_name"].ToString();
                        }
                        else
                        {
                            lsvisitor_name = "";
                        }
                        objODBCDatareader.Close();
                        msSQL = "select visitor_gid,group_concat(employee_name) as visitingofficer_name from ocs_mst_tvisitingofficer_name where visitor_gid='" + dr_datarow["visitor_gid"] + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsvisitingofficer_name = objODBCDatareader["visitingofficer_name"].ToString();
                        }
                        else
                        {
                            lsvisitingofficer_name = "";
                        }
                        objODBCDatareader.Close();
                        getVisitor.Add(new Visitor
                        {
                            visitor_gid = (dr_datarow["visitor_gid"].ToString()),
                            visit_id = (dr_datarow["visit_id"].ToString()),
                            branch_name = (dr_datarow["branch_name"].ToString()),
                            visiting_type = (dr_datarow["visiting_type"].ToString()),
                            visit_date = (dr_datarow["visit_date"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            generate_status = lsgenerate_status,
                            visitor_name = lsvisitor_name,
                            visitingofficial_name = lsvisitingofficer_name,
                            now_date = (dr_datarow["now_date"].ToString())
                        });
                    }
                    objVisitor.visitor_list = getVisitor;
                }
                dt_datatable.Dispose();
                objVisitor.status = true;
            }
            catch
            {
                objVisitor.status = false;
            }
        }

        //Delete Visitor
        public void DaDeleteVisitor(string visitor_gid, Visitor values)
        {
            string lsvisitorname_gid = objdbconn.GetExecuteScalar("select visitorname_gid from ocs_mst_tvisitorname where visitor_gid='" + visitor_gid + "'");
         
            msSQL = "delete from ocs_mst_tvisitor2email where visitorname_gid='" + lsvisitorname_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            msSQL = "delete from ocs_mst_tvisitor2contactno where visitorname_gid='" + lsvisitorname_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            msSQL = "delete from ocs_mst_tvisitor2photo where visitorname_gid='" + lsvisitorname_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.message = "Visitor Deleted Successfully";
                values.status = true;
                msSQL = "delete from ocs_mst_tvisitorname where visitor_gid='" + visitor_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                msSQL = "delete from ocs_mst_tvisitor where visitor_gid='" + visitor_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            }
            else
            {
                values.message = "Error Occured while Deleting";
                values.status = false;

            }
        }
        //Edit Visitor View
        public void DaGetVisitorView( Visitor objVisitor,string visitor_gid)
        {
            try
            {
                msSQL = " select visitor_gid,visit_id,a.branch_gid,branch_name,visiting_type,visit_date, purpose_of_visit,visitingofficer_name,in_time,tentative_out_time," +
                        " actual_out_time,date_format(a.created_date,'%d-%m-%Y  %h:%i %p') as created_date,concat(c.user_firstname,' ' ,c.user_lastname,'||',c.user_code) as created_by " +
                        " from ocs_mst_tvisitor a" +
                        " left join hrm_mst_temployee b on a.created_by=b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid=b.user_gid where a.visitor_gid = '" + visitor_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {

                    objVisitor.visitor_gid = objODBCDatareader["visitor_gid"].ToString();
                    objVisitor.visit_id = objODBCDatareader["visit_id"].ToString();
                    objVisitor.branch_gid = objODBCDatareader["branch_gid"].ToString();
                    objVisitor.branch_name = objODBCDatareader["branch_name"].ToString();
                    objVisitor.visiting_type = objODBCDatareader["visiting_type"].ToString();
                    objVisitor.visit_date = objODBCDatareader["visit_date"].ToString();
                    objVisitor.created_date = objODBCDatareader["created_date"].ToString();
                    objVisitor.created_by = objODBCDatareader["created_by"].ToString();
                    objVisitor.purpose_of_visit = objODBCDatareader["purpose_of_visit"].ToString();
                    objVisitor.in_time = objODBCDatareader["in_time"].ToString();
                    objVisitor.tentative_out_time = objODBCDatareader["tentative_out_time"].ToString();             
                    objVisitor.actual_out_time = objODBCDatareader["actual_out_time"].ToString();
                    if (objODBCDatareader["in_time"].ToString() == "" || objODBCDatareader["in_time"].ToString() == null)
                    {
                    }
                    else
                    {
                        objVisitor.Tin_time = Convert.ToDateTime(objODBCDatareader["in_time"].ToString());
                    }
                    if (objODBCDatareader["tentative_out_time"].ToString() == "" || objODBCDatareader["tentative_out_time"].ToString() == null)
                    {
                    }
                    else
                    {
                        objVisitor.Ttentative_out_time = Convert.ToDateTime(objODBCDatareader["tentative_out_time"].ToString());
                    }
                    if (objODBCDatareader["actual_out_time"].ToString() == "" || objODBCDatareader["actual_out_time"].ToString() == null)
                    {
                    }
                    else
                    {
                        objVisitor.Tactual_out_time = Convert.ToDateTime(objODBCDatareader["actual_out_time"].ToString());
                    }
                }
                objODBCDatareader.Close();
                msSQL = " select visitingofficer_gid,employee_gid,employee_name from ocs_mst_tvisitingofficer_name " +
                       " where visitor_gid='" + visitor_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getofficerlist = new List<visitingofficer_name>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getofficerlist.Add(new visitingofficer_name
                        {
                            visitingofficer_gid = dt["visitingofficer_gid"].ToString(),
                            employee_gid = dt["employee_gid"].ToString(),
                            employee_name = dt["employee_name"].ToString(),
                        });
                        objVisitor.visitingofficer_name = getofficerlist;
                    }
                }
                dt_datatable.Dispose();

                msSQL = " SELECT a.user_firstname,a.user_gid ,concat(a.user_firstname,' ',a.user_lastname,' / ',a.user_code) as employee_name,b.employee_gid from adm_mst_tuser a " +
                         " LEFT JOIN hrm_mst_temployee b ON a.user_gid=b.user_gid " +
                         " where user_status<>'N' order by a.user_firstname asc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_visitingemployee = new List<visitingofficerem_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    objVisitor.visitingofficerem_list = dt_datatable.AsEnumerable().Select(row =>
                      new visitingofficerem_list
                      {
                          employee_gid = row["employee_gid"].ToString(),
                          employee_name = row["employee_name"].ToString()
                      }
                    ).ToList();
                }
                dt_datatable.Dispose();
                msSQL = "select visitor_gid,group_concat(employee_name) as visitingofficer_name from ocs_mst_tvisitingofficer_name where visitor_gid='" + visitor_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objVisitor.visitofficer_name = objODBCDatareader["visitingofficer_name"].ToString();
                }
                else
                {
                    objVisitor.visitofficer_name = "";
                }
                objODBCDatareader.Close();
                objVisitor.status = true;
            }
            catch
            {
                objVisitor.status = false;
            }
        }

        public void DaGetVisitorTagView( visitortag objVisitor, string visitorname_gid)
        {
            try
            {
                msSQL = " select visitorname_gid,tag_id,date_format(tag_validity_from,'%Y-%m-%d') as tag_validity_from,date_format(tag_validity_to,'%Y-%m-%d') as tag_validity_to" +
                        " from ocs_mst_tvisitorname a where a.visitorname_gid = '" + visitorname_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {

                    objVisitor.visitorname_gid = objODBCDatareader["visitorname_gid"].ToString();     
                    objVisitor.tag_id = objODBCDatareader["tag_id"].ToString();
                    objVisitor.tag_validity_from = objODBCDatareader["tag_validity_from"].ToString();
                    objVisitor.tag_validity_to = objODBCDatareader["tag_validity_to"].ToString();
                   
                }
                objODBCDatareader.Close();
                
                objVisitor.status = true;
            }
            catch
            {
                objVisitor.status = false;
            }
        }
        //Visitor photo list
        public void DaGetVisitPhotoList(string employee_gid, MdlVisitor values ,string visitorname_gid)
        {
            msSQL = "select visitor2photo_gid,visitphoto_name,visitphoto_path,file_name  from ocs_mst_tvisitor2photo where " +
                   " visitorname_gid='" + visitorname_gid + "' or visitorname_gid ='" + employee_gid + "' order by visitor2photo_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getVisitorUploadphotoList = new List<VisitorUploadphotoList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getVisitorUploadphotoList.Add(new VisitorUploadphotoList
                    {
                        visitor2photo_gid = (dr_datarow["visitor2photo_gid"].ToString()),
                        photo_name = (dr_datarow["visitphoto_name"].ToString()),
                        document_path = objcmnstorage.EncryptData((dr_datarow["visitphoto_path"].ToString())),
                        filename = (dr_datarow["file_name"].ToString())
                    });
                }
                values.VisitorUploadphotoList = getVisitorUploadphotoList;
            }
            dt_datatable.Dispose();
            values.status = true;
        }
        //Visitor mobile list
        public void DaGetVisitMobileNoList(string employee_gid, VisitorMobileNo values, string visitorname_gid)
        {
            msSQL = "select mobile_no,visitor2contact_gid,primary_mobileno from ocs_mst_tvisitor2contactno where " +
              " visitorname_gid='" + visitorname_gid + "' or visitorname_gid ='" + employee_gid+"'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstmobileno_list = new List<VisitorMobileNo>();
            foreach (DataRow dr_datarow in dt_datatable.Rows)
            {
                getmstmobileno_list.Add(new VisitorMobileNo
                {
                    visitor2contact_gid = (dr_datarow["visitor2contact_gid"].ToString()),
                    mobile_no = (dr_datarow["mobile_no"].ToString()),
                    primary_mobileno = (dr_datarow["primary_mobileno"].ToString()),
                });
            }
            values.visitormobileno_list = getmstmobileno_list;
            dt_datatable.Dispose();
        }
        //Viitor Email List
        public void DaGetVisitorEmailAddress(string employee_gid, VisitorEmailAddress values)
        {
            msSQL = "select email_address,visitor2email_gid,primary_emailaddress from ocs_mst_tvisitor2email where " +
                  "  visitorname_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstemailaddress_list = new List<VisitorEmailAddress>();
            foreach (DataRow dr_datarow in dt_datatable.Rows)
            {
                getmstemailaddress_list.Add(new VisitorEmailAddress
                {
                    visitor2email_gid = (dr_datarow["visitor2email_gid"].ToString()),
                    email_address = (dr_datarow["email_address"].ToString()),
                    primary_emailaddress = (dr_datarow["primary_emailaddress"].ToString())
                });
            }
            values.visitoremailaddress_list = getmstemailaddress_list;
            dt_datatable.Dispose();
            values.status = true;
        }
        //visitor Name list
        public void DaGetVisitorNameList(string employee_gid, VisitorName values,string visitor_gid)
        {
            msSQL = "select visitor_name,visitorname_gid,visitor_idproofno,visitor_idproof,generate_status from ocs_mst_tvisitorname where " +
              " visitor_gid='" + visitor_gid + "'or visitor_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstvisitor_list = new List<VisitorName>();

            foreach (DataRow dr_datarow in dt_datatable.Rows)
            {
                getmstvisitor_list.Add(new VisitorName
                {
                    visitorname_gid = (dr_datarow["visitorname_gid"].ToString()),
                    visitor_name = (dr_datarow["visitor_name"].ToString()),
                    visitoridproof_no = (dr_datarow["visitor_idproofno"].ToString()),
                    visitoridproof = (dr_datarow["visitor_idproof"].ToString()),
                    generate_status = (dr_datarow["generate_status"].ToString())
                });
            }
            values.visitorname_list = getmstvisitor_list;

            dt_datatable.Dispose();
            values.status = true;
        }
        //Update Visitor
        public void DaUpdateVisitor(Visitor values, string employee_gid)
        {
            msSQL = "select visitorname_gid from ocs_mst_tvisitorname where visitor_gid = '" + employee_gid + "' or  visitor_gid = '" + values.visitor_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Kindly Add Visitor Name";
                return;
            }
            objODBCDatareader.Close();
          
           
            msSQL = " update ocs_mst_tvisitor set" +
                    " branch_gid =" + "'" + values.branch_gid + "'," +
                    " branch_name =" + "'" + values.branch_name + "'," +                    
                    " visiting_type =" + "'" + values.visiting_type + "'," +
                    " purpose_of_visit=" + "'" + values.purpose_of_visit.Replace("'", " ") + "'," +
                    " in_time =" + "'" + Convert.ToDateTime(values.in_time).ToString("HH:mm:ss") + "'," +
                    " tentative_out_time=" + "'" + Convert.ToDateTime(values.tentative_out_time).ToString("HH:mm:ss") + "'," +
                    " visit_date=" + "'" + values.visit_date + "'," +
                    " actual_out_time=" + "'" + Convert.ToDateTime(values.actual_out_time).ToString("HH:mm:ss") + "'," +
                    " created_by=" + "'" + employee_gid + "'," +
                    " created_date =" + "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    " where  visitor_gid = '" + values.visitor_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            msSQL = " delete from ocs_mst_tvisitingofficer_name where  visitor_gid = '" + values.visitor_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                for (var i = 0; i < values.visitingofficer_name.Count; i++)
                {
                    msGetGid1 = objcmnfunctions.GetMasterGID("VSOF");

                    msSQL = "Insert into ocs_mst_tvisitingofficer_name( " +
                           " visitingofficer_gid, " +
                           " visitor_gid," +
                           " employee_gid," +
                           " employee_name," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetGid1 + "'," +
                           "'" + values.visitor_gid + "'," +
                           "'" + values.visitingofficer_name[i].employee_gid + "'," +
                           "'" + values.visitingofficer_name[i].employee_name + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Visitor Updated Successfully";

                 msSQL = "update ocs_mst_tvisitorname set visitor_gid ='" + values.visitor_gid + "' where visitor_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Updating";
            }
        }

        public void DaUpdateVisitorTag(visitortag values, string employee_gid)
        {
           
            msSQL = "select date_format(tag_validity_from,'%Y-%m-%d') as tag_validity_from, date_format(tag_validity_to,'%Y-%m-%d') as tag_validity_to," +
                    "tag_id,taggenerated_by,taggenerated_date from ocs_mst_tvisitorname where visitorname_gid ='" + values.visitorname_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                string lsUpdatedBy = objODBCDatareader["taggenerated_by"].ToString();
                string lsUpdatedDate = objODBCDatareader["taggenerated_date"].ToString();

                if (!(String.IsNullOrEmpty(lsUpdatedBy)) && !(String.IsNullOrEmpty(lsUpdatedDate)))
                {
                    msGetGid = objcmnfunctions.GetMasterGID("VTLO");
                    msSQL = " insert into ocs_mst_tvisitortaglog(" +
                              " visitortaglog_gid," +
                              " visitorname_gid," +
                              " tag_id , " +
                              " tag_validity_from , " +
                              " tag_validity_to , " +                            
                              " updated_by, " +
                              " updated_date) " +
                              " values(" +
                              "'" + msGetGid + "'," +
                              "'" + values.visitorname_gid + "'," +
                              "'" + objODBCDatareader["tag_id"].ToString() + "'," +
                              "'" + objODBCDatareader["tag_validity_from"].ToString() + "'," +
                              "'" + objODBCDatareader["tag_validity_to"].ToString() + "'," +
                              "'" + employee_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            objODBCDatareader.Close();
            msSQL = " update ocs_mst_tvisitorname set" +
                    " tag_id=" + "'" + values.tag_id + "'," +
                    " generate_status=" + "'Y',";
            if (values.tag_validity_from == null || values.tag_validity_from == "")
            {
                msSQL += " tag_validity_from=null,";
            }
            else if (values.tag_validity_from == "exist")
            {

            }
            else
            {
                msSQL += " tag_validity_from='" + Convert.ToDateTime(values.tag_validity_from).ToString("yyyy-MM-dd") + "',";
            }
            if (values.tag_validity_to == null || values.tag_validity_to == "")
            {
                msSQL += " tag_validity_to=null,";
            }
            else if (values.tag_validity_to == "exist")
            {

            }
            else
            {
                msSQL += " tag_validity_to='" + Convert.ToDateTime(values.tag_validity_to).ToString("yyyy-MM-dd") + "',";
            }
            msSQL += " taggenerated_by=" + "'" + employee_gid + "'," +
                    " taggenerated_date =" + "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    " where   visitorname_gid = '" + values.visitorname_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Visitor Tag Generated Successfully";

                
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Generating";
            }
        }

        // EditMobile Number Add
        public bool DaPostEditVisitorMobileNo(string employee_gid, VisitorMobileNo values)
        {
            msSQL = "select primary_mobileno from ocs_mst_tvisitor2contactno where primary_mobileno='Yes' and (visitorname_gid='" + employee_gid + "' or visitorname_gid = '" + values.visitor_gid + "')";
            string lsprimary_mobileno = objdbconn.GetExecuteScalar(msSQL);
            if (lsprimary_mobileno == (values.primary_mobileno))
            {

                values.status = false;
                values.message = "Already Primary Mobile Number Added";
                return false;
            }
            msSQL = "select visitor2contact_gid from ocs_mst_tvisitor2contactno where mobile_no='" + values.mobile_no + "' " +
                " and visitorname_gid='" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Already This Mobile Number Added";
                return false;
            }
            msGetGid = objcmnfunctions.GetMasterGID("V2CN");
            msSQL = " insert into ocs_mst_tvisitor2contactno(" +
                    " visitor2contact_gid," +
                    " visitorname_gid," +
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
                values.message = "Mobile Number Updated Successfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured";
                return false;
            }
        }
        public bool DaPostEditVisitorEmailAddress(string employee_gid, VisitorEmailAddress values)
        {
            msSQL = "select primary_emailaddress from ocs_mst_tvisitor2email where primary_emailaddress='Yes' and (visitor_gid='" + employee_gid + "' or visitorname_gid='" + values.visitorname_gid + "')";
            string lsprimary_emailaddress = objdbconn.GetExecuteScalar(msSQL);
            if (lsprimary_emailaddress == (values.primary_emailaddress))
            {

                values.status = false;
                values.message = "Already Primary Email Address Added";
                return false;
            }
            msSQL = "select visitor2email_gid from ocs_mst_tvisitor2email where email_address='" + values.email_address + "' and visitorname_gid='" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Already This Email Address Added";
                return false;
            }
            msGetGid = objcmnfunctions.GetMasterGID("V2EA");
            msSQL = " insert into ocs_mst_tvisitor2email(" +
                    " visitor2email_gid," +
                    " visitorname_gid," +
                    " email_address," +
                    " primary_emailaddress," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + values.email_address.Replace("'", " ") + "'," +
                    "'" + values.primary_emailaddress + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                values.status = true;
                values.message = "Email Address Updated Successfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured";
                return false;
            }
        }
        //Visitor Name
        public bool DaPostEditVisitorName(string employee_gid, VisitorName values)
        {
            msSQL = "select visitorname_gid from ocs_mst_tvisitorname where visitor_name='" + values.visitor_name + "' and (visitor_gid='" + employee_gid + "' or visitor_gid='" + values.visitor_gid + "')";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Already This Visitor Name Added";
                return false;
            }
            objODBCDatareader.Close();
            msSQL = "select visitor2contact_gid from ocs_mst_tvisitor2contactno where visitor_gid = '" + employee_gid + "'";
            string lsphonecount = objdbconn.GetExecuteScalar(msSQL);
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Kindly Add Primary Phone Number";
                return false;
            }
            objODBCDatareader.Close();
         
            msGetGid = objcmnfunctions.GetMasterGID("V2VN");
            msSQL = " insert into ocs_mst_tvisitorname(" +
                    " visitorname_gid," +
                    " visitor_gid," +
                    " visitor_name," +
                    " tag_id," +
                    " visitor_idproof," +
                    " visitor_idproofno," +
                    " temperature," +
                    " tag_validity_from," +
                    " tag_validity_to," +
                    " vaccination_status, " +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + values.visitor_name.Replace("'", " ") + "'," +
                    "'" + values.tag_id + "'," +
                    "'" + values.visitoridproof.Replace("'", " ") + "'," +
                    "'" + values.visitoridproof_no + "'," +
                    "'" + values.temperature.Replace("'", " ") + "'," +
                    "'" + Convert.ToDateTime(values.tag_validity_from).ToString("yyyy-MM-dd") + "'," +
                    "'" + Convert.ToDateTime(values.tag_validity_to).ToString("yyyy-MM-dd") + "'," +
                    "'" + values.vaccination_status + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                values.status = true;
                values.message = "Visitor Name Updated Successfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured";
                return false;
            }
        }

        public void DaGetTaggedVisitor(MdlVisitor objVisitor, string employee_gid)
        {
            try
            {
                msSQL = " select a.visitor_gid, branch_name, visiting_type, visit_date, visit_id," +
                    " date_format(a.created_date,'%d-%m-%Y  %h:%i %p') as created_date,concat(c.user_firstname,' ' ,c.user_lastname,'/',c.user_code) as created_by, " +
                    " date_format(now(),'%d-%m-%Y') as now_date " +
                    " from ocs_mst_tvisitor a" +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " left join ocs_mst_tvisitorname d on d.visitor_gid = a.visitor_gid " +
                    " where d.generate_status='Y' and a.created_by = '" + employee_gid + "' and d.tag_validity_to >= CURDATE() group by a.visitor_gid order by a.visitor_gid desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getVisitor = new List<Visitor>();
                if (dt_datatable.Rows.Count != 0)
                {

                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        string lsgenerate_status;
                        string lsvisitor_name;
                        string lsvisitingofficer_name;
                        msSQL = "select visitor_gid from ocs_mst_tvisitorname where generate_status='Y' and visitor_gid='" + dr_datarow["visitor_gid"] + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsgenerate_status = "Y";

                        }
                        else
                        {
                            lsgenerate_status = "N";

                        }
                        objODBCDatareader.Close();
                        msSQL = "select visitor_gid,group_concat(visitor_name) as visitor_name from ocs_mst_tvisitorname where visitor_gid='" + dr_datarow["visitor_gid"] + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsvisitor_name = objODBCDatareader["visitor_name"].ToString();
                        }
                        else
                        {
                            lsvisitor_name = "";
                        }
                        objODBCDatareader.Close();
                        msSQL = "select visitor_gid,group_concat(employee_name) as visitingofficer_name from ocs_mst_tvisitingofficer_name where visitor_gid='" + dr_datarow["visitor_gid"] + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsvisitingofficer_name = objODBCDatareader["visitingofficer_name"].ToString();
                        }
                        else
                        {
                            lsvisitingofficer_name = "";
                        }
                        objODBCDatareader.Close();
                        getVisitor.Add(new Visitor
                        {
                            visitor_gid = (dr_datarow["visitor_gid"].ToString()),
                            visit_id = (dr_datarow["visit_id"].ToString()),
                            branch_name = (dr_datarow["branch_name"].ToString()),
                            visiting_type = (dr_datarow["visiting_type"].ToString()),
                            visit_date = (dr_datarow["visit_date"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            generate_status = lsgenerate_status,
                            visitor_name = lsvisitor_name,
                            visitingofficial_name = lsvisitingofficer_name,
                            now_date = (dr_datarow["now_date"].ToString())
                        });
                    }
                    objVisitor.visitor_list = getVisitor;
                }
                dt_datatable.Dispose();
                objVisitor.status = true;
            }
            catch
            {
                objVisitor.status = false;
            }
        }

        public void DaGetHistoryVisitor(MdlVisitor objVisitor, string employee_gid)
        {
            try
            {
                msSQL = " select a.visitor_gid, branch_name, visiting_type, visit_date, visit_id," +
                    " date_format(a.created_date,'%d-%m-%Y  %h:%i %p') as created_date,concat(c.user_firstname,' ' ,c.user_lastname,'/',c.user_code) as created_by, " +
                    " date_format(now(),'%d-%m-%Y') as now_date " +
                    " from ocs_mst_tvisitor a" +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " left join ocs_mst_tvisitorname d on d.visitor_gid = a.visitor_gid " +
                    " where a.created_date < CURDATE() and (d.tag_validity_to < CURDATE() or d.tag_validity_to is null) and a.created_by = '" + employee_gid + "' group by a.visitor_gid order by a.visitor_gid desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getVisitor = new List<Visitor>();
                if (dt_datatable.Rows.Count != 0)
                {

                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        string lsgenerate_status;
                        string lsvisitor_name;
                        string lsvisitingofficer_name;
                        msSQL = "select visitor_gid from ocs_mst_tvisitorname where generate_status='Y' and visitor_gid='" + dr_datarow["visitor_gid"] + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsgenerate_status = "Y";

                        }
                        else
                        {
                            lsgenerate_status = "N";

                        }
                        objODBCDatareader.Close();
                        msSQL = "select visitor_gid,group_concat(visitor_name) as visitor_name from ocs_mst_tvisitorname where visitor_gid='" + dr_datarow["visitor_gid"] + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsvisitor_name = objODBCDatareader["visitor_name"].ToString();
                        }
                        else
                        {
                            lsvisitor_name = "";
                        }
                        objODBCDatareader.Close();
                        msSQL = "select visitor_gid,group_concat(employee_name) as visitingofficer_name from ocs_mst_tvisitingofficer_name where visitor_gid='" + dr_datarow["visitor_gid"] + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsvisitingofficer_name = objODBCDatareader["visitingofficer_name"].ToString();
                        }
                        else
                        {
                            lsvisitingofficer_name = "";
                        }
                        objODBCDatareader.Close();
                        getVisitor.Add(new Visitor
                        {
                            visitor_gid = (dr_datarow["visitor_gid"].ToString()),
                            visit_id = (dr_datarow["visit_id"].ToString()),
                            branch_name = (dr_datarow["branch_name"].ToString()),
                            visiting_type = (dr_datarow["visiting_type"].ToString()),
                            visit_date = (dr_datarow["visit_date"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            generate_status = lsgenerate_status,
                            visitor_name = lsvisitor_name,
                            visitingofficial_name = lsvisitingofficer_name,
                            now_date = (dr_datarow["now_date"].ToString())
                        });
                    }
                    objVisitor.visitor_list = getVisitor;
                }
                dt_datatable.Dispose();
                objVisitor.status = true;
            }
            catch
            {
                objVisitor.status = false;
            }
        }

        public void DaVisitorCount(string user_gid, string employee_gid, VisitorCount values)
        {
            msSQL = " select count(a.visitor_gid) as todayvisitor_count from ocs_mst_tvisitor a " +
                    " where a.created_date >= CURDATE() and a.created_by = '" + employee_gid + "'  order by a.visitor_gid desc";
            values.todayvisitor_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select count(distinct(a.visitor_gid)) as taggedvisitor_count from ocs_mst_tvisitor a " +
                    " left join ocs_mst_tvisitorname b on b.visitor_gid = a.visitor_gid  " +
                    " where b.generate_status='Y' and a.created_by = '" + employee_gid + "' and b.tag_validity_to >= CURDATE() order by a.visitor_gid desc ";
            values.taggedvisitor_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select count(distinct(a.visitor_gid)) as visitorhistory_count from ocs_mst_tvisitor a " +
                    " left join ocs_mst_tvisitorname b on b.visitor_gid = a.visitor_gid  " +
                    " where a.created_date < CURDATE() and (b.tag_validity_to < CURDATE() or b.tag_validity_to is null) and a.created_by = '" + employee_gid + "' order by a.visitor_gid desc ";
            values.visitorhistory_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select count(visitor_gid) as totalvisitor_count from ocs_mst_tvisitor where created_by = '" + employee_gid + "'  order by visitor_gid desc";
            values.totalvisitor_count = objdbconn.GetExecuteScalar(msSQL);
        }

        public void DaGetVisitorNameViewList(string employee_gid, VisitorNameView values, string visitor_gid)
        {
            string lstagvalidatefrom, lstagvalidateto;
            msSQL = " select visitor_gid,visitor_name,visitorname_gid,visitorcompany_name,visitor_idproofno,visitor_idproof, " +
                    " temperature,spo2,vaccination_status,visitor_email,visitor_mobileno,generate_status, " +
                    " tag_id, date_format(tag_validity_from,'%d-%m-%Y') as tag_validity_from, " + 
                    " date_format(tag_validity_to,'%d-%m-%Y') as tag_validity_to from ocs_mst_tvisitorname a " +
                    " where visitor_gid='" + visitor_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstvisitorview_list = new List<VisitorNameView>();

            foreach (DataRow dr_datarow in dt_datatable.Rows)
            {

                if (dr_datarow["tag_validity_from"].ToString() == "0001-01-01")
                {
                    lstagvalidatefrom = "";
                }
                else
                {
                    lstagvalidatefrom = (dr_datarow["tag_validity_from"].ToString());
                }
                if (dr_datarow["tag_validity_to"].ToString() == "0001-01-01")
                {
                    lstagvalidateto = "";
                }
                else
                {
                    lstagvalidateto = (dr_datarow["tag_validity_to"].ToString());
                }
                getmstvisitorview_list.Add(new VisitorNameView
                {                    
                    visitor_gid = (dr_datarow["visitor_gid"].ToString()),
                    visitorname_gid = (dr_datarow["visitorname_gid"].ToString()),
                    visitor_name = (dr_datarow["visitor_name"].ToString()),
                    visitorcompany_name = (dr_datarow["visitorcompany_name"].ToString()),
                    visitor_idproofno = (dr_datarow["visitor_idproofno"].ToString()),
                    visitor_idproof = (dr_datarow["visitor_idproof"].ToString()),
                    temperature = (dr_datarow["temperature"].ToString()),
                    spo2 = (dr_datarow["spo2"].ToString()),
                    vaccination_status = (dr_datarow["vaccination_status"].ToString()),
                    visitor_email = (dr_datarow["visitor_email"].ToString()),
                    visitor_mobileno = (dr_datarow["visitor_mobileno"].ToString()),
                    generate_status = (dr_datarow["generate_status"].ToString()),
                    tag_id = (dr_datarow["tag_id"].ToString()),                   
                    tag_validity_from = lstagvalidatefrom,
                    tag_validity_to = lstagvalidateto,
                });
            }
            values.visitornameview_list = getmstvisitorview_list;

            dt_datatable.Dispose();
            values.status = true;
        }

        public void DaGetVisitorUploadDoc(string visitorname_gid, MdlMstvisitordocview values)
        {
            try
            {
                msSQL = " select visitor2photo_gid,visitorname_gid,visitphoto_name,concat(date_format(a.created_date, '%d-%m-%Y %H:%i %p')) as uploaded_date, " +
                        " visitphoto_path, concat(b.user_firstname, ' ', b.user_lastname, ' / ', b.user_code) as uploaded_by,file_name " +
                        " from ocs_mst_tvisitor2photo a " +
                        " left join adm_mst_tuser b on a.created_by = b.user_gid " +
                        " where visitorname_gid = '" + visitorname_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var UploadDocument_List = new List<UploadDocument_List>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        UploadDocument_List.Add(new UploadDocument_List
                        {
                            visitphoto_path = objcmnstorage.EncryptData((dr_datarow["visitphoto_path"].ToString())),
                            file_name = (dr_datarow["file_name"].ToString()),
                            visitor2photo_gid = (dr_datarow["visitor2photo_gid"].ToString()),
                            uploaded_by = dr_datarow["uploaded_by"].ToString(),
                            updated_date = dr_datarow["uploaded_date"].ToString(),
                            visitphoto_name = dr_datarow["visitphoto_name"].ToString()
                        });
                    }
                    values.UploadDocument_List = UploadDocument_List;
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

        public void DaGetVisitorManage(MdlVisitor objVisitor, string branch_gid)
        {
            try
            {
                if (branch_gid == null || branch_gid == "")
                {
                    msSQL = " select a.visitor_gid, branch_name, visiting_type, visit_date, visit_id," +
                        " date_format(a.created_date,'%d-%m-%Y  %h:%i %p') as created_date,concat(c.user_firstname,' ' ,c.user_lastname,'/',c.user_code) as created_by, " +
                        " date_format(now(),'%d-%m-%Y') as now_date, " +
                        " case when " +
                        " (select count(visitor_gid) from ocs_mst_tvisitorname where visitor_gid = a.visitor_gid) > 0 then 'Y' else 'N' end as generate_status, " +
                        " (select group_concat(visitor_name) from ocs_mst_tvisitorname where visitor_gid = a.visitor_gid) as visitor_name, " +
                        " (select group_concat(employee_name) from ocs_mst_tvisitingofficer_name where visitor_gid = a.visitor_gid) as visitingofficer_name " +                     
                        " from ocs_mst_tvisitor a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " left join ocs_mst_tvisitorname d on d.visitor_gid = a.visitor_gid " +
                        " where a.created_date < CURDATE() and (d.tag_validity_to < CURDATE() or d.tag_validity_to is null) group by a.visitor_gid order by a.visitor_gid desc";
                }
                else
                {
                    msSQL = " select a.visitor_gid, branch_name, visiting_type, visit_date, visit_id," +
                        " date_format(a.created_date,'%d-%m-%Y  %h:%i %p') as created_date,concat(c.user_firstname,' ' ,c.user_lastname,'/',c.user_code) as created_by, " +
                        " date_format(now(),'%d-%m-%Y') as now_date, " +
                        " case when " +
                        " (select count(visitor_gid) from ocs_mst_tvisitorname where visitor_gid = a.visitor_gid) > 0 then 'Y' else 'N' end as generate_status, " +
                        " (select group_concat(visitor_name) from ocs_mst_tvisitorname where visitor_gid = a.visitor_gid) as visitor_name, " +
                        " (select group_concat(employee_name) from ocs_mst_tvisitingofficer_name where visitor_gid = a.visitor_gid) as visitingofficer_name " +
                        " from ocs_mst_tvisitor a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " left join ocs_mst_tvisitorname d on d.visitor_gid = a.visitor_gid " +
                        " where a.branch_gid = '"+ branch_gid + "' and a.created_date < CURDATE() and (d.tag_validity_to < CURDATE() or d.tag_validity_to is null) group by a.visitor_gid order by a.visitor_gid desc";
                }
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getVisitor = new List<Visitor>();
                if (dt_datatable.Rows.Count != 0)
                {

                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        //string lsgenerate_status;
                        //string lsvisitor_name;
                        //string lsvisitingofficer_name;
                        //msSQL = "select visitor_gid from ocs_mst_tvisitorname where generate_status='Y' and visitor_gid='" + dr_datarow["visitor_gid"] + "'";


                        
                        //objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        //if (objODBCDatareader.HasRows == true)
                        //{
                        //    lsgenerate_status = "Y";

                        //}
                        //else
                        //{
                        //    lsgenerate_status = "N";

                        //}
                        //objODBCDatareader.Close();
                        //msSQL = "select visitor_gid,group_concat(visitor_name) as visitor_name from ocs_mst_tvisitorname where visitor_gid='" + dr_datarow["visitor_gid"] + "'";
                        //objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        //if (objODBCDatareader.HasRows == true)
                        //{
                        //    lsvisitor_name = objODBCDatareader["visitor_name"].ToString();
                        //}
                        //else
                        //{
                        //    lsvisitor_name = "";
                        //}
                        //objODBCDatareader.Close();
                        //msSQL = "select visitor_gid,group_concat(employee_name) as visitingofficer_name from ocs_mst_tvisitingofficer_name where visitor_gid='" + dr_datarow["visitor_gid"] + "'";
                        //objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        //if (objODBCDatareader.HasRows == true)
                        //{
                        //    lsvisitingofficer_name = objODBCDatareader["visitingofficer_name"].ToString();
                        //}
                        //else
                        //{
                        //    lsvisitingofficer_name = "";
                        //}
                        //objODBCDatareader.Close();
                        getVisitor.Add(new Visitor
                        {
                            visitor_gid = (dr_datarow["visitor_gid"].ToString()),
                            visit_id = (dr_datarow["visit_id"].ToString()),
                            branch_name = (dr_datarow["branch_name"].ToString()),
                            visiting_type = (dr_datarow["visiting_type"].ToString()),
                            visit_date = (dr_datarow["visit_date"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            generate_status = (dr_datarow["generate_status"].ToString()),
                            visitor_name = (dr_datarow["visitor_name"].ToString()),
                            visitingofficial_name = (dr_datarow["visitingofficer_name"].ToString()),
                            now_date = (dr_datarow["now_date"].ToString())
                        });
                    }
                    objVisitor.visitor_list = getVisitor;
                }
                dt_datatable.Dispose();
                objVisitor.status = true;
            }
            catch
            {
                objVisitor.status = false;
            }
        }

        public void DaShowVisitor(VisitorName objVisitor)
        {
            msSQL = " select visitor_gid,visitor_name,visitor_idproof,visitor_idproofno,temperature,vaccination_status,visitorcompany_name,spo2,visitor_email " +
         " from ocs_mst_tvisitorname  where visitor_mobileno= '" + objVisitor.visitor_mobileno + "' order by created_date desc limit 1";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objVisitor.visitor_gid = objODBCDatareader["visitor_gid"].ToString();
                objVisitor.visitor_name = objODBCDatareader["visitor_name"].ToString();
                objVisitor.visitoridproof = objODBCDatareader["visitor_idproof"].ToString();
                objVisitor.visitoridproof_no = objODBCDatareader["visitor_idproofno"].ToString();
                objVisitor.temperature = objODBCDatareader["temperature"].ToString();
                objVisitor.vaccination_status = objODBCDatareader["vaccination_status"].ToString();
                objVisitor.visitorcompany_name = objODBCDatareader["visitorcompany_name"].ToString();
                objVisitor.spo2 = objODBCDatareader["spo2"].ToString();
                objVisitor.visitor_email = objODBCDatareader["visitor_email"].ToString();
                objVisitor.status = true;

            }
            else
            {
                objVisitor.status = false;
            }

            objODBCDatareader.Close();
            objVisitor.status = true;
        }

        public void DaVisitorExportExcel(VisitorExport objvisitorreport)
        {
            msSQL = " select a.visit_id as 'Visit ID',visit_date as 'Visit Date',group_concat(e.employee_name) as 'Visiting Official Name',a.branch_name as 'Branch Name',a.visiting_type as 'Visiting Type',a.purpose_of_visit as 'Purpose of Visit',  " +
                         " d.visitorcompany_name as 'Visitor Company Name',in_time as 'In Time',tentative_out_time as 'Tentative Out Time',  " +
                         " actual_out_time as 'Actual Out Time',d.visitor_name as 'Visitor Name',d.visitor_idproofno as 'Visitor IDProof Number',d.visitor_idproof as 'Visitor IDProof',d.temperature as 'Temperature',  " +
                         " d.spo2 as 'SPO2',d.vaccination_status as 'Vaccination Status',d.visitor_email as 'Visitor Email',d.visitor_mobileno as 'Visitor Mobile Number',  " +
                         " d.tag_id as 'Tag ID', date_format(d.tag_validity_from, '%d-%m-%Y') as 'Tag Validity From',  " +
                         " date_format(d.tag_validity_to, '%d-%m-%Y') as 'Tag Validity To' , date_format(a.created_date, '%d-%m-%Y  %h:%i %p') as  " +
                         " 'Created Date',concat(c.user_firstname, ' ', c.user_lastname, '||', c.user_code) as 'Created By' " +
                         " from ocs_mst_tvisitor a  " +
                         " left join ocs_mst_tvisitingofficer_name e on e.visitor_gid = a.visitor_gid " +
                         " left join ocs_mst_tvisitorname d on a.visitor_gid = d.visitor_gid " +
                         " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                         " left join adm_mst_tuser c on c.user_gid = b.user_gid group by d.visitorname_gid";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            string lscompany_code = string.Empty;
            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            var workSheet = excel.Workbook.Worksheets.Add("LSA List");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";
                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                objvisitorreport.lsname = "Visitor Report.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/Visitor Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                objvisitorreport.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/Visitor Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objvisitorreport.lsname;
                objvisitorreport.lscloudpath =  lscompany_code + "/" + "Master/Visitor Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objvisitorreport.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }

                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(objvisitorreport.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 23])  //Address "A1:A23"
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);
                }
                excel.SaveAs(ms);
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", objvisitorreport.lscloudpath, ms);
                ms.Close();
            }
            catch (Exception ex)
            {
                objvisitorreport.status = false;
                objvisitorreport.message = "Failure";
            }
            objvisitorreport.lscloudpath = objcmnstorage.EncryptData(objvisitorreport.lscloudpath);
            objvisitorreport.lspath = objcmnstorage.EncryptData(objvisitorreport.lspath);
            objvisitorreport.status = true;
            objvisitorreport.message = "Success";
        }

    }
}