using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Odbc;
using ems.vp.Models;
using ems.utilities.Functions;

namespace ems.vp.DataAccess
{
    public class DaIssue
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable, dt_date, dt_chat;
        string msSQL, msGetGid;
        int mnResult;
        int i = 0;
        string lsissue_status;
        string lsVendorAddressGid;

        public bool DaGetVendorDetail(string user_gid, vendordtl objvendordtl)
        {
            msSQL = " select a.application_code,a.application_name,a.department_name,a.current_status," +
                    " a.team_name,a.applicationmaster_gid," +
                    " c.* from its_mst_tapplicationmaster a" +
                    " left join adm_mst_tvendoruser b on a.application_code = b.vendoruser_code" +
                    " left join acp_mst_tvendor c on a.vendor_gid = c.vendor_gid" +
                    " where b.vendoruser_gid = '" + user_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            try
            {
                List<app_details> apps = null;
                if (objODBCDatareader.HasRows == true)
                {
                    objvendordtl.active_flag = objODBCDatareader["active_flag"].ToString();
                    objvendordtl.address_gid = objODBCDatareader["address_gid"].ToString();
                    objvendordtl.bank_details = objODBCDatareader["bank_details"].ToString();
                    objvendordtl.contact_telephonenumber = objODBCDatareader["contact_telephonenumber"].ToString();
                    objvendordtl.cst_number = objODBCDatareader["cst_number"].ToString();
                    objvendordtl.contactperson_name = objODBCDatareader["contactperson_name"].ToString();
                    objvendordtl.currencyexchange_gid = objODBCDatareader["currencyexchange_gid"].ToString();
                    objvendordtl.email_id = objODBCDatareader["email_id"].ToString();
                    objvendordtl.excise_details = objODBCDatareader["excise_details"].ToString();
                    objvendordtl.gst_number = objODBCDatareader["gst_number"].ToString();
                    objvendordtl.ifsc_code = objODBCDatareader["ifsc_code"].ToString();
                    objvendordtl.pan_number = objODBCDatareader["pan_number"].ToString();
                    objvendordtl.payment_terms = objODBCDatareader["payment_terms"].ToString();
                    objvendordtl.servicetax_number = objODBCDatareader["servicetax_number"].ToString();
                    objvendordtl.tin_number = objODBCDatareader["tin_number"].ToString();
                    objvendordtl.vendor_code = objODBCDatareader["vendor_code"].ToString();
                    objvendordtl.vendor_companyname = objODBCDatareader["vendor_companyname"].ToString();
                    objvendordtl.vendor_gid = objODBCDatareader["vendor_gid"].ToString();
                    objvendordtl.application_code = objODBCDatareader["application_code"].ToString();
                    objvendordtl.application_name = objODBCDatareader["application_name"].ToString();
                    objvendordtl.department_name = objODBCDatareader["department_name"].ToString();
                    objvendordtl.current_status = objODBCDatareader["current_status"].ToString();
                    objvendordtl.team_name = objODBCDatareader["team_name"].ToString();
                    lsVendorAddressGid = objODBCDatareader["address_gid"].ToString();

                    msSQL = " SELECT applicationmaster_gid ,application_name,application_code,current_status,department_name,team_name,current_status" +
                             " FROM its_mst_tapplicationmaster  " +
                             " WHERE vendor_gid='" + objODBCDatareader["vendor_gid"].ToString() + "' and applicationmaster_gid<>'" + objODBCDatareader["applicationmaster_gid"].ToString() + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable.Rows.Count != 0)
                    {
                        apps = dt_datatable.AsEnumerable().Select(row =>
                            new app_details
                            {
                                app_code = row["application_code"].ToString(),
                                app_name = row["application_name"].ToString(),
                                app_status = row["current_status"].ToString(),
                                dept = row["department_name"].ToString(),
                                team_name = row["team_name"].ToString(),
                                current_status = row["current_status"].ToString()
                            }).ToList();
                        dt_datatable.Dispose();
                        objvendordtl.applications = apps;
                    }
                    objODBCDatareader.Close();

                    msSQL = " SELECT address1 FROM adm_mst_taddress WHERE address_gid='" + lsVendorAddressGid + "'";

                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        objvendordtl.address_gid = objODBCDatareader["address1"].ToString();
                    }
                    objODBCDatareader.Close();

                    objvendordtl.status = true;
                    objvendordtl.message = "Success";
                    return true;
                }
                else
                {
                    objvendordtl.status = false;
                    objvendordtl.message = "failure";
                    return false;
                }
            }
            catch (Exception ex)
            {
                objvendordtl.message = ex.ToString();
                return false;
            }

        }

        public bool DaGetStatus(string issue_gid, issuestate objissuestate)
        {
            try
            {
                msSQL = " SELECT issue_status FROM its_trn_tissuetracker WHERE issuetracker_gid='" + issue_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                objODBCDatareader.Read();
                objissuestate.issue_status = objODBCDatareader["issue_status"].ToString();
                objissuestate.status = true;
                objissuestate.message = "success";
                return true;
            }
            catch
            {
                objissuestate.status = false;
                objissuestate.message = "failure";
                return false;
            }
        }

        public bool DaGetIssueData(issuetrackertable objissuetrackertable, string user_gid)
        {
            List<tabledata> getdata = null;
            try
            {
                msSQL = " SELECT a.count_new,b.count_open,c.count_closed,d.count_uatacknowledged,e.count_uat,f.count_replycomments,g.count_changedetails FROM " +
                       " (SELECT COUNT(issue_status) AS count_new FROM  its_trn_tissuetracker " +
                       " WHERE issue_status='Pending Acknowledgement'" +
                       " AND application_gid=(SELECT applicationmaster_gid FROM its_mst_tapplicationmaster" +
                       " WHERE application_code=(SELECT vendoruser_code FROM adm_mst_tvendoruser WHERE vendoruser_gid='" + user_gid + "'))) AS a, " +
                       " " +
                       " (SELECT COUNT(issue_status) AS count_open FROM  its_trn_tissuetracker" +
                       " WHERE issue_status <> 'Pending Acknowledgement' AND issue_status <> 'Closed'" +
                       " AND application_gid=(SELECT applicationmaster_gid FROM its_mst_tapplicationmaster" +
                       " WHERE application_code=(SELECT vendoruser_code FROM adm_mst_tvendoruser WHERE vendoruser_gid='" + user_gid + "'))) AS b, " +
                       " " +
                       " (SELECT COUNT(issue_status) AS count_closed FROM  its_trn_tissuetracker" +
                       " WHERE issue_status='Ready to Release'" +
                       " AND application_gid=(SELECT applicationmaster_gid FROM its_mst_tapplicationmaster" +
                       " WHERE application_code=(SELECT vendoruser_code FROM adm_mst_tvendoruser WHERE vendoruser_gid='" + user_gid + "'))) AS c," +
                       " " +
                       " (SELECT COUNT(issue_status) AS count_uatacknowledged FROM  its_trn_tissuetracker " +
                       " WHERE issue_status='UAT - Approved'" +
                       " AND application_gid=(SELECT applicationmaster_gid FROM its_mst_tapplicationmaster" +
                       " WHERE application_code=(SELECT vendoruser_code FROM adm_mst_tvendoruser WHERE vendoruser_gid='" + user_gid + "'))) AS d," +
                       " " +
                       " (SELECT COUNT(issue_status) AS count_uat FROM  its_trn_tissuetracker " +
                       " WHERE ( issue_status='UAT' or issue_status='UAT – WIP')" +
                       " AND application_gid=(SELECT applicationmaster_gid FROM its_mst_tapplicationmaster" +
                       " WHERE application_code=(SELECT vendoruser_code FROM adm_mst_tvendoruser WHERE vendoruser_gid='" + user_gid + "'))) AS e," +
                       " " +
                       " (SELECT COUNT(reply_flag) AS count_replycomments FROM  its_trn_tissuetracker " +
                       " WHERE reply_flag='Y'" +
                       " AND application_gid=(SELECT applicationmaster_gid FROM its_mst_tapplicationmaster" +
                       " WHERE application_code=(SELECT vendoruser_code FROM adm_mst_tvendoruser WHERE vendoruser_gid='" + user_gid + "'))) AS f," +
                       " " +
                       " (SELECT COUNT(changedetails_flag) AS count_changedetails FROM  its_trn_trelease " +
                       " WHERE changedetails_flag='Y'" +
                       " AND application_gid=(SELECT applicationmaster_gid FROM its_mst_tapplicationmaster" +
                       " WHERE application_code=(SELECT vendoruser_code FROM adm_mst_tvendoruser WHERE vendoruser_gid='" + user_gid + "'))) AS g ";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    objissuetrackertable.count_new = objODBCDatareader["count_new"].ToString();
                    objissuetrackertable.count_open = objODBCDatareader["count_open"].ToString();
                    objissuetrackertable.count_closed = objODBCDatareader["count_closed"].ToString();
                    objissuetrackertable.count_uatacknowledged = objODBCDatareader["count_uatacknowledged"].ToString();
                    objissuetrackertable.count_uat = objODBCDatareader["count_uat"].ToString();
                    objissuetrackertable.count_replycomments = objODBCDatareader["count_replycomments"].ToString();
                    objissuetrackertable.count_changedetails = objODBCDatareader["count_changedetails"].ToString();
                    objODBCDatareader.Close();
                }

                msSQL = " SELECT issuetracker_gid, application_gid, application_gid, complaint_gid, categor_gid,reply_flag, " +
                        " subcategory_gid, type_gid, issue_refno, date_format(issue_date, '%d-%m-%Y') as issue_date, issue_type, issue_title, issue_remarks, Severity, priority, response_time, request, " +
                        " request_originator, request_department, request_type, Impacted_Modules, Impacted_System, Reasons, change_description, alternative_ways," +
                        " vendor_gid, vendor_name, created_by, created_date,issuetracker_status, issue_status, change_severity " +
                        " FROM its_trn_tissuetracker WHERE application_gid=(SELECT applicationmaster_gid " +
                        " FROM its_mst_tapplicationmaster WHERE application_code=(SELECT vendoruser_code FROM adm_mst_tvendoruser WHERE vendoruser_gid='" + user_gid + "')) order by issuetracker_gid desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    getdata = dt_datatable.AsEnumerable().Select(row =>
                      new tabledata
                      {
                          issuetracker_gid = row["issuetracker_gid"].ToString(),
                          applicationmaster_gid = row["application_gid"].ToString(),
                          application_gid = row["application_gid"].ToString(),
                          complaint_gid = row["complaint_gid"].ToString(),
                          type_gid = row["type_gid"].ToString(),
                          issue_refno = row["issue_refno"].ToString(),
                          issue_date = row["issue_date"].ToString(),
                          issue_type = row["issue_type"].ToString(),
                          issue_title = row["issue_title"].ToString(),
                          issue_remarks = row["issue_remarks"].ToString(),
                          Severity = row["Severity"].ToString(),
                          priority = row["priority"].ToString(),
                          reply_flag =row["reply_flag"].ToString(),
                          response_time = row["response_time"].ToString(),
                          issue_status = row["issuetracker_status"].ToString()
                      }).ToList();
                    objissuetrackertable.tabledata = getdata;
                }
                objODBCDatareader.Close();
                objissuetrackertable.status = true;
                objissuetrackertable.message = "success";
                return true;
            }
            catch (Exception ex)
            {
                objissuetrackertable.status = false;
                objissuetrackertable.message = "failure";
                return false;
            }

        }

        public bool DaGetViewData(string issue_gid, viewIssueDoc objviewIssueDoc)
        {
            List<documents> doc = null;
            try
            {

                msSQL = " SELECT issuetracker_gid, application_gid, application_gid, complaint_gid, categor_gid, " +
                       " subcategory_gid, type_gid, issue_refno, date_format(issue_date, '%d-%m-%Y') as issue_date," +
                       " issue_type, issue_title, issue_remarks, Severity, priority, response_time, request, " +
                       " request_originator, request_department, request_type, Impacted_Modules, Impacted_System, Reasons, change_description, alternative_ways," +
                       " vendor_gid, vendor_name, created_by, created_date, issue_status, change_severity FROM its_trn_tissuetracker WHERE issuetracker_gid='" + issue_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows==true)
                {
                    objviewIssueDoc.issue_refno = objODBCDatareader["issue_refno"].ToString();
                    objviewIssueDoc.issuetracker_gid = objODBCDatareader["issuetracker_gid"].ToString();
                    objviewIssueDoc.issue_date = objODBCDatareader["issue_date"].ToString();
                    objviewIssueDoc.issue_status = objODBCDatareader["issue_status"].ToString();
                    objviewIssueDoc.issue_title = objODBCDatareader["issue_title"].ToString();
                    objviewIssueDoc.issue_type = objODBCDatareader["issue_type"].ToString();
                    objviewIssueDoc.issue_remarks = objODBCDatareader["issue_remarks"].ToString();
                    objviewIssueDoc.priority = objODBCDatareader["priority"].ToString();

                    msSQL = " SELECT * FROM its_trn_tticketdocument WHERE complaint_gid='" + objODBCDatareader["complaint_gid"].ToString() + "'";
                    objODBCDatareader.Close();

                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable.Rows.Count != 0)
                    {
                        objviewIssueDoc.docStatus = "true";
                        doc = dt_datatable.AsEnumerable().Select(row =>
                          new documents
                          {
                              document_path = (row["document_path"].ToString()),
                              document_name = row["document_name"].ToString(),
                              ticketdocument_gid = row["ticketdocument_gid"].ToString()
                          }
                        ).ToList();
                        dt_datatable.Dispose();
                        objviewIssueDoc.path = doc;
                    }
                    else
                    {
                        dt_datatable.Dispose();

                        msSQL = " SELECT * FROM its_trn_tissuedocument WHERE issuetracker_gid='" + issue_gid + "'";
                        dt_datatable = objdbconn.GetDataTable(msSQL);
                        if (dt_datatable.Rows.Count != 0)
                        {
                            objviewIssueDoc.docStatus = "true";
                            doc = dt_datatable.AsEnumerable().Select(row =>
                              new documents
                              {
                                  document_path = (row["document_path"].ToString()),
                                  document_name = row["document_name"].ToString(),
                                  ticketdocument_gid = row["issuedocument_gid"].ToString()
                              }
                            ).ToList();
                            dt_datatable.Dispose();
                            objviewIssueDoc.path = doc;
                        }
                        else
                        {
                            objviewIssueDoc.message = "No files";
                        }

                    }

                    msSQL = " select a.issuestatuslog_gid,a.issue_status,a.remarks,a.reply_comments,a.done_by,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                           " concat(b.user_firstname, ' ', b.user_lastname) as user_name from its_trn_tissuestatuslog a " +
                           " left join adm_mst_tuser b on a.created_by = b.user_gid where issuetracker_gid = '" + issue_gid + "'" +
                           " order by a.created_date asc";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var get_issuestatuslog = new List<issuestatuslog>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dr_datarow in dt_datatable.Rows)
                        {
                            get_issuestatuslog.Add(new issuestatuslog
                            {
                                issue_status = (dr_datarow["issue_status"].ToString()),
                                issue_remarks = (dr_datarow["remarks"].ToString()),
                                created_date = (dr_datarow["created_date"].ToString()),
                                created_by = (dr_datarow["done_by"].ToString()),
                                reply_comments = (dr_datarow["reply_comments"].ToString())
                            });
                        }
                        objviewIssueDoc.issuestatuslog = get_issuestatuslog;
                    }
                    dt_datatable.Dispose();
                    msSQL = " update its_trn_tissuetracker set " +
                            " reply_flag='N' where issuetracker_gid='" + issue_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                }
              
                else
                {
                    objODBCDatareader.Close();
                    
                }
                objviewIssueDoc.status = true;
                objviewIssueDoc.message = "success";
                return true;

            }
            catch (Exception ex)
            {
                objviewIssueDoc.status = false;
                objviewIssueDoc.message = "failure";
                return false;
            }

        }

        public bool DaGetStatusLog(releaseData objStatusLog, string issue_gid)
        {

            List<tabledata> getdata = null;
            msSQL = " select a.issuetracker_gid,a.reply_comments,a.issuestatuslog_gid,b.issue_refno,b.issue_title,a.issue_status,date_format(b.issue_date, '%d-%m-%Y') as issue_date," +
                  " a.reply_date,a.remarks,date_format(a.uat_date,'%d-%m-%Y') as uat_date,date_format(a.created_date,'%d-%m-%Y') as created_date," +
                  " case when a.done_by is null then concat(user_code,' / ', user_firstname, ' / ', user_lastname) else done_by end as created_by" +
                  " from its_trn_tissuestatuslog a" +
                  " left join its_trn_tissuetracker b on a.issuetracker_gid = b.issuetracker_gid" +
                  " left join its_trn_tuattracker d on a.issuetracker_gid = d.issuetracker_gid" +
                  " left join adm_mst_tuser c on a.created_by = c.user_gid" +
                  " where a.issuetracker_gid='" + issue_gid + "'" +
                  " group by a.issuestatuslog_gid  order by a.created_date desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {

                getdata = dt_datatable.AsEnumerable().Select(row =>
                  new tabledata
                  {
                      issue_refno = row["issue_refno"].ToString(),
                      issue_title = row["issue_title"].ToString(),
                      issue_date = row["issue_date"].ToString(),
                      issue_status = row["issue_status"].ToString(),
                      issue_remarks = row["remarks"].ToString(),
                      created_date = row["created_date"].ToString(),
                      created_by = row["created_by"].ToString(),
                      reply_comments = row["reply_comments"].ToString()

                  }).ToList();
                objStatusLog.tabledata = getdata;

            }
            objStatusLog.status = true;
            objStatusLog.message = "success";
            dt_datatable.Dispose();

            return true;
        }

        public bool DaGetChatData(string issue_gid, issuechat objissuechat)
        {
            var date = new List<chatdate>();
            try
            {
                msSQL = " SELECT date_format(response_date,'%d/%m/%Y') as date,response_date FROM its_trn_tissueresponse " +
                        " WHERE issuetracker_gid='" + issue_gid + "' GROUP BY response_date ORDER BY response_date ";
                dt_date = objdbconn.GetDataTable(msSQL);
                if (dt_date.Rows.Count != 0)
                {
                    foreach (DataRow dr_date in dt_date.Rows)
                    {
                        string cdate = Convert.ToDateTime(dr_date["response_date"]).ToString("yyyy-MM-dd");
                        msSQL = " SELECT response_gid, onprimise_text, vendor_text, read_status, response_time FROM its_trn_tissueresponse " +
                               " WHERE response_date='" + cdate + "' ORDER BY response_time ";
                        dt_chat = objdbconn.GetDataTable(msSQL);
                        List<issueChatdtl> chat = null;
                        if (dt_chat.Rows.Count != 0)
                        {
                            chat = dt_chat.AsEnumerable().Select(row =>
                              new issueChatdtl
                              {
                                  response_gid = row["response_gid"].ToString(),
                                  onprimise_text = row["onprimise_text"].ToString(),
                                  vendor_text = row["vendor_text"].ToString(),
                                  response_time = row["response_time"].ToString(),
                                  read_status = row["read_status"].ToString()
                              }).ToList();
                        }
                        date.Add(new chatdate
                        {
                            dates = dr_date["date"].ToString(),
                            Chatdtl = chat
                        });
                    }
                    objissuechat.chatstatus = "true";
                    objissuechat.datelist = date;
                    objissuechat.status = true;
                    objissuechat.message = "success";
                }
                else
                {
                    objissuechat.status = false;
                    objissuechat.message = "failure";
                    objissuechat.chatstatus = "false";
                }
            }
            catch (Exception ex)
            {
                objissuechat.status = false;
                objissuechat.message = ex.ToString();
            }
            
            return true;
        }

        public bool DaSendIssueChat(chatlog values, string user_gid)
        {
            msSQL = " SELECT vendor_gid FROM acp_mst_tvendor WHERE user_gid='" + user_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            objODBCDatareader.Read();
            string vendor = objODBCDatareader["vendor_gid"].ToString();
            objODBCDatareader.Close();
            msGetGid = objcmnfunctions.GetMasterGID("IRL");
            msSQL = " insert into its_trn_tissueresponse(" +
                " response_gid," +
                " issuetracker_gid," +
                " vendor_text," +
                " response_date," +
                " vendor_gid," +
                " response_time)" +
                " values(" +
                "'" + msGetGid + "'," +
                "'" + values.issue_gid + "'," +
                "'" + values.vendor_text + "'," +
                "'" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                "'" + vendor + "'," +
                "'" + DateTime.Now.ToString("HH:mm:ss") + "')";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "success";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "failure";
                return false;
            }
        }

        public bool DaPostStatus(statusUpdate values, string user_gid)
        {
            try
            {
                if (values.IssueStatus == "Issue - Acknowledged")
                {
                    lsissue_status = "Acknowledged";
                }
                else if ((values.IssueStatus == "DEV - Unable to Re-produce") || (values.IssueStatus == "DEV - In-Progress"))
                {
                    lsissue_status = "Work-In-Progress";
                }
                else if ((values .IssueStatus == "Test - Approved") ||(values.IssueStatus == "Test - WIP") ||(values .IssueStatus == "Test - Rejected") ||(values .IssueStatus == "UAT - Ready to Release"))
                {
                    lsissue_status = "Testing";
                }
                else if (values .IssueStatus == "UAT - Released")
                {
                    lsissue_status = "UAT";
                }
              
                msSQL = " Update its_trn_tissuetracker SET issue_status='" + lsissue_status + "'," +
                        " issuetracker_status='" + values .IssueStatus  + "'," +
                       " remarks='" + values.StatusRemarks + "'," +
                       " release_remarks='" + values.StatusRemarks + "'" +
                       " WHERE issuetracker_gid='" + values.issuetrackergid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msGetGid = objcmnfunctions.GetMasterGID("ISRS");
                msSQL = " insert into its_trn_tissuestatuslog (" +
                         " issuestatuslog_gid, " +
                         " issue_status," +
                         " remarks," +
                         " created_date," +
                         " created_by," +
                         " done_by," +
                         " issuetracker_gid)" +
                         "Values(" +
                         "'" + msGetGid + "'," +
                         "'" + values.IssueStatus + "'," +
                         "'" + values.StatusRemarks + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                         "'" + user_gid + "'," +
                         "'" + values.DoneBy + "'," +
                         "'" + values.issuetrackergid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "success";
                    return true;
                }
                else
                {
                    values.status = false;
                    values.message = "failure";
                    return false;
                }
            }
            catch
            {
                values.status = false;
                values.message = "failure";
                return false;
            }
        }
    }
}