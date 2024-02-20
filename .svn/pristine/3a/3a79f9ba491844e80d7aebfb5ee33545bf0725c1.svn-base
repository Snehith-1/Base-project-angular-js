using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Data;
using System.Data.Odbc;
using ems.help.Models;
using System.IO;
using System.Configuration;

namespace ems.help.DataAccess
{
    public class DaHlpMstHelp
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        DataTable dt_datatable;
        HttpPostedFile httpPostedFile;
        string msSQL, msGetGid, msGet_documentGid, msGet_visitGid;
        int mnResult;
        int mailflag;
        string msGetGidcode, msGetGIDDOC;
        string lspath;
        OdbcDataReader objODBCDataReader, objODBCDataReader1;
        String lsuser_code, lsmodule_name, lscompany_name, lsmodule_gid, lsmoduleparent_name;
        string supportmail, emailpassword, lspop_server, lspop_port, company_name, message,raise_by,employeemail_id;

    
        public bool Dagetpage(mdlpagename values, string employee_gid ,string module_gid)
        {

            msSQL= " select distinct(b.module_name),a.module_gid from adm_mst_tprivilege a " +  
                    " left join adm_mst_tmodule b on a.module_gid = b.module_gid " +
                    " left join hrm_mst_temployee d on d.user_gid = a.user_gid " +
                    " where d.employee_gid = '" + employee_gid + "' and b.menu_level = '3' and b.module_gid_parent like '" + module_gid +"%'  order by module_gid desc";

            //msSQL = "select distinct(b.module_name),a.module_gid from adm_mst_tmodule2employee a " +
            //       " left join adm_mst_tmodule b on a.module_gid = b.module_gid " +
            //       " left join adm_mst_tprivilege c on c.module_gid = b.module_gid " +
            //       " left join hrm_mst_temployee d on d.employee_gid = a.employee_gid " +
            //       " where a.employee_gid = '" + employee_gid + "' and b.module_gid <> 'HLP' order by module_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getrequesttype_list = new List<pagename_list>();
            if (dt_datatable.Rows.Count != 0)
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getrequesttype_list.Add(new pagename_list
                    {
                        page_gid = (dr_datarow["module_gid"].ToString()),
                        page_name = (dr_datarow["module_name"].ToString()),

                    });

                }

            values.pagename_list = getrequesttype_list;

            dt_datatable.Dispose();
            values.status = true;
            return true;
        }

        public bool DaGetfaqSummary(string user_gid, string module_gid, FaqList values)
        {

            msSQL = "select distinct(a.faqdocument_gid),a.document_name,a.document_ques,a.document_ans,a.page_name,c.module_name from hlp_trn_tfaqdocument a " +
                 "left join adm_mst_tprivilege b on a.modulemenu_gid = b.module_gid " +
                 "left join adm_mst_tmodule c on c.module_gid = a.module_gid where b.user_gid='" + user_gid + "' and a.module_gid='" + module_gid + "' order by a.module_gid";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getfaqsummary = new List<faqsummary>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {

                    getfaqsummary.Add(new faqsummary
                    {
                        document_name = dt["document_name"].ToString(),
                        faqdocument_gid = dt["faqdocument_gid"].ToString(),
                        document_ques = dt["document_ques"].ToString(),
                        document_ans = dt["document_ans"].ToString(),
                        page_name = dt["page_name"].ToString(),
                        module_name = dt["module_name"].ToString(),
                    });


                }
                values.faqsummary = getfaqsummary;
                values.status = true;
            }
            else
            {
                values.status = false;
                values.message = "No Records Found";
            }
            dt_datatable.Dispose();
            return true;
        }


        public bool DaGetreplyofquerySummary(string user_gid, string module_gid, replyquerylist values)
        {
           
            msSQL = "select distinct(a.query_title),a.query_description,b.reply_cause,b.reply_resolve,b.reply_avoid,a.raisequery_gid,a.page_name from hlp_mst_traisequery a " +
                   "left join hlp_mst_treplyquery b on a.raisequery_gid = b.raisequery_gid " +
                   "left join adm_mst_tprivilege c on a.module_gid = c.module_gid " +
                   "where a.moduleparent_gid =  '" + module_gid + "' and a.raisequery_gid in (select raisequery_gid from hlp_mst_treplyquery)  order by a.raisequery_gid desc";

            //msSQL = "Select raisequery_gid,query_code,query_title,query_description,date_format(created_date,'%d-%m-%Y') as created_date,query_status,concat(moduleparent_name,'/',page_name) as module "+
            //        "from hlp_mst_traisequery a left join adm_mst_tprivilege b on a.module_gid=b.module_gid where a.module_gid = '" + module_gid + "'  order by raisequery_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getreplyquerysummary = new List<replyquerysummary>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {

                    getreplyquerysummary.Add(new replyquerysummary
                    {
                        raisequery_gid = dt["raisequery_gid"].ToString(),
                        query_title = dt["query_title"].ToString(),
                        query_description = dt["query_description"].ToString(),
                        reply_cause = dt["reply_cause"].ToString(),
                        reply_resolve = dt["reply_resolve"].ToString(),
                        reply_avoid = dt["reply_avoid"].ToString(),
                        page_name = dt["page_name"].ToString(),
                    });


                }
                values.replyquerysummary = getreplyquerysummary;
                values.status = true;
            }
            else
            {
                values.status = false;
                values.message = "No Records Found";
            }
            dt_datatable.Dispose();
            return true;
        }


        public bool DaGetquerySummary(string employee_gid, string module_gid, querylist values)
        {

            msSQL = "select distinct(query_title),query_code,query_description,raisequery_gid,page_name,moduleparent_gid from hlp_mst_traisequery a " +
                   "left join adm_mst_tprivilege b on a.module_gid = b.module_gid " +
                   "where a.moduleparent_gid =  '" + module_gid + "' and a.created_by='" + employee_gid + "' and a.raisequery_gid not in(select raisequery_gid from hlp_mst_treplyquery)  order by a.raisequery_gid desc";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getquerysummary = new List<querysummary>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {

                    getquerysummary.Add(new querysummary
                    {
                        moduleparent_gid = dt["moduleparent_gid"].ToString(),
                        query_code = dt["query_code"].ToString(),
                        query_title = dt["query_title"].ToString(),
                        query_description = dt["query_description"].ToString(),
                        raisequery_gid = dt["raisequery_gid"].ToString(),
                        page_name = dt["page_name"].ToString(),
                    });


                }
                values.querysummary = getquerysummary;
                values.status = true;
            }
            else
            {
                values.status = false;
                values.message = "No Records Found";
            }
            dt_datatable.Dispose();
            return true;
        }


        public bool DaGetmodule(string user_gid, moduleList values)
        {
            msSQL = "select distinct SUBSTRING(a.module_gid, 1, 3 ) as module_gid,b.module_name from adm_mst_tprivilege a " +
                    "left join adm_mst_tmodule b on a.module_gid = b.module_gid where b.module_gid_parent = '$' and " +
                    " a.user_gid = '" + user_gid + "' and b.module_gid <> 'HLP'";
                    
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getusefulresourcemodulesummary = new List<modulesummary>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {

                    getusefulresourcemodulesummary.Add(new modulesummary
                    {
                        module_name = dt["module_name"].ToString(),
                        module_gid = dt["module_gid"].ToString(),
                    });


                }
                values.modulesummary = getusefulresourcemodulesummary;
                values.status = true;
            }
            else
            {
                values.status = false;
                values.message = "No Records Found";
            }
            dt_datatable.Dispose();
            return true;
        }

        public bool DaGetsinglemodule(string user_gid, modulename values)
        {
            msSQL = "select distinct SUBSTRING(a.module_gid, 1, 3 ) as module_gid,b.module_name from adm_mst_tprivilege a " +
                    "left join adm_mst_tmodule b on a.module_gid = b.module_gid where b.module_gid_parent = '$' and a.user_gid = '" + user_gid + "' and b.module_gid <> 'HLP'";
                   
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows)
            {
                values.module_gid = objODBCDataReader["module_gid"].ToString();
                values.module_name = objODBCDataReader["module_name"].ToString();

            }

            else
            {
                values.status = false;
                values.message = "No Records Found";
            }
            return true;
        }

        public bool DaGetcompanylogo( string user_gid, logo values)
        {
            msSQL = "select company_code,company_uilogo_path from adm_mst_tcompany";

            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows)
            {
                values.company_uilogo_path = objODBCDataReader["company_uilogo_path"].ToString();
                values.company_code = objODBCDataReader["company_code"].ToString();

            }

            else
            {
                values.status = false;
                values.message = "No Records Found";
            }
            return true;
        }

        public bool DaGetsinglemodulename(string user_gid, string module_gid, modulename values)
        {
            msSQL = "select module_gid,module_name from adm_mst_tmodule where module_gid ='" + module_gid + "'";

            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows)
            {
                values.module_gid = objODBCDataReader["module_gid"].ToString();
                values.module_name = objODBCDataReader["module_name"].ToString();

            }

            else
            {
                values.status = false;
                values.message = "No Records Found";
            }
            return true;
        }

        public bool DaGetusefulresourceSummary(string user_gid, string module_gid, UsefulResourceList values)
        {

            msSQL = "select distinct(a.usefulresourcesdocument_gid),a.document_name,a.document_title,a.page_name,c.module_name from hlp_trn_tusefulresourcesdocument a " +
                 "left join adm_mst_tprivilege b on a.modulemenu_gid = b.module_gid " +
                 "left join adm_mst_tmodule c on c.module_gid = a.module_gid where b.user_gid='" + user_gid + "' and a.module_gid='" + module_gid + "' order by a.module_gid";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getusefulresourcesummary = new List<usefulresourcesummary>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {

                    getusefulresourcesummary.Add(new usefulresourcesummary
                    {
                        document_name = dt["document_name"].ToString(),
                        usefulresourcesdocument_gid = dt["usefulresourcesdocument_gid"].ToString(),
                        document_title = dt["document_title"].ToString(),
                        page_name = dt["page_name"].ToString(),
                        module_name = dt["module_name"].ToString(),
                    });


                }
                values.usefulresourcesummary = getusefulresourcesummary;
                values.status = true;
            }
            else
            {
                values.status = false;
                values.message = "No Records Found";
            }
            dt_datatable.Dispose();
            return true;
        }

        public bool DaGethowtouseSummary(string user_gid, string module_gid, HowToUseList values)
        {

            msSQL = "select distinct(a.howtousedocument_gid),a.document_name,a.document_title,a.page_name,c.module_name from hlp_trn_thowtousedocument a " +
                   "left join adm_mst_tprivilege b on a.modulemenu_gid = b.module_gid " +
                   "left join adm_mst_tmodule c on c.module_gid = a.module_gid where b.user_gid='" + user_gid + "' and a.module_gid='" + module_gid + "' order by a.module_gid";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var gethowtousesummary = new List<howtousesummary>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {

                    gethowtousesummary.Add(new howtousesummary
                    {
                        document_name = dt["document_name"].ToString(),
                        howtousedocument_gid = dt["howtousedocument_gid"].ToString(),
                        document_title = dt["document_title"].ToString(),
                        page_name = dt["page_name"].ToString(),
                        module_name = dt["module_name"].ToString(),
                    });


                }
                values.howtousesummary = gethowtousesummary;
                values.status = true;
            }
            else
            {
                values.status = false;
                values.message = "No Records Found";
            }
            dt_datatable.Dispose();
            return true;
        }


        public bool DaGetUsefulDocument(usedocument values, string usefulresourcesdocument_gid)
        {
            msSQL = "select document_path from hlp_trn_tusefulresourcesdocument where usefulresourcesdocument_gid='" + usefulresourcesdocument_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows)
            {
                values.document_path = objODBCDataReader["document_path"].ToString();
                values.status = true;

            }
            else
            {
                values.status = false;
                values.message = "No Records Found";
            }

            return true;
        }

        public bool DaPostattachPhoto(HttpRequest httpRequest, uploaddocument objfilename, string employee_gid, string user_gid)
        {
            upload_list objdocumentmodel = new upload_list();
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
            path = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "/Help/QueryDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
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

                        if ((FileExtension == ".jpg") || (FileExtension == ".jpeg") || (FileExtension == ".png"))
                        {
                            lsfile_gid = lsfile_gid + FileExtension;
                            ls_readStream = httpPostedFile.InputStream;
                            ls_readStream.CopyTo(ms);
                            lspath = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "/Help/QueryDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                            objcmnfunctions.uploadFile(lspath, lsfile_gid);
                            lspath = "../../erp_documents" + "/" + lscompany_code + "/" + "/Help/QueryDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                            msSQL = " insert into hlp_tmp_tquerydocument( " +
                            " path," +
                            " file_name," +
                            " created_by," +
                            " created_date" +
                            " )values(" +
                            "'" + lspath + lsfile_gid + "'," +
                            "'" + httpPostedFile.FileName + "'," +
                            "'" + user_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            if (mnResult == 1)
                            {
                                objfilename.status = true;
                                objfilename.message = "Screenshot Uploaded Successfully..!";
                            }
                            else
                            {
                                objfilename.status = false;
                                objfilename.message = "Error Occured..!";
                            }
                        }
                        else
                        {
                            objfilename.status = false;
                            objfilename.message = "File Format is Not Supported..!";
                        }
                    }
                }
            }
            catch
            {

            }



            return true;
        }

        public void DaPostqueryCreate(mdlquery values, string employee_gid,string user_gid)
        {

            msSQL = "select company_name from adm_mst_tcompany";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                objODBCDataReader.Read();
                lscompany_name = objODBCDataReader["company_name"].ToString();

            }
            objODBCDataReader.Close();

            msSQL = "select user_code from adm_mst_tuser where user_gid='" + user_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                objODBCDataReader.Read();
                lsuser_code = objODBCDataReader["user_code"].ToString();

            }
            objODBCDataReader.Close();

            msSQL = "select module_name from adm_mst_tmodule where module_gid='" + values.page_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                objODBCDataReader.Read();
                lsmodule_name = objODBCDataReader["module_name"].ToString();

            }
            objODBCDataReader.Close();
            msSQL = "select SUBSTRING(module_gid,1,3) as module_gid from adm_mst_tmodule where module_gid='" + values.page_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                objODBCDataReader.Read();
                lsmodule_gid = objODBCDataReader["module_gid"].ToString();
                msSQL = "select module_name from adm_mst_tmodule where module_gid='" + lsmodule_gid + "'";
                objODBCDataReader1 = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader1.HasRows == true)
                {
                    objODBCDataReader.Read();
                    lsmoduleparent_name = objODBCDataReader1["module_name"].ToString();

                }
                objODBCDataReader1.Close();
            }
            objODBCDataReader.Close();
            msGetGid = objcmnfunctions.GetMasterGID("HLPQ");
            msGetGidcode = objcmnfunctions.GetMasterGID("QC");
            msSQL = " insert into hlp_mst_traisequery (" +
                   " raisequery_gid, " +
                   " query_code, " +
                   " query_status, " +
                   " query_title, " +
                   " query_description," +
                   " module_gid," +
                   " company_name," +
                   " user_code," +
                   " page_name," +
                   " moduleparent_name," +
                   " moduleparent_gid," +
                   " created_by," +
                   " created_date)" +
                   " values (" +
                   "'" + msGetGid + "', " +
                   "'" + msGetGidcode + "'," +
                   "'Progress'," +
                   "'" + values.txt_querytitle.Replace("'", "") + "', " +
                   "'" + values.txtquerydescription.Replace("'", "") + "', " +
                   "'" + values.page_gid + "'," +
                   "'" + lscompany_name + "', " +
                   "'" + lsuser_code + "', " +
                   "'" + lsmodule_name + "', " +
                   "'" + lsmoduleparent_name + "'," +
                   "'" + lsmodule_gid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "select path,file_name from hlp_tmp_tquerydocument where created_by='" + user_gid + "'";

            dt_datatable = objdbconn.GetDataTable(msSQL);

            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    msGetGIDDOC = objcmnfunctions.GetMasterGID("HLQD");
                    msSQL = " insert into hlp_trn_tquerydocument( " +
                          " querydocument_gid," +
                          " raisequery_gid," +
                          " document_path," +
                          " document_name," +
                          " created_by," +
                          " created_date" +
                          " )values(" +
                          "'" + msGetGIDDOC + "'," +
                          "'" + msGetGid + "'," +
                          "'" + dr_datarow["path"].ToString() + "'," +
                          "'" + dr_datarow["file_name"].ToString() + "'," +
                          "'" + employee_gid + "'," +
                          "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    dt_datatable.Dispose();
                }

                msSQL = "delete from hlp_tmp_tquerydocument where created_by='" + user_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
           
            msSQL = "select pop_username,pop_password,pop_server,pop_port,company_name from adm_mst_tcompany";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                objODBCDataReader.Read();
                supportmail = objODBCDataReader["pop_username"].ToString();
                emailpassword = objODBCDataReader["pop_password"].ToString();
                lspop_server = objODBCDataReader["pop_server"].ToString();
                lspop_port = objODBCDataReader["pop_port"].ToString();
                company_name = objODBCDataReader["company_name"].ToString();
            }
            objODBCDataReader.Close();

            msSQL = "select Concat(c.user_firstname,' ',c.user_lastname) as username,b.employee_emailid from hlp_mst_traisequery a " +
                    "left join hrm_mst_temployee  b on a.created_by= b.employee_gid " +
                    "left join adm_mst_tuser c on c.user_gid=b.user_gid where a.module_gid='" + values.page_gid + "' ";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                objODBCDataReader.Read();
                raise_by = objODBCDataReader["username"].ToString();
                employeemail_id = objODBCDataReader["employee_emailid"].ToString();
            }
            objODBCDataReader.Close();

            message = "Hi Sir/Madam,  <br />";
            message = message + "<br />";
            message = message + "<b>Query Titile :-</b>" + values.txt_querytitle.Replace("'", "")+ "<br />";
            message = message + "<br />";
            message = message + "<b>Query From :-</b>" + company_name + "<br />";
            message = message + "<br />";
            message = message + "The Query has been raised in the Following Page <br />";
            message = message + "<br />";
            message = message + "<b>Module Name:-</b>" + lsmoduleparent_name + "<br />";
            message = message + "<br />";
            message = message + "<b>Page Name :</b> " + lsmodule_name + "<br />";
            message = message + "<br />";
            message = message + "<b>Kindly Support me to solve this issue</b> <br />";
            message = message + "<br />";
            message = message + "<br />";
            message = message + " Thanks and Regards  <br />";
            message = message + " " + raise_by + "<br /><br />";

            mailflag = objcmnfunctions.SendSMTP2(supportmail, emailpassword, employeemail_id, " '" + raise_by + "' Raised A Query", message, "", "", "");
            if (mailflag != 0)
            {
                values.status = true;
                values.message = "Query Raised Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
            }

            values.moduleparent_name = lsmodule_gid;
        }

        public bool DaGetattachPhoto(attachphotolist values, string user_gid)
        {

            msSQL = "select id,path,file_name from hlp_tmp_tquerydocument where created_by='" + user_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_visitreportdtl = new List<attachphoto>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    get_visitreportdtl.Add(new attachphoto
                    {                       
                        path = HttpContext.Current.Server.MapPath(dt["path"].ToString()),
                        file_name = dt["file_name"].ToString(),
                        id = dt["id"].ToString(),
                    });
                }
                values.attachphoto = get_visitreportdtl;
            }
            dt_datatable.Dispose();

            return true;
        }


        public bool DaattachphotoCancel(string id, attachphotolist values)
        {

            msSQL = "delete from hlp_tmp_tquerydocument where id='" + id + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Screenshot Deleted Successfully..!";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
                return false;
            }
        }

        public bool DaattachphotooverallCancel(attachphotolist values, string user_gid)
        {

            msSQL = "delete from hlp_tmp_tquerydocument where created_by='" + user_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Photo Deleted Successfully..!";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
                return false;
            }
        }

        public bool DaqueryCancel(string raisequery_gid, querylist values)
        {

            msSQL = "delete from hlp_mst_traisequery where raisequery_gid='" + raisequery_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from hlp_trn_tquerydocument where raisequery_gid='" + raisequery_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Query Deleted Successfully..!";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
                return false;
            }
        }

    }
}