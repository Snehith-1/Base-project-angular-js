using ems.mastersamagro.Models;
using ems.utilities.Functions;
using System;
using System.Web;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Data.Odbc;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace ems.mastersamagro.DataAccess
{
    /// <summary>
    /// This DataAccess will provide access to business approval process in aplication creation flow.
    /// </summary>
    /// <remarks>Written by Sherin Augusta, Logapriya, Abilash.A, Premchander.K </remarks>
    public class DaAgrTrnApplicationApproval
    {

        string sToken = string.Empty;
        Random rand = new Random();
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        OdbcDataReader objODBCDatareader, objODBCDatareader1;
        DataTable dt_datatable;
        string msSQL, msGetGid;
        int mnResult;
        public string ls_username;
        public string ls_password;
        public string ls_server;
        public int ls_port;
        public string body;
        public string sub;
        public string cc_mailid, lsBccmail_id;
        public string tomail_id;
        public string lssource;
        public string lsclusterhead;
        private IEnumerable<string> lsCCReceipients, lstoReceipients, lsBCCReceipients;
        private string customer_name;
        private string application_no;
        private string cluster_head;
        private string zonal_head;
        private string creditnationalmanager_mailid, reportingto_mailid;
        private string creditregionalmanager_mailid;
        private string relationshipmanager_name;
        private string relationshipmanager_mailid,lsapplicationapproval_gid;
        private string finalapproved_time;
        private string regional_head_mailid;
        private string business_head_mailid;
        private string zonalhead_mailid;
        private string creater_mailid;
        private string cluster_head_name, reportingto_name;
        private string business_head_gid, reportingto_gid;
        private string regional_head_gid;
        private string zonal_head_gid;
        private string content;
        private string cluster_head_gid;
        private string cluster_head_mailid;
        private string business_head_name;
        private string regional_head_name;
        private string relationshipmanager_gid;
        private object head_name;
        private string creater_name;
        private string closure_time;
        private string lsstatus;
        private string rm_name;

        string lsoveralllimit_amount, ls_Product, ls_Program, ls_Margin, regionalhead_name, lsproductdesk_name, lsproductdesk_flag;

        public void DaGetappapprovalinitiate(string application_gid, string employee_gid, string user_gid)
        {
            try
            {
                msSQL = " select applicationapproval_gid from agr_trn_tapplicationapproval where application_gid ='" + application_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    msSQL = " delete from agr_trn_tapplicationapproval where application_gid ='" + application_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                objODBCDatareader.Close();
                int k;
                string lsapproval_gid;
                string lsapprovalname;
                msSQL = " select concat( g.user_firstname, ' ', g.user_lastname, '/', g.user_code) as level_one ,a.employeereporting_to " +
                        "  from adm_mst_tmodule2employee a " +
                        "  left join hrm_mst_temployee f on a.employeereporting_to = f.employee_gid " +
                        "  left join adm_mst_tuser g on g.user_gid = f.user_gid  " +
                               "  where a.module_gid in  (select module_gid_parent from adm_mst_tmodule where module_gid in " +
                               "  (select modulereportingto_gid from adm_mst_tcompany)) " +
                               " and g.user_status = 'Y' and a.employee_gid ='" + employee_gid + "' group by a.employee_gid ";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    k = 1;
                    string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
                    sToken = "";
                    int Length = 100;
                    for (int j = 0; j < Length; j++)
                    {
                        string sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                        sToken += sTempChars;
                    }

                    msGetGid = objcmnfunctions.GetMasterGID("APAP");
                    msSQL = "Insert into agr_trn_tapplicationapproval( " +
                           " applicationapproval_gid, " +
                           " application_gid," +
                           " approval_gid," +
                           " approval_name," +
                           " approval_type," +
                           " hierary_level," +
                           " approval_token," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetGid + "'," +
                           "'" + application_gid + "'," +
                           "'" + objODBCDatareader["employeereporting_to"].ToString() + "'," +
                           "'" + objODBCDatareader["level_one"].ToString() + "'," +
                           "'sequence'," +
                           "'" + k + "'," +
                           "'" + sToken + "'," +
                           "'" + user_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                }
                objODBCDatareader.Close();

                msSQL = " select clustermanager_name,zonalhead_name,regionalhead_name,businesshead_name,clustermanager_gid,zonalhead_gid,regionalhead_gid,businesshead_gid from agr_mst_tapplication where application_gid = '" + application_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {

                    for (k = 2; k < 6; k++)
                    {
                        char level;
                        level = Convert.ToChar(k);
                        lsapproval_gid = "";
                        lsapprovalname = "";

                        if (level == '\u0002')
                        {
                            lsapproval_gid = objODBCDatareader["clustermanager_gid"].ToString();
                            lsapprovalname = objODBCDatareader["clustermanager_name"].ToString();
                        }
                        else if (level == '\u0003')
                        {
                            lsapproval_gid = objODBCDatareader["regionalhead_gid"].ToString();
                            lsapprovalname = objODBCDatareader["regionalhead_name"].ToString();
                        }
                        else if (level == '\u0004')
                        {
                            lsapproval_gid = objODBCDatareader["zonalhead_gid"].ToString();
                            lsapprovalname = objODBCDatareader["zonalhead_name"].ToString();
                        }
                        else if (level == '\u0005')
                        {
                            lsapproval_gid = objODBCDatareader["businesshead_gid"].ToString();
                            lsapprovalname = objODBCDatareader["businesshead_name"].ToString();
                        }
                        string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
                        sToken = "";
                        int Length = 100;
                        for (int j = 0; j < Length; j++)
                        {
                            string sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                            sToken += sTempChars;
                        }

                        msGetGid = objcmnfunctions.GetMasterGID("APAP");

                        msSQL = "Insert into agr_trn_tapplicationapproval( " +
                               " applicationapproval_gid, " +
                               " application_gid," +
                               " approval_gid," +
                               " approval_name," +
                               " approval_type," +
                               " hierary_level," +
                               " approval_token," +
                               " created_by," +
                               " created_date)" +
                               " values(" +
                               "'" + msGetGid + "'," +
                               "'" + application_gid + "'," +
                               "'" + lsapproval_gid + "'," +
                               "'" + lsapprovalname + "'," +
                               "'sequence'," +
                               "'" + k + "'," +
                               "'" + sToken + "'," +
                               "'" + user_gid + "'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    }
                }

                objODBCDatareader.Close();


            }
            catch (Exception ex)
            {

            }
        }

        public void DaPostApplicationcommentadd(mdlcomment values, string user_gid, string employee_gid)
        {
            msGetGid = objcmnfunctions.GetMasterGID("APCM");

            msSQL = " insert into agr_trn_tapplicationcomment(" +
                     " applicationcomment_gid," +
                     " applicationapproval_gid," +
                     " application_gid, " +
                     " comment_title," +
                     " comment_description," +
                     " created_by," +
                     " created_date)" +
                     " values(" +
                     "'" + msGetGid + "'," +
                     "'" + values.applicationapproval_gid + "', " +
                     "'" + values.application_gid + "', " +
                     "'" + values.commenttitle + "'," +
                     "'" + values.commentdesc.Replace("'", "") + "'," +
                     "'" + user_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msSQL = "update agr_mst_tapplication set  headapproval_status='Comment Raised'" +
                        " where application_gid='" + values.application_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                values.status = true;
                values.message = "Comment Created Successfully..!";
                //Mail Start
                try
                {
                    String lshierarchylevel;
                    lshierarchylevel = objdbconn.GetExecuteScalar("select hierary_level from agr_trn_tapplicationcomment a " +
                    "left join agr_trn_tapplicationapproval b on a.applicationapproval_gid = b.applicationapproval_gid where b.applicationapproval_gid='" + values.applicationapproval_gid + "'");
                    int level = Convert.ToInt16(lshierarchylevel);
                    int nextlevel = level + 1;
                    char nexthierlevel = Convert.ToChar(nextlevel);

                    msSQL = "select application_no from agr_mst_tapplication where application_gid='" + values.application_gid + "'";
                    application_no = objdbconn.GetExecuteScalar(msSQL);
                    msSQL = "select customerref_name from agr_mst_tapplication where application_gid='" + values.application_gid + "'";
                    customer_name = objdbconn.GetExecuteScalar(msSQL);
                    msSQL = "select b.employee_emailid from agr_mst_tapplication a left join hrm_mst_temployee b on b.employee_gid = a.clustermanager_gid where a.application_gid='" + values.application_gid + "'";
                    cluster_head_mailid = objdbconn.GetExecuteScalar(msSQL);
                    msSQL = " select b.employee_emailid from agr_mst_tapplication a left join hrm_mst_temployee b on b.employee_gid = a.zonalhead_gid where a.application_gid='" + values.application_gid + "'";
                    zonalhead_mailid = objdbconn.GetExecuteScalar(msSQL);
                    msSQL = " select b.employee_emailid from agr_mst_tapplication a left join hrm_mst_temployee b on b.employee_gid = a.regionalhead_gid where a.application_gid='" + values.application_gid + "'";
                    regional_head_mailid = objdbconn.GetExecuteScalar(msSQL);
                    msSQL = " select b.employee_emailid from agr_mst_tapplication a left join hrm_mst_temployee b on b.employee_gid = a.businesshead_gid where a.application_gid='" + values.application_gid + "'";
                    business_head_mailid = objdbconn.GetExecuteScalar(msSQL);
                    msSQL = " select b.employee_emailid from agr_trn_tapplicationapproval a left join hrm_mst_temployee b on b.employee_gid = a.approval_gid where a.application_gid='" + values.application_gid + "'  and hierary_level ='1'";
                    reportingto_mailid = objdbconn.GetExecuteScalar(msSQL);
                    msSQL = " select b.employee_emailid from agr_mst_tapplication a left join hrm_mst_temployee b on b.employee_gid = a.created_by where a.application_gid='" + values.application_gid + "'";
                    creater_mailid = objdbconn.GetExecuteScalar(msSQL);


                    msSQL = " select clustermanager_name,zonalhead_name,relationshipmanager_name from agr_mst_tapplication where application_gid = '" + values.application_gid + "'";
                    objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader1.HasRows == true)
                    {
                        cluster_head = objODBCDatareader1["clustermanager_name"].ToString();
                        zonal_head = objODBCDatareader1["zonalhead_name"].ToString();
                        relationshipmanager_name = objODBCDatareader1["relationshipmanager_name"].ToString();
                    }
                    objODBCDatareader1.Close();

                    msSQL = " select clustermanager_name,zonalhead_name,regionalhead_name,businesshead_name,clustermanager_gid, zonalhead_gid,regionalhead_gid,businesshead_gid from agr_mst_tapplication where application_gid = '" + values.application_gid + "'";
                    objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader1.HasRows == true)
                    {
                        cluster_head_name = objODBCDatareader1["clustermanager_name"].ToString();
                        zonal_head = objODBCDatareader1["zonalhead_name"].ToString();
                        regional_head_name = objODBCDatareader1["regionalhead_name"].ToString();
                        business_head_name = objODBCDatareader1["businesshead_name"].ToString();
                        cluster_head_gid = objODBCDatareader1["clustermanager_gid"].ToString();
                        zonal_head_gid = objODBCDatareader1["zonalhead_gid"].ToString();
                        regional_head_gid = objODBCDatareader1["regionalhead_gid"].ToString();
                        business_head_gid = objODBCDatareader1["businesshead_gid"].ToString();
                    }
                    objODBCDatareader1.Close();

                    msSQL = " SELECT pop_server, pop_port, pop_username, pop_password" +
                                    " FROM adm_mst_tcompany";
                    objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader1.HasRows == true)
                    {
                        ls_server = objODBCDatareader1["pop_server"].ToString();
                        ls_port = Convert.ToInt32(objODBCDatareader1["pop_port"]);
                        ls_username = objODBCDatareader1["pop_username"].ToString();
                        ls_password = objODBCDatareader1["pop_password"].ToString();
                    }
                    objODBCDatareader1.Close();
                    tomail_id = creater_mailid;
                    if (nexthierlevel == '\u0006')
                    {
                        cc_mailid = "" + reportingto_mailid + "," + regional_head_mailid + "," + cluster_head_mailid + "," + zonalhead_mailid + "";
                    }
                    else if (nexthierlevel == '\u0005')
                    {
                        cc_mailid = "" + reportingto_mailid + "," + cluster_head_mailid + "," + regional_head_mailid + "," + creater_mailid + "";
                    }
                    else if (nexthierlevel == '\u0004')
                    {
                        cc_mailid = "" + reportingto_mailid + "," + creater_mailid + "," + cluster_head_mailid + "";
                    }
                    else if (nexthierlevel == '\u0003')
                    {
                        cc_mailid = "" + reportingto_mailid + "," + zonalhead_mailid + "," + creater_mailid + "";
                    }
                    else if (nexthierlevel == '\u0002')
                    {
                        cc_mailid = "" + reportingto_mailid + "," + creater_mailid + "," + zonalhead_mailid + "";
                    }

                    msSQL = " select approval_status from agr_trn_tapplicationapproval  where applicationapproval_gid ='" + values.applicationapproval_gid + "'";
                    lsstatus = objdbconn.GetExecuteScalar(msSQL);

                    //lssource = ConfigurationManager.AppSettings["img_path"];
                    sub = " New comment raised on ARN(" + application_no + ")  ";
                    body = "<style>table, th, td {border: 1px solid black;border-collapse: collapse;}</style>";
                    body = body + "<table style='border-right: 1px solid black;border-top: 1px solid black;border-bottom: 1px solid black;'><tr><td style='border-right-color:white;align:center;'>";
                    body = body + "<br />";
                    body = body + "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp <img style='height:150px; width:380px;' src='" + lssource + "'><br />";
                    body = body + "<br />";
                    body = body + " &nbsp &nbsp Dear Sir / Madam, <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp Greetings! <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp New comment has been raised for the below application ";
                    body = body + "<br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp <b> Application Number : </b> " + application_no + "  <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp <b> Customer Name : </b> " + HttpUtility.HtmlEncode(customer_name) + "  <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp <b> RM Name : </b> " + HttpUtility.HtmlEncode(relationshipmanager_name)+ "  <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp <b> Cluster Head Name : </b> " + HttpUtility.HtmlEncode(cluster_head )+ "  <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp <b> Zonal Head Name : </b> " + HttpUtility.HtmlEncode(zonal_head)+ "  <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp Login to " + ConfigurationManager.AppSettings["livedomain_url"].ToString() + " and complete the necessary actions.";
                    body = body + "<br />";
                    //body = body + "&nbsp &nbsp Regards";
                    //body = body + "<br />";
                    //body = body + "&nbsp &nbsp Sam-Custopedia <br /> ";
                    //body = body + "<br />";
                    //body = body + "&nbsp &nbsp<hr>&nbsp&nbsp";
                    //body = body + "&nbsp &nbsp Reach out to us at samcustopedia@samunnati.com <br /> ";
                    //body = body + "<br />";
                    body = body + "</td><td style='margin-left:20px; border-left-color:white;'>&nbsp&nbsp</td></tr></table>";

                    MailMessage message = new MailMessage();
                    SmtpClient smtp = new SmtpClient();
                    message.From = new MailAddress(ls_username);
                    message.To.Add(new MailAddress(tomail_id));
                    lsBccmail_id = ConfigurationManager.AppSettings["SamagroApprovalBccMail"].ToString();

                    if (lsBccmail_id != null & lsBccmail_id != string.Empty & lsBccmail_id != "")
                    {
                        lsBCCReceipients = lsBccmail_id.Split(',');
                        if (lsBccmail_id.Length == 0)
                        {
                            message.Bcc.Add(new MailAddress(lsBccmail_id));
                        }
                        else
                        {
                            foreach (string BCCEmail in lsBCCReceipients)
                            {
                                message.Bcc.Add(new MailAddress(BCCEmail)); //Adding Multiple BCC email Id
                            }
                        }
                    }


                    if (cc_mailid != null & cc_mailid != string.Empty & cc_mailid != "")
                    {
                        lsCCReceipients = cc_mailid.Split(',');
                        if (cc_mailid.Length == 0)
                        {
                            message.CC.Add(new MailAddress(cc_mailid));
                        }
                        else
                        {
                            foreach (string CCEmail in lsCCReceipients)
                            {
                                message.CC.Add(new MailAddress(CCEmail)); //Adding Multiple CC email Id
                            }
                        }
                    }



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
                }
                catch (Exception ex)
                {
                    values.message = ex.ToString();
                }
                //Mail End
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }


        }

        public void DaGetApprovalSummary(applicationapproval values, string application_gid)
        {
            msSQL = " select applicationapproval_gid,approval_name,approval_status ,b.user_code,a.hierary_level," +
                     " concat(b.user_firstname, ' ', b.user_lastname)  as created_by,approval_remarks," +
                     " date_format(a.approved_date, '%d-%m-%Y %h:%i %p') as approved_date, " +
                     " date_format(b.created_date, '%d-%m-%Y %h:%i %p') as created_date,date_format(a.hold_date, '%d-%m-%Y %h:%i %p') as hold_date," +
                     " date_format(a.rejected_date, '%d-%m-%Y %h:%i %p') as rejected_date from agr_trn_tapplicationapproval a " +
                     " left join adm_mst_tuser b on b.user_gid = a.created_by " +
                     " where a.application_gid='" + application_gid + "' order by hierary_level asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationapprovallist = new List<applicationapprovallist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getapplicationapprovallist.Add(new applicationapprovallist
                    {
                        applicationapproval_gid = dt["applicationapproval_gid"].ToString(),
                        approval_name = dt["approval_name"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        approval_remarks = dt["approval_remarks"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        approved_date = dt["approved_date"].ToString(),
                        hold_date = dt["hold_date"].ToString(),
                        rejected_date = dt["rejected_date"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        hierary_level = dt["hierary_level"].ToString(),
                        user_code = dt["user_code"].ToString(),
                    });
                    values.applicationapprovallist = getapplicationapprovallist;
                }
            }
            dt_datatable.Dispose();


        }

        public void DaGetAppcommentsSummary(applicationapproval values, string application_gid)
        {
            msSQL = " select applicationcomment_gid,a.applicationapproval_gid,comment_title,comment_status,comment_description,c.approval_name,c.hierary_level,a.close_remarks, " +
                     " concat(b.user_firstname, ' ', b.user_lastname, ' / ', b.user_code) as created_by,date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date from agr_trn_tapplicationcomment a" +
                     " left join adm_mst_tuser b on b.user_gid = a.created_by " +
                     " left join agr_trn_tapplicationapproval c on a.applicationapproval_gid=c.applicationapproval_gid " +
                     " where a.application_gid='" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationcommentslist = new List<applicationcommentslist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getapplicationcommentslist.Add(new applicationcommentslist
                    {
                        applicationapproval_gid = dt["applicationapproval_gid"].ToString(),
                        applicationcomment_gid = dt["applicationcomment_gid"].ToString(),
                        commenttitle = dt["comment_title"].ToString(),
                        commenttatus = dt["comment_status"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        commentdesc = dt["comment_description"].ToString(),
                        hierary_level = dt["hierary_level"].ToString(),
                        close_remarks = dt["close_remarks"].ToString()
                    });
                    values.applicationcommentslist = getapplicationcommentslist;
                }
            }
            dt_datatable.Dispose();


        }

        public void DaGetAppcommentdesc(mdlcommentdesc values, string applicationcomment_gid)
        {
            msSQL = "select comment_description from agr_trn_tapplicationcomment where applicationcomment_gid='" + applicationcomment_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.commentdesc = objODBCDatareader["comment_description"].ToString();
            }
            objODBCDatareader.Close();
        }

        public void DaGetapplicationhierarchylist(applicationhierarchy values, string application_gid, string employee_gid)
        {
            try
            {
                msSQL = " select clustermanager_name,zonalhead_name,regionalhead_name,businesshead_name, relationshipmanager_name as level_zero, drm_name as level_one from agr_mst_tapplication where application_gid = '" + application_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.clusterhead = objODBCDatareader["clustermanager_name"].ToString();
                    values.zonalhead = objODBCDatareader["zonalhead_name"].ToString();
                    values.regionhead = objODBCDatareader["regionalhead_name"].ToString();
                    values.businesshead = objODBCDatareader["businesshead_name"].ToString();
                    values.level_zero = objODBCDatareader["level_zero"].ToString();
                    values.level_one = objODBCDatareader["level_one"].ToString();
                }

                objODBCDatareader.Close();

                //msSQL = " select a.employeereporting_to,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as level_zero,b.employee_gid, " +
                //        "  concat( g.user_firstname, ' ', g.user_lastname, ' / ', g.user_code) as level_one  " +
                //        "  from adm_mst_tmodule2employee a " +
                //        "  left join hrm_mst_temployee b on b.employee_gid = a.employee_gid " +
                //        "  left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                //        "  left join hrm_mst_temployee f on a.employeereporting_to = f.employee_gid " +
                //        "  left join adm_mst_tuser g on g.user_gid = f.user_gid  " +
                //               " where a.module_gid in  (select module_gid_parent from adm_mst_tmodule where module_gid in " +
                //               " and g.user_status = 'Y' and a.employee_gid ='" + employee_gid + "' group by a.employee_gid  ";
                //objODBCDatareader = objdbconn.GetDataReader(msSQL);
                //if (objODBCDatareader.HasRows == true)
                //{
                //    values.level_zero = objODBCDatareader["level_zero"].ToString();
                //    values.level_one = objODBCDatareader["level_one"].ToString();
                //    objODBCDatareader.Close();
                //}


                //objODBCDatareader.Close();
                values.status = true;
            }
            catch (Exception ex)
            {
                values.status = false;
            }
        }

        public void DaGetapplicationdetails(applicationdetials values, string application_gid)
        {
            try
            {
                msSQL = " select application_no,customer_name,approval_status,region,overalllimit_amount,shortclosing_reason,expired_flag,program_name from agr_mst_tapplication where application_gid = '" + application_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.program_name = objODBCDatareader["program_name"].ToString();
                    values.application_no = objODBCDatareader["application_no"].ToString();
                    values.customer_name = objODBCDatareader["customer_name"].ToString();
                    values.approval_status = objODBCDatareader["approval_status"].ToString();
                    values.region = objODBCDatareader["region"].ToString();
                    values.overalllimit_amount = objODBCDatareader["overalllimit_amount"].ToString();
                    values.shortclosing_reason = objODBCDatareader["shortclosing_reason"].ToString();
                    values.expired_flag = objODBCDatareader["expired_flag"].ToString();
                }
                objODBCDatareader.Close();
                msSQL = " select a.product_type,a.loanfacility_amount,a.tenureoverall_limit from agr_mst_tapplication2loan a " +
                          " left join agr_mst_tapplication b on a.application_gid=b.application_gid" +
                          " where a.application_gid = '" + application_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getproductlist = new List<productlist>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getproductlist.Add(new productlist
                        {
                            product_name = (dr_datarow["product_type"].ToString()),
                            facility_limit = (dr_datarow["loanfacility_amount"].ToString()),
                            tenure_limit = (dr_datarow["tenureoverall_limit"].ToString())
                        });
                    }
                    values.productlist = getproductlist;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch (Exception ex)
            {
                values.status = false;
            }
        }


        public bool DaPostApplicationHeadApproval(mdlappapproval values, string user_gid, string employee_gid)
        {
            String lshierarchylevel;
            lshierarchylevel = objdbconn.GetExecuteScalar("select hierary_level from agr_trn_tapplicationapproval where applicationapproval_gid='" + values.applicationapproval_gid + "'");
            Char hierarchylevel;
            int level = Convert.ToInt16(lshierarchylevel);
            int nextlevel = level + 1;
            char nexthierlevel = Convert.ToChar(nextlevel);
            hierarchylevel = Convert.ToChar(level);

            if (values.approval_status == "Approved" && hierarchylevel != '\u0004')
            {
                msSQL = " update agr_trn_tapplicationapproval set  approval_status='Approved',approval_remarks='" + values.approval_remarks.Replace("'", "") + "'," +
                        " approved_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                        " where applicationapproval_gid='" + values.applicationapproval_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                msSQL = "update agr_mst_tapplication set  headapproval_status='L" + lshierarchylevel + " - Approved',headapproval_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                      " where application_gid='" + values.application_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                msSQL = " select applicationapproval_gid,hierary_level from agr_trn_tapplicationapproval " +
                        " where application_gid='" + values.application_gid + "' and approval_gid='" + employee_gid + "' and " +
                        " applicationapproval_gid <>'" + values.applicationapproval_gid + "' and hierary_level = '" + nextlevel + "' order by hierary_level asc";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == false)
                {
                    try
                    {
                        msSQL = "select application_no from agr_mst_tapplication where application_gid='" + values.application_gid + "'";
                        application_no = objdbconn.GetExecuteScalar(msSQL);
                        msSQL = "select customerref_name from agr_mst_tapplication where application_gid='" + values.application_gid + "'";
                        customer_name = objdbconn.GetExecuteScalar(msSQL);
                        msSQL = " select clustermanager_name,zonalhead_name,regionalhead_name,businesshead_name,clustermanager_gid, zonalhead_gid,regionalhead_gid,businesshead_gid from agr_mst_tapplication where application_gid = '" + values.application_gid + "'";
                        objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader1.HasRows == true)
                        {
                            cluster_head_name = objODBCDatareader1["clustermanager_name"].ToString();
                            zonal_head = objODBCDatareader1["zonalhead_name"].ToString();
                            regional_head_name = objODBCDatareader1["regionalhead_name"].ToString();
                            business_head_name = objODBCDatareader1["businesshead_name"].ToString();
                            cluster_head_gid = objODBCDatareader1["clustermanager_gid"].ToString();
                            zonal_head_gid = objODBCDatareader1["zonalhead_gid"].ToString();
                            regional_head_gid = objODBCDatareader1["regionalhead_gid"].ToString();
                            business_head_gid = objODBCDatareader1["businesshead_gid"].ToString();
                        }
                        objODBCDatareader1.Close();
                        msSQL = " select approval_gid,approval_name from agr_trn_tapplicationapproval where application_gid = '" + values.application_gid + "' and hierary_level ='1'";
                        objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader1.HasRows == true)
                        {
                            reportingto_gid = objODBCDatareader1["approval_gid"].ToString();
                            reportingto_name = objODBCDatareader1["approval_name"].ToString();
                        }
                        objODBCDatareader1.Close();

                        msSQL = "select b.employee_emailid from agr_mst_tapplication a left join hrm_mst_temployee b on b.employee_gid = a.clustermanager_gid where application_gid='" + values.application_gid + "'";
                        cluster_head_mailid = objdbconn.GetExecuteScalar(msSQL);
                        msSQL = " select b.employee_emailid from agr_mst_tapplication a left join hrm_mst_temployee b on b.employee_gid = a.zonalhead_gid where a.application_gid='" + values.application_gid + "'";
                        zonalhead_mailid = objdbconn.GetExecuteScalar(msSQL);
                        msSQL = " select b.employee_emailid from agr_mst_tapplication a left join hrm_mst_temployee b on b.employee_gid = a.regionalhead_gid where a.application_gid='" + values.application_gid + "'";
                        regional_head_mailid = objdbconn.GetExecuteScalar(msSQL);
                        msSQL = " select b.employee_emailid from agr_mst_tapplication a left join hrm_mst_temployee b on b.employee_gid = a.businesshead_gid where a.application_gid='" + values.application_gid + "'";
                        business_head_mailid = objdbconn.GetExecuteScalar(msSQL);
                        msSQL = " select b.employee_emailid from agr_trn_tapplicationapproval a left join hrm_mst_temployee b on b.employee_gid = a.approval_gid where a.application_gid='" + values.application_gid + "'  and hierary_level ='1'";
                        reportingto_mailid = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = "select  concat( c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code)  from agr_mst_tapplication a left join hrm_mst_temployee b on b.employee_gid = a.clustermanager_gid join adm_mst_tuser c on c.user_gid = b.user_gid where application_gid='" + values.application_gid + "'";
                        cluster_head = objdbconn.GetExecuteScalar(msSQL);
                        msSQL = " select  concat( c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code)  from agr_mst_tapplication a left join hrm_mst_temployee b on b.employee_gid = a.zonalhead_gid left join adm_mst_tuser c on c.user_gid = b.user_gid where a.application_gid='" + values.application_gid + "'";
                        zonal_head = objdbconn.GetExecuteScalar(msSQL);
                        msSQL = " select  concat( c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code)  from agr_mst_tapplication a left join hrm_mst_temployee b on b.employee_gid = a.relationshipmanager_gid left join adm_mst_tuser c on c.user_gid = b.user_gid where a.application_gid='" + values.application_gid + "'";
                        rm_name = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = " select b.employee_emailid from agr_mst_tapplication a left join hrm_mst_temployee b on b.employee_gid = a.created_by where a.application_gid='" + values.application_gid + "'";
                        creater_mailid = objdbconn.GetExecuteScalar(msSQL);
                        msSQL = " select concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by  from agr_mst_tapplication a" +
                                " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                                " left join adm_mst_tuser c on c.user_gid=b.user_gid  where a.created_by = '" + values.created_by + "'";
                        creater_name = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = " select  concat( c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code)  from agr_mst_tapplication a left join hrm_mst_temployee b on b.employee_gid = a.regionalhead_gid left join adm_mst_tuser c on c.user_gid = b.user_gid where a.application_gid='" + values.application_gid + "'";
                        regionalhead_name = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = " select  a.overalllimit_amount  from agr_mst_tapplication a  where a.application_gid='" + values.application_gid + "'";
                        lsoveralllimit_amount = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = " select  group_concat(product_type) as Product, group_concat(productsub_type) as Program, group_concat(rate_interest) as Margin from agr_mst_tapplication2loan  where application_gid = '" + values.application_gid + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            ls_Product = objODBCDatareader["Product"].ToString();
                            ls_Program = objODBCDatareader["Program"].ToString();
                            ls_Margin = objODBCDatareader["Margin"].ToString();
                        }
                        objODBCDatareader.Close();

                        //if (nexthierlevel == '\u0006')
                        //{
                        //    tomail_id = creater_mailid;
                        //    cc_mailid = "" + reportingto_mailid + "," + regional_head_mailid + "," + cluster_head_mailid + "," + zonalhead_mailid + "";
                        //    content = "L5 Approved";
                        //    head_name = creater_name;
                        //}
                        //else if (nexthierlevel == '\u0005')
                        //{
                        //    tomail_id = business_head_mailid;
                        //    cc_mailid = "" + reportingto_mailid + "," + cluster_head_mailid + "," + regional_head_mailid + "," + creater_mailid + "";
                        //    content = "Pending L5 Approval";
                        //    head_name = business_head_name;
                        //}
                        if (nexthierlevel == '\u0005')
                        {
                            tomail_id = creater_mailid;
                            cc_mailid = "" + reportingto_mailid + "," + regional_head_mailid + "," + cluster_head_mailid + "," + zonalhead_mailid + "";
                            content = "L5 Approved";
                            head_name = creater_name;
                        }
                        else if (nexthierlevel == '\u0004')
                        {
                            tomail_id = zonalhead_mailid;
                            cc_mailid = "" + reportingto_mailid + "," + creater_mailid + "," + cluster_head_mailid + "";
                            content = "Pending L4 Approval";
                            head_name = zonal_head;
                        }
                        else if (nexthierlevel == '\u0003')
                        {
                            tomail_id = regional_head_mailid;
                            cc_mailid = "" + reportingto_mailid + "," + zonalhead_mailid + "," + creater_mailid + "";
                            content = "Pending L3 Approval";
                            head_name = regional_head_name;
                        }
                        else if (nexthierlevel == '\u0002')
                        {
                            tomail_id = cluster_head_mailid;
                            cc_mailid = "" + reportingto_mailid + "," + creater_mailid + "," + zonalhead_mailid + "";
                            content = "Pending L2 Approval";
                            head_name = cluster_head_name;
                        }


                        msSQL = " SELECT pop_server, pop_port, pop_username, pop_password" +
                            " FROM adm_mst_tcompany";
                        objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader1.HasRows == true)
                        {
                            ls_server = objODBCDatareader1["pop_server"].ToString();
                            ls_port = Convert.ToInt32(objODBCDatareader1["pop_port"]);
                            ls_username = objODBCDatareader1["pop_username"].ToString();
                            ls_password = objODBCDatareader1["pop_password"].ToString();
                        }
                        //lssource = ConfigurationManager.AppSettings["img_path"];
                        objODBCDatareader1.Close();
                        if (nexthierlevel == '\u0005')
                        {

                        }
                        else
                        {
                            sub = " ARN(" + application_no + ") : Application approval required ";
                            body = "<style>table, th, td {border: 1px solid black;border-collapse: collapse;}</style>";
                            body = body + "<table style='border-right: 1px solid black;border-top: 1px solid black;border-bottom: 1px solid black;'><tr><td style='border-right-color:white;align:center;'>";
                            body = body + " &nbsp&nbsp Dear Sir/Madam,<br />";
                            body = body + "<br />";
                            body = body + "&nbsp&nbsp Greetings<br />";
                            body = body + "<br />";
                            body = body + "&nbsp&nbsp The below application has been submitted, please validate and approve to proceed for underwriting.<br/>";
                            body = body + "<br/>";
                            body = body + "&nbsp&nbsp <b>Application Number:</b> " + application_no + "<br /><br />";
                            body = body + "&nbsp&nbsp <b>Customer Name:</b> " + HttpUtility.HtmlEncode(customer_name)+ "<br /><br />";
                            body = body + "&nbsp&nbsp <b>RM Name:</b>" + HttpUtility.HtmlEncode(rm_name )+ "<br /><br />";
                            body = body + "&nbsp&nbsp <b>Cluster Head Name:</b>" + HttpUtility.HtmlEncode(cluster_head)+ "<br /><br />";
                            body = body + "&nbsp&nbsp <b>Zonal Head Name:</b>" + HttpUtility.HtmlEncode(zonal_head)+ "<br /><br />";
                            body = body + "&nbsp&nbsp <b>Product:</b> " + ls_Product + "<br /><br />";
                            body = body + "&nbsp&nbsp <b>Program:</b> " + ls_Program + "<br /><br />";
                            body = body + "&nbsp&nbsp <b>Overall Limit Amount:</b> " + HttpUtility.HtmlEncode(lsoveralllimit_amount) + "<br /><br />";
                            body = body + "&nbsp&nbsp <b>Margin:</b> " + HttpUtility.HtmlEncode(ls_Margin)+ "<br /><br />";
                            body = body + "&nbsp&nbsp <b>Action Time:</b> " + DateTime.Now.ToString("dd-MM-yyyy hh:mm tt") + "<br />";
                            body = body + "<br />";
                            body = body + "&nbsp&nbsp Login to " + ConfigurationManager.AppSettings["livedomain_url"].ToString() + " and complete the necessary actions.";                          
                            body = body + "<br />";
                            body = body + "</td><td style='margin-left:20px; border-left-color:white;'>&nbsp&nbsp</td></tr></table>";

                            MailMessage message = new MailMessage();
                            SmtpClient smtp = new SmtpClient();
                            message.From = new MailAddress(ls_username);
                            message.To.Add(new MailAddress(tomail_id));
                            lsBccmail_id = ConfigurationManager.AppSettings["SamagroApprovalBccMail"].ToString();

                            if (lsBccmail_id != null & lsBccmail_id != string.Empty & lsBccmail_id != "")
                            {
                                lsBCCReceipients = lsBccmail_id.Split(',');
                                if (lsBccmail_id.Length == 0)
                                {
                                    message.Bcc.Add(new MailAddress(lsBccmail_id));
                                }
                                else
                                {
                                    foreach (string BCCEmail in lsBCCReceipients)
                                    {
                                        message.Bcc.Add(new MailAddress(BCCEmail)); //Adding Multiple BCC email Id
                                    }
                                }
                            }


                            if (cc_mailid != null & cc_mailid != string.Empty & cc_mailid != "")
                            {
                                lsCCReceipients = cc_mailid.Split(',');
                                if (cc_mailid.Length == 0)
                                {
                                    message.CC.Add(new MailAddress(cc_mailid));
                                }
                                else
                                {
                                    foreach (string CCEmail in lsCCReceipients)
                                    {
                                        message.CC.Add(new MailAddress(CCEmail)); //Adding Multiple CC email Id
                                    }
                                }
                            }



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

                    }
                    catch (Exception ex)
                    {
                        values.message = ex.ToString();
                        values.status = false;
                    }
                }
                objODBCDatareader.Close();
            }
            else if (values.approval_status == "Hold")
            {
                msSQL = " update agr_trn_tapplicationapproval set  approval_status='Hold',approval_remarks='" + values.approval_remarks.Replace("'", "") + "'," +
                       " hold_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                       " where applicationapproval_gid='" + values.applicationapproval_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                msSQL = "update agr_mst_tapplication set approval_status='Hold By Business',  headapproval_status='L" + lshierarchylevel + " - Hold',headapproval_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where application_gid='" + values.application_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                msSQL = "update agr_trn_tapplicationapproval set  approval_status='-' " +
                     " where application_gid='" + values.application_gid + "' and applicationapproval_gid > '" + values.applicationapproval_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else if (values.approval_status == "Rejected")
            {
                msSQL = " update agr_trn_tapplicationapproval set  approval_status='Rejected',approval_remarks='" + values.approval_remarks.Replace("'", "") + "'," +
                        " rejected_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                        " where applicationapproval_gid='" + values.applicationapproval_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                msSQL = "update agr_mst_tapplication set approval_status='Rejected By Business',  headapproval_status='L" + lshierarchylevel + " - Rejected',headapproval_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                       " where application_gid='" + values.application_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                msSQL = "update agr_trn_tapplicationapproval set  approval_status='-' " +
                      " where application_gid='" + values.application_gid + "' and applicationapproval_gid > '" + values.applicationapproval_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }


            else if (hierarchylevel == '\u0004' && values.approval_status == "Approved")
            {
                msSQL = " update agr_trn_tapplicationapproval set  approval_status='Approved',approval_remarks='" + values.approval_remarks.Replace("'", "") + "'," +
                       " approved_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                       " where applicationapproval_gid='" + values.applicationapproval_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "select applicationapproval_gid from agr_trn_tapplicationapproval where application_gid='" + values.application_gid + "' and  hierary_level = '5'";
                lsapplicationapproval_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " update agr_trn_tapplicationapproval set  approval_status='Approved',approval_remarks='Auto approved in backend',initiate_flag='Y'," +
                        " approved_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                        " where applicationapproval_gid='" + lsapplicationapproval_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    string lsproductdesk_gid = "";
                    msSQL = " select productdesk_gid from agr_mst_tproductdeskmapping where products_gid in (select producttype_gid from agr_mst_tapplication2loan	" +
                            " where application_gid='" + values.application_gid + "') and app_productdesk='Y' and productdesk_status='Y'";
                    lsproductdesk_gid = objdbconn.GetExecuteScalar(msSQL);
                    if (lsproductdesk_gid != "")
                    {
                        msSQL = " update agr_mst_tapplication set productdesk_flag='Y',approval_flag='Y', " +
                                " approval_status='Submitted to Product Desk', headapproval_status='All Heads Approved', productdesk_gid ='" + lsproductdesk_gid + "'" +
                                " where application_gid='" + values.application_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL); 
                    }
                    else
                    {
                        msSQL = " update agr_mst_tapplication set  approval_flag='Y', approval_status='Submitted to Underwriting', headapproval_status='All Heads Approved'" +
                                " where application_gid='" + values.application_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL); 
                        DaAgrTrnProductApproval objapproval = new DaAgrTrnProductApproval();
                        objapproval.FnAutoProductApprovalFlow(values.application_gid, employee_gid, user_gid);
                    }

                      msSQL = " select  productdesk_flag from agr_mst_tapplication a  where a.application_gid='" + values.application_gid + "'";
                            lsproductdesk_flag = objdbconn.GetExecuteScalar(msSQL);

                    try
                    {

                        if (hierarchylevel == '\u0005' && values.approval_status == "Approved" && String.IsNullOrEmpty(lsproductdesk_gid) )
                        {

                            msSQL = "select application_no from agr_mst_tapplication where application_gid='" + values.application_gid + "'";
                            application_no = objdbconn.GetExecuteScalar(msSQL);
                            msSQL = "select customerref_name from agr_mst_tapplication where application_gid='" + values.application_gid + "'";
                            customer_name = objdbconn.GetExecuteScalar(msSQL);
                            msSQL = "select approved_date from agr_trn_tapplicationapproval a left join agr_mst_tapplication b on b.application_gid = a.application_gid and hierary_level = '5' where b.application_gid='" + values.application_gid + "'";
                            finalapproved_time = objdbconn.GetExecuteScalar(msSQL);
                            msSQL = " select clustermanager_name,zonalhead_name,relationshipmanager_name from agr_mst_tapplication where application_gid = '" + values.application_gid + "'";
                            objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader1.HasRows == true)
                            {
                                cluster_head = objODBCDatareader1["clustermanager_name"].ToString();
                                zonal_head = objODBCDatareader1["zonalhead_name"].ToString();
                                relationshipmanager_name = objODBCDatareader1["relationshipmanager_name"].ToString();
                            }
                            objODBCDatareader1.Close();
                            msSQL = "select group_concat(c.employee_emailid) from agr_mst_tapplication a left join ocs_mst_tcredit2nationalmanager b on a.creditgroup_gid = b.creditmapping_gid left join hrm_mst_temployee c on c.employee_gid = b.employee_gid where a.application_gid = '" + values.application_gid + "'";
                            creditnationalmanager_mailid = objdbconn.GetExecuteScalar(msSQL);

                            msSQL = "select group_concat(c.employee_emailid) from agr_mst_tapplication a left join ocs_mst_tcreditr2regionalmanager b on a.creditgroup_gid = b.creditmapping_gid left join hrm_mst_temployee c on c.employee_gid = b.employee_gid where a.application_gid = '" + values.application_gid + "'";
                            creditregionalmanager_mailid = objdbconn.GetExecuteScalar(msSQL);

                            tomail_id = "" + creditnationalmanager_mailid + "," + creditregionalmanager_mailid + "";

                            msSQL = " select b.employee_emailid from agr_mst_tapplication a left join hrm_mst_temployee b on b.employee_gid = a.relationshipmanager_gid where a.application_gid='" + values.application_gid + "'";
                            relationshipmanager_mailid = objdbconn.GetExecuteScalar(msSQL);

                            msSQL = " select  concat( c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code)  from agr_mst_tapplication a left join hrm_mst_temployee b on b.employee_gid = a.regionalhead_gid left join adm_mst_tuser c on c.user_gid = b.user_gid where a.application_gid='" + values.application_gid + "'";
                            regionalhead_name = objdbconn.GetExecuteScalar(msSQL);

                            msSQL = " select  a.overalllimit_amount  from agr_mst_tapplication a  where a.application_gid='" + values.application_gid + "'";
                            lsoveralllimit_amount = objdbconn.GetExecuteScalar(msSQL);

                            msSQL = " select  group_concat(product_type) as Product, group_concat(productsub_type) as Program, group_concat(rate_interest) as Margin from agr_mst_tapplication2loan  where application_gid = '" + values.application_gid + "'";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                ls_Product = objODBCDatareader["Product"].ToString();
                                ls_Program = objODBCDatareader["Program"].ToString();
                                ls_Margin = objODBCDatareader["Margin"].ToString();
                            }
                            objODBCDatareader.Close();

                            msSQL = " SELECT pop_server, pop_port, pop_username, pop_password" +
                                    " FROM adm_mst_tcompany";
                            objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader1.HasRows == true)
                            {
                                ls_server = objODBCDatareader1["pop_server"].ToString();
                                ls_port = Convert.ToInt32(objODBCDatareader1["pop_port"]);
                                ls_username = objODBCDatareader1["pop_username"].ToString();
                                ls_password = objODBCDatareader1["pop_password"].ToString();
                            }
                            objODBCDatareader1.Close();
                            sub = " Application allocation : " + application_no + " ";
                            body = "<style>table, th, td {border: 1px solid black;border-collapse: collapse;}</style>";
                            body = body + "<table style='border-right: 1px solid black;border-top: 1px solid black;border-bottom: 1px solid black;'><tr><td style='border-right-color:white;'>";

                            body = body + " &nbsp &nbsp Dear Sir / Madam, <br />";
                            body = body + "<br />";
                            body = body + "&nbsp &nbsp Greetings! <br />";
                            body = body + "<br />";
                            body = body + "&nbsp &nbsp The below application has been submitted, please allocate it to a credit manager <br />";
                            body = body + "<br />";
                            body = body + "&nbsp &nbsp <b> Application Number : </b> " + application_no + "  <br />";
                            body = body + "<br />";
                            body = body + "&nbsp &nbsp <b> Customer Name : </b> " + HttpUtility.HtmlEncode(customer_name )+ "  <br />";
                            body = body + "<br />";
                            body = body + "&nbsp &nbsp <b> RM Name : </b> " + HttpUtility.HtmlEncode(relationshipmanager_name )+ "  <br />";
                            body = body + "<br />";
                            body = body + "&nbsp &nbsp <b> Cluster Head Name : </b> " + HttpUtility.HtmlEncode(cluster_head )+ "  <br />";
                            body = body + "<br />";
                            body = body + "&nbsp &nbsp <b> Zonal Head Name : </b> " + HttpUtility.HtmlEncode(zonal_head)+ "  <br />";
                            body = body + "<br />";
                            body = body + "&nbsp&nbsp <b>   Regional Head Name:</b> " + HttpUtility.HtmlEncode(regionalhead_name) + "<br />";
                            body = body + "<br />";
                            body = body + "&nbsp&nbsp <b>   Product:</b> " + ls_Product + "<br />";
                            body = body + "<br />";
                            body = body + "&nbsp&nbsp <b>   Program:</b> " + ls_Program + "<br />";
                            body = body + "<br />";
                            body = body + "&nbsp&nbsp <b>   Overall Limit Amount:</b> " + HttpUtility.HtmlEncode(lsoveralllimit_amount )+ "<br />";
                            body = body + "<br />";
                            body = body + "&nbsp&nbsp <b>   Margin:</b> " + HttpUtility.HtmlEncode(ls_Margin )+ "<br />";
                            body = body + "<br />";
                            body = body + "&nbsp &nbsp <b> Final Approval Time :</b>  " + finalapproved_time + "  <br />";
                            body = body + "<br />";
                            body = body + "&nbsp &nbsp Log into " + ConfigurationManager.AppSettings["livedomain_url"].ToString() + " and complete the necessary actions.";
                            body = body + "<br />";

                            body = body + "</td><td style='margin-left:20px; border-left-color:white;'>&nbsp&nbsp</td></tr></table>";


                            cc_mailid = "";
                            MailMessage message = new MailMessage();
                            SmtpClient smtp = new SmtpClient();
                            message.From = new MailAddress(ls_username);
                            //message.To.Add(new MailAddress(tomail_id));
                            string[] lstoReceipients;
                            if (tomail_id != null & tomail_id != string.Empty & tomail_id != "")
                            {
                                lstoReceipients = tomail_id.Split(',');
                                if (tomail_id.Length == 0)
                                {
                                    message.To.Add(new MailAddress(tomail_id));
                                }
                                else
                                {
                                    foreach (string tomail_id in lstoReceipients)
                                    {
                                        message.To.Add(new MailAddress(tomail_id));
                                    }
                                }
                            }
                            cc_mailid = "" + relationshipmanager_mailid + "";

                            if (cc_mailid != null & cc_mailid != string.Empty & cc_mailid != "")
                            {
                                lsCCReceipients = cc_mailid.Split(',');
                                if (cc_mailid.Length == 0)
                                {
                                    message.CC.Add(new MailAddress(cc_mailid));
                                }
                                else
                                {
                                    foreach (string CCEmail in lsCCReceipients)
                                    {
                                        message.CC.Add(new MailAddress(CCEmail)); //Adding Multiple CC email Id
                                    }
                                }
                            }
                            lsBccmail_id = ConfigurationManager.AppSettings["SamagroApprovalBccMail"].ToString();

                            if (lsBccmail_id != null & lsBccmail_id != string.Empty & lsBccmail_id != "")
                            {
                                lsBCCReceipients = lsBccmail_id.Split(',');
                                if (lsBccmail_id.Length == 0)
                                {
                                    message.Bcc.Add(new MailAddress(lsBccmail_id));
                                }
                                else
                                {
                                    foreach (string BCCEmail in lsBCCReceipients)
                                    {
                                        message.Bcc.Add(new MailAddress(BCCEmail)); //Adding Multiple BCC email Id
                                    }
                                }
                            }


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

                        }

                        else if (!String.IsNullOrEmpty(lsproductdesk_gid) && lsproductdesk_flag == "Y")
                        {

                            msSQL = "select application_no from agr_mst_tapplication where application_gid='" + values.application_gid + "'";
                            application_no = objdbconn.GetExecuteScalar(msSQL);
                            msSQL = "select customerref_name from agr_mst_tapplication where application_gid='" + values.application_gid + "'";
                            customer_name = objdbconn.GetExecuteScalar(msSQL);
                            msSQL = "select date_format(a.approved_date, '%d-%m-%Y %h:%i %p') as approved_date from agr_trn_tapplicationapproval a left join agr_mst_tapplication b on b.application_gid = a.application_gid and hierary_level = '5' where b.application_gid='" + values.application_gid + "'";
                            finalapproved_time = objdbconn.GetExecuteScalar(msSQL);
                            msSQL = " select clustermanager_name,zonalhead_name,relationshipmanager_name from agr_mst_tapplication where application_gid = '" + values.application_gid + "'";
                            objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader1.HasRows == true)
                            {
                                cluster_head = objODBCDatareader1["clustermanager_name"].ToString();
                                zonal_head = objODBCDatareader1["zonalhead_name"].ToString();
                                relationshipmanager_name = objODBCDatareader1["relationshipmanager_name"].ToString();
                            }
                            objODBCDatareader1.Close();
                            msSQL = "select group_concat(c.employee_emailid) from agr_mst_tapplication a left join ocs_mst_tcredit2nationalmanager b on a.creditgroup_gid = b.creditmapping_gid left join hrm_mst_temployee c on c.employee_gid = b.employee_gid where a.application_gid = '" + values.application_gid + "'";
                            creditnationalmanager_mailid = objdbconn.GetExecuteScalar(msSQL);

                            msSQL = "select group_concat(c.employee_emailid) from agr_mst_tproductdeskmapping_member a left join hrm_mst_temployee c on c.employee_gid = a.employee_gid  where productdesk_gid='" + lsproductdesk_gid + "'";
                            tomail_id = objdbconn.GetExecuteScalar(msSQL);

                            msSQL = "select group_concat(c.employee_emailid) from agr_mst_tapplication a left join ocs_mst_tcreditr2regionalmanager b on a.creditgroup_gid = b.creditmapping_gid left join hrm_mst_temployee c on c.employee_gid = b.employee_gid where a.application_gid = '" + values.application_gid + "'";
                            creditregionalmanager_mailid = objdbconn.GetExecuteScalar(msSQL);

                            //tomail_id = "" + creditnationalmanager_mailid + "," + creditregionalmanager_mailid + "";

                            msSQL = " select b.employee_emailid from agr_mst_tapplication a left join hrm_mst_temployee b on b.employee_gid = a.relationshipmanager_gid where a.application_gid='" + values.application_gid + "'";
                            relationshipmanager_mailid = objdbconn.GetExecuteScalar(msSQL);

                            msSQL = " select  concat( c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code)  from agr_mst_tapplication a left join hrm_mst_temployee b on b.employee_gid = a.regionalhead_gid left join adm_mst_tuser c on c.user_gid = b.user_gid where a.application_gid='" + values.application_gid + "'";
                            regionalhead_name = objdbconn.GetExecuteScalar(msSQL);

                            msSQL = " select  a.overalllimit_amount  from agr_mst_tapplication a  where a.application_gid='" + values.application_gid + "'";
                            lsoveralllimit_amount = objdbconn.GetExecuteScalar(msSQL);

                            msSQL = " select  productdesk_name from agr_mst_tproductdeskmapping   where productdesk_gid='" + lsproductdesk_gid + "'";
                            lsproductdesk_name = objdbconn.GetExecuteScalar(msSQL);


                            msSQL = " select  group_concat(product_type) as Product, group_concat(productsub_type) as Program, group_concat(rate_interest) as Margin from agr_mst_tapplication2loan  where application_gid = '" + values.application_gid + "'";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                ls_Product = objODBCDatareader["Product"].ToString();
                                ls_Program = objODBCDatareader["Program"].ToString();
                                ls_Margin = objODBCDatareader["Margin"].ToString();
                            }
                            objODBCDatareader.Close();

                            msSQL = " SELECT pop_server, pop_port, pop_username, pop_password" +
                                    " FROM adm_mst_tcompany";
                            objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader1.HasRows == true)
                            {
                                ls_server = objODBCDatareader1["pop_server"].ToString();
                                ls_port = Convert.ToInt32(objODBCDatareader1["pop_port"]);
                                ls_username = objODBCDatareader1["pop_username"].ToString();
                                ls_password = objODBCDatareader1["pop_password"].ToString();
                            }
                            objODBCDatareader1.Close();
                            sub = " Application allocation (Product Desk): " + HttpUtility.HtmlEncode(customer_name)+ " ";
                            body = "<style>table, th, td {border: 1px solid black;border-collapse: collapse;}</style>";
                            body = body + "<table style='border-right: 1px solid black;border-top: 1px solid black;border-bottom: 1px solid black;'><tr><td style='border-right-color:white;'>";

                            body = body + " &nbsp &nbsp Dear Sir / Madam, <br />";
                            body = body + "<br />";
                            body = body + "&nbsp &nbsp Greetings! <br />";
                            body = body + "<br />";
                            body = body + "&nbsp &nbsp The below application has been submitted, please allocate it to a member of product desk for assessment <br />";
                            body = body + "<br />";
                            body = body + "&nbsp &nbsp <b> Application Number : </b> " + application_no + "  <br />";
                            body = body + "<br />";
                            body = body + "&nbsp &nbsp <b> Customer Name : </b> " + HttpUtility.HtmlEncode(customer_name)+ "  <br />";
                            body = body + "<br />";
                            body = body + "&nbsp &nbsp <b> RM Name : </b> " + HttpUtility.HtmlEncode(relationshipmanager_name )+ "  <br />";
                            body = body + "<br />";
                            body = body + "&nbsp &nbsp <b> Product:</b> " + HttpUtility.HtmlEncode(ls_Product)+ "<br />";
                            body = body + "<br />";
                            body = body + "&nbsp &nbsp <b> Program:</b> " + ls_Program + "<br />";
                            body = body + "<br />";
                            body = body + "&nbsp &nbsp <b> Cluster Head Name : </b> " + HttpUtility.HtmlEncode(cluster_head )+ "  <br />";
                            body = body + "<br />";
                            body = body + "&nbsp &nbsp <b> Regional Head Name:</b> " + HttpUtility.HtmlEncode(regionalhead_name)+ "<br />";
                            body = body + "<br />";
                            body = body + "&nbsp &nbsp <b> Zonal Head Name : </b> " + HttpUtility.HtmlEncode(zonal_head)+ "  <br />";
                            body = body + "<br />";
                            body = body + "&nbsp &nbsp <b> Final Approval Time :</b>  " + finalapproved_time + "  <br />";
                            body = body + "<br />";
                            body = body + "&nbsp &nbsp Log into " + ConfigurationManager.AppSettings["livedomain_url"].ToString() + " and complete the necessary actions.";
                            body = body + "<br />";
                            body = body + "<br />";
                            body = body + "&nbsp &nbsp **This is an automated e-mail. Please do not reply to this mailbox**";
                            body = body + "<br />";
                            body = body + "<br />";
                            body = body + "</td><td style='margin-left:20px; border-left-color:white;'>&nbsp&nbsp</td></tr></table>";


                            cc_mailid = "";
                            MailMessage message = new MailMessage();
                            SmtpClient smtp = new SmtpClient();
                            message.From = new MailAddress(ls_username);
                            //message.To.Add(new MailAddress(tomail_id));
                            string[] lstoReceipients;
                            if (tomail_id != null & tomail_id != string.Empty & tomail_id != "")
                            {
                                lstoReceipients = tomail_id.Split(',');
                                if (tomail_id.Length == 0)
                                {
                                    message.To.Add(new MailAddress(tomail_id));
                                }
                                else
                                {
                                    foreach (string tomail_id in lstoReceipients)
                                    {
                                        message.To.Add(new MailAddress(tomail_id));
                                    }
                                }
                            }
                            cc_mailid = ConfigurationManager.AppSettings["Samagrostf"].ToString();

                            if (cc_mailid != null & cc_mailid != string.Empty & cc_mailid != "")
                            {
                                lsCCReceipients = cc_mailid.Split(',');
                                if (cc_mailid.Length == 0)
                                {
                                    message.CC.Add(new MailAddress(cc_mailid));
                                }
                                else
                                {
                                    foreach (string CCEmail in lsCCReceipients)
                                    {
                                        message.CC.Add(new MailAddress(CCEmail)); //Adding Multiple CC email Id
                                    }
                                }
                            }
                            lsBccmail_id = ConfigurationManager.AppSettings["Samagrostfbcc"].ToString();

                            if (lsBccmail_id != null & lsBccmail_id != string.Empty & lsBccmail_id != "")
                            {
                                lsBCCReceipients = lsBccmail_id.Split(',');
                                if (lsBccmail_id.Length == 0)
                                {
                                    message.Bcc.Add(new MailAddress(lsBccmail_id));
                                }
                                else
                                {
                                    foreach (string BCCEmail in lsBCCReceipients)
                                    {
                                        message.Bcc.Add(new MailAddress(BCCEmail)); //Adding Multiple BCC email Id
                                    }
                                }
                            }


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

                    }
                    catch (Exception ex)
                    {
                        values.message = ex.ToString();
                        values.status = false;

                    }
                }
            }

            if (nexthierlevel == '\u0005')
            {

            }
            else
            {
                if (values.approval_status == "Approved")
                {
                    msSQL = " select applicationapproval_gid,hierary_level from agr_trn_tapplicationapproval " +
                            " where application_gid='" + values.application_gid + "' and approval_gid='" + employee_gid + "' and " +
                            " applicationapproval_gid >'" + values.applicationapproval_gid + "' order by hierary_level asc";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable.Rows.Count != 0)
                    {
                        int levelcheck = level;
                        char charlevel = Convert.ToChar(levelcheck);
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            levelcheck = levelcheck + 1;
                            string lcheck = dt["hierary_level"].ToString();
                            int nowlevel = Convert.ToInt16(lcheck);
                            charlevel = Convert.ToChar(levelcheck);
                            if (levelcheck == nowlevel)
                            {
                                msSQL = " update agr_trn_tapplicationapproval set  approval_status='Approved',approval_remarks='" + values.approval_remarks.Replace("'", "") + "',initiate_flag='Y'," +
                                       " approved_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                                       " where applicationapproval_gid='" + dt["applicationapproval_gid"] + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                msSQL = "update agr_mst_tapplication set  headapproval_status='L" + nowlevel + " - Approved',headapproval_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                                       " where application_gid='" + values.application_gid + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                string lslevel;
                                char hierarchy;
                                lslevel = dt["hierary_level"].ToString();
                                int lshierlevel = Convert.ToInt16(lslevel);
                                hierarchy = Convert.ToChar(lshierlevel);
                                if (hierarchy == '\u0004')
                                {
                                    msSQL = "select applicationapproval_gid from agr_trn_tapplicationapproval where application_gid='" + values.application_gid + "' and  hierary_level = '5'";
                                    lsapplicationapproval_gid = objdbconn.GetExecuteScalar(msSQL);

                                    msSQL = " update agr_trn_tapplicationapproval set  approval_status='Approved',approval_remarks='Auto approved in backend',initiate_flag='Y'," +
                                            " approved_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                                            " where applicationapproval_gid='" + lsapplicationapproval_gid + "'";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                    msSQL = "update agr_mst_tapplication set  approval_flag='Y', approval_status='Submitted to Underwriting', headapproval_status='All Heads Approved'" +
                                               " where application_gid='" + values.application_gid + "'";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                    msSQL = "select applicationapproval_gid from agr_trn_tapplicationapproval where application_gid='" + values.application_gid + "' and approval_status='Pending' ";
                                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                    if (objODBCDatareader.HasRows == false)
                                    {
                                        string lsproductdesk_gid = "";
                                        msSQL = " select productdesk_gid from agr_mst_tproductdeskmapping where products_gid in (select producttype_gid from agr_mst_tapplication2loan	" +
                                                " where application_gid='" + values.application_gid + "') and app_productdesk='Y' and productdesk_status='Y'";
                                        lsproductdesk_gid = objdbconn.GetExecuteScalar(msSQL);
                                        if (lsproductdesk_gid != "")
                                        {
                                            msSQL = " update agr_mst_tapplication set productdesk_flag='Y',approval_flag='Y', " +
                                                    " approval_status='Submitted to Product Desk', headapproval_status='All Heads Approved', productdesk_gid ='" + lsproductdesk_gid + "'" +
                                                    " where application_gid='" + values.application_gid + "'";
                                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL); 
                                        }
                                        else
                                        {  
                                            msSQL = " update agr_mst_tapplication set  approval_flag='Y', approval_status='Submitted to Underwriting', headapproval_status='All Heads Approved'" +
                                                    " where application_gid='" + values.application_gid + "'";
                                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL); 
                                            DaAgrTrnProductApproval objapproval = new DaAgrTrnProductApproval();
                                            objapproval.FnAutoProductApprovalFlow(values.application_gid, employee_gid, user_gid);
                                        } 
                                        if (mnResult != 0)
                                        {
                                            try
                                            {
                                                msSQL = "select application_no from agr_mst_tapplication where application_gid='" + values.application_gid + "'";
                                                application_no = objdbconn.GetExecuteScalar(msSQL);
                                                msSQL = "select customerref_name from agr_mst_tapplication where application_gid='" + values.application_gid + "'";
                                                customer_name = objdbconn.GetExecuteScalar(msSQL);
                                                msSQL = "select approved_date from agr_trn_tapplicationapproval a left join agr_mst_tapplication b on b.application_gid = a.application_gid and hierary_level = '5' where b.application_gid='" + values.application_gid + "'";
                                                finalapproved_time = objdbconn.GetExecuteScalar(msSQL);
                                                msSQL = " select clustermanager_name,zonalhead_name,relationshipmanager_name from agr_mst_tapplication where application_gid = '" + values.application_gid + "'";
                                                objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                                                if (objODBCDatareader1.HasRows == true)
                                                {
                                                    cluster_head = objODBCDatareader1["clustermanager_name"].ToString();
                                                    zonal_head = objODBCDatareader1["zonalhead_name"].ToString();
                                                    relationshipmanager_name = objODBCDatareader1["relationshipmanager_name"].ToString();
                                                }
                                                objODBCDatareader1.Close();
                                                msSQL = "select group_concat(c.employee_emailid) from agr_mst_tapplication a left join ocs_mst_tcredit2nationalmanager b on a.creditgroup_gid = b.creditmapping_gid left join hrm_mst_temployee c on c.employee_gid = b.employee_gid where a.application_gid = '" + values.application_gid + "'";
                                                creditnationalmanager_mailid = objdbconn.GetExecuteScalar(msSQL);

                                                msSQL = "select group_concat(c.employee_emailid) from agr_mst_tapplication a left join ocs_mst_tcreditr2regionalmanager b on a.creditgroup_gid = b.creditmapping_gid left join hrm_mst_temployee c on c.employee_gid = b.employee_gid where a.application_gid = '" + values.application_gid + "'";
                                                creditregionalmanager_mailid = objdbconn.GetExecuteScalar(msSQL);
                                                
                                                tomail_id = "" + creditnationalmanager_mailid + "";

                                                msSQL = " select b.employee_emailid from agr_mst_tapplication a left join hrm_mst_temployee b on b.employee_gid = a.relationshipmanager_gid where a.application_gid='" + values.application_gid + "'";
                                                relationshipmanager_mailid = objdbconn.GetExecuteScalar(msSQL);

                                                msSQL = " select  concat( c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code)  from agr_mst_tapplication a left join hrm_mst_temployee b on b.employee_gid = a.regionalhead_gid left join adm_mst_tuser c on c.user_gid = b.user_gid where a.application_gid='" + values.application_gid + "'";
                                                regionalhead_name = objdbconn.GetExecuteScalar(msSQL);

                                                msSQL = " select  a.overalllimit_amount  from agr_mst_tapplication a  where a.application_gid='" + values.application_gid + "'";
                                                lsoveralllimit_amount = objdbconn.GetExecuteScalar(msSQL);

                                                msSQL = " select  group_concat(product_type) as Product, group_concat(productsub_type) as Program, group_concat(rate_interest) as Margin from agr_mst_tapplication2loan  where application_gid = '" + values.application_gid + "'";
                                                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                                if (objODBCDatareader.HasRows == true)
                                                {
                                                    ls_Product = objODBCDatareader["Product"].ToString();
                                                    ls_Program = objODBCDatareader["Program"].ToString();
                                                    ls_Margin = objODBCDatareader["Margin"].ToString();
                                                }
                                                objODBCDatareader.Close();

                                                msSQL = " SELECT pop_server, pop_port, pop_username, pop_password" +
                                                        " FROM adm_mst_tcompany";
                                                objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                                                if (objODBCDatareader1.HasRows == true)
                                                {
                                                    ls_server = objODBCDatareader1["pop_server"].ToString();
                                                    ls_port = Convert.ToInt32(objODBCDatareader1["pop_port"]);
                                                    ls_username = objODBCDatareader1["pop_username"].ToString();
                                                    ls_password = objODBCDatareader1["pop_password"].ToString();
                                                }
                                                //lssource = ConfigurationManager.AppSettings["img_path"];
                                                objODBCDatareader1.Close();
                                                sub = " Application allocation : " + application_no + " ";
                                                body = "<style>table, th, td {border: 1px solid black;border-collapse: collapse;}</style>";
                                                body = body + "<table style='border-right: 1px solid black;border-top: 1px solid black;border-bottom: 1px solid black;'><tr><td style='border-right-color:white;align:center;'>";
                                                //body = body + "<br />";
                                                //body = body + "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp <img style='height:150px; width:380px;' src='" + lssource + "'><br />";
                                                //body = body + "<br />";
                                                body = body + " &nbsp &nbsp Dear Sir / Madam, <br />";
                                                body = body + "<br />";
                                                body = body + "&nbsp &nbsp Greetings! <br />";
                                                body = body + "<br />";
                                                body = body + "&nbsp &nbsp The below application has been submitted, please allocate it to a credit manager <br />";
                                                body = body + "<br />";
                                                body = body + "&nbsp &nbsp <b> Application Number : </b> " + application_no + "  <br />";
                                                body = body + "<br />";
                                                body = body + "&nbsp &nbsp <b>  Customer Name : </b> " + HttpUtility.HtmlEncode(customer_name )+ "  <br />";
                                                body = body + "<br />";
                                                body = body + "&nbsp &nbsp <b>  RM Name : </b> " + HttpUtility.HtmlEncode(relationshipmanager_name )+ "  <br />";
                                                body = body + "<br />";
                                                body = body + "&nbsp &nbsp <b>  Cluster Head Name : </b> " + HttpUtility.HtmlEncode(cluster_head) + "  <br />";
                                                body = body + "<br />";
                                                body = body + "&nbsp &nbsp <b>  Zonal Head Name : </b> " + HttpUtility.HtmlEncode(zonal_head )+ "  <br />";
                                                body = body + "<br />";
                                                body = body + "&nbsp&nbsp <b>Regional Head Name:</b> " + HttpUtility.HtmlEncode(regionalhead_name)+ "<br />";
                                                body = body + "<br />";
                                                body = body + "&nbsp&nbsp <b>Product:</b> " + ls_Product + "<br />";
                                                body = body + "<br />";
                                                body = body + "&nbsp&nbsp <b>Program:</b> " + ls_Program + "<br />";
                                                body = body + "<br />";
                                                body = body + "&nbsp&nbsp <b>Overall Limit Amount:</b> " + HttpUtility.HtmlEncode(lsoveralllimit_amount )+ "<br />";
                                                body = body + "<br />";
                                                body = body + "&nbsp&nbsp <b>Margin:</b> " + HttpUtility.HtmlEncode(ls_Margin)+ "<br />";
                                                body = body + "<br />";
                                                body = body + "&nbsp &nbsp <b>  Final Approval Time : </b> " + finalapproved_time + "  <br />";
                                                body = body + "<br />";
                                                body = body + "&nbsp &nbsp Log into " + ConfigurationManager.AppSettings["livedomain_url"].ToString() + " and complete the necessary actions.";
                                                body = body + "<br />";
                                                //body = body + "&nbsp &nbsp Regards";
                                                //body = body + "<br />";
                                                //body = body + "&nbsp &nbsp Sam-Custopedia <br /> ";
                                                //body = body + "<br />";
                                                //body = body + "&nbsp &nbsp<hr>&nbsp&nbsp";
                                                //body = body + "&nbsp &nbsp Reach out to us at samcustopedia@samunnati.com <br /> ";
                                                //body = body + "<br />";
                                                body = body + "</td><td style='margin-left:20px; border-left-color:white;'>&nbsp&nbsp</td></tr></table>";


                                                cc_mailid = "";
                                                MailMessage message = new MailMessage();
                                                SmtpClient smtp = new SmtpClient();
                                                message.From = new MailAddress(ls_username);
                                                //message.To.Add(new MailAddress(tomail_id));
                                                string[] lstoReceipients;
                                                if (tomail_id != null & tomail_id != string.Empty & tomail_id != "")
                                                {
                                                    lstoReceipients = tomail_id.Split(',');
                                                    if (tomail_id.Length == 0)
                                                    {
                                                        message.To.Add(new MailAddress(tomail_id));
                                                    }
                                                    else
                                                    {
                                                        foreach (string tomail_id in lstoReceipients)
                                                        {
                                                            message.To.Add(new MailAddress(tomail_id));
                                                        }
                                                    }
                                                }
                                                cc_mailid = "" + relationshipmanager_mailid + "";

                                                if (cc_mailid != null & cc_mailid != string.Empty & cc_mailid != "")
                                                {
                                                    lsCCReceipients = cc_mailid.Split(',');
                                                    if (cc_mailid.Length == 0)
                                                    {
                                                        message.CC.Add(new MailAddress(cc_mailid));
                                                    }
                                                    else
                                                    {
                                                        foreach (string CCEmail in lsCCReceipients)
                                                        {
                                                            message.CC.Add(new MailAddress(CCEmail)); //Adding Multiple CC email Id
                                                        }
                                                    }
                                                }
                                                lsBccmail_id = ConfigurationManager.AppSettings["SamagroApprovalBccMail"].ToString();

                                                if (lsBccmail_id != null & lsBccmail_id != string.Empty & lsBccmail_id != "")
                                                {
                                                    lsBCCReceipients = lsBccmail_id.Split(',');
                                                    if (lsBccmail_id.Length == 0)
                                                    {
                                                        message.Bcc.Add(new MailAddress(lsBccmail_id));
                                                    }
                                                    else
                                                    {
                                                        foreach (string BCCEmail in lsBCCReceipients)
                                                        {
                                                            message.Bcc.Add(new MailAddress(BCCEmail)); //Adding Multiple BCC email Id
                                                        }
                                                    }
                                                }


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
                                            }
                                            catch (Exception ex)
                                            {
                                                values.message = ex.ToString();
                                                values.status = false;

                                            }
                                        }
                                    }
                                    objODBCDatareader.Close();

                                }
                                msSQL = "select applicationapproval_gid,hierary_level from agr_trn_tapplicationapproval where application_gid='" + values.application_gid + "' and ( hierary_level <'" + dt["hierary_level"].ToString() + "' or hierary_level >'" + dt["hierary_level"].ToString() + "') and initiate_flag='N' and approval_status='Pending'  order by hierary_level asc";
                                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                if (objODBCDatareader.HasRows == true)
                                {
                                    msSQL = "update agr_trn_tapplicationapproval set initiate_flag='Y'" +
                                           " where applicationapproval_gid='" + objODBCDatareader["applicationapproval_gid"].ToString() + "'";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                }
                                objODBCDatareader.Close();

                            }
                            else
                            {
                                msSQL = " select applicationapproval_gid,hierary_level from agr_trn_tapplicationapproval " +
                                        " where application_gid='" + values.application_gid + "' and approval_gid='" + employee_gid + "' and " +
                                        " applicationapproval_gid <>'" + values.applicationapproval_gid + "' and hierary_level = '" + nextlevel + "' order by hierary_level asc";
                                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                if (objODBCDatareader.HasRows == false)
                                {
                                    objODBCDatareader.Close();

                                    msSQL = "select applicationapproval_gid from agr_trn_tapplicationapproval " +
                                            "where  application_gid='" + values.application_gid + "' and hierary_level >'" + level + "' and initiate_flag='N' order by hierary_level asc";
                                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                    if (objODBCDatareader.HasRows == true)
                                    {

                                        msSQL = "update agr_trn_tapplicationapproval set initiate_flag='Y'" +
                                                " where applicationapproval_gid='" + objODBCDatareader["applicationapproval_gid"] + "'";
                                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                    }
                                    objODBCDatareader.Close();
                                    dt_datatable.Dispose();
                                    values.status = true;
                                    values.message = "Application " + values.approval_status + " Successfully..!";
                                    return true;
                                }
                                else
                                {
                                    objODBCDatareader.Close();
                                }

                            }
                            msSQL = " select approval_gid from agr_trn_tapplicationapproval where application_gid = '" + values.application_gid + "' and hierary_level ='" + levelcheck + "' and approval_status='Approved'";
                            objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader1.HasRows == false)
                            {
                                int llevel = levelcheck - 1;
                                charlevel = Convert.ToChar(llevel);
                            }
                            objODBCDatareader1.Close();
                            if (charlevel == '\u0005')
                            {
                                int llevel = levelcheck - 1;
                                charlevel = Convert.ToChar(llevel);
                            }

                        }

                        try
                        {
                            msSQL = "select application_no from agr_mst_tapplication where application_gid='" + values.application_gid + "'";
                            application_no = objdbconn.GetExecuteScalar(msSQL);
                            msSQL = "select customerref_name from agr_mst_tapplication where application_gid='" + values.application_gid + "'";
                            customer_name = objdbconn.GetExecuteScalar(msSQL);
                            msSQL = " select clustermanager_name,zonalhead_name,regionalhead_name,businesshead_name,clustermanager_gid, zonalhead_gid,regionalhead_gid,businesshead_gid from agr_mst_tapplication where application_gid = '" + values.application_gid + "'";
                            objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader1.HasRows == true)
                            {
                                cluster_head_name = objODBCDatareader1["clustermanager_name"].ToString();
                                zonal_head = objODBCDatareader1["zonalhead_name"].ToString();
                                regional_head_name = objODBCDatareader1["regionalhead_name"].ToString();
                                business_head_name = objODBCDatareader1["businesshead_name"].ToString();
                                cluster_head_gid = objODBCDatareader1["clustermanager_gid"].ToString();
                                zonal_head_gid = objODBCDatareader1["zonalhead_gid"].ToString();
                                regional_head_gid = objODBCDatareader1["regionalhead_gid"].ToString();
                                business_head_gid = objODBCDatareader1["businesshead_gid"].ToString();
                            }
                            objODBCDatareader1.Close();
                            msSQL = " select approval_gid,approval_name from agr_trn_tapplicationapproval where application_gid = '" + values.application_gid + "' and hierary_level ='1'";
                            objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader1.HasRows == true)
                            {
                                reportingto_gid = objODBCDatareader1["approval_gid"].ToString();
                                reportingto_name = objODBCDatareader1["approval_name"].ToString();
                            }
                            objODBCDatareader1.Close();

                            msSQL = "select b.employee_emailid from agr_mst_tapplication a left join hrm_mst_temployee b on b.employee_gid = a.clustermanager_gid where application_gid='" + values.application_gid + "'";
                            cluster_head_mailid = objdbconn.GetExecuteScalar(msSQL);
                            msSQL = " select b.employee_emailid from agr_mst_tapplication a left join hrm_mst_temployee b on b.employee_gid = a.zonalhead_gid where a.application_gid='" + values.application_gid + "'";
                            zonalhead_mailid = objdbconn.GetExecuteScalar(msSQL);
                            msSQL = " select b.employee_emailid from agr_mst_tapplication a left join hrm_mst_temployee b on b.employee_gid = a.regionalhead_gid where a.application_gid='" + values.application_gid + "'";
                            regional_head_mailid = objdbconn.GetExecuteScalar(msSQL);
                            msSQL = " select b.employee_emailid from agr_mst_tapplication a left join hrm_mst_temployee b on b.employee_gid = a.businesshead_gid where a.application_gid='" + values.application_gid + "'";
                            business_head_mailid = objdbconn.GetExecuteScalar(msSQL);
                            msSQL = " select b.employee_emailid from agr_trn_tapplicationapproval a left join hrm_mst_temployee b on b.employee_gid = a.approval_gid where a.application_gid='" + values.application_gid + "'  and hierary_level ='1'";
                            reportingto_mailid = objdbconn.GetExecuteScalar(msSQL);

                            msSQL = " select b.employee_emailid from agr_mst_tapplication a left join hrm_mst_temployee b on b.employee_gid = a.created_by where a.application_gid='" + values.application_gid + "'";
                            creater_mailid = objdbconn.GetExecuteScalar(msSQL);
                            msSQL = " select concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by  from agr_mst_tapplication a" +
                                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                                    " left join adm_mst_tuser c on c.user_gid=b.user_gid  where a.created_by = '" + values.created_by + "'";
                            creater_name = objdbconn.GetExecuteScalar(msSQL);

                            msSQL = "select  concat( c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code)  from agr_mst_tapplication a left join hrm_mst_temployee b on b.employee_gid = a.clustermanager_gid join adm_mst_tuser c on c.user_gid = b.user_gid where application_gid='" + values.application_gid + "'";
                            cluster_head = objdbconn.GetExecuteScalar(msSQL);
                            msSQL = " select  concat( c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code)  from agr_mst_tapplication a left join hrm_mst_temployee b on b.employee_gid = a.zonalhead_gid left join adm_mst_tuser c on c.user_gid = b.user_gid where a.application_gid='" + values.application_gid + "'";
                            zonal_head = objdbconn.GetExecuteScalar(msSQL);
                            msSQL = " select  concat( c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code)  from agr_mst_tapplication a left join hrm_mst_temployee b on b.employee_gid = a.relationshipmanager_gid left join adm_mst_tuser c on c.user_gid = b.user_gid where a.application_gid='" + values.application_gid + "'";
                            rm_name = objdbconn.GetExecuteScalar(msSQL);
                            msSQL = " select  concat( c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code)  from agr_mst_tapplication a left join hrm_mst_temployee b on b.employee_gid = a.regionalhead_gid left join adm_mst_tuser c on c.user_gid = b.user_gid where a.application_gid='" + values.application_gid + "'";
                            regionalhead_name = objdbconn.GetExecuteScalar(msSQL);

                            msSQL = " select  a.overalllimit_amount  from agr_mst_tapplication a  where a.application_gid='" + values.application_gid + "'";
                            lsoveralllimit_amount = objdbconn.GetExecuteScalar(msSQL);

                            msSQL = " select  group_concat(product_type) as Product, group_concat(productsub_type) as Program, group_concat(rate_interest) as Margin from agr_mst_tapplication2loan  where application_gid = '" + values.application_gid + "'";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                ls_Product = objODBCDatareader["Product"].ToString();
                                ls_Program = objODBCDatareader["Program"].ToString();
                                ls_Margin = objODBCDatareader["Margin"].ToString();
                            }
                            objODBCDatareader.Close();

                            //if (charlevel == '\u0005')
                            //{
                            //    tomail_id = creater_mailid;
                            //    cc_mailid = "" + reportingto_mailid + "," + regional_head_mailid + "," + cluster_head_mailid + "," + zonalhead_mailid + "";
                            //    content = "L5 Approved";
                            //    head_name = creater_name;
                            //}
                            //else if (charlevel == '\u0004')
                            //{
                            //    tomail_id = business_head_mailid;
                            //    cc_mailid = "" + reportingto_mailid + "," + cluster_head_mailid + "," + regional_head_mailid + "," + creater_mailid + "";
                            //    content = "Pending L5 Approval";
                            //    head_name = business_head_name;
                            //}
                            if (charlevel == '\u0004')
                            {
                                tomail_id = creater_mailid;
                                cc_mailid = "" + reportingto_mailid + "," + regional_head_mailid + "," + cluster_head_mailid + "," + zonalhead_mailid + "";
                                content = "L5 Approved";
                                head_name = creater_name;
                            }
                            else if (charlevel == '\u0003')
                            {
                                tomail_id = zonalhead_mailid;
                                cc_mailid = "" + reportingto_mailid + "," + creater_mailid + "," + cluster_head_mailid + "";
                                content = "Pending L4 Approval";
                                head_name = zonal_head;
                            }
                            else if (charlevel == '\u0002')
                            {
                                tomail_id = regional_head_mailid;
                                cc_mailid = "" + reportingto_mailid + "," + zonalhead_mailid + "," + creater_mailid + "";
                                content = "Pending L3 Approval";
                                head_name = regional_head_name;
                            }
                            else if (charlevel == '\u0001')
                            {
                                tomail_id = cluster_head_mailid;
                                cc_mailid = "" + reportingto_mailid + "," + creater_mailid + "," + zonalhead_mailid + "";
                                content = "Pending L2 Approval";
                                head_name = cluster_head_name;
                            }


                            msSQL = " SELECT pop_server, pop_port, pop_username, pop_password" +
                                " FROM adm_mst_tcompany";
                            objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader1.HasRows == true)
                            {
                                ls_server = objODBCDatareader1["pop_server"].ToString();
                                ls_port = Convert.ToInt32(objODBCDatareader1["pop_port"]);
                                ls_username = objODBCDatareader1["pop_username"].ToString();
                                ls_password = objODBCDatareader1["pop_password"].ToString();
                            }
                            //lssource = ConfigurationManager.AppSettings["img_path"];
                            objODBCDatareader1.Close();
                            if (charlevel == '\u0004')
                            {

                            }
                            else
                            {
                                sub = " ARN(" + application_no + ") : Application approval required ";
                                body = "<style>table, th, td {border: 1px solid black;border-collapse: collapse;}</style>";
                                body = body + "<table style='border-right: 1px solid black;border-top: 1px solid black;border-bottom: 1px solid black;'><tr><td style='border-right-color:white;align:center;'>";
                                //body = body + "<br />";
                                //body = body + "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp <img style='height:150px; width:380px;' src='" + lssource + "'><br />";
                                //body = body + "<br />";
                                body = body + " &nbsp&nbsp Dear Sir/Madam,<br />";
                                body = body + "<br />";
                                body = body + "&nbsp&nbsp Greetings<br />";
                                body = body + "<br />";
                                body = body + "&nbsp&nbsp The below application has been submitted, please validate and approve to proceed for underwriting.<br />";
                                body = body + "<br />";
                                body = body + "&nbsp&nbsp <b>Application Number:</b> " + application_no + "<br /><br />";
                                body = body + "&nbsp&nbsp <b>Customer Name:</b> " + HttpUtility.HtmlEncode(customer_name)+ "<br /><br />";
                                body = body + "&nbsp&nbsp <b>RM Name:</b>" + HttpUtility.HtmlEncode(rm_name)+ "<br /><br />";
                                body = body + "&nbsp&nbsp <b>Cluster Head Name</b>:" + HttpUtility.HtmlEncode(cluster_head)+ "<br /><br />";
                                body = body + "&nbsp&nbsp <b>Zonal Head Name:</b>" + HttpUtility.HtmlEncode(zonal_head)+ "<br /><br />";
                                body = body + "&nbsp&nbsp <b>Regional Head Name:</b> " + HttpUtility.HtmlEncode(regionalhead_name)+ "<br /><br />";
                                body = body + "&nbsp&nbsp <b>Product:</b> " + ls_Product + "<br /><br />";
                                body = body + "&nbsp&nbsp <b>Program:</b> " + ls_Program + "<br /><br />";
                                body = body + "&nbsp&nbsp <b>Overall Limit Amount:</b> " + HttpUtility.HtmlEncode(lsoveralllimit_amount)+ "<br /><br />";
                                body = body + "&nbsp&nbsp <b>Margin:</b> " + HttpUtility.HtmlEncode(ls_Margin)+ "<br /><br />";
                                body = body + "&nbsp&nbsp <b>Action Time:</b> " + DateTime.Now.ToString("dd-MM-yyyy hh:mm tt") + "<br />";
                                body = body + "<br />";
                                body = body + "&nbsp&nbsp Login to " + ConfigurationManager.AppSettings["livedomain_url"].ToString() + " and complete the necessary actions.";
                                //body = body + "&nbsp&nbsp Regards,";
                                //body = body + "<br />";
                                //body = body + "&nbsp&nbsp Sam-Custopedia <br /> ";
                                body = body + "<br />";
                                body = body + "</td><td style='margin-left:20px; border-left-color:white;'>&nbsp&nbsp</td></tr></table>";

                                //sub = " ARN(" + application_no + ") : Application approval required ";
                                //body = "<style>table, th, td {border: 1px solid black;border-collapse: collapse;}</style>";
                                //body = body + "<table style='border-right: 1px solid black;border-top: 1px solid black;border-bottom: 1px solid black;'><tr><td style='border-right-color:white;align:center;'>";
                                //body = body + "<br />";
                                //body = body + "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp <img style='height:150px; width:380px;' src='" + lssource + "'><br />";
                                //body = body + "<br />";
                                //body = body + " &nbsp&nbsp Hello," + head_name + " <br />";
                                //body = body + "<br />";
                                //body = body + "&nbsp&nbsp Greetings! Quick Heads-up on the below<br />";
                                //body = body + "<br />";
                                //body = body + "<table style='margin-left:18px; margin-right:18px;'><tr><th >Group</th><th>ARN</th><th>Customer Name</th><th>Comments</th></tr>";
                                //body = body + "<tr><td>Awaiting approval</td><td>" + application_no + "</td><td>" + customer_name + "</td><td>" + content + " </td></tr>";
                                //body = body + "<tr><td>Actions on Comments</td><td></td><td></td><td></td></tr>";
                                //body = body + "<tr><td>Queries to be addressed</td><td></td><td></td><td></td></tr></table>";
                                //body = body + "<br />";
                                //body = body + "&nbsp&nbsp Log into Sam-Custopedia and complete the necessary actions. <br />";
                                //body = body + "<br />";
                                //body = body + "&nbsp&nbsp Have a fantastic day!<br />";
                                //body = body + "<br />";
                                //body = body + "&nbsp&nbsp Thanks";
                                //body = body + "<br />";
                                //body = body + "&nbsp&nbsp Sam-Custopedia <br /> ";
                                //body = body + "<br />";
                                //body = body + "&nbsp&nbsp<hr>&nbsp&nbsp";
                                //body = body + "&nbsp&nbsp Reach out to us at samcustopedia@samunnati.com <br /> ";
                                //body = body + "<br />";
                                //body = body + "</td><td style='margin-left:20px; border-left-color:white;'>&nbsp&nbsp</td></tr></table>";

                                MailMessage message = new MailMessage();
                                SmtpClient smtp = new SmtpClient();
                                message.From = new MailAddress(ls_username);
                                message.To.Add(new MailAddress(tomail_id));
                                lsBccmail_id = ConfigurationManager.AppSettings["SamagroApprovalBccMail"].ToString();

                                if (lsBccmail_id != null & lsBccmail_id != string.Empty & lsBccmail_id != "")
                                {
                                    lsBCCReceipients = lsBccmail_id.Split(',');
                                    if (lsBccmail_id.Length == 0)
                                    {
                                        message.Bcc.Add(new MailAddress(lsBccmail_id));
                                    }
                                    else
                                    {
                                        foreach (string BCCEmail in lsBCCReceipients)
                                        {
                                            message.Bcc.Add(new MailAddress(BCCEmail)); //Adding Multiple BCC email Id
                                        }
                                    }
                                }

                                if (cc_mailid != null & cc_mailid != string.Empty & cc_mailid != "")
                                {
                                    lsCCReceipients = cc_mailid.Split(',');
                                    if (cc_mailid.Length == 0)
                                    {
                                        message.CC.Add(new MailAddress(cc_mailid));
                                    }
                                    else
                                    {
                                        foreach (string CCEmail in lsCCReceipients)
                                        {
                                            message.CC.Add(new MailAddress(CCEmail)); //Adding Multiple CC email Id
                                        }
                                    }
                                }



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
                        msSQL = "select applicationapproval_gid from agr_trn_tapplicationapproval " +
                                "where  application_gid='" + values.application_gid + "' and hierary_level >'" + level + "' and initiate_flag='N' order by hierary_level asc";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {

                            msSQL = "update agr_trn_tapplicationapproval set initiate_flag='Y'" +
                                    " where applicationapproval_gid='" + objODBCDatareader["applicationapproval_gid"] + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        }
                        objODBCDatareader.Close();

                    }
                    dt_datatable.Dispose();
                }
            }
            values.status = true;
            values.message = "Application " + values.approval_status + " Successfully..!";
            return true;


        }

        public void DaGetAppApprovalNewSummary(string employee_gid, MdlMstApplicationAdd values)
        {
            msSQL = " select a.application_gid,applicationapproval_gid,application_no,customerref_name as customer_name,customer_urn,vertical_name,a.headapproval_status,e.initiate_flag," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,a.approval_status,applicant_type,a.created_by as createdby," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,date_format(a.submitted_date,'%d-%m-%Y %h:%i %p') as updated_date,date_format(a.headapproval_date,'%d-%m-%Y %h:%i %p') as headapproval_date, " +
                    " productcharge_flag, economical_flag,region,overalllimit_amount from agr_mst_tapplication a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                    " left join agr_trn_tapplicationapproval e on e.application_gid=a.application_gid" +
                    " where e.approval_gid='" + employee_gid + "' and  e.approval_status='Pending' " +
                    " and a.application_gid not in (select application_gid from agr_mst_tapplication where headapproval_status like '%Rejected' or headapproval_status like '%Hold') group by a.application_gid order by a.application_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<applicationadd_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getapplicationadd_list.Add(new applicationadd_list
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical_name = dt["vertical_name"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        economical_flag = dt["economical_flag"].ToString(),
                        productcharge_flag = dt["productcharge_flag"].ToString(),
                        application_status = dt["approval_status"].ToString(),
                        applicant_type = dt["applicant_type"].ToString(),
                        updated_date = dt["updated_date"].ToString(),
                        createdby = dt["createdby"].ToString(),
                        applicationapproval_gid = dt["applicationapproval_gid"].ToString(),
                        headapproval_status = dt["headapproval_status"].ToString(),
                        initiate_flag = dt["initiate_flag"].ToString(),
                        headapproval_date = dt["headapproval_date"].ToString(),
                        region = dt["region"].ToString(),
                        overalllimit_amount = dt["overalllimit_amount"].ToString(),
                    });

                }
            }
            values.applicationadd_list = getapplicationadd_list;
            dt_datatable.Dispose();
        }

        public void DaGetAppApprovalRejectedSummary(string employee_gid, MdlMstApplicationAdd values)
        {
            msSQL = " select application_gid from agr_trn_tapplicationapproval where approval_gid='" + employee_gid + "' group by application_gid";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<applicationadd_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr in dt_datatable.Rows)
                {
                    msSQL = " select application_gid from agr_trn_tapplicationapproval where application_gid='" + dr["application_gid"] + "' and approval_status='Rejected'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        msSQL = " select a.application_gid,application_no,customerref_name as customer_name,customer_urn,vertical_name,a.headapproval_status," +
                                " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,a.approval_status,applicant_type,a.created_by as createdby," +
                                " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,date_format(a.submitted_date,'%d-%m-%Y %h:%i %p') as updated_date,date_format(a.headapproval_date,'%d-%m-%Y %h:%i %p') as headapproval_date, " +
                                " productcharge_flag, economical_flag,region,overalllimit_amount from agr_mst_tapplication a" +
                                " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                                " left join adm_mst_tuser c on c.user_gid=b.user_gid  where  application_gid='" + objODBCDatareader["application_gid"] + "'";

                        objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader1.HasRows == true)
                        {
                            getapplicationadd_list.Add(new applicationadd_list
                            {
                                application_no = objODBCDatareader1["application_no"].ToString(),
                                customer_name = objODBCDatareader1["customer_name"].ToString(),
                                customer_urn = objODBCDatareader1["customer_urn"].ToString(),
                                vertical_name = objODBCDatareader1["vertical_name"].ToString(),
                                created_date = objODBCDatareader1["created_date"].ToString(),
                                created_by = objODBCDatareader1["created_by"].ToString(),
                                application_gid = objODBCDatareader1["application_gid"].ToString(),
                                economical_flag = objODBCDatareader1["economical_flag"].ToString(),
                                productcharge_flag = objODBCDatareader1["productcharge_flag"].ToString(),
                                application_status = objODBCDatareader1["approval_status"].ToString(),
                                applicant_type = objODBCDatareader1["applicant_type"].ToString(),
                                updated_date = objODBCDatareader1["updated_date"].ToString(),
                                createdby = objODBCDatareader1["createdby"].ToString(),
                                headapproval_status = objODBCDatareader1["headapproval_status"].ToString(),
                                headapproval_date = objODBCDatareader1["headapproval_date"].ToString(),
                                region = objODBCDatareader1["region"].ToString(),
                                overalllimit_amount = objODBCDatareader1["overalllimit_amount"].ToString(),
                            });

                        }
                        objODBCDatareader1.Close();
                    }
                    objODBCDatareader.Close();
                }
            }
            values.applicationadd_list = getapplicationadd_list;
            dt_datatable.Dispose();

        }


        public void DaGetAppApprovalHoldSummary(string employee_gid, MdlMstApplicationAdd values)
        {
            msSQL = " select application_gid from agr_trn_tapplicationapproval where approval_gid='" + employee_gid + "' group by application_gid";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<applicationadd_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr in dt_datatable.Rows)
                {
                    msSQL = " select application_gid from agr_trn_tapplicationapproval where application_gid='" + dr["application_gid"] + "' and approval_status='Hold'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        msSQL = " select a.application_gid,application_no,customerref_name as customer_name,customer_urn,vertical_name,a.headapproval_status," +
                                " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,a.approval_status,applicant_type,a.created_by as createdby," +
                                " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,date_format(a.submitted_date,'%d-%m-%Y %h:%i %p') as updated_date,date_format(a.headapproval_date,'%d-%m-%Y %h:%i %p') as headapproval_date, " +
                                " productcharge_flag, economical_flag,region,overalllimit_amount from agr_mst_tapplication a" +
                                " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                                " left join adm_mst_tuser c on c.user_gid=b.user_gid  where  application_gid='" + objODBCDatareader["application_gid"] + "'";

                        objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader1.HasRows == true)
                        {
                            getapplicationadd_list.Add(new applicationadd_list
                            {
                                application_no = objODBCDatareader1["application_no"].ToString(),
                                customer_name = objODBCDatareader1["customer_name"].ToString(),
                                customer_urn = objODBCDatareader1["customer_urn"].ToString(),
                                vertical_name = objODBCDatareader1["vertical_name"].ToString(),
                                created_date = objODBCDatareader1["created_date"].ToString(),
                                created_by = objODBCDatareader1["created_by"].ToString(),
                                application_gid = objODBCDatareader1["application_gid"].ToString(),
                                economical_flag = objODBCDatareader1["economical_flag"].ToString(),
                                productcharge_flag = objODBCDatareader1["productcharge_flag"].ToString(),
                                application_status = objODBCDatareader1["approval_status"].ToString(),
                                applicant_type = objODBCDatareader1["applicant_type"].ToString(),
                                updated_date = objODBCDatareader1["updated_date"].ToString(),
                                createdby = objODBCDatareader1["createdby"].ToString(),
                                headapproval_status = objODBCDatareader1["headapproval_status"].ToString(),
                                headapproval_date = objODBCDatareader1["headapproval_date"].ToString(),
                                region = objODBCDatareader1["region"].ToString(),
                                overalllimit_amount = objODBCDatareader1["overalllimit_amount"].ToString(),
                            });

                        }
                        objODBCDatareader1.Close();
                    }
                    objODBCDatareader.Close();
                }
            }
            values.applicationadd_list = getapplicationadd_list;
            dt_datatable.Dispose();
        }


        public void DaGetAppApprovalApprovedSummary(string employee_gid, MdlMstApplicationAdd values)
        {
            msSQL = " select a.application_gid,applicationapproval_gid,application_no,customerref_name as customer_name,customer_urn,vertical_name,a.headapproval_status," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,a.approval_status,applicant_type,a.created_by as createdby," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,date_format(a.submitted_date,'%d-%m-%Y %h:%i %p') as updated_date,date_format(a.headapproval_date,'%d-%m-%Y %h:%i %p') as headapproval_date, " +
                    " productcharge_flag, economical_flag,region,overalllimit_amount from agr_mst_tapplication a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid  " +
                    " left join agr_trn_tapplicationapproval e on e.application_gid=a.application_gid " +
                    " where  e.approval_gid='" + employee_gid + "' and e.approval_status='Approved' and a.headapproval_status like '%Approved' group by a.application_gid order by a.application_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<applicationadd_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getapplicationadd_list.Add(new applicationadd_list
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical_name = dt["vertical_name"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        economical_flag = dt["economical_flag"].ToString(),
                        productcharge_flag = dt["productcharge_flag"].ToString(),
                        application_status = dt["approval_status"].ToString(),
                        applicant_type = dt["applicant_type"].ToString(),
                        updated_date = dt["updated_date"].ToString(),
                        createdby = dt["createdby"].ToString(),
                        applicationapproval_gid = dt["applicationapproval_gid"].ToString(),
                        headapproval_status = dt["headapproval_status"].ToString(),
                        headapproval_date = dt["headapproval_date"].ToString(),
                        region = dt["region"].ToString(),
                        overalllimit_amount = dt["overalllimit_amount"].ToString(),
                    });

                }
            }
            values.applicationadd_list = getapplicationadd_list;
            dt_datatable.Dispose();
        }

        public void DaGetAppCommentStatus(mdlcommentstatus values, string application_gid, string employee_gid)
        {
            msSQL = "select comment_status from agr_trn_tapplicationcomment where  application_gid ='" + application_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objODBCDatareader.Close();
                msSQL = "select comment_status from agr_trn_tapplicationcomment where comment_status='Open' and  application_gid ='" + application_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.commentstatus_flag = "N";
                }
                else
                {
                    values.commentstatus_flag = "Y";
                }
                objODBCDatareader.Close();
            }
            else
            {
                objODBCDatareader.Close();
                values.commentstatus_flag = "Y";
            }


            msSQL = " select approval_status from agr_trn_tapplicationapproval where approval_gid = '" + employee_gid + "' and application_gid = '" + application_gid + "'  order by hierary_level asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    string lsapprovalstatus = dt["approval_status"].ToString();
                    if (lsapprovalstatus == "Approved" || lsapprovalstatus == "Rejected" || lsapprovalstatus == "Hold")
                    {
                        values.approved_flag = "Y";
                    }
                    else
                    {
                        values.approved_flag = "N";
                    }
                }
            }
            dt_datatable.Dispose();
            values.status = true;

        }

        public void DaGetUpdateCommentStatus(mdlcommentstatus values, string applicationcomment_gid, string application_gid, string close_remarks, string user_gid)
        {
            msSQL = " update agr_trn_tapplicationcomment set  comment_status='Closed',";

                 if (close_remarks == null || close_remarks == "")
            {
                msSQL += " close_remarks='',";
            }
            else
            {
                msSQL += " close_remarks='" + close_remarks.Replace("'", "") + "',";

            }
            msSQL +=

                //" close_remarks='" + close_remarks.Replace("'", "") + "'," +
                    " closed_by='" + user_gid + "', closed_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    " where applicationcomment_gid='" + applicationcomment_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "select comment_status from agr_trn_tapplicationcomment where comment_status='Open' and  application_gid ='" + application_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == false)
                {
                    msSQL = "update agr_mst_tapplication set  headapproval_status='Comment Closed'" +
                           " where application_gid='" + application_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                objODBCDatareader.Close();
                values.status = true;
                values.message = "Comment Closed Successfully..!";
                //Mail Start
                try
                {
                    String lshierarchylevel;
                    lshierarchylevel = objdbconn.GetExecuteScalar("select hierary_level from agr_trn_tapplicationcomment a " +
                    "left join agr_trn_tapplicationapproval b on a.applicationapproval_gid = b.applicationapproval_gid where a.applicationcomment_gid='" + applicationcomment_gid + "'");
                    int level = Convert.ToInt16(lshierarchylevel);
                    int nextlevel = level + 1;
                    char nexthierlevel = Convert.ToChar(nextlevel);

                    msSQL = "select application_no from agr_mst_tapplication where application_gid='" + application_gid + "'";
                    application_no = objdbconn.GetExecuteScalar(msSQL);
                    msSQL = "select customerref_name from agr_mst_tapplication where application_gid='" + application_gid + "'";
                    customer_name = objdbconn.GetExecuteScalar(msSQL);
                    msSQL = "select closed_date from agr_trn_tapplicationcomment where applicationcomment_gid='" + applicationcomment_gid + "'";
                    closure_time = objdbconn.GetExecuteScalar(msSQL);
                    msSQL = "select b.employee_emailid from agr_mst_tapplication a left join hrm_mst_temployee b on b.employee_gid = a.clustermanager_gid where application_gid='" + application_gid + "'";
                    cluster_head_mailid = objdbconn.GetExecuteScalar(msSQL);
                    msSQL = " select b.employee_emailid from agr_mst_tapplication a left join hrm_mst_temployee b on b.employee_gid = a.zonalhead_gid where a.application_gid='" + application_gid + "'";
                    zonalhead_mailid = objdbconn.GetExecuteScalar(msSQL);
                    msSQL = " select b.employee_emailid from agr_mst_tapplication a left join hrm_mst_temployee b on b.employee_gid = a.regionalhead_gid where a.application_gid='" + application_gid + "'";
                    regional_head_mailid = objdbconn.GetExecuteScalar(msSQL);
                    msSQL = " select b.employee_emailid from agr_mst_tapplication a left join hrm_mst_temployee b on b.employee_gid = a.businesshead_gid where a.application_gid='" + application_gid + "'";
                    business_head_mailid = objdbconn.GetExecuteScalar(msSQL);
                    msSQL = " select b.employee_emailid from agr_trn_tapplicationapproval a left join hrm_mst_temployee b on b.employee_gid = a.approval_gid where a.application_gid='" + application_gid + "'  and hierary_level ='1'";
                    reportingto_mailid = objdbconn.GetExecuteScalar(msSQL);
                    msSQL = " select b.employee_emailid from agr_mst_tapplication a left join hrm_mst_temployee b on b.employee_gid = a.created_by where a.application_gid='" + application_gid + "'";
                    creater_mailid = objdbconn.GetExecuteScalar(msSQL);
                    msSQL = " select b.employee_emailid from agr_mst_tapplication a left join hrm_mst_temployee b on b.employee_gid = a.created_by where a.application_gid='" + application_gid + "'";
                    creater_mailid = objdbconn.GetExecuteScalar(msSQL);
                    msSQL = " select c.employee_emailid from agr_trn_tapplicationcomment a " +
                            " left join adm_mst_tuser b on a.created_by = b.user_gid " +
                            " left join hrm_mst_temployee c on c.user_gid = b.user_gid where applicationcomment_gid='" + applicationcomment_gid + "'";
                    tomail_id = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = " select clustermanager_name,zonalhead_name,relationshipmanager_name from agr_mst_tapplication where application_gid = '" + application_gid + "'";
                    objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader1.HasRows == true)
                    {
                        cluster_head = objODBCDatareader1["clustermanager_name"].ToString();
                        zonal_head = objODBCDatareader1["zonalhead_name"].ToString();
                        relationshipmanager_name = objODBCDatareader1["relationshipmanager_name"].ToString();
                    }
                    objODBCDatareader1.Close();



                    msSQL = " SELECT pop_server, pop_port, pop_username, pop_password" +
                                    " FROM adm_mst_tcompany";
                    objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader1.HasRows == true)
                    {
                        ls_server = objODBCDatareader1["pop_server"].ToString();
                        ls_port = Convert.ToInt32(objODBCDatareader1["pop_port"]);
                        ls_username = objODBCDatareader1["pop_username"].ToString();
                        ls_password = objODBCDatareader1["pop_password"].ToString();
                    }
                    objODBCDatareader1.Close();
                    if (nexthierlevel == '\u0006')
                    {

                        cc_mailid = "" + reportingto_mailid + "," + regional_head_mailid + "," + cluster_head_mailid + "," + zonalhead_mailid + "";
                    }
                    else if (nexthierlevel == '\u0005')
                    {

                        cc_mailid = "" + reportingto_mailid + "," + cluster_head_mailid + "," + regional_head_mailid + "," + creater_mailid + "";
                    }
                    else if (nexthierlevel == '\u0004')
                    {

                        cc_mailid = "" + reportingto_mailid + "," + creater_mailid + "," + cluster_head_mailid + "";
                    }
                    else if (nexthierlevel == '\u0003')
                    {

                        cc_mailid = "" + reportingto_mailid + "," + zonalhead_mailid +  "," + creater_mailid + "";
                    }
                    else if (nexthierlevel == '\u0002')
                    {

                        cc_mailid = "" + reportingto_mailid + "," + creater_mailid + "," + zonalhead_mailid + "";
                    }


                    //lssource = ConfigurationManager.AppSettings["img_path"];
                    sub = " Comment Closed : ARN(" + application_no + ")  ";
                    body = "<style>table, th, td {border: 1px solid black;border-collapse: collapse;}</style>";
                    body = body + "<table style='border-right: 1px solid black;border-top: 1px solid black;border-bottom: 1px solid black;'><tr><td style='border-right-color:white;align:center;'>";
                    body = body + "<br />";
                    body = body + "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp <img style='height:150px; width:380px;' src='" + lssource + "'><br />";
                    body = body + "<br />";
                    body = body + " &nbsp &nbsp Dear Sir / Madam, <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp Greetings! <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp The user has closed the comment. Please check and proceed with the application. <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp <b> Application Number : </b> " + application_no + "  <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp <b> Customer Name : </b> " + HttpUtility.HtmlEncode(customer_name)+ "  <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp <b> RM Name : </b> " + HttpUtility.HtmlEncode(relationshipmanager_name)+ "  <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp <b> Cluster Head Name : </b> " + HttpUtility.HtmlEncode(cluster_head)+ "  <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp <b> Zonal Head Name : </b> " + HttpUtility.HtmlEncode(zonal_head)+ "  <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp <b> Closure Time :</b>  " + closure_time + "  <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp Log into " + ConfigurationManager.AppSettings["livedomain_url"].ToString() + " and complete the necessary actions.";
                    body = body + "<br />";
                    //body = body + "&nbsp &nbsp Regards";
                    //body = body + "<br />";
                    //body = body + "&nbsp &nbsp Sam-Custopedia <br /> ";
                    //body = body + "<br />";
                    //body = body + "&nbsp &nbsp<hr>&nbsp&nbsp";
                    //body = body + "&nbsp &nbsp Reach out to us at samcustopedia@samunnati.com <br /> ";
                    //body = body + "<br />";
                    body = body + "</td><td style='margin-left:20px; border-left-color:white;'>&nbsp&nbsp</td></tr></table>";

                    MailMessage message = new MailMessage();
                    SmtpClient smtp = new SmtpClient();
                    message.From = new MailAddress(ls_username);
                    message.To.Add(new MailAddress(tomail_id));
                    lsBccmail_id = ConfigurationManager.AppSettings["SamagroApprovalBccMail"].ToString();

                    if (lsBccmail_id != null & lsBccmail_id != string.Empty & lsBccmail_id != "")
                    {
                        lsBCCReceipients = lsBccmail_id.Split(',');
                        if (lsBccmail_id.Length == 0)
                        {
                            message.Bcc.Add(new MailAddress(lsBccmail_id));
                        }
                        else
                        {
                            foreach (string BCCEmail in lsBCCReceipients)
                            {
                                message.Bcc.Add(new MailAddress(BCCEmail)); //Adding Multiple BCC email Id
                            }
                        }
                    }


                    if (cc_mailid != null & cc_mailid != string.Empty & cc_mailid != "")
                    {
                        lsCCReceipients = cc_mailid.Split(',');
                        if (cc_mailid.Length == 0)
                        {
                            message.CC.Add(new MailAddress(cc_mailid));
                        }
                        else
                        {
                            foreach (string CCEmail in lsCCReceipients)
                            {
                                message.CC.Add(new MailAddress(CCEmail)); //Adding Multiple CC email Id
                            }
                        }
                    }



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
                }
                catch (Exception ex)
                {
                    values.message = ex.ToString();
                }
                //Mail End
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }

        }

        public void DaBusinessApplicationCount(string user_gid, string employee_gid, businessApplicationCount values)
        {

            msSQL = " select count(a.application_gid) as newcount from agr_mst_tapplication a " +
                     " left join agr_trn_tapplicationapproval b on a.application_gid = b.application_gid" +
                     " where  b.approval_gid='" + employee_gid + "' and b.approval_status='Pending'" +
                     " and a.application_gid not in (select application_gid from agr_mst_tapplication where headapproval_status like '%Rejected' or headapproval_status like '%Hold')";
            values.newbusinessapplication_count = objdbconn.GetExecuteScalar(msSQL);
            int newapplicationcount = Convert.ToInt16(values.newbusinessapplication_count);

            msSQL = " select application_gid from agr_trn_tapplicationapproval where approval_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getrejectedlist = new List<rejectedlist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr in dt_datatable.Rows)
                {
                    msSQL = " select application_gid from agr_trn_tapplicationapproval where application_gid='" + dr["application_gid"] + "' and approval_status='Rejected'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        msSQL = " select a.application_gid  from agr_mst_tapplication a" +
                                "   where  application_gid='" + objODBCDatareader["application_gid"] + "'";

                        objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader1.HasRows == true)
                        {
                            getrejectedlist.Add(new rejectedlist
                            {

                            });

                        }
                        objODBCDatareader1.Close();
                    }
                    objODBCDatareader.Close();
                }
            }
            values.rejectedlist = getrejectedlist;
            dt_datatable.Dispose();
            int rejectedcount = getrejectedlist.Count;
            values.rejectedapplication_count = Convert.ToInt16(rejectedcount);

            msSQL = " select application_gid from agr_trn_tapplicationapproval where approval_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getHoldlist = new List<Holdlist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr in dt_datatable.Rows)
                {
                    msSQL = " select application_gid from agr_trn_tapplicationapproval where application_gid='" + dr["application_gid"] + "' and approval_status='Hold'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        msSQL = " select a.application_gid  from agr_mst_tapplication a" +
                                "   where  application_gid='" + objODBCDatareader["application_gid"] + "'";

                        objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader1.HasRows == true)
                        {
                            getHoldlist.Add(new Holdlist
                            {


                            });

                        }
                        objODBCDatareader1.Close();
                    }
                    objODBCDatareader.Close();
                }
            }
            values.Holdlist = getHoldlist;
            dt_datatable.Dispose();
            int holdcount = getHoldlist.Count;

            values.holdapplication_count = Convert.ToInt16(holdcount);

            msSQL = " select count(a.application_gid) as approvedcount from agr_mst_tapplication a" +
                    " left join agr_trn_tapplicationapproval e on e.application_gid=a.application_gid " +
                    " where  e.approval_gid='" + employee_gid + "' and e.approval_status='Approved' and " +
                    " a.headapproval_status like '%Approved'  ";
            values.approvedapplication_count = objdbconn.GetExecuteScalar(msSQL);
            int approvedcount = Convert.ToInt16(values.approvedapplication_count);
            int lstotal = newapplicationcount + rejectedcount + holdcount + approvedcount;
            values.lstotalcount = Convert.ToInt16(lstotal);
        }

        public void DaGetproceedapprovalflag(Mdlproceedapproval values, string application_gid)
        {

            try
            {

                msSQL = " SELECT  a.application_gid,a.approval_status,b.approval_status,b.hierary_level,b.approved_date,b.created_date,b.approval_gid, " +
            " CASE  WHEN((a.approval_status != 'Incomplete') AND(b.approval_status is not null or b.approval_status = 'Approved')) " +
          " THEN 'N' else 'Y'  END AS 'proceedtoapproval_flag' " +
           " from agr_mst_tapplication a " +
         " left join agr_trn_tapplicationapproval b on a.application_gid = b.application_gid " +
              " Where  a.approval_status not like '%Rejected%' and a.application_gid ='" + application_gid + "'group by a.application_gid order by b.created_date desc";

                objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader1.HasRows == true)
                {
                    values.proceedtoapproval_flag = objODBCDatareader1["proceedtoapproval_flag"].ToString();
                   
                }
                objODBCDatareader1.Close();

            }

            catch (Exception ex)
            {


            }

        }
    }
}