using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data;
using System.Data.Odbc;
using ems.utilities.Functions;
using ems.rsk.Models;
using System.Configuration;
using ems.storage.Functions;

namespace ems.rsk.DataAccess
{
    public class DaVisitReport
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        OdbcDataReader objODBCDatareader;
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        DataTable dt_datatable;
        HttpPostedFile httpPostedFile;
        string msSQL, msGetGid, msGet_documentGid, msGet_visitGid;
        int mnResult;
        string lspath;
        string lsvisitreport_generateGid, msGet_VisitGid;

        public bool DaPostVisitReport(string employee_gid, visitreport values)
        {
            try
            {
                msSQL = "select count(*) as photocount from rsk_trn_tvisitreportphoto where allocationdtl_gid='" + values.allocationdtl_gid + "'";
                string lsphotocount = objdbconn.GetExecuteScalar(msSQL);

                if (values.report_status == "Completed" && Convert.ToInt16(lsphotocount) == 0)
                {
                    values.status = false;
                    values.message = "Kindly Upload the Visit Photo";
                    return false;
                }
                else
                {

                    msSQL = "select allocationdtl_gid from rsk_trn_tvisitreportgenerate where allocationdtl_gid='" + values.allocationdtl_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == false)
                    {
                        objODBCDatareader.Close();
                        msGetGid = objcmnfunctions.GetMasterGID("VRGD");

                        msSQL = "Insert into rsk_trn_tvisitreportgenerate(" +
                               " visitreport_generateGid ," +
                               " allocationdtl_gid," +
                               " customer_gid," +
                               " customer_name," +
                               " risk_code," +
                               " constitution," +
                               " visit_date," +
                               " dealing_withsince," +
                               " business_vintage," +
                               " typeof_loanvertical," +
                               " typeof_riskreview," +
                               " business_sector," +
                               " registeredoffice_address," +
                               " present_address," +
                               " contact_details1," +
                               " contact_details2," +
                               " visit_latitude ," +
                               " visit_longitude," +
                               " sanctioned_limit," +
                               " tenure_period," +
                               " primarysecondary_valuechain," +
                               " clientbusiness_vintage," +
                               " geneticcode_complied, " +
                               " RMD_visitedGid," +
                               " RMD_visitedname," +
                               " RM_name," +
                               " PPA_name," +
                               " credit_managername," +
                               " visit_done," +
                               " purpose_ofloan," +
                               " requestedamount_byclient," +
                               " sanctionedamount_byclient," +
                               " disbursement_date," +
                               " disbursement_amount," +
                               " totalloan_outstanding," +
                               " overdue," +
                               " repayment_track," +
                               " repayment_trackremarks," +
                               " basicrecords_maintain," +
                               " basicrecords_remarks," +
                               " turnover_lastFY," +
                               " presentFY_sales," +
                               " deferral_pendency," +
                               " total_noofGroups," +
                               " CBOfunded_noofGroups," +
                               " RMD_visitgroups," +
                               " borrower_commitment," +
                               " pending_documentation," +
                               " assetverification_createdoutofloan," +
                               " assetverification_securitydtls, " +
                               " assetverification_mortgaged," +
                               " assetverification_ROCcreation," +
                               " briefdtls_client," +
                               " purposeof_funding," +
                               " utilisation_details," +
                               " adequacy_loanamount, " +
                               " adequacy_impactassessment, " +
                               " adequacy_additional_funding," +
                               " overall_remarks," +
                               " PDD_compliance," +
                               " briefrpt_financials," +
                               " briefrpt_process," +
                               " briefrpt_customer," +
                               " briefrpt_learnings," +
                               " briefrpt_valuechain," +
                               " valuechain_mapanalysis," +
                               " competitorbusiness_segment," +
                               " portfolio_noofmembers," +
                               " portfolio_activemembers, " +
                               " total_disbursementamount, " +
                               " outstanding_ondate, " +
                               " overdue_beneficiary, " +
                               " overdue_amount," +
                               " overdueaccount_funding, " +
                               " report_status," +
                               " created_by," +
                               " created_date)" +
                               " values (" +
                               "'" + msGetGid + "'," +
                               "'" + values.allocationdtl_gid + "'," +
                               "'" + values.customer_gid + "'," +
                               "'" + values.customer_name + "',";
                        if (values.risk_code == null)
                        {
                            msSQL += "'',";

                        }
                        else
                        {
                            msSQL += "'" + values.risk_code + "',";
                        }
                        if (values.constitution == null)
                        {
                            msSQL += "'',";

                        }
                        else
                        {
                            msSQL += "'" + values.constitution.Replace("'", "\\'") + "',";
                        }
                        msSQL += "'" + Convert.ToDateTime(values.visitDate).ToString("yyyy-MM-dd") + "'," +
                             "'" + Convert.ToDateTime(values.relationship_Startedfrom).ToString("yyyy-MM-dd") + "',";
                        if (values.business_vintage == null)
                        {
                            msSQL += "'',";

                        }
                        else
                        {
                            msSQL += "'" + values.business_vintage.Replace("'", "\\'") + "',";
                        }
                        if (values.typeof_loanvertical == null)
                        {
                            msSQL += "'',";

                        }
                        else
                        {
                            msSQL += "'" + values.typeof_loanvertical + "',";
                        }
                        if (values.typeof_riskreview == null)
                        {
                            msSQL += "'',";

                        }
                        else
                        {
                            msSQL += "'" + values.typeof_riskreview + "',";
                        }
                        if (values.business_sector == null)
                        {
                            msSQL += "'',";

                        }
                        else
                        {
                            msSQL += "'" + values.business_sector + "',";
                        }
                        if (values.registeredoffice_address == null)
                        {
                            msSQL += "'',";

                        }
                        else
                        {
                            msSQL += "'" + values.registeredoffice_address.Replace("'", "\\'") + "',";
                        }
                        if (values.present_address == null)
                        {
                            msSQL += "'',";

                        }
                        else
                        {
                            msSQL += "'" + values.present_address.Replace("'", "\\'") + "',";
                        }
                        if (values.contact_details1 == null)
                        {
                            msSQL += "'',";

                        }
                        else
                        {
                            msSQL += "'" + values.contact_details1.Replace("'", "\\'") + "',";
                        }
                        if (values.contact_details2 == null)
                        {
                            msSQL += "'',";

                        }
                        else
                        {
                            msSQL += "'" + values.contact_details2.Replace("'", "\\'") + "',";
                        }
                        if (values.visit_latitude == null)
                        {
                            msSQL += "'',";

                        }
                        else
                        {
                            msSQL += "'" + values.visit_latitude.Replace("'", "\\'") + "',";
                        }
                        if (values.visit_longitude == null)
                        {
                            msSQL += "'',";

                        }
                        else
                        {
                            msSQL += "'" + values.visit_longitude.Replace("'", "\\'") + "',";
                        }
                        if (values.sanctioned_limit == null)
                        {
                            msSQL += "null,";
                        }
                        else
                        {
                            msSQL += "'" + values.sanctioned_limit.Replace("'", "\\'") + "',";
                        }
                        if (values.tenure_period == null)
                        {
                            msSQL += "'',";
                        }
                        else
                        {
                            msSQL += "'" + values.tenure_period.Replace("'", "\\'") + "',";
                        }
                        if (values.primarysecondary_valuechain == null)
                        {
                            msSQL += "'',";
                        }
                        else
                        {
                            msSQL += "'" + values.primarysecondary_valuechain.Replace("'", "\\'") + "',";
                        }
                        if (values.clientbusiness_vintage == null)
                        {
                            msSQL += "'',";
                        }
                        else
                        {
                            msSQL += "'" + values.clientbusiness_vintage.Replace("'", "\\'") + "',";
                        }

                        if (values.geneticcode_complied == null)
                        {
                            msSQL += "'',";
                        }
                        else
                        {
                            msSQL += "'" + values.geneticcode_complied + "',";
                        }
                        if (values.RMD_visitedGid == null)
                        {
                            msSQL += "'',";
                            msSQL += "'',";
                        }
                        else
                        {
                            msSQL += "'" + values.RMD_visitedGid + "',";
                            msSQL += "'" + values.RMD_visitedname + "',";
                        }
                        if (values.RM_name == null)
                        {
                            msSQL += "'',";
                        }
                        else
                        {
                            msSQL += "'" + values.RM_name + "',";
                        }
                        if (values.PPA_name == null)
                        {
                            msSQL += "'',";
                        }
                        else
                        {
                            msSQL += "'" + values.PPA_name + "',";
                        }
                        if (values.credit_managername == null)
                        {
                            msSQL += "'',";
                        }
                        else
                        {
                            msSQL += "'" + values.credit_managername + "',";
                        }
                        if (values.visit_done == null)
                        {
                            msSQL += "'',";
                        }
                        else
                        {
                            msSQL += "'" + values.visit_done + "',";
                        }
                        if (values.purpose_ofloan == null)
                        {
                            msSQL += "'',";
                        }
                        else
                        {
                            msSQL += "'" + values.purpose_ofloan.Replace("'", "\\'") + "',";
                        }
                        if (values.requestedamount_byclient == null || values.requestedamount_byclient == "")
                        {
                            msSQL += "null,";
                        }
                        else
                        {
                            msSQL += "'" + values.requestedamount_byclient + "',";
                        }
                        if (values.sanctionedamount_byclient == null)
                        {
                            msSQL += "'',";
                        }
                        else
                        {
                            msSQL += "'" + values.sanctionedamount_byclient.Replace("'", "\\'") + "',";
                        }
                        if (values.disbursement_Date == null)
                        {
                            msSQL += "'" + Convert.ToDateTime(values.disbursement_Date).ToString("yyyy-MM-dd") + "',";
                        }
                        else
                        {
                            msSQL += "'" + Convert.ToDateTime(values.disbursement_Date).ToString("yyyy-MM-dd") + "',";
                        }
                        if (values.disbursement_amount == null)
                        {
                            msSQL += "'',";
                        }
                        else
                        {
                            msSQL += "'" + values.disbursement_amount.Replace("'", "\\'") + "',";
                        }
                        if (values.totalloan_outstanding == null || values.totalloan_outstanding == "")
                        {
                            msSQL += "null,";
                        }
                        else
                        {
                            msSQL += "'" + values.totalloan_outstanding + "',";
                        }
                        if (values.overdue == null)
                        {
                            msSQL += "'',";
                        }
                        else
                        {
                            msSQL += "'" + values.overdue.Replace("'", "\\'") + "',";
                        }
                        if (values.repayment_track == null)
                        {
                            msSQL += "'',";
                        }
                        else
                        {
                            msSQL += "'" + values.repayment_track + "',";
                        }
                        if (values.repayment_trackremarks == null)
                        {
                            msSQL += "'',";
                        }
                        else
                        {
                            msSQL += "'" + values.repayment_trackremarks.Replace("'", "\\'") + "',";
                        }
                        if (values.basicrecords_maintain == null)
                        {
                            msSQL += "'',";
                        }
                        else
                        {
                            msSQL += "'" + values.basicrecords_maintain + "',";
                        }
                        if (values.basicrecords_remarks == null)
                        {
                            msSQL += "'',";
                        }
                        else
                        {
                            msSQL += "'" + values.basicrecords_remarks.Replace("'", "\\'") + "',";
                        }
                        if (values.turnover_lastFY == null || values.turnover_lastFY == "")
                        {
                            msSQL += "null,";
                        }
                        else
                        {
                            msSQL += "'" + values.turnover_lastFY + "',";
                        }
                        if (values.presentFY_sales == null || values.presentFY_sales == "")
                        {
                            msSQL += "null,";
                        }
                        else
                        {
                            msSQL += "'" + values.presentFY_sales + "',";
                        }
                        if (values.deferral_pendency == null)
                        {
                            msSQL += "'',";
                        }
                        else
                        {
                            msSQL += "'" + values.deferral_pendency.Replace("'", "\\'") + "',";
                        }
                        if (values.total_noofGroups == null)
                        {
                            msSQL += "'',";
                        }
                        else
                        {
                            msSQL += "'" + values.total_noofGroups.Replace("'", "\\'") + "',";
                        }
                        if (values.CBOfunded_noofGroups == null)
                        {
                            msSQL += "'',";
                        }
                        else
                        {
                            msSQL += "'" + values.CBOfunded_noofGroups.Replace("'", "\\'") + "',";
                        }
                        if (values.RMD_visitgroups == null)
                        {
                            msSQL += "'',";
                        }
                        else
                        {
                            msSQL += "'" + values.RMD_visitgroups.Replace("'", "\\'") + "',";
                        }
                        if (values.borrower_commitment == null)
                        {
                            msSQL += "'',";
                        }
                        else
                        {
                            msSQL += "'" + values.borrower_commitment.Replace("'", "\\'") + "',";
                        }
                        if (values.pending_documentation == null)
                        {
                            msSQL += "'',";
                        }
                        else
                        {
                            msSQL += "'" + values.pending_documentation.Replace("'", "\\'") + "',";
                        }
                        if (values.assetverification_createdoutofloan == null)
                        {
                            msSQL += "'',";
                        }
                        else
                        {
                            msSQL += "'" + values.assetverification_createdoutofloan.Replace("'", "\\'") + "',";
                        }
                        if (values.assetverification_securitydtls == null)
                        {
                            msSQL += "'',";
                        }
                        else
                        {
                            msSQL += "'" + values.assetverification_securitydtls.Replace("'", "\\'") + "',";
                        }
                        if (values.assetverification_mortgaged == null)
                        {
                            msSQL += "'',";
                        }
                        else
                        {
                            msSQL += "'" + values.assetverification_mortgaged.Replace("'", "\\'") + "',";
                        }
                        if (values.assetverification_ROCcreation == null)
                        {
                            msSQL += "'',";
                        }
                        else
                        {
                            msSQL += "'" + values.assetverification_ROCcreation.Replace("'", "\\'") + "',";
                        }
                        if (values.briefdtls_client == null)
                        {
                            msSQL += "'',";
                        }
                        else
                        {
                            msSQL += "'" + values.briefdtls_client.Replace("'", "\\'") + "',";
                        }
                        if (values.purposeof_funding == null)
                        {
                            msSQL += "'',";
                        }
                        else
                        {
                            msSQL += "'" + values.purposeof_funding.Replace("'", "\\'") + "',";
                        }
                        if (values.utilisation_details == null)
                        {
                            msSQL += "'',";
                        }
                        else
                        {
                            msSQL += "'" + values.utilisation_details.Replace("'", "\\'") + "',";
                        }
                        if (values.adequacy_loanamount == null)
                        {
                            msSQL += "'',";
                        }
                        else
                        {
                            msSQL += "'" + values.adequacy_loanamount.Replace("'", "\\'") + "',";
                        }
                        if (values.adequacy_impactassessment == null)
                        {
                            msSQL += "'',";
                        }
                        else
                        {
                            msSQL += "'" + values.adequacy_impactassessment.Replace("'", "\\'") + "',";
                        }
                        if (values.adequacy_additionalfunding == null)
                        {
                            msSQL += "'',";
                        }
                        else
                        {
                            msSQL += "'" + values.adequacy_additionalfunding.Replace("'", "\\'") + "',";
                        }
                        if (values.overall_remarks == null)
                        {
                            msSQL += "'',";
                        }
                        else
                        {
                            msSQL += "'" + values.overall_remarks.Replace("'", "\\'") + "',";
                        }
                        if (values.PDD_compliance == null)
                        {
                            msSQL += "'',";
                        }
                        else
                        {
                            msSQL += "'" + values.PDD_compliance.Replace("'", "\\'") + "',";
                        }
                        if (values.briefrpt_financials == null)
                        {
                            msSQL += "'',";
                        }
                        else
                        {
                            msSQL += "'" + values.briefrpt_financials.Replace("'", "\\'") + "',";
                        }
                        if (values.briefrpt_process == null)
                        {
                            msSQL += "'',";
                        }
                        else
                        {
                            msSQL += "'" + values.briefrpt_process.Replace("'", "\\'") + "',";
                        }
                        if (values.briefrpt_customer == null)
                        {
                            msSQL += "'',";
                        }
                        else
                        {
                            msSQL += "'" + values.briefrpt_customer.Replace("'", "\\'") + "',";
                        }
                        if (values.briefrpt_learnings == null)
                        {
                            msSQL += "'',";
                        }
                        else
                        {
                            msSQL += "'" + values.briefrpt_learnings.Replace("'", "\\'") + "',";
                        }
                        if (values.briefrpt_valuechain == null)
                        {
                            msSQL += "'',";
                        }
                        else
                        {
                            msSQL += "'" + values.briefrpt_valuechain.Replace("'", "\\'") + "',";
                        }
                        if (values.valuechain_mapanalysis == null)
                        {
                            msSQL += "'',";
                        }
                        else
                        {
                            msSQL += "'" + values.valuechain_mapanalysis.Replace("'", "\\'") + "',";
                        }
                        if (values.competitorbusiness_segment == null)
                        {
                            msSQL += "'',";
                        }
                        else
                        {
                            msSQL += "'" + values.competitorbusiness_segment.Replace("'", "\\'") + "',";
                        }
                        if (values.portfolio_noofmembers == null)
                        {
                            msSQL += "'',";
                        }
                        else
                        {
                            msSQL += "'" + values.portfolio_noofmembers.Replace("'", "\\'") + "',";
                        }
                        if (values.portfolio_activemembers == null)
                        {
                            msSQL += "'',";
                        }
                        else
                        {
                            msSQL += "'" + values.portfolio_activemembers.Replace("'", "\\'") + "',";
                        }
                        if (values.total_disbursementamount == null || values.total_disbursementamount == "")
                        {
                            msSQL += "null,";
                        }
                        else
                        {
                            msSQL += "'" + values.total_disbursementamount + "',";
                        }
                        if (values.outstanding_ondate == null)
                        {
                            msSQL += "'',";
                        }
                        else
                        {
                            msSQL += "'" + values.outstanding_ondate.Replace("'", "\\'") + "',";
                        }
                        if (values.overdue_beneficiary == null)
                        {
                            msSQL += "'',";
                        }
                        else
                        {
                            msSQL += "'" + values.overdue_beneficiary.Replace("'", "\\'") + "',";
                        }
                        if (values.overdue_amount == null)
                        {
                            msSQL += "'',";
                        }
                        else
                        {
                            msSQL += "'" + values.overdue_amount.Replace("'", "\\'") + "',";
                        }
                        if (values.overdueaccount_funding == null)
                        {
                            msSQL += "'',";
                        }
                        else
                        {
                            msSQL += "'" + values.overdueaccount_funding.Replace("'", "\\'") + "',";
                        }
                        msSQL += "'" + values.report_status + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        for (var i = 0; i < values.visittype.Count; i++)
                        {
                            msGet_VisitGid = objcmnfunctions.GetMasterGID("ALVI");

                            msSQL = "Insert into rsk_trn_tvisitdone( " +
                                   " trnvisitdone_gid, " +
                                   " allocationdtl_gid," +
                                   " visitreport_generateGid," +
                                   " vistdone_gid," +
                                   " visit_type," +
                                   " created_by," +
                                   " created_date)" +
                                   " values(" +
                                   "'" + msGet_VisitGid + "'," +
                                   "'" + values.allocationdtl_gid + "'," +
                                   "'" + msGetGid + "'," +
                                   "'" + values.visittype[i].vistdone_gid + "'," +
                                   "'" + values.visittype[i].visit_type + "'," +
                                   "'" + employee_gid + "'," +
                                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }

                    }
                    else
                    {
                        objODBCDatareader.Close();

                        msSQL = " update rsk_trn_tvisitreportgenerate set customer_gid ='"+ values.customer_gid +"', " +
                                " customer_name ='"+ values.customer_name + "' where allocationdtl_gid='"+ values.allocationdtl_gid +"'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " update rsk_trn_tvisitreportgenerate set " +
                                " visit_date ='" + Convert.ToDateTime(values.visitDate).ToString("yyyy-MM-dd") + "',";
                        if (values.constitution == null)
                        {
                            msSQL += " constitution='',";
                        }
                        else
                        {
                            msSQL += " constitution='" + values.constitution + "',";
                        }
                        if (values.risk_code == null)
                        {
                            msSQL += " risk_code='',";
                        }
                        else
                        {
                            msSQL += " risk_code='" + values.risk_code + "',";
                        }
                        msSQL += " dealing_withsince ='" + Convert.ToDateTime(values.relationship_Startedfrom).ToString("yyyy-MM-dd") + "',";
                        if (values.business_vintage == null)
                        {
                            msSQL += " business_vintage='',";
                        }
                        else
                        {
                            msSQL += " business_vintage='" + values.business_vintage.Replace("'", "\\'") + "',";
                        }
                        if (values.typeof_loanvertical == null)
                        {
                            msSQL += " typeof_loanvertical='',";
                        }
                        else
                        {
                            msSQL += " typeof_loanvertical='" + values.typeof_loanvertical + "',";
                        }
                        if (values.typeof_riskreview == null)
                        {
                            msSQL += " typeof_riskreview='',";
                        }
                        else
                        {
                            msSQL += " typeof_riskreview='" + values.typeof_riskreview + "',";
                        }
                        if (values.business_sector == null)
                        {
                            msSQL += " business_sector='',";
                        }
                        else
                        {
                            msSQL += " business_sector='" + values.business_sector + "',";
                        }
                        if (values.registeredoffice_address == null)
                        {
                            msSQL += " registeredoffice_address='',";
                        }
                        else
                        {
                            msSQL += " registeredoffice_address='" + values.registeredoffice_address.Replace("'", "\\'") + "',";
                        }
                        if (values.present_address == null)
                        {
                            msSQL += " present_address='',";
                        }
                        else
                        {
                            msSQL += " present_address='" + values.present_address.Replace("'", "\\'") + "',";
                        }
                        if (values.contact_details1 == null)
                        {
                            msSQL += " contact_details1='',";
                        }
                        else
                        {
                            msSQL += " contact_details1='" + values.contact_details1.Replace("'", "\\'") + "',";
                        }
                        if (values.contact_details2 == null)
                        {
                            msSQL += " contact_details2='',";
                        }
                        else
                        {
                            msSQL += " contact_details2='" + values.contact_details2.Replace("'", "\\'") + "',";
                        }
                        if (values.visit_latitude == null)
                        {
                            msSQL += " visit_latitude='',";
                        }
                        else
                        {
                            msSQL += " visit_latitude='" + values.visit_latitude.Replace("'", "\\'") + "',";
                        }
                        if (values.visit_longitude == null)
                        {
                            msSQL += " visit_longitude='',";
                        }
                        else
                        {
                            msSQL += " visit_longitude='" + values.visit_longitude.Replace("'", "\\'") + "',";
                        }
                        if (values.primarysecondary_valuechain == null)
                        {
                            msSQL += " primarysecondary_valuechain='',";
                        }
                        else
                        {
                            msSQL += " primarysecondary_valuechain='" + values.primarysecondary_valuechain.Replace("'", "\\'") + "',";
                        }
                        if (values.geneticcode_complied == null)
                        {
                            msSQL += " geneticcode_complied='',";
                        }
                        else
                        {
                            msSQL += " geneticcode_complied='" + values.geneticcode_complied + "',";
                        }
                        if (values.RMD_visitedGid == null)
                        {
                            msSQL += " RMD_visitedGid='',";
                            msSQL += " RMD_visitedname='',";
                        }
                        else
                        {
                            msSQL += " RMD_visitedGid='" + values.RMD_visitedGid + "',";
                            msSQL += " RMD_visitedname='" + values.RMD_visitedname + "',";
                        }
                        if (values.RM_name == null)
                        {
                            msSQL += " RM_name='',";
                        }
                        else
                        {
                            msSQL += " RM_name='" + values.RM_name + "',";
                        }
                        if (values.PPA_name == null)
                        {
                            msSQL += " PPA_name='',";
                        }
                        else
                        {
                            msSQL += " PPA_name='" + values.PPA_name.Replace("'", "\\'") + "',";
                        }
                        if (values.credit_managername == null)
                        {
                            msSQL += " credit_managername='',";
                        }
                        else
                        {
                            msSQL += " credit_managername='" + values.credit_managername + "',";
                        }
                        if (values.visit_done == null)
                        {
                            msSQL += " visit_done='',";
                        }
                        else
                        {
                            msSQL += " visit_done='" + values.visit_done.Replace("'", "\\'") + "',";
                        }
                        if (values.purpose_ofloan == null)
                        {
                            msSQL += " purpose_ofloan='',";
                        }
                        else
                        {
                            msSQL += " purpose_ofloan='" + values.purpose_ofloan.Replace("'", "\\'") + "',";
                        }
                        if (values.requestedamount_byclient == null || values.requestedamount_byclient == "")
                        {
                            msSQL += " requestedamount_byclient=null,";
                        }
                        else
                        {
                            msSQL += " requestedamount_byclient='" + values.requestedamount_byclient + "',";
                        }
                        if (values.sanctionedamount_byclient == null)
                        {
                            msSQL += " sanctionedamount_byclient='',";
                        }
                        else
                        {
                            msSQL += " sanctionedamount_byclient='" + values.sanctionedamount_byclient.Replace("'", "\\'") + "',";
                        }
                        if (values.disbursement_date == null)
                        {
                            msSQL += " disbursement_date='" + Convert.ToDateTime(values.disbursement_Date).ToString("yyyy-MM-dd") + "',";
                        }
                        else
                        {
                            msSQL += " disbursement_date='" + Convert.ToDateTime(values.disbursement_Date).ToString("yyyy-MM-dd") + "',";
                        }
                        if (values.disbursement_amount == null)
                        {
                            msSQL += " disbursement_amount='',";
                        }
                        else
                        {
                            msSQL += " disbursement_amount='" + values.disbursement_amount.Replace("'", "\\'") + "',";
                        }
                        if (values.totalloan_outstanding == null || values.totalloan_outstanding == "")
                        {
                            msSQL += " totalloan_outstanding=null,";
                        }
                        else
                        {
                            msSQL += " totalloan_outstanding='" + values.totalloan_outstanding + "',";
                        }
                        if (values.repayment_track == null)
                        {
                            msSQL += " repayment_track='',";
                        }
                        else
                        {
                            msSQL += " repayment_track='" + values.repayment_track + "',";
                        }
                        if (values.repayment_trackremarks == null)
                        {
                            msSQL += " repayment_trackremarks='',";
                        }
                        else
                        {
                            msSQL += " repayment_trackremarks='" + values.repayment_trackremarks.Replace("'", "\\'") + "',";
                        }
                        if (values.basicrecords_maintain == null)
                        {
                            msSQL += " basicrecords_maintain='',";
                        }
                        else
                        {
                            msSQL += " basicrecords_maintain='" + values.basicrecords_maintain.Replace("'", "\\'") + "',";
                        }
                        if (values.basicrecords_remarks == null)
                        {
                            msSQL += " basicrecords_remarks='',";
                        }
                        else
                        {
                            msSQL += " basicrecords_remarks='" + values.basicrecords_remarks.Replace("'", "\\'") + "',";
                        }
                        if (values.turnover_lastFY == null || values.turnover_lastFY == "")
                        {
                            msSQL += " turnover_lastFY=null,";
                        }
                        else
                        {
                            msSQL += " turnover_lastFY='" + values.turnover_lastFY + "',";
                        }
                        if (values.presentFY_sales == null || values.presentFY_sales == "")
                        {
                            msSQL += " presentFY_sales=null,";
                        }
                        else
                        {
                            msSQL += " presentFY_sales='" + values.presentFY_sales + "',";
                        }
                        if (values.deferral_pendency == null)
                        {
                            msSQL += " deferral_pendency='',";
                        }
                        else
                        {
                            msSQL += " deferral_pendency='" + values.deferral_pendency.Replace("'", "\\'") + "',";
                        }
                        if (values.total_noofGroups == null)
                        {
                            msSQL += " total_noofGroups='',";
                        }
                        else
                        {
                            msSQL += " total_noofGroups='" + values.total_noofGroups.Replace("'", "\\'") + "',";
                        }
                        if (values.CBOfunded_noofGroups == null)
                        {
                            msSQL += " CBOfunded_noofGroups='',";
                        }
                        else
                        {
                            msSQL += " CBOfunded_noofGroups='" + values.CBOfunded_noofGroups.Replace("'", "\\'") + "',";
                        }
                        if (values.RMD_visitgroups == null)
                        {
                            msSQL += " RMD_visitgroups='',";
                        }
                        else
                        {
                            msSQL += " RMD_visitgroups='" + values.RMD_visitgroups.Replace("'", "\\'") + "',";
                        }
                        if (values.assetverification_createdoutofloan == null)
                        {
                            msSQL += " assetverification_createdoutofloan='',";
                        }
                        else
                        {
                            msSQL += " assetverification_createdoutofloan='" + values.assetverification_createdoutofloan.Replace("'", "\\'") + "',";
                        }
                        if (values.assetverification_securitydtls == null)
                        {
                            msSQL += " assetverification_securitydtls='',";
                        }
                        else
                        {
                            msSQL += " assetverification_securitydtls='" + values.assetverification_securitydtls.Replace("'", "\\'") + "',";
                        }
                        if (values.assetverification_mortgaged == null)
                        {
                            msSQL += " assetverification_mortgaged='',";
                        }
                        else
                        {
                            msSQL += " assetverification_mortgaged='" + values.assetverification_mortgaged.Replace("'", "\\'") + "',";
                        }
                        if (values.assetverification_ROCcreation == null)
                        {
                            msSQL += " assetverification_ROCcreation='',";
                        }
                        else
                        {
                            msSQL += " assetverification_ROCcreation='" + values.assetverification_ROCcreation.Replace("'", "\\'") + "',";
                        }
                        if (values.purposeof_funding == null)
                        {
                            msSQL += " purposeof_funding='',";
                        }
                        else
                        {
                            msSQL += " purposeof_funding='" + values.purposeof_funding.Replace("'", "\\'") + "',";
                        }
                        if (values.utilisation_details == null)
                        {
                            msSQL += " utilisation_details='',";
                        }
                        else
                        {
                            msSQL += " utilisation_details='" + values.utilisation_details.Replace("'", "\\'") + "',";
                        }
                        if (values.adequacy_loanamount == null)
                        {
                            msSQL += " adequacy_loanamount='',";
                        }
                        else
                        {
                            msSQL += " adequacy_loanamount='" + values.adequacy_loanamount.Replace("'", "\\'") + "',";
                        }
                        if (values.adequacy_impactassessment == null)
                        {
                            msSQL += " adequacy_impactassessment='',";
                        }
                        else
                        {
                            msSQL += " adequacy_impactassessment='" + values.adequacy_impactassessment.Replace("'", "\\'") + "',";
                        }
                        if (values.adequacy_additionalfunding == null)
                        {
                            msSQL += " adequacy_additional_funding='',";
                        }
                        else
                        {
                            msSQL += " adequacy_additional_funding='" + values.adequacy_additionalfunding.Replace("'", "\\'") + "',";
                        }
                        if (values.portfolio_noofmembers == null)
                        {
                            msSQL += " portfolio_noofmembers='',";
                        }
                        else
                        {
                            msSQL += " portfolio_noofmembers='" + values.portfolio_noofmembers.Replace("'", "\\'") + "',";
                        }
                        if (values.portfolio_activemembers == null)
                        {
                            msSQL += " portfolio_activemembers='',";
                        }
                        else
                        {
                            msSQL += " portfolio_activemembers='" + values.portfolio_activemembers.Replace("'", "\\'") + "',";
                        }
                        if (values.total_disbursementamount == null || values.total_disbursementamount == "")
                        {
                            msSQL += " total_disbursementamount=null,";
                        }
                        else
                        {
                            msSQL += " total_disbursementamount='" + values.total_disbursementamount + "',";
                        }
                        if (values.outstanding_ondate == null)
                        {
                            msSQL += " outstanding_ondate='',";
                        }
                        else
                        {
                            msSQL += " outstanding_ondate='" + values.outstanding_ondate.Replace("'", "\\'") + "',";
                        }
                        if (values.overdue_beneficiary == null)
                        {
                            msSQL += " overdue_beneficiary='',";
                        }
                        else
                        {
                            msSQL += " overdue_beneficiary='" + values.overdue_beneficiary.Replace("'", "\\'") + "',";
                        }
                        if (values.overdue_amount == null)
                        {
                            msSQL += " overdue_amount='',";
                        }
                        else
                        {
                            msSQL += " overdue_amount='" + values.overdue_amount.Replace("'", "\\'") + "',";
                        }
                        if (values.overdueaccount_funding == null)
                        {
                            msSQL += " overdueaccount_funding='',";
                        }
                        else
                        {
                            msSQL += " overdueaccount_funding='" + values.overdueaccount_funding.Replace("'", "\\'") + "',";
                        }
                        if (values.sanctioned_limit == null)
                        {
                            msSQL += " sanctioned_limit='',";
                        }
                        else
                        {
                            msSQL += " sanctioned_limit='" + values.sanctioned_limit.Replace("'", "\\'") + "',";
                        }
                        if (values.tenure_period == null)
                        {
                            msSQL += " tenure_period='',";
                        }
                        else
                        {
                            msSQL += " tenure_period='" + values.tenure_period.Replace("'", "\\'") + "',";
                        }
                        if (values.overdue == null)
                        {
                            msSQL += " overdue='',";
                        }
                        else
                        {
                            msSQL += " overdue='" + values.overdue.Replace("'", "\\'") + "',";
                        }
                        if (values.borrower_commitment == null)
                        {
                            msSQL += " borrower_commitment='',";
                        }
                        else
                        {
                            msSQL += " borrower_commitment='" + values.borrower_commitment.Replace("'", "\\'") + "',";
                        }
                        if (values.pending_documentation == null)
                        {
                            msSQL += " pending_documentation='',";
                        }
                        else
                        {
                            msSQL += " pending_documentation='" + values.pending_documentation.Replace("'", "\\'") + "',";
                        }
                        if (values.briefdtls_client == null)
                        {
                            msSQL += " briefdtls_client='',";
                        }
                        else
                        {
                            msSQL += " briefdtls_client='" + values.briefdtls_client.Replace("'", "\\'") + "',";
                        }
                        if (values.overall_remarks == null)
                        {
                            msSQL += " overall_remarks='',";
                        }
                        else
                        {
                            msSQL += " overall_remarks='" + values.overall_remarks.Replace("'", "\\'") + "',";
                        }
                        if (values.PDD_compliance == null)
                        {
                            msSQL += " PDD_compliance='',";
                        }
                        else
                        {
                            msSQL += " PDD_compliance='" + values.PDD_compliance.Replace("'", "\\'") + "',";
                        }
                        if (values.briefrpt_financials == null)
                        {
                            msSQL += " briefrpt_financials='',";
                        }
                        else
                        {
                            msSQL += " briefrpt_financials='" + values.briefrpt_financials.Replace("'", "\\'") + "',";
                        }
                        if (values.briefrpt_process == null)
                        {
                            msSQL += " briefrpt_process='',";
                        }
                        else
                        {
                            msSQL += " briefrpt_process='" + values.briefrpt_process.Replace("'", "\\'") + "',";
                        }
                        if (values.briefrpt_customer == null)
                        {
                            msSQL += " briefrpt_customer='',";
                        }
                        else
                        {
                            msSQL += " briefrpt_customer='" + values.briefrpt_customer.Replace("'", "\\'") + "',";
                        }
                        if (values.briefrpt_learnings == null)
                        {
                            msSQL += " briefrpt_learnings='',";
                        }
                        else
                        {
                            msSQL += " briefrpt_learnings='" + values.briefrpt_learnings.Replace("'", "\\'") + "',";
                        }
                        if (values.briefrpt_valuechain == null)
                        {
                            msSQL += " briefrpt_valuechain='',";
                        }
                        else
                        {
                            msSQL += " briefrpt_valuechain='" + values.briefrpt_valuechain.Replace("'", "\\'") + "',";
                        }
                        if (values.valuechain_mapanalysis == null)
                        {
                            msSQL += " valuechain_mapanalysis='',";
                        }
                        else
                        {
                            msSQL += " valuechain_mapanalysis='" + values.valuechain_mapanalysis.Replace("'", "\\'") + "',";
                        }
                        if (values.competitorbusiness_segment == null)
                        {
                            msSQL += " competitorbusiness_segment='',";
                        }
                        else
                        {
                            msSQL += " competitorbusiness_segment='" + values.competitorbusiness_segment.Replace("'", "\\'") + "',";
                        }

                        msSQL += " report_status='" + values.report_status.Replace("'", "\\'") + "'," +
                                " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where allocationdtl_gid='" + values.allocationdtl_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = "select visitreport_generateGid from rsk_trn_tvisitreportgenerate where allocationdtl_gid='" + values.allocationdtl_gid + "'";
                        string lsvisitreport_generateGid = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = "delete from rsk_trn_tvisitdone where visitreport_generateGid='" + lsvisitreport_generateGid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult == 1)
                        {
                            for (var i = 0; i < values.visittype.Count; i++)
                            {
                                msGet_VisitGid = objcmnfunctions.GetMasterGID("ALVI");

                                msSQL = "Insert into rsk_trn_tvisitdone( " +
                                       " trnvisitdone_gid, " +
                                       " allocationdtl_gid," +
                                       " visitreport_generateGid," +
                                       " vistdone_gid," +
                                       " visit_type," +
                                       " created_by," +
                                       " created_date)" +
                                       " values(" +
                                       "'" + msGet_VisitGid + "'," +
                                       "'" + values.allocationdtl_gid + "'," +
                                       "'" + lsvisitreport_generateGid + "'," +
                                       "'" + values.visittype[i].vistdone_gid + "'," +
                                       "'" + values.visittype[i].visit_type + "'," +
                                       "'" + employee_gid + "'," +
                                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }
                        }
                    }
                    if (values.report_status == "Completed")
                    {
                        msSQL = "select visitreport_generateGid from rsk_trn_tvisitreportgenerate where allocationdtl_gid='" + values.allocationdtl_gid + "'";
                        lsvisitreport_generateGid = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = " update rsk_trn_tvisitreportdocument set visitreport_generateGid='" + lsvisitreport_generateGid + "' " +
                                " where allocationdtl_gid='" + values.allocationdtl_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " update rsk_trn_tvisitreportphoto set visitreport_generateGid='" + lsvisitreport_generateGid + "' " +
                                " where allocationdtl_gid='" + values.allocationdtl_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " update rsk_trn_tallocationdtl set completed_flag='Y', " +
                                " allocation_flag='C'," +
                                " allocation_status = 'Completed'," +
                                " lastvisit_date='" + values.visit_date.ToString("yyyy-MM-dd") + "'," +
                                " completed_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                                " where allocationdtl_gid='" + values.allocationdtl_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = "select allocation_gid from rsk_trn_tallocationdtl  where allocationdtl_gid='" + values.allocationdtl_gid + "'";
                        string lsallocation_gid = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = " update rsk_trn_tallocation set status='Completed' " +
                                " where allocation_gid='" + lsallocation_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = "select customer_urn from ocs_mst_tcustomer where customer_gid='" + values.customer_gid + "'";
                        values.customer_urn = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = "select customer_gid from rsk_trn_tcustomervisit where customer_gid='" + values.customer_gid + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {

                            msSQL = " update rsk_trn_tcustomervisit set lastvisit_date='" + values.visit_date.ToString("yyyy-MM-dd") + "'" +
                                    " where customer_gid='" + values.customer_gid + "'";
                            objODBCDatareader.Close();
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                        else
                        {

                            msGet_visitGid = objcmnfunctions.GetMasterGID("CUVD");

                            msSQL = "insert into rsk_trn_tcustomervisit(" +
                                   " customervisit_gid ," +
                                   " customer_urn," +
                                   " customer_gid," +
                                   " lastvisit_date," +
                                   " created_by," +
                                   " created_date)" +
                                   " values(" +
                                   "'" + msGet_visitGid + "'," +
                                   "'" + values.customer_urn + "'," +
                                   "'" + values.customer_gid + "'," +
                                   "'" + values.visit_date.ToString("yyyy-MM-dd") + "'," +
                                   "'" + employee_gid + "'," +
                                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        }
                        objODBCDatareader.Close();
                        msSQL = " select customer2escrow_gid,customer2sanction_gid,customer_gid,disbursement_date,transaction_date, " +
                                " transactionref_no,escrow_account_no,dealer_name,master_account_no, " +
                                " amount,beneficiary_customer_account_name,sender_customer_account_name,sender_customer_account_no, " +
                                " remittance_info,sender_branch_ifsc,reference,credit_time,remarks " +
                                " from rsk_mst_tcustomer2escrow where customer_gid='" + values.customer_gid + "'";
                        dt_datatable = objdbconn.GetDataTable(msSQL);
                        if (dt_datatable.Rows.Count != 0)
                        {
                            foreach (DataRow dt in dt_datatable.Rows)
                            {
                                string msGetEscrowGid = objcmnfunctions.GetMasterGID("TRES");

                                msSQL = " INSERT INTO rsk_trn_tcustomer2escrow(" +
                                        " trncustomer2escrow_gid," +
                                        " allocationdtl_gid," +
                                        " customer2escrow_gid," +
                                        " customer2sanction_gid," +
                                        " customer_gid," +
                                        " disbursement_date," +
                                        " transaction_date," +
                                        " transactionref_no," +
                                        " escrow_account_no," +
                                        " dealer_name," +
                                        " master_account_no," +
                                        " amount," +
                                        " beneficiary_customer_account_name," +
                                        " sender_customer_account_name," +
                                        " sender_customer_account_no," +
                                        " remittance_info," +
                                        " sender_branch_ifsc," +
                                        " reference," +
                                        " credit_time," +
                                        " remarks," +
                                        " created_date," +
                                        " created_by)" +
                                        " values(" +
                                        "'" + msGetEscrowGid + "'," +
                                        "'" + values.allocationdtl_gid + "'," +
                                        "'" + dt["customer2escrow_gid"].ToString() + "'," +
                                        "'" + dt["customer2sanction_gid"].ToString() + "'," +
                                        "'" + dt["customer_gid"].ToString() + "'," +
                                        "'" + Convert.ToDateTime(dt["disbursement_date"].ToString()).ToString("yyyy-MM-dd") + "'," +
                                        "'" + Convert.ToDateTime(dt["transaction_date"].ToString()).ToString("yyyy-MM-dd") + "'," +
                                        "'" + dt["transactionref_no"].ToString() + "'," +
                                        "'" + dt["escrow_account_no"].ToString() + "'," +
                                        "'" + dt["dealer_name"].ToString() + "'," +
                                        "'" + dt["master_account_no"].ToString() + "'," +
                                        "'" + dt["amount"].ToString() + "'," +
                                        "'" + dt["beneficiary_customer_account_name"].ToString() + "'," +
                                        "'" + dt["sender_customer_account_name"].ToString() + "'," +
                                        "'" + dt["sender_customer_account_no"].ToString() + "'," +
                                        "'" + dt["remittance_info"].ToString() + "'," +
                                        "'" + dt["sender_branch_ifsc"].ToString() + "'," +
                                        "'" + dt["reference"].ToString() + "'," +
                                        "'" + dt["credit_time"].ToString() + "'," +
                                        "'" + dt["remarks"].ToString() + "'," +
                                        "CURRENT_TIMESTAMP," +
                                        "'" + employee_gid + "')";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }
                        }
                        dt_datatable.Dispose();

                        msSQL = "update rsk_trn_tcustomerdisbursement set allocate_flag = 'N' where customer_urn = '" + values.customer_urn + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        msSQL = " update rsk_trn_tvisitreportgenerate set completed_flag='Y', " +
                                " completed_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                                "where allocationdtl_gid='" + values.allocationdtl_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        if (mnResult != 0)
                        {
                            values.status = true;
                            values.message = "Visit Report Details are Completed Successfully..!";
                            return true;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                values.message = ex.ToString();
            }

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Visit Report Details are Updated Successfully..!";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
                return false;
            }

        }


        public bool DapostVisitReportGenerate(string employee_gid, visitreport values)
        {

            msSQL = "select count(*) as photocount from rsk_trn_tvisitreportphoto where allocationdtl_gid='" + values.allocationdtl_gid + "'";
            string lsphotocount = objdbconn.GetExecuteScalar(msSQL);
            if (values.report_status == "Completed" && Convert.ToInt16(lsphotocount) == 0)
            {
                values.status = false;
                values.message = "Kindly Upload the Visit Photo";
                return false;
            }

            msSQL = "select visitreport_generateGid from rsk_trn_tvisitreportgenerate where allocationdtl_gid='" + values.allocationdtl_gid + "'";
            msGetGid = objdbconn.GetExecuteScalar(msSQL);
            if (msGetGid == "")
            {
                msGetGid = objcmnfunctions.GetMasterGID("VRGD");
                msSQL = "Insert into rsk_trn_tvisitreportgenerate(" +
                        " visitreport_generateGid," +
                        " allocationdtl_gid," +
                        " customer_gid," +
                        " customer_name," +
                        " report_status," +
                        " created_by," +
                        " created_date)" +
                        " values (" +
                        "'" + msGetGid + "'," +
                        "'" + values.allocationdtl_gid + "'," +
                        "'" + values.customer_gid + "'," +
                        "'" + values.customer_name + "'," +
                        "'" + values.report_status + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            switch (values.tab_name)
            {

                case "basic_details":
                    msSQL = " update rsk_trn_tvisitreportgenerate set " +
                            " visit_date ='" + Convert.ToDateTime(values.visitDate).ToString("yyyy-MM-dd") + "',";
                    if (values.constitution == null)
                    {
                        msSQL += " constitution='',";
                    }
                    else
                    {
                        msSQL += " constitution='" + values.constitution + "',";
                    }
                    if (values.risk_code == null)
                    {
                        msSQL += " risk_code='',";
                    }
                    else
                    {
                        msSQL += " risk_code='" + values.risk_code + "',";
                    }
                    msSQL += " dealing_withsince ='" + Convert.ToDateTime(values.relationship_Startedfrom).ToString("yyyy-MM-dd") + "',";
                    if (values.business_vintage == null)
                    {
                        msSQL += " business_vintage='',";
                    }
                    else
                    {
                        msSQL += " business_vintage='" + values.business_vintage.Replace("'", "\\'") + "',";
                    }
                    if (values.typeof_loanvertical == null)
                    {
                        msSQL += " typeof_loanvertical='',";
                    }
                    else
                    {
                        msSQL += " typeof_loanvertical='" + values.typeof_loanvertical + "',";
                    }
                    if (values.typeof_riskreview == null)
                    {
                        msSQL += " typeof_riskreview='',";
                    }
                    else
                    {
                        msSQL += " typeof_riskreview='" + values.typeof_riskreview + "',";
                    }
                    if (values.business_sector == null)
                    {
                        msSQL += " business_sector='',";
                    }
                    else
                    {
                        msSQL += " business_sector='" + values.business_sector + "',";
                    }
                    if (values.registeredoffice_address == null)
                    {
                        msSQL += " registeredoffice_address='',";
                    }
                    else
                    {
                        msSQL += " registeredoffice_address='" + values.registeredoffice_address.Replace("'", "\\'") + "',";
                    }
                    if (values.present_address == null)
                    {
                        msSQL += " present_address='',";
                    }
                    else
                    {
                        msSQL += " present_address='" + values.present_address.Replace("'", "\\'") + "',";
                    }
                    if (values.contact_details1 == null)
                    {
                        msSQL += " contact_details1='',";
                    }
                    else
                    {
                        msSQL += " contact_details1='" + values.contact_details1.Replace("'", "\\'") + "',";
                    }
                    if (values.contact_details2 == null)
                    {
                        msSQL += " contact_details2='',";
                    }
                    else
                    {
                        msSQL += " contact_details2='" + values.contact_details2.Replace("'", "\\'") + "',";
                    }
                    if (values.visit_latitude == null)
                    {
                        msSQL += " visit_latitude='',";
                    }
                    else
                    {
                        msSQL += " visit_latitude='" + values.visit_latitude.Replace("'", "\\'") + "',";
                    }
                    if (values.visit_longitude == null)
                    {
                        msSQL += " visit_longitude='',";
                    }
                    else
                    {
                        msSQL += " visit_longitude='" + values.visit_longitude.Replace("'", "\\'") + "',";
                    }
                    msSQL += " report_status='" + values.report_status.Replace("'", "\\'") + "'," +
                            " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where allocationdtl_gid='" + values.allocationdtl_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    values.status = true;
                    values.message = "Visit Report Details are Updated Successfully..!";
                    break;
                case "visit_details":
                    msSQL = " update rsk_trn_tvisitreportgenerate set ";
                    if (values.primarysecondary_valuechain == null)
                    {
                        msSQL += " primarysecondary_valuechain='',";
                    }
                    else
                    {
                        msSQL += " primarysecondary_valuechain='" + values.primarysecondary_valuechain.Replace("'", "\\'") + "',";
                    }
                    if (values.geneticcode_complied == null)
                    {
                        msSQL += " geneticcode_complied='',";
                    }
                    else
                    {
                        msSQL += " geneticcode_complied='" + values.geneticcode_complied + "',";
                    }
                    if (values.RMD_visitedGid == null)
                    {
                        msSQL += " RMD_visitedGid='',";
                        msSQL += " RMD_visitedname='',";
                    }
                    else
                    {
                        msSQL += " RMD_visitedGid='" + values.RMD_visitedGid + "',";
                        msSQL += " RMD_visitedname='" + values.RMD_visitedname + "',";
                    }
                    if (values.RM_name == null)
                    {
                        msSQL += " RM_name='',";
                    }
                    else
                    {
                        msSQL += " RM_name='" + values.RM_name + "',";
                    }
                    if (values.PPA_name == null)
                    {
                        msSQL += " PPA_name='',";
                    }
                    else
                    {
                        msSQL += " PPA_name='" + values.PPA_name.Replace("'", "\\'") + "',";
                    }
                    if (values.credit_managername == null)
                    {
                        msSQL += " credit_managername='',";
                    }
                    else
                    {
                        msSQL += " credit_managername='" + values.credit_managername + "',";
                    }
                    if (values.visit_done == null)
                    {
                        msSQL += " visit_done='',";
                    }
                    else
                    {
                        msSQL += " visit_done='" + values.visit_done.Replace("'", "\\'") + "',";
                    }
                    if (values.purpose_ofloan == null)
                    {
                        msSQL += " purpose_ofloan='',";
                    }
                    else
                    {
                        msSQL += " purpose_ofloan='" + values.purpose_ofloan.Replace("'", "\\'") + "',";
                    }
                    if (values.requestedamount_byclient == null || values.requestedamount_byclient == "")
                    {
                        msSQL += " requestedamount_byclient=null,";
                    }
                    else
                    {
                        msSQL += " requestedamount_byclient='" + values.requestedamount_byclient + "',";
                    }
                    if (values.sanctionedamount_byclient == null)
                    {
                        msSQL += " sanctionedamount_byclient='',";
                    }
                    else
                    {
                        msSQL += " sanctionedamount_byclient='" + values.sanctionedamount_byclient.Replace("'", "\\'") + "',";
                    }
                    if (values.disbursement_date == null)
                    {
                        msSQL += " disbursement_date='" + Convert.ToDateTime(values.disbursement_Date).ToString("yyyy-MM-dd") + "',";
                    }
                    else
                    {
                        msSQL += " disbursement_date='" + Convert.ToDateTime(values.disbursement_Date).ToString("yyyy-MM-dd") + "',";
                    }
                    if (values.disbursement_amount == null)
                    {
                        msSQL += " disbursement_amount='',";
                    }
                    else
                    {
                        msSQL += " disbursement_amount='" + values.disbursement_amount.Replace("'", "\\'") + "',";
                    }
                    if (values.totalloan_outstanding == null || values.totalloan_outstanding == "")
                    {
                        msSQL += " totalloan_outstanding=null,";
                    }
                    else
                    {
                        msSQL += " totalloan_outstanding='" + values.totalloan_outstanding + "',";
                    }
                    if (values.overdue == null)
                    {
                        msSQL += " overdue='',";
                    }
                    else
                    {
                        msSQL += " overdue='" + values.overdue.Replace("'", "\\'") + "',";
                    }
                    if (values.repayment_track == null)
                    {
                        msSQL += " repayment_track='',";
                    }
                    else
                    {
                        msSQL += " repayment_track='" + values.repayment_track + "',";
                    }
                    if (values.repayment_trackremarks == null)
                    {
                        msSQL += " repayment_trackremarks='',";
                    }
                    else
                    {
                        msSQL += " repayment_trackremarks='" + values.repayment_trackremarks.Replace("'", "\\'") + "',";
                    }
                    if (values.basicrecords_maintain == null)
                    {
                        msSQL += " basicrecords_maintain='',";
                    }
                    else
                    {
                        msSQL += " basicrecords_maintain='" + values.basicrecords_maintain.Replace("'", "\\'") + "',";
                    }
                    if (values.basicrecords_remarks == null)
                    {
                        msSQL += " basicrecords_remarks='',";
                    }
                    else
                    {
                        msSQL += " basicrecords_remarks='" + values.basicrecords_remarks.Replace("'", "\\'") + "',";
                    }
                    if (values.turnover_lastFY == null || values.turnover_lastFY == "")
                    {
                        msSQL += " turnover_lastFY=null,";
                    }
                    else
                    {
                        msSQL += " turnover_lastFY='" + values.turnover_lastFY + "',";
                    }
                    if (values.presentFY_sales == null || values.presentFY_sales == "")
                    {
                        msSQL += " presentFY_sales=null,";
                    }
                    else
                    {
                        msSQL += " presentFY_sales='" + values.presentFY_sales + "',";
                    }
                    if (values.deferral_pendency == null)
                    {
                        msSQL += " deferral_pendency='',";
                    }
                    else
                    {
                        msSQL += " deferral_pendency='" + values.deferral_pendency.Replace("'", "\\'") + "',";
                    }
                    if (values.total_noofGroups == null)
                    {
                        msSQL += " total_noofGroups='',";
                    }
                    else
                    {
                        msSQL += " total_noofGroups='" + values.total_noofGroups.Replace("'", "\\'") + "',";
                    }
                    if (values.borrower_commitment == null)
                    {
                        msSQL += " borrower_commitment='',";
                    }
                    else
                    {
                        msSQL += " borrower_commitment='" + values.borrower_commitment.Replace("'", "\\'") + "',";
                    }
                    if (values.pending_documentation == null)
                    {
                        msSQL += " pending_documentation='',";
                    }
                    else
                    {
                        msSQL += " pending_documentation='" + values.pending_documentation.Replace("'", "\\'") + "',";
                    }
                    if (values.CBOfunded_noofGroups == null)
                    {
                        msSQL += " CBOfunded_noofGroups='',";
                    }
                    else
                    {
                        msSQL += " CBOfunded_noofGroups='" + values.CBOfunded_noofGroups.Replace("'", "\\'") + "',";
                    }
                    if (values.RMD_visitgroups == null)
                    {
                        msSQL += " RMD_visitgroups='',";
                    }
                    else
                    {
                        msSQL += " RMD_visitgroups='" + values.RMD_visitgroups.Replace("'", "\\'") + "',";
                    }
                    msSQL += " report_status='" + values.report_status.Replace("'", "\\'") + "'," +
                            " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where allocationdtl_gid='" + values.allocationdtl_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                    msSQL = "select visitreport_generateGid from rsk_trn_tvisitreportgenerate where allocationdtl_gid='" + values.allocationdtl_gid + "'";
                    string lsvisitreport_generateGid = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = "delete from rsk_trn_tvisitdone where visitreport_generateGid='" + lsvisitreport_generateGid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnResult == 1)
                    {
                        for (var i = 0; i < values.visittype.Count; i++)
                        {
                            msGet_VisitGid = objcmnfunctions.GetMasterGID("ALVI");

                            msSQL = "Insert into rsk_trn_tvisitdone( " +
                                   " trnvisitdone_gid, " +
                                   " allocationdtl_gid," +
                                   " visitreport_generateGid," +
                                   " vistdone_gid," +
                                   " visit_type," +
                                   " created_by," +
                                   " created_date)" +
                                   " values(" +
                                   "'" + msGet_VisitGid + "'," +
                                   "'" + values.allocationdtl_gid + "'," +
                                   "'" + lsvisitreport_generateGid + "'," +
                                   "'" + values.visittype[i].vistdone_gid + "'," +
                                   "'" + values.visittype[i].visit_type + "'," +
                                   "'" + employee_gid + "'," +
                                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    values.status = true;
                    values.message = "Visit Report Details are Updated Successfully..!";
                    break;
                case "asset_verification":
                    msSQL = " update rsk_trn_tvisitreportgenerate set ";
                    if (values.assetverification_createdoutofloan == null)
                    {
                        msSQL += " assetverification_createdoutofloan='',";
                    }
                    else
                    {
                        msSQL += " assetverification_createdoutofloan='" + values.assetverification_createdoutofloan.Replace("'", "\\'") + "',";
                    }
                    if (values.assetverification_securitydtls == null)
                    {
                        msSQL += " assetverification_securitydtls='',";
                    }
                    else
                    {
                        msSQL += " assetverification_securitydtls='" + values.assetverification_securitydtls.Replace("'", "\\'") + "',";
                    }
                    if (values.assetverification_mortgaged == null)
                    {
                        msSQL += " assetverification_mortgaged='',";
                    }
                    else
                    {
                        msSQL += " assetverification_mortgaged='" + values.assetverification_mortgaged.Replace("'", "\\'") + "',";
                    }
                    if (values.assetverification_ROCcreation == null)
                    {
                        msSQL += " assetverification_ROCcreation='',";
                    }
                    else
                    {
                        msSQL += " assetverification_ROCcreation='" + values.assetverification_ROCcreation.Replace("'", "\\'") + "',";
                    }
                    if (values.purposeof_funding == null)
                    {
                        msSQL += " purposeof_funding='',";
                    }
                    else
                    {
                        msSQL += " purposeof_funding='" + values.purposeof_funding.Replace("'", "\\'") + "',";
                    }
                    if (values.utilisation_details == null)
                    {
                        msSQL += " utilisation_details='',";
                    }
                    else
                    {
                        msSQL += " utilisation_details='" + values.utilisation_details.Replace("'", "\\'") + "',";
                    }
                    if (values.adequacy_loanamount == null)
                    {
                        msSQL += " adequacy_loanamount='',";
                    }
                    else
                    {
                        msSQL += " adequacy_loanamount='" + values.adequacy_loanamount.Replace("'", "\\'") + "',";
                    }
                    if (values.adequacy_impactassessment == null)
                    {
                        msSQL += " adequacy_impactassessment='',";
                    }
                    else
                    {
                        msSQL += " adequacy_impactassessment='" + values.adequacy_impactassessment.Replace("'", "\\'") + "',";
                    }
                    if (values.adequacy_additionalfunding == null)
                    {
                        msSQL += " adequacy_additional_funding='',";
                    }
                    else
                    {
                        msSQL += " adequacy_additional_funding='" + values.adequacy_additionalfunding.Replace("'", "\\'") + "',";
                    }
                    if (values.briefdtls_client == null)
                    {
                        msSQL += " briefdtls_client='',";
                    }
                    else
                    {
                        msSQL += " briefdtls_client='" + values.briefdtls_client.Replace("'", "\\'") + "',";
                    }
                    if (values.adequacy_additionalfunding == null)
                    {
                        msSQL += " adequacy_additional_funding='',";
                    }
                    else
                    {
                        msSQL += " adequacy_additional_funding='" + values.adequacy_additionalfunding.Replace("'", "\\'") + "',";
                    }
                    if (values.overall_remarks == null)
                    {
                        msSQL += " overall_remarks='',";
                    }
                    else
                    {
                        msSQL += " overall_remarks='" + values.overall_remarks.Replace("'", "\\'") + "',";
                    }
                    if (values.PDD_compliance == null)
                    {
                        msSQL += " PDD_compliance='',";
                    }
                    else
                    {
                        msSQL += " PDD_compliance='" + values.PDD_compliance.Replace("'", "\\'") + "',";
                    }
                    msSQL += " report_status='" + values.report_status.Replace("'", "\\'") + "'," +
                             " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where allocationdtl_gid='" + values.allocationdtl_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    values.status = true;
                    values.message = "Visit Report Details are Updated Successfully..!";
                    break;
                case "portfolio_report":
                    msSQL = " update rsk_trn_tvisitreportgenerate set ";
                    if (values.portfolio_noofmembers == null)
                    {
                        msSQL += " portfolio_noofmembers='',";
                    }
                    else
                    {
                        msSQL += " portfolio_noofmembers='" + values.portfolio_noofmembers.Replace("'", "\\'") + "',";
                    }
                    if (values.portfolio_activemembers == null)
                    {
                        msSQL += " portfolio_activemembers='',";
                    }
                    else
                    {
                        msSQL += " portfolio_activemembers='" + values.portfolio_activemembers.Replace("'", "\\'") + "',";
                    }
                    if (values.total_disbursementamount == null || values.total_disbursementamount == "")
                    {
                        msSQL += " total_disbursementamount=null,";
                    }
                    else
                    {
                        msSQL += " total_disbursementamount='" + values.total_disbursementamount + "',";
                    }
                    if (values.outstanding_ondate == null)
                    {
                        msSQL += " outstanding_ondate='',";
                    }
                    else
                    {
                        msSQL += " outstanding_ondate='" + values.outstanding_ondate.Replace("'", "\\'") + "',";
                    }
                    if (values.overdue_beneficiary == null)
                    {
                        msSQL += " overdue_beneficiary='',";
                    }
                    else
                    {
                        msSQL += " overdue_beneficiary='" + values.overdue_beneficiary.Replace("'", "\\'") + "',";
                    }
                    if (values.overdue_amount == null)
                    {
                        msSQL += " overdue_amount='',";
                    }
                    else
                    {
                        msSQL += " overdue_amount='" + values.overdue_amount.Replace("'", "\\'") + "',";
                    }
                    if (values.overdueaccount_funding == null)
                    {
                        msSQL += " overdueaccount_funding='',";
                    }
                    else
                    {
                        msSQL += " overdueaccount_funding='" + values.overdueaccount_funding.Replace("'", "\\'") + "',";
                    }
                    msSQL += " report_status='" + values.report_status.Replace("'", "\\'") + "'," +
                             " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where allocationdtl_gid='" + values.allocationdtl_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    values.status = true;
                    values.message = "Visit Report Details are Updated Successfully..!";
                    break;
                case "brief_report":
                    msSQL = " update rsk_trn_tvisitreportgenerate set ";
                    if (values.briefrpt_financials == null)
                    {
                        msSQL += " briefrpt_financials='',";
                    }
                    else
                    {
                        msSQL += " briefrpt_financials='" + values.briefrpt_financials.Replace("'", "\\'") + "',";
                    }
                    if (values.briefrpt_process == null)
                    {
                        msSQL += " briefrpt_process='',";
                    }
                    else
                    {
                        msSQL += " briefrpt_process='" + values.briefrpt_process.Replace("'", "\\'") + "',";
                    }
                    if (values.briefrpt_customer == null)
                    {
                        msSQL += " briefrpt_customer='',";
                    }
                    else
                    {
                        msSQL += " briefrpt_customer='" + values.briefrpt_customer.Replace("'", "\\'") + "',";
                    }
                    if (values.briefrpt_learnings == null)
                    {
                        msSQL += " briefrpt_learnings='',";
                    }
                    else
                    {
                        msSQL += " briefrpt_learnings='" + values.briefrpt_learnings.Replace("'", "\\'") + "',";
                    }
                    if (values.briefrpt_valuechain == null)
                    {
                        msSQL += " briefrpt_valuechain='',";
                    }
                    else
                    {
                        msSQL += " briefrpt_valuechain='" + values.briefrpt_valuechain.Replace("'", "\\'") + "',";
                    }
                    if (values.valuechain_mapanalysis == null)
                    {
                        msSQL += " valuechain_mapanalysis='',";
                    }
                    else
                    {
                        msSQL += " valuechain_mapanalysis='" + values.valuechain_mapanalysis.Replace("'", "\\'") + "',";
                    }
                    if (values.competitorbusiness_segment == null)
                    {
                        msSQL += " competitorbusiness_segment='',";
                    }
                    else
                    {
                        msSQL += " competitorbusiness_segment='" + values.competitorbusiness_segment.Replace("'", "\\'") + "',";
                    }
                    msSQL += " report_status='" + values.report_status.Replace("'", "\\'") + "'," +
                           " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where allocationdtl_gid='" + values.allocationdtl_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    values.status = true;
                    values.message = "Visit Report Details are Updated Successfully..!";
                    break;
            }
            return true;
        }

        public bool DaGetVisitReportDtl(string allocationdtl_gid, visitreport values)
        {

            msSQL = " select visitreport_generateGid,allocationdtl_gid,risk_code,customer_gid,customer_name,constitution,visit_date,typeof_riskreview, " +
                    " date_format(disbursement_date,'%Y-%m-%d') as disb_date,visit_latitude,visit_longitude," +
                    " dealing_withsince,business_vintage,typeof_loanvertical,business_sector," +
                    " registeredoffice_address,present_address,contact_details1,contact_details2,sanctioned_limit,tenure_period, " +
                    " relationship_startedfrom,primarysecondary_valuechain,clientbusiness_vintage,geneticcode_complied, " +
                    " RMD_visitedGid,RMD_visitedname,RM_name,PPA_name,credit_managername,visit_done,purpose_ofloan, " +
                    " requestedamount_byclient,sanctionedamount_byclient,disbursement_date,valuechain_mapanalysis,competitorbusiness_segment," +
                    " overdue,repayment_track,repayment_trackremarks,basicrecords_maintain,basicrecords_remarks,turnover_lastFY," +
                    " presentFY_sales, deferral_pendency,total_noofGroups, disbursement_amount,totalloan_outstanding,  " +
                    " CBOfunded_noofGroups,RMD_visitgroups,borrower_commitment,pending_documentation,assetverification_createdoutofloan, " +
                    " assetverification_securitydtls,assetverification_mortgaged,assetverification_ROCcreation,briefdtls_client, " +
                    " purposeof_funding,utilisation_details,adequacy_loanamount,adequacy_impactassessment,adequacy_additional_funding,overall_remarks,PDD_compliance, " +
                    " portfolio_noofmembers,portfolio_activemembers,total_disbursementamount,outstanding_ondate,overdue_beneficiary," +
                    " overdue_amount,overdueaccount_funding,briefrpt_financials,briefrpt_process,briefrpt_customer," +
                    " briefrpt_learnings,briefrpt_valuechain,report_status,completed_date " +
                    " from rsk_trn_tvisitreportgenerate a " +
                    " where a.allocationdtl_gid='" + allocationdtl_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.visitreport_generateGid = objODBCDatareader["visitreport_generateGid"].ToString();
                values.allocationdtl_gid = objODBCDatareader["allocationdtl_gid"].ToString();
                values.customer_gid = objODBCDatareader["customer_gid"].ToString();
                values.customer_name = objODBCDatareader["customer_name"].ToString();
                values.constitution = objODBCDatareader["constitution"].ToString();
                values.visit_latitude = objODBCDatareader["visit_latitude"].ToString();
                values.visit_longitude = objODBCDatareader["visit_longitude"].ToString();
                values.typeof_riskreview = objODBCDatareader["typeof_riskreview"].ToString();
                values.risk_code = objODBCDatareader["risk_code"].ToString();
                if (objODBCDatareader["visit_date"].ToString() != "")
                {
                    values.visit_date = Convert.ToDateTime(objODBCDatareader["visit_date"].ToString());
                }
                else
                {
                    values.visit_date = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                }
                values.visitDate = objODBCDatareader["visit_date"].ToString();
                if (objODBCDatareader["dealing_withsince"].ToString() != "")
                {
                    values.dealing_withsince = Convert.ToDateTime(objODBCDatareader["dealing_withsince"].ToString());
                }
                values.business_vintage = objODBCDatareader["business_vintage"].ToString();
                values.typeof_loanvertical = objODBCDatareader["typeof_loanvertical"].ToString();
                values.business_sector = objODBCDatareader["business_sector"].ToString();
                values.registeredoffice_address = objODBCDatareader["registeredoffice_address"].ToString();
                values.present_address = objODBCDatareader["present_address"].ToString();
                values.contact_details1 = objODBCDatareader["contact_details1"].ToString();
                values.contact_details2 = objODBCDatareader["contact_details2"].ToString();
                values.sanctioned_limit = objODBCDatareader["sanctioned_limit"].ToString();
                values.tenure_period = objODBCDatareader["tenure_period"].ToString();
                values.relationship_Startedfrom = objODBCDatareader["relationship_startedfrom"].ToString();
                values.clientbusiness_vintage = objODBCDatareader["clientbusiness_vintage"].ToString();
                values.primarysecondary_valuechain = objODBCDatareader["primarysecondary_valuechain"].ToString();
                values.geneticcode_complied = objODBCDatareader["geneticcode_complied"].ToString();
                values.RMD_visitedGid = objODBCDatareader["RMD_visitedGid"].ToString();
                values.RMD_visitedname = objODBCDatareader["RMD_visitedname"].ToString();
                values.RM_name = objODBCDatareader["RM_name"].ToString();
                values.PPA_name = objODBCDatareader["PPA_name"].ToString();
                values.credit_managername = objODBCDatareader["credit_managername"].ToString();
                values.visit_done = objODBCDatareader["visit_done"].ToString();
                values.purpose_ofloan = objODBCDatareader["purpose_ofloan"].ToString();
                values.requestedamount_byclient = objODBCDatareader["requestedamount_byclient"].ToString();
                values.sanctionedamount_byclient = objODBCDatareader["sanctionedamount_byclient"].ToString();
                if (objODBCDatareader["disb_date"].ToString() != "")
                {
                    values.disbursement_date = Convert.ToDateTime(objODBCDatareader["disb_date"].ToString());
                }
                values.disbursement_Date = objODBCDatareader["disbursement_date"].ToString();
                values.disbursement_amount = objODBCDatareader["disbursement_amount"].ToString();
                values.totalloan_outstanding = objODBCDatareader["totalloan_outstanding"].ToString();
                values.repayment_track = objODBCDatareader["repayment_track"].ToString();
                values.repayment_trackremarks = objODBCDatareader["repayment_trackremarks"].ToString();
                values.overdue = objODBCDatareader["overdue"].ToString();
                values.basicrecords_maintain = objODBCDatareader["basicrecords_maintain"].ToString();
                values.basicrecords_remarks = objODBCDatareader["basicrecords_remarks"].ToString();
                values.turnover_lastFY = objODBCDatareader["turnover_lastFY"].ToString();
                values.presentFY_sales = objODBCDatareader["presentFY_sales"].ToString();
                values.deferral_pendency = objODBCDatareader["deferral_pendency"].ToString();
                values.total_noofGroups = objODBCDatareader["total_noofGroups"].ToString();
                values.CBOfunded_noofGroups = objODBCDatareader["CBOfunded_noofGroups"].ToString();
                values.RMD_visitgroups = objODBCDatareader["RMD_visitgroups"].ToString();
                values.borrower_commitment = objODBCDatareader["borrower_commitment"].ToString();
                values.pending_documentation = objODBCDatareader["pending_documentation"].ToString();
                values.assetverification_createdoutofloan = objODBCDatareader["assetverification_createdoutofloan"].ToString();
                values.assetverification_securitydtls = objODBCDatareader["assetverification_securitydtls"].ToString();
                values.assetverification_mortgaged = objODBCDatareader["assetverification_mortgaged"].ToString();
                values.assetverification_ROCcreation = objODBCDatareader["assetverification_ROCcreation"].ToString();
                values.briefdtls_client = objODBCDatareader["briefdtls_client"].ToString();
                values.purposeof_funding = objODBCDatareader["purposeof_funding"].ToString();
                values.utilisation_details = objODBCDatareader["utilisation_details"].ToString();
                values.adequacy_loanamount = objODBCDatareader["adequacy_loanamount"].ToString();
                values.adequacy_impactassessment = objODBCDatareader["adequacy_impactassessment"].ToString();
                values.adequacy_additionalfunding = objODBCDatareader["adequacy_additional_funding"].ToString();
                values.overall_remarks = objODBCDatareader["overall_remarks"].ToString();
                values.PDD_compliance = objODBCDatareader["PDD_compliance"].ToString();
                values.portfolio_noofmembers = objODBCDatareader["portfolio_noofmembers"].ToString();
                values.portfolio_activemembers = objODBCDatareader["portfolio_activemembers"].ToString();
                values.total_disbursementamount = objODBCDatareader["total_disbursementamount"].ToString();
                values.outstanding_ondate = objODBCDatareader["outstanding_ondate"].ToString();
                values.overdue_beneficiary = objODBCDatareader["overdue_beneficiary"].ToString();
                values.overdue_amount = objODBCDatareader["overdue_amount"].ToString();
                values.overdueaccount_funding = objODBCDatareader["overdueaccount_funding"].ToString();
                values.briefrpt_financials = objODBCDatareader["briefrpt_financials"].ToString();
                values.briefrpt_process = objODBCDatareader["briefrpt_process"].ToString();
                values.briefrpt_customer = objODBCDatareader["briefrpt_customer"].ToString();
                values.briefrpt_learnings = objODBCDatareader["briefrpt_learnings"].ToString();
                values.briefrpt_valuechain = objODBCDatareader["briefrpt_valuechain"].ToString();
                values.valuechain_mapanalysis = objODBCDatareader["valuechain_mapanalysis"].ToString();
                values.competitorbusiness_segment = objODBCDatareader["competitorbusiness_segment"].ToString();
                values.report_status = objODBCDatareader["report_status"].ToString();
                values.completed_date = objODBCDatareader["completed_date"].ToString();
            }
            else
            {
                values.visit_date = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
            }
            objODBCDatareader.Close();

            msSQL = " select vistdone_gid, visit_type from rsk_mst_tvisitdone";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_visittypedtl = new List<visittype>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    get_visittypedtl.Add(new visittype
                    {
                        vistdone_gid = dt["vistdone_gid"].ToString(),
                        visit_type = dt["visit_type"].ToString(),
                    });
                }
                values.visittype = get_visittypedtl;
            }
            dt_datatable.Dispose();

            msSQL = " select vistdone_gid,visit_type from rsk_trn_tvisitdone " +
                    " where visitreport_generateGid='" + values.visitreport_generateGid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_editvisittype = new List<editvisittype>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    get_editvisittype.Add(new editvisittype
                    {
                        vistdone_gid = dt["vistdone_gid"].ToString(),
                        visit_type = dt["visit_type"].ToString(),
                    });
                }
                values.editvisittype = get_editvisittype;
            }
            dt_datatable.Dispose();

            return true;
        }


        public bool DaPostvisitReportUpload(HttpRequest httpRequest, uploaddocument objfilename, string employee_gid, string user_gid)
        {
            upload_list objdocumentmodel = new upload_list();
            HttpFileCollection httpFileCollection;

            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            //MemoryStream ms = new MemoryStream();
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string pdfFilName = string.Empty;
            Stream ls_readStream;
            string lsdocumenttype_gid = string.Empty;
            string lsdocument_title = "";
            string project_flag = httpRequest.Form["project_flag"].ToString();
            try
            {
                 lsdocument_title = httpRequest.Form["document_title"].ToString();
            }
            catch(Exception ex)
            {
                 lsdocument_title ="";
            }
           
            string lsallocationgid = httpRequest.Form["allocationdtl_gid"].ToString();
            String path = lspath;

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "RSK/VisitReportDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
            {
                if ((!System.IO.Directory.Exists(path)))
                    System.IO.Directory.CreateDirectory(path);
            }
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
                        //string lsfile_gid = msdocument_gid + FileExtension;
                        string lsfile_gid = msdocument_gid;
                        FileExtension = Path.GetExtension(FileExtension).ToLower();
                        lsfile_gid = lsfile_gid + FileExtension;
                        ls_readStream = httpPostedFile.InputStream;
                        MemoryStream ms = new MemoryStream();
                        ls_readStream.CopyTo(ms);
                        //lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "RSK/VisitReportDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        //objcmnfunctions.uploadFile(lspath, lsfile_gid);
                        ////FileStream file = new FileStream(lspath, FileMode.Create, FileAccess.Write);
                        ////ms.WriteTo(file);
                        ////file.Close();
                        ////ms.Close();
                        //lspath = "../../erpdocument" + "/" + lscompany_code + "/" + "RSK/VisitReportDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.message = "File format is not supported";
                            return false;
                        }

                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "RSK/VisitReportDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "RSK/VisitReportDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension;


                        msGetGid = objcmnfunctions.GetMasterGID("VRDO");

                        msSQL = " insert into rsk_trn_tvisitreportdocument( " +
                                    " visitreport_documentGid," +
                                    " allocationdtl_gid ," +
                                    " document_title," +
                                    " document_name ," +
                                    " document_path," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + lsallocationgid + "'," +
                                    "'" + lsdocument_title + "'," +
                                    "'" + httpPostedFile.FileName + "'," +
                                    "'" + lspath + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult == 1)
                        {
                            objfilename.status = true;
                            objfilename.message = "Document Uploaded Successfully..!";
                        }
                        else
                        {
                            objfilename.status = false;
                            objfilename.message = "Error Occured..!";
                        }
                    }
                }
            }
            catch
            {
            }
            return true;
        }

        public bool DaPostVisitReportPhoto(HttpRequest httpRequest, uploaddocument objfilename, string employee_gid, string user_gid)
        {
            upload_list objdocumentmodel = new upload_list();
            HttpFileCollection httpFileCollection;

            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            //MemoryStream ms = new MemoryStream();
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string pdfFilName = string.Empty;
            Stream ls_readStream;
            string lsdocumenttype_gid = string.Empty;
            string lsallocationgid = httpRequest.Form["allocationdtl_gid"].ToString();
            string project_flag = httpRequest.Form["project_flag"].ToString();
            string lsuploadphoto_title = "";
            try
            {
                lsuploadphoto_title = httpRequest.Form["txtuploadphoto_title"].ToString();
            }
            catch (Exception ex)
            {
                lsuploadphoto_title = "";
            } 
            string lsphoto_tile;
            String path = lspath;
            if(lsuploadphoto_title=="undefined")
            {
                lsphoto_tile = "";
            }
            else
            {
                lsphoto_tile = lsuploadphoto_title;
            }
            msSQL = "select count(*) from rsk_trn_tvisitreportphoto where allocationdtl_gid='" + lsallocationgid + "'";
            string lsmaxphoto = objdbconn.GetExecuteScalar(msSQL);

            if (Convert.ToInt16(lsmaxphoto) < 6)
            {
                msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "RSK/VisitReportPhoto/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
                {
                    if ((!System.IO.Directory.Exists(path)))
                        System.IO.Directory.CreateDirectory(path);
                }
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
                            //string lsfile_gid = msdocument_gid + FileExtension;
                            string lsfile_gid = msdocument_gid;
                            FileExtension = Path.GetExtension(FileExtension).ToLower();

                            if ((FileExtension == ".jpg") || (FileExtension == ".jpeg") || (FileExtension == ".png"))
                            {
                                lsfile_gid = lsfile_gid + FileExtension;
                                ls_readStream = httpPostedFile.InputStream;
                                MemoryStream ms = new MemoryStream();
                                ls_readStream.CopyTo(ms);
                                //lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "RSK/VisitReportPhoto/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                                //objcmnfunctions.uploadFile(lspath, lsfile_gid);

                                //lspath = "../../erpdocument" + "/" + lscompany_code + "/" + "RSK/VisitReportPhoto/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                                byte[] bytes = ms.ToArray();
                                if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                                {
                                    objfilename.message = "File format is not supported";
                                    return false;
                                }

                                bool status;
                                status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "RSK/VisitReportPhoto/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                                ms.Close();
                                lspath = "erpdocument" + "/" + lscompany_code + "/" + "RSK/VisitReportPhoto/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension;


                                msGetGid = objcmnfunctions.GetMasterGID("VRPH");

                                msSQL = " insert into rsk_trn_tvisitreportphoto( " +
                                            " visitreport_photoGid," +
                                            " allocationdtl_gid," +
                                            " uploadphoto_title," +
                                            " document_name ," +
                                            " document_path," +
                                            " created_by," +
                                            " created_date" +
                                            " )values(" +
                                            "'" + msGetGid + "'," +
                                            "'" + lsallocationgid + "'," +
                                            "'" + lsphoto_tile + "'," +
                                            "'" + httpPostedFile.FileName + "'," +
                                            "'" + lspath + "'," +
                                            "'" + employee_gid + "'," +
                                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                objfilename.visitreport_photoGid = msGetGid;
                                if (mnResult == 1)
                                {
                                    objfilename.status = true;
                                    objfilename.message = "Photo Uploaded Successfully..!";
                                }
                                else
                                {
                                    objfilename.status = false;
                                    objfilename.message = "Error Occured..!";
                                }
                            }
                            else
                            {
                                objfilename.status = false;
                                objfilename.message = "File Format is Not Supported..!";
                            }
                        }
                    }
                }
                catch
                {

                }
            }
            else
            {
                objfilename.status = false;
                objfilename.message = "Maximum 6 Photos Only Allowed..!";
            }


            return true;
        }

        public bool DaGetVisitRptDocumentCancel(string visitreport_documentGid, document values)
        {

            msSQL = "delete from rsk_trn_tvisitreportdocument where visitreport_documentGid='" + visitreport_documentGid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Documents are Cancelled Successfully..!";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
                return false;
            }
        }

        public bool DaGetVisitRptPhotoCancel(string visitreport_photoGid, document values)
        {

            msSQL = "delete from rsk_trn_tvisitreportphoto where visitreport_photoGid='" + visitreport_photoGid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Photos are cancelled Successfully..!";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
                return false;
            }
        }

        public bool DaGetVisitReportDocument(visitreportdocumentList values, string allocationdtl_gid)
        {
            msSQL = " select document_title,visitreport_documentGid,customer_gid,document_path,document_name,created_date,created_by " +
                    " from rsk_trn_tvisitreportdocument where allocationdtl_gid ='" + allocationdtl_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_visitreportdtl = new List<visitreportdocument>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    get_visitreportdtl.Add(new visitreportdocument
                    {
                        visitreport_documentGid = dt["visitreport_documentGid"].ToString(),
                        document_path = objcmnstorage.EncryptData((dt["document_path"].ToString())),
                        document_name = dt["document_name"].ToString(),
                        document_title = dt["document_title"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                    });
                }
                values.visitreportdocument = get_visitreportdtl;
            }
            dt_datatable.Dispose();

            return true;
        }

        public bool DaGetVisitReportPhoto(visitreportphotoList values, string allocationdtl_gid)
        {

            msSQL = " select visitreport_photoGid,customer_gid,document_path,document_name,created_date,created_by,uploadphoto_title " +
                    " from rsk_trn_tvisitreportphoto where allocationdtl_gid ='" + allocationdtl_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_visitreportdtl = new List<visitreportphoto>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    get_visitreportdtl.Add(new visitreportphoto
                    {
                        visitreport_photoGid = dt["visitreport_photoGid"].ToString(),
                        document_path = objcmnstorage.EncryptData((dt["document_path"].ToString())),
                        document_name = dt["document_name"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        photo_title = dt["uploadphoto_title"].ToString(),
                    });
                }
                values.visitreportphoto = get_visitreportdtl;
            }
            dt_datatable.Dispose();

            return true;
        }

        public bool DaGetSanctionTenurePeriod(sanctionloanlist values, string allocationdtl_gid)
        {
            msSQL = " select a.sanction_gid,a.sanction_refno,date_format(a.sanction_date,'%d-%m-%Y') as sanctiondate,a.sanction_type, " +
                   " format(sanction_amount, 2) as sanction_amount,facility_type,a.entity,a.colanding_status,a.colander_name" +
                   " from rsk_trn_tallocatesanctiondtl a " +
                   " where a.allocationdtl_gid = '" + allocationdtl_gid + "' order by a.allocatesanctiondtl_Gid desc ";
            
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getloanlistdtl = new List<loandtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getloanlistdtl.Add(new loandtl
                    {
                        sanction_amount = (dr_datarow["sanction_amount"].ToString()),
                        sanction_refno = (dr_datarow["sanction_refno"].ToString()),
                        sanction_date = (dr_datarow["sanctiondate"].ToString()),
                        sanction_gid = (dr_datarow["sanction_gid"].ToString()),
                        facility_type = (dr_datarow["facility_type"].ToString()),
                        sanction_type = (dr_datarow["sanction_type"].ToString()),
                        entity = (dr_datarow["entity"].ToString()),
                        colanding_status = (dr_datarow["colanding_status"].ToString()),
                        colander_name = (dr_datarow["colander_name"].ToString()),
                    });
                }
                values.loandtl = getloanlistdtl;
            }
            dt_datatable.Dispose();

            msSQL = " select format(sum(sanction_amount),2) as totalsanction_amount from rsk_trn_tallocatesanctiondtl " +
                    " where allocationdtl_gid='" + allocationdtl_gid + "'";
            values.totalsanction_amount = objdbconn.GetExecuteScalar(msSQL);

            return true;
        }

        public bool DaGetPreVisitRMUpload(uploaddocument objvalues, string allocationdtl_gid, string employee_gid)
        {
            msSQL = "select document_name,document_path,document_type from rsk_tmp_tallocationdocument where created_by='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    string msGetDocumentGid = objcmnfunctions.GetMasterGID("DOAL");

                    msSQL = " Insert into rsk_trn_tallocationdocument( " +
                              " allocation_documentGid," +
                              " allocationdtl_gid," +
                              " document_name," +
                              " document_path," +
                              " document_type," +
                              " created_by," +
                              " created_date)" +
                              " values(" +
                              "'" + msGetDocumentGid + "', " +
                              "'" + allocationdtl_gid + "'," +
                              "'" + dt["document_name"].ToString() + "'," +
                              "'" + dt["document_path"].ToString() + "'," +
                              "'" + dt["document_type"].ToString() + "'," +
                              "'" + employee_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult == 1)

                    {
                        msSQL = "delete from rsk_tmp_tallocationdocument where tmp_documentGid ='" + dt["tmp_documentGid"].ToString() + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
            }
            dt_datatable.Dispose();
            if (mnResult == 1)
            {
                objvalues.status = true;
                objvalues.message = "Pre-Visit Documents Uploaded Successfully..!";
            }
            else
            {
                objvalues.status = false;
                objvalues.message = "Error Occured..!";
            }
            return true;
        }

        public bool DaPostScheduleLog(string employee_gid, schedulelogdtl values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("SCLG");
            msSQL = " Insert into rsk_trn_tschedulelog( " +
                            " schedulelog_gid," +
                            " allocationdtl_gid," +
                            " customer_gid," +
                            " appointment_date," +
                            " appointment_time," +
                            " appointment_status," +
                            " appointment_remarks," +
                            " schedule_status," +
                            " created_by," +
                            " created_date)" +
                            " values(" +
                            "'" + msGetGid + "', " +
                            "'" + values.allocationdtl_gid + "'," +
                            "'" + values.customer_gid + "'," +
                            "'" + Convert.ToDateTime(values.appointment_date).ToString("yyyy-MM-dd") + "'," +
                            "'" + values.appointment_time + "'," +
                            "'" + values.appointment_status + "'," +
                            "'" + values.appointment_remarks + "'," +
                            "'Pending'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult == 1)
            {
                values.status = true;
                values.message = "Scheduled Log Details are Added Successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
            return true;
        }

        public bool DaPostCallLog(string employee_gid, calllogdtl values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("CALG");
            msSQL = " Insert into rsk_trn_tcalllogdtl( " +
                            " calllog_gid," +
                            " allocationdtl_gid," +
                            " customer_gid," +
                            " dialed_number," +
                            " call_response," +
                            " call_remarks," +
                            " created_by," +
                            " created_date)" +
                            " values(" +
                            "'" + msGetGid + "', " +
                            "'" + values.allocationdtl_gid + "'," +
                            "'" + values.customer_gid + "'," +
                            "'" + values.dialed_number + "'," +
                            "'" + values.call_response + "'," +
                            "'" + values.call_remarks + "'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult == 1)
            {
                values.status = true;
                values.message = "Call Log Details are Added Successfully..!";

            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
            return true;
        }

        public bool DaGetAllocationLogDetail(string allocationdtl_gid, allocationlogdtl values)
        {
            msSQL = " select customer_urn,customername,a.customer_gid,date_format(a.created_date,'%d-%m-%Y') as allocated_date  from rsk_trn_tallocationdtl a " +
                   " left join ocs_mst_tcustomer b on b.customer_gid = a.customer_gid " +
                   " where allocationdtl_gid = '" + allocationdtl_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.customer_urn = objODBCDatareader["customer_urn"].ToString();
                values.customer_name = objODBCDatareader["customername"].ToString();
                values.customer_gid = objODBCDatareader["customer_gid"].ToString();
                values.allocated_date = objODBCDatareader["allocated_date"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " select schedulelog_gid,date_format(appointment_date,'%d-%m-%Y') as appointmentdate, " +
                   " reschedule_flag,appointment_time,appointment_status,appointment_remarks,schedule_status " +
                   " from rsk_trn_tschedulelog where allocationdtl_gid = '" + allocationdtl_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getschedulelistdtl = new List<schedulelogdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getschedulelistdtl.Add(new schedulelogdtl
                    {
                        schedulelog_gid = (dr_datarow["schedulelog_gid"].ToString()),
                        appointment_date = (dr_datarow["appointmentdate"].ToString()),
                        appointment_time = dr_datarow["appointment_time"].ToString(),
                        appointment_status = (dr_datarow["appointment_status"].ToString()),
                        appointment_remarks = (dr_datarow["appointment_remarks"].ToString()),
                        reschedule_flag = (dr_datarow["reschedule_flag"].ToString()),
                        schedule_status = (dr_datarow["schedule_status"].ToString()),
                    });
                }
                values.schedulelogdtl = getschedulelistdtl;
            }
            dt_datatable.Dispose();

            msSQL = "select calllog_gid,dialed_number,call_response,call_remarks from rsk_trn_tcalllogdtl where allocationdtl_gid= '" + allocationdtl_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcalllistdtl = new List<calllogdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcalllistdtl.Add(new calllogdtl
                    {
                        dialed_number = (dr_datarow["dialed_number"].ToString()),
                        call_response = dr_datarow["call_response"].ToString(),
                        call_remarks = (dr_datarow["call_remarks"].ToString()),
                        calllog_gid = (dr_datarow["calllog_gid"].ToString()),
                    });
                }
                values.calllogdtl = getcalllistdtl;
            }
            dt_datatable.Dispose();

            return true;
        }

        public bool DaGetRMTodayActivity(todayactivityList values, string employee_gid)
        {
            msSQL = " select  concat(customer_urn,' / ',customername) as customer_name,schedulelog_gid, " +
                  " date_format(appointment_date,'%Y-%m-%d') as appointmentdate, " +
                  " date_format(appointment_date,'%d-%m-%Y') as scheduled_date, " +
                  " appointment_time,appointment_status,appointment_remarks " +
                 " from rsk_trn_tschedulelog a " +
                 " left join ocs_mst_tcustomer b on b.customer_gid = a.customer_gid " +
                 " where appointment_date = '" + DateTime.Now.ToString("yyyy-MM-dd") + "' and a.created_by='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getschedulelistdtl = new List<todayactivity>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getschedulelistdtl.Add(new todayactivity
                    {
                        scheduled_date = dr_datarow["scheduled_date"].ToString(),
                        scheduled_time = dr_datarow["appointment_time"].ToString(),
                        remarks = dr_datarow["appointment_remarks"].ToString(),
                        customer_name = dr_datarow["customer_name"].ToString(),
                    });
                }
                values.todayactivity = getschedulelistdtl;
            }

            msSQL = " select  concat(customer_urn,' / ',customername) as customer_name,schedulelog_gid, " +
                       " date_format(appointment_date,'%d-%m-%Y') as appointmentdate," +
                       " appointment_time,appointment_status,appointment_remarks " +
                       " from rsk_trn_tschedulelog a " +
                       " left join ocs_mst_tcustomer b on b.customer_gid = a.customer_gid " +
                       " where month(appointment_date) = month(curdate()) and a.created_by = '" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmonthlylist = new List<monthlyactivity>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmonthlylist.Add(new monthlyactivity
                    {
                        scheduled_date = (dr_datarow["appointmentdate"].ToString()),
                        scheduled_time = (dr_datarow["appointment_time"].ToString()),
                        remarks = dr_datarow["appointment_remarks"].ToString(),
                        customer_name = dr_datarow["customer_name"].ToString(),

                    });
                }
                values.monthlyactivity = getmonthlylist;
            }
            dt_datatable.Dispose();

            msSQL = " select zonal_name,state_gid,state_name, district_gid,district_name, assigned_RM from rsk_mst_tRMmapping a " +
                    " left join rsk_mst_tzonalmapping b on b.zonalmapping_gid = a.zonalmapping_gid " +
                    " where assigned_RM = '" + employee_gid + "' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_mydistrictdtl = new List<mydistrictdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    get_mydistrictdtl.Add(new mydistrictdtl
                    {
                        state_name = dt["state_name"].ToString(),
                        state_gid = dt["state_gid"].ToString(),
                        district_gid = dt["district_gid"].ToString(),
                        district_name = dt["district_name"].ToString(),
                        zonal_name = dt["zonal_name"].ToString(),
                    });
                }
                values.mydistrictdtl = get_mydistrictdtl;
            }
            dt_datatable.Dispose();

            msSQL = " select a.count_current,b.count_upcoming,c.count_completed,d.count_exclusion from " +
                   " (select count(*) as count_current from rsk_trn_tallocationdtl where allocation_status = 'Allocated' " +
                   " and allocation_assignedRM ='" + employee_gid + "' and visit_allocated_date <= NOW())as a, " +
                   " (select count(*) as count_upcoming from rsk_trn_tallocationdtl where allocation_status = 'Allocated' " +
                   " and allocation_assignedRM ='" + employee_gid + "' and month(visit_allocated_date) = month(NOW() + interval 1 month))as b, " +
                   " (select count(*) as count_completed from rsk_trn_tallocationdtl where allocation_status = 'Completed' and allocate_external is null " +
                   " and allocation_assignedRM ='" + employee_gid + "' )as c , " +
                   " (select count(*) as count_exclusion from rsk_trn_tallocationdtl where allocation_status = 'Excluded' " +
                   " and allocation_assignedRM ='" + employee_gid + "' )as d";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.count_current = objODBCDatareader["count_current"].ToString();
                values.count_upcoming = objODBCDatareader["count_upcoming"].ToString();
                values.count_completed = objODBCDatareader["count_completed"].ToString();
                values.count_external = objODBCDatareader["count_exclusion"].ToString();
            }
            objODBCDatareader.Close();

            return true;
        }

        public bool DaGetRMCustomerDetails(string state_gid, string district_gid, mycustomerdtllist values, string employee_gid)
        {
            msSQL = " select a.customer_gid,a.customer_urn,a.customername,a.vertical_code, " +
                    " case when b.lastvisit_date is null then 'Fresh' else 'Re-Visit' end as customer_status " +
                    " from ocs_mst_tcustomer a " +
                    " left join rsk_trn_tcustomervisit b on a.customer_gid = b.customer_gid " +
                    " where assigned_RM = '" + employee_gid + "' and district_gid = '" + district_gid + "' and state_gid = '" + state_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmycustomerdtl = new List<mycustomerdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmycustomerdtl.Add(new mycustomerdtl
                    {
                        customer_urn = dr_datarow["customer_urn"].ToString(),
                        customer_name = dr_datarow["customername"].ToString(),
                        customer_status = dr_datarow["customer_status"].ToString(),
                        vertical_code = dr_datarow["vertical_code"].ToString(),
                    });
                }
                values.mycustomerdtl = getmycustomerdtl;
            }
            dt_datatable.Dispose();
            return true;
        }

        public bool DaGetRMCalenderDtl(calendarevent values, string employee_gid)
        {
            msSQL = " select  concat(customername ,' / ',customer_urn) as customer_name, " +
                    " date_format(appointment_date,'%Y-%m-%d') as appointmentdate," +
                    " appointment_time " +
                    " from rsk_trn_tschedulelog a " +
                    " left join ocs_mst_tcustomer b on b.customer_gid = a.customer_gid " +
                    " where a.created_by = '" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var geteventlist = new List<createevent>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    geteventlist.Add(new createevent
                    {
                        event_date = Convert.ToDateTime(dr_datarow["appointmentdate"].ToString()),
                        event_time = (Convert.ToDateTime(dr_datarow["appointment_time"].ToString())),
                        event_title = dr_datarow["customer_name"].ToString(),
                    });
                }
                values.createevent = geteventlist;
            }
            dt_datatable.Dispose();
            return true;
        }

        public void DaGetEditcalllog(string calllog_gid, calllogedit values)
        {
            msSQL = " select dialed_number,call_response,call_remarks from rsk_trn_tcalllogdtl where calllog_gid ='" + calllog_gid + "' ";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                values.dialed_number = objODBCDatareader["dialed_number"].ToString();
                values.call_response = objODBCDatareader["call_response"].ToString();
                values.call_remarks = objODBCDatareader["call_remarks"].ToString();
                values.calllog_gid = calllog_gid;
            }
            objODBCDatareader.Close();

        }

        public void DaPostUpdatecalllog(string employee_gid, calllogedit values)
        {
            try
            {
                msSQL = " update rsk_trn_tcalllogdtl set " +
                " dialed_number='" + values.dialed_number + "'," +
                " call_response='" + values.call_response + "'," +
                " call_remarks='" + values.call_remarks + "'," +
                " updated_by='" + employee_gid + "'," +
                " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                " where calllog_gid='" + values.calllog_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {

                    values.status = true;
                    values.message = "Call Log Details are Updated Successfully..!";
                }
                else
                {
                    values.status = false;
                    values.message = "failure..!";

                }

            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "failure..!";

            }

        }

        public void DaGetEditschedule(string schedulelog_gid, schedulelogedit values)
        {
            msSQL = " select appointment_time,appointment_date, " +
                    " appointment_status,appointment_remarks from rsk_trn_tschedulelog where schedulelog_gid ='" + schedulelog_gid + "' ";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                if (objODBCDatareader["appointment_date"].ToString() != "")
                {
                    values.appointment_Date = Convert.ToDateTime(objODBCDatareader["appointment_date"].ToString());
                }

                values.appointment_Time = Convert.ToDateTime(objODBCDatareader["appointment_time"].ToString());

                values.appointment_status = objODBCDatareader["appointment_status"].ToString();
                values.appointment_remarks = objODBCDatareader["appointment_remarks"].ToString();
                values.schedulelog_gid = schedulelog_gid;
            }
            objODBCDatareader.Close();

        }

        public void DaPostUpdateScheduleLog(string employee_gid, schedulelogedit values)
        {
            try
            {
                msGetGid = objcmnfunctions.GetMasterGID("HISL");
                msSQL = " insert into rsk_trn_thistoryschedulelog (history_scheduleloggid,schedulelog_gid,allocationdtl_gid,customer_gid, " +
                        " appointment_date,appointment_time,appointment_status,appointment_remarks,created_by,created_date) " +
                        " (select '" + msGetGid + "', schedulelog_gid, allocationdtl_gid, customer_gid, " +
                        " appointment_date, appointment_time, appointment_status, appointment_remarks, " +
                        " created_by, created_date from rsk_trn_tschedulelog where  schedulelog_gid= '" + values.schedulelog_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update rsk_trn_tschedulelog set " +
                " appointment_date='" + Convert.ToDateTime(values.appointment_date).ToString("yyyy-MM-dd") + "'," +
                " appointment_time='" + Convert.ToDateTime(values.appointment_time).ToString("HH:mm:ss") + "'," +
                " appointment_status='" + values.appointment_status + "'," +
                " appointment_remarks ='" + values.appointment_remarks + "' ," +
                " schedule_status='Pending'," +
                " reschedule_flag='Y'," +
                " updated_by='" + employee_gid + "'," +
                " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                " where schedulelog_gid='" + values.schedulelog_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {

                    values.status = true;
                    values.message = "Schedule Log Details are Updated Successfully..!";
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occured..!";

                }

            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "failure";

            }

        }

        public bool DaGetScheduleLogHistory(schedulelogdtlhistory values, string schedulelog_gid)
        {
            msSQL = " select date_format(appointment_date,'%d-%m-%Y') as appointmentdate, " +
                   " appointment_time,appointment_status,appointment_remarks " +
                    " from rsk_trn_thistoryschedulelog where schedulelog_gid = '" + schedulelog_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getschedulelistdtl = new List<schedulelogdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getschedulelistdtl.Add(new schedulelogdtl
                    {
                        appointment_date = (dr_datarow["appointmentdate"].ToString()),
                        appointment_time = dr_datarow["appointment_time"].ToString(),
                        appointment_status = (dr_datarow["appointment_status"].ToString()),
                        appointment_remarks = (dr_datarow["appointment_remarks"].ToString()),
                    });
                }
                values.schedulelogdtl = getschedulelistdtl;
            }
            dt_datatable.Dispose();
            return true;
        }

        public bool DaPostScheduleStatus(schedulestatus values)
        {

            msSQL = " update rsk_trn_tschedulelog set schedule_status='" + values.schedule_status + "'" +
                  " where schedulelog_gid='" + values.schedulelog_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = " Schedule Log Status Updated Successfully..!";
            }
            else
            {
                values.status = false;
                values.message = " Error Occured..!";
            }
            return true;
        }

        public void DaGetScheduleInfo(string allocationdtl_gid, scheduleinfo values)
        {
            msSQL = "select schedule_status from rsk_trn_tschedulelog where allocationdtl_gid='" + allocationdtl_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                msSQL = "select count(schedulelog_gid) from rsk_trn_tschedulelog where allocationdtl_gid='" + allocationdtl_gid + "'";
                string lsoverall_count = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select count(schedule_status) as schedulelog from rsk_trn_tschedulelog " +
                        " where allocationdtl_gid ='" + allocationdtl_gid + "' and schedule_status = 'Completed'";
                string lscompleted_count = objdbconn.GetExecuteScalar(msSQL);
                if (lsoverall_count == lscompleted_count)
                {
                    values.info_flag = "Y";
                    values.message = "Schedule Log Status Updated Successfully..!";
                }
                else
                {
                    values.info_flag = "N";
                    values.message = "Schedule Log Details are Pending..!";
                }
            }
            else
            {
                values.info_flag = "N";
                values.message = "Schedule Log Details are empty..!";
            }
            objODBCDatareader.Close();
        }
    }
}