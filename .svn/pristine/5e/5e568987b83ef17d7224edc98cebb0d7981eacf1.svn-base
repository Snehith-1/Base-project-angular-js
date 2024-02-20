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
using ems.storage.Functions;


namespace ems.mastersamagro.DataAccess
{
    /// <summary>
    /// This DataAccess will provide access to various functionalities in AC management flow.
    /// </summary>
    /// <remarks>Written by Sherin Augusta, Logapriya, Abilash.A, Premchander.K </remarks>

    public class DaAgrTrnCC
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        HttpPostedFile httpPostedFile;
        OdbcDataReader objODBCDatareader, objODBCDatareader1;
        string arn_no;
        string rm_mailid, ch_mailid, rh_mailid, zh_mailid, bh_mailid, cm_mailid, rcm_mailid, ncm_mailid, rm_gid, ch_gid, rcm_gid, ncm_gid;
        private string customer_name;
        private string relationshipmanager_name;
        private string allocated_by;
        private string creditassigned_date;
        public string lssource;
        DataTable dt_datatable;
        string msSQL, msGetGid, msGetDocumentGid, msGetGid1, msGetGid2;

        int mnResult, ls_port, MailFlag, k;
        string lspath, lssession_user, lsdocument_attached;
        public string ls_server, ls_username, ls_password, tomail_id, tomail_id1, tomail_id2, sub, body, employeename, cc_mailid, cc_mailid1, cc_mailid2, employee_reporting_to;
        public string[] lsCCReceipients;
        public string lsBccmail_id;
        public string[] lsBCCReceipients;
        string lsemployee_name, lsemployee_gid, lsccmeeting_date, lsccgroup_name, lsloanfacility_amount, lscustomer_name, lsccadmin_name, lsapplication_no;
        string sToken = string.Empty;
        string lsrequested_by, message;
        Random rand = new Random();
        string lsto_mail, frommail_id, lscc_mail, strBCC, lsbcc_mail, lsccadmin_gid, approver, lscontent = string.Empty;
        string lsdatabase = ConfigurationManager.AppSettings["externalportal"].ToString();

        //CC Meeting Pending Summary
        public void DaGetCCPendingSummary(string employee_gid, MdlMstApplicationAdd values)
        {
            msSQL = " select application_gid,application_no,customerref_name,customer_urn,vertical_name," +
                    " a.customer_name as customer_name,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as submitted_by,a.approval_status,applicant_type," +
                    " date_format(a.submitted_date,'%d-%m-%Y %h:%i %p') as submitted_date,date_format(a.ccsubmitted_date,'%d-%m-%Y %h:%i %p') as ccsubmitted_date," +
                    " region,concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as ccsubmitted_by,ccgroup_name,overalllimit_amount," +
                    " a.renewal_flag,a.amendment_flag,a.shortclosing_flag from agr_mst_tapplication a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.submitted_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                    " left join hrm_mst_temployee d on a.ccsubmitted_by = d.employee_gid" +
                    " left join adm_mst_tuser e on e.user_gid = d.user_gid where ccsubmit_flag='Y' and meeting_status='Pending'" +
                    " order by ccsubmitted_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<applicationadd_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getapplicationadd_list.Add(new applicationadd_list
                    {
                        application_gid = dt["application_gid"].ToString(),
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical_name = dt["vertical_name"].ToString(),
                        application_status = dt["approval_status"].ToString(),
                        applicant_type = dt["applicant_type"].ToString(),
                        submitted_date = dt["submitted_date"].ToString(),
                        ccsubmitted_date = dt["ccsubmitted_date"].ToString(),
                        submitted_by = dt["submitted_by"].ToString(),
                        ccsubmitted_by = dt["ccsubmitted_by"].ToString(),
                        region = dt["region"].ToString(),
                        ccgroup_name = dt["ccgroup_name"].ToString(),
                        overalllimit_amount = dt["overalllimit_amount"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        renewal_flag = dt["renewal_flag"].ToString(),
                        amendment_flag = dt["amendment_flag"].ToString(),
                        shortclosing_flag = dt["shortclosing_flag"].ToString()
                    });
                }
            }
            values.applicationadd_list = getapplicationadd_list;
            dt_datatable.Dispose();
        }
        //CC Meeting Scheduled Summary
        public void DaGetCCScheduledSummary(string employee_gid, MdlMstApplicationAdd values)
        {
            msSQL = " select a.application_gid,application_no,customerref_name,customer_urn,vertical_name," +
                    " a.customer_name as customer_name,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as submitted_by,applicant_type," +
                    " date_format(a.submitted_date,'%d-%m-%Y %h:%i %p') as submitted_date,date_format(a.ccsubmitted_date,'%d-%m-%Y %h:%i %p') as ccsubmitted_date," +
                    " region,concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as ccsubmitted_by,a.ccgroup_name, " +
                    " date_format(f.ccmeeting_date, '%d-%m-%Y') as ccmeeting_date,concat(time_format(start_time, '%h:%i %p'), '-', time_format(end_time, '%h:%i %p')) as ccmeting_time, " +
                    " date_format(f.created_date, '%d-%m-%Y %h:%i %p') as created_date, concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as created_by, " +
                    " a.approval_status,a.renewal_flag,a.amendment_flag,a.shortclosing_flag from agr_mst_tapplication a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.submitted_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                    " left join agr_mst_tccschedulemeeting f on a.application_gid = f.application_gid " +
                    " left join hrm_mst_temployee d on f.created_by = d.employee_gid" +
                    " left join adm_mst_tuser e on e.user_gid = d.user_gid " +
                    " left join hrm_mst_temployee g on g.created_by = g.employee_gid" +
                    " left join adm_mst_tuser h on h.user_gid = g.user_gid " +
                    " where ccsubmit_flag='Y' and meeting_status='Scheduled' and (( a.approval_status = 'Submitted to CC' and  a.approval_status <> 'CC Approved') or (a.approval_status = 'CC Rejected' and cccompleted_flag = 'N')) " +
                    " order by  STR_TO_DATE(ccmeeting_date,'%Y-%m-%d') asc  ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<applicationadd_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getapplicationadd_list.Add(new applicationadd_list
                    {
                        application_gid = dt["application_gid"].ToString(),
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical_name = dt["vertical_name"].ToString(),
                        application_status = dt["approval_status"].ToString(),
                        applicant_type = dt["applicant_type"].ToString(),
                        submitted_date = dt["submitted_date"].ToString(),
                        ccsubmitted_date = dt["ccsubmitted_date"].ToString(),
                        submitted_by = dt["submitted_by"].ToString(),
                        ccsubmitted_by = dt["ccsubmitted_by"].ToString(),
                        region = dt["region"].ToString(),
                        ccgroup_name = dt["ccgroup_name"].ToString(),
                        ccmeeting_date = dt["ccmeeting_date"].ToString(),
                        ccmeting_time = dt["ccmeting_time"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        renewal_flag = dt["renewal_flag"].ToString(),
                        amendment_flag = dt["amendment_flag"].ToString(),
                        shortclosing_flag = dt["shortclosing_flag"].ToString()
                    });
                }
            }
            values.applicationadd_list = getapplicationadd_list;
            dt_datatable.Dispose();
        }
        //CC Meeting Scheduled - Completed Summary
        public void DaGetMeetingCompletedSummary(string employee_gid, MdlMstApplicationAdd values)
        {
            msSQL = " select a.application_gid,application_no,customerref_name,customer_urn,vertical_name," +
                    " a.customer_name as customer_name,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as submitted_by,applicant_type," +
                    " date_format(a.submitted_date,'%d-%m-%Y %h:%i %p') as submitted_date,date_format(a.ccsubmitted_date,'%d-%m-%Y %h:%i %p') as ccsubmitted_date," +
                    " region,concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as ccsubmitted_by,a.ccgroup_name, " +
                    " date_format(f.ccmeeting_date, '%d-%m-%Y') as ccmeeting_date,concat(time_format(start_time, '%h:%i %p'), '-', time_format(end_time, '%h:%i %p')) as ccmeting_time, " +
                    " date_format(f.created_date, '%d-%m-%Y %h:%i %p') as created_date, concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as created_by, " +
                    " a.approval_status,date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as  cccompleted_date,a.renewal_flag,a.amendment_flag,a.shortclosing_flag from agr_mst_tapplication a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.submitted_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                    " left join hrm_mst_temployee d on a.ccsubmitted_by = d.employee_gid" +
                    " left join adm_mst_tuser e on e.user_gid = d.user_gid " +
                    " left join agr_mst_tccschedulemeeting f on a.application_gid = f.application_gid " +
                    " left join hrm_mst_temployee g on g.created_by = g.employee_gid" +
                    " left join adm_mst_tuser h on h.user_gid = g.user_gid " +
                    " where ccsubmit_flag='Y' and meeting_status='Scheduled' and  ( a.approval_status = 'CC Approved' or (a.approval_status = 'CC Rejected' and cccompleted_flag = 'Y'))" +
                    " order by  STR_TO_DATE(ccmeeting_date,'%Y-%m-%d') desc  ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<applicationadd_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getapplicationadd_list.Add(new applicationadd_list
                    {
                        application_gid = dt["application_gid"].ToString(),
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical_name = dt["vertical_name"].ToString(),
                        application_status = dt["approval_status"].ToString(),
                        applicant_type = dt["applicant_type"].ToString(),
                        submitted_date = dt["submitted_date"].ToString(),
                        ccsubmitted_date = dt["ccsubmitted_date"].ToString(),
                        submitted_by = dt["submitted_by"].ToString(),
                        ccsubmitted_by = dt["ccsubmitted_by"].ToString(),
                        region = dt["region"].ToString(),
                        ccgroup_name = dt["ccgroup_name"].ToString(),
                        ccmeeting_date = dt["ccmeeting_date"].ToString(),
                        ccmeting_time = dt["ccmeting_time"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        cccompleted_date = dt["cccompleted_date"].ToString(),
                        renewal_flag = dt["renewal_flag"].ToString(),
                        amendment_flag = dt["amendment_flag"].ToString(),
                        shortclosing_flag = dt["shortclosing_flag"].ToString()
                    });
                }
            }
            values.applicationadd_list = getapplicationadd_list;
            dt_datatable.Dispose();
        }
        public void DaGetCCcount(string employee_gid, MdlCCcount values)
        {
            msSQL = " select count(*) from agr_mst_tapplication a " +
                    " left join agr_mst_tccschedulemeeting b on a.application_gid=b.application_gid" +
                     " where ccsubmit_flag='Y' and meeting_status='Scheduled' and (( a.approval_status = 'Submitted to CC' and  a.approval_status <> 'CC Approved') or (a.approval_status = 'CC Rejected' and cccompleted_flag = 'N'))";
            values.scheduled_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select count(*) from agr_mst_tapplication a " +
                    " where ccsubmit_flag='Y' and meeting_status='Pending'";
            values.pending_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select count(*) from agr_mst_tapplication a " +
                   " left join agr_mst_tccschedulemeeting b on a.application_gid=b.application_gid" +
                   " where ccsubmit_flag='Y' and meeting_status='Scheduled' and  ( a.approval_status = 'CC Approved' or (a.approval_status = 'CC Rejected' and cccompleted_flag = 'Y'))";
            values.completed_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select count(*) from agr_mst_tapplication a " +
                    " where ccsubmit_flag='N' and meeting_status='Pending' and approval_status = 'Sent Back to Credit'";
            values.cctocredit_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select count(distinct a.application_gid) from agr_mst_tapplication a " +
                   " left join agr_mst_tccschedulemeeting b on a.application_gid=b.application_gid" +
                   " where a.onboarding_status = 'Direct'";
            values.advance_count = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " select count(distinct application_gid) as ccmeetingskip_count from agr_trn_tccmeetingskip ";
            values.ccmeetingskip_count = objdbconn.GetExecuteScalar(msSQL);

        }
        public void DaGetApplicantInfo(string application_gid, string employee_gid, MdlMstCC values)
        {
            try
            {
                msSQL = " select a.application_no,a.customerref_name as customer_name,date_format(a.submitted_date,'%d-%m-%Y %h:%i %p') as submitted_date," +
                     " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as submitted_by," +
                     " date_format(a.ccsubmitted_date, '%d-%m-%Y %h:%i %p') as ccsubmitted_date," +
                     " concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as ccsubmitted_by " +
                     " from agr_mst_tapplication a" +
                     " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                     " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                     " left join hrm_mst_temployee d on a.ccsubmitted_by = d.employee_gid" +
                     " left join adm_mst_tuser e on e.user_gid = d.user_gid where application_gid='" + application_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.application_no = objODBCDatareader["application_no"].ToString();
                    values.customer_name = objODBCDatareader["customer_name"].ToString();
                    values.submitted_date = objODBCDatareader["submitted_date"].ToString();
                    values.submitted_by = objODBCDatareader["submitted_by"].ToString();
                    values.ccsubmitted_date = objODBCDatareader["ccsubmitted_date"].ToString();
                    values.ccsubmitted_by = objODBCDatareader["ccsubmitted_by"].ToString();
                    values.vertical_name = objODBCDatareader["vertical_name"].ToString();
                }
                values.status = true;
                objODBCDatareader.Close();
            }
            catch
            {
                values.status = false;
            }
        }
        public bool DaPostScheduleMeeting(string employee_gid, MdlMstCCschedule values)
        {
            string lsccgroup_gid = string.Empty;
            string lsccgroup_name = string.Empty;
            string lsotheruser_gid = string.Empty;
            string lsotheruser_name = string.Empty;
            string loopccgroup_gid = string.Empty;
            string lsccadmin_gid = string.Empty;
            string lsccadmin_name = string.Empty;
            if (values.ccschedule_list != null)
            {
                for (var i = 0; i < values.ccschedule_list.Count; i++)
                {
                    lsccgroup_gid += values.ccschedule_list[i].ccgroupname_gid + ",";
                    lsccgroup_name += values.ccschedule_list[i].ccgroup_name + ",";
                    loopccgroup_gid += "'" + values.ccschedule_list[i].ccgroupname_gid + "'" + ",";
                }
                lsccgroup_gid = lsccgroup_gid.TrimEnd(',');
                lsccgroup_name = lsccgroup_name.TrimEnd(',');
                loopccgroup_gid = loopccgroup_gid.TrimEnd(',');
            }
            if (values.otheremployee_list != null)
            {
                for (var i = 0; i < values.otheremployee_list.Count; i++)
                {
                    lsotheruser_gid += values.otheremployee_list[i].employee_gid + ",";
                    lsotheruser_name += values.otheremployee_list[i].employee_name + ",";
                }

                lsotheruser_gid = lsotheruser_gid.TrimEnd(',');
                lsotheruser_name = lsotheruser_name.TrimEnd(',');
            }
            if (values.ccadmin_list != null)
            {
                for (var i = 0; i < values.ccadmin_list.Count; i++)
                {
                    lsccadmin_gid += values.ccadmin_list[i].employee_gid + ",";
                    lsccadmin_name += values.ccadmin_list[i].employee_name + ",";
                }

                lsccadmin_gid = lsccadmin_gid.TrimEnd(',');
                lsccadmin_name = lsccadmin_name.TrimEnd(',');
            }
            msGetGid = objcmnfunctions.GetMasterGID("CCSM");
            msSQL = " insert into agr_mst_tccschedulemeeting(" +
                    " ccschedulemeeting_gid," +
                    " application_gid," +
                    " ccmeeting_title," +
                    " ccmeeting_no," +
                    " ccmeeting_date," +
                    " start_time," +
                    " end_time," +
                    " ccmeeting_mode," +
                    " ccgroupname_gid," +
                    " ccgroup_name," +
                    " otheruser_gid," +
                    " otheruser_name," +
                    " ccadmin_gid," +
                    " ccadmin_name," +
                    " description," +
                    " non_users," +
                    " created_by," +
                    " created_date," +
                    " updated_by," +
                    " updated_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.application_gid + "'," +
                    "'" + values.ccmeeting_title.Replace("'", "").Replace("'", "") + "'," +
                    "'" + values.ccmeeting_no.Replace("'", "").Replace("'", "") + "',";
            if (values.ccmeeting_date == null || values.ccmeeting_date == "")
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.ccmeeting_date).ToString("yyyy-MM-dd") + "',";
            }
            if (values.start_time == null || values.start_time == "")
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.start_time).ToString("HH:mm:ss") + "',";
            }
            if (values.end_time == null || values.end_time == "")
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.end_time).ToString("HH:mm:ss") + "',";
            }
            msSQL += "'" + values.ccmeeting_mode + "'," +
                       "'" + lsccgroup_gid + "'," +
                       "'" + lsccgroup_name + "',";
            if (values.otheremployee_list == null)
            {
                msSQL += "''," +
                         "'',";
            }
            else
            {
                msSQL += "'" + lsotheruser_gid + "'," +
                         "'" + lsotheruser_name + "',";
            }
            if (values.ccadmin_list == null)
            {
                msSQL += "''," +
                         "'',";
            }
            else
            {
                msSQL += "'" + lsccadmin_gid + "'," +
                         "'" + lsccadmin_name + "',";
            }
            if (values.description == null || values.description == "")
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.description.Replace("'", "") + "',";
            }
            if (values.non_users == null || values.non_users == "")
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.non_users.Replace("'", "") + "',";
            }
            msSQL += "'" + employee_gid + "'," +
                      "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                      "'" + employee_gid + "'," +
                      "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msSQL = " update agr_mst_tapplication set ccgroup_name='" + lsccgroup_name + "'," +
                         " meeting_status='Scheduled'," +
                         " updated_by = '" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where application_gid='" + values.application_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                //CC members
                msSQL = " select a.ccmember_name,a.ccmember_gid,b.ccgroup_name from ocs_mst_tccmember a" +
                    " left join ocs_mst_tccgroupname b on a.ccgroupname_gid=b.ccgroupname_gid where" +
                    " a.ccgroupname_gid in (" + loopccgroup_gid + ")";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {

                        msGetGid1 = objcmnfunctions.GetMasterGID("CM2M");
                        msSQL = " insert into agr_mst_tccmeeting2members(" +
                                " ccmeeting2members_gid," +
                                " application_gid," +
                                " ccschedulemeeting_gid," +
                                " ccmember_name," +
                                " ccgroup_name," +
                                " ccmember_gid," +
                                " created_by," +
                                " created_date)" +
                                " values(" +
                                "'" + msGetGid1 + "'," +
                                "'" + values.application_gid + "'," +
                                "'" + msGetGid + "'," +
                                "'" + dt["ccmember_name"].ToString() + "'," +
                                "'" + dt["ccgroup_name"].ToString() + "'," +
                                "'" + dt["ccmember_gid"].ToString() + "'," +
                                "'" + employee_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                dt_datatable.Dispose();
                //Other users
                if (values.otheremployee_list == null)
                {
                }
                else
                {
                    for (var i = 0; i < values.otheremployee_list.Count; i++)
                    {
                        msGetGid1 = objcmnfunctions.GetMasterGID("CPRI");
                        msSQL = " insert into agr_mst_tccmeeting2othermembers(" +
                                " ccmeeting2othermembers_gid," +
                                " application_gid," +
                                " ccschedulemeeting_gid," +
                                " employee_name," +
                                " employee_gid," +
                                " created_by," +
                                " created_date)" +
                                " values(" +
                                "'" + msGetGid1 + "'," +
                                "'" + values.application_gid + "'," +
                                "'" + msGetGid + "'," +
                                "'" + values.otheremployee_list[i].employee_name + "'," +
                                "'" + values.otheremployee_list[i].employee_gid + "'," +
                                "'" + employee_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }

                values.status = true;
                values.message = "Meeting Scheduled Successfully";

                //Mail Starts

                msSQL = " select  distinct b.employee_emailid from ocs_mst_tccmember a " +
                       " left join hrm_mst_temployee b on a.ccmember_gid = b.employee_gid " +
                       " left join adm_mst_tuser c on b.user_gid = c.user_gid " +
                       " left join ocs_mst_tccgroupname d on d.ccgroupname_gid = a.ccgroupname_gid " +
                       " where user_status = 'Y' and d.ccgroupname_gid in  ('" + lsccgroup_gid.Replace(",", "','") + "')";
                tomail_id1 = objdbconn.GetExecuteScalar(msSQL);

                if (lsotheruser_gid == "" || lsotheruser_gid == null)
                {

                }
                else
                {
                    msSQL = " select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lsotheruser_gid.Replace(",", "','") + "')";
                    tomail_id2 = objdbconn.GetExecuteScalar(msSQL);
                }

                msSQL = " select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lsccadmin_gid.Replace(",", "','") + "')";
                cc_mailid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select application_no from agr_mst_tapplication where application_gid = '" + values.application_gid + "'";
                string application_no = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select customer_name from agr_mst_tapplication where application_gid = '" + values.application_gid + "'";
                string customer_name = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select vertical_name from agr_mst_tapplication where application_gid = '" + values.application_gid + "'";
                string vertical = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select overalllimit_amount from agr_mst_tapplication where application_gid = '" + values.application_gid + "'";
                string overall_limit = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " SELECT pop_server, pop_port, pop_username, pop_password" +
                        " FROM adm_mst_tcompany";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    ls_server = objODBCDatareader["pop_server"].ToString();
                    ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                    ls_username = objODBCDatareader["pop_username"].ToString();
                    ls_password = objODBCDatareader["pop_password"].ToString();
                }
                objODBCDatareader.Close();
                string[] tomail1_array = tomail_id1.Split(',');

                if (tomail_id2 == "" || tomail_id2 == null)
                {

                }
                else
                {
                    string[] tomail2_array = tomail_id2.Split(',');
                    tomail1_array = tomail1_array.Union(tomail2_array).ToArray();
                }
                string[] ccmail = cc_mailid.Split(',');
                string[] ccmail_Array = ccmail.Except(tomail1_array).ToArray();
                tomail_id = string.Join(",", tomail1_array);
                cc_mailid = string.Join(",", ccmail_Array);
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(ls_username);
                string[] lstoReceipients;
                if (tomail_id != null & tomail_id != string.Empty & tomail_id != "")
                {
                    lstoReceipients = tomail1_array;
                    if (tomail_id.Length == 0)
                    {
                        message.To.Add(new MailAddress(tomail_id));
                    }
                    else
                    {
                        foreach (string tomail_id in tomail1_array)
                        {
                            message.To.Add(new MailAddress(tomail_id));
                        }
                    }
                }
                if (cc_mailid != null & cc_mailid != string.Empty & cc_mailid != "")
                {
                    lsCCReceipients = ccmail_Array;
                    if (cc_mailid.Length == 0)
                    {
                        message.CC.Add(new MailAddress(cc_mailid));
                    }
                    else
                    {
                        foreach (string CCEmail in ccmail_Array)
                        {
                            message.CC.Add(new MailAddress(CCEmail)); //Adding Multiple CC email Id
                        }
                    }
                }
                lsBccmail_id = ConfigurationManager.AppSettings["SamagroCCMeetingbcc"].ToString();

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
                sub = " CC Meeting scheduled-" + HttpUtility.HtmlEncode( Convert.ToDateTime (values.ccmeeting_date).ToString("yyyy-MM-dd")) + "-" + Convert.ToDateTime(values.start_time).ToString("HH:mm:ss");
                body = "&nbsp &nbsp <br /> ";
                body = body + "&nbsp&nbsp Dear Sir/Madam,<br /><br />";
                body = body + "&nbsp&nbsp A new CC Meeting has been scheduled, details are given below,<br /><br />";
                body = body + "&nbsp &nbsp <b> Application Number : </b> " + application_no + "  <br /><br />";
                body = body + "&nbsp &nbsp <b> Customer Name : </b> " + HttpUtility.HtmlEncode(customer_name)+ "  <br /><br />";
                body = body + "&nbsp &nbsp <b> Customer/Supplier Type : </b> " + HttpUtility.HtmlEncode(vertical)+ "  <br /><br />";
                body = body + "&nbsp &nbsp <b> Overall Limit : </b> " + HttpUtility.HtmlEncode(overall_limit)+ "  <br /><br />";
                body = body + "&nbsp &nbsp <b> CC Meeting Name : </b> " + HttpUtility.HtmlEncode(values.ccmeeting_title).Replace("'", "") + "  <br /><br />";
                body = body + "&nbsp &nbsp <b> CC Meeting Date : </b> " + Convert.ToDateTime(values.ccmeeting_date).ToString("yyyy-MM-dd") + "  <br />";
                body = body + "<br />";
                body = body + "&nbsp &nbsp <b> Start Time : </b> " + Convert.ToDateTime(values.start_time).ToString("HH:mm:ss") + "  <br />";
                body = body + "<br />";
                body = body + "&nbsp &nbsp <b> End Time : </b> " + Convert.ToDateTime(values.end_time).ToString("HH:mm:ss") + "  <br />";
                body = body + "<br />";
                body = body + "&nbsp &nbsp <b> CC Meeting Mode : </b> " + HttpUtility.HtmlEncode(values.ccmeeting_mode) + "  <br />";
                body = body + "<br />";
                if (values.description == null || values.description == "")
                {

                }
                else
                {
                    body = body + "&nbsp &nbsp <b> Description : </b>" + HttpUtility.HtmlEncode(values.description).Replace("'", "");
                }
                body = body + "<br /><br />";
                body = body + "&nbsp &nbsp Log into " + ConfigurationManager.AppSettings["livedomain_url"].ToString() + " and complete the necessary actions.";
                body = body + "<br /><br />";
                //body = body + "&nbsp &nbsp Regards";
                //body = body + "<br />";
                //body = body + "&nbsp &nbsp Sam-Custopedia <br /> ";
                //body = body + "<br />";


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
                //mail Ends
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while Schedule Meeting";
                return false;
            }
        }

        //Get Schedule Meeting Information
        public void DaGetScheduleMeeting(string application_gid, string employee_gid, MdlMstCCschedule values)
        {
            try
            {
                msSQL = " select a.ccschedulemeeting_gid,a.ccmeeting_no,a.ccmeeting_title,a.start_time,a.end_time,a.ccmeeting_mode,a.ccgroup_name,a.ccgroupname_gid," +
                     " a.description,date_format(a.ccmeeting_date,'%d-%m-%Y') as ccmeeting_date,ccmeeting_date as meeting_date,otheruser_name," +
                     " otheruser_gid,non_users,ccadmin_gid,ccadmin_name," +
                     " concat(time_format(start_time, '%h:%i %p'), '-', time_format(end_time, '%h:%i %p')) as ccmeeting_time from agr_mst_tccschedulemeeting a" +
                     " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                     " left join adm_mst_tuser c on c.user_gid = b.user_gid where application_gid='" + application_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.ccschedulemeeting_gid = objODBCDatareader["ccschedulemeeting_gid"].ToString();
                    values.ccmeeting_no = objODBCDatareader["ccmeeting_no"].ToString();
                    values.ccmeeting_title = objODBCDatareader["ccmeeting_title"].ToString();
                    values.ccmeeting_time = objODBCDatareader["ccmeeting_time"].ToString();
                    values.end_time = objODBCDatareader["end_time"].ToString();
                    values.start_time = objODBCDatareader["start_time"].ToString();
                    values.ccmeeting_mode = objODBCDatareader["ccmeeting_mode"].ToString();
                    values.ccgroup_name = objODBCDatareader["ccgroup_name"].ToString();
                    values.ccgroupname_gid = objODBCDatareader["ccgroupname_gid"].ToString();
                    values.description = objODBCDatareader["description"].ToString();
                    values.ccmeeting_date = objODBCDatareader["ccmeeting_date"].ToString();
                    values.otheruser_name = objODBCDatareader["otheruser_name"].ToString();
                    values.non_users = objODBCDatareader["non_users"].ToString();
                    values.ccadmin_name = objODBCDatareader["ccadmin_name"].ToString();
                    if (objODBCDatareader["meeting_date"].ToString() == "")
                    {
                    }
                    else
                    {
                        values.meeting_date = Convert.ToDateTime(objODBCDatareader["meeting_date"]).ToString("MM-dd-yyyy");
                    }
                    if (objODBCDatareader["start_time"].ToString() == "" || objODBCDatareader["start_time"].ToString() == null)
                    {
                    }
                    else
                    {
                        values.Tstart_time = Convert.ToDateTime(objODBCDatareader["start_time"].ToString());
                    }
                    if (objODBCDatareader["end_time"].ToString() == "" || objODBCDatareader["end_time"].ToString() == null)
                    {
                    }
                    else
                    {
                        values.Tend_time = Convert.ToDateTime(objODBCDatareader["end_time"].ToString());
                    }

                    //CC Group Name
                    String[] ccgroupGID_list = objODBCDatareader["ccgroupname_gid"].ToString().Split(',');
                    String[] ccgroupname_list = objODBCDatareader["ccgroup_name"].ToString().Split(',');


                    var getccschedule_list = new List<ccschedule_list>();
                    for (var i = 0; i < ccgroupGID_list.Length; i++)
                    {
                        getccschedule_list.Add(new ccschedule_list
                        {
                            ccgroupname_gid = ccgroupGID_list[i],
                            ccgroup_name = ccgroupname_list[i],
                        });
                    }
                    values.ccschedule_list = getccschedule_list;
                    //Other Users List
                    if (objODBCDatareader["otheruser_gid"].ToString() == "" || objODBCDatareader["otheruser_gid"].ToString() == null)
                    {

                    }
                    else
                    {
                        String[] OtheruserGID_list = objODBCDatareader["otheruser_gid"].ToString().Split(',');
                        String[] Otherusername_list = objODBCDatareader["otheruser_name"].ToString().Split(',');

                        var getotheremployee_list = new List<otheremployee_list>();
                        for (var i = 0; i < OtheruserGID_list.Length; i++)
                        {
                            getotheremployee_list.Add(new otheremployee_list
                            {
                                employee_gid = OtheruserGID_list[i],
                                employee_name = Otherusername_list[i],
                            });
                        }
                        values.otheremployee_list = getotheremployee_list;
                    }
                    //CC Admin List
                    if (objODBCDatareader["ccadmin_gid"].ToString() == "" || objODBCDatareader["ccadmin_gid"].ToString() == null)
                    {

                    }
                    else
                    {


                        String[] CCAdminGID_list = objODBCDatareader["ccadmin_gid"].ToString().Split(',');
                        String[] CCAdminName_list = objODBCDatareader["ccadmin_name"].ToString().Split(',');

                        var getccadmin_list = new List<ccadmin_list>();
                        for (var i = 0; i < CCAdminGID_list.Length; i++)
                        {
                            getccadmin_list.Add(new ccadmin_list
                            {
                                employee_gid = CCAdminGID_list[i],
                                employee_name = CCAdminName_list[i],
                            });
                        }
                        values.ccadmin_list = getccadmin_list;
                    }
                }
                values.status = true;
                objODBCDatareader.Close();

                try
                {
                    msSQL = " select a.ccmember_name,a.ccmember_gid,a.ccgroup_name,a.ccmeeting2members_gid,a.ccapproval_flag,b.momapproval_flag, " +
                        " a.attendance_status,a.approval_status,b.approval_status as overapproval_status,date_format(a.approved_date,'%d-%m-%Y %h:%i %p') as approveddate " +
                        " from agr_mst_tccmeeting2members a left join agr_mst_tapplication b on b.application_gid = a.application_gid " +
                        " where a.application_gid='" + application_gid + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getccmember_list = new List<ccmember_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            getccmember_list.Add(new ccmember_list
                            {
                                CCMember_name = dt["ccmember_name"].ToString(),
                                ccmember_gid = dt["ccmember_gid"].ToString(),
                                ccgroup_name = dt["ccgroup_name"].ToString(),
                                attendance_status = dt["attendance_status"].ToString(),
                                ccmeeting2members_gid = dt["ccmeeting2members_gid"].ToString(),
                                approval_status = dt["approval_status"].ToString(),
                                ccapproval_flag = dt["ccapproval_flag"].ToString(),
                                overapproval_status = dt["overapproval_status"].ToString(),
                                momapproval_flag = dt["momapproval_flag"].ToString(),
                                approved_date = dt["approveddate"].ToString(),
                            });
                        }
                        values.ccmember_list = getccmember_list;
                    }
                    dt_datatable.Dispose();

                    msSQL = " select employee_name,employee_gid,attendance_status, ccmeeting2othermembers_gid,approval_status " +
                       " from agr_mst_tccmeeting2othermembers where application_gid='" + application_gid + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getccothermember_list = new List<otheruser_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            getccothermember_list.Add(new otheruser_list
                            {
                                employee_name = dt["employee_name"].ToString(),
                                employee_gid = dt["employee_gid"].ToString(),
                                attendance_status = dt["attendance_status"].ToString(),
                                ccmeeting2othermembers_gid = dt["ccmeeting2othermembers_gid"].ToString(),
                                approval_status = dt["approval_status"].ToString(),
                            });
                        }
                        values.otheruser_list = getccothermember_list;
                    }
                    dt_datatable.Dispose();

                    //log

                    msSQL = " select ccmember_name,ccmember_gid,ccgroup_name,ccmeeting2members_gid,attendance_status,approval_status " +
                        " from agr_mst_tccmeeting2memberslog where application_gid='" + application_gid + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getccmemberlog_list = new List<ccmemberlog_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            getccmemberlog_list.Add(new ccmemberlog_list
                            {
                                CCMember_name = dt["ccmember_name"].ToString(),
                                ccmember_gid = dt["ccmember_gid"].ToString(),
                                ccgroup_name = dt["ccgroup_name"].ToString(),
                                attendance_status = dt["attendance_status"].ToString(),
                                ccmeeting2members_gid = dt["ccmeeting2members_gid"].ToString(),
                                approval_status = dt["approval_status"].ToString(),
                            });
                        }
                        values.ccmemberlog_list = getccmemberlog_list;
                    }
                    dt_datatable.Dispose();

                    msSQL = " select employee_name,employee_gid,attendance_status, ccmeeting2othermemberslog_gid,approval_status " +
                       " from agr_mst_tccmeeting2othermemberslog where application_gid='" + application_gid + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getccothermemberlog_list = new List<otheruserlog_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            getccothermemberlog_list.Add(new otheruserlog_list
                            {
                                employee_name = dt["employee_name"].ToString(),
                                employee_gid = dt["employee_gid"].ToString(),
                                attendance_status = dt["attendance_status"].ToString(),
                                ccmeeting2othermembers_gid = dt["ccmeeting2othermemberslog_gid"].ToString(),
                                approval_status = dt["approval_status"].ToString(),
                            });
                        }
                        values.otheruserlog_list = getccothermemberlog_list;
                    }
                    dt_datatable.Dispose();
                }
                catch (Exception ex)
                {
                    values.status = false;
                    values.message = ex.Message.ToString();
                }
            }
            catch
            {
                values.status = false;
            }
        }

        public bool DaRecheduleMeeting(string employee_gid, MdlMstCCschedule values)
        {

            msSQL = " select application_gid from agr_mst_tapplication where application_gid = '" + values.application_gid + "' and momapproval_flag = 'Y'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Submit to approve has been initiated so you can't reschedule meeting ";
                return true;
            }
            else
            {
                objODBCDatareader.Close();
                msSQL = " insert into agr_mst_tccschedulemeetinglog (select ccschedulemeeting_gid, " +
                   " application_gid, ccmeeting_no, ccmeeting_title, ccmeeting_date, start_time,  " +
                   " end_time, ccmeeting_mode, ccgroupname_gid, ccgroup_name, description, " +
                   "  created_by, created_date, updated_by, updated_date, otheruser_gid,  " +
                   " otheruser_name, non_users, ccadmin_name, ccadmin_gid " +
                   " from agr_mst_tccschedulemeeting where " +
               " ccschedulemeeting_gid='" + values.ccschedulemeeting_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                string lsccgroup_gid = string.Empty;
                string lsccgroup_name = string.Empty;
                string lsotheruser_gid = string.Empty;
                string lsotheruser_name = string.Empty;
                string lsccadmin_gid = string.Empty;
                string lsccadmin_name = string.Empty;
                string loopccgroup_gid = string.Empty;
                if (values.ccschedule_list != null)
                {
                    for (var i = 0; i < values.ccschedule_list.Count; i++)
                    {
                        lsccgroup_gid += values.ccschedule_list[i].ccgroupname_gid + ",";
                        lsccgroup_name += values.ccschedule_list[i].ccgroup_name + ",";
                        loopccgroup_gid += "'" + values.ccschedule_list[i].ccgroupname_gid + "'" + ",";
                    }
                    lsccgroup_gid = lsccgroup_gid.TrimEnd(',');
                    lsccgroup_name = lsccgroup_name.TrimEnd(',');
                    loopccgroup_gid = loopccgroup_gid.TrimEnd(',');
                }
                if (values.otheremployee_list != null)
                {
                    for (var i = 0; i < values.otheremployee_list.Count; i++)
                    {
                        lsotheruser_gid += values.otheremployee_list[i].employee_gid + ",";
                        lsotheruser_name += values.otheremployee_list[i].employee_name + ",";
                    }

                    lsotheruser_gid = lsotheruser_gid.TrimEnd(',');
                    lsotheruser_name = lsotheruser_name.TrimEnd(',');
                }
                //CC Admin List
                if (values.ccadmin_list != null)
                {
                    for (var i = 0; i < values.ccadmin_list.Count; i++)
                    {
                        lsccadmin_gid += values.ccadmin_list[i].employee_gid + ",";
                        lsccadmin_name += values.ccadmin_list[i].employee_name + ",";
                    }

                    lsccadmin_gid = lsccadmin_gid.TrimEnd(',');
                    lsccadmin_name = lsccadmin_name.TrimEnd(',');
                }
                msSQL = " update agr_mst_tccschedulemeeting set" +
                        " ccmeeting_title='" + values.ccmeeting_title.Replace("'", "").Replace("'", "") + "'," +
                        " ccmeeting_no='" + values.ccmeeting_no.Replace("'", "").Replace("'", "") + "',";
                if ((values.start_time == null) || (values.start_time == ""))
                {
                    msSQL += "start_time=null,";
                }
                else
                {
                    msSQL += "start_time='" + Convert.ToDateTime(values.start_time).ToString("HH:mm:ss") + "',";
                }
                if ((values.end_time == null) || (values.end_time == ""))
                {
                    msSQL += "end_time=null,";
                }
                else
                {
                    msSQL += "end_time='" + Convert.ToDateTime(values.end_time).ToString("HH:mm:ss") + "',";
                }
                msSQL += " ccmeeting_mode='" + values.ccmeeting_mode + "'," +
                        " ccgroupname_gid='" + lsccgroup_gid + "'," +
                        " ccgroup_name='" + lsccgroup_name + "'," +
                        " otheruser_name='" + lsotheruser_name + "'," +
                        " otheruser_gid='" + lsotheruser_gid + "'," +
                        " ccadmin_gid='" + lsccadmin_gid + "'," +
                        " ccadmin_name='" + lsccadmin_name + "',";
                if ((values.description == null) || (values.description == ""))
                {
                    msSQL += "description=null,";
                }
                else
                {
                    msSQL += "description='" + values.description.Replace("'", "") + "',";
                }
                if ((values.non_users == null) || (values.non_users == ""))
                {
                    msSQL += "non_users=null,";
                }
                else
                {
                    msSQL += "non_users='" + values.non_users.Replace("'", "") + "',";
                }
                msSQL += " updated_by='" + employee_gid + "'," +
                        " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                        " where ccschedulemeeting_gid='" + values.ccschedulemeeting_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (Convert.ToDateTime(values.meetingdate).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
                {
                }
                else
                {
                    msSQL = "update agr_mst_tccschedulemeeting set ccmeeting_date='" + Convert.ToDateTime(values.meetingdate).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "'" +
                        "where  ccschedulemeeting_gid='" + values.ccschedulemeeting_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                if (mnResult != 0)
                {
                    msSQL = " update agr_mst_tapplication set ccgroup_name='" + lsccgroup_name + "'," +
                             " meeting_status='Scheduled'," +
                             " updated_by = '" + employee_gid + "'," +
                             " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                             " where application_gid='" + values.application_gid + "' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    //CC Members
                    msSQL = "select a.ccmember_name,a.ccmember_gid,b.ccgroup_name from ocs_mst_tccmember a" +
                        " left join ocs_mst_tccgroupname b on a.ccgroupname_gid=b.ccgroupname_gid where" +
                         " a.ccgroupname_gid in (" + loopccgroup_gid + ")";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable.Rows.Count != 0)
                    {
                        msSQL = "delete from agr_mst_tccmeeting2members where ccschedulemeeting_gid='" + values.ccschedulemeeting_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        foreach (DataRow dt in dt_datatable.Rows)
                        {

                            msGetGid1 = objcmnfunctions.GetMasterGID("CM2M");
                            msSQL = " insert into agr_mst_tccmeeting2members(" +
                                    " ccmeeting2members_gid," +
                                    " application_gid," +
                                    " ccschedulemeeting_gid," +
                                    " ccmember_name," +
                                    " ccgroup_name," +
                                    " ccmember_gid," +
                                    " created_by," +
                                    " created_date)" +
                                    " values(" +
                                    "'" + msGetGid1 + "'," +
                                    "'" + values.application_gid + "'," +
                                    "'" + values.ccschedulemeeting_gid + "'," +
                                    "'" + dt["ccmember_name"].ToString() + "'," +
                                    "'" + dt["ccgroup_name"].ToString() + "'," +
                                    "'" + dt["ccmember_gid"].ToString() + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    dt_datatable.Dispose();

                    //Other Users
                    if (values.otheremployee_list == null)
                    {
                    }
                    else
                    {
                        msSQL = "delete from agr_mst_tccmeeting2othermembers where ccschedulemeeting_gid='" + values.ccschedulemeeting_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        for (var i = 0; i < values.otheremployee_list.Count; i++)
                        {

                            msGetGid1 = objcmnfunctions.GetMasterGID("CPRI");
                            msSQL = " insert into agr_mst_tccmeeting2othermembers(" +
                                    " ccmeeting2othermembers_gid," +
                                    " application_gid," +
                                    " ccschedulemeeting_gid," +
                                    " employee_name," +
                                    " employee_gid," +
                                    " created_by," +
                                    " created_date)" +
                                    " values(" +
                                    "'" + msGetGid1 + "'," +
                                    "'" + values.application_gid + "'," +
                                    "'" + values.ccschedulemeeting_gid + "'," +
                                    "'" + values.otheremployee_list[i].employee_name + "'," +
                                    "'" + values.otheremployee_list[i].employee_gid + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    values.status = true;
                    values.message = "Reschedule Meeting Successfully";
                    return true;
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occured while Schedule Meeting";
                    return false;
                }

            }

        }
        //Cancel Meeting
        public bool DaCancelMeeting(string employee_gid, MdlMstCCschedule values)
        {
            msSQL = " update agr_mst_tapplication set ccgroup_name='" + values.ccgroup_name + "'," +
                         " meeting_status='Cancelled'," +
                         " cancel_remark='" + values.remarks + "'," +
                         " updated_by = '" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where application_gid='" + values.application_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = " Meeting Cancelled Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while Cancel Meeting";
                return false;
            }
        }

        public void DaGetScheduleMeetingLog(string application_gid, string employee_gid, MdlMstCCschedule values)
        {
            try
            {
                msSQL = " select a.ccschedulemeetinglog_gid,a.ccmeeting_no,a.ccmeeting_title,a.start_time,a.end_time,a.ccmeeting_mode,a.ccgroup_name,a.ccgroupname_gid," +
                     " a.description,date_format(a.ccmeeting_date,'%d-%m-%Y') as ccmeeting_date," +
                     " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                     " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by from agr_mst_tccschedulemeetinglog a" +
                     " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                     " left join adm_mst_tuser c on c.user_gid = b.user_gid where application_gid='" + application_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getccschedule_list = new List<ccschedule_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getccschedule_list.Add(new ccschedule_list
                        {
                            ccmeeting_no = dt["ccmeeting_no"].ToString(),
                            ccmeeting_title = dt["ccmeeting_title"].ToString(),
                            start_time = dt["start_time"].ToString(),
                            end_time = dt["end_time"].ToString(),
                            ccmeeting_mode = dt["ccmeeting_mode"].ToString(),
                            ccgroup_name = dt["ccgroup_name"].ToString(),
                            description = dt["description"].ToString(),
                            ccmeeting_date = dt["ccmeeting_date"].ToString(),
                            created_by = dt["created_by"].ToString(),
                            created_date = dt["created_date"].ToString(),
                        });
                    }
                    values.ccschedule_list = getccschedule_list;
                }
                dt_datatable.Dispose();
                values.status = true;


            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetcc2member(MdlMstCCMember values)
        {
            try
            {
                string gsccgroup_gid = string.Empty;
                string gsccgrup_name = string.Empty;

                if (values.ccgroup_list != null)
                {
                    for (var i = 0; i < values.ccgroup_list.Count; i++)
                    {
                        gsccgroup_gid += "'" + values.ccgroup_list[i].ccgroupname_gid + "'" + ",";
                    }

                    gsccgroup_gid = gsccgroup_gid.TrimEnd(',');
                }
                msSQL = " select a.ccmember_name,a.ccmember_gid,b.ccgroup_name from ocs_mst_tccmember a" +
                    " left join ocs_mst_tccgroupname b on  a.ccgroupname_gid=b.ccgroupname_gid where a.ccgroupname_gid in (" + gsccgroup_gid + ") ";


                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getccmember_list = new List<ccmember_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getccmember_list.Add(new ccmember_list
                        {
                            CCMember_name = dt["ccmember_name"].ToString(),
                            ccmember_gid = dt["ccmember_gid"].ToString(),
                            ccgroup_name = dt["ccgroup_name"].ToString(),
                        });
                    }
                    values.ccmember_list = getccmember_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = ex.Message.ToString();
            }
        }

        public void DaGetEmployee(MdlOtherEmployee objemployee)
        {
            try
            {
                // CC Group list and Get CC member
                string gsccgroup_gid = string.Empty;


                if (objemployee.ccgroup_list != null)
                {
                    for (var i = 0; i < objemployee.ccgroup_list.Count; i++)
                    {
                        gsccgroup_gid += "'" + objemployee.ccgroup_list[i].ccgroupname_gid + "'" + ",";
                    }

                    gsccgroup_gid = gsccgroup_gid.TrimEnd(',');
                }

                msSQL = "select group_concat(CONCAT('''', ccmember_gid, '''' )) from ocs_mst_tccmember" +
                    " where ccgroupname_gid  in (" + gsccgroup_gid + ") ";
                string lsmember_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " SELECT a.user_firstname,a.user_gid ,concat(a.user_firstname,' ',a.user_lastname,' / ',a.user_code) as employee_name,b.employee_gid from adm_mst_tuser a " +
                   " LEFT JOIN hrm_mst_temployee b ON a.user_gid=b.user_gid " +
                   " where user_status<>'N' and b.employee_gid not in (" + lsmember_gid + ")" +
                   " order by a.user_firstname asc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_employee = new List<otheremployee_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    objemployee.otheremployee_list = dt_datatable.AsEnumerable().Select(row =>
                      new otheremployee_list
                      {
                          employee_gid = row["employee_gid"].ToString(),
                          employee_name = row["employee_name"].ToString()
                      }
                    ).ToList();
                }
                dt_datatable.Dispose();
                objemployee.status = true;
            }
            catch (Exception ex)
            {
                objemployee.status = false;
            }
        }
        public bool DaGetCCCalenderDtl(calendarevent values, string employee_gid)
        {

            msSQL = " select distinct date_format(a.ccmeeting_date,'%Y-%m-%d') as ccmeeting_date, a.start_time,a.end_time,a.ccmeeting_title  " +
                    " from agr_mst_tccschedulemeeting a" +
                    " left join agr_mst_tccmeeting2members b on a.application_gid=b.application_gid" +
                    " left join agr_mst_tccmeeting2othermembers c on a.application_gid=c.application_gid" +
                    " where (a.start_time is not null and a.end_time is not null and a.ccmeeting_date is not null)" +
                    " and (a.created_by='" + employee_gid + "' or  ccmember_gid='" + employee_gid + "' or c.employee_gid='" + employee_gid + "'" +
                    " or a.ccadmin_gid like '%" + employee_gid + "%')group by a.application_gid";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var geteventlist = new List<createevent>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {


                    geteventlist.Add(new createevent
                    {

                        event_date = Convert.ToDateTime(dr_datarow["ccmeeting_date"].ToString()),
                        event_time = (Convert.ToDateTime(dr_datarow["start_time"].ToString())),
                        event_title = ((dr_datarow["ccmeeting_title"].ToString())),
                    });
                }
                values.createevent = geteventlist;
            }
            dt_datatable.Dispose();
            return true;
        }

        public void DaMOMDescSave(string employee_gid, MdlMstCC values)
        {
            msSQL = " update agr_mst_tapplication set ";
            if (values.mom_description == null || values.mom_description == "")
            {
                msSQL += " mom_description='',";
            }
            else
            {
                msSQL += " mom_description='" + values.mom_description.Replace("'", "") + "',";

            }
            msSQL += " updated_by='" + employee_gid + "'," +
                      " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                      " where application_gid='" + values.application_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "MOM Description Saved Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured While Saving MOM Description";
            }
        }

        public bool DaMOMDocumentUpload(HttpRequest httpRequest, MdlMstCC objfilename, string employee_gid)
        {
            upload_list objdocumentmodel = new upload_list();
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string lsdocument_title = httpRequest.Form["document_title"].ToString();
            string lsapplication_gid = httpRequest.Form["application_gid"].ToString();
            string path, lspath;
            string project_flag = httpRequest.Form["project_flag"].ToString();

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/MOMDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
            {
                if ((!System.IO.Directory.Exists(path)))
                    System.IO.Directory.CreateDirectory(path);
            }
            try
            {
                if (httpRequest.Files.Count > 0)
                {
                    string lsfirstdocument_filepath = string.Empty;
                    httpFileCollection = httpRequest.Files;
                    for (int i = 0; i < httpFileCollection.Count; i++)
                    {
                        string msdocument_gid = objcmnfunctions.GetMasterGID("UPLF");
                        httpPostedFile = httpFileCollection[i];
                        string FileExtension = httpPostedFile.FileName;
                        string lsfile_gid = msdocument_gid;
                        FileExtension = Path.GetExtension(FileExtension).ToLower();
                        lsfile_gid = lsfile_gid + FileExtension;
                        Stream ls_readStream;
                        ls_readStream = httpPostedFile.InputStream;
                        MemoryStream ms = new MemoryStream();
                        ls_readStream.CopyTo(ms);

                        // Check Document validation;

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.message = "File format is not supported";
                            return false;
                        }

                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "SamAgro/MOMDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "SamAgro/MOMDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";


                        msGetGid = objcmnfunctions.GetMasterGID("A2MD");
                        msSQL = " insert into agr_mst_tapplication2momdoc( " +
                                    " application2momdoc_gid," +
                                    " application_gid," +
                                    " document_title  ," +
                                    " document_name  ," +
                                    " document_path," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + lsapplication_gid + "'," +
                                    "'" + lsdocument_title + "'," +
                                    "'" + httpPostedFile.FileName + "'," +
                                    "'" + lspath + msdocument_gid + FileExtension + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult == 1)
                        {
                            msSQL = " update agr_mst_tapplication set " +
                                    " momdocumentupload_flag='Y'," +
                                    " updated_by='" + employee_gid + "'," +
                                      " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                                      " where application_gid='" + lsapplication_gid + "' ";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            objfilename.status = true;
                            objfilename.message = "Document Uploaded Successfully..!";
                        }
                        else
                        {
                            objfilename.status = false;
                            objfilename.message = "Error Occured..!";
                        }

                        msSQL = " select application2momdoc_gid,application_gid,document_name,document_path,document_title from " +
                            " agr_mst_tapplication2momdoc where application_gid='" + lsapplication_gid + "'";
                        dt_datatable = objdbconn.GetDataTable(msSQL);
                        var getdocumentdtlList = new List<momdocument_list>();
                        if (dt_datatable.Rows.Count != 0)
                        {
                            foreach (DataRow dt in dt_datatable.Rows)
                            {
                                getdocumentdtlList.Add(new momdocument_list
                                {
                                    document_name = dt["document_name"].ToString(),
                                    document_title = dt["document_title"].ToString(),
                                    document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                                    application_gid = dt["application_gid"].ToString(),
                                    application2momdoc_gid = dt["application2momdoc_gid"].ToString(),

                                });
                                objfilename.momdocument_list = getdocumentdtlList;
                            }
                        }
                        dt_datatable.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                objfilename.message = ex.ToString();
            }
            return true;
        }

        public void DaMOMDescSubmit(string employee_gid, MdlMstCC values)
        {
            msSQL = " update agr_mst_tapplication set ";
            if (values.mom_description == null || values.mom_description == "")
            {
                msSQL += " mom_description='',";
            }
            else
            {
                msSQL += " mom_description='" + values.mom_description.Replace("'", "") + "',";

            }
            msSQL += " updated_by='" + employee_gid + "'," +
                     " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                     " where application_gid='" + values.application_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "MOM Details Submitted  Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured While Submitting MOM Details";
            }
        }
        //Query Conversation 
        public bool DaPostSendCCRequestor(string employee_gid, ccrequestordtl values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("CCRC");
            msSQL = " insert into agr_trn_tccrequestorcommunication(" +
                    " ccrequestorcommunication_gid," +
                    " application_gid," +
                    " remarks," +
                    " ccrequestor_gid," +
                    " created_by, " +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.application_gid + "'," +
                    "'" + values.remarks.Replace("'", "") + "'," +
                    "'" + employee_gid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.message = "Message Sent Successfully";
                values.status = true;
                return true;
            }
            else
            {
                values.message = "Error Occured..!";
                values.status = false;
                return false;
            }
        }
        //Query Conversation List
        public bool DaGetRequestorlist(string employee_gid, string application_gid, ccrequestorlist values)
        {
            try
            {
                msSQL = "select a.ccrequestorcommunication_gid,a.remarks,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date ,a.ccrequestor_gid,a.document_name,a.document_path," +
                    " concat(c.user_firstname, ' ', c.user_lastname, '/ ', c.user_code) as sender_name " +
                    " from agr_trn_tccrequestorcommunication a " +
                    " left join hrm_mst_temployee b on a.ccrequestor_gid = b.employee_gid " +
                    " left join adm_mst_tuser c on b.user_gid = c.user_gid " +
                    " where a.application_gid = '" + application_gid + "' order by a.created_date asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getccrequestordtl = new List<ccrequestordtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    if (dr_datarow["ccrequestor_gid"].ToString() == employee_gid)
                    {
                        lssession_user = "Y";
                    }
                    else
                    {
                        lssession_user = "N";
                    }
                    if (dr_datarow["document_name"].ToString() == "")
                    {
                        lsdocument_attached = "N";
                    }
                    else
                    {
                        lsdocument_attached = "Y";
                    }
                    getccrequestordtl.Add(new ccrequestordtl
                    {
                        ccrequestorcommunication_gid = (dr_datarow["ccrequestorcommunication_gid"].ToString()),
                        remarks = (dr_datarow["remarks"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        sender_name = (dr_datarow["sender_name"].ToString()),
                        document_name = (dr_datarow["document_name"].ToString()),
                        document_path = objcmnstorage.EncryptData(dr_datarow["document_path"].ToString()),
                        session_user = lssession_user,
                        document_attached = lssession_user + "/" + lsdocument_attached
                    });
                }
                values.ccrequestordtl = getccrequestordtl;
            }
            dt_datatable.Dispose();
            values.status = true;
            return true;

            }
            catch
            {
                values.message = "Error Occured..!";
                values.status = false;
                return false;
            }
}
        //Query Conversation Document Upload
        public void DaConversationCCDocUpload(HttpRequest httpRequest, upload_document objfilename, string employee_gid, string user_gid)
        {
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string pdfFilName = string.Empty;
            Stream ls_readStream;
            string lsdocumenttype_gid = string.Empty;
            String path = lspath;
            string lsdocument_title = httpRequest.Form["document_title"];
            string application_gid = HttpContext.Current.Request.Params["application_gid"];
            string project_flag = httpRequest.Form["project_flag"].ToString();

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            // path = HttpContext.Current.Server.MapPath("../../../erpdocument" + "/" + lscompany_code + "/" + "SamAgro/ConversationCCDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/ConversationCCDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
            if ((!System.IO.Directory.Exists(path)))
                System.IO.Directory.CreateDirectory(path);

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
                        MemoryStream ms = new MemoryStream();
                        ls_readStream.CopyTo(ms);

                        // Check Document validation;

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.message = "File format is not supported";
                            objfilename.status = false;
                            return;
                        }
                
                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "SamAgro/ConversationCCDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "SamAgro/ConversationCCDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        msGetGid = objcmnfunctions.GetMasterGID("CCRC");
                        msSQL = " insert into agr_trn_tccrequestorcommunication(" +
                                " ccrequestorcommunication_gid," +
                                " application_gid," +
                                " ccrequestor_gid," +
                                " document_name ," +
                                " document_path," +
                                " created_by, " +
                                " created_date)" +
                                " values(" +
                                "'" + msGetGid + "'," +
                                "'" + application_gid + "'," +
                                "'" + employee_gid + "'," +
                                "'" + httpPostedFile.FileName + "'," +
                                "'" + lspath + msdocument_gid + FileExtension + "'," +
                                "'" + employee_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                        if (mnResult != 0)
                        {
                            objfilename.status = true;
                            objfilename.message = "Document Uploaded Successfully";
                        }
                        else
                        {
                            objfilename.status = false;
                            objfilename.message = "Error Occured";
                        }
                    }
                }
            }
            catch
            {
            }
        }
        public void DaGetAdminPrivilege(string employee_gid, string application_gid, MdlMstCC values)
        {
            msSQL = "select application_gid from agr_mst_tccschedulemeeting where ccadmin_gid like '%" + employee_gid + "%' and application_gid='" + application_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.privilege_gid = "Y";
            }
            else
            {
                values.privilege_gid = "N";
            }
            objODBCDatareader.Close();
            values.status = true;
        }
        public void DaGetMOMDescription(string employee_gid, string application_gid, MdlMstCC values)
        {
            msSQL = "select mom_description from agr_mst_tapplication where application_gid='" + application_gid + "'";
            values.mom_description = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select application2momdoc_gid,application_gid,document_name,document_path,document_title from " +
                            " agr_mst_tapplication2momdoc where application_gid='" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdocumentdtlList = new List<momdocument_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getdocumentdtlList.Add(new momdocument_list
                    {
                        document_name = dt["document_name"].ToString(),
                        document_title = dt["document_title"].ToString(),
                        document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                        application_gid = dt["application_gid"].ToString(),
                        application2momdoc_gid = dt["application2momdoc_gid"].ToString(),

                    });
                    values.momdocument_list = getdocumentdtlList;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaPostCCAttendance(string employee_gid, MdlCCAttendance values)
        {
            msSQL = " update agr_mst_tccmeeting2members set " +
                " attendance_status='" + values.attendance_status + "'," +
                " updated_by='" + employee_gid + "'," +
                      " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                      " where ccmeeting2members_gid='" + values.ccmeeting2members_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Attendance Status Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured While Updating Attendance";
            }
        }
        public void DaPostothersAttendance(string employee_gid, MdlCCAttendance values)
        {
            msSQL = " update agr_mst_tccmeeting2othermembers set " +
                " attendance_status='" + values.attendance_status + "'," +
                " updated_by='" + employee_gid + "'," +
                      " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                      " where ccmeeting2othermembers_gid='" + values.ccmeeting2othermembers_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Attendance Status Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured While Updating Attendance";
            }
        }
        public void DaPostMOMSubmit(string employee_gid, MdlMstCC values, string user_gid)
        {
            msSQL = " update agr_mst_tapplication set " +
                              " momapproval_flag='Y'," +
                              " momupdated_by='" + employee_gid + "'," +
                              " momupdated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                              " updated_by='" + employee_gid + "'," +
                              " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                              " where application_gid='" + values.application_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msSQL = "update agr_mst_tccmeeting2othermembers set approval_status='Pending' where application_gid='" + values.application_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tccmeeting2members set approval_status='Pending', ccmail_flag = 'Y' where application_gid='" + values.application_gid + "' and ccapproval_flag = 'Y'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);



                if (mnResult != 0)
                {

                    k = 1;

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

                    msSQL = " select a.application_gid, a.ccmember_name,a.ccmember_gid,a.ccgroup_name,a.ccmeeting2members_gid,f.ccadmin_gid,  " +
                            " a.ccapproval_flag,concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as approvalinitiate_by, " +
                            " date_format(a.approved_date,'%d-%m-%Y %h:%i %p') as approved_date, " +
                            " a.attendance_status,a.approval_status,b.approval_status as overapproval_status " +
                            " from agr_mst_tccmeeting2members a  " +
                            " left join agr_mst_tapplication b on b.application_gid = a.application_gid " +
                            " left join agr_mst_tccschedulemeeting f on a.application_gid = f.application_gid " +
                            " left join hrm_mst_temployee d on a.approvalinitiate_by = d.employee_gid" +
                            " left join adm_mst_tuser e on e.user_gid = d.user_gid " +
                            " where a.application_gid='" + values.application_gid + "'and  a.ccapproval_flag = 'Y'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getccmember_list = new List<ccmembers_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {

                            lsemployee_name = dt["ccmember_name"].ToString();
                            lsemployee_gid = dt["ccmember_gid"].ToString();
                            lsccadmin_gid = dt["ccadmin_gid"].ToString();

                            string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
                            sToken = "";
                            int Length = 100;
                            for (int j = 0; j < Length; j++)
                            {
                                string sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                                sToken += sTempChars;
                            }

                            msGetGid = objcmnfunctions.GetMasterGID("CCAP");

                            msSQL = "Insert into agr_trn_tccapproval( " +
                                   " ccapproval_gid, " +
                                   " application_gid, " +
                                   " ccmeeting2members_gid," +
                                   " approval_gid," +
                                   " approval_name," +
                                   " approval_token," +
                                   //" requestapproval_remarks," +
                                   " ccapprovalrequest_flag," +
                                   " created_by," +
                                   " created_date)" +
                                   " values(" +
                                   "'" + msGetGid + "'," +
                                   "'" + dt["application_gid"].ToString() + "'," +
                                   "'" + dt["ccmeeting2members_gid"].ToString() + "'," +
                                   "'" + lsemployee_gid + "'," +
                                   "'" + lsemployee_name + "'," +
                                   "'" + sToken + "'," +
                                   //"'" + values.approval_remarks.Replace("'", "\\'") + "'," +
                                   "'Y'," +
                                   "'" + user_gid + "'," +
                                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            k = k + 1;


                            msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee " +
                                    " where employee_gid = '" + lsemployee_gid + "'";
                            lsto_mail = objdbconn.GetExecuteScalar(msSQL);


                            //msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lsccadmin_gid.Replace(",", "', '") + "')";
                            //cc_mailid = objdbconn.GetExecuteScalar(msSQL);

                            msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lsccadmin_gid.Replace(",", "', '") + "')";
                            cc_mailid = objdbconn.GetExecuteScalar(msSQL);


                            msSQL = " select   (b.employee_emailid)  from agr_mst_tapplication a  " +
                                    " left join hrm_mst_temployee b on a.relationshipmanager_gid = b.employee_gid  " +
                                    " left join adm_mst_tuser c on b.user_gid = c.user_gid " +
                                     " where a.application_gid = '" + values.application_gid + "'";
                            cc_mailid1 = objdbconn.GetExecuteScalar(msSQL);


                            msSQL = " select   (b.employee_emailid)  from agr_trn_tapplicationapproval a  " +
                                    " left join hrm_mst_temployee b on a.approval_gid = b.employee_gid  " +
                                    " where a.hierary_level= '1' and a.application_gid = '" + values.application_gid + "'";
                            cc_mailid2 = objdbconn.GetExecuteScalar(msSQL);



                            msSQL = " select a.application_no,d.ccadmin_name,a.customer_name,e.loanfacility_amount,a.ccgroup_name,d.ccmeeting_date" +
                                    " from agr_mst_tapplication a left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                    " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                                    " left join agr_mst_tccschedulemeeting d on a.application_gid = d.application_gid" +
                                    " left join agr_mst_tapplication2loan e on a.application_gid = e.application_gid" +
                                    " where a.application_gid = '" + values.application_gid + "'";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                lsapplication_no = objODBCDatareader["application_no"].ToString();
                                lsccadmin_name = objODBCDatareader["ccadmin_name"].ToString();
                                lscustomer_name = objODBCDatareader["customer_name"].ToString();
                                lsloanfacility_amount = objODBCDatareader["loanfacility_amount"].ToString();
                                lsccgroup_name = objODBCDatareader["ccgroup_name"].ToString();
                                lsccmeeting_date = objODBCDatareader["ccmeeting_date"].ToString();

                            }
                            objODBCDatareader.Close();

                            msSQL = "select concat(user_firstname, ' ', user_lastname, '/', user_code) as requested_by from adm_mst_tuser where user_gid='" + user_gid + "'";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                lsrequested_by = objODBCDatareader["requested_by"].ToString();
                            }
                            objODBCDatareader.Close();

                            sub = " CC Approval Required ";
                            body = "Dear Sir/Madam,  <br />";
                            body = body + "<br />";
                            body = body + "Greetings,  <br />";
                            body = body + "<br />";
                            body = body + lsrequested_by + " has been Initiated the Approval Request and the details are as follows,<br />";
                            body = body + "<br />";
                            body = body + "<b>Application Number :</b> " + lsapplication_no + "<br />";
                            body = body + "<br />";
                            body = body + "<b>Customer Name :</b> " + HttpUtility.HtmlEncode(lscustomer_name )+ "<br />";
                            body = body + "<br />";
                            body = body + "<b>Facility Loan Amount :</b> " + HttpUtility.HtmlEncode(lsloanfacility_amount )+ "<br />";
                            body = body + "<br />";
                            body = body + "<b>CC Meeting Scheduled On :</b> " + lsccmeeting_date + "<br />";
                            body = body + "<br />";
                            body = body + "<b>CC Group Name :</b> " + HttpUtility.HtmlEncode(lsccgroup_name )+ "<br />";
                            body = body + "<br />";
                            //message = message + "<b>CC Admin :</b> " + lsccadmin_name + "<br />";
                            //message = message + "<br />";
                            body = body + "Kindly <a href=" + ConfigurationManager.AppSettings["Agrccapprovalurl"].ToString() + "?id=" + sToken + "> Click Here</a> and do the needful.<br />";
                            body = body + "<br />";
                            body = body + "<b>Thanks & Regards, </b> ";
                            body = body + "<br />";
                            body = body + "<b> CC Admin </b> ";
                            body = body + "<br />";
                            //message = message + lsccadmin_name + "<br />";
                            //message = message + "<br />";

                            //MailFlag = objcmnfunctions.SendSMTP2(ls_username, ls_password, lsto_mail, "CC Meeting Mail Approval", message, lscc_mail, strBCC);


                            string[] ccmail_Array = cc_mailid.Split(',');
                            string[] ccmail1_Array = cc_mailid1.Split(',');
                            string[] ccmail2_Array = cc_mailid2.Split(',');
                            ccmail1_Array = ccmail_Array.Union(ccmail1_Array).ToArray();
                            ccmail_Array = ccmail1_Array.Union(ccmail2_Array).ToArray();
                            cc_mailid = string.Join(",", ccmail_Array);

                            MailMessage message = new MailMessage();
                            SmtpClient smtp = new SmtpClient();
                            message.From = new MailAddress(ls_username);
                            message.To.Add(new MailAddress(lsto_mail));

                            lsBccmail_id = ConfigurationManager.AppSettings["SamagroCCMeetingbcc"].ToString();

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
                            //    lsCCReceipients = ccmail_Array;
                            //    if (cc_mailid.Length == 0)
                            //    {
                            //        message.CC.Add(new MailAddress(cc_mailid));
                            //    }
                            //    else
                            //    {
                            //        foreach (string CCEmail in ccmail_Array)
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

                            if (values.status == true)
                            {
                                msSQL = "Insert into agr_mst_tccmailcount( " +
                                " application_gid," +
                                " from_mail," +
                                " to_mail," +
                                //" cc_mail," +
                                " mail_status," +
                                " mail_senddate, " +
                                " created_by," +
                                " created_date)" +
                                " values(" +
                                "'" + values.application_gid + "'," +
                                "'" + ls_username + "'," +
                                "'" + lsto_mail + "'," +
                                //"'" + lscc_mail + "'," +
                                "'CC Meeting Mail Approval'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                "'" + user_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }

                        }

                        values.status = true;
                        values.message = "MOM Submitted Successfully";
                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error Occured While Submitting MOM";
                    }
                    dt_datatable.Dispose();
                }

            }
        }
        //Undo Attendance
        public void DaPostUndoCCAttendance(string employee_gid, MdlCCAttendance values)
        {
            msSQL = " update agr_mst_tccmeeting2members set " +
                    " attendance_status= null," +
                    " ccapproval_flag = 'N' ," +
                    " updated_by='" + employee_gid + "'," +
                    " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    " where ccmeeting2members_gid='" + values.ccmeeting2members_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Attendance Status Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured While Updating Attendance";
            }
        }

        public void DaPostUndoOthersAttendance(string employee_gid, MdlCCAttendance values)
        {
            msSQL = " update agr_mst_tccmeeting2othermembers set " +
                    " attendance_status= null," +
                    " updated_by='" + employee_gid + "'," +
                    " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    " where ccmeeting2othermembers_gid='" + values.ccmeeting2othermembers_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Attendance Status Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured While Updating Attendance";
            }
        }


        //Get MOM Approval Flag
        public void DaGetMOMApprovalFlag(string employee_gid, string application_gid, MdlMstCC values)
        {
            //msSQL = "select momdocumentupload_flag from agr_mst_tapplication where application_gid='" + application_gid + "'";
            //string lsmomdocumentupload_flag = objdbconn.GetExecuteScalar(msSQL);
            //if (lsmomdocumentupload_flag == "N")
            //{
            //    values.proceed_flag = "N";
            //}
            //else
            //{
            msSQL = " select application_gid, attendance_status, ccmember_name from agr_mst_tccmeeting2members  " +
           " where application_gid='" + application_gid + "' and attendance_status is null union select application_gid, attendance_status, '' from agr_mst_tccmeeting2othermembers " +
            " where application_gid='" + application_gid + "' and attendance_status is null";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objODBCDatareader.Close();
                values.proceed_flag = "N";
            }
            else
            {
                //objODBCDatareader.Close();
                //msSQL = " select application_gid from agr_mst_tccmeeting2othermembers where application_gid='" + application_gid + "' and attendance_status is null";
                //objODBCDatareader = objdbconn.GetDataReader(msSQL);
                //if (objODBCDatareader.HasRows == true)
                //{
                //    objODBCDatareader.Close();
                //    values.proceed_flag = "N";
                //}
                //else
                //{
                objODBCDatareader.Close();
                values.proceed_flag = "Y";
                //}
            }
            //}
            values.status = true;
        }
        public void DaPostCCApprove(string employee_gid, MdlMstCC values)
        {
            // CC member condition
            msSQL = "select ccmember_gid from agr_mst_tccmeeting2members where ccmember_gid='" + employee_gid + "' and application_gid='" + values.application_gid + "'";
            string lsccmember_gid = objdbconn.GetExecuteScalar(msSQL);
            if (lsccmember_gid == null || lsccmember_gid == "")
            {
            }
            else
            {
                msSQL = " update agr_mst_tccmeeting2members set approval_status='" + values.approval_status + "',";
                if (values.remarks == "" || values.remarks == null)
                {
                    msSQL += " approval_remarks='',";
                }
                else
                {
                    msSQL += " approval_remarks='" + values.remarks.Replace("'", "") + "',";
                }
                msSQL += " updated_by='" + employee_gid + "'," +
                        " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                           " approved_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                        " where ccmember_gid='" + employee_gid + "'  and application_gid='" + values.application_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            if (values.approval_status == "Rejected")
            {//If Rejects, status updation in applictaion table
                msSQL = " update agr_mst_tapplication set approval_status='CC Rejected',";
                if (values.remarks == "" || values.remarks == null)
                {
                    msSQL += " cc_remarks='',";
                }
                else
                {
                    msSQL += " cc_remarks='" + values.remarks.Replace("'", "") + "',";
                }
                msSQL += " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                         " cccompleted_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                         " cccompleted_flag='Y'" +
                         " where  application_gid='" + values.application_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    msSQL = "update agr_mst_tccmeeting2members set  approval_status='-' " +
                  " where application_gid='" + values.application_gid + "' and approval_status = 'Pending'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                else
                {

                }
            }
            else
            {//Approval Condition checking for all CC members 
                msSQL = " select a.application_gid from agr_mst_tccmeeting2members a " +
                         " left join agr_mst_tapplication f on a.application_gid = f.application_gid " +
                            " where a.application_gid='" + values.application_gid + "' " +
                            " and a.attendance_status='P' and a.ccapproval_flag = 'Y' and  (  a.approval_status='Pending'  or  a.approval_status='Rejected') and (f.approval_status ='CC Rejected' or f.approval_status ='Submitted to CC' ) ";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == false)
                {
                    msSQL = " update agr_mst_tccmeeting2members set ccapproval_flag='N' " +
                           " where application_gid='" + values.application_gid + "' and  ccapproval_flag='Y' and ccmail_flag = 'N' and approval_status ='-' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update agr_mst_tapplication set approval_status='CC Approved', process_type = null,";
                    if (values.remarks == "" || values.remarks == null)
                    {
                        msSQL += " cc_remarks='',";
                    }
                    else
                    {
                        msSQL += " cc_remarks='" + values.remarks.Replace("'", "") + "',";
                    }
                    msSQL += " updated_by='" + employee_gid + "'," +
                             " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                             " cccompleted_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                             " cccompleted_flag='Y'" +
                             " where  application_gid='" + values.application_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                }
                objODBCDatareader.Close();
            }

            //msSQL = " select application_gid from agr_mst_tccmeeting2members a  " +
            //        " left join agr_mst_tapplication f on a.application_gid = f.application_gid " +
            //        " where a.application_gid='" + values.application_gid + "' and a.attendance_status = 'P'" +
            //        " and a.ccapproval_flag = 'Y' and(a.approval_status = 'Pending')";
            //objODBCDatareader = objdbconn.GetDataReader(msSQL);
            //if (objODBCDatareader.HasRows == false)
            //{
            //    msSQL = " update agr_mst_tapplication set cccompleted_flag='Y'," +
            //            " updated_by='" + employee_gid + "'," +
            //            " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
            //            " cccompleted_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
            //            " where  application_gid='" + values.application_gid + "'";
            //    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            //}
            //objODBCDatareader.Close();

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Status Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured While Updating status";
            }
        }

        public void DaViewCCRemarks(string employee_gid, string ccmeeting2members_gid, string application_gid, MdlMstCC values)
        {
            msSQL = "select approval_remarks from agr_mst_tccmeeting2members where ccmeeting2members_gid='" + ccmeeting2members_gid + "'";
            values.remarks = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select approval_remarks from agr_mst_tccmeeting2memberslog where ccmeeting2members_gid='" + ccmeeting2members_gid + "'";
            values.cc_remarkslog = objdbconn.GetExecuteScalar(msSQL);
            values.status = true;
        }
        public void DaViewOtherRemarks(string employee_gid, string ccmeeting2othermembers_gid, string application_gid, MdlMstCC values)
        {
            msSQL = "select approval_remarks from agr_mst_tccmeeting2othermembers where ccmeeting2othermembers_gid='" + ccmeeting2othermembers_gid + "'";
            values.remarks = objdbconn.GetExecuteScalar(msSQL);
            values.status = true;
        }
        public void DaGetCCSummary(string employee_gid, MdlMstApplicationAdd values)
        {
            msSQL = " select a.application_gid,application_no,customerref_name,customer_urn,vertical_name, " +
                      " a.customer_name as customer_name,concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as submitted_by,a.approval_status,applicant_type," +
                      " date_format(a.submitted_date, '%d-%m-%Y %h:%i %p') as submitted_date,date_format(a.ccsubmitted_date, '%d-%m-%Y %h:%i %p') as ccsubmitted_date,  " +
                      " region,concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as ccsubmitted_by,a.ccgroup_name, " +
                      " date_format(f.ccmeeting_date, '%d-%m-%Y') as ccmeeting_date,date_format(f.created_date, '%d-%m-%Y %h:%i %p') as scheduled_date, " +
                      " concat(time_format(start_time, '%h:%i %p'), '-', time_format(end_time, '%h:%i %p')) as ccmeting_time," +
                      " date_format(now(),'%d-%m-%Y') as now_date,a.overalllimit_amount,concat(j.user_firstname, ' ', j.user_lastname, ' / ', j.user_code) as updated_by," +
                      " a.renewal_flag,a.amendment_flag,a.shortclosing_flag from agr_mst_tapplication a" +
                      " left join hrm_mst_temployee b on b.employee_gid = a.submitted_by" +
                      " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                      " left join hrm_mst_temployee d on a.ccsubmitted_by = d.employee_gid" +
                      " left join adm_mst_tuser e on e.user_gid = d.user_gid" +
                      " left join agr_mst_tccschedulemeeting f on a.application_gid = f.application_gid" +
                      " left join agr_mst_tccmeeting2members g on f.application_gid=g.application_gid" +
                      " left join agr_mst_tccmeeting2othermembers h on f.application_gid = h.application_gid" +
                      " left join hrm_mst_temployee i on f.updated_by = i.employee_gid " +
                      " left join adm_mst_tuser j on j.user_gid = i.user_gid " +
                    " where   ccsubmit_flag = 'Y' and meeting_status = 'Scheduled' and" +
                      " ((( f.ccadmin_gid like '%" + employee_gid + "%') and momapproval_flag = 'N')  or f.created_by='" + employee_gid + "' or g.ccmember_gid='" + employee_gid + "' or h.employee_gid='" + employee_gid + "')" +
                      " and a.momapproval_flag='N' group by a.application_gid order by " +
                      " STR_TO_DATE(ccmeeting_date, '%Y-%m-%d') asc; ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<applicationadd_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getapplicationadd_list.Add(new applicationadd_list
                    {
                        application_gid = dt["application_gid"].ToString(),
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical_name = dt["vertical_name"].ToString(),
                        application_status = dt["approval_status"].ToString(),
                        applicant_type = dt["applicant_type"].ToString(),
                        submitted_date = dt["submitted_date"].ToString(),
                        ccsubmitted_date = dt["ccsubmitted_date"].ToString(),
                        submitted_by = dt["submitted_by"].ToString(),
                        ccsubmitted_by = dt["ccsubmitted_by"].ToString(),
                        region = dt["region"].ToString(),
                        ccgroup_name = dt["ccgroup_name"].ToString(),
                        ccmeeting_date = dt["ccmeeting_date"].ToString(),
                        ccmeting_time = dt["ccmeting_time"].ToString(),
                        scheduled_date = dt["scheduled_date"].ToString(),
                        now_date = dt["now_date"].ToString(),
                        overalllimit_amount = dt["overalllimit_amount"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        updated_by = dt["updated_by"].ToString(),
                        renewal_flag = dt["renewal_flag"].ToString(),
                        amendment_flag = dt["amendment_flag"].ToString(),
                        shortclosing_flag = dt["shortclosing_flag"].ToString()
                    });
                }
            }
            values.applicationadd_list = getapplicationadd_list;
            dt_datatable.Dispose();
        }
        // CC Completed Summary
        public void DaGetCCCompletedSummary(string employee_gid, MdlMstApplicationAdd values)
        {
            msSQL = " select a.application_gid,application_no,customerref_name,customer_urn,vertical_name, " +
                    " a.customer_name as customer_name,concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as submitted_by,a.approval_status,applicant_type," +
                    " date_format(a.submitted_date, '%d-%m-%Y %h:%i %p') as submitted_date,date_format(a.ccsubmitted_date, '%d-%m-%Y %h:%i %p') as ccsubmitted_date,  " +
                    " region,concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as ccsubmitted_by,a.ccgroup_name, " +
                    " date_format(f.ccmeeting_date, '%d-%m-%Y') as ccmeeting_date,date_format(f.created_date, '%d-%m-%Y %h:%i %p') as scheduled_date, " +
                    " concat(time_format(start_time, '%h:%i %p'), '-', time_format(end_time, '%h:%i %p')) as ccmeting_time," +
                    " date_format(now(),'%d-%m-%Y') as now_date,a.overalllimit_amount,date_format(cccompleted_date,'%d-%m-%Y') as cccompleted_date, " +
                    " a.renewal_flag,a.amendment_flag,a.shortclosing_flag from agr_mst_tapplication a" +
                    " left join hrm_mst_temployee b on b.employee_gid = a.submitted_by" +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " left join hrm_mst_temployee d on a.ccsubmitted_by = d.employee_gid" +
                    " left join adm_mst_tuser e on e.user_gid = d.user_gid" +
                    " left join agr_mst_tccschedulemeeting f on a.application_gid = f.application_gid" +
                    " left join agr_mst_tccmeeting2members g on f.application_gid=g.application_gid" +
                    " left join agr_mst_tccmeeting2othermembers h on f.application_gid = h.application_gid" +
                    " where ccsubmit_flag = 'Y' and meeting_status = 'Scheduled' and" +
                    " (f.created_by='" + employee_gid + "' or g.ccmember_gid='" + employee_gid + "' or h.employee_gid='" + employee_gid + "' " +
                    " or f.ccadmin_gid like '%" + employee_gid + "%') " +
                    " and a.approval_status='CC Approved' or a.approval_status='CC Rejected' and a.momapproval_flag='Y' and a.cccompleted_flag='Y' group by a.application_gid order by " +
                    " STR_TO_DATE(ccmeeting_date, '%Y-%m-%d') asc; ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<applicationadd_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getapplicationadd_list.Add(new applicationadd_list
                    {
                        application_gid = dt["application_gid"].ToString(),
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical_name = dt["vertical_name"].ToString(),
                        application_status = dt["approval_status"].ToString(),
                        applicant_type = dt["applicant_type"].ToString(),
                        submitted_date = dt["submitted_date"].ToString(),
                        ccsubmitted_date = dt["ccsubmitted_date"].ToString(),
                        submitted_by = dt["submitted_by"].ToString(),
                        ccsubmitted_by = dt["ccsubmitted_by"].ToString(),
                        region = dt["region"].ToString(),
                        ccgroup_name = dt["ccgroup_name"].ToString(),
                        ccmeeting_date = dt["ccmeeting_date"].ToString(),
                        ccmeting_time = dt["ccmeting_time"].ToString(),
                        scheduled_date = dt["scheduled_date"].ToString(),
                        now_date = dt["now_date"].ToString(),
                        overalllimit_amount = dt["overalllimit_amount"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        cccompleted_date = dt["cccompleted_date"].ToString(),
                        renewal_flag = dt["renewal_flag"].ToString(),
                        amendment_flag = dt["amendment_flag"].ToString(),
                        shortclosing_flag = dt["shortclosing_flag"].ToString()
                    });
                }
            }
            values.applicationadd_list = getapplicationadd_list;
            dt_datatable.Dispose();
        }
        public void DaGetApprovalFlag(string employee_gid, string application_gid, MdlMstCC values)
        {
            msSQL = "select ccmember_gid from agr_mst_tccmeeting2members where application_gid='" + application_gid + "' and ccmember_gid='" + employee_gid + "'" +
                " and (approval_status='Approved' or approval_status='Rejected' or attendance_status='A')";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objODBCDatareader.Close();
                values.approval_flag = "Y";
            }
            else
            {
                objODBCDatareader.Close();
                msSQL = "select ccmember_gid from agr_mst_tccmeeting2members where application_gid='" + application_gid + "' and ccmember_gid='" + employee_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == false)
                {
                    objODBCDatareader.Close();
                    values.approval_flag = "Y";
                }
                else
                {
                    objODBCDatareader.Close();
                    values.approval_flag = "N";
                }


            }
            values.status = true;
        }
        public void DaMOM_delete(string application2momdoc_gid, string application_gid, MdlMstCC values)
        {
            msSQL = "delete from agr_mst_tapplication2momdoc where application2momdoc_gid='" + application2momdoc_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = " select application2momdoc_gid,application_gid,document_name,document_path,document_title from " +
                           " agr_mst_tapplication2momdoc where application_gid='" + application_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmomdocument_list = new List<momdocument_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getmomdocument_list.Add(new momdocument_list
                        {
                            document_name = dt["document_name"].ToString(),
                            document_title = dt["document_title"].ToString(),
                            document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                            application_gid = dt["application_gid"].ToString(),
                            application2momdoc_gid = dt["application2momdoc_gid"].ToString(),

                        });
                        values.momdocument_list = getmomdocument_list;
                    }
                }
                dt_datatable.Dispose();
                values.message = "MOM Document deleted successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occrued while deleting document";
                values.status = false;
            }
        }

        public void DaPostRevertCCtoCredit(string employee_gid, MdlCCRevert values)
        {
            msGetGid2 = objcmnfunctions.GetMasterGID("CCRL");
            msSQL = " insert into agr_trn_tccmeetingtocreditlog(" +
                    " cctocreditlog_gid," +
                    " application_gid," +
                    " reason," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid2 + "'," +
                    "'" + values.application_gid + "'," +
                    "'" + values.cctocredit_reason.Replace("'", "") + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " select application_gid, appcreditapproval_gid, approval_gid, approval_name, approval_type, " +
                    " approval_status, approval_remarks, approved_date, rejected_date, hold_date, approval_token, " +
                    " hierary_level, created_by, created_date, initiate_flag from agr_trn_tappcreditapproval " +
                    " where application_gid ='" + values.application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msGetGid1 = objcmnfunctions.GetMasterGID("CAPL");
                    msSQL = " insert into agr_trn_tappcreditapprovallog(" +
                            " appcreditapprovallog_gid," +
                            " appcreditapproval_gid," +
                            " application_gid," +
                            " approval_gid," +
                            " approval_name," +
                            " approval_type," +
                            " approval_status," +
                            " approval_remarks," +
                            " approved_date," +
                            " rejected_date," +
                            " hold_date," +
                            " approval_token," +
                            " hierary_level," +
                            " approval_initiateby," +
                            " approval_initiatedate," +
                            " initiate_flag," +
                            " created_by," +
                            " created_date)" +
                            " values(" +
                            "'" + msGetGid1 + "'," +
                            "'" + dt["appcreditapproval_gid"].ToString() + "'," +
                            "'" + values.application_gid + "'," +
                            "'" + dt["approval_gid"].ToString() + "'," +
                            "'" + dt["approval_name"].ToString() + "'," +
                            "'" + dt["approval_type"].ToString() + "'," +
                            "'" + dt["approval_status"].ToString() + "'," +
                            "'" + dt["approval_remarks"].ToString() + "',";
                    if (dt["approved_date"].ToString() == "" || dt["approved_date"].ToString() == null)
                    {
                        msSQL += "null,";
                    }
                    else
                    {
                        msSQL += "'" + Convert.ToDateTime(dt["approved_date"]).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                    }
                    if (dt["rejected_date"].ToString() == "" || dt["rejected_date"].ToString() == null)
                    {
                        msSQL += "null,";
                    }
                    else
                    {
                        msSQL += "'" + Convert.ToDateTime(dt["rejected_date"]).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                    }
                    if (dt["hold_date"].ToString() == "" || dt["hold_date"].ToString() == null)
                    {
                        msSQL += "null,";
                    }
                    else
                    {
                        msSQL += "'" + Convert.ToDateTime(dt["hold_date"]).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                    }

                    msSQL += "'" + dt["approval_token"].ToString() + "'," +
                            "'" + dt["hierary_level"].ToString() + "'," +
                            "'" + dt["created_by"].ToString() + "'," +
                            "'" + Convert.ToDateTime(dt["created_date"]).ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                            "'" + dt["initiate_flag"].ToString() + "'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            dt_datatable.Dispose();

            msSQL = " update agr_trn_tappcreditapproval set " +
                    " approval_status='Pending'," +
                    " approval_remarks=null," +
                    " approved_date=null" +
                    " where application_gid='" + values.application_gid + "' and hierary_level='0' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " delete from agr_trn_tappcreditapproval " +
                    " where application_gid='" + values.application_gid + "' and hierary_level<>'0' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msSQL = " UPDATE agr_mst_tapplication SET approval_status = 'Sent Back to Credit', " +
                        " ccsubmit_flag = 'N', creditheadapproval_status = 'Pending', " +
                        " cctocredit_reason = '" + values.cctocredit_reason.Replace("'", "") + "' " +
                        " WHERE application_gid = '" + values.application_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                //mail trigger starts
                try
                {
                    msSQL = "select relationshipmanager_name,relationshipmanager_gid,creditassigned_date,application_no,clustermanager_gid,creditregionalmanager_gid,creditnationalmanager_gid,customerref_name from agr_mst_tapplication where application_gid = '" + values.application_gid + "'";
                    objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader1.HasRows == true)
                    {
                        customer_name = objODBCDatareader1["customerref_name"].ToString();
                        rm_gid = objODBCDatareader1["relationshipmanager_gid"].ToString();
                        arn_no = objODBCDatareader1["application_no"].ToString();
                        ch_gid = objODBCDatareader1["clustermanager_gid"].ToString();
                        rcm_gid = objODBCDatareader1["creditregionalmanager_gid"].ToString();
                        ncm_gid = objODBCDatareader1["creditnationalmanager_gid"].ToString();
                        relationshipmanager_name = objODBCDatareader1["relationshipmanager_name"].ToString();
                        creditassigned_date = objODBCDatareader1["creditassigned_date"].ToString();
                    }

                    lssource = ConfigurationManager.AppSettings["img_path"];
                    objODBCDatareader1.Close();
                    msSQL = " select employee_emailid from hrm_mst_temployee where employee_gid = '" + ch_gid + "'";
                    ch_mailid = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = " select employee_emailid from hrm_mst_temployee where employee_gid = '" + rcm_gid + "'";
                    rcm_mailid = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = " select employee_emailid from hrm_mst_temployee where employee_gid = '" + rm_gid + "'";
                    rm_mailid = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = " select employee_emailid from hrm_mst_temployee where employee_gid = '" + ncm_gid + "'";
                    ncm_mailid = objdbconn.GetExecuteScalar(msSQL);
                    cc_mailid = ch_mailid + "," + rcm_mailid + "," + ncm_mailid + "," + rm_mailid;
                    cc_mailid = cc_mailid.Replace(",,", ",");
                    string[] cc_mail_id = cc_mailid.Split(',');
                    string[] cc_mail_id_send = cc_mail_id.Distinct().ToArray();
                    cc_mailid = cc_mail_id_send[0];
                    for (int i = 1; i < cc_mail_id_send.Length; i++)
                    {
                        cc_mailid = cc_mailid + "," + cc_mail_id_send[i];
                    }
                    msSQL = "select concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as allocated_by from agr_mst_tapplication a left join hrm_mst_temployee b" +
                        " on b.employee_gid = a.creditassigned_by left join adm_mst_tuser c on c.user_gid = b.user_gid where a.application_gid='" + values.application_gid + "'";
                    allocated_by = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = "select approval_gid from agr_trn_tappcreditapproval where hierary_level='0' and application_gid = '" + values.application_gid + "'";
                    tomail_id = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = " select employee_emailid from hrm_mst_temployee where employee_gid = '" + tomail_id + "'";
                    tomail_id = objdbconn.GetExecuteScalar(msSQL);

                    sub = " Application: " + arn_no + " has been SendBack to Underwriting";
                    body = "<style>table, th, td {border: 1px solid black;border-collapse: collapse;}</style>";
                    body = body + "<table style='border-right: 1px solid black;border-top: 1px solid black;border-bottom: 1px solid black;'><tr><td style='border-right-color:white;align:center;'>";
                    body = body + "<br />";
                    //body = body + "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp <img style='height:150px; width:380px;' src='" + lssource + "'><br />";
                    //body = body + "<br />";
                    body = body + " &nbsp &nbsp Dear Sir / Madam, <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp Greetings! <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp The below application has been SendBack to Underwriting to you. <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp <b> Application Number : </b> " + arn_no + "  <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp <b> Customer Name : </b> " + HttpUtility.HtmlEncode(customer_name) + "  <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp <b> RM Name : </b> " + HttpUtility.HtmlEncode(relationshipmanager_name)+ "  <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp <b> Allocated By :</b>  " + HttpUtility.HtmlEncode(allocated_by)+ "  <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp <b> Allocation Time :</b>  " + creditassigned_date + "  <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp Log into one.samunnati.com and complete the necessary actions.";
                    //body = body + "<br />";
                    //body = body + "&nbsp &nbsp Regards";
                    //body = body + "<br />";
                    //body = body + "&nbsp &nbsp Sam-Custopedia <br /> ";
                    //body = body + "<br />";
                    //body = body + "&nbsp &nbsp<hr>&nbsp&nbsp";
                    //body = body + "&nbsp &nbsp Reach out to us at samcustopedia@samunnati.com <br /> ";
                    body = body + "<br />";
                    body = body + "<br />";
                    body = body + "</td><td style='margin-left:20px; border-left-color:white;'>&nbsp&nbsp</td></tr></table>";

                    //sendback mail trigger event

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
                    lsBccmail_id = ConfigurationManager.AppSettings["SamagroSentBackBccMail"].ToString();
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
                    values.status = false;

                }
                values.status = true;
                values.message = "Application Reverted From CC to Credit";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured While Revert an Application From CC to Credit";
            }
        }

        //CC Meeting Pending Summary
        public void DaGetCCtoCreditSummary(string employee_gid, MdlMstApplicationAdd values)
        {
            msSQL = " select application_gid,application_no,customerref_name,customer_urn,vertical_name," +
                    " a.customer_name as customer_name,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as submitted_by,a.approval_status,applicant_type," +
                    " date_format(a.submitted_date,'%d-%m-%Y %h:%i %p') as submitted_date,date_format(a.ccsubmitted_date,'%d-%m-%Y %h:%i %p') as ccsubmitted_date," +
                    " region,concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as ccsubmitted_by,ccgroup_name,overalllimit_amount," +
                    " a.renewal_flag,a.amendment_flag,a.shortclosing_flag from agr_mst_tapplication a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.submitted_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                    " left join hrm_mst_temployee d on a.ccsubmitted_by = d.employee_gid" +
                    " left join adm_mst_tuser e on e.user_gid = d.user_gid where ccsubmit_flag='N' and meeting_status='Pending' " +
                    " and approval_status = 'Sent Back to Credit' order by ccsubmitted_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<applicationadd_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getapplicationadd_list.Add(new applicationadd_list
                    {
                        application_gid = dt["application_gid"].ToString(),
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical_name = dt["vertical_name"].ToString(),
                        application_status = dt["approval_status"].ToString(),
                        applicant_type = dt["applicant_type"].ToString(),
                        submitted_date = dt["submitted_date"].ToString(),
                        ccsubmitted_date = dt["ccsubmitted_date"].ToString(),
                        submitted_by = dt["submitted_by"].ToString(),
                        ccsubmitted_by = dt["ccsubmitted_by"].ToString(),
                        region = dt["region"].ToString(),
                        ccgroup_name = dt["ccgroup_name"].ToString(),
                        overalllimit_amount = dt["overalllimit_amount"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        renewal_flag = dt["renewal_flag"].ToString(),
                        amendment_flag = dt["amendment_flag"].ToString(),
                        shortclosing_flag = dt["shortclosing_flag"].ToString()
                    });
                }
            }
            values.applicationadd_list = getapplicationadd_list;
            dt_datatable.Dispose();
        }
        public void DaGetAppRevertReasonRemarks(MdlappCreditassign values, string application_gid)
        {
            try
            {
                msSQL = " select cctocredit_reason " +
                        " from agr_mst_tapplication  where application_gid = '" + application_gid + "'";
                values.cctocredit_reason = objdbconn.GetExecuteScalar(msSQL);


                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        //Get Schedule Meeting log Information
        public void DaGetScheduleMeetingBasicLog(string application_gid, string employee_gid, MdlMstCCschedule values)
        {
            try
            {
                msSQL = " select a.ccschedulemeetinglog_gid,a.ccmeeting_no,a.ccmeeting_title,a.start_time,a.end_time,a.ccmeeting_mode,a.ccgroup_name,a.ccgroupname_gid," +
                     " a.description,date_format(a.ccmeeting_date,'%d-%m-%Y') as ccmeeting_date,ccmeeting_date as meeting_date,otheruser_name," +
                     " otheruser_gid,non_users,ccadmin_gid,ccadmin_name," +
                     " concat(time_format(start_time, '%h:%i %p'), '-', time_format(end_time, '%h:%i %p')) as ccmeeting_time from agr_mst_tccschedulemeetinglog a" +
                     " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                     " left join adm_mst_tuser c on c.user_gid = b.user_gid where application_gid='" + application_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.ccschedulemeeting_gid = objODBCDatareader["ccschedulemeetinglog_gid"].ToString();
                    values.ccmeeting_no = objODBCDatareader["ccmeeting_no"].ToString();
                    values.ccmeeting_title = objODBCDatareader["ccmeeting_title"].ToString();
                    values.ccmeeting_time = objODBCDatareader["ccmeeting_time"].ToString();
                    values.end_time = objODBCDatareader["end_time"].ToString();
                    values.start_time = objODBCDatareader["start_time"].ToString();
                    values.ccmeeting_mode = objODBCDatareader["ccmeeting_mode"].ToString();
                    values.ccgroup_name = objODBCDatareader["ccgroup_name"].ToString();
                    values.ccgroupname_gid = objODBCDatareader["ccgroupname_gid"].ToString();
                    values.description = objODBCDatareader["description"].ToString();
                    values.ccmeeting_date = objODBCDatareader["ccmeeting_date"].ToString();
                    values.otheruser_name = objODBCDatareader["otheruser_name"].ToString();
                    values.non_users = objODBCDatareader["non_users"].ToString();
                    values.ccadmin_name = objODBCDatareader["ccadmin_name"].ToString();
                    if (objODBCDatareader["meeting_date"].ToString() == "")
                    {
                    }
                    else
                    {
                        values.meeting_date = Convert.ToDateTime(objODBCDatareader["meeting_date"]).ToString("MM-dd-yyyy");
                    }
                    if (objODBCDatareader["start_time"].ToString() == "" || objODBCDatareader["start_time"].ToString() == null)
                    {
                    }
                    else
                    {
                        values.Tstart_time = Convert.ToDateTime(objODBCDatareader["start_time"].ToString());
                    }
                    if (objODBCDatareader["end_time"].ToString() == "" || objODBCDatareader["end_time"].ToString() == null)
                    {
                    }
                    else
                    {
                        values.Tend_time = Convert.ToDateTime(objODBCDatareader["end_time"].ToString());
                    }

                    //CC Group Name
                    String[] ccgroupGID_list = objODBCDatareader["ccgroupname_gid"].ToString().Split(',');
                    String[] ccgroupname_list = objODBCDatareader["ccgroup_name"].ToString().Split(',');


                    var getccschedule_list = new List<ccschedule_list>();
                    for (var i = 0; i < ccgroupGID_list.Length; i++)
                    {
                        getccschedule_list.Add(new ccschedule_list
                        {
                            ccgroupname_gid = ccgroupGID_list[i],
                            ccgroup_name = ccgroupname_list[i],
                        });
                    }
                    values.ccschedule_list = getccschedule_list;
                    //Other Users List
                    if (objODBCDatareader["otheruser_gid"].ToString() == "" || objODBCDatareader["otheruser_gid"].ToString() == null)
                    {

                    }
                    else
                    {
                        String[] OtheruserGID_list = objODBCDatareader["otheruser_gid"].ToString().Split(',');
                        String[] Otherusername_list = objODBCDatareader["otheruser_name"].ToString().Split(',');

                        var getotheremployee_list = new List<otheremployee_list>();
                        for (var i = 0; i < OtheruserGID_list.Length; i++)
                        {
                            getotheremployee_list.Add(new otheremployee_list
                            {
                                employee_gid = OtheruserGID_list[i],
                                employee_name = Otherusername_list[i],
                            });
                        }
                        values.otheremployee_list = getotheremployee_list;
                    }
                    //CC Admin List
                    if (objODBCDatareader["ccadmin_gid"].ToString() == "" || objODBCDatareader["ccadmin_gid"].ToString() == null)
                    {

                    }
                    else
                    {


                        String[] CCAdminGID_list = objODBCDatareader["ccadmin_gid"].ToString().Split(',');
                        String[] CCAdminName_list = objODBCDatareader["ccadmin_name"].ToString().Split(',');

                        var getccadmin_list = new List<ccadmin_list>();
                        for (var i = 0; i < CCAdminGID_list.Length; i++)
                        {
                            getccadmin_list.Add(new ccadmin_list
                            {
                                employee_gid = CCAdminGID_list[i],
                                employee_name = CCAdminName_list[i],
                            });
                        }
                        values.ccadmin_list = getccadmin_list;
                    }
                }
                values.status = true;
                objODBCDatareader.Close();

            }
            catch
            {
                values.status = false;
            }
        }

        // CC Approval Pending Summary
        public void DaGetCCApprovalPendingSummary(string employee_gid, MdlMstApplicationAdd values)
        {
            msSQL = " select a.application_gid,application_no,customerref_name,customer_urn,vertical_name, " +
                      " a.customer_name as customer_name,concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as submitted_by,a.approval_status,applicant_type," +
                      " date_format(a.submitted_date, '%d-%m-%Y %h:%i %p') as submitted_date,date_format(a.ccsubmitted_date, '%d-%m-%Y %h:%i %p') as ccsubmitted_date,  " +
                      " region,concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as ccsubmitted_by,a.ccgroup_name, " +
                      " date_format(f.ccmeeting_date, '%d-%m-%Y') as ccmeeting_date,date_format(f.created_date, '%d-%m-%Y %h:%i %p') as scheduled_date, " +
                      " concat(time_format(start_time, '%h:%i %p'), '-', time_format(end_time, '%h:%i %p')) as ccmeting_time," +
                      " date_format(now(),'%d-%m-%Y') as now_date,a.overalllimit_amount,concat(j.user_firstname, ' ', j.user_lastname, ' / ', j.user_code) as updated_by, " +
                      " concat(l.user_firstname, ' ', l.user_lastname, ' / ', l.user_code) as momupdated_by, momupdated_date,a.renewal_flag,a.amendment_flag,a.shortclosing_flag from agr_mst_tapplication a" +
                      " left join hrm_mst_temployee b on b.employee_gid = a.submitted_by" +
                      " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                      " left join hrm_mst_temployee d on a.ccsubmitted_by = d.employee_gid" +
                      " left join adm_mst_tuser e on e.user_gid = d.user_gid" +
                      " left join agr_mst_tccschedulemeeting f on a.application_gid = f.application_gid" +
                      " left join agr_mst_tccmeeting2members g on f.application_gid=g.application_gid" +
                      " left join agr_mst_tccmeeting2othermembers h on f.application_gid = h.application_gid" +
                      " left join hrm_mst_temployee i on f.updated_by = i.employee_gid " +
                      " left join adm_mst_tuser j on j.user_gid = i.user_gid " +
                      " left join hrm_mst_temployee k on a.momupdated_by = k.employee_gid " +
                      " left join adm_mst_tuser l on l.user_gid = k.user_gid " +
                      " where ccsubmit_flag = 'Y' and meeting_status = 'Scheduled' and" +
                      " (f.created_by='" + employee_gid + "' or g.ccmember_gid='" + employee_gid + "' or h.employee_gid='" + employee_gid + "'" +
                      " or f.ccadmin_gid like '%" + employee_gid + "%') " +
                      " and a.momapproval_flag='Y' and a.cccompleted_flag='N' group by a.application_gid order by " +
                      " STR_TO_DATE(ccmeeting_date, '%Y-%m-%d') asc; ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<applicationadd_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getapplicationadd_list.Add(new applicationadd_list
                    {
                        application_gid = dt["application_gid"].ToString(),
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical_name = dt["vertical_name"].ToString(),
                        application_status = dt["approval_status"].ToString(),
                        applicant_type = dt["applicant_type"].ToString(),
                        submitted_date = dt["submitted_date"].ToString(),
                        ccsubmitted_date = dt["ccsubmitted_date"].ToString(),
                        submitted_by = dt["submitted_by"].ToString(),
                        ccsubmitted_by = dt["ccsubmitted_by"].ToString(),
                        region = dt["region"].ToString(),
                        ccgroup_name = dt["ccgroup_name"].ToString(),
                        ccmeeting_date = dt["ccmeeting_date"].ToString(),
                        ccmeting_time = dt["ccmeting_time"].ToString(),
                        scheduled_date = dt["scheduled_date"].ToString(),
                        now_date = dt["now_date"].ToString(),
                        overalllimit_amount = dt["overalllimit_amount"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        updated_by = dt["updated_by"].ToString(),
                        momupdated_by = dt["momupdated_by"].ToString(),
                        momupdated_date = dt["momupdated_date"].ToString(),
                        renewal_flag = dt["renewal_flag"].ToString(),
                        amendment_flag = dt["amendment_flag"].ToString(),
                        shortclosing_flag = dt["shortclosing_flag"].ToString()
                    });
                }
            }
            values.applicationadd_list = getapplicationadd_list;
            dt_datatable.Dispose();
        }

        // CC Schedule Meeting Calender View
        public void DaGetCCMeetingCalenderView(string employee_gid, MdlMstApplicationAdd values)
        {
            msSQL = " select a.application_gid,application_no,customerref_name,customer_urn,vertical_name, " +
                      " a.customer_name as customer_name,concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as submitted_by,a.approval_status,applicant_type," +
                      " date_format(a.submitted_date, '%d-%m-%Y %h:%i %p') as submitted_date,date_format(a.ccsubmitted_date, '%d-%m-%Y %h:%i %p') as ccsubmitted_date,  " +
                      " region,concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as ccsubmitted_by,a.ccgroup_name, " +
                      " date_format(f.ccmeeting_date, '%d-%m-%Y') as ccmeeting_date,date_format(f.created_date, '%d-%m-%Y %h:%i %p') as scheduled_date, " +
                      " concat(time_format(start_time, '%h:%i %p'), '-', time_format(end_time, '%h:%i %p')) as ccmeting_time," +
                      " date_format(now(),'%d-%m-%Y') as now_date,a.overalllimit_amount,concat(j.user_firstname, ' ', j.user_lastname, ' / ', j.user_code) as updated_by  from agr_mst_tapplication a" +
                      " left join hrm_mst_temployee b on b.employee_gid = a.submitted_by" +
                      " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                      " left join hrm_mst_temployee d on a.ccsubmitted_by = d.employee_gid" +
                      " left join adm_mst_tuser e on e.user_gid = d.user_gid" +
                      " left join agr_mst_tccschedulemeeting f on a.application_gid = f.application_gid" +
                      " left join agr_mst_tccmeeting2members g on f.application_gid=g.application_gid" +
                      " left join agr_mst_tccmeeting2othermembers h on f.application_gid = h.application_gid" +
                      " left join hrm_mst_temployee i on f.updated_by = i.employee_gid " +
                      " left join adm_mst_tuser j on j.user_gid = i.user_gid " +
                      " where ccsubmit_flag = 'Y' and meeting_status = 'Scheduled' and" +
                      " (f.created_by='" + employee_gid + "' or g.ccmember_gid='" + employee_gid + "' or h.employee_gid='" + employee_gid + "'" +
                      " or f.ccadmin_gid like '%" + employee_gid + "%') " +
                      " and (ccmeeting_date >= CURDATE() or ccmeeting_date is null) group by a.application_gid order by " +
                      " STR_TO_DATE(ccmeeting_date, '%Y-%m-%d') asc; ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<applicationadd_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getapplicationadd_list.Add(new applicationadd_list
                    {
                        application_gid = dt["application_gid"].ToString(),
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical_name = dt["vertical_name"].ToString(),
                        application_status = dt["approval_status"].ToString(),
                        applicant_type = dt["applicant_type"].ToString(),
                        submitted_date = dt["submitted_date"].ToString(),
                        ccsubmitted_date = dt["ccsubmitted_date"].ToString(),
                        submitted_by = dt["submitted_by"].ToString(),
                        ccsubmitted_by = dt["ccsubmitted_by"].ToString(),
                        region = dt["region"].ToString(),
                        ccgroup_name = dt["ccgroup_name"].ToString(),
                        ccmeeting_date = dt["ccmeeting_date"].ToString(),
                        ccmeting_time = dt["ccmeting_time"].ToString(),
                        scheduled_date = dt["scheduled_date"].ToString(),
                        now_date = dt["now_date"].ToString(),
                        overalllimit_amount = dt["overalllimit_amount"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        updated_by = dt["updated_by"].ToString(),
                    });
                }
            }
            values.applicationadd_list = getapplicationadd_list;
            dt_datatable.Dispose();
        }

        public void DaGetApprovalInitiate(string application_gid, string ccmeeting2members_gid, string employee_gid, MdlMstCCschedule values)
        {
            msSQL = " select a.ccmember_name, ccmember_gid from agr_mst_tccmeeting2members a" +
                  " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                  " left join adm_mst_tuser c on c.user_gid = b.user_gid where application_gid='" + application_gid + "' and  ccmeeting2members_gid = '" + ccmeeting2members_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.ccmember_name = objODBCDatareader["ccmember_name"].ToString();
                values.ccmember_gid = objODBCDatareader["ccmember_gid"].ToString();

            }
            objODBCDatareader.Close();

        }
        public void DaPostApprovalInitiate(string employee_gid, MdlMstCCschedule values, string user_gid)
        {
            msSQL = " update agr_mst_tccmeeting2members set ccapproval_flag='Y'," +
                    " approvalinitiate_by = '" + employee_gid + "'" +

                    " where application_gid='" + values.application_gid + "' and  ccmeeting2members_gid = '" + values.ccmeeting2members_gid + "' ";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Click Submit to Approval Button to Initiate Approval";
            }
            else
            {
                values.message = "Error Occured While Approval Initiate";
                values.status = false;
            }
        }

        // Service Request Delete
        public void DaCancelApprovalInitiate(MdlMstCCschedule values, string application_gid, string ccmeeting2members_gid, string user_gid)
        {

            msSQL = " update agr_mst_tccmeeting2members set ccapproval_flag='N' , ccmail_flag = 'N' ,approval_status ='-' where application_gid='" + application_gid + "' and  ccmeeting2members_gid = '" + ccmeeting2members_gid + "' ";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);



            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Approval Initiate Cancelled Successfully..!";
            }


            else
            {
                values.status = false;
                values.message = "Error Occured..!, While Cancel";
            }
        }

        public void DaGetApprovalList(string application_gid, string employee_gid, MdlMstCCschedule values)
        {

            msSQL = " select a.ccmember_name,a.ccmember_gid,a.ccgroup_name,a.ccmeeting2members_gid,a.ccapproval_flag,concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as approvalinitiate_by, " +
                    " date_format(a.approved_date,'%d-%m-%Y %h:%i %p') as approved_date, " +
                        " a.attendance_status,a.approval_status,b.approval_status as overapproval_status " +
                        " from agr_mst_tccmeeting2members a left join agr_mst_tapplication b on b.application_gid = a.application_gid " +
                         " left join hrm_mst_temployee d on a.approvalinitiate_by = d.employee_gid" +
                    " left join adm_mst_tuser e on e.user_gid = d.user_gid " +
                        " where a.application_gid='" + application_gid + "'and  a.ccapproval_flag = 'Y'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getccmember_list = new List<ccmembers_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getccmember_list.Add(new ccmembers_list
                    {
                        CCMember_name = dt["ccmember_name"].ToString(),
                        ccmember_gid = dt["ccmember_gid"].ToString(),
                        ccgroup_name = dt["ccgroup_name"].ToString(),
                        attendance_status = dt["attendance_status"].ToString(),
                        ccmeeting2members_gid = dt["ccmeeting2members_gid"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        ccapproval_flag = dt["ccapproval_flag"].ToString(),
                        overapproval_status = dt["overapproval_status"].ToString(),
                        approvalinitiate_by = dt["approvalinitiate_by"].ToString(),
                        approved_date = dt["approved_date"].ToString(),
                    });
                }
                values.ccmembers_list = getccmember_list;

            }
            dt_datatable.Dispose();

            //msSQL = " select employee_name,employee_gid,attendance_status, ccmeeting2othermembers_gid,approval_status " +
            //   " from agr_mst_tccmeeting2othermembers where application_gid='" + application_gid + "'";
            //dt_datatable = objdbconn.GetDataTable(msSQL);
            //var getccothermember_list = new List<otheruser_list>();
            //if (dt_datatable.Rows.Count != 0)
            //{
            //    foreach (DataRow dt in dt_datatable.Rows)
            //    {
            //        getccothermember_list.Add(new otheruser_list
            //        {
            //            employee_name = dt["employee_name"].ToString(),
            //            employee_gid = dt["employee_gid"].ToString(),
            //            attendance_status = dt["attendance_status"].ToString(),
            //            ccmeeting2othermembers_gid = dt["ccmeeting2othermembers_gid"].ToString(),
            //            approval_status = dt["approval_status"].ToString(),
            //        });
            //    }
            //    values.otheruser_list = getccothermember_list;
            //}
            //dt_datatable.Dispose();
        }

        public void DaGetApprovalShowFlag(string employee_gid, string application_gid, MdlMstCC values)
        {
            msSQL = "select ccmember_gid from agr_mst_tccmeeting2members where application_gid='" + application_gid + "' and ccmember_gid='" + employee_gid + "'" +
                " and ccapproval_flag='Y' and ccmail_flag ='Y'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objODBCDatareader.Close();
                values.approvalshow_flag = "Y";
            }
            else
            {
                objODBCDatareader.Close();
                values.approvalshow_flag = "N";
            }

        }

        public void DaGetMomFlag(string employee_gid, string application_gid, MdlMstCC values)
        {
            msSQL = "select approval_status from agr_mst_tapplication where application_gid='" + application_gid + "' " +
                " and (approval_status='CC Approved' or approval_status='CC Rejected')  ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objODBCDatareader.Close();
                values.mom_flag = "Y";
            }
            else
            {
                objODBCDatareader.Close();
                values.mom_flag = "N";

            }

        }



        public void DaGetMOMDescriptions(string employee_gid, string application_gid, MdlMstCC values)
        {
            msSQL = "select mom_description from agr_mst_tapplication where application_gid='" + application_gid + "' " +
            " and (mom_description='' or mom_description is null)  ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objODBCDatareader.Close();
                values.mom_descflag = "Y";
            }
            else
            {
                objODBCDatareader.Close();
                values.mom_descflag = "N";

            }

        }

        public void DaGetMOMReapproval(string employee_gid, string application_gid, MdlMstCC values)
        {
            msSQL = " select application_gid from agr_mst_tccmeeting2members where application_gid='" + values.application_gid + "' and approval_status='Pending' " +
                    " and attendance_status='P' and ccapproval_flag = 'Y'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objODBCDatareader.Close();
                values.mom_reapprove = "Y";
            }
            else
            {
                objODBCDatareader.Close();
                values.mom_reapprove = "N";

            }

        }

        public void DaGetMOMRemail(string employee_gid, string application_gid, MdlMstCC values)
        {
            msSQL = " select a.application_gid from agr_mst_tccmeeting2members a " +
                    " left join agr_mst_tapplication f on a.application_gid = f.application_gid  where a.application_gid='" + application_gid + "'  " +
                    " and a.attendance_status='P' and a.ccapproval_flag = 'Y' and a.ccmail_flag = 'N' and f.momapproval_flag = 'Y'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objODBCDatareader.Close();
                values.mom_ccmailflag = "Y";
            }
            else
            {
                objODBCDatareader.Close();
                values.mom_ccmailflag = "N";

            }

        }

        public void DaPostReMOMSubmit(string employee_gid, MdlMstCC values, string user_gid)
        {
            msSQL = " update agr_mst_tapplication set " +
                              " momapproval_flag='Y'," +
                              " momupdated_by='" + employee_gid + "'," +
                              " momupdated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                              " updated_by='" + employee_gid + "'," +
                              " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                              " where application_gid='" + values.application_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                if (mnResult != 0)
                {

                    k = 1;

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

                    msSQL = " select a.application_gid, a.ccmember_name,a.ccmember_gid,a.ccgroup_name,a.ccmeeting2members_gid,f.ccadmin_gid,  " +
                            " a.ccapproval_flag,concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as approvalinitiate_by, " +
                            " date_format(a.approved_date,'%d-%m-%Y %h:%i %p') as approved_date, " +
                            " a.attendance_status,a.approval_status,b.approval_status as overapproval_status " +
                            " from agr_mst_tccmeeting2members a  " +
                            " left join agr_mst_tapplication b on b.application_gid = a.application_gid " +
                            " left join agr_mst_tccschedulemeeting f on a.application_gid = f.application_gid " +
                            " left join hrm_mst_temployee d on a.approvalinitiate_by = d.employee_gid" +
                            " left join adm_mst_tuser e on e.user_gid = d.user_gid " +
                            " where a.application_gid='" + values.application_gid + "'and  a.ccapproval_flag = 'Y' and ccmail_flag ='N' ";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getccmember_list = new List<ccmembers_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {

                            lsemployee_name = dt["ccmember_name"].ToString();
                            lsemployee_gid = dt["ccmember_gid"].ToString();
                            lsccadmin_gid = dt["ccadmin_gid"].ToString();

                            string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
                            sToken = "";
                            int Length = 100;
                            for (int j = 0; j < Length; j++)
                            {
                                string sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                                sToken += sTempChars;
                            }

                            msGetGid = objcmnfunctions.GetMasterGID("CCAP");

                            msSQL = "Insert into agr_trn_tccapproval( " +
                                   " ccapproval_gid, " +
                                   " application_gid, " +
                                   " ccmeeting2members_gid," +
                                   " approval_gid," +
                                   " approval_name," +
                                   " approval_token," +
                                   //" requestapproval_remarks," +
                                   " ccapprovalrequest_flag," +
                                   " created_by," +
                                   " created_date)" +
                                   " values(" +
                                   "'" + msGetGid + "'," +
                                   "'" + dt["application_gid"].ToString() + "'," +
                                   "'" + dt["ccmeeting2members_gid"].ToString() + "'," +
                                   "'" + lsemployee_gid + "'," +
                                   "'" + lsemployee_name + "'," +
                                   "'" + sToken + "'," +
                                   //"'" + values.approval_remarks.Replace("'", "\\'") + "'," +
                                   "'Y'," +
                                   "'" + user_gid + "'," +
                                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            k = k + 1;


                            msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee " +
                                    " where employee_gid = '" + lsemployee_gid + "'";
                            lsto_mail = objdbconn.GetExecuteScalar(msSQL);


                            msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lsccadmin_gid.Replace(",", "', '") + "')";
                            cc_mailid = objdbconn.GetExecuteScalar(msSQL);
                        
                            msSQL = " select   (b.employee_emailid)  from agr_mst_tapplication a  " +
                                    " left join hrm_mst_temployee b on a.relationshipmanager_gid = b.employee_gid  " +
                                    " left join adm_mst_tuser c on b.user_gid = c.user_gid " +
                                     " where a.application_gid = '" + values.application_gid + "'";
                            cc_mailid1 = objdbconn.GetExecuteScalar(msSQL);


                            msSQL = " select   (b.employee_emailid)  from agr_trn_tapplicationapproval a  " +
                                    " left join hrm_mst_temployee b on a.approval_gid = b.employee_gid  " +
                                    " where a.hierary_level= '1' and a.application_gid = '" + values.application_gid + "'";
                            cc_mailid2 = objdbconn.GetExecuteScalar(msSQL);


                            msSQL = " select a.application_no,d.ccadmin_name,a.customer_name,e.loanfacility_amount,a.ccgroup_name,d.ccmeeting_date" +
                                    " from agr_mst_tapplication a left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                    " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                                    " left join agr_mst_tccschedulemeeting d on a.application_gid = d.application_gid" +
                                    " left join agr_mst_tapplication2loan e on a.application_gid = e.application_gid" +
                                    " where a.application_gid = '" + values.application_gid + "'";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                lsapplication_no = objODBCDatareader["application_no"].ToString();
                                lsccadmin_name = objODBCDatareader["ccadmin_name"].ToString();
                                lscustomer_name = objODBCDatareader["customer_name"].ToString();
                                lsloanfacility_amount = objODBCDatareader["loanfacility_amount"].ToString();
                                lsccgroup_name = objODBCDatareader["ccgroup_name"].ToString();
                                lsccmeeting_date = objODBCDatareader["ccmeeting_date"].ToString();

                            }
                            objODBCDatareader.Close();

                            msSQL = "select concat(user_firstname, ' ', user_lastname, '/', user_code) as requested_by from adm_mst_tuser where user_gid='" + user_gid + "'";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                lsrequested_by = objODBCDatareader["requested_by"].ToString();
                            }
                            objODBCDatareader.Close();

                            sub = " CC Approval Required ";
                            body = "Dear Sir/Madam,  <br />";
                            body = body + "<br />";
                            body = body + "Greetings,  <br />";
                            body = body + "<br />";
                            body = body + lsrequested_by + " has been Initiated the Approval Request and the details are as follows,<br />";
                            body = body + "<br />";
                            body = body + "<b>Application Number :</b> " + lsapplication_no + "<br />";
                            body = body + "<br />";
                            body = body + "<b>Customer Name :</b> " + HttpUtility.HtmlEncode(lscustomer_name )+ "<br />";
                            body = body + "<br />";
                            body = body + "<b>Facility Loan Amount :</b> " + HttpUtility.HtmlEncode(lsloanfacility_amount)+ "<br />";
                            body = body + "<br />";
                            body = body + "<b>CC Meeting Scheduled On :</b> " + lsccmeeting_date + "<br />";
                            body = body + "<br />";
                            body = body + "<b>CC Group Name :</b> " + HttpUtility.HtmlEncode(lsccgroup_name)+ "<br />";
                            body = body + "<br />";
                            //message = message + "<b>CC Admin :</b> " + lsccadmin_name + "<br />";
                            //message = message + "<br />";
                            body = body + "Kindly <a href=" + ConfigurationManager.AppSettings["Agrccapprovalurl"].ToString() + "?id=" + sToken + "> Click Here</a> and do the needful.<br />";
                            body = body + "<br />";
                            body = body + "<b>Thanks & Regards, </b> ";
                            body = body + "<br />";
                            body = body + "<b> CC Admin </b> ";
                            body = body + "<br />";
                            //message = message + lsccadmin_name + "<br />";
                            //message = message + "<br />";

                            //MailFlag = objcmnfunctions.SendSMTP2(ls_username, ls_password, lsto_mail, "CC Meeting Mail Approval", message, lscc_mail, strBCC);


                            string[] ccmail_Array = cc_mailid.Split(',');
                            string[] ccmail1_Array = cc_mailid1.Split(',');
                            string[] ccmail2_Array = cc_mailid2.Split(',');
                            ccmail1_Array = ccmail_Array.Union(ccmail1_Array).ToArray();
                            ccmail_Array = ccmail1_Array.Union(ccmail2_Array).ToArray();
                            cc_mailid = string.Join(",", ccmail_Array);

                            MailMessage message = new MailMessage();
                            SmtpClient smtp = new SmtpClient();
                            message.From = new MailAddress(ls_username);
                            message.To.Add(new MailAddress(lsto_mail));

                            lsBccmail_id = ConfigurationManager.AppSettings["SamagroCCMeetingbcc"].ToString();

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


                            if (cc_mailid != null & cc_mailid != string.Empty & cc_mailid != "")
                            {
                                lsCCReceipients = ccmail_Array;
                                if (cc_mailid.Length == 0)
                                {
                                    message.CC.Add(new MailAddress(cc_mailid));
                                }
                                else
                                {
                                    foreach (string CCEmail in ccmail_Array)
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

                            if (values.status == true)
                            {
                                msSQL = "Insert into agr_mst_tccmailcount( " +
                                " application_gid," +
                                " from_mail," +
                                " to_mail," +
                                //" cc_mail," +
                                " mail_status," +
                                " mail_senddate, " +
                                " created_by," +
                                " created_date)" +
                                " values(" +
                                "'" + values.application_gid + "'," +
                                "'" + ls_username + "'," +
                                "'" + lsto_mail + "'," +
                                //"'" + lscc_mail + "'," +
                                "'CC Meeting Mail Approval'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                "'" + user_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                if (mnResult != 0)
                                {

                                    msSQL = "update agr_mst_tccmeeting2members set approval_status='Pending', ccmail_flag = 'Y' where application_gid='" + values.application_gid + "' and ccapproval_flag = 'Y' and ccmail_flag = 'N' ";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                }
                                else
                                {
                                }
                            }



                        }

                        values.status = true;
                        values.message = "MOM Submitted Successfully";
                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error Occured While Submitting MOM";
                    }
                    dt_datatable.Dispose();

                }

            }
        }

        public void DaPastDateCheck(string date, result values)
        {
            DateTime documentdate = DateTime.Parse(Convert.ToDateTime(date).ToShortDateString());
            DateTime nowdate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));

            if (documentdate < nowdate)
            {
                values.status = false;
                values.message = "Past Date is Not Allowed...";
            }
            else
            {
                values.status = true;
            }
        }

        public void DaGetCCtoCreditLog(string application_gid, string employee_gid, MdlCCRevert values)
        {
            msSQL = " select application_gid,cctocreditlog_gid,reason as cctocredit_reason," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as sentbacktocc_by," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as sentbacktocc_date " +
                    " from agr_trn_tccmeetingtocreditlog a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                    " where a.application_gid = '" + application_gid + "' order by a.created_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcctocreditlog_list = new List<cctocreditlog_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getcctocreditlog_list.Add(new cctocreditlog_list
                    {
                        application_gid = dt["application_gid"].ToString(),
                        cctocreditlog_gid = dt["cctocreditlog_gid"].ToString(),
                        cctocredit_reason = dt["cctocredit_reason"].ToString(),
                        sentbacktocc_by = dt["sentbacktocc_by"].ToString(),
                        sentbacktocc_date = dt["sentbacktocc_date"].ToString()
                    });
                }
            }
            values.cctocreditlog_list = getcctocreditlog_list;
            dt_datatable.Dispose();
        }
        // CC Approval Pending Summary
        public void DaGetCCApprovalSummary(string employee_gid, MdlMstApplicationAdd values)
        {
            msSQL = " select a.application_gid,application_no,customerref_name,customer_urn,vertical_name, " +
                      " a.customer_name as customer_name,concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as submitted_by,a.approval_status,applicant_type," +
                      " date_format(a.submitted_date, '%d-%m-%Y %h:%i %p') as submitted_date,date_format(a.ccsubmitted_date, '%d-%m-%Y %h:%i %p') as ccsubmitted_date,  " +
                      " region,concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as ccsubmitted_by,a.ccgroup_name, " +
                      " date_format(f.ccmeeting_date, '%d-%m-%Y') as ccmeeting_date,date_format(f.created_date, '%d-%m-%Y %h:%i %p') as scheduled_date, " +
                      " concat(time_format(start_time, '%h:%i %p'), '-', time_format(end_time, '%h:%i %p')) as ccmeting_time," +
                      " date_format(now(),'%d-%m-%Y') as now_date,a.overalllimit_amount,concat(j.user_firstname, ' ', j.user_lastname, ' / ', j.user_code) as updated_by, " +
                      " concat(l.user_firstname, ' ', l.user_lastname, ' / ', l.user_code) as momupdated_by, momupdated_date,a.renewal_flag,a.amendment_flag,a.shortclosing_flag from agr_mst_tapplication a" +
                      " left join hrm_mst_temployee b on b.employee_gid = a.submitted_by" +
                      " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                      " left join hrm_mst_temployee d on a.ccsubmitted_by = d.employee_gid" +
                      " left join adm_mst_tuser e on e.user_gid = d.user_gid" +
                      " left join agr_mst_tccschedulemeeting f on a.application_gid = f.application_gid" +
                      " left join agr_mst_tccmeeting2members g on f.application_gid=g.application_gid" +
                      " left join agr_mst_tccmeeting2othermembers h on f.application_gid = h.application_gid" +
                      " left join hrm_mst_temployee i on f.updated_by = i.employee_gid " +
                      " left join adm_mst_tuser j on j.user_gid = i.user_gid " +
                      " left join hrm_mst_temployee k on a.momupdated_by = k.employee_gid " +
                      " left join adm_mst_tuser l on l.user_gid = k.user_gid " +
                      //" left join agr_trn_tccapproval m on m.application_gid = g.application_gid " +
                      " where ccsubmit_flag = 'Y' and meeting_status = 'Scheduled' and" +
                      " (f.created_by='" + employee_gid + "' or g.ccmember_gid='" + employee_gid + "' or h.employee_gid='" + employee_gid + "'" +
                      " or f.ccadmin_gid like '%" + employee_gid + "%') and g.attendance_status ='P' and g.ccapproval_flag='Y' and g.ccmail_flag ='Y'" +
                      " and a.momapproval_flag='Y' and a.cccompleted_flag='N' and g.ccmember_gid ='" + employee_gid + "' and g.approval_status ='Pending' " +
                      " and g.approval_status = 'Pending' " +
                      " group by a.application_gid order by " +
                      " STR_TO_DATE(ccmeeting_date, '%Y-%m-%d') asc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<applicationadd_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getapplicationadd_list.Add(new applicationadd_list
                    {
                        application_gid = dt["application_gid"].ToString(),
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical_name = dt["vertical_name"].ToString(),
                        application_status = dt["approval_status"].ToString(),
                        applicant_type = dt["applicant_type"].ToString(),
                        submitted_date = dt["submitted_date"].ToString(),
                        ccsubmitted_date = dt["ccsubmitted_date"].ToString(),
                        submitted_by = dt["submitted_by"].ToString(),
                        ccsubmitted_by = dt["ccsubmitted_by"].ToString(),
                        region = dt["region"].ToString(),
                        ccgroup_name = dt["ccgroup_name"].ToString(),
                        ccmeeting_date = dt["ccmeeting_date"].ToString(),
                        ccmeting_time = dt["ccmeting_time"].ToString(),
                        scheduled_date = dt["scheduled_date"].ToString(),
                        now_date = dt["now_date"].ToString(),
                        overalllimit_amount = dt["overalllimit_amount"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        updated_by = dt["updated_by"].ToString(),
                        momupdated_by = dt["momupdated_by"].ToString(),
                        momupdated_date = dt["momupdated_date"].ToString(),
                        renewal_flag = dt["renewal_flag"].ToString(),
                        amendment_flag = dt["amendment_flag"].ToString(),
                        shortclosing_flag = dt["shortclosing_flag"].ToString()
                    });
                }
            }
            values.applicationadd_list = getapplicationadd_list;
            dt_datatable.Dispose();
        }

         public void DaGetAdminCCSummary(string employee_gid, MdlMstApplicationAdd values)
        {
            msSQL = " select a.application_gid,application_no,customerref_name,customer_urn,vertical_name, " +
                      " a.customer_name as customer_name,concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as submitted_by,a.approval_status,applicant_type," +
                      " date_format(a.submitted_date, '%d-%m-%Y %h:%i %p') as submitted_date,date_format(a.ccsubmitted_date, '%d-%m-%Y %h:%i %p') as ccsubmitted_date,  " +
                      " region,concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as ccsubmitted_by,a.ccgroup_name, " +
                      " date_format(f.ccmeeting_date, '%d-%m-%Y') as ccmeeting_date,date_format(f.created_date, '%d-%m-%Y %h:%i %p') as scheduled_date, " +
                      " concat(time_format(start_time, '%h:%i %p'), '-', time_format(end_time, '%h:%i %p')) as ccmeting_time," +
                      " date_format(now(),'%d-%m-%Y') as now_date,a.overalllimit_amount,concat(j.user_firstname, ' ', j.user_lastname, ' / ', j.user_code) as updated_by, " +
                      " a.renewal_flag,a.amendment_flag,a.shortclosing_flag from agr_mst_tapplication a" +
                      " left join hrm_mst_temployee b on b.employee_gid = a.submitted_by" +
                      " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                      " left join hrm_mst_temployee d on a.ccsubmitted_by = d.employee_gid" +
                      " left join adm_mst_tuser e on e.user_gid = d.user_gid" +
                      " left join agr_mst_tccschedulemeeting f on a.application_gid = f.application_gid" +
                      " left join agr_mst_tccmeeting2members g on f.application_gid=g.application_gid" +
                      " left join agr_mst_tccmeeting2othermembers h on f.application_gid = h.application_gid" +
                      " left join hrm_mst_temployee i on f.updated_by = i.employee_gid " +
                      " left join adm_mst_tuser j on j.user_gid = i.user_gid " +
                      " where ccsubmit_flag = 'Y' and meeting_status = 'Scheduled' and" +
                      " (( f.ccadmin_gid like '%" + employee_gid + "%') and momapproval_flag = 'Y') " +
                      "  and (( a.approval_status = 'Submitted to CC'))  group by a.application_gid order by " +
                      " STR_TO_DATE(ccmeeting_date, '%Y-%m-%d') asc; ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<applicationadd_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getapplicationadd_list.Add(new applicationadd_list
                    {
                        application_gid = dt["application_gid"].ToString(),
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical_name = dt["vertical_name"].ToString(),
                        application_status = dt["approval_status"].ToString(),
                        applicant_type = dt["applicant_type"].ToString(),
                        submitted_date = dt["submitted_date"].ToString(),
                        ccsubmitted_date = dt["ccsubmitted_date"].ToString(),
                        submitted_by = dt["submitted_by"].ToString(),
                        ccsubmitted_by = dt["ccsubmitted_by"].ToString(),
                        region = dt["region"].ToString(),
                        ccgroup_name = dt["ccgroup_name"].ToString(),
                        ccmeeting_date = dt["ccmeeting_date"].ToString(),
                        ccmeting_time = dt["ccmeting_time"].ToString(),
                        scheduled_date = dt["scheduled_date"].ToString(),
                        now_date = dt["now_date"].ToString(),
                        overalllimit_amount = dt["overalllimit_amount"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        updated_by = dt["updated_by"].ToString(),
                        renewal_flag = dt["renewal_flag"].ToString(),
                        amendment_flag = dt["amendment_flag"].ToString(),
                        shortclosing_flag = dt["shortclosing_flag"].ToString()
                    });
                }
            }
            values.applicationadd_list = getapplicationadd_list;
            dt_datatable.Dispose();
        }


        public void DaGetScheduleMeetingList(string application_gid, string employee_gid, MdlMstCCschedule values)
        {
            try
            {
                msSQL = " select a.ccmember_name,a.ccmember_gid,a.ccgroup_name,a.ccmeeting2members_gid,a.ccapproval_flag,b.momapproval_flag, " +
                           " a.attendance_status,a.approval_status,b.approval_status as overapproval_status, " +
                           "  date_format(a.approved_date, '%d-%m-%Y %h:%i %p') as approved_date from agr_mst_tccmeeting2members a " +
                           " left join agr_mst_tapplication b on b.application_gid = a.application_gid " +
                           " where a.application_gid='" + application_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getccmember_list = new List<ccmember_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getccmember_list.Add(new ccmember_list
                        {
                            CCMember_name = dt["ccmember_name"].ToString(),
                            ccmember_gid = dt["ccmember_gid"].ToString(),
                            ccgroup_name = dt["ccgroup_name"].ToString(),
                            attendance_status = dt["attendance_status"].ToString(),
                            ccmeeting2members_gid = dt["ccmeeting2members_gid"].ToString(),
                            approval_status = dt["approval_status"].ToString(),
                            ccapproval_flag = dt["ccapproval_flag"].ToString(),
                            overapproval_status = dt["overapproval_status"].ToString(),
                            momapproval_flag = dt["momapproval_flag"].ToString(),
                            approved_date = dt["approved_date"].ToString(),
                        });
                    }
                    values.ccmember_list = getccmember_list;
                }
                dt_datatable.Dispose();

                msSQL = " select employee_name,employee_gid,attendance_status, ccmeeting2othermembers_gid,approval_status " +
                   " from agr_mst_tccmeeting2othermembers where application_gid='" + application_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getccothermember_list = new List<otheruser_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getccothermember_list.Add(new otheruser_list
                        {
                            employee_name = dt["employee_name"].ToString(),
                            employee_gid = dt["employee_gid"].ToString(),
                            attendance_status = dt["attendance_status"].ToString(),
                            ccmeeting2othermembers_gid = dt["ccmeeting2othermembers_gid"].ToString(),
                            approval_status = dt["approval_status"].ToString(),
                        });
                    }
                    values.otheruser_list = getccothermember_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch (Exception ex)
            {
                values.message = ex.ToString();
                values.status = false;
            }

        }

        public void DaCCApplicationCount(string user_gid, string employee_gid, CCCount_list values)
        {
            msSQL = " select COUNT(distinct a.application_gid) from agr_mst_tapplication a " +
                      " left join agr_mst_tccschedulemeeting f on a.application_gid = f.application_gid " +
                      " left join agr_mst_tccmeeting2members g on f.application_gid=g.application_gid " +
                      " left join agr_mst_tccmeeting2othermembers h on f.application_gid = h.application_gid  " +
                      " where ccsubmit_flag = 'Y' and meeting_status = 'Scheduled' and " +
                      " (f.created_by='" + employee_gid + "' or g.ccmember_gid='" + employee_gid + "' or h.employee_gid='" + employee_gid + "' " +
                      " or f.ccadmin_gid like '%" + employee_gid + "%') and g.attendance_status ='P'  " +
                      " and a.momapproval_flag='Y' and a.cccompleted_flag='N' and g.ccmember_gid ='" + employee_gid + "' and g.approval_status ='Pending'  " +
                      " and g.approval_status = 'Pending'";
            values.my_approval = objdbconn.GetExecuteScalar(msSQL);
            int my_approval = Convert.ToInt16(values.my_approval);

            msSQL =   " select COUNT(distinct a.application_gid) from agr_mst_tapplication a " +
                      " left join agr_mst_tccschedulemeeting f on a.application_gid = f.application_gid " +
                      " left join agr_mst_tccmeeting2members g on f.application_gid=g.application_gid " +
                      " left join agr_mst_tccmeeting2othermembers h on f.application_gid = h.application_gid " +
                      " where ccsubmit_flag = 'Y' and meeting_status = 'Scheduled' and" +
                      " (f.created_by='" + employee_gid + "' or g.ccmember_gid='" + employee_gid + "' or h.employee_gid='" + employee_gid + "'" +
                      " or f.ccadmin_gid like '%" + employee_gid + "%') " +
                      " and a.momapproval_flag='Y' and a.cccompleted_flag='N'  ";
            values.cc_tagged = objdbconn.GetExecuteScalar(msSQL);
            int cc_tagged = Convert.ToInt16(values.cc_tagged);

            msSQL = "   select COUNT(distinct a.application_gid) from agr_mst_tapplication a " +
                    " left join agr_mst_tccschedulemeeting f on a.application_gid = f.application_gid " +
                    " left join agr_mst_tccmeeting2members g on f.application_gid=g.application_gid " +
                    " left join agr_mst_tccmeeting2othermembers h on f.application_gid = h.application_gid " +
                    " where ccsubmit_flag = 'Y' and meeting_status = 'Scheduled' and " +
                    " (f.created_by='" + employee_gid + "' or g.ccmember_gid='" + employee_gid + "' or h.employee_gid='" + employee_gid + "' " +
                    " or f.ccadmin_gid like '%" + employee_gid + "%') " +
                    " and a.approval_status='CC Approved' or a.approval_status='CC Rejected' and a.momapproval_flag='Y' and a.cccompleted_flag='Y' ";
            values.cc_completed = objdbconn.GetExecuteScalar(msSQL);
            int cc_completed = Convert.ToInt16(values.cc_completed);

            msSQL = " select COUNT(distinct a.application_gid)from agr_mst_tapplication a " +
                    " left join agr_mst_tccschedulemeeting f on a.application_gid = f.application_gid " +
                    " left join agr_mst_tccmeeting2members g on f.application_gid=g.application_gid " +
                    " left join agr_mst_tccmeeting2othermembers h on f.application_gid = h.application_gid " +
                    " where   ccsubmit_flag = 'Y' and meeting_status = 'Scheduled' and " +
                    " ((( f.ccadmin_gid like '%" + employee_gid + "%') and momapproval_flag = 'N')  or f.created_by='" + employee_gid + "' or g.ccmember_gid='" + employee_gid + "' or h.employee_gid='" + employee_gid + "')" +
                    " and a.momapproval_flag='N'";
            values.scheduled_meeting = objdbconn.GetExecuteScalar(msSQL);


            int scheduled_meeting = Convert.ToInt16(values.scheduled_meeting);
            msSQL = " select COUNT(distinct a.application_gid) from agr_mst_tapplication a  " +
                    "  left join agr_mst_tccschedulemeeting f on a.application_gid = f.application_gid " +
                    " left join agr_mst_tccmeeting2members g on f.application_gid=g.application_gid " +
                    " where ccsubmit_flag = 'Y' and meeting_status = 'Scheduled' and " +
                    " (( f.ccadmin_gid like '%" + employee_gid + "%') and momapproval_flag = 'Y') " +
                    "  and (( a.approval_status = 'Submitted to CC'))";
            values.approval_pending = objdbconn.GetExecuteScalar(msSQL);
            int approval_pending = Convert.ToInt16(values.approval_pending);

          

        }


        public void DaGetACAutoApprovalSummary(string employee_gid, MdlMstApplicationAdd values)
        {
            msSQL = " select a.application_gid,application_no,customerref_name,customer_urn,vertical_name," +
                    " a.customer_name as customer_name,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as submitted_by,applicant_type,overalllimit_amount," +
                    " date_format(a.submitted_date,'%d-%m-%Y %h:%i %p') as submitted_date,date_format(a.ccsubmitted_date,'%d-%m-%Y %h:%i %p') as ccsubmitted_date," +
                    " region,concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as ccsubmitted_by,a.ccgroup_name, " +
                    " CASE WHEN(a.onboarding_status = 'Proposal' )  THEN 'Credit' " +
                    " WHEN(a.onboarding_status = 'Direct' ) THEN 'Advance' " +
                    " ELSE '-' END as onboarding_status , " +
                    " date_format(f.ccmeeting_date, '%d-%m-%Y') as ccmeeting_date,concat(time_format(start_time, '%h:%i %p'), '-', time_format(end_time, '%h:%i %p')) as ccmeting_time, " +
                    " date_format(f.created_date, '%d-%m-%Y %h:%i %p') as created_date, concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as created_by, " +
                    " a.approval_status,date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as  cccompleted_date from agr_mst_tapplication a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.submitted_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                    " left join hrm_mst_temployee d on a.ccsubmitted_by = d.employee_gid" +
                    " left join adm_mst_tuser e on e.user_gid = d.user_gid " +
                    " left join agr_mst_tccschedulemeeting f on a.application_gid = f.application_gid " +
                    " left join hrm_mst_temployee g on g.created_by = g.employee_gid" +
                    " left join adm_mst_tuser h on h.user_gid = g.user_gid " +
                    " where a.onboarding_status = 'Direct' group by a.application_gid " +
                    " order by  STR_TO_DATE(ccmeeting_date,'%Y-%m-%d') desc  ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<applicationadd_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getapplicationadd_list.Add(new applicationadd_list
                    {
                        application_gid = dt["application_gid"].ToString(),
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical_name = dt["vertical_name"].ToString(),
                        application_status = dt["approval_status"].ToString(),
                        applicant_type = dt["applicant_type"].ToString(),
                        submitted_date = dt["submitted_date"].ToString(),
                        ccsubmitted_date = dt["ccsubmitted_date"].ToString(),
                        submitted_by = dt["submitted_by"].ToString(),
                        ccsubmitted_by = dt["ccsubmitted_by"].ToString(),
                        region = dt["region"].ToString(),
                        ccgroup_name = dt["ccgroup_name"].ToString(),
                        ccmeeting_date = dt["ccmeeting_date"].ToString(),
                        ccmeting_time = dt["ccmeting_time"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        cccompleted_date = dt["cccompleted_date"].ToString(),
                        onboarding_status = dt["onboarding_status"].ToString(),
                        overalllimit_amount = dt["overalllimit_amount"].ToString()
                    });
                }
            }
            values.applicationadd_list = getapplicationadd_list;
            dt_datatable.Dispose();
        }
        public void DaPostCcMeetingSkip(string employee_gid, MdlCcMeetingSkip values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("CCMS");
            msSQL = " insert into agr_trn_tccmeetingskip(" +
                    " ccmeetingskip_gid," +
                    " application_gid," +
                    " reason," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.application_gid + "'," +
                    "'" + values.ccmeetingskip_reason.Replace("'", "") + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msSQL = " select application_gid, appcreditapproval_gid, approval_gid, approval_name, approval_type, " +
                        " approval_status, approval_remarks, approved_date, rejected_date, hold_date, approval_token, " +
                        " hierary_level, created_by, created_date, initiate_flag from agr_trn_tappcreditapproval " +
                        " where application_gid ='" + values.application_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        msGetGid1 = objcmnfunctions.GetMasterGID("CAPL");
                        msSQL = " insert into agr_trn_tappcreditapprovallog(" +
                                " appcreditapprovallog_gid," +
                                " appcreditapproval_gid," +
                                " application_gid," +
                                " approval_gid," +
                                " approval_name," +
                                " approval_type," +
                                " approval_status," +
                                " approval_remarks," +
                                " approved_date," +
                                " rejected_date," +
                                " hold_date," +
                                " approval_token," +
                                " hierary_level," +
                                " approval_initiateby," +
                                " approval_initiatedate," +
                                " initiate_flag," +
                                " ccmeetingskip_status," +
                                " created_by," +
                                " created_date)" +
                                " values(" +
                                "'" + msGetGid1 + "'," +
                                "'" + dt["appcreditapproval_gid"].ToString() + "'," +
                                "'" + values.application_gid + "'," +
                                "'" + dt["approval_gid"].ToString() + "'," +
                                "'" + dt["approval_name"].ToString() + "'," +
                                "'" + dt["approval_type"].ToString() + "'," +
                                "'" + dt["approval_status"].ToString() + "'," +
                                "'" + dt["approval_remarks"].ToString().Replace("'", "") + "',";
                        if (dt["approved_date"].ToString() == "" || dt["approved_date"].ToString() == null)
                        {
                            msSQL += "null,";
                        }
                        else
                        {
                            msSQL += "'" + Convert.ToDateTime(dt["approved_date"]).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                        }
                        if (dt["rejected_date"].ToString() == "" || dt["rejected_date"].ToString() == null)
                        {
                            msSQL += "null,";
                        }
                        else
                        {
                            msSQL += "'" + Convert.ToDateTime(dt["rejected_date"]).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                        }
                        if (dt["hold_date"].ToString() == "" || dt["hold_date"].ToString() == null)
                        {
                            msSQL += "null,";
                        }
                        else
                        {
                            msSQL += "'" + Convert.ToDateTime(dt["hold_date"]).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                        }

                        msSQL += "'" + dt["approval_token"].ToString() + "'," +
                                "'" + dt["hierary_level"].ToString() + "'," +
                                "'" + dt["created_by"].ToString() + "'," +
                                "'" + Convert.ToDateTime(dt["created_date"]).ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                "'" + dt["initiate_flag"].ToString() + "'," +
                                "'CC Meeting Skip'," +
                                "'" + employee_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                dt_datatable.Dispose();
                if (mnResult != 0)
                {
                    msSQL = " update agr_trn_tappcreditapproval set " +
                            " approval_status='Pending'," +
                            " approval_remarks=null," +
                            " approved_date=null," +
                            " initiate_flag='Y'" +
                            " where application_gid='" + values.application_gid + "' and hierary_level='0' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " delete from agr_trn_tappcreditapproval " +
                            " where application_gid='" + values.application_gid + "' and hierary_level<>'0' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " Update agr_mst_tapplication SET creditheadapproval_status = 'Pending' " +
                            " where application_gid = '" + values.application_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                //objvalues.Daccapprovedmail(values.application_gid);
                values.status = true;
                values.message = "CC Meeting Skipped Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured While Skip CC Meeting";
            }
        }
        public void DaGetGetCCMeetingSkipHistory(string employee_gid, MdlCcMeetingSkip values, string application_gid)
        {
            msSQL = " select f.application_gid,application_no,customerref_name,customer_urn,vertical_name," +
                    " f.customer_name as customer_name,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as submitted_by, " +
                    " f.approval_status,applicant_type, " +
                    " date_format(f.submitted_date,'%d-%m-%Y %h:%i %p') as submitted_date,region,overalllimit_amount,  " +
                    " concat(n.user_firstname,' ',n.user_lastname,' / ',n.user_code) as ccmeetingskip_by, " +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as ccmeetingskip_date,ccmeetingskip_gid " +
                    " from agr_trn_tccmeetingskip a " +
                    " left join agr_mst_tapplication f on f.application_gid = a.application_gid " +
                    " left join hrm_mst_temployee b on b.employee_gid= f.submitted_by " +
                    " left join adm_mst_tuser c  on c.user_gid=b.user_gid " +
                    " left join hrm_mst_temployee m on a.created_by = m.employee_gid " +
                    " left join adm_mst_tuser n on n.user_gid = m.user_gid " +
                    " where a.application_gid = '" + application_gid + "' order by ccsubmitted_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getccmeetingskiphistory_list = new List<ccmeetingskiphistory_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getccmeetingskiphistory_list.Add(new ccmeetingskiphistory_list
                    {
                        application_gid = dt["application_gid"].ToString(),
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical_name = dt["vertical_name"].ToString(),
                        application_status = dt["approval_status"].ToString(),
                        applicant_type = dt["applicant_type"].ToString(),
                        submitted_date = dt["submitted_date"].ToString(),
                        submitted_by = dt["submitted_by"].ToString(),
                        region = dt["region"].ToString(),
                        overalllimit_amount = dt["overalllimit_amount"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        ccmeetingskip_by = dt["ccmeetingskip_by"].ToString(),
                        ccmeetingskip_date = dt["ccmeetingskip_date"].ToString(),
                        ccmeetingskip_gid = dt["ccmeetingskip_gid"].ToString()
                    });
                }
            }
            values.ccmeetingskiphistory_list = getccmeetingskiphistory_list;
            dt_datatable.Dispose();
        }
        public void DaGetCCMeetingSkippedReason(MdlCcMeetingSkip values, string ccmeetingskip_gid)
        {
            try
            {
                msSQL = "select reason from agr_trn_tccmeetingskip where ccmeetingskip_gid = '" + ccmeetingskip_gid + "'";
                values.reason = objdbconn.GetExecuteScalar(msSQL);
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }
        public void DaGetCCMeetingSkipSummary(string employee_gid, MdlCcMeetingSkip values)
        {
            //msSQL = " select f.application_gid,application_no,customerref_name,customer_urn,vertical_name," +
            //        " f.customer_name as customer_name,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as submitted_by, " +
            //        " f.approval_status,applicant_type, " +
            //        " date_format(f.submitted_date,'%d-%m-%Y %h:%i %p') as submitted_date,region,overalllimit_amount,  " +
            //        " concat(n.user_firstname,' ',n.user_lastname,' / ',n.user_code) as ccmeetingskip_by, " +
            //        " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as ccmeetingskip_date,ccmeetingskip_gid, " +
            //        " f.renewal_flag,f.enhancement_flag from ocs_trn_tccmeetingskip a " +
            //        " left join ocs_mst_tapplication f on f.application_gid = a.application_gid " +
            //        " left join hrm_mst_temployee b on b.employee_gid= f.submitted_by " +
            //        " left join adm_mst_tuser c  on c.user_gid=b.user_gid " +
            //        " left join hrm_mst_temployee m on a.created_by = m.employee_gid " +
            //        " left join adm_mst_tuser n on n.user_gid = m.user_gid " +
            //        " where (a.created_date = (select max(h.created_date) from ocs_trn_tccmeetingskip h where h.application_gid=a.application_gid)) " +
            //        " group by a.application_gid order by ccsubmitted_date desc ";
            //dt_datatable = objdbconn.GetDataTable(msSQL);
            msSQL = "call agr_trn_scheduledccmeetingskipsummary";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getccmeetingskip_list = new List<ccmeetingskip_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getccmeetingskip_list.Add(new ccmeetingskip_list
                    {
                        application_gid = dt["application_gid"].ToString(),
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical_name = dt["vertical_name"].ToString(),
                        application_status = dt["approval_status"].ToString(),
                        applicant_type = dt["applicant_type"].ToString(),
                        submitted_date = dt["submitted_date"].ToString(),
                        submitted_by = dt["submitted_by"].ToString(),
                        region = dt["region"].ToString(),
                        overalllimit_amount = dt["overalllimit_amount"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        ccmeetingskip_by = dt["ccmeetingskip_by"].ToString(),
                        ccmeetingskip_date = dt["ccmeetingskip_date"].ToString(),
                        ccmeetingskip_gid = dt["ccmeetingskip_gid"].ToString(),
                        renewal_flag = dt["renewal_flag"].ToString(),
                        amendment_flag = dt["amendment_flag"].ToString()
                    });
                }
            }
            values.ccmeetingskip_list = getccmeetingskip_list;
            dt_datatable.Dispose();
        }

        public void DaGetSendbackCCMeetingSkippedReason(MdlCcMeetingSkip values, string application_gid)
        {
            try
            {
                msSQL = " select reason as ccmeetingskip_remarks,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as ccmeetingskip_by," +
                        " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as ccmeetingskip_date" +
                        " from agr_trn_tccmeetingskip a" +
                        " left join hrm_mst_temployee b on b.employee_gid= a.created_by " +
                        " left join adm_mst_tuser c  on c.user_gid=b.user_gid " +
                        " where (a.created_date = (select max(h.created_date) from agr_trn_tccmeetingskip h where h.application_gid=a.application_gid)) " +
                        " and application_gid = '" + application_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.ccmeetingskip_remarks = objODBCDatareader["ccmeetingskip_remarks"].ToString();
                    values.ccmeetingskip_by = objODBCDatareader["ccmeetingskip_by"].ToString();
                    values.ccmeetingskip_date = objODBCDatareader["ccmeetingskip_date"].ToString();
                }
                objODBCDatareader.Close();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }
    }
}