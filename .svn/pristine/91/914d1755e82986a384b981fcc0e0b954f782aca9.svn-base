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
    public class DaRmsTrnJob
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        DataTable dt_datatable;
        string msSQL;
        string lsspoc_name, lsspoc_gid, lscurval, MSJOBCODE, msGetGid,  lsjoblocation_gid, ls_gid, ls_location;
        string msgetassign2team_gid, msgetassign2employee_gid, lsbusinessunit2team_gid, lsbusinessunitteam_gid,lsrecruiter_gid, msgetteam2recruiter_gid;
        OdbcDataReader objODBCDataReader,objODBCDataReader1, objODBCDataReader2;
        int mnResult;
        string lsbusinessunitGID, lsbusinessunitname, lsjobLocation;
        

        public bool DaGetBusinessUnit(string employee_gid, BusinessUnitList values)
        {

            msSQL = " select distinct a.businessunit_gid,businessunit_name from rms_mst_tbusinessunit  a" +
                     " left join rms_trn_tbusinessunit2manager b on a.businessunit_gid=b.businessunit_gid" +
                     " left join rms_mst_tbusinessunit2team c on a.businessunit_gid=c.businessunit_gid" +
                     " left join rms_trn_tbusinessunit2spoc d on d.businessunit_gid=c.businessunit_gid " +
                     " where d.employee_gid='" + employee_gid + "' or b.employee_gid='" + employee_gid + "' or" +
                     " c.employee_gid='" + employee_gid + "' or a.created_by ='" + employee_gid + "'  order by businessunit_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getbusinessunit = new List<BusinessUnit>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {

                    getbusinessunit.Add(new BusinessUnit
                    {
                        businessunit_gid = dt["businessunit_gid"].ToString(),
                        businessunit_name = dt["businessunit_name"].ToString(),
                        

                    });
                }
                values.BusinessUnit = getbusinessunit;
            }
            dt_datatable.Dispose();

            return true;
        }

        public bool DaGetBusinessUnitteam(string businessunit_gid, BusinessUnitteamList values)
        {

            msSQL = " select businessunit2team_gid,businessunitteam_name from rms_mst_tbusinessunit2team where businessunit_gid ='" + businessunit_gid + "' order by businessunit_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getbusinessunitteam = new List<BusinessUnitteam>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {

                    getbusinessunitteam.Add(new BusinessUnitteam
                    {
                        businessunit2team_gid = dt["businessunit2team_gid"].ToString(),
                        businessunit2team_name = dt["businessunitteam_name"].ToString(),


                    });
                }
                values.BusinessUnitteam = getbusinessunitteam;
            }
            dt_datatable.Dispose();

            return true;
        }

        public bool DaGetAssignSPOC(string businessunit_gid,AssignSPOCList values)
        {

            msSQL = " select a.employee_gid,concat(c.user_code,' / ',c.user_firstname,' ',c.user_lastname)  as user_name from rms_trn_tbusinessunit2spoc a" +
                    " left join hrm_mst_temployee b on a.employee_gid=b.employee_gid" +
                    " left join adm_mst_tuser c on b.user_gid=c.user_gid where businessunit_gid='" + businessunit_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getspoc = new List<AssignSPOC>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {

                    getspoc.Add(new AssignSPOC
                    {
                        spoc_gid = dt["employee_gid"].ToString(),
                        spoc_name = dt["user_name"].ToString(),


                    });
                }
                values.AssignSPOC = getspoc;
            }
            dt_datatable.Dispose();

            return true;
        }

        public bool DaGetRecruiter(RecruiterList values)
        {
            for (var i = 0; i < values.Recruiter.Count; i++)
            {
                lsbusinessunit2team_gid += "'" + values.Recruiter[i].businessunit2team_gid + "',";

            }

            msSQL = "select a.employee_gid,concat(c.user_code,' / ',c.user_firstname,' ',c.user_lastname) as recruiter_name  from rms_trn_tbusinessunitteam2recruiter a" +
                    " left join  hrm_mst_temployee b on b.employee_gid=a.employee_gid" +
                    " left join adm_mst_tuser c on b.user_gid=c.user_gid where businessunit2team_gid in (" + lsbusinessunit2team_gid.TrimEnd(',') + ")";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getrecruiter = new List<Recruiter>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {

                    getrecruiter.Add(new Recruiter
                    {
                        recruiter_gid = dt["employee_gid"].ToString(),
                        recruiter_name = dt["recruiter_name"].ToString(),


                    });
                }
                values.Recruiter = getrecruiter;
            }
            dt_datatable.Dispose();

            return true;
        }

        public bool DaGetJobLocation(JobLocationList values)
        {

            msSQL = "select joblocation_gid, joblocation_name from rms_trn_tjoblocationcandidate";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getlocation = new List<Joblocation>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {

                    getlocation.Add(new Joblocation
                    {
                        joblocation_gid = dt["joblocation_gid"].ToString(),
                        joblocation_name = dt["joblocation_name"].ToString(),


                    });
                }
                values.Joblocation = getlocation;
            }
            dt_datatable.Dispose();

            return true;
        }

        public bool DaGetJobDetails(JobDetails values,string jobposition_gid)
        {

            msSQL = " Select  job_code, job_title, company_name, domain, website, job_location, " +
           " noof_position, education_qualification, ctcbudget_min,ctcbudget_max,spoc_name," +
           " notice_period, experience_min,experience_max, mandatroy_skills, search_location,duplicate_validity, job_description from rms_trn_tjobposition a  " +
           " left join rms_trn_tjobposition2team b on a.jobposition_gid = b.jobposition_gid  " +
           " WHERE a.jobposition_gid = '" + jobposition_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows)
            {
                values.jobcode = objODBCDataReader["job_code"].ToString();
                values.job_type = objODBCDataReader["job_title"].ToString();
                values.company_name = objODBCDataReader["company_name"].ToString();
                values.website = objODBCDataReader["website"].ToString();
                values.assign_spoc = objODBCDataReader["spoc_name"].ToString();
                values.domain_name = objODBCDataReader["domain"].ToString();
                values.joblocation = objODBCDataReader["job_location"].ToString();
                values.noof_position = objODBCDataReader["noof_position"].ToString();
                values.skills = objODBCDataReader["mandatroy_skills"].ToString();
                values.search_location = objODBCDataReader["search_location"].ToString();
                values.education_qualification = objODBCDataReader["education_qualification"].ToString();
                values.experiencemax = objODBCDataReader["experience_max"].ToString();
                values.experience = objODBCDataReader["experience_min"].ToString();
                values.ctcbudget_min = objODBCDataReader["ctcbudget_min"].ToString();
                values.ctcbudget_max = objODBCDataReader["ctcbudget_max"].ToString();
                values.notice_period = objODBCDataReader["notice_period"].ToString();
                values.duplicate_validity = objODBCDataReader["duplicate_validity"].ToString();
                values.jobdescription = objODBCDataReader["job_description"].ToString();

            }
            return true;
        }

        public bool DaGetJobRecruiterFreezeDetails(JobRecruiterFreezeDetails values)
        {

            msSQL = "select concat(job_code,'/',job_title) as job_title from rms_trn_tjobposition where jobposition_gid = '" + values.jobposition_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows)
            {
                values.jobPosition = objODBCDataReader["job_title"].ToString();
               
            }
            msSQL = " select concat(a.user_firstname,'/',a.user_lastname) as user_firstname from adm_mst_tuser a " +
                    " left join hrm_mst_temployee b on a.user_gid=b.user_gid " +
                    " where b.employee_gid='" + values.recruiter_gid + "' ";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows)
            {
                values.recruiter = objODBCDataReader["user_firstname"].ToString();

            }
            return true;
        }

        public bool DaGetJobCodeFreezeDetails(JobFreezeDetails values, string jobposition_gid)
        {

            msSQL = "select concat(job_code,'/',job_title) as job_title from rms_trn_tjobposition where jobposition_gid= '" + jobposition_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows)
            {
                values.jobPosition = objODBCDataReader["job_title"].ToString();

            }
            
            return true;
        }

        public void DaGetJobUpdate( string employee_gid, JobDetails values)
        {
            msSQL = "select joblocation_gid,joblocation_name from rms_trn_tjoblocation where joblocation_gid = '" + values.joblocation.Replace("'", "") + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows)
            {
                lsjobLocation = objODBCDataReader["joblocation_name"].ToString();                

            }

            msSQL = " UPDATE rms_trn_tjobposition " +
                    " SET job_code = '" + values.jobcode.Replace("'", "") + "'," +
                    " job_title = '" + values.job_type.Replace("'", "") + "'," +
                    " company_name = '" + values.company_name.Replace("'", "") + "'," +
                    " domain = '" + values.domain_name.Replace("'", "") + "'," +
                    " website = '" + values.website.Replace("'", "") + "'," +
                    " job_location = '" + lsjobLocation + "'," +
                    " noof_position = '" + values.noof_position.Replace("'", "") + "',";
            if((values.skills=="") ||(values.skills==null))
            {
                msSQL += " mandatroy_skills = '',";
            }
            else
            {
                msSQL+=" mandatroy_skills = '" + values.skills.Replace("'", "") + "',";
            }

            msSQL += " education_qualification = '" + values.education_qualification.Replace("'", "") + "'," +
                     " experience_max = '" + values.experiencemax.Replace("'", "") + "'," +
                     " experience_min ='" + values.experience.Replace("'", "") + "'," +
                     " ctcbudget_min = '" + values.ctcbudget_min.Replace("'", "") + "'," +
                     " ctcbudget_max ='" + values.ctcbudget_max.Replace("'", "") + "'," +
                     " notice_period = '" + values.notice_period.Replace("'", "") + "'," +
                     " duplicate_validity = '" + values.duplicate_validity.Replace("'", "") + "'," +
                     " search_location = '" + values.search_location.Replace("'", "") + "',";
            if ((values.jobdescription == "") || (values.jobdescription == null))
            {
                msSQL += " job_description = '',";
            }
            else
            {
                msSQL += " job_description = '" + values.jobdescription.Replace("'", "") + "',";
            }
            msSQL+=" WHERE jobposition_gid = '" + values.jobposition_gid+ "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                values.message = "Job Code Updated Successfully";
                values.status = true;
                
            }
            else
            {
                values.message = " Error Occured While Updating Job Code";
                values.status = false;

            }
        }

        public void DaGetJobRecruiterFreeze(string employee_gid, JobRecruiterFreeze values)
        {

            msSQL = " update rms_trn_tjobposition2recruiter set freeze_flag='Y',freezed_by='" + employee_gid + "'," +
                    " recruiterfreezed_remarks ='" + values.recruiter_freezeremarks + "' where jobposition_gid='" + values.jobposition_gid + "'" +
                    " and recruiter_gid='" + values.recruiter_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                values.message = "Recruiter Freezed Successfully";
                values.status = true;

            }
            else
            {
                values.message = " Error Occured While Freezing Recruiter";
                values.status = false;

            }
        }

        public void DaGetJobCodeFreeze(string employee_gid, JobcodeFreeze values)
        {

            msSQL = " update rms_trn_tjobposition set jobcodefreeze_flag='Y',jobcodefreezed_by='" + employee_gid + "'," +
                    " jobcodefreezed_remarks ='" + values.code_freezeremarks + "' where jobposition_gid='" + values.jobposition_gid + "'";
              
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                values.message = "Job Code Freezed Successfully";
                values.status = true;

            }
            else
            {
                values.message = " Error Occured While Freezing Job Code";
                values.status = false;

            }
        }

        public void DaGetJobCodeUnFreeze(string employee_gid, JobcodeFreeze values)
        {

            msSQL = " update rms_trn_tjobposition set jobcodefreeze_flag='N', " +
                    " jobcodeunfreezed_by='" + employee_gid +"' where jobposition_gid='" + values.jobposition_gid + "'";
            
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                values.message = "Job Code UnFreezed Successfully";
                values.status = true;

            }
            else
            {
                values.message = " Error Occured While Freezing Job Code";
                values.status = false;

            }
        }

        public void DaGetRecruiterUnFreeze(string employee_gid, JobRecruiterFreeze values)
        {

            msSQL = " update rms_trn_tjobposition2recruiter set freeze_flag='N',freezed_by='" + employee_gid + "' where jobposition_gid='" + values.jobposition_gid + "'" +
                " and recruiter_gid='" + values.recruiter_gid +"'";
            
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                values.message = "Recruiter UnFreezed Successfully";
                values.status = true;

            }
            else
            {
                values.message = " Error Occured While Freezing Recruiter";
                values.status = false;

            }
        }

        public bool DaGetJobSummary(string employee_gid, JobList values)
        {
             
            msSQL = "  select distinct a.jobposition_gid,c.jobposition2team_gid,d.businessunit_gid,b.businessunit2team_gid,company_name,job_code,job_title,spoc_name,noof_position," +
                " date_format(a.created_date,'%d-%m-%Y') as created_date,businessunit_name,transfer_flag,jobcodefreeze_flag,transfer_from,transfer_to," +
                " case when transfer_flag ='Y' then concat(transfer_from_name ,' Shared Job Code To<b> ',transfer_to_name)" +
                "  when   jobcodefreeze_flag ='Y' then  concat(jobcodefreezed_by,'was freezed the Job Code For the reason is',jobcodefreezed_remarks)" +
                " else '---' end  sharing  from rms_trn_tjobposition  a" +
                " left join rms_trn_tjobposition2unit d on a.jobposition_gid=d.jobposition_gid" +
                " left join rms_trn_tjobposition2team c on a.jobposition_gid=c.jobposition_gid" +
                " left join rms_mst_tbusinessunit2team b on c.businessunit2team_gid=b.businessunit2team_gid " +
                " left join rms_mst_tbusinessunit h on d.businessunit_gid=h.businessunit_gid" +
                " where  a.created_by='" + employee_gid + "'  group by a.jobposition_gid  order by jobposition_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getjobsummary = new List<jobsummary>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {

                    getjobsummary.Add(new jobsummary
                    {
                        jobposition_gid = dt["jobposition_gid"].ToString(),
                        jobposition2team_gid = dt["jobposition2team_gid"].ToString(),
                        businessunit_gid = dt["businessunit_gid"].ToString(),
                        businessunit2team_gid = dt["businessunit2team_gid"].ToString(),
                        company_name = dt["company_name"].ToString(),
                        job_code = dt["job_code"].ToString(),
                        job_title = dt["job_title"].ToString(),
                        spoc_name = dt["spoc_name"].ToString(),
                        noof_position = dt["noof_position"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        transfer_flag = dt["transfer_flag"].ToString(),
                        jobcodefreeze_flag = dt["jobcodefreeze_flag"].ToString(),
                        transfer_from = dt["transfer_from"].ToString(),
                        transfer_to = dt["transfer_to"].ToString(),
                        sharing = dt["sharing"].ToString(),
                        businessunit_name = dt["businessunit_name"].ToString(),  
                    });

                }
                values.jobsummary = getjobsummary;
                values.status = true;
            }
            else
            {
                values.status = false;
                values.message = "No Records Found";
            }
            dt_datatable.Dispose();
            return true;
        }

        public bool DaGetTodayInterviewSummary(string employee_gid, TodayInterviewList values)
        {
            msSQL = "select distinct count(interview_gid) as today_count,g.businessunit_name,c.job_code,c.job_title,concat(user_firstname,' ',user_lastname) as recruiter_name,concat(candidate_firstname,' ',candidate_lastname)" +
              " as candidate_name,candidate_mobileno,candidate_dob,experience,interviewscheduled_time,interview_gid,c.company_name,businessunitteam_name from rms_trn_tinterviewdetails a" +
              " left join rms_trn_trecruitmentadd b on a.recruitmentadd_gid=b.recruitmentadd_gid" +
              " left join rms_trn_tjobposition c on b.jobposition_gid=c.jobposition_gid" +
              " left join rms_trn_tbusinessunit2manager d on d.employee_gid=c.jobposition2spoc_gid" +
              " left join hrm_mst_temployee e on b.recruiter_gid=e.employee_gid" +
              " left join adm_mst_tuser f on e.user_gid=f.user_gid" +
              " left join rms_mst_tbusinessunit g on b.businessunit_gid=g.businessunit_gid" +
              " left join rms_mst_tbusinessunit2team h on h.businessunit2team_gid=b.businessunit2team_gid" +
              " where (c.jobposition2spoc_gid='" + employee_gid + "'" +
              " and interview_date='" + DateTime.Now.ToString("yyyy-MM-dd") + "') group by a.jobposition_gid";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getjobsummary = new List<TodayInterviewSummary>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {

                    getjobsummary.Add(new TodayInterviewSummary
                    {
                        today_count = dt["today_count"].ToString(),                       
                        businessunit_name = dt["businessunit_name"].ToString(),
                        job_code = dt["job_code"].ToString(),
                        job_title = dt["job_title"].ToString(),
                        recruiter_name = dt["recruiter_name"].ToString(),
                        candidate_name = dt["candidate_name"].ToString(),
                        candidate_mobileno = dt["candidate_mobileno"].ToString(),
                        candidate_dob = dt["candidate_dob"].ToString(),
                        experience = dt["experience"].ToString(),
                        interviewscheduled_time = dt["interviewscheduled_time"].ToString(),
                        interview_gid = dt["interview_gid"].ToString(),
                        company_name = dt["company_name"].ToString(),
                        businessunitteam_name = dt["businessunitteam_name"].ToString(),

                    });
                }
                values.TodayInterviewSummary = getjobsummary;
                values.status = true;
                
            }
            else
            {
                values.status = false;
                values.message = "No Records Found";
            }
            dt_datatable.Dispose();
            return true;
        }

        public bool DaGetTodaySummary(string employee_gid, InterviewList values)
        {
            msSQL = "select distinct g.businessunit_name,c.job_code,c.job_title,concat(user_firstname,' ',user_lastname) as recruiter_name,concat(candidate_firstname,' ',candidate_lastname)" +
              " as candidate_name,candidate_mobileno,candidate_dob,experience,interviewscheduled_time,interview_gid,c.company_name,businessunitteam_name from rms_trn_tinterviewdetails a" +
              " left join rms_trn_trecruitmentadd b on a.recruitmentadd_gid=b.recruitmentadd_gid" +
              " left join rms_trn_tjobposition c on b.jobposition_gid=c.jobposition_gid" +
              " left join rms_trn_tbusinessunit2manager d on d.employee_gid=c.jobposition2spoc_gid" +
              " left join hrm_mst_temployee e on b.recruiter_gid=e.employee_gid" +
              " left join adm_mst_tuser f on e.user_gid=f.user_gid" +
              " left join rms_mst_tbusinessunit g on b.businessunit_gid=g.businessunit_gid" +
              " left join rms_mst_tbusinessunit2team h on h.businessunit2team_gid=b.businessunit2team_gid" +
              " where ( c.jobposition2spoc_gid='" + employee_gid + "'" +
              " and interview_date='" + DateTime.Now.ToString("yyyy-MM-dd") + "' ) order by a.jobposition_gid ,h.businessunit2team_gid asc";
              

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var geinterviewsummary = new List<InterviewSummary>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {

                    geinterviewsummary.Add(new InterviewSummary
                    {
                   
                        businessunit_name = dt["businessunit_name"].ToString(),
                        job_code = dt["job_code"].ToString(),
                        job_title = dt["job_title"].ToString(),
                        recruiter_name = dt["recruiter_name"].ToString(),
                        candidate_name = dt["candidate_name"].ToString(),
                        candidate_mobileno = dt["candidate_mobileno"].ToString(),
                        candidate_dob = dt["candidate_dob"].ToString(),
                        experience = dt["experience"].ToString(),
                        interviewscheduled_time = dt["interviewscheduled_time"].ToString(),
                        interview_gid = dt["interview_gid"].ToString(),
                        company_name = dt["company_name"].ToString(),
                        businessunitteam_name = dt["businessunitteam_name"].ToString(),

                    });
                }
                values.InterviewSummary = geinterviewsummary;
                values.status = true;

            }
            else
            {
                values.status = false;
                values.message = "No Records Found";
            }
            dt_datatable.Dispose();
            return true;
        }

        public bool DaGetAssignSPOCSummary(string employee_gid,string jobposition_gid, AssignSPOCSummaryList values)
        {
            msSQL = " select a.employee_gid,businessunit_name,businessunitteam_name,concat(f.user_code,' / ',f.user_firstname,' ',f.user_lastname) as user_name from rms_trn_tbusinessunitteam2recruiter a" +
                " left join rms_mst_tbusinessunit2team b on a.businessunit2team_gid=b.businessunit2team_gid" +
                " left join rms_mst_tbusinessunit c on b.businessunit_gid=c.businessunit_gid" +
                " left join rms_trn_tbusinessunit2manager d on c.businessunit_gid=d.businessunit_gid" +
                " left join rms_trn_tbusinessunit2spoc g on c.businessunit_gid=g.businessunit_gid " +
                " left join hrm_mst_temployee e on e.employee_gid=a.employee_gid" +
                " left join adm_mst_tuser f on e.user_gid=f.user_gid " +
                " where a.employee_gid not in (select recruiter_gid from rms_trn_tjobposition2recruiter where jobposition_gid='" + jobposition_gid + "')" +
                " and (b.employee_gid='" + employee_gid + "' or d.employee_gid='" + employee_gid + "' or g.employee_gid='" + employee_gid + "') group by a.employee_gid";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getassignspocsummary = new List<assignSPOCsummary>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {

                    getassignspocsummary.Add(new assignSPOCsummary
                    {
                        employee_gid = dt["employee_gid"].ToString(),
                        employee_name = dt["user_name"].ToString(),
                        businessunit_name = dt["businessunit_name"].ToString(),
                        businessunitteam_name = dt["businessunitteam_name"].ToString(),                      

                    });
                }
                values.assignSPOCsummary = getassignspocsummary;
                values.status = true;
                
            }
            else
            {
                values.status = false;
                values.message = "No Records Found";
            }
            dt_datatable.Dispose();

            return true;
        }

        public bool DaGetsearchAssignSPOCSummary(string employee_gid, SearchAssignSPOCSummaryList values)
        {           
            for (var i = 0; i < values.searchbusinessunitteam.Count; i++)
            {
                lsbusinessunitteam_gid += "'" + values.searchbusinessunitteam[i].businessunit2team_gid + "',";

            }

            msSQL = "select distinct a.employee_gid,businessunit_name,businessunitteam_name,concat(f.user_code,' / ',f.user_firstname,' ',f.user_lastname) as user_name from rms_trn_tbusinessunitteam2recruiter a" +
                " left join rms_mst_tbusinessunit2team b on a.businessunit2team_gid=b.businessunit2team_gid" +
                " left join rms_mst_tbusinessunit c on b.businessunit_gid=c.businessunit_gid" +
                " left join rms_trn_tbusinessunit2manager d on c.businessunit_gid=d.businessunit_gid" +
                " left join hrm_mst_temployee e on e.employee_gid=a.employee_gid" +
                " left join adm_mst_tuser f on e.user_gid=f.user_gid " +
                " where a.businessunit2team_gid in (" + lsbusinessunitteam_gid.TrimEnd(',') + ") and b.businessunit_gid='" + values.businessunit_gid  + "' and  a.employee_gid not in " +
                " (select recruiter_gid from rms_trn_tjobposition2recruiter where jobposition_gid='" + values.jobposition_gid + "' and" +
                " businessunit2team_gid in (" + lsbusinessunitteam_gid.TrimEnd(',') + "))";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getsearchassignspocsummary = new List<searchassignSPOCsummary>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getsearchassignspocsummary.Add(new searchassignSPOCsummary
                    {
                        employee_gid = dt["employee_gid"].ToString(),
                        employee_name = dt["user_name"].ToString(),
                        businessunit_name = dt["businessunit_name"].ToString(),
                        businessunitteam_name = dt["businessunitteam_name"].ToString(),
                    });
                }
                values.searchassignSPOCsummary = getsearchassignspocsummary;
                values.status = true;
                
            }
            else
            {
                values.status = false;
                values.message = "No Records Found";
            }
            dt_datatable.Dispose();
          
            return true;
        }

        public bool DaGetShareTeamSummary( string jobposition_gid, ShareTeamSummaryList values)
        {
            msSQL = " select distinct a.businessunit_gid,a.businessunit_name,concat(d.user_code,' / ',d.user_firstname,' ',d.user_lastname) as businessunit_manager" +
               " from rms_mst_tbusinessunit a" +
               " left join rms_trn_tbusinessunit2manager b on a.businessunit_gid=b.businessunit_gid" +
               " left join hrm_mst_temployee c on b.employee_gid=c.employee_gid" +
               " left join adm_mst_tuser d on c.user_gid=d.user_gid where a.businessunit_gid not in( select businessunit_gid from rms_trn_tjobposition2unit" +
               " where jobposition_gid='" + jobposition_gid + "') group by a.businessunit_name";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getshareTeamsummary = new List<ShareTeamsummary>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {

                    getshareTeamsummary.Add(new ShareTeamsummary
                    {
                        businessunit_gid = dt["businessunit_gid"].ToString(),                      
                        businessunit_name = dt["businessunit_name"].ToString(),
                        businessunit_manager = dt["businessunit_manager"].ToString(),

                    });
                }
                values.ShareTeamsummary = getshareTeamsummary;
                values.status = true;
            }
            else
            {
                values.status = false;
                values.message = "No Records Found";
            }
            dt_datatable.Dispose();
           
            return true;
        }

        public bool DaGetinlineRecruiterSummary(string jobposition_gid, inlinerecruiterList values)
        {
            msSQL = " select a.jobposition_gid,a.recruiter_gid,a.freeze_flag,c.businessunitteam_name,d.businessunit_name," +
                      "  concat(f.user_code,' / ',f.user_firstname,' ',f.user_lastname)  as recruiter_name from rms_trn_tjobposition2recruiter a" +
                      "  left join rms_trn_tjobposition2team b on a.jobposition_gid=b.jobposition_gid" +
                      "  left join rms_mst_tbusinessunit2team c on a.businessunit2team_gid=c.businessunit2team_gid" +
                      "  left join rms_mst_tbusinessunit d on c.businessunit_gid=d.businessunit_gid" +
                      "  left join hrm_mst_temployee e on a.recruiter_gid=e.employee_gid" +
                      "  left join adm_mst_tuser f on e.user_gid=f.user_gid" +
                        " where a.jobposition_gid='" + jobposition_gid + "' group by a.recruiter_gid order by a.businessunit2team_gid asc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getinlinerecruiterSummary = new List<inlinerecruiterSummary>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {

                    getinlinerecruiterSummary.Add(new inlinerecruiterSummary
                    {
                        jobposition_gid = dt["jobposition_gid"].ToString(),
                        recruiter_gid = dt["recruiter_gid"].ToString(),
                        businessunitteam_name = dt["businessunitteam_name"].ToString(),
                        freeze_flag = dt["freeze_flag"].ToString(),
                        businessunit_name = dt["businessunit_name"].ToString(),
                        recruiter_name = dt["recruiter_name"].ToString(),

                    });
                }
                values.inlinerecruiterSummary = getinlinerecruiterSummary;
                values.status = true;
            }
            else
            {
                values.status = false;
                values.message = "No Records Found";
            }
            
            dt_datatable.Dispose();            

            return true;
        }

        public bool DGetSPOCvalidation(string employee_gid, JobList values)
        {
            msSQL = "select businessunit_gid from rms_trn_tbusinessunit2spoc where employee_gid='" + employee_gid + "'";
           string lsbusinessunit_gid = objdbconn.GetExecuteScalar(msSQL);
            if(lsbusinessunit_gid == "")
            {

                values.spocvalidation = "false";
            }
            else
            {
                msSQL = "select concat(b.user_firstname,' ',b.user_lastname) as user_name from hrm_mst_temployee a" +
                        " left join adm_mst_tuser b on a.user_gid=b.user_gid where employee_gid='" + employee_gid + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if(objODBCDataReader.HasRows==true)
                {
                    values.lsuser_name = objODBCDataReader["user_name"].ToString();
                    values.spocvalidation = "True";
                }
                objODBCDataReader.Close();
            }
           
            return true;
        }

        public bool DaGetBusinessUnitNameDetails(string jobposition_gid, businessunitteamList values)
        {
          msSQL = " select b.user_gid,b.employee_gid,businessunitteam_name " +
                 " from rms_trn_tjobposition2team a " +
                 " left join rms_mst_tbusinessunit2team d on a.businessunit2team_gid=d.businessunit2team_gid " +
                 " left join hrm_mst_temployee b on d.employee_gid=b.employee_gid " +
                 " left join adm_mst_tuser c on b.user_gid=c.user_gid " +
                 "  where a.jobposition_gid='" + jobposition_gid + "' group by d.businessunit2team_gid";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getbusinessunitteam = new List<businessunitteamdetail>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {

                    getbusinessunitteam.Add(new businessunitteamdetail
                    {
                        user_gid = dt["user_gid"].ToString(),
                        businessunitteam_name = dt["businessunitteam_name"].ToString(),
                        employee_gid = dt["employee_gid"].ToString(),

                    });
                }
                values.businessunitteamdetail = getbusinessunitteam;
                values.status = true;
            }
            else
            {
                values.status = false;
            }
            
            dt_datatable.Dispose();

            return true;
        }

        public bool DaGetSharingDetails(string jobposition_gid, sharingdetailList values)
        {
            msSQL = " select jobposition_gid,case when transfer_flag ='Y' then concat(transfer_from_name ,' Shared Job Code To ',transfer_to_name)" +
                 " when jobcodefreeze_flag ='Y' then concat(c.user_firstname,' ',c.user_lastname,' ','was freezed the Job Code for the reason is',' ',jobcodefreezed_remarks)" +
                 " else '---' end  sharing from rms_trn_tjobposition a" +
                 " left join hrm_mst_temployee b on a.jobcodefreezed_by=b.employee_gid" +
                 " left join adm_mst_tuser c on b.user_gid=c.user_gid where jobposition_gid='" + jobposition_gid + "' group by jobposition_gid";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getsharingdtl = new List<sharingdetail>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {

                    getsharingdtl.Add(new sharingdetail
                    {
                        
                        sharing = dt["sharing"].ToString(),                       

                    });
                }
                values.sharingdetail = getsharingdtl;
                values.status = true;
            }
            else
            {
                values.status = false;
            }
            dt_datatable.Dispose();
            return true;
        }

        //public bool DaGetColorDetails(string employee_gid,colordetailList values)
        //{
        //    msSQL = "select businessunit_gid from rms_trn_tbusinessunit2manager where employee_gid='" + employee_gid + "'";
        //    dt_datatable = objdbconn.GetDataTable(msSQL);
        //    var getsharingdtl = new List<colordetail>();
        //    if (dt_datatable.Rows.Count != 0)
        //    {
        //        foreach (DataRow dt in dt_datatable.Rows)
        //        {

        //            getsharingdtl.Add(new colordetail
        //            {

        //                sharing = dt["sharing"].ToString(),

        //            });
        //        }
        //        values.colordetail = getsharingdtl;
        //    }
        //    dt_datatable.Dispose();

        //    return true;
        //}

        public bool DaGetUnAssignSPOCSummary( string jobposition_gid, UnAssignSPOCSummaryList values)
        {
            msSQL = " select a.jobposition_gid,a.recruiter_gid,e.employee_gid,c.businessunitteam_name,f.user_code,d.businessunit_name," +
                      "  concat(f.user_firstname,' ',f.user_lastname)  as user_name from rms_trn_tjobposition2recruiter a" +
                      "  left join rms_trn_tjobposition2team b on a.jobposition_gid=b.jobposition_gid" +
                      "  left join rms_mst_tbusinessunit2team c on a.businessunit2team_gid=c.businessunit2team_gid" +
                      "  left join rms_mst_tbusinessunit d on c.businessunit_gid=d.businessunit_gid" +
                      "  left join hrm_mst_temployee e on a.recruiter_gid=e.employee_gid" +
                      "  left join adm_mst_tuser f on e.user_gid=f.user_gid" +
                        " where a.jobposition_gid='" + jobposition_gid + "' group by a.recruiter_gid ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getUnassignspocsummary = new List<UnassignSPOCsummary>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {

                    getUnassignspocsummary.Add(new UnassignSPOCsummary
                    {
                        recruiter_gid = dt["recruiter_gid"].ToString(),
                        employee_gid = dt["employee_gid"].ToString(),
                        employee_name = dt["user_name"].ToString(),
                        employee_code = dt["user_code"].ToString(),
                        businessunit_name = dt["businessunit_name"].ToString(),
                        businessunitteam_name = dt["businessunitteam_name"].ToString(),

                    });
                }
                values.UnassignSPOCsummary = getUnassignspocsummary;
                values.status = true;
            }
            dt_datatable.Dispose();
            values.status = false;
            values.message = "No Records Found";
            return true;
        }

        public void DaPostAssignSPOC(AddAssignSPOC values, string employee_gid, result objResult)
        {
            

            var count = 0;
            foreach (string i in values.employee_gid)
            {
                msSQL = " select distinct b.businessunit_gid,b.businessunit2team_gid,a.employee_gid,businessunit_name,businessunitteam_name," +
                    " concat(f.user_code,' / ',f.user_firstname,' ',f.user_lastname) as user_name from rms_trn_tbusinessunitteam2recruiter a" +
                " left join rms_mst_tbusinessunit2team b on a.businessunit2team_gid=b.businessunit2team_gid" +
                " left join rms_mst_tbusinessunit c on b.businessunit_gid=c.businessunit_gid" +
                " left join rms_trn_tbusinessunit2manager d on c.businessunit_gid=d.businessunit_gid" +
                " left join hrm_mst_temployee e on e.employee_gid=a.employee_gid" +
                " left join adm_mst_tuser f on e.user_gid=f.user_gid " +
                        " where  a.employee_gid='" + i + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows)
                {
                    string lsbusinessunit2eam_gid = objODBCDataReader["businessunit2team_gid"].ToString();
                    string lsbusinessunit_gid = objODBCDataReader["businessunit_gid"].ToString();
                    msSQL = "select businessunit_gid from rms_trn_tjobposition2unit where jobposition_gid='" + values.jobposition_gid + "'" +
                             " and businessunit_gid='" + lsbusinessunit_gid + "'";
                    objODBCDataReader1 = objdbconn.GetDataReader(msSQL);
                    if (objODBCDataReader1.HasRows == false)
                    {
                        msgetassign2team_gid = objcmnfunctions.GetMasterGID("JCTU");
                        msSQL = " insert into rms_trn_tjobposition2unit ( " +
                                " jobposition2unit_gid," +
                                " jobposition_gid," +
                                " businessunit_gid ," +
                                " created_by," +
                                " created_date" +
                                " ) Values ( " +
                                " '" + msgetassign2team_gid + "', " +
                                 "'" + values.jobposition_gid + "'," +
                                "'" + values.businessunit_gid + "', " +
                                "'" + employee_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        
                    }

                        msSQL = "select businessunit2team_gid,employee_gid from rms_trn_tjobposition2team where jobposition_gid='" + values.jobposition_gid + "'" +
                             " and businessunit2team_gid='" + lsbusinessunit2eam_gid + "'";
                    objODBCDataReader2 = objdbconn.GetDataReader(msSQL);
                    if (objODBCDataReader2.HasRows == false)
                    {
                        msgetassign2team_gid = objcmnfunctions.GetMasterGID("JCTE");
                        msSQL = " insert into rms_trn_tjobposition2team ( " +
                                " jobposition2team_gid," +
                                " jobposition_gid," +
                                " businessunit_gid ," +
                                " businessunit2team_gid ," +
                                " employee_gid ," +
                                " created_by," +
                                " created_date" +
                                " ) Values ( " +
                                " '" + msgetassign2team_gid + "', " +
                                "'" + values.jobposition_gid + "'," +
                                "'" + objODBCDataReader["businessunit_gid"].ToString() + "', " +
                                "'" + objODBCDataReader["businessunit2team_gid"].ToString() + "'," +
                                "'" + objODBCDataReader["employee_gid"].ToString() + "'," +
                                "'" + employee_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    msgetassign2employee_gid = objcmnfunctions.GetMasterGID("JCTR");
                            msSQL = " insert into rms_trn_tjobposition2recruiter ( " +
                                    " jobposition2recruiter_gid," +
                                    " jobposition_gid," +
                                    " recruiter_gid," +
                                    " businessunit2team_gid," +
                                    " created_by," +
                                    " created_date" +
                                    " ) Values ( " +
                                    " '" + msgetassign2employee_gid + "', " +
                                    "'" + values.jobposition_gid + "'," +
                                    "'" + objODBCDataReader["employee_gid"].ToString() + "'," +
                                    "'" + objODBCDataReader["businessunit2team_gid"].ToString() + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                          
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
                values.message = "SPOC Assigned  Successfully";
                values.status = true;
            }
        }

        public void DaPostUnAssignSPOC(AddUnAssignSPOC values, string employee_gid, result objResult)
        {
            

            var count = 0;
            foreach (string i in values.employee_gid)
            {
                msSQL = " Delete from rms_trn_tjobposition2recruiter where recruiter_gid='" + i +"' " +
                       "  and jobposition_gid='" + values.jobposition_gid +"'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                count = count + 1;
            }



            if (count == 0)
            {
                values.message = "Select Atleast One";
                values.status = false;

            }
            else
            {
                values.message = "SPOC UnAssigned  Successfully";
                values.status = true;

            }
        }

        public void DaPostShareTeam(AddShareTeam values, string employee_gid, result objResult)
        {
            
            msSQL = "select distinct a.businessunit_gid,businessunit_name from rms_trn_tjobposition2unit a" +
                    " left join rms_mst_tbusinessunit b on a.businessunit_gid=b.businessunit_gid where jobposition_gid='" + values.jobposition_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows)
            {
                lsbusinessunitGID= objODBCDataReader["businessunit_gid"].ToString();
                lsbusinessunitname = objODBCDataReader["businessunit_name"].ToString();

            }

            msSQL = " update rms_trn_tjobposition set transfer_flag='Y' ," +
            " transfer_from='" + lsbusinessunitGID + "'," +
            " transfer_from_name ='" + lsbusinessunitname + "'where jobposition_gid='" + values.jobposition_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            var count = 0;
            foreach (string i in values.businessunit_gid)
            {

                msSQL = " insert into rms_trn_tjobposition2unit ( " +
                        " jobposition2unit_gid," +
                        " jobposition_gid," +
                        " businessunit_gid ," +
                        " created_by," +
                        " created_date" +
                        " ) Values ( " +
                        " '" + msgetassign2employee_gid + "', " +
                        "'" + values.jobposition_gid + "'," +
                        "'" + i + "', " +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
               
                count = count + 1;

                msSQL="select businessunit_name from rms_mst_tbusinessunit where businessunit_gid='"+i+"'";
                string lsbuisnessunit_name = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "update rms_trn_tjobposition set transfer_to ='" + i + "'," +
                   " transfer_to_name='" + lsbuisnessunit_name + "' where jobposition_gid='" + values.jobposition_gid + "'";


                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }



            if (count == 0)
            {
                values.message = "Select Atleast One";

            }
            else
            {
                values.message = "Share to the team Successfully";

            }
            
        }

        public void DaPostJob(AddJob values, string employee_gid, result objResult)
        {

            msSQL = "select job_title ,created_date, job_code,company_name from rms_trn_tjobposition where job_title='" + values.job_type + "' and" +
                    " created_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'and company_name='" + values.company_name.Replace("   ", "").Replace("\n", "") + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == false)
            {
                msSQL = "select businessunit_gid from rms_trn_tbusinessunit2spoc where employee_gid='" + employee_gid + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("JOBA");
                    values.businessunit_gid = objODBCDataReader["businessunit_gid"].ToString();
                    if (values.businessunit_gid == "")
                    {
                        values.pnl_spoc = "N";
                        values.pnl_spocdropdown = "Y";
                        lsspoc_name = values.spoc_name;
                        lsspoc_gid = values.spoc_gid;
                    }
                    else
                    {
                        values.pnl_spoc = "Y";
                        values.pnl_spocdropdown = "Y";
                        msSQL = "select concat(b.user_code,' / ',b.user_firstname,' ',b.user_lastname) as user_name,a.employee_gid from hrm_mst_temployee a" +
                               " left join adm_mst_tuser b on a.user_gid=b.user_gid where employee_gid='" + employee_gid + "'";
                        objODBCDataReader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDataReader.HasRows)
                        {
                            values.user_name = objODBCDataReader["user_name"].ToString();
                            lsspoc_name = objODBCDataReader["user_name"].ToString();
                            lsspoc_gid = objODBCDataReader["employee_gid"].ToString();

                        }

                    }
                }
                    for (var i = 0; i < values.Joblocation.Count; i++)
                    {
                        lsjoblocation_gid += "'" + values.Joblocation[i].joblocation_gid + "',";

                    }

                    msSQL = "select joblocation_gid,joblocation_name from rms_trn_tjoblocation where joblocation_gid in(" + lsjoblocation_gid.TrimEnd(',') + ")";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        ls_gid += "'" + dr_datarow["joblocation_gid"].ToString() + "',";
                        ls_location += "'" + dr_datarow["joblocation_name"].ToString() + "',";
                    }
                    ls_location = ls_location.TrimEnd(',').Replace("'", " ");

                    msSQL = "select sequence_curval from adm_mst_tsequence where sequence_code='JOBA' order by sequence_gid desc limit 0,1";
                    lscurval = objdbconn.GetExecuteScalar(msSQL);
                    MSJOBCODE = "JC" + string.Format((lscurval), "0000");
                msSQL = " insert into rms_trn_tjobposition (" +
                        " jobposition_gid, " +
                        " job_code , " +
                        " job_title , " +
                        " company_name, " +
                        " domain, " +
                        " website ," +
                        " spoc_name ," +
                        " jobposition2spoc_gid, " +
                        " job_location," +
                        " noof_position," +
                        " experience_min ," +
                        " experience_max," +
                        " mandatroy_skills ," +
                        " education_qualification ," +
                        " ctcbudget_min ," +
                        " ctcbudget_max ," +
                        " search_location ," +
                        " notice_period ," +
                        " job_description ," +
                        " duplicate_validity ," +
                        " created_by, " +
                        " created_date )" +
                        " values (" +
                        "'" + msGetGid + "'," +
                        "'" + MSJOBCODE + "'," +
                        "'" + values.job_type + "'," +
                        "'" + values.company_name.Replace(" ", "").Replace("\n", "") + "'," +
                        "'" + values.domain_name + "'," +
                        "'" + values.website + "'," +
                        "'" + lsspoc_name + "'," +
                        "'" + lsspoc_gid + "'," +
                        "'" + ls_location + "'," +
                        "'" + values.noof_position + "'," +
                        "'" + values.experience + "'," +
                        "'" + values.experience_max + "',";
                if((values.mandatory_skills=="") || (values.mandatory_skills == null))
                {
                    msSQL += "'',";
                }
                else
                {
                    msSQL += "'" + values.mandatory_skills.Replace("'", "").Replace("\n", "") + "',";
                }

                msSQL += "'" + values.education_qualification + "'," +
                   "'" + values.ctc_budget + "'," +
                   "'" + values.ctc_max + "'," +
                   "'" + values.search_location + "'," +
                   "'" + values.noticeperiod + "',";
                if ((values.job_description == "") || (values.job_description == null))
                {
                    msSQL += "'',";
                }
                else
                {
                    msSQL += "'" + values.job_description.Replace("'", "").Replace("\n", "") + "',";
                }
                
                       msSQL+="'" + values.duplicate_validity + "'," +                           
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnResult !=0)
                    {

                        msgetassign2team_gid = objcmnfunctions.GetMasterGID("JCTU");
                        msSQL = " insert into rms_trn_tjobposition2unit ( " +
                                " jobposition2unit_gid," +
                                " jobposition_gid," +
                                " businessunit_gid ," +
                                " created_by," +
                                " created_date" +
                                " ) Values ( " +
                                " '" + msgetassign2team_gid + "', " +
                                "'" + msGetGid + "'," +
                                "'" + values.businessunit_gid + "', " +
                                "'" + employee_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnResult != 0)
                    {
                        for (var i = 0; i < values.businessunit2team.Count; i++)
                        {
                            lsbusinessunit2team_gid = "'" + values.businessunit2team[i].businessunit2team_gid + "'";

                            msSQL = "select  businessunit_gid,employee_gid from rms_mst_tbusinessunit2team where businessunit2team_gid =" + lsbusinessunit2team_gid + "";
                            objODBCDataReader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDataReader.HasRows)
                            {
                                var lsbusinessunit_gid = objODBCDataReader["businessunit_gid"].ToString();
                                var lsemployee_gid = objODBCDataReader["employee_gid"].ToString();

                                msgetassign2team_gid = objcmnfunctions.GetMasterGID("JCTE");
                                msSQL = " insert into rms_trn_tjobposition2team ( " +
                                       " jobposition2team_gid," +
                                       " jobposition_gid," +
                                       " businessunit_gid ," +
                                       " businessunit2team_gid ," +
                                       " employee_gid ," +
                                       " created_by," +
                                       " created_date" +
                                       " ) Values ( " +
                                       " '" + msgetassign2team_gid + "', " +
                                       "'" + msGetGid + "'," +
                                       "'" + lsbusinessunit_gid + "', " +
                                       "'" + lsbusinessunit2team_gid.Replace("'", "") + "'," +
                                       "'" + lsemployee_gid + "', " +
                                       "'" + employee_gid + "'," +
                                      "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                if (mnResult == 1)
                                {
                                    values.status = true;
                                }
                                else
                                {
                                    values.message = "Error Occured While Adding Team";
                                    values.status = false;

                                }
                            }
                        }
                            for (var j = 0; j < values.positionrecruiter_list.Count; j++)
                            {
                                lsrecruiter_gid = "'" + values.positionrecruiter_list[j].recruiter_gid + "'";

                                msSQL = "select businessunit2team_gid from rms_trn_tbusinessunitteam2recruiter where employee_gid = " + lsrecruiter_gid + " ";
                                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                                if (objODBCDataReader.HasRows)
                                {
                                    var lsbusinessunit2teamgid = objODBCDataReader["businessunit2team_gid"].ToString();
                                    msgetteam2recruiter_gid = objcmnfunctions.GetMasterGID("JCTR");
                                    msSQL = " insert into rms_trn_tjobposition2recruiter( " +
                                           " jobposition2recruiter_gid," +
                                           " jobposition_gid," +
                                           " businessunit2team_gid ," +
                                           " recruiter_gid ," +
                                           " created_by," +
                                           " created_date" +
                                           " ) Values ( " +
                                           " '" + msgetteam2recruiter_gid + "', " +
                                           "'" + msGetGid + "'," +
                                           "'" + lsbusinessunit2teamgid + "', " +
                                           "'" + lsrecruiter_gid.Replace("'", "") + "'," +
                                           "'" + employee_gid + "'," +
                                          "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                    if (mnResult == 1)
                                    {
                                        values.message = "Job Code Created Successfully";
                                        values.status = true;
                                    }
                                    else
                                    {
                                        values.message = "Error Occured While Adding Recruiter";
                                        values.status = false;
                                    }
                                }
                            }
                    }

                    else
                    {
                        values.message = "Error Occured While Adding Unit";
                        values.status = false;
                    }
                  }
            }
            else
            {
                values.message = "Job Code Created Successfully";
                values.status = true;
            }
        }

        //public bool DaGetsharingcolourcode( UnAssignSPOCSummaryList values, string jobposition_gid)
        //{
        //    msSQL = " select transfer_flag from rms_trn_tjobposition " +
        //                " where a.jobposition_gid='" + jobposition_gid + "'";
        //    dt_datatable = objdbconn.GetDataTable(msSQL);
        //    var getUnassignspocsummary = new List<UnassignSPOCsummary>();
        //    if (dt_datatable.Rows.Count != 0)
        //    {
        //        foreach (DataRow dt in dt_datatable.Rows)
        //        {

        //            getUnassignspocsummary.Add(new UnassignSPOCsummary
        //            {
        //                recruiter_gid = dt["recruiter_gid"].ToString(),
        //                employee_gid = dt["employee_gid"].ToString(),
        //                employee_name = dt["user_name"].ToString(),
        //                employee_code = dt["user_code"].ToString(),
        //                businessunit_name = dt["businessunit_name"].ToString(),
        //                businessunitteam_name = dt["businessunitteam_name"].ToString(),

        //            });
        //        }
        //        values.UnassignSPOCsummary = getUnassignspocsummary;
        //        values.status = true;
        //    }
        //    dt_datatable.Dispose();
        //    values.status = false;
        //    values.message = "No Records Found";
        //    return true;
        //}
    }
}