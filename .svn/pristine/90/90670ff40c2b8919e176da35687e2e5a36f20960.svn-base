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
using ems.hbapiconn.Models;

namespace ems.mastersamagro.DataAccess
{
    /// <summary>
    /// This DataAccess will provide access to add single and multiple datas in Buyer Onboard stage.  (Includes overall summary adding of onboarded buyer general, company & individual info & initiate, approve & reject records)
    /// </summary>
    /// <remarks>Written by Sherin Augusta, Premchander.K </remarks>
    public class DaAgrMstBuyerOnboard
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        DataTable dt_datatable, dt_datatable1, dt_datatable2;
        OdbcDataReader objODBCDatareader, objODBCDatareader1;
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        DaAgrCustomerSupplierOnboardMailTriggers objCusotmerMail = new DaAgrCustomerSupplierOnboardMailTriggers();
        HttpPostedFile httpPostedFile;
        string msSQL, msGetGid, msGetDocumentGid, msGetGidpan;
        string lsemployee_name, lsapplication_gid, lsapp_refno;
        string lsmobile_no, lsprimary_status, lswhatsapp_no, lsinstitution2mobileno_gid, lsemail_address, lsinstitution2email_gid;
        string lsaddress_typegid, lsaddress_type, lsaddressline1, lsaddressline2, lslandmark, lstaluka, lspostal_code, lscity, lsdistrict, lsinstitution2branch_gid;
        string lsstate_gid, lsstate, lscountry, lslatitude, lslongitude, lsinstitution2address_gid, lsinstitution_gid, lsgststate_gid, lsgst_state, lsgst_no, lsgst_registered;
        string lsinstitution2licensedtl_gid, lsmobileno, lslicenseexpiry_date, lslicenseissue_date, lslicense_number, lslicensetype_name, lslicensetype_gid, lspath;
        string lsbranch_name, lsbank_address, lscontact_gid, lsmicr_code, lsbankaccount_name, lsbankaccounttype_gid, lsbankaccounttype_name, lsbankaccount_number, lsconfirmbankaccountnumber, lsjoinaccount_status, lsjoinaccount_name, lschequebook_status, lsaccountopen_date, lsbank_name, lsifsc_code;
        string lscreated_by, lsrm_gid, lscreated_date, lsapproved_date, lsapproved_by, lsrm_mail, lsvan_no, lsrm_cc, lscreatedby_name;

        int mnResult, k;
        OdbcDataReader objODBCDataReader;

        public string ls_server, ls_username, ls_password, tomail_id, tomail_id1, tomail_id2, sub, body, employeename, cc_mailid, lsto_mail, employee_reporting_to;
        int ls_port;
        string lstomembers, lsBccmail_id, lsccweb, lsccdb, lscc2members, lsrm_name, lsarn_number, lscustomer_name, lsquery_title, lsquery_description, lsqueryraised_by, lsquery_to, lsqueryraised_time, lsdocument_name;

        string lsapplication_no, lscustomerref_name, lsloanproduct_gid;

        string sToken = string.Empty;
        Random rand = new Random();
        public string[] lsToReceipients;
        public string[] lsCCReceipients;
        public string[] lsBCCReceipients;

        FnSamAgroHBAPIConn objFnSamAgroHBAPIConn = new FnSamAgroHBAPIConn();
        FnSamAgroHBAPIConnEdit objFnSamAgroHBAPIConnEdit = new FnSamAgroHBAPIConnEdit();
        FnSamAgroHBAPIContract objFnSamAgroHBAPIContract = new FnSamAgroHBAPIContract();


        public void DaGetBuyerApprovalPendingSummary(string employee_gid, MdlMstOnboardApplicationlist values)
        {
                msSQL = " select application_gid,application_no,customerref_name as customer_name,customer_urn,vertical_name,onboard_approvalstatus,status, query_status, sourced_by, " +
               " applicant_type,a.createdby_name as createdby_name,  " +
             " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,date_format(a.approval_submitteddate,'%d-%m-%Y %h:%i %p') as submittedto_approval " +
             " from agr_mst_tbyronboard a " +
             " where a.created_by='" + employee_gid + "' and ( a.SASubmitted_flag is null or a.SASubmitted_flag = 'Y') " +
            " and onboard_approvalstatus='" + OnboardAppStatus.Pending + "' order by application_gid desc ";

           // msSQL = " select application_gid,application_no,customerref_name as customer_name,customer_urn,vertical_name,onboard_approvalstatus,status, query_status, " +
            //        " applicant_type,a.createdby_name as createdby_name,  " +
             //       " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,date_format(a.approval_submitteddate,'%d-%m-%Y %h:%i %p') as submittedto_approval " +
              //      " from agr_mst_tbyronboard a " +
              //      " where a.created_by='" + employee_gid + "' " +
               //     " and onboard_approvalstatus='" + OnboardAppStatus.Pending + "' order by application_gid desc ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<onboardapplicationdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getapplicationadd_list.Add(new onboardapplicationdtl
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical_name = dt["vertical_name"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["createdby_name"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        onboard_approvalstatus = dt["onboard_approvalstatus"].ToString(),
                        onboard_applicationstatus = dt["status"].ToString(),
                        applicant_type = dt["applicant_type"].ToString(),
                        submittedto_approval = dt["submittedto_approval"].ToString(),
                        query_status = dt["query_status"].ToString(),
                        sourced_by = dt["sourced_by"].ToString(),
                    });

                }
            }
            values.onboardapplicationdtl = getapplicationadd_list;
            dt_datatable.Dispose();
            values.status = true;
        }

        public void DaGetBuyerApprovedSummary(string employee_gid, MdlMstOnboardApplicationlist values)
        {
            msSQL = " select application_gid,application_no,customerref_name as customer_name,customer_urn,vertical_name,onboard_approvalstatus,status," +
                    " applicant_type,a.createdby_name as createdby_name,onboard_approvedbyname,date_format(a.onboard_approveddate,'%d-%m-%Y %h:%i %p') as onboard_approveddate, " +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,date_format(a.approval_submitteddate,'%d-%m-%Y %h:%i %p') as submittedto_approval,initiated_by, " +
                    " virtualaccount_number,date_format(a.initiated_date,'%d-%m-%Y %h:%i %p') as initiated_date, lgltag_status, createdby_name " +
                    " from agr_mst_tbyronboard a " +
                    " where a.created_by='" + employee_gid + "' " +
                    " and onboard_approvalstatus='" + OnboardAppStatus.Approved + "' order by application_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<onboardapplicationdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getapplicationadd_list.Add(new onboardapplicationdtl
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical_name = dt["vertical_name"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["createdby_name"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        onboard_approvalstatus = dt["onboard_approvalstatus"].ToString(),
                        onboard_applicationstatus = dt["status"].ToString(),
                        applicant_type = dt["applicant_type"].ToString(),
                        submittedto_approval = dt["submittedto_approval"].ToString(),
                        onboard_approvedby = dt["onboard_approvedbyname"].ToString(),
                        onboard_approveddate = dt["onboard_approveddate"].ToString(),
                        application_initiateddate = dt["initiated_date"].ToString(),
                        application_initiatedby = dt["initiated_by"].ToString(),
                        virtualaccount_number = dt["virtualaccount_number"].ToString(),
                        lgltag_status = dt["lgltag_status"].ToString(),
                        submitted_by = dt["createdby_name"].ToString(),
                    });

                }
            }
            values.onboardapplicationdtl = getapplicationadd_list;
            dt_datatable.Dispose();
            values.status = true;
        }

        public void DaGetBuyerRejectedSummary(string FromRM, string employee_gid, MdlMstOnboardApplicationlist values)
        {
            msSQL = " select application_gid,application_no,customerref_name as customer_name,customer_urn,vertical_name,onboard_approvalstatus,status," +
                    " applicant_type,a.createdby_name as createdby_name,onboard_approvedbyname,date_format(a.onboard_approveddate,'%d-%m-%Y %h:%i %p') as onboard_approveddate, " +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,date_format(a.approval_submitteddate,'%d-%m-%Y %h:%i %p') as submittedto_approval " +
                    " from agr_mst_tbyronboard a ";
            if (FromRM == "Y")
            {
                msSQL += " where a.created_by ='" + employee_gid + "' " +
                         " and onboard_approvalstatus='" + OnboardAppStatus.Rejected + "' order by application_gid desc ";
            }
            else
            {
                msSQL += " where onboard_approvalstatus='" + OnboardAppStatus.Rejected + "' order by application_gid desc ";
            }
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<onboardapplicationdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getapplicationadd_list.Add(new onboardapplicationdtl
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical_name = dt["vertical_name"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["createdby_name"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        onboard_approvalstatus = dt["onboard_approvalstatus"].ToString(),
                        onboard_applicationstatus = dt["status"].ToString(),
                        applicant_type = dt["applicant_type"].ToString(),
                        submittedto_approval = dt["submittedto_approval"].ToString(),
                        onboard_approvedby = dt["onboard_approvedbyname"].ToString(),
                        onboard_approveddate = dt["onboard_approveddate"].ToString(),
                    });

                }
            }
            values.onboardapplicationdtl = getapplicationadd_list;
            dt_datatable.Dispose();
            values.status = true;
        }

        public void DaGetSupplierApprovalPendingSummary(string employee_gid, MdlMstOnboardApplicationlist values)
        {
            //msSQL = " select application_gid,application_no,customerref_name as customer_name,customer_urn,vertical_name,onboard_approvalstatus,status, query_status, " +
            //       " applicant_type,a.createdby_name as createdby_name," +
            //       " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,date_format(a.approval_submitteddate,'%d-%m-%Y %h:%i %p') as submittedto_approval " +
            //      " from agr_mst_tsupronboard a " +
            //      " where a.created_by='" + employee_gid + "' " +
            //       " and onboard_approvalstatus='" + OnboardAppStatus.Pending + "' order by application_gid desc ";

            msSQL = " select application_gid,application_no,customerref_name as customer_name,customer_urn,vertical_name,onboard_approvalstatus,status, query_status,  sourced_by, " +
                    " applicant_type,a.createdby_name as createdby_name," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,date_format(a.approval_submitteddate,'%d-%m-%Y %h:%i %p') as submittedto_approval " +
                    " from agr_mst_tsupronboard a " +
                    " where a.created_by='" + employee_gid + "' and ( a.SASubmitted_flag is null or a.SASubmitted_flag = 'Y') " +
                    " and onboard_approvalstatus='" + OnboardAppStatus.Pending + "' order by application_gid desc ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<onboardapplicationdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getapplicationadd_list.Add(new onboardapplicationdtl
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical_name = dt["vertical_name"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["createdby_name"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        onboard_approvalstatus = dt["onboard_approvalstatus"].ToString(),
                        onboard_applicationstatus = dt["status"].ToString(),
                        applicant_type = dt["applicant_type"].ToString(),
                        submittedto_approval = dt["submittedto_approval"].ToString(),
                        query_status = dt["query_status"].ToString(),
                        sourced_by = dt["sourced_by"].ToString(),
                    });

                }
            }
            values.onboardapplicationdtl = getapplicationadd_list;
            dt_datatable.Dispose();
            values.status = true;
        }

        public void DaGetSupplierApprovedSummary(string employee_gid, MdlMstOnboardApplicationlist values)
        {
            msSQL = " select application_gid,application_no,customerref_name as customer_name,customer_urn,vertical_name,onboard_approvalstatus,status," +
                    " applicant_type,a.createdby_name as createdby_name,onboard_approvedbyname,date_format(a.onboard_approveddate,'%d-%m-%Y %h:%i %p') as onboard_approveddate, " +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,date_format(a.approval_submitteddate,'%d-%m-%Y %h:%i %p') as submittedto_approval, initiated_by, " +
                    " date_format(a.initiated_date,'%d-%m-%Y %h:%i %p') as initiated_date, lgltag_status, createdby_name " +
                    " from agr_mst_tsupronboard a " +
                    " where a.created_by='" + employee_gid + "' " +
                    " and onboard_approvalstatus='" + OnboardAppStatus.Approved + "' order by application_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<onboardapplicationdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getapplicationadd_list.Add(new onboardapplicationdtl
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical_name = dt["vertical_name"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["createdby_name"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        onboard_approvalstatus = dt["onboard_approvalstatus"].ToString(),
                        onboard_applicationstatus = dt["status"].ToString(),
                        applicant_type = dt["applicant_type"].ToString(),
                        submittedto_approval = dt["submittedto_approval"].ToString(),
                        initiated_by = dt["initiated_by"].ToString(),
                        onboard_approvedby = dt["onboard_approvedbyname"].ToString(),
                        onboard_approveddate = dt["onboard_approveddate"].ToString(),
                        application_initiateddate = dt["initiated_date"].ToString(),
                        lgltag_status = dt["lgltag_status"].ToString(),
                        submitted_by = dt["createdby_name"].ToString(),
                    });

                }
            }
            values.onboardapplicationdtl = getapplicationadd_list;
            dt_datatable.Dispose();
            values.status = true;
        }

        public void DaGetSupplierRejectedSummary(string FromRM, string employee_gid, MdlMstOnboardApplicationlist values)
        {
            msSQL = " select application_gid,application_no,customerref_name as customer_name,customer_urn,vertical_name,onboard_approvalstatus,status," +
                    " applicant_type,a.createdby_name as createdby_name,onboard_approvedbyname,date_format(a.onboard_approveddate,'%d-%m-%Y %h:%i %p') as onboard_approveddate, " +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,date_format(a.approval_submitteddate,'%d-%m-%Y %h:%i %p') as submittedto_approval " +
                    " from agr_mst_tsupronboard a ";
            if (FromRM == "Y")
            {
                msSQL += " where a.created_by ='" + employee_gid + "' " +
                         " and onboard_approvalstatus='" + OnboardAppStatus.Rejected + "' order by application_gid desc ";
            }
            else
            {
                msSQL += " where onboard_approvalstatus='" + OnboardAppStatus.Rejected + "' order by application_gid desc ";
            }
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<onboardapplicationdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getapplicationadd_list.Add(new onboardapplicationdtl
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical_name = dt["vertical_name"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["createdby_name"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        onboard_approvalstatus = dt["onboard_approvalstatus"].ToString(),
                        onboard_applicationstatus = dt["status"].ToString(),
                        applicant_type = dt["applicant_type"].ToString(),
                        submittedto_approval = dt["submittedto_approval"].ToString(),
                        onboard_approvedby = dt["onboard_approvedbyname"].ToString(),
                        onboard_approveddate = dt["onboard_approveddate"].ToString(),
                    });

                }
            }
            values.onboardapplicationdtl = getapplicationadd_list;
            dt_datatable.Dispose();
            values.status = true;
        }

        public void DaGetRMApprovalCountDetail(string employee_gid, MdlOnboardApprovalCountdtl values)
        {
            msSQL = " select (select count(*) from agr_mst_tbyronboard where created_by = '" + employee_gid + "' " +
                    " and onboard_approvalstatus = '" + OnboardAppStatus.Pending + "') as BuyerApprovalPending, " +
                    " (select count(*) from agr_mst_tbyronboard where created_by = '" + employee_gid + "' and onboard_approvalstatus = '" + OnboardAppStatus.Approved + "') as BuyerApproved, " +
                    " (select count(*) from agr_mst_tsupronboard where created_by = '" + employee_gid + "' and onboard_approvalstatus = '" + OnboardAppStatus.Pending + "') as SupplierApprovalPending," +
                    " (select count(*) from agr_mst_tsupronboard where created_by = '" + employee_gid + "' and onboard_approvalstatus = '" + OnboardAppStatus.Approved + "') as SupplierApproved; ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.BuyerApprovalPending = objODBCDatareader["BuyerApprovalPending"].ToString();
                values.BuyerApproved = objODBCDatareader["BuyerApproved"].ToString();
                values.SupplierApprovalPending = objODBCDatareader["SupplierApprovalPending"].ToString();
                values.SupplierApproved = objODBCDatareader["SupplierApproved"].ToString();
            }
            objODBCDatareader.Close();
            values.status = true;
        }

        public void DaGetApproverPendingCountDetail(string employee_gid, MdlOnboardApprovalCountdtl values)
        {
            msSQL = " select (select count(*) from agr_mst_tbyronboard where status='Completed' " +
                     " and approval_submittedflag='Y' and onboard_approvalstatus = '" + OnboardAppStatus.Pending + "') as BuyerApprovalPending, " +
                     " (select count(*) from agr_mst_tsupronboard where status='Completed' and approval_submittedflag='Y' and onboard_approvalstatus = '" + OnboardAppStatus.Pending + "') as SupplierApprovalPending ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.BuyerApprovalPending = objODBCDatareader["BuyerApprovalPending"].ToString();
                values.SupplierApprovalPending = objODBCDatareader["SupplierApprovalPending"].ToString();
            }
            objODBCDatareader.Close();
            values.status = true;
        }

        public void DaGetApproverApprovedCountDetail(string employee_gid, MdlOnboardApprovalCountdtl values)
        {
            msSQL = " select (select count(*) from agr_mst_tbyronboard where status='Completed' " +
                     " and onboard_approvalstatus = '" + OnboardAppStatus.Approved + "') as BuyerApproved, " +
                     " (select count(*) from agr_mst_tsupronboard where status='Completed' and onboard_approvalstatus = '" + OnboardAppStatus.Approved + "') as SupplierApproved ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.BuyerApproved = objODBCDatareader["BuyerApproved"].ToString();
                values.SupplierApproved = objODBCDatareader["SupplierApproved"].ToString();
            }
            objODBCDatareader.Close();
            values.status = true;
        }


        public void DaGetRejectedCountDetail(string FromRM, string employee_gid, MdlOnboardApprovalCountdtl values)
        {
            if (FromRM == "Y")
            {
                msSQL = " select (select count(*) from agr_mst_tbyronboard where created_by = '" + employee_gid + "' and onboard_approvalstatus = '" + OnboardAppStatus.Rejected + "') as BuyerRejected, " +
                        " (select count(*) from agr_mst_tsupronboard where created_by = '" + employee_gid + "' and onboard_approvalstatus = '" + OnboardAppStatus.Rejected + "') as SupplierRejected ";
            }
            else
            {
                msSQL = " select (select count(*) from agr_mst_tbyronboard where onboard_approvalstatus = '" + OnboardAppStatus.Rejected + "') as BuyerRejected, " +
                        " (select count(*) from agr_mst_tsupronboard where onboard_approvalstatus = '" + OnboardAppStatus.Rejected + "') as SupplierRejected ";
            }
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.BuyerRejected = objODBCDatareader["BuyerRejected"].ToString();
                values.SupplierRejected = objODBCDatareader["SupplierRejected"].ToString();
            }
            objODBCDatareader.Close();
            values.status = true;
        }

        public void DaGetBuyerOnboardingApprovalPending(string employee_gid, MdlMstOnboardApplicationlist values)
        {
            msSQL = " select application_gid,application_no,customerref_name as customer_name,customer_urn,vertical_name,onboard_approvalstatus,status," +
                    " applicant_type,a.createdby_name as createdby_name," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,date_format(a.approval_submitteddate,'%d-%m-%Y %h:%i %p') as submittedto_approval " +
                    " from agr_mst_tbyronboard a " +
                    " where a.status='Completed'  and approval_submittedflag='Y' and onboard_approvalstatus='" + OnboardAppStatus.Pending + "' order by application_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<onboardapplicationdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getapplicationadd_list.Add(new onboardapplicationdtl
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical_name = dt["vertical_name"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["createdby_name"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        onboard_approvalstatus = dt["onboard_approvalstatus"].ToString(),
                        onboard_applicationstatus = dt["status"].ToString(),
                        applicant_type = dt["applicant_type"].ToString(),
                        submittedto_approval = dt["submittedto_approval"].ToString(),
                    });

                }
            }
            values.onboardapplicationdtl = getapplicationadd_list;
            dt_datatable.Dispose();
            values.status = true;
        }
        public void DaGetSuprOnboardingApprovalPending(string employee_gid, MdlMstOnboardApplicationlist values)
        {
            msSQL = " select application_gid,application_no,customerref_name as customer_name,customer_urn,vertical_name,onboard_approvalstatus,status," +
                    " applicant_type,a.createdby_name as createdby_name," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,date_format(a.approval_submitteddate,'%d-%m-%Y %h:%i %p') as submittedto_approval " +
                    " from agr_mst_tsupronboard a " +
                    " where a.status='Completed' and approval_submittedflag='Y' and onboard_approvalstatus='" + OnboardAppStatus.Pending + "' order by application_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<onboardapplicationdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getapplicationadd_list.Add(new onboardapplicationdtl
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical_name = dt["vertical_name"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["createdby_name"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        onboard_approvalstatus = dt["onboard_approvalstatus"].ToString(),
                        onboard_applicationstatus = dt["status"].ToString(),
                        applicant_type = dt["applicant_type"].ToString(),
                        submittedto_approval = dt["submittedto_approval"].ToString(),
                    });

                }
            }
            values.onboardapplicationdtl = getapplicationadd_list;
            dt_datatable.Dispose();
            values.status = true;
        }

        public void DaGetBuyerOnboardApprovedSummary(string employee_gid, MdlMstOnboardApplicationlist values)
        {
            msSQL = " select a.application_gid,a.application_no,a.customerref_name as customer_name,a.customer_urn,a.vertical_name,a.onboard_approvalstatus,a.status," +
                    " a.applicant_type,a.createdby_name as createdby_name,a.onboard_approvedbyname,date_format(a.onboard_approveddate,'%d-%m-%Y %h:%i %p') as onboard_approveddate, " +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,date_format(a.approval_submitteddate,'%d-%m-%Y %h:%i %p') as submittedto_approval, " +
                    " a.virtualaccount_number,date_format(a.initiated_date,'%d-%m-%Y %h:%i %p') as initiated_date, a.initiated_by, a.lgltag_status, " +
                    " case when b.application_gid is not null then b.urn when c.application_gid is not null then c.urn else '' end as erp_id,a.vacreation_status" +
                    " from agr_mst_tbyronboard a " +
                    " left join agr_mst_tbyronboard2institution b on (a.application_gid = b.application_gid and b.stakeholder_type = 'Applicant')" +
                    " left join agr_mst_tbyronboardcontact c on (a.application_gid = c.application_gid and c.stakeholder_type = 'Applicant')" +
                    " where onboard_approvalstatus='" + OnboardAppStatus.Approved + "' order by application_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<onboardapplicationdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getapplicationadd_list.Add(new onboardapplicationdtl
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical_name = dt["vertical_name"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["createdby_name"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        onboard_approvalstatus = dt["onboard_approvalstatus"].ToString(),
                        onboard_applicationstatus = dt["status"].ToString(),
                        applicant_type = dt["applicant_type"].ToString(),
                        submittedto_approval = dt["submittedto_approval"].ToString(),
                        onboard_approvedby = dt["onboard_approvedbyname"].ToString(),
                        onboard_approveddate = dt["onboard_approveddate"].ToString(),
                        application_initiateddate = dt["initiated_date"].ToString(),
                        application_initiatedby = dt["initiated_by"].ToString(),
                        virtualaccount_number = dt["virtualaccount_number"].ToString(),
                        lgltag_status = dt["lgltag_status"].ToString(),
                        erp_id = dt["erp_id"].ToString(),
                        vacreation_status = dt["vacreation_status"].ToString()

                    });

                }

            }
            values.onboardapplicationdtl = getapplicationadd_list;
            dt_datatable.Dispose();
            values.status = true;
        }

        public void DaGetSupplierOnboardApprovedSummary(string employee_gid, MdlMstOnboardApplicationlist values)
        {
            msSQL = " select a.application_gid,a.application_no,a.customerref_name as customer_name,a.customer_urn,a.vertical_name,a.onboard_approvalstatus,a.status," +
                    " a.applicant_type,a.createdby_name as createdby_name,a.onboard_approvedbyname,date_format(a.onboard_approveddate,'%d-%m-%Y %h:%i %p') as onboard_approveddate, " +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,date_format(a.approval_submitteddate,'%d-%m-%Y %h:%i %p') as submittedto_approval, " +
                    " date_format(a.initiated_date,'%d-%m-%Y %h:%i %p') as initiated_date, a.initiated_by, a.lgltag_status," +
                    " case when b.application_gid is not null then b.urn when c.application_gid is not null then c.urn else '' end as erp_id" +
                    " from agr_mst_tsupronboard a " +
                    " left join agr_mst_tsupronboard2institution b on (a.application_gid = b.application_gid and b.stakeholder_type = 'Applicant')" +
                    " left join agr_mst_tsupronboardcontact c on (a.application_gid = c.application_gid and c.stakeholder_type = 'Applicant')" +
                    " where onboard_approvalstatus='" + OnboardAppStatus.Approved + "' order by application_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<onboardapplicationdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getapplicationadd_list.Add(new onboardapplicationdtl
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical_name = dt["vertical_name"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["createdby_name"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        onboard_approvalstatus = dt["onboard_approvalstatus"].ToString(),
                        onboard_applicationstatus = dt["status"].ToString(),
                        applicant_type = dt["applicant_type"].ToString(),
                        submittedto_approval = dt["submittedto_approval"].ToString(),
                        onboard_approvedby = dt["onboard_approvedbyname"].ToString(),
                        onboard_approveddate = dt["onboard_approveddate"].ToString(),
                        application_initiateddate = dt["initiated_date"].ToString(),
                        application_initiatedby = dt["initiated_by"].ToString(),
                        lgltag_status = dt["lgltag_status"].ToString(),
                        erp_id = dt["erp_id"].ToString()
                    });

                }
            }
            values.onboardapplicationdtl = getapplicationadd_list;
            dt_datatable.Dispose();
            values.status = true;
        }

        // Approve and Reject Flow

        public void DaPostBuyerOnboardApproval(string employee_gid, MdlOnboardApproval values)
        {
            msSQL = " select institution_gid from agr_mst_tbyronboard2institution a " +
                " where stakeholder_type = 'Applicant' and a.application_gid = '" + values.application_gid + "'";
            objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader1.HasRows == true)
            {
                values.status = true;
            }
            else
            {
                msSQL = " select contact_gid from agr_mst_tbyronboardcontact a " +
                        " where stakeholder_type = 'Applicant' and a.application_gid = '" + values.application_gid + "'";
                objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader1.HasRows == true)
                {
                    values.status = true;
                }
                else
                {
                    values.message = "Kindly Add the Applicant for the Application";
                    values.status = false;
                    return;
                }
                objODBCDatareader1.Close();
            }
            objODBCDatareader1.Close();



            msSQL = " select institution_gid from agr_mst_tbyronboard2institution a " +
                               " where a.application_gid = '" + values.application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msSQL = " select institution_gid from agr_mst_tbyronboard2institution a " +
                            " where institution_gid = '" + dt["institution_gid"].ToString() + "' and a.application_gid = '" + values.application_gid + "' and institution_status ='Incomplete'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        values.message = "Institution details having Incomplete record. Kindly, Complete it.";
                        values.status = false;
                        return;
                    }
                    else
                    {
                        values.status = true;

                    }
                    objODBCDatareader.Close();
                }
            }
            dt_datatable.Dispose();

            msSQL = " select contact_gid from agr_mst_tbyronboardcontact a " +
                    " where a.application_gid = '" + values.application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msSQL = " select contact_gid from agr_mst_tbyronboardcontact a " +
                            " where contact_gid = '" + dt["contact_gid"].ToString() + "' and a.application_gid = '" + values.application_gid + "' and contact_status  ='Incomplete'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        values.message = "Individual details having Incomplete record. Kindly, Complete it.";
                        values.status = false;
                        return;
                    }
                    else
                    {
                        values.status = true;

                    }
                    objODBCDatareader.Close();
                }
            }
            dt_datatable.Dispose();
            msSQL = " select concat(a.user_firstname,' ',a.user_lastname,' / ',a.user_code) as approvalperson_name from adm_mst_tuser a " +
                    " left join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                    " where b.employee_gid = '" + employee_gid + "'";
            string lsapprovalperson_name = objdbconn.GetExecuteScalar(msSQL);

            if (values.approval_status == "Approved")
            {
                msSQL = " update agr_mst_tbyronboard set onboard_approvalstatus='" + OnboardAppStatus.Approved + "', " +
                        " onboard_approvalremarks='" + values.approval_remarks.Replace("'", "") + "'," +
                        " onboard_approvedby='" + employee_gid + "'," +
                        " onboard_approvedbyname='" + lsapprovalperson_name + "'," +
                        " onboard_approveddate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                        " where application_gid='" + values.application_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    string msGETRef = objcmnfunctions.GetMasterGID("BYRG");
                    msGETRef = msGETRef.Replace("BYRG", "");
                    lsapp_refno = $"{msGETRef:00000}";
                    lsapp_refno = "2" + lsapp_refno;

                    //Virtual Account Details
                    string msGETVirtualRef = objcmnfunctions.GetMasterGID("BVAN");
                    msGETVirtualRef = msGETVirtualRef.Replace("BVAN", "");
                    string lsapp_virtualno = $"{msGETVirtualRef:000000}";
                    lsapp_virtualno = "SAMAG2" + lsapp_virtualno;

                    //string lsapp_virtualno = "";

                    msSQL = " update agr_mst_tbyronboard set application_no='" + lsapp_refno + "', " +
                            " virtualaccount_number='" + lsapp_virtualno + "'," +
                            " customerbank_name='SAMUNNATI AGRO SOLUTIONS PVT LTD'," +
                            " branch_name='IDFC First Bank, R.K. Salai, Chennai'," +
                            " ifsc_code='IDFB0080101'," +
                            " micr_code='600751001'" +
                            " where application_gid = '" + values.application_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                    msSQL = " select institution_gid from agr_mst_tbyronboard2institution" +
                  " where application_gid = '" + values.application_gid + "' and stakeholder_type='Applicant'";
                    lsinstitution_gid = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = " select contact_gid from agr_mst_tbyronboardcontact" +
                       " where application_gid = '" + values.application_gid + "' and stakeholder_type='Applicant'";
                    lscontact_gid = objdbconn.GetExecuteScalar(msSQL);

                    if (ConfigurationManager.AppSettings["sysSamagroHyperbrdigeAPIEnable"].ToString() == "Yes")
                    {
                        HttpContext ctx = HttpContext.Current;

                        Thread t = new Thread(new ThreadStart(() =>
                        {
                            HttpContext.Current = ctx;
                            if (!String.IsNullOrEmpty(lsinstitution_gid))
                            {
                                objFnSamAgroHBAPIConn.PostBuyerInstitutionHBAPI(values.application_gid, lsapprovalperson_name);
                            }
                            else if (!String.IsNullOrEmpty(lscontact_gid))
                            {
                                objFnSamAgroHBAPIConn.PostBuyerContactHBAPI(values.application_gid, lsapprovalperson_name);
                            }

                        }));

                        t.Start();
                    }



                    values.status = true;
                    values.message = "Buyer Onboarding Approved Successfully!";


                    k = 1;

                    msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                    objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDataReader.HasRows == true)
                    {
                        ls_server = objODBCDataReader["pop_server"].ToString();
                        ls_port = Convert.ToInt32(objODBCDataReader["pop_port"]);
                        ls_username = objODBCDataReader["pop_username"].ToString();
                        ls_password = objODBCDataReader["pop_password"].ToString();
                    }
                    objODBCDataReader.Close();

                    msSQL = " select a.application_no, a.customerref_name, a.virtualaccount_number, a.created_by, concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as onboard_approvedby,   " +
                        " date_format(a.onboard_approveddate,'%d-%m-%Y %h:%i %p') as onboard_approveddate, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date" +
                        " from agr_mst_tbyronboard a" +
                         " left join hrm_mst_temployee d on a.onboard_approvedby = d.employee_gid" +
                             " left join adm_mst_tuser e on e.user_gid = d.user_gid " +
                           " where a.application_gid='" + values.application_gid + "'";
                    objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDataReader.HasRows == true)
                    {
                        lsapplication_no = objODBCDataReader["application_no"].ToString();
                        lscustomerref_name = objODBCDataReader["customerref_name"].ToString();
                        lsapproved_by = objODBCDataReader["onboard_approvedby"].ToString();
                        lsapproved_date = objODBCDataReader["onboard_approveddate"].ToString();
                        lscreated_by = objODBCDataReader["created_by"].ToString();
                        lscreated_date = objODBCDataReader["created_date"].ToString();
                        lsvan_no = objODBCDataReader["virtualaccount_number"].ToString();

                    }
                    objODBCDataReader.Close();

                    msSQL = " select concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as employee_name " +
                     " from hrm_mst_temployee a" +
                     " left join adm_mst_tuser b on a.user_gid=b.user_gid" +
                     " where a.employee_gid='" + lscreated_by + "'";
                    lscreatedby_name = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee " +
                             " where employee_gid in ('" + lscreated_by.Replace(",", "', '") + "')";
                    lsto_mail = objdbconn.GetExecuteScalar(msSQL);

                    lsccweb = ConfigurationManager.AppSettings["Approvedbyrcc"].ToString();

                    //cc_mailid = lsrm_cc + "," + lsccweb;
                    cc_mailid = lsccweb;

                    sub = " VAN has been generated for (" + HttpUtility.HtmlEncode(lscustomerref_name)+ ") ";

                    body = body + " &nbsp&nbsp Dear Team,<br>";
                    body = body + "<br>";
                    body = body + " &nbsp&nbsp Greetings,<br>";
                    body = body + "<br>";
                    body = body + "&nbsp&nbsp Please find the details on the Customer Virtual Account Number, generated for the Customer \"" + HttpUtility.HtmlEncode(lscustomerref_name) + "\" <br>";
                    body = body + "<br>";
                    body = body + "&nbsp&nbsp <b>Customer VAN:</b> " + lsvan_no + "<br>";
                    body = body + "<br />";
                    body = body + "&nbsp&nbsp <b>Customer Name:</b> " + HttpUtility.HtmlEncode(lscustomerref_name)+ "<br>";
                    body = body + "<br />";
                    body = body + "&nbsp&nbsp <b>Customer ID:</b> " + lsapplication_no + "<br>";
                    body = body + "<br />";
                    body = body + "&nbsp&nbsp <b>Creator Name:</b> " + HttpUtility.HtmlEncode(lscreatedby_name)+ "<br>";
                    body = body + "<br />";
                    body = body + "&nbsp&nbsp <b>Onboarded On:</b> " + lsapproved_date + "<br>";
                    body = body + "<br />";
                    body = body + "&nbsp&nbsp Thanks & Regards, <br>";
                    body = body + "<br />";
                    body = body + "&nbsp&nbsp Team PMG-Contract Management <br> ";
                    body = body + "<br>";


                    MailMessage message = new MailMessage();
                    SmtpClient smtp = new SmtpClient();
                    message.From = new MailAddress(ls_username);
                    //message.To.Add(new MailAddress(lsto_mail));


                    lsBccmail_id = ConfigurationManager.AppSettings["ApprovedbyrBcc"].ToString();

                    if (lsBccmail_id != null & lsBccmail_id != string.Empty & lsBccmail_id != "")
                    {
                        lsBCCReceipients = lsBccmail_id.Split(',');
                        if (lsBccmail_id.Length == 0)
                        {
                            message.Bcc.Add(new MailAddress(lsBccmail_id));
                        }
                        else
                        {
                            foreach (string BCCEmail in lsBCCReceipients)
                            {
                                message.Bcc.Add(new MailAddress(BCCEmail)); //Adding Multiple BCC email Id
                            }
                        }
                    }

                    // lsto_mail = ConfigurationManager.AppSettings["ApprovedbyrTo"].ToString();

                    if (lsto_mail != null & lsto_mail != string.Empty & lsto_mail != "")

                    {
                        lsToReceipients = lsto_mail.Split(',');
                        if (lsto_mail.Length == 0)
                        {
                            message.To.Add(new MailAddress(lsto_mail));
                        }
                        else
                        {
                            foreach (string ToEmail in lsToReceipients)
                            {
                                message.To.Add(new MailAddress(ToEmail)); //Adding Multiple To email Id
                            }
                        }
                    }

                    //cc_mailid = ConfigurationManager.AppSettings["Mail2PMG"].ToString();

                    if (cc_mailid != null & cc_mailid != string.Empty & cc_mailid != "")
                    {
                        lsCCReceipients = cc_mailid.Split(',');
                        if (cc_mailid.Length == 0)
                        {
                            message.CC.Add(new MailAddress(cc_mailid));
                        }
                        else
                        {
                            foreach (string CCEmail in lsCCReceipients)
                            {
                                message.CC.Add(new MailAddress(CCEmail)); //Adding Multiple CC email Id
                            }
                        }
                    }

                    message.Subject = sub;
                    message.IsBodyHtml = true; //to make message body as html  
                    message.Body = body;
                    smtp.Port = ls_port;
                    smtp.Host = ls_server; //for gmail host  
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Send(message);

                    values.status = true;

                    bool customer_mail_status = objCusotmerMail.DaonboardedMail(values.application_gid, lsapproved_date, lsto_mail, lscreatedby_name, lscustomerref_name, lsapplication_no);


                }



                else
                {
                    values.status = false;
                    values.message = "Error Occured!";


                }
                


            }
            else
            {
                msSQL = " update agr_mst_tbyronboard set onboard_approvalstatus='" + OnboardAppStatus.Rejected + "', " +
                        " onboard_approvalremarks='" + values.approval_remarks.Replace("'", "") + "'," +
                        " onboard_approvedby='" + employee_gid + "'," +
                        " onboard_approvedbyname='" + lsapprovalperson_name + "'," +
                        " onboard_approveddate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                        " where application_gid='" + values.application_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult == 1)
                {
                    values.status = true;
                    values.message = "Buyer Onboarding Rejected Successfully!";

                    k = 1;

                    msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                    objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDataReader.HasRows == true)
                    {
                        ls_server = objODBCDataReader["pop_server"].ToString();
                        ls_port = Convert.ToInt32(objODBCDataReader["pop_port"]);
                        ls_username = objODBCDataReader["pop_username"].ToString();
                        ls_password = objODBCDataReader["pop_password"].ToString();
                    }
                    objODBCDataReader.Close();

                    msSQL = " select a.application_no, a.customerref_name, a.created_by,concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as onboard_approvedby,   " +
                        " date_format(a.onboard_approveddate,'%d-%m-%Y %h:%i %p') as onboard_approveddate, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date" +
                         " from agr_mst_tbyronboard a" +
                         " left join hrm_mst_temployee d on a.onboard_approvedby = d.employee_gid" +
                             " left join adm_mst_tuser e on e.user_gid = d.user_gid " +
                           " where a.application_gid='" + values.application_gid + "'";
                    objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDataReader.HasRows == true)
                    {
                        lsapplication_no = objODBCDataReader["application_no"].ToString();
                        lscustomerref_name = objODBCDataReader["customerref_name"].ToString();
                        lsapproved_by = objODBCDataReader["onboard_approvedby"].ToString();
                        lsapproved_date = objODBCDataReader["onboard_approveddate"].ToString();
                        lscreated_by = objODBCDataReader["created_by"].ToString();
                        lscreated_date = objODBCDataReader["created_date"].ToString();

                    }
                    objODBCDataReader.Close();

                    msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee " +
                            " where employee_gid in ('" + lscreated_by.Replace(",", "', '") + "')";
                    lsto_mail = objdbconn.GetExecuteScalar(msSQL);


                    sub = " Buyer Onboard Application Approval ";
                    body = body + "<br />";
                    body = body + " &nbsp&nbsp Dear Sir/Madam,<br />";
                    body = body + "<br />";
                    body = body + "&nbsp&nbsp The below Buyer has been rejected. Below are the details:<br />";
                    body = body + "<br />";
                    body = body + "&nbsp&nbsp <b>Buyer name:</b> " + HttpUtility.HtmlEncode(lscustomerref_name )+ "<br />";
                    body = body + "<br />";
                    //body = body + "&nbsp&nbsp <b>Buyer id:</b> " + lsapplication_no + "<br />";
                    //body = body + "<br />";
                    body = body + "&nbsp&nbsp <b>Rejected by:</b> " + HttpUtility.HtmlEncode(lsapproved_by)+ "<br />";
                    body = body + "<br />";
                    body = body + "&nbsp&nbsp <b>Rejected date:</b> " + lsapproved_date + "<br />";
                    body = body + "<br />";
                    body = body + "&nbsp&nbsp <b>Pathway :</b> <br />";
                    body = body + "<br />";
                    body = body + "&nbsp&nbsp Login " + ConfigurationManager.AppSettings["livedomain_url"].ToString() + " > SamAgro > Buyer / Supplier Approval <br />";
                    body = body + "<br />";
                    body = body + "&nbsp&nbsp This is a system generated mail, do not reply.<br /> ";
                    body = body + "<br />";

                    MailMessage message = new MailMessage();
                    SmtpClient smtp = new SmtpClient();
                    message.From = new MailAddress(ls_username);
                    //message.To.Add(new MailAddress(lsto_mail));


                    lsBccmail_id = ConfigurationManager.AppSettings["SamagroOnboardBccMail"].ToString();

                    if (lsBccmail_id != null & lsBccmail_id != string.Empty & lsBccmail_id != "")
                    {
                        lsBCCReceipients = lsBccmail_id.Split(',');
                        if (lsBccmail_id.Length == 0)
                        {
                            message.Bcc.Add(new MailAddress(lsBccmail_id));
                        }
                        else
                        {
                            foreach (string BCCEmail in lsBCCReceipients)
                            {
                                message.Bcc.Add(new MailAddress(BCCEmail)); //Adding Multiple BCC email Id
                            }
                        }
                    }

                    //lsto_mail = ConfigurationManager.AppSettings["Mail2PMG"].ToString();

                    if (lsto_mail != null & lsto_mail != string.Empty & lsto_mail != "")
                    {
                        lsToReceipients = lsto_mail.Split(',');
                        if (lsto_mail.Length == 0)
                        {
                            message.To.Add(new MailAddress(lsto_mail));
                        }
                        else
                        {
                            foreach (string ToEmail in lsToReceipients)
                            {
                                message.To.Add(new MailAddress(ToEmail)); //Adding Multiple To email Id
                            }
                        }
                    }

                    cc_mailid = ConfigurationManager.AppSettings["Mail2PMG"].ToString();

                    if (cc_mailid != null & cc_mailid != string.Empty & cc_mailid != "")
                    {
                        lsCCReceipients = cc_mailid.Split(',');
                        if (cc_mailid.Length == 0)
                        {
                            message.CC.Add(new MailAddress(cc_mailid));
                        }
                        else
                        {
                            foreach (string CCEmail in lsCCReceipients)
                            {
                                message.CC.Add(new MailAddress(CCEmail)); //Adding Multiple CC email Id
                            }
                        }
                    }

                    message.Subject = sub;
                    message.IsBodyHtml = true; //to make message body as html  
                    message.Body = body;
                    smtp.Port = ls_port;
                    smtp.Host = ls_server; //for gmail host  
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Send(message);

                    values.status = true;

                }
                else
                {
                    values.status = true;
                    values.message = "Error Occured!";
                }
            }
        }

        public void DaPostInitiateBuyerApplication(string employee_gid, MdlOnboardApproval values)

        {

            msSQL = " select a.created_by as rm_gid, concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by from agr_mst_tbyronboard a" +
                " left join hrm_mst_temployee b on b.employee_gid = a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
            " where a.application_gid = '" + values.application_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lscreated_by = objODBCDatareader["created_by"].ToString();
                lsrm_gid = objODBCDatareader["rm_gid"].ToString();
            }
            objODBCDatareader.Close();
            string lscreditgroup_gid = "", lscreditgroup_name = "";
            if (values.onboarding_status == "Direct")
            {
                msSQL = " SELECT creditgroup_name,creditmapping_gid from ocs_mst_tcreditmapping where creditgroup_name='" + getAutoApprovalClass.CreditGroupName + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lscreditgroup_gid = objODBCDatareader["creditmapping_gid"].ToString();
                    lscreditgroup_name = objODBCDatareader["creditgroup_name"].ToString();
                }
                objODBCDatareader.Close();
            }

            string msGetApplicationGID = objcmnfunctions.GetMasterGID("APPC");

            msSQL = " insert into agr_mst_tapplication (buyeronboard_gid,application_gid, application_no, customer_urn, customer_name, entity_gid, " +
                           " entity_name, vertical_gid, vertical_name, verticaltaggs_gid, verticaltaggs_name, constitution_gid, constitution_name," +
                           " businessunit_gid, businessunit_name, sa_status, sa_id, sa_name, baselocation_gid, baselocation_name, " +
                           " cluster_gid, cluster_name, region_gid, region_name, zone_gid, zone_name, relationshipmanager_name, " +
                           " relationshipmanager_gid, drm_gid, drm_name, clustermanager_gid, clustermanager_name, zonalhead_name, " +
                           " zonalhead_gid, regionalhead_name, regionalhead_gid, businesshead_name, businesshead_gid, creditmanager_name, " +
                           " creditmanager_gid, zonalriskmanager_name, zonalriskmanager_gid, riskmanager_gid, riskmanager_name, headriskmonitoring_gid, " +
                           " headriskmonitoring_name, created_date, created_by, updated_date, updated_by, vernacular_language, " +
                           " vernacularlanguage_gid, contactpersonfirst_name, contactpersonmiddle_name, contactpersonlast_name," +
                           " designation_gid, designation_type, landline_no, social_capital, trade_capital, overalllimit_amount, " +
                           " validityoveralllimit_year, validityoveralllimit_month, validityoveralllimit_days, validityfrom_date, " +
                           " validityto_date, calculationoveralllimit_validity, principalfrequency_name, principalfrequency_gid, " +
                           " interestfrequency_name, interestfrequency_gid, interest_status, moratorium_status, moratorium_type," +
                           " moratorium_startdate, moratorium_enddate, enduse_purpose, processing_fee, processing_collectiontype," +
                           " doc_charges, doccharge_collectiontype, fieldvisit_charge, fieldvisit_collectiontype, adhoc_fee, " +
                           " adhoc_collectiontype, life_insurance, lifeinsurance_collectiontype, acct_insurance, total_collect, " +
                           " total_deduct, status, dateofsurvey, objevtiveoffpo, majorcrops, alternativeincomesource, " +
                           " overallfporating, overallfpograde, recommendation, fpo_acscore, numnerofaactive_fig," +
                           " existinglending_directandindirect, outstandingportfolio_directandindirect, par90_managedbyonlyinstitution_direct," +
                           " nonnegotiableconditions_met, institution_directandindrectborrowing, totaldisbursements_otherlenders, " +
                           " saname_gid, economical_flag, productcharge_flag, applicant_type, customerref_name, customerref_no, " +
                           " productcharges_status, approval_status, mobile_no, email_address, approval_flag, gradingdraft_flag, " +
                           " hypothecation_flag, submitted_by, submitted_date, ccsubmit_flag, ccsubmitted_by, ccsubmitted_date," +
                           " meeting_status, region, ccgroup_name, cancel_remark, mom_description, momapproval_flag, mom_flag, " +
                           " momupdated_by, momupdated_date, momdocumentupload_flag, cc_remarks, cccompleted_date, cam_content," +
                           " camgenerated_date, camgenerated_by, headapproval_status, document_name, document_path," +
                           " headapproval_date, creditgroup_gid, creditgroup_name, creditgroup_status, credithead_gid, " +
                           " creditnationalmanager_gid, creditregionalmanager_gid, credithead_name, " +
                           " creditnationalmanager_name, creditregionalmanager_name, remarks, creditassigned_by, " +
                           " creditassigned_date, creditheadapproval_status, creditheadapproval_date, process_type, " +
                           " processtype_remarks, processupdated_by, processupdated_date, program_gid, program_name," +
                           " docchecklist_makerflag, docchecklist_checkerflag, docchecklist_approvalflag, " +
                           " cctocredit_reason, cadtocc_reason, cadtocredit_reason, cccompleted_flag, " +
                           " hierarchyupdated_flag, product_gid, product_name, sector_name, category_name, variety_gid," +
                           " variety_name, botanical_name, alternative_name, pslcompleted_flag, pslupdated_date," +
                           " pslupdated_by, pslcompleteremarks, sanction_approvalflag, " +
                           " scheduled_by, scheduled_date, cccompleted_by, onboarding_status, " +
                           "  productdesk_flag, productdesk_gid, productquery_status, refapplication_gid)" +
                           " select application_gid,'" + msGetApplicationGID + "', '', customer_urn, customer_name, entity_gid," +
                           " entity_name, vertical_gid, vertical_name, verticaltaggs_gid, verticaltaggs_name, constitution_gid, constitution_name," +
                           " businessunit_gid, businessunit_name, sa_status, sa_id, sa_name, baselocation_gid, baselocation_name," +
                           " cluster_gid, cluster_name, region_gid, region_name, zone_gid, zone_name, " +
                           " '" + lscreated_by + "', '" + lsrm_gid + "', drm_gid, drm_name, clustermanager_gid, clustermanager_name, zonalhead_name," +
                           //" relationshipmanager_name, relationshipmanager_gid, drm_gid, drm_name, clustermanager_gid, clustermanager_name, zonalhead_name," +
                           " zonalhead_gid, regionalhead_name, regionalhead_gid, businesshead_name, businesshead_gid, creditmanager_name," +
                           " creditmanager_gid, zonalriskmanager_name, zonalriskmanager_gid, riskmanager_gid, riskmanager_name, headriskmonitoring_gid," +
                           " headriskmonitoring_name, created_date, created_by, updated_date, updated_by, vernacular_language," +
                           " vernacularlanguage_gid, contactpersonfirst_name, contactpersonmiddle_name, contactpersonlast_name," +
                           " designation_gid, designation_type, landline_no, social_capital, trade_capital, overalllimit_amount," +
                           " validityoveralllimit_year, validityoveralllimit_month, validityoveralllimit_days, validityfrom_date," +
                           " validityto_date, calculationoveralllimit_validity, principalfrequency_name, principalfrequency_gid," +
                           " interestfrequency_name, interestfrequency_gid, interest_status, moratorium_status, moratorium_type," +
                           " moratorium_startdate, moratorium_enddate, enduse_purpose, processing_fee, processing_collectiontype," +
                           " doc_charges, doccharge_collectiontype, fieldvisit_charge, fieldvisit_collectiontype, adhoc_fee," +
                           " adhoc_collectiontype, life_insurance, lifeinsurance_collectiontype, acct_insurance, total_collect," +
                           " total_deduct, 'Incomplete', dateofsurvey, objevtiveoffpo, majorcrops, alternativeincomesource," +
                           " overallfporating, overallfpograde, recommendation, fpo_acscore, numnerofaactive_fig," +
                           " existinglending_directandindirect, outstandingportfolio_directandindirect, par90_managedbyonlyinstitution_direct," +
                           " nonnegotiableconditions_met, institution_directandindrectborrowing, totaldisbursements_otherlenders," +
                           " saname_gid, economical_flag, productcharge_flag, applicant_type, customerref_name, customerref_no," +
                           " productcharges_status, approval_status, mobile_no, email_address, approval_flag, gradingdraft_flag," +
                           " hypothecation_flag, submitted_by, submitted_date, ccsubmit_flag, ccsubmitted_by, ccsubmitted_date," +
                           " meeting_status, region, ccgroup_name, cancel_remark, mom_description, momapproval_flag, mom_flag," +
                           " momupdated_by, momupdated_date, momdocumentupload_flag, cc_remarks, cccompleted_date, cam_content," +
                           " camgenerated_date, camgenerated_by, headapproval_status, document_name, document_path," +
                           " headapproval_date, '" + lscreditgroup_gid + "','" + lscreditgroup_name + "', creditgroup_status, credithead_gid," +
                           " creditnationalmanager_gid, creditregionalmanager_gid, credithead_name," +
                           " creditnationalmanager_name, creditregionalmanager_name, remarks, creditassigned_by," +
                           " creditassigned_date, creditheadapproval_status, creditheadapproval_date, process_type," +
                           " processtype_remarks, processupdated_by, processupdated_date, program_gid, program_name," +
                           " docchecklist_makerflag, docchecklist_checkerflag, docchecklist_approvalflag," +
                           " cctocredit_reason, cadtocc_reason, cadtocredit_reason, cccompleted_flag," +
                           " hierarchyupdated_flag, product_gid, product_name, sector_name, category_name, variety_gid," +
                           " variety_name, botanical_name, alternative_name, pslcompleted_flag, pslupdated_date," +
                           " pslupdated_by, pslcompleteremarks, sanction_approvalflag," +
                           " scheduled_by, scheduled_date, cccompleted_by, '" + values.onboarding_status + "'," +
                           " productdesk_flag, productdesk_gid, productquery_status , '' " +
                           " from agr_mst_tbyronboard where application_gid = '" + values.application_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult == 1)
            {

                msSQL = " select concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as employee_name " +
                      " from hrm_mst_temployee a" +
                      " left join adm_mst_tuser b on a.user_gid=b.user_gid" +
                      " where a.employee_gid='" + employee_gid + "'";
                lsemployee_name = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " update agr_mst_tbyronboard set initiated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', " +
                        " initiated_by='" + lsemployee_name + "'," +
                        " initiated_remarks='" + values.approval_remarks.Replace("'", "") + "' where application_gid = '" + values.application_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " insert into agr_mst_tonboardinitiatedtl(" +
                        " buyeronboard_gid," +
                        " application_gid," +
                        " product_gid, " +
                        " product_name, " +
                        " program_gid, " +
                        " program_name," +
                        " onboarding_status, "+
                        " initiated_remarks," +
                        " created_byname, " +
                            " created_date," +
                            " created_by)" +
                            " VALUES(" +
                            "'" + values.application_gid + "'," +
                            "'" + msGetApplicationGID + "'," +
                            "'" + values.product_gid + "'," +
                            "'" + values.product_name + "'," +
                            "'" + values.program_gid + "'," +
                            "'" + values.program_name + "'," +
                            "'" + values.onboarding_status + "'," +
                            "'" + values.approval_remarks.Replace("'", "") + "'," +
                            "'" + lsemployee_name + "'," +
                            "current_timestamp," +
                            "'" + employee_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " select contact_gid from agr_mst_tbyronboardcontact where application_gid= '" + values.application_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    string msGetInidividualGID = objcmnfunctions.GetMasterGID("CTCT");

                    msSQL = " insert into agr_mst_tcontact (contact_gid,application_gid,application_no,pan_status,pan_no,aadhar_no," +
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
                        "select '" + msGetInidividualGID + "', '" + msGetApplicationGID + "', application_no, pan_status, pan_no, " +
                            " aadhar_no, first_name, last_name, middle_name, mother_lastname,  individual_dob, age, gender_gid, gender_name, " +
                            " designation_gid, designation_name, educationalqualification_gid, educationalqualification_name, main_occupation, " +
                            " annual_income, monthly_income, pep_status, pepverified_date, stakeholdertype_gid, stakeholder_type, user_type, " +
                            " maritalstatus_gid, maritalstatus_name, father_firstname, father_middlename, father_lastname, father_dob, " +
                            " father_age, mother_firstname, mother_middlename, mother_dob, mother_age, spouse_firstname, spouse_middlename, " +
                            " spouse_lastname, spouse_dob, spouse_age, ownershiptype_gid, ownershiptype_name, propertyholder_gid, " +
                            " propertyholder_name, residencetype_gid, residencetype_name, currentresidence_years, branch_distance, " +
                            " bureauname_gid, bureauname_name, bureau_score, bureauscore_date, observations, bureau_response, " +
                            " cicdocument_name, cicdocument_path, created_by, created_date, updated_date, updated_by, " +
                            " contact_status, email_address, mobile_no, incometype_gid, incometype_name, group_gid, group_name, " +
                            " profile, urn_status, urn, fathernominee_status, mothernominee_status, spousenominee_status, othernominee_status, " +
                            " relationshiptype, nomineefirst_name, nominee_middlename, nominee_lastname, nominee_dob, " +
                            " nominee_age, totallandinacres, cultivatedland, previouscrop, prposedcrop, economical_status, " +
                            " social_capital, trade_capital, institution_gid, institution_name, startupasofloansanction_date, " +
                            " occupation_gid, occupation, lineofactivity_gid, lineofactivity, bsrcode_gid, bsrcode, pslcategory_gid, " +
                            " pslcategory, weakersection_gid, weakersection, pslpurpose_gid, pslpurpose, totalsanction_financialinstitution, " +
                            " pslsanction_limit, natureofentity_gid, natureofentity, indulgeinmarketing_activity, plantandmachineryinvestment_gid, " +
                            " plantandmachineryinvestment, turnover_gid, turnover, msmeclassification_gid, msmeclassification, " +
                            " loansanction_date, entityincorporation_date, hq_metropolitancity, clientdtl_gid, client_dtl, " +
                            " economical_flag, psltagging_flag, credit_status from agr_mst_tbyronboardcontact " +
                            " where contact_gid= '" + dt["contact_gid"].ToString() + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                    msSQL = " select contact2email_gid from agr_mst_tbyronboardcontact2email where contact_gid ='" + dt["contact_gid"].ToString() + "'";
                    dt_datatable1 = objdbconn.GetDataTable(msSQL);
                    foreach (DataRow dt1 in dt_datatable1.Rows)
                    {
                        string msGetOnboardGID = objcmnfunctions.GetMasterGID("C2EA");

                        msSQL = " insert into agr_mst_tcontact2email select '" + msGetOnboardGID + "', '" + msGetInidividualGID + "', " +
                                " email_address, primary_status, created_by, created_date, updated_date, updated_by " +
                                " from agr_mst_tbyronboardcontact2email where contact2email_gid= '" + dt1["contact2email_gid"].ToString() + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    dt_datatable1.Dispose();



                    msSQL = " select contact2panform60_gid from agr_mst_tbyronboardcontact2panform60 where contact_gid ='" + dt["contact_gid"].ToString() + "'";
                    dt_datatable1 = objdbconn.GetDataTable(msSQL);
                    foreach (DataRow dt1 in dt_datatable1.Rows)
                    {
                        string msGetOnboardGID = objcmnfunctions.GetMasterGID("CF60");

                        msSQL = " insert into agr_mst_tcontact2panform60 select '" + msGetOnboardGID + "', '" + msGetInidividualGID + "', " +
                                " document_name, document_path, created_by, created_date, updated_date, updated_by " +
                                " from agr_mst_tbyronboardcontact2panform60 where contact2panform60_gid= '" + dt1["contact2panform60_gid"].ToString() + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    dt_datatable1.Dispose();

                    msSQL = " select contact2bankdtl_gid from agr_mst_tbyronboardcontact2bankdtl " +
                           " where contact_gid in ('" + dt["contact_gid"].ToString() + "')";
                    dt_datatable1 = objdbconn.GetDataTable(msSQL);
                    foreach (DataRow dt1 in dt_datatable1.Rows)
                    {
                        string msGetOnboardGID = objcmnfunctions.GetMasterGID("I2BD");

                        msSQL = " insert into agr_mst_tcontact2bankdtl select '" + msGetOnboardGID + "', '" + msGetInidividualGID + "', '" + msGetApplicationGID + "'," +
                                " bankaccount_number, ifsc_code, bank_name, branch_name, micr_code, accountholder_name, accounttype_gid, accounttype_name, " +
                                " joint_account, jointaccountholder_name, chequebookfacility_available, accountopen_date, created_by, created_date, " +
                                " updated_by, updated_date, bank_address, bankaccount_name, bankaccounttype_gid, bankaccounttype_name, confirmbankaccountnumber, " +
                                " joinaccount_status, joinaccount_name, chequebook_status, disbursement_accountstatus, primary_status " +
                                " from agr_mst_tbyronboardcontact2bankdtl where contact2bankdtl_gid= '" + dt1["contact2bankdtl_gid"].ToString() + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    dt_datatable1.Dispose();

                    msSQL = " select contact2mobileno_gid from agr_mst_tbyronboardcontact2mobileno where contact_gid ='" + dt["contact_gid"].ToString() + "'";
                    dt_datatable1 = objdbconn.GetDataTable(msSQL);
                    foreach (DataRow dt1 in dt_datatable1.Rows)
                    {
                        string msGetOnboardGID = objcmnfunctions.GetMasterGID("C2MN");

                        msSQL = " insert into agr_mst_tcontact2mobileno select '" + msGetOnboardGID + "', '" + msGetInidividualGID + "', " +
                                " mobile_no, primary_status, whatsapp_no, landline_no, created_by, created_date, updated_date, updated_by " +
                                " from agr_mst_tbyronboardcontact2mobileno where contact2mobileno_gid= '" + dt1["contact2mobileno_gid"].ToString() + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    dt_datatable1.Dispose();

                    msSQL = " select contact2idproof_gid from agr_mst_tbyronboardcontact2idproof where contact_gid ='" + dt["contact_gid"].ToString() + "'";
                    dt_datatable1 = objdbconn.GetDataTable(msSQL);
                    foreach (DataRow dt1 in dt_datatable1.Rows)
                    {
                        string msGetOnboardGID = objcmnfunctions.GetMasterGID("C2IP");

                        msSQL = " insert into agr_mst_tcontact2idproof select '" + msGetOnboardGID + "', '" + msGetInidividualGID + "', " +
                                " idproof_gid, idproof_name, idproof_no, idproof_dob, file_no, document_name, document_path, " +
                                " created_by, created_date, updated_date, updated_by" +
                                " from agr_mst_tbyronboardcontact2idproof where contact2idproof_gid= '" + dt1["contact2idproof_gid"].ToString() + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    dt_datatable1.Dispose();

                    msSQL = " select contact2address_gid from agr_mst_tbyronboardcontact2address where contact_gid ='" + dt["contact_gid"].ToString() + "'";
                    dt_datatable1 = objdbconn.GetDataTable(msSQL);
                    foreach (DataRow dt1 in dt_datatable1.Rows)
                    {
                        string msGetOnboardGID = objcmnfunctions.GetMasterGID("C2AD");

                        msSQL = " insert into agr_mst_tcontact2address select '" + msGetOnboardGID + "', '" + msGetInidividualGID + "', " +
                                " addresstype_gid, addresstype_name, primary_status, addressline1, addressline2, landmark, postal_code, " +
                                " city, taluka, district, state, country, latitude, longitude, created_by, created_date, updated_date, updated_by " +
                                " from agr_mst_tbyronboardcontact2address where contact2address_gid= '" + dt1["contact2address_gid"].ToString() + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    dt_datatable1.Dispose();

                    msSQL = " select contact2panabsencereason_gid from agr_mst_tbyronboardcontact2panabsencereason where contact_gid ='" + dt["contact_gid"].ToString() + "'";
                    dt_datatable1 = objdbconn.GetDataTable(msSQL);
                    foreach (DataRow dt1 in dt_datatable1.Rows)
                    {
                        string msGetOnboardGID = objcmnfunctions.GetMasterGID("C2PR");

                        msSQL = " insert into agr_mst_tcontact2panabsencereason select '" + msGetOnboardGID + "', '" + msGetInidividualGID + "', " +
                                " panabsencereason, created_by, created_date, updated_date, updated_by " +
                                " from agr_mst_tbyronboardcontact2panabsencereason where contact2panabsencereason_gid= '" + dt1["contact2panabsencereason_gid"].ToString() + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    dt_datatable1.Dispose();

                    msSQL = " select contact2document_gid from agr_mst_tbyronboardcontact2document where contact_gid ='" + dt["contact_gid"].ToString() + "'";
                    dt_datatable1 = objdbconn.GetDataTable(msSQL);
                    foreach (DataRow dt1 in dt_datatable1.Rows)
                    {
                        string msGetOnboardGID = objcmnfunctions.GetMasterGID("C2DO");

                        msSQL = " insert into agr_mst_tcontact2document select '" + msGetOnboardGID + "', '" + msGetInidividualGID + "', " +
                                " individualdocument_gid, document_gid, document_title, document_name, document_path, created_by, created_date, " +
                                " updated_date, updated_by, covenant_type, untagged_type, untagged_by, untagged_date, covenant_periods, " +
                                " covenantperiod_updatedby, covenantperiod_updateddate , documenttype_gid, documenttype_name " +
                                " from agr_mst_tbyronboardcontact2document where contact2document_gid= '" + dt1["contact2document_gid"].ToString() + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    dt_datatable1.Dispose();

                }
                dt_datatable.Dispose();


                msSQL = " select institution_gid from agr_mst_tbyronboard2institution where application_gid= '" + values.application_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    string msGetInstitutionGID = objcmnfunctions.GetMasterGID("APIN");

                    msSQL = " insert into agr_mst_tinstitution (institution_gid,application_gid,application_no,company_name,date_incorporation," +
                        "form60document_name,form60document_path,companypan_no,year_business,month_business,cin_no,official_telephoneno," +
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
                   " sundrydebt_adv,fixed_assets,profit,revenue,incometax_returnsstatus, msme_registration,lei_renewaldate,kin,lglentity_id) " +
                       " select '" + msGetInstitutionGID + "', '" + msGetApplicationGID + "',  application_no,company_name,date_incorporation, " +
                             "form60document_name,form60document_path,companypan_no,year_business,month_business,cin_no,official_telephoneno," +
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
                   " sundrydebt_adv,fixed_assets,profit,revenue,incometax_returnsstatus, msme_registration,lei_renewaldate,kin,lglentity_id" +
                   " from agr_mst_tbyronboard2institution " +
                            " where institution_gid= '" + dt["institution_gid"].ToString() + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " select institution2address_gid from agr_mst_tbyronboardinstitution2address " +
                            " where institution_gid in ('" + dt["institution_gid"].ToString() + "')";
                    dt_datatable1 = objdbconn.GetDataTable(msSQL);
                    foreach (DataRow dt1 in dt_datatable1.Rows)
                    {
                        string msGetOnboardGID = objcmnfunctions.GetMasterGID("IT2A");

                        msSQL = " insert into agr_mst_tinstitution2address select '" + msGetOnboardGID + "', '" + msGetInstitutionGID + "', " +
                                " addresstype_gid, addresstype_name, primary_status, addressline1, addressline2, landmark, postal_code, city, " +
                                " taluka, district, state, country, latitude, longitude, erp_id, created_by, created_date, updated_date, updated_by " +
                                " from agr_mst_tbyronboardinstitution2address where institution2address_gid= '" + dt1["institution2address_gid"].ToString() + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    dt_datatable1.Dispose();

                    msSQL = " select institution2branch_gid from agr_mst_tbyronboardinstitution2branch " +
                          " where institution_gid in ('" + dt["institution_gid"].ToString() + "')";
                    dt_datatable1 = objdbconn.GetDataTable(msSQL);
                    foreach (DataRow dt1 in dt_datatable1.Rows)
                    {
                        string msGetOnboardGID = objcmnfunctions.GetMasterGID("ITGS");

                        msSQL = " insert into agr_mst_tinstitution2branch(institution2branch_gid, institution_gid, gst_registered, gst_state, gst_no, created_by, created_date, updated_date, updated_by, gststate_gid, status, authentication_status, returnfilling_status, verification_status, headoffice_status ) select '" + msGetOnboardGID + "', '" + msGetInstitutionGID + "', " +
                                " gst_registered, gst_state, gst_no, created_by, created_date, updated_date, updated_by, gststate_gid, status, " +
                                " authentication_status, returnfilling_status, verification_status, headoffice_status " +
                                " from agr_mst_tbyronboardinstitution2branch where institution2branch_gid= '" + dt1["institution2branch_gid"].ToString() + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    dt_datatable1.Dispose();

                    msSQL = " select institution2mobileno_gid from agr_mst_tbyronboardinstitution2mobileno " +
                            " where institution_gid in ('" + dt["institution_gid"].ToString() + "')";
                    dt_datatable1 = objdbconn.GetDataTable(msSQL);
                    foreach (DataRow dt1 in dt_datatable1.Rows)
                    {
                        string msGetOnboardGID = objcmnfunctions.GetMasterGID("IT2M");

                        msSQL = " insert into agr_mst_tinstitution2mobileno select '" + msGetOnboardGID + "', '" + msGetInstitutionGID + "', " +
                                " mobile_no, primary_status, whatsapp_no, landline_no, created_by, created_date, updated_date, updated_by " +
                                " from agr_mst_tbyronboardinstitution2mobileno where institution2mobileno_gid= '" + dt1["institution2mobileno_gid"].ToString() + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    dt_datatable1.Dispose();

                    msSQL = " select institution2email_gid from agr_mst_tbyronboardinstitution2email " +
                          " where institution_gid in ('" + dt["institution_gid"].ToString() + "')";
                    dt_datatable1 = objdbconn.GetDataTable(msSQL);
                    foreach (DataRow dt1 in dt_datatable1.Rows)
                    {
                        string msGetOnboardGID = objcmnfunctions.GetMasterGID("IT2E");

                        msSQL = " insert into agr_mst_tinstitution2email select '" + msGetOnboardGID + "', '" + msGetInstitutionGID + "', " +
                                " email_address, primary_status, created_by, created_date, updated_date, updated_by " +
                                " from agr_mst_tbyronboardinstitution2email where institution2email_gid= '" + dt1["institution2email_gid"].ToString() + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    dt_datatable1.Dispose();

                    msSQL = " select institution2ratingdetail_gid from agr_mst_tbyronboardinstitution2ratingdetail " +
                        " where institution_gid in ('" + dt["institution_gid"].ToString() + "')";
                    dt_datatable1 = objdbconn.GetDataTable(msSQL);
                    foreach (DataRow dt1 in dt_datatable1.Rows)
                    {
                        string msGetOnboardGID = objcmnfunctions.GetMasterGID("INRD");

                        msSQL = " insert into agr_mst_tinstitution2ratingdetail select '" + msGetOnboardGID + "', '" + msGetInstitutionGID + "', '" + msGetApplicationGID + "'," +
                                " creditrating_agencygid, creditrating_agencyname, creditrating_gid, creditrating_name, assessed_on, creditrating_link, created_by, created_date, updated_by, updated_date " +
                                " from agr_mst_tbyronboardinstitution2ratingdetail where institution2ratingdetail_gid= '" + dt1["institution2ratingdetail_gid"].ToString() + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    dt_datatable1.Dispose();

                    msSQL = " select institution2bankdtl_gid from agr_mst_tbyronboardinstitution2bankdtl " +
                           " where institution_gid in ('" + dt["institution_gid"].ToString() + "')";
                    dt_datatable1 = objdbconn.GetDataTable(msSQL);
                    foreach (DataRow dt1 in dt_datatable1.Rows)
                    {
                        string msGetOnboardGID = objcmnfunctions.GetMasterGID("I2BD");

                        msSQL = " insert into agr_mst_tinstitution2bankdtl select '" + msGetOnboardGID + "', '" + msGetInstitutionGID + "', '" + msGetApplicationGID + "'," +
                                " bankaccount_number, ifsc_code, bank_name, branch_name, micr_code, accountholder_name, accounttype_gid, accounttype_name, " +
                                " joint_account, jointaccountholder_name, chequebookfacility_available, accountopen_date, created_by, created_date, " +
                                " updated_by, updated_date, bank_address, bankaccount_name, bankaccounttype_gid, bankaccounttype_name, confirmbankaccountnumber, " +
                                " joinaccount_status, joinaccount_name, chequebook_status, disbursement_accountstatus, primary_status " +
                                " from agr_mst_tbyronboardinstitution2bankdtl where institution2bankdtl_gid= '" + dt1["institution2bankdtl_gid"].ToString() + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    dt_datatable1.Dispose();

                    msSQL = " select institution2form60documentupload_gid from agr_mst_tbyronboardinstitution2form60documentupload " +
                       " where institution_gid in ('" + dt["institution_gid"].ToString() + "')";
                    dt_datatable1 = objdbconn.GetDataTable(msSQL);
                    foreach (DataRow dt1 in dt_datatable1.Rows)
                    {
                        string msGetOnboardGID = objcmnfunctions.GetMasterGID("IF6D");

                        msSQL = " insert into agr_mst_tinstitution2form60documentupload select '" + msGetOnboardGID + "', '" + msGetInstitutionGID + "', " +
                                " form60document_name, form60document_path, created_by, created_date, updated_date, updated_by " +
                                " from agr_mst_tbyronboardinstitution2form60documentupload where institution2form60documentupload_gid= '" + dt1["institution2form60documentupload_gid"].ToString() + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    dt_datatable1.Dispose();

                    msSQL = " select institution2licensedtl_gid from agr_mst_tbyronboardinstitution2licensedtl " +
                            " where institution_gid in ('" + dt["institution_gid"].ToString() + "')";
                    dt_datatable1 = objdbconn.GetDataTable(msSQL);
                    foreach (DataRow dt1 in dt_datatable1.Rows)
                    {
                        string msGetOnboardGID = objcmnfunctions.GetMasterGID("IT2L");

                        msSQL = " insert into agr_mst_tinstitution2licensedtl select '" + msGetOnboardGID + "', '" + msGetInstitutionGID + "', " +
                                " licensetype_gid, licensetype_name, license_no, issue_date, expiry_date, created_by, created_date, updated_date, updated_by " +
                                " from agr_mst_tbyronboardinstitution2licensedtl where institution2licensedtl_gid= '" + dt1["institution2licensedtl_gid"].ToString() + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    dt_datatable1.Dispose();

                    msSQL = " select institution2documentupload_gid from agr_mst_tbyronboardinstitution2documentupload " +
                           " where institution_gid in ('" + dt["institution_gid"].ToString() + "')";
                    dt_datatable1 = objdbconn.GetDataTable(msSQL);
                    foreach (DataRow dt1 in dt_datatable1.Rows)
                    {
                        string msGetOnboardGID = objcmnfunctions.GetMasterGID("INDO");

                        msSQL = " insert into agr_mst_tinstitution2documentupload select '" + msGetOnboardGID + "', '" + msGetInstitutionGID + "', " +
                                " document_name, document_path, created_by, created_date, updated_date, updated_by, companydocument_gid, document_title, " +
                                " document_id, covenant_type, untagged_type, untagged_by, untagged_date, covenant_periods, covenantperiod_updatedby, covenantperiod_updateddate, documenttype_gid, documenttype_name " +
                                " from agr_mst_tbyronboardinstitution2documentupload where institution2documentupload_gid= '" + dt1["institution2documentupload_gid"].ToString() + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    dt_datatable1.Dispose();

                }
                dt_datatable.Dispose();

                msSQL = " select application2product_gid from agr_mst_tbyronboard2product where application_gid= '" + values.application_gid + "'";
                dt_datatable1 = objdbconn.GetDataTable(msSQL);
                foreach (DataRow dt1 in dt_datatable1.Rows)
                {
                    string msGetOnboardGID = objcmnfunctions.GetMasterGID("AP2P");

                    msSQL = " insert into agr_mst_tapplication2product " +
                       " (application2product_gid, application_gid, application2loan_gid, product_gid, product_name, variety_gid, variety_name, sector_name, category_name, botanical_name, " +
                        " alternative_name, created_by, created_date, updated_by, updated_date, hsn_code, unitpricevalue_commodity," +
                        " natureformstate_commoditygid, natureformstate_commodity, qualityof_commodity, quantity, uom_gid, uom_name, milestone_applicability, "  +
                        " insurance_applicability, milestonepayment_gid, milestonepayment_name, sa_payout, insurance_availability, insurance_percent, " +
                        " insurance_cost, net_yield, markto_marketvalue, pricereference_source, headingdesc_product, typeofsupply_naturegid, " +
                        " typeofsupply_naturename, sectorclassification_gid, sectorclassification_name, creditperiod_years, creditperiod_months, " +
                        " creditperiod_days, overallcreditperiod_limit, commodity_margin, commoditynet_yield, graceperiod_days, customerpaymenttype_gid, " +
                        " customerpaymenttype_name, maximumpercent_paymentterm, erp_status) " +
                        " select '" + msGetOnboardGID + "', '" + msGetApplicationGID + "', " +
                            " application2loan_gid, product_gid, product_name, variety_gid, variety_name, sector_name, category_name, botanical_name, " +
                            " alternative_name, created_by, created_date, updated_by, updated_date, hsn_code, unitpricevalue_commodity, " +
                            " natureformstate_commoditygid, natureformstate_commodity, qualityof_commodity, quantity, uom_gid, uom_name, " +
                            " milestone_applicability, insurance_applicability, milestonepayment_gid, milestonepayment_name, sa_payout, " +
                            " insurance_availability, insurance_percent, insurance_cost, net_yield, markto_marketvalue, pricereference_source, " +
                            " headingdesc_product, typeofsupply_naturegid, typeofsupply_naturename, sectorclassification_gid, sectorclassification_name, " +
                            " creditperiod_years, creditperiod_months, creditperiod_days, overallcreditperiod_limit, commodity_margin, commoditynet_yield, graceperiod_days, customerpaymenttype_gid, customerpaymenttype_name, maximumpercent_paymentterm, erp_status " +
                            " from agr_mst_tbyronboard2product where application2product_gid= '" + dt1["application2product_gid"].ToString() + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                dt_datatable1.Dispose();

                msSQL = " select application2contact_gid from agr_mst_tbyronboard2contactno where application_gid= '" + values.application_gid + "'";
                dt_datatable1 = objdbconn.GetDataTable(msSQL);
                foreach (DataRow dt1 in dt_datatable1.Rows)
                {
                    string msGetOnboardGID = objcmnfunctions.GetMasterGID("A2CN");

                    msSQL = " insert into agr_mst_tapplication2contactno select '" + msGetOnboardGID + "', '" + msGetApplicationGID + "', " +
                            "  mobile_no, primary_mobileno, whatsapp_mobileno, created_by, created_date, updated_by, updated_date " +
                            " from agr_mst_tbyronboard2contactno where application2contact_gid= '" + dt1["application2contact_gid"].ToString() + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                dt_datatable1.Dispose();

                msSQL = " select application2email_gid from agr_mst_tbyronboard2email where application_gid= '" + values.application_gid + "'";
                dt_datatable1 = objdbconn.GetDataTable(msSQL);
                foreach (DataRow dt1 in dt_datatable1.Rows)
                {
                    string msGetOnboardGID = objcmnfunctions.GetMasterGID("A2EA");

                    msSQL = " insert into agr_mst_tapplication2email select '" + msGetOnboardGID + "', '" + msGetApplicationGID + "', " +
                            " email_address, primary_emailaddress, created_by, created_date, updated_by, updated_date " +
                            " from agr_mst_tbyronboard2email where application2email_gid= '" + dt1["application2email_gid"].ToString() + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                dt_datatable1.Dispose();

                msSQL = " select application2geneticcode_gid from agr_mst_tbyronboard2geneticcode where application_gid= '" + values.application_gid + "'";
                dt_datatable1 = objdbconn.GetDataTable(msSQL);
                foreach (DataRow dt1 in dt_datatable1.Rows)
                {
                    string msGetOnboardGID = objcmnfunctions.GetMasterGID("A2GC");

                    msSQL = " insert into agr_mst_tapplication2geneticcode select '" + msGetOnboardGID + "', '" + msGetApplicationGID + "', " +
                            " geneticcode_gid, geneticcode_name, genetic_status, genetic_remarks, created_by, created_date, updated_by, updated_date " +
                            " from agr_mst_tbyronboard2geneticcode where application2geneticcode_gid= '" + dt1["application2geneticcode_gid"].ToString() + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                dt_datatable1.Dispose();


                msSQL = " select individualdocument_gid, contact2document_gid, contact_gid from agr_mst_tcontact2document where contact_gid in " +
                        " (select contact_gid from agr_mst_tcontact where application_gid='" + msGetApplicationGID + "')";
                dt_datatable2 = objdbconn.GetDataTable(msSQL);
                foreach (DataRow dt in dt_datatable2.Rows)
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
                dt_datatable2.Dispose();

                msSQL = " select contact_gid from agr_mst_tcontact2document  where contact_gid in " +
                        " (select contact_gid from agr_mst_tcontact where application_gid='" + msGetApplicationGID + "') group by contact_gid";
                dt_datatable1 = objdbconn.GetDataTable(msSQL);
                foreach (DataRow dt in dt_datatable1.Rows)
                {
                    DaAgrMstScannedDocument objvalues1 = new DaAgrMstScannedDocument();
                    string lscredit_gid = dt["contact_gid"].ToString();
                    objvalues1.DaGroupDocChecklistinfo(values.application_gid, lscredit_gid, employee_gid);
                }
                dt_datatable1.Dispose();


                msSQL = " select companydocument_gid, institution2documentupload_gid, institution_gid from agr_mst_tinstitution2documentupload  where institution_gid in " +
                       " (select institution_gid from agr_mst_tinstitution where application_gid='" + msGetApplicationGID + "')";
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
                        " (select institution_gid from agr_mst_tinstitution where application_gid='" + msGetApplicationGID + "') group by institution_gid";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    DaAgrMstScannedDocument objvalues = new DaAgrMstScannedDocument();
                    string lscredit_gid = dt["institution_gid"].ToString();
                    objvalues.DaGroupDocChecklistinfo(values.application_gid, lscredit_gid, employee_gid);
                }
                dt_datatable.Dispose();

                msSQL = "update agr_mst_tkycgstsbpan set function_gid='" + msGetApplicationGID + "' where function_gid='" + values.application_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycpanauthentication set function_gid='" + msGetApplicationGID + "' where function_gid='" + values.application_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycpanauthentication set function_gid ='" + msGetApplicationGID + "' where function_gid='" + values.application_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycpanaadhaarlink set function_gid ='" + msGetApplicationGID + "' where function_gid='" + values.application_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycdlauthentication set function_gid ='" + msGetApplicationGID + "' where function_gid='" + values.application_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycepicauthentication set function_gid ='" + msGetApplicationGID + "' where function_gid='" + values.application_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycpassportauthentication set function_gid ='" + msGetApplicationGID + "' where function_gid='" + values.application_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycifscauthentication set function_gid ='" + msGetApplicationGID + "' where function_gid='" + values.application_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycbankaccverification set function_gid ='" + msGetApplicationGID + "' where function_gid='" + values.application_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);



                values.status = true;
                values.message = "Buyer Onboarding Initiated Successfully!";
            }
            else
            {
                values.status = true;
                values.message = "Error Occured!";
            }

        }

        public bool DaPostProductDetailAdd(string employee_gid, MdlMstBuyerOnboardProductDetailAdd values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("AP2P");
            msSQL = " insert into agr_mst_tbyronboard2product (" +
                    " application2product_gid," +
                    " application2loan_gid," +
                    " application_gid," +
                    " product_gid," +
                    " product_name," +
                    " variety_gid," +
                    " variety_name," +
                    " sector_name," +
                    " category_name," +
                    " botanical_name," +
                    " alternative_name," +
                    " hsn_code, " +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "null," +
                    "'" + employee_gid + "'," +
                    "'" + values.product_gid + "'," +
                    "'" + values.product_name + "'," +
                    "'" + values.variety_gid + "'," +
                    "'" + values.variety_name + "'," +
                    "'" + values.sector_name + "'," +
                    "'" + values.category_name + "'," +
                    "'" + values.botanical_name + "'," +
                    "'" + values.alternative_name + "'," +
                    "'" + values.hsn_code + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                values.status = true;
                values.message = "Product Details Added Successfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured";
                return false;
            }
        }

        public void DaGetProductDetailList(string employee_gid, MdlMstBuyerOnboardProductDetailList values)
        {
            msSQL = " select application2product_gid,product_gid,product_name,variety_gid,variety_name,sector_name,category_name,hsn_code, " +
                    " botanical_name,alternative_name from agr_mst_tbyronboard2product where application_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstproduct_list = new List<mstBuyerOnboardproduct_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstproduct_list.Add(new mstBuyerOnboardproduct_list
                    {
                        application2product_gid = (dr_datarow["application2product_gid"].ToString()),
                        product_gid = (dr_datarow["product_gid"].ToString()),
                        product_name = (dr_datarow["product_name"].ToString()),
                        variety_gid = (dr_datarow["variety_gid"].ToString()),
                        variety_name = (dr_datarow["variety_name"].ToString()),
                        sector_name = (dr_datarow["sector_name"].ToString()),
                        category_name = (dr_datarow["category_name"].ToString()),
                        botanical_name = (dr_datarow["botanical_name"].ToString()),
                        alternative_name = (dr_datarow["alternative_name"].ToString()),
                        hsn_code = (dr_datarow["hsn_code"].ToString())
                    });
                }
                values.mstBuyerOnboardproduct_list = getmstproduct_list;
            }
            dt_datatable.Dispose();
        }

        public void DaDeleteProductDetail(string application2product_gid, MdlMstBuyerOnboardProductDetailAdd values, string employee_gid)
        {
            msSQL = "delete from agr_mst_tbyronboard2product where application2product_gid='" + application2product_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                //msSQL = "delete from agr_mst_tappproduct2commoditygststatus where application2product_gid='" + application2product_gid + "'";
                //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                //msSQL = "delete from agr_mst_tappproduct2commoditytradedtl where application2product_gid='" + application2product_gid + "'";
                //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                //msSQL = "delete from agr_mst_tappproduct2commoditydocument where application2product_gid='" + application2product_gid + "'";
                //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                values.message = "Product Details are Deleted Successfully";
                values.status = true;
                msSQL = " select application2product_gid,product_gid,product_name,variety_gid,variety_name,sector_name,category_name,hsn_code," +
                        " botanical_name,alternative_name,application2loan_gid,unitpricevalue_commodity, natureformstate_commodity,qualityof_commodity, " +
                        " quantity,uom_name, headingdesc_product,typeofsupply_naturename,sectorclassification_name " +
                        " from agr_mst_tbyronboard2product where application_gid='" + employee_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmstproduct_list = new List<mstBuyerOnboardproduct_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmstproduct_list.Add(new mstBuyerOnboardproduct_list
                        {
                            application2product_gid = (dr_datarow["application2product_gid"].ToString()),
                            product_gid = (dr_datarow["product_gid"].ToString()),
                            product_name = (dr_datarow["product_name"].ToString()),
                            variety_gid = (dr_datarow["variety_gid"].ToString()),
                            variety_name = (dr_datarow["variety_name"].ToString()),
                            sector_name = (dr_datarow["sector_name"].ToString()),
                            category_name = (dr_datarow["category_name"].ToString()),
                            botanical_name = (dr_datarow["botanical_name"].ToString()),
                            alternative_name = (dr_datarow["alternative_name"].ToString()),
                            application2loan_gid = (dr_datarow["application2loan_gid"].ToString()),
                            hsn_code = (dr_datarow["hsn_code"].ToString()),
                            unitpricevalue_commodity = (dr_datarow["unitpricevalue_commodity"].ToString()),
                            natureformstate_commodity = (dr_datarow["natureformstate_commodity"].ToString()),
                            qualityof_commodity = (dr_datarow["qualityof_commodity"].ToString()),
                            quantity = (dr_datarow["quantity"].ToString()),
                            uom_name = (dr_datarow["uom_name"].ToString()),
                            headingdesc_product = dr_datarow["headingdesc_product"].ToString(),
                            typeofsupply_naturename = dr_datarow["typeofsupply_naturename"].ToString(),
                            sectorclassification_name = dr_datarow["sectorclassification_name"].ToString(),
                        });
                    }
                    values.mstBuyerOnboardproduct_list = getmstproduct_list;
                }
                dt_datatable.Dispose();
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }

        public bool DaPostMobileNo(string employee_gid, MdlMstBuyerOnboardMobileNo values)
        {
            msSQL = "select primary_mobileno from agr_mst_tbyronboard2contactno where primary_mobileno='Yes' and (application_gid='" + employee_gid + "' or" +
               " application_gid='" + values.application_gid + "') ";
            string lsprimary_mobileno = objdbconn.GetExecuteScalar(msSQL);
            if (lsprimary_mobileno == (values.primary_mobileno))
            {

                values.status = false;
                values.message = "Already Primary Mobile Number Added";
                return false;
            }
            msSQL = "select application2contact_gid from agr_mst_tbyronboard2contactno where mobile_no='" + values.mobile_no + "' " +
                " and (application_gid='" + employee_gid + "' or application_gid='" + values.application_gid + "')";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Already This Mobile Number Added";
                return false;
            }

            msGetGid = objcmnfunctions.GetMasterGID("A2CN");
            msSQL = " insert into agr_mst_tbyronboard2contactno(" +
                    " application2contact_gid," +
                    " application_gid," +
                    " mobile_no," +
                    " primary_mobileno," +
                    " whatsapp_mobileno," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + values.mobile_no + "'," +
                    "'" + values.primary_mobileno + "'," +
                    "'" + values.whatsapp_mobileno + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                values.status = true;
                values.message = "Mobile Number Added Successfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured";
                return false;
            }
        }
        public void DaGetAppMobileNoList(string employee_gid, MdlMstBuyerOnboardMobileNo values)
        {
            msSQL = "select mobile_no,application2contact_gid,primary_mobileno,whatsapp_mobileno from agr_mst_tbyronboard2contactno where " +
              " application_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstmobileno_list = new List<mstBuyerOnboardmobileno_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstmobileno_list.Add(new mstBuyerOnboardmobileno_list
                    {
                        application2contact_gid = (dr_datarow["application2contact_gid"].ToString()),
                        mobile_no = (dr_datarow["mobile_no"].ToString()),
                        primary_mobileno = (dr_datarow["primary_mobileno"].ToString()),
                        whatsapp_mobileno = (dr_datarow["whatsapp_mobileno"].ToString()),
                    });
                }
                values.mstBuyerOnboardmobileno_list = getmstmobileno_list;
            }
            dt_datatable.Dispose();
        }
        public void DaDeleteMobileNo(string application2contact_gid, MdlMstBuyerOnboardMobileNo values)
        {
            msSQL = "delete from agr_mst_tbyronboard2contactno where application2contact_gid='" + application2contact_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "Mobile Number Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }
        public bool DaPostEmailAddress(string employee_gid, MdlMstBuyerOnboardEmailAddress values)
        {
            msSQL = "select primary_emailaddress from agr_mst_tbyronboard2email where primary_emailaddress='Yes' and (application_gid='" + employee_gid + "' or application_gid='" + values.application_gid + "')";
            string lsprimary_emailaddress = objdbconn.GetExecuteScalar(msSQL);
            if (lsprimary_emailaddress == (values.primary_emailaddress))
            {

                values.status = false;
                values.message = "Already Primary Email Address Added";
                return false;
            }
            msSQL = "select application2email_gid from agr_mst_tbyronboard2email where email_address='" + values.email_address + "' and (application_gid='" + employee_gid + "' or application_gid='" + values.application_gid + "')";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Already This Email Address Added";
                return false;
            }
            objODBCDatareader.Close();
            msGetGid = objcmnfunctions.GetMasterGID("A2EA");
            msSQL = " insert into agr_mst_tbyronboard2email(" +
                    " application2email_gid," +
                    " application_gid," +
                    " email_address," +
                    " primary_emailaddress," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + values.email_address + "'," +
                    "'" + values.primary_emailaddress + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                values.status = true;
                values.message = "Email Address Added Successfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured";
                return false;
            }
        }

        public void DaGetAppEmailAddressList(string employee_gid, MdlMstBuyerOnboardEmailAddress values)
        {
            msSQL = "select email_address,application2email_gid,primary_emailaddress from agr_mst_tbyronboard2email where " +
              " application_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstemailaddress_list = new List<mstBuyerOnboardemailaddress_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstemailaddress_list.Add(new mstBuyerOnboardemailaddress_list
                    {
                        application2email_gid = (dr_datarow["application2email_gid"].ToString()),
                        email_address = (dr_datarow["email_address"].ToString()),
                        primary_emailaddress = (dr_datarow["primary_emailaddress"].ToString())
                    });
                }
                values.mstBuyerOnboardemailaddress_list = getmstemailaddress_list;
            }
            dt_datatable.Dispose();
            values.status = true;
        }

        public void DaDeleteEmailAddress(string application2email_gid, MdlMstBuyerOnboardEmailAddress values)
        {
            msSQL = "delete from agr_mst_tbyronboard2email where application2email_gid='" + application2email_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "Email Address Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }


        public void DaPostGeneticCode(string employee_gid, MdlMstBuyerOnboardGeneticCode values)
        {

            msSQL = "select geneticcode_gid from agr_mst_tbyronboard2geneticcode where application_gid='" + employee_gid + "' and geneticcode_gid='" + values.geneticcode_gid + "'";
            string lsgenetic_code = objdbconn.GetExecuteScalar(msSQL);
            if (lsgenetic_code == (values.geneticcode_gid))
            {

                values.status = false;
                values.message = "Already Genetic Code Added";
                return;

            }
            msGetGid = objcmnfunctions.GetMasterGID("A2GC");
            msSQL = " insert into agr_mst_tbyronboard2geneticcode(" +
                   " application2geneticcode_gid," +
                   " application_gid," +
                   " geneticcode_gid," +
                   " geneticcode_name," +
                   " genetic_status," +
                   " genetic_remarks," +
                   " created_by," +
                   " created_date)" +
                   " values(" +
                   "'" + msGetGid + "'," +
                   "'" + employee_gid + "'," +
                   "'" + values.geneticcode_gid + "'," +
                   "'" + values.geneticcode_name.Replace("'", " ") + "'," +
                   "'" + values.genetic_status + "'," +
                   "'" + values.genetic_remarks.Replace("'", " ") + "'," +
                   "'" + employee_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Genetic Code details Added successfully";

                msSQL = " select application2geneticcode_gid,geneticcode_gid,geneticcode_name,genetic_status,genetic_remarks" +
                     " from agr_mst_tbyronboard2geneticcode where " +
                     " application_gid='" + employee_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getgenetic_list = new List<mstBuyerOnboardgeneticcode_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getgenetic_list.Add(new mstBuyerOnboardgeneticcode_list
                        {
                            application2geneticcode_gid = (dr_datarow["application2geneticcode_gid"].ToString()),
                            geneticcode_gid = (dr_datarow["geneticcode_gid"].ToString()),
                            geneticcode_name = (dr_datarow["geneticcode_name"].ToString()),
                            genetic_status = (dr_datarow["genetic_status"].ToString()),
                            genetic_remarks = (dr_datarow["genetic_remarks"].ToString()),
                        });
                    }
                    values.mstBuyerOnboardgeneticcode_list = getgenetic_list;
                }
                dt_datatable.Dispose();
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while Adding Genetic Code";
            }
        }

        public void DaDeleteGenetic(string application2geneticcode_gid, MdlMstBuyerOnboardGeneticCode values, string employee_gid)
        {
            msSQL = "delete from agr_mst_tbyronboard2geneticcode where application2geneticcode_gid='" + application2geneticcode_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Genetic Code details Deleted successfully";

                msSQL = " select application2geneticcode_gid,geneticcode_gid,geneticcode_name,genetic_status,genetic_remarks" +
                     " from agr_mst_tbyronboard2geneticcode where " +
                     " application_gid='" + employee_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getgenetic_list = new List<mstBuyerOnboardgeneticcode_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getgenetic_list.Add(new mstBuyerOnboardgeneticcode_list
                        {
                            application2geneticcode_gid = (dr_datarow["application2geneticcode_gid"].ToString()),
                            geneticcode_gid = (dr_datarow["geneticcode_gid"].ToString()),
                            geneticcode_name = (dr_datarow["geneticcode_name"].ToString()),
                            genetic_status = (dr_datarow["genetic_status"].ToString()),
                            genetic_remarks = (dr_datarow["genetic_remarks"].ToString()),
                        });
                    }
                    values.mstBuyerOnboardgeneticcode_list = getgenetic_list;
                }
                dt_datatable.Dispose();
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred while deleting Genetic Code";
            }
        }

        public void DaSubmitGeneralDtl(MdlMstBuyerOnboardApplicationAdd values, string employee_gid)
        {

            msSQL = "select application_gid from tmp_application where employee_gid='" + employee_gid + "'";
            lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);
            if (lsapplication_gid == "" || lsapplication_gid == null)
            {
                lsapplication_gid = values.application_gid;
            }
            msSQL = "select count(*) from ocs_mst_tgeneticcode where status='Y'";
            string lsmastercount = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select count(*) from agr_mst_tbyronboard2geneticcode where application_gid='" + employee_gid + "' or application_gid='" + lsapplication_gid + "'";
            string lsgeneticcount = objdbconn.GetExecuteScalar(msSQL);
            if (lsmastercount == lsgeneticcount)
            {

                msSQL = "select application_gid from agr_mst_tbyronboard2contactno where (application_gid='" + employee_gid + "' or application_gid='" + lsapplication_gid + "') and primary_mobileno='Yes'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == false)
                {
                    objODBCDatareader.Close();
                    values.status = false;
                    values.message = "Kindly Add Primary Mobile No";
                    return;
                }
                else
                {
                    objODBCDatareader.Close();
                }

                msSQL = "select application_gid from agr_mst_tbyronboard2email where (application_gid='" + employee_gid + "' or application_gid='" + lsapplication_gid + "') and primary_emailaddress='Yes'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == false)
                {
                    objODBCDatareader.Close();
                    values.status = false;
                    values.message = "Kindly Add Primary Email Adddress";
                    return;
                }
                else
                {
                    objODBCDatareader.Close();
                }

                msSQL = "select vertical_refno from ocs_mst_tvertical where vertical_gid='" + values.vertical_gid + "'";
                string lsvertical_refno = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select entity_gid from ocs_mst_tvertical where vertical_gid='" + values.vertical_gid + "'";
                string lsentity_gid = objdbconn.GetExecuteScalar(msSQL);

                string lsentity_code = "SA";

                string lsapp_refno = "ARN" + lsentity_code + lsvertical_refno + DateTime.Now.ToString("ddMMyyyy");

                //string msGETRef = objcmnfunctions.GetMasterGID("APP");
                //msGETRef = msGETRef.Replace("APP", "");

                //lsapp_refno = lsapp_refno + msGETRef + "IN01";

                msGetGid = objcmnfunctions.GetMasterGID("BYOG");
                string gsvernacularlanguage_gid = string.Empty;
                string gsvernacular_language = string.Empty;
                if (values.vernacularlanguage_list != null)
                {
                    for (var i = 0; i < values.vernacularlanguage_list.Count; i++)
                    {
                        gsvernacularlanguage_gid += values.vernacularlanguage_list[i].vernacularlanguage_gid + ",";
                        gsvernacular_language += values.vernacularlanguage_list[i].vernacular_language + ",";

                    }
                    gsvernacularlanguage_gid = gsvernacularlanguage_gid.TrimEnd(',');
                    gsvernacular_language = gsvernacular_language.TrimEnd(',');
                }

                msSQL = "select concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as employee_name" +
                       " from hrm_mst_temployee a" +
                       " left join adm_mst_tuser b on a.user_gid=b.user_gid" +
                       " where a.employee_gid='" + employee_gid + "'";
                lsemployee_name = objdbconn.GetExecuteScalar(msSQL);


                msSQL = " insert into agr_mst_tbyronboard(" +
                        " application_gid," +
                        " customer_urn," +
                        " customerref_name," +
                        " vertical_gid," +
                        " vertical_name," +
                        " buyersuppliertype_gid," +
                        " buyersuppliertype_name," +
                        " constitution_gid," +
                        " constitution_name," +
                        " sourced_by, " +
                        " sa_status," +
                        " saname_gid," +
                        " sa_name," +
                        " vernacular_language," +
                        " vernacularlanguage_gid," +
                        " contactpersonfirst_name," +
                        " contactpersonmiddle_name," +
                        " contactpersonlast_name," +
                        " designation_gid," +
                        " designation_type," +
                        " landline_no," +
                        " program_gid," +
                        " program_name," +
                        "createdby_name," +
                        " status," +
                        " created_by," +
                        " created_date) values(" +
                          "'" + msGetGid + "'," +
                            "'" + values.customer_urn + "'," +
                            "'" + values.customer_name.Replace("'", "") + "'," +
                            "'" + values.vertical_gid + "'," +
                            "'" + values.vertical_name + "'," +
                            "'" + values.buyersuppliertype_gid + "'," +
                            "'" + values.buyersuppliertype_name.Replace("'", "\\'") + "'," +
                            "'" + values.constitution_gid + "'," +
                            "'" + values.constitution_name + "'," +
                            " 'RM' ," +
                            "'" + values.sa_status + "'," +
                            "'" + values.saname_gid + "'," +
                            "'" + values.sa_name + "'," +
                            "'" + gsvernacular_language + "'," +
                            "'" + gsvernacularlanguage_gid + "'," +
                            "'" + values.contactpersonfirst_name + "'," +
                            "'" + values.contactpersonmiddle_name + "'," +
                            "'" + values.contactpersonlast_name + "'," +
                            "'" + values.designation_gid + "'," +
                            "'" + values.designation_type + "'," +
                            "'" + values.landline_no + "'," +
                            "'" + values.program_gid + "'," +
                            "'" + values.program_name + "'," +
                            "'" + lsemployee_name + "'," +
                            "'Completed'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            }
            else
            {
                values.message = "Kindly Add all Genetic details";
                values.status = false;
                return;
            }

            if (mnResult != 0)
            {
                values.application_gid = msGetGid;

                msSQL = "update agr_mst_tbyronboard2product set application_gid ='" + msGetGid + "' where application_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tbyronboard2contactno set application_gid='" + msGetGid + "' where application_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    msSQL = "update agr_mst_tbyronboard2email set application_gid='" + msGetGid + "' where application_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnResult != 0)
                    {
                        msSQL = "update agr_mst_tbyronboard2geneticcode set application_gid='" + msGetGid + "' where application_gid='" + employee_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult != 0)
                        {
                            msSQL = "insert into tmp_application(application_gid,employee_gid)values('" + msGetGid + "','" + employee_gid + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            values.application_no = lsapp_refno;

                            values.message = "General Information Saved successfully";
                            values.status = true;
                        }
                        else
                        {
                            values.message = "Error Occured while Saving Information";
                            values.status = false;
                        }
                    }
                    else
                    {
                        values.message = "Error Occured while Saving Information";
                        values.status = false;
                    }
                }
                else
                {
                    values.message = "Error Occured while Saving Information";
                    values.status = false;
                }
            }
            else
            {
                values.message = "Error Occured while Saving Information";
                values.status = false;
                return;
            }
        }

        public bool DaPostInstitutionGST(string employee_gid, MdlMstBuyerOnboardGST values)
        {
            msSQL = "select application_gid from tmp_application where employee_gid='" + employee_gid + "'";
            lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select institution_gid from agr_mst_tbyronboardinstitution2branch where gst_no='" + values.gst_no + "' and institution_gid='" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Already Added";
                return false;
            }

            msGetGid = objcmnfunctions.GetMasterGID("ITGS");
            msSQL = " insert into agr_mst_tbyronboardinstitution2branch(" +
                    " institution2branch_gid," +
                    " institution_gid," +
                    " gst_state," +
                    " gst_no," +
                    " gst_registered," +
                     " headoffice_status, " +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + values.gst_state + "'," +
                    "'" + values.gst_no + "'," +
                    "'" + values.gst_registered + "'," +
                    "'No'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "GST Details Added Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
                return false;
            }
        }

        public bool DaPostInstitutionGSTList(string employee_gid, MdlMstBuyerOnboardGST values)
        {

            InstitutionBuyerOnboardGSTDetails[] GstArray = values.GSTArray;
            string GSTValue, GSTStateCode, GSTState;

            for (int i = 0; i < GstArray.Length; i++)
            {
                GSTValue = GstArray[i].gstinId;
                GSTStateCode = GSTValue.Substring(0, 2);

                msSQL = "select gst_state from agr_mst_tgstcode2state where " +
                       " gst_code='" + GSTStateCode + "'";
                GSTState = objdbconn.GetExecuteScalar(msSQL);

                msGetGid = objcmnfunctions.GetMasterGID("ITGS");
                msSQL = " insert into agr_mst_tbyronboardinstitution2branch(" +
                    " institution2branch_gid," +
                    " institution_gid," +
                    " gst_state," +
                    " gst_no," +
                    " gst_registered," +
                    " headoffice_status, " +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + GSTState + "'," +
                    "'" + GSTValue + "'," +
                    "'" + "Yes" + "'," +
                    "'No'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "GST Details Added Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
                return false;
            }
        }

        public void DaGetInstitutionGSTList(string employee_gid, MdlMstBuyerOnboardGST values)
        {
            msSQL = " select institution2branch_gid,gst_state,gst_no, gst_registered,authentication_status,returnfilling_status,verification_status , headoffice_status from agr_mst_tbyronboardinstitution2branch where institution_gid='" + employee_gid + "' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstgst_list = new List<mstBuyerOnboardgst_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstgst_list.Add(new mstBuyerOnboardgst_list
                    {
                        institution2branch_gid = (dr_datarow["institution2branch_gid"].ToString()),
                        gst_state = (dr_datarow["gst_state"].ToString()),
                        gst_no = (dr_datarow["gst_no"].ToString()),
                        gst_registered = (dr_datarow["gst_registered"].ToString()),
                        authentication_status = (dr_datarow["authentication_status"].ToString()),
                        returnfilling_status = (dr_datarow["returnfilling_status"].ToString()),
                        verification_status = (dr_datarow["verification_status"].ToString()),
                        headoffice_status = (dr_datarow["headoffice_status"].ToString())

                    });
                }
                values.mstBuyerOnboardgst_list = getmstgst_list;
            }
            dt_datatable.Dispose();
        }

        public void DaEditInstitutionGST(string institution2branch_gid, MdlMstBuyerOnboardGST values)
        {
            try
            {
                msSQL = "select gst_state, gst_no, institution_gid, institution2branch_gid, gst_registered" +
                    " from agr_mst_tbyronboardinstitution2branch where institution2branch_gid='" + institution2branch_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.gst_state = objODBCDatareader["gst_state"].ToString();
                    values.gst_no = objODBCDatareader["gst_no"].ToString();
                    values.institution2branch_gid = objODBCDatareader["institution2branch_gid"].ToString();
                    values.institution_gid = objODBCDatareader["institution_gid"].ToString();
                    values.gst_registered = objODBCDatareader["gst_registered"].ToString();
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

        public void DaUpdateInstitutionGST(string employee_gid, MdlMstBuyerOnboardGST values)
        {
            msSQL = "select gst_state, gst_no, gst_registered, institution_gid, institution2branch_gid" +
                " from agr_mst_tbyronboardinstitution2branch where institution2branch_gid='" + values.institution2branch_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsgst_state = objODBCDatareader["gst_state"].ToString();
                lsgst_no = objODBCDatareader["gst_no"].ToString();
                lsinstitution2branch_gid = objODBCDatareader["institution2branch_gid"].ToString();
                lsinstitution_gid = objODBCDatareader["institution_gid"].ToString();
                lsgst_registered = objODBCDatareader["gst_registered"].ToString();
            }
            objODBCDatareader.Close();
            try
            {
                msSQL = " update agr_mst_tbyronboardinstitution2branch set " +
                         " gst_state='" + values.gst_state + "'," +
                         " gst_no='" + values.gst_no + "'," +
                         " gst_registered='" + values.gst_registered + "'," +
                         " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where institution2branch_gid='" + values.institution2branch_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("IGUL");

                    msSQL = "Insert into agr_mst_tbyronboardinstitution2branchupdatelog(" +
                   " institution2gstupdatelog_gid, " +
                   " institution2branch_gid, " +
                   " institution_gid, " +
                   " gst_state," +
                   " gst_no," +
                   " gst_registered," +
                   " created_by," +
                   " created_date)" +
                   " values (" +
                   "'" + msGetGid + "'," +
                   "'" + values.institution2branch_gid + "'," +
                   "'" + values.institution_gid + "'," +
                   "'" + lsgst_state + "'," +
                   "'" + lsgst_no + "'," +
                   "'" + lsgst_registered + "'," +
                   "'" + employee_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    values.status = true;
                    values.message = "GST Updated Successfully";
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Error Occured..";
            }
        }

        public void DaDeleteInstitutionGST(string institution2branch_gid, MdlMstBuyerOnboardGST values)
        {
            msSQL = "delete from agr_mst_tbyronboardinstitution2branch where institution2branch_gid='" + institution2branch_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "delete from agr_mst_tbyronboardinstitution2branchupdatelog where institution2branch_gid='" + institution2branch_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.message = "GST Details Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }

        // Institution Mobile Number

        public bool DaPostInstitutionMobileNo(string employee_gid, MdlMstBuyerOnboardMobileNo values)
        {
            msSQL = "select primary_status from agr_mst_tbyronboardinstitution2mobileno where primary_status='Yes' and (institution_gid='" + employee_gid + "' or institution_gid='" + values.institution_gid + "')";
            string lsprimary_status = objdbconn.GetExecuteScalar(msSQL);
            if (lsprimary_status == (values.primary_status))
            {
                values.status = false;
                values.message = "Already Primary Mobile Number Added";
                return false;
            }

            msSQL = "select institution2mobileno_gid from agr_mst_tbyronboardinstitution2mobileno where mobile_no='" + values.mobile_no + "' and (institution_gid='" + employee_gid + "' or institution_gid='" + values.institution_gid + "')";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Already This Mobile Number Added";
                return false;
            }
            objODBCDatareader.Close();
            msGetGid = objcmnfunctions.GetMasterGID("IT2M");
            msSQL = " insert into agr_mst_tbyronboardinstitution2mobileno(" +
                    " institution2mobileno_gid," +
                    " institution_gid," +
                    " mobile_no," +
                    " primary_status," +
                    " whatsapp_no," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + values.mobile_no + "'," +
                    "'" + values.primary_status + "'," +
                    "'" + values.whatsapp_no + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Mobile Number Added Successfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured";
                return false;
            }
        }

        public void DaGetInstitutionMobileNoList(string employee_gid, MdlMstBuyerOnboardMobileNo values)
        {
            msSQL = "select mobile_no,institution2mobileno_gid,primary_status,whatsapp_no from agr_mst_tbyronboardinstitution2mobileno where " +
              " institution_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstmobileno_list = new List<mstBuyerOnboardmobileno_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstmobileno_list.Add(new mstBuyerOnboardmobileno_list
                    {
                        institution2mobileno_gid = (dr_datarow["institution2mobileno_gid"].ToString()),
                        mobile_no = (dr_datarow["mobile_no"].ToString()),
                        primary_status = (dr_datarow["primary_status"].ToString()),
                        whatsapp_no = (dr_datarow["whatsapp_no"].ToString()),
                    });
                }
                values.mstBuyerOnboardmobileno_list = getmstmobileno_list;
            }
            dt_datatable.Dispose();
        }

        public void DaEditInstitutionMobileNo(string institution2mobileno_gid, MdlMstBuyerOnboardMobileNo values)
        {
            try
            {
                msSQL = " select mobile_no,institution2mobileno_gid,primary_status,whatsapp_no from agr_mst_tbyronboardinstitution2mobileno where " +
                        " institution2mobileno_gid='" + institution2mobileno_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.mobile_no = objODBCDatareader["mobile_no"].ToString();
                    values.primary_status = objODBCDatareader["primary_status"].ToString();
                    values.whatsapp_no = objODBCDatareader["whatsapp_no"].ToString();
                    values.institution2mobileno_gid = objODBCDatareader["institution2mobileno_gid"].ToString();
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

        public void DaUpdateInstitutionMobileNo(string employee_gid, MdlMstBuyerOnboardMobileNo values)
        {
            msSQL = " select mobile_no,institution2mobileno_gid,primary_status,whatsapp_no from agr_mst_tbyronboardinstitution2mobileno where " +
                    " institution2mobileno_gid='" + values.institution2mobileno_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsmobile_no = objODBCDatareader["mobile_no"].ToString();
                lsprimary_status = objODBCDatareader["primary_status"].ToString();
                lswhatsapp_no = objODBCDatareader["whatsapp_no"].ToString();
                lsinstitution2mobileno_gid = objODBCDatareader["institution2mobileno_gid"].ToString();
            }
            objODBCDatareader.Close();
            try
            {
                msSQL = " update agr_mst_tbyronboardinstitution2mobileno set " +
                         " mobile_no='" + values.mobile_no + "'," +
                         " primary_status='" + values.primary_status + "'," +
                         " whatsapp_no='" + values.whatsapp_no + "'," +
                         " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where institution2mobileno_gid='" + values.institution2mobileno_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("IMUL");

                    msSQL = "Insert into agr_mst_tbyronboardinstitution2mobilenoupdatelog(" +
                   " institution2mobilenoupdatelog_gid, " +
                   " institution2mobileno_gid, " +
                   " institution_gid, " +
                   " mobile_no," +
                   " primary_status," +
                   " whatsapp_no," +
                   " created_by," +
                   " created_date)" +
                   " values (" +
                   "'" + msGetGid + "'," +
                   "'" + values.institution2mobileno_gid + "'," +
                   "'" + values.institution_gid + "'," +
                   "'" + lsmobile_no + "'," +
                   "'" + lsprimary_status + "'," +
                   "'" + lswhatsapp_no + "'," +
                   "'" + employee_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    values.status = true;
                    values.message = "Institution Mobile Number Updated Successfully";
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Error Occured..";
            }
        }

        public void DaDeleteInstitutionMobileNo(string institution2mobileno_gid, MdlMstBuyerOnboardMobileNo values)
        {
            msSQL = "delete from agr_mst_tbyronboardinstitution2mobileno where institution2mobileno_gid='" + institution2mobileno_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "delete from agr_mst_tbyronboardinstitution2mobilenoupdatelog where institution2mobileno_gid='" + institution2mobileno_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.message = "Mobile Number Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;
            }
        }

        // Institution Email Address

        public bool DaPostInstitutionEmailAddress(string employee_gid, MdlMstBuyerOnboardEmailAddress values)
        {
            msSQL = "select primary_status from agr_mst_tbyronboardinstitution2email where primary_status='Yes' and (institution_gid='" + employee_gid + "' or institution_gid='" + values.institution_gid + "')";
            string lsprimary_status = objdbconn.GetExecuteScalar(msSQL);
            if (lsprimary_status == (values.primary_status))
            {

                values.status = false;
                values.message = "Already Primary Email Address Added";
                return false;
            }
            msSQL = "select institution2email_gid from agr_mst_tbyronboardinstitution2email where email_address='" + values.email_address + "' and (institution_gid='" + employee_gid + "' or institution_gid='" + values.institution_gid + "')";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Already This Email Address Added";
                return false;
            }
            objODBCDatareader.Close();
            msGetGid = objcmnfunctions.GetMasterGID("IT2E");
            msSQL = " insert into agr_mst_tbyronboardinstitution2email(" +
                    " institution2email_gid," +
                    " institution_gid," +
                    " email_address," +
                    " primary_status," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + values.email_address + "'," +
                    "'" + values.primary_status + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                values.status = true;
                values.message = "Email Address Added Successfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured";
                return false;
            }
        }

        public void DaGetInstitutionEmailAddressList(string employee_gid, MdlMstBuyerOnboardEmailAddress values)
        {
            msSQL = " select email_address,institution2email_gid,primary_status from agr_mst_tbyronboardinstitution2email where institution_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstemailaddress_list = new List<mstBuyerOnboardemailaddress_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstemailaddress_list.Add(new mstBuyerOnboardemailaddress_list
                    {
                        institution2email_gid = (dr_datarow["institution2email_gid"].ToString()),
                        email_address = (dr_datarow["email_address"].ToString()),
                        primary_status = (dr_datarow["primary_status"].ToString())
                    });
                }
                values.mstBuyerOnboardemailaddress_list = getmstemailaddress_list;
            }
            dt_datatable.Dispose();
        }

        public void DaEditInstitutionEmailAddress(string institution2email_gid, MdlMstBuyerOnboardEmailAddress values)
        {
            try
            {
                msSQL = " select email_address,institution2email_gid,primary_status from agr_mst_tbyronboardinstitution2email where " +
                        " institution2email_gid='" + institution2email_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.email_address = objODBCDatareader["email_address"].ToString();
                    values.primary_status = objODBCDatareader["primary_status"].ToString();
                    values.institution2email_gid = objODBCDatareader["institution2email_gid"].ToString();
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

        public void DaUpdateInstitutionEmailAddress(string employee_gid, MdlMstBuyerOnboardEmailAddress values)
        {
            msSQL = " select email_address,institution2email_gid,primary_status from agr_mst_tbyronboardinstitution2email where " +
                        " institution2email_gid='" + values.institution2email_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsemail_address = objODBCDatareader["email_address"].ToString();
                lsprimary_status = objODBCDatareader["primary_status"].ToString();
                lsinstitution2email_gid = objODBCDatareader["institution2email_gid"].ToString();
            }
            objODBCDatareader.Close();
            try
            {
                msSQL = " update agr_mst_tbyronboardinstitution2email set " +
                         " email_address='" + values.email_address + "'," +
                         " primary_status='" + values.primary_status + "'," +
                         " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where institution2email_gid='" + values.institution2email_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("IEUL");

                    msSQL = "Insert into agr_mst_tbyronboardinstitution2emailupdatelog(" +
                   " institution2emailaddressupdatelog_gid, " +
                   " institution2email_gid, " +
                   " institution_gid, " +
                   " email_address," +
                   " primary_status," +
                   " created_by," +
                   " created_date)" +
                   " values (" +
                   "'" + msGetGid + "'," +
                   "'" + values.institution2email_gid + "'," +
                   "'" + values.institution_gid + "'," +
                   "'" + lsemail_address + "'," +
                   "'" + lsprimary_status + "'," +
                   "'" + employee_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    values.status = true;
                    values.message = "Email Address Updated Successfully";
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Error Occured..";
            }
        }

        public void DaDeleteInstitutionEmailAddress(string institution2email_gid, MdlMstBuyerOnboardEmailAddress values)
        {
            msSQL = "delete from agr_mst_tbyronboardinstitution2email where institution2email_gid='" + institution2email_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "delete from agr_mst_tbyronboardinstitution2emailupdatelog where institution2email_gid='" + institution2email_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.message = "Email Address Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }

        // Institution Address Details

        public bool DaPostInstitutionAddressDetail(string employee_gid, string user_gid, MdlMstBuyerOnboardAddressDetails values)
        {
            msSQL = "select primary_status from agr_mst_tbyronboardinstitution2address where primary_status='Yes' and (institution_gid='" + employee_gid + "' or institution_gid='" + values.institution_gid + "')";
            string lsprimary_status = objdbconn.GetExecuteScalar(msSQL);
            if (lsprimary_status == (values.primary_status))
            {
                values.status = false;
                values.message = "Already Primary Address Added";
                return false;
            }
            //msSQL = "select institution2address_gid from agr_mst_tbyronboardinstitution2address where addresstype_name='" + values.address_type + "' and (institution_gid='" + employee_gid + "' or institution_gid='" + values.institution_gid + "')";
            //objODBCDatareader = objdbconn.GetDataReader(msSQL);
            //if (objODBCDatareader.HasRows)
            //{
            //    objODBCDatareader.Close();
            //    values.status = false;
            //    values.message = "Already Address Type Added";
            //    return false;
            //}
            //objODBCDatareader.Close();
            msGetGid = objcmnfunctions.GetMasterGID("IT2A");
            msSQL = " insert into agr_mst_tbyronboardinstitution2address(" +
                    " institution2address_gid," +
                    " institution_gid," +
                    " addresstype_gid," +
                    " addresstype_name," +
                    " addressline1," +
                    " addressline2," +
                    " primary_status," +
                    " landmark," +
                    " postal_code," +
                    " city," +
                    " taluka," +
                    " district," +
                    " state," +
                    " country," +
                    " latitude," +
                    " longitude," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + values.address_typegid + "'," +
                    "'" + values.address_type + "'," +
                    "'" + values.addressline1 + "'," +
                    "'" + values.addressline2 + "'," +
                    "'" + values.primary_status + "'," +
                    "'" + values.landmark + "'," +
                    "'" + values.postal_code + "'," +
                    "'" + values.city + "'," +
                    "'" + values.taluka + "'," +
                    "'" + values.district + "'," +
                    "'" + values.state + "'," +
                    "'" + values.country + "'," +
                    "'" + values.latitude + "'," +
                    "'" + values.longitude + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Address Details Added Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
                return false;
            }

        }

        public void DaGetInstitutionAddressList(string employee_gid, MdlMstBuyerOnboardAddressDetails values)
        {
            msSQL = "  select institution2address_gid,addresstype_name,primary_status, addressline1, addressline2, taluka, district, state, country, landmark, latitude, longitude," +
                    " postal_code from agr_mst_tbyronboardinstitution2address where institution_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstaddress_list = new List<mstBuyerOnboardaddress_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstaddress_list.Add(new mstBuyerOnboardaddress_list
                    {
                        institution2address_gid = (dr_datarow["institution2address_gid"].ToString()),
                        address_type = (dr_datarow["addresstype_name"].ToString()),
                        primary_status = (dr_datarow["primary_status"].ToString()),
                        addressline1 = (dr_datarow["addressline1"].ToString()),
                        addressline2 = (dr_datarow["addressline2"].ToString()),
                        taluka = (dr_datarow["taluka"].ToString()),
                        district = (dr_datarow["district"].ToString()),
                        state = (dr_datarow["state"].ToString()),
                        country = (dr_datarow["country"].ToString()),
                        postal_code = (dr_datarow["postal_code"].ToString()),
                        landmark = (dr_datarow["landmark"].ToString()),
                        latitude = (dr_datarow["latitude"].ToString()),
                        longitude = (dr_datarow["longitude"].ToString()),
                    });
                }
                values.mstBuyerOnboardaddress_list = getmstaddress_list;
            }
            dt_datatable.Dispose();
        }

        public void DaEditInstitutionAddressDetail(string institution2address_gid, MdlMstBuyerOnboardAddressDetails values)
        {
            try
            {
                msSQL = "select addresstype_gid, addresstype_name, addressline1, addressline2, landmark, taluka, primary_status, postal_code, city," +
                    " district, state, country, latitude, longitude, institution_gid, institution2address_gid " +
                    " from agr_mst_tbyronboardinstitution2address where institution2address_gid='" + institution2address_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.address_typegid = objODBCDatareader["addresstype_gid"].ToString();
                    values.address_type = objODBCDatareader["addresstype_name"].ToString();
                    values.addressline1 = objODBCDatareader["addressline1"].ToString();
                    values.addressline2 = objODBCDatareader["addressline2"].ToString();
                    values.landmark = objODBCDatareader["landmark"].ToString();
                    values.taluka = objODBCDatareader["taluka"].ToString();
                    values.primary_status = objODBCDatareader["primary_status"].ToString();
                    values.postal_code = objODBCDatareader["postal_code"].ToString();
                    values.city = objODBCDatareader["city"].ToString();
                    values.district = objODBCDatareader["district"].ToString();
                    values.state = objODBCDatareader["state"].ToString();
                    values.country = objODBCDatareader["country"].ToString();
                    values.latitude = objODBCDatareader["latitude"].ToString();
                    values.longitude = objODBCDatareader["longitude"].ToString();
                    values.institution_gid = objODBCDatareader["institution_gid"].ToString();
                    values.institution2address_gid = objODBCDatareader["institution2address_gid"].ToString();
                }
                objODBCDatareader.Close();
                values.status = true;
                values.message = "success";
            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }
        }

        public void DaUpdateInstitutionAddressDetail(string employee_gid, MdlMstBuyerOnboardAddressDetails values)
        {
            msSQL = "select addresstype_gid, addresstype_name, addressline1, addressline2, landmark, taluka, primary_status, postal_code, city," +
                    " district, state, country, latitude, longitude, institution_gid, institution2address_gid " +
                    " from agr_mst_tbyronboardinstitution2address where institution2address_gid='" + values.institution2address_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsaddress_typegid = objODBCDatareader["addresstype_gid"].ToString();
                lsaddress_type = objODBCDatareader["addresstype_name"].ToString();
                lsaddressline1 = objODBCDatareader["addressline1"].ToString();
                lsaddressline2 = objODBCDatareader["addressline2"].ToString();
                lslandmark = objODBCDatareader["landmark"].ToString();
                lstaluka = objODBCDatareader["taluka"].ToString();
                lsprimary_status = objODBCDatareader["primary_status"].ToString();
                lspostal_code = objODBCDatareader["postal_code"].ToString();
                lscity = objODBCDatareader["city"].ToString();
                lsdistrict = objODBCDatareader["district"].ToString();
                lsstate = objODBCDatareader["state"].ToString();
                lscountry = objODBCDatareader["country"].ToString();
                lslatitude = objODBCDatareader["latitude"].ToString();
                lslongitude = objODBCDatareader["longitude"].ToString();
                lsinstitution_gid = objODBCDatareader["institution_gid"].ToString();
                lsinstitution2address_gid = objODBCDatareader["institution2address_gid"].ToString();
            }
            objODBCDatareader.Close();
            try
            {
                msSQL = " update agr_mst_tbyronboardinstitution2address set " +
                         " addresstype_gid='" + values.address_typegid + "'," +
                         " addresstype_name='" + values.address_type + "'," +
                         " addressline1='" + values.addressline1 + "'," +
                         " addressline2='" + values.addressline2 + "'," +
                         " landmark='" + values.landmark + "'," +
                         " taluka='" + values.taluka + "'," +
                         " primary_status='" + values.primary_status + "'," +
                         " postal_code='" + values.postal_code + "'," +
                         " city='" + values.city + "'," +
                         " district='" + values.district + "'," +
                         " state='" + values.state + "'," +
                         " country='" + values.country + "'," +
                         " latitude='" + values.latitude + "'," +
                         " longitude='" + values.longitude + "'," +
                         " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where institution2address_gid='" + values.institution2address_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("IAUL");

                    msSQL = " insert into agr_mst_tbyronboardinstitution2addressupdatelog(" +
                  " institution2addressupdatelog_gid," +
                  " institution2address_gid," +
                  " institution_gid," +
                  " addresstype_gid," +
                  " addresstype_name," +
                  " addressline1," +
                  " addressline2," +
                  " primary_status," +
                  " landmark," +
                  " postal_code," +
                  " city," +
                  " taluka," +
                  " district," +
                  " state," +
                  " country," +
                  " latitude," +
                  " longitude," +
                  " created_by," +
                  " created_date)" +
                  " values(" +
                  "'" + msGetGid + "'," +
                  "'" + values.institution2address_gid + "'," +
                  "'" + lsaddress_typegid + "'," +
                  "'" + lsaddress_type + "'," +
                  "'" + lsaddressline1 + "'," +
                  "'" + lsaddressline2 + "'," +
                  "'" + lsprimary_status + "'," +
                  "'" + lslandmark + "'," +
                  "'" + lspostal_code + "'," +
                  "'" + lscity + "'," +
                  "'" + lstaluka + "'," +
                  "'" + lsdistrict + "'," +
                  "'" + lsstate + "'," +
                  "'" + lscountry + "'," +
                  "'" + lslatitude + "'," +
                  "'" + lslongitude + "'," +
                  "'" + employee_gid + "'," +
                  "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    values.status = true;
                    values.message = "Address Details Updated Successfully";
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Error Occured..";
            }
        }

        public void DaDeleteInstitutionAddressDetail(string institution2address_gid, string employee_gid, MdlMstBuyerOnboardAddressDetails values)
        {
            msSQL = "delete from agr_mst_tbyronboardinstitution2address where institution2address_gid='" + institution2address_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "delete from agr_mst_tbyronboardinstitution2addressupdatelog where institution2address_gid='" + institution2address_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.message = "Address Details Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }

        // Institution License Details

        public bool DaPostInstitutionLicenseDetail(string employee_gid, string user_gid, MdlMstBuyerOnboardLicenseDetails values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("IT2L");
            msSQL = " insert into agr_mst_tbyronboardinstitution2licensedtl(" +
                    " institution2licensedtl_gid," +
                    " institution_gid," +
                    " licensetype_gid," +
                    " licensetype_name," +
                    " license_no," +
                    " issue_date," +
                    " expiry_date," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + values.licensetype_gid + "'," +
                    "'" + values.licensetype_name + "'," +
                    "'" + values.license_number + "',";
            if ((values.licenseissue_date == null) || (values.licenseissue_date == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.licenseissue_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            if ((values.licenseexpiry_date == null) || (values.licenseexpiry_date == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.licenseexpiry_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            msSQL += "'" + employee_gid + "'," +
             "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "License Details Added Successfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured";
                return false;
            }

        }

        public void DaGetInstitutionLicenseList(string employee_gid, MdlMstBuyerOnboardLicenseDetails values)
        {
            msSQL = " select institution2licensedtl_gid,licensetype_gid,licensetype_name,license_no,date_format(issue_date,'%d-%m-%Y') as issue_date," +
                    " date_format(expiry_date,'%d-%m-%Y') as expiry_date from agr_mst_tbyronboardinstitution2licensedtl" +
                    " where institution_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstlicense_list = new List<mstBuyerOnboardlicense_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstlicense_list.Add(new mstBuyerOnboardlicense_list
                    {
                        institution2licensedtl_gid = (dr_datarow["institution2licensedtl_gid"].ToString()),
                        licensetype_gid = (dr_datarow["licensetype_gid"].ToString()),
                        licensetype_name = (dr_datarow["licensetype_name"].ToString()),
                        license_number = (dr_datarow["license_no"].ToString()),
                        licenseissue_date = (dr_datarow["issue_date"].ToString()),
                        licenseexpiry_date = (dr_datarow["expiry_date"].ToString())
                    });
                }
                values.mstBuyerOnboardlicense_list = getmstlicense_list;
            }
            dt_datatable.Dispose();
        }

        public void DaEditInstitutionLicenseDetail(string institution2licensedtl_gid, MdlMstBuyerOnboardLicenseDetails values)
        {
            try
            {
                //msSQL = " select institution2licensedtl_gid,licensetype_gid,licensetype_name,license_no,date_format(issue_date,'%d-%m-%Y') as issue_date," +
                //   " date_format(expiry_date,'%d-%m-%Y') as expiry_date, date_format(expiry_date,'%Y-%m-%d') as expiry_dateedit,date_format(issue_date,'%Y-%m-%d') as issue_dateedit,institution_gid from agr_mst_tbyronboardinstitution2licensedtl" +
                //   " where institution2licensedtl_gid='" + institution2licensedtl_gid + "'";

                msSQL = " select institution2licensedtl_gid,licensetype_gid,licensetype_name,license_no, date_format(issue_date,'%d-%m-%Y') as issue_date," +
                  " date_format(expiry_date,'%d-%m-%Y') as expiry_date, institution_gid from agr_mst_tbyronboardinstitution2licensedtl" +
                  " where institution2licensedtl_gid='" + institution2licensedtl_gid + "'";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.licensetype_gid = objODBCDatareader["licensetype_gid"].ToString();
                    values.licensetype_name = objODBCDatareader["licensetype_name"].ToString();
                    values.license_number = objODBCDatareader["license_no"].ToString();
                    values.licenseissue_date = objODBCDatareader["issue_date"].ToString();
                    values.licenseexpiry_date = objODBCDatareader["expiry_date"].ToString();
                    //values.licenseissue_dateedit = objODBCDatareader["issue_dateedit"].ToString();
                    //values.licenseexpiry_dateedit = objODBCDatareader["expiry_dateedit"].ToString();
                    values.institution2licensedtl_gid = objODBCDatareader["institution2licensedtl_gid"].ToString();
                    values.institution_gid = objODBCDatareader["institution_gid"].ToString();
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

        public void DaUpdateInstitutionLicenseDetail(string employee_gid, MdlMstBuyerOnboardLicenseDetails values)
        {
            msSQL = " select institution2licensedtl_gid,licensetype_gid,licensetype_name,license_no,date_format(issue_date,'%d-%m-%Y') as issue_date," +
                  " date_format(expiry_date,'%d-%m-%Y') as expiry_date, institution_gid from agr_mst_tbyronboardinstitution2licensedtl" +
                  " where institution2licensedtl_gid='" + values.institution2licensedtl_gid + "'";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lslicensetype_gid = objODBCDatareader["licensetype_gid"].ToString();
                lslicensetype_name = objODBCDatareader["licensetype_name"].ToString();
                lslicense_number = objODBCDatareader["license_no"].ToString();
                lslicenseissue_date = objODBCDatareader["issue_date"].ToString();
                lslicenseexpiry_date = objODBCDatareader["expiry_date"].ToString();
                lsinstitution2licensedtl_gid = objODBCDatareader["institution2licensedtl_gid"].ToString();
                lsinstitution_gid = objODBCDatareader["institution_gid"].ToString();
            }
            objODBCDatareader.Close();
            try
            {
                msSQL = " update agr_mst_tbyronboardinstitution2licensedtl set " +
                         " licensetype_gid='" + values.licensetype_gid + "'," +
                         " licensetype_name='" + values.licensetype_name + "'," +
                         " license_no='" + values.license_number + "',";
                if (Convert.ToDateTime(values.licenseissue_date).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
                {

                }
                else
                {
                    msSQL += " issue_date='" + Convert.ToDateTime(values.licenseissue_date).ToString("yyyy-MM-dd 00:00:00") + "',";
                }
                if (Convert.ToDateTime(values.licenseexpiry_date).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
                {

                }
                else
                {
                    msSQL += " expiry_date='" + Convert.ToDateTime(values.licenseexpiry_date).ToString("yyyy-MM-dd 00:00:00") + "',";
                }
                msSQL += " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where institution2licensedtl_gid='" + values.institution2licensedtl_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("ILUL");

                    msSQL = "Insert into agr_mst_tinstitution2licenseupdatelog(" +
                   " institution2licenseupdatelog_gid, " +
                   " institution2licensedtl_gid, " +
                   " institution_gid, " +
                   " licensetype_gid," +
                   " licensetype_name," +
                   " license_no," +
                   " issue_date," +
                   " expiry_date," +
                   " created_by," +
                   " created_date)" +
                   " values (" +
                   "'" + msGetGid + "'," +
                   "'" + values.institution2licensedtl_gid + "'," +
                   "'" + values.institution_gid + "'," +
                   "'" + lslicensetype_gid + "'," +
                   "'" + lslicensetype_name + "'," +
                   "'" + lslicense_number + "'," +
                   "'" + lslicenseissue_date + "'," +
                   "'" + lslicenseexpiry_date + "'," +
                   "'" + employee_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    values.status = true;
                    values.message = "License Details Updated Successfully";
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Error Occured..";
            }
        }

        public void DaDeleteInstitutionLicenseDetail(string institution2licensedtl_gid, MdlMstBuyerOnboardLicenseDetails values)
        {
            msSQL = "delete from agr_mst_tbyronboardinstitution2licensedtl where institution2licensedtl_gid='" + institution2licensedtl_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "delete from agr_mst_tinstitution2licenseupdatelog where institution2licensedtl_gid='" + institution2licensedtl_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.message = "License Details Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }

        public bool DaInstitutionDocumentUpload(HttpRequest httpRequest, BuyerOnboardinstitutionuploaddocument objfilename, string employee_gid)
        {
            upload_list objdocumentmodel = new upload_list();
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string lsdocument_title = httpRequest.Form["document_title"].ToString();
            string lsdocument_id = httpRequest.Form["document_id"].ToString();
            string lscompanydocument_gid = httpRequest.Form["companydocument_gid"].ToString();
            String path = lspath;
            string project_flag = httpRequest.Form["project_flag"].ToString();

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/InstitutionDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
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
                        lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/InstitutionDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "SamAgro/InstitutionDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "SamAgro/InstitutionDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        msSQL = "select covenant_type from ocs_mst_tcompanydocument where companydocument_gid='" + lscompanydocument_gid + "'";
                        string lscovenant_type = objdbconn.GetExecuteScalar(msSQL);

                        msGetGid = objcmnfunctions.GetMasterGID("INDO");
                        msSQL = " insert into agr_mst_tbyronboardinstitution2documentupload( " +
                                    " institution2documentupload_gid," +
                                    " institution_gid," +
                                    " document_title ," +
                                    " document_id," +
                                    " document_name ," +
                                    " document_path," +
                                    " companydocument_gid, " +
                                    " covenant_type," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + lsdocument_title + "'," +
                                    "'" + lsdocument_id + "'," +
                                    "'" + httpPostedFile.FileName + "'," +
                                    "'" + lspath + msdocument_gid + FileExtension + "'," +
                                    "'" + lscompanydocument_gid + "'," +
                                    "'" + lscovenant_type + "'," +
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

                        msSQL = " select institution2documentupload_gid,institution_gid,document_name,document_path,document_title,document_id from agr_mst_tbyronboardinstitution2documentupload " +
                                " where institution_gid='" + employee_gid + "'";
                        dt_datatable = objdbconn.GetDataTable(msSQL);
                        var getdocumentdtlList = new List<BuyerOnboardinstitutionupload_list>();
                        if (dt_datatable.Rows.Count != 0)
                        {
                            foreach (DataRow dt in dt_datatable.Rows)
                            {
                                getdocumentdtlList.Add(new BuyerOnboardinstitutionupload_list
                                {
                                    document_name = dt["document_name"].ToString(),
                                    document_path = objcmnstorage.EncryptData((dt["document_path"].ToString())),
                                    institution_gid = dt["institution_gid"].ToString(),
                                    institution2documentupload_gid = dt["institution2documentupload_gid"].ToString(),
                                    document_title = dt["document_title"].ToString(),
                                    document_id = dt["document_id"].ToString(),
                                });
                                objfilename.BuyerOnboardinstitutionupload_list = getdocumentdtlList;
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

        public void DaInstitutionDocumentDelete(string institution2documentupload_gid, BuyerOnboardinstitutionuploaddocument objfilename, string employee_gid)
        {
            msSQL = "delete from agr_mst_tbyronboardinstitution2documentupload where institution2documentupload_gid='" + institution2documentupload_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult == 1)
            {
                msSQL = " select groupdocumentchecklist_gid from agr_trn_tdocumentchecktls where documentuploaded_gid='" + institution2documentupload_gid + "'";
                string lsgroupdocumentchecklist_gid = objdbconn.GetExecuteScalar(msSQL);

                if (lsgroupdocumentchecklist_gid != "")
                {
                    msSQL = " select count(documentcheckdtl_gid) as documentcount from agr_trn_tdocumentchecktls " +
                            " where groupdocumentchecklist_gid='" + lsgroupdocumentchecklist_gid + "'";
                    string lsdocumentcount = objdbconn.GetExecuteScalar(msSQL);
                    if (lsdocumentcount == "1")
                    {
                        msSQL = "delete from agr_trn_tgroupdocumentchecklist where groupdocumentchecklist_gid='" + lsgroupdocumentchecklist_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                msSQL = " select groupcovdocumentchecklist_gid from agr_trn_tcovanantdocumentcheckdtls where documentuploaded_gid='" + institution2documentupload_gid + "'";
                string lschecklist_gid = objdbconn.GetExecuteScalar(msSQL);

                if (lschecklist_gid != "")
                {
                    msSQL = " select count(covenantdocumentcheckdtl_gid) as documentcount from agr_trn_tcovanantdocumentcheckdtls " +
                      " where groupcovdocumentchecklist_gid='" + lschecklist_gid + "'";
                    string lsdocumentcount = objdbconn.GetExecuteScalar(msSQL);
                    if (lsdocumentcount == "1")
                    {
                        msSQL = "delete from agr_trn_tgroupcovenantdocumentchecklist where groupcovdocumentchecklist_gid='" + lschecklist_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                msSQL = "delete from agr_trn_tcovanantdocumentcheckdtls where documentuploaded_gid='" + institution2documentupload_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "delete from agr_trn_tdocumentchecktls where documentuploaded_gid='" + institution2documentupload_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            msSQL = " select institution2documentupload_gid,institution_gid,document_name,document_path,document_title,document_id from agr_mst_tbyronboardinstitution2documentupload " +
                               " where institution_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdocumentdtlList = new List<BuyerOnboardinstitutionupload_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getdocumentdtlList.Add(new BuyerOnboardinstitutionupload_list
                    {
                        document_name = dt["document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData((dt["document_path"].ToString())),
                        institution_gid = dt["institution_gid"].ToString(),
                        institution2documentupload_gid = dt["institution2documentupload_gid"].ToString(),
                        document_title = dt["document_title"].ToString(),
                        document_id = dt["document_id"].ToString(),
                    });
                    objfilename.BuyerOnboardinstitutionupload_list = getdocumentdtlList;
                }
            }
            dt_datatable.Dispose();

            if (mnResult != 0)
            {
                objfilename.message = "Document Deleted Successfully";
                objfilename.status = true;
            }
            else
            {
                objfilename.message = "Error Occured";
                objfilename.status = false;

            }
        }

        public void DaDeleteGSTInstitution(string employee_gid, string institution_gid, MdlMstBuyerOnboardGST values)
        {
            msSQL = "select institution2branch_gid from agr_mst_tbyronboardinstitution2branch where institution_gid='" + employee_gid + "' or institution_gid='" + institution_gid + "'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            string institution2branch_gid;
            foreach (DataRow dr_datarow in dt_datatable.Rows)
            {
                institution2branch_gid = (dr_datarow["institution2branch_gid"].ToString());
                msSQL = "delete from agr_mst_tbyronboardinstitution2branch where institution2branch_gid='" + institution2branch_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            dt_datatable.Dispose();

            if (mnResult != 0)
            {
                values.message = "GST Details Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured While Deleting The Gst Details";
                values.status = false;

            }
        }

        public void DaPostRatingdtl(string employee_gid, MdlBuyerOnboardRatingdtl values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("INRD");

            if (values.tmpadd_status == true)
                values.institution_gid = employee_gid;

            msSQL = " insert into agr_mst_tbyronboardinstitution2ratingdetail(" +
                    " institution2ratingdetail_gid," +
                    " institution_gid," +
                    " application_gid," +
                    " creditrating_agencygid," +
                    " creditrating_agencyname," +
                    " creditrating_gid," +
                    " creditrating_name," +
                    " assessed_on," +
                    " creditrating_link," +
                    " created_by," +
                    " created_date) values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.institution_gid + "'," +
                    "'" + values.application_gid + "'," +
                    "'" + values.creditrating_agencygid + "'," +
                    "'" + values.creditrating_agencyname + "'," +
                    "'" + values.creditrating_gid + "'," +
                    "'" + values.creditrating_name + "'," +
                    "'" + Convert.ToDateTime(values.assessed_on).ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                    "'" + values.creditrating_link.Replace("'", "") + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Rating Details are added Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
            }
        }

        public void DaGetInstitutionRatingList(string institution_gid, string employee_gid, string tmp_status, MdlBuyerOnboardRatingList values)
        {
            msSQL = " select institution2ratingdetail_gid,application_gid, institution_gid,creditrating_agencygid,creditrating_agencyname," +
                    " creditrating_gid,creditrating_name,date_format(a.assessed_on,'%d-%m-%Y') as assessed_on,creditrating_link, " +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date " +
                    " from agr_mst_tbyronboardinstitution2ratingdetail a ";
            if (tmp_status == "true")
                msSQL += " where a.institution_gid = '" + employee_gid + "' order by institution2ratingdetail_gid desc";
            else if (tmp_status == "both")
                msSQL += " where (a.institution_gid = '" + employee_gid + "' or a.institution_gid = '" + institution_gid + "') order by institution2ratingdetail_gid desc";
            else
                msSQL += " where a.institution_gid = '" + institution_gid + "' order by institution2ratingdetail_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getMdlRatingdtllist = new List<MdlBuyerOnboardRatingdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getMdlRatingdtllist.Add(new MdlBuyerOnboardRatingdtl
                    {
                        institution2ratingdetail_gid = (dr_datarow["institution2ratingdetail_gid"].ToString()),
                        application_gid = (dr_datarow["application_gid"].ToString()),
                        institution_gid = (dr_datarow["institution_gid"].ToString()),
                        creditrating_agencygid = (dr_datarow["creditrating_agencygid"].ToString()),
                        creditrating_agencyname = (dr_datarow["creditrating_agencyname"].ToString()),
                        creditrating_gid = (dr_datarow["creditrating_gid"].ToString()),
                        creditrating_name = (dr_datarow["creditrating_name"].ToString()),
                        assessed_on = (dr_datarow["assessed_on"].ToString()),
                        creditrating_link = (dr_datarow["creditrating_link"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                    });
                }
                values.MdlBuyerOnboardRatingdtl = getMdlRatingdtllist;
            }
            dt_datatable.Dispose();
        }

        public void DaDeleteRatingDtl(string institution2ratingdetail_gid, result values)
        {
            msSQL = "delete from agr_mst_tbyronboardinstitution2ratingdetail where institution2ratingdetail_gid='" + institution2ratingdetail_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "Rating Details are Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }

        public bool DaPostInstitutionBank(string employee_gid, MdlBuyerOnboardInstitution2BankAcc values)
        {

            msSQL = "select primary_status from agr_mst_tbyronboardinstitution2bankdtl where primary_status='Yes' and (institution_gid='" + employee_gid + "' or institution_gid='" + values.institution_gid + "')";
            string lsprimary_status = objdbconn.GetExecuteScalar(msSQL);
            if (lsprimary_status == (values.primary_status))
            {
                values.status = false;
                values.message = "Already Primary Bank Account Number Added";
                return false;
            }

            msGetGid = objcmnfunctions.GetMasterGID("I2BD");
            msSQL = " insert into agr_mst_tbyronboardinstitution2bankdtl(" +
                    " institution2bankdtl_gid," +
                    " institution_gid," +
                    //" application_gid," +
                    " bank_name," +
                    " branch_name," +
                    " bank_address," +
                    " micr_code," +
                    " ifsc_code," +
                    " bankaccount_name," +
                    " bankaccounttype_gid," +
                    " bankaccounttype_name," +
                    " bankaccount_number," +
                    " confirmbankaccountnumber," +
                    " joinaccount_status," +
                    " joinaccount_name," +
                    " primary_status," +
                    " chequebook_status," +
                    " accountopen_date," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    //"'" + values.application_gid + "'," +
                    "'" + values.bank_name.Replace("'", "") + "'," +
                    "'" + values.branch_name.Replace("'", "") + "'," +
                    "'" + values.bank_address.Replace("'", "") + "'," +
                    "'" + values.micr_code + "'," +
                    "'" + values.ifsc_code.Replace("'", "") + "'," +
                    "'" + values.bankaccount_name.Replace("'", "") + "'," +
                    "'" + values.bankaccounttype_gid + "'," +
                    "'" + values.bankaccounttype_name + "'," +
                    "'" + values.bankaccount_number + "'," +
                    "'" + values.confirmbankaccountnumber + "'," +
                    "'" + values.joint_account + "'," +
                    "'" + values.jointaccountholder_name + "'," +
                    "'" + values.primary_status + "'," +
                    "'" + values.chequebook_status + "',";
            if (values.accountopen_date == null || values.accountopen_date == "")
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.accountopen_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            msSQL += "'" + employee_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                msSQL = " select institution2bankdtl_gid,bank_name,branch_name,ifsc_code,bankaccount_number from " +
                          " agr_mst_tbyronboardinstitution2bankdtl where institution_gid='" + values.institution_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcreditbankacc_list = new List<BuyerOnboardinstitution2bankacc_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getcreditbankacc_list.Add(new BuyerOnboardinstitution2bankacc_list
                        {
                            bank_name = dt["bank_name"].ToString(),
                            branch_name = dt["branch_name"].ToString(),
                            ifsc_code = dt["ifsc_code"].ToString(),
                            bankaccount_number = dt["bankaccount_number"].ToString(),

                        });
                        values.BuyerOnboardinstitution2bankacc_list = getcreditbankacc_list;
                    }
                }
                dt_datatable.Dispose();
                values.status = true;
                values.message = "Bank Account Details Added Successfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured While Adding Bank Account Details";
                return false;
            }

        }

        public void DaGetInstitutionBankAccDtl(string employee_gid, MdlBuyerOnboardInstitution2BankAcc values)
        {
            msSQL = " select institution2bankdtl_gid,institution_gid,bank_name,branch_name,ifsc_code,bankaccount_number,primary_status, " +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                    " date_format(a.updated_date, '%d-%m-%Y %h:%i %p') as updated_date, " +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, " +
                    " concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as updated_by " +
                    " from agr_mst_tbyronboardinstitution2bankdtl a " +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " left join hrm_mst_temployee d on a.updated_by = d.employee_gid " +
                    " left join adm_mst_tuser e on e.user_gid = d.user_gid " +
                    " where institution_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcreditbankacc_list = new List<BuyerOnboardinstitution2bankacc_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getcreditbankacc_list.Add(new BuyerOnboardinstitution2bankacc_list
                    {
                        bank_name = dt["bank_name"].ToString(),
                        branch_name = dt["branch_name"].ToString(),
                        ifsc_code = dt["ifsc_code"].ToString(),
                        bankaccount_number = dt["bankaccount_number"].ToString(),
                        institution2bankdtl_gid = dt["institution2bankdtl_gid"].ToString(),
                        institution_gid = dt["institution_gid"].ToString(),
                        primary_status = dt["primary_status"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        updated_date = dt["updated_date"].ToString(),
                        updated_by = dt["updated_by"].ToString(),
                    });
                    values.BuyerOnboardinstitution2bankacc_list = getcreditbankacc_list;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaDeleteinstitutionBankAcc(string institution2bankdtl_gid, string institution_gid, MdlBuyerOnboardInstitution2BankAcc values, string employee_gid)
        {
            msSQL = "delete from agr_mst_tbyronboardinstitution2bankdtl where institution2bankdtl_gid='" + institution2bankdtl_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Bank Account Details Deleted successfully";

                msSQL = " select institution2bankdtl_gid,bank_name,branch_name,ifsc_code,bankaccount_number from " +
                           " agr_mst_tbyronboardinstitution2bankdtl where institution_gid='" + institution_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcreditbankacc_list = new List<BuyerOnboardinstitution2bankacc_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getcreditbankacc_list.Add(new BuyerOnboardinstitution2bankacc_list
                        {
                            bank_name = dt["bank_name"].ToString(),
                            branch_name = dt["branch_name"].ToString(),
                            ifsc_code = dt["ifsc_code"].ToString(),
                            bankaccount_number = dt["bankaccount_number"].ToString(),

                        });
                        values.BuyerOnboardinstitution2bankacc_list = getcreditbankacc_list;
                    }
                }
                dt_datatable.Dispose();
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred while deleting";
            }
        }


        public void DaInstitution2bankTmpList(string institution_gid, string employee_gid, MdlBuyerOnboardInstitution2BankAcc values)
        {
            msSQL = " select institution2bankdtl_gid,bank_name,branch_name,ifsc_code,bankaccount_number, primary_status, " +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                    " date_format(a.updated_date, '%d-%m-%Y %h:%i %p') as updated_date, " +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, " +
                    " concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as updated_by " +
                    " from agr_mst_tbyronboardinstitution2bankdtl a " +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " left join hrm_mst_temployee d on a.updated_by = d.employee_gid " +
                    " left join adm_mst_tuser e on e.user_gid = d.user_gid " +
                    " where institution_gid='" + institution_gid + "' or institution_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstlicense_list = new List<BuyerOnboardinstitution2bankacc_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getmstlicense_list.Add(new BuyerOnboardinstitution2bankacc_list
                    {
                        bank_name = dt["bank_name"].ToString(),
                        branch_name = dt["branch_name"].ToString(),
                        ifsc_code = dt["ifsc_code"].ToString(),
                        primary_status = dt["primary_status"].ToString(),
                        bankaccount_number = dt["bankaccount_number"].ToString(),
                        institution2bankdtl_gid = dt["institution2bankdtl_gid"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        updated_date = dt["updated_date"].ToString(),
                        updated_by = dt["updated_by"].ToString(),
                    });
                }
                values.BuyerOnboardinstitution2bankacc_list = getmstlicense_list;
            }
            dt_datatable.Dispose();
        }

        public void DaEditGetCreditBankAccDtl(string institution2bankdtl_gid, MdlBuyerOnboardInstitution2BankAcc values)
        {
            try
            {
                msSQL = "select institution2bankdtl_gid,institution_gid,application_gid,bank_name,branch_name,bank_address,micr_code,ifsc_code,bankaccount_name,primary_status," +
                " bankaccounttype_gid,bankaccounttype_name,bankaccount_number,confirmbankaccountnumber,joinaccount_status,joinaccount_name," +
                " chequebook_status,accountopen_date from agr_mst_tbyronboardinstitution2bankdtl where institution2bankdtl_gid='" + institution2bankdtl_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.institution2bankdtl_gid = objODBCDatareader["institution2bankdtl_gid"].ToString();
                    values.institution_gid = objODBCDatareader["institution_gid"].ToString();
                    values.application_gid = objODBCDatareader["application_gid"].ToString();
                    values.bank_name = objODBCDatareader["bank_name"].ToString();
                    values.branch_name = objODBCDatareader["branch_name"].ToString();
                    values.bank_address = objODBCDatareader["bank_address"].ToString();
                    values.micr_code = objODBCDatareader["micr_code"].ToString();
                    values.ifsc_code = objODBCDatareader["ifsc_code"].ToString();
                    values.bankaccount_name = objODBCDatareader["bankaccount_name"].ToString();
                    values.chequebook_status = objODBCDatareader["chequebook_status"].ToString();
                    values.bankaccounttype_gid = objODBCDatareader["bankaccounttype_gid"].ToString();
                    values.bankaccounttype_name = objODBCDatareader["bankaccounttype_name"].ToString();
                    values.bankaccount_number = objODBCDatareader["bankaccount_number"].ToString();
                    values.confirmbankaccountnumber = objODBCDatareader["confirmbankaccountnumber"].ToString();
                    values.joint_account = objODBCDatareader["joinaccount_status"].ToString();
                    values.jointaccountholder_name = objODBCDatareader["joinaccount_name"].ToString();
                    values.chequebook_status = objODBCDatareader["chequebook_status"].ToString();
                    values.primary_status = objODBCDatareader["primary_status"].ToString();

                    if (objODBCDatareader["accountopen_date"].ToString() == "")
                    {
                    }
                    else
                    {
                        values.accountopen_date = Convert.ToDateTime(objODBCDatareader["accountopen_date"]).ToString("MM-dd-yyyy");
                    }

                }
                values.status = true;


                dt_datatable.Dispose();
                values.message = "success";
                objODBCDatareader.Close();
            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }
        }

        public void DaUpdateInstitutionBankAccDtl(string employee_gid, MdlBuyerOnboardInstitution2BankAcc values)
        {

            msSQL = " select institution2bankdtl_gid,institution_gid,application_gid,bank_name,branch_name,bank_address,micr_code,ifsc_code,bankaccount_name," +
                " bankaccounttype_gid,bankaccounttype_name,bankaccount_number,confirmbankaccountnumber,joinaccount_status,joinaccount_name," +
                " chequebook_status,accountopen_date from agr_mst_tbyronboardinstitution2bankdtl where institution2bankdtl_gid='" + values.institution2bankdtl_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsbank_name = objODBCDatareader["bank_name"].ToString();
                lsbranch_name = objODBCDatareader["branch_name"].ToString();
                lsbank_address = objODBCDatareader["bank_address"].ToString();
                lsmicr_code = objODBCDatareader["micr_code"].ToString();
                lsifsc_code = objODBCDatareader["ifsc_code"].ToString();
                lsbankaccount_name = objODBCDatareader["bankaccount_name"].ToString();
                lsbankaccounttype_gid = objODBCDatareader["bankaccounttype_gid"].ToString();
                lsbankaccounttype_name = objODBCDatareader["bankaccounttype_name"].ToString();
                lsbankaccount_number = objODBCDatareader["bankaccount_number"].ToString();
                lsconfirmbankaccountnumber = objODBCDatareader["confirmbankaccountnumber"].ToString();
                lsjoinaccount_status = objODBCDatareader["joinaccount_status"].ToString();
                lsjoinaccount_name = objODBCDatareader["joinaccount_name"].ToString();
                lschequebook_status = objODBCDatareader["chequebook_status"].ToString();
                lsaccountopen_date = objODBCDatareader["accountopen_date"].ToString();
                lsinstitution_gid = objODBCDatareader["institution_gid"].ToString();
                lsapplication_gid = objODBCDatareader["application_gid"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " update agr_mst_tbyronboardinstitution2bankdtl set " +
                     " bank_name='" + values.bank_name + "'," +
                     " branch_name='" + values.branch_name.Replace("'", "") + "'," +
                     " bank_address='" + values.bank_address.Replace("'", "") + "'," +
                     " micr_code='" + values.micr_code + "'," +
                     " ifsc_code='" + values.ifsc_code + "'," +
                     " bankaccount_name='" + values.bankaccount_name.Replace("'", "") + "'," +
                     " bankaccounttype_gid='" + values.bankaccounttype_gid + "'," +
                     " bankaccounttype_name='" + values.bankaccounttype_name + "'," +
                     " bankaccount_number='" + values.bankaccount_number + "'," +
                     " confirmbankaccountnumber='" + values.confirmbankaccountnumber + "'," +
                     " joinaccount_status='" + values.joint_account + "'," +
                     " joinaccount_name='" + values.jointaccountholder_name + "'," +
                     " primary_status='" + values.primary_status + "'," +
                     " chequebook_status='" + values.chequebook_status + "',";
            if ((values.accountopen_date == null) || (values.accountopen_date == ""))
            {
                msSQL += "accountopen_date=null,";
            }
            else
            {
                msSQL += "accountopen_date= '" + Convert.ToDateTime(values.accountopen_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }

            msSQL +=
                     " updated_by='" + employee_gid + "'," +
                     " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                     " where institution2bankdtl_gid='" + values.institution2bankdtl_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult == 1)
            {//Date Updation
                //if (Convert.ToDateTime(values.accountopendate).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
                //{

                //}
                //else
                //{
                //    msSQL = "update agr_mst_tbyronboardinstitution2bankdtl set accountopen_date='" + Convert.ToDateTime(values.accountopendate).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "'" +
                //        "where institution2bankdtl_gid='" + values.institution2bankdtl_gid + "' ";
                //    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                //}

                msGetGid = objcmnfunctions.GetMasterGID("I2BU");

                msSQL = " insert into agr_mst_tbyronboardinstitution2bankdtlupdatelog(" +
                    " institution2bankdtlupdatelog_gid," +
                    " institution2bankdtl_gid," +
                    " institution_gid," +
                    " application_gid," +
                    " bank_name," +
                    " branch_name," +
                    " bank_address," +
                    " micr_code," +
                    " ifsc_code," +
                    " bankaccount_name," +
                    " bankaccounttype_gid," +
                    " bankaccounttype_name," +
                    " bankaccount_number," +
                    " confirmbankaccountnumber," +
                    " joinaccount_status," +
                    " joinaccount_name," +
                    " chequebook_status," +
                    " accountopen_date," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.institution2bankdtl_gid + "'," +
                    "'" + lsinstitution_gid + "'," +
                    "'" + lsapplication_gid + "'," +
                    "'" + lsbank_name + "'," +
                    "'" + lsbranch_name.Replace("'", "") + "'," +
                    "'" + lsbank_address.Replace("'", "") + "'," +
                    "'" + lsmicr_code + "'," +
                    "'" + lsifsc_code + "'," +
                    "'" + lsbankaccount_name + "'," +
                    "'" + lsbankaccounttype_gid + "'," +
                    "'" + lsbankaccounttype_name + "'," +
                    "'" + lsbankaccount_number + "'," +
                    "'" + lsconfirmbankaccountnumber + "'," +
                    "'" + lsjoinaccount_status + "'," +
                    "'" + lsjoinaccount_name + "'," +
                    "'" + lschequebook_status + "',";
                if (values.accountopen_date == null || values.accountopen_date == "")
                {
                    msSQL += "null,";
                }
                else
                {
                    msSQL += "'" + Convert.ToDateTime(values.accountopen_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                }
                msSQL +=
                        "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Bank Account Details Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while updating Bank Account";
            }
        }

        public bool DaSubmitInstitutionDtl(MdlMstBuyerOnboardInstitutionAdd values, string employee_gid)
        {
            msSQL = "select institution_gid from agr_mst_tbyronboardinstitution2mobileno where institution_gid='" + employee_gid + "' and primary_status='Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Primary Mobile Number";
                return false;
            }
            objODBCDatareader.Close();
            msSQL = "select institution_gid from agr_mst_tbyronboardinstitution2mobileno where institution_gid='" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Atleast One Mobile Number";
                return false;
            }
            objODBCDatareader.Close();
            msSQL = "select institution_gid from agr_mst_tbyronboardinstitution2email where institution_gid='" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Atleast One Email Address";
                return false;
            }
            objODBCDatareader.Close();
            msSQL = "select institution_gid from agr_mst_tbyronboardinstitution2address where institution_gid='" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Atleast One Address Detail";
                return false;
            }
            objODBCDatareader.Close();
            msSQL = "select application_gid from tmp_application where employee_gid='" + employee_gid + "'";
            lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select stakeholder_type from agr_mst_tbyronboardcontact where application_gid='" + lsapplication_gid + "' and stakeholder_type in ('Borrower','Applicant')";
            string lsstakeholder_type = objdbconn.GetExecuteScalar(msSQL);

            if (lsstakeholder_type == values.stakeholder_type)
            {

                values.status = false;
                values.message = "Applicant/Borrower Information Already Added";
                return false;
            }

            msSQL = "select stakeholder_type from agr_mst_tbyronboard2institution where application_gid='" + lsapplication_gid + "' and stakeholder_type in ('Borrower','Applicant')";
            lsstakeholder_type = objdbconn.GetExecuteScalar(msSQL);

            if (lsstakeholder_type == values.stakeholder_type)
            {

                values.status = false;
                values.message = "Applicant/Borrower Information Already Added";
                return false;
            }

            msGetGid = objcmnfunctions.GetMasterGID("APIN");
            msSQL = " insert into agr_mst_tbyronboard2institution(" +
                " institution_gid," +
                " application_gid," +
                " company_name," +
                " date_incorporation," +
                " businessstart_date," +
                " year_business," +
                " month_business," +
                " companypan_no," +
                " cin_no," +
                " official_telephoneno," +
                " officialemail_address," +
                " companytype_gid," +
                " companytype_name," +
                " stakeholdertype_gid," +
                " stakeholder_type," +
                " assessmentagency_gid," +
                " assessmentagency_name," +
                " assessmentagencyrating_gid," +
                " assessmentagencyrating_name," +
                " ratingas_on," +
                " amlcategory_gid," +
                " amlcategory_name," +
                " businesscategory_gid," +
                " businesscategory_name," +
                " contactperson_firstname," +
                " contactperson_middlename," +
                " contactperson_lastname," +
                " designation_gid," +
                " designation," +
                " start_date," +
                " end_date," +
                " lastyear_turnover," +
                " escrow," +
                " urn_status," +
                " urn," +
                " institution_status," +
                " tan_number," +
                " incometax_returnsstatus," +
                " revenue," +
                " profit," +
                " fixed_assets," +
                " sundrydebt_adv," +
                " created_by," +
                " created_date) values(" +
                  "'" + msGetGid + "'," +
                  "'" + lsapplication_gid + "'," +
                  "'" + values.company_name + "',";
            if ((values.date_incorporation == null) || (values.date_incorporation == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.date_incorporation).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            if ((values.businessstartdate == null) || (values.businessstartdate == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.businessstartdate).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            msSQL += "'" + values.year_business + "'," +
                    "'" + values.month_business + "'," +
                    "'" + values.companypan_no + "'," +
                    "'" + values.cin_no + "'," +
                    "'" + values.official_telephoneno + "'," +
                    "'" + values.official_mailid + "'," +
                    "'" + values.companytype_gid + "'," +
                    "'" + values.companytype_name + "'," +
                    "'" + values.stakeholdertype_gid + "'," +
                    "'" + values.stakeholder_type + "'," +
                    "'" + values.assessmentagency_gid + "'," +
                    "'" + values.assessmentagency_name + "'," +
                    "'" + values.assessmentagencyrating_gid + "'," +
                    "'" + values.assessmentagencyrating_name + "',";
            if ((values.ratingas_on == null) || (values.ratingas_on == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.ratingas_on).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            msSQL += "'" + values.amlcategory_gid + "'," +
                    "'" + values.amlcategory_name + "'," +
                    "'" + values.businesscategory_gid + "'," +
                    "'" + values.businesscategory_name + "'," +
                    "'" + values.contactperson_firstname + "'," +
                    "'" + values.contactperson_middlename + "'," +
                    "'" + values.contactperson_lastname + "'," +
                    "'" + values.designation_gid + "'," +
                    "'" + values.designation + "',";
            if ((values.start_date == null) || (values.start_date == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.start_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            if ((values.end_date == null) || (values.end_date == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.end_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            msSQL += "'" + values.lastyear_turnover + "'," +
                    "'" + values.escrow + "'," +
                    "'" + values.urn_status + "'," +
                    "'" + values.urn + "'," +
                    "'Completed'," +
                    "'" + values.tan_number + "'," +
                    "'" + values.incometax_returnsstatus + "',";
            if (values.revenue == null || values.revenue == "")
            {
                msSQL += "'0.00',";
            }
            else
            {
                msSQL += "'" + values.revenue.Replace(",", "") + "',";
            }
            if (values.profit == null || values.profit == "")
            {
                msSQL += "'0.00',";
            }
            else
            {
                msSQL += "'" + values.profit.Replace(",", "") + "',";
            }
            if (values.fixed_assets == null || values.fixed_assets == "")
            {
                msSQL += "'0.00',";
            }
            else
            {
                msSQL += "'" + values.sundrydebt_adv.Replace(",", "") + "',";
            }
            if (values.sundrydebt_adv == null || values.sundrydebt_adv == "")
            {
                msSQL += "'0.00',";
            }
            else
            {
                msSQL += "'" + values.sundrydebt_adv.Replace(",", "") + "',";
            }

            msSQL +=
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "update agr_mst_tbyronboardinstitution2branch set institution_gid='" + msGetGid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tbyronboardinstitution2mobileno set institution_gid='" + msGetGid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tbyronboardinstitution2email set institution_gid='" + msGetGid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tbyronboardinstitution2address set institution_gid='" + msGetGid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tbyronboardinstitution2licensedtl set institution_gid='" + msGetGid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tbyronboardinstitution2ratingdetail set institution_gid='" + msGetGid + "', application_gid ='" + lsapplication_gid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tbyronboardinstitution2bankdtl set institution_gid='" + msGetGid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_trn_ttandtl set function_gid='" + msGetGid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                msSQL = "update agr_mst_tbyronboardinstitution2documentupload set institution_gid='" + msGetGid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tbyronboardinstitution2form60documentupload set institution_gid='" + msGetGid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycgstsbpan set function_gid ='" + msGetGid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                msSQL = "select mobile_no from agr_mst_tbyronboardinstitution2mobileno where institution_gid='" + msGetGid + "' and primary_status='yes'";
                lsmobileno = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select email_address from agr_mst_tbyronboardinstitution2email where institution_gid='" + msGetGid + "' and primary_status='yes'";
                lsemail_address = objdbconn.GetExecuteScalar(msSQL);
                if (values.stakeholder_type == "Borrower" || values.stakeholder_type == "Applicant")
                {
                    msSQL = "update agr_mst_tbyronboard set applicant_type ='Institution' where application_gid='" + lsapplication_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_tbyronboard2institution set mobile_no='" + lsmobileno + "'," +
                     " email_address='" + lsemail_address + "' where institution_gid='" + msGetGid + "' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }


                values.message = "Institution Information Submitted Successfully";
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

        public bool DaPANForm60DocumentUpload(HttpRequest httpRequest, BuyerOnboarduploaddocument objfilename, string employee_gid)
        {
            BuyerOnboardupload_list objdocumentmodel = new BuyerOnboardupload_list();
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string project_flag = httpRequest.Form["project_flag"].ToString();
            String path = lspath;

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/PANForm60Document/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
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
                        lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/PANForm60Document/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "SamAgro/PANForm60Document/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "SamAgro/PANForm60Document/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        msGetGid = objcmnfunctions.GetMasterGID("CF60");
                        msSQL = " insert into agr_mst_tbyronboardcontact2panform60(" +
                                " contact2panform60_gid," +
                                " contact_gid," +
                                " document_name," +
                                " document_path," +
                                " created_by," +
                                " created_date)" +
                                " values(" +
                                "'" + msGetGid + "'," +
                                "'" + employee_gid + "'," +
                                "'" + httpPostedFile.FileName + "'," +
                                "'" + lspath + msdocument_gid + FileExtension + "'," +
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
            catch (Exception ex)
            {
                objfilename.message = ex.ToString();
            }
            return true;
        }

        public void DaPANForm60Delete(string contact2panform60_gid, MdlBuyerOnboardContactPANForm60 values)
        {
            msSQL = "delete from agr_mst_tbyronboardcontact2panform60 where contact2panform60_gid='" + contact2panform60_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "Form-60 Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }

        public void DaGetPANForm60List(string employee_gid, MdlBuyerOnboardContactPANForm60 values)
        {
            msSQL = "select contact2panform60_gid,document_name, document_path from agr_mst_tbyronboardcontact2panform60 where " +
              " contact_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcontactpanform60_list = new List<BuyerOnboardcontactpanform60_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcontactpanform60_list.Add(new BuyerOnboardcontactpanform60_list
                    {
                        contact2panform60_gid = (dr_datarow["contact2panform60_gid"].ToString()),
                        document_name = (dr_datarow["document_name"].ToString()),
                        document_path = objcmnstorage.EncryptData((dr_datarow["document_path"].ToString())),
                    });

                    values.BuyerOnboardcontactpanform60_list = getcontactpanform60_list;
                }
                dt_datatable.Dispose();
            }

        }

        public void DaGetEditPANForm60List(string employee_gid, string contact_gid, MdlBuyerOnboardContactPANForm60 values)
        {
            msSQL = "select contact2panform60_gid,document_name, document_path from agr_mst_tbyronboardcontact2panform60 where " +
              " contact_gid='" + employee_gid + "'or contact_gid='" + contact_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcontactpanform60_list = new List<BuyerOnboardcontactpanform60_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcontactpanform60_list.Add(new BuyerOnboardcontactpanform60_list
                    {
                        contact2panform60_gid = (dr_datarow["contact2panform60_gid"].ToString()),
                        document_name = (dr_datarow["document_name"].ToString()),
                        document_path = objcmnstorage.EncryptData((dr_datarow["document_path"].ToString())),
                    });

                    values.BuyerOnboardcontactpanform60_list = getcontactpanform60_list;
                }
                dt_datatable.Dispose();
            }

        }

        public void DaPANAbsenceReasonList(MdlBuyerOnboardPANAbsenceReason objMdlPANAbsenceReason)
        {
            try
            {
                msSQL = " SELECT panabsencereason" +
                   " from agr_mst_tpanabsencereason";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getpanabsencereason_list = new List<BuyerOnboardpanabsencereason_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    objMdlPANAbsenceReason.BuyerOnboardpanabsencereason_list = dt_datatable.AsEnumerable().Select(row =>
                      new BuyerOnboardpanabsencereason_list
                      {
                          panabsencereason = row["panabsencereason"].ToString(),
                      }
                    ).ToList();
                }
                dt_datatable.Dispose();
                objMdlPANAbsenceReason.status = true;
            }
            catch (Exception ex)
            {
                objMdlPANAbsenceReason.status = false;
            }

        }

        public void DaPostPANAbsenceReasons(MdlBuyerOnboardPANAbsenceReason values, string employee_gid)
        {
            foreach (string reason in values.panabsencereason_selectedlist)
            {

                msGetGid = objcmnfunctions.GetMasterGID("C2PR");
                msSQL = " INSERT INTO agr_mst_tbyronboardcontact2panabsencereason(" +
                        " contact2panabsencereason_gid," +
                        " contact_gid," +
                        " panabsencereason," +
                        " created_date," +
                        " created_by)" +
                        " VALUES(" +
                        "'" + msGetGid + "'," +
                        "'" + employee_gid + "'," +
                        "'" + reason + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        "'" + employee_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            }

            if (mnResult == 1)
            {
                values.status = true;
                values.message = "PAN Absence Reasons submitted successfully...";
            }
            else
            {
                values.status = true;
                values.message = "Error Occured";
            }
        }

        public void DaPANReasonsCheck(MdlBuyerOnboardPANAbsenceReason objMdlPANAbsenceReason, string employee_gid)
        {
            try
            {
                msSQL = " SELECT count(panabsencereason)" +
                   " from agr_mst_tbyronboardcontact2panabsencereason" +
                   " where contact_gid='" + employee_gid + "'";

                string lspanabsencereason_count = objdbconn.GetExecuteScalar(msSQL);

                if (int.Parse(lspanabsencereason_count) > 0)
                {
                    objMdlPANAbsenceReason.status = true;
                }
                else
                {
                    objMdlPANAbsenceReason.status = false;
                }
            }
            catch (Exception ex)
            {
                objMdlPANAbsenceReason.status = false;
            }
        }

        public bool DaIndividualProofDocumentUpload(HttpRequest httpRequest, BuyerOnboarduploaddocument objfilename, string employee_gid)
        {
            BuyerOnboardupload_list objdocumentmodel = new BuyerOnboardupload_list();
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string lsidproof_type = httpRequest.Form["idproof_type"].ToString();
            string lsidproof_no = httpRequest.Form["idproof_no"].ToString();
            string lsidproof_dob = httpRequest.Form["idproof_dob"].ToString();
            string lsfile_no = httpRequest.Form["file_no"].ToString();
            string project_flag = httpRequest.Form["project_flag"].ToString();
            String path = lspath;

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/IndividualProofDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
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
                        lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/IndividualProofDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "SamAgro/IndividualProofDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "SamAgro/IndividualProofDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        msGetGid = objcmnfunctions.GetMasterGID("C2IP");
                        msSQL = " insert into agr_mst_tbyronboardcontact2idproof(" +
                                " contact2idproof_gid," +
                                " contact_gid," +
                                " idproof_name," +
                                " idproof_no," +
                                " idproof_dob," +
                                " file_no," +
                                " document_name," +
                                " document_path," +
                                " created_by," +
                                " created_date)" +
                                " values(" +
                                "'" + msGetGid + "'," +
                                "'" + employee_gid + "'," +
                                "'" + lsidproof_type + "'," +
                                "'" + lsidproof_no + "'," +
                                "'" + lsidproof_dob + "'," +
                                "'" + lsfile_no + "'," +
                                "'" + httpPostedFile.FileName + "'," +
                                "'" + lspath + msdocument_gid + FileExtension + "'," +
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
            catch (Exception ex)
            {
                objfilename.message = ex.ToString();
            }
            return true;
        }

        public void DaGetIndividualProofList(string employee_gid, MdlBuyerOnboardContactIdProof values)
        {
            msSQL = "select contact2idproof_gid,idproof_name,idproof_no,idproof_dob,file_no,document_name, document_path from agr_mst_tbyronboardcontact2idproof where " +
              " contact_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcontactidproof_list = new List<contactBuyerOnboardidproof_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcontactidproof_list.Add(new contactBuyerOnboardidproof_list
                    {
                        contact2idproof_gid = (dr_datarow["contact2idproof_gid"].ToString()),
                        idproof_name = (dr_datarow["idproof_name"].ToString()),
                        idproof_no = (dr_datarow["idproof_no"].ToString()),
                        idproof_dob = (dr_datarow["idproof_dob"].ToString()),
                        file_no = (dr_datarow["file_no"].ToString()),
                        document_name = (dr_datarow["document_name"].ToString()),
                        document_path = objcmnstorage.EncryptData((dr_datarow["document_path"].ToString()))
                    });

                    values.contactBuyerOnboardidproof_list = getcontactidproof_list;
                }
                dt_datatable.Dispose();
            }

        }

        public void DaIndividualProofDelete(string contact2idproof_gid, MdlBuyerOnboardContactIdProof values)
        {
            msSQL = "delete from agr_mst_tbyronboardcontact2idproof where contact2idproof_gid='" + contact2idproof_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "ID Proof Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }

        public bool DaIndividualDocumentUpload(HttpRequest httpRequest, BuyerOnboarduploaddocument objfilename, string employee_gid)
        {
            BuyerOnboardupload_list objdocumentmodel = new BuyerOnboardupload_list();
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string lsdocument_title = httpRequest.Form["document_title"].ToString();
            String path = lspath;
            string lsindividualdocument_gid = httpRequest.Form["individualdocument_gid"].ToString();
            string project_flag = httpRequest.Form["project_flag"].ToString();

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/IndividualDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
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
                        lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/IndividualDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "SamAgro/IndividualDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "SamAgro/IndividualDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        msSQL = "select covenant_type from ocs_mst_tindividualdocument where individualdocument_gid='" + lsindividualdocument_gid + "'";
                        string lscovenant_type = objdbconn.GetExecuteScalar(msSQL);

                        msGetGid = objcmnfunctions.GetMasterGID("C2DO");
                        msGetDocumentGid = objcmnfunctions.GetMasterGID("BSDA");

                        msSQL = " insert into agr_mst_tbyronboardcontact2document( " +
                                    " contact2document_gid ," +
                                    " contact_gid ," +
                                    " document_gid ," +
                                    " document_title ," +
                                    " document_name ," +
                                    " document_path," +
                                    " individualdocument_gid, " +
                                    " covenant_type," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + msGetDocumentGid + "'," +
                                    "'" + lsdocument_title + "'," +
                                    "'" + httpPostedFile.FileName + "'," +
                                    "'" + lspath + msdocument_gid + FileExtension + "'," +
                                    "'" + lsindividualdocument_gid + "'," +
                                    "'" + lscovenant_type + "'," +
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
            catch (Exception ex)
            {
                objfilename.message = ex.ToString();
            }
            return true;
        }

        public void DaGetIndividualDocList(string employee_gid, MdlBuyerOnboardContactDocument values)
        {
            msSQL = " select contact2document_gid,document_name,document_path,document_title from agr_mst_tbyronboardcontact2document " +
                                 " where contact_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdocumentdtlList = new List<uploadBuyerOnboardindividualdoc_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getdocumentdtlList.Add(new uploadBuyerOnboardindividualdoc_list
                    {
                        document_name = dt["document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData((dt["document_path"].ToString())),
                        contact2document_gid = dt["contact2document_gid"].ToString(),
                        document_title = dt["document_title"].ToString(),
                    });
                    values.uploadBuyerOnboardindividualdoc_list = getdocumentdtlList;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaGetIndividualDocListEdit(string employee_gid,string contact_gid, MdlBuyerOnboardContactDocument values)
        {
            msSQL = " select contact2document_gid,document_name,document_path,document_title from agr_mst_tbyronboardcontact2document " +
                                 " where contact_gid='" + employee_gid + "'or contact_gid='" + contact_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdocumentdtlList = new List<uploadBuyerOnboardindividualdoc_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getdocumentdtlList.Add(new uploadBuyerOnboardindividualdoc_list
                    {
                        document_name = dt["document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData((dt["document_path"].ToString())),
                        contact2document_gid = dt["contact2document_gid"].ToString(),
                        document_title = dt["document_title"].ToString(),
                    });
                    values.uploadBuyerOnboardindividualdoc_list = getdocumentdtlList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaIndividualDocDelete(string contact2document_gid, MdlBuyerOnboardContactDocument values)
        {
            msSQL = "delete from agr_mst_tbyronboardcontact2document where contact2document_gid='" + contact2document_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                msSQL = " select groupdocumentchecklist_gid from agr_trn_tdocumentchecktls where documentuploaded_gid='" + contact2document_gid + "'";
                string lsgroupdocumentchecklist_gid = objdbconn.GetExecuteScalar(msSQL);

                if (lsgroupdocumentchecklist_gid != "")
                {
                    msSQL = " select count(documentcheckdtl_gid) as documentcount from agr_trn_tdocumentchecktls " +
                            " where groupdocumentchecklist_gid='" + lsgroupdocumentchecklist_gid + "'";
                    string lsdocumentcount = objdbconn.GetExecuteScalar(msSQL);
                    if (lsdocumentcount == "1")
                    {
                        msSQL = "delete from agr_trn_tgroupdocumentchecklist where groupdocumentchecklist_gid='" + lsgroupdocumentchecklist_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }

                msSQL = " select groupcovdocumentchecklist_gid from agr_trn_tcovanantdocumentcheckdtls where documentuploaded_gid='" + contact2document_gid + "'";
                string lschecklist_gid = objdbconn.GetExecuteScalar(msSQL);

                if (lschecklist_gid != "")
                {
                    msSQL = " select count(covenantdocumentcheckdtl_gid) as documentcount from agr_trn_tcovanantdocumentcheckdtls " +
                      " where groupcovdocumentchecklist_gid='" + lschecklist_gid + "'";
                    string lsdocumentcount = objdbconn.GetExecuteScalar(msSQL);
                    if (lsdocumentcount == "1")
                    {
                        msSQL = "delete from agr_trn_tgroupcovenantdocumentchecklist where groupcovdocumentchecklist_gid='" + lschecklist_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }

                msSQL = "delete from agr_trn_tcovanantdocumentcheckdtls where documentuploaded_gid='" + contact2document_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "delete from agr_trn_tdocumentchecktls where documentuploaded_gid='" + contact2document_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            if (mnResult != 0)
            {

                values.message = "Document Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }

        public bool DaMobileNumberAdd(string employee_gid, MdlBuyerOnboardContactMobileNo values)
        {
            msSQL = "select primary_status from agr_mst_tbyronboardcontact2mobileno where primary_status='Yes' and (contact_gid='" + employee_gid + "' or contact_gid='" + values.contact_gid + "')";
            string lsprimary_status = objdbconn.GetExecuteScalar(msSQL);
            if (lsprimary_status == (values.primary_status))
            {
                values.status = false;
                values.message = "Already Primary Mobile Number Added";
                objdbconn.CloseConn();
                return false;
            }

            //msSQL = "select mobile_no from agr_mst_tbyronboardcontact2mobileno where mobile_no='" + values.mobile_no + "' and contact_gid='" + employee_gid + "'";
            //string lsmobile_no = objdbconn.GetExecuteScalar(msSQL);
            //if (lsmobile_no == (values.mobile_no))
            //{

            //    values.status = false;
            //    values.message = "Already This Mobile Number Added";
            //    objdbconn.CloseConn();
            //    return false;
            //}

            msGetGid = objcmnfunctions.GetMasterGID("C2MN");

            msSQL = " insert into agr_mst_tbyronboardcontact2mobileno(" +
                    " contact2mobileno_gid," +
                    " contact_gid," +
                    " mobile_no," +
                    " primary_status," +
                    " whatsapp_no," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + values.mobile_no + "'," +
                    "'" + values.primary_status + "'," +
                    "'" + values.whatsapp_no + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            objdbconn.CloseConn();

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Mobile Number Added Successfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured";
                return false;
            }
        }

        public void DaGetMobileNoList(string employee_gid, MdlBuyerOnboardContactMobileNo values)
        {
            msSQL = "select mobile_no,contact2mobileno_gid,primary_status,whatsapp_no from agr_mst_tbyronboardcontact2mobileno where " +
              " contact_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcontactmobileno_list = new List<BuyerOnboardcontactmobileno_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcontactmobileno_list.Add(new BuyerOnboardcontactmobileno_list
                    {
                        contact2mobileno_gid = (dr_datarow["contact2mobileno_gid"].ToString()),
                        mobile_no = (dr_datarow["mobile_no"].ToString()),
                        primary_status = (dr_datarow["primary_status"].ToString()),
                        whatsapp_no = (dr_datarow["whatsapp_no"].ToString()),
                    });
                }
            }
            values.BuyerOnboardcontactmobileno_list = getcontactmobileno_list;
            dt_datatable.Dispose();
        }

        public void DaMobileNoDelete(string contact2mobileno_gid, MdlBuyerOnboardContactMobileNo values)
        {
            msSQL = "delete from agr_mst_tbyronboardcontact2mobileno where contact2mobileno_gid='" + contact2mobileno_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "Mobile Number Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }

        public bool DaEmailAddressAdd(string employee_gid, MdlBuyerOnboardContactEmail values)
        {
            msSQL = "select primary_status from agr_mst_tbyronboardcontact2email where primary_status='Yes' and (contact_gid='" + employee_gid + "' or contact_gid='" + values.contact_gid + "') ";
            string lsprimary_status = objdbconn.GetExecuteScalar(msSQL);
            if (lsprimary_status == (values.primary_status))
            {
                values.status = false;
                values.message = "Already Primary Email Address Added";
                objdbconn.CloseConn();
                return false;
            }

            //msSQL = "select email_address from agr_mst_tbyronboardcontact2email where email_address='" + values.email_address + "' and contact_gid='" + employee_gid + "'";
            //string lsemail_address = objdbconn.GetExecuteScalar(msSQL);
            //if (lsemail_address == (values.email_address))
            //{
            //    values.status = false;
            //    values.message = "Already This Email Address Added";
            //    objdbconn.CloseConn();
            //    return false;
            //}

            msGetGid = objcmnfunctions.GetMasterGID("C2EA");
            msSQL = " insert into agr_mst_tbyronboardcontact2email(" +
                    " contact2email_gid," +
                    " contact_gid," +
                    " email_address," +
                    " primary_status," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + values.email_address + "'," +
                    "'" + values.primary_status + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            objdbconn.CloseConn();
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Email Address Added Successfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured";
                return false;
            }
        }

        public void DaGetEmailList(string employee_gid, MdlBuyerOnboardContactEmail values)
        {
            msSQL = "select email_address,contact2email_gid,primary_status from agr_mst_tbyronboardcontact2email where " +
              " contact_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcontactemail_list = new List<BuyerOnboardcontactemail_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcontactemail_list.Add(new BuyerOnboardcontactemail_list
                    {
                        contact2email_gid = (dr_datarow["contact2email_gid"].ToString()),
                        email_address = (dr_datarow["email_address"].ToString()),
                        primary_status = (dr_datarow["primary_status"].ToString()),
                    });
                }
            }
            values.BuyerOnboardcontactemail_list = getcontactemail_list;
            dt_datatable.Dispose();
        }

        public void DaEmailAddressDelete(string contact2email_gid, MdlBuyerOnboardContactEmail values)
        {
            msSQL = "delete from agr_mst_tbyronboardcontact2email where contact2email_gid='" + contact2email_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "Email Address Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }

        public bool DaAddressAdd(string employee_gid, MdlBuyerOnboardContactAddress values)
        {
            msSQL = "select primary_status from agr_mst_tbyronboardcontact2address where primary_status='Yes' and (contact_gid='" + employee_gid + "' or contact_gid = '" +values.contact_gid + "')";
            string lsprimary_status = objdbconn.GetExecuteScalar(msSQL);
            if (lsprimary_status == (values.primary_status))
            {
                values.status = false;
                values.message = "Already Primary Address Added";
                return false;
            }

            //msSQL = "select contact2address_gid from agr_mst_tbyronboardcontact2address where addresstype_name='" + values.addresstype_name + "' and " +
            //    " contact_gid='" + employee_gid + "' ";
            //objODBCDatareader = objdbconn.GetDataReader(msSQL);
            //if (objODBCDatareader.HasRows)
            //{
            //    objODBCDatareader.Close();
            //    values.status = false;
            //    values.message = "Already Address Type Added";
            //    return false;
            //}
            //objODBCDatareader.Close();
            msGetGid = objcmnfunctions.GetMasterGID("C2AD");
            msSQL = " insert into agr_mst_tbyronboardcontact2address(" +
                    " contact2address_gid," +
                    " contact_gid," +
                    " addresstype_gid," +
                    " addresstype_name," +
                    " primary_status," +
                    " addressline1," +
                    " addressline2," +
                    " landmark," +
                    " postal_code," +
                    " city," +
                    " taluka," +
                    " district," +
                    " state," +
                    " country," +
                    " latitude," +
                    " longitude," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + values.addresstype_gid + "'," +
                    "'" + values.addresstype_name + "'," +
                    "'" + values.primary_status + "'," +
                    "'" + values.addressline1 + "'," +
                    "'" + values.addressline2 + "'," +
                    "'" + values.landmark + "'," +
                    "'" + values.postal_code + "'," +
                    "'" + values.city + "'," +
                    "'" + values.taluka + "'," +
                    "'" + values.district + "'," +
                    "'" + values.state + "'," +
                    "'" + values.country + "'," +
                    "'" + values.latitude + "'," +
                    "'" + values.longitude + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                values.status = true;
                values.message = "Address Details Added Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
                return false;
            }

        }

        public void DaGetAddressList(string employee_gid, MdlBuyerOnboardContactAddress values)
        {
            msSQL = " select contact2address_gid,addresstype_name,primary_status, addressline1, addressline2, taluka, district, state, country, latitude, longitude," +
                    " postal_code from agr_mst_tbyronboardcontact2address where contact_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcontactaddress_list = new List<BuyerOnboardcontactaddress_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcontactaddress_list.Add(new BuyerOnboardcontactaddress_list
                    {
                        contact2address_gid = (dr_datarow["contact2address_gid"].ToString()),
                        addresstype_name = (dr_datarow["addresstype_name"].ToString()),
                        primary_status = (dr_datarow["primary_status"].ToString()),
                        addressline1 = (dr_datarow["addressline1"].ToString()),
                        addressline2 = (dr_datarow["addressline2"].ToString()),
                        taluka = (dr_datarow["taluka"].ToString()),
                        district = (dr_datarow["district"].ToString()),
                        state = (dr_datarow["state"].ToString()),
                        country = (dr_datarow["country"].ToString()),
                        latitude = (dr_datarow["latitude"].ToString()),
                        longitude = (dr_datarow["longitude"].ToString()),
                        postal_code = (dr_datarow["postal_code"].ToString())
                    });
                }
                values.BuyerOnboardcontactaddress_list = getcontactaddress_list;
            }
            dt_datatable.Dispose();
        }

        public void DaAddressDelete(string contact2address_gid, MdlBuyerOnboardContactAddress values)
        {
            msSQL = "delete from agr_mst_tbyronboardcontact2address where contact2address_gid='" + contact2address_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "Address Detail Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }


        public void DaIndividualSubmit(string employee_gid, MdlMstBuyerOnboardContact values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("CTCT");


            msSQL = "select application_gid from tmp_application where employee_gid='" + employee_gid + "'";
            lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select stakeholder_type from agr_mst_tbyronboardcontact where application_gid='" + lsapplication_gid + "' and stakeholder_type in ('Borrower','Applicant')";
            string lsstakeholder_type = objdbconn.GetExecuteScalar(msSQL);

            if (lsstakeholder_type == values.stakeholdertype_name)
            {

                values.status = false;
                values.message = "Applicant/Borrower Information Already Added";
                return;
            }

            msSQL = "select stakeholder_type from agr_mst_tbyronboardinstitution where application_gid='" + lsapplication_gid + "' and stakeholder_type in ('Borrower','Applicant')";
            lsstakeholder_type = objdbconn.GetExecuteScalar(msSQL);

            if (lsstakeholder_type == values.stakeholdertype_name)
            {

                values.status = false;
                values.message = "Applicant/Borrower Information Already Added";
                return;
            }

            msSQL = "select contact_gid from agr_mst_tbyronboardcontact2mobileno where contact_gid='" + employee_gid + "' and primary_status='Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Primary Mobile Number ";
                return;
            }
            objODBCDatareader.Close();

            msSQL = "select contact_gid from agr_mst_tbyronboardcontact2email where contact_gid='" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add primary Email Address";
                return;
            }
            objODBCDatareader.Close();
            msSQL = "select contact_gid from agr_mst_tbyronboardcontact2address where contact_gid='" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add primary Address";
                return;
            }
            objODBCDatareader.Close();
            msSQL = " insert into agr_mst_tbyronboardcontact(" +
                   " contact_gid," +
                   " application_gid," +
                   " application_no," +
                   " pan_status, " +
                   " pan_no," +
                   " aadhar_no," +
                   " first_name," +
                   " middle_name," +
                   " last_name," +
                   " individual_dob," +
                   " age," +
                   " gender_gid," +
                   " gender_name," +
                   " designation_gid," +
                   " designation_name," +
                   " educationalqualification_gid," +
                   " educationalqualification_name," +
                   " main_occupation," +
                   " annual_income," +
                   " monthly_income," +
                   " pep_status," +
                   " pepverified_date," +
                   " stakeholdertype_gid," +
                   " stakeholder_type," +
                   " maritalstatus_gid," +
                   " maritalstatus_name," +
                   " father_firstname," +
                   " father_middlename," +
                   " father_lastname," +
                   " father_dob," +
                   " father_age," +
                   " mother_firstname," +
                   " mother_middlename," +
                   " mother_lastname," +
                   " mother_dob," +
                   " mother_age," +
                   " spouse_firstname," +
                   " spouse_middlename," +
                   " spouse_lastname," +
                   " spouse_dob," +
                   " spouse_age," +
                   " ownershiptype_gid," +
                   " ownershiptype_name," +
                   " propertyholder_gid," +
                   " propertyholder_name," +
                   " residencetype_gid," +
                   " residencetype_name," +
                   " incometype_gid," +
                   " incometype_name," +
                   " currentresidence_years," +
                   " branch_distance," +
                   " group_gid," +
                   " group_name," +
                   " profile," +
                   " urn_status," +
                   " urn," +
                   " fathernominee_status," +
                   " mothernominee_status," +
                   " spousenominee_status," +
                   " othernominee_status," +
                   " relationshiptype," +
                   " nomineefirst_name," +
                   " nominee_middlename," +
                   " nominee_lastname," +
                   " nominee_dob," +
                   " nominee_age," +
                   " totallandinacres," +
                   " cultivatedland," +
                   " previouscrop," +
                   " prposedcrop," +
                   " institution_gid," +
                   " institution_name," +
                   " contact_status," +
                   " created_by," +
                   " created_date)" +
                   " values(" +
                   "'" + msGetGid + "'," +
                   "'" + lsapplication_gid + "'," +
                   "'" + employee_gid + "'," +
                   "'" + values.pan_status + "'," +
                   "'" + values.pan_no + "'," +
                   "'" + values.aadhar_no + "',";
            if (values.first_name == "" || values.first_name == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.first_name.Replace("'", "") + "',";
            }
            if (values.middle_name == "" || values.middle_name == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.middle_name.Replace("'", "") + "',";
            }
            if (values.last_name == "" || values.last_name == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.last_name.Replace("'", "") + "',";
            }
            msSQL += "'" + values.individual_dob + "'," +
                     "'" + values.age + "'," +
                     "'" + values.gender_gid + "'," +
                     "'" + values.gender_name + "'," +
                     "'" + values.designation_gid + "'," +
                     "'" + values.designation_name + "'," +
                     "'" + values.educationalqualification_gid + "'," +
                     "'" + values.educationalqualification_name + "'," +
                     "'" + values.main_occupation + "'," +
                     "'" + values.annual_income + "'," +
                     "'" + values.monthly_income + "'," +
                     "'" + values.pep_status + "',";

            if ((values.pepverified_date == null) || (values.pepverified_date == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.pepverified_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }



            msSQL += "'" + values.stakeholdertype_gid + "'," +
                "'" + values.stakeholdertype_name + "'," +
                     "'" + values.maritalstatus_gid + "'," +
                     "'" + values.maritalstatus_name + "',";
            if (values.father_firstname == "" || values.father_firstname == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.father_firstname.Replace("'", "") + "',";
            }
            if (values.father_middlename == "" || values.father_middlename == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.father_middlename.Replace("'", "") + "',";
            }
            if (values.father_lastname == "" || values.father_lastname == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.father_lastname.Replace("'", "") + "',";
            }
            msSQL += "'" + values.father_dob + "'," +
                     "'" + values.father_age + "',";
            if (values.mother_firstname == "" || values.mother_firstname == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.mother_firstname.Replace("'", "") + "',";
            }
            if (values.mother_middlename == "" || values.mother_middlename == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.mother_middlename.Replace("'", "") + "',";
            }
            if (values.mother_lastname == "" || values.mother_lastname == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.mother_lastname.Replace("'", "") + "',";
            }
            msSQL += "'" + values.mother_dob + "'," +
                     "'" + values.mother_age + "',";
            if (values.spouse_firstname == "" || values.spouse_firstname == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.spouse_firstname.Replace("'", "") + "',";
            }
            if (values.spouse_middlename == "" || values.spouse_middlename == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.spouse_middlename.Replace("'", "") + "',";
            }
            if (values.spouse_lastname == "" || values.spouse_lastname == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.spouse_lastname.Replace("'", "") + "',";
            }
            msSQL += "'" + values.spouse_dob + "'," +
                     "'" + values.spouse_age + "'," +
                     "'" + values.ownershiptype_gid + "'," +
                     "'" + values.ownershiptype_name + "'," +
                     "'" + values.propertyholder_gid + "'," +
                     "'" + values.propertyholder_name + "'," +
                     "'" + values.residencetype_gid + "'," +
                     "'" + values.residencetype_name + "'," +
                     "'" + values.incometype_gid + "'," +
                     "'" + values.incometype_name + "'," +
                     "'" + values.currentresidence_years + "'," +
                     "'" + values.branch_distance + "'," +
                     "'" + values.group_gid + "'," +
                     "'" + values.group_name + "'," +
                     "'" + values.profile + "'," +
                     "'" + values.urn_status + "'," +
                     "'" + values.urn + "'," +
                     "'" + values.fathernominee_status + "'," +
                     "'" + values.mothernominee_status + "'," +
                     "'" + values.spousenominee_status + "'," +
                     "'" + values.othernominee_status + "'," +
                     "'" + values.relationshiptype + "',";
            if (values.nomineefirst_name == "" || values.nomineefirst_name == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.nomineefirst_name.Replace("'", "") + "',";
            }
            if (values.nominee_middlename == "" || values.nominee_middlename == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.nominee_middlename.Replace("'", "") + "',";
            }
            if (values.nominee_lastname == "" || values.nominee_lastname == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.nominee_lastname.Replace("'", "") + "',";
            }
            msSQL += "'" + values.nominee_dob + "'," +
                     "'" + values.nominee_age + "'," +
                     "'" + values.totallandinacres + "'," +
                     "'" + values.cultivatedland + "'," +
                     "'" + values.previouscrop + "'," +
                     "'" + values.prposedcrop + "'," +
                     "'" + values.institution_gid + "'," +
                     "'" + values.institution_name + "'," +
                     "'Completed'," +
                     "'" + employee_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            if (mnResult != 0)
            {
                // PAN Update
                if (values.pan_status == "Customer Submitting Form 60")
                {
                    foreach (string reason in values.panabsencereason_selectedlist)
                    {
                        msGetGidpan = objcmnfunctions.GetMasterGID("C2PR");
                        msSQL = " INSERT INTO agr_mst_tbyronboardcontact2panabsencereason(" +
                               " contact2panabsencereason_gid," +
                               " contact_gid," +
                               " panabsencereason," +
                               " created_date," +
                               " created_by)" +
                               " VALUES(" +
                               "'" + msGetGidpan + "'," +
                               "'" + msGetGid + "'," +
                               "'" + reason + "'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                               "'" + employee_gid + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                //Updates

                msSQL = "update agr_mst_tbyronboardcontact2mobileno set contact_gid ='" + msGetGid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tbyronboardcontact2email set contact_gid ='" + msGetGid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsupronboardcontact2bankdtl set contact_gid='" + msGetGid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tbyronboardcontact2address set contact_gid ='" + msGetGid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tbyronboardcontact2idproof set contact_gid ='" + msGetGid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tbyronboardcontact2panform60 set contact_gid ='" + msGetGid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tbyronboardcontact2panabsencereason set contact_gid ='" + msGetGid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tbyronboardcontact2document set contact_gid ='" + msGetGid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycpanauthentication set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycpanaadhaarlink set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycdlauthentication set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycepicauthentication set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycpassportauthentication set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                msSQL = "select mobile_no from agr_mst_tbyronboardcontact2mobileno where contact_gid='" + msGetGid + "' and primary_status='yes'";
                lsmobileno = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select email_address from agr_mst_tbyronboardcontact2email where contact_gid='" + msGetGid + "' and primary_status='yes'";
                lsemail_address = objdbconn.GetExecuteScalar(msSQL);

                if (values.stakeholdertype_name == "Borrower" || values.stakeholdertype_name == "Applicant")
                {
                    msSQL = "update agr_mst_tbyronboard set applicant_type ='Individual' where application_gid='" + lsapplication_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_tbyronboardcontact set mobile_no='" + lsmobileno + "'," +
                        " email_address='" + lsemail_address + "' where contact_gid='" + msGetGid + "' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                values.status = true;
                values.message = "Individual Details Submitted Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
            }


        }


        public void DaGetGeneralInfo(string employee_gid, MdlMstBuyerOnboardApplicationAdd values)
        {
            msSQL = "select application_gid from tmp_application where employee_gid='" + employee_gid + "'";
            lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " select application_gid,if(application_no is null,'-',application_no) as application_no,customerref_name as customer_name,customer_urn,social_capital," +
                    " vertical_name,trade_capital,overalllimit_amount,processing_fee,doc_charges,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,status,applicant_type,hypothecation_flag,productcharge_flag, " +
                    " product_gid,variety_gid from agr_mst_tbyronboard a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid" +
                    " where a.application_gid='" + lsapplication_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.application_gid = objODBCDatareader["application_gid"].ToString();
                values.social_capital = objODBCDatareader["social_capital"].ToString();
                values.trade_capital = objODBCDatareader["trade_capital"].ToString();
                values.overalllimit_amount = objODBCDatareader["overalllimit_amount"].ToString();
                values.processing_fee = objODBCDatareader["processing_fee"].ToString();
                values.doc_charges = objODBCDatareader["doc_charges"].ToString();
                values.application_no = objODBCDatareader["application_no"].ToString();
                values.customer_name = objODBCDatareader["customer_name"].ToString();
                values.customer_urn = objODBCDatareader["customer_urn"].ToString();
                values.vertical_name = objODBCDatareader["vertical_name"].ToString();
                values.created_by = objODBCDatareader["created_by"].ToString();
                values.created_date = objODBCDatareader["created_date"].ToString();
                values.application_status = objODBCDatareader["status"].ToString();
                values.applicant_type = objODBCDatareader["applicant_type"].ToString();
                values.hypothecation_flag = objODBCDatareader["hypothecation_flag"].ToString();
                values.productcharge_flag = objODBCDatareader["productcharge_flag"].ToString();
                values.product_gid = objODBCDatareader["product_gid"].ToString();
                values.variety_gid = objODBCDatareader["variety_gid"].ToString();
            }
            objODBCDatareader.Close();
            values.status = true;
        }


        public void DaGetIndividualSummary(string employee_gid, MdlBuyerOnboardCICIndividual values)
        {
            msSQL = "select application_gid from tmp_application where employee_gid='" + employee_gid + "'";
            lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);
            if (lsapplication_gid != "")
            {
                msSQL = " select contact_gid,concat(first_name, ' ',middle_name,' ',last_name) as individual_name," +
                        " a.pan_no,aadhar_no,stakeholder_type,contact_status,institution_name,group_name," +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date" +
                        " from agr_mst_tbyronboardcontact a " +
                        " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                        " left join adm_mst_tuser c on c.user_gid=b.user_gid" +
                        " where a.application_gid='" + lsapplication_gid + "' order by contact_gid desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcicindividualList = new List<BuyerOnboardcicindividual_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getcicindividualList.Add(new BuyerOnboardcicindividual_list
                        {
                            contact_gid = dt["contact_gid"].ToString(),
                            individual_name = dt["individual_name"].ToString(),
                            pan_no = dt["pan_no"].ToString(),
                            aadhar_no = dt["aadhar_no"].ToString(),
                            stakeholder_type = dt["stakeholder_type"].ToString(),
                            created_date = dt["created_date"].ToString(),
                            created_by = dt["created_by"].ToString(),
                            contact_status = dt["contact_status"].ToString(),
                            institution_name = dt["institution_name"].ToString(),
                            group_name = dt["group_name"].ToString(),
                        });

                    }
                }
                values.BuyerOnboardcicindividual_list = getcicindividualList;
                dt_datatable.Dispose();
            }
            values.status = true;

        }

        public void DaGetInstitutionSummary(string employee_gid, MdlBuyerOnboardCICInstitution values)
        {
            msSQL = "select application_gid from tmp_application where employee_gid='" + employee_gid + "'";
            lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);

            if (lsapplication_gid != "")
            {
                msSQL = " select institution_gid,company_name,date_incorporation,stakeholder_type,institution_status,businessstart_date," +
                   " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date" +
                   " from agr_mst_tbyronboard2institution a " +
                   " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                   " left join adm_mst_tuser c on c.user_gid=b.user_gid" +
                   " where a.application_gid='" + lsapplication_gid + "' order by institution_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcicinstitutionList = new List<BuyerOnboardcicinstitution_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getcicinstitutionList.Add(new BuyerOnboardcicinstitution_list
                        {
                            institution_gid = dt["institution_gid"].ToString(),
                            company_name = dt["company_name"].ToString(),
                            date_incorporation = dt["date_incorporation"].ToString(),
                            stakeholder_type = dt["stakeholder_type"].ToString(),
                            created_date = dt["created_date"].ToString(),
                            created_by = dt["created_by"].ToString(),
                            institution_status = dt["institution_status"].ToString(),
                            businessstart_date = dt["businessstart_date"].ToString(),
                        });

                    }
                }
                values.BuyerOnboardcicinstitution_list = getcicinstitutionList;
                dt_datatable.Dispose();
            }
            values.status = true;

        }

        public void DabyronboardTmpClear(string employee_gid, MdlMstBuyerOnboardApplicationAdd values)
        {
            msSQL = "delete from agr_mst_tbyronboard2product where application_gid ='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            msSQL = "delete from agr_mst_tbyronboard2contactno where application_gid ='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from agr_mst_tbyronboard2email where application_gid ='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from agr_mst_tbyronboard2geneticcode where application_gid ='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from agr_mst_tbyronboard where application_gid ='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from agr_mst_tbyronboardinstitution2branch where institution_gid ='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " delete from agr_mst_tbyronboardinstitution2ratingdetail  where institution_gid ='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " delete from agr_mst_tbyronboardinstitution2mobileno where institution_gid ='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " delete from agr_mst_tbyronboardinstitution2bankdtl where institution_gid ='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " delete from agr_mst_tbyronboardinstitution2documentupload where institution_gid ='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " delete from agr_mst_tbyronboardinstitution2licensedtl where institution_gid ='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " delete from agr_mst_tbyronboard2institution  where application_gid ='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " delete from agr_mst_tbyronboardcontact2mobileno  where contact_gid ='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " delete from agr_mst_tbyronboardcontact2idproof  where contact_gid ='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " delete from agr_mst_tbyronboardcontact2address  where contact_gid ='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " delete from agr_mst_tbyronboardcontact2document where contact_gid ='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " delete from agr_mst_tbyronboardcontact where application_gid ='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " delete from agr_mst_tbyronboardinstitution2address where institution_gid ='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " delete from agr_mst_tbyronboardcontact2email where contact_gid ='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " delete from agr_mst_tbyronboardcontact2panabsencereason where contact_gid ='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " delete from agr_mst_tbyronboardcontact2panform60 where contact_gid ='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " delete from agr_mst_tbyronboardinstitution2email where institution_gid ='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " delete from agr_mst_tbyronboardcontact2bankdtl where contact_gid ='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            //msSQL = "delete from tmp_application where employee_gid='" + employee_gid + "'";
            //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            values.status = true;
        }

        public void DaGetOnboardApplicationGeneralInfo(string application_gid, MdlMstOnboardApplicationView values)
        {
            try
            {
                msSQL = " select a.application_gid, a.application_no, customer_urn, customerref_name as customer_name, vertical_name, verticaltaggs_name,a.cccompleted_flag, " +
                        " constitution_name, businessunit_name, vernacular_language, sa_status, sa_id, sa_name, a.social_capital, a.trade_capital," +
                        " designation_type, landline_no, concat_ws(' ', contactpersonfirst_name, contactpersonmiddle_name, contactpersonlast_name) as contactperson_name," +
                        "  a.region, date_format(a.headapproval_date,'%d-%m-%Y %h:%i %p') as businessapproved_date, date_format(a.cccompleted_date,'%d-%m-%Y %h:%i %p') as ccapproved_date," +
                        " momapproval_flag,approval_status,creditgroup_name, date_format(a.initiated_date,'%d-%m-%Y %h:%i %p') as initiated_date, initiated_remarks, " +
                        " docchecklist_makerflag,docchecklist_checkerflag,docchecklist_approvalflag,product_gid,product_name, " +
                        " sector_name,category_name,variety_gid,variety_name,botanical_name,alternative_name,program_gid,program_name, " +
                        " case when e.urn = '' then d.urn else e.urn end as customer_urnno, a.vertical_name, a.customerref_name, a.buyersuppliertype_name from agr_mst_tbyronboard a" +
                        " left join agr_mst_tbyronboard2institution d on d.application_gid = a.application_gid " +
                        " left join agr_mst_tbyronboardcontact e on e.application_gid = a.application_gid " +
                        " where a.application_gid='" + application_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.application_gid = objODBCDatareader["application_gid"].ToString();
                    values.application_no = objODBCDatareader["application_no"].ToString();
                    values.customer_urn = objODBCDatareader["customer_urn"].ToString();
                    values.customer_name = objODBCDatareader["customer_name"].ToString();
                    values.vertical_name = objODBCDatareader["vertical_name"].ToString();
                    values.verticaltaggs_name = objODBCDatareader["verticaltaggs_name"].ToString();
                    values.constitution_name = objODBCDatareader["constitution_name"].ToString();
                    values.businessunit_name = objODBCDatareader["businessunit_name"].ToString();
                    values.vernacular_language = objODBCDatareader["vernacular_language"].ToString();
                    values.sa_status = objODBCDatareader["sa_status"].ToString();
                    values.sa_id = objODBCDatareader["sa_id"].ToString();
                    values.sa_name = objODBCDatareader["sa_name"].ToString();
                    values.landline_no = objODBCDatareader["landline_no"].ToString();
                    values.designation_type = objODBCDatareader["designation_type"].ToString();
                    values.contactperson_name = objODBCDatareader["contactperson_name"].ToString();
                    values.social_capital = objODBCDatareader["social_capital"].ToString();
                    values.trade_capital = objODBCDatareader["trade_capital"].ToString();
                    values.momapproval_flag = objODBCDatareader["momapproval_flag"].ToString();
                    values.approval_status = objODBCDatareader["approval_status"].ToString();
                    values.creditgroup_name = objODBCDatareader["creditgroup_name"].ToString();
                    values.businessapproved_date = objODBCDatareader["businessapproved_date"].ToString();
                    values.ccapproved_date = objODBCDatareader["ccapproved_date"].ToString();
                    values.region = objODBCDatareader["region"].ToString();
                    values.product_gid = objODBCDatareader["product_gid"].ToString();
                    values.product_name = objODBCDatareader["product_name"].ToString();
                    values.sector_name = objODBCDatareader["sector_name"].ToString();
                    values.category_name = objODBCDatareader["category_name"].ToString();
                    values.variety_gid = objODBCDatareader["variety_gid"].ToString();
                    values.variety_name = objODBCDatareader["variety_name"].ToString();
                    values.botanical_name = objODBCDatareader["botanical_name"].ToString();
                    values.alternative_name = objODBCDatareader["alternative_name"].ToString();
                    values.program_gid = objODBCDatareader["program_gid"].ToString();
                    values.program_name = objODBCDatareader["program_name"].ToString();
                    values.customer_urnno = objODBCDatareader["customer_urnno"].ToString();
                    values.cccompleted_flag = objODBCDatareader["cccompleted_flag"].ToString();
                    values.application_initiateddate = objODBCDatareader["initiated_date"].ToString();
                    values.application_initiatedremarks = objODBCDatareader["initiated_remarks"].ToString();
                    values.vertical_name = objODBCDatareader["vertical_name"].ToString();
                    values.customerref_name = objODBCDatareader["customerref_name"].ToString();
                    values.buyersuppliertype_name = objODBCDatareader["buyersuppliertype_name"].ToString();
                }

                objODBCDatareader.Close();

                msSQL = " select application_gid from agr_mst_tbyronboard2institution " +
                        " where application_gid='" + application_gid + "' and (stakeholder_type='Applicant' or stakeholder_type='Borrower')";
                lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);

                if (lsapplication_gid != "")
                {
                    values.borrower_flag = "Y";
                    values.borrower_type = "Institution";
                }
                else
                {
                    msSQL = " select application_gid from agr_mst_tbyronboardcontact " +
                            " where application_gid='" + application_gid + "' and (stakeholder_type='Applicant' or stakeholder_type='Borrower')";
                    string lsapplication_gidcontact = objdbconn.GetExecuteScalar(msSQL);
                    if (lsapplication_gidcontact != "")
                    {
                        values.borrower_flag = "N";
                        values.borrower_type = "Individual";
                    }
                    else
                    {
                        values.borrower_type = "";
                        values.borrower_flag = "";
                    }
                }

                msSQL = "select mobile_no from agr_mst_tbyronboard2contactno where application_gid='" + application_gid + "' and primary_mobileno = 'Yes'";
                values.primary_mobileno = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select email_address from agr_mst_tbyronboard2email where application_gid='" + application_gid + "' and primary_emailaddress = 'Yes'";
                values.primary_email = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select application2contact_gid, application_gid, mobile_no, whatsapp_mobileno " +
                        " from agr_mst_tbyronboard2contactno " +
                        " where application_gid = '" + application_gid + "' and primary_mobileno = 'No' ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getapplication_list = new List<mobilenumber_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getapplication_list.Add(new mobilenumber_list
                        {
                            application_gid = (dr_datarow["application_gid"].ToString()),
                            mobile_no = (dr_datarow["mobile_no"].ToString()),
                            whatsapp_mobileno = (dr_datarow["whatsapp_mobileno"].ToString()),

                        });
                    }
                    values.mobilenumber_list = getapplication_list;
                }
                dt_datatable.Dispose();
                values.status = true;

                msSQL = " select application2email_gid, application_gid, email_address " +
                        " from agr_mst_tbyronboard2email " +
                        " where application_gid = '" + application_gid + "' and primary_emailaddress = 'No' ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmail_list = new List<mail_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmail_list.Add(new mail_list
                        {
                            application_gid = (dr_datarow["application_gid"].ToString()),
                            application2email_gid = (dr_datarow["application2email_gid"].ToString()),
                            email_address = (dr_datarow["email_address"].ToString()),

                        });
                    }
                    values.mail_list = getmail_list;
                }
                dt_datatable.Dispose();
                values.status = true;

                msSQL = " select geneticcode_name, genetic_status, genetic_remarks, application_gid, geneticcode_gid " +
                       " from agr_mst_tbyronboard2geneticcode " +
                       " where application_gid = '" + application_gid + "' ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getgenetic_list = new List<geneticdetails_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getgenetic_list.Add(new geneticdetails_list
                        {
                            application_gid = (dr_datarow["application_gid"].ToString()),
                            geneticcode_name = (dr_datarow["geneticcode_name"].ToString()),
                            genetic_status = (dr_datarow["genetic_status"].ToString()),
                            genetic_remarks = (dr_datarow["genetic_remarks"].ToString()),

                        });
                    }
                    values.geneticdetails_list = getgenetic_list;
                }
                dt_datatable.Dispose();
                msSQL = " select application2product_gid,product_gid,product_name,variety_gid,variety_name,sector_name,category_name,hsn_code, " +
                         " botanical_name,alternative_name from agr_mst_tbyronboard2product where application_gid='" + application_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmstproduct_list = new List<mstproduct_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmstproduct_list.Add(new mstproduct_list
                        {
                            application2product_gid = (dr_datarow["application2product_gid"].ToString()),
                            product_gid = (dr_datarow["product_gid"].ToString()),
                            product_name = (dr_datarow["product_name"].ToString()),
                            variety_gid = (dr_datarow["variety_gid"].ToString()),
                            variety_name = (dr_datarow["variety_name"].ToString()),
                            sector_name = (dr_datarow["sector_name"].ToString()),
                            category_name = (dr_datarow["category_name"].ToString()),
                            botanical_name = (dr_datarow["botanical_name"].ToString()),
                            alternative_name = (dr_datarow["alternative_name"].ToString()),
                            hsn_code = (dr_datarow["hsn_code"].ToString())
                        });
                    }
                    values.mstproduct_list = getmstproduct_list;
                }
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


        public void DaGetOnboardIndividualInfo(string application_gid, MdlMstOnboardIndividual values)
        {
            msSQL = " select a.contact_gid, a.application_gid, concat_ws(' ', first_name, last_name, middle_name) as individual_name, " +
                        " a.pan_no, aadhar_no, date_format(individual_dob, '%d-%m-%Y') as individual_dob," +
                        " main_occupation, date_format(a.created_date, '%d-%m-%Y') as created_date," +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,stakeholder_type from agr_mst_tbyronboardcontact a" +
                        " left join hrm_mst_temployee b on b.employee_gid = a.created_by" +
                        " left join adm_mst_tuser c on b.user_gid = c.user_gid" +
                        " where application_gid = '" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getIndividualList = new List<OnboardIndividual_List>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getIndividualList.Add(new OnboardIndividual_List
                    {
                        contact_gid = (dr_datarow["contact_gid"].ToString()),
                        individual_name = (dr_datarow["individual_name"].ToString()),
                        pan_no = (dr_datarow["pan_no"].ToString()),
                        aadhar_no = (dr_datarow["aadhar_no"].ToString()),
                        individual_dob = (dr_datarow["individual_dob"].ToString()),
                        main_occupation = (dr_datarow["main_occupation"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        stakeholder_type = (dr_datarow["stakeholder_type"].ToString()),
                    });
                }
                values.OnboardIndividual_List = getIndividualList;
            }
            dt_datatable.Dispose();
            values.status = true;
        }

        public void DaGetOnboardInstitutionInfo(string application_gid, MdlMstOnboardInstitution values)
        {
            msSQL = " select a.institution_gid, a.application_gid, cin_no, companytype_name, " +
                        " company_name, companypan_no, date_format(date_incorporation, '%d-%m-%Y') as date_incorporation," +
                        " date_format(a.created_date, '%d-%m-%Y') as created_date," +
                        "concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,stakeholder_type from agr_mst_tbyronboard2institution a" +
                        " left join hrm_mst_temployee b on b.employee_gid = a.created_by" +
                        " left join adm_mst_tuser c on b.user_gid = c.user_gid" +
                        " where application_gid = '" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getInstitutionList = new List<OnboardInstitution_List>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getInstitutionList.Add(new OnboardInstitution_List
                    {
                        institution_gid = (dr_datarow["institution_gid"].ToString()),
                        company_name = (dr_datarow["company_name"].ToString()),
                        companypan_no = (dr_datarow["companypan_no"].ToString()),
                        cin_no = (dr_datarow["cin_no"].ToString()),
                        companytype_name = (dr_datarow["companytype_name"].ToString()),
                        date_incorporation = (dr_datarow["date_incorporation"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        stakeholder_type = (dr_datarow["stakeholder_type"].ToString()),
                    });
                }
                values.OnboardInstitution_List = getInstitutionList;
            }
            dt_datatable.Dispose();
            values.status = true;
        }

        public void DaGetOnboardInstitutionView(string institution_gid, MdlMstInstitutionDtlView values)
        {
            try
            {
                msSQL = " select institution_gid, application_gid, company_name, companypan_no, date_format(date_incorporation, '%d-%m-%Y') as date_incorporation, " +
                        " year_business, month_business, cin_no, official_telephoneno, officialemail_address, companytype_name, escrow, tan_number, stakeholder_type,incometax_returnsstatus, revenue, profit, fixed_assets, sundrydebt_adv,  " +
                        " lastyear_turnover, date_format(start_date, '%d-%m-%Y') as start_date, " +
                        " date_format(end_date, '%d-%m-%Y') as end_date, assessmentagency_name, " +
                        " assessmentagencyrating_name, date_format(ratingas_on, '%d-%m-%Y') as ratingas_on, " +
                        " amlcategory_name, businesscategory_name, urn_status, urn,  msme_registration,lei_renewaldate,kin,lglentity_id," +
                        " contactperson_firstname,contactperson_middlename,contactperson_lastname,designation, " +
                        " date_format(businessstart_date, '%d-%m-%Y') as businessstart_date from agr_mst_tbyronboard2institution " +
                        " where institution_gid = '" + institution_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.application_gid = objODBCDatareader["application_gid"].ToString();
                    values.institution_gid = objODBCDatareader["institution_gid"].ToString();
                    values.company_name = objODBCDatareader["company_name"].ToString();
                    values.companypan_no = objODBCDatareader["companypan_no"].ToString();
                    values.date_incorporation = objODBCDatareader["date_incorporation"].ToString();
                    values.year_business = objODBCDatareader["year_business"].ToString();
                    values.month_business = objODBCDatareader["month_business"].ToString();
                    values.cin_no = objODBCDatareader["cin_no"].ToString();
                    values.official_telephoneno = objODBCDatareader["official_telephoneno"].ToString();
                    values.officialemail_address = objODBCDatareader["officialemail_address"].ToString();
                    values.companytype_name = objODBCDatareader["companytype_name"].ToString();
                    values.escrow = objODBCDatareader["escrow"].ToString();
                    values.lastyear_turnover = objODBCDatareader["lastyear_turnover"].ToString();
                    values.start_date = objODBCDatareader["start_date"].ToString();
                    values.end_date = objODBCDatareader["end_date"].ToString();
                    values.assessmentagency_name = objODBCDatareader["assessmentagency_name"].ToString();
                    values.assessmentagencyrating_name = objODBCDatareader["assessmentagencyrating_name"].ToString();
                    values.ratingas_on = objODBCDatareader["ratingas_on"].ToString();
                    values.amlcategory_name = objODBCDatareader["amlcategory_name"].ToString();
                    values.businesscategory_name = objODBCDatareader["businesscategory_name"].ToString();
                    values.borrower_type = "Institution";
                    values.urn_status = objODBCDatareader["urn_status"].ToString();
                    values.urn = objODBCDatareader["urn"].ToString();
                    values.contactperson_firstname = objODBCDatareader["contactperson_firstname"].ToString();
                    values.contactperson_middlename = objODBCDatareader["contactperson_middlename"].ToString();
                    values.contactperson_lastname = objODBCDatareader["contactperson_lastname"].ToString();
                    values.designation = objODBCDatareader["designation"].ToString();
                    values.businessstart_date = objODBCDatareader["businessstart_date"].ToString();
                    values.tan_number = objODBCDatareader["tan_number"].ToString();
                    values.stakeholder_type = objODBCDatareader["stakeholder_type"].ToString();
                    values.revenue = objODBCDatareader["revenue"].ToString();
                    values.incometax_returnsstatus = objODBCDatareader["incometax_returnsstatus"].ToString();
                    values.profit = objODBCDatareader["profit"].ToString();
                    values.fixed_assets = objODBCDatareader["fixed_assets"].ToString();
                    values.sundrydebt_adv = objODBCDatareader["sundrydebt_adv"].ToString();
                    if (objODBCDatareader["lei_renewaldate"].ToString() == "")
                    {
                    }
                    else
                    {
                        values.editlei_renewaldate = Convert.ToDateTime(objODBCDatareader["lei_renewaldate"]).ToString("dd-MM-yyyy");
                    }
                    values.msme_registration = objODBCDatareader["msme_registration"].ToString();
                    values.lglentity_id = objODBCDatareader["lglentity_id"].ToString();
                    values.kin = objODBCDatareader["kin"].ToString();
                }

                objODBCDatareader.Close();

                msSQL = "select institution2branch_gid,gst_state,gst_no, gst_registered,headoffice_status from agr_mst_tbyronboardinstitution2branch where institution_gid='" + institution_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmstgst_list = new List<mstgst_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmstgst_list.Add(new mstgst_list
                        {
                            institution2branch_gid = (dr_datarow["institution2branch_gid"].ToString()),
                            gst_state = (dr_datarow["gst_state"].ToString()),
                            gst_no = (dr_datarow["gst_no"].ToString()),
                            gst_registered = (dr_datarow["gst_registered"].ToString()),
                            headoffice_status = (dr_datarow["headoffice_status"].ToString())
                        });
                    }
                    values.mstgst_list = getmstgst_list;
                }
                dt_datatable.Dispose();

                msSQL = "  select institution2address_gid,addresstype_name,primary_status, addressline1, addressline2, taluka, district, state, country, landmark," +
                    " postal_code, city,latitude,longitude from agr_mst_tbyronboardinstitution2address where institution_gid='" + institution_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmstaddress_list = new List<mstaddress_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmstaddress_list.Add(new mstaddress_list
                        {
                            institution2address_gid = (dr_datarow["institution2address_gid"].ToString()),
                            address_type = (dr_datarow["addresstype_name"].ToString()),
                            primary_status = (dr_datarow["primary_status"].ToString()),
                            addressline1 = (dr_datarow["addressline1"].ToString()),
                            addressline2 = (dr_datarow["addressline2"].ToString()),
                            taluka = (dr_datarow["taluka"].ToString()),
                            district = (dr_datarow["district"].ToString()),
                            state = (dr_datarow["state"].ToString()),
                            country = (dr_datarow["country"].ToString()),
                            postal_code = (dr_datarow["postal_code"].ToString()),
                            city = (dr_datarow["city"].ToString()),
                            landmark = (dr_datarow["landmark"].ToString()),
                            latitude = (dr_datarow["latitude"].ToString()),
                            longitude = (dr_datarow["longitude"].ToString()),
                        });
                    }
                    values.mstaddress_list = getmstaddress_list;
                }
                dt_datatable.Dispose();


                msSQL = "select mobile_no from agr_mst_tbyronboardinstitution2mobileno where institution_gid='" + institution_gid + "' and primary_status = 'Yes'";
                values.primaryinstitution_mobileno = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select email_address from agr_mst_tbyronboardinstitution2email  where institution_gid='" + institution_gid + "' and primary_status = 'Yes'";
                values.primaryinstitution_email = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select institution_gid, mobile_no, whatsapp_no, primary_status " +
                        " from agr_mst_tbyronboardinstitution2mobileno " +
                        " where institution_gid = '" + institution_gid + "' and primary_status = 'Yes' ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getapplication_list = new List<instituionmobilenumber_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getapplication_list.Add(new instituionmobilenumber_list
                        {
                            institution_gid = (dr_datarow["institution_gid"].ToString()),
                            primary_status = (dr_datarow["primary_status"].ToString()),
                            mobile_no = (dr_datarow["mobile_no"].ToString()),
                            whatsapp_no = (dr_datarow["whatsapp_no"].ToString()),

                        });
                    }
                    values.instituionmobilenumber_list = getapplication_list;
                }
                dt_datatable.Dispose();
                values.status = true;

                msSQL = " select institution_gid, email_address, primary_status " +
                        " from agr_mst_tbyronboardinstitution2email " +
                        " where institution_gid = '" + institution_gid + "' and primary_status = 'Yes' ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmail_list = new List<instituionmail_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmail_list.Add(new instituionmail_list
                        {
                            primary_status = (dr_datarow["primary_status"].ToString()),
                            email_address = (dr_datarow["email_address"].ToString()),

                        });
                    }
                    values.instituionmail_list = getmail_list;
                }
                dt_datatable.Dispose();


                msSQL = " select institution2form60documentupload_gid,form60document_name,form60document_path from agr_mst_tbyronboardinstitution2form60documentupload " +
                               " where institution_gid ='" + institution_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getdocumentdtlList = new List<institutionform60_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getdocumentdtlList.Add(new institutionform60_list
                        {
                            document_name = dt["form60document_name"].ToString(),
                            document_path = objcmnstorage.EncryptData((dt["form60document_path"].ToString())),
                            institution2form60documentupload_gid = dt["institution2form60documentupload_gid"].ToString()
                        });
                        values.institutionform60_list = getdocumentdtlList;
                    }
                }
                dt_datatable.Dispose();

                msSQL = " select institution2documentupload_gid,institution_gid,document_name,document_path,document_title,document_id from agr_mst_tbyronboardinstitution2documentupload " +
                        " where institution_gid='" + institution_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getdocumentList = new List<institutiondoc_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getdocumentList.Add(new institutiondoc_list
                        {
                            document_name = dt["document_name"].ToString(),
                            document_path = objcmnstorage.EncryptData((dt["document_path"].ToString())),
                            institution2documentupload_gid = dt["institution2documentupload_gid"].ToString(),
                            document_title = dt["document_title"].ToString(),
                            document_id = dt["document_id"].ToString(),
                        });
                        values.institutiondoc_list = getdocumentList;
                    }
                }
                dt_datatable.Dispose();

                msSQL = " select institution2licensedtl_gid,licensetype_gid,licensetype_name,license_no,date_format(issue_date,'%d-%m-%Y') as issue_date," +
                        " date_format(expiry_date,'%d-%m-%Y') as expiry_date from agr_mst_tbyronboardinstitution2licensedtl" +
                        " where institution_gid='" + institution_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmstlicense_list = new List<mstlicense_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmstlicense_list.Add(new mstlicense_list
                        {
                            institution2licensedtl_gid = (dr_datarow["institution2licensedtl_gid"].ToString()),
                            licensetype_gid = (dr_datarow["licensetype_gid"].ToString()),
                            licensetype_name = (dr_datarow["licensetype_name"].ToString()),
                            license_number = (dr_datarow["license_no"].ToString()),
                            licenseissue_date = (dr_datarow["issue_date"].ToString()),
                            licenseexpiry_date = (dr_datarow["expiry_date"].ToString())
                        });
                    }
                    values.mstlicense_list = getmstlicense_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }
        }

        public void DaGetGurantorIndividualView(string contact_gid, MdlMstIndividualDtlView values)
        {
            try
            {
                msSQL = " select contact_gid, application_gid, concat_ws(' ', first_name, last_name, middle_name) as individual_name,stakeholder_type," +
                        " pan_no, aadhar_no, individual_dob," +
                        " age, gender_name, designation_name, main_occupation, pep_status, date_format(pepverified_date, '%d-%m-%Y') as pepverified_date, " +
                        " maritalstatus_name, concat_ws(' ', father_firstname, father_middlename, father_lastname) as father_name, " +
                        " father_dob, father_age, " +
                        " concat_ws(' ', mother_firstname, mother_middlename, mother_lastname) as mother_name, " +
                        " mother_dob, mother_age, " +
                        " concat_ws(' ', spouse_firstname, spouse_middlename, spouse_lastname) as spouse_name, " +
                        " spouse_dob, spouse_age, educationalqualification_name, " +
                        " annual_income, monthly_income, incometype_name as user_type, ownershiptype_name, propertyholder_name, residencetype_name, " +
                        " currentresidence_years, branch_distance, bureauname_name, bureau_score, observations, " +
                        " date_format(bureauscore_date, '%d-%m-%Y') as bureauscore_date, bureau_response,pan_status, pep_status, " +
                        " group_name, profile, urn_status, urn, fathernominee_status, mothernominee_status, spousenominee_status, othernominee_status,institution_name," +
                        " relationshiptype, nomineefirst_name, nominee_middlename, nominee_lastname, nominee_dob, nominee_age, totallandinacres, cultivatedland, previouscrop, prposedcrop" +
                        " from agr_mst_tbyronboardcontact " +
                        " where contact_gid = '" + contact_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.application_gid = objODBCDatareader["application_gid"].ToString();
                    values.contact_gid = objODBCDatareader["contact_gid"].ToString();
                    values.individual_name = objODBCDatareader["individual_name"].ToString();
                    values.pan_no = objODBCDatareader["pan_no"].ToString();
                    values.aadhar_no = objODBCDatareader["aadhar_no"].ToString();
                    values.individual_dob = objODBCDatareader["individual_dob"].ToString();
                    values.age = objODBCDatareader["age"].ToString();
                    values.gender_name = objODBCDatareader["gender_name"].ToString();
                    values.designation_name = objODBCDatareader["designation_name"].ToString();
                    values.main_occupation = objODBCDatareader["main_occupation"].ToString();
                    values.pep_status = objODBCDatareader["pep_status"].ToString();
                    values.pepverified_date = objODBCDatareader["pepverified_date"].ToString();
                    values.maritalstatus_name = objODBCDatareader["maritalstatus_name"].ToString();
                    values.father_name = objODBCDatareader["father_name"].ToString();
                    values.father_dob = objODBCDatareader["father_dob"].ToString();
                    values.father_age = objODBCDatareader["father_age"].ToString();
                    values.mother_name = objODBCDatareader["mother_name"].ToString();
                    values.mother_dob = objODBCDatareader["mother_dob"].ToString();
                    values.mother_age = objODBCDatareader["mother_age"].ToString();
                    values.spouse_name = objODBCDatareader["spouse_name"].ToString();
                    values.spouse_dob = objODBCDatareader["spouse_dob"].ToString();
                    values.spouse_age = objODBCDatareader["spouse_age"].ToString();
                    values.educationalqualification_name = objODBCDatareader["educationalqualification_name"].ToString();
                    values.annual_income = objODBCDatareader["annual_income"].ToString();
                    values.monthly_income = objODBCDatareader["monthly_income"].ToString();
                    values.user_type = objODBCDatareader["user_type"].ToString();
                    values.ownershiptype_name = objODBCDatareader["ownershiptype_name"].ToString();
                    values.propertyholder_name = objODBCDatareader["propertyholder_name"].ToString();
                    values.residencetype_name = objODBCDatareader["residencetype_name"].ToString();
                    values.currentresidence_years = objODBCDatareader["currentresidence_years"].ToString();
                    values.branch_distance = objODBCDatareader["branch_distance"].ToString();
                    values.indbureauname_name = objODBCDatareader["bureauname_name"].ToString();
                    values.indbureau_score = objODBCDatareader["bureau_score"].ToString();
                    values.indobservations = objODBCDatareader["observations"].ToString();
                    values.indbureauscore_date = objODBCDatareader["bureauscore_date"].ToString();
                    values.indbureau_response = objODBCDatareader["bureau_response"].ToString();
                    values.borrower_type = "Individual";

                    values.group_name = objODBCDatareader["group_name"].ToString();
                    values.profile = objODBCDatareader["profile"].ToString();
                    values.urn_status = objODBCDatareader["urn_status"].ToString();
                    values.urn = objODBCDatareader["urn"].ToString();
                    values.fathernominee_status = objODBCDatareader["fathernominee_status"].ToString();
                    values.mothernominee_status = objODBCDatareader["mothernominee_status"].ToString();
                    values.spousenominee_status = objODBCDatareader["spousenominee_status"].ToString();
                    values.othernominee_status = objODBCDatareader["othernominee_status"].ToString();
                    values.relationshiptype = objODBCDatareader["relationshiptype"].ToString();
                    values.nomineefirst_name = objODBCDatareader["nomineefirst_name"].ToString();
                    values.nominee_middlename = objODBCDatareader["nominee_middlename"].ToString();
                    values.nominee_lastname = objODBCDatareader["nominee_lastname"].ToString();
                    values.nominee_dob = objODBCDatareader["nominee_dob"].ToString();
                    values.nominee_age = objODBCDatareader["nominee_age"].ToString();
                    values.totallandinacres = objODBCDatareader["totallandinacres"].ToString();
                    values.cultivatedland = objODBCDatareader["cultivatedland"].ToString();
                    values.previouscrop = objODBCDatareader["previouscrop"].ToString();
                    values.prposedcrop = objODBCDatareader["prposedcrop"].ToString();
                    values.institution_name = objODBCDatareader["institution_name"].ToString();
                    values.pan_status = objODBCDatareader["pan_status"].ToString();
                    values.stakeholder_type = objODBCDatareader["stakeholder_type"].ToString();
                    values.pep_status = objODBCDatareader["pep_status"].ToString();
                }

                objODBCDatareader.Close();

                msSQL = " SELECT panabsencereason" +
              " from agr_mst_tbyronboardcontact2panabsencereason where contact_gid='" + contact_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcontactpanabsencereasons_list = new List<contactpanabsencereasons_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    values.contactpanabsencereasons_list = dt_datatable.AsEnumerable().Select(row =>
                      new contactpanabsencereasons_list
                      {
                          panabsencereason = row["panabsencereason"].ToString(),
                      }
                    ).ToList();
                }
                dt_datatable.Dispose();

                msSQL = " select contact2address_gid,addresstype_name,primary_status, addressline1, addressline2, taluka, district, state, country," +
                   " postal_code, landmark, city, latitude,longitude from agr_mst_tbyronboardcontact2address where contact_gid='" + contact_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcontactaddress_list = new List<contactaddress_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcontactaddress_list.Add(new contactaddress_list
                        {
                            contact2address_gid = (dr_datarow["contact2address_gid"].ToString()),
                            addresstype_name = (dr_datarow["addresstype_name"].ToString()),
                            primary_status = (dr_datarow["primary_status"].ToString()),
                            addressline1 = (dr_datarow["addressline1"].ToString()),
                            addressline2 = (dr_datarow["addressline2"].ToString()),
                            taluka = (dr_datarow["taluka"].ToString()),
                            district = (dr_datarow["district"].ToString()),
                            state = (dr_datarow["state"].ToString()),
                            country = (dr_datarow["country"].ToString()),
                            postal_code = (dr_datarow["postal_code"].ToString()),
                            landmark = (dr_datarow["landmark"].ToString()),
                            city = (dr_datarow["city"].ToString()),
                            latitude = (dr_datarow["latitude"].ToString()),
                            longitude = (dr_datarow["longitude"].ToString()),
                        });
                    }
                    values.contactaddress_list = getcontactaddress_list;
                }
                dt_datatable.Dispose();


                msSQL = "select mobile_no from agr_mst_tbyronboardcontact2mobileno where contact_gid='" + contact_gid + "' and primary_status = 'Yes'";
                values.primaryindividual_mobileno = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select email_address from agr_mst_tbyronboardcontact2email where contact_gid='" + contact_gid + "' and primary_status = 'Yes'";
                values.primaryindividual_email = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select email_address,contact2email_gid,primary_status from agr_mst_tbyronboardcontact2email where " +
                         " contact_gid='" + contact_gid + "' and primary_status = 'Yes'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcontactemail_list = new List<contactemail_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcontactemail_list.Add(new contactemail_list
                        {
                            contact2email_gid = (dr_datarow["contact2email_gid"].ToString()),
                            email_address = (dr_datarow["email_address"].ToString()),
                            primary_status = (dr_datarow["primary_status"].ToString()),
                        });
                    }
                }
                values.contactemail_list = getcontactemail_list;
                dt_datatable.Dispose();

                msSQL = "select mobile_no,contact2mobileno_gid,primary_status,whatsapp_no from agr_mst_tbyronboardcontact2mobileno where " +
                        " contact_gid='" + contact_gid + "' and primary_status = 'Yes'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcontactmobileno_list = new List<contactmobileno_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcontactmobileno_list.Add(new contactmobileno_list
                        {
                            contact2mobileno_gid = (dr_datarow["contact2mobileno_gid"].ToString()),
                            mobile_no = (dr_datarow["mobile_no"].ToString()),
                            primary_status = (dr_datarow["primary_status"].ToString()),
                            whatsapp_no = (dr_datarow["whatsapp_no"].ToString()),
                        });
                    }
                }
                values.contactmobileno_list = getcontactmobileno_list;
                dt_datatable.Dispose();

                msSQL = "select contact2idproof_gid,idproof_name,idproof_no,document_name, document_path from agr_mst_tbyronboardcontact2idproof where " +
                        " contact_gid='" + contact_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcontactidproof_list = new List<contactidproof_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcontactidproof_list.Add(new contactidproof_list
                        {
                            contact2idproof_gid = (dr_datarow["contact2idproof_gid"].ToString()),
                            idproof_name = (dr_datarow["idproof_name"].ToString()),
                            idproof_no = (dr_datarow["idproof_no"].ToString()),
                            document_name = (dr_datarow["document_name"].ToString()),
                            document_path = objcmnstorage.EncryptData((dr_datarow["document_path"].ToString())),
                        });

                        values.contactidproof_list = getcontactidproof_list;
                    }
                }
                dt_datatable.Dispose();
                msSQL = " select document_title,document_name,document_path from agr_mst_tbyronboardcontact2document " +
                                " where contact_gid='" + contact_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getdocumentdtlList = new List<uploadindividualdoc_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getdocumentdtlList.Add(new uploadindividualdoc_list
                        {
                            document_title = dt["document_title"].ToString(),
                            document_path = objcmnstorage.EncryptData((dt["document_path"].ToString())),
                            document_name = dt["document_name"].ToString(),
                        });
                        values.uploadindividualdoc_list = getdocumentdtlList;
                    }
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }

        }

        public void DaGetOnboardSubmitApproval(string application_gid, result values, string user_gid)
        {
            msSQL = " select institution_gid from agr_mst_tbyronboard2institution a " +
                   " where a.application_gid = '" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msSQL = " select institution_gid from agr_mst_tbyronboard2institution a " +
                            " where institution_gid = '" + dt["institution_gid"].ToString() + "' and a.application_gid = '" + application_gid + "' and institution_status ='Incomplete'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        values.message = "Institution details having Incomplete record. Kindly, Complete it.";
                        values.status = false;
                        return;
                    }
                    else
                    {
                        values.status = true;

                    }
                    objODBCDatareader.Close();
                }
            }


            msSQL = " select contact_gid from agr_mst_tbyronboardcontact a " +
                    " where a.application_gid = '" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msSQL = " select contact_gid from agr_mst_tbyronboardcontact a " +
                            " where contact_gid = '" + dt["contact_gid"].ToString() + "' and a.application_gid = '" + application_gid + "' and contact_status  ='Incomplete'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        values.message = "Individual details having Incomplete record. Kindly, Complete it.";
                        values.status = false;
                        return;
                    }
                    else
                    {
                        values.status = true;

                    }
                    objODBCDatareader.Close();
                }
            }


            msSQL = " update agr_mst_tbyronboard set " +
                    " approval_submittedflag='Y'," +
                    " approval_submitteddate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    " where application_gid='" + application_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Submitted to Approval Successfully!";

                k = 1;

                msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows == true)
                {
                    ls_server = objODBCDataReader["pop_server"].ToString();
                    ls_port = Convert.ToInt32(objODBCDataReader["pop_port"]);
                    ls_username = objODBCDataReader["pop_username"].ToString();
                    ls_password = objODBCDataReader["pop_password"].ToString();
                }
                objODBCDataReader.Close();

                msSQL = " select  a.customerref_name, a.created_by as rm_mail, concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as created_by, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date  " +
                     " from agr_mst_tbyronboard a" +
                     " left join hrm_mst_temployee d on a.created_by = d.employee_gid" +
                         " left join adm_mst_tuser e on e.user_gid = d.user_gid " +
                       " where a.application_gid='" + application_gid + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows == true)
                {
                    lscustomerref_name = objODBCDataReader["customerref_name"].ToString();
                    lscreated_by = objODBCDataReader["created_by"].ToString();
                    lscreated_date = objODBCDataReader["created_date"].ToString();
                    lsrm_mail = objODBCDataReader["rm_mail"].ToString();

                }
                objODBCDataReader.Close();

                sub = " Buyer Onboard Application Approval ";
                body = body + "<br />";
                body = body + " &nbsp&nbsp Dear Sir/Madam,<br />";
                body = body + "<br />";
                body = body + "&nbsp&nbsp The below customer /buyer has been created by an employee. Please review and approve. Below are the details:<br />";
                body = body + "<br />";
                body = body + "&nbsp&nbsp <b>Customer/Buyer name:</b> " + HttpUtility.HtmlEncode(lscustomerref_name )+ "<br />";
                body = body + "<br />";
                body = body + "&nbsp&nbsp <b>Created by:</b> " + HttpUtility.HtmlEncode(lscreated_by)+ "<br />";
                body = body + "<br />";
                body = body + "&nbsp&nbsp <b>Created date:</b> " + lscreated_date + "<br />";
                body = body + "<br />";
                body = body + "&nbsp&nbsp <b>Pathway :</b> <br />";
                body = body + "<br />";
                body = body + "&nbsp&nbsp Login " + ConfigurationManager.AppSettings["livedomain_url"].ToString() + " > SamAgro > Buyer / Supplier Onboarding <br />";
                body = body + "<br />";
                body = body + "&nbsp&nbsp This is a system generated mail, do not reply.<br /> ";
                body = body + "<br />";

                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(ls_username);
                //message.To.Add(new MailAddress(lsto_mail));

                msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee " +
                                   " where employee_gid in ('" + lsrm_mail.Replace(",", "', '") + "')";
                cc_mailid = objdbconn.GetExecuteScalar(msSQL);


                lsBccmail_id = ConfigurationManager.AppSettings["SamagroOnboardBccMail"].ToString();

                if (lsBccmail_id != null & lsBccmail_id != string.Empty & lsBccmail_id != "")
                {
                    lsBCCReceipients = lsBccmail_id.Split(',');
                    if (lsBccmail_id.Length == 0)
                    {
                        message.Bcc.Add(new MailAddress(lsBccmail_id));
                    }
                    else
                    {
                        foreach (string BCCEmail in lsBCCReceipients)
                        {
                            message.Bcc.Add(new MailAddress(BCCEmail)); //Adding Multiple BCC email Id
                        }
                    }
                }

                lsto_mail = ConfigurationManager.AppSettings["Mail2PMG"].ToString();

                if (lsto_mail != null & lsto_mail != string.Empty & lsto_mail != "")
                {
                    lsToReceipients = lsto_mail.Split(',');
                    if (lsto_mail.Length == 0)
                    {
                        message.To.Add(new MailAddress(lsto_mail));
                    }
                    else
                    {
                        foreach (string ToEmail in lsToReceipients)
                        {
                            message.To.Add(new MailAddress(ToEmail)); //Adding Multiple To email Id
                        }
                    }
                }

                if (cc_mailid != null & cc_mailid != string.Empty & cc_mailid != "")
                {
                    lsCCReceipients = cc_mailid.Split(',');
                    if (cc_mailid.Length == 0)
                    {
                        message.CC.Add(new MailAddress(cc_mailid));
                    }
                    else
                    {
                        foreach (string CCEmail in lsCCReceipients)
                        {
                            message.CC.Add(new MailAddress(CCEmail)); //Adding Multiple CC email Id
                        }
                    }
                }

                message.Subject = sub;
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = body;
                smtp.Port = ls_port;
                smtp.Host = ls_server; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);

                values.status = true;
            }



            else
            {
                values.status = true;
                values.message = "Error Occured!";
            }
        }


        public void DaGetCompanyList(string application_gid, MdlDropDown values)
        {
            msSQL = " SELECT company_name, institution_gid from agr_mst_tbyronboard2institution where application_gid='" + application_gid + "' order by institution_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getinstitution = new List<institutionlist>();
            if (dt_datatable.Rows.Count != 0)
            {
                getinstitution.Add(new institutionlist
                {
                    institution_name = "NA",
                    institution_gid = "NA",
                });
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getinstitution.Add(new institutionlist
                    {
                        institution_name = (dr_datarow["company_name"].ToString()),
                        institution_gid = (dr_datarow["institution_gid"].ToString()),
                    });
                }
                values.institutionlist = getinstitution;
            }

            if (dt_datatable.Rows.Count == 0)
            {
                getinstitution.Add(new institutionlist
                {
                    institution_name = "NA",
                    institution_gid = "NA",
                });
                values.institutionlist = getinstitution;
            }
            dt_datatable.Dispose();

            values.status = true;
        }

        public void DaInstitutionGSTList(string institution_gid, MdlMstGST values)
        {
            msSQL = "select institution2branch_gid,gst_state,gst_no, gst_registered from agr_mst_tbyronboardinstitution2branch where institution_gid='" + institution_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstgst_list = new List<mstgst_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstgst_list.Add(new mstgst_list
                    {
                        institution2branch_gid = (dr_datarow["institution2branch_gid"].ToString()),
                        gst_state = (dr_datarow["gst_state"].ToString()),
                        gst_no = (dr_datarow["gst_no"].ToString()),
                        gst_registered = (dr_datarow["gst_registered"].ToString())
                    });
                }
                values.mstgst_list = getmstgst_list;
            }
            dt_datatable.Dispose();
        }


        public void DaGetbyrInstitutionRatingList(string institution_gid, string employee_gid, MdlRatingList values)
        {
            msSQL = " select institution2ratingdetail_gid,application_gid, institution_gid,creditrating_agencygid,creditrating_agencyname," +
                    " creditrating_gid,creditrating_name,date_format(a.assessed_on,'%d-%m-%Y') as assessed_on,creditrating_link, " +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date " +
                    " from agr_mst_tbyronboardinstitution2ratingdetail a " +
                     " where a.institution_gid = '" + institution_gid + "' order by institution2ratingdetail_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getMdlRatingdtllist = new List<MdlRatingdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getMdlRatingdtllist.Add(new MdlRatingdtl
                    {
                        institution2ratingdetail_gid = (dr_datarow["institution2ratingdetail_gid"].ToString()),
                        application_gid = (dr_datarow["application_gid"].ToString()),
                        institution_gid = (dr_datarow["institution_gid"].ToString()),
                        creditrating_agencygid = (dr_datarow["creditrating_agencygid"].ToString()),
                        creditrating_agencyname = (dr_datarow["creditrating_agencyname"].ToString()),
                        creditrating_gid = (dr_datarow["creditrating_gid"].ToString()),
                        creditrating_name = (dr_datarow["creditrating_name"].ToString()),
                        assessed_on = (dr_datarow["assessed_on"].ToString()),
                        creditrating_link = (dr_datarow["creditrating_link"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                    });
                }
                values.MdlRatingdtl = getMdlRatingdtllist;
            }
            dt_datatable.Dispose();
        }

        public void DaGetbyrInstitutionBankAccDtl(string institution_gid, string employee_gid, MdlInstitution2BankAcc values)
        {
            msSQL = " select institution2bankdtl_gid,institution_gid,bank_name,branch_name,ifsc_code,bankaccount_number, primary_status," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                    " date_format(a.updated_date, '%d-%m-%Y %h:%i %p') as updated_date, " +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, " +
                    " concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as updated_by " +
                    " from agr_mst_tbyronboardinstitution2bankdtl a " +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " left join hrm_mst_temployee d on a.updated_by = d.employee_gid " +
                    " left join adm_mst_tuser e on e.user_gid = d.user_gid " +
                    " where institution_gid='" + institution_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcreditbankacc_list = new List<institution2bankacc_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getcreditbankacc_list.Add(new institution2bankacc_list
                    {
                        bank_name = dt["bank_name"].ToString(),
                        branch_name = dt["branch_name"].ToString(),
                        ifsc_code = dt["ifsc_code"].ToString(),
                        primary_status = dt["primary_status"].ToString(),
                        bankaccount_number = dt["bankaccount_number"].ToString(),
                        institution2bankdtl_gid = dt["institution2bankdtl_gid"].ToString(),
                        institution_gid = dt["institution_gid"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        updated_date = dt["updated_date"].ToString(),
                        updated_by = dt["updated_by"].ToString(),
                    });
                    values.institution2bankacc_list = getcreditbankacc_list;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetbyrIndividualBankAccDtl(string contact_gid, string employee_gid, Mdlindividual2bankacc values)
        {
            msSQL = " select contact2bankdtl_gid,contact_gid,bank_name,branch_name,ifsc_code,bankaccount_number, primary_status," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                    " date_format(a.updated_date, '%d-%m-%Y %h:%i %p') as updated_date, " +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, " +
                    " concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as updated_by " +
                    " from agr_mst_tbyronboardcontact2bankdtl a " +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " left join hrm_mst_temployee d on a.updated_by = d.employee_gid " +
                    " left join adm_mst_tuser e on e.user_gid = d.user_gid " +
                    " where contact_gid='" + contact_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcreditbankacc_list = new List<individual2bankacc_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getcreditbankacc_list.Add(new individual2bankacc_list
                    {
                        bank_name = dt["bank_name"].ToString(),
                        branch_name = dt["branch_name"].ToString(),
                        primary_status = dt["primary_status"].ToString(),
                        ifsc_code = dt["ifsc_code"].ToString(),
                        bankaccount_number = dt["bankaccount_number"].ToString(),
                        contact2bankdtl_gid = dt["contact2bankdtl_gid"].ToString(),
                        contact_gid = dt["contact_gid"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        updated_date = dt["updated_date"].ToString(),
                        updated_by = dt["updated_by"].ToString(),
                    });
                    values.individual2bankacc_list = getcreditbankacc_list;
                }
            }
            dt_datatable.Dispose();
        }


        public void DaGetbyrPANForm60List(string contact_gid, MdlContactPANForm60 values)
        {
            msSQL = "select contact2panform60_gid,document_name, document_path from agr_mst_tbyronboardcontact2panform60 where " +
              " contact_gid='" + contact_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcontactpanform60_list = new List<contactpanform60_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcontactpanform60_list.Add(new contactpanform60_list
                    {
                        contact2panform60_gid = (dr_datarow["contact2panform60_gid"].ToString()),
                        document_name = (dr_datarow["document_name"].ToString()),
                        document_path = objcmnstorage.EncryptData((dr_datarow["document_path"].ToString())),
                    });

                    values.contactpanform60_list = getcontactpanform60_list;
                }
                dt_datatable.Dispose();
            }

        }

        public void DaEditGetCreditBankAccDtl(string institution2bankdtl_gid, MdlInstitution2BankAcc values)
        {
            try
            {
                msSQL = "select institution2bankdtl_gid,institution_gid,application_gid,bank_name,branch_name,bank_address,micr_code,ifsc_code,bankaccount_name," +
                " bankaccounttype_gid,bankaccounttype_name,bankaccount_number,confirmbankaccountnumber,joinaccount_status,joinaccount_name," +
                " chequebook_status,accountopen_date from agr_mst_tbyronboardinstitution2bankdtl where institution2bankdtl_gid='" + institution2bankdtl_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.institution2bankdtl_gid = objODBCDatareader["institution2bankdtl_gid"].ToString();
                    values.institution_gid = objODBCDatareader["institution_gid"].ToString();
                    values.application_gid = objODBCDatareader["application_gid"].ToString();
                    values.bank_name = objODBCDatareader["bank_name"].ToString();
                    values.branch_name = objODBCDatareader["branch_name"].ToString();
                    values.bank_address = objODBCDatareader["bank_address"].ToString();
                    values.micr_code = objODBCDatareader["micr_code"].ToString();
                    values.ifsc_code = objODBCDatareader["ifsc_code"].ToString();
                    values.bankaccount_name = objODBCDatareader["bankaccount_name"].ToString();
                    values.chequebook_status = objODBCDatareader["chequebook_status"].ToString();
                    values.bankaccounttype_gid = objODBCDatareader["bankaccounttype_gid"].ToString();
                    values.bankaccounttype_name = objODBCDatareader["bankaccounttype_name"].ToString();
                    values.bankaccount_number = objODBCDatareader["bankaccount_number"].ToString();
                    values.confirmbankaccountnumber = objODBCDatareader["confirmbankaccountnumber"].ToString();
                    values.joint_account = objODBCDatareader["joinaccount_status"].ToString();
                    values.jointaccountholder_name = objODBCDatareader["joinaccount_name"].ToString();
                    values.chequebook_status = objODBCDatareader["chequebook_status"].ToString();

                    if (objODBCDatareader["accountopen_date"].ToString() == "")
                    {
                    }
                    else
                    {
                        values.accountopen_date = Convert.ToDateTime(objODBCDatareader["accountopen_date"]).ToString("MM-dd-yyyy");
                    }

                }
                values.status = true;


                dt_datatable.Dispose();
                values.message = "success";
                objODBCDatareader.Close();
            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }
        }

        public void DaDeleteinstitution(string institution_gid, string application_gid, string employee_gid, MdlCICInstitution values)
        {
            msSQL = " Delete from agr_mst_tbyronboard2institution where institution_gid='" + institution_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msSQL = " select institution_gid,company_name,date_incorporation,stakeholder_type,institution_status," +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date" +
                        " from agr_mst_tbyronboard2institution a " +
                        " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                        " left join adm_mst_tuser c on c.user_gid=b.user_gid" +
                        " where a.application_gid='" + application_gid + "' order by institution_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcicinstitutionList = new List<cicinstitution_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getcicinstitutionList.Add(new cicinstitution_list
                        {
                            institution_gid = dt["institution_gid"].ToString(),
                            company_name = dt["company_name"].ToString(),
                            date_incorporation = dt["date_incorporation"].ToString(),
                            stakeholder_type = dt["stakeholder_type"].ToString(),
                            created_date = dt["created_date"].ToString(),
                            created_by = dt["created_by"].ToString(),
                            institution_status = dt["institution_status"].ToString(),
                        });

                    }
                }
                values.cicinstitution_list = getcicinstitutionList;
                dt_datatable.Dispose();
                values.message = "Company Information Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured while Deleting";
                values.status = false;
            }

        }

        public void DaDeleteindividual(string contact_gid, string application_gid, string employee_gid, MdlCICIndividual values)
        {
            msSQL = "Delete from agr_mst_tbyronboardcontact where contact_gid='" + contact_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                msSQL = " select contact_gid,concat(first_name, ' ',middle_name,' ',last_name) as individual_name," +
                   " a.pan_no,aadhar_no,stakeholder_type,contact_status," +
                   " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date" +
                   " from agr_mst_tbyronboardcontact a " +
                   " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                   " left join adm_mst_tuser c on c.user_gid=b.user_gid" +
                   " where a.application_gid='" + application_gid + "' order by contact_gid desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcicindividualList = new List<cicindividual_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getcicindividualList.Add(new cicindividual_list
                        {
                            contact_gid = dt["contact_gid"].ToString(),
                            individual_name = dt["individual_name"].ToString(),
                            pan_no = dt["pan_no"].ToString(),
                            aadhar_no = dt["aadhar_no"].ToString(),
                            stakeholder_type = dt["stakeholder_type"].ToString(),
                            created_date = dt["created_date"].ToString(),
                            created_by = dt["created_by"].ToString(),
                            contact_status = dt["contact_status"].ToString(),
                        });

                    }
                }
                values.cicindividual_list = getcicindividualList;
                dt_datatable.Dispose();
                values.message = "Individual Information Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured while Deleting";
                values.status = false;
            }

        }

        public void DaPostOnboardTranferRM(string employee_gid, MdlRmtransferdtl values)
        {
            if (values.transfer_remarks == null)
                values.transfer_remarks = "";
            msSQL = " select created_by, createdby_name from agr_mst_tbyronboard where application_gid='" + values.onboard_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.transferfrom_employeegid = objODBCDatareader["created_by"].ToString();
                values.transferfrom_employeename = objODBCDatareader["createdby_name"].ToString();
            }
            objODBCDatareader.Close();


            msSQL = " insert into agr_mst_tonboardrmtransferlog(" +
                    " onboard_gid," +
                    " transferfrom_employeegid," +
                    " transferfrom_employeename," +
                    " transferto_employeegid," +
                    " transferto_employeename, " +
                    " transfer_remarks, " +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + values.onboard_gid + "'," +
                    "'" + values.transferfrom_employeegid + "'," +
                    "'" + values.transferfrom_employeename + "'," +
                    "'" + values.transferto_employeegid + "'," +
                    "'" + values.transferto_employeename + "'," +
                    "'" + values.transfer_remarks.Replace("'", "") + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult == 1)
            {
                msSQL = " update agr_mst_tbyronboard set " +
                        " created_by= '" + values.transferto_employeegid + "'," +
                        " createdby_name='" + values.transferto_employeename + "'" +
                        " where application_gid='" + values.onboard_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update agr_mst_tbyronboardcontact set " +
                        " created_by= '" + values.transferto_employeegid + "' " +
                        " where application_gid='" + values.onboard_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update agr_mst_tbyronboard2institution set " +
                        " created_by= '" + values.transferto_employeegid + "' " +
                        " where application_gid='" + values.onboard_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            if (mnResult != 0)
            {
                if (ConfigurationManager.AppSettings["sysSamagroHyperbrdigeAPIEnable"].ToString() == "Yes")
                {
                    HttpContext ctx = HttpContext.Current;

                    Thread t = new Thread(new ThreadStart(() =>
                    {
                        HttpContext.Current = ctx;

                        //Update Tag Status

                        objFnSamAgroHBAPIConnEdit.UpdateBuyerRMHBAPI(values.onboard_gid);

                    }));

                    t.Start();
                }


                values.status = true;
                values.message = " Onboard Transferred to " + values.transferto_employeename + " Successfully!";
            }

            else
            {
                values.status = false;
                values.message = "Error Occured!";
            }
        }

        public void DaGetOnboardTranferRMLog(string onboard_gid, MdlRmtransferlist values)
        {
            msSQL = " select transferfrom_employeename,transferto_employeename,a.transfer_remarks," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date " +
                    " from agr_mst_tonboardrmtransferlog a " +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid" +
                    " where a.onboard_gid='" + onboard_gid + "' order by onboardrmtransferlog_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcicinstitutionList = new List<MdlRmtransferdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getcicinstitutionList.Add(new MdlRmtransferdtl
                    {
                        transferfrom_employeename = dt["transferfrom_employeename"].ToString(),
                        transferto_employeename = dt["transferto_employeename"].ToString(),
                        transfer_remarks = dt["transfer_remarks"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                    });

                }
                values.MdlRmtransferdtl = getcicinstitutionList;
            }
            dt_datatable.Dispose();
            values.status = true;
        }

        //public void DaGetOnboardValidatePANAadhar(MdlonboardValidatedtl values)
        //{
        //    string lsapplication_gid = "", lspan = "", lsaadhar = "";

        //    if (values.onboard_gid != "" || values.onboard_gid != null)
        //    {
        //        msSQL = " select created_by from agr_mst_tbyronboard2institution where ";
        //        if (values.stakeholder_type == "Applicant")
        //        {
        //            msSQL += " (stakeholder_type='Applicant' and companypan_no='" + values.pan_no + "'" +
        //                     " and institution_gid !='" + values.institution_gid + "')";
        //        }
        //        else
        //        {
        //            msSQL += " stakeholder_type in ('Guarantor','Member','Applicant') and companypan_no='" + values.pan_no + "'" +
        //                     " and application_gid ='" + values.onboard_gid + "'";
        //            if (values.institution_gid != null)
        //                msSQL += " and institution_gid !='" + values.institution_gid + "'";

        //        }
        //        lspan = objdbconn.GetExecuteScalar(msSQL);
        //        if (lspan != "")
        //            values.panoraadhar = "PAN";
        //        if (lsapplication_gid == "")
        //        {
        //            if (lspan == "") { 
        //                msSQL = " select created_by from agr_mst_tbyronboardcontact where ";
        //            if (values.stakeholder_type == "Applicant")
        //            {
        //                msSQL += " (stakeholder_type='Applicant' and pan_no ='" + values.pan_no + "' " +
        //                         " and contact_gid !='" + values.contact_gid + "')";
        //            }
        //            else
        //            {
        //                msSQL += " stakeholder_type in ('Guarantor','Member','Applicant') and pan_no ='" + values.pan_no + "' " +
        //                         " and application_gid ='" + values.onboard_gid + "'";
        //                if (values.contact_gid != null)
        //                    msSQL += " and contact_gid != '" + values.contact_gid + "'";
        //            }
        //            lspan = objdbconn.GetExecuteScalar(msSQL);

        //        }
        //        }

        //        if (values.aadhar_no != "" && values.aadhar_no != null)
        //        {
        //            msSQL = " select application_gid from agr_mst_tbyronboardcontact where ";
        //            if (values.stakeholder_type == "Applicant")
        //            {
        //                msSQL += " (stakeholder_type='Applicant' and aadhar_no ='" + values.aadhar_no + "' " +
        //                         " and contact_gid !='" + values.contact_gid + "')";
        //            }
        //            else
        //            {
        //                msSQL += " stakeholder_type in ('Guarantor','Member','Applicant') and aadhar_no ='" + values.aadhar_no + "'" +
        //                         " and application_gid ='" + values.onboard_gid + "'";
        //                if (values.contact_gid != null)
        //                    msSQL += " and contact_gid !='" + values.contact_gid + "'";
        //            }
        //            lsaadhar = objdbconn.GetExecuteScalar(msSQL);
        //        }
        //        msSQL = " select concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as employee_name " +
        //       " from hrm_mst_temployee a" +
        //       " left join adm_mst_tuser b on a.user_gid=b.user_gid" +
        //       " where a.employee_gid='" + lspan + "'";
        //        values.lscreatedby_name = objdbconn.GetExecuteScalar(msSQL);
        //        if (lspan != "" && lsaadhar == "")
        //            values.panoraadhar = "PAN";
        //        else if (lsaadhar != "" && lspan == "")
        //            values.panoraadhar = "Aadhar";
        //        else if (lspan != "" && lsaadhar != "")
        //            values.panoraadhar = "Both";
        //    }
        //    values.status = true;
        //}


        public void DaGetOnboardValidatePANAadhar(MdlonboardValidatedtl values)
        {
            string lsapplication_gid = "", lspan = "", lsaadhar = "";

            //string lsrejectcount, lspancount, lstotalpancount;

            msSQL = " select (select count(b.application_gid) from agr_mst_tbyronboard b " +
                            " left join agr_mst_tbyronboard2institution a on b.application_gid = a.application_gid " +
                            " where companypan_no ='" + values.pan_no + "' and stakeholder_type='Applicant' and (b.onboard_approvalstatus = 'R' ) ) + " +
                            " (select count(b.application_gid) from agr_mst_tbyronboard a " +
                            " left join agr_mst_tbyronboardcontact b on b.application_gid = a.application_gid " +
                           " where (pan_no ='" + values.pan_no + "' ) and stakeholder_type='Applicant' " +
                           " and (a.onboard_approvalstatus = 'R' )) ";

            values.lsrejectcount = (objdbconn.GetExecuteScalar(msSQL));

            if (Convert.ToInt32(values.lsrejectcount) == 1)
            {

                msSQL = " select (select count(b.application_gid) from agr_mst_tbyronboard b " +
                          " left join agr_mst_tbyronboard2institution a on b.application_gid = a.application_gid " +
                          " where companypan_no ='" + values.pan_no + "' and stakeholder_type='Applicant' and (b.onboard_approvalstatus =  'N' or b.onboard_approvalstatus = 'Y' ) ) + " +
                          " (select count(b.application_gid) from agr_mst_tbyronboard a " +
                          " left join agr_mst_tbyronboardcontact b on b.application_gid = a.application_gid " +
                          " where (pan_no ='" + values.pan_no + "' ) and stakeholder_type='Applicant' and (a.onboard_approvalstatus =  'N' or a.onboard_approvalstatus = 'Y' ) ) ";

                values.lstotalpancount = (objdbconn.GetExecuteScalar(msSQL));

                if (Convert.ToInt32(values.lstotalpancount) == 0)
                {

                    msSQL = " select (select count(b.application_gid) from agr_mst_tbyronboard b " +
                            " left join agr_mst_tbyronboard2institution a on b.application_gid = a.application_gid " +
                            " where companypan_no ='" + values.pan_no + "' and stakeholder_type='Applicant' and a.application_gid ='" + values.onboard_gid + "' ) + " +
                            " (select count(a.application_gid) from agr_mst_tbyronboard a " +
                            " left join agr_mst_tbyronboardcontact b on b.application_gid = a.application_gid " +
                            " where (pan_no ='" + values.pan_no + "' ) and stakeholder_type='Applicant' and b.application_gid ='" + values.onboard_gid + "') ";


                    values.lspancount = (objdbconn.GetExecuteScalar(msSQL));

                    if ((Convert.ToInt32(values.lspancount)) == 0 || (Convert.ToInt32(values.lspancount) == 1))
                    {

                        values.status = true;
                        return;
                    }

                    else
                    {

                        if (!string.IsNullOrEmpty(values.institution_gid))
                        {
                            msSQL = " select  institution_gid from  agr_mst_tbyronboard2institution where companypan_no ='" + values.pan_no + "' and stakeholder_type='Applicant' and  application_gid ='" + values.onboard_gid + "' and institution_gid ='" + values.institution_gid + "'";
                            string lsinstitution_gid = objdbconn.GetExecuteScalar(msSQL);

                            if (lsinstitution_gid == values.institution_gid)
                            {

                                values.status = true;
                                return;

                            }

                        }

                        else if (!string.IsNullOrEmpty(values.contact_gid))
                        {

                            msSQL = " select  contact_gid from  agr_mst_tbyronboardcontact where pan_no ='" + values.pan_no + "' and stakeholder_type='Applicant' and  application_gid ='" + values.onboard_gid + "'and contact_gid = '" + values.contact_gid + "' ";
                            string lscontact_gid = objdbconn.GetExecuteScalar(msSQL);

                            if (lscontact_gid == values.contact_gid)
                            {

                                values.status = true;
                                return;

                            }

                        }

                    }

                }

                else if (values.onboard_gid != "" || values.onboard_gid != null)
                {
                    msSQL = " select created_by from agr_mst_tbyronboard2institution where ";
                    if (values.stakeholder_type == "Applicant")
                    {
                        msSQL += " (stakeholder_type='Applicant' and companypan_no='" + values.pan_no + "'" +
                                 " and institution_gid !='" + values.institution_gid + "')";
                    }
                    else
                    {
                        msSQL += " stakeholder_type in ('Guarantor','Member','Applicant') and companypan_no='" + values.pan_no + "'" +
                                 " and application_gid ='" + values.onboard_gid + "'";
                        if (values.institution_gid != null)
                            msSQL += " and institution_gid !='" + values.institution_gid + "'";

                    }
                    lspan = objdbconn.GetExecuteScalar(msSQL);
                    if (lspan != "")
                        values.panoraadhar = "PAN";
                    if (lsapplication_gid == "")
                    {
                        if (lspan == "")
                        {
                            msSQL = " select created_by from agr_mst_tbyronboardcontact where ";
                            if (values.stakeholder_type == "Applicant")
                            {
                                msSQL += " (stakeholder_type='Applicant' and pan_no ='" + values.pan_no + "' " +
                                         " and contact_gid !='" + values.contact_gid + "')";
                            }
                            else
                            {
                                msSQL += " stakeholder_type in ('Guarantor','Member','Applicant') and pan_no ='" + values.pan_no + "' " +
                                         " and application_gid ='" + values.onboard_gid + "'";
                                if (values.contact_gid != null)
                                    msSQL += " and contact_gid != '" + values.contact_gid + "'";
                            }
                            lspan = objdbconn.GetExecuteScalar(msSQL);

                        }
                    }

                    if (values.aadhar_no != "" && values.aadhar_no != null)
                    {
                        msSQL = " select application_gid from agr_mst_tbyronboardcontact where ";
                        if (values.stakeholder_type == "Applicant")
                        {
                            msSQL += " (stakeholder_type='Applicant' and aadhar_no ='" + values.aadhar_no + "' " +
                                     " and contact_gid !='" + values.contact_gid + "')";
                        }
                        else
                        {
                            msSQL += " stakeholder_type in ('Guarantor','Member','Applicant') and aadhar_no ='" + values.aadhar_no + "'" +
                                     " and application_gid ='" + values.onboard_gid + "'";
                            if (values.contact_gid != null)
                                msSQL += " and contact_gid !='" + values.contact_gid + "'";
                        }
                        lsaadhar = objdbconn.GetExecuteScalar(msSQL);
                    }
                    msSQL = " select concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as employee_name " +
                   " from hrm_mst_temployee a" +
                   " left join adm_mst_tuser b on a.user_gid=b.user_gid" +
                   " where a.employee_gid='" + lspan + "'";
                    values.lscreatedby_name = objdbconn.GetExecuteScalar(msSQL);
                    if (lspan != "" && lsaadhar == "")
                        values.panoraadhar = "PAN";
                    else if (lsaadhar != "" && lspan == "")
                        values.panoraadhar = "Aadhar";
                    else if (lspan != "" && lsaadhar != "")
                        values.panoraadhar = "Both";
                }
            }

            else if (values.onboard_gid != "" || values.onboard_gid != null)
            {

                msSQL = " select (select count(b.application_gid) from agr_mst_tbyronboard b " +
                           " left join agr_mst_tbyronboard2institution a on b.application_gid = a.application_gid " +
                           " where companypan_no ='" + values.pan_no + "' and stakeholder_type='Applicant' and a.application_gid ='" + values.onboard_gid + "' ) + " +
                           " (select count(a.application_gid) from agr_mst_tbyronboard a " +
                           " left join agr_mst_tbyronboardcontact b on b.application_gid = a.application_gid " +
                           " where (pan_no ='" + values.pan_no + "' ) and stakeholder_type='Applicant' and b.application_gid ='" + values.onboard_gid + "') ";


                values.lspancount = (objdbconn.GetExecuteScalar(msSQL));

                if ((Convert.ToInt32(values.lspancount) == 1))
                {

                    msSQL = " select (select count(b.application_gid) from agr_mst_tbyronboard b " +
                       " left join agr_mst_tbyronboard2institution a on b.application_gid = a.application_gid " +
                       " where companypan_no ='" + values.pan_no + "' and stakeholder_type in ('Guarantor','Member') and a.application_gid ='" + values.onboard_gid + "' ) + " +
                       " (select count(a.application_gid) from agr_mst_tbyronboard a " +
                       " left join agr_mst_tbyronboardcontact b on b.application_gid = a.application_gid " +
                       " where (pan_no ='" + values.pan_no + "' ) and stakeholder_type in ('Guarantor','Member') and b.application_gid ='" + values.onboard_gid + "') ";

                    values.lsnonapplicantpancount = (objdbconn.GetExecuteScalar(msSQL));

                    if ((Convert.ToInt32(values.lsnonapplicantpancount)) == 0)
                    {

                        if (!string.IsNullOrEmpty(values.institution_gid))
                        {
                            msSQL = " select  institution_gid from  agr_mst_tbyronboard2institution where companypan_no ='" + values.pan_no + "' and stakeholder_type in ('Guarantor','Member') and  application_gid ='" + values.onboard_gid + "' and institution_gid ='" + values.institution_gid + "'";
                            string lsinstitution_gid = objdbconn.GetExecuteScalar(msSQL);

                            if (lsinstitution_gid == values.institution_gid)
                            {

                                values.status = true;
                                return;

                            }

                        }

                        else if (!string.IsNullOrEmpty(values.contact_gid))
                        {

                            msSQL = " select  contact_gid from  agr_mst_tbyronboardcontact where pan_no ='" + values.pan_no + "' and stakeholder_type in ('Guarantor','Member') and  application_gid ='" + values.onboard_gid + "'and contact_gid = '" + values.contact_gid + "' ";
                            string lscontact_gid = objdbconn.GetExecuteScalar(msSQL);

                            if (lscontact_gid == values.contact_gid)
                            {

                                values.status = true;
                                return;

                            }

                        }

                        else
                        {
                            values.status = true;
                            return;

                        }
                    }


                    else
                    {

                        if (!string.IsNullOrEmpty(values.institution_gid))
                        {
                            msSQL = " select  institution_gid from  agr_mst_tbyronboard2institution where companypan_no ='" + values.pan_no + "' and stakeholder_type in ('Guarantor','Member') and  application_gid ='" + values.onboard_gid + "' and institution_gid ='" + values.institution_gid + "'";
                            string lsinstitution_gid = objdbconn.GetExecuteScalar(msSQL);

                            if (lsinstitution_gid == values.institution_gid)
                            {

                                values.status = true;
                                return;

                            }

                        }

                        else if (!string.IsNullOrEmpty(values.contact_gid))
                        {

                            msSQL = " select  contact_gid from  agr_mst_tbyronboardcontact where pan_no ='" + values.pan_no + "' and stakeholder_type in ('Guarantor','Member') and  application_gid ='" + values.onboard_gid + "'and contact_gid = '" + values.contact_gid + "' ";
                            string lscontact_gid = objdbconn.GetExecuteScalar(msSQL);

                            if (lscontact_gid == values.contact_gid)
                            {

                                values.status = true;
                                return;

                            }

                        }


                        msSQL = " select created_by from agr_mst_tbyronboard2institution where ";
                        if (values.stakeholder_type == "Applicant")
                        {
                            msSQL += " (stakeholder_type='Applicant' and companypan_no='" + values.pan_no + "'" +
                                     " and institution_gid !='" + values.institution_gid + "')";
                        }
                        else
                        {
                            msSQL += " stakeholder_type in ('Guarantor','Member','Applicant') and companypan_no='" + values.pan_no + "'" +
                                     " and application_gid ='" + values.onboard_gid + "'";
                            if (values.institution_gid != null)
                                msSQL += " and institution_gid !='" + values.institution_gid + "'";

                        }
                        lspan = objdbconn.GetExecuteScalar(msSQL);
                        if (lspan != "")
                            values.panoraadhar = "PAN";
                        if (lsapplication_gid == "")
                        {
                            if (lspan == "")
                            {
                                msSQL = " select created_by from agr_mst_tbyronboardcontact where ";
                                if (values.stakeholder_type == "Applicant")
                                {
                                    msSQL += " (stakeholder_type='Applicant' and pan_no ='" + values.pan_no + "' " +
                                             " and contact_gid !='" + values.contact_gid + "')";
                                }
                                else
                                {
                                    msSQL += " stakeholder_type in ('Guarantor','Member','Applicant') and pan_no ='" + values.pan_no + "' " +
                                             " and application_gid ='" + values.onboard_gid + "'";
                                    if (values.contact_gid != null)
                                        msSQL += " and contact_gid != '" + values.contact_gid + "'";
                                }
                                lspan = objdbconn.GetExecuteScalar(msSQL);

                            }
                        }

                        if (values.aadhar_no != "" && values.aadhar_no != null)
                        {
                            msSQL = " select application_gid from agr_mst_tbyronboardcontact where ";
                            if (values.stakeholder_type == "Applicant")
                            {
                                msSQL += " (stakeholder_type='Applicant' and aadhar_no ='" + values.aadhar_no + "' " +
                                         " and contact_gid !='" + values.contact_gid + "')";
                            }
                            else
                            {
                                msSQL += " stakeholder_type in ('Guarantor','Member','Applicant') and aadhar_no ='" + values.aadhar_no + "'" +
                                         " and application_gid ='" + values.onboard_gid + "'";
                                if (values.contact_gid != null)
                                    msSQL += " and contact_gid !='" + values.contact_gid + "'";
                            }
                            lsaadhar = objdbconn.GetExecuteScalar(msSQL);
                        }
                        msSQL = " select concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as employee_name " +
                       " from hrm_mst_temployee a" +
                       " left join adm_mst_tuser b on a.user_gid=b.user_gid" +
                       " where a.employee_gid='" + lspan + "'";
                        values.lscreatedby_name = objdbconn.GetExecuteScalar(msSQL);
                        if (lspan != "" && lsaadhar == "")
                            values.panoraadhar = "PAN";
                        else if (lsaadhar != "" && lspan == "")
                            values.panoraadhar = "Aadhar";
                        else if (lspan != "" && lsaadhar != "")
                            values.panoraadhar = "Both";
                    }
                }

                else
                {

                    msSQL = " select created_by from agr_mst_tbyronboard2institution where ";
                    if (values.stakeholder_type == "Applicant")
                    {
                        msSQL += " (stakeholder_type='Applicant' and companypan_no='" + values.pan_no + "'" +
                                 " and institution_gid !='" + values.institution_gid + "')";
                    }
                    else
                    {
                        msSQL += " stakeholder_type in ('Guarantor','Member','Applicant') and companypan_no='" + values.pan_no + "'" +
                                 " and application_gid ='" + values.onboard_gid + "'";
                        if (values.institution_gid != null)
                            msSQL += " and institution_gid !='" + values.institution_gid + "'";

                    }
                    lspan = objdbconn.GetExecuteScalar(msSQL);
                    if (lspan != "")
                        values.panoraadhar = "PAN";
                    if (lsapplication_gid == "")
                    {
                        if (lspan == "")
                        {
                            msSQL = " select created_by from agr_mst_tbyronboardcontact where ";
                            if (values.stakeholder_type == "Applicant")
                            {
                                msSQL += " (stakeholder_type='Applicant' and pan_no ='" + values.pan_no + "' " +
                                         " and contact_gid !='" + values.contact_gid + "')";
                            }
                            else
                            {
                                msSQL += " stakeholder_type in ('Guarantor','Member','Applicant') and pan_no ='" + values.pan_no + "' " +
                                         " and application_gid ='" + values.onboard_gid + "'";
                                if (values.contact_gid != null)
                                    msSQL += " and contact_gid != '" + values.contact_gid + "'";
                            }
                            lspan = objdbconn.GetExecuteScalar(msSQL);

                        }
                    }

                    if (values.aadhar_no != "" && values.aadhar_no != null)
                    {
                        msSQL = " select application_gid from agr_mst_tbyronboardcontact where ";
                        if (values.stakeholder_type == "Applicant")
                        {
                            msSQL += " (stakeholder_type='Applicant' and aadhar_no ='" + values.aadhar_no + "' " +
                                     " and contact_gid !='" + values.contact_gid + "')";
                        }
                        else
                        {
                            msSQL += " stakeholder_type in ('Guarantor','Member','Applicant') and aadhar_no ='" + values.aadhar_no + "'" +
                                     " and application_gid ='" + values.onboard_gid + "'";
                            if (values.contact_gid != null)
                                msSQL += " and contact_gid !='" + values.contact_gid + "'";
                        }
                        lsaadhar = objdbconn.GetExecuteScalar(msSQL);
                    }
                    msSQL = " select concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as employee_name " +
                   " from hrm_mst_temployee a" +
                   " left join adm_mst_tuser b on a.user_gid=b.user_gid" +
                   " where a.employee_gid='" + lspan + "'";
                    values.lscreatedby_name = objdbconn.GetExecuteScalar(msSQL);
                    if (lspan != "" && lsaadhar == "")
                        values.panoraadhar = "PAN";
                    else if (lsaadhar != "" && lspan == "")
                        values.panoraadhar = "Aadhar";
                    else if (lspan != "" && lsaadhar != "")
                        values.panoraadhar = "Both";
                }

            }
            values.status = true;
        }


        public void DaGetonboardInitiateDetail(string buyeronboard_gid, MdlonboardInitiateDetail values)
        {
            msSQL = " select application_no,virtualaccount_number,customerref_name from agr_mst_tbyronboard " +
                    " where application_gid='" + buyeronboard_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.buyer_id = objODBCDatareader["application_no"].ToString();
                values.virtualaccount_number = objODBCDatareader["virtualaccount_number"].ToString();
                values.buyer_name = objODBCDatareader["customerref_name"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " select application_no,approval_status, a.product_name, a.program_name,initiated_remarks,a.onboarding_status, " +
                    " created_byname,date_format(a.created_date, '%d-%m-%Y') as initiated_date " +
                    " from agr_mst_tonboardinitiatedtl a " +
                    " left join agr_mst_tapplication b on a.application_gid = b.application_gid " +
                    " where a.buyeronboard_gid ='" + buyeronboard_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getonboardapplication_list = new List<onboardapplicationList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getonboardapplication_list.Add(new onboardapplicationList
                    {
                        application_no = (dr_datarow["application_no"].ToString()),
                        approval_status = (dr_datarow["approval_status"].ToString()),
                        product_name = (dr_datarow["product_name"].ToString()),
                        program_name = (dr_datarow["program_name"].ToString()),
                        initiated_remarks = (dr_datarow["initiated_remarks"].ToString()),
                        created_byname = (dr_datarow["created_byname"].ToString()),
                        initiated_date = (dr_datarow["initiated_date"].ToString()),
                        onboarding_status = (dr_datarow["onboarding_status"].ToString()),
                    });
                }
                values.onboardapplicationList = getonboardapplication_list;
            }
            dt_datatable.Dispose();
            //Loan Product
            msSQL = " SELECT loanproduct_gid,loanproduct_name FROM agr_mst_tloanproduct a" +
                       " where status='Y' order by a.loanproduct_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getloanproduct_list = new List<loanproductlist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getloanproduct_list.Add(new loanproductlist
                    {
                        loanproduct_gid = (dr_datarow["loanproduct_gid"].ToString()),
                        loanproduct_name = (dr_datarow["loanproduct_name"].ToString()),
                    });
                }
                values.loanproductlist = getloanproduct_list;
            }
            dt_datatable.Dispose();
            values.status = true;
        }

        public void DaGetValidateProductProgram(string loanproduct_gid, string buyeronboard_gid, MdlMstApplication360 values)
        {
            msSQL = " select products_gid from agr_mst_tproductdeskmapping where products_gid = '" + loanproduct_gid + "'";
            lsloanproduct_gid = objdbconn.GetExecuteScalar(msSQL);

            if (string.IsNullOrEmpty(lsloanproduct_gid))
            {

                values.status = false;
                values.message = "Kindly map the chosen product to the Product Desk Mapping Master and proceed to initiate";

            }

            else
            {

                msSQL = "  SELECT  loansubproduct_gid,loansubproduct_name  " +
                        " FROM agr_mst_tloansubproduct a  where  a.status = 'Y' " +
                        " and loanproduct_gid = '" + loanproduct_gid + "' and ( a.loansubproduct_gid not in (select a.program_gid from agr_mst_tonboardinitiatedtl a " +
                        "  left join agr_mst_tapplication b on a.application_gid = b.application_gid " +
                        " where a.buyeronboard_gid = '" + buyeronboard_gid + "' " +
                        " and ((b.process_type is null) or (b.process_type='Accept' and a.onboarding_status='Proposal'))) " +
                        " or a.loansubproduct_gid  in (select a.program_gid from agr_mst_tonboardinitiatedtl a " +
                        " left join agr_mst_tapplication b on a.application_gid = b.application_gid " +
                        " where a.buyeronboard_gid = '" + buyeronboard_gid + "'  and (approval_status in ('Rejected By Business', 'Rejected by Credit Manager', " +
                        "'CC Rejected','Rejected By Credit','Product Approval - Rejected','Pmg Advance Rejected') or close_flag='Y' )))" +
                        " order by loansubproduct_name asc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getapplication_list = new List<application_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getapplication_list.Add(new application_list
                        {
                            loansubproduct_gid = (dr_datarow["loansubproduct_gid"].ToString()),
                            loansubproduct_name = (dr_datarow["loansubproduct_name"].ToString()),
                        });
                    }
                    values.application_list = getapplication_list;
                }
                dt_datatable.Dispose();
                values.status = true;

            }
            //string lsalreadyadded = "";
            //msSQL = " select a.product_gid from agr_mst_tonboardinitiatedtl a " +
            //        " left join agr_mst_tapplication b on a.application_gid = b.application_gid " +
            //        " where b.process_type is null and a.product_gid = '" + product_gid + "' and a.program_gid = '" + program_gid + "'";
            //lsalreadyadded = objdbconn.GetExecuteScalar(msSQL);
            //if (lsalreadyadded != "")
            //    values.status = true;
            //else
            //    values.status = false;
        }

        public bool DaPostIndividualBank(string employee_gid, MdlBuyerOnboardIndividual2BankAcc values)
        {

            msSQL = "select primary_status from agr_mst_tbyronboardcontact2bankdtl where primary_status='Yes' and (contact_gid='" + employee_gid + "' or contact_gid='" + values.contact_gid + "')";
            string lsprimary_status = objdbconn.GetExecuteScalar(msSQL);
            if (lsprimary_status == (values.primary_status))
            {
                values.status = false;
                values.message = "Already Primary Bank Account Number Added";
                return false;
            }

            msGetGid = objcmnfunctions.GetMasterGID("I2BD");
            msSQL = " insert into agr_mst_tbyronboardcontact2bankdtl(" +
                    " contact2bankdtl_gid," +
                    " contact_gid," +
                    //" application_gid," +
                    " bank_name," +
                    " branch_name," +
                    " bank_address," +
                    " micr_code," +
                    " ifsc_code," +
                    " bankaccount_name," +
                    " bankaccounttype_gid," +
                    " bankaccounttype_name," +
                    " bankaccount_number," +
                    " confirmbankaccountnumber," +
                    " joinaccount_status," +
                    " joinaccount_name," +
                    " primary_status," +
                    " chequebook_status," +
                    " accountopen_date," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    //"'" + values.application_gid + "'," +
                    "'" + values.bank_name + "'," +
                    "'" + values.branch_name.Replace("'", "") + "'," +
                    "'" + values.bank_address.Replace("'", "") + "'," +
                    "'" + values.micr_code + "'," +
                    "'" + values.ifsc_code + "'," +
                    "'" + values.bankaccount_name.Replace("'", "") + "'," +
                    "'" + values.bankaccounttype_gid + "'," +
                    "'" + values.bankaccounttype_name + "'," +
                    "'" + values.bankaccount_number + "'," +
                    "'" + values.confirmbankaccountnumber + "'," +
                    "'" + values.joint_account + "'," +
                    "'" + values.jointaccountholder_name + "'," +
                    "'" + values.primary_status + "'," +
                    "'" + values.chequebook_status + "',";
            if (values.accountopen_date == null || values.accountopen_date == "")
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.accountopen_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            msSQL += "'" + employee_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                msSQL = " select contact2bankdtl_gid,bank_name,branch_name,ifsc_code,bankaccount_number from " +
                          " agr_mst_tbyronboardcontact2bankdtl where contact_gid='" + values.contact_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcreditbankacc_list = new List<BuyerOnboardIndividual2bankacc_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getcreditbankacc_list.Add(new BuyerOnboardIndividual2bankacc_list
                        {
                            bank_name = dt["bank_name"].ToString(),
                            branch_name = dt["branch_name"].ToString(),
                            ifsc_code = dt["ifsc_code"].ToString(),
                            bankaccount_number = dt["bankaccount_number"].ToString(),

                        });
                        values.BuyerOnboardIndividual2bankacc_list = getcreditbankacc_list;
                    }
                }
                dt_datatable.Dispose();
                values.status = true;
                values.message = "Bank Account Details Added Successfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured While Adding Bank Account Details";
                return false;
            }

        }

        public void DaGetIndividualBankAccDtl(string employee_gid, MdlBuyerOnboardIndividual2BankAcc values)
        {
            msSQL = " select contact2bankdtl_gid,contact_gid,bank_name,branch_name,ifsc_code,bankaccount_number,primary_status, " +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                    " date_format(a.updated_date, '%d-%m-%Y %h:%i %p') as updated_date, " +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, " +
                    " concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as updated_by " +
                    " from agr_mst_tbyronboardcontact2bankdtl a " +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " left join hrm_mst_temployee d on a.updated_by = d.employee_gid " +
                    " left join adm_mst_tuser e on e.user_gid = d.user_gid " +
                    " where contact_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcreditbankacc_list = new List<BuyerOnboardIndividual2bankacc_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getcreditbankacc_list.Add(new BuyerOnboardIndividual2bankacc_list
                    {
                        bank_name = dt["bank_name"].ToString(),
                        branch_name = dt["branch_name"].ToString(),
                        ifsc_code = dt["ifsc_code"].ToString(),
                        bankaccount_number = dt["bankaccount_number"].ToString(),
                        contact2bankdtl_gid = dt["contact2bankdtl_gid"].ToString(),
                        contact_gid = dt["contact_gid"].ToString(),
                        primary_status = dt["primary_status"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        updated_date = dt["updated_date"].ToString(),
                        updated_by = dt["updated_by"].ToString(),
                    });
                    values.BuyerOnboardIndividual2bankacc_list = getcreditbankacc_list;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaDeleteIndividualBankAcc(string contact2bankdtl_gid, string contact_gid, MdlBuyerOnboardIndividual2BankAcc values, string employee_gid)
        {
            msSQL = "delete from agr_mst_tbyronboardcontact2bankdtl where contact2bankdtl_gid='" + contact2bankdtl_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Bank Account Details Deleted successfully";

                msSQL = " select contact2bankdtl_gid,bank_name,branch_name,ifsc_code,bankaccount_number from " +
                           " agr_mst_tbyronboardcontact2bankdtl where contact_gid='" + contact_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcreditbankacc_list = new List<BuyerOnboardIndividual2bankacc_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getcreditbankacc_list.Add(new BuyerOnboardIndividual2bankacc_list
                        {
                            bank_name = dt["bank_name"].ToString(),
                            branch_name = dt["branch_name"].ToString(),
                            ifsc_code = dt["ifsc_code"].ToString(),
                            bankaccount_number = dt["bankaccount_number"].ToString(),

                        });
                        values.BuyerOnboardIndividual2bankacc_list = getcreditbankacc_list;
                    }
                }
                dt_datatable.Dispose();
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred while deleting";
            }
        }


        public void DaIndividual2bankTmpList(string contact_gid, string employee_gid, MdlBuyerOnboardIndividual2BankAcc values)
        {
            msSQL = " select contact2bankdtl_gid,bank_name,branch_name,ifsc_code,bankaccount_number, primary_status, " +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                    " date_format(a.updated_date, '%d-%m-%Y %h:%i %p') as updated_date, " +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, " +
                    " concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as updated_by " +
                    " from agr_mst_tbyronboardcontact2bankdtl a " +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " left join hrm_mst_temployee d on a.updated_by = d.employee_gid " +
                    " left join adm_mst_tuser e on e.user_gid = d.user_gid " +
                    " where contact_gid='" + contact_gid + "' or contact_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstlicense_list = new List<BuyerOnboardIndividual2bankacc_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getmstlicense_list.Add(new BuyerOnboardIndividual2bankacc_list
                    {
                        bank_name = dt["bank_name"].ToString(),
                        branch_name = dt["branch_name"].ToString(),
                        ifsc_code = dt["ifsc_code"].ToString(),
                        primary_status = dt["primary_status"].ToString(),
                        bankaccount_number = dt["bankaccount_number"].ToString(),
                        contact2bankdtl_gid = dt["contact2bankdtl_gid"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        updated_date = dt["updated_date"].ToString(),
                        updated_by = dt["updated_by"].ToString(),
                    });
                }
                values.BuyerOnboardIndividual2bankacc_list = getmstlicense_list;
            }
            dt_datatable.Dispose();
        }

        public void DaEditGetIndividualBankAccDtl(string contact2bankdtl_gid, MdlBuyerOnboardIndividual2BankAcc values)
        {
            try
            {
                msSQL = "select contact2bankdtl_gid,contact_gid,application_gid,bank_name,branch_name,bank_address,micr_code,ifsc_code,bankaccount_name,primary_status," +
                " bankaccounttype_gid,bankaccounttype_name,bankaccount_number,confirmbankaccountnumber,joinaccount_status,joinaccount_name," +
                " chequebook_status,accountopen_date from agr_mst_tbyronboardcontact2bankdtl where contact2bankdtl_gid='" + contact2bankdtl_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.contact2bankdtl_gid = objODBCDatareader["contact2bankdtl_gid"].ToString();
                    values.contact_gid = objODBCDatareader["contact_gid"].ToString();
                    values.application_gid = objODBCDatareader["application_gid"].ToString();
                    values.bank_name = objODBCDatareader["bank_name"].ToString();
                    values.branch_name = objODBCDatareader["branch_name"].ToString();
                    values.bank_address = objODBCDatareader["bank_address"].ToString();
                    values.micr_code = objODBCDatareader["micr_code"].ToString();
                    values.ifsc_code = objODBCDatareader["ifsc_code"].ToString();
                    values.bankaccount_name = objODBCDatareader["bankaccount_name"].ToString();
                    values.chequebook_status = objODBCDatareader["chequebook_status"].ToString();
                    values.bankaccounttype_gid = objODBCDatareader["bankaccounttype_gid"].ToString();
                    values.bankaccounttype_name = objODBCDatareader["bankaccounttype_name"].ToString();
                    values.bankaccount_number = objODBCDatareader["bankaccount_number"].ToString();
                    values.confirmbankaccountnumber = objODBCDatareader["confirmbankaccountnumber"].ToString();
                    values.joint_account = objODBCDatareader["joinaccount_status"].ToString();
                    values.jointaccountholder_name = objODBCDatareader["joinaccount_name"].ToString();
                    values.chequebook_status = objODBCDatareader["chequebook_status"].ToString();
                    values.primary_status = objODBCDatareader["primary_status"].ToString();

                    if (objODBCDatareader["accountopen_date"].ToString() == "")
                    {
                    }
                    else
                    {
                        values.accountopen_date = Convert.ToDateTime(objODBCDatareader["accountopen_date"]).ToString("MM-dd-yyyy");
                    }

                }
                values.status = true;


                dt_datatable.Dispose();
                values.message = "success";
                objODBCDatareader.Close();
            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }
        }

        public void DaUpdateIndividualBankAccDtl(string employee_gid, MdlBuyerOnboardIndividual2BankAcc values)
        {

            msSQL = " select contact2bankdtl_gid,contact_gid,application_gid,bank_name,branch_name,bank_address,micr_code,ifsc_code,bankaccount_name," +
                " bankaccounttype_gid,bankaccounttype_name,bankaccount_number,confirmbankaccountnumber,joinaccount_status,joinaccount_name," +
                " chequebook_status,accountopen_date from agr_mst_tbyronboardcontact2bankdtl where contact2bankdtl_gid='" + values.contact2bankdtl_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsbank_name = objODBCDatareader["bank_name"].ToString();
                lsbranch_name = objODBCDatareader["branch_name"].ToString();
                lsbank_address = objODBCDatareader["bank_address"].ToString();
                lsmicr_code = objODBCDatareader["micr_code"].ToString();
                lsifsc_code = objODBCDatareader["ifsc_code"].ToString();
                lsbankaccount_name = objODBCDatareader["bankaccount_name"].ToString();
                lsbankaccounttype_gid = objODBCDatareader["bankaccounttype_gid"].ToString();
                lsbankaccounttype_name = objODBCDatareader["bankaccounttype_name"].ToString();
                lsbankaccount_number = objODBCDatareader["bankaccount_number"].ToString();
                lsconfirmbankaccountnumber = objODBCDatareader["confirmbankaccountnumber"].ToString();
                lsjoinaccount_status = objODBCDatareader["joinaccount_status"].ToString();
                lsjoinaccount_name = objODBCDatareader["joinaccount_name"].ToString();
                lschequebook_status = objODBCDatareader["chequebook_status"].ToString();
                lsaccountopen_date = objODBCDatareader["accountopen_date"].ToString();
                lscontact_gid = objODBCDatareader["contact_gid"].ToString();
                lsapplication_gid = objODBCDatareader["application_gid"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " update agr_mst_tbyronboardcontact2bankdtl set " +
                     " bank_name='" + values.bank_name + "'," +
                     " branch_name='" + values.branch_name.Replace("'", "") + "'," +
                     " bank_address='" + values.bank_address.Replace("'", "") + "'," +
                     " micr_code='" + values.micr_code + "'," +
                     " ifsc_code='" + values.ifsc_code + "'," +
                     " bankaccount_name='" + values.bankaccount_name.Replace("'", "") + "'," +
                     " bankaccounttype_gid='" + values.bankaccounttype_gid + "'," +
                     " bankaccounttype_name='" + values.bankaccounttype_name + "'," +
                     " bankaccount_number='" + values.bankaccount_number + "'," +
                     " confirmbankaccountnumber='" + values.confirmbankaccountnumber + "'," +
                     " joinaccount_status='" + values.joint_account + "'," +
                     " joinaccount_name='" + values.jointaccountholder_name + "'," +
                     " primary_status='" + values.primary_status + "'," +
                     " chequebook_status='" + values.chequebook_status + "',";
            if ((values.accountopen_date == null) || (values.accountopen_date == ""))
            {
                msSQL += "accountopen_date=null,";
            }
            else
            {
                msSQL += "accountopen_date= '" + Convert.ToDateTime(values.accountopen_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }

            msSQL +=
                     " updated_by='" + employee_gid + "'," +
                     " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                     " where contact2bankdtl_gid='" + values.contact2bankdtl_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult == 1)
            {//Date Updation
             //if (Convert.ToDateTime(values.accountopendate).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
             //{

                //}
                //else
                //{
                //    msSQL = "update agr_mst_tbyronboardcontact2bankdtl set accountopen_date='" + Convert.ToDateTime(values.accountopendate).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "'" +
                //        "where contact2bankdtl_gid='" + values.contact2bankdtl_gid + "' ";
                //    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                //}

                msGetGid = objcmnfunctions.GetMasterGID("I2BU");

                msSQL = " insert into agr_mst_tbyronboardcontact2bankdtlupdatelog(" +
                    " contact2bankdtlupdatelog_gid," +
                    " contact2bankdtl_gid," +
                    " contact_gid," +
                    " application_gid," +
                    " bank_name," +
                    " branch_name," +
                    " bank_address," +
                    " micr_code," +
                    " ifsc_code," +
                    " bankaccount_name," +
                    " bankaccounttype_gid," +
                    " bankaccounttype_name," +
                    " bankaccount_number," +
                    " confirmbankaccountnumber," +
                    " joinaccount_status," +
                    " joinaccount_name," +
                    " chequebook_status," +
                    " accountopen_date," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.contact2bankdtl_gid + "'," +
                    "'" + lscontact_gid + "'," +
                    "'" + lsapplication_gid + "'," +
                    "'" + lsbank_name + "'," +
                    "'" + lsbranch_name.Replace("'", "") + "'," +
                    "'" + lsbank_address.Replace("'", "") + "'," +
                    "'" + lsmicr_code + "'," +
                    "'" + lsifsc_code + "'," +
                    "'" + lsbankaccount_name + "'," +
                    "'" + lsbankaccounttype_gid + "'," +
                    "'" + lsbankaccounttype_name + "'," +
                    "'" + lsbankaccount_number + "'," +
                    "'" + lsconfirmbankaccountnumber + "'," +
                    "'" + lsjoinaccount_status + "'," +
                    "'" + lsjoinaccount_name + "'," +
                    "'" + lschequebook_status + "'," +
                    "'" + lsaccountopen_date + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Bank Account Details Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while updating Bank Account";
            }
        }


        public void DaPostOnboardlglstatus(string employee_gid, MdlOboardlglstatuslist values)
        {

            msSQL = " insert into agr_mst_tonboardlgltagstatuslog(" +
                    " byronboard_gid," +
                    " lgltag_status," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + values.onboard_gid + "'," +
                    "'" + values.lgltag_status + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult == 1)
            {
                msSQL = " update agr_mst_tbyronboard set " +
                        " lgltag_status= '" + values.lgltag_status + "'" +
                        " where application_gid='" + values.onboard_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            }



            if (mnResult != 0)
            {
                if (ConfigurationManager.AppSettings["sysSamagroHyperbrdigeAPIEnable"].ToString() == "Yes")
                {
                    HttpContext ctx = HttpContext.Current;

                    Thread t = new Thread(new ThreadStart(() =>
                    {
                        HttpContext.Current = ctx;

                        //Update Tag Status

                        objFnSamAgroHBAPIConnEdit.UpdateBuyerTagStatusHBAPI(values.onboard_gid);

                    }));

                    t.Start();
                }


                values.status = true;
                values.message = " Onboard Legal status tagged Successfully as " + values.lgltag_status + " !";
            }

            else
            {
                values.status = false;
                values.message = "Error Occured!";
            }
        }

        public void DaGetOnboardLgltagstatusLog(string onboard_gid, MdlOboardlglstatus values)
        {
            msSQL = " select lgltag_status," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date " +
                    " from agr_mst_tonboardlgltagstatuslog a " +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid" +
                    " where a.byronboard_gid='" + onboard_gid + "' order by onboardlgltagstatuslog_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcicinstitutionList = new List<MdlOboardlglstatuslist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getcicinstitutionList.Add(new MdlOboardlglstatuslist
                    {
                        lgltag_status = dt["lgltag_status"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                    });

                }
                values.MdlOboardlglstatuslist = getcicinstitutionList;
            }
            dt_datatable.Dispose();
            values.status = true;
        }
        public void DaGetOnboardLimitManagementdtl(string onboard_gid, MdlOnboardLimitManagement values)
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
            msSQL = " select application2loan_gid, producttype_gid,product_type from agr_mst_tapplication2loan " +
                    " where application_gid in (select application_gid from agr_mst_tapplication " +
                    " where buyeronboard_gid = '" + onboard_gid + "' and process_type = 'Accept') group by producttype_gid ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getMdlProductTypeList = new List<MdlProductTypeList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getMdlProductTypeList.Add(new MdlProductTypeList
                    {
                        producttype_gid = dt["producttype_gid"].ToString(),
                        producttype_name = dt["product_type"].ToString(),
                        application2loan_gid = dt["application2loan_gid"].ToString(),
                    });

                }
                values.MdlProductTypeList = getMdlProductTypeList;
            }
            dt_datatable.Dispose();

            msSQL = " select application_gid,producttype_gid, productsubtype_gid,productsub_type from agr_mst_tapplication2loan " +
                    " where application_gid in (select application_gid from agr_mst_tapplication " +
                    " where  buyeronboard_gid = '" + onboard_gid + "' and process_type = 'Accept') group by productsubtype_gid ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getMdlProductSubTypeList = new List<MdlProductSubTypeList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getMdlProductSubTypeList.Add(new MdlProductSubTypeList
                    {
                        producttype_gid = dt["producttype_gid"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        productsubtype_name = dt["productsub_type"].ToString(),
                        productsubtype_gid = dt["productsubtype_gid"].ToString(),
                    });

                }
                values.MdlProductSubTypeList = getMdlProductSubTypeList;
            }
            dt_datatable.Dispose();


            msSQL = " select application_gid,producttype_gid, productsubtype_gid,productsub_type from agr_mst_tapplication2loan " +
                   " where application_gid in (select application_gid from agr_mst_tapplication " +
                   " where  buyeronboard_gid = '" + onboard_gid + "' and process_type = 'Accept')";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getMdlProductSubTypeAppList = new List<MdlProductSubTypeList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getMdlProductSubTypeAppList.Add(new MdlProductSubTypeList
                    {
                        producttype_gid = dt["producttype_gid"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        productsubtype_name = dt["productsub_type"].ToString(),
                        productsubtype_gid = dt["productsubtype_gid"].ToString(),
                    });

                }
                values.MdlProductSubTypeApplicationList = getMdlProductSubTypeAppList;
            }
            dt_datatable.Dispose();

            msSQL = " select application_gid,renewal_flag,amendment_flag, application_no as 'ARN', contract_id,  " +
                    " case when DATE(validityto_date) <= DATE(NOW()) then 'Expired' " +
                    " when DATE(validityto_date) > DATE(NOW())  then 'Active' else '' end as 'ContractStatus', " +
                    "  (select sum(loanfacility_amount) from agr_mst_tapplication2loan " +
                    " where application_gid = a.application_gid) as product_overallamount, " +
                    " date_format(validityto_date, '%d-%m-%Y') as validityto_date " +
                    " from agr_mst_tapplication a where buyeronboard_gid = '" + onboard_gid + "' and process_type = 'Accept'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getMdlApplicationList = new List<MdlApplicationList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getMdlApplicationList.Add(new MdlApplicationList
                    {
                        application_no = dt["ARN"].ToString(),
                        contract_id = dt["contract_id"].ToString(),
                        contract_status = dt["ContractStatus"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        product_overallamount = dt["product_overallamount"].ToString(),
                        processupdated_date = dt["validityto_date"].ToString(),
                        renewal_flag = dt["renewal_flag"].ToString(),
                        amendment_flag = dt["amendment_flag"].ToString(),
                         //application2loan_gid = dt["application2loan_gid"].ToString(),

                    });

                }
                values.MdlApplicationList = getMdlApplicationList;
            }
            dt_datatable.Dispose();

            //result1234 response = objFnSamAgroHBAPIContract.fnNsLimitAPI(onboard_gid);

            msSQL = " select  a.application_gid,concat(product_type, ' / ',productsub_type)  as 'facility', a.application2loan_gid,  " +
                   " loanfacility_amount as 'ApprovedLimit'," +
                   " utilized_limit as 'UtilizedLimit',available_limit as 'AvailableLimit'," +
                   " date_format(programlimit_validdfrom, '%d-%m-%Y') as 'ValidFrom' , overdue_balance," +
                   " date_format(programlimit_validdto, '%d-%m-%Y') as 'ValidTo' , " +
                   " case when DATE(programlimit_validdto) < DATE(NOW()) then 'Expired' " +
                   " when DATE(programlimit_validdto) >= DATE(NOW())  then 'Active' else '' end as 'FacilityStatus' " +
                   " from agr_mst_tapplication2loan a " +
                   " left join agr_mst_tapplication b on a.application_gid = b.application_gid " +
                   " where b.buyeronboard_gid = '" + onboard_gid + "' and b.process_type = 'Accept'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getMdlFaclilitydtl = new List<MdlFaclilitydtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                 //   msSQL = " select  date_format(processupdated_date, '%d-%m-%Y') as 'processupdated_date' from agr_mst_tapplication " +
                 //" where application_gid = '" + dt["application_gid"].ToString() + "' and process_type = 'Accept' and (renewal_flag = 'Y' or amendment_flag = 'Y')";
                 //   string lsprocessupdated_date = objdbconn.GetExecuteScalar(msSQL);

                    getMdlFaclilitydtl.Add(new MdlFaclilitydtl
                    {
                        facility = dt["Facility"].ToString(),
                        ApprovedLimit = dt["ApprovedLimit"].ToString(),
                        UtilizedLimit = dt["UtilizedLimit"].ToString(),
                        AvailableLimit = dt["AvailableLimit"].ToString(),
                        ValidFrom = dt["ValidFrom"].ToString(),
                        ValidTo = dt["ValidTo"].ToString(),
                        FacilityStatus = dt["FacilityStatus"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        application2loan_gid = dt["application2loan_gid"].ToString(),
                        overdue_balance = dt["overdue_balance"].ToString()
                        //processupdated_date = lsprocessupdated_date
                    });

                }
                values.MdlFaclilitydtl = getMdlFaclilitydtl;
            }
            dt_datatable.Dispose();


            //msSQL = " select  processupdated_date from agr_mst_tapplication " +        
            //      " where buyeronboard_gid = '" + onboard_gid + "' and process_type = 'Accept' and (renewal_flag = 'Y' or amendment_flag = 'Y')";
            //dt_datatable = objdbconn.GetDataTable(msSQL);
            //var getMdlPmgExpiryDate = new List<MdlPmgExpiryDate>();
            //if (dt_datatable.Rows.Count != 0)
            //{
            //    foreach (DataRow dt in dt_datatable.Rows)
            //    {
            //        getMdlPmgExpiryDate.Add(new MdlPmgExpiryDate
            //        {
            //            processupdated_date = dt["processupdated_date"].ToString(),

            //        });

            //    }
            //    values.MdlPmgExpiryDate = getMdlPmgExpiryDate;
            //}
            //dt_datatable.Dispose();
            //values.message = response.message;
            values.status = true;
        }

        public void DaGetcompanydocumentlist(string employee_gid, BuyerOnboardinstitutionuploaddocument values)
        {

            msSQL = " select institution2documentupload_gid,institution_gid,document_name,document_path,document_title,document_id from agr_mst_tbyronboardinstitution2documentupload " +
                                " where institution_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdocumentdtlList = new List<BuyerOnboardinstitutionupload_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getdocumentdtlList.Add(new BuyerOnboardinstitutionupload_list
                    {
                        document_name = dt["document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData((dt["document_path"].ToString())),
                        institution_gid = dt["institution_gid"].ToString(),
                        institution2documentupload_gid = dt["institution2documentupload_gid"].ToString(),
                        document_title = dt["document_title"].ToString(),
                        document_id = dt["document_id"].ToString(),
                    });
                    values.BuyerOnboardinstitutionupload_list = getdocumentdtlList;
                }
            }
            dt_datatable.Dispose();

            values.status = true;
        }


        public void DaGetcompanyeditdocumentlist(string employee_gid, string institution_gid, BuyerOnboardinstitutionuploaddocument values)
        {

            msSQL = " select institution2documentupload_gid,institution_gid,document_name,document_path,document_title,document_id from agr_mst_tbyronboardinstitution2documentupload " +
                                " where institution_gid='" + employee_gid + "' or institution_gid='" + institution_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdocumentdtlList = new List<BuyerOnboardinstitutionupload_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getdocumentdtlList.Add(new BuyerOnboardinstitutionupload_list
                    {
                        document_name = dt["document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData((dt["document_path"].ToString())),
                        institution_gid = dt["institution_gid"].ToString(),
                        institution2documentupload_gid = dt["institution2documentupload_gid"].ToString(),
                        document_title = dt["document_title"].ToString(),
                        document_id = dt["document_id"].ToString(),
                    });
                    values.BuyerOnboardinstitutionupload_list = getdocumentdtlList;
                }
            }
            dt_datatable.Dispose();

            values.status = true;
        }


        public void DaGetOnboardLimitFacilitydtl(string application_gid, MdlFaclilityList values)
        {

            msSQL = " select  concat(product_type, ' / ',productsub_type)  as 'facility', " +
                    " loanfacility_amount as 'ApprovedLimit', " +
                    " date_format(programlimit_validdfrom, '%d-%m-%Y') as 'ValidFrom' , " +
                    " date_format(programlimit_validdto, '%d-%m-%Y') as 'ValidTo' , " +
                    " case when DATE(programlimit_validdto) < DATE(NOW()) then 'Expired' " +
                    " when DATE(programlimit_validdto) >= DATE(NOW())  then 'Active' else '' end as 'FacilityStatus' " +
                    " from agr_mst_tapplication2loan where application_gid = '" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getMdlFaclilitydtl = new List<MdlFaclilitydtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getMdlFaclilitydtl.Add(new MdlFaclilitydtl
                    {
                        facility = dt["Facility"].ToString(),
                        ApprovedLimit = dt["ApprovedLimit"].ToString(),
                        ValidFrom = dt["ValidFrom"].ToString(),
                        ValidTo = dt["ValidTo"].ToString(),
                        FacilityStatus = dt["FacilityStatus"].ToString(),
                    });

                }
                values.MdlFaclilitydtl = getMdlFaclilitydtl;
            }
            dt_datatable.Dispose();

            values.status = true;
        }

        // Buyer/Supplier Type

        public void DaGetBuyerSupplierType(MdlBuyerSupplierType objbuyersuppliertype)
        {
            try
            {
                msSQL = " SELECT buyersuppliertype_gid, buyersuppliertype_name " +
                        " FROM agr_mst_tbuyersuppliertype a where status='Y' order by buyersuppliertype_gid desc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getapplication_list = new List<BuyerSupplierType_List>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getapplication_list.Add(new BuyerSupplierType_List
                        {
                            buyersuppliertype_gid = (dr_datarow["buyersuppliertype_gid"].ToString()),
                            buyersuppliertype_name = (dr_datarow["buyersuppliertype_name"].ToString()),

                        });
                    }
                    objbuyersuppliertype.BuyerSupplierType_List = getapplication_list;
                }
                dt_datatable.Dispose();
                objbuyersuppliertype.status = true;
            }
            catch
            {
                objbuyersuppliertype.status = false;
            }
        }


        public void DaUpdateGSTHeadOffice(string employee_gid, MdlGSTHeadOffice values)
        {
            msSQL = " update agr_mst_tbyronboardinstitution2branch set headoffice_status = 'Yes' " +
                    " where institution2branch_gid = '" + values.institution2branch_gid + "' " +
                    " and institution_gid = '" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msSQL = " update agr_mst_tbyronboardinstitution2branch set headoffice_status='No' " +
                        " where institution2branch_gid<>'" + values.institution2branch_gid + "' " +
                        " and institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Head Office Confirmed Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
            }

        }

    }


}