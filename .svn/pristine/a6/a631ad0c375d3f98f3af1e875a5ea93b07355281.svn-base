using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Odbc;
using ems.utilities.Functions;
using ems.mastersamagro.Models;
using System.Configuration;
using ems.storage.Functions;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using System.Globalization;
using OfficeOpenXml;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Net;
using System.IO;
using ems.hbapiconn.Functions;
using System.Threading;
using Newtonsoft.Json;

namespace ems.mastersamagro.DataAccess
{
    /// <summary>
    /// This DataAccess will help to clone records from buyer onboard for renewal, amendment & short closing
    /// </summary>
    /// <remarks>Written by Logapriya.S, Abilash.A </remarks>
    public class DaAgrTrnCloneApplication
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        DataTable dt_datatable, dt_tloan, dt_tcontact, dt_tinstitution, dt_thypothecation, dt_datatable2, dt_datatable3, dt_datatable4, dt_datatable5, dt_datatable6, dt_datatable7, dt_datatable8, dt_datatable9, dt_datatable10, dt_datatable11, dt_datatable12, dt_datatable13, dt_datatable21;
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        HttpPostedFile httpPostedFile;
        string msSQL, msGetGid, msGetDocumentGid, msGetGidpan;
        string lsemployee_name, lsapplication_gid, lsapp_refno;        
        int mnResult, k;
        OdbcDataReader objODBCDataReader, objODBCDatareader;        
        string lsapplication_no, lscustomerref_name;
        string sToken = string.Empty;
        Random rand = new Random();
        string msGetGidApplication, msGetGidContactno, msGetGidemail, msGetGidgeneticcode, msGetGidinstitution, msGetGidinstitutionbranch, msGetGidinstitutionmobile, msGetGidinstitutionemail, msGetGidinstitutionaddress, msGetGidinstitutionlicense, msGetGidinstitutiondocument, msGetGidinstitutionratingdtl, msGetGidinstitutionbankdtl;
        string msGetGidcontact, msGetGidcontactpanreason, msGetGidcontactpanform60, msGetGidcontactmobile, msGetGidcontactemail, msGetGidcontactidproof, msGetGidcontactaddress, msGetGidcontactdocument;
        string msGetGidapplication2loan, msGetGidpaymenttypecustomer, msGetGidapplication2product, msGetGidproduct2gststatus, msGetGidproduct2commoditytrade, msGetGidproduct2commoditydoc, msGetGidloan2supplierdtl, msGetGidapp2suppliergstdtl, msGetGidloan2supplierpayment, msGetGidapplication2buyer, msGetGidappl2servicecharge, msGetGidgroupdocumentchecklist;
        string msGetGidappltrade2warehouse, msGetGidapplication2trade, msGetamendmentreason_gid, msGetGiddocumentchecktls;
        int mnResultapplicationcreation, mnResultapplicationcontact, mnResultapplicationgeneticcode, mnResultapplicationemail, mnResultinstitution, mnResultinstitutionbranch, mnResultinstitutionmobile, mnResultinstitutionemail, mnResultinstitutionaddress, mnResultinstitutionlicense, mnResultinstitutiondocument, mnResultinstitutionratingdtl, mnResultinstitutionbankdtl;
        int mnResultcontact, mnResultcontactpanabsencereason, mnResultcontactpanform60, mnResultcontactmobile, mnResultcontactemail, mnResultcontactidproof, mnResultcontactaddress, mnResultcontactdocument;
        int mnResultapplication2loan, mnResultpaymenttypecustomer, mnResultapplication2product, mnResultproduct2gststatus, mnResultproduct2commoditytrade, mnResultproduct2commoditydoc, mnResultloan2supplierdtl, mnResultapp2suppliergstdtl, mnResultloan2supplierpayment, mnResultapplication2buyer, mnResultappl2servicecharge;
        int mnResultappltrade2warehouse, mnResultapplication2trade, mnResultonboardinitiate, mnResultamendmentreason, mnResultdocumentchecktls, mnResultgroupdocumentchecklist;
        string lsshortclosing_flag, lsapproval_status, lsapproval_status_check;

        // Application Renewal
        public bool DaPostRenewalAdd(string employee_gid, string user_gid, MdlMstCloneRenewalAdd values)
        {
            //msSQL = " select a.application_no,a.application_gid,a.processupdated_date as dateofapp " +
            //        " from agr_mst_tapplication a " +
            //        " where a.processupdated_date = (select max(c.processupdated_date) from agr_mst_tapplication c " +
            //        " where c.buyeronboard_gid = '" + values.buyer_gid + "' and a.onboarding_status = 'Proposal' and a.process_type = 'Accept') " +
            //        " union " +
            //        " select p.application_no,p.application_gid, p.headapproval_date as dateofapp from agr_mst_tapplication p " +
            //        " where (p.renewal_flag = 'Y' and p.approval_status like '%Rejected%' and p.headapproval_date = (select max(b.headapproval_date) from agr_mst_tapplication b " +
            //        " where b.approval_status like '%Rejected%' and b.buyeronboard_gid = '" + values.buyer_gid + "')) " +
            //        " union " +
            //        " select q.application_no,q.application_gid,q.creditheadapproval_date as dateofapp from agr_mst_tapplication q " +
            //        " where (q.renewal_flag = 'Y' and q.approval_status like '%Rejected%' and q.creditheadapproval_date = (select max(b.creditheadapproval_date) from agr_mst_tapplication b " +
            //        " where b.approval_status like '%Rejected%' and b.buyeronboard_gid = '" + values.buyer_gid + "')) " +
            //        " union " +
            //        " select r.application_no,r.application_gid,r.cccompleted_date as dateofapp from agr_mst_tapplication r " +
            //        " where (r.renewal_flag = 'Y' and r.approval_status like '%Rejected%' and r.cccompleted_date = (select max(b.cccompleted_date) from agr_mst_tapplication b " +
            //        " where b.approval_status like '%Rejected%' and b.buyeronboard_gid = '" + values.buyer_gid + "')) " +
            //        " order by dateofapp desc limit 1 ";
            //objODBCDataReader = objdbconn.GetDataReader(msSQL);
            //if (objODBCDataReader.HasRows == true)
            //{
            //    values.refapplication_no = objODBCDataReader["application_no"].ToString();
            //    values.refapplication_gid = objODBCDataReader["application_gid"].ToString();
            //    values.dateofapp = objODBCDataReader["dateofapp"].ToString();
            //}
            //objODBCDataReader.Close();

            //msSQL = " select a.application_no,a.application_gid,a.processupdated_date as dateofapp " +
            //        " from agr_mst_tapplication a " +
            //        " where a.processupdated_date = (select max(c.processupdated_date) from agr_mst_tapplication c " +
            //        " where c.buyeronboard_gid = '" + values.buyer_gid + "' and c.onboarding_status = 'Proposal' and c.process_type = 'Accept'and expired_flag ='N') ";
            //objODBCDataReader = objdbconn.GetDataReader(msSQL);
            //if (objODBCDataReader.HasRows == true)
            //{
            //    values.application_no = objODBCDataReader["application_no"].ToString();
            //    values.application_gid = objODBCDataReader["application_gid"].ToString();
            //}
            //objODBCDataReader.Close();


            //msSQL = " select a.shortclosing_flag from agr_mst_tapplication a " +
            //      " where a.created_date = (select max(c.created_date) from agr_mst_tapplication c " +
            //      " where c.buyeronboard_gid = '" + values.buyer_gid + "' and c.onboarding_status = 'Proposal') ";
            //lsshortclosing_flag = objdbconn.GetExecuteScalar(msSQL);
            //if (lsshortclosing_flag != "N")
            //{
            //    msSQL = " select a.approval_status from agr_mst_tapplication a " +
            //            " where a.created_date = (select max(c.created_date) from agr_mst_tapplication c " +
            //            " where c.buyeronboard_gid = '" + values.buyer_gid + "' and c.onboarding_status = 'Proposal') ";
            //    lsapproval_status = objdbconn.GetExecuteScalar(msSQL);
            //    if (lsapproval_status == "Rejected By Business" || lsapproval_status == "Rejected By Credit" || lsapproval_status == "CC Rejected")
            //    {
            //        msSQL = " select a.application_no,a.application_gid,a.processupdated_date as dateofapp " +
            //      " from agr_mst_tapplication a " +
            //      " where a.processupdated_date = (select max(c.processupdated_date) from agr_mst_tapplication c " +
            //      " where c.buyeronboard_gid = '" + values.buyer_gid + "' and c.onboarding_status = 'Proposal' and c.process_type = 'Accept' ) ";
            //        objODBCDataReader = objdbconn.GetDataReader(msSQL);
            //        if (objODBCDataReader.HasRows == true)
            //        {
            //            values.application_no = objODBCDataReader["application_no"].ToString();
            //            values.application_gid = objODBCDataReader["application_gid"].ToString();
            //        }
            //        objODBCDataReader.Close();
            //    }
            //    else
            //    {
            //        msSQL = " select a.application_no,a.application_gid,a.processupdated_date as dateofapp " +
            //        " from agr_mst_tapplication a " +
            //        " where a.processupdated_date = (select max(c.processupdated_date) from agr_mst_tapplication c " +
            //        " where c.buyeronboard_gid = '" + values.buyer_gid + "' and c.onboarding_status = 'Proposal' and c.process_type = 'Accept' and expired_flag !='Y' )  ";
            //        objODBCDataReader = objdbconn.GetDataReader(msSQL);
            //        if (objODBCDataReader.HasRows == true)
            //        {
            //            values.application_no = objODBCDataReader["application_no"].ToString();
            //            values.application_gid = objODBCDataReader["application_gid"].ToString();
            //        }
            //        objODBCDataReader.Close();
            //    }

            //}
            //else
            //{
            //    msSQL = " select a.approval_status from agr_mst_tapplication a " +
            //        " where a.created_date = (select max(c.created_date) from agr_mst_tapplication c " +
            //        " where c.buyeronboard_gid = '" + values.buyer_gid + "' and c.onboarding_status = 'Proposal' and shortclosing_flag ='Y')";
            //    lsapproval_status_check = objdbconn.GetExecuteScalar(msSQL);
            //    if (lsapproval_status_check == "Rejected By Business" || lsapproval_status_check == "Rejected By Credit" || lsapproval_status_check == "CC Rejected")
            //    {
            //        msSQL = " select a.application_no,a.application_gid,a.processupdated_date as dateofapp " +
            //        " from agr_mst_tapplication a " +
            //        " where a.processupdated_date = (select max(c.processupdated_date) from agr_mst_tapplication c " +
            //        " where c.buyeronboard_gid = '" + values.buyer_gid + "' and c.onboarding_status = 'Proposal' and c.process_type = 'Accept' ) ";
            //        objODBCDataReader = objdbconn.GetDataReader(msSQL);
            //        if (objODBCDataReader.HasRows == true)
            //        {
            //            values.application_no = objODBCDataReader["application_no"].ToString();
            //            values.application_gid = objODBCDataReader["application_gid"].ToString();
            //        }
            //        objODBCDataReader.Close();
            //    }
            //    else
            //    {
            //        msSQL = " select a.application_no,a.application_gid,a.processupdated_date as dateofapp " +
            //        " from agr_mst_tapplication a " +
            //        " where a.processupdated_date = (select max(c.processupdated_date) from agr_mst_tapplication c " +
            //        " where c.buyeronboard_gid = '" + values.buyer_gid + "' and c.onboarding_status = 'Proposal' and c.process_type = 'Accept' and expired_flag !='Y' )  ";
            //        objODBCDataReader = objdbconn.GetDataReader(msSQL);
            //        if (objODBCDataReader.HasRows == true)
            //        {
            //            values.application_no = objODBCDataReader["application_no"].ToString();
            //            values.application_gid = objODBCDataReader["application_gid"].ToString();
            //        }
            //        objODBCDataReader.Close();
            //    }

            //}

            msSQL = " select b.producttype_gid,b.productsubtype_gid,application_no,customer_urn,customer_name,entity_gid,entity_name, " +
                    " vertical_gid,vertical_name,verticaltaggs_gid,verticaltaggs_name,constitution_gid,constitution_name,businessunit_gid, " +
                    " businessunit_name,sa_status as sastatus,sa_id,sa_name,baselocation_gid,baselocation_name,cluster_gid,cluster_name,region_gid, " +
                    " region_name,zone_gid,zone_name,relationshipmanager_name,relationshipmanager_gid,drm_gid,drm_name,clustermanager_gid, " +
                    " clustermanager_name,zonalhead_name,zonalhead_gid,regionalhead_name,regionalhead_gid,businesshead_name,businesshead_gid, " +
                    " vernacular_language,vernacularlanguage_gid,contactpersonfirst_name,contactpersonmiddle_name, " +
                    " contactpersonlast_name,designation_gid,designation_type,landline_no,status as application_status, " +
                    " saname_gid,economical_flag,productcharge_flag,applicant_type,customerref_name, " +
                    " productcharges_status,mobile_no,email_address,approval_flag,gradingdraft_flag,hypothecation_flag,submitted_by, " +
                    " date_format(submitted_date, '%Y-%m-%d %H:%i:%s') as submitted_date,region,creditgroup_gid,creditgroup_name, " +
                    " a.program_gid,a.program_name,a.product_gid,a.product_name,a.sector_name,a.category_name,a.variety_gid,a.variety_name, " +
                    " a.botanical_name,a.alternative_name,approval_status,document_name,document_path,renewal_flag, " +
                    " validityfrom_date,validityto_date,onboarding_status," +
                    " productdesk_flag,productdesk_gid,productquery_status,contract_id,buyeronboard_gid, " +
                    " social_capital,trade_capital,overalllimit_amount,calculationoveralllimit_validity " +
                    " from agr_mst_tapplication a " +
                    " left join agr_mst_tapplication2loan b on a.application_gid = b.application_gid " +
                    "where a.application_gid='" + values.application_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.application_no = objODBCDatareader["application_no"].ToString();
                values.customer_urn = objODBCDatareader["customer_urn"].ToString();
                values.customer_name = objODBCDatareader["customer_name"].ToString();
                values.entity_gid = objODBCDatareader["entity_gid"].ToString();
                values.entity_name = objODBCDatareader["entity_name"].ToString();
                values.vertical_gid = objODBCDatareader["vertical_gid"].ToString();
                values.vertical_name = objODBCDatareader["vertical_name"].ToString();
                values.verticaltaggs_gid = objODBCDatareader["verticaltaggs_gid"].ToString();
                values.verticaltaggs_name = objODBCDatareader["verticaltaggs_name"].ToString();
                values.constitution_gid = objODBCDatareader["constitution_gid"].ToString();
                values.constitution_name = objODBCDatareader["constitution_name"].ToString();
                values.businessunit_gid = objODBCDatareader["businessunit_gid"].ToString();
                values.businessunit_name = objODBCDatareader["businessunit_name"].ToString();
                values.sastatus = objODBCDatareader["sastatus"].ToString();
                values.sa_id = objODBCDatareader["sa_id"].ToString();
                values.sa_name = objODBCDatareader["sa_name"].ToString();
                values.baselocation_gid = objODBCDatareader["baselocation_gid"].ToString();
                values.baselocation_name = objODBCDatareader["baselocation_name"].ToString();
                values.cluster_gid = objODBCDatareader["cluster_gid"].ToString();
                values.cluster_name = objODBCDatareader["cluster_name"].ToString();
                values.region_gid = objODBCDatareader["region_gid"].ToString();
                values.region_name = objODBCDatareader["region_name"].ToString();
                values.zone_gid = objODBCDatareader["zone_gid"].ToString();
                values.zone_name = objODBCDatareader["zone_name"].ToString();
                values.relationshipmanager_name = objODBCDatareader["relationshipmanager_name"].ToString();
                values.relationshipmanager_gid = objODBCDatareader["relationshipmanager_gid"].ToString();
                values.drm_gid = objODBCDatareader["drm_gid"].ToString();
                values.drm_name = objODBCDatareader["drm_name"].ToString();
                values.clustermanager_gid = objODBCDatareader["clustermanager_gid"].ToString();
                values.clustermanager_name = objODBCDatareader["clustermanager_name"].ToString();
                values.zonalhead_name = objODBCDatareader["zonalhead_name"].ToString();
                values.zonalhead_gid = objODBCDatareader["zonalhead_gid"].ToString();
                values.regionalhead_name = objODBCDatareader["regionalhead_name"].ToString();
                values.regionalhead_gid = objODBCDatareader["regionalhead_gid"].ToString();
                values.businesshead_name = objODBCDatareader["businesshead_name"].ToString();
                values.businesshead_gid = objODBCDatareader["businesshead_gid"].ToString();
                values.vernacular_language = objODBCDatareader["vernacular_language"].ToString();
                values.vernacularlanguage_gid = objODBCDatareader["vernacularlanguage_gid"].ToString();
                values.contactpersonfirst_name = objODBCDatareader["contactpersonfirst_name"].ToString();
                values.contactpersonmiddle_name = objODBCDatareader["contactpersonmiddle_name"].ToString();
                values.contactpersonlast_name = objODBCDatareader["contactpersonlast_name"].ToString();
                values.designation_gid = objODBCDatareader["designation_gid"].ToString();
                values.designation_type = objODBCDatareader["designation_type"].ToString();
                values.landline_no = objODBCDatareader["landline_no"].ToString();
                values.application_status = objODBCDatareader["application_status"].ToString();
                values.saname_gid = objODBCDatareader["saname_gid"].ToString();
                values.economical_flag = objODBCDatareader["economical_flag"].ToString();
                values.productcharge_flag = objODBCDatareader["productcharge_flag"].ToString();
                values.applicant_type = objODBCDatareader["applicant_type"].ToString();
                values.customerref_name = objODBCDatareader["customerref_name"].ToString();
                values.productcharges_status = objODBCDatareader["productcharges_status"].ToString();
                values.approval_status = objODBCDatareader["approval_status"].ToString();
                values.mobile_no = objODBCDatareader["mobile_no"].ToString();
                values.email_address = objODBCDatareader["email_address"].ToString();
                values.approval_flag = objODBCDatareader["approval_flag"].ToString();
                values.gradingdraft_flag = objODBCDatareader["gradingdraft_flag"].ToString();
                values.hypothecation_flag = objODBCDatareader["hypothecation_flag"].ToString();
                values.submitted_by = objODBCDatareader["submitted_by"].ToString();
                values.submitted_date = objODBCDatareader["submitted_date"].ToString();
                values.region = objODBCDatareader["region"].ToString();
                values.creditgroup_gid = objODBCDatareader["creditgroup_gid"].ToString();
                values.creditgroup_name = objODBCDatareader["creditgroup_name"].ToString();
                values.program_gid = objODBCDatareader["program_gid"].ToString();
                values.program_name = objODBCDatareader["program_name"].ToString();
                values.product_gid = objODBCDatareader["product_gid"].ToString();
                values.product_name = objODBCDatareader["product_name"].ToString();
                values.sector_name = objODBCDatareader["sector_name"].ToString();
                values.category_name = objODBCDatareader["category_name"].ToString();
                values.variety_gid = objODBCDatareader["variety_gid"].ToString();
                values.variety_name = objODBCDatareader["variety_name"].ToString();
                values.botanical_name = objODBCDatareader["botanical_name"].ToString();
                values.alternative_name = objODBCDatareader["alternative_name"].ToString();
                values.document_name = objODBCDatareader["document_name"].ToString();
                values.document_path = objODBCDatareader["document_path"].ToString();
                values.validityfrom_date = objODBCDatareader["validityfrom_date"].ToString();
                values.validityto_date = objODBCDatareader["validityto_date"].ToString();
                values.onboarding_status = objODBCDatareader["onboarding_status"].ToString();               
                values.productdesk_flag = objODBCDatareader["productdesk_flag"].ToString();
                values.productdesk_gid = objODBCDatareader["productdesk_gid"].ToString();
                values.productquery_status = objODBCDatareader["productquery_status"].ToString();
                values.contract_id = objODBCDatareader["contract_id"].ToString();
                values.buyeronboard_gid = objODBCDatareader["buyeronboard_gid"].ToString();
                values.social_capital = objODBCDatareader["social_capital"].ToString();
                values.trade_capital = objODBCDatareader["trade_capital"].ToString();
                values.overalllimit_amount = objODBCDatareader["overalllimit_amount"].ToString();
                values.calculationoveralllimit_validity = objODBCDatareader["calculationoveralllimit_validity"].ToString();
                values.producttype_gid = objODBCDatareader["producttype_gid"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = "select renewal_flag from agr_mst_tapplication a" +
                    " where buyeronboard_gid = '" + values.buyer_gid + "'" +
                    " and a.created_date = (select max(c.created_date) from agr_mst_tapplication c " +
                    " left join agr_mst_tapplication2loan b on c.application_gid = b.application_gid " +
                    " where c.buyeronboard_gid = '" + values.buyer_gid + "' and producttype_gid = '" + values.producttype_gid + "' and productsubtype_gid = '" + values.productsubtype_gid + "' )";
            string lslatesrenewal_flag = objdbconn.GetExecuteScalar(msSQL);
            if (lslatesrenewal_flag == "Y")
            {

                msSQL = " select a.application_no from agr_mst_tapplication a " +
                    " where buyeronboard_gid = '" + values.buyer_gid + "'" +
                    " and a.created_date = (select max(c.created_date) from agr_mst_tapplication c " +
                    " left join agr_mst_tapplication2loan b on c.application_gid = b.application_gid " +
                " where c.buyeronboard_gid = '" + values.buyer_gid + "' and producttype_gid = '" + values.producttype_gid + "' and productsubtype_gid =  '" + values.productsubtype_gid + "')";
                values.lslatestapplication_refno = objdbconn.GetExecuteScalar(msSQL);

            }
            else
            {
                msSQL = " select a.application_no from agr_mst_tapplication a " +
                       " where buyeronboard_gid = '" + values.buyer_gid + "'" +
                       " and a.created_date = (select max(c.created_date) from agr_mst_tapplication c " +
                       " left join agr_mst_tapplication2loan b on c.application_gid = b.application_gid " +
                   " where c.buyeronboard_gid = '" + values.buyer_gid + "' and producttype_gid = '" + values.producttype_gid + "' and productsubtype_gid =  '" + values.productsubtype_gid + "' and renewal_flag = 'Y')";
                values.lslatestapplication_refno = objdbconn.GetExecuteScalar(msSQL);
                if (values.lslatestapplication_refno == "")
                {
                    msSQL = " select a.application_no from agr_mst_tapplication a " +
                        " where a.processupdated_date = (select max(c.processupdated_date) from agr_mst_tapplication c " +
                        " left join agr_mst_tapplication2loan b on c.application_gid = b.application_gid " +
                        " where c.buyeronboard_gid = '" + values.buyer_gid + "' and " +
                        " c.onboarding_status = 'Proposal' and c.process_type = 'Accept' and producttype_gid = '" + values.producttype_gid + "' and productsubtype_gid =  '" + values.productsubtype_gid + "') ";
                values.lslatestapplication_refno = objdbconn.GetExecuteScalar(msSQL);
                }
            }

            msSQL = " select renewal_flag from agr_mst_tapplication where application_no='" + values.lslatestapplication_refno + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                values.applicationrenewal_flag = objODBCDataReader["renewal_flag"].ToString();
            }
            objODBCDataReader.Close();

            msSQL = "select vertical_refno from ocs_mst_tvertical where vertical_gid='" + values.vertical_gid + "'";
            string lsvertical_refno = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select entity_gid from ocs_mst_tvertical where vertical_gid='" + values.vertical_gid + "'";
            string lsentity_gid = objdbconn.GetExecuteScalar(msSQL);

            //msSQL = "select entity_code from adm_mst_tentity where entity_gid='" + lsentity_gid + "'";
            //string lsentity_code = objdbconn.GetExecuteScalar(msSQL);
            string lsentity_code = "SA";

            string lsapp_refno = "ARN" + lsentity_code + lsvertical_refno + DateTime.Now.ToString("ddMMyyyy");

            string msGETRef = objcmnfunctions.GetMasterGID("APP");
            msGETRef = msGETRef.Replace("APP", "");
            string finalInt = "";
            if (values.applicationrenewal_flag == "Y")
            {
                string pattern = @"[\d]{1}RN(.*)";
                Match m = Regex.Match(values.lslatestapplication_refno, pattern, RegexOptions.IgnoreCase);
                string toBeSearched = "RN";
                string tempString = m.Value.Substring(m.Value.IndexOf(toBeSearched) + toBeSearched.Length);

                //string tempString = m.Value.Replace("RN", String.Empty);
                int tempInt = Int16.Parse(tempString) + 1;
                if (tempInt >= 1 && tempInt <= 9)
                {
                    finalInt = tempInt.ToString().PadLeft(2, '0');
                }
                else
                {
                    finalInt = tempInt.ToString();
                }
            }
            else
            {
                finalInt = "01";
            }

            lsapp_refno = lsapp_refno + msGETRef + "RN" + finalInt;

            // Insert data in Application Table
            msGetGidApplication = objcmnfunctions.GetMasterGID("APPC");
            msSQL = " insert into agr_mst_tapplication( " +
                    " application_gid ," +
                    " application_no ," +
                    " refapplication_gid ," +
                    " refapplication_no ," +
                    " customer_urn ," +
                    " customer_name ," +
                    " entity_gid ," +
                    " entity_name ," +
                    " vertical_gid ," +
                    " vertical_name ," +
                    " verticaltaggs_gid ," +
                    " verticaltaggs_name ," +
                    " constitution_gid ," +
                    " constitution_name ," +
                    " businessunit_gid ," +
                    " businessunit_name ," +
                    " sa_status ," +
                    " sa_id ," +
                    " sa_name ," +
                    " baselocation_gid ," +
                    " baselocation_name ," +
                    " cluster_gid ," +
                    " cluster_name ," +
                    " region_gid ," +
                    " region_name ," +
                    " zone_gid ," +
                    " zone_name ," +
                    " relationshipmanager_name ," +
                    " relationshipmanager_gid ," +
                    " drm_gid ," +
                    " drm_name ," +
                    " clustermanager_gid ," +
                    " clustermanager_name ," +
                    " zonalhead_name ," +
                    " zonalhead_gid ," +
                    " regionalhead_name ," +
                    " regionalhead_gid ," +
                    " businesshead_name ," +
                    " businesshead_gid ," +
                    " vernacular_language ," +
                    " vernacularlanguage_gid ," +
                    " contactpersonfirst_name ," +
                    " contactpersonmiddle_name ," +
                    " contactpersonlast_name ," +
                    " designation_gid ," +
                    " designation_type ," +
                    " landline_no ," +
                    " social_capital ," +
                    " trade_capital ," +
                    " overalllimit_amount ," +
                    " calculationoveralllimit_validity ," +
                    " status ," +
                    " saname_gid ," +
                    " economical_flag ," +
                    " productcharge_flag ," +
                    " applicant_type ," +
                    " customerref_name ," +
                    " approval_status ," +
                    " mobile_no ," +
                    " email_address ," +
                    " approval_flag ," +
                    " gradingdraft_flag ," +
                    " hypothecation_flag ," +
                    " ccsubmit_flag ," +
                    " meeting_status ," +
                    " region ," +
                    " momapproval_flag ," +
                    " mom_flag ," +
                    " momdocumentupload_flag ," +
                    " headapproval_status ," +
                    " document_name ," +
                    " document_path ," +
                    " creditgroup_gid ," +
                    " creditgroup_name ," +
                    " creditgroup_status ," +
                    " creditheadapproval_status ," +
                    " program_gid ," +
                    " program_name ," +
                    " docchecklist_makerflag ," +
                    " docchecklist_checkerflag ," +
                    " docchecklist_approvalflag ," +
                    " cccompleted_flag ," +
                    " hierarchyupdated_flag ," +
                    " product_gid ," +
                    " product_name ," +
                    " sector_name ," +
                    " category_name ," +
                    " variety_gid ," +
                    " variety_name ," +
                    " botanical_name ," +
                    " alternative_name ," +
                    " pslcompleted_flag ," +
                    " sanction_approvalflag ," +
                    " renewal_flag," +
                    " validityfrom_date," +
                    " validityto_date," +
                    " onboarding_status," +
                    " productdesk_flag," +
                    " productdesk_gid," +
                    " productquery_status," +
                    //" contract_id," +
                    " buyeronboard_gid," +
                    " productcharges_status," +
                    " created_by , " +
                    " created_date ) " +
                    " values (" +
                   "'" + msGetGidApplication + "'," +
                   "'" + lsapp_refno + "'," +
                   "'" + values.application_gid + "'," +
                   "'" + values.application_no + "'," +
                   "'" + values.customer_urn + "'," +
                   "'" + values.customer_name + "'," +
                   "'" + values.entity_gid + "'," +
                   "'" + values.entity_name + "'," +
                   "'" + values.vertical_gid + "'," +
                   "'" + values.vertical_name + "'," +
                   "'" + values.verticaltaggs_gid + "'," +
                   "'" + values.verticaltaggs_name + "'," +
                   "'" + values.constitution_gid + "'," +
                   "'" + values.constitution_name + "'," +
                   "'" + values.businessunit_gid + "'," +
                   "'" + values.businessunit_name + "'," +
                   "'" + values.sastatus + "'," +
                   "'" + values.sa_id + "'," +
                   "'" + values.sa_name + "'," +
                   "'" + values.baselocation_gid + "'," +
                   "'" + values.baselocation_name + "'," +
                   "'" + values.cluster_gid + "'," +
                   "'" + values.cluster_name + "'," +
                   "'" + values.region_gid + "'," +
                   "'" + values.region_name + "'," +
                   "'" + values.zone_gid + "'," +
                   "'" + values.zone_name + "'," +
                   "'" + values.relationshipmanager_name + "'," +
                   "'" + values.relationshipmanager_gid + "'," +
                   "'" + values.drm_gid + "'," +
                   "'" + values.drm_name + "'," +
                   "'" + values.clustermanager_gid + "'," +
                   "'" + values.clustermanager_name + "'," +
                   "'" + values.zonalhead_name + "'," +
                   "'" + values.zonalhead_gid + "'," +
                   "'" + values.regionalhead_name + "'," +
                   "'" + values.regionalhead_gid + "'," +
                   "'" + values.businesshead_name + "'," +
                   "'" + values.businesshead_gid + "'," +
                   "'" + values.vernacular_language + "'," +
                   "'" + values.vernacularlanguage_gid + "'," +
                   "'" + values.contactpersonfirst_name + "'," +
                   "'" + values.contactpersonmiddle_name + "'," +
                   "'" + values.contactpersonlast_name + "'," +
                   "'" + values.designation_gid + "'," +
                   "'" + values.designation_type + "'," +
                   "'" + values.landline_no + "'," +
                   "'" + values.social_capital + "'," +
                   "'" + values.trade_capital + "'," +
                   "'" + values.overalllimit_amount + "'," +
                   "'" + values.calculationoveralllimit_validity + "'," +
                   "'Completed'," +
                   "'" + values.saname_gid + "'," +
                   "'Y'," +
                   "'Y'," +
                   "'" + values.applicant_type + "'," +
                   "'" + values.customerref_name + "'," +
                   "'Incomplete'," +
                   "'" + values.mobile_no + "'," +
                   "'" + values.email_address + "'," +
                   "'N'," +
                   "'N'," +
                   "'N'," +
                   "'N'," +
                   "'Pending'," +
                   "'" + values.region + "'," +
                   "'N'," +
                   "'N'," +
                   "'N'," +
                   "'Pending'," +
                   "'" + values.document_name + "'," +
                   "'" + values.document_path + "'," +
                   "'" + values.creditgroup_gid + "'," +
                   "'" + values.creditgroup_name + "'," +
                   "'Pending'," +
                   "'Pending'," +
                   "'" + values.program_gid + "'," +
                   "'" + values.program_name + "'," +
                   "'N', " +
                   "'N', " +
                   "'N', " +
                   "'N', " +
                   "'N'," +
                   "'" + values.product_gid + "'," +
                   "'" + values.product_name + "'," +
                   "'" + values.sector_name + "'," +
                   "'" + values.category_name + "'," +
                   "'" + values.variety_gid + "'," +
                   "'" + values.variety_name + "'," +
                   "'" + values.botanical_name + "'," +
                   "'" + values.alternative_name + "'," +
                   "'N'," +
                   "'N', " +
                   "'Y'," +
                   "'" + Convert.ToDateTime(values.validityfrom_date).ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                   "'" + Convert.ToDateTime(values.validityto_date).ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                   "'" + values.onboarding_status + "'," +
                   "'" + values.productdesk_flag + "'," +
                   "'" + values.productdesk_gid + "'," +
                   "'" + values.productquery_status + "'," +
                   //"'" + values.contract_id + "'," +
                   "'" + values.buyeronboard_gid + "'," +
                   "'" + values.productcharges_status + "'," +
                   "'" + employee_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResultapplicationcreation = objdbconn.ExecuteNonQuerySQL(msSQL);

           

            // Insert data in Onboardinitiate Detail Table
            msSQL = "select onboardinitiatedtl_gid from agr_mst_tonboardinitiatedtl where application_gid='" + values.application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msSQL = " insert into agr_mst_tonboardinitiatedtl " +
                            " (buyeronboard_gid, supplieronboard_gid, application_gid, product_gid, " +
                            " product_name, program_gid, program_name, initiated_remarks, renewal_flag, created_byname, " +
                            " created_by, created_date) " +
                            " select buyeronboard_gid,supplieronboard_gid, @application_gid:= '" + msGetGidApplication + "',product_gid, " +
                            " product_name, program_gid, program_name, initiated_remarks, @renewal_flag:= 'Y', created_byname, " +
                            " created_by, @created_date:= '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                            " from agr_mst_tonboardinitiatedtl " +
                            " where application_gid='" + values.application_gid + "'";
                    mnResultonboardinitiate = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            else
            {
                mnResultonboardinitiate = 1;
            }

            // Insert data in Application2Contact Table
            msSQL = "select application2contact_gid from agr_mst_tapplication2contactno where application_gid='" + values.application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msGetGidContactno = objcmnfunctions.GetMasterGID("A2CN");
                    msSQL = " insert into agr_mst_tapplication2contactno " +
                            " (application2contact_gid, application_gid, mobile_no, primary_mobileno, whatsapp_mobileno, created_by," +
                            " created_date, updated_by, updated_date) " +
                            " select @application2contact_gid := '" + msGetGidContactno + "', @application_gid:= '" + msGetGidApplication + "', mobile_no, " +
                            " primary_mobileno,whatsapp_mobileno,created_by,created_date, updated_by," +
                            " updated_date from agr_mst_tapplication2contactno " +
                            " where application_gid='" + values.application_gid + "'" +
                            " and application2contact_gid='" + dt["application2contact_gid"].ToString() + "'";
                    mnResultapplicationcontact = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            else
            {
                mnResultapplicationcontact = 1;
            }

            // Insert data in Application2Email Table
            msSQL = "select application2email_gid from agr_mst_tapplication2email where application_gid='" + values.application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msGetGidemail = objcmnfunctions.GetMasterGID("A2EA");
                    msSQL = " insert into agr_mst_tapplication2email " +
                            " (application2email_gid, application_gid, email_address, primary_emailaddress,created_by," +
                            " created_date, updated_by, updated_date) " +
                            " select @application2email_gid := '" + msGetGidemail + "', @application_gid:= '" + msGetGidApplication + "', email_address, " +
                            " primary_emailaddress,created_by,created_date, updated_by," +
                            " updated_date from agr_mst_tapplication2email " +
                            " where application_gid='" + values.application_gid + "'" +
                            " and application2email_gid='" + dt["application2email_gid"].ToString() + "'";
                    mnResultapplicationemail = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            else
            {
                mnResultapplicationemail = 1;
            }

            // Insert data in Application2GeneticCode Table from CAD Table
            msSQL = "select application2geneticcode_gid from agr_mst_tapplication2geneticcode where application_gid='" + values.application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msGetGidgeneticcode = objcmnfunctions.GetMasterGID("A2GC");
                    msSQL = " insert into agr_mst_tapplication2geneticcode " +
                            " (application2geneticcode_gid, application_gid, geneticcode_gid, geneticcode_name,genetic_status,genetic_remarks," +
                            " created_by, created_date, updated_by, updated_date) " +
                            " select @application2geneticcode_gid := '" + msGetGidgeneticcode + "', @application_gid:= '" + msGetGidApplication + "',  " +
                            " geneticcode_gid, geneticcode_name,genetic_status,genetic_remarks,created_by,created_date, updated_by," +
                            " updated_date from agr_mst_tapplication2geneticcode " +
                            " where application_gid='" + values.application_gid + "' " +
                            " and application2geneticcode_gid='" + dt["application2geneticcode_gid"].ToString() + "'";
                    mnResultapplicationgeneticcode = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            else
            {
                mnResultapplicationgeneticcode = 1;
            }
            // Insert data in Institution Table 
            msSQL = "select institution_gid from agr_mst_tinstitution where application_gid='" + values.application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msGetGidinstitution = objcmnfunctions.GetMasterGID("APIN");
                    msSQL = " insert into agr_mst_tinstitution " +
                            " (institution_gid,application_gid,application_no,company_name,date_incorporation," +
                            " form60document_name,form60document_path,companypan_no,year_business,month_business,cin_no,official_telephoneno," +
                            " officialemail_address,companytype_gid,companytype_name,stakeholder_type,stakeholdertype_gid,assessmentagency_gid," +
                            " assessmentagency_name,assessmentagencyrating_gid,assessmentagencyrating_name,ratingas_on,amlcategory_gid,amlcategory_name," +
                            " businesscategory_gid,businesscategory_name,contactperson_firstname,contactperson_middlename,contactperson_lastname," +
                            " contactperson_contactno,designation_gid,designation,lastyear_turnover,escrow,start_date,end_date,created_by," +
                            " created_date,updated_date,updated_by,urn,urn_status,observations,bureau_response,institution_status," +
                            " businessstart_date,bureauname_name,bureau_score,bureauname_gid,bureauscore_date,cicdocument_name,cicdocument_path," +
                            " mobile_no,email_address,economical_status,social_capital,trade_capital,official_mailid,economical_flag," +
                            " startupasofloansanction_date,occupation_gid,occupation,lineofactivity_gid,lineofactivity,bsrcode_gid,bsrcode," +
                            " pslcategory_gid,pslcategory,weakersection_gid,weakersection,pslpurpose_gid,pslpurpose,totalsanction_financialinstitution," +
                            " pslsanction_limit,natureofentity_gid,natureofentity,indulgeinmarketing_activity,plantandmachineryinvestment_gid," +
                            " plantandmachineryinvestment,turnover_gid,turnover,msmeclassification_gid,msmeclassification,loansanction_date," +
                            " entityincorporation_date,hq_metropolitancity,clientdtl_gid,client_dtl,psltagging_flag,credit_status,tan_number," +
                            " sundrydebt_adv,fixed_assets,profit,revenue,incometax_returnsstatus) " +
                            " select @institution_gid := '" + msGetGidinstitution + "', @application_gid:= '" + msGetGidApplication + "', application_no,company_name,date_incorporation, " +
                            " form60document_name,form60document_path,companypan_no,year_business,month_business,cin_no,official_telephoneno," +
                            " officialemail_address,companytype_gid,companytype_name,stakeholder_type,stakeholdertype_gid,assessmentagency_gid," +
                            " assessmentagency_name,assessmentagencyrating_gid,assessmentagencyrating_name,ratingas_on,amlcategory_gid,amlcategory_name," +
                            " businesscategory_gid,businesscategory_name,contactperson_firstname,contactperson_middlename,contactperson_lastname," +
                            " contactperson_contactno,designation_gid,designation,lastyear_turnover,escrow,start_date,end_date,created_by," +
                            " created_date,updated_date,updated_by,urn,urn_status,observations,bureau_response,institution_status," +
                            " businessstart_date,bureauname_name,bureau_score,bureauname_gid,bureauscore_date,cicdocument_name,cicdocument_path," +
                            " mobile_no,email_address,economical_status,social_capital,trade_capital,official_mailid,economical_flag," +
                            " startupasofloansanction_date,occupation_gid,occupation,lineofactivity_gid,lineofactivity,bsrcode_gid,bsrcode," +
                            " pslcategory_gid,pslcategory,weakersection_gid,weakersection,pslpurpose_gid,pslpurpose,totalsanction_financialinstitution," +
                            " pslsanction_limit,natureofentity_gid,natureofentity,indulgeinmarketing_activity,plantandmachineryinvestment_gid," +
                            " plantandmachineryinvestment,turnover_gid,turnover,msmeclassification_gid,msmeclassification,loansanction_date," +
                            " entityincorporation_date,hq_metropolitancity,clientdtl_gid,client_dtl,psltagging_flag,credit_status,tan_number," +
                            " sundrydebt_adv,fixed_assets,profit,revenue,incometax_returnsstatus from agr_mst_tinstitution " +
                            " where application_gid='" + values.application_gid + "' " +
                            " and institution_gid='" + dt["institution_gid"].ToString() + "'";
                    mnResultinstitution = objdbconn.ExecuteNonQuerySQL(msSQL);

                    // Insert data in Institution Branch Table from CAD Table
                    msSQL = "select institution2branch_gid from agr_mst_tinstitution2branch where institution_gid='" + dt["institution_gid"].ToString() + "'";
                    dt_datatable2 = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable2.Rows.Count != 0)
                    {
                        foreach (DataRow dt2 in dt_datatable2.Rows)
                        {
                            msGetGidinstitutionbranch = objcmnfunctions.GetMasterGID("ITGS");
                            msSQL = " insert into ocs_mst_tinstitution2branch " +
                                    " (institution2branch_gid, institution_gid, " +
                                    " gst_registered, gst_state, gst_no, created_by, created_date, updated_date, updated_by, gststate_gid, status, " +
                                    " authentication_status, returnfilling_status, verification_status )" +
                                    " select @institution2branch_gid := '" + msGetGidinstitutionbranch + "', @institution_gid:= '" + msGetGidinstitution + "',  " +
                                    " gst_registered, gst_state, gst_no, created_by, created_date, updated_date, updated_by, gststate_gid, status, " +
                                    " authentication_status, returnfilling_status, verification_status " +
                                    " from ocs_mst_tinstitution2branch " +
                                    " where institution_gid='" + dt["institution_gid"].ToString() + "' " +
                                    " and institution2branch_gid='" + dt2["institution2branch_gid"].ToString() + "'";
                            mnResultinstitutionbranch = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    else
                    {
                        mnResultinstitutionbranch = 1;
                    }

                    // Insert data in Institution Mobile Table 
                    msSQL = "select institution2mobileno_gid from agr_mst_tinstitution2mobileno where institution_gid='" + dt["institution_gid"].ToString() + "'";
                    dt_datatable3 = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable3.Rows.Count != 0)
                    {
                        foreach (DataRow dt3 in dt_datatable3.Rows)
                        {
                            msGetGidinstitutionmobile = objcmnfunctions.GetMasterGID("IT2M");
                            msSQL = " insert into agr_mst_tinstitution2mobileno " +
                                    " (institution2mobileno_gid, institution_gid, mobile_no, primary_status, whatsapp_no, landline_no, created_by, created_date, updated_date, updated_by ) " +
                                    " select @institution2mobileno_gid := '" + msGetGidinstitutionmobile + "', @institution_gid:= '" + msGetGidinstitution + "',  " +
                                    "  mobile_no, primary_status, whatsapp_no, landline_no, created_by, created_date, updated_date, updated_by " +
                                    " from agr_mst_tinstitution2mobileno " +
                                    " where institution_gid='" + dt["institution_gid"].ToString() + "' " +
                                    " and institution2mobileno_gid='" + dt3["institution2mobileno_gid"].ToString() + "'";
                            mnResultinstitutionmobile = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    else
                    {
                        mnResultinstitutionmobile = 1;
                    }

                    // Insert data in Institution Email Table 
                    msSQL = "select institution2email_gid from agr_mst_tinstitution2email where institution_gid='" + dt["institution_gid"].ToString() + "'";
                    dt_datatable4 = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable4.Rows.Count != 0)
                    {
                        foreach (DataRow dt4 in dt_datatable4.Rows)
                        {
                            msGetGidinstitutionemail = objcmnfunctions.GetMasterGID("IT2E");
                            msSQL = " insert into agr_mst_tinstitution2email " +
                                    " (institution2email_gid, institution_gid, email_address, primary_status, created_by, created_date, updated_date, updated_by) " +
                                    " select @institution2email_gid := '" + msGetGidinstitutionemail + "', @institution_gid:= '" + msGetGidinstitution + "',  " +
                                    " email_address, primary_status, created_by, created_date, updated_date, updated_by " +
                                    " from agr_mst_tinstitution2email " +
                                    " where institution_gid='" + dt["institution_gid"].ToString() + "' " +
                                    " and institution2email_gid='" + dt4["institution2email_gid"].ToString() + "'";
                            mnResultinstitutionemail = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    else
                    {
                        mnResultinstitutionemail = 1;
                    }

                    // Insert data in Institution Address Table
                    msSQL = "select institution2address_gid from agr_mst_tinstitution2address where institution_gid='" + dt["institution_gid"].ToString() + "'";
                    dt_datatable5 = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable5.Rows.Count != 0)
                    {
                        foreach (DataRow dt5 in dt_datatable5.Rows)
                        {
                            msGetGidinstitutionaddress = objcmnfunctions.GetMasterGID("IT2A");
                            msSQL = " insert into agr_mst_tinstitution2address " +
                                    " (institution2address_gid, institution_gid, addresstype_gid, addresstype_name, primary_status, addressline1, addressline2, landmark, postal_code, city, " +
                                    " taluka, district, state, country, latitude, longitude, created_by, created_date, updated_date, updated_by) " +
                                    " select @institution2address_gid := '" + msGetGidinstitutionaddress + "', @institution_gid:= '" + msGetGidinstitution + "',  " +
                                    " addresstype_gid, addresstype_name, primary_status, addressline1, addressline2, landmark, postal_code, city, " +
                                    " taluka, district, state, country, latitude, longitude, created_by, created_date, updated_date, updated_by " +
                                    " from agr_mst_tinstitution2address " +
                                    " where institution_gid='" + dt["institution_gid"].ToString() + "' " +
                                    " and institution2address_gid='" + dt5["institution2address_gid"].ToString() + "'";
                            mnResultinstitutionaddress = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    else
                    {
                        mnResultinstitutionaddress = 1;
                    }
                    // Insert data in Institution License Detail Table
                    msSQL = "select institution2licensedtl_gid from agr_mst_tinstitution2licensedtl where institution_gid='" + dt["institution_gid"].ToString() + "'";
                    dt_datatable6 = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable6.Rows.Count != 0)
                    {
                        foreach (DataRow dt6 in dt_datatable6.Rows)
                        {
                            msGetGidinstitutionlicense = objcmnfunctions.GetMasterGID("IT2L");
                            msSQL = " insert into agr_mst_tinstitution2licensedtl " +
                                    " (institution2licensedtl_gid, institution_gid, licensetype_gid, licensetype_name, license_no, issue_date, expiry_date, created_by, created_date, updated_date, updated_by) " +
                                    " select @institution2licensedtl_gid := '" + msGetGidinstitutionlicense + "', @institution_gid:= '" + msGetGidinstitution + "',  " +
                                    " licensetype_gid, licensetype_name, license_no, issue_date, expiry_date, created_by, created_date, updated_date, updated_by" +
                                    " from agr_mst_tinstitution2licensedtl " +
                                    " where institution_gid='" + dt["institution_gid"].ToString() + "' " +
                                    " and institution2licensedtl_gid='" + dt6["institution2licensedtl_gid"].ToString() + "'";
                            mnResultinstitutionlicense = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    else
                    {
                        mnResultinstitutionlicense = 1;
                    }

                    // Insert data in Institution Document Table
                    msSQL = "select institution2documentupload_gid from agr_mst_tinstitution2documentupload where institution_gid='" + dt["institution_gid"].ToString() + "'";
                    dt_datatable7 = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable7.Rows.Count != 0)
                    {
                        foreach (DataRow dt7 in dt_datatable7.Rows)
                        {
                            msGetGidinstitutiondocument = objcmnfunctions.GetMasterGID("INDO");
                            msSQL = " insert into agr_mst_tinstitution2documentupload " +
                                    " ( institution2documentupload_gid, institution_gid, " +
                                    " document_name, document_path, created_by, created_date, updated_date, updated_by, companydocument_gid, document_title, " +
                                    " document_id, covenant_type, untagged_type, untagged_by, untagged_date, covenant_periods, covenantperiod_updatedby, covenantperiod_updateddate, documenttype_gid, documenttype_name) " +
                                    " select @institution2documentupload_gid := '" + msGetGidinstitutiondocument + "', @institution_gid:= '" + msGetGidinstitution + "',  " +
                                    " document_name, document_path, created_by, created_date, updated_date, updated_by, companydocument_gid, document_title, " +
                                    " document_id, covenant_type, untagged_type, untagged_by, untagged_date, covenant_periods, covenantperiod_updatedby, covenantperiod_updateddate, documenttype_gid, documenttype_name " +
                                    " from agr_mst_tinstitution2documentupload " +
                                    " where institution_gid='" + dt["institution_gid"].ToString() + "' " +
                                    " and institution2documentupload_gid='" + dt7["institution2documentupload_gid"].ToString() + "'";
                            mnResultinstitutiondocument = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    else
                    {
                        mnResultinstitutiondocument = 1;
                    }


                    // Insert data in Institution Rating Details Table
                    msSQL = "select institution2ratingdetail_gid from agr_mst_tinstitution2ratingdetail where institution_gid='" + dt["institution_gid"].ToString() + "'";
                    dt_datatable8 = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable8.Rows.Count != 0)
                    {
                        foreach (DataRow dt8 in dt_datatable8.Rows)
                        {
                            msGetGidinstitutionratingdtl = objcmnfunctions.GetMasterGID("INRD");
                            msSQL = " insert into agr_mst_tinstitution2ratingdetail " +
                                    " ( institution2ratingdetail_gid, institution_gid,application_gid, " +
                                    " creditrating_agencygid, creditrating_agencyname, creditrating_gid, creditrating_name, assessed_on, creditrating_link, created_by, created_date, updated_by, updated_date ) " +
                                    " select @institution2ratingdetail_gid := '" + msGetGidinstitutionratingdtl + "', @institution_gid:= '" + msGetGidinstitution + "', @application_gid:= '" + msGetGidApplication + "', " +
                                    "  creditrating_agencygid, creditrating_agencyname, creditrating_gid, creditrating_name, assessed_on, creditrating_link, created_by, created_date, updated_by, updated_date" +
                                    " from agr_mst_tinstitution2ratingdetail " +
                                    " where institution_gid='" + dt["institution_gid"].ToString() + "' " +
                                    " and institution2ratingdetail_gid='" + dt8["institution2ratingdetail_gid"].ToString() + "'";
                            mnResultinstitutionratingdtl = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    else
                    {
                        mnResultinstitutionratingdtl = 1;
                    }

                    // Insert data in Institution Bank Details Table 
                    msSQL = "select institution2bankdtl_gid from agr_mst_tinstitution2bankdtl where institution_gid='" + dt["institution_gid"].ToString() + "'";
                    dt_datatable9 = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable9.Rows.Count != 0)
                    {
                        foreach (DataRow dt9 in dt_datatable9.Rows)
                        {
                            msGetGidinstitutionbankdtl = objcmnfunctions.GetMasterGID("I2BD");
                            msSQL = " insert into agr_mst_tinstitution2bankdtl " +
                                    " (institution2bankdtl_gid, institution_gid, application_gid, " +
                                    " bankaccount_number, ifsc_code, bank_name, branch_name, micr_code, accountholder_name, accounttype_gid, accounttype_name, " +
                                    " joint_account, jointaccountholder_name, chequebookfacility_available, accountopen_date, created_by, created_date, " +
                                    " updated_by, updated_date, bank_address, bankaccount_name, bankaccounttype_gid, bankaccounttype_name, confirmbankaccountnumber, " +
                                    " joinaccount_status, joinaccount_name, chequebook_status, disbursement_accountstatus, primary_status ) " +
                                    " select @institution2bankdtl_gid := '" + msGetGidinstitutionbankdtl + "', @institution_gid:= '" + msGetGidinstitution + "', @application_gid:= '" + msGetGidApplication + "', " +
                                    " bankaccount_number, ifsc_code, bank_name, branch_name, micr_code, accountholder_name, accounttype_gid, accounttype_name, " +
                                    " joint_account, jointaccountholder_name, chequebookfacility_available, accountopen_date, created_by, created_date, " +
                                    " updated_by, updated_date, bank_address, bankaccount_name, bankaccounttype_gid, bankaccounttype_name, confirmbankaccountnumber, " +
                                    " joinaccount_status, joinaccount_name, chequebook_status, disbursement_accountstatus, primary_status " +
                                    " from agr_mst_tinstitution2bankdtl " +
                                    " where institution_gid='" + dt["institution_gid"].ToString() + "' " +
                                    " and institution2bankdtl_gid='" + dt9["institution2bankdtl_gid"].ToString() + "'";
                            mnResultinstitutionbankdtl = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    else
                    {
                        mnResultinstitutionbankdtl = 1;
                    }

                }
            }
            else
            {
                mnResultinstitution = 1;
                mnResultinstitutionbranch = 1;
                mnResultinstitutionmobile = 1;
                mnResultinstitutionemail = 1;
                mnResultinstitutionaddress = 1;
                mnResultinstitutionlicense = 1;
                mnResultinstitutiondocument = 1;
                mnResultinstitutionratingdtl = 1;
                mnResultinstitutionbankdtl = 1;

            }
            // Insert data in Contact Table
            msSQL = "select contact_gid from agr_mst_tcontact where application_gid='" + values.application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msGetGidcontact = objcmnfunctions.GetMasterGID("CTCT");
                    msSQL = " insert into agr_mst_tcontact " +
                            " (contact_gid,application_gid,application_no,pan_status,pan_no,aadhar_no, " +
                            " first_name,last_name,middle_name,mother_lastname,individual_dob,age,gender_gid,gender_name," +
                            "designation_gid,designation_name,educationalqualification_gid,educationalqualification_name,main_occupation," +
                            "annual_income,monthly_income,pep_status,pepverified_date,stakeholdertype_gid,stakeholder_type,user_type," +
                            "maritalstatus_gid,maritalstatus_name,father_firstname,father_middlename,father_lastname,father_dob,father_age," +
                            "mother_firstname,mother_middlename,mother_dob,mother_age,spouse_firstname,spouse_middlename,spouse_lastname," +
                            "spouse_dob,spouse_age,ownershiptype_gid,ownershiptype_name,propertyholder_gid,propertyholder_name," +
                            "residencetype_gid,residencetype_name,currentresidence_years,branch_distance,bureauname_gid,bureauname_name," +
                            "bureau_score,bureauscore_date,observations,bureau_response,cicdocument_name,cicdocument_path,created_by," +
                            "created_date,updated_date,updated_by,contact_status,email_address,mobile_no,incometype_gid,incometype_name," +
                            "group_gid,group_name,profile,urn_status,urn,fathernominee_status,mothernominee_status,spousenominee_status," +
                            "othernominee_status,relationshiptype,nomineefirst_name,nominee_middlename,nominee_lastname,nominee_dob," +
                            "nominee_age,totallandinacres,cultivatedland,previouscrop,prposedcrop,economical_status,social_capital," +
                            "trade_capital,institution_gid,institution_name,startupasofloansanction_date,occupation_gid,occupation," +
                            "lineofactivity_gid,lineofactivity,bsrcode_gid,bsrcode,pslcategory_gid,pslcategory,weakersection_gid,weakersection," +
                            "pslpurpose_gid,pslpurpose,totalsanction_financialinstitution,pslsanction_limit,natureofentity_gid,natureofentity," +
                            "indulgeinmarketing_activity,plantandmachineryinvestment_gid,plantandmachineryinvestment,turnover_gid,turnover," +
                            "msmeclassification_gid,msmeclassification,loansanction_date,entityincorporation_date,hq_metropolitancity,clientdtl_gid," +
                            "client_dtl,economical_flag,psltagging_flag,credit_status)" +
                            " select @contact_gid := '" + msGetGidcontact + "', @application_gid:= '" + msGetGidApplication + "',  " +
                            " application_no,pan_status,pan_no,aadhar_no, " +
                            " first_name,last_name,middle_name,mother_lastname,individual_dob,age,gender_gid,gender_name," +
                            "designation_gid,designation_name,educationalqualification_gid,educationalqualification_name,main_occupation," +
                            "annual_income,monthly_income,pep_status,pepverified_date,stakeholdertype_gid,stakeholder_type,user_type," +
                            "maritalstatus_gid,maritalstatus_name,father_firstname,father_middlename,father_lastname,father_dob,father_age," +
                            "mother_firstname,mother_middlename,mother_dob,mother_age,spouse_firstname,spouse_middlename,spouse_lastname," +
                            "spouse_dob,spouse_age,ownershiptype_gid,ownershiptype_name,propertyholder_gid,propertyholder_name," +
                            "residencetype_gid,residencetype_name,currentresidence_years,branch_distance,bureauname_gid,bureauname_name," +
                            "bureau_score,bureauscore_date,observations,bureau_response,cicdocument_name,cicdocument_path,created_by," +
                            "created_date,updated_date,updated_by,contact_status,email_address,mobile_no,incometype_gid,incometype_name," +
                            "group_gid,group_name,profile,urn_status,urn,fathernominee_status,mothernominee_status,spousenominee_status," +
                            "othernominee_status,relationshiptype,nomineefirst_name,nominee_middlename,nominee_lastname,nominee_dob," +
                            "nominee_age,totallandinacres,cultivatedland,previouscrop,prposedcrop,economical_status,social_capital," +
                            "trade_capital,institution_gid,institution_name,startupasofloansanction_date,occupation_gid,occupation," +
                            "lineofactivity_gid,lineofactivity,bsrcode_gid,bsrcode,pslcategory_gid,pslcategory,weakersection_gid,weakersection," +
                            "pslpurpose_gid,pslpurpose,totalsanction_financialinstitution,pslsanction_limit,natureofentity_gid,natureofentity," +
                            "indulgeinmarketing_activity,plantandmachineryinvestment_gid,plantandmachineryinvestment,turnover_gid,turnover," +
                            "msmeclassification_gid,msmeclassification,loansanction_date,entityincorporation_date,hq_metropolitancity,clientdtl_gid," +
                            "client_dtl,economical_flag,psltagging_flag,credit_status " +
                            " from agr_mst_tcontact " +
                            " where application_gid='" + values.application_gid + "' " +
                            " and contact_gid='" + dt["contact_gid"].ToString() + "'";
                    mnResultcontact = objdbconn.ExecuteNonQuerySQL(msSQL);

                    // Insert data in Contact Pan Absence Reason Table
                    msSQL = "select contact2panabsencereason_gid from agr_mst_tcontact2panabsencereason where contact_gid='" + dt["contact_gid"].ToString() + "'";
                    dt_datatable2 = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable2.Rows.Count != 0)
                    {
                        foreach (DataRow dt2 in dt_datatable2.Rows)
                        {
                            msGetGidcontactpanreason = objcmnfunctions.GetMasterGID("C2PR");
                            msSQL = " insert into agr_mst_tcontact2panabsencereason " +
                                    " (contact2panabsencereason_gid, contact_gid, panabsencereason, created_by, created_date, updated_date, updated_by) " +
                                    " select @contact2panabsencereason_gid := '" + msGetGidcontactpanreason + "', @contact_gid:= '" + msGetGidcontact + "',  " +
                                    " panabsencereason, created_by, created_date, updated_date, updated_by " +
                                    " from agr_mst_tcontact2panabsencereason " +
                                    " where contact_gid='" + dt["contact_gid"].ToString() + "' " +
                                    " and contact2panabsencereason_gid='" + dt2["contact2panabsencereason_gid"].ToString() + "'";
                            mnResultcontactpanabsencereason = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    else
                    {
                        mnResultcontactpanabsencereason = 1;
                    }

                    // Insert data in Contact Pan Form-60 Document Table 
                    msSQL = "select contact2panform60_gid from agr_mst_tcontact2panform60 where contact_gid='" + dt["contact_gid"].ToString() + "'";
                    dt_datatable3 = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable3.Rows.Count != 0)
                    {
                        foreach (DataRow dt3 in dt_datatable3.Rows)
                        {
                            msGetGidcontactpanform60 = objcmnfunctions.GetMasterGID("CF60");
                            msSQL = " insert into agr_mst_tcontact2panform60 " +
                                    " (contact2panform60_gid, contact_gid, document_name, document_path, created_by, created_date, updated_date, updated_by) " +
                                    " select @contact2panform60_gid := '" + msGetGidcontactpanform60 + "', @contact_gid:= '" + msGetGidcontact + "',  " +
                                    " document_name, document_path, created_by, created_date, updated_date, updated_by " +
                                    " from agr_mst_tcontact2panform60 " +
                                    " where contact_gid='" + dt["contact_gid"].ToString() + "' " +
                                    " and contact2panform60_gid='" + dt3["contact2panform60_gid"].ToString() + "'";
                            mnResultcontactpanform60 = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    else
                    {
                        mnResultcontactpanform60 = 1;
                    }

                    // Insert data in Contact Mobile Number Table
                    msSQL = "select contact2mobileno_gid from agr_mst_tcontact2mobileno where contact_gid='" + dt["contact_gid"].ToString() + "'";
                    dt_datatable4 = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable4.Rows.Count != 0)
                    {
                        foreach (DataRow dt4 in dt_datatable4.Rows)
                        {
                            msGetGidcontactmobile = objcmnfunctions.GetMasterGID("C2MN");
                            msSQL = " insert into agr_mst_tcontact2mobileno " +
                                    " (contact2mobileno_gid, contact_gid, mobile_no, primary_status, whatsapp_no, landline_no, " +
                                    " created_by, created_date, updated_date, updated_by ) " +
                                    " select @contact2mobileno_gid := '" + msGetGidcontactmobile + "', @contact_gid:= '" + msGetGidcontact + "',  " +
                                    " mobile_no, primary_status, whatsapp_no, landline_no, " +
                                    " created_by, created_date, updated_date, updated_by " +
                                    " from agr_mst_tcontact2mobileno " +
                                    " where contact_gid='" + dt["contact_gid"].ToString() + "' " +
                                    " and contact2mobileno_gid='" + dt4["contact2mobileno_gid"].ToString() + "'";
                            mnResultcontactmobile = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    else
                    {
                        mnResultcontactmobile = 1;
                    }

                    // Insert data in Contact Email Table
                    msSQL = "select contact2email_gid from agr_mst_tcontact2email where contact_gid='" + dt["contact_gid"].ToString() + "'";
                    dt_datatable5 = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable5.Rows.Count != 0)
                    {
                        foreach (DataRow dt5 in dt_datatable5.Rows)
                        {
                            msGetGidcontactemail = objcmnfunctions.GetMasterGID("C2EA");
                            msSQL = " insert into agr_mst_tcontact2email " +
                                    " (contact2email_gid, contact_gid, email_address, primary_status, created_by, created_date, updated_date, updated_by ) " +
                                    " select @contact2email_gid := '" + msGetGidcontactemail + "', @contact_gid:= '" + msGetGidcontact + "',  " +
                                    " email_address, primary_status, created_by, created_date, updated_date, updated_by " +
                                    " from agr_mst_tcontact2email " +
                                    " where contact_gid='" + dt["contact_gid"].ToString() + "' " +
                                    " and contact2email_gid='" + dt5["contact2email_gid"].ToString() + "'";
                            mnResultcontactemail = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    else
                    {
                        mnResultcontactemail = 1;
                    }
                    // Insert data in Contact ID Proof Table 
                    msSQL = "select contact2idproof_gid from agr_mst_tcontact2idproof where contact_gid='" + dt["contact_gid"].ToString() + "'";
                    dt_datatable6 = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable6.Rows.Count != 0)
                    {
                        foreach (DataRow dt6 in dt_datatable6.Rows)
                        {
                            msGetGidcontactidproof = objcmnfunctions.GetMasterGID("C2IP");
                            msSQL = " insert into agr_mst_tcontact2idproof " +
                                    " (contact2idproof_gid, contact_gid, idproof_gid, idproof_name, idproof_no, idproof_dob, file_no, document_name, " +
                                    " document_path, created_by, created_date, updated_date, updated_by ) " +
                                    " select @contact2idproof_gid := '" + msGetGidcontactidproof + "', @contact_gid:= '" + msGetGidcontact + "',  " +
                                    " idproof_gid, idproof_name, idproof_no, idproof_dob, file_no, document_name, " +
                                    " document_path, created_by, created_date, updated_date, updated_by " +
                                    " from agr_mst_tcontact2idproof " +
                                    " where contact_gid='" + dt["contact_gid"].ToString() + "' " +
                                    " and contact2idproof_gid='" + dt6["contact2idproof_gid"].ToString() + "'";
                            mnResultcontactidproof = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    else
                    {
                        mnResultcontactidproof = 1;
                    }

                    // Insert data in Contact Address Table
                    msSQL = "select contact2address_gid from agr_mst_tcontact2address where contact_gid='" + dt["contact_gid"].ToString() + "'";
                    dt_datatable7 = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable7.Rows.Count != 0)
                    {
                        foreach (DataRow dt7 in dt_datatable7.Rows)
                        {
                            msGetGidcontactaddress = objcmnfunctions.GetMasterGID("C2AD");
                            msSQL = " insert into agr_mst_tcontact2address " +
                                    " (contact2address_gid, contact_gid, addresstype_gid, addresstype_name, primary_status, addressline1, " +
                                    " addressline2, landmark, postal_code, city, taluka, district, state, country, latitude, longitude, " +
                                    " created_by, created_date, updated_date, updated_by ) " +
                                    " select @contact2address_gid := '" + msGetGidcontactaddress + "', @contact_gid:= '" + msGetGidcontact + "',  " +
                                    " addresstype_gid, addresstype_name, primary_status, addressline1, " +
                                    " addressline2, landmark, postal_code, city, taluka, district, state, country, latitude, longitude, " +
                                    " created_by, created_date, updated_date, updated_by" +
                                    " from agr_mst_tcontact2address " +
                                    " where contact_gid='" + dt["contact_gid"].ToString() + "' " +
                                    " and contact2address_gid='" + dt7["contact2address_gid"].ToString() + "'";
                            mnResultcontactaddress = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    else
                    {
                        mnResultcontactaddress = 1;
                    }

                    // Insert data in Contact Document Table
                    msSQL = "select contact2document_gid from agr_mst_tcontact2document where contact_gid='" + dt["contact_gid"].ToString() + "'";
                    dt_datatable8 = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable8.Rows.Count != 0)
                    {
                        foreach (DataRow dt8 in dt_datatable8.Rows)
                        {
                            msGetGidcontactdocument = objcmnfunctions.GetMasterGID("C2DO");
                            msSQL = " insert into agr_mst_tcontact2document " +
                                    " (contact2document_gid, contact_gid, individualdocument_gid, document_gid, document_title, document_name, document_path, " +
                                    " created_by, created_date, updated_date, updated_by, covenant_type, untagged_type, untagged_by, untagged_date, " +
                                    " covenant_periods, covenantperiod_updatedby, covenantperiod_updateddate, documenttype_gid, documenttype_name) " +
                                    " select @contact2document_gid := '" + msGetGidcontactdocument + "', @contact_gid:= '" + msGetGidcontact + "',  " +
                                    " individualdocument_gid, document_gid, document_title, document_name, document_path, " +
                                    " created_by, created_date, updated_date, updated_by, covenant_type, untagged_type, untagged_by, untagged_date, " +
                                    " covenant_periods, covenantperiod_updatedby, covenantperiod_updateddate , documenttype_gid, documenttype_name " +
                                    " from agr_mst_tcontact2document " +
                                    " where contact_gid='" + dt["contact_gid"].ToString() + "' " +
                                    " and contact2document_gid='" + dt8["contact2document_gid"].ToString() + "'";
                            mnResultcontactdocument = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    else
                    {
                        mnResultcontactdocument = 1;
                    }
                }
            }

            else
            {
                mnResultcontact = 1;
                mnResultcontactpanabsencereason = 1;
                mnResultcontactpanform60 = 1;
                mnResultcontactmobile = 1;
                mnResultcontactemail = 1;
                mnResultcontactidproof = 1;
                mnResultcontactaddress = 1;
                mnResultcontactdocument = 1;
            }

            msSQL = " select contact_gid, individualdocument_gid, contact2document_gid from agr_mst_tcontact2document where contact_gid in " +
                    " (select contact_gid from agr_mst_tcontact where application_gid='" + msGetGidApplication + "')";
            dt_datatable21 = objdbconn.GetDataTable(msSQL);
            foreach (DataRow dt in dt_datatable21.Rows)
            {
                string lscovenant_type = "", lsdocumenttype_gid = "", lsdocumenttype_name = "", lscompanydocument_name = "";

                string msGetdefDocchecklistGID = objcmnfunctions.GetMasterGID("DOCG");
                msSQL = " select individualdocument_gid,documenttypes_gid,documenttype_name,individualdocument_name,covenant_type " +
                        " from ocs_mst_tindividualdocument where individualdocument_gid='" + dt["individualdocument_gid"].ToString() + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lsdocumenttype_gid = objODBCDatareader["documenttypes_gid"].ToString();
                    lsdocumenttype_name = objODBCDatareader["documenttype_name"].ToString();
                    lscompanydocument_name = objODBCDatareader["individualdocument_name"].ToString();
                    lscovenant_type = objODBCDatareader["covenant_type"].ToString();
                }
                objODBCDatareader.Close();
                msSQL = " insert into agr_trn_tdocumentchecktls(" +
                        " documentcheckdtl_gid," +
                        " application_gid," +
                        " credit_gid, " +
                        " individualdocument_gid, " +
                         " documentuploaded_gid, " +
                        " documenttype_gid," +
                        " documenttype_code," +
                        " documenttype_name," +
                        " covenant_type, " +
                        " tagged_by, " +
                        " created_date," +
                        " created_by)" +
                        " VALUES(" +
                        "'" + msGetdefDocchecklistGID + "'," +
                        "'" + msGetGidApplication + "'," +
                        "'" + dt["contact_gid"].ToString() + "'," +
                        "'" + dt["individualdocument_gid"].ToString() + "'," +
                        "'" + dt["contact2document_gid"].ToString() + "'," +
                        "'" + lsdocumenttype_gid + "'," +
                        "'" + lsdocumenttype_name + "'," +
                        "'" + lscompanydocument_name.Replace("'", "") + "'," +
                        "'" + lscovenant_type + "'," +
                        "'N'," +
                        "current_timestamp," +
                        "'" + employee_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (lscovenant_type == "Y")
                {
                    string msGetDocchecklistGID = objcmnfunctions.GetMasterGID("CDCL");
                    msSQL = " insert into agr_trn_tcovanantdocumentcheckdtls(" +
                           " covenantdocumentcheckdtl_gid," +
                           " application_gid," +
                           " credit_gid," +
                           " individualdocument_gid," +
                           " documentuploaded_gid, " +
                           " documenttype_gid," +
                           " documenttype_code," +
                           " documenttype_name," +
                           " covenant_type, " +
                           " tagged_by, " +
                           " created_date," +
                           " created_by)" +
                           " VALUES(" +
                           "'" + msGetDocchecklistGID + "'," +
                           "'" + msGetGidApplication + "'," +
                           "'" + dt["contact_gid"].ToString() + "'," +
                           "'" + dt["individualdocument_gid"].ToString() + "'," +
                           "'" + dt["contact2document_gid"].ToString() + "'," +
                           "'" + lsdocumenttype_gid + "'," +
                           "'" + lsdocumenttype_name + "'," +
                           "'" + lscompanydocument_name.Replace("'", "") + "'," +
                           "'" + lscovenant_type + "'," +
                           "'N'," +
                           "current_timestamp," +
                           "'" + employee_gid + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            dt_datatable21.Dispose();

            msSQL = " select contact_gid from agr_mst_tcontact2document  where contact_gid in " +
                    " (select contact_gid from agr_mst_tcontact where application_gid='" + msGetGidApplication + "') group by contact_gid";
            dt_datatable21 = objdbconn.GetDataTable(msSQL);
            foreach (DataRow dt in dt_datatable21.Rows)
            {
                DaAgrMstScannedDocument objvalues1 = new DaAgrMstScannedDocument();
                string lscredit_gid = dt["contact_gid"].ToString();
                objvalues1.DaGroupDocChecklistinfo(msGetGidApplication, lscredit_gid, employee_gid);

            }
            dt_datatable21.Dispose();


            msSQL = " select institution_gid,companydocument_gid, institution2documentupload_gid  from agr_mst_tinstitution2documentupload  where institution_gid in " +
                   " (select institution_gid from agr_mst_tinstitution where application_gid='" + msGetGidApplication + "')";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            foreach (DataRow dt in dt_datatable.Rows)
            {
                string lscovenant_type = "", lsdocumenttype_gid = "", lsdocumenttype_name = "", lscompanydocument_name = "";

                string msGetdefDocchecklistGID = objcmnfunctions.GetMasterGID("DOCG");
                msSQL = " select companydocument_gid,documenttypes_gid,documenttype_name,companydocument_name,covenant_type " +
                            " from ocs_mst_tcompanydocument where companydocument_gid='" + dt["companydocument_gid"].ToString() + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lsdocumenttype_gid = objODBCDatareader["documenttypes_gid"].ToString();
                    lsdocumenttype_name = objODBCDatareader["documenttype_name"].ToString();
                    lscompanydocument_name = objODBCDatareader["companydocument_name"].ToString();
                    lscovenant_type = objODBCDatareader["covenant_type"].ToString();
                }
                objODBCDatareader.Close();
                msSQL = " insert into agr_trn_tdocumentchecktls(" +
                        " documentcheckdtl_gid," +
                        " application_gid," +
                        " credit_gid, " +
                        " companydocument_gid, " +
                        " documentuploaded_gid, " +
                        " documenttype_gid," +
                        " documenttype_code," +
                        " documenttype_name," +
                        " covenant_type, " +
                        " tagged_by, " +
                        " created_date," +
                        " created_by)" +
                        " VALUES(" +
                        "'" + msGetdefDocchecklistGID + "'," +
                        "'" + msGetGidApplication + "'," +
                        "'" + dt["institution_gid"].ToString() + "'," +
                        "'" + dt["companydocument_gid"].ToString() + "'," +
                        "'" + dt["institution2documentupload_gid"].ToString() + "'," +
                        "'" + lsdocumenttype_gid + "'," +
                        "'" + lsdocumenttype_name + "'," +
                        "'" + lscompanydocument_name.Replace("'", "") + "'," +
                        "'" + lscovenant_type + "'," +
                        "'N'," +
                        "current_timestamp," +
                        "'" + employee_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (lscovenant_type == "Y")
                {
                    string msGetDocchecklistGID = objcmnfunctions.GetMasterGID("CDCL");
                    msSQL = " insert into agr_trn_tcovanantdocumentcheckdtls(" +
                           " covenantdocumentcheckdtl_gid," +
                           " application_gid," +
                           " credit_gid," +
                           " companydocument_gid," +
                           " documentuploaded_gid, " +
                           " documenttype_gid," +
                           " documenttype_code," +
                           " documenttype_name," +
                           " covenant_type, " +
                           " tagged_by, " +
                           " created_date," +
                           " created_by)" +
                           " VALUES(" +
                           "'" + msGetDocchecklistGID + "'," +
                           "'" + msGetGidApplication + "'," +
                           "'" + dt["institution_gid"].ToString() + "'," +
                           "'" + dt["companydocument_gid"].ToString() + "'," +
                           "'" + dt["institution2documentupload_gid"].ToString() + "'," +
                           "'" + lsdocumenttype_gid + "'," +
                           "'" + lsdocumenttype_name + "'," +
                           "'" + lscompanydocument_name.Replace("'", "") + "'," +
                           "'" + lscovenant_type + "'," +
                           "'N'," +
                           "current_timestamp," +
                           "'" + employee_gid + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            dt_datatable.Dispose();

            msSQL = " select institution_gid from agr_mst_tinstitution2documentupload  where institution_gid in " +
                    " (select institution_gid from agr_mst_tinstitution where application_gid='" + msGetGidApplication + "') group by institution_gid";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            foreach (DataRow dt in dt_datatable.Rows)
            {
                DaAgrMstScannedDocument objvalues = new DaAgrMstScannedDocument();
                string lscredit_gid = dt["institution_gid"].ToString();
                objvalues.DaGroupDocChecklistinfo(msGetGidApplication, lscredit_gid, employee_gid);
            }
            dt_datatable.Dispose();

            string lspage = "";
            DaAgrMstApplicationAdd objMstApplicationAdd = new DaAgrMstApplicationAdd();
            objMstApplicationAdd.FnProgramBasedDcoument(msGetGidApplication, employee_gid, user_gid, values.onboarding_status, lspage);

            // Insert data in Application2Loan(Product Details) Table Details
            msSQL = "select application2loan_gid from agr_mst_tapplication2loan where application_gid='" + values.application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msGetGidapplication2loan = objcmnfunctions.GetMasterGID("AP2L");
                    msSQL = " insert into agr_mst_tapplication2loan " +
                            " (application2loan_gid, application_gid, facilityrequested_date, product_type, producttype_gid, productsub_type, productsubtype_gid, loantype_gid, loan_type, " +
                            " loanfacility_amount, rate_interest, penal_interest, tenureproduct_year, tenureproduct_month, tenureproduct_days, tenureoverall_limit, " +
                            " facility_type, facility_mode, created_by, created_date, scheme_type, principalfrequency_name, principalfrequency_gid, " +
                            " interestfrequency_name, interestfrequency_gid, program_gid, program, primaryvaluechain_gid, primaryvaluechain_name, " +
                            " secondaryvaluechain_gid, secondaryvaluechain_name, interest_status, moratorium_status, moratorium_type, " +
                            " moratorium_startdate, moratorium_enddate, source_type, guideline_value, guideline_date, marketvalue_date, market_value, " +
                            " forcedsource_value, collateralSSV_value, forcedvalueassessed_on, collateralobservation_summary, enduse_purpose, " +
                            " updated_by, updated_date, interchangeability, expiry_date, report_structure, document_limit, product_gid, product_name, " +
                            " sector_name, category_name, variety_gid, variety_name, botanical_name, alternative_name, limit_product, trade_orginatedby, " +
                            " SA_Brokerage, programlimit_validdfrom, programlimit_validdto, programoverall_limit, holding_periods, holdingmonthly_procurement, " +
                            " extendedholding_periods, extendedmonthly_procurement, charges_extendedperiod, customer_advance, reimburesementof_expenses, " +
                            " reimburesementof_expensespenalty, bankfundingdata_filename, bankfundingdata_filepath, needfor_stocking, product_portfolio, " +
                            " production_capacity, natureof_operations, averagemonthly_inventoryholding, financialinstitutions_relationship, " +
                            " utilized_limit, available_limit)" +
                            " select @application2loan_gid := '" + msGetGidapplication2loan + "', @application_gid:= '" + msGetGidApplication + "', " +
                            " facilityrequested_date, product_type, producttype_gid, productsub_type, productsubtype_gid, loantype_gid, loan_type, " +
                            " loanfacility_amount, rate_interest, penal_interest, tenureproduct_year, tenureproduct_month, tenureproduct_days, tenureoverall_limit, " +
                            " facility_type, facility_mode, created_by, created_date, scheme_type, principalfrequency_name, principalfrequency_gid, " +
                            " interestfrequency_name, interestfrequency_gid, program_gid, program, primaryvaluechain_gid, primaryvaluechain_name, " +
                            " secondaryvaluechain_gid, secondaryvaluechain_name, interest_status, moratorium_status, moratorium_type, " +
                            " moratorium_startdate, moratorium_enddate, source_type, guideline_value, guideline_date, marketvalue_date, market_value, " +
                            " forcedsource_value, collateralSSV_value, forcedvalueassessed_on, collateralobservation_summary, enduse_purpose, " +
                            " updated_by, updated_date, interchangeability, expiry_date, report_structure, document_limit, product_gid, product_name, " +
                            " sector_name, category_name, variety_gid, variety_name, botanical_name, alternative_name, limit_product, trade_orginatedby, " +
                            " SA_Brokerage, programlimit_validdfrom, programlimit_validdto, programoverall_limit, holding_periods, holdingmonthly_procurement, " +
                            " extendedholding_periods, extendedmonthly_procurement, charges_extendedperiod, customer_advance, reimburesementof_expenses, " +
                            " reimburesementof_expensespenalty, bankfundingdata_filename, bankfundingdata_filepath, needfor_stocking, product_portfolio, " +
                            " production_capacity, natureof_operations, averagemonthly_inventoryholding, financialinstitutions_relationship, " +
                            " utilized_limit, available_limit " +
                            " from agr_mst_tapplication2loan " +
                            " where application_gid='" + values.application_gid + "' " +
                            " and application2loan_gid='" + dt["application2loan_gid"].ToString() + "'";
                    mnResultapplication2loan = objdbconn.ExecuteNonQuerySQL(msSQL);

                    // Insert data in Payment Type Customer Table
                    msSQL = "select paymenttypecustomer_gid from agr_mst_tapploan2paymenttypecustomer where application2loan_gid='" + dt["application2loan_gid"].ToString() + "'";
                    dt_datatable2 = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable2.Rows.Count != 0)
                    {
                        foreach (DataRow dt2 in dt_datatable2.Rows)
                        {
                            msGetGidpaymenttypecustomer = objcmnfunctions.GetMasterGID("PTC");
                            msSQL = " insert into agr_mst_tapploan2paymenttypecustomer " +
                                    " (paymenttypecustomer_gid, application_gid, application2loan_gid, customerpaymenttype_gid, customerpaymenttype_name, " +
                                    " maximumpercent_paymentterm, created_by, created_date) " +
                                    " select @paymenttypecustomer_gid := '" + msGetGidpaymenttypecustomer + "', @application_gid:= '" + msGetGidApplication + "', " +
                                    " @application2loan_gid:= '" + msGetGidapplication2loan + "', " +
                                    " customerpaymenttype_gid, customerpaymenttype_name,maximumpercent_paymentterm, created_by, created_date " +
                                    " from agr_mst_tapploan2paymenttypecustomer " +
                                    " where application2loan_gid='" + dt["application2loan_gid"].ToString() + "' " +
                                    " and paymenttypecustomer_gid='" + dt2["paymenttypecustomer_gid"].ToString() + "'";
                            mnResultpaymenttypecustomer = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    else
                    {
                        mnResultpaymenttypecustomer = 1;
                    }

                    // Insert data in Application2Loan(Product Details) Table 
                    msSQL = "select application2product_gid from agr_mst_tapplication2product where application2loan_gid='" + dt["application2loan_gid"].ToString() + "'";
                    dt_datatable3 = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable3.Rows.Count != 0)
                    {
                        foreach (DataRow dt3 in dt_datatable3.Rows)
                        {
                            msGetGidapplication2product = objcmnfunctions.GetMasterGID("AP2P");
                            msSQL = " insert into agr_mst_tapplication2product " +
                                    " (application2product_gid, application_gid, application2loan_gid, product_gid, product_name, " +
                                    " variety_gid, variety_name, sector_name, category_name, botanical_name, alternative_name, created_by, " +
                                    " created_date, updated_by, updated_date, hsn_code, unitpricevalue_commodity, natureformstate_commoditygid, " +
                                    " natureformstate_commodity, qualityof_commodity, quantity, uom_gid, uom_name, milestone_applicability, " +
                                    " milestonepayment_gid, milestonepayment_name, sa_payout, insurance_availability, insurance_percent, insurance_cost, " +
                                    " net_yield, markto_marketvalue, pricereference_source, headingdesc_product, typeofsupply_naturegid, " +
                                    " typeofsupply_naturename, sectorclassification_gid, sectorclassification_name, creditperiod_years, creditperiod_months, " +
                                    " creditperiod_days, overallcreditperiod_limit, commodity_margin, commoditynet_yield, graceperiod_days, " +
                                    " customerpaymenttype_gid, customerpaymenttype_name, maximumpercent_paymentterm) " +
                                    " select @application2product_gid := '" + msGetGidapplication2product + "',@application_gid:= '" + msGetGidApplication + "', " +
                                    " @application2loan_gid:= '" + msGetGidapplication2loan + "', " +
                                    " product_gid, product_name, " +
                                    " variety_gid, variety_name, sector_name, category_name, botanical_name, alternative_name, created_by, " +
                                    " created_date, updated_by, updated_date, hsn_code, unitpricevalue_commodity, natureformstate_commoditygid, " +
                                    " natureformstate_commodity, qualityof_commodity, quantity, uom_gid, uom_name, milestone_applicability, " +
                                    " milestonepayment_gid, milestonepayment_name, sa_payout, insurance_availability, insurance_percent, insurance_cost, " +
                                    " net_yield, markto_marketvalue, pricereference_source, headingdesc_product, typeofsupply_naturegid, " +
                                    " typeofsupply_naturename, sectorclassification_gid, sectorclassification_name, creditperiod_years, creditperiod_months, " +
                                    " creditperiod_days, overallcreditperiod_limit, commodity_margin, commoditynet_yield, graceperiod_days, " +
                                    " @customerpaymenttype_gid:= '"+ msGetGidpaymenttypecustomer +"', customerpaymenttype_name, maximumpercent_paymentterm " +
                                    " from agr_mst_tapplication2product " +
                                    " where application2loan_gid='" + dt["application2loan_gid"].ToString() + "' " +
                                    " and application2product_gid='" + dt3["application2product_gid"].ToString() + "'";
                            mnResultapplication2product = objdbconn.ExecuteNonQuerySQL(msSQL);

                            // Insert data in product2commodity gst status Table
                            msSQL = "select appproduct2commoditygststatus_gid from agr_mst_tappproduct2commoditygststatus where application2product_gid='" + dt3["application2product_gid"].ToString() + "'";
                            dt_datatable4 = objdbconn.GetDataTable(msSQL);
                            if (dt_datatable4.Rows.Count != 0)
                            {
                                foreach (DataRow dt4 in dt_datatable4.Rows)
                                {
                                    msGetGidproduct2gststatus = objcmnfunctions.GetMasterGID("APGT");
                                    msSQL = " insert into agr_mst_tappproduct2commoditygststatus " +
                                            " (appproduct2commoditygststatus_gid, commoditygststatus_gid, application2product_gid, product_gid, " +
                                            " variety_gid, IGST_percent, SGST_percent, CGST_percent, CESS_percent, wef_date, created_by, " +
                                            " created_date) " +
                                            " select @appproduct2commoditygststatus_gid := '" + msGetGidproduct2gststatus + "',commoditygststatus_gid," +
                                            " @application2product_gid:= '" + msGetGidapplication2product + "', " +
                                            "  product_gid, " +
                                            " variety_gid, IGST_percent, SGST_percent, CGST_percent, CESS_percent, wef_date, created_by, created_date " +
                                            " from agr_mst_tappproduct2commoditygststatus " +
                                            " where application2product_gid= '" + dt3["application2product_gid"].ToString() + "'" +
                                            " and appproduct2commoditygststatus_gid='" + dt4["appproduct2commoditygststatus_gid"].ToString() + "'";
                                    mnResultproduct2gststatus = objdbconn.ExecuteNonQuerySQL(msSQL);
                                }
                            }
                            else
                            {
                                mnResultproduct2gststatus = 1;
                            }
                            // Insert data in product2commodity trade detail Table
                            msSQL = "select appproduct2commoditytrade_gid from agr_mst_tappproduct2commoditytradedtl where application2product_gid='" + dt3["application2product_gid"].ToString() + "'";
                            dt_datatable5 = objdbconn.GetDataTable(msSQL);
                            if (dt_datatable5.Rows.Count != 0)
                            {
                                foreach (DataRow dt5 in dt_datatable5.Rows)
                                {
                                    msGetGidproduct2commoditytrade = objcmnfunctions.GetMasterGID("ACTP");
                                    msSQL = " insert into agr_mst_tappproduct2commoditytradedtl " +
                                            " (appproduct2commoditytrade_gid, commoditytradeproductdtl_gid, application2product_gid, mstproduct_gid, " +
                                            " variety_gid, product_gid, product_name, subproduct_gid, subproduct_name, insurancecompany_gid, " +
                                            " insurancecompany_name, insurancepolicy_gid, insurancepolicy_name, created_by, created_date) " +
                                            " select @appproduct2commoditytrade_gid := '" + msGetGidproduct2commoditytrade + "',commoditytradeproductdtl_gid, @application2product_gid:= '" + msGetGidapplication2product + "', " +
                                            " mstproduct_gid,variety_gid, product_gid, product_name, subproduct_gid, subproduct_name, insurancecompany_gid, " +
                                            " insurancecompany_name, insurancepolicy_gid, insurancepolicy_name, created_by, created_date " +
                                            " from agr_mst_tappproduct2commoditytradedtl " +
                                            " where application2product_gid= '" + dt3["application2product_gid"].ToString() + "'" +
                                            " and appproduct2commoditytrade_gid='" + dt5["appproduct2commoditytrade_gid"].ToString() + "'";
                                    mnResultproduct2commoditytrade = objdbconn.ExecuteNonQuerySQL(msSQL);
                                }
                            }
                            else
                            {
                                mnResultproduct2commoditytrade = 1;
                            }
                            // Insert data in product2commodity document detail Table
                            msSQL = "select appproduct2commoditydocument_gid from agr_mst_tappproduct2commoditydocument where application2product_gid='" + dt3["application2product_gid"].ToString() + "'";
                            dt_datatable6 = objdbconn.GetDataTable(msSQL);
                            if (dt_datatable6.Rows.Count != 0)
                            {
                                foreach (DataRow dt6 in dt_datatable6.Rows)
                                {
                                    msGetGidproduct2commoditydoc = objcmnfunctions.GetMasterGID("APCD");
                                    msSQL = " insert into agr_mst_tappproduct2commoditydocument " +
                                            " (appproduct2commoditydocument_gid, commoditydocument_gid, application2product_gid, ason_date, commodityreport_filename, " +
                                            " commodityreport_filepath, riskanalysisreport_filename, riskanalysisreport_filepath, created_by, created_date, " +
                                            " variety_gid) " +
                                            " select @appproduct2commoditydocument_gid := '" + msGetGidproduct2commoditydoc + "',  commoditydocument_gid, @application2product_gid:= '" + msGetGidapplication2product + "', " +
                                            " ason_date, commodityreport_filename, " +
                                            " commodityreport_filepath, riskanalysisreport_filename, riskanalysisreport_filepath, created_by, created_date, " +
                                            " variety_gid " +
                                            " from agr_mst_tappproduct2commoditydocument " +
                                            " where application2product_gid= '" + dt3["application2product_gid"].ToString() + "'" +
                                            " and appproduct2commoditydocument_gid='" + dt6["appproduct2commoditydocument_gid"].ToString() + "'";
                                    mnResultproduct2commoditydoc = objdbconn.ExecuteNonQuerySQL(msSQL);
                                }
                            }
                            else
                            {
                                mnResultproduct2commoditydoc = 1;
                            }
                        }
                    }
                    else
                    {
                        mnResultapplication2product = 1;
                        mnResultproduct2gststatus = 1;
                        mnResultproduct2commoditytrade = 1;
                        mnResultproduct2commoditydoc = 1;
                    }
                    // Insert data in application loan2supplier details Table
                    msSQL = "select apploan2supplierdtl_gid from agr_mst_tapploan2supplierdtl where application2loan_gid='" + dt["application2loan_gid"].ToString() + "'";
                    dt_datatable7 = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable7.Rows.Count != 0)
                    {
                        foreach (DataRow dt7 in dt_datatable7.Rows)
                        {
                            msGetGidloan2supplierdtl = objcmnfunctions.GetMasterGID("ALSD");
                            msSQL = " insert into agr_mst_tapploan2supplierdtl " +
                                    " (apploan2supplierdtl_gid, application_gid, application2loan_gid, supplier_gid, supplier_name, " +
                                    " supplier_address, supplier_emailid, supplier_phoneno, supplier_gstno, supplier_pandtl, milestone_applicable, " +
                                    " milestonepaymenttype_gid, milestonepaymenttype_name, supplier_vintage, created_by, created_date, supplier_margin) " +
                                    " select @apploan2supplierdtl_gid := '" + msGetGidloan2supplierdtl + "', @application_gid:= '" + msGetGidApplication + "', " +
                                    " @application2loan_gid:= '" + msGetGidapplication2loan + "', " +
                                    " supplier_gid, supplier_name, supplier_address, supplier_emailid, supplier_phoneno, supplier_gstno, " +
                                    " supplier_pandtl, milestone_applicable, milestonepaymenttype_gid, milestonepaymenttype_name, supplier_vintage, " +
                                    " created_by, created_date, supplier_margin " +
                                    " from agr_mst_tapploan2supplierdtl " +
                                    " where application2loan_gid='" + dt["application2loan_gid"].ToString() + "' " +
                                    " and apploan2supplierdtl_gid='" + dt7["apploan2supplierdtl_gid"].ToString() + "'";
                            mnResultloan2supplierdtl = objdbconn.ExecuteNonQuerySQL(msSQL);

                            // Insert data in application2supplier gst details Table
                            msSQL = "select app2suppliergstdtl_gid from agr_mst_tapp2suppliergstdtl where apploan2supplierdtl_gid='" + dt7["apploan2supplierdtl_gid"].ToString() + "'";
                            dt_datatable8 = objdbconn.GetDataTable(msSQL);
                            if (dt_datatable8.Rows.Count != 0)
                            {
                                foreach (DataRow dt8 in dt_datatable8.Rows)
                                {
                                    msGetGidapp2suppliergstdtl = objcmnfunctions.GetMasterGID("A2SG");
                                    msSQL = " insert into agr_mst_tapp2suppliergstdtl " +
                                            " (app2suppliergstdtl_gid, apploan2supplierdtl_gid, institution2branch_gid, gst_state, gst_no, created_by, created_date) " +
                                            " select @app2suppliergstdtl_gid := '" + msGetGidapp2suppliergstdtl + "', @apploan2supplierdtl_gid:= '" + msGetGidloan2supplierdtl + "', " +
                                            " institution2branch_gid, gst_state, gst_no, created_by, created_date " +
                                            " from agr_mst_tapp2suppliergstdtl " +
                                            " where apploan2supplierdtl_gid='" + dt7["apploan2supplierdtl_gid"].ToString() + "' " +
                                            " and app2suppliergstdtl_gid='" + dt8["app2suppliergstdtl_gid"].ToString() + "'";
                                    mnResultapp2suppliergstdtl = objdbconn.ExecuteNonQuerySQL(msSQL);
                                }
                            }
                            else
                            {
                                mnResultapp2suppliergstdtl = 1;
                            }
                        }
                    }
                    else
                    {
                        mnResultloan2supplierdtl = 1;
                        mnResultapp2suppliergstdtl = 1;
                    }
                    // Insert data in application loan2supplier payment Table
                    msSQL = "select apploan2supplierpayment_gid from agr_mst_tapploan2supplierpayment where application2loan_gid='" + dt["application2loan_gid"].ToString() + "'";
                    dt_datatable9 = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable9.Rows.Count != 0)
                    {
                        foreach (DataRow dt9 in dt_datatable9.Rows)
                        {
                            msGetGidloan2supplierpayment = objcmnfunctions.GetMasterGID("ALSD");
                            msSQL = " insert into agr_mst_tapploan2supplierpayment " +
                                    " (apploan2supplierpayment_gid, application_gid, application2loan_gid, commodity_gid, commodity_name," +
                                    " supplierpayment_type, supplierpayment_typegid, maxpercent_paymentterm, created_by, created_date, apploan2supplierdtl_gid) " +
                                    " select @apploan2supplierpayment_gid := '" + msGetGidloan2supplierpayment + "', @application_gid:= '" + msGetGidApplication + "', " +
                                    " @application2loan_gid:= '" + msGetGidapplication2loan + "', " +
                                    " commodity_gid, commodity_name,supplierpayment_type, supplierpayment_typegid, " +
                                    " maxpercent_paymentterm, created_by, created_date,  @apploan2supplierdtl_gid:= '" + msGetGidloan2supplierdtl + "' " +
                                    " from agr_mst_tapploan2supplierpayment " +
                                    " where application2loan_gid='" + dt["application2loan_gid"].ToString() + "' " +
                                    " and apploan2supplierpayment_gid='" + dt9["apploan2supplierpayment_gid"].ToString() + "'";
                            mnResultloan2supplierpayment = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    else
                    {
                        mnResultloan2supplierpayment = 1;
                    }
                    // Insert data in application2buyer Table
                    msSQL = "select application2buyer_gid from agr_mst_tapplication2buyer where application2loan_gid='" + dt["application2loan_gid"].ToString() + "'";
                    dt_datatable10 = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable10.Rows.Count != 0)
                    {
                        foreach (DataRow dt10 in dt_datatable10.Rows)
                        {
                            msGetGidapplication2buyer = objcmnfunctions.GetMasterGID("AP2B");
                            msSQL = " insert into agr_mst_tapplication2buyer " +
                                    " (application2buyer_gid, application2loan_gid, buyer_gid, buyer_name, buyer_limit, " +
                                    " availed_limit, balance_limit, bill_tenure, margin, created_by, created_date, updated_by, " +
                                    " updated_date) " +
                                    " select @application2buyer_gid := '" + msGetGidapplication2buyer + "'," +
                                    " @application2loan_gid:= '" + msGetGidapplication2loan + "', " +
                                    "  buyer_gid, buyer_name, buyer_limit,  " +
                                    " availed_limit, balance_limit, bill_tenure, margin, created_by, created_date, updated_by, " +
                                    " updated_date " +
                                    " from agr_mst_tapplication2buyer " +
                                    " where application2loan_gid='" + dt["application2loan_gid"].ToString() + "' " +
                                    " and application2buyer_gid='" + dt10["application2buyer_gid"].ToString() + "'";
                            mnResultapplication2buyer = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    else
                    {
                        mnResultapplication2buyer = 1;
                    }
                    // Insert data in application Service Charges Details Table
                    msSQL = "select application2servicecharge_gid from agr_mst_tapplicationservicecharge where application2loan_gid='" + dt["application2loan_gid"].ToString() + "'";
                    dt_datatable11 = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable11.Rows.Count != 0)
                    {
                        foreach (DataRow dt11 in dt_datatable11.Rows)
                        {
                            msGetGidappl2servicecharge = objcmnfunctions.GetMasterGID("AP2C");
                            msSQL = " insert into agr_mst_tapplicationservicecharge " +
                                    " (application2servicecharge_gid, application_gid, application2loan_gid, processing_fee, processing_collectiontype," +
                                    " doc_charges, doccharge_collectiontype, fieldvisit_charges, fieldvisit_charges_collectiontype, adhoc_fee, " +
                                    " adhoc_collectiontype, life_insurance, lifeinsurance_collectiontype, acct_insurance, total_collect, " +
                                    " total_deduct, product_type, producttype_gid, created_by, created_date, updated_date, updated_by, " +
                                    " acctinsurance_collectiontype) " +
                                    " select @application2servicecharge_gid := '" + msGetGidappl2servicecharge + "',@application_gid:= '" + msGetGidApplication + "'," +
                                    " @application2loan_gid:= '" + msGetGidapplication2loan + "', processing_fee, processing_collectiontype," +
                                     " doc_charges, doccharge_collectiontype, fieldvisit_charges, fieldvisit_charges_collectiontype, adhoc_fee, " +
                                    " adhoc_collectiontype, life_insurance, lifeinsurance_collectiontype, acct_insurance, total_collect, " +
                                    " total_deduct, product_type, producttype_gid, created_by, created_date, updated_date, updated_by, " +
                                    " acctinsurance_collectiontype " +
                                    " from agr_mst_tapplicationservicecharge " +
                                    " where application2loan_gid='" + dt["application2loan_gid"].ToString() + "' " +
                                    " and application2servicecharge_gid='" + dt11["application2servicecharge_gid"].ToString() + "'";
                            mnResultappl2servicecharge = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    else
                    {
                        mnResultappl2servicecharge = 1;
                    }
                    // Insert data in application trade2warehouse Details Table
                    msSQL = "select applicationtrade2warehouse_gid from agr_mst_tapplicationtrade2warehouse where application2loan_gid='" + dt["application2loan_gid"].ToString() + "'";
                    dt_datatable12 = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable12.Rows.Count != 0)
                    {
                        foreach (DataRow dt12 in dt_datatable12.Rows)
                        {
                            msGetGidappltrade2warehouse = objcmnfunctions.GetMasterGID("AT2W");
                            msSQL = " insert into agr_mst_tapplicationtrade2warehouse " +
                                    " (applicationtrade2warehouse_gid, application2trade_gid, application2loan_gid, application_gid, creditor_gid," +
                                    " warehouse_gid, warehouse_agency, warehouse_name, typeofwarehouse_name, volume_uomgid, volume_uom, " +
                                    " totalcapacity_volume, totalcapacity_area, totalcapacityarea_uomgid, area_uom, warehouse2address_gid, " +
                                    " warehouse_address, capacity_commodity, capacity_panina, created_by, created_date, updated_by, updated_date) " +
                                    " select @applicationtrade2warehouse_gid := '" + msGetGidappltrade2warehouse + "',@application_gid:= '" + msGetGidApplication + "'," +
                                    " @application2loan_gid:= '" + msGetGidapplication2loan + "', application2trade_gid,creditor_gid," +
                                    " warehouse_gid, warehouse_agency, warehouse_name, typeofwarehouse_name, volume_uomgid, volume_uom, " +
                                    " totalcapacity_volume, totalcapacity_area, totalcapacityarea_uomgid, area_uom, warehouse2address_gid, " +
                                    " warehouse_address, capacity_commodity, capacity_panina, created_by, created_date, updated_by, updated_date " +
                                    " from agr_mst_tapplicationtrade2warehouse " +
                                    " where application2loan_gid='" + dt["application2loan_gid"].ToString() + "' " +
                                    " and applicationtrade2warehouse_gid='" + dt12["applicationtrade2warehouse_gid"].ToString() + "'";
                            mnResultappltrade2warehouse = objdbconn.ExecuteNonQuerySQL(msSQL);

                            // Insert data in application2trade Details Table
                            msSQL = "select application2trade_gid from agr_mst_tapplication2trade where application2loan_gid='" + dt["application2loan_gid"].ToString() + "'";
                            dt_datatable13 = objdbconn.GetDataTable(msSQL);
                            if (dt_datatable13.Rows.Count != 0)
                            {
                                foreach (DataRow dt13 in dt_datatable13.Rows)
                                {
                                    msGetGidapplication2trade = objcmnfunctions.GetMasterGID("APTR");
                                    msSQL = " insert into agr_mst_tapplication2trade " +
                                            " (application2trade_gid, application_gid, producttype_gid, producttype_name, productsubtype_gid, " +
                                            " productsubtype_name, salescontract_availability, scopeof_transportgid, scopeof_transport, " +
                                            " scopeof_loadinggid, scopeof_loading, scopeof_unloadinggid, scopeof_unloading, " +
                                            " scopeof_qualityandquantitygid, scopeof_qualityandquantity, scopeof_moisturegainlossgid, " +
                                            " scopeof_moisturegainloss, created_by, created_date, updated_by, updated_date, application2loan_gid, " +
                                            " scopeof_insurancegid, scopeof_insurance) " +
                                            " select @application2trade_gid := '" + msGetGidapplication2trade + "',@application_gid:= '" + msGetGidApplication + "'," +
                                            " producttype_gid, producttype_name, productsubtype_gid, " +
                                            " productsubtype_name, salescontract_availability, scopeof_transportgid, scopeof_transport, " +
                                            " scopeof_loadinggid, scopeof_loading, scopeof_unloadinggid, scopeof_unloading, " +
                                            " scopeof_qualityandquantitygid, scopeof_qualityandquantity, scopeof_moisturegainlossgid, " +
                                            " scopeof_moisturegainloss, created_by, created_date, updated_by, updated_date, " +
                                            " @application2loan_gid:= '" + msGetGidapplication2loan + "', " +
                                            " scopeof_insurancegid, scopeof_insurance " +
                                            " from agr_mst_tapplication2trade " +
                                            " where application2loan_gid='" + dt["application2loan_gid"].ToString() + "' " +
                                            " and application2trade_gid='" + dt13["application2trade_gid"].ToString() + "'";
                                    mnResultapplication2trade = objdbconn.ExecuteNonQuerySQL(msSQL);
                                }
                            }
                            else
                            {
                                mnResultapplication2trade = 1;
                            }
                        }
                    }
                    else
                    {
                        mnResultappltrade2warehouse = 1;
                        mnResultapplication2trade = 1;
                    }

                }
            }

            else
            {
                mnResultapplication2loan = 1;
                mnResultpaymenttypecustomer = 1;
                mnResultapplication2product = 1;
                mnResultproduct2gststatus = 1;
                mnResultproduct2commoditytrade = 1;
                mnResultproduct2commoditydoc = 1;
                mnResultloan2supplierdtl = 1;
                mnResultapp2suppliergstdtl = 1;
                mnResultloan2supplierpayment = 1;
                mnResultapplication2buyer = 1;
                mnResultappl2servicecharge = 1;
                mnResultappltrade2warehouse = 1;
                mnResultapplication2trade = 1;
            }

            if (mnResultapplicationcreation != 0 && mnResultapplicationcontact != 0 && mnResultapplicationgeneticcode != 0 && mnResultapplicationemail != 0 && mnResultinstitution != 0 && mnResultinstitutionbranch != 0 && mnResultinstitutionmobile != 0 && mnResultinstitutionemail != 0 && mnResultinstitutionaddress != 0 && mnResultinstitutionlicense != 0 && mnResultinstitutiondocument != 0 && mnResultinstitutionratingdtl != 0 && mnResultinstitutionbankdtl != 0 && mnResultcontact != 0 && mnResultcontactpanabsencereason != 0 && mnResultcontactpanform60 != 0 && mnResultcontactmobile != 0 && mnResultcontactemail != 0 && mnResultcontactidproof != 0 && mnResultcontactaddress != 0 && mnResultcontactdocument != 0 && mnResultapplication2loan != 0 && mnResultpaymenttypecustomer != 0 && mnResultapplication2product != 0 && mnResultproduct2gststatus != 0 && mnResultproduct2commoditytrade != 0 && mnResultproduct2commoditydoc != 0 && mnResultloan2supplierdtl != 0 && mnResultapp2suppliergstdtl != 0 && mnResultloan2supplierpayment != 0 && mnResultapplication2buyer != 0 && mnResultappl2servicecharge != 0 && mnResultappltrade2warehouse != 0 && mnResultapplication2trade != 0 && mnResultonboardinitiate != 0)
            {
                 msSQL = "INSERT INTO agr_mst_tonboardclonelog(" +
                            " onboard_gid," +
                            " existingapplication_gid," +
                            " cloneapplication_gid, " +
                            " clone_status, " +
                            " existing_overallvaliditydate, " +
                            " existing_overallcalculation, " +
                            " created_by," +
                            " created_date)" +
                            " VALUES(" +
                            "'" + values.buyeronboard_gid + "'," +
                            "'" + values.application_gid + "'," +
                            "'" + msGetGidApplication + "'," +
                            "'Renewal'," +
                            "'" + Convert.ToDateTime(values.validityto_date).ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                            "'" + values.calculationoveralllimit_validity + "'," +
                            "'" + employee_gid + "'," +
                            "current_timestamp)";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.application_no = lsapp_refno;
                values.message = "Application Renewed Successfully";
                return true;
            }
            else
            {
                // Delete Tables

                msSQL = " select GROUP_CONCAT(institution_gid) as totalinstitution_gid " +
                        " from agr_mst_tinstitution where application_gid='" + msGetGidApplication + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.totalinstitution_gid = objODBCDatareader["totalinstitution_gid"].ToString();
                }
                msSQL = " delete from agr_mst_tinstitution2branch where institution_gid in ('" + values.totalinstitution_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tinstitution2mobileno where institution_gid in ('" + values.totalinstitution_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tinstitution2email where institution_gid in ('" + values.totalinstitution_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tinstitution2email where institution_gid in ('" + values.totalinstitution_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tinstitution2licensedtl where institution_gid in ('" + values.totalinstitution_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tinstitution2documentupload where institution_gid in ('" + values.totalinstitution_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tinstitution2ratingdetail where institution_gid in ('" + values.totalinstitution_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tinstitution2bankdtl here institution_gid in ('" + values.totalinstitution_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_trn_tdocumentchecktls where credit_gid in ('" + values.totalinstitution_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_trn_tcovanantdocumentcheckdtls where credit_gid in ('" + values.totalinstitution_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " select GROUP_CONCAT(contact_gid) as totalcontact_gid " +
                        " from agr_mst_tcontact where application_gid='" + msGetGidApplication + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.totalcontact_gid = objODBCDatareader["totalcontact_gid"].ToString();
                }
                msSQL = " delete from agr_mst_tcontact2panabsencereason where contact_gid in ('" + values.totalcontact_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tcontact2panform60 where contact_gid in ('" + values.totalcontact_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tcontact2mobileno where contact_gid in ('" + values.totalcontact_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tcontact2email where contact_gid in ('" + values.totalcontact_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tcontact2idproof where contact_gid in ('" + values.totalcontact_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tcontact2address where contact_gid in ('" + values.totalcontact_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tcontact2document where contact_gid in ('" + values.totalcontact_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_trn_tdocumentchecktls where credit_gid in ('" + values.totalcontact_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_trn_tcovanantdocumentcheckdtls where credit_gid in ('" + values.totalcontact_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " select GROUP_CONCAT(application2product_gid) as totalapplication2product_gid " +
                        " from agr_mst_tapplication2product where application_gid='" + msGetGidApplication + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.totalapplication2product_gid = objODBCDatareader["totalapplication2product_gid"].ToString();
                }
                msSQL = " delete from agr_mst_tappproduct2commoditygststatus where application2product_gid in ('" + values.totalapplication2product_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tappproduct2commoditytradedtl where application2product_gid in ('" + values.totalapplication2product_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tappproduct2commoditydocument where application2product_gid in ('" + values.totalapplication2product_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " select GROUP_CONCAT(apploan2supplierdtl_gid) as totalapploan2supplierdtl_gid " +
                        " from agr_mst_tapploan2supplierdtl where application_gid='" + msGetGidApplication + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.totalapploan2supplierdtl_gid = objODBCDatareader["totalapploan2supplierdtl_gid"].ToString();
                }
                msSQL = " delete from agr_mst_tapp2suppliergstdtl where apploan2supplierdtl_gid in ('" + values.totalapploan2supplierdtl_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " select GROUP_CONCAT(application2loan_gid) as totalapplication2loan_gid " +
                       " from agr_mst_tapplication2loan where application_gid='" + msGetGidApplication + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.totalapplication2loan_gid = objODBCDatareader["totalapplication2loan_gid"].ToString();
                }
                msSQL = " delete from agr_mst_tapplication2buyer where application2loan_gid in ('" + values.totalapplication2loan_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tapplication where application_gid='" + msGetGidApplication + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tapplication2contactno where application_gid='" + msGetGidApplication + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tapplication2email where application_gid='" + msGetGidApplication + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tapplication2geneticcode where application_gid='" + msGetGidApplication + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tinstitution where application_gid='" + msGetGidApplication + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tcontact where application_gid='" + msGetGidApplication + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tapplication2loan where application_gid='" + msGetGidApplication + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tapploan2paymenttypecustomer where application_gid='" + msGetGidApplication + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tapplication2product where application_gid='" + msGetGidApplication + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tapploan2supplierdtl where application_gid='" + msGetGidApplication + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tapploan2supplierpayment where application_gid='" + msGetGidApplication + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tapplicationservicecharge where application_gid='" + msGetGidApplication + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tapplicationtrade2warehouse where application_gid='" + msGetGidApplication + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tapplication2trade where application_gid='" + msGetGidApplication + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tonboardinitiatedtl where application_gid='" + msGetGidApplication + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = false;
                values.message = "Error Occurred while Renew the Application";
                return false;
            }

        }
        public void DaGetOnboardLimitManagementdtl(string onboard_gid, MdlRenewalOnboardLimitManagement values)
        {
            msSQL = " select customerref_name,application_no,lgltag_status from agr_mst_tbyronboard " +
                    " where application_gid = '" + onboard_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                values.customerref_name = objODBCDataReader["customerref_name"].ToString();
                values.application_no = objODBCDataReader["application_no"].ToString();
                values.lgltag_status = objODBCDataReader["lgltag_status"].ToString();
            }
            objODBCDataReader.Close();
            msSQL = " select a.application_gid,producttype_gid,product_type from agr_mst_tapplication2loan a " +
                    " left join agr_mst_tapplication b on a.application_gid = b.application_gid " +
                    " where a.application_gid in (select application_gid from agr_mst_tapplication c" +
                    " where buyeronboard_gid = '" + onboard_gid + "' and ( process_type = 'Accept' or close_flag='Y')) " +
                    " group by a.producttype_gid ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getMdlRenewalProductTypeList = new List<MdlRenewalProductTypeList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getMdlRenewalProductTypeList.Add(new MdlRenewalProductTypeList
                    {
                        producttype_gid = dt["producttype_gid"].ToString(),
                        producttype_name = dt["product_type"].ToString(),
                

                    });

                }
                values.MdlRenewalProductTypeList = getMdlRenewalProductTypeList;
            }
            dt_datatable.Dispose();

            msSQL = " select a.application_gid,producttype_gid, productsubtype_gid,productsub_type " +
                    " from agr_mst_tapplication2loan a" +
                    " left join agr_mst_tapplication b on a.application_gid = b.application_gid " +
                    " where a.application_gid in (select application_gid from agr_mst_tapplication c" +
                    " where  buyeronboard_gid = '" + onboard_gid + "' and ( process_type = 'Accept' or close_flag='Y'))" +
                    " group by b.application_gid ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getMdlRenewalProductSubTypeList = new List<MdlRenewalProductSubTypeList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getMdlRenewalProductSubTypeList.Add(new MdlRenewalProductSubTypeList
                    {
                        producttype_gid = dt["producttype_gid"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        productsubtype_name = dt["productsub_type"].ToString(),
                        productsubtype_gid = dt["productsubtype_gid"].ToString(),
                    });

                }
                values.MdlRenewalProductSubTypeList = getMdlRenewalProductSubTypeList;
            }
            dt_datatable.Dispose();

            msSQL = " select b.producttype_gid,b.productsubtype_gid,a.application_gid,a.application_no, a.renewal_flag,a.amendment_flag,a.shortclosing_flag, a.application_no as 'ARN', a.contract_id, a.buyeronboard_gid, " +                   
                    " case when DATE(validityto_date) <= DATE(NOW()) then 'Expired' " +            
                    " when DATE(validityto_date) > DATE(NOW())  then 'Active' else '' end as 'ContractStatus', " +
                    "  (select sum(e.loanfacility_amount) from agr_mst_tapplication2loan e" +
                    " where e.application_gid = a.application_gid) as product_overallamount, a.onboarding_status, " +
                    " (select date_format (c.processupdated_date, '%d-%m-%Y') from agr_mst_tapplication c " +
                    " WHERE c.application_gid > a.application_gid and buyeronboard_gid = '" + onboard_gid + "'  " +
                    " and process_type = 'Accept' ORDER BY application_gid LIMIT 1) as 'processupdated_date' " +
                    " from agr_mst_tapplication a " +
                    " left join agr_mst_tapplication2loan b on a.application_gid = b.application_gid " +
                    " where buyeronboard_gid = '" + onboard_gid + "' and  ( process_type = 'Accept' or close_flag='Y') group by a.application_gid  ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getMdlRenewalApplicationList = new List<MdlRenewalApplicationList>();
            if (dt_datatable.Rows.Count != 0)
            {
                string lsrenewal_application = "";
                string lsrejected_flag = "";
              
                string lsrenewalnew_application = "";
                string lsrenewalprocessnull_application = "";
                string lsapplicationshortclose_flag = "";
                string lsprocessupdated_date = "";
                string lsmaxprocessupdated_date = "";
                string lsprocess_type = "";

                foreach (DataRow dt in dt_datatable.Rows)
                {
                    string lsapplicationrenewal_flag = "N";
                    msSQL = " select a.application_gid from agr_mst_tapplication a " +
                            " where a.application_gid ='" + dt["application_gid"].ToString() + "'  and " +
                            " a.processupdated_date = (select max(c.processupdated_date) from agr_mst_tapplication c " +
                             " left join agr_mst_tapplication2loan b on c.application_gid = b.application_gid " +
                            " where c.buyeronboard_gid = '" + onboard_gid + "' and c.onboarding_status = 'Proposal' and " +
                            " b.producttype_gid = '" + dt["producttype_gid"].ToString() + "' and b.productsubtype_gid = '" + dt["productsubtype_gid"].ToString() + "' and c.process_type = 'Accept')";
                    lsrenewal_application = objdbconn.GetExecuteScalar(msSQL);
                    if (lsrenewal_application != "")
                    {
                        lsapplicationrenewal_flag = "Y";
                    }

                    msSQL = " select a.application_gid from agr_mst_tapplication a " +
                            " where a.created_date = (select max(c.created_date) from agr_mst_tapplication c " +
                             " left join agr_mst_tapplication2loan b on c.application_gid = b.application_gid " +
                            " where c.buyeronboard_gid = '" + onboard_gid + "' and b.producttype_gid = '" + dt["producttype_gid"].ToString() + "' and b.productsubtype_gid = '" + dt["productsubtype_gid"].ToString() + "' and c.onboarding_status = 'Proposal' and " +
                            " (c.renewal_flag='Y' or c.amendment_flag='Y' or c.shortclosing_flag='Y') and " +
                            " (c.approval_status = 'Rejected By Business' or c.approval_status = 'Rejected by Credit Manager' or " +
                            " c.approval_status = 'Rejected By Credit' or c.approval_status = 'CC Rejected' or c.approval_status ='Product Approval - Rejected' or c.close_flag ='Y'))";
                    lsrejected_flag = objdbconn.GetExecuteScalar(msSQL);
                    if (lsrejected_flag != "")
                    {
                        msSQL = " select a.application_gid from agr_mst_tapplication a " +
                            " where a.application_gid ='" + dt["application_gid"].ToString() + "'  and " +
                            " a.processupdated_date = (select max(c.processupdated_date) from agr_mst_tapplication c " +
                             " left join agr_mst_tapplication2loan b on c.application_gid = b.application_gid " +
                            " where c.buyeronboard_gid = '" + onboard_gid + "' and b.producttype_gid = '" + dt["producttype_gid"].ToString() + "' and b.productsubtype_gid = '" + dt["productsubtype_gid"].ToString() + "' and c.onboarding_status = 'Proposal' and " +
                            " c.process_type = 'Accept')";
                        lsrenewalnew_application = objdbconn.GetExecuteScalar(msSQL);
                        if (lsrenewalnew_application != "")
                        {
                            lsapplicationrenewal_flag = "Y";
                        }

                    }

                    msSQL = " select a.onboarding_status from agr_mst_tapplication a " +
                            
                            " where a.processupdated_date = (select max(c.processupdated_date) from agr_mst_tapplication c " +
                            " left join agr_mst_tapplication2loan b on c.application_gid = b.application_gid " +
                            " where c.buyeronboard_gid = '" + onboard_gid + "' and b.producttype_gid = '" + dt["producttype_gid"].ToString() + "' and b.productsubtype_gid = '" + dt["productsubtype_gid"].ToString() + "' ) ";
                   string lslatestdirect_flag = objdbconn.GetExecuteScalar(msSQL);
                    if (lslatestdirect_flag == "Direct")
                    {
                        msSQL = " (select onboarding_status from agr_mst_tapplication c " +
                                " left join agr_mst_tapplication2loan b on c.application_gid = b.application_gid " +
                                " where c.buyeronboard_gid = '" + onboard_gid + "' and b.producttype_gid = '" + dt["producttype_gid"].ToString() + "' and b.productsubtype_gid = '" + dt["productsubtype_gid"].ToString() + "' and c.onboarding_status = 'Proposal' )";
                        string lsdirectnull_application1 = objdbconn.GetExecuteScalar(msSQL);
                        if (lsdirectnull_application1 == "Proposal")
                        {
                            msSQL = " select a.process_type from agr_mst_tapplication a " +
                                " where a.application_gid ='" + dt["application_gid"].ToString() + "' and " +
                                " a.created_date = (select max(c.created_date) from agr_mst_tapplication c " +
                                 " left join agr_mst_tapplication2loan b on c.application_gid = b.application_gid " +
                                " where c.buyeronboard_gid = '" + onboard_gid + "' and b.producttype_gid = '" + dt["producttype_gid"].ToString() + "' and b.productsubtype_gid = '" + dt["productsubtype_gid"].ToString() + "' and c.onboarding_status = 'Proposal'" +
                                " )";
                        string lsdirectnull_application = objdbconn.GetExecuteScalar(msSQL);
                        if (lsdirectnull_application != "")
                        {
                            msSQL = " select a.application_gid from agr_mst_tapplication a " +
                                    " where a.application_gid ='" + dt["application_gid"].ToString() + "'  and " +
                                    " a.processupdated_date = (select max(c.processupdated_date) from agr_mst_tapplication c " +
                                     " left join agr_mst_tapplication2loan b on c.application_gid = b.application_gid " +
                                    " where c.buyeronboard_gid = '" + onboard_gid + "' and b.producttype_gid = '" + dt["producttype_gid"].ToString() + "' and b.productsubtype_gid = '" + dt["productsubtype_gid"].ToString() + "' and c.onboarding_status = 'Proposal' and " +
                                    " c.process_type = 'Accept' )";
                            string lsdirectrenewal_application = objdbconn.GetExecuteScalar(msSQL);
                            if (lsdirectrenewal_application != "")
                            {
                                lsapplicationrenewal_flag = "Y";
                            }
                        }
                    }
                }

                    msSQL = " select a.application_gid from agr_mst_tapplication a " +
                            " where a.created_date = (select max(c.created_date) from agr_mst_tapplication c " +
                            " left join agr_mst_tapplication2loan b on c.application_gid = b.application_gid " +
                            " where c.buyeronboard_gid = '" + onboard_gid + "' and b.producttype_gid = '" + dt["producttype_gid"].ToString() + "' and b.productsubtype_gid = '" + dt["productsubtype_gid"].ToString() + "' and c.onboarding_status = 'Proposal' and " +
                            " c.process_type is null  and (c.renewal_flag='Y' or c.amendment_flag='Y' or c.shortclosing_flag='Y')) and " +
                            " (a.approval_status != 'Rejected By Business' and a.approval_status != 'Rejected by Credit Manager' and " +
                            " a.approval_status != 'Rejected By Credit' and a.approval_status != 'CC Rejected' and a.approval_status !='Product Approval - Rejected' and a.close_flag !='Y')";
                    lsrenewalprocessnull_application = objdbconn.GetExecuteScalar(msSQL);
                    if (lsrenewalprocessnull_application != "")
                    {
                        lsapplicationrenewal_flag = "N";
                    }
                    else
                    {

                        msSQL = " select a.process_type from agr_mst_tapplication a " +
                            " where a.created_date = (select max(c.created_date) from agr_mst_tapplication c " +
                             " left join agr_mst_tapplication2loan b on c.application_gid = b.application_gid " +
                            " where c.buyeronboard_gid = '" + onboard_gid + "' and b.producttype_gid = '" + dt["producttype_gid"].ToString() + "' and b.productsubtype_gid = '" + dt["productsubtype_gid"].ToString() + "' and c.onboarding_status = 'Proposal' )";
                        lsprocess_type = objdbconn.GetExecuteScalar(msSQL);
                        if (lsprocess_type != "")
                        {
                            //msSQL = " select a.expired_flag from agr_mst_tapplication a " +
                            //   " where  a.processupdated_date = (select max(c.processupdated_date) from agr_mst_tapplication c " +
                            //    " left join agr_mst_tapplication2loan b on c.application_gid = b.application_gid " +
                            //   " where c.buyeronboard_gid = '" + onboard_gid + "' and b.producttype_gid = '" + dt["producttype_gid"].ToString() + "' and c.onboarding_status = 'Proposal' and " +
                            //   " (c.process_type = 'Accept' ) )";
                            //lsapplicationshortclose_flag = objdbconn.GetExecuteScalar(msSQL);
                            //if (lsapplicationshortclose_flag == "Y")
                            //{
                                msSQL = " select max(c.processupdated_date) from agr_mst_tapplication c " +
                                     " left join agr_mst_tapplication2loan b on c.application_gid = b.application_gid " +
                                        " where c.application_gid ='" + dt["application_gid"].ToString() + "' and b.producttype_gid = '" + dt["producttype_gid"].ToString() + "' and b.productsubtype_gid = '" + dt["productsubtype_gid"].ToString() + "' and c.onboarding_status = 'Proposal' and " +
                                        " (c.process_type = 'Accept' )";
                                lsprocessupdated_date = objdbconn.GetExecuteScalar(msSQL);

                                msSQL = " select max(c.processupdated_date) from agr_mst_tapplication c " +
                                     " left join agr_mst_tapplication2loan b on c.application_gid = b.application_gid " +
                                  " where c.buyeronboard_gid = '" + onboard_gid + "' and b.producttype_gid = '" + dt["producttype_gid"].ToString() + "' and b.productsubtype_gid = '" + dt["productsubtype_gid"].ToString() + "' and c.onboarding_status = 'Proposal' and " +
                                  " (c.process_type = 'Accept' ) ";
                                lsmaxprocessupdated_date = objdbconn.GetExecuteScalar(msSQL);
                                if (lsmaxprocessupdated_date == lsprocessupdated_date)
                                {
                                    msSQL = " select a.application_gid from agr_mst_tapplication a " +
                                   " where a.application_gid ='" + dt["application_gid"].ToString() + "' and " +
                                   " a.processupdated_date = (select max(c.processupdated_date) from agr_mst_tapplication c " +
                                    " left join agr_mst_tapplication2loan b on c.application_gid = b.application_gid " +
                                   " where c.buyeronboard_gid = '" + onboard_gid + "' and b.producttype_gid = '" + dt["producttype_gid"].ToString() + "' and b.productsubtype_gid = '" + dt["productsubtype_gid"].ToString() + "' and c.onboarding_status = 'Proposal' and " +
                                   " (c.process_type = 'Accept' ) )";
                                    lsapplicationshortclose_flag = objdbconn.GetExecuteScalar(msSQL);
                                    if (lsapplicationshortclose_flag != "")
                                    {
                                        lsapplicationrenewal_flag = "Y";
                                    }
                                }
                            //}

                        }
                        msSQL = " select a.close_flag from agr_mst_tapplication a " +
                               " where a.created_date = (select max(c.created_date) from agr_mst_tapplication c " +
                                " left join agr_mst_tapplication2loan b on c.application_gid = b.application_gid " +
                               " where c.buyeronboard_gid = '" + onboard_gid + "' and b.producttype_gid = '" + dt["producttype_gid"].ToString() + "' and  b.productsubtype_gid = '" + dt["productsubtype_gid"].ToString() + "'  and c.onboarding_status = 'Proposal')";
                        lsapplicationshortclose_flag = objdbconn.GetExecuteScalar(msSQL);
                        if (lsapplicationshortclose_flag == "Y")
                        {
                            lsapplicationrenewal_flag = "N";
                        }
                        //msSQL = " select a.process_type from agr_mst_tapplication a " +
                        //    " where a.created_date = (select max(c.created_date) from agr_mst_tapplication c " +
                        //     " left join agr_mst_tapplication2loan b on c.application_gid = b.application_gid " +
                        //    " where c.buyeronboard_gid = '" + onboard_gid + "' and b.producttype_gid = '" + dt["producttype_gid"].ToString() + "' and c.onboarding_status = 'Proposal' )";
                        //lsprocess_type = objdbconn.GetExecuteScalar(msSQL);
                        //if (lsprocess_type != "")
                        //{
                        //    msSQL = " select a.expired_flag from agr_mst_tapplication a " +
                        //     " where a.application_gid ='" + dt["application_gid"].ToString() + "'";
                        //    lsapplicationshortclose_flag = objdbconn.GetExecuteScalar(msSQL);
                        //    if (lsapplicationshortclose_flag == "Y")
                        //    {
                        //        lsapplicationrenewal_flag = "N";
                        //    }
                        //}
                        //msSQL = " select a.application_gid from agr_mst_tapplication a " +
                        //   " where a.created_date = (select max(c.created_date) from agr_mst_tapplication c " +
                        //    " left join agr_mst_tapplication2loan b on c.application_gid = b.application_gid " +
                        //   " where c.buyeronboard_gid = '" + onboard_gid + "' and b.producttype_gid = '" + dt["producttype_gid"].ToString() + "' and  c.onboarding_status = 'Proposal' and " +

                        //   " (c.approval_status = 'Rejected By Business' or " +
                        //   " c.approval_status = 'Rejected By Credit' or c.approval_status = 'CC Rejected' or c.close_flag ='Y'))";
                        //lsrejected_flag = objdbconn.GetExecuteScalar(msSQL);
                        //if (lsrejected_flag != "")
                        //{

                        //    msSQL = " select a.application_gid from agr_mst_tapplication a " +
                        //      " where a.application_gid ='" + dt["application_gid"].ToString() + "' and " +
                        //      " a.processupdated_date = (select max(c.processupdated_date) from agr_mst_tapplication c " +
                        //       " left join agr_mst_tapplication2loan b on c.application_gid = b.application_gid " +
                        //      " where c.buyeronboard_gid = '" + onboard_gid + "' and b.producttype_gid = '" + dt["producttype_gid"].ToString() + "' and c.onboarding_status = 'Proposal' and " +
                        //      " (c.process_type = 'Accept' ) and c.expired_flag=='Y')";
                        //    lsapplicationshortclose_flag = objdbconn.GetExecuteScalar(msSQL);
                        //    if (lsapplicationshortclose_flag != "")
                        //    {
                        //        lsapplicationrenewal_flag = "N";
                        //    }

                        //    //msSQL = " select a.application_gid from agr_mst_tapplication a " +
                        //    //   " where a.application_gid ='" + dt["application_gid"].ToString() + "' and " +
                        //    //   " a.processupdated_date = (select max(c.processupdated_date) from agr_mst_tapplication c " +
                        //    //   " where c.buyeronboard_gid = '" + onboard_gid + "' and c.onboarding_status = 'Proposal' and " +
                        //    //   " (c.process_type = 'Accept' ) and c.expired_flag!='Y')";
                        //    //lsapplicationshortclose_flag = objdbconn.GetExecuteScalar(msSQL);
                        //    //if (lsapplicationshortclose_flag != "")
                        //    //{
                        //    //    lsapplicationrenewal_flag = "Y";
                        //    //}
                        //}

                        msSQL = " select a.close_flag from agr_mst_tapplication a " +
                           " where a.application_gid ='" + dt["application_gid"].ToString() + "' and close_flag ='Y'";
                        lsapplicationshortclose_flag = objdbconn.GetExecuteScalar(msSQL);
                        if (lsapplicationshortclose_flag == "Y")
                        {
                            lsapplicationrenewal_flag = "N";
                        }
                    }

                   
                    getMdlRenewalApplicationList.Add(new MdlRenewalApplicationList
                    {
                        application_no = dt["ARN"].ToString(),
                        contract_id = dt["contract_id"].ToString(),
                        contract_status = dt["ContractStatus"].ToString(),
                        application_gid = dt["application_gid"].ToString(),                       
                        product_overallamount = dt["product_overallamount"].ToString(),
                        processupdated_date = dt["processupdated_date"].ToString(),
                        renewal_flag = dt["renewal_flag"].ToString(),
                        amendment_flag = dt["amendment_flag"].ToString(),
                        shortclosing_flag = dt["shortclosing_flag"].ToString(),
                        buyeronboard_gid = dt["buyeronboard_gid"].ToString(),
                        onboarding_status = dt["onboarding_status"].ToString(),
                        applicationrenewal_flag = lsapplicationrenewal_flag
                    });

                }
                values.MdlRenewalApplicationList = getMdlRenewalApplicationList;
            }
            dt_datatable.Dispose();

            msSQL = " select  a.application_gid,concat(product_type, ' / ',productsub_type)  as 'facility', " +
                   " loanfacility_amount as 'ApprovedLimit',utilized_limit as 'UtilizedLimit',available_limit as 'AvailableLimit', " +
                   " date_format(programlimit_validdfrom, '%d-%m-%Y') as 'ValidFrom' , " +
                   " date_format(programlimit_validdto, '%d-%m-%Y') as 'ValidTo' , " +
                   " case when DATE(programlimit_validdto) < DATE(NOW()) then 'Expired' " +
                   " when DATE(programlimit_validdto) >= DATE(NOW())  then 'Active' else '' end as 'FacilityStatus' " +
                   " from agr_mst_tapplication2loan a " +
                   " left join agr_mst_tapplication b on a.application_gid = b.application_gid " +
                   " where b.buyeronboard_gid = '" + onboard_gid + "' and  (b.process_type = 'Accept' or b.close_flag='Y') group by a.application_gid";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getMdlRenewalFaclilityDtl = new List<MdlRenewalFaclilityDtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getMdlRenewalFaclilityDtl.Add(new MdlRenewalFaclilityDtl
                    {
                        facility = dt["Facility"].ToString(),
                        ApprovedLimit = dt["ApprovedLimit"].ToString(),
                        UtilizedLimit = dt["UtilizedLimit"].ToString(),
                        AvailableLimit = dt["AvailableLimit"].ToString(),
                        ValidFrom = dt["ValidFrom"].ToString(),
                        ValidTo = dt["ValidTo"].ToString(),
                        FacilityStatus = dt["FacilityStatus"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                    });

                }
                values.MdlRenewalFaclilityDtl = getMdlRenewalFaclilityDtl;
            }
            dt_datatable.Dispose();

            //msSQL = " select  processupdated_date from agr_mst_tapplication " +
            //   " where buyeronboard_gid = '" + onboard_gid + "' and process_type = 'Accept' and (renewal_flag = 'Y' or amendment_flag = 'Y')";
            //dt_datatable = objdbconn.GetDataTable(msSQL);
            //var getMdlPmgExpiryDate = new List<MdlPmgcloneExpiryDate>();
            //if (dt_datatable.Rows.Count != 0)
            //{
            //    foreach (DataRow dt in dt_datatable.Rows)
            //    {
            //        getMdlPmgExpiryDate.Add(new MdlPmgcloneExpiryDate
            //        {
            //            processupdated_date = dt["processupdated_date"].ToString(),

            //        });

            //    }
            //    values.MdlPmgcloneExpiryDate = getMdlPmgExpiryDate;
            //}
            //dt_datatable.Dispose();

            values.status = true;
        }

        public void DaGetOnboardLimitFacilitydtl(string application_gid, MdlRenewalFaclilityList values)
        {

            msSQL = " select  concat(product_type, ' / ',productsub_type)  as 'facility', " +
                    " loanfacility_amount as 'ApprovedLimit', " +
                    " date_format(programlimit_validdfrom, '%d-%m-%Y') as 'ValidFrom' , " +
                    " date_format(programlimit_validdto, '%d-%m-%Y') as 'ValidTo' , " +
                    " case when DATE(programlimit_validdto) < DATE(NOW()) then 'Expired' " +
                    " when DATE(programlimit_validdto) >= DATE(NOW())  then 'Active' else '' end as 'FacilityStatus' " +
                    " from agr_mst_tapplication2loan where application_gid = '" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getMdlRenewalFaclilityDtl = new List<MdlRenewalFaclilityDtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getMdlRenewalFaclilityDtl.Add(new MdlRenewalFaclilityDtl
                    {
                        facility = dt["Facility"].ToString(),
                        ApprovedLimit = dt["ApprovedLimit"].ToString(),
                        ValidFrom = dt["ValidFrom"].ToString(),
                        ValidTo = dt["ValidTo"].ToString(),
                        FacilityStatus = dt["FacilityStatus"].ToString(),
                    });

                }
                values.MdlRenewalFaclilityDtl = getMdlRenewalFaclilityDtl;
            }
            dt_datatable.Dispose();

            values.status = true;
        }

        public void DaGetApplCloneHistoryDtlLog(string onboard_gid, MdlApplCloneHistoryDtlLog values)
        {

            msSQL = " select application_no,application_gid,renewal_flag, " +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                    " concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as updated_by,amendment_flag, " +
                    " shortclosing_flag from agr_mst_tapplication a " +
                    " left join hrm_mst_temployee d on a.created_by = d.employee_gid" +
                    " left join adm_mst_tuser e on e.user_gid = d.user_gid " +
                    " where buyeronboard_gid = '" + onboard_gid + "'" +
                    " and (renewal_flag='Y' or amendment_flag='Y' or shortclosing_flag='Y') " +
                    " order by a.created_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getApplclonehistorylog_list = new List<Applclonehistorylog_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getApplclonehistorylog_list.Add(new Applclonehistorylog_list
                    {
                        application_gid = dt["application_gid"].ToString(),
                        application_no = dt["application_no"].ToString(),
                        updated_date = dt["updated_date"].ToString(),
                        updated_by = dt["updated_by"].ToString(),
                        renewal_flag = dt["renewal_flag"].ToString(),
                        amendment_flag = dt["amendment_flag"].ToString(),
                        shortclosing_flag = dt["shortclosing_flag"].ToString()
                    });

                }
                values.Applclonehistorylog_list = getApplclonehistorylog_list;
            }
            dt_datatable.Dispose();

            values.status = true;
        }
        // Get Amentment Master List
        public void DaGetAmentmentMasterList(MdlAmentmentMasterList objMdlAmentmentMasterList)
        {
            msSQL = "select amendment_gid,amendment from agr_mst_tamendment where status='Y'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcloneamendment_list = new List<cloneamendment_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                objMdlAmentmentMasterList.cloneamendment_list = dt_datatable.AsEnumerable().Select(row => new cloneamendment_list
                {
                    amendment_gid = row["amendment_gid"].ToString(),
                    amendment = row["amendment"].ToString()
                }).ToList();
            }
            dt_datatable.Dispose();
        }
        // Application Renewal
        public bool DaPostAmendmentAdd(string employee_gid, string user_gid, MdlMstCloneAmendmentAdd values)
        {
            //msSQL = " select a.application_no,a.application_gid,a.processupdated_date as dateofapp " +
            //        " from agr_mst_tapplication a " +
            //        " where a.processupdated_date = (select max(c.processupdated_date) from agr_mst_tapplication c " +
            //        " where c.buyeronboard_gid = '" + values.buyer_gid + "' and a.onboarding_status = 'Proposal' and a.process_type = 'Accept') " +
            //        " union " +
            //        " select p.application_no,p.application_gid, p.headapproval_date as dateofapp from agr_mst_tapplication p " +
            //        " where (p.amendment_flag = 'Y' and p.approval_status like '%Rejected%' and p.headapproval_date = (select max(b.headapproval_date) from agr_mst_tapplication b " +
            //        " where b.approval_status like '%Rejected%' and b.buyeronboard_gid = '" + values.buyer_gid + "')) " +
            //        " union " +
            //        " select q.application_no,q.application_gid,q.creditheadapproval_date as dateofapp from agr_mst_tapplication q " +
            //        " where (q.amendment_flag = 'Y' and q.approval_status like '%Rejected%' and q.creditheadapproval_date = (select max(b.creditheadapproval_date) from agr_mst_tapplication b " +
            //        " where b.approval_status like '%Rejected%' and b.buyeronboard_gid = '" + values.buyer_gid + "')) " +
            //        " union " +
            //        " select r.application_no,r.application_gid,r.cccompleted_date as dateofapp from agr_mst_tapplication r " +
            //        " where (r.amendment_flag = 'Y' and r.approval_status like '%Rejected%' and r.cccompleted_date = (select max(b.cccompleted_date) from agr_mst_tapplication b " +
            //        " where b.approval_status like '%Rejected%' and b.buyeronboard_gid = '" + values.buyer_gid + "')) " +
            //        " order by dateofapp desc limit 1 ";
            //objODBCDataReader = objdbconn.GetDataReader(msSQL);
            //if (objODBCDataReader.HasRows == true)
            //{
            //    values.refapplication_no = objODBCDataReader["application_no"].ToString();
            //    values.refapplication_gid = objODBCDataReader["application_gid"].ToString();
            //    values.dateofapp = objODBCDataReader["dateofapp"].ToString();
            //}
            //objODBCDataReader.Close();

            ////msSQL = " select a.application_no,a.application_gid,a.processupdated_date as dateofapp " +
            ////        " from agr_mst_tapplication a " +
            ////        " where a.processupdated_date = (select max(c.processupdated_date) from agr_mst_tapplication c " +
            ////        " where c.buyeronboard_gid = '" + values.buyer_gid + "' and c.onboarding_status = 'Proposal' and c.process_type = 'Accept'and expired_flag ='N') ";
            ////objODBCDataReader = objdbconn.GetDataReader(msSQL);
            ////if (objODBCDataReader.HasRows == true)
            ////{
            ////    values.application_no = objODBCDataReader["application_no"].ToString();
            ////    values.application_gid = objODBCDataReader["application_gid"].ToString();
            ////}
            ////objODBCDataReader.Close();


            //msSQL = " select a.shortclosing_flag from agr_mst_tapplication a " +
            //      " where a.created_date = (select max(c.created_date) from agr_mst_tapplication c " +
            //      " where c.buyeronboard_gid = '" + values.buyer_gid + "' and c.onboarding_status = 'Proposal') ";
            //lsshortclosing_flag = objdbconn.GetExecuteScalar(msSQL);
            //if (lsshortclosing_flag != "N")
            //{
            //    msSQL = " select a.approval_status from agr_mst_tapplication a " +
            //            " where a.created_date = (select max(c.created_date) from agr_mst_tapplication c " +
            //            " where c.buyeronboard_gid = '" + values.buyer_gid + "' and c.onboarding_status = 'Proposal') ";
            //    lsapproval_status = objdbconn.GetExecuteScalar(msSQL);
            //    if (lsapproval_status == "Rejected By Business" || lsapproval_status == "Rejected By Credit" || lsapproval_status == "CC Rejected")
            //    {
            //        msSQL = " select a.application_no,a.application_gid,a.processupdated_date as dateofapp " +
            //      " from agr_mst_tapplication a " +
            //      " where a.processupdated_date = (select max(c.processupdated_date) from agr_mst_tapplication c " +
            //      " where c.buyeronboard_gid = '" + values.buyer_gid + "' and c.onboarding_status = 'Proposal' and c.process_type = 'Accept' ) ";
            //        objODBCDataReader = objdbconn.GetDataReader(msSQL);
            //        if (objODBCDataReader.HasRows == true)
            //        {
            //            values.application_no = objODBCDataReader["application_no"].ToString();
            //            values.application_gid = objODBCDataReader["application_gid"].ToString();
            //        }
            //        objODBCDataReader.Close();
            //    }
            //    else
            //    {
            //        msSQL = " select a.application_no,a.application_gid,a.processupdated_date as dateofapp " +
            //        " from agr_mst_tapplication a " +
            //        " where a.processupdated_date = (select max(c.processupdated_date) from agr_mst_tapplication c " +
            //        " where c.buyeronboard_gid = '" + values.buyer_gid + "' and c.onboarding_status = 'Proposal' and c.process_type = 'Accept'  and expired_flag !='Y' ) ";
            //        objODBCDataReader = objdbconn.GetDataReader(msSQL);
            //        if (objODBCDataReader.HasRows == true)
            //        {
            //            values.application_no = objODBCDataReader["application_no"].ToString();
            //            values.application_gid = objODBCDataReader["application_gid"].ToString();
            //        }
            //        objODBCDataReader.Close();
            //    }

            //}
            //else
            //{
            //    msSQL = " select a.approval_status from agr_mst_tapplication a " +
            //      " where a.created_date = (select max(c.created_date) from agr_mst_tapplication c " +
            //      " where c.buyeronboard_gid = '" + values.buyer_gid + "' and c.onboarding_status = 'Proposal' and shortclosing_flag ='Y')";
            //    lsapproval_status_check = objdbconn.GetExecuteScalar(msSQL);
            //    if (lsapproval_status_check == "Rejected By Business" || lsapproval_status_check == "Rejected By Credit" || lsapproval_status_check == "CC Rejected")
            //    {
            //        msSQL = " select a.application_no,a.application_gid,a.processupdated_date as dateofapp " +
            //        " from agr_mst_tapplication a " +
            //        " where a.processupdated_date = (select max(c.processupdated_date) from agr_mst_tapplication c " +
            //        " where c.buyeronboard_gid = '" + values.buyer_gid + "' and c.onboarding_status = 'Proposal' and c.process_type = 'Accept' ) ";
            //        objODBCDataReader = objdbconn.GetDataReader(msSQL);
            //        if (objODBCDataReader.HasRows == true)
            //        {
            //            values.application_no = objODBCDataReader["application_no"].ToString();
            //            values.application_gid = objODBCDataReader["application_gid"].ToString();
            //        }
            //        objODBCDataReader.Close();
            //    }
            //    else
            //    {
            //        msSQL = " select a.application_no,a.application_gid,a.processupdated_date as dateofapp " +
            //        " from agr_mst_tapplication a " +
            //        " where a.processupdated_date = (select max(c.processupdated_date) from agr_mst_tapplication c " +
            //        " where c.buyeronboard_gid = '" + values.buyer_gid + "' and c.onboarding_status = 'Proposal' and c.process_type = 'Accept'  and expired_flag !='Y' ) ";
            //        objODBCDataReader = objdbconn.GetDataReader(msSQL);
            //        if (objODBCDataReader.HasRows == true)
            //        {
            //            values.application_no = objODBCDataReader["application_no"].ToString();
            //            values.application_gid = objODBCDataReader["application_gid"].ToString();
            //        }
            //        objODBCDataReader.Close();
            //    }

            //}

            msSQL = " select b.producttype_gid,b.productsubtype_gid,application_no,customer_urn,customer_name,entity_gid,entity_name, " +
                    " vertical_gid,vertical_name,verticaltaggs_gid,verticaltaggs_name,constitution_gid,constitution_name,businessunit_gid, " +
                    " businessunit_name,sa_status as sastatus,sa_id,sa_name,baselocation_gid,baselocation_name,cluster_gid,cluster_name,region_gid, " +
                    " region_name,zone_gid,zone_name,relationshipmanager_name,relationshipmanager_gid,drm_gid,drm_name,clustermanager_gid, " +
                    " clustermanager_name,zonalhead_name,zonalhead_gid,regionalhead_name,regionalhead_gid,businesshead_name,businesshead_gid, " +
                    " vernacular_language,vernacularlanguage_gid,contactpersonfirst_name,contactpersonmiddle_name, " +
                    " contactpersonlast_name,designation_gid,designation_type,landline_no,status as application_status, " +
                    " saname_gid,economical_flag,productcharge_flag,applicant_type,customerref_name, " +
                    " productcharges_status,mobile_no,email_address,approval_flag,gradingdraft_flag,hypothecation_flag,submitted_by, " +
                    " date_format(submitted_date, '%Y-%m-%d %H:%i:%s') as submitted_date,region,creditgroup_gid,creditgroup_name, " +
                    " a.program_gid,a.program_name,a.product_gid,a.product_name,a.sector_name,a.category_name,a.variety_gid,a.variety_name, " +
                    " a.botanical_name,a.alternative_name,approval_status,document_name,document_path,renewal_flag,amendment_flag, " +
                    " validityfrom_date,validityto_date,onboarding_status," +
                    " productdesk_flag,productdesk_gid,productquery_status,contract_id,buyeronboard_gid, " +
                    " social_capital,trade_capital,overalllimit_amount,calculationoveralllimit_validity " +
                    " from agr_mst_tapplication a" +
                    " left join agr_mst_tapplication2loan b on a.application_gid = b.application_gid " +
                    " where a.application_gid='" + values.application_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.application_no = objODBCDatareader["application_no"].ToString();
                values.customer_urn = objODBCDatareader["customer_urn"].ToString();
                values.customer_name = objODBCDatareader["customer_name"].ToString();
                values.entity_gid = objODBCDatareader["entity_gid"].ToString();
                values.entity_name = objODBCDatareader["entity_name"].ToString();
                values.vertical_gid = objODBCDatareader["vertical_gid"].ToString();
                values.vertical_name = objODBCDatareader["vertical_name"].ToString();
                values.verticaltaggs_gid = objODBCDatareader["verticaltaggs_gid"].ToString();
                values.verticaltaggs_name = objODBCDatareader["verticaltaggs_name"].ToString();
                values.constitution_gid = objODBCDatareader["constitution_gid"].ToString();
                values.constitution_name = objODBCDatareader["constitution_name"].ToString();
                values.businessunit_gid = objODBCDatareader["businessunit_gid"].ToString();
                values.businessunit_name = objODBCDatareader["businessunit_name"].ToString();
                values.sastatus = objODBCDatareader["sastatus"].ToString();
                values.sa_id = objODBCDatareader["sa_id"].ToString();
                values.sa_name = objODBCDatareader["sa_name"].ToString();
                values.baselocation_gid = objODBCDatareader["baselocation_gid"].ToString();
                values.baselocation_name = objODBCDatareader["baselocation_name"].ToString();
                values.cluster_gid = objODBCDatareader["cluster_gid"].ToString();
                values.cluster_name = objODBCDatareader["cluster_name"].ToString();
                values.region_gid = objODBCDatareader["region_gid"].ToString();
                values.region_name = objODBCDatareader["region_name"].ToString();
                values.zone_gid = objODBCDatareader["zone_gid"].ToString();
                values.zone_name = objODBCDatareader["zone_name"].ToString();
                values.relationshipmanager_name = objODBCDatareader["relationshipmanager_name"].ToString();
                values.relationshipmanager_gid = objODBCDatareader["relationshipmanager_gid"].ToString();
                values.drm_gid = objODBCDatareader["drm_gid"].ToString();
                values.drm_name = objODBCDatareader["drm_name"].ToString();
                values.clustermanager_gid = objODBCDatareader["clustermanager_gid"].ToString();
                values.clustermanager_name = objODBCDatareader["clustermanager_name"].ToString();
                values.zonalhead_name = objODBCDatareader["zonalhead_name"].ToString();
                values.zonalhead_gid = objODBCDatareader["zonalhead_gid"].ToString();
                values.regionalhead_name = objODBCDatareader["regionalhead_name"].ToString();
                values.regionalhead_gid = objODBCDatareader["regionalhead_gid"].ToString();
                values.businesshead_name = objODBCDatareader["businesshead_name"].ToString();
                values.businesshead_gid = objODBCDatareader["businesshead_gid"].ToString();
                values.vernacular_language = objODBCDatareader["vernacular_language"].ToString();
                values.vernacularlanguage_gid = objODBCDatareader["vernacularlanguage_gid"].ToString();
                values.contactpersonfirst_name = objODBCDatareader["contactpersonfirst_name"].ToString();
                values.contactpersonmiddle_name = objODBCDatareader["contactpersonmiddle_name"].ToString();
                values.contactpersonlast_name = objODBCDatareader["contactpersonlast_name"].ToString();
                values.designation_gid = objODBCDatareader["designation_gid"].ToString();
                values.designation_type = objODBCDatareader["designation_type"].ToString();
                values.landline_no = objODBCDatareader["landline_no"].ToString();
                values.application_status = objODBCDatareader["application_status"].ToString();
                values.saname_gid = objODBCDatareader["saname_gid"].ToString();
                values.economical_flag = objODBCDatareader["economical_flag"].ToString();
                values.productcharge_flag = objODBCDatareader["productcharge_flag"].ToString();
                values.applicant_type = objODBCDatareader["applicant_type"].ToString();
                values.customerref_name = objODBCDatareader["customerref_name"].ToString();
                values.productcharges_status = objODBCDatareader["productcharges_status"].ToString();
                values.approval_status = objODBCDatareader["approval_status"].ToString();
                values.mobile_no = objODBCDatareader["mobile_no"].ToString();
                values.email_address = objODBCDatareader["email_address"].ToString();
                values.approval_flag = objODBCDatareader["approval_flag"].ToString();
                values.gradingdraft_flag = objODBCDatareader["gradingdraft_flag"].ToString();
                values.hypothecation_flag = objODBCDatareader["hypothecation_flag"].ToString();
                values.submitted_by = objODBCDatareader["submitted_by"].ToString();
                values.submitted_date = objODBCDatareader["submitted_date"].ToString();
                values.region = objODBCDatareader["region"].ToString();
                values.creditgroup_gid = objODBCDatareader["creditgroup_gid"].ToString();
                values.creditgroup_name = objODBCDatareader["creditgroup_name"].ToString();
                values.program_gid = objODBCDatareader["program_gid"].ToString();
                values.program_name = objODBCDatareader["program_name"].ToString();
                values.product_gid = objODBCDatareader["product_gid"].ToString();
                values.product_name = objODBCDatareader["product_name"].ToString();
                values.sector_name = objODBCDatareader["sector_name"].ToString();
                values.category_name = objODBCDatareader["category_name"].ToString();
                values.variety_gid = objODBCDatareader["variety_gid"].ToString();
                values.variety_name = objODBCDatareader["variety_name"].ToString();
                values.botanical_name = objODBCDatareader["botanical_name"].ToString();
                values.alternative_name = objODBCDatareader["alternative_name"].ToString();
                values.document_name = objODBCDatareader["document_name"].ToString();
                values.document_path = objODBCDatareader["document_path"].ToString();
                values.validityfrom_date = objODBCDatareader["validityfrom_date"].ToString();
                values.validityto_date = objODBCDatareader["validityto_date"].ToString();
                values.onboarding_status = objODBCDatareader["onboarding_status"].ToString();               
                values.productdesk_flag = objODBCDatareader["productdesk_flag"].ToString();
                values.productdesk_gid = objODBCDatareader["productdesk_gid"].ToString();
                values.productquery_status = objODBCDatareader["productquery_status"].ToString();
                values.contract_id = objODBCDatareader["contract_id"].ToString();
                values.buyeronboard_gid = objODBCDatareader["buyeronboard_gid"].ToString();
                values.social_capital = objODBCDatareader["social_capital"].ToString();
                values.trade_capital = objODBCDatareader["trade_capital"].ToString();
                values.overalllimit_amount = objODBCDatareader["overalllimit_amount"].ToString();
                values.calculationoveralllimit_validity = objODBCDatareader["calculationoveralllimit_validity"].ToString();
                values.amendment_flag = objODBCDatareader["amendment_flag"].ToString();
                values.producttype_gid = objODBCDatareader["producttype_gid"].ToString();
                values.productsubtype_gid = objODBCDatareader["productsubtype_gid"].ToString();
                
            }
            objODBCDatareader.Close();

            msSQL = "select amendment_flag from agr_mst_tapplication a" +
                    " where buyeronboard_gid = '" + values.buyer_gid + "'" +
                    " and a.created_date = (select max(c.created_date) from agr_mst_tapplication c " +
                    " left join agr_mst_tapplication2loan b on c.application_gid = b.application_gid " +
                    " where c.buyeronboard_gid = '" + values.buyer_gid + "' and producttype_gid = '" + values.producttype_gid + "' and productsubtype_gid =  '" + values.productsubtype_gid + "')";
            string lslatestamendment_flag = objdbconn.GetExecuteScalar(msSQL);
            if (lslatestamendment_flag == "Y")
            {
                msSQL = " select a.application_no from agr_mst_tapplication a " +
                        " where buyeronboard_gid = '" + values.buyer_gid + "'" +
                        " and a.created_date = (select max(c.created_date) from agr_mst_tapplication c " +
                       " left join agr_mst_tapplication2loan b on c.application_gid = b.application_gid " +
                    " where c.buyeronboard_gid = '" + values.buyer_gid + "' and producttype_gid = '" + values.producttype_gid + "' and productsubtype_gid =  '" + values.productsubtype_gid + "')";
                values.lslatestapplication_refno = objdbconn.GetExecuteScalar(msSQL);
            }
            else
            {
                msSQL = " select a.application_no from agr_mst_tapplication a " +
                       " where buyeronboard_gid = '" + values.buyer_gid + "'" +
                       " and a.created_date = (select max(c.created_date) from agr_mst_tapplication c " +
                      " left join agr_mst_tapplication2loan b on c.application_gid = b.application_gid " +
                   " where c.buyeronboard_gid = '" + values.buyer_gid + "' and producttype_gid = '" + values.producttype_gid + "' and productsubtype_gid =  '" + values.productsubtype_gid + "' and amendment_flag ='Y')";
                values.lslatestapplication_refno = objdbconn.GetExecuteScalar(msSQL);
                if (values.lslatestapplication_refno == "")
                {
                    msSQL = " select a.application_no from agr_mst_tapplication a " +
                        " where a.processupdated_date = (select max(c.processupdated_date) from agr_mst_tapplication c " +
                        " left join agr_mst_tapplication2loan b on c.application_gid = b.application_gid " +
                        " where c.buyeronboard_gid = '" + values.buyer_gid + "' and " +
                        " c.onboarding_status = 'Proposal' and c.process_type = 'Accept' and producttype_gid = '" + values.producttype_gid + "' and productsubtype_gid =  '" + values.productsubtype_gid + "') ";
                values.lslatestapplication_refno = objdbconn.GetExecuteScalar(msSQL);
                }
            }

            msSQL = " select amendment_flag from agr_mst_tapplication where application_no='" + values.lslatestapplication_refno + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                values.applicationamendment_flag = objODBCDataReader["amendment_flag"].ToString();
            }
            objODBCDataReader.Close();

            msSQL = "select vertical_refno from ocs_mst_tvertical where vertical_gid='" + values.vertical_gid + "'";
            string lsvertical_refno = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select entity_gid from ocs_mst_tvertical where vertical_gid='" + values.vertical_gid + "'";
            string lsentity_gid = objdbconn.GetExecuteScalar(msSQL);

            //msSQL = "select entity_code from adm_mst_tentity where entity_gid='" + lsentity_gid + "'";
            //string lsentity_code = objdbconn.GetExecuteScalar(msSQL);
            string lsentity_code = "SA";

            string lsapp_refno = "ARN" + lsentity_code + lsvertical_refno + DateTime.Now.ToString("ddMMyyyy");

            string msGETRef = objcmnfunctions.GetMasterGID("APP");
            msGETRef = msGETRef.Replace("APP", "");
            string finalInt = "";
            if (values.applicationamendment_flag == "Y")
            {
                string pattern = @"[\d]{1}AM(.*)";
                Match m = Regex.Match(values.lslatestapplication_refno, pattern, RegexOptions.IgnoreCase);
                string toBeSearched = "AM";
                string tempString = m.Value.Substring(m.Value.IndexOf(toBeSearched) + toBeSearched.Length);

                //string tempString = m.Value.Replace("RN", String.Empty);
                int tempInt = Int16.Parse(tempString) + 1;
                if (tempInt >= 1 && tempInt <= 9)
                {
                    finalInt = tempInt.ToString().PadLeft(2, '0');
                }
                else
                {
                    finalInt = tempInt.ToString();
                }
            }
            else
            {
                finalInt = "01";
            }

            lsapp_refno = lsapp_refno + msGETRef + "AM" + finalInt;

            // Insert data in Application Table
            msGetGidApplication = objcmnfunctions.GetMasterGID("APPC");
            msSQL = " insert into agr_mst_tapplication( " +
                    " application_gid ," +
                    " application_no ," +
                    " refapplication_gid ," +
                    " refapplication_no ," +
                    " customer_urn ," +
                    " customer_name ," +
                    " entity_gid ," +
                    " entity_name ," +
                    " vertical_gid ," +
                    " vertical_name ," +
                    " verticaltaggs_gid ," +
                    " verticaltaggs_name ," +
                    " constitution_gid ," +
                    " constitution_name ," +
                    " businessunit_gid ," +
                    " businessunit_name ," +
                    " sa_status ," +
                    " sa_id ," +
                    " sa_name ," +
                    " baselocation_gid ," +
                    " baselocation_name ," +
                    " cluster_gid ," +
                    " cluster_name ," +
                    " region_gid ," +
                    " region_name ," +
                    " zone_gid ," +
                    " zone_name ," +
                    " relationshipmanager_name ," +
                    " relationshipmanager_gid ," +
                    " drm_gid ," +
                    " drm_name ," +
                    " clustermanager_gid ," +
                    " clustermanager_name ," +
                    " zonalhead_name ," +
                    " zonalhead_gid ," +
                    " regionalhead_name ," +
                    " regionalhead_gid ," +
                    " businesshead_name ," +
                    " businesshead_gid ," +
                    " vernacular_language ," +
                    " vernacularlanguage_gid ," +
                    " contactpersonfirst_name ," +
                    " contactpersonmiddle_name ," +
                    " contactpersonlast_name ," +
                    " designation_gid ," +
                    " designation_type ," +
                    " landline_no ," +
                    " social_capital ," +
                    " trade_capital ," +
                    " overalllimit_amount ," +
                    " calculationoveralllimit_validity ," +
                    " status ," +
                    " saname_gid ," +
                    " economical_flag ," +
                    " productcharge_flag ," +
                    " applicant_type ," +
                    " customerref_name ," +
                    " approval_status ," +
                    " mobile_no ," +
                    " email_address ," +
                    " approval_flag ," +
                    " gradingdraft_flag ," +
                    " hypothecation_flag ," +
                    " ccsubmit_flag ," +
                    " meeting_status ," +
                    " region ," +
                    " momapproval_flag ," +
                    " mom_flag ," +
                    " momdocumentupload_flag ," +
                    " headapproval_status ," +
                    " document_name ," +
                    " document_path ," +
                    " creditgroup_gid ," +
                    " creditgroup_name ," +
                    " creditgroup_status ," +
                    " creditheadapproval_status ," +
                    " program_gid ," +
                    " program_name ," +
                    " docchecklist_makerflag ," +
                    " docchecklist_checkerflag ," +
                    " docchecklist_approvalflag ," +
                    " cccompleted_flag ," +
                    " hierarchyupdated_flag ," +
                    " product_gid ," +
                    " product_name ," +
                    " sector_name ," +
                    " category_name ," +
                    " variety_gid ," +
                    " variety_name ," +
                    " botanical_name ," +
                    " alternative_name ," +
                    " pslcompleted_flag ," +
                    " sanction_approvalflag ," +
                    " amendment_flag," +
                    " validityfrom_date," +
                    " validityto_date," +
                    " onboarding_status," +                   
                    " productdesk_flag," +
                    " productdesk_gid," +
                    " productquery_status," +
                    " contract_id," +
                    " buyeronboard_gid," +
                    " productcharges_status," +
                    " amendment_remarks, " +
                    " created_by , " +
                    " created_date ) " +
                    " values (" +
                   "'" + msGetGidApplication + "'," +
                   "'" + lsapp_refno + "'," +
                   "'" + values.application_gid + "'," +
                   "'" + values.application_no + "'," +
                   "'" + values.customer_urn + "'," +
                   "'" + values.customer_name + "'," +
                   "'" + values.entity_gid + "'," +
                   "'" + values.entity_name + "'," +
                   "'" + values.vertical_gid + "'," +
                   "'" + values.vertical_name + "'," +
                   "'" + values.verticaltaggs_gid + "'," +
                   "'" + values.verticaltaggs_name + "'," +
                   "'" + values.constitution_gid + "'," +
                   "'" + values.constitution_name + "'," +
                   "'" + values.businessunit_gid + "'," +
                   "'" + values.businessunit_name + "'," +
                   "'" + values.sastatus + "'," +
                   "'" + values.sa_id + "'," +
                   "'" + values.sa_name + "'," +
                   "'" + values.baselocation_gid + "'," +
                   "'" + values.baselocation_name + "'," +
                   "'" + values.cluster_gid + "'," +
                   "'" + values.cluster_name + "'," +
                   "'" + values.region_gid + "'," +
                   "'" + values.region_name + "'," +
                   "'" + values.zone_gid + "'," +
                   "'" + values.zone_name + "'," +
                   "'" + values.relationshipmanager_name + "'," +
                   "'" + values.relationshipmanager_gid + "'," +
                   "'" + values.drm_gid + "'," +
                   "'" + values.drm_name + "'," +
                   "'" + values.clustermanager_gid + "'," +
                   "'" + values.clustermanager_name + "'," +
                   "'" + values.zonalhead_name + "'," +
                   "'" + values.zonalhead_gid + "'," +
                   "'" + values.regionalhead_name + "'," +
                   "'" + values.regionalhead_gid + "'," +
                   "'" + values.businesshead_name + "'," +
                   "'" + values.businesshead_gid + "'," +
                   "'" + values.vernacular_language + "'," +
                   "'" + values.vernacularlanguage_gid + "'," +
                   "'" + values.contactpersonfirst_name + "'," +
                   "'" + values.contactpersonmiddle_name + "'," +
                   "'" + values.contactpersonlast_name + "'," +
                   "'" + values.designation_gid + "'," +
                   "'" + values.designation_type + "'," +
                   "'" + values.landline_no + "'," +
                   "'" + values.social_capital + "'," +
                   "'" + values.trade_capital + "'," +
                   "'" + values.overalllimit_amount + "'," +
                   "'" + values.calculationoveralllimit_validity + "'," +
                   "'Completed'," +
                   "'" + values.saname_gid + "'," +
                   "'Y'," +
                   "'Y'," +
                   "'" + values.applicant_type + "'," +
                   "'" + values.customerref_name + "'," +
                   "'Incomplete'," +
                   "'" + values.mobile_no + "'," +
                   "'" + values.email_address + "'," +
                   "'N'," +
                   "'N'," +
                   "'N'," +
                   "'N'," +
                   "'Pending'," +
                   "'" + values.region + "'," +
                   "'N'," +
                   "'N'," +
                   "'N'," +
                   "'Pending'," +
                   "'" + values.document_name + "'," +
                   "'" + values.document_path + "'," +
                   "'" + values.creditgroup_gid + "'," +
                   "'" + values.creditgroup_name + "'," +
                   "'Pending'," +
                   "'Pending'," +
                   "'" + values.program_gid + "'," +
                   "'" + values.program_name + "'," +
                   "'N', " +
                   "'N', " +
                   "'N', " +
                   "'N', " +
                   "'N'," +
                   "'" + values.product_gid + "'," +
                   "'" + values.product_name + "'," +
                   "'" + values.sector_name + "'," +
                   "'" + values.category_name + "'," +
                   "'" + values.variety_gid + "'," +
                   "'" + values.variety_name + "'," +
                   "'" + values.botanical_name + "'," +
                   "'" + values.alternative_name + "'," +
                   "'N'," +
                   "'N', " +
                   "'Y'," +
                   "'" + Convert.ToDateTime(values.validityfrom_date).ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                   "'" + Convert.ToDateTime(values.validityto_date).ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                   "'" + values.onboarding_status + "'," +
                   "'" + values.productdesk_flag + "'," +
                   "'" + values.productdesk_gid + "'," +
                   "'" + values.productquery_status + "'," +
                   "'" + values.contract_id + "'," +
                   "'" + values.buyeronboard_gid + "'," +
                   "'" + values.productcharges_status + "'," +
                   "'" + values.amendment_remarks.Replace("'", "") + "'," +
                   "'" + employee_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResultapplicationcreation = objdbconn.ExecuteNonQuerySQL(msSQL);

            // Insert data in Amendment reason Table
            for (var i = 0; i < values.amendmentreason.Count; i++)
            {
                msGetamendmentreason_gid = objcmnfunctions.GetMasterGID("AMTR");

                msSQL = " insert into agr_mst_tamendmentreason( " +
                       " amendmentreason_gid, " +
                       " application_gid," +
                       " buyeronboard_gid," +
                       " amendment_gid," +
                       " amendment," +
                       " created_by," +
                       " created_date)" +
                       " values(" +
                       "'" + msGetamendmentreason_gid + "'," +
                       "'" + msGetGidApplication + "'," +
                       "'" + values.buyer_gid + "'," +
                       "'" + values.amendmentreason[i].amendment_gid + "'," +
                       "'" + values.amendmentreason[i].amendment.Replace("'", "") + "'," +
                       "'" + employee_gid + "'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResultamendmentreason = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            // Insert data in Onboardinitiate Detail Table
            msSQL = "select onboardinitiatedtl_gid from agr_mst_tonboardinitiatedtl where application_gid='" + values.application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msSQL = " insert into agr_mst_tonboardinitiatedtl " +
                            " (buyeronboard_gid, supplieronboard_gid, application_gid, product_gid, " +
                            " product_name, program_gid, program_name, initiated_remarks, amendment_flag, created_byname, " +
                            " created_by, created_date) " +
                            " select buyeronboard_gid,supplieronboard_gid, @application_gid:= '" + msGetGidApplication + "',product_gid, " +
                            " product_name, program_gid, program_name, initiated_remarks, @amendment_flag:= 'Y', created_byname, " +
                            " created_by, @created_date:= '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                            " from agr_mst_tonboardinitiatedtl " +
                            " where application_gid='" + values.application_gid + "'";
                    mnResultonboardinitiate = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            else
            {
                mnResultonboardinitiate = 1;
            }

            // Insert data in Application2Contact Table
            msSQL = "select application2contact_gid from agr_mst_tapplication2contactno where application_gid='" + values.application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msGetGidContactno = objcmnfunctions.GetMasterGID("A2CN");
                    msSQL = " insert into agr_mst_tapplication2contactno " +
                            " (application2contact_gid, application_gid, mobile_no, primary_mobileno, whatsapp_mobileno, created_by," +
                            " created_date, updated_by, updated_date) " +
                            " select @application2contact_gid := '" + msGetGidContactno + "', @application_gid:= '" + msGetGidApplication + "', mobile_no, " +
                            " primary_mobileno,whatsapp_mobileno,created_by,created_date, updated_by," +
                            " updated_date from agr_mst_tapplication2contactno " +
                            " where application_gid='" + values.application_gid + "'" +
                            " and application2contact_gid='" + dt["application2contact_gid"].ToString() + "'";
                    mnResultapplicationcontact = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            else
            {
                mnResultapplicationcontact = 1;
            }

            // Insert data in Application2Email Table
            msSQL = "select application2email_gid from agr_mst_tapplication2email where application_gid='" + values.application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msGetGidemail = objcmnfunctions.GetMasterGID("A2EA");
                    msSQL = " insert into agr_mst_tapplication2email " +
                            " (application2email_gid, application_gid, email_address, primary_emailaddress,created_by," +
                            " created_date, updated_by, updated_date) " +
                            " select @application2email_gid := '" + msGetGidemail + "', @application_gid:= '" + msGetGidApplication + "', email_address, " +
                            " primary_emailaddress,created_by,created_date, updated_by," +
                            " updated_date from agr_mst_tapplication2email " +
                            " where application_gid='" + values.application_gid + "'" +
                            " and application2email_gid='" + dt["application2email_gid"].ToString() + "'";
                    mnResultapplicationemail = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            else
            {
                mnResultapplicationemail = 1;
            }

            // Insert data in Application2GeneticCode Table from CAD Table
            msSQL = "select application2geneticcode_gid from agr_mst_tapplication2geneticcode where application_gid='" + values.application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msGetGidgeneticcode = objcmnfunctions.GetMasterGID("A2GC");
                    msSQL = " insert into agr_mst_tapplication2geneticcode " +
                            " (application2geneticcode_gid, application_gid, geneticcode_gid, geneticcode_name,genetic_status,genetic_remarks," +
                            " created_by, created_date, updated_by, updated_date) " +
                            " select @application2geneticcode_gid := '" + msGetGidgeneticcode + "', @application_gid:= '" + msGetGidApplication + "',  " +
                            " geneticcode_gid, geneticcode_name,genetic_status,genetic_remarks,created_by,created_date, updated_by," +
                            " updated_date from agr_mst_tapplication2geneticcode " +
                            " where application_gid='" + values.application_gid + "' " +
                            " and application2geneticcode_gid='" + dt["application2geneticcode_gid"].ToString() + "'";
                    mnResultapplicationgeneticcode = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            else
            {
                mnResultapplicationgeneticcode = 1;
            }
            // Insert data in Institution Table 
            msSQL = "select institution_gid from agr_mst_tinstitution where application_gid='" + values.application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msGetGidinstitution = objcmnfunctions.GetMasterGID("APIN");
                    msSQL = " insert into agr_mst_tinstitution " +
                            " (institution_gid,application_gid,application_no,company_name,date_incorporation," +
                            " form60document_name,form60document_path,companypan_no,year_business,month_business,cin_no,official_telephoneno," +
                            " officialemail_address,companytype_gid,companytype_name,stakeholder_type,stakeholdertype_gid,assessmentagency_gid," +
                            " assessmentagency_name,assessmentagencyrating_gid,assessmentagencyrating_name,ratingas_on,amlcategory_gid,amlcategory_name," +
                            " businesscategory_gid,businesscategory_name,contactperson_firstname,contactperson_middlename,contactperson_lastname," +
                            " contactperson_contactno,designation_gid,designation,lastyear_turnover,escrow,start_date,end_date,created_by," +
                            " created_date,updated_date,updated_by,urn,urn_status,observations,bureau_response,institution_status," +
                            " businessstart_date,bureauname_name,bureau_score,bureauname_gid,bureauscore_date,cicdocument_name,cicdocument_path," +
                            " mobile_no,email_address,economical_status,social_capital,trade_capital,official_mailid,economical_flag," +
                            " startupasofloansanction_date,occupation_gid,occupation,lineofactivity_gid,lineofactivity,bsrcode_gid,bsrcode," +
                            " pslcategory_gid,pslcategory,weakersection_gid,weakersection,pslpurpose_gid,pslpurpose,totalsanction_financialinstitution," +
                            " pslsanction_limit,natureofentity_gid,natureofentity,indulgeinmarketing_activity,plantandmachineryinvestment_gid," +
                            " plantandmachineryinvestment,turnover_gid,turnover,msmeclassification_gid,msmeclassification,loansanction_date," +
                            " entityincorporation_date,hq_metropolitancity,clientdtl_gid,client_dtl,psltagging_flag,credit_status,tan_number," +
                            " sundrydebt_adv,fixed_assets,profit,revenue,incometax_returnsstatus) " +
                            " select @institution_gid := '" + msGetGidinstitution + "', @application_gid:= '" + msGetGidApplication + "', application_no,company_name,date_incorporation, " +
                            " form60document_name,form60document_path,companypan_no,year_business,month_business,cin_no,official_telephoneno," +
                            " officialemail_address,companytype_gid,companytype_name,stakeholder_type,stakeholdertype_gid,assessmentagency_gid," +
                            " assessmentagency_name,assessmentagencyrating_gid,assessmentagencyrating_name,ratingas_on,amlcategory_gid,amlcategory_name," +
                            " businesscategory_gid,businesscategory_name,contactperson_firstname,contactperson_middlename,contactperson_lastname," +
                            " contactperson_contactno,designation_gid,designation,lastyear_turnover,escrow,start_date,end_date,created_by," +
                            " created_date,updated_date,updated_by,urn,urn_status,observations,bureau_response,institution_status," +
                            " businessstart_date,bureauname_name,bureau_score,bureauname_gid,bureauscore_date,cicdocument_name,cicdocument_path," +
                            " mobile_no,email_address,economical_status,social_capital,trade_capital,official_mailid,economical_flag," +
                            " startupasofloansanction_date,occupation_gid,occupation,lineofactivity_gid,lineofactivity,bsrcode_gid,bsrcode," +
                            " pslcategory_gid,pslcategory,weakersection_gid,weakersection,pslpurpose_gid,pslpurpose,totalsanction_financialinstitution," +
                            " pslsanction_limit,natureofentity_gid,natureofentity,indulgeinmarketing_activity,plantandmachineryinvestment_gid," +
                            " plantandmachineryinvestment,turnover_gid,turnover,msmeclassification_gid,msmeclassification,loansanction_date," +
                            " entityincorporation_date,hq_metropolitancity,clientdtl_gid,client_dtl,psltagging_flag,credit_status,tan_number," +
                            " sundrydebt_adv,fixed_assets,profit,revenue,incometax_returnsstatus from agr_mst_tinstitution " +
                            " where application_gid='" + values.application_gid + "' " +
                            " and institution_gid='" + dt["institution_gid"].ToString() + "'";
                    mnResultinstitution = objdbconn.ExecuteNonQuerySQL(msSQL);

                    // Insert data in Institution Branch Table from CAD Table
                    msSQL = "select institution2branch_gid from agr_mst_tinstitution2branch where institution_gid='" + dt["institution_gid"].ToString() + "'";
                    dt_datatable2 = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable2.Rows.Count != 0)
                    {
                        foreach (DataRow dt2 in dt_datatable2.Rows)
                        {
                            msGetGidinstitutionbranch = objcmnfunctions.GetMasterGID("ITGS");
                            msSQL = " insert into ocs_mst_tinstitution2branch " +
                                    " (institution2branch_gid, institution_gid, " +
                                    " gst_registered, gst_state, gst_no, created_by, created_date, updated_date, updated_by, gststate_gid, status, " +
                                    " authentication_status, returnfilling_status, verification_status )" +
                                    " select @institution2branch_gid := '" + msGetGidinstitutionbranch + "', @institution_gid:= '" + msGetGidinstitution + "',  " +
                                    " gst_registered, gst_state, gst_no, created_by, created_date, updated_date, updated_by, gststate_gid, status, " +
                                    " authentication_status, returnfilling_status, verification_status " +
                                    " from ocs_mst_tinstitution2branch " +
                                    " where institution_gid='" + dt["institution_gid"].ToString() + "' " +
                                    " and institution2branch_gid='" + dt2["institution2branch_gid"].ToString() + "'";
                            mnResultinstitutionbranch = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    else
                    {
                        mnResultinstitutionbranch = 1;
                    }

                    // Insert data in Institution Mobile Table 
                    msSQL = "select institution2mobileno_gid from agr_mst_tinstitution2mobileno where institution_gid='" + dt["institution_gid"].ToString() + "'";
                    dt_datatable3 = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable3.Rows.Count != 0)
                    {
                        foreach (DataRow dt3 in dt_datatable3.Rows)
                        {
                            msGetGidinstitutionmobile = objcmnfunctions.GetMasterGID("IT2M");
                            msSQL = " insert into agr_mst_tinstitution2mobileno " +
                                    " (institution2mobileno_gid, institution_gid, mobile_no, primary_status, whatsapp_no, landline_no, created_by, created_date, updated_date, updated_by ) " +
                                    " select @institution2mobileno_gid := '" + msGetGidinstitutionmobile + "', @institution_gid:= '" + msGetGidinstitution + "',  " +
                                    "  mobile_no, primary_status, whatsapp_no, landline_no, created_by, created_date, updated_date, updated_by " +
                                    " from agr_mst_tinstitution2mobileno " +
                                    " where institution_gid='" + dt["institution_gid"].ToString() + "' " +
                                    " and institution2mobileno_gid='" + dt3["institution2mobileno_gid"].ToString() + "'";
                            mnResultinstitutionmobile = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    else
                    {
                        mnResultinstitutionmobile = 1;
                    }

                    // Insert data in Institution Email Table 
                    msSQL = "select institution2email_gid from agr_mst_tinstitution2email where institution_gid='" + dt["institution_gid"].ToString() + "'";
                    dt_datatable4 = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable4.Rows.Count != 0)
                    {
                        foreach (DataRow dt4 in dt_datatable4.Rows)
                        {
                            msGetGidinstitutionemail = objcmnfunctions.GetMasterGID("IT2E");
                            msSQL = " insert into agr_mst_tinstitution2email " +
                                    " (institution2email_gid, institution_gid, email_address, primary_status, created_by, created_date, updated_date, updated_by) " +
                                    " select @institution2email_gid := '" + msGetGidinstitutionemail + "', @institution_gid:= '" + msGetGidinstitution + "',  " +
                                    " email_address, primary_status, created_by, created_date, updated_date, updated_by " +
                                    " from agr_mst_tinstitution2email " +
                                    " where institution_gid='" + dt["institution_gid"].ToString() + "' " +
                                    " and institution2email_gid='" + dt4["institution2email_gid"].ToString() + "'";
                            mnResultinstitutionemail = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    else
                    {
                        mnResultinstitutionemail = 1;
                    }

                    // Insert data in Institution Address Table
                    msSQL = "select institution2address_gid from agr_mst_tinstitution2address where institution_gid='" + dt["institution_gid"].ToString() + "'";
                    dt_datatable5 = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable5.Rows.Count != 0)
                    {
                        foreach (DataRow dt5 in dt_datatable5.Rows)
                        {
                            msGetGidinstitutionaddress = objcmnfunctions.GetMasterGID("IT2A");
                            msSQL = " insert into agr_mst_tinstitution2address " +
                                    " (institution2address_gid, institution_gid, addresstype_gid, addresstype_name, primary_status, addressline1, addressline2, landmark, postal_code, city, " +
                                    " taluka, district, state, country, latitude, longitude, created_by, created_date, updated_date, updated_by) " +
                                    " select @institution2address_gid := '" + msGetGidinstitutionaddress + "', @institution_gid:= '" + msGetGidinstitution + "',  " +
                                    " addresstype_gid, addresstype_name, primary_status, addressline1, addressline2, landmark, postal_code, city, " +
                                    " taluka, district, state, country, latitude, longitude, created_by, created_date, updated_date, updated_by " +
                                    " from agr_mst_tinstitution2address " +
                                    " where institution_gid='" + dt["institution_gid"].ToString() + "' " +
                                    " and institution2address_gid='" + dt5["institution2address_gid"].ToString() + "'";
                            mnResultinstitutionaddress = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    else
                    {
                        mnResultinstitutionaddress = 1;
                    }
                    // Insert data in Institution License Detail Table
                    msSQL = "select institution2licensedtl_gid from agr_mst_tinstitution2licensedtl where institution_gid='" + dt["institution_gid"].ToString() + "'";
                    dt_datatable6 = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable6.Rows.Count != 0)
                    {
                        foreach (DataRow dt6 in dt_datatable6.Rows)
                        {
                            msGetGidinstitutionlicense = objcmnfunctions.GetMasterGID("IT2L");
                            msSQL = " insert into agr_mst_tinstitution2licensedtl " +
                                    " (institution2licensedtl_gid, institution_gid, licensetype_gid, licensetype_name, license_no, issue_date, expiry_date, created_by, created_date, updated_date, updated_by) " +
                                    " select @institution2licensedtl_gid := '" + msGetGidinstitutionlicense + "', @institution_gid:= '" + msGetGidinstitution + "',  " +
                                    " licensetype_gid, licensetype_name, license_no, issue_date, expiry_date, created_by, created_date, updated_date, updated_by" +
                                    " from agr_mst_tinstitution2licensedtl " +
                                    " where institution_gid='" + dt["institution_gid"].ToString() + "' " +
                                    " and institution2licensedtl_gid='" + dt6["institution2licensedtl_gid"].ToString() + "'";
                            mnResultinstitutionlicense = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    else
                    {
                        mnResultinstitutionlicense = 1;
                    }

                    // Insert data in Institution Document Table
                    msSQL = "select institution2documentupload_gid from agr_mst_tinstitution2documentupload where institution_gid='" + dt["institution_gid"].ToString() + "'";
                    dt_datatable7 = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable7.Rows.Count != 0)
                    {
                        foreach (DataRow dt7 in dt_datatable7.Rows)
                        {
                            msGetGidinstitutiondocument = objcmnfunctions.GetMasterGID("INDO");
                            msSQL = " insert into agr_mst_tinstitution2documentupload " +
                                    " ( institution2documentupload_gid, institution_gid, " +
                                    " document_name, document_path, created_by, created_date, updated_date, updated_by, companydocument_gid, document_title, " +
                                    " document_id, covenant_type, untagged_type, untagged_by, untagged_date, covenant_periods, covenantperiod_updatedby, covenantperiod_updateddate) " +
                                    " select @institution2documentupload_gid := '" + msGetGidinstitutiondocument + "', @institution_gid:= '" + msGetGidinstitution + "',  " +
                                    " document_name, document_path, created_by, created_date, updated_date, updated_by, companydocument_gid, document_title, " +
                                    " document_id, covenant_type, untagged_type, untagged_by, untagged_date, covenant_periods, covenantperiod_updatedby, covenantperiod_updateddate " +
                                    " from agr_mst_tinstitution2documentupload " +
                                    " where institution_gid='" + dt["institution_gid"].ToString() + "' " +
                                    " and institution2documentupload_gid='" + dt7["institution2documentupload_gid"].ToString() + "'";
                            mnResultinstitutiondocument = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    else
                    {
                        mnResultinstitutiondocument = 1;
                    }


                    // Insert data in Institution Rating Details Table
                    msSQL = "select institution2ratingdetail_gid from agr_mst_tinstitution2ratingdetail where institution_gid='" + dt["institution_gid"].ToString() + "'";
                    dt_datatable8 = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable8.Rows.Count != 0)
                    {
                        foreach (DataRow dt8 in dt_datatable8.Rows)
                        {
                            msGetGidinstitutionratingdtl = objcmnfunctions.GetMasterGID("INRD");
                            msSQL = " insert into agr_mst_tinstitution2ratingdetail " +
                                    " ( institution2ratingdetail_gid, institution_gid,application_gid, " +
                                    " creditrating_agencygid, creditrating_agencyname, creditrating_gid, creditrating_name, assessed_on, creditrating_link, created_by, created_date, updated_by, updated_date ) " +
                                    " select @institution2ratingdetail_gid := '" + msGetGidinstitutionratingdtl + "', @institution_gid:= '" + msGetGidinstitution + "', @application_gid:= '" + msGetGidApplication + "', " +
                                    "  creditrating_agencygid, creditrating_agencyname, creditrating_gid, creditrating_name, assessed_on, creditrating_link, created_by, created_date, updated_by, updated_date" +
                                    " from agr_mst_tinstitution2ratingdetail " +
                                    " where institution_gid='" + dt["institution_gid"].ToString() + "' " +
                                    " and institution2ratingdetail_gid='" + dt8["institution2ratingdetail_gid"].ToString() + "'";
                            mnResultinstitutionratingdtl = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    else
                    {
                        mnResultinstitutionratingdtl = 1;
                    }

                    // Insert data in Institution Bank Details Table 
                    msSQL = "select institution2bankdtl_gid from agr_mst_tinstitution2bankdtl where institution_gid='" + dt["institution_gid"].ToString() + "'";
                    dt_datatable9 = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable9.Rows.Count != 0)
                    {
                        foreach (DataRow dt9 in dt_datatable9.Rows)
                        {
                            msGetGidinstitutionbankdtl = objcmnfunctions.GetMasterGID("I2BD");
                            msSQL = " insert into agr_mst_tinstitution2bankdtl " +
                                    " (institution2bankdtl_gid, institution_gid, application_gid, " +
                                    " bankaccount_number, ifsc_code, bank_name, branch_name, micr_code, accountholder_name, accounttype_gid, accounttype_name, " +
                                    " joint_account, jointaccountholder_name, chequebookfacility_available, accountopen_date, created_by, created_date, " +
                                    " updated_by, updated_date, bank_address, bankaccount_name, bankaccounttype_gid, bankaccounttype_name, confirmbankaccountnumber, " +
                                    " joinaccount_status, joinaccount_name, chequebook_status, disbursement_accountstatus, primary_status ) " +
                                    " select @institution2bankdtl_gid := '" + msGetGidinstitutionbankdtl + "', @institution_gid:= '" + msGetGidinstitution + "', @application_gid:= '" + msGetGidApplication + "', " +
                                    " bankaccount_number, ifsc_code, bank_name, branch_name, micr_code, accountholder_name, accounttype_gid, accounttype_name, " +
                                    " joint_account, jointaccountholder_name, chequebookfacility_available, accountopen_date, created_by, created_date, " +
                                    " updated_by, updated_date, bank_address, bankaccount_name, bankaccounttype_gid, bankaccounttype_name, confirmbankaccountnumber, " +
                                    " joinaccount_status, joinaccount_name, chequebook_status, disbursement_accountstatus, primary_status " +
                                    " from agr_mst_tinstitution2bankdtl " +
                                    " where institution_gid='" + dt["institution_gid"].ToString() + "' " +
                                    " and institution2bankdtl_gid='" + dt9["institution2bankdtl_gid"].ToString() + "'";
                            mnResultinstitutionbankdtl = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    else
                    {
                        mnResultinstitutionbankdtl = 1;
                    }

                }
            }
            else
            {
                mnResultinstitution = 1;
                mnResultinstitutionbranch = 1;
                mnResultinstitutionmobile = 1;
                mnResultinstitutionemail = 1;
                mnResultinstitutionaddress = 1;
                mnResultinstitutionlicense = 1;
                mnResultinstitutiondocument = 1;
                mnResultinstitutionratingdtl = 1;
                mnResultinstitutionbankdtl = 1;
            }
            // Insert data in Contact Table
            msSQL = "select contact_gid from agr_mst_tcontact where application_gid='" + values.application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msGetGidcontact = objcmnfunctions.GetMasterGID("CTCT");
                    msSQL = " insert into agr_mst_tcontact " +
                            " (contact_gid,application_gid,application_no,pan_status,pan_no,aadhar_no, " +
                            " first_name,last_name,middle_name,mother_lastname,individual_dob,age,gender_gid,gender_name," +
                            "designation_gid,designation_name,educationalqualification_gid,educationalqualification_name,main_occupation," +
                            "annual_income,monthly_income,pep_status,pepverified_date,stakeholdertype_gid,stakeholder_type,user_type," +
                            "maritalstatus_gid,maritalstatus_name,father_firstname,father_middlename,father_lastname,father_dob,father_age," +
                            "mother_firstname,mother_middlename,mother_dob,mother_age,spouse_firstname,spouse_middlename,spouse_lastname," +
                            "spouse_dob,spouse_age,ownershiptype_gid,ownershiptype_name,propertyholder_gid,propertyholder_name," +
                            "residencetype_gid,residencetype_name,currentresidence_years,branch_distance,bureauname_gid,bureauname_name," +
                            "bureau_score,bureauscore_date,observations,bureau_response,cicdocument_name,cicdocument_path,created_by," +
                            "created_date,updated_date,updated_by,contact_status,email_address,mobile_no,incometype_gid,incometype_name," +
                            "group_gid,group_name,profile,urn_status,urn,fathernominee_status,mothernominee_status,spousenominee_status," +
                            "othernominee_status,relationshiptype,nomineefirst_name,nominee_middlename,nominee_lastname,nominee_dob," +
                            "nominee_age,totallandinacres,cultivatedland,previouscrop,prposedcrop,economical_status,social_capital," +
                            "trade_capital,institution_gid,institution_name,startupasofloansanction_date,occupation_gid,occupation," +
                            "lineofactivity_gid,lineofactivity,bsrcode_gid,bsrcode,pslcategory_gid,pslcategory,weakersection_gid,weakersection," +
                            "pslpurpose_gid,pslpurpose,totalsanction_financialinstitution,pslsanction_limit,natureofentity_gid,natureofentity," +
                            "indulgeinmarketing_activity,plantandmachineryinvestment_gid,plantandmachineryinvestment,turnover_gid,turnover," +
                            "msmeclassification_gid,msmeclassification,loansanction_date,entityincorporation_date,hq_metropolitancity,clientdtl_gid," +
                            "client_dtl,economical_flag,psltagging_flag,credit_status)" +
                            " select @contact_gid := '" + msGetGidcontact + "', @application_gid:= '" + msGetGidApplication + "',  " +
                            " application_no,pan_status,pan_no,aadhar_no, " +
                            " first_name,last_name,middle_name,mother_lastname,individual_dob,age,gender_gid,gender_name," +
                            "designation_gid,designation_name,educationalqualification_gid,educationalqualification_name,main_occupation," +
                            "annual_income,monthly_income,pep_status,pepverified_date,stakeholdertype_gid,stakeholder_type,user_type," +
                            "maritalstatus_gid,maritalstatus_name,father_firstname,father_middlename,father_lastname,father_dob,father_age," +
                            "mother_firstname,mother_middlename,mother_dob,mother_age,spouse_firstname,spouse_middlename,spouse_lastname," +
                            "spouse_dob,spouse_age,ownershiptype_gid,ownershiptype_name,propertyholder_gid,propertyholder_name," +
                            "residencetype_gid,residencetype_name,currentresidence_years,branch_distance,bureauname_gid,bureauname_name," +
                            "bureau_score,bureauscore_date,observations,bureau_response,cicdocument_name,cicdocument_path,created_by," +
                            "created_date,updated_date,updated_by,contact_status,email_address,mobile_no,incometype_gid,incometype_name," +
                            "group_gid,group_name,profile,urn_status,urn,fathernominee_status,mothernominee_status,spousenominee_status," +
                            "othernominee_status,relationshiptype,nomineefirst_name,nominee_middlename,nominee_lastname,nominee_dob," +
                            "nominee_age,totallandinacres,cultivatedland,previouscrop,prposedcrop,economical_status,social_capital," +
                            "trade_capital,institution_gid,institution_name,startupasofloansanction_date,occupation_gid,occupation," +
                            "lineofactivity_gid,lineofactivity,bsrcode_gid,bsrcode,pslcategory_gid,pslcategory,weakersection_gid,weakersection," +
                            "pslpurpose_gid,pslpurpose,totalsanction_financialinstitution,pslsanction_limit,natureofentity_gid,natureofentity," +
                            "indulgeinmarketing_activity,plantandmachineryinvestment_gid,plantandmachineryinvestment,turnover_gid,turnover," +
                            "msmeclassification_gid,msmeclassification,loansanction_date,entityincorporation_date,hq_metropolitancity,clientdtl_gid," +
                            "client_dtl,economical_flag,psltagging_flag,credit_status " +
                            " from agr_mst_tcontact " +
                            " where application_gid='" + values.application_gid + "' " +
                            " and contact_gid='" + dt["contact_gid"].ToString() + "'";
                    mnResultcontact = objdbconn.ExecuteNonQuerySQL(msSQL);

                    // Insert data in Contact Pan Absence Reason Table
                    msSQL = "select contact2panabsencereason_gid from agr_mst_tcontact2panabsencereason where contact_gid='" + dt["contact_gid"].ToString() + "'";
                    dt_datatable2 = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable2.Rows.Count != 0)
                    {
                        foreach (DataRow dt2 in dt_datatable2.Rows)
                        {
                            msGetGidcontactpanreason = objcmnfunctions.GetMasterGID("C2PR");
                            msSQL = " insert into agr_mst_tcontact2panabsencereason " +
                                    " (contact2panabsencereason_gid, contact_gid, panabsencereason, created_by, created_date, updated_date, updated_by) " +
                                    " select @contact2panabsencereason_gid := '" + msGetGidcontactpanreason + "', @contact_gid:= '" + msGetGidcontact + "',  " +
                                    " panabsencereason, created_by, created_date, updated_date, updated_by " +
                                    " from agr_mst_tcontact2panabsencereason " +
                                    " where contact_gid='" + dt["contact_gid"].ToString() + "' " +
                                    " and contact2panabsencereason_gid='" + dt2["contact2panabsencereason_gid"].ToString() + "'";
                            mnResultcontactpanabsencereason = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    else
                    {
                        mnResultcontactpanabsencereason = 1;
                    }

                    // Insert data in Contact Pan Form-60 Document Table 
                    msSQL = "select contact2panform60_gid from agr_mst_tcontact2panform60 where contact_gid='" + dt["contact_gid"].ToString() + "'";
                    dt_datatable3 = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable3.Rows.Count != 0)
                    {
                        foreach (DataRow dt3 in dt_datatable3.Rows)
                        {
                            msGetGidcontactpanform60 = objcmnfunctions.GetMasterGID("CF60");
                            msSQL = " insert into agr_mst_tcontact2panform60 " +
                                    " (contact2panform60_gid, contact_gid, document_name, document_path, created_by, created_date, updated_date, updated_by) " +
                                    " select @contact2panform60_gid := '" + msGetGidcontactpanform60 + "', @contact_gid:= '" + msGetGidcontact + "',  " +
                                    " document_name, document_path, created_by, created_date, updated_date, updated_by " +
                                    " from agr_mst_tcontact2panform60 " +
                                    " where contact_gid='" + dt["contact_gid"].ToString() + "' " +
                                    " and contact2panform60_gid='" + dt3["contact2panform60_gid"].ToString() + "'";
                            mnResultcontactpanform60 = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    else
                    {
                        mnResultcontactpanform60 = 1;
                    }

                    // Insert data in Contact Mobile Number Table
                    msSQL = "select contact2mobileno_gid from agr_mst_tcontact2mobileno where contact_gid='" + dt["contact_gid"].ToString() + "'";
                    dt_datatable4 = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable4.Rows.Count != 0)
                    {
                        foreach (DataRow dt4 in dt_datatable4.Rows)
                        {
                            msGetGidcontactmobile = objcmnfunctions.GetMasterGID("C2MN");
                            msSQL = " insert into agr_mst_tcontact2mobileno " +
                                    " (contact2mobileno_gid, contact_gid, mobile_no, primary_status, whatsapp_no, landline_no, " +
                                    " created_by, created_date, updated_date, updated_by ) " +
                                    " select @contact2mobileno_gid := '" + msGetGidcontactmobile + "', @contact_gid:= '" + msGetGidcontact + "',  " +
                                    " mobile_no, primary_status, whatsapp_no, landline_no, " +
                                    " created_by, created_date, updated_date, updated_by " +
                                    " from agr_mst_tcontact2mobileno " +
                                    " where contact_gid='" + dt["contact_gid"].ToString() + "' " +
                                    " and contact2mobileno_gid='" + dt4["contact2mobileno_gid"].ToString() + "'";
                            mnResultcontactmobile = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    else
                    {
                        mnResultcontactmobile = 1;
                    }

                    // Insert data in Contact Email Table
                    msSQL = "select contact2email_gid from agr_mst_tcontact2email where contact_gid='" + dt["contact_gid"].ToString() + "'";
                    dt_datatable5 = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable5.Rows.Count != 0)
                    {
                        foreach (DataRow dt5 in dt_datatable5.Rows)
                        {
                            msGetGidcontactemail = objcmnfunctions.GetMasterGID("C2EA");
                            msSQL = " insert into agr_mst_tcontact2email " +
                                    " (contact2email_gid, contact_gid, email_address, primary_status, created_by, created_date, updated_date, updated_by ) " +
                                    " select @contact2email_gid := '" + msGetGidcontactemail + "', @contact_gid:= '" + msGetGidcontact + "',  " +
                                    " email_address, primary_status, created_by, created_date, updated_date, updated_by " +
                                    " from agr_mst_tcontact2email " +
                                    " where contact_gid='" + dt["contact_gid"].ToString() + "' " +
                                    " and contact2email_gid='" + dt5["contact2email_gid"].ToString() + "'";
                            mnResultcontactemail = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    else
                    {
                        mnResultcontactemail = 1;
                    }
                    // Insert data in Contact ID Proof Table 
                    msSQL = "select contact2idproof_gid from agr_mst_tcontact2idproof where contact_gid='" + dt["contact_gid"].ToString() + "'";
                    dt_datatable6 = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable6.Rows.Count != 0)
                    {
                        foreach (DataRow dt6 in dt_datatable6.Rows)
                        {
                            msGetGidcontactidproof = objcmnfunctions.GetMasterGID("C2IP");
                            msSQL = " insert into agr_mst_tcontact2idproof " +
                                    " (contact2idproof_gid, contact_gid, idproof_gid, idproof_name, idproof_no, idproof_dob, file_no, document_name, " +
                                    " document_path, created_by, created_date, updated_date, updated_by ) " +
                                    " select @contact2idproof_gid := '" + msGetGidcontactidproof + "', @contact_gid:= '" + msGetGidcontact + "',  " +
                                    " idproof_gid, idproof_name, idproof_no, idproof_dob, file_no, document_name, " +
                                    " document_path, created_by, created_date, updated_date, updated_by " +
                                    " from agr_mst_tcontact2idproof " +
                                    " where contact_gid='" + dt["contact_gid"].ToString() + "' " +
                                    " and contact2idproof_gid='" + dt6["contact2idproof_gid"].ToString() + "'";
                            mnResultcontactidproof = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    else
                    {
                        mnResultcontactidproof = 1;
                    }

                    // Insert data in Contact Address Table
                    msSQL = "select contact2address_gid from agr_mst_tcontact2address where contact_gid='" + dt["contact_gid"].ToString() + "'";
                    dt_datatable7 = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable7.Rows.Count != 0)
                    {
                        foreach (DataRow dt7 in dt_datatable7.Rows)
                        {
                            msGetGidcontactaddress = objcmnfunctions.GetMasterGID("C2AD");
                            msSQL = " insert into agr_mst_tcontact2address " +
                                    " (contact2address_gid, contact_gid, addresstype_gid, addresstype_name, primary_status, addressline1, " +
                                    " addressline2, landmark, postal_code, city, taluka, district, state, country, latitude, longitude, " +
                                    " created_by, created_date, updated_date, updated_by ) " +
                                    " select @contact2address_gid := '" + msGetGidcontactaddress + "', @contact_gid:= '" + msGetGidcontact + "',  " +
                                    " addresstype_gid, addresstype_name, primary_status, addressline1, " +
                                    " addressline2, landmark, postal_code, city, taluka, district, state, country, latitude, longitude, " +
                                    " created_by, created_date, updated_date, updated_by" +
                                    " from agr_mst_tcontact2address " +
                                    " where contact_gid='" + dt["contact_gid"].ToString() + "' " +
                                    " and contact2address_gid='" + dt7["contact2address_gid"].ToString() + "'";
                            mnResultcontactaddress = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    else
                    {
                        mnResultcontactaddress = 1;
                    }

                    // Insert data in Contact Document Table
                    msSQL = "select contact2document_gid from agr_mst_tcontact2document where contact_gid='" + dt["contact_gid"].ToString() + "'";
                    dt_datatable8 = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable8.Rows.Count != 0)
                    {
                        foreach (DataRow dt8 in dt_datatable8.Rows)
                        {
                            msGetGidcontactdocument = objcmnfunctions.GetMasterGID("C2DO");
                            msSQL = " insert into agr_mst_tcontact2document " +
                                    " (contact2document_gid, contact_gid, individualdocument_gid, document_gid, document_title, document_name, document_path, " +
                                    " created_by, created_date, updated_date, updated_by, covenant_type, untagged_type, untagged_by, untagged_date, " +
                                    " covenant_periods, covenantperiod_updatedby, covenantperiod_updateddate, documenttype_gid, documenttype_name) " +
                                    " select @contact2document_gid := '" + msGetGidcontactdocument + "', @contact_gid:= '" + msGetGidcontact + "',  " +
                                    " individualdocument_gid, document_gid, document_title, document_name, document_path, " +
                                    " created_by, created_date, updated_date, updated_by, covenant_type, untagged_type, untagged_by, untagged_date, " +
                                    " covenant_periods, covenantperiod_updatedby, covenantperiod_updateddate, documenttype_gid, documenttype_name " +
                                    " from agr_mst_tcontact2document " +
                                    " where contact_gid='" + dt["contact_gid"].ToString() + "' " +
                                    " and contact2document_gid='" + dt8["contact2document_gid"].ToString() + "'";
                            mnResultcontactdocument = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    else
                    {
                        mnResultcontactdocument = 1;
                    }
                }
            }

            else
            {
                mnResultcontact = 1;
                mnResultcontactpanabsencereason = 1;
                mnResultcontactpanform60 = 1;
                mnResultcontactmobile = 1;
                mnResultcontactemail = 1;
                mnResultcontactidproof = 1;
                mnResultcontactaddress = 1;
                mnResultcontactdocument = 1;
            }

            msSQL = " select contact_gid, individualdocument_gid, contact2document_gid from agr_mst_tcontact2document where contact_gid in " +
                    " (select contact_gid from agr_mst_tcontact where application_gid='" + msGetGidApplication + "')";
            dt_datatable21 = objdbconn.GetDataTable(msSQL);
            foreach (DataRow dt in dt_datatable21.Rows)
            {
                string lscovenant_type = "", lsdocumenttype_gid = "", lsdocumenttype_name = "", lscompanydocument_name = "";

                string msGetdefDocchecklistGID = objcmnfunctions.GetMasterGID("DOCG");
                msSQL = " select individualdocument_gid,documenttypes_gid,documenttype_name,individualdocument_name,covenant_type " +
                        " from ocs_mst_tindividualdocument where individualdocument_gid='" + dt["individualdocument_gid"].ToString() + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lsdocumenttype_gid = objODBCDatareader["documenttypes_gid"].ToString();
                    lsdocumenttype_name = objODBCDatareader["documenttype_name"].ToString();
                    lscompanydocument_name = objODBCDatareader["individualdocument_name"].ToString();
                    lscovenant_type = objODBCDatareader["covenant_type"].ToString();
                }
                objODBCDatareader.Close();
                msSQL = " insert into agr_trn_tdocumentchecktls(" +
                        " documentcheckdtl_gid," +
                        " application_gid," +
                        " credit_gid, " +
                        " individualdocument_gid, " +
                         " documentuploaded_gid, " +
                        " documenttype_gid," +
                        " documenttype_code," +
                        " documenttype_name," +
                        " covenant_type, " +
                        " tagged_by, " +
                        " created_date," +
                        " created_by)" +
                        " VALUES(" +
                        "'" + msGetdefDocchecklistGID + "'," +
                        "'" + msGetGidApplication + "'," +
                        "'" + dt["contact_gid"].ToString() + "'," +
                        "'" + dt["individualdocument_gid"].ToString() + "'," +
                        "'" + dt["contact2document_gid"].ToString() + "'," +
                        "'" + lsdocumenttype_gid + "'," +
                        "'" + lsdocumenttype_name + "'," +
                        "'" + lscompanydocument_name.Replace("'", "") + "'," +
                        "'" + lscovenant_type + "'," +
                        "'N'," +
                        "current_timestamp," +
                        "'" + employee_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (lscovenant_type == "Y")
                {
                    string msGetDocchecklistGID = objcmnfunctions.GetMasterGID("CDCL");
                    msSQL = " insert into agr_trn_tcovanantdocumentcheckdtls(" +
                           " covenantdocumentcheckdtl_gid," +
                           " application_gid," +
                           " credit_gid," +
                           " individualdocument_gid," +
                           " documentuploaded_gid, " +
                           " documenttype_gid," +
                           " documenttype_code," +
                           " documenttype_name," +
                           " covenant_type, " +
                           " tagged_by, " +
                           " created_date," +
                           " created_by)" +
                           " VALUES(" +
                           "'" + msGetDocchecklistGID + "'," +
                           "'" + msGetGidApplication + "'," +
                           "'" + dt["contact_gid"].ToString() + "'," +
                           "'" + dt["individualdocument_gid"].ToString() + "'," +
                           "'" + dt["contact2document_gid"].ToString() + "'," +
                           "'" + lsdocumenttype_gid + "'," +
                           "'" + lsdocumenttype_name + "'," +
                           "'" + lscompanydocument_name.Replace("'", "") + "'," +
                           "'" + lscovenant_type + "'," +
                           "'N'," +
                           "current_timestamp," +
                           "'" + employee_gid + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            dt_datatable21.Dispose();

            msSQL = " select contact_gid from agr_mst_tcontact2document  where contact_gid in " +
                    " (select contact_gid from agr_mst_tcontact where application_gid='" + msGetGidApplication + "') group by contact_gid";
            dt_datatable21 = objdbconn.GetDataTable(msSQL);
            foreach (DataRow dt in dt_datatable21.Rows)
            {
                DaAgrMstScannedDocument objvalues1 = new DaAgrMstScannedDocument();
                string lscredit_gid = dt["contact_gid"].ToString();
                objvalues1.DaGroupDocChecklistinfo(values.application_gid, lscredit_gid, employee_gid);
            }
            dt_datatable21.Dispose();


            msSQL = " select institution_gid,companydocument_gid, institution2documentupload_gid  from agr_mst_tinstitution2documentupload  where institution_gid in " +
                   " (select institution_gid from agr_mst_tinstitution where application_gid='" + msGetGidApplication + "')";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            foreach (DataRow dt in dt_datatable.Rows)
            {
                string lscovenant_type = "", lsdocumenttype_gid = "", lsdocumenttype_name = "", lscompanydocument_name = "";

                string msGetdefDocchecklistGID = objcmnfunctions.GetMasterGID("DOCG");
                msSQL = " select companydocument_gid,documenttypes_gid,documenttype_name,companydocument_name,covenant_type " +
                            " from ocs_mst_tcompanydocument where companydocument_gid='" + dt["companydocument_gid"].ToString() + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lsdocumenttype_gid = objODBCDatareader["documenttypes_gid"].ToString();
                    lsdocumenttype_name = objODBCDatareader["documenttype_name"].ToString();
                    lscompanydocument_name = objODBCDatareader["companydocument_name"].ToString();
                    lscovenant_type = objODBCDatareader["covenant_type"].ToString();
                }
                objODBCDatareader.Close();
                msSQL = " insert into agr_trn_tdocumentchecktls(" +
                        " documentcheckdtl_gid," +
                        " application_gid," +
                        " credit_gid, " +
                        " companydocument_gid, " +
                        " documentuploaded_gid, " +
                        " documenttype_gid," +
                        " documenttype_code," +
                        " documenttype_name," +
                        " covenant_type, " +
                        " tagged_by, " +
                        " created_date," +
                        " created_by)" +
                        " VALUES(" +
                        "'" + msGetdefDocchecklistGID + "'," +
                        "'" + msGetGidApplication + "'," +
                        "'" + dt["institution_gid"].ToString() + "'," +
                        "'" + dt["companydocument_gid"].ToString() + "'," +
                        "'" + dt["institution2documentupload_gid"].ToString() + "'," +
                        "'" + lsdocumenttype_gid + "'," +
                        "'" + lsdocumenttype_name + "'," +
                        "'" + lscompanydocument_name.Replace("'", "") + "'," +
                        "'" + lscovenant_type + "'," +
                        "'N'," +
                        "current_timestamp," +
                        "'" + employee_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (lscovenant_type == "Y")
                {
                    string msGetDocchecklistGID = objcmnfunctions.GetMasterGID("CDCL");
                    msSQL = " insert into agr_trn_tcovanantdocumentcheckdtls(" +
                           " covenantdocumentcheckdtl_gid," +
                           " application_gid," +
                           " credit_gid," +
                           " companydocument_gid," +
                           " documentuploaded_gid, " +
                           " documenttype_gid," +
                           " documenttype_code," +
                           " documenttype_name," +
                           " covenant_type, " +
                           " tagged_by, " +
                           " created_date," +
                           " created_by)" +
                           " VALUES(" +
                           "'" + msGetDocchecklistGID + "'," +
                           "'" + msGetGidApplication + "'," +
                           "'" + dt["institution_gid"].ToString() + "'," +
                           "'" + dt["companydocument_gid"].ToString() + "'," +
                           "'" + dt["institution2documentupload_gid"].ToString() + "'," +
                           "'" + lsdocumenttype_gid + "'," +
                           "'" + lsdocumenttype_name + "'," +
                           "'" + lscompanydocument_name.Replace("'", "") + "'," +
                           "'" + lscovenant_type + "'," +
                           "'N'," +
                           "current_timestamp," +
                           "'" + employee_gid + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            dt_datatable.Dispose();

            msSQL = " select institution_gid from agr_mst_tinstitution2documentupload  where institution_gid in " +
                    " (select institution_gid from agr_mst_tinstitution where application_gid='" + msGetGidApplication + "') group by institution_gid";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            foreach (DataRow dt in dt_datatable.Rows)
            {
                DaAgrMstScannedDocument objvalues = new DaAgrMstScannedDocument();
                string lscredit_gid = dt["institution_gid"].ToString();
                objvalues.DaGroupDocChecklistinfo(msGetGidApplication, lscredit_gid, employee_gid);
            }
            dt_datatable.Dispose();
            string lspage = "";
            DaAgrMstApplicationAdd objMstApplicationAdd = new DaAgrMstApplicationAdd();
            objMstApplicationAdd.FnProgramBasedDcoument(msGetGidApplication, employee_gid, user_gid, values.onboarding_status, lspage);

            // Insert data in Application2Loan(Product Details) Table Details
            msSQL = "select application2loan_gid from agr_mst_tapplication2loan where application_gid='" + values.application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msGetGidapplication2loan = objcmnfunctions.GetMasterGID("AP2L");
                    msSQL = " insert into agr_mst_tapplication2loan " +
                            " (application2loan_gid, application_gid, facilityrequested_date, product_type, producttype_gid, productsub_type, productsubtype_gid, loantype_gid, loan_type, " +
                            " loanfacility_amount, rate_interest, penal_interest, tenureproduct_year, tenureproduct_month, tenureproduct_days, tenureoverall_limit, " +
                            " facility_type, facility_mode, created_by, created_date, scheme_type, principalfrequency_name, principalfrequency_gid, " +
                            " interestfrequency_name, interestfrequency_gid, program_gid, program, primaryvaluechain_gid, primaryvaluechain_name, " +
                            " secondaryvaluechain_gid, secondaryvaluechain_name, interest_status, moratorium_status, moratorium_type, " +
                            " moratorium_startdate, moratorium_enddate, source_type, guideline_value, guideline_date, marketvalue_date, market_value, " +
                            " forcedsource_value, collateralSSV_value, forcedvalueassessed_on, collateralobservation_summary, enduse_purpose, " +
                            " updated_by, updated_date, interchangeability, expiry_date, report_structure, document_limit, product_gid, product_name, " +
                            " sector_name, category_name, variety_gid, variety_name, botanical_name, alternative_name, limit_product, trade_orginatedby, " +
                            " SA_Brokerage, programlimit_validdfrom, programlimit_validdto, programoverall_limit, holding_periods, holdingmonthly_procurement, " +
                            " extendedholding_periods, extendedmonthly_procurement, charges_extendedperiod, customer_advance, reimburesementof_expenses, " +
                            " reimburesementof_expensespenalty, bankfundingdata_filename, bankfundingdata_filepath, needfor_stocking, product_portfolio, " +
                            " production_capacity, natureof_operations, averagemonthly_inventoryholding, financialinstitutions_relationship, " +
                            " utilized_limit, available_limit)" +
                            " select @application2loan_gid := '" + msGetGidapplication2loan + "', @application_gid:= '" + msGetGidApplication + "', " +
                            " facilityrequested_date, product_type, producttype_gid, productsub_type, productsubtype_gid, loantype_gid, loan_type, " +
                            " loanfacility_amount, rate_interest, penal_interest, tenureproduct_year, tenureproduct_month, tenureproduct_days, tenureoverall_limit, " +
                            " facility_type, facility_mode, created_by, created_date, scheme_type, principalfrequency_name, principalfrequency_gid, " +
                            " interestfrequency_name, interestfrequency_gid, program_gid, program, primaryvaluechain_gid, primaryvaluechain_name, " +
                            " secondaryvaluechain_gid, secondaryvaluechain_name, interest_status, moratorium_status, moratorium_type, " +
                            " moratorium_startdate, moratorium_enddate, source_type, guideline_value, guideline_date, marketvalue_date, market_value, " +
                            " forcedsource_value, collateralSSV_value, forcedvalueassessed_on, collateralobservation_summary, enduse_purpose, " +
                            " updated_by, updated_date, interchangeability, expiry_date, report_structure, document_limit, product_gid, product_name, " +
                            " sector_name, category_name, variety_gid, variety_name, botanical_name, alternative_name, limit_product, trade_orginatedby, " +
                            " SA_Brokerage, programlimit_validdfrom, programlimit_validdto, programoverall_limit, holding_periods, holdingmonthly_procurement, " +
                            " extendedholding_periods, extendedmonthly_procurement, charges_extendedperiod, customer_advance, reimburesementof_expenses, " +
                            " reimburesementof_expensespenalty, bankfundingdata_filename, bankfundingdata_filepath, needfor_stocking, product_portfolio, " +
                            " production_capacity, natureof_operations, averagemonthly_inventoryholding, financialinstitutions_relationship, " +
                            " utilized_limit, available_limit " +
                            " from agr_mst_tapplication2loan " +
                            " where application_gid='" + values.application_gid + "' " +
                            " and application2loan_gid='" + dt["application2loan_gid"].ToString() + "'";
                    mnResultapplication2loan = objdbconn.ExecuteNonQuerySQL(msSQL);

                    // Insert data in Payment Type Customer Table
                    msSQL = "select paymenttypecustomer_gid from agr_mst_tapploan2paymenttypecustomer where application2loan_gid='" + dt["application2loan_gid"].ToString() + "'";
                    dt_datatable2 = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable2.Rows.Count != 0)
                    {
                        foreach (DataRow dt2 in dt_datatable2.Rows)
                        {
                            msGetGidpaymenttypecustomer = objcmnfunctions.GetMasterGID("PTC");
                            msSQL = " insert into agr_mst_tapploan2paymenttypecustomer " +
                                    " (paymenttypecustomer_gid, application_gid, application2loan_gid, customerpaymenttype_gid, customerpaymenttype_name, " +
                                    " maximumpercent_paymentterm, created_by, created_date) " +
                                    " select @paymenttypecustomer_gid := '" + msGetGidpaymenttypecustomer + "', @application_gid:= '" + msGetGidApplication + "', " +
                                    " @application2loan_gid:= '" + msGetGidapplication2loan + "', " +
                                    " customerpaymenttype_gid, customerpaymenttype_name,maximumpercent_paymentterm, created_by, created_date " +
                                    " from agr_mst_tapploan2paymenttypecustomer " +
                                    " where application2loan_gid='" + dt["application2loan_gid"].ToString() + "' " +
                                    " and paymenttypecustomer_gid='" + dt2["paymenttypecustomer_gid"].ToString() + "'";
                            mnResultpaymenttypecustomer = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    else
                    {
                        mnResultpaymenttypecustomer = 1;
                    }

                    // Insert data in Application2Loan(Product Details) Table 
                    msSQL = "select application2product_gid from agr_mst_tapplication2product where application2loan_gid='" + dt["application2loan_gid"].ToString() + "'";
                    dt_datatable3 = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable3.Rows.Count != 0)
                    {
                        foreach (DataRow dt3 in dt_datatable3.Rows)
                        {
                            msGetGidapplication2product = objcmnfunctions.GetMasterGID("AP2P");
                            msSQL = " insert into agr_mst_tapplication2product " +
                                    " (application2product_gid, application_gid, application2loan_gid, product_gid, product_name, " +
                                    " variety_gid, variety_name, sector_name, category_name, botanical_name, alternative_name, created_by, " +
                                    " created_date, updated_by, updated_date, hsn_code, unitpricevalue_commodity, natureformstate_commoditygid, " +
                                    " natureformstate_commodity, qualityof_commodity, quantity, uom_gid, uom_name, milestone_applicability, " +
                                    " milestonepayment_gid, milestonepayment_name, sa_payout, insurance_availability, insurance_percent, insurance_cost, " +
                                    " net_yield, markto_marketvalue, pricereference_source, headingdesc_product, typeofsupply_naturegid, " +
                                    " typeofsupply_naturename, sectorclassification_gid, sectorclassification_name, creditperiod_years, creditperiod_months, " +
                                    " creditperiod_days, overallcreditperiod_limit, commodity_margin, commoditynet_yield, graceperiod_days, " +
                                    " customerpaymenttype_gid, customerpaymenttype_name, maximumpercent_paymentterm) " +
                                    " select @application2product_gid := '" + msGetGidapplication2product + "',@application_gid:= '" + msGetGidApplication + "', " +
                                    " @application2loan_gid:= '" + msGetGidapplication2loan + "', " +
                                    " product_gid, product_name, " +
                                    " variety_gid, variety_name, sector_name, category_name, botanical_name, alternative_name, created_by, " +
                                    " created_date, updated_by, updated_date, hsn_code, unitpricevalue_commodity, natureformstate_commoditygid, " +
                                    " natureformstate_commodity, qualityof_commodity, quantity, uom_gid, uom_name, milestone_applicability, " +
                                    " milestonepayment_gid, milestonepayment_name, sa_payout, insurance_availability, insurance_percent, insurance_cost, " +
                                    " net_yield, markto_marketvalue, pricereference_source, headingdesc_product, typeofsupply_naturegid, " +
                                    " typeofsupply_naturename, sectorclassification_gid, sectorclassification_name, creditperiod_years, creditperiod_months, " +
                                    " creditperiod_days, overallcreditperiod_limit, commodity_margin, commoditynet_yield, graceperiod_days, " +
                                    "  @customerpaymenttype_gid:= '"+ msGetGidpaymenttypecustomer +"', customerpaymenttype_name, maximumpercent_paymentterm " +
                                    " from agr_mst_tapplication2product " +
                                    " where application2loan_gid='" + dt["application2loan_gid"].ToString() + "' " +
                                    " and application2product_gid='" + dt3["application2product_gid"].ToString() + "'";
                            mnResultapplication2product = objdbconn.ExecuteNonQuerySQL(msSQL);

                            // Insert data in product2commodity gst status Table
                            msSQL = "select appproduct2commoditygststatus_gid from agr_mst_tappproduct2commoditygststatus where application2product_gid='" + dt3["application2product_gid"].ToString() + "'";
                            dt_datatable4 = objdbconn.GetDataTable(msSQL);
                            if (dt_datatable4.Rows.Count != 0)
                            {
                                foreach (DataRow dt4 in dt_datatable4.Rows)
                                {
                                    msGetGidproduct2gststatus = objcmnfunctions.GetMasterGID("APGT");
                                    msSQL = " insert into agr_mst_tappproduct2commoditygststatus " +
                                            " (appproduct2commoditygststatus_gid, commoditygststatus_gid, application2product_gid, product_gid, " +
                                            " variety_gid, IGST_percent, SGST_percent, CGST_percent, CESS_percent, wef_date, created_by, " +
                                            " created_date) " +
                                            " select @appproduct2commoditygststatus_gid := '" + msGetGidproduct2gststatus + "',commoditygststatus_gid," +
                                            " @application2product_gid:= '" + msGetGidapplication2product + "', " +
                                            "  product_gid, " +
                                            " variety_gid, IGST_percent, SGST_percent, CGST_percent, CESS_percent, wef_date, created_by, created_date " +
                                            " from agr_mst_tappproduct2commoditygststatus " +
                                            " where application2product_gid= '" + dt3["application2product_gid"].ToString() + "'" +
                                            " and appproduct2commoditygststatus_gid='" + dt4["appproduct2commoditygststatus_gid"].ToString() + "'";
                                    mnResultproduct2gststatus = objdbconn.ExecuteNonQuerySQL(msSQL);
                                }
                            }
                            else
                            {
                                mnResultproduct2gststatus = 1;
                            }
                            // Insert data in product2commodity trade detail Table
                            msSQL = "select appproduct2commoditytrade_gid from agr_mst_tappproduct2commoditytradedtl where application2product_gid='" + dt3["application2product_gid"].ToString() + "'";
                            dt_datatable5 = objdbconn.GetDataTable(msSQL);
                            if (dt_datatable5.Rows.Count != 0)
                            {
                                foreach (DataRow dt5 in dt_datatable5.Rows)
                                {
                                    msGetGidproduct2commoditytrade = objcmnfunctions.GetMasterGID("ACTP");
                                    msSQL = " insert into agr_mst_tappproduct2commoditytradedtl " +
                                            " (appproduct2commoditytrade_gid, commoditytradeproductdtl_gid, application2product_gid, mstproduct_gid, " +
                                            " variety_gid, product_gid, product_name, subproduct_gid, subproduct_name, insurancecompany_gid, " +
                                            " insurancecompany_name, insurancepolicy_gid, insurancepolicy_name, created_by, created_date) " +
                                            " select @appproduct2commoditytrade_gid := '" + msGetGidproduct2commoditytrade + "',commoditytradeproductdtl_gid, @application2product_gid:= '" + msGetGidapplication2product + "', " +
                                            " mstproduct_gid,variety_gid, product_gid, product_name, subproduct_gid, subproduct_name, insurancecompany_gid, " +
                                            " insurancecompany_name, insurancepolicy_gid, insurancepolicy_name, created_by, created_date " +
                                            " from agr_mst_tappproduct2commoditytradedtl " +
                                            " where application2product_gid= '" + dt3["application2product_gid"].ToString() + "'" +
                                            " and appproduct2commoditytrade_gid='" + dt5["appproduct2commoditytrade_gid"].ToString() + "'";
                                    mnResultproduct2commoditytrade = objdbconn.ExecuteNonQuerySQL(msSQL);
                                }
                            }
                            else
                            {
                                mnResultproduct2commoditytrade = 1;
                            }
                            // Insert data in product2commodity document detail Table
                            msSQL = "select appproduct2commoditydocument_gid from agr_mst_tappproduct2commoditydocument where application2product_gid='" + dt3["application2product_gid"].ToString() + "'";
                            dt_datatable6 = objdbconn.GetDataTable(msSQL);
                            if (dt_datatable6.Rows.Count != 0)
                            {
                                foreach (DataRow dt6 in dt_datatable6.Rows)
                                {
                                    msGetGidproduct2commoditydoc = objcmnfunctions.GetMasterGID("APCD");
                                    msSQL = " insert into agr_mst_tappproduct2commoditydocument " +
                                            " (appproduct2commoditydocument_gid, commoditydocument_gid, application2product_gid, ason_date, commodityreport_filename, " +
                                            " commodityreport_filepath, riskanalysisreport_filename, riskanalysisreport_filepath, created_by, created_date, " +
                                            " variety_gid) " +
                                            " select @appproduct2commoditydocument_gid := '" + msGetGidproduct2commoditydoc + "',  commoditydocument_gid, @application2product_gid:= '" + msGetGidapplication2product + "', " +
                                            " ason_date, commodityreport_filename, " +
                                            " commodityreport_filepath, riskanalysisreport_filename, riskanalysisreport_filepath, created_by, created_date, " +
                                            " variety_gid " +
                                            " from agr_mst_tappproduct2commoditydocument " +
                                            " where application2product_gid= '" + dt3["application2product_gid"].ToString() + "'" +
                                            " and appproduct2commoditydocument_gid='" + dt6["appproduct2commoditydocument_gid"].ToString() + "'";
                                    mnResultproduct2commoditydoc = objdbconn.ExecuteNonQuerySQL(msSQL);
                                }
                            }
                            else
                            {
                                mnResultproduct2commoditydoc = 1;
                            }
                        }
                    }
                    else
                    {
                        mnResultapplication2product = 1;
                        mnResultproduct2gststatus = 1;
                        mnResultproduct2commoditytrade = 1;
                        mnResultproduct2commoditydoc = 1;
                    }
                    // Insert data in application loan2supplier details Table
                    msSQL = "select apploan2supplierdtl_gid from agr_mst_tapploan2supplierdtl where application2loan_gid='" + dt["application2loan_gid"].ToString() + "'";
                    dt_datatable7 = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable7.Rows.Count != 0)
                    {
                        foreach (DataRow dt7 in dt_datatable7.Rows)
                        {
                            msGetGidloan2supplierdtl = objcmnfunctions.GetMasterGID("ALSD");
                            msSQL = " insert into agr_mst_tapploan2supplierdtl " +
                                    " (apploan2supplierdtl_gid, application_gid, application2loan_gid, supplier_gid, supplier_name, " +
                                    " supplier_address, supplier_emailid, supplier_phoneno, supplier_gstno, supplier_pandtl, milestone_applicable, " +
                                    " milestonepaymenttype_gid, milestonepaymenttype_name, supplier_vintage, created_by, created_date, supplier_margin) " +
                                    " select @apploan2supplierdtl_gid := '" + msGetGidloan2supplierdtl + "', @application_gid:= '" + msGetGidApplication + "', " +
                                    " @application2loan_gid:= '" + msGetGidapplication2loan + "', " +
                                    " supplier_gid, supplier_name, supplier_address, supplier_emailid, supplier_phoneno, supplier_gstno, " +
                                    " supplier_pandtl, milestone_applicable, milestonepaymenttype_gid, milestonepaymenttype_name, supplier_vintage, " +
                                    " created_by, created_date, supplier_margin " +
                                    " from agr_mst_tapploan2supplierdtl " +
                                    " where application2loan_gid='" + dt["application2loan_gid"].ToString() + "' " +
                                    " and apploan2supplierdtl_gid='" + dt7["apploan2supplierdtl_gid"].ToString() + "'";
                            mnResultloan2supplierdtl = objdbconn.ExecuteNonQuerySQL(msSQL);

                            // Insert data in application2supplier gst details Table
                            msSQL = "select app2suppliergstdtl_gid from agr_mst_tapp2suppliergstdtl where apploan2supplierdtl_gid='" + dt7["apploan2supplierdtl_gid"].ToString() + "'";
                            dt_datatable8 = objdbconn.GetDataTable(msSQL);
                            if (dt_datatable8.Rows.Count != 0)
                            {
                                foreach (DataRow dt8 in dt_datatable8.Rows)
                                {
                                    msGetGidapp2suppliergstdtl = objcmnfunctions.GetMasterGID("A2SG");
                                    msSQL = " insert into agr_mst_tapp2suppliergstdtl " +
                                            " (app2suppliergstdtl_gid, apploan2supplierdtl_gid, institution2branch_gid, gst_state, gst_no, created_by, created_date) " +
                                            " select @app2suppliergstdtl_gid := '" + msGetGidapp2suppliergstdtl + "', @apploan2supplierdtl_gid:= '" + msGetGidloan2supplierdtl + "', " +
                                            " institution2branch_gid, gst_state, gst_no, created_by, created_date " +
                                            " from agr_mst_tapp2suppliergstdtl " +
                                            " where apploan2supplierdtl_gid='" + dt7["apploan2supplierdtl_gid"].ToString() + "' " +
                                            " and app2suppliergstdtl_gid='" + dt8["app2suppliergstdtl_gid"].ToString() + "'";
                                    mnResultapp2suppliergstdtl = objdbconn.ExecuteNonQuerySQL(msSQL);
                                }
                            }
                            else
                            {
                                mnResultapp2suppliergstdtl = 1;
                            }
                        }
                    }
                    else
                    {
                        mnResultloan2supplierdtl = 1;
                        mnResultapp2suppliergstdtl = 1;
                    }
                    // Insert data in application loan2supplier payment Table
                    msSQL = "select apploan2supplierpayment_gid from agr_mst_tapploan2supplierpayment where application2loan_gid='" + dt["application2loan_gid"].ToString() + "'";
                    dt_datatable9 = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable9.Rows.Count != 0)
                    {
                        foreach (DataRow dt9 in dt_datatable9.Rows)
                        {
                            msGetGidloan2supplierpayment = objcmnfunctions.GetMasterGID("ALSD");
                            msSQL = " insert into agr_mst_tapploan2supplierpayment " +
                                    " (apploan2supplierpayment_gid, application_gid, application2loan_gid, commodity_gid, commodity_name," +
                                    " supplierpayment_type, supplierpayment_typegid, maxpercent_paymentterm, created_by, created_date,  apploan2supplierdtl_gid) " +
                                    " select @apploan2supplierpayment_gid := '" + msGetGidloan2supplierpayment + "', @application_gid:= '" + msGetGidApplication + "', " +
                                    " @application2loan_gid:= '" + msGetGidapplication2loan + "', " +
                                    " commodity_gid, commodity_name,supplierpayment_type, supplierpayment_typegid, " +
                                    " maxpercent_paymentterm, created_by, created_date,   @apploan2supplierdtl_gid:= '" + msGetGidloan2supplierdtl + "' " +
                                    " from agr_mst_tapploan2supplierpayment " +
                                    " where application2loan_gid='" + dt["application2loan_gid"].ToString() + "' " +
                                    " and apploan2supplierpayment_gid='" + dt9["apploan2supplierpayment_gid"].ToString() + "'";
                            mnResultloan2supplierpayment = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    else
                    {
                        mnResultloan2supplierpayment = 1;
                    }
                    // Insert data in application2buyer Table
                    msSQL = "select application2buyer_gid from agr_mst_tapplication2buyer where application2loan_gid='" + dt["application2loan_gid"].ToString() + "'";
                    dt_datatable10 = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable10.Rows.Count != 0)
                    {
                        foreach (DataRow dt10 in dt_datatable10.Rows)
                        {
                            msGetGidapplication2buyer = objcmnfunctions.GetMasterGID("AP2B");
                            msSQL = " insert into agr_mst_tapplication2buyer " +
                                    " (application2buyer_gid, application2loan_gid, buyer_gid, buyer_name, buyer_limit, " +
                                    " availed_limit, balance_limit, bill_tenure, margin, created_by, created_date, updated_by, " +
                                    " updated_date) " +
                                    " select @application2buyer_gid := '" + msGetGidapplication2buyer + "'," +
                                    " @application2loan_gid:= '" + msGetGidapplication2loan + "', " +
                                    "  buyer_gid, buyer_name, buyer_limit,  " +
                                    " availed_limit, balance_limit, bill_tenure, margin, created_by, created_date, updated_by, " +
                                    " updated_date " +
                                    " from agr_mst_tapplication2buyer " +
                                    " where application2loan_gid='" + dt["application2loan_gid"].ToString() + "' " +
                                    " and application2buyer_gid='" + dt10["application2buyer_gid"].ToString() + "'";
                            mnResultapplication2buyer = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    else
                    {
                        mnResultapplication2buyer = 1;
                    }
                    // Insert data in application Service Charges Details Table
                    msSQL = "select application2servicecharge_gid from agr_mst_tapplicationservicecharge where application2loan_gid='" + dt["application2loan_gid"].ToString() + "'";
                    dt_datatable11 = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable11.Rows.Count != 0)
                    {
                        foreach (DataRow dt11 in dt_datatable11.Rows)
                        {
                            msGetGidappl2servicecharge = objcmnfunctions.GetMasterGID("AP2C");
                            msSQL = " insert into agr_mst_tapplicationservicecharge " +
                                    " (application2servicecharge_gid, application_gid, application2loan_gid, processing_fee, processing_collectiontype," +
                                    " doc_charges, doccharge_collectiontype, fieldvisit_charges, fieldvisit_charges_collectiontype, adhoc_fee, " +
                                    " adhoc_collectiontype, life_insurance, lifeinsurance_collectiontype, acct_insurance, total_collect, " +
                                    " total_deduct, product_type, producttype_gid, created_by, created_date, updated_date, updated_by, " +
                                    " acctinsurance_collectiontype) " +
                                    " select @application2servicecharge_gid := '" + msGetGidappl2servicecharge + "',@application_gid:= '" + msGetGidApplication + "'," +
                                    " @application2loan_gid:= '" + msGetGidapplication2loan + "', processing_fee, processing_collectiontype," +
                                     " doc_charges, doccharge_collectiontype, fieldvisit_charges, fieldvisit_charges_collectiontype, adhoc_fee, " +
                                    " adhoc_collectiontype, life_insurance, lifeinsurance_collectiontype, acct_insurance, total_collect, " +
                                    " total_deduct, product_type, producttype_gid, created_by, created_date, updated_date, updated_by, " +
                                    " acctinsurance_collectiontype " +
                                    " from agr_mst_tapplicationservicecharge " +
                                    " where application2loan_gid='" + dt["application2loan_gid"].ToString() + "' " +
                                    " and application2servicecharge_gid='" + dt11["application2servicecharge_gid"].ToString() + "'";
                            mnResultappl2servicecharge = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    else
                    {
                        mnResultappl2servicecharge = 1;
                    }
                    // Insert data in application trade2warehouse Details Table
                    msSQL = "select applicationtrade2warehouse_gid from agr_mst_tapplicationtrade2warehouse where application2loan_gid='" + dt["application2loan_gid"].ToString() + "'";
                    dt_datatable12 = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable12.Rows.Count != 0)
                    {
                        foreach (DataRow dt12 in dt_datatable12.Rows)
                        {
                            msGetGidappltrade2warehouse = objcmnfunctions.GetMasterGID("AT2W");
                            msSQL = " insert into agr_mst_tapplicationtrade2warehouse " +
                                    " (applicationtrade2warehouse_gid, application2trade_gid, application2loan_gid, application_gid, creditor_gid," +
                                    " warehouse_gid, warehouse_agency, warehouse_name, typeofwarehouse_name, volume_uomgid, volume_uom, " +
                                    " totalcapacity_volume, totalcapacity_area, totalcapacityarea_uomgid, area_uom, warehouse2address_gid, " +
                                    " warehouse_address, capacity_commodity, capacity_panina, created_by, created_date, updated_by, updated_date) " +
                                    " select @applicationtrade2warehouse_gid := '" + msGetGidappltrade2warehouse + "',@application_gid:= '" + msGetGidApplication + "'," +
                                    " @application2loan_gid:= '" + msGetGidapplication2loan + "', application2trade_gid,creditor_gid," +
                                    " warehouse_gid, warehouse_agency, warehouse_name, typeofwarehouse_name, volume_uomgid, volume_uom, " +
                                    " totalcapacity_volume, totalcapacity_area, totalcapacityarea_uomgid, area_uom, warehouse2address_gid, " +
                                    " warehouse_address, capacity_commodity, capacity_panina, created_by, created_date, updated_by, updated_date " +
                                    " from agr_mst_tapplicationtrade2warehouse " +
                                    " where application2loan_gid='" + dt["application2loan_gid"].ToString() + "' " +
                                    " and applicationtrade2warehouse_gid='" + dt12["applicationtrade2warehouse_gid"].ToString() + "'";
                            mnResultappltrade2warehouse = objdbconn.ExecuteNonQuerySQL(msSQL);

                            // Insert data in application2trade Details Table
                            msSQL = "select application2trade_gid from agr_mst_tapplication2trade where application2loan_gid='" + dt["application2loan_gid"].ToString() + "'";
                            dt_datatable13 = objdbconn.GetDataTable(msSQL);
                            if (dt_datatable13.Rows.Count != 0)
                            {
                                foreach (DataRow dt13 in dt_datatable13.Rows)
                                {
                                    msGetGidapplication2trade = objcmnfunctions.GetMasterGID("APTR");
                                    msSQL = " insert into agr_mst_tapplication2trade " +
                                            " (application2trade_gid, application_gid, producttype_gid, producttype_name, productsubtype_gid, " +
                                            " productsubtype_name, salescontract_availability, scopeof_transportgid, scopeof_transport, " +
                                            " scopeof_loadinggid, scopeof_loading, scopeof_unloadinggid, scopeof_unloading, " +
                                            " scopeof_qualityandquantitygid, scopeof_qualityandquantity, scopeof_moisturegainlossgid, " +
                                            " scopeof_moisturegainloss, created_by, created_date, updated_by, updated_date, application2loan_gid, " +
                                            " scopeof_insurancegid, scopeof_insurance) " +
                                            " select @application2trade_gid := '" + msGetGidapplication2trade + "',@application_gid:= '" + msGetGidApplication + "'," +
                                            " producttype_gid, producttype_name, productsubtype_gid, " +
                                            " productsubtype_name, salescontract_availability, scopeof_transportgid, scopeof_transport, " +
                                            " scopeof_loadinggid, scopeof_loading, scopeof_unloadinggid, scopeof_unloading, " +
                                            " scopeof_qualityandquantitygid, scopeof_qualityandquantity, scopeof_moisturegainlossgid, " +
                                            " scopeof_moisturegainloss, created_by, created_date, updated_by, updated_date, " +
                                            " @application2loan_gid:= '" + msGetGidapplication2loan + "', " +
                                            " scopeof_insurancegid, scopeof_insurance " +
                                            " from agr_mst_tapplication2trade " +
                                            " where application2loan_gid='" + dt["application2loan_gid"].ToString() + "' " +
                                            " and application2trade_gid='" + dt13["application2trade_gid"].ToString() + "'";
                                    mnResultapplication2trade = objdbconn.ExecuteNonQuerySQL(msSQL);
                                }
                            }
                            else
                            {
                                mnResultapplication2trade = 1;
                            }
                        }
                    }
                    else
                    {
                        mnResultappltrade2warehouse = 1;
                        mnResultapplication2trade = 1;
                    }

                }
            }

            else
            {
                mnResultapplication2loan = 1;
                mnResultpaymenttypecustomer = 1;
                mnResultapplication2product = 1;
                mnResultproduct2gststatus = 1;
                mnResultproduct2commoditytrade = 1;
                mnResultproduct2commoditydoc = 1;
                mnResultloan2supplierdtl = 1;
                mnResultapp2suppliergstdtl = 1;
                mnResultloan2supplierpayment = 1;
                mnResultapplication2buyer = 1;
                mnResultappl2servicecharge = 1;
                mnResultappltrade2warehouse = 1;
                mnResultapplication2trade = 1;
            }

            if (mnResultapplicationcreation != 0 && mnResultapplicationcontact != 0 && mnResultapplicationgeneticcode != 0 && mnResultapplicationemail != 0 && mnResultinstitution != 0 && mnResultinstitutionbranch != 0 && mnResultinstitutionmobile != 0 && mnResultinstitutionemail != 0 && mnResultinstitutionaddress != 0 && mnResultinstitutionlicense != 0 && mnResultinstitutiondocument != 0 && mnResultinstitutionratingdtl != 0 && mnResultinstitutionbankdtl != 0 && mnResultcontact != 0 && mnResultcontactpanabsencereason != 0 && mnResultcontactpanform60 != 0 && mnResultcontactmobile != 0 && mnResultcontactemail != 0 && mnResultcontactidproof != 0 && mnResultcontactaddress != 0 && mnResultcontactdocument != 0 && mnResultapplication2loan != 0 && mnResultpaymenttypecustomer != 0 && mnResultapplication2product != 0 && mnResultproduct2gststatus != 0 && mnResultproduct2commoditytrade != 0 && mnResultproduct2commoditydoc != 0 && mnResultloan2supplierdtl != 0 && mnResultapp2suppliergstdtl != 0 && mnResultloan2supplierpayment != 0 && mnResultapplication2buyer != 0 && mnResultappl2servicecharge != 0 && mnResultappltrade2warehouse != 0 && mnResultapplication2trade != 0 && mnResultonboardinitiate != 0 && mnResultamendmentreason!=0 )
            {

                msSQL = "INSERT INTO agr_mst_tonboardclonelog(" +
                        " onboard_gid," +
                        " existingapplication_gid," +
                        " cloneapplication_gid, " +
                        " clone_status, " +
                        " existing_overallvaliditydate, " +
                        " existing_overallcalculation, " +
                        " created_by," +
                        " created_date)" +
                        " VALUES(" +
                        "'" + values.buyeronboard_gid + "'," +
                        "'" + values.application_gid + "'," +
                        "'" + msGetGidApplication + "'," +
                        "'Amendment'," +
                        "'" + Convert.ToDateTime(values.validityto_date).ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        "'" + values.calculationoveralllimit_validity + "'," +
                        "'" + employee_gid + "'," +
                        "current_timestamp)";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                values.status = true;
                values.application_no = lsapp_refno;
                values.message = "Application Amendmented Successfully";
                return true;
            }
            else
            {
                // Delete Tables

                msSQL = " select GROUP_CONCAT(institution_gid) as totalinstitution_gid " +
                        " from agr_mst_tinstitution where application_gid='" + msGetGidApplication + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.totalinstitution_gid = objODBCDatareader["totalinstitution_gid"].ToString();
                }
                msSQL = " delete from agr_mst_tinstitution2branch where institution_gid in ('" + values.totalinstitution_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tinstitution2mobileno where institution_gid in ('" + values.totalinstitution_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tinstitution2email where institution_gid in ('" + values.totalinstitution_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tinstitution2email where institution_gid in ('" + values.totalinstitution_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tinstitution2licensedtl where institution_gid in ('" + values.totalinstitution_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tinstitution2documentupload where institution_gid in ('" + values.totalinstitution_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tinstitution2ratingdetail where institution_gid in ('" + values.totalinstitution_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tinstitution2bankdtl here institution_gid in ('" + values.totalinstitution_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_trn_tdocumentchecktls where credit_gid in ('" + values.totalinstitution_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_trn_tcovanantdocumentcheckdtls where credit_gid in ('" + values.totalinstitution_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " select GROUP_CONCAT(contact_gid) as totalcontact_gid " +
                        " from agr_mst_tcontact where application_gid='" + msGetGidApplication + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.totalcontact_gid = objODBCDatareader["totalcontact_gid"].ToString();
                }
                msSQL = " delete from agr_mst_tcontact2panabsencereason where contact_gid in ('" + values.totalcontact_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tcontact2panform60 where contact_gid in ('" + values.totalcontact_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tcontact2mobileno where contact_gid in ('" + values.totalcontact_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tcontact2email where contact_gid in ('" + values.totalcontact_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tcontact2idproof where contact_gid in ('" + values.totalcontact_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tcontact2address where contact_gid in ('" + values.totalcontact_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tcontact2document where contact_gid in ('" + values.totalcontact_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_trn_tdocumentchecktls where credit_gid in ('" + values.totalcontact_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_trn_tcovanantdocumentcheckdtls where credit_gid in ('" + values.totalcontact_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " select GROUP_CONCAT(application2product_gid) as totalapplication2product_gid " +
                        " from agr_mst_tapplication2product where application_gid='" + msGetGidApplication + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.totalapplication2product_gid = objODBCDatareader["totalapplication2product_gid"].ToString();
                }
                msSQL = " delete from agr_mst_tappproduct2commoditygststatus where application2product_gid in ('" + values.totalapplication2product_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tappproduct2commoditytradedtl where application2product_gid in ('" + values.totalapplication2product_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tappproduct2commoditydocument where application2product_gid in ('" + values.totalapplication2product_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " select GROUP_CONCAT(apploan2supplierdtl_gid) as totalapploan2supplierdtl_gid " +
                        " from agr_mst_tapploan2supplierdtl where application_gid='" + msGetGidApplication + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.totalapploan2supplierdtl_gid = objODBCDatareader["totalapploan2supplierdtl_gid"].ToString();
                }
                msSQL = " delete from agr_mst_tapp2suppliergstdtl where apploan2supplierdtl_gid in ('" + values.totalapploan2supplierdtl_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " select GROUP_CONCAT(application2loan_gid) as totalapplication2loan_gid " +
                       " from agr_mst_tapplication2loan where application_gid='" + msGetGidApplication + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.totalapplication2loan_gid = objODBCDatareader["totalapplication2loan_gid"].ToString();
                }
                msSQL = " delete from agr_mst_tapplication2buyer where application2loan_gid in ('" + values.totalapplication2loan_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tapplication where application_gid='" + msGetGidApplication + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tapplication2contactno where application_gid='" + msGetGidApplication + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tapplication2email where application_gid='" + msGetGidApplication + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tapplication2geneticcode where application_gid='" + msGetGidApplication + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tinstitution where application_gid='" + msGetGidApplication + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tcontact where application_gid='" + msGetGidApplication + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tapplication2loan where application_gid='" + msGetGidApplication + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tapploan2paymenttypecustomer where application_gid='" + msGetGidApplication + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tapplication2product where application_gid='" + msGetGidApplication + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tapploan2supplierdtl where application_gid='" + msGetGidApplication + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tapploan2supplierpayment where application_gid='" + msGetGidApplication + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tapplicationservicecharge where application_gid='" + msGetGidApplication + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tapplicationtrade2warehouse where application_gid='" + msGetGidApplication + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tapplication2trade where application_gid='" + msGetGidApplication + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tonboardinitiatedtl where application_gid='" + msGetGidApplication + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tamendmentreason where application_gid='" + msGetGidApplication + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = false;
                values.message = "Error Occurred while Amendmented the Application";
                return false;
            }

        }
        public void DaGetHistoryLogRemarksView(string application_gid, MdlApplCloneHistoryDtlLog values)
        {

            msSQL = " select amendmentreason_gid,application_gid,amendment_gid,amendment " + 
                    " from agr_mst_tamendmentreason a " +
                    " where application_gid = '" + application_gid + "' order by a.created_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var gethistorylogremarks_list = new List<historylogremarks_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    gethistorylogremarks_list.Add(new historylogremarks_list
                    {
                        amendmentreason_gid = dt["amendmentreason_gid"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        amendment_gid = dt["amendment_gid"].ToString(),
                        amendment = dt["amendment"].ToString()
                    });

                }
                values.historylogremarks_list = gethistorylogremarks_list;
            }
            dt_datatable.Dispose();

            values.status = true;
        }
        // Application Renewal
        public bool DaPostShortClosingAdd(string employee_gid, MdlMstCloneShortClosingAdd values)
        {
         //   msSQL = " select a.application_no,a.application_gid,a.processupdated_date as dateofapp " +
         //           " from agr_mst_tapplication a " +
         //           " where a.processupdated_date = (select max(c.processupdated_date) from agr_mst_tapplication c " +
         //           " where c.buyeronboard_gid = '" + values.buyer_gid + "' and a.onboarding_status = 'Proposal' and a.process_type = 'Accept') " +
         //           " union " +
         //           " select p.application_no,p.application_gid, p.headapproval_date as dateofapp from agr_mst_tapplication p " +
         //           " where (p.shortclosing_flag = 'Y' and p.approval_status like '%Rejected%' and p.headapproval_date = (select max(b.headapproval_date) from agr_mst_tapplication b " +
         //           " where b.approval_status like '%Rejected%' and b.buyeronboard_gid = '" + values.buyer_gid + "')) " +
         //           " union " +
         //           " select q.application_no,q.application_gid,q.creditheadapproval_date as dateofapp from agr_mst_tapplication q " +
         //           " where (q.shortclosing_flag = 'Y' and q.approval_status like '%Rejected%' and q.creditheadapproval_date = (select max(b.creditheadapproval_date) from agr_mst_tapplication b " +
         //           " where b.approval_status like '%Rejected%' and b.buyeronboard_gid = '" + values.buyer_gid + "')) " +
         //           " union " +
         //           " select r.application_no,r.application_gid,r.cccompleted_date as dateofapp from agr_mst_tapplication r " +
         //           " where (r.shortclosing_flag = 'Y' and r.approval_status like '%Rejected%' and r.cccompleted_date = (select max(b.cccompleted_date) from agr_mst_tapplication b " +
         //           " where b.approval_status like '%Rejected%' and b.buyeronboard_gid = '" + values.buyer_gid + "')) " +
         //           " order by dateofapp desc limit 1 ";
         //   objODBCDataReader = objdbconn.GetDataReader(msSQL);
         //   if (objODBCDataReader.HasRows == true)
         //   {
         //       values.refapplication_no = objODBCDataReader["application_no"].ToString();
         //       values.refapplication_gid = objODBCDataReader["application_gid"].ToString();
         //       values.dateofapp = objODBCDataReader["dateofapp"].ToString();
         //   }
         //   objODBCDataReader.Close();

            
         //   msSQL = " select a.shortclosing_flag from agr_mst_tapplication a " +
         //           " where a.created_date = (select max(c.created_date) from agr_mst_tapplication c " +
         //           " where c.buyeronboard_gid = '" + values.buyer_gid + "' and c.onboarding_status = 'Proposal') ";
         //   lsshortclosing_flag = objdbconn.GetExecuteScalar(msSQL);
         //   if (lsshortclosing_flag != "N")
         //   {
         //msSQL = " select a.approval_status from agr_mst_tapplication a " +
         //        " where a.created_date = (select max(c.created_date) from agr_mst_tapplication c " +
         //        " where c.buyeronboard_gid = '" + values.buyer_gid + "' and c.onboarding_status = 'Proposal' )";
         //       lsapproval_status = objdbconn.GetExecuteScalar(msSQL);
         //       if (lsapproval_status == "Rejected By Business" || lsapproval_status == "Rejected By Credit" || lsapproval_status == "CC Rejected")
         //       {
         //           msSQL = " select a.application_no,a.application_gid,a.processupdated_date as dateofapp " +
         //         " from agr_mst_tapplication a " +
         //         " where a.processupdated_date = (select max(c.processupdated_date) from agr_mst_tapplication c " +
         //         " where c.buyeronboard_gid = '" + values.buyer_gid + "' and c.onboarding_status = 'Proposal' and c.process_type = 'Accept' ) ";
         //           objODBCDataReader = objdbconn.GetDataReader(msSQL);
         //           if (objODBCDataReader.HasRows == true)
         //           {
         //               values.application_no = objODBCDataReader["application_no"].ToString();
         //               values.application_gid = objODBCDataReader["application_gid"].ToString();
         //           }
         //           objODBCDataReader.Close();
         //       }

         //   }
         //   else
         //   {
         //       msSQL = " select a.approval_status from agr_mst_tapplication a " +
         //         " where a.created_date = (select max(c.created_date) from agr_mst_tapplication c " +
         //         " where c.buyeronboard_gid = '" + values.buyer_gid + "' and c.onboarding_status = 'Proposal' and shortclosing_flag ='Y') ";
         //       lsapproval_status_check = objdbconn.GetExecuteScalar(msSQL);
         //       if (lsapproval_status_check == "Rejected By Business" || lsapproval_status_check == "Rejected By Credit" || lsapproval_status_check == "CC Rejected")
         //       {
         //           msSQL = " select a.application_no,a.application_gid,a.processupdated_date as dateofapp " +
         //           " from agr_mst_tapplication a " +
         //           " where a.processupdated_date = (select max(c.processupdated_date) from agr_mst_tapplication c " +
         //           " where c.buyeronboard_gid = '" + values.buyer_gid + "' and c.onboarding_status = 'Proposal' and c.process_type = 'Accept' ) ";
         //           objODBCDataReader = objdbconn.GetDataReader(msSQL);
         //           if (objODBCDataReader.HasRows == true)
         //           {
         //               values.application_no = objODBCDataReader["application_no"].ToString();
         //               values.application_gid = objODBCDataReader["application_gid"].ToString();
         //           }
         //           objODBCDataReader.Close();
         //       }
         //       else
         //       {
         //           msSQL = " select a.application_no,a.application_gid,a.processupdated_date as dateofapp " +
         //           " from agr_mst_tapplication a " +
         //           " where a.processupdated_date = (select max(c.processupdated_date) from agr_mst_tapplication c " +
         //           " where c.buyeronboard_gid = '" + values.buyer_gid + "' and c.onboarding_status = 'Proposal' and c.process_type = 'Accept'  and expired_flag !='Y' ) ";
         //           objODBCDataReader = objdbconn.GetDataReader(msSQL);
         //           if (objODBCDataReader.HasRows == true)
         //           {
         //               values.application_no = objODBCDataReader["application_no"].ToString();
         //               values.application_gid = objODBCDataReader["application_gid"].ToString();
         //           }
         //           objODBCDataReader.Close();
         //       }

         //   }



            msSQL = " select b.producttype_gid,b.productsubtype_gid,application_no,customer_urn,customer_name,entity_gid,entity_name, " +
                    " vertical_gid,vertical_name,verticaltaggs_gid,verticaltaggs_name,constitution_gid,constitution_name,businessunit_gid, " +
                    " businessunit_name,sa_status as sastatus,sa_id,sa_name,baselocation_gid,baselocation_name,cluster_gid,cluster_name,region_gid, " +
                    " region_name,zone_gid,zone_name,relationshipmanager_name,relationshipmanager_gid,drm_gid,drm_name,clustermanager_gid, " +
                    " clustermanager_name,zonalhead_name,zonalhead_gid,regionalhead_name,regionalhead_gid,businesshead_name,businesshead_gid, " +
                    " vernacular_language,vernacularlanguage_gid,contactpersonfirst_name,contactpersonmiddle_name, " +
                    " contactpersonlast_name,designation_gid,designation_type,landline_no,status as application_status, " +
                    " saname_gid,economical_flag,productcharge_flag,applicant_type,customerref_name, " +
                    " productcharges_status,mobile_no,email_address,approval_flag,gradingdraft_flag,hypothecation_flag,submitted_by, " +
                    " date_format(submitted_date, '%Y-%m-%d %H:%i:%s') as submitted_date,region,creditgroup_gid,creditgroup_name, " +
                    " a.program_gid,a.program_name,a.product_gid,a.product_name,a.sector_name,a.category_name,a.variety_gid,a.variety_name, " +
                    " a.botanical_name,a.alternative_name,approval_status,document_name,document_path,shortclosing_flag, " +
                    " validityfrom_date,validityto_date,onboarding_status," +
                    " productdesk_flag,productdesk_gid,productquery_status,contract_id,buyeronboard_gid, " +
                    " social_capital,trade_capital,overalllimit_amount,calculationoveralllimit_validity " +
                    " from agr_mst_tapplication a" +
                    " left join agr_mst_tapplication2loan b on a.application_gid = b.application_gid " +
                    " where a.application_gid='" + values.application_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.application_no = objODBCDatareader["application_no"].ToString();
                values.customer_urn = objODBCDatareader["customer_urn"].ToString();
                values.customer_name = objODBCDatareader["customer_name"].ToString();
                values.entity_gid = objODBCDatareader["entity_gid"].ToString();
                values.entity_name = objODBCDatareader["entity_name"].ToString();
                values.vertical_gid = objODBCDatareader["vertical_gid"].ToString();
                values.vertical_name = objODBCDatareader["vertical_name"].ToString();
                values.verticaltaggs_gid = objODBCDatareader["verticaltaggs_gid"].ToString();
                values.verticaltaggs_name = objODBCDatareader["verticaltaggs_name"].ToString();
                values.constitution_gid = objODBCDatareader["constitution_gid"].ToString();
                values.constitution_name = objODBCDatareader["constitution_name"].ToString();
                values.businessunit_gid = objODBCDatareader["businessunit_gid"].ToString();
                values.businessunit_name = objODBCDatareader["businessunit_name"].ToString();
                values.sastatus = objODBCDatareader["sastatus"].ToString();
                values.sa_id = objODBCDatareader["sa_id"].ToString();
                values.sa_name = objODBCDatareader["sa_name"].ToString();
                values.baselocation_gid = objODBCDatareader["baselocation_gid"].ToString();
                values.baselocation_name = objODBCDatareader["baselocation_name"].ToString();
                values.cluster_gid = objODBCDatareader["cluster_gid"].ToString();
                values.cluster_name = objODBCDatareader["cluster_name"].ToString();
                values.region_gid = objODBCDatareader["region_gid"].ToString();
                values.region_name = objODBCDatareader["region_name"].ToString();
                values.zone_gid = objODBCDatareader["zone_gid"].ToString();
                values.zone_name = objODBCDatareader["zone_name"].ToString();
                values.relationshipmanager_name = objODBCDatareader["relationshipmanager_name"].ToString();
                values.relationshipmanager_gid = objODBCDatareader["relationshipmanager_gid"].ToString();
                values.drm_gid = objODBCDatareader["drm_gid"].ToString();
                values.drm_name = objODBCDatareader["drm_name"].ToString();
                values.clustermanager_gid = objODBCDatareader["clustermanager_gid"].ToString();
                values.clustermanager_name = objODBCDatareader["clustermanager_name"].ToString();
                values.zonalhead_name = objODBCDatareader["zonalhead_name"].ToString();
                values.zonalhead_gid = objODBCDatareader["zonalhead_gid"].ToString();
                values.regionalhead_name = objODBCDatareader["regionalhead_name"].ToString();
                values.regionalhead_gid = objODBCDatareader["regionalhead_gid"].ToString();
                values.businesshead_name = objODBCDatareader["businesshead_name"].ToString();
                values.businesshead_gid = objODBCDatareader["businesshead_gid"].ToString();
                values.vernacular_language = objODBCDatareader["vernacular_language"].ToString();
                values.vernacularlanguage_gid = objODBCDatareader["vernacularlanguage_gid"].ToString();
                values.contactpersonfirst_name = objODBCDatareader["contactpersonfirst_name"].ToString();
                values.contactpersonmiddle_name = objODBCDatareader["contactpersonmiddle_name"].ToString();
                values.contactpersonlast_name = objODBCDatareader["contactpersonlast_name"].ToString();
                values.designation_gid = objODBCDatareader["designation_gid"].ToString();
                values.designation_type = objODBCDatareader["designation_type"].ToString();
                values.landline_no = objODBCDatareader["landline_no"].ToString();
                values.application_status = objODBCDatareader["application_status"].ToString();
                values.saname_gid = objODBCDatareader["saname_gid"].ToString();
                values.economical_flag = objODBCDatareader["economical_flag"].ToString();
                values.productcharge_flag = objODBCDatareader["productcharge_flag"].ToString();
                values.applicant_type = objODBCDatareader["applicant_type"].ToString();
                values.customerref_name = objODBCDatareader["customerref_name"].ToString();
                values.productcharges_status = objODBCDatareader["productcharges_status"].ToString();
                values.approval_status = objODBCDatareader["approval_status"].ToString();
                values.mobile_no = objODBCDatareader["mobile_no"].ToString();
                values.email_address = objODBCDatareader["email_address"].ToString();
                values.approval_flag = objODBCDatareader["approval_flag"].ToString();
                values.gradingdraft_flag = objODBCDatareader["gradingdraft_flag"].ToString();
                values.hypothecation_flag = objODBCDatareader["hypothecation_flag"].ToString();
                values.submitted_by = objODBCDatareader["submitted_by"].ToString();
                values.submitted_date = objODBCDatareader["submitted_date"].ToString();
                values.region = objODBCDatareader["region"].ToString();
                values.creditgroup_gid = objODBCDatareader["creditgroup_gid"].ToString();
                values.creditgroup_name = objODBCDatareader["creditgroup_name"].ToString();
                values.program_gid = objODBCDatareader["program_gid"].ToString();
                values.program_name = objODBCDatareader["program_name"].ToString();
                values.product_gid = objODBCDatareader["product_gid"].ToString();
                values.product_name = objODBCDatareader["product_name"].ToString();
                values.sector_name = objODBCDatareader["sector_name"].ToString();
                values.category_name = objODBCDatareader["category_name"].ToString();
                values.variety_gid = objODBCDatareader["variety_gid"].ToString();
                values.variety_name = objODBCDatareader["variety_name"].ToString();
                values.botanical_name = objODBCDatareader["botanical_name"].ToString();
                values.alternative_name = objODBCDatareader["alternative_name"].ToString();
                values.document_name = objODBCDatareader["document_name"].ToString();
                values.document_path = objODBCDatareader["document_path"].ToString();
                values.validityfrom_date = objODBCDatareader["validityfrom_date"].ToString();
                values.validityto_date = objODBCDatareader["validityto_date"].ToString();
                values.onboarding_status = objODBCDatareader["onboarding_status"].ToString();
                values.productdesk_flag = objODBCDatareader["productdesk_flag"].ToString();
                values.productdesk_gid = objODBCDatareader["productdesk_gid"].ToString();
                values.productquery_status = objODBCDatareader["productquery_status"].ToString();
                values.contract_id = objODBCDatareader["contract_id"].ToString();
                values.buyeronboard_gid = objODBCDatareader["buyeronboard_gid"].ToString();
                values.social_capital = objODBCDatareader["social_capital"].ToString();
                values.trade_capital = objODBCDatareader["trade_capital"].ToString();
                values.overalllimit_amount = objODBCDatareader["overalllimit_amount"].ToString();
                values.calculationoveralllimit_validity = objODBCDatareader["calculationoveralllimit_validity"].ToString();
                values.producttype_gid = objODBCDatareader["producttype_gid"].ToString();
                values.productsubtype_gid = objODBCDatareader["productsubtype_gid"].ToString();
            }
            objODBCDatareader.Close();

            //msSQL = " update agr_mst_tapplication set  expired_flag='Y'" +
            //        " where application_gid='" + values.application_gid + "'";
            //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);



            msSQL = "select shortclosing_flag  from agr_mst_tapplication a" +
                    " where buyeronboard_gid = '" + values.buyer_gid + "'" +
                    " and a.created_date = (select max(c.created_date) from agr_mst_tapplication c " +
                   " left join agr_mst_tapplication2loan b on c.application_gid = b.application_gid " +
                    " where c.buyeronboard_gid = '" + values.buyer_gid + "' and producttype_gid = '" + values.producttype_gid + "' and productsubtype_gid =  '" + values.productsubtype_gid + "')";
            string lslatestshortclosing_flag = objdbconn.GetExecuteScalar(msSQL);
            if (lslatestshortclosing_flag == "Y")
            {
                msSQL = " select a.application_no from agr_mst_tapplication a " +
                        " where buyeronboard_gid = '" + values.buyer_gid + "'" +
                        " and a.created_date = (select max(c.created_date) from agr_mst_tapplication c " +
                       " left join agr_mst_tapplication2loan b on c.application_gid = b.application_gid " +
                    " where c.buyeronboard_gid = '" + values.buyer_gid + "' and producttype_gid = '" + values.producttype_gid + "' and productsubtype_gid =  '" + values.productsubtype_gid + "')";
                values.lslatestapplication_refno = objdbconn.GetExecuteScalar(msSQL);
            }
            else
            {

                msSQL = " select a.application_no from agr_mst_tapplication a " +
                       " where buyeronboard_gid = '" + values.buyer_gid + "'" +
                       " and a.created_date = (select max(c.created_date) from agr_mst_tapplication c " +
                      " left join agr_mst_tapplication2loan b on c.application_gid = b.application_gid " +
                   " where c.buyeronboard_gid = '" + values.buyer_gid + "' and producttype_gid = '" + values.producttype_gid + "' and productsubtype_gid =  '" + values.productsubtype_gid + "' and shortclosing_flag ='Y')";
                values.lslatestapplication_refno = objdbconn.GetExecuteScalar(msSQL);
                if (values.lslatestapplication_refno == "")
                {
                    msSQL = " select a.application_no from agr_mst_tapplication a " +
                        " where a.processupdated_date = (select max(c.processupdated_date) from agr_mst_tapplication c " +
                        " left join agr_mst_tapplication2loan b on c.application_gid = b.application_gid " +
                        " where c.buyeronboard_gid = '" + values.buyer_gid + "' and " +
                        " c.onboarding_status = 'Proposal' and c.process_type = 'Accept' and producttype_gid = '" + values.producttype_gid + "' and productsubtype_gid =  '" + values.productsubtype_gid + "') ";
                values.lslatestapplication_refno = objdbconn.GetExecuteScalar(msSQL);
               }
            }

            msSQL = " select shortclosing_flag from agr_mst_tapplication where application_no='" + values.lslatestapplication_refno + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                values.applicationshortclosing_flag = objODBCDataReader["shortclosing_flag"].ToString();
            }
            objODBCDataReader.Close();

            msSQL = "select vertical_refno from ocs_mst_tvertical where vertical_gid='" + values.vertical_gid + "'";
            string lsvertical_refno = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select entity_gid from ocs_mst_tvertical where vertical_gid='" + values.vertical_gid + "'";
            string lsentity_gid = objdbconn.GetExecuteScalar(msSQL);

            //msSQL = "select entity_code from adm_mst_tentity where entity_gid='" + lsentity_gid + "'";
            //string lsentity_code = objdbconn.GetExecuteScalar(msSQL);
            string lsentity_code = "SA";

            string lsapp_refno = "ARN" + lsentity_code + lsvertical_refno + DateTime.Now.ToString("ddMMyyyy");

            string msGETRef = objcmnfunctions.GetMasterGID("APP");
            msGETRef = msGETRef.Replace("APP", "");
            string finalInt = "";
            if (values.applicationshortclosing_flag == "Y")
            {
                string pattern = @"[\d]{1}SC(.*)";
                Match m = Regex.Match(values.lslatestapplication_refno, pattern, RegexOptions.IgnoreCase);
                string toBeSearched = "SC";
                string tempString = m.Value.Substring(m.Value.IndexOf(toBeSearched) + toBeSearched.Length);

                //string tempString = m.Value.Replace("RN", String.Empty);
                int tempInt = Int16.Parse(tempString) + 1;
                if (tempInt >= 1 && tempInt <= 9)
                {
                    finalInt = tempInt.ToString().PadLeft(2, '0');
                }
                else
                {
                    finalInt = tempInt.ToString();
                }
            }
            else
            {
                finalInt = "01";
            }

            lsapp_refno = lsapp_refno + msGETRef + "SC" + finalInt;

            // Insert data in Application Table
            msGetGidApplication = objcmnfunctions.GetMasterGID("APPC");
            msSQL = " insert into agr_mst_tapplication( " +
                    " application_gid ," +
                    " application_no ," +
                    " refapplication_gid ," +
                    " refapplication_no ," +
                    " customer_urn ," +
                    " customer_name ," +
                    " entity_gid ," +
                    " entity_name ," +
                    " vertical_gid ," +
                    " vertical_name ," +
                    " verticaltaggs_gid ," +
                    " verticaltaggs_name ," +
                    " constitution_gid ," +
                    " constitution_name ," +
                    " businessunit_gid ," +
                    " businessunit_name ," +
                    " sa_status ," +
                    " sa_id ," +
                    " sa_name ," +
                    " baselocation_gid ," +
                    " baselocation_name ," +
                    " cluster_gid ," +
                    " cluster_name ," +
                    " region_gid ," +
                    " region_name ," +
                    " zone_gid ," +
                    " zone_name ," +
                    " relationshipmanager_name ," +
                    " relationshipmanager_gid ," +
                    " drm_gid ," +
                    " drm_name ," +
                    " clustermanager_gid ," +
                    " clustermanager_name ," +
                    " zonalhead_name ," +
                    " zonalhead_gid ," +
                    " regionalhead_name ," +
                    " regionalhead_gid ," +
                    " businesshead_name ," +
                    " businesshead_gid ," +
                    " vernacular_language ," +
                    " vernacularlanguage_gid ," +
                    " contactpersonfirst_name ," +
                    " contactpersonmiddle_name ," +
                    " contactpersonlast_name ," +
                    " designation_gid ," +
                    " designation_type ," +
                    " landline_no ," +
                    " social_capital ," +
                    " trade_capital ," +
                    " overalllimit_amount ," +
                    " calculationoveralllimit_validity ," +
                    " status ," +
                    " saname_gid ," +
                    " economical_flag ," +
                    " productcharge_flag ," +
                    " applicant_type ," +
                    " customerref_name ," +
                    " approval_status ," +
                    " mobile_no ," +
                    " email_address ," +
                    " approval_flag ," +
                    " gradingdraft_flag ," +
                    " hypothecation_flag ," +
                    " ccsubmit_flag ," +
                    " meeting_status ," +
                    " region ," +
                    " momapproval_flag ," +
                    " mom_flag ," +
                    " momdocumentupload_flag ," +
                    " headapproval_status ," +
                    " document_name ," +
                    " document_path ," +
                    " creditgroup_gid ," +
                    " creditgroup_name ," +
                    " creditgroup_status ," +
                    " creditheadapproval_status ," +
                    " program_gid ," +
                    " program_name ," +
                    " docchecklist_makerflag ," +
                    " docchecklist_checkerflag ," +
                    " docchecklist_approvalflag ," +
                    " cccompleted_flag ," +
                    " hierarchyupdated_flag ," +
                    " product_gid ," +
                    " product_name ," +
                    " sector_name ," +
                    " category_name ," +
                    " variety_gid ," +
                    " variety_name ," +
                    " botanical_name ," +
                    " alternative_name ," +
                    " pslcompleted_flag ," +
                    " sanction_approvalflag ," +
                    " shortclosing_flag ," +
                    " validityfrom_date," +
                    " validityto_date," +
                    " onboarding_status," +
                    " productdesk_flag," +
                    " productdesk_gid," +
                    " productquery_status," +
                   " contract_id," +
                    " buyeronboard_gid," +
                    " productcharges_status," +
                    " shortclosing_reason," +
                    " expired_flag," +
                    " created_by , " +
                    " created_date ) " +
                    " values (" +
                   "'" + msGetGidApplication + "'," +
                   "'" + lsapp_refno + "'," +
                   "'" + values.application_gid + "'," +
                   "'" + values.application_no + "'," +
                   "'" + values.customer_urn + "'," +
                   "'" + values.customer_name + "'," +
                   "'" + values.entity_gid + "'," +
                   "'" + values.entity_name + "'," +
                   "'" + values.vertical_gid + "'," +
                   "'" + values.vertical_name + "'," +
                   "'" + values.verticaltaggs_gid + "'," +
                   "'" + values.verticaltaggs_name + "'," +
                   "'" + values.constitution_gid + "'," +
                   "'" + values.constitution_name + "'," +
                   "'" + values.businessunit_gid + "'," +
                   "'" + values.businessunit_name + "'," +
                   "'" + values.sastatus + "'," +
                   "'" + values.sa_id + "'," +
                   "'" + values.sa_name + "'," +
                   "'" + values.baselocation_gid + "'," +
                   "'" + values.baselocation_name + "'," +
                   "'" + values.cluster_gid + "'," +
                   "'" + values.cluster_name + "'," +
                   "'" + values.region_gid + "'," +
                   "'" + values.region_name + "'," +
                   "'" + values.zone_gid + "'," +
                   "'" + values.zone_name + "'," +
                   "'" + values.relationshipmanager_name + "'," +
                   "'" + values.relationshipmanager_gid + "'," +
                   "'" + values.drm_gid + "'," +
                   "'" + values.drm_name + "'," +
                   "'" + values.clustermanager_gid + "'," +
                   "'" + values.clustermanager_name + "'," +
                   "'" + values.zonalhead_name + "'," +
                   "'" + values.zonalhead_gid + "'," +
                   "'" + values.regionalhead_name + "'," +
                   "'" + values.regionalhead_gid + "'," +
                   "'" + values.businesshead_name + "'," +
                   "'" + values.businesshead_gid + "'," +
                   "'" + values.vernacular_language + "'," +
                   "'" + values.vernacularlanguage_gid + "'," +
                   "'" + values.contactpersonfirst_name + "'," +
                   "'" + values.contactpersonmiddle_name + "'," +
                   "'" + values.contactpersonlast_name + "'," +
                   "'" + values.designation_gid + "'," +
                   "'" + values.designation_type + "'," +
                   "'" + values.landline_no + "'," +
                   "'" + values.social_capital + "'," +
                   "'" + values.trade_capital + "'," +
                   "'" + values.overalllimit_amount + "'," +
                   "'" + values.calculationoveralllimit_validity + "'," +
                   "'Completed'," +
                   "'" + values.saname_gid + "'," +
                   "'Y'," +
                   "'Y'," +
                   "'" + values.applicant_type + "'," +
                   "'" + values.customerref_name + "'," +
                   "'Incomplete'," +
                   "'" + values.mobile_no + "'," +
                   "'" + values.email_address + "'," +
                   "'N'," +
                   "'N'," +
                   "'N'," +
                   "'N'," +
                   "'Pending'," +
                   "'" + values.region + "'," +
                   "'N'," +
                   "'N'," +
                   "'N'," +
                   "'Pending'," +
                   "'" + values.document_name + "'," +
                   "'" + values.document_path + "'," +
                   "'" + values.creditgroup_gid + "'," +
                   "'" + values.creditgroup_name + "'," +
                   "'Pending'," +
                   "'Pending'," +
                   "'" + values.program_gid + "'," +
                   "'" + values.program_name + "'," +
                   "'N', " +
                   "'N', " +
                   "'N', " +
                   "'N', " +
                   "'N'," +
                   "'" + values.product_gid + "'," +
                   "'" + values.product_name + "'," +
                   "'" + values.sector_name + "'," +
                   "'" + values.category_name + "'," +
                   "'" + values.variety_gid + "'," +
                   "'" + values.variety_name + "'," +
                   "'" + values.botanical_name + "'," +
                   "'" + values.alternative_name + "'," +
                   "'N'," +
                   "'N', " +
                   "'Y'," +
                   "'" + Convert.ToDateTime(values.validityfrom_date).ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                   "'" + Convert.ToDateTime(values.validityto_date).ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                   "'" + values.onboarding_status + "'," +
                   "'" + values.productdesk_flag + "'," +
                   "'" + values.productdesk_gid + "'," +
                   "'" + values.productquery_status + "'," +
                   "'" + values.contract_id + "'," +
                   "'" + values.buyeronboard_gid + "'," +
                   "'" + values.productcharges_status + "'," +
                   "'" + values.shortclosing_reason.Replace("'", "") + "'," +
                   "'Y'," +
                   "'" + employee_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResultapplicationcreation = objdbconn.ExecuteNonQuerySQL(msSQL);

            // Insert data in Onboardinitiate Detail Table
            msSQL = "select onboardinitiatedtl_gid from agr_mst_tonboardinitiatedtl where application_gid='" + values.application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msSQL = " insert into agr_mst_tonboardinitiatedtl " +
                            " (buyeronboard_gid, supplieronboard_gid, application_gid, product_gid, " +
                            " product_name, program_gid, program_name, initiated_remarks, shortclosing_flag , created_byname, " +
                            " created_by, created_date) " +
                            " select buyeronboard_gid,supplieronboard_gid, @application_gid:= '" + msGetGidApplication + "',product_gid, " +
                            " product_name, program_gid, program_name, initiated_remarks, @shortclosing_flag := 'Y', created_byname, " +
                            " created_by, @created_date:= '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                            " from agr_mst_tonboardinitiatedtl " +
                            " where application_gid='" + values.application_gid + "'";
                    mnResultonboardinitiate = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            else
            {
                mnResultonboardinitiate = 1;
            }

            // Insert data in Application2Contact Table
            msSQL = "select application2contact_gid from agr_mst_tapplication2contactno where application_gid='" + values.application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msGetGidContactno = objcmnfunctions.GetMasterGID("A2CN");
                    msSQL = " insert into agr_mst_tapplication2contactno " +
                            " (application2contact_gid, application_gid, mobile_no, primary_mobileno, whatsapp_mobileno, created_by," +
                            " created_date, updated_by, updated_date) " +
                            " select @application2contact_gid := '" + msGetGidContactno + "', @application_gid:= '" + msGetGidApplication + "', mobile_no, " +
                            " primary_mobileno,whatsapp_mobileno,created_by,created_date, updated_by," +
                            " updated_date from agr_mst_tapplication2contactno " +
                            " where application_gid='" + values.application_gid + "'" +
                            " and application2contact_gid='" + dt["application2contact_gid"].ToString() + "'";
                    mnResultapplicationcontact = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            else
            {
                mnResultapplicationcontact = 1;
            }

            // Insert data in Application2Email Table
            msSQL = "select application2email_gid from agr_mst_tapplication2email where application_gid='" + values.application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msGetGidemail = objcmnfunctions.GetMasterGID("A2EA");
                    msSQL = " insert into agr_mst_tapplication2email " +
                            " (application2email_gid, application_gid, email_address, primary_emailaddress,created_by," +
                            " created_date, updated_by, updated_date) " +
                            " select @application2email_gid := '" + msGetGidemail + "', @application_gid:= '" + msGetGidApplication + "', email_address, " +
                            " primary_emailaddress,created_by,created_date, updated_by," +
                            " updated_date from agr_mst_tapplication2email " +
                            " where application_gid='" + values.application_gid + "'" +
                            " and application2email_gid='" + dt["application2email_gid"].ToString() + "'";
                    mnResultapplicationemail = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            else
            {
                mnResultapplicationemail = 1;
            }

            // Insert data in Application2GeneticCode Table from CAD Table
            msSQL = "select application2geneticcode_gid from agr_mst_tapplication2geneticcode where application_gid='" + values.application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msGetGidgeneticcode = objcmnfunctions.GetMasterGID("A2GC");
                    msSQL = " insert into agr_mst_tapplication2geneticcode " +
                            " (application2geneticcode_gid, application_gid, geneticcode_gid, geneticcode_name,genetic_status,genetic_remarks," +
                            " created_by, created_date, updated_by, updated_date) " +
                            " select @application2geneticcode_gid := '" + msGetGidgeneticcode + "', @application_gid:= '" + msGetGidApplication + "',  " +
                            " geneticcode_gid, geneticcode_name,genetic_status,genetic_remarks,created_by,created_date, updated_by," +
                            " updated_date from agr_mst_tapplication2geneticcode " +
                            " where application_gid='" + values.application_gid + "' " +
                            " and application2geneticcode_gid='" + dt["application2geneticcode_gid"].ToString() + "'";
                    mnResultapplicationgeneticcode = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            else
            {
                mnResultapplicationgeneticcode = 1;
            }
            // Insert data in Institution Table 
            msSQL = "select institution_gid from agr_mst_tinstitution where application_gid='" + values.application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msGetGidinstitution = objcmnfunctions.GetMasterGID("APIN");
                    msSQL = " insert into agr_mst_tinstitution " +
                            " (institution_gid,application_gid,application_no,company_name,date_incorporation," +
                            " form60document_name,form60document_path,companypan_no,year_business,month_business,cin_no,official_telephoneno," +
                            " officialemail_address,companytype_gid,companytype_name,stakeholder_type,stakeholdertype_gid,assessmentagency_gid," +
                            " assessmentagency_name,assessmentagencyrating_gid,assessmentagencyrating_name,ratingas_on,amlcategory_gid,amlcategory_name," +
                            " businesscategory_gid,businesscategory_name,contactperson_firstname,contactperson_middlename,contactperson_lastname," +
                            " contactperson_contactno,designation_gid,designation,lastyear_turnover,escrow,start_date,end_date,created_by," +
                            " created_date,updated_date,updated_by,urn,urn_status,observations,bureau_response,institution_status," +
                            " businessstart_date,bureauname_name,bureau_score,bureauname_gid,bureauscore_date,cicdocument_name,cicdocument_path," +
                            " mobile_no,email_address,economical_status,social_capital,trade_capital,official_mailid,economical_flag," +
                            " startupasofloansanction_date,occupation_gid,occupation,lineofactivity_gid,lineofactivity,bsrcode_gid,bsrcode," +
                            " pslcategory_gid,pslcategory,weakersection_gid,weakersection,pslpurpose_gid,pslpurpose,totalsanction_financialinstitution," +
                            " pslsanction_limit,natureofentity_gid,natureofentity,indulgeinmarketing_activity,plantandmachineryinvestment_gid," +
                            " plantandmachineryinvestment,turnover_gid,turnover,msmeclassification_gid,msmeclassification,loansanction_date," +
                            " entityincorporation_date,hq_metropolitancity,clientdtl_gid,client_dtl,psltagging_flag,credit_status,tan_number," +
                            " sundrydebt_adv,fixed_assets,profit,revenue,incometax_returnsstatus) " +
                            " select @institution_gid := '" + msGetGidinstitution + "', @application_gid:= '" + msGetGidApplication + "', application_no,company_name,date_incorporation, " +
                            " form60document_name,form60document_path,companypan_no,year_business,month_business,cin_no,official_telephoneno," +
                            " officialemail_address,companytype_gid,companytype_name,stakeholder_type,stakeholdertype_gid,assessmentagency_gid," +
                            " assessmentagency_name,assessmentagencyrating_gid,assessmentagencyrating_name,ratingas_on,amlcategory_gid,amlcategory_name," +
                            " businesscategory_gid,businesscategory_name,contactperson_firstname,contactperson_middlename,contactperson_lastname," +
                            " contactperson_contactno,designation_gid,designation,lastyear_turnover,escrow,start_date,end_date,created_by," +
                            " created_date,updated_date,updated_by,urn,urn_status,observations,bureau_response,institution_status," +
                            " businessstart_date,bureauname_name,bureau_score,bureauname_gid,bureauscore_date,cicdocument_name,cicdocument_path," +
                            " mobile_no,email_address,economical_status,social_capital,trade_capital,official_mailid,economical_flag," +
                            " startupasofloansanction_date,occupation_gid,occupation,lineofactivity_gid,lineofactivity,bsrcode_gid,bsrcode," +
                            " pslcategory_gid,pslcategory,weakersection_gid,weakersection,pslpurpose_gid,pslpurpose,totalsanction_financialinstitution," +
                            " pslsanction_limit,natureofentity_gid,natureofentity,indulgeinmarketing_activity,plantandmachineryinvestment_gid," +
                            " plantandmachineryinvestment,turnover_gid,turnover,msmeclassification_gid,msmeclassification,loansanction_date," +
                            " entityincorporation_date,hq_metropolitancity,clientdtl_gid,client_dtl,psltagging_flag,credit_status,tan_number," +
                            " sundrydebt_adv,fixed_assets,profit,revenue,incometax_returnsstatus from agr_mst_tinstitution " +
                            " where application_gid='" + values.application_gid + "' " +
                            " and institution_gid='" + dt["institution_gid"].ToString() + "'";
                    mnResultinstitution = objdbconn.ExecuteNonQuerySQL(msSQL);

                    // Insert data in Institution Branch Table from CAD Table
                    msSQL = "select institution2branch_gid from agr_mst_tinstitution2branch where institution_gid='" + dt["institution_gid"].ToString() + "'";
                    dt_datatable2 = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable2.Rows.Count != 0)
                    {
                        foreach (DataRow dt2 in dt_datatable2.Rows)
                        {
                            msGetGidinstitutionbranch = objcmnfunctions.GetMasterGID("ITGS");
                            msSQL = " insert into ocs_mst_tinstitution2branch " +
                                    " (institution2branch_gid, institution_gid, " +
                                    " gst_registered, gst_state, gst_no, created_by, created_date, updated_date, updated_by, gststate_gid, status, " +
                                    " authentication_status, returnfilling_status, verification_status )" +
                                    " select @institution2branch_gid := '" + msGetGidinstitutionbranch + "', @institution_gid:= '" + msGetGidinstitution + "',  " +
                                    " gst_registered, gst_state, gst_no, created_by, created_date, updated_date, updated_by, gststate_gid, status, " +
                                    " authentication_status, returnfilling_status, verification_status " +
                                    " from ocs_mst_tinstitution2branch " +
                                    " where institution_gid='" + dt["institution_gid"].ToString() + "' " +
                                    " and institution2branch_gid='" + dt2["institution2branch_gid"].ToString() + "'";
                            mnResultinstitutionbranch = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    else
                    {
                        mnResultinstitutionbranch = 1;
                    }

                    // Insert data in Institution Mobile Table 
                    msSQL = "select institution2mobileno_gid from agr_mst_tinstitution2mobileno where institution_gid='" + dt["institution_gid"].ToString() + "'";
                    dt_datatable3 = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable3.Rows.Count != 0)
                    {
                        foreach (DataRow dt3 in dt_datatable3.Rows)
                        {
                            msGetGidinstitutionmobile = objcmnfunctions.GetMasterGID("IT2M");
                            msSQL = " insert into agr_mst_tinstitution2mobileno " +
                                    " (institution2mobileno_gid, institution_gid, mobile_no, primary_status, whatsapp_no, landline_no, created_by, created_date, updated_date, updated_by ) " +
                                    " select @institution2mobileno_gid := '" + msGetGidinstitutionmobile + "', @institution_gid:= '" + msGetGidinstitution + "',  " +
                                    "  mobile_no, primary_status, whatsapp_no, landline_no, created_by, created_date, updated_date, updated_by " +
                                    " from agr_mst_tinstitution2mobileno " +
                                    " where institution_gid='" + dt["institution_gid"].ToString() + "' " +
                                    " and institution2mobileno_gid='" + dt3["institution2mobileno_gid"].ToString() + "'";
                            mnResultinstitutionmobile = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    else
                    {
                        mnResultinstitutionmobile = 1;
                    }

                    // Insert data in Institution Email Table 
                    msSQL = "select institution2email_gid from agr_mst_tinstitution2email where institution_gid='" + dt["institution_gid"].ToString() + "'";
                    dt_datatable4 = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable4.Rows.Count != 0)
                    {
                        foreach (DataRow dt4 in dt_datatable4.Rows)
                        {
                            msGetGidinstitutionemail = objcmnfunctions.GetMasterGID("IT2E");
                            msSQL = " insert into agr_mst_tinstitution2email " +
                                    " (institution2email_gid, institution_gid, email_address, primary_status, created_by, created_date, updated_date, updated_by) " +
                                    " select @institution2email_gid := '" + msGetGidinstitutionemail + "', @institution_gid:= '" + msGetGidinstitution + "',  " +
                                    " email_address, primary_status, created_by, created_date, updated_date, updated_by " +
                                    " from agr_mst_tinstitution2email " +
                                    " where institution_gid='" + dt["institution_gid"].ToString() + "' " +
                                    " and institution2email_gid='" + dt4["institution2email_gid"].ToString() + "'";
                            mnResultinstitutionemail = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    else
                    {
                        mnResultinstitutionemail = 1;
                    }

                    // Insert data in Institution Address Table
                    msSQL = "select institution2address_gid from agr_mst_tinstitution2address where institution_gid='" + dt["institution_gid"].ToString() + "'";
                    dt_datatable5 = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable5.Rows.Count != 0)
                    {
                        foreach (DataRow dt5 in dt_datatable5.Rows)
                        {
                            msGetGidinstitutionaddress = objcmnfunctions.GetMasterGID("IT2A");
                            msSQL = " insert into agr_mst_tinstitution2address " +
                                    " (institution2address_gid, institution_gid, addresstype_gid, addresstype_name, primary_status, addressline1, addressline2, landmark, postal_code, city, " +
                                    " taluka, district, state, country, latitude, longitude, created_by, created_date, updated_date, updated_by) " +
                                    " select @institution2address_gid := '" + msGetGidinstitutionaddress + "', @institution_gid:= '" + msGetGidinstitution + "',  " +
                                    " addresstype_gid, addresstype_name, primary_status, addressline1, addressline2, landmark, postal_code, city, " +
                                    " taluka, district, state, country, latitude, longitude, created_by, created_date, updated_date, updated_by " +
                                    " from agr_mst_tinstitution2address " +
                                    " where institution_gid='" + dt["institution_gid"].ToString() + "' " +
                                    " and institution2address_gid='" + dt5["institution2address_gid"].ToString() + "'";
                            mnResultinstitutionaddress = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    else
                    {
                        mnResultinstitutionaddress = 1;
                    }
                    // Insert data in Institution License Detail Table
                    msSQL = "select institution2licensedtl_gid from agr_mst_tinstitution2licensedtl where institution_gid='" + dt["institution_gid"].ToString() + "'";
                    dt_datatable6 = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable6.Rows.Count != 0)
                    {
                        foreach (DataRow dt6 in dt_datatable6.Rows)
                        {
                            msGetGidinstitutionlicense = objcmnfunctions.GetMasterGID("IT2L");
                            msSQL = " insert into agr_mst_tinstitution2licensedtl " +
                                    " (institution2licensedtl_gid, institution_gid, licensetype_gid, licensetype_name, license_no, issue_date, expiry_date, created_by, created_date, updated_date, updated_by) " +
                                    " select @institution2licensedtl_gid := '" + msGetGidinstitutionlicense + "', @institution_gid:= '" + msGetGidinstitution + "',  " +
                                    " licensetype_gid, licensetype_name, license_no, issue_date, expiry_date, created_by, created_date, updated_date, updated_by" +
                                    " from agr_mst_tinstitution2licensedtl " +
                                    " where institution_gid='" + dt["institution_gid"].ToString() + "' " +
                                    " and institution2licensedtl_gid='" + dt6["institution2licensedtl_gid"].ToString() + "'";
                            mnResultinstitutionlicense = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    else
                    {
                        mnResultinstitutionlicense = 1;
                    }

                    // Insert data in Institution Document Table
                    msSQL = "select institution2documentupload_gid from agr_mst_tinstitution2documentupload where institution_gid='" + dt["institution_gid"].ToString() + "'";
                    dt_datatable7 = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable7.Rows.Count != 0)
                    {
                        foreach (DataRow dt7 in dt_datatable7.Rows)
                        {
                            msGetGidinstitutiondocument = objcmnfunctions.GetMasterGID("INDO");
                            msSQL = " insert into agr_mst_tinstitution2documentupload " +
                                    " ( institution2documentupload_gid, institution_gid, " +
                                    " document_name, document_path, created_by, created_date, updated_date, updated_by, companydocument_gid, document_title, " +
                                    " document_id, covenant_type, untagged_type, untagged_by, untagged_date, covenant_periods, covenantperiod_updatedby, covenantperiod_updateddate) " +
                                    " select @institution2documentupload_gid := '" + msGetGidinstitutiondocument + "', @institution_gid:= '" + msGetGidinstitution + "',  " +
                                    " document_name, document_path, created_by, created_date, updated_date, updated_by, companydocument_gid, document_title, " +
                                    " document_id, covenant_type, untagged_type, untagged_by, untagged_date, covenant_periods, covenantperiod_updatedby, covenantperiod_updateddate " +
                                    " from agr_mst_tinstitution2documentupload " +
                                    " where institution_gid='" + dt["institution_gid"].ToString() + "' " +
                                    " and institution2documentupload_gid='" + dt7["institution2documentupload_gid"].ToString() + "'";
                            mnResultinstitutiondocument = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    else
                    {
                        mnResultinstitutiondocument = 1;
                    }


                    // Insert data in Institution Rating Details Table
                    msSQL = "select institution2ratingdetail_gid from agr_mst_tinstitution2ratingdetail where institution_gid='" + dt["institution_gid"].ToString() + "'";
                    dt_datatable8 = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable8.Rows.Count != 0)
                    {
                        foreach (DataRow dt8 in dt_datatable8.Rows)
                        {
                            msGetGidinstitutionratingdtl = objcmnfunctions.GetMasterGID("INRD");
                            msSQL = " insert into agr_mst_tinstitution2ratingdetail " +
                                    " ( institution2ratingdetail_gid, institution_gid,application_gid, " +
                                    " creditrating_agencygid, creditrating_agencyname, creditrating_gid, creditrating_name, assessed_on, creditrating_link, created_by, created_date, updated_by, updated_date ) " +
                                    " select @institution2ratingdetail_gid := '" + msGetGidinstitutionratingdtl + "', @institution_gid:= '" + msGetGidinstitution + "', @application_gid:= '" + msGetGidApplication + "', " +
                                    "  creditrating_agencygid, creditrating_agencyname, creditrating_gid, creditrating_name, assessed_on, creditrating_link, created_by, created_date, updated_by, updated_date" +
                                    " from agr_mst_tinstitution2ratingdetail " +
                                    " where institution_gid='" + dt["institution_gid"].ToString() + "' " +
                                    " and institution2ratingdetail_gid='" + dt8["institution2ratingdetail_gid"].ToString() + "'";
                            mnResultinstitutionratingdtl = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    else
                    {
                        mnResultinstitutionratingdtl = 1;
                    }

                    // Insert data in Institution Bank Details Table 
                    msSQL = "select institution2bankdtl_gid from agr_mst_tinstitution2bankdtl where institution_gid='" + dt["institution_gid"].ToString() + "'";
                    dt_datatable9 = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable9.Rows.Count != 0)
                    {
                        foreach (DataRow dt9 in dt_datatable9.Rows)
                        {
                            msGetGidinstitutionbankdtl = objcmnfunctions.GetMasterGID("I2BD");
                            msSQL = " insert into agr_mst_tinstitution2bankdtl " +
                                    " (institution2bankdtl_gid, institution_gid, application_gid, " +
                                    " bankaccount_number, ifsc_code, bank_name, branch_name, micr_code, accountholder_name, accounttype_gid, accounttype_name, " +
                                    " joint_account, jointaccountholder_name, chequebookfacility_available, accountopen_date, created_by, created_date, " +
                                    " updated_by, updated_date, bank_address, bankaccount_name, bankaccounttype_gid, bankaccounttype_name, confirmbankaccountnumber, " +
                                    " joinaccount_status, joinaccount_name, chequebook_status, disbursement_accountstatus, primary_status ) " +
                                    " select @institution2bankdtl_gid := '" + msGetGidinstitutionbankdtl + "', @institution_gid:= '" + msGetGidinstitution + "', @application_gid:= '" + msGetGidApplication + "', " +
                                    " bankaccount_number, ifsc_code, bank_name, branch_name, micr_code, accountholder_name, accounttype_gid, accounttype_name, " +
                                    " joint_account, jointaccountholder_name, chequebookfacility_available, accountopen_date, created_by, created_date, " +
                                    " updated_by, updated_date, bank_address, bankaccount_name, bankaccounttype_gid, bankaccounttype_name, confirmbankaccountnumber, " +
                                    " joinaccount_status, joinaccount_name, chequebook_status, disbursement_accountstatus, primary_status " +
                                    " from agr_mst_tinstitution2bankdtl " +
                                    " where institution_gid='" + dt["institution_gid"].ToString() + "' " +
                                    " and institution2bankdtl_gid='" + dt9["institution2bankdtl_gid"].ToString() + "'";
                            mnResultinstitutionbankdtl = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    else
                    {
                        mnResultinstitutionbankdtl = 1;
                    }

                }
            }
            else
            {
                mnResultinstitution = 1;
                mnResultinstitutionbranch = 1;
                mnResultinstitutionmobile = 1;
                mnResultinstitutionemail = 1;
                mnResultinstitutionaddress = 1;
                mnResultinstitutionlicense = 1;
                mnResultinstitutiondocument = 1;
                mnResultinstitutionratingdtl = 1;
                mnResultinstitutionbankdtl = 1;
            }
            // Insert data in Contact Table
            msSQL = "select contact_gid from agr_mst_tcontact where application_gid='" + values.application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msGetGidcontact = objcmnfunctions.GetMasterGID("CTCT");
                    msSQL = " insert into agr_mst_tcontact " +
                            " (contact_gid,application_gid,application_no,pan_status,pan_no,aadhar_no, " +
                            " first_name,last_name,middle_name,mother_lastname,individual_dob,age,gender_gid,gender_name," +
                            "designation_gid,designation_name,educationalqualification_gid,educationalqualification_name,main_occupation," +
                            "annual_income,monthly_income,pep_status,pepverified_date,stakeholdertype_gid,stakeholder_type,user_type," +
                            "maritalstatus_gid,maritalstatus_name,father_firstname,father_middlename,father_lastname,father_dob,father_age," +
                            "mother_firstname,mother_middlename,mother_dob,mother_age,spouse_firstname,spouse_middlename,spouse_lastname," +
                            "spouse_dob,spouse_age,ownershiptype_gid,ownershiptype_name,propertyholder_gid,propertyholder_name," +
                            "residencetype_gid,residencetype_name,currentresidence_years,branch_distance,bureauname_gid,bureauname_name," +
                            "bureau_score,bureauscore_date,observations,bureau_response,cicdocument_name,cicdocument_path,created_by," +
                            "created_date,updated_date,updated_by,contact_status,email_address,mobile_no,incometype_gid,incometype_name," +
                            "group_gid,group_name,profile,urn_status,urn,fathernominee_status,mothernominee_status,spousenominee_status," +
                            "othernominee_status,relationshiptype,nomineefirst_name,nominee_middlename,nominee_lastname,nominee_dob," +
                            "nominee_age,totallandinacres,cultivatedland,previouscrop,prposedcrop,economical_status,social_capital," +
                            "trade_capital,institution_gid,institution_name,startupasofloansanction_date,occupation_gid,occupation," +
                            "lineofactivity_gid,lineofactivity,bsrcode_gid,bsrcode,pslcategory_gid,pslcategory,weakersection_gid,weakersection," +
                            "pslpurpose_gid,pslpurpose,totalsanction_financialinstitution,pslsanction_limit,natureofentity_gid,natureofentity," +
                            "indulgeinmarketing_activity,plantandmachineryinvestment_gid,plantandmachineryinvestment,turnover_gid,turnover," +
                            "msmeclassification_gid,msmeclassification,loansanction_date,entityincorporation_date,hq_metropolitancity,clientdtl_gid," +
                            "client_dtl,economical_flag,psltagging_flag,credit_status)" +
                            " select @contact_gid := '" + msGetGidcontact + "', @application_gid:= '" + msGetGidApplication + "',  " +
                            " application_no,pan_status,pan_no,aadhar_no, " +
                            " first_name,last_name,middle_name,mother_lastname,individual_dob,age,gender_gid,gender_name," +
                            "designation_gid,designation_name,educationalqualification_gid,educationalqualification_name,main_occupation," +
                            "annual_income,monthly_income,pep_status,pepverified_date,stakeholdertype_gid,stakeholder_type,user_type," +
                            "maritalstatus_gid,maritalstatus_name,father_firstname,father_middlename,father_lastname,father_dob,father_age," +
                            "mother_firstname,mother_middlename,mother_dob,mother_age,spouse_firstname,spouse_middlename,spouse_lastname," +
                            "spouse_dob,spouse_age,ownershiptype_gid,ownershiptype_name,propertyholder_gid,propertyholder_name," +
                            "residencetype_gid,residencetype_name,currentresidence_years,branch_distance,bureauname_gid,bureauname_name," +
                            "bureau_score,bureauscore_date,observations,bureau_response,cicdocument_name,cicdocument_path,created_by," +
                            "created_date,updated_date,updated_by,contact_status,email_address,mobile_no,incometype_gid,incometype_name," +
                            "group_gid,group_name,profile,urn_status,urn,fathernominee_status,mothernominee_status,spousenominee_status," +
                            "othernominee_status,relationshiptype,nomineefirst_name,nominee_middlename,nominee_lastname,nominee_dob," +
                            "nominee_age,totallandinacres,cultivatedland,previouscrop,prposedcrop,economical_status,social_capital," +
                            "trade_capital,institution_gid,institution_name,startupasofloansanction_date,occupation_gid,occupation," +
                            "lineofactivity_gid,lineofactivity,bsrcode_gid,bsrcode,pslcategory_gid,pslcategory,weakersection_gid,weakersection," +
                            "pslpurpose_gid,pslpurpose,totalsanction_financialinstitution,pslsanction_limit,natureofentity_gid,natureofentity," +
                            "indulgeinmarketing_activity,plantandmachineryinvestment_gid,plantandmachineryinvestment,turnover_gid,turnover," +
                            "msmeclassification_gid,msmeclassification,loansanction_date,entityincorporation_date,hq_metropolitancity,clientdtl_gid," +
                            "client_dtl,economical_flag,psltagging_flag,credit_status " +
                            " from agr_mst_tcontact " +
                            " where application_gid='" + values.application_gid + "' " +
                            " and contact_gid='" + dt["contact_gid"].ToString() + "'";
                    mnResultcontact = objdbconn.ExecuteNonQuerySQL(msSQL);

                    // Insert data in Contact Pan Absence Reason Table
                    msSQL = "select contact2panabsencereason_gid from agr_mst_tcontact2panabsencereason where contact_gid='" + dt["contact_gid"].ToString() + "'";
                    dt_datatable2 = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable2.Rows.Count != 0)
                    {
                        foreach (DataRow dt2 in dt_datatable2.Rows)
                        {
                            msGetGidcontactpanreason = objcmnfunctions.GetMasterGID("C2PR");
                            msSQL = " insert into agr_mst_tcontact2panabsencereason " +
                                    " (contact2panabsencereason_gid, contact_gid, panabsencereason, created_by, created_date, updated_date, updated_by) " +
                                    " select @contact2panabsencereason_gid := '" + msGetGidcontactpanreason + "', @contact_gid:= '" + msGetGidcontact + "',  " +
                                    " panabsencereason, created_by, created_date, updated_date, updated_by " +
                                    " from agr_mst_tcontact2panabsencereason " +
                                    " where contact_gid='" + dt["contact_gid"].ToString() + "' " +
                                    " and contact2panabsencereason_gid='" + dt2["contact2panabsencereason_gid"].ToString() + "'";
                            mnResultcontactpanabsencereason = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    else
                    {
                        mnResultcontactpanabsencereason = 1;
                    }

                    // Insert data in Contact Pan Form-60 Document Table 
                    msSQL = "select contact2panform60_gid from agr_mst_tcontact2panform60 where contact_gid='" + dt["contact_gid"].ToString() + "'";
                    dt_datatable3 = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable3.Rows.Count != 0)
                    {
                        foreach (DataRow dt3 in dt_datatable3.Rows)
                        {
                            msGetGidcontactpanform60 = objcmnfunctions.GetMasterGID("CF60");
                            msSQL = " insert into agr_mst_tcontact2panform60 " +
                                    " (contact2panform60_gid, contact_gid, document_name, document_path, created_by, created_date, updated_date, updated_by) " +
                                    " select @contact2panform60_gid := '" + msGetGidcontactpanform60 + "', @contact_gid:= '" + msGetGidcontact + "',  " +
                                    " document_name, document_path, created_by, created_date, updated_date, updated_by " +
                                    " from agr_mst_tcontact2panform60 " +
                                    " where contact_gid='" + dt["contact_gid"].ToString() + "' " +
                                    " and contact2panform60_gid='" + dt3["contact2panform60_gid"].ToString() + "'";
                            mnResultcontactpanform60 = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    else
                    {
                        mnResultcontactpanform60 = 1;
                    }

                    // Insert data in Contact Mobile Number Table
                    msSQL = "select contact2mobileno_gid from agr_mst_tcontact2mobileno where contact_gid='" + dt["contact_gid"].ToString() + "'";
                    dt_datatable4 = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable4.Rows.Count != 0)
                    {
                        foreach (DataRow dt4 in dt_datatable4.Rows)
                        {
                            msGetGidcontactmobile = objcmnfunctions.GetMasterGID("C2MN");
                            msSQL = " insert into agr_mst_tcontact2mobileno " +
                                    " (contact2mobileno_gid, contact_gid, mobile_no, primary_status, whatsapp_no, landline_no, " +
                                    " created_by, created_date, updated_date, updated_by ) " +
                                    " select @contact2mobileno_gid := '" + msGetGidcontactmobile + "', @contact_gid:= '" + msGetGidcontact + "',  " +
                                    " mobile_no, primary_status, whatsapp_no, landline_no, " +
                                    " created_by, created_date, updated_date, updated_by " +
                                    " from agr_mst_tcontact2mobileno " +
                                    " where contact_gid='" + dt["contact_gid"].ToString() + "' " +
                                    " and contact2mobileno_gid='" + dt4["contact2mobileno_gid"].ToString() + "'";
                            mnResultcontactmobile = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    else
                    {
                        mnResultcontactmobile = 1;
                    }

                    // Insert data in Contact Email Table
                    msSQL = "select contact2email_gid from agr_mst_tcontact2email where contact_gid='" + dt["contact_gid"].ToString() + "'";
                    dt_datatable5 = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable5.Rows.Count != 0)
                    {
                        foreach (DataRow dt5 in dt_datatable5.Rows)
                        {
                            msGetGidcontactemail = objcmnfunctions.GetMasterGID("C2EA");
                            msSQL = " insert into agr_mst_tcontact2email " +
                                    " (contact2email_gid, contact_gid, email_address, primary_status, created_by, created_date, updated_date, updated_by ) " +
                                    " select @contact2email_gid := '" + msGetGidcontactemail + "', @contact_gid:= '" + msGetGidcontact + "',  " +
                                    " email_address, primary_status, created_by, created_date, updated_date, updated_by " +
                                    " from agr_mst_tcontact2email " +
                                    " where contact_gid='" + dt["contact_gid"].ToString() + "' " +
                                    " and contact2email_gid='" + dt5["contact2email_gid"].ToString() + "'";
                            mnResultcontactemail = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    else
                    {
                        mnResultcontactemail = 1;
                    }
                    // Insert data in Contact ID Proof Table 
                    msSQL = "select contact2idproof_gid from agr_mst_tcontact2idproof where contact_gid='" + dt["contact_gid"].ToString() + "'";
                    dt_datatable6 = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable6.Rows.Count != 0)
                    {
                        foreach (DataRow dt6 in dt_datatable6.Rows)
                        {
                            msGetGidcontactidproof = objcmnfunctions.GetMasterGID("C2IP");
                            msSQL = " insert into agr_mst_tcontact2idproof " +
                                    " (contact2idproof_gid, contact_gid, idproof_gid, idproof_name, idproof_no, idproof_dob, file_no, document_name, " +
                                    " document_path, created_by, created_date, updated_date, updated_by ) " +
                                    " select @contact2idproof_gid := '" + msGetGidcontactidproof + "', @contact_gid:= '" + msGetGidcontact + "',  " +
                                    " idproof_gid, idproof_name, idproof_no, idproof_dob, file_no, document_name, " +
                                    " document_path, created_by, created_date, updated_date, updated_by " +
                                    " from agr_mst_tcontact2idproof " +
                                    " where contact_gid='" + dt["contact_gid"].ToString() + "' " +
                                    " and contact2idproof_gid='" + dt6["contact2idproof_gid"].ToString() + "'";
                            mnResultcontactidproof = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    else
                    {
                        mnResultcontactidproof = 1;
                    }

                    // Insert data in Contact Address Table
                    msSQL = "select contact2address_gid from agr_mst_tcontact2address where contact_gid='" + dt["contact_gid"].ToString() + "'";
                    dt_datatable7 = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable7.Rows.Count != 0)
                    {
                        foreach (DataRow dt7 in dt_datatable7.Rows)
                        {
                            msGetGidcontactaddress = objcmnfunctions.GetMasterGID("C2AD");
                            msSQL = " insert into agr_mst_tcontact2address " +
                                    " (contact2address_gid, contact_gid, addresstype_gid, addresstype_name, primary_status, addressline1, " +
                                    " addressline2, landmark, postal_code, city, taluka, district, state, country, latitude, longitude, " +
                                    " created_by, created_date, updated_date, updated_by ) " +
                                    " select @contact2address_gid := '" + msGetGidcontactaddress + "', @contact_gid:= '" + msGetGidcontact + "',  " +
                                    " addresstype_gid, addresstype_name, primary_status, addressline1, " +
                                    " addressline2, landmark, postal_code, city, taluka, district, state, country, latitude, longitude, " +
                                    " created_by, created_date, updated_date, updated_by" +
                                    " from agr_mst_tcontact2address " +
                                    " where contact_gid='" + dt["contact_gid"].ToString() + "' " +
                                    " and contact2address_gid='" + dt7["contact2address_gid"].ToString() + "'";
                            mnResultcontactaddress = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    else
                    {
                        mnResultcontactaddress = 1;
                    }

                    // Insert data in Contact Document Table
                    msSQL = "select contact2document_gid from agr_mst_tcontact2document where contact_gid='" + dt["contact_gid"].ToString() + "'";
                    dt_datatable8 = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable8.Rows.Count != 0)
                    {
                        foreach (DataRow dt8 in dt_datatable8.Rows)
                        {
                            msGetGidcontactdocument = objcmnfunctions.GetMasterGID("C2DO");
                            msSQL = " insert into agr_mst_tcontact2document " +
                                    " (contact2document_gid, contact_gid, individualdocument_gid, document_gid, document_title, document_name, document_path, " +
                                    " created_by, created_date, updated_date, updated_by, covenant_type, untagged_type, untagged_by, untagged_date, " +
                                    " covenant_periods, covenantperiod_updatedby, covenantperiod_updateddate) " +
                                    " select @contact2document_gid := '" + msGetGidcontactdocument + "', @contact_gid:= '" + msGetGidcontact + "',  " +
                                    " individualdocument_gid, document_gid, document_title, document_name, document_path, " +
                                    " created_by, created_date, updated_date, updated_by, covenant_type, untagged_type, untagged_by, untagged_date, " +
                                    " covenant_periods, covenantperiod_updatedby, covenantperiod_updateddate" +
                                    " from agr_mst_tcontact2document " +
                                    " where contact_gid='" + dt["contact_gid"].ToString() + "' " +
                                    " and contact2document_gid='" + dt8["contact2document_gid"].ToString() + "'";
                            mnResultcontactdocument = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    else
                    {
                        mnResultcontactdocument = 1;
                    }
                }
            }

            else
            {
                mnResultcontact = 1;
                mnResultcontactpanabsencereason = 1;
                mnResultcontactpanform60 = 1;
                mnResultcontactmobile = 1;
                mnResultcontactemail = 1;
                mnResultcontactidproof = 1;
                mnResultcontactaddress = 1;
                mnResultcontactdocument = 1;
            }

            msSQL = " select contact_gid, individualdocument_gid, contact2document_gid from agr_mst_tcontact2document where contact_gid in " +
                    " (select contact_gid from agr_mst_tcontact where application_gid='" + msGetGidApplication + "')";
            dt_datatable21 = objdbconn.GetDataTable(msSQL);
            foreach (DataRow dt in dt_datatable21.Rows)
            {
                string lscovenant_type = "", lsdocumenttype_gid = "", lsdocumenttype_name = "", lscompanydocument_name = "";

                string msGetdefDocchecklistGID = objcmnfunctions.GetMasterGID("DOCG");
                msSQL = " select individualdocument_gid,documenttypes_gid,documenttype_name,individualdocument_name,covenant_type " +
                        " from ocs_mst_tindividualdocument where individualdocument_gid='" + dt["individualdocument_gid"].ToString() + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lsdocumenttype_gid = objODBCDatareader["documenttypes_gid"].ToString();
                    lsdocumenttype_name = objODBCDatareader["documenttype_name"].ToString();
                    lscompanydocument_name = objODBCDatareader["individualdocument_name"].ToString();
                    lscovenant_type = objODBCDatareader["covenant_type"].ToString();
                }
                objODBCDatareader.Close();
                msSQL = " insert into agr_trn_tdocumentchecktls(" +
                        " documentcheckdtl_gid," +
                        " application_gid," +
                        " credit_gid, " +
                        " individualdocument_gid, " +
                         " documentuploaded_gid, " +
                        " documenttype_gid," +
                        " documenttype_code," +
                        " documenttype_name," +
                        " covenant_type, " +
                        " tagged_by, " +
                        " created_date," +
                        " created_by)" +
                        " VALUES(" +
                        "'" + msGetdefDocchecklistGID + "'," +
                        "'" + values.application_gid + "'," +
                        "'" + dt["contact_gid"].ToString() + "'," +
                        "'" + dt["individualdocument_gid"].ToString() + "'," +
                        "'" + dt["contact2document_gid"].ToString() + "'," +
                        "'" + lsdocumenttype_gid + "'," +
                        "'" + lsdocumenttype_name + "'," +
                        "'" + lscompanydocument_name.Replace("'", "") + "'," +
                        "'" + lscovenant_type + "'," +
                        "'N'," +
                        "current_timestamp," +
                        "'" + employee_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (lscovenant_type == "Y")
                {
                    string msGetDocchecklistGID = objcmnfunctions.GetMasterGID("CDCL");
                    msSQL = " insert into agr_trn_tcovanantdocumentcheckdtls(" +
                           " covenantdocumentcheckdtl_gid," +
                           " application_gid," +
                           " credit_gid," +
                           " individualdocument_gid," +
                           " documentuploaded_gid, " +
                           " documenttype_gid," +
                           " documenttype_code," +
                           " documenttype_name," +
                           " covenant_type, " +
                           " tagged_by, " +
                           " created_date," +
                           " created_by)" +
                           " VALUES(" +
                           "'" + msGetDocchecklistGID + "'," +
                           "'" + values.application_gid + "'," +
                           "'" + dt["contact_gid"].ToString() + "'," +
                           "'" + dt["individualdocument_gid"].ToString() + "'," +
                           "'" + dt["contact2document_gid"].ToString() + "'," +
                           "'" + lsdocumenttype_gid + "'," +
                           "'" + lsdocumenttype_name + "'," +
                           "'" + lscompanydocument_name.Replace("'", "") + "'," +
                           "'" + lscovenant_type + "'," +
                           "'N'," +
                           "current_timestamp," +
                           "'" + employee_gid + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            dt_datatable21.Dispose();

            msSQL = " select contact_gid from agr_mst_tcontact2document  where contact_gid in " +
                    " (select contact_gid from agr_mst_tcontact where application_gid='" + msGetGidApplication + "') group by contact_gid";
            dt_datatable21 = objdbconn.GetDataTable(msSQL);
            foreach (DataRow dt in dt_datatable21.Rows)
            {
                DaAgrMstScannedDocument objvalues1 = new DaAgrMstScannedDocument();
                string lscredit_gid = dt["contact_gid"].ToString();
                objvalues1.DaGroupDocChecklistinfo(values.application_gid, lscredit_gid, employee_gid);
            }
            dt_datatable21.Dispose();


            msSQL = " select institution_gid, companydocument_gid, institution2documentupload_gid from agr_mst_tinstitution2documentupload  where institution_gid in " +
                   " (select institution_gid from agr_mst_tinstitution where application_gid='" + msGetGidApplication + "')";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            foreach (DataRow dt in dt_datatable.Rows)
            {
                string lscovenant_type = "", lsdocumenttype_gid = "", lsdocumenttype_name = "", lscompanydocument_name = "";

                string msGetdefDocchecklistGID = objcmnfunctions.GetMasterGID("DOCG");
                msSQL = " select companydocument_gid,documenttypes_gid,documenttype_name,companydocument_name,covenant_type " +
                            " from ocs_mst_tcompanydocument where companydocument_gid='" + dt["companydocument_gid"].ToString() + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lsdocumenttype_gid = objODBCDatareader["documenttypes_gid"].ToString();
                    lsdocumenttype_name = objODBCDatareader["documenttype_name"].ToString();
                    lscompanydocument_name = objODBCDatareader["companydocument_name"].ToString();
                    lscovenant_type = objODBCDatareader["covenant_type"].ToString();
                }
                objODBCDatareader.Close();
                msSQL = " insert into agr_trn_tdocumentchecktls(" +
                        " documentcheckdtl_gid," +
                        " application_gid," +
                        " credit_gid, " +
                        " companydocument_gid, " +
                        " documentuploaded_gid, " +
                        " documenttype_gid," +
                        " documenttype_code," +
                        " documenttype_name," +
                        " covenant_type, " +
                        " tagged_by, " +
                        " created_date," +
                        " created_by)" +
                        " VALUES(" +
                        "'" + msGetdefDocchecklistGID + "'," +
                        "'" + values.application_gid + "'," +
                        "'" + dt["institution_gid"].ToString() + "'," +
                        "'" + dt["companydocument_gid"].ToString() + "'," +
                        "'" + dt["institution2documentupload_gid"].ToString() + "'," +
                        "'" + lsdocumenttype_gid + "'," +
                        "'" + lsdocumenttype_name + "'," +
                        "'" + lscompanydocument_name.Replace("'", "") + "'," +
                        "'" + lscovenant_type + "'," +
                        "'N'," +
                        "current_timestamp," +
                        "'" + employee_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (lscovenant_type == "Y")
                {
                    string msGetDocchecklistGID = objcmnfunctions.GetMasterGID("CDCL");
                    msSQL = " insert into agr_trn_tcovanantdocumentcheckdtls(" +
                           " covenantdocumentcheckdtl_gid," +
                           " application_gid," +
                           " credit_gid," +
                           " companydocument_gid," +
                           " documentuploaded_gid, " +
                           " documenttype_gid," +
                           " documenttype_code," +
                           " documenttype_name," +
                           " covenant_type, " +
                           " tagged_by, " +
                           " created_date," +
                           " created_by)" +
                           " VALUES(" +
                           "'" + msGetDocchecklistGID + "'," +
                           "'" + values.application_gid + "'," +
                           "'" + dt["institution_gid"].ToString() + "'," +
                           "'" + dt["companydocument_gid"].ToString() + "'," +
                           "'" + dt["institution2documentupload_gid"].ToString() + "'," +
                           "'" + lsdocumenttype_gid + "'," +
                           "'" + lsdocumenttype_name + "'," +
                           "'" + lscompanydocument_name.Replace("'", "") + "'," +
                           "'" + lscovenant_type + "'," +
                           "'N'," +
                           "current_timestamp," +
                           "'" + employee_gid + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            dt_datatable.Dispose();

            msSQL = " select institution_gid from agr_mst_tinstitution2documentupload  where institution_gid in " +
                    " (select institution_gid from agr_mst_tinstitution where application_gid='" + msGetGidApplication + "') group by institution_gid";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            foreach (DataRow dt in dt_datatable.Rows)
            {
                DaAgrMstScannedDocument objvalues = new DaAgrMstScannedDocument();
                string lscredit_gid = dt["institution_gid"].ToString();
                objvalues.DaGroupDocChecklistinfo(values.application_gid, lscredit_gid, employee_gid);
            }
            dt_datatable.Dispose();

            // Insert data in Application2Loan(Product Details) Table Details
            msSQL = "select application2loan_gid from agr_mst_tapplication2loan where application_gid='" + values.application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msGetGidapplication2loan = objcmnfunctions.GetMasterGID("AP2L");
                    msSQL = " insert into agr_mst_tapplication2loan " +
                            " (application2loan_gid, application_gid, facilityrequested_date, product_type, producttype_gid, productsub_type, productsubtype_gid, loantype_gid, loan_type, " +
                            " loanfacility_amount, rate_interest, penal_interest, tenureproduct_year, tenureproduct_month, tenureproduct_days, tenureoverall_limit, " +
                            " facility_type, facility_mode, created_by, created_date, scheme_type, principalfrequency_name, principalfrequency_gid, " +
                            " interestfrequency_name, interestfrequency_gid, program_gid, program, primaryvaluechain_gid, primaryvaluechain_name, " +
                            " secondaryvaluechain_gid, secondaryvaluechain_name, interest_status, moratorium_status, moratorium_type, " +
                            " moratorium_startdate, moratorium_enddate, source_type, guideline_value, guideline_date, marketvalue_date, market_value, " +
                            " forcedsource_value, collateralSSV_value, forcedvalueassessed_on, collateralobservation_summary, enduse_purpose, " +
                            " updated_by, updated_date, interchangeability, expiry_date, report_structure, document_limit, product_gid, product_name, " +
                            " sector_name, category_name, variety_gid, variety_name, botanical_name, alternative_name, limit_product, trade_orginatedby, " +
                            " SA_Brokerage, programlimit_validdfrom, programlimit_validdto, programoverall_limit, holding_periods, holdingmonthly_procurement, " +
                            " extendedholding_periods, extendedmonthly_procurement, charges_extendedperiod, customer_advance, reimburesementof_expenses, " +
                            " reimburesementof_expensespenalty, bankfundingdata_filename, bankfundingdata_filepath, needfor_stocking, product_portfolio, " +
                            " production_capacity, natureof_operations, averagemonthly_inventoryholding, financialinstitutions_relationship, " +
                            " utilized_limit, available_limit)" +
                            " select @application2loan_gid := '" + msGetGidapplication2loan + "', @application_gid:= '" + msGetGidApplication + "', " +
                            " facilityrequested_date, product_type, producttype_gid, productsub_type, productsubtype_gid, loantype_gid, loan_type, " +
                            " loanfacility_amount, rate_interest, penal_interest, tenureproduct_year, tenureproduct_month, tenureproduct_days, tenureoverall_limit, " +
                            " facility_type, facility_mode, created_by, created_date, scheme_type, principalfrequency_name, principalfrequency_gid, " +
                            " interestfrequency_name, interestfrequency_gid, program_gid, program, primaryvaluechain_gid, primaryvaluechain_name, " +
                            " secondaryvaluechain_gid, secondaryvaluechain_name, interest_status, moratorium_status, moratorium_type, " +
                            " moratorium_startdate, moratorium_enddate, source_type, guideline_value, guideline_date, marketvalue_date, market_value, " +
                            " forcedsource_value, collateralSSV_value, forcedvalueassessed_on, collateralobservation_summary, enduse_purpose, " +
                            " updated_by, updated_date, interchangeability, expiry_date, report_structure, document_limit, product_gid, product_name, " +
                            " sector_name, category_name, variety_gid, variety_name, botanical_name, alternative_name, limit_product, trade_orginatedby, " +
                            " SA_Brokerage, programlimit_validdfrom, programlimit_validdto, programoverall_limit, holding_periods, holdingmonthly_procurement, " +
                            " extendedholding_periods, extendedmonthly_procurement, charges_extendedperiod, customer_advance, reimburesementof_expenses, " +
                            " reimburesementof_expensespenalty, bankfundingdata_filename, bankfundingdata_filepath, needfor_stocking, product_portfolio, " +
                            " production_capacity, natureof_operations, averagemonthly_inventoryholding, financialinstitutions_relationship, " +
                            " utilized_limit, available_limit " +
                            " from agr_mst_tapplication2loan " +
                            " where application_gid='" + values.application_gid + "' " +
                            " and application2loan_gid='" + dt["application2loan_gid"].ToString() + "'";
                    mnResultapplication2loan = objdbconn.ExecuteNonQuerySQL(msSQL);

                    // Insert data in Payment Type Customer Table
                    msSQL = "select paymenttypecustomer_gid from agr_mst_tapploan2paymenttypecustomer where application2loan_gid='" + dt["application2loan_gid"].ToString() + "'";
                    dt_datatable2 = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable2.Rows.Count != 0)
                    {
                        foreach (DataRow dt2 in dt_datatable2.Rows)
                        {
                            msGetGidpaymenttypecustomer = objcmnfunctions.GetMasterGID("PTC");
                            msSQL = " insert into agr_mst_tapploan2paymenttypecustomer " +
                                    " (paymenttypecustomer_gid, application_gid, application2loan_gid, customerpaymenttype_gid, customerpaymenttype_name, " +
                                    " maximumpercent_paymentterm, created_by, created_date) " +
                                    " select @paymenttypecustomer_gid := '" + msGetGidpaymenttypecustomer + "', @application_gid:= '" + msGetGidApplication + "', " +
                                    " @application2loan_gid:= '" + msGetGidapplication2loan + "', " +
                                    " customerpaymenttype_gid, customerpaymenttype_name,maximumpercent_paymentterm, created_by, created_date " +
                                    " from agr_mst_tapploan2paymenttypecustomer " +
                                    " where application2loan_gid='" + dt["application2loan_gid"].ToString() + "' " +
                                    " and paymenttypecustomer_gid='" + dt2["paymenttypecustomer_gid"].ToString() + "'";
                            mnResultpaymenttypecustomer = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    else
                    {
                        mnResultpaymenttypecustomer = 1;
                    }

                    // Insert data in Application2Loan(Product Details) Table 
                    msSQL = "select application2product_gid from agr_mst_tapplication2product where application2loan_gid='" + dt["application2loan_gid"].ToString() + "'";
                    dt_datatable3 = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable3.Rows.Count != 0)
                    {
                        foreach (DataRow dt3 in dt_datatable3.Rows)
                        {
                            msGetGidapplication2product = objcmnfunctions.GetMasterGID("AP2P");
                            msSQL = " insert into agr_mst_tapplication2product " +
                                    " (application2product_gid, application_gid, application2loan_gid, product_gid, product_name, " +
                                    " variety_gid, variety_name, sector_name, category_name, botanical_name, alternative_name, created_by, " +
                                    " created_date, updated_by, updated_date, hsn_code, unitpricevalue_commodity, natureformstate_commoditygid, " +
                                    " natureformstate_commodity, qualityof_commodity, quantity, uom_gid, uom_name, milestone_applicability, " +
                                    " milestonepayment_gid, milestonepayment_name, sa_payout, insurance_availability, insurance_percent, insurance_cost, " +
                                    " net_yield, markto_marketvalue, pricereference_source, headingdesc_product, typeofsupply_naturegid, " +
                                    " typeofsupply_naturename, sectorclassification_gid, sectorclassification_name, creditperiod_years, creditperiod_months, " +
                                    " creditperiod_days, overallcreditperiod_limit, commodity_margin, commoditynet_yield, graceperiod_days, " +
                                    " customerpaymenttype_gid, customerpaymenttype_name, maximumpercent_paymentterm) " +
                                    " select @application2product_gid := '" + msGetGidapplication2product + "',@application_gid:= '" + msGetGidApplication + "', " +
                                    " @application2loan_gid:= '" + msGetGidapplication2loan + "', " +
                                    " product_gid, product_name, " +
                                    " variety_gid, variety_name, sector_name, category_name, botanical_name, alternative_name, created_by, " +
                                    " created_date, updated_by, updated_date, hsn_code, unitpricevalue_commodity, natureformstate_commoditygid, " +
                                    " natureformstate_commodity, qualityof_commodity, quantity, uom_gid, uom_name, milestone_applicability, " +
                                    " milestonepayment_gid, milestonepayment_name, sa_payout, insurance_availability, insurance_percent, insurance_cost, " +
                                    " net_yield, markto_marketvalue, pricereference_source, headingdesc_product, typeofsupply_naturegid, " +
                                    " typeofsupply_naturename, sectorclassification_gid, sectorclassification_name, creditperiod_years, creditperiod_months, " +
                                    " creditperiod_days, overallcreditperiod_limit, commodity_margin, commoditynet_yield, graceperiod_days , " +
                                    "  @customerpaymenttype_gid:= '"+ msGetGidpaymenttypecustomer +"', customerpaymenttype_name, maximumpercent_paymentterm " +
                                    " from agr_mst_tapplication2product " +
                                    " where application2loan_gid='" + dt["application2loan_gid"].ToString() + "' " +
                                    " and application2product_gid='" + dt3["application2product_gid"].ToString() + "'";
                            mnResultapplication2product = objdbconn.ExecuteNonQuerySQL(msSQL);

                            // Insert data in product2commodity gst status Table
                            msSQL = "select appproduct2commoditygststatus_gid from agr_mst_tappproduct2commoditygststatus where application2product_gid='" + dt3["application2product_gid"].ToString() + "'";
                            dt_datatable4 = objdbconn.GetDataTable(msSQL);
                            if (dt_datatable4.Rows.Count != 0)
                            {
                                foreach (DataRow dt4 in dt_datatable4.Rows)
                                {
                                    msGetGidproduct2gststatus = objcmnfunctions.GetMasterGID("APGT");
                                    msSQL = " insert into agr_mst_tappproduct2commoditygststatus " +
                                            " (appproduct2commoditygststatus_gid, commoditygststatus_gid, application2product_gid, product_gid, " +
                                            " variety_gid, IGST_percent, SGST_percent, CGST_percent, CESS_percent, wef_date, created_by, " +
                                            " created_date) " +
                                            " select @appproduct2commoditygststatus_gid := '" + msGetGidproduct2gststatus + "',commoditygststatus_gid," +
                                            " @application2product_gid:= '" + msGetGidapplication2product + "', " +
                                            "  product_gid, " +
                                            " variety_gid, IGST_percent, SGST_percent, CGST_percent, CESS_percent, wef_date, created_by, created_date " +
                                            " from agr_mst_tappproduct2commoditygststatus " +
                                            " where application2product_gid= '" + dt3["application2product_gid"].ToString() + "'" +
                                            " and appproduct2commoditygststatus_gid='" + dt4["appproduct2commoditygststatus_gid"].ToString() + "'";
                                    mnResultproduct2gststatus = objdbconn.ExecuteNonQuerySQL(msSQL);
                                }
                            }
                            else
                            {
                                mnResultproduct2gststatus = 1;
                            }
                            // Insert data in product2commodity trade detail Table
                            msSQL = "select appproduct2commoditytrade_gid from agr_mst_tappproduct2commoditytradedtl where application2product_gid='" + dt3["application2product_gid"].ToString() + "'";
                            dt_datatable5 = objdbconn.GetDataTable(msSQL);
                            if (dt_datatable5.Rows.Count != 0)
                            {
                                foreach (DataRow dt5 in dt_datatable5.Rows)
                                {
                                    msGetGidproduct2commoditytrade = objcmnfunctions.GetMasterGID("ACTP");
                                    msSQL = " insert into agr_mst_tappproduct2commoditytradedtl " +
                                            " (appproduct2commoditytrade_gid, commoditytradeproductdtl_gid, application2product_gid, mstproduct_gid, " +
                                            " variety_gid, product_gid, product_name, subproduct_gid, subproduct_name, insurancecompany_gid, " +
                                            " insurancecompany_name, insurancepolicy_gid, insurancepolicy_name, created_by, created_date) " +
                                            " select @appproduct2commoditytrade_gid := '" + msGetGidproduct2commoditytrade + "',commoditytradeproductdtl_gid, @application2product_gid:= '" + msGetGidapplication2product + "', " +
                                            " mstproduct_gid,variety_gid, product_gid, product_name, subproduct_gid, subproduct_name, insurancecompany_gid, " +
                                            " insurancecompany_name, insurancepolicy_gid, insurancepolicy_name, created_by, created_date " +
                                            " from agr_mst_tappproduct2commoditytradedtl " +
                                            " where application2product_gid= '" + dt3["application2product_gid"].ToString() + "'" +
                                            " and appproduct2commoditytrade_gid='" + dt5["appproduct2commoditytrade_gid"].ToString() + "'";
                                    mnResultproduct2commoditytrade = objdbconn.ExecuteNonQuerySQL(msSQL);
                                }
                            }
                            else
                            {
                                mnResultproduct2commoditytrade = 1;
                            }
                            // Insert data in product2commodity document detail Table
                            msSQL = "select appproduct2commoditydocument_gid from agr_mst_tappproduct2commoditydocument where application2product_gid='" + dt3["application2product_gid"].ToString() + "'";
                            dt_datatable6 = objdbconn.GetDataTable(msSQL);
                            if (dt_datatable6.Rows.Count != 0)
                            {
                                foreach (DataRow dt6 in dt_datatable6.Rows)
                                {
                                    msGetGidproduct2commoditydoc = objcmnfunctions.GetMasterGID("APCD");
                                    msSQL = " insert into agr_mst_tappproduct2commoditydocument " +
                                            " (appproduct2commoditydocument_gid, commoditydocument_gid, application2product_gid, ason_date, commodityreport_filename, " +
                                            " commodityreport_filepath, riskanalysisreport_filename, riskanalysisreport_filepath, created_by, created_date, " +
                                            " variety_gid) " +
                                            " select @appproduct2commoditydocument_gid := '" + msGetGidproduct2commoditydoc + "',  commoditydocument_gid, @application2product_gid:= '" + msGetGidapplication2product + "', " +
                                            " ason_date, commodityreport_filename, " +
                                            " commodityreport_filepath, riskanalysisreport_filename, riskanalysisreport_filepath, created_by, created_date, " +
                                            " variety_gid " +
                                            " from agr_mst_tappproduct2commoditydocument " +
                                            " where application2product_gid= '" + dt3["application2product_gid"].ToString() + "'" +
                                            " and appproduct2commoditydocument_gid='" + dt6["appproduct2commoditydocument_gid"].ToString() + "'";
                                    mnResultproduct2commoditydoc = objdbconn.ExecuteNonQuerySQL(msSQL);
                                }
                            }
                            else
                            {
                                mnResultproduct2commoditydoc = 1;
                            }
                        }
                    }
                    else
                    {
                        mnResultapplication2product = 1;
                        mnResultproduct2gststatus = 1;
                        mnResultproduct2commoditytrade = 1;
                        mnResultproduct2commoditydoc = 1;
                    }
                    // Insert data in application loan2supplier details Table
                    msSQL = "select apploan2supplierdtl_gid from agr_mst_tapploan2supplierdtl where application2loan_gid='" + dt["application2loan_gid"].ToString() + "'";
                    dt_datatable7 = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable7.Rows.Count != 0)
                    {
                        foreach (DataRow dt7 in dt_datatable7.Rows)
                        {
                            msGetGidloan2supplierdtl = objcmnfunctions.GetMasterGID("ALSD");
                            msSQL = " insert into agr_mst_tapploan2supplierdtl " +
                                    " (apploan2supplierdtl_gid, application_gid, application2loan_gid, supplier_gid, supplier_name, " +
                                    " supplier_address, supplier_emailid, supplier_phoneno, supplier_gstno, supplier_pandtl, milestone_applicable, " +
                                    " milestonepaymenttype_gid, milestonepaymenttype_name, supplier_vintage, created_by, created_date, supplier_margin) " +
                                    " select @apploan2supplierdtl_gid := '" + msGetGidloan2supplierdtl + "', @application_gid:= '" + msGetGidApplication + "', " +
                                    " @application2loan_gid:= '" + msGetGidapplication2loan + "', " +
                                    " supplier_gid, supplier_name, supplier_address, supplier_emailid, supplier_phoneno, supplier_gstno, " +
                                    " supplier_pandtl, milestone_applicable, milestonepaymenttype_gid, milestonepaymenttype_name, supplier_vintage, " +
                                    " created_by, created_date, supplier_margin " +
                                    " from agr_mst_tapploan2supplierdtl " +
                                    " where application2loan_gid='" + dt["application2loan_gid"].ToString() + "' " +
                                    " and apploan2supplierdtl_gid='" + dt7["apploan2supplierdtl_gid"].ToString() + "'";
                            mnResultloan2supplierdtl = objdbconn.ExecuteNonQuerySQL(msSQL);

                            // Insert data in application2supplier gst details Table
                            msSQL = "select app2suppliergstdtl_gid from agr_mst_tapp2suppliergstdtl where apploan2supplierdtl_gid='" + dt7["apploan2supplierdtl_gid"].ToString() + "'";
                            dt_datatable8 = objdbconn.GetDataTable(msSQL);
                            if (dt_datatable8.Rows.Count != 0)
                            {
                                foreach (DataRow dt8 in dt_datatable8.Rows)
                                {
                                    msGetGidapp2suppliergstdtl = objcmnfunctions.GetMasterGID("A2SG");
                                    msSQL = " insert into agr_mst_tapp2suppliergstdtl " +
                                            " (app2suppliergstdtl_gid, apploan2supplierdtl_gid, institution2branch_gid, gst_state, gst_no, created_by, created_date) " +
                                            " select @app2suppliergstdtl_gid := '" + msGetGidapp2suppliergstdtl + "', @apploan2supplierdtl_gid:= '" + msGetGidloan2supplierdtl + "', " +
                                            " institution2branch_gid, gst_state, gst_no, created_by, created_date " +
                                            " from agr_mst_tapp2suppliergstdtl " +
                                            " where apploan2supplierdtl_gid='" + dt7["apploan2supplierdtl_gid"].ToString() + "' " +
                                            " and app2suppliergstdtl_gid='" + dt8["app2suppliergstdtl_gid"].ToString() + "'";
                                    mnResultapp2suppliergstdtl = objdbconn.ExecuteNonQuerySQL(msSQL);
                                }
                            }
                            else
                            {
                                mnResultapp2suppliergstdtl = 1;
                            }
                        }
                    }
                    else
                    {
                        mnResultloan2supplierdtl = 1;
                        mnResultapp2suppliergstdtl = 1;
                    }
                    // Insert data in application loan2supplier payment Table
                    msSQL = "select apploan2supplierpayment_gid from agr_mst_tapploan2supplierpayment where application2loan_gid='" + dt["application2loan_gid"].ToString() + "'";
                    dt_datatable9 = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable9.Rows.Count != 0)
                    {
                        foreach (DataRow dt9 in dt_datatable9.Rows)
                        {
                            msGetGidloan2supplierpayment = objcmnfunctions.GetMasterGID("ALSD");
                            msSQL = " insert into agr_mst_tapploan2supplierpayment " +
                                    " (apploan2supplierpayment_gid, application_gid, application2loan_gid, commodity_gid, commodity_name," +
                                    " supplierpayment_type, supplierpayment_typegid, maxpercent_paymentterm, created_by, created_date,  apploan2supplierdtl_gid) " +
                                    " select @apploan2supplierpayment_gid := '" + msGetGidloan2supplierpayment + "', @application_gid:= '" + msGetGidApplication + "', " +
                                    " @application2loan_gid:= '" + msGetGidapplication2loan + "', " +
                                    " commodity_gid, commodity_name,supplierpayment_type, supplierpayment_typegid, " +
                                    " maxpercent_paymentterm, created_by, created_date ,  @apploan2supplierdtl_gid:= '" + msGetGidloan2supplierdtl + "' " +
                                    " from agr_mst_tapploan2supplierpayment " +
                                    " where application2loan_gid='" + dt["application2loan_gid"].ToString() + "' " +
                                    " and apploan2supplierpayment_gid='" + dt9["apploan2supplierpayment_gid"].ToString() + "'";
                            mnResultloan2supplierpayment = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    else
                    {
                        mnResultloan2supplierpayment = 1;
                    }
                    // Insert data in application2buyer Table
                    msSQL = "select application2buyer_gid from agr_mst_tapplication2buyer where application2loan_gid='" + dt["application2loan_gid"].ToString() + "'";
                    dt_datatable10 = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable10.Rows.Count != 0)
                    {
                        foreach (DataRow dt10 in dt_datatable10.Rows)
                        {
                            msGetGidapplication2buyer = objcmnfunctions.GetMasterGID("AP2B");
                            msSQL = " insert into agr_mst_tapplication2buyer " +
                                    " (application2buyer_gid, application2loan_gid, buyer_gid, buyer_name, buyer_limit, " +
                                    " availed_limit, balance_limit, bill_tenure, margin, created_by, created_date, updated_by, " +
                                    " updated_date) " +
                                    " select @application2buyer_gid := '" + msGetGidapplication2buyer + "'," +
                                    " @application2loan_gid:= '" + msGetGidapplication2loan + "', " +
                                    "  buyer_gid, buyer_name, buyer_limit,  " +
                                    " availed_limit, balance_limit, bill_tenure, margin, created_by, created_date, updated_by, " +
                                    " updated_date " +
                                    " from agr_mst_tapplication2buyer " +
                                    " where application2loan_gid='" + dt["application2loan_gid"].ToString() + "' " +
                                    " and application2buyer_gid='" + dt10["application2buyer_gid"].ToString() + "'";
                            mnResultapplication2buyer = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    else
                    {
                        mnResultapplication2buyer = 1;
                    }
                    // Insert data in application Service Charges Details Table
                    msSQL = "select application2servicecharge_gid from agr_mst_tapplicationservicecharge where application2loan_gid='" + dt["application2loan_gid"].ToString() + "'";
                    dt_datatable11 = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable11.Rows.Count != 0)
                    {
                        foreach (DataRow dt11 in dt_datatable11.Rows)
                        {
                            msGetGidappl2servicecharge = objcmnfunctions.GetMasterGID("AP2C");
                            msSQL = " insert into agr_mst_tapplicationservicecharge " +
                                    " (application2servicecharge_gid, application_gid, application2loan_gid, processing_fee, processing_collectiontype," +
                                    " doc_charges, doccharge_collectiontype, fieldvisit_charges, fieldvisit_charges_collectiontype, adhoc_fee, " +
                                    " adhoc_collectiontype, life_insurance, lifeinsurance_collectiontype, acct_insurance, total_collect, " +
                                    " total_deduct, product_type, producttype_gid, created_by, created_date, updated_date, updated_by, " +
                                    " acctinsurance_collectiontype) " +
                                    " select @application2servicecharge_gid := '" + msGetGidappl2servicecharge + "',@application_gid:= '" + msGetGidApplication + "'," +
                                    " @application2loan_gid:= '" + msGetGidapplication2loan + "', processing_fee, processing_collectiontype," +
                                     " doc_charges, doccharge_collectiontype, fieldvisit_charges, fieldvisit_charges_collectiontype, adhoc_fee, " +
                                    " adhoc_collectiontype, life_insurance, lifeinsurance_collectiontype, acct_insurance, total_collect, " +
                                    " total_deduct, product_type, producttype_gid, created_by, created_date, updated_date, updated_by, " +
                                    " acctinsurance_collectiontype " +
                                    " from agr_mst_tapplicationservicecharge " +
                                    " where application2loan_gid='" + dt["application2loan_gid"].ToString() + "' " +
                                    " and application2servicecharge_gid='" + dt11["application2servicecharge_gid"].ToString() + "'";
                            mnResultappl2servicecharge = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    else
                    {
                        mnResultappl2servicecharge = 1;
                    }
                    // Insert data in application trade2warehouse Details Table
                    msSQL = "select applicationtrade2warehouse_gid from agr_mst_tapplicationtrade2warehouse where application2loan_gid='" + dt["application2loan_gid"].ToString() + "'";
                    dt_datatable12 = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable12.Rows.Count != 0)
                    {
                        foreach (DataRow dt12 in dt_datatable12.Rows)
                        {
                            msGetGidappltrade2warehouse = objcmnfunctions.GetMasterGID("AT2W");
                            msSQL = " insert into agr_mst_tapplicationtrade2warehouse " +
                                    " (applicationtrade2warehouse_gid, application2trade_gid, application2loan_gid, application_gid, creditor_gid," +
                                    " warehouse_gid, warehouse_agency, warehouse_name, typeofwarehouse_name, volume_uomgid, volume_uom, " +
                                    " totalcapacity_volume, totalcapacity_area, totalcapacityarea_uomgid, area_uom, warehouse2address_gid, " +
                                    " warehouse_address, capacity_commodity, capacity_panina, created_by, created_date, updated_by, updated_date) " +
                                    " select @applicationtrade2warehouse_gid := '" + msGetGidappltrade2warehouse + "',@application_gid:= '" + msGetGidApplication + "'," +
                                    " @application2loan_gid:= '" + msGetGidapplication2loan + "', application2trade_gid,creditor_gid," +
                                    " warehouse_gid, warehouse_agency, warehouse_name, typeofwarehouse_name, volume_uomgid, volume_uom, " +
                                    " totalcapacity_volume, totalcapacity_area, totalcapacityarea_uomgid, area_uom, warehouse2address_gid, " +
                                    " warehouse_address, capacity_commodity, capacity_panina, created_by, created_date, updated_by, updated_date " +
                                    " from agr_mst_tapplicationtrade2warehouse " +
                                    " where application2loan_gid='" + dt["application2loan_gid"].ToString() + "' " +
                                    " and applicationtrade2warehouse_gid='" + dt12["applicationtrade2warehouse_gid"].ToString() + "'";
                            mnResultappltrade2warehouse = objdbconn.ExecuteNonQuerySQL(msSQL);

                            // Insert data in application2trade Details Table
                            msSQL = "select application2trade_gid from agr_mst_tapplication2trade where application2loan_gid='" + dt["application2loan_gid"].ToString() + "'";
                            dt_datatable13 = objdbconn.GetDataTable(msSQL);
                            if (dt_datatable13.Rows.Count != 0)
                            {
                                foreach (DataRow dt13 in dt_datatable13.Rows)
                                {
                                    msGetGidapplication2trade = objcmnfunctions.GetMasterGID("APTR");
                                    msSQL = " insert into agr_mst_tapplication2trade " +
                                            " (application2trade_gid, application_gid, producttype_gid, producttype_name, productsubtype_gid, " +
                                            " productsubtype_name, salescontract_availability, scopeof_transportgid, scopeof_transport, " +
                                            " scopeof_loadinggid, scopeof_loading, scopeof_unloadinggid, scopeof_unloading, " +
                                            " scopeof_qualityandquantitygid, scopeof_qualityandquantity, scopeof_moisturegainlossgid, " +
                                            " scopeof_moisturegainloss, created_by, created_date, updated_by, updated_date, application2loan_gid, " +
                                            " scopeof_insurancegid, scopeof_insurance) " +
                                            " select @application2trade_gid := '" + msGetGidapplication2trade + "',@application_gid:= '" + msGetGidApplication + "'," +
                                            " producttype_gid, producttype_name, productsubtype_gid, " +
                                            " productsubtype_name, salescontract_availability, scopeof_transportgid, scopeof_transport, " +
                                            " scopeof_loadinggid, scopeof_loading, scopeof_unloadinggid, scopeof_unloading, " +
                                            " scopeof_qualityandquantitygid, scopeof_qualityandquantity, scopeof_moisturegainlossgid, " +
                                            " scopeof_moisturegainloss, created_by, created_date, updated_by, updated_date, " +
                                            " @application2loan_gid:= '" + msGetGidapplication2loan + "', " +
                                            " scopeof_insurancegid, scopeof_insurance " +
                                            " from agr_mst_tapplication2trade " +
                                            " where application2loan_gid='" + dt["application2loan_gid"].ToString() + "' " +
                                            " and application2trade_gid='" + dt13["application2trade_gid"].ToString() + "'";
                                    mnResultapplication2trade = objdbconn.ExecuteNonQuerySQL(msSQL);
                                }
                            }
                            else
                            {
                                mnResultapplication2trade = 1;
                            }
                        }
                    }
                    else
                    {
                        mnResultappltrade2warehouse = 1;
                        mnResultapplication2trade = 1;
                    }

                }
            }

            else
            {
                mnResultapplication2loan = 1;
                mnResultpaymenttypecustomer = 1;
                mnResultapplication2product = 1;
                mnResultproduct2gststatus = 1;
                mnResultproduct2commoditytrade = 1;
                mnResultproduct2commoditydoc = 1;
                mnResultloan2supplierdtl = 1;
                mnResultapp2suppliergstdtl = 1;
                mnResultloan2supplierpayment = 1;
                mnResultapplication2buyer = 1;
                mnResultappl2servicecharge = 1;
                mnResultappltrade2warehouse = 1;
                mnResultapplication2trade = 1;
            }

            if (mnResultapplicationcreation != 0 && mnResultapplicationcontact != 0 && mnResultapplicationgeneticcode != 0 && mnResultapplicationemail != 0 && mnResultinstitution != 0 && mnResultinstitutionbranch != 0 && mnResultinstitutionmobile != 0 && mnResultinstitutionemail != 0 && mnResultinstitutionaddress != 0 && mnResultinstitutionlicense != 0 && mnResultinstitutiondocument != 0 && mnResultinstitutionratingdtl != 0 && mnResultinstitutionbankdtl != 0 && mnResultcontact != 0 && mnResultcontactpanabsencereason != 0 && mnResultcontactpanform60 != 0 && mnResultcontactmobile != 0 && mnResultcontactemail != 0 && mnResultcontactidproof != 0 && mnResultcontactaddress != 0 && mnResultcontactdocument != 0 && mnResultapplication2loan != 0 && mnResultpaymenttypecustomer != 0 && mnResultapplication2product != 0 && mnResultproduct2gststatus != 0 && mnResultproduct2commoditytrade != 0 && mnResultproduct2commoditydoc != 0 && mnResultloan2supplierdtl != 0 && mnResultapp2suppliergstdtl != 0 && mnResultloan2supplierpayment != 0 && mnResultapplication2buyer != 0 && mnResultappl2servicecharge != 0 && mnResultappltrade2warehouse != 0 && mnResultapplication2trade != 0 && mnResultonboardinitiate != 0)
            {

                msSQL = "INSERT INTO agr_mst_tonboardclonelog(" +
                       " onboard_gid," +
                       " existingapplication_gid," +
                       " cloneapplication_gid, " +
                       " clone_status, " +
                       " existing_overallvaliditydate, " +
                       " existing_overallcalculation, " +
                       " created_by," +
                       " created_date)" +
                       " VALUES(" +
                       "'" + values.buyeronboard_gid + "'," +
                       "'" + values.application_gid + "'," +
                       "'" + msGetGidApplication + "'," +
                       "'Short Closing'," +
                       "'" + Convert.ToDateTime(values.validityto_date).ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                       "'" + values.calculationoveralllimit_validity + "'," +
                       "'" + employee_gid + "'," +
                       "current_timestamp)";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.application_no = lsapp_refno;
                values.message = "Application Short Closed Successfully";
                return true;
            }
            else
            {
                // Delete Tables

                msSQL = " select GROUP_CONCAT(institution_gid) as totalinstitution_gid " +
                        " from agr_mst_tinstitution where application_gid='" + msGetGidApplication + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.totalinstitution_gid = objODBCDatareader["totalinstitution_gid"].ToString();
                }
                msSQL = " delete from agr_mst_tinstitution2branch where institution_gid in ('" + values.totalinstitution_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tinstitution2mobileno where institution_gid in ('" + values.totalinstitution_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tinstitution2email where institution_gid in ('" + values.totalinstitution_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tinstitution2email where institution_gid in ('" + values.totalinstitution_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tinstitution2licensedtl where institution_gid in ('" + values.totalinstitution_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tinstitution2documentupload where institution_gid in ('" + values.totalinstitution_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tinstitution2ratingdetail where institution_gid in ('" + values.totalinstitution_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tinstitution2bankdtl here institution_gid in ('" + values.totalinstitution_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_trn_tdocumentchecktls where credit_gid in ('" + values.totalinstitution_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_trn_tcovanantdocumentcheckdtls where credit_gid in ('" + values.totalinstitution_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " select GROUP_CONCAT(contact_gid) as totalcontact_gid " +
                        " from agr_mst_tcontact where application_gid='" + msGetGidApplication + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.totalcontact_gid = objODBCDatareader["totalcontact_gid"].ToString();
                }
                msSQL = " delete from agr_mst_tcontact2panabsencereason where contact_gid in ('" + values.totalcontact_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tcontact2panform60 where contact_gid in ('" + values.totalcontact_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tcontact2mobileno where contact_gid in ('" + values.totalcontact_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tcontact2email where contact_gid in ('" + values.totalcontact_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tcontact2idproof where contact_gid in ('" + values.totalcontact_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tcontact2address where contact_gid in ('" + values.totalcontact_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tcontact2document where contact_gid in ('" + values.totalcontact_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_trn_tdocumentchecktls where credit_gid in ('" + values.totalcontact_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_trn_tcovanantdocumentcheckdtls where credit_gid in ('" + values.totalcontact_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " select GROUP_CONCAT(application2product_gid) as totalapplication2product_gid " +
                        " from agr_mst_tapplication2product where application_gid='" + msGetGidApplication + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.totalapplication2product_gid = objODBCDatareader["totalapplication2product_gid"].ToString();
                }
                msSQL = " delete from agr_mst_tappproduct2commoditygststatus where application2product_gid in ('" + values.totalapplication2product_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tappproduct2commoditytradedtl where application2product_gid in ('" + values.totalapplication2product_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tappproduct2commoditydocument where application2product_gid in ('" + values.totalapplication2product_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " select GROUP_CONCAT(apploan2supplierdtl_gid) as totalapploan2supplierdtl_gid " +
                        " from agr_mst_tapploan2supplierdtl where application_gid='" + msGetGidApplication + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.totalapploan2supplierdtl_gid = objODBCDatareader["totalapploan2supplierdtl_gid"].ToString();
                }
                msSQL = " delete from agr_mst_tapp2suppliergstdtl where apploan2supplierdtl_gid in ('" + values.totalapploan2supplierdtl_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " select GROUP_CONCAT(application2loan_gid) as totalapplication2loan_gid " +
                       " from agr_mst_tapplication2loan where application_gid='" + msGetGidApplication + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.totalapplication2loan_gid = objODBCDatareader["totalapplication2loan_gid"].ToString();
                }
                msSQL = " delete from agr_mst_tapplication2buyer where application2loan_gid in ('" + values.totalapplication2loan_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tapplication where application_gid='" + msGetGidApplication + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tapplication2contactno where application_gid='" + msGetGidApplication + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tapplication2email where application_gid='" + msGetGidApplication + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tapplication2geneticcode where application_gid='" + msGetGidApplication + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tinstitution where application_gid='" + msGetGidApplication + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tcontact where application_gid='" + msGetGidApplication + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tapplication2loan where application_gid='" + msGetGidApplication + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tapploan2paymenttypecustomer where application_gid='" + msGetGidApplication + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tapplication2product where application_gid='" + msGetGidApplication + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tapploan2supplierdtl where application_gid='" + msGetGidApplication + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tapploan2supplierpayment where application_gid='" + msGetGidApplication + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tapplicationservicecharge where application_gid='" + msGetGidApplication + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tapplicationtrade2warehouse where application_gid='" + msGetGidApplication + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tapplication2trade where application_gid='" + msGetGidApplication + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from agr_mst_tonboardinitiatedtl where application_gid='" + msGetGidApplication + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = false;
                values.message = "Error Occurred while Renew the Application";
                return false;
            }

        }
    }
}