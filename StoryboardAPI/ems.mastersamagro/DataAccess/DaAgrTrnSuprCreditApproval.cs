﻿using ems.mastersamagro.Models;
using ems.utilities.Functions;
using System;
using System.Web;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace ems.mastersamagro.DataAccess
{
    /// <summary>
    /// This DataAccess will help to pull data's from back end and trigger approvals from credit risk approval stage 
    /// </summary>
    /// <remarks>Written by Logapriya </remarks>
    public class DaAgrTrnSuprCreditApproval
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        OdbcDataReader objODBCDatareader, objODBCDatareader1;
        DataTable dt_datatable;
        string msSQL, msGetGid;
        int mnResult;
        string lsrmquery_flag, lsunderwriting_flag;
        string sToken = string.Empty;
        Random rand = new Random();
        private string cc_mailid, lsBccmail_id;
        private IEnumerable<string> lsCCReceipients, lsBCCReceipients;
        private string body;
        private string sub;
        private int ls_port;
        private string ls_server;
        private string ls_username;
        private string ls_password;
        private string tomail_id;
        private string lssource;
        private string cluster_head_mailid;
        private string regional_head_mailid;
        private string business_head_mailid;
        private string zonalhead_mailid;
        private string reportingto_name;
        private string reportingto_mailid;
        private string customer_name;
        private string application_no;
        private string reportingto_gid;
        private string creditmanager_mailid;
        private string nationalmanager_mailid;
        private string credithead_mailid;
        private string relationshipmanager_mailid;
        private string regionalmanager_mailid;
        private string creater_name;
        private string head_name;
        private string content;
        private string creditmanager_name;
        private string creditnationalmanager_name;
        private string creditregionalmanager_name;
        private string credithead_name;
        string creditregionalmanager_mailid, creditnationalmanager_mailid;
        public string closure_time;
        public string relationshipmanager_name;
        private IEnumerable<string> lstoReceipients;
        private string creater_mailid;
        private string lsstatus;

        public void DaGetMyAppAssignedSummary(string employee_gid, MdlMstCreditApproval values)
        {
            msSQL = " select a.application_gid,a.application_no,a.customerref_name as customer_name,a.customer_urn,a.vertical_name," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,a.status,a.applicant_type,a.created_by as createdby," +
                    " date_format(a.submitted_date,'%d-%m-%Y %h:%i %p') as updated_date, a.productcharge_flag, a.economical_flag,a.approval_status, " +
                    " a.overalllimit_amount, region,a.creditgroup_gid,d.initiate_flag,appcreditapproval_gid, " +
                    " concat(t.user_firstname,' ',t.user_lastname,' / ',t.user_code)  as creditassigned_by,date_format(a.creditassigned_date,'%d-%m-%Y %h:%i %p') as creditassigned_date," +
                    " creditheadapproval_status,date_format(a.creditheadapproval_date,'%d-%m-%Y %h:%i %p') as creditheadapproval_date,a.product_gid,a.variety_gid from agr_mst_tsuprapplication a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.submitted_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                    " left join hrm_mst_temployee s on s.employee_gid=a.creditassigned_by " +
                    " left join adm_mst_tuser t on t.user_gid=s.user_gid " +
                    " left join agr_trn_tsuprAppcreditapproval d on a.application_gid=d.application_gid " +
                    " where a.approval_flag='Y' and d.approval_gid='" + employee_gid + "' and  d.approval_status='Pending' and d.hierary_level='0' group by a.application_gid order by a.updated_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicaition_list = new List<applicaition_list>();
            if (dt_datatable.Rows.Count != 0)
            {

                foreach (DataRow dt in dt_datatable.Rows)
                {


                    getapplicaition_list.Add(new applicaition_list
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical_name = dt["vertical_name"].ToString(),
                        created_date = dt["updated_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        economical_flag = dt["economical_flag"].ToString(),
                        productcharge_flag = dt["productcharge_flag"].ToString(),
                        application_status = dt["status"].ToString(),
                        applicant_type = dt["applicant_type"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        region = dt["region"].ToString(),
                        overalllimit_amount = dt["overalllimit_amount"].ToString(),
                        creditgroup_gid = dt["creditgroup_gid"].ToString(),
                        createdby = dt["createdby"].ToString(),
                        initiate_flag = dt["initiate_flag"].ToString(),
                        appcreditapproval_gid = dt["appcreditapproval_gid"].ToString(),
                        creditassigned_date = dt["creditassigned_date"].ToString(),
                        creditassigned_by = dt["creditassigned_by"].ToString(),
                        creditheadapproval_status = dt["creditheadapproval_status"].ToString(),
                        creditheadapproval_date = dt["creditheadapproval_date"].ToString(),
                        product_gid = dt["product_gid"].ToString(),
                        variety_gid = dt["variety_gid"].ToString()
                    });

                }
            }
            values.applicaition_list = getapplicaition_list;
            dt_datatable.Dispose();
        }

        public void DaGetMyAppSubmittedSummary(string employee_gid, MdlMstCreditApproval values)
        {
            msSQL = " select a.application_gid,a.application_no,a.customerref_name as customer_name,a.customer_urn,a.vertical_name," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,a.status,a.applicant_type,a.created_by as createdby," +
                    " date_format(a.submitted_date,'%d-%m-%Y %h:%i %p') as updated_date, a.productcharge_flag, a.economical_flag,a.approval_status, " +
                    " a.overalllimit_amount, region,a.creditgroup_gid,d.initiate_flag,appcreditapproval_gid," +
                    " concat(t.user_firstname,' ',t.user_lastname,' / ',t.user_code)  as creditassigned_by,date_format(a.creditassigned_date,'%d-%m-%Y %h:%i %p') as creditassigned_date," +
                    " creditheadapproval_status,date_format(a.creditheadapproval_date,'%d-%m-%Y %h:%i %p') as creditheadapproval_date  from agr_mst_tsuprapplication a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.submitted_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                    " left join hrm_mst_temployee s on s.employee_gid=a.creditassigned_by " +
                    " left join adm_mst_tuser t on t.user_gid=s.user_gid " +
                    " left join agr_trn_tsuprAppcreditapproval d on a.application_gid=d.application_gid " +
                    " where a.approval_flag='Y' and d.approval_gid='" + employee_gid + "' and  d.approval_status='Submitted to Approval' and  a.approval_status <>'Submitted to CC' and a.approval_status <>'CC Approved' and a.approval_status <>'CC Rejected' and" +
                    " (a.approval_status='Submitted to Credit Approval' or a.approval_status like '%Approved' or a.creditheadapproval_status ='Comment Raised to RM' or a.creditheadapproval_status='Comment Raised' or a.creditheadapproval_status ='Comment Closed') and d.hierary_level='0' and " +
                    "  a.application_gid not in (select application_gid from agr_mst_tsuprapplication where creditheadapproval_status like '%Rejected' or creditheadapproval_status like '%Hold'  ) " +
                    " group by a.application_gid order by a.updated_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicaition_list = new List<applicaition_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                string lsapproval_flag;
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msSQL = " select application_gid from agr_trn_tsuprapplicationcreditquery where query_status = 'Open' and application_gid = '" + dt["application_gid"].ToString() + "' " +
                            " and (queryraised_to = 'RM' or queryraised_to = 'Credit')";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == false)
                    {
                        lsunderwriting_flag = "Y";
                    }
                    else
                    {
                        lsunderwriting_flag = "N";
                    }
                    objODBCDatareader.Close();
                    msSQL = "select a.appcreditquery_gid from agr_trn_tsuprapplicationcreditquery a " +
                            "left join agr_trn_tsuprAppcreditapproval b on a.appcreditapproval_gid=b.appcreditapproval_gid " +
                            "where a.application_gid='" + dt["application_gid"].ToString() + "' and a.query_status='Open' and b.hierary_level<>'0'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lsapproval_flag = "Y";
                    }
                    else
                    {
                        lsapproval_flag = "N";
                    }
                    objODBCDatareader.Close();
                    getapplicaition_list.Add(new applicaition_list
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical_name = dt["vertical_name"].ToString(),
                        created_date = dt["updated_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        economical_flag = dt["economical_flag"].ToString(),
                        productcharge_flag = dt["productcharge_flag"].ToString(),
                        application_status = dt["status"].ToString(),
                        applicant_type = dt["applicant_type"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        region = dt["region"].ToString(),
                        overalllimit_amount = dt["overalllimit_amount"].ToString(),
                        creditgroup_gid = dt["creditgroup_gid"].ToString(),
                        createdby = dt["createdby"].ToString(),
                        initiate_flag = dt["initiate_flag"].ToString(),
                        appcreditapproval_gid = dt["appcreditapproval_gid"].ToString(),
                        creditheadapproval_status = dt["creditheadapproval_status"].ToString(),
                        creditheadapproval_date = dt["creditheadapproval_date"].ToString(),
                        creditassigned_date = dt["creditassigned_date"].ToString(),
                        creditassigned_by = dt["creditassigned_by"].ToString(),
                        approval_flag = lsapproval_flag,
                        underwriting_flag = lsunderwriting_flag
                    });

                }
            }
            values.applicaition_list = getapplicaition_list;
            dt_datatable.Dispose();
        }


        public void DaGetMyAppSubmittedtoccSummary(string employee_gid, MdlMstCreditApproval values)
        {
            msSQL = " select a.application_gid,a.application_no,a.customerref_name as customer_name,a.customer_urn,a.vertical_name," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,a.status,a.applicant_type,a.created_by as createdby," +
                    " date_format(a.submitted_date,'%d-%m-%Y %h:%i %p') as updated_date, a.productcharge_flag, a.economical_flag,a.approval_status, " +
                    " a.overalllimit_amount, region,a.creditgroup_gid,d.initiate_flag,appcreditapproval_gid," +
                    " concat(t.user_firstname,' ',t.user_lastname,' / ',t.user_code)  as creditassigned_by,date_format(a.creditassigned_date,'%d-%m-%Y %h:%i %p') as creditassigned_date," +
                    " creditheadapproval_status,date_format(a.creditheadapproval_date,'%d-%m-%Y %h:%i %p') as creditheadapproval_date  from agr_mst_tsuprapplication a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.submitted_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                    " left join hrm_mst_temployee s on s.employee_gid=a.creditassigned_by " +
                    " left join adm_mst_tuser t on t.user_gid=s.user_gid " +
                    " left join agr_trn_tsuprAppcreditapproval d on a.application_gid=d.application_gid " +
                    " where a.approval_flag='Y' and d.approval_gid='" + employee_gid + "' and  a.approval_status='Submitted to CC' and d.hierary_level='0' group by a.application_gid order by a.updated_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicaition_list = new List<applicaition_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getapplicaition_list.Add(new applicaition_list
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical_name = dt["vertical_name"].ToString(),
                        created_date = dt["updated_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        economical_flag = dt["economical_flag"].ToString(),
                        productcharge_flag = dt["productcharge_flag"].ToString(),
                        application_status = dt["status"].ToString(),
                        applicant_type = dt["applicant_type"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        region = dt["region"].ToString(),
                        overalllimit_amount = dt["overalllimit_amount"].ToString(),
                        creditgroup_gid = dt["creditgroup_gid"].ToString(),
                        createdby = dt["createdby"].ToString(),
                        initiate_flag = dt["initiate_flag"].ToString(),
                        appcreditapproval_gid = dt["appcreditapproval_gid"].ToString(),
                        creditassigned_date = dt["creditassigned_date"].ToString(),
                        creditassigned_by = dt["creditassigned_by"].ToString(),
                        creditheadapproval_status = dt["creditheadapproval_status"].ToString(),
                        creditheadapproval_date = dt["creditheadapproval_date"].ToString()
                    });

                }
            }
            values.applicaition_list = getapplicaition_list;
            dt_datatable.Dispose();
        }

        public void DaGetMyAppcreditSubmittedtoccSummary(string employee_gid, MdlMstCreditApproval values)
        {
            msSQL = " select a.application_gid,a.application_no,a.customerref_name as customer_name,a.customer_urn,a.vertical_name," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,a.status,a.applicant_type,a.created_by as createdby," +
                    " date_format(a.submitted_date,'%d-%m-%Y %h:%i %p') as updated_date, a.productcharge_flag, a.economical_flag,a.approval_status, " +
                    " a.overalllimit_amount, region,a.creditgroup_gid,d.initiate_flag,appcreditapproval_gid," +
                    " concat(t.user_firstname,' ',t.user_lastname,' / ',t.user_code)  as creditassigned_by,date_format(a.creditassigned_date,'%d-%m-%Y %h:%i %p') as creditassigned_date," +
                    " creditheadapproval_status,date_format(a.creditheadapproval_date,'%d-%m-%Y %h:%i %p') as creditheadapproval_date  from agr_mst_tsuprapplication a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.submitted_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                    " left join hrm_mst_temployee s on s.employee_gid=a.creditassigned_by " +
                    " left join adm_mst_tuser t on t.user_gid=s.user_gid " +
                    " left join agr_trn_tsuprAppcreditapproval d on a.application_gid=d.application_gid " +
                    " where a.approval_flag='Y' and d.approval_gid='" + employee_gid + "' and  a.approval_status='Submitted to CC' and d.hierary_level<>'0' group by a.application_gid order by a.updated_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicaition_list = new List<applicaition_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    string lssubmitted_by, lssubmitted_date;
                    lssubmitted_by = objdbconn.GetExecuteScalar("select approval_name from agr_trn_tsuprAppcreditapproval  where application_gid ='" + dt["application_gid"].ToString() + "' and hierary_level = '0'");
                    lssubmitted_date = objdbconn.GetExecuteScalar("select date_format(approved_date,'%d-%m-%Y %h:%i %p') as approved_date  from agr_trn_tsuprAppcreditapproval  where application_gid ='" + dt["application_gid"].ToString() + "' and hierary_level = '0'");

                    getapplicaition_list.Add(new applicaition_list
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical_name = dt["vertical_name"].ToString(),
                        created_date = dt["updated_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        economical_flag = dt["economical_flag"].ToString(),
                        productcharge_flag = dt["productcharge_flag"].ToString(),
                        application_status = dt["status"].ToString(),
                        applicant_type = dt["applicant_type"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        region = dt["region"].ToString(),
                        overalllimit_amount = dt["overalllimit_amount"].ToString(),
                        creditgroup_gid = dt["creditgroup_gid"].ToString(),
                        createdby = dt["createdby"].ToString(),
                        initiate_flag = dt["initiate_flag"].ToString(),
                        appcreditapproval_gid = dt["appcreditapproval_gid"].ToString(),
                        creditassigned_date = dt["creditassigned_date"].ToString(),
                        creditassigned_by = dt["creditassigned_by"].ToString(),
                        creditheadapproval_status = dt["creditheadapproval_status"].ToString(),
                        creditheadapproval_date = dt["creditheadapproval_date"].ToString(),
                        submitted_by = lssubmitted_by,
                        submitted_date = lssubmitted_date
                    });

                }
            }
            values.applicaition_list = getapplicaition_list;
            dt_datatable.Dispose();
        }

        public void DaGetMyAppApprovalSummary(string employee_gid, MdlMstCreditApproval values)
        {
            msSQL = " select a.application_gid,a.application_no,a.customerref_name as customer_name,a.customer_urn,a.vertical_name," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,a.status,a.applicant_type,a.created_by as createdby," +
                    " date_format(a.submitted_date,'%d-%m-%Y %h:%i %p') as updated_date, a.productcharge_flag, a.economical_flag,a.approval_status, " +
                    " a.overalllimit_amount, region,a.creditgroup_gid,d.initiate_flag,appcreditapproval_gid," +
                    " concat(t.user_firstname,' ',t.user_lastname,' / ',t.user_code)  as creditassigned_by,date_format(a.creditassigned_date,'%d-%m-%Y %h:%i %p') as creditassigned_date," +
                    " creditheadapproval_status,date_format(a.creditheadapproval_date,'%d-%m-%Y %h:%i %p') as creditheadapproval_date  from agr_mst_tsuprapplication a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.submitted_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                    " left join hrm_mst_temployee s on s.employee_gid=a.creditassigned_by " +
                    " left join adm_mst_tuser t on t.user_gid=s.user_gid " +
                    " left join agr_trn_tsuprAppcreditapproval d on a.application_gid=d.application_gid " +
                    " where a.approval_flag='Y' and d.approval_gid='" + employee_gid + "' and  d.approval_status='Pending' " +
                    " and d.hierary_level<>'0' and a.application_gid not in (select application_gid from agr_mst_tsuprapplication where creditheadapproval_status like '%Rejected' or creditheadapproval_status like '%Hold'  ) group by a.application_gid order by a.updated_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicaition_list = new List<applicaition_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    string lssubmitted_by, lssubmitted_date;
                    lssubmitted_by = objdbconn.GetExecuteScalar("select approval_name from agr_trn_tsuprAppcreditapproval  where application_gid ='" + dt["application_gid"].ToString() + "' and hierary_level = '0'");
                    lssubmitted_date = objdbconn.GetExecuteScalar("select date_format(approved_date,'%d-%m-%Y %h:%i %p') as approved_date  from agr_trn_tsuprAppcreditapproval  where application_gid ='" + dt["application_gid"].ToString() + "' and hierary_level = '0'");

                    getapplicaition_list.Add(new applicaition_list
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical_name = dt["vertical_name"].ToString(),
                        updated_date = dt["updated_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        economical_flag = dt["economical_flag"].ToString(),
                        productcharge_flag = dt["productcharge_flag"].ToString(),
                        application_status = dt["status"].ToString(),
                        applicant_type = dt["applicant_type"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        region = dt["region"].ToString(),
                        overalllimit_amount = dt["overalllimit_amount"].ToString(),
                        creditgroup_gid = dt["creditgroup_gid"].ToString(),
                        createdby = dt["createdby"].ToString(),
                        initiate_flag = dt["initiate_flag"].ToString(),
                        appcreditapproval_gid = dt["appcreditapproval_gid"].ToString(),
                        creditassigned_date = dt["creditassigned_date"].ToString(),
                        creditassigned_by = dt["creditassigned_by"].ToString(),
                        creditheadapproval_status = dt["creditheadapproval_status"].ToString(),
                        creditheadapproval_date = dt["creditheadapproval_date"].ToString(),
                        submitted_by = lssubmitted_by,
                        submitted_date = lssubmitted_date
                    });

                }
            }
            values.applicaition_list = getapplicaition_list;
            dt_datatable.Dispose();
        }

        public void DaGetMyAppApprovedSummary(string employee_gid, MdlMstCreditApproval values)
        {
            msSQL = " select a.application_gid,a.application_no,a.customerref_name as customer_name,a.customer_urn,a.vertical_name," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,a.status,a.applicant_type,a.created_by as createdby," +
                    " date_format(a.submitted_date,'%d-%m-%Y %h:%i %p') as updated_date, a.productcharge_flag, a.economical_flag,a.approval_status, " +
                    " a.overalllimit_amount, region,a.creditgroup_gid,d.initiate_flag,appcreditapproval_gid ," +
                    " concat(t.user_firstname,' ',t.user_lastname,' / ',t.user_code)  as creditassigned_by,date_format(a.creditassigned_date,'%d-%m-%Y %h:%i %p') as creditassigned_date," +
                    " creditheadapproval_status,date_format(a.creditheadapproval_date,'%d-%m-%Y %h:%i %p') as creditheadapproval_date  from agr_mst_tsuprapplication a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.submitted_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                    " left join hrm_mst_temployee s on s.employee_gid=a.creditassigned_by " +
                    " left join adm_mst_tuser t on t.user_gid=s.user_gid " +
                    " left join agr_trn_tsuprAppcreditapproval d on a.application_gid=d.application_gid " +
                    " where a.approval_flag='Y' and d.approval_status='Approved'  and  a.approval_status <>'Submitted to CC' and  a.creditheadapproval_status like '%Approved' and d.approval_gid='" + employee_gid + "' and d.hierary_level<>'0'  group by a.application_gid order by a.updated_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicaition_list = new List<applicaition_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    string lssubmitted_by, lssubmitted_date;
                    lssubmitted_by = objdbconn.GetExecuteScalar("select approval_name from agr_trn_tsuprAppcreditapproval  where application_gid ='" + dt["application_gid"].ToString() + "' and hierary_level = '0'");
                    lssubmitted_date = objdbconn.GetExecuteScalar("select date_format(approved_date,'%d-%m-%Y %h:%i %p') as approved_date  from agr_trn_tsuprAppcreditapproval  where application_gid ='" + dt["application_gid"].ToString() + "' and hierary_level = '0'");

                    getapplicaition_list.Add(new applicaition_list
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical_name = dt["vertical_name"].ToString(),
                        updated_date = dt["updated_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        economical_flag = dt["economical_flag"].ToString(),
                        productcharge_flag = dt["productcharge_flag"].ToString(),
                        application_status = dt["status"].ToString(),
                        applicant_type = dt["applicant_type"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        region = dt["region"].ToString(),
                        overalllimit_amount = dt["overalllimit_amount"].ToString(),
                        creditgroup_gid = dt["creditgroup_gid"].ToString(),
                        createdby = dt["createdby"].ToString(),
                        initiate_flag = dt["initiate_flag"].ToString(),
                        appcreditapproval_gid = dt["appcreditapproval_gid"].ToString(),
                        creditassigned_date = dt["creditassigned_date"].ToString(),
                        creditassigned_by = dt["creditassigned_by"].ToString(),
                        creditheadapproval_status = dt["creditheadapproval_status"].ToString(),
                        creditheadapproval_date = dt["creditheadapproval_date"].ToString(),
                        submitted_by = lssubmitted_by,
                        submitted_date = lssubmitted_date
                    });

                }
            }
            values.applicaition_list = getapplicaition_list;
            dt_datatable.Dispose();
        }


        public void DaGetMyAppRejectHoldSummary(string employee_gid, MdlMstCreditApproval values)
        {
            msSQL = " select application_gid from agr_trn_tsuprAppcreditapproval where approval_gid='" + employee_gid + "'  and hierary_level='0' group by application_gid";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicaition_list = new List<applicaition_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr in dt_datatable.Rows)
                {
                    msSQL = " select application_gid from agr_trn_tsuprAppcreditapproval where application_gid='" + dr["application_gid"] + "' and (approval_status='Rejected' or approval_status='Hold')";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {

                        msSQL = " select a.application_gid,application_no,customerref_name as customer_name,customer_urn,vertical_name,a.headapproval_status," +
                                " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,a.approval_status,applicant_type,a.created_by as createdby," +
                                " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,date_format(a.submitted_date,'%d-%m-%Y %h:%i %p') as updated_date,date_format(a.creditheadapproval_date,'%d-%m-%Y %h:%i %p') as creditheadapproval_date, " +
                                " concat(t.user_firstname,' ',t.user_lastname,' / ',t.user_code)  as creditassigned_by,date_format(a.creditassigned_date,'%d-%m-%Y %h:%i %p') as creditassigned_date," +
                                " productcharge_flag, economical_flag,region,overalllimit_amount,creditheadapproval_status from agr_mst_tsuprapplication a" +
                                " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                                " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                                " left join hrm_mst_temployee s on s.employee_gid=a.creditassigned_by " +
                                " left join adm_mst_tuser t on t.user_gid=s.user_gid " +
                                " where  application_gid='" + objODBCDatareader["application_gid"] + "'";

                        objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader1.HasRows == true)
                        {
                            getapplicaition_list.Add(new applicaition_list
                            {
                                application_no = objODBCDatareader1["application_no"].ToString(),
                                customer_name = objODBCDatareader1["customer_name"].ToString(),
                                customer_urn = objODBCDatareader1["customer_urn"].ToString(),
                                vertical_name = objODBCDatareader1["vertical_name"].ToString(),
                                created_date = objODBCDatareader1["created_date"].ToString(),
                                created_by = objODBCDatareader1["created_by"].ToString(),
                                application_gid = objODBCDatareader1["application_gid"].ToString(),
                                economical_flag = objODBCDatareader1["economical_flag"].ToString(),
                                productcharge_flag = objODBCDatareader1["productcharge_flag"].ToString(),
                                application_status = objODBCDatareader1["approval_status"].ToString(),
                                applicant_type = objODBCDatareader1["applicant_type"].ToString(),
                                updated_date = objODBCDatareader1["updated_date"].ToString(),
                                createdby = objODBCDatareader1["createdby"].ToString(),
                                creditheadapproval_status = objODBCDatareader1["creditheadapproval_status"].ToString(),
                                creditheadapproval_date = objODBCDatareader1["creditheadapproval_date"].ToString(),
                                region = objODBCDatareader1["region"].ToString(),
                                overalllimit_amount = objODBCDatareader1["overalllimit_amount"].ToString(),
                                creditassigned_date = objODBCDatareader1["creditassigned_date"].ToString(),
                                creditassigned_by = objODBCDatareader1["creditassigned_by"].ToString()
                            });

                        }
                        objODBCDatareader1.Close();
                    }
                    objODBCDatareader.Close();
                }
            }
            values.applicaition_list = getapplicaition_list;
            dt_datatable.Dispose();

        }

        public void DaGetMyAppcreditRejectHoldSummary(string employee_gid, MdlMstCreditApproval values)
        {
            msSQL = " select application_gid from agr_trn_tsuprAppcreditapproval where approval_gid='" + employee_gid + "'  and hierary_level<>'0' group by application_gid";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicaition_list = new List<applicaition_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr in dt_datatable.Rows)
                {
                    msSQL = " select application_gid from agr_trn_tsuprAppcreditapproval where application_gid='" + dr["application_gid"] + "' and (approval_status='Rejected' or approval_status='Hold')";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        msSQL = " select a.application_gid,application_no,customerref_name as customer_name,customer_urn,vertical_name,a.headapproval_status," +
                                " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,a.approval_status,applicant_type,a.created_by as createdby," +
                                " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,date_format(a.submitted_date,'%d-%m-%Y %h:%i %p') as updated_date,date_format(a.creditheadapproval_date,'%d-%m-%Y %h:%i %p') as creditheadapproval_date, " +
                                " concat(t.user_firstname,' ',t.user_lastname,' / ',t.user_code)  as creditassigned_by,date_format(a.creditassigned_date,'%d-%m-%Y %h:%i %p') as creditassigned_date," +
                                " productcharge_flag, economical_flag,region,overalllimit_amount,creditheadapproval_status from agr_mst_tsuprapplication a" +
                                " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                                " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                                " left join hrm_mst_temployee s on s.employee_gid=a.creditassigned_by " +
                                " left join adm_mst_tuser t on t.user_gid=s.user_gid " +
                                " where  application_gid='" + objODBCDatareader["application_gid"] + "'";

                        objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader1.HasRows == true)
                        {
                            string lssubmitted_by, lssubmitted_date;
                            lssubmitted_by = objdbconn.GetExecuteScalar("select approval_name from agr_trn_tsuprAppcreditapproval  where application_gid ='" + objODBCDatareader1["application_gid"].ToString() + "' and hierary_level = '0'");
                            lssubmitted_date = objdbconn.GetExecuteScalar("select date_format(approved_date,'%d-%m-%Y %h:%i %p') as approved_date  from agr_trn_tsuprAppcreditapproval  where application_gid ='" + objODBCDatareader1["application_gid"].ToString() + "' and hierary_level = '0'");

                            getapplicaition_list.Add(new applicaition_list
                            {
                                application_no = objODBCDatareader1["application_no"].ToString(),
                                customer_name = objODBCDatareader1["customer_name"].ToString(),
                                customer_urn = objODBCDatareader1["customer_urn"].ToString(),
                                vertical_name = objODBCDatareader1["vertical_name"].ToString(),
                                created_date = objODBCDatareader1["created_date"].ToString(),
                                created_by = objODBCDatareader1["created_by"].ToString(),
                                application_gid = objODBCDatareader1["application_gid"].ToString(),
                                economical_flag = objODBCDatareader1["economical_flag"].ToString(),
                                productcharge_flag = objODBCDatareader1["productcharge_flag"].ToString(),
                                application_status = objODBCDatareader1["approval_status"].ToString(),
                                applicant_type = objODBCDatareader1["applicant_type"].ToString(),
                                updated_date = objODBCDatareader1["updated_date"].ToString(),
                                createdby = objODBCDatareader1["createdby"].ToString(),
                                creditheadapproval_status = objODBCDatareader1["creditheadapproval_status"].ToString(),
                                creditheadapproval_date = objODBCDatareader1["creditheadapproval_date"].ToString(),
                                region = objODBCDatareader1["region"].ToString(),
                                overalllimit_amount = objODBCDatareader1["overalllimit_amount"].ToString(),
                                creditassigned_date = objODBCDatareader1["creditassigned_date"].ToString(),
                                creditassigned_by = objODBCDatareader1["creditassigned_by"].ToString(),
                                submitted_by = lssubmitted_by,
                                submitted_date = lssubmitted_date
                            });

                        }
                        objODBCDatareader1.Close();
                    }
                    objODBCDatareader.Close();
                }
            }
            values.applicaition_list = getapplicaition_list;
            dt_datatable.Dispose();

        }

        public void DaGetcreditheadsview(MdlappCreditassign objVisitor, string application_gid)
        {
            try
            {
                msSQL = " select credithead_name,creditnationalmanager_name,creditregionalmanager_name,creditmanager_name,creditgroup_name,remarks " +
                        " from agr_mst_tsuprapplication  where application_gid = '" + application_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {

                    objVisitor.credithead_name = objODBCDatareader["credithead_name"].ToString();
                    objVisitor.nationalcredit_name = objODBCDatareader["creditnationalmanager_name"].ToString();
                    objVisitor.regionalcredit_name = objODBCDatareader["creditregionalmanager_name"].ToString();
                    objVisitor.creditmanager_name = objODBCDatareader["creditmanager_name"].ToString();
                    objVisitor.creditgroup_name = objODBCDatareader["creditgroup_name"].ToString();
                    objVisitor.remarks = objODBCDatareader["remarks"].ToString();

                }
                objODBCDatareader.Close();

                objVisitor.status = true;
            }
            catch
            {
                objVisitor.status = false;
            }
        }


        public void DaGetappcreditapprovalinitiate(string application_gid, string employee_gid, string user_gid, result values)
        {
            try
            {

                msSQL = " update agr_trn_tsuprAppcreditapproval set  approval_status='Submitted to Approval'," +
                            " approved_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                            " where application_gid='" + application_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                int k;
                string lsapproval_gid, lsinitiate_flag, lsapprovalname;
                msSQL = " select credithead_name,creditnationalmanager_name,creditregionalmanager_name,credithead_gid,creditnationalmanager_gid,creditregionalmanager_gid " +
                        "  from agr_mst_tsuprapplication where application_gid = '" + application_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {

                    for (k = 1; k < 4; k++)
                    {
                        char level;
                        level = Convert.ToChar(k);
                        lsapproval_gid = "";
                        lsapprovalname = "";
                        lsinitiate_flag = "";
                        if (level == '\u0001')
                        {
                            lsapproval_gid = objODBCDatareader["creditregionalmanager_gid"].ToString();
                            lsapprovalname = objODBCDatareader["creditregionalmanager_name"].ToString();
                            lsinitiate_flag = "Y";

                        }
                        else if (level == '\u0002')
                        {
                            lsapproval_gid = objODBCDatareader["creditnationalmanager_gid"].ToString();
                            lsapprovalname = objODBCDatareader["creditnationalmanager_name"].ToString();
                            lsinitiate_flag = "N";
                        }
                        else if (level == '\u0003')
                        {
                            lsapproval_gid = objODBCDatareader["credithead_gid"].ToString();
                            lsapprovalname = objODBCDatareader["credithead_name"].ToString();
                            lsinitiate_flag = "N";
                        }

                        string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
                        sToken = "";
                        int Length = 100;
                        for (int j = 0; j < Length; j++)
                        {
                            string sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                            sToken += sTempChars;
                        }

                        msGetGid = objcmnfunctions.GetMasterGID("CRAP");

                        msSQL = "Insert into agr_trn_tsuprAppcreditapproval( " +
                               " appcreditapproval_gid," +
                               " application_gid, " +
                               " approval_gid," +
                               " approval_name," +
                               " approval_type," +
                               " hierary_level," +
                               " approval_token," +
                               " initiate_flag," +
                               " created_by," +
                               " created_date)" +
                               " values(" +
                               "'" + msGetGid + "'," +
                               "'" + application_gid + "'," +
                               "'" + lsapproval_gid + "'," +
                               "'" + lsapprovalname + "'," +
                               "'sequence'," +
                               "'" + k + "'," +
                               "'" + sToken + "'," +
                               "'" + lsinitiate_flag + "'," +
                               "'" + user_gid + "'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    }
                }

                objODBCDatareader.Close();
                if (mnResult != 0)
                {
                    msSQL = "update agr_mst_tsuprapplication set approval_status='Submitted to Credit Approval' " +
                           " where application_gid='" + application_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult != 0)
                    {

                        try
                        {
                            msSQL = "select application_no from agr_mst_tsuprapplication where application_gid='" + application_gid + "'";
                            application_no = objdbconn.GetExecuteScalar(msSQL);

                            msSQL = "select customerref_name from agr_mst_tsuprapplication where application_gid='" + application_gid + "'";
                            customer_name = objdbconn.GetExecuteScalar(msSQL);

                            msSQL = " select creditmanager_name, creditnationalmanager_name, creditregionalmanager_name, credithead_name from agr_mst_tsuprapplication where application_gid = '" + application_gid + "'";
                            objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader1.HasRows == true)
                            {
                                creditmanager_name = objODBCDatareader1["creditmanager_name"].ToString();
                                creditnationalmanager_name = objODBCDatareader1["creditnationalmanager_name"].ToString();
                                creditregionalmanager_name = objODBCDatareader1["creditregionalmanager_name"].ToString();
                                credithead_name = objODBCDatareader1["credithead_name"].ToString();
                            }
                            objODBCDatareader1.Close();

                            msSQL = "select b.employee_emailid from agr_mst_tsuprapplication a left join hrm_mst_temployee b on b.employee_gid = a.creditnationalmanager_gid  where application_gid = '" + application_gid + "'";
                            nationalmanager_mailid = objdbconn.GetExecuteScalar(msSQL);

                            msSQL = "select b.employee_emailid from agr_mst_tsuprapplication a left join hrm_mst_temployee b on b.employee_gid = a.creditregionalmanager_gid  where application_gid = '" + application_gid + "'";
                            tomail_id = objdbconn.GetExecuteScalar(msSQL);

                            msSQL = "select b.employee_emailid from agr_mst_tsuprapplication a left join hrm_mst_temployee b on b.employee_gid = a.credithead_gid  where application_gid = '" + application_gid + "'";
                            credithead_mailid = objdbconn.GetExecuteScalar(msSQL);

                            msSQL = " select b.employee_emailid from agr_mst_tsuprapplication a left join hrm_mst_temployee b on b.employee_gid = a.relationshipmanager_gid where a.application_gid='" + application_gid + "'";
                            relationshipmanager_mailid = objdbconn.GetExecuteScalar(msSQL);

                            msSQL = "select b.employee_emailid from agr_mst_tsuprapplication a left join hrm_mst_temployee b on b.employee_gid = a.creditmanager_gid  where application_gid = '" + application_gid + "'";
                            creditmanager_mailid = objdbconn.GetExecuteScalar(msSQL);

                            msSQL = " SELECT pop_server, pop_port, pop_username, pop_password" +
                                " FROM adm_mst_tcompany";
                            objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader1.HasRows == true)
                            {
                                ls_server = objODBCDatareader1["pop_server"].ToString();
                                ls_port = Convert.ToInt32(objODBCDatareader1["pop_port"]);
                                ls_username = objODBCDatareader1["pop_username"].ToString();
                                ls_password = objODBCDatareader1["pop_password"].ToString();
                            }
                            //lssource = ConfigurationManager.AppSettings["img_path"];
                            objODBCDatareader1.Close();

                            sub = " ARN(" + application_no + ") : Application approval required ";
                            body = "<style>table, th, td {border: 1px solid black;border-collapse: collapse;}</style>";
                            body = body + "<table style='border-right: 1px solid black;border-top: 1px solid black;border-bottom: 1px solid black;'><tr><td style='border-right-color:white;align:center;'>";
                            //body = body + "<br />";
                            //body = body + "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp <img style='height:150px; width:380px;' src='" + lssource + "'><br />";
                            //body = body + "<br />";
                            body = body + " &nbsp&nbsp Hello," + HttpUtility.HtmlEncode(creditregionalmanager_name)+ " <br />";
                            body = body + "<br />";
                            body = body + "&nbsp&nbsp Greetings! Quick Heads-up on the below<br />";
                            body = body + "<br />";
                            body = body + "<table style='margin-left:18px; margin-right:18px;'><tr><th >Group</th><th>ARN</th><th>Customer Name</th><th>Comments</th></tr>";
                            body = body + "<tr><td>Awaiting approval</td><td>" + application_no + "</td><td>" + HttpUtility.HtmlEncode(customer_name)+ "</td><td> Pending L1 Approval </td></tr>";
                            body = body + "<tr><td>Actions on Comments</td><td></td><td></td><td></td></tr>";
                            body = body + "<tr><td>Queries to be addressed</td><td></td><td></td><td></td></tr></table>";
                            body = body + "<br />";
                            body = body + "&nbsp&nbsp Log into one.samunnati.com and complete the necessary actions. <br />";
                            body = body + "<br />";
                            //body = body + "&nbsp&nbsp Have a fantastic day!<br />";
                            //body = body + "<br />";
                            //body = body + "&nbsp&nbsp Thanks";
                            //body = body + "<br />";
                            //body = body + "&nbsp&nbsp Sam-Custopedia <br /> ";
                            //body = body + "<br />";
                            //body = body + "&nbsp&nbsp<hr>&nbsp&nbsp";
                            //body = body + "&nbsp&nbsp Reach out to us at samcustopedia@samunnati.com <br /> ";
                            //body = body + "<br />";
                            body = body + "</td><td style='margin-left:20px; border-left-color:white;'>&nbsp&nbsp</td></tr></style></table>";

                            MailMessage message = new MailMessage();
                            SmtpClient smtp = new SmtpClient();
                            message.From = new MailAddress(ls_username);
                            message.To.Add(new MailAddress(tomail_id));
                            lsBccmail_id = ConfigurationManager.AppSettings["SamagroApprovalBccMail"].ToString();

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

                            cc_mailid = "" + creditmanager_mailid + "," + credithead_mailid + "," + nationalmanager_mailid + "," + relationshipmanager_mailid + "";

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
                            smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                            smtp.Send(message);
                            values.status = true;

                        }
                        catch (Exception ex)
                        {
                            values.message = ex.ToString();
                            values.status = false;
                        }

                    }

                    values.status = true;
                    values.message = "Submitted to Approval Successfully..!";
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occured..!";
                }

            }
            catch (Exception ex)
            {

            }
        }


        public void DaPostAppcreditqueryadd(mdlcreditquery values, string user_gid, string employee_gid)
        {
            msGetGid = objcmnfunctions.GetMasterGID("CRCM");

            if (values.queryraised_to == "RM")
            {
                msSQL = "select appcreditapproval_gid from agr_trn_tsuprAppcreditapproval where application_gid = '" + values.application_gid + "'  and hierary_level = '0'";
                values.appcreditapproval_gid = objdbconn.GetExecuteScalar(msSQL);
            }

            msSQL = " insert into agr_trn_tsuprapplicationcreditquery(" +
                     " appcreditquery_gid," +
                     " appcreditapproval_gid," +
                     " application_gid, " +
                     " query_title," +
                     " query_description," +
                     " queryraised_to," +
                     " created_by," +
                     " created_date)" +
                     " values(" +
                     "'" + msGetGid + "'," +
                     "'" + values.appcreditapproval_gid + "', " +
                     "'" + values.application_gid + "', " +
                     "'" + values.querytitle + "'," +
                     "'" + values.querydesc.Replace("'", "") + "'," +
                     "'" + values.queryraised_to + "'," +
                     "'" + user_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                string lshierarchy = objdbconn.GetExecuteScalar("select hierary_level from agr_trn_tsuprAppcreditapproval  where appcreditapproval_gid='" + values.appcreditapproval_gid + "'");
                if (lshierarchy == "0")
                {
                    msSQL = "update agr_mst_tsuprapplication set  creditheadapproval_status='Comment Raised to RM'" +
                           " where application_gid='" + values.application_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                else
                {
                    msSQL = "update agr_mst_tsuprapplication set  creditheadapproval_status='Comment Raised'" +
                       " where application_gid='" + values.application_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                //Mail Start
                try
                {
                    String lshierarchylevel;
                    lshierarchylevel = objdbconn.GetExecuteScalar("select hierary_level from agr_trn_tsuprapplicationcreditquery a " +
                    "left join agr_trn_tsuprAppcreditapproval b on a.appcreditapproval_gid = b.appcreditapproval_gid where b.appcreditapproval_gid='" + values.appcreditapproval_gid + "'");
                    int level = Convert.ToInt16(lshierarchylevel);
                    char hierlevel = Convert.ToChar(level);

                    msSQL = "select application_no from agr_mst_tsuprapplication where application_gid='" + values.application_gid + "'";
                    application_no = objdbconn.GetExecuteScalar(msSQL);
                    msSQL = "select customerref_name from agr_mst_tsuprapplication where application_gid='" + values.application_gid + "'";
                    customer_name = objdbconn.GetExecuteScalar(msSQL);
                    msSQL = "select b.employee_emailid from agr_mst_tsuprapplication a left join hrm_mst_temployee b on b.employee_gid = a.creditmanager_gid where a.application_gid='" + values.application_gid + "'";
                    creditmanager_mailid = objdbconn.GetExecuteScalar(msSQL);
                    msSQL = " select b.employee_emailid from agr_mst_tsuprapplication a left join hrm_mst_temployee b on b.employee_gid = a.creditregionalmanager_gid where a.application_gid='" + values.application_gid + "'";
                    creditregionalmanager_mailid = objdbconn.GetExecuteScalar(msSQL);
                    msSQL = " select b.employee_emailid from agr_mst_tsuprapplication a left join hrm_mst_temployee b on b.employee_gid = a.creditnationalmanager_gid where a.application_gid='" + values.application_gid + "'";
                    creditnationalmanager_mailid = objdbconn.GetExecuteScalar(msSQL);
                    msSQL = " select b.employee_emailid from agr_mst_tsuprapplication a left join hrm_mst_temployee b on b.employee_gid = a.credithead_gid where a.application_gid='" + values.application_gid + "'";
                    credithead_mailid = objdbconn.GetExecuteScalar(msSQL);
                    msSQL = " select b.employee_emailid from agr_mst_tsuprapplication a left join hrm_mst_temployee b on b.employee_gid = a.created_by where a.application_gid='" + values.application_gid + "'";
                    creater_mailid = objdbconn.GetExecuteScalar(msSQL);


                    msSQL = " select creditmanager_name,creditregionalmanager_name,creditnationalmanager_name,relationshipmanager_name,credithead_name from agr_mst_tsuprapplication where application_gid = '" + values.application_gid + "'";
                    objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader1.HasRows == true)
                    {
                        credithead_name = objODBCDatareader1["credithead_name"].ToString();
                        creditregionalmanager_name = objODBCDatareader1["creditregionalmanager_name"].ToString();
                        creditnationalmanager_name = objODBCDatareader1["creditnationalmanager_name"].ToString();
                        creditmanager_name = objODBCDatareader1["creditmanager_name"].ToString();
                        relationshipmanager_name = objODBCDatareader1["relationshipmanager_name"].ToString();
                    }
                    objODBCDatareader1.Close();


                    msSQL = " SELECT pop_server, pop_port, pop_username, pop_password" +
                                    " FROM adm_mst_tcompany";
                    objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader1.HasRows == true)
                    {
                        ls_server = objODBCDatareader1["pop_server"].ToString();
                        ls_port = Convert.ToInt32(objODBCDatareader1["pop_port"]);
                        ls_username = objODBCDatareader1["pop_username"].ToString();
                        ls_password = objODBCDatareader1["pop_password"].ToString();
                    }
                    objODBCDatareader1.Close();

                    if (hierlevel == '\0')
                    {
                        tomail_id = creater_mailid;
                        cc_mailid = "" + creditmanager_mailid + "," + creditregionalmanager_mailid + "," + creditnationalmanager_mailid + "," + credithead_mailid + "";
                    }
                    else if (hierlevel == '\u0001')
                    {
                        tomail_id = creditmanager_mailid;
                        cc_mailid = "" + creater_mailid + "," + creditregionalmanager_mailid + "," + creditnationalmanager_mailid + "," + credithead_mailid + "";
                    }
                    else if (hierlevel == '\u0002')
                    {
                        tomail_id = creditmanager_mailid;
                        cc_mailid = "" + creater_mailid + "," + creditregionalmanager_mailid + "," + creditnationalmanager_mailid + "," + credithead_mailid + "";
                    }
                    else if (hierlevel == '\u0003')
                    {
                        tomail_id = creditmanager_mailid;
                        cc_mailid = "" + creater_mailid + "," + creditregionalmanager_mailid + "," + creditnationalmanager_mailid + "," + credithead_mailid + "";
                    }

                    msSQL = " select approval_status from agr_trn_tsuprAppcreditapproval  where appcreditapproval_gid ='" + values.appcreditapproval_gid + "'";
                    lsstatus = objdbconn.GetExecuteScalar(msSQL);

                    //lssource = ConfigurationManager.AppSettings["img_path"];
                    sub = " New Query raised on ARN(" + application_no + ")  ";
                    body = "<style>table, th, td {border: 1px solid black;border-collapse: collapse;}</style>";
                    body = body + "<table style='border-right: 1px solid black;border-top: 1px solid black;border-bottom: 1px solid black;'><tr><td style='border-right-color:white;align:center;'>";
                    body = body + "<br />";
                    body = body + "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp <img style='height:150px; width:380px;' src='" + lssource + "'><br />";
                    body = body + "<br />";
                    body = body + " &nbsp &nbsp Dear Sir / Madam, <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp Greetings! <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp New Query has been raised for the below application ";
                    body = body + "<br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp <b> Application Number : </b> " + application_no + "  <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp <b> Customer Name : </b> " + HttpUtility.HtmlEncode(customer_name)+ "  <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp <b> RM Name : </b> " + HttpUtility.HtmlEncode(relationshipmanager_name)+ "  <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp <b> Credit Manager Name : </b> " + HttpUtility.HtmlEncode(creditmanager_name)+ "  <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp <b> Credit Regional Head Name : </b> " + HttpUtility.HtmlEncode(creditregionalmanager_name)+ "  <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp <b> Credit National Head Name : </b> " + HttpUtility.HtmlEncode(creditnationalmanager_name)+ "  <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp <b> Credit Head Name : </b> " + HttpUtility.HtmlEncode(credithead_name)+ "  <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp Log into Sam - Custopedia and complete the necessary actions.";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp Regards";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp Sam-Custopedia <br /> ";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp<hr>&nbsp&nbsp";
                    body = body + "&nbsp &nbsp Reach out to us at samcustopedia@samunnati.com <br /> ";
                    body = body + "<br />";
                    body = body + "</td><td style='margin-left:20px; border-left-color:white;'>&nbsp&nbsp</td></tr></table>";

                    MailMessage message = new MailMessage();
                    SmtpClient smtp = new SmtpClient();
                    message.From = new MailAddress(ls_username);
                    message.To.Add(new MailAddress(tomail_id));
                    lsBccmail_id = ConfigurationManager.AppSettings["SamagroApprovalBccMail"].ToString();

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
                    smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Send(message);
                    values.status = true;
                }
                catch (Exception ex)
                {
                    values.message = ex.ToString();
                }
                //Mail End
                values.status = true;
                values.message = "Query Raised Successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }


        }

        public void DaGetAppcreditApprovalSummary(applcreditapproval values, string application_gid)
        {
            msSQL = " select appcreditapproval_gid,approval_name,approval_status ,b.user_code,a.hierary_level," +
                     " concat(b.user_firstname, ' ', b.user_lastname)  as created_by,approval_remarks,date_format(a.approved_date, '%d-%m-%Y %h:%i %p') as approved_date, " +
                     " date_format(b.created_date, '%d-%m-%Y %h:%i %p') as created_date,date_format(a.hold_date, '%d-%m-%Y %h:%i %p') as hold_date," +
                     " date_format(a.rejected_date, '%d-%m-%Y %h:%i %p') as rejected_date  from agr_trn_tsuprAppcreditapproval a " +
                     " left join adm_mst_tuser b on b.user_gid = a.created_by " +
                     " where a.application_gid='" + application_gid + "'  and a.hierary_level <>'0' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getappcreditapprovallist = new List<appcreditapprovallist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getappcreditapprovallist.Add(new appcreditapprovallist
                    {
                        appcreditapproval_gid = dt["appcreditapproval_gid"].ToString(),
                        approval_name = dt["approval_name"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        approval_remarks = dt["approval_remarks"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        approved_date = dt["approved_date"].ToString(),
                        hold_date = dt["hold_date"].ToString(),
                        rejected_date = dt["rejected_date"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        hierary_level = dt["hierary_level"].ToString(),
                        user_code = dt["user_code"].ToString(),
                    });
                    values.appcreditapprovallist = getappcreditapprovallist;
                }
            }
            dt_datatable.Dispose();


        }

        public void DaGetAppcreditquerySummary(applcreditapproval values, string application_gid)
        {
            msSQL = " select a.application_gid, appcreditquery_gid,a.appcreditapproval_gid,query_title,query_status,query_description,c.approval_name,c.hierary_level,a.close_remarks, " +
                     " concat(b.user_firstname, ' ', b.user_lastname, ' / ', b.user_code) as created_by,date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date from agr_trn_tsuprapplicationcreditquery a" +
                     " left join adm_mst_tuser b on b.user_gid = a.created_by " +
                     " left join agr_trn_tsuprAppcreditapproval c on a.appcreditapproval_gid=c.appcreditapproval_gid " +
                     " where a.application_gid='" + application_gid + "' and c.hierary_level <>'0' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getappcreditquerylist = new List<appcreditquerylist>();
            if (dt_datatable.Rows.Count != 0)
            {
                string lshierarchy = objdbconn.GetExecuteScalar("select approval_name from agr_trn_tsuprAppcreditapproval where application_gid='" + application_gid + "' and hierary_level ='0'");
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msSQL = " select application_gid from agr_trn_tsuprapplicationcreditquery where query_status = 'Open' and application_gid = '" + dt["application_gid"].ToString() + "' " +
                           " and queryraised_to = 'RM'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == false)
                    {
                        lsrmquery_flag = "Y";
                    }
                    else
                    {
                        lsrmquery_flag = "N";
                    }
                    objODBCDatareader.Close();

                    getappcreditquerylist.Add(new appcreditquerylist
                    {
                        appcreditapproval_gid = dt["appcreditapproval_gid"].ToString(),
                        appcreditquery_gid = dt["appcreditquery_gid"].ToString(),
                        querytitle = dt["query_title"].ToString(),
                        querystatus = dt["query_status"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        querydesc = dt["query_description"].ToString(),
                        hierary_level = dt["hierary_level"].ToString(),
                        close_remarks = dt["close_remarks"].ToString(),
                        query_to = lshierarchy,
                        rmquery_flag = lsrmquery_flag,
                    });
                    values.appcreditquerylist = getappcreditquerylist;
                }
            }
            dt_datatable.Dispose();


        }

        public void DaGetApprmquerysSummary(applcreditapproval values, string application_gid)
        {
            msSQL = " select appcreditquery_gid,a.appcreditapproval_gid,query_title,query_status,query_description,c.approval_name,c.hierary_level,a.close_remarks, " +
                     " concat(b.user_firstname, ' ', b.user_lastname, ' / ', b.user_code) as created_by,date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date from agr_trn_tsuprapplicationcreditquery a" +
                     " left join adm_mst_tuser b on b.user_gid = a.created_by " +
                     " left join agr_trn_tsuprAppcreditapproval c on a.appcreditapproval_gid=c.appcreditapproval_gid " +
                     " where a.application_gid='" + application_gid + "' and c.hierary_level = '0' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getappcreditquerylist = new List<appcreditquerylist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getappcreditquerylist.Add(new appcreditquerylist
                    {
                        appcreditapproval_gid = dt["appcreditapproval_gid"].ToString(),
                        appcreditquery_gid = dt["appcreditquery_gid"].ToString(),
                        querytitle = dt["query_title"].ToString(),
                        querystatus = dt["query_status"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        querydesc = dt["query_description"].ToString(),
                        hierary_level = dt["hierary_level"].ToString(),
                        close_remarks = dt["close_remarks"].ToString()
                    });
                    values.appcreditquerylist = getappcreditquerylist;
                }
            }
            dt_datatable.Dispose();


        }

        public void DaGetGetAppcreditqueryesc(mdlcreditquery values, string appcreditquery_gid)
        {
            msSQL = "select query_description from agr_trn_tsuprapplicationcreditquery where appcreditquery_gid='" + appcreditquery_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.querydesc = objODBCDatareader["query_description"].ToString();
            }
            objODBCDatareader.Close();
        }

        public void DaPostAppcreditHeadApproval(mdlappcreditapproval values, string user_gid, string employee_gid)
        {
            String lshierarchylevel;
            lshierarchylevel = objdbconn.GetExecuteScalar("select hierary_level from agr_trn_tsuprAppcreditapproval where appcreditapproval_gid='" + values.appcreditapproval_gid + "'");
            Char hierarchylevel;
            int level = Convert.ToInt16(lshierarchylevel);
            int nextlevel = level + 1;
            char nexthierlevel = Convert.ToChar(nextlevel);
            hierarchylevel = Convert.ToChar(level);

            if (values.approval_status == "Approved" && hierarchylevel != '\u0003')
            {
                msSQL = " update agr_trn_tsuprAppcreditapproval set  approval_status='Approved',approval_remarks='" + values.approval_remarks + "'," +
                        " approved_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                        " where appcreditapproval_gid='" + values.appcreditapproval_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                msSQL = "update agr_mst_tsuprapplication set  creditheadapproval_status='L" + lshierarchylevel + " - Approved',creditheadapproval_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                      " where application_gid='" + values.application_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                msSQL = " select appcreditapproval_gid,hierary_level from agr_trn_tsuprAppcreditapproval " +
                    " where application_gid='" + values.application_gid + "' and approval_gid='" + employee_gid + "' and " +
                    " appcreditapproval_gid <>'" + values.appcreditapproval_gid + "' and hierary_level = '" + nextlevel + "' order by hierary_level asc";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == false)
                {
                    // sequence Approval
                    try
                    {
                        msSQL = "select application_no from agr_mst_tsuprapplication where application_gid='" + values.application_gid + "'";
                        application_no = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = "select customerref_name from agr_mst_tsuprapplication where application_gid='" + values.application_gid + "'";
                        customer_name = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = " select creditmanager_name, creditnationalmanager_name, creditregionalmanager_name, credithead_name from agr_mst_tsuprapplication where application_gid = '" + values.application_gid + "'";
                        objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader1.HasRows == true)
                        {
                            creditmanager_name = objODBCDatareader1["creditmanager_name"].ToString();
                            creditnationalmanager_name = objODBCDatareader1["creditnationalmanager_name"].ToString();
                            creditregionalmanager_name = objODBCDatareader1["creditregionalmanager_name"].ToString();
                            credithead_name = objODBCDatareader1["credithead_name"].ToString();
                        }
                        objODBCDatareader1.Close();
                        msSQL = " select approval_gid,approval_name from agr_trn_tsuprAppcreditapproval where application_gid = '" + values.application_gid + "' and hierary_level ='1'";
                        objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader1.HasRows == true)
                        {
                            reportingto_gid = objODBCDatareader1["approval_gid"].ToString();
                            reportingto_name = objODBCDatareader1["approval_name"].ToString();
                        }
                        objODBCDatareader1.Close();

                        msSQL = "select b.employee_emailid from agr_mst_tsuprapplication a left join hrm_mst_temployee b on b.employee_gid = a.creditnationalmanager_gid  where application_gid = '" + values.application_gid + "'";
                        nationalmanager_mailid = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = "select b.employee_emailid from agr_mst_tsuprapplication a left join hrm_mst_temployee b on b.employee_gid = a.creditregionalmanager_gid  where application_gid = '" + values.application_gid + "'";
                        regionalmanager_mailid = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = "select b.employee_emailid from agr_mst_tsuprapplication a left join hrm_mst_temployee b on b.employee_gid = a.credithead_gid  where application_gid = '" + values.application_gid + "'";
                        credithead_mailid = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = " select b.employee_emailid from agr_mst_tsuprapplication a left join hrm_mst_temployee b on b.employee_gid = a.relationshipmanager_gid where a.application_gid='" + values.application_gid + "'";
                        relationshipmanager_mailid = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = "select b.employee_emailid from agr_mst_tsuprapplication a left join hrm_mst_temployee b on b.employee_gid = a.creditmanager_gid  where application_gid = '" + values.application_gid + "'";
                        creditmanager_mailid = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = " select b.employee_emailid from agr_trn_tsuprAppcreditapproval a left join hrm_mst_temployee b on b.employee_gid = a.approval_gid where a.application_gid='" + values.application_gid + "'  and hierary_level ='2'";
                        reportingto_mailid = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = " select concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by  from agr_mst_tsuprapplication a" +
                                " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                                " left join adm_mst_tuser c on c.user_gid=b.user_gid  where a.created_by = '" + values.created_by + "'";
                        creater_name = objdbconn.GetExecuteScalar(msSQL);

                        if (nexthierlevel == '\u0003')
                        {
                            tomail_id = credithead_mailid;
                            cc_mailid = "" + creditmanager_mailid + "," + regionalmanager_mailid + "," + nationalmanager_mailid + "," + relationshipmanager_mailid + "";
                            content = "Pending L3 Approval";
                            head_name = credithead_name;
                        }
                        else if (nexthierlevel == '\u0002')
                        {
                            tomail_id = nationalmanager_mailid;
                            cc_mailid = "" + creditmanager_mailid + "," + regionalmanager_mailid + "," + credithead_mailid + "," + relationshipmanager_mailid + "";
                            content = "Pending L2 Approval";
                            head_name = creditnationalmanager_name;
                        }

                        msSQL = " SELECT pop_server, pop_port, pop_username, pop_password" +
                            " FROM adm_mst_tcompany";
                        objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader1.HasRows == true)
                        {
                            ls_server = objODBCDatareader1["pop_server"].ToString();
                            ls_port = Convert.ToInt32(objODBCDatareader1["pop_port"]);
                            ls_username = objODBCDatareader1["pop_username"].ToString();
                            ls_password = objODBCDatareader1["pop_password"].ToString();
                        }
                        //lssource = ConfigurationManager.AppSettings["img_path"];
                        objODBCDatareader1.Close();
                        if (nexthierlevel == '\u0004')
                        {

                        }
                        else
                        {
                            sub = " ARN(" + application_no + ") : Application approval required ";
                            body = "<style>table, th, td {border: 1px solid black;border-collapse: collapse;}</style>";
                            body = body + "<table style='border-right: 1px solid black;border-top: 1px solid black;border-bottom: 1px solid black;'><tr><td style='border-right-color:white;align:center;'>";
                            //body = body + "<br />";
                            //body = body + "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp <img style='height:150px; width:380px;' src='" + lssource + "'><br />";
                            //body = body + "<br />";
                            body = body + " &nbsp&nbsp Hello," + HttpUtility.HtmlEncode(head_name)+ " <br />";
                            body = body + "<br />";
                            body = body + "&nbsp&nbsp Greetings! Quick Heads-up on the below<br />";
                            body = body + "<br />";
                            body = body + "<table style='margin-left:18px; margin-right:18px;'><tr><th >Group</th><th>ARN</th><th>Customer Name</th><th>Comments</th></tr>";
                            body = body + "<tr><td>Awaiting approval</td><td>" + application_no + "</td><td>" + HttpUtility.HtmlEncode(customer_name)+ "</td><td>" + HttpUtility.HtmlEncode(content) + " </td></tr>";
                            body = body + "<tr><td>Actions on Comments</td><td></td><td></td><td></td></tr>";
                            body = body + "<tr><td>Queries to be addressed</td><td></td><td></td><td></td></tr></table>";
                            body = body + "<br />";
                            body = body + "&nbsp&nbsp Log into one.samunnati.com and complete the necessary actions. <br />";
                            body = body + "<br />";
                            //body = body + "&nbsp&nbsp Have a fantastic day!<br />";
                            //body = body + "<br />";
                            //body = body + "&nbsp&nbsp Thanks";
                            //body = body + "<br />";
                            //body = body + "&nbsp&nbsp Sam-Custopedia <br /> ";
                            //body = body + "<br />";
                            //body = body + "&nbsp&nbsp<hr>&nbsp&nbsp";
                            //body = body + "&nbsp&nbsp Reach out to us at samcustopedia@samunnati.com <br /> ";
                            //body = body + "<br />";
                            body = body + "</td><td style='margin-left:20px; border-left-color:white;'>&nbsp&nbsp</td></tr></table>";

                            MailMessage message = new MailMessage();
                            SmtpClient smtp = new SmtpClient();
                            message.From = new MailAddress(ls_username);
                            message.To.Add(new MailAddress(tomail_id));
                            lsBccmail_id = ConfigurationManager.AppSettings["SamagroApprovalBccMail"].ToString();

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
                            smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                            smtp.Send(message);
                            values.status = true;
                        }

                    }
                    catch (Exception ex)
                    {
                        values.message = ex.ToString();
                        values.status = false;
                    }
                }
                objODBCDatareader.Close();
            }
            else if (values.approval_status == "Hold")
            {
                msSQL = " update agr_trn_tsuprAppcreditapproval set  approval_status='Hold',approval_remarks='" + values.approval_remarks + "'," +
                       " hold_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                       " where appcreditapproval_gid='" + values.appcreditapproval_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                msSQL = "update agr_mst_tsuprapplication set approval_status='Hold By Credit',  creditheadapproval_status='L" + lshierarchylevel + " - Hold',creditheadapproval_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where application_gid='" + values.application_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                msSQL = "update agr_trn_tsuprAppcreditapproval set  approval_status='-' " +
                        " where application_gid='" + values.application_gid + "' and appcreditapproval_gid > '" + values.appcreditapproval_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            }
            else if (values.approval_status == "Rejected")
            {
                msSQL = " update agr_trn_tsuprAppcreditapproval set  approval_status='Rejected',approval_remarks='" + values.approval_remarks + "'," +
                        " rejected_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                        " where appcreditapproval_gid='" + values.appcreditapproval_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                msSQL = "update agr_mst_tsuprapplication set approval_status='Rejected By Credit',  creditheadapproval_status='L" + lshierarchylevel + " - Rejected',creditheadapproval_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                       " where application_gid='" + values.application_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                msSQL = "update agr_trn_tsuprAppcreditapproval set  approval_status='-' " +
                      " where application_gid='" + values.application_gid + "' and appcreditapproval_gid > '" + values.appcreditapproval_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }


            else if (hierarchylevel == '\u0003' && values.approval_status == "Approved")
            {
                msSQL = " update agr_trn_tsuprAppcreditapproval set  approval_status='Approved',approval_remarks='" + values.approval_remarks + "'," +
                 " approved_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where appcreditapproval_gid='" + values.appcreditapproval_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsuprapplication set  approval_flag='Y', creditheadapproval_status='All Heads Approved'," +
                       " ccsubmit_flag='Y', approval_status='Submitted to CC'" +
                       " where application_gid='" + values.application_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    // Master to Transaction Document Checklist - Covenant Type Freezing
                    msSQL = " update agr_trn_tsuprdocumentchecktls A " +
                            " left join ocs_mst_tcompanydocument B on A.companydocument_gid = B.companydocument_gid " +
                            " set A.covenant_type = B.covenant_type " +
                            " where a.application_gid = '" + values.application_gid + "' and b.companydocument_gid is not null";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update agr_trn_tsuprdocumentchecktls A " +
                            " left join ocs_mst_tindividualdocument B on A.individualdocument_gid = B.individualdocument_gid " +
                            " set A.covenant_type = B.covenant_type " +
                            " where a.application_gid = '" + values.application_gid + "' and b.individualdocument_gid is not null";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update agr_trn_tsuprdocumentchecktls A " +
                           " left join ocs_mst_tgroupdocument B on A.groupdocument_gid = B.groupdocument_gid " +
                           " set A.covenant_type = B.covenant_type " +
                           " where a.application_gid = '" + values.application_gid + "' and b.groupdocument_gid is not null";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update agr_trn_tsuprcovanantdocumentcheckdtls A " +
                           " left join ocs_mst_tcompanydocument B on A.companydocument_gid = B.companydocument_gid " +
                           " set A.covenant_type = B.covenant_type " +
                           " where a.application_gid = '" + values.application_gid + "' and b.companydocument_gid is not null";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update agr_trn_tsuprcovanantdocumentcheckdtls A " +
                            " left join ocs_mst_tindividualdocument B on A.individualdocument_gid = B.individualdocument_gid " +
                            " set A.covenant_type = B.covenant_type " +
                            " where a.application_gid = '" + values.application_gid + "' and b.individualdocument_gid is not null";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update agr_trn_tsuprcovanantdocumentcheckdtls A " +
                           " left join ocs_mst_tgroupdocument B on A.groupdocument_gid = B.groupdocument_gid " +
                           " set A.covenant_type = B.covenant_type " +
                           " where a.application_gid = '" + values.application_gid + "' and b.groupdocument_gid is not null";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update agr_trn_tsuprgroupdocumentchecklist A " +
                           " left join ocs_mst_tcompanydocument B on A.mstdocument_gid = B.companydocument_gid " +
                           " set A.mstcovenant_type = B.covenant_type " +
                           " where application_gid = '" + values.application_gid + "' and mstdocument_gid is not null";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update agr_trn_tsuprgroupdocumentchecklist A " +
                          " left join ocs_mst_tindividualdocument B on A.mstdocument_gid = B.individualdocument_gid " +
                          " set A.mstcovenant_type = B.covenant_type " +
                          " where application_gid = '" + values.application_gid + "' and mstdocument_gid is not null";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update agr_trn_tsuprgroupdocumentchecklist A " +
                          " left join ocs_mst_tgroupdocument B on A.mstdocument_gid = B.groupdocument_gid " +
                          " set A.mstcovenant_type = B.covenant_type " +
                          " where application_gid = '" + values.application_gid + "' and mstdocument_gid is not null";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update agr_trn_tsuprgroupcovenantdocumentchecklist A " +
                          " left join ocs_mst_tcompanydocument B on A.mstdocument_gid = B.companydocument_gid " +
                          " set A.mstcovenant_type = B.covenant_type " +
                          " where application_gid = '" + values.application_gid + "' and mstdocument_gid is not null";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update agr_trn_tsuprgroupcovenantdocumentchecklist A " +
                          " left join ocs_mst_tindividualdocument B on A.mstdocument_gid = B.individualdocument_gid " +
                          " set A.mstcovenant_type = B.covenant_type " +
                          " where application_gid = '" + values.application_gid + "' and mstdocument_gid is not null";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update agr_trn_tsuprgroupcovenantdocumentchecklist A " +
                          " left join ocs_mst_tgroupdocument B on A.mstdocument_gid = B.groupdocument_gid " +
                          " set A.mstcovenant_type = B.covenant_type " +
                          " where application_gid = '" + values.application_gid + "' and mstdocument_gid is not null";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    try
                    {
                        msSQL = "select application_no from agr_mst_tsuprapplication where application_gid='" + values.application_gid + "'";
                        application_no = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = "select customerref_name from agr_mst_tsuprapplication where application_gid='" + values.application_gid + "'";
                        customer_name = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = " select creditmanager_name, creditnationalmanager_name, creditregionalmanager_name, credithead_name from agr_mst_tsuprapplication where application_gid = '" + values.application_gid + "'";
                        objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader1.HasRows == true)
                        {
                            creditmanager_name = objODBCDatareader1["creditmanager_name"].ToString();
                            creditnationalmanager_name = objODBCDatareader1["creditnationalmanager_name"].ToString();
                            creditregionalmanager_name = objODBCDatareader1["creditregionalmanager_name"].ToString();
                            credithead_name = objODBCDatareader1["credithead_name"].ToString();
                        }
                        objODBCDatareader1.Close();
                        msSQL = " select approval_gid,approval_name from agr_trn_tsuprAppcreditapproval where application_gid = '" + values.application_gid + "' and hierary_level ='1'";
                        objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader1.HasRows == true)
                        {
                            reportingto_gid = objODBCDatareader1["approval_gid"].ToString();
                            reportingto_name = objODBCDatareader1["approval_name"].ToString();
                        }
                        objODBCDatareader1.Close();

                        msSQL = " select b.employee_emailid from agr_mst_tsuprapplication a left join hrm_mst_temployee b on b.employee_gid = a.relationshipmanager_gid where a.application_gid='" + values.application_gid + "'";
                        tomail_id = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = "select b.employee_emailid from agr_mst_tsuprapplication a left join hrm_mst_temployee b on b.employee_gid = a.creditmanager_gid  where application_gid = '" + values.application_gid + "'";
                        creditmanager_mailid = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = "select b.employee_emailid from agr_mst_tsuprapplication a left join hrm_mst_temployee b on b.employee_gid = a.clustermanager_gid where application_gid='" + values.application_gid + "'";
                        cluster_head_mailid = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = " select b.employee_emailid from agr_mst_tsuprapplication a left join hrm_mst_temployee b on b.employee_gid = a.zonalhead_gid where a.application_gid='" + values.application_gid + "'";
                        zonalhead_mailid = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = " select b.employee_emailid from agr_mst_tsuprapplication a left join hrm_mst_temployee b on b.employee_gid = a.regionalhead_gid where a.application_gid='" + values.application_gid + "'";
                        regional_head_mailid = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = " select b.employee_emailid from agr_mst_tsuprapplication a left join hrm_mst_temployee b on b.employee_gid = a.businesshead_gid where a.application_gid='" + values.application_gid + "'";
                        business_head_mailid = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = " select b.employee_emailid from agr_trn_tapplicationapproval a left join hrm_mst_temployee b on b.employee_gid = a.approval_gid where a.application_gid='" + values.application_gid + "'  and hierary_level ='1'";
                        reportingto_mailid = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = " SELECT pop_server, pop_port, pop_username, pop_password" +
                                " FROM adm_mst_tcompany";
                        objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader1.HasRows == true)
                        {
                            ls_server = objODBCDatareader1["pop_server"].ToString();
                            ls_port = Convert.ToInt32(objODBCDatareader1["pop_port"]);
                            ls_username = objODBCDatareader1["pop_username"].ToString();
                            ls_password = objODBCDatareader1["pop_password"].ToString();
                        }
                        //lssource = ConfigurationManager.AppSettings["img_path"];
                        objODBCDatareader1.Close();
                        sub = " Application allocation : " + application_no + " ";
                        body = "<style>table, th, td {border: 1px solid black;border-collapse: collapse;}</style>";
                        body = body + "<table style='border-right: 1px solid black;border-top: 1px solid black;border-bottom: 1px solid black;'><tr><td style='border-right-color:white;align:center;'>";
                        //body = body + "<br />";
                        //body = body + "&nbsp&nbsp <img style='height:150px; width:380px;' src='" + lssource + "'><br />";
                        //body = body + "<br />";
                        body = body + " &nbsp &nbsp Dear Sir / Madam, <br />";
                        body = body + "<br />";
                        body = body + "&nbsp &nbsp Greetings! <br />";
                        body = body + "<br />";
                        body = body + "&nbsp &nbsp The below application has been approved to CC. <br />";
                        body = body + "<br />";
                        body = body + "&nbsp &nbsp <b> Application Number : </b> " + application_no + "  <br />";
                        body = body + "<br />";
                        body = body + "&nbsp &nbsp <b> Customer Name : </b> " + HttpUtility.HtmlEncode(customer_name)+ "  <br />";
                        body = body + "<br />";
                        //body = body + "&nbsp &nbsp Regards";
                        //body = body + "<br />";
                        //body = body + "&nbsp &nbsp Sam-Custopedia <br /> ";
                        //body = body + "<br />";
                        //body = body + "&nbsp &nbsp<hr>&nbsp&nbsp";
                        //body = body + "&nbsp &nbsp Reach out to us at samcustopedia@samunnati.com <br /> ";
                        //body = body + "<br />";
                        body = body + "</td><td style='margin-left:20px; border-left-color:white;'>&nbsp&nbsp</td></tr></table>";


                        cc_mailid = "";
                        MailMessage message = new MailMessage();
                        SmtpClient smtp = new SmtpClient();
                        message.From = new MailAddress(ls_username);
                        message.To.Add(new MailAddress(tomail_id));
                        lsBccmail_id = ConfigurationManager.AppSettings["SamagroApprovalBccMail"].ToString();

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

                        cc_mailid = "" + creditmanager_mailid + "," + cluster_head_mailid + "," + zonalhead_mailid + "," + regional_head_mailid + "," + business_head_mailid + "," + reportingto_mailid + "";

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
                        smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.Send(message);
                        values.status = true;
                    }
                    catch (Exception ex)
                    {
                        values.message = ex.ToString();
                        values.status = false;

                    }
                }
            }

            if (nexthierlevel == '\u0004')
            {

            }
            else
            {
                if (values.approval_status == "Approved")
                {
                    msSQL = " select appcreditapproval_gid,hierary_level from agr_trn_tsuprAppcreditapproval " +
                            " where application_gid='" + values.application_gid + "' and " +
                            " approval_gid='" + employee_gid + "' and appcreditapproval_gid >'" + values.appcreditapproval_gid + "' and hierary_level<>'0' order by hierary_level asc";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable.Rows.Count != 0)
                    {
                        int levelcheck = level;
                        char charlevel = Convert.ToChar(levelcheck);
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            levelcheck = levelcheck + 1;
                            string lcheck = dt["hierary_level"].ToString();
                            int nowlevel = Convert.ToInt16(lcheck);
                            charlevel = Convert.ToChar(levelcheck);
                            if (levelcheck == nowlevel)
                            {
                                msSQL = " update agr_trn_tsuprAppcreditapproval set  approval_status='Approved',approval_remarks='" + values.approval_remarks + "',initiate_flag='Y'," +
                                   " approved_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                                   " where appcreditapproval_gid='" + dt["appcreditapproval_gid"] + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                msSQL = "update agr_mst_tsuprapplication set  creditheadapproval_status='L" + nowlevel + " - Approved',creditheadapproval_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                                        " where application_gid='" + values.application_gid + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                string lslevel;
                                char hierarchy;
                                lslevel = dt["hierary_level"].ToString();
                                int lshierlevel = Convert.ToInt16(lslevel);
                                hierarchy = Convert.ToChar(lshierlevel);
                                if (hierarchy == '\u0003')
                                {

                                    msSQL = "select appcreditapproval_gid from agr_trn_tsuprAppcreditapproval where application_gid='" + values.application_gid + "' and approval_status='Pending' ";
                                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                    if (objODBCDatareader.HasRows == false)
                                    {
                                        msSQL = "update agr_mst_tsuprapplication set  approval_flag='Y', creditheadapproval_status='All Heads Approved'," +
                                                " ccsubmit_flag='Y', approval_status='Submitted to CC'" +
                                                " where application_gid='" + values.application_gid + "'";
                                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                    }
                                    objODBCDatareader.Close();
                                    if (mnResult != 0)
                                    {
                                        try
                                        {
                                            msSQL = "select application_no from agr_mst_tsuprapplication where application_gid='" + values.application_gid + "'";
                                            application_no = objdbconn.GetExecuteScalar(msSQL);

                                            msSQL = "select customerref_name from agr_mst_tsuprapplication where application_gid='" + values.application_gid + "'";
                                            customer_name = objdbconn.GetExecuteScalar(msSQL);

                                            msSQL = " select creditmanager_name, creditnationalmanager_name, creditregionalmanager_name, credithead_name from agr_mst_tsuprapplication where application_gid = '" + values.application_gid + "'";
                                            objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                                            if (objODBCDatareader1.HasRows == true)
                                            {
                                                creditmanager_name = objODBCDatareader1["creditmanager_name"].ToString();
                                                creditnationalmanager_name = objODBCDatareader1["creditnationalmanager_name"].ToString();
                                                creditregionalmanager_name = objODBCDatareader1["creditregionalmanager_name"].ToString();
                                                credithead_name = objODBCDatareader1["credithead_name"].ToString();
                                            }
                                            objODBCDatareader1.Close();
                                            msSQL = " select approval_gid,approval_name from agr_trn_tsuprAppcreditapproval where application_gid = '" + values.application_gid + "' and hierary_level ='1'";
                                            objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                                            if (objODBCDatareader1.HasRows == true)
                                            {
                                                reportingto_gid = objODBCDatareader1["approval_gid"].ToString();
                                                reportingto_name = objODBCDatareader1["approval_name"].ToString();
                                            }
                                            objODBCDatareader1.Close();

                                            msSQL = " select b.employee_emailid from agr_mst_tsuprapplication a left join hrm_mst_temployee b on b.employee_gid = a.relationshipmanager_gid where a.application_gid='" + values.application_gid + "'";
                                            tomail_id = objdbconn.GetExecuteScalar(msSQL);

                                            msSQL = "select b.employee_emailid from agr_mst_tsuprapplication a left join hrm_mst_temployee b on b.employee_gid = a.creditmanager_gid  where application_gid = '" + values.application_gid + "'";
                                            creditmanager_mailid = objdbconn.GetExecuteScalar(msSQL);

                                            msSQL = "select b.employee_emailid from agr_mst_tsuprapplication a left join hrm_mst_temployee b on b.employee_gid = a.clustermanager_gid where application_gid='" + values.application_gid + "'";
                                            cluster_head_mailid = objdbconn.GetExecuteScalar(msSQL);

                                            msSQL = " select b.employee_emailid from agr_mst_tsuprapplication a left join hrm_mst_temployee b on b.employee_gid = a.zonalhead_gid where a.application_gid='" + values.application_gid + "'";
                                            zonalhead_mailid = objdbconn.GetExecuteScalar(msSQL);

                                            msSQL = " select b.employee_emailid from agr_mst_tsuprapplication a left join hrm_mst_temployee b on b.employee_gid = a.regionalhead_gid where a.application_gid='" + values.application_gid + "'";
                                            regional_head_mailid = objdbconn.GetExecuteScalar(msSQL);

                                            msSQL = " select b.employee_emailid from agr_mst_tsuprapplication a left join hrm_mst_temployee b on b.employee_gid = a.businesshead_gid where a.application_gid='" + values.application_gid + "'";
                                            business_head_mailid = objdbconn.GetExecuteScalar(msSQL);

                                            msSQL = " select b.employee_emailid from agr_trn_tapplicationapproval a left join hrm_mst_temployee b on b.employee_gid = a.approval_gid where a.application_gid='" + values.application_gid + "'  and hierary_level ='1'";
                                            reportingto_mailid = objdbconn.GetExecuteScalar(msSQL);

                                            msSQL = " SELECT pop_server, pop_port, pop_username, pop_password" +
                                                    " FROM adm_mst_tcompany";
                                            objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                                            if (objODBCDatareader1.HasRows == true)
                                            {
                                                ls_server = objODBCDatareader1["pop_server"].ToString();
                                                ls_port = Convert.ToInt32(objODBCDatareader1["pop_port"]);
                                                ls_username = objODBCDatareader1["pop_username"].ToString();
                                                ls_password = objODBCDatareader1["pop_password"].ToString();
                                            }
                                            //lssource = ConfigurationManager.AppSettings["img_path"];
                                            objODBCDatareader1.Close();
                                            sub = " Application allocation : " + application_no + " ";
                                            body = "<style>table, th, td {border: 1px solid black;border-collapse: collapse;}</style>";
                                            body = body + "<table style='border-right: 1px solid black;border-top: 1px solid black;border-bottom: 1px solid black;'><tr><td style='border-right-color:white;align:center;'>";
                                            body = body + "<br />";
                                            body = body + "&nbsp&nbsp <img style='height:150px; width:380px;' src='" + lssource + "'><br />";
                                            body = body + "<br />";
                                            body = body + " &nbsp &nbsp Dear Sir / Madam, <br />";
                                            body = body + "<br />";
                                            body = body + "&nbsp &nbsp Greetings! <br />";
                                            body = body + "<br />";
                                            body = body + "&nbsp &nbsp The below application has been approved to CC. <br />";
                                            body = body + "<br />";
                                            body = body + "&nbsp &nbsp <b> Application Number : </b> " + application_no + "  <br />";
                                            body = body + "<br />";
                                            body = body + "&nbsp &nbsp <b> Customer Name : </b> " + HttpUtility.HtmlEncode(customer_name)+ "  <br />";
                                            body = body + "<br />";
                                            body = body + "&nbsp &nbsp Log into " + ConfigurationManager.AppSettings["livedomain_url"].ToString() + " and complete the necessary actions. ";
                                            //body = body + "&nbsp &nbsp Regards";
                                            //body = body + "<br />";
                                            //body = body + "&nbsp &nbsp Sam-Custopedia <br /> ";
                                            //body = body + "<br />";
                                            //body = body + "&nbsp &nbsp<hr>&nbsp&nbsp";
                                            //body = body + "&nbsp &nbsp Reach out to us at samcustopedia@samunnati.com <br /> ";
                                            body = body + "<br />";
                                            body = body + "</td><td style='margin-left:20px; border-left-color:white;'>&nbsp&nbsp</td></tr></table>";


                                            cc_mailid = "";
                                            MailMessage message = new MailMessage();
                                            SmtpClient smtp = new SmtpClient();
                                            message.From = new MailAddress(ls_username);
                                            message.To.Add(new MailAddress(tomail_id));
                                            lsBccmail_id = ConfigurationManager.AppSettings["SamagroApprovalBccMail"].ToString();

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

                                            cc_mailid = "" + creditmanager_mailid + "," + cluster_head_mailid + "," + zonalhead_mailid + "," + regional_head_mailid + "," + business_head_mailid + "," + reportingto_mailid + "";

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
                                            smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                                            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                                            smtp.Send(message);
                                            values.status = true;
                                        }
                                        catch (Exception ex)
                                        {
                                            values.message = ex.ToString();
                                            values.status = false;

                                        }
                                    }

                                }

                                msSQL = "select appcreditapproval_gid from agr_trn_tsuprAppcreditapproval where application_gid='" + values.application_gid + "' and ( hierary_level <'" + dt["hierary_level"].ToString() + "' or hierary_level >'" + dt["hierary_level"].ToString() + "')  and initiate_flag='N' and approval_status='Pending'  order by hierary_level asc";
                                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                if (objODBCDatareader.HasRows == true)
                                {
                                    msSQL = "update agr_trn_tsuprAppcreditapproval set initiate_flag='Y'" +
                                           " where appcreditapproval_gid='" + objODBCDatareader["appcreditapproval_gid"].ToString() + "'";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                }
                                objODBCDatareader.Close();
                            }
                            else
                            {
                                msSQL = " select appcreditapproval_gid,hierary_level from agr_trn_tsuprAppcreditapproval " +
                                       " where application_gid='" + values.application_gid + "' and approval_gid='" + employee_gid + "' and " +
                                       " appcreditapproval_gid <>'" + values.appcreditapproval_gid + "' and hierary_level = '" + nextlevel + "' order by hierary_level asc";
                                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                if (objODBCDatareader.HasRows == false)
                                {
                                    objODBCDatareader.Close();

                                    msSQL = "select appcreditapproval_gid from agr_trn_tsuprAppcreditapproval " +
                                        "where  application_gid='" + values.application_gid + "' and hierary_level >'" + level + "' and initiate_flag='N' order by hierary_level asc";
                                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                    if (objODBCDatareader.HasRows == true)
                                    {

                                        msSQL = "update agr_trn_tsuprAppcreditapproval set initiate_flag='Y'" +
                                            " where appcreditapproval_gid='" + objODBCDatareader["appcreditapproval_gid"] + "'";
                                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                    }
                                    objODBCDatareader.Close();
                                    dt_datatable.Dispose();
                                    values.status = true;
                                    values.message = "Application " + values.approval_status + " Successfully..!";
                                }
                                else
                                {
                                    objODBCDatareader.Close();
                                }


                            }
                            msSQL = " select approval_gid from agr_trn_tsuprAppcreditapproval where application_gid = '" + values.application_gid + "' and hierary_level ='" + levelcheck + "' and approval_status='Approved'";
                            objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader1.HasRows == false)
                            {
                                int llevel = levelcheck - 1;
                                charlevel = Convert.ToChar(llevel);
                            }
                            objODBCDatareader1.Close();
                            if (charlevel == '\u0004')
                            {
                                int llevel = levelcheck - 1;
                                charlevel = Convert.ToChar(llevel);
                            }

                        }

                        try
                        {
                            msSQL = "select application_no from agr_mst_tsuprapplication where application_gid='" + values.application_gid + "'";
                            application_no = objdbconn.GetExecuteScalar(msSQL);

                            msSQL = "select customerref_name from agr_mst_tsuprapplication where application_gid='" + values.application_gid + "'";
                            customer_name = objdbconn.GetExecuteScalar(msSQL);

                            msSQL = " select creditmanager_name, creditnationalmanager_name, creditregionalmanager_name, credithead_name from agr_mst_tsuprapplication where application_gid = '" + values.application_gid + "'";
                            objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader1.HasRows == true)
                            {
                                creditmanager_name = objODBCDatareader1["creditmanager_name"].ToString();
                                creditnationalmanager_name = objODBCDatareader1["creditnationalmanager_name"].ToString();
                                creditregionalmanager_name = objODBCDatareader1["creditregionalmanager_name"].ToString();
                                credithead_name = objODBCDatareader1["credithead_name"].ToString();
                            }
                            objODBCDatareader1.Close();
                            msSQL = " select approval_gid,approval_name from agr_trn_tsuprAppcreditapproval where application_gid = '" + values.application_gid + "' and hierary_level ='1'";
                            objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader1.HasRows == true)
                            {
                                reportingto_gid = objODBCDatareader1["approval_gid"].ToString();
                                reportingto_name = objODBCDatareader1["approval_name"].ToString();
                            }
                            objODBCDatareader1.Close();

                            msSQL = "select b.employee_emailid from agr_mst_tsuprapplication a left join hrm_mst_temployee b on b.employee_gid = a.creditnationalmanager_gid  where application_gid = '" + values.application_gid + "'";
                            nationalmanager_mailid = objdbconn.GetExecuteScalar(msSQL);

                            msSQL = "select b.employee_emailid from agr_mst_tsuprapplication a left join hrm_mst_temployee b on b.employee_gid = a.creditregionalmanager_gid  where application_gid = '" + values.application_gid + "'";
                            regionalmanager_mailid = objdbconn.GetExecuteScalar(msSQL);

                            msSQL = "select b.employee_emailid from agr_mst_tsuprapplication a left join hrm_mst_temployee b on b.employee_gid = a.credithead_gid  where application_gid = '" + values.application_gid + "'";
                            credithead_mailid = objdbconn.GetExecuteScalar(msSQL);

                            msSQL = " select b.employee_emailid from agr_mst_tsuprapplication a left join hrm_mst_temployee b on b.employee_gid = a.relationshipmanager_gid where a.application_gid='" + values.application_gid + "'";
                            relationshipmanager_mailid = objdbconn.GetExecuteScalar(msSQL);

                            msSQL = "select b.employee_emailid from agr_mst_tsuprapplication a left join hrm_mst_temployee b on b.employee_gid = a.creditmanager_gid  where application_gid = '" + values.application_gid + "'";
                            creditmanager_mailid = objdbconn.GetExecuteScalar(msSQL);

                            msSQL = " select b.employee_emailid from agr_trn_tsuprAppcreditapproval a left join hrm_mst_temployee b on b.employee_gid = a.approval_gid where a.application_gid='" + values.application_gid + "'  and hierary_level ='2'";
                            reportingto_mailid = objdbconn.GetExecuteScalar(msSQL);

                            msSQL = " select concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by  from agr_mst_tsuprapplication a" +
                                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                                    " left join adm_mst_tuser c on c.user_gid=b.user_gid  where a.created_by = '" + values.created_by + "'";
                            creater_name = objdbconn.GetExecuteScalar(msSQL);

                            if (charlevel == '\u0002')
                            {
                                tomail_id = credithead_mailid;
                                cc_mailid = "" + creditmanager_mailid + "," + regionalmanager_mailid + "," + nationalmanager_mailid + "," + relationshipmanager_mailid + "";
                                content = "Pending L3 Approval";
                                head_name = credithead_name;
                            }
                            else if (charlevel == '\u0001')
                            {
                                tomail_id = nationalmanager_mailid;
                                cc_mailid = "" + creditmanager_mailid + "," + regionalmanager_mailid + "," + credithead_mailid + "," + relationshipmanager_mailid + "";
                                content = "Pending L2 Approval";
                                head_name = creditnationalmanager_name;
                            }
                            msSQL = " SELECT pop_server, pop_port, pop_username, pop_password" +
                                " FROM adm_mst_tcompany";
                            objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader1.HasRows == true)
                            {
                                ls_server = objODBCDatareader1["pop_server"].ToString();
                                ls_port = Convert.ToInt32(objODBCDatareader1["pop_port"]);
                                ls_username = objODBCDatareader1["pop_username"].ToString();
                                ls_password = objODBCDatareader1["pop_password"].ToString();
                            }
                            //lssource = ConfigurationManager.AppSettings["img_path"];
                            objODBCDatareader1.Close();
                            if (charlevel == '\u0003')
                            {

                            }
                            else
                            {
                                sub = " ARN(" + application_no + ") : Application approval required ";
                                body = "<style>table, th, td {border: 1px solid black;border-collapse: collapse;}</style>";
                                body = body + "<table style='border-right: 1px solid black;border-top: 1px solid black;border-bottom: 1px solid black;'><tr><td style='border-right-color:white;align:center;'>";
                                //body = body + "<br />";
                                //body = body + "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp <img style='height:150px; width:380px;' src='" + lssource + "'><br />";
                                //body = body + "<br />";
                                body = body + " &nbsp&nbsp Hello," + HttpUtility.HtmlEncode(head_name)+ " <br />";
                                body = body + "<br />";
                                body = body + "&nbsp&nbsp Greetings! Quick Heads-up on the below<br />";
                                body = body + "<br />";
                                body = body + "<table style='margin-left:18px; margin-right:18px;'><tr><th >Group</th><th>ARN</th><th>Customer Name</th><th>Comments</th></tr>";
                                body = body + "<tr><td>Awaiting approval</td><td>" + application_no + "</td><td>" + HttpUtility.HtmlEncode(customer_name)+ "</td><td>" + HttpUtility.HtmlEncode(content)+ " </td></tr>";
                                body = body + "<tr><td>Actions on Comments</td><td></td><td></td><td></td></tr>";
                                body = body + "<tr><td>Queries to be addressed</td><td></td><td></td><td></td></tr></table>";
                                body = body + "<br />";
                                body = body + "&nbsp&nbsp Log into one.samunnati.com and complete the necessary actions. <br />";
                                body = body + "<br />";
                                //body = body + "&nbsp&nbsp Have a fantastic day!<br />";
                                //body = body + "<br />";
                                //body = body + "&nbsp&nbsp Thanks";
                                //body = body + "<br />";
                                //body = body + "&nbsp&nbsp Sam-Custopedia <br /> ";
                                //body = body + "<br />";
                                //body = body + "&nbsp&nbsp<hr>&nbsp&nbsp";
                                //body = body + "&nbsp&nbsp Reach out to us at samcustopedia@samunnati.com <br /> ";
                                //body = body + "<br />";
                                body = body + "</td><td style='margin-left:20px; border-left-color:white;'>&nbsp&nbsp</td></tr></table>";

                                MailMessage message = new MailMessage();
                                SmtpClient smtp = new SmtpClient();
                                message.From = new MailAddress(ls_username);
                                message.To.Add(new MailAddress(tomail_id));
                                lsBccmail_id = ConfigurationManager.AppSettings["SamagroApprovalBccMail"].ToString();

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
                                smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                                smtp.Send(message);
                                values.status = true;
                            }

                        }
                        catch (Exception ex)
                        {
                            values.message = ex.ToString();
                            values.status = false;
                        }
                    }
                    else
                    {
                        msSQL = "select appcreditapproval_gid from agr_trn_tsuprAppcreditapproval " +
                                "where  application_gid='" + values.application_gid + "' and hierary_level >'" + level + "' and initiate_flag='N' order by hierary_level asc";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {

                            msSQL = "update agr_trn_tsuprAppcreditapproval set initiate_flag='Y'" +
                                    " where appcreditapproval_gid='" + objODBCDatareader["appcreditapproval_gid"] + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        }
                        objODBCDatareader.Close();

                    }
                    dt_datatable.Dispose();
                }
            }
            values.status = true;
            values.message = "Application " + values.approval_status + " Successfully..!";



        }

        public void DaGetAppqueryStatus(mdlquerystatus values, string application_gid, string employee_gid)
        {
            msSQL = "select query_status from agr_trn_tsuprapplicationcreditquery where  application_gid ='" + application_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objODBCDatareader.Close();
                msSQL = "select query_status from agr_trn_tsuprapplicationcreditquery where query_status='Open' and  application_gid ='" + application_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.querystatus_flag = "N";
                }
                else
                {
                    values.querystatus_flag = "Y";
                }
                objODBCDatareader.Close();
            }
            else
            {
                objODBCDatareader.Close();
                values.querystatus_flag = "Y";
            }
            msSQL = " select approval_status from agr_trn_tsuprAppcreditapproval where approval_gid='" + employee_gid + "' and  application_gid ='" + application_gid + "' order by hierary_level asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    string lsapprovalstatus = dt["approval_status"].ToString();
                    if (lsapprovalstatus == "Approved" || lsapprovalstatus == "Rejected" || lsapprovalstatus == "Hold")
                    {
                        values.approved_flag = "Y";
                    }
                    else
                    {
                        values.approved_flag = "N";
                    }
                }
            }
            dt_datatable.Dispose();
            msSQL = "select application_gid from agr_trn_tsuprAppcreditapproval where  application_gid ='" + application_gid + "' and hierary_level<>'0'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.submitapproval_flag = "Y";
            }
            else
            {
                values.submitapproval_flag = "N";
            }
            objODBCDatareader.Close();
            values.status = true;

        }

        public void DaGetUpdatequerStatus(mdlquerystatus values, string appcreditquery_gid, string application_gid, string close_remarks, string user_gid)
        {
            msSQL = " update agr_trn_tsuprapplicationcreditquery set  query_status='Closed', close_remarks='" + close_remarks.Replace("'", "") + "'," +
                    " closed_by='" + user_gid + "', closed_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    " where appcreditquery_gid='" + appcreditquery_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "select query_status from agr_trn_tsuprapplicationcreditquery where query_status='Open' and  application_gid ='" + application_gid + "'";


             
                msSQL = "select application_gid from agr_trn_tsuprapplicationcreditquery where query_status = 'Open' and application_gid = 'APPC20211211337' " +
                    " and queryraised_to = 'RM'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == false)
                {
                    msSQL = "update agr_mst_tsuprapplication set  creditheadapproval_status='Comment Closed'" +
                           " where application_gid='" + application_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                objODBCDatareader.Close();
                //Mail Start
                try
                {
                    String lshierarchylevel;
                    lshierarchylevel = objdbconn.GetExecuteScalar("select hierary_level from agr_trn_tsuprapplicationcreditquery a " +
                    "left join agr_trn_tsuprAppcreditapproval b on a.appcreditapproval_gid = b.appcreditapproval_gid where a.appcreditquery_gid='" + appcreditquery_gid + "'");
                    int level = Convert.ToInt16(lshierarchylevel);
                    char hierlevel = Convert.ToChar(level);

                    msSQL = "select application_no from agr_mst_tsuprapplication where application_gid='" + application_gid + "'";
                    application_no = objdbconn.GetExecuteScalar(msSQL);
                    msSQL = "select customerref_name from agr_mst_tsuprapplication where application_gid='" + application_gid + "'";
                    customer_name = objdbconn.GetExecuteScalar(msSQL);
                    msSQL = "select closed_date from agr_trn_tsuprapplicationcreditquery where appcreditquery_gid='" + appcreditquery_gid + "'";
                    closure_time = objdbconn.GetExecuteScalar(msSQL);
                    msSQL = "select b.employee_emailid from agr_mst_tsuprapplication a left join hrm_mst_temployee b on b.employee_gid = a.creditmanager_gid where a.application_gid='" + application_gid + "'";
                    creditmanager_mailid = objdbconn.GetExecuteScalar(msSQL);
                    msSQL = " select b.employee_emailid from agr_mst_tsuprapplication a left join hrm_mst_temployee b on b.employee_gid = a.creditregionalmanager_gid where a.application_gid='" + application_gid + "'";
                    creditregionalmanager_mailid = objdbconn.GetExecuteScalar(msSQL);
                    msSQL = " select b.employee_emailid from agr_mst_tsuprapplication a left join hrm_mst_temployee b on b.employee_gid = a.creditnationalmanager_gid where a.application_gid='" + application_gid + "'";
                    creditnationalmanager_mailid = objdbconn.GetExecuteScalar(msSQL);
                    msSQL = " select b.employee_emailid from agr_mst_tsuprapplication a left join hrm_mst_temployee b on b.employee_gid = a.credithead_gid where a.application_gid='" + application_gid + "'";
                    credithead_mailid = objdbconn.GetExecuteScalar(msSQL);
                    msSQL = " select b.employee_emailid from agr_mst_tsuprapplication a left join hrm_mst_temployee b on b.employee_gid = a.created_by where a.application_gid='" + application_gid + "'";
                    creater_mailid = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = " select c.employee_emailid from agr_trn_tsuprapplicationcreditquery a " +
                            " left join adm_mst_tuser b on a.created_by = b.user_gid " +
                            " left join hrm_mst_temployee c on c.user_gid = b.user_gid where appcreditquery_gid='" + appcreditquery_gid + "'";
                    tomail_id = objdbconn.GetExecuteScalar(msSQL);
                    msSQL = " select creditmanager_name,creditregionalmanager_name,creditnationalmanager_name,relationshipmanager_name,credithead_name from agr_mst_tsuprapplication where application_gid = '" + application_gid + "'";
                    objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader1.HasRows == true)
                    {
                        credithead_name = objODBCDatareader1["credithead_name"].ToString();
                        creditregionalmanager_name = objODBCDatareader1["creditregionalmanager_name"].ToString();
                        creditnationalmanager_name = objODBCDatareader1["creditnationalmanager_name"].ToString();
                        creditmanager_name = objODBCDatareader1["creditmanager_name"].ToString();
                        relationshipmanager_name = objODBCDatareader1["relationshipmanager_name"].ToString();
                    }
                    objODBCDatareader1.Close();


                    if (hierlevel == '\0')
                    {
                        tomail_id = creditmanager_mailid;
                        cc_mailid = "" + creater_mailid + "," + creditregionalmanager_mailid + "," + creditnationalmanager_mailid + "," + credithead_mailid + "";
                    }
                    else if (hierlevel == '\u0001')
                    {
                        tomail_id = creditregionalmanager_mailid;
                        cc_mailid = "" + creditmanager_mailid + "," + creater_mailid + "," + creditnationalmanager_mailid + "," + credithead_mailid + "";
                    }
                    else if (hierlevel == '\u0002')
                    {
                        tomail_id = creditnationalmanager_mailid;
                        cc_mailid = "" + creater_mailid + "," + creditregionalmanager_mailid + "," + creditmanager_mailid + "," + credithead_mailid + "";
                    }
                    else if (hierlevel == '\u0003')
                    {
                        tomail_id = credithead_mailid;
                        cc_mailid = "" + creater_mailid + "," + creditregionalmanager_mailid + "," + creditnationalmanager_mailid + "," + creditmanager_mailid + "";
                    }


                    msSQL = " SELECT pop_server, pop_port, pop_username, pop_password" +
                                    " FROM adm_mst_tcompany";
                    objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader1.HasRows == true)
                    {
                        ls_server = objODBCDatareader1["pop_server"].ToString();
                        ls_port = Convert.ToInt32(objODBCDatareader1["pop_port"]);
                        ls_username = objODBCDatareader1["pop_username"].ToString();
                        ls_password = objODBCDatareader1["pop_password"].ToString();
                    }
                    objODBCDatareader1.Close();


                    //lssource = ConfigurationManager.AppSettings["img_path"];
                    sub = " Query Closed : ARN(" + application_no + ")  ";
                    body = "<style>table, th, td {border: 1px solid black;border-collapse: collapse;}</style>";
                    body = body + "<table style='border-right: 1px solid black;border-top: 1px solid black;border-bottom: 1px solid black;'><tr><td style='border-right-color:white;align:center;'>";
                    body = body + "<br />";
                    body = body + "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp <img style='height:150px; width:380px;' src='" + lssource + "'><br />";
                    body = body + "<br />";
                    body = body + " &nbsp &nbsp Dear Sir / Madam, <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp Greetings! <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp The user has closed the query. Please check and proceed with the application. <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp <b> Application Number : </b> " + application_no + "  <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp <b> Customer Name : </b> " + HttpUtility.HtmlEncode(customer_name)+ "  <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp <b> RM Name : </b> " + HttpUtility.HtmlEncode(relationshipmanager_name)+ "  <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp <b> Credit Manager Name : </b> " + HttpUtility.HtmlEncode(creditmanager_name)+ "  <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp <b> Credit Regional Head Name : </b> " + HttpUtility.HtmlEncode(creditregionalmanager_name) + "  <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp <b> Credit National Head Name : </b> " + HttpUtility.HtmlEncode(creditnationalmanager_name)+ "  <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp <b> Credit Head Name : </b> " + HttpUtility.HtmlEncode(credithead_name)+ "  <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp Log into Sam - Custopedia and complete the necessary actions.";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp Regards";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp Sam-Custopedia <br /> ";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp<hr>&nbsp&nbsp";
                    body = body + "&nbsp &nbsp Reach out to us at samcustopedia@samunnati.com <br /> ";
                    body = body + "<br />";
                    body = body + "</td><td style='margin-left:20px; border-left-color:white;'>&nbsp&nbsp</td></tr></table>";

                    MailMessage message = new MailMessage();
                    SmtpClient smtp = new SmtpClient();
                    message.From = new MailAddress(ls_username);
                    message.To.Add(new MailAddress(tomail_id));
                    lsBccmail_id = ConfigurationManager.AppSettings["SamagroApprovalBccMail"].ToString();

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
                    smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Send(message);
                    values.status = true;
                }
                catch (Exception ex)
                {
                    values.message = ex.ToString();
                }
                //Mail End
                values.status = true;
                values.message = "Query Closed Successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }

        }

        public void DaCreditApplicationCount(string user_gid, string employee_gid, credtiApplicationCount values)
        {

            msSQL = " select count(a.application_gid) as newcount from agr_mst_tsuprapplication a " +
                     " left join agr_trn_tsuprAppcreditapproval b on a.application_gid = b.application_gid" +
                     " where a.approval_flag='Y' and b.approval_gid='" + employee_gid + "' and  b.approval_status='Pending'" +
                     " and a.application_gid not in (select application_gid from agr_mst_tsuprapplication where creditheadapproval_status like '%Rejected' or creditheadapproval_status like '%Hold'  ) and b.hierary_level<>'0' ";
            values.newcreditapplication_count = objdbconn.GetExecuteScalar(msSQL);
            int newapplicationcount = Convert.ToInt16(values.newcreditapplication_count);

            msSQL = " select count(distinct b.application_gid) as newcount from agr_mst_tsuprapplication a " +
                     " left join agr_trn_tsuprAppcreditapproval b on a.application_gid = b.application_gid  " +
                     " where a.approval_flag = 'Y' and b.approval_status = 'Approved' and  a.approval_status <>'Submitted to CC' and a.creditheadapproval_status like '%Approved' and b.approval_gid = '" + employee_gid + "' and b.hierary_level <> '0'";
            values.approvedapplication_count = objdbconn.GetExecuteScalar(msSQL);
            int approvedcount = Convert.ToInt16(values.approvedapplication_count);

            msSQL = " select count(distinct b.application_gid) as newcount from agr_mst_tsuprapplication a " +
                   " left join agr_trn_tsuprAppcreditapproval b on a.application_gid = b.application_gid" +
                   "  where a.approval_flag='Y' and b.approval_gid='" + employee_gid + "' and  a.approval_status='Submitted to CC' and b.hierary_level<>'0' ";
            values.submitted2ccapp_count = objdbconn.GetExecuteScalar(msSQL);
            int submitted2ccappcount = Convert.ToInt16(values.submitted2ccapp_count);

            msSQL = " select application_gid from agr_trn_tsuprAppcreditapproval where approval_gid='" + employee_gid + "'  and hierary_level<>'0' group by application_gid";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getrejectholdlist = new List<rejectholdlist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr in dt_datatable.Rows)
                {
                    msSQL = " select application_gid from agr_trn_tsuprAppcreditapproval where application_gid='" + dr["application_gid"] + "' and (approval_status='Rejected' or approval_status='Hold')";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        msSQL = " select a.application_gid  from agr_mst_tsuprapplication a" +
                                "   where  application_gid='" + objODBCDatareader["application_gid"] + "'";

                        objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader1.HasRows == true)
                        {
                            getrejectholdlist.Add(new rejectholdlist
                            {

                            });

                        }
                        objODBCDatareader1.Close();
                    }
                    objODBCDatareader.Close();
                }
            }
            values.rejectholdlist = getrejectholdlist;
            dt_datatable.Dispose();
            int rejectholdcount = getrejectholdlist.Count;
            values.rejectholdapplication_count = Convert.ToInt16(rejectholdcount);

            int lstotal = newapplicationcount + rejectholdcount + approvedcount + submitted2ccappcount;
            values.lstotalcount = Convert.ToInt16(lstotal);
        }

        public void DaMyApplicationCount(string user_gid, string employee_gid, credtiApplicationCount values)
        {

            msSQL = " select count(a.application_gid) as newcount from agr_mst_tsuprapplication a " +
                     " left join agr_trn_tsuprAppcreditapproval b on a.application_gid = b.application_gid" +
                     "  where a.approval_flag='Y' and b.approval_gid='" + employee_gid + "' and  b.approval_status='Pending'and b.hierary_level='0' ";
            values.newcreditapplication_count = objdbconn.GetExecuteScalar(msSQL);
            int newapplicationcount = Convert.ToInt16(values.newcreditapplication_count);

            msSQL = " select count(a.application_gid) as newcount from agr_mst_tsuprapplication a " +
                     " left join agr_trn_tsuprAppcreditapproval b on a.application_gid = b.application_gid" +
                    " where a.approval_flag='Y' and b.approval_gid='" + employee_gid + "' and  b.approval_status='Submitted to Approval' and  a.approval_status <>'Submitted to CC' and a.approval_status <>'CC Approved' and a.approval_status <>'CC Rejected' and" +
                    " (a.approval_status='Submitted to Credit Approval' or a.approval_status like '%Approved' or a.creditheadapproval_status='Comment Raised' or a.creditheadapproval_status ='Comment Raised to RM' or a.creditheadapproval_status ='Comment Closed') and b.hierary_level='0' and " +
                    "  a.application_gid not in (select application_gid from agr_mst_tsuprapplication where creditheadapproval_status like '%Rejected' or creditheadapproval_status like '%Hold'  ) ";
            values.approvedapplication_count = objdbconn.GetExecuteScalar(msSQL);
            int approvedcount = Convert.ToInt16(values.approvedapplication_count);

            msSQL = " select count(a.application_gid) as newcount from agr_mst_tsuprapplication a " +
               " left join agr_trn_tsuprAppcreditapproval b on a.application_gid = b.application_gid" +
               " where a.approval_flag='Y' and b.approval_gid='" + employee_gid + "' and  b.approval_status='Submitted to Approval' and  a.approval_status <>'Submitted to CC' and a.approval_status='CC Approved'";
            values.ccapproval_count = objdbconn.GetExecuteScalar(msSQL);
            int ccapprovalcount = Convert.ToInt16(values.ccapproval_count);

            msSQL = " select count(a.application_gid) as newcount from agr_mst_tsuprapplication a " +
                   " left join agr_trn_tsuprAppcreditapproval b on a.application_gid = b.application_gid" +
                   "  where a.approval_flag='Y' and b.approval_gid='" + employee_gid + "' and  a.approval_status='Submitted to CC' and b.hierary_level='0' ";
            values.submitted2ccapp_count = objdbconn.GetExecuteScalar(msSQL);
            int submitted2ccappcount = Convert.ToInt16(values.submitted2ccapp_count);

            msSQL = " select application_gid from agr_trn_tsuprAppcreditapproval where approval_gid='" + employee_gid + "'  and hierary_level='0' group by application_gid";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getrejectholdlist = new List<rejectholdlist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr in dt_datatable.Rows)
                {
                    msSQL = " select application_gid from agr_trn_tsuprAppcreditapproval where application_gid='" + dr["application_gid"] + "' and (approval_status='Rejected' or approval_status='Hold')";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        msSQL = " select a.application_gid  from agr_mst_tsuprapplication a" +
                                "   where  application_gid='" + objODBCDatareader["application_gid"] + "'";

                        objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader1.HasRows == true)
                        {
                            getrejectholdlist.Add(new rejectholdlist
                            {

                            });

                        }
                        objODBCDatareader1.Close();
                    }
                    objODBCDatareader.Close();
                }
            }
            values.rejectholdlist = getrejectholdlist;
            dt_datatable.Dispose();
            int rejectholdcount = getrejectholdlist.Count;
            values.rejectholdapplication_count = Convert.ToInt16(rejectholdcount);

            int lstotal = newapplicationcount + rejectholdcount + approvedcount + submitted2ccappcount;
            values.lstotalcount = Convert.ToInt16(lstotal);
        }

        public void GetAppcreditApprovallogSummary(applcreditapproval values, string application_gid)
        {
            msSQL = " select appcreditapprovallog_gid, appcreditapproval_gid,approval_name,approval_status ,b.user_code,a.hierary_level," +
                     " concat(b.user_firstname, ' ', b.user_lastname)  as created_by,approval_remarks,date_format(a.approved_date, '%d-%m-%Y %h:%i %p') as approved_date, " +
                     " date_format(b.created_date, '%d-%m-%Y %h:%i %p') as created_date,date_format(a.hold_date, '%d-%m-%Y %h:%i %p') as hold_date," +
                     " date_format(a.rejected_date, '%d-%m-%Y %h:%i %p') as rejected_date  from agr_trn_tsuprAppcreditapprovallog a " +
                     " left join adm_mst_tuser b on b.user_gid = a.created_by " +
                     " where a.application_gid='" + application_gid + "'  and a.hierary_level <>'0' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getappcreditapprovallist = new List<appcreditapprovallist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getappcreditapprovallist.Add(new appcreditapprovallist
                    {
                        appcreditapprovallog_gid = dt["appcreditapprovallog_gid"].ToString(),
                        appcreditapproval_gid = dt["appcreditapproval_gid"].ToString(),
                        approval_name = dt["approval_name"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        approval_remarks = dt["approval_remarks"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        approved_date = dt["approved_date"].ToString(),
                        hold_date = dt["hold_date"].ToString(),
                        rejected_date = dt["rejected_date"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        hierary_level = dt["hierary_level"].ToString(),
                        user_code = dt["user_code"].ToString(),
                    });
                    values.appcreditapprovallist = getappcreditapprovallist;
                }
            }
            dt_datatable.Dispose();


        }

        public void DaGetCCApprovalSummary(string employee_gid, MdlMstCreditApproval values)
        {
            msSQL = " select a.application_gid,a.application_no,a.customerref_name as customer_name,a.customer_urn,a.vertical_name," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,a.status,a.applicant_type,a.created_by as createdby," +
                    " date_format(a.submitted_date,'%d-%m-%Y %h:%i %p') as updated_date, a.productcharge_flag, a.economical_flag,a.approval_status, " +
                    " a.overalllimit_amount, region,a.creditgroup_gid,d.initiate_flag,appcreditapproval_gid," +
                    " concat(t.user_firstname,' ',t.user_lastname,' / ',t.user_code)  as creditassigned_by,date_format(a.creditassigned_date,'%d-%m-%Y %h:%i %p') as creditassigned_date," +
                    " creditheadapproval_status,date_format(a.creditheadapproval_date,'%d-%m-%Y %h:%i %p') as creditheadapproval_date  from agr_mst_tsuprapplication a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.submitted_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                    " left join hrm_mst_temployee s on s.employee_gid=a.creditassigned_by " +
                    " left join adm_mst_tuser t on t.user_gid=s.user_gid " +
                    " left join agr_trn_tsuprAppcreditapproval d on a.application_gid=d.application_gid " +
                    " where a.approval_flag='Y' and d.approval_gid='" + employee_gid + "' and  d.approval_status='Submitted to Approval' and a.approval_status ='CC Approved' and a.approval_status <>'Submitted to CC'   " +
                    " group by a.application_gid order by a.updated_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicaition_list = new List<applicaition_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                string lsapproval_flag;
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msSQL = " select application_gid from agr_trn_tsuprapplicationcreditquery where query_status = 'Open' and application_gid = '" + dt["application_gid"].ToString() + "' " +
                            " and (queryraised_to = 'RM' or queryraised_to = 'Credit')";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == false)
                    {
                        lsunderwriting_flag = "Y";
                    }
                    else
                    {
                        lsunderwriting_flag = "N";
                    }
                    objODBCDatareader.Close();
                    msSQL = "select a.appcreditquery_gid from agr_trn_tsuprapplicationcreditquery a " +
                            "left join agr_trn_tsuprAppcreditapproval b on a.appcreditapproval_gid=b.appcreditapproval_gid " +
                            "where a.application_gid='" + dt["application_gid"].ToString() + "' and a.query_status='Open' and b.hierary_level<>'0'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lsapproval_flag = "Y";
                    }
                    else
                    {
                        lsapproval_flag = "N";
                    }
                    objODBCDatareader.Close();
                    getapplicaition_list.Add(new applicaition_list
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical_name = dt["vertical_name"].ToString(),
                        created_date = dt["updated_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        economical_flag = dt["economical_flag"].ToString(),
                        productcharge_flag = dt["productcharge_flag"].ToString(),
                        application_status = dt["status"].ToString(),
                        applicant_type = dt["applicant_type"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        region = dt["region"].ToString(),
                        overalllimit_amount = dt["overalllimit_amount"].ToString(),
                        creditgroup_gid = dt["creditgroup_gid"].ToString(),
                        createdby = dt["createdby"].ToString(),
                        initiate_flag = dt["initiate_flag"].ToString(),
                        appcreditapproval_gid = dt["appcreditapproval_gid"].ToString(),
                        creditheadapproval_status = dt["creditheadapproval_status"].ToString(),
                        creditheadapproval_date = dt["creditheadapproval_date"].ToString(),
                        creditassigned_date = dt["creditassigned_date"].ToString(),
                        creditassigned_by = dt["creditassigned_by"].ToString(),
                        approval_flag = lsapproval_flag,
                        underwriting_flag = lsunderwriting_flag
                    });

                }
            }
            values.applicaition_list = getapplicaition_list;
            dt_datatable.Dispose();
        }
    }
}