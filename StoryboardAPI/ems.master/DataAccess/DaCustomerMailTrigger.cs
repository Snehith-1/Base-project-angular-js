using ems.master.DataAccess;
using ems.utilities.Functions;
using System;
using System.Web;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Data.Odbc;
using System.Configuration;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Net;
using System.Linq;
using ems.master.Models;

namespace ems.master.DataAccess
{
    public class DaCustomerMailTrigger
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        OdbcDataReader objODBCDatareader, objODBCDatareader1, objODBCDataReader;
        string msSQL, sub, body, ls_server, ls_username, ls_password, institution_gid, contact_gid;
        string tomail_id, cc_mailid, lsBccmail_id;
        string applicant_type,applicant_name,arn_no,overalllimit_amount,rm_name,rm_gid,rm_mobile_no,rh_mobile_no,ch_mobile_no,bh_mobile_no;
        // RM, Cluster Head, Regional Head, Zonal Head, Business Head
        string rm_mailid, ch_mailid, rh_mailid, zh_mailid, bh_mailid, cm_mailid, rcm_mailid, ncm_mailid;
        string ch_gid, rh_gid, zh_gid, bh_gid, cm_gid,rcm_gid, ncm_gid, rh_name, bh_name, ch_name, result;
        bool status, mail_send_result, mail_details_result;
        int mnResult;
        private int ls_port;
        private IEnumerable<string> lsCCReceipients, lsBCCReceipients;
        
        public bool DaApplicationCreationMail(string application_gid)
        {
            msSQL = " select application_no from ocs_mst_tapplication where application_gid = '" + application_gid + "'";
            arn_no = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select format(overalllimit_amount,0,'en_IN') from ocs_mst_tapplication where application_gid = '" + application_gid + "'";
            overalllimit_amount = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select overalllimit_amount from ocs_mst_tapplication where application_gid = '" + application_gid + "'";
            string overalllimit_amount_int = objdbconn.GetExecuteScalar(msSQL);

            string overalllimit_amout_string = ConvertNumbertoWords((long)Convert.ToDouble(overalllimit_amount));

            overalllimit_amout_string = words(int.Parse(overalllimit_amount_int));

            msSQL = " select applicant_type from ocs_mst_tapplication where application_gid = '" + application_gid + "'";
            applicant_type = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select relationshipmanager_gid from ocs_mst_tapplication where application_gid = '" + application_gid + "'";
            rm_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select clustermanager_gid from ocs_mst_tapplication where application_gid = '" + application_gid + "'";
            ch_gid = objdbconn.GetExecuteScalar(msSQL);

            //msSQL = " select regionalhead_gid from ocs_mst_tapplication where application_gid = '" + application_gid + "'";
            //rh_gid = objdbconn.GetExecuteScalar(msSQL);

            //msSQL = " select zonalhead_gid from ocs_mst_tapplication where application_gid = '" + application_gid + "'";
            //zh_gid = objdbconn.GetExecuteScalar(msSQL);

            //msSQL = " select businesshead_gid from ocs_mst_tapplication where application_gid = '" + application_gid + "'";
            //bh_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select employee_emailid from hrm_mst_temployee where employee_gid = '" + rm_gid + "'";
            rm_mailid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select employee_emailid from hrm_mst_temployee where employee_gid = '" + ch_gid + "'";
            ch_mailid = objdbconn.GetExecuteScalar(msSQL);

            //msSQL = " select employee_emailid from hrm_mst_temployee where employee_gid = '" + rh_gid + "'";
            //rh_mailid = objdbconn.GetExecuteScalar(msSQL);

            //msSQL = " select employee_emailid from hrm_mst_temployee where employee_gid = '" + zh_gid + "'";
            //zh_mailid = objdbconn.GetExecuteScalar(msSQL);

            //msSQL = " select employee_emailid from hrm_mst_temployee where employee_gid = '" + bh_gid + "'";
            //bh_mailid = objdbconn.GetExecuteScalar(msSQL);

            //cc_mailid = rm_mailid + "," + ch_mailid + "," + rh_mailid + "," + zh_mailid + ","+ bh_mailid;
            cc_mailid = rm_mailid + "," + ch_mailid;
            cc_mailid = cc_mailid.Replace(",,",",");
            string[] cc_mail_id = cc_mailid.Split(',');
            string[] cc_mail_id_send = cc_mail_id.Distinct().ToArray();
            cc_mailid = "";
            for(int i=0; i < cc_mail_id_send.Length; i++)
            {
                cc_mailid = cc_mailid + "," + cc_mail_id_send[i];
            }
            cc_mailid = rm_mailid;
            msSQL = " select if(employee_phoneno = '' or employee_phoneno is null ,if(employee_mobileno = '' " +
                     "or employee_mobileno is null ,employee_personalno,employee_mobileno),employee_phoneno)  " +
                     "AS 'employee_phoneno' from hrm_mst_temployee where employee_gid = '" + rm_gid + "'";
            rm_mobile_no = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select concat(b.user_firstname,' ',b.user_lastname)  from hrm_mst_temployee a " +
                "left join adm_mst_tuser b on a.user_gid = b.user_gid " +
                "where a.employee_gid ='"+ rm_gid + "'";
            rm_name = objdbconn.GetExecuteScalar(msSQL);

            if (applicant_type == "Institution") {
                msSQL = " select company_name from ocs_mst_tinstitution where application_gid = '" + application_gid + "'";
                applicant_name = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " select institution_gid from ocs_mst_tinstitution where application_gid = '" + application_gid + "'";
                institution_gid = objdbconn.GetExecuteScalar(msSQL);
                msSQL = "select email_address from ocs_mst_tinstitution2email where primary_status = 'Yes' and institution_gid = '"+ institution_gid + "'";
                tomail_id = objdbconn.GetExecuteScalar(msSQL);
            }//Change tomail id
            if (applicant_type == "Individual") {
                msSQL = " select concat(first_name,' ',middle_name,' ',last_name) from ocs_mst_tcontact where application_gid = '" + application_gid + "'";
                applicant_name = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " select contact_gid from ocs_mst_tcontact where application_gid = '" + application_gid + "'";
                contact_gid = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " select email_address from ocs_mst_tcontact2email where primary_status = 'Yes' and contact_gid = '" + contact_gid + "'";
                tomail_id = objdbconn.GetExecuteScalar(msSQL);
            }//Change tomail id

            sub = "Status update on your request – " + arn_no + ". ";
            body = "Dear "+ HttpUtility.HtmlEncode(applicant_name) + ",<br><br>";
            body = body + "We acknowledge receipt of your application with reference number " + arn_no + " for ₹"+ HttpUtility.HtmlEncode(overalllimit_amount) + " - "+ HttpUtility.HtmlEncode(overalllimit_amout_string) + " Rupees.<br><br>";
            body = body + "Mr./Ms. " + HttpUtility.HtmlEncode(rm_name) + " is your Relationship Manager (+91 "+ rm_mobile_no.Insert(5, "-") + ") for any assistance with reference to your application.<br><br>";
            body = body + "With Warm Regards,<br>";
            body = body + "Samunnati.<br><br>";
            body = body + "Note: : This is a system generated mail. You can reach out to us on email :<br>samunnatisakthi@samunnati.com or our helpline at +91 97908 97000.";


            status = Dasendmailtocustomer(sub, body, tomail_id, cc_mailid, application_gid, arn_no);
            if (status == true) { 
                return true; }
            else { 
                return false; }
        }
        public bool DaAllocatedtocreditmail(string application_gid)
        {
            msSQL = " select application_no from ocs_mst_tapplication where application_gid = '" + application_gid + "'";
            arn_no = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select applicant_type from ocs_mst_tapplication where application_gid = '" + application_gid + "'";
            applicant_type = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select relationshipmanager_gid from ocs_mst_tapplication where application_gid = '" + application_gid + "'";
            rm_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select clustermanager_gid from ocs_mst_tapplication where application_gid = '" + application_gid + "'";
            ch_gid = objdbconn.GetExecuteScalar(msSQL);

            //msSQL = " select regionalhead_gid from ocs_mst_tapplication where application_gid = '" + application_gid + "'";
            //rh_gid = objdbconn.GetExecuteScalar(msSQL);

            //msSQL = " select businesshead_gid from ocs_mst_tapplication where application_gid = '" + application_gid + "'";
            //bh_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select creditmanager_gid from ocs_mst_tapplication where application_gid = '" + application_gid + "'";
            cm_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select creditregionalmanager_gid from ocs_mst_tapplication where application_gid = '" + application_gid + "'";
            rcm_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select employee_emailid from hrm_mst_temployee where employee_gid = '" + rm_gid + "'";
            rm_mailid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select employee_emailid from hrm_mst_temployee where employee_gid = '" + ch_gid + "'";
            ch_mailid = objdbconn.GetExecuteScalar(msSQL);

            //msSQL = " select employee_emailid from hrm_mst_temployee where employee_gid = '" + rh_gid + "'";
            //rh_mailid = objdbconn.GetExecuteScalar(msSQL);

            //msSQL = " select employee_emailid from hrm_mst_temployee where employee_gid = '" + bh_gid + "'";
            bh_mailid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select employee_emailid from hrm_mst_temployee where employee_gid = '" + cm_gid + "'";
            cm_mailid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select employee_emailid from hrm_mst_temployee where employee_gid = '" + rcm_gid + "'";
            rcm_mailid = objdbconn.GetExecuteScalar(msSQL);

            //cc_mailid = rm_mailid + "," + ch_mailid + "," + rh_mailid + "," + bh_mailid + "," + cm_mailid + "," + rcm_mailid;
            cc_mailid = rm_mailid + "," + ch_mailid + "," + cm_mailid + "," + rcm_mailid;
            cc_mailid = cc_mailid.Replace(",,", ",");
            string[] cc_mail_id = cc_mailid.Split(',');
            string[] cc_mail_id_send = cc_mail_id.Distinct().ToArray();
            cc_mailid = "";
            for (int i = 0; i < cc_mail_id_send.Length; i++)
            {
                cc_mailid = cc_mailid + "," +cc_mail_id_send[i];
            }
            cc_mailid = rm_mailid;
            msSQL = " select if(employee_phoneno = '' or employee_phoneno is null ,if(employee_mobileno = '' " +
                    "or employee_mobileno is null ,employee_personalno,employee_mobileno),employee_phoneno)  " +
                    "AS 'employee_phoneno' from hrm_mst_temployee where employee_gid = '" + rm_gid + "'";
            rm_mobile_no = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select concat(b.user_firstname,' ',b.user_lastname)  from hrm_mst_temployee a " +
                "left join adm_mst_tuser b on a.user_gid = b.user_gid " +
                "where a.employee_gid ='" + rm_gid + "'";
            rm_name = objdbconn.GetExecuteScalar(msSQL);

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
                msSQL = " select concat(first_name,' ',middle_name,' ',last_name) from ocs_mst_tcontact where application_gid = '" + application_gid + "'";
                applicant_name = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " select contact_gid from ocs_mst_tcontact where application_gid = '" + application_gid + "'";
                contact_gid = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " select email_address from ocs_mst_tcontact2email where primary_status = 'Yes' and contact_gid = '" + contact_gid + "'";
                tomail_id = objdbconn.GetExecuteScalar(msSQL);
            }

            sub = " Status update on your request  " + arn_no +" is allocated to Credit Assessment.";
            body = "Dear " + HttpUtility.HtmlEncode(applicant_name) + ",<br><br>";
            body = body + "We wish to advise that your request is now taken up for credit assessment.<br><br>";
            body = body + "Your Relationship Manager/Credit Manager may reach out to you for any additional details that may be required. We request you to provide necessary support to expedite your request. <br><br>";
            //body = body + rm_name + ", is your Relationship Manager. You can reach out through " + rm_mobile_no + " for assistance.<br><br>";
            body = body + "With Warm Regards,<br>";
            body = body + "Samunnati.<br><br>";
            body = body + "Note: This is a system generated mail. You can reach out to us on email :<br>samunnatisakthi@samunnati.com or our helpline at +91 97908 97000";

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

        public bool Daccapprovedcustomermail(string application_gid) 
        {
            msSQL = " select application_no from ocs_mst_tapplication where application_gid = '" + application_gid + "'";
            arn_no = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select applicant_type from ocs_mst_tapplication where application_gid = '" + application_gid + "'";
            applicant_type = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select relationshipmanager_gid from ocs_mst_tapplication where application_gid = '" + application_gid + "'";
            rm_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select clustermanager_gid from ocs_mst_tapplication where application_gid = '" + application_gid + "'";
            ch_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select regionalhead_gid from ocs_mst_tapplication where application_gid = '" + application_gid + "'";
            rh_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select businesshead_gid from ocs_mst_tapplication where application_gid = '" + application_gid + "'";
            bh_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select creditmanager_gid from ocs_mst_tapplication where application_gid = '" + application_gid + "'";
            cm_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select creditregionalmanager_gid from ocs_mst_tapplication where application_gid = '" + application_gid + "'";
            rcm_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select creditnationalmanager_gid from ocs_mst_tapplication where application_gid = '" + application_gid + "'";
            ncm_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select employee_emailid from hrm_mst_temployee where employee_gid = '" + rm_gid + "'";
            rm_mailid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select employee_emailid from hrm_mst_temployee where employee_gid = '" + ch_gid + "'";
            ch_mailid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select employee_emailid from hrm_mst_temployee where employee_gid = '" + rh_gid + "'";
            rh_mailid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select employee_emailid from hrm_mst_temployee where employee_gid = '" + bh_gid + "'";
            bh_mailid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select employee_emailid from hrm_mst_temployee where employee_gid = '" + cm_gid + "'";
            cm_mailid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select employee_emailid from hrm_mst_temployee where employee_gid = '" + rcm_gid + "'";
            rcm_mailid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select employee_emailid from hrm_mst_temployee where employee_gid = '" + ncm_gid + "'";
            ncm_mailid = objdbconn.GetExecuteScalar(msSQL);

            //cc_mailid = rm_mailid + "," + ch_mailid + "," + rh_mailid + "," + bh_mailid + "," + cm_mailid + "," + rcm_mailid + "," + ncm_mailid;
            cc_mailid = rm_mailid + "," + ch_mailid + "," + cm_mailid + "," + rcm_mailid + "," + ncm_mailid;
            cc_mailid = cc_mailid.Replace(",,", ",");
            string[] cc_mail_id = cc_mailid.Split(',');
            string[] cc_mail_id_send = cc_mail_id.Distinct().ToArray();
            cc_mailid = "";
            for (int i = 0; i < cc_mail_id_send.Length; i++)
            {
                cc_mailid = cc_mailid + "," + cc_mail_id_send[i];
            }
            cc_mailid = rm_mailid;
            msSQL = " select if(employee_phoneno = '' or employee_phoneno is null ,if(employee_mobileno = '' " +
                    "or employee_mobileno is null ,employee_personalno,employee_mobileno),employee_phoneno)  " +
                    "AS 'employee_phoneno' from hrm_mst_temployee where employee_gid = '" + rm_gid + "'";
            rm_mobile_no = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select concat(b.user_firstname,' ',b.user_lastname)  from hrm_mst_temployee a " +
                "left join adm_mst_tuser b on a.user_gid = b.user_gid " +
                "where a.employee_gid ='" + rm_gid + "'";
            rm_name = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select if(employee_phoneno = '' or employee_phoneno is null ,if(employee_mobileno = '' " +
                   "or employee_mobileno is null ,employee_personalno,employee_mobileno),employee_phoneno)  " +
                   "AS 'employee_phoneno' from hrm_mst_temployee where employee_gid = '" + rh_gid + "'";
            rh_mobile_no = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select concat(b.user_firstname,' ',b.user_lastname)  from hrm_mst_temployee a " +
                "left join adm_mst_tuser b on a.user_gid = b.user_gid " +
                "where a.employee_gid ='" + rh_gid + "'";
            rh_name = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select if(employee_phoneno = '' or employee_phoneno is null ,if(employee_mobileno = '' " +
                    "or employee_mobileno is null ,employee_personalno,employee_mobileno),employee_phoneno)  " +
                    "AS 'employee_phoneno' from hrm_mst_temployee where employee_gid = '" + bh_gid + "'";
            bh_mobile_no = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select concat(b.user_firstname,' ',b.user_lastname)  from hrm_mst_temployee a " +
                "left join adm_mst_tuser b on a.user_gid = b.user_gid " +
                "where a.employee_gid ='" + bh_gid + "'";
            bh_name = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select if(employee_phoneno = '' or employee_phoneno is null ,if(employee_mobileno = '' " +
                   "or employee_mobileno is null ,employee_personalno,employee_mobileno),employee_phoneno)  " +
                   "AS 'employee_phoneno' from hrm_mst_temployee where employee_gid = '" + ch_gid + "'";
            ch_mobile_no = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select concat(b.user_firstname,' ',b.user_lastname)  from hrm_mst_temployee a " +
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
                msSQL = " select concat(first_name,' ',middle_name,' ',last_name) from ocs_mst_tcontact where application_gid = '" + application_gid + "'";
                applicant_name = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " select contact_gid from ocs_mst_tcontact where application_gid = '" + application_gid + "'";
                contact_gid = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " select email_address from ocs_mst_tcontact2email where primary_status = 'Yes' and contact_gid = '" + contact_gid + "'";
                tomail_id = objdbconn.GetExecuteScalar(msSQL);
            }

            
            sub = "Your request - " + arn_no + "- is Approved.";
            body = "Dear " + HttpUtility.HtmlEncode(applicant_name) + ",<br><br>";
            body = body + "We are pleased to inform that your application "+arn_no+" has been approved. The sanction letter will be shared with you shortly. <br><br>";
            body = body + "Your relationship manager Mr./Ms. "+ HttpUtility.HtmlEncode(rm_name) +" will contact you for completing the documentation. We request you to complete the documentation within 5 days so that we can serve you faster.<br><br>";
            body = body + "Disclaimer: This mail shall not be treated as Sanction or Confirmation of Grant of Loan. The Sanction / Grant of the Loan shall be subject to clear due diligence of the documents and information provided by you, payment of processing fees and you're meeting the eligibility criteria of Samunnati.<br><br>";
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

        public bool Daccrejectcustomermail(string application_gid)
        {
            msSQL = " select application_no from ocs_mst_tapplication where application_gid = '" + application_gid + "'";
            arn_no = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select applicant_type from ocs_mst_tapplication where application_gid = '" + application_gid + "'";
            applicant_type = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select relationshipmanager_gid from ocs_mst_tapplication where application_gid = '" + application_gid + "'";
            rm_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select clustermanager_gid from ocs_mst_tapplication where application_gid = '" + application_gid + "'";
            ch_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select regionalhead_gid from ocs_mst_tapplication where application_gid = '" + application_gid + "'";
            rh_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select businesshead_gid from ocs_mst_tapplication where application_gid = '" + application_gid + "'";
            bh_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select creditmanager_gid from ocs_mst_tapplication where application_gid = '" + application_gid + "'";
            cm_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select creditregionalmanager_gid from ocs_mst_tapplication where application_gid = '" + application_gid + "'";
            rcm_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select creditnationalmanager_gid from ocs_mst_tapplication where application_gid = '" + application_gid + "'";
            ncm_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select employee_emailid from hrm_mst_temployee where employee_gid = '" + rm_gid + "'";
            rm_mailid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select employee_emailid from hrm_mst_temployee where employee_gid = '" + ch_gid + "'";
            ch_mailid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select employee_emailid from hrm_mst_temployee where employee_gid = '" + rh_gid + "'";
            rh_mailid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select employee_emailid from hrm_mst_temployee where employee_gid = '" + bh_gid + "'";
            bh_mailid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select employee_emailid from hrm_mst_temployee where employee_gid = '" + cm_gid + "'";
            cm_mailid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select employee_emailid from hrm_mst_temployee where employee_gid = '" + rcm_gid + "'";
            rcm_mailid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select employee_emailid from hrm_mst_temployee where employee_gid = '" + ncm_gid + "'";
            ncm_mailid = objdbconn.GetExecuteScalar(msSQL);

            //cc_mailid = rm_mailid + "," + ch_mailid + "," + rh_mailid + "," + bh_mailid + "," + cm_mailid + "," + rcm_mailid + "," + ncm_mailid;
            cc_mailid = rm_mailid + "," + ch_mailid;
            cc_mailid = cc_mailid.Replace(",,", ",");
            string[] cc_mail_id = cc_mailid.Split(',');
            string[] cc_mail_id_send = cc_mail_id.Distinct().ToArray();
            cc_mailid = "";
            for (int i = 1; i < cc_mail_id_send.Length; i++)
            {
                cc_mailid = cc_mailid + "," + cc_mail_id_send[i];
            }
            if(rm_mailid == ch_mailid)
            {
                cc_mailid = rm_mailid + "," + ch_mailid;
            }
            else
            {
                cc_mailid = rm_mailid;
            }
            msSQL = "select concat(b.user_firstname,' ',b.user_lastname)  from hrm_mst_temployee a " +
                "left join adm_mst_tuser b on a.user_gid = b.user_gid " +
                "where a.employee_gid ='" + rm_gid + "'";
            rm_name = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select if(employee_phoneno = '' or employee_phoneno is null ,if(employee_mobileno = '' " +
                    "or employee_mobileno is null ,employee_personalno,employee_mobileno),employee_phoneno)  " +
                    "AS 'employee_phoneno' from hrm_mst_temployee where employee_gid = '" + rm_gid + "'";
            rm_mobile_no = objdbconn.GetExecuteScalar(msSQL);

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
                msSQL = " select concat(first_name,' ',middle_name,' ',last_name) from ocs_mst_tcontact where application_gid = '" + application_gid + "'";
                applicant_name = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " select contact_gid from ocs_mst_tcontact where application_gid = '" + application_gid + "'";
                contact_gid = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " select email_address from ocs_mst_tcontact2email where primary_status = 'Yes' and contact_gid = '" + contact_gid + "'";
                tomail_id = objdbconn.GetExecuteScalar(msSQL);
            }
            sub = "Your request - " + arn_no + " – is Not Approved";
            body = "We regret to inform you that we are unable to process your request further at this juncture.<br><br>";
            body = body + "Our Relationship Manager Mr./Ms. "+ HttpUtility.HtmlEncode(rm_name) +" will stay connected with you to support any future requirements.<br><br>";
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
        public bool Dasendmailtocustomer(string sub,string body,string tomail_id, string cc_mailid, string application_gid, string arn_no)
        {
            try
            {
                msSQL = " SELECT pop_server, pop_port, pop_username, pop_password  FROM adm_mst_tcompany";
                objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader1.HasRows == true)
                {
                    ls_server = objODBCDatareader1["pop_server"].ToString();
                    ls_port = Convert.ToInt32(objODBCDatareader1["pop_port"]);
                    ls_username = objODBCDatareader1["pop_username"].ToString();
                    ls_password = objODBCDatareader1["pop_password"].ToString();
                }
                objODBCDatareader1.Close();

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
                try { 
                    smtp.Send(message);
                    mail_send_result =  true;
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
            msSQL = "Insert into ocs_mst_tcustomermaillog( " +
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
                                   "'" + sub.Replace("'", "") + "'," +
                                   "'" + body.Replace("'", "") + "'," +
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
        public string ConvertNumbertoWords(long number)
        {
            if (number == 0) return "ZERO";
            if (number < 0) return "minus " + ConvertNumbertoWords(Math.Abs(number));
            string words = "";
            if ((number / 100000000) > 0)
            {
                words += ConvertNumbertoWords(number / 100000) + " LAKES ";
                number %= 100000000;
            }
            if ((number / 1000000) > 0)
            {
                words += ConvertNumbertoWords(number / 100000) + " LAKES ";
                number %= 1000000;
            }
            if ((number / 1000) > 0)
            {
                words += ConvertNumbertoWords(number / 1000) + " THOUSAND ";
                number %= 1000;
            }
            if ((number / 100) > 0)
            {
                words += ConvertNumbertoWords(number / 100) + " HUNDRED ";
                number %= 100;
            }
            //if ((number / 10) > 0)  
            //{  
            // words += ConvertNumbertoWords(number / 10) + " RUPEES ";  
            // number %= 10;  
            //}  
            if (number > 0)
            {
                if (words != "") words += "AND ";
                var unitsMap = new[]
                {
            "ZERO", "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE", "TEN", "ELEVEN", "TWELVE", "THIRTEEN", "FOURTEEN", "FIFTEEN", "SIXTEEN", "SEVENTEEN", "EIGHTEEN", "NINETEEN"
        };
                var tensMap = new[]
                {
            "ZERO", "TEN", "TWENTY", "THIRTY", "FORTY", "FIFTY", "SIXTY", "SEVENTY", "EIGHTY", "NINETY"
        };
                if (number < 20) words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0) words += " " + unitsMap[number % 10];
                }
            }
            return words;
        }
        public string words(int numbers)
        {
            int number = numbers;

            if (number == 0) return "Zero";
            int[] num = new int[4];
            int first = 0;
            int u, h, t;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (number < 0)
            {
                sb.Append("Minus ");
                number = -number;
            }
            string[] words0 = {"" ,"One ", "Two ", "Three ", "Four ", "Five " ,"Six ", "Seven ", "Eight ", "Nine "};
            string[] words1 = {"Ten ", "Eleven ", "Twelve ", "Thirteen ", "Fourteen ", "Fifteen ","Sixteen ","Seventeen ","Eighteen ", "Nineteen "};
            string[] words2 = {"Twenty ", "Thirty ", "Forty ", "Fifty ", "Sixty ", "Seventy ","Eighty ", "Ninety "};
            string[] words3 = { "Thousand ", "Lakh ", "Crore " };
            num[0] = number % 1000; // units
            num[1] = number / 1000;
            num[2] = number / 100000;
            num[1] = num[1] - 100 * num[2]; // thousands
            num[3] = number / 10000000; // crores
            num[2] = num[2] - 100 * num[3]; // lakhs
            for (int i = 3; i > 0; i--)
            {
                if (num[i] != 0)
                {
                    first = i;
                    break;
                }
            }
            for (int i = first; i >= 0; i--)
            {
                if (num[i] == 0) continue;
                u = num[i] % 10; // ones
                t = num[i] / 10;
                h = num[i] / 100; // hundreds
                t = t - 10 * h; // tens
                if (h > 0) sb.Append(words0[h] + "Hundred ");
                if (u > 0 || t > 0)
                {
                    if (h > 0 || i == 0) sb.Append("and ");
                    if (t == 0)
                        sb.Append(words0[u]);
                    else if (t == 1)
                        sb.Append(words1[u]);
                    else
                        sb.Append(words2[t - 2] + words0[u]);
                }
                if (i != 0) sb.Append(words3[i - 1]);
            }
            return sb.ToString().TrimEnd();
        }
    }
}