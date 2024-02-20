using ems.mastersamagro.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Web;
using System.IO;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Collections;

namespace ems.mastersamagro.DataAccess
{
    /// <summary>
    /// This DataAccess provide access for various events (CC Approval mail and Approved Mail
    /// events) in CC Mail Approval
    /// </summary>
    /// <remarks>Written by Premchander.K </remarks>
    public class DaAgrTrnCCMailApproval
    {

        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        HttpPostedFile httpPostedFile;
        OdbcDataReader objODBCDatareader, objODBCDatareader1;
        DataTable dt_datatable;
        string msSQL, msGetGid, msGetDocumentGid, msGetGid1, msGetGid2;
        int mnResult, ls_port, MailFlag, k;
        string lspath, lssession_user, lsdocument_attached;
        public string ls_server, ls_username, ls_password, tomail_id, tomail_id1, tomail_id2, sub, body, employeename, cc_mailid, employee_reporting_to;
        public string[] lsCCReceipients;
        public string lsBccmail_id;
        public string[] lsBCCReceipients;
        string lsemployee_name, lsemployee_gid, lsccmeeting_date, lsccgroup_name, lsloanfacility_amount, lscustomer_name, lsccadmin_name, lsapplication_no;
        string sToken = string.Empty;
        string lsrequested_by, message;
        Random rand = new Random();
        string lsto_mail, frommail_id, lscc_mail, strBCC, lsbcc_mail, lsccadmin_gid, approver, lscontent = string.Empty;
        string lsdatabase = ConfigurationManager.AppSettings["externalportal"].ToString();

        public void DaGetApprovalMailList(string approval_token, MdlMstCCschedule values)
        {

            msSQL = "select application_gid from " + lsdatabase + ".agr_trn_tccapproval where approval_token='" + approval_token + "'  and approval_status='Pending'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {

                msSQL = " select distinct a.application_gid,a.application_no,d.ccadmin_name,a.customer_name,e.loanfacility_amount,  date_format(d.ccmeeting_date,'%d-%m-%Y %h:%i %p') as ccmeeting_date, " +
                    " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as rm_name, a.mom_description" +
                    " from " + lsdatabase + ".agr_mst_tapplication a " +
                    " left join  " + lsdatabase + ".hrm_mst_temployee b on a.created_by = b.employee_gid " +
                    " left join  " + lsdatabase + ".adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " left join  " + lsdatabase + ".agr_mst_tccschedulemeeting d on a.application_gid = d.application_gid " +
                    " left join  " + lsdatabase + ".agr_mst_tapplication2loan e on a.application_gid = e.application_gid " +
                      //" left join  " + lsdatabase + ".ocs_mst_tapplication f on a.application_gid = f.application_gid " +
                      " left join  " + lsdatabase + ".agr_trn_tccapproval g on a.application_gid = g.application_gid " +
                   " where g.approval_token='" + approval_token + "'";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.application_no = objODBCDatareader["application_no"].ToString();
                    values.ccadmin_name = objODBCDatareader["ccadmin_name"].ToString();
                    values.customer_name = objODBCDatareader["customer_name"].ToString();
                    values.loanfacility_amount = objODBCDatareader["loanfacility_amount"].ToString();
                    values.ccmeeting_date = objODBCDatareader["ccmeeting_date"].ToString();
                    values.rm_name = objODBCDatareader["rm_name"].ToString();
                    values.application_gid = objODBCDatareader["application_gid"].ToString();
                    values.mom_description = objODBCDatareader["mom_description"].ToString();

                }
                objODBCDatareader.Close();
                values.status = true;
            }
            else
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Already Approved / Rejected..!";
            }

        }

        public void DaPostCCMailApproved(MdlMstCCschedule values)
        {

            msSQL = " select approval_gid from " + lsdatabase + ".agr_trn_tccapproval  " +
                        " where approval_token='" + values.approval_token + "'";
            approver = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select ccmeeting2members_gid from agr_mst_tccmeeting2members a " +
                  " where ccmember_gid='" + approver + "'  and application_gid='" + values.application_gid + "' and attendance_status ='P' and  ccapproval_flag ='Y' and  ccmail_flag ='Y'";
            objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader1.HasRows == true)
            {
                values.status = true;
            }
            else
            {

                values.message = "Your Approval Requests have been Canceled";
                values.status = false;
                return;

            }
            objODBCDatareader1.Close();

            msSQL = " update " + lsdatabase + ".agr_trn_tccapproval set approval_status='" + values.approval_status + "',  " +
                    " approval_remarks='" + values.approval_remarks.Replace("'", "\\'") + "'," +
                    " approved_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    " where approval_token='" + values.approval_token + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                msSQL = " select approval_gid from " + lsdatabase + ".agr_trn_tccapproval  " +
                        " where approval_token='" + values.approval_token + "'";
                approver = objdbconn.GetExecuteScalar(msSQL);

                if (approver == null || approver == "")
                {
                }
                else
                {
                    msSQL = " update " + lsdatabase + ".agr_mst_tccmeeting2members set approval_status='" + values.approval_status + "',";
                    if (values.approval_remarks == "" || values.approval_remarks == null)
                    {
                        msSQL += " approval_remarks='',";
                    }
                    else
                    {
                        msSQL += " approval_remarks='" + values.approval_remarks.Replace("'", "") + "',";
                    }
                    msSQL += " updated_by='" + approver + "'," +
                            " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                            " approved_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                            " where ccmember_gid='" + approver + "'  and application_gid='" + values.application_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                if (values.approval_status == "Rejected")
                {//If Rejects, status updation in applictaion table
                    msSQL = " update " + lsdatabase + ".agr_mst_tapplication set approval_status='CC Rejected',";
                    if (values.approval_remarks == "" || values.approval_remarks == null)
                    {
                        msSQL += " cc_remarks='',";
                    }
                    else
                    {
                        msSQL += " cc_remarks='" + values.approval_remarks.Replace("'", "") + "',";
                    }
                    msSQL += " updated_by='" + approver + "'," +
                           " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                           " cccompleted_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                           //" cccompleted_flag='Y'" +
                           " where  application_gid='" + values.application_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                else
                {//Approval Condition checking for all CC members 
                    msSQL = "select a.application_gid from " + lsdatabase + ".agr_mst_tccmeeting2members a " +
                            " left join " + lsdatabase + ".agr_mst_tapplication f on a.application_gid = f.application_gid " +
                            " where a.application_gid='" + values.application_gid + "' " +
                            " and a.attendance_status='P' and a.ccapproval_flag = 'Y' and  (  a.approval_status='Pending'  or  a.approval_status='Rejected') and (f.approval_status ='CC Rejected' or f.approval_status ='Submitted to CC' ) ";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == false)
                    {

                        msSQL = " update " + lsdatabase + ".agr_mst_tapplication set approval_status='CC Approved', process_type = null,";
                        if (values.approval_remarks == "" || values.approval_remarks == null)
                        {
                            msSQL += " cc_remarks='',";
                        }
                        else
                        {
                            msSQL += " cc_remarks='" + values.approval_remarks.Replace("'", "") + "',";
                        }
                        msSQL += " updated_by='" + approver + "'," +
                                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                 " cccompleted_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                 " cccompleted_flag='Y'" +
                                 " where  application_gid='" + values.application_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    }
                }


                msSQL = " select a.application_gid from " + lsdatabase + ".agr_mst_tccmeeting2members a  " +
                        " left join " + lsdatabase + ".agr_mst_tapplication f on a.application_gid = f.application_gid " +
                        " where a.application_gid='" + values.application_gid + "' and a.attendance_status = 'P'" +
                        " and a.ccapproval_flag = 'Y' and(a.approval_status = 'Pending')";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == false)
                {
                    msSQL = " update " + lsdatabase + ".agr_mst_tapplication set cccompleted_flag='Y'," +
                             " updated_by='" + approver + "'," +
                             " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                             " cccompleted_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                             " where  application_gid='" + values.application_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                }
                values.status = true;
                values.message = "CC Approval Status Updated Successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }

        }
    }
}