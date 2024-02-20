using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ems.utilities.Functions;
using ems.rms.Models;
using System.Data.Odbc;
using System.Data;

namespace ems.rms.DataAccess
{
    public class daRMSdashboard
    {
        
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        OdbcConnection objODBCconnection;
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msSQL, lscandidatesourced, lscandidatejoined,lsopeningjob_count,lsclosedjob_count;
        string lblEmployeeGID, lblUserCode, lsemployeeGID, businessunitGID;
        string  lsbusinessunitGID;
        int mnresult;
        public bool DagetRMSdashboard(string employee_gid, mdldashboard values)
        {
          
          
            lblEmployeeGID = employeereporting(employee_gid);
           
            msSQL = " Select a.hierarchy_level " +
                  " from adm_mst_tsubmodule a " +
                  " where a.employee_gid = '" + employee_gid + "' and a.module_gid = 'RMS' and a.submodule_id='RMS'";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                lblUserCode = objODBCDatareader["hierarchy_level"].ToString();
            }
            objODBCDatareader.Close();
            msSQL = "select a.businessunit_gid,a.businessunit_name from rms_mst_tbusinessunit a, rms_trn_tbusinessunit2manager b "+
                " where a.businessunit_gid=b.businessunit_gid and (b.employee_gid='"+employee_gid+"'";
            if (lblUserCode != "-1")
            {
                msSQL += " or b.employee_gid in (" + lblEmployeeGID + "))";
            }
            msSQL += " group by a.businessunit_gid";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_businessunit = new List<businessunit_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msSQL = "select count(recruitmentadd_gid) from rms_trn_trecruitmentadd where businessunit_gid='" + dt["businessunit_gid"] + "'";
                    lscandidatesourced = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = "select count(recruitmentadd_gid) from rms_trn_trecruitmentadd where businessunit_gid='" + dt["businessunit_gid"] + "'"+
                        " and candidate_status='Invoice Raised-Joined'";
                    lscandidatejoined = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = "select count(a.jobposition_gid) from rms_trn_tjobposition a, rms_trn_tjobposition2unit b where job_status='closed' and" +
                        " a.jobposition_gid=b.jobposition_gid and  businessunit_gid='" + dt["businessunit_gid"] + "' and"+
                        " (transfer_from='" + dt["businessunit_gid"] + "' or transfer_from is null)";
                    lsclosedjob_count = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = "select count(a.jobposition_gid) from rms_trn_tjobposition a, rms_trn_tjobposition2unit b where job_status='opening' and " +
                        " a.jobposition_gid=b.jobposition_gid and businessunit_gid='" + dt["businessunit_gid"] + "' and "+
                         " (transfer_from='" + dt["businessunit_gid"] + "'  or transfer_from is null)"; 
                    lsopeningjob_count = objdbconn.GetExecuteScalar(msSQL);

                    get_businessunit.Add(new businessunit_list
                    {
                        businessunit_gid = dt["businessunit_gid"].ToString(),
                        businessunit_name = dt["businessunit_name"].ToString(),
                        candidatesourced_count= lscandidatesourced,
                        candidatejoined_count = lscandidatejoined,
                        openingjob_count=lsopeningjob_count,
                        closedjob_count =lsclosedjob_count
                    });
                }
                values.businessunit_list = get_businessunit;
              
            }           
            dt_datatable.Dispose();
          
            return true;
                   
        }
        public bool DaRecruiterSummary(string employee_gid, mdlRecruitersummary objRecruitersummary)
        {
          
            lblEmployeeGID = employeereporting(employee_gid);
           
            msSQL = " Select a.hierarchy_level " +
                  " from adm_mst_tsubmodule a " +
                  " where a.employee_gid = '" + employee_gid + "' and a.module_gid = 'RMS' and a.submodule_id='RMS'";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                lblUserCode = objODBCDatareader["hierarchy_level"].ToString();
            }
            objODBCDatareader.Close();

            string lsmonth,lsstartdate,lsenddate,lsyear,lsrecruiter_photo,lsmonth_name,lsmonth_number;
             DateTime lsprevious_month = DateTime.Now;
            lsmonth = string.Format("{0:MM}", DateTime.Now ); 
            lsprevious_month = lsprevious_month.AddMonths(-1);
            lsmonth_name = lsprevious_month.ToString("MMMM");
            lsmonth_number = lsprevious_month.ToString("MM");
            lsyear = lsprevious_month.ToString("yyyy");


            //lsyear = DateTime.Now.Year.ToString();
            msSQL = "select date_format(last_day(concat('"+lsyear+"','" + lsmonth_number + "','01')),'%Y-%m-%d')";
            lsenddate = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select date_format(concat('" + lsyear + "','" + lsmonth_number + "','01'),'%Y-%m-%d')";
            lsstartdate = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select concat(e.user_firstname,' ',e.user_lastname) as recruiter,b.businessunitteam_name,count(a.recruitmentadd_gid) as candidate_sourced," +
                    " (select count(*) from rms_trn_tcandidateconfirm x where x.invoice_date between ('" + lsstartdate + "')  and ('" + lsenddate + "') and x.created_by=a.recruiter_gid)as candidate_joined," +
                    " f.businessunit_name,d.employee_photo from rms_trn_trecruitmentadd a " +
                    " left join rms_mst_tbusinessunit2team b on b.businessunit2team_gid = a.businessunit2team_gid " +
                    " left join rms_trn_tcandidateconfirm c on c.recruitmentadd_gid =a.recruitmentadd_gid " +
                    " left join hrm_mst_temployee d on d.employee_gid = a.recruiter_gid " +
                    " left join adm_mst_tuser e on e.user_gid = d.user_gid " +
                    " left join rms_mst_tbusinessunit f on a.businessunit_gid=f.businessunit_gid " +
                    " left join rms_trn_tbusinessunit2manager g on g.businessunit_gid=b.businessunit_gid  where c.invoice_date between ('" + lsstartdate + "') " +
                    " and ('" + lsenddate + "') and (g.employee_gid='" + employee_gid + "'";
              

            if (lblUserCode != "-1")
            {
                msSQL += " or b.employee_gid in (" + lblEmployeeGID + ") )";              
            }
            msSQL += "  and e.user_status = 'Y'   group by a.recruiter_gid order by candidate_joined desc limit 5 ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getRecruitersummary = new List<recruitersummary_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {

                    if (dr_datarow["employee_photo"].ToString ()=="")
                    {
                        lsrecruiter_photo = "N";
                    }
                    else
                    {
                        lsrecruiter_photo = dr_datarow["employee_photo"].ToString();
                    }

                    getRecruitersummary.Add(new recruitersummary_list
                    {
                        recruiter_photo=lsrecruiter_photo,
                        recruitername = (dr_datarow["recruiter"].ToString()),
                        businessunitteam_name = (dr_datarow["businessunitteam_name"].ToString()),
                        candidate_sourced = (dr_datarow["candidate_sourced"].ToString()),
                        candidate_joined = (dr_datarow["candidate_joined"].ToString()),
                        businessunit_name=(dr_datarow["businessunit_name"].ToString ())
                    });
                }
                objRecruitersummary.recruitersummary_list = getRecruitersummary;

               
            }
            objRecruitersummary.month = lsmonth_name;
            dt_datatable.Dispose();

         
            return true;
        }
        public bool Dagetoverallstatus(mdloverallstatus values, string employee_gid)
        {
           
          
            lblEmployeeGID = employeereporting(employee_gid);
         
            msSQL = " Select a.hierarchy_level " +
                  " from adm_mst_tsubmodule a " +
                  " where a.employee_gid = '" + employee_gid + "' and a.module_gid = 'RMS' and a.submodule_id='RMS'";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                lblUserCode = objODBCDatareader["hierarchy_level"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = "select businessunit_gid  from rms_trn_tbusinessunit2manager where employee_gid ='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);

            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {

                    businessunitGID += "'" + dr_datarow["businessunit_gid"].ToString() + "',";
                }
                businessunitGID = businessunitGID.Remove(businessunitGID.LastIndexOf(","));
                lsbusinessunitGID = "(" + businessunitGID + ")";

            }
            msSQL = "select " +
                    " (select count(x.recruitmentadd_gid) from rms_trn_trecruitmentadd x where x.candidate_status = 'Invoice Raised-Joined' " +
                    "  and businessunit_gid in " + lsbusinessunitGID + ") as countjoined," +
                    " (select count(x.recruitmentadd_gid) from rms_trn_trecruitmentadd x  " +
                    " where  businessunit_gid in " + lsbusinessunitGID + ") as countsourced," +
                    " (select count(x.recruitmentadd_gid) from rms_trn_trecruitmentadd x where x.candidate_status = 'Selected' " +
                    " and businessunit_gid in " + lsbusinessunitGID + ") as countselected," +
                    " (select count(x.recruitmentadd_gid) from rms_trn_trecruitmentadd x where x.candidate_status = 'Rejected' " +
                    " and businessunit_gid in " + lsbusinessunitGID + ") as countrejected" +
                    "  from rms_mst_tbusinessunit a  limit 1";
                  
           
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                if (objODBCDatareader["countjoined"].ToString() == "")
                {
                    values.countjoined = "0";
                }
                else
                {
                    values.countjoined = objODBCDatareader["countjoined"].ToString();
                }

                if (objODBCDatareader["countsourced"].ToString() == "")
                {
                    values.countsourced = "0";
                }
                else
                {
                    values.countsourced = objODBCDatareader["countsourced"].ToString();
                }
                if (objODBCDatareader["countselected"].ToString() == "")
                {
                    values.countselected = "0";
                }
                else
                {
                    values.countselected = objODBCDatareader["countselected"].ToString();
                }
                if (objODBCDatareader["countrejected"].ToString() == "")
                {
                    values.countrejected = "0";
                }
                else
                {
                    values.countrejected = objODBCDatareader["countrejected"].ToString();
                }
            }
            
            objODBCDatareader.Close();
      return true;
        }
        public string childloop(string employee)
        {
            
            msSQL = " select a.*, concat(b.user_firstname, '-', b.user_code) as user  from adm_mst_tmodule2employee a  " +
                " inner join hrm_mst_temployee c on a.employee_gid = c.employee_gid  " +
                " inner join adm_mst_tuser b on c.user_gid = b.user_gid  " +
                " where a.module_gid = 'RMS'  " +
                " and a.employeereporting_to = '" + employee + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            foreach (DataRow dr_datarow in dt_datatable.Rows)
            {
                msSQL = " select a.*, b.user_gid  from adm_mst_tmodule2employee a  " +
                    " inner join hrm_mst_temployee c on a.employee_gid = c.employee_gid  " +
                    " inner join adm_mst_tuser b on c.user_gid = b.user_gid  " +
                    " where a.module_gid = 'RMS' ";
                msSQL += " and a.employee_gid = '" + dr_datarow["employee_gid"].ToString() + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objODBCDatareader.Read();
                    lsemployeeGID = lsemployeeGID + "'" + objODBCDatareader["employee_gid"].ToString() + "',";
                }
                objODBCDatareader.Close();
                childloop(dr_datarow["employee_gid"].ToString());
            }

            dt_datatable.Dispose();
            return lsemployeeGID;
        }
        public string employeereporting(string employee_gid)
        {
         
            var lsemployeeGID = childloop(employee_gid);

            if (lsemployeeGID != "")
            {

                lsemployeeGID = lsemployeeGID.TrimEnd(',');
                lblEmployeeGID = lsemployeeGID;
            }
            else
            {
                lblEmployeeGID = "'" + employee_gid + "'";
            }
         
            return lblEmployeeGID;
        }
        public void DaprojectPrivilege(string employee_gid, dashboardprivilege values)
        {

            msSQL = " SELECT employee_gid from rms_trn_tbusinessunit2manager" +
                    " WHERE employee_gid = '" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
              if(objODBCDatareader.HasRows==true)
            {
                values.privilege = "Y";
            }
            else
            {
                values.privilege = "N";
            }
            objODBCDatareader.Close();
        }
        public void Daemployeemail(employeemail values)
        {
            msSQL = "select a.employee_gid from hrm_mst_temployee a"+
                " left join adm_mst_tuser b on a.user_gid=b.user_gid where a.employee_emailid='" + values.employeeemail_id + "' and " +
                " user_code ='" + values.user_code + "'";
            string lsmailid = objdbconn.GetExecuteScalar(msSQL);
            if (lsmailid == "")
            {
                values.status = false;
            }
            else
            {
                values.status = true;
            }
        }
        public void Daupdatelawyerpassword(employeemail values)
        {
            msSQL = "select user_gid from hrm_mst_temployee where employee_emailid='" + values.employeeemail_id + "'";
            string lsuser_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "update adm_mst_tuser set user_password='" + objcmnfunctions.ConvertToAscii(values.user_password) + "' where user_gid='" + lsuser_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

          
            if (mnresult == 0)
            {
                values.status = false;
            }
            else
            {
                values.status = true;
            }
        }
    }
}