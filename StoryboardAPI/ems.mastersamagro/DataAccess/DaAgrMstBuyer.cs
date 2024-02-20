using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Odbc;
using ems.utilities.Functions;
using ems.mastersamagro.Models;
using System.Net.Mail;
using System.Net;
using System.Configuration;
using System.Web;
namespace ems.mastersamagro.DataAccess
{
    /// <summary>
    /// This DataAccess will provide access to various functions in Buyer Master
    /// </summary>
    /// <remarks>Written by Abilash.A </remarks>
    public class DaAgrMstbuyer
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        OdbcDataReader objODBCDatareader, objreader;
        DataTable dt_datatable, dt_table;
        string msSQL, msGetGid, msGetGid1, msGetGidREF, lspath, lsemployeeGID;
        int mnResult;
        string frommail_id, sub, tomail_id, lsstate_name, contactperson, customer_name, body, message, ls_server, ls_username, ls_password, lscontent = string.Empty;
        string lsbuyer_code, lsbuyer_name, lscoi_date, lsbusinessstart_date, lsyear_business, lsmonth_business, lsconstitution_gid, lsconstitution_name;
        string lscin_no, lspan_no, lsgst_no, lscontactperson_firstname, lscontactperson_middlename, lscontactperson_lastname;
        string lscreatedby, lscreateddate;
        int ls_port;
        
        string[] lsToReceipients;
        string cc, commoncc;
        string cc_mailid = string.Empty;
        string to, commonto;
        public string lssource;

        string lsmember_gid, lssupportteam_gid, lssuportteam_name, lsmember_name, lsstatus, count, app_count;


        public bool DabuyerSave(string employee_gid, MdlMstbuyer values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("BB");
            msGetGidREF = objcmnfunctions.GetMasterGID("BBCH_");

            msSQL = " insert into ocs_mst_tbuyer(" +
                    " buyer_gid," +
                    " buyer_code," +
                    " buyer_name," +
                    " coi_date," +
                    " businessstart_date," +
                    " year_business," +
                    " month_business," +
                    " constitution_gid," +
                    " constitution_name," +
                    " cin_no," +
                    " pan_no," +
                    " contactperson_fn," +
                    " contactperson_mn," +
                    " contactperson_ln," +
                    " buyertocredit_flag," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + msGetGidREF + "'," +
                    "'" + values.buyer_name + "',";

            if ((values.coi_date == null) || (values.coi_date == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.coi_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            if ((values.businessstart_date == null) || (values.businessstart_date == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.businessstart_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }

            msSQL += "'" + values.year_business + "'," +
                    "'" + values.month_business + "'," +
                    "'" + values.constitution_gid + "'," +
                    "'" + values.constitution_name + "'," +
                    "'" + values.cin_no + "'," +
                    "'" + values.pan_no + "'," +
                    "'" + values.contactperson_fn + "'," +
                    "'" + values.contactperson_mn + "'," +
                    "'" + values.contactperson_ln + "'," +
                    "'" + "N" + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                //Updates

                msSQL = "update ocs_mst_tbuyer2mobileno set buyer_gid ='" + msGetGid + "' where buyer_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tbuyer2emailaddress set buyer_gid ='" + msGetGid + "' where buyer_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tbuyer2address set buyer_gid ='" + msGetGid + "' where buyer_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tbuyer2bank set buyer_gid ='" + msGetGid + "' where buyer_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tbuyer2gst set buyer_gid ='" + msGetGid + "' where buyer_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Buyer Details Saved Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured While Saving Buyer";
                return false;
            }

        }

        public bool DabuyerEditSave(string employee_gid, MdlMstbuyer values)
        {

            msSQL = " select buyer_gid, buyer_code, buyer_name, coi_date, businessstart_date, year_business, month_business, constitution_gid," +
                   " constitution_name, cin_no, pan_no, contactperson_fn, contactperson_mn, contactperson_ln " +
                   " from ocs_mst_tbuyer where buyer_gid='" + values.buyer_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsbuyer_code = objODBCDatareader["buyer_code"].ToString();
                lsbuyer_name = objODBCDatareader["buyer_name"].ToString();
                if (objODBCDatareader["coi_date"].ToString() == "")
                {
                }
                else
                {
                    lscoi_date = Convert.ToDateTime(objODBCDatareader["coi_date"]).ToString("dd-MM-yyyy");
                }
                if (objODBCDatareader["businessstart_date"].ToString() == "")
                {
                }
                else
                {
                    lsbusinessstart_date = Convert.ToDateTime(objODBCDatareader["businessstart_date"]).ToString("dd-MM-yyyy");
                }
                lsyear_business = objODBCDatareader["year_business"].ToString();
                lsmonth_business = objODBCDatareader["month_business"].ToString();
                lsconstitution_gid = objODBCDatareader["constitution_gid"].ToString();
                lsconstitution_name = objODBCDatareader["constitution_name"].ToString();
                lscin_no = objODBCDatareader["cin_no"].ToString();
                lspan_no = objODBCDatareader["pan_no"].ToString();
                lscontactperson_firstname = objODBCDatareader["contactperson_fn"].ToString();
                lscontactperson_middlename = objODBCDatareader["contactperson_mn"].ToString();
                lscontactperson_lastname = objODBCDatareader["contactperson_ln"].ToString();
            }
            objODBCDatareader.Close();
            msSQL = " update ocs_mst_tbuyer set " +
                    " buyer_code='" + values.buyer_code + "'," +
                    " buyer_name='" + values.buyer_name + "',";
            if (Convert.ToDateTime(values.editcoi_date).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
            {

            }
            else
            {
                msSQL += " coi_date='" + Convert.ToDateTime(values.editcoi_date).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "',";
            }
            if (Convert.ToDateTime(values.editbusinessstart_date).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
            {

            }
            else
            {
                msSQL += " businessstart_date='" + Convert.ToDateTime(values.editbusinessstart_date).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "',";
            }
            msSQL += " year_business='" + values.year_business + "'," +
                     " month_business='" + values.month_business + "'," +
                     " constitution_gid='" + values.constitution_gid + "'," +
                     " constitution_name='" + values.constitution_name + "'," +
                     " cin_no='" + values.cin_no + "'," +
                     " pan_no='" + values.pan_no + "'," +
                     " contactperson_fn='" + values.contactperson_fn + "'," +
                     " contactperson_mn='" + values.contactperson_mn + "'," +
                     " contactperson_ln='" + values.contactperson_ln + "'," +
                     " updated_by='" + employee_gid + "'," +
                     " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                     " where buyer_gid='" + values.buyer_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
               if (mnResult != 0)
            {

                msGetGid = objcmnfunctions.GetMasterGID("BULG");

                msSQL = "Insert into ocs_mst_tbuyerupdatelog(" +
               " buyerupdatelog_gid, " +
               " buyer_gid, " +
               " buyer_code," +
               " buyer_name," +
               " coi_date," +
               " businessstart_date," +
               " year_business," +
               " month_business," +
               " constitution_gid," +
               " constitution_name," +
               " cin_no," +
               " pan_no," +
               " contactperson_fn," +
               " contactperson_mn," +
               " contactperson_ln," +
               " updated_by," +
               " updated_date)" +
               " values (" +
               "'" + msGetGid + "'," +
               "'" + values.buyer_gid + "'," +
               "'" + lsbuyer_code + "'," +
               "'" + lsbuyer_name + "'," +
               "'" + lscoi_date + "'," +
               "'" + lsbusinessstart_date + "'," +
               "'" + lsyear_business + "'," +
                         "'" + lsmonth_business + "'," +
                         "'" + lsconstitution_gid + "'," +
                         "'" + lsconstitution_name + "'," +
                         "'" + lscin_no + "'," +
                         "'" + lspan_no + "'," +
                         "'" + lscontactperson_firstname + "'," +
                         "'" + lscontactperson_middlename + "'," +
                         "'" + lscontactperson_lastname + "'," +
                         "'" + employee_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                //Updates

                msSQL = "update ocs_mst_tbuyer2mobileno set buyer_gid ='" + values.buyer_gid + "' where buyer_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tbuyer2emailaddress set buyer_gid ='" + values.buyer_gid + "' where buyer_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tbuyer2address set buyer_gid ='" + values.buyer_gid + "' where buyer_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tbuyer2bank set buyer_gid ='" + values.buyer_gid + "' where buyer_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tbuyer2gst set buyer_gid ='" + values.buyer_gid + "' where buyer_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Buyer Details Saved Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured While Saving Buyer";
                return false;
            }

        }


        public bool DabuyerSubmit(string employee_gid, MdlMstbuyer values)
        {
            msSQL = "select buyer_gid from ocs_mst_tbuyer2mobileno where buyer_gid='" + employee_gid + "' and primary_mobileno='Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Primary Mobile Number";
                return false;
            }
            objODBCDatareader.Close();
            msSQL = "select buyer_gid from ocs_mst_tbuyer2mobileno where buyer_gid='" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Atleast One Mobile Number";
                return false;
            }
            objODBCDatareader.Close();
            msSQL = "select buyer_gid from ocs_mst_tbuyer2address where buyer_gid='" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Atleast One Address";
                return false;
            }
            objODBCDatareader.Close();
            msGetGid = objcmnfunctions.GetMasterGID("BB");
            msGetGidREF = objcmnfunctions.GetMasterGID("BBCH_");

            msSQL = " insert into ocs_mst_tbuyer(" +
                    " buyer_gid," +
                    " buyer_code," +
                    " buyer_name," +
                    " coi_date," +
                    " businessstart_date," +
                    " year_business," +
                    " month_business," +
                    " constitution_gid," +
                    " constitution_name," +
                    " cin_no," +
                    " pan_no," +
                    " contactperson_fn," +
                    " contactperson_mn," +
                    " contactperson_ln," +
                    " buyertocredit_flag," +
                    " credit_status," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + msGetGidREF + "'," +
                    "'" + values.buyer_name + "',";

            if ((values.coi_date == null) || (values.coi_date == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.coi_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            if ((values.businessstart_date == null) || (values.businessstart_date == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.businessstart_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }

            msSQL += "'" + values.year_business + "'," +
                    "'" + values.month_business + "'," +
                    "'" + values.constitution_gid + "'," +
                    "'" + values.constitution_name + "'," +
                    "'" + values.cin_no + "'," +
                    "'" + values.pan_no + "'," +
                    "'" + values.contactperson_fn + "'," +
                    "'" + values.contactperson_mn + "'," +
                    "'" + values.contactperson_ln + "'," +
                    "'" + "Y" + "'," +
                    "'" + "Pending" + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

            //Buyer mail function
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                //Updates

                msSQL = "update ocs_mst_tbuyer2mobileno set buyer_gid ='" + msGetGid + "' where buyer_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tbuyer2emailaddress set buyer_gid ='" + msGetGid + "' where buyer_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tbuyer2address set buyer_gid ='" + msGetGid + "' where buyer_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tbuyer2bank set buyer_gid ='" + msGetGid + "' where buyer_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tbuyer2gst set buyer_gid ='" + msGetGid + "' where buyer_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                values.status = true;
                values.message = "Buyer Details Submitted Successfully";
                try
                  {
                    msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        ls_server = objODBCDatareader["pop_server"].ToString();
                        ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                        ls_username = objODBCDatareader["pop_username"].ToString();
                        ls_password = objODBCDatareader["pop_password"].ToString();

                    }
                     objODBCDatareader.Close();

                    msSQL = "select group_concat(employee_emailid) from ocs_mst_tcredit2nationalmanager a left join hrm_mst_temployee b on a.employee_gid=b.employee_gid";
                    tomail_id = objdbconn.GetExecuteScalar(msSQL) + ",";

                    msSQL = "select group_concat(employee_emailid) from ocs_mst_tcreditr2regionalmanager a left join hrm_mst_temployee b on a.employee_gid=b.employee_gid;";
                    to = objdbconn.GetExecuteScalar(msSQL);

                    tomail_id += to;

                    List<string> uniques = tomail_id.Split(',').Distinct().ToList();
                    string tomailid = string.Join(",", uniques);

                    msSQL = "select state_name from ocs_mst_tbuyer2address where buyer_gid ='" + msGetGid + "'";
                    lsstate_name = objdbconn.GetExecuteScalar(msSQL);
                    msSQL = " select  buyer_name,  date_format(a.created_date, '%d-%m-%Y %h:%i:%s %p') as 'created_date'," +
                     " concat(c.user_firstname, ' ', c.user_lastname, '/', c.user_code) as created_by" +
                     " from ocs_mst_tbuyer a  left join hrm_mst_temployee b on b.employee_gid = a.created_by"  +
                     " left join adm_mst_tuser c on b.user_gid = c.user_gid  " +
                     " where buyer_gid='" + msGetGid + "'";
                     objODBCDatareader = objdbconn.GetDataReader(msSQL);
                     if (objODBCDatareader.HasRows == true)
                    {
                        lscreatedby = objODBCDatareader["created_by"].ToString();
                        lscreateddate = objODBCDatareader["created_date"].ToString();
                        lsbuyer_name = objODBCDatareader["buyer_name"].ToString();
                    }
                    //lssource = ConfigurationManager.AppSettings["img_path"];
                    objODBCDatareader.Close();
                    //lssource = ConfigurationManager.AppSettings["img_path"];
                    sub = "Buyer Approval Required";
                    body = "<style>table, th, td {border: 1px solid black;border-collapse: collapse;}</style>";
                    body = body + "<table style='border-right: 1px solid black;border-top: 1px solid black;border-bottom: 1px solid black;'><tr><td style='border-right-color:white;align:center;'>";
                    body = body + "<br />";
                    body = body + "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp <img style='height:150px; width:380px;' src='" + lssource + "'><br />";
                    body = body + "<br />";
                    body = body + " &nbsp &nbsp Dear Sir / Madam, <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp Greetings! <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp The below buyer has been created. Please log into Sam-Custopedia, valdiate, assign limit and approve. <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp <b>  Buyer Name :  </b>  " + HttpUtility.HtmlEncode(lsbuyer_name)+ "  <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp <b> Buyer State : </b>" + HttpUtility.HtmlEncode(lsstate_name) + " <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp <b> RM Name : </b> " + HttpUtility.HtmlEncode(lscreatedby)+ "  <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp <b> Submission Time : </b> " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "   <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp Login to " + ConfigurationManager.AppSettings["livedomain_url"].ToString() + " and complete the necessary actions.";
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

                    if (tomailid != null & tomailid != string.Empty & tomailid != "")
                    {
                        lsToReceipients = tomailid.Split(',');
                        if (tomailid.Length == 0)
                        {
                            message.To.Add(new MailAddress(tomailid));
                        }
                        else
                        {
                            foreach (string ToEmail in lsToReceipients)
                            {
                                message.To.Add(new MailAddress(ToEmail)); //Adding Multiple TO email Id
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
                    return true;
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
                values.status = false;
                values.message = "Error Occured While Submitting Buyer";
                return false;
            }

        }

        public bool DabuyerEditSubmit(string employee_gid, MdlMstbuyer values)
        {

            msSQL = "select buyer_gid from ocs_mst_tbuyer2mobileno where buyer_gid='" + employee_gid + "' or buyer_gid='" + values.buyer_gid + "' and primary_mobileno='Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Primary Mobile Number";
                return false;
            }
            objODBCDatareader.Close();

            msSQL = "select buyer_gid from ocs_mst_tbuyer2mobileno where buyer_gid='" + employee_gid + "' or buyer_gid='" + values.buyer_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Atleast One Mobile Number";
                return false;
            }
            objODBCDatareader.Close();

            msSQL = "select buyer_gid from ocs_mst_tbuyer2address where buyer_gid='" + employee_gid + "' or buyer_gid='" + values.buyer_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Atleast One Address";
                return false;
            }
            objODBCDatareader.Close();

            msSQL = " select buyer_gid, buyer_code, buyer_name, coi_date, businessstart_date, year_business, month_business, constitution_gid," +
                     " constitution_name, cin_no, pan_no, contactperson_fn, contactperson_mn, contactperson_ln " +
                     " from ocs_mst_tbuyer where buyer_gid='" + values.buyer_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsbuyer_code = objODBCDatareader["buyer_code"].ToString();
                lsbuyer_name = objODBCDatareader["buyer_name"].ToString();
                if (objODBCDatareader["coi_date"].ToString() == "")
                {
                }
                else
                {
                    lscoi_date = Convert.ToDateTime(objODBCDatareader["coi_date"]).ToString("dd-MM-yyyy");
                }
                if (objODBCDatareader["businessstart_date"].ToString() == "")
                {
                }
                else
                {
                    lsbusinessstart_date = Convert.ToDateTime(objODBCDatareader["businessstart_date"]).ToString("dd-MM-yyyy");
                }
                lsyear_business = objODBCDatareader["year_business"].ToString();
                lsmonth_business = objODBCDatareader["month_business"].ToString();
                lsconstitution_gid = objODBCDatareader["constitution_gid"].ToString();
                lsconstitution_name = objODBCDatareader["constitution_name"].ToString();
                lscin_no = objODBCDatareader["cin_no"].ToString();
                lspan_no = objODBCDatareader["pan_no"].ToString();
                lscontactperson_firstname = objODBCDatareader["contactperson_fn"].ToString();
                lscontactperson_middlename = objODBCDatareader["contactperson_mn"].ToString();
                lscontactperson_lastname = objODBCDatareader["contactperson_ln"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " update ocs_mst_tbuyer set " +
                    " buyer_code='" + values.buyer_code + "'," +
                    " buyer_name='" + values.buyer_name + "',";
            if (Convert.ToDateTime(values.editcoi_date).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
            {

            }
            else
            {
                msSQL += " coi_date='" + Convert.ToDateTime(values.editcoi_date).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "',";
            }
            if (Convert.ToDateTime(values.editbusinessstart_date).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
            {

            }
            else
            {
                msSQL += " businessstart_date='" + Convert.ToDateTime(values.editbusinessstart_date).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "',";
            }
            msSQL += " year_business='" + values.year_business + "'," +
                     " month_business='" + values.month_business + "'," +
                     " constitution_gid='" + values.constitution_gid + "'," +
                     " constitution_name='" + values.constitution_name + "'," +
                     " cin_no='" + values.cin_no + "'," +
                     " pan_no='" + values.pan_no + "'," +
                     " contactperson_fn='" + values.contactperson_fn + "'," +
                     " contactperson_mn='" + values.contactperson_mn + "'," +
                     " contactperson_ln='" + values.contactperson_ln + "'," +
                     " buyertocredit_flag='" + "Y" + "'," +
                     " credit_status='" + "Pending" + "'," +
                     " updated_by='" + employee_gid + "'," +
                     " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                     " where buyer_gid='" + values.buyer_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                msGetGid = objcmnfunctions.GetMasterGID("BULG");

                msSQL = "Insert into ocs_mst_tbuyerupdatelog(" +
               " buyerupdatelog_gid, " +
               " buyer_gid, " +
               " buyer_code," +
               " buyer_name," +
               " coi_date," +
               " businessstart_date," +
               " year_business," +
               " month_business," +
               " constitution_gid," +
               " constitution_name," +
               " cin_no," +
               " pan_no," +
               " contactperson_fn," +
               " contactperson_mn," +
               " contactperson_ln," +
               " updated_by," +
               " updated_date)" +
               " values (" +
               "'" + msGetGid + "'," +
               "'" + values.buyer_gid + "'," +
               "'" + lsbuyer_code + "'," +
               "'" + lsbuyer_name + "'," +
               "'" + lscoi_date + "'," +
               "'" + lsbusinessstart_date + "'," +
               "'" + lsyear_business + "'," +
                         "'" + lsmonth_business + "'," +
                         "'" + lsconstitution_gid + "'," +
                         "'" + lsconstitution_name + "'," +
                         "'" + lscin_no + "'," +
                         "'" + lspan_no + "'," +
                         "'" + lscontactperson_firstname + "'," +
                         "'" + lscontactperson_middlename + "'," +
                         "'" + lscontactperson_lastname + "'," +
                         "'" + employee_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                //Updates

                msSQL = "update ocs_mst_tbuyer2mobileno set buyer_gid ='" + values.buyer_gid + "' where buyer_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tbuyer2emailaddress set buyer_gid ='" + values.buyer_gid + "' where buyer_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tbuyer2address set buyer_gid ='" + values.buyer_gid + "' where buyer_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tbuyer2bank set buyer_gid ='" + values.buyer_gid + "' where buyer_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tbuyer2gst set buyer_gid ='" + values.buyer_gid + "' where buyer_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                values.status = true;
                values.message = "Buyer Details Submitted Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
                return false;
            }

        }

        public bool DabuyerEditUpdate(string employee_gid, MdlMstbuyer values)
        {

            msSQL = "select buyer_gid from ocs_mst_tbuyer2mobileno where buyer_gid='" + employee_gid + "' or buyer_gid='" + values.buyer_gid + "' and primary_mobileno='Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Primary Mobile Number";
                return false;
            }
            objODBCDatareader.Close();

            msSQL = "select buyer_gid from ocs_mst_tbuyer2mobileno where buyer_gid='" + employee_gid + "' or buyer_gid='" + values.buyer_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Atleast One Mobile Number";
                return false;
            }
            objODBCDatareader.Close();

            msSQL = "select buyer_gid from ocs_mst_tbuyer2address where buyer_gid='" + employee_gid + "' or buyer_gid='" + values.buyer_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Atleast One Address";
                return false;
            }
            objODBCDatareader.Close();

            msSQL = " select buyer_gid, buyer_code, buyer_name, coi_date, businessstart_date, year_business, month_business, constitution_gid," +
                     " constitution_name, cin_no, pan_no, contactperson_fn, contactperson_mn, contactperson_ln " +
                     " from ocs_mst_tbuyer where buyer_gid='" + values.buyer_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsbuyer_code = objODBCDatareader["buyer_code"].ToString();
                lsbuyer_name = objODBCDatareader["buyer_name"].ToString();
                if (objODBCDatareader["coi_date"].ToString() == "")
                {
                }
                else
                {
                    lscoi_date = Convert.ToDateTime(objODBCDatareader["coi_date"]).ToString("dd-MM-yyyy");
                }
                if (objODBCDatareader["businessstart_date"].ToString() == "")
                {
                }
                else
                {
                    lsbusinessstart_date = Convert.ToDateTime(objODBCDatareader["businessstart_date"]).ToString("dd-MM-yyyy");
                }
                lsyear_business = objODBCDatareader["year_business"].ToString();
                lsmonth_business = objODBCDatareader["month_business"].ToString();
                lsconstitution_gid = objODBCDatareader["constitution_gid"].ToString();
                lsconstitution_name = objODBCDatareader["constitution_name"].ToString();
                lscin_no = objODBCDatareader["cin_no"].ToString();
                lspan_no = objODBCDatareader["pan_no"].ToString();
                lscontactperson_firstname = objODBCDatareader["contactperson_fn"].ToString();
                lscontactperson_middlename = objODBCDatareader["contactperson_mn"].ToString();
                lscontactperson_lastname = objODBCDatareader["contactperson_ln"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " update ocs_mst_tbuyer set " +
                    " buyer_code='" + values.buyer_code + "'," +
                    " buyer_name='" + values.buyer_name + "',";
            if (Convert.ToDateTime(values.editcoi_date).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
            {

            }
            else
            {
                msSQL += " coi_date='" + Convert.ToDateTime(values.editcoi_date).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "',";
            }
            if (Convert.ToDateTime(values.editbusinessstart_date).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
            {

            }
            else
            {
                msSQL += " businessstart_date='" + Convert.ToDateTime(values.editbusinessstart_date).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "',";
            }
            msSQL += " year_business='" + values.year_business + "'," +
                     " month_business='" + values.month_business + "'," +
                     " constitution_gid='" + values.constitution_gid + "'," +
                     " constitution_name='" + values.constitution_name + "'," +
                     " cin_no='" + values.cin_no + "'," +
                     " pan_no='" + values.pan_no + "'," +
                     " contactperson_fn='" + values.contactperson_fn + "'," +
                     " contactperson_mn='" + values.contactperson_mn + "'," +
                     " contactperson_ln='" + values.contactperson_ln + "'," +
                     " buyertocredit_flag='" + "Y" + "'," +
                     " updated_by='" + employee_gid + "'," +
                     " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                     " where buyer_gid='" + values.buyer_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                msGetGid = objcmnfunctions.GetMasterGID("BULG");

                msSQL = "Insert into ocs_mst_tbuyerupdatelog(" +
               " buyerupdatelog_gid, " +
               " buyer_gid, " +
               " buyer_code," +
               " buyer_name," +
               " coi_date," +
               " businessstart_date," +
               " year_business," +
               " month_business," +
               " constitution_gid," +
               " constitution_name," +
               " cin_no," +
               " pan_no," +
               " contactperson_fn," +
               " contactperson_mn," +
               " contactperson_ln," +
               " updated_by," +
               " updated_date)" +
               " values (" +
               "'" + msGetGid + "'," +
               "'" + values.buyer_gid + "'," +
               "'" + lsbuyer_code + "'," +
               "'" + lsbuyer_name + "'," +
               "'" + lscoi_date + "'," +
               "'" + lsbusinessstart_date + "'," +
               "'" + lsyear_business + "'," +
                         "'" + lsmonth_business + "'," +
                         "'" + lsconstitution_gid + "'," +
                         "'" + lsconstitution_name + "'," +
                         "'" + lscin_no + "'," +
                         "'" + lspan_no + "'," +
                         "'" + lscontactperson_firstname + "'," +
                         "'" + lscontactperson_middlename + "'," +
                         "'" + lscontactperson_lastname + "'," +
                         "'" + employee_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                //Updates

                msSQL = "update ocs_mst_tbuyer2mobileno set buyer_gid ='" + values.buyer_gid + "' where buyer_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tbuyer2emailaddress set buyer_gid ='" + values.buyer_gid + "' where buyer_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tbuyer2address set buyer_gid ='" + values.buyer_gid + "' where buyer_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tbuyer2bank set buyer_gid ='" + values.buyer_gid + "' where buyer_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tbuyer2gst set buyer_gid ='" + values.buyer_gid + "' where buyer_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                values.status = true;
                values.message = "Buyer Details Updated Successfully";
                try
                {
                   msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                   objODBCDatareader = objdbconn.GetDataReader(msSQL);
                   if (objODBCDatareader.HasRows == true)
                    {
                        ls_server = objODBCDatareader["pop_server"].ToString();
                        ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                        ls_username = objODBCDatareader["pop_username"].ToString();
                        ls_password = objODBCDatareader["pop_password"].ToString();
                    }
                    objODBCDatareader.Close();
                    msSQL = "select email_id from hrm_mst_tdepartment where department_name='Credit Administration'";
                    tomail_id = objdbconn.GetExecuteScalar(msSQL);
                    msSQL = " select  date_format(a.created_date, '%d-%m-%Y %h:%i:%s %p') as 'created_date'," +
                    " concat(c.user_firstname, ' ', c.user_lastname, '/', c.user_code) as created_by" +
                    " from ocs_mst_tbuyer a  left join hrm_mst_temployee b on b.employee_gid = a.created_by" +
                    " left join adm_mst_tuser c on b.user_gid = c.user_gid  " +
                    " where buyer_gid='" + values.buyer_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lscreatedby = objODBCDatareader["created_by"].ToString();
                        lscreateddate = objODBCDatareader["created_date"].ToString();
                    }
                    objODBCDatareader.Close();
                    sub = "Buyer ";
                    body = "Dear Sir/Madam,  <br />";
                    body = body + "<br />";
                    body = body + "Greetings,  <br />";
                    body = body + "<br />";
                    body = body + "A New Buyer is Created By " + HttpUtility.HtmlEncode(lscreatedby)+ "  On:" + lscreateddate + "<br />";
                    body = body + "<br />";
                    body = body + "<b>Thanks & Regards, </b> ";
                    body = body + "<br />";
                    body = body + "<b> Team Buyer ";
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
                    smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    //smtp.Send(message);
                    values.status = true;
                    return true;
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
                values.status = false;
                values.message = "Error Occured While Updating Buyer";
                return false;
            }

        }


        public void DaGetbuyerSummary(string employee_gid, MdlMstbuyer values)
        {
            msSQL = " select buyer_gid,buyer_code,buyer_name,date_format(created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                    " case when creditActive_status='N' then 'Inactive' else 'Active' end as creditActive_status," +
                    " case when credit_status='NA' then 'Not yet Completed' when credit_status='Pending' then 'Approval Pending' else 'Completed' end as credit_status," +
                    " case when creditstatus_Approval='Y' then 'Approved' when creditstatus_Approval='N' then 'Non Approved' else 'NA' end as creditstatus_Approval" +
                    " from ocs_mst_tbuyer where created_by='"+ employee_gid + "' order by buyer_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getbuyer_list = new List<buyer_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getbuyer_list.Add(new buyer_list
                    {
                        buyer_gid = (dr_datarow["buyer_gid"].ToString()),
                        buyer_code = (dr_datarow["buyer_code"].ToString()),
                        buyer_name = (dr_datarow["buyer_name"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        creditActive_status = (dr_datarow["creditActive_status"].ToString()),
                        credit_status = (dr_datarow["credit_status"].ToString()),
                        creditstatus_Approval = (dr_datarow["creditstatus_Approval"].ToString())
                    });
                }
                values.buyer_list = getbuyer_list;
            }
            dt_datatable.Dispose();
        }

        public void DaDeletebuyer(string buyer_gid, MdlMstbuyer values)
        {
            msSQL = "delete from ocs_mst_tbuyer where buyer_gid='" + buyer_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            
            if (mnResult != 0)
            {
                values.message = "buyer Details Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured While Deleting buyer Details";
                values.status = false;

            }
        }

        public bool DaPostMobileNo(string employee_gid, MdlbuyerMobileNo values)
        {

            msSQL = "select primary_mobileno " + " from ocs_mst_tbuyer2mobileno where buyer_gid='" + employee_gid + "' or buyer_gid='" + values.buyer_gid + "' and primary_mobileno='Yes'";
            string lsprimary_mobileno = objdbconn.GetExecuteScalar(msSQL);
            if (lsprimary_mobileno == (values.primary_mobileno))
            {
                values.status = false;
                values.message = "Already Primary Mobile Number Added";
                objdbconn.CloseConn();
                return false;
            }

            msSQL = "select mobile_no from ocs_mst_tbuyer2mobileno where buyer_gid='" + employee_gid + "' or buyer_gid='" + values.buyer_gid + "' and mobile_no ='" + values.mobile_no + "'";
            string mobile_no = objdbconn.GetExecuteScalar(msSQL);
            if (mobile_no == (values.mobile_no))
            {
                values.status = false;
                values.message = "Already This Mobile Number Added";
                objdbconn.CloseConn();
                return false;
            }

            msGetGid = objcmnfunctions.GetMasterGID("B2MN");
            msSQL = " insert into ocs_mst_tbuyer2mobileno(" +
                    " buyer2mobileno_gid," +
                    " buyer_gid," +
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
                objdbconn.CloseConn();
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured While Adding Mobile Number";
                objdbconn.CloseConn();
                return false;
            }
        }

        public void DaGetMobileNoList(string employee_gid, MdlbuyerMobileNo values)
        {
            msSQL = "select mobile_no,buyer2mobileno_gid,primary_mobileno,whatsapp_mobileno from ocs_mst_tbuyer2mobileno where " +
              " buyer_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getbuyermobileno_list = new List<buyermobileno_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getbuyermobileno_list.Add(new buyermobileno_list
                    {
                        buyer2mobileno_gid = (dr_datarow["buyer2mobileno_gid"].ToString()),
                        mobile_no = (dr_datarow["mobile_no"].ToString()),
                        primary_mobileno = (dr_datarow["primary_mobileno"].ToString()),
                        whatsapp_mobileno = (dr_datarow["whatsapp_mobileno"].ToString()),
                    });
                }
                values.buyermobileno_list = getbuyermobileno_list;
            }
            dt_datatable.Dispose();
        }

        public void DaDeleteMobileNo(string buyer2mobileno_gid, MdlbuyerMobileNo values)
        {
            msSQL = "delete from ocs_mst_tbuyer2mobileno where buyer2mobileno_gid='" + buyer2mobileno_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "Mobile Number Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured While Deleting The Mobile Number";
                values.status = false;

            }
        }

        public bool DaPostEmailAddress(string employee_gid, MdlbuyerEmailAddress values)
        {
             msSQL = "select primary_emailaddress " + " from ocs_mst_tbuyer2emailaddress where buyer_gid='" + employee_gid + "' or buyer_gid='" + values.buyer_gid + "' and primary_emailaddress='Yes'";
            string lsprimary_emailaddress = objdbconn.GetExecuteScalar(msSQL);
            
            if (lsprimary_emailaddress == (values.primary_emailaddress))
            {
                values.status = false;
                values.message = "Already Primary Email Address Added";
                objdbconn.CloseConn();
                return false;
            }
            msSQL = "select email_address from ocs_mst_tbuyer2emailaddress where buyer_gid='" + employee_gid + "' or buyer_gid='" + values.buyer_gid + "' and email_address='" + values.email_address + "'";
            string lsemail_address = objdbconn.GetExecuteScalar(msSQL);
            if (lsemail_address == (values.email_address))
            {
                values.status = false;
                values.message = "Already This Email Address Added";
                objdbconn.CloseConn();
                return false;
            }
            msGetGid = objcmnfunctions.GetMasterGID("B2EA");
            msSQL = " insert into ocs_mst_tbuyer2emailaddress(" +
                    " buyer2emailaddress_gid," +
                    " buyer_gid," +
                    " email_address," +
                    " primary_emailaddress," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + values.email_address + "'," +
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
                values.message = "Error Occured While Adding Email Address";
                return false;
            }
        }

        public void DaGetEmailAddressList(string employee_gid, MdlbuyerEmailAddress values)
        {
            msSQL = "select email_address,buyer2emailaddress_gid,primary_emailaddress from ocs_mst_tbuyer2emailaddress where " +
              " buyer_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getbuyeremailaddress_list = new List<buyeremailaddress_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getbuyeremailaddress_list.Add(new buyeremailaddress_list
                    {
                        buyer2emailaddress_gid = (dr_datarow["buyer2emailaddress_gid"].ToString()),
                        email_address = (dr_datarow["email_address"].ToString()),
                        primary_emailaddress = (dr_datarow["primary_emailaddress"].ToString())
                    });
                }
                values.buyeremailaddress_list = getbuyeremailaddress_list;
            }
            dt_datatable.Dispose();
        }

        public void DaDeleteEmailAddress(string buyer2emailaddress_gid, MdlbuyerEmailAddress values)
        {
            msSQL = "delete from ocs_mst_tbuyer2emailaddress where buyer2emailaddress_gid='" + buyer2emailaddress_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "Email Address deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured While deleting the email address";
                values.status = false;

            }
        }

        public bool DaPostAddress(string employee_gid, MdlbuyerAddress values)
        {
            msSQL = "select primary_address from ocs_mst_tbuyer2address where primary_address='Yes' and buyer_gid='" + employee_gid + "'";
            string lsprimary_address = objdbconn.GetExecuteScalar(msSQL);
            if (lsprimary_address == (values.primary_address))
            {
                values.status = false;
                values.message = "Already Primary Address Added";
                return false;
            }

            msGetGid = objcmnfunctions.GetMasterGID("B2AD");
            msSQL = " insert into ocs_mst_tbuyer2address(" +
                    " buyer2address_gid," +
                    " buyer_gid," +
                    " addresstype_gid," +
                    " addresstype_name," +
                    " addressline1," +
                    " addressline2," +
                    " primary_address," +
                    " landmark," +
                    " postal_code," +
                    " city," +
                    " taluka," +
                    " district," +
                    " state_name," +
                    " country," +
                    " latitude," +
                    " longitude," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + values.addresstype_gid + "'," +
                    "'" + values.addresstype_name + "'," +
                    "'" + values.addressline1 + "'," +
                    "'" + values.addressline2 + "'," +
                    "'" + values.primary_address + "'," +
                    "'" + values.landmark + "'," +
                    "'" + values.postal_code + "'," +
                    "'" + values.city + "'," +
                    "'" + values.taluka + "'," +
                    "'" + values.district + "'," +
                    "'" + values.state_name + "'," +
                    "'" + values.country + "'," +
                    "'" + values.latitude + "'," +
                    "'" + values.longitude + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                values.status = true;
                values.message = "Address Details Added Sucessfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured While Adding Address";
                return false;
            }

        }

        public void DaGetAddressList(string employee_gid, MdlbuyerAddress values)
        {
            msSQL = " select buyer2address_gid,addresstype_name,primary_address, addressline1, addressline2, taluka, district, state_name, country, latitude, longitude," +
                    " postal_code from ocs_mst_tbuyer2address where buyer_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getbuyeraddress_list = new List<buyeraddress_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getbuyeraddress_list.Add(new buyeraddress_list
                    {
                        buyer2address_gid = (dr_datarow["buyer2address_gid"].ToString()),
                        addresstype_name = (dr_datarow["addresstype_name"].ToString()),
                        primary_address = (dr_datarow["primary_address"].ToString()),
                        addressline1 = (dr_datarow["addressline1"].ToString()),
                        addressline2 = (dr_datarow["addressline2"].ToString()),
                        taluka = (dr_datarow["taluka"].ToString()),
                        district = (dr_datarow["district"].ToString()),
                        state_name = (dr_datarow["state_name"].ToString()),
                        country = (dr_datarow["country"].ToString()),
                        latitude = (dr_datarow["latitude"].ToString()),
                        longitude = (dr_datarow["longitude"].ToString()),
                        postal_code = (dr_datarow["postal_code"].ToString())
                        
                    });
                }
                values.buyeraddress_list = getbuyeraddress_list;
            }
            dt_datatable.Dispose();
        }

        public void DaDeleteAddress(string buyer2address_gid, MdlbuyerAddress values)
        {
            msSQL = "delete from ocs_mst_tbuyer2address where buyer2address_gid='" + buyer2address_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "Address Details Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured While Deleting The Address";
                values.status = false;

            }
        }

        public bool DaPostBank(string employee_gid, MdlbuyerBank values)
        {

            msGetGid = objcmnfunctions.GetMasterGID("B2BK");
            msSQL = " insert into ocs_mst_tbuyer2bank(" +
                    " buyer2bank_gid," +
                    " buyer_gid," +
                    " bank_name," +
                    " branch_name," +
                    " bank_address," +
                    " micr_code," +
                    " ifsc_code," +
                    " bankaccount_name," +
                    " bankaccountlevel_gid," +
                    " bankaccountlevel_name," +
                    " bankaccounttype_gid," +
                    " bankaccounttype_name," +
                    " bankaccount_number," +
                    " confirmbankaccountnumber," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                     "'" + values.bank_name + "'," +
                    "'" + values.branch_name + "'," +
                    "'" + values.bank_address + "'," +
                    "'" + values.micr_code + "'," +
                    "'" + values.ifsc_code + "'," +
                    "'" + values.bankaccount_name + "'," +
                    "'" + values.bankaccountlevel_gid + "'," +
                    "'" + values.bankaccountlevel_name + "'," +
                    "'" + values.bankaccounttype_gid + "'," +
                    "'" + values.bankaccounttype_name + "'," +
                    "'" + values.bankaccount_number + "'," +
                    "'" + values.confirmbankaccountnumber + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                values.status = true;
                values.message = "Bank Details Added Successfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured While Adding Bank Details";
                return false;
            }

        }

        public void DaGetBankList(string employee_gid, MdlbuyerBank values)
        {
            msSQL = "select buyer2bank_gid,bank_name,branch_name,bank_address,micr_code,ifsc_code,bankaccount_name, bankaccount_number, bankaccountlevel_name,bankaccounttype_name from ocs_mst_tbuyer2bank where " +
              " buyer_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getbuyerbank_list = new List<buyerbank_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getbuyerbank_list.Add(new buyerbank_list
                    {
                        buyer2bank_gid = (dr_datarow["buyer2bank_gid"].ToString()),
                        bank_name = (dr_datarow["bank_name"].ToString()),
                        branch_name = (dr_datarow["branch_name"].ToString()),
                        bank_address = (dr_datarow["bank_address"].ToString()),
                        micr_code = (dr_datarow["micr_code"].ToString()),
                        ifsc_code = (dr_datarow["ifsc_code"].ToString()),
                        bankaccount_name = (dr_datarow["bankaccount_name"].ToString()),
                        bankaccount_number = (dr_datarow["bankaccount_number"].ToString()),
                        bankaccountlevel_name = (dr_datarow["bankaccountlevel_name"].ToString()),
                        bankaccounttype_name = (dr_datarow["bankaccounttype_name"].ToString()),
                    });
                }
                values.buyerbank_list = getbuyerbank_list;
            }
            dt_datatable.Dispose();
        }

        public void DaDeleteBank(string buyer2bank_gid, MdlbuyerBank values)
        {
            msSQL = "delete from ocs_mst_tbuyer2bank where buyer2bank_gid='" + buyer2bank_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "Bank Details Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured While Deleting The Bank Details";
                values.status = false;

            }
        }

        public bool DaPostGST(string employee_gid, MdlbuyerGST values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("B2GS");
            msSQL = " insert into ocs_mst_tbuyer2gst(" +
                    " buyer2gst_gid," +
                    " buyer_gid," +
                    " gststate_name," +
                    " gst_no," +
                    " gstregister_status," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + values.gststate_name + "'," +
                    "'" + values.gst_no + "'," +
                    "'" + values.gstregister_status + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "GST details Added Successfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured While Adding GST Details";
                return false;
            }

        }

        public bool DaPostGSTList(string employee_gid, MdlbuyerGST values)
        {
            BuyerGSTDetails[] GstArray = values.GSTArray;
            string GSTValue, GSTStateCode, GSTState;

            for (int i = 0; i < GstArray.Length; i++)
            {
                GSTValue = GstArray[i].gstinId;
                GSTStateCode = GSTValue.Substring(0, 2);

                msSQL = "select gst_state from ocs_mst_tgstcode2state where " +
                       " gst_code='" + GSTStateCode + "'";
                GSTState = objdbconn.GetExecuteScalar(msSQL);

                msGetGid = objcmnfunctions.GetMasterGID("B2GS");
                msSQL = " insert into ocs_mst_tbuyer2gst(" +
                        " buyer2gst_gid," +
                        " buyer_gid," +
                        " gststate_name," +
                        " gst_no," +
                        " gstregister_status," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + employee_gid + "'," +
                        "'" + GSTState + "'," +
                        "'" + GSTValue + "'," +
                        "'" + "Yes" + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
           

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "GST details Added Successfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured While Adding GST Details";
                return false;
            }

        }

        public void DaGetGSTList(string employee_gid, MdlbuyerGST values)
        {
            msSQL = "select buyer2gst_gid,gststate_name,gst_no,gstregister_status from ocs_mst_tbuyer2gst where " +
              " buyer_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getbuyergst_list = new List<buyergst_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getbuyergst_list.Add(new buyergst_list
                    {
                        buyer2gst_gid = (dr_datarow["buyer2gst_gid"].ToString()),
                        gststate_name = (dr_datarow["gststate_name"].ToString()),
                        gst_no = (dr_datarow["gst_no"].ToString()),
                        gstregister_status = (dr_datarow["gstregister_status"].ToString())
                    });
                }
                values.buyergst_list = getbuyergst_list;
            }
            dt_datatable.Dispose();
        }

        public void DaDeleteGST(string buyer2gst_gid, MdlbuyerGST values)
        {
            msSQL = "delete from ocs_mst_tbuyer2gst where buyer2gst_gid='" + buyer2gst_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.message = "GST Details Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured While Deleting The Gst Details";
                values.status = false;

            }
        }

        public void DaDeleteGSTBuyer(string employee_gid, string buyer_gid, MdlbuyerGST values)
        {
            msSQL = "select buyer2gst_gid from ocs_mst_tbuyer2gst where buyer_gid='" + employee_gid + "' or buyer_gid='" + buyer_gid + "'";
                        
            dt_datatable = objdbconn.GetDataTable(msSQL);
            string buyer2gst_gid;
            foreach (DataRow dr_datarow in dt_datatable.Rows)
            {
                buyer2gst_gid = (dr_datarow["buyer2gst_gid"].ToString());
                msSQL = "delete from ocs_mst_tbuyer2gst where buyer2gst_gid='" + buyer2gst_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            dt_datatable.Dispose();

            if (mnResult != 0)
            {
                values.message = "GST Details Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured While Deleting The Gst Details";
                values.status = false;

            }
        }

        public void DaGetBankAccountLevel(MdlMstbuyer objMdlMstbuyer)
        {
            try
            {
                msSQL = " SELECT bankaccountlevel_gid,bankaccountlevel_name FROM ocs_mst_tbankaccountlevel where status='Y'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getBankAccountLevel = new List<bankaccountlevel_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getBankAccountLevel.Add(new bankaccountlevel_list
                        {
                            bankaccountlevel_gid = (dr_datarow["bankaccountlevel_gid"].ToString()),
                            bankaccountlevel_name = (dr_datarow["bankaccountlevel_name"].ToString()),
                        });
                    }
                    objMdlMstbuyer.bankaccountlevel_list = getBankAccountLevel;
                }
                dt_datatable.Dispose();

                objMdlMstbuyer.status = true;
            }
            catch
            {
                objMdlMstbuyer.status = false;
            }

        }

        public void DaGetYearsAndMonthsInBusiness(string businessstart_date, MdlMstbuyer values)
        {
            try
            {
                if (businessstart_date == "" || businessstart_date == null)
                {

                }
                else
                {

                    var date = DateTime.Parse(new string(businessstart_date.Take(24).ToArray()));
                    var businessstartdate = date.ToString("yyyy/MM/dd");

                    msSQL = "select TIMESTAMPDIFF( YEAR, ('" + businessstartdate + "'), now() ) as year";
                    values.year_business = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = "select TIMESTAMPDIFF( MONTH, ('" + businessstartdate + "'), now() ) % 12 as month";
                    values.month_business = objdbconn.GetExecuteScalar(msSQL);
                    values.status = true;




                }
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Kindly Select Valid Date...";
            }
        }





        public void DaGetPostalCodeDetails(string postal_code, MdlbuyerAddress objMdlbuyerAddress)
        {
            try
            {
                msSQL = "select city,taluka,district, state from ocs_mst_tpostalcode where " +
                        " postalcode_value='" + postal_code + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        objMdlbuyerAddress.city = (dr_datarow["city"].ToString());
                        objMdlbuyerAddress.taluka = (dr_datarow["taluka"].ToString());
                        objMdlbuyerAddress.district = (dr_datarow["district"].ToString());
                        objMdlbuyerAddress.state_name = (dr_datarow["state"].ToString());
                    }

                }
                dt_datatable.Dispose();

                objMdlbuyerAddress.status = true;
            }
            catch
            {
                objMdlbuyerAddress.status = false;
            }

        }

        public void DaGetbuyerTempClear(string employee_gid, result values)
        {
            msSQL = "delete from ocs_mst_tbuyer2mobileno where buyer_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from ocs_mst_tbuyer2emailaddress where buyer_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from ocs_mst_tbuyer2address where buyer_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from ocs_mst_tbuyer2bank where buyer_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from ocs_mst_tbuyer2gst where buyer_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            values.status = true;
        }

        public void DaGetBankAccountType(MdlMstbuyer objMdlMstbuyer)
        {
            try
            {
                msSQL = " SELECT bankaccounttype_gid,bankaccounttype_name FROM ocs_mst_tbankaccounttype where status='Y'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getBankAccountType = new List<bankaccounttype_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getBankAccountType.Add(new bankaccounttype_list
                        {
                            bankaccounttype_gid = (dr_datarow["bankaccounttype_gid"].ToString()),
                            bankaccounttype_name = (dr_datarow["bankaccounttype_name"].ToString()),
                        });
                    }
                    objMdlMstbuyer.bankaccounttype_list = getBankAccountType;
                }
                dt_datatable.Dispose();

                objMdlMstbuyer.status = true;
            }
            catch
            {
                objMdlMstbuyer.status = false;
            }

        }

    }
}