using ems.mastersamagro.Models;
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
using OfficeOpenXml.FormulaParsing.Excel.Functions.Logical;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;

namespace ems.mastersamagro.DataAccess
{
    /// <summary>
    /// This DataAccess will help to create contracts for pmg accepted records
    /// </summary>
    /// <remarks>Written by Venkatesh </remarks>
    public class DaAgrTrnContract
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        //DaSendBackMailTrigger objvalues = new DaSendBackMailTrigger();
        DataTable dt_datatable, dt_child, dt_childindividual, dt_childgroup, dt_datatable2;
        string msSQL, msGetGid, msGetGid1, msGetGidCC, msGetGid2, msGetGid3, lscadapplication_gid, msGetgroupDocchecklistGID, msGetCovgroupDocchecklistGID;
        int mnResult, mnResult1, mnResult2, mnResultCAD, mnResultuploadesdeclarationdocumentlog, mnResultlimitproductinfolog, mnResultdeviationmaildocumentlog, mnResultapplication2sanctiondoclog;
        HttpPostedFile httpPostedFile;
        OdbcDataReader objODBCDataReader, objODBCDataReader1, objODBCDataReader2, objODBCDatareader;
        string lssanctionref_no, lstemplate_content, lscompany_code, lspath, lsdocument_path, fileName, edit_flag, lscontract_date, tmp_documentGid;
        string msGetRef, msGetGID, lsdocument_code, lsdocument_name, lsdocumenttype_name, lscompanydocument_name, lsindividualdocument_name, lsgroupdocument_name, lsdocumenttype_gid, lsapplicationvisit_gid;
        string lscontent = string.Empty;
        string application2sanction_gid, sanction_refno, sanction_date, sanction_amount, entity, paycard, branch_gid, branch_name, contract_date, validityto_date, validityfrom_date,
                      entity_gid, application_gid, ccapproved_date, applicationtype_gid, application_type, esdeclaration_status,
                      sanctionto_gid, sanctionto_name, sanctionfrom_date, sanctiontill_date, contactpersonaddress_gid,
                      contactperson_address, contactperson_name, contactperson_number, contactpersonmobileno_gid,
                      contactpersonemail_gid, contactpersonemail_address, sanction_type, natureof_proposal, created_by, created_date,
                      makerfile_path, makerfile_name, sanctionletter_status, template_content, makersubmitted_by, makersubmitted_on, application2sanctionlog_gid,
                     sanctiongenerated_by, sanctiongenerated_on, template_name, checkerapproved_by, checkerupdated_on, checkerpushback_remarks, checkerapproval_flag, checkerapproved_on, digitalsignature_flag, contractlog_gid;
        string interchangeability, report_structure_gid, report_structure, odlim_condition, contract_id,
            documented_limit, dateof_Expiry, updated_by, updated_date;

        //Maker Summary
        public void DaGetContractMakerSummary(string employee_gid, MdlMstCAD values)
        {
            msSQL = " select a.application_gid,a.application_no,a.customerref_name,a.customer_urn,a.creditgroup_name,d.sanction_refno," +
                     " a.customer_name as customer_name,date_format(a.ccsubmitted_date, '%d-%m-%Y %h:%i %p') as ccapproved_date, a.approval_status," +
                     " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as cadaccepted_by, " +
                     " date_format(a.processupdated_date, '%d-%m-%Y %h:%i %p') as cadaccepted_date, a.created_by, d.sanction_status, " +
                     " a.creditgroup_gid,e.cadgroup_name,a.customer_urn,a.renewal_flag,a.amendment_flag,a.shortclosing_flag from agr_mst_tapplication a " +
                     " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by " +
                     " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                     " left join agr_trn_tapplication2contract d on d.application_gid = a.application_gid " +
                      " left join agr_trn_tprocesstype_assign e on e.application_gid = a.application_gid " +
                     " where a.process_type = 'Accept' and " +
                     " a.application_gid in (Select application_gid from agr_trn_tprocesstype_assign where menu_gid = '" + getMenuClass.Contract + "' " +
                     " and maker_gid = '" + employee_gid + "' and maker_approvalflag ='N')" +
                     " group by a.application_gid order by a.updated_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<cadapplicationlist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    string lsccgroup_name;
                    string lsccadmin_name;
                    string lscadgroup_name;

                    msSQL = "select group_concat(ccgroup_name) as ccgroup_name from agr_mst_tapplication where application_gid='" + dt["application_gid"] + "'";
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
                    msSQL = "select group_concat(ccadmin_name) as ccadmin_name from agr_mst_tccschedulemeeting where application_gid='" + dt["application_gid"] + "'";
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
                        cadgroupname = dt["cadgroup_name"].ToString(),
                        cadaccepted_by = dt["cadaccepted_by"].ToString(),
                        cadaccepted_date = dt["cadaccepted_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        sanction_status = dt["sanction_status"].ToString(),
                        sanction_refno = dt["sanction_refno"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        renewal_flag = dt["renewal_flag"].ToString(),
                        amendment_flag = dt["amendment_flag"].ToString(),
                        shortclosing_flag = dt["shortclosing_flag"].ToString()
                    });

                }
            }
            values.cadapplicationlist = getapplicationadd_list;
            dt_datatable.Dispose();
        }

        //Checker Summary
        

        public bool DaContractToCheckerSummary(sanctiondetailsList values, string employee_gid)
        {
            msSQL = " SELECT a.application2sanction_gid, a.sanction_refno,date_format(a.sanction_date,'%d-%m-%Y') as sanction_date, " +
                   " b.application_no, b.approval_status,a.sanction_refno,b.customer_urn," +
                   " format((sanction_amount),2) as sanction_amount,a.sanction_limit,b.customer_name,sanctionto_name, " +
                   " sanction_status, b.application_gid, " +
                   " concat(d.user_code,' / ',d.user_firstname,d.user_lastname) as makersubmitted_by, " +
                   " date_format(a.makersubmitted_on,'%d-%m-%Y %h:%i %p') as makersubmitted_on," +
                   " date_format(b.ccsubmitted_date,'%d-%m-%Y %h:%i %p') as ccapproved_date," +
                   " reset_flag, e.cadgroup_name,b.renewal_flag,b.amendment_flag,b.shortclosing_flag FROM agr_trn_tapplication2contract a " +
                   " LEFT JOIN agr_mst_tapplication b ON a.application_gid = b.application_gid" +
                   " LEFT JOIN hrm_mst_temployee c ON a.makersubmitted_by=c.employee_gid" +
                   " LEFT JOIN adm_mst_tuser d ON c.user_gid=d.user_gid " +
                   " left join agr_trn_tprocesstype_assign e on e.application_gid = a.application_gid " +
                   " where (a.sanctionletter_flag='Y' or sanction_status='Pushback') and a.checkerletter_flag='N' and e.menu_gid = 'AGDMGTCON' " +
                   " and e.checker_gid = '" + employee_gid + "' and e.checker_approvalflag='N' " +
                   " group by a.application_gid  ORDER BY application2sanction_gid DESC ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_sanctiondtl = new List<sanctiondetails>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    string lsccgroup_name;
                    string lscadgroup_name;

                    msSQL = "select group_concat(ccgroup_name) as ccgroup_name from agr_mst_tapplication where application_gid='" + dt["application_gid"] + "'";
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
                    //msSQL = "select group_concat(cadgroup_name) as cadgroup_name from agr_trn_tprocesstype_assign where application_gid='" + dt["application_gid"] + "'";
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
                        ccgroup_name = lsccgroup_name,
                        cadgroupname = dt["cadgroup_name"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                          renewal_flag = dt["renewal_flag"].ToString(),
                        amendment_flag = dt["amendment_flag"].ToString(),
                        shortclosing_flag = dt["shortclosing_flag"].ToString()

                    });
                }
                values.sanctiondetails = get_sanctiondtl;
            }
            dt_datatable.Dispose();

            return true;
        }

        //Approval Summary     

        public bool DaContractApprovalSummary(sanctiondetailsList values, string employee_gid)
        {
            msSQL = " SELECT a.application2sanction_gid,a.sanction_refno,date_format(a.sanction_date,'%d-%m-%Y') as sanction_date, sanction_status, b.application_gid, " +
                   " date_format(b.ccsubmitted_date, '%d-%m-%Y %h:%i %p') as ccapproved_date,format((sanction_amount),2) as sanction_amount,b.customer_name,checkerapproval_flag, a.checkerreject_remarks, b.approval_status," +
                   " b.application_no, concat(d.user_code,' / ',d.user_firstname,d.user_lastname) as checkerupdated_by, " +
                   " date_format(a.checkerupdated_on,'%d-%m-%Y %h:%i %p') as checkerupdated_on, sanctionto_name, e.cadgroup_name," +
                   " a.sanction_refno,b.customer_urn,b.renewal_flag,b.amendment_flag,b.shortclosing_flag FROM agr_trn_tapplication2contract a " +
                   " LEFT JOIN agr_mst_tapplication  b ON a.application_gid = b.application_gid" +
                   " LEFT JOIN hrm_mst_temployee c ON a.checkerupdated_by=c.employee_gid" +
                   " LEFT JOIN adm_mst_tuser d ON c.user_gid=d.user_gid " +
                   " left join agr_trn_tprocesstype_assign e on e.application_gid = a.application_gid " +
                   " where a.checkerapproval_flag='N' and e.menu_gid = 'AGDMGTCON' " +
                   " and e.approver_gid = '" + employee_gid + "' and e.approver_approvalflag='N' ORDER BY application2sanction_gid DESC ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_sanctiondtl = new List<sanctiondetails>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    string lsccgroup_name;
                    string lscadgroup_name;

                    msSQL = "select group_concat(ccgroup_name) as ccgroup_name from agr_mst_tapplication where application_gid='" + dt["application_gid"] + "'";
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
                    //msSQL = "select group_concat(cadgroup_name) as cadgroup_name from agr_trn_tprocesstype_assign where application_gid='" + dt["application_gid"] + "'";
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
                        ccgroup_name = lsccgroup_name,
                        cadgroupname = dt["cadgroup_name"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        application_no = dt["application_no"].ToString(),
                        ccapproved_date = dt["ccapproved_date"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                           renewal_flag = dt["renewal_flag"].ToString(),
                        amendment_flag = dt["amendment_flag"].ToString(),
                        shortclosing_flag = dt["shortclosing_flag"].ToString()
                    });
                }
                values.sanctiondetails = get_sanctiondtl;
            }
            dt_datatable.Dispose();

            return true;
        }

        //Maker Count
        public void DaCADContractSummaryCount(string user_gid, string employee_gid, CadSanctionCount values)
        {

            msSQL = " select(select count(*) from agr_trn_tprocesstype_assign a  " +
                   " left join agr_mst_tapplication b on a.application_gid = b.application_gid " +
                   " where maker_approvalflag = 'N' and b.sanction_approvalflag = 'N' " +
                   " and maker_gid = '" + employee_gid + "' and menu_gid = 'AGDMGTCON') as MakerPendingCount, " +
                   "  (select count(*) from agr_trn_tprocesstype_assign a " +
                   "  left join agr_mst_tapplication b on a.application_gid = b.application_gid " +
                   "  where maker_approvalflag = 'Y' and b.sanction_approvalflag = 'N' " +
                   "  and maker_gid = '" + employee_gid + "' and menu_gid = 'AGDMGTCON') as MakerFollowUpCount, " +
                   "  (select count(*) from agr_trn_tprocesstype_assign a " +
                   "  left join agr_mst_tapplication b on a.application_gid = b.application_gid " +
                   " left join agr_trn_tapplication2contract c on a.application_gid = c.application_gid " +
                   "  where checker_approvalflag = 'N' and b.sanction_approvalflag = 'N' " +
                   "  and checker_gid = '" + employee_gid + "' and menu_gid = 'AGDMGTCON' and c.sanction_status in ('Pushback','Checker Approval Pending')) as CheckerPendingCount,   " +
                   "  (select count(*) from agr_trn_tprocesstype_assign " +
                   "  where checker_approvalflag = 'Y' and maker_approvalflag = 'Y'  and  approver_approvalflag = 'N'" +
                   "  and checker_gid = '" + employee_gid + "' and menu_gid = 'AGDMGTCON') as CheckerFollowUpCount,  " +
                   "  (select count(*) from agr_trn_tprocesstype_assign a" +
                    " left join agr_trn_tapplication2contract c on a.application_gid = c.application_gid " +
                   "  where a.approver_approvalflag = 'N' and a.checker_approvalflag = 'Y' and c.checkerletter_flag='Y' " +
                   "  and approver_gid = '" + employee_gid + "' and menu_gid = 'AGDMGTCON')  as ApproverPendingCount,  " +
                   "  (select count(*) from agr_trn_tprocesstype_assign a" +
                   " left join agr_trn_tsanctionacceptlog f on f.application_gid = a.application_gid " +
                   "  where approver_approvalflag = 'Y' and checker_approvalflag = 'Y'  and menu_gid = 'AGDMGTCON'  and (f.accepted_status ='N' OR f.accepted_status is null) " +
                   " and( f.updated_date = (select max(updated_date) from agr_trn_tsanctionacceptlog y where y.application_gid = a.application_gid ) " +
                   " or  (f.updated_date is null )) " +
                   "  and(maker_gid = '" + employee_gid + "' or checker_gid = '" + employee_gid + "' or approver_gid = '" + employee_gid + "')) as CompletedCount, " +
                      " (select count(*) from agr_trn_tprocesstype_assign a " +
                   " left join agr_trn_tsanctionacceptlog f on f.application_gid = a.application_gid " +
                     " left join agr_trn_tapplication2contract c on a.application_gid = c.application_gid " +
                     " where c.checkerapproval_flag = 'R' and checker_approvalflag = 'Y'  and menu_gid = 'AGDMGTCON'  and(f.accepted_status = 'N' OR f.accepted_status is null) "+
                    " and(f.updated_date = (select max(updated_date) from agr_trn_tsanctionacceptlog y where y.application_gid = a.application_gid ) " +
                   " or (f.updated_date is null)) " +
                   "  and(maker_gid = '" + employee_gid + "' or checker_gid = '" + employee_gid + "' or approver_gid = '" + employee_gid + "')) "+
                   "  as RejectedCount , " +
                   "  (select count(*) from agr_trn_tprocesstype_assign a" +
                   " left join agr_trn_tsanctionacceptlog f on f.application_gid = a.application_gid " +
                   "  where approver_approvalflag = 'Y' and checker_approvalflag = 'Y' and (  f.accepted_status is null) and menu_gid = 'AGDMGTCON'  and (f.accepted_status ='N' OR f.accepted_status is null) " +
                   " and( f.updated_date = (select max(updated_date) from agr_trn_tsanctionacceptlog y where y.application_gid = a.application_gid ) " +
                   " or  (f.updated_date is null )) " +
                   "  and(maker_gid = '" + employee_gid + "' or checker_gid = '" + employee_gid + "' or approver_gid = '" + employee_gid + "')) as CompletedCADCount, " +
                   "  (select count(*) from agr_trn_tprocesstype_assign a" +
                   " left join agr_trn_tsanctionacceptlog f on f.application_gid = a.application_gid " +
                   "  where approver_approvalflag = 'Y' and checker_approvalflag = 'Y' and (  f.accepted_status is not null) and menu_gid = 'AGDMGTCON'  and (f.accepted_status ='N' OR f.accepted_status is null) " +
                   " and( f.updated_date = (select max(updated_date) from agr_trn_tsanctionacceptlog y where y.application_gid = a.application_gid ) " +
                   " or  (f.updated_date is null )) " +
                   "  ) as CompletedNotAcceptedCount, " +
                   "  (select count(*) from agr_trn_tprocesstype_assign a" +
                   " left join agr_trn_tsanctionacceptlog f on f.application_gid = a.application_gid " +
                   "  where approver_approvalflag = 'Y' and checker_approvalflag = 'Y'  and menu_gid = 'AGDMGTCON'  and f.accepted_status ='Y' " +
                   " and( f.updated_date = (select max(updated_date) from agr_trn_tsanctionacceptlog y where y.application_gid = a.application_gid ) " +
                   " or  (f.updated_date is null )) " +
                   "  and(maker_gid = '" + employee_gid + "' or checker_gid = '" + employee_gid + "' or approver_gid = '" + employee_gid + "')) as AcceptedCount; ";
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
                values.RejectedCount = objODBCDataReader["RejectedCount"].ToString();
            }
            objODBCDataReader.Close();

        }

        //Maker Follow Up Summary
        public void DaGetContractFollowupMakerSummary(string employee_gid, MdlMstCAD values)
        {
            msSQL = " select a.application_gid,a.application_no,a.customerref_name,a.customer_urn,a.creditgroup_name, d.sanction_refno," +
                     " a.customer_name as customer_name,date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as ccapproved_date, a.approval_status," +
                     " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as cadaccepted_by, " +
                     " date_format(a.processupdated_date, '%d-%m-%Y %h:%i %p') as cadaccepted_date, a.created_by, d.sanction_status, " +
                     " a.creditgroup_gid,e.cadgroup_name,a.customer_urn,a.renewal_flag,a.amendment_flag,a.shortclosing_flag from agr_mst_tapplication a " +
                     " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by " +
                     " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                     " left join agr_trn_tapplication2contract d on d.application_gid = a.application_gid " +
                      " left join agr_trn_tprocesstype_assign e on e.application_gid = a.application_gid " +
                     " where a.process_type = 'Accept' and d.sanction_status not in ('Approved') and " +
                     " a.application_gid in (Select application_gid from agr_trn_tprocesstype_assign where menu_gid = '" + getMenuClass.Contract + "' " +
                     " and maker_gid = '" + employee_gid + "' and maker_approvalflag ='Y')" +
                     " group by a.application_gid order by a.updated_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<cadapplicationlist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    string lsccgroup_name;
                    string lsccadmin_name;
                    string lscadgroup_name;

                    msSQL = "select group_concat(ccgroup_name) as ccgroup_name from agr_mst_tapplication where application_gid='" + dt["application_gid"] + "'";
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
                    msSQL = "select group_concat(ccadmin_name) as ccadmin_name from agr_mst_tccschedulemeeting where application_gid='" + dt["application_gid"] + "'";
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
                        cadgroupname = dt["cadgroup_name"].ToString(),
                        cadaccepted_by = dt["cadaccepted_by"].ToString(),
                        cadaccepted_date = dt["cadaccepted_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        sanction_status = dt["sanction_status"].ToString(),
                        sanction_refno = dt["sanction_refno"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        renewal_flag = dt["renewal_flag"].ToString(),
                        amendment_flag = dt["amendment_flag"].ToString(),
                        shortclosing_flag = dt["shortclosing_flag"].ToString()

                    });

                }
            }
            values.cadapplicationlist = getapplicationadd_list;
            dt_datatable.Dispose();
        }

        //Checker Follow Up Summary
        public bool DaContractToCheckerFollowupSummary(sanctiondetailsList values, string employee_gid)
        {
            msSQL = " SELECT a.application2sanction_gid,a.sanction_refno,date_format(a.sanction_date,'%d-%m-%Y') as sanction_date, b.application_no, b.approval_status," +
                   " format((sanction_amount),2) as sanction_amount,a.sanction_limit,b.customer_name, date_format(b.cccompleted_date, '%d-%m-%Y %h:%i %p') as ccapproved_date, sanctionto_name, sanction_status, b.application_gid, " +
                   " concat(d.user_code,' / ',d.user_firstname,d.user_lastname) as makersubmitted_by,date_format(a.makersubmitted_on,'%d-%m-%Y %h:%i %p') as makersubmitted_on," +
                   " reset_flag, e.cadgroup_name,b.renewal_flag,b.amendment_flag,b.shortclosing_flag FROM agr_trn_tapplication2contract a " +
                   " LEFT JOIN agr_mst_tapplication b ON a.application_gid = b.application_gid" +
                   " LEFT JOIN hrm_mst_temployee c ON a.makersubmitted_by=c.employee_gid" +
                   " LEFT JOIN adm_mst_tuser d ON c.user_gid=d.user_gid " +
                   " left join agr_trn_tprocesstype_assign e on e.application_gid = a.application_gid " +
                   " where e.menu_gid =  '" + getMenuClass.Contract + "'   and e.checker_gid = '" + employee_gid + "' and e.checker_approvalflag='Y' and e.approver_approvalflag='N' " +
                   " group by a.application_gid ORDER BY application2sanction_gid DESC ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_sanctiondtl = new List<sanctiondetails>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    string lsccgroup_name;
                    string lscadgroup_name;

                    msSQL = "select group_concat(ccgroup_name) as ccgroup_name from agr_mst_tapplication where application_gid='" + dt["application_gid"] + "'";
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
                        ccgroup_name = lsccgroup_name,
                        cadgroupname = dt["cadgroup_name"].ToString(),
                        renewal_flag = dt["renewal_flag"].ToString(),
                        amendment_flag = dt["amendment_flag"].ToString(),
                        shortclosing_flag = dt["shortclosing_flag"].ToString()
                    });

                }
                values.sanctiondetails = get_sanctiondtl;
            }
            dt_datatable.Dispose();

            return true;
        }



        //Upload Documnet 
        public bool DaContractDocumentUpload(HttpRequest httpRequest, uploaddocument objfilename, string employee_gid)
        {
            upload_list objdocumentmodel = new upload_list();
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            String path = lspath;
            string project_flag = httpRequest.Form["project_flag"].ToString();

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/ContractUploadDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
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
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Master/ContractUploadDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "Master/ContractUploadDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        msGetGid = objcmnfunctions.GetMasterGID("CODU");
                        msSQL = " insert into agr_trn_tcontractdocumentupload( " +
                                    " contractdocumentupload_gid, " +
                                    " application2sanction_gid," +
                                    " document_name," +
                                    " document_path," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + httpPostedFile.FileName.Replace("'", "") + "'," +
                                    "'" + lspath + msdocument_gid + FileExtension.Replace("'", "") + "'," +
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
                        msSQL = " select contractdocumentupload_gid,application2sanction_gid,document_name,document_path from agr_trn_tcontractdocumentupload " +
                                                       " where application2sanction_gid='" + employee_gid + "'";

                        dt_datatable = objdbconn.GetDataTable(msSQL);
                        var getdocumentdtlList = new List<upload_list>();
                        if (dt_datatable.Rows.Count != 0)
                        {
                            foreach (DataRow dt in dt_datatable.Rows)
                            {
                                getdocumentdtlList.Add(new upload_list
                                {
                                    document_name = dt["document_name"].ToString(),
                                    document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                                    contractdocumentupload_gid = dt["contractdocumentupload_gid"].ToString(),
                                });
                                objfilename.upload_list = getdocumentdtlList;
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

        public void DaContractDocList(string application2sanction_gid, string employee_gid, MdlMstCAD values)
        {
            msSQL = " select contractdocumentupload_gid, application2sanction_gid,document_name,document_path from agr_trn_tcontractdocumentupload " +
                                 " where application2sanction_gid='" + application2sanction_gid + "' or application2sanction_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdocumentdtlList = new List<Contractupload_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getdocumentdtlList.Add(new Contractupload_list
                    {
                        document_name = dt["document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                        application2sanction_gid = dt["application2sanction_gid"].ToString(),
                        tmpcicdocument_gid = dt["contractdocumentupload_gid"].ToString(),
                       
                    });
                    values.Contractupload_list = getdocumentdtlList;
                }
            }
            dt_datatable.Dispose();
        }


        //public void DaTmpDocumentDelete(string tmp_documentGid, MdlMstCAD values)
        //{
        //    msSQL = " delete from agr_trn_tcontractdocumentupload where contractdocumentupload_gid='" + tmp_documentGid + "'";
        //    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

        //    if (mnResult != 0)
        //    {
        //        values.status = true;
        //        values.message = "Document Deleted Successfully..!";
        //    }
        //    else
        //    {
        //        values.status = false;
        //        values.message = "Error Occured..!";
        //    }
        //}

        public void DaTmpDocumentDelete(string contractdocumentupload_gid, uploaddocument objfilename, string employee_gid)
        {
            msSQL = " delete from agr_trn_tcontractdocumentupload where contractdocumentupload_gid='" + contractdocumentupload_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            //msSQL = " select contractdocumentupload_gid,document_name,document_path from agr_trn_tcontractdocumentupload " +
            //        " where application2sanction_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdocumentdtlList = new List<upload_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getdocumentdtlList.Add(new upload_list
                    {
                        document_name = dt["document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                        contractdocumentupload_gid = dt["contractdocumentupload_gid"].ToString(),
                    });
                    objfilename.upload_list = getdocumentdtlList;
                }
            }
            dt_datatable.Dispose();

            if (mnResult != 0)
            {
                objfilename.status = true;
                objfilename.message = "Document Deleted Successfully..!";
            }
            else
            {
                objfilename.status = false;
                objfilename.message = "Error Occured..!";
            }
        }

        //Document Delete Start 
        public void DaContractSummaryDocList(string application2sanction_gid, string employee_gid, uploaddocument values)
        {
            msSQL = " select contractdocumentupload_gid,application2sanction_gid,document_name,document_path from agr_trn_tcontractdocumentupload " +
                                 " where application2sanction_gid='" + application2sanction_gid + "' or application2sanction_gid='" + employee_gid + "' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdocumentdtlList = new List<upload_list>();
            if (dt_datatable.Rows.Count != 0)
            {

                // Create list
                var file_name = new List<string>();
                var file_path = string.Empty;

                foreach (DataRow dt in dt_datatable.Rows)
                {
                    file_name.Add(dt["document_name"].ToString());
                    file_path = objcmnstorage.EncryptData(dt["document_path"].ToString());
                }
                values.filename = file_name.ToArray();
                values.filepath = file_path.ToString();

                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getdocumentdtlList.Add(new upload_list
                    {
                        document_name = dt["document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                        application2sanction_gid = dt["application2sanction_gid"].ToString(),
                        contractdocumentupload_gid = dt["contractdocumentupload_gid"].ToString(),
                        //document_content = dt["document_content"].ToString(),
                    });
                    values.upload_list = getdocumentdtlList;
                }
            }
            dt_datatable.Dispose();
        }
        //Document Delete End


        //Proceed To Checker
        public void DaPostProceedToChecker(cadtemplate_list values, string employee_gid)
        {
            msSQL = " select maker_gid from agr_trn_tprocesstype_assign a " +
                   " where a.processtype_name = 'Accept' and a.menu_gid = 'AGDMGTCON' and a.application_gid = '" + values.application_gid + "'";
            values.maker_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select checker_gid from agr_trn_tprocesstype_assign a " +
                   " where a.processtype_name = 'Accept' and a.menu_gid = 'AGDMGTCON' and a.application_gid = '" + values.application_gid + "'";
            values.checker_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select approver_gid from agr_trn_tprocesstype_assign a " +
                   " where a.processtype_name = 'Accept' and a.menu_gid = 'AGDMGTCON' and a.application_gid = '" + values.application_gid + "'";
            values.approver_gid = objdbconn.GetExecuteScalar(msSQL);

            if (values.maker_gid == values.checker_gid)
            {

              
               


                msSQL = " update agr_trn_tapplication2contract set sanctionletter_flag='Y',checkerletter_flag='Y', sanction_status ='Final Approval Pending', checkerupdated_by ='" + employee_gid + "', " +
                        " checkerupdated_on='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where application2sanction_gid= '" + values.application2sanction_gid + "'";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    msSQL = " select  processtypeassign_gid from agr_trn_tprocesstype_assign a " +
                             " left join agr_trn_tapplication2contract b on a.application_gid = b.application_gid " +
                             " where application2sanction_gid = '" + values.application2sanction_gid + "' and a.menu_gid ='" + getMenuClass.Contract + "'";
                    string lsprocesstypeassign_gid = objdbconn.GetExecuteScalar(msSQL);

                    if (lsprocesstypeassign_gid != "")
                    {
                        msSQL = " update agr_trn_tprocesstype_assign set maker_approvalflag='Y', " +
                                " maker_approveddate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                " checker_approvalflag ='Y', checker_approveddate = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                " overall_approvalstatus='Approved'  where processtypeassign_gid='" + lsprocesstypeassign_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }

                    //msSQL = " select template_name, template_gid from agr_trn_tapplication2sanction where application2sanction_gid='" + values.sanction_gid + "'";
                    //objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    //if (objODBCDataReader.HasRows == true)
                    //{
                    //    values.template_name = objODBCDataReader["template_name"].ToString();
                    //    values.template_gid = objODBCDataReader["template_gid"].ToString();
                    //}
                    //objODBCDataReader.Close();

                    msGetGid1 = objcmnfunctions.GetMasterGID("ASSA");
                    msSQL = "insert into agr_trn_tsanctionapprovallog(" +
                            " sanctionapprovallog_gid, " +
                            " sanction_gid," +
                            //" template_gid, " +
                            //" template_name, " +
                            //" template_content, " +
                            " sanctionletter_flag," +
                            " sanction_status," +
                            " checkerpushback_remarks," +
                            " checkerreject_remarks," +
                            " created_by," +
                            " created_date)" +
                            " values (" +
                            "'" + msGetGid1 + "'," +
                            "'" + values.application2sanction_gid + "'," +
                            //"'" + values.template_gid + "'," +
                            //"'" + values.template_name + "'," +
                            //"'" + values.template_content.Replace("'", "''") + "'," +
                            "'Y'," +
                            "'Checker Approved'," +
                            "''," +
                            "''," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    values.message = "Contract proceeded to Approval Successfully";
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

              
             

                msSQL = " select application_gid,relationshipmanager_name, relationshipmanager_gid, clustermanager_gid, clustermanager_name, zonalhead_name, zonalhead_gid," +
                " regionalhead_name, regionalhead_gid, businesshead_name, businesshead_gid from agr_mst_tapplication" +
                " where application_gid = '" + values.application_gid + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows == true)
                {
                    msSQL = " update agr_trn_tapplication2contract set relationshipmgr_gid='" + objODBCDataReader["relationshipmanager_gid"].ToString() + "'," +
                            " relationshipmgr_name='" + objODBCDataReader["relationshipmanager_name"].ToString() + "' where application2sanction_gid='" + values.application2sanction_gid + "'";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                objODBCDataReader.Close();

                msSQL = "update agr_trn_tapplication2contract set sanctionletter_flag='Y', sanction_status='Checker Approval Pending' where application2sanction_gid='" + values.application2sanction_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    msSQL = " select  processtypeassign_gid from agr_trn_tprocesstype_assign a " +
                             " left join agr_trn_tapplication2contract b on a.application_gid = b.application_gid " +
                             " where application2sanction_gid = '" + values.application2sanction_gid + "' and a.menu_gid ='" + getMenuClass.Contract + "'";
                    string lsprocesstypeassign_gid = objdbconn.GetExecuteScalar(msSQL);

                    if (lsprocesstypeassign_gid != "")
                    {
                        msSQL = " update agr_trn_tprocesstype_assign set maker_approvalflag='Y', " +
                                " maker_approveddate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                " overall_approvalstatus='Proceed to Checker'  where processtypeassign_gid='" + lsprocesstypeassign_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }

                    //msSQL = " select template_name, template_gid from agr_trn_tapplication2sanction where application2sanction_gid='" + values.sanction_gid + "'";
                    //objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    //if (objODBCDataReader.HasRows == true)
                    //{
                    //    values.template_name = objODBCDataReader["template_name"].ToString();
                    //    values.template_gid = objODBCDataReader["template_gid"].ToString();
                    //}
                    //objODBCDataReader.Close();

                    msGetGid1 = objcmnfunctions.GetMasterGID("ASLL");
                    msSQL = "insert into agr_trn_tsanctionapprovallog(" +
                        " sanctionapprovallog_gid, " +
                        " sanction_gid," +
                        //" template_gid, " +
                        //" template_name, " +
                        //" template_content, " +
                        " sanctionletter_flag," +
                        " sanction_status," +
                        " checkerpushback_remarks," +
                        " checkerreject_remarks," +
                        " created_by," +
                        " created_date)" +
                        " values (" +
                        "'" + msGetGid1 + "'," +
                        "'" + values.application2sanction_gid + "'," +
                        //"'" + values.template_gid + "'," +
                        //"'" + values.template_name + "'," +
                        //"'" + values.template_content.Replace("'", "''") + "'," +
                        "'Y'," +
                        "'Checker Approval Pending'," +
                        "''," +
                        "''," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    values.message = "Contract proceeded to Checker Successfully";
                    values.status = true;
                }
                else
                {
                    values.message = "Error Occrued";
                    values.status = false;
                }
            }
        }


        //Contract Submit

        public bool DaPostCADSanction(string employee_gid, cadsanctiondetails values)
        {
            msSQL = " select validityfrom_date from agr_mst_tapplication where application_gid='" + values.application_gid + "'";
            string validityfrom_date = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " select validityto_date from agr_mst_tapplication where application_gid='" + values.application_gid + "'";
            string validityto_date = objdbconn.GetExecuteScalar(msSQL);
            msGetGid = objcmnfunctions.GetMasterGID("SACF");


            msSQL = " INSERT INTO agr_trn_tapplication2contract( " +
                           " application2sanction_gid," +
                           " contract_id," +
                           " application_gid ," +
                           " validityfrom_date," +
                           " validityto_date," +
                           " contract_date," +
                           " created_by," +
                           " created_date," +
                           " updated_by," +
                           " updated_date)" +
                           " values(" +
                           "'" + msGetGid + "', " +
                           "'" + values.contract_id + "'," +
                           "'" + values.application_gid + "'," +
                     " (select validityfrom_date from agr_mst_tapplication where application_gid = '" + values.application_gid + "') ," +
                      "(select validityto_date from agr_mst_tapplication where application_gid='" + values.application_gid + "'),";
            if ((values.contract_date == null) || (values.contract_date == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.contract_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            msSQL += "'" + employee_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                     "'" + employee_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult1 = objdbconn.ExecuteNonQuerySQL(msSQL);


            if (mnResult1 != 0)
            {


                msSQL = " select application_gid,relationshipmanager_name, relationshipmanager_gid, clustermanager_gid, clustermanager_name, zonalhead_name, zonalhead_gid," +
                        " regionalhead_name, regionalhead_gid, businesshead_name, businesshead_gid from agr_mst_tapplication" +
                        " where application_gid = '" + values.application_gid + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows == true)
                {
                    msSQL = " update agr_trn_tapplication2contract set relationshipmgr_gid='" + objODBCDataReader["relationshipmanager_gid"].ToString() + "'," +
                            " relationshipmgr_name='" + objODBCDataReader["relationshipmanager_name"].ToString() + "' where application2sanction_gid='" + msGetGid + "'";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                objODBCDataReader.Close();

                msSQL = " update agr_trn_tcontractdocumentupload set application2sanction_gid='" + msGetGid + "' " +
                        " where  application2sanction_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                //Mdlloanfacility_type objvalue = new Mdlloanfacility_type();
                ////DaGetCADLoanFacilityTemplateList(msGetGid, objvalue);


                //// CAM Document Updation
                //msSQL = " select application2camdoc_gid, application_gid,document_name,document_path,document_title" +
                //   " from agr_mst_tapplication2camdoc a " +
                //   " where application_gid='" + values.application_gid + "'";
                //dt_datatable = objdbconn.GetDataTable(msSQL);
                //var getdocumentdtlList = new List<cadmomdocument_list>();
                //if (dt_datatable.Rows.Count != 0)
                //{
                //    foreach (DataRow dt in dt_datatable.Rows)
                //    {
                //        msGetGid1 = objcmnfunctions.GetMasterGID("ACAM");
                //        msSQL = " insert into agr_trn_tuploadcamdocument( " +
                //                     " camdocument_gid," +
                //                     " document_name, " +
                //                     " document_path, " +
                //                     " application2sanction_gid," +
                //                     " application_gid," +
                //                     " document_title," +
                //                     " created_by ," +
                //                     " created_date " +
                //                     " )values(" +
                //                     "'" + msGetGid1 + "'," +
                //                     "'" + dt["document_name"].ToString() + "'," +
                //                     "'" + dt["document_path"].ToString() + "'," +
                //                     "'" + msGetGid + "'," +
                //                     "'" + values.application_gid + "'," +
                //                     "'" + dt["document_title"].ToString() + "'," +
                //                     "'" + employee_gid + "'," +
                //                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                //        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                //    }
                //}
                //dt_datatable.Dispose();

                //// MOM Document Updation
                //msSQL = " select application2momdoc_gid,application_gid,document_name,document_path,document_title" +
                //       " from  agr_mst_tapplication2momdoc a " +
                //       " where application_gid='" + values.application_gid + "'";
                //dt_datatable = objdbconn.GetDataTable(msSQL);
                //var getcamdocumentdtlList = new List<cadcamdocument_list>();
                //if (dt_datatable.Rows.Count != 0)
                //{
                //    foreach (DataRow dt in dt_datatable.Rows)
                //    {
                //        msGetGid1 = objcmnfunctions.GetMasterGID("AMOM");
                //        msSQL = " insert into agr_trn_tuploadmomdocument( " +
                //         " momdocument_gid," +
                //         " document_name, " +
                //         " document_path, " +
                //         " application2sanction_gid," +
                //         " application_gid," +
                //         " document_title," +
                //         " created_by ," +
                //         " created_date " +
                //         " )values(" +
                //         "'" + msGetGid1 + "'," +
                //         "'" + dt["document_name"].ToString() + "'," +
                //         "'" + dt["document_path"].ToString() + "'," +
                //         "'" + msGetGid + "'," +
                //         "'" + values.application_gid + "'," +
                //         "'" + dt["document_title"].ToString() + "'," +
                //         "'" + employee_gid + "'," +
                //         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                //        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                //    }
                //}
                //dt_datatable.Dispose();

                ////Completed..//
                ////CC Member//
                //msSQL = " select ccmember_name,ccmember_gid,attendance_status,approval_status, ccmeeting2members_gid as ccmeeting2members_gid, ccgroup_name as ccgroup_name" +
                //       " from agr_mst_tccmeeting2members where application_gid='" + values.application_gid + "'" +
                //       " union" +
                //       " select employee_name as ccmember_name,employee_gid as ccmember_gid,attendance_status, approval_status, ccmeeting2othermembers_gid as ccmeeting2members_gid, '-' as ccgroup_name" +
                //       " from agr_mst_tccmeeting2othermembers where application_gid='" + values.application_gid + "'";
                //dt_datatable = objdbconn.GetDataTable(msSQL);
                //var getccmember_list = new List<cadccmember_list>();
                //if (dt_datatable.Rows.Count != 0)
                //{
                //    foreach (DataRow dt in dt_datatable.Rows)
                //    {
                //        msGetGidCC = objcmnfunctions.GetMasterGID("ACCM");
                //        msSQL = " insert into agr_trn_tsanction2ccmemberlist(" +
                //                " ccmemberlist_gid," +
                //                " application2sanction_gid," +
                //                " application_gid," +
                //                " ccmember_gid," +
                //                " ccmember_name," +
                //                " ccgroup_name," +
                //                " created_by," +
                //                " created_date)" +
                //                " values(" +
                //                "'" + msGetGidCC + "'," +
                //                "'" + msGetGid + "'," +
                //                "'" + values.application_gid + "'," +
                //                "'" + dt["ccmember_gid"].ToString() + "'," +
                //                "'" + dt["ccmember_name"].ToString() + "'," +
                //                "'" + dt["ccgroup_name"].ToString() + "'," +
                //                "'" + employee_gid + "'," +
                //                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                //        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                //    }
                //}
                //dt_datatable.Dispose();

                //msSQL = " select application2loan_gid from agr_mst_tapplication2loan where application_gid='" + values.application_gid + "' and productsub_type='Agri Receivable Finance (ARF)'";
                //string lsapplication2loan_gid = objdbconn.GetExecuteScalar(msSQL);

                //msSQL = " select application2buyer_gid as addbuyer_gid, buyer_name, buyer_gid, buyer_limit, availed_limit, balance_limit, bill_tenure, margin " +
                //       " from agr_mst_tapplication2buyer where application2loan_gid='" + lsapplication2loan_gid + "'" +
                //       " union " +
                //       " select creditbuyer_gid as addbuyer_gid, buyer_name, buyer_gid, buyer_limit, availed_limit, balance_limit, bill_tenuredays as bill_tenure, margin " +
                //       " from agr_mst_tcreditbuyer where application_gid='" + values.application_gid + "'";
                //dt_datatable = objdbconn.GetDataTable(msSQL);
                //var get_filename = new List<cadbuyer_list>();
                //if (dt_datatable.Rows.Count != 0)
                //{
                //    foreach (DataRow dr_datarow in dt_datatable.Rows)
                //    {
                //        msGetGid1 = objcmnfunctions.GetMasterGID("ABUY");
                //        msSQL = "insert into agr_trn_taddbuyer (" +
                //              " addbuyer_gid," +
                //              " buyer_name," +
                //              " application2sanction_gid," +
                //              " application_gid," +
                //              " buyer_limit," +
                //              " availed_limit," +
                //              " balance_limit," +
                //              " bill_tenure," +
                //              " margin," +
                //              " created_by," +
                //              " created_date" +
                //              " )values(" +
                //              "'" + msGetGid1 + "'," +
                //              "'" + dr_datarow["buyer_name"].ToString() + "'," +
                //              "'" + msGetGid + "'," +
                //              "'" + values.application_gid + "'," +
                //              "'" + dr_datarow["buyer_limit"].ToString() + "'," +
                //              "'" + dr_datarow["availed_limit"].ToString() + "'," +
                //              "'" + dr_datarow["balance_limit"].ToString() + "'," +
                //              "'" + dr_datarow["bill_tenure"].ToString() + "'," +
                //              "'" + dr_datarow["margin"].ToString() + "'," +
                //              "'" + employee_gid + "'," +
                //              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                //        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                //    }
                //}
                //dt_datatable.Dispose();

                //msSQL = " update agr_trn_tlimitproductinfo set application2sanction_gid='" + msGetGid + "' " +
                //        " where application_gid='" + values.application_gid + "' and application2sanction_gid='" + employee_gid + "'";
                //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                values.lsapplication2sanction_gid = "'" + msGetGid + "'";
                values.status = true;
                values.message = "Contract Created Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured While Creating Contract";
                return false;

            }
        }

        //Contrate Summary

        public void DaGetContract(string application_gid,MdlMstCAD objapplication360)
        {
            try
            {
                msSQL = " SELECT application2sanction_gid,a.contract_id,date_format(a.validityfrom_date,'%d-%m-%Y') as validityfrom_date," +
                    " date_format(a.validityto_date,'%d-%m-%Y') as validityto_date,d.customer_name, d.application_no," +
                    " date_format(a.contract_date,'%d-%m-%Y') as contract_date " +
                    " FROM agr_trn_tapplication2contract a" +
                    " left join agr_mst_tapplication d on d.application_gid = a.application_gid" +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " Where a.application_gid='" + application_gid + "'" +
                    " order by a.application2sanction_gid desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcontractsummary_list = new List<contractsummary_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcontractsummary_list.Add(new contractsummary_list
                        {
                            application2sanction_gid = (dr_datarow["application2sanction_gid"].ToString()),
                            contract_id = (dr_datarow["contract_id"].ToString()),
                            application_no = (dr_datarow["application_no"].ToString()),
                            customer_name = (dr_datarow["customer_name"].ToString()),
                            validityfrom_date = (dr_datarow["validityfrom_date"].ToString()),
                            validityto_date = (dr_datarow["validityto_date"].ToString()),
                            contract_date = (dr_datarow["contract_date"].ToString())
                            //created_by = (dr_datarow["created_by"].ToString()),
                            //created_date = (dr_datarow["created_date"].ToString()),                         
                        });
                    }
                    objapplication360.contractsummary_list = getcontractsummary_list;
                }
                dt_datatable.Dispose();
                objapplication360.status = true;
            }
            catch
            {
                objapplication360.status = false;
            }
        }

        //Contract Edit Document Upload
        public void DaContractEditDocList(string application2sanction_gid, string employee_gid, MdlMstCAD values)
        {
            msSQL = " select contractdocumentupload_gid, application2sanction_gid,document_name,document_path from agr_trn_tcontractdocumentupload " +
                                 " where application2sanction_gid='" + application2sanction_gid + "' or application2sanction_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdocumentdtlList = new List<Contractupload_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getdocumentdtlList.Add(new Contractupload_list
                    {
                        document_name = dt["document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                        application2sanction_gid = dt["application2sanction_gid"].ToString(),
                        contractdocumentupload_gid = dt["contractdocumentupload_gid"].ToString(),
                       
                    });
                    values.Contractupload_list = getdocumentdtlList;
                }
            }
            dt_datatable.Dispose();
        }


        public void DaContractDocEditActList( string employee_gid, MdlMstCAD values)
        {
            msSQL = " select contractdocumentupload_gid, application2sanction_gid,document_name,document_path from agr_trn_tcontractdocumentupload " +
                                 " where   application2sanction_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdocumentdtlList = new List<Contractupload_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getdocumentdtlList.Add(new Contractupload_list
                    {
                        document_name = dt["document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                        application2sanction_gid = dt["application2sanction_gid"].ToString(),
                        contractdocumentupload_gid = dt["contractdocumentupload_gid"].ToString(),

                    });
                    values.Contractupload_list = getdocumentdtlList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetContractDtl(string application2sanction_gid, MdlMstApplicationView values)
        {
            try
            {
                msSQL = " SELECT application2sanction_gid,a.contract_id,b.buyeragreement_id ,date_format(a.validityfrom_date,'%d-%m-%Y') as validityfrom_date," +
                    " date_format(a.validityto_date,'%d-%m-%Y') as validityto_date," +
                    " date_format(a.contract_date,'%d-%m-%Y') as contract_date " +
                    " FROM agr_trn_tapplication2contract a" +
                    " left join agr_mst_tapplication b on a.application_gid = b.application_gid " +
                    " where application2sanction_gid='" + application2sanction_gid + "' group by a.application2sanction_gid ";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.application2sanction_gid = objODBCDatareader["application2sanction_gid"].ToString();
                    values.contract_id = objODBCDatareader["contract_id"].ToString();                                  
                    values.validityfrom_date = objODBCDatareader["validityfrom_date"].ToString();
                    values.validityto_date = objODBCDatareader["validityto_date"].ToString();
                    values.contract_date = objODBCDatareader["contract_date"].ToString();
                   values.buyeragreement_id = objODBCDatareader["buyeragreement_id"].ToString();
                }
                values.status = true;
                values.message = "success";
                objODBCDatareader.Close();

                

                //objODBCDatareader.Close();
            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }
        }
        
        //Checker detail Summary
        public void DaGetAppContractSummary(string application_gid, MdlMstCAD values)
        {
            msSQL = " select a.application2sanction_gid, a.sanction_refno, date_format(a.sanction_date,'%d-%m-%Y') as sanction_date, b.customer_name," +
                    " b.ccsubmitted_date,a.contract_id, b.application_no, b.application_gid, sanctionto_name, " +
                    " date_format(a.validityfrom_date, '%d-%m-%Y') as validityfrom_date," +
                    " date_format(a.validityto_date,'%d-%m-%Y') as validityto_date," +
                    " date_format(a.contract_date,'%d-%m-%Y') as contract_date, sanction_status" +
                    " from agr_trn_tapplication2contract a " +
                    " left join agr_mst_tapplication b on b.application_gid = a.application_gid" +
                    " left join hrm_mst_temployee d on a.created_by = d.employee_gid" +
                    " left join adm_mst_tuser c on c.user_gid = d.user_gid " +
                    " where a.application_gid='" + application_gid + "'" +
                    " order by a.application2sanction_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getappsanction_list = new List<appsanction_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getappsanction_list.Add(new appsanction_list
                    {
                        application2sanction_gid = dt["application2sanction_gid"].ToString(),
                        sanction_refno = dt["sanction_refno"].ToString(),
                        sanction_date = dt["sanction_date"].ToString(),
                        application_name = dt["customer_name"].ToString(),
                        ccapproved_date = dt["ccsubmitted_date"].ToString(),
                        application_no = dt["application_no"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        contract_id = dt["contract_id"].ToString(),
                        validityfrom_date = dt["validityfrom_date"].ToString(),
                        validityto_date = dt["validityto_date"].ToString(),
                        contract_date = dt["contract_date"].ToString(),
                        sanctionto_name = dt["sanctionto_name"].ToString(),
                        sanction_status = dt["sanction_status"].ToString(),
                    });
                    values.appsanction_list = getappsanction_list;
                }
            }
            dt_datatable.Dispose();
        }


        //Template

        public bool DaGetCADTemplateDetails(mdltemplate values, string application2sanction_gid)
        {
            values.mstcontent_flag = "N";
            msSQL = " select application_gid, sanctionletter_status,template_gid, template_name, template_content, makerfile_name, makerfile_path, sanctionletter_flag, checkerapproval_flag," +
                    " checkerletter_flag, checkerpushback_remarks, digitalsignature_flag, date_format(checkerupdated_on, '%d-%m-%Y') as checkerupdated_on," +
                    " concat(c.user_firstname, ' ', c.user_lastname, '/', c.user_code) as checkerupdated_by, date_format(makersubmitted_on, '%d-%m-%Y') as makersubmitted_on," +
                    " concat(e.user_firstname, ' ', e.user_lastname, '/', e.user_code) as makersubmitted_by,defaulttemplate_content " +
                    " from agr_trn_tapplication2contract a " +
                    " left join hrm_mst_temployee b on b.employee_gid = a.checkerupdated_by " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " left join hrm_mst_temployee d on d.employee_gid = a.makersubmitted_by " +
                    " left join adm_mst_tuser e on e.user_gid = d.user_gid " +
                    " where application2sanction_gid='" + application2sanction_gid + "'";
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
                       " left join agr_mst_tapplication b on a.vertical_gid = b.vertical_gid and a.program_gid = b.program_gid " +
                       " where b.application_gid = '" + application_gid + "' and a.template_type='" + getTemplateClass.Contract + "'" +
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

        public bool DaCADContractLetterSummary(string application2sanction_gid, sanctiondetailsList values)
        {
            msSQL = " SELECT a.sanctionapprovallog_gid, a.sanction_gid, a.sanction_status, concat(c.user_firstname, c.user_lastname, ' / ', c.user_code) as created_by, " +
                   " date_format(a.created_date, '%d-%m-%Y %H:%i %p') as created_date, checkerpushback_remarks" +
                   " FROM agr_trn_tsanctionapprovallog a " +
                   " LEFT JOIN hrm_mst_temployee b ON a.created_by=b.employee_gid" +
                   " LEFT JOIN adm_mst_tuser c ON c.user_gid=b.user_gid where sanction_gid= '" + application2sanction_gid + "'";
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

        public bool DaCADContractLetterSave(cadtemplate_list values, string employee_gid)
        {
            msSQL = " update agr_trn_tapplication2contract set sanctionletter_status='Saved'," +
                    " template_content='" + values.template_content.Replace("'", "''") + "', makersubmitted_by='" + employee_gid + "'," +
                    " makersubmitted_on='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where application2sanction_gid='" + values.application2sanction_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult == 1)
            {
                values.status = true;
                values.message = "Contract Letter Saved Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
                return true;
            }
        }

        public bool DaCADContractLetterSubmit(cadtemplate_list values, string employee_gid)
        {
            msSQL = " select template_name, template_gid from agr_trn_tapplicationcontract where application2sanction_gid='" + values.application2sanction_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                values.template_name = objODBCDataReader["template_name"].ToString();
                values.template_gid = objODBCDataReader["template_gid"].ToString();
            }
            objODBCDataReader.Close();
            msGetGid = objcmnfunctions.GetMasterGID("ASLG");
            msSQL = "insert into agr_trn_tsanctionlettergenerate(" +
                " sanctionlettergenerate_gid, " +
                " sanction_gid," +
                " template_gid, " +
                " template_name, " +
                " template_content, " +
                " sanctionlettergenerated_by," +
                " sanctionlettergenerated_date," +
                " created_by," +
                " created_date)" +
                " values (" +
                "'" + msGetGid + "'," +
                "'" + values.application2sanction_gid + "'," +
                "'" + values.template_gid + "'," +
                "'" + values.template_name + "'," +
                "'" + values.template_content.Replace("'", "''") + "'," +
                "'" + employee_gid + "'," +
                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                "'" + employee_gid + "'," +
                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult == 1)
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                //values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "SamAgro/SanctionLetterGeneration/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/ContractLetterGeneration/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                {
                    if ((!System.IO.Directory.Exists(values.lspath)))
                        System.IO.Directory.CreateDirectory(values.lspath);
                }

                values.lsname = "Contract_Letter" + DateTime.Now.ToString("(dd-MM-yyyy HH-mm-ss)") + ".docx";
                //values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "SamAgro/SanctionLetterGeneration/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;
                values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/ContractLetterGeneration/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;

                msSQL = " update agr_trn_tapplication2contract set makerfile_path='" + values.lspath + "', makerfile_name='" + values.lsname + "', sanctionletter_status='Generated'," +
                        " template_content='" + values.template_content.Replace("'", "''") + "', makersubmitted_by='" + employee_gid + "'," +
                        " makersubmitted_on='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where application2sanction_gid='" + values.application2sanction_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                //lspath = HttpContext.Current.Server.MapPath("../../../erp_documents" + "/" + lscompany_code + "/" + "SamAgro/SanctionLetterGeneration/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/");
                lspath = HttpContext.Current.Server.MapPath("erpdocument" + "/" + lscompany_code + "/" + "SamAgro/ContractLetterGeneration/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/");
                string lsfile_gid = objcmnfunctions.GetMasterGID("UPLF");

                // Save the HTML string as HTML File.
                //string htmlFilePath = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "SamAgro/SanctionLetterHTML/sanctionletterdoc.html";
                string htmlFilePath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/ContractLetterHTML/Contractletterdoc.html";
                File.WriteAllText(htmlFilePath, values.template_content);

                Spire.Doc.Document document = new Spire.Doc.Document();

                document.LoadFromFile(htmlFilePath, Spire.Doc.FileFormat.Html, XHTMLValidationType.None);

                //Read Header and Footer File

                Document doc1 = new Document();
                //doc1.LoadFromFile(ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "TmpFileSamAgro/Logo/headerfile.docx");
                doc1.LoadFromFile(ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "TmpFileSamAgro/Logo/headerfile.docx");
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
                //string lssanctionletterfile = HttpContext.Current.Server.MapPath("../../../erp_documents" + "/" + lscompany_code + "/" + "TmpFileSamAgro/Footer/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + lsfile_gid + "/");
                string lssanctionletterfile = HttpContext.Current.Server.MapPath("erpdocument" + "/" + lscompany_code + "/" + "TmpFileSamAgro/Footer/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + lsfile_gid + "/");
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
                values.message = "Contract Letter Generated Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
                return true;
            }
        }


        public bool DaCADSanctionLetterSummary(string application2sanction_gid, sanctiondetailsList values)
        {
            msSQL = " SELECT a.sanctionapprovallog_gid, a.sanction_gid, a.sanction_status, concat(c.user_firstname, c.user_lastname, ' / ', c.user_code) as created_by, " +
                   " date_format(a.created_date, '%d-%m-%Y %H:%i %p') as created_date, checkerpushback_remarks" +
                   " FROM agr_trn_tsanctionapprovallog a " +
                   " LEFT JOIN hrm_mst_temployee b ON a.created_by=b.employee_gid" +
                   " LEFT JOIN adm_mst_tuser c ON c.user_gid=b.user_gid where sanction_gid= '" + application2sanction_gid + "'";
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


        public bool DaCADSanctionLetterSave(cadtemplate_list values, string employee_gid)
        {
            msSQL = " update agr_trn_tapplication2contract set sanctionletter_status='Saved'," +
                    " template_content='" + values.template_content.Replace("'", "''") + "'," +
                    " defaulttemplate_content='" + values.defaulttemplate_content.Replace("'", "''") + "'," +
                    " makersubmitted_by='" + employee_gid + "'," +
                    " makersubmitted_on='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where application2sanction_gid='" + values.sanction_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " update ocs_trn_tdynamictemplatedtl set template_content='" + values.defaulttemplate_content.Replace("'", "''") + "' " +
                    " where templatetype_gid='" + values.sanction_gid + "' and templatetype_name='" + getTemplateClass.Contract + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult == 1)
            {
                values.status = true;
                values.message = "Contract Letter Saved Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
                return true;
            }
        }

        public bool DaCADSanctionLetterSubmit(cadtemplate_list values, string employee_gid)
        {
            msSQL = " select template_name, template_gid from agr_trn_tapplication2contract where application2sanction_gid='" + values.sanction_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                values.template_name = objODBCDataReader["template_name"].ToString();
                values.template_gid = objODBCDataReader["template_gid"].ToString();
            }
            objODBCDataReader.Close();
            msGetGid = objcmnfunctions.GetMasterGID("SAGG");
            msSQL = "insert into agr_trn_tsanctionlettergenerate(" +
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
                values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/ContractLetterGeneration/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                {
                    if ((!System.IO.Directory.Exists(values.lspath)))
                        System.IO.Directory.CreateDirectory(values.lspath);
                }

                values.lsname = "Contract_Letter" + DateTime.Now.ToString("(dd-MM-yyyy HH-mm-ss)") + ".docx";
                values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/ContractLetterGeneration/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;

                msSQL = " update agr_trn_tapplication2contract set makerfile_path='" + lscompany_code + "/" + "Master/ContractLetterGeneration/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname + "'," +
                        " makerfile_name='" + values.lsname + "', sanctionletter_status='Generated'," +
                        " defaulttemplate_content='" + values.defaulttemplate_content.Replace("'", "''") + "'," +
                        " template_content='" + values.template_content.Replace("'", "''") + "', makersubmitted_by='" + employee_gid + "'," +
                        " makersubmitted_on='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where application2sanction_gid='" + values.sanction_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update ocs_trn_tdynamictemplatedtl set template_content='" + values.defaulttemplate_content.Replace("'", "''") + "' " +
                        " where templatetype_gid='" + values.sanction_gid + "' and templatetype_name='" + getTemplateClass.Contract + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                lspath = HttpContext.Current.Server.MapPath("erpdocument" + "/" + lscompany_code + "/" + "Master/ContractLetterGeneration/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/");
                string lsfile_gid = objcmnfunctions.GetMasterGID("UPLF");

                // Save the HTML string as HTML File.
                string htmlFilePath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/ContractLetterHTML";
                {
                    if ((!System.IO.Directory.Exists(htmlFilePath)))
                        System.IO.Directory.CreateDirectory(htmlFilePath);
                }

                htmlFilePath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/ContractLetterHTML/Contractletterdoc.html";
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
                status = objcmnstorage.UploadStream("../../../erpdocument", lscompany_code + "/" + "Master/ContractLetterGeneration/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname, ms);
                ms.Close();
                File.Delete(htmlFilePath);

                values.status = true;
                values.message = "Contract Letter Generated Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
                return true;
            }
        }
        public void DaGetApprovalDetails(string application_gid, MdlChequeApprovalDetails values)
        {
            try
            {
                msSQL = " select application_gid,maker_name,checker_name,approver_name,date_format(maker_approveddate, '%d-%m-%Y %h:%i %p') as maker_approveddate,date_format(checker_approveddate, '%d-%m-%Y %h:%i %p') as checker_approveddate,date_format(approver_approveddate, '%d-%m-%Y %h:%i %p') as approver_approveddate " +
                        " from agr_trn_tprocesstype_assign where processtype_name = 'Accept' and menu_gid = '" + getMenuClass.Contract + "' and application_gid='" + application_gid + "'";
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

     

        public void DaPostProceedToApproval(string employee_gid, cadtemplate_list values)
        {
            msSQL = " select maker_gid from agr_trn_tprocesstype_assign a " +
                   " where a.processtype_name = 'Accept' and a.menu_gid = 'AGDMGTCON' and a.application_gid = '" + values.application_gid + "'";
            values.maker_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select checker_gid from agr_trn_tprocesstype_assign a " +
                   " where a.processtype_name = 'Accept' and a.menu_gid = 'AGDMGTCON' and a.application_gid = '" + values.application_gid + "'";
            values.checker_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select approver_gid from agr_trn_tprocesstype_assign a " +
                   " where a.processtype_name = 'Accept' and a.menu_gid = 'AGDMGTCON' and a.application_gid = '" + values.application_gid + "'";
            values.approver_gid = objdbconn.GetExecuteScalar(msSQL);

            //if (values.checker_gid == values.approver_gid)
            //{
            //    msSQL = " update agr_trn_tapplication2contract set checkerletter_flag='Y', checkerupdated_by ='" + employee_gid + "', " +
            //            " checkerupdated_on='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', " +
            //            " sanction_status ='Approved' where application2sanction_gid='" + values.sanction_gid + "'";
            //    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            //    if (mnResult != 0)
            //    {
            //        msSQL = " select  processtypeassign_gid from agr_trn_tprocesstype_assign a " +
            //                " left join agr_trn_tapplication2contract b on a.application_gid = b.application_gid " +
            //                " where application2sanction_gid = '" + values.sanction_gid + "' and menu_gid = '" + getMenuClass.Sanction + "'";
            //        string lsprocesstypeassign_gid = objdbconn.GetExecuteScalar(msSQL);

            //        if (lsprocesstypeassign_gid != "")
            //        {
            //            msSQL = " update agr_trn_tprocesstype_assign set checker_approvalflag='Y', " +
            //                    " checker_approveddate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
            //                    " approver_approvalflag ='Y', approver_approveddate = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
            //                    " overall_approvalstatus='Approved'  where processtypeassign_gid='" + lsprocesstypeassign_gid + "'";
            //            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            //        }

            //        msSQL = " select template_name, template_gid, template_content from agr_trn_tapplication2contract where application2sanction_gid='" + values.sanction_gid + "'";
            //        objODBCDataReader = objdbconn.GetDataReader(msSQL);
            //        if (objODBCDataReader.HasRows == true)
            //        {
            //            values.template_name = objODBCDataReader["template_name"].ToString();
            //            values.template_gid = objODBCDataReader["template_gid"].ToString();
            //            values.template_content = objODBCDataReader["template_content"].ToString();
            //        }
            //        objODBCDataReader.Close();

            //        msGetGid = objcmnfunctions.GetMasterGID("ASLL");
            //        msSQL = "insert into agr_trn_tsanctionapprovallog(" +
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
            msSQL = " update agr_trn_tapplication2contract set checkerletter_flag='Y', sanction_status ='Final Approval Pending', checkerupdated_by ='" + employee_gid + "', " +
                    " checkerupdated_on='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where application2sanction_gid='" + values.sanction_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = " select  processtypeassign_gid from agr_trn_tprocesstype_assign a " +
                        " left join agr_trn_tapplication2contract b on a.application_gid = b.application_gid " +
                        " where application2sanction_gid = '" + values.sanction_gid + "' and menu_gid = '" + getMenuClass.Contract + "'";
                string lsprocesstypeassign_gid = objdbconn.GetExecuteScalar(msSQL);

                if (lsprocesstypeassign_gid != "")
                {
                    msSQL = " update agr_trn_tprocesstype_assign set checker_approvalflag='Y', " +
                            " checker_approveddate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                            " overall_approvalstatus='Approved'  where processtypeassign_gid='" + lsprocesstypeassign_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                msSQL = " select template_name, template_gid, template_content from agr_trn_tapplication2contract where application2sanction_gid='" + values.sanction_gid + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows == true)
                {
                    values.template_name = objODBCDataReader["template_name"].ToString();
                    values.template_gid = objODBCDataReader["template_gid"].ToString();
                    values.template_content = objODBCDataReader["template_content"].ToString();
                }
                objODBCDataReader.Close();

                msGetGid = objcmnfunctions.GetMasterGID("ASSA");
                msSQL = "insert into agr_trn_tsanctionapprovallog(" +
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

                values.message = "Contract Proceeded to Approval Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;
            }
            //}               
        }

        public void DaPostDigitalSignature(string sanction_gid, string employee_gid, cadtemplate_list values)
        {
            msSQL = " SELECT template_content FROM agr_trn_tapplication2contract where application2sanction_gid='" + sanction_gid + "'";
            string lstemplatecontent = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " SELECT document_path FROM agr_mst_tdigitalsignature where employee_gid='" + employee_gid + "'";
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

                values.lsname = "Contract_Letter" + DateTime.Now.ToString("(dd-MM-yyyy HH-mm-ss)") + ".docx";
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

                msSQL = " update agr_trn_tapplication2contract set digitalsignature_flag='Y', makerfile_path='" + values.lspath + "', makerfile_name='" + values.lsname + "'" +
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

        public void DaUpdateCheckerApproval(cadtemplate_list values, string employee_gid)
        {
            if (values.sanction_status == "Approved")
            {
                msSQL = " update agr_trn_tapplication2contract set checkerapproval_flag='Y', sanction_status='" + values.sanction_status + "', checkerapproved_by='" + employee_gid + "',";
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
                msSQL = " update agr_trn_tapplication2contract set checkerapproval_flag='R', sanction_status='" + values.sanction_status + "', checkerapproved_by='" + employee_gid + "',";
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
                msSQL = " select a.processtypeassign_gid,a.application_gid from agr_trn_tprocesstype_assign a " +
                      " left join agr_trn_tapplication2contract b on a.application_gid = b.application_gid " +
                      " where application2sanction_gid = '" + values.sanction_gid + "' and menu_gid ='" + getMenuClass.Contract + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lsprocesstypeassign_gid = objODBCDatareader["processtypeassign_gid"].ToString();
                    lsapplication_gid = objODBCDatareader["application_gid"].ToString();
                }
                objODBCDatareader.Close();

                if (lsprocesstypeassign_gid != "" && values.sanction_status == "Approved")
                {
                    msSQL = " update agr_trn_tprocesstype_assign set approver_approvalflag='Y', " +
                            " approver_approveddate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                            " overall_approvalstatus='Approved'  where processtypeassign_gid='" + lsprocesstypeassign_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update agr_mst_tapplication set sanction_approvalflag='Y' " +
                            " where application_gid='" + lsapplication_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                msSQL = " select template_name, template_gid, template_content from agr_trn_tapplication2contract " +
                        " where application2sanction_gid='" + values.sanction_gid + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows == true)
                {
                    values.template_name = objODBCDataReader["template_name"].ToString();
                    values.template_gid = objODBCDataReader["template_gid"].ToString();
                    values.template_content = objODBCDataReader["template_content"].ToString();
                }
                objODBCDataReader.Close();

                msGetGid = objcmnfunctions.GetMasterGID("ASSA");
                msSQL = "insert into agr_trn_tsanctionapprovallog(" +
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
                    values.message = "Contract Approved Successfully";
                    values.status = true;
                }
                else
                {
                    values.message = "Contract Rejected Successfully";
                    values.status = true;
                }
            }
            else
            {
                values.message = "Error Occrued";
                values.status = false;
            }
        }

        public void DaPusbackToMaker(cadtemplate_list values, string employee_gid)
        {
            msSQL = " update agr_trn_tapplication2contract set sanctionletter_flag='N', checkerpushback_remarks='" + values.pushback_remarks.Replace("'", "") + "'," +
                    " sanction_status='Pushback' where application2sanction_gid='" + values.sanction_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                msSQL = " select  processtypeassign_gid from agr_trn_tprocesstype_assign a " +
                         " left join agr_trn_tapplication2contract b on a.application_gid = b.application_gid " +
                         " where application2sanction_gid = '" + values.sanction_gid + "' and menu_gid = '" + getMenuClass.Contract + "'";
                string lsprocesstypeassign_gid = objdbconn.GetExecuteScalar(msSQL);

                if (lsprocesstypeassign_gid != "")
                {
                    msSQL = " update agr_trn_tprocesstype_assign set maker_approvalflag='N', " +
                            " overall_approvalstatus='Pushback To Maker' where processtypeassign_gid='" + lsprocesstypeassign_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                msSQL = " select template_name, template_gid, template_content from agr_trn_tapplication2contract where application2sanction_gid='" + values.sanction_gid + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows == true)
                {
                    values.template_name = objODBCDataReader["template_name"].ToString();
                    values.template_gid = objODBCDataReader["template_gid"].ToString();
                    values.template_content = objODBCDataReader["template_content"].ToString();
                }
                objODBCDataReader.Close();

                msGetGid = objcmnfunctions.GetMasterGID("ASSA");
                msSQL = "insert into agr_trn_tsanctionapprovallog(" +
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


        public bool DaGetTemplateDetails(mdltemplate values, string sanction_gid)
        {
            msSQL = " select sanctionletter_status, template_name, template_content, makerfile_name, makerfile_path, sanctionletter_flag, checkerapproval_flag," +
                    " checkerletter_flag, checkerpushback_remarks, digitalsignature_flag, date_format(checkerupdated_on, '%d-%m-%Y') as checkerupdated_on," +
                    " concat(c.user_firstname, ' ', c.user_lastname, '/', c.user_code) as checkerupdated_by, date_format(makersubmitted_on, '%d-%m-%Y') as makersubmitted_on," +
                    " concat(e.user_firstname, ' ', e.user_lastname, '/', e.user_code) as makersubmitted_by, " +
                    " f.approver_name as approved_by,date_format(f.approver_approveddate, '%d-%m-%Y') as approved_date " +
                    " from agr_trn_tapplication2contract a " +
                    " left join hrm_mst_temployee b on b.employee_gid = a.checkerupdated_by " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " left join hrm_mst_temployee d on d.employee_gid = a.makersubmitted_by " +
                    " left join adm_mst_tuser e on e.user_gid = d.user_gid " +
                    " left join agr_trn_tprocesstype_assign f on f.application_gid = a.application_gid " +
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

        public bool DaGetTemplateLogDetails(mdltemplate values, string sanctionapprovallog_gid, string sanction_gid)
        {
            msSQL = " select template_name, template_content, sanctionletter_flag, sanction_status" +
                    " from agr_trn_tsanctionapprovallog " +
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
        public void DAGetAppSanctionSummary(string application_gid, MdlMstCAD values, string employee_gid)
        {
            msSQL = " select a.application2sanction_gid,a.contract_id, a.sanction_refno,a.submitedtoapproval_status, date_format(a.sanction_date,'%d-%m-%Y') as sanction_date, a.application_name," +
                    " date_format(b.cccompleted_date, '%d-%m-%Y %h:%i %p') as ccapproved_date, b.application_no, b.application_gid, sanctionto_name," +
                    " date_format(a.created_date,'%d-%m-%Y %H:%i %p') as created_date, sanction_status,checkerapproval_flag" +
                    " from agr_trn_tapplication2contract a " +
                    " left join agr_mst_tapplication b on b.application_gid = a.application_gid" +
                    " where a.application_gid='" + application_gid + "'  ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getappsanction_list = new List<appsanction_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                string lsemployeegid = "";
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msSQL = " select sanctionacceptlog_gid from agr_trn_tsanctionacceptlog  " +
                   " where  (updated_date = (select max(y.updated_date) from agr_trn_tsanctionacceptlog y where y.application_gid = '" + application_gid + "' ) " +
                   " or  (updated_date is null ) )  and accepted_status ='N' ";
                    string sanctionacceptlog_gid = objdbconn.GetExecuteScalar(msSQL);
                    if (sanctionacceptlog_gid != "")
                    {
                        edit_flag = "Y";

                    }
                    else
                    {
                        edit_flag = "N";
                        msSQL = " select sanctionacceptlog_gid from agr_trn_tsanctionacceptlog  " +
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
                        contract_id = dt["contract_id"].ToString(),
                        edit_flag = edit_flag,
                        lsemployeegid = lsemployeegid

                    });
                    values.appsanction_list = getappsanction_list;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaGetPDFGenerate(string sanction_gid, string employee_gid, cadtemplate_list values)
        {
            try
            {
                msSQL = " SELECT template_content FROM agr_trn_tapplication2contract where application2sanction_gid='" + sanction_gid + "'";
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

                msSQL = " SELECT makerfile_path, makerfile_name FROM agr_trn_tapplication2contract where application2sanction_gid='" + sanction_gid + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows == true)
                {
                    values.lspath = objODBCDataReader["makerfile_path"].ToString();
                    values.lsname = objODBCDataReader["makerfile_name"].ToString();
                }
                objODBCDataReader.Close();

                values.lsname1 = "Contract_Letter" + DateTime.Now.ToString("(dd-MM-yyyy HH-mm-ss)") + ".pdf";
                values.lspath1 = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/ContractLetterGenerationPDF/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname1;
                string cloud_path = lscompany_code + "/" + "Master/ContractLetterGenerationPDF/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname1;
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
      

        public bool DaApprovalCompletedSummary(sanctiondetailsList values, string employee_gid)
        {
            msSQL = " SELECT a.application2sanction_gid,a.sanction_refno,date_format(a.sanction_date,'%d-%m-%Y') as sanction_date, sanction_status, b.application_gid, " +
                   " date_format(b.cccompleted_date, '%d-%m-%Y %h:%i %p') as ccapproved_date, format((sanction_amount),2) as sanction_amount,b.customer_name,checkerapproval_flag, a.checkerreject_remarks, b.approval_status," +
                   " b.application_no, concat(d.user_code,' / ',d.user_firstname,d.user_lastname) as checkerupdated_by, " +
                   " date_format(a.checkerupdated_on,'%d-%m-%Y %h:%i %p') as checkerupdated_on, sanctionto_name, e.cadgroup_name," +
                   " date_format(e.approver_approveddate,'%d-%m-%Y %h:%i %p') as approver_approveddate, " +
                   " a.sanction_refno,b.customer_urn,b.renewal_flag,b.amendment_flag,b.shortclosing_flag FROM agr_trn_tapplication2contract a " +
                   " LEFT JOIN agr_mst_tapplication b ON a.application_gid = b.application_gid" +
                   " LEFT JOIN hrm_mst_temployee c ON a.checkerupdated_by=c.employee_gid" +
                   " LEFT JOIN adm_mst_tuser d ON c.user_gid=d.user_gid " +
                   " left join agr_trn_tprocesstype_assign e on e.application_gid = a.application_gid " +
                   " left join agr_trn_tsanctionacceptlog f on f.application_gid = a.application_gid " +
                   " where e.menu_gid = 'AGDMGTCON' and (e.maker_gid ='" + employee_gid + "' or e.checker_gid='" + employee_gid + "' or e.approver_gid = '" + employee_gid + "') " +
                   " and e.approver_approvalflag='Y' and (  f.accepted_status is null) " +
                   " and  (f.updated_date is null ) " +
                   " ORDER BY application2sanction_gid DESC ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_sanctiondtl = new List<sanctiondetails>();
            if (dt_datatable.Rows.Count != 0)
            {

                foreach (DataRow dt in dt_datatable.Rows)
                {
                    string lsccgroup_name;
                    string lscadgroup_name;

                    msSQL = "select group_concat(ccgroup_name) as ccgroup_name from agr_mst_tapplication where application_gid='" + dt["application_gid"] + "'";
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


                    // msSQL = " select sanctionacceptlog_gid from agr_trn_tsanctionacceptlog  " +
                    //" where  (updated_date = (select max(y.updated_date) from agr_trn_tsanctionacceptlog y where y.application_gid = '" + dt["application_gid"].ToString() + "' ) " +
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
                        ccgroup_name = lsccgroup_name,
                        cadgroupname = dt["cadgroup_name"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        application_no = dt["application_no"].ToString(),
                        ccapproved_date = dt["ccapproved_date"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        approver_approveddate = dt["approver_approveddate"].ToString(),
                        renewal_flag = dt["renewal_flag"].ToString(),
                        amendment_flag = dt["amendment_flag"].ToString(),
                        shortclosing_flag = dt["shortclosing_flag"].ToString()

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
                   " date_format(b.cccompleted_date, '%d-%m-%Y %h:%i %p') as ccapproved_date, format((sanction_amount),2) as sanction_amount,b.customer_name,checkerapproval_flag, a.checkerreject_remarks, b.approval_status," +
                   " b.application_no, concat(d.user_code,' / ',d.user_firstname,d.user_lastname) as checkerupdated_by, " +
                   " date_format(a.checkerupdated_on,'%d-%m-%Y %h:%i %p') as checkerupdated_on, sanctionto_name, e.cadgroup_name," +
                   " date_format(e.approver_approveddate,'%d-%m-%Y %h:%i %p') as approver_approveddate, " +
                   " a.sanction_refno,b.customer_urn,b.renewal_flag FROM agr_trn_tapplication2contract a " +
                   " LEFT JOIN agr_mst_tapplication b ON a.application_gid = b.application_gid" +
                   " LEFT JOIN hrm_mst_temployee c ON a.checkerupdated_by=c.employee_gid" +
                   " LEFT JOIN adm_mst_tuser d ON c.user_gid=d.user_gid " +
                   " left join agr_trn_tprocesstype_assign e on e.application_gid = a.application_gid " +
                   " left join agr_trn_tsanctionacceptlog f on f.application_gid = a.application_gid " +
                   " where e.menu_gid = 'AGDMGTCON' and (e.maker_gid ='" + employee_gid + "' or e.checker_gid='" + employee_gid + "' or e.approver_gid = '" + employee_gid + "') " +
                   " and e.approver_approvalflag='Y'  and f.accepted_status ='N' " +
                   " and( f.updated_date = (select max(updated_date) from agr_trn_tsanctionacceptlog y where y.application_gid = a.application_gid ) )" +
                   " ORDER BY application2sanction_gid DESC ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_sanctiondtl = new List<sanctiondetails>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    string lsccgroup_name;
                    string lscadgroup_name;

                    msSQL = "select group_concat(ccgroup_name) as ccgroup_name from agr_mst_tapplication where application_gid='" + dt["application_gid"] + "'";
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
                        ccgroup_name = lsccgroup_name,
                        cadgroupname = dt["cadgroup_name"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        application_no = dt["application_no"].ToString(),
                        ccapproved_date = dt["ccapproved_date"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        approver_approveddate = dt["approver_approveddate"].ToString(),
                        renewal_flag = dt["renewal_flag"].ToString(),
                        amendment_flag = dt["amendment_flag"].ToString(),
                        shortclosing_flag = dt["shortclosing_flag"].ToString(),
                        submitedtoapproval_status = dt["submitedtoapproval_status"].ToString()
                    });
                }
                values.sanctiondetails = get_sanctiondtl;
            }
            dt_datatable.Dispose();

            return true;
        }


        public bool DaSanctionAcceptedSummary(sanctiondetailsList values, string employee_gid)
        {
            msSQL = " SELECT a.application2sanction_gid,a.sanction_refno,date_format(a.sanction_date,'%d-%m-%Y') as sanction_date, sanction_status, b.application_gid, " +
                   " date_format(b.cccompleted_date, '%d-%m-%Y %h:%i %p') as ccapproved_date, format((sanction_amount),2) as sanction_amount,b.customer_name,checkerapproval_flag, a.checkerreject_remarks, b.approval_status," +
                   " b.application_no, concat(d.user_code,' / ',d.user_firstname,d.user_lastname) as checkerupdated_by, " +
                   " date_format(a.checkerupdated_on,'%d-%m-%Y %h:%i %p') as checkerupdated_on, sanctionto_name, e.cadgroup_name," +
                   " date_format(e.approver_approveddate,'%d-%m-%Y %h:%i %p') as approver_approveddate, " +
                   " a.sanction_refno,b.customer_urn FROM agr_trn_tapplication2contract a " +
                   " LEFT JOIN agr_mst_tapplication b ON a.application_gid = b.application_gid" +
                   " LEFT JOIN hrm_mst_temployee c ON a.checkerupdated_by=c.employee_gid" +
                   " LEFT JOIN adm_mst_tuser d ON c.user_gid=d.user_gid " +
                   " left join agr_trn_tprocesstype_assign e on e.application_gid = a.application_gid " +
                   " left join agr_trn_tsanctionacceptlog f on f.application_gid = a.application_gid " +
                   " where e.menu_gid = 'AGDMGTCON' and (e.maker_gid ='" + employee_gid + "' or e.checker_gid='" + employee_gid + "' or e.approver_gid = '" + employee_gid + "') " +
                   " and e.approver_approvalflag='Y' and  f.accepted_status ='Y' and  a.accepted_status ='Y'  " +
                   " and( f.updated_date = (select max(updated_date) from agr_trn_tsanctionacceptlog y where y.application_gid = a.application_gid ) )" +
                   " ORDER BY application2sanction_gid DESC ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_sanctiondtl = new List<sanctiondetails>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    string lsccgroup_name;
                    string lscadgroup_name;

                    msSQL = "select group_concat(ccgroup_name) as ccgroup_name from agr_mst_tapplication where application_gid='" + dt["application_gid"] + "'";
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
                        ccgroup_name = lsccgroup_name,
                        cadgroupname = dt["cadgroup_name"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        application_no = dt["application_no"].ToString(),
                        ccapproved_date = dt["ccapproved_date"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        approver_approveddate = dt["approver_approveddate"].ToString(),
                        renewal_flag = dt["renewal_flag"].ToString(),
                        amendment_flag = dt["amendment_flag"].ToString(),
                        shortclosing_flag = dt["shortclosing_flag"].ToString()

                    });
                }
                values.sanctiondetails = get_sanctiondtl;
            }
            dt_datatable.Dispose();

            return true;
        }

        public bool DaPostReUpdateSanction(string employee_gid, cadsanctiondetails values)
        {

            msSQL = " select contractlog_gid,application2sanction_gid, contract_id,  validityfrom_date, validityto_date, contract_date,  " +
                   "  application_gid , created_by,created_date, " +
                   " makerfile_path,makerfile_name,sanctionletter_status,template_content,makersubmitted_by,makersubmitted_on," +
                   " template_name,checkerapproved_by,checkerupdated_on,checkerpushback_remarks,checkerapproval_flag," +
                   " checkerapproved_on,digitalsignature_flag," +
                   " sanctiongenerated_by,sanctiongenerated_on " +
                   " from agr_mst_tcontractupdatelog " +
                   " where created_date = (select max(created_date) from agr_mst_tcontractupdatelog " +
                   " where  application2sanction_gid ='" + values.application2sanction_gid + "')";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows)
            {
                contractlog_gid = objODBCDataReader["contractlog_gid"].ToString();
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
                contract_id = objODBCDataReader["contract_id"].ToString();
                application_gid = objODBCDataReader["application_gid"].ToString();
                if (objODBCDataReader["contract_date"].ToString() != "")
                {
                    contract_date = objODBCDataReader["contract_date"].ToString();
                }
                validityfrom_date = objODBCDataReader["validityfrom_date"].ToString();
                validityto_date = objODBCDataReader["validityto_date"].ToString();

                created_by = objODBCDataReader["created_by"].ToString();
                created_date = objODBCDataReader["created_date"].ToString();
            }
            objODBCDataReader.Close();



           


            msSQL = " update agr_trn_tapplication2contract set " +
                     " contract_date='" + Convert.ToDateTime(values.contract_date).ToString("yyyy-MM-dd") + "'," +                     
                     " resubmitenable_flag='Y'," +
                     " contactpersonemail_address='" + values.contactpersonemail_address + "'," +           
                     " updated_by='" + employee_gid + "'," +
                    " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    " where application2sanction_gid='" + values.application2sanction_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            
            msSQL = " INSERT INTO agr_mst_tcontractupdatelog( " +
                         
                          " application2sanction_gid," +
                           " application_gid ," +
                          " contract_id," +
                          " contract_date," +
                          " validityfrom_date," +
                          " validityto_date," +
                          " newcontract_id," +
                          " newcontract_date," +
                          " newvalidityfrom_date," +
                          " newvalidityto_date," +
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
                          
                          "'" + application2sanction_gid + "'," +
                          "'" + application_gid + "'," +
                          "'" + contract_id + "', " +
                          "'" + contract_date + "', " +
                          "'" + validityfrom_date + "', " +
                          "'" + validityto_date + "', ";

            msSQL += "'" + values.contract_id + "', " +
                     "'" + Convert.ToDateTime(values.contract_date).ToString("yyyy-MM-dd") + "'," +
                     "'" + values.validityfrom_date + "', " +
                     "'" + values.validityto_date + "', ";                     
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

            msSQL = " select  contractdocumentupload_gid from agr_trn_tcontractdocumentupload " +
                    " WHERE application2sanction_gid ='" + values.application2sanction_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                msSQL = " insert into agr_trn_tcontractdocumentuploadlog " +
                        " ( contractdocumentupload_gid, application2sanction_gid, document_name, document_path, created_by, created_date, sacreated_date) " +
                        " select contractdocumentupload_gid, application2sanction_gid, document_name, document_path, created_by, created_date,  @sacreated_date := '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                        " from agr_trn_tcontractdocumentupload " +
                        " WHERE application2sanction_gid ='" + values.application2sanction_gid + "'";
                mnResultapplication2sanctiondoclog = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            objODBCDataReader.Close();


          

         

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

        public void DaSanctionSubmitToApproval(appsanction_list values, string employee_gid)
        {
            msSQL = " select application2sanction_gid from agr_trn_tapplication2contract a " +
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
            msSQL = " update agr_trn_tapplication2contract set submitedtoapproval_status ='Y'" +
                    " where application2sanction_gid='" + values.application2sanction_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                msSQL = " select contractlog_gid,application2sanction_gid, contract_id,  validityfrom_date, validityto_date, contract_date,  " +
                   "  application_gid , created_by,created_date, " +
                   " makerfile_path,makerfile_name,sanctionletter_status,template_content,makersubmitted_by,makersubmitted_on," +
                   " template_name,checkerapproved_by,checkerupdated_on,checkerpushback_remarks,checkerapproval_flag," +
                   " checkerapproved_on,digitalsignature_flag," +
                   " sanctiongenerated_by,sanctiongenerated_on " +
                   " from agr_mst_tcontractupdatelog " +
                   " where created_date = (select max(created_date) from agr_mst_tcontractupdatelog " +
                   " where  application2sanction_gid ='" + values.application2sanction_gid + "')";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows)
                {
                    contractlog_gid = objODBCDataReader["contractlog_gid"].ToString();
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
                    contract_id = objODBCDataReader["contract_id"].ToString();
                    application_gid = objODBCDataReader["application_gid"].ToString();
                    if (objODBCDataReader["contract_date"].ToString() != "")
                    {
                        contract_date = objODBCDataReader["contract_date"].ToString();
                    }
                    validityfrom_date = objODBCDataReader["validityfrom_date"].ToString();
                    validityto_date = objODBCDataReader["validityto_date"].ToString();
                    
                    created_by = objODBCDataReader["created_by"].ToString();
                    created_date = objODBCDataReader["created_date"].ToString();
                }
                objODBCDataReader.Close();


                
                msSQL = " INSERT INTO agr_trn_tsanctionsubmittoapprovallog( " +
                             
                              " application2sanctionlog_gid," +
                              " application2sanction_gid," +
                               " application_gid ," +
                              " contract_id," +
                              " contract_date," +
                              " validityfrom_date," +
                              " validityto_date," +
                             

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
                              "'" + contractlog_gid + "'," +
                              "'" + application2sanction_gid + "'," +
                              "'" + application_gid + "'," +
                              "'" + contract_id + "', " +
                              "'" + contract_date + "', " +
                              "'" + validityfrom_date + "', " +
                              "'" + validityto_date + "', " +                             
                         "'" + created_by + "'," +
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




                msSQL = " select  contractdocumentuploadlog_gid from agr_trn_tcontractdocumentuploadlog " +
                   " WHERE application2sanction_gid ='" + values.application2sanction_gid + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows == true)
                {
                    msSQL = " insert into agr_trn_tsamcontractdocumentuploadlog " +
                             " ( contractdocumentuploadlog_gid, contractdocumentupload_gid, application2sanction_gid, document_name, document_path, created_by, created_date, sacreated_date )" +
                             " select contractdocumentuploadlog_gid, contractdocumentupload_gid, application2sanction_gid, document_name, document_path, created_by, created_date, " +
                             "  @sacreated_date := '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' from agr_trn_tcontractdocumentuploadlog " +
                             " where sacreated_date = (select max(sacreated_date) from agr_trn_tcontractdocumentuploadlog " +
                             " where  application2sanction_gid ='" + values.application2sanction_gid + "')";
                    mnResultapplication2sanctiondoclog = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                objODBCDataReader.Close();









                values.status = true;
                values.message = "Sanction Re-submitted Successfully";


            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }

        }

        //Update Contract

        public void DaUpdateContractDetail(string employee_gid, MdlMstApplicationView values)
        {
            msSQL = " select application2sanction_gid,date_format(contract_date,'%d-%m-%Y') as contract_date from agr_trn_tapplication2contract" +
                  " where application2sanction_gid='" + values.application2sanction_gid + "'";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                application2sanction_gid = objODBCDatareader["application2sanction_gid"].ToString();
                lscontract_date = objODBCDatareader["contract_date"].ToString();
            }
            objODBCDatareader.Close();
            try
            {
                msSQL = " update agr_trn_tapplication2contract set ";


                //if (Convert.ToDateTime(values.contract_date).ToString("yyyy-MM-dd") == "0001-01-01")
                //{

                //}
                //else
                //{
                //    msSQL += " contract_date='" + Convert.ToDateTime(values.contract_date).ToString("yyyy-MM-dd") + "',";
                //}
                if (lscontract_date == values.contract_date)
                {
                }
                else
                {
                    msSQL += " contract_date='" + Convert.ToDateTime(values.contract_date).ToString("yyyy-MM-dd") + "',";
                }

                msSQL += " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where application2sanction_gid='" + values.application2sanction_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    //msGetGid = objcmnfunctions.GetMasterGID("ILUL");

                    msSQL = "Insert into agr_mst_tcontractupdatelog(" +
                   //" contractlog_gid, " +
                   " application2sanction_gid, " +
                   " contract_date," +
                   " updated_by ," +
                   " updated_date)" +
                   " values (" +
                   //"'" + msGetGid + "'," +
                   "'" + values.application2sanction_gid + "'," +
                   "'" + lscontract_date + "'," +
                   "'" + employee_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    values.status = true;
                    values.message = "Contract Details Updated Successfully";

                    msSQL = " update agr_trn_tcontractdocumentupload set application2sanction_gid='" + values.application2sanction_gid + "' " +
                            " where  application2sanction_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Error Occured..";
            }
        }


        public void DaSanctionPopup(string application_gid, appsanction_list values)
        {

            msSQL = " select a.application_gid, a.application2sanction_gid, a.sanction_refno, accepted_status,accepted_reason" +
                    " from agr_trn_tapplication2contract a " +
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
            msSQL = " select application2sanction_gid from agr_trn_tapplication2contract a " +
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

            msSQL = " select application2sanction_gid from agr_trn_tapplication2contract a " +
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




            msSQL = " update agr_trn_tapplication2contract set accepted_status ='" + values.rbo_status + "',";
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
                msSQL += " accepted_reason='" + values.remarks.Replace("'", "") + "'";

            }
            msSQL += " where application2sanction_gid='" + values.application2sanction_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("SASA");

                msSQL = " insert into agr_trn_tsanctionacceptlog (" +
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
                        " FROM agr_trn_tsanctionacceptlog a" +
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

        public bool DaGettempcontractdelete(string employee_gid, string application_gid, result value)
        {

            msSQL = " delete from agr_trn_tcontractdocumentupload where application2sanction_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            //msSQL = " delete from agr_trn_tcontractdocumentupload where application_gid='" + application_gid + "' and application2sanction_gid='" + employee_gid + "'";
            //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
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

        //Customer 360 Contract Summary

        public void DAGetAppContract360Summary(string application_gid, MdlMstCAD values, string employee_gid)
        {
            msSQL = " select a.application2sanction_gid, a.sanction_refno,a.submitedtoapproval_status, date_format(a.sanction_date,'%d-%m-%Y') as sanction_date, a.application_name," +
                    " cccompleted_date, b.application_no, b.application_gid, customer_name," +
                    " date_format(a.created_date,'%d-%m-%Y %H:%i %p') as created_date, sanction_status,checkerapproval_flag" +
                    " from agr_trn_tapplication2contract a " +
                    " left join agr_mst_tapplication b on b.application_gid = a.application_gid" +
                    " where a.application_gid='" + application_gid + "'  ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getappsanction_list = new List<appsanction_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                string lsemployeegid = "";
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msSQL = " select sanctionacceptlog_gid from agr_trn_tsanctionacceptlog  " +
                   " where  (updated_date = (select max(y.updated_date) from agr_trn_tsanctionacceptlog y where y.application_gid = '" + application_gid + "' ) " +
                   " or  (updated_date is null ) )  and accepted_status ='N' ";
                    string sanctionacceptlog_gid = objdbconn.GetExecuteScalar(msSQL);
                    if (sanctionacceptlog_gid != "")
                    {
                        edit_flag = "Y";

                    }
                    else
                    {
                        edit_flag = "N";
                        msSQL = " select sanctionacceptlog_gid from agr_trn_tsanctionacceptlog  " +
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
                        ccapproved_date = dt["cccompleted_date"].ToString(),
                        application_no = dt["application_no"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        sanctionto_name = dt["customer_name"].ToString(),
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

        public bool DaContractRejectedSummary(sanctiondetailsList values, string employee_gid)
        {
            msSQL = " SELECT a.application2sanction_gid,a.sanction_refno,date_format(a.sanction_date,'%d-%m-%Y') as sanction_date, sanction_status, b.application_gid, " +
                   " date_format(b.cccompleted_date, '%d-%m-%Y %h:%i %p') as ccapproved_date, format((sanction_amount),2) as sanction_amount,b.customer_name,checkerapproval_flag, a.checkerreject_remarks, b.approval_status," +
                   " b.application_no, concat(d.user_code,' / ',d.user_firstname,d.user_lastname) as checkerupdated_by, " +
                   " date_format(a.checkerupdated_on,'%d-%m-%Y %h:%i %p') as checkerupdated_on, sanctionto_name, e.cadgroup_name," +
                   " date_format(e.approver_approveddate,'%d-%m-%Y %h:%i %p') as approver_approveddate, " +
                   " a.sanction_refno,b.customer_urn,b.renewal_flag,b.amendment_flag,b.shortclosing_flag FROM agr_trn_tapplication2contract a " +
                   " LEFT JOIN agr_mst_tapplication b ON a.application_gid = b.application_gid" +
                   " LEFT JOIN hrm_mst_temployee c ON a.checkerupdated_by=c.employee_gid" +
                   " LEFT JOIN adm_mst_tuser d ON c.user_gid=d.user_gid " +
                   " left join agr_trn_tprocesstype_assign e on e.application_gid = a.application_gid " +
                   " left join agr_trn_tsanctionacceptlog f on f.application_gid = a.application_gid " +
                   " where e.menu_gid = 'AGDMGTCON' and (e.maker_gid ='" + employee_gid + "' or e.checker_gid='" + employee_gid + "' or e.approver_gid = '" + employee_gid + "') " +
                   " and a.checkerapproval_flag='R'  " +
                   " ORDER BY application2sanction_gid DESC ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_sanctiondtl = new List<sanctiondetails>();
            if (dt_datatable.Rows.Count != 0)
            {

                foreach (DataRow dt in dt_datatable.Rows)
                {
                    string lsccgroup_name;
                    string lscadgroup_name;

                    msSQL = "select group_concat(ccgroup_name) as ccgroup_name from agr_mst_tapplication where application_gid='" + dt["application_gid"] + "'";
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
                        ccgroup_name = lsccgroup_name,
                        cadgroupname = dt["cadgroup_name"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        application_no = dt["application_no"].ToString(),
                        ccapproved_date = dt["ccapproved_date"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        approver_approveddate = dt["approver_approveddate"].ToString(),
                        renewal_flag = dt["renewal_flag"].ToString(),
                        amendment_flag = dt["amendment_flag"].ToString(),
                        shortclosing_flag = dt["shortclosing_flag"].ToString()

                    });
                }
                values.sanctiondetails = get_sanctiondtl;
            }
            dt_datatable.Dispose();

            return true;
        }

        public void DaContractTempClear(string employee_gid, result values)
        {
            //msSQL = "delete from agr_trn_tapplication2contract where application2sanction_gid='" + employee_gid + "'";
            //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from agr_trn_tcontractdocumentupload where application2sanction_gid='" + employee_gid + "'";
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
    }
}