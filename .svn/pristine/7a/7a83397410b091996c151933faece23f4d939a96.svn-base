using ems.master.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Web;

namespace ems.master.DataAccess
{
    public class DaTrnRuleEngine
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        DataTable dt_datatable;
        OdbcDataReader objODBCDatareader;
        string msSQL, msGetGid, msGetGid1, lsrule_function, ruleengine_result, ruleengineresult_details, 
               lsbureau_score, lstemplate_bureauscore, lsrule_id, lstemplate_code, lsruleenginemaster_gid, lsapplicant_age, lstemplate_age,
            lscinno_applicant, lscompany_name, lsregisteredaddress_applicant, lslicensetype_name, lsoverallfporating, lstotallandinacres, lsloanpurpose_name,
            lsmajorcrops,lscultivatedland, lsdate_incorporation, lsmonth_diff, dateincorporation_monthdiff;
        int mnResult, template_bureauscore, application_bureauscore,applicant_age, template_age, month_difference;
        
        
        DaBRETemplateSAN objDaBRETemplateSAN = new DaBRETemplateSAN();

        //Submit Configured Values of Template
        public void DaSubmitConfiguredValuesofTemplate(MdlMstRuleEngine values, string user_gid)
        {
            int count = 0, get_count=0;
            var lsruletemplateparameter_gid = values.ruletemplateparameter_gid;
            var lsrule_parameter = values.rule_parameter;

            var lsruletemplateparameter_gidAndlsrule_parameter = lsruletemplateparameter_gid.Zip(lsrule_parameter, (g, r) => new { Ruletemplateparameter_Gid = g, Rule_Parameter = r });
            foreach (var gr in lsruletemplateparameter_gidAndlsrule_parameter)
            {
                msSQL = " update ocs_trn_truletemplateparameter set " +
                        " rule_parameter='" + gr.Rule_Parameter + "'," +
                        " updated_by='" + user_gid + "'," +
                        " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                        " where ruletemplateparameter_gid='" + gr.Ruletemplateparameter_Gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                count = count + 1;
            }
            get_count=values.ruletemplateparameter_gid.Count();

            if (count == get_count)
            {
                values.status = true;
                values.message = "success";                
            }
            else
            {
                values.status = false;
                values.message = "failure";
            }
        }

        // Bureau Score of BoDs - Rule
        public void ChkBureauScrOfBoDs(RuleEngineFunctionParams values)
        {
            msSQL = " SELECT a.bureau_score from ocs_mst_tcontact2bureau a " +
                    " left join ocs_mst_tcontact b on a.contact_gid = b.contact_gid "+
                    " left join ocs_mst_tapplication c on b.application_gid = c.application_gid "+
                    " where c.application_gid ='" + values.application_gid + "' " +
                    " order by a.created_date desc limit 1 ";
            
            lsbureau_score = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " SELECT a.rule_parameter,b.template_code,a.ruleenginemaster_gid,a.rule_id FROM ocs_trn_truletemplateparameter a " +
                    " Left Join ocs_mst_truletemplatemaster b on a.ruletemplatemaster_gid = b.ruletemplatemaster_gid " +
                    " Left Join ocs_mst_truleenginemaster c on a.ruleenginemaster_gid = c.ruleenginemaster_gid " +                    
                    " where a.ruletemplatemaster_gid = '" + values.ruletemplatemaster_gid + "'" +
                    " and a.ruleenginemaster_gid = '" + values.ruleenginemaster_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lstemplate_bureauscore = objODBCDatareader["rule_parameter"].ToString();
                lsruleenginemaster_gid = objODBCDatareader["ruleenginemaster_gid"].ToString();
                lsrule_id = objODBCDatareader["rule_id"].ToString();
                lstemplate_code = objODBCDatareader["template_code"].ToString();
            }
            objODBCDatareader.Close();

            int.TryParse(lsbureau_score, out application_bureauscore);
            int.TryParse(lstemplate_bureauscore, out template_bureauscore);

            if (String.IsNullOrEmpty(lsbureau_score) || String.IsNullOrEmpty(lstemplate_bureauscore))
            {
                ruleengine_result = "DataNotAvailable";
                ruleengineresult_details = "application_bureauscore = " + lsbureau_score + "template_bureauscore =" + lstemplate_bureauscore + "";
            }
            else
            {
                if (application_bureauscore >= template_bureauscore)
                {
                    ruleengine_result = "Success";
                }
                else
                {
                    ruleengine_result = "Failed";
                }
                ruleengineresult_details = "application_bureauscore = " + application_bureauscore + " / template_bureauscore =" + template_bureauscore + "";
            }
          
            msGetGid1 = objcmnfunctions.GetMasterGID("RERD");
           
            msSQL = " insert into ocs_trn_tpostruleenginerundetails(" +
                 " postruleenginerundetails_gid," +
                 " postruleenginerun_gid," +
                 " application_gid," +
                 " ruletemplatemaster_gid ," +
                 " template_code," +
                 " ruleenginemaster_gid ," +
                 " rule_id," +
                 " ruleengine_result," +
                 " ruleengineresult_details)" +
                 " values(" +
                 "'" + msGetGid1 + "'," +
                 "'" + values.postruleenginerun_gid + "'," +
                 "'" + values.application_gid + "'," +
                 "'" + values.ruletemplatemaster_gid + "'," +
                 "'" + lstemplate_code + "'," +
                 "'" + lsruleenginemaster_gid + "'," +
                 "'" + lsrule_id + "'," +
                 "'" + ruleengine_result + "'," +
                 "'" + ruleengineresult_details + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
        }


        // Check Maximum Age Limit of Applicant - Rule 
        public void ChkMaxAgeLimitOfApplicant(RuleEngineFunctionParams values)
        {
            msSQL = " SELECT age FROM ocs_mst_tcontact " +
                        " where application_gid='" + values.application_gid + "' ";
            lsapplicant_age = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " SELECT a.rule_parameter,b.template_code,a.ruleenginemaster_gid,a.rule_id FROM ocs_trn_truletemplateparameter a " +
                    " Left Join ocs_mst_truletemplatemaster b on a.ruletemplatemaster_gid = b.ruletemplatemaster_gid " +
                    " Left Join ocs_mst_truleenginemaster c on a.ruleenginemaster_gid = c.ruleenginemaster_gid " +
                    " where a.ruletemplatemaster_gid = '" + values.ruletemplatemaster_gid + "'" +
                    " and a.ruleenginemaster_gid = '" + values.ruleenginemaster_gid + "'";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lstemplate_age = objODBCDatareader["rule_parameter"].ToString();
                lsruleenginemaster_gid = objODBCDatareader["ruleenginemaster_gid"].ToString();
                lsrule_id = objODBCDatareader["rule_id"].ToString();
                lstemplate_code = objODBCDatareader["template_code"].ToString();
            }
            objODBCDatareader.Close();
   
            int.TryParse(lsapplicant_age, out applicant_age);
            int.TryParse(lstemplate_age, out template_age);

            if (String.IsNullOrEmpty(lsapplicant_age) || String.IsNullOrEmpty(lstemplate_age))
            {
                ruleengine_result = "DataNotAvailable";
                ruleengineresult_details = "applicant_age = " + lsapplicant_age +" / template_age =" + lstemplate_age + "";
            }
            else
            {
                if (applicant_age <= template_age)
                {
                    ruleengine_result = "Success";
                }
                else
                {
                    ruleengine_result = "Failed";
                }
                ruleengineresult_details = "applicant_age = " + applicant_age + " / template_age =" + template_age + "";
            }
                  
            msGetGid1 = objcmnfunctions.GetMasterGID("RERD");
            
            msSQL = " insert into ocs_trn_tpostruleenginerundetails(" +
                 " postruleenginerundetails_gid," +
                 " postruleenginerun_gid," +
                 " application_gid," +
                 " ruletemplatemaster_gid ," +
                 " template_code," +
                 " ruleenginemaster_gid ," +
                 " rule_id," +
                 " ruleengine_result," +
                 " ruleengineresult_details)" +
                 " values(" +
                 "'" + msGetGid1 + "'," +
                 "'" + values.postruleenginerun_gid + "'," +
                 "'" + values.application_gid + "'," +
                 "'" + values.ruletemplatemaster_gid + "'," +
                 "'" + lstemplate_code + "'," +
                 "'" + lsruleenginemaster_gid + "'," +
                 "'" + lsrule_id + "'," +
                 "'" + ruleengine_result + "'," +
                 "'" + ruleengineresult_details + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
        }

        // Capture CIN
        public void CaptureCIN(RuleEngineFunctionParams values)
        {
            msSQL = " SELECT cin_no from ocs_mst_tinstitution  " +                    
                    " where application_gid ='" + values.application_gid + "' ";
            lscinno_applicant = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " SELECT a.rule_parameter,b.template_code,a.ruleenginemaster_gid,a.rule_id FROM ocs_trn_truletemplateparameter a " +
                   " Left Join ocs_mst_truletemplatemaster b on a.ruletemplatemaster_gid = b.ruletemplatemaster_gid " +
                   " Left Join ocs_mst_truleenginemaster c on a.ruleenginemaster_gid = c.ruleenginemaster_gid " +
                   " where a.ruletemplatemaster_gid = '" + values.ruletemplatemaster_gid + "'" +
                   " and a.ruleenginemaster_gid = '" + values.ruleenginemaster_gid + "'";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {               
                lsruleenginemaster_gid = objODBCDatareader["ruleenginemaster_gid"].ToString();
                lsrule_id = objODBCDatareader["rule_id"].ToString();
                lstemplate_code = objODBCDatareader["template_code"].ToString();
            }
            objODBCDatareader.Close();

            if (String.IsNullOrEmpty(lscinno_applicant))
            {
                ruleengine_result = "DataNotAvailable";
                ruleengineresult_details = "CIN No is not available for given Application ID";
            }
            else
            {
                ruleengine_result = lscinno_applicant;
                ruleengineresult_details = "application_cin no = " + lscinno_applicant +"";
            }

            msGetGid1 = objcmnfunctions.GetMasterGID("RERD");

            msSQL = " insert into ocs_trn_tpostruleenginerundetails(" +
                 " postruleenginerundetails_gid," +
                 " postruleenginerun_gid," +
                 " application_gid," +
                 " ruletemplatemaster_gid ," +
                 " template_code," +
                 " ruleenginemaster_gid ," +
                 " rule_id," +
                 " ruleengine_result," +
                 " ruleengineresult_details)" +
                 " values(" +
                 "'" + msGetGid1 + "'," +
                 "'" + values.postruleenginerun_gid + "'," +
                 "'" + values.application_gid + "'," +
                 "'" + values.ruletemplatemaster_gid + "'," +
                 "'" + lstemplate_code + "'," +
                 "'" + lsruleenginemaster_gid + "'," +
                 "'" + lsrule_id + "'," +
                 "'" + ruleengine_result + "'," +
                 "'" + ruleengineresult_details + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
        }

        // Capture Company Name
        public void CaptureCompanyName(RuleEngineFunctionParams values)
        {
            msSQL = " SELECT company_name from ocs_mst_tinstitution  " +
                    " where application_gid ='" + values.application_gid + "' ";
            lscompany_name = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " SELECT a.rule_parameter,b.template_code,a.ruleenginemaster_gid,a.rule_id FROM ocs_trn_truletemplateparameter a " +
                  " Left Join ocs_mst_truletemplatemaster b on a.ruletemplatemaster_gid = b.ruletemplatemaster_gid " +
                  " Left Join ocs_mst_truleenginemaster c on a.ruleenginemaster_gid = c.ruleenginemaster_gid " +
                  " where a.ruletemplatemaster_gid = '" + values.ruletemplatemaster_gid + "'" +
                  " and a.ruleenginemaster_gid = '" + values.ruleenginemaster_gid + "'";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsruleenginemaster_gid = objODBCDatareader["ruleenginemaster_gid"].ToString();
                lsrule_id = objODBCDatareader["rule_id"].ToString();
                lstemplate_code = objODBCDatareader["template_code"].ToString();
            }
            objODBCDatareader.Close();

            if (String.IsNullOrEmpty(lscompany_name))
            {
                ruleengine_result = "DataNotAvailable";
                ruleengineresult_details = "Company Name is not available for given Application ID";
            }
            else
            {
                ruleengine_result = lscompany_name;
                ruleengineresult_details = "application_company name = " + lscompany_name + "";
            }

            msGetGid1 = objcmnfunctions.GetMasterGID("RERD");

            msSQL = " insert into ocs_trn_tpostruleenginerundetails(" +
                 " postruleenginerundetails_gid," +
                 " postruleenginerun_gid," +
                 " application_gid," +
                 " ruletemplatemaster_gid ," +
                 " template_code," +
                 " ruleenginemaster_gid ," +
                 " rule_id," +
                 " ruleengine_result," +
                 " ruleengineresult_details)" +
                 " values(" +
                 "'" + msGetGid1 + "'," +
                 "'" + values.postruleenginerun_gid + "'," +
                 "'" + values.application_gid + "'," +
                 "'" + values.ruletemplatemaster_gid + "'," +
                 "'" + lstemplate_code + "'," +
                 "'" + lsruleenginemaster_gid + "'," +
                 "'" + lsrule_id + "'," +
                 "'" + ruleengine_result + "'," +
                 "'" + ruleengineresult_details + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
        }

        // Capture Registered Address
        public void CaptureRegisteredAddress(RuleEngineFunctionParams values)
        {
            msSQL = " SELECT concat(addressline1,' ',addressline2,' ',city,' ',state,' ',postal_code) as registered_address  " +
                       " FROM ocs_mst_tinstitution2address a" +
                       " left join ocs_mst_tinstitution b on a.institution_gid=b.institution_gid" +
                       " left join ocs_mst_tapplication c on b.application_gid=c.application_gid " +
                       " where c.application_gid ='" + values.application_gid + "' ";

            lsregisteredaddress_applicant = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " SELECT a.rule_parameter,b.template_code,a.ruleenginemaster_gid,a.rule_id FROM ocs_trn_truletemplateparameter a " +
                  " Left Join ocs_mst_truletemplatemaster b on a.ruletemplatemaster_gid = b.ruletemplatemaster_gid " +
                  " Left Join ocs_mst_truleenginemaster c on a.ruleenginemaster_gid = c.ruleenginemaster_gid " +
                  " where a.ruletemplatemaster_gid = '" + values.ruletemplatemaster_gid + "'" +
                  " and a.ruleenginemaster_gid = '" + values.ruleenginemaster_gid + "'";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsruleenginemaster_gid = objODBCDatareader["ruleenginemaster_gid"].ToString();
                lsrule_id = objODBCDatareader["rule_id"].ToString();
                lstemplate_code = objODBCDatareader["template_code"].ToString();
            }
            objODBCDatareader.Close();

            if (String.IsNullOrEmpty(lsregisteredaddress_applicant))
            {
                ruleengine_result = "DataNotAvailable";
                ruleengineresult_details = "Registered Address is not available for given Application ID";
            }
            else
            {
                ruleengine_result = lsregisteredaddress_applicant;
                ruleengineresult_details = "Registered Address = " + lsregisteredaddress_applicant + "";
            }

            msGetGid1 = objcmnfunctions.GetMasterGID("RERD");

            msSQL = " insert into ocs_trn_tpostruleenginerundetails(" +
                 " postruleenginerundetails_gid," +
                 " postruleenginerun_gid," +
                 " application_gid," +
                 " ruletemplatemaster_gid ," +
                 " template_code," +
                 " ruleenginemaster_gid ," +
                 " rule_id," +
                 " ruleengine_result," +
                 " ruleengineresult_details)" +
                 " values(" +
                 "'" + msGetGid1 + "'," +
                 "'" + values.postruleenginerun_gid + "'," +
                 "'" + values.application_gid + "'," +
                 "'" + values.ruletemplatemaster_gid + "'," +
                 "'" + lstemplate_code + "'," +
                 "'" + lsruleenginemaster_gid + "'," +
                 "'" + lsrule_id + "'," +
                 "'" + ruleengine_result + "'," +
                 "'" + ruleengineresult_details + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
        }

        //Capture License Type
        public void CaptureLicenseType(RuleEngineFunctionParams values)
        {
            msSQL = " SELECT licensetype_name from ocs_mst_tinstitution  " +
                    " where application_gid ='" + values.application_gid + "' ";
            lslicensetype_name = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " SELECT a.rule_parameter,b.template_code,a.ruleenginemaster_gid,a.rule_id FROM ocs_trn_truletemplateparameter a " +
                  " Left Join ocs_mst_truletemplatemaster b on a.ruletemplatemaster_gid = b.ruletemplatemaster_gid " +
                  " Left Join ocs_mst_truleenginemaster c on a.ruleenginemaster_gid = c.ruleenginemaster_gid " +
                  " where a.ruletemplatemaster_gid = '" + values.ruletemplatemaster_gid + "'" +
                  " and a.ruleenginemaster_gid = '" + values.ruleenginemaster_gid + "'";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsruleenginemaster_gid = objODBCDatareader["ruleenginemaster_gid"].ToString();
                lsrule_id = objODBCDatareader["rule_id"].ToString();
                lstemplate_code = objODBCDatareader["template_code"].ToString();
            }
            objODBCDatareader.Close();

            if (String.IsNullOrEmpty(lslicensetype_name))
            {
                ruleengine_result = "DataNotAvailable";
                ruleengineresult_details = "License type is not available for given Application ID";
            }
            else
            {
                ruleengine_result = lslicensetype_name;
                ruleengineresult_details = "application_licensetype = " + lslicensetype_name + "";
            }

            msGetGid1 = objcmnfunctions.GetMasterGID("RERD");

            msSQL = " insert into ocs_trn_tpostruleenginerundetails(" +
                 " postruleenginerundetails_gid," +
                 " postruleenginerun_gid," +
                 " application_gid," +
                 " ruletemplatemaster_gid ," +
                 " template_code," +
                 " ruleenginemaster_gid ," +
                 " rule_id," +
                 " ruleengine_result," +
                 " ruleengineresult_details)" +
                 " values(" +
                 "'" + msGetGid1 + "'," +
                 "'" + values.postruleenginerun_gid + "'," +
                 "'" + values.application_gid + "'," +
                 "'" + values.ruletemplatemaster_gid + "'," +
                 "'" + lstemplate_code + "'," +
                 "'" + lsruleenginemaster_gid + "'," +
                 "'" + lsrule_id + "'," +
                 "'" + ruleengine_result + "'," +
                 "'" + ruleengineresult_details + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
        }

        //Capture FPO Grading Tool - Output
        public void CaptrFPOGradingToolOutput(RuleEngineFunctionParams values)
        {
            msSQL = " SELECT overallfporating from ocs_mst_tapplication2gradingtool  " +
                    " where application_gid ='" + values.application_gid + "' ";
            lsoverallfporating = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " SELECT a.rule_parameter,b.template_code,a.ruleenginemaster_gid,a.rule_id FROM ocs_trn_truletemplateparameter a " +
                  " Left Join ocs_mst_truletemplatemaster b on a.ruletemplatemaster_gid = b.ruletemplatemaster_gid " +
                  " Left Join ocs_mst_truleenginemaster c on a.ruleenginemaster_gid = c.ruleenginemaster_gid " +
                  " where a.ruletemplatemaster_gid = '" + values.ruletemplatemaster_gid + "'" +
                  " and a.ruleenginemaster_gid = '" + values.ruleenginemaster_gid + "'";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsruleenginemaster_gid = objODBCDatareader["ruleenginemaster_gid"].ToString();
                lsrule_id = objODBCDatareader["rule_id"].ToString();
                lstemplate_code = objODBCDatareader["template_code"].ToString();
            }
            objODBCDatareader.Close();

            if (String.IsNullOrEmpty(lsoverallfporating))
            {
                ruleengine_result = "DataNotAvailable";
                ruleengineresult_details = "Overallfporating is not available for given Application ID";
            }
            else
            {
                ruleengine_result = lsoverallfporating;
                ruleengineresult_details = "application_licensetype = " + lsoverallfporating + "";
            }

            msGetGid1 = objcmnfunctions.GetMasterGID("RERD");

            msSQL = " insert into ocs_trn_tpostruleenginerundetails(" +
                 " postruleenginerundetails_gid," +
                 " postruleenginerun_gid," +
                 " application_gid," +
                 " ruletemplatemaster_gid ," +
                 " template_code," +
                 " ruleenginemaster_gid ," +
                 " rule_id," +
                 " ruleengine_result," +
                 " ruleengineresult_details)" +
                 " values(" +
                 "'" + msGetGid1 + "'," +
                 "'" + values.postruleenginerun_gid + "'," +
                 "'" + values.application_gid + "'," +
                 "'" + values.ruletemplatemaster_gid + "'," +
                 "'" + lstemplate_code + "'," +
                 "'" + lsruleenginemaster_gid + "'," +
                 "'" + lsrule_id + "'," +
                 "'" + ruleengine_result + "'," +
                 "'" + ruleengineresult_details + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
        }

        // Capture Geography Covered
        public void CaptrGeographyCovered(RuleEngineFunctionParams values)
        {
            msSQL = " SELECT totallandinacres from ocs_mst_tcontact  " +
                    " where application_gid ='" + values.application_gid + "' ";
            lstotallandinacres = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " SELECT a.rule_parameter,b.template_code,a.ruleenginemaster_gid,a.rule_id FROM ocs_trn_truletemplateparameter a " +
                   " Left Join ocs_mst_truletemplatemaster b on a.ruletemplatemaster_gid = b.ruletemplatemaster_gid " +
                   " Left Join ocs_mst_truleenginemaster c on a.ruleenginemaster_gid = c.ruleenginemaster_gid " +
                   " where a.ruletemplatemaster_gid = '" + values.ruletemplatemaster_gid + "'" +
                   " and a.ruleenginemaster_gid = '" + values.ruleenginemaster_gid + "'";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsruleenginemaster_gid = objODBCDatareader["ruleenginemaster_gid"].ToString();
                lsrule_id = objODBCDatareader["rule_id"].ToString();
                lstemplate_code = objODBCDatareader["template_code"].ToString();
            }
            objODBCDatareader.Close();

            if (String.IsNullOrEmpty(lstotallandinacres))
            {
                ruleengine_result = "DataNotAvailable";
                ruleengineresult_details = "Total Land in Acres is not available for given Application ID";
            }
            else
            {
                ruleengine_result = lstotallandinacres;
                ruleengineresult_details = "Total Land in Acres = " + lstotallandinacres + "";
            }

            msGetGid1 = objcmnfunctions.GetMasterGID("RERD");

            msSQL = " insert into ocs_trn_tpostruleenginerundetails(" +
                 " postruleenginerundetails_gid," +
                 " postruleenginerun_gid," +
                 " application_gid," +
                 " ruletemplatemaster_gid ," +
                 " template_code," +
                 " ruleenginemaster_gid ," +
                 " rule_id," +
                 " ruleengine_result," +
                 " ruleengineresult_details)" +
                 " values(" +
                 "'" + msGetGid1 + "'," +
                 "'" + values.postruleenginerun_gid + "'," +
                 "'" + values.application_gid + "'," +
                 "'" + values.ruletemplatemaster_gid + "'," +
                 "'" + lstemplate_code + "'," +
                 "'" + lsruleenginemaster_gid + "'," +
                 "'" + lsrule_id + "'," +
                 "'" + ruleengine_result + "'," +
                 "'" + ruleengineresult_details + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
        }


        // Capture Loan Purpose
        public void CaptrLoanPurpose(RuleEngineFunctionParams values)
        {
            msSQL = " SELECT loanpurpose_name from ocs_mst_tloanpurpose  " +
                    " where application_gid ='" + values.application_gid + "' ";
            lsloanpurpose_name = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " SELECT a.rule_parameter,b.template_code,a.ruleenginemaster_gid,a.rule_id FROM ocs_trn_truletemplateparameter a " +
                   " Left Join ocs_mst_truletemplatemaster b on a.ruletemplatemaster_gid = b.ruletemplatemaster_gid " +
                   " Left Join ocs_mst_truleenginemaster c on a.ruleenginemaster_gid = c.ruleenginemaster_gid " +
                   " where a.ruletemplatemaster_gid = '" + values.ruletemplatemaster_gid + "'" +
                   " and a.ruleenginemaster_gid = '" + values.ruleenginemaster_gid + "'";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsruleenginemaster_gid = objODBCDatareader["ruleenginemaster_gid"].ToString();
                lsrule_id = objODBCDatareader["rule_id"].ToString();
                lstemplate_code = objODBCDatareader["template_code"].ToString();
            }
            objODBCDatareader.Close();

            if (String.IsNullOrEmpty(lsloanpurpose_name))
            {
                ruleengine_result = "DataNotAvailable";
                ruleengineresult_details = "Loan Purpose is not available for given Application ID";
            }
            else
            {
                ruleengine_result = lsloanpurpose_name;
                ruleengineresult_details = "Loan Purpose is" + lsloanpurpose_name + "";
            }

            msGetGid1 = objcmnfunctions.GetMasterGID("RERD");

            msSQL = " insert into ocs_trn_tpostruleenginerundetails(" +
                 " postruleenginerundetails_gid," +
                 " postruleenginerun_gid," +
                 " application_gid," +
                 " ruletemplatemaster_gid ," +
                 " template_code," +
                 " ruleenginemaster_gid ," +
                 " rule_id," +
                 " ruleengine_result," +
                 " ruleengineresult_details)" +
                 " values(" +
                 "'" + msGetGid1 + "'," +
                 "'" + values.postruleenginerun_gid + "'," +
                 "'" + values.application_gid + "'," +
                 "'" + values.ruletemplatemaster_gid + "'," +
                 "'" + lstemplate_code + "'," +
                 "'" + lsruleenginemaster_gid + "'," +
                 "'" + lsrule_id + "'," +
                 "'" + ruleengine_result + "'," +
                 "'" + ruleengineresult_details + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
        }


        // Capture Major Crops and their Acreage
        public void CaptrMajorCropsandAcreage(RuleEngineFunctionParams values)
        {
            msSQL = " SELECT majorcrops from ocs_mst_tapplication2gradingtool  " +
                    " where application_gid ='" + values.application_gid + "' ";
            lsmajorcrops = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " SELECT cultivatedland from ocs_mst_tcontact  " +
                    " where application_gid ='" + values.application_gid + "' ";
            lscultivatedland = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " SELECT a.rule_parameter,b.template_code,a.ruleenginemaster_gid,a.rule_id FROM ocs_trn_truletemplateparameter a " +
                   " Left Join ocs_mst_truletemplatemaster b on a.ruletemplatemaster_gid = b.ruletemplatemaster_gid " +
                   " Left Join ocs_mst_truleenginemaster c on a.ruleenginemaster_gid = c.ruleenginemaster_gid " +
                   " where a.ruletemplatemaster_gid = '" + values.ruletemplatemaster_gid + "'" +
                   " and a.ruleenginemaster_gid = '" + values.ruleenginemaster_gid + "'";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsruleenginemaster_gid = objODBCDatareader["ruleenginemaster_gid"].ToString();
                lsrule_id = objODBCDatareader["rule_id"].ToString();
                lstemplate_code = objODBCDatareader["template_code"].ToString();
            }
            objODBCDatareader.Close();

            if (String.IsNullOrEmpty(lsmajorcrops) || String.IsNullOrEmpty(lscultivatedland))
            {
                ruleengine_result = "DataNotAvailable";
                ruleengineresult_details = "Major Crops and Acreage details is not available for given Application ID";
            }
            else
            {
                ruleengine_result = lsloanpurpose_name;
                ruleengineresult_details = "Major Crops = " + lsmajorcrops + " / Acreage =" + lscultivatedland + "";
            }

            msGetGid1 = objcmnfunctions.GetMasterGID("RERD");

            msSQL = " insert into ocs_trn_tpostruleenginerundetails(" +
                 " postruleenginerundetails_gid," +
                 " postruleenginerun_gid," +
                 " application_gid," +
                 " ruletemplatemaster_gid ," +
                 " template_code," +
                 " ruleenginemaster_gid ," +
                 " rule_id," +
                 " ruleengine_result," +
                 " ruleengineresult_details)" +
                 " values(" +
                 "'" + msGetGid1 + "'," +
                 "'" + values.postruleenginerun_gid + "'," +
                 "'" + values.application_gid + "'," +
                 "'" + values.ruletemplatemaster_gid + "'," +
                 "'" + lstemplate_code + "'," +
                 "'" + lsruleenginemaster_gid + "'," +
                 "'" + lsrule_id + "'," +
                 "'" + ruleengine_result + "'," +
                 "'" + ruleengineresult_details + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
        }

        // Capture Date Incorporation
        public void CaptureDateIncorporation(RuleEngineFunctionParams values)
        {
            msSQL = " SELECT date_format(date_incorporation,'%d-%m-%Y') as date_incorporation from ocs_mst_tinstitution  " +
                    " where application_gid ='" + values.application_gid + "' ";
            lsdate_incorporation = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " SELECT a.rule_parameter,b.template_code,a.ruleenginemaster_gid,a.rule_id FROM ocs_trn_truletemplateparameter a " +
                   " Left Join ocs_mst_truletemplatemaster b on a.ruletemplatemaster_gid = b.ruletemplatemaster_gid " +
                   " Left Join ocs_mst_truleenginemaster c on a.ruleenginemaster_gid = c.ruleenginemaster_gid " +
                   " where a.ruletemplatemaster_gid = '" + values.ruletemplatemaster_gid + "'" +
                   " and a.ruleenginemaster_gid = '" + values.ruleenginemaster_gid + "'";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsruleenginemaster_gid = objODBCDatareader["ruleenginemaster_gid"].ToString();
                lsrule_id = objODBCDatareader["rule_id"].ToString();
                lstemplate_code = objODBCDatareader["template_code"].ToString();
            }
            objODBCDatareader.Close();

            if (String.IsNullOrEmpty(lsdate_incorporation))
            {
                ruleengine_result = "DataNotAvailable";
                ruleengineresult_details = "Date Incorporation is not available for given Application ID";
            }
            else
            {
                ruleengine_result = lsdate_incorporation;
                ruleengineresult_details = "Date Incorporation is" + lsdate_incorporation + "";
            }

            msGetGid1 = objcmnfunctions.GetMasterGID("RERD");

            msSQL = " insert into ocs_trn_tpostruleenginerundetails(" +
                 " postruleenginerundetails_gid," +
                 " postruleenginerun_gid," +
                 " application_gid," +
                 " ruletemplatemaster_gid ," +
                 " template_code," +
                 " ruleenginemaster_gid ," +
                 " rule_id," +
                 " ruleengine_result," +
                 " ruleengineresult_details)" +
                 " values(" +
                 "'" + msGetGid1 + "'," +
                 "'" + values.postruleenginerun_gid + "'," +
                 "'" + values.application_gid + "'," +
                 "'" + values.ruletemplatemaster_gid + "'," +
                 "'" + lstemplate_code + "'," +
                 "'" + lsruleenginemaster_gid + "'," +
                 "'" + lsrule_id + "'," +
                 "'" + ruleengine_result + "'," +
                 "'" + ruleengineresult_details + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
        }

        public void CaptrCategoryformDateIncorporation(RuleEngineFunctionParams values)
        {

            msSQL = " SELECT date_format(date_incorporation,'%Y/%m/%d') from ocs_mst_tinstitution  " +
                    " where application_gid ='" + values.application_gid + "' ";
            var lsdate_incorporation = objdbconn.GetExecuteScalar(msSQL);


            msSQL = " SELECT a.rule_parameter,b.template_code,a.ruleenginemaster_gid,a.rule_id FROM ocs_trn_truletemplateparameter a " +
                   " Left Join ocs_mst_truletemplatemaster b on a.ruletemplatemaster_gid = b.ruletemplatemaster_gid " +
                   " Left Join ocs_mst_truleenginemaster c on a.ruleenginemaster_gid = c.ruleenginemaster_gid " +
                   " where a.ruletemplatemaster_gid = '" + values.ruletemplatemaster_gid + "'" +
                   " and a.ruleenginemaster_gid = '" + values.ruleenginemaster_gid + "'";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsruleenginemaster_gid = objODBCDatareader["ruleenginemaster_gid"].ToString();
                lsrule_id = objODBCDatareader["rule_id"].ToString();
                lstemplate_code = objODBCDatareader["template_code"].ToString();
            }
            objODBCDatareader.Close();



            if (String.IsNullOrEmpty(lsdate_incorporation))
            {
                ruleengine_result = "DataNotAvailable";
                ruleengineresult_details = "Data is not available to Calculate Category";
            }

            else
            {

                msSQL = "select TIMESTAMPDIFF( MONTH, ('" + lsdate_incorporation + "'), now() ) % 12 as month";
                dateincorporation_monthdiff = objdbconn.GetExecuteScalar(msSQL);


                int.TryParse(dateincorporation_monthdiff, out month_difference);

                if (month_difference >= 12)
                {
                    ruleengine_result = "Matured";
                }
                else if (month_difference >= 6 && month_difference <= 12)
                {
                    ruleengine_result = "Incubation";
                }
                else
                {
                    ruleengine_result = "Both Matured and Incubation Condition Failed";
                }
                ruleengineresult_details = "Month difference from Date Incorporation" + month_difference + "";
            }

            msGetGid1 = objcmnfunctions.GetMasterGID("RERD");

            msSQL = " insert into ocs_trn_tpostruleenginerundetails(" +
                 " postruleenginerundetails_gid," +
                 " postruleenginerun_gid," +
                 " application_gid," +
                 " ruletemplatemaster_gid ," +
                 " template_code," +
                 " ruleenginemaster_gid ," +
                 " rule_id," +
                 " ruleengine_result," +
                 " ruleengineresult_details)" +
                 " values(" +
                 "'" + msGetGid1 + "'," +
                 "'" + values.postruleenginerun_gid + "'," +
                 "'" + values.application_gid + "'," +
                 "'" + values.ruletemplatemaster_gid + "'," +
                 "'" + lstemplate_code + "'," +
                 "'" + lsruleenginemaster_gid + "'," +
                 "'" + lsrule_id + "'," +
                 "'" + ruleengine_result + "'," +
                 "'" + ruleengineresult_details + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
        }





        //Rule Engine Execute
        public void DaRuleEngineExecute(string user_gid,string ruletemplatemaster_gid, string application_gid, MdlTrnRuleEngine values)
        {


            Dictionary<string, Action<RuleEngineFunctionParams>> ruleenginemethods = new Dictionary<string, Action<RuleEngineFunctionParams>>();
            ruleenginemethods.Add("ChkMaxAgeLimitOfApplicant", ChkMaxAgeLimitOfApplicant);
            ruleenginemethods.Add("CaptureCIN", CaptureCIN);
            ruleenginemethods.Add("CaptureCompanyName", CaptureCompanyName);
            ruleenginemethods.Add("CaptureRegisteredAddress", CaptureRegisteredAddress);
            ruleenginemethods.Add("CaptureLicenseType", CaptureLicenseType);            
            ruleenginemethods.Add("CaptrFPOGradingToolOutput", CaptrFPOGradingToolOutput);
            ruleenginemethods.Add("CaptrGeographyCovered", CaptrGeographyCovered);
            ruleenginemethods.Add("CaptrLoanPurpose", CaptrLoanPurpose);
            ruleenginemethods.Add("CaptrMajorCropsandAcreage", CaptrMajorCropsandAcreage);
            ruleenginemethods.Add("CaptureDateIncorporation", CaptureDateIncorporation);
            ruleenginemethods.Add("CaptrCategoryformDateIncorporation",CaptrCategoryformDateIncorporation);
            ruleenginemethods.Add("ChkBureauScrOfBoDs", ChkBureauScrOfBoDs);
            ruleenginemethods.Add("ChkRequestLimit", objDaBRETemplateSAN.ChkRequestLimit);
            ruleenginemethods.Add("ChkProductType", objDaBRETemplateSAN.ChkProductType);
            ruleenginemethods.Add("ChkBODBureauScore", objDaBRETemplateSAN.ChkBODBureauScore);
            ruleenginemethods.Add("ChkEntityBureauScore", objDaBRETemplateSAN.ChkEntityBureauScore);
            ruleenginemethods.Add("ChkDocumentMOA", objDaBRETemplateSAN.ChkDocumentMOA);
            ruleenginemethods.Add("ChkDocumentAOA", objDaBRETemplateSAN.ChkDocumentAOA);
            ruleenginemethods.Add("ChkDocumentUpload", objDaBRETemplateSAN.ChkDocumentUpload);

            



            msSQL = " SELECT ruleenginemaster_gid,rule_id FROM ocs_trn_truletemplateparameter " +
                       " where ruletemplatemaster_gid='" + ruletemplatemaster_gid + "' ";

            dt_datatable = objdbconn.GetDataTable(msSQL);

            List<string> ruletemplatemastergid_list = new List<string>();

            ruletemplatemastergid_list = dt_datatable.AsEnumerable().Select(p => p.Field<string>("ruleenginemaster_gid")).ToList();

            msGetGid = objcmnfunctions.GetMasterGID("PRER");

            msSQL = " insert into ocs_trn_tpostruleenginerun(" +
                        " postruleenginerun_gid," +
                        " application_gid," +
                        " ruletemplatemaster_gid," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + application_gid + "'," +
                        "'" + ruletemplatemaster_gid + "'," +
                        "'" + user_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            foreach (var lsruleenginemaster_gid in ruletemplatemastergid_list)
            {
                msSQL = " SELECT rule_function FROM ocs_mst_truleenginemaster " +
                       " where ruleenginemaster_gid='" + lsruleenginemaster_gid + "' ";

                lsrule_function = objdbconn.GetExecuteScalar(msSQL);

                RuleEngineFunctionParams ruleEngineFunctionParams = new RuleEngineFunctionParams();
                ruleEngineFunctionParams.application_gid = application_gid;
                ruleEngineFunctionParams.ruletemplatemaster_gid = ruletemplatemaster_gid;
                ruleEngineFunctionParams.postruleenginerun_gid = msGetGid;
                ruleEngineFunctionParams.ruleenginemaster_gid = lsruleenginemaster_gid;

                ruleenginemethods[lsrule_function](ruleEngineFunctionParams);
            }

            if (mnResult == 1)
            {
                values.status = true;
                values.message = "Rule Engine Executed Successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Rule Engine Execution..!";
            }

        }
    }
}