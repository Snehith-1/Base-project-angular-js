
using ems.mastersamagro.Models;
using ems.utilities.Functions;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Pdf;
using Spire.Pdf.HtmlConverter;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Text.RegularExpressions;
using ems.storage.Functions;
using System.Net;
using System.Net.Mail;

namespace ems.mastersamagro.DataAccess
{
    /// <summary>
    /// This DataAccess  will provide access to product desk flow by posting and getting necessary data for the applications
    /// </summary>
    /// <remarks>Written by Sherin Augusta.A & Logapriya.S </remarks>
    public class DaAgrTrnProductApproval
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        DataTable dt_datatable, dt_child, dt_childindividual, dt_childgroup;
        string msSQL, msGetGid, msGetGid1, msGetGidCC, msGetGid2, msGetGid3;
        int mnResult, mnResult1, mnResult2;
        HttpPostedFile httpPostedFile;
        OdbcDataReader objODBCDataReader, objODBCDataReader1, objODBCDataReader2, objODBCDatareader, objODBCDatareader1;
        string lssanctionref_no, lstemplate_content, lscompany_code, lspath, lsdocument_path, fileName;
        string msGetRef, msGetGID, lsdocument_code, lsdocument_name, lsdocumenttype_name, lscompanydocument_name;
        string lscontent = string.Empty;
        private string body;
        private string sub;
        private int ls_port;
        private string ls_server;
        private string ls_username;
        private string ls_password, lsrmquery_flag, lsunderwriting_flag;
        public string relationshipmanager_name, productmanager_name, productmember_name, query_title,lsassign_remarks, creater_name, customer_name, application_no, Assignedby_name, Assigned_date, Submitted_date;
        public string lsBccmail_id, cc_mailid, tomail_id, lsstatus, lssource;
        public string productmember_mailid, productmanager_mailid, creater_mailid, ls_approvaldate, ls_approval, lspromember_id, lspromanager_id, lsccweb, lsclosed_date, lsclosed_by;
        public string finalapproved_time, cluster_head, zonal_head, creditnationalmanager_mailid, relationshipmanager_mailid, creditregionalmanager_mailid, regionalhead_name, lsoveralllimit_amount, ls_Product, ls_Program, ls_Margin;
        private IEnumerable<string> lsCCReceipients, lsBCCReceipients;

        public void DaGetAppProductPendingAssignmentSummary(string employee_gid, MdlMstApplicationAdd values)
        {
            msSQL = " select a.application_gid,a.application_no,a.customerref_name as customer_name,a.customer_urn,a.vertical_name, a.created_by as createdby," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,a.status,a.applicant_type," +
                    " date_format(a.submitted_date,'%d-%m-%Y %h:%i %p') as updated_date, a.productcharge_flag, a.economical_flag,a.approval_status, " +
                    " a.overalllimit_amount, region,a.productdesk_gid,a.renewal_flag,a.amendment_flag,a.shortclosing_flag, " +
                    " date_format(a.headapproval_date,'%d-%m-%Y %h:%i %p') as headapproval_dated, d.productdesk_name   from agr_mst_tapplication a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.submitted_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                    " left join agr_mst_tproductdeskmapping d on d.productdesk_gid = a.productdesk_gid" +
                    " where a.productdesk_flag='" + getProductAppStatus.InitiatedProductDesk + "' and a.approval_flag='Y' and " +
                    " (a.productdesk_gid in (select productdesk_gid from agr_mst_tproductdeskmapping_manager where employee_gid ='" + employee_gid + "')" +
                    " or a.productdesk_gid in (select productdesk_gid from agr_mst_tproductdeskmapping_member where employee_gid ='" + employee_gid + "'))" +
                    " group by a.application_gid order by a.updated_date desc ";
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
                        created_date = dt["updated_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        economical_flag = dt["economical_flag"].ToString(),
                        productcharge_flag = dt["productcharge_flag"].ToString(),
                        application_status = dt["status"].ToString(),
                        applicant_type = dt["applicant_type"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        region = dt["region"].ToString(),
                        overalllimit_amount = dt["overalllimit_amount"].ToString(),
                        createdby = dt["createdby"].ToString(),
                        productdesk_gid = dt["productdesk_gid"].ToString(),
                        headapproval_date = dt["headapproval_dated"].ToString(),
                        productdesk_name = dt["productdesk_name"].ToString(),
                        renewal_flag = dt["renewal_flag"].ToString(),
                        amendment_flag = dt["amendment_flag"].ToString(),
                        shortclosing_flag = dt["shortclosing_flag"].ToString()
                    });

                }
            }
            values.applicationadd_list = getapplicationadd_list;
            values.status = true;
            dt_datatable.Dispose();
        }

        public void DaAssignProductApplicationCount(string employee_gid, AssignApplicationCount values)
        {
            msSQL = " select count(application_gid) as pending_count from agr_mst_tapplication a where a.productdesk_flag='" + getProductAppStatus.InitiatedProductDesk + "' and " +
                    " a.approval_flag='Y' and creditgroup_status='Pending' and " +
                    " (a.productdesk_gid in (select productdesk_gid from agr_mst_tproductdeskmapping_manager where employee_gid ='" + employee_gid + "')" +
                    " or a.productdesk_gid in (select productdesk_gid from agr_mst_tproductdeskmapping_member where employee_gid ='" + employee_gid + "'))";
            values.pending_count = objdbconn.GetExecuteScalar(msSQL);
            int pending_count = Convert.ToInt16(values.pending_count);

            msSQL = "select count(application_gid) as assigned_count from agr_mst_tapplication a where ( a.productdesk_flag='" + getProductAppStatus.PendingApproval + "' and " +
                    " a.approval_flag='Y') and" +
                    " (a.productdesk_gid in (select productdesk_gid from agr_mst_tproductdeskmapping_manager where employee_gid ='" + employee_gid + "')" +
                    " or a.productdesk_gid in (select productdesk_gid from agr_mst_tproductdeskmapping_member where employee_gid ='" + employee_gid + "'))";
            values.assigned_count = objdbconn.GetExecuteScalar(msSQL);
            int assigned_count = Convert.ToInt16(values.assigned_count);

            int totalcount = pending_count + assigned_count;
            values.lstotalcount = Convert.ToInt16(totalcount);
        }

        public void DaGetProductApprovalManagerMember(string productdesk_gid, MdlProductGroup objvalues)
        {
            msSQL = " select productdesk_gid,productdesk_name from agr_mst_tproductdeskmapping where productdesk_gid='" + productdesk_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objvalues.productdesk_name = objODBCDatareader["productdesk_name"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " select productdeskmanager_gid,employee_gid,employee_name from agr_mst_tproductdeskmapping_manager " +
                    " where productdesk_gid='" + productdesk_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getProductManagerGroup = new List<ProductManagerGroup>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getProductManagerGroup.Add(new ProductManagerGroup
                    {
                        productdeskmanager_gid = dt["productdeskmanager_gid"].ToString(),
                        employee_gid = dt["employee_gid"].ToString(),
                        employee_name = dt["employee_name"].ToString(),
                    });
                    objvalues.ProductManagerGroup = getProductManagerGroup;
                }
            }
            dt_datatable.Dispose();

            msSQL = " select productdeskmember_gid,employee_gid,employee_name from agr_mst_tproductdeskmapping_member " +
                    " where productdesk_gid='" + productdesk_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getProductMemberGroup = new List<ProductMemberGroup>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getProductMemberGroup.Add(new ProductMemberGroup
                    {
                        productdeskmember_gid = dt["productdeskmember_gid"].ToString(),
                        employee_gid = dt["employee_gid"].ToString(),
                        employee_name = dt["employee_name"].ToString(),
                    });
                    objvalues.ProductMemberGroup = getProductMemberGroup;
                }
            }
            dt_datatable.Dispose();
            objvalues.status = true;
        }

        public bool DaPostProductAssign(string employee_gid, Mdlproductdeskassign values)
        {
            string lsproducts_gid = "", lsproducts_name = "";
            msSQL = " select products_gid,products_name from agr_mst_tproductdeskmapping where productdesk_gid='" + values.productdesk_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsproducts_gid = objODBCDatareader["products_gid"].ToString();
                lsproducts_name = objODBCDatareader["products_name"].ToString();
            }

            if (values.assign_remarks == null || values.assign_remarks == "")
            {
                lsassign_remarks = "";
            }
            else
            {
                lsassign_remarks = values.assign_remarks.Replace("'", "");
            }

            objODBCDatareader.Close();
            msGetGid = objcmnfunctions.GetMasterGID("APAG");
            msSQL = "Insert into agr_trn_tappproductapproval( " +
                   " appproductapproval_gid, " +
                   " application_gid," +
                   " productdesk_gid," +
                   " productdesk_name, " +
                   " product_gid," +
                   " product_name," +
                   " product_managergid," +
                   " product_managername," +
                   //" productmanager_approvalflag," +
                   " product_membergid, " +
                   " product_membername, " +
                   //" productmember_approvalflag, " +
                   " assign_remarks," +
                   " created_by," +
                   " created_date)" +
                   " values(" +
                   "'" + msGetGid + "'," +
                   "'" + values.application_gid + "'," +
                   "'" + values.productdesk_gid + "'," +
                   "'" + values.productdesk_name + "'," +
                   "'" + lsproducts_gid + "'," +
                   "'" + lsproducts_name + "'," +
                   "'" + values.product_managergid + "'," +
                   "'" + values.product_managername + "'," +
                   //"'" + values.productmanager_approvalflag + "'," +
                   "'" + values.product_membergid + "'," +
                   "'" + values.product_membername + "'," +
                   //"'" + values.productmember_approvalflag + "'," +
                   //"'" + values.assign_remarks.Replace("'", "").ToString() + "'," +
                   "'" + lsassign_remarks + "'," +
                   "'" + employee_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msSQL = " update agr_mst_tapplication set productdesk_flag='" + getProductAppStatus.PendingApproval + "'" +
                        " where application_gid='" + values.application_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                try
                {

                        msSQL = "select application_no from agr_mst_tapplication where application_gid='" + values.application_gid + "'";
                        application_no = objdbconn.GetExecuteScalar(msSQL);
                        msSQL = "select customerref_name from agr_mst_tapplication where application_gid='" + values.application_gid + "'";
                        customer_name = objdbconn.GetExecuteScalar(msSQL);
                        msSQL = "select date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date from agr_trn_tappproductapproval a  where a.application_gid='" + values.application_gid + "'";
                        Assigned_date = objdbconn.GetExecuteScalar(msSQL);
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

                    msSQL = "select group_concat(c.employee_emailid) from agr_trn_tappproductapproval a left join hrm_mst_temployee c on c.employee_gid = a.product_membergid  where a.application_gid='" + values.application_gid + "'";
                    tomail_id = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = "select group_concat(c.employee_emailid) from agr_mst_tapplication a left join ocs_mst_tcreditr2regionalmanager b on a.creditgroup_gid = b.creditmapping_gid left join hrm_mst_temployee c on c.employee_gid = b.employee_gid where a.application_gid = '" + values.application_gid + "'";
                        creditregionalmanager_mailid = objdbconn.GetExecuteScalar(msSQL);

                        //tomail_id = "" + creditnationalmanager_mailid + "," + creditregionalmanager_mailid + "";

                        msSQL = " select b.employee_emailid from agr_mst_tapplication a left join hrm_mst_temployee b on b.employee_gid = a.relationshipmanager_gid where a.application_gid='" + values.application_gid + "'";
                        relationshipmanager_mailid = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = " select  concat( c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code)  from agr_mst_tapplication a left join hrm_mst_temployee b on b.employee_gid = a.regionalhead_gid left join adm_mst_tuser c on c.user_gid = b.user_gid where a.application_gid='" + values.application_gid + "'";
                        regionalhead_name = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = " select  concat( c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code)  from agr_trn_tappproductapproval a left join hrm_mst_temployee b on b.employee_gid = a.created_by left join adm_mst_tuser c on c.user_gid = b.user_gid where a.application_gid='" + values.application_gid + "'";
                    Assignedby_name = objdbconn.GetExecuteScalar(msSQL);

                    //msSQL = " select  a.overalllimit_amount  from agr_mst_tapplication a  where a.application_gid='" + values.application_gid + "'";
                    //lsoveralllimit_amount = objdbconn.GetExecuteScalar(msSQL);

                    //msSQL = " select  productdesk_name from agr_mst_tproductdeskmapping   where productdesk_gid='" + lsproductdesk_gid + "'";
                    //lsproductdesk_name = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = " select  group_concat(c.employee_emailid)  from agr_trn_tappproductapproval a left join hrm_mst_temployee c on c.employee_gid = a.product_membergid   where a.application_gid='" + values.application_gid + "'";
                    lspromember_id = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = " select  group_concat(c.employee_emailid)  from agr_trn_tappproductapproval a left join hrm_mst_temployee c on c.employee_gid = a.product_managergid   where a.application_gid='" + values.application_gid + "'";
                    lspromanager_id = objdbconn.GetExecuteScalar(msSQL);

                    lsccweb = ConfigurationManager.AppSettings["Samagrostf"].ToString();

                    cc_mailid =  lsccweb + "," + lspromanager_id;



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
                        sub = " Application of " + HttpUtility.HtmlEncode(customer_name)+ " has been assigned to you for assessment ";
                        body = "<style>table, th, td {border: 1px solid black;border-collapse: collapse;}</style>";
                        body = body + "<table style='border-right: 1px solid black;border-top: 1px solid black;border-bottom: 1px solid black;'><tr><td style='border-right-color:white;'>";

                        body = body + " &nbsp &nbsp Dear Sir / Madam, <br />";
                        body = body + "<br />";
                        body = body + "&nbsp &nbsp Greetings! <br />";
                        body = body + "<br />";
                        body = body + "&nbsp &nbsp The below application has been assigned to you. The details are as follows, <br />";
                        body = body + "<br />";
                        body = body + "&nbsp &nbsp <b> Application Number : </b> " + application_no + "  <br />";
                        body = body + "<br />";
                        body = body + "&nbsp &nbsp <b> Customer Name : </b> " + HttpUtility.HtmlEncode(customer_name)+ "  <br />";
                        body = body + "<br />";
                        body = body + "&nbsp &nbsp <b> RM Name : </b> " + HttpUtility.HtmlEncode(relationshipmanager_name)+ "  <br />";
                        body = body + "<br />";
                        body = body + "&nbsp &nbsp <b> Product:</b> " + HttpUtility.HtmlEncode(ls_Product)+ "<br />";
                        body = body + "<br />";
                        body = body + "&nbsp &nbsp <b> Program:</b> " + ls_Program + "<br />";
                        body = body + "<br />";
                        body = body + "&nbsp &nbsp <b> Cluster Head Name : </b> " + HttpUtility.HtmlEncode(cluster_head)+ "  <br />";
                        body = body + "<br />";
                        body = body + "&nbsp &nbsp <b> Regional Head Name:</b> " + HttpUtility.HtmlEncode(regionalhead_name)+ "<br />";
                        body = body + "<br />";
                        body = body + "&nbsp &nbsp <b> Zonal Head Name : </b> " + HttpUtility.HtmlEncode(zonal_head)+ "  <br />";
                        body = body + "<br />";
                        body = body + "&nbsp &nbsp <b> Assigned Date :</b>  " + HttpUtility.HtmlEncode(Assigned_date)+ "  <br />";
                        body = body + "<br />";
                        body = body + "&nbsp &nbsp <b> Assigned By :</b>  " + HttpUtility.HtmlEncode(Assignedby_name)+ "  <br />";
                        body = body + "<br />";
                        body = body + "&nbsp &nbsp Log into " + ConfigurationManager.AppSettings["livedomain_url"].ToString() + " and complete the necessary actions.";
                        body = body + "<br />";
                        body = body + "<br />";
                        body = body + "&nbsp &nbsp **This is an automated e-mail. Please do not reply to this mailbox**";
                        body = body + "<br />";
                        body = body + "<br />";
                        body = body + "</td><td style='margin-left:20px; border-left-color:white;'>&nbsp&nbsp</td></tr></table>";


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
                        //cc_mailid = ConfigurationManager.AppSettings["Samagrostf"].ToString();

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

                
                catch (Exception ex)
                {
                    values.message = ex.ToString();
                    values.status = false;

                }

                values.status = true;
                values.message = "Product Desk Group Assigned successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while Assigning Product Desk Group";
                return false;
            }
        }

        public void DaGetAppProductAssignedAssignmentSummary(string employee_gid, MdlMstApplicationAdd values)
        {
            msSQL = " select a.application_gid,a.application_no,a.customerref_name as customer_name,a.customer_urn,a.vertical_name," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,a.status,a.applicant_type, a.created_by as createdby," +
                    " date_format(a.headapproval_date,'%d-%m-%Y %h:%i %p') as headapproval_date, d.product_managername, d.product_membername, d.productdesk_name," +
                    " date_format(a.submitted_date,'%d-%m-%Y %h:%i %p') as updated_date, a.productcharge_flag, a.economical_flag,a.approval_status,a.creditmanager_name, " +
                    " a.overalllimit_amount, region,a.productdesk_gid,a.renewal_flag,a.amendment_flag,a.shortclosing_flag,  " +
                    " date_format(a.creditassigned_date,'%d-%m-%Y %h:%i %p') as creditassigned_date from agr_mst_tapplication a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.submitted_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                    " left join agr_trn_tappproductapproval d on d.application_gid=a.application_gid " +
                    " where a.productdesk_flag='" + getProductAppStatus.PendingApproval + "' and a.approval_flag='Y' and " +
                    " (a.productdesk_gid in (select productdesk_gid from agr_mst_tproductdeskmapping_manager where employee_gid ='" + employee_gid + "')" +
                    " or a.productdesk_gid in (select productdesk_gid from agr_mst_tproductdeskmapping_member where employee_gid ='" + employee_gid + "'))" +
                    " group by a.application_gid order by a.updated_date desc ";
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
                        created_date = dt["updated_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        createdby = dt["createdby"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        economical_flag = dt["economical_flag"].ToString(),
                        productcharge_flag = dt["productcharge_flag"].ToString(),
                        application_status = dt["status"].ToString(),
                        applicant_type = dt["applicant_type"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        region = dt["region"].ToString(),
                        overalllimit_amount = dt["overalllimit_amount"].ToString(),
                        productdesk_gid = dt["productdesk_gid"].ToString(),
                        creditassigned_date = dt["creditassigned_date"].ToString(),
                        creditassigned_to = dt["creditmanager_name"].ToString(),
                        headapproval_date = dt["headapproval_date"].ToString(),
                        product_managername = dt["product_managername"].ToString(),
                        product_membername = dt["product_membername"].ToString(),
                        productdesk_name = dt["productdesk_name"].ToString(),
                        renewal_flag = dt["renewal_flag"].ToString(),
                        amendment_flag = dt["amendment_flag"].ToString(),
                        shortclosing_flag = dt["shortclosing_flag"].ToString()
                    });

                }
            }
            values.applicationadd_list = getapplicationadd_list;
            values.status = true;
            dt_datatable.Dispose();
        }

        public void DaGetAppProductAprovalinfo(string application_gid, Mdlproductdeskassign values)
        {
            msSQL = " select appproductapproval_gid, product_managergid ,product_managername,productmanager_approvalflag, product_membergid, " +
                    " product_membername, productmember_approvalflag,assign_remarks, productdesk_name " +
                    " from agr_trn_tappproductapproval where application_gid = '" + application_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.appproductapproval_gid = objODBCDatareader["appproductapproval_gid"].ToString();
                values.product_managergid = objODBCDatareader["product_managergid"].ToString();
                values.product_managername = objODBCDatareader["product_managername"].ToString();
                values.productmanager_approvalflag = objODBCDatareader["productmanager_approvalflag"].ToString();
                values.product_membergid = objODBCDatareader["product_membergid"].ToString();
                values.product_membername = objODBCDatareader["product_membername"].ToString();
                values.productmember_approvalflag = objODBCDatareader["productmember_approvalflag"].ToString();
                values.assign_remarks = objODBCDatareader["assign_remarks"].ToString();
                values.productdesk_name = objODBCDatareader["productdesk_name"].ToString();
            }
            values.status = true;
            objODBCDatareader.Close();
        }

        public bool DaGetProductReassignUpdate(string employee_gid, Mdlproductdeskassign values)
        {
            string lsproduct_managergid = "", lsproduct_managername = "", lsproduct_membergid = "", lsproduct_membername = "", lsassign_remarks = "",
                lsappproductapproval_gid = "";
            msSQL = " select appproductapproval_gid, product_managergid ,product_managername,productmanager_approvalflag, product_membergid, " +
                    " product_membername, productmember_approvalflag,assign_remarks " +
                    " from agr_trn_tappproductapproval where application_gid = '" + values.application_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsproduct_managergid = objODBCDatareader["product_managergid"].ToString();
                lsproduct_managername = objODBCDatareader["product_managername"].ToString();
                lsproduct_membergid = objODBCDatareader["product_membergid"].ToString();
                lsproduct_membername = objODBCDatareader["product_membername"].ToString();
                lsassign_remarks = objODBCDatareader["assign_remarks"].ToString();
                lsappproductapproval_gid = objODBCDatareader["appproductapproval_gid"].ToString();
            }

            msSQL = " Insert into agr_trn_tappproductapprovalreassignlog( " +
                    " application_gid, " +
                    " appproductapproval_gid," +
                    " product_managergid," +
                    " product_managername," +
                    " reassignto_product_managergid," +
                    " reassignto_product_managername," +
                    " product_membergid," +
                    " product_membername," +
                    " reassignto_product_membergid," +
                    " reassignto_product_membername," +
                    " assign_remarks," +
                    " reassignto_assign_remarks," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + values.application_gid + "'," +
                    "'" + lsappproductapproval_gid + "',";
            if (lsproduct_managergid == values.product_managergid)
            {
                msSQL += "''," +
                         "''," +
                         "''," +
                         "'',";
            }
            else
            {
                msSQL += "'" + lsproduct_managergid + "'," +
                         "'" + lsproduct_managername + "'," +
                         "'" + values.product_managergid + "'," +
                         "'" + values.product_managername + "',";
            }
            if (lsproduct_membergid == values.product_membergid)
            {
                msSQL += "''," +
                        "''," +
                        "''," +
                        "'',";
            }
            else
            {
                msSQL += "'" + lsproduct_membergid + "'," +
                         "'" + lsproduct_membername + "'," +
                         "'" + values.product_membergid + "'," +
                         "'" + values.product_membername + "',";
            }
            msSQL += "'" + lsassign_remarks.Replace("'", "") + "'," +
                    "'" + values.assign_remarks.Replace("'", "") + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                msSQL = " update agr_trn_tappproductapproval set " +
                       " product_managergid='" + values.product_managergid + "'," +
                       " product_managername='" + values.product_managername + "'," +
                       " assign_remarks='" + values.assign_remarks.Replace("'", "") + "'" +
                       " where application_gid='" + values.application_gid + "' and productmanager_approvalflag='N'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update agr_trn_tappproductapproval set " +
                        " product_membergid='" + values.product_membergid + "'," +
                        " product_membername ='" + values.product_membername + "'," +
                        " assign_remarks='" + values.assign_remarks.Replace("'", "") + "'" +
                        " where application_gid='" + values.application_gid + "' and productmember_approvalflag='N'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                values.status = true;
                values.message = "Product Approval Reassigning successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while Reassigning Product Approval";
                return false;
            }
        }

        public void DaGetProductReassignedLog(string application_gid, MdlProductreassignedlogInfo values)
        {
            msSQL = " SELECT a.application_gid,a.product_managergid ,a.product_managername,a.reassignto_product_managergid,a.reassignto_product_managername, " +
                      " a.product_membergid,a.product_membername,a.reassignto_product_membergid,a.reassignto_product_membername , " +
                      " a.assign_remarks,a.reassignto_assign_remarks,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                      " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by " +
                      " FROM agr_trn_tappproductapprovalreassignlog a" +
                      " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                      " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                      " where application_gid = '" + application_gid + "' order by a.created_date asc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getreassignedlog = new List<Productreassignedloglist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getreassignedlog.Add(new Productreassignedloglist
                    {
                        application_gid = (dr_datarow["application_gid"].ToString()),
                        product_managergid = (dr_datarow["product_managergid"].ToString()),
                        product_managername = (dr_datarow["product_managername"].ToString()),
                        reassignto_product_managergid = (dr_datarow["reassignto_product_managergid"].ToString()),
                        reassignto_product_managername = (dr_datarow["reassignto_product_managername"].ToString()),

                        product_membergid = (dr_datarow["product_membergid"].ToString()),
                        product_membername = (dr_datarow["product_membername"].ToString()),
                        reassignto_product_membergid = (dr_datarow["reassignto_product_membergid"].ToString()),
                        reassignto_product_membername = (dr_datarow["reassignto_product_membername"].ToString()),

                        remarks = (dr_datarow["assign_remarks"].ToString()),
                        reassign_remarks = (dr_datarow["reassignto_assign_remarks"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),

                    });
                }
                values.Productreassignedloglist = getreassignedlog;
            }
            dt_datatable.Dispose();
            values.status = true;
        }

        public void DaGetMyAppProductAssignedSummary(string employee_gid, MdlMstProductApproval values)
        {
            msSQL = " select a.application_gid,a.application_no,a.customerref_name as customer_name,a.customer_urn,a.vertical_name," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,a.status,a.applicant_type, a.created_by as createdby, " +
                    " date_format(a.submitted_date,'%d-%m-%Y %h:%i %p') as updated_date,a.approval_status, d.productdesk_name," +
                    " region,a.productdesk_gid,appproductapproval_gid,a.renewal_flag,a.amendment_flag,a.shortclosing_flag from agr_mst_tapplication a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.submitted_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                    " left join agr_trn_tappproductapproval d on a.application_gid=d.application_gid " +
                    " where a.productdesk_flag='" + getProductAppStatus.PendingApproval + "' and d.productmember_approvalflag='N' " +
                    " and d.product_membergid='" + employee_gid + "' group by a.application_gid order by a.updated_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicaition_list = new List<applicationProduct_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getapplicaition_list.Add(new applicationProduct_list
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical_name = dt["vertical_name"].ToString(),
                        created_date = dt["updated_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        application_status = dt["status"].ToString(),
                        applicant_type = dt["applicant_type"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        createdby = dt["createdby"].ToString(),
                        region = dt["region"].ToString(),
                        productdesk_gid = dt["productdesk_gid"].ToString(),
                        appproductapproval_gid = dt["appproductapproval_gid"].ToString(),
                        productdesk_name = dt["productdesk_name"].ToString(),
                        renewal_flag = dt["renewal_flag"].ToString(),
                        amendment_flag = dt["amendment_flag"].ToString(),
                        shortclosing_flag = dt["shortclosing_flag"].ToString()
                    });

                }
            }
            values.applicationProduct_list = getapplicaition_list;
            dt_datatable.Dispose();
        }

        public void DaMyApplicationProductCount(string employee_gid, MdlProductApplicationCount values)
        {

            msSQL = " select count(a.application_gid) as newcount from agr_mst_tapplication a " +
                     " left join agr_trn_tappproductapproval b on a.application_gid = b.application_gid" +
                     " where a.productdesk_flag='" + getProductAppStatus.PendingApproval + "' and b.productmember_approvalflag='N' " +
                     " and b.product_membergid='" + employee_gid + "'";
            values.newapplication_count = objdbconn.GetExecuteScalar(msSQL);
            int newapplicationcount = Convert.ToInt16(values.newapplication_count);

            msSQL = " select count(a.application_gid) as newcount from agr_mst_tapplication a " +
                     " left join agr_trn_tappproductapproval b on a.application_gid = b.application_gid" +
                     " where a.productdesk_flag in ('" + getProductAppStatus.PendingApproval + "','" + getProductAppStatus.Completed + "') " +
                     " and b.productmember_approvalflag='Y' and b.product_membergid='" + employee_gid + "'";
            //" and b.productmember_approvalflag='Y' ";

            values.approvedapplication_count = objdbconn.GetExecuteScalar(msSQL);
            int approvedcount = Convert.ToInt16(values.approvedapplication_count);

            msSQL = " select count(a.application_gid) as newcount from agr_mst_tapplication a " +
                    " left join agr_trn_tappproductapproval b on a.application_gid = b.application_gid" +
                    " where a.productdesk_flag in ('" + getProductAppStatus.Rejected + "','" + getProductAppStatus.Holded + "')  ";
                    //"and b.product_membergid='" + employee_gid + "'";
            values.rejectholdapplication_count = objdbconn.GetExecuteScalar(msSQL);
            int rejectholdcount = Convert.ToInt16(values.rejectholdapplication_count);

            int lstotal = newapplicationcount + rejectholdcount + approvedcount;
            values.lstotalcount = Convert.ToInt16(lstotal);
        }


        public void DaGetappproductmemberapproval(string appproductapproval_gid, string application_gid, string employee_gid, result values)
        {
            try
            {

                msSQL = " update agr_trn_tappproductapproval set productmember_approvalflag='Y'," +
                        " productmember_approvaldate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                        " where appproductapproval_gid='" + appproductapproval_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    msSQL = " update agr_mst_tapplication set approval_status='Submitted to Product Approval', " +
                            " productdesk_flag='" + getProductAppStatus.PendingApproval + "'" +
                            " where application_gid='" + application_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                    try
                    {

                        msSQL = "select application_no from agr_mst_tapplication where application_gid='" + application_gid + "'";
                        application_no = objdbconn.GetExecuteScalar(msSQL);
                        msSQL = "select customerref_name from agr_mst_tapplication where application_gid='" + application_gid + "'";
                        customer_name = objdbconn.GetExecuteScalar(msSQL);
                        msSQL = "select date_format(a.productmember_approvaldate, '%d-%m-%Y %h:%i %p') as productmember_approvaldate from agr_trn_tappproductapproval a  where a.application_gid='" + application_gid + "'";
                        Submitted_date = objdbconn.GetExecuteScalar(msSQL);
                        msSQL = " select clustermanager_name,zonalhead_name,relationshipmanager_name from agr_mst_tapplication where application_gid = '" + application_gid + "'";
                        objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader1.HasRows == true)
                        {
                            cluster_head = objODBCDatareader1["clustermanager_name"].ToString();
                            zonal_head = objODBCDatareader1["zonalhead_name"].ToString();
                            relationshipmanager_name = objODBCDatareader1["relationshipmanager_name"].ToString();
                        }
                        objODBCDatareader1.Close();
                        msSQL = "select group_concat(c.employee_emailid) from agr_mst_tapplication a left join ocs_mst_tcredit2nationalmanager b on a.creditgroup_gid = b.creditmapping_gid left join hrm_mst_temployee c on c.employee_gid = b.employee_gid where a.application_gid = '" + application_gid + "'";
                        creditnationalmanager_mailid = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = "select group_concat(c.employee_emailid) from agr_trn_tappproductapproval a left join hrm_mst_temployee c on c.employee_gid = a.product_managergid  where a.application_gid='" + application_gid + "'";
                        tomail_id = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = "select group_concat(c.employee_emailid) from agr_mst_tapplication a left join ocs_mst_tcreditr2regionalmanager b on a.creditgroup_gid = b.creditmapping_gid left join hrm_mst_temployee c on c.employee_gid = b.employee_gid where a.application_gid = '" + application_gid + "'";
                        creditregionalmanager_mailid = objdbconn.GetExecuteScalar(msSQL);

                        //tomail_id = "" + creditnationalmanager_mailid + "," + creditregionalmanager_mailid + "";

                        msSQL = " select b.employee_emailid from agr_mst_tapplication a left join hrm_mst_temployee b on b.employee_gid = a.relationshipmanager_gid where a.application_gid='" + application_gid + "'";
                        relationshipmanager_mailid = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = " select  concat( c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code)  from agr_mst_tapplication a left join hrm_mst_temployee b on b.employee_gid = a.regionalhead_gid left join adm_mst_tuser c on c.user_gid = b.user_gid where a.application_gid='" + application_gid + "'";
                        regionalhead_name = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = " select  concat( c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code)  from agr_trn_tappproductapproval a left join hrm_mst_temployee b on b.employee_gid = a.product_membergid left join adm_mst_tuser c on c.user_gid = b.user_gid where a.application_gid='" + application_gid + "'";
                        Assignedby_name = objdbconn.GetExecuteScalar(msSQL);

                        //msSQL = " select  a.overalllimit_amount  from agr_mst_tapplication a  where a.application_gid='" + values.application_gid + "'";
                        //lsoveralllimit_amount = objdbconn.GetExecuteScalar(msSQL);

                        //msSQL = " select  productdesk_name from agr_mst_tproductdeskmapping   where productdesk_gid='" + lsproductdesk_gid + "'";
                        //lsproductdesk_name = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = " select  group_concat(c.employee_emailid)  from agr_trn_tappproductapproval a left join hrm_mst_temployee c on c.employee_gid = a.product_membergid   where a.application_gid='" + application_gid + "'";
                        lspromember_id = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = " select  group_concat(c.employee_emailid)  from agr_trn_tappproductapproval a left join hrm_mst_temployee c on c.employee_gid = a.product_managergid   where a.application_gid='" + application_gid + "'";
                        lspromanager_id = objdbconn.GetExecuteScalar(msSQL);

                        lsccweb = ConfigurationManager.AppSettings["Samagrostf"].ToString();

                        cc_mailid = lsccweb + "," + lspromember_id;



                        msSQL = " select  group_concat(product_type) as Product, group_concat(productsub_type) as Program, group_concat(rate_interest) as Margin from agr_mst_tapplication2loan  where application_gid = '" + application_gid + "'";
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
                        sub = " Product Desk Head approval is required for application of " + HttpUtility.HtmlEncode(customer_name) + " ";
                        body = "<style>table, th, td {border: 1px solid black;border-collapse: collapse;}</style>";
                        body = body + "<table style='border-right: 1px solid black;border-top: 1px solid black;border-bottom: 1px solid black;'><tr><td style='border-right-color:white;'>";

                        body = body + " &nbsp &nbsp Dear Sir / Madam, <br />";
                        body = body + "<br />";
                        body = body + "&nbsp &nbsp Greetings! <br />";
                        body = body + "<br />";
                        body = body + "&nbsp &nbsp The below application awaits for your approval. The details are as follows, <br />";
                        body = body + "<br />";
                        body = body + "&nbsp &nbsp <b> Application Number : </b> " + application_no + "  <br />";
                        body = body + "<br />";
                        body = body + "&nbsp &nbsp <b> Customer Name : </b> " + HttpUtility.HtmlEncode(customer_name)+ "  <br />";
                        body = body + "<br />";
                        body = body + "&nbsp &nbsp <b> RM Name : </b> " + HttpUtility.HtmlEncode(relationshipmanager_name)+ "  <br />";
                        body = body + "<br />";
                        body = body + "&nbsp &nbsp <b> Product:</b> " + ls_Product + "<br />";
                        body = body + "<br />";
                        body = body + "&nbsp &nbsp <b> Program:</b> " + ls_Program + "<br />";
                        body = body + "<br />";
                        body = body + "&nbsp &nbsp <b> Cluster Head Name : </b> " + HttpUtility.HtmlEncode(cluster_head)+ "  <br />";
                        body = body + "<br />";
                        body = body + "&nbsp &nbsp <b> Regional Head Name:</b> " + HttpUtility.HtmlEncode(regionalhead_name)+ "<br />";
                        body = body + "<br />";
                        body = body + "&nbsp &nbsp <b> Zonal Head Name : </b> " + HttpUtility.HtmlEncode(zonal_head)+ "  <br />";
                        body = body + "<br />";
                        body = body + "&nbsp &nbsp <b> Submitted Date :</b>  " + Submitted_date + "  <br />";
                        body = body + "<br />";
                        body = body + "&nbsp &nbsp <b> Product Desk Member :</b>  " + HttpUtility.HtmlEncode(Assignedby_name)+ "  <br />";
                        body = body + "<br />";
                        body = body + "&nbsp &nbsp Log into " + ConfigurationManager.AppSettings["livedomain_url"].ToString() + " and complete the necessary actions.";
                        body = body + "<br />";
                        body = body + "<br />";
                        body = body + "&nbsp &nbsp **This is an automated e-mail. Please do not reply to this mailbox**";
                        body = body + "<br />";
                        body = body + "<br />";
                        body = body + "</td><td style='margin-left:20px; border-left-color:white;'>&nbsp&nbsp</td></tr></table>";


                       
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
                        //cc_mailid = ConfigurationManager.AppSettings["Samagrostf"].ToString();

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


                    catch (Exception ex)
                    {
                        values.message = ex.ToString();
                        values.status = false;

                    }


                    values.status = true;
                    values.message = "Submitted to Approval Successfully..!";
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occured..!";
                }

            }
            catch (Exception ex)
            {

            }
        }

        public void DaGetMyAppProductSubmittedSummary(string employee_gid, MdlMstProductApproval values)
        {
            msSQL = " select a.application_gid,a.application_no,a.customerref_name as customer_name,a.customer_urn,a.vertical_name," +
              " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,a.status,a.applicant_type, a.created_by as createdby, " +
              " date_format(a.submitted_date,'%d-%m-%Y %h:%i %p') as updated_date,a.approval_status, d.productdesk_name, d.productmember_approvaldate," +
              " region,a.productdesk_gid,appproductapproval_gid,productquery_status,a.renewal_flag,a.amendment_flag,a.shortclosing_flag from agr_mst_tapplication a" +
              " left join hrm_mst_temployee b on b.employee_gid=a.submitted_by " +
              " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
              " left join agr_trn_tappproductapproval d on a.application_gid=d.application_gid " +
              " where a.productdesk_flag in ('" + getProductAppStatus.PendingApproval + "','" + getProductAppStatus.Completed + "') " +
              " and d.productmember_approvalflag='Y' " +
              " and d.product_membergid='" + employee_gid + "' group by a.application_gid order by a.updated_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicaition_list = new List<applicationProduct_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msSQL = " select application_gid from agr_trn_tapplicationproductdescquery where query_status = 'Open' and application_gid = '" + dt["application_gid"].ToString() + "' " +
                            " and (queryraised_to = 'RM' or queryraised_to = 'Member')";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == false)
                    {
                        lsunderwriting_flag = "Y";
                    }
                    else
                    {
                        lsunderwriting_flag = "N";
                    }
                    objODBCDatareader.Close();
                    getapplicaition_list.Add(new applicationProduct_list
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical_name = dt["vertical_name"].ToString(),
                        created_date = dt["updated_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        application_status = dt["status"].ToString(),
                        applicant_type = dt["applicant_type"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        region = dt["region"].ToString(),
                        productdesk_gid = dt["productdesk_gid"].ToString(),
                        appproductapproval_gid = dt["appproductapproval_gid"].ToString(),
                        productquery_status = dt["productquery_status"].ToString(),
                        createdby = dt["createdby"].ToString(),
                        underwriting_flag = lsunderwriting_flag,
                        productdesk_name = dt["productdesk_name"].ToString(),
                        productmember_approvaldate = dt["productmember_approvaldate"].ToString(),
                        renewal_flag = dt["renewal_flag"].ToString(),
                        amendment_flag = dt["amendment_flag"].ToString(),
                        shortclosing_flag = dt["shortclosing_flag"].ToString()
                    });

                }
            }
            values.applicationProduct_list = getapplicaition_list;
        }

        public void DaGetMyAppProductRejectHoldSummary(string employee_gid, MdlMstProductApproval values)
        {
            msSQL = " select a.application_gid,application_no,customerref_name as customer_name,customer_urn,vertical_name,a.headapproval_status, d.productdesk_name, d.productmanager_approvaldate, d.product_managername," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,a.approval_status,applicant_type,a.created_by as createdby," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,date_format(a.submitted_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                    " a.renewal_flag,a.amendment_flag,a.shortclosing_flag from agr_mst_tapplication a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                    " left join agr_trn_tappproductapproval d on a.application_gid=d.application_gid " +
                    //" where a.application_gid in (select application_gid from agr_trn_tappproductapproval where product_membergid='" + employee_gid + "') " +
                    //" and productdesk_flag in ('" + getProductAppStatus.Rejected + "','" + getProductAppStatus.Holded + "')";
                    "where a.productdesk_flag in ('" + getProductAppStatus.Rejected + "','" + getProductAppStatus.Holded + "')  ";
                    //" and d.product_membergid='" + employee_gid + "'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicaition_list = new List<applicationProduct_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getapplicaition_list.Add(new applicationProduct_list
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical_name = dt["vertical_name"].ToString(),
                        created_date = dt["updated_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        //application_status = dt["status"].ToString(),
                        applicant_type = dt["applicant_type"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        //region = dt["region"].ToString(),
                        productdesk_name = dt["productdesk_name"].ToString(),
                        createdby = dt["createdby"].ToString(),                
                        product_managername = dt["product_managername"].ToString(),
                        productmanager_approvaldate = dt["productmanager_approvaldate"].ToString(),
                        renewal_flag = dt["renewal_flag"].ToString(),
                        amendment_flag = dt["amendment_flag"].ToString(),
                        shortclosing_flag = dt["shortclosing_flag"].ToString()

                    });

                }
            }
            values.applicationProduct_list = getapplicaition_list;
            dt_datatable.Dispose();
            values.status = true;
        }

        public void DaGetMyAppProductApprovalSummary(string employee_gid, MdlMstProductApproval values)
        {
            msSQL = " select a.application_gid,a.application_no,a.customerref_name as customer_name,a.customer_urn,a.vertical_name, d.productdesk_name, " +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,a.status,a.applicant_type, a.created_by as createdby," +
                    " date_format(a.submitted_date,'%d-%m-%Y %h:%i %p') as updated_date,a.approval_status, d.product_membername, d.productmember_approvaldate, " +
                    " region,a.productdesk_gid,appproductapproval_gid,a.renewal_flag,a.amendment_flag,a.shortclosing_flag from agr_mst_tapplication a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.submitted_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                    " left join agr_trn_tappproductapproval d on a.application_gid=d.application_gid " +
                    " where a.productdesk_flag='" + getProductAppStatus.PendingApproval + "' and d.productmember_approvalflag='Y' " +
                    " and d.productmanager_approvalflag='N' and d.product_managergid='" + employee_gid + "' group by a.application_gid order by a.updated_date desc ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicaition_list = new List<applicationProduct_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getapplicaition_list.Add(new applicationProduct_list
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical_name = dt["vertical_name"].ToString(),
                        created_date = dt["updated_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        application_status = dt["status"].ToString(),
                        applicant_type = dt["applicant_type"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        region = dt["region"].ToString(),
                        productdesk_gid = dt["productdesk_gid"].ToString(),
                        appproductapproval_gid = dt["appproductapproval_gid"].ToString(),
                        productdesk_name = dt["productdesk_name"].ToString(),
                        product_membername = dt["product_membername"].ToString(),
                        createdby = dt["createdby"].ToString(),
                        productmember_approvaldate = dt["productmember_approvaldate"].ToString(),
                        renewal_flag = dt["renewal_flag"].ToString(),
                        amendment_flag = dt["amendment_flag"].ToString(),
                        shortclosing_flag = dt["shortclosing_flag"].ToString()

                    });

                }
            }
            values.applicationProduct_list = getapplicaition_list;
            dt_datatable.Dispose();
        }

        public void DaProductApplicationCount(string employee_gid, MdlProductApplicationCount values)
        {

            msSQL = " select count(a.application_gid) as newcount from agr_mst_tapplication a " +
                     " left join agr_trn_tappproductapproval b on a.application_gid = b.application_gid" +
                     " where a.productdesk_flag='" + getProductAppStatus.PendingApproval + "' and b.productmember_approvalflag='Y' " +
                     " and b.productmanager_approvalflag='N' and b.product_managergid='" + employee_gid + "'";
            values.newapplication_count = objdbconn.GetExecuteScalar(msSQL);
            int newapplicationcount = Convert.ToInt16(values.newapplication_count);

            msSQL = " select count(a.application_gid) as newcount from agr_mst_tapplication a " +
                     " left join agr_trn_tappproductapproval b on a.application_gid = b.application_gid" +
                     " where a.productdesk_flag in ('" + getProductAppStatus.PendingApproval + "','" + getProductAppStatus.Completed + "') " +
                    " and b.productmanager_approvalflag='Y'  and b.productmember_approvalflag='Y' and b.product_managergid='" + employee_gid + "'";
            //" and b.productmanager_approvalflag='Y'  and b.productmember_approvalflag='Y' ";

            values.approvedapplication_count = objdbconn.GetExecuteScalar(msSQL);
            int approvedcount = Convert.ToInt16(values.approvedapplication_count);

            msSQL = " select count(a.application_gid) as newcount from agr_mst_tapplication a " +
                    " left join agr_trn_tappproductapproval b on a.application_gid = b.application_gid" +
                    " where a.productdesk_flag in ('" + getProductAppStatus.Rejected + "','" + getProductAppStatus.Holded + "') "+
                    " and b.product_managergid='" + employee_gid + "'";
            values.rejectholdapplication_count = objdbconn.GetExecuteScalar(msSQL);
            int rejectholdcount = Convert.ToInt16(values.rejectholdapplication_count);

            msSQL = " select count(distinct a.application_gid) as newcount from agr_mst_tapplication a " +
                  " left join agr_trn_tappproductapproval b on a.application_gid = b.application_gid" +
                  "  where a.productdesk_flag in ('" + getProductAppStatus.Completed + "') " +
                  " and b.productmanager_approvalflag='Y'  and b.productmember_approvalflag='Y' ";
            values.Advanceapplication_count = objdbconn.GetExecuteScalar(msSQL);

            int advancecount = Convert.ToInt16(values.Advanceapplication_count);

            int lstotal = newapplicationcount + rejectholdcount + approvedcount + advancecount;
            values.lstotalcount = Convert.ToInt16(lstotal);
        }

        public void FnAutoProductApprovalFlow(string lsapplication_gid, string employee_gid, string user_gid)
        {
            string lsproductdesk_gid = "";
            msSQL = " select productdesk_gid from agr_mst_tproductdeskmapping where products_gid in (select producttype_gid from agr_mst_tapplication2loan	" +
                    " where application_gid='" + lsapplication_gid + "') and productdesk_status='Y'";
            lsproductdesk_gid = objdbconn.GetExecuteScalar(msSQL);
            if (lsproductdesk_gid != "")
            {
                msSQL = " update agr_mst_tapplication set productdesk_flag='Y', productdesk_gid ='" + lsproductdesk_gid + "'" +
                         " where application_gid='" + lsapplication_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                string lsproducts_gid = "", lsproducts_name = "", lsproductdesk_name = "", lsproduct_managergid = "";
                string lsproduct_managername = "", lsproduct_membergid = "", lsproduct_membername = "";
                msSQL = " select products_gid,products_name,productdesk_name,b.employee_gid as product_managergid, " +
                   " b.employee_name as product_managername, c.employee_gid as product_membergid, " +
                   " c.employee_name as product_membername from agr_mst_tproductdeskmapping a " +
                   " left join agr_mst_tproductdeskmapping_manager b on a.productdesk_gid = b.productdesk_gid " +
                   " left join agr_mst_tproductdeskmapping_member c on a.productdesk_gid = c.productdesk_gid  " +
                   " where a.productdesk_gid='" + lsproductdesk_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lsproducts_gid = objODBCDatareader["products_gid"].ToString();
                    lsproducts_name = objODBCDatareader["products_name"].ToString();
                    lsproductdesk_name = objODBCDatareader["productdesk_name"].ToString();
                    lsproduct_managergid = objODBCDatareader["product_managergid"].ToString();
                    lsproduct_managername = objODBCDatareader["product_managername"].ToString();
                    lsproduct_membergid = objODBCDatareader["product_membergid"].ToString();
                    lsproduct_membername = objODBCDatareader["product_membername"].ToString();
                }
                objODBCDatareader.Close();

                msGetGid = objcmnfunctions.GetMasterGID("APAG");
                msSQL = "Insert into agr_trn_tappproductapproval( " +
                       " appproductapproval_gid, " +
                       " application_gid," +
                       " productdesk_gid," +
                       " productdesk_name, " +
                       " product_gid," +
                       " product_name," +
                       " product_managergid," +
                       " product_managername," +
                       " product_membergid, " +
                       " product_membername, " +
                       " productmember_approvalflag, " +
                       " productmember_approvaldate, " +
                       " productmanager_approvalflag, " +
                       " productmanager_approvaldate, " +
                       " productmanager_approvalremarks, " +
                       " assign_remarks," +
                       " created_by," +
                       " created_date)" +
                       " values(" +
                       "'" + msGetGid + "'," +
                       "'" + lsapplication_gid + "'," +
                       "'" + lsproductdesk_gid + "'," +
                       "'" + lsproductdesk_name + "'," +
                       "'" + lsproducts_gid + "'," +
                       "'" + lsproducts_name + "'," +
                       "'" + lsproduct_managergid + "'," +
                       "'" + lsproduct_managername + "'," +
                       "'" + lsproduct_membergid + "'," +
                       "'" + lsproduct_membername + "'," +
                       "'Y'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                       "'Y'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                       "'Auto Approval'," +
                       "'Auto Approval'," +
                       "'" + employee_gid + "'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                if (mnResult != 0)
                {
                    msSQL = " update agr_mst_tapplication set approval_status='Submitted to Underwriting', " +
                           " productdesk_flag='" + getProductAppStatus.Completed + "'" +
                           " where application_gid='" + lsapplication_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }

        }

        public void DaGetProductRejectHoldSummary(string employee_gid, MdlMstProductApproval values)
        {
            msSQL = " select a.application_gid,application_no,customerref_name as customer_name,customer_urn,vertical_name,a.headapproval_status, d.productdesk_name, d.productmanager_approvaldate, d.product_managername," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,a.approval_status,applicant_type,a.created_by as createdby, a.approval_status," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,date_format(a.submitted_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                    " a.renewal_flag,a.amendment_flag,a.shortclosing_flag from agr_mst_tapplication a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                    " left join agr_trn_tappproductapproval d on a.application_gid=d.application_gid " +
                    " where a.productdesk_flag in ('" + getProductAppStatus.Rejected + "','" + getProductAppStatus.Holded + "')  " +
                    //" and d.product_managergid='" + employee_gid + "'";
                    " and (d.product_managergid='" + employee_gid + "' or d.product_membergid = '" + employee_gid + "')";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicaition_list = new List<applicationProduct_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getapplicaition_list.Add(new applicationProduct_list
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical_name = dt["vertical_name"].ToString(),
                        created_date = dt["updated_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        //application_status = dt["status"].ToString(),
                        applicant_type = dt["applicant_type"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        //region = dt["region"].ToString(),
                        productdesk_name = dt["productdesk_name"].ToString(),
                        product_managername = dt["product_managername"].ToString(),
                        createdby = dt["createdby"].ToString(),
                        productmanager_approvaldate = dt["productmanager_approvaldate"].ToString(),
                        renewal_flag = dt["renewal_flag"].ToString(),
                        amendment_flag = dt["amendment_flag"].ToString(),
                        shortclosing_flag = dt["shortclosing_flag"].ToString()
                    });

                }
            }
            values.applicationProduct_list = getapplicaition_list;
            dt_datatable.Dispose();
            values.status = true;
        }

        public void DaGetProductApprovedSummary(string employee_gid, MdlMstProductApproval values)
        {
            msSQL = " select a.application_gid,a.application_no,a.customerref_name as customer_name,a.customer_urn,a.vertical_name,d.productdesk_name," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,a.status,a.applicant_type, a.created_by as createdby, " +
                    " date_format(a.submitted_date,'%d-%m-%Y %h:%i %p') as updated_date,a.approval_status, d.product_managername, d.productmanager_approvaldate, " +
                    " region,a.productdesk_gid,appproductapproval_gid, a.renewal_flag,a.amendment_flag,a.shortclosing_flag from agr_mst_tapplication a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.submitted_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                    " left join agr_trn_tappproductapproval d on a.application_gid=d.application_gid " +
                    " where a.productdesk_flag in ('" + getProductAppStatus.PendingApproval + "','" + getProductAppStatus.Completed + "') " +
                    " and d.productmanager_approvalflag='Y'  and d.productmember_approvalflag='Y' and (d.product_managergid='" + employee_gid + "' or d.product_membergid = '" + employee_gid + "')";
                     //" and d.productmanager_approvalflag='Y'  and d.productmember_approvalflag='Y' ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicaition_list = new List<applicationProduct_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getapplicaition_list.Add(new applicationProduct_list
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical_name = dt["vertical_name"].ToString(),
                        created_date = dt["updated_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        application_status = dt["status"].ToString(),
                        applicant_type = dt["applicant_type"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        region = dt["region"].ToString(),
                        createdby = dt["createdby"].ToString(),
                        productdesk_gid = dt["productdesk_gid"].ToString(),
                        appproductapproval_gid = dt["appproductapproval_gid"].ToString(),
                        productdesk_name = dt["productdesk_name"].ToString(),
                        productmanager_approvaldate = dt["productmanager_approvaldate"].ToString(),
                        product_managername = dt["product_managername"].ToString(),
                        renewal_flag = dt["renewal_flag"].ToString(),
                        amendment_flag = dt["amendment_flag"].ToString(),
                        shortclosing_flag = dt["shortclosing_flag"].ToString()
                    });

                }
            }
            values.applicationProduct_list = getapplicaition_list;
            dt_datatable.Dispose();
        }
        public void DaPostAppProductqueryadd(mdlproductquery values, string user_gid, string employee_gid)
        {
            msGetGid = objcmnfunctions.GetMasterGID("APDQ");
            if (values.queryraised_to == "RM")
            {
                msSQL = "select appproductapproval_gid from agr_trn_tappproductapproval where application_gid = '" + values.application_gid + "'";
                values.appproductapproval_gid = objdbconn.GetExecuteScalar(msSQL);
            }

            msSQL = " insert into agr_trn_tapplicationproductdescquery(" +
                     " appproductquery_gid," +
                     " appproductapproval_gid," +
                     " application_gid, " +
                     " query_title," +
                     " query_description," +
                     " queryraised_to," +
                     " created_by," +
                     " created_date)" +
                     " values(" +
                     "'" + msGetGid + "'," +
                     "'" + values.appproductapproval_gid + "', " +
                     "'" + values.application_gid + "', " +
                     "'" + values.querytitle.Replace("'", "") + "'," +
                     "'" + values.querydesc.Replace("'", "") + "'," +
                     "'" + values.queryraised_to + "'," +
                     "'" + user_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "update agr_mst_tapplication set  productquery_status='Query Raised to RM'" +
                       " where application_gid='" + values.application_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                //Mail Start
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
                    //msSQL = "select group_concat(c.employee_emailid) from agr_mst_tapplication a left join ocs_mst_tcredit2nationalmanager b on a.creditgroup_gid = b.creditmapping_gid left join hrm_mst_temployee c on c.employee_gid = b.employee_gid where a.application_gid = '" + values.application_gid + "'";
                    //creditnationalmanager_mailid = objdbconn.GetExecuteScalar(msSQL);

                    //msSQL = "select group_concat(c.employee_emailid) from agr_mst_tapplication a left join ocs_mst_tcreditr2regionalmanager b on a.creditgroup_gid = b.creditmapping_gid left join hrm_mst_temployee c on c.employee_gid = b.employee_gid where a.application_gid = '" + values.application_gid + "'";
                    //creditregionalmanager_mailid = objdbconn.GetExecuteScalar(msSQL);

                    //tomail_id = "" + creditnationalmanager_mailid + "," + creditregionalmanager_mailid + "";

                    msSQL = " select b.employee_emailid from agr_mst_tapplication a left join hrm_mst_temployee b on b.employee_gid = a.relationshipmanager_gid where a.application_gid='" + values.application_gid + "'";
                    tomail_id = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = " select  concat( c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code)  from agr_mst_tapplication a left join hrm_mst_temployee b on b.employee_gid = a.regionalhead_gid left join adm_mst_tuser c on c.user_gid = b.user_gid where a.application_gid='" + values.application_gid + "'";
                    regionalhead_name = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = " select  a.overalllimit_amount  from agr_mst_tapplication a  where a.application_gid='" + values.application_gid + "'";
                    lsoveralllimit_amount = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = " select  group_concat(c.employee_emailid)  from agr_trn_tappproductapproval a left join hrm_mst_temployee c on c.employee_gid = a.product_membergid   where a.application_gid='" + values.application_gid + "'";
                    lspromember_id = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = " select  group_concat(c.employee_emailid)  from agr_trn_tappproductapproval a left join hrm_mst_temployee c on c.employee_gid = a.product_managergid   where a.application_gid='" + values.application_gid + "'";
                    lspromanager_id = objdbconn.GetExecuteScalar(msSQL);

                    lsccweb = ConfigurationManager.AppSettings["Samagrostf"].ToString();

                    cc_mailid = lspromanager_id + "," + lsccweb + "," + lspromember_id;



                    msSQL = " select  group_concat(product_type) as Product, group_concat(productsub_type) as Program, group_concat(rate_interest) as Margin from agr_mst_tapplication2loan  where application_gid = '" + values.application_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        ls_Product = objODBCDatareader["Product"].ToString();
                        ls_Program = objODBCDatareader["Program"].ToString();
                        ls_Margin = objODBCDatareader["Margin"].ToString();
                    }
                    objODBCDatareader.Close();

                    msSQL = " select query_title  from agr_trn_tapplicationproductdescquery  " +
                        "where appproductquery_gid = '" + msGetGid + "'";

                    query_title = objdbconn.GetExecuteScalar(msSQL);
                    //ls_approval = objODBCDatareader["appproductapproval_gid"].ToString();

                    msSQL = "select concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as employee_name" +
                      " from hrm_mst_temployee a" +
                      " left join adm_mst_tuser b on a.user_gid=b.user_gid" +
                      " where a.employee_gid='" + employee_gid + "'";
                    ls_approval = objdbconn.GetExecuteScalar(msSQL);

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
                    //lssource = ConfigurationManager.AppSettings["img_path"];
                    sub = " A query has been raised to you  ";
                    body = "<style>table, th, td {border: 1px solid black;border-collapse: collapse;}</style>";
                    body = body + "<table style='border-right: 1px solid black;border-top: 1px solid black;border-bottom: 1px solid black;'><tr><td style='border-right-color:white;align:center;'>";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp Dear Sir / Madam, <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp Greetings! <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp A query has been raised. The details are as follows, <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp <b> Application Number : </b> " + application_no + "  <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp <b> Customer Name : </b> " + HttpUtility.HtmlEncode(customer_name)+ "  <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp <b> RM Name : </b> " + HttpUtility.HtmlEncode(relationshipmanager_name)+ "  <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp <b> Product:</b> " + ls_Product + "<br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp <b> Program:</b> " + ls_Program + "<br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp <b> Cluster Head Name : </b> " + HttpUtility.HtmlEncode(cluster_head)+ "  <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp <b> Regional Head Name:</b> " + HttpUtility.HtmlEncode(regionalhead_name) + "<br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp <b> Zonal Head Name : </b> " + HttpUtility.HtmlEncode(zonal_head)+ "  <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp <b> Query Raised By :</b>  " + ls_approval + "  <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp <b> Query Title :</b>  " + HttpUtility.HtmlEncode(query_title)+ "  <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp Log into " + ConfigurationManager.AppSettings["livedomain_url"].ToString() + " and complete the necessary actions.";
                    body = body + "<br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp **This is an automated e-mail. Please do not reply to this mailbox**";
                    body = body + "<br />";
                    body = body + "<br />";
                    body = body + "</td><td style='margin-left:20px; border-left-color:white;'>&nbsp&nbsp</td></tr></table>";

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
                catch (Exception ex)
                {
                    values.message = ex.ToString();
                }
                //Mail End
                values.status = true;
                values.message = "Query Raised Successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }


        }

        public void DaGetApprmquerysSummary(string employee_gid, applproductapproval values, string application_gid)
        {
            msSQL =  " select appproductquery_gid,a.appproductapproval_gid,query_title,query_status,query_description,a.close_remarks, " +
                     " concat(b.user_firstname, ' ', b.user_lastname, ' / ', b.user_code) as created_by, " +
                     " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date from agr_trn_tapplicationproductdescquery a" +
                     " left join adm_mst_tuser b on b.user_gid = a.created_by " +
                     " left join agr_trn_tappproductapproval c on a.appproductapproval_gid=c.appproductapproval_gid " +
                     " where a.application_gid='" + application_gid + "' and a.queryraised_to = 'RM' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getappproductquerylist = new List<appproductquerylist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getappproductquerylist.Add(new appproductquerylist
                    {
                        appproductquery_gid = dt["appproductquery_gid"].ToString(),
                        appproductapproval_gid = dt["appproductapproval_gid"].ToString(),
                        querytitle = dt["query_title"].ToString(),
                        querystatus = dt["query_status"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        querydesc = dt["query_description"].ToString(),
                        close_remarks = dt["close_remarks"].ToString()
                    });
                    values.appproductquerylist = getappproductquerylist;
                }
            }
            dt_datatable.Dispose();


        }

        public void DaGetGetAppcreditqueryesc(mdlproductquery values, string appproductquery_gid)
        {
            msSQL = "select query_description from agr_trn_tapplicationproductdescquery where appproductquery_gid='" + appproductquery_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.querydesc = objODBCDatareader["query_description"].ToString();
            }
            objODBCDatareader.Close();
        }

        public void DaGetAppqueryStatus(mdlquerystatus values, string application_gid, string employee_gid)
        {
            msSQL = "select query_status from agr_trn_tapplicationproductdescquery where  application_gid ='" + application_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objODBCDatareader.Close();
                msSQL = "select query_status from agr_trn_tapplicationproductdescquery where query_status='Open' and  application_gid ='" + application_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.querystatus_flag = "N";
                }
                else
                {
                    values.querystatus_flag = "Y";
                }
                objODBCDatareader.Close();
            }
            else
            {
                objODBCDatareader.Close();
                values.querystatus_flag = "Y";
            }

            msSQL = "select application_gid from agr_trn_tappproductapproval where  application_gid ='" + application_gid + "' and product_membergid <> '" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.submitapproval_flag = "Y";
            }
            else
            {
                values.submitapproval_flag = "N";
            }
            objODBCDatareader.Close();

            msSQL = " select application_gid from agr_trn_tappproductapproval where application_gid='" + application_gid + "'" +
                    " and productmanager_approvalflag in ('Y','R','H') and productmember_approvalflag = 'Y'";
            values.approved_flag = objdbconn.GetExecuteScalar(msSQL);
            if (values.approved_flag == "" || values.approved_flag == null)
                values.approved_flag = "N";
            else
                values.approved_flag = "Y";
            values.status = true;

        }

        public void DaGetAppProductrmquerysSummary(applproductapproval values, string application_gid)
        {
            msSQL = " select product_membergid from agr_trn_tappproductapproval where application_gid='" + application_gid + "'";
            values.product_membergid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select appproductquery_gid,a.appproductapproval_gid,query_title,query_status,query_description,a.close_remarks, " +
                    " concat(b.user_firstname, ' ', b.user_lastname, ' / ', b.user_code) as created_by, " +
                    " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date from agr_trn_tapplicationproductdescquery a" +
                    " left join adm_mst_tuser b on b.user_gid = a.created_by " +
                    " left join agr_trn_tappproductapproval c on a.appproductapproval_gid=c.appproductapproval_gid " +
                    " where a.application_gid='" + application_gid + "' and a.queryraised_to = 'RM'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getappproductquerylist = new List<appproductquerylist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getappproductquerylist.Add(new appproductquerylist
                    {
                        appproductquery_gid = dt["appproductquery_gid"].ToString(),
                        appproductapproval_gid = dt["appproductapproval_gid"].ToString(),
                        querytitle = dt["query_title"].ToString(),
                        querystatus = dt["query_status"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        querydesc = dt["query_description"].ToString(),
                        close_remarks = dt["close_remarks"].ToString()
                    });
                    values.appproductquerylist = getappproductquerylist;
                }
            }
            dt_datatable.Dispose();

        }

        public void DaGetProductUpdatequeryStatus(mdlquerystatus values, string appproductquery_gid, string application_gid, string close_remarks, string user_gid)
        {
            msSQL = " update agr_trn_tapplicationproductdescquery set  query_status='Closed', close_remarks='" + close_remarks.Replace("'", "") + "'," +
                    " closed_by='" + user_gid + "', closed_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    " where appproductquery_gid='" + appproductquery_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "select application_gid from agr_trn_tapplicationproductdescquery where query_status = 'Open' and application_gid = '" + application_gid + "'" +
                        " and queryraised_to = 'RM'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == false)
                {
                    msSQL = "update agr_mst_tapplication set  productquery_status='Query Closed'" +
                           " where application_gid='" + application_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                objODBCDatareader.Close();
                //Mail Start
                try
                {
                    msSQL = "select application_no from agr_mst_tapplication where application_gid='" + application_gid + "'";
                    application_no = objdbconn.GetExecuteScalar(msSQL);
                    msSQL = "select customerref_name from agr_mst_tapplication where application_gid='" + application_gid + "'";
                    customer_name = objdbconn.GetExecuteScalar(msSQL);
                    msSQL = "select approved_date from agr_trn_tapplicationapproval a left join agr_mst_tapplication b on b.application_gid = a.application_gid and hierary_level = '5' where b.application_gid='" + application_gid + "'";
                    finalapproved_time = objdbconn.GetExecuteScalar(msSQL);
                    msSQL = " select clustermanager_name,zonalhead_name,relationshipmanager_name from agr_mst_tapplication where application_gid = '" + application_gid + "'";
                    objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader1.HasRows == true)
                    {
                        cluster_head = objODBCDatareader1["clustermanager_name"].ToString();
                        zonal_head = objODBCDatareader1["zonalhead_name"].ToString();
                        relationshipmanager_name = objODBCDatareader1["relationshipmanager_name"].ToString();
                    }
                    objODBCDatareader1.Close();
                    //msSQL = "select group_concat(c.employee_emailid) from agr_mst_tapplication a left join ocs_mst_tcredit2nationalmanager b on a.creditgroup_gid = b.creditmapping_gid left join hrm_mst_temployee c on c.employee_gid = b.employee_gid where a.application_gid = '" + values.application_gid + "'";
                    //creditnationalmanager_mailid = objdbconn.GetExecuteScalar(msSQL);

                    //msSQL = "select group_concat(c.employee_emailid) from agr_mst_tapplication a left join ocs_mst_tcreditr2regionalmanager b on a.creditgroup_gid = b.creditmapping_gid left join hrm_mst_temployee c on c.employee_gid = b.employee_gid where a.application_gid = '" + values.application_gid + "'";
                    //creditregionalmanager_mailid = objdbconn.GetExecuteScalar(msSQL);

                    //tomail_id = "" + creditnationalmanager_mailid + "," + creditregionalmanager_mailid + "";

                    msSQL = " select b.employee_emailid from agr_mst_tapplication a left join hrm_mst_temployee b on b.employee_gid = a.relationshipmanager_gid where a.application_gid='" +application_gid + "'";
                    tomail_id = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = " select  concat( c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code)  from agr_mst_tapplication a left join hrm_mst_temployee b on b.employee_gid = a.regionalhead_gid left join adm_mst_tuser c on c.user_gid = b.user_gid where a.application_gid='" + application_gid + "'";
                    regionalhead_name = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = " select  a.overalllimit_amount  from agr_mst_tapplication a  where a.application_gid='" + application_gid + "'";
                    lsoveralllimit_amount = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = " select  group_concat(c.employee_emailid)  from agr_trn_tappproductapproval a left join hrm_mst_temployee c on c.employee_gid = a.product_membergid   where a.application_gid='" + application_gid + "'";
                    lspromember_id = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = " select  group_concat(c.employee_emailid)  from agr_trn_tappproductapproval a left join hrm_mst_temployee c on c.employee_gid = a.product_managergid   where a.application_gid='" + application_gid + "'";
                    lspromanager_id = objdbconn.GetExecuteScalar(msSQL);

                    lsccweb = ConfigurationManager.AppSettings["Samagrostf"].ToString();

                    cc_mailid = lspromember_id + "," + lsccweb;



                    msSQL = " select  group_concat(product_type) as Product, group_concat(productsub_type) as Program, group_concat(rate_interest) as Margin from agr_mst_tapplication2loan  where application_gid = '" + application_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        ls_Product = objODBCDatareader["Product"].ToString();
                        ls_Program = objODBCDatareader["Program"].ToString();
                        ls_Margin = objODBCDatareader["Margin"].ToString();
                    }
                    objODBCDatareader.Close();

                    msSQL = " select date_format(a.closed_date, '%d-%m-%Y %h:%i %p') as closed_date, concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as closed_by  from agr_trn_tapplicationproductdescquery a " +
                //" left join hrm_mst_temployee b on b.employee_gid=a.appproductapproval_gid " +
                " left join adm_mst_tuser c on c.user_gid=a.closed_by " +
                        "where application_gid = '" + application_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lsclosed_date = objODBCDatareader["closed_date"].ToString();
                        lsclosed_by = objODBCDatareader["closed_by"].ToString();
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
                    //lssource = ConfigurationManager.AppSettings["img_path"];
                    sub = " A query has been closed  ";
                    body = "<style>table, th, td {border: 1px solid black;border-collapse: collapse;}</style>";
                    body = body + "<table style='border-right: 1px solid black;border-top: 1px solid black;border-bottom: 1px solid black;'><tr><td style='border-right-color:white;align:center;'>";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp Dear Sir / Madam, <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp Greetings! <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp A query has been closed. The details are as follows, <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp <b> Application Number : </b> " + application_no + "  <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp <b> Customer Name : </b> " + HttpUtility.HtmlEncode(customer_name)+ "  <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp <b> RM Name : </b> " + HttpUtility.HtmlEncode(relationshipmanager_name)+ "  <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp <b> Product:</b> " + ls_Product + "<br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp <b> Program:</b> " + ls_Program + "<br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp <b> Cluster Head Name : </b> " + HttpUtility.HtmlEncode(cluster_head)+ "  <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp <b> Regional Head Name:</b> " + HttpUtility.HtmlEncode(regionalhead_name)+ "<br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp <b> Zonal Head Name : </b> " + HttpUtility.HtmlEncode(zonal_head)+ "  <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp <b> Query Closed date :</b>  " + lsclosed_date + "  <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp <b> Query Closed by :</b>  " + lsclosed_by + "  <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp Log into " + ConfigurationManager.AppSettings["livedomain_url"].ToString() + " and complete the necessary actions.";
                    body = body + "<br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp **This is an automated e-mail. Please do not reply to this mailbox**";
                    body = body + "<br />";
                    body = body + "<br />";
                    body = body + "</td><td style='margin-left:20px; border-left-color:white;'>&nbsp&nbsp</td></tr></table>";

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
                catch (Exception ex)
                {
                    values.message = ex.ToString();
                }
                //Mail End
                values.status = true;
                values.message = "Query Closed Successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }

        }

        public void DaPostManagerqueryadd(mdlproductquery values, string user_gid, string employee_gid)
        {
            msGetGid = objcmnfunctions.GetMasterGID("APDQ");

            msSQL = " select product_membergid from agr_trn_tappproductapproval where application_gid='" + values.application_gid + "'";
            values.product_membergid = objdbconn.GetExecuteScalar(msSQL);

            if (values.queryraised_to == "RM")
            {
                msSQL = "select appproductapproval_gid from agr_trn_tappproductapproval where application_gid = '" + values.application_gid + "'  and product_membergid = '" + values.product_membergid + "'";
                values.appproductapproval_gid = objdbconn.GetExecuteScalar(msSQL);
            }

            msSQL = " insert into agr_trn_tapplicationproductdescquery(" +
                     " appproductquery_gid," +
                     " appproductapproval_gid," +
                     " application_gid, " +
                     " query_title," +
                     " query_description," +
                     " queryraised_to," +
                     " created_by," +
                     " created_date)" +
                     " values(" +
                     "'" + msGetGid + "'," +
                     "'" + values.appproductapproval_gid + "', " +
                     "'" + values.application_gid + "', " +
                     "'" + values.querytitle + "'," +
                     "'" + values.querydesc.Replace("'", "") + "'," +
                     "'" + values.queryraised_to + "'," +
                     "'" + user_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "update agr_mst_tapplication set productquery_status='Query Raised'" +
                        " where application_gid='" + values.application_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                //Mail Start
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
                    //msSQL = "select group_concat(c.employee_emailid) from agr_mst_tapplication a left join ocs_mst_tcredit2nationalmanager b on a.creditgroup_gid = b.creditmapping_gid left join hrm_mst_temployee c on c.employee_gid = b.employee_gid where a.application_gid = '" + values.application_gid + "'";
                    //creditnationalmanager_mailid = objdbconn.GetExecuteScalar(msSQL);

                    //msSQL = "select group_concat(c.employee_emailid) from agr_mst_tapplication a left join ocs_mst_tcreditr2regionalmanager b on a.creditgroup_gid = b.creditmapping_gid left join hrm_mst_temployee c on c.employee_gid = b.employee_gid where a.application_gid = '" + values.application_gid + "'";
                    //creditregionalmanager_mailid = objdbconn.GetExecuteScalar(msSQL);

                    //tomail_id = "" + creditnationalmanager_mailid + "," + creditregionalmanager_mailid + "";

                    //msSQL = " select b.employee_emailid from agr_mst_tapplication a left join hrm_mst_temployee b on b.employee_gid = a.relationshipmanager_gid where a.application_gid='" + values.application_gid + "'";
                    //tomail_id = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = " select  concat( c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code)  from agr_mst_tapplication a left join hrm_mst_temployee b on b.employee_gid = a.regionalhead_gid left join adm_mst_tuser c on c.user_gid = b.user_gid where a.application_gid='" + values.application_gid + "'";
                    regionalhead_name = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = " select  a.overalllimit_amount  from agr_mst_tapplication a  where a.application_gid='" + values.application_gid + "'";
                    lsoveralllimit_amount = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = " select  group_concat(c.employee_emailid)  from agr_trn_tappproductapproval a left join hrm_mst_temployee c on c.employee_gid = a.product_membergid   where a.application_gid='" + values.application_gid + "'";
                    tomail_id = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = " select  group_concat(c.employee_emailid)  from agr_trn_tappproductapproval a left join hrm_mst_temployee c on c.employee_gid = a.product_managergid   where a.application_gid='" + values.application_gid + "'";
                    lspromanager_id = objdbconn.GetExecuteScalar(msSQL);

                    lsccweb = ConfigurationManager.AppSettings["Samagrostf"].ToString();

                    cc_mailid = lspromanager_id + "," + lsccweb;



                    msSQL = " select  group_concat(product_type) as Product, group_concat(productsub_type) as Program, group_concat(rate_interest) as Margin from agr_mst_tapplication2loan  where application_gid = '" + values.application_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        ls_Product = objODBCDatareader["Product"].ToString();
                        ls_Program = objODBCDatareader["Program"].ToString();
                        ls_Margin = objODBCDatareader["Margin"].ToString();
                    }
                    objODBCDatareader.Close();

                    msSQL = " select query_title  from agr_trn_tapplicationproductdescquery  " +
                         "where appproductquery_gid = '" + msGetGid + "'";

                    query_title = objdbconn.GetExecuteScalar(msSQL);
                    //ls_approval = objODBCDatareader["appproductapproval_gid"].ToString();

                    msSQL = "select concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as employee_name" +
                      " from hrm_mst_temployee a" +
                      " left join adm_mst_tuser b on a.user_gid=b.user_gid" +
                      " where a.employee_gid='" + employee_gid + "'";
                    ls_approval = objdbconn.GetExecuteScalar(msSQL);


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
                    //lssource = ConfigurationManager.AppSettings["img_path"];
                    sub = " A query has been raised to you  ";
                    body = "<style>table, th, td {border: 1px solid black;border-collapse: collapse;}</style>";
                    body = body + "<table style='border-right: 1px solid black;border-top: 1px solid black;border-bottom: 1px solid black;'><tr><td style='border-right-color:white;align:center;'>";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp Dear Sir / Madam, <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp Greetings! <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp A query has been raised. The details are as follows, <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp <b> Application Number : </b> " + application_no + "  <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp <b> Customer Name : </b> " + HttpUtility.HtmlEncode(customer_name)+ "  <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp <b> RM Name : </b> " + HttpUtility.HtmlEncode(relationshipmanager_name)+ "  <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp <b> Product:</b> " + ls_Product + "<br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp <b> Program:</b> " + ls_Program + "<br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp <b> Cluster Head Name : </b> " + HttpUtility.HtmlEncode(cluster_head)+ "  <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp <b> Regional Head Name:</b> " + HttpUtility.HtmlEncode(regionalhead_name)+ "<br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp <b> Zonal Head Name : </b> " + HttpUtility.HtmlEncode(zonal_head)+ "  <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp <b> Query Raised By :</b>  " + ls_approval + "  <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp <b> Query Title :</b>  " + HttpUtility.HtmlEncode(query_title)+ "  <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp Log into " + ConfigurationManager.AppSettings["livedomain_url"].ToString() + " and complete the necessary actions.";
                    body = body + "<br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp **This is an automated e-mail. Please do not reply to this mailbox**";
                    body = body + "<br />";
                    body = body + "<br />";
                    body = body + "</td><td style='margin-left:20px; border-left-color:white;'>&nbsp&nbsp</td></tr></table>";

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
                catch (Exception ex)
                {
                    values.message = ex.ToString();
                }
                //Mail End
                values.status = true;
                values.message = "Query Raised Successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }


        }
        public void DaGetManagerquerySummary(applproductapproval values, string application_gid)
        {
            msSQL = " select product_managergid from agr_trn_tappproductapproval where application_gid='" + application_gid + "'";
            values.product_managergid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select a.application_gid, appproductquery_gid,a.appproductapproval_gid,a.query_title,a.query_status,a.query_description,a.close_remarks, " +
                     " concat(b.user_firstname, ' ', b.user_lastname, ' / ', b.user_code) as created_by, " +
                     " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date from agr_trn_tapplicationproductdescquery a" +
                     " left join adm_mst_tuser b on b.user_gid = a.created_by " +
                     " left join agr_trn_tappproductapproval c on a.appproductapproval_gid=c.appproductapproval_gid " +
                     " where a.application_gid='" + application_gid + "' and a.queryraised_to = 'Member'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getappmanagerquerylist = new List<appmanagerquerylist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msSQL = " select application_gid from agr_trn_tapplicationproductdescquery where query_status = 'Open' and application_gid = '" + dt["application_gid"].ToString() + "' " +
                           " and queryraised_to = 'RM'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == false)
                    {
                        lsrmquery_flag = "Y";
                    }
                    else
                    {
                        lsrmquery_flag = "N";
                    }
                    objODBCDatareader.Close();

                    getappmanagerquerylist.Add(new appmanagerquerylist
                    {
                        appproductapproval_gid = dt["appproductapproval_gid"].ToString(),
                        appproductquery_gid = dt["appproductquery_gid"].ToString(),
                        querytitle = dt["query_title"].ToString(),
                        querystatus = dt["query_status"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        querydesc = dt["query_description"].ToString(),
                        close_remarks = dt["close_remarks"].ToString(),
                        query_to = values.product_membergid,
                        rmquery_flag = lsrmquery_flag,
                    });
                    values.appmanagerquerylist = getappmanagerquerylist;
                }
            }
            dt_datatable.Dispose();
        }


        public void DaPostAppProductManagerApproval(MdlProductApprovaldtl values, string employee_gid, string user_gid)
        {
            try
            {
                if (values.approval_status == "Approved")
                {
                    msSQL = " update agr_trn_tappproductapproval set productmanager_approvalflag='Y'," +
                            " productmanager_approvalremarks ='" + values.approval_remarks.Replace("'", "") + "'," +
                            " productmanager_approvaldate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                            " where appproductapproval_gid='" + values.appproductapproval_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnResult != 0)
                    {
                        msSQL = " update agr_mst_tapplication set approval_status='Submitted to Underwriting', " +
                                " productdesk_flag='" + getProductAppStatus.Completed + "'" +
                                " where application_gid='" + values.application_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        msSQL = "select onboarding_status from agr_mst_tapplication where application_gid='" + values.application_gid + "'";
                        string lsonboarding_status = objdbconn.GetExecuteScalar(msSQL);
                        if (lsonboarding_status == "Direct")
                        {
                            DaAgrMstApplicationEdit objDaAgrMstApplicationEdit = new DaAgrMstApplicationEdit();
                            objDaAgrMstApplicationEdit.FnAutoApprovalFlow(values.application_gid, employee_gid, user_gid, 1);
                        } 
                        values.message = "Product Approval Approved Successfully..!";


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

                        msSQL = " select  group_concat(c.employee_emailid)  from agr_trn_tappproductapproval a left join hrm_mst_temployee c on c.employee_gid = a.product_membergid   where a.application_gid='" + values.application_gid + "'";
                        lspromember_id = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = " select  group_concat(c.employee_emailid)  from agr_trn_tappproductapproval a left join hrm_mst_temployee c on c.employee_gid = a.product_managergid   where a.application_gid='" + values.application_gid + "'";
                        lspromanager_id = objdbconn.GetExecuteScalar(msSQL);

                        lsccweb = ConfigurationManager.AppSettings["Samagrostf"].ToString();

                        cc_mailid = lspromember_id + "," + lsccweb + "," + lspromanager_id;



                        msSQL = " select  group_concat(product_type) as Product, group_concat(productsub_type) as Program, group_concat(rate_interest) as Margin from agr_mst_tapplication2loan  where application_gid = '" + values.application_gid + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            ls_Product = objODBCDatareader["Product"].ToString();
                            ls_Program = objODBCDatareader["Program"].ToString();
                            ls_Margin = objODBCDatareader["Margin"].ToString();
                        }
                        objODBCDatareader.Close();

                        msSQL = " select date_format(a.productmanager_approvaldate, '%d-%m-%Y %h:%i %p') as  productmanager_approvaldate, concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as product_managergid  from agr_trn_tappproductapproval a " +
                            " left join hrm_mst_temployee b on b.employee_gid=a.product_managergid " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                            "where application_gid = '" + values.application_gid + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            ls_approvaldate = objODBCDatareader["productmanager_approvaldate"].ToString();
                            ls_approval = objODBCDatareader["product_managergid"].ToString();
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
                        sub = " Application allocation for Credit assessment ";
                        body = "<style>table, th, td {border: 1px solid black;border-collapse: collapse;}</style>";
                        body = body + "<table style='border-right: 1px solid black;border-top: 1px solid black;border-bottom: 1px solid black;'><tr><td style='border-right-color:white;'>";

                        body = body + " &nbsp &nbsp Dear Sir / Madam, <br />";
                        body = body + "<br />";
                        body = body + "&nbsp &nbsp Greetings! <br />";
                        body = body + "<br />";
                        body = body + "&nbsp &nbsp The below application has been submitted, please allocate it for credit assessment. Details are as follows, <br />";
                        body = body + "<br />";
                        body = body + "&nbsp &nbsp <b> Application Number : </b> " + application_no + "  <br />";
                        body = body + "<br />";
                        body = body + "&nbsp &nbsp <b> Customer Name : </b> " + HttpUtility.HtmlEncode(customer_name)+ "  <br />";
                        body = body + "<br />";
                        body = body + "&nbsp &nbsp <b> RM Name : </b> " + HttpUtility.HtmlEncode(relationshipmanager_name)+ "  <br />";
                        body = body + "<br />";
                        body = body + "&nbsp &nbsp <b> Product:</b> " + ls_Product + "<br />";
                        body = body + "<br />";
                        body = body + "&nbsp &nbsp <b> Program:</b> " + ls_Program + "<br />";
                        body = body + "<br />";
                        body = body + "&nbsp &nbsp <b> Cluster Head Name : </b> " + HttpUtility.HtmlEncode(cluster_head)+ "  <br />";
                        body = body + "<br />";
                        body = body + "&nbsp &nbsp <b> Regional Head Name:</b> " + HttpUtility.HtmlEncode(regionalhead_name)+ "<br />";
                        body = body + "<br />";
                        body = body + "&nbsp &nbsp <b> Zonal Head Name : </b> " + HttpUtility.HtmlEncode(zonal_head)+ "  <br />";
                        body = body + "<br />";
                        body = body + "&nbsp &nbsp <b> Product Desk Approved Date :</b>  " + ls_approvaldate + "  <br />";
                        body = body + "<br />";
                        body = body + "&nbsp &nbsp <b> Approved By :</b>  " + HttpUtility.HtmlEncode(ls_approval)+ "  <br />";
                        body = body + "<br />";
                        body = body + "&nbsp &nbsp Log into " + ConfigurationManager.AppSettings["livedomain_url"].ToString() + " and complete the necessary actions.";
                        body = body + "<br />";
                        body = body + "<br />";
                        body = body + "&nbsp &nbsp **This is an automated e-mail. Please do not reply to this mailbox**";
                        body = body + "<br />";
                        body = body + "<br />";
                        body = body + "</td><td style='margin-left:20px; border-left-color:white;'>&nbsp&nbsp</td></tr></table>";


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
                        //cc_mailid = ConfigurationManager.AppSettings["Samagrostf"].ToString();

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
                else if (values.approval_status == "Rejected")
                {
                    msSQL = " update agr_trn_tappproductapproval set productmanager_approvalflag='R'," +
                           " productmanager_approvalremarks ='" + values.approval_remarks.Replace("'", "") + "'," +
                           " productmanager_approvaldate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                           " where appproductapproval_gid='" + values.appproductapproval_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult != 0)
                    {
                        msSQL = " update agr_mst_tapplication set approval_status='Product Approval - Rejected', " +
                                " productdesk_flag='" + getProductAppStatus.Rejected + "'" +
                                " where application_gid='" + values.application_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


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
                        //msSQL = "select group_concat(c.employee_emailid) from agr_mst_tapplication a left join ocs_mst_tcredit2nationalmanager b on a.creditgroup_gid = b.creditmapping_gid left join hrm_mst_temployee c on c.employee_gid = b.employee_gid where a.application_gid = '" + values.application_gid + "'";
                        //creditnationalmanager_mailid = objdbconn.GetExecuteScalar(msSQL);

                        //msSQL = "select group_concat(c.employee_emailid) from agr_mst_tapplication a left join ocs_mst_tcreditr2regionalmanager b on a.creditgroup_gid = b.creditmapping_gid left join hrm_mst_temployee c on c.employee_gid = b.employee_gid where a.application_gid = '" + values.application_gid + "'";
                        //creditregionalmanager_mailid = objdbconn.GetExecuteScalar(msSQL);

                        //tomail_id = "" + creditnationalmanager_mailid + "," + creditregionalmanager_mailid + "";

                        msSQL = " select b.employee_emailid from agr_mst_tapplication a left join hrm_mst_temployee b on b.employee_gid = a.relationshipmanager_gid where a.application_gid='" + values.application_gid + "'";
                        tomail_id = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = " select  concat( c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code)  from agr_mst_tapplication a left join hrm_mst_temployee b on b.employee_gid = a.regionalhead_gid left join adm_mst_tuser c on c.user_gid = b.user_gid where a.application_gid='" + values.application_gid + "'";
                        regionalhead_name = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = " select  a.overalllimit_amount  from agr_mst_tapplication a  where a.application_gid='" + values.application_gid + "'";
                        lsoveralllimit_amount = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = " select  group_concat(c.employee_emailid)  from agr_trn_tappproductapproval a left join hrm_mst_temployee c on c.employee_gid = a.product_membergid   where a.application_gid='" + values.application_gid + "'";
                        lspromember_id = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = " select  group_concat(c.employee_emailid)  from agr_trn_tappproductapproval a left join hrm_mst_temployee c on c.employee_gid = a.product_managergid   where a.application_gid='" + values.application_gid + "'";
                        lspromanager_id = objdbconn.GetExecuteScalar(msSQL);

                        lsccweb = ConfigurationManager.AppSettings["Samagrostf"].ToString();

                        cc_mailid = lspromember_id + "," + lsccweb ;



                        msSQL = " select  group_concat(product_type) as Product, group_concat(productsub_type) as Program, group_concat(rate_interest) as Margin from agr_mst_tapplication2loan  where application_gid = '" + values.application_gid + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            ls_Product = objODBCDatareader["Product"].ToString();
                            ls_Program = objODBCDatareader["Program"].ToString();
                            ls_Margin = objODBCDatareader["Margin"].ToString();
                        }
                        objODBCDatareader.Close();

                        msSQL = " select date_format(a.productmanager_approvaldate, '%d-%m-%Y %h:%i %p') as  productmanager_approvaldate, concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as product_managergid  from agr_trn_tappproductapproval a " +
                            " left join hrm_mst_temployee b on b.employee_gid=a.product_managergid " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                            "where application_gid = '" + values.application_gid + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            ls_approvaldate = objODBCDatareader["productmanager_approvaldate"].ToString();
                            ls_approval = objODBCDatareader["product_managergid"].ToString();
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
                        sub = " An application has been rejected ";
                        body = "<style>table, th, td {border: 1px solid black;border-collapse: collapse;}</style>";
                        body = body + "<table style='border-right: 1px solid black;border-top: 1px solid black;border-bottom: 1px solid black;'><tr><td style='border-right-color:white;'>";

                        body = body + " &nbsp &nbsp Dear Sir / Madam, <br />";
                        body = body + "<br />";
                        body = body + "&nbsp &nbsp Greetings! <br />";
                        body = body + "<br />";
                        body = body + "&nbsp &nbsp The below application has been rejected. The details are as follows, <br />";
                        body = body + "<br />";
                        body = body + "&nbsp &nbsp <b> Application Number : </b> " + application_no + "  <br />";
                        body = body + "<br />";
                        body = body + "&nbsp &nbsp <b> Customer Name : </b> " + HttpUtility.HtmlEncode(customer_name)+ "  <br />";
                        body = body + "<br />";
                        body = body + "&nbsp &nbsp <b> RM Name : </b> " + HttpUtility.HtmlEncode(relationshipmanager_name)+ "  <br />";
                        body = body + "<br />";
                        body = body + "&nbsp &nbsp <b> Product:</b> " + ls_Product + "<br />";
                        body = body + "<br />";
                        body = body + "&nbsp &nbsp <b> Program:</b> " + ls_Program + "<br />";
                        body = body + "<br />";
                        body = body + "&nbsp &nbsp <b> Cluster Head Name : </b> " + HttpUtility.HtmlEncode(cluster_head)+ "  <br />";
                        body = body + "<br />";
                        body = body + "&nbsp &nbsp <b> Regional Head Name:</b> " + HttpUtility.HtmlEncode(regionalhead_name)+ "<br />";
                        body = body + "<br />";
                        body = body + "&nbsp &nbsp <b> Zonal Head Name : </b> " + HttpUtility.HtmlEncode(zonal_head)+ "  <br />";
                        body = body + "<br />";
                        body = body + "&nbsp &nbsp <b> Rejected Date :</b>  " + ls_approvaldate + "  <br />";
                        body = body + "<br />";
                        body = body + "&nbsp &nbsp <b> Rejected By :</b>  " + HttpUtility.HtmlEncode(ls_approval)+ "  <br />";
                        body = body + "<br />";
                        body = body + "&nbsp &nbsp Log into " + ConfigurationManager.AppSettings["livedomain_url"].ToString() + " and complete the necessary actions.";
                        body = body + "<br />";
                        body = body + "<br />";
                        body = body + "&nbsp &nbsp **This is an automated e-mail. Please do not reply to this mailbox**";
                        body = body + "<br />";
                        body = body + "<br />";
                        body = body + "</td><td style='margin-left:20px; border-left-color:white;'>&nbsp&nbsp</td></tr></table>";


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
                        //cc_mailid = ConfigurationManager.AppSettings["Samagrostf"].ToString();

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



                        values.message = "Product Approval Rejected Successfully..!";
                    }
                }
                else
                {
                    msSQL = " update agr_trn_tappproductapproval set productmanager_approvalflag='H'," +
                        " productmanager_approvalremarks ='" + values.approval_remarks.Replace("'", "") + "'," +
                           " productmanager_approvaldate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                           " where appproductapproval_gid='" + values.appproductapproval_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult != 0)
                    {
                        msSQL = " update agr_mst_tapplication set approval_status='Product Approval - Hold', " +
                                " productdesk_flag='" + getProductAppStatus.Holded + "'" +
                                " where application_gid='" + values.application_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        values.message = "Product Approval Holded Successfully..!";
                    }
                }

                if (mnResult != 0)
                {
                    values.status = true;
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occured..!";
                }

            }
            catch (Exception ex)
            {

            }
        }

        public void  DaGetMemberMangerApprovalDtls(MdlProductApprovaldtl values, string application_gid )
        {

            try
            {

                msSQL = "select product_membername, date_format(productmember_approvaldate, '%d-%m-%Y %h:%i %p') as productmember_approvaldate,   " +
                    " CASE WHEN(productmember_approvalflag = 'N')  THEN 'Member Approval Pending'" +
                    "  WHEN(productmember_approvalflag = 'Y')  THEN 'Member Approval Completed'" +
                    " ELSE '' END as productmember_approvalflag , " +
                        "product_managername, date_format(productmanager_approvaldate, '%d-%m-%Y %h:%i %p') as productmanager_approvaldate, " +
                        " CASE WHEN(productmanager_approvalflag = 'N')  THEN 'Manager Approval Pending'" +
                        "  WHEN(productmanager_approvalflag = 'Y')  THEN 'Manager Approval Completed'" +
                        "  WHEN(productmanager_approvalflag = 'R')  THEN 'Manager Approval Rejected'" +
                        "  WHEN(productmanager_approvalflag = 'H')  THEN 'Manager Approval Hold'" +
                         " ELSE '' END as productmanager_approvalflag , productmanager_approvalremarks" +
                        " from agr_trn_tappproductapproval where application_gid ='" + application_gid + "'";

                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows == true)
                {
                    values.member_name = objODBCDataReader["product_membername"].ToString();
                    values.memberapproval_date = objODBCDataReader["productmember_approvaldate"].ToString();
                    values.memberapproval_flag = objODBCDataReader["productmember_approvalflag"].ToString();
                    values.manager_name = objODBCDataReader["product_managername"].ToString();
                    values.manager_approvaldate = objODBCDataReader["productmanager_approvaldate"].ToString();
                    values.manager_approvalflag = objODBCDataReader["productmanager_approvalflag"].ToString();
                    values.manager_approvalremarks = objODBCDataReader["productmanager_approvalremarks"].ToString();
                }
                objODBCDataReader.Close();

            }

            catch(Exception ex)
            {


            }

        }


        public void DaGetAutoApprovalSummary(string employee_gid, MdlMstProductApproval values)
        {
            msSQL = " select a.application_gid,a.application_no,a.customerref_name as customer_name,a.customer_urn,a.vertical_name,d.productdesk_name," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,a.status,a.applicant_type, a.created_by as createdby, " +
                    " date_format(a.submitted_date,'%d-%m-%Y %h:%i %p') as updated_date,a.approval_status, d.product_managername, d.productmanager_approvaldate, " +
                    " CASE WHEN(a.onboarding_status = 'Proposal' )  THEN 'Credit' " +
                    " WHEN(a.onboarding_status = 'Direct' ) THEN 'Advance' " +
                    " ELSE '-' END as onboarding_status , " +
                    " region,a.productdesk_gid,appproductapproval_gid from agr_mst_tapplication a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                    " left join agr_trn_tappproductapproval d on a.application_gid=d.application_gid " +
                    " left join agr_mst_tproductdeskmapping e on  a.productdesk_gid = e.productdesk_gid " +
                    " where a.productdesk_flag in ('" + getProductAppStatus.Completed + "') " +
                    " and d.productmanager_approvalflag='Y'  and d.productmember_approvalflag='Y' group by a.application_gid ";
            //" and d.productmanager_approvalflag='Y'  and d.productmember_approvalflag='Y' ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicaition_list = new List<applicationProduct_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getapplicaition_list.Add(new applicationProduct_list
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical_name = dt["vertical_name"].ToString(),
                        created_date = dt["updated_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        application_status = dt["status"].ToString(),
                        applicant_type = dt["applicant_type"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        region = dt["region"].ToString(),
                        createdby = dt["createdby"].ToString(),
                        productdesk_gid = dt["productdesk_gid"].ToString(),
                        appproductapproval_gid = dt["appproductapproval_gid"].ToString(),
                        productdesk_name = dt["productdesk_name"].ToString(),
                        productmanager_approvaldate = dt["productmanager_approvaldate"].ToString(),
                        product_managername = dt["product_managername"].ToString(),
                        onboarding_status = dt["onboarding_status"].ToString()
                    });

                }
            }
            values.applicationProduct_list = getapplicaition_list;
            dt_datatable.Dispose();
        }


    }
}











