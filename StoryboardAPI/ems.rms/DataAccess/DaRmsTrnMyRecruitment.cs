using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Web.Http;
using ems.utilities.Models;
using ems.utilities.Functions;
using System.Data;
using System.Data.Odbc;
using ems.rms.Models;

namespace ems.rms.DataAccess
{
    public class DaRmsTrnMyRecruitment
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        DataTable dt_datatable;
        string msSQL, msgetGID;
        string lsduplicate_validity,lscreated_date;
        string lsbusinessunit_gid, lsbusinessunit2team_gid;
        OdbcDataReader objODBCDatareader, objODBCDataReader1;
        int mnResult;
       
        public bool DaGetmyrecruitmentsummary(string employee_gid, MdlRmsTrnMyRecruitment values)
        {

            msSQL = "select distinct date_format(a.created_date,'%d-%m-%Y') as created_date,a.company_name,a.job_code,a.job_title,a.noof_position,a.jobposition_gid," +
                 " (select count(x.recruitmentadd_gid) as candidate_fetched from rms_trn_trecruitmentadd x where a.jobposition_gid=x.jobposition_gid and" +
                 " x.created_by='" + employee_gid + "') as candidate_fetched,(noof_position-(select count(x.recruitmentadd_gid) as position_closed from" +
                 " rms_trn_trecruitmentadd x where x.candidate_status='Invoice Raised-Joined' and a.jobposition_gid=x.jobposition_gid and x.created_by='" + employee_gid + "')) as position_pending," +
                 " (select count(x.recruitmentadd_gid) as position_closed from rms_trn_trecruitmentadd x where x.candidate_status='Invoice Raised-Joined' and" +
                 " a.jobposition_gid=x.jobposition_gid and x.created_by='" + employee_gid + "') as position_closed," +
                 " a.spoc_name,f.businessunit_name,jobcodefreeze_flag   from rms_trn_tjobposition a" +
               " left join rms_trn_tjobposition2team i on i.jobposition_gid=a.jobposition_gid" +
               " left join hrm_mst_temployee d on i.employee_gid=d.employee_gid" +
               " left join adm_mst_tuser e on d.user_gid=e.user_gid" +
               " left join rms_trn_tjobposition2recruiter g on a.jobposition_gid=g.jobposition_gid" +
               " left join rms_mst_tbusinessunit f on i.businessunit_gid=f.businessunit_gid where g.recruiter_gid ='" + employee_gid + "'" +
              "  group by  a.jobposition_gid order by a.jobposition_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getrecruitmentsummary = new List<myrecruitmentsummary_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {

                    getrecruitmentsummary.Add(new myrecruitmentsummary_list
                    {
                        created_date = dt["created_date"].ToString(),
                        company_name = dt["company_name"].ToString(),
                        job_code = dt["job_code"].ToString(),
                        job_title = dt["job_title"].ToString(),
                        noof_position = dt["noof_position"].ToString(),
                        candidate_fetched = dt["candidate_fetched"].ToString(),
                        position_pending = dt["position_pending"].ToString(),
                        position_closed = dt["position_closed"].ToString(),
                        spoc_name = dt["spoc_name"].ToString(),
                        businessunit_name = dt["businessunit_name"].ToString(),
                        jobcodefreeze_flag = dt["jobcodefreeze_flag"].ToString(),
                        jobposition_gid = dt["jobposition_gid"].ToString()
                    });
                }
                values.myrecruitmentsummary_list = getrecruitmentsummary;
            }
            dt_datatable.Dispose();

            return true;
        }
        public void DaGetJobinfo( MdlJobinfo values,string jobposition_gid)
        {

            msSQL = "select job_code,job_title,spoc_name,noof_position,concat(experience_min ,' To ',experience_max) as experience," +
                " concat(ctcbudget_min,' To ',ctcbudget_max) as ctc_budget," +
            " education_qualification,job_description,mandatroy_skills,notice_period,domain,job_location  from rms_trn_tjobposition  " +
            " where jobposition_gid='" + jobposition_gid + "'";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                values.job_code = objODBCDatareader["job_code"].ToString();
                values.job_title = objODBCDatareader["job_title"].ToString();
                values.spoc_name = objODBCDatareader["spoc_name"].ToString();
                values.noof_position = objODBCDatareader["noof_position"].ToString();
                values.experience = objODBCDatareader["experience"].ToString();
                values.qualification = objODBCDatareader["education_qualification"].ToString();
                values.job_description = objODBCDatareader["job_description"].ToString();
                values.ctc_budget = objODBCDatareader["ctc_budget"].ToString();
                values.skills = objODBCDatareader["mandatroy_skills"].ToString();
                values.notice_period = objODBCDatareader["notice_period"].ToString();
                values.job_location = objODBCDatareader["job_location"].ToString();
                
            }
            objODBCDatareader.Close();
            values.status = true;
    }
        public void Dapostcandidateinfo(mdlcandidateinfo values, string employee_gid)
        {
            msSQL = "select duplicate_validity,date_format(created_date ,'%Y-%m-%d') as created_date from rms_trn_tjobposition where " +
              " jobposition_gid='" + values.jobposition_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsduplicate_validity= objODBCDatareader["duplicate_validity"].ToString();
             //   var dt = objODBCDatareader["created_date"].ToString();
                lscreated_date = objODBCDatareader["created_date"].ToString();
            }

            DateTime created_date = Convert.ToDateTime(lscreated_date);
            DateTime date = DateTime.UtcNow.Date;
            var lsdate = (date - created_date).TotalDays.ToString();

            long lsdiff_day = Convert.ToInt64(lsdate);
            long lsduplicate_day = Convert.ToInt64(lsduplicate_validity);
            if (lsdiff_day >= lsduplicate_day)
            {

            }
            else
            {
                msSQL = " select candidate_status from rms_trn_trecruitmentadd where jobposition_gid='" + values.jobposition_gid + "' and " +
                   " (candidate_mobileno='" + values.mobile_no + "' or candidateemail_address1='" + values.emailaddress1 + "')";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    if (objODBCDatareader["candidate_status"].ToString() == values.candidate_status)
                    {
                        values.message = "Duplicate Entry For this Job Code";
                        values.status = false;
                    }
                }
            }
            msSQL = "select a.businessunit_gid,a.businessunit2team_gid from rms_trn_tjobposition2team a" +
            " left join rms_mst_tbusinessunit2team b on b.businessunit2team_gid=a.businessunit2team_gid" +
            " left join rms_trn_tbusinessunitteam2recruiter c on  b.businessunit2team_gid=c.businessunit2team_gid where jobposition_gid='" + values.jobposition_gid + "'" +
            " and c.employee_gid='" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
        if(objODBCDatareader.HasRows == true)
            {
                lsbusinessunit_gid = objODBCDatareader["businessunit_gid"].ToString() ;
                lsbusinessunit2team_gid = objODBCDatareader["businessunit2team_gid"].ToString();
            }
          
      
            msgetGID = objcmnfunctions.GetMasterGID("RCAN");
            msSQL = " insert into rms_trn_trecruitmentadd (" +
                        " recruitmentadd_gid, " +
                        " jobposition_gid ," +
                        " businessunit_gid ," +
                        " businessunit2team_gid ," +
                        " recruiter_gid ," +
                        " candidate_firstname, " +
                        " candidate_lastname ," +
                        " candidate_location ," +
                        " candidate_gender ," +
                        " candidate_dob ," +
                        " candidate_mobileno," +
                        " candidateemail_address1 ," +
                        " candidateemail_address2 ," +
                        " candidate_status ," +
                        " experience, " +
                        " current_company ," +
                        " current_ctc ," +
                        " designation ," +
                        " education ," +
                        " candidate_remarks ," +
                        " created_by, " +
                        " created_date ," +
                        " updated_by, " +
                        " updated_date )" +
                        " values (" +
                        "'" + msgetGID + "'," +
                        "'" + values.jobposition_gid + "'," +
                        "'" + lsbusinessunit_gid + "'," +
                        "'" + lsbusinessunit2team_gid + "'," +
                        "'" + employee_gid + "'," +
                        "'" + values.firstname + "'," +
                        "'" + values.lastname + "'," +
                        "'" + values.current_location + "'," +
                        "'" + values.gender + "',";
            if((values.dob==null)||(values.dob == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.dob).ToString("yyyy-MM-dd") + "',";
            }

            msSQL += "'" + values.mobile_no + "'," +
                            "'"+ values.emailaddress1 +"'," +
                            "'" + values.emailaddress2 +"'," +
                            "'" + values.candidate_status + "'," +
                            "'" + values.experience + "'," +
                            "'" + values.currentcompany +"'," +
                            "'" + values.current_ctc +"'," +
                            "'" + values.current_designation + "'," +
                            "'" + values.qualification + "'," +
                            "'" + values.remarks + "'," +
                             "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +  "'," +
                            "'" + employee_gid + "'," +
                            "'"  + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+"')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if(mnResult!=0)
            {
                values.message = "Candidate Details Added Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured while adding";
                values.status = false;
            }

        }

        public void DaGetcandidatesummary(MdlRmsTrnMyRecruitment values, string jobposition_gid,string employee_gid)
        {

            msSQL = " select concat(candidate_firstname,' ',candidate_lastname) as candidate_name,candidate_mobileno,candidateemail_address1,candidateemail_address2," +
                " candidate_status,experience,date_format(created_date,'%d-%m-%Y %H:%i %p') as created_date," +
                " date_format(updated_date,'%d-%m-%Y %h:%i %p') as updated_date,recruitmentadd_gid,recruiter_gid from " +
                " rms_trn_trecruitmentadd where recruiter_gid='" + employee_gid + "' " +
                " and jobposition_gid='" + jobposition_gid + "'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcandidate_list = new List<candidate_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcandidate_list.Add(new candidate_list
                    {
                        candidate_name = dr_datarow["candidate_name"].ToString(),
                        mobile_no = dr_datarow["candidate_mobileno"].ToString(),
                        emailaddress1 = (dr_datarow["candidateemail_address1"].ToString()),
                        emailaddress2 = (dr_datarow["candidateemail_address2"].ToString()),
                        candidate_status = (dr_datarow["candidate_status"].ToString()),
                        experience = (dr_datarow["experience"].ToString()),
                        recruitmentadd_gid = (dr_datarow["recruitmentadd_gid"].ToString()),
                        recruiter_gid = (dr_datarow["recruiter_gid"].ToString()),
                        updated_date = (dr_datarow["updated_date"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                    });
                }
                values.candidate_list = getcandidate_list;
            }
            dt_datatable.Dispose();
            values.status = true;
        }

        public void DaGetCandidateinfo(MdlRmsTrnMyRecruitment values,string jobposition_gid, string recruitmentadd_gid)
        {

            msSQL = " select recruitmentadd_gid, recruiter_gid, concat(candidate_firstname,' ', candidate_lastname) as candidate_name,candidate_mobileno," +
               "  candidateemail_address1,case when candidateemail_address2='' then '---' else candidateemail_address2 end as candidateemail_address2,candidate_gender," +
               "  date_format(candidate_dob,'%d-%m-%Y') as candidate_dob,case when candidate_location='' then '---' else candidate_location end as candidate_location," +
                " case when current_company='' then '---' else current_company end as current_company ," +
                " case when current_ctc ='' then '---' else current_ctc end as current_ctc,case when designation='' then '---'" +
               "  else designation end as designation,case when education='' then '---' else education end as education," +
               "  candidate_status,case when experience='' then '---' else experience end as experience,candidate_remarks from" +
             " rms_trn_trecruitmentadd where recruitmentadd_gid='" + recruitmentadd_gid + "'" +
             " and jobposition_gid='" + jobposition_gid + "'";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                values.candidate_name = objODBCDatareader["candidate_name"].ToString();
                values.candidate_mobileno = objODBCDatareader["candidate_mobileno"].ToString();
                values.candidateemail_address1 = objODBCDatareader["candidateemail_address1"].ToString();
                values.candidateemail_address2 = objODBCDatareader["candidateemail_address2"].ToString();
                values.candidate_gender = objODBCDatareader["candidate_gender"].ToString();
                values.candidate_dob = objODBCDatareader["candidate_dob"].ToString();
                values.candidate_location = objODBCDatareader["candidate_location"].ToString();
                values.current_company = objODBCDatareader["current_company"].ToString();
                values.current_ctc = objODBCDatareader["current_ctc"].ToString();
                values.candidate_status = objODBCDatareader["candidate_status"].ToString();
                values.experience = objODBCDatareader["experience"].ToString();
                values.candidate_remarks = objODBCDatareader["candidate_remarks"].ToString();
            }
            objODBCDatareader.Close();
            values.status = true;
        }

        public void Dapostcandidatevalidation(Mdlinterviewprocess values, string employee_gid)
        {
            msgetGID = objcmnfunctions.GetMasterGID("RMJV");

            msSQL = "insert into rms_trn_tvalidationlog (" +
                    " validation_gid," +
                    " recruitmentadd_gid," +
                    " jobposition_gid," +
                    " candidate_status ," +
                    " created_date ," +
                    " created_by )" +
                    " values (" +
                    "'" + msgetGID + "'," +
                    "'" + values.recruitmentadd_gid + "'," +
                    "'" + values.jobposition_gid+ "'," +
                    "'" + values.validation_status+  "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                    "'" + employee_gid + "')";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update rms_trn_trecruitmentadd set " +
                   " candidate_status='" + values.validation_status+ "'," +
                   " updated_by='" + employee_gid +"'," +
                   "updated_date ='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                   " where recruitmentadd_gid='" + values.recruitmentadd_gid+ "'";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
       if(mnResult!=0)
            {
                values.message = "validation Status Updated Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured while updating status";
                values.status = false;
            }

        }

        public void DaPostInterviewDetails(Mdlinterviewprocess values, string employee_gid)
        {

            msgetGID = objcmnfunctions.GetMasterGID("RMSL");
            msSQL = "insert into rms_trn_tinterviewschedulelog (" +
                         " interviewschedulelog_gid ," +
                         " recruitmentadd_gid ," +
                         " jobposition_gid ," +
                         " interview_date ," +
                         " interview_type," +
                         " interviewscheduled_time ," +
                         " interview_status," +
                         " interview_remarks ," +
                         " created_by , " +
                         " created_date " +
                         " )values(" +
                         " '" + msgetGID + "'," +
                         " '" + values.recruitmentadd_gid + "'," +
                         " '" + values.jobposition_gid + "'," +
                         " '" + Convert.ToDateTime(values.interview_date).ToString("yyyy-MM-dd") + "'," +
                         " '" +values.interview_type+ "'," +
                         " '" + values.interview_scheduletime + "'," +
                         " '" + values.interview_status + "'," +
                         " '" + values.interview_remarks + "'," +
                         "'" + employee_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")  + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

        msSQL = "select interview_gid from rms_trn_tinterviewdetails where recruitmentadd_gid='" + values.recruitmentadd_gid + "'";
            string lsinterview_gid = objdbconn.GetExecuteScalar(msSQL);
      if(lsinterview_gid=="")
            {
                msgetGID = objcmnfunctions.GetMasterGID("IVDT");
           

            msSQL = " insert into rms_trn_tinterviewdetails(" +
                     " interview_gid," +
                     " recruitmentadd_gid," +
                     " jobposition_gid," +
                     " interview_date," +
                     " interview_type," +
                     " interviewscheduled_time ," +
                     " interview_status," +
                     " interview_remarks ," +
                     " created_by , " +
                     " created_date " +
                     " )values(" +
                     " '" + msgetGID + "'," +
                         " '" + values.recruitmentadd_gid + "'," +
                         " '" + values.jobposition_gid + "'," +
                         " '" + Convert.ToDateTime(values.interview_date).ToString("yyyy-MM-dd") + "'," +
                         " '" + values.interview_type + "'," +
                         " '" + values.interview_scheduletime + "'," +
                         " '" + values.interview_status + "'," +
                         " '" + values.interview_remarks + "'," +
                         "'" + employee_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
 }

            else
            {

                msSQL = "update rms_trn_tinterviewdetails set interview_date='" + Convert.ToDateTime(values.interview_date).ToString("yyyy-MM-dd") + "'," +
                    " interview_type ='" + values.interview_type+ "'," +
                    " interviewscheduled_time='" + values.interview_scheduletime + "'," +
                    " interview_status='" + values.interview_status + "'," +
                    " interview_remarks ='" + values.interview_remarks + "'," +
                      " updated_by='" + employee_gid +"'," +
               "updated_date ='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
               "where recruitmentadd_gid='" + values.recruitmentadd_gid + "' and interview_gid='" + lsinterview_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
       }

            msSQL = "update rms_trn_trecruitmentadd set " +
              " candidate_status='" + values.interview_status + "'," +
              " updated_by='" + employee_gid+"'," +
               "updated_date ='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
              " where recruitmentadd_gid='"+ values.recruitmentadd_gid+ "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
        if(mnResult!=0)
            {
                values.message = "Interview Status Updated Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured while updating status";
                values.status = false;
            }
        }

        public void DaPostInterviewStatus(Mdlinterviewprocess values, string employee_gid)
        {

            msgetGID = objcmnfunctions.GetMasterGID("RMJS");

            msSQL = "insert into rms_trn_tscheduledlog (" +
                    " scheduled_gid," +
                    " recruitmentadd_gid," +
                    " jobposition_gid," +
                    " candidate_status ," +
                    " created_by ," +
                    " created_date )" +
                    " values (" +
                    "'" + msgetGID +"'," +
                    "'" + values.recruitmentadd_gid+"'," +
                    "'" + values.jobposition_gid+ "'," +
                    "'" + values.interview_status+ "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update rms_trn_trecruitmentadd set " +
                   " candidate_status='" + values.interview_status + "'," +
                   " updated_by='" + employee_gid + "'," +
                   "updated_date ='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                   " where recruitmentadd_gid='"+ values.recruitmentadd_gid+ "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                 if (mnResult != 0)
            {
                values.message = "Interview Scheduled Status Updated Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured while updating status";
                values.status = false;
            }
           
        }

        public void DaPostJoiningStatus(Mdlinterviewprocess values, string employee_gid)
        {

           if( values.joining_status == "")
            {


                msSQL = " update  rms_trn_trecruitmentadd set " +
                " candidate_status='Confirmed on Joining'," +
                " updated_by='" + employee_gid+ "'," +
                "updated_date ='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                " where recruitmentadd_gid='" + values.recruitmentadd_gid+"'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update rms_trn_tcandidateconfirm set " +
                " date_joining='" + Convert.ToDateTime(values.joining_date).ToString("yyyy-MM-dd")+"'," +
                " updated_by='"  +employee_gid + "'," +
                   "updated_date ='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                " where recruitmentadd_gid='" + values.recruitmentadd_gid + "' and jobposition_gid='" + values.jobposition_gid+"'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
       }
            else
            {


                msSQL = " update  rms_trn_trecruitmentadd set " +
                 " candidate_status='"+ values.joining_status+ "'," +
                 " updated_by='" + employee_gid + "'," +
                       "updated_date ='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where recruitmentadd_gid='" + values.recruitmentadd_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

        }
            if (mnResult != 0)
            {
                values.message = "Joining Status Updated Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured while updating status";
                values.status = false;
            }

        }

        public void DaPostJobStaus(Mdlinterviewprocess values, string employee_gid)
        {
            msSQL = " update  rms_trn_trecruitmentadd set " +
            " candidate_status='Invoice Raised-Joined'," +
            " updated_by='"+ employee_gid+ "'," +
               "updated_date ='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
            " where recruitmentadd_gid='" + values.recruitmentadd_gid+"'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "select candidateconfirm_gid from rms_trn_tcandidateconfirm where recruitmentadd_gid='" + values.recruitmentadd_gid + "' " +
                " and jobposition_gid='" + values.jobposition_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {


                msSQL = "update rms_trn_tcandidateconfirm set ctc_offered='"+ values.offered_ctc+"'," +
                    " date_joining='" + Convert.ToDateTime(values.joining_date).ToString("yyyy-MM-dd") + "'," +
                    " invoice_date='" + Convert.ToDateTime(values.invoice_date).ToString("yyyy-MM-dd") + "'," +
                    " invoice_number='" + values.invoice_no+ "'," +
                    " remark='" + values.remarks + "'," +
                    " status ='Invoice Raised-Joined'," +
                    " updated_by='" + employee_gid + "'," +
                    " updated_date ='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    " where recruitmentadd_gid='" + values.recruitmentadd_gid + "' and jobposition_gid='" + values.jobposition_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
            else
            {

                msgetGID = objcmnfunctions.GetMasterGID("CDDT");
            msSQL = " insert into rms_trn_tcandidateconfirm(" +
                      " candidateconfirm_gid," +
                      " recruitmentadd_gid," +
                      " jobposition_gid," +
                      " ctc_offered," +
                      " date_joining," +
                      " invoice_date ," +
                      " invoice_number ," +
                      " remark ," +
                      " status , " +
                      " created_by , " +
                      " created_date )values(" +
                      " '" + msgetGID + "'," +
                      " '" + values.recruitmentadd_gid + "'," +
                      " '" + values.jobposition_gid + "'," +
                      " '" + values.offered_ctc + "'," +
                      " '" + Convert.ToDateTime(values.joining_date).ToString("yyyy-MM-dd") + "'," +
                      " '" + Convert.ToDateTime(values.invoice_date).ToString("yyyy-MM-dd") + "'," +
                      " '" + values.invoice_no + "'," +
                      " '" + values.remarks + "'," +
                      " 'Invoice Raised-Joined'," +
                      "'" + employee_gid+ "'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
      }
            if (mnResult != 0)
            {
                values.message = "Job Status Updated Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured while updating status";
                values.status = false;
            }

        }
        public void DaEditCandidateinfo(mdlcandidateinfo values, string recruitmentadd_gid)
        {

            msSQL = "select recruitmentadd_gid,recruiter_gid,candidate_firstname,candidate_lastname,candidate_mobileno,candidateemail_address1,candidate_location," +
               " candidateemail_address2,candidate_remarks,candidate_gender,candidate_status,candidate_dob,experience,current_company,current_ctc," +
               " designation,education from" +
             " rms_trn_trecruitmentadd where recruitmentadd_gid='" + recruitmentadd_gid + "'";
           

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                values.firstname = objODBCDatareader["candidate_firstname"].ToString();
                values.lastname = objODBCDatareader["candidate_lastname"].ToString();
               // values.mobile_no = objODBCDatareader["candidate_mobileno"].ToString();
                values.emailaddress1 = objODBCDatareader["candidateemail_address1"].ToString();
                values.emailaddress2 = objODBCDatareader["candidateemail_address2"].ToString();
                values.gender = objODBCDatareader["candidate_gender"].ToString();
                if (objODBCDatareader["candidate_dob"].ToString() == "")
                {
                }
                else
                {
                    values.dateof_birth = Convert.ToDateTime(objODBCDatareader["candidate_dob"]);
                }
                if (objODBCDatareader["candidate_mobileno"].ToString() != "")
                {
                    values.mobilenumber = Convert.ToDouble(objODBCDatareader["candidate_mobileno"].ToString());
                }
                values.current_location = objODBCDatareader["candidate_location"].ToString();
                values.currentcompany = objODBCDatareader["current_company"].ToString();
                values.current_ctc = objODBCDatareader["current_ctc"].ToString();
                values.candidate_status = objODBCDatareader["candidate_status"].ToString();
                values.experience = objODBCDatareader["experience"].ToString();
                values.remarks = objODBCDatareader["candidate_remarks"].ToString();
                values.qualification = objODBCDatareader["education"].ToString();
                values.current_designation = objODBCDatareader["designation"].ToString();
               
            }
            objODBCDatareader.Close();
            msSQL = "select joblocation_gid from rms_trn_tjoblocationcandidate where joblocation_name='" + values.current_location + "'";
            values.joblocation_gid = objdbconn.GetExecuteScalar(msSQL);

            values.status = true;
        }
        public void Daupdatecandidateinfo(mdlcandidateinfo values,string employee_gid)
        {
            msSQL = "select joblocation_name from rms_trn_tjoblocationcandidate where joblocation_gid='" + values.current_location + "'";
            string lscurrent_location = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select jobposition_gid from rms_trn_trecruitmentadd where recruitmentadd_gid='" + values.recruitmentadd_gid + "'";
            string lsjobposition_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select candidate_status from rms_trn_trecruitmentadd where jobposition_gid='" + lsjobposition_gid + "' and " +
                " (candidate_mobileno='" +values.mobile_no+ "' or candidateemail_address1='" + values.emailaddress1 + "')";
            string lscandidate_status = objdbconn.GetExecuteScalar(msSQL);
            if(values.candidate_status==lscandidate_status)
                {
                msSQL = " update rms_trn_trecruitmentadd set " +
                   " candidate_firstname='" + values.firstname+ "', " +
                   " candidate_lastname='"+ values.lastname+ "' ," +
                   " candidate_location ='" + lscurrent_location + "'," +
                   " candidate_gender ='" +values.gender+ "',";
               
            msSQL += " candidate_mobileno='" + Convert.ToDouble(values.mobilenumber).ToString() + "',"  +
                            " candidateemail_address1='"+values.emailaddress1+ "' ," +
                            " candidateemail_address2 ='" + values.emailaddress1 + "'," +
                            " candidate_status='" + values.candidate_status+ "' ," +
                            " experience='" + values.experience+"', " +
                            " current_company='"+values.currentcompany+ "' ," +
                            " current_ctc='" + values.current_ctc+ "' ," +
                            " designation='"+ values.current_designation+ "' ," +
                            " education ='"+values.qualification+ "'," +
                            " candidate_remarks='" + values.remarks+ "' ," +
                            " updated_by='" + employee_gid+"'," +
                            " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                            " where recruitmentadd_gid='"+ values.recruitmentadd_gid + "' and"+
                            " jobposition_gid='" + lsjobposition_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (Convert.ToDateTime(values.dob).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
                {

                }
                else
                {
                    msSQL = "update rms_trn_trecruitmentadd set dob='" + Convert.ToDateTime(values.dob).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "'" +
                        " where recruitmentadd_gid='" + values.recruitmentadd_gid + "' and" +
                            " jobposition_gid='" + lsjobposition_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

            }

            else if (values.candidate_status !="Interested")
            {
                msSQL = " update rms_trn_trecruitmentadd set " +
                    " candidate_firstname='" + values.firstname + "', " +
                    " candidate_lastname='" + values.lastname + "' ," +
                    " candidate_location ='" + lscurrent_location + "'," +
                    " candidate_gender ='" + values.gender + "',";
               
              msSQL += " candidate_mobileno='" + Convert.ToDouble(values.mobilenumber).ToString() + "'," +
                            " candidateemail_address1='" + values.emailaddress1 + "' ," +
                            " candidateemail_address2 ='" + values.emailaddress1 + "'," +
                            " candidate_status='" + values.candidate_status + "' ," +
                            " experience='" + values.experience + "', " +
                            " current_company='" + values.currentcompany + "' ," +
                            " current_ctc='" + values.current_ctc + "' ," +
                            " designation='" + values.current_designation + "' ," +
                            " education ='" + values.qualification + "'," +
                            " candidate_remarks='" + values.remarks + "' ," +
                            " updated_by='" + employee_gid + "'," +
                            " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                            " where recruitmentadd_gid='" + values.recruitmentadd_gid + "' and" +
                            " jobposition_gid='" + lsjobposition_gid + "'";
                if (Convert.ToDateTime(values.dob).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
                {

                }
                else
                {
                    msSQL = "update rms_trn_trecruitmentadd set dob='" + Convert.ToDateTime(values.dob).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "'" +
                        " where recruitmentadd_gid='" + values.recruitmentadd_gid + "' and" +
                            " jobposition_gid='" + lsjobposition_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
      else if(values.candidate_status=="Interested")
            {
                if(lscandidate_status==values.candidate_status)
                {
                    values.message = "Duplicate Entry for this Job Code";
                    values.status = true;
                }
                else
                {
                   msSQL = " update rms_trn_trecruitmentadd set " +
                        " candidate_firstname='" + values.firstname + "', " +
                        " candidate_lastname='" + values.lastname + "' ," +
                        " candidate_location ='" + lscurrent_location + "'," +
                        " candidate_gender ='" + values.gender + "',";
                   
                  msSQL += " candidate_mobileno='" + Convert.ToDouble(values.mobilenumber).ToString() + "'," +
                            " candidateemail_address1='" + values.emailaddress1 + "' ," +
                            " candidateemail_address2 ='" + values.emailaddress1 + "'," +
                            " candidate_status='" + values.candidate_status + "' ," +
                            " experience='" + values.experience + "', " +
                            " current_company='" + values.currentcompany + "' ," +
                            " current_ctc='" + values.current_ctc + "' ," +
                            " designation='" + values.current_designation + "' ," +
                            " education ='" + values.qualification + "'," +
                            " candidate_remarks='" + values.remarks + "' ," +
                            " updated_by='" + employee_gid + "'," +
                            " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                            " where recruitmentadd_gid='" + values.recruitmentadd_gid + "' and" +
                            " jobposition_gid='" + lsjobposition_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (Convert.ToDateTime(values.dob).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
                    {

                    }
                    else
                    {
                        msSQL = "update rms_trn_trecruitmentadd set dob='" + Convert.ToDateTime(values.dob).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "'" +
                            " where recruitmentadd_gid='" + values.recruitmentadd_gid + "' and" +
                                " jobposition_gid='" + lsjobposition_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
            }
      
        }
        public void DaGetCloningSummary(MdlRmsTrnMyRecruitment values, string jobposition_gid,string employee_gid )
        {

            msSQL = " select concat(candidate_firstname,' ',candidate_lastname) as candidate_name,candidate_mobileno,candidateemail_address1,candidateemail_address2," +
                " candidate_status,experience,date_format(created_date,'%d-%m-%Y %H:%i %p') as created_date," +
                " date_format(updated_date,'%d-%m-%Y %h:%i %p') as updated_date,recruitmentadd_gid,recruiter_gid from " +
                " rms_trn_trecruitmentadd where recruiter_gid='" + employee_gid + "' " +
                " and jobposition_gid='" + jobposition_gid + "'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcandidate_list = new List<candidate_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcandidate_list.Add(new candidate_list
                    {
                        candidate_name = dr_datarow["candidate_name"].ToString(),
                        mobile_no = dr_datarow["candidate_mobileno"].ToString(),
                        emailaddress1 = (dr_datarow["candidateemail_address1"].ToString()),
                        emailaddress2 = (dr_datarow["candidateemail_address2"].ToString()),
                        candidate_status = (dr_datarow["candidate_status"].ToString()),
                        experience = (dr_datarow["experience"].ToString()),
                        recruitmentadd_gid = (dr_datarow["recruitmentadd_gid"].ToString()),
                        recruiter_gid = (dr_datarow["recruiter_gid"].ToString()),
                        updated_date = (dr_datarow["updated_date"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                    });
                }
                values.candidate_list = getcandidate_list;
            }
            dt_datatable.Dispose();
            values.status = true;
        }
        public void DaGetJobInfo(MdlRmsTrnMyRecruitment values, string jobposition_gid, string employee_gid)
        {

            msSQL = " select a.jobposition_gid,concat(job_code,'/',job_title) as jobcodetitle from rms_trn_tjobposition2recruiter a" +
                " left join rms_trn_tjobposition b on a.jobposition_gid=b.jobposition_gid where recruiter_gid='"+employee_gid+"'"+
                " and a.jobposition_gid not" +
                " in ('" + jobposition_gid+"') and b.jobcodefreeze_flag='N' group by a.jobposition_gid";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getjobcode_list = new List<jobcode_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getjobcode_list.Add(new jobcode_list
                    {
                        jobposition_gid = dr_datarow["jobposition_gid"].ToString(),
                        jobcodetitle = dr_datarow["jobcodetitle"].ToString(),
                       
                    });
                }
                values.jobcode_list = getjobcode_list;
            }
            dt_datatable.Dispose();
            values.status = true;
        }


        public void Dapostcloning(MdlCloning values, string employee_gid, result objResult)
        {


            var count = 0;
           
           for (var j = 0; j < values.jobcode_list.Count; j++)
            {

                msSQL = "select a.businessunit_gid,a.businessunit2team_gid from rms_trn_tjobposition2team a" +
           " left join rms_mst_tbusinessunit2team b on b.businessunit2team_gid=a.businessunit2team_gid" +
           " left join rms_trn_tbusinessunitteam2recruiter c on  b.businessunit2team_gid=c.businessunit2team_gid where" +
           " jobposition_gid='" + values.jobcode_list[j].jobposition_gid + "'" +
           " and c.employee_gid='" + employee_gid + "'";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lsbusinessunit_gid = objODBCDatareader["businessunit_gid"].ToString();
                    string lsbusinessunit2team_gid = objODBCDatareader["businessunit2team_gid"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = "select duplicate_validity,date_format(created_date ,'%Y-%m-%d') as created_date from rms_trn_tjobposition where " +
                    " jobposition_gid='" + values.jobcode_list[j].jobposition_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if(objODBCDatareader.HasRows==true)
                {
                    lsduplicate_validity = objODBCDatareader["duplicate_validity"].ToString();
                    string lscreated_date = objODBCDatareader["created_date"].ToString();
                }
                objODBCDatareader.Close();
                var recruit = 0;
            foreach (string i in values.recruitmentadd_gid)
            {
                    msSQL = "select recruitmentadd_gid,recruiter_gid,candidate_firstname,candidate_lastname,candidate_location,candidate_gender," +
                        " date_format(candidate_dob,'%Y-%m-%d')  as candidate_dob,candidate_mobileno,candidate_mobileno,candidateemail_address1," +
                        " candidateemail_address2,candidate_status,experience,current_company,current_ctc," +
                       " designation,education,candidate_remarks from rms_trn_trecruitmentadd where " +
                       " recruitmentadd_gid='" + i + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if(objODBCDatareader.HasRows==true)
                    {
                        msSQL = " select concat(candidate_firstname,' ',candidate_lastname) as candidate_name,candidate_status from rms_trn_trecruitmentadd" +
                                       " where jobposition_gid='" + values.jobcode_list[j].jobposition_gid + "' and " +
                                      " (candidate_mobileno='" + objODBCDatareader["candidate_mobileno"].ToString() + "' or " +
                                      " candidateemail_address1='" + objODBCDatareader["candidateemail_address1"].ToString() + "')";
                        objODBCDataReader1 = objdbconn.GetDataReader(msSQL);
                        if (objODBCDataReader1.HasRows == true)
                        {
                            if (objODBCDataReader1["candidate_status"].ToString() == objODBCDatareader["candidate_status"].ToString())
                            {
                                values.message = "Duplicate Entry for this Job Code";
                                values.status = false;
                                recruit = recruit + 1;
                            }
                            else
                            {
                                msgetGID = objcmnfunctions.GetMasterGID("RCAN");

                                DateTime created_date = Convert.ToDateTime(lscreated_date);
                                DateTime date = DateTime.UtcNow.Date;
                                var lsdate = (date - created_date).TotalDays.ToString();

                                long lsdiff_day = Convert.ToInt64(lsdate);
                                long lsduplicate_day = Convert.ToInt64(lsduplicate_validity);

                                if (lsdiff_day >= lsduplicate_day)
                                {
                                    msSQL = " insert into rms_trn_trecruitmentadd (" +
                                                             " recruitmentadd_gid, " +
                                                             " jobposition_gid ," +
                                                             " businessunit_gid ," +
                                                             " businessunit2team_gid ," +
                                                             " recruiter_gid ," +
                                                             " candidate_firstname, " +
                                                             " candidate_lastname ," +
                                                             " candidate_location ," +
                                                             " candidate_gender ," +
                                                             " candidate_dob ," +
                                                             " candidate_mobileno," +
                                                             " candidateemail_address1 ," +
                                                             " candidateemail_address2 ," +
                                                             " candidate_status ," +
                                                             " experience, " +
                                                             " current_company ," +
                                                             " current_ctc ," +
                                                             " designation ," +
                                                             " education ," +
                                                             " candidate_remarks ," +
                                                             " created_by, " +
                                                             " created_date ," +
                                                             " updated_by, " +
                                                             " updated_date )" +
                                                             " values (" +
                                                             "'" + msgetGID + "'," +
                                                             "'" + values.jobcode_list[j].jobposition_gid + "'," +
                                                             "'" + lsbusinessunit_gid + "'," +
                                                             "'" + lsbusinessunit2team_gid + "'," +
                                                             "'" + employee_gid + "'," +
                                                             "'" + objODBCDatareader["candidate_firstname"].ToString() + "'," +
                                                             "'" + objODBCDatareader["candidate_lastname"].ToString() + "'," +
                                                             "'" + objODBCDatareader["candidate_location"].ToString() + "'," +
                                                             "'" + objODBCDatareader["candidate_gender"].ToString() + "'," +
                                                             "'" + objODBCDatareader["candidate_dob"].ToString() + "'," +
                                                             "'" + objODBCDatareader["candidate_mobileno"].ToString() + "'," +
                                                             "'" + objODBCDatareader["candidateemail_address1"].ToString() + "'," +
                                                             "'" + objODBCDatareader["candidateemail_address2"].ToString() + "'," +
                                                             "'Interested'," +
                                                             "'" + objODBCDatareader["experience"].ToString() + "'," +
                                                             "'" + objODBCDatareader["current_company"].ToString() + "'," +
                                                             "'" + objODBCDatareader["current_ctc"].ToString() + "'," +
                                                             "'" + objODBCDatareader["designation"].ToString() + "'," +
                                                             "'" + objODBCDatareader["education"].ToString() + "'," +
                                                             "'" + objODBCDatareader["candidate_remarks"].ToString() + "'," +
                                                              "'" + employee_gid + "'," +
                                                             "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                                             "'" + employee_gid + "'," +
                                                             "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                }
                                else
                                {

                                    msSQL = " insert into rms_trn_trecruitmentadd (" +
                                              " recruitmentadd_gid, " +
                                              " jobposition_gid ," +
                                              " businessunit_gid ," +
                                              " businessunit2team_gid ," +
                                              " recruiter_gid ," +
                                              " candidate_firstname, " +
                                              " candidate_lastname ," +
                                              " candidate_location ," +
                                              " candidate_gender ," +
                                              " candidate_dob ," +
                                              " candidate_mobileno," +
                                              " candidateemail_address1 ," +
                                              " candidateemail_address2 ," +
                                              " candidate_status ," +
                                              " experience, " +
                                              " current_company ," +
                                              " current_ctc ," +
                                              " designation ," +
                                              " education ," +
                                              " candidate_remarks ," +
                                              " created_by, " +
                                              " created_date ," +
                                              " updated_by, " +
                                              " updated_date )" +
                                              " values (" +
                                              "'" + msgetGID + "'," +
                                              "'" + values.jobcode_list[j].jobposition_gid + "'," +
                                              "'" + lsbusinessunit_gid + "'," +
                                              "'" + lsbusinessunit2team_gid + "'," +
                                              "'" + employee_gid + "'," +
                                               "'" + objODBCDatareader["candidate_firstname"].ToString() + "'," +
                                                    "'" + objODBCDatareader["candidate_lastname"].ToString() + "'," +
                                                    "'" + objODBCDatareader["candidate_location"].ToString() + "'," +
                                                    "'" + objODBCDatareader["candidate_gender"].ToString() + "'," +
                                                    "'" + objODBCDatareader["candidate_dob"].ToString() + "'," +
                                                    "'" + objODBCDatareader["candidate_mobileno"].ToString() + "'," +
                                                    "'" + objODBCDatareader["candidateemail_address1"].ToString() + "'," +
                                                    "'" + objODBCDatareader["candidateemail_address2"].ToString() + "'," +
                                                    "'Interested'," +
                                                    "'" + objODBCDatareader["experience"].ToString() + "'," +
                                                    "'" + objODBCDatareader["current_company"].ToString() + "'," +
                                                    "'" + objODBCDatareader["current_ctc"].ToString() + "'," +
                                                    "'" + objODBCDatareader["designation"].ToString() + "'," +
                                                    "'" + objODBCDatareader["education"].ToString() + "'," +
                                                    "'" + objODBCDatareader["candidate_remarks"].ToString() + "'," +
                                                     "'" + employee_gid + "'," +
                                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                                    "'" + employee_gid + "'," +
                                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }
                            
                                objODBCDataReader1.Close();
                            }

                            //else
                            //{
                            //    msSQL = " insert into rms_trn_trecruitmentadd (" +
                            //                                                     " recruitmentadd_gid, " +
                            //                                                     " jobposition_gid ," +
                            //                                                     " businessunit_gid ," +
                            //                                                     " businessunit2team_gid ," +
                            //                                                     " recruiter_gid ," +
                            //                                                     " candidate_firstname, " +
                            //                                                     " candidate_lastname ," +
                            //                                                     " candidate_location ," +
                            //                                                     " candidate_gender ," +
                            //                                                     " candidate_dob ," +
                            //                                                     " candidate_mobileno," +
                            //                                                     " candidateemail_address1 ," +
                            //                                                     " candidateemail_address2 ," +
                            //                                                     " candidate_status ," +
                            //                                                     " experience, " +
                            //                                                     " current_company ," +
                            //                                                     " current_ctc ," +
                            //                                                     " designation ," +
                            //                                                     " education ," +
                            //                                                     " candidate_remarks ," +
                            //                                                     " created_by, " +
                            //                                                     " created_date ," +
                            //                                                     " updated_by, " +
                            //                                                     " updated_date )" +
                            //                                                     " values (" +
                            //                                                     "'" + msgetGID + "'," +
                            //                                                     "'" + values.jobcode_list[j].jobposition_gid + "'," +
                            //                                                     "'" + lsbusinessunit_gid + "'," +
                            //                                                     "'" + lsbusinessunit2team_gid + "'," +
                            //                                                     "'" +employee_gid + "'," +
                            //                                                     "'" + objODBCDatareader["candidate_firstname"].ToString() + "'," +
                            //                         "'" + objODBCDatareader["candidate_lastname"].ToString() + "'," +
                            //                         "'" + objODBCDatareader["candidate_location"].ToString() + "'," +
                            //                         "'" + objODBCDatareader["candidate_gender"].ToString() + "'," +
                            //                         "'" + objODBCDatareader["candidate_dob"].ToString() + "'," +
                            //                         "'" + objODBCDatareader["candidate_mobileno"].ToString() + "'," +
                            //                         "'" + objODBCDatareader["candidateemail_address1"].ToString() + "'," +
                            //                         "'" + objODBCDatareader["candidateemail_address2"].ToString() + "'," +
                            //                         "'Interested'," +
                            //                         "'" + objODBCDatareader["experience"].ToString() + "'," +
                            //                         "'" + objODBCDatareader["current_company"].ToString() + "'," +
                            //                         "'" + objODBCDatareader["current_ctc"].ToString() + "'," +
                            //                         "'" + objODBCDatareader["designation"].ToString() + "'," +
                            //                         "'" + objODBCDatareader["education"].ToString() + "'," +
                            //                         "'" + objODBCDatareader["candidate_remarks"].ToString() + "'," +
                            //                          "'" + employee_gid + "'," +
                            //                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                            //                         "'" + employee_gid + "'," +
                            //                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            //    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            //}                
                    }
                            }
                   

             
                recruit = recruit + 1;
            }
                count = count + 1;
            }

            if (count == 0)
            {
                values.message = "Select Atleast One";
                values.status = false;

            }
            else
            {
                values.message = "Cloning Updated Successfully";
                values.status = true;
            }
        }
    }
}