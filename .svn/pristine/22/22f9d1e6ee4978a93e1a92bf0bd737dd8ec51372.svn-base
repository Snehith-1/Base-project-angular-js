using ems.master.Models;
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

/// <summary>
/// (It's used for pages in CC Schedule Mail Approval)CCMailApproval DataAccess Class accessed by API methods from related Controller class and is returning relevant response to client.
/// </summary>
/// <remarks>Written by Sumala,Logapriya and Abilash</remarks>

namespace ems.master.DataAccess
{
    public class DaCCMailApproval
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        HttpPostedFile httpPostedFile;
        OdbcDataReader objODBCDatareader, objODBCDatareader1;
        DataTable dt_datatable;
        string msSQL, msGetGid, msGetDocumentGid, msGetGid1, msGetGid2;
        int mnResult, ls_port, MailFlag, k;
        string lspath, lssession_user, lsdocument_attached, institution_gid, contact_gid ;
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
        bool status, mail_send_result, mail_details_result;
        string applicant_type, applicant_name, arn_no, overalllimit_amount, rm_name, rm_gid, rm_mobile_no, rh_mobile_no, ch_mobile_no, bh_mobile_no;
        // RM, Cluster Head, Regional Head, Zonal Head, Business Head
        string rm_mailid, ch_mailid, rh_mailid, zh_mailid, bh_mailid, cm_mailid, rcm_mailid, ncm_mailid;
        string ch_gid, rh_gid, zh_gid, bh_gid, cm_gid, rcm_gid, ncm_gid, rh_name, bh_name, ch_name , result;


        public void DaGetApprovalMailList(string approval_token, MdlMstCCschedule values)
        {

            msSQL = "select application_gid from " + lsdatabase + ".ocs_trn_tccapproval where approval_token='" + approval_token + "'  and approval_status='Pending'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {

                msSQL = " select distinct a.application_gid,a.application_no,d.ccadmin_name,a.customer_name,e.loanfacility_amount,  date_format(d.ccmeeting_date,'%d-%m-%Y %h:%i %p') as ccmeeting_date, " +
                    " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as rm_name,a.mom_description" +
                    " from " + lsdatabase + ".ocs_mst_tapplication a " +
                    " left join  " + lsdatabase + ".hrm_mst_temployee b on a.created_by = b.employee_gid " +
                    " left join  " + lsdatabase + ".adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " left join  " + lsdatabase + ".ocs_mst_tccschedulemeeting d on a.application_gid = d.application_gid " +
                    " left join  " + lsdatabase + ".ocs_mst_tapplication2loan e on a.application_gid = e.application_gid " +
                      //" left join  " + lsdatabase + ".ocs_mst_tapplication f on a.application_gid = f.application_gid " +
                      " left join  " + lsdatabase + ".ocs_trn_tccapproval g on a.application_gid = g.application_gid " +
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
            msSQL = " select approval_gid from " + lsdatabase + ".ocs_trn_tccapproval  " +
                        " where approval_token='" + values.approval_token + "'";
            approver = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select ccmeeting2members_gid from ocs_mst_tccmeeting2members a " +
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

            msSQL = " update " + lsdatabase + ".ocs_trn_tccapproval set approval_status='" + values.approval_status + "',  " +
                    " approval_remarks='" + values.approval_remarks.Replace("'", "\\'") + "'," +
                    " approved_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    " where approval_token='" + values.approval_token + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                msSQL = " select approval_gid from " + lsdatabase + ".ocs_trn_tccapproval  " +
                        " where approval_token='" + values.approval_token + "'";
                approver = objdbconn.GetExecuteScalar(msSQL);

                if (approver == null || approver == "")
                {
                }
                else
                {
                    msSQL = " update " + lsdatabase + ".ocs_mst_tccmeeting2members set approval_status='" + values.approval_status + "',";
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
                    msSQL = " update " + lsdatabase + ".ocs_mst_tapplication set approval_status='CC Rejected',";
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
                    //bool customer_mail_status = Daccrejectcustomermail(values.application_gid);
                }
                else
                {//Approval Condition checking for all CC members 
                    msSQL = "select ccmeeting2members_gid from " + lsdatabase + ".ocs_mst_tccmeeting2members a " +
                            " left join " + lsdatabase + ".ocs_mst_tapplication f on a.application_gid = f.application_gid " +
                            " where a.application_gid='" + values.application_gid + "' " +
                            " and a.attendance_status='P' and a.ccapproval_flag = 'Y' and  (  a.approval_status='Pending'  or  a.approval_status='Rejected') and (f.approval_status ='CC Rejected' or f.approval_status ='Submitted to CC' ) ";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == false)
                    {

                        msSQL = " update " + lsdatabase + ".ocs_mst_tapplication set approval_status='CC Approved', process_type = null,";
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
                        //bool customer_mail_status = Daccapprovedcustomermail(values.application_gid);
                    }
                }


                msSQL = " select ccmeeting2members_gid from " + lsdatabase + ".ocs_mst_tccmeeting2members a  " +
                        " left join " + lsdatabase + ".ocs_mst_tapplication f on a.application_gid = f.application_gid " +
                        " where a.application_gid='" + values.application_gid + "' and a.attendance_status = 'P'" +
                        " and a.ccapproval_flag = 'Y' and(a.approval_status = 'Pending')";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == false)
                {
                    msSQL = " update " + lsdatabase + ".ocs_mst_tapplication set cccompleted_flag='Y'," +
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
        public bool Daccapprovedcustomermail(string application_gid)
        {
            msSQL = " select application_no from " + lsdatabase + ".ocs_mst_tapplication where application_gid = '" + application_gid + "'";
            arn_no = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select applicant_type from " + lsdatabase + ".ocs_mst_tapplication where application_gid = '" + application_gid + "'";
            applicant_type = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select relationshipmanager_gid from " + lsdatabase + ".ocs_mst_tapplication where application_gid = '" + application_gid + "'";
            rm_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select clustermanager_gid from " + lsdatabase + ".ocs_mst_tapplication where application_gid = '" + application_gid + "'";
            ch_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select regionalhead_gid from " + lsdatabase + ".ocs_mst_tapplication where application_gid = '" + application_gid + "'";
            rh_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select businesshead_gid from " + lsdatabase + ".ocs_mst_tapplication where application_gid = '" + application_gid + "'";
            bh_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select creditmanager_gid from " + lsdatabase + ".ocs_mst_tapplication where application_gid = '" + application_gid + "'";
            cm_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select creditregionalmanager_gid from " + lsdatabase + ".ocs_mst_tapplication where application_gid = '" + application_gid + "'";
            rcm_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select creditnationalmanager_gid from " + lsdatabase + ".ocs_mst_tapplication where application_gid = '" + application_gid + "'";
            ncm_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select employee_emailid from " + lsdatabase + ".hrm_mst_temployee where employee_gid = '" + rm_gid + "'";
            rm_mailid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select employee_emailid from " + lsdatabase + ".hrm_mst_temployee where employee_gid = '" + ch_gid + "'";
            ch_mailid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select employee_emailid from " + lsdatabase + ".hrm_mst_temployee where employee_gid = '" + rh_gid + "'";
            rh_mailid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select employee_emailid from " + lsdatabase + ".hrm_mst_temployee where employee_gid = '" + bh_gid + "'";
            bh_mailid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select employee_emailid from " + lsdatabase + ".hrm_mst_temployee where employee_gid = '" + cm_gid + "'";
            cm_mailid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select employee_emailid from " + lsdatabase + ".hrm_mst_temployee where employee_gid = '" + rcm_gid + "'";
            rcm_mailid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select employee_emailid from " + lsdatabase + ".hrm_mst_temployee where employee_gid = '" + ncm_gid + "'";
            ncm_mailid = objdbconn.GetExecuteScalar(msSQL);

            cc_mailid = rm_mailid + "," + ch_mailid + "," + rh_mailid + "," + bh_mailid + "," + cm_mailid + "," + rcm_mailid + "," + ncm_mailid;
            cc_mailid = cc_mailid.Replace(",,", ",");

            cc_mailid = rm_mailid;

            msSQL = " select employee_mobileno from " + lsdatabase + ".hrm_mst_temployee where employee_gid = '" + rm_gid + "'";
            rm_mobile_no = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select concat(b.user_firstname,' ',b.user_lastname)  from " + lsdatabase + ".hrm_mst_temployee a " +
                "left join adm_mst_tuser b on a.user_gid = b.user_gid " +
                "where a.employee_gid ='" + rm_gid + "'";
            rm_name = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select employee_mobileno from " + lsdatabase + ".hrm_mst_temployee where employee_gid = '" + rh_gid + "'";
            rh_mobile_no = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select concat(b.user_firstname,' ',b.user_lastname)  from " + lsdatabase + ".hrm_mst_temployee a " +
                "left join adm_mst_tuser b on a.user_gid = b.user_gid " +
                "where a.employee_gid ='" + rh_gid + "'";
            rh_name = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select employee_mobileno from " + lsdatabase + ".hrm_mst_temployee where employee_gid = '" + bh_gid + "'";
            bh_mobile_no = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select concat(b.user_firstname,' ',b.user_lastname)  from " + lsdatabase + ".hrm_mst_temployee a " +
                "left join adm_mst_tuser b on a.user_gid = b.user_gid " +
                "where a.employee_gid ='" + bh_gid + "'";
            bh_name = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select employee_mobileno from " + lsdatabase + ".hrm_mst_temployee where employee_gid = '" + ch_gid + "'";
            ch_mobile_no = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select concat(b.user_firstname,' ',b.user_lastname)  from " + lsdatabase + ".hrm_mst_temployee a " +
                "left join adm_mst_tuser b on a.user_gid = b.user_gid " +
                "where a.employee_gid ='" + ch_gid + "'";
            ch_name = objdbconn.GetExecuteScalar(msSQL);


            if (applicant_type == "Institution")
            {
                msSQL = " select company_name from ocs_mst_tinstitution where application_gid = '" + application_gid + "'";
                applicant_name = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " select institution_gid from ocs_mst_tinstitution where application_gid = '" + application_gid + "'";
                institution_gid = objdbconn.GetExecuteScalar(msSQL);
                msSQL = "select email_address from ocs_mst_tinstitution2email where primary_status = 'Yes' and institution_gid = '" + institution_gid + "'";
                tomail_id = objdbconn.GetExecuteScalar(msSQL);
            }
            if (applicant_type == "Individual")
            {
                msSQL = " select concat(first_name,' ',middle_name,' ',last_name) from " + lsdatabase + ".ocs_mst_tcontact where application_gid = '" + application_gid + "'";
                applicant_name = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " select contact_gid from " + lsdatabase + ".ocs_mst_tcontact where application_gid = '" + application_gid + "'";
                contact_gid = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " select email_address from " + lsdatabase + ".ocs_mst_tcontact2email where primary_status = 'Yes' and contact_gid = '" + contact_gid + "'";
                tomail_id = objdbconn.GetExecuteScalar(msSQL);
            }

            sub = "Your request - " + HttpUtility.HtmlEncode(arn_no) + "- is Approved.";
            body = "Dear " + HttpUtility.HtmlEncode(applicant_name) + ",<br><br>";
            body = body + "We are pleased to inform that your application " + HttpUtility.HtmlEncode(arn_no) + " has been approved. The sanction letter will be shared with you shortly. <br><br>";
            body = body + "Your relationship manager Mr./Ms. " + HttpUtility.HtmlEncode(rm_name) + " will contact you for completing the documentation. We request you to complete the documentation within 5 days so that we can serve you faster.<br><br>";
            body = body + "Disclaimer: This mail shall not be treated as Sanction or Confirmation of Grant of Loan. The Sanction / Grant of the Loan shall be subject to clear due diligence of the documents and information provided by you, payment of processing fees and you're meeting the eligibility criteria of Samunnati.<br><br>";
            body = body + "With Warm Regards,<br>";
            body = body + "Samunnati.<br><br>";
            body = body + "Note: This is a system generated mail. You can reach out to us on email:<br> samunnatisakthi@samunnati.com or our helpline at +91 97908 97000.";

            status = Dasendmailtocustomer(sub, body, tomail_id, cc_mailid, application_gid, arn_no);
            if (status == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Daccrejectcustomermail(string application_gid)
        {
            msSQL = " select application_no from " + lsdatabase + ".ocs_mst_tapplication where application_gid = '" + application_gid + "'";
            arn_no = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select applicant_type from " + lsdatabase + ".ocs_mst_tapplication where application_gid = '" + application_gid + "'";
            applicant_type = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select relationshipmanager_gid from " + lsdatabase + ".ocs_mst_tapplication where application_gid = '" + application_gid + "'";
            rm_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select clustermanager_gid from " + lsdatabase + ".ocs_mst_tapplication where application_gid = '" + application_gid + "'";
            ch_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select regionalhead_gid from " + lsdatabase + ".ocs_mst_tapplication where application_gid = '" + application_gid + "'";
            rh_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select businesshead_gid from " + lsdatabase + ".ocs_mst_tapplication where application_gid = '" + application_gid + "'";
            bh_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select creditmanager_gid from " + lsdatabase + ".ocs_mst_tapplication where application_gid = '" + application_gid + "'";
            cm_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select creditregionalmanager_gid from " + lsdatabase + ".ocs_mst_tapplication where application_gid = '" + application_gid + "'";
            rcm_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select creditnationalmanager_gid from " + lsdatabase + ".ocs_mst_tapplication where application_gid = '" + application_gid + "'";
            ncm_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select employee_emailid from " + lsdatabase + ".hrm_mst_temployee where employee_gid = '" + rm_gid + "'";
            rm_mailid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select employee_emailid from " + lsdatabase + ".hrm_mst_temployee where employee_gid = '" + ch_gid + "'";
            ch_mailid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select employee_emailid from " + lsdatabase + ".hrm_mst_temployee where employee_gid = '" + rh_gid + "'";
            rh_mailid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select employee_emailid from " + lsdatabase + ".hrm_mst_temployee where employee_gid = '" + bh_gid + "'";
            bh_mailid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select employee_emailid from " + lsdatabase + ".hrm_mst_temployee where employee_gid = '" + cm_gid + "'";
            cm_mailid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select employee_emailid from " + lsdatabase + ".hrm_mst_temployee where employee_gid = '" + rcm_gid + "'";
            rcm_mailid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select employee_emailid from " + lsdatabase + ".hrm_mst_temployee where employee_gid = '" + ncm_gid + "'";
            ncm_mailid = objdbconn.GetExecuteScalar(msSQL);

            cc_mailid = rm_mailid + "," + ch_mailid;
            cc_mailid = cc_mailid.Replace(",,", ",");
            string[] cc_mail_id = cc_mailid.Split(',');
            string[] cc_mail_id_send = cc_mail_id.Distinct().ToArray();
            cc_mailid = "";
            for (int i = 1; i < cc_mail_id_send.Length; i++)
            {
                cc_mailid = cc_mailid + "," + cc_mail_id_send[i];
            }
            if (rm_mailid == ch_mailid)
            {
                cc_mailid = rm_mailid + "," + ch_mailid;
            }
            else
            {
                cc_mailid = rm_mailid;
            }
            if (applicant_type == "Institution")
            {
                msSQL = " select company_name from ocs_mst_tinstitution where application_gid = '" + application_gid + "'";
                applicant_name = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " select institution_gid from ocs_mst_tinstitution where application_gid = '" + application_gid + "'";
                institution_gid = objdbconn.GetExecuteScalar(msSQL);
                msSQL = "select email_address from ocs_mst_tinstitution2email where primary_status = 'Yes' and institution_gid = '" + institution_gid + "'";
                tomail_id = objdbconn.GetExecuteScalar(msSQL);
            }
            if (applicant_type == "Individual")
            {
                msSQL = " select concat(first_name,' ',middle_name,' ',last_name) from " + lsdatabase + ".ocs_mst_tcontact where application_gid = '" + application_gid + "'";
                applicant_name = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " select contact_gid from " + lsdatabase + ".ocs_mst_tcontact where application_gid = '" + application_gid + "'";
                contact_gid = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " select email_address from " + lsdatabase + ".ocs_mst_tcontact2email where primary_status = 'Yes' and contact_gid = '" + contact_gid + "'";
                tomail_id = objdbconn.GetExecuteScalar(msSQL);
            }
            sub = "Your request - " + HttpUtility.HtmlEncode(arn_no) + " – is Not Approved";
            body = "We regret to inform you that we are unable to process your request further at this juncture.<br><br>";
            body = body + "Our Relationship Manager Mr./Ms. " + HttpUtility.HtmlEncode(rm_name) + " will stay connected with you to support any future requirements.<br><br>";
            body = body + "We thank you for your interest in Samunnati.<br><br>";
            body = body + "With Warm Regards,<br>";
            body = body + "Samunnati.<br><br>";
            body = body + "Note: This is a system generated mail. You can reach out to us on email :<br>samunnatisakthi@samunnati.com or our helpline at +91 97908 97000.";
            status = Dasendmailtocustomer(sub, body, tomail_id, cc_mailid, application_gid, arn_no);
            if (status == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Dasendmailtocustomer(string sub, string body, string tomail_id, string cc_mailid, string application_gid, string arn_no)
        {
            try
            {
                msSQL = " SELECT pop_server, pop_port, pop_username, pop_password  FROM " + lsdatabase + ".adm_mst_tcompany";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    ls_server = objODBCDatareader["pop_server"].ToString();
                    ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                    ls_username = objODBCDatareader["pop_username"].ToString();
                    ls_password = objODBCDatareader["pop_password"].ToString();
                }
                objODBCDatareader.Close();

                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(ls_username);
                message.To.Add(new MailAddress(tomail_id));
                lsBccmail_id = ConfigurationManager.AppSettings["CustomerMailBccMail"].ToString();
                lsBccmail_id = cc_mailid + "," + lsBccmail_id;

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

                //if (cc_mailid != null & cc_mailid != string.Empty & cc_mailid != "")
                //{
                //    lsCCReceipients = cc_mailid.Split(',');
                //    if (cc_mailid.Length == 0)
                //    {
                //        message.CC.Add(new MailAddress(cc_mailid));
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
                try
                {
                    smtp.Send(message);
                    mail_send_result = true;
                    result = "Mail Send Successfully";

                }
                catch (Exception ex)
                {
                    mail_send_result = false;
                    result = ex.ToString();
                }
                mail_details_result = true;
            }
            catch (Exception ex)
            {
                result = ex.ToString();
                mail_details_result = false;
            }
            //Mail Log
            msSQL = "Insert into " + lsdatabase + ".ocs_mst_tcustomermaillog( " +
                                   " application_gid," +
                                   " application_number," +
                                   " from_mailid," +
                                   " cc," +
                                   " bcc," +
                                   " email_to," +
                                   " email_date," +
                                   " email_subject," +
                                   " email_content," +
                                   " status," +
                                   " created_date)" +
                                   " values(" +
                                   "'" + application_gid + "'," +
                                   "'" + arn_no + "'," +
                                   "'" + ls_username + "'," +
                                   "'" + cc_mailid + "'," +
                                   "'" + lsBccmail_id + "'," +
                                   "'" + tomail_id + "'," +
                                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                   "'" + sub + "'," +
                                   "'" + body + "'," +
                                   "'" + result + "'," +
                                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mail_send_result == true && mail_details_result == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}