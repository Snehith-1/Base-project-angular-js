using ems.master.Models;
using ems.utilities.Functions;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Pdf;
using Spire.Pdf.HtmlConverter;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.IO;
using System.Linq;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Threading;
using System.Web;
using System.Text.RegularExpressions;
using System.Globalization;
using Spire.Doc.Fields;
using Spire.Pdf.Graphics;
using ems.storage.Functions;
using static OfficeOpenXml.ExcelErrorValue;
using System.Web.Hosting;

/// <summary>
/// (It's used for flow after CAD Accepted in Samfin)CAD DataAccess Class accessed by API methods from related Controller class and is returning relevant response to client.
/// </summary>
/// <remarks>Written by Sumala,Logapriya and Abilash</remarks>

namespace ems.master.DataAccess
{
    public class DaMstCAD
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        DaSendBackMailTrigger objvalues = new DaSendBackMailTrigger();
        DataTable dt_datatable, dt_child, dt_childindividual, dt_childgroup, dt_datatable2;
        string msSQL, msGetGid, msGetGid1, msGetGidCC, msGetGid2, msGetGid3, lscadapplication_gid, msGetgroupDocchecklistGID, msGetCovgroupDocchecklistGID;
        int mnResult, mnResultUntag, mnResult1, mnResult2, mnResultCAD, mnResultuploadesdeclarationdocumentlog, mnResultlimitproductinfolog, mnResultdeviationmaildocumentlog, mnResultapplication2sanctiondoclog;
        HttpPostedFile httpPostedFile;
        OdbcDataReader objODBCDataReader, objODBCDataReader1, objODBCDataReader2, objODBCDatareader;
        string lssanctionref_no, lstemplate_content, lscompany_code, lspath, lsdocument_path, fileName, edit_flag;
        string msGetRef, msGetGID, msGetGidlsafeescharge, lsdocument_code, lsdocument_name, lsdocumenttype_name, lscompanydocument_name, lsindividualdocument_name, lsgroupdocument_name, lsdocumenttype_gid, lsapplicationvisit_gid;
        string lscontent = string.Empty;
        string application2sanction_gid, sanction_refno, sanction_date, sanction_amount, entity, paycard, branch_gid, branch_name,
                      entity_gid, application_gid, ccapproved_date, applicationtype_gid, application_type, esdeclaration_status,
                      sanctionto_gid, sanctionto_name, sanctionfrom_date, sanctiontill_date, contactpersonaddress_gid,
                      contactperson_address, contactperson_name, contactperson_number, contactpersonmobileno_gid,
                      contactpersonemail_gid, contactpersonemail_address, sanction_type, natureof_proposal, created_by, created_date,
                      makerfile_path, makerfile_name, sanctionletter_status, template_content, makersubmitted_by, makersubmitted_on, application2sanctionlog_gid,
                     sanctiongenerated_by, sanctiongenerated_on, template_name, checkerapproved_by, checkerupdated_on, checkerpushback_remarks, checkerapproval_flag, checkerapproved_on, digitalsignature_flag;
        string interchangeability, report_structure_gid, report_structure, odlim_condition,
            documented_limit, dateof_Expiry, updated_by, updated_date;

        public string DocList, CreditList, DocUntagList;

        public void DaUpdateProcessType(string employee_gid, MdlUpdateProcessType values)
        {
            msSQL = " select application_gid from ocs_trn_tcadapplication where application_gid= '" + values.application_gid + "'";
            lscadapplication_gid = objdbconn.GetExecuteScalar(msSQL);
            if (String.IsNullOrEmpty(lscadapplication_gid))
            {
                msSQL = " update ocs_mst_tapplication set " +
                     " process_type='" + values.process_type + "',";
                if (values.processtype_remarks == null || values.processtype_remarks == "")
                {
                    msSQL += " processtype_remarks='',";
                }
                else
                {
                    msSQL += " processtype_remarks='" + values.processtype_remarks.Replace("'", "") + "',";

                }
                msSQL += " processupdated_by='" + employee_gid + "'," +
                         " processupdated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where application_gid='" + values.application_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            }
            else
            {
                msSQL = " update ocs_mst_tapplication set " +
                         " process_type='" + values.process_type + "',";
                if (values.processtype_remarks == null || values.processtype_remarks == "")
                {
                    msSQL += " processtype_remarks='',";
                }
                else
                {
                    msSQL += " processtype_remarks='" + values.processtype_remarks.Replace("'", "") + "',";

                }
                msSQL += " reprocesstypeupdated_by='" + employee_gid + "'," +
                         " reprocesstypeupdated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where application_gid='" + values.application_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            msSQL = " select application_gid from ocs_trn_tcadapplication where application_gid= '" + values.application_gid + "'";
            lscadapplication_gid = objdbconn.GetExecuteScalar(msSQL);
            if (String.IsNullOrEmpty(lscadapplication_gid))
            {
                CreateCADApplication(values.application_gid);
            }
            else
            {
                msSQL = " update ocs_trn_tcadapplication set " +
                        " process_type='" + values.process_type + "',";
                if (values.processtype_remarks == null || values.processtype_remarks == "")
                {
                    msSQL += " processtype_remarks='',";
                }
                else
                {
                    msSQL += " processtype_remarks='" + values.processtype_remarks.Replace("'", "") + "',";

                }
                msSQL += " reprocesstypeupdated_by='" + employee_gid + "'," +
                        " reprocesstypeupdated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                        " where application_gid='" + values.application_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            if (mnResult != 0)
            {
                msSQL = "update ocs_trn_tprocesstype_assign set application_gid='" + values.application_gid + "' where application_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                // objvalues.Daccapprovedmail(values.application_gid);
                values.message = "Process Type Added Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }

        public void CreateCADApplication(string application_gid)
        {
            msSQL = " insert into ocs_trn_tcadapplication select * from ocs_mst_tapplication where application_gid='" + application_gid + "'";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadapplication2contactno select * from ocs_mst_tapplication2contactno where application_gid='" + application_gid + "'";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadapplication2email select * from ocs_mst_tapplication2email where application_gid='" + application_gid + "'";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadapplication2geneticcode select * from ocs_mst_tapplication2geneticcode where application_gid='" + application_gid + "'";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadinstitution select * from ocs_mst_tinstitution where application_gid='" + application_gid + "'";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadinstitution2branch select * from ocs_mst_tinstitution2branch " +
                    " where institution_gid in (select institution_gid from ocs_mst_tinstitution where application_gid='" + application_gid + "')";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadinstitution2mobileno select * from ocs_mst_tinstitution2mobileno " +
                    " where institution_gid in (select institution_gid from ocs_mst_tinstitution where application_gid='" + application_gid + "')";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadinstitution2email select * from ocs_mst_tinstitution2email " +
                    " where institution_gid in (select institution_gid from ocs_mst_tinstitution where application_gid='" + application_gid + "')";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadinstitution2address select * from ocs_mst_tinstitution2address " +
                    " where institution_gid in (select institution_gid from ocs_mst_tinstitution where application_gid='" + application_gid + "')";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadinstitution2licensedtl select * from ocs_mst_tinstitution2licensedtl " +
                    " where institution_gid in (select institution_gid from ocs_mst_tinstitution where application_gid='" + application_gid + "')";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadinstitution2documentupload select * from ocs_mst_tinstitution2documentupload " +
                    " where institution_gid in (select institution_gid from ocs_mst_tinstitution where application_gid='" + application_gid + "')";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            //msSQL = " insert into ocs_trn_tcadcontact select * from ocs_mst_tcontact where application_gid='" + application_gid + "'";
            //mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);


            msSQL = "select contact_gid from ocs_mst_tcontact where application_gid='" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msSQL = " insert into ocs_trn_tcadcontact select * from ocs_mst_tcontact where contact_gid='" + dt["contact_gid"].ToString() + "'";
                    mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            else
            {
                mnResultCAD = 1;
            }

            msSQL = " insert into ocs_trn_tcadcontact2panabsencereason select * from ocs_mst_tcontact2panabsencereason " +
                    " where contact_gid in (select contact_gid from ocs_mst_tcontact where application_gid='" + application_gid + "')";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadcontact2panform60 select * from ocs_mst_tcontact2panform60 " +
                    " where contact_gid in (select contact_gid from ocs_mst_tcontact where application_gid='" + application_gid + "')";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadcontact2mobileno select * from ocs_mst_tcontact2mobileno " +
                    " where contact_gid in (select contact_gid from ocs_mst_tcontact where application_gid='" + application_gid + "')";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadcontact2email select * from ocs_mst_tcontact2email " +
                    " where contact_gid in (select contact_gid from ocs_mst_tcontact where application_gid='" + application_gid + "')";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadcontact2idproof select * from ocs_mst_tcontact2idproof " +
                    " where contact_gid in (select contact_gid from ocs_mst_tcontact where application_gid='" + application_gid + "')";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadcontact2address select * from ocs_mst_tcontact2address " +
                    " where contact_gid in (select contact_gid from ocs_mst_tcontact where application_gid='" + application_gid + "')";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadcontact2document select * from ocs_mst_tcontact2document " +
                    " where contact_gid in (select contact_gid from ocs_mst_tcontact where application_gid='" + application_gid + "')";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadgroup select * from ocs_mst_tgroup where application_gid='" + application_gid + "'";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadgroup2document select * from ocs_mst_tgroup2document " +
                    " where group_gid in (select group_gid from ocs_mst_tgroup where application_gid='" + application_gid + "')";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadgroup2address select * from ocs_mst_tgroup2address " +
                    " where group_gid in (select group_gid from ocs_mst_tgroup where application_gid='" + application_gid + "')";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadgroup2bank select * from ocs_mst_tgroup2bank " +
                    " where group_gid in (select group_gid from ocs_mst_tgroup where application_gid='" + application_gid + "')";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            //msSQL = " insert into ocs_trn_tcadapplication2loan select * from ocs_mst_tapplication2loan where application_gid='" + application_gid + "'";
            //mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);



            msSQL = "select application2loan_gid from ocs_mst_tapplication2loan where application_gid='" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msSQL = " insert into ocs_trn_tcadapplication2loan select * from ocs_mst_tapplication2loan where application2loan_gid='" + dt["application2loan_gid"].ToString() + "'";
                    mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " insert into ocs_trn_tcadapplication2product select * from ocs_mst_tapplication2product where application2loan_gid='" + dt["application2loan_gid"].ToString() + "'";
                    mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            else
            {
                mnResultCAD = 1;
            }

            msSQL = " insert into ocs_trn_tcadapplication2collateral select * from ocs_mst_tapplication2collateral where application_gid='" + application_gid + "'";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);
            msSQL = " insert into ocs_trn_tcadapplication2product select * from ocs_mst_tapplication2product where application_gid='" + application_gid + "'";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcaduploadcollateraldocument select * from ocs_mst_tuploadcollateraldocument " +
                    " where application2loan_gid in (select application2loan_gid from ocs_mst_tapplication2loan where application_gid='" + application_gid + "')";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadapplication2buyer select * from ocs_mst_tapplication2buyer " +
                    " where application2loan_gid in (select application2loan_gid from ocs_mst_tapplication2loan where application_gid='" + application_gid + "')";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadapplication2hypothecation select * from ocs_mst_tapplication2hypothecation where application_gid='" + application_gid + "'";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcaduploadhypothecationocument select * from ocs_mst_tuploadhypothecationocument " +
                    " where application2hypothecation_gid in (select application2hypothecation_gid from ocs_mst_tapplication2hypothecation where application_gid='" + application_gid + "')";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadapplicationservicecharge select * from ocs_mst_tapplicationservicecharge where application_gid='" + application_gid + "'";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "select application2servicecharge_gid from ocs_mst_tapplicationservicecharge where application_gid='" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msGetGidlsafeescharge = objcmnfunctions.GetMasterGID("LFCG");
                    msSQL = " insert into ocs_trn_tlsafeescharge " +
                            " (lsafeescharge_gid, application2servicecharge_gid, application_gid, generatelsa_gid,processing_fee," +
                            " processing_collectiontype, doc_charges, doccharge_collectiontype,fieldvisit_charges,fieldvisit_charges_collectiontype," +
                            " adhoc_fee,adhoc_collectiontype,life_insurance,lifeinsurance_collectiontype,acct_insurance,acctinsurance_collectiontype," +
                            " total_collect,total_deduct,product_type,created_by,created_date) " +
                            " select @lsafeescharge_gid := '" + msGetGidlsafeescharge + "',application2servicecharge_gid, @application_gid:= '" + application_gid + "'," +
                            " '" + application_gid + "',processing_fee,processing_collectiontype,doc_charges,doccharge_collectiontype," +
                            " fieldvisit_charges,fieldvisit_charges_collectiontype,adhoc_fee,adhoc_collectiontype,life_insurance,lifeinsurance_collectiontype," +
                            " acct_insurance,acctinsurance_collectiontype,total_collect,total_deduct,product_type,created_by,created_date" +
                            " from ocs_mst_tapplicationservicecharge " +
                            " where application_gid='" + application_gid + "'";
                    mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            else
            {
                mnResultCAD = 1;
            }

            msSQL = " insert into ocs_trn_tcadcontact2bureau select * from ocs_mst_tcontact2bureau " +
                    " where contact_gid in (select contact_gid from ocs_mst_tcontact where application_gid='" + application_gid + "')";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadindividual2cicdocumentupload select * from ocs_mst_tindividual2cicdocumentupload " +
                    " where contact2bureau_gid in (select contact2bureau_gid from ocs_mst_tcontact2bureau " +
                    " where contact_gid in (select contact_gid from ocs_mst_tcontact where application_gid='" + application_gid + "'))";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadinstitution2bureau select * from ocs_mst_tinstitution2bureau " +
                    " where institution_gid in (select institution_gid from ocs_mst_tinstitution where application_gid='" + application_gid + "')";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadinstitution2cicdocumentupload select * from ocs_mst_tinstitution2cicdocumentupload " +
                    " where institution2bureau_gid in (select institution2bureau_gid from ocs_mst_tinstitution2bureau " +
                    " where institution_gid in (select institution_gid from ocs_mst_tinstitution where application_gid='" + application_gid + "'))";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadapplicationvisitreport select * from ocs_mst_tapplicationvisitreport where application_gid='" + application_gid + "'";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);



            msSQL = "select applicationvisit_gid from ocs_mst_tapplicationvisitreport where application_gid='" + application_gid + "'";
            lsapplicationvisit_gid = objdbconn.GetExecuteScalar(msSQL);
            if (String.IsNullOrEmpty(lsapplicationvisit_gid))
            {
                mnResultCAD = 1;
            }
            else
            {


                msSQL = " insert into ocs_trn_tcadapplicationvisit2document select * from ocs_mst_tapplicationvisit2document where applicationvisit_gid in (select applicationvisit_gid from ocs_mst_tapplicationvisitreport where application_gid='" + application_gid + "')";
                mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);
            }


            msSQL = "select applicationvisit_gid from ocs_mst_tapplicationvisitreport where application_gid='" + application_gid + "'";
            lsapplicationvisit_gid = objdbconn.GetExecuteScalar(msSQL);
            if (String.IsNullOrEmpty(lsapplicationvisit_gid))
            {
                //foreach (DataRow dt in dt_datatable.Rows)
                //{
                mnResultCAD = 1;
                //}
            }
            else
            {

                msSQL = " insert into ocs_trn_tcadapplicationvisit2photo select * from ocs_mst_tapplicationvisit2photo where applicationvisit_gid in (select applicationvisit_gid from ocs_mst_tapplicationvisitreport where application_gid='" + application_gid + "')";
                mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            msSQL = " insert into ocs_trn_tcadapplication2gradingtool select * from ocs_mst_tapplication2gradingtool where application_gid='" + application_gid + "'";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            //Credit Action

            msSQL = " insert into ocs_trn_tcadcreditgeneticcode select * from ocs_mst_tcreditgeneticcode where application_gid='" + application_gid + "'";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadcreditsupplier select * from ocs_mst_tcreditsupplier where application_gid='" + application_gid + "'";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadcreditbankdtl select * from ocs_mst_tcreditbankdtl where application_gid='" + application_gid + "'";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadcreditbankdtl2cheque select * from ocs_mst_tcreditbankdtl2cheque " +
                 " where creditbankdtl_gid in (select creditbankdtl_gid from ocs_mst_tcreditbankdtl where application_gid='" + application_gid + "')";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadcreditbankfacilitydtl select * from ocs_mst_tcreditbankfacilitydtl where application_gid='" + application_gid + "'";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadcreditrepaymentdtl select * from ocs_mst_tcreditrepaymentdtl where application_gid='" + application_gid + "'";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadbankstatementanalysis select * from ocs_mst_tbankstatementanalysis where application_gid='" + application_gid + "'";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadfsaupload select * from ocs_trn_tfsaupload where application_gid='" + application_gid + "'";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadcreditbalancesheet select * from ocs_trn_tcreditbalancesheet where application_gid='" + application_gid + "'";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadcreditbalancesheet2 select * from ocs_trn_tcreditbalancesheet2 where application_gid='" + application_gid + "'";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadcreditprofitloss select * from ocs_trn_tcreditprofitloss where application_gid='" + application_gid + "'";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadcreditprofitlosstemp2 select * from ocs_trn_tcreditprofitlosstemp2 where application_gid='" + application_gid + "'";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadsummarytemplate1 select * from ocs_trn_tsummarytemplate1 where application_gid='" + application_gid + "'";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadsummarytemplate2 select * from ocs_trn_tsummarytemplate2 where application_gid='" + application_gid + "'";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadcreditobservation select * from ocs_mst_tcreditobservation where application_gid='" + application_gid + "'";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadcreditobservationupdatelog select * from ocs_mst_tcreditobservationupdatelog where application_gid='" + application_gid + "'";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadcreditgeneticcodeupdatelog select * from ocs_mst_tcreditgeneticcodeupdatelog where application_gid='" + application_gid + "'";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadcreditsupplierupdatelog select * from ocs_mst_tcreditsupplierupdatelog where application_gid='" + application_gid + "'";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadcreditbuyerupdatelog select * from ocs_mst_tcreditbuyerupdatelog where application_gid='" + application_gid + "'";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadcreditbuyer select * from ocs_mst_tcreditbuyer where application_gid='" + application_gid + "'";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadcreditbankdtllog select * from ocs_mst_tcreditbankdtllog where application_gid='" + application_gid + "'";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadcreditbankfacilitydtlupdatelog select * from ocs_mst_tcreditbankfacilitydtlupdatelog where application_gid='" + application_gid + "'";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadcreditrepaymentdtlupdatelog select * from ocs_mst_tcreditrepaymentdtlupdatelog where application_gid='" + application_gid + "'";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            //KYC API

            msSQL = " insert into ocs_trn_tcadkycpanauthentication select * from ocs_mst_tkycpanauthentication where function_gid='" + application_gid + "'";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadkycdlauthentication select * from ocs_mst_tkycdlauthentication where function_gid='" + application_gid + "'";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadkycepicauthentication select * from ocs_mst_tkycepicauthentication where function_gid='" + application_gid + "'";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadkycpassportauthentication select * from ocs_mst_tkycpassportauthentication where function_gid='" + application_gid + "'";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadkycifscauthentication select * from ocs_mst_tkycifscauthentication where function_gid='" + application_gid + "'";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadkycbankaccverification select * from ocs_mst_tkycbankaccverification where function_gid='" + application_gid + "'";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadgspinverification select * from ocs_trn_tgspinverification where application_gid='" + application_gid + "'";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadgstreturnfilling select * from ocs_trn_tgstreturnfilling where application_gid='" + application_gid + "'";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadgspinauthentication select * from ocs_trn_tgspinauthentication where application_gid='" + application_gid + "'";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadtandtl select * from ocs_trn_ttandtl where application_gid='" + application_gid + "'";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadcompanyllpno select * from ocs_trn_tcompanyllpno where application_gid='" + application_gid + "'";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_cadmcasignatories select * from ocs_trn_mcasignatories where application_gid='" + application_gid + "'";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_cadmcasignatorydetails select * from ocs_trn_mcasignatorydetails " +
                    " where mcasignatories_gid in (select mcasignatories_gid from ocs_trn_cadmcasignatories where application_gid='" + application_gid + "')";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadfssailicenseauthentication select * from ocs_trn_tfssailicenseauthentication where application_gid='" + application_gid + "'";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadfdalicenseauthentication select * from ocs_trn_tfdalicenseauthentication where application_gid='" + application_gid + "'";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadiecdtl select * from ocs_trn_tiecdtl where application_gid='" + application_gid + "'";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadshopandestablishment select * from ocs_trn_tshopandestablishment where application_gid='" + application_gid + "'";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadpropertytax select * from ocs_trn_tpropertytax where application_gid='" + application_gid + "'";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadvehiclercauthadvanced select * from ocs_trn_tvehiclercauthadvanced where application_gid='" + application_gid + "'";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadvehiclercsearch select * from ocs_trn_tvehiclercsearch where application_gid='" + application_gid + "'";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadlpgiddtl select * from ocs_trn_tlpgiddtl where application_gid='" + application_gid + "'";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadinstitutionprobedetails select * from ocs_trn_tinstitutionprobedetails where application_gid='" + application_gid + "'";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadinstitutionprobedetailslog select * from ocs_trn_tinstitutionprobedetailslog where application_gid='" + application_gid + "'";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadinstitutionprobedocumentdetails select * from ocs_trn_tinstitutionprobedocumentdetails where application_gid='" + application_gid + "'";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadinstitutionprobedocumentdetailslog select * from ocs_trn_tinstitutionprobedocumentdetailslog where application_gid='" + application_gid + "'";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadcrimereportcontact select * from ocs_mst_tcrimereportcontact " +
            " where contact_gid in (select contact_gid from ocs_mst_tcontact where application_gid='" + application_gid + "')";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadcrimereportinstitution select * from ocs_mst_tcrimereportinstitution " +
                     " where institution_gid in (select institution_gid from ocs_mst_tinstitution where application_gid='" + application_gid + "')";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadcrimecasetaggedcontact select * from ocs_mst_tcrimecasetaggedcontact " +
                    " where contact_gid in (select contact_gid from ocs_mst_tcontact where application_gid='" + application_gid + "')";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadcrimecasetaggedinstitution select * from ocs_mst_tcrimecasetaggedinstitution " +
                    " where institution_gid in (select institution_gid from ocs_mst_tinstitution where application_gid='" + application_gid + "')";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadcontact2tuhighriskalert select * from ocs_mst_tcontact2tuhighriskalert " +
                    " where contact2bureau_gid in (select contact2bureau_gid from ocs_mst_tcontact2bureau " +
                    " where contact_gid in (select contact_gid from ocs_mst_tcontact where application_gid='" + application_gid + "'))";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadapplication2product select * from ocs_mst_tapplication2product where application_gid='" + application_gid + "'";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadinstitution2livestock select * from ocs_mst_tinstitution2livestock " +
                    " where institution_gid in (select institution_gid from ocs_mst_tinstitution where application_gid='" + application_gid + "')";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadinstitution2equipment select * from ocs_mst_tinstitution2equipment " +
                    " where institution_gid in (select institution_gid from ocs_mst_tinstitution where application_gid='" + application_gid + "')";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadcontact2livestock select * from ocs_mst_tcontact2livestock " +
                    " where contact_gid in (select contact_gid from ocs_mst_tcontact where application_gid='" + application_gid + "')";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadcontact2equipment select * from ocs_mst_tcontact2equipment " +
                    " where contact_gid in (select contact_gid from ocs_mst_tcontact where application_gid='" + application_gid + "')";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadgroup2livestock select * from ocs_mst_tgroup2livestock " +
                    " where group_gid in (select group_gid from ocs_mst_tgroup where application_gid='" + application_gid + "')";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadgroup2equipment select * from ocs_mst_tgroup2equipment " +
                    " where group_gid in (select group_gid from ocs_mst_tgroup where application_gid='" + application_gid + "')";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadinstitution2receivable select * from ocs_mst_tinstitution2receivable " +
                    " where institution_gid in (select institution_gid from ocs_mst_tinstitution where application_gid='" + application_gid + "')";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadinstitution2fpocacity select * from ocs_mst_tinstitution2fpocacity " +
                    " where institution_gid in (select institution_gid from ocs_mst_tinstitution where application_gid='" + application_gid + "')";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);



            // Document CHecklist
            msSQL = " insert into ocs_trn_tcaddocumentchecktls select * from ocs_trn_tdocumentchecktls where application_gid='" + application_gid + "'";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadcovanantdocumentcheckdtls select * from ocs_trn_tcovanantdocumentcheckdtls where application_gid='" + application_gid + "'";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadcreditguaranteedtl select * from ocs_mst_tcreditguaranteedtl where application_gid='" + application_gid + "'";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);


            msSQL = "select creditguaranteedtl_gid from ocs_mst_tcreditguaranteedtl where application_gid='" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msSQL = " insert into ocs_trn_tcadcreditguaranteedtldocument select * from ocs_mst_tcreditguaranteedtldocument where creditguaranteedtl_gid='" + dt["creditguaranteedtl_gid"].ToString() + "'";
                    mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            else
            {
                mnResultCAD = 1;
            }



            msSQL = "select groupdocumentchecklist_gid from ocs_trn_tgroupdocumentchecklist where application_gid='" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msGetgroupDocchecklistGID = objcmnfunctions.GetMasterGID("GDCG");
                    msSQL = " insert into ocs_trn_tcadgroupdocumentchecklist " +
                           " ( groupdocumentchecklist_gid, application_gid, credit_gid, mstdocument_gid, mstdocument_name, mstcovenant_type, " +
                           " mstdocumenttype_gid, mstdocumenttype_name, untagged_type, tagged_by, covenant_periods, covenantperiod_updatedby, " +
                           " covenantperiod_updateddate, created_by, created_date, overall_docstatus, due_date, extendeddue_date, " +
                           " documentconfirmation_remarks, confirmation_updatedby, confirmation_updateddate, physicaloverall_docstatus, " +
                           " physical_extendedduedate, physical_confirmation_updatedby, physical_confirmation_updateddate, document_code) " +
                           " select groupdocumentchecklist_gid, application_gid , " +
                           " credit_gid, mstdocument_gid, mstdocument_name, mstcovenant_type, " +
                           " mstdocumenttype_gid, mstdocumenttype_name, untagged_type, tagged_by, covenant_periods, covenantperiod_updatedby, " +
                           " covenantperiod_updateddate, created_by, created_date, overall_docstatus, due_date, extendeddue_date, " +
                           " documentconfirmation_remarks, confirmation_updatedby, confirmation_updateddate, physicaloverall_docstatus, " +
                           " physical_extendedduedate, physical_confirmation_updatedby, physical_confirmation_updateddate, document_code " +
                           " from ocs_trn_tgroupdocumentchecklist " +
                           " where application_gid='" + application_gid + "'" +
                           " and groupdocumentchecklist_gid='" + dt["groupdocumentchecklist_gid"].ToString() + "'";
                    mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            else
            {
                mnResultCAD = 1;
            }

            msSQL = "select groupcovdocumentchecklist_gid from ocs_trn_tgroupcovenantdocumentchecklist where application_gid='" + application_gid + "'";
            dt_datatable2 = objdbconn.GetDataTable(msSQL);
            if (dt_datatable2.Rows.Count != 0)
            {
                foreach (DataRow dt2 in dt_datatable2.Rows)
                {
                    msGetCovgroupDocchecklistGID = objcmnfunctions.GetMasterGID("GCDG");
                    msSQL = " insert into ocs_trn_tcadgroupcovenantdocumentchecklist " +
                           " (groupcovdocumentchecklist_gid, application_gid, credit_gid, mstdocument_gid, mstdocument_name, " +
                           " mstcovenant_type, mstdocumenttype_gid, mstdocumenttype_name, untagged_type, tagged_by, covenant_periods, " +
                           " covenantperiod_updatedby, covenantperiod_updateddate, overall_docstatus, due_date, extendeddue_date, " +
                           " documentconfirmation_remarks, confirmation_updatedby, confirmation_updateddate, created_by, created_date, " +
                           " physicaloverall_docstatus, physical_extendedduedate, physical_covenant_periods, " +
                           " physical_covenantperiod_updatedby, physical_covenantperiod_updateddate, physical_confirmation_updatedby, " +
                           " physical_confirmation_updateddate) " +
                           " select groupcovdocumentchecklist_gid, application_gid , " +
                           " credit_gid, mstdocument_gid, mstdocument_name, " +
                            " mstcovenant_type, mstdocumenttype_gid, mstdocumenttype_name, untagged_type, tagged_by, covenant_periods, " +
                           " covenantperiod_updatedby, covenantperiod_updateddate, overall_docstatus, due_date, extendeddue_date, " +
                           " documentconfirmation_remarks, confirmation_updatedby, confirmation_updateddate, created_by, created_date, " +
                           " physicaloverall_docstatus, physical_extendedduedate, physical_covenant_periods, " +
                           " physical_covenantperiod_updatedby, physical_covenantperiod_updateddate, physical_confirmation_updatedby, " +
                           " physical_confirmation_updateddate " +
                           " from ocs_trn_tgroupcovenantdocumentchecklist " +
                           " where application_gid='" + application_gid + "'" +
                           " and groupcovdocumentchecklist_gid='" + dt2["groupcovdocumentchecklist_gid"].ToString() + "'";
                    mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);
                }


            }
            else
            {
                mnResultCAD = 1;
            }

            //crimecheckStatusTracking
            msSQL = " insert into ocs_trn_tcadcrimechecksearchrecord select * from ocs_trn_tcrimechecksearchrecord " +
                  " where institution_gid in (select institution_gid from ocs_mst_tinstitution where application_gid='" + application_gid + "')";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadcrimecheckrtsearchrecord select * from ocs_trn_tcrimecheckrtsearchrecord " +
                 " where institution_gid in (select institution_gid from ocs_mst_tinstitution where application_gid='" + application_gid + "')";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);
            msSQL = " insert into ocs_trn_tcadcrimechecksearchrecord select * from ocs_trn_tcrimechecksearchrecord " +
                            " where contact_gid in (select contact_gid from ocs_mst_tcontact where application_gid='" + application_gid + "')";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_trn_tcadcrimecheckrtsearchrecord select * from ocs_trn_tcrimecheckrtsearchrecord " +
                 " where contact_gid in (select contact_gid from ocs_mst_tcontact where application_gid='" + application_gid + "')";
            mnResultCAD = objdbconn.ExecuteNonQuerySQL(msSQL);
        }

        public void DaGetPendingCADReviewSummary(string employee_gid, MdlMstCAD values)
        {
            msSQL = "call ocs_trn_applicationpendingsummary";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<cadapplicationlist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    //string lsverification_flag;
                    //msSQL = " select a.application_gid from ocs_mst_tcreditcolendingdtl a " +
                    //       " left join ocs_mst_tapplication b on a.application_gid = b.application_gid " +
                    //       " left join ocs_mst_tcreditverification c on a.application_gid = c.application_gid " +
                    //       " left join ocs_mst_tccapprovedverification d on (a.application_gid = d.application_gid and d.verification_status = 'Completed')  " +
                    //       " where approval_status = 'CC Approved' and c.verification_status = 'Completed' and d.application_gid is null " +
                    //       " and a.application_gid='" + dt["application_gid"] + "' group by(a.application_gid)";  
                    //objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    //if (objODBCDataReader.HasRows == true)
                    //{
                    //    lsverification_flag = "Y";
                    //}
                    //else
                    //{
                    //    lsverification_flag = "N";
                    //}
                    //objODBCDataReader.Close();

                    //string lsccgroup_name;
                    //msSQL = "select group_concat(ccgroup_name) as ccgroup_name from ocs_mst_tapplication where application_gid='" + dt["application_gid"] + "'";
                    //objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    //if (objODBCDataReader.HasRows == true)
                    //{
                    //    lsccgroup_name = objODBCDataReader["ccgroup_name"].ToString();
                    //}
                    //else
                    //{
                    //    lsccgroup_name = "";
                    //}
                    //objODBCDataReader.Close();
                    getapplicationadd_list.Add(new cadapplicationlist
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        ccapproved_date = dt["ccapproved_date"].ToString(),
                        creditgroup_name = dt["creditgroup_name"].ToString(),
                        //ccgroup_name = lsccgroup_name,
                        product_gid = dt["product_gid"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        variety_gid = dt["variety_gid"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        history_flag = dt["history_flag"].ToString(),
                        ccmeetingskipcolor_flag = dt["ccmeetingskipcolor_flag"].ToString(),
                        //verification_flag = lsverification_flag,
                        renewal_flag = dt["renewal_flag"].ToString(),
                        enhancement_flag = dt["enhancement_flag"].ToString()

                    });

                }
                values.cadapplicationlist = getapplicationadd_list;
                values.status = true;
            }
            else
            {
                values.status = false;
            }

            dt_datatable.Dispose();
        }

        public void DaGetSentBackToCCSummary(string employee_gid, MdlMstCAD values)
        {
            msSQL = " select a.application_gid,a.application_no,a.customerref_name,a.customer_urn,a.creditgroup_name,a.created_by, " +
                     " a.customer_name as customer_name,date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as ccapproved_date, a.approval_status," +
                     " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as cadsentback_by, " +
                     " date_format(a.processupdated_date, '%d-%m-%Y %h:%i %p') as cadsentback_date, " +
                     " a.creditgroup_gid,a.renewal_flag,a.enhancement_flag from ocs_mst_tapplication a " +
                     " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by " +
                     " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                     " where a.process_type = 'Sendback to CC' " +
                     " group by a.application_gid order by a.updated_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<cadapplicationlist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    string lsccgroup_name;
                    string lsccadmin_name;

                    msSQL = "select group_concat(ccgroup_name) as ccgroup_name from ocs_mst_tapplication where application_gid='" + dt["application_gid"] + "'";
                    objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDataReader.HasRows == true)
                    {
                        lsccgroup_name = objODBCDataReader["ccgroup_name"].ToString();
                    }
                    else
                    {
                        lsccgroup_name = "";
                    }
                    objODBCDataReader.Close();
                    msSQL = "select group_concat(ccadmin_name) as ccadmin_name from ocs_mst_tccschedulemeeting where application_gid='" + dt["application_gid"] + "'";
                    objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDataReader.HasRows == true)
                    {
                        lsccadmin_name = objODBCDataReader["ccadmin_name"].ToString();
                    }
                    else
                    {
                        lsccadmin_name = "";
                    }
                    objODBCDataReader.Close();
                    getapplicationadd_list.Add(new cadapplicationlist
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        ccapproved_date = dt["ccapproved_date"].ToString(),
                        creditgroup_name = dt["creditgroup_name"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        ccgroup_name = lsccgroup_name,
                        ccadmin_name = lsccadmin_name,
                        cadsentback_by = dt["cadsentback_by"].ToString(),
                        cadsentback_date = dt["cadsentback_date"].ToString(),
                        renewal_flag = dt["renewal_flag"].ToString(),
                        enhancement_flag = dt["enhancement_flag"].ToString()

                    });

                }
                values.cadapplicationlist = getapplicationadd_list;
                values.status = true;
            }
            else
            {
                values.status = false;
            }

            dt_datatable.Dispose();
        }

        public void DaGetCADAcceptedCustomerSummary(string employee_gid, MdlMstCAD values)
        {
            msSQL = "call ocs_trn_applicationacceptedsummary";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<cadapplicationlist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    //string lsccgroup_name;
                    //string lsccadmin_name;
                    //string lscadgroup_name;

                    //msSQL = "select group_concat(ccgroup_name) as ccgroup_name from ocs_mst_tapplication where application_gid='" + dt["application_gid"] + "'";
                    //objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    //if (objODBCDataReader.HasRows == true)
                    //{
                    //    lsccgroup_name = objODBCDataReader["ccgroup_name"].ToString();
                    //}
                    //else
                    //{
                    //    lsccgroup_name = "";
                    //}
                    //objODBCDataReader.Close();
                    //msSQL = "select group_concat(ccadmin_name) as ccadmin_name from ocs_mst_tccschedulemeeting where application_gid='" + dt["application_gid"] + "'";
                    //objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    //if (objODBCDataReader.HasRows == true)
                    //{
                    //    lsccadmin_name = objODBCDataReader["ccadmin_name"].ToString();
                    //}
                    //else
                    //{
                    //    lsccadmin_name = "";
                    //}
                    //objODBCDataReader.Close();
                    //msSQL = "select group_concat(cadgroup_name) as cadgroup_name from ocs_trn_tprocesstype_assign where application_gid='" + dt["application_gid"] + "'";
                    //objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    //if (objODBCDataReader.HasRows == true)
                    //{
                    //    lscadgroup_name = objODBCDataReader["cadgroup_name"].ToString();
                    //}
                    //else
                    //{
                    //    lscadgroup_name = "";
                    //}
                    //objODBCDataReader.Close();
                    getapplicationadd_list.Add(new cadapplicationlist
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        ccapproved_date = dt["ccapproved_date"].ToString(),
                        creditgroup_name = dt["creditgroup_name"].ToString(),
                        //ccgroup_name = lsccgroup_name,
                        //ccadmin_name = lsccadmin_name,
                        cadgroupname = dt["cadgroup_name"].ToString(),
                        cadaccepted_by = dt["cadaccepted_by"].ToString(),
                        cadaccepted_date = dt["cadaccepted_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        vertical_name = dt["vertical_name"].ToString(),
                        region = dt["region"].ToString(),
                        product_gid = dt["product_gid"].ToString(),
                        variety_gid = dt["variety_gid"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        renewal_flag = dt["renewal_flag"].ToString(),
                        enhancement_flag = dt["enhancement_flag"].ToString(),
                        LMS_status = dt["LMS_status"].ToString(),
                    });

                }
                values.cadapplicationlist = getapplicationadd_list;
                values.status = true;
            }
            else
            {
                values.status = false;
            }

            dt_datatable.Dispose();
        }

        public void DaGetSentBackToUnderwritingSummary(string employee_gid, MdlMstCAD values)
        {
            msSQL = " select a.application_gid,a.application_no,a.customerref_name,a.customer_urn,a.creditgroup_name,a.created_by, " +
                     " a.customer_name as customer_name, date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as ccapproved_date, a.approval_status," +
                     " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as cadsentback_by, " +
                     " date_format(a.processupdated_date, '%d-%m-%Y %h:%i %p') as cadsentback_date, " +
                     " a.creditgroup_gid,a.renewal_flag,a.enhancement_flag from ocs_mst_tapplication a " +
                     " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by " +
                     " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                     " left join ocs_trn_tccmeetingskip d on d.application_gid = a.application_gid " +
                     " where a.process_type = 'Sendback to Credit Underwriting' or " +
                     " (d.created_date = (select max(h.created_date) from ocs_trn_tccmeetingskip h where h.application_gid=a.application_gid)) " +
                     " group by a.application_gid order by a.updated_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<cadapplicationlist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    string lsccgroup_name;
                    string lsccadmin_name;

                    msSQL = "select group_concat(ccgroup_name) as ccgroup_name from ocs_mst_tapplication where application_gid='" + dt["application_gid"] + "'";
                    objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDataReader.HasRows == true)
                    {
                        lsccgroup_name = objODBCDataReader["ccgroup_name"].ToString();
                    }
                    else
                    {
                        lsccgroup_name = "";
                    }
                    objODBCDataReader.Close();
                    msSQL = "select group_concat(ccadmin_name) as ccadmin_name from ocs_mst_tccschedulemeeting where application_gid='" + dt["application_gid"] + "'";
                    objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDataReader.HasRows == true)
                    {
                        lsccadmin_name = objODBCDataReader["ccadmin_name"].ToString();
                    }
                    else
                    {
                        lsccadmin_name = "";
                    }
                    objODBCDataReader.Close();
                    getapplicationadd_list.Add(new cadapplicationlist
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        ccapproved_date = dt["ccapproved_date"].ToString(),
                        creditgroup_name = dt["creditgroup_name"].ToString(),
                        ccgroup_name = lsccgroup_name,
                        ccadmin_name = lsccadmin_name,
                        cadsentback_by = dt["cadsentback_by"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        cadsentback_date = dt["cadsentback_date"].ToString(),
                        renewal_flag = dt["renewal_flag"].ToString(),
                        enhancement_flag = dt["enhancement_flag"].ToString()
                    });

                }
                values.cadapplicationlist = getapplicationadd_list;
                values.status = true;
            }
            else
            {
                values.status = false;
            }

            dt_datatable.Dispose();
        }

        public void DaGetCCRejectedSummary(string employee_gid, MdlMstCAD values)
        {
            msSQL = " select a.application_gid,a.application_no,a.customerref_name,a.customer_urn,a.creditgroup_name,a.created_by, " +
                     " a.customer_name as customer_name,date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as ccapproved_date, a.approval_status," +
                     " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as ccrejected_by, " +
                     " date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as ccrejected_date, " +
                     " a.renewal_flag,a.enhancement_flag from ocs_mst_tapplication a " +
                     " left join hrm_mst_temployee b on b.employee_gid = a.cccompleted_by " +
                     " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                     " where a.approval_status = 'CC Rejected' and a.process_type is null" +
                     " group by a.application_gid order by a.updated_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<cadapplicationlist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    string lsccgroup_name;
                    string lsccadmin_name;
                    msSQL = "select group_concat(ccgroup_name) as ccgroup_name from ocs_mst_tapplication where application_gid='" + dt["application_gid"] + "'";
                    objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDataReader.HasRows == true)
                    {
                        lsccgroup_name = objODBCDataReader["ccgroup_name"].ToString();
                    }
                    else
                    {
                        lsccgroup_name = "";
                    }
                    objODBCDataReader.Close();
                    msSQL = "select group_concat(ccadmin_name) as ccadmin_name from ocs_mst_tccschedulemeeting where application_gid='" + dt["application_gid"] + "'";
                    objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDataReader.HasRows == true)
                    {
                        lsccadmin_name = objODBCDataReader["ccadmin_name"].ToString();
                    }
                    else
                    {
                        lsccadmin_name = "";
                    }
                    objODBCDataReader.Close();
                    getapplicationadd_list.Add(new cadapplicationlist
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        ccapproved_date = dt["ccapproved_date"].ToString(),
                        creditgroup_name = dt["creditgroup_name"].ToString(),
                        ccrejected_by = dt["ccrejected_by"].ToString(),
                        ccrejected_date = dt["ccrejected_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        ccgroup_name = lsccgroup_name,
                        ccadmin_name = lsccadmin_name,
                        renewal_flag = dt["renewal_flag"].ToString(),
                        enhancement_flag = dt["enhancement_flag"].ToString()

                    });

                }
                values.cadapplicationlist = getapplicationadd_list;
                values.status = true;
            }
            else
            {
                values.status = false;
            }
            dt_datatable.Dispose();
        }

        public void DaCADApplicationCount(string user_gid, string employee_gid, CadApplicationCount values)
        {
            msSQL = " select count(a.application_gid) as cadreview_count from ocs_mst_tapplication a " +
                    " left join ocs_trn_tccmeetingskip d on d.application_gid = a.application_gid " +
                    " where (a.approval_status = 'CC Approved' and  a.process_type is null and d.application_gid is null) or " +
 " (a.approval_status = 'CC Approved' and  a.process_type is null and d.application_gid is not null and d.ccmeetingskip_flag = 'Y' and " +
 " d.created_date = (select max(f.created_date) from ocs_trn_tccmeetingskip f where f.application_gid = a.application_gid))  ";
            values.cadreview_count = objdbconn.GetExecuteScalar(msSQL);
            int cadreview_count = Convert.ToInt16(values.cadreview_count);

            msSQL = " select count(application_gid) as sentbackcc_count from ocs_mst_tapplication a " +
                     " where a.process_type = 'Sendback to CC'";
            values.sentbackcc_count = objdbconn.GetExecuteScalar(msSQL);
            int sentbackcc_count = Convert.ToInt16(values.sentbackcc_count);

            msSQL = " select count(application_gid) as accept_count from ocs_trn_tcadapplication a " +
                    " where a.process_type = 'Accept'";
            values.accept_count = objdbconn.GetExecuteScalar(msSQL);
            int accept_count = Convert.ToInt16(values.accept_count);

            msSQL = " select count(application_gid) as urngrouping_count from ocs_trn_tcadapplication a " +
                    " where a.process_type = 'Accept' and (a.processupdated_date = (select max(e.processupdated_date) from ocs_trn_tcadapplication e where e.customer_urn = a.customer_urn and a.customer_urn <> '')) ";
            values.urngrouping_count = objdbconn.GetExecuteScalar(msSQL);
            int urngrouping_count = Convert.ToInt16(values.urngrouping_count);

            msSQL = " select count(a.application_gid) as backtounderwriting_count from ocs_mst_tapplication a " +
                     " left join ocs_trn_tccmeetingskip b on b.application_gid = a.application_gid " +
                     " where a.process_type = 'Sendback to Credit Underwriting' or " +
                     " (b.created_date = (select max(h.created_date) from ocs_trn_tccmeetingskip h where h.application_gid=a.application_gid)) ";
            values.backtounderwriting_count = objdbconn.GetExecuteScalar(msSQL);
            int backtounderwriting_count = Convert.ToInt16(values.backtounderwriting_count);

            msSQL = " select count(application_gid) as ccrejected_count from ocs_mst_tapplication a " +
                     " where a.approval_status = 'CC Rejected' and a.process_type is null";
            values.ccrejected_count = objdbconn.GetExecuteScalar(msSQL);
            int ccrejected_count = Convert.ToInt16(values.ccrejected_count);


            int lstotal = cadreview_count + sentbackcc_count + accept_count + backtounderwriting_count + ccrejected_count;
            values.lstotalcount = Convert.ToInt16(lstotal);
        }

        public void DaGetCADMembers(string cadgroup_gid, MdlCadGroup objmaster)
        {
            msSQL = " select cadgroup_gid,cadgroup_name  from ocs_mst_tcadgroup " +
                    " where cadgroup_gid='" + cadgroup_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                objmaster.cadgroup_gid = objODBCDataReader["cadgroup_gid"].ToString();
                objmaster.cadgroup_name = objODBCDataReader["cadgroup_name"].ToString();

            }
            objODBCDataReader.Close();
            // Approval Name
            //msSQL = " select cadgroupmanager_gid,employee_gid,employee_name from ocs_mst_tcadgroupassignment a " +
            //        " left join ocs_mst_tcadgroupmanager b on b.cadgroup_gid = a.cadgroup_gid " +
            //        " where a.cadgroup_gid='" + cadgroup_gid + "' and a.menu_gid = '" + menu_gid + "' group by a.cadgroup_gid ";
            //dt_datatable = objdbconn.GetDataTable(msSQL);
            //var getcadmanagerList = new List<cadmanager>();
            //if (dt_datatable.Rows.Count != 0)
            //{
            //    foreach (DataRow dt in dt_datatable.Rows)
            //    {
            //        getcadmanagerList.Add(new cadmanager
            //        {
            //            cadgroupmanager_gid = dt["cadgroupmanager_gid"].ToString(),
            //            employee_gid = dt["employee_gid"].ToString(),
            //            employee_name = dt["employee_name"].ToString(),
            //        });
            //        objmaster.cadmanager = getcadmanagerList;
            //    }
            //}
            //dt_datatable.Dispose();
            // Checker,Approver and Maker Name
            msSQL = " select member_gid, employee_gid, employee_name from (select cadgroupmanager_gid as member_gid, employee_gid, employee_name from ocs_mst_tcadgroupmanager where cadgroup_gid = '" + cadgroup_gid + "'" +
                    " union " +
                    " select cadgroupmembers_gid as member_gid, employee_gid, employee_name from ocs_mst_tcadgroupmembers where cadgroup_gid ='" + cadgroup_gid + "') employee " +
                    " group by employee_gid ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getCadmembersList = new List<cadmembers>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getCadmembersList.Add(new cadmembers
                    {
                        member_gid = dt["member_gid"].ToString(),
                        employee_gid = dt["employee_gid"].ToString(),
                        employee_name = dt["employee_name"].ToString(),
                    });
                    objmaster.cadmembers = getCadmembersList;
                }
            }
            dt_datatable.Dispose();

        }

        public void DaPostProcessType(MdlProcessType values, string employee_gid)
        {
            if (values.applyall_flag == "N")
            {
                for (var i = 0; i < values.menulist.Count; i++)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("PRTA");
                    msSQL = " insert into ocs_trn_tprocesstype_assign(" +
                            " processtypeassign_gid ," +
                            " application_gid," +
                            " processtype_name," +
                            " cadgroup_gid ," +
                            " cadgroup_name," +
                            " menu_gid ," +
                            " menu_name," +
                            " maker_gid ," +
                            " maker_name," +
                            " checker_gid," +
                            " checker_name ," +
                            " approver_gid," +
                            " approver_name ," +
                            " created_by," +
                            " created_date)" +
                            " values(" +
                            "'" + msGetGid + "'," +
                            "'" + values.application_gid + "'," +
                            "'" + values.processtype_name + "'," +
                            "'" + values.cadgroup_gid + "'," +
                            "'" + values.cadgroup_name + "'," +
                            "'" + values.menulist[i].menu_gid + "'," +
                            "'" + values.menulist[i].menu_name + "'," +
                            "'" + values.maker_gid + "'," +
                            "'" + values.maker_name + "'," +
                            "'" + values.checker_gid + "'," +
                            "'" + values.checker_name + "'," +
                            "'" + values.approver_gid + "'," +
                            "'" + values.approver_name + "'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            else if (values.applyall_flag == "Y")
            {
                msSQL = " select module_code as menu_gid,module_name as menu_name from adm_mst_tmodule where module_gid in ('CADMGTSAN','CADMGTDCL','CADMGTLSA','CADMGTDTS','CADMGTPYD','CADMGTCMS')" +
                        " and module_gid not in (select menu_gid from ocs_trn_tprocesstype_assign where application_gid = '" + values.application_gid + "')";
                dt_child = objdbconn.GetDataTable(msSQL);
                if (dt_child.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_child.Rows)
                    {

                        msGetGid = objcmnfunctions.GetMasterGID("PRTA");
                        msSQL = " insert into ocs_trn_tprocesstype_assign(" +
                                " processtypeassign_gid ," +
                                " application_gid," +
                                " processtype_name," +
                                " cadgroup_gid ," +
                                " cadgroup_name," +
                                " menu_gid ," +
                                " menu_name," +
                                " maker_gid ," +
                                " maker_name," +
                                " checker_gid," +
                                " checker_name ," +
                                " approver_gid," +
                                " approver_name ," +
                                " created_by," +
                                " created_date)" +
                                " values(" +
                                "'" + msGetGid + "'," +
                                "'" + values.application_gid + "'," +
                                "'" + values.processtype_name + "'," +
                                "'" + values.cadgroup_gid + "'," +
                                "'" + values.cadgroup_name + "'," +
                                "'" + dt["menu_gid"].ToString() + "'," +
                                "'" + dt["menu_name"].ToString() + "'," +
                                "'" + values.maker_gid + "'," +
                                "'" + values.maker_name + "'," +
                                "'" + values.checker_gid + "'," +
                                "'" + values.checker_name + "'," +
                                "'" + values.approver_gid + "'," +
                                "'" + values.approver_name + "'," +
                                "'" + employee_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                dt_child.Dispose();
            }
            else
            {
                values.message = "Error Occured While Adding";
                values.status = false;
            }

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Process Type Added Successfully";

            }
            else
            {
                values.message = "Error Occured While Adding";
                values.status = false;
            }
        }

        public void DaGetProcessTypeSummary(string application_gid, MdlMstProcessTypeSummary objmaster, string employee_gid)
        {
            try
            {
                msSQL = " SELECT a.processtypeassign_gid, a.application_gid, a.processtype_name, a.cadgroup_name, a.menu_name, " +
                        " a.checker_name, a.approver_name,a.maker_name, " +
                        " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by " +
                        " FROM ocs_trn_tprocesstype_assign a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where application_gid = '" + application_gid + "' order by a.created_date desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getprocesstype_list = new List<processtype_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getprocesstype_list.Add(new processtype_list
                        {
                            processtypeassign_gid = (dr_datarow["processtypeassign_gid"].ToString()),
                            application_gid = (dr_datarow["application_gid"].ToString()),
                            processtype_name = (dr_datarow["processtype_name"].ToString()),
                            cadgroup_name = (dr_datarow["cadgroup_name"].ToString()),
                            menu_name = (dr_datarow["menu_name"].ToString()),
                            checker_name = (dr_datarow["checker_name"].ToString()),
                            maker_name = (dr_datarow["maker_name"].ToString()),
                            approver_name = (dr_datarow["approver_name"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                        });
                    }
                    objmaster.processtype_list = getprocesstype_list;
                }
                dt_datatable.Dispose();

                msSQL = " SELECT a.processtypeassign_gid, a.application_gid, a.processtype_name, a.cadgroup_name,a.cadgroup_gid, a.menu_name, " +
                        " a.checker_name, a.approver_name,a.maker_name, " +
                        " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by " +
                        " FROM ocs_trn_tprocesstype_assign a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where application_gid = '" + application_gid + "' order by a.created_date desc ";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objmaster.application_gid = objODBCDatareader["application_gid"].ToString();
                    objmaster.processtype_name = objODBCDatareader["processtype_name"].ToString();
                    objmaster.cadgroup_name = objODBCDatareader["cadgroup_name"].ToString();
                    objmaster.cadgroup_gid = objODBCDatareader["cadgroup_gid"].ToString();
                }

                objODBCDatareader.Close();

                objmaster.status = true;
            }
            catch (Exception ex)
            {
                objmaster.status = false;
            }
        }

        public void DaGetCADBasicView(string application_gid, MdlMstCAD values)
        {
            try
            {
                msSQL = " select vertical_gid from ocs_mst_tapplication where application_gid='" + application_gid + "'";
                string lsvertical_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select entity_name from ocs_mst_tvertical where vertical_gid='" + lsvertical_gid + "'";
                string lsentity_name = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select entity_gid, entity_name from adm_mst_tentity where entity_name='" + lsentity_name + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows == true)
                {
                    values.entity_gid = objODBCDataReader["entity_gid"].ToString();
                    values.entity_name = objODBCDataReader["entity_name"].ToString();
                }
                objODBCDataReader.Close();

                msSQL = " select date_format(approved_date,'%d-%m-%Y %H:%i %p') as approved_date from ocs_trn_tAppcreditapproval" +
                        " where application_gid='" + application_gid + "' and hierary_level='3'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows == true)
                {
                    values.creditapproved_date = objODBCDataReader["approved_date"].ToString();
                }
                objODBCDataReader.Close();

                msSQL = " select vertical_code from ocs_mst_tvertical where vertical_gid='" + lsvertical_gid + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows == true)
                {
                    values.vertical_code = objODBCDataReader["vertical_code"].ToString();
                }
                objODBCDataReader.Close();

                msSQL = " select stakeholder_type, stakeholdertype_gid from ocs_mst_tinstitution where application_gid = '" + application_gid + "'" +
                        " union " +
                        " select stakeholder_type, stakeholdertype_gid from ocs_mst_tcontact where application_gid = '" + application_gid + "' ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getsanctiontype_list = new List<sanctiontype_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getsanctiontype_list.Add(new sanctiontype_list
                        {
                            sanctiontype_gid = dt["stakeholdertype_gid"].ToString(),
                            sanctiontype_name = dt["stakeholder_type"].ToString(),
                        });
                    }
                }
                values.sanctiontype_list = getsanctiontype_list;
                dt_datatable.Dispose();

                values.status = true;
                values.message = "success";
            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }
        }

        public void DaGetSanctionToList(string sanctiontype_name, string application_gid, MdlMstCAD values)
        {
            if (sanctiontype_name == "Applicant")
            {
                msSQL = " select applicant_type from ocs_mst_tapplication where application_gid = '" + application_gid + "'";
                string lsapplicant_type = objdbconn.GetExecuteScalar(msSQL);
                if (lsapplicant_type == "Institution")
                {
                    msSQL = " select company_name, institution_gid from ocs_mst_tinstitution where application_gid = '" + application_gid + "'" +
                            " and stakeholder_type='Applicant'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getsanctionto_list = new List<sanctionto_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            getsanctionto_list.Add(new sanctionto_list
                            {
                                sanctionto_name = dt["company_name"].ToString(),
                                sanctionto_gid = dt["institution_gid"].ToString(),
                            });
                        }
                    }
                    values.sanctionto_list = getsanctionto_list;
                    dt_datatable.Dispose();
                }
                else
                {
                    msSQL = " select concat(first_name, ' ', middle_name, ' ', last_name) as individual_name, contact_gid from ocs_mst_tcontact" +
                            " where application_gid = '" + application_gid + "' and stakeholder_type='Applicant'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getsanctionto_list = new List<sanctionto_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            getsanctionto_list.Add(new sanctionto_list
                            {
                                sanctionto_name = dt["individual_name"].ToString(),
                                sanctionto_gid = dt["contact_gid"].ToString(),
                            });
                        }
                    }
                    values.sanctionto_list = getsanctionto_list;
                    dt_datatable.Dispose();
                }
            }
            else
            {
                msSQL = " select a.company_name as sanctionto_name, a.institution_gid as sanctionto_gid from ocs_mst_tinstitution a " +
                        " inner join ocs_mst_tapplication b on a.application_gid = b.application_gid " +
                        " where b.application_gid = '" + application_gid + "' and a.stakeholder_type = '" + sanctiontype_name + "'" +
                        " union " +
                        " select concat(first_name, ' ', middle_name, ' ', last_name) as sanctionto_name, a.contact_gid as sanctionto_gid from ocs_mst_tcontact a " +
                        " inner join ocs_mst_tapplication b on a.application_gid = b.application_gid " +
                        " where b.application_gid = '" + application_gid + "' and a.stakeholder_type = '" + sanctiontype_name + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getsanctionto_list = new List<sanctionto_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getsanctionto_list.Add(new sanctionto_list
                        {
                            sanctionto_name = dt["sanctionto_name"].ToString(),
                            sanctionto_gid = dt["sanctionto_gid"].ToString(),
                        });
                    }
                }
                values.sanctionto_list = getsanctionto_list;
                dt_datatable.Dispose();
            }
        }

        public void DaGetContactPersonDetail(string sanctionto_gid, MdlMstCAD values)
        {
            msSQL = " select contact_gid from ocs_mst_tcontact where contact_gid = '" + sanctionto_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                objODBCDataReader.Close();
                msSQL = "select concat(first_name,',',middle_name,',',last_name) as contactperson_name from ocs_mst_tcontact where contact_gid = '" + sanctionto_gid + "'";
                objODBCDataReader1 = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader1.HasRows == true)
                {
                    values.contactperson_name = objODBCDataReader1["contactperson_name"].ToString();
                }
                objODBCDataReader1.Close();

                //msSQL = "select concat(addressline1,',',addressline2) as primary_address from ocs_mst_tcontact2address where contact_gid = '" + sanctionto_gid + "' and primary_status='Yes'";
                //objODBCDataReader1 = objdbconn.GetDataReader(msSQL);
                //if (objODBCDataReader1.HasRows == true)
                //{
                //    values.primary_address = objODBCDataReader1["primary_address"].ToString();
                //}
                //objODBCDataReader1.Close();

                msSQL = " select concat(addressline1,',',addressline2) as address, contact2address_gid as address_gid from ocs_mst_tcontact2address where contact_gid = '" + sanctionto_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var geaddress_list = new List<cadaddress_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        geaddress_list.Add(new cadaddress_list
                        {
                            address_gid = dt["address_gid"].ToString(),
                            address = dt["address"].ToString(),
                        });
                    }
                }
                values.cadaddress_list = geaddress_list;
                dt_datatable.Dispose();

                msSQL = " select mobile_no, contact2mobileno_gid as mobileno_gid from ocs_mst_tcontact2mobileno where contact_gid = '" + sanctionto_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmobileno_list = new List<cadmobileno_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getmobileno_list.Add(new cadmobileno_list
                        {
                            mobileno_gid = dt["mobileno_gid"].ToString(),
                            mobile_no = dt["mobile_no"].ToString(),
                        });
                    }
                }
                values.cadmobileno_list = getmobileno_list;
                dt_datatable.Dispose();

                msSQL = " select email_address, contact2email_gid as email_gid from ocs_mst_tcontact2email where contact_gid = '" + sanctionto_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getemail_list = new List<cademail_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getemail_list.Add(new cademail_list
                        {
                            email_gid = dt["email_gid"].ToString(),
                            email_address = dt["email_address"].ToString(),
                        });
                    }
                }
                values.cademail_list = getemail_list;
                dt_datatable.Dispose();

            }
            else
            {
                objODBCDataReader.Close();
                msSQL = "select concat(contactperson_firstname,',',contactperson_middlename,',',contactperson_lastname) as contactperson_name from ocs_mst_tinstitution where institution_gid = '" + sanctionto_gid + "'";
                objODBCDataReader1 = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader1.HasRows == true)
                {
                    values.contactperson_name = objODBCDataReader1["contactperson_name"].ToString();
                }
                objODBCDataReader1.Close();

                //msSQL = "select concat(addressline1,',',addressline2) as primary_address from ocs_mst_tinstitution2address where institution_gid = '" + sanctionto_gid + "' and primary_status='Yes'";
                //objODBCDataReader1 = objdbconn.GetDataReader(msSQL);
                //if (objODBCDataReader1.HasRows == true)
                //{
                //    values.primary_address = objODBCDataReader1["primary_address"].ToString();
                //}
                //objODBCDataReader1.Close();
                msSQL = " select concat(addressline1,',',addressline2) as address, institution2address_gid as address_gid from ocs_mst_tinstitution2address where institution_gid = '" + sanctionto_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var geaddress_list = new List<cadaddress_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        geaddress_list.Add(new cadaddress_list
                        {
                            address_gid = dt["address_gid"].ToString(),
                            address = dt["address"].ToString(),
                        });
                    }
                }
                values.cadaddress_list = geaddress_list;
                dt_datatable.Dispose();

                msSQL = " select mobile_no, institution2mobileno_gid as mobileno_gid from ocs_mst_tinstitution2mobileno where institution_gid = '" + sanctionto_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmobileno_list = new List<cadmobileno_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getmobileno_list.Add(new cadmobileno_list
                        {
                            mobileno_gid = dt["mobileno_gid"].ToString(),
                            mobile_no = dt["mobile_no"].ToString(),
                        });
                    }
                }
                values.cadmobileno_list = getmobileno_list;
                dt_datatable.Dispose();

                msSQL = " select email_address, institution2email_gid as email_gid from ocs_mst_tinstitution2email where institution_gid = '" + sanctionto_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getemail_list = new List<cademail_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getemail_list.Add(new cademail_list
                        {
                            email_gid = dt["email_gid"].ToString(),
                            email_address = dt["email_address"].ToString(),
                        });
                    }
                }
                values.cademail_list = getemail_list;
                dt_datatable.Dispose();
            }

        }

        public void DaGetBuyerList(string application_gid, MdlMstCAD values, string employee_gid)
        {
            msSQL = " select application2loan_gid from ocs_mst_tapplication2loan where application_gid='" + application_gid + "' and product_type='Agri Receivable Finance (ARF)'";
            string lsapplication2loan_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select application2buyer_gid,buyer_gid,buyer_name,buyer_limit,availed_limit,balance_limit,margin,bill_tenure " +
                   " from ocs_mst_tapplication2buyer where application2loan_gid='" + lsapplication2loan_gid + "'" +
                   " union" +
                   " select creditbuyer_gid as application2buyer_gid,buyer_gid,buyer_name,format(buyer_limit,'en-IN')as buyer_limit,format(availed_limit,'en-IN')as availed_limit, " +
                   " format(balance_limit,'en-IN')as balance_limit, margin, bill_tenuredays as bill_tenure from ocs_mst_tcreditbuyer" +
                   " where application_gid='" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getrmbuyer_list = new List<rmbuyer_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getrmbuyer_list.Add(new rmbuyer_list
                    {
                        application2buyer_gid = (dr_datarow["application2buyer_gid"].ToString()),
                        buyer_gid = (dr_datarow["buyer_gid"].ToString()),
                        buyer_name = (dr_datarow["buyer_name"].ToString()),
                        buyer_limit = (dr_datarow["buyer_limit"].ToString()),
                        availed_limit = (dr_datarow["availed_limit"].ToString()),
                        balance_limit = (dr_datarow["balance_limit"].ToString()),
                        bill_tenuredays = (dr_datarow["bill_tenure"].ToString()),
                        margin = (dr_datarow["margin"].ToString())
                    });
                }
                values.rmbuyer_list = getrmbuyer_list;
            }
            dt_datatable.Dispose();

            msSQL = " select creditbuyer_gid as application2buyer_gid,buyer_gid,buyer_name,format(buyer_limit,'en-IN')as buyer_limit,format(availed_limit,'en-IN')as availed_limit, " +
                   " format(balance_limit,'en-IN')as balance_limit, margin, bill_tenuredays as bill_tenure from ocs_mst_tcreditbuyer" +
                   " where application_gid='" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcreditbuyer_list = new List<creditbuyer_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcreditbuyer_list.Add(new creditbuyer_list
                    {
                        application2buyer_gid = (dr_datarow["application2buyer_gid"].ToString()),
                        buyer_gid = (dr_datarow["buyer_gid"].ToString()),
                        buyer_name = (dr_datarow["buyer_name"].ToString()),
                        buyer_limit = (dr_datarow["buyer_limit"].ToString()),
                        availed_limit = (dr_datarow["availed_limit"].ToString()),
                        balance_limit = (dr_datarow["balance_limit"].ToString()),
                        bill_tenuredays = (dr_datarow["bill_tenure"].ToString()),
                        margin = (dr_datarow["margin"].ToString())
                    });
                }
                values.creditbuyer_list = getcreditbuyer_list;
            }
            dt_datatable.Dispose();

            values.status = true;
        }

        public void DaGetProductList(string application_gid, MdlMstCAD values)
        {
            try
            {
                msSQL = "select product_type, producttype_gid from ocs_mst_tapplication2loan where application_gid='" + application_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getproducttype = new List<producttype_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getproducttype.Add(new producttype_list
                        {
                            product_type = (dr_datarow["product_type"].ToString()),
                            producttype_gid = (dr_datarow["producttype_gid"].ToString()),
                        });
                    }
                    values.producttype_list = getproducttype;
                }
                dt_datatable.Dispose();

                values.status = true;
            }
            catch
            {
                values.status = false;
            }

        }

        public void DaGetSubProductList(string producttype_gid, string application_gid, MdlMstCAD values)
        {
            try
            {
                msSQL = "select productsub_type,productsubtype_gid from ocs_mst_tapplication2loan where application_gid='" + application_gid + "' and producttype_gid='" + producttype_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getsubproducttype = new List<productsubtype_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getsubproducttype.Add(new productsubtype_list
                        {
                            productsub_type = (dr_datarow["productsub_type"].ToString()),
                            productsubtype_gid = (dr_datarow["productsubtype_gid"].ToString()),
                        });
                    }
                    values.productsubtype_list = getsubproducttype;
                }
                dt_datatable.Dispose();

                values.status = true;
            }
            catch
            {
                values.status = false;
            }

        }

        public void DaGetProductDetail(string application_gid, string productsubtype_gid, MdlMstCAD values)
        {
            msSQL = "select loanfacility_amount, rate_interest, facility_mode, tenureoverall_limit, loan_type, facility_type " +
                   " from ocs_mst_tapplication2loan where productsubtype_gid='" + productsubtype_gid + "' and application_gid='" + application_gid + "' ";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                values.loanfacility_amount = objODBCDataReader["loanfacility_amount"].ToString();
                values.rate_interest = objODBCDataReader["rate_interest"].ToString();
                values.facility_mode = objODBCDataReader["facility_mode"].ToString();
                values.tenureoverall_limit = objODBCDataReader["tenureoverall_limit"].ToString();
                values.loan_type = objODBCDataReader["loan_type"].ToString();
                values.facility_type = objODBCDataReader["facility_type"].ToString();
            }
            objODBCDataReader.Close();
            values.status = true;
        }

        public void DaGetMOMCAMDocument(string application_gid, MdlMstCAD values)
        {
            msSQL = " select application2momdoc_gid,application_gid,document_name,document_path,document_title, date_format(a.created_date,'%d-%m-%Y %H:%i %p') as created_date," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                    " from  ocs_mst_tapplication2momdoc a " +
                    " left join hrm_mst_temployee b on a.created_by=b.employee_gid" +
                    " left join adm_mst_tuser c on b.user_gid = c.user_gid" +
                    " where application_gid='" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdocumentdtlList = new List<cadmomdocument_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getdocumentdtlList.Add(new cadmomdocument_list
                    {
                        document_name = dt["document_name"].ToString(),
                        document_title = dt["document_title"].ToString(),
                        document_path = objcmnstorage.EncryptData((dt["document_path"].ToString())),
                        application_gid = dt["application_gid"].ToString(),
                        application2momdoc_gid = dt["application2momdoc_gid"].ToString(),
                        uploaded_by = dt["created_by"].ToString(),
                        updated_date = dt["created_date"].ToString()
                    });
                    values.cadmomdocument_list = getdocumentdtlList;
                }
            }
            dt_datatable.Dispose();

            msSQL = " select application2camdoc_gid,application_gid,document_name,document_path,document_title, date_format(a.created_date,'%d-%m-%Y %H:%i %p') as created_date," +
                   " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                   " from  ocs_mst_tapplication2camdoc a " +
                   " left join hrm_mst_temployee b on a.created_by=b.employee_gid" +
                   " left join adm_mst_tuser c on b.user_gid = c.user_gid" +
                   " where application_gid='" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcamdocumentdtlList = new List<cadcamdocument_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getcamdocumentdtlList.Add(new cadcamdocument_list
                    {
                        document_name = dt["document_name"].ToString(),
                        document_title = dt["document_title"].ToString(),
                        document_path = objcmnstorage.EncryptData((dt["document_path"].ToString())),
                        application_gid = dt["application_gid"].ToString(),
                        application2camdoc_gid = dt["application2camdoc_gid"].ToString(),
                        uploaded_by = dt["created_by"].ToString(),
                        updated_date = dt["created_date"].ToString()
                    });
                    values.cadcamdocument_list = getcamdocumentdtlList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaUpdateLoanDetails(string employee_gid, cadloanfacilitytype_list values)
        {
            msSQL = " update ocs_mst_tapplication2loan set " +
                     " interchangeability='" + values.interchangeability + "'," +
                     " report_structure='" + values.report_structure + "'," +
                     " document_limit='" + values.document_limit.Replace(",", "").Trim() + "'," +
                     " expiry_date='" + Convert.ToDateTime(values.expiry_date).ToString("yyyy-MM-dd") + "'," +
                     " updated_by='" + employee_gid + "'," +
                     " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                     " where application2loan_gid='" + values.application2loan_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.message = "Loan Details Updated Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }

        public void DaGetLoanDetail(string application2loan_gid, cadloanfacilitytype_list values)
        {
            msSQL = " select application2loan_gid,interchangeability,report_structure,document_limit, date_format(expiry_date,'%Y-%m-%d') as expiry_date" +
                    " from ocs_mst_tapplication2loan " +
                    " where application2loan_gid='" + application2loan_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                values.application2loan_gid = objODBCDataReader["application2loan_gid"].ToString();
                values.interchangeability = objODBCDataReader["interchangeability"].ToString();
                values.report_structure = objODBCDataReader["report_structure"].ToString();
                values.document_limit = objODBCDataReader["document_limit"].ToString();
                values.expiry_date = objODBCDataReader["expiry_date"].ToString();
            }
            objODBCDataReader.Close();
        }

        public bool DaPostCADSanction(string employee_gid, cadsanctiondetails values)
        {
            if (values.sanction_type == "Existing Customer")
            {
                if ((values.natureof_proposal == null) || (values.natureof_proposal == ""))
                {
                    values.message = "Kindly Select Nature of Proposal";
                    values.status = false;
                    return false;
                }
            }
            msGetGid = objcmnfunctions.GetMasterGID("AP2S");
            msGetRef = objcmnfunctions.GetMasterGID("SRNO");
            msGetRef = Regex.Replace(msGetRef, "[^0-9.]", "");
            string lsentity = "", lsverticalcode = "", lssanctionfromdate = "", lssanctiontodate = "";
            msSQL = " select b.entity_name,b.vertical_code from ocs_mst_tapplication a " +
                    " left join ocs_mst_tvertical b on a.vertical_gid = b.vertical_gid " +
                    " where a.application_gid = '" + values.application_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsentity = objODBCDatareader["entity_name"].ToString();
                lsverticalcode = objODBCDatareader["vertical_code"].ToString();
                if (lsentity.ToLower() == "samfin")
                    lsentity = "SF";
                else if (lsentity.ToLower() == "samagro")
                    lsentity = "SA";
                else
                    lsentity = "SR";
            }
            objODBCDatareader.Close();
            if (values.sanctionfrom_date != "" && values.sanctionfrom_date != null)
            {
                lssanctionfromdate = Convert.ToDateTime(values.sanctionfrom_date).ToString("dd-MM-yyyy");
                lssanctionfromdate = lssanctionfromdate.Replace("-", "");
            }
            if (values.sanctiontill_date != "" && values.sanctiontill_date != null)
            {
                lssanctiontodate = Convert.ToDateTime(values.sanctiontill_date).ToString("dd-MM-yyyy");
                lssanctiontodate = lssanctiontodate.Replace("-", "");
            }

            string sanctionref_no = lsentity + "/" + msGetRef + "/" + lsverticalcode + "/" + lssanctionfromdate + "-" + lssanctiontodate;


            msSQL = " INSERT INTO ocs_trn_tapplication2sanction( " +
                           " application2sanction_gid," +
                           " sanction_refno," +
                           " sanction_date," +
                           " sanction_amount," +
                           " entity," +
                           " entity_gid," +
                           " application_gid ," +
                           " ccapproved_date," +
                           " applicationtype_gid," +
                           " application_type," +
                           " sanctionto_gid," +
                           " sanctionto_name," +
                           " sanctionfrom_date," +
                           " sanctiontill_date," +
                           " contactpersonaddress_gid," +
                           " contactperson_address," +
                           " contactperson_name," +
                           " contactperson_number," +
                           " contactpersonmobileno_gid," +
                           " contactpersonemail_gid," +
                           " contactpersonemail_address," +
                           " sanction_type," +
                           " natureof_proposal," +
                           " paycard," +
                           " esdeclaration_status," +
                           " branch_gid, " +
                           " branch_name, " +
                           " created_by," +
                           " created_date," +
                           " updated_by," +
                           " updated_date)" +
                           " values(" +
                           "'" + msGetGid + "', " +
                           "'" + sanctionref_no + "'," +
                           "'" + Convert.ToDateTime(values.sanction_date).ToString("yyyy-MM-dd") + "'," +
                           "'" + values.sanction_amount + "'," +
                           "'" + values.entity + "'," +
                           "'" + values.entity_gid + "'," +
                           "'" + values.application_gid + "'," +
                           "'" + values.ccapproved_date + "'," +
                         "'" + values.applicationtype_gid + "'," +
                         "'" + values.application_type + "'," +
                         "'" + values.sanctionto_gid + "'," +
                         "'" + values.sanctionto_name.Replace("'", "") + "',";
            if ((values.sanctionfrom_date == null) || (values.sanctionfrom_date == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.sanctionfrom_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            if ((values.sanctiontill_date == null) || (values.sanctiontill_date == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.sanctiontill_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            msSQL += "'" + values.contactpersonaddress_gid + "'," +
                     "'" + values.contactperson_address.Replace("'", "") + "'," +
                     "'" + values.contactperson_name + "'," +
                     "'" + values.contactperson_number + "'," +
                     "'" + values.contactpersonmobileno_gid + "'," +
                     "'" + values.contactpersonemail_gid + "'," +
                     "'" + values.contactpersonemail_address + "'," +
                     "'" + values.sanction_type + "'," +
                     "'" + values.natureof_proposal + "'," +
                     "'" + values.paycard + "'," +
                     "'" + values.esdeclaration_status + "'," +
                     "'" + values.branch_gid + "'," +
                     "'" + values.branch_name + "'," +
                     "'" + employee_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                     "'" + employee_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult1 = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " select application_gid,relationshipmanager_name, relationshipmanager_gid, clustermanager_gid, clustermanager_name, zonalhead_name, zonalhead_gid," +
                    " regionalhead_name, regionalhead_gid, businesshead_name, businesshead_gid from ocs_mst_tapplication" +
                    " where application_gid = '" + values.application_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                msSQL = " update ocs_trn_tapplication2sanction set relationshipmgr_gid='" + objODBCDataReader["relationshipmanager_gid"].ToString() + "'," +
                        " relationshipmgr_name='" + objODBCDataReader["relationshipmanager_name"].ToString() + "' where application2sanction_gid='" + msGetGid + "'";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            objODBCDataReader.Close();

            if (mnResult1 != 0)
            {
                // Mdlloanfacility_type objvalue = new Mdlloanfacility_type();
                //DaGetCADLoanFacilityTemplateList(msGetGid, objvalue); Removed for Dynamic Template Concept

                //msSQL = " UPDATE ocs_trn_tloan SET sanction_refno='" + lssanctionref_no.Replace("'", "\\'") + "'," +
                //               " sanction_date='" + Convert.ToDateTime(values.sanction_date).ToString("yyyy-MM-dd") + "'" +
                //               " WHERE sanction_gid='" + msGetGid + "'";
                //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                //msSQL = " UPDATE ocs_trn_tdeferral a " +
                //       " LEFT JOIN ocs_trn_tloan b on a.loan_gid=b.loan_gid" +
                //       " SET a.sanction_refno='" + lssanctionref_no.Replace("'", "\\'") + "'," +
                //       " a.sanction_date='" + Convert.ToDateTime(values.sanction_date).ToString("yyyy-MM-dd") + "'" +
                //       " WHERE b.sanction_gid='" + msGetGid + "'";
                //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                //msSQL = " UPDATE ocs_trn_tdeferral2loan a " +
                //        " LEFT JOIN ocs_trn_tloan b on a.loan_gid=b.loan_gid" +
                //        " SET a.sanction_refno='" + lssanctionref_no.Replace("'", "\\'") + "'," +
                //        " a.sanction_date='" + Convert.ToDateTime(values.sanction_date).ToString("yyyy-MM-dd") + "'" +
                //        " WHERE b.sanction_gid='" + msGetGid + "'";
                //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                // CAM Document Updation
                msSQL = " select application2camdoc_gid, application_gid,document_name,document_path,document_title" +
                   " from ocs_mst_tapplication2camdoc a " +
                   " where application_gid='" + values.application_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getdocumentdtlList = new List<cadmomdocument_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        msGetGid1 = objcmnfunctions.GetMasterGID("ACAM");
                        msSQL = " insert into ocs_trn_tuploadcamdocument( " +
                                     " camdocument_gid," +
                                     " document_name, " +
                                     " document_path, " +
                                     " application2sanction_gid," +
                                     " application_gid," +
                                     " document_title," +
                                     " created_by ," +
                                     " created_date " +
                                     " )values(" +
                                     "'" + msGetGid1 + "'," +
                                     "'" + dt["document_name"].ToString() + "'," +
                                     "'" + dt["document_path"].ToString() + "'," +
                                     "'" + msGetGid + "'," +
                                     "'" + values.application_gid + "'," +
                                     "'" + dt["document_title"].ToString() + "'," +
                                     "'" + employee_gid + "'," +
                                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                dt_datatable.Dispose();

                // MOM Document Updation
                msSQL = " select application2momdoc_gid,application_gid,document_name,document_path,document_title" +
                       " from  ocs_mst_tapplication2momdoc a " +
                       " where application_gid='" + values.application_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcamdocumentdtlList = new List<cadcamdocument_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        msGetGid1 = objcmnfunctions.GetMasterGID("AMOM");
                        msSQL = " insert into ocs_trn_tuploadmomdocument( " +
                         " momdocument_gid," +
                         " document_name, " +
                         " document_path, " +
                         " application2sanction_gid," +
                         " application_gid," +
                         " document_title," +
                         " created_by ," +
                         " created_date " +
                         " )values(" +
                         "'" + msGetGid1 + "'," +
                         "'" + dt["document_name"].ToString() + "'," +
                         "'" + dt["document_path"].ToString() + "'," +
                         "'" + msGetGid + "'," +
                         "'" + values.application_gid + "'," +
                         "'" + dt["document_title"].ToString() + "'," +
                         "'" + employee_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                dt_datatable.Dispose();

                //Completed..//
                //CC Member//
                msSQL = " select ccmember_name,ccmember_gid,attendance_status,approval_status, ccmeeting2members_gid as ccmeeting2members_gid, ccgroup_name as ccgroup_name" +
                       " from ocs_mst_tccmeeting2members where application_gid='" + values.application_gid + "'" +
                       " union" +
                       " select employee_name as ccmember_name,employee_gid as ccmember_gid,attendance_status, approval_status, ccmeeting2othermembers_gid as ccmeeting2members_gid, '-' as ccgroup_name" +
                       " from ocs_mst_tccmeeting2othermembers where application_gid='" + values.application_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getccmember_list = new List<cadccmember_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        msGetGidCC = objcmnfunctions.GetMasterGID("ACCM");
                        msSQL = " insert into ocs_trn_tsanction2ccmemberlist(" +
                                " ccmemberlist_gid," +
                                " application2sanction_gid," +
                                " application_gid," +
                                " ccmember_gid," +
                                " ccmember_name," +
                                " ccgroup_name," +
                                " created_by," +
                                " created_date)" +
                                " values(" +
                                "'" + msGetGidCC + "'," +
                                "'" + msGetGid + "'," +
                                "'" + values.application_gid + "'," +
                                "'" + dt["ccmember_gid"].ToString() + "'," +
                                "'" + dt["ccmember_name"].ToString() + "'," +
                                "'" + dt["ccgroup_name"].ToString() + "'," +
                                "'" + employee_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                dt_datatable.Dispose();

                msSQL = " select application2loan_gid from ocs_mst_tapplication2loan where application_gid='" + values.application_gid + "' and productsub_type='Agri Receivable Finance (ARF)'";
                string lsapplication2loan_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select application2buyer_gid as addbuyer_gid, buyer_name, buyer_gid, buyer_limit, availed_limit, balance_limit, bill_tenure, margin " +
                       " from ocs_mst_tapplication2buyer where application2loan_gid='" + lsapplication2loan_gid + "'" +
                       " union " +
                       " select creditbuyer_gid as addbuyer_gid, buyer_name, buyer_gid, buyer_limit, availed_limit, balance_limit, bill_tenuredays as bill_tenure, margin " +
                       " from ocs_mst_tcreditbuyer where application_gid='" + values.application_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_filename = new List<cadbuyer_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        msGetGid1 = objcmnfunctions.GetMasterGID("ABUY");
                        msSQL = "insert into ocs_trn_taddbuyer (" +
                              " addbuyer_gid," +
                              " buyer_name," +
                              " application2sanction_gid," +
                              " application_gid," +
                              " buyer_limit," +
                              " availed_limit," +
                              " balance_limit," +
                              " bill_tenure," +
                              " margin," +
                              " created_by," +
                              " created_date" +
                              " )values(" +
                              "'" + msGetGid1 + "'," +
                              "'" + dr_datarow["buyer_name"].ToString() + "'," +
                              "'" + msGetGid + "'," +
                              "'" + values.application_gid + "'," +
                              "'" + dr_datarow["buyer_limit"].ToString() + "'," +
                              "'" + dr_datarow["availed_limit"].ToString() + "'," +
                              "'" + dr_datarow["balance_limit"].ToString() + "'," +
                              "'" + dr_datarow["bill_tenure"].ToString() + "'," +
                              "'" + dr_datarow["margin"].ToString() + "'," +
                              "'" + employee_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                dt_datatable.Dispose();

                msSQL = " update ocs_trn_tlimitproductinfo set application2sanction_gid='" + msGetGid + "' " +
                        " where application_gid='" + values.application_gid + "' and application2sanction_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (values.esdeclaration_status == "Yes")
                {
                    msSQL = "update ocs_trn_tuploadesdeclarationdocument set application2sanction_gid='" + msGetGid + "' where application2sanction_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "delete from ocs_trn_tdeviationmaildocument where application2sanction_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                else
                {
                    msSQL = "update ocs_trn_tdeviationmaildocument set application2sanction_gid='" + msGetGid + "' where application2sanction_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "delete from ocs_trn_tuploadesdeclarationdocument where application2sanction_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                values.status = true;
                values.message = "Sanction Created Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured While Creating Sanction";
                return false;

            }
        }

        public void DaPostlimitproductinfo(string employee_gid, limitandproducts values)
        {
            msSQL = " select producttype_gid,product_type,productsubtype_gid,productsub_type,loanfacility_amount " +
                   " from ocs_mst_tapplication2loan where application2loan_gid = '" + values.application2loan_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                values.producttype_gid = objODBCDataReader["producttype_gid"].ToString();
                values.product_type = objODBCDataReader["product_type"].ToString();
                values.productsubtype_gid = objODBCDataReader["productsubtype_gid"].ToString();
                values.productsub_type = objODBCDataReader["productsub_type"].ToString();
                values.loanfacility_amount = objODBCDataReader["loanfacility_amount"].ToString();
            }
            objODBCDataReader.Close();

            string msGetLimitGid = objcmnfunctions.GetMasterGID("LPIG");
            msSQL = " insert into ocs_trn_tlimitproductinfo(" +
                        " limitproductinfodtl_gid," +
                        " application_gid, " +
                        " application2sanction_gid," +
                        " application2loan_gid, " +
                        " producttype_gid, " +
                        " product_type, " +
                        " productsubtype_gid," +
                        " productsub_type," +
                        " loanfacility_amount, " +
                        " interchangeability," +
                        " report_structure_gid, " +
                        " report_structure," +
                        " odlim_amount," +
                        " odlim_condition," +
                        " documented_limit," +
                        " dateof_Expiry," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetLimitGid + "'," +
                        "'" + values.application_gid + "'," +
                        "'" + employee_gid + "'," +
                        "'" + values.application2loan_gid + "'," +
                        "'" + values.producttype_gid + "'," +
                        "'" + values.product_type + "'," +
                        "'" + values.productsubtype_gid + "'," +
                        "'" + values.productsub_type + "'," +
                        "'" + values.loanfacility_amount + "'," +
                        "'" + values.interchangeability + "',";
            if (values.report_structuregid == null)
                msSQL += "'',";
            else
                msSQL += "'" + values.report_structuregid + "',";
            if (values.report_structure == null)
                msSQL += "'',";
            else
                msSQL += "'" + values.report_structure.Replace("'", "").Trim() + "',";
            msSQL += "'" + values.odlim_amount.Replace(",", "") + "'," +
                       "'" + values.odlim_condition + "'," +
                       "'" + values.documented_limit.Replace(",", "") + "'," +
                       "'" + Convert.ToDateTime(values.dateof_Expiry).ToString("yyyy-MM-dd") + "',";
            msSQL += "'" + employee_gid + "'," +
                     "current_timestamp)";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "update ocs_mst_tapplication2loan set limit_product='Y' where application2loan_gid ='" + values.application2loan_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                values.status = true;
                values.message = "Product Details are Added Successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
        }


        public void DaGetSanctionLimitInfoDtl(limitandproductslist values, string application_gid, string employee_gid)
        {
            try
            {
                msSQL = " select limitproductinfodtl_gid,interchangeability,report_structure_gid,product_type,productsub_type, " +
                       " report_structure,format(odlim_amount,2,'en_IN') as odlim_amount,odlim_condition,format(existing_limit,2,'en_IN') as existing_limit , " +
                       " format(limit_released,2,'en_IN') as limit_released,format(documented_limit,2,'en_IN') as documented_limit, " +
                       " date_format(dateof_Expiry,'%d-%m-%Y') as dateof_Expiry, " +
                       " limitinfo_remarks,created_by,created_date from ocs_trn_tlimitproductinfo " +
                       " where application_gid='" + application_gid + "' and application2sanction_gid='" + employee_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getlimit_info = new List<limitandproducts>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getlimit_info.Add(new limitandproducts
                        {
                            limitproductinfodtl_gid = dr_datarow["limitproductinfodtl_gid"].ToString(),
                            existing_limit = (dr_datarow["existing_limit"].ToString()),
                            limit_released = (dr_datarow["limit_released"].ToString()),
                            interchangeability = (dr_datarow["interchangeability"].ToString()),
                            report_structuregid = (dr_datarow["report_structure_gid"].ToString()),
                            report_structure = (dr_datarow["report_structure"].ToString()),
                            odlim_amount = (dr_datarow["odlim_amount"].ToString()),
                            odlim_condition = (dr_datarow["odlim_condition"].ToString()),
                            limitinfo_remarks = (dr_datarow["limitinfo_remarks"].ToString()),
                            product_type = (dr_datarow["product_type"].ToString()),
                            productsub_type = (dr_datarow["productsub_type"].ToString()),
                            documented_limit = (dr_datarow["documented_limit"].ToString()),
                            dateof_Expiry = (dr_datarow["dateof_Expiry"].ToString()),
                        });
                    }
                    values.limitandproducts = getlimit_info;
                    dt_datatable.Dispose();
                }

                //msSQL = " select format(sum(limit_released),2,'en_IN') from ocs_trn_tlimitproductinfo  a " +
                //        " where generatelsa_gid='" + generatelsa_gid + "'";
                //values.total_limitreleased = objdbconn.GetExecuteScalar(msSQL);

                //msSQL = " select format(sum(existing_limit),2,'en_IN') from ocs_trn_tlimitproductinfo  a " +
                //       " where generatelsa_gid='" + generatelsa_gid + "'";
                //values.total_existinglimit = objdbconn.GetExecuteScalar(msSQL);
                values.status = true;
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = ex.ToString();
            }
        }

        public void DaGetApp2SanctionLimitInfoDtl(limitandproductslist values, string application_gid, string application2sanction_gid)
        {
            try
            {
                msSQL = " select limitproductinfodtl_gid,interchangeability,report_structure_gid,product_type,productsub_type, " +
                       " report_structure,format(odlim_amount,2,'en_IN') as odlim_amount,odlim_condition,format(existing_limit,2,'en_IN') as existing_limit , " +
                       " format(limit_released,2,'en_IN') as limit_released,format(documented_limit,2,'en_IN') as documented_limit, " +
                       " date_format(dateof_Expiry,'%d-%m-%Y') as dateof_Expiry, " +
                       " limitinfo_remarks,created_by,created_date from ocs_trn_tlimitproductinfo " +
                       " where application_gid='" + application_gid + "' and application2sanction_gid='" + application2sanction_gid + "'" +
                       " group by(limitproductinfodtl_gid) ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getlimit_info = new List<limitandproducts>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getlimit_info.Add(new limitandproducts
                        {
                            limitproductinfodtl_gid = dr_datarow["limitproductinfodtl_gid"].ToString(),
                            existing_limit = (dr_datarow["existing_limit"].ToString()),
                            limit_released = (dr_datarow["limit_released"].ToString()),
                            interchangeability = (dr_datarow["interchangeability"].ToString()),
                            report_structuregid = (dr_datarow["report_structure_gid"].ToString()),
                            report_structure = (dr_datarow["report_structure"].ToString()),
                            odlim_amount = (dr_datarow["odlim_amount"].ToString()),
                            odlim_condition = (dr_datarow["odlim_condition"].ToString()),
                            limitinfo_remarks = (dr_datarow["limitinfo_remarks"].ToString()),
                            product_type = (dr_datarow["product_type"].ToString()),
                            productsub_type = (dr_datarow["productsub_type"].ToString()),
                            documented_limit = (dr_datarow["documented_limit"].ToString()),
                            dateof_Expiry = (dr_datarow["dateof_Expiry"].ToString()),
                        });
                    }
                    values.limitandproducts = getlimit_info;
                    dt_datatable.Dispose();
                }

                //msSQL = " select format(sum(limit_released),2,'en_IN') from ocs_trn_tlimitproductinfo  a " +
                //        " where generatelsa_gid='" + generatelsa_gid + "'";
                //values.total_limitreleased = objdbconn.GetExecuteScalar(msSQL);

                //msSQL = " select format(sum(existing_limit),2,'en_IN') from ocs_trn_tlimitproductinfo  a " +
                //       " where generatelsa_gid='" + generatelsa_gid + "'";
                //values.total_existinglimit = objdbconn.GetExecuteScalar(msSQL);
                values.status = true;
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = ex.ToString();
            }
        }

        public void DaGetAppLimitInfoDtl(limitandproductslist values, string application_gid)
        {
            try
            {
                msSQL = " select limitproductinfodtl_gid,interchangeability,report_structure_gid,product_type,productsub_type, " +
                       " report_structure,format(odlim_amount,2,'en_IN') as odlim_amount,odlim_condition,format(existing_limit,2,'en_IN') as existing_limit , " +
                       " format(limit_released,2,'en_IN') as limit_released,format(documented_limit,2,'en_IN') as documented_limit, " +
                       " date_format(dateof_Expiry,'%d-%m-%Y') as dateof_Expiry, " +
                       " limitinfo_remarks,created_by,created_date from ocs_trn_tlimitproductinfo " +
                       " where application_gid='" + application_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getlimit_info = new List<limitandproducts>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getlimit_info.Add(new limitandproducts
                        {
                            limitproductinfodtl_gid = dr_datarow["limitproductinfodtl_gid"].ToString(),
                            existing_limit = (dr_datarow["existing_limit"].ToString()),
                            limit_released = (dr_datarow["limit_released"].ToString()),
                            interchangeability = (dr_datarow["interchangeability"].ToString()),
                            report_structuregid = (dr_datarow["report_structure_gid"].ToString()),
                            report_structure = (dr_datarow["report_structure"].ToString()),
                            odlim_amount = (dr_datarow["odlim_amount"].ToString()),
                            odlim_condition = (dr_datarow["odlim_condition"].ToString()),
                            limitinfo_remarks = (dr_datarow["limitinfo_remarks"].ToString()),
                            product_type = (dr_datarow["product_type"].ToString()),
                            productsub_type = (dr_datarow["productsub_type"].ToString()),
                            documented_limit = (dr_datarow["documented_limit"].ToString()),
                            dateof_Expiry = (dr_datarow["dateof_Expiry"].ToString()),
                        });
                    }
                    values.limitandproducts = getlimit_info;
                    dt_datatable.Dispose();
                }

                //msSQL = " select format(sum(limit_released),2,'en_IN') from ocs_trn_tlimitproductinfo  a " +
                //        " where generatelsa_gid='" + generatelsa_gid + "'";
                //values.total_limitreleased = objdbconn.GetExecuteScalar(msSQL);

                //msSQL = " select format(sum(existing_limit),2,'en_IN') from ocs_trn_tlimitproductinfo  a " +
                //       " where generatelsa_gid='" + generatelsa_gid + "'";
                //values.total_existinglimit = objdbconn.GetExecuteScalar(msSQL);
                values.status = true;
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = ex.ToString();
            }
        }

        public bool DaSanctionUpdatelimitproduct(limitandproducts values, string employee_gid)
        {

            //msSQL = " select interchangeability,report_structure_gid,report_structure,odlim_condition, " +
            //        " documented_limit,dateof_Expiry,updated_by,updated_date from  ocs_trn_tlimitproductinfo " +
            //        " where limitproductinfodtl_gid ='" + values.limitproductinfodtl_gid + "'";
            //objODBCDataReader = objdbconn.GetDataReader(msSQL);
            //if (objODBCDataReader.HasRows == true)
            //{
            //    interchangeability = objODBCDataReader["interchangeability"].ToString();
            //    report_structure_gid = objODBCDataReader["report_structure_gid"].ToString();
            //    report_structure = objODBCDataReader["report_structure"].ToString();
            //    odlim_condition = objODBCDataReader["odlim_condition"].ToString();
            //    documented_limit = objODBCDataReader["documented_limit"].ToString();
            //    dateof_Expiry = objODBCDataReader["dateof_Expiry"].ToString();
            //    updated_by = objODBCDataReader["updated_by"].ToString();
            //    updated_date = objODBCDataReader["updated_date"].ToString();
            //}
            //objODBCDataReader.Close();
            msSQL = " Insert into ocs_trn_tlimitproductinfolog (product_type,productsub_type,odlim_amount,application2sanction_gid,limitproductinfodtl_gid,interchangeability,report_structure_gid,report_structure," +
                    " odlim_condition,documented_limit,dateof_Expiry,updated_by,updated_date,created_date)" +
                    " select product_type,productsub_type,odlim_amount,application2sanction_gid,limitproductinfodtl_gid,interchangeability,report_structure_gid,report_structure,odlim_condition, " +
                    " documented_limit,dateof_Expiry,updated_by,updated_date,@created_date := '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    " from  ocs_trn_tlimitproductinfo " +
                    " where limitproductinfodtl_gid ='" + values.limitproductinfodtl_gid + "'";
            mnResultlimitproductinfolog = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " update ocs_trn_tlimitproductinfo set " +
                    " interchangeability ='" + values.interchangeability + "'," +
                    " report_structure_gid ='" + values.report_structuregid + "', " +
                    " report_structure ='" + values.report_structure.Trim() + "'," +
                    " odlim_condition ='" + values.odlim_condition + "'," +
                    " documented_limit ='" + values.documented_limit.Replace(",", "") + "'," +
                    " dateof_Expiry ='" + Convert.ToDateTime(values.dateof_Expiry).ToString("yyyy-MM-dd") + "'," +
                    " updated_by='" + employee_gid + "'," +
                    " updated_date=current_timestamp " +
                    " where limitproductinfodtl_gid ='" + values.limitproductinfodtl_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                values.status = true;
                values.message = "Product Details are updated Successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
            return true;
        }

        public void DAGetAppSanctionSummary(string application_gid, MdlMstCAD values, string employee_gid)
        {
            msSQL = " select a.application2sanction_gid, a.sanction_refno,a.submitedtoapproval_status, date_format(a.sanction_date,'%d-%m-%Y') as sanction_date, a.application_name," +
                    " ccapproved_date, b.application_no, b.application_gid, sanctionto_name," +
                    " date_format(a.created_date,'%d-%m-%Y %H:%i %p') as created_date, sanction_status,checkerapproval_flag" +
                    " from ocs_trn_tapplication2sanction a " +
                    " left join ocs_trn_tcadapplication b on b.application_gid = a.application_gid" +
                    " where a.application_gid='" + application_gid + "'  ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getappsanction_list = new List<appsanction_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                string lsemployeegid = "";
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msSQL = " select sanctionacceptlog_gid from ocs_trn_tsanctionacceptlog  " +
                   " where  (updated_date = (select max(y.updated_date) from ocs_trn_tsanctionacceptlog y where y.application_gid = '" + application_gid + "' ) " +
                   " or  (updated_date is null ) )  and accepted_status ='N' ";
                    string sanctionacceptlog_gid = objdbconn.GetExecuteScalar(msSQL);
                    if (sanctionacceptlog_gid != "")
                    {
                        edit_flag = "Y";

                    }
                    else
                    {
                        edit_flag = "N";
                        msSQL = " select sanctionacceptlog_gid from ocs_trn_tsanctionacceptlog  " +
                                " where application_gid = '" + application_gid + "' and  (updated_date is not null )   ";
                        string sanctionacceptemptylog_gid = objdbconn.GetExecuteScalar(msSQL);
                        if (sanctionacceptemptylog_gid == "")
                        {
                            edit_flag = "";
                        }
                    }
                    lsemployeegid = "'" + employee_gid + "'";

                    getappsanction_list.Add(new appsanction_list
                    {
                        application2sanction_gid = dt["application2sanction_gid"].ToString(),
                        sanction_refno = dt["sanction_refno"].ToString(),
                        sanction_date = dt["sanction_date"].ToString(),
                        application_name = dt["application_name"].ToString(),
                        ccapproved_date = dt["ccapproved_date"].ToString(),
                        application_no = dt["application_no"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        sanctionto_name = dt["sanctionto_name"].ToString(),
                        sanction_status = dt["sanction_status"].ToString(),
                        checkerapproval_flag = dt["checkerapproval_flag"].ToString(),
                        submitedtoapproval_status = dt["submitedtoapproval_status"].ToString(),

                        edit_flag = edit_flag,
                        lsemployeegid = lsemployeegid

                    });
                    values.appsanction_list = getappsanction_list;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaCADSanctionDtls(string sanction_gid, cadsanctiondetails values)
        {
            try
            {
                msSQL = " SELECT a.entity,a.application2sanction_gid,b.application_gid,a.sanction_refno,sanction_date,a.state,a.batch_status, " +
                "  format((sanction_amount), 2) as sanction_amount,a.sanctionto_name, date_format(sanction_date,'%d-%m-%Y') as sanctionDate," +
                 " a.esdeclaration_status,a.makerfile_path,a.makerfile_name, checkerletter_flag, checkerapproval_flag FROM ocs_trn_tapplication2sanction a " +
                 " LEFT JOIN ocs_mst_tapplication b ON a.application_gid = b.application_gid " +
                 " WHERE application2sanction_gid ='" + sanction_gid + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows)
                {
                    values.sanction_refno = objODBCDataReader["sanction_refno"].ToString();
                    values.batch_status = objODBCDataReader["batch_status"].ToString();
                    values.application2sanction_gid = objODBCDataReader["application2sanction_gid"].ToString();
                    values.sanction_amount = objODBCDataReader["sanction_amount"].ToString();
                    values.sanction_date = objODBCDataReader["sanctionDate"].ToString();
                    values.sanctionto_name = objODBCDataReader["sanctionto_name"].ToString();

                    if (objODBCDataReader["sanction_date"].ToString() != "")
                    {
                        values.sanctionDate = Convert.ToDateTime(objODBCDataReader["sanction_date"].ToString());
                    }
                }
                objODBCDataReader.Close();


                //msSQL = " select generaldocument_gid,document_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,document_path,document_type, " +
                //     " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as uploaded_by" +
                //     " from ocs_trn_tuploadgeneraldocument a,hrm_mst_temployee b, adm_mst_tuser c where a.created_by=b.employee_gid" +
                //     " and b.user_gid = c.user_gid and application2sanction_gid='" + sanction_gid + "'";

                //dt_datatable = objdbconn.GetDataTable(msSQL);
                //var getfilename = new List<UploadgeneralDocumentList>();
                //if (dt_datatable.Rows.Count != 0)
                //{
                //    foreach (DataRow dr_datarow in dt_datatable.Rows)
                //    {
                //        getfilename.Add(new UploadgeneralDocumentList
                //        {
                //            document_name = (dr_datarow["document_name"].ToString()),
                //            document_gid = (dr_datarow["generaldocument_gid"].ToString()),
                //            document_type = dr_datarow["document_type"].ToString(),
                //            uploaded_by = dr_datarow["uploaded_by"].ToString(),
                //            updated_date = dr_datarow["uploaded_date"].ToString(),
                //            document_path = (dr_datarow["document_path"].ToString())
                //        });
                //    }
                //    values.UploadgeneralDocumentList = getfilename;
                //}
                //dt_datatable.Dispose();

                //msSQL = " select momdocument_gid,document_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,document_type,document_path, " +
                // " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as uploaded_by" +
                // " from ocs_trn_tuploadmomdocument a,hrm_mst_temployee b, adm_mst_tuser c where a.created_by=b.employee_gid" +
                // " and b.user_gid = c.user_gid and application2sanction_gid='" + sanction_gid + "'";

                //dt_datatable = objdbconn.GetDataTable(msSQL);
                //var getMOM_filename = new List<UploadMOMDocumentList>();
                //if (dt_datatable.Rows.Count != 0)
                //{
                //    foreach (DataRow dr_datarow in dt_datatable.Rows)
                //    {
                //        getMOM_filename.Add(new UploadMOMDocumentList
                //        {
                //            document_path = (dr_datarow["document_path"].ToString()),
                //            document_name = (dr_datarow["document_name"].ToString()),
                //            document_type = dr_datarow["document_type"].ToString(),
                //            document_gid = (dr_datarow["momdocument_gid"].ToString()),
                //            uploaded_by = dr_datarow["uploaded_by"].ToString(),
                //            updated_date = dr_datarow["uploaded_date"].ToString()
                //        });
                //    }
                //    values.UploadMOMDocumentList = getMOM_filename;
                //}
                //dt_datatable.Dispose();

                //msSQL = " select camdocument_gid,document_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,document_type,document_path, " +
                //  " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as uploaded_by" +
                //  " from ocs_trn_tuploadcamdocument a,hrm_mst_temployee b, adm_mst_tuser c where a.created_by=b.employee_gid" +
                //  " and b.user_gid = c.user_gid and application2sanction_gid='" + sanction_gid + "'";

                //dt_datatable = objdbconn.GetDataTable(msSQL);
                //var getCAM_filename = new List<UploadCOMDocumentList>();
                //if (dt_datatable.Rows.Count != 0)
                //{
                //    foreach (DataRow dr_datarow in dt_datatable.Rows)
                //    {
                //        getCAM_filename.Add(new UploadCOMDocumentList
                //        {
                //            document_path = (dr_datarow["document_path"].ToString()),
                //            document_name = (dr_datarow["document_name"].ToString()),
                //            document_type = dr_datarow["document_type"].ToString(),
                //            document_gid = (dr_datarow["camdocument_gid"].ToString()),
                //            uploaded_by = dr_datarow["uploaded_by"].ToString(),
                //            updated_date = dr_datarow["uploaded_date"].ToString()
                //        });
                //    }
                //    values.UploadCOMDocumentList = getCAM_filename;
                //}
                //dt_datatable.Dispose();

                //msSQL = " select esdeclaration_gid,document_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,document_path,document_type, " +
                //    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as uploaded_by" +
                //    " from ocs_trn_tuploadesdeclarationdocument a,hrm_mst_temployee b, adm_mst_tuser c where a.created_by=b.employee_gid" +
                //    " and b.user_gid = c.user_gid and application2sanction_gid='" + sanction_gid + "'";

                //dt_datatable = objdbconn.GetDataTable(msSQL);
                //var get_esdeclarationfilename = new List<UploadES_DocumentList>();
                //if (dt_datatable.Rows.Count != 0)
                //{
                //    foreach (DataRow dr_datarow in dt_datatable.Rows)
                //    {
                //        get_esdeclarationfilename.Add(new UploadES_DocumentList
                //        {
                //            document_path = (dr_datarow["document_path"].ToString()),
                //            document_name = (dr_datarow["document_name"].ToString()),
                //            document_gid = (dr_datarow["esdeclaration_gid"].ToString()),
                //            document_type = dr_datarow["document_type"].ToString(),
                //            uploaded_by = dr_datarow["uploaded_by"].ToString(),
                //            updated_date = dr_datarow["uploaded_date"].ToString()
                //        });
                //    }
                //    values.UploadES_DocumentList = get_esdeclarationfilename;
                //}
                //dt_datatable.Dispose();

                //msSQL = " select maildocument_gid,document_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,document_path,document_type, " +
                //    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as uploaded_by" +
                //    " from ocs_trn_tdeviationmaildocument a,hrm_mst_temployee b, adm_mst_tuser c where a.created_by=b.employee_gid" +
                //    " and b.user_gid = c.user_gid and application2sanction_gid='" + sanction_gid + "'";

                //dt_datatable = objdbconn.GetDataTable(msSQL);
                //var get_mailfilename = new List<DeviationMail_DocumentList>();
                //if (dt_datatable.Rows.Count != 0)
                //{
                //    foreach (DataRow dr_datarow in dt_datatable.Rows)
                //    {
                //        get_mailfilename.Add(new DeviationMail_DocumentList
                //        {
                //            document_path = (dr_datarow["document_path"].ToString()),
                //            document_name = (dr_datarow["document_name"].ToString()),
                //            document_gid = (dr_datarow["maildocument_gid"].ToString()),
                //            document_type = dr_datarow["document_type"].ToString(),
                //            uploaded_by = dr_datarow["uploaded_by"].ToString(),
                //            updated_date = dr_datarow["uploaded_date"].ToString()
                //        });
                //    }
                //    values.DeviationMail_DocumentList = get_mailfilename;
                //}
                //dt_datatable.Dispose();

                //msSQL = " select  addbuyer_gid,if (document_name is null,'---',document_name) as document_name,baldocument_gid," +
                //    " concat(date_format(a.created_date, '%d-%m-%Y %H:%i %p')) as uploaded_date,document_path,buyer_name," +
                //    " buyer_exposure,concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as uploaded_by from ocs_trn_taddbuyer a" +
                //    " left join ocs_trn_tuploadbaldocument d on a.addbuyer_gid = d.buyer_gid" +
                //     " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                //     " left  join adm_mst_tuser c on b.user_gid = c.user_gid where" +
                //     " application2sanction_gid ='" + sanction_gid + "'";

                //dt_datatable = objdbconn.GetDataTable(msSQL);
                //var get_filename = new List<buyer_list>();
                //if (dt_datatable.Rows.Count != 0)
                //{
                //    foreach (DataRow dr_datarow in dt_datatable.Rows)
                //    {
                //        get_filename.Add(new buyer_list
                //        {
                //            document_name = (dr_datarow["document_name"].ToString()),
                //            document_path = ((dr_datarow["document_path"].ToString())),
                //            buyer_gid = (dr_datarow["addbuyer_gid"].ToString()),
                //            buyer_name = dr_datarow["buyer_name"].ToString(),
                //            uploaded_by = dr_datarow["uploaded_by"].ToString(),
                //            uploaded_date = dr_datarow["uploaded_date"].ToString(),
                //            buyer_exposure = dr_datarow["buyer_exposure"].ToString(),
                //            baldocument_gid = dr_datarow["baldocument_gid"].ToString()
                //        });
                //    }
                //    values.buyer_list = get_filename;
                //}
                //dt_datatable.Dispose();

                //msSQL = "select ccmemberlist_gid,ccmember_name,ccmember_gid,ccmember_remarks,ccgroup_name from ocs_mst_tsanction2ccmemberlist " +
                //" where application2sanction_gid='" + sanction_gid + "'";
                //dt_datatable = objdbconn.GetDataTable(msSQL);
                //var get_mdlccmember = new List<mdlccmember>();
                //if (dt_datatable.Rows.Count != 0)
                //{
                //    foreach (DataRow dr_datarow in dt_datatable.Rows)
                //    {
                //        get_mdlccmember.Add(new mdlccmember
                //        {
                //            ccmemberlist_gid = (dr_datarow["ccmemberlist_gid"].ToString()),
                //            ccmember_name = (dr_datarow["ccmember_name"].ToString()),
                //            ccmember_gid = (dr_datarow["ccmember_gid"].ToString()),
                //            ccmember_remarks = (dr_datarow["ccmember_remarks"].ToString()),
                //            ccgroup_name = (dr_datarow["ccgroup_name"].ToString()),
                //        });
                //    }
                //    values.mdlccmember = get_mdlccmember;
                //}
                //dt_datatable.Dispose();
                //msSQL = "select sanction2loanfacilitytype_gid,loanfacility_gid,format(loanfacility_amount,2) as loanfacility_amount,loanfacility_type, " +
                //" format(document_limit,2) as document_limit,margin,date_format(expiry_date, '%d-%m-%Y') as expiry_date,revolving_type,tenure," +
                //" interchangeability,if(report_structure='','---',report_structure) as report_structure,loanfacilityref_no,proposed_roi" +
                //" from ocs_trn_tsanction2loanfacilitytype  where application2sanction_gid='" + sanction_gid + "'";
                //dt_datatable = objdbconn.GetDataTable(msSQL);
                //var getloanfacilitytype = new List<loanfacilitytype_list>();
                //if (dt_datatable.Rows.Count != 0)
                //{
                //    foreach (DataRow dr_datarow in dt_datatable.Rows)
                //    {
                //        getloanfacilitytype.Add(new loanfacilitytype_list
                //        {
                //            sanction2loanfacilitytype_gid = (dr_datarow["sanction2loanfacilitytype_gid"].ToString()),
                //            loanfacility_gid = (dr_datarow["loanfacility_gid"].ToString()),
                //            loanfacility_amount = (dr_datarow["loanfacility_amount"].ToString()),
                //            loanfacility_type = (dr_datarow["loanfacility_type"].ToString()),
                //            document_limit = (dr_datarow["document_limit"].ToString()),
                //            expiry_date = (dr_datarow["expiry_date"].ToString()),
                //            revolving_type = (dr_datarow["revolving_type"].ToString()),
                //            tenure = (dr_datarow["tenure"].ToString()),
                //            margin = (dr_datarow["margin"].ToString()),
                //            interchangeability = (dr_datarow["interchangeability"].ToString()),
                //            report_structure = (dr_datarow["report_structure"].ToString()),
                //            loanfacilityref_no = (dr_datarow["loanfacilityref_no"].ToString()),
                //            proposed_roi = (dr_datarow["proposed_roi"].ToString()),
                //        });
                //    }
                //    values.loanfacilitytype_list = getloanfacilitytype;
                //}
                //dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = ex.ToString();
                values.status = false;
            }

        }

        public bool DaGetCADTemplateDetails(mdltemplate values, string sanction_gid)
        {
            values.mstcontent_flag = "N";
            msSQL = " select application_gid, sanctionletter_status,template_gid, template_name, template_content, makerfile_name, makerfile_path, sanctionletter_flag, checkerapproval_flag," +
                    " checkerletter_flag, checkerpushback_remarks, digitalsignature_flag, date_format(checkerupdated_on, '%d-%m-%Y') as checkerupdated_on," +
                    " concat(c.user_firstname, ' ', c.user_lastname, '/', c.user_code) as checkerupdated_by, date_format(makersubmitted_on, '%d-%m-%Y') as makersubmitted_on," +
                    " concat(e.user_firstname, ' ', e.user_lastname, '/', e.user_code) as makersubmitted_by,defaulttemplate_content " +
                    " from ocs_trn_tapplication2sanction a " +
                    " left join hrm_mst_temployee b on b.employee_gid = a.checkerupdated_by " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " left join hrm_mst_temployee d on d.employee_gid = a.makersubmitted_by " +
                    " left join adm_mst_tuser e on e.user_gid = d.user_gid " +
                    " where application2sanction_gid='" + sanction_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                values.sanctionletter_status = objODBCDataReader["sanctionletter_status"].ToString();
                values.template_name = objODBCDataReader["template_name"].ToString();
                values.template_content = objODBCDataReader["template_content"].ToString();
                values.makerfile_name = objODBCDataReader["makerfile_name"].ToString();
                values.makerfile_path = objcmnstorage.EncryptData((objODBCDataReader["makerfile_path"].ToString()));
                values.sanctionletter_flag = objODBCDataReader["sanctionletter_flag"].ToString();
                values.checkerapproval_flag = objODBCDataReader["checkerapproval_flag"].ToString();
                values.checkerletter_flag = objODBCDataReader["checkerletter_flag"].ToString();
                values.checkerpushback_remarks = objODBCDataReader["checkerpushback_remarks"].ToString();
                values.digitalsignature_flag = objODBCDataReader["digitalsignature_flag"].ToString();
                values.checkerupdated_by = objODBCDataReader["checkerupdated_by"].ToString();
                values.checkerupdated_on = objODBCDataReader["checkerupdated_on"].ToString();
                values.makersubmitted_by = objODBCDataReader["makersubmitted_by"].ToString();
                values.makersubmitted_on = objODBCDataReader["makersubmitted_on"].ToString();
                values.template_gid = objODBCDataReader["template_gid"].ToString();
                values.defaulttemplate_content = objODBCDataReader["defaulttemplate_content"].ToString();
                application_gid = objODBCDataReader["application_gid"].ToString();
            }
            objODBCDataReader.Close();
            if (values.template_name == "" || values.template_name == null)
            {
                values.mstcontent_flag = "Y";
                msSQL = " select template_gid,template_name,template_content from ocs_mst_ttemplate a " +
                       " left join ocs_trn_tcadapplication b on a.vertical_gid = b.vertical_gid and a.program_gid = b.program_gid " +
                       " where b.application_gid = '" + application_gid + "' and a.template_type='" + getTemplateClass.Sanction + "'" +
                       " and a.template_status='Y'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var objMdlTemplatedtl = new List<MdlTemplatedtl>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        objMdlTemplatedtl.Add(new MdlTemplatedtl
                        {
                            template_gid = dt["template_gid"].ToString(),
                            template_name = dt["template_name"].ToString(),
                            template_content = dt["template_content"].ToString(),
                        });
                    }
                }
                values.MdlTemplatedtl = objMdlTemplatedtl;
            }

            values.status = true;
            return true;
        }

        public bool DaCADSanctionLetterSummary(string sanction_gid, sanctiondetailsList values)
        {
            msSQL = " SELECT a.sanctionapprovallog_gid, a.sanction_gid, a.sanction_status, concat(c.user_firstname, c.user_lastname, ' / ', c.user_code) as created_by, " +
                   " date_format(a.created_date, '%d-%m-%Y %H:%i %p') as created_date, checkerpushback_remarks" +
                   " FROM ocs_trn_tsanctionapprovallog a " +
                   " LEFT JOIN hrm_mst_temployee b ON a.created_by=b.employee_gid" +
                   " LEFT JOIN adm_mst_tuser c ON c.user_gid=b.user_gid where sanction_gid= '" + sanction_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_sanctiondtl = new List<sanctiondetails>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    get_sanctiondtl.Add(new sanctiondetails
                    {
                        sanctionapprovallog_gid = dt["sanctionapprovallog_gid"].ToString(),
                        application2sanction_gid = dt["sanction_gid"].ToString(),
                        sanction_status = dt["sanction_status"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        remarks = dt["checkerpushback_remarks"].ToString(),
                    });
                }
                values.sanctiondetails = get_sanctiondtl;
            }
            dt_datatable.Dispose();

            return true;
        }



        public bool DaSanctionContent(cadtemplate_list values)
        {
            //msSQL = " update ocs_trn_tapplication2sanction set template_name='" + values.template_name + "', template_gid='" + values.template_gid + "' where application2sanction_gid='" + values.sanction_gid + "'";
            //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            //Get Template Content 
            msSQL = " select  a.template_content from adm_mst_ttemplate a where a.template_gid='" + values.template_gid + "'";
            lstemplate_content = objdbconn.GetExecuteScalar(msSQL);

            lscontent = lstemplate_content;
            string lsapplication_no = "", lssanction_date = "", lsapplication_gid = "", lssanctionto_name = "";
            msSQL = " select a.sanction_refno, a.application_name, a.sanctionto_name, a.ccapproved_date, a.validity_months,b.application_no, date_format(a.sanction_date, '%d-%m-%Y') as sanction_date, " +
                    " a.contactperson_name, a.contactperson_number, a.contactpersonemail_address, a.contactperson_address, a.purpose_lending, b.relationshipmanager_gid," +
                    " b.relationshipmanager_name, c.employee_mobileno,a.application_gid, c.employee_emailid" +
                    " from ocs_trn_tapplication2sanction a " +
                    " LEFT JOIN ocs_mst_tapplication b on a.application_gid = b.application_gid " +
                    " LEFT JOIN hrm_mst_temployee c ON c.employee_gid = b.relationshipmanager_gid " +
                    " where application2sanction_gid='" + values.sanction_gid + "'";

            objODBCDataReader1 = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader1.HasRows == true)
            {
                values.sanction_refno = objODBCDataReader1["sanction_refno"].ToString();
                lsapplication_gid = objODBCDataReader1["application_gid"].ToString();
                values.application_name = objODBCDataReader1["application_name"].ToString();
                lsapplication_no = objODBCDataReader1["application_no"].ToString();
                lssanction_date = objODBCDataReader1["sanction_date"].ToString();
                values.ccapproved_date = objODBCDataReader1["ccapproved_date"].ToString();
                values.address = objODBCDataReader1["contactperson_address"].ToString();
                values.mobileno = objODBCDataReader1["contactperson_number"].ToString();
                values.email = objODBCDataReader1["contactpersonemail_address"].ToString();
                values.contactperson = objODBCDataReader1["contactperson_name"].ToString();
                values.purpose_lending = objODBCDataReader1["purpose_lending"].ToString();
                values.validity_months = objODBCDataReader1["validity_months"].ToString();
                values.relationshipmgmt_name = objODBCDataReader1["relationshipmanager_name"].ToString();
                values.employee_mobileno = objODBCDataReader1["employee_mobileno"].ToString();
                values.employee_mailid = objODBCDataReader1["employee_emailid"].ToString();
                lssanctionto_name = objODBCDataReader1["sanctionto_name"].ToString();
            }
            objODBCDataReader1.Close();
            lscontent = lscontent.Replace("sanctionto_name", lssanctionto_name);
            lscontent = lscontent.Replace("contact_person", values.contactperson);
            lscontent = lscontent.Replace("mobile_no", values.mobileno);
            lscontent = lscontent.Replace("applicatication_refno", lsapplication_no);
            lscontent = lscontent.Replace("sanction_date", lssanction_date);
            lscontent = lscontent.Replace("ToContactaddress", values.address);
            lscontent = lscontent.Replace("ccapproved_date", values.ccapproved_date);
            lscontent = lscontent.Replace("email", values.email);
            lscontent = lscontent.Replace("application_name", values.application_name);
            lscontent = lscontent.Replace("sanction_refno", values.sanction_refno);
            lscontent = lscontent.Replace("validity_months", values.validity_months);
            lscontent = lscontent.Replace("relationshipmgmt_name", values.relationshipmgmt_name);
            lscontent = lscontent.Replace("employee_mobileno", values.employee_mobileno);
            lscontent = lscontent.Replace("employee_mailid", values.employee_mailid);

            //msSQL = "select sanction2loanfacilitytype_gid,loanfacility_gid,format(loanfacility_amount,2) as loanfacility_amount,loanfacility_type, " +
            //   " format(document_limit,2) as document_limit,margin,date_format(expiry_date, '%d-%m-%Y') as expiry_date,revolving_type,tenure, " +
            //   " interchangeability,loanfacilityref_no,SUBSTRING_INDEX(proposed_roi, '%', 1) proposed_roi" +
            //   " from ocs_trn_tsanction2loanfacilitytype  where application2sanction_gid='" + values.sanction_gid + "'";
            msSQL = " select concat(product_type,'-', productsub_type) as product_type,format(loanfacility_amount,2) as facility_limit, " +
                  " enduse_purpose as purposeofloan, facility_mode ,rate_interest as margin,tenureoverall_limit " +
                  " from ocs_mst_tapplication2loan where application_gid = '" + lsapplication_gid + "'";
            objODBCDataReader2 = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader2.HasRows == true)
            {
                values.loanfacility_type = objODBCDataReader2["product_type"].ToString();
                values.loanfacility_amount = objODBCDataReader2["facility_limit"].ToString();
                values.revolving_type = objODBCDataReader2["facility_mode"].ToString();
                values.tenure = objODBCDataReader2["tenureoverall_limit"].ToString();
                values.purpose_lending = objODBCDataReader2["purposeofloan"].ToString();
                values.margin = objODBCDataReader2["margin"].ToString();
            }
            objODBCDataReader2.Close();
            //double proposed_roi = Convert.ToDouble(values.proposed_roi);
            double loanfacility_amount = Convert.ToDouble(values.loanfacility_amount);


            //double interest_amount = (loanfacility_amount * (proposed_roi / 100));
            //int interestamount = Convert.ToInt32(interest_amount);
            //values.interest_amount = Convert.ToString(interest_amount);

            double addoncharge = (loanfacility_amount * 1 / 100);
            //int addon_charge = Convert.ToInt32(addoncharge);
            //values.interest_amount = Convert.ToString(interest_amount);
            //values.addoncharge = Convert.ToString(addoncharge);

            int facilityamount = Convert.ToInt32(loanfacility_amount);

            //string interest_words = NumberToWords(interestamount);
            string facilityamount_words = NumberToWords(facilityamount);
            //string addonwords = NumberToWords(addon_charge);

            lscontent = lscontent.Replace("loanfacility_type", values.loanfacility_type);
            lscontent = lscontent.Replace("loanfacility_amount", values.loanfacility_amount);
            lscontent = lscontent.Replace("facilityamount_words", facilityamount_words);
            lscontent = lscontent.Replace("tenure", values.tenure);
            lscontent = lscontent.Replace("purpose_lending", values.purpose_lending);
            lscontent = lscontent.Replace("revolving_type", values.revolving_type);
            lscontent = lscontent.Replace("margin", values.margin);
            //lscontent = lscontent.Replace("interest_amount", values.interest_amount);

            //lscontent = lscontent.Replace("addoncharge", values.addoncharge);
            //lscontent = lscontent.Replace("addonwords", addonwords);
            //msSQL = " select  a.template_content from adm_mst_ttemplate a where a.template_gid='" + values.template_gid + "'";
            //lstemplate_content = objdbconn.GetExecuteScalar(msSQL);
            //lscontent = lstemplate_content;

            values.template_content = lscontent;
            values.status = true;
            return true;

        }

        public bool DaPostTemplateSanction2Facility(cadtemplate_list values)
        {
            msSQL = " update ocs_trn_tapplication2sanction set template_name='" + values.template_name + "', template_gid='" + values.template_gid + "' where application2sanction_gid='" + values.sanction_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            //Get Template Content
            msSQL = " select  a.template_content from adm_mst_ttemplate a " +
              " where a.template_gid='" + values.template_gid + "'";
            lstemplate_content = objdbconn.GetExecuteScalar(msSQL);

            lscontent = lstemplate_content;

            msSQL = " select a.sanction_refno, a.application_name, a.ccapproved_date, a.validity_months, template_name," +
            " a.contactperson_name, a.contactperson_number, a.contactpersonemail_address, a.contactperson_address, a.purpose_lending, b.relationshipmanager_gid," +
            " b.relationshipmanager_name, c.employee_mobileno, c.employee_emailid, address" +
            " from ocs_trn_tapplication2sanction a " +
            " LEFT JOIN ocs_mst_tapplication b ON a.application_gid = b.application_gid " +
            " LEFT JOIN hrm_mst_temployee c ON c.employee_gid = b.relationshipmanager_gid " +
            " where application2sanction_gid='" + values.sanction_gid + "'";

            objODBCDataReader1 = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader1.HasRows == true)
            {
                values.sanction_refno = objODBCDataReader1["sanction_refno"].ToString();
                values.application_name = objODBCDataReader1["application_name"].ToString();
                values.ccapproved_date = objODBCDataReader1["ccapproved_date"].ToString();
                values.address = objODBCDataReader1["address"].ToString();
                values.mobileno = objODBCDataReader1["mobileno"].ToString();
                values.email = objODBCDataReader1["email"].ToString();
                values.contactperson = objODBCDataReader1["contactperson"].ToString();
                values.purpose_lending = objODBCDataReader1["purpose_lending"].ToString();
                values.validity_months = objODBCDataReader1["validity_months"].ToString();
                values.relationshipmgmt_name = objODBCDataReader1["relationshipmgmt_name"].ToString();
                values.employee_mobileno = objODBCDataReader1["employee_mobileno"].ToString();
                values.employee_mailid = objODBCDataReader1["employee_emailid"].ToString();
            }
            objODBCDataReader1.Close();
            lscontent = lscontent.Replace("contact_person", values.contactperson);
            //lscontent = lscontent.Replace("mobile_no", values.mobileno + ",");
            lscontent = lscontent.Replace("address", values.address);
            lscontent = lscontent.Replace("ccapproved_date", values.ccapproved_date);
            lscontent = lscontent.Replace("email", values.email);
            lscontent = lscontent.Replace("application_name", values.application_name);
            lscontent = lscontent.Replace("sanction_ref_no", values.sanction_refno);
            lscontent = lscontent.Replace("validity_months", values.validity_months);
            lscontent = lscontent.Replace("relationshipmgmt_name", values.relationshipmgmt_name);
            lscontent = lscontent.Replace("employee_mobileno", values.employee_mobileno);
            lscontent = lscontent.Replace("employee_mailid", values.employee_mailid);

            msSQL = "select sanction2loanfacilitytype_gid,loanfacility_gid,format(loanfacility_amount,2) as loanfacility_amount,loanfacility_type, " +
               " format(document_limit,2) as document_limit,margin,revolving_type,tenure, SUBSTRING_INDEX(proposed_roi, '%', 1) proposed_roi," +
               " interchangeability" +
               " from ocs_trn_tsanction2loanfacilitytype  where application2sanction_gid='" + values.sanction_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getloanfacilitytype = new List<cadloanfacilitytype_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getloanfacilitytype.Add(new cadloanfacilitytype_list
                    {
                        sanction2loanfacilitytype_gid = (dr_datarow["sanction2loanfacilitytype_gid"].ToString()),
                        loanfacility_gid = (dr_datarow["loanfacility_gid"].ToString()),
                        loanfacility_amount = (dr_datarow["loanfacility_amount"].ToString()),
                        loanfacility_type = (dr_datarow["loanfacility_type"].ToString()),
                        document_limit = (dr_datarow["document_limit"].ToString()),
                        revolving_type = (dr_datarow["revolving_type"].ToString()),
                        tenure = (dr_datarow["tenure"].ToString()),
                        margin = (dr_datarow["margin"].ToString()),
                        interchangeability = (dr_datarow["interchangeability"].ToString()),
                        proposed_roi = (dr_datarow["proposed_roi"].ToString()),

                    });
                }
                values.cadloanfacilitytype_list = getloanfacilitytype;
            }
            dt_datatable.Dispose();

            String proposedroi1 = values.cadloanfacilitytype_list[0].proposed_roi;
            String proposedroi2 = values.cadloanfacilitytype_list[1].proposed_roi;
            String loanfacilityamount1 = values.cadloanfacilitytype_list[0].loanfacility_amount;
            String loanfacilityamount2 = values.cadloanfacilitytype_list[1].loanfacility_amount;

            double proposed_roi1 = Convert.ToDouble(proposedroi1);
            double loanfacility_amount1 = Convert.ToDouble(loanfacilityamount1);
            double loanfacility_amount2 = Convert.ToDouble(loanfacilityamount2);

            double interest_amount = ((loanfacility_amount1 + loanfacility_amount2) * (proposed_roi1 / 100));
            int interestamount = Convert.ToInt32(interest_amount);
            values.interest_amount = Convert.ToString(interest_amount);

            double addoncharge = ((loanfacility_amount1 + loanfacility_amount2) * 1 / 100);
            int addon_charge = Convert.ToInt32(addoncharge);
            values.interest_amount = Convert.ToString(interest_amount);

            int facilityamount1 = Convert.ToInt32(loanfacility_amount1);
            int facilityamount2 = Convert.ToInt32(loanfacility_amount2);

            string interest_words = NumberToWords(interestamount);
            string facilityamount1_words = NumberToWords(facilityamount1);
            string facilityamount2_words = NumberToWords(facilityamount2);

            lscontent = lscontent.Replace("revolving_type1", values.cadloanfacilitytype_list[0].revolving_type);
            lscontent = lscontent.Replace("revolving_type2", values.cadloanfacilitytype_list[1].revolving_type);
            lscontent = lscontent.Replace("tenure1", values.cadloanfacilitytype_list[0].tenure);
            lscontent = lscontent.Replace("tenure2", values.cadloanfacilitytype_list[1].tenure);
            lscontent = lscontent.Replace("loanfacility_type1", values.cadloanfacilitytype_list[0].loanfacility_type);
            lscontent = lscontent.Replace("loanfacility_type2", values.cadloanfacilitytype_list[1].loanfacility_type);
            lscontent = lscontent.Replace("proposed_roi", values.cadloanfacilitytype_list[1].proposed_roi);
            lscontent = lscontent.Replace("loanfacility_amount1", values.cadloanfacilitytype_list[0].loanfacility_amount);
            lscontent = lscontent.Replace("loanfacility_amount2", values.cadloanfacilitytype_list[1].loanfacility_amount);
            lscontent = lscontent.Replace("margin1", values.cadloanfacilitytype_list[0].margin);
            lscontent = lscontent.Replace("margin2", values.cadloanfacilitytype_list[1].margin);
            lscontent = lscontent.Replace("purpose_lending", values.purpose_lending);
            lscontent = lscontent.Replace("interest_amount", values.interest_amount);
            lscontent = lscontent.Replace("facilityamount1_words", facilityamount1_words);
            lscontent = lscontent.Replace("facilityamount2_words", facilityamount2_words);

            values.template_content = lscontent;
            values.status = true;
            return true;

        }

        public bool DaPostTemplateSanctionStandbyLine(cadtemplate_list values)
        {
            msSQL = " update ocs_trn_tapplication2sanction set template_name='" + values.template_name + "', template_gid='" + values.template_gid + "' where application2sanction_gid='" + values.sanction_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            //Get Template Content
            msSQL = " select  a.template_content from adm_mst_ttemplate a where a.template_gid='" + values.template_gid + "'";
            lstemplate_content = objdbconn.GetExecuteScalar(msSQL);

            lscontent = lstemplate_content;

            msSQL = " select a.sanction_refno, a.application_name, a.ccapproved_date, a.validity_months, template_name," +
             " a.contactperson_name, a.contactperson_number, a.contactpersonemail_address, a.contactperson_address, a.purpose_lending, b.relationshipmanager_gid," +
             " b.relationshipmanager_name, c.employee_mobileno, c.employee_emailid" +
             " from ocs_trn_tapplication2sanction a " +
             " LEFT JOIN ocs_mst_tapplication b ON a.application_gid = b.application_gid " +
             " LEFT JOIN hrm_mst_temployee c ON c.employee_gid = b.relationshipmanager_gid " +
             " where application2sanction_gid='" + values.sanction_gid + "'";
            objODBCDataReader1 = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader1.HasRows == true)
            {
                objODBCDataReader1.Read();
                values.sanction_refno = objODBCDataReader1["sanction_refno"].ToString();
                values.application_name = objODBCDataReader1["application_name"].ToString();
                values.ccapproved_date = objODBCDataReader1["ccapproved_date"].ToString();
                values.address = objODBCDataReader1["contactperson_address"].ToString();
                values.mobileno = objODBCDataReader1["contactperson_number"].ToString();
                values.email = objODBCDataReader1["contactpersonemail_address"].ToString();
                values.contactperson = objODBCDataReader1["contactperson_name"].ToString();
                values.purpose_lending = objODBCDataReader1["purpose_lending"].ToString();
                values.validity_months = objODBCDataReader1["validity_months"].ToString();
                values.relationshipmgmt_name = objODBCDataReader1["relationshipmanager_name"].ToString();
                values.employee_mobileno = objODBCDataReader1["employee_mobileno"].ToString();
                values.employee_mailid = objODBCDataReader1["employee_emailid"].ToString();
                values.template_name = objODBCDataReader1["template_name"].ToString();
            }
            objODBCDataReader1.Close();
            lscontent = lscontent.Replace("contact_person", values.contactperson);
            lscontent = lscontent.Replace("mobile_no", values.mobileno + ",");
            lscontent = lscontent.Replace("address", values.address);
            lscontent = lscontent.Replace("ccapproved_date", values.ccapproved_date);
            lscontent = lscontent.Replace("email", values.email);
            lscontent = lscontent.Replace("application_name", values.application_name);
            lscontent = lscontent.Replace("sanction_ref_no", values.sanction_refno);
            lscontent = lscontent.Replace("validity_months", values.validity_months);

            msSQL = "select sanction2loanfacilitytype_gid,loanfacility_gid,format(loanfacility_amount,2) as loanfacility_amount,loanfacility_type, " +
               " format(document_limit,2) as document_limit,margin,date_format(expiry_date, '%d-%m-%Y') as expiry_date,revolving_type,tenure," +
               " interchangeability,if(report_structure='','---',report_structure) as report_structure,loanfacilityref_no,proposed_roi" +
               " from ocs_trn_tsanction2loanfacilitytype  where application2sanction_gid='" + values.sanction_gid + "'";
            objODBCDataReader2 = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader2.HasRows == true)
            {
                values.loanfacility_type = objODBCDataReader2["loanfacility_type"].ToString();
                values.document_limit = objODBCDataReader2["document_limit"].ToString();
                values.revolving_type = objODBCDataReader2["revolving_type"].ToString();
                values.tenure = objODBCDataReader2["tenure"].ToString();
                values.loanfacility_amount = objODBCDataReader2["loanfacility_amount"].ToString();
                values.proposed_roi = objODBCDataReader2["proposed_roi"].ToString();
                values.margin = objODBCDataReader2["margin"].ToString();
            }
            objODBCDataReader2.Close();
            String proposedroi = values.proposed_roi;

            string p1 = proposedroi.Substring(0, 2);

            double proposed_roi = Convert.ToDouble(p1);
            double loanfacility_amount = Convert.ToDouble(values.loanfacility_amount);

            double interest_amount = (loanfacility_amount * (proposed_roi / 100));
            int interestamount = Convert.ToInt32(interest_amount);
            values.interest_amount = Convert.ToString(interest_amount);

            double addoncharge = (loanfacility_amount * (1 / 100));
            int addon_charge = Convert.ToInt32(addoncharge);
            values.interest_amount = Convert.ToString(interest_amount);

            int facilityamount = Convert.ToInt32(loanfacility_amount);

            string interest_words = NumberToWords(interestamount);
            string facilityamount_words = NumberToWords(facilityamount);

            lscontent = lscontent.Replace("revolving_type", values.revolving_type);
            lscontent = lscontent.Replace("tenure", values.tenure);
            lscontent = lscontent.Replace("loanfacility_type", values.loanfacility_type);
            lscontent = lscontent.Replace("proposed_roi", values.proposed_roi);
            lscontent = lscontent.Replace("loanfacility_amount", values.loanfacility_amount);
            lscontent = lscontent.Replace("margin", values.margin);
            lscontent = lscontent.Replace("purpose_lending", values.purpose_lending);
            lscontent = lscontent.Replace("interest_amount", values.interest_amount);
            lscontent = lscontent.Replace("facilityamount_words", values.facilityamount_words);

            values.template_content = lscontent;
            values.status = true;
            return true;
        }

        public bool DaPostTemplateSanctionMultipleFacility(cadtemplate_list values)
        {
            //msSQL = " update ocs_trn_tapplication2sanction set template_name='" + values.template_name + "', template_gid='" + values.template_gid + "' where application2sanction_gid='" + values.sanction_gid + "'";
            //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            //Get Template Content
            msSQL = " select  a.template_content from adm_mst_ttemplate a " +
              " where a.template_gid='" + values.template_gid + "'";
            lstemplate_content = objdbconn.GetExecuteScalar(msSQL);

            lscontent = lstemplate_content;
            string lsapplication_gid = "", lsapplication_no = "", lssanction_date = "", lssanctionto_name = "";
            msSQL = " select a.application_gid, a.sanction_refno, a.application_name, a.ccapproved_date, a.validity_months ,b.application_no, " +
                    " date_format(a.sanction_date, '%d-%m-%Y') as sanction_date,a.sanctionto_name, " +
                    " a.contactperson_name, a.contactperson_number, a.contactpersonemail_address, a.contactperson_address, a.purpose_lending, b.relationshipmanager_gid," +
                    " b.relationshipmanager_name, c.employee_mobileno, c.employee_emailid" +
                    " from ocs_trn_tapplication2sanction a " +
                    " LEFT JOIN ocs_mst_tapplication b on a.application_gid = b.application_gid " +
                    " LEFT JOIN hrm_mst_temployee c ON c.employee_gid = b.relationshipmanager_gid " +
                    " where application2sanction_gid='" + values.sanction_gid + "'";
            objODBCDataReader1 = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader1.HasRows == true)
            {
                objODBCDataReader1.Read();
                values.sanction_refno = objODBCDataReader1["sanction_refno"].ToString();
                values.application_name = objODBCDataReader1["application_name"].ToString();
                lsapplication_gid = objODBCDataReader1["application_gid"].ToString();
                values.ccapproved_date = objODBCDataReader1["ccapproved_date"].ToString();
                values.address = objODBCDataReader1["contactperson_address"].ToString();
                values.mobileno = objODBCDataReader1["contactperson_number"].ToString();
                values.email = objODBCDataReader1["contactpersonemail_address"].ToString();
                values.contactperson = objODBCDataReader1["contactperson_name"].ToString();
                values.purpose_lending = objODBCDataReader1["purpose_lending"].ToString();
                values.validity_months = objODBCDataReader1["validity_months"].ToString();
                values.relationshipmgmt_name = objODBCDataReader1["relationshipmanager_name"].ToString();
                values.employee_mobileno = objODBCDataReader1["employee_mobileno"].ToString();
                values.employee_mailid = objODBCDataReader1["employee_emailid"].ToString();
                lsapplication_no = objODBCDataReader1["application_no"].ToString();
                lssanction_date = objODBCDataReader1["sanction_date"].ToString();
                lssanctionto_name = objODBCDataReader1["sanctionto_name"].ToString();
            }
            objODBCDataReader1.Close();
            lscontent = lscontent.Replace("sanctionto_name", lssanctionto_name);
            lscontent = lscontent.Replace("contact_person", values.contactperson);
            lscontent = lscontent.Replace("mobile_no", values.mobileno + ",");
            lscontent = lscontent.Replace("address", values.address);
            lscontent = lscontent.Replace("ccapproved_date", values.ccapproved_date);
            lscontent = lscontent.Replace("email", values.email);
            lscontent = lscontent.Replace("application_name", values.application_name);
            lscontent = lscontent.Replace("sanction_refno", values.sanction_refno);
            lscontent = lscontent.Replace("validity_months", values.validity_months);
            lscontent = lscontent.Replace("relationshipmgmt_name", values.relationshipmgmt_name);
            lscontent = lscontent.Replace("employee_mobileno", values.employee_mobileno);
            lscontent = lscontent.Replace("employee_mailid", values.employee_mailid);
            lscontent = lscontent.Replace("sanction_date", lssanction_date);
            lscontent = lscontent.Replace("application_no", lsapplication_no);


            msSQL = " select concat(product_type,'-', productsub_type) as product_type,format(loanfacility_amount,2) as facility_limit, " +
                    " enduse_purpose as purposeofloan, facility_mode ,rate_interest as margin,tenureoverall_limit " +
                    " from ocs_mst_tapplication2loan where application_gid = '" + lsapplication_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getloanfacilitytype = new List<cadloanfacilitytype_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getloanfacilitytype.Add(new cadloanfacilitytype_list
                    {
                        loanfacility_amount = (dr_datarow["facility_limit"].ToString()),
                        loanfacility_type = (dr_datarow["product_type"].ToString()),
                        revolving_type = (dr_datarow["facility_mode"].ToString()),
                        tenure = (dr_datarow["tenureoverall_limit"].ToString()),
                        margin = (dr_datarow["margin"].ToString()),
                        loanTitle = (dr_datarow["purposeofloan"].ToString()),
                    });
                }
                values.cadloanfacilitytype_list = getloanfacilitytype;
            }
            dt_datatable.Dispose();

            if (values.cadloanfacilitytype_list != null && values.cadloanfacilitytype_list.Count != 0)
            {
                int j = 1;
                for (int i = 0; i < values.cadloanfacilitytype_list.Count; i++)
                {

                    String loanfacilityamount = values.cadloanfacilitytype_list[i].loanfacility_amount;
                    double loanfacility_amount = Convert.ToDouble(loanfacilityamount);
                    int facilityamount = Convert.ToInt32(loanfacility_amount);
                    string facilityamount_words = NumberToWords(facilityamount);

                    lscontent = lscontent.Replace("revolving_type" + j + "", values.cadloanfacilitytype_list[i].revolving_type);
                    lscontent = lscontent.Replace("tenure" + j + "", values.cadloanfacilitytype_list[i].tenure);
                    lscontent = lscontent.Replace("loanfacility_type" + j + "", values.cadloanfacilitytype_list[i].loanfacility_type);
                    lscontent = lscontent.Replace("loanfacility_amount" + j + "", values.cadloanfacilitytype_list[i].loanfacility_amount);
                    lscontent = lscontent.Replace("margin" + j + "", values.cadloanfacilitytype_list[i].margin);
                    lscontent = lscontent.Replace("purpose_lending" + j + "", values.cadloanfacilitytype_list[i].loanTitle);
                    lscontent = lscontent.Replace("facilityamount" + j + "_words", facilityamount_words);
                    j++;
                }
                if (values.cadloanfacilitytype_list.Count == 2)
                {
                    lscontent = lscontent.Replace("revolving_type3", "");
                    lscontent = lscontent.Replace("tenure3", "");
                    lscontent = lscontent.Replace("loanfacility_type3", "");
                    lscontent = lscontent.Replace("loanfacility_amount3", "");
                    lscontent = lscontent.Replace("margin3", "");
                    lscontent = lscontent.Replace("purpose_lending3", "");
                    lscontent = lscontent.Replace("facilityamount3_words", "");
                }
                //String proposedroi1 = values.cadloanfacilitytype_list[0].proposed_roi;
                //String proposedroi2 = values.cadloanfacilitytype_list[1].proposed_roi;
                //String loanfacilityamount1 = values.cadloanfacilitytype_list[0].loanfacility_amount;
                //String loanfacilityamount2 = values.cadloanfacilitytype_list[1].loanfacility_amount; 


                ////double proposed_roi1 = Convert.ToDouble(proposedroi1);
                //double loanfacility_amount1 = Convert.ToDouble(loanfacilityamount1);
                //double loanfacility_amount2 = Convert.ToDouble(loanfacilityamount2);


                ////double interest_amount = ((loanfacility_amount1 + loanfacility_amount2) * (proposed_roi1 / 100));
                ////int interestamount = Convert.ToInt32(interest_amount);
                ////values.interest_amount = Convert.ToString(interest_amount);

                ////double addoncharge = ((loanfacility_amount1 + loanfacility_amount2) * 1 / 100);
                ////int addon_charge = Convert.ToInt32(addoncharge);
                ////values.interest_amount = Convert.ToString(interest_amount);

                //int facilityamount1 = Convert.ToInt32(loanfacility_amount1);
                //int facilityamount2 = Convert.ToInt32(loanfacility_amount2);


                ////string interest_words = NumberToWords(interestamount);
                //string facilityamount1_words = NumberToWords(facilityamount1);
                //string facilityamount2_words = NumberToWords(facilityamount2);


                //lscontent = lscontent.Replace("revolving_type1", values.cadloanfacilitytype_list[0].revolving_type);
                //lscontent = lscontent.Replace("revolving_type2", values.cadloanfacilitytype_list[1].revolving_type);
                //lscontent = lscontent.Replace("tenure1", values.cadloanfacilitytype_list[0].tenure);
                //lscontent = lscontent.Replace("tenure2", values.cadloanfacilitytype_list[1].tenure);
                //lscontent = lscontent.Replace("loanfacility_type1", values.cadloanfacilitytype_list[0].loanfacility_type);
                //lscontent = lscontent.Replace("loanfacility_type2", values.cadloanfacilitytype_list[1].loanfacility_type);

                ////lscontent = lscontent.Replace("proposed_roi1", values.cadloanfacilitytype_list[0].proposed_roi);
                ////lscontent = lscontent.Replace("proposed_roi2", values.cadloanfacilitytype_list[1].proposed_roi);
                ////lscontent = lscontent.Replace("proposed_roi3", values.cadloanfacilitytype_list[2].proposed_roi);
                //lscontent = lscontent.Replace("loanfacility_amount1", values.cadloanfacilitytype_list[0].loanfacility_amount);
                //lscontent = lscontent.Replace("loanfacility_amount2", values.cadloanfacilitytype_list[1].loanfacility_amount);
                //lscontent = lscontent.Replace("margin1", values.cadloanfacilitytype_list[0].margin);
                //lscontent = lscontent.Replace("margin2", values.cadloanfacilitytype_list[1].margin);
                //lscontent = lscontent.Replace("purpose_lending1", values.cadloanfacilitytype_list[0].loanTitle);
                //lscontent = lscontent.Replace("purpose_lending2", values.cadloanfacilitytype_list[1].loanTitle);
                ////lscontent = lscontent.Replace("interest_amount", values.interest_amount);
                //lscontent = lscontent.Replace("facilityamount1_words", facilityamount1_words);
                //lscontent = lscontent.Replace("facilityamount2_words", facilityamount2_words);

                //if (values.cadloanfacilitytype_list.Count == 3)
                //{
                //    String loanfacilityamount3 = values.cadloanfacilitytype_list[2].loanfacility_amount;
                //    double loanfacility_amount3 = Convert.ToDouble(loanfacilityamount3);
                //    int facilityamount3 = Convert.ToInt32(loanfacility_amount3);
                //    string facilityamount3_words = NumberToWords(facilityamount3);
                //    lscontent = lscontent.Replace("revolving_type3", values.cadloanfacilitytype_list[2].revolving_type);
                //    lscontent = lscontent.Replace("tenure3", values.cadloanfacilitytype_list[2].tenure);
                //    lscontent = lscontent.Replace("loanfacility_type3", values.cadloanfacilitytype_list[2].loanfacility_type);
                //    lscontent = lscontent.Replace("loanfacility_amount3", values.cadloanfacilitytype_list[2].loanfacility_amount);
                //    lscontent = lscontent.Replace("margin3", values.cadloanfacilitytype_list[2].margin);
                //    lscontent = lscontent.Replace("purpose_lending3", values.cadloanfacilitytype_list[2].loanTitle);
                //    lscontent = lscontent.Replace("facilityamount3_words", facilityamount3_words);
                //}
            }
            //msSQL = " select  a.template_content from adm_mst_ttemplate a " +
            // " where a.template_gid='" + values.template_gid + "'";
            //lstemplate_content = objdbconn.GetExecuteScalar(msSQL);
            // lscontent = lstemplate_content;

            values.template_content = lscontent;


            values.status = true;
            return true;
        }

        public bool DaPostTemplateDBSColending(cadtemplate_list values)
        {
            msSQL = " update ocs_trn_tapplication2sanction set template_name='" + values.template_name + "', template_gid='" + values.template_gid + "' where application2sanction_gid='" + values.sanction_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            //Get Template Content
            msSQL = " select  a.template_content from adm_mst_ttemplate a " +
              " where a.template_gid='" + values.template_gid + "'";
            lstemplate_content = objdbconn.GetExecuteScalar(msSQL);

            lscontent = lstemplate_content;

            msSQL = " select a.sanction_refno, a.application_name, a.ccapproved_date, a.validity_months, template_name," +
           " a.contactperson_name, a.contactperson_number, a.contactpersonemail_address, a.contactperson_address, a.purpose_lending, b.relationshipmanager_gid," +
           " b.relationshipmanager_name, c.employee_mobileno, c.employee_emailid" +
           " from ocs_trn_tapplication2sanction a " +
           " LEFT JOIN ocs_mst_tapplication b ON a.application_gid = b.application_gid " +
           " LEFT JOIN hrm_mst_temployee c ON c.employee_gid = b.relationshipmanager_gid " +
           " where application2sanction_gid='" + values.sanction_gid + "'";
            objODBCDataReader1 = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader1.HasRows == true)
            {
                objODBCDataReader1.Read();
                values.sanction_refno = objODBCDataReader1["sanction_refno"].ToString();
                values.application_name = objODBCDataReader1["application_name"].ToString();
                values.ccapproved_date = objODBCDataReader1["ccapproved_date"].ToString();
                values.address = objODBCDataReader1["contactperson_address"].ToString();
                values.mobileno = objODBCDataReader1["contactperson_number"].ToString();
                values.email = objODBCDataReader1["contactpersonemail_address"].ToString();
                values.contactperson = objODBCDataReader1["contactperson_name"].ToString();
                values.purpose_lending = objODBCDataReader1["purpose_lending"].ToString();
                values.validity_months = objODBCDataReader1["validity_months"].ToString();
                values.relationshipmgmt_name = objODBCDataReader1["relationshipmanager_name"].ToString();
                values.employee_mobileno = objODBCDataReader1["employee_mobileno"].ToString();
                values.employee_mailid = objODBCDataReader1["employee_emailid"].ToString();
                values.template_name = objODBCDataReader1["template_name"].ToString();
            }
            objODBCDataReader1.Close();

            lscontent = lscontent.Replace("contact_person", values.contactperson);
            lscontent = lscontent.Replace("mobile_no", values.mobileno + ",");
            lscontent = lscontent.Replace("address", values.address);
            lscontent = lscontent.Replace("ccapproved_date", values.ccapproved_date);
            lscontent = lscontent.Replace("email", values.email);
            lscontent = lscontent.Replace("application_name", values.application_name);
            lscontent = lscontent.Replace("sanction_ref_no", values.sanction_refno);
            lscontent = lscontent.Replace("validity_months", values.validity_months);
            lscontent = lscontent.Replace("relationshipmgmt_name", values.relationshipmgmt_name);
            lscontent = lscontent.Replace("employee_mobileno", values.employee_mobileno);
            lscontent = lscontent.Replace("employee_mailid", values.employee_mailid);

            msSQL = "select sanction2loanfacilitytype_gid,loanfacility_gid,format(loanfacility_amount,2) as loanfacility_amount,loanfacility_type, " +
               " format(document_limit,2) as document_limit,margin,date_format(expiry_date, '%d-%m-%Y') as expiry_date,revolving_type,tenure, " +
               " interchangeability,loanfacilityref_no,SUBSTRING_INDEX(proposed_roi, '%', 1) proposed_roi" +
               " from ocs_trn_tsanction2loanfacilitytype  where application2sanction_gid='" + values.sanction_gid + "'";
            objODBCDataReader2 = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader2.HasRows == true)
            {
                values.loanfacility_type = objODBCDataReader2["loanfacility_type"].ToString();
                values.document_limit = objODBCDataReader2["document_limit"].ToString();
                values.revolving_type = objODBCDataReader2["revolving_type"].ToString();
                values.tenure = objODBCDataReader2["tenure"].ToString();
                values.loanfacility_amount = objODBCDataReader2["loanfacility_amount"].ToString();
                values.proposed_roi = objODBCDataReader2["proposed_roi"].ToString();
                values.margin = objODBCDataReader2["margin"].ToString();
            }
            objODBCDataReader2.Close();

            double proposed_roi = Convert.ToDouble(values.proposed_roi);
            double loanfacility_amount = Convert.ToDouble(values.loanfacility_amount);


            double interest_amount = (loanfacility_amount * (proposed_roi / 100));
            int interestamount = Convert.ToInt32(interest_amount);
            values.interest_amount = Convert.ToString(interest_amount);

            double addoncharge = (loanfacility_amount * 0.75 / 100);
            int addon_charge = Convert.ToInt32(addoncharge);
            values.interest_amount = Convert.ToString(interest_amount);
            values.addoncharge = Convert.ToString(addoncharge);

            int facilityamount = Convert.ToInt32(loanfacility_amount);

            string interest_words = NumberToWords(interestamount);
            string facilityamount_words = NumberToWords(facilityamount);
            string addonwords = NumberToWords(addon_charge);

            lscontent = lscontent.Replace("revolving_type", values.revolving_type);
            lscontent = lscontent.Replace("tenure", values.tenure);
            lscontent = lscontent.Replace("loanfacility_type", values.loanfacility_type);
            lscontent = lscontent.Replace("proposed_roi", values.proposed_roi);
            lscontent = lscontent.Replace("loanfacility_amount", values.loanfacility_amount);
            lscontent = lscontent.Replace("margin", values.margin);
            lscontent = lscontent.Replace("purpose_lending", values.purpose_lending);
            lscontent = lscontent.Replace("interest_amount", values.interest_amount);
            lscontent = lscontent.Replace("facilityamount_words", facilityamount_words);
            lscontent = lscontent.Replace("addoncharge", values.addoncharge);
            lscontent = lscontent.Replace("addonwords", addonwords);

            values.template_content = lscontent;



            values.status = true;
            return true;

        }


        public bool DaCADSanctionLetterSubmit(cadtemplate_list values, string employee_gid)
        {
            msSQL = " select template_name, template_gid from ocs_trn_tapplication2sanction where application2sanction_gid='" + values.sanction_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                values.template_name = objODBCDataReader["template_name"].ToString();
                values.template_gid = objODBCDataReader["template_gid"].ToString();
            }
            objODBCDataReader.Close();
            msGetGid = objcmnfunctions.GetMasterGID("ASLG");
            msSQL = "insert into ocs_trn_tsanctionlettergenerate(" +
                " sanctionlettergenerate_gid, " +
                " sanction_gid," +
                " template_gid, " +
                " template_name, " +
                " template_content, " +
                " defaulttemplate_content, " +
                " sanctionlettergenerated_by," +
                " sanctionlettergenerated_date," +
                " created_by," +
                " created_date)" +
                " values (" +
                "'" + msGetGid + "'," +
                "'" + values.sanction_gid + "'," +
                "'" + values.template_gid + "'," +
                "'" + values.template_name + "'," +
                "'" + values.template_content.Replace("'", "''") + "'," +
                "'" + values.defaulttemplate_content.Replace("'", "''") + "'," +
                "'" + employee_gid + "'," +
                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                "'" + employee_gid + "'," +
                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult == 1)
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/SanctionLetterGeneration/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                {
                    if ((!System.IO.Directory.Exists(values.lspath)))
                        System.IO.Directory.CreateDirectory(values.lspath);
                }

                values.lsname = "Sanction_Letter" + DateTime.Now.ToString("(dd-MM-yyyy HH-mm-ss)") + ".docx";
                values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/SanctionLetterGeneration/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;

                msSQL = " update ocs_trn_tapplication2sanction set makerfile_path='" + lscompany_code + "/" + "Master/SanctionLetterGeneration/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname + "'," +
                        " makerfile_name='" + values.lsname + "', sanctionletter_status='Generated'," +
                        " defaulttemplate_content='" + values.defaulttemplate_content.Replace("'", "''") + "'," +
                        " template_content='" + values.template_content.Replace("'", "''") + "', makersubmitted_by='" + employee_gid + "'," +
                        " makersubmitted_on='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where application2sanction_gid='" + values.sanction_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update ocs_trn_tdynamictemplatedtl set template_content='" + values.defaulttemplate_content.Replace("'", "''") + "' " +
                        " where templatetype_gid='" + values.sanction_gid + "' and templatetype_name='" + getTemplateClass.Sanction + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                lspath = HttpContext.Current.Server.MapPath("erpdocument" + "/" + lscompany_code + "/" + "Master/SanctionLetterGeneration/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/");
                string lsfile_gid = objcmnfunctions.GetMasterGID("UPLF");

                // Save the HTML string as HTML File.
                string htmlFilePath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/SanctionLetterHTML";
                {
                    if ((!System.IO.Directory.Exists(htmlFilePath)))
                        System.IO.Directory.CreateDirectory(htmlFilePath);
                }

                htmlFilePath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/SanctionLetterHTML/sanctionletterdoc.html";
                File.WriteAllText(htmlFilePath, values.template_content);

                Spire.Doc.Document document = new Spire.Doc.Document();

                document.LoadFromFile(htmlFilePath, Spire.Doc.FileFormat.Html, XHTMLValidationType.None);

                //Read Header and Footer File

                Document doc1 = new Document();
                doc1.LoadFromFile(ConfigurationManager.AppSettings["file_path"] + "/templates/headerfile.docx");
                HeaderFooter header = doc1.Sections[0].HeadersFooters.Header;
                HeaderFooter footer = doc1.Sections[0].HeadersFooters.Footer;
                Document doc2 = new Document(htmlFilePath);

                foreach (Section section in doc2.Sections)
                {

                    foreach (Paragraph paragraph in section.Paragraphs)
                    {
                        paragraph.Format.BeforeSpacing = 0;
                        paragraph.Format.AfterAutoSpacing = false;
                        paragraph.Format.AfterSpacing = 0;
                        paragraph.Format.BeforeAutoSpacing = false;
                        paragraph.Format.HorizontalAlignment = HorizontalAlignment.Justify;

                    }

                    foreach (DocumentObject obj in section.Body.ChildObjects)
                    {
                        if (obj is Paragraph)
                        {
                            var para = obj as Paragraph;
                            foreach (DocumentObject Pobj in para.ChildObjects)
                            {

                                if (Pobj is TextRange)
                                {
                                    TextRange textRange = Pobj as TextRange;
                                    textRange.CharacterFormat.FontName = "Calibri";
                                    textRange.CharacterFormat.FontSize = 10;
                                }
                            }
                        }
                    }
                    foreach (Spire.Doc.Table table in section.Tables)
                    {
                        foreach (Spire.Doc.TableRow row in table.Rows)
                        {
                            foreach (Spire.Doc.TableCell cell in row.Cells)
                            {

                                foreach (Paragraph p in cell.Paragraphs)
                                {
                                    foreach (DocumentObject Pobj in p.ChildObjects)
                                    {
                                        if (Pobj is TextRange)
                                        {
                                            TextRange textRange = Pobj as TextRange;
                                            textRange.CharacterFormat.FontName = "Calibri";
                                            textRange.CharacterFormat.FontSize = 10;
                                        }
                                    }
                                    p.Format.BeforeSpacing = 0;
                                    p.Format.AfterAutoSpacing = false;
                                    p.Format.AfterSpacing = 0;
                                    p.Format.BeforeAutoSpacing = false;
                                    p.Format.HorizontalAlignment = HorizontalAlignment.Justify;
                                }
                            }

                        }
                    }

                    foreach (DocumentObject obj in header.ChildObjects)
                    {
                        section.HeadersFooters.Header.ChildObjects.Add(obj.Clone());
                    }
                    foreach (DocumentObject obj in footer.ChildObjects)
                    {
                        section.HeadersFooters.Footer.ChildObjects.Add(obj.Clone());
                    }
                }

                // Document
                string lssanctionletterfile = HttpContext.Current.Server.MapPath("erpdocument" + "/" + lscompany_code + "/" + "TmpFile/Footer/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + lsfile_gid + "/");
                {
                    if ((!System.IO.Directory.Exists(lssanctionletterfile)))
                        System.IO.Directory.CreateDirectory(lssanctionletterfile);
                }
                lssanctionletterfile = lssanctionletterfile + values.lsname;
                doc2.SaveToFile(lssanctionletterfile, Spire.Doc.FileFormat.Docx);

                Document doc3 = new Document();
                doc3.LoadFromFile(lssanctionletterfile);

                // Inser Footer Number
                // HeaderFooter footer = doc3.Sections[0].HeadersFooters.Footer;
                //HeaderFooter footer = doc3.Sections[0].HeadersFooters.Header;

                //Paragraph footerParagraph = footer.AddParagraph();
                //footerParagraph.AppendField("page number", FieldType.FieldPage);
                //footerParagraph.AppendText(" of ");
                //footerParagraph.AppendField("number of pages", FieldType.FieldNumPages);
                //footerParagraph.Format.HorizontalAlignment = HorizontalAlignment.Center;

                // Set Margin
                doc3.Sections[0].PageSetup.Margins.Left = 27.9f;
                doc3.Sections[0].PageSetup.Margins.Right = 27.9f;
                doc3.SaveToFile(values.lspath, Spire.Doc.FileFormat.Docx);

                MemoryStream ms = new MemoryStream();
                doc3.SaveToStream(ms, Spire.Doc.FileFormat.Docx);

                bool status;
                status = objcmnstorage.UploadStream("../../../erpdocument", lscompany_code + "/" + "Master/SanctionLetterGeneration/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname, ms);
                
                ms.Close();
                File.Delete(htmlFilePath);

                values.status = true;
                values.message = "Sanction Letter Generated Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
                return true;
            }
        }

        public void DaPostProceedToChecker(cadtemplate_list values, string employee_gid)
        {
            msSQL = " select maker_gid from ocs_trn_tprocesstype_assign a " +
                   " where a.processtype_name = 'Accept' and a.menu_gid = 'CADMGTSAN' and a.application_gid = '" + values.application_gid + "'";
            values.maker_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select checker_gid from ocs_trn_tprocesstype_assign a " +
                   " where a.processtype_name = 'Accept' and a.menu_gid = 'CADMGTSAN' and a.application_gid = '" + values.application_gid + "'";
            values.checker_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select approver_gid from ocs_trn_tprocesstype_assign a " +
                   " where a.processtype_name = 'Accept' and a.menu_gid = 'CADMGTSAN' and a.application_gid = '" + values.application_gid + "'";
            values.approver_gid = objdbconn.GetExecuteScalar(msSQL);

            if (values.maker_gid == values.checker_gid)
            {
                //if (values.maker_gid == values.approver_gid)
                //{
                //    msSQL = "update ocs_trn_tapplication2sanction set sanctionletter_flag='Y', sanction_status='Approved' where application2sanction_gid='" + values.sanction_gid + "'";
                //    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                //    if (mnResult != 0)
                //    {
                //        msSQL = " select  processtypeassign_gid from ocs_trn_tprocesstype_assign a " +
                //                 " left join ocs_trn_tapplication2sanction b on a.application_gid = b.application_gid " +
                //                 " where application2sanction_gid = '" + values.sanction_gid + "' and a.menu_gid ='" + getMenuClass.Sanction + "'";
                //        string lsprocesstypeassign_gid = objdbconn.GetExecuteScalar(msSQL);

                //        if (lsprocesstypeassign_gid != "")
                //        {
                //            msSQL = " update ocs_trn_tprocesstype_assign set maker_approvalflag='Y', " +
                //                    " maker_approveddate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                //                    " checker_approvalflag ='Y', checker_approveddate = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                //                    " approver_approvalflag ='Y', approver_approveddate = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                //                    " overall_approvalstatus='Approved'  where processtypeassign_gid='" + lsprocesstypeassign_gid + "'";
                //            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                //        }

                //        msSQL = " select template_name, template_gid from ocs_trn_tapplication2sanction where application2sanction_gid='" + values.sanction_gid + "'";
                //        objODBCDataReader = objdbconn.GetDataReader(msSQL);
                //        if (objODBCDataReader.HasRows == true)
                //        {
                //            values.template_name = objODBCDataReader["template_name"].ToString();
                //            values.template_gid = objODBCDataReader["template_gid"].ToString();
                //        }
                //        objODBCDataReader.Close();

                //        msGetGid = objcmnfunctions.GetMasterGID("ASLL");
                //        msSQL = "insert into ocs_trn_tsanctionapprovallog(" +
                //            " sanctionapprovallog_gid, " +
                //            " sanction_gid," +
                //            " template_gid, " +
                //            " template_name, " +
                //            " template_content, " +
                //            " sanctionletter_flag," +
                //            " sanction_status," +
                //            " checkerpushback_remarks," +
                //            " checkerreject_remarks," +
                //            " created_by," +
                //            " created_date)" +
                //            " values (" +
                //            "'" + msGetGid + "'," +
                //            "'" + values.sanction_gid + "'," +
                //            "'" + values.template_gid + "'," +
                //            "'" + values.template_name + "'," +
                //            "'" + values.template_content.Replace("'", "''") + "'," +
                //            "'Y'," +
                //            "'Approved'," +
                //            "''," +
                //            "''," +
                //            "'" + employee_gid + "'," +
                //            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                //        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                //        values.message = "Sanction Approved Successfully";
                //        values.status = true;
                //    }
                //    else
                //    {
                //        values.message = "Error Occrued";
                //        values.status = false;
                //    }
                //}
                ////else
                ////{
                msSQL = " update ocs_trn_tapplication2sanction set sanctionletter_flag='Y',checkerletter_flag='Y', sanction_status ='Final Approval Pending', checkerupdated_by ='" + employee_gid + "', " +
                        " checkerupdated_on='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where application2sanction_gid='" + values.sanction_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    msSQL = " select  processtypeassign_gid from ocs_trn_tprocesstype_assign a " +
                             " left join ocs_trn_tapplication2sanction b on a.application_gid = b.application_gid " +
                             " where application2sanction_gid = '" + values.sanction_gid + "' and a.menu_gid ='" + getMenuClass.Sanction + "'";
                    string lsprocesstypeassign_gid = objdbconn.GetExecuteScalar(msSQL);

                    if (lsprocesstypeassign_gid != "")
                    {
                        msSQL = " update ocs_trn_tprocesstype_assign set maker_approvalflag='Y', " +
                                " maker_approveddate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                " checker_approvalflag ='Y', checker_approveddate = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                " overall_approvalstatus='Approved'  where processtypeassign_gid='" + lsprocesstypeassign_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }

                    msSQL = " select template_name, template_gid from ocs_trn_tapplication2sanction where application2sanction_gid='" + values.sanction_gid + "'";
                    objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDataReader.HasRows == true)
                    {
                        values.template_name = objODBCDataReader["template_name"].ToString();
                        values.template_gid = objODBCDataReader["template_gid"].ToString();
                    }
                    objODBCDataReader.Close();

                    msGetGid = objcmnfunctions.GetMasterGID("ASLL");
                    msSQL = "insert into ocs_trn_tsanctionapprovallog(" +
                            " sanctionapprovallog_gid, " +
                            " sanction_gid," +
                            " template_gid, " +
                            " template_name, " +
                            " template_content, " +
                            " sanctionletter_flag," +
                            " sanction_status," +
                            " checkerpushback_remarks," +
                            " checkerreject_remarks," +
                            " created_by," +
                            " created_date)" +
                            " values (" +
                            "'" + msGetGid + "'," +
                            "'" + values.sanction_gid + "'," +
                            "'" + values.template_gid + "'," +
                            "'" + values.template_name + "'," +
                            "'" + values.template_content.Replace("'", "''") + "'," +
                            "'Y'," +
                            "'Checker Approved'," +
                            "''," +
                            "''," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    values.message = "Sanction proceeded to Approval Successfully";
                    values.status = true;
                }
                else
                {
                    values.message = "Error Occrued";
                    values.status = false;
                }
                //}
            }
            else
            {
                msSQL = "update ocs_trn_tapplication2sanction set sanctionletter_flag='Y', sanction_status='Checker Approval Pending' where application2sanction_gid='" + values.sanction_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    msSQL = " select  processtypeassign_gid from ocs_trn_tprocesstype_assign a " +
                             " left join ocs_trn_tapplication2sanction b on a.application_gid = b.application_gid " +
                             " where application2sanction_gid = '" + values.sanction_gid + "' and a.menu_gid ='" + getMenuClass.Sanction + "'";
                    string lsprocesstypeassign_gid = objdbconn.GetExecuteScalar(msSQL);

                    if (lsprocesstypeassign_gid != "")
                    {
                        msSQL = " update ocs_trn_tprocesstype_assign set maker_approvalflag='Y', " +
                                " maker_approveddate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                " overall_approvalstatus='Proceed to Checker'  where processtypeassign_gid='" + lsprocesstypeassign_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }

                    msSQL = " select template_name, template_gid from ocs_trn_tapplication2sanction where application2sanction_gid='" + values.sanction_gid + "'";
                    objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDataReader.HasRows == true)
                    {
                        values.template_name = objODBCDataReader["template_name"].ToString();
                        values.template_gid = objODBCDataReader["template_gid"].ToString();
                    }
                    objODBCDataReader.Close();

                    msGetGid = objcmnfunctions.GetMasterGID("ASLL");
                    msSQL = "insert into ocs_trn_tsanctionapprovallog(" +
                        " sanctionapprovallog_gid, " +
                        " sanction_gid," +
                        " template_gid, " +
                        " template_name, " +
                        " template_content, " +
                        " sanctionletter_flag," +
                        " sanction_status," +
                        " checkerpushback_remarks," +
                        " checkerreject_remarks," +
                        " created_by," +
                        " created_date)" +
                        " values (" +
                        "'" + msGetGid + "'," +
                        "'" + values.sanction_gid + "'," +
                        "'" + values.template_gid + "'," +
                        "'" + values.template_name + "'," +
                        "'" + values.template_content.Replace("'", "''") + "'," +
                        "'Y'," +
                        "'Checker Approval Pending'," +
                        "''," +
                        "''," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    values.message = "Sanction proceeded to Checker Successfully";
                    values.status = true;
                }
                else
                {
                    values.message = "Error Occrued";
                    values.status = false;
                }
            }
        }

        public bool DaSanctionToCheckerSummary(sanctiondetailsList values, string employee_gid)
        {
            //msSQL = " SELECT a.application2sanction_gid, a.sanction_refno,date_format(a.sanction_date,'%d-%m-%Y') as sanction_date, " +
            //       " b.application_no, b.approval_status,a.sanction_refno,b.customer_urn," +
            //       " format((sanction_amount),2) as sanction_amount,a.sanction_limit,b.customer_name, ccapproved_date, sanctionto_name, " +
            //       " sanction_status, b.application_gid, " +
            //       " concat(d.user_code,' / ',d.user_firstname,d.user_lastname) as makersubmitted_by, " +
            //       " date_format(a.makersubmitted_on,'%d-%m-%Y %h:%i %p') as makersubmitted_on," +
            //       " reset_flag, e.cadgroup_name,b.renewal_flag,b.enhancement_flag FROM ocs_trn_tapplication2sanction a " +
            //       " LEFT JOIN ocs_mst_tapplication b ON a.application_gid = b.application_gid" +
            //       " LEFT JOIN hrm_mst_temployee c ON a.makersubmitted_by=c.employee_gid" +
            //       " LEFT JOIN adm_mst_tuser d ON c.user_gid=d.user_gid " +
            //       " left join ocs_trn_tprocesstype_assign e on e.application_gid = a.application_gid " +
            //       " where (a.sanctionletter_flag='Y' or sanction_status='Pushback') and a.checkerletter_flag='N' and e.menu_gid = 'CADMGTSAN' " +
            //       " and e.checker_gid = '" + employee_gid + "' and e.checker_approvalflag='N' " +
            //       " group by a.application_gid  ORDER BY application2sanction_gid DESC ";
            msSQL = "call ocs_trn_spsanctiontocheckersummary ('" + employee_gid + "')";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_sanctiondtl = new List<sanctiondetails>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    string lsccgroup_name;
                    string lscadgroup_name;

                    //msSQL = "select group_concat(ccgroup_name) as ccgroup_name from ocs_mst_tapplication where application_gid='" + dt["application_gid"] + "'";
                    //objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    //if (objODBCDataReader.HasRows == true)
                    //{
                    //    lsccgroup_name = objODBCDataReader["ccgroup_name"].ToString();
                    //}
                    //else
                    //{
                    //    lsccgroup_name = "";
                    //}
                    //objODBCDataReader.Close();
                    //msSQL = "select group_concat(cadgroup_name) as cadgroup_name from ocs_trn_tprocesstype_assign where application_gid='" + dt["application_gid"] + "'";
                    //objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    //if (objODBCDataReader.HasRows == true)
                    //{
                    //    lscadgroup_name = objODBCDataReader["cadgroup_name"].ToString();
                    //}
                    //else
                    //{
                    //    lscadgroup_name = "";
                    //}
                    //objODBCDataReader.Close();

                    get_sanctiondtl.Add(new sanctiondetails
                    {
                        application_gid = dt["application_gid"].ToString(),
                        application2sanction_gid = dt["application2sanction_gid"].ToString(),
                        sanction_refno = dt["sanction_refno"].ToString(),
                        sanction_date = dt["sanction_date"].ToString(),
                        sanction_amount = dt["sanction_amount"].ToString(),
                        sanction_limit = dt["sanction_limit"].ToString(),
                        customername = dt["customer_name"].ToString(),
                        created_by = dt["makersubmitted_by"].ToString(),
                        created_date = dt["makersubmitted_on"].ToString(),
                        reset_flag = dt["reset_flag"].ToString(),
                        application_no = dt["application_no"].ToString(),
                        ccapproved_date = dt["ccapproved_date"].ToString(),
                        sanction_status = dt["sanction_status"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        ccgroup_name = dt["ccgroup_name"].ToString(),
                        //ccgroup_name = lsccgroup_name,
                        cadgroupname = dt["cadgroup_name"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        renewal_flag = dt["renewal_flag"].ToString(),
                        enhancement_flag = dt["enhancement_flag"].ToString()

                    });
                }
                values.sanctiondetails = get_sanctiondtl;
            }
            dt_datatable.Dispose();

            return true;
        }


        public bool DaSanctionToCheckerFollowupSummary(sanctiondetailsList values, string employee_gid)
        {
            //msSQL = " SELECT a.application2sanction_gid,a.sanction_refno,date_format(a.sanction_date,'%d-%m-%Y') as sanction_date, b.application_no, b.approval_status," +
            //       " format((sanction_amount),2) as sanction_amount,a.sanction_limit,b.customer_name, ccapproved_date, sanctionto_name, sanction_status, b.application_gid, " +
            //       " concat(d.user_code,' / ',d.user_firstname,d.user_lastname) as makersubmitted_by,date_format(a.makersubmitted_on,'%d-%m-%Y %h:%i %p') as makersubmitted_on," +
            //       " reset_flag, e.cadgroup_name,a.sanction_refno,b.customer_urn,b.renewal_flag,b.enhancement_flag FROM ocs_trn_tapplication2sanction a " +
            //       " LEFT JOIN ocs_trn_tcadapplication b ON a.application_gid = b.application_gid" +
            //       " LEFT JOIN hrm_mst_temployee c ON a.makersubmitted_by=c.employee_gid" +
            //       " LEFT JOIN adm_mst_tuser d ON c.user_gid=d.user_gid " +
            //       " left join ocs_trn_tprocesstype_assign e on e.application_gid = a.application_gid " +
            //       " where e.menu_gid = 'CADMGTSAN'  and e.checker_gid = '" + employee_gid + "' and e.checker_approvalflag='Y' and e.approver_approvalflag='N' " +
            //       " group by a.application_gid ORDER BY application2sanction_gid DESC ";
            msSQL = "call ocs_trn_spsanctionTocheckerfollowupsummary ('" + employee_gid + "')";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_sanctiondtl = new List<sanctiondetails>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    string lsccgroup_name;
                    string lscadgroup_name;

                    //msSQL = "select group_concat(ccgroup_name) as ccgroup_name from ocs_mst_tapplication where application_gid='" + dt["application_gid"] + "'";
                    //objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    //if (objODBCDataReader.HasRows == true)
                    //{
                    //    lsccgroup_name = objODBCDataReader["ccgroup_name"].ToString();
                    //}
                    //else
                    //{
                    //    lsccgroup_name = "";
                    //}
                    //objODBCDataReader.Close();

                    get_sanctiondtl.Add(new sanctiondetails
                    {
                        application2sanction_gid = dt["application2sanction_gid"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        sanction_refno = dt["sanction_refno"].ToString(),
                        sanction_date = dt["sanction_date"].ToString(),
                        sanction_amount = dt["sanction_amount"].ToString(),
                        sanction_limit = dt["sanction_limit"].ToString(),
                        customername = dt["customer_name"].ToString(),
                        created_by = dt["makersubmitted_by"].ToString(),
                        created_date = dt["makersubmitted_on"].ToString(),
                        reset_flag = dt["reset_flag"].ToString(),
                        application_no = dt["application_no"].ToString(),
                        ccapproved_date = dt["ccapproved_date"].ToString(),
                        sanction_status = dt["sanction_status"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        //ccgroup_name = lsccgroup_name,
                        ccgroup_name = dt["ccgroup_name"].ToString(),
                        cadgroupname = dt["cadgroup_name"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        renewal_flag = dt["renewal_flag"].ToString(),
                        enhancement_flag = dt["enhancement_flag"].ToString()
                    });
                }
                values.sanctiondetails = get_sanctiondtl;
            }
            dt_datatable.Dispose();

            return true;
        }


        public void DaPostProceedToApproval(string employee_gid, cadtemplate_list values)
        {
            msSQL = " select maker_gid from ocs_trn_tprocesstype_assign a " +
                   " where a.processtype_name = 'Accept' and a.menu_gid = 'CADMGTSAN' and a.application_gid = '" + values.application_gid + "'";
            values.maker_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select checker_gid from ocs_trn_tprocesstype_assign a " +
                   " where a.processtype_name = 'Accept' and a.menu_gid = 'CADMGTSAN' and a.application_gid = '" + values.application_gid + "'";
            values.checker_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select approver_gid from ocs_trn_tprocesstype_assign a " +
                   " where a.processtype_name = 'Accept' and a.menu_gid = 'CADMGTSAN' and a.application_gid = '" + values.application_gid + "'";
            values.approver_gid = objdbconn.GetExecuteScalar(msSQL);

            //if (values.checker_gid == values.approver_gid)
            //{
            //    msSQL = " update ocs_trn_tapplication2sanction set checkerletter_flag='Y', checkerupdated_by ='" + employee_gid + "', " +
            //            " checkerupdated_on='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', " +
            //            " sanction_status ='Approved' where application2sanction_gid='" + values.sanction_gid + "'";
            //    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            //    if (mnResult != 0)
            //    {
            //        msSQL = " select  processtypeassign_gid from ocs_trn_tprocesstype_assign a " +
            //                " left join ocs_trn_tapplication2sanction b on a.application_gid = b.application_gid " +
            //                " where application2sanction_gid = '" + values.sanction_gid + "' and menu_gid = '" + getMenuClass.Sanction + "'";
            //        string lsprocesstypeassign_gid = objdbconn.GetExecuteScalar(msSQL);

            //        if (lsprocesstypeassign_gid != "")
            //        {
            //            msSQL = " update ocs_trn_tprocesstype_assign set checker_approvalflag='Y', " +
            //                    " checker_approveddate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
            //                    " approver_approvalflag ='Y', approver_approveddate = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
            //                    " overall_approvalstatus='Approved'  where processtypeassign_gid='" + lsprocesstypeassign_gid + "'";
            //            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            //        }

            //        msSQL = " select template_name, template_gid, template_content from ocs_trn_tapplication2sanction where application2sanction_gid='" + values.sanction_gid + "'";
            //        objODBCDataReader = objdbconn.GetDataReader(msSQL);
            //        if (objODBCDataReader.HasRows == true)
            //        {
            //            values.template_name = objODBCDataReader["template_name"].ToString();
            //            values.template_gid = objODBCDataReader["template_gid"].ToString();
            //            values.template_content = objODBCDataReader["template_content"].ToString();
            //        }
            //        objODBCDataReader.Close();

            //        msGetGid = objcmnfunctions.GetMasterGID("ASLL");
            //        msSQL = "insert into ocs_trn_tsanctionapprovallog(" +
            //            " sanctionapprovallog_gid, " +
            //            " sanction_gid," +
            //            " template_gid, " +
            //            " template_name, " +
            //            " template_content, " +
            //            " sanctionletter_flag," +
            //            " sanction_status," +
            //            " checkerpushback_remarks," +
            //            " checkerreject_remarks," +
            //            " created_by," +
            //            " created_date)" +
            //            " values (" +
            //            "'" + msGetGid + "'," +
            //            "'" + values.sanction_gid + "'," +
            //            "'" + values.template_gid + "'," +
            //            "'" + values.template_name + "'," +
            //            "'" + values.template_content.Replace("'", "''") + "'," +
            //            "'Y'," +
            //            "'Approved'," +
            //            "''," +
            //            "''," +
            //            "'" + employee_gid + "'," +
            //            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            //        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            //        values.message = "Sanction Approved Successfully";
            //        values.status = true;
            //    }
            //    else
            //    {
            //        values.message = "Error Occured";
            //        values.status = false;
            //    }
            //}
            //else
            //{
            msSQL = " update ocs_trn_tapplication2sanction set checkerletter_flag='Y', sanction_status ='Final Approval Pending', checkerupdated_by ='" + employee_gid + "', " +
                    " checkerupdated_on='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where application2sanction_gid='" + values.sanction_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = " select  processtypeassign_gid from ocs_trn_tprocesstype_assign a " +
                        " left join ocs_trn_tapplication2sanction b on a.application_gid = b.application_gid " +
                        " where application2sanction_gid = '" + values.sanction_gid + "' and menu_gid = '" + getMenuClass.Sanction + "'";
                string lsprocesstypeassign_gid = objdbconn.GetExecuteScalar(msSQL);

                if (lsprocesstypeassign_gid != "")
                {
                    msSQL = " update ocs_trn_tprocesstype_assign set checker_approvalflag='Y', " +
                            " checker_approveddate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                            " overall_approvalstatus='Approved'  where processtypeassign_gid='" + lsprocesstypeassign_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                msSQL = " select template_name, template_gid, template_content from ocs_trn_tapplication2sanction where application2sanction_gid='" + values.sanction_gid + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows == true)
                {
                    values.template_name = objODBCDataReader["template_name"].ToString();
                    values.template_gid = objODBCDataReader["template_gid"].ToString();
                    values.template_content = objODBCDataReader["template_content"].ToString();
                }
                objODBCDataReader.Close();

                msGetGid = objcmnfunctions.GetMasterGID("ASLL");
                msSQL = "insert into ocs_trn_tsanctionapprovallog(" +
                    " sanctionapprovallog_gid, " +
                    " sanction_gid," +
                    " template_gid, " +
                    " template_name, " +
                    " template_content, " +
                    " sanctionletter_flag," +
                    " sanction_status," +
                    " checkerpushback_remarks," +
                    " checkerreject_remarks," +
                    " created_by," +
                    " created_date)" +
                    " values (" +
                    "'" + msGetGid + "'," +
                    "'" + values.sanction_gid + "'," +
                    "'" + values.template_gid + "'," +
                    "'" + values.template_name + "'," +
                    "'" + values.template_content.Replace("'", "''") + "'," +
                    "'Y'," +
                    "'Checker Approved'," +
                    "''," +
                    "''," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.message = "Sanction Proceeded to Approval Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;
            }
            //}               
        }

        public void DaPusbackToMaker(cadtemplate_list values, string employee_gid)
        {
            msSQL = " update ocs_trn_tapplication2sanction set sanctionletter_flag='N', checkerpushback_remarks='" + values.pushback_remarks.Replace("'", "") + "'," +
                    " sanction_status='Pushback' where application2sanction_gid='" + values.sanction_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                msSQL = " select  processtypeassign_gid from ocs_trn_tprocesstype_assign a " +
                         " left join ocs_trn_tapplication2sanction b on a.application_gid = b.application_gid " +
                         " where application2sanction_gid = '" + values.sanction_gid + "' and menu_gid = '" + getMenuClass.Sanction + "'";
                string lsprocesstypeassign_gid = objdbconn.GetExecuteScalar(msSQL);

                if (lsprocesstypeassign_gid != "")
                {
                    msSQL = " update ocs_trn_tprocesstype_assign set maker_approvalflag='N', " +
                            " overall_approvalstatus='Pushback To Maker' where processtypeassign_gid='" + lsprocesstypeassign_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                msSQL = " select template_name, template_gid, template_content from ocs_trn_tapplication2sanction where application2sanction_gid='" + values.sanction_gid + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows == true)
                {
                    values.template_name = objODBCDataReader["template_name"].ToString();
                    values.template_gid = objODBCDataReader["template_gid"].ToString();
                    values.template_content = objODBCDataReader["template_content"].ToString();
                }
                objODBCDataReader.Close();

                msGetGid = objcmnfunctions.GetMasterGID("ASLL");
                msSQL = "insert into ocs_trn_tsanctionapprovallog(" +
                    " sanctionapprovallog_gid, " +
                    " sanction_gid," +
                    " template_gid, " +
                    " template_name, " +
                    " template_content, " +
                    " sanctionletter_flag," +
                    " sanction_status," +
                    " checkerpushback_remarks," +
                    " checkerreject_remarks," +
                    " created_by," +
                    " created_date)" +
                    " values (" +
                    "'" + msGetGid + "'," +
                    "'" + values.sanction_gid + "'," +
                    "'" + values.template_gid + "'," +
                    "'" + values.template_name + "'," +
                    "'" + values.template_content.Replace("'", "''") + "'," +
                    "'N'," +
                    "'Checker Pushback'," +
                    "'" + values.pushback_remarks.Replace("'", "") + "'," +
                    "''," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.message = "Sanction Pushbacked Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occrued";
                values.status = false;
            }
        }

        public bool DaCheckerApprovalSummary(sanctiondetailsList values, string employee_gid)
        {
            //msSQL = " SELECT a.application2sanction_gid,a.sanction_refno,date_format(a.sanction_date,'%d-%m-%Y') as sanction_date, sanction_status, b.application_gid, " +
            //       " ccapproved_date, format((sanction_amount),2) as sanction_amount,b.customer_name,checkerapproval_flag, a.checkerreject_remarks, b.approval_status," +
            //       " b.application_no, concat(d.user_code,' / ',d.user_firstname,d.user_lastname) as checkerupdated_by, " +
            //       " date_format(a.checkerupdated_on,'%d-%m-%Y %h:%i %p') as checkerupdated_on, sanctionto_name, e.cadgroup_name," +
            //       " a.sanction_refno,b.customer_urn,b.renewal_flag,b.enhancement_flag FROM ocs_trn_tapplication2sanction a " +
            //       " LEFT JOIN ocs_trn_tcadapplication b ON a.application_gid = b.application_gid" +
            //       " LEFT JOIN hrm_mst_temployee c ON a.checkerupdated_by=c.employee_gid" +
            //       " LEFT JOIN adm_mst_tuser d ON c.user_gid=d.user_gid " +
            //       " left join ocs_trn_tprocesstype_assign e on e.application_gid = a.application_gid " +
            //       " where a.checkerletter_flag='Y' and e.menu_gid = 'CADMGTSAN' " +
            //       " and e.approver_gid = '" + employee_gid + "' and e.approver_approvalflag='N' ORDER BY application2sanction_gid DESC ";
            msSQL = "call ocs_trn_spsanctioncheckerapprovalsummary ('" + employee_gid + "')";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_sanctiondtl = new List<sanctiondetails>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    string lsccgroup_name;
                    string lscadgroup_name;

                    //msSQL = "select group_concat(ccgroup_name) as ccgroup_name from ocs_trn_tcadapplication where application_gid='" + dt["application_gid"] + "'";
                    //objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    //if (objODBCDataReader.HasRows == true)
                    //{
                    //    lsccgroup_name = objODBCDataReader["ccgroup_name"].ToString();
                    //}
                    //else
                    //{
                    //    lsccgroup_name = "";
                    //}
                    //objODBCDataReader.Close();
                    //msSQL = "select group_concat(cadgroup_name) as cadgroup_name from ocs_trn_tprocesstype_assign where application_gid='" + dt["application_gid"] + "'";
                    //objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    //if (objODBCDataReader.HasRows == true)
                    //{
                    //    lscadgroup_name = objODBCDataReader["cadgroup_name"].ToString();
                    //}
                    //else
                    //{
                    //    lscadgroup_name = "";
                    //}
                    //objODBCDataReader.Close();

                    get_sanctiondtl.Add(new sanctiondetails
                    {
                        application2sanction_gid = dt["application2sanction_gid"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        sanction_refno = dt["sanction_refno"].ToString(),
                        sanction_date = dt["sanction_date"].ToString(),
                        sanction_amount = dt["sanction_amount"].ToString(),
                        customername = dt["customer_name"].ToString(),
                        created_by = dt["checkerupdated_by"].ToString(),
                        created_date = dt["checkerupdated_on"].ToString(),
                        checkerapproval_flag = dt["checkerapproval_flag"].ToString(),
                        checkerreject_remarks = dt["checkerreject_remarks"].ToString(),
                        sanction_status = dt["sanction_status"].ToString(),
                        //ccgroup_name = lsccgroup_name,
                        ccgroup_name = dt["ccgroup_name"].ToString(),
                        cadgroupname = dt["cadgroup_name"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        application_no = dt["application_no"].ToString(),
                        ccapproved_date = dt["ccapproved_date"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        renewal_flag = dt["renewal_flag"].ToString(),
                        enhancement_flag = dt["enhancement_flag"].ToString()
                    });
                }
                values.sanctiondetails = get_sanctiondtl;
            }
            dt_datatable.Dispose();

            return true;
        }

        public bool DaApprovalCompletedSummary(sanctiondetailsList values, string employee_gid)
        {
            //msSQL = " SELECT a.application2sanction_gid,a.sanction_refno,date_format(a.sanction_date,'%d-%m-%Y') as sanction_date, sanction_status, b.application_gid, " +
            //       " ccapproved_date, format((sanction_amount),2) as sanction_amount,b.customer_name,checkerapproval_flag, a.checkerreject_remarks, b.approval_status," +
            //       " b.application_no, concat(d.user_code,' / ',d.user_firstname,d.user_lastname) as checkerupdated_by, " +
            //       " date_format(a.checkerupdated_on,'%d-%m-%Y %h:%i %p') as checkerupdated_on, sanctionto_name, e.cadgroup_name," +
            //       " date_format(e.approver_approveddate,'%d-%m-%Y %h:%i %p') as approver_approveddate, " +
            //       " a.sanction_refno,b.customer_urn,b.renewal_flag,b.enhancement_flag FROM ocs_trn_tapplication2sanction a " +
            //       " LEFT JOIN ocs_trn_tcadapplication b ON a.application_gid = b.application_gid" +
            //       " LEFT JOIN hrm_mst_temployee c ON a.checkerupdated_by=c.employee_gid" +
            //       " LEFT JOIN adm_mst_tuser d ON c.user_gid=d.user_gid " +
            //       " left join ocs_trn_tprocesstype_assign e on e.application_gid = a.application_gid " +
            //       " left join ocs_trn_tsanctionacceptlog f on f.application_gid = a.application_gid " +
            //       " where e.menu_gid = 'CADMGTSAN' and (e.maker_gid ='" + employee_gid + "' or e.checker_gid='" + employee_gid + "' or e.approver_gid = '" + employee_gid + "') " +
            //       " and e.approver_approvalflag='Y' and (  f.accepted_status is null) " +
            //       " and  (f.updated_date is null ) " +
            //       " ORDER BY application2sanction_gid DESC ";
            msSQL = "call ocs_trn_spapprovalcompletedsummary ('" + employee_gid + "')";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_sanctiondtl = new List<sanctiondetails>();
            if (dt_datatable.Rows.Count != 0)
            {

                foreach (DataRow dt in dt_datatable.Rows)
                {
                    string lsccgroup_name;
                    string lscadgroup_name;

                    //msSQL = "select group_concat(ccgroup_name) as ccgroup_name from ocs_mst_tapplication where application_gid='" + dt["application_gid"] + "'";
                    //objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    //if (objODBCDataReader.HasRows == true)
                    //{
                    //    lsccgroup_name = objODBCDataReader["ccgroup_name"].ToString();
                    //}
                    //else
                    //{
                    //    lsccgroup_name = "";
                    //}
                    //objODBCDataReader.Close();


                    // msSQL = " select sanctionacceptlog_gid from ocs_trn_tsanctionacceptlog  " +
                    //" where  (updated_date = (select max(y.updated_date) from ocs_trn_tsanctionacceptlog y where y.application_gid = '" + dt["application_gid"].ToString() + "' ) " +
                    //" or  (updated_date is null ) )  and accepted_status ='N' ";
                    // string sanctionacceptlog_gid = objdbconn.GetExecuteScalar(msSQL);
                    // if (sanctionacceptlog_gid != "")
                    // {
                    //     edit_flag = "Y";
                    // }
                    // else
                    // {
                    //     edit_flag = "N";
                    // }

                    //lsemployeegid = "'" + employee_gid + "'";
                    get_sanctiondtl.Add(new sanctiondetails
                    {
                        application_gid = dt["application_gid"].ToString(),
                        application2sanction_gid = dt["application2sanction_gid"].ToString(),
                        sanction_refno = dt["sanction_refno"].ToString(),
                        sanction_date = dt["sanction_date"].ToString(),
                        sanction_amount = dt["sanction_amount"].ToString(),
                        customername = dt["customer_name"].ToString(),
                        created_by = dt["checkerupdated_by"].ToString(),
                        created_date = dt["checkerupdated_on"].ToString(),
                        checkerapproval_flag = dt["checkerapproval_flag"].ToString(),
                        checkerreject_remarks = dt["checkerreject_remarks"].ToString(),
                        sanction_status = dt["sanction_status"].ToString(),
                        //ccgroup_name = lsccgroup_name,
                        ccgroup_name = dt["ccgroup_name"].ToString(),
                        cadgroupname = dt["cadgroup_name"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        application_no = dt["application_no"].ToString(),
                        ccapproved_date = dt["ccapproved_date"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        approver_approveddate = dt["approver_approveddate"].ToString(),
                        renewal_flag = dt["renewal_flag"].ToString(),
                        enhancement_flag = dt["enhancement_flag"].ToString()

                    });
                }
                values.sanctiondetails = get_sanctiondtl;
            }
            dt_datatable.Dispose();

            return true;
        }

        public void DaPostDigitalSignature(string sanction_gid, string employee_gid, cadtemplate_list values)
        {
            msSQL = " SELECT template_content FROM ocs_trn_tapplication2sanction where application2sanction_gid='" + sanction_gid + "'";
            string lstemplatecontent = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " SELECT document_path FROM ocs_mst_tdigitalsignature where employee_gid='" + employee_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                lsdocument_path = HttpContext.Current.Server.MapPath("../../../" + (objODBCDataReader["document_path"].ToString()));
            }
            else
            {
                objODBCDataReader.Close();
                values.status = false;
                values.message = "Kindly Upload your Digital Signature in Master";
                return;
            }
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/SanctionLetterGenerationPDF/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                {
                    if ((!System.IO.Directory.Exists(values.lspath)))
                        System.IO.Directory.CreateDirectory(values.lspath);
                }

                values.lsname = "Sanction_Letter" + DateTime.Now.ToString("(dd-MM-yyyy HH-mm-ss)") + ".docx";
                values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/SanctionLetterGenerationPDF/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;

                string msdocument_gid = objcmnfunctions.GetMasterGID("UPLF");
                string lsfile_gid = msdocument_gid;

                // Save the HTML string as HTML File.
                string htmlFilePath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/SanctionLetterHTML";
                {
                    if ((!System.IO.Directory.Exists(htmlFilePath)))
                        System.IO.Directory.CreateDirectory(htmlFilePath);
                }

                htmlFilePath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/SanctionLetterHTML/sanctionletterdoc.html";


                File.WriteAllText(htmlFilePath, lstemplatecontent);

                Spire.Doc.Document document = new Spire.Doc.Document();

                document.LoadFromFile(htmlFilePath, Spire.Doc.FileFormat.Html, XHTMLValidationType.None);

                //Read Header and Footer File

                Document doc1 = new Document();
                doc1.LoadFromFile(ConfigurationManager.AppSettings["file_path"] + "/templates/headerfile_sanction.docx");
                HeaderFooter header = doc1.Sections[0].HeadersFooters.Header;
                HeaderFooter footer = doc1.Sections[0].HeadersFooters.Footer;
                Document doc2 = new Document(htmlFilePath);

                foreach (Section section in doc2.Sections)
                {
                    foreach (Paragraph paragraph in section.Paragraphs)
                    {
                        paragraph.Format.BeforeSpacing = 0;
                        paragraph.Format.AfterAutoSpacing = false;
                        paragraph.Format.AfterSpacing = 0;
                        paragraph.Format.BeforeAutoSpacing = false;
                        paragraph.Format.HorizontalAlignment = HorizontalAlignment.Justify;
                    }
                    foreach (DocumentObject obj in section.Body.ChildObjects)
                    {
                        if (obj is Paragraph)
                        {
                            var para = obj as Paragraph;
                            foreach (DocumentObject Pobj in para.ChildObjects)
                            {

                                if (Pobj is TextRange)
                                {
                                    TextRange textRange = Pobj as TextRange;
                                    textRange.CharacterFormat.FontName = "Calibri";
                                    textRange.CharacterFormat.FontSize = 10;
                                }
                            }
                        }
                    }
                    foreach (Spire.Doc.Table table in section.Tables)
                    {
                        foreach (Spire.Doc.TableRow row in table.Rows)
                        {
                            foreach (Spire.Doc.TableCell cell in row.Cells)
                            {
                                foreach (Paragraph p in cell.Paragraphs)
                                {
                                    foreach (DocumentObject Pobj in p.ChildObjects)
                                    {
                                        if (Pobj is TextRange)
                                        {
                                            TextRange textRange = Pobj as TextRange;
                                            textRange.CharacterFormat.FontName = "Calibri";
                                            textRange.CharacterFormat.FontSize = 10;
                                        }
                                    }
                                    p.Format.BeforeSpacing = 0;
                                    p.Format.AfterAutoSpacing = false;
                                    p.Format.AfterSpacing = 0;
                                    p.Format.BeforeAutoSpacing = false;
                                    p.Format.HorizontalAlignment = HorizontalAlignment.Justify;
                                }
                            }

                        }
                    }

                    foreach (DocumentObject obj in header.ChildObjects)
                    {
                        section.HeadersFooters.Header.ChildObjects.Add(obj.Clone());
                    }
                    foreach (DocumentObject obj in footer.ChildObjects)
                    {
                        //section.PageSetup.FooterDistance = 18;
                        section.HeadersFooters.Footer.ChildObjects.Add(obj.Clone());
                    }
                }

                // Document
                string lsprefilfile = HttpContext.Current.Server.MapPath("erpdocument" + "/" + lscompany_code + "/" + "TmpFile/Footer/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + lsfile_gid + "/");
                {
                    if ((!System.IO.Directory.Exists(lsprefilfile)))
                        System.IO.Directory.CreateDirectory(lsprefilfile);
                }
                lsprefilfile = lsprefilfile + values.lsname;
                doc2.SaveToFile(lsprefilfile, Spire.Doc.FileFormat.Docx);

                Document doc3 = new Document();
                doc3.LoadFromFile(lsprefilfile);
                //// Insert Footer Image
                HeaderFooter footer1 = doc3.Sections[0].HeadersFooters.Footer;
                Paragraph footerParagraph = footer1.AddParagraph();
                footerParagraph.AppendPicture(Image.FromFile(lsdocument_path));

                footerParagraph.Format.HorizontalAlignment = HorizontalAlignment.Right;
                //Loop through all paragraphs in the document
                //foreach (Section section in doc3.Sections)
                //{
                //    for (int i = 0; i < section.Body.ChildObjects.Count; i++)
                //    {
                //        if (section.Body.ChildObjects[i].DocumentObjectType == DocumentObjectType.HeaderFooter)
                //        {
                //            //Determine if the paragraph is a blank paragraph
                //            if (String.IsNullOrEmpty((section.Body.ChildObjects[i] as Paragraph).Text.Trim()))
                //            {
                //                //Remove blank paragraphs
                //                section.Body.ChildObjects.Remove(section.Body.ChildObjects[i]);
                //                i--;
                //            }
                //        }

                //    }
                //}
                //foreach (Section section in doc3.Sections)
                //{
                //    foreach (DocumentObject obj in footer1.ChildObjects)
                //    {
                //        Paragraph footerParagraph = footer1.AddParagraph();
                //        if (obj is DocPicture)
                //        {
                //            DocPicture picture = obj as DocPicture;
                //            picture.Width = 50f;
                //            picture.Height = 50f;
                //        }
                //        footerParagraph.AppendPicture(Image.FromFile(lsdocument_path));
                //        footerParagraph.Format.HorizontalAlignment = HorizontalAlignment.Right;
                //    }
                //    section.PageSetup.FooterDistance = 100;
                //}
                //// Inser Footer Page Number
                //HeaderFooter footer1 = doc3.Sections[0].HeadersFooters.Footer;
                //Paragraph footerParagraph1 = footer1.AddParagraph();
                //footerParagraph1.AppendField("page number", FieldType.FieldPage);
                //footerParagraph1.AppendText(" of ");
                //footerParagraph1.AppendField("number of pages", FieldType.FieldNumPages);
                //footerParagraph1.Format.HorizontalAlignment = HorizontalAlignment.Center;
                //// Inser WaterMark
                TextWatermark txtWatermark = new TextWatermark();
                //set the text watermark with text string, font, color and layout.
                txtWatermark.Text = "Samunnati";
                txtWatermark.FontSize = 23;
                txtWatermark.Color = Color.Gray;
                txtWatermark.Layout = WatermarkLayout.Diagonal;
                //add the text watermark
                doc3.Watermark = txtWatermark;
                //Protect Word
                doc3.Protect(ProtectionType.AllowOnlyReading, "Welcome@123");

                // Set Margin
                doc3.Sections[0].PageSetup.Margins.Left = 27.9f;
                doc3.Sections[0].PageSetup.Margins.Right = 27.9f;

                doc3.SaveToFile(values.lspath, Spire.Doc.FileFormat.Docx);
                File.Delete(htmlFilePath);

                msSQL = " update ocs_trn_tapplication2sanction set digitalsignature_flag='Y', makerfile_path='" + values.lspath + "', makerfile_name='" + values.lsname + "'" +
                       " where application2sanction_gid='" + sanction_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Digital Signature Uploaded Successfully";
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = ex.ToString();
            }
        }

        public void DaGetPDFGenerate(string sanction_gid, string employee_gid, cadtemplate_list values)
        {
            try
            {
                msSQL = " SELECT template_content FROM ocs_trn_tapplication2sanction where application2sanction_gid='" + sanction_gid + "'";
                string finalData = objdbconn.GetExecuteScalar(msSQL);

                //fileName = fileName.Replace(".html", ".pdf");

                //PdfDocument pdf = new PdfDocument();

                //PdfHtmlLayoutFormat htmlLayoutFormat = new PdfHtmlLayoutFormat();

                //htmlLayoutFormat.IsWaiting = false;

                //PdfPageSettings setting = new PdfPageSettings();

                //setting.Size = PdfPageSize.A2;

                //Thread thread = new Thread(() =>
                //{
                //    pdf.LoadFromHTML(finalData, true, setting, htmlLayoutFormat);
                //});

                //thread.SetApartmentState(ApartmentState.STA);

                //thread.Start();

                //thread.Join();

                msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                string lsreportabspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/HighmarkReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                {
                    if ((!System.IO.Directory.Exists(lsreportabspath)))
                        System.IO.Directory.CreateDirectory(lsreportabspath);
                }

                //lsreportabspath = lsreportabspath + fileName;

                //pdf.SaveToFile(lsreportabspath, Spire.Pdf.FileFormat.PDF);

                msSQL = " SELECT makerfile_path, makerfile_name FROM ocs_trn_tapplication2sanction where application2sanction_gid='" + sanction_gid + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows == true)
                {
                    values.lspath = objODBCDataReader["makerfile_path"].ToString();
                    values.lsname = objODBCDataReader["makerfile_name"].ToString();
                }
                objODBCDataReader.Close();

                values.lsname1 = "Sanction_Letter" + DateTime.Now.ToString("(dd-MM-yyyy HH-mm-ss)") + ".pdf";
                values.lspath1 = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/SanctionLetterGenerationPDF/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname1;
                string cloud_path = lscompany_code + "/" + "Master/SanctionLetterGenerationPDF/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname1;
                Spire.Doc.Document document = new Spire.Doc.Document();
                //values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + values.lspath;
                msSQL = "select company_document_flag from adm_mst_tcompany";
                string lscompany_document_flag = objdbconn.GetExecuteScalar(msSQL);
                Document doc4 = new Document();
                //doc4.LoadFromFile(values.lspath);
                bool status;
                MemoryStream ms = new MemoryStream();
                ms = objcmnstorage.DownloadStream("erpdocument", values.lspath, lscompany_document_flag);
                MemoryStream ms1 = new MemoryStream();
                //FileStream file = new FileStream(values.lspath, FileMode.Open, FileAccess.Read);
                //file.CopyTo(ms);
                //System.IO.Stream file_cloud_name = values.lsname1;
                //Convert Word to PDF
                //doc4.SaveToFile(values.lspath1, Spire.Doc.FileFormat.PDF);
                Document document1 = new Document();
                using (MemoryStream targetStream = new MemoryStream())
                {
                    document1.LoadFromStream(ms, Spire.Doc.FileFormat.Auto);
                    document1.SaveToStream(ms1, Spire.Doc.FileFormat.PDF);
                }
                //PdfDocument pdf = new PdfDocument();
                //pdf.LoadFromStream(ms);
                //pdf.SaveToStream(ms1, Spire.Pdf.FileFormat.PDF);
                //System.Diagnostics.Process.Start(values.lsname1);
                status = objcmnstorage.UploadStream("erpdocument", cloud_path, ms1);

                //doc4.SaveToStream(values.lspath1, Spire.Doc.FileFormat.PDF);
                values.lspath1 = objcmnstorage.EncryptData(cloud_path);
                values.status = true;
                values.message = "PDF downloaded Successfully";
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Failure";
            }
        }

        public bool DaSanctionLetterLogDownload(string sanctionapprovallog_gid, cadtemplate_list values)
        {
            msSQL = " select template_content from ocs_trn_tsanctionapprovallog where sanctionapprovallog_gid='" + sanctionapprovallog_gid + "'";
            string lstemplate_content = objdbconn.GetExecuteScalar(msSQL);

            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/SanctionLetterGeneration/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                {
                    if ((!System.IO.Directory.Exists(values.lspath)))
                        System.IO.Directory.CreateDirectory(values.lspath);
                }

                values.lsname = "Sanction_Letter" + DateTime.Now.ToString("(dd-MM-yyyy HH-mm-ss)") + ".docx";
                values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/SanctionLetterGeneration/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;

                lspath = HttpContext.Current.Server.MapPath("erpdocument" + "/" + lscompany_code + "/" + "Master/SanctionLetterGeneration/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/");
                string lsfile_gid = objcmnfunctions.GetMasterGID("UPLF");

                // Save the HTML string as HTML File.
                //string htmlFilePath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/SanctionLetterHTML/sanctionletterdoc.html";

                string htmlFilePath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/SanctionLetterHTML";
                {
                    if ((!System.IO.Directory.Exists(htmlFilePath)))
                        System.IO.Directory.CreateDirectory(htmlFilePath);
                }

                htmlFilePath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/SanctionLetterHTML/sanctionletterdoc.html";


                File.WriteAllText(htmlFilePath, lstemplate_content);

                Spire.Doc.Document document = new Spire.Doc.Document();

                document.LoadFromFile(htmlFilePath, Spire.Doc.FileFormat.Html, XHTMLValidationType.None);

                //Read Header and Footer File

                Document doc1 = new Document();
                doc1.LoadFromFile(ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "TmpFile/Logo/headerfile.docx");
                HeaderFooter header = doc1.Sections[0].HeadersFooters.Header;

                Document doc2 = new Document(htmlFilePath);

                foreach (Section section in doc2.Sections)
                {
                    foreach (DocumentObject obj in header.ChildObjects)
                    {
                        section.HeadersFooters.Header.ChildObjects.Add(obj.Clone());
                    }
                }

                // Document
                string lssanctionletterfile = HttpContext.Current.Server.MapPath("erpdocument" + "/" + lscompany_code + "/" + "TmpFile/Footer/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + lsfile_gid + "/");
                {
                    if ((!System.IO.Directory.Exists(lssanctionletterfile)))
                        System.IO.Directory.CreateDirectory(lssanctionletterfile);
                }
                lssanctionletterfile = lssanctionletterfile + values.lsname;
                doc2.SaveToFile(lssanctionletterfile, Spire.Doc.FileFormat.Docx);

                Document doc3 = new Document();
                doc3.LoadFromFile(lssanctionletterfile);
                // Inser Footer Number
                HeaderFooter footer = doc3.Sections[0].HeadersFooters.Footer;
                Paragraph footerParagraph = footer.AddParagraph();
                footerParagraph.AppendField("page number", FieldType.FieldPage);
                footerParagraph.AppendText(" of ");
                footerParagraph.AppendField("number of pages", FieldType.FieldNumPages);
                footerParagraph.Format.HorizontalAlignment = HorizontalAlignment.Center;

                // Set Margin
                doc3.Sections[0].PageSetup.Margins.Left = 27.9f;
                doc3.Sections[0].PageSetup.Margins.Right = 27.9f;

                doc3.SaveToFile(values.lspath, Spire.Doc.FileFormat.Docx);

                File.Delete(htmlFilePath);

                values.status = true;
                values.message = "Document Downloaded Successfully";
                return true;
            }
            catch
            {
                values.status = false;
                values.message = "Error Occurred";
                return true;
            }
        }

        public void DaUpdateCheckerApproval(cadtemplate_list values, string employee_gid)
        {
            if (values.sanction_status == "Approved")
            {
                msSQL = " update ocs_trn_tapplication2sanction set checkerapproval_flag='Y', sanction_status='" + values.sanction_status + "', checkerapproved_by='" + employee_gid + "',";
                if (values.reject_remarks == "" || values.reject_remarks == null || values.reject_remarks == "undefined")
                {
                    msSQL += " checkerreject_remarks='', ";
                }
                else
                {
                    msSQL += " checkerreject_remarks='" + values.reject_remarks.Replace("'", "") + "', ";
                }
                msSQL += " checkerapproved_on='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where application2sanction_gid='" + values.sanction_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else
            {
                msSQL = " update ocs_trn_tapplication2sanction set checkerapproval_flag='R', sanction_status='" + values.sanction_status + "', checkerapproved_by='" + employee_gid + "',";
                if (values.reject_remarks == "" || values.reject_remarks == null || values.reject_remarks == "undefined")
                {
                    msSQL += " checkerreject_remarks='', ";
                }
                else
                {
                    msSQL += " checkerreject_remarks='" + values.reject_remarks.Replace("'", "") + "', ";
                }
                msSQL += " checkerapproved_on='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where application2sanction_gid='" + values.sanction_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            if (mnResult != 0)
            {
                string lsprocesstypeassign_gid = "", lsapplication_gid = "";
                msSQL = " select a.processtypeassign_gid,a.application_gid from ocs_trn_tprocesstype_assign a " +
                      " left join ocs_trn_tapplication2sanction b on a.application_gid = b.application_gid " +
                      " where application2sanction_gid = '" + values.sanction_gid + "' and menu_gid ='" + getMenuClass.Sanction + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lsprocesstypeassign_gid = objODBCDatareader["processtypeassign_gid"].ToString();
                    lsapplication_gid = objODBCDatareader["application_gid"].ToString();
                }
                objODBCDatareader.Close();

                if (lsprocesstypeassign_gid != "" && values.sanction_status == "Approved")
                {
                    msSQL = " update ocs_trn_tprocesstype_assign set approver_approvalflag='Y', " +
                            " approver_approveddate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                            " overall_approvalstatus='Approved'  where processtypeassign_gid='" + lsprocesstypeassign_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update ocs_mst_tapplication set sanction_approvalflag='Y' " +
                            " where application_gid='" + lsapplication_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                msSQL = " select template_name, template_gid, template_content from ocs_trn_tapplication2sanction " +
                        " where application2sanction_gid='" + values.sanction_gid + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows == true)
                {
                    values.template_name = objODBCDataReader["template_name"].ToString();
                    values.template_gid = objODBCDataReader["template_gid"].ToString();
                    values.template_content = objODBCDataReader["template_content"].ToString();
                }
                objODBCDataReader.Close();

                msGetGid = objcmnfunctions.GetMasterGID("ASLL");
                msSQL = "insert into ocs_trn_tsanctionapprovallog(" +
                    " sanctionapprovallog_gid, " +
                    " sanction_gid," +
                    " template_gid, " +
                    " template_name, " +
                    " template_content, " +
                    " sanctionletter_flag," +
                    " sanction_status," +
                    " checkerpushback_remarks," +
                    " checkerreject_remarks," +
                    " created_by," +
                    " created_date)" +
                    " values (" +
                    "'" + msGetGid + "'," +
                    "'" + values.sanction_gid + "'," +
                    "'" + values.template_gid + "'," +
                    "'" + values.template_name + "'," +
                    "'" + values.template_content.Replace("'", "''") + "'," +
                    "'Y'," +
                    "'" + values.sanction_status + "'," +
                    "'',";
                if (values.reject_remarks == "" || values.reject_remarks == null || values.reject_remarks == "undefined")
                {
                    msSQL += " '', ";
                }
                else
                {
                    msSQL += "'" + values.reject_remarks.Replace("'", "") + "',";
                }
                msSQL += "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (values.sanction_status == "Approved")
                {
                    values.message = "Sanction Approved Successfully";
                    values.status = true;
                }
                else
                {
                    values.message = "Sanction Rejected Successfully";
                    values.status = true;
                }
            }
            else
            {
                values.message = "Error Occrued";
                values.status = false;
            }
        }

        public bool DaGetTemplateLogDetails(mdltemplate values, string sanctionapprovallog_gid, string sanction_gid)
        {
            msSQL = " select template_name, template_content, sanctionletter_flag, sanction_status" +
                    " from ocs_trn_tsanctionapprovallog " +
                    " where sanctionapprovallog_gid='" + sanctionapprovallog_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                values.template_name = objODBCDataReader["template_name"].ToString();
                values.template_content = objODBCDataReader["template_content"].ToString();
                values.sanction_status = objODBCDataReader["sanction_status"].ToString();
            }
            objODBCDataReader.Close();

            values.status = true;
            return true;
        }

        public bool DaGetTemplateDetails(mdltemplate values, string sanction_gid)
        {
            msSQL = " select sanctionletter_status, template_name, template_content, makerfile_name, makerfile_path, sanctionletter_flag, checkerapproval_flag," +
                    " checkerletter_flag, checkerpushback_remarks, digitalsignature_flag, date_format(checkerupdated_on, '%d-%m-%Y') as checkerupdated_on," +
                    " concat(c.user_firstname, ' ', c.user_lastname, '/', c.user_code) as checkerupdated_by, date_format(makersubmitted_on, '%d-%m-%Y') as makersubmitted_on," +
                    " concat(e.user_firstname, ' ', e.user_lastname, '/', e.user_code) as makersubmitted_by, " +
                    " f.approver_name as approved_by,date_format(f.approver_approveddate, '%d-%m-%Y') as approved_date " +
                    " from ocs_trn_tapplication2sanction a " +
                    " left join hrm_mst_temployee b on b.employee_gid = a.checkerupdated_by " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " left join hrm_mst_temployee d on d.employee_gid = a.makersubmitted_by " +
                    " left join adm_mst_tuser e on e.user_gid = d.user_gid " +
                    " left join ocs_trn_tprocesstype_assign f on f.application_gid = a.application_gid " +
                    " where application2sanction_gid='" + sanction_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                values.sanctionletter_status = objODBCDataReader["sanctionletter_status"].ToString();
                values.template_name = objODBCDataReader["template_name"].ToString();
                values.template_content = objODBCDataReader["template_content"].ToString();
                values.makerfile_name = objODBCDataReader["makerfile_name"].ToString();
                values.makerfile_path = objcmnstorage.EncryptData((objODBCDataReader["makerfile_path"].ToString()));
                values.sanctionletter_flag = objODBCDataReader["sanctionletter_flag"].ToString();
                values.checkerapproval_flag = objODBCDataReader["checkerapproval_flag"].ToString();
                values.checkerletter_flag = objODBCDataReader["checkerletter_flag"].ToString();
                values.checkerpushback_remarks = objODBCDataReader["checkerpushback_remarks"].ToString();
                values.digitalsignature_flag = objODBCDataReader["digitalsignature_flag"].ToString();
                values.checkerupdated_by = objODBCDataReader["checkerupdated_by"].ToString();
                values.checkerupdated_on = objODBCDataReader["checkerupdated_on"].ToString();
                values.makersubmitted_by = objODBCDataReader["makersubmitted_by"].ToString();
                values.makersubmitted_on = objODBCDataReader["makersubmitted_on"].ToString();
                values.approved_by = objODBCDataReader["approved_by"].ToString();
                values.approved_date = objODBCDataReader["approved_date"].ToString();
            }
            objODBCDataReader.Close();

            values.status = true;
            return true;
        }

        public void DaCADAppSanctionCount(string user_gid, string employee_gid, CadSanctionCount values)
        {
            msSQL = " select count(application_gid) as cadsanction_count from ocs_mst_tapplication a " +
                    " where a.process_type = 'Accept'";
            values.cadmaker_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select count(application2sanction_gid) as cadchecker_count from ocs_trn_tapplication2sanction a " +
                     " where a.sanctionletter_flag='Y' and a.checkerletter_flag='N' ";
            values.cadchecker_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select count(application2sanction_gid) as cadcheckerapproval_count from ocs_trn_tapplication2sanction a " +
                     " where a.checkerletter_flag='Y'";
            values.cadcheckerapproval_count = objdbconn.GetExecuteScalar(msSQL);
        }

        public void DaGetCADLoanFacilityTemplateList(string sanction_gid, Mdlloanfacility_type values)
        {
            //msSQL = " select count(*) as count from ocs_mst_tapplication2loan a " +
            //        " left join ocs_trn_tapplication2sanction b on a.application_gid = b.application_gid " +
            //        " where application2sanction_gid = '" + sanction_gid + "'";
            //int lscount = Convert.ToInt16(objdbconn.GetExecuteScalar(msSQL));
            string lstemplate_name = "", lstemplate_gid = "";
            //if (lscount == 1)
            //{
            //    msSQL = " select template_gid,template_name from adm_mst_ttemplate where template_name ='Sanction - Simplified Norms Single Facility'";
            //    objODBCDataReader = objdbconn.GetDataReader(msSQL);
            //    if (objODBCDataReader.HasRows == true)
            //    {
            //        lstemplate_gid = objODBCDataReader["template_gid"].ToString();
            //        lstemplate_name = objODBCDataReader["template_name"].ToString();
            //    }
            //    objODBCDataReader.Close();

            //}
            //else
            //{
            //    msSQL = " select template_gid,template_name from adm_mst_ttemplate where template_name ='Sanction - Multiple Facility'";
            //    objODBCDataReader = objdbconn.GetDataReader(msSQL);
            //    if (objODBCDataReader.HasRows == true)
            //    {
            //        lstemplate_gid = objODBCDataReader["template_gid"].ToString();
            //        lstemplate_name = objODBCDataReader["template_name"].ToString();
            //    }
            //    objODBCDataReader.Close(); 
            //}
            msSQL = " select template_gid,template_name from ocs_mst_ttemplate where template_gid ='OCTE2023011915'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                lstemplate_gid = objODBCDataReader["template_gid"].ToString();
                lstemplate_name = objODBCDataReader["template_name"].ToString();
            }
            objODBCDataReader.Close();

            msSQL = " update ocs_trn_tapplication2sanction set template_name='" + lstemplate_name + "', template_gid='" + lstemplate_gid + "' where application2sanction_gid='" + sanction_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            msSQL = "select templateinputdtl_gid from ocs_mst_ttemplateinputdtl where template_gid='" + lstemplate_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    string msGetTrnGid = objcmnfunctions.GetMasterGID("TTIG");
                    msSQL = " insert into ocs_trn_ttemplateinputdtl (trntemplateinputdtl_gid, templateinputdtl_gid,  " +
                     " templatetype_gid, templatetype_name, template_gid, input_fieldid, input_fieldname, " +
                     " inputgroup_gid, inputgroup_name, input_type, input_placeholder, inputmax_length, " +
                     " input_mandatory, input_previewtext, created_by, created_date) " +
                     " (select '" + msGetTrnGid + "',templateinputdtl_gid, '" + sanction_gid + "', '" + lstemplate_name + "', " +
                     " template_gid, input_fieldid, input_fieldname, " +
                     " inputgroup_gid, inputgroup_name, input_type, input_placeholder, inputmax_length, " +
                     " input_mandatory,input_previewtext, created_by, created_date " +
                     " from ocs_mst_ttemplateinputdtl where templateinputdtl_gid='" + dt["templateinputdtl_gid"].ToString() + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " insert into ocs_trn_ttemplateinputlistdtl (templateinputlistdtl_gid, trntemplateinputdtl_gid,  " +
                    " templatetype_gid, input_fieldid, input_type, input_previewtext, input_value, " +
                    " created_by, created_date) " +
                    " (select  templateinputlistdtl_gid, '" + msGetTrnGid + "', '" + sanction_gid + "', input_fieldid , " +
                    " input_type, input_previewtext, input_value, created_by, created_date " +
                    " from ocs_mst_ttemplateinputlistdtl where templateinputdtl_gid='" + dt["templateinputdtl_gid"].ToString() + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            dt_datatable.Dispose();
        }

        public bool DaGetDocumentTypeList(string credit_gid, string application_gid, MdlMstCADCompany values)
        {

            //string program_gid = "";

            //msSQL = " select program_gid from ocs_mst_tapplication where application_gid = '" + application_gid + "' ";

            //program_gid = objdbconn.GetExecuteScalar(msSQL);

            //msSQL = " select a.companydocument_gid,a.documenttypes_gid,a.documenttype_name,a.companydocument_name,a.covenant_type " +
            //        " from ocs_mst_tcompanydocument a " +
            //        " left join ocs_mst_tcompanydocumentprogram b on a.companydocument_gid = b.companydocument_gid " +
            //        " where a.status='Y' and b.program_gid = '" + program_gid + "' " +
            //        " and a.companydocument_gid not in (select companydocument_gid " +
            //        " from ocs_trn_tdocumentchecktls where credit_gid = '" + credit_gid + "' and untagged_type is null group by companydocument_gid)";


            msSQL = " SELECT companydocument_gid,companydocument_name,documenttypes_gid,documenttype_name,covenant_type " +
                   " FROM ocs_mst_tcompanydocument where status='Y' and companydocument_gid not in (select companydocument_gid " +
                   " from ocs_trn_tdocumentchecktls where credit_gid = '" + credit_gid + "' and untagged_type is null group by companydocument_gid)";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getDocList = new List<CADDocument>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getDocList.Add(new CADDocument
                    {
                        document_gid = dt["companydocument_gid"].ToString(),
                        documenttype_gid = dt["documenttypes_gid"].ToString(),
                        document_name = dt["companydocument_name"].ToString(),
                        documenttype_name = dt["documenttype_name"].ToString(),
                        covenant_type = dt["covenant_type"].ToString(),
                    });
                }
                values.CADDocument = getDocList;
                values.status = true;
                values.message = "Data Fetched";
            }
            else
            {
                values.status = false;
                values.message = "No Record Found";
            }
            dt_datatable.Dispose();

            return true;
        }

        public void DaPostDocumentCheckList(MdlMstCAD values, string user_gid, string employee_gid)
        {
            string lsStackholdertype;
            msSQL = " select stakeholder_type from ocs_mst_tinstitution where institution_gid = '" + values.credit_gid + "'";

            lsStackholdertype = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select institution_gid from ocs_mst_tinstitution where institution_gid='" + values.credit_gid + "'";
            string lsinstitution = objdbconn.GetExecuteScalar(msSQL);
            msSQL = "select contact_gid from ocs_mst_tcontact where contact_gid='" + values.credit_gid + "'";
            string lsindividual = objdbconn.GetExecuteScalar(msSQL);
            msSQL = "select group_gid from ocs_mst_tgroup where group_gid='" + values.credit_gid + "'";
            string lsgroup = objdbconn.GetExecuteScalar(msSQL);
            bool lsgroupupdate = false;

            foreach (string i in values.document_gid)
            {
                string lscovenant_type = "", lscompanydocument_gid = "", lsindividualdocument_gid = "", lsgroupdocument_gid = "";

                msGetGID = objcmnfunctions.GetMasterGID("DOCG");
                if (lsinstitution != "")
                {
                    msSQL = " select companydocument_gid,documenttypes_gid,documenttype_name,companydocument_name,covenant_type " +
                            " from ocs_mst_tcompanydocument where companydocument_gid='" + i + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lsdocumenttype_gid = objODBCDatareader["documenttypes_gid"].ToString();
                        lsdocumenttype_name = objODBCDatareader["documenttype_name"].ToString();
                        lscompanydocument_name = objODBCDatareader["companydocument_name"].ToString();
                        lscovenant_type = objODBCDatareader["covenant_type"].ToString();
                        lscompanydocument_gid = objODBCDatareader["companydocument_gid"].ToString();
                    }
                    objODBCDatareader.Close();

                    msSQL = " select groupdocumentchecklist_gid from ocs_trn_tdocumentchecktls where application_gid='" + values.application_gid + "'" +
                      " and credit_gid='" + values.credit_gid + "' and companydocument_gid ='" + lscompanydocument_gid + "'";
                }
                else if (lsindividual != "")
                {
                    msSQL = " select individualdocument_gid,documenttypes_gid,documenttype_name,individualdocument_name,covenant_type " +
                          " from ocs_mst_tindividualdocument where individualdocument_gid='" + i + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lsdocumenttype_gid = objODBCDatareader["documenttypes_gid"].ToString();
                        lsdocumenttype_name = objODBCDatareader["documenttype_name"].ToString();
                        lscompanydocument_name = objODBCDatareader["individualdocument_name"].ToString();
                        lscovenant_type = objODBCDatareader["covenant_type"].ToString();
                        lsindividualdocument_gid = objODBCDatareader["individualdocument_gid"].ToString();
                    }
                    objODBCDatareader.Close();

                    msSQL = " select groupdocumentchecklist_gid from ocs_trn_tdocumentchecktls where application_gid='" + values.application_gid + "'" +
                      " and credit_gid='" + values.credit_gid + "' and individualdocument_gid ='" + lsindividualdocument_gid + "'";
                }
                else if (lsgroup != "")
                {
                    msSQL = " select groupdocument_gid,documenttypes_gid,documenttype_name,groupdocument_name,covenant_type " +
                         " from ocs_mst_tgroupdocument where groupdocument_gid='" + i + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lsdocumenttype_gid = objODBCDatareader["documenttypes_gid"].ToString();
                        lsdocumenttype_name = objODBCDatareader["documenttype_name"].ToString();
                        lscompanydocument_name = objODBCDatareader["groupdocument_name"].ToString();
                        lscovenant_type = objODBCDatareader["covenant_type"].ToString();
                        lsgroupdocument_gid = objODBCDatareader["groupdocument_gid"].ToString();
                    }
                    objODBCDatareader.Close();
                    msSQL = " select groupdocumentchecklist_gid from ocs_trn_tdocumentchecktls where application_gid='" + values.application_gid + "'" +
                      " and credit_gid='" + values.credit_gid + "' and groupdocument_gid ='" + lsgroupdocument_gid + "'";
                }
                string lsgroupdocumentchecklist_gid = objdbconn.GetExecuteScalar(msSQL);
                msSQL = "select concat(user_firstname,' ', user_lastname, ' / ', user_code) as username from adm_mst_tuser where user_gid='" + user_gid + "'";
                string tagged_name = objdbconn.GetExecuteScalar(msSQL);
                if (lsgroupdocumentchecklist_gid != "")
                {
                    msSQL = " update ocs_trn_tdocumentchecktls set untagged_type=null, " +
                   " untagged_by='" + user_gid + "', " +
                   " tagged_name='" + tagged_name + "'," +
                   " tagged_by='" + values.taggedby + "'," +
                   " untagged_date=current_timestamp" +
                   " where groupdocumentchecklist_gid='" + lsgroupdocumentchecklist_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update ocs_trn_tgroupdocumentchecklist set untagged_type=null, " +
                            " tagged_by = '" + values.taggedby + "' where groupdocumentchecklist_gid='" + lsgroupdocumentchecklist_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (lsStackholdertype == "Guarantor")
                    {
                        msSQL = "update ocs_trn_tdocumentchecktls set com_gur_flag = 'Y' " +
                           " where groupdocumentchecklist_gid = '" + lsgroupdocumentchecklist_gid + "'";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }

                    if (lsStackholdertype == "Member")
                    {
                        msSQL = "update ocs_trn_tdocumentchecktls set com_mem_flag = 'Y' " +
                           " where groupdocumentchecklist_gid = '" + lsgroupdocumentchecklist_gid + "'";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                else
                {
                    msSQL = " insert into ocs_trn_tdocumentchecktls(" +
                   " documentcheckdtl_gid," +
                   " application_gid," +
                   " credit_gid, ";
                    if (lsinstitution != "")
                        msSQL += " companydocument_gid, ";
                    else if (lsindividual != "")
                        msSQL += " individualdocument_gid, ";
                    else
                    {
                        msSQL += " groupdocument_gid,";
                    }
                    msSQL += " documenttype_gid," +
                        " documenttype_code," +
                        " documenttype_name," +
                        " covenant_type, " +
                        " tagged_by, " +
                        " tagged_name, " +
                        " created_date," +
                        " created_by)" +
                        " VALUES(" +
                        "'" + msGetGID + "'," +
                        "'" + values.application_gid + "'," +
                        "'" + values.credit_gid + "',";
                    if (lsinstitution != "")
                        msSQL += "'" + lscompanydocument_gid + "',";
                    else if (lsindividual != "")
                        msSQL += "'" + lsindividualdocument_gid + "',";
                    else
                    {
                        msSQL += "'" + lsgroupdocument_gid + "',";
                    }
                    msSQL += "'" + lsdocumenttype_gid + "'," +
                        "'" + lsdocumenttype_name + "'," +
                        "'" + lscompanydocument_name.Replace("'", "") + "'," +
                        "'" + lscovenant_type + "'," +
                        "'" + values.taggedby + "'," +
                        "'" + tagged_name + "'," +
                        "current_timestamp," +
                        "'" + user_gid + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    lsgroupupdate = true;


                    if (lsStackholdertype == "Guarantor")
                    {
                        msSQL = "update ocs_trn_tdocumentchecktls set com_gur_flag = 'Y' " +
                           " where application_gid = '" + values.application_gid + "' and " +
                           " credit_gid = '" + values.credit_gid + "' and documentcheckdtl_gid = '" + msGetGID + "'";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }

                    if (lsStackholdertype == "Member")
                    {
                        msSQL = "update ocs_trn_tdocumentchecktls set com_mem_flag = 'Y' " +
                           " where application_gid = '" + values.application_gid + "' and " +
                           " credit_gid = '" + values.credit_gid + "' and documentcheckdtl_gid = '" + msGetGID + "'";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
            }
            if (lsgroupupdate)
            {
                DaMstScannedDocument objvalues = new DaMstScannedDocument();
                objvalues.DaGroupDocChecklistinfoCredit(values.application_gid, values.credit_gid, employee_gid);
            }
            if (mnResult == 1)
            {
                values.status = true;
                values.message = "Document Type Tagged Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
            }
        }

        //Individual Document
        public bool DaGetIndividualTypeList(MdlMstCAD values, string credit_gid, string application_gid)
        {

            //string program_gid = "";

            //msSQL = " select program_gid from ocs_mst_tapplication where application_gid = '" + application_gid + "' ";

            //program_gid = objdbconn.GetExecuteScalar(msSQL);

            //msSQL = " select a.individualdocument_gid, a.documenttypes_gid, a.documenttype_name, a.individualdocument_name, a.covenant_type " +
            //       " from ocs_mst_tindividualdocument a " +
            //       " left join ocs_mst_tindividualdocumentprogram b on a.individualdocument_gid = b.individualdocument_gid " +
            //       " where a.status='Y' and b.program_gid = '" + program_gid + "' and a.individualdocument_gid not in (select individualdocument_gid " +
            //       " from ocs_trn_tdocumentchecktls where credit_gid = '" + credit_gid + "' and untagged_type is null  group by individualdocument_gid)";


            msSQL = " SELECT individualdocument_gid,individualdocument_name,documenttypes_gid,documenttype_name,covenant_type " +
                   " FROM ocs_mst_tindividualdocument where status='Y' and individualdocument_gid not in (select individualdocument_gid " +
                  " from ocs_trn_tdocumentchecktls where credit_gid = '" + credit_gid + "' and untagged_type is null group by individualdocument_gid)";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getDocList = new List<CADDocument>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getDocList.Add(new CADDocument
                    {
                        document_gid = dt["individualdocument_gid"].ToString(),
                        documenttype_gid = dt["documenttypes_gid"].ToString(),
                        document_name = dt["individualdocument_name"].ToString(),
                        documenttype_name = dt["documenttype_name"].ToString(),
                        covenant_type = dt["covenant_type"].ToString(),
                    });
                }
                values.CADDocument = getDocList;
                values.status = true;
                values.message = "Data Fetched";
            }
            else
            {
                values.status = false;
                values.message = "No Record Found";
            }
            dt_datatable.Dispose();

            return true;
        }

        public bool DaGetCovenantIndividualDocumentList(MdlMstCADCompany values, string credit_gid, string application_gid)
        {

            string program_gid = "";

            msSQL = " select program_gid from ocs_mst_tapplication where application_gid = '" + application_gid + "' ";

            program_gid = objdbconn.GetExecuteScalar(msSQL);

            if (credit_gid != "")
            {
                msSQL = " SELECT individualdocument_gid,individualdocument_name,documenttypes_gid,documenttype_name,covenant_type " +
                  " FROM ocs_mst_tindividualdocument where status='Y' and covenant_type='Y' and individualdocument_gid not in (select individualdocument_gid " +
                  " from ocs_trn_tcovanantdocumentcheckdtls where credit_gid = '" + credit_gid + "' and untagged_type is null group by individualdocument_gid)";

                //msSQL = " select a.individualdocument_gid, a.documenttypes_gid, a.documenttype_name, a.individualdocument_name, a.covenant_type " +
                //       " from ocs_mst_tindividualdocument a " +
                //       " left join ocs_mst_tindividualdocumentprogram b on a.individualdocument_gid = b.individualdocument_gid " +
                //       " where a.status='Y' and b.program_gid = '" + program_gid + "' and covenant_type='Y' and a.individualdocument_gid not in (select individualdocument_gid " +
                //       " from ocs_trn_tdocumentchecktls where credit_gid = '" + credit_gid + "' and untagged_type is null  group by individualdocument_gid)";

            }
            else
            {
                msSQL = " SELECT individualdocument_gid,individualdocument_name,documenttypes_gid,documenttype_name,covenant_type " +
                " FROM ocs_mst_tindividualdocument where status='Y' and covenant_type='Y'";

           //     msSQL = " select a.individualdocument_gid, a.documenttypes_gid, a.documenttype_name, a.individualdocument_name, a.covenant_type " +
           //" from ocs_mst_tindividualdocument a " +
           //" left join ocs_mst_tindividualdocumentprogram b on a.individualdocument_gid = b.individualdocument_gid " +
           //" where a.status='Y' and b.program_gid = '" + program_gid + "'  and a.covenant_type = 'Y' ";

            }

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getDocList = new List<CADDocument>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getDocList.Add(new CADDocument
                    {
                        document_gid = dt["individualdocument_gid"].ToString(),
                        documenttype_gid = dt["documenttypes_gid"].ToString(),
                        document_name = dt["individualdocument_name"].ToString(),
                        documenttype_name = dt["documenttype_name"].ToString(),
                        covenant_type = dt["covenant_type"].ToString(),
                    });
                }
                values.CADDocument = getDocList;
                values.status = true;
                values.message = "Data Fetched";
            }
            else
            {
                values.status = false;
                values.message = "No Record Found";
            }
            dt_datatable.Dispose();

            return true;
        }
        public void DaApplyALLDocumentList(MdlMstCAD values, string user_gid, string employee_gid)
        {
            string lscovenant_type = "", lscompanydocument_gid = "", lsindividualdocument_gid = "", lsgroupdocument_gid = "";

            List<string> DocumentList = new List<string>();


            string documenttype_gid = "", documenttype_code = "", documenttype_name = "", groupdocumentchecklist_gid = "", documentuploaded_gid = "";
            string lsStackholdertype;

            msSQL = "select concat(user_firstname,' ', user_lastname, ' / ', user_code) as username from adm_mst_tuser where user_gid='" + user_gid + "'";
            string tagged_name = objdbconn.GetExecuteScalar(msSQL);


            if (values.applicant_type == "Institution")
            {
                //values.application_gid = "APPC20220628489";

                msSQL = " select stakeholder_type from ocs_mst_tinstitution where institution_gid = '" + values.credit_gid + "'";

                lsStackholdertype = objdbconn.GetExecuteScalar(msSQL);

                if (lsStackholdertype == "Guarantor")
                {
                    msSQL = " select institution_gid from ocs_mst_tinstitution where application_gid = '" + values.application_gid + "'" +
                   " and  stakeholder_type = 'Guarantor'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable.Rows.Count != 0)
                    {

                        msSQL = " select group_concat(distinct companydocument_gid) " +
                                " from ocs_trn_tdocumentchecktls where  " +
                                " application_gid = '" + values.application_gid + "'" +
                                " and credit_gid = '" + values.credit_gid + "' " +
                                // " and companyalldoc_flag <> 'Y' " +
                                " and com_gur_flag = 'Y' and untagged_type is null";


                        DocList = objdbconn.GetExecuteScalar(msSQL);
                        DocumentList = DocList.Split(',').ToList();

                        msSQL = " select group_concat(distinct companydocument_gid) " +
                               " from ocs_trn_tdocumentchecktls where  " +
                               " application_gid = '" + values.application_gid + "'" +
                               " and credit_gid = '" + values.credit_gid + "' " +
                               // " and companyalldoc_flag <> 'Y' " +
                               " and com_gur_flag = 'Y' and untagged_type is not null";


                        DocUntagList = objdbconn.GetExecuteScalar(msSQL);

                    };
                    if ((DocList != "") || DocUntagList != "")
                    {

                        foreach (DataRow dt in dt_datatable.Rows)
                        {


                            // Applying newly added records

                            List<string> list = new List<string>();
                            List<string> toRemove = new List<string>();
                            List<string> FinalList = new List<string>();
                            List<string> CreditDocList = new List<string>();
                            List<string> DocumentUntagList = new List<string>();

                            CreditList = string.Empty;

                            msSQL = " select group_concat(distinct companydocument_gid) " +
                                " from ocs_trn_tdocumentchecktls where  " +
                                " application_gid = '" + values.application_gid + "'" +
                                " and credit_gid = '" + dt["institution_gid"].ToString() + "'";

                            CreditList = objdbconn.GetExecuteScalar(msSQL);
                            if (CreditList.Any(",".Contains))
                            {
                                CreditDocList = CreditList.Split(',').ToList();
                            }
                            else { CreditDocList.Add(CreditList); }

                            list = DocumentList;
                            toRemove = CreditDocList;
                            FinalList = list.Except(toRemove).ToList();
                            DocumentUntagList = list.Intersect(toRemove).ToList();

                            // Applying already untagged doc to tagged doc

                            if (DocumentUntagList != null)
                            {
                                foreach (string UntagListitem in DocumentUntagList)
                                {
                                    msSQL = " select* from ocs_trn_tdocumentchecktls " +
                                             " where  application_gid = '" + values.application_gid + "'" +
                                             " and credit_gid = '" + dt["institution_gid"].ToString() + "' and " +
                                             " companydocument_gid='" + UntagListitem + "' and untagged_type is not null ";
                                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                    if (objODBCDatareader.HasRows == true)
                                    {
                                        msSQL = "update ocs_trn_tdocumentchecktls set " +
                                             " untagged_type = null where  application_gid = '" + values.application_gid + "'" +
                                             " and credit_gid = '" + dt["institution_gid"].ToString() + "' and " +
                                             " companydocument_gid='" + UntagListitem + "'";

                                        mnResultUntag = objdbconn.ExecuteNonQuerySQL(msSQL);
                                    }
                                    objODBCDatareader.Close();
                                }
                            }

                            if (FinalList != null)
                            {
                                bool lsgroupupdate = false;

                                foreach (string Listitem in FinalList)
                                {
                                    msSQL = " select companydocument_gid,documenttypes_gid,documenttype_name,companydocument_name,covenant_type " +
                                               " from ocs_mst_tcompanydocument where companydocument_gid='" + Listitem + "'";
                                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                    if (objODBCDatareader.HasRows == true)
                                    {
                                        lsdocumenttype_gid = objODBCDatareader["documenttypes_gid"].ToString();
                                        lsdocumenttype_name = objODBCDatareader["documenttype_name"].ToString();
                                        lscompanydocument_name = objODBCDatareader["companydocument_name"].ToString();
                                        lscovenant_type = objODBCDatareader["covenant_type"].ToString();
                                        lscompanydocument_gid = objODBCDatareader["companydocument_gid"].ToString();
                                    }
                                    objODBCDatareader.Close();

                                    msGetGID = objcmnfunctions.GetMasterGID("DOCG");

                                    msSQL = " insert into ocs_trn_tdocumentchecktls(" +
                                            " documentcheckdtl_gid," +
                                            " application_gid," +
                                            " credit_gid, " +
                                            " companydocument_gid, " +
                                            " documenttype_gid," +
                                            " documenttype_code," +
                                            " documenttype_name," +
                                            " covenant_type, " +
                                            " tagged_by, " +
                                            " tagged_name, " +
                                            " created_date," +
                                            " created_by,com_gur_flag,untagged_type)" +
                                            " VALUES(" +
                                            "'" + msGetGID + "'," +
                                            "'" + values.application_gid + "'," +
                                            "'" + dt["institution_gid"].ToString() + "'," +
                                            "'" + lscompanydocument_gid + "'," +
                                            "'" + lsdocumenttype_gid + "'," +
                                            "'" + lsdocumenttype_name + "'," +
                                            "'" + lscompanydocument_name.Replace("'", "") + "'," +
                                            "'" + lscovenant_type + "'," +
                                            "'" + values.taggedby + "'," +
                                            "'" + tagged_name + "'," +
                                            "current_timestamp," +
                                            "'" + user_gid + "','Y',null)";

                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                                    DaMstScannedDocument objvalues = new DaMstScannedDocument();
                                    objvalues.DaGroupDocChecklistinfoCredit(values.application_gid, dt["institution_gid"].ToString(), employee_gid);


                                }
                                dt_datatable.Dispose();

                            }

                        }
                        if ((mnResult == 1) || (mnResultUntag == 1))
                        {
                            values.status = true;
                            values.message = "Document type checklist applied successfully to all Guarantors ";
                        }
                        else
                        {
                            values.status = false;
                            values.message = "There is No Document type checklist to apply for Guarantors";
                        }
                    }
                }
                else if (lsStackholdertype == "Member")
                {
                    msSQL = " select institution_gid from ocs_mst_tinstitution where application_gid = '" + values.application_gid + "'" +
                                       " and  stakeholder_type ='Member'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable.Rows.Count != 0)
                    {
                        msSQL = " select group_concat(distinct companydocument_gid) " +
                                " from ocs_trn_tdocumentchecktls where  " +
                                " application_gid = '" + values.application_gid + "'" +
                                " and credit_gid = '" + values.credit_gid + "' " +
                                " and com_mem_flag = 'Y' and untagged_type is null";


                        DocList = objdbconn.GetExecuteScalar(msSQL);
                        DocumentList = DocList.Split(',').ToList();

                        msSQL = " select group_concat(distinct companydocument_gid) " +
                              " from ocs_trn_tdocumentchecktls where  " +
                              " application_gid = '" + values.application_gid + "'" +
                              " and credit_gid = '" + values.credit_gid + "' " +
                              " and com_mem_flag = 'Y' and untagged_type is not null";


                        DocUntagList = objdbconn.GetExecuteScalar(msSQL);
                    };
                    if ((DocList != "") || DocUntagList != "")
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            // Applying newly added records
                            List<string> list = new List<string>();
                            List<string> toRemove = new List<string>();
                            List<string> FinalList = new List<string>();
                            List<string> CreditDocList = new List<string>();
                            List<string> DocumentUntagList = new List<string>();
                            CreditList = string.Empty;

                            msSQL = " select group_concat(distinct companydocument_gid) " +
                                " from ocs_trn_tdocumentchecktls where  " +
                                " application_gid = '" + values.application_gid + "'" +
                                " and credit_gid = '" + dt["institution_gid"].ToString() + "'";

                            CreditList = objdbconn.GetExecuteScalar(msSQL);
                            if (CreditList.Any(",".Contains))
                            {
                                CreditDocList = CreditList.Split(',').ToList();
                            }
                            else { CreditDocList.Add(CreditList); }

                            list = DocumentList;
                            toRemove = CreditDocList;
                            FinalList = list.Except(toRemove).ToList();
                            DocumentUntagList = list.Intersect(toRemove).ToList();

                            // Applying already untagged doc to tagged doc

                            if (DocumentUntagList != null)
                            {
                                foreach (string UntagListitem in DocumentUntagList)
                                {
                                    msSQL = " select* from ocs_trn_tdocumentchecktls " +
                                            " where  application_gid = '" + values.application_gid + "'" +
                                            " and credit_gid = '" + dt["institution_gid"].ToString() + "' and " +
                                            " companydocument_gid='" + UntagListitem + "' and untagged_type is not null ";
                                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                    if (objODBCDatareader.HasRows == true)
                                    {
                                        msSQL = "update ocs_trn_tdocumentchecktls set " +
                                             " untagged_type = null where  application_gid = '" + values.application_gid + "'" +
                                             " and credit_gid = '" + dt["institution_gid"].ToString() + "' and " +
                                             " companydocument_gid='" + UntagListitem + "'";

                                        mnResultUntag = objdbconn.ExecuteNonQuerySQL(msSQL);
                                    }
                                    objODBCDatareader.Close();

                                }
                            }
                            if (FinalList != null)
                            {
                                foreach (string Listitem in FinalList)
                                {
                                    msSQL = " select companydocument_gid,documenttypes_gid,documenttype_name,companydocument_name,covenant_type " +
                                               " from ocs_mst_tcompanydocument where companydocument_gid='" + Listitem + "'";
                                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                    if (objODBCDatareader.HasRows == true)
                                    {
                                        lsdocumenttype_gid = objODBCDatareader["documenttypes_gid"].ToString();
                                        lsdocumenttype_name = objODBCDatareader["documenttype_name"].ToString();
                                        lscompanydocument_name = objODBCDatareader["companydocument_name"].ToString();
                                        lscovenant_type = objODBCDatareader["covenant_type"].ToString();
                                        lscompanydocument_gid = objODBCDatareader["companydocument_gid"].ToString();
                                    }
                                    objODBCDatareader.Close();

                                    msGetGID = objcmnfunctions.GetMasterGID("DOCG");

                                    msSQL = " insert into ocs_trn_tdocumentchecktls(" +
                                            " documentcheckdtl_gid," +
                                            " application_gid," +
                                            " credit_gid, " +
                                            " companydocument_gid, " +
                                            " documenttype_gid," +
                                            " documenttype_code," +
                                            " documenttype_name," +
                                            " covenant_type, " +
                                            " tagged_by, " +
                                            " tagged_name, " +
                                            " created_date," +
                                            " created_by,com_mem_flag,untagged_type)" +
                                            " VALUES(" +
                                            "'" + msGetGID + "'," +
                                            "'" + values.application_gid + "'," +
                                            "'" + dt["institution_gid"].ToString() + "'," +
                                            "'" + lscompanydocument_gid + "'," +
                                            "'" + lsdocumenttype_gid + "'," +
                                            "'" + lsdocumenttype_name + "'," +
                                            "'" + lscompanydocument_name.Replace("'", "") + "'," +
                                            "'" + lscovenant_type + "'," +
                                            "'" + values.taggedby + "'," +
                                            "'" + tagged_name + "'," +
                                            "current_timestamp," +
                                            "'" + user_gid + "','Y',null)";

                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                                    DaMstScannedDocument objvalues = new DaMstScannedDocument();
                                    objvalues.DaGroupDocChecklistinfoCredit(values.application_gid, dt["institution_gid"].ToString(), employee_gid);

                                }
                            }
                            dt_datatable.Dispose();

                        }

                    }
                    if ((mnResult == 1) || (mnResultUntag == 1))
                    {
                        values.status = true;
                        values.message = "Document type checklist applied successfully to all Members ";
                    }
                    else
                    {
                        values.status = false;
                        values.message = "There is No Document type checklist to apply for Members";
                    }

                }

            }
            else if (values.applicant_type == "Individual")
            {
                //values.application_gid = "APPC20220628489";

                msSQL = " select stakeholder_type from ocs_mst_tcontact where contact_gid = '" + values.credit_gid + "'";

                lsStackholdertype = objdbconn.GetExecuteScalar(msSQL);

                if (lsStackholdertype == "Guarantor")
                {
                    msSQL = " select contact_gid from ocs_mst_tcontact where application_gid = '" + values.application_gid + "'" +
                   " and  stakeholder_type = 'Guarantor'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable.Rows.Count != 0)
                    {

                        msSQL = " select group_concat(distinct individualdocument_gid) " +
                                " from ocs_trn_tdocumentchecktls where  " +
                                " application_gid = '" + values.application_gid + "'" +
                                " and credit_gid = '" + values.credit_gid + "' " +
                                " and ind_gur_flag = 'Y' and untagged_type is null";


                        DocList = objdbconn.GetExecuteScalar(msSQL);
                        DocumentList = DocList.Split(',').ToList();

                        msSQL = " select group_concat(distinct individualdocument_gid) " +
                             " from ocs_trn_tdocumentchecktls where  " +
                             " application_gid = '" + values.application_gid + "'" +
                             " and credit_gid = '" + values.credit_gid + "' " +
                             " and ind_gur_flag = 'Y' and untagged_type is not null";


                        DocUntagList = objdbconn.GetExecuteScalar(msSQL);
                    };
                    if (DocList != "")
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            // Applying newly added records
                            List<string> list = new List<string>();
                            List<string> toRemove = new List<string>();
                            List<string> FinalList = new List<string>();
                            List<string> CreditDocList = new List<string>();
                            List<string> DocumentUntagList = new List<string>();
                            CreditList = string.Empty;

                            msSQL = " select group_concat(distinct individualdocument_gid) " +
                                " from ocs_trn_tdocumentchecktls where  " +
                                " application_gid = '" + values.application_gid + "'" +
                                " and credit_gid = '" + dt["contact_gid"].ToString() + "'";

                            CreditList = objdbconn.GetExecuteScalar(msSQL);
                            if (CreditList.Any(",".Contains))
                            {
                                CreditDocList = CreditList.Split(',').ToList();
                            }
                            else { CreditDocList.Add(CreditList); }

                            list = DocumentList;
                            toRemove = CreditDocList;
                            FinalList = list.Except(toRemove).ToList();
                            DocumentUntagList = list.Intersect(toRemove).ToList();

                            // Applying already untagged doc to tagged doc

                            if (DocumentUntagList != null)
                            {
                                foreach (string UntagListitem in DocumentUntagList)
                                {
                                    msSQL = " select* from ocs_trn_tdocumentchecktls " +
                                            " where  application_gid = '" + values.application_gid + "'" +
                                            " and credit_gid = '" + dt["contact_gid"].ToString() + "' and " +
                                            " individualdocument_gid='" + UntagListitem + "' and untagged_type is not null ";
                                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                    if (objODBCDatareader.HasRows == true)
                                    {
                                        msSQL = "update ocs_trn_tdocumentchecktls set " +
                                             " untagged_type = null where  application_gid = '" + values.application_gid + "'" +
                                             " and credit_gid = '" + dt["contact_gid"].ToString() + "' and " +
                                             " individualdocument_gid='" + UntagListitem + "'";

                                        mnResultUntag = objdbconn.ExecuteNonQuerySQL(msSQL);
                                    }
                                    objODBCDatareader.Close();

                                }
                                if (FinalList != null)
                                {
                                    foreach (string Listitem in FinalList)
                                    {
                                        msSQL = " select individualdocument_gid,documenttypes_gid,documenttype_name, " +
                                                " individualdocument_name,covenant_type " +
                                                " from ocs_mst_tindividualdocument where individualdocument_gid='" + Listitem + "'";
                                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                        if (objODBCDatareader.HasRows == true)
                                        {
                                            lsdocumenttype_gid = objODBCDatareader["documenttypes_gid"].ToString();
                                            lsdocumenttype_name = objODBCDatareader["documenttype_name"].ToString();
                                            lscompanydocument_name = objODBCDatareader["individualdocument_name"].ToString();
                                            lscovenant_type = objODBCDatareader["covenant_type"].ToString();
                                            lsindividualdocument_gid = objODBCDatareader["individualdocument_gid"].ToString();
                                        }
                                        objODBCDatareader.Close();


                                        msGetGID = objcmnfunctions.GetMasterGID("DOCG");

                                        msSQL = " insert into ocs_trn_tdocumentchecktls(" +
                                               " documentcheckdtl_gid," +
                                               " application_gid," +
                                               " credit_gid, " +
                                               " individualdocument_gid, " +
                                               " documenttype_gid," +
                                               " documenttype_code," +
                                               " documenttype_name," +
                                               " covenant_type, " +
                                               " tagged_by, " +
                                               " tagged_name, " +
                                               " created_date," +
                                               " created_by,ind_gur_flag,untagged_type)" +
                                               " VALUES(" +
                                               "'" + msGetGID + "'," +
                                               "'" + values.application_gid + "'," +
                                               "'" + dt["contact_gid"].ToString() + "'," +
                                               "'" + lsindividualdocument_gid + "'," +
                                               "'" + lsdocumenttype_gid + "'," +
                                               "'" + lsdocumenttype_name + "'," +
                                               "'" + lscompanydocument_name.Replace("'", "") + "'," +
                                               "'" + lscovenant_type + "'," +
                                               "'" + values.taggedby + "'," +
                                               "'" + tagged_name + "'," +
                                               "current_timestamp," +
                                               "'" + user_gid + "','Y',null)";

                                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                        DaMstScannedDocument objvalues = new DaMstScannedDocument();
                                        objvalues.DaGroupDocChecklistinfoCredit(values.application_gid, dt["contact_gid"].ToString(), employee_gid);

                                    }

                                }
                                dt_datatable.Dispose();

                            }

                        }
                    }
                    if ((mnResult == 1) || (mnResultUntag == 1))
                    {
                        values.status = true;
                        values.message = "Document type checklist applied successfully to all Guarantors ";
                    }
                    else
                    {
                        values.status = false;
                        values.message = "There is No Document type checklist to apply for Guarantors";
                    }

                }

                else if (lsStackholdertype == "Member")
                {
                    msSQL = " select contact_gid from ocs_mst_tcontact where application_gid = '" + values.application_gid + "'" +
                                       " and  stakeholder_type ='Member'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable.Rows.Count != 0)
                    {
                        msSQL = " select group_concat(distinct individualdocument_gid) " +
                                " from ocs_trn_tdocumentchecktls where  " +
                                " application_gid = '" + values.application_gid + "'" +
                                " and credit_gid = '" + values.credit_gid + "' " +
                                " and ind_mem_flag = 'Y' and untagged_type is null";

                        DocList = objdbconn.GetExecuteScalar(msSQL);
                        DocumentList = DocList.Split(',').ToList();
                        msSQL = " select group_concat(distinct companydocument_gid) " +
                                " from ocs_trn_tdocumentchecktls where  " +
                                " application_gid = '" + values.application_gid + "'" +
                                " and credit_gid = '" + values.credit_gid + "' " +
                                " and ind_mem_flag = 'Y' and untagged_type is not null";


                        DocUntagList = objdbconn.GetExecuteScalar(msSQL);
                    };
                    if (DocList != "")
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            // Applying newly added records
                            List<string> list = new List<string>();
                            List<string> toRemove = new List<string>();
                            List<string> FinalList = new List<string>();
                            List<string> CreditDocList = new List<string>();
                            List<string> DocumentUntagList = new List<string>();
                            CreditList = string.Empty;

                            msSQL = " select group_concat(distinct individualdocument_gid) " +
                                " from ocs_trn_tdocumentchecktls where  " +
                                " application_gid = '" + values.application_gid + "'" +
                                " and credit_gid = '" + dt["contact_gid"].ToString() + "'";

                            CreditList = objdbconn.GetExecuteScalar(msSQL);
                            if (CreditList.Any(",".Contains))
                            {
                                CreditDocList = CreditList.Split(',').ToList();
                            }
                            else { CreditDocList.Add(CreditList); }

                            list = DocumentList;
                            toRemove = CreditDocList;
                            FinalList = list.Except(toRemove).ToList();
                            DocumentUntagList = list.Intersect(toRemove).ToList();

                            // Applying already untagged doc to tagged doc

                            if (DocumentUntagList != null)
                            {
                                foreach (string UntagListitem in DocumentUntagList)
                                {
                                    msSQL = " select* from ocs_trn_tdocumentchecktls " +
                                            " where  application_gid = '" + values.application_gid + "'" +
                                            " and credit_gid = '" + dt["contact_gid"].ToString() + "' and " +
                                            " individualdocument_gid='" + UntagListitem + "' and untagged_type is not null ";
                                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                    if (objODBCDatareader.HasRows == true)
                                    {
                                        msSQL = "update ocs_trn_tdocumentchecktls set " +
                                             " untagged_type = null where  application_gid = '" + values.application_gid + "'" +
                                             " and credit_gid = '" + dt["contact_gid"].ToString() + "' and " +
                                             " individualdocument_gid='" + UntagListitem + "'";

                                        mnResultUntag = objdbconn.ExecuteNonQuerySQL(msSQL);
                                    }
                                    objODBCDatareader.Close();

                                }

                                if (FinalList != null)
                                {
                                    foreach (string Listitem in FinalList)
                                    {
                                        msSQL = " select individualdocument_gid,documenttypes_gid,documenttype_name, " +
                                                " individualdocument_name,covenant_type " +
                                                " from ocs_mst_tindividualdocument where individualdocument_gid='" + Listitem + "'";
                                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                        if (objODBCDatareader.HasRows == true)
                                        {
                                            lsdocumenttype_gid = objODBCDatareader["documenttypes_gid"].ToString();
                                            lsdocumenttype_name = objODBCDatareader["documenttype_name"].ToString();
                                            lscompanydocument_name = objODBCDatareader["individualdocument_name"].ToString();
                                            lscovenant_type = objODBCDatareader["covenant_type"].ToString();
                                            lsindividualdocument_gid = objODBCDatareader["individualdocument_gid"].ToString();
                                        }
                                        objODBCDatareader.Close();

                                        msGetGID = objcmnfunctions.GetMasterGID("DOCG");

                                        msSQL = " insert into ocs_trn_tdocumentchecktls(" +
                                               " documentcheckdtl_gid," +
                                               " application_gid," +
                                               " credit_gid, " +
                                               " individualdocument_gid, " +
                                               " documenttype_gid," +
                                               " documenttype_code," +
                                               " documenttype_name," +
                                               " covenant_type, " +
                                               " tagged_by, " +
                                               " tagged_name, " +
                                               " created_date," +
                                               " created_by,ind_mem_flag,untagged_type)" +
                                               " VALUES(" +
                                               "'" + msGetGID + "'," +
                                               "'" + values.application_gid + "'," +
                                               "'" + dt["contact_gid"].ToString() + "'," +
                                               "'" + lsindividualdocument_gid + "'," +
                                               "'" + lsdocumenttype_gid + "'," +
                                               "'" + lsdocumenttype_name + "'," +
                                               "'" + lscompanydocument_name.Replace("'", "") + "'," +
                                               "'" + lscovenant_type + "'," +
                                               "'" + values.taggedby + "'," +
                                               "'" + tagged_name + "'," +
                                               "current_timestamp," +
                                               "'" + user_gid + "','Y',null)";

                                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                        DaMstScannedDocument objvalues = new DaMstScannedDocument();
                                        objvalues.DaGroupDocChecklistinfoCredit(values.application_gid, dt["contact_gid"].ToString(), employee_gid);

                                    }
                                }
                                dt_datatable.Dispose();
                            }
                        }
                        if ((mnResult == 1) || (mnResultUntag == 1))
                        {
                            values.status = true;
                            values.message = "Document type checklist applied successfully to all Members ";
                        }
                        else
                        {
                            values.status = false;
                            values.message = "There is No Document type checklist to apply for Members";
                        }
                    }
                }

            }
        }

        public void DaApplyALLCovenantDocumentList(MdlMstCAD values, string user_gid, string employee_gid)
        {
            //List<CovenantPeriod> TaggedDocumentChecklist = values.CovenantPeriod.Where(a => a.covenantchecked == true).ToList();
            //List<CovenantPeriod> UnTaggedDocumentChecklist = values.CovenantPeriod.Where(a => a.covenantchecked == false).ToList();

            //List<CovenantPeriod> caddocchecklist = new List<CovenantPeriod>();
            //List<CovenantPeriod> newlyaddedchecklist = new List<CovenantPeriod>();
            //List<CovenantPeriod> untaggedcaddocchecklist = new List<CovenantPeriod>();

            //caddocchecklist = TaggedDocumentChecklist.Where(a => a.groupcovdocumentchecklist_gid != "" && a.groupcovdocumentchecklist_gid != null).ToList();
            //newlyaddedchecklist = TaggedDocumentChecklist.Where(a => a.groupcovdocumentchecklist_gid == "" || a.groupcovdocumentchecklist_gid == null).ToList();
            //untaggedcaddocchecklist = UnTaggedDocumentChecklist.Where(a => a.groupcovdocumentchecklist_gid != "" && a.groupcovdocumentchecklist_gid != null).ToList();
            bool Isgroupnew = false;

            string lscovenant_type = "", lscompanydocument_gid = "", lsindividualdocument_gid = "", lsgroupdocument_gid = "";

            List<string> DocumentList = new List<string>();

            string documenttype_gid = "", documenttype_code = "", documenttype_name = "", groupdocumentchecklist_gid = "", documentuploaded_gid = "";
            string lsStackholdertype;
            string lsApplication_gid = string.Empty;
            string lsCredit_gid = string.Empty;
            string buffer_days = string.Empty;
            string covenant_periods = string.Empty;

            lsApplication_gid = values.application_gid;
            lsCredit_gid = values.credit_gid;

            msSQL = "select concat(user_firstname,' ', user_lastname, ' / ', user_code) as username from adm_mst_tuser where user_gid='" + user_gid + "'";
            string tagged_name = objdbconn.GetExecuteScalar(msSQL);


            if (values.applicant_type == "Institution")
            {
                //values.application_gid = "APPC20220628489";

                msSQL = " select stakeholder_type from ocs_mst_tinstitution where institution_gid = '" + lsCredit_gid + "'";

                lsStackholdertype = objdbconn.GetExecuteScalar(msSQL);

                if (lsStackholdertype == "Guarantor")
                {
                    msSQL = " select institution_gid from ocs_mst_tinstitution where application_gid = '" + lsApplication_gid + "'" +
                   " and  stakeholder_type = 'Guarantor'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable.Rows.Count != 0)
                    {

                        msSQL = " select group_concat(distinct companydocument_gid) " +
                                " from ocs_trn_tcovanantdocumentcheckdtls where  " +
                                " application_gid = '" + lsApplication_gid + "'" +
                                " and credit_gid = '" + lsCredit_gid + "' " +
                                // " and companyalldoc_flag <> 'Y' " +
                                " and com_gur_flag = 'Y' and untagged_type is null";


                        DocList = objdbconn.GetExecuteScalar(msSQL);
                        DocumentList = DocList.Split(',').ToList();
                    };
                    if ((DocList != ""))
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            List<string> list = new List<string>();
                            List<string> toRemove = new List<string>();
                            List<string> FinalList = new List<string>();
                            List<string> CreditDocList = new List<string>();
                            CreditList = string.Empty;

                            msSQL = " select group_concat(distinct companydocument_gid) " +
                                " from ocs_trn_tcovanantdocumentcheckdtls where  " +
                                " application_gid = '" + lsApplication_gid + "'" +
                                " and credit_gid = '" + dt["institution_gid"].ToString() + "'";

                            CreditList = objdbconn.GetExecuteScalar(msSQL);
                            if (CreditList.Any(",".Contains))
                            {
                                CreditDocList = CreditList.Split(',').ToList();
                            }
                            else { CreditDocList.Add(CreditList); }

                            list = DocumentList;
                            toRemove = CreditDocList;
                            FinalList = list.Except(toRemove).ToList();

                            if (FinalList != null)
                            {
                                bool lsgroupupdate = false;

                                foreach (string Listitem in FinalList)
                                {
                                    if (values.applicant_type == "Individual")
                                    {
                                        msGetGID = objcmnfunctions.GetMasterGID("CDCL");
                                        msSQL = " select individualdocument_gid,documenttypes_gid,documenttype_name,individualdocument_name,covenant_type " +
                                                    " from ocs_mst_tindividualdocument where individualdocument_gid='" + Listitem + "'";
                                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                        if (objODBCDatareader.HasRows == true)
                                        {
                                            lsdocumenttype_gid = objODBCDatareader["documenttypes_gid"].ToString();
                                            lsdocumenttype_name = objODBCDatareader["documenttype_name"].ToString();
                                            lscompanydocument_name = objODBCDatareader["individualdocument_name"].ToString();
                                            lscovenant_type = objODBCDatareader["covenant_type"].ToString();

                                        }
                                        objODBCDatareader.Close();
                                    }
                                    else if (values.applicant_type == "Group")
                                    {
                                        msGetGID = objcmnfunctions.GetMasterGID("CDCL");
                                        msSQL = " select groupdocument_gid,documenttypes_gid,documenttype_name,groupdocument_name,covenant_type " +
                                                    " from ocs_mst_tgroupdocument  where groupdocument_gid='" + Listitem + "'";
                                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                        if (objODBCDatareader.HasRows == true)
                                        {
                                            lsdocumenttype_gid = objODBCDatareader["documenttypes_gid"].ToString();
                                            lsdocumenttype_name = objODBCDatareader["documenttype_name"].ToString();
                                            lscompanydocument_name = objODBCDatareader["groupdocument_name"].ToString();
                                            lscovenant_type = objODBCDatareader["covenant_type"].ToString();
                                        }
                                        objODBCDatareader.Close();
                                    }
                                    else
                                    {
                                        msGetGID = objcmnfunctions.GetMasterGID("CDCL");
                                        msSQL = " select companydocument_gid,documenttypes_gid,documenttype_name,companydocument_name,covenant_type " +
                                                    " from ocs_mst_tcompanydocument where companydocument_gid='" + Listitem + "'";
                                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                        if (objODBCDatareader.HasRows == true)
                                        {
                                            lsdocumenttype_gid = objODBCDatareader["documenttypes_gid"].ToString();
                                            lsdocumenttype_name = objODBCDatareader["documenttype_name"].ToString();
                                            lscompanydocument_name = objODBCDatareader["companydocument_name"].ToString();
                                            lscovenant_type = objODBCDatareader["covenant_type"].ToString();
                                        }
                                        objODBCDatareader.Close();
                                    }

                                    msSQL = " select covenant_periods, buffer_days " +
                                            " from ocs_trn_tcovanantdocumentcheckdtls where " +
                                            " application_gid = '" + lsApplication_gid + "'  and " +
                                            " credit_gid = '" + lsCredit_gid + "' and " +
                                            " com_gur_flag = 'Y' and companydocument_gid = '" + Listitem + "'";

                                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                    if (objODBCDatareader.HasRows == true)
                                    {
                                        covenant_periods = objODBCDatareader["covenant_periods"].ToString();
                                        buffer_days = objODBCDatareader["buffer_days"].ToString();

                                    }
                                    objODBCDatareader.Close();

                                    msSQL = " insert into ocs_trn_tcovanantdocumentcheckdtls(" +
                                             " covenantdocumentcheckdtl_gid," +
                                             " application_gid," +
                                             " credit_gid, ";
                                    if (values.applicant_type == "Institution")
                                        msSQL += " companydocument_gid, ";
                                    else if (values.applicant_type == "Individual")
                                        msSQL += " individualdocument_gid, ";
                                    else
                                    {
                                        msSQL += " groupdocument_gid,";
                                    }
                                    msSQL += " documenttype_gid," +
                                        " documenttype_code," +
                                        " documenttype_name," +
                                        " covenant_type, " +
                                        " tagged_by, " +
                                        " tagged_name, " +
                                        " covenant_periods, " +
                                        " covenantperiod_updatedby, " +
                                        " covenantperiod_updateddate, " +
                                        " buffer_days, " +
                                        " bufferday_updatedby, " +
                                        " bufferday_updateddate, " +
                                        " created_date," +
                                        " created_by)" +
                                        " VALUES(" +
                                        "'" + msGetGID + "'," +
                                        "'" + lsApplication_gid + "'," +
                                        "'" + dt["institution_gid"].ToString() + "'," +
                                        "'" + Listitem + "'," +
                                        "'" + lsdocumenttype_gid + "'," +
                                        "'" + lsdocumenttype_name + "'," +
                                        "'" + lscompanydocument_name.Replace("'", "") + "'," +
                                        "'" + lscovenant_type + "'," +
                                        "'" + values.taggedby + "'," +
                                        "'" + tagged_name + "'," +
                                        "'" + covenant_periods + "'," +
                                        "'" + user_gid + "'," +
                                        "current_timestamp," +
                                        "'" + buffer_days + "'," +
                                        "'" + user_gid + "'," +
                                        "current_timestamp," +
                                        "current_timestamp," +
                                        "'" + user_gid + "')";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                    Isgroupnew = true;


                                    DaMstScannedDocument objvalues = new DaMstScannedDocument();
                                    objvalues.DaGroupDocChecklistinfoCredit(lsApplication_gid, dt["institution_gid"].ToString(), employee_gid);


                                }
                                dt_datatable.Dispose();

                            }

                        }
                        if (mnResult == 1)
                        {
                            values.status = true;
                            values.message = "Document type checklist applied successfully to all Guarantors ";
                        }
                        else
                        {
                            values.status = false;
                            values.message = "There is No Document type checklist to apply for Guarantors";
                        }
                    }
                }
                else if (lsStackholdertype == "Member")
                {
                    msSQL = " select institution_gid from ocs_mst_tinstitution where application_gid = '" + lsApplication_gid + "'" +
                                       " and  stakeholder_type ='Member'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable.Rows.Count != 0)
                    {
                        msSQL = " select group_concat(distinct companydocument_gid) " +
                                " from ocs_trn_tcovanantdocumentcheckdtls where  " +
                                " application_gid = '" + lsApplication_gid + "'" +
                                " and credit_gid = '" + lsCredit_gid + "' " +
                                " and com_mem_flag = 'Y' and untagged_type is null";


                        DocList = objdbconn.GetExecuteScalar(msSQL);
                        DocumentList = DocList.Split(',').ToList();
                    };
                    if ((DocList != ""))
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            List<string> list = new List<string>();
                            List<string> toRemove = new List<string>();
                            List<string> FinalList = new List<string>();
                            List<string> CreditDocList = new List<string>();
                            CreditList = string.Empty;

                            msSQL = " select group_concat(distinct companydocument_gid) " +
                                " from ocs_trn_tcovanantdocumentcheckdtls where  " +
                                " application_gid = '" + lsApplication_gid + "'" +
                                " and credit_gid = '" + dt["institution_gid"].ToString() + "'";

                            CreditList = objdbconn.GetExecuteScalar(msSQL);
                            if (CreditList.Any(",".Contains))
                            {
                                CreditDocList = CreditList.Split(',').ToList();
                            }
                            else { CreditDocList.Add(CreditList); }

                            list = DocumentList;
                            toRemove = CreditDocList;
                            FinalList = list.Except(toRemove).ToList();

                            if (FinalList != null)
                            {
                                foreach (string Listitem in FinalList)
                                {
                                    if (values.applicant_type == "Individual")
                                    {
                                        msGetGID = objcmnfunctions.GetMasterGID("CDCL");
                                        msSQL = " select individualdocument_gid,documenttypes_gid,documenttype_name,individualdocument_name,covenant_type " +
                                                    " from ocs_mst_tindividualdocument where individualdocument_gid='" + Listitem + "'";
                                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                        if (objODBCDatareader.HasRows == true)
                                        {
                                            lsdocumenttype_gid = objODBCDatareader["documenttypes_gid"].ToString();
                                            lsdocumenttype_name = objODBCDatareader["documenttype_name"].ToString();
                                            lscompanydocument_name = objODBCDatareader["individualdocument_name"].ToString();
                                            lscovenant_type = objODBCDatareader["covenant_type"].ToString();

                                        }
                                        objODBCDatareader.Close();
                                    }
                                    else if (values.applicant_type == "Group")
                                    {
                                        msGetGID = objcmnfunctions.GetMasterGID("CDCL");
                                        msSQL = " select groupdocument_gid,documenttypes_gid,documenttype_name,groupdocument_name,covenant_type " +
                                                    " from ocs_mst_tgroupdocument  where groupdocument_gid='" + Listitem + "'";
                                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                        if (objODBCDatareader.HasRows == true)
                                        {
                                            lsdocumenttype_gid = objODBCDatareader["documenttypes_gid"].ToString();
                                            lsdocumenttype_name = objODBCDatareader["documenttype_name"].ToString();
                                            lscompanydocument_name = objODBCDatareader["groupdocument_name"].ToString();
                                            lscovenant_type = objODBCDatareader["covenant_type"].ToString();
                                        }
                                        objODBCDatareader.Close();
                                    }
                                    else
                                    {
                                        msGetGID = objcmnfunctions.GetMasterGID("CDCL");
                                        msSQL = " select companydocument_gid,documenttypes_gid,documenttype_name,companydocument_name,covenant_type " +
                                                    " from ocs_mst_tcompanydocument where companydocument_gid='" + Listitem + "'";
                                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                        if (objODBCDatareader.HasRows == true)
                                        {
                                            lsdocumenttype_gid = objODBCDatareader["documenttypes_gid"].ToString();
                                            lsdocumenttype_name = objODBCDatareader["documenttype_name"].ToString();
                                            lscompanydocument_name = objODBCDatareader["companydocument_name"].ToString();
                                            lscovenant_type = objODBCDatareader["covenant_type"].ToString();
                                        }
                                        objODBCDatareader.Close();
                                    }

                                    msSQL = " select covenant_periods, buffer_days " +
                                            " from ocs_trn_tcovanantdocumentcheckdtls where " +
                                            " application_gid = '" + lsApplication_gid + "'  and " +
                                            " credit_gid = '" + lsCredit_gid + "' and " +
                                            " com_mem_flag = 'Y' and companydocument_gid = '" + Listitem + "'";

                                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                    if (objODBCDatareader.HasRows == true)
                                    {
                                        covenant_periods = objODBCDatareader["covenant_periods"].ToString();
                                        buffer_days = objODBCDatareader["buffer_days"].ToString();

                                    }
                                    objODBCDatareader.Close();

                                    msSQL = " insert into ocs_trn_tcovanantdocumentcheckdtls(" +
                                             " covenantdocumentcheckdtl_gid," +
                                             " application_gid," +
                                             " credit_gid, ";
                                    if (values.applicant_type == "Institution")
                                        msSQL += " companydocument_gid, ";
                                    else if (values.applicant_type == "Individual")
                                        msSQL += " individualdocument_gid, ";
                                    else
                                    {
                                        msSQL += " groupdocument_gid,";
                                    }
                                    msSQL += " documenttype_gid," +
                                        " documenttype_code," +
                                        " documenttype_name," +
                                        " covenant_type, " +
                                        " tagged_by, " +
                                        " tagged_name, " +
                                        " covenant_periods, " +
                                        " covenantperiod_updatedby, " +
                                        " covenantperiod_updateddate, " +
                                        " buffer_days, " +
                                        " bufferday_updatedby, " +
                                        " bufferday_updateddate, " +
                                        " created_date," +
                                        " created_by)" +
                                        " VALUES(" +
                                        "'" + msGetGID + "'," +
                                        "'" + lsApplication_gid + "'," +
                                        "'" + dt["institution_gid"].ToString() + "'," +
                                        "'" + Listitem + "'," +
                                        "'" + lsdocumenttype_gid + "'," +
                                        "'" + lsdocumenttype_name + "'," +
                                        "'" + lscompanydocument_name.Replace("'", "") + "'," +
                                        "'" + lscovenant_type + "'," +
                                        "'" + values.taggedby + "'," +
                                        "'" + tagged_name + "'," +
                                        "'" + covenant_periods + "'," +
                                        "'" + user_gid + "'," +
                                        "current_timestamp," +
                                        "'" + buffer_days + "'," +
                                        "'" + user_gid + "'," +
                                        "current_timestamp," +
                                        "current_timestamp," +
                                        "'" + user_gid + "')";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                    Isgroupnew = true;


                                    DaMstScannedDocument objvalues = new DaMstScannedDocument();
                                    objvalues.DaGroupDocChecklistinfoCredit(lsApplication_gid, dt["institution_gid"].ToString(), employee_gid);


                                }



                            }
                            dt_datatable.Dispose();

                        }

                    }
                    if (mnResult == 1)
                    {
                        values.status = true;
                        values.message = "Document type checklist applied successfully to all Members ";
                    }
                    else
                    {
                        values.status = false;
                        values.message = "There is No Document type checklist to apply for Members";
                    }

                }

            }
            else if (values.applicant_type == "Individual")
            {
                //values.application_gid = "APPC20220628489";

                msSQL = " select stakeholder_type from ocs_mst_tcontact where contact_gid = '" + lsCredit_gid + "'";

                lsStackholdertype = objdbconn.GetExecuteScalar(msSQL);

                if (lsStackholdertype == "Guarantor")
                {
                    msSQL = " select contact_gid from ocs_mst_tcontact where application_gid = '" + lsApplication_gid + "'" +
                   " and  stakeholder_type = 'Guarantor'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable.Rows.Count != 0)
                    {

                        msSQL = " select group_concat(distinct individualdocument_gid) " +
                                " from ocs_trn_tcovanantdocumentcheckdtls where  " +
                                " application_gid = '" + lsApplication_gid + "'" +
                                " and credit_gid = '" + lsCredit_gid + "' " +
                                " and ind_gur_flag = 'Y' and untagged_type is null";


                        DocList = objdbconn.GetExecuteScalar(msSQL);
                        DocumentList = DocList.Split(',').ToList();
                    };
                    if (DocList != "")
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            List<string> list = new List<string>();
                            List<string> toRemove = new List<string>();
                            List<string> FinalList = new List<string>();
                            List<string> CreditDocList = new List<string>();
                            CreditList = string.Empty;

                            msSQL = " select group_concat(distinct individualdocument_gid) " +
                                " from ocs_trn_tcovanantdocumentcheckdtls where  " +
                                " application_gid = '" + lsApplication_gid + "'" +
                                " and credit_gid = '" + dt["contact_gid"].ToString() + "'";

                            CreditList = objdbconn.GetExecuteScalar(msSQL);
                            if (CreditList.Any(",".Contains))
                            {
                                CreditDocList = CreditList.Split(',').ToList();
                            }
                            else { CreditDocList.Add(CreditList); }

                            list = DocumentList;
                            toRemove = CreditDocList;
                            FinalList = list.Except(toRemove).ToList();

                            if (FinalList != null)
                            {
                                foreach (string Listitem in FinalList)
                                {
                                    if (values.applicant_type == "Individual")
                                    {
                                        msGetGID = objcmnfunctions.GetMasterGID("CDCL");
                                        msSQL = " select individualdocument_gid,documenttypes_gid,documenttype_name,individualdocument_name,covenant_type " +
                                                    " from ocs_mst_tindividualdocument where individualdocument_gid='" + Listitem + "'";
                                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                        if (objODBCDatareader.HasRows == true)
                                        {
                                            lsdocumenttype_gid = objODBCDatareader["documenttypes_gid"].ToString();
                                            lsdocumenttype_name = objODBCDatareader["documenttype_name"].ToString();
                                            lscompanydocument_name = objODBCDatareader["individualdocument_name"].ToString();
                                            lscovenant_type = objODBCDatareader["covenant_type"].ToString();

                                        }
                                        objODBCDatareader.Close();
                                    }
                                    else if (values.applicant_type == "Group")
                                    {
                                        msGetGID = objcmnfunctions.GetMasterGID("CDCL");
                                        msSQL = " select groupdocument_gid,documenttypes_gid,documenttype_name,groupdocument_name,covenant_type " +
                                                    " from ocs_mst_tgroupdocument  where groupdocument_gid='" + Listitem + "'";
                                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                        if (objODBCDatareader.HasRows == true)
                                        {
                                            lsdocumenttype_gid = objODBCDatareader["documenttypes_gid"].ToString();
                                            lsdocumenttype_name = objODBCDatareader["documenttype_name"].ToString();
                                            lscompanydocument_name = objODBCDatareader["groupdocument_name"].ToString();
                                            lscovenant_type = objODBCDatareader["covenant_type"].ToString();
                                        }
                                        objODBCDatareader.Close();
                                    }
                                    else
                                    {
                                        msGetGID = objcmnfunctions.GetMasterGID("CDCL");
                                        msSQL = " select companydocument_gid,documenttypes_gid,documenttype_name,companydocument_name,covenant_type " +
                                                    " from ocs_mst_tcompanydocument where companydocument_gid='" + Listitem + "'";
                                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                        if (objODBCDatareader.HasRows == true)
                                        {
                                            lsdocumenttype_gid = objODBCDatareader["documenttypes_gid"].ToString();
                                            lsdocumenttype_name = objODBCDatareader["documenttype_name"].ToString();
                                            lscompanydocument_name = objODBCDatareader["companydocument_name"].ToString();
                                            lscovenant_type = objODBCDatareader["covenant_type"].ToString();
                                        }
                                        objODBCDatareader.Close();
                                    }

                                    msSQL = " select covenant_periods, buffer_days " +
                                            " from ocs_trn_tcovanantdocumentcheckdtls where " +
                                            " application_gid = '" + lsApplication_gid + "'  and " +
                                            " credit_gid = '" + lsCredit_gid + "' and " +
                                            " ind_gur_flag = 'Y' and individualdocument_gid = '" + Listitem + "'";

                                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                    if (objODBCDatareader.HasRows == true)
                                    {
                                        covenant_periods = objODBCDatareader["covenant_periods"].ToString();
                                        buffer_days = objODBCDatareader["buffer_days"].ToString();

                                    }
                                    objODBCDatareader.Close();

                                    msSQL = " insert into ocs_trn_tcovanantdocumentcheckdtls(" +
                                             " covenantdocumentcheckdtl_gid," +
                                             " application_gid," +
                                             " credit_gid, ";
                                    if (values.applicant_type == "Institution")
                                        msSQL += " companydocument_gid, ";
                                    else if (values.applicant_type == "Individual")
                                        msSQL += " individualdocument_gid, ";
                                    else
                                    {
                                        msSQL += " groupdocument_gid,";
                                    }
                                    msSQL += " documenttype_gid," +
                                        " documenttype_code," +
                                        " documenttype_name," +
                                        " covenant_type, " +
                                        " tagged_by, " +
                                        " tagged_name, " +
                                        " covenant_periods, " +
                                        " covenantperiod_updatedby, " +
                                        " covenantperiod_updateddate, " +
                                        " buffer_days, " +
                                        " bufferday_updatedby, " +
                                        " bufferday_updateddate, " +
                                        " created_date," +
                                        " created_by)" +
                                        " VALUES(" +
                                        "'" + msGetGID + "'," +
                                        "'" + lsApplication_gid + "'," +
                                        "'" + dt["contact_gid"].ToString() + "'," +
                                        "'" + Listitem + "'," +
                                        "'" + lsdocumenttype_gid + "'," +
                                        "'" + lsdocumenttype_name + "'," +
                                        "'" + lscompanydocument_name.Replace("'", "") + "'," +
                                        "'" + lscovenant_type + "'," +
                                        "'" + values.taggedby + "'," +
                                        "'" + tagged_name + "'," +
                                        "'" + covenant_periods + "'," +
                                        "'" + user_gid + "'," +
                                        "current_timestamp," +
                                        "'" + buffer_days + "'," +
                                        "'" + user_gid + "'," +
                                        "current_timestamp," +
                                        "current_timestamp," +
                                        "'" + user_gid + "')";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                    Isgroupnew = true;


                                    DaMstScannedDocument objvalues = new DaMstScannedDocument();
                                    objvalues.DaGroupDocChecklistinfoCredit(lsApplication_gid, dt["contact_gid"].ToString(), employee_gid);


                                }

                            }
                            dt_datatable.Dispose();

                        }

                    }
                    if (mnResult == 1)
                    {
                        values.status = true;
                        values.message = "Document type checklist applied successfully to all Guarantors ";
                    }
                    else
                    {
                        values.status = false;
                        values.message = "There is No Document type checklist to apply for Guarantors";
                    }

                }

                else if (lsStackholdertype == "Member")
                {
                    msSQL = " select contact_gid from ocs_mst_tcontact where application_gid = '" + lsApplication_gid + "'" +
                                       " and  stakeholder_type ='Member'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable.Rows.Count != 0)
                    {
                        msSQL = " select group_concat(distinct individualdocument_gid) " +
                                " from ocs_trn_tcovanantdocumentcheckdtls where  " +
                                " application_gid = '" + lsApplication_gid + "'" +
                                " and credit_gid = '" + lsCredit_gid + "' " +
                                " and ind_mem_flag = 'Y'";

                        DocList = objdbconn.GetExecuteScalar(msSQL);
                        DocumentList = DocList.Split(',').ToList();
                    };
                    if (DocList != "")
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            List<string> list = new List<string>();
                            List<string> toRemove = new List<string>();
                            List<string> FinalList = new List<string>();
                            List<string> CreditDocList = new List<string>();
                            CreditList = string.Empty;

                            msSQL = " select group_concat(distinct individualdocument_gid) " +
                                " from ocs_trn_tcovanantdocumentcheckdtls where  " +
                                " application_gid = '" + lsApplication_gid + "'" +
                                " and credit_gid = '" + dt["contact_gid"].ToString() + "'";

                            CreditList = objdbconn.GetExecuteScalar(msSQL);
                            if (CreditList.Any(",".Contains))
                            {
                                CreditDocList = CreditList.Split(',').ToList();
                            }
                            else { CreditDocList.Add(CreditList); }

                            list = DocumentList;
                            toRemove = CreditDocList;
                            FinalList = list.Except(toRemove).ToList();

                            if (FinalList != null)
                            {
                                foreach (string Listitem in FinalList)
                                {
                                    if (values.applicant_type == "Individual")
                                    {
                                        msGetGID = objcmnfunctions.GetMasterGID("CDCL");
                                        msSQL = " select individualdocument_gid,documenttypes_gid,documenttype_name,individualdocument_name,covenant_type " +
                                                    " from ocs_mst_tindividualdocument where individualdocument_gid='" + Listitem + "'";
                                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                        if (objODBCDatareader.HasRows == true)
                                        {
                                            lsdocumenttype_gid = objODBCDatareader["documenttypes_gid"].ToString();
                                            lsdocumenttype_name = objODBCDatareader["documenttype_name"].ToString();
                                            lscompanydocument_name = objODBCDatareader["individualdocument_name"].ToString();
                                            lscovenant_type = objODBCDatareader["covenant_type"].ToString();

                                        }
                                        objODBCDatareader.Close();
                                    }
                                    else if (values.applicant_type == "Group")
                                    {
                                        msGetGID = objcmnfunctions.GetMasterGID("CDCL");
                                        msSQL = " select groupdocument_gid,documenttypes_gid,documenttype_name,groupdocument_name,covenant_type " +
                                                    " from ocs_mst_tgroupdocument  where groupdocument_gid='" + Listitem + "'";
                                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                        if (objODBCDatareader.HasRows == true)
                                        {
                                            lsdocumenttype_gid = objODBCDatareader["documenttypes_gid"].ToString();
                                            lsdocumenttype_name = objODBCDatareader["documenttype_name"].ToString();
                                            lscompanydocument_name = objODBCDatareader["groupdocument_name"].ToString();
                                            lscovenant_type = objODBCDatareader["covenant_type"].ToString();
                                        }
                                        objODBCDatareader.Close();
                                    }
                                    else
                                    {
                                        msGetGID = objcmnfunctions.GetMasterGID("CDCL");
                                        msSQL = " select companydocument_gid,documenttypes_gid,documenttype_name,companydocument_name,covenant_type " +
                                                    " from ocs_mst_tcompanydocument where companydocument_gid='" + Listitem + "'";
                                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                        if (objODBCDatareader.HasRows == true)
                                        {
                                            lsdocumenttype_gid = objODBCDatareader["documenttypes_gid"].ToString();
                                            lsdocumenttype_name = objODBCDatareader["documenttype_name"].ToString();
                                            lscompanydocument_name = objODBCDatareader["companydocument_name"].ToString();
                                            lscovenant_type = objODBCDatareader["covenant_type"].ToString();
                                        }
                                        objODBCDatareader.Close();
                                    }

                                    msSQL = " select covenant_periods, buffer_days " +
                                            " from ocs_trn_tcovanantdocumentcheckdtls where " +
                                            " application_gid = '" + lsApplication_gid + "'  and " +
                                            " credit_gid = '" + lsCredit_gid + "' and " +
                                            " ind_mem_flag = 'Y' and individualdocument_gid = '" + Listitem + "'";

                                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                    if (objODBCDatareader.HasRows == true)
                                    {
                                        covenant_periods = objODBCDatareader["covenant_periods"].ToString();
                                        buffer_days = objODBCDatareader["buffer_days"].ToString();

                                    }
                                    objODBCDatareader.Close();

                                    msSQL = " insert into ocs_trn_tcovanantdocumentcheckdtls(" +
                                             " covenantdocumentcheckdtl_gid," +
                                             " application_gid," +
                                             " credit_gid, ";
                                    if (values.applicant_type == "Institution")
                                        msSQL += " companydocument_gid, ";
                                    else if (values.applicant_type == "Individual")
                                        msSQL += " individualdocument_gid, ";
                                    else
                                    {
                                        msSQL += " groupdocument_gid,";
                                    }
                                    msSQL += " documenttype_gid," +
                                        " documenttype_code," +
                                        " documenttype_name," +
                                        " covenant_type, " +
                                        " tagged_by, " +
                                        " tagged_name, " +
                                        " covenant_periods, " +
                                        " covenantperiod_updatedby, " +
                                        " covenantperiod_updateddate, " +
                                        " buffer_days, " +
                                        " bufferday_updatedby, " +
                                        " bufferday_updateddate, " +
                                        " created_date," +
                                        " created_by)" +
                                        " VALUES(" +
                                        "'" + msGetGID + "'," +
                                        "'" + lsApplication_gid + "'," +
                                        "'" + dt["contact_gid"].ToString() + "'," +
                                        "'" + Listitem + "'," +
                                        "'" + lsdocumenttype_gid + "'," +
                                        "'" + lsdocumenttype_name + "'," +
                                        "'" + lscompanydocument_name.Replace("'", "") + "'," +
                                        "'" + lscovenant_type + "'," +
                                        "'" + values.taggedby + "'," +
                                        "'" + tagged_name + "'," +
                                        "'" + covenant_periods + "'," +
                                        "'" + user_gid + "'," +
                                        "current_timestamp," +
                                        "'" + buffer_days + "'," +
                                        "'" + user_gid + "'," +
                                        "current_timestamp," +
                                        "current_timestamp," +
                                        "'" + user_gid + "')";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                    Isgroupnew = true;


                                    DaMstScannedDocument objvalues = new DaMstScannedDocument();
                                    objvalues.DaGroupDocChecklistinfoCredit(lsApplication_gid, dt["contact_gid"].ToString(), employee_gid);


                                }
                            }
                            dt_datatable.Dispose();
                        }
                    }
                    if (mnResult == 1)
                    {
                        values.status = true;
                        values.message = "Document type checklist applied successfully to all Members ";
                    }
                    else
                    {
                        values.status = false;
                        values.message = "There is No Document type checklist to apply for Members";
                    }
                }
            }


        }
        public void DaPostIndividualCheckList(MdlMstCAD values, string user_gid, string employee_gid)
        {
            bool lsgroupupdate = false;
            string lsindividualdocument_gid = "";
            string lsStackholdertype;
            msSQL = " select stakeholder_type from ocs_mst_tcontact where contact_gid= '" + values.credit_gid + "'";

            lsStackholdertype = objdbconn.GetExecuteScalar(msSQL);
            foreach (string i in values.document_gid)
            {
                msGetGID = objcmnfunctions.GetMasterGID("DOCG");
                string lscovenant_type = "";
                msSQL = " select individualdocument_gid,documenttypes_gid,documenttype_name,individualdocument_name,covenant_type " +
                        " from ocs_mst_tindividualdocument where individualdocument_gid='" + i + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lsdocumenttype_gid = objODBCDatareader["documenttypes_gid"].ToString();
                    lsdocumenttype_name = objODBCDatareader["documenttype_name"].ToString();
                    lsindividualdocument_name = objODBCDatareader["individualdocument_name"].ToString();
                    lscovenant_type = objODBCDatareader["covenant_type"].ToString();
                    lsindividualdocument_gid = objODBCDatareader["individualdocument_gid"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = " select groupdocumentchecklist_gid from ocs_trn_tdocumentchecktls where application_gid='" + values.application_gid + "'" +
                        " and credit_gid='" + values.credit_gid + "' and individualdocument_gid ='" + lsindividualdocument_gid + "'";
                string lsgroupdocumentchecklist_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select concat(user_firstname,' ', user_lastname, ' / ', user_code) as username from adm_mst_tuser where user_gid='" + user_gid + "'";
                string tagged_name = objdbconn.GetExecuteScalar(msSQL);
                if (lsgroupdocumentchecklist_gid != "")
                {
                    msSQL = " update ocs_trn_tdocumentchecktls set untagged_type=null, " +
                   " untagged_by='" + user_gid + "', " +
                   " tagged_name='" + tagged_name + "'," +
                   " tagged_by='" + values.taggedby + "'," +
                   " untagged_date=current_timestamp" +
                   " where groupdocumentchecklist_gid='" + lsgroupdocumentchecklist_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update ocs_trn_tgroupdocumentchecklist set untagged_type=null where groupdocumentchecklist_gid='" + lsgroupdocumentchecklist_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (lsStackholdertype == "Guarantor")
                    {
                        msSQL = "update ocs_trn_tdocumentchecktls set ind_gur_flag = 'Y' " +
                           " where groupdocumentchecklist_gid = '" + lsgroupdocumentchecklist_gid + "'";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }

                    if (lsStackholdertype == "Member")
                    {
                        msSQL = "update ocs_trn_tdocumentchecktls set ind_mem_flag = 'Y' " +
                           " where groupdocumentchecklist_gid = '" + lsgroupdocumentchecklist_gid + "'";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                else
                {
                    msSQL = " insert into ocs_trn_tdocumentchecktls(" +
                        " documentcheckdtl_gid," +
                        " application_gid," +
                        " credit_gid," +
                        " individualdocument_gid," +
                        " documenttype_gid," +
                        " documenttype_code," +
                        " documenttype_name," +
                        " covenant_type, " +
                        " tagged_by, " +
                        " created_date," +
                        " created_by)" +
                        " VALUES(" +
                        "'" + msGetGID + "'," +
                        "'" + values.application_gid + "'," +
                        "'" + values.credit_gid + "'," +
                        "'" + i + "'," +
                        "'" + lsdocumenttype_gid + "'," +
                        "'" + lsdocumenttype_name + "'," +
                        "'" + lsindividualdocument_name.Replace("'", "") + "'," +
                        "'" + lscovenant_type + "'," +
                        "'" + values.taggedby + "'," +
                        "current_timestamp," +
                        "'" + user_gid + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    lsgroupupdate = true;

                    if (lsStackholdertype == "Guarantor")
                    {
                        msSQL = "update ocs_trn_tdocumentchecktls set ind_gur_flag = 'Y' " +
                           " where application_gid = '" + values.application_gid + "' and " +
                           " credit_gid = '" + values.credit_gid + "' and documentcheckdtl_gid = '" + msGetGID + "'";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }

                    if (lsStackholdertype == "Member")
                    {
                        msSQL = "update ocs_trn_tdocumentchecktls set ind_mem_flag = 'Y' " +
                           " where application_gid = '" + values.application_gid + "' and " +
                           " credit_gid = '" + values.credit_gid + "' and documentcheckdtl_gid = '" + msGetGID + "'";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
            }
            if (lsgroupupdate)
            {
                DaMstScannedDocument objvalues = new DaMstScannedDocument();
                objvalues.DaGroupDocChecklistinfoCredit(values.application_gid, values.credit_gid, employee_gid);
            }
            if (mnResult == 1)
            {
                values.status = true;
                values.message = "Document Type Tagged Successfully";
            }
            else
            {
                values.status = true;
                values.message = "Error Occured";
            }
        }

        //Group Document
        public bool DaGetGroupTypeList(MdlMstCAD values, string credit_gid)
        {
            msSQL = " SELECT groupdocument_gid,groupdocument_name,documenttypes_gid,documenttype_name,covenant_type " +
                   " FROM ocs_mst_tgroupdocument where status='Y' and delete_flag='N' and groupdocument_gid not in (select groupdocument_gid " +
                  " from ocs_trn_tdocumentchecktls where credit_gid = '" + credit_gid + "' and untagged_type is null group by groupdocument_gid)";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getDocList = new List<CADDocument>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getDocList.Add(new CADDocument
                    {
                        document_gid = dt["groupdocument_gid"].ToString(),
                        documenttype_gid = dt["documenttypes_gid"].ToString(),
                        document_name = dt["groupdocument_name"].ToString(),
                        documenttype_name = dt["documenttype_name"].ToString(),
                        covenant_type = dt["covenant_type"].ToString(),
                    });
                }
                values.CADDocument = getDocList;
                values.status = true;
                values.message = "Data Fetched";
            }
            else
            {
                values.status = false;
                values.message = "No Record Found";
            }
            dt_datatable.Dispose();

            return true;
        }

        public bool DaGetCovenantGroupDocumentList(MdlMstCADCompany values, string credit_gid)
        {
            if (credit_gid != "")
            {
                msSQL = " SELECT groupdocument_gid,groupdocument_name,documenttypes_gid,documenttype_name,covenant_type " +
                        " FROM ocs_mst_tgroupdocument where status='Y' and covenant_type='Y' and groupdocument_gid not in (select groupdocument_gid " +
                        " from ocs_trn_tcovanantdocumentcheckdtls where credit_gid = '" + credit_gid + "' and untagged_type is null group by groupdocument_gid)";
            }
            else
            {
                msSQL = " SELECT groupdocument_gid,groupdocument_name,documenttypes_gid,documenttype_name,covenant_type " +
                       " FROM ocs_mst_tgroupdocument where status='Y' and covenant_type='Y'";
            }

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getDocList = new List<CADDocument>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getDocList.Add(new CADDocument
                    {
                        document_gid = dt["groupdocument_gid"].ToString(),
                        documenttype_gid = dt["documenttypes_gid"].ToString(),
                        document_name = dt["groupdocument_name"].ToString(),
                        documenttype_name = dt["documenttype_name"].ToString(),
                        covenant_type = dt["covenant_type"].ToString(),
                    });
                }
                values.CADDocument = getDocList;
                values.status = true;
                values.message = "Data Fetched";
            }
            else
            {
                values.status = false;
                values.message = "No Record Found";
            }
            dt_datatable.Dispose();

            return true;
        }

        public void DaPostGroupCheckList(MdlMstCAD values, string user_gid, string employee_gid)
        {
            bool lsgroupupdate = false;
            foreach (string i in values.document_gid)
            {

                string lscovenant_type = "";
                msSQL = " select documenttypes_gid,documenttype_name,groupdocument_name,covenant_type " +
                        " from ocs_mst_tgroupdocument where groupdocument_gid='" + i + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lsdocumenttype_gid = objODBCDatareader["documenttypes_gid"].ToString();
                    lsdocumenttype_name = objODBCDatareader["documenttype_name"].ToString();
                    lsgroupdocument_name = objODBCDatareader["groupdocument_name"].ToString();
                    lscovenant_type = objODBCDatareader["covenant_type"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = " select groupdocumentchecklist_gid from ocs_trn_tdocumentchecktls where application_gid='" + values.application_gid + "'" +
                      " and credit_gid='" + values.credit_gid + "' and individualdocument_gid ='" + i + "'";
                string lsgroupdocumentchecklist_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select concat(user_firstname,' ', user_lastname, ' / ', user_code) as username from adm_mst_tuser where user_gid='" + user_gid + "'";
                string tagged_name = objdbconn.GetExecuteScalar(msSQL);
                if (lsgroupdocumentchecklist_gid != "")
                {
                    msSQL = " update ocs_trn_tdocumentchecktls set untagged_type=null, " +
                   " untagged_by='" + user_gid + "', " +
                   " tagged_name='" + tagged_name + "'," +
                   " tagged_by='" + values.taggedby + "'," +
                   " untagged_date=current_timestamp" +
                   " where groupdocumentchecklist_gid='" + lsgroupdocumentchecklist_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update ocs_trn_tgroupdocumentchecklist set untagged_type=null where groupdocumentchecklist_gid='" + lsgroupdocumentchecklist_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                else
                {
                    msGetGID = objcmnfunctions.GetMasterGID("DOCG");
                    msSQL = " insert into ocs_trn_tdocumentchecktls(" +
                            " documentcheckdtl_gid," +
                            " application_gid," +
                            " credit_gid," +
                            " groupdocument_gid, " +
                            " documenttype_gid," +
                            " documenttype_code," +
                            " documenttype_name," +
                            " covenant_type, " +
                            " tagged_by, " +
                            " created_date," +
                            " created_by)" +
                            " VALUES(" +
                            "'" + msGetGID + "'," +
                            "'" + values.application_gid + "'," +
                            "'" + values.credit_gid + "'," +
                            "'" + i + "'," +
                            "'" + lsdocumenttype_gid + "'," +
                            "'" + lsdocumenttype_name + "'," +
                            "'" + lsgroupdocument_name.Replace("'", "") + "'," +
                            "'" + lscovenant_type + "'," +
                            "'" + values.taggedby + "'," +
                            "current_timestamp," +
                            "'" + user_gid + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    lsgroupupdate = true;
                }
            }

            if (lsgroupupdate)
            {
                DaMstScannedDocument objvalues = new DaMstScannedDocument();
                objvalues.DaGroupDocChecklistinfoCredit(values.application_gid, values.credit_gid, employee_gid);
            }

            if (mnResult == 1)
            {
                values.status = true;
                values.message = "Document Type Tagged Successfully";
            }
            else
            {
                values.status = true;
                values.message = "Error Occured";
            }
        }
        //OldCV
        //public bool DaGetCADTaggedDocList(MdlMstCAD values, string credit_gid)
        //{
        //    msSQL = "select institution2documentupload_gid from ocs_mst_tinstitution2documentupload where institution_gid='" + credit_gid + "'";
        //    string lsinstitution = objdbconn.GetExecuteScalar(msSQL);
        //    msSQL = "select contact2document_gid from ocs_mst_tcontact2document where contact_gid='" + credit_gid + "'";
        //    string lsindividual = objdbconn.GetExecuteScalar(msSQL);
        //    msSQL = "select group2document_gid from ocs_mst_tgroup2document where group_gid='" + credit_gid + "'";
        //    string lsgroup = objdbconn.GetExecuteScalar(msSQL);
        //    if (lsinstitution != "")
        //    {
        //        msSQL = " SELECT documentcheckdtl_gid,documenttype_gid,documenttype_code,a.documenttype_name,a.tagged_by as taggedby, b.covenant_type,application_gid," +
        //           " (SELECT COUNT(*) FROM ocs_trn_tdocumentchecktls x" +
        //           " WHERE x.documenttype_name = a.documenttype_name AND x.credit_gid = a.credit_gid GROUP BY x.documenttype_name) as documenttype_count" +
        //           " FROM ocs_trn_tdocumentchecktls a" +
        //           " left join ocs_mst_tcompanydocument b on a.companydocument_gid = b.companydocument_gid " +
        //           " WHERE credit_gid = '" + credit_gid + "'";
        //    }
        //    else if (lsindividual != "")
        //    {
        //        msSQL = " SELECT documentcheckdtl_gid,documenttype_gid,documenttype_code,a.documenttype_name,a.tagged_by as taggedby,b.covenant_type,application_gid," +
        //         " (SELECT COUNT(*) FROM ocs_trn_tdocumentchecktls x" +
        //         " WHERE x.documenttype_name = a.documenttype_name AND x.credit_gid = a.credit_gid GROUP BY x.documenttype_name) as documenttype_count" +
        //         " FROM ocs_trn_tdocumentchecktls a" +
        //         " left join ocs_mst_tindividualdocument b on a.individualdocument_gid = b.individualdocument_gid " +
        //         " WHERE credit_gid = '" + credit_gid + "'";
        //    }
        //    else if (lsgroup != "")
        //    {
        //        msSQL = " SELECT documentcheckdtl_gid,documenttype_gid,documenttype_code,a.documenttype_name,a.tagged_by as taggedby,b.covenant_type,application_gid," +
        //         " (SELECT COUNT(*) FROM ocs_trn_tdocumentchecktls x" +
        //         " WHERE x.documenttype_name = a.documenttype_name AND x.credit_gid = a.credit_gid GROUP BY x.documenttype_name) as documenttype_count" +
        //         " FROM ocs_trn_tdocumentchecktls a" +
        //         " left join ocs_mst_tgroupdocument b on a.groupdocument_gid = b.groupdocument_gid " +
        //         " WHERE credit_gid = '" + credit_gid + "'";
        //    }
        //    dt_datatable = objdbconn.GetDataTable(msSQL);
        //    dt_datatable.Columns.Add("institution2documentupload_gid", typeof(String));
        //    dt_datatable.Columns.Add("contact2document_gid", typeof(String));
        //    dt_datatable.Columns.Add("group2document_gid", typeof(String));

        //    msSQL = " select institution2documentupload_gid,institution_gid, document_id as documenttype_code,'N' as taggedby, " +
        //            " document_title as  documenttype_name, document_path, b.covenant_type " +
        //            " from ocs_mst_tinstitution2documentupload a " +
        //            " left join ocs_mst_tcompanydocument b on a.companydocument_gid = b.companydocument_gid " +
        //            " where institution_gid = '" + credit_gid + "' and untagged_type is null";
        //    dt_child = objdbconn.GetDataTable(msSQL);

        //    msSQL = " select contact2document_gid,document_gid, b.documenttype_name as documenttype_code,'N' as taggedby, " +
        //            " document_title as documenttype_name, document_name, document_path, b.covenant_type " +
        //            " from ocs_mst_tcontact2document a " +
        //            " left join ocs_mst_tindividualdocument b on a.individualdocument_gid = b.individualdocument_gid " +
        //            " where contact_gid = '" + credit_gid + "' and untagged_type is null";
        //    dt_childindividual = objdbconn.GetDataTable(msSQL);

        //    msSQL = " select group2document_gid,document_gid, b.documenttype_name as documenttype_code,'N' as taggedby, " +
        //            " document_title as documenttype_name, document_name, document_path, b.covenant_type " +
        //            " from ocs_mst_tgroup2document a " +
        //            " left join ocs_mst_tgroupdocument b on a.groupdocument_gid = b.groupdocument_gid " +
        //            " where group_gid = '" + credit_gid + "' and untagged_type is null";
        //    dt_childgroup = objdbconn.GetDataTable(msSQL);

        //    if (dt_child.Rows.Count != 0)
        //    {
        //        dt_datatable.Merge(dt_child);
        //        dt_datatable.AcceptChanges();
        //    }
        //    else if (dt_childindividual.Rows.Count != 0)
        //    {
        //        dt_datatable.Merge(dt_childindividual);
        //        dt_datatable.AcceptChanges();
        //    }
        //    else if (dt_childgroup.Rows.Count != 0)
        //    {
        //        dt_datatable.Merge(dt_childgroup);
        //        dt_datatable.AcceptChanges();
        //    }

        //    if (dt_datatable.Rows.Count != 0)
        //    {
        //        values.TaggedDocument = dt_datatable.AsEnumerable().Select(row => new TaggedDocument
        //        {
        //            documentcheckdtl_gid = row["documentcheckdtl_gid"].ToString(),
        //            documenttype_gid = row["documenttype_gid"].ToString(),
        //            documenttype_code = row["documenttype_code"].ToString(),
        //            documenttype_name = row["documenttype_name"].ToString(),
        //            documenttype_count = row["documenttype_count"].ToString(),
        //            covenant_type = row["covenant_type"].ToString(),
        //            institution2documentupload_gid = row["institution2documentupload_gid"].ToString(),
        //            individual2document_gid = row["contact2document_gid"].ToString(),
        //            group2document_gid = row["group2document_gid"].ToString(),
        //            taggedby = row["taggedby"].ToString(),
        //        }).ToList();
        //        values.status = true;
        //        values.message = "Success";
        //    }
        //    else
        //    {
        //        values.status = false;
        //        values.message = "No Record Found";
        //    }
        //    dt_datatable.Dispose();
        //    return true;
        //}

        public bool DaGetCADTrnTaggedDocList(MdlMstCAD values, string credit_gid)
        {
            msSQL = "select institution_gid from ocs_mst_tinstitution where institution_gid='" + credit_gid + "'";
            string lsinstitution = objdbconn.GetExecuteScalar(msSQL);
            msSQL = "select contact_gid from ocs_mst_tcontact where contact_gid='" + credit_gid + "'";
            string lsindividual = objdbconn.GetExecuteScalar(msSQL);
            msSQL = "select group_gid from ocs_mst_tgroup where group_gid='" + credit_gid + "'";
            string lsgroup = objdbconn.GetExecuteScalar(msSQL);

            if (lsinstitution != "")
            {
                msSQL = " SELECT groupdocumentchecklist_gid, documentcheckdtl_gid,documenttype_gid,documenttype_code,a.documenttype_name,a.tagged_by, " +
                        " a.tagged_name, a.covenant_type,application_gid " +
                        " FROM ocs_trn_tdocumentchecktls a" +
                        " WHERE credit_gid = '" + credit_gid + "' and a.untagged_type is null or a.untagged_type = 'N' GROUP BY a.companydocument_gid";
            }
            else if (lsindividual != "")
            {

                msSQL = " SELECT groupdocumentchecklist_gid, documentcheckdtl_gid,documenttype_gid,documenttype_code,a.documenttype_name,a.tagged_by, " +
                       " a.tagged_name, a.covenant_type,application_gid " +
                       " FROM ocs_trn_tdocumentchecktls a" +
                       " WHERE credit_gid = '" + credit_gid + "' and a.untagged_type is null or a.untagged_type = 'N' GROUP BY a.individualdocument_gid";
            }
            else if (lsgroup != "")
            {
                msSQL = " SELECT groupdocumentchecklist_gid, documentcheckdtl_gid,documenttype_gid,documenttype_code,a.documenttype_name,a.tagged_by, " +
                       " a.tagged_name, a.covenant_type,application_gid " +
                       " FROM ocs_trn_tdocumentchecktls a" +
                       " WHERE credit_gid = '" + credit_gid + "' and a.untagged_type is null or a.untagged_type = 'N' GROUP BY a.groupdocument_gid";
            }
            dt_datatable = objdbconn.GetDataTable(msSQL);

            if (dt_datatable.Rows.Count != 0)
            {
                values.TaggedDocument = dt_datatable.AsEnumerable().Select(row => new TaggedDocument
                {
                    groupdocumentchecklist_gid = row["groupdocumentchecklist_gid"].ToString(),
                    documenttype_gid = row["documenttype_gid"].ToString(),
                    documenttype_code = row["documenttype_code"].ToString(),
                    documenttype_name = row["documenttype_name"].ToString(),
                    covenant_type = row["covenant_type"].ToString(),
                    taggedby = row["tagged_by"].ToString(),
                    tagged_name = row["tagged_name"].ToString(),
                }).ToList();
                values.status = true;
                values.message = "Success";
            }
            else
            {
                values.status = false;
                values.message = "No Record Found";
            }
            dt_datatable.Dispose();
            return true;
        }

        public bool DaGetCADTrnCovenantTaggedDocList(MdlMstCAD values, string credit_gid)
        {
            msSQL = "select institution_gid from ocs_mst_tinstitution where institution_gid='" + credit_gid + "'";
            string lsinstitution = objdbconn.GetExecuteScalar(msSQL);
            msSQL = "select contact_gid from ocs_mst_tcontact where contact_gid='" + credit_gid + "'";
            string lsindividual = objdbconn.GetExecuteScalar(msSQL);
            msSQL = "select group_gid from ocs_mst_tgroup where group_gid='" + credit_gid + "'";
            string lsgroup = objdbconn.GetExecuteScalar(msSQL);

            if (lsinstitution != "")
            {
                msSQL = " SELECT groupcovdocumentchecklist_gid,a.companydocument_gid,documenttype_gid,documenttype_code,a.documenttype_name,a.covenant_periods, " +
                        " a.covenant_type,a.buffer_days,application_gid,a.tagged_name, a.tagged_by as taggedby  " +
                        " FROM ocs_trn_tcovanantdocumentcheckdtls a" +
                        " WHERE credit_gid = '" + credit_gid + "' and a.covenant_type='Y' and a.untagged_type is null or a.untagged_type = 'N'" +
                        " GROUP BY a.companydocument_gid";
            }
            else if (lsindividual != "")
            {
                msSQL = " SELECT groupcovdocumentchecklist_gid,a.individualdocument_gid as companydocument_gid,documenttype_gid,documenttype_code, " +
                        " a.documenttype_name,a.covenant_periods, a.covenant_type,a.buffer_days,application_gid,a.tagged_name, a.tagged_by as taggedby  " +
                       " FROM ocs_trn_tcovanantdocumentcheckdtls a" +
                       " WHERE credit_gid = '" + credit_gid + "' and a.covenant_type='Y' and a.untagged_type is null or a.untagged_type = 'N'" +
                       " GROUP BY a.individualdocument_gid";
            }
            else if (lsgroup != "")
            {
                msSQL = " SELECT groupcovdocumentchecklist_gid,a.groupdocument_gid as companydocument_gid,documenttype_gid,documenttype_code, " +
                      " a.documenttype_name,a.covenant_periods, a.covenant_type,a.buffer_days,application_gid,a.tagged_name, a.tagged_by as taggedby " +
                     " FROM ocs_trn_tcovanantdocumentcheckdtls a" +
                     " WHERE credit_gid = '" + credit_gid + "' and a.covenant_type='Y' and a.untagged_type is null or a.untagged_type = 'N'" +
                     " GROUP BY a.groupdocument_gid";
            }
            dt_datatable = objdbconn.GetDataTable(msSQL);

            if (dt_datatable.Rows.Count != 0)
            {
                values.TaggedDocument = dt_datatable.AsEnumerable().Select(row => new TaggedDocument
                {
                    groupcovdocumentchecklist_gid = row["groupcovdocumentchecklist_gid"].ToString(),
                    documenttype_gid = row["documenttype_gid"].ToString(),
                    documenttype_code = row["documenttype_code"].ToString(),
                    documenttype_name = row["documenttype_name"].ToString(),
                    covenant_type = row["covenant_type"].ToString(),
                    companydocument_gid = row["companydocument_gid"].ToString(),
                    taggedby = row["taggedby"].ToString(),
                    covenantperiod = row["covenant_periods"].ToString(),
                    tagged_name = row["tagged_name"].ToString(),
                    buffer_days = row["buffer_days"].ToString(),
                }).ToList();
                values.status = true;
                values.message = "Success";
            }
            else
            {
                values.status = false;
                values.message = "No Record Found";
            }
            dt_datatable.Dispose();
            return true;
        }

        public bool DaGetCADTaggedDocList(MdlMstCAD values, string credit_gid)
        {
            msSQL = "select institution_gid from ocs_mst_tinstitution where institution_gid='" + credit_gid + "'";
            string lsinstitution = objdbconn.GetExecuteScalar(msSQL);
            msSQL = "select contact_gid from ocs_mst_tcontact where contact_gid='" + credit_gid + "'";
            string lsindividual = objdbconn.GetExecuteScalar(msSQL);
            msSQL = "select group_gid from ocs_mst_tgroup where group_gid='" + credit_gid + "'";
            string lsgroup = objdbconn.GetExecuteScalar(msSQL);

            if (lsinstitution != "")
            {
                msSQL = " SELECT groupdocumentchecklist_gid,documentcheckdtl_gid,documenttype_gid,documenttype_code,a.documenttype_name, " +
                       " a.tagged_by, b.covenant_type,application_gid FROM ocs_trn_tdocumentchecktls a " +
                       " left join ocs_mst_tcompanydocument b on a.companydocument_gid = b.companydocument_gid " +
                       " WHERE credit_gid = '" + credit_gid + "' and a.untagged_type is null or a.untagged_type = 'N' GROUP BY a.companydocument_gid";
            }
            else if (lsindividual != "")
            {
                msSQL = " SELECT groupdocumentchecklist_gid,documentcheckdtl_gid,documenttype_gid,documenttype_code,a.documenttype_name, " +
                     " a.tagged_by, b.covenant_type,application_gid FROM ocs_trn_tdocumentchecktls a " +
                     " left join ocs_mst_tindividualdocument b on a.individualdocument_gid = b.individualdocument_gid " +
                     " WHERE credit_gid = '" + credit_gid + "' and a.untagged_type is null or a.untagged_type = 'N' GROUP BY a.individualdocument_gid";

            }
            else if (lsgroup != "")
            {
                msSQL = " SELECT groupdocumentchecklist_gid,documentcheckdtl_gid,documenttype_gid,documenttype_code,a.documenttype_name, " +
                  " a.tagged_by, b.covenant_type,application_gid FROM ocs_trn_tdocumentchecktls a " +
                  " left join ocs_mst_tgroupdocument b on a.groupdocument_gid = b.groupdocument_gid " +
                  " WHERE credit_gid = '" + credit_gid + "' and a.untagged_type is null or a.untagged_type = 'N' GROUP BY a.groupdocument_gid";

            }
            dt_datatable = objdbconn.GetDataTable(msSQL);

            if (dt_datatable.Rows.Count != 0)
            {
                values.TaggedDocument = dt_datatable.AsEnumerable().Select(row => new TaggedDocument
                {
                    groupdocumentchecklist_gid = row["groupdocumentchecklist_gid"].ToString(),
                    documenttype_gid = row["documenttype_gid"].ToString(),
                    documenttype_code = row["documenttype_code"].ToString(),
                    documenttype_name = row["documenttype_name"].ToString(),
                    // documenttype_count = row["documenttype_count"].ToString(),
                    covenant_type = row["covenant_type"].ToString(),
                    taggedby = row["tagged_by"].ToString(),
                }).ToList();
                values.status = true;
                values.message = "Success";
            }
            else
            {
                values.status = false;
                values.message = "No Record Found";
            }
            dt_datatable.Dispose();
            return true;
        }

        public bool DaGetCADCovenantTaggedDocList(MdlMstCAD values, string credit_gid)
        {
            msSQL = "select institution_gid from ocs_mst_tinstitution where institution_gid='" + credit_gid + "'";
            string lsinstitution = objdbconn.GetExecuteScalar(msSQL);
            msSQL = "select contact_gid from ocs_mst_tcontact where contact_gid='" + credit_gid + "'";
            string lsindividual = objdbconn.GetExecuteScalar(msSQL);
            msSQL = "select group_gid from ocs_mst_tgroup where group_gid='" + credit_gid + "'";
            string lsgroup = objdbconn.GetExecuteScalar(msSQL);

            //msSQL = "select institution2documentupload_gid from ocs_mst_tinstitution2documentupload where institution_gid='" + credit_gid + "'";
            //string lsinstitution = objdbconn.GetExecuteScalar(msSQL);
            //msSQL = "select contact2document_gid from ocs_mst_tcontact2document where contact_gid='" + credit_gid + "'";
            //string lsindividual = objdbconn.GetExecuteScalar(msSQL);
            //msSQL = "select group2document_gid from ocs_mst_tgroup2document where group_gid='" + credit_gid + "'";
            //string lsgroup = objdbconn.GetExecuteScalar(msSQL);


            if (lsinstitution != "")
            {
                msSQL = " SELECT groupcovdocumentchecklist_gid,covenantdocumentcheckdtl_gid,a.companydocument_gid,documenttype_gid, " +
                    " documenttype_code,a.documenttype_name,a.covenant_periods, b.covenant_type, application_gid,a.buffer_days,a.tagged_by as taggedby  " +
                    " FROM ocs_trn_tcovanantdocumentcheckdtls a" +
                    " left join ocs_mst_tcompanydocument b on a.companydocument_gid = b.companydocument_gid " +
                    " WHERE credit_gid = '" + credit_gid + "' and b.covenant_type='Y' and a.untagged_type is null or a.untagged_type = 'N' " +
                    " GROUP BY a.companydocument_gid";
            }
            else if (lsindividual != "")
            {
                msSQL = " SELECT groupcovdocumentchecklist_gid,covenantdocumentcheckdtl_gid,a.individualdocument_gid as companydocument_gid,documenttype_gid, " +
                    " documenttype_code,a.documenttype_name,a.covenant_periods, b.covenant_type, application_gid,a.buffer_days, a.tagged_by as taggedby  " +
                    " FROM ocs_trn_tcovanantdocumentcheckdtls a" +
                    " left join ocs_mst_tindividualdocument b on a.individualdocument_gid = b.individualdocument_gid " +
                    " WHERE credit_gid = '" + credit_gid + "' and b.covenant_type='Y' and a.untagged_type is null or a.untagged_type = 'N' " +
                    " GROUP BY a.individualdocument_gid";
            }
            else if (lsgroup != "")
            {
                msSQL = " SELECT groupcovdocumentchecklist_gid,covenantdocumentcheckdtl_gid,a.groupdocument_gid as companydocument_gid,documenttype_gid, " +
                    " documenttype_code,a.documenttype_name,a.covenant_periods, b.covenant_type, application_gid,a.buffer_days, a.tagged_by as taggedby  " +
                    " FROM ocs_trn_tcovanantdocumentcheckdtls a" +
                    " left join ocs_mst_tgroupdocument b on a.groupdocument_gid = b.groupdocument_gid " +
                    " WHERE credit_gid = '" + credit_gid + "' and b.covenant_type='Y' and a.untagged_type is null or a.untagged_type = 'N' " +
                    " GROUP BY a.groupdocument_gid";
            }
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.TaggedDocument = dt_datatable.AsEnumerable().Select(row => new TaggedDocument
                {
                    groupcovdocumentchecklist_gid = row["groupcovdocumentchecklist_gid"].ToString(),
                    documenttype_gid = row["documenttype_gid"].ToString(),
                    documenttype_code = row["documenttype_code"].ToString(),
                    documenttype_name = row["documenttype_name"].ToString(),
                    covenant_type = row["covenant_type"].ToString(),
                    companydocument_gid = row["companydocument_gid"].ToString(),
                    taggedby = row["taggedby"].ToString(),
                    covenantperiod = row["covenant_periods"].ToString(),
                    buffer_days = row["buffer_days"].ToString(),
                }).ToList();
                values.status = true;
                values.message = "Success";
            }
            else
            {
                values.status = false;
                values.message = "No Record Found";
            }
            dt_datatable.Dispose();
            return true;
        }

        //OldCV
        //public bool DaGetCADCovenantTaggedDocList(MdlMstCAD values, string credit_gid)
        //{
        //    msSQL = "select institution2documentupload_gid from ocs_mst_tinstitution2documentupload where institution_gid='" + credit_gid + "'";
        //    string lsinstitution = objdbconn.GetExecuteScalar(msSQL);
        //    msSQL = "select contact2document_gid from ocs_mst_tcontact2document where contact_gid='" + credit_gid + "'";
        //    string lsindividual = objdbconn.GetExecuteScalar(msSQL);
        //    msSQL = "select group2document_gid from ocs_mst_tgroup2document where group_gid='" + credit_gid + "'";
        //    string lsgroup = objdbconn.GetExecuteScalar(msSQL);
        //    if (lsinstitution != "")
        //    {
        //        msSQL = " SELECT covenantdocumentcheckdtl_gid,a.companydocument_gid,documenttype_gid,documenttype_code,a.documenttype_name,a.covenant_periods, " +
        //            " b.covenant_type,application_gid, a.tagged_by as taggedby, " +
        //            " (SELECT COUNT(*) FROM ocs_trn_tcovanantdocumentcheckdtls x" +
        //            " WHERE x.documenttype_name = a.documenttype_name AND x.credit_gid = a.credit_gid GROUP BY x.documenttype_name) as documenttype_count" +
        //            " FROM ocs_trn_tcovanantdocumentcheckdtls a" +
        //            " left join ocs_mst_tcompanydocument b on a.companydocument_gid = b.companydocument_gid " +
        //            " WHERE credit_gid = '" + credit_gid + "' and b.covenant_type='Y'";
        //    }
        //    else if (lsindividual != "")
        //    {
        //        msSQL = " SELECT covenantdocumentcheckdtl_gid,a.individualdocument_gid as companydocument_gid,documenttype_gid,documenttype_code,a.covenant_periods, " +
        //                " a.documenttype_name,b.covenant_type,application_gid, a.tagged_by as taggedby," +
        //         " (SELECT COUNT(*) FROM ocs_trn_tcovanantdocumentcheckdtls x" +
        //         " WHERE x.documenttype_name = a.documenttype_name AND x.credit_gid = a.credit_gid GROUP BY x.documenttype_name) as documenttype_count" +
        //         " FROM ocs_trn_tcovanantdocumentcheckdtls a" +
        //         " left join ocs_mst_tindividualdocument b on a.individualdocument_gid = b.individualdocument_gid " +
        //         " WHERE credit_gid = '" + credit_gid + "'";
        //    }
        //    else if (lsgroup != "")
        //    {
        //        msSQL = " SELECT covenantdocumentcheckdtl_gid,a.groupdocument_gid as companydocument_gid,documenttype_gid,documenttype_code,a.covenant_periods, " +
        //            " a.documenttype_name,b.covenant_type,application_gid,a.tagged_by as taggedby," +
        //         " (SELECT COUNT(*) FROM ocs_trn_tdocumentchecktls x" +
        //         " WHERE x.documenttype_name = a.documenttype_name AND x.credit_gid = a.credit_gid GROUP BY x.documenttype_name) as documenttype_count" +
        //         " FROM ocs_trn_tcovanantdocumentcheckdtls a" +
        //         " left join ocs_mst_tgroupdocument b on a.groupdocument_gid = b.groupdocument_gid " +
        //         " WHERE credit_gid = '" + credit_gid + "'";
        //    }
        //    dt_datatable = objdbconn.GetDataTable(msSQL);
        //    dt_datatable.Columns.Add("institution2documentupload_gid", typeof(String));
        //    dt_datatable.Columns.Add("contact2document_gid", typeof(String));
        //    dt_datatable.Columns.Add("group2document_gid", typeof(String));

        //    msSQL = " select institution2documentupload_gid,a.companydocument_gid,institution_gid, document_id as documenttype_code, " +
        //            " 'N' as taggedby, document_title as  documenttype_name, document_path, b.covenant_type,a.covenant_periods " +
        //            " from ocs_mst_tinstitution2documentupload a " +
        //            " left join ocs_mst_tcompanydocument b on a.companydocument_gid = b.companydocument_gid " +
        //            " where institution_gid = '" + credit_gid + "' and untagged_type is null and b.covenant_type='Y'";
        //    dt_child = objdbconn.GetDataTable(msSQL);

        //    msSQL = " select contact2document_gid,document_gid,a.individualdocument_gid as companydocument_gid, b.documenttype_name as documenttype_code, " +
        //            " 'N' as taggedby, document_title as documenttype_name, document_name, document_path, b.covenant_type,a.covenant_periods " +
        //            " from ocs_mst_tcontact2document a " +
        //            " left join ocs_mst_tindividualdocument b on a.individualdocument_gid = b.individualdocument_gid " +
        //            " where contact_gid = '" + credit_gid + "' and untagged_type is null and b.covenant_type='Y'";
        //    dt_childindividual = objdbconn.GetDataTable(msSQL);

        //    msSQL = " select  group2document_gid,document_gid,a.groupdocument_gid as companydocument_gid, b.documenttype_name as documenttype_code, " +
        //            " 'N' as taggedby, document_title as documenttype_name, document_name, document_path, b.covenant_type,a.covenant_periods " +
        //            " from ocs_mst_tgroup2document a " +
        //            " left join ocs_mst_tgroupdocument b on a.groupdocument_gid = b.groupdocument_gid " +
        //            " where group_gid = '" + credit_gid + "' and untagged_type is null and b.covenant_type='Y'";
        //    dt_childgroup = objdbconn.GetDataTable(msSQL);

        //    if (dt_child.Rows.Count != 0)
        //    {
        //        dt_datatable.Merge(dt_child);
        //        dt_datatable.AcceptChanges();
        //    }
        //    else if (dt_childindividual.Rows.Count != 0)
        //    {
        //        dt_datatable.Merge(dt_childindividual);
        //        dt_datatable.AcceptChanges();
        //    }
        //    else if (dt_childgroup.Rows.Count != 0)
        //    {
        //        dt_datatable.Merge(dt_childgroup);
        //        dt_datatable.AcceptChanges();
        //    }

        //    if (dt_datatable.Rows.Count != 0)
        //    {
        //        values.TaggedDocument = dt_datatable.AsEnumerable().Select(row => new TaggedDocument
        //        {
        //            documentcheckdtl_gid = row["covenantdocumentcheckdtl_gid"].ToString(),
        //            documenttype_gid = row["documenttype_gid"].ToString(),
        //            documenttype_code = row["documenttype_code"].ToString(),
        //            documenttype_name = row["documenttype_name"].ToString(),
        //            documenttype_count = row["documenttype_count"].ToString(),
        //            covenant_type = row["covenant_type"].ToString(),
        //            companydocument_gid = row["companydocument_gid"].ToString(),
        //            institution2documentupload_gid = row["institution2documentupload_gid"].ToString(),
        //            individual2document_gid = row["contact2document_gid"].ToString(),
        //            group2document_gid = row["group2document_gid"].ToString(),
        //            taggedby = row["taggedby"].ToString(),
        //            covenantperiod = row["covenant_periods"].ToString(),
        //        }).ToList();
        //        values.status = true;
        //        values.message = "Success";
        //    }
        //    else
        //    {
        //        values.status = false;
        //        values.message = "No Record Found";
        //    }
        //    dt_datatable.Dispose();
        //    return true;
        //}

        public void DaUnTagDocument(string documentcheckdtl_gid, result objResult, string user_gid)
        {


            msSQL = "select groupdocumentchecklist_gid from ocs_trn_tgroupdocumentchecklist where groupdocumentchecklist_gid='" + documentcheckdtl_gid + "'";
            string lsdeferral = objdbconn.GetExecuteScalar(msSQL);
            if (lsdeferral != "")
            {
                msSQL = " update ocs_trn_tdocumentchecktls set untagged_type='Y', " +
                         " com_gur_flag= 'N',ind_gur_flag='N',com_mem_flag='N',ind_mem_flag='N'," +
                         " untagged_by='" + user_gid + "', " +
                         " untagged_date=current_timestamp " +
                         " where groupdocumentchecklist_gid='" + documentcheckdtl_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update ocs_trn_tgroupdocumentchecklist set untagged_type='Y' where groupdocumentchecklist_gid='" + documentcheckdtl_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            }
            else
            {
                msSQL = " update ocs_trn_tcovanantdocumentcheckdtls set untagged_type='Y', " +
                      " com_gur_flag= 'N',ind_gur_flag='N',com_mem_flag='N',ind_mem_flag='N'," +
                    " untagged_by='" + user_gid + "', " +
                    " untagged_date=current_timestamp" +
                    " where groupcovdocumentchecklist_gid='" + documentcheckdtl_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update ocs_trn_tgroupcovenantdocumentchecklist set untagged_type='Y' where groupcovdocumentchecklist_gid='" + documentcheckdtl_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            //msSQL = "select covenantdocumentcheckdtl_gid from ocs_trn_tcovanantdocumentcheckdtls where covenantdocumentcheckdtl_gid='" + documentcheckdtl_gid + "'";
            //string lscovenant = objdbconn.GetExecuteScalar(msSQL);
            //if (lscovenant != "")
            //    msSQL = "DELETE FROM ocs_trn_tcovanantdocumentcheckdtls WHERE covenantdocumentcheckdtl_gid='" + documentcheckdtl_gid + "'";
            //else
            //    msSQL = "DELETE FROM ocs_trn_tdocumentchecktls WHERE documentcheckdtl_gid='" + documentcheckdtl_gid + "'";
            //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            //msSQL = "select institution2documentupload_gid from ocs_mst_tinstitution2documentupload where institution2documentupload_gid='" + documentcheckdtl_gid + "'";
            //string lsinstitution = objdbconn.GetExecuteScalar(msSQL);
            //msSQL = "select contact2document_gid from ocs_mst_tcontact2document where contact2document_gid='" + documentcheckdtl_gid + "'";
            //string lsindividual = objdbconn.GetExecuteScalar(msSQL);
            //msSQL = "select group2document_gid from ocs_mst_tgroup2document where group2document_gid='" + documentcheckdtl_gid + "'";
            //string lsgroup = objdbconn.GetExecuteScalar(msSQL);
            //if (lsinstitution != "")
            //{

            //}
            //else if (lsindividual != "")
            //{
            //    msSQL = " update ocs_mst_tcontact2document set untagged_type='Y', " +
            //       " untagged_by='" + user_gid + "', " +
            //       " untagged_date=current_timestamp" +
            //       " where contact2document_gid='" + documentcheckdtl_gid + "'";
            //    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            //}
            //else if (lsgroup != "")
            //{
            //    msSQL = " update ocs_mst_tgroup2document set untagged_type='Y', " +
            //       " untagged_by='" + user_gid + "', " +
            //       " untagged_date=current_timestamp" +
            //       " where group2document_gid='" + documentcheckdtl_gid + "'";
            //    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            //}
            //else
            //{
            //    msSQL = "DELETE FROM ocs_trn_tdocumentchecktls WHERE documentcheckdtl_gid='" + documentcheckdtl_gid + "'";
            //    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            //}
            if (mnResult == 1)
            {
                objResult.status = true;
                objResult.message = "Document Type UnTagged Successfully..!";
            }
            else
            {
                objResult.status = false;
                objResult.message = "Error Occured";
            }
        }

        public void DaCheckALLDocumentList(MdlMstCAD values, string user_gid)
        {
            if (values.applicant_type == "Institution")
            {
                msSQL = " select institution_gid from ocs_mst_tinstitution where application_gid = '" + values.application_gid + "' and institution_gid<> '" + values.credit_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        msSQL = " select documenttype_gid, documenttype_code, documenttype_name from ocs_trn_tdocumentchecktls where credit_gid='" + values.credit_gid + "'";
                        dt_child = objdbconn.GetDataTable(msSQL);
                        if (dt_child.Rows.Count != 0)
                        {
                            foreach (DataRow dt1 in dt_child.Rows)
                            {
                                msGetGID = objcmnfunctions.GetMasterGID("DOCG");

                                msSQL = " insert into ocs_trn_tdocumentchecktls(" +
                                        " documentcheckdtl_gid," +
                                        " application_gid," +
                                        " credit_gid," +
                                        " documenttype_gid," +
                                        " documenttype_code," +
                                        " documenttype_name," +
                                        " created_date," +
                                        " created_by)" +
                                        " VALUES(" +
                                        "'" + msGetGID + "'," +
                                        "'" + values.application_gid + "'," +
                                        "'" + dt["institution_gid"].ToString() + "'," +
                                        "'" + dt1["documenttype_gid"].ToString() + "'," +
                                        "'" + dt1["documenttype_code"].ToString() + "'," +
                                        "'" + dt1["documenttype_name"].ToString() + "'," +
                                        "current_timestamp," +
                                        "'" + user_gid + "')";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }
                        }
                        dt_child.Dispose();
                    }
                    if (mnResult == 1)
                    {
                        values.status = true;
                        values.message = "Document Type Tagged Successfully";
                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error Occured";
                    }
                }
                dt_datatable.Dispose();
            }
            else if (values.applicant_type == "Individual")
            {
                msSQL = " select contact_gid from ocs_mst_tcontact where application_gid = '" + values.application_gid + "' and contact_gid<> '" + values.credit_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        msSQL = " select documenttype_gid, documenttype_code, documenttype_name from ocs_trn_tdocumentchecktls where credit_gid='" + values.credit_gid + "'";
                        dt_child = objdbconn.GetDataTable(msSQL);
                        if (dt_child.Rows.Count != 0)
                        {
                            foreach (DataRow dt1 in dt_child.Rows)
                            {
                                msGetGID = objcmnfunctions.GetMasterGID("DOCG");

                                msSQL = " insert into ocs_trn_tdocumentchecktls(" +
                                        " documentcheckdtl_gid," +
                                        " application_gid," +
                                        " credit_gid," +
                                        " documenttype_gid," +
                                        " documenttype_code," +
                                        " documenttype_name," +
                                        " created_date," +
                                        " created_by)" +
                                        " VALUES(" +
                                        "'" + msGetGID + "'," +
                                        "'" + values.application_gid + "'," +
                                        "'" + dt["contact_gid"].ToString() + "'," +
                                        "'" + dt1["documenttype_gid"].ToString() + "'," +
                                        "'" + dt1["documenttype_code"].ToString() + "'," +
                                        "'" + dt1["documenttype_name"].ToString() + "'," +
                                        "current_timestamp," +
                                        "'" + user_gid + "')";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }
                        }
                        dt_child.Dispose();
                    }
                    if (mnResult == 1)
                    {
                        values.status = true;
                        values.message = "Document Type Tagged Successfully";
                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error Occured";
                    }
                }
                dt_datatable.Dispose();
            }
            else
            {
                msSQL = " select group_gid from ocs_mst_tgroup where application_gid = '" + values.application_gid + "' and group_gid<> '" + values.credit_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        msSQL = " select documenttype_gid, documenttype_code, documenttype_name from ocs_trn_tdocumentchecktls where credit_gid='" + values.credit_gid + "'";
                        dt_child = objdbconn.GetDataTable(msSQL);
                        if (dt_child.Rows.Count != 0)
                        {
                            foreach (DataRow dt1 in dt_child.Rows)
                            {
                                msGetGID = objcmnfunctions.GetMasterGID("DOCG");

                                msSQL = " insert into ocs_trn_tdocumentchecktls(" +
                                        " documentcheckdtl_gid," +
                                        " application_gid," +
                                        " credit_gid," +
                                        " documenttype_gid," +
                                        " documenttype_code," +
                                        " documenttype_name," +
                                        " created_date," +
                                        " created_by)" +
                                        " VALUES(" +
                                        "'" + msGetGID + "'," +
                                        "'" + values.application_gid + "'," +
                                        "'" + dt["group_gid"].ToString() + "'," +
                                        "'" + dt1["documenttype_gid"].ToString() + "'," +
                                        "'" + dt1["documenttype_code"].ToString() + "'," +
                                        "'" + dt1["documenttype_name"].ToString() + "'," +
                                        "current_timestamp," +
                                        "'" + user_gid + "')";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }
                        }
                        dt_child.Dispose();
                    }
                    if (mnResult == 1)
                    {
                        values.status = true;
                        values.message = "Document Type Tagged Successfully";
                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error Occured";
                    }
                }
                dt_datatable.Dispose();
            }
        }
        public void DaGetSanctionEdit(string application2sanction_gid, cadsanctiondetails values)
        {
            try
            {
                msSQL = " select application2sanction_gid, sanction_refno,  sanction_date, sanction_amount,  entity, " +
                        " entity_gid, application_gid , ccapproved_date, applicationtype_gid, application_type, " +
                        " sanctionto_gid, sanctionto_name, sanctionfrom_date, sanctiontill_date, contactpersonaddress_gid, " +
                        " contactperson_address, contactperson_name, contactperson_number, contactpersonmobileno_gid, " +
                        " contactpersonemail_gid, contactpersonemail_address, sanction_type, natureof_proposal " +
                        " from ocs_trn_tapplication2sanction " +
                        " WHERE application2sanction_gid ='" + application2sanction_gid + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows)
                {
                    values.application_type = objODBCDataReader["application_type"].ToString();
                    values.application_gid = objODBCDataReader["application_gid"].ToString();
                    values.sanctionto_gid = objODBCDataReader["sanctionto_gid"].ToString();
                }
                objODBCDataReader.Close();
                if (values.application_type == "Applicant")
                {
                    msSQL = " select applicant_type from ocs_mst_tapplication where application_gid = '" + values.application_gid + "'";
                    string lsapplicant_type = objdbconn.GetExecuteScalar(msSQL);
                    if (lsapplicant_type == "Institution")
                    {
                        msSQL = " select company_name, institution_gid from ocs_mst_tinstitution where application_gid = '" + values.application_gid + "'" +
                                " and stakeholder_type='Applicant'";
                        dt_datatable = objdbconn.GetDataTable(msSQL);
                        var getsanctionto_list = new List<sanctionto_list>();
                        if (dt_datatable.Rows.Count != 0)
                        {
                            foreach (DataRow dt in dt_datatable.Rows)
                            {
                                getsanctionto_list.Add(new sanctionto_list
                                {
                                    sanctionto_name = dt["company_name"].ToString(),
                                    sanctionto_gid = dt["institution_gid"].ToString(),
                                });
                            }
                        }
                        values.sanctionto_list = getsanctionto_list;
                        dt_datatable.Dispose();
                    }
                    else
                    {
                        msSQL = " select concat(first_name, ' ', middle_name, ' ', last_name) as individual_name, contact_gid from ocs_mst_tcontact" +
                                " where application_gid = '" + values.application_gid + "' and stakeholder_type='Applicant'";
                        dt_datatable = objdbconn.GetDataTable(msSQL);
                        var getsanctionto_list = new List<sanctionto_list>();
                        if (dt_datatable.Rows.Count != 0)
                        {
                            foreach (DataRow dt in dt_datatable.Rows)
                            {
                                getsanctionto_list.Add(new sanctionto_list
                                {
                                    sanctionto_name = dt["individual_name"].ToString(),
                                    sanctionto_gid = dt["contact_gid"].ToString(),
                                });
                            }
                        }
                        values.sanctionto_list = getsanctionto_list;
                        dt_datatable.Dispose();
                    }
                }
                else
                {
                    msSQL = " select a.company_name as sanctionto_name, a.institution_gid as sanctionto_gid from ocs_mst_tinstitution a " +
                            " inner join ocs_mst_tapplication b on a.application_gid = b.application_gid " +
                            " where b.application_gid = '" + values.application_gid + "' and a.stakeholder_type = '" + values.application_type + "'" +
                            " union " +
                            " select concat(first_name, ' ', middle_name, ' ', last_name) as sanctionto_name, a.contact_gid as sanctionto_gid from ocs_mst_tcontact a " +
                            " inner join ocs_mst_tapplication b on a.application_gid = b.application_gid " +
                            " where b.application_gid = '" + values.application_gid + "' and a.stakeholder_type = '" + values.application_type + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getsanctionto_list = new List<sanctionto_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            getsanctionto_list.Add(new sanctionto_list
                            {
                                sanctionto_name = dt["sanctionto_name"].ToString(),
                                sanctionto_gid = dt["sanctionto_gid"].ToString(),
                            });
                        }
                    }
                    values.sanctionto_list = getsanctionto_list;
                    dt_datatable.Dispose();
                }
                // for personal details
                msSQL = " select contact_gid from ocs_mst_tcontact where contact_gid = '" + values.sanctionto_gid + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows == true)
                {
                    objODBCDataReader.Close();
                    msSQL = "select concat(first_name,',',middle_name,',',last_name) as contactperson_name from ocs_mst_tcontact where contact_gid = '" + values.sanctionto_gid + "'";
                    objODBCDataReader1 = objdbconn.GetDataReader(msSQL);
                    if (objODBCDataReader1.HasRows == true)
                    {
                        values.contactperson_name = objODBCDataReader1["contactperson_name"].ToString();
                    }
                    objODBCDataReader1.Close();

                    //msSQL = "select concat(addressline1,',',addressline2) as primary_address from ocs_mst_tcontact2address where contact_gid = '" + sanctionto_gid + "' and primary_status='Yes'";
                    //objODBCDataReader1 = objdbconn.GetDataReader(msSQL);
                    //if (objODBCDataReader1.HasRows == true)
                    //{
                    //    values.primary_address = objODBCDataReader1["primary_address"].ToString();
                    //}
                    //objODBCDataReader1.Close();

                    msSQL = " select concat(addressline1,',',addressline2) as address, contact2address_gid as address_gid from ocs_mst_tcontact2address where contact_gid = '" + values.sanctionto_gid + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var geaddress_list = new List<cadaddress_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            geaddress_list.Add(new cadaddress_list
                            {
                                address_gid = dt["address_gid"].ToString(),
                                address = dt["address"].ToString(),
                            });
                        }
                    }
                    values.cadaddress_list = geaddress_list;
                    dt_datatable.Dispose();

                    msSQL = " select mobile_no, contact2mobileno_gid as mobileno_gid from ocs_mst_tcontact2mobileno where contact_gid = '" + values.sanctionto_gid + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getmobileno_list = new List<cadmobileno_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            getmobileno_list.Add(new cadmobileno_list
                            {
                                mobileno_gid = dt["mobileno_gid"].ToString(),
                                mobile_no = dt["mobile_no"].ToString(),
                            });
                        }
                    }
                    values.cadmobileno_list = getmobileno_list;
                    dt_datatable.Dispose();

                    msSQL = " select email_address, contact2email_gid as email_gid from ocs_mst_tcontact2email where contact_gid = '" + values.sanctionto_gid + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getemail_list = new List<cademail_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            getemail_list.Add(new cademail_list
                            {
                                email_gid = dt["email_gid"].ToString(),
                                email_address = dt["email_address"].ToString(),
                            });
                        }
                    }
                    values.cademail_list = getemail_list;
                    dt_datatable.Dispose();

                }
                else
                {
                    objODBCDataReader.Close();
                    msSQL = "select concat(contactperson_firstname,',',contactperson_middlename,',',contactperson_lastname) as contactperson_name from ocs_mst_tinstitution where institution_gid = '" + values.sanctionto_gid + "'";
                    objODBCDataReader1 = objdbconn.GetDataReader(msSQL);
                    if (objODBCDataReader1.HasRows == true)
                    {
                        values.contactperson_name = objODBCDataReader1["contactperson_name"].ToString();
                    }
                    objODBCDataReader1.Close();

                    //msSQL = "select concat(addressline1,',',addressline2) as primary_address from ocs_mst_tinstitution2address where institution_gid = '" + sanctionto_gid + "' and primary_status='Yes'";
                    //objODBCDataReader1 = objdbconn.GetDataReader(msSQL);
                    //if (objODBCDataReader1.HasRows == true)
                    //{
                    //    values.primary_address = objODBCDataReader1["primary_address"].ToString();
                    //}
                    //objODBCDataReader1.Close();
                    msSQL = " select concat(addressline1,',',addressline2) as address, institution2address_gid as address_gid from ocs_mst_tinstitution2address where institution_gid = '" + values.sanctionto_gid + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var geaddress_list = new List<cadaddress_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            geaddress_list.Add(new cadaddress_list
                            {
                                address_gid = dt["address_gid"].ToString(),
                                address = dt["address"].ToString(),
                            });
                        }
                    }
                    values.cadaddress_list = geaddress_list;
                    dt_datatable.Dispose();

                    msSQL = " select mobile_no, institution2mobileno_gid as mobileno_gid from ocs_mst_tinstitution2mobileno where institution_gid = '" + values.sanctionto_gid + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getmobileno_list = new List<cadmobileno_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            getmobileno_list.Add(new cadmobileno_list
                            {
                                mobileno_gid = dt["mobileno_gid"].ToString(),
                                mobile_no = dt["mobile_no"].ToString(),
                            });
                        }
                    }
                    values.cadmobileno_list = getmobileno_list;
                    dt_datatable.Dispose();

                    msSQL = " select email_address, institution2email_gid as email_gid from ocs_mst_tinstitution2email where institution_gid = '" + values.sanctionto_gid + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getemail_list = new List<cademail_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            getemail_list.Add(new cademail_list
                            {
                                email_gid = dt["email_gid"].ToString(),
                                email_address = dt["email_address"].ToString(),
                            });
                        }
                    }
                    values.cademail_list = getemail_list;
                    dt_datatable.Dispose();
                }


                msSQL = " select application2sanction_gid, sanction_refno,  sanction_date, sanction_amount,  entity, paycard, branch_gid,branch_name,  " +
                        " entity_gid, application_gid , ccapproved_date, applicationtype_gid, application_type,esdeclaration_status, " +
                        " sanctionto_gid, sanctionto_name, sanctionfrom_date, sanctiontill_date, contactpersonaddress_gid, " +
                        " contactperson_address, contactperson_name, contactperson_number, contactpersonmobileno_gid, " +
                        " contactpersonemail_gid, contactpersonemail_address, sanction_type, natureof_proposal " +
                        " from ocs_trn_tapplication2sanction " +
                        " WHERE application2sanction_gid ='" + application2sanction_gid + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows)
                {
                    values.application2sanction_gid = objODBCDataReader["application2sanction_gid"].ToString();
                    values.sanction_refno = objODBCDataReader["sanction_refno"].ToString();
                    values.sanction_amount = objODBCDataReader["sanction_amount"].ToString();
                    values.entity = objODBCDataReader["entity"].ToString();
                    values.entity_gid = objODBCDataReader["entity_gid"].ToString();
                    values.application_gid = objODBCDataReader["application_gid"].ToString();
                    //values.sanction_date = objODBCDataReader["sanctionDate"].ToString();

                    if (objODBCDataReader["sanction_date"].ToString() != "")
                    {
                        values.sanctionDate = Convert.ToDateTime(objODBCDataReader["sanction_date"].ToString());
                    }
                    values.applicationtype_gid = objODBCDataReader["applicationtype_gid"].ToString();
                    values.application_type = objODBCDataReader["application_type"].ToString();
                    values.sanctionto_gid = objODBCDataReader["sanctionto_gid"].ToString();
                    values.sanctionto_name = objODBCDataReader["sanctionto_name"].ToString();
                    values.contactpersonaddress_gid = objODBCDataReader["contactpersonaddress_gid"].ToString();
                    values.contactperson_address = objODBCDataReader["contactperson_address"].ToString();
                    values.contactperson_name = objODBCDataReader["contactperson_name"].ToString();
                    values.contactperson_number = objODBCDataReader["contactperson_number"].ToString();
                    values.contactpersonmobileno_gid = objODBCDataReader["contactpersonmobileno_gid"].ToString();
                    values.contactpersonemail_gid = objODBCDataReader["contactpersonemail_gid"].ToString();
                    values.contactpersonemail_address = objODBCDataReader["contactpersonemail_address"].ToString();
                    values.sanction_type = objODBCDataReader["sanction_type"].ToString();
                    values.natureof_proposal = objODBCDataReader["natureof_proposal"].ToString();
                    values.branch_gid = objODBCDataReader["branch_gid"].ToString();
                    values.branch_name = objODBCDataReader["branch_name"].ToString();
                    if (objODBCDataReader["sanctionfrom_date"].ToString() != "")
                    {
                        values.sanctionfrom_date = Convert.ToDateTime(objODBCDataReader["sanctionfrom_date"]).ToString("dd-MM-yyyy");
                        values.sanctionfromDate = Convert.ToDateTime(objODBCDataReader["sanctionfrom_date"].ToString());
                    }
                    if (objODBCDataReader["sanctiontill_date"].ToString() != "")
                    {
                        values.sanctiontill_date = Convert.ToDateTime(objODBCDataReader["sanctiontill_date"]).ToString("dd-MM-yyyy");
                        values.sanctiontillDate = Convert.ToDateTime(objODBCDataReader["sanctiontill_date"].ToString());
                    }
                    values.paycard = objODBCDataReader["paycard"].ToString();
                    values.esdeclaration_status = objODBCDataReader["esdeclaration_status"].ToString();
                }
                objODBCDataReader.Close();

            }
            catch (Exception ex)
            {
                values.message = ex.ToString();
                values.status = false;
            }
        }
        public bool DaPostUpdateSanction(string employee_gid, cadsanctiondetails values)
        {
            if (values.sanction_type == "Existing Customer")
            {
                if ((values.natureof_proposal == null) || (values.natureof_proposal == ""))
                {
                    values.message = "Kindly Select Nature of Proposal";
                    values.status = false;
                    return false;
                }
            }
            msSQL = " select application2sanction_gid, sanction_refno,  sanction_date, sanction_amount,  entity, paycard, branch_gid,branch_name,  " +
                     " entity_gid, application_gid , ccapproved_date, applicationtype_gid, application_type,esdeclaration_status, " +
                     " sanctionto_gid, sanctionto_name, sanctionfrom_date, sanctiontill_date, contactpersonaddress_gid, " +
                     " contactperson_address, contactperson_name, contactperson_number, contactpersonmobileno_gid, " +
                     " contactpersonemail_gid, contactpersonemail_address, sanction_type, natureof_proposal,created_by,created_date, " +
                     " makerfile_path,makerfile_name,sanctionletter_status,template_content,makersubmitted_by,makersubmitted_on," +
                     " template_name,checkerapproved_by,checkerupdated_on,checkerpushback_remarks,checkerapproval_flag," +
                     " checkerapproved_on,digitalsignature_flag," +
                     " sanctiongenerated_by,sanctiongenerated_on " +
                     " from ocs_trn_tapplication2sanction " +
                     " WHERE application2sanction_gid ='" + values.application2sanction_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows)
            {
                template_name = objODBCDataReader["template_name"].ToString();
                checkerapproved_by = objODBCDataReader["checkerapproved_by"].ToString();
                checkerupdated_on = objODBCDataReader["checkerupdated_on"].ToString();
                checkerpushback_remarks = objODBCDataReader["checkerpushback_remarks"].ToString();
                checkerapproval_flag = objODBCDataReader["checkerapproval_flag"].ToString();
                checkerapproved_on = objODBCDataReader["checkerapproved_on"].ToString();
                digitalsignature_flag = objODBCDataReader["digitalsignature_flag"].ToString();


                makerfile_path = objcmnstorage.EncryptData((objODBCDataReader["makerfile_path"].ToString()));
                makerfile_name = objODBCDataReader["makerfile_name"].ToString();
                sanctionletter_status = objODBCDataReader["sanctionletter_status"].ToString();
                template_content = objODBCDataReader["template_content"].ToString();
                makersubmitted_by = objODBCDataReader["makersubmitted_by"].ToString();
                makersubmitted_on = objODBCDataReader["makersubmitted_on"].ToString();
                sanctiongenerated_by = objODBCDataReader["sanctiongenerated_by"].ToString();
                sanctiongenerated_on = objODBCDataReader["sanctiongenerated_on"].ToString();


                application2sanction_gid = objODBCDataReader["application2sanction_gid"].ToString();
                sanction_refno = objODBCDataReader["sanction_refno"].ToString();
                sanction_amount = objODBCDataReader["sanction_amount"].ToString();
                entity = objODBCDataReader["entity"].ToString();
                entity_gid = objODBCDataReader["entity_gid"].ToString();
                application_gid = objODBCDataReader["application_gid"].ToString();
                //sanction_date = objODBCDataReader["sanctionDate"].ToString();

                if (objODBCDataReader["sanction_date"].ToString() != "")
                {
                    sanction_date = objODBCDataReader["sanction_date"].ToString();
                }
                applicationtype_gid = objODBCDataReader["applicationtype_gid"].ToString();
                application_type = objODBCDataReader["application_type"].ToString();
                sanctionto_gid = objODBCDataReader["sanctionto_gid"].ToString();
                sanctionto_name = objODBCDataReader["sanctionto_name"].ToString();
                contactpersonaddress_gid = objODBCDataReader["contactpersonaddress_gid"].ToString();
                contactperson_address = objODBCDataReader["contactperson_address"].ToString();
                contactperson_name = objODBCDataReader["contactperson_name"].ToString();
                contactperson_number = objODBCDataReader["contactperson_number"].ToString();
                contactpersonmobileno_gid = objODBCDataReader["contactpersonmobileno_gid"].ToString();
                contactpersonemail_gid = objODBCDataReader["contactpersonemail_gid"].ToString();
                contactpersonemail_address = objODBCDataReader["contactpersonemail_address"].ToString();
                sanction_type = objODBCDataReader["sanction_type"].ToString();
                natureof_proposal = objODBCDataReader["natureof_proposal"].ToString();
                branch_gid = objODBCDataReader["branch_gid"].ToString();
                branch_name = objODBCDataReader["branch_name"].ToString();
                if (objODBCDataReader["sanctionfrom_date"].ToString() != "")
                {
                    sanctionfrom_date = objODBCDataReader["sanctionfrom_date"].ToString();
                    //sanctionfromDate = Convert.ToDateTime(objODBCDataReader["sanctionfrom_date"].ToString());
                }
                if (objODBCDataReader["sanctiontill_date"].ToString() != "")
                {
                    sanctiontill_date = objODBCDataReader["sanctiontill_date"].ToString();
                    //sanctiontillDate = Convert.ToDateTime(objODBCDataReader["sanctiontill_date"].ToString());
                }
                paycard = objODBCDataReader["paycard"].ToString();
                esdeclaration_status = objODBCDataReader["esdeclaration_status"].ToString();
                created_by = objODBCDataReader["created_by"].ToString();
                created_date = objODBCDataReader["created_date"].ToString();
            }
            objODBCDataReader.Close();



            msSQL = " update ocs_trn_tapplication2sanction set " +
                     " sanction_date='" + Convert.ToDateTime(values.sanction_date).ToString("yyyy-MM-dd") + "'," +
                     " sanction_type='" + values.sanction_type + "'," +
                     " natureof_proposal='" + values.natureof_proposal + "'," +
                     " applicationtype_gid='" + values.applicationtype_gid + "'," +
                     " application_type='" + values.application_type + "'," +
                     " sanctionto_gid='" + values.sanctionto_gid + "'," +
                     " sanctionto_name='" + values.sanctionto_name.Replace("'", "") + "'," +
                     " contactpersonaddress_gid='" + values.contactpersonaddress_gid + "'," +
                     " contactperson_address='" + values.contactperson_address.Replace("'", "") + "'," +
                     " contactperson_name='" + values.contactperson_name + "'," +
                     " contactpersonmobileno_gid='" + values.contactpersonmobileno_gid + "'," +
                     " contactperson_number='" + values.contactperson_number + "'," +
                     " contactpersonemail_gid='" + values.contactpersonemail_gid + "'," +
                     " paycard ='" + values.paycard + "'," +
                     " esdeclaration_status ='" + values.esdeclaration_status + "'," +
                     " branch_gid ='" + values.branch_gid + "'," +
                     " branch_name ='" + values.branch_name + "'," +
                     " contactpersonemail_address='" + values.contactpersonemail_address + "',";
            if ((values.sanctionfrom_date == null) || (values.sanctionfrom_date == ""))
            {
                msSQL += "sanctionfrom_date=null,";
            }
            else
            {
                msSQL += "sanctionfrom_date='" + Convert.ToDateTime(values.sanctionfrom_date).ToString("yyyy-MM-dd") + "',";
            }
            if ((values.sanctiontill_date == null) || (values.sanctiontill_date == ""))
            {
                msSQL += "sanctiontill_date=null,";
            }
            else
            {
                msSQL += "sanctiontill_date='" + Convert.ToDateTime(values.sanctiontill_date).ToString("yyyy-MM-dd") + "',";
            }
            msSQL += " updated_by='" + employee_gid + "'," +
                    " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    " where application2sanction_gid='" + values.application2sanction_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msGetGid = objcmnfunctions.GetMasterGID("SALG");
            msSQL = " INSERT INTO ocs_trn_tapplication2sanctionlog( " +
                          " application2sanctionlog_gid," +
                          " application2sanction_gid," +
                           " application_gid ," +
                          " sanction_refno," +
                          " sanction_date," +
                          " sanction_type," +
                          " natureof_proposal," +
                          " applicationtype_gid," +
                          " application_type," +
                          " sanctionto_gid," +
                          " sanctionto_name," +
                          " contactpersonaddress_gid," +
                          " contactperson_address," +
                          " contactperson_name," +
                          " contactpersonmobileno_gid," +
                          " contactperson_number," +
                          " contactpersonemail_gid," +
                          " contactpersonemail_address," +
                          " paycard," +
                          " esdeclaration_status," +
                          " branch_gid, " +
                          " branch_name, " +
                          " sanctionfrom_date," +
                          " sanctiontill_date," +
                          " newsanction_refno," +
                          " newsanction_date," +
                          " newsanction_type," +
                          " newnatureof_proposal," +
                          " newapplicationtype_gid," +
                          " newapplication_type," +
                          " newsanctionto_gid," +
                          " newsanctionto_name," +
                          " newcontactpersonaddress_gid," +
                          " newcontactperson_address," +
                          " newcontactperson_name," +
                          " newcontactpersonmobileno_gid," +
                          " newcontactperson_number," +
                          " newcontactpersonemail_gid," +
                          " newcontactpersonemail_address," +
                          " newpaycard," +
                          " newesdeclaration_status," +
                          " newbranch_gid, " +
                          " newbranch_name, " +
                          " newsanctionfrom_date," +
                          " newsanctiontill_date," +
                          " santioncreated_by," +
                          " santioncreated_date," +
                          " makerfile_path, " +
                          " makerfile_name, " +
                          " sanctionletter_status," +
                          " template_content, " +
                          " makersubmitted_by, " +
                          " makersubmitted_on," +
                          " sanctiongenerated_by, " +
                          " sanctiongenerated_on, " +
                           " template_name, " +
                          " checkerapproved_by, " +
                          " checkerupdated_on," +
                          " checkerpushback_remarks, " +
                          " checkerapproval_flag, " +
                          " checkerapproved_on," +
                          " digitalsignature_flag, " +
                          " created_by," +
                          " created_date)" +
                          " values(" +
                          "'" + msGetGid + "', " +
                          "'" + application2sanction_gid + "'," +
                          "'" + application_gid + "'," +
                          "'" + sanction_refno + "', " +
                          "'" + sanction_date + "', " +
                          "'" + sanction_type + "', " +
                          "'" + natureof_proposal + "', " +
                          "'" + applicationtype_gid + "', " +
                          "'" + application_type + "', " +
                          "'" + sanctionto_gid + "', " +
                          "'" + sanctionto_name.Replace("'", "") + "', " +
                          "'" + contactpersonaddress_gid + "', " +
                          "'" + contactperson_address.Replace("'", "") + "', " +
                          "'" + contactperson_name + "', " +
                          "'" + contactpersonmobileno_gid + "', " +
                          "'" + contactperson_number + "', " +
                          "'" + contactpersonemail_gid + "', " +
                          "'" + contactpersonemail_address + "', " +
                          "'" + paycard + "', " +
                          "'" + esdeclaration_status + "', " +
                          "'" + branch_gid + "', " +
                          "'" + branch_name + "', ";
            if ((sanctionfrom_date == null) || (sanctionfrom_date == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(sanctionfrom_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                // msSQL += "'" + sanctionfrom_date + "', " ;
            }
            if ((sanctiontill_date == null) || (sanctiontill_date == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(sanctiontill_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                // msSQL += "'" + sanctiontill_date + "', " ;
            }
            msSQL += "'" + values.sanction_refno + "', " +
                     "'" + Convert.ToDateTime(values.sanction_date).ToString("yyyy-MM-dd") + "'," +
                     "'" + values.sanction_type + "', " +
                     "'" + values.natureof_proposal + "', " +
                     "'" + values.applicationtype_gid + "', " +
                     "'" + values.application_type + "', " +
                     "'" + values.sanctionto_gid + "', " +
                     "'" + values.sanctionto_name + "', " +
                     "'" + values.contactpersonaddress_gid + "', " +
                     "'" + values.contactperson_address + "', " +
                     "'" + values.contactperson_name + "', " +
                     "'" + values.contactpersonmobileno_gid + "', " +
                     "'" + values.contactperson_number + "', " +
                     "'" + values.contactpersonemail_gid + "', " +
                     "'" + values.contactpersonemail_address + "', " +
                     "'" + values.paycard + "', " +
                     "'" + values.esdeclaration_status + "', " +
                     "'" + values.branch_gid + "', " +
                     "'" + values.branch_name + "', ";
            if ((values.sanctionfrom_date == null) || (values.sanctionfrom_date == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.sanctionfrom_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            if ((values.sanctiontill_date == null) || (values.sanctiontill_date == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.sanctiontill_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            msSQL += "'" + created_by + "'," +
                     "'" + Convert.ToDateTime(values.created_date).ToString("yyyy-MM-dd") + "'," +
                     "'" + makerfile_path + "'," +
                     "'" + makerfile_name + "'," +
                     "'" + sanctionletter_status + "'," +
                     "'" + template_content.Replace("'", "''") + "'," +
                     "'" + makersubmitted_by + "',";
            if ((makersubmitted_on == null) || (makersubmitted_on == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(makersubmitted_on).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }

            msSQL += "'" + employee_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                      "'" + template_name + "'," +
                     "'" + checkerapproved_by + "',";
            if ((values.checkerupdated_on == null) || (values.checkerupdated_on == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.checkerupdated_on).ToString("yyyy-MM-dd") + "',";
            }
            msSQL += "'" + checkerpushback_remarks + "'," +
                     "'" + checkerapproval_flag + "',";
            if ((values.checkerapproved_on == null) || (values.checkerapproved_on == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.checkerapproved_on).ToString("yyyy-MM-dd") + "',";
            }
            msSQL += "'" + digitalsignature_flag + "'," +
                     "'" + employee_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult1 = objdbconn.ExecuteNonQuerySQL(msSQL);


            if (values.esdeclaration_status == "Yes")
            {
                msSQL = " insert into ocs_trn_tuploadesdeclarationdocumentlog " +
                        " ( application2sanctionlog_gid, application2sanction_gid,esdeclaration_gid,  document_name, " +
                        " document_path, document_type, created_by, created_date, delete_flag, updated_by,updated_date) " +
                        " select @application2sanctionlog_gid := '" + msGetGid + "',  " +
                        "  application2sanction_gid, esdeclaration_gid, document_name, document_path, " +
                        " document_type, created_by, created_date, delete_flag, updated_by,  @updated_date := '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' from ocs_trn_tuploadesdeclarationdocument " +
                        " where application2sanction_gid='" + values.application2sanction_gid + "'";
                mnResultuploadesdeclarationdocumentlog = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else
            {
                msSQL = " insert into ocs_trn_tdeviationmaildocumentlog " +
                           " (maildocument_gid,application2sanctionlog_gid, application2sanction_gid, document_name, " +
                           " document_path, document_type, created_by, created_date, delete_flag, updated_by,updated_date) " +
                           " select maildocument_gid, @application2sanctionlog_gid := '" + msGetGid + "',application2sanction_gid,  " +
                           "  document_name, document_path, " +
                           " document_type, created_by, created_date, delete_flag, updated_by,  @updated_date := '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                           "  from ocs_trn_tdeviationmaildocument " +
                           " where application2sanction_gid='" + values.application2sanction_gid + "'";
                mnResultdeviationmaildocumentlog = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            if (values.esdeclaration_status == "Yes")
            {
                msSQL = "delete from ocs_trn_tdeviationmaildocument where application2sanction_gid='" + values.application2sanction_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else
            {
                msSQL = "delete from ocs_trn_tuploadesdeclarationdocument where application2sanction_gid='" + values.application2sanction_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            if (mnResult != 0)
            {

                values.message = "Sanction Details are Updated Successfully";
                values.status = true;
                return true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;
                return false;

            }
        }
        public string NumberToWords(int number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + NumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += NumberToWords(number / 1000000) + " million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
                var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return words;
        }
        public void DaGetCADDocChecklistMakerSummary(string employee_gid, MdlMstCAD values)
        {
            //msSQL = " select a.application_gid,a.application_no,a.customerref_name,a.customer_urn,a.creditgroup_name,e.sanction_refno, " +
            //         " a.customer_name as customer_name,date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as ccapproved_date, a.approval_status," +
            //         " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as cadaccepted_by, " +
            //         " date_format(a.processupdated_date, '%d-%m-%Y %h:%i %p') as cadaccepted_date, a.created_by, " +
            //         " a.creditgroup_gid, d.cadgroup_name,a.customer_urn,a.renewal_flag,a.enhancement_flag from ocs_trn_tcadapplication a " +
            //         " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by " +
            //         " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
            //         " left join ocs_trn_tprocesstype_assign d on d.application_gid = a.application_gid " +
            //         " left join ocs_trn_tapplication2sanction e on e.application_gid = a.application_gid " +
            //         " where a.process_type = 'Accept' and a.docchecklist_makerflag='N' and " +
            //         " a.application_gid in (Select application_gid from ocs_trn_tprocesstype_assign where menu_gid = 'CADMGTDCL' " +
            //         " and maker_gid = '" + employee_gid + "')" +
            //         " group by a.application_gid order by a.updated_date desc ";
            msSQL = " call ocs_trn_spcaddocchecklistmakersummary('" + employee_gid + "') ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<cadapplicationlist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    string lsccgroup_name;
                    string lsccadmin_name;
                    string lscadgroup_name;

                    //msSQL = "select group_concat(ccgroup_name) as ccgroup_name from ocs_mst_tapplication where application_gid='" + dt["application_gid"] + "'";
                    //objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    //if (objODBCDataReader.HasRows == true)
                    //{
                    //    lsccgroup_name = objODBCDataReader["ccgroup_name"].ToString();
                    //}
                    //else
                    //{
                    //    lsccgroup_name = "";
                    //}
                    //objODBCDataReader.Close();
                    //msSQL = "select group_concat(ccadmin_name) as ccadmin_name from ocs_mst_tccschedulemeeting where application_gid='" + dt["application_gid"] + "'";
                    //objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    //if (objODBCDataReader.HasRows == true)
                    //{
                    //    lsccadmin_name = objODBCDataReader["ccadmin_name"].ToString();
                    //}
                    //else
                    //{
                    //    lsccadmin_name = "";
                    //}
                    //objODBCDataReader.Close();
                    //msSQL = "select group_concat(cadgroup_name) as cadgroup_name from ocs_trn_tprocesstype_assign where application_gid='" + dt["application_gid"] + "'";
                    //objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    //if (objODBCDataReader.HasRows == true)
                    //{
                    //    lscadgroup_name = objODBCDataReader["cadgroup_name"].ToString();
                    //}
                    //else
                    //{
                    //    lscadgroup_name = "";
                    //}
                    //objODBCDataReader.Close();
                    getapplicationadd_list.Add(new cadapplicationlist
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        ccapproved_date = dt["ccapproved_date"].ToString(),
                        creditgroup_name = dt["creditgroup_name"].ToString(),
                        //ccgroup_name = lsccgroup_name,
                        //ccadmin_name = lsccadmin_name,
                        ccgroup_name = dt["ccgroup_name"].ToString(),
                        cadgroupname = dt["cadgroup_name"].ToString(),
                        cadaccepted_by = dt["cadaccepted_by"].ToString(),
                        cadaccepted_date = dt["cadaccepted_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        sanction_refno = dt["sanction_refno"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        renewal_flag = dt["renewal_flag"].ToString(),
                        enhancement_flag = dt["enhancement_flag"].ToString()
                    });

                }
            }
            values.cadapplicationlist = getapplicationadd_list;
            dt_datatable.Dispose();
        }

        public void DaGetCADDocChecklistFollowupMakerSummary(string employee_gid, MdlMstCAD values)
        {
            //msSQL = " select a.application_gid,a.application_no,a.customerref_name,a.customer_urn,a.creditgroup_name,e.sanction_refno, " +
            //         " a.customer_name as customer_name,date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as ccapproved_date, a.approval_status," +
            //         " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as cadaccepted_by, " +
            //         " date_format(a.processupdated_date, '%d-%m-%Y %h:%i %p') as cadaccepted_date, a.created_by, " +
            //         " a.creditgroup_gid, d.cadgroup_name,a.customer_urn,a.renewal_flag,a.enhancement_flag from ocs_trn_tcadapplication a " +
            //         " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by " +
            //         " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
            //         " left join ocs_trn_tprocesstype_assign d on d.application_gid = a.application_gid " +
            //         " left join ocs_trn_tapplication2sanction e on e.application_gid = a.application_gid " +
            //         " left join ocs_trn_tapplication2docchecklist f on f.application_gid = a.application_gid " +
            //         " where a.process_type = 'Accept' and a.docchecklist_makerflag='Y' and f.approval_flag='N' and " +
            //         " a.application_gid in (Select application_gid from ocs_trn_tprocesstype_assign where menu_gid = 'CADMGTDCL' " +
            //         " and maker_gid = '" + employee_gid + "')" +
            //         " group by a.application_gid order by a.updated_date desc ";
            msSQL = " call ocs_trn_spcaddocchecklistfollowupmakersummary('" + employee_gid + "') ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<cadapplicationlist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    string lsccgroup_name;
                    string lsccadmin_name;

                    //msSQL = "select group_concat(ccgroup_name) as ccgroup_name from ocs_trn_tcadapplication where application_gid='" + dt["application_gid"] + "'";
                    //objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    //if (objODBCDataReader.HasRows == true)
                    //{
                    //    lsccgroup_name = objODBCDataReader["ccgroup_name"].ToString();
                    //}
                    //else
                    //{
                    //    lsccgroup_name = "";
                    //}
                    //objODBCDataReader.Close();
                    //msSQL = "select group_concat(ccadmin_name) as ccadmin_name from ocs_mst_tccschedulemeeting where application_gid='" + dt["application_gid"] + "'";
                    //objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    //if (objODBCDataReader.HasRows == true)
                    //{
                    //    lsccadmin_name = objODBCDataReader["ccadmin_name"].ToString();
                    //}
                    //else
                    //{
                    //    lsccadmin_name = "";
                    //}
                    //objODBCDataReader.Close();
                    getapplicationadd_list.Add(new cadapplicationlist
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        ccapproved_date = dt["ccapproved_date"].ToString(),
                        creditgroup_name = dt["creditgroup_name"].ToString(),
                        //ccgroup_name = lsccgroup_name,
                        //ccadmin_name = lsccadmin_name,
                        ccgroup_name = dt["ccgroup_name"].ToString(),
                        cadgroupname = dt["cadgroup_name"].ToString(),
                        cadaccepted_by = dt["cadaccepted_by"].ToString(),
                        cadaccepted_date = dt["cadaccepted_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        sanction_refno = dt["sanction_refno"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        renewal_flag = dt["renewal_flag"].ToString(),
                        enhancement_flag = dt["enhancement_flag"].ToString()
                    });

                }
            }
            values.cadapplicationlist = getapplicationadd_list;
            dt_datatable.Dispose();
        }

        public void DaPostDocChecklistMakerSubmit(string employee_gid, MdlDocChecklistdetails values)
        {
            msSQL = " select maker_gid from ocs_trn_tprocesstype_assign a " +
                  " where a.processtype_name = 'Accept' and a.menu_gid = 'CADMGTDCL' and a.application_gid = '" + values.application_gid + "'";
            values.maker_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select checker_gid from ocs_trn_tprocesstype_assign a " +
                   " where a.processtype_name = 'Accept' and a.menu_gid = 'CADMGTDCL' and a.application_gid = '" + values.application_gid + "'";
            values.checker_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select approver_gid from ocs_trn_tprocesstype_assign a " +
                   " where a.processtype_name = 'Accept' and a.menu_gid = 'CADMGTDCL' and a.application_gid = '" + values.application_gid + "'";
            values.approver_gid = objdbconn.GetExecuteScalar(msSQL);

            if (values.maker_gid == values.checker_gid)
            {
                if (values.maker_gid == values.approver_gid)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("A2DC");
                    msSQL = " insert into ocs_trn_tapplication2docchecklist(" +
                            " application2docchecklist_gid ," +
                            " application_gid," +
                            " application_no," +
                            " customer_name ," +
                            " makersubmitted_by," +
                            " makersubmitted_on ," +
                            " maker_flag," +
                            " checkerapproved_by," +
                            " checkerapproved_on," +
                            " checker_flag," +
                            " approved_by," +
                            " approved_on," +
                            " approval_flag," +
                            " approval_status," +
                            " created_by," +
                            " created_date)" +
                            " values(" +
                            "'" + msGetGid + "'," +
                            "'" + values.application_gid + "'," +
                            "'" + values.application_no + "'," +
                            "'" + values.customer_name.Replace("'", "") + "'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                            "'Y'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                            "'Y'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                            "'Y'," +
                            "'Approved'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult != 0)
                    {
                        msSQL = " update ocs_mst_tapplication set  docchecklist_makerflag='Y',docchecklist_checkerflag='Y',docchecklist_approvalflag='Y' where application_gid='" + values.application_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " update ocs_trn_tprocesstype_assign set maker_approvalflag='Y', maker_approveddate= NOW()," +
                                " checker_approvalflag='Y', checker_approveddate= NOW(), " +
                                " approver_approvalflag ='Y', approver_approveddate= NOW() " +
                                " where application_gid='" + values.application_gid + "' and menu_gid='" + getMenuClass.DocumentChecklist + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        values.status = true;
                        values.message = "Document Checklist Approved Successfully";
                    }
                    else
                    {
                        values.message = "Error Occured";
                        values.status = false;
                    }
                }
                else
                {
                    msGetGid = objcmnfunctions.GetMasterGID("A2DC");
                    msSQL = " insert into ocs_trn_tapplication2docchecklist(" +
                            " application2docchecklist_gid ," +
                            " application_gid," +
                            " application_no," +
                            " customer_name ," +
                            " makersubmitted_by," +
                            " makersubmitted_on ," +
                            " maker_flag," +
                            " checkerapproved_by," +
                            " checkerapproved_on," +
                            " checker_flag," +
                            " approval_status," +
                            " created_by," +
                            " created_date)" +
                            " values(" +
                            "'" + msGetGid + "'," +
                            "'" + values.application_gid + "'," +
                            "'" + values.application_no + "'," +
                            "'" + values.customer_name + "'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                            "'Y'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                            "'Y'," +
                            "'Approval Pending'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult != 0)
                    {
                        msSQL = " update ocs_mst_tapplication set docchecklist_makerflag='Y',docchecklist_checkerflag='Y' where application_gid='" + values.application_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " update ocs_trn_tprocesstype_assign set maker_approvalflag='Y', maker_approveddate= NOW(), " +
                                " checker_approvalflag='Y', checker_approveddate= NOW() " +
                                " where application_gid='" + values.application_gid + "' and menu_gid='" + getMenuClass.DocumentChecklist + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        values.status = true;
                        values.message = "Document Checklist Submitted for Approval";
                    }
                    else
                    {
                        values.message = "Error Occured While Adding";
                        values.status = false;
                    }
                }
            }
            else
            {
                msGetGid = objcmnfunctions.GetMasterGID("A2DC");
                msSQL = " insert into ocs_trn_tapplication2docchecklist(" +
                        " application2docchecklist_gid ," +
                        " application_gid," +
                        " application_no," +
                        " customer_name ," +
                        " makersubmitted_by," +
                        " makersubmitted_on ," +
                        " maker_flag," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + values.application_gid + "'," +
                        "'" + values.application_no + "'," +
                        "'" + values.customer_name + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        "'Y'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    msSQL = " update ocs_mst_tapplication set docchecklist_makerflag='Y' where application_gid='" + values.application_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update ocs_trn_tprocesstype_assign set maker_approvalflag='Y', maker_approveddate= NOW() " +
                           " where application_gid='" + values.application_gid + "' and menu_gid='" + getMenuClass.DocumentChecklist + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    values.status = true;
                    values.message = "Document Checklist Submitted for Checker";
                }
                else
                {
                    values.message = "Error Occured While Adding";
                    values.status = false;
                }
            }
        }
        public static class getMenuClass
        {
            public const string
                 DocumentChecklist = "CADMGTDCL",
                 Sanction = "CADMGTSAN",
                 ScannedDocument = "CADMGTDTS",
                 Physical_document = "CADMGTPYD";
        }

        public void DaGetCADDocChecklistCheckerSummary(string employee_gid, MdlMstCAD values)
        {
            //msSQL = " select a.application_gid,a.application_no,a.customerref_name,a.customer_urn,a.creditgroup_name,f.sanction_refno, " +
            //         " a.customer_name as customer_name,date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as ccapproved_date, a.approval_status," +
            //         " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as cadaccepted_by, " +
            //         " date_format(a.processupdated_date, '%d-%m-%Y %h:%i %p') as cadaccepted_date, a.created_by, " +
            //         " a.creditgroup_gid, e.cadgroup_name,a.customer_urn,a.renewal_flag,a.enhancement_flag from ocs_trn_tcadapplication a " +
            //         " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by " +
            //         " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
            //         " left join ocs_trn_tapplication2docchecklist d on d.application_gid = a.application_gid " +
            //         " left join ocs_trn_tprocesstype_assign e on e.application_gid = a.application_gid " +
            //         " left join ocs_trn_tapplication2sanction f on f.application_gid = a.application_gid " +
            //         " where a.process_type = 'Accept' and d.maker_flag='Y' and d.checker_flag='N' and " +
            //         " a.application_gid in (Select application_gid from ocs_trn_tprocesstype_assign where menu_gid = 'CADMGTDCL' " +
            //         " and checker_gid = '" + employee_gid + "')" +
            //         " group by a.application_gid order by a.updated_date desc ";
            msSQL = " call ocs_trn_spcaddocchecklistcheckersummary('" + employee_gid + "') ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<cadapplicationlist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    string lsccgroup_name;
                    string lsccadmin_name;
                    string lscadgroup_name;

                    //msSQL = "select group_concat(ccgroup_name) as ccgroup_name from ocs_trn_tcadapplication where application_gid='" + dt["application_gid"] + "'";
                    //objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    //if (objODBCDataReader.HasRows == true)
                    //{
                    //    lsccgroup_name = objODBCDataReader["ccgroup_name"].ToString();
                    //}
                    //else
                    //{
                    //    lsccgroup_name = "";
                    //}
                    //objODBCDataReader.Close();
                    //msSQL = "select group_concat(ccadmin_name) as ccadmin_name from ocs_mst_tccschedulemeeting where application_gid='" + dt["application_gid"] + "'";
                    //objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    //if (objODBCDataReader.HasRows == true)
                    //{
                    //    lsccadmin_name = objODBCDataReader["ccadmin_name"].ToString();
                    //}
                    //else
                    //{
                    //    lsccadmin_name = "";
                    //}
                    //objODBCDataReader.Close();
                    //msSQL = "select group_concat(cadgroup_name) as cadgroup_name from ocs_trn_tprocesstype_assign where application_gid='" + dt["application_gid"] + "'";
                    //objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    //if (objODBCDataReader.HasRows == true)
                    //{
                    //    lscadgroup_name = objODBCDataReader["cadgroup_name"].ToString();
                    //}
                    //else
                    //{
                    //    lscadgroup_name = "";
                    //}
                    //objODBCDataReader.Close();
                    getapplicationadd_list.Add(new cadapplicationlist
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        ccapproved_date = dt["ccapproved_date"].ToString(),
                        creditgroup_name = dt["creditgroup_name"].ToString(),
                        //ccgroup_name = lsccgroup_name,
                        //ccadmin_name = lsccadmin_name,
                        ccgroup_name = dt["ccgroup_name"].ToString(),
                        cadgroupname = dt["cadgroup_name"].ToString(),
                        cadaccepted_by = dt["cadaccepted_by"].ToString(),
                        cadaccepted_date = dt["cadaccepted_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        sanction_refno = dt["sanction_refno"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        renewal_flag = dt["renewal_flag"].ToString(),
                        enhancement_flag = dt["enhancement_flag"].ToString()

                    });

                }
            }
            values.cadapplicationlist = getapplicationadd_list;
            dt_datatable.Dispose();
        }

        public void DaGetCADDocChecklistCheckerFollowupSummary(string employee_gid, MdlMstCAD values)
        {
            //msSQL = " select a.application_gid,a.application_no,a.customerref_name,a.customer_urn,a.creditgroup_name,f.sanction_refno,  " +
            //         " a.customer_name as customer_name,date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as ccapproved_date, a.approval_status," +
            //         " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as cadaccepted_by, " +
            //         " date_format(a.processupdated_date, '%d-%m-%Y %h:%i %p') as cadaccepted_date, a.created_by, " +
            //         " a.creditgroup_gid, e.cadgroup_name,a.customer_urn,a.renewal_flag,a.enhancement_flag from ocs_trn_tcadapplication a " +
            //         " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by " +
            //         " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
            //         " left join ocs_trn_tapplication2docchecklist d on d.application_gid = a.application_gid " +
            //         " left join ocs_trn_tprocesstype_assign e on e.application_gid = a.application_gid " +
            //         " left join ocs_trn_tapplication2sanction f on f.application_gid = a.application_gid " +
            //         " where a.process_type = 'Accept' and d.maker_flag='Y' and d.checker_flag='Y' and d.approval_flag='N' and" +
            //         " a.application_gid in (Select application_gid from ocs_trn_tprocesstype_assign where menu_gid = 'CADMGTDCL' " +
            //         " and checker_gid = '" + employee_gid + "')" +
            //         " group by a.application_gid order by a.updated_date desc ";
            msSQL = " call ocs_trn_spcaddocchecklistcheckerfollowupsummary('" + employee_gid + "') ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<cadapplicationlist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    string lsccgroup_name;
                    string lsccadmin_name;
                    string lscadgroup_name;

                    //msSQL = "select group_concat(ccgroup_name) as ccgroup_name from ocs_trn_tcadapplication where application_gid='" + dt["application_gid"] + "'";
                    //objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    //if (objODBCDataReader.HasRows == true)
                    //{
                    //    lsccgroup_name = objODBCDataReader["ccgroup_name"].ToString();
                    //}
                    //else
                    //{
                    //    lsccgroup_name = "";
                    //}
                    //objODBCDataReader.Close();
                    //msSQL = "select group_concat(ccadmin_name) as ccadmin_name from ocs_mst_tccschedulemeeting where application_gid='" + dt["application_gid"] + "'";
                    //objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    //if (objODBCDataReader.HasRows == true)
                    //{
                    //    lsccadmin_name = objODBCDataReader["ccadmin_name"].ToString();
                    //}
                    //else
                    //{
                    //    lsccadmin_name = "";
                    //}
                    //objODBCDataReader.Close();
                    //msSQL = "select group_concat(cadgroup_name) as cadgroup_name from ocs_trn_tprocesstype_assign where application_gid='" + dt["application_gid"] + "'";
                    //objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    //if (objODBCDataReader.HasRows == true)
                    //{
                    //    lscadgroup_name = objODBCDataReader["cadgroup_name"].ToString();
                    //}
                    //else
                    //{
                    //    lscadgroup_name = "";
                    //}
                    //objODBCDataReader.Close();
                    getapplicationadd_list.Add(new cadapplicationlist
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        ccapproved_date = dt["ccapproved_date"].ToString(),
                        creditgroup_name = dt["creditgroup_name"].ToString(),
                        //ccgroup_name = lsccgroup_name,
                        //ccadmin_name = lsccadmin_name,
                        ccgroup_name = dt["ccgroup_name"].ToString(),
                        ccadmin_name = dt["ccadmin_name"].ToString(),
                        cadgroupname = dt["cadgroup_name"].ToString(),
                        cadaccepted_by = dt["cadaccepted_by"].ToString(),
                        cadaccepted_date = dt["cadaccepted_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        sanction_refno = dt["sanction_refno"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        renewal_flag = dt["renewal_flag"].ToString(),
                        enhancement_flag = dt["enhancement_flag"].ToString()

                    });

                }
            }
            values.cadapplicationlist = getapplicationadd_list;
            dt_datatable.Dispose();
        }

        public void DaPostDocChecklistCheckerSubmit(string employee_gid, MdlDocChecklistdetails values)
        {
            msSQL = " select maker_gid from ocs_trn_tprocesstype_assign a " +
                   " where a.processtype_name = 'Accept' and a.menu_gid = 'CADMGTDCL' and a.application_gid = '" + values.application_gid + "'";
            values.maker_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select checker_gid from ocs_trn_tprocesstype_assign a " +
                   " where a.processtype_name = 'Accept' and a.menu_gid = 'CADMGTDCL' and a.application_gid = '" + values.application_gid + "'";
            values.checker_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select approver_gid from ocs_trn_tprocesstype_assign a " +
                   " where a.processtype_name = 'Accept' and a.menu_gid = 'CADMGTDCL' and a.application_gid = '" + values.application_gid + "'";
            values.approver_gid = objdbconn.GetExecuteScalar(msSQL);

            if (values.checker_gid == values.approver_gid)
            {
                msSQL = " update ocs_trn_tapplication2docchecklist set checker_flag = 'Y'," +
                        " checkerapproved_by='" + employee_gid + "', checkerapproved_on = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', " +
                        " approved_by='" + employee_gid + "', approved_on = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', " +
                        " approval_status = 'Approved', " +
                        " approval_flag = 'Y' " +
                        " where application_gid='" + values.application_gid + "'";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    msSQL = " update ocs_mst_tapplication set docchecklist_checkerflag='Y',docchecklist_approvalflag='Y' where application_gid='" + values.application_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update ocs_trn_tprocesstype_assign set checker_approvalflag='Y', checker_approveddate= NOW(), " +
                            " approver_approvalflag='Y', approver_approveddate= NOW() " +
                            " where application_gid='" + values.application_gid + "' and menu_gid='" + getMenuClass.DocumentChecklist + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    values.status = true;
                    values.message = "Document Checklist Approved Successfully";
                }
                else
                {
                    values.message = "Error Occured While Adding";
                    values.status = false;
                }
            }
            else
            {
                msSQL = " update ocs_trn_tapplication2docchecklist set checkerapproved_by='" + employee_gid + "', " +
                        " checkerapproved_on = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', " +
                        " approval_status = 'Approval Pending', " +
                        " checker_flag = 'Y' " +
                        " where application_gid='" + values.application_gid + "'";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    msSQL = " update ocs_mst_tapplication set docchecklist_checkerflag='Y' where application_gid='" + values.application_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update ocs_trn_tprocesstype_assign set checker_approvalflag='Y', checker_approveddate= NOW()" +
                            " where application_gid='" + values.application_gid + "' and menu_gid='" + getMenuClass.DocumentChecklist + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    values.status = true;
                    values.message = "Document Checklist Submitted for Approval";
                }
                else
                {
                    values.message = "Error Occured While Adding";
                    values.status = false;
                }
            }
        }
        public void DaGetCADDocChecklistApprovalSummary(string employee_gid, MdlMstCAD values)
        {
            //msSQL = " select a.application_gid,a.application_no,a.customerref_name,a.customer_urn,a.creditgroup_name,h.sanction_refno, " +
            //         " a.customer_name as customer_name,date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as ccapproved_date, a.approval_status," +
            //         " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as cadaccepted_by, " +
            //         " date_format(a.processupdated_date, '%d-%m-%Y %h:%i %p') as cadaccepted_date, a.created_by, " +
            //         " concat(f.user_firstname, ' ', f.user_lastname, ' / ', f.user_code) as checkerapproved_by, " +
            //         " date_format(d.checkerapproved_on, '%d-%m-%Y %h:%i %p') as checkerapproved_on,a.customer_urn," +
            //         " a.creditgroup_gid, docchecklist_approvalflag, d.approval_status as approval, g.cadgroup_name,a.renewal_flag,a.enhancement_flag from ocs_trn_tcadapplication a " +
            //         " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by " +
            //         " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
            //         " left join ocs_trn_tapplication2docchecklist d on d.application_gid = a.application_gid " +
            //         " left join hrm_mst_temployee e on e.employee_gid = d.checkerapproved_by " +
            //         " left join adm_mst_tuser f on f.user_gid = e.user_gid " +
            //         " left join ocs_trn_tprocesstype_assign g on g.application_gid = a.application_gid " +
            //         " left join ocs_trn_tapplication2sanction h on h.application_gid = a.application_gid " +
            //         " where a.process_type = 'Accept' and docchecklist_checkerflag='Y'and docchecklist_approvalflag='N' and " +
            //         " a.application_gid in (Select application_gid from ocs_trn_tprocesstype_assign where menu_gid = 'CADMGTDCL' and checker_flag='Y' " +
            //         " and approver_gid = '" + employee_gid + "')" +
            //         " group by a.application_gid order by a.updated_date desc ";
            msSQL = " call ocs_trn_spcaddocchecklistapprovalsummary('" + employee_gid + "') ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<cadapplicationlist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    string lsccgroup_name;
                    string lsccadmin_name;
                    string lscadgroup_name;

                    //msSQL = "select group_concat(ccgroup_name) as ccgroup_name from ocs_mst_tapplication where application_gid='" + dt["application_gid"] + "'";
                    //objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    //if (objODBCDataReader.HasRows == true)
                    //{
                    //    lsccgroup_name = objODBCDataReader["ccgroup_name"].ToString();
                    //}
                    //else
                    //{
                    //    lsccgroup_name = "";
                    //}
                    //objODBCDataReader.Close();
                    //msSQL = "select group_concat(ccadmin_name) as ccadmin_name from ocs_mst_tccschedulemeeting where application_gid='" + dt["application_gid"] + "'";
                    //objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    //if (objODBCDataReader.HasRows == true)
                    //{
                    //    lsccadmin_name = objODBCDataReader["ccadmin_name"].ToString();
                    //}
                    //else
                    //{
                    //    lsccadmin_name = "";
                    //}
                    //objODBCDataReader.Close();
                    //msSQL = "select group_concat(cadgroup_name) as cadgroup_name from ocs_trn_tprocesstype_assign where application_gid='" + dt["application_gid"] + "'";
                    //objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    //if (objODBCDataReader.HasRows == true)
                    //{
                    //    lscadgroup_name = objODBCDataReader["cadgroup_name"].ToString();
                    //}
                    //else
                    //{
                    //    lscadgroup_name = "";
                    //}
                    //objODBCDataReader.Close();
                    getapplicationadd_list.Add(new cadapplicationlist
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        ccapproved_date = dt["ccapproved_date"].ToString(),
                        creditgroup_name = dt["creditgroup_name"].ToString(),
                        //ccgroup_name = lsccgroup_name,
                        //ccadmin_name = lsccadmin_name,
                        ccgroup_name = dt["ccgroup_name"].ToString(),
                        cadgroupname = dt["cadgroup_name"].ToString(),
                        cadaccepted_by = dt["cadaccepted_by"].ToString(),
                        cadaccepted_date = dt["cadaccepted_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        docchecklist_approvalflag = dt["docchecklist_approvalflag"].ToString(),
                        approval = dt["approval"].ToString(),
                        checkerapproved_by = dt["checkerapproved_by"].ToString(),
                        checkerapproved_on = dt["checkerapproved_on"].ToString(),
                        sanction_refno = dt["sanction_refno"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        renewal_flag = dt["renewal_flag"].ToString(),
                        enhancement_flag = dt["enhancement_flag"].ToString()
                    });

                }
            }
            values.cadapplicationlist = getapplicationadd_list;
            dt_datatable.Dispose();
        }

        public void DaPostDocChecklistApproval(string employee_gid, MdlDocChecklistdetails values)
        {
            msSQL = " update ocs_trn_tapplication2docchecklist set approved_by='" + employee_gid + "', " +
                    " approved_on = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', " +
                    " approval_status = 'Approved', " +
                    " approval_flag = 'Y' " +
                    " where application_gid='" + values.application_gid + "'";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msSQL = " update ocs_mst_tapplication set docchecklist_approvalflag='Y' where application_gid='" + values.application_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update ocs_trn_tprocesstype_assign set approver_approvalflag='Y', approver_approveddate= NOW()" +
                        " where application_gid='" + values.application_gid + "' and menu_gid='" + getMenuClass.DocumentChecklist + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Document Checklist Approved Successfully";
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;
            }
        }

        public void DaGetSanctionMakerSummary(string employee_gid, MdlMstCAD values)
        {
            //msSQL = " select a.application_gid,a.application_no,a.customerref_name,a.customer_urn,a.creditgroup_name,d.sanction_refno," +
            //         " a.customer_name as customer_name,date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as ccapproved_date, a.approval_status," +
            //         " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as cadaccepted_by, " +
            //         " date_format(a.processupdated_date, '%d-%m-%Y %h:%i %p') as cadaccepted_date, a.created_by, d.sanction_status, " +
            //         " a.creditgroup_gid,e.cadgroup_name,a.customer_urn,a.renewal_flag,a.enhancement_flag from ocs_trn_tcadapplication a " +
            //         " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by " +
            //         " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
            //         " left join ocs_trn_tapplication2sanction d on d.application_gid = a.application_gid " +
            //          " left join ocs_trn_tprocesstype_assign e on e.application_gid = a.application_gid " +
            //         " where a.process_type = 'Accept' and " +
            //         " a.application_gid in (Select application_gid from ocs_trn_tprocesstype_assign where menu_gid = '" + getMenuClass.Sanction + "' " +
            //         " and maker_gid = '" + employee_gid + "' and maker_approvalflag ='N')" +
            //         " group by a.application_gid order by a.updated_date desc ";
            msSQL = "call ocs_trn_spsanctionmakersummary ('" + employee_gid + "')";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<cadapplicationlist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    string lsccgroup_name;
                    string lsccadmin_name;
                    string lscadgroup_name;

                    //msSQL = "select group_concat(ccgroup_name) as ccgroup_name from ocs_trn_tcadapplication where application_gid='" + dt["application_gid"] + "'";
                    //objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    //if (objODBCDataReader.HasRows == true)
                    //{
                    //    lsccgroup_name = objODBCDataReader["ccgroup_name"].ToString();
                    //}
                    //else
                    //{
                    //    lsccgroup_name = "";
                    //}
                    //objODBCDataReader.Close();
                    //msSQL = "select group_concat(ccadmin_name) as ccadmin_name from ocs_mst_tccschedulemeeting where application_gid='" + dt["application_gid"] + "'";
                    //objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    //if (objODBCDataReader.HasRows == true)
                    //{
                    //    lsccadmin_name = objODBCDataReader["ccadmin_name"].ToString();
                    //}
                    //else
                    //{
                    //    lsccadmin_name = "";
                    //}
                    //objODBCDataReader.Close();
                    //msSQL = "select group_concat(cadgroup_name) as cadgroup_name from ocs_trn_tprocesstype_assign where application_gid='" + dt["application_gid"] + "'";
                    //objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    //if (objODBCDataReader.HasRows == true)
                    //{
                    //    lscadgroup_name = objODBCDataReader["cadgroup_name"].ToString();
                    //}
                    //else
                    //{
                    //    lscadgroup_name = "";
                    //}
                    //objODBCDataReader.Close();
                    getapplicationadd_list.Add(new cadapplicationlist
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        ccapproved_date = dt["ccapproved_date"].ToString(),
                        creditgroup_name = dt["creditgroup_name"].ToString(),
                        //ccgroup_name = lsccgroup_name,
                        //ccadmin_name = lsccadmin_name,
                        ccgroup_name = dt["ccgroup_name"].ToString(),
                        cadgroupname = dt["cadgroup_name"].ToString(),
                        cadaccepted_by = dt["cadaccepted_by"].ToString(),
                        cadaccepted_date = dt["cadaccepted_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        sanction_status = dt["sanction_status"].ToString(),
                        sanction_refno = dt["sanction_refno"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        renewal_flag = dt["renewal_flag"].ToString(),
                        enhancement_flag = dt["enhancement_flag"].ToString()
                    });

                }
            }
            values.cadapplicationlist = getapplicationadd_list;
            dt_datatable.Dispose();
        }

        public void DaGetSanctionFollowupMakerSummary(string employee_gid, MdlMstCAD values)
        {
            //msSQL = " select a.application_gid,a.application_no,a.customerref_name,a.customer_urn,a.creditgroup_name, d.sanction_refno," +
            //         " a.customer_name as customer_name,date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as ccapproved_date, a.approval_status," +
            //         " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as cadaccepted_by, " +
            //         " date_format(a.processupdated_date, '%d-%m-%Y %h:%i %p') as cadaccepted_date, a.created_by, d.sanction_status, " +
            //         " a.creditgroup_gid,e.cadgroup_name,a.customer_urn,a.renewal_flag,a.enhancement_flag from ocs_trn_tcadapplication a " +
            //         " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by " +
            //         " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
            //         " left join ocs_trn_tapplication2sanction d on d.application_gid = a.application_gid " +
            //          " left join ocs_trn_tprocesstype_assign e on e.application_gid = a.application_gid " +
            //         " where a.process_type = 'Accept' and d.sanction_status not in ('Approved') and " +
            //         " a.application_gid in (Select application_gid from ocs_trn_tprocesstype_assign where menu_gid = '" + getMenuClass.Sanction + "' " +
            //         " and maker_gid = '" + employee_gid + "' and maker_approvalflag ='Y')" +
            //         " group by a.application_gid order by a.updated_date desc ";
            msSQL = "call ocs_trn_spsanctionfollowupmakersummary ('" + employee_gid + "')";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<cadapplicationlist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    string lsccgroup_name;
                    string lsccadmin_name;
                    string lscadgroup_name;

                    //msSQL = "select group_concat(ccgroup_name) as ccgroup_name from ocs_trn_tcadapplication where application_gid='" + dt["application_gid"] + "'";
                    //objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    //if (objODBCDataReader.HasRows == true)
                    //{
                    //    lsccgroup_name = objODBCDataReader["ccgroup_name"].ToString();
                    //}
                    //else
                    //{
                    //    lsccgroup_name = "";
                    //}
                    //objODBCDataReader.Close();
                    //msSQL = "select group_concat(ccadmin_name) as ccadmin_name from ocs_mst_tccschedulemeeting where application_gid='" + dt["application_gid"] + "'";
                    //objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    //if (objODBCDataReader.HasRows == true)
                    //{
                    //    lsccadmin_name = objODBCDataReader["ccadmin_name"].ToString();
                    //}
                    //else
                    //{
                    //    lsccadmin_name = "";
                    //}
                    //objODBCDataReader.Close();
                    //msSQL = "select group_concat(cadgroup_name) as cadgroup_name from ocs_trn_tprocesstype_assign where application_gid='" + dt["application_gid"] + "'";
                    //objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    //if (objODBCDataReader.HasRows == true)
                    //{
                    //    lscadgroup_name = objODBCDataReader["cadgroup_name"].ToString();
                    //}
                    //else
                    //{
                    //    lscadgroup_name = "";
                    //}
                    //objODBCDataReader.Close();
                    getapplicationadd_list.Add(new cadapplicationlist
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        ccapproved_date = dt["ccapproved_date"].ToString(),
                        creditgroup_name = dt["creditgroup_name"].ToString(),
                        //ccgroup_name = lsccgroup_name,
                        //ccadmin_name = lsccadmin_name,
                        ccgroup_name = dt["ccgroup_name"].ToString(),
                        cadgroupname = dt["cadgroup_name"].ToString(),
                        cadaccepted_by = dt["cadaccepted_by"].ToString(),
                        cadaccepted_date = dt["cadaccepted_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        sanction_status = dt["sanction_status"].ToString(),
                        sanction_refno = dt["sanction_refno"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        renewal_flag = dt["renewal_flag"].ToString(),
                        enhancement_flag = dt["enhancement_flag"].ToString()

                    });

                }
            }
            values.cadapplicationlist = getapplicationadd_list;
            dt_datatable.Dispose();
        }

        public void DaCADSanctionSummaryCount(string user_gid, string employee_gid, CadSanctionCount values)
        {

            msSQL = " select(select count(*) from ocs_trn_tprocesstype_assign a  " +
                   " left join ocs_trn_tcadapplication b on a.application_gid = b.application_gid " +
                   " where maker_approvalflag = 'N' and b.sanction_approvalflag = 'N' " +
                   " and maker_gid = '" + employee_gid + "' and menu_gid = 'CADMGTSAN') as MakerPendingCount, " +
                   "  (select count(*) from ocs_trn_tprocesstype_assign a " +
                   "  left join ocs_trn_tcadapplication b on a.application_gid = b.application_gid " +
                   "  where maker_approvalflag = 'Y' and b.sanction_approvalflag = 'N' " +
                   "  and maker_gid = '" + employee_gid + "' and menu_gid = 'CADMGTSAN') as MakerFollowUpCount, " +
                   "  (select count(*) from ocs_trn_tprocesstype_assign a " +
                   "  left join ocs_trn_tcadapplication b on a.application_gid = b.application_gid " +
                   " left join ocs_trn_tapplication2sanction c on a.application_gid = c.application_gid " +
                   "  where checker_approvalflag = 'N' and b.sanction_approvalflag = 'N' " +
                   "  and checker_gid = '" + employee_gid + "' and menu_gid = 'CADMGTSAN' and c.sanction_status in ('Pushback','Checker Approval Pending')) as CheckerPendingCount,   " +
                   "  (select count(*) from ocs_trn_tprocesstype_assign " +
                   "  where checker_approvalflag = 'Y' and maker_approvalflag = 'Y'  and  approver_approvalflag = 'N'" +
                   "  and checker_gid = '" + employee_gid + "' and menu_gid = 'CADMGTSAN') as CheckerFollowUpCount,  " +
                   "  (select count(*) from ocs_trn_tprocesstype_assign a" +
                    " left join ocs_trn_tapplication2sanction c on a.application_gid = c.application_gid " +
                   "  where a.approver_approvalflag = 'N' and a.checker_approvalflag = 'Y' and c.checkerletter_flag='Y' " +
                   "  and approver_gid = '" + employee_gid + "' and menu_gid = 'CADMGTSAN')  as ApproverPendingCount,  " +
                   "  (select count(*) from ocs_trn_tprocesstype_assign a" +
                   " left join ocs_trn_tsanctionacceptlog f on f.application_gid = a.application_gid " +
                   "  where approver_approvalflag = 'Y' and checker_approvalflag = 'Y'  and menu_gid = 'CADMGTSAN'  and (f.accepted_status ='N' OR f.accepted_status is null) " +
                   " and( f.updated_date = (select max(updated_date) from ocs_trn_tsanctionacceptlog y where y.application_gid = a.application_gid ) " +
                   " or  (f.updated_date is null )) " +
                   "  and(maker_gid = '" + employee_gid + "' or checker_gid = '" + employee_gid + "' or approver_gid = '" + employee_gid + "')) as CompletedCount, " +
                   "  (select count(*) from ocs_trn_tprocesstype_assign a" +
                   " left join ocs_trn_tsanctionacceptlog f on f.application_gid = a.application_gid " +
                   "  where approver_approvalflag = 'Y' and checker_approvalflag = 'Y' and (  f.accepted_status is null) and menu_gid = 'CADMGTSAN'  and (f.accepted_status ='N' OR f.accepted_status is null) " +
                   " and( f.updated_date = (select max(updated_date) from ocs_trn_tsanctionacceptlog y where y.application_gid = a.application_gid ) " +
                   " or  (f.updated_date is null )) " +
                   "  and(maker_gid = '" + employee_gid + "' or checker_gid = '" + employee_gid + "' or approver_gid = '" + employee_gid + "')) as CompletedCADCount, " +
                   "  (select count(*) from ocs_trn_tprocesstype_assign a" +
                   " left join ocs_trn_tsanctionacceptlog f on f.application_gid = a.application_gid " +
                   "  where approver_approvalflag = 'Y' and checker_approvalflag = 'Y' and (  f.accepted_status is not null) and menu_gid = 'CADMGTSAN'  and (f.accepted_status ='N' OR f.accepted_status is null) " +
                   " and( f.updated_date = (select max(updated_date) from ocs_trn_tsanctionacceptlog y where y.application_gid = a.application_gid ) " +
                   " or  (f.updated_date is null )) " +
                   "  and(maker_gid = '" + employee_gid + "' or checker_gid = '" + employee_gid + "' or approver_gid = '" + employee_gid + "')) as CompletedNotAcceptedCount, " +
                   "  (select count(*) from ocs_trn_tprocesstype_assign a" +
                   " left join ocs_trn_tsanctionacceptlog f on f.application_gid = a.application_gid " +
                   "  where approver_approvalflag = 'Y' and checker_approvalflag = 'Y'  and menu_gid = 'CADMGTSAN'  and f.accepted_status ='Y' " +
                   " and( f.updated_date = (select max(updated_date) from ocs_trn_tsanctionacceptlog y where y.application_gid = a.application_gid ) " +
                   ") " +
                   "  ) as AcceptedCount; ";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {

                values.MakerPendingCount = objODBCDataReader["MakerPendingCount"].ToString();
                values.MakerFollowUpCount = objODBCDataReader["MakerFollowUpCount"].ToString();
                values.CheckerPendingCount = objODBCDataReader["CheckerPendingCount"].ToString();
                values.CheckerFollowUpCount = objODBCDataReader["CheckerFollowUpCount"].ToString();
                values.ApproverPendingCount = objODBCDataReader["ApproverPendingCount"].ToString();
                values.CompletedCount = objODBCDataReader["CompletedCount"].ToString();
                values.AcceptedCount = objODBCDataReader["AcceptedCount"].ToString();

            }
            objODBCDataReader.Close();

        }

        public void DaCADDocChecklistSummaryCount(string user_gid, string employee_gid, CadSanctionCount values)
        {
            msSQL = " select count(application_gid) as cadsanction_count from ocs_trn_tcadapplication a " +
                    " where a.process_type = 'Accept' and a.docchecklist_makerflag='N' and " +
                     " a.application_gid in (Select application_gid from ocs_trn_tprocesstype_assign where menu_gid = 'CADMGTDCL' " +
                     " and maker_gid = '" + employee_gid + "')";
            values.cadmaker_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select count(a.application_gid) as cadsanction_count from ocs_trn_tcadapplication a " +
                    " left join ocs_trn_tapplication2docchecklist d on d.application_gid = a.application_gid " +
                    " where a.process_type = 'Accept' and a.docchecklist_makerflag='Y' and d.approval_flag='N'and " +
                    " a.application_gid in (Select application_gid from ocs_trn_tprocesstype_assign where menu_gid = 'CADMGTDCL' " +
                    " and maker_gid = '" + employee_gid + "')";
            values.makerfollowup_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select count(a.application_gid) as cadchecker_count from ocs_trn_tcadapplication a " +
                     " left join ocs_trn_tapplication2docchecklist d on d.application_gid = a.application_gid " +
                     " where a.process_type = 'Accept' and d.maker_flag='Y' and d.checker_flag='N' and " +
                     " a.application_gid in (Select application_gid from ocs_trn_tprocesstype_assign where menu_gid = 'CADMGTDCL' " +
                     " and checker_gid = '" + employee_gid + "')";
            values.cadchecker_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select count(a.application_gid) as cadchecker_count from ocs_trn_tcadapplication a " +
                     " left join ocs_trn_tapplication2docchecklist d on d.application_gid = a.application_gid " +
                     " where a.process_type = 'Accept' and d.maker_flag='Y' and d.checker_flag='Y' and d.approval_flag='N' and " +
                     " a.application_gid in (Select application_gid from ocs_trn_tprocesstype_assign where menu_gid = 'CADMGTDCL' " +
                     " and checker_gid = '" + employee_gid + "')";
            values.checkerfollowup_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select count(application_gid) as cadcheckerapproval_count from ocs_trn_tcadapplication a " +
                     " where a.process_type = 'Accept' and docchecklist_checkerflag='Y' and docchecklist_approvalflag='N' and " +
                     " a.application_gid in (Select application_gid from ocs_trn_tprocesstype_assign where menu_gid = 'CADMGTDCL' " +
                     " and approver_gid = '" + employee_gid + "')";
            values.cadcheckerapproval_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select count(application_gid) as approvalcompleted_count from ocs_trn_tcadapplication a " +
                     " where a.process_type = 'Accept' and docchecklist_approvalflag='Y' and " +
                     " a.application_gid in (Select application_gid from ocs_trn_tprocesstype_assign where menu_gid = 'CADMGTDCL' " +
                     " and approver_approvalflag='Y' )";
            values.approvalcompleted_count = objdbconn.GetExecuteScalar(msSQL);
        }

        public void DaGetAssignmentView(string application_gid, MdlMstAssignmentview values)
        {
            msSQL = " select a.processtypeassign_gid, a.application_gid, a.processtype_name, a.cadgroup_name,a.cadgroup_gid, a.menu_name, a.menu_gid," +
                    " a.maker_name, a.checker_name, approver_name, date_format(a.created_date, '%d-%m-%Y') as created_date, " +
                    " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as created_by, " +
                    " date_format(a.approver_approveddate, '%d-%m-%Y %h:%i %p') as approved_date " +
                    " from ocs_trn_tprocesstype_assign a " +
                    " left join hrm_mst_temployee b on b.employee_gid = a.created_by " +
                    " left join adm_mst_tuser c on b.user_gid = c.user_gid " +
                    " where application_gid = '" + application_gid + "' and lsareinitiate_flag='N'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getassignment_list = new List<assignment_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getassignment_list.Add(new assignment_list
                    {
                        processtypeassign_gid = (dr_datarow["processtypeassign_gid"].ToString()),
                        application_gid = (dr_datarow["application_gid"].ToString()),
                        processtype_name = (dr_datarow["processtype_name"].ToString()),
                        cadgroup_name = (dr_datarow["cadgroup_name"].ToString()),
                        menu_name = (dr_datarow["menu_name"].ToString()),
                        maker_name = (dr_datarow["maker_name"].ToString()),
                        checker_name = (dr_datarow["checker_name"].ToString()),
                        approver_name = (dr_datarow["approver_name"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        cadgroup_gid = (dr_datarow["cadgroup_gid"].ToString()),
                        menu_gid = (dr_datarow["menu_gid"].ToString()),
                        approved_date = (dr_datarow["approved_date"].ToString()),
                    });
                }
                values.assignment_list = getassignment_list;
            }
            dt_datatable.Dispose();
            values.status = true;
        }

        public void DaGetSentbackToCCUnderwritingView(string application_gid, MdlMstAssignmentview values)
        {
            msSQL = " select a.application_gid, a.processtype_remarks, a.process_type, " +
                    " date_format(a.processupdated_date, '%d-%m-%Y') as processupdated_date, " +
                    " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as processupdated_by from ocs_mst_tapplication a " +
                    " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by " +
                    " left join adm_mst_tuser c on b.user_gid = c.user_gid " +
                    " where a.application_gid = '" + application_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.application_gid = objODBCDatareader["application_gid"].ToString();
                values.processtype_remarks = objODBCDatareader["processtype_remarks"].ToString();
                values.process_type = objODBCDatareader["process_type"].ToString();
                values.processupdated_by = objODBCDatareader["processupdated_by"].ToString();
                values.processupdated_date = objODBCDatareader["processupdated_date"].ToString();
            }

            objODBCDatareader.Close();
            values.status = true;
        }

        //Menu
        public void DaGetMenu(string application_gid, MdlMstCADGetMenu objmenu)
        {
            try
            {
                msSQL = " select module_code as menu_gid,module_name as menu_name from adm_mst_tmodule where module_gid in ('CADMGTSAN','CADMGTDCL','CADMGTLSA','CADMGTDTS','CADMGTPYD','CADMGTCMS')" +
                        " and module_gid not in (select menu_gid from ocs_trn_tprocesstype_assign where application_gid = '" + application_gid + "')";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var menu_list = new List<MdlMstCADGetMenuList>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        menu_list.Add(new MdlMstCADGetMenuList
                        {
                            menu_gid = (dr_datarow["menu_gid"].ToString()),
                            menu_name = (dr_datarow["menu_name"].ToString()),
                        });
                    }
                    objmenu.menu_list = menu_list;
                }
                dt_datatable.Dispose();
                objmenu.status = true;
            }
            catch (Exception ex)
            {
                objmenu.status = false;
            }
        }

        public void DaPostRevertCADtoCC(string employee_gid, MdlCADRevert values)
        {
            msGetGid2 = objcmnfunctions.GetMasterGID("CACC");
            msSQL = " insert into ocs_trn_tcadtoccmeetinglog(" +
                    " cadtocclog_gid," +
                    " application_gid," +
                    " reason," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid2 + "'," +
                    "'" + values.application_gid + "'," +
                    "'" + values.cadtocc_reason.Replace("'", "") + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " select ccmeeting2members_gid, application_gid,approvalinitiate_by, ccschedulemeeting_gid, ccmember_name, " +
                    " ccgroup_name, ccmember_gid, created_by, created_date, updated_by, updated_date, " +
                    " attendance_status, approval_remarks, approval_status from ocs_mst_tccmeeting2members " +
                    " where application_gid ='" + values.application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msGetGid1 = objcmnfunctions.GetMasterGID("CM2L");
                    msSQL = " insert into ocs_mst_tccmeeting2memberslog(" +
                            " ccmeeting2memberslog_gid," +
                            " ccmeeting2members_gid," +
                            " application_gid," +
                            " ccschedulemeeting_gid," +
                            " ccmember_name," +
                            " ccgroup_name," +
                            " ccmember_gid," +
                            " approvalinitiate_by," +
                            " ccmember_createdby," +
                            " ccmember_createddate," +
                            " ccmember_updateddate," +
                            " ccmember_updatedby," +
                            " attendance_status," +
                            " approval_remarks," +
                            " approval_status," +
                            " created_by," +
                            " created_date)" +
                            " values(" +
                            "'" + msGetGid1 + "'," +
                            "'" + dt["ccmeeting2members_gid"].ToString() + "'," +
                            "'" + values.application_gid + "'," +
                            "'" + dt["ccschedulemeeting_gid"].ToString() + "'," +
                            "'" + dt["ccmember_name"].ToString() + "'," +
                            "'" + dt["ccgroup_name"].ToString() + "'," +
                            "'" + dt["ccmember_gid"].ToString() + "'," +
                            "'" + dt["approvalinitiate_by"].ToString() + "'," +
                            "'" + dt["created_by"].ToString() + "',";
                    if (dt["created_date"].ToString() == "" || dt["created_date"].ToString() == null)
                    {
                        msSQL += "null,";
                    }
                    else
                    {
                        msSQL += "'" + Convert.ToDateTime(dt["created_date"]).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                    }
                    if (dt["updated_date"].ToString() == "" || dt["updated_date"].ToString() == null)
                    {
                        msSQL += "null,";
                    }
                    else
                    {
                        msSQL += "'" + Convert.ToDateTime(dt["updated_date"]).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                    }

                    msSQL += "'" + dt["updated_by"].ToString() + "'," +
                            "'" + dt["attendance_status"].ToString() + "'," +
                            "'" + dt["approval_remarks"].ToString().Replace("'", "") + "'," +
                            "'" + dt["approval_status"].ToString() + "'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            dt_datatable.Dispose();

            msSQL = " insert into ocs_mst_tccschedulemeetinglog (select * from ocs_mst_tccschedulemeeting where " +
                " application_gid='" + values.application_gid + "')";
            mnResult1 = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_mst_tccmeeting2othermemberslog (select * from ocs_mst_tccmeeting2othermembers where " +
               " application_gid='" + values.application_gid + "')";
            mnResult2 = objdbconn.ExecuteNonQuerySQL(msSQL);


            if (mnResult != 0 && mnResult1 != 0 && mnResult2 != 0)
            {
                msSQL = " delete from ocs_mst_tccmeeting2members " +
                    " where application_gid='" + values.application_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from ocs_mst_tccschedulemeeting " +
                    " where application_gid='" + values.application_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from ocs_mst_tccmeeting2othermembers " +
                    " where application_gid='" + values.application_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " UPDATE ocs_mst_tapplication SET approval_status = 'Sent Back to CC', " +
                        " ccsubmit_flag = 'Y', meeting_status = 'Pending', " +
                        " ccgroup_name = null, mom_description = null, " +
                        " momapproval_flag = 'N', mom_flag = 'N', " +
                        " momupdated_by = null, momupdated_date = null, " +
                        " cccompleted_by = null,cccompleted_flag = 'N'," +
                        " momdocumentupload_flag = 'N', cc_remarks = null, cccompleted_date = null, " +
                        " cadtocc_reason = '" + values.cadtocc_reason.Replace("'", "") + "' " +
                        " WHERE application_gid = '" + values.application_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                //cccompleted_by = null,cccompleted_flag = 'N',
                values.status = true;
                values.message = "Application Reverted From CAD to CC";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured While Revert an Application From CAD to CC";
            }
        }

        public void DaPostRevertCADtoCredit(string employee_gid, MdlCADRevert values)
        {
            msGetGid3 = objcmnfunctions.GetMasterGID("CACR");
            msSQL = " insert into ocs_trn_tcadtocreditlog(" +
                    " cadtocreditlog_gid," +
                    " application_gid," +
                    " reason," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid3 + "'," +
                    "'" + values.application_gid + "'," +
                    "'" + values.cadtocredit_reason.Replace("'", "") + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            //CC to Credit
            msSQL = " select application_gid, appcreditapproval_gid, approval_gid, approval_name, approval_type, " +
                    " approval_status, approval_remarks, approved_date, rejected_date, hold_date, approval_token, " +
                    " hierary_level, created_by, created_date, initiate_flag from ocs_trn_tappcreditapproval " +
                    " where application_gid ='" + values.application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msGetGid1 = objcmnfunctions.GetMasterGID("CAPL");
                    msSQL = " insert into ocs_trn_tappcreditapprovallog(" +
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
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            dt_datatable.Dispose();


            // CAD to CC 

            msSQL = " select ccmeeting2members_gid, application_gid,approvalinitiate_by, ccschedulemeeting_gid, ccmember_name, " +
                    " ccgroup_name, ccmember_gid, created_by, created_date, updated_by, updated_date, " +
                    " attendance_status, approval_remarks, approval_status from ocs_mst_tccmeeting2members " +
                    " where application_gid ='" + values.application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msGetGid1 = objcmnfunctions.GetMasterGID("CM2L");
                    msSQL = " insert into ocs_mst_tccmeeting2memberslog(" +
                            " ccmeeting2memberslog_gid," +
                            " ccmeeting2members_gid," +
                            " application_gid," +
                            " ccschedulemeeting_gid," +
                            " ccmember_name," +
                            " ccgroup_name," +
                            " ccmember_gid," +
                            " approvalinitiate_by," +
                            " ccmember_createdby," +
                            " ccmember_createddate," +
                            " ccmember_updateddate," +
                            " ccmember_updatedby," +
                            " attendance_status," +
                            " approval_remarks," +
                            " approval_status," +
                            " created_by," +
                            " created_date)" +
                            " values(" +
                            "'" + msGetGid1 + "'," +
                            "'" + dt["ccmeeting2members_gid"].ToString() + "'," +
                            "'" + values.application_gid + "'," +
                            "'" + dt["ccschedulemeeting_gid"].ToString() + "'," +
                            "'" + dt["ccmember_name"].ToString() + "'," +
                            "'" + dt["ccgroup_name"].ToString() + "'," +
                            "'" + dt["ccmember_gid"].ToString() + "'," +
                            "'" + dt["approvalinitiate_by"].ToString() + "'," +
                            "'" + dt["created_by"].ToString() + "',";
                    if (dt["created_date"].ToString() == "" || dt["created_date"].ToString() == null)
                    {
                        msSQL += "null,";
                    }
                    else
                    {
                        msSQL += "'" + Convert.ToDateTime(dt["created_date"]).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                    }
                    if (dt["updated_date"].ToString() == "" || dt["updated_date"].ToString() == null)
                    {
                        msSQL += "null,";
                    }
                    else
                    {
                        msSQL += "'" + Convert.ToDateTime(dt["updated_date"]).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                    }

                    msSQL += "'" + dt["updated_by"].ToString() + "'," +
                            "'" + dt["attendance_status"].ToString() + "'," +
                            "'" + dt["approval_remarks"].ToString().Replace("'", "") + "'," +
                            "'" + dt["approval_status"].ToString() + "'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            dt_datatable.Dispose();

            msSQL = " insert into ocs_mst_tccschedulemeetinglog (select * from ocs_mst_tccschedulemeeting where " +
                " application_gid='" + values.application_gid + "')";
            mnResult1 = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " insert into ocs_mst_tccmeeting2othermemberslog (select * from ocs_mst_tccmeeting2othermembers where " +
               " application_gid='" + values.application_gid + "')";
            mnResult2 = objdbconn.ExecuteNonQuerySQL(msSQL);


            if (mnResult != 0 && mnResult1 != 0 && mnResult2 != 0)
            {
                msSQL = " update ocs_trn_tappcreditapproval set " +
                    " approval_status='Pending'," +
                    " approval_remarks=null," +
                    " approved_date=null" +
                    " where application_gid='" + values.application_gid + "' and hierary_level='0' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from ocs_trn_tappcreditapproval " +
                        " where application_gid='" + values.application_gid + "' and hierary_level<>'0' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from ocs_mst_tccmeeting2members " +
                    " where application_gid='" + values.application_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from ocs_mst_tccschedulemeeting " +
                    " where application_gid='" + values.application_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from ocs_mst_tccmeeting2othermembers " +
                    " where application_gid='" + values.application_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " UPDATE ocs_mst_tapplication SET approval_status = 'Sent Back to Credit', " +
                        " ccsubmit_flag = 'N', meeting_status = 'Pending', " +
                        " ccgroup_name = null, mom_description = null, " +
                        " momapproval_flag = 'N', mom_flag = 'N', " +
                        " momupdated_by = null, momupdated_date = null, " +
                        " cccompleted_by = null, cccompleted_flag = 'N'," +
                        " momdocumentupload_flag = 'N', cc_remarks = null, cccompleted_date = null, creditheadapproval_status = 'Pending', " +
                        " cadtocredit_reason = '" + values.cadtocredit_reason.Replace("'", "") + "' " +
                        " WHERE application_gid = '" + values.application_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                objvalues.Daccapprovedmail(values.application_gid);
                values.status = true;
                values.message = "Application Reverted From CAD to Credit";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured While Revert an Application From CAD to Credit";
            }
        }

        public bool DaGetCovenantDocumentTypeList(MdlMstCADCompany values, string credit_gid, string application_gid)
        {


            string program_gid = "";

            msSQL = " select program_gid from ocs_mst_tapplication where application_gid = '" + application_gid + "' ";

            program_gid = objdbconn.GetExecuteScalar(msSQL);


            if (credit_gid != "")
            {

                //msSQL = " select a.companydocument_gid,a.documenttypes_gid,a.documenttype_name,a.companydocument_name,a.covenant_type " +
                //   " from ocs_mst_tcompanydocument a " +
                //   " left join ocs_mst_tcompanydocumentprogram b on a.companydocument_gid = b.companydocument_gid " +
                //   " where a.status='Y' and b.program_gid = '" + program_gid + "' and a.covenant_type = 'Y' " +
                //     " and a.companydocument_gid not in (select companydocument_gid " +
                //     " from ocs_trn_tcovanantdocumentcheckdtls where credit_gid = '" + credit_gid + "' and untagged_type is null  group by companydocument_gid)";


                msSQL = " SELECT companydocument_gid,companydocument_name,documenttypes_gid,documenttype_name,covenant_type " +
                        " FROM ocs_mst_tcompanydocument where status='Y' and covenant_type='Y' and companydocument_gid not in (select companydocument_gid " +
                        " from ocs_trn_tcovanantdocumentcheckdtls where credit_gid = '" + credit_gid + "' and untagged_type is null group by companydocument_gid)";
            }
            else
            {
                msSQL = " SELECT companydocument_gid,companydocument_name,documenttypes_gid,documenttype_name,covenant_type " +
                        " FROM ocs_mst_tcompanydocument where status='Y' and covenant_type='Y'";

                //msSQL = " select a.companydocument_gid,a.documenttypes_gid,a.documenttype_name,a.companydocument_name,a.covenant_type " +
                //    " from ocs_mst_tcompanydocument a " +
                //    " left join ocs_mst_tcompanydocumentprogram b on a.companydocument_gid = b.companydocument_gid " +
                //    " where a.status='Y' and b.program_gid = '" + program_gid + "' and a.covenant_type = 'Y' ";

            }

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getDocList = new List<CADDocument>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getDocList.Add(new CADDocument
                    {
                        document_gid = dt["companydocument_gid"].ToString(),
                        documenttype_gid = dt["documenttypes_gid"].ToString(),
                        document_name = dt["companydocument_name"].ToString(),
                        documenttype_name = dt["documenttype_name"].ToString(),
                        covenant_type = dt["covenant_type"].ToString(),
                    });
                }
                values.CADDocument = getDocList;
                values.status = true;
            }
            else
            {
                values.status = false;
                values.message = "No Record Found";
            }
            dt_datatable.Dispose();

            return true;
        }

        public void DaPostCovenantPeriods(MdlCovenantPeriodlist values, result objResult, string user_gid, string employee_gid)
        {
            List<CovenantPeriod> TaggedDocumentChecklist = values.CovenantPeriod.Where(a => a.covenantchecked == true).ToList();
            List<CovenantPeriod> UnTaggedDocumentChecklist = values.CovenantPeriod.Where(a => a.covenantchecked == false).ToList();

            List<CovenantPeriod> caddocchecklist = new List<CovenantPeriod>();
            List<CovenantPeriod> newlyaddedchecklist = new List<CovenantPeriod>();
            List<CovenantPeriod> untaggedcaddocchecklist = new List<CovenantPeriod>();

            caddocchecklist = TaggedDocumentChecklist.Where(a => a.groupcovdocumentchecklist_gid != "" && a.groupcovdocumentchecklist_gid != null).ToList();
            newlyaddedchecklist = TaggedDocumentChecklist.Where(a => a.groupcovdocumentchecklist_gid == "" || a.groupcovdocumentchecklist_gid == null).ToList();
            untaggedcaddocchecklist = UnTaggedDocumentChecklist.Where(a => a.groupcovdocumentchecklist_gid != "" && a.groupcovdocumentchecklist_gid != null).ToList();
            bool Isgroupnew = false;

            //if (caddocchecklist.Count > 0)
            //{
                foreach (CovenantPeriod i in caddocchecklist)
                {
                    if ((i.buffer_days == "--Select Buffer Days--") || (i.buffer_days == null) || (i.buffer_days == "" ) || (i.buffer_days == "undefined") || (i.covenantperiod == "--Select Covenant Periods--")  || (i.covenantperiod == null) || (i.covenantperiod == "") || (i.covenantperiod == "undefined"))
                    {
                        objResult.status = false;
                        objResult.message = "Kindly Select the Covenant Periods & Buffer Days for the selected Document..!";
                        return;
                    }
                    //if ((i.covenantperiod == null) || (i.covenantperiod == "") || (i.covenantperiod == "undefined"))
                    //{
                    //    objResult.status = false;
                    //    objResult.message = "Kindly Select the Covenant Periods for the selected Document..!";
                    //    return;
                    //}
                }
            //}
            if (caddocchecklist.Count > 0)
            {
                foreach (CovenantPeriod i in caddocchecklist)
                {
                    msSQL = " update ocs_trn_tgroupcovenantdocumentchecklist set covenant_periods='" + i.covenantperiod + "', " +
                           " buffer_days = '" + i.buffer_days + "', " +
                           " covenantperiod_updatedby='" + user_gid + "', " +
                           " bufferday_updatedby='" + user_gid + "', " +
                           " untagged_type=null," +
                           " covenantperiod_updateddate=current_timestamp," +
                           " bufferday_updateddate=current_timestamp" +
                           " where groupcovdocumentchecklist_gid='" + i.groupcovdocumentchecklist_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update ocs_trn_tcovanantdocumentcheckdtls set covenant_periods='" + i.covenantperiod + "', " +
                            " buffer_days = '" + i.buffer_days + "', " +
                            " covenantperiod_updatedby='" + user_gid + "', " +
                            " bufferday_updatedby='" + user_gid + "', " +
                            " untagged_type=null," +
                            " covenantperiod_updateddate=current_timestamp," +
                            " bufferday_updateddate=current_timestamp" +
                            " where groupcovdocumentchecklist_gid='" + i.groupcovdocumentchecklist_gid + "'";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            if (untaggedcaddocchecklist.Count > 0)
            {
                string[] getid = untaggedcaddocchecklist.Where(p => p.groupcovdocumentchecklist_gid != "").Select(p => p.groupcovdocumentchecklist_gid.ToString()).ToArray();
                var getdocumentid = DaGetvalueswithComma(getid);
                //msSQL = "DELETE FROM ocs_trn_tcovanantdocumentcheckdtls WHERE covenantdocumentcheckdtl_gid in (" + getdocumentid + ")";
                //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                msSQL = " UPDATE ocs_trn_tgroupcovenantdocumentchecklist SET covenant_periods = '', " +
                        " buffer_days = '', " +
                        " untagged_type = 'Y', " +
                        " covenantperiod_updatedby = '" + user_gid + "', " +
                       
                        " bufferday_updatedby='" + user_gid + "', " +
                        " covenantperiod_updateddate = current_timestamp() , " +
                        " bufferday_updateddate=current_timestamp()" +
                        " WHERE groupcovdocumentchecklist_gid in (" + getdocumentid + ")";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " UPDATE ocs_trn_tcovanantdocumentcheckdtls SET covenant_periods = '', " +
                        " buffer_days = '', " +
                      " untagged_type = 'Y', " +
                      " untagged_by='" + user_gid + "', " +
                      " untagged_date=current_timestamp," +
                       " bufferday_updatedby='" + user_gid + "', " +
                      " covenantperiod_updatedby = '" + user_gid + "', " +
                      " covenantperiod_updateddate = current_timestamp(), " +
                      " bufferday_updateddate=current_timestamp()" +
                      " WHERE groupcovdocumentchecklist_gid in (" + getdocumentid + ")";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            string lscovenant_type = "";
            if (newlyaddedchecklist.Count > 0)
            {
                foreach (CovenantPeriod i in newlyaddedchecklist)
                {
                    msSQL = "select concat(user_firstname,' ', user_lastname, ' / ', user_code) as username from adm_mst_tuser where user_gid='" + user_gid + "'";
                    string tagged_name = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = " select groupcovdocumentchecklist_gid from ocs_trn_tgroupcovenantdocumentchecklist " +
                            " where mstdocument_gid='" + i.companydocument_gid + "' and credit_gid='" + i.credit_gid + "'";
                    string lsgroupcovdocumentchecklist_gid = objdbconn.GetExecuteScalar(msSQL);
                    if (lsgroupcovdocumentchecklist_gid != "")
                    {
                        msSQL = " update ocs_trn_tgroupcovenantdocumentchecklist set covenant_periods='" + i.covenantperiod + "', " +
                            " buffer_days = '" + i.buffer_days + "', " +
                          " covenantperiod_updatedby='" + user_gid + "', " +
                          " bufferday_updatedby='" + user_gid + "', " +
                          " untagged_type=null," +
                          " tagged_by='" + values.taggedby + "'," +
                          " covenantperiod_updateddate=current_timestamp," +
                          " bufferday_updateddate=current_timestamp" +
                          " where groupcovdocumentchecklist_gid='" + lsgroupcovdocumentchecklist_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " update ocs_trn_tcovanantdocumentcheckdtls set covenant_periods='" + i.covenantperiod + "', " +
                                " buffer_days = '" + i.buffer_days + "', " +
                                " covenantperiod_updatedby='" + user_gid + "', " +
                                " bufferday_updatedby='" + user_gid + "', " +
                                " untagged_type=null," +
                                " tagged_by='" + values.taggedby + "'," +
                                " tagged_name='" + tagged_name + "'," +
                                " covenantperiod_updateddate=current_timestamp," +
                                 " bufferday_updateddate=current_timestamp" +
                                " where groupcovdocumentchecklist_gid='" + lsgroupcovdocumentchecklist_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    else
                    {
                        if (values.CovenantPeriod[0].lstype == "Individual")
                        {
                            msGetGID = objcmnfunctions.GetMasterGID("CDCL");
                            msSQL = " select individualdocument_gid,documenttypes_gid,documenttype_name,individualdocument_name,covenant_type " +
                                        " from ocs_mst_tindividualdocument where individualdocument_gid='" + i.companydocument_gid + "'";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                lsdocumenttype_gid = objODBCDatareader["documenttypes_gid"].ToString();
                                lsdocumenttype_name = objODBCDatareader["documenttype_name"].ToString();
                                lscompanydocument_name = objODBCDatareader["individualdocument_name"].ToString();
                                lscovenant_type = objODBCDatareader["covenant_type"].ToString();
                            }
                            objODBCDatareader.Close();
                        }
                        else if (values.CovenantPeriod[0].lstype == "Group")
                        {
                            msGetGID = objcmnfunctions.GetMasterGID("CDCL");
                            msSQL = " select groupdocument_gid,documenttypes_gid,documenttype_name,groupdocument_name,covenant_type " +
                                        " from ocs_mst_tgroupdocument  where groupdocument_gid='" + i.companydocument_gid + "'";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                lsdocumenttype_gid = objODBCDatareader["documenttypes_gid"].ToString();
                                lsdocumenttype_name = objODBCDatareader["documenttype_name"].ToString();
                                lscompanydocument_name = objODBCDatareader["groupdocument_name"].ToString();
                                lscovenant_type = objODBCDatareader["covenant_type"].ToString();
                            }
                            objODBCDatareader.Close();
                        }
                        else
                        {
                            msGetGID = objcmnfunctions.GetMasterGID("CDCL");
                            msSQL = " select companydocument_gid,documenttypes_gid,documenttype_name,companydocument_name,covenant_type " +
                                        " from ocs_mst_tcompanydocument where companydocument_gid='" + i.companydocument_gid + "'";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                lsdocumenttype_gid = objODBCDatareader["documenttypes_gid"].ToString();
                                lsdocumenttype_name = objODBCDatareader["documenttype_name"].ToString();
                                lscompanydocument_name = objODBCDatareader["companydocument_name"].ToString();
                                lscovenant_type = objODBCDatareader["covenant_type"].ToString();
                            }
                            objODBCDatareader.Close();
                        }



                        msSQL = " insert into ocs_trn_tcovanantdocumentcheckdtls(" +
                                " covenantdocumentcheckdtl_gid," +
                                " application_gid," +
                                " credit_gid, ";
                        if (values.CovenantPeriod[0].lstype == "Institution")
                            msSQL += " companydocument_gid, ";
                        else if (values.CovenantPeriod[0].lstype == "Individual")
                            msSQL += " individualdocument_gid, ";
                        else
                        {
                            msSQL += " groupdocument_gid,";
                        }
                        msSQL += " documenttype_gid," +
                            " documenttype_code," +
                            " documenttype_name," +
                            " covenant_type, " +
                            " tagged_by, " +
                            " tagged_name, " +
                            " covenant_periods, " +
                            " covenantperiod_updatedby, " +
                            " covenantperiod_updateddate, " +
                            " buffer_days, " +
                            " bufferday_updatedby, " +
                            " bufferday_updateddate, " +
                            " created_date," +
                            " created_by)" +
                            " VALUES(" +
                            "'" + msGetGID + "'," +
                            "'" + i.application_gid + "'," +
                            "'" + i.credit_gid + "'," +
                            "'" + i.companydocument_gid + "'," +
                            "'" + lsdocumenttype_gid + "'," +
                            "'" + lsdocumenttype_name + "'," +
                            "'" + lscompanydocument_name.Replace("'", "") + "'," +
                            "'" + lscovenant_type + "'," +
                            "'" + values.taggedby + "'," +
                            "'" + tagged_name + "'," +
                            "'" + i.covenantperiod + "'," +
                            "'" + user_gid + "'," +
                            "current_timestamp," +
                             "'" + i.buffer_days + "'," +
                            "'" + user_gid + "'," +
                            "current_timestamp," +
                            "current_timestamp," +
                            "'" + user_gid + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        Isgroupnew = true;

                        string lsStackholdertype;
                        if (values.CovenantPeriod[0].lstype == "Institution")
                        {
                            msSQL = " select stakeholder_type from ocs_mst_tinstitution where institution_gid = '" + i.credit_gid + "'";

                            lsStackholdertype = objdbconn.GetExecuteScalar(msSQL);

                            if (lsStackholdertype == "Guarantor")
                            {
                                msSQL = "update ocs_trn_tcovanantdocumentcheckdtls set com_gur_flag = 'Y' " +
                                   " where application_gid = '" + i.application_gid + "' and " +
                                   " credit_gid = '" + i.credit_gid + "' and covenantdocumentcheckdtl_gid = '" + msGetGID + "'";

                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }

                            if (lsStackholdertype == "Member")
                            {
                                msSQL = "update ocs_trn_tcovanantdocumentcheckdtls set com_mem_flag = 'Y' " +
                                   " where application_gid = '" + i.application_gid + "' and " +
                                   " credit_gid = '" + i.credit_gid + "' and covenantdocumentcheckdtl_gid = '" + msGetGID + "'";

                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }
                        }
                        else if (values.CovenantPeriod[0].lstype == "Individual")
                        {
                            msSQL = " select stakeholder_type from ocs_mst_tcontact where contact_gid = '" + i.credit_gid + "'";

                            lsStackholdertype = objdbconn.GetExecuteScalar(msSQL);

                            if (lsStackholdertype == "Guarantor")
                            {
                                msSQL = "update ocs_trn_tcovanantdocumentcheckdtls set ind_gur_flag = 'Y' " +
                                   " where application_gid = '" + i.application_gid + "' and " +
                                   " credit_gid = '" + i.credit_gid + "' and covenantdocumentcheckdtl_gid = '" + msGetGID + "'";

                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }

                            if (lsStackholdertype == "Member")
                            {
                                msSQL = "update ocs_trn_tcovanantdocumentcheckdtls set ind_mem_flag = 'Y' " +
                                   " where application_gid = '" + i.application_gid + "' and " +
                                   " credit_gid = '" + i.credit_gid + "' and covenantdocumentcheckdtl_gid = '" + msGetGID + "'";

                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }

                        }
                    }
                }
                if (Isgroupnew)
                {
                    DaMstScannedDocument objvalues = new DaMstScannedDocument();
                    objvalues.DaGroupDocChecklistinfoCredit(newlyaddedchecklist[0].application_gid, newlyaddedchecklist[0].credit_gid, employee_gid);
                }
            }

            if (mnResult == 1)
            {
                objResult.status = true;
                objResult.message = "Covenant Periods are Updated Successfully..!";
            }
            else
            {
                objResult.status = false;
                objResult.message = "Error Occured..!";
            }
        }

        public string DaGetvalueswithComma(string[] array)
        {
            var value = "";
            foreach (var h in array)
            {
                value += "'" + h + "',";
            }
            value = value.TrimEnd(',');
            return value;
        }

        public void DaGetAppRevertReasonRemarks(MdlappCreditassign values, string application_gid)
        {
            try
            {
                msSQL = " select processtype_remarks " +
                        " from ocs_mst_tapplication  where application_gid = '" + application_gid + "'";
                values.processtype_remarks = objdbconn.GetExecuteScalar(msSQL);

                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaUpdatePSLCompleted(string employee_gid, pslcsacomplete values)
        {
            msSQL = " UPDATE ocs_trn_tcadapplication SET pslcompleted_flag = 'Y', " +
                    " pslupdated_by = '" + employee_gid + "', " +
                    " pslcompleteremarks ='" + values.pslcompleteremarks + "'," +
                    " pslupdated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    " WHERE application_gid = '" + values.application_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Completed Status Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
            }
        }

        public void DaGetPSLCSACompleteSummary(string employee_gid, MdlMstCAD values)
        {
            msSQL = " select a.application_gid,a.application_no,a.customerref_name,a.customer_urn,a.creditgroup_name, " +
                     " a.customer_name as customer_name, date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as ccapproved_date,a.pslcompleteremarks, a.approval_status," +
                     " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as cadaccepted_by, " +
                     " concat(f.user_firstname, ' ', f.user_lastname, ' / ', f.user_code) as pslupdated_by, " +
                     " date_format(a.processupdated_date, '%d-%m-%Y %h:%i %p') as cadaccepted_date, a.created_by,date_format(a.pslupdated_date, '%d-%m-%Y %h:%i %p') as pslupdated_date, " +
                     " a.creditgroup_gid, d.cadgroup_name from ocs_trn_tcadapplication a " +
                     " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by " +
                     " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                       " left join hrm_mst_temployee e on e.employee_gid = a.pslupdated_by " +
                     " left join adm_mst_tuser f on f.user_gid = e.user_gid " +
                     " left join ocs_trn_tprocesstype_assign d on d.application_gid = a.application_gid " +
                     " where a.process_type = 'Accept' and a.pslcompleted_flag = 'Y' " +
                     " group by a.application_gid order by a.pslupdated_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<cadapplicationlist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    string lsccgroup_name;
                    string lsccadmin_name;
                    string lscadgroup_name;

                    msSQL = "select group_concat(ccgroup_name) as ccgroup_name from ocs_trn_tcadapplication where application_gid='" + dt["application_gid"] + "'";
                    objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDataReader.HasRows == true)
                    {
                        lsccgroup_name = objODBCDataReader["ccgroup_name"].ToString();
                    }
                    else
                    {
                        lsccgroup_name = "";
                    }
                    objODBCDataReader.Close();
                    msSQL = "select group_concat(ccadmin_name) as ccadmin_name from ocs_mst_tccschedulemeeting where application_gid='" + dt["application_gid"] + "'";
                    objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDataReader.HasRows == true)
                    {
                        lsccadmin_name = objODBCDataReader["ccadmin_name"].ToString();
                    }
                    else
                    {
                        lsccadmin_name = "";
                    }
                    objODBCDataReader.Close();
                    //msSQL = "select group_concat(cadgroup_name) as cadgroup_name from ocs_trn_tprocesstype_assign where application_gid='" + dt["application_gid"] + "'";
                    //objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    //if (objODBCDataReader.HasRows == true)
                    //{
                    //    lscadgroup_name = objODBCDataReader["cadgroup_name"].ToString();
                    //}
                    //else
                    //{
                    //    lscadgroup_name = "";
                    //}
                    //objODBCDataReader.Close();
                    getapplicationadd_list.Add(new cadapplicationlist
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        ccapproved_date = dt["ccapproved_date"].ToString(),
                        creditgroup_name = dt["creditgroup_name"].ToString(),
                        ccgroup_name = lsccgroup_name,
                        ccadmin_name = lsccadmin_name,
                        cadgroupname = dt["cadgroup_name"].ToString(),
                        cadaccepted_by = dt["cadaccepted_by"].ToString(),
                        cadaccepted_date = dt["cadaccepted_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        pslupdated_date = dt["pslupdated_date"].ToString(),
                        pslupdated_by = dt["pslupdated_by"].ToString(),
                        pslcompleteremarks = dt["pslcompleteremarks"].ToString(),
                    });

                }
           
               values.cadapplicationlist = getapplicationadd_list;
                values.status = true;
            }
            else
            {
                values.status = false;
            }
            dt_datatable.Dispose();
        }

        public void DaGetPSLCSAManagementSummary(string employee_gid, MdlMstCAD values)
        {
            msSQL = " select a.application_gid,a.application_no,a.customerref_name,a.customer_urn,a.creditgroup_name, " +
                     " a.customer_name as customer_name, date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as ccapproved_date, a.approval_status," +
                     " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as cadaccepted_by, " +
                     " concat(h.user_firstname, ' ', h.user_lastname, ' / ', h.user_code) as rm_name," +
                     " concat(f.user_firstname, ' ', f.user_lastname, ' / ', f.user_code) as ccsubmitted_by, " +
                     " date_format(a.processupdated_date, '%d-%m-%Y %h:%i %p') as cadaccepted_date, a.created_by,date_format(a.pslupdated_date, '%d-%m-%Y %h:%i %p') as pslupdated_date, " +
                     " a.creditgroup_gid, d.cadgroup_name from ocs_trn_tcadapplication a " +
                     " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by " +
                     " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                     " left join ocs_trn_tprocesstype_assign d on d.application_gid = a.application_gid " +
                     " left join hrm_mst_temployee g on g.employee_gid = a.created_by " +
                     " left join adm_mst_tuser h on h.user_gid = g.user_gid " +
                     " left join hrm_mst_temployee e on e.employee_gid = a.ccsubmitted_by " +
                     " left join adm_mst_tuser f on f.user_gid = e.user_gid " +
                     " where a.process_type = 'Accept' and a.pslcompleted_flag = 'N' " +
                     " group by a.application_gid order by a.processupdated_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<cadapplicationlist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    string lsccgroup_name;
                    string lsccadmin_name;
                    string lscadgroup_name;

                    //msSQL = "select group_concat(ccgroup_name) as ccgroup_name from ocs_trn_tcadapplication where application_gid='" + dt["application_gid"] + "'";
                    //objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    //if (objODBCDataReader.HasRows == true)
                    //{
                    //    lsccgroup_name = objODBCDataReader["ccgroup_name"].ToString();
                    //}
                    //else
                    //{
                    //    lsccgroup_name = "";
                    //}
                    //objODBCDataReader.Close();
                    //msSQL = "select group_concat(ccadmin_name) as ccadmin_name from ocs_mst_tccschedulemeeting where application_gid='" + dt["application_gid"] + "'";
                    //objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    //if (objODBCDataReader.HasRows == true)
                    //{
                    //    lsccadmin_name = objODBCDataReader["ccadmin_name"].ToString();
                    //}
                    //else
                    //{
                    //    lsccadmin_name = "";
                    //}
                    //objODBCDataReader.Close();
                    //msSQL = "select group_concat(cadgroup_name) as cadgroup_name from ocs_trn_tprocesstype_assign where application_gid='" + dt["application_gid"] + "'";
                    //objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    //if (objODBCDataReader.HasRows == true)
                    //{
                    //    lscadgroup_name = objODBCDataReader["cadgroup_name"].ToString();
                    //}
                    //else
                    //{
                    //    lscadgroup_name = "";
                    //}
                    //objODBCDataReader.Close();
                    getapplicationadd_list.Add(new cadapplicationlist
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        ccapproved_date = dt["ccapproved_date"].ToString(),
                        creditgroup_name = dt["creditgroup_name"].ToString(),
                        //ccgroup_name = lsccgroup_name,
                        //ccadmin_name = lsccadmin_name,
                        cadgroupname = dt["cadgroup_name"].ToString(),
                        cadaccepted_by = dt["cadaccepted_by"].ToString(),
                        cadaccepted_date = dt["cadaccepted_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        pslupdated_date = dt["pslupdated_date"].ToString(),
                        rm_name = dt["rm_name"].ToString(),
                        ccsubmitted_by = dt["ccsubmitted_by"].ToString(),
                    });

                }
           
            values.cadapplicationlist = getapplicationadd_list;
                values.status = true;
            }
            else
            {
                values.status = false;
            }
            dt_datatable.Dispose();
        }

        public void DaGetPSLCompleteFlag(string employee_gid, string application_gid, MdlMstCAD values)
        {
            msSQL = " select application_gid from ocs_trn_tcadapplication" +
                " where application_gid='" + application_gid + "'" +
                    " and pslcompleted_flag ='N' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objODBCDatareader.Close();
                values.pslcsacomplete_flag = "Y";
            }
            else
            {
                objODBCDatareader.Close();
                values.pslcsacomplete_flag = "N";

            }

        }

        public bool DaCADSanctionLetterSave(cadtemplate_list values, string employee_gid)
        {
            msSQL = " update ocs_trn_tapplication2sanction set sanctionletter_status='Saved'," +
                    " template_content='" + values.template_content.Replace("'", "''") + "'," +
                    " defaulttemplate_content='" + values.defaulttemplate_content.Replace("'", "''") + "'," +
                    " makersubmitted_by='" + employee_gid + "'," +
                    " makersubmitted_on='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where application2sanction_gid='" + values.sanction_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " update ocs_trn_tdynamictemplatedtl set template_content='" + values.defaulttemplate_content.Replace("'", "''") + "' " +
                    " where templatetype_gid='" + values.sanction_gid + "' and templatetype_name='" + getTemplateClass.Sanction + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult == 1)
            {
                values.status = true;
                values.message = "Sanction Letter Saved Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
                return true;
            }
        }

        public void DaGetCADGroupDtl(string employee_gid, string application_gid, string menu_gid, MdlMstCAD values)
        {
            msSQL = " select application_gid,cadgroup_gid,cadgroup_name,menu_gid,menu_name, maker_gid,maker_name, " +
                    " checker_gid,checker_name,approver_gid,approver_name from ocs_trn_tprocesstype_assign" +
                    " where application_gid='" + application_gid + "' and menu_gid='" + menu_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.application_gid = objODBCDatareader["application_gid"].ToString();
                values.cadgroup_gid = objODBCDatareader["cadgroup_gid"].ToString();
                values.cadgroup_name = objODBCDatareader["cadgroup_name"].ToString();
                values.menu_gid = objODBCDatareader["menu_gid"].ToString();
                values.menu_name = objODBCDatareader["menu_name"].ToString();
                values.maker_gid = objODBCDatareader["maker_gid"].ToString();
                values.maker_name = objODBCDatareader["maker_name"].ToString();
                values.checker_gid = objODBCDatareader["checker_gid"].ToString();
                values.checker_name = objODBCDatareader["checker_name"].ToString();
                values.approver_gid = objODBCDatareader["approver_gid"].ToString();
                values.approver_name = objODBCDatareader["approver_name"].ToString();
            }

            objODBCDatareader.Close();
            values.status = true;

        }

        public void DaPostReassignCADApplication(string employee_gid, MdlReassignCadApplication values)
        {
            msSQL = " select application_gid,cadgroup_gid,cadgroup_name,menu_gid,menu_name, maker_gid,maker_name, " +
                  " checker_gid,checker_name,approver_gid,approver_name from ocs_trn_tprocesstype_assign" +
                  " where application_gid='" + values.application_gid + "' and menu_gid='" + values.menu_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                msGetGid = objcmnfunctions.GetMasterGID("RACA");
                msSQL = " insert into ocs_trn_tprocesstype_assignlog(" +
                        " processtypeassignlog_gid," +
                        " application_gid, " +
                        " cadgroup_gid," +
                        " cadgroup_name," +
                        " menu_gid," +
                        " menu_name, " +
                        " maker_gid," +
                        " maker_name," +
                        " checker_gid," +
                        " checker_name, " +
                        " approver_gid," +
                        " approver_name," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + objODBCDatareader["application_gid"].ToString() + "'," +
                        "'" + objODBCDatareader["cadgroup_gid"].ToString() + "'," +
                        "'" + objODBCDatareader["cadgroup_name"].ToString() + "'," +
                        "'" + objODBCDatareader["menu_gid"].ToString() + "'," +
                        "'" + objODBCDatareader["menu_name"].ToString() + "'," +
                        "'" + objODBCDatareader["maker_gid"].ToString() + "'," +
                        "'" + objODBCDatareader["maker_name"].ToString() + "'," +
                        "'" + objODBCDatareader["checker_gid"].ToString() + "'," +
                        "'" + objODBCDatareader["checker_name"].ToString() + "'," +
                        "'" + objODBCDatareader["approver_gid"].ToString() + "'," +
                        "'" + objODBCDatareader["approver_name"].ToString() + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            if (mnResult != 0)
            {

                msSQL = " update ocs_trn_tprocesstype_assign set " +
                         " application_gid='" + values.application_gid + "'," +
                         " cadgroup_gid='" + values.cadgroup_gid + "'," +
                         " cadgroup_name='" + values.cadgroup_name + "'," +
                         " menu_gid='" + values.menu_gid + "'," +
                         " menu_name='" + values.menu_name + "'," +
                         " maker_gid='" + values.maker_gid + "'," +
                         " maker_name='" + values.maker_name + "'," +
                         " checker_gid='" + values.checker_gid + "'," +
                         " checker_name='" + values.checker_name + "'," +
                         " approver_gid='" + values.approver_gid + "'," +
                         " approver_name='" + values.approver_name + "'," +
                         " created_by='" + employee_gid + "'," +
                         " created_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where application_gid='" + values.application_gid + "' and menu_gid='" + values.menu_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    values.message = "Application Reassigned Successfully";
                    values.status = true;
                }
                else
                {
                    values.message = "Error Occured while Reassigning Application";
                    values.status = false;

                }
            }
            else
            {
                values.message = "Error Occurred while Adding into Log Table";
                values.status = false;
            }
        }

        public void DaGetReassignApplicationView(string application_gid, string menu_gid, MdlMstAssignmentview values)
        {
            msSQL = " select a.processtypeassignlog_gid, a.application_gid, " +
                    " a.maker_name, a.checker_name, approver_name, date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date, " +
                    " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as created_by from ocs_trn_tprocesstype_assignlog a " +
                    " left join hrm_mst_temployee b on b.employee_gid = a.created_by " +
                    " left join adm_mst_tuser c on b.user_gid = c.user_gid " +
                    " where application_gid = '" + application_gid + "' and menu_gid='" + menu_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getReassignedlog_list = new List<Reassignedlog_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getReassignedlog_list.Add(new Reassignedlog_list
                    {
                        processtypeassignlog_gid = (dr_datarow["processtypeassignlog_gid"].ToString()),
                        application_gid = (dr_datarow["application_gid"].ToString()),
                        maker_name = (dr_datarow["maker_name"].ToString()),
                        checker_name = (dr_datarow["checker_name"].ToString()),
                        approver_name = (dr_datarow["approver_name"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString())
                    });
                }
                values.Reassignedlog_list = getReassignedlog_list;
            }
            dt_datatable.Dispose();
            values.status = true;
        }

        public void DaDeleteCADAssignment(string processtypeassign_gid, MdlMstCADAssignment values)
        {
            msSQL = "delete from ocs_trn_tprocesstype_assign where processtypeassign_gid='" + processtypeassign_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Process Type Deleted successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while deleted";
            }
        }

        public bool DaUploades_declarationdocument(HttpRequest httpRequest, UploadCADDocumentname objfilename, string employee_gid, string user_gid)
        {
            UploadCADES_DocumentList objdocumentmodel = new UploadCADES_DocumentList();
            HttpFileCollection httpFileCollection;
            string lsdocument_type = string.Empty;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
          
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string pdfFilName = string.Empty;
            Stream ls_readStream;
            string lsdocumenttype_gid = string.Empty;
            string project_flag = httpRequest.Form["project_flag"].ToString();
            String path = lspath;
            string application2sanction_gid = httpRequest.Form["application2sanction_gid"].ToString();
            if (application2sanction_gid == "")
                application2sanction_gid = employee_gid;
            string document_type = httpRequest.Form["document_type"].ToString();


            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";

            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = HttpContext.Current.Server.MapPath("erpdocument" + "/" + lscompany_code + "/" + "Master/esdeclarationdocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month);

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
                            return false;
                        }

                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Master/esdeclarationdocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "Master/esdeclarationdocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        //lspath = HttpContext.Current.Server.MapPath("../../../erpdocument" + "/" + lscompany_code + "/" + "Master/esdeclarationdocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/");
                        //objcmnfunctions.uploadFile(lspath, lsfile_gid);
                        if (document_type == "undefined")
                        {
                            lsdocument_type = "";
                        }
                        else
                        {
                            lsdocument_type = document_type;
                        }
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "Master/esdeclarationdocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        msGetGid = objcmnfunctions.GetMasterGID("SESG");
                        msSQL = " insert into ocs_trn_tuploadesdeclarationdocument( " +
                                    " esdeclaration_gid," +
                                    " application2sanction_gid ," +
                                    " document_name," +
                                    " document_path," +
                                    " document_type ," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + application2sanction_gid + "'," +
                                    "'" + httpPostedFile.FileName.Replace("'", " ") + "'," +
                                    "'" + lspath + msdocument_gid + FileExtension.Replace("'", " ") + "'," +
                                    "'" + lsdocument_type.Replace("'", "") + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }

            }
            catch (Exception ex)
            {
                objfilename.status = false;
                objfilename.message = ex.ToString();
            }
            if (mnResult == 1)
            {
                objfilename.status = true;
                objfilename.message = "E & S Declaration Document Uploaded Successfully";
                return true;
            }
            else
            {
                objfilename.status = false;
                objfilename.message = "Error Ocuured while uploading document";
                return false;
            }
        }

        public void DaGetesdocument(UploadCADDocumentname values, string employee_gid)
        {
            msSQL = " select esdeclaration_gid,document_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,document_path,document_type, " +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as uploaded_by" +
                    " from ocs_trn_tuploadesdeclarationdocument a,hrm_mst_temployee b, adm_mst_tuser c where a.created_by=b.employee_gid" +
                    " and b.user_gid = c.user_gid and ( application2sanction_gid='" + employee_gid + "') and delete_flag='N'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_esdeclarationfilename = new List<UploadCADES_DocumentList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    get_esdeclarationfilename.Add(new UploadCADES_DocumentList
                    {
                        document_path = objcmnstorage.EncryptData((dr_datarow["document_path"].ToString())),
                        document_name = (dr_datarow["document_name"].ToString()),
                        document_gid = (dr_datarow["esdeclaration_gid"].ToString()),
                        document_type = dr_datarow["document_type"].ToString(),
                        uploaded_by = dr_datarow["uploaded_by"].ToString(),
                        updated_date = dr_datarow["uploaded_date"].ToString()
                    });
                }
                values.UploadCADES_DocumentList = get_esdeclarationfilename;
            }
        }

        public void DaGetApp2sanctionesdocument(UploadCADDocumentname values, string application2sanction_gid)
        {
            msSQL = " select esdeclaration_gid,document_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,document_path,document_type, " +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as uploaded_by" +
                    " from ocs_trn_tuploadesdeclarationdocument a,hrm_mst_temployee b, adm_mst_tuser c where a.created_by=b.employee_gid" +
                    " and b.user_gid = c.user_gid and ( application2sanction_gid='" + application2sanction_gid + "') and delete_flag='N'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_esdeclarationfilename = new List<UploadCADES_DocumentList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    get_esdeclarationfilename.Add(new UploadCADES_DocumentList
                    {
                        document_path = objcmnstorage.EncryptData((dr_datarow["document_path"].ToString())),
                        document_name = (dr_datarow["document_name"].ToString()),
                        document_gid = (dr_datarow["esdeclaration_gid"].ToString()),
                        document_type = dr_datarow["document_type"].ToString(),
                        uploaded_by = dr_datarow["uploaded_by"].ToString(),
                        updated_date = dr_datarow["uploaded_date"].ToString()
                    });
                }
                values.UploadCADES_DocumentList = get_esdeclarationfilename;
            }
        }


        public bool DaUploadmaildocument(HttpRequest httpRequest, UploadCADDocumentname objfilename, string employee_gid, string user_gid)
        {
            UploadCADES_DocumentList objdocumentmodel = new UploadCADES_DocumentList();
            HttpFileCollection httpFileCollection;
            string lsdocument_type = string.Empty;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string pdfFilName = string.Empty;
            Stream ls_readStream;
            string lsdocumenttype_gid = string.Empty;
            string project_flag = httpRequest.Form["project_flag"].ToString();
            String path = lspath;
            string application2sanction_gid = httpRequest.Form["application2sanction_gid"].ToString();
            if (application2sanction_gid == "")
                application2sanction_gid = employee_gid;
            string document_type = httpRequest.Form["document_type"].ToString();

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";

            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = HttpContext.Current.Server.MapPath("erpdocument" + "/" + lscompany_code + "/" + "Master/deviationmaildocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month);

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
                            return false;
                        }

                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Master/deviationmaildocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "Master/deviationmaildocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";


                        //lspath = HttpContext.Current.Server.MapPath("erpdocument" + "/" + lscompany_code + "/" + "Master/deviationmaildocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/");
                        //objcmnfunctions.uploadFile(lspath, lsfile_gid);

                        if (document_type == "undefined")
                        {
                            lsdocument_type = "";
                        }
                        else
                        {
                            lsdocument_type = document_type;
                        }
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "Master/deviationmaildocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        msGetGid = objcmnfunctions.GetMasterGID("SMDG");
                        msSQL = " insert into ocs_trn_tdeviationmaildocument( " +
                                    " maildocument_gid," +
                                    " application2sanction_gid ," +
                                    " document_name," +
                                    " document_path," +
                                    " document_type ," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + application2sanction_gid + "'," +
                                    "'" + httpPostedFile.FileName.Replace("'", " ") + "'," +
                                    "'" + lspath + msdocument_gid + FileExtension.Replace("'", " ") + "'," +
                                    "'" + lsdocument_type.Replace("'", "") + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }

            }
            catch
            {

            }
            if (mnResult == 1)
            {
                objfilename.status = true;
                objfilename.message = "Deviation Mail Document Uploaded Successfully";
                return true;
            }
            else
            {
                objfilename.status = false;
                objfilename.message = "Error Ocuured while uploading document";
                return false;
            }
        }

        public void DaGetMaildocument(UploadCADDocumentname values, string employee_gid)
        {
            msSQL = " select maildocument_gid,document_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,document_path,document_type, " +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as uploaded_by" +
                    " from ocs_trn_tdeviationmaildocument a,hrm_mst_temployee b, adm_mst_tuser c where a.created_by=b.employee_gid" +
                    " and b.user_gid = c.user_gid and ( application2sanction_gid='" + employee_gid + "') and delete_flag='N'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_mailfilename = new List<DeviationCADMail_DocumentList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    get_mailfilename.Add(new DeviationCADMail_DocumentList
                    {
                        document_path = objcmnstorage.EncryptData((dr_datarow["document_path"].ToString())),
                        document_name = (dr_datarow["document_name"].ToString()),
                        document_gid = (dr_datarow["maildocument_gid"].ToString()),
                        document_type = dr_datarow["document_type"].ToString(),
                        uploaded_by = dr_datarow["uploaded_by"].ToString(),
                        updated_date = dr_datarow["uploaded_date"].ToString()
                    });
                }
                values.DeviationCADMail_DocumentList = get_mailfilename;
            }
        }

        public void DaGetApp2sanctionMaildocument(UploadCADDocumentname values, string application2sanction_gid)
        {
            msSQL = " select maildocument_gid,document_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,document_path,document_type, " +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as uploaded_by" +
                    " from ocs_trn_tdeviationmaildocument a,hrm_mst_temployee b, adm_mst_tuser c where a.created_by=b.employee_gid" +
                    " and b.user_gid = c.user_gid and ( application2sanction_gid='" + application2sanction_gid + "') and delete_flag='N'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_mailfilename = new List<DeviationCADMail_DocumentList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    get_mailfilename.Add(new DeviationCADMail_DocumentList
                    {
                        document_path = objcmnstorage.EncryptData((dr_datarow["document_path"].ToString())),
                        document_name = (dr_datarow["document_name"].ToString()),
                        document_gid = (dr_datarow["maildocument_gid"].ToString()),
                        document_type = dr_datarow["document_type"].ToString(),
                        uploaded_by = dr_datarow["uploaded_by"].ToString(),
                        updated_date = dr_datarow["uploaded_date"].ToString()
                    });
                }
                values.DeviationCADMail_DocumentList = get_mailfilename;
            }
        }

        public void DaGetuploadesdocumentadd_delete(string document_gid, result values, string employee_gid)
        {
            msSQL = "delete from ocs_trn_tuploadesdeclarationdocument where esdeclaration_gid='" + document_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.message = "Document deleted successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occrued while deleting document";
                values.status = false;
            }
        }

        public void DaMaildocumentadd_delete(string document_gid, result values, string employee_gid)
        {
            msSQL = "delete from ocs_trn_tdeviationmaildocument where maildocument_gid='" + document_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.message = "Document deleted successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occrued while deleting document";
                values.status = false;
            }
        }

        public bool DaGetTempDelete(string employee_gid, string application_gid, result value)
        {

            msSQL = " delete from ocs_trn_tuploadesdeclarationdocument where application2sanction_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " delete from ocs_trn_tdeviationmaildocument where application2sanction_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " delete from ocs_trn_tlimitproductinfo where application_gid='" + application_gid + "' and application2sanction_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " update ocs_mst_tapplication2loan set limit_product='N' where application_gid='" + application_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " update ocs_trn_tcadapplication2loan set limit_product='N' where application_gid='" + application_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
           

            if (mnResult != 0)
            {
                value.status = true;
                return true;
            }
            else
            {
                value.status = false;
                return false;
            }
        }

        public void DaGetSanctionlimitvalidation(Sanctionlimitvalidation values, string application2sanction_gid, string application_gid, string employee_gid)
        {
            if (application2sanction_gid == null || application2sanction_gid == "")
                application2sanction_gid = employee_gid;

            msSQL = " select sum(documented_limit) as documented_limit from ocs_trn_tlimitproductinfo  where " +
                    " application_gid = '" + application_gid + "' and application2sanction_gid = '" + application2sanction_gid + "' " +
                    " and interchangeability='No' and odlim_condition='Applicable'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                values.total_documentlimit = objODBCDataReader["documented_limit"].ToString();
            }
            objODBCDataReader.Close();
            values.status = true;
        }
       // CAD To CC Log Start
        public void DaGetCADtoCCMeetingLog(string application_gid, string employee_gid, MdlCADtoCCMeetingLog values)
        {
            msSQL = " select application_gid,cadtocclog_gid,reason as cadtoccmeeting_reason," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as sentbackcadtocc_by," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as sentbackcadtocc_date " +
                    " from ocs_trn_tcadtoccmeetinglog a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                    " where a.application_gid = '" + application_gid + "' order by a.created_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcadtoccmeetinglog_list = new List<cadtoccmeetinglog_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getcadtoccmeetinglog_list.Add(new cadtoccmeetinglog_list
                    {
                        application_gid = dt["application_gid"].ToString(),
                        cadtocclog_gid = dt["cadtocclog_gid"].ToString(),
                        cadtoccmeeting_reason = dt["cadtoccmeeting_reason"].ToString(),
                        sentbackcadtocc_by = dt["sentbackcadtocc_by"].ToString(),
                        sentbackcadtocc_date = dt["sentbackcadtocc_date"].ToString()
                    });
                }
            }
            values.cadtoccmeetinglog_list = getcadtoccmeetinglog_list;
            dt_datatable.Dispose();
        }
        // CAD To CC Log End
        public void DaGetCADtoCreditLog(string application_gid, string employee_gid, MdlCADtoCreditLog values)
        {
            msSQL = " select application_gid,cadtocreditlog_gid,reason as cadtocredit_reason," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as sentbackcadtocredit_by," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as sentbackcadtocredit_date " +
                    " from ocs_trn_tcadtocreditlog a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                    " where a.application_gid = '" + application_gid + "' order by a.created_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcadtocreditlog_list = new List<cadtocreditlog_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getcadtocreditlog_list.Add(new cadtocreditlog_list
                    {
                        application_gid = dt["application_gid"].ToString(),
                        cadtocreditlog_gid = dt["cadtocreditlog_gid"].ToString(),
                        cadtocredit_reason = dt["cadtocredit_reason"].ToString(),
                        sentbackcadtocredit_by = dt["sentbackcadtocredit_by"].ToString(),
                        sentbackcadtocredit_date = dt["sentbackcadtocredit_date"].ToString()
                    });
                }
            }
            values.cadtocreditlog_list = getcadtocreditlog_list;
            dt_datatable.Dispose();
        }
//Send Back to Credit without CC Log
        public void DaGetCreditWithoutCCLog(string application_gid, string employee_gid, MdlCreditWithoutCCLog values)
        {
            msSQL = " select application_gid,ccmeetingskip_gid,reason as creditwithoutcc_reason," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as sentbackcreditwithoutcc_by," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as sentbackcreditwithoutcc_date " +
                    " from ocs_trn_tccmeetingskip a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                    " where a.application_gid = '" + application_gid + "' order by a.created_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcreditwithoutcclog_list = new List<creditwithoutcclog_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getcreditwithoutcclog_list.Add(new creditwithoutcclog_list
                    {
                        application_gid = dt["application_gid"].ToString(),
                        ccmeetingskip_gid = dt["ccmeetingskip_gid"].ToString(),
                        creditwithoutcc_reason = dt["creditwithoutcc_reason"].ToString(),
                        sentbackcreditwithoutcc_by = dt["sentbackcreditwithoutcc_by"].ToString(),
                        sentbackcreditwithoutcc_date = dt["sentbackcreditwithoutcc_date"].ToString()
                    });
                }
            }
            values.creditwithoutcclog_list = getcreditwithoutcclog_list;
            dt_datatable.Dispose();
        }
        public void DaGetApprovalDetails(string application_gid, MdlSanctionApprovalDetails values)
        {
            try
            {
                msSQL = " select application_gid,maker_name,checker_name,approver_name, " +
                        " date_format(a.maker_approveddate,'%d-%m-%Y %h:%i %p') as maker_approveddate, " +
                        " date_format(a.checker_approveddate,'%d-%m-%Y %h:%i %p') as checker_approveddate, " +
                        " date_format(a.approver_approveddate,'%d-%m-%Y %h:%i %p') as approver_approveddate " +
                        " from ocs_trn_tprocesstype_assign a where processtype_name = 'Accept' and menu_gid = 'CADMGTSAN' and application_gid='" + application_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.application_gid = objODBCDatareader["application_gid"].ToString();
                    values.maker_name = objODBCDatareader["maker_name"].ToString();
                    values.checker_name = objODBCDatareader["checker_name"].ToString();
                    values.approver_name = objODBCDatareader["approver_name"].ToString();
                    values.maker_approveddate = objODBCDatareader["maker_approveddate"].ToString();
                    values.checker_approveddate = objODBCDatareader["checker_approveddate"].ToString();
                    values.approver_approveddate = objODBCDatareader["approver_approveddate"].ToString();
                }
                values.status = true;
                values.message = "success";
                objODBCDatareader.Close();
            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }
        }

        public void DaGetDocChecklistApprovalDtls(string application_gid, MdlSanctionApprovalDetails values)
        {
            try
            {
                msSQL = " select application_gid,maker_name,checker_name,approver_name, " +
                        " date_format(a.maker_approveddate,'%d-%m-%Y %h:%i %p') as maker_approveddate, " +
                        " date_format(a.checker_approveddate,'%d-%m-%Y %h:%i %p') as checker_approveddate, " +
                        " date_format(a.approver_approveddate,'%d-%m-%Y %h:%i %p') as approver_approveddate " +
                        " from ocs_trn_tprocesstype_assign a where processtype_name = 'Accept' and menu_gid = 'CADMGTDCL' and application_gid='" + application_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.application_gid = objODBCDatareader["application_gid"].ToString();
                    values.maker_name = objODBCDatareader["maker_name"].ToString();
                    values.checker_name = objODBCDatareader["checker_name"].ToString();
                    values.approver_name = objODBCDatareader["approver_name"].ToString();
                    values.maker_approveddate = objODBCDatareader["maker_approveddate"].ToString();
                    values.checker_approveddate = objODBCDatareader["checker_approveddate"].ToString();
                    values.approver_approveddate = objODBCDatareader["approver_approveddate"].ToString();
                }
                values.status = true;
                values.message = "success";
                objODBCDatareader.Close();
            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }
        }

        public void DaGetDocChecklistApprovalCompletedSummary(string employee_gid, MdlMstCAD values)
        {
            //msSQL = " select a.application_gid,a.application_no,a.customerref_name,a.customer_urn,a.creditgroup_name,h.sanction_refno, " +
            //         " a.customer_name as customer_name,date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as ccapproved_date, a.approval_status," +
            //         " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as cadaccepted_by, " +
            //         " date_format(a.processupdated_date, '%d-%m-%Y %h:%i %p') as cadaccepted_date, a.created_by, " +
            //         " concat(f.user_firstname, ' ', f.user_lastname, ' / ', f.user_code) as checkerapproved_by, " +
            //         " date_format(d.approved_on, '%d-%m-%Y %h:%i %p') as approved_on, " +
            //         " date_format(d.checkerapproved_on, '%d-%m-%Y %h:%i %p') as checkerapproved_on,a.customer_urn," +
            //         " a.creditgroup_gid, docchecklist_approvalflag, d.approval_status as approval, g.cadgroup_name,"+
            //         " a.renewal_flag,a.enhancement_flag from ocs_trn_tcadapplication a " +
            //         " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by " +
            //         " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
            //         " left join ocs_trn_tapplication2docchecklist d on d.application_gid = a.application_gid " +
            //         " left join hrm_mst_temployee e on e.employee_gid = d.checkerapproved_by " +
            //         " left join adm_mst_tuser f on f.user_gid = e.user_gid " +
            //         " left join ocs_trn_tprocesstype_assign g on g.application_gid = a.application_gid " +
            //         " left join ocs_trn_tapplication2sanction h on h.application_gid = a.application_gid " +
            //         " where a.process_type = 'Accept' and docchecklist_approvalflag='Y' and " +
            //         " a.application_gid in (Select application_gid from ocs_trn_tprocesstype_assign where menu_gid = 'CADMGTDCL' and approver_approvalflag='Y'  )" +
            //         " group by a.application_gid order by a.updated_date desc ";
            msSQL = "call ocs_trn_spdocchecklistapprovalcompletedsummary ()";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<cadapplicationlist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    string lsccgroup_name;
                    string lsccadmin_name;
                    string lscadgroup_name;

                    //msSQL = "select group_concat(ccgroup_name) as ccgroup_name from ocs_trn_tcadapplication where application_gid='" + dt["application_gid"] + "'";
                    //objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    //if (objODBCDataReader.HasRows == true)
                    //{
                    //    lsccgroup_name = objODBCDataReader["ccgroup_name"].ToString();
                    //}
                    //else
                    //{
                    //    lsccgroup_name = "";
                    //}
                    //objODBCDataReader.Close();
                    //msSQL = "select group_concat(ccadmin_name) as ccadmin_name from ocs_mst_tccschedulemeeting where application_gid='" + dt["application_gid"] + "'";
                    //objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    //if (objODBCDataReader.HasRows == true)
                    //{
                    //    lsccadmin_name = objODBCDataReader["ccadmin_name"].ToString();
                    //}
                    //else
                    //{
                    //    lsccadmin_name = "";
                    //}
                    //objODBCDataReader.Close();
                    //msSQL = "select group_concat(cadgroup_name) as cadgroup_name from ocs_trn_tprocesstype_assign where application_gid='" + dt["application_gid"] + "'";
                    //objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    //if (objODBCDataReader.HasRows == true)
                    //{
                    //    lscadgroup_name = objODBCDataReader["cadgroup_name"].ToString();
                    //}
                    //else
                    //{
                    //    lscadgroup_name = "";
                    //}
                    //objODBCDataReader.Close();
                    getapplicationadd_list.Add(new cadapplicationlist
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        ccapproved_date = dt["ccapproved_date"].ToString(),
                        creditgroup_name = dt["creditgroup_name"].ToString(),
                        //ccgroup_name = lsccgroup_name,
                        //ccadmin_name = lsccadmin_name,
                        ccgroup_name = dt["ccgroup_name"].ToString(),
                        cadgroupname = dt["cadgroup_name"].ToString(),
                        cadaccepted_by = dt["cadaccepted_by"].ToString(),
                        cadaccepted_date = dt["cadaccepted_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        docchecklist_approvalflag = dt["docchecklist_approvalflag"].ToString(),
                        approval = dt["approval"].ToString(),
                        checkerapproved_by = dt["checkerapproved_by"].ToString(),
                        checkerapproved_on = dt["checkerapproved_on"].ToString(),
                        sanction_refno = dt["sanction_refno"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        approved_on = dt["approved_on"].ToString(),
                        renewal_flag = dt["renewal_flag"].ToString(),
                        enhancement_flag = dt["enhancement_flag"].ToString()
                    });

                }
            }
            values.cadapplicationlist = getapplicationadd_list;
            dt_datatable.Dispose();
        }

        public void GetCADUrnGroupingDtlsSummary(string customer_urn, string employee_gid, MdlMstCAD values)
        {
            msSQL = " select a.application_gid,a.application_no,a.customer_urn, " +
                     " a.customer_name as customer_name, a.approval_status," +
                     " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as cadaccepted_by, " +
                     " a.relationshipmanager_name,a.overalllimit_amount, enhancement_flag," +
                     " date_format(a.processupdated_date, '%d-%m-%Y %h:%i %p') as cadaccepted_date, a.created_by, " +
                     " d.cadgroup_name,a.vertical_name,a.region,a.renewal_flag,approval_status, " +
                     " (select application_gid from ocs_mst_tapplication g " +
                     " where g.created_date = (select max(f.created_date) from ocs_mst_tapplication f where f.customer_urn = g.customer_urn) " +
                     " and g.process_type is null and g.renewal_flag = 'Y' and g.customer_urn = a.customer_urn) as renewal_status, " +
                     " (select application_gid from ocs_mst_tapplication h " +
                     " where h.created_date = (select max(i.created_date) from ocs_mst_tapplication i where i.customer_urn = h.customer_urn) " +
                     " and h.process_type is null and h.enhancement_flag = 'Y' and h.customer_urn = a.customer_urn) as enhancement_status,  " +
                     " (select approval_status from ocs_mst_tapplication g " +
                     " where g.created_date = (select max(f.created_date) from ocs_mst_tapplication f where f.customer_urn = g.customer_urn) " +
                     " and g.process_type is null and g.renewal_flag = 'Y' and " +
                     " (g.approval_status = 'Rejected By Business' or g.approval_status = 'Rejected By Credit' or g.approval_status = 'CC Rejected'" +
                     " or g.approval_status='Rejected by Credit Manager') and " +
                     " g.customer_urn = a.customer_urn) as renewalapproval_status, " +
                     " (select approval_status from ocs_mst_tapplication g " +
                     " where g.created_date = (select max(f.created_date) from ocs_mst_tapplication f where f.customer_urn = g.customer_urn) " +
                     " and g.process_type is null and g.enhancement_flag = 'Y' and " +
                     " (g.approval_status = 'Rejected By Business' or g.approval_status = 'Rejected By Credit' or g.approval_status = 'CC Rejected'" +
                     " or g.approval_status = 'Rejected by Credit Manager') and " +
                     " g.customer_urn = a.customer_urn) as enhancementapproval_status " +
                     " from ocs_trn_tcadapplication a " +
                     " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by " +
                     " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                     " left join ocs_trn_tprocesstype_assign d on d.application_gid = a.application_gid " +
                     " where a.process_type = 'Accept'  and a.customer_urn='" + customer_urn + "' " +
                     " group by a.application_gid order by a.updated_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<cadapplicationlist>();
            if (dt_datatable.Rows.Count != 0)
            {
              
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    string lsrenewalenhancementapproval_status = "";
                    msSQL = " select application_gid from ocs_trn_tcadapplication g where g.created_date = (select max(f.created_date) from ocs_trn_tcadapplication f where f.customer_urn = '" + customer_urn + "') " +
                            " and g.process_type = 'Accept' and g.renewal_flag != 'Y' and  g.enhancement_flag != 'Y'";
                    var lsnormalflow_flag = objdbconn.GetExecuteScalar(msSQL);
                    if (lsnormalflow_flag != "")
                    {
                        msSQL = " select application_gid from ocs_trn_tcadapplication g where g.created_date = (select max(f.created_date) from ocs_trn_tcadapplication f where f.customer_urn = '" + customer_urn + "') " +
                         " and g.process_type = 'Accept' and g.application_gid = '" + dt["application_gid"].ToString() + "' ";
                        var lsnormalflow_flag1 = objdbconn.GetExecuteScalar(msSQL);
                        if (lsnormalflow_flag1 != "")
                        {
                            lsrenewalenhancementapproval_status = "Y";
                        }
                        
                    }
                    msSQL = " select renewal_flag from ocs_mst_tapplication g where g.created_date = (select max(f.created_date) from ocs_mst_tapplication f where f.customer_urn = '" + customer_urn + "') " +
                            " and g.process_type is null ";
                    var lsrenewal_flag = objdbconn.GetExecuteScalar(msSQL);

                
                    if (lsrenewal_flag == "Y")
                    {

                        msSQL = " select renewal_flag from ocs_mst_tapplication g where g.created_date = (select max(f.created_date) from ocs_mst_tapplication f where f.customer_urn = '" + customer_urn + "') " +
                                " and g.process_type is null and " +
                                " (g.approval_status = 'Rejected By Business' or g.approval_status = 'Rejected By Credit' or g.approval_status = 'CC Rejected'" +
                                " or g.approval_status = 'Rejected by Credit Manager') ";
                        var lsrenewalreject_flag = objdbconn.GetExecuteScalar(msSQL);
                        if (lsrenewalreject_flag == "Y")
                        {
                            msSQL = " select application_gid from ocs_trn_tcadapplication g where g.created_date = (select max(f.created_date) from ocs_trn_tcadapplication f where f.customer_urn = '" + customer_urn + "' and f.process_type = 'Accept') " +
                           " and g.process_type = 'Accept' and g.application_gid = '" + dt["application_gid"].ToString() + "' ";
                            var lsnormalflow_flag1 = objdbconn.GetExecuteScalar(msSQL);
                            if (lsnormalflow_flag1 != "")
                            {
                                lsrenewalenhancementapproval_status = "Y";
                            }
                        }
                       
                    }
                    msSQL = "select enhancement_flag from ocs_mst_tapplication h where h.created_date = (select max(i.created_date) from ocs_mst_tapplication i where i.customer_urn = '" + customer_urn + "') " +
                        " and h.process_type is null ";
                    var lsenhancement_flag = objdbconn.GetExecuteScalar(msSQL);
                    if (lsenhancement_flag == "Y")
                    {

                        msSQL = " select enhancement_flag from ocs_mst_tapplication g where g.created_date = (select max(f.created_date) from ocs_mst_tapplication f where f.customer_urn = '" + customer_urn + "') " +
                                " and g.process_type is null and  " +
                                " (g.approval_status = 'Rejected By Business' or g.approval_status = 'Rejected By Credit' or g.approval_status = 'CC Rejected'" +
                                " or g.approval_status = 'Rejected by Credit Manager') ";
                        var lsenhancementreject_flag1 = objdbconn.GetExecuteScalar(msSQL);
                        if (lsenhancementreject_flag1 == "Y")
                        {
                            msSQL = " select application_gid from ocs_trn_tcadapplication g where g.created_date = (select max(f.created_date) from ocs_trn_tcadapplication f where f.customer_urn = '" + customer_urn + "'  and f.process_type = 'Accept') " +
                           " and g.process_type = 'Accept' and g.application_gid = '" + dt["application_gid"].ToString() + "' ";
                            var lsnormalflow_flag1 = objdbconn.GetExecuteScalar(msSQL);
                            if (lsnormalflow_flag1 != "")
                            {
                                lsrenewalenhancementapproval_status = "Y";
                            }
                        }

                    }
                    msSQL = " select renewal_flag from ocs_trn_tcadapplication g where g.created_date = (select max(f.created_date) from ocs_trn_tcadapplication f where f.customer_urn = '" + customer_urn + "') " +
                         " and g.process_type = 'Accept' and g.renewal_flag = 'Y'";
                    var lsrenewalaccepted_flag = objdbconn.GetExecuteScalar(msSQL);
                    if (lsrenewalaccepted_flag == "Y")
                    {
                        msSQL = " select application_gid from ocs_trn_tcadapplication g where g.created_date = (select max(f.created_date) from ocs_trn_tcadapplication f where f.customer_urn = '" + customer_urn + "') " +
                            " and g.process_type = 'Accept' and g.application_gid = '" + dt["application_gid"].ToString() + "' and g.renewal_flag = 'Y'";
                        var lsnormalflow_flag1 = objdbconn.GetExecuteScalar(msSQL);
                        if (lsnormalflow_flag1 != "")
                        {
                            lsrenewalenhancementapproval_status = "Y";
                        }
                    }
                    msSQL = "select enhancement_flag from ocs_trn_tcadapplication h where h.created_date = (select max(i.created_date) from ocs_trn_tcadapplication i where i.customer_urn = '" + customer_urn + "') " +
                            " and h.process_type = 'Accept' and h.enhancement_flag = 'Y'";
                    var lsenhancementaccepted_flag = objdbconn.GetExecuteScalar(msSQL);
                    if (lsenhancementaccepted_flag == "Y")
                    {
                        msSQL = " select application_gid from ocs_trn_tcadapplication g where g.created_date = (select max(f.created_date) from ocs_trn_tcadapplication f where f.customer_urn = '" + customer_urn + "') " +
                            " and g.process_type = 'Accept' and g.application_gid = '" + dt["application_gid"].ToString() + "' and g.enhancement_flag = 'Y'";
                        var lsnormalflow_flag1 = objdbconn.GetExecuteScalar(msSQL);
                        if (lsnormalflow_flag1 != "")
                        {
                            lsrenewalenhancementapproval_status = "Y";
                        }
                    }

                    msSQL = " select application_gid from ocs_mst_tapplication g where g.created_date = (select max(f.created_date) from ocs_mst_tapplication f where f.customer_urn = '" + customer_urn + "') " +
                               " and g.process_type is null and (g.enhancement_flag = 'Y' or  g.renewal_flag = 'Y'  ) and" +
                               "  (g.approval_status = 'Incomplete' or g.approval_status = 'Submitted to Approval' or g.approval_status = 'Submitted to Underwriting' or g.approval_status = 'Sent Back to CC'  " +
                               " or g.approval_status = 'Sent Back to Credit' or g.approval_status = 'Submitted to CC'or g.approval_status = 'Submitted to Credit Approval' or g.approval_status = 'Submitted to Heads Approval' " +
                               " or g.approval_status ='CC Pending'or g.approval_status = 'CC Approved' )  ";
                    var lsenhancementreject_flag = objdbconn.GetExecuteScalar(msSQL);
                    if (lsenhancementreject_flag != "")
                    {
                       
                            lsrenewalenhancementapproval_status = "";
                      
                    }

                    getapplicationadd_list.Add(new cadapplicationlist
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        cadgroupname = dt["cadgroup_name"].ToString(),
                        cadaccepted_by = dt["cadaccepted_by"].ToString(),
                        cadaccepted_date = dt["cadaccepted_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        vertical_name = dt["vertical_name"].ToString(),
                        region = dt["region"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        relationshipmanager_name = dt["relationshipmanager_name"].ToString(),
                        overalllimit_amount = dt["overalllimit_amount"].ToString(),
                        renewal_flag = dt["renewal_flag"].ToString(),
                        renewal_status = dt["renewal_status"].ToString(),
                        enhancement_status = dt["enhancement_status"].ToString(),
                        enhancement_flag = dt["enhancement_flag"].ToString(),
                        renewalenhancementapproval_status = lsrenewalenhancementapproval_status,
                        //enhancementapproval_status = lsenhancementapproval_status
                    });

                }
            }
            values.cadapplicationlist = getapplicationadd_list;
            dt_datatable.Dispose();
        }
        //public void GetCADUrnGroupingDtlsSummary(string customer_urn, string employee_gid, MdlMstCAD values)
        //{
        //    msSQL =  " select a.application_gid,a.application_no,a.customer_urn, " +
        //             " a.customer_name as customer_name, a.approval_status," +
        //             " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as cadaccepted_by, " +
        //             " a.relationshipmanager_name,a.overalllimit_amount, enhancement_flag," +
        //             " date_format(a.processupdated_date, '%d-%m-%Y %h:%i %p') as cadaccepted_date, a.created_by, " +
        //             " d.cadgroup_name,a.vertical_name,a.region,a.renewal_flag,approval_status, " +
        //             " (select application_gid from ocs_mst_tapplication g " +
        //             " where g.created_date = (select max(f.created_date) from ocs_mst_tapplication f where f.customer_urn = g.customer_urn ) " +
        //             " and g.process_type = 'Accept' and g.renewal_flag = 'Y' and g.application_gid = a.application_gid) as renewal_status, " +
        //             " (select application_gid from ocs_mst_tapplication h " +
        //             " where h.created_date = (select max(i.created_date) from ocs_mst_tapplication i where i.customer_urn = h.customer_urn ) " +
        //             " and h.process_type = 'Accept' and h.enhancement_flag = 'Y'  and h.application_gid = a.application_gid) as enhancement_status,  " +
        //             " (select approval_status from ocs_mst_tapplication g " +
        //             " where g.created_date = (select max(f.created_date) from ocs_mst_tapplication f where f.customer_urn = g.customer_urn ) " +
        //             " and " +
        //             " (g.approval_status = 'Rejected By Business' or g.approval_status = 'Rejected By Credit' or g.approval_status = 'CC Rejected'" +
        //             " or g.approval_status='Rejected by Credit Manager') and g.process_type is null and g.renewal_flag = 'Y' and " +
        //             " g.application_gid = a.application_gid) as renewalapproval_status, " +
        //             " (select approval_status from ocs_mst_tapplication g " +
        //             " where g.created_date = (select max(f.created_date) from ocs_mst_tapplication f where f.customer_urn = g.customer_urn ) " +
        //             " and " +
        //             " (g.approval_status = 'Rejected By Business' or g.approval_status = 'Rejected By Credit' or g.approval_status = 'CC Rejected'"+
        //             " or g.approval_status = 'Rejected by Credit Manager') and g.process_type is null and g.enhancement_flag = 'Y' and " +
        //             " g.application_gid = a.application_gid) as enhancementapproval_status " +                   
        //             " from ocs_trn_tcadapplication a " +
        //             " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by " +
        //             " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
        //             " left join ocs_trn_tprocesstype_assign d on d.application_gid = a.application_gid " +
        //             " where a.process_type = 'Accept'  and a.customer_urn='" + customer_urn + "' " +
        //             " group by a.application_gid order by a.updated_date desc ";
        //    dt_datatable = objdbconn.GetDataTable(msSQL);
        //    var getapplicationadd_list = new List<cadapplicationlist>();
        //    if (dt_datatable.Rows.Count != 0)
        //    {
        //        string lsrenewalapproval_status = "", lsenhancementapproval_status = "";
        //        foreach (DataRow dt in dt_datatable.Rows)
        //        {
        //            msSQL = " select renewal_flag from ocs_mst_tapplication g where g.created_date = (select max(f.created_date) from ocs_mst_tapplication f where f.customer_urn = '" + customer_urn + "' and f.renewal_flag = 'Y') and g.process_type is null and " +
        //                    " g.application_gid = '" + dt["application_gid"] + "'";
        //            var lsrenewal_flag = objdbconn.GetExecuteScalar(msSQL);

        //            msSQL = " select enhancement_flag from ocs_mst_tapplication h where h.created_date = (select max(i.created_date) from ocs_mst_tapplication i where i.customer_urn = '" + customer_urn + "' and i.enhancement_flag = 'Y')  and h.process_type is null and " +
        //                    " h.application_gid = '" + dt["application_gid"] + "'";
        //            var lsenhancement_flag = objdbconn.GetExecuteScalar(msSQL);
        //            //string lsrenewal_flag, lsenhancement_flag;
        //            //lsrenewal_flag = dt["lsrenewal_flag"].ToString();
        //            //lsenhancement_flag = dt["lsenhancement_flag"].ToString();
        //            if (lsrenewal_flag == "Y")
        //            {
        //                lsrenewalapproval_status = dt["renewalapproval_status"].ToString();
        //            }
        //            else if (lsenhancement_flag == "Y")
        //            {
        //                lsenhancementapproval_status = dt["enhancementapproval_status"].ToString();
        //            }
        //            else { }

        //            getapplicationadd_list.Add(new cadapplicationlist
        //            {
        //                application_no = dt["application_no"].ToString(),
        //                customer_name = dt["customer_name"].ToString(),
        //                application_gid = dt["application_gid"].ToString(),
        //                approval_status = dt["approval_status"].ToString(),
        //                cadgroupname = dt["cadgroup_name"].ToString(),
        //                cadaccepted_by = dt["cadaccepted_by"].ToString(),
        //                cadaccepted_date = dt["cadaccepted_date"].ToString(),
        //                created_by = dt["created_by"].ToString(),
        //                vertical_name = dt["vertical_name"].ToString(),
        //                region = dt["region"].ToString(),
        //                customer_urn = dt["customer_urn"].ToString(),
        //                relationshipmanager_name = dt["relationshipmanager_name"].ToString(),
        //                overalllimit_amount = dt["overalllimit_amount"].ToString(),
        //                renewal_flag = dt["renewal_flag"].ToString(),
        //                renewal_status = dt["renewal_status"].ToString(),
        //                enhancement_status = dt["enhancement_status"].ToString(),                        
        //                enhancement_flag = dt["enhancement_flag"].ToString(),
        //                renewalapproval_status = lsrenewalapproval_status,
        //                enhancementapproval_status = lsenhancementapproval_status
        //            });

        //        }
        //    }
        //    values.cadapplicationlist = getapplicationadd_list;
        //    dt_datatable.Dispose();
        //}

        public void DaGetCADUrnGroupingSummary(string employee_gid, MdlMstCAD values)
        {
            msSQL = " select a.application_gid,a.application_no,a.customerref_name,a.customer_urn,a.creditgroup_name, " +
                     " a.customer_name as customer_name, date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as ccapproved_date, a.approval_status," +
                     " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as cadaccepted_by, " +
                     " date_format(a.processupdated_date, '%d-%m-%Y %h:%i %p') as cadaccepted_date, a.created_by, " +
                     " a.creditgroup_gid, a.vertical_name,a.region,a.customer_urn,d.customer2tag_gid,d.npatag_flag,d.legaltag_flag,"+
                     " a.renewal_flag,a.enhancement_flag from ocs_trn_tcadapplication a " +
                     " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by " +
                     " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                     " left join ocs_trn_tcustomer2tag d on d.urn_number = a.customer_urn " +
                     " where a.process_type = 'Accept' and (a.processupdated_date = (select max(e.processupdated_date) from ocs_trn_tcadapplication e where e.customer_urn = a.customer_urn and a.customer_urn <> '')) " +
                     " order by a.processupdated_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<cadapplicationlist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getapplicationadd_list.Add(new cadapplicationlist
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        ccapproved_date = dt["ccapproved_date"].ToString(),
                        creditgroup_name = dt["creditgroup_name"].ToString(),
                        cadaccepted_by = dt["cadaccepted_by"].ToString(),
                        cadaccepted_date = dt["cadaccepted_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        vertical_name = dt["vertical_name"].ToString(),
                        region = dt["region"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        npatag_flag = dt["npatag_flag"].ToString(),
                        legaltag_flag = dt["legaltag_flag"].ToString(),
                        customer2tag_gid = dt["customer2tag_gid"].ToString(),
                        renewal_flag = dt["renewal_flag"].ToString(),
                        enhancement_flag = dt["enhancement_flag"].ToString()
                    });

                }
                values.cadapplicationlist = getapplicationadd_list;
                values.status = true;
            }
            else
            {
                values.status = false;
            }

            dt_datatable.Dispose();
        }

        public void DaGetCADAcceptDetails(string application_gid, MdlCADAcceptDetails values)
        {
            try
            {
                msSQL = " select a.application_gid,d.cadgroup_name, " +
                        " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as cadaccepted_by, " +
                        " date_format(a.processupdated_date, '%d-%m-%Y %h:%i %p') as cadaccepted_date" +
                        " from ocs_trn_tcadapplication a " +
                        " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by " +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " left join ocs_trn_tprocesstype_assign d on d.application_gid = a.application_gid " +
                        " where a.application_gid='" + application_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.application_gid = objODBCDatareader["application_gid"].ToString();
                    values.cadgroup_name = objODBCDatareader["cadgroup_name"].ToString();
                    values.cadaccepted_by = objODBCDatareader["cadaccepted_by"].ToString();
                    values.cadaccepted_date = objODBCDatareader["cadaccepted_date"].ToString();
                }
                values.status = true;
                values.message = "success";
                objODBCDatareader.Close();
            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }
        }

        public void DaGetRMCADUrnGroupingSummary(string employee_gid, MdlMstCAD values)
        {
            //msSQL = " select a.application_gid,a.application_no,a.customer_urn,a.customer_name as customer_name,a.approval_status, " +
            //         " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as cadaccepted_by, " +
            //         " date_format(a.processupdated_date, '%d-%m-%Y %h:%i %p') as cadaccepted_date, a.created_by,a.vertical_name,a.region, " +
            //         " (select application_gid from ocs_mst_tapplication g " +
            //         " where g.created_date = (select max(f.created_date) from ocs_mst_tapplication f where f.customer_urn = g.customer_urn) " +
            //         " and g.process_type is null and g.renewal_flag = 'Y' and g.customer_urn = a.customer_urn) as renewal_status, " +
            //         " (select application_gid from ocs_mst_tapplication h " +
            //         " where h.created_date = (select max(i.created_date) from ocs_mst_tapplication i where i.customer_urn = h.customer_urn) " +
            //         " and h.process_type is null and h.enhancement_flag = 'Y' and h.customer_urn = a.customer_urn) as enhancement_status,  " +
            //         " (select approval_status from ocs_mst_tapplication g " +
            //         " where g.created_date = (select max(f.created_date) from ocs_mst_tapplication f where f.customer_urn = g.customer_urn) " +
            //         " and g.process_type is null and g.renewal_flag = 'Y' and " +
            //         " (g.approval_status = 'Rejected By Business' or g.approval_status = 'Rejected By Credit' or g.approval_status = 'CC Rejected'"+
            //         " or g.approval_status='Rejected by Credit Manager') and " +
            //         " g.customer_urn = a.customer_urn) as renewalapproval_status, " +
            //         " (select approval_status from ocs_mst_tapplication g " +
            //         " where g.created_date = (select max(f.created_date) from ocs_mst_tapplication f where f.customer_urn = g.customer_urn) " +
            //         " and g.process_type is null and g.enhancement_flag = 'Y' and " +
            //         " (g.approval_status = 'Rejected By Business' or g.approval_status = 'Rejected By Credit' or g.approval_status = 'CC Rejected'" +
            //         " or g.approval_status='Rejected by Credit Manager') and " +
            //         " g.customer_urn = a.customer_urn) as enhancementapproval_status " +
            //         " from ocs_trn_tcadapplication a " + 
            //         " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by " +
            //         " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
            //         " where a.process_type = 'Accept' and (a.processupdated_date = (select max(e.processupdated_date) from ocs_trn_tcadapplication e where e.customer_urn = a.customer_urn and a.customer_urn <> '')) " +
            //         " and relationshipmanager_gid='" + employee_gid + "' order by a.processupdated_date desc ";
            //dt_datatable = objdbconn.GetDataTable(msSQL);


            msSQL = "call ocs_trn_sprmcadurngroupingsummary ('" + employee_gid + "')";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<cadapplicationlist>();
            if (dt_datatable.Rows.Count != 0)
            {
               
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    string lsrenewalapproval_status = "", lsenhancementapproval_status = "";
                    string  lsrenewal_flag,lsenhancement_flag;
                    //msSQL = " select renewal_flag from ocs_mst_tapplication g where " +
                    //        " g.created_date = (select max(f.created_date) from ocs_mst_tapplication f where f.customer_urn ='" + dt["customer_urn"] + "')" +
                    //        " and g.process_type is null and g.renewal_flag = 'Y'";
                    //var lsrenewal_flag = objdbconn.GetExecuteScalar(msSQL);

                    //msSQL = "select enhancement_flag from ocs_mst_tapplication h where " +
                    //        " h.created_date = (select max(i.created_date) from ocs_mst_tapplication i where i.customer_urn ='" + dt["customer_urn"] + "')" +
                    //        " and h.process_type is null and h.enhancement_flag = 'Y'";
                    //var lsenhancement_flag = objdbconn.GetExecuteScalar(msSQL);

                    lsrenewal_flag = dt["renewal_flag"].ToString();
                    lsenhancement_flag = dt["enhancement_flag"].ToString();
                 
                    if (lsrenewal_flag == "Y")
                    {
                         lsrenewalapproval_status = dt["renewalapproval_status"].ToString();
                    }
                    else if (lsenhancement_flag == "Y")
                    {
                        lsenhancementapproval_status = dt["enhancementapproval_status"].ToString();
                    }
                    else { }

                    getapplicationadd_list.Add(new cadapplicationlist
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        application_gid = dt["application_gid"].ToString(),                       
                        approval_status = dt["approval_status"].ToString(),
                        cadaccepted_by = dt["cadaccepted_by"].ToString(),
                        cadaccepted_date = dt["cadaccepted_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        vertical_name = dt["vertical_name"].ToString(),
                        region = dt["region"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        renewal_status = dt["renewal_status"].ToString(),
                        enhancement_status = dt["enhancement_status"].ToString(),
                        renewalapproval_status = lsrenewalapproval_status, 
                        enhancementapproval_status = lsenhancementapproval_status
                    });

                }
            }
            values.cadapplicationlist = getapplicationadd_list;
            dt_datatable.Dispose();
        }

        public void DaGetRMMyCustomerListSummary(string employee_gid, MdlMstCAD values)
        {
            //msSQL = " select a.application_gid,a.application_no,a.customerref_name,a.customer_urn,a.creditgroup_name,a.product_gid,a.variety_gid, " +
            //         " a.customer_name as customer_name, date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as ccapproved_date, a.approval_status," +
            //         " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as cadaccepted_by, " +
            //         " date_format(a.processupdated_date, '%d-%m-%Y %h:%i %p') as cadaccepted_date, a.created_by, " +
            //         " a.creditgroup_gid, d.cadgroup_name,a.vertical_name,a.region,a.customer_urn from ocs_trn_tcadapplication a " +
            //         " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by " +
            //         " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
            //         " left join ocs_trn_tprocesstype_assign d on d.application_gid = a.application_gid " +
            //         " where a.process_type = 'Accept' and a.customer_urn = '' and relationshipmanager_gid='" + employee_gid + "'" +
            //         " group by a.application_gid order by a.updated_date desc ";
            //dt_datatable = objdbconn.GetDataTable(msSQL);

            msSQL = "call ocs_trn_sprmmycustomerlistsummary ('" + employee_gid + "')";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<cadapplicationlist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    //string lsccgroup_name;
                    //string lsccadmin_name;

                    //msSQL = "select group_concat(ccgroup_name) as ccgroup_name from ocs_mst_tapplication where application_gid='" + dt["application_gid"] + "'";
                    //objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    //if (objODBCDataReader.HasRows == true)
                    //{
                    //    lsccgroup_name = objODBCDataReader["ccgroup_name"].ToString();
                    //}
                    //else
                    //{
                    //    lsccgroup_name = "";
                    //}
                    //objODBCDataReader.Close();
                    //msSQL = "select group_concat(ccadmin_name) as ccadmin_name from ocs_mst_tccschedulemeeting where application_gid='" + dt["application_gid"] + "'";
                    //objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    //if (objODBCDataReader.HasRows == true)
                    //{
                    //    lsccadmin_name = objODBCDataReader["ccadmin_name"].ToString();
                    //}
                    //else
                    //{
                    //    lsccadmin_name = "";
                    //}
                    //objODBCDataReader.Close();
                    getapplicationadd_list.Add(new cadapplicationlist
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        ccapproved_date = dt["ccapproved_date"].ToString(),
                        creditgroup_name = dt["creditgroup_name"].ToString(),
                        //ccgroup_name = lsccgroup_name,
                        //ccadmin_name = lsccadmin_name,
                        ccgroup_name = dt["ccgroup_name"].ToString(),
                        ccadmin_name = dt["ccadmin_name"].ToString(),
                        cadgroupname = dt["cadgroup_name"].ToString(),
                        cadaccepted_by = dt["cadaccepted_by"].ToString(),
                        cadaccepted_date = dt["cadaccepted_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        vertical_name = dt["vertical_name"].ToString(),
                        region = dt["region"].ToString(),
                        product_gid = dt["product_gid"].ToString(),
                        variety_gid = dt["variety_gid"].ToString(),
                        customer_urn = dt["customer_urn"].ToString()
                    });

                }
            }
            values.cadapplicationlist = getapplicationadd_list;
            dt_datatable.Dispose();
        }
        public void DaGetRMRnewalApplicationSummary(string customer_urn, string employee_gid, MdlMstCAD values)
        {
            msSQL =  " select a.application_gid,a.application_no,a.customerref_name,a.customer_urn,a.creditgroup_name, " +
                     " a.customer_name as customer_name, date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as ccapproved_date, a.approval_status," +
                     " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as cadaccepted_by, " +
                     " a.relationshipmanager_name,a.overalllimit_amount, " +
                     " date_format(a.processupdated_date, '%d-%m-%Y %h:%i %p') as cadaccepted_date, a.created_by, " +
                     " a.creditgroup_gid,a.vertical_name,a.region,a.renewal_flag,a.approval_status,a.enhancement_flag " +
                     " from ocs_mst_tapplication a " +
                     " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by " +
                     " left join adm_mst_tuser c on c.user_gid = b.user_gid " +                     
                     " where a.customer_urn='" + customer_urn + "' and (renewal_flag ='Y' or enhancement_flag='Y') and a.process_type is null " +
                     " group by a.application_gid order by a.updated_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<cadapplicationlist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getapplicationadd_list.Add(new cadapplicationlist
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        ccapproved_date = dt["ccapproved_date"].ToString(),
                        creditgroup_name = dt["creditgroup_name"].ToString(),                       
                        cadaccepted_by = dt["cadaccepted_by"].ToString(),
                        cadaccepted_date = dt["cadaccepted_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        vertical_name = dt["vertical_name"].ToString(),
                        region = dt["region"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        relationshipmanager_name = dt["relationshipmanager_name"].ToString(),
                        overalllimit_amount = dt["overalllimit_amount"].ToString(),
                        renewal_flag = dt["renewal_flag"].ToString(),
                        enhancement_flag = dt["enhancement_flag"].ToString()
                    });

                }
            }
            values.cadapplicationlist = getapplicationadd_list;
            dt_datatable.Dispose();
        }


        public bool DaSanctionDocumentUpload(HttpRequest httpRequest, MdlMstCAD objfilename, string employee_gid)
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
            string lsapplication2sanction_gid = httpRequest.Form["application2sanction_gid"].ToString();
            string path, lspath;
            string project_flag = httpRequest.Form["project_flag"].ToString();

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/SanctionDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
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
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Master/SanctionDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "Master/SanctionDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        msGetGid = objcmnfunctions.GetMasterGID("SDOC");
                        msSQL = " insert into ocs_trn_tapplication2sanctiondoc( " +
                                    " application2sanctiondoc_gid," +
                                    " application2sanction_gid," +
                                    " application_gid," +
                                    " document_title," +
                                    " document_name," +
                                    " document_path," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + msGetGid + "'," +                                 
                                    "'" + lsapplication2sanction_gid + "'," +
                                    "'" + lsapplication_gid + "'," +
                                    "'" + lsdocument_title.Replace("'", "") + "'," +
                                    "'" + httpPostedFile.FileName.Replace("'", "") + "'," +
                                    "'" + lspath + msdocument_gid + FileExtension.Replace("'", "") + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult == 1)
                        {
                            msSQL = " update ocs_trn_tapplication2sanction set " +
                                    " sanction_flag='Y'," +
                                    " sanctiondocupdated_by='" + employee_gid + "'," +
                                      " sanctiondocupdated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                                      " where application2sanction_gid ='" + lsapplication2sanction_gid + "' ";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            objfilename.status = true;
                            objfilename.message = "Document Uploaded Successfully..!";
                        }
                        else
                        {
                            objfilename.status = false;
                            objfilename.message = "Error Occured..!";
                        }

                        msSQL = " select application2sanctiondoc_gid,application_gid,document_name,document_path,document_title from " +
                            " ocs_trn_tapplication2sanctiondoc where application_gid='" + lsapplication_gid + "'";
                        dt_datatable = objdbconn.GetDataTable(msSQL);
                        var getsanctiondocument_list = new List<sanctiondocument_list>();
                        if (dt_datatable.Rows.Count != 0)
                        {
                            foreach (DataRow dt in dt_datatable.Rows)
                            {
                                getsanctiondocument_list.Add(new sanctiondocument_list
                                {
                                    document_name = dt["document_name"].ToString(),
                                    document_title = dt["document_title"].ToString(),
                                    document_path =  objcmnstorage.EncryptData((dt["document_path"].ToString())), 
                                    application_gid = dt["application_gid"].ToString(),
                                    application2sanctiondoc_gid = dt["application2sanctiondoc_gid"].ToString(),

                                });
                                objfilename.sanctiondocument_list = getsanctiondocument_list;
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
        public void DaSanctionDocDelete(string application2sanctiondoc_gid, string application_gid, MdlMstCAD values)
        {
            msSQL = "delete from ocs_trn_tapplication2sanctiondoc where application2sanctiondoc_gid='" + application2sanctiondoc_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = " select application2sanctiondoc_gid,application_gid,document_name,document_path,document_title from " +
                           " ocs_trn_tapplication2sanctiondoc where application_gid='" + application_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getsanctiondocument_list = new List<sanctiondocument_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getsanctiondocument_list.Add(new sanctiondocument_list
                        {
                            document_name = dt["document_name"].ToString(),
                            document_title = dt["document_title"].ToString(),
                            document_path = objcmnstorage.EncryptData((dt["document_path"].ToString())),
                            application_gid = dt["application_gid"].ToString(),
                            application2sanctiondoc_gid = dt["application2sanctiondoc_gid"].ToString(),

                        });
                        values.sanctiondocument_list = getsanctiondocument_list;
                    }
                }
                dt_datatable.Dispose();
                values.message = "Sanction Document Deleted successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occrued While Deleting Document";
                values.status = false;
            }
        }

        public void DaGetSanction(string application_gid, MdlMstCAD values)
        {
            msSQL = " select application2sanctiondoc_gid,application_gid,document_name,document_path,document_title from " +
                       " ocs_trn_tapplication2sanctiondoc where application_gid='" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getsanctiondocument_list = new List<sanctiondocument_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getsanctiondocument_list.Add(new sanctiondocument_list
                    {
                        document_name = dt["document_name"].ToString(),
                        document_title = dt["document_title"].ToString(),
                        document_path = objcmnstorage.EncryptData((dt["document_path"].ToString())),
                        application_gid = dt["application_gid"].ToString(),
                        application2sanctiondoc_gid = dt["application2sanctiondoc_gid"].ToString(),

                    });
                    values.sanctiondocument_list = getsanctiondocument_list;
                }
            }
            dt_datatable.Dispose();
        }


        public bool DaURNTag(string employee_gid, customerurntag values)
        {
            msSQL = "select group_concat(application_gid) from ocs_trn_tcadapplication where customer_urn = '" + values.currentcustomer_urn + "'";
            string application_list = objdbconn.GetExecuteScalar(msSQL);
            string[] application_array = application_list.Split(',');

            
            if (values.tag_type == "NPA")
            {
                if (values.customer2tag_gid == null || values.customer2tag_gid == "") {
                    msGetGid1 = objcmnfunctions.GetMasterGID("UTMS");
                    msSQL = " insert into ocs_trn_tcustomer2tag(" +
                            " customer2tag_gid," +
                            " application_gid," +
                            " npatag_type," +
                            " npatag_flag," +
                            " urn_number," +
                            " nparemarks," +
                            " npacreated_by," +
                            " npacreated_date)" +
                            " values(" +
                            "'" + msGetGid1 + "'," +
                            "'" + application_list + "'," +
                            "'" + values.tag_type + "'," +
                            "'Y'," +
                            "'" + values.currentcustomer_urn + "'," +
                            "'" + values.tag_remarks.Replace("'", "") + "'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                else
                {
                    msGetGid1 = values.customer2tag_gid;
                    msSQL = " update ocs_trn_tcustomer2tag set " +
                     " nparemarks='" + values.tag_remarks.Replace("'", "") + "'," +
                     " npatag_type = '" + values.tag_type + "'," +
                     " npatag_flag='Y'," +
                     " npaupdated_by='" + employee_gid + "'," +
                     " npaupdated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                     " where customer2tag_gid='" + values.customer2tag_gid + "' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            if (values.tag_type == "Legal")
            {
                if (values.customer2tag_gid == null || values.customer2tag_gid == "")
                {
                    msGetGid1 = objcmnfunctions.GetMasterGID("UTMS");
                    msSQL = " insert into ocs_trn_tcustomer2tag(" +
                " customer2tag_gid," +
                " application_gid," +
                " legaltag_type," +
                " legaltag_flag," +
                " urn_number," +
                " legalremarks," +
                " legalcreated_by," +
                " legalcreated_date)" +
                " values(" +
                "'" + msGetGid1 + "'," +
                "'" + application_list + "'," +
                "'" + values.tag_type + "'," +
                "'Y'," +
                "'" + values.currentcustomer_urn + "'," +
                "'" + values.tag_remarks.Replace("'", "") + "'," +
                "'" + employee_gid + "'," +
                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else
            {
                msGetGid1 = values.customer2tag_gid;
                msSQL = " update ocs_trn_tcustomer2tag set " +
                 " legalremarks='" + values.tag_remarks.Replace("'", "") + "'," +
                 " legaltag_type = '" + values.tag_type + "'," +
                 " legaltag_flag='Y'," +
                 " legalupdated_by='" + employee_gid + "'," +
                 " legalupdated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where customer2tag_gid='" + values.customer2tag_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            }

            for (int i = 0; i < application_array.Length; i++) { 
                msGetGid = objcmnfunctions.GetMasterGID("UTHS");

                msSQL = " insert into ocs_trn_tcustomer2taghistory(" +
                    " customer2taghistory_gid," +
                    " customer2tag_gid," +
                    " application_gid," +
                    " tag_type," +
                    " customer_name," +
                    " customer_urn," +
                    " customer_status," +
                    " remarks," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + msGetGid1 + "'," +
                    "'" + application_array[i] + "'," +
                    "'" + values.tag_type+ "'," +
                    "'" + values.customer_name.Replace("'", "") + "'," +
                    "'" + values.currentcustomer_urn + "'," +
                    "'Tagged'," +
                    "'" + values.tag_remarks.Replace("'", "") + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Tagged to "+ values.tag_type + " Successfully..!";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
                return false;
            }
        }

        public void DaGetCADUrnGroupingLegalTagSummary(string employee_gid, MdlMstCAD values)
        {
            msSQL = " select d.customer2tag_gid,a.customerref_name,a.customer_urn, " +
                    " a.customer_name as customer_name, a.vertical_name,a.region,concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as tagged_by," +
                    " date_format(d.legalcreated_date, '%d-%m-%Y %h:%i %p') as tagged_date,d.legalremarks as remarks, " +
                    " concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as updated_by," +
                    " date_format(d.npaupdated_date, '%d-%m-%Y %h:%i %p') as updated_date,a.renewal_flag,a.enhancement_flag " +
                    " from ocs_trn_tcadapplication a " +
                    " left join ocs_trn_tcustomer2tag d on d.urn_number = a.customer_urn " +
                    " left join hrm_mst_temployee b on b.employee_gid = d.legalcreated_by " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " left join hrm_mst_temployee f on f.employee_gid = d.npaupdated_by " +
                    " left join adm_mst_tuser e on e.user_gid = f.user_gid " +
                    " where a.process_type = 'Accept' and(a.processupdated_date = (select max(e.processupdated_date) from ocs_trn_tcadapplication e where e.customer_urn = a.customer_urn and a.customer_urn <> '')) " +
                    " and d.legaltag_flag = 'Y' " +
                    " order by d.legalcreated_date desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<cadapplicationlist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getapplicationadd_list.Add(new cadapplicationlist
                    {
                        customer_name = dt["customer_name"].ToString(),
                        tagged_by = dt["tagged_by"].ToString(),
                        tagged_date = dt["tagged_date"].ToString(),
                        updated_by = dt["updated_by"].ToString(),
                        updated_date = dt["updated_date"].ToString(),
                        vertical_name = dt["vertical_name"].ToString(),
                        region = dt["region"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        remarks = dt["remarks"].ToString(),
                        customer2tag_gid = dt["customer2tag_gid"].ToString(),
                        renewal_flag = dt["renewal_flag"].ToString(),
                        enhancement_flag = dt["enhancement_flag"].ToString()
                    });

                }
                values.cadapplicationlist = getapplicationadd_list;
                values.status = true;
            }
            else
            {
                values.status = false;
            }

            dt_datatable.Dispose();
        }

        public void DaGetCADUrnGroupingNPATagSummary(string employee_gid, MdlMstCAD values)
        {
            msSQL = " select d.customer2tag_gid,a.customerref_name,a.customer_urn, " +
                    " a.customer_name as customer_name, a.vertical_name,a.region,concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as tagged_by," +
                    " date_format(d.npacreated_date, '%d-%m-%Y %h:%i %p') as tagged_date,d.nparemarks as remarks, " +
                    " concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as updated_by," +
                    " date_format(d.npaupdated_date, '%d-%m-%Y %h:%i %p') as updated_date,a.renewal_flag,a.enhancement_flag " +
                    " from ocs_trn_tcadapplication a " +
                    " left join ocs_trn_tcustomer2tag d on d.urn_number = a.customer_urn " +
                    " left join hrm_mst_temployee b on b.employee_gid = d.npacreated_by " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " left join hrm_mst_temployee f on f.employee_gid = d.npaupdated_by " +
                    " left join adm_mst_tuser e on e.user_gid = f.user_gid " +
                    " where a.process_type = 'Accept' and(a.processupdated_date = (select max(e.processupdated_date) from ocs_trn_tcadapplication e where e.customer_urn = a.customer_urn and a.customer_urn <> '')) " +
                    " and d.npatag_flag = 'Y' " +
                    " order by d.npacreated_date desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<cadapplicationlist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getapplicationadd_list.Add(new cadapplicationlist
                    {
                        customer_name = dt["customer_name"].ToString(),
                        tagged_by = dt["tagged_by"].ToString(),
                        tagged_date = dt["tagged_date"].ToString(),
                        updated_by = dt["updated_by"].ToString(),
                        updated_date = dt["updated_date"].ToString(),
                        vertical_name = dt["vertical_name"].ToString(),
                        region = dt["region"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        remarks = dt["remarks"].ToString(),
                        customer2tag_gid = dt["customer2tag_gid"].ToString(),
                        renewal_flag = dt["renewal_flag"].ToString(),
                        enhancement_flag = dt["enhancement_flag"].ToString()
                    });

                }
                values.cadapplicationlist = getapplicationadd_list;
                values.status = true;
            }
            else
            {
                values.status = false;
            }

            dt_datatable.Dispose();
        }

        public bool DaURNUntag(string employee_gid, customerurntag values)
        {
            msSQL = "select application_gid from ocs_trn_tcustomer2tag where customer2tag_gid = '" + values.customer2tag_gid + "'";
            string application_list = objdbconn.GetExecuteScalar(msSQL);
            string[] application_array = application_list.Split(',');
            msSQL = "select distinct customer_name from ocs_trn_tcustomer2taghistory where customer2tag_gid = '" + values.customer2tag_gid + "'";
            string customer_name = objdbconn.GetExecuteScalar(msSQL);
            msSQL = "select distinct customer_urn from ocs_trn_tcustomer2taghistory where customer2tag_gid = '" + values.customer2tag_gid + "'";
            string currentcustomer_urn = objdbconn.GetExecuteScalar(msSQL);

            if (values.tag_type == "NPA")
            {
                msSQL = " update ocs_trn_tcustomer2tag set " +
                     " nparemarks='" + values.tag_remarks.Replace("'", "") + "'," +
                     " npatag_flag='N'," +
                     " npaupdated_by='" + employee_gid + "'," +
                     " npaupdated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                     " where customer2tag_gid='" + values.customer2tag_gid + "' ";


                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            if (values.tag_type == "Legal")
            {
                msSQL = " update ocs_trn_tcustomer2tag set " +
                     " legalremarks='" + values.tag_remarks.Replace("'", "") + "'," +
                     " legaltag_flag='N'," +
                     " legalupdated_by='" + employee_gid + "'," +
                     " legalupdated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                     " where customer2tag_gid='" + values.customer2tag_gid + "' ";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            for (int i = 0; i < application_array.Length; i++)
            {
                msGetGid1 = objcmnfunctions.GetMasterGID("UTHS");

                msSQL = " insert into ocs_trn_tcustomer2taghistory(" +
                    " customer2taghistory_gid," +
                    " customer2tag_gid," +
                    " application_gid," +
                    " tag_type," +
                    " customer_name," +
                    " customer_urn," +
                    " customer_status," +
                    " remarks," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid1 + "'," +
                    "'" + values.customer2tag_gid + "'," +
                    "'" + application_array[i] + "'," +
                    "'" + values.tag_type + "'," +
                    "'" + customer_name.Replace("'", "") + "'," +
                    "'" + currentcustomer_urn + "'," +
                    "'Untagged'," +
                    "'" + values.tag_remarks.Replace("'", "") + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Untagged from " + values.tag_type + " Successfully..!";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
                return false;
            }
        }
        public void DaURNLegaltagHistory(string customer2tag_gid, customerurntag values)
        {
            
            msSQL = " select distinct customer2tag_gid,tag_type,a.remarks,concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) " +
                    " as created_by,date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date, customer_status as tag_status" +
                    " from ocs_trn_tcustomer2taghistory a " +
                    " left join hrm_mst_temployee b on b.employee_gid = a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " where tag_type = 'Legal' and customer2tag_gid = '" + customer2tag_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcustomerurntag_list = new List<customerurntag_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getcustomerurntag_list.Add(new customerurntag_list
                    {
                        customer2tag_gid = dt["customer2tag_gid"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        tag_type = dt["tag_type"].ToString(),
                        remarks = dt["remarks"].ToString(),
                        tag_status = dt["tag_status"].ToString()
                    });

                }
                values.customerurntag_list = getcustomerurntag_list;
            }
            else
            {
                values.status = false;
            }

            dt_datatable.Dispose();
        }
        public void DaURNNPAtagHistory(string customer2tag_gid, customerurntag values)
        {
            msSQL = " select distinct customer2tag_gid,tag_type,a.remarks,concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) " +
                    " as created_by,date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date, customer_status as tag_status " +
                    " from ocs_trn_tcustomer2taghistory a " +
                    " left join hrm_mst_temployee b on b.employee_gid = a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " where tag_type = 'NPA' and customer2tag_gid = '" + customer2tag_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcustomerurntag_list = new List<customerurntag_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getcustomerurntag_list.Add(new customerurntag_list
                    {
                        customer2tag_gid = dt["customer2tag_gid"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        tag_type = dt["tag_type"].ToString(),
                        remarks = dt["remarks"].ToString(),
                        tag_status = dt["tag_status"].ToString(),
                    });

                }
                values.customerurntag_list = getcustomerurntag_list;
                values.status = true;
            }
            else
            {
                values.status = false;
            }

            dt_datatable.Dispose();
        }

        public void DaURNTagCount(string customer2tag_gid, customerurntag values)
        {
            msSQL = " select count(d.customer2tag_gid) from ocs_trn_tcadapplication a " +
                    " left join ocs_trn_tcustomer2tag d on d.urn_number = a.customer_urn " +
                    " where a.process_type = 'Accept' and(a.processupdated_date = (select max(e.processupdated_date) from ocs_trn_tcadapplication e where e.customer_urn = a.customer_urn and a.customer_urn <> '')) " +
                    " and d.npatag_flag = 'Y' ";
            string taggednpa_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select count(d.customer2tag_gid)  from ocs_trn_tcadapplication a " + 
                    " left join ocs_trn_tcustomer2tag d on d.urn_number = a.customer_urn " +
                    " where a.process_type = 'Accept' and(a.processupdated_date = (select max(e.processupdated_date) from ocs_trn_tcadapplication e where e.customer_urn = a.customer_urn and a.customer_urn <> '')) " +
                    " and d.legaltag_flag = 'Y' ";
            string taggedlegal_count = objdbconn.GetExecuteScalar(msSQL);

            values.taggednpa_count = taggednpa_count; 
            values.taggedlegal_count = taggedlegal_count; 
            values.status = true;
        }

        public void DaSanctionPopup(string application_gid, appsanction_list values)
        {

            msSQL = " select a.application_gid, a.application2sanction_gid, a.sanction_refno, accepted_status,accepted_reason" +
                    " from ocs_trn_tapplication2sanction a " +
                    " left join ocs_trn_tcadapplication b on b.application_gid = a.application_gid" +
                    " where a.application_gid='" + application_gid + "'  ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {

                values.application2sanction_gid = objODBCDatareader["application2sanction_gid"].ToString();
                values.application_gid = objODBCDatareader["application_gid"].ToString();
                values.sanction_refno = objODBCDatareader["sanction_refno"].ToString();
                values.accepted_status = objODBCDatareader["accepted_status"].ToString();
                values.accepted_reason = objODBCDatareader["accepted_reason"].ToString();
               

            }
            objODBCDatareader.Close();

        }


        public void DaSanctionAccepte(appsanction_list values, string employee_gid)
        {
            msSQL = " select application2sanction_gid from ocs_trn_tapplication2sanction a " +
                   " where application2sanction_gid = '" + values.application2sanction_gid + "' and accepted_status = 'Y'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == false)
            {
                values.status = true;
            }
            else
            {
                values.message = "The Sanction Customer has been Accepted";
                values.status = false;
                return;
            }
            objODBCDatareader.Close();

            msSQL = " select application2sanction_gid from ocs_trn_tapplication2sanction a " +
                " where application2sanction_gid = '" + values.application2sanction_gid + "' and submitedtoapproval_status = 'N'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == false)
            {
                values.status = true;
            }
            else
            {
                values.message = " Sanction Details is not Re-submitted";
                values.status = false;
                return;
            }
            objODBCDatareader.Close();




            msSQL = " update ocs_trn_tapplication2sanction set accepted_status ='" + values.rbo_status + "',";
                    if (values.rbo_status == 'N')
                    {
                        msSQL += " submitedtoapproval_status='N',";
                        msSQL += " resubmitenable_flag='N',";
                        msSQL += " sanctionletter_flag='N',";
                    }
                    else
                    {
                      msSQL += " sanctionletter_flag='Y',";
                    }
                    if (values.remarks == null || values.remarks == "")
                    {
                        msSQL += " accepted_reason=null";
                    }
                    else
                    {
                        msSQL += " accepted_reason='" + values.remarks.Replace("'", "") + "'" ;

                    }          
                    msSQL += " where application2sanction_gid='" + values.application2sanction_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("SALG");

                msSQL = " insert into ocs_trn_tsanctionacceptlog (" +
                      " sanctionacceptlog_gid  , " +
                      " application2sanction_gid," +
                      " application_gid," +
                      " sanction_refno," +
                      " accepted_status," +
                      " accepted_reason," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                       " '" + values.application2sanction_gid + "'," +
                      " '" + values.application_gid + "'," +
                      " '" + values.sanction_refno + "'," +
                      " '" + values.rbo_status + "',";
                     if (values.remarks == null || values.remarks == "")
                     {
                      msSQL += "null,";
                     }
                     else
                     {
                      msSQL += " '" + values.remarks.Replace("'", "") + "',";
                     }         
                      msSQL += " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "Sanction Not Accepted Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Sanction Accepted Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }

        }



        public void DaSanctionAcceptedLog(string application_gid, MdlMstCAD values)
        {
            try
            {
                msSQL = " SELECT application_gid,date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                        " case when a.accepted_status='N' then 'Not Accepted' else 'Accepted' end as Status, a.accepted_reason" +
                        " FROM ocs_trn_tsanctionacceptlog a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where application_gid ='" + application_gid + "' order by a.sanctionacceptlog_gid   desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getappsanction_list = new List<appsanction_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getappsanction_list.Add(new appsanction_list
                        {
                            application_gid = (dr_datarow["application_gid"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            accepted_status = (dr_datarow["Status"].ToString()),
                            remarks = (dr_datarow["accepted_reason"].ToString()),
                        });
                    }
                    values.appsanction_list = getappsanction_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public bool DaSanctionAcceptedSummary(sanctiondetailsList values, string employee_gid)
        {
            //msSQL = " SELECT a.application2sanction_gid,a.sanction_refno,date_format(a.sanction_date,'%d-%m-%Y') as sanction_date, sanction_status, b.application_gid, " +
            //       " ccapproved_date, format((sanction_amount),2) as sanction_amount,b.customer_name,checkerapproval_flag, a.checkerreject_remarks, b.approval_status," +
            //       " b.application_no, concat(d.user_code,' / ',d.user_firstname,d.user_lastname) as checkerupdated_by, " +
            //       " date_format(a.checkerupdated_on,'%d-%m-%Y %h:%i %p') as checkerupdated_on, sanctionto_name, e.cadgroup_name," +
            //       " date_format(e.approver_approveddate,'%d-%m-%Y %h:%i %p') as approver_approveddate, " +
            //       " a.sanction_refno,b.customer_urn FROM ocs_trn_tapplication2sanction a " +
            //       " LEFT JOIN ocs_trn_tcadapplication b ON a.application_gid = b.application_gid" +
            //       " LEFT JOIN hrm_mst_temployee c ON a.checkerupdated_by=c.employee_gid" +
            //       " LEFT JOIN adm_mst_tuser d ON c.user_gid=d.user_gid " +
            //       " left join ocs_trn_tprocesstype_assign e on e.application_gid = a.application_gid " +
            //       " left join ocs_trn_tsanctionacceptlog f on f.application_gid = a.application_gid " +
            //       " where e.menu_gid = 'CADMGTSAN'  " +
            //       " and e.approver_approvalflag='Y' and  f.accepted_status ='Y' and  a.accepted_status ='Y'  " +
            //       " and( f.updated_date = (select max(updated_date) from ocs_trn_tsanctionacceptlog y where y.application_gid = a.application_gid ) )" +
            //       " ORDER BY application2sanction_gid DESC ";
            msSQL = "call ocs_trn_spsanctionacceptedsummary";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_sanctiondtl = new List<sanctiondetails>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    string lsccgroup_name;
                    string lscadgroup_name;

                    //msSQL = "select group_concat(ccgroup_name) as ccgroup_name from ocs_mst_tapplication where application_gid='" + dt["application_gid"] + "'";
                    //objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    //if (objODBCDataReader.HasRows == true)
                    //{
                    //    lsccgroup_name = objODBCDataReader["ccgroup_name"].ToString();
                    //}
                    //else
                    //{
                    //    lsccgroup_name = "";
                    //}
                    //objODBCDataReader.Close();
                    get_sanctiondtl.Add(new sanctiondetails
                    {
                        application_gid = dt["application_gid"].ToString(),
                        application2sanction_gid = dt["application2sanction_gid"].ToString(),
                        sanction_refno = dt["sanction_refno"].ToString(),
                        sanction_date = dt["sanction_date"].ToString(),
                        sanction_amount = dt["sanction_amount"].ToString(),
                        customername = dt["customer_name"].ToString(),
                        created_by = dt["checkerupdated_by"].ToString(),
                        created_date = dt["checkerupdated_on"].ToString(),
                        checkerapproval_flag = dt["checkerapproval_flag"].ToString(),
                        checkerreject_remarks = dt["checkerreject_remarks"].ToString(),
                        sanction_status = dt["sanction_status"].ToString(),
                        //ccgroup_name = lsccgroup_name,
                        ccgroup_name = dt["ccgroup_name"].ToString(),
                        cadgroupname = dt["cadgroup_name"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        application_no = dt["application_no"].ToString(),
                        ccapproved_date = dt["ccapproved_date"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        approver_approveddate = dt["approver_approveddate"].ToString()
                    });
                }
                values.sanctiondetails = get_sanctiondtl;
            }
            dt_datatable.Dispose();

            return true;
        }

        public bool DaSanctionNotAcceptedSummary(sanctiondetailsList values, string employee_gid)
        {
            msSQL = " SELECT a.application2sanction_gid,a.sanction_refno,date_format(a.sanction_date,'%d-%m-%Y') as sanction_date, sanction_status, b.application_gid, a.submitedtoapproval_status," +
                   " ccapproved_date, format((sanction_amount),2) as sanction_amount,b.customer_name,checkerapproval_flag, a.checkerreject_remarks, b.approval_status," +
                   " b.application_no, concat(d.user_code,' / ',d.user_firstname,d.user_lastname) as checkerupdated_by, " +
                   " date_format(a.checkerupdated_on,'%d-%m-%Y %h:%i %p') as checkerupdated_on, sanctionto_name, e.cadgroup_name," +
                   " date_format(e.approver_approveddate,'%d-%m-%Y %h:%i %p') as approver_approveddate, " +
                   " a.sanction_refno,b.customer_urn,b.renewal_flag,b.enhancement_flag FROM ocs_trn_tapplication2sanction a " +
                   " LEFT JOIN ocs_trn_tcadapplication b ON a.application_gid = b.application_gid" +
                   " LEFT JOIN hrm_mst_temployee c ON a.checkerupdated_by=c.employee_gid" +
                   " LEFT JOIN adm_mst_tuser d ON c.user_gid=d.user_gid " +
                   " left join ocs_trn_tprocesstype_assign e on e.application_gid = a.application_gid " +
                   " left join ocs_trn_tsanctionacceptlog f on f.application_gid = a.application_gid " +
                   " where e.menu_gid = 'CADMGTSAN' and (e.maker_gid ='" + employee_gid + "' or e.checker_gid='" + employee_gid + "' or e.approver_gid = '" + employee_gid + "') " +
                   " and e.approver_approvalflag='Y'  and f.accepted_status ='N' " +
                   " and( f.updated_date = (select max(updated_date) from ocs_trn_tsanctionacceptlog y where y.application_gid = a.application_gid ) )" +                   
                   " ORDER BY application2sanction_gid DESC ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_sanctiondtl = new List<sanctiondetails>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    //string lsccgroup_name;
                    //string lscadgroup_name;

                    //msSQL = "select group_concat(ccgroup_name) as ccgroup_name from ocs_mst_tapplication where application_gid='" + dt["application_gid"] + "'";
                    //objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    //if (objODBCDataReader.HasRows == true)
                    //{
                    //    lsccgroup_name = objODBCDataReader["ccgroup_name"].ToString();
                    //}
                    //else
                    //{
                    //    lsccgroup_name = "";
                    //}
                    //objODBCDataReader.Close();
                    get_sanctiondtl.Add(new sanctiondetails
                    {
                        application_gid = dt["application_gid"].ToString(),
                        application2sanction_gid = dt["application2sanction_gid"].ToString(),
                        sanction_refno = dt["sanction_refno"].ToString(),
                        sanction_date = dt["sanction_date"].ToString(),
                        sanction_amount = dt["sanction_amount"].ToString(),
                        customername = dt["customer_name"].ToString(),
                        created_by = dt["checkerupdated_by"].ToString(),
                        created_date = dt["checkerupdated_on"].ToString(),
                        checkerapproval_flag = dt["checkerapproval_flag"].ToString(),
                        checkerreject_remarks = dt["checkerreject_remarks"].ToString(),
                        sanction_status = dt["sanction_status"].ToString(),
                        //ccgroup_name = lsccgroup_name,
                        cadgroupname = dt["cadgroup_name"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        application_no = dt["application_no"].ToString(),
                        ccapproved_date = dt["ccapproved_date"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        approver_approveddate = dt["approver_approveddate"].ToString(),
                        renewal_flag = dt["renewal_flag"].ToString(),
                        enhancement_flag = dt["enhancement_flag"].ToString(),
                        submitedtoapproval_status = dt["submitedtoapproval_status"].ToString()
                    });
                }
                values.sanctiondetails = get_sanctiondtl;
            }
            dt_datatable.Dispose();

            return true;
        }

        public void DaSanctionHistory(string application2sanction_gid, MdlMstCAD values)
        {
            try
            {
                msSQL = " select sanctionsubmittoapprovallog_gid,application2sanction_gid,application2sanctionlog_gid,a.application_gid, sanction_refno,b.application_no, date_format(a.sanction_date,'%d-%m-%Y') as sanction_date,  b.application_gid, " +
                         " b.customer_name, concat(d.user_firstname,d.user_lastname,' / ',d.user_code) as created_by,a.created_date,b.customer_urn " +
                         " from ocs_trn_tsanctionsubmittoapprovallog a" +
                         " LEFT JOIN ocs_trn_tcadapplication b ON a.application_gid = b.application_gid" +
                         " LEFT JOIN hrm_mst_temployee c ON a.created_by = c.employee_gid" +
                         " LEFT JOIN adm_mst_tuser d ON c.user_gid=d.user_gid " +
                         " WHERE application2sanction_gid ='" + application2sanction_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getappsanction_list = new List<appsanction_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getappsanction_list.Add(new appsanction_list
                        {
                            sanction_refno = (dr_datarow["sanction_refno"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            sanction_date = (dr_datarow["sanction_date"].ToString()),
                            application_no = (dr_datarow["application_no"].ToString()),
                            customername = (dr_datarow["customer_name"].ToString()),
                            application2sanction_gid = (dr_datarow["application2sanction_gid"].ToString()),
                            application2sanctionlog_gid = (dr_datarow["application2sanctionlog_gid"].ToString()),
                            application_gid = (dr_datarow["application_gid"].ToString()),
                            customer_urn = (dr_datarow["customer_urn"].ToString()),
                            sanctionsubmittoapprovallog_gid = (dr_datarow["sanctionsubmittoapprovallog_gid"].ToString()),
                        });
                    }
                    values.appsanction_list = getappsanction_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaCADSanctionDtlslog(string sanctionsubmittoapprovallog_gid, reportcadsanctiondetails values)
        {
            try
            {
                msSQL = " SELECT a.application2sanction_gid,b.application_gid,b.application_no,a.sanction_refno," +
                "  format((c.sanction_amount), 2, 'en_IN') as sanction_amount,a.sanctionto_name, date_format(a.sanction_date,'%d-%m-%Y') as sanction_date," +
                "  a.application_type,a.contactperson_address,a.contactperson_name,a.contactperson_number,a.contactpersonemail_address,date_format(a.sanctionfrom_date, '%d-%m-%Y') as sanctionfrom_date," +
                "  date_format(a.sanctiontill_date, '%d-%m-%Y') as sanctiontill_date,a.paycard,a.sanction_type,a.natureof_proposal,a.branch_name,a.esdeclaration_status," +
                 " a.makerfile_path,a.makerfile_name FROM ocs_trn_tsanctionsubmittoapprovallog a " +
                 " LEFT JOIN ocs_trn_tcadapplication b ON a.application_gid = b.application_gid " +
                 " LEFT JOIN ocs_trn_tapplication2sanction c ON a.application_gid = c.application_gid " +
                 " WHERE sanctionsubmittoapprovallog_gid ='" + sanctionsubmittoapprovallog_gid + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows)
                {
                    values.sanction_refno = objODBCDataReader["sanction_refno"].ToString();
                    values.application2sanction_gid = objODBCDataReader["application2sanction_gid"].ToString();
                    values.sanction_amount = objODBCDataReader["sanction_amount"].ToString();
                    values.sanction_date = objODBCDataReader["sanction_date"].ToString();
                    values.sanctionto_name = objODBCDataReader["sanctionto_name"].ToString();

                    //if (objODBCDataReader["sanction_date"].ToString() != "")
                    //{
                    //    values.sanctionDate = Convert.ToDateTime(objODBCDataReader["sanction_date"].ToString());
                    //}
                    values.application_no = objODBCDataReader["application_no"].ToString();

                    values.application_type = objODBCDataReader["application_type"].ToString();
                    values.contactperson_address = objODBCDataReader["contactperson_address"].ToString();
                    values.contactperson_name = objODBCDataReader["contactperson_name"].ToString();
                    values.contactperson_number = objODBCDataReader["contactperson_number"].ToString();
                    values.contactpersonemail_address = objODBCDataReader["contactpersonemail_address"].ToString();
                    values.sanctionfrom_date = objODBCDataReader["sanctionfrom_date"].ToString();
                    values.sanctiontill_date = objODBCDataReader["sanctiontill_date"].ToString();
                    values.paycard = objODBCDataReader["paycard"].ToString();
                    values.sanction_type = objODBCDataReader["sanction_type"].ToString();
                    values.natureof_proposal = objODBCDataReader["natureof_proposal"].ToString();
                    values.branch_name = objODBCDataReader["branch_name"].ToString();
                    values.esdeclaration_status = objODBCDataReader["esdeclaration_status"].ToString();


                }
                objODBCDataReader.Close();

            }
            catch (Exception ex)
            {
                values.message = ex.ToString();
                values.status = false;
            }

        }


        public bool DaGetTemplateDetailslog(reportmdltemplate values, string sanctionsubmittoapprovallog_gid)
        {
            msSQL = " select sanctionletter_status, template_name, template_content, makerfile_name, makerfile_path, sanctionletter_flag, checkerapproval_flag," +
                    " checkerletter_flag, checkerpushback_remarks, digitalsignature_flag, date_format(checkerupdated_on, '%d-%m-%Y') as checkerupdated_on," +
                    " concat(c.user_firstname, ' ', c.user_lastname, '/', c.user_code) as checkerupdated_by, date_format(makersubmitted_on, '%d-%m-%Y') as makersubmitted_on," +
                    " concat(e.user_firstname, ' ', e.user_lastname, '/', e.user_code) as makersubmitted_by, " +
                    " f.approver_name as approved_by,date_format(f.approver_approveddate, '%d-%m-%Y') as approved_date " +
                    " from ocs_trn_tsanctionsubmittoapprovallog a " +
                    " left join hrm_mst_temployee b on b.employee_gid = a.checkerupdated_by " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " left join hrm_mst_temployee d on d.employee_gid = a.makersubmitted_by " +
                    " left join adm_mst_tuser e on e.user_gid = d.user_gid " +
                    " left join ocs_trn_tprocesstype_assign f on f.application_gid = a.application_gid " +
                    " where sanctionsubmittoapprovallog_gid='" + sanctionsubmittoapprovallog_gid + "' and f.menu_gid ='CADMGTSAN'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {

                values.sanctionletter_status = objODBCDataReader["sanctionletter_status"].ToString();
                values.template_name = objODBCDataReader["template_name"].ToString();
                values.template_content = objODBCDataReader["template_content"].ToString();
                values.makerfile_name = objODBCDataReader["makerfile_name"].ToString();
                values.makerfile_path = objcmnstorage.EncryptData((objODBCDataReader["makerfile_path"].ToString()));
                values.sanctionletter_flag = objODBCDataReader["sanctionletter_flag"].ToString();
                values.checkerapproval_flag = objODBCDataReader["checkerapproval_flag"].ToString();
                values.checkerletter_flag = objODBCDataReader["checkerletter_flag"].ToString();
                values.checkerpushback_remarks = objODBCDataReader["checkerpushback_remarks"].ToString();
                values.digitalsignature_flag = objODBCDataReader["digitalsignature_flag"].ToString();
                values.checkerupdated_by = objODBCDataReader["checkerupdated_by"].ToString();
                values.checkerupdated_on = objODBCDataReader["checkerupdated_on"].ToString();
                values.makersubmitted_by = objODBCDataReader["makersubmitted_by"].ToString();
                values.makersubmitted_on = objODBCDataReader["makersubmitted_on"].ToString();
                values.approved_by = objODBCDataReader["approved_by"].ToString();
                values.approved_date = objODBCDataReader["approved_date"].ToString();
            }
            objODBCDataReader.Close();

            values.status = true;
            return true;
        }

        public void DaGetesdocumentlog(ReportUploadCADDocumentname values, string sanctionsubmittoapprovallog_gid)
        {
            msSQL = " select esdeclaration_gid,document_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,document_path,document_type, " +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as uploaded_by" +
                    " from ocs_trn_tesdeclarationdocumentsubmitlog a,hrm_mst_temployee b, adm_mst_tuser c where a.created_by=b.employee_gid" +
                    " and b.user_gid = c.user_gid and ( sanctionsubmittoapprovallog_gid='" + sanctionsubmittoapprovallog_gid + "') and delete_flag='N'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_esdeclarationdocumentlist = new List<ReportUploadCADES_DocumentList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    get_esdeclarationdocumentlist.Add(new ReportUploadCADES_DocumentList
                    {
                        document_path = objcmnstorage.EncryptData((dr_datarow["document_path"].ToString())),
                        document_name = (dr_datarow["document_name"].ToString()),
                        document_gid = (dr_datarow["esdeclaration_gid"].ToString()),
                        document_type = dr_datarow["document_type"].ToString(),
                        uploaded_by = dr_datarow["uploaded_by"].ToString(),
                        updated_date = dr_datarow["uploaded_date"].ToString()
                    });
                }
                values.UploadCADES_DocumentList = get_esdeclarationdocumentlist;
            }
        }

        public void DaGetMaildocumentlog(ReportUploadCADDocumentname values, string sanctionsubmittoapprovallog_gid)
        {
            msSQL = " select maildocument_gid,document_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,document_path,document_type, " +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as uploaded_by" +
                    " from ocs_trn_tdeviationmaildocumentsubmitlog a,hrm_mst_temployee b, adm_mst_tuser c where a.created_by=b.employee_gid" +
                    " and b.user_gid = c.user_gid and ( sanctionsubmittoapprovallog_gid='" + sanctionsubmittoapprovallog_gid + "') and delete_flag='N'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_maildocumentlist = new List<ReportDeviationCADMail_DocumentList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    get_maildocumentlist.Add(new ReportDeviationCADMail_DocumentList
                    {
                        document_path = objcmnstorage.EncryptData((dr_datarow["document_path"].ToString())),
                        document_name = (dr_datarow["document_name"].ToString()),
                        document_gid = (dr_datarow["maildocument_gid"].ToString()),
                        document_type = dr_datarow["document_type"].ToString(),
                        uploaded_by = dr_datarow["uploaded_by"].ToString(),
                        updated_date = dr_datarow["uploaded_date"].ToString()
                    });
                }
                values.DeviationCADMail_DocumentList = get_maildocumentlist;
            }
        }

        public void DaSanctionSubmitToApproval(appsanction_list values, string employee_gid)
        {
            msSQL = " select application2sanction_gid from ocs_trn_tapplication2sanction a " +
                  " where application2sanction_gid = '" + values.application2sanction_gid + "' and resubmitenable_flag = 'N'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == false)
            {
                values.status = true;
            }
            else
            {
                values.message = "Kindly Update the Sanction Details";
                values.status = false;
                return;
            }
            objODBCDatareader.Close();
            msSQL = " update ocs_trn_tapplication2sanction set submitedtoapproval_status ='Y'" +       
                    " where application2sanction_gid='" + values.application2sanction_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                msSQL = " select application2sanctionlog_gid,application2sanction_gid, sanction_refno,  sanction_date, paycard, branch_gid,branch_name,  " +
                   "  application_gid ,  applicationtype_gid, application_type,esdeclaration_status, " +
                   " sanctionto_gid, sanctionto_name, sanctionfrom_date, sanctiontill_date, contactpersonaddress_gid, " +
                   " contactperson_address, contactperson_name, contactperson_number, contactpersonmobileno_gid, " +
                   " contactpersonemail_gid, contactpersonemail_address, sanction_type, natureof_proposal,created_by,created_date, " +
                   " makerfile_path,makerfile_name,sanctionletter_status,template_content,makersubmitted_by,makersubmitted_on," +
                   " template_name,checkerapproved_by,checkerupdated_on,checkerpushback_remarks,checkerapproval_flag," +
                   " checkerapproved_on,digitalsignature_flag," +
                   " sanctiongenerated_by,sanctiongenerated_on " +
                   " from ocs_trn_tapplication2sanctionlog " +
                   " where created_date = (select max(created_date) from ocs_trn_tapplication2sanctionlog " +
                   " where  application2sanction_gid ='" + values.application2sanction_gid + "')";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows)
                {
                    application2sanctionlog_gid = objODBCDataReader["application2sanctionlog_gid"].ToString();
                    template_name = objODBCDataReader["template_name"].ToString();
                    checkerapproved_by = objODBCDataReader["checkerapproved_by"].ToString();
                    checkerupdated_on = objODBCDataReader["checkerupdated_on"].ToString();
                    checkerpushback_remarks = objODBCDataReader["checkerpushback_remarks"].ToString();
                    checkerapproval_flag = objODBCDataReader["checkerapproval_flag"].ToString();
                    checkerapproved_on = objODBCDataReader["checkerapproved_on"].ToString();
                    digitalsignature_flag = objODBCDataReader["digitalsignature_flag"].ToString();
                    makerfile_path = objcmnstorage.EncryptData((objODBCDataReader["makerfile_path"].ToString()));
                    makerfile_name = objODBCDataReader["makerfile_name"].ToString();
                    sanctionletter_status = objODBCDataReader["sanctionletter_status"].ToString();
                    template_content = objODBCDataReader["template_content"].ToString();
                    makersubmitted_by = objODBCDataReader["makersubmitted_by"].ToString();
                    makersubmitted_on = objODBCDataReader["makersubmitted_on"].ToString();
                    sanctiongenerated_by = objODBCDataReader["sanctiongenerated_by"].ToString();
                    sanctiongenerated_on = objODBCDataReader["sanctiongenerated_on"].ToString();
                    application2sanction_gid = objODBCDataReader["application2sanction_gid"].ToString();
                    sanction_refno = objODBCDataReader["sanction_refno"].ToString();
                    application_gid = objODBCDataReader["application_gid"].ToString();
                    if (objODBCDataReader["sanction_date"].ToString() != "")
                    {
                        sanction_date = objODBCDataReader["sanction_date"].ToString();
                    }
                    applicationtype_gid = objODBCDataReader["applicationtype_gid"].ToString();
                    application_type = objODBCDataReader["application_type"].ToString();
                    sanctionto_gid = objODBCDataReader["sanctionto_gid"].ToString();
                    sanctionto_name = objODBCDataReader["sanctionto_name"].ToString();
                    contactpersonaddress_gid = objODBCDataReader["contactpersonaddress_gid"].ToString();
                    contactperson_address = objODBCDataReader["contactperson_address"].ToString();
                    contactperson_name = objODBCDataReader["contactperson_name"].ToString();
                    contactperson_number = objODBCDataReader["contactperson_number"].ToString();
                    contactpersonmobileno_gid = objODBCDataReader["contactpersonmobileno_gid"].ToString();
                    contactpersonemail_gid = objODBCDataReader["contactpersonemail_gid"].ToString();
                    contactpersonemail_address = objODBCDataReader["contactpersonemail_address"].ToString();
                    sanction_type = objODBCDataReader["sanction_type"].ToString();
                    natureof_proposal = objODBCDataReader["natureof_proposal"].ToString();
                    branch_gid = objODBCDataReader["branch_gid"].ToString();
                    branch_name = objODBCDataReader["branch_name"].ToString();
                    if (objODBCDataReader["sanctionfrom_date"].ToString() != "")
                    {
                        sanctionfrom_date = objODBCDataReader["sanctionfrom_date"].ToString();
                    }
                    if (objODBCDataReader["sanctiontill_date"].ToString() != "")
                    {
                        sanctiontill_date = objODBCDataReader["sanctiontill_date"].ToString();
                    }
                    paycard = objODBCDataReader["paycard"].ToString();
                    esdeclaration_status = objODBCDataReader["esdeclaration_status"].ToString();
                    created_by = objODBCDataReader["created_by"].ToString();
                    created_date = objODBCDataReader["created_date"].ToString();
                }
                objODBCDataReader.Close();


                msGetGid = objcmnfunctions.GetMasterGID("SULG");
                msSQL = " INSERT INTO ocs_trn_tsanctionsubmittoapprovallog( " +
                              " sanctionsubmittoapprovallog_gid," +
                              " application2sanctionlog_gid," +
                              " application2sanction_gid," +
                               " application_gid ," +
                              " sanction_refno," +
                              " sanction_date," +
                              " sanction_type," +
                              " natureof_proposal," +
                              " applicationtype_gid," +
                              " application_type," +
                              " sanctionto_gid," +
                              " sanctionto_name," +
                              " contactpersonaddress_gid," +
                              " contactperson_address," +
                              " contactperson_name," +
                              " contactpersonmobileno_gid," +
                              " contactperson_number," +
                              " contactpersonemail_gid," +
                              " contactpersonemail_address," +
                              " paycard," +
                              " esdeclaration_status," +
                              " branch_gid, " +
                              " branch_name, " +
                              " sanctionfrom_date," +
                              " sanctiontill_date," +
                              
                              " santioncreated_by," +
                              " santioncreated_date," +
                              " makerfile_path, " +
                              " makerfile_name, " +
                              " sanctionletter_status," +
                              " template_content, " +
                              " makersubmitted_by, " +
                              " makersubmitted_on," +
                              " sanctiongenerated_by, " +
                              " sanctiongenerated_on, " +
                               " template_name, " +
                              " checkerapproved_by, " +
                              //" checkerupdated_on," +
                              " checkerpushback_remarks, " +
                              " checkerapproval_flag, " +
                              //" checkerapproved_on," +
                              " digitalsignature_flag, " +
                              " created_by," +
                              " created_date)" +
                              " values(" +
                              "'" + msGetGid + "', " +
                              "'" + application2sanctionlog_gid + "'," +
                              "'" + application2sanction_gid + "'," +
                              "'" + application_gid + "'," +
                              "'" + sanction_refno + "', " +
                              "'" + sanction_date + "', " +
                              "'" + sanction_type + "', " +
                              "'" + natureof_proposal + "', " +
                              "'" + applicationtype_gid + "', " +
                              "'" + application_type + "', " +
                              "'" + sanctionto_gid + "', " +
                              "'" + sanctionto_name + "', " +
                              "'" + contactpersonaddress_gid + "', " +
                              "'" + contactperson_address + "', " +
                              "'" + contactperson_name + "', " +
                              "'" + contactpersonmobileno_gid + "', " +
                              "'" + contactperson_number + "', " +
                              "'" + contactpersonemail_gid + "', " +
                              "'" + contactpersonemail_address + "', " +
                              "'" + paycard + "', " +
                              "'" + esdeclaration_status + "', " +
                              "'" + branch_gid + "', " +
                              "'" + branch_name + "', ";
                if ((sanctionfrom_date == null) || (sanctionfrom_date == ""))
                {
                    msSQL += "null,";
                }
                else
                {
                    msSQL += "'" + Convert.ToDateTime(sanctionfrom_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                    // msSQL += "'" + sanctionfrom_date + "', " ;
                }
                if ((sanctiontill_date == null) || (sanctiontill_date == ""))
                {
                    msSQL += "null,";
                }
                else
                {
                    msSQL += "'" + Convert.ToDateTime(sanctiontill_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                    // msSQL += "'" + sanctiontill_date + "', " ;
                }              
                msSQL += "'" + created_by + "'," +
                         "'" + Convert.ToDateTime(values.created_date).ToString("yyyy-MM-dd") + "'," +
                         "'" + makerfile_path + "'," +
                         "'" + makerfile_name + "'," +
                         "'" + sanctionletter_status + "'," +
                         "'" + template_content.Replace("'", "''") + "'," +
                         "'" + makersubmitted_by + "',";
                if ((makersubmitted_on == null) || (makersubmitted_on == ""))
                {
                    msSQL += "null,";
                }
                else
                {
                    msSQL += "'" + Convert.ToDateTime(makersubmitted_on).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                }

                msSQL += "'" + employee_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                          "'" + template_name + "'," +
                         "'" + checkerapproved_by + "',";
                //if ((values.checkerupdated_on == null) || (values.checkerupdated_on == ""))
                //{
                //    msSQL += "null,";
                //}
                //else
                //{
                //    msSQL += "'" + Convert.ToDateTime(values.checkerupdated_on).ToString("yyyy-MM-dd") + "',";
                //}
                msSQL += "'" + checkerpushback_remarks + "'," +
                         "'" + checkerapproval_flag + "',";
                //if ((values.checkerapproved_on == null) || (values.checkerapproved_on == ""))
                //{
                //    msSQL += "null,";
                //}
                //else
                //{
                //    msSQL += "'" + Convert.ToDateTime(values.checkerapproved_on).ToString("yyyy-MM-dd") + "',";
                //}
                msSQL += "'" + digitalsignature_flag + "'," +
                         "'" + employee_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult1 = objdbconn.ExecuteNonQuerySQL(msSQL);


            
                        ////objODBCDataReader = objdbconn.GetDataReader(msSQL);




                msSQL = " select  application2sanctiondoclog_gid from ocs_trn_tapplication2sanctiondoclog " +
                   " WHERE application2sanction_gid ='" + values.application2sanction_gid + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows == true)
                {
                    msSQL =  " insert into ocs_trn_tapplication2sanctiondocsubmitlog " +
                             " (application2sanctiondoc_gid,sanctionsubmittoapprovallog_gid,application2sanctionlog_gid,application2sanctiondoclog_gid, application2sanction_gid, application_gid, document_title, document_path, document_name, " +
                             " created_by, created_date, sacreated_date )" +
                             " select application2sanctiondoc_gid,@sanctionsubmittoapprovallog_gid := '" + msGetGid + "',application2sanctionlog_gid,application2sanctiondoclog_gid,  application2sanction_gid, application_gid, document_title, " +
                             " document_path, document_name, created_by, created_date,  @sacreated_date := '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' from ocs_trn_tapplication2sanctiondoclog " +                        
                             " where sacreated_date = (select max(sacreated_date) from ocs_trn_tapplication2sanctiondoclog " +
                             " where  application2sanction_gid ='" + values.application2sanction_gid + "')";
                    mnResultapplication2sanctiondoclog = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                objODBCDataReader.Close();




                msSQL = " Insert into ocs_trn_tlimitproductinfosubmitlog (product_type,productsub_type,odlim_amount,sanctionsubmittoapprovallog_gid,application2sanction_gid,limitproductinfodtllog_gid,limitproductinfodtl_gid,interchangeability,report_structure_gid,report_structure," +
                        " odlim_condition,documented_limit,dateof_Expiry,updated_by,updated_date,created_date)" +
                        " select product_type,productsub_type,odlim_amount,@sanctionsubmittoapprovallog_gid := '" + msGetGid + "',application2sanction_gid,limitproductinfodtllog_gid,limitproductinfodtl_gid,interchangeability,report_structure_gid,report_structure,odlim_condition, " +
                        " documented_limit,dateof_Expiry,updated_by,updated_date,created_date " +
                        " from  ocs_trn_tlimitproductinfolog " +
                        " where created_date = (select max(created_date) from ocs_trn_tlimitproductinfolog " +
                        " where  application2sanction_gid ='" + values.application2sanction_gid + "')";
                         mnResultlimitproductinfolog = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (esdeclaration_status == "Yes")
                {
                    msSQL = " insert into ocs_trn_tesdeclarationdocumentsubmitlog " +
                            " ( sanctionsubmittoapprovallog_gid, esdeclarationlog_gid,application2sanctionlog_gid, application2sanction_gid,esdeclaration_gid,  document_name, " +
                            " document_path, document_type, created_by, created_date, delete_flag, updated_by, updated_date) " +
                            " select @sanctionsubmittoapprovallog_gid := '" + msGetGid + "', esdeclarationlog_gid,application2sanctionlog_gid,  " +
                            "  application2sanction_gid, esdeclaration_gid, document_name, document_path, " +
                            " document_type, created_by, created_date, delete_flag, updated_by, updated_date from ocs_trn_tuploadesdeclarationdocumentlog " +
                            " where updated_date = (select max(updated_date) from ocs_trn_tuploadesdeclarationdocumentlog " +
                            " where  application2sanction_gid ='" + values.application2sanction_gid + "')";
                        mnResultuploadesdeclarationdocumentlog = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                else
                {
                    msSQL = " insert into ocs_trn_tdeviationmaildocumentsubmitlog " +
                               " (sanctionsubmittoapprovallog_gid,maildocumentlog_gid,maildocument_gid,application2sanctionlog_gid, application2sanction_gid, document_name, " +
                               " document_path, document_type, created_by, created_date, delete_flag, updated_by,updated_date) " +
                               " select @sanctionsubmittoapprovallog_gid := '" + msGetGid + "',maildocumentlog_gid,maildocument_gid, application2sanctionlog_gid,application2sanction_gid,  " +
                               "  document_name, document_path, " +
                               " document_type, created_by, created_date, delete_flag, updated_by,updated_date" +
                               "  from ocs_trn_tdeviationmaildocumentlog " +
                               " where updated_date = (select max(updated_date) from ocs_trn_tdeviationmaildocumentlog " +
                               " where  application2sanction_gid ='" + values.application2sanction_gid + "')";
                    mnResultdeviationmaildocumentlog = objdbconn.ExecuteNonQuerySQL(msSQL);
                }




                values.status = true;
                    values.message = "Sanction Re-submitted Successfully";
                
               
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }

        }

        public void DaGetApp2SanctionLimitInfoSubmitDtl(limitandproductslist values, string sanctionsubmittoapprovallog_gid)
        {
            try
            {
                msSQL = " select limitproductinfodtl_gid,interchangeability,report_structure_gid,product_type,productsub_type, " +
                       " report_structure,format(odlim_amount,2,'en_IN') as odlim_amount,odlim_condition,format(existing_limit,2,'en_IN') as existing_limit , " +
                       " format(limit_released,2,'en_IN') as limit_released,format(documented_limit,2,'en_IN') as documented_limit, " +
                       " date_format(dateof_Expiry,'%d-%m-%Y') as dateof_Expiry, " +
                       " limitinfo_remarks,created_by,created_date from ocs_trn_tlimitproductinfosubmitlog " +
                       " where sanctionsubmittoapprovallog_gid='" + sanctionsubmittoapprovallog_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getlimit_info = new List<limitandproducts>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getlimit_info.Add(new limitandproducts
                        {
                            limitproductinfodtl_gid = dr_datarow["limitproductinfodtl_gid"].ToString(),
                            existing_limit = (dr_datarow["existing_limit"].ToString()),
                            limit_released = (dr_datarow["limit_released"].ToString()),
                            interchangeability = (dr_datarow["interchangeability"].ToString()),
                            report_structuregid = (dr_datarow["report_structure_gid"].ToString()),
                            report_structure = (dr_datarow["report_structure"].ToString()),
                            odlim_amount = (dr_datarow["odlim_amount"].ToString()),
                            odlim_condition = (dr_datarow["odlim_condition"].ToString()),
                            limitinfo_remarks = (dr_datarow["limitinfo_remarks"].ToString()),
                            product_type = (dr_datarow["product_type"].ToString()),
                            productsub_type = (dr_datarow["productsub_type"].ToString()),
                            documented_limit = (dr_datarow["documented_limit"].ToString()),
                            dateof_Expiry = (dr_datarow["dateof_Expiry"].ToString()),
                        });
                    }
                    values.limitandproducts = getlimit_info;
                    dt_datatable.Dispose();
                }

                //msSQL = " select format(sum(limit_released),2,'en_IN') from ocs_trn_tlimitproductinfo  a " +
                //        " where generatelsa_gid='" + generatelsa_gid + "'";
                //values.total_limitreleased = objdbconn.GetExecuteScalar(msSQL);

                //msSQL = " select format(sum(existing_limit),2,'en_IN') from ocs_trn_tlimitproductinfo  a " +
                //       " where generatelsa_gid='" + generatelsa_gid + "'";
                //values.total_existinglimit = objdbconn.GetExecuteScalar(msSQL);
                values.status = true;
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = ex.ToString();
            }
        }

        public bool DaPostReUpdateSanction(string employee_gid, cadsanctiondetails values)
        {
            if (values.sanction_type == "Existing Customer")
            {
                if ((values.natureof_proposal == null) || (values.natureof_proposal == ""))
                {
                    values.message = "Kindly Select Nature of Proposal";
                    values.status = false;
                    return false;
                }
            }
            msSQL = " select application2sanction_gid, sanction_refno,  sanction_date, sanction_amount,  entity, paycard, branch_gid,branch_name,  " +
                     " entity_gid, application_gid , ccapproved_date, applicationtype_gid, application_type,esdeclaration_status, " +
                     " sanctionto_gid, sanctionto_name, sanctionfrom_date, sanctiontill_date, contactpersonaddress_gid, " +
                     " contactperson_address, contactperson_name, contactperson_number, contactpersonmobileno_gid, " +
                     " contactpersonemail_gid, contactpersonemail_address, sanction_type, natureof_proposal,created_by,created_date, " +
                     " makerfile_path,makerfile_name,sanctionletter_status,template_content,makersubmitted_by,makersubmitted_on," +
                     " template_name,checkerapproved_by,checkerupdated_on,checkerpushback_remarks,checkerapproval_flag," +
                     " checkerapproved_on,digitalsignature_flag," +
                     " sanctiongenerated_by,sanctiongenerated_on " +
                     " from ocs_trn_tapplication2sanction " +
                     " WHERE application2sanction_gid ='" + values.application2sanction_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows)
            {
                template_name = objODBCDataReader["template_name"].ToString();
                checkerapproved_by = objODBCDataReader["checkerapproved_by"].ToString();
                checkerupdated_on = objODBCDataReader["checkerupdated_on"].ToString();
                checkerpushback_remarks = objODBCDataReader["checkerpushback_remarks"].ToString();
                checkerapproval_flag = objODBCDataReader["checkerapproval_flag"].ToString();
                checkerapproved_on = objODBCDataReader["checkerapproved_on"].ToString();
                digitalsignature_flag = objODBCDataReader["digitalsignature_flag"].ToString();


                makerfile_path = objcmnstorage.EncryptData((objODBCDataReader["makerfile_path"].ToString()));
                makerfile_name = objODBCDataReader["makerfile_name"].ToString();
                sanctionletter_status = objODBCDataReader["sanctionletter_status"].ToString();
                template_content = objODBCDataReader["template_content"].ToString();
                makersubmitted_by = objODBCDataReader["makersubmitted_by"].ToString();
                makersubmitted_on = objODBCDataReader["makersubmitted_on"].ToString();
                sanctiongenerated_by = objODBCDataReader["sanctiongenerated_by"].ToString();
                sanctiongenerated_on = objODBCDataReader["sanctiongenerated_on"].ToString();


                application2sanction_gid = objODBCDataReader["application2sanction_gid"].ToString();
                sanction_refno = objODBCDataReader["sanction_refno"].ToString();
                sanction_amount = objODBCDataReader["sanction_amount"].ToString();
                entity = objODBCDataReader["entity"].ToString();
                entity_gid = objODBCDataReader["entity_gid"].ToString();
                application_gid = objODBCDataReader["application_gid"].ToString();
                //sanction_date = objODBCDataReader["sanctionDate"].ToString();

                if (objODBCDataReader["sanction_date"].ToString() != "")
                {
                    sanction_date = objODBCDataReader["sanction_date"].ToString();
                }
                applicationtype_gid = objODBCDataReader["applicationtype_gid"].ToString();
                application_type = objODBCDataReader["application_type"].ToString();
                sanctionto_gid = objODBCDataReader["sanctionto_gid"].ToString();
                sanctionto_name = objODBCDataReader["sanctionto_name"].ToString();
                contactpersonaddress_gid = objODBCDataReader["contactpersonaddress_gid"].ToString();
                contactperson_address = objODBCDataReader["contactperson_address"].ToString();
                contactperson_name = objODBCDataReader["contactperson_name"].ToString();
                contactperson_number = objODBCDataReader["contactperson_number"].ToString();
                contactpersonmobileno_gid = objODBCDataReader["contactpersonmobileno_gid"].ToString();
                contactpersonemail_gid = objODBCDataReader["contactpersonemail_gid"].ToString();
                contactpersonemail_address = objODBCDataReader["contactpersonemail_address"].ToString();
                sanction_type = objODBCDataReader["sanction_type"].ToString();
                natureof_proposal = objODBCDataReader["natureof_proposal"].ToString();
                branch_gid = objODBCDataReader["branch_gid"].ToString();
                branch_name = objODBCDataReader["branch_name"].ToString();
                if (objODBCDataReader["sanctionfrom_date"].ToString() != "")
                {
                    sanctionfrom_date = objODBCDataReader["sanctionfrom_date"].ToString();
                    //sanctionfromDate = Convert.ToDateTime(objODBCDataReader["sanctionfrom_date"].ToString());
                }
                if (objODBCDataReader["sanctiontill_date"].ToString() != "")
                {
                    sanctiontill_date = objODBCDataReader["sanctiontill_date"].ToString();
                    //sanctiontillDate = Convert.ToDateTime(objODBCDataReader["sanctiontill_date"].ToString());
                }
                paycard = objODBCDataReader["paycard"].ToString();
                esdeclaration_status = objODBCDataReader["esdeclaration_status"].ToString();
                created_by = objODBCDataReader["created_by"].ToString();
                created_date = objODBCDataReader["created_date"].ToString();
            }
            objODBCDataReader.Close();



            msSQL = " update ocs_trn_tapplication2sanction set " +
                     " sanction_date='" + Convert.ToDateTime(values.sanction_date).ToString("yyyy-MM-dd") + "'," +
                     " sanction_type='" + values.sanction_type + "'," +
                     " natureof_proposal='" + values.natureof_proposal + "'," +
                     " applicationtype_gid='" + values.applicationtype_gid + "'," +
                     " application_type='" + values.application_type + "'," +
                     " sanctionto_gid='" + values.sanctionto_gid + "'," +
                     " sanctionto_name='" + values.sanctionto_name.Replace("'", "") + "'," +
                     " contactpersonaddress_gid='" + values.contactpersonaddress_gid + "'," +
                     " contactperson_address='" + values.contactperson_address.Replace("'", "") + "'," +
                     " contactperson_name='" + values.contactperson_name + "'," +
                     " contactpersonmobileno_gid='" + values.contactpersonmobileno_gid + "'," +
                     " contactperson_number='" + values.contactperson_number + "'," +
                     " contactpersonemail_gid='" + values.contactpersonemail_gid + "'," +
                     " paycard ='" + values.paycard + "'," +
                     " esdeclaration_status ='" + values.esdeclaration_status + "'," +
                     " branch_gid ='" + values.branch_gid + "'," +
                     " branch_name ='" + values.branch_name + "'," +
                     " resubmitenable_flag='Y'," +
                     " contactpersonemail_address='" + values.contactpersonemail_address + "',";
            if ((values.sanctionfrom_date == null) || (values.sanctionfrom_date == ""))
            {
                msSQL += "sanctionfrom_date=null,";
            }
            else
            {
                msSQL += "sanctionfrom_date='" + Convert.ToDateTime(values.sanctionfrom_date).ToString("yyyy-MM-dd") + "',";
            }
            if ((values.sanctiontill_date == null) || (values.sanctiontill_date == ""))
            {
                msSQL += "sanctiontill_date=null,";
            }
            else
            {
                msSQL += "sanctiontill_date='" + Convert.ToDateTime(values.sanctiontill_date).ToString("yyyy-MM-dd") + "',";
            }
            msSQL += " updated_by='" + employee_gid + "'," +
                    " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    " where application2sanction_gid='" + values.application2sanction_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msGetGid = objcmnfunctions.GetMasterGID("SALG");
            msSQL = " INSERT INTO ocs_trn_tapplication2sanctionlog( " +
                          " application2sanctionlog_gid," +
                          " application2sanction_gid," +
                           " application_gid ," +
                          " sanction_refno," +
                          " sanction_date," +
                          " sanction_type," +
                          " natureof_proposal," +
                          " applicationtype_gid," +
                          " application_type," +
                          " sanctionto_gid," +
                          " sanctionto_name," +
                          " contactpersonaddress_gid," +
                          " contactperson_address," +
                          " contactperson_name," +
                          " contactpersonmobileno_gid," +
                          " contactperson_number," +
                          " contactpersonemail_gid," +
                          " contactpersonemail_address," +
                          " paycard," +
                          " esdeclaration_status," +
                          " branch_gid, " +
                          " branch_name, " +
                          " sanctionfrom_date," +
                          " sanctiontill_date," +
                          " newsanction_refno," +
                          " newsanction_date," +
                          " newsanction_type," +
                          " newnatureof_proposal," +
                          " newapplicationtype_gid," +
                          " newapplication_type," +
                          " newsanctionto_gid," +
                          " newsanctionto_name," +
                          " newcontactpersonaddress_gid," +
                          " newcontactperson_address," +
                          " newcontactperson_name," +
                          " newcontactpersonmobileno_gid," +
                          " newcontactperson_number," +
                          " newcontactpersonemail_gid," +
                          " newcontactpersonemail_address," +
                          " newpaycard," +
                          " newesdeclaration_status," +
                          " newbranch_gid, " +
                          " newbranch_name, " +
                          " newsanctionfrom_date," +
                          " newsanctiontill_date," +
                          " santioncreated_by," +
                          " santioncreated_date," +
                          " makerfile_path, " +
                          " makerfile_name, " +
                          " sanctionletter_status," +
                          " template_content, " +
                          " makersubmitted_by, " +
                          " makersubmitted_on," +
                          " sanctiongenerated_by, " +
                          " sanctiongenerated_on, " +
                           " template_name, " +
                          " checkerapproved_by, " +
                          " checkerupdated_on," +
                          " checkerpushback_remarks, " +
                          " checkerapproval_flag, " +
                          " checkerapproved_on," +
                          " digitalsignature_flag, " +
                          " created_by," +
                          " created_date)" +
                          " values(" +
                          "'" + msGetGid + "', " +
                          "'" + application2sanction_gid + "'," +
                          "'" + application_gid + "'," +
                          "'" + sanction_refno + "', " +
                          "'" + sanction_date + "', " +
                          "'" + sanction_type + "', " +
                          "'" + natureof_proposal + "', " +
                          "'" + applicationtype_gid + "', " +
                          "'" + application_type + "', " +
                          "'" + sanctionto_gid + "', " +
                          "'" + sanctionto_name + "', " +
                          "'" + contactpersonaddress_gid + "', " +
                          "'" + contactperson_address.Replace("'", "") + "', " +
                          "'" + contactperson_name + "', " +
                          "'" + contactpersonmobileno_gid + "', " +
                          "'" + contactperson_number + "', " +
                          "'" + contactpersonemail_gid + "', " +
                          "'" + contactpersonemail_address + "', " +
                          "'" + paycard + "', " +
                          "'" + esdeclaration_status + "', " +
                          "'" + branch_gid + "', " +
                          "'" + branch_name + "', ";
            if ((sanctionfrom_date == null) || (sanctionfrom_date == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(sanctionfrom_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                // msSQL += "'" + sanctionfrom_date + "', " ;
            }
            if ((sanctiontill_date == null) || (sanctiontill_date == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(sanctiontill_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                // msSQL += "'" + sanctiontill_date + "', " ;
            }
            msSQL += "'" + values.sanction_refno + "', " +
                     "'" + Convert.ToDateTime(values.sanction_date).ToString("yyyy-MM-dd") + "'," +
                     "'" + values.sanction_type + "', " +
                     "'" + values.natureof_proposal + "', " +
                     "'" + values.applicationtype_gid + "', " +
                     "'" + values.application_type + "', " +
                     "'" + values.sanctionto_gid + "', " +
                     "'" + values.sanctionto_name.Replace("'", "") + "', " +
                     "'" + values.contactpersonaddress_gid + "', " +
                     "'" + values.contactperson_address + "', " +
                     "'" + values.contactperson_name + "', " +
                     "'" + values.contactpersonmobileno_gid + "', " +
                     "'" + values.contactperson_number + "', " +
                     "'" + values.contactpersonemail_gid + "', " +
                     "'" + values.contactpersonemail_address + "', " +
                     "'" + values.paycard + "', " +
                     "'" + values.esdeclaration_status + "', " +
                     "'" + values.branch_gid + "', " +
                     "'" + values.branch_name + "', ";
            if ((values.sanctionfrom_date == null) || (values.sanctionfrom_date == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.sanctionfrom_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            if ((values.sanctiontill_date == null) || (values.sanctiontill_date == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.sanctiontill_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            msSQL += "'" + created_by + "'," +
                     "'" + Convert.ToDateTime(values.created_date).ToString("yyyy-MM-dd") + "'," +
                     "'" + makerfile_path + "'," +
                     "'" + makerfile_name + "'," +
                     "'" + sanctionletter_status + "'," +
                     "'" + template_content.Replace("'", "''") + "'," +
                     "'" + makersubmitted_by + "',";
            if ((makersubmitted_on == null) || (makersubmitted_on == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(makersubmitted_on).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }

            msSQL += "'" + employee_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                      "'" + template_name + "'," +
                     "'" + checkerapproved_by + "',";
            if ((values.checkerupdated_on == null) || (values.checkerupdated_on == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.checkerupdated_on).ToString("yyyy-MM-dd") + "',";
            }
            msSQL += "'" + checkerpushback_remarks + "'," +
                     "'" + checkerapproval_flag + "',";
            if ((values.checkerapproved_on == null) || (values.checkerapproved_on == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.checkerapproved_on).ToString("yyyy-MM-dd") + "',";
            }
            msSQL += "'" + digitalsignature_flag + "'," +
                     "'" + employee_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult1 = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " select  application2sanctiondoc_gid, application2sanction_gid, application_gid, document_title, " +
                    " document_path, document_name, created_by, created_date from ocs_trn_tapplication2sanctiondoc " +
                    " WHERE application2sanction_gid ='" + values.application2sanction_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                msSQL = " insert into ocs_trn_tapplication2sanctiondoclog " +
                        " (application2sanctiondoc_gid,application2sanctionlog_gid, application2sanction_gid, application_gid, document_title, document_path, document_name, " +
                        " created_by, created_date, sacreated_date) " +
                        " select application2sanctiondoc_gid,@application2sanctionlog_gid := '" + msGetGid + "', application2sanction_gid, application_gid, document_title, " +
                        " document_path, document_name, created_by, created_date,  @sacreated_date := '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' from ocs_trn_tapplication2sanctiondoc " +
                        " WHERE application2sanction_gid ='" + values.application2sanction_gid + "'";
                        mnResultapplication2sanctiondoclog = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            objODBCDataReader.Close();


            if (values.esdeclaration_status == "Yes")
            {
                msSQL = " insert into ocs_trn_tuploadesdeclarationdocumentlog " +
                        " ( application2sanctionlog_gid, application2sanction_gid,esdeclaration_gid,  document_name, " +
                        " document_path, document_type, created_by, created_date, delete_flag, updated_by,updated_date) " +
                        " select @application2sanctionlog_gid := '" + msGetGid + "',  " +
                        "  application2sanction_gid, esdeclaration_gid, document_name, document_path, " +
                        " document_type, created_by, created_date, delete_flag, updated_by,  @updated_date := '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' from ocs_trn_tuploadesdeclarationdocument " +
                        " where application2sanction_gid='" + values.application2sanction_gid + "'";
                mnResultuploadesdeclarationdocumentlog = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else
            {
                msSQL = " insert into ocs_trn_tdeviationmaildocumentlog " +
                           " (maildocument_gid,application2sanctionlog_gid, application2sanction_gid, document_name, " +
                           " document_path, document_type, created_by, created_date, delete_flag, updated_by,updated_date) " +
                           " select maildocument_gid, @application2sanctionlog_gid := '" + msGetGid + "',application2sanction_gid,  " +
                           "  document_name, document_path, " +
                           " document_type, created_by, created_date, delete_flag, updated_by,  @updated_date := '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                           "  from ocs_trn_tdeviationmaildocument " +
                           " where application2sanction_gid='" + values.application2sanction_gid + "'";
                mnResultdeviationmaildocumentlog = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            if (values.esdeclaration_status == "Yes")
            {
                msSQL = "delete from ocs_trn_tdeviationmaildocument where application2sanction_gid='" + values.application2sanction_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else
            {
                msSQL = "delete from ocs_trn_tuploadesdeclarationdocument where application2sanction_gid='" + values.application2sanction_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            if (mnResult != 0)
            {

                values.message = "Sanction Details are Updated Successfully";
                values.status = true;
                return true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;
                return false;

            }
        }


        public void DaGetSanctionHistory(string sanctionsubmittoapprovallog_gid, MdlMstCAD values)
        {
            msSQL = " select application2sanctiondoc_gid,application_gid,document_name,document_path,document_title from " +
                       " ocs_trn_tapplication2sanctiondocsubmitlog where sanctionsubmittoapprovallog_gid='" + sanctionsubmittoapprovallog_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getsanctiondocument_list = new List<sanctiondocument_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getsanctiondocument_list.Add(new sanctiondocument_list
                    {
                        document_name = dt["document_name"].ToString(),
                        document_title = dt["document_title"].ToString(),
                        document_path = objcmnstorage.EncryptData((dt["document_path"].ToString())),
                        application_gid = dt["application_gid"].ToString(),
                        application2sanctiondoc_gid = dt["application2sanctiondoc_gid"].ToString(),

                    });
                    values.sanctiondocument_list = getsanctiondocument_list;
                }
            }
            dt_datatable.Dispose();
        }

        //npa report
        public void DaGetExportNpaReport(MdlMstCAD objMstCAD)
        {
           
          msSQL = " select a.customer_urn as 'Customer Urn',a.customer_name as 'Customer Name', a.vertical_name as 'Vertical',a.region as 'Region'," +
                  " case when d.npatag_flag = 'Y' then 'Tagged' when d.npatag_flag = 'N' then 'Untagged' else 'NA' end as 'NPA Tagging', " +
                  " date_format(a.processupdated_date, '%d-%m-%Y %h:%i %p') as 'Cad Accepted Date', " +
                  " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as 'Cad Accepted By', " +
                  " case when a.renewal_flag = 'Y' then 'Renewal' when a.enhancement_flag = 'Y' then 'Enhancement' else 'New' end as 'Type', " +
                  " concat(h.user_firstname, ' ', h.user_lastname, ' / ', h.user_code) as 'Tagged By', " +
                  " date_format(d.npacreated_date, '%d-%m-%Y %h:%i %p') as 'Tagged Date', " +
                  " d.nparemarks as 'Remarks'  from ocs_trn_tcadapplication a " +               
                  " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by " +
                  " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                  " left join ocs_trn_tcustomer2tag d on d.urn_number = a.customer_urn " +
                  " left join hrm_mst_temployee g on g.employee_gid = d.npacreated_by " +
                  " left join adm_mst_tuser h on h.user_gid = g.user_gid " +   
                  " where a.process_type = 'Accept' and(a.processupdated_date = (select max(e.processupdated_date) from ocs_trn_tcadapplication e where e.customer_urn = a.customer_urn and a.customer_urn <> '')) " +
                  " and d.npatag_flag = 'Y' order by a.processupdated_date desc ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            string lscompany_code = string.Empty;
            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            var workSheet = excel.Workbook.Worksheets.Add("NPA Tagged Report");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                objMstCAD.lsname = "NPA Report.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/NPA Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                objMstCAD.lscloudpath = lscompany_code + "/" + "Master/NPA Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstCAD.lsname;
                objMstCAD.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/NPA Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstCAD.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(objMstCAD.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 23])  //Address "A1:A9"
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);
                }
                excel.SaveAs(ms);
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", objMstCAD.lscloudpath, ms);
                ms.Close();

            }
            catch (Exception ex)
            {
                objMstCAD.status = false;
                objMstCAD.message = "Failure";
            }
            objMstCAD.lscloudpath = objcmnstorage.EncryptData(objMstCAD.lscloudpath);
            objMstCAD.lspath = objcmnstorage.EncryptData(objMstCAD.lspath);
            objMstCAD.status = true;
            objMstCAD.message = "Success";
        }

        //Legal Report
        public void DaExportLegalReport(MdlMstCAD objMstCAD)
        {
            msSQL = " select a.customer_urn as 'Customer Urn',a.customer_name as 'Customer Name', a.vertical_name as 'Vertical',a.region as 'Region', " +
                    " case when d.legaltag_flag = 'Y' then 'Tagged' when d.legaltag_flag = 'N' then 'Untagged' else 'NA' end as 'Legal Tagging', " +            
                    " date_format(a.processupdated_date, '%d-%m-%Y %h:%i %p') as 'Cad Accepted Date', " +
                    " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as 'Cad Accepted By', " +
                    " case when a.renewal_flag = 'Y' then 'Renewal' when a.enhancement_flag = 'Y' then 'Enhancement' else 'New' end as 'Type', " +
                    " concat(h.user_firstname, ' ', h.user_lastname, ' / ', h.user_code) as 'Tagged By', " +
                    " date_format(d.legalcreated_date, '%d-%m-%Y %h:%i %p') as 'Tagged Date', " +               
                    " d.legalremarks as Remarks  from ocs_trn_tcadapplication a " +        
                    " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " left join ocs_trn_tcustomer2tag d on d.urn_number = a.customer_urn " +
                    " left join hrm_mst_temployee g on g.employee_gid = d.legalcreated_by " +
                    " left join adm_mst_tuser h on h.user_gid = g.user_gid " +               
                    " where a.process_type = 'Accept' and(a.processupdated_date = (select max(e.processupdated_date) from ocs_trn_tcadapplication e where e.customer_urn = a.customer_urn and a.customer_urn <> '')) " +
                    " and d.legaltag_flag = 'Y'  order by a.processupdated_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            string lscompany_code = string.Empty;
            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            var workSheet = excel.Workbook.Worksheets.Add("Legal Tagged Report");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                objMstCAD.lsname = "Legal Report.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/Legal Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                objMstCAD.lscloudpath = lscompany_code + "/" + "Master/Legal Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstCAD.lsname;
                objMstCAD.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/Legal Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstCAD.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(objMstCAD.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 23])  //Address "A1:A9"
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);
                }
                excel.SaveAs(ms);
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", objMstCAD.lscloudpath, ms);
                ms.Close();

            }
            catch (Exception ex)
            {
                objMstCAD.status = false;
                objMstCAD.message = "Failure";
            }
            objMstCAD.lscloudpath = objcmnstorage.EncryptData(objMstCAD.lscloudpath);
            objMstCAD.lspath = objcmnstorage.EncryptData(objMstCAD.lspath);
            objMstCAD.status = true;
            objMstCAD.message = "Success";
        }

        public void DaGetCadDocumentSubmissionFlag(string employee_gid,string application_gid, MdlCadDocumentSubmissionFlag values)
        {
            try
            {
                msSQL = " select a.application_gid from ocs_trn_tcadapplication a " +
                        " left join ocs_trn_tprocesstype_assign b on b.application_gid = a.application_gid " +
                        " where a.process_type = 'Accept' and docchecklist_approvalflag = 'Y' and menu_gid = 'CADMGTDCL'" +
                        " and approver_approvalflag = 'Y' " +
                        " and a.application_gid = '" + application_gid + "'" +
                        " group by a.application_gid order by a.updated_date desc";
                var lsdocsubmission_flag = objdbconn.GetExecuteScalar(msSQL);
                if (lsdocsubmission_flag == "")
                {
                    values.docsubmission_flag = "N";
                }
                else {
                    values.docsubmission_flag = "Y";
                }                
               
            }
            catch (Exception ex)
            {
                values.message = ex.ToString();
                values.status = false;
            }
        }
        public void DaSentBackUpdateProcessType(string employee_gid, MdlUpdateProcessType values)
        {
            msSQL = " update ocs_mst_tapplication set " +
                     " process_type='" + values.process_type + "',";
            if (values.processtype_remarks == null || values.processtype_remarks == "")
            {
                msSQL += " processtype_remarks='',";
            }
            else
            {
                msSQL += " processtype_remarks='" + values.processtype_remarks.Replace("'", "") + "',";

            }
            msSQL += " processupdated_by='" + employee_gid + "'," +
                     " processupdated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                     " where application_gid='" + values.application_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);         

            if (mnResult != 0)
            {               
                values.message = "Process Type Added Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }

    }
}