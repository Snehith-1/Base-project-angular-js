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
using System.Net.Mail;
using System.Net;

namespace ems.lgl.DataAccess
{
    public class DaRequestcompliance
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        OdbcDataReader objodbcdatareader;
        DataTable dt_datatable, dt_leveltwo;
        DataTable dt_table;
        string msSQL, msGetGid, lsdocument_type, msGETgidREF, msGetDocumentGid;
        string ls_server, ls_username, ls_password, lsto_mail, frommail_id, tomail_id, tomailid_list, ccmail_id, ccmailid_list, body, sub, lscontent, lawyeruser_password, lawyeruser_code, lawyeruser_name = string.Empty;
        string lsrequestref_no, lsrequest_type, lsraised_by, lsraised_date;
        String lawyerfirmuser_password;
        int mnresult, ls_port;
        HttpPostedFile httpPostedFile;
        string lspath, lssession_user, lsresponse_flag;
        //Submit Request Compliance
        public bool DaPostRequestCompliance(string employee_gid, MdlRequestcompliance values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("RCOM");
            msGETgidREF = objcmnfunctions.GetMasterGID("RC_");
            msSQL = "insert into lgl_mst_trequestcompliance(" +
                      " requestcompliance_gid  ," +
                      " requestref_no ," +
                      " request_type ," +
                      " requesttype_gid ," +
                      " request_date ," +
                      " others_title ," +
                      " remarks ," +
                      " created_by," +
                      " created_date)" +
                      " values(" +
                      "'" + msGetGid + "'," +
                      "'" + msGETgidREF + "'," +
                      "'" + values.request_type + "'," +
                      "'" + values.requesttype_gid + "'," +
                      "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',";
            if (values.others_title == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.others_title.Replace("'", "") + "',";
            }
            msSQL += "'" + values.remarks.Replace("'", "") + "'," +
             "'" + employee_gid + "'," +
             "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnresult != 0)
            {
                msSQL = "update lgl_tmp_tuploadcompliancedocument set requestcompliance_gid='" + msGetGid + "' where requestcompliance_gid='" + employee_gid + "' " +
                        " and created_by='" + employee_gid + "'";
                mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
                values.status = true;
                values.message = "Request Compliance Added Sucessfully";

                try
                {

                    msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                    objodbcdatareader = objdbconn.GetDataReader(msSQL);
                    if (objodbcdatareader.HasRows == true)
                    {
                        objodbcdatareader.Read();
                        ls_server = objodbcdatareader["pop_server"].ToString();
                        ls_port = Convert.ToInt32(objodbcdatareader["pop_port"]);
                        ls_username = objodbcdatareader["pop_username"].ToString();
                        ls_password = objodbcdatareader["pop_password"].ToString();

                    }
                    objodbcdatareader.Close();


                    MailMessage message = new MailMessage();
                    SmtpClient smtp = new SmtpClient();

                    msSQL = "select tomailid from ocs_trn_ttomaillist where mailtrigger_function='Legal - Request Compliance'";

                    dt_table = objdbconn.GetDataTable(msSQL);
                    foreach (DataRow dr_datarow in dt_table.Rows)
                    {
                        tomail_id = "";
                        tomail_id += dr_datarow["tomailid"].ToString();
                        message.To.Add(new MailAddress(tomail_id));
                        tomailid_list += "" + tomail_id + ",";
                    }
                    tomailid_list = tomailid_list.TrimEnd(',');
                    dt_table.Dispose();

                    msSQL = "select employee_mailid from ocs_trn_tmailcclist where mailtrigger_function='Legal - Request Compliance'";

                    dt_table = objdbconn.GetDataTable(msSQL);
                    foreach (DataRow dr_datarow in dt_table.Rows)
                    {
                        ccmail_id = "";
                        ccmail_id += dr_datarow["employee_mailid"].ToString();
                        message.CC.Add(new MailAddress(ccmail_id));
                        ccmailid_list += "" + ccmail_id + ",";
                    }
                    ccmailid_list = ccmailid_list.TrimEnd(',');
                    dt_table.Dispose();

                    msSQL = " select requestref_no,request_type," +
                            " concat(c.user_firstname, ' ', c.user_lastname, '/', c.user_code) as raised_by, " +
                            " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as raised_date " +
                            " from lgl_mst_trequestcompliance a " +
                            " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                            " left join adm_mst_tuser c on b.user_gid = c.user_gid" +
                            " where requestcompliance_gid ='" + msGetGid + "'";
                    objodbcdatareader = objdbconn.GetDataReader(msSQL);
                    if (objodbcdatareader.HasRows == true)
                    {
                        lsrequestref_no = objodbcdatareader["requestref_no"].ToString();
                        lsrequest_type = objodbcdatareader["request_type"].ToString();
                        lsraised_by = objodbcdatareader["raised_by"].ToString();
                        lsraised_date = objodbcdatareader["raised_date"].ToString();
                    }


                    objodbcdatareader.Close();

                    sub = " Legal Compliance ";

                    body = "Dear Sir/Madam,  <br />";
                    body = body + "<br />";
                    body = body + "Greetings,  <br />";
                    body = body + "<br />";
                    body = body + "<b>" + HttpUtility.HtmlEncode(lsraised_by) + "</b>" + " has raised Legal Compliance in Legal. Kindly do the needful.<br />";
                    body = body + "<br />";
                    body = body + "<b>Ref No :</b> " + HttpUtility.HtmlEncode(lsrequestref_no) + "<br />";
                    body = body + "<br />";
                    body = body + "<b>Raised Date :</b> " + lsraised_date + "<br />";
                    body = body + "<br />";
                    body = body + "<b>Category :</b> " + HttpUtility.HtmlEncode(lsrequest_type) + "<br />";
                    body = body + "<br />";


                    body = body + "<b>Yours Sincerely,</b> ";
                    body = body + "<br />";
                    body = body + "Samunnati Financial Intermediation & Services Pvt Ltd.<br /> ";
                    body = body + "<br />";
                    body = body + "<b>Please Note:</b> " + "This is an auto generated e-mail that cannot receive replies. " + "<br />";
                    body = body + "<br />";




                    message.From = new MailAddress(ls_username);


                    message.Subject = sub;
                    message.IsBodyHtml = true; //to make message body as html  
                    message.Body = body;
                    smtp.Port = ls_port;
                    smtp.Host = ls_server; //for gmail host  
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Send(message);

                    values.status = true;

                    if (values.status == true)
                    {
                        msSQL = "Insert into lgl_trn_requestcompliancesentmail( " +
                        " requestcompliance_gid," +
                        " from_mail," +
                        " to_mail," +
                        " cc_mail," +
                        " mail_status," +
                        " mail_senddate, " +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + ls_username + "'," +
                        "'" + tomailid_list + "'," +
                        "'" + ccmailid_list + "'," +
                        "'Request Compliance Raised'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                        mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }

                }
                catch (Exception ex)
                {
                    values.message = ex.ToString();
                    values.status = false;
                }

                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while adding";
                return false;
            }

        }
        public bool DagetrequestCompliancesummary(MdlRequestcompliance objrequestCompliance, string employee_gid)
        {
            msSQL = "select requestcompliance_gid,request_type,requesttype_gid,date_format(request_date,'%d-%m-%Y') as request_date,a.status," +
                " requestref_no,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code,' / ',d.department_name) as requested_by,others_title," +
                " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date from lgl_mst_trequestcompliance a,hrm_mst_temployee b , adm_mst_tuser c ,hrm_mst_tdepartment d " +
                " where a.created_by=b.employee_gid and b.user_gid=c.user_gid and d.department_gid=b.department_gid and a.created_by='" + employee_gid + "' order by a.requestcompliance_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getrequestcompliance = new List<requestcompliance_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    msSQL = " SELECT requestcompliance_gid FROM lgl_trn_trequestcompliance2query " +
                            " WHERE requestcompliance_gid='" + dr_datarow["requestcompliance_gid"].ToString() + "' AND response_new='Y' and requester_gid <>'" + employee_gid + "'";
                    objodbcdatareader = objdbconn.GetDataReader(msSQL);
                    if (objodbcdatareader.HasRows == true)
                    {
                        lsresponse_flag = "Y";
                        objodbcdatareader.Close();
                    }
                    else
                    {
                        lsresponse_flag = "N";
                        objodbcdatareader.Close();
                    }


                    getrequestcompliance.Add(new requestcompliance_list
                    {
                        requestcompliance_gid = (dr_datarow["requestcompliance_gid"].ToString()),
                        request_type = (dr_datarow["request_type"].ToString()),
                        request_date = (dr_datarow["request_date"].ToString()),
                        requesttype_gid = (dr_datarow["requesttype_gid"].ToString()),
                        others_title = (dr_datarow["others_title"].ToString()),
                        requestref_no = (dr_datarow["requestref_no"].ToString()),
                        requested_by = (dr_datarow["requested_by"].ToString()),
                        response_flag = lsresponse_flag,
                        request_status = dr_datarow["status"].ToString(),
                        created_date = (dr_datarow["created_date"].ToString()),
                    });
                }
                objrequestCompliance.requestcompliance_list = getrequestcompliance;
            }

            objrequestCompliance.status = true;
            dt_datatable.Dispose();
            return true;
        }

        //Manage Compliance Summary
        public bool DaGetComplianceSummary(MdlRequestcompliance objrequestCompliance, string employee_gid)
        {
            //Pending List
            msSQL = "select requestcompliance_gid,request_type,date_format(request_date,'%d-%m-%Y') as request_date,a.status,others_title," +
                " requesttype_gid,requestref_no,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code,' / ',d.department_name) as requested_by," +
                " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date from lgl_mst_trequestcompliance a,hrm_mst_temployee b , adm_mst_tuser c,hrm_mst_tdepartment d " +
                " where a.created_by=b.employee_gid and b.user_gid=c.user_gid and d.department_gid=b.department_gid " +
                " and status='Pending' and tag_flag='N' order by  a.requestcompliance_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getrequestcompliance = new List<requestcompliance_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    msSQL = " SELECT requestcompliance_gid FROM lgl_trn_trequestcompliance2query " +
                          " WHERE requestcompliance_gid='" + dr_datarow["requestcompliance_gid"].ToString() + "' AND response_new='Y' and created_by<>'" + employee_gid + "'";
                    objodbcdatareader = objdbconn.GetDataReader(msSQL);
                    if (objodbcdatareader.HasRows == true)
                    {
                        lsresponse_flag = "Y";
                        objodbcdatareader.Close();
                    }
                    else
                    {
                        lsresponse_flag = "N";
                        objodbcdatareader.Close();
                    }

                    getrequestcompliance.Add(new requestcompliance_list
                    {
                        requestcompliance_gid = (dr_datarow["requestcompliance_gid"].ToString()),
                        request_type = (dr_datarow["request_type"].ToString()),
                        request_date = (dr_datarow["request_date"].ToString()),
                        requesttype_gid = (dr_datarow["requesttype_gid"].ToString()),
                        others_title = (dr_datarow["others_title"].ToString()),
                        requestref_no = (dr_datarow["requestref_no"].ToString()),
                        requested_by = (dr_datarow["requested_by"].ToString()),
                        response_flag = lsresponse_flag,
                        request_status = dr_datarow["status"].ToString(),
                        created_date = (dr_datarow["created_date"].ToString())
                    });
                }
                objrequestCompliance.requestcompliance_list = getrequestcompliance;
            }
            dt_datatable.Dispose();
            objrequestCompliance.status = true;
            //Work In Progess
            msSQL = "select requestcompliance_gid,request_type,date_format(request_date,'%d-%m-%Y') as request_date,a.status,others_title," +
                " requesttype_gid,requestref_no,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code,' / ',d.department_name) as requested_by," +
                " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date from lgl_mst_trequestcompliance a,hrm_mst_temployee b , adm_mst_tuser c,hrm_mst_tdepartment d " +
                " where a.created_by=b.employee_gid and b.user_gid=c.user_gid and d.department_gid=b.department_gid " +
                " and status='Work In Progress' and tag_flag='N' order by  a.requestcompliance_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getrequestcomplianceworkinprogress_list = new List<requestcomplianceworkinprogress_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    msSQL = " SELECT requestcompliance_gid FROM lgl_trn_trequestcompliance2query " +
                          " WHERE requestcompliance_gid='" + dr_datarow["requestcompliance_gid"].ToString() + "' AND response_new='Y' and created_by<>'" + employee_gid + "'";
                    objodbcdatareader = objdbconn.GetDataReader(msSQL);
                    if (objodbcdatareader.HasRows == true)
                    {
                        lsresponse_flag = "Y";
                        objodbcdatareader.Close();
                    }
                    else
                    {
                        lsresponse_flag = "N";
                        objodbcdatareader.Close();
                    }

                    getrequestcomplianceworkinprogress_list.Add(new requestcomplianceworkinprogress_list
                    {
                        requestcompliance_gid = (dr_datarow["requestcompliance_gid"].ToString()),
                        request_type = (dr_datarow["request_type"].ToString()),
                        request_date = (dr_datarow["request_date"].ToString()),
                        requesttype_gid = (dr_datarow["requesttype_gid"].ToString()),
                        others_title = (dr_datarow["others_title"].ToString()),
                        requestref_no = (dr_datarow["requestref_no"].ToString()),
                        requested_by = (dr_datarow["requested_by"].ToString()),
                        response_flag = lsresponse_flag,
                        request_status = dr_datarow["status"].ToString(),
                        created_date = (dr_datarow["created_date"].ToString())
                    });
                }
                objrequestCompliance.requestcomplianceworkinprogress_list = getrequestcomplianceworkinprogress_list;
            }
            dt_datatable.Dispose();
            objrequestCompliance.status = true;
            //Rejected list
            msSQL = "select requestcompliance_gid,request_type,date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date,concat(f.user_firstname,' ',f.user_lastname) as updated_by,a.status,others_title," +
                " requesttype_gid,requestref_no,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code,' / ',d.department_name) as requested_by," +
                " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date from lgl_mst_trequestcompliance a,hrm_mst_temployee b , adm_mst_tuser c,hrm_mst_tdepartment d, hrm_mst_temployee e , adm_mst_tuser f" +
                " where a.created_by=b.employee_gid and b.user_gid=c.user_gid and d.department_gid=b.department_gid and a.updated_by=e.employee_gid and e.user_gid=f.user_gid" +
                " and status='Rejected'  order by  a.requestcompliance_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getrequestcompliancerejected_list = new List<requestcompliancerejected_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    msSQL = " SELECT requestcompliance_gid FROM lgl_trn_trequestcompliance2query " +
                          " WHERE requestcompliance_gid='" + dr_datarow["requestcompliance_gid"].ToString() + "' AND response_new='Y' and created_by<>'" + employee_gid + "'";
                    objodbcdatareader = objdbconn.GetDataReader(msSQL);
                    if (objodbcdatareader.HasRows == true)
                    {
                        lsresponse_flag = "Y";
                        objodbcdatareader.Close();
                    }
                    else
                    {
                        lsresponse_flag = "N";
                        objodbcdatareader.Close();
                    }

                    getrequestcompliancerejected_list.Add(new requestcompliancerejected_list
                    {
                        requestcompliance_gid = (dr_datarow["requestcompliance_gid"].ToString()),
                        request_type = (dr_datarow["request_type"].ToString()),
                        requesttype_gid = (dr_datarow["requesttype_gid"].ToString()),
                        others_title = (dr_datarow["others_title"].ToString()),
                        requestref_no = (dr_datarow["requestref_no"].ToString()),
                        requested_by = (dr_datarow["requested_by"].ToString()),
                        response_flag = lsresponse_flag,
                        request_status = dr_datarow["status"].ToString(),
                        created_date = (dr_datarow["created_date"].ToString()),
                        updated_date = (dr_datarow["updated_date"].ToString()),
                        updated_by = (dr_datarow["updated_by"].ToString())
                    });
                }
                objrequestCompliance.requestcompliancerejected_list = getrequestcompliancerejected_list;
            }
            dt_datatable.Dispose();
            objrequestCompliance.status = true;
            //Completed List
            msSQL = "select requestcompliance_gid,request_type,date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date,concat(f.user_firstname,' ',f.user_lastname) as updated_by,a.status,others_title," +
                " requesttype_gid,requestref_no,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code,' / ',d.department_name) as requested_by," +
                " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date from lgl_mst_trequestcompliance a,hrm_mst_temployee b , adm_mst_tuser c,hrm_mst_tdepartment d, hrm_mst_temployee e, adm_mst_tuser f" +
                " where a.created_by=b.employee_gid and b.user_gid=c.user_gid and d.department_gid=b.department_gid and a.updated_by=e.employee_gid and e.user_gid=f.user_gid" +
                " and status='Completed'  order by  a.requestcompliance_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getrequestcompliancecompleted_list = new List<requestcompliancecompleted_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    msSQL = " SELECT requestcompliance_gid FROM lgl_trn_trequestcompliance2query " +
                          " WHERE requestcompliance_gid='" + dr_datarow["requestcompliance_gid"].ToString() + "' AND response_new='Y' and created_by<>'" + employee_gid + "'";
                    objodbcdatareader = objdbconn.GetDataReader(msSQL);
                    if (objodbcdatareader.HasRows == true)
                    {
                        lsresponse_flag = "Y";
                        objodbcdatareader.Close();
                    }
                    else
                    {
                        lsresponse_flag = "N";
                        objodbcdatareader.Close();
                    }

                    getrequestcompliancecompleted_list.Add(new requestcompliancecompleted_list
                    {
                        requestcompliance_gid = (dr_datarow["requestcompliance_gid"].ToString()),
                        request_type = (dr_datarow["request_type"].ToString()),
                        requesttype_gid = (dr_datarow["requesttype_gid"].ToString()),
                        others_title = (dr_datarow["others_title"].ToString()),
                        requestref_no = (dr_datarow["requestref_no"].ToString()),
                        requested_by = (dr_datarow["requested_by"].ToString()),
                        response_flag = lsresponse_flag,
                        request_status = dr_datarow["status"].ToString(),
                        created_date = (dr_datarow["created_date"].ToString()),
                        updated_date = (dr_datarow["updated_date"].ToString()),
                        updated_by = (dr_datarow["updated_by"].ToString())
                    });
                }
                objrequestCompliance.requestcompliancecompleted_list = getrequestcompliancecompleted_list;
            }
            dt_datatable.Dispose();
            objrequestCompliance.status = true;
            //Tagged to lawyer
            msSQL = "select requestcompliance_gid,request_type,date_format(request_date,'%d-%m-%Y') as request_date,a.status,others_title, " +
                    " requesttype_gid,requestref_no,concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code, ' / ', d.department_name) as requested_by, " +
                    " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date,date_format(a.assigned_date, '%d-%m-%Y %h:%i %p') as assigned_date," +
                    " concat(f.user_firstname, ' ', f.user_lastname, ' / ', f.user_code) as assigned_by " +
                    " from lgl_mst_trequestcompliance a" +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                    " left join adm_mst_tuser c on b.user_gid = c.user_gid" +
                    " left join hrm_mst_tdepartment d on d.department_gid = b.department_gid" +
                    " left join hrm_mst_temployee e on a.assigned_by = e.employee_gid" +
                    " left join adm_mst_tuser f on e.user_gid = f.user_gid" +
                    " where tag_flag = 'Y' and status<> 'Completed' order by  a.assigned_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getrequestcompliancetaggedlawyer_list = new List<requestcompliancetaggedlawyer_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    msSQL = " SELECT requestcompliance_gid FROM lgl_trn_trequestcompliance2query " +
                          " WHERE requestcompliance_gid='" + dr_datarow["requestcompliance_gid"].ToString() + "' AND response_new='Y' and created_by<>'" + employee_gid + "'";
                    objodbcdatareader = objdbconn.GetDataReader(msSQL);
                    if (objodbcdatareader.HasRows == true)
                    {
                        lsresponse_flag = "Y";
                        objodbcdatareader.Close();
                    }
                    else
                    {
                        lsresponse_flag = "N";
                        objodbcdatareader.Close();
                    }

                    getrequestcompliancetaggedlawyer_list.Add(new requestcompliancetaggedlawyer_list
                    {
                        requestcompliance_gid = (dr_datarow["requestcompliance_gid"].ToString()),
                        request_type = (dr_datarow["request_type"].ToString()),
                        request_date = (dr_datarow["request_date"].ToString()),
                        requesttype_gid = (dr_datarow["requesttype_gid"].ToString()),
                        others_title = (dr_datarow["others_title"].ToString()),
                        requestref_no = (dr_datarow["requestref_no"].ToString()),
                        requested_by = (dr_datarow["requested_by"].ToString()),
                        response_flag = lsresponse_flag,
                        request_status = dr_datarow["status"].ToString(),
                        created_date = (dr_datarow["created_date"].ToString()),
                        assigned_by = dr_datarow["assigned_by"].ToString(),
                        assigned_date = (dr_datarow["assigned_date"].ToString())
                    });
                }
                objrequestCompliance.requestcompliancetaggedlawyer_list = getrequestcompliancetaggedlawyer_list;
            }
            dt_datatable.Dispose();
            objrequestCompliance.status = true;
            //--------------Total Count Info--------------------//

            //.........Pending list.......//
            msSQL = "select count(requestcompliance_gid) as pending_count from lgl_mst_trequestcompliance where status='Pending' and tag_flag='N' ";
            objrequestCompliance.pending_count = objdbconn.GetExecuteScalar(msSQL);
            //.......Completed List........//
            msSQL = "select count(requestcompliance_gid) as completed_count from lgl_mst_trequestcompliance where status='Completed'  ";
            objrequestCompliance.completed_count = objdbconn.GetExecuteScalar(msSQL);
            //.......Rejected/Not relevent List......//
            msSQL = "select count(requestcompliance_gid) as rejected_count from lgl_mst_trequestcompliance where status='Rejected'  ";
            objrequestCompliance.rejected_count = objdbconn.GetExecuteScalar(msSQL);
            //.......Work in Progress List.....//
            msSQL = "select count(requestcompliance_gid) as workinprogress_count from lgl_mst_trequestcompliance where status='Work In Progress' and tag_flag='N' ";
            objrequestCompliance.workinprogress_count = objdbconn.GetExecuteScalar(msSQL);
            //------Tagged To Lawyer List-----//
            msSQL = "select count(requestcompliance_gid) as taggedlawyer_count from lgl_mst_trequestcompliance where tag_flag='Y' and status not in ('Completed','Rejected')";
            objrequestCompliance.taggedlawyer_count = objdbconn.GetExecuteScalar(msSQL);
            objrequestCompliance.status = true;
            return true;
        }
        // Manage Compliance - view 360
        public bool DaGetComplianceManagementSummary(string requestcompliance_gid, MdlRequestcompliance values)
        {
            string status = string.Empty;
            msSQL = "select status from lgl_mst_trequestcompliance where requestcompliance_gid ='" + requestcompliance_gid + "'";
            status = objdbconn.GetExecuteScalar(msSQL);

            if (status == "Completed" || status == "Rejected")
            {
                msSQL = "select requestcompliance_gid,concat(request_type,' ',others_title) as request_type,date_format(request_date,'%d-%m-%Y') as request_date,b.employee_photo,a.assign_lawyergid,a.status," +
                    " requesttype_gid,requestref_no,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as requested_by,others_title," +
                    " d.branch_name,e.department_name,f.designation_name,a.remarks,a.rejected_remarks,a.completed_remarks, date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date," +
                    " concat(h.user_firstname,' ',h.user_lastname,' / ',h.user_code) as updated_by from lgl_mst_trequestcompliance a,hrm_mst_temployee b , adm_mst_tuser c ," +
                    " hrm_mst_tbranch d, hrm_mst_tdepartment e,adm_mst_tdesignation f, hrm_mst_temployee g , adm_mst_tuser h" +
                    " where a.created_by=b.employee_gid and b.user_gid=c.user_gid and  b.branch_gid=d.branch_gid and b.department_gid=e.department_gid" +
                    " and b.designation_gid = f.designation_gid and a.updated_by=g.employee_gid and g.user_gid=h.user_gid and requestcompliance_gid ='" + requestcompliance_gid + "' order by  a.requestcompliance_gid desc";
            }
            else
            {
                msSQL = "select requestcompliance_gid,concat(request_type,' ',others_title) as request_type,date_format(request_date,'%d-%m-%Y') as request_date,b.employee_photo,a.assign_lawyergid,a.status," +
                    " requesttype_gid,requestref_no,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as requested_by,others_title," +
                    " d.branch_name,e.department_name,f.designation_name,a.remarks from lgl_mst_trequestcompliance a,hrm_mst_temployee b , adm_mst_tuser c ," +
                    " hrm_mst_tbranch d, hrm_mst_tdepartment e,adm_mst_tdesignation f" +
                    " where a.created_by=b.employee_gid and b.user_gid=c.user_gid and  b.branch_gid=d.branch_gid and b.department_gid=e.department_gid" +
                    " and b.designation_gid = f.designation_gid and requestcompliance_gid ='" + requestcompliance_gid + "' order by  a.requestcompliance_gid desc";
            }
            objodbcdatareader = objdbconn.GetDataReader(msSQL);

            if (objodbcdatareader.HasRows)
            {
                values.requestref_no = objodbcdatareader["requestref_no"].ToString();
                values.request_type = objodbcdatareader["request_type"].ToString();
                values.requesttype_gid = objodbcdatareader["requesttype_gid"].ToString();
                values.others_title = objodbcdatareader["others_title"].ToString();
                // values.deadline_date = objodbcdatareader["deadline_date"].ToString();
                values.request_date = objodbcdatareader["request_date"].ToString();
                values.requested_by = objodbcdatareader["requested_by"].ToString();
                values.branch_name = objodbcdatareader["branch_name"].ToString();
                values.department_name = objodbcdatareader["department_name"].ToString();
                values.designation_name = objodbcdatareader["designation_name"].ToString();
                values.remarks = objodbcdatareader["remarks"].ToString();
                values.requestcompliance_gid = objodbcdatareader["requestcompliance_gid"].ToString();
                values.request_status = objodbcdatareader["status"].ToString();

                if (status == "Completed" || status == "Rejected")
                {
                    values.rejected_remarks = objodbcdatareader["rejected_remarks"].ToString();
                    values.completed_remarks = objodbcdatareader["completed_remarks"].ToString();
                    values.updated_date = objodbcdatareader["updated_date"].ToString();
                    values.updated_by = objodbcdatareader["updated_by"].ToString();
                }

                if (objodbcdatareader["employee_photo"].ToString() == "")
                {
                    values.employee_photo = "N";
                }
                else
                {
                    values.employee_photo = objodbcdatareader["employee_photo"].ToString();
                }
                if (objodbcdatareader["assign_lawyergid"].ToString() != "")
                {
                    values.assign_lawyergid = objodbcdatareader["assign_lawyergid"].ToString();
                    msSQL = "select concat(lawyeruser_name,'/',lawyeruser_code) as  lawyeruser_name,mobile_no,b.email_address  from lgl_mst_tlawyeruser a" +
                            " left join lgl_mst_tregisterlawyer b on a.lawyerregister_gid = b.lawyerregister_gid where" +
                            " lawyeruser_gid='" + objodbcdatareader["assign_lawyergid"].ToString() + "'";
                    objodbcdatareader.Close();
                    objodbcdatareader = objdbconn.GetDataReader(msSQL);
                    if (objodbcdatareader.HasRows == true)
                    {
                        values.assign_lawyername = objodbcdatareader["lawyeruser_name"].ToString();
                        values.assign_emailaddress = objodbcdatareader["email_address"].ToString();
                        values.assign_mobileno = objodbcdatareader["mobile_no"].ToString();
                    }
                }
                else
                {
                    values.assign_lawyergid = "Y";
                }
            }
            objodbcdatareader.Close();
            msSQL = "select uploaddocument_gid,file_path,document_type,concat(file_name,date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as file_name ," +
                " if (correctedfile_name is null ,'---',concat(correctedfile_name,' / ',date_format(a.updated_date,'%d-%m-%Y %H:%i %p'))) as correctedfile_name ," +
                " correctedfile_path,correcteddocument_type,a.remarks,document_corrected,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                " from lgl_tmp_tuploadcompliancedocument a" +
                " left join hrm_mst_temployee b on a.created_by=b.employee_gid " +
                " left join adm_mst_tuser c on b.user_gid=c.user_gid where " +
                " requestcompliance_gid='" + requestcompliance_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_filename = new List<document_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    get_filename.Add(new document_list
                    {
                        file_name = (dr_datarow["file_name"].ToString()),
                        file_path = objcmnstorage.EncryptData(dr_datarow["file_path"].ToString()),
                        uploaddocument_gid = (dr_datarow["uploaddocument_gid"].ToString()),
                        document_type = dr_datarow["document_type"].ToString(),
                        correctedfile_name = dr_datarow["correctedfile_name"].ToString(),
                        correctedfile_path = objcmnstorage.EncryptData(dr_datarow["correctedfile_path"].ToString()),
                        correcteddocument_type = dr_datarow["correcteddocument_type"].ToString(),
                        remarks = dr_datarow["remarks"].ToString(),
                        document_corrected = dr_datarow["document_corrected"].ToString(),
                        created_by = dr_datarow["created_by"].ToString()
                    });
                }
                values.document_list = get_filename;
            }
            dt_datatable.Dispose();

            msSQL = "update lgl_mst_trequestcompliance set request_flag='' where requestcompliance_gid='" + values.requestcompliance_gid + "' and request_flag='Y'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "select response_new,requester_gid from lgl_trn_trequestcompliance2query where requestcompliance_gid='" + requestcompliance_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msSQL = " update lgl_trn_trequestcompliance2query set response_new='' where response_new='Y' and " +
                            " requestcompliance_gid='" + requestcompliance_gid + "'";
                    mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            dt_datatable.Dispose();

            msSQL = " select a.seeklawyer_remarks, date_format(a.assigned_date,'%d-%m-%Y') as assigned_date," +
                   " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as assigned_by from lgl_mst_trequestcompliance a " +
                   " left join hrm_mst_temployee b on a.assigned_by = b.employee_gid " +
                   " left join adm_mst_tuser c on c.user_gid = b.user_gid where requestcompliance_gid='" + requestcompliance_gid + "'";
            objodbcdatareader = objdbconn.GetDataReader(msSQL);
            if (objodbcdatareader.HasRows == true)
            {
                values.assigned_by = objodbcdatareader["assigned_by"].ToString();
                values.seeklawyer_remarks = objodbcdatareader["seeklawyer_remarks"].ToString();
                values.assigned_date = objodbcdatareader["assigned_date"].ToString();

            }
            objodbcdatareader.Close();
            msSQL = " select a.seekdocument_gid,concat(a.document_name,' - ',date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as document_name,a.document_path,a.correctedfile_name,a.lawyer_corrected,a.correctedfile_path,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by " +
                                  " from lgl_trn_tseeklawyerdocument a " +
                                  " left join hrm_mst_temployee b on a.created_by=b.employee_gid " +
                                  " left join adm_mst_tuser c on c.user_gid=b.user_gid" +
                                  " where a.request_compliancegid='" + requestcompliance_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_fileseekname = new List<uploadseek_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    get_fileseekname.Add(new uploadseek_list
                    {
                        seekdocument_gid = (dr_datarow["seekdocument_gid"].ToString()),
                        document_name = dr_datarow["document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData(dr_datarow["document_path"].ToString()),
                        correctedfile_name = (dr_datarow["correctedfile_name"].ToString()),
                        correctedfile_path = objcmnstorage.EncryptData(dr_datarow["correctedfile_path"].ToString()),
                        lawyer_corrected = (dr_datarow["lawyer_corrected"].ToString()),
                        created_by = dr_datarow["created_by"].ToString(),
                    });
                }
                values.uploadseek_list = get_fileseekname;
            }
            dt_datatable.Dispose();



            values.status = true;
            return true;
        }
        //----Request Compliance 360------//
        public bool DaGetrequestComplianceView(string requestcompliance_gid, MdlRequestcompliance values)
        {
            string status = string.Empty;
            msSQL = "select status from lgl_mst_trequestcompliance where requestcompliance_gid ='" + requestcompliance_gid + "'";
            status = objdbconn.GetExecuteScalar(msSQL);

            if (status == "Completed" || status == "Rejected")
            {
                msSQL = "select requestcompliance_gid,concat(request_type,' ',others_title) as request_type,date_format(request_date,'%d-%m-%Y') as request_date," +
               " b.employee_photo,a.assign_lawyergid,a.status," +
               " requesttype_gid,others_title,requestref_no,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as requested_by," +
               " d.branch_name,e.department_name,f.designation_name,a.remarks,a.rejected_remarks,a.completed_remarks,date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date," +
               " concat(h.user_firstname,' ',h.user_lastname,' / ',h.user_code) as updated_by from lgl_mst_trequestcompliance a,hrm_mst_temployee b , adm_mst_tuser c ," +
               " hrm_mst_tbranch d, hrm_mst_tdepartment e,adm_mst_tdesignation f ,hrm_mst_temployee g , adm_mst_tuser h" +
               " where a.created_by=b.employee_gid and b.user_gid=c.user_gid and  b.branch_gid=d.branch_gid and b.department_gid=e.department_gid" +
               " and b.designation_gid = f.designation_gid and a.updated_by=g.employee_gid and g.user_gid=h.user_gid and" +
               " requestcompliance_gid ='" + requestcompliance_gid + "' order by  a.requestcompliance_gid desc";
            }
            else
            {
                msSQL = "select requestcompliance_gid,concat(request_type,' ',others_title) as request_type,date_format(request_date,'%d-%m-%Y') as request_date," +
               " b.employee_photo,a.assign_lawyergid,a.status," +
               "requesttype_gid,others_title,requestref_no,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as requested_by," +
               " d.branch_name,e.department_name,f.designation_name,a.remarks" +
               " from lgl_mst_trequestcompliance a,hrm_mst_temployee b , adm_mst_tuser c ," +
               " hrm_mst_tbranch d, hrm_mst_tdepartment e,adm_mst_tdesignation f" +
               " where a.created_by=b.employee_gid and b.user_gid=c.user_gid and  b.branch_gid=d.branch_gid and b.department_gid=e.department_gid" +
               " and b.designation_gid = f.designation_gid and" +
               " requestcompliance_gid ='" + requestcompliance_gid + "' order by  a.requestcompliance_gid desc";
            }

            objodbcdatareader = objdbconn.GetDataReader(msSQL);

            if (objodbcdatareader.HasRows)
            {
                values.requestref_no = objodbcdatareader["requestref_no"].ToString();
                values.request_type = objodbcdatareader["request_type"].ToString();
                values.requesttype_gid = objodbcdatareader["requesttype_gid"].ToString();
                values.others_title = objodbcdatareader["others_title"].ToString();
                // values.deadline_date = objodbcdatareader["deadline_date"].ToString();
                values.request_date = objodbcdatareader["request_date"].ToString();
                values.requested_by = objodbcdatareader["requested_by"].ToString();
                values.branch_name = objodbcdatareader["branch_name"].ToString();
                values.department_name = objodbcdatareader["department_name"].ToString();
                values.designation_name = objodbcdatareader["designation_name"].ToString();
                values.remarks = objodbcdatareader["remarks"].ToString().Replace("\n", "<br>");


                values.requestcompliance_gid = objodbcdatareader["requestcompliance_gid"].ToString();
                values.request_status = objodbcdatareader["status"].ToString();
                if (status == "Completed" || status == "Rejected")
                {
                    values.rejected_remarks = objodbcdatareader["rejected_remarks"].ToString().Replace("\n", "<br>");
                    values.completed_remarks = objodbcdatareader["completed_remarks"].ToString().Replace("\n", "<br>");
                    values.updated_date = objodbcdatareader["updated_date"].ToString();
                    values.updated_by = objodbcdatareader["updated_by"].ToString();
                }
                if (objodbcdatareader["employee_photo"].ToString() == "")
                {
                    values.employee_photo = "N";
                }
                else
                {
                    values.employee_photo = objodbcdatareader["employee_photo"].ToString();
                }
                if (objodbcdatareader["assign_lawyergid"].ToString() != "")
                {
                    values.assign_lawyergid = objodbcdatareader["assign_lawyergid"].ToString();
                    msSQL = "select concat(lawyeruser_name,'/',lawyeruser_code) as  lawyeruser_name,mobile_no,b.email_address  from lgl_mst_tlawyeruser a" +
                            " left join lgl_mst_tregisterlawyer b on a.lawyerregister_gid = b.lawyerregister_gid where" +
                            " lawyeruser_gid='" + objodbcdatareader["assign_lawyergid"].ToString() + "'";
                    objodbcdatareader.Close();
                    objodbcdatareader = objdbconn.GetDataReader(msSQL);
                    if (objodbcdatareader.HasRows == true)
                    {
                        values.assign_lawyername = objodbcdatareader["lawyeruser_name"].ToString();
                        values.assign_emailaddress = objodbcdatareader["email_address"].ToString();
                        values.assign_mobileno = objodbcdatareader["mobile_no"].ToString();
                    }
                }
                else
                {
                    values.assign_lawyergid = "Y";
                }
            }
            objodbcdatareader.Close();
            msSQL = "select uploaddocument_gid,file_path,document_type,concat(file_name,date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as file_name ," +
                " if (correctedfile_name is null ,'---',concat(correctedfile_name,' / ',date_format(a.updated_date,'%d-%m-%Y %H:%i %p'))) as correctedfile_name ," +
                " correctedfile_path,correcteddocument_type,a.remarks,document_corrected,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                " from lgl_tmp_tuploadcompliancedocument a" +
                " left join hrm_mst_temployee b on a.created_by=b.employee_gid " +
                " left join adm_mst_tuser c on b.user_gid=c.user_gid where " +
                " requestcompliance_gid='" + requestcompliance_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_filename = new List<document_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    get_filename.Add(new document_list
                    {
                        file_name = (dr_datarow["file_name"].ToString()),
                        file_path = objcmnstorage.EncryptData(dr_datarow["file_path"].ToString()),
                        uploaddocument_gid = (dr_datarow["uploaddocument_gid"].ToString()),
                        document_type = dr_datarow["document_type"].ToString(),
                        correctedfile_name = dr_datarow["correctedfile_name"].ToString(),
                        correctedfile_path = objcmnstorage.EncryptData(dr_datarow["correctedfile_path"].ToString()),
                        correcteddocument_type = dr_datarow["correcteddocument_type"].ToString(),
                        remarks = dr_datarow["remarks"].ToString(),
                        document_corrected = dr_datarow["document_corrected"].ToString(),
                        created_by = dr_datarow["created_by"].ToString()
                    });
                }
                values.document_list = get_filename;
            }
            dt_datatable.Dispose();

            msSQL = "update lgl_mst_trequestcompliance set request_flag='' where requestcompliance_gid='" + values.requestcompliance_gid + "' and request_flag='Y'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "select response_new,requester_gid from lgl_trn_trequestcompliance2query where requestcompliance_gid='" + requestcompliance_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msSQL = " update lgl_trn_trequestcompliance2query set response_new='' where response_new='Y' and " +
                            " requestcompliance_gid='" + requestcompliance_gid + "'";
                    mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            dt_datatable.Dispose();

            msSQL = " select a.seeklawyer_remarks, date_format(a.assigned_date,'%d-%m-%Y') as assigned_date," +
                   " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as assigned_by from lgl_mst_trequestcompliance a " +
                   " left join hrm_mst_temployee b on a.assigned_by = b.employee_gid " +
                   " left join adm_mst_tuser c on c.user_gid = b.user_gid where requestcompliance_gid='" + requestcompliance_gid + "'";
            objodbcdatareader = objdbconn.GetDataReader(msSQL);
            if (objodbcdatareader.HasRows == true)
            {
                values.assigned_by = objodbcdatareader["assigned_by"].ToString();
                values.seeklawyer_remarks = objodbcdatareader["seeklawyer_remarks"].ToString();
                values.assigned_date = objodbcdatareader["assigned_date"].ToString();

            }
            objodbcdatareader.Close();
            msSQL = " select a.seekdocument_gid,a.document_name,a.document_path,a.correctedfile_name,a.lawyer_corrected,a.correctedfile_path,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by " +
                                  " from lgl_trn_tseeklawyerdocument a " +
                                  " left join hrm_mst_temployee b on a.created_by=b.employee_gid " +
                                  " left join adm_mst_tuser c on c.user_gid=b.user_gid" +
                                  " where a.request_compliancegid='" + requestcompliance_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_fileseekname = new List<uploadseek_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    get_fileseekname.Add(new uploadseek_list
                    {
                        seekdocument_gid = (dr_datarow["seekdocument_gid"].ToString()),
                        document_path = objcmnstorage.EncryptData(dr_datarow["document_path"].ToString()),
                        document_name = dr_datarow["document_name"].ToString(),
                        correctedfile_name = (dr_datarow["correctedfile_name"].ToString()),
                        correctedfile_path = objcmnstorage.EncryptData(dr_datarow["correctedfile_path"].ToString()),
                        lawyer_corrected = (dr_datarow["lawyer_corrected"].ToString()),
                        created_by = dr_datarow["created_by"].ToString(),
                    });
                }
                values.uploadseek_list = get_fileseekname;
            }
            dt_datatable.Dispose();



            values.status = true;
            return true;
        }
        //Document Upload
        public bool DaUploaddocument(HttpRequest httpRequest, uploaddocument objfilename, string employee_gid, string user_gid)
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
            string document_type = httpRequest.Form["document_type"].ToString();
            string project_flag = httpRequest.Form["project_flag"].ToString();


            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "LGL/DocumentCompliance/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
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
                        lsfile_gid = lsfile_gid + FileExtension;
                        ls_readStream = httpPostedFile.InputStream;
                        ls_readStream.CopyTo(ms);
                        lspath = path;
                        // objcmnfunctions.uploadFile(lspath, lsfile_gid);

                        //lspath = "../../erp_documents" + "/" + lscompany_code + "/" + "LGL/DocumentCompliance/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension;

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.status = false;
                            objfilename.message = "File format is not supported";
                            return false;
                        }

                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "LGL/DocumentCompliance/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "LGL/DocumentCompliance/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";


                        if (document_type == "undefined")
                        {
                            lsdocument_type = "";
                        }
                        else
                        {
                            lsdocument_type = document_type;
                        }
                        msGetGid = objcmnfunctions.GetMasterGID("RCUD");
                        msSQL = " insert into lgl_tmp_tuploadcompliancedocument( " +
                                    " uploaddocument_gid," +
                                    " requestcompliance_gid ," +
                                    " file_name," +
                                    " file_path," +
                                    " document_type ," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + httpPostedFile.FileName + "'," +
                                    "'" + lspath + msdocument_gid + FileExtension + "'," +
                                    "'" + lsdocument_type + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                        mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);


                    }

                }


                msSQL = "select uploaddocument_gid,file_name,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code,' ',date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_by,file_path,document_type " +
                    " from lgl_tmp_tuploadcompliancedocument a,hrm_mst_temployee b , adm_mst_tuser c where a.created_by=b.employee_gid " +
                  " and b.user_gid = c.user_gid and  requestcompliance_gid='" + employee_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_filename = new List<upload_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_filename.Add(new upload_list
                        {
                            file_name = (dr_datarow["file_name"].ToString()),
                            file_path = objcmnstorage.EncryptData((dr_datarow["file_path"].ToString())),
                            uploaddocument_gid = (dr_datarow["uploaddocument_gid"].ToString()),
                            document_type = dr_datarow["document_type"].ToString(),
                            uploaded_details = dr_datarow["uploaded_by"].ToString()
                        });
                    }
                    objfilename.upload_list = get_filename;
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
                objfilename.message = "Error Occured while uploading";
                return false;

            }
        }


        public bool DaPostSeekLawyerUpload(HttpRequest httpRequest, uploaddocument objfilename, string employee_gid, string user_gid)
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
            string project_flag = httpRequest.Form["project_flag"].ToString();


            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "LGL/SeekLawyerDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
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
                        //objcmnfunctions.uploadFile(lspath, lsfile_gid);
                        //lspath = "../../erp_documents" + "/" + lscompany_code + "/" + "LGL/SeekLawyerDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension;

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.status = false;
                            objfilename.message = "File format is not supported";
                            return false;
                        }

                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "LGL/SeekLawyerDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "LGL/SeekLawyerDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";


                        msGetGid = objcmnfunctions.GetMasterGID("RCUD");
                        msSQL = " insert into lgl_tmp_tseeklawyerdocument( " +
                                    " document_name ," +
                                    " document_path," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + httpPostedFile.FileName + "'," +
                                    "'" + lspath + msdocument_gid + FileExtension + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                        mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnresult == 1)
                        {
                            objfilename.status = true;

                        }
                        else
                        {
                            objfilename.status = false;
                        }
                    }
                }
                msSQL = "select tmpseek_documentgid,document_name,document_path,created_date " +
                        " from lgl_tmp_tseeklawyerdocument where created_by='" + employee_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_filename = new List<uploadseek_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_filename.Add(new uploadseek_list
                        {

                            tmpseek_documentgid = (dr_datarow["tmpseek_documentgid"].ToString()),
                            document_name = dr_datarow["document_name"].ToString(),
                            document_path = objcmnstorage.EncryptData(dr_datarow["document_path"].ToString()),
                            //  document_path = HttpContext.Current.Server.MapPath(dr_datarow["document_path"].ToString()),
                            created_date = dr_datarow["created_date"].ToString(),
                        });
                    }
                    objfilename.uploadseek_list = get_filename;
                }
                dt_datatable.Dispose();
            }
            catch
            {

            }
            objfilename.status = true;
            return true;
        }


        public bool DaGetSeekLawyerUploadCancel(string tmpseek_documentgid, uploaddocument objfilename, string employee_gid)
        {

            msSQL = "delete from lgl_tmp_tseeklawyerdocument where tmpseek_documentgid='" + tmpseek_documentgid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "select tmpseek_documentgid,document_name,document_path,created_date " +
                       " from lgl_tmp_tseeklawyerdocument where created_by='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_filename = new List<uploadseek_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    get_filename.Add(new uploadseek_list
                    {

                        tmpseek_documentgid = (dr_datarow["tmpseek_documentgid"].ToString()),
                        document_name = dr_datarow["document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData(dr_datarow["document_path"].ToString()),
                        // document_path = HttpContext.Current.Server.MapPath(dr_datarow["document_path"].ToString()),
                        created_date = dr_datarow["created_date"].ToString(),
                    });
                }
                objfilename.uploadseek_list = get_filename;
            }
            dt_datatable.Dispose();



            if (mnresult != 0)
            {
                objfilename.status = true;
                return true;
            }
            else
            {
                objfilename.status = false;
                return false;
            }

        }

        public bool DaPostAdditionalDocUpload(HttpRequest httpRequest, uploaddocument objfilename, string employee_gid, string user_gid)
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
            string document_type = httpRequest.Form["document_type"].ToString();
            string requestcompliance_gid = httpRequest.Form["requestcompliance_gid"].ToString();
            string project_flag = httpRequest.Form["project_flag"].ToString();



            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";

            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "LGL/DocumentCompliance/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
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
                        //objcmnfunctions.uploadFile(lspath, lsfile_gid);
                        //lspath = "../../erp_documents" + "/" + lscompany_code + "/" + "LGL/DocumentCompliance/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension;

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.status = false;
                            objfilename.message = "File format is not supported";
                            return false;
                        }

                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "LGL/DocumentCompliance/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "LGL/DocumentCompliance/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";


                        if (document_type == "undefined")
                        {
                            lsdocument_type = "";
                        }
                        else
                        {
                            lsdocument_type = document_type;
                        }
                        msGetGid = objcmnfunctions.GetMasterGID("RCUD");
                        msSQL = " insert into lgl_tmp_tuploadcompliancedocument( " +
                                    " uploaddocument_gid," +
                                    " requestcompliance_gid ," +
                                    " file_name," +
                                    " file_path," +
                                    " document_type ," +
                                   " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + requestcompliance_gid + "'," +
                                    "'" + httpPostedFile.FileName + "'," +
                                    "'" + lspath + "'," +
                                    "'" + lsdocument_type + "'," +
                                     "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                        mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnresult == 1)
                        {
                            objfilename.status = true;
                        }
                        else
                        {
                            objfilename.status = false;
                        }
                    }
                }
                msSQL = "select uploaddocument_gid,file_path,document_type,concat(file_name,date_format(created_date,'%d-%m-%Y %H:%i %p')) as file_name ," +
                      " if (correctedfile_name is null ,'---',concat(correctedfile_name,' / ',date_format(updated_date,'%d-%m-%Y %H:%i %p'))) as correctedfile_name ," +
                      " correctedfile_path from lgl_tmp_tuploadcompliancedocument where requestcompliance_gid='" + requestcompliance_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_filename = new List<upload_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_filename.Add(new upload_list
                        {
                            file_name = (dr_datarow["file_name"].ToString()),
                            file_path = objcmnstorage.EncryptData((dr_datarow["file_path"].ToString())),
                            uploaddocument_gid = (dr_datarow["uploaddocument_gid"].ToString()),
                            document_type = dr_datarow["document_type"].ToString(),
                            uploaded_details = dr_datarow["uploaded_by"].ToString()
                        });
                    }
                    objfilename.upload_list = get_filename;
                }
                dt_datatable.Dispose();
            }
            catch
            {

            }
            objfilename.status = true;
            return true;
        }
        public bool DaEdituploaddocument(HttpRequest httpRequest, uploaddocument objfilename, string employee_gid, string user_gid)
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
            string document_type = httpRequest.Form["document_type"].ToString();
            string requestcompliance_gid = httpRequest.Form["requestcompliance_gid"].ToString();
            string project_flag = httpRequest.Form["project_flag"].ToString();

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "LGL/DocumentCompliance/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
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
                        //lspath = "../../erp_documents" + "/" + lscompany_code + "/" + "LGL/DocumentCompliance/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension;
                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "LGL/DocumentCompliance/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "LGL/DocumentCompliance/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";



                        if (document_type == "undefined")
                        {
                            lsdocument_type = "";
                        }
                        else
                        {
                            lsdocument_type = document_type;
                        }
                        msGetGid = objcmnfunctions.GetMasterGID("RCUD");
                        msSQL = " insert into lgl_tmp_tuploadcompliancedocument( " +
                                    " uploaddocument_gid," +
                                    " requestcompliance_gid ," +
                                    " file_name," +
                                    " file_path," +
                                    " document_type ," +
                                     " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + httpPostedFile.FileName + "'," +
                                    "'" + lspath + "'," +
                                    "'" + lsdocument_type + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                        mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnresult == 1)
                        {
                            objfilename.status = true;
                        }
                        else
                        {
                            objfilename.status = false;
                        }
                    }
                }
                msSQL = "select uploaddocument_gid,file_name,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code,' ',date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_by,file_path,document_type " +
                    " from lgl_tmp_tuploadcompliancedocument a,hrm_mst_temployee b , adm_mst_tuser c where a.created_by=b.employee_gid " +
                  " and b.user_gid = c.user_gid and (requestcompliance_gid='" + employee_gid + "' or requestcompliance_gid='" + requestcompliance_gid + "')";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_filename = new List<upload_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_filename.Add(new upload_list
                        {
                            file_name = (dr_datarow["file_name"].ToString()),
                            file_path = objcmnstorage.EncryptData((dr_datarow["file_path"].ToString())),
                            uploaddocument_gid = (dr_datarow["uploaddocument_gid"].ToString()),
                            document_type = dr_datarow["document_type"].ToString(),
                            uploaded_details = dr_datarow["uploaded_by"].ToString()
                        });
                    }
                    objfilename.upload_list = get_filename;
                }
                dt_datatable.Dispose();
            }
            catch
            {

            }
            objfilename.status = true;
            return true;
        }
        public bool DaUploadCorrectedDocument(HttpRequest httpRequest, uploaddocument objfilename, string employee_gid)
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
            string uploaddocumet_gid = httpRequest.Form["uploaddocument_gid"].ToString();
            string project_flag = httpRequest.Form["project_flag"].ToString();

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "LGL/DocumentCompliance/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
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
                        lsfile_gid = lsfile_gid + FileExtension;
                        ls_readStream = httpPostedFile.InputStream;
                        ls_readStream.CopyTo(ms);
                        lspath = path;
                        //objcmnfunctions.uploadFile(lspath, lsfile_gid);
                        //lspath = "../../erp_documents" + "/" + lscompany_code + "/" + "LGL/DocumentCompliance/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension;

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.status = false;
                            objfilename.message = "File format is not supported";
                            return false;
                        }

                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "LGL/DocumentCompliance/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "LGL/DocumentCompliance/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";



                        msSQL = " update lgl_tmp_tuploadcompliancedocument set  " +
                             " document_corrected='" + 'Y' + "'," +
                                    " correctedfile_name='" + httpPostedFile.FileName + "'," +
                                    " correctedfile_path='" + lspath + "' ," +
                                    " updated_by='" + employee_gid + "'," +
                                    " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                                    " where uploaddocument_gid='" + uploaddocumet_gid + "'";

                        mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnresult != 0)
                        {
                            objfilename.status = true;
                            return true;
                        }
                        else
                        {
                            objfilename.status = false;
                            return false;
                        }
                    }
                }
                msSQL = "select uploaddocument_gid,if (correctedfile_name is null ,'---',concat(correctedfile_name,' / ',date_format(updated_date,'%d-%m-%Y %H:%i %p'))) as" +
                    " correctedfile_name ,correctedfile_path from lgl_tmp_tuploadcompliancedocument";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_filename = new List<upload_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_filename.Add(new upload_list
                        {
                            correctedfile_name = (dr_datarow["correctedfile_name"].ToString()),
                            correctedfile_path = objcmnstorage.EncryptData((dr_datarow["correctedfile_path"].ToString())),
                            uploaddocument_gid = (dr_datarow["uploaddocument_gid"].ToString())
                        });
                    }
                    objfilename.upload_list = get_filename;
                }
                dt_datatable.Dispose();
            }
            catch
            {

            }
            objfilename.status = true;
            return true;
        }
        public bool DaGetDocumentCancel(string uploaddocument_gid, resultvalue value)
        {

            msSQL = " delete from lgl_tmp_tuploadcompliancedocument where uploaddocument_gid= '" + uploaddocument_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnresult != 0)
            {
                value.status = true;
                value.message = "Document deleted successfully";
                return true;
            }
            else
            {
                value.status = false;
                value.message = "Error occured while deleting";
                return false;
            }

        }
        public bool DaGetRequestCompliance(string user_gid, string requestcompliance_gid, MdlRequestcompliance values)
        {
            msSQL = " select requestref_no,request_type,date_format(request_date,'%d-%m-%Y') as request_date,requesttype_gid,others_title," +
                " requestcompliance_gid,remarks from lgl_mst_trequestcompliance where requestcompliance_gid='" + requestcompliance_gid + "' ";

            objodbcdatareader = objdbconn.GetDataReader(msSQL);
            if (objodbcdatareader.HasRows)
            {
                values.requestref_no = objodbcdatareader["requestref_no"].ToString();
                values.request_type = objodbcdatareader["request_type"].ToString();
                values.request_date = objodbcdatareader["request_date"].ToString();
                values.requesttype_gid = objodbcdatareader["requesttype_gid"].ToString();
                values.others_title = objodbcdatareader["others_title"].ToString();
                values.requestcompliance_gid = requestcompliance_gid;
                values.remarks = objodbcdatareader["remarks"].ToString();

            }
            objodbcdatareader.Close();

            msSQL = "select uploaddocument_gid,file_name,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code,' ',date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_by,file_path,document_type " +
                    " from lgl_tmp_tuploadcompliancedocument a,hrm_mst_temployee b , adm_mst_tuser c where a.created_by=b.employee_gid " +
                  " and b.user_gid = c.user_gid and  (requestcompliance_gid='" + requestcompliance_gid + "' or requestcompliance_gid='" + user_gid + "')";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdocument = new List<upload_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getdocument.Add(new upload_list
                    {
                        uploaddocument_gid = (dr_datarow["uploaddocument_gid"].ToString()),
                        file_name = (dr_datarow["file_name"].ToString()),
                        file_path = objcmnstorage.EncryptData((dr_datarow["file_path"].ToString())),
                        document_type = dr_datarow["document_type"].ToString(),
                        uploaded_details = dr_datarow["uploaded_by"].ToString()
                    });
                }
                values.upload_list = getdocument;
            }
            dt_datatable.Dispose();
            values.status = true;
            return true;
        }
        public bool DaUpdateRequestCompliance(string employee_gid, MdlRequestcompliance values)
        {
            msSQL = "select request_type from lgl_mst_trequesttype where requesttype_gid='" + values.requesttype_gid + "'";
            string lsrequest_type = objdbconn.GetExecuteScalar(msSQL);
            if (lsrequest_type == "Others")
            {
                if ((values.others_title == null) || (values.others_title == ""))
                {
                    values.status = false;
                    values.message = "Kindly Enter Others Title";
                    return false;
                }
            }

            msSQL = "update lgl_mst_trequestcompliance set" +
                " request_type='" + lsrequest_type + "'," +
                 " requesttype_gid='" + values.requesttype_gid + "',";
            if (values.others_title == null)
            {
                msSQL += " others_title='',";
            }
            else
            {
                msSQL += " others_title='" + values.others_title.Replace("'", "") + "',";
            }
            msSQL += " remarks='" + values.remarks.Replace("'", "") + "'" +
                " where requestcompliance_gid='" + values.requestcompliance_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update lgl_tmp_tuploadcompliancedocument set requestcompliance_gid='" + values.requestcompliance_gid + "' where created_by ='" + employee_gid + "' and " +
                   " requestcompliance_gid='" + employee_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnresult != 0)
            {

                values.status = true;
                values.message = "Request Compliance updated successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while updating";
                return false;
            }
        }
        public bool DaGetRequestComplianceDelete(string requestcompliance_gid, MdlRequestcompliance values)
        {

            msSQL = " delete from lgl_mst_trequestcompliance where requestcompliance_gid='" + requestcompliance_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnresult != 0)
            {
                msSQL = "delete from lgl_tmp_tuploadcompliancedocument where requestcompliance_gid='" + requestcompliance_gid + "'";

                mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Document deleted succesfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while deleting";
                return false;
            }
        }
        //Query Conversation 
        public bool DaPostSendQuery(string employee_gid, querydetails values)
        {
            //Request Flag use is blinking new one added(query)
            msGetGid = objcmnfunctions.GetMasterGID("RCQD");
            msSQL = " insert into lgl_trn_trequestcompliance2query(" +
                    " requestcompliance2query_gid," +
                    " requestcompliance_gid," +
                    " queries," +
                    " response_new, " +
                    " requester_gid," +
                    " employee_gid ," +
                    " created_by, " +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.requestcompliance_gid + "'," +
                    "'" + values.queries + "'," +
                    "'Y'," +
                    "'" + employee_gid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnresult != 0)
            {

                msSQL = "update lgl_mst_trequestcompliance set request_flag='Y' where requestcompliance_gid='" + values.requestcompliance_gid + "'";
                mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                return true;
            }
            else
            {
                values.status = false;
                return false;
            }
        }
        //Query Conversation Summary
        public bool DaGetQueryDetails(string employee_gid, string requestcompliance_gid, querylist values)
        {

            msSQL = "select a.queries,a.created_date,a.employee_gid,concat(c.user_firstname, ' ', c.user_lastname, '/ ', c.user_code) as sender_name " +
                    " from lgl_trn_trequestcompliance2query a " +
                    " left join hrm_mst_temployee b on a.employee_gid = b.employee_gid " +
                    " left join adm_mst_tuser c on b.user_gid = c.user_gid " +
                    " where a.requestcompliance_gid = '" + requestcompliance_gid + "' order by a.created_date asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getquerieslist = new List<querydetails>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    if (dr_datarow["employee_gid"].ToString() == employee_gid)
                    {
                        lssession_user = "Y";
                    }
                    else
                    {
                        lssession_user = "N";
                    }
                    getquerieslist.Add(new querydetails
                    {
                        queries = (dr_datarow["queries"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        sender_name = (dr_datarow["sender_name"].ToString()),
                        session_user = lssession_user
                    });
                }
                values.querydetails = getquerieslist;
            }
            dt_datatable.Dispose();
            values.status = true;
            return true;
        }
        public bool DaUploadCorrectedRemarks(uploaddocument objfilename, string employee_gid)
        {

            msSQL = " update lgl_tmp_tuploadcompliancedocument set remarks='" + objfilename.uploadremarks + "'" +
                " where uploaddocument_gid='" + objfilename.uploaddocument_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);


            if (mnresult != 0)
            {
                objfilename.status = true;
                return true;
            }
            else
            {
                objfilename.status = false;
                return false;
            }

        }

        public bool DaGetAssignLawyer(taggedcase values)
        {

            msSQL = " select lawyeruser_gid,lawyeruser_code,concat(lawyeruser_name,'/',lawyeruser_code) as lawyeruser_name ,lawyerregister_gid " +
                " from lgl_mst_tlawyeruser where lawyeruser_status in('Y') and register_type='Lawyer' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_assignLawyer = new List<taggedlawyerList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    get_assignLawyer.Add(new taggedlawyerList
                    {
                        lawyeruser_gid = (dr_datarow["lawyeruser_gid"].ToString()),
                        lawyeruser_code = (dr_datarow["lawyeruser_code"].ToString()),
                        lawyeruser_name = (dr_datarow["lawyeruser_name"].ToString()),
                        lawyerregister_gid = (dr_datarow["lawyerregister_gid"].ToString())
                    });
                }
                values.taggedlawyerList = get_assignLawyer;
            }
            dt_datatable.Dispose();

            msSQL = " select lawyeruser_gid,lawyeruser_code,concat(lawyeruser_name,'/',lawyeruser_code) as lawyeruser_name ,lawyerregister_gid " +
               " from lgl_mst_tlawyeruser where lawyeruser_status in('Y') and register_type='Law Firm' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var gettaggedlawfirmList = new List<taggedlawfirmList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    gettaggedlawfirmList.Add(new taggedlawfirmList
                    {
                        lawyeruser_gid = (dr_datarow["lawyeruser_gid"].ToString()),
                        lawyeruser_code = (dr_datarow["lawyeruser_code"].ToString()),
                        lawyeruser_name = (dr_datarow["lawyeruser_name"].ToString()),
                        lawyerregister_gid = (dr_datarow["lawyerregister_gid"].ToString())
                    });
                }
                values.taggedlawfirmList = gettaggedlawfirmList;
            }
            dt_datatable.Dispose();
            values.status = true;
            return true;
        }

        public bool DaGetSeekDocumentClear(string employee_gid)
        {

            msSQL = "delete from lgl_tmp_tseeklawyerdocument where created_by='" + employee_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnresult != 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool DaGetAssignCompliance(assignlawyer values, string employee_gid)
        {

            msSQL = " update lgl_mst_trequestcompliance set tag_flag='Y'," +
                    " assigned_by ='" + employee_gid + "'," +
                    " assigned_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    " where requestcompliance_gid='" + values.requestcompliance_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);


            if ((values.taggedlawyerList != null))
            {

                for (var i = 0; i < values.taggedlawyerList.Count; i++)
                {

                    msGetGid = objcmnfunctions.GetMasterGID("RC2T");
                    msSQL = " insert into lgl_trn_trequestcompliance2lawyerdtl(" +
                            " requestcompliance2lawyerdtl_gid," +
                            " requestcompliance_gid," +
                            " lawyerregister_gid," +
                            " lawyeruser_name," +
                            " seek_remarks," +
                            " tagged_by," +
                            " tagged_date)" +
                            " values(" +
                            "'" + msGetGid + "'," +
                            "'" + values.requestcompliance_gid + "'," +
                            "'" + values.taggedlawyerList[i].lawyerregister_gid + "'," +
                            "'" + values.taggedlawyerList[i].lawyeruser_name + "'," +
                            "'" + values.seeklawyerremarks + "'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    // Lawyer Mail
                    try
                    {

                        msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                        objodbcdatareader = objdbconn.GetDataReader(msSQL);
                        if (objodbcdatareader.HasRows == true)
                        {
                            objodbcdatareader.Read();
                            frommail_id = objodbcdatareader["company_mail"].ToString();
                            ls_server = objodbcdatareader["pop_server"].ToString();
                            ls_port = Convert.ToInt32(objodbcdatareader["pop_port"]);
                            ls_username = objodbcdatareader["pop_username"].ToString();
                            ls_password = objodbcdatareader["pop_password"].ToString();

                        }
                        objodbcdatareader.Close();



                        msSQL = "select email_address from lgl_mst_tlawyeruser a " +
                                " where lawyerregister_gid='" + values.taggedlawyerList[i].lawyerregister_gid + "'";
                        tomail_id = objdbconn.GetExecuteScalar(msSQL);


                        msSQL = " select a.lawyeruser_code, a.lawyeruser_name, b.lawyeruser_password from lgl_mst_tlawyeruser a" +
                               " left join lgl_mst_tregisterlawyer b on a.lawyerregister_gid = b.lawyerregister_gid " +
                               " where a.lawyerregister_gid='" + values.taggedlawyerList[i].lawyerregister_gid + "'";
                        objodbcdatareader = objdbconn.GetDataReader(msSQL);
                        if (objodbcdatareader.HasRows == true)
                        {
                            lawyeruser_code = objodbcdatareader["lawyeruser_code"].ToString();
                            lawyeruser_name = objodbcdatareader["lawyeruser_name"].ToString();
                            lawyeruser_password = objodbcdatareader["lawyeruser_password"].ToString();
                        }
                        objodbcdatareader.Close();

                        sub = " Credientials ";


                        lscontent = HttpUtility.HtmlEncode(values.content);

                        body = "Dear Sir/Madam,  <br />";
                        body = body + "<br />";
                        body = body + "Greetings,  <br />";
                        body = body + "<br />";
                        body = body + " The Credientials are as Follows: ,<br />";
                        body = body + "<br />";
                        body = body + "<b>User Code :</b> " + HttpUtility.HtmlEncode(lawyeruser_code) + "<br />";
                        body = body + "<br />";
                        body = body + "<b>User Name :</b> " + HttpUtility.HtmlEncode(lawyeruser_name) + "<br />";
                        body = body + "<br />";
                        body = body + "<b>Password :</b> " + HttpUtility.HtmlEncode(lawyeruser_password) + "<br />";
                        body = body + "<br />";
                        body = body + "Kindly Login <b> https://" + ConfigurationManager.AppSettings["livedomain_url"].ToString() + "/lp </b> and Do the Needful <br /> ";
                        body = body + "<br />";

                        body = body + "<b>Thanks & Regards, </b> ";
                        body = body + "<br />";
                        body = body + "<b> Support Team</b> ";
                        body = body + "<br />";

                        //cc_mailid = "";
                        MailMessage message = new MailMessage();
                        SmtpClient smtp = new SmtpClient();
                        message.From = new MailAddress(ls_username);
                        message.To.Add(new MailAddress(tomail_id));
                        //message.CC.Add(cc);


                        //if (cc != null & cc != string.Empty & cc != "")
                        //{
                        //    lsCCReceipients = cc.Split(',');
                        //    if (cc.Length == 0)
                        //    {
                        //        message.CC.Add(new MailAddress(cc));
                        //    }
                        //    else
                        //    {
                        //        foreach (string CCEmail in lsCCReceipients)
                        //        {
                        //            message.CC.Add(new MailAddress(CCEmail)); //Adding Multiple CC email Id
                        //        }
                        //    }
                        //}

                        message.Subject = sub;
                        message.IsBodyHtml = true; //to make message body as html  
                        message.Body = body;
                        smtp.Port = ls_port;
                        smtp.Host = ls_server; //for gmail host  
                        smtp.EnableSsl = true;
                        smtp.UseDefaultCredentials = false;
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                        smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.Send(message);

                        values.status = true;

                    }
                    catch (Exception ex)
                    {
                        values.message = ex.ToString();
                        values.status = false;
                    }

                    msSQL = "select * from lgl_tmp_tseeklawyerdocument where created_by='" + employee_gid + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            msGetDocumentGid = objcmnfunctions.GetMasterGID("SLDO");
                            msSQL = " Insert into lgl_trn_tseeklawyerdocument( " +
                                   " seekdocument_gid," +
                                   " requestcompliance2lawyerdtl_gid," +
                                   " request_compliancegid," +
                                   " document_name," +
                                   " document_path," +
                                   " created_by," +
                                   " created_date)" +
                                   " values(" +
                                   "'" + msGetDocumentGid + "', " +
                                   "'" + msGetGid + "'," +
                                   "'" + values.requestcompliance_gid + "'," +
                                   "'" + dt["document_name"].ToString() + "'," +
                                   "'" + dt["document_path"].ToString() + "'," +
                                   "'" + employee_gid + "'," +
                                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        }
                    }
                    dt_datatable.Dispose();

                }

            }
            if ((values.taggedlawfirmList != null))
            {
                for (var i = 0; i < values.taggedlawfirmList.Count; i++)
                {


                    msGetGid = objcmnfunctions.GetMasterGID("RC2T");
                    msSQL = " insert into lgl_trn_trequestcompliance2lawyerdtl(" +
                            " requestcompliance2lawyerdtl_gid," +
                            " requestcompliance_gid," +
                            " lawyerregister_gid," +
                            " lawyeruser_name," +
                            " seek_remarks," +
                            " tagged_by," +
                            " tagged_date)" +
                            " values(" +
                            "'" + msGetGid + "'," +
                            "'" + values.requestcompliance_gid + "'," +
                            "'" + values.taggedlawfirmList[i].lawyerregister_gid + "'," +
                            "'" + values.taggedlawfirmList[i].lawyeruser_name + "'," +
                             "'" + values.seeklawyerremarks + "'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);


                    // LawFirm Mail
                    try
                    {

                        msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                        objodbcdatareader = objdbconn.GetDataReader(msSQL);
                        if (objodbcdatareader.HasRows == true)
                        {
                            objodbcdatareader.Read();
                            frommail_id = objodbcdatareader["company_mail"].ToString();
                            ls_server = objodbcdatareader["pop_server"].ToString();
                            ls_port = Convert.ToInt32(objodbcdatareader["pop_port"]);
                            ls_username = objodbcdatareader["pop_username"].ToString();
                            ls_password = objodbcdatareader["pop_password"].ToString();

                        }
                        objodbcdatareader.Close();

                        //msSQL = "select group_concat(distinct employee_emailid) as ccmail_id from osd_trn_ttaggedmemberlist a " +
                        //    " left join hrm_mst_temployee b on a.tagmember_gid = b.employee_gid where servicerequest_gid='" + msGetGid + "'";
                        //cc = objdbconn.GetExecuteScalar(msSQL);


                        msSQL = "select email_address from lgl_mst_tlawyeruser " +
                                " where lawyerregister_gid='" + values.taggedlawfirmList[i].lawyerregister_gid + "'";
                        tomail_id = objdbconn.GetExecuteScalar(msSQL);


                        msSQL = " select a.lawyeruser_code, a.lawyeruser_name, b.lawfirmuser_password from lgl_mst_tlawyeruser a" +
                                " left join lgl_mst_tlawfirm b on a.lawyerregister_gid = b.lawfirm_gid " +
                               " where a.lawyerregister_gid='" + values.taggedlawfirmList[i].lawyerregister_gid + "'";
                        objodbcdatareader = objdbconn.GetDataReader(msSQL);
                        if (objodbcdatareader.HasRows == true)
                        {
                            lawyeruser_code = objodbcdatareader["lawyeruser_code"].ToString();
                            lawyeruser_name = objodbcdatareader["lawyeruser_name"].ToString();
                            lawyerfirmuser_password = objodbcdatareader["lawfirmuser_password"].ToString();
                        }
                        objodbcdatareader.Close();

                        sub = " Credientials ";


                        lscontent = HttpUtility.HtmlEncode(values.content);

                        body = "Dear Sir/Madam,  <br />";
                        body = body + "<br />";
                        body = body + "Greetings,  <br />";
                        body = body + "<br />";
                        body = body + " The Credientials are as Follows: <br />";
                        body = body + "<br />";
                        body = body + "<b>User Code :</b> " + HttpUtility.HtmlEncode(lawyeruser_code) + "<br />";
                        body = body + "<br />";
                        body = body + "<b>User Name :</b> " + HttpUtility.HtmlEncode(lawyeruser_name) + "<br />";
                        body = body + "<br />";
                        body = body + "<b>Password :</b> " + HttpUtility.HtmlEncode(lawyerfirmuser_password) + "<br />";
                        body = body + "<br />";

                        body = body + "Kindly Login <b> https://" + ConfigurationManager.AppSettings["livedomain_url"].ToString() + "/lp </b> and Do the Needful <br />";
                        body = body + "<br />";

                        body = body + "<b>Thanks & Regards, </b> ";
                        body = body + "<br />";
                        body = body + "<b> Support Team</b> ";
                        body = body + "<br />";

                        //cc_mailid = "";
                        MailMessage message = new MailMessage();
                        SmtpClient smtp = new SmtpClient();
                        message.From = new MailAddress(ls_username);
                        message.To.Add(new MailAddress(tomail_id));
                        //message.CC.Add(cc);


                        //if (cc != null & cc != string.Empty & cc != "")
                        //{
                        //    lsCCReceipients = cc.Split(',');
                        //    if (cc.Length == 0)
                        //    {
                        //        message.CC.Add(new MailAddress(cc));
                        //    }
                        //    else
                        //    {
                        //        foreach (string CCEmail in lsCCReceipients)
                        //        {
                        //            message.CC.Add(new MailAddress(CCEmail)); //Adding Multiple CC email Id
                        //        }
                        //    }
                        //}

                        message.Subject = sub;
                        message.IsBodyHtml = true; //to make message body as html  
                        message.Body = body;
                        smtp.Port = ls_port;
                        smtp.Host = ls_server; //for gmail host  
                        smtp.EnableSsl = true;
                        smtp.UseDefaultCredentials = false;
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                        smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.Send(message);

                        values.status = true;

                    }
                    catch (Exception ex)
                    {
                        values.message = ex.ToString();
                        values.status = false;
                    }

                    msSQL = "select * from lgl_tmp_tseeklawyerdocument where created_by='" + employee_gid + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            msGetDocumentGid = objcmnfunctions.GetMasterGID("SLDO");
                            msSQL = " Insert into lgl_trn_tseeklawyerdocument( " +
                                   " seekdocument_gid," +
                                   " requestcompliance2lawyerdtl_gid," +
                                   " request_compliancegid," +
                                   " document_name," +
                                   " document_path," +
                                   " created_by," +
                                   " created_date)" +
                                   " values(" +
                                   "'" + msGetDocumentGid + "', " +
                                   "'" + msGetGid + "'," +
                                   "'" + values.requestcompliance_gid + "'," +
                                   "'" + dt["document_name"].ToString() + "'," +
                                   "'" + dt["document_path"].ToString() + "'," +
                                   "'" + employee_gid + "'," +
                                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        }
                    }
                    dt_datatable.Dispose();

                }
            }

            if (mnresult == 1)
            {
                msSQL = "delete from lgl_tmp_tseeklawyerdocument where created_by ='" + employee_gid + "'";
                mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }



            if (mnresult != 0)
            {
                values.status = true;
                return true;
            }
            else
            {
                values.status = false;
                return false;
            }

        }
        public bool DaGetTempdelete(string user_gid, resultvalue value)
        {

            msSQL = " delete from lgl_tmp_tuploadcompliancedocument where (requestcompliance_gid like 'SER%' or requestcompliance_gid='E1')";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnresult != 0)
            {
                value.status = true;
                value.message = "Document deleted successfully";
                return true;
            }

            else
            {
                value.status = false;
                value.message = "Error Occured while deleteing";
                return false;
            }
        }

        public bool DaGetcorrecteddoc_delete(string employee_gid, string uploaddocument_gid, resultvalue value)
        {

            msSQL = " update lgl_tmp_tuploadcompliancedocument set " +
                 " document_corrected='" + 'N' + "'," +
                                    " correctedfile_name=''," +
                                    " correctedfile_path='' ," +
                                    " updated_by='" + employee_gid + "'," +
                                    " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                " where uploaddocument_gid= '" + uploaddocument_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnresult != 0)
            {
                value.status = true;
                value.message = "Document deleted successfully";
                return true;
            }
            else
            {
                value.status = false;
                value.message = "Error occured while deleting";
                return false;
            }

        }

        public void DaGetmandatory_check(string employee_gid, resultvalue value)
        {

            msSQL = "select * from  lgl_tmp_tuploadcompliancedocument where requestcompliance_gid='" + employee_gid + "'";
            objodbcdatareader = objdbconn.GetDataReader(msSQL);
            if (objodbcdatareader.HasRows == false)
            {

                value.message = "Kindly upload Document";
                value.status = false;
                objodbcdatareader.Close();
            }
            else
            {
                value.status = true;
                objodbcdatareader.Close();

            }



        }
        public void DaGeteditmandatory_check(string employee_gid, resultvalue value, string requestcompliance_gid)
        {

            msSQL = "select * from  lgl_tmp_tuploadcompliancedocument where requestcompliance_gid='" + employee_gid + "' or requestcompliance_gid='" + requestcompliance_gid + "' ";
            objodbcdatareader = objdbconn.GetDataReader(msSQL);
            if (objodbcdatareader.HasRows == false)
            {

                value.message = "Kindly upload  document";
                value.status = false;
                objodbcdatareader.Close();
            }
            else
            {
                value.status = true;
                objodbcdatareader.Close();

            }

        }
        public bool Dapostrequesttype(string employee_gid, mdlrequesttype values)
        {
            if ((values.request_type == "Others") || (values.request_type == "Other") || (values.request_type == "other") || (values.request_type == "others"))
            {
                values.status = false;
                values.message = "Can't able to add this Request Type";
                return false;
            }
            msSQL = "select request_type from lgl_mst_trequesttype where request_type='" + values.request_type + "' or request_code='" + values.request_code + "'";
            objodbcdatareader = objdbconn.GetDataReader(msSQL);
            if (objodbcdatareader.HasRows == false)
            {
                msGetGid = objcmnfunctions.GetMasterGID("LRQT");

                msSQL = "insert into lgl_mst_trequesttype(" +
                          " requesttype_gid ," +
                          " request_code ," +
                          " request_type ," +
                          " created_by," +
                          " created_date)" +
                          " values(" +
                          "'" + msGetGid + "'," +
                          "'" + values.request_code.Replace("'", "") + "'," +
                          "'" + values.request_type.Replace("'", "") + "'," +
                          "'" + employee_gid + "'," +
                          "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
                objodbcdatareader.Close();

                if (mnresult != 0)
                {


                    values.status = true;
                    values.message = "Request Type Added Sucessfully";
                    return true;
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occured while adding";
                    return false;
                }


            }
            else
            {
                objodbcdatareader.Close();
                values.status = false;
                values.message = "Request Type / Code Already Exists";
                return false;
            }

        }
        public bool Dagetrequesttype(mdlrequesttype values, string employee_gid)
        {

            msSQL = "select requesttype_gid,request_code,request_type,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                " concat(c.user_firstname, ' ', c.user_lastname,' / ',c.user_code) as created_by from lgl_mst_trequesttype a " +
                " left join hrm_mst_temployee b on a.created_by=b.employee_gid" +
                " left join adm_mst_tuser c on c.user_gid=b.user_gid order by requesttype_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getrequesttype_list = new List<requesttype_list>();
            if (dt_datatable.Rows.Count != 0)
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {


                    getrequesttype_list.Add(new requesttype_list
                    {
                        requesttype_gid = (dr_datarow["requesttype_gid"].ToString()),
                        request_code = (dr_datarow["request_code"].ToString()),
                        request_type = (dr_datarow["request_type"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                    });
                }
            values.requesttype_list = getrequesttype_list;

            dt_datatable.Dispose();
            values.status = true;
            return true;
        }
        public bool Daupdaterequesttype(string employee_gid, mdlrequesttype values)
        {
            if ((values.request_type == "Others") || (values.request_type == "Other") || (values.request_type == "other") || (values.request_type == "others"))
            {
                values.status = false;
                values.message = "Can't able to add this Request Type";
                return false;
            }

            msSQL = "update lgl_mst_trequesttype set" +
                      " request_code='" + values.request_code + "'," +
                      " request_type='" + values.request_type + "'," +
                      " updated_by='" + employee_gid + "'," +
                      " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                      " where requesttype_gid='" + values.requesttype_gid + "'";

            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnresult != 0)
            {


                values.status = true;
                values.message = "Request Type updated Sucessfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while updating ";
                return false;
            }


        }
        public bool Daeditrequesttype(string employee_gid, string requesttype_gid, mdlrequesttype values)
        {

            msSQL = "select requesttype_gid,request_code,request_type from lgl_mst_trequesttype where requesttype_gid='" + requesttype_gid + "'";
            objodbcdatareader = objdbconn.GetDataReader(msSQL);
            if (objodbcdatareader.HasRows == true)
            {
                values.requesttype_gid = objodbcdatareader["requesttype_gid"].ToString();
                values.request_code = objodbcdatareader["request_code"].ToString();
                values.request_type = objodbcdatareader["request_type"].ToString();
            }
            objodbcdatareader.Close();
            values.status = true;
            return true;
        }
        public bool Dadeleterequesttype(string requesttype_gid, mdlrequesttype values)
        {

            msSQL = "delete from lgl_mst_trequesttype where requesttype_gid='" + requesttype_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnresult != 0)
            {
                values.status = true;
                values.message = "Request Type deleted Sucessfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while deleting ";
                return false;
            }
        }
        public bool Daupdatestatus(string employee_gid, MdlRequestcompliance values)
        {

            msSQL = "update lgl_mst_trequestcompliance set" +
                     " status='" + values.request_status + "',";

            if (values.request_status == "Rejected")
            {

                if (values.rejected_remarks == null || values.rejected_remarks == "")
                {
                    msSQL += "rejected_remarks=null,";
                }
                else
                {
                    msSQL += "rejected_remarks='" + values.rejected_remarks.Replace("'", "") + "'," +
                             "rejected_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',";
                }

            }
            else if (values.request_status == "Completed")
            {
                if (values.completed_remarks == null || values.completed_remarks == "")
                {
                    msSQL += "completed_remarks=null,";
                }
                else
                {
                    msSQL += "completed_remarks='" + values.completed_remarks.Replace("'", "") + "'," +
                             "completed_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',";
                }
            }
            else
            {
                msSQL += "rejected_remarks=null,";
            }
            msSQL += " updated_by='" + employee_gid + "'," +
                     " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                     " where requestcompliance_gid='" + values.requestcompliance_gid + "'";

            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnresult != 0)
            {


                values.status = true;
                values.message = "Status updated Successfully";
                if (values.request_status == "Rejected" || values.request_status == "Completed")
                {
                    try
                    {

                        msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                        objodbcdatareader = objdbconn.GetDataReader(msSQL);
                        if (objodbcdatareader.HasRows == true)
                        {
                            objodbcdatareader.Read();
                            ls_server = objodbcdatareader["pop_server"].ToString();
                            ls_port = Convert.ToInt32(objodbcdatareader["pop_port"]);
                            ls_username = objodbcdatareader["pop_username"].ToString();
                            ls_password = objodbcdatareader["pop_password"].ToString();

                        }
                        objodbcdatareader.Close();


                        msSQL = "select b.employee_emailid from lgl_mst_trequestcompliance a left join hrm_mst_temployee b on a.created_by = b.employee_gid where a.requestcompliance_gid = '" + values.requestcompliance_gid + "'";
                        tomail_id = objdbconn.GetExecuteScalar(msSQL);



                        msSQL = " select requestref_no,request_type," +
                               " concat(c.user_firstname, ' ', c.user_lastname, '/', c.user_code) as raised_by, " +
                               " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as raised_date " +
                               " from lgl_mst_trequestcompliance a " +
                               " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                               " left join adm_mst_tuser c on b.user_gid = c.user_gid" +
                               " where requestcompliance_gid ='" + values.requestcompliance_gid + "'";
                        objodbcdatareader = objdbconn.GetDataReader(msSQL);
                        if (objodbcdatareader.HasRows == true)
                        {
                            lsrequestref_no = objodbcdatareader["requestref_no"].ToString();
                            lsrequest_type = objodbcdatareader["request_type"].ToString();
                            lsraised_by = objodbcdatareader["raised_by"].ToString();
                            lsraised_date = objodbcdatareader["raised_date"].ToString();
                        }


                        objodbcdatareader.Close();

                        sub = " Legal Compliance ";


                        body = "Dear Sir/Madam,  <br />";
                        body = body + "<br />";
                        body = body + "Greetings,  <br />";
                        body = body + "<br />";
                        body = body + "The Legal Compliance request raised by " + "<b>" + HttpUtility.HtmlEncode(lsraised_by) + "</b>" + " has been " + HttpUtility.HtmlEncode(values.request_status) + ". Kindly login to the Samunnati Website for more details.<br />";
                        body = body + "<br />";
                        body = body + "<b>Ref No :</b> " + HttpUtility.HtmlEncode(lsrequestref_no) + "<br />";
                        body = body + "<br />";
                        body = body + "<b>Raised Date :</b> " + lsraised_date + "<br />";
                        body = body + "<br />";
                        body = body + "<b>Category :</b> " + HttpUtility.HtmlEncode(lsrequest_type) + "<br />";
                        body = body + "<br />";

                        body = body + "<b>Yours Sincerely,</b> ";
                        body = body + "<br />";
                        body = body + "Samunnati Financial Intermediation & Services Pvt Ltd.<br /> ";
                        body = body + "<br />";
                        body = body + "<b>Please Note:</b> " + "This is an auto generated e-mail that cannot receive replies. " + "<br />";
                        body = body + "<br />";


                        MailMessage message = new MailMessage();
                        SmtpClient smtp = new SmtpClient();

                        message.From = new MailAddress(ls_username);
                        message.To.Add(new MailAddress(tomail_id));


                        message.Subject = sub;
                        message.IsBodyHtml = true; //to make message body as html  
                        message.Body = body;
                        smtp.Port = ls_port;
                        smtp.Host = ls_server; //for gmail host  
                        smtp.EnableSsl = true;
                        smtp.UseDefaultCredentials = false;
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                        smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.Send(message);

                        values.status = true;

                        if (values.status == true)
                        {
                            msSQL = "Insert into lgl_trn_requestcompliancesentmail( " +
                            " requestcompliance_gid," +
                            " from_mail," +
                            " to_mail," +
                            " cc_mail," +
                            " mail_status," +
                            " mail_senddate, " +
                            " created_by," +
                            " created_date)" +
                            " values(" +
                            "'" + values.requestcompliance_gid + "'," +
                            "'" + ls_username + "'," +
                            "'" + tomail_id + "'," +
                            "'" + ccmail_id + "'," +
                            "'Request Compliance " + values.request_status + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }

                    }
                    catch (Exception ex)
                    {
                        values.message = ex.ToString();
                        values.status = false;
                    }

                }
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while updating ";
                return false;
            }

        }
        // Manage Comppliance-corrected document upload
        public bool DaUploadComplianceCorrected_doc(HttpRequest httpRequest, Managecomplianuploaddoc objfilename, string employee_gid, string user_gid)
        {
            Managecomplianuploaddoc_list objdocumentmodel = new Managecomplianuploaddoc_list();
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
            string requestcompliance_gid = httpRequest.Form["requestcompliance_gid"].ToString();
            string project_flag = httpRequest.Form["project_flag"].ToString();


            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";

            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "LGL/ManageCompliance/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
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
                        lspath = path;
                        //objcmnfunctions.uploadFile(lspath, lsfile_gid);
                        //lspath = "../../erp_documents" + "/" + lscompany_code + "/" + "LGL/ManageCompliance/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension;

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.status = false;
                            objfilename.message = "File format is not supported";
                            return false;
                        }

                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "LGL/ManageCompliance/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "LGL/ManageCompliance/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";


                        if (document_type == "undefined")
                        {
                            lsdocument_type = "";
                        }
                        else
                        {
                            lsdocument_type = document_type;
                        }
                        msGetGid = objcmnfunctions.GetMasterGID("UECD");
                        msSQL = " insert into lgl_trn_tmanagecompliancecorrecteddoc( " +
                                    " document_gid," +
                                    " requestcompliance_gid ," +
                                    " document_name," +
                                    " document_path," +
                                    " document_type ," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + httpPostedFile.FileName + "'," +
                                  "'" + lspath + msdocument_gid + FileExtension + "'," +
                                    "'" + lsdocument_type + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    }
                }
                msSQL = " select document_gid,document_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,document_path,document_type, " +
                     " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as uploaded_by" +
                     " from lgl_trn_tmanagecompliancecorrecteddoc a, hrm_mst_temployee b, adm_mst_tuser c where a.created_by=b.employee_gid and " +
                     " b.user_gid=c.user_gid and a.requestcompliance_gid='" + employee_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_filename = new List<Managecomplianuploaddoc_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_filename.Add(new Managecomplianuploaddoc_list
                        {
                            document_name = (dr_datarow["document_name"].ToString()),
                            document_gid = (dr_datarow["document_gid"].ToString()),
                            document_type = dr_datarow["document_type"].ToString(),
                            uploaded_by = dr_datarow["uploaded_by"].ToString(),
                            updated_date = dr_datarow["uploaded_date"].ToString()
                        });
                    }
                    objfilename.Managecomplianuploaddoc_list = get_filename;
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

        public void DasubmitComplianceCorrected_doc(string employee_gid, MdlRequestcompliance values)
        {
            msSQL = "select * from lgl_trn_tmanagecompliancecorrecteddoc where  requestcompliance_gid='" + employee_gid + "'";
            objodbcdatareader = objdbconn.GetDataReader(msSQL);
            if (objodbcdatareader.HasRows == true)
            {
                msSQL = "update lgl_trn_tmanagecompliancecorrecteddoc set requestcompliance_gid='" + values.requestcompliance_gid + "' where requestcompliance_gid='" + employee_gid + "'";
                mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnresult == 1)
                {
                    values.status = true;
                    values.message = "Document upload Successfully";

                }
                else
                {
                    values.status = false;
                    values.message = "Error Occured while uploading document";

                }
                objodbcdatareader.Close();
            }
            else
            {
                values.status = false;
                values.message = "Kindly upload atleast one document";
                objodbcdatareader.Close();
            }

        }

        public void Dadeletecorrecteddo_upload(string document_gid, MdlRequestcompliance values)
        {
            msSQL = "delete from lgl_trn_tmanagecompliancecorrecteddoc where document_gid='" + document_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnresult == 1)
            {
                values.status = true;
                values.message = "Document deleted Successfully";

            }
            else
            {
                values.status = false;
                values.message = "Error Occured while deleting document";

            }
        }

        public void Dagetcorrecteddocument(string requestcompliance_gid, string employee_gid, Managecomplianuploaddoc values)
        {
            msSQL = " select document_gid,concat(a.document_name) as document_name,document_path,document_type, " +
                    " concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as uploaded_by" +
                    " from lgl_trn_tmanagecompliancecorrecteddoc a,hrm_mst_temployee c, adm_mst_tuser b where a.created_by=c.employee_gid" +
                    " and b.user_gid=c.user_gid and  a.requestcompliance_gid='" + requestcompliance_gid + "'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_filename = new List<Managecomplianuploaddoc_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    get_filename.Add(new Managecomplianuploaddoc_list
                    {
                        document_name = (dr_datarow["document_name"].ToString()),
                        document_path = objcmnstorage.EncryptData((dr_datarow["document_path"].ToString())),
                        document_gid = (dr_datarow["document_gid"].ToString()),
                        document_type = dr_datarow["document_type"].ToString(),
                        uploaded_by = dr_datarow["uploaded_by"].ToString(),
                        // updated_date = dr_datarow["uploaded_date"].ToString()
                    });
                }
                values.Managecomplianuploaddoc_list = get_filename;
            }
            dt_datatable.Dispose();
            msSQL = " select document_gid,document_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,document_path,document_type, " +
                    " concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as uploaded_by" +
                    " from lgl_trn_tmanagecompliancecorrecteddoc a, adm_mst_tuser b where a.created_by=b.user_gid and a.requestcompliance_gid='" + employee_gid + "'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getuploaddoc_list = new List<uploaddoc_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getuploaddoc_list.Add(new uploaddoc_list
                    {
                        document_name = (dr_datarow["document_name"].ToString()),
                        document_gid = (dr_datarow["document_gid"].ToString()),
                        document_type = dr_datarow["document_type"].ToString(),
                        uploaded_by = dr_datarow["uploaded_by"].ToString(),
                        updated_date = dr_datarow["uploaded_date"].ToString()
                    });
                }
                values.uploaddoc_list = getuploaddoc_list;
            }
            dt_datatable.Dispose();
        }

        public bool Dagetrequesttype2compliance(mdlrequesttype values, string employee_gid)
        {

            msSQL = "select requesttype_gid,request_code,request_type from lgl_mst_trequesttype order by requesttype_gid asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getrequesttype_list = new List<requesttype_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {


                    getrequesttype_list.Add(new requesttype_list
                    {
                        requesttype_gid = (dr_datarow["requesttype_gid"].ToString()),
                        request_code = (dr_datarow["request_code"].ToString()),
                        request_type = (dr_datarow["request_type"].ToString()),

                    });

                }
                getrequesttype_list.Add(new requesttype_list
                {
                    request_type = "Others",
                    requesttype_gid = "Others",
                });
                values.requesttype_list = getrequesttype_list;

            }
            dt_datatable.Dispose();
            values.status = true;
            return true;
        }

        public bool DaGettaggedlist(MdlTaggedInfo values, string requestcompliance_gid)
        {
            msSQL = "select a.lawyeruser_name,b.email_address,b.lawyeruser_code,date_format(a.tagged_date,'%d-%m-%Y') as tagged_date," +
                    " concat(d.user_firstname,' ',d.user_lastname,'/',d.user_code) as tagged_by,a.seek_remarks,a.requestcompliance2lawyerdtl_gid," +
                    " a.request_status from lgl_trn_trequestcompliance2lawyerdtl a " +
                    " left join hrm_mst_temployee c on c.employee_gid=a.tagged_by" +
                    " left join adm_mst_tuser d on c.user_gid=d.user_gid" +
                    " left join lgl_mst_tlawyeruser b on a. lawyerregister_gid=b.lawyerregister_gid where requestcompliance_gid ='" + requestcompliance_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var gettaggedinfo_list = new List<taggedinfo_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    gettaggedinfo_list.Add(new taggedinfo_list
                    {
                        lawyeruser_name = (dr_datarow["lawyeruser_name"].ToString()),
                        email_address = (dr_datarow["email_address"].ToString()),
                        lawyeruser_code = (dr_datarow["lawyeruser_code"].ToString()),
                        tagged_by = (dr_datarow["tagged_by"].ToString()),
                        tagged_date = (dr_datarow["tagged_date"].ToString()),
                        seek_remarks = (dr_datarow["seek_remarks"].ToString()),
                        requestcompliance2lawyerdtl_gid = (dr_datarow["requestcompliance2lawyerdtl_gid"].ToString()),
                        request_status = (dr_datarow["request_status"].ToString()),
                    });


                    msSQL = " select a.document_name,a.document_path,a.requestcompliance2lawyerdtl_gid from lgl_trn_tseeklawyerdocument a" +
                            " where a.requestcompliance2lawyerdtl_gid='" + dr_datarow["requestcompliance2lawyerdtl_gid"].ToString() + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getlist = new List<taggeddoc_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow drdatarow in dt_datatable.Rows)
                        {
                            getlist.Add(new taggeddoc_list
                            {
                                requestcompliance2lawyerdtl_gid = (drdatarow["requestcompliance2lawyerdtl_gid"].ToString()),
                                document_name = (drdatarow["document_name"].ToString()),
                                document_path = objcmnstorage.EncryptData(drdatarow["document_path"].ToString()),
                            });
                        }
                        values.taggeddoc_list = getlist;
                    }
                    dt_datatable.Dispose();
                    msSQL = " select a.file_name,a.file_path,a.requestcompliance2lawyerdtl_gid from lgl_trn_tlawyeruploaddocument a" +
                            " where a.requestcompliance2lawyerdtl_gid='" + dr_datarow["requestcompliance2lawyerdtl_gid"].ToString() + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getlistlawyer = new List<taggedlawyerdoc_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dr_listlawyer in dt_datatable.Rows)
                        {
                            getlistlawyer.Add(new taggedlawyerdoc_list
                            {
                                requestcompliance2lawyerdtl_gid = (dr_listlawyer["requestcompliance2lawyerdtl_gid"].ToString()),
                                document_name = (dr_listlawyer["file_name"].ToString()),
                                document_path = objcmnstorage.EncryptData(dr_listlawyer["file_path"].ToString()),
                            });
                        }
                        values.taggedlawyerdoc_list = getlistlawyer;
                    }
                    dt_datatable.Dispose();
                }
                values.taggedinfo_list = gettaggedinfo_list;


                //msSQL = "select requesttype_gid,request_code,request_type from lgl_mst_trequesttype order by requesttype_gid asc";
                //dt_datatable = objdbconn.GetDataTable(msSQL);
                //var getrequesttype_list = new List<requesttype_list>();
                //if (dt_datatable.Rows.Count != 0)
                //    foreach (DataRow dr_datarow in dt_datatable.Rows)
                //    {


                //        getrequesttype_list.Add(new requesttype_list
                //        {
                //            requesttype_gid = (dr_datarow["requesttype_gid"].ToString()),
                //            request_code = (dr_datarow["request_code"].ToString()),
                //            request_type = (dr_datarow["request_type"].ToString()),

                //        });
                //    }
                //values.requesttype_list = getrequesttype_list;


                //dt_datatable.Dispose();
            }
            dt_datatable.Dispose();
            values.status = true;
            return true;

        }
        public void DaViewuploaddoc_lawyer(string requestcompliance2lawyerdtl_gid, MdlTaggedInfo values)
        {
            msSQL = "select a.document_name,a.document_path,a.requestcompliance2lawyerdtl_gid from lgl_trn_tseeklawyerdocument a" +
            " where a.requestcompliance2lawyerdtl_gid='" + requestcompliance2lawyerdtl_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getlist = new List<taggeddoc_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getlist.Add(new taggeddoc_list
                    {
                        requestcompliance2lawyerdtl_gid = (dr_datarow["requestcompliance2lawyerdtl_gid"].ToString()),
                        file_name = (dr_datarow["document_name"].ToString()),
                        file_path = objcmnstorage.EncryptData((dr_datarow["document_path"].ToString())),
                    });
                }
                values.taggeddoc_list = getlist;

            }
            dt_datatable.Dispose();
        }
        public void DaGetuploadbyLawyer(string requestcompliance2lawyerdtl_gid, MdlTaggedInfo values)
        {
            msSQL = "select a.file_name,a.file_path,a.requestcompliance2lawyerdtl_gid from lgl_trn_tlawyeruploaddocument a" +
            " where a.requestcompliance2lawyerdtl_gid='" + requestcompliance2lawyerdtl_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getlist = new List<taggeddoc_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getlist.Add(new taggeddoc_list
                    {
                        requestcompliance2lawyerdtl_gid = (dr_datarow["requestcompliance2lawyerdtl_gid"].ToString()),
                        document_name = (dr_datarow["file_name"].ToString()),
                        document_path = objcmnstorage.EncryptData((dr_datarow["file_path"].ToString())),
                    });
                }
                values.taggeddoc_list = getlist;

            }
            dt_datatable.Dispose();
        }

        // Lawyer Conversation
        public void DaGetLawyerSummary(string requestcompliance_gid, MdlLawyerSummaryList values)
        {


            msSQL = " SELECT requestcompliance2lawyerdtl_gid, lawyerregister_gid, lawyeruser_name," +
                    " UPPER((SUBSTR(lawyeruser_name, 1, 1))) as name_initial," +
                    " (select concat(SUBSTR(x.msgconversation,1,128),'...')" +
                    " from lgl_trn_tcompliance2lawyerconversation x" +
                    " where x.requestcompliance_gid = requestcompliance_gid and x.requestcompliance2lawyerdtl_gid=y.requestcompliance2lawyerdtl_gid" +
                    " order by created_date Desc limit 0,1) as lastconversation," +
                    " (select count(*)" +
                    " from lgl_trn_tcompliance2lawyerconversation x" +
                    " where x.requestcompliance_gid = requestcompliance_gid and x.requestcompliance2lawyerdtl_gid=y.requestcompliance2lawyerdtl_gid" +
                    " and msgview_flag='N' and user_flag='Lawyer') as newmsg_count" +
                    " FROM lgl_trn_trequestcompliance2lawyerdtl y" +
                    " WHERE requestcompliance_gid = '" + requestcompliance_gid + "'" +
                    " ORDER BY lawyeruser_name";
            dt_datatable = objdbconn.GetDataTable(msSQL);

            if (dt_datatable.Rows.Count != 0)
            {
                values.MdlLawyerSummary = dt_datatable.AsEnumerable().Select(row => new MdlLawyerSummary
                {
                    requestcompliance2lawyerdtl_gid = row["requestcompliance2lawyerdtl_gid"].ToString(),
                    lawyerregister_gid = row["lawyerregister_gid"].ToString(),
                    lawyeruser_name = row["lawyeruser_name"].ToString(),
                    name_initial = row["name_initial"].ToString(),
                    lastconversation = (row["lastconversation"].ToString() == "") ? "No Conversation" : row["lastconversation"].ToString(),
                    newmsg_count = (row["newmsg_count"].ToString() == "") ? "0" : row["newmsg_count"].ToString()
                }).ToList();
                values.status = true;
                values.message = "Record Retrieved";

                dt_datatable.Dispose();
            }
            else
            {
                dt_datatable.Dispose();
                values.status = false;
                values.message = "No Record";
            }

        }

        public result DaPostLawyerConversation(MdlLawyerConversation values, string user_gid, string employee_gid)
        {


            msGetGid = objcmnfunctions.GetMasterGID("LCON");

            result objResult = new Models.result();
            msSQL = " insert into lgl_trn_tcompliance2lawyerconversation(" +
                    " lawyerconversation_gid," +
                    " requestcompliance_gid," +
                    " requestcompliance2lawyerdtl_gid," +
                    " user_gid," +
                    " user_name," +
                    " msgconversation," +
                    " user_flag," +
                    " created_by)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.requestcompliance_gid + "'," +
                    "'" + values.requestcompliance2lawyerdtl_gid + "'," +
                    "'" + user_gid + "',";
            if (values.user_flag == "Lawyer")
            {
                msSQL += "(select concat(lawyeruser_code,'/', lawyeruser_name) from lgl_mst_tlawyeruser where  lawyeruser_gid='" + user_gid + "'),";
            }
            else
            {
                msSQL += "(select concat(user_code,'/',user_firstname,' ',user_lastname) from adm_mst_tuser where user_gid='" + user_gid + "'),";
            }
            msSQL += "'" + values.msgconversation.Replace("'", "") + "'," +
                     "'" + values.user_flag + "'," +
                     "'" + user_gid + "')";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnresult == 1)
            {
                objResult.status = true;
                objResult.message = "Conversation Raised Successfully";

            }
            else
            {
                objResult.status = false;
                objResult.message = "Error";
            }

            return objResult;

        }
        public void DaGetLawyerConversation(ComplainceValue values, MdlConversationSummaryList objResult, string user_gid)
        {
            msSQL = " select lawyerconversation_gid,user_name,msgconversation,msgview_flag,user_flag,user_gid," +
                    " date_format(created_date,'%d-%m-%Y %h:%i %p') as created_date" +
                    " from lgl_trn_tcompliance2lawyerconversation" +
                    " where requestcompliance_gid='" + values.requestcompliance_gid + "' and requestcompliance2lawyerdtl_gid='" + values.requestcompliance2lawyerdtl_gid + "'" +
                    " order by created_date";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                objResult.MdlConversationSummary = dt_datatable.AsEnumerable().Select(row => new MdlConversationSummary
                {
                    myself_flag = (row["user_gid"].ToString() == user_gid) ? "Y" : "N",
                    lawyerconversation_gid = row["lawyerconversation_gid"].ToString(),
                    user_name = row["user_name"].ToString(),
                    msgconversation = row["msgconversation"].ToString(),
                    msgview_flag = row["msgview_flag"].ToString(),
                    created_date = row["created_date"].ToString(),
                    user_flag = row["user_flag"].ToString(),
                }).ToList();
                objResult.status = true;
                objResult.message = "Record Retrieved";

                dt_datatable.Dispose();
            }
            else
            {
                dt_datatable.Dispose();
                objResult.status = false;
                objResult.message = "No Record";
            }
        }
        public void DaMsgViewed(ComplainceValue values)
        {
            msSQL = " update lgl_trn_tcompliance2lawyerconversation set" +
                    " msgview_flag='Y'" +
                    " where requestcompliance_gid ='" + values.requestcompliance_gid + "' and requestcompliance2lawyerdtl_gid='" + values.requestcompliance2lawyerdtl_gid + "' and user_flag='" + values.user_flag + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
        }

        public void DaGetLawyerGroupDtls(string requestcompliance_gid, MdlLawyerGroupDtls values)
        {
            msSQL = " select * from" +
                    " (select count(*) as newmsg_count" +
                    " from lgl_trn_tcompliance2lawyerconversation " +
                    " where requestcompliance_gid = '" + requestcompliance_gid + "' and requestcompliance2lawyerdtl_gid='" + requestcompliance_gid + "'" +
                    " and msgview_flag='N' and user_flag='Lawyer') as newmsg_count," +
                     " (select count(*) as totalmsg_count" +
                    " from lgl_trn_tcompliance2lawyerconversation x" +
                    " where x.requestcompliance_gid = '" + requestcompliance_gid + "'" +
                    " and msgview_flag='N' and user_flag='Lawyer') as totalmsg_count," +
                    " (select count(*) as lawyer_count from lgl_trn_trequestcompliance2lawyerdtl where requestcompliance_gid='" + requestcompliance_gid + "') as lawyer_count," +
                    " (select group_concat(distinct lawyeruser_name order by lawyeruser_name separator ' ; ') as group_member from lgl_trn_trequestcompliance2lawyerdtl where requestcompliance_gid='" + requestcompliance_gid + "')as group_member";
            objodbcdatareader = objdbconn.GetDataReader(msSQL);
            if (objodbcdatareader.HasRows)
            {
                objodbcdatareader.Read();
                values.group_member = objodbcdatareader["group_member"].ToString();

                values.newmsg_count = objodbcdatareader["newmsg_count"].ToString();
                values.totalmsg_count = objodbcdatareader["totalmsg_count"].ToString();
                values.lawyer_count = objodbcdatareader["lawyer_count"].ToString();
                objodbcdatareader.Close();
            }
            else
            {
                values.group_member = "";
                values.newmsg_count = "0";
                values.lawyer_count = "0";
                values.totalmsg_count = "0";
                objodbcdatareader.Close();
            }

            msSQL = "select concat(SUBSTR(x.msgconversation, 1, 128), '...') as lastconversation" +
                    " from lgl_trn_tcompliance2lawyerconversation x" +
                    " where x.requestcompliance_gid = '" + requestcompliance_gid + "' and x.requestcompliance2lawyerdtl_gid='" + requestcompliance_gid + "'" +
                    " order by created_date Desc limit 0,1";
            values.lastconversation = objdbconn.GetExecuteScalar(msSQL);
            if (values.lastconversation == "")
            {
                values.lastconversation = "No Conversation";
            }
        }
        public void DaGeteditrequesttype(string requesttype_gid, mdlrequesttype values)
        {
            msSQL = "select request_type from lgl_mst_trequesttype where requesttype_gid='" + requesttype_gid + "'";
            values.request_type = objdbconn.GetExecuteScalar(msSQL);
            values.status = true;
        }
        // Lawyer Group Conversation
        public result DaPostLawyerGroupConversation(MdlLawyerConversation values, string user_gid, string employee_gid)
        {
            msSQL = "select requestcompliance2lawyerdtl_gid from lgl_trn_trequestcompliance2lawyerdtl where requestcompliance_gid='" + values.requestcompliance_gid + "'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("LCON");
                    msSQL = " insert into lgl_trn_tcompliance2lawyerconversation(" +
                            " lawyerconversation_gid," +
                            " requestcompliance_gid," +
                            " requestcompliance2lawyerdtl_gid," +
                            " user_gid," +
                            " user_name," +
                            " msgconversation," +
                            " user_flag," +
                            " created_by)" +
                            " values(" +
                            "'" + msGetGid + "'," +
                            "'" + values.requestcompliance_gid + "'," +
                            "'" + dt["requestcompliance2lawyerdtl_gid"].ToString() + "'," +
                            "'" + user_gid + "',";
                    if (values.user_flag == "Lawyer")
                    {
                        msSQL += "(select concat(lawyeruser_code,'/', lawyeruser_name) from lgl_mst_tlawyeruser where  lawyeruser_gid='" + user_gid + "'),";
                    }
                    else
                    {
                        msSQL += "(select concat(user_code,'/',user_firstname,' ',user_lastname) from adm_mst_tuser where user_gid='" + user_gid + "'),";
                    }
                    msSQL += "'" + values.msgconversation.Replace("'", "") + "'," +
                             "'" + values.user_flag + "'," +
                             "'" + user_gid + "')";
                    mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            dt_datatable.Dispose();

            msGetGid = objcmnfunctions.GetMasterGID("LCON");

            result objResult = new Models.result();
            msSQL = " insert into lgl_trn_tcompliance2lawyerconversation(" +
                    " lawyerconversation_gid," +
                    " requestcompliance_gid," +
                    " requestcompliance2lawyerdtl_gid," +
                    " user_gid," +
                    " user_name," +
                    " msgconversation," +
                    " user_flag," +
                    " created_by)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.requestcompliance_gid + "'," +
                    "'" + values.requestcompliance2lawyerdtl_gid + "'," +
                    "'" + user_gid + "',";
            if (values.user_flag == "Lawyer")
            {
                msSQL += "(select concat(lawyeruser_code,'/', lawyeruser_name) from lgl_mst_tlawyeruser where  lawyeruser_gid='" + user_gid + "'),";
            }
            else
            {
                msSQL += "(select concat(user_code,'/',user_firstname,' ',user_lastname) from adm_mst_tuser where user_gid='" + user_gid + "'),";
            }
            msSQL += "'" + values.msgconversation.Replace("'", "") + "'," +
                     "'" + values.user_flag + "'," +
                     "'" + user_gid + "')";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnresult == 1)
            {
                objResult.status = true;
                objResult.message = "Conversation Raised Successfully";

            }
            else
            {
                objResult.status = false;
                objResult.message = "Error";
            }

            return objResult;
        }
        public void DaGetlawyerStatus(string requestcompliance_gid, MdlLawyerSummaryList values)
        {


            msSQL = " select lawyeruser_name,request_status from lgl_trn_trequestcompliance2lawyerdtl" +
                    " WHERE requestcompliance_gid = '" + requestcompliance_gid + "'" +
                    " ORDER BY lawyeruser_name";
            dt_datatable = objdbconn.GetDataTable(msSQL);

            if (dt_datatable.Rows.Count != 0)
            {
                values.MdlLawyerSummary = dt_datatable.AsEnumerable().Select(row => new MdlLawyerSummary
                {
                    lawyeruser_name = row["lawyeruser_name"].ToString(),
                    request_status = row["request_status"].ToString(),

                }).ToList();
                values.status = true;
                values.message = "Record Retrieved";

                dt_datatable.Dispose();
            }
            else
            {
                dt_datatable.Dispose();
                values.status = false;
                values.message = "No Record";
            }

        }
        public bool DaUploadAdditionalDocument(assignlawyer values, string employee_gid)
        {


            if ((values.taggedlawyerList != null))
            {
                for (var i = 0; i < values.taggedlawyerList.Count; i++)
                {

                    msSQL = "select * from lgl_tmp_tseeklawyerdocument where created_by='" + employee_gid + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            msGetDocumentGid = objcmnfunctions.GetMasterGID("SLDO");
                            msSQL = " Insert into lgl_trn_tseeklawyerdocument( " +
                                   " seekdocument_gid," +
                                   " requestcompliance2lawyerdtl_gid," +
                                   " request_compliancegid," +
                                   " document_name," +
                                   " document_path," +
                                   " created_by," +
                                   " created_date)" +
                                   " values(" +
                                   "'" + msGetDocumentGid + "', " +
                                   "'" + values.taggedlawyerList[i].requestcompliance2lawyerdtl_gid + "'," +
                                   "'" + values.requestcompliance_gid + "'," +
                                   "'" + dt["document_name"].ToString() + "'," +
                                   "'" + dt["document_path"].ToString() + "'," +
                                   "'" + employee_gid + "'," +
                                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        }
                    }
                    dt_datatable.Dispose();

                }
            }

            if (mnresult == 1)
            {
                msSQL = "delete from lgl_tmp_tseeklawyerdocument where created_by ='" + employee_gid + "'";
                mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }



            if (mnresult != 0)
            {
                values.status = true;
                return true;
            }
            else
            {
                values.status = false;
                return false;
            }

        }


        public bool DaGetTempDelete(string user_gid, resultvalue value)
        {

            msSQL = " delete from lgl_trn_tmanagecompliancecorrecteddoc where (requestcompliance_gid like 'SER%' or requestcompliance_gid='E1')";
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


        public bool DaGetTempDel(string user_gid, resultvalue value)
        {

            msSQL = " delete from lgl_tmp_tuploadcompliancedocument where (requestcompliance_gid like 'SER%' or requestcompliance_gid='E1')";
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


    }
}