using ems.master.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;



/// <summary>
/// (It's used for ApplicationGradingTool) ApplicationGradingTool DataAccess Class accessed by API methods from related Controller class and is returning relevant response to client.
/// </summary>
/// <remarks>Written by Sumala,Logapriya and Abilash</remarks>

namespace ems.master.DataAccess
{
    public class DaMstApplicationGradingTool
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msSQL, mspSQL, msGetGid, msGetGid1;
        int mnResult;

        public void DaGettmpGradingTool(string employee_gid, Mdlgradingtool values)
        {
            msSQL = "select application2gradingassesment_gid,application2gradingtool_gid,maximumscored,actualscored," +
                " assessmentcriteria_in,assessmentcriteria_ingrade,shareholdermale_in,shareholderfemale_in,bodmale_in,bodfemale_in, " +
                   " (select group_concat(distinct assessmentcriteria_name) as assessmentcriteria_name  from ocs_mst_tassessmentcriteria2dropdown d " +
                    " where d.application2gradingassesment_gid = a.application2gradingassesment_gid ) as assessmentcriteria_name " +
                " from ocs_mst_tapplication2gradingassessmentcriteria a  " +
                " where application2gradingtool_gid ='" + employee_gid + "'";

            dt_datatable = objdbconn.GetDataTable(msSQL);

            var getgradingtool_list = new List<gradingtool_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getgradingtool_list.Add(new gradingtool_list
                    {
                        application2gradingassesment_gid = (dr_datarow["application2gradingassesment_gid"].ToString()),
                        application2gradingtool_gid = (dr_datarow["application2gradingtool_gid"].ToString()),
                        assessmentcriteria_name = (dr_datarow["assessmentcriteria_name"].ToString()),
                        maximum_score = (dr_datarow["maximumscored"].ToString()),
                        actual_score = (dr_datarow["actualscored"].ToString()),
                        assessment_in = (dr_datarow["assessmentcriteria_in"].ToString()),
                        assessment_ingrade = (dr_datarow["assessmentcriteria_ingrade"].ToString()),
                        shareholders_male = (dr_datarow["shareholdermale_in"].ToString()),
                        shareholders_female = (dr_datarow["shareholderfemale_in"].ToString()),
                        bodmale_in = (dr_datarow["bodmale_in"].ToString()),
                        bodfemale_in = (dr_datarow["bodfemale_in"].ToString()),
                    });
                }
                values.gradingtool_list = getgradingtool_list;
            }
            dt_datatable.Dispose();


        }

        public void DaGetEditGradingToolassesment(string application2gradingtool_gid, Mdlgradingtool values)
        {
            msSQL = "select application2gradingassesment_gid,application2gradingtool_gid,maximumscored,actualscored," +
                " assessmentcriteria_in,assessmentcriteria_ingrade,shareholdermale_in,shareholderfemale_in,bodmale_in,bodfemale_in, " +
                " (select group_concat(distinct assessmentcriteria_name) as assessmentcriteria_name  from ocs_mst_tassessmentcriteria2dropdown d " +
                " where d.application2gradingassesment_gid = a.application2gradingassesment_gid ) as assessmentcriteria_name " +
                " from ocs_mst_tapplication2gradingassessmentcriteria a  " +
                " where application2gradingtool_gid ='" + application2gradingtool_gid + "'";

            dt_datatable = objdbconn.GetDataTable(msSQL);

            var getgradingtool_list = new List<gradingtool_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getgradingtool_list.Add(new gradingtool_list
                    {
                        application2gradingassesment_gid = (dr_datarow["application2gradingassesment_gid"].ToString()),
                        application2gradingtool_gid = (dr_datarow["application2gradingtool_gid"].ToString()),
                        assessmentcriteria_name = (dr_datarow["assessmentcriteria_name"].ToString()),
                        maximum_score = (dr_datarow["maximumscored"].ToString()),
                        actual_score = (dr_datarow["actualscored"].ToString()),
                        assessment_in = (dr_datarow["assessmentcriteria_in"].ToString()),
                        assessment_ingrade = (dr_datarow["assessmentcriteria_ingrade"].ToString()),
                        shareholders_male = (dr_datarow["shareholdermale_in"].ToString()),
                        shareholders_female = (dr_datarow["shareholderfemale_in"].ToString()),
                        bodmale_in = (dr_datarow["bodmale_in"].ToString()),
                        bodfemale_in = (dr_datarow["bodfemale_in"].ToString()),

                    });
                }
                values.gradingtool_list = getgradingtool_list;
            }
            dt_datatable.Dispose();
        }

        public void DaGetEdittmpGradingToolassesment(string employee_gid, string application2gradingtool_gid, Mdlgradingtool values)
        {
            msSQL = "select application2gradingassesment_gid,application2gradingtool_gid,maximumscored,actualscored," +
                " assessmentcriteria_in,assessmentcriteria_ingrade,shareholdermale_in,shareholderfemale_in,bodmale_in,bodfemale_in, " +
                " (select group_concat(distinct assessmentcriteria_name) as assessmentcriteria_name  from ocs_mst_tassessmentcriteria2dropdown d " +
                " where d.application2gradingassesment_gid = a.application2gradingassesment_gid ) as assessmentcriteria_name " +
                " from ocs_mst_tapplication2gradingassessmentcriteria a  " +
                " where application2gradingtool_gid ='" + application2gradingtool_gid + "' or application2gradingtool_gid ='" + employee_gid + "'";

            dt_datatable = objdbconn.GetDataTable(msSQL);

            var getgradingtool_list = new List<gradingtool_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getgradingtool_list.Add(new gradingtool_list
                    {
                        application2gradingassesment_gid = (dr_datarow["application2gradingassesment_gid"].ToString()),
                        application2gradingtool_gid = (dr_datarow["application2gradingtool_gid"].ToString()),
                        assessmentcriteria_name = (dr_datarow["assessmentcriteria_name"].ToString()),
                        maximum_score = (dr_datarow["maximumscored"].ToString()),
                        actual_score = (dr_datarow["actualscored"].ToString()),
                        assessment_in = (dr_datarow["assessmentcriteria_in"].ToString()),
                        assessment_ingrade = (dr_datarow["assessmentcriteria_ingrade"].ToString()),
                        shareholders_male = (dr_datarow["shareholdermale_in"].ToString()),
                        shareholders_female = (dr_datarow["shareholderfemale_in"].ToString()),
                        bodmale_in = (dr_datarow["bodmale_in"].ToString()),
                        bodfemale_in = (dr_datarow["bodfemale_in"].ToString()),


                    });
                }
                values.gradingtool_list = getgradingtool_list;
            }
            dt_datatable.Dispose();
        }
        
        public void DaGetGradingTool(string application_gid, string statusupdated_by, Mdlgradingtool values)
        {
            msSQL = "select a.application2gradingtool_gid,a.application_gid, a.overallfporating, a.overallfpograde, " +
                 "date_format(a.dateofsurvey,'%d-%m-%Y') as dateofsurvey,concat(b.user_firstname,' ',b.user_lastname,'/',b.user_code) as created_by,date_format(a.created_date,'%d-%m-%Y %H:%m:%s') as created_date" +
                " from ocs_mst_tapplication2gradingtool a  " +
                " left join hrm_mst_temployee d on a.created_by=d.employee_gid " +
                " left join adm_mst_tuser b on d.user_gid=b.user_gid " +
                " where a.application_gid ='" + application_gid + "' and a.statusupdated_by='"+ statusupdated_by + "'";

            dt_datatable = objdbconn.GetDataTable(msSQL);

            var getgrading_list = new List<grading_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getgrading_list.Add(new grading_list
                    {
                        application2gradingtool_gid = (dr_datarow["application2gradingtool_gid"].ToString()),
                        application_gid = (dr_datarow["application_gid"].ToString()),
                        dateofsurvey = (dr_datarow["dateofsurvey"].ToString()),
                        Created_by = (dr_datarow["created_by"].ToString()),
                        Created_date = (dr_datarow["created_date"].ToString()),
                        overallfporating = (dr_datarow["overallfporating"].ToString()),
                        overallfpograde = (dr_datarow["overallfpograde"].ToString())
                    });
                }
                values.grading_list = getgrading_list;
            }
            dt_datatable.Dispose();


        }

        public void DaSubmitGradingassesment(string employee_gid, Mdlgradingtool values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("APGA");
            msSQL = " insert into ocs_mst_tapplication2gradingassessmentcriteria(" +
                    " application2gradingassesment_gid," +
                    " application2gradingtool_gid," +
                    " maximumscored," +
                    " actualscored," +
                    " assessmentcriteria_in," +
                    " assessmentcriteria_ingrade," +
                    "shareholdermale_in," +
                    "shareholderfemale_in," +
                    "bodmale_in," +
                    "bodfemale_in," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + values.maximum_score + "'," +
                    "'" + values.actual_score + "',";
            if (values.assessment_in == null || values.assessment_in == "")
            {
                msSQL += "'0.00',";
            }
            else
            {
                msSQL += "'" + values.assessment_in + "',";
            }

            if (values.assessment_ingrade == null || values.assessment_ingrade == "")
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.assessment_ingrade + "',";
            }

            if (values.shareholders_male == null || values.shareholders_male == "")
            {
                msSQL += "'0.00',";
            }
            else
            {
                msSQL += "'" + values.shareholders_male + "',";
            }
            if (values.shareholders_female == null || values.shareholders_female == "")
            {
                msSQL += "'0.00',";
            }
            else
            {
                msSQL += "'" + values.shareholders_female + "',";
            }
            if (values.bodmale_in == null || values.bodmale_in == "")
            {
                msSQL += "'0.00',";
            }
            else
            {
                msSQL += "'" + values.bodmale_in + "',";
            }
            if (values.bodfemale_in == null || values.bodfemale_in == "")
            {
                msSQL += "'0.00',";
            }
            else
            {
                msSQL += "'" + values.bodfemale_in + "',";
            }


            msSQL += "'" + employee_gid + "'," +
                 "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            for (var i = 0; i < values.gradingtool_list.Count; i++)
            {
                msGetGid1 = objcmnfunctions.GetMasterGID("ACDD");

                msSQL = "Insert into ocs_mst_tassessmentcriteria2dropdown( " +
                       " assessmentcriteria2dropdown_gid, " +
                        " application2gradingassesment_gid, " +
                       "assessmentcriteria_gid, " +
                       " assessmentcriteria_name," +
                       " created_by," +
                       " created_date)" +
                       " values(" +
                       "'" + msGetGid1 + "'," +
                         "'" + msGetGid + "'," +
                       "'" + values.gradingtool_list[i].assessmentcriteria_gid + "'," +
                       "'" + values.gradingtool_list[i].assessmentcriteria_name + "'," +
                       "'" + employee_gid + "'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Assessment Criteria Details are Added Successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }

        }

        public void DaEditAssessmentCriteriaDetails(string application2gradingassesment_gid, Mdlgradingtool values)
        {
            msSQL = " SELECT a.application2gradingassesment_gid,a.application2gradingtool_gid,a.assessmentcriteria,a.maximumscored,a.actualscored,a.assessmentcriteria_in,a.assessmentcriteria_ingrade,a.shareholdermale_in,a.shareholderfemale_in,a.bodmale_in,a.bodfemale_in," +
                    " (select group_concat(distinct assessmentcriteria_name) as assessmentcriteria_name  from ocs_mst_tassessmentcriteria2dropdown d " +
                    " where d.application2gradingassesment_gid = a.application2gradingassesment_gid ) as assessmentcriteria_name FROM ocs_mst_tapplication2gradingassessmentcriteria a where a.application2gradingassesment_gid='" + application2gradingassesment_gid + "' ";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.application2gradingassesment_gid = objODBCDatareader["application2gradingassesment_gid"].ToString();
                values.application2gradingtool_gid = objODBCDatareader["application2gradingtool_gid"].ToString();
                values.assessmentcriteria_name = objODBCDatareader["assessmentcriteria_name"].ToString();
                values.maximum_score = objODBCDatareader["maximumscored"].ToString();
                values.actual_score = objODBCDatareader["actualscored"].ToString();
                values.assessment_in = objODBCDatareader["assessmentcriteria_in"].ToString();
                values.assessment_ingrade = objODBCDatareader["assessmentcriteria_ingrade"].ToString();
                values.shareholders_male = objODBCDatareader["shareholdermale_in"].ToString();
                values.shareholders_female = objODBCDatareader["shareholderfemale_in"].ToString();
                values.bodmale_in = objODBCDatareader["bodmale_in"].ToString();
                values.bodfemale_in = objODBCDatareader["bodfemale_in"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " select assessmentcriteria2dropdown_gid,assessmentcriteria_gid,assessmentcriteria_name, application2gradingassesment_gid from ocs_mst_tassessmentcriteria2dropdown " +
                    " where application2gradingassesment_gid='" + application2gradingassesment_gid + "'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getgradingtool_list = new List<gradingtool_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getgradingtool_list.Add(new gradingtool_list
                    {
                        assessmentcriteria_gid = dt["assessmentcriteria_gid"].ToString(),
                        assessmentcriteria_name = dt["assessmentcriteria_name"].ToString(),
                        application2gradingassesment_gid = dt["application2gradingassesment_gid"].ToString(),
                    });
                    values.gradingtool_list = getgradingtool_list;
                }
            }
            dt_datatable.Dispose();

            msSQL = " SELECT assessmentcriteria_gid,assessmentcriteria_name from ocs_mst_tassessmentcriteria where status='Y'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getassessmentcriteria_list = new List<assessmentcriteria_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getassessmentcriteria_list.Add(new assessmentcriteria_list
                    {
                        assessmentcriteria_gid = dt["assessmentcriteria_gid"].ToString(),
                        assessmentcriteria_name = dt["assessmentcriteria_name"].ToString(),
                    });
                    values.assessmentcriteria_list = getassessmentcriteria_list;
                }
            }
            dt_datatable.Dispose();

        }
        
        public void DaUpdateAssessmentCriteriaDetails(string employee_gid, Mdlgradingtool values)
        {
            mspSQL += "(";
            for (var i = 0; i < values.gradingtool_list.Count; i++)
            {
                mspSQL += "'" + values.gradingtool_list[i].assessmentcriteria_gid + "',";
            }
            msSQL = "select * from ocs_mst_tassessmentcriteria2dropdown where application2gradingassesment_gid='" + values.application2gradingassesment_gid + "'";
            msSQL += " and assessmentcriteria_gid not in " + mspSQL.TrimEnd(',') + "";
            msSQL += ")";

            dt_datatable = objdbconn.GetDataTable(msSQL);

            msSQL = " update ocs_mst_tapplication2gradingassessmentcriteria set " +
         " maximumscored='" + values.maximum_score + "'," +
         " actualscored='" + values.actual_score + "',";
            if (values.assessment_in == null || values.assessment_in == "")
            {
                msSQL += "assessmentcriteria_in='0.00',";
            }
            else
            {
                msSQL += "assessmentcriteria_in='" + values.assessment_in + "',";
            }
            if (values.assessment_ingrade == null || values.assessment_ingrade == "")
            {
                msSQL += "assessmentcriteria_ingrade='0.00',";
            }
            else
            {
                msSQL += "assessmentcriteria_ingrade='" + values.assessment_ingrade + "',";
            }
            if (values.shareholders_male == null || values.shareholders_male == "")
            {
                msSQL += "shareholdermale_in='0.00',";
            }
            else
            {
                msSQL += "shareholdermale_in='" + values.shareholders_male + "',";
            }
            if (values.shareholders_female == null || values.shareholders_female == "")
            {
                msSQL += "shareholderfemale_in='0.00',";
            }
            else
            {
                msSQL += "shareholderfemale_in='" + values.shareholders_female + "',";
            }
            if (values.bodmale_in == null || values.bodmale_in == "")
            {
                msSQL += "bodmale_in='0.00',";
            }
            else
            {
                msSQL += "bodmale_in='" + values.bodmale_in + "',";
            }
            if (values.bodfemale_in == null || values.bodfemale_in == "")
            {
                msSQL += "bodfemale_in='0.00'";
            }
            else
            {
                msSQL += "bodfemale_in='" + values.bodfemale_in + "'";
            }

            msSQL += " where application2gradingassesment_gid='" + values.application2gradingassesment_gid + "' ";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            msSQL = " delete from ocs_mst_tassessmentcriteria2dropdown where application2gradingassesment_gid ='" + values.application2gradingassesment_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
                for (var i = 0; i < values.gradingtool_list.Count; i++)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("ACDD");

                    msSQL = "Insert into ocs_mst_tassessmentcriteria2dropdown( " +
                           " assessmentcriteria2dropdown_gid, " +
                           "application2gradingassesment_gid," +
                           "assessmentcriteria_gid, " +
                           " assessmentcriteria_name," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetGid + "'," +
                           "'" + values.application2gradingassesment_gid + "'," +
                           "'" + values.gradingtool_list[i].assessmentcriteria_gid + "'," +
                           "'" + values.gradingtool_list[i].assessmentcriteria_name + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

            if (mnResult != 0)
            {

                values.status = true;
                values.message = "Assessment Criteria Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Updating";
            }

        }

        public void DaDeletegradingassesment(string application2gradingassesment_gid, Mdlgradingtool values)
        {
            msSQL = "delete from ocs_mst_tapplication2gradingassessmentcriteria where application2gradingassesment_gid='" + application2gradingassesment_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "Assessment Criteria Details Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured While Deleting Assessment Criteria Details";
                values.status = false;

            }
        }
        
        public void DaGetAssessmentCriteriaDropDown(string employee_gid, mdlcriteria values)
        {
            msSQL = " SELECT assessmentcriteria_gid,assessmentcriteria_name from ocs_mst_tassessmentcriteria where status='Y'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getassessmentcriteria_list = new List<criteria_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getassessmentcriteria_list.Add(new criteria_list
                    {
                        assessmentcriteria_gid = (dr_datarow["assessmentcriteria_gid"].ToString()),
                        assessmentcriteria_name = (dr_datarow["assessmentcriteria_name"].ToString()),


                    });
                }
                values.criteria_list = getassessmentcriteria_list;
            }
            dt_datatable.Dispose();

        }

        public void DaSaveasDraftGradingToolDetails(string employee_gid, Mdlgeographicdetails values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("AP2G");
            msSQL = "insert into ocs_mst_tapplication2gradingtool (" +
                    " application2gradingtool_gid," +
                    " application_gid," +
                    " dateofsurvey," +
                    " overallfporating," +
                    " overallfpograde," +
                    " majorcrops," +
                    " alternativeincomesource," +
                    " objevtiveoffpo," +
                    " recommendation," +
                    " fpo_acscore," +
                    " numnerofaactive_fig," +
                    " existinglending_directandindirect," +
                    " nonnegotiableconditions_met," +
                    " outstandingportfolio_directandindirect," +
                    " institution_directandindrectborrowing," +
                    " totaldisbursements_otherlenders," +
                    " par90_managedbyonlyinstitution_direct," +
                    " recommendation2," +
                    " gradingdraft_flag," +
                    " statusupdated_by," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.application_gid + "',";
            if (values.dateofsurvey == null || values.dateofsurvey == "")
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.dateofsurvey).ToString("yyyy-MM-dd") + "',";
            }
            if (values.overallfporating == null || values.overallfporating == "")
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + values.overallfporating.Replace("'", "") + "',";
            }
            if (values.overallfpograding == null || values.overallfpograding == "")
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + values.overallfpograding.Replace("'", "") + "',";
            }
            if (values.majorcrops == null || values.majorcrops == "")
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + values.majorcrops.Replace("'", "") + "',";
            }
            if (values.alternativeincomesource == null || values.alternativeincomesource == "")
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + values.alternativeincomesource.Replace("'", "") + "',";
            }
            if (values.objevtiveoffpo == null || values.objevtiveoffpo == "")
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + values.objevtiveoffpo.Replace("'", "") + "',";
            }
            if (values.recommendation == null || values.recommendation == "")
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + values.recommendation.Replace("'", "") + "',";
            }
            if (values.fpo_acscore == null || values.fpo_acscore == "")
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + values.fpo_acscore.Replace("'", "") + "',";
            }
            if (values.numnerofaactive_fig == null || values.numnerofaactive_fig == "")
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + values.numnerofaactive_fig.Replace("'", "") + "',";
            }
            if (values.existinglending_directindirect == null || values.existinglending_directindirect == "")
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + values.existinglending_directindirect.Replace("'", "") + "',";
            }
            if (values.nonnegotiableconditions_met == null || values.nonnegotiableconditions_met == "")
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + values.nonnegotiableconditions_met.Replace("'", "") + "',";
            }
            if (values.outstandingportfolio_directindirect == null || values.outstandingportfolio_directindirect == "")
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + values.outstandingportfolio_directindirect.Replace("'", "") + "',";
            }
            if (values.institution_directindrectborrowing == null || values.institution_directindrectborrowing == "")
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + values.institution_directindrectborrowing.Replace("'", "") + "',";
            }
            if (values.totaldisbursements_otherlenders == null || values.totaldisbursements_otherlenders == "")
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + values.totaldisbursements_otherlenders.Replace("'", "") + "',";
            }
            if (values.par90_managedbyonlyinstitution_direct == null || values.par90_managedbyonlyinstitution_direct == "")
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + values.par90_managedbyonlyinstitution_direct.Replace("'", "") + "',";
            }

            if (values.recommendation1 == null || values.recommendation1 == "")
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + values.recommendation1.Replace("'", "") + "',";
            }
            msSQL += "'Y', " +
                    "'" + values.statusupdated_by + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update ocs_mst_tapplication2gradingassessmentcriteria set application2gradingtool_gid ='" + msGetGid + "' where application2gradingtool_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msGetGid1 = objcmnfunctions.GetMasterGID("A2GD");
            msSQL = " insert into ocs_mst_tapplication2geographic(" +
                    " application2geographic_gid," +
                    " application2gradingtool_gid," +
                    " gradingdraft_flag," +
                    " numberofstates ," +
                    " numberofdistricts ," +
                    " numberofbranches ," +
                    " numberofmembers ," +
                    " numberof_activemembers ," +
                    " numberofgroups  ," +
                    " zonaloffices  ," +
                    " regionaloffices ," +
                    " branches ," +
                    " adminstaff ," +
                    " fieldstaff  ," +
                    " fieldstaff_ratio  ," +
                    " statusupdated_by," +
                    " created_by ," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid1 + "'," +
                     "'" + msGetGid + "'," +
                     "'Y'," +
                    "'" + values.numberofstates + "'," +
                    "'" + values.numberofdistricts + "'," +
                    "'" + values.numberofbranches + "'," +
                     "'" + values.numberofmembers + "'," +
                    "'" + values.numberof_activemembers + "'," +
                    "'" + values.numberofgroups + "'," +
                     "'" + values.zonaloffices + "'," +
                    "'" + values.regionaloffices + "'," +
                    "'" + values.branches + "'," +
                     "'" + values.adminstaff + "'," +
                    "'" + values.fieldstaff + "'," +
                    "'" + values.fieldstaff_ratio + "'," +
                    "'" + values.statusupdated_by + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Assessed Score Details Saved Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Adding";
            }
        }
        
        public bool DaSaveasEditDraftGradingToolDetails(string employee_gid, Mdlgeographicdetails values)
        {
            msSQL = "select date_format(dateofsurvey,'%d-%m-%Y') as dateofsurvey from ocs_mst_tapplication2gradingtool where application2gradingtool_gid='" + values.application2gradingtool_gid + "'";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                if (values.dateofsurvey == (objODBCDatareader["dateofsurvey"].ToString()))
                {
                    values.dateofsurvey = "exist";
                }
                objODBCDatareader.Close();

            }
            else
            {
                objODBCDatareader.Close();
            }
            msSQL = " update ocs_mst_tapplication2gradingtool set ";
            if (values.dateofsurvey == null || values.dateofsurvey == "")
            {
                msSQL += " dateofsurvey=null,";
            }
            else if (values.dateofsurvey == "exist")
            {

            }
            else
            {
                msSQL += " dateofsurvey='" + Convert.ToDateTime(values.dateofsurvey).ToString("yyyy-MM-dd") + "',";
            }
            if (values.overallfporating == null || values.overallfporating == "")
            {
                msSQL += " overallfporating=null,";
            }
            else
            {
                msSQL += " overallfporating='" + values.overallfporating.Replace("'", "") + "',";
            }
            if (values.overallfpograding == null || values.overallfpograding == "")
            {
                msSQL += " overallfpograde=null,";
            }
            else
            {
                msSQL += " overallfpograde='" + values.overallfpograding.Replace("'", "") + "',";
            }
            if (values.majorcrops == null || values.majorcrops == "")
            {
                msSQL += " majorcrops=null,";
            }
            else
            {
                msSQL += " majorcrops='" + values.majorcrops.Replace("'", "") + "',";
            }
            if (values.alternativeincomesource == null || values.alternativeincomesource == "")
            {
                msSQL += " alternativeincomesource=null,";
            }
            else
            {
                msSQL += " alternativeincomesource='" + values.alternativeincomesource.Replace("'", "") + "',";
            }
            if (values.objevtiveoffpo == null || values.objevtiveoffpo == "")
            {
                msSQL += " objevtiveoffpo=null,";
            }
            else
            {
                msSQL += " objevtiveoffpo='" + values.objevtiveoffpo.Replace("'", "") + "',";
            }
            if (values.recommendation == null || values.recommendation == "")
            {
                msSQL += " recommendation=null,";
            }
            else
            {
                msSQL += " recommendation='" + values.recommendation.Replace("'", "") + "',";
            }
            if (values.fpo_acscore == null || values.fpo_acscore == "")
            {
                msSQL += " fpo_acscore=null,";
            }
            else
            {
                msSQL += " fpo_acscore='" + values.fpo_acscore.Replace("'", "") + "',";
            }
            if (values.numnerofaactive_fig == null || values.numnerofaactive_fig == "")
            {
                msSQL += " numnerofaactive_fig=null,";
            }
            else
            {
                msSQL += " numnerofaactive_fig='" + values.numnerofaactive_fig.Replace("'", "") + "',";
            }
            if (values.existinglending_directindirect == null || values.existinglending_directindirect == "")
            {
                msSQL += " existinglending_directandindirect=null,";
            }
            else
            {
                msSQL += " existinglending_directandindirect='" + values.existinglending_directindirect.Replace("'", "") + "',";
            }
            if (values.nonnegotiableconditions_met == null || values.nonnegotiableconditions_met == "")
            {
                msSQL += " nonnegotiableconditions_met=null,";
            }
            else
            {
                msSQL += " nonnegotiableconditions_met='" + values.nonnegotiableconditions_met.Replace("'", "") + "',";
            }
            if (values.outstandingportfolio_directindirect == null || values.outstandingportfolio_directindirect == "")
            {
                msSQL += " outstandingportfolio_directandindirect=null,";
            }
            else
            {
                msSQL += " outstandingportfolio_directandindirect='" + values.outstandingportfolio_directindirect.Replace("'", "") + "',";
            }
            if (values.institution_directindrectborrowing == null || values.institution_directindrectborrowing == "")
            {
                msSQL += " institution_directandindrectborrowing=null,";
            }
            else
            {
                msSQL += " institution_directandindrectborrowing='" + values.institution_directindrectborrowing.Replace("'", "") + "',";
            }
            if (values.totaldisbursements_otherlenders == null || values.totaldisbursements_otherlenders == "")
            {
                msSQL += " totaldisbursements_otherlenders=null,";
            }
            else
            {
                msSQL += " totaldisbursements_otherlenders='" + values.totaldisbursements_otherlenders.Replace("'", "") + "',";
            }
            if (values.par90_managedbyonlyinstitution_direct == null || values.par90_managedbyonlyinstitution_direct == "")
            {
                msSQL += " par90_managedbyonlyinstitution_direct=null,";
            }
            else
            {
                msSQL += " par90_managedbyonlyinstitution_direct='" + values.par90_managedbyonlyinstitution_direct.Replace("'", "") + "',";
            }
            if (values.par90_managedbyonlyinstitution_direct == null || values.par90_managedbyonlyinstitution_direct == "")
            {
                msSQL += " par90_managedbyonlyinstitution_direct=null,";
            }
            else
            {
                msSQL += " par90_managedbyonlyinstitution_direct='" + values.par90_managedbyonlyinstitution_direct.Replace("'", "") + "',";
            }
            if (values.recommendation1 == null || values.recommendation1 == "")
            {
                msSQL += " recommendation2=null,";
            }
            else
            {
                msSQL += " recommendation2='" + values.recommendation1.Replace("'", "") + "',";
            }
            msSQL += " gradingdraft_flag = 'Y', " +
                     " statusupdated_by = '" + values.statusupdated_by + "' " +
                     "where application2gradingtool_gid='" + values.application2gradingtool_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update ocs_mst_tapplication2gradingassessmentcriteria set application2gradingtool_gid ='" + values.application2gradingtool_gid + "' where application2gradingtool_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " update ocs_mst_tapplication2geographic set " +
           " numberofstates='" + values.numberofstates + "'," +
           " numberofdistricts='" + values.numberofdistricts + "'," +
            " numberofbranches='" + values.numberofbranches + "'," +
               " numberofmembers='" + values.numberofmembers + "'," +
               " numberof_activemembers='" + values.numberof_activemembers + "'," +
               " numberofgroups='" + values.numberofgroups + "'," +
               " zonaloffices='" + values.zonaloffices + "'," +
               " regionaloffices='" + values.regionaloffices + "'," +
               " branches='" + values.branches + "'," +
               " adminstaff='" + values.adminstaff + "'," +
               " fieldstaff='" + values.fieldstaff + "'," +
               " fieldstaff_ratio='" + values.fieldstaff_ratio + "'," +
               " gradingdraft_flag='N'," +
               " statusupdated_by = '" + values.statusupdated_by + "' " +
               " where application2gradingtool_gid='" + values.application2gradingtool_gid + "'";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Assessed Score Details Saved Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Saving";
                return false;
            }
        }

        public bool DaSubmitGradingToolDetails(string employee_gid, Mdlgeographicdetails values)
        {
            msSQL = "select application2gradingtool_gid from ocs_mst_tapplication2gradingassessmentcriteria where application2gradingtool_gid='" + employee_gid + "' or application2gradingtool_gid ='" + values.application2gradingtool_gid + "'";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Add Assesment Criteria Details";
                return false;
            }
            else
            {
                objODBCDatareader.Close();
            }
            msSQL = "select application2gradingtool_gid from ocs_mst_tapplication2gradingtool where application2gradingtool_gid='" + values.application2gradingtool_gid + "'";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                msGetGid = objcmnfunctions.GetMasterGID("AP2G");
                msSQL = "insert into ocs_mst_tapplication2gradingtool (" +
                        " application2gradingtool_gid," +
                        " application_gid," +
                        " dateofsurvey," +
                        " overallfporating," +
                        " overallfpograde," +
                        " majorcrops," +
                        " alternativeincomesource," +
                        " objevtiveoffpo," +
                        " recommendation," +
                        " fpo_acscore," +
                        " numnerofaactive_fig," +
                        " existinglending_directandindirect," +
                        " nonnegotiableconditions_met," +
                        " outstandingportfolio_directandindirect," +
                        " institution_directandindrectborrowing," +
                        " totaldisbursements_otherlenders," +
                        " par90_managedbyonlyinstitution_direct," +
                        " recommendation2," +
                        " gradingdraft_flag," +
                        " statusupdated_by," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + values.application_gid + "',";
                if (values.dateofsurvey == null || values.dateofsurvey == "")
                {
                    msSQL += "null,";
                }
                else
                {
                    msSQL += "'" + Convert.ToDateTime(values.dateofsurvey).ToString("yyyy-MM-dd") + "',";
                }
                if (values.overallfporating == null || values.overallfporating == "")
                {
                    msSQL += "null,";
                }
                else
                {
                    msSQL += "'" + values.overallfporating.Replace("'", "") + "',";
                }
                if (values.overallfpograding == null || values.overallfpograding == "")
                {
                    msSQL += "null,";
                }
                else
                {
                    msSQL += "'" + values.overallfpograding.Replace("'", "") + "',";
                }
                if (values.majorcrops == null || values.majorcrops == "")
                {
                    msSQL += "null,";
                }
                else
                {
                    msSQL += "'" + values.majorcrops.Replace("'", "") + "',";
                }
                if (values.alternativeincomesource == null || values.alternativeincomesource == "")
                {
                    msSQL += "null,";
                }
                else
                {
                    msSQL += "'" + values.alternativeincomesource.Replace("'", "") + "',";
                }
                if (values.objevtiveoffpo == null || values.objevtiveoffpo == "")
                {
                    msSQL += "null,";
                }
                else
                {
                    msSQL += "'" + values.objevtiveoffpo.Replace("'", "") + "',";
                }
                if (values.recommendation == null || values.recommendation == "")
                {
                    msSQL += "null,";
                }
                else
                {
                    msSQL += "'" + values.recommendation.Replace("'", "") + "',";
                }
                if (values.fpo_acscore == null || values.fpo_acscore == "")
                {
                    msSQL += "null,";
                }
                else
                {
                    msSQL += "'" + values.fpo_acscore.Replace("'", "") + "',";
                }
                if (values.numnerofaactive_fig == null || values.numnerofaactive_fig == "")
                {
                    msSQL += "null,";
                }
                else
                {
                    msSQL += "'" + values.numnerofaactive_fig.Replace("'", "") + "',";
                }
                if (values.existinglending_directindirect == null || values.existinglending_directindirect == "")
                {
                    msSQL += "null,";
                }
                else
                {
                    msSQL += "'" + values.existinglending_directindirect.Replace("'", "") + "',";
                }
                if (values.nonnegotiableconditions_met == null || values.nonnegotiableconditions_met == "")
                {
                    msSQL += "null,";
                }
                else
                {
                    msSQL += "'" + values.nonnegotiableconditions_met.Replace("'", "") + "',";
                }
                if (values.outstandingportfolio_directindirect == null || values.outstandingportfolio_directindirect == "")
                {
                    msSQL += "null,";
                }
                else
                {
                    msSQL += "'" + values.outstandingportfolio_directindirect.Replace("'", "") + "',";
                }
                if (values.institution_directindrectborrowing == null || values.institution_directindrectborrowing == "")
                {
                    msSQL += "null,";
                }
                else
                {
                    msSQL += "'" + values.institution_directindrectborrowing.Replace("'", "") + "',";
                }
                if (values.totaldisbursements_otherlenders == null || values.totaldisbursements_otherlenders == "")
                {
                    msSQL += "null,";
                }
                else
                {
                    msSQL += "'" + values.totaldisbursements_otherlenders.Replace("'", "") + "',";
                }
                if (values.par90_managedbyonlyinstitution_direct == null || values.par90_managedbyonlyinstitution_direct == "")
                {
                    msSQL += "null,";
                }
                else
                {
                    msSQL += "'" + values.par90_managedbyonlyinstitution_direct.Replace("'", "") + "',";
                }

                if (values.recommendation1 == null || values.recommendation1 == "")
                {
                    msSQL += "null,";
                }
                else
                {
                    msSQL += "'" + values.recommendation1.Replace("'", "") + "',";
                }
                msSQL += "'N', " +
                       "'" + values.statusupdated_by + "'," +
                       "'" + employee_gid + "'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tapplication2gradingassessmentcriteria set application2gradingtool_gid ='" + msGetGid + "' where application2gradingtool_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else
            {
                objODBCDatareader.Close();
                msSQL = "select date_format(dateofsurvey,'%d-%m-%Y') as dateofsurvey from ocs_mst_tapplication2gradingtool where application2gradingtool_gid='" + values.application2gradingtool_gid + "'";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    if (values.dateofsurvey == (objODBCDatareader["dateofsurvey"].ToString()))
                    {
                        values.dateofsurvey = "exist";
                    }
                    objODBCDatareader.Close();

                }
                else
                {
                    objODBCDatareader.Close();
                }
                msSQL = "update ocs_mst_tapplication2gradingassessmentcriteria set application2gradingtool_gid ='" + values.application2gradingtool_gid + "' where application2gradingtool_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update ocs_mst_tapplication2gradingtool set ";

                if (values.dateofsurvey == null || values.dateofsurvey == "")
                {
                    msSQL += " dateofsurvey=null,";
                }
                else if (values.dateofsurvey == "exist")
                {

                }
                else
                {
                    msSQL += " dateofsurvey='" + Convert.ToDateTime(values.dateofsurvey).ToString("yyyy-MM-dd") + "',";
                }
                if (values.overallfporating == null || values.overallfporating == "")
                {
                    msSQL += " overallfporating=null,";
                }
                else
                {
                    msSQL += " overallfporating='" + values.overallfporating.Replace("'", "") + "',";
                }
                if (values.overallfpograding == null || values.overallfpograding == "")
                {
                    msSQL += " overallfpograde=null,";
                }
                else
                {
                    msSQL += " overallfpograde='" + values.overallfpograding.Replace("'", "") + "',";
                }
                if (values.majorcrops == null || values.majorcrops == "")
                {
                    msSQL += " majorcrops=null,";
                }
                else
                {
                    msSQL += " majorcrops='" + values.majorcrops.Replace("'", "") + "',";
                }
                if (values.alternativeincomesource == null || values.alternativeincomesource == "")
                {
                    msSQL += " alternativeincomesource=null,";
                }
                else
                {
                    msSQL += " alternativeincomesource='" + values.alternativeincomesource.Replace("'", "") + "',";
                }
                if (values.objevtiveoffpo == null || values.objevtiveoffpo == "")
                {
                    msSQL += " objevtiveoffpo=null,";
                }
                else
                {
                    msSQL += " objevtiveoffpo='" + values.objevtiveoffpo.Replace("'", "") + "',";
                }
                if (values.recommendation == null || values.recommendation == "")
                {
                    msSQL += " recommendation=null,";
                }
                else
                {
                    msSQL += " recommendation='" + values.recommendation.Replace("'", "") + "',";
                }
                if (values.fpo_acscore == null || values.fpo_acscore == "")
                {
                    msSQL += " fpo_acscore=null,";
                }
                else
                {
                    msSQL += " fpo_acscore='" + values.fpo_acscore.Replace("'", "") + "',";
                }
                if (values.numnerofaactive_fig == null || values.numnerofaactive_fig == "")
                {
                    msSQL += " numnerofaactive_fig=null,";
                }
                else
                {
                    msSQL += " numnerofaactive_fig='" + values.numnerofaactive_fig.Replace("'", "") + "',";
                }
                if (values.existinglending_directindirect == null || values.existinglending_directindirect == "")
                {
                    msSQL += " existinglending_directandindirect=null,";
                }
                else
                {
                    msSQL += " existinglending_directandindirect='" + values.existinglending_directindirect.Replace("'", "") + "',";
                }
                if (values.nonnegotiableconditions_met == null || values.nonnegotiableconditions_met == "")
                {
                    msSQL += " nonnegotiableconditions_met=null,";
                }
                else
                {
                    msSQL += " nonnegotiableconditions_met='" + values.nonnegotiableconditions_met.Replace("'", "") + "',";
                }
                if (values.outstandingportfolio_directindirect == null || values.outstandingportfolio_directindirect == "")
                {
                    msSQL += " outstandingportfolio_directandindirect=null,";
                }
                else
                {
                    msSQL += " outstandingportfolio_directandindirect='" + values.outstandingportfolio_directindirect.Replace("'", "") + "',";
                }
                if (values.institution_directindrectborrowing == null || values.institution_directindrectborrowing == "")
                {
                    msSQL += " institution_directandindrectborrowing=null,";
                }
                else
                {
                    msSQL += " institution_directandindrectborrowing='" + values.institution_directindrectborrowing.Replace("'", "") + "',";
                }
                if (values.totaldisbursements_otherlenders == null || values.totaldisbursements_otherlenders == "")
                {
                    msSQL += " totaldisbursements_otherlenders=null,";
                }
                else
                {
                    msSQL += " totaldisbursements_otherlenders='" + values.totaldisbursements_otherlenders.Replace("'", "") + "',";
                }
                if (values.par90_managedbyonlyinstitution_direct == null || values.par90_managedbyonlyinstitution_direct == "")
                {
                    msSQL += " par90_managedbyonlyinstitution_direct=null,";
                }
                else
                {
                    msSQL += " par90_managedbyonlyinstitution_direct='" + values.par90_managedbyonlyinstitution_direct.Replace("'", "") + "',";
                }
                if (values.par90_managedbyonlyinstitution_direct == null || values.par90_managedbyonlyinstitution_direct == "")
                {
                    msSQL += " par90_managedbyonlyinstitution_direct=null,";
                }
                else
                {
                    msSQL += " par90_managedbyonlyinstitution_direct='" + values.par90_managedbyonlyinstitution_direct.Replace("'", "") + "',";
                }
                if (values.recommendation1 == null || values.recommendation1 == "")
                {
                    msSQL += " recommendation2=null,";
                }
                else
                {
                    msSQL += " recommendation2='" + values.recommendation1.Replace("'", "") + "',";
                }
                msSQL += " gradingdraft_flag = 'N'," +
                         " statusupdated_by = '" + values.statusupdated_by + "' " +
                         "where application2gradingtool_gid='" + values.application2gradingtool_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            }

            msSQL = "select application2gradingtool_gid from ocs_mst_tapplication2geographic where application2gradingtool_gid='" + values.application2gradingtool_gid + "'";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == false)
            {

                objODBCDatareader.Close();
                msGetGid1 = objcmnfunctions.GetMasterGID("A2GD");
                msSQL = " insert into ocs_mst_tapplication2geographic(" +
                        " application2geographic_gid," +
                        " application2gradingtool_gid," +
                        " gradingdraft_flag," +
                        " numberofstates ," +
                        " numberofdistricts ," +
                        " numberofbranches ," +
                        " numberofmembers ," +
                        " numberof_activemembers ," +
                        " numberofgroups  ," +
                        " zonaloffices  ," +
                        " regionaloffices ," +
                        " branches ," +
                        " adminstaff ," +
                        " fieldstaff  ," +
                        " fieldstaff_ratio  ," +
                        " statusupdated_by  ," +
                        " created_by ," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid1 + "'," +
                         "'" + msGetGid + "'," +
                         "'N'," +
                        "'" + values.numberofstates + "'," +
                        "'" + values.numberofdistricts + "'," +
                        "'" + values.numberofbranches + "'," +
                         "'" + values.numberofmembers + "'," +
                        "'" + values.numberof_activemembers + "'," +
                        "'" + values.numberofgroups + "'," +
                         "'" + values.zonaloffices + "'," +
                        "'" + values.regionaloffices + "'," +
                        "'" + values.branches + "'," +
                         "'" + values.adminstaff + "'," +
                        "'" + values.fieldstaff + "'," +
                        "'" + values.fieldstaff_ratio + "'," +
                        "'" + values.statusupdated_by + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else
            {
                objODBCDatareader.Close();
                msSQL = " update ocs_mst_tapplication2geographic set " +
                   " numberofstates='" + values.numberofstates + "'," +
                   " numberofdistricts='" + values.numberofdistricts + "'," +
                    " numberofbranches='" + values.numberofbranches + "'," +
                       " numberofmembers='" + values.numberofmembers + "'," +
                       " numberof_activemembers='" + values.numberof_activemembers + "'," +
                       " numberofgroups='" + values.numberofgroups + "'," +
                       " zonaloffices='" + values.zonaloffices + "'," +
                       " regionaloffices='" + values.regionaloffices + "'," +
                       " branches='" + values.branches + "'," +
                       " adminstaff='" + values.adminstaff + "'," +
                       " fieldstaff='" + values.fieldstaff + "'," +
                       " fieldstaff_ratio='" + values.fieldstaff_ratio + "'," +
                       " statusupdated_by = '" + values.statusupdated_by + "' " +
                       " where application2gradingtool_gid='" + values.application2gradingtool_gid + "'";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Assessed Score Details Submitted Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Submitting";
                return false;
            }
        }

        public bool DaUpdateGradingToolDetails(string employee_gid, Mdlgeographicdetails values)
        {
            msSQL = "select application2gradingtool_gid from ocs_mst_tapplication2gradingassessmentcriteria where application2gradingtool_gid='" + employee_gid + "' or application2gradingtool_gid='" + values.application2gradingtool_gid + "'";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Add Assesment Criteria Details";
                return false;
            }
            else
            {
                objODBCDatareader.Close();
            }
            msSQL = "select date_format(dateofsurvey,'%d-%m-%Y') as dateofsurvey from ocs_mst_tapplication2gradingtool where application2gradingtool_gid='" + values.application2gradingtool_gid + "'";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                if (values.dateofsurvey == (objODBCDatareader["dateofsurvey"].ToString()))
                {
                    values.dateofsurvey = "exist";
                }
                objODBCDatareader.Close();

            }
            else
            {
                objODBCDatareader.Close();
            }
            msSQL = "update ocs_mst_tapplication2gradingassessmentcriteria set application2gradingtool_gid ='" + values.application2gradingtool_gid + "' where application2gradingtool_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " update ocs_mst_tapplication2gradingtool set ";

            if (values.dateofsurvey == null || values.dateofsurvey == "")
            {
                msSQL += " dateofsurvey=null,";
            }
            else if (values.dateofsurvey == "exist")
            {

            }
            else
            {
                msSQL += " dateofsurvey='" + Convert.ToDateTime(values.dateofsurvey).ToString("yyyy-MM-dd") + "',";
            }
            if (values.overallfporating == null || values.overallfporating == "")
            {
                msSQL += " overallfporating=null,";
            }
            else
            {
                msSQL += " overallfporating='" + values.overallfporating.Replace("'", "") + "',";
            }
            if (values.overallfpograding == null || values.overallfpograding == "")
            {
                msSQL += " overallfpograde=null,";
            }
            else
            {
                msSQL += " overallfpograde='" + values.overallfpograding.Replace("'", "") + "',";
            }
            if (values.majorcrops == null || values.majorcrops == "")
            {
                msSQL += " majorcrops=null,";
            }
            else
            {
                msSQL += " majorcrops='" + values.majorcrops.Replace("'", "") + "',";
            }
            if (values.alternativeincomesource == null || values.alternativeincomesource == "")
            {
                msSQL += " alternativeincomesource=null,";
            }
            else
            {
                msSQL += " alternativeincomesource='" + values.alternativeincomesource.Replace("'", "") + "',";
            }
            if (values.objevtiveoffpo == null || values.objevtiveoffpo == "")
            {
                msSQL += " objevtiveoffpo=null,";
            }
            else
            {
                msSQL += " objevtiveoffpo='" + values.objevtiveoffpo.Replace("'", "") + "',";
            }
            if (values.recommendation == null || values.recommendation == "")
            {
                msSQL += " recommendation=null,";
            }
            else
            {
                msSQL += " recommendation='" + values.recommendation.Replace("'", "") + "',";
            }
            if (values.fpo_acscore == null || values.fpo_acscore == "")
            {
                msSQL += " fpo_acscore=null,";
            }
            else
            {
                msSQL += " fpo_acscore='" + values.fpo_acscore.Replace("'", "") + "',";
            }
            if (values.numnerofaactive_fig == null || values.numnerofaactive_fig == "")
            {
                msSQL += " numnerofaactive_fig=null,";
            }
            else
            {
                msSQL += " numnerofaactive_fig='" + values.numnerofaactive_fig.Replace("'", "") + "',";
            }
            if (values.existinglending_directindirect == null || values.existinglending_directindirect == "")
            {
                msSQL += " existinglending_directandindirect=null,";
            }
            else
            {
                msSQL += " existinglending_directandindirect='" + values.existinglending_directindirect.Replace("'", "") + "',";
            }
            if (values.nonnegotiableconditions_met == null || values.nonnegotiableconditions_met == "")
            {
                msSQL += " nonnegotiableconditions_met=null,";
            }
            else
            {
                msSQL += " nonnegotiableconditions_met='" + values.nonnegotiableconditions_met.Replace("'", "") + "',";
            }
            if (values.outstandingportfolio_directindirect == null || values.outstandingportfolio_directindirect == "")
            {
                msSQL += " outstandingportfolio_directandindirect=null,";
            }
            else
            {
                msSQL += " outstandingportfolio_directandindirect='" + values.outstandingportfolio_directindirect.Replace("'", "") + "',";
            }
            if (values.institution_directindrectborrowing == null || values.institution_directindrectborrowing == "")
            {
                msSQL += " institution_directandindrectborrowing=null,";
            }
            else
            {
                msSQL += " institution_directandindrectborrowing='" + values.institution_directindrectborrowing.Replace("'", "") + "',";
            }
            if (values.totaldisbursements_otherlenders == null || values.totaldisbursements_otherlenders == "")
            {
                msSQL += " totaldisbursements_otherlenders=null,";
            }
            else
            {
                msSQL += " totaldisbursements_otherlenders='" + values.totaldisbursements_otherlenders.Replace("'", "") + "',";
            }
            if (values.par90_managedbyonlyinstitution_direct == null || values.par90_managedbyonlyinstitution_direct == "")
            {
                msSQL += " par90_managedbyonlyinstitution_direct=null,";
            }
            else
            {
                msSQL += " par90_managedbyonlyinstitution_direct='" + values.par90_managedbyonlyinstitution_direct.Replace("'", "") + "',";
            }
            if (values.par90_managedbyonlyinstitution_direct == null || values.par90_managedbyonlyinstitution_direct == "")
            {
                msSQL += " par90_managedbyonlyinstitution_direct=null,";
            }
            else
            {
                msSQL += " par90_managedbyonlyinstitution_direct='" + values.par90_managedbyonlyinstitution_direct.Replace("'", "") + "',";
            }
            if (values.recommendation1 == null || values.recommendation1 == "")
            {
                msSQL += " recommendation2=null,";
            }
            else
            {
                msSQL += " recommendation2='" + values.recommendation1.Replace("'", "") + "',";
            }
            msSQL += " gradingdraft_flag = 'N', " +
                     " statusupdated_by = '" + values.statusupdated_by + "' " +
                     " where application2gradingtool_gid='" + values.application2gradingtool_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " update ocs_mst_tapplication2geographic set " +
                 " numberofstates='" + values.numberofstates + "'," +
                 " numberofdistricts='" + values.numberofdistricts + "'," +
                  " numberofbranches='" + values.numberofbranches + "'," +
                     " numberofmembers='" + values.numberofmembers + "'," +
                     " numberof_activemembers='" + values.numberof_activemembers + "'," +
                     " numberofgroups='" + values.numberofgroups + "'," +
                     " zonaloffices='" + values.zonaloffices + "'," +
                     " regionaloffices='" + values.regionaloffices + "'," +
                     " branches='" + values.branches + "'," +
                     " adminstaff='" + values.adminstaff + "'," +
                     " fieldstaff='" + values.fieldstaff + "'," +
                     " fieldstaff_ratio='" + values.fieldstaff_ratio + "'," +
                     " statusupdated_by = '" + values.statusupdated_by + "' " +
                     " where application2gradingtool_gid='" + values.application2gradingtool_gid + "'";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Assessed Score Details Updated Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Updating";
                return false;
            }
        }
        
        public void DaDeleteGradingToolDetails(string application2gradingassesment_gid, Mdlgradingtool values)
        {
            msSQL = "delete from ocs_mst_tapplication2gradingassessmentcriteria where application2gradingassesment_gid='" + application2gradingassesment_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            msSQL = "delete from ocs_mst_tassessmentcriteria2dropdown where application2gradingassesment_gid='" + application2gradingassesment_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "Assessed Score Details Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured While Deleting Assessed Score Details";
                values.status = false;

            }
        }

        public void DaDeletetmpGradingTool(string employee_gid, Mdlgradingtool values)
        {
            msSQL = "select application2gradingassesment_gid from ocs_mst_tapplication2gradingassessmentcriteria where application2gradingtool_gid='" + employee_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                msSQL = "delete from ocs_mst_tassessmentcriteria2dropdown where application2gradingassesment_gid ='" + objODBCDatareader["application2gradingassesment_gid"].ToString() + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                objODBCDatareader.Close();
            }
            else
            {
                objODBCDatareader.Close();
            }

            msSQL = "delete from ocs_mst_tapplication2gradingassessmentcriteria where application2gradingtool_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
            }
            else
            {
                values.status = false;

            }
        }

        public void DaGetEditGradingTooltotal(string application2gradingtool_gid, Mdlgeographicdetails values)
        {
            msSQL = " SELECT a.application2gradingtool_gid,overallfporating,overallfpograde,majorcrops,alternativeincomesource,objevtiveoffpo, "+     
                    " recommendation,fpo_acscore,numnerofaactive_fig,existinglending_directandindirect, nonnegotiableconditions_met,outstandingportfolio_directandindirect, " +
                    " institution_directandindrectborrowing ,totaldisbursements_otherlenders,par90_managedbyonlyinstitution_direct,recommendation2,a.gradingdraft_flag, " +
                    " numberofstates,numberofdistricts,numberofbranches,numberofmembers,numberof_activemembers,numberofgroups,zonaloffices,regionaloffices,branches,adminstaff,fieldstaff,fieldstaff_ratio," +
                    " date_format(dateofsurvey,'%d-%m-%Y') as dateof_survey FROM ocs_mst_tapplication2gradingtool a" +
                    " left join ocs_mst_tapplication2geographic b on a.application2gradingtool_gid =b.application2gradingtool_gid " +
                    " where a.application2gradingtool_gid='" + application2gradingtool_gid + "' ";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.application2gradingtool_gid = objODBCDatareader["application2gradingtool_gid"].ToString();
                values.dateofsurvey = objODBCDatareader["dateof_survey"].ToString();
                values.overallfporating = objODBCDatareader["overallfporating"].ToString();
                values.overallfpograding = objODBCDatareader["overallfpograde"].ToString();
                values.majorcrops = objODBCDatareader["majorcrops"].ToString();
                values.alternativeincomesource = objODBCDatareader["alternativeincomesource"].ToString();
                values.objevtiveoffpo = objODBCDatareader["objevtiveoffpo"].ToString();
                values.recommendation = objODBCDatareader["recommendation"].ToString();
                values.fpo_acscore = objODBCDatareader["fpo_acscore"].ToString();
                values.numnerofaactive_fig = objODBCDatareader["numnerofaactive_fig"].ToString();
                values.existinglending_directindirect = objODBCDatareader["existinglending_directandindirect"].ToString();
                values.nonnegotiableconditions_met = objODBCDatareader["nonnegotiableconditions_met"].ToString();
                values.outstandingportfolio_directindirect = objODBCDatareader["outstandingportfolio_directandindirect"].ToString();
                values.institution_directindrectborrowing = objODBCDatareader["institution_directandindrectborrowing"].ToString();
                values.totaldisbursements_otherlenders = objODBCDatareader["totaldisbursements_otherlenders"].ToString();
                values.par90_managedbyonlyinstitution_direct = objODBCDatareader["par90_managedbyonlyinstitution_direct"].ToString();
                values.recommendation1 = objODBCDatareader["recommendation2"].ToString();
                values.gradingdraft_flag = objODBCDatareader["gradingdraft_flag"].ToString();
                values.numberofstates = objODBCDatareader["numberofstates"].ToString();
                values.numberofdistricts = objODBCDatareader["numberofdistricts"].ToString();
                values.numberofbranches = objODBCDatareader["numberofbranches"].ToString();
                values.numberofmembers = objODBCDatareader["numberofmembers"].ToString();
                values.numberof_activemembers = objODBCDatareader["numberof_activemembers"].ToString();
                values.numberofgroups = objODBCDatareader["numberofgroups"].ToString();
                values.zonaloffices = objODBCDatareader["zonaloffices"].ToString();
                values.regionaloffices = objODBCDatareader["regionaloffices"].ToString();
                values.branches = objODBCDatareader["branches"].ToString();
                values.adminstaff = objODBCDatareader["adminstaff"].ToString();
                values.fieldstaff = objODBCDatareader["fieldstaff"].ToString();
                values.fieldstaff_ratio = objODBCDatareader["fieldstaff_ratio"].ToString();


            }
            objODBCDatareader.Close();



        }
    }
}