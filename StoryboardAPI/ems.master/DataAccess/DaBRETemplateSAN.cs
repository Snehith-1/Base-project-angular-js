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
    public class DaBRETemplateSAN
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        DataTable dt_datatable;
        OdbcDataReader objODBCDatareader;
        string msSQL,msGetGid1, ruleengine_result, ruleengineresult_details,
                lsrule_id, lstemplate_code, lsruleenginemaster_gid; 
        string lsloanfacility_amount, lstemplate_loanfacilityamount, lsproduct_type, lsBODbureau_score, lstemplate_BODbureauscore,
            lsprobedocument_name, lstemplate_probedocumentname, lsEntitybureau_score, lstemplate_Entitybureauscore, lsdocument_title,
            lstemplate_documentname,lstemplate_document_title;
        int application_loanfacilityamount, template_loanfacilityamount, application_BODbureauscore, template_BODbureauscore,
            application_Entitybureauscore, template_Entitybureauscore;
        int mnResult;
    
     


        // Check Request Limit - Rule
        public void ChkRequestLimit(RuleEngineFunctionParams values)
        {
            msSQL = " SELECT loanfacility_amount from ocs_mst_tapplication2loan " +
                    " where application_gid ='" + values.application_gid + "' ";


            lsloanfacility_amount = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " SELECT a.rule_parameter,b.template_code,a.ruleenginemaster_gid,a.rule_id FROM ocs_trn_truletemplateparameter a " +
                    " Left Join ocs_mst_truletemplatemaster b on a.ruletemplatemaster_gid = b.ruletemplatemaster_gid " +
                    " Left Join ocs_mst_truleenginemaster c on a.ruleenginemaster_gid = c.ruleenginemaster_gid " +
                    " where a.ruletemplatemaster_gid = '" + values.ruletemplatemaster_gid + "'" +
                    " and a.ruleenginemaster_gid = '" + values.ruleenginemaster_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lstemplate_loanfacilityamount = objODBCDatareader["rule_parameter"].ToString();
                lsruleenginemaster_gid = objODBCDatareader["ruleenginemaster_gid"].ToString();
                lsrule_id = objODBCDatareader["rule_id"].ToString();
                lstemplate_code = objODBCDatareader["template_code"].ToString();
            }
            objODBCDatareader.Close();

            int.TryParse(lsloanfacility_amount, out application_loanfacilityamount);
            int.TryParse(lstemplate_loanfacilityamount, out template_loanfacilityamount);

            if (String.IsNullOrEmpty(lsloanfacility_amount))
            {
                ruleengine_result = "Failed";
                ruleengineresult_details = "Error in Program Selected ||  " +
                                           "Application Value = " + lsloanfacility_amount + "  || Template Value =" + lstemplate_loanfacilityamount + "";
            }
            else
            {
                if (application_loanfacilityamount <= template_loanfacilityamount)
                {
                    ruleengine_result = "Pass";
                }
                else
                {
                    ruleengine_result = "Failed";
                }
                ruleengineresult_details = "Application Value = " + lsloanfacility_amount + "  || Template Value =" + lstemplate_loanfacilityamount + "";
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


        // Check Product Type - Rule
        public void ChkProductType(RuleEngineFunctionParams values)
        {
            msSQL = " SELECT product_type from ocs_mst_tapplication2loan " +
                    " where application_gid ='" + values.application_gid + "' ";


            lsproduct_type = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " SELECT a.rule_parameter,b.template_code,a.ruleenginemaster_gid,a.rule_id FROM ocs_trn_truletemplateparameter a " +
                    " Left Join ocs_mst_truletemplatemaster b on a.ruletemplatemaster_gid = b.ruletemplatemaster_gid " +
                    " Left Join ocs_mst_truleenginemaster c on a.ruleenginemaster_gid = c.ruleenginemaster_gid " +
                    " where a.ruletemplatemaster_gid = '" + values.ruletemplatemaster_gid + "'" +
                    " and a.ruleenginemaster_gid = '" + values.ruleenginemaster_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lstemplate_loanfacilityamount = objODBCDatareader["rule_parameter"].ToString();
                lsruleenginemaster_gid = objODBCDatareader["ruleenginemaster_gid"].ToString();
                lsrule_id = objODBCDatareader["rule_id"].ToString();
                lstemplate_code = objODBCDatareader["template_code"].ToString();
            }
            objODBCDatareader.Close();

           

            if (String.IsNullOrEmpty(lsproduct_type))
            {
                ruleengine_result = "Failed";
                ruleengineresult_details = "Input License is Mandatory for Sub-Product Input  " +
                                           "Application Product type = " + lsproduct_type + "";
            }
            else
            {
            
                    ruleengine_result = "Pass";
                    ruleengineresult_details = "Application Product type = " + lsproduct_type + "";
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

        //Check BOD Bureau Score - Rule
        public void ChkBODBureauScore(RuleEngineFunctionParams values)
        {
            msSQL = " SELECT a.bureau_score from ocs_mst_tcontact2bureau a " +
                    " left join ocs_mst_tcontact b on a.contact_gid = b.contact_gid " +
                    " left join ocs_mst_tapplication c on b.application_gid = c.application_gid " +
                    " where c.application_gid ='" + values.application_gid + "' " +
                    " order by a.created_date desc limit 1 ";

            lsBODbureau_score = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " SELECT a.rule_parameter,b.template_code,a.ruleenginemaster_gid,a.rule_id FROM ocs_trn_truletemplateparameter a " +
                    " Left Join ocs_mst_truletemplatemaster b on a.ruletemplatemaster_gid = b.ruletemplatemaster_gid " +
                    " Left Join ocs_mst_truleenginemaster c on a.ruleenginemaster_gid = c.ruleenginemaster_gid " +
                    " where a.ruletemplatemaster_gid = '" + values.ruletemplatemaster_gid + "'" +
                    " and a.ruleenginemaster_gid = '" + values.ruleenginemaster_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lstemplate_BODbureauscore = objODBCDatareader["rule_parameter"].ToString();
                lsruleenginemaster_gid = objODBCDatareader["ruleenginemaster_gid"].ToString();
                lsrule_id = objODBCDatareader["rule_id"].ToString();
                lstemplate_code = objODBCDatareader["template_code"].ToString();
            }
            objODBCDatareader.Close();

            int.TryParse(lsBODbureau_score, out application_BODbureauscore);
            int.TryParse(lstemplate_BODbureauscore, out template_BODbureauscore);
            int credit_notavailable = -1;

            if (String.IsNullOrEmpty(lsBODbureau_score))
            {
                ruleengine_result = "Failed";
                ruleengineresult_details = "BoDs Bureau Score is not upto the mark || "+
                                           "Application BOD bureau score = Null || Template BOD bureau score =" + lstemplate_BODbureauscore + "";
            }
            else
            {
                if (application_BODbureauscore >= template_BODbureauscore )
                {
                    ruleengine_result = "Pass";
                    ruleengineresult_details = "Application BOD bureau score = " + application_BODbureauscore + " / Template BOD bureau score =" + template_BODbureauscore + "";
                }
                else if (application_BODbureauscore == credit_notavailable)
                {
                    ruleengine_result = "Failed";
                    ruleengineresult_details = "Credit record is not available";
                }
                else  
                {
                    ruleengine_result = "Failed";
                    ruleengineresult_details = "Application BOD bureau score = " + application_BODbureauscore + " / Template BOD bureau score =" + template_BODbureauscore + "";
                }
                ruleengineresult_details = "Application BOD bureau score = " + application_BODbureauscore + " / Template BOD bureau score =" + template_BODbureauscore + "";

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


        //Check Entity Bureau Score - Rule
        public void ChkEntityBureauScore(RuleEngineFunctionParams values)
        {
            msSQL = " SELECT a.bureau_score from ocs_mst_tinstitution2bureau a " +
                    " left join ocs_mst_tinstitution b on a.institution_gid = b.institution_gid " +
                    " left join ocs_mst_tapplication c on b.application_gid = c.application_gid " +
                    " where c.application_gid ='" + values.application_gid + "' " +
                    " order by a.created_date desc limit 1 ";

            lsEntitybureau_score = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " SELECT a.rule_parameter,b.template_code,a.ruleenginemaster_gid,a.rule_id FROM ocs_trn_truletemplateparameter a " +
                    " Left Join ocs_mst_truletemplatemaster b on a.ruletemplatemaster_gid = b.ruletemplatemaster_gid " +
                    " Left Join ocs_mst_truleenginemaster c on a.ruleenginemaster_gid = c.ruleenginemaster_gid " +
                    " where a.ruletemplatemaster_gid = '" + values.ruletemplatemaster_gid + "'" +
                    " and a.ruleenginemaster_gid = '" + values.ruleenginemaster_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lstemplate_Entitybureauscore = objODBCDatareader["rule_parameter"].ToString();
                lsruleenginemaster_gid = objODBCDatareader["ruleenginemaster_gid"].ToString();
                lsrule_id = objODBCDatareader["rule_id"].ToString();
                lstemplate_code = objODBCDatareader["template_code"].ToString();
            }
            objODBCDatareader.Close();

          


            if (String.IsNullOrEmpty(lsEntitybureau_score) || lsEntitybureau_score.Length < 5)
            {
                ruleengine_result = "Failed";
                ruleengineresult_details = "Entity Bureau Score is not upto the mark || " +
                                           "Application Entity bureau score = " + lsEntitybureau_score + " || Template Entity Bureau score =" + lstemplate_Entitybureauscore + "";
            }
            else
            {
                var first_3 = lsEntitybureau_score.Substring(0, 3);

                if (first_3=="CMR")
                {
                    var last_charapplication = lsEntitybureau_score.Substring(lsEntitybureau_score.Length - 1);

                    var last_chartemplate = lstemplate_Entitybureauscore.Substring(lstemplate_Entitybureauscore.Length - 1);



                    int.TryParse(last_charapplication, out application_Entitybureauscore);
                    int.TryParse(last_chartemplate, out template_Entitybureauscore);



                    if (application_Entitybureauscore <= template_Entitybureauscore)
                    {
                        ruleengine_result = "Pass";
                        ruleengineresult_details = "Application Bureau score = " + application_Entitybureauscore + " / Template Bureau score =" + template_Entitybureauscore + "";
                    }
                    else
                    {
                        ruleengine_result = "Failed";
                        ruleengineresult_details = "Entity Bureau Score is not upto the mark || Application Bureau score = " + application_Entitybureauscore + " / Template Bureau score =" + template_Entitybureauscore + "";
                    }                    
                }
                else
                {
                    ruleengine_result = "Failed";
                    ruleengineresult_details = "Entity Bureau Score is not upto the mark || Expected Application Bureau score format = CMR.. || Actual Application Bureau score = " + lsEntitybureau_score + "";
                }               
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

        // Check Document MOA - Rule
        public void ChkDocumentMOA(RuleEngineFunctionParams values)
        {

            String ChkDocumentMOA = "Document_MOA";

            msSQL = " SELECT a.document_title from ocs_mst_tinstitution2documentupload a " +
                  " left join ocs_mst_tinstitution b on a.institution_gid = b.institution_gid " +
                  " left join ocs_mst_tapplication c on b.application_gid = c.application_gid " +
                  " where c.application_gid ='" + values.application_gid + "'" +
                  " and a.document_title = '" + ChkDocumentMOA + "'" +
                  " order by a.created_date desc limit 1 ";

            lsdocument_title = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " SELECT a.rule_parameter,b.template_code,a.ruleenginemaster_gid,a.rule_id FROM ocs_trn_truletemplateparameter a " +
                    " Left Join ocs_mst_truletemplatemaster b on a.ruletemplatemaster_gid = b.ruletemplatemaster_gid " +
                    " Left Join ocs_mst_truleenginemaster c on a.ruleenginemaster_gid = c.ruleenginemaster_gid " +
                    " where a.ruletemplatemaster_gid = '" + values.ruletemplatemaster_gid + "'" +
                    " and a.ruleenginemaster_gid = '" + values.ruleenginemaster_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lstemplate_probedocumentname = objODBCDatareader["rule_parameter"].ToString();
                lsruleenginemaster_gid = objODBCDatareader["ruleenginemaster_gid"].ToString();
                lsrule_id = objODBCDatareader["rule_id"].ToString();
                lstemplate_code = objODBCDatareader["template_code"].ToString();
            }
            objODBCDatareader.Close();



            if (String.IsNullOrEmpty(lsdocument_title))
            {
                ruleengine_result = "Failed";
                ruleengineresult_details = "MOA Document not submitted";
                                           
            }
            else
            {

                ruleengine_result = "Pass";
                ruleengineresult_details = "Application Probe document = " + lsdocument_title + "";
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

        // Check Document AOA - Rule
        public void ChkDocumentAOA(RuleEngineFunctionParams values)
        {
            String ChkDocumentAOA = "Document_AOA";

            msSQL = " SELECT a.document_title from ocs_mst_tinstitution2documentupload a " +
                  " left join ocs_mst_tinstitution b on a.institution_gid = b.institution_gid " +
                  " left join ocs_mst_tapplication c on b.application_gid = c.application_gid " +
                  " where c.application_gid ='" + values.application_gid + "'" +
                  " and a.document_title = '" + ChkDocumentAOA + "'" +
                  " order by a.created_date desc limit 1 ";

            lsdocument_title = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " SELECT a.rule_parameter,b.template_code,a.ruleenginemaster_gid,a.rule_id FROM ocs_trn_truletemplateparameter a " +
                    " Left Join ocs_mst_truletemplatemaster b on a.ruletemplatemaster_gid = b.ruletemplatemaster_gid " +
                    " Left Join ocs_mst_truleenginemaster c on a.ruleenginemaster_gid = c.ruleenginemaster_gid " +
                    " where a.ruletemplatemaster_gid = '" + values.ruletemplatemaster_gid + "'" +
                    " and a.ruleenginemaster_gid = '" + values.ruleenginemaster_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lstemplate_document_title = objODBCDatareader["rule_parameter"].ToString();
                lsruleenginemaster_gid = objODBCDatareader["ruleenginemaster_gid"].ToString();
                lsrule_id = objODBCDatareader["rule_id"].ToString();
                lstemplate_code = objODBCDatareader["template_code"].ToString();
            }
            objODBCDatareader.Close();



            if (String.IsNullOrEmpty(lsdocument_title))
            {
                ruleengine_result = "Failed";
                ruleengineresult_details = "AOA Document not submitted";
                                           
            }
            else
            {

                ruleengine_result = "Pass";
                ruleengineresult_details = "Application Probe document = " + lsdocument_title + "";
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

        // Check Document Upload - Rule
        public void ChkDocumentUpload(RuleEngineFunctionParams values)
        {
          
            msSQL = " SELECT a.rule_parameter,b.template_code,a.ruleenginemaster_gid,a.rule_id FROM ocs_trn_truletemplateparameter a " +
                    " Left Join ocs_mst_truletemplatemaster b on a.ruletemplatemaster_gid = b.ruletemplatemaster_gid " +
                    " Left Join ocs_mst_truleenginemaster c on a.ruleenginemaster_gid = c.ruleenginemaster_gid " +
                    " where a.ruletemplatemaster_gid = '" + values.ruletemplatemaster_gid + "'" +
                    " and a.ruleenginemaster_gid = '" + values.ruleenginemaster_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lstemplate_documentname = objODBCDatareader["rule_parameter"].ToString();
                lsruleenginemaster_gid = objODBCDatareader["ruleenginemaster_gid"].ToString();
                lsrule_id = objODBCDatareader["rule_id"].ToString();
                lstemplate_code = objODBCDatareader["template_code"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " SELECT a.document_title from ocs_mst_tinstitution2documentupload a " +
                 " left join ocs_mst_tinstitution b on a.institution_gid = b.institution_gid " +
                 " left join ocs_mst_tapplication c on b.application_gid = c.application_gid " +
                  " where c.application_gid = '" + values.application_gid + "'" +
                  " and a.document_title = '" + lstemplate_documentname + "'";

            lsdocument_title = objdbconn.GetExecuteScalar(msSQL);


            if (String.IsNullOrEmpty(lsdocument_title))
            {
                ruleengine_result = "Failed";
                ruleengineresult_details = ""+ lstemplate_documentname + " Document not submitted";

            }
            else
            {

                ruleengine_result = "Pass";
                ruleengineresult_details = "Application document = " + lsdocument_title + "";
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
    }
}