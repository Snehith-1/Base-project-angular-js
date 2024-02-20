using System;
using System.Net;
using System.Data;
using System.Data.Odbc;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using ems.utilities.Functions;
using ems.its.Models;
using System.Text;
using System.Configuration;
using System.Web.Script.Serialization;

namespace ems.its.DataAccess
{
    public class DaDeployment
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;
        int mnResult;
        string msGetGID = string.Empty;
        string msGetCode = string.Empty;
        string msGetApp = string.Empty;
        DataTable dt_client, dt_project, dt_employee, dt_summary;
        OdbcDataReader objODBCDataReader;
        StringBuilder sb1 = new StringBuilder();
        StringBuilder sb2 = new StringBuilder();
        StringBuilder sb3 = new StringBuilder();
        string project_gid = string.Empty;
        string project_name = string.Empty;
        string to = string.Empty;
        string sub = string.Empty;
        string msg = string.Empty;
        static string INSTANCE_ID = string.Empty;
        static string CLIENT_ID = string.Empty;
        static string CLIENT_SECRET = string.Empty;
        static string GROUP_API_URL = string.Empty;
        string pages = string.Empty;
        string reports = string.Empty;
        string description = string.Empty;
        public bool DaGetClientList(client_list values)
        {
           try
            {
                msSQL = " SELECT customer_gid,customer_name FROM crm_mst_tcustomer WHERE module_flag='Y'";
                dt_client = objdbconn.GetDataTable(msSQL);
                if (dt_client.Rows.Count != 0)
                {
                    values.clients = dt_client.AsEnumerable().Select(row =>
                    new client
                    {
                        client_gid = row["customer_gid"].ToString(),
                        client_name = row["customer_name"].ToString()
                    }).ToList();
                    dt_client.Dispose();
                    return true;
                }
                else
                {
                    dt_client.Dispose();
                  
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool DaGetProjectList(string id, project_list values)
        {
           try
            {
                msSQL = " SELECT a.project_gid,a.project_name FROM prj_mst_tproject a LEFT JOIN prj_trn_tcustomer2project b on a.project_gid=b.project_gid " +
                   " WHERE b.customer_gid='" + id + "'";
                dt_project = objdbconn.GetDataTable(msSQL);
                if (dt_project.Rows.Count != 0)
                {
                    values.projects = dt_project.AsEnumerable().Select(row =>
                    new project
                    {
                        project_gid = row["project_gid"].ToString(),
                        project_name = row["project_name"].ToString()
                    }).ToList();
                    dt_project.Dispose();
                    return true;
                }
                else
                {
                    dt_project.Dispose();
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool DaAddDeployment(add values, string user_gid, string employee_gid)
        {
            try
            {
                string user_code = objdbconn.GetExecuteScalar(" SELECT CONCAT(user_code,' / ' ,user_firstname,' ',user_lastname) as user_code FROM adm_mst_tuser WHERE user_gid='" + user_gid + "'");
                msGetGID = objcmnfunctions.GetMasterGID("DEPT");
                msGetCode = objcmnfunctions.GetMasterGID("DEPY");
                for (int i = 0; i < values.projects.Count; i++)
                {
                    sb1.Append(values.projects[i].project_gid + ",");
                    sb2.Append(values.projects[i].project_name + ",");
                }
                project_gid = sb1.ToString();
                project_name = sb2.ToString();
                sb1.Clear();
                sb2.Clear();
                project_gid = project_gid.Trim(",".ToCharArray());
                project_name = project_name.Trim(",".ToCharArray());

                msSQL = " INSERT INTO its_trn_tdeployment ( " +
                       " deployment_gid, " +
                       " deployment_code, " +
                       " customer_gid, " +
                       " customer_name, " +
                       " deployment_type, " +
                       " project_gid, " +
                       " project_name, " +
                       " pages, " +
                       " reports, " +
                       " description, " +
                       " need, " +
                       " request_from, " +
                       " status, " +
                       " created_by, " +
                       " created_date, " +
                       " priority " +
                       " )VALUES( " +
                       " '" + msGetGID + "'," +
                       " '" + msGetCode + "'," +
                       " '" + values.client_gid + "'," +
                       " '" + values.client_name + "'," +
                       " '" + values.stage + "'," +
                       " '" + project_gid + "'," +
                       " '" + project_name + "',";
                if (values.new_page == true)
                {
                    msSQL += " '" + values.pages.Replace("'", "\\'") + "',";
                }
                else
                {
                    msSQL += " '',";
                }
                if (values.new_report == true)
                {
                    msSQL += " '" + values.reports.Replace("'", "\\'") + "',";
                }
                else
                {
                    msSQL += "'',";
                }
                msSQL += " '" + values.description.Replace("'", "\\'") + "'," +
                         " '" + values.need + "'," +
                         " '" + user_code + "',";
                if (values.stage == "LIVE")
                {
                    msSQL += " 'Approval Pending',";

                }
                else
                {
                    msSQL += " 'Deployment Pending',";
                }

                msSQL += " '" + employee_gid + "'," +
                        " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',";
                if (values.stage == "LIVE")
                {
                    msSQL += " '" + values.priority + "')";

                }
                else
                {
                    msSQL += " '')";
                }

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    if (values.stage == "LIVE")
                    {
                        for (var i = 0; i < values.approvals.Count; i++)
                        {
                            msGetApp = objcmnfunctions.GetMasterGID("DEPT");
                            msSQL = " INSERT INTO its_trn_tdeploymentapproval ( " +
                                    " deploymentapproval_gid, " +
                                    " deployment_gid, " +
                                    " approver_gid, " +
                                    " status  " +
                                    " )values ( " +
                                    " '" + msGetApp + "'," +
                                    " '" + msGetGID + "'," +
                                    " '" + values.approvals[i].employee_gid + "'," +
                                    " 'Pending')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            if (values.mailFlag == true)
                            {
                                msSQL = " SELECT CONCAT(b.user_firstname, ' ' ,b.user_lastname) AS username, a.employee_emailid FROM hrm_mst_temployee a " +
                                        " LEFT JOIN adm_mst_tuser b ON a.user_gid=b.user_gid WHERE a.employee_gid='" + values.approvals[i].employee_gid + "'";
                                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                                if (objODBCDataReader.HasRows)
                                {
                                    to = objODBCDataReader["employee_emailid"].ToString();
                                    sb3.Append("Dear " + HttpUtility.HtmlEncode(objODBCDataReader["username"].ToString()) + ",<br /><br />");
                                }
                                sub = "Deployment: Waiting for your Approval";
                                sb3.Append("Following Deployment needs your Approval to proceed further.<br /><br />");
                                sb3.AppendLine();
                                sb3.Append("Please review the same and take action ASAP. <br /><br />");
                                sb3.AppendLine();
                                sb3.Append("<b>Deployment Details:</b><br /><br />");
                                sb3.Append("<table cellpadding='5' cellspacing='0' style='border: 1px solid #fff;font-size: 9pt;font-family:Arial'>");
                                sb3.Append("<td>Code</td>" + "<td>: " + msGetCode + "</td></tr>");
                                sb3.Append("<tr><td>Raised By</td><td>: " + HttpUtility.HtmlEncode(user_code) + "</td></tr>");
                                sb3.Append("<tr><td>Raised Date</td><td>: " + DateTime.Now.ToString() + "</td></tr>");
                                sb3.Append("<tr><td>Stage</td><td>: " + HttpUtility.HtmlEncode(values.stage) + "</td></tr>");
                                sb3.Append("<tr><td>Client</td><td>: " + HttpUtility.HtmlEncode(values.client_name) + "</td></tr>");
                                sb3.Append("<tr><td>Modules</td><td>: " + HttpUtility.HtmlEncode(project_name) + "</td></tr>");
                                sb3.Append("<tr><td>Priority</td><td>: <b>" + HttpUtility.HtmlEncode(values.priority) + "</b></td></tr></table><br /><br />");
                                sb3.Append("Thanks and regards,<br />Deployment Team.<br /><br />");
                                sb3.Append("Click <a href='" + ConfigurationManager.AppSettings["protocol"].ToString() + ConfigurationManager.AppSettings["host"].ToString() + "/v1'>here</a> to review Deployment Details");
                                sb3.Append("<br />* It is a System triggered mail. Please Do not Reply");
                                msg = sb3.ToString();
                                objODBCDataReader.Close();
                                objcmnfunctions.mail(to, sub, msg);
                                sb3.Clear();
                            }
                        }
                    }
                    else
                    {
                        to = "deployment@vcidex.com";
                        sub = "Deployment Pending -" + HttpUtility.HtmlEncode(values.stage);
                        msg = "Record added for deployment with the deployment code(<b>" + msGetCode +
                             "</b>).<br />Deploy the files as soon as possible and update the status." +
                             " <br /><br /> * It is a System triggered mail. Please Do not Reply";
                        objcmnfunctions.mail(to, sub, msg);


                        sb1.Append("*" + HttpUtility.HtmlEncode(user_code) + "*");
                        sb1.Append(" wants to deploy the ");
                        sb1.Append(HttpUtility.HtmlEncode(project_name));
                        sb1.Append(" module(s) on ");
                        sb1.Append(HttpUtility.HtmlEncode(values.stage));
                        sb1.Append(" server for ");
                        sb1.Append(HttpUtility.HtmlEncode(values.client_name));
                        sb1.AppendLine();
                        sb1.AppendLine();
                        sb1.Append("_Description_:- ");
                        sb1.AppendLine();
                        sb1.AppendLine();
                        sb1.Append(HttpUtility.HtmlEncode(values.description));
                        SendGroupMessage(sb1.ToString());
                        sb1.Clear();
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
            
        }

        public bool DaGetSummary(summary_list values, string employee_gid)
        {
           try
            {
                msSQL = " SELECT * from prj_trn_temployee2projectteam a" +
                   " LEFT JOIN prj_trn_tprojectteammanager b ON a.projectteam_gid=b.projectteam_gid" +
                   " LEFT JOIN prj_trn_tprojectteam  c ON b.projectteam_gid=c.projectteam_gid" +
                   " WHERE c.projectteam_name='Deployment' AND (a.employee_gid='" + employee_gid + "' or b.projectteam_manager='" + employee_gid + "')";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows)
                {
                    values.deployer = true;
                }
                else
                {
                    values.deployer = false;
                }
                objODBCDataReader.Close();
                msSQL = " SELECT a.approval_pending,b.approval_rejected,c.deployment_done,d.deployment_pending,e.deployment_rejected FROM( " +
                        " (SELECT COUNT(status) AS approval_pending FROM its_trn_tdeployment WHERE status = 'Approval Pending') AS a, " +
                        " (SELECT COUNT(status) AS approval_rejected FROM its_trn_tdeployment WHERE status = 'Approval Rejected') AS b, " +
                        " (SELECT COUNT(status) AS deployment_done FROM its_trn_tdeployment WHERE status = 'Deployment Done') AS c, " +
                        " (SELECT COUNT(status) AS deployment_pending FROM its_trn_tdeployment WHERE status = 'Deployment Pending') AS d, " +
                        " (SELECT COUNT(status) AS deployment_rejected FROM its_trn_tdeployment WHERE status = 'Deployment Rejected') AS e)";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows)
                {
                    objODBCDataReader.Read();
                    values.count_approvalPending = objODBCDataReader["approval_pending"].ToString();
                    values.count_approvalRejected = objODBCDataReader["approval_rejected"].ToString();
                    values.count_deploymentDone = objODBCDataReader["deployment_done"].ToString();
                    values.count_deploymentPending = objODBCDataReader["deployment_pending"].ToString();
                    values.count_deploymentRejected = objODBCDataReader["deployment_rejected"].ToString();
                }
                objODBCDataReader.Close();
                msSQL = " SELECT a.live,b.uat,c.test FROM ( " +
                        " (SELECT COUNT(deployment_type) AS live FROM its_trn_tdeployment WHERE deployment_type = 'LIVE') AS a, " +
                        " (SELECT COUNT(deployment_type) AS uat FROM its_trn_tdeployment WHERE deployment_type = 'UAT') AS b, " +
                        " (SELECT COUNT(deployment_type) AS test FROM its_trn_tdeployment WHERE deployment_type = 'TEST') AS c) ";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows)
                {
                    objODBCDataReader.Read();
                    values.count_live = objODBCDataReader["live"].ToString();
                    values.count_uat = objODBCDataReader["uat"].ToString();
                    values.count_test = objODBCDataReader["test"].ToString();
                }
                objODBCDataReader.Close();

                msSQL = " SELECT deployment_gid,deployment_code,deployment_type,request_from,date_format(created_date, '%d/%m/%Y %H:%i:%s') as created_date,customer_name,project_name,status FROM its_trn_tdeployment ORDER BY created_date DESC ";
                dt_summary = objdbconn.GetDataTable(msSQL);
                if (dt_summary.Rows.Count != 0)
                {
                    values.summaries = dt_summary.AsEnumerable().Select(row =>
                    new summary
                    {
                        deployment_gid = row["deployment_gid"].ToString(),
                        deployment_code = row["deployment_code"].ToString(),
                        created_date = row["created_date"].ToString(),
                        stage = row["deployment_type"].ToString(),
                        client_name = row["customer_name"].ToString(),
                        module_name = row["project_name"].ToString(),
                        status = row["status"].ToString(),
                        raised_by = row["request_from"].ToString()
                    }).ToList();
                    dt_summary.Dispose();
                    return true;
                }
                else
                {
                    dt_summary.Dispose();
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        public bool DaEditDeployment(string id, edit values)
        {
            try
            {
                msSQL = " SELECT customer_gid, project_gid,deployment_type,need,pages,reports,description,priority FROM its_trn_tdeployment WHERE deployment_gid='" + id + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows)
                {
                    objODBCDataReader.Read();
                    values.client_gid = objODBCDataReader["customer_gid"].ToString();
                    values.module_gid = objODBCDataReader["project_gid"].ToString();
                    values.stage = objODBCDataReader["deployment_type"].ToString();
                    values.need = objODBCDataReader["need"].ToString();
                    values.pages = objODBCDataReader["pages"].ToString();
                    values.reports = objODBCDataReader["reports"].ToString();
                    values.description = objODBCDataReader["description"].ToString();
                    values.priority = objODBCDataReader["priority"].ToString();
                    objODBCDataReader.Close();
                    msSQL = " SELECT approver_gid FROM its_trn_tdeploymentapproval WHERE deployment_gid='" + id + "'";
                    dt_employee = objdbconn.GetDataTable(msSQL);
                    values.approvals = dt_employee.AsEnumerable().Select(row => new approval { employee_gid = row["approver_gid"].ToString() }).ToList(); dt_employee.Dispose();
                    return true;
                }
                else
                {
                    objODBCDataReader.Close();
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool DaUpdateDeployment(string employee_gid, string user_gid, update values)
        {
            try
            {
                for (int i = 0; i < values.projects.Count; i++)
                {
                    sb1.Append(values.projects[i].project_gid + ",");
                    sb2.Append(values.projects[i].project_name + ",");
                }
                project_gid = sb1.ToString();
                project_name = sb2.ToString();
                project_gid = project_gid.Trim(",".ToCharArray());
                project_name = project_name.Trim(",".ToCharArray());
                msSQL = " UPDATE its_trn_tdeployment SET " +
                        " customer_gid='" + values.client_gid + "'," +
                        " customer_name='" + values.client_name + "'," +
                        " deployment_type='" + values.stage + "'," +
                        " project_gid='" + project_gid + "'," +
                        " project_name='" + project_name + "',";
                if (values.stage == "LIVE")
                {
                    msSQL += " priority='" + values.priority + "',";
                }
                else
                {
                    msSQL += " priority='',";
                }
                if (values.new_page == true)
                {
                    msSQL += " pages='" + values.pages.Replace("'", "\\'") + "',";
                }
                else
                {
                    msSQL += " pages='',";
                }
                if (values.new_report == true)
                {
                    msSQL += " reports='" + values.reports.Replace("'", "\\'") + "',";
                }
                else
                {
                    msSQL += "reports='',";
                }
                msSQL += " description='" + values.description.Replace("'", "\\'") + "'," +
                         " need='" + values.need + "'," +
                         " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " WHERE deployment_gid='" + values.deployment_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    msGetCode = objdbconn.GetExecuteScalar(" SELECT deployment_code FROM its_trn_tdeployment WHERE deployment_gid='" + values.deployment_gid + "'");
                    if (values.stage == "LIVE")
                    {
                        msSQL = " DELETE FROM its_trn_tdeploymentapproval WHERE deployment_gid='" + values.deployment_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        string user_code = objdbconn.GetExecuteScalar(" SELECT CONCAT(user_code,' / ' ,user_firstname,' ',user_lastname) as user_code FROM adm_mst_tuser WHERE user_gid='" + user_gid + "'");
                        for (int i = 0; i < values.approvals.Count; i++)
                        {
                            msGetApp = objcmnfunctions.GetMasterGID("DEPT");
                            msSQL = " INSERT INTO its_trn_tdeploymentapproval ( " +
                                    " deploymentapproval_gid, " +
                                    " deployment_gid, " +
                                    " approver_gid, " +
                                    " status  " +
                                    " )values ( " +
                                    " '" + msGetApp + "'," +
                                    " '" + values.deployment_gid + "'," +
                                    " '" + values.approvals[i].employee_gid + "'," +
                                    " 'Pending')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            if (values.mailFlag == true)
                            {
                                msSQL = " SELECT CONCAT(b.user_firstname, ' ' ,b.user_lastname) AS username, a.employee_emailid FROM hrm_mst_temployee a " +
                                        " LEFT JOIN adm_mst_tuser b ON a.user_gid=b.user_gid WHERE a.employee_gid='" + values.approvals[i].employee_gid + "'";
                                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                                if (objODBCDataReader.HasRows)
                                {
                                    to = objODBCDataReader["employee_emailid"].ToString();
                                    sb3.Append("Dear " + objODBCDataReader["username"].ToString() + ",<br /><br />");
                                }
                                sub = "Deployment: Waiting for your Approval(Modified)";
                                sb3.Append("Following Modified Deployment needs your Approval to proceed further.<br />");
                                sb3.AppendLine();
                                sb3.Append("Please review the same and take action ASAP. <br /><br />");
                                sb3.AppendLine();
                                sb3.Append("<b>Deployment Details:</b><br /><br />");
                                sb3.Append("<table cellpadding='5' cellspacing='0' style='border: 1px solid #fff;font-size: 9pt;font-family:Arial'>");
                                sb3.Append("<td>Code</td>" + "<td>: " + msGetCode + "</td></tr>");
                                sb3.Append("<tr><td>Modified By</td><td>: " + HttpUtility.HtmlEncode(user_code) + "</td></tr>");
                                sb3.Append("<tr><td>Modified Date</td><td>: " + DateTime.Now.ToString() + "</td></tr>");
                                sb3.Append("<tr><td>Stage</td><td>: " + HttpUtility.HtmlEncode(values.stage) + "</td></tr>");
                                sb3.Append("<tr><td>Client</td><td>: " + HttpUtility.HtmlEncode(values.client_name) + "</td></tr>");
                                sb3.Append("<tr><td>Modules</td><td>: " + HttpUtility.HtmlEncode(project_name) + "</td></tr>");
                                sb3.Append("<tr><td>Priority</td><td>: <b>" + HttpUtility.HtmlEncode(values.priority) + "</b></td></tr></table><br /><br />");
                                sb3.Append("Thanks and regards,<br />Deployment Team.<br /><br />");
                                sb3.Append("Click <a href='" + ConfigurationManager.AppSettings["protocol"].ToString() + ConfigurationManager.AppSettings["host"].ToString() + "/v1'>here</a> to review Deployment Details");
                                sb3.Append("<br />*It is a System triggered mail. Please Do not Reply");
                                msg = sb3.ToString();
                                objODBCDataReader.Close();
                                objcmnfunctions.mail(to, sub, msg);
                                sb3.Clear();
                            }
                        }
                        mnResult = objdbconn.ExecuteNonQuerySQL("UPDATE its_trn_tdeployment SET status='Approval Pending' WHERE deployment_gid='" + values.deployment_gid + "'");
                    }
                    else
                    {
                        to = "deployment@vcidex.com";
                        sub = "Deployment Pending -" + HttpUtility.HtmlEncode(values.stage) + "(Modified)";
                        msg = "Record Modified for deployment with the deployment code(<b>" + msGetCode +
                             "</b>).<br />Deploy the files as soon as possible and update the status." +
                             " <br /><br /> * It is a System triggered mail. Please Do not Reply";
                        objcmnfunctions.mail(to, sub, msg);
                    }
                    values.status = true;
                    return true;
                }
                else
                {
                    values.status = false;
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool DaViewDeployment(string id, string employee_id, view values)
        {
           try
            {
                msSQL = " SELECT deployment_code, customer_name, deployment_type, project_name, pages, reports, description, need, request_from, deployed_by, status, date_format(created_date, '%d/%m/%Y %H:%i:%s') as created_date,updated_by,date_format(updated_date, '%d/%m/%Y %H:%i:%s') as updated_date FROM its_trn_tdeployment " +
                   " WHERE deployment_gid='" + id + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows)
                {
                    objODBCDataReader.Read();
                    values.client_name = objODBCDataReader["customer_name"].ToString();
                    values.deployment_code = objODBCDataReader["deployment_code"].ToString();
                    values.stage = objODBCDataReader["deployment_type"].ToString();
                    values.project_name = objODBCDataReader["project_name"].ToString();
                    values.description = objODBCDataReader["description"].ToString();
                    values.need = objODBCDataReader["need"].ToString();
                    values.request_from = objODBCDataReader["request_from"].ToString();
                    values.status = objODBCDataReader["status"].ToString();
                    values.date = objODBCDataReader["created_date"].ToString();
                    values.employee_gid = employee_id;
                    if (objODBCDataReader["deployed_by"].ToString() == "---")
                    {
                        values.deployed_by = "Not Yet Deployed";
                    }
                    else
                    {
                        values.deployed_by = objODBCDataReader["deployed_by"].ToString();
                    }
                    if (objODBCDataReader["pages"].ToString() == "")
                    {
                        values.pages = "No New Pages";
                    }
                    else
                    {
                        values.pages = objODBCDataReader["pages"].ToString();
                    }
                    if (objODBCDataReader["reports"].ToString() == "")
                    {
                        values.reports = "No New Reports";
                    }
                    else
                    {
                        values.reports = objODBCDataReader["reports"].ToString();
                    }

                    if (objODBCDataReader["updated_by"].ToString() != null)
                    {
                        values.updated_by = objODBCDataReader["updated_by"].ToString();
                        values.updated_date = objODBCDataReader["updated_date"].ToString();
                        objODBCDataReader.Close();
                        msSQL = " SELECT CONCAT(a.user_code, ' / ',a.user_firstname,a.user_lastname) AS updated_by FROM adm_mst_tuser a " +
                                " LEFT JOIN hrm_mst_temployee b ON a.user_gid=b.user_gid WHERE b.employee_gid='" + values.updated_by + "'";
                        values.updated_by = objdbconn.GetExecuteScalar(msSQL);
                    }
                    msSQL = " SELECT a.deploymentapproval_gid,a.status,CONCAT(b.user_code ,' / ',b.user_firstname,b.user_lastname) as employee_name,c.employee_gid FROM " +
                            " its_trn_tdeploymentapproval a LEFT JOIN hrm_mst_temployee c ON c.employee_gid =a.approver_gid " +
                            " LEFT JOIN adm_mst_tuser b ON b.user_gid=c.user_gid " +
                            " WHERE a.deployment_gid='" + id + "'";
                    dt_employee = objdbconn.GetDataTable(msSQL);
                    if (dt_employee.Rows.Count != 0)
                    {
                        values.approvals = dt_employee.AsEnumerable().Select(row =>
                   new approval
                   {
                       employee_gid = row["employee_gid"].ToString(),
                       employee_name = row["employee_name"].ToString(),
                       depAppGid = row["deploymentapproval_gid"].ToString(),
                       status = row["status"].ToString()
                   }).ToList();

                        dt_employee.Dispose();
                    }


                    return true;
                }
                else
                {
                    objODBCDataReader.Close();
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool DaDelRecord(string id)
        {
           try
            {
                msSQL = " DELETE FROM its_trn_tdeployment WHERE deployment_gid='" + id + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    msSQL = " DELETE FROM its_trn_tdeploymentapproval WHERE deployment_gid='" + id + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool DaGetEmployee(employee values)
        {
            try
            {
                msSQL = " SELECT a.employee_gid, CONCAT(b.user_code,' / ',b.user_firstname,' ',b.user_lastname) as employee_name FROM hrm_mst_temployee a " +
                    " LEFT JOIN adm_mst_tuser b ON a.user_gid=b.user_gid WHERE b.user_status='Y'";
                dt_employee = objdbconn.GetDataTable(msSQL);
                if (dt_employee.Rows.Count != 0)
                {
                    values.employee_list = dt_employee.AsEnumerable().Select(row =>
                    new employee_list
                    {
                        employee_gid = row["employee_gid"].ToString(),
                        employee_name = row["employee_name"].ToString()
                    }).ToList();
                    dt_employee.Dispose();
                    return true;
                }
                else
                {
                    dt_employee.Dispose();
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool Approve(string id)
        {
           try
            {
                msSQL = " UPDATE its_trn_tdeploymentapproval SET status='Approved' WHERE deploymentapproval_gid='" + id + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    msSQL = " SELECT deploymentapproval_gid FROM its_trn_tdeploymentapproval WHERE status IN ('Pending','Rejected') AND deployment_gid= " +
                            " (SELECT deployment_gid from its_trn_tdeploymentapproval WHERE deploymentapproval_gid='" + id + "')";
                    objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDataReader.HasRows)
                    {
                        objODBCDataReader.Close();
                        msSQL = " SELECT deploymentapproval_gid FROM its_trn_tdeploymentapproval WHERE status ='Rejected' AND deployment_gid= " +
                                " (SELECT deployment_gid from its_trn_tdeploymentapproval WHERE deploymentapproval_gid='" + id + "')";
                        objODBCDataReader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDataReader.HasRows)
                        {
                            objODBCDataReader.Close();
                            msSQL = " UPDATE its_trn_tdeployment SET status='Approval Rejected' WHERE deployment_gid=(SELECT deployment_gid from its_trn_tdeploymentapproval " +
                            " WHERE deploymentapproval_gid='" + id + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                        else
                        {
                            objODBCDataReader.Close();
                            msSQL = " UPDATE its_trn_tdeployment SET status='Approval Pending' WHERE deployment_gid=(SELECT deployment_gid from its_trn_tdeploymentapproval " +
                               " WHERE deploymentapproval_gid='" + id + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    else
                    {
                        objODBCDataReader.Close();
                        msSQL = " UPDATE its_trn_tdeployment SET status='Deployment Pending' WHERE deployment_gid=(SELECT deployment_gid from its_trn_tdeploymentapproval " +
                                " WHERE deploymentapproval_gid='" + id + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult != 0)
                        {
                            msSQL = " SELECT deployment_code FROM its_trn_tdeployment WHERE deployment_gid=(SELECT deployment_gid from its_trn_tdeploymentapproval " +
                                    " WHERE deploymentapproval_gid='" + id + "')";
                            msGetCode = objdbconn.GetExecuteScalar(msSQL);
                            to = "deployment@vcidex.com";
                            sub = "Deployment Pending - LIVE";
                            msg = "All Authorities approved the Live deployment of <b>" + msGetCode + "</b>" +
                                 " .<br />Deploy the files as soon as possible and update the status " +
                                  " <br /><br /> * It is a System triggered mail. Please Do not Reply";
                            objcmnfunctions.mail(to, sub, msg);
                            msSQL = " SELECT customer_name,request_from,project_name,deployment_type,description FROM its_trn_tdeployment WHERE deployment_gid= " +
                                    " (SELECT deployment_gid FROM its_trn_tdeploymentapproval WHERE deploymentapproval_gid='" + id + "')";
                            objODBCDataReader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDataReader.HasRows)
                            {
                                sb1.Append("*" + HttpUtility.HtmlEncode(objODBCDataReader["request_from"].ToString()) + "*");
                                sb1.Append(" wants to deploy the ");
                                sb1.Append(HttpUtility.HtmlEncode(objODBCDataReader["project_name"].ToString()));
                                sb1.Append(" module(s) on ");
                                sb1.Append(HttpUtility.HtmlEncode(objODBCDataReader["deployment_type"].ToString()));
                                sb1.Append(" server for ");
                                sb1.Append(HttpUtility.HtmlEncode(objODBCDataReader["customer_name"].ToString()));
                                sb1.AppendLine();
                                sb1.AppendLine();
                                sb1.Append("_Approval Status_:- ");
                                sb1.AppendLine();
                                sb1.AppendLine();
                                sb1.Append("All Authorities has been approved the LIVE Deployment");
                                sb1.AppendLine();
                                sb1.AppendLine();
                                sb1.Append("_Description_:- ");
                                sb1.AppendLine();
                                sb1.AppendLine();
                                sb1.Append(HttpUtility.HtmlEncode(objODBCDataReader["description"].ToString()));
                                SendGroupMessage(sb1.ToString());
                                sb1.Clear();
                            }
                        }
                    }
                    objODBCDataReader.Close();
                    if (mnResult != 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool Reject(string id)
        {
           try
            {
                msSQL = " UPDATE its_trn_tdeploymentapproval SET status='Rejected' WHERE deploymentapproval_gid='" + id + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    msSQL = " UPDATE its_trn_tdeployment SET status='Approval Rejected' WHERE deployment_gid=(SELECT deployment_gid from its_trn_tdeploymentapproval " +
                               " WHERE deploymentapproval_gid='" + id + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    msSQL = " SELECT a.deployment_code,c.employee_emailid,CONCAT(d.user_code,' / ',d.user_firstname,d.user_lastname) AS employee_name FROM its_trn_tdeploymentapproval b" +
                            " LEFT JOIN its_trn_tdeployment a ON a.deployment_gid=b.deployment_gid" +
                            " LEFT JOIN hrm_mst_temployee c ON b.approver_gid=c.employee_gid " +
                            " LEFT JOIN adm_mst_tuser d ON c.user_gid=d.user_gid " +
                            " WHERE deploymentapproval_gid='" + id + "'";
                    objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDataReader.HasRows)
                    {
                        objODBCDataReader.Read();
                        to = objODBCDataReader["employee_emailid"].ToString();
                        sub = "Deployment Approval Rejected";
                        msg = "Your Deployment request has been rejected by <b>" + HttpUtility.HtmlEncode(objODBCDataReader["employee_name"].ToString()) +
                              "</b><br /><br /><br /> Deployment Code : <b>" + HttpUtility.HtmlEncode(objODBCDataReader["deployment_code"].ToString()) +
                              " </b><br /><br /><br /> * It is System triggered Mail. Do not reply.";

                        objODBCDataReader.Close();
                        objcmnfunctions.mail(to, sub, msg);

                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateDepStatus(deploymentStatus values, string employee_gid)
        {
            try
            {
                string deployer = string.Empty;
                msSQL = " SELECT CONCAT(a.user_code, ' /',a.user_firstname,a.user_lastname) as deployer FROM adm_mst_tuser a " +
                      " LEFT JOIN hrm_mst_temployee b ON a.user_gid=b.user_gid WHERE b.employee_gid='" + employee_gid + "'";
                deployer = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " UPDATE its_trn_tdeployment SET ";
                if (values.statusDep == "deployed")
                {
                    msSQL += "status='Deployment Done',";
                }
                else
                {
                    msSQL += "status='Deployment Rejected',";
                }
                msSQL += " deployed_by='" + deployer + "' WHERE deployment_gid='" + values.deployment_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (values.mailDep == "true")
                {
                    string type = string.Empty;
                    string requester = string.Empty;
                    type = objdbconn.GetExecuteScalar("SELECT deployment_type FROM its_trn_tdeployment WHERE deployment_gid='" + values.deployment_gid + "'");

                    if (type == "LIVE")
                    {
                        msSQL = " SELECT deployment_code,request_from,created_date,created_by,deployment_type,customer_name,project_name,priority,deployed_by" +
                                " FROM its_trn_tdeployment WHERE deployment_gid='" + values.deployment_gid + "'";
                        objODBCDataReader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDataReader.HasRows)
                        {
                            var userName = objODBCDataReader["request_from"].ToString().Split('/');
                            sb3.Append("Dear " + HttpUtility.HtmlEncode(userName[1]) + ",<br /><br />");
                            if (values.statusDep == "deployed")
                            {
                                sub = type + " Deployment Done ";
                                sb3.Append("Your files with the following details has been deployed successfully.<br /><br />");
                            }
                            else
                            {
                                sub = type + " Deployment Rejected";
                                sb3.Append("Your files with the following details has been Rejected by the Deployment Team.<br /><br />");
                            }
                            sb3.AppendLine();
                            sb3.Append("<b>Deployment Details:</b><br /><br />");
                            sb3.Append("<table cellpadding='5' cellspacing='0' style='border: 1px solid #fff;font-size: 9pt;font-family:Arial'>");
                            sb3.Append("<td>Code</td>" + "<td>:" + HttpUtility.HtmlEncode(objODBCDataReader["deployment_code"].ToString()) + "</td></tr>");
                            sb3.Append("<tr><td>Raised By</td><td>:" + HttpUtility.HtmlEncode(objODBCDataReader["request_from"].ToString()) + "</td></tr>");
                            sb3.Append("<tr><td>Raised Date</td><td>:" + HttpUtility.HtmlEncode(objODBCDataReader["created_date"].ToString()) + "</td></tr>");
                            sb3.Append("<tr><td>Stage</td><td>:" + "LIVE" + "</td></tr>");
                            sb3.Append("<tr><td>Client</td><td>:" + HttpUtility.HtmlEncode(objODBCDataReader["customer_name"].ToString()) + "</td></tr>");
                            sb3.Append("<tr><td>Modules</td><td>:" + HttpUtility.HtmlEncode(objODBCDataReader["project_name"].ToString()) + "</td></tr>");
                            sb3.Append("<tr><td>Deployed By</td><td>:" + HttpUtility.HtmlEncode(objODBCDataReader["deployed_by"].ToString()) + "</td></tr>");
                            sb3.Append("<tr><td>Priority</td><td>:<b>" + HttpUtility.HtmlEncode(objODBCDataReader["priority"].ToString()) + "</b></td></tr></table><br /><br />");
                            sb3.Append("Thanks and regards,<br />Deployment Team.<br /><br />");
                            sb3.Append("Click <a href='" + ConfigurationManager.AppSettings["protocol"].ToString() + ConfigurationManager.AppSettings["host"].ToString() + "/v1'>here</a> to review Deployment Details");
                            sb3.Append("<br />* It is a System triggered mail. Please Do not Reply");
                            msg = sb3.ToString();
                            requester = objODBCDataReader["created_by"].ToString();
                            objODBCDataReader.Close();
                            to = objdbconn.GetExecuteScalar(" SELECT employee_emailid FROM hrm_mst_temployee WHERE employee_gid='" + requester + "'");
                            objcmnfunctions.mail(to, sub, msg);
                            sb3.Clear();
                        }

                    }
                    else
                    {
                        msSQL = " SELECT deployment_code,request_from,created_date,deployment_type,customer_name,created_by,project_name,deployed_by" +
                                " FROM its_trn_tdeployment WHERE deployment_gid='" + values.deployment_gid + "'";
                        objODBCDataReader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDataReader.HasRows)
                        {
                            var userName = objODBCDataReader["request_from"].ToString().Split('/');
                            sb3.Append("Dear " + HttpUtility.HtmlEncode(userName[1]) + ",<br /><br />");
                            if (values.statusDep == "deployed")
                            {
                                sub = type + " Deployment Done ";
                                sb3.Append("Your files with the following details has been deployed successfully.<br />");
                            }
                            else
                            {
                                sub = type + " Deployment Rejected";
                                sb3.Append("Your files with the following details has been Rejected by the Deployment Team.<br />");
                            }
                            sb3.AppendLine();
                            sb3.Append("<b>Deployment Details:</b><br />");
                            sb3.Append("<table cellpadding='5' cellspacing='0' style='border: 1px solid #fff;font-size: 9pt;font-family:Arial'>");
                            sb3.Append("<td>Code</td>" + "<td>:" + HttpUtility.HtmlEncode(objODBCDataReader["deployment_code"].ToString()) + "</td></tr>");
                            sb3.Append("<tr><td>Raised By</td><td>:" + HttpUtility.HtmlEncode(objODBCDataReader["request_from"].ToString()) + "</td></tr>");
                            sb3.Append("<tr><td>Raised Date</td><td>:" + objODBCDataReader["created_date"].ToString() + "</td></tr>");
                            sb3.Append("<tr><td>Stage</td><td>:" + HttpUtility.HtmlEncode(type) + "</td></tr>");
                            sb3.Append("<tr><td>Client</td><td>:" + HttpUtility.HtmlEncode(objODBCDataReader["customer_name"].ToString()) + "</td></tr>");
                            sb3.Append("<tr><td>Modules</td><td>:" + HttpUtility.HtmlEncode(objODBCDataReader["project_name"].ToString()) + "</td></tr>");
                            sb3.Append("<tr><td>Deployed By</td><td>:" + HttpUtility.HtmlEncode(objODBCDataReader["deployed_by"].ToString()) + "</td></tr></td></tr></table><br /><br />");
                            sb3.Append("Thanks and regards,<br />Deployment Team.<br /><br />");
                            sb3.Append("Click <a href='" + ConfigurationManager.AppSettings["protocol"].ToString() + ConfigurationManager.AppSettings["host"].ToString() + "/v1'>here</a> to review Deployment Details");
                            sb3.Append("<br />* It is a System triggered mail. Please Do not Reply");
                            msg = sb3.ToString();
                            requester = HttpUtility.HtmlEncode(objODBCDataReader["created_by"].ToString());
                            objODBCDataReader.Close();
                            to = objdbconn.GetExecuteScalar(" SELECT employee_emailid FROM hrm_mst_temployee WHERE employee_gid='" + HttpUtility.HtmlEncode(requester) + "'");
                            objcmnfunctions.mail(to, sub, msg);
                            sb3.Clear();
                        }
                    }

                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void SendGroupMessage(string msg)
        {
            msSQL = " SELECT whatsapp_client_id,whatsapp_instance_id,whatsapp_client_secret FROM adm_mst_tcompany ";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if ((objODBCDataReader.HasRows == true))
            {
                INSTANCE_ID = objODBCDataReader["whatsapp_instance_id"].ToString();
                CLIENT_ID = objODBCDataReader["whatsapp_client_id"].ToString();
                CLIENT_SECRET = objODBCDataReader["whatsapp_client_secret"].ToString();
            }
            objODBCDataReader.Close();
            GROUP_API_URL = ("http://api.whatsmate.net/v3/whatsapp/group/text/message/" + INSTANCE_ID);

            WebClient webClient = new WebClient();
            try
            {
                GroupPayload payloadObj = new GroupPayload
                {
                    group_admin = ConfigurationManager.AppSettings["group_admin"].ToString(),
                    group_name = ConfigurationManager.AppSettings["group_name"].ToString(),
                    message = msg
                };
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string postData = serializer.Serialize(payloadObj);
                webClient.Headers["content-type"] = "application/json";
                webClient.Headers["X-WM-CLIENT-ID"] = CLIENT_ID;
                webClient.Headers["X-WM-CLIENT-SECRET"] = CLIENT_SECRET;
                webClient.Encoding = Encoding.UTF8;
                string response = webClient.UploadString(GROUP_API_URL, postData);
            }
            catch (WebException webEx)
            {

            }
        }
    }
}