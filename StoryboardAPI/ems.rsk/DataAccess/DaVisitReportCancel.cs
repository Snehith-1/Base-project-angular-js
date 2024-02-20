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

namespace ems.rsk.DataAccess
{
    public class DaVisitReportCancel
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msSQL, msGetGid, msGet_VisitGid;
        int mnResult;

        public bool DaPostCancelReport(string employee_gid, visitreportcancel values)
        {
            msSQL = " update rsk_trn_tvisitreportgenerate set cancel_remarks='" + values.cancel_reason.Replace("'", "\\'") + "'" +
                    " where allocationdtl_gid='" + values.allocationdtl_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;

                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
                return false;
            }
        }

        public bool DaPostCancelReportSubmit(string employee_gid, visitreport values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("VRCA");

            msSQL = " insert into rsk_trn_tcancelvisitreport(visitreport_cancelGid,visitreport_generateGid,allocationdtl_gid,customer_gid,customer_name,risk_code, " +
                    " constitution,visit_date,typeof_riskreview, " +
                    " dealing_withsince,business_vintage,typeof_loanvertical,business_sector,registeredoffice_address,present_address,contact_details1, " +
                    " contact_details2,visit_latitude,visit_longitude,sanctioned_limit,tenure_period,relationship_startedfrom,primarysecondary_valuechain,clientbusiness_vintage,geneticcode_complied," +
                     " RMD_visitedGid,RMD_visitedname,RM_name,PPA_name,credit_managername,visit_done,purpose_ofloan,requestedamount_byclient,sanctionedamount_byclient," +
                     " disbursement_date,disbursement_amount,totalloan_outstanding,overdue,repayment_track,basicrecords_maintain,basicrecords_remarks,turnover_lastFY,presentFY_sales," +
                     " deferral_pendency,total_noofGroups,CBOfunded_noofGroups,RMD_visitgroups,borrower_commitment,pending_documentation,assetverification_createdoutofloan," +
                     " assetverification_securitydtls,assetverification_mortgaged,assetverification_ROCcreation,briefdtls_client,purposeof_funding,utilisation_details," +
                     " adequacy_loanamount,adequacy_impactassessment,adequacy_additional_funding,overall_remarks,PDD_compliance,portfolio_noofmembers,portfolio_activemembers,total_disbursementamount," +
                    " outstanding_ondate,overdue_beneficiary,overdue_amount,overdueaccount_funding,briefrpt_financials,briefrpt_process,briefrpt_customer,briefrpt_learnings," +
                     "  briefrpt_valuechain,valuechain_mapanalysis,competitorbusiness_segment,repayment_trackremarks,created_by,created_date,cancel_remarks)" +
                      " (select '" + msGetGid + "' ,visitreport_generateGid, allocationdtl_gid, customer_gid, customer_name,risk_code, constitution, visit_date,typeof_riskreview, " +
                     " dealing_withsince, business_vintage, typeof_loanvertical, business_sector, registeredoffice_address, present_address, contact_details1, " +
                     " contact_details2,visit_latitude,visit_longitude,sanctioned_limit, tenure_period, relationship_startedfrom, primarysecondary_valuechain, clientbusiness_vintage, geneticcode_complied, " +
                      " RMD_visitedGid, RMD_visitedname, RM_name, PPA_name, credit_managername, visit_done, purpose_ofloan, requestedamount_byclient, sanctionedamount_byclient, " +
                      " disbursement_date, disbursement_amount, totalloan_outstanding, overdue, repayment_track, basicrecords_maintain,basicrecords_remarks, turnover_lastFY, presentFY_sales, " +
                      " deferral_pendency, total_noofGroups, CBOfunded_noofGroups, RMD_visitgroups, borrower_commitment, pending_documentation, assetverification_createdoutofloan, " +
                     " assetverification_securitydtls, assetverification_mortgaged, assetverification_ROCcreation, briefdtls_client, purposeof_funding, utilisation_details, " +
                     " adequacy_loanamount, adequacy_impactassessment,adequacy_additional_funding, overall_remarks, PDD_compliance, portfolio_noofmembers, portfolio_activemembers, total_disbursementamount, " +
                      " outstanding_ondate, overdue_beneficiary, overdue_amount, overdueaccount_funding, briefrpt_financials, briefrpt_process, briefrpt_customer, briefrpt_learnings, " +
                     " briefrpt_valuechain,valuechain_mapanalysis,competitorbusiness_segment,repayment_trackremarks, created_by, current_timestamp, cancel_remarks from rsk_trn_tvisitreportgenerate " +
                      " where allocationdtl_gid='" + values.allocationdtl_gid + "') ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " update rsk_trn_tvisitreportgenerate set cancel_flag='Y'" +
                   " where allocationdtl_gid='" + values.allocationdtl_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "select allocation_gid from rsk_trn_tallocationdtl where allocationdtl_gid='" + values.allocationdtl_gid + "'";
            string lsallocation_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "update rsk_trn_tallocation set reportcancel_flag='Y'" +
                   " where allocation_gid='" + lsallocation_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult == 1)
            {
                msSQL = " update rsk_trn_tvisitreportgenerate set " +
                " visit_date ='" + Convert.ToDateTime(values.visitDate).ToString("yyyy-MM-dd") + "',";
                if (values.constitution == null)
                {
                    msSQL += " constitution='',";
                }
                else
                {
                    msSQL += " constitution='" + values.constitution.Replace("'", "\\'") + "',";
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
                if (values.requestedamount_byclient == null || values.requestedamount_byclient=="")
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
                if (values.totalloan_outstanding == null || values.totalloan_outstanding=="")
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
                if (values.turnover_lastFY == null || values.turnover_lastFY=="")
                {
                    msSQL += " turnover_lastFY=null,";
                }
                else
                {
                    msSQL += " turnover_lastFY='" + values.turnover_lastFY + "',";
                }
                if (values.presentFY_sales == null || values.presentFY_sales=="")
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
                if (values.total_disbursementamount == null || values.total_disbursementamount=="")
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
                string lsallocationGid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " update rsk_trn_tallocation set status='Completed' " +
                        " where allocation_gid='" + lsallocationGid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


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
                   string msGet_visitGid = objcmnfunctions.GetMasterGID("CUVD");

                    msSQL = "insert into rsk_trn_tcustomervisit(" +
                           " customervisit_gid ," +
                           " customer_gid," +
                           " lastvisit_date," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGet_visitGid + "'," +
                           "'" + values.customer_gid + "'," +
                           "'" + values.visit_date.ToString("yyyy-MM-dd") + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                }
                objODBCDatareader.Close();

                msSQL = " update rsk_trn_tvisitreportgenerate set " +
                        " report_lastchangesdate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                        " where allocationdtl_gid='" + values.allocationdtl_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Visit Report Latest Details Updated Successfully..!";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
                return false;
            }
        }

        public bool DaGetVisitCancelLog(visistreportcancelList values, string allocationdtl_gid)
        {
            msSQL = " select cancel_remarks,date_format(a.created_date, '%d-%m-%Y %h:%m %p') as created_date, " +
                    " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as created_by from rsk_trn_tcancelvisitreport a " +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " where allocationdtl_gid='" + allocationdtl_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_visistreportcancel = new List<visistreportcancel>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    get_visistreportcancel.Add(new visistreportcancel
                    {
                        cancel_remarks = dt["cancel_remarks"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                    });
                }
                values.visistreportcancel = get_visistreportcancel;
            }
            dt_datatable.Dispose();

            msSQL = " update rsk_trn_tvisitreportgenerate set cancel_flag='N'" +
                  " where allocationdtl_gid='" + allocationdtl_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "select allocation_gid from rsk_trn_tallocationdtl where allocationdtl_gid='" + allocationdtl_gid + "'";
            string lsallocation_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "update rsk_trn_tallocation set reportcancel_flag='N'" +
                   " where allocation_gid='" + lsallocation_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            return true;
        }

        public bool DaGetExternalVisitCancelLog(visistreportcancelList values, string allocationdtl_gid)
        {
            msSQL = "select cancel_remarks,date_format(a.created_date, '%d-%m-%Y %h:%m %p') as created_date, " +
                    " concat(b.external_username, ' / ',b.external_usercode) as created_by from rsk_trn_tcancelvisitreport a " +
                    "  left join rsk_mst_texternaluser b on a.created_by = b.external_usergid " +
                    " where allocationdtl_gid='" + allocationdtl_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_visistreportcancel = new List<visistreportcancel>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    get_visistreportcancel.Add(new visistreportcancel
                    {
                        cancel_remarks = dt["cancel_remarks"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                    });
                }
                values.visistreportcancel = get_visistreportcancel;
            }
            dt_datatable.Dispose();
            msSQL = " update rsk_trn_tvisitreportgenerate set cancel_flag='N'" +
                            " where allocationdtl_gid='" + allocationdtl_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "select allocation_gid from rsk_trn_tallocationdtl where allocationdtl_gid='" + allocationdtl_gid + "'";
            string lsallocation_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "update rsk_trn_tallocation set reportcancel_flag='N'" +
                   " where allocation_gid='" + lsallocation_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            return true;
        }

        public bool DaPostVisitStatus(visitstatus values)
        {
 
            msSQL = " update rsk_trn_tallocationdtl set visit_status='" + values.visit_status.Replace("'", "\\'") + "'" +
                  " where allocationdtl_gid='" + values.allocationdtl_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if(mnResult!=0)
            {
                values.status = true;
                values.message = " Visit report Status Updated Successfully..!";
            }
            else
            {
                values.status = false;
                values.message = " Error Occured..!";
            }
            return true;
        }
    }
}