using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Odbc;
using ems.utilities.Functions;
using ems.foundation.Models;
using System.Net.Mail;
using System.Net;
using System.Configuration;
using System.Globalization;
using System.Web;

namespace ems.foundation.DataAccess
{
    public class DaTrnCampaign
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable, dt_datatable1;
        string msSQL, msGetGid, msGetGidREF;
        int mnResult;
        public string lssource;
        string lsstartdate;
        string lsenddate;
        string lsassesmentdate;

        int k;
        public string ls_server, lsassignee, ls_username, ls_password, tomail_id, tomail_id1, tomail_id2, sub, body, employeename, cc_mailid, lsto_mail, employee_reporting_to;
        int ls_port;
        string lsemployee_name, lsemployee_gid, lcampaign_status, lsTo2members, lscampaign_type, lsname, lscampaign_name, lscampaign_apr, lscampaign_remarks, lsBccmail_id, lscreated_by, lstomembers, lsdescription, lscc2members, lstonames, lsauditcreation_gid, lscreated_date, frommail_id, lscc_mail, strBCC, lsbcc_mail;
        string sToken = string.Empty;
        Random rand = new Random();
        public string[] lsToReceipients;
        public string[] lsCCReceipients;
        public string[] lsBCCReceipients;

        public void DaGetCampaignType(MdlTrnCampaign objcampaigntype, string employee_gid)
        {
            try
            {

                msSQL = "delete from fnd_trn_tcampaigndtl where status = 'Y' and created_by = '" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " SELECT campaigntype_gid,campaigntype_name FROM fnd_mst_tcampaigntype ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcampaigntype_list = new List<campaigntype_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcampaigntype_list.Add(new campaigntype_list
                        {
                            campaigntype_gid = (dr_datarow["campaigntype_gid"].ToString()),
                            campaigntype_name = (dr_datarow["campaigntype_name"].ToString()),
                        });
                    }
                    objcampaigntype.campaigntype_list = getcampaigntype_list;
                }
                dt_datatable.Dispose();

                objcampaigntype.status = true;
            }
            catch
            {
                objcampaigntype.status = false;
            }

        }
        public void DaGetCustomer(MdlTrnCampaign objcustomer, string employee_gid)
        {
            try
            {


                msSQL = " SELECT customer_gid,customer_name FROM fnd_mst_tcustomer where status_remarks = 'Approved' ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcustomer_list = new List<customer_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcustomer_list.Add(new customer_list
                        {
                            customer_gid = (dr_datarow["customer_gid"].ToString()),
                            customer_name = (dr_datarow["customer_name"].ToString()),
                        });
                    }
                    objcustomer.customer_list = getcustomer_list;
                }
                dt_datatable.Dispose();

                objcustomer.status = true;
            }
            catch
            {
                objcustomer.status = false;
            }

        }
        public void DaGetCampaignSummary(string employee_gid, MdlTrnCampaign values)
        {
            msSQL = " select a.campaign_gid,a.campaign_code,a.campaign_status,a.campaign_name," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                    " case when a.status='N' then 'Inactive' else 'Active' end as status," +
                    " case when a.campaign_status='NA' then 'Not yet Completed' when a.campaign_status='Pending' " +
                    " then 'Approval Pending' else 'Completed' end as campaign_status," +
                    " a.status_flag,b.customer_name ," +
                    " concat(d.user_firstname, ' ', d.user_lastname, ' / ', d.user_code) as created_by " +
                    " from fnd_trn_tcampaign a left join fnd_mst_tcustomer b on a.customer_gid = b.customer_gid " +
                    " left join hrm_mst_temployee c on a.created_by = c.employee_gid" +
                    " left join adm_mst_tuser d on c.user_gid = d.user_gid " +
                    " where a.created_by='" + employee_gid + "' and (a.campaign_status = 'pending' or a.status_flag = 'pending 'or a.status_flag = 'N')  order by a.created_date  desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcampaign_list = new List<campaign_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcampaign_list.Add(new campaign_list
                    {
                        campaign_gid = (dr_datarow["campaign_gid"].ToString()),
                        campaign_code = (dr_datarow["campaign_code"].ToString()),
                        campaign_name = (dr_datarow["campaign_name"].ToString()),
                        Status = (dr_datarow["status"].ToString()),
                        campaign_status = (dr_datarow["campaign_status"].ToString()),
                        status_flag = (dr_datarow["status_flag"].ToString()),                      
                        customer_name = (dr_datarow["customer_name"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString())

                    });
                }
                values.campaign_list = getcampaign_list;
            }
            dt_datatable.Dispose();
        }




        public void DaGetCampaignApprovalApproved(string employee_gid, MdlTrnCampaign values)
        {
            msSQL = " select a.campaign_gid,a.campaign_code,a.campaign_name,a.campaign_status," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                    " a.status_flag,b.customer_name ," +
                    " concat(d.user_firstname, ' ', d.user_lastname, ' / ', d.user_code) as created_by " +
                    " from fnd_trn_tcampaign a left join fnd_mst_tcustomer b on a.customer_gid = b.customer_gid " +
                    " left join hrm_mst_temployee c on a.created_by = c.employee_gid" +
                    " left join adm_mst_tuser d on c.user_gid = d.user_gid " +
                    " where a.created_by='" + employee_gid + "' and " +
                    " (a.campaign_status= 'Approved' or a.campaign_status= 'WIP' or  a.campaign_status= 'Pending for Final Approval' ) " +
                    " order by  a.created_date  desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcampaign_list = new List<campaign_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcampaign_list.Add(new campaign_list
                    {
                        campaign_gid = (dr_datarow["campaign_gid"].ToString()),
                        campaign_code = (dr_datarow["campaign_code"].ToString()),
                        campaign_name = (dr_datarow["campaign_name"].ToString()),
                        customer_name = (dr_datarow["customer_name"].ToString()),
                        campaign_status = (dr_datarow["campaign_status"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString())

                    });
                }
                values.campaign_list = getcampaign_list;
            }
            dt_datatable.Dispose();
        }

        public void DaGetCampaignApprovalRejected(string employee_gid, MdlTrnCampaign values)
        {
            msSQL = " select a.campaign_gid,a.campaign_code,a.campaign_status,a.campaign_name," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                    " a.status_flag,b.customer_name ," +
                    " concat(d.user_firstname, ' ', d.user_lastname, ' / ', d.user_code) as created_by " +
                    " from fnd_trn_tcampaign a left join fnd_mst_tcustomer b on a.customer_gid = b.customer_gid " +
                    " left join hrm_mst_temployee c on a.created_by = c.employee_gid" +
                    " left join adm_mst_tuser d on c.user_gid = d.user_gid " +
                    " where a.created_by='" + employee_gid + "' and a.campaign_status= 'Rejected' order by a.created_date  desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcampaign_list = new List<campaign_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcampaign_list.Add(new campaign_list
                    {
                        campaign_gid = (dr_datarow["campaign_gid"].ToString()),
                        campaign_code = (dr_datarow["campaign_code"].ToString()),
                        campaign_name = (dr_datarow["campaign_name"].ToString()),
                        customer_name = (dr_datarow["customer_name"].ToString()),
                        campaign_status = (dr_datarow["campaign_status"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString())

                    });
                }
                values.campaign_list = getcampaign_list;
            }
            dt_datatable.Dispose();
        }

        public void DaGetCampaignClosed(string employee_gid, MdlTrnCampaign values)
        {
            msSQL = " select a.campaign_gid,a.campaign_code,a.campaign_status,a.campaign_name," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                    " a.status_flag,b.customer_name ," +
                    " concat(d.user_firstname, ' ', d.user_lastname, ' / ', d.user_code) as created_by " +
                    " from fnd_trn_tcampaign a left join fnd_mst_tcustomer b on a.customer_gid = b.customer_gid " +
                    " left join hrm_mst_temployee c on a.created_by = c.employee_gid" +
                    " left join adm_mst_tuser d on c.user_gid = d.user_gid " +
                    " where a.created_by='" + employee_gid + "' and a.campaign_status= 'Mycampaign closed' order by  a.created_date  desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcampaign_list = new List<campaign_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcampaign_list.Add(new campaign_list
                    {
                        campaign_gid = (dr_datarow["campaign_gid"].ToString()),
                        campaign_code = (dr_datarow["campaign_code"].ToString()),
                        campaign_name = (dr_datarow["campaign_name"].ToString()),
                        customer_name = (dr_datarow["customer_name"].ToString()),
                        campaign_status = (dr_datarow["campaign_status"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString())

                    });
                }
                values.campaign_list = getcampaign_list;
            }
            dt_datatable.Dispose();
        }
        public void DaGetCampaignCounts(MdlTrnCampaign values, string Employee_gid)
        {
            msSQL = " select count(campaign_gid) as campaignpending_count from fnd_trn_tcampaign  " +
            " where created_by='" + Employee_gid + "' and (campaign_status = 'pending' or status_flag = 'pending 'or status_flag = 'N') ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.campaignpending_count = objODBCDatareader["campaignpending_count"].ToString();

            }
            objODBCDatareader.Close();

            msSQL = "select count(campaign_gid) as rejected_count from fnd_trn_tcampaign  " +
                    " where created_by='" + Employee_gid + "' and campaign_status= 'Rejected'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                values.rejected_count = objODBCDatareader["rejected_count"].ToString();

            }

            objODBCDatareader.Close();

            msSQL = "select count(campaign_gid) as approved_count from fnd_trn_tcampaign  " +
                    " where created_by='" + Employee_gid + "' and (campaign_status= 'Approved' or campaign_status= 'WIP' or  campaign_status= 'Pending for Final Approval') ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                values.approved_count = objODBCDatareader["approved_count"].ToString();

            }

            objODBCDatareader.Close();

            msSQL = "select count(campaign_gid) as closed_count from fnd_trn_tcampaign " +
                    " where created_by='" + Employee_gid + "' and campaign_status= 'Mycampaign closed'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                values.closed_count = objODBCDatareader["closed_count"].ToString();

            }

            objODBCDatareader.Close();
        }
        public void DaGetCampaignApprovalCounts(MdlTrnCampaign values, string Employee_gid)
        {
            msSQL = " select count(campaign_gid) as campaignapprovalpending_count from fnd_trn_tcampaign  " +
            " where campaign_approver='" + Employee_gid + "' and campaign_status = 'pending'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.campaignapprovalpending_count = objODBCDatareader["campaignapprovalpending_count"].ToString();

            }
            objODBCDatareader.Close();

            msSQL = "select count(campaign_gid) as approvalrejected_count from fnd_trn_tcampaign  " +
                    " where campaign_approver='" + Employee_gid + "' and campaign_status= 'Rejected'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                values.approvalrejected_count = objODBCDatareader["approvalrejected_count"].ToString();

            }

            objODBCDatareader.Close();

            msSQL = "select count(campaign_gid) as approvalapproved_count from fnd_trn_tcampaign  " +
                    " where campaign_approver='" + Employee_gid + "' and (campaign_status= 'Approved' or campaign_status= 'WIP') ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                values.approvalapproved_count = objODBCDatareader["approvalapproved_count"].ToString();

            }

            objODBCDatareader.Close();
        }


        public void DacampaignDetailsEdit(string campaign_gid, MdlTrnCampaign values, string employee_gid)
        {


            msSQL = " select a.campaign_gid,a.campaign_code,a.campaign_name,a.campaign_approver," +
               " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,a.created_by,b.customer_name,b.customer_gid,e.campaigntype_gid,e.campaigntype_name," +
               " a.status_flag,b.customer_name ,a.contact_name,a.contact_mobile,a.contact_email,a.campaign_cost,a.start_date,a.end_date,a.assesment_date,a.os_assesment_date,a.loan_availed," +
               " concat(d.user_firstname, ' ', d.user_lastname, ' / ', d.user_code) as created_by " +
               " from fnd_trn_tcampaign a left join fnd_mst_tcustomer b on a.customer_gid = b.customer_gid " +
               " left join fnd_mst_tcampaigntype e on e.campaigntype_gid = a.campaigntype_gid " +
               " left join hrm_mst_temployee c on a.created_by = c.employee_gid" +
               " left join adm_mst_tuser d on c.user_gid = d.user_gid " +
               " where a.campaign_gid='" + campaign_gid + "' " +
               " order by campaign_gid desc";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.campaign_code = objODBCDatareader["campaign_code"].ToString();
                values.campaign_name = objODBCDatareader["campaign_name"].ToString();
                values.created_by = objODBCDatareader["created_by"].ToString();
                values.campaigntype_name = objODBCDatareader["campaigntype_name"].ToString();
                values.customer_gid = objODBCDatareader["customer_gid"].ToString();
                values.campaigntype_gid = objODBCDatareader["campaigntype_gid"].ToString();
                values.customer_name = objODBCDatareader["customer_name"].ToString();
                values.contact_name = objODBCDatareader["contact_name"].ToString();
                values.campaign_approver = objODBCDatareader["campaign_approver"].ToString();
                values.contact_mobile = objODBCDatareader["contact_mobile"].ToString();
                values.contact_email = objODBCDatareader["contact_email"].ToString();
                values.campaign_cost = objODBCDatareader["campaign_cost"].ToString();

                if (objODBCDatareader["start_date"].ToString() == "")
                {
                }
                else
                {
                    values.start_date = Convert.ToDateTime(objODBCDatareader["start_date"]).ToString("dd-MM-yyyy");
                }


                if (objODBCDatareader["end_date"].ToString() == "")
                {
                }
                else
                {
                    values.end_date = Convert.ToDateTime(objODBCDatareader["end_date"]).ToString("dd-MM-yyyy");
                }

                if (objODBCDatareader["assesment_date"].ToString() == "")
                {
                }
                else
                {
                    values.assesment_date = Convert.ToDateTime(objODBCDatareader["assesment_date"]).ToString("dd-MM-yyyy");
                }
                values.os_assesment_date = objODBCDatareader["os_assesment_date"].ToString();
                values.loan_availed = objODBCDatareader["loan_availed"].ToString();


            }

            objODBCDatareader.Close();
            msSQL = " select campaignapproving2employee_gid,employee_gid,employee_name from fnd_mst_tcampaignapproving2employee " +
                " where campaign_gid='" + campaign_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getemployeeList = new List<employees>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getemployeeList.Add(new employees
                    {
                        campaignapproving2employee_gid = dt["campaignapproving2employee_gid"].ToString(),
                        employee_gid = dt["employee_gid"].ToString(),
                        employee_name = dt["employee_name"].ToString(),
                    });
                    values.employees = getemployeeList;
                }
            }
            dt_datatable.Dispose();
            msSQL = " SELECT a.user_firstname,a.user_gid ,concat(a.user_firstname,' ',a.user_lastname,' / ',a.user_code) as employee_name,b.employee_gid from adm_mst_tuser a " +
                " LEFT JOIN hrm_mst_temployee b ON a.user_gid=b.user_gid " +
                " where user_status<>'N' order by a.user_firstname asc";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_manageremployee = new List<assignee>();
            if (dt_datatable.Rows.Count != 0)
            {
                values.assignee = dt_datatable.AsEnumerable().Select(row =>
                  new assignee
                  {
                      employee_gid = row["employee_gid"].ToString(),
                      employee_name = row["employee_name"].ToString()
                  }
                ).ToList();
            }
            dt_datatable.Dispose();

            values.status = true;

        }
        public void DacampaignDetailsView(string campaign_gid, MdlTrnCampaign values, string employee_gid)
        {
            msSQL = " select a.campaign_gid,a.campaign_code,a.campaign_name,h.employee_gid,group_concat(h.employee_name) as employee_name," +
               " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,a.created_by,b.customer_name,b.customer_gid,e.campaigntype_gid,e.campaigntype_name," +
               " date_format(start_date, '%d-%m-%Y') as start_date ,date_format(end_date, '%d-%m-%Y') as end_date ,date_format(assesment_date, '%d-%m-%Y') as assesment_date ,date_format(os_assesment_date, '%d-%m-%Y') as os_assesment_date ,a.status_flag,b.customer_name ,a.contact_name,a.contact_mobile,a.contact_email,a.campaign_cost,a.loan_availed," +
               " concat(d.user_firstname, ' ', d.user_lastname, ' / ', d.user_code) as created_by, " +
               " concat(f.user_firstname, ' ', f.user_lastname, ' / ', f.user_code) as campaign_approver " +
               " from fnd_trn_tcampaign a left join fnd_mst_tcustomer b on a.customer_gid = b.customer_gid " +
               " left join fnd_mst_tcampaignapproving2employee h on h.campaign_gid = a.campaign_gid " +
                " left join fnd_mst_tcampaigntype e on e.campaigntype_gid = a.campaigntype_gid " +
               " left join hrm_mst_temployee c on a.created_by = c.employee_gid" +
               " left join hrm_mst_temployee g on a.campaign_approver = g.employee_gid" +
               " left join adm_mst_tuser d on c.user_gid = d.user_gid " +
               " left join adm_mst_tuser f on g.user_gid = f.user_gid " +
               " where a.campaign_gid='" + campaign_gid + "' " +
               " group by campaign_gid desc";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.campaign_code = objODBCDatareader["campaign_code"].ToString();
                values.campaign_name = objODBCDatareader["campaign_name"].ToString();
                values.created_by = objODBCDatareader["created_by"].ToString();
                values.campaigntype_name = objODBCDatareader["campaigntype_name"].ToString();
                values.customer_gid = objODBCDatareader["customer_gid"].ToString();
                values.campaigntype_gid = objODBCDatareader["campaigntype_gid"].ToString();
                values.customer_name = objODBCDatareader["customer_name"].ToString();
                values.contact_name = objODBCDatareader["contact_name"].ToString();
                values.campaign_approver = objODBCDatareader["campaign_approver"].ToString();
                values.contact_mobile = objODBCDatareader["contact_mobile"].ToString();
                values.contact_email = objODBCDatareader["contact_email"].ToString();
                values.campaign_cost = objODBCDatareader["campaign_cost"].ToString();
                values.employee_gid = objODBCDatareader["employee_gid"].ToString();
                values.manager_name = objODBCDatareader["employee_name"].ToString();
                values.os_assesment_date = objODBCDatareader["os_assesment_date"].ToString();
                values.start_date = objODBCDatareader["start_date"].ToString();
                values.end_date = objODBCDatareader["end_date"].ToString();
                values.assesment_date = objODBCDatareader["assesment_date"].ToString();
                values.loan_availed = objODBCDatareader["loan_availed"].ToString();


            }

            objODBCDatareader.Close();
        }

        public void DaGetCampaignDetails(MdlTrnCampaign values, string campaign_gid)
        {

            try
            {
                msSQL = " select c.campaign_gid,a.campaigndtl_gid,a.questionnarie_gid, " +
                    "campaign_code,d.campaigntype_name,c.start_date,c.end_date,c.assesment_date,e.customer_name," +
                    "c.contact_name, c.contact_mobile, c.contact_email," +
                        " c.campaign_name,a.questionnarie_type,a.questionnarie_answer, " +
                        " a.questionnarie_name,c.campaign_status from fnd_trn_tcampaigndtl a " +
                        " left join fnd_mst_tquestionnarie b on a.questionnarie_gid = b.questionnarie_gid " +
                        " left join fnd_trn_tcampaign c on c.campaign_gid = a.campaign_gid " +
                        " left join fnd_mst_tcampaigntype d on d.campaigntype_gid = c.campaigntype_gid " +
                         " left join fnd_mst_tcustomer e on e.customer_gid = c.campaigntype_gid " +
                        " where a.campaign_gid='" + campaign_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcampaign_details = new List<campaign_details>();

                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {


                        var getanswerdesc_details = new List<answerdesc_list>();
                        if ((dr_datarow["questionnarie_type"].ToString() == "List"))
                        {
                            string answerdesc_list = dr_datarow["questionnarie_answer"].ToString();
                            string[] ansdesc_sList = answerdesc_list.Split(',');

                            foreach (string author in ansdesc_sList)
                            {
                                getanswerdesc_details.Add(new answerdesc_list
                                {
                                    name = author,

                                });
                            }

                        }
                        if ((dr_datarow["questionnarie_type"].ToString() == "Radio_Button"))
                        {
                            string answerdesc_list = dr_datarow["questionnarie_answer"].ToString();
                            string[] ansdesc_sList = answerdesc_list.Split(',');

                            foreach (string author in ansdesc_sList)
                            {
                                getanswerdesc_details.Add(new answerdesc_list
                                {
                                    id = (dr_datarow["questionnarie_gid"].ToString()),
                                    name = author,

                                });
                            }

                        }
                        getcampaign_details.Add(new campaign_details
                        {
                            campaign_gid = (dr_datarow["campaign_gid"].ToString()),
                            campaigndtl_gid = (dr_datarow["campaigndtl_gid"].ToString()),
                            questionnarie_gid = (dr_datarow["questionnarie_gid"].ToString()),
                            answer_type = (dr_datarow["questionnarie_type"].ToString()),
                            answer_desc = (dr_datarow["questionnarie_answer"].ToString()),
                            campaign_name = (dr_datarow["campaign_name"].ToString()),
                            question = (dr_datarow["questionnarie_name"].ToString()),
                            campaignrefno = dr_datarow["campaign_code"].ToString(),
                            campaign_type = dr_datarow["campaigntype_name"].ToString(),
                            customer_name = dr_datarow["customer_name"].ToString(),
                            contactperson_fn = dr_datarow["contact_name"].ToString(),
                            contactperson_mobile = dr_datarow["contact_mobile"].ToString(),
                            contactperson_email = dr_datarow["contact_email"].ToString(),
                            start_date = dr_datarow["start_date"].ToString(),
                            end_date = dr_datarow["end_date"].ToString(),
                            assesment_date = dr_datarow["assesment_date"].ToString(),
                            answerdesc_list = getanswerdesc_details,
                        });

                    }
                    values.campaign_details = getcampaign_details;




                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch (Exception ex)
            {
                values.status = false;
            }
        }
        public void DaGetEmployeelist(MdlTrnCampaign objmaster)
        {
            try
            {
                msSQL = " SELECT a.user_firstname,a.user_gid ,concat(a.user_firstname,' ',a.user_lastname,' / ',a.user_code) as employee_name,b.employee_gid from adm_mst_tuser a " +
                   " LEFT JOIN hrm_mst_temployee b ON a.user_gid=b.user_gid " +
                   " where user_status<>'N' order by a.user_firstname asc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_assignee = new List<assigneelist>();
                if (dt_datatable.Rows.Count != 0)
                {
                    objmaster.assigneelist = dt_datatable.AsEnumerable().Select(row =>
                      new assigneelist
                      {
                          employee_gid = row["employee_gid"].ToString(),
                          employee_name = row["employee_name"].ToString()
                      }
                    ).ToList();
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch (Exception ex)
            {
                objmaster.status = false;
            }
        }
        public void DaGetQuestionCategory(MdlTrnCampaign objcategory, string employee_gid)
        {
            try
            {


                msSQL = " SELECT categorytype_gid,categorytype_name FROM fnd_mst_tcategorytype ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcategorytype_list = new List<category_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcategorytype_list.Add(new category_list
                        {
                            categorytype_gid = (dr_datarow["categorytype_gid"].ToString()),
                            categorytype_name = (dr_datarow["categorytype_name"].ToString()),
                        });
                    }
                    objcategory.category_list = getcategorytype_list;
                }
                dt_datatable.Dispose();

                objcategory.status = true;
            }
            catch
            {
                objcategory.status = false;
            }

        }
        public void DaGetQuestionnarie(MdlTrnCampaign objcampaigntype, string categorytype_gid)
        {
            try
            {


                msSQL = " SELECT questionnarie_gid,questionnarie_name FROM fnd_mst_tquestionnarie  " +
                        " where categorytype_gid = '" + categorytype_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getquestionnarie_list = new List<questionnarie_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getquestionnarie_list.Add(new questionnarie_list
                        {
                            questionnarie_gid = (dr_datarow["questionnarie_gid"].ToString()),
                            questionnarie_name = (dr_datarow["questionnarie_name"].ToString()),
                        });
                    }
                    objcampaigntype.questionnarie_list = getquestionnarie_list;
                }
                dt_datatable.Dispose();

                objcampaigntype.status = true;
            }
            catch
            {
                objcampaigntype.status = false;
            }

        }

        public void DaGetCampaignQuestionnarie(MdlTrnCampaign objcampaigntype, string categorytype_gid,string campaign_gid)
        {
            try
            {


             msSQL = " select questionnarie_name, questionnarie_gid, categorytype_gid from fnd_mst_tquestionnarie " +
                     " where (categorytype_gid = '" + categorytype_gid + "' )and( questionnarie_gid not in (select questionnarie_gid from fnd_trn_tcampaigndtl  where campaign_gid = '" + campaign_gid + "' ))";


                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getquestionnarie_list = new List<questionnarie_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getquestionnarie_list.Add(new questionnarie_list
                        {
                            questionnarie_gid = (dr_datarow["questionnarie_gid"].ToString()),
                            questionnarie_name = (dr_datarow["questionnarie_name"].ToString()),
                        });
                    }
                    objcampaigntype.questionnarie_list = getquestionnarie_list;
                }
                dt_datatable.Dispose();

                objcampaigntype.status = true;
            }
            catch
            {
                objcampaigntype.status = false;
            }

        }



        public void DaGetMultipleListQuestionnarie(MdlTrnCampaign objcampaigntype, string categorytype_gid)
        {
            try
            {


                msSQL = " SELECT questionnarie_gid,questionnarie_name FROM fnd_mst_tquestionnarie  " +
                        " where categorytype_gid = '" + categorytype_gid + "' and questionnarie_type not in ('List','Radio_Button','Radio Button')";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmultiplequestionnarie_list = new List<multiplequestionnarie_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmultiplequestionnarie_list.Add(new multiplequestionnarie_list
                        {
                            questionnarie_gid = (dr_datarow["questionnarie_gid"].ToString()),
                            questionnarie_name = (dr_datarow["questionnarie_name"].ToString()),
                        });
                    }
                    objcampaigntype.multiplequestionnarie_list = getmultiplequestionnarie_list;
                }
                dt_datatable.Dispose();

                objcampaigntype.status = true;
            }
            catch
            {
                objcampaigntype.status = false;
            }

        }
        public void DaGetMultipleSelectListQuestionnarie(MdlTrnCampaign objcampaigntype, string categorytype_gid, string campaign_gid)
        {
            {
                try
                {


                    msSQL = " SELECT questionnarie_gid,questionnarie_name FROM fnd_mst_tquestionnarie  " +
                            " where categorytype_gid = '" + categorytype_gid + "' and questionnarie_type not in ('List','Radio_Button','Radio Button') and " +
                            " ( questionnarie_gid not in (select questionnarie_gid from fnd_trn_tcampaigndtl  where campaign_gid = '" + campaign_gid + "' ))";


                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getmultiplequestionnarie_list = new List<multiplequestionnarie_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dr_datarow in dt_datatable.Rows)
                        {
                            getmultiplequestionnarie_list.Add(new multiplequestionnarie_list
                            {
                                questionnarie_gid = (dr_datarow["questionnarie_gid"].ToString()),
                                questionnarie_name = (dr_datarow["questionnarie_name"].ToString()),
                            });
                        }
                        objcampaigntype.multiplequestionnarie_list = getmultiplequestionnarie_list;
                    }
                    dt_datatable.Dispose();

                    objcampaigntype.status = true;
                }
                catch
                {
                    objcampaigntype.status = false;
                }

            }
        }

        public void DaGetCustomerdtl(MdlTrnCampaign objcustomer, string customer_gid)
        {
            try
            {


                msSQL = " SELECT a.customer_gid,a.customer_name,a.contactperson_fn, " +
                        " b.email_address,c.mobile_no FROM fnd_mst_tcustomer a " +
                        " inner join fnd_mst_tcustomer2emailaddress b on a.customer_gid = b.customer_gid " +
                        " inner join fnd_mst_tcustomer2mobileno c on a.customer_gid = c.customer_gid " +
                        " where a.customer_gid = '" + customer_gid + "' LIMIT 1";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objcustomer.customer_gid = objODBCDatareader["customer_gid"].ToString();
                    objcustomer.contactperson_fn = objODBCDatareader["contactperson_fn"].ToString();
                    objcustomer.email_address = objODBCDatareader["email_address"].ToString();
                    objcustomer.mobile_no = objODBCDatareader["mobile_no"].ToString();


                }

                objcustomer.status = true;
                objcustomer.message = "success";
                objODBCDatareader.Close();

            }
            catch
            {
                objcustomer.status = false;
            }

        }
        public bool DaPostSingleform(MdlTrnCampaign values, string employee_gid)
        {
            string importance = "";
            if (values.campaign_gid == null)
            {
                msSQL = "select campaigndtl_gid,campaign_gid from fnd_trn_tcampaigndtl where status = 'Y' and created_by = '" + employee_gid + "' ";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.campaign_gid = objODBCDatareader["campaign_gid"].ToString();

                }
                else
                {
                    values.campaign_gid = objcmnfunctions.GetMasterGID("FCCC");
                }
            }

            msSQL = "select questionnarie_type,questionnarie_answer,importance from fnd_mst_tquestionnarie where questionnarie_gid = '" + values.questionnarie_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.questionnarie_type = objODBCDatareader["questionnarie_type"].ToString();
                values.questionnarie_answer = objODBCDatareader["questionnarie_answer"].ToString();
                importance = objODBCDatareader["importance"].ToString();


            }
            objODBCDatareader.Close();
            msGetGid = objcmnfunctions.GetMasterGID("FCSF");
            msSQL = " insert into fnd_trn_tcampaigndtl(" +

                    " campaigndtl_gid," +
                    " campaign_gid," +
                    " questionnarie_gid," +
                    " questionnarie_name," +
                    " questionnarie_type," +
                    " questionnarie_answer, " +
                    " display_order," +
                    " status, " +
                     " form_type, " +
                    " created_by, " +
                    " categorytype_gid ," +
                    " importance " +
                    " )" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.campaign_gid + "'," +
                    "'" + values.questionnarie_gid + "'," +
                    "'" + values.questionnarie_name + "'," +
                    "'" + values.questionnarie_type + "'," +
                    "'" + values.questionnarie_answer + "'," +
                    "'" + values.display_order + "'," +
                    "'Y', " +
                      "'S', " +
                    "'" + employee_gid + "'," +
                    "'" + values.category_gid + "'," +
                     "'" + importance + "'" +
                    " )";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                values.status = true;
                values.message = "Single form Questionnarie Added Successfully";
                values.campaign_gid = values.campaign_gid;
                objdbconn.CloseConn();
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured While Adding Answer description";
                objdbconn.CloseConn();
                return false;
            }
        }
        public bool DaPostSingleformEdit(MdlTrnCampaign values, string employee_gid)
        {
            string importance = "";
            if (values.campaign_gid == null)
            {
                msSQL = "select campaigndtl_gid,campaign_gid from fnd_trn_tcampaigndtl where status = 'Y' and created_by = '" + employee_gid + "' ";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.campaign_gid = objODBCDatareader["campaign_gid"].ToString();

                }
                else
                {
                    values.campaign_gid = objcmnfunctions.GetMasterGID("FCCC");
                }
            }

            msSQL = "select questionnarie_type,questionnarie_answer,importance from fnd_mst_tquestionnarie where questionnarie_gid = '" + values.questionnarie_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.questionnarie_type = objODBCDatareader["questionnarie_type"].ToString();
                values.questionnarie_answer = objODBCDatareader["questionnarie_answer"].ToString();
                importance = objODBCDatareader["importance"].ToString();


            }
            objODBCDatareader.Close();
            msGetGid = objcmnfunctions.GetMasterGID("FCSF");
            msSQL = " insert into fnd_trn_tcampaigndtl(" +

                    " campaigndtl_gid," +
                    " campaign_gid," +
                    " questionnarie_gid," +
                    " questionnarie_name," +
                    " questionnarie_type," +
                    " questionnarie_answer, " +
                    " display_order," +
                    " status, " +
                     " form_type, " +
                    " created_by, " +
                    " categorytype_gid ," +
                    " importance " +
                    " )" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.campaign_gid + "'," +
                    "'" + values.questionnarie_gid + "'," +
                    "'" + values.questionnarie_name + "'," +
                    "'" + values.questionnarie_type + "'," +
                    "'" + values.questionnarie_answer + "'," +
                    "'" + values.display_order + "'," +
                    "'N', " +
                      "'S', " +
                    "'" + employee_gid + "'," +
                    "'" + values.category_gid + "'," +
                     "'" + importance + "'" +
                    " )";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                values.status = true;
                values.message = "Single form Questionnarie Added Successfully";
                values.campaign_gid = values.campaign_gid;
                objdbconn.CloseConn();
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured While Adding Answer description";
                objdbconn.CloseConn();
                return false;
            }
        }
        public void DaGetSingleform(MdlTrnCampaign values, string employee_gid, string campaign_gid)
        {


            msSQL = "select a.campaigndtl_gid,  a.questionnarie_gid,a.categorytype_gid, a.questionnarie_type, " +
                     " a.questionnarie_answer,a.questionnarie_name,a.display_order,a.form_type, " +
                    " b.categorytype_name from fnd_trn_tcampaigndtl a " +
                    " left join fnd_mst_tcategorytype b on a.categorytype_gid = b.categorytype_gid " +
                    "where a.created_by = '" + employee_gid + "' and a.status = 'Y' and a.form_type = 'S'" +
                    " and a.campaign_gid = '" + campaign_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var singleform_list = new List<singleform_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    singleform_list.Add(new singleform_list
                    {
                        questionnarie_gid = (dr_datarow["campaigndtl_gid"].ToString()),
                        questionnarie_type = (dr_datarow["categorytype_name"].ToString()),
                        questionnarie_answer = (dr_datarow["questionnarie_name"].ToString()),
                        categorytype_gid = (dr_datarow["categorytype_gid"].ToString()),
                    });
                }
                values.singleform_list = singleform_list;
            }
            dt_datatable.Dispose();
        }
        public void DaGetCampaignSingleform(MdlTrnCampaign values,string category_gid)
        {


            msSQL = " select questionnarie_name from fnd_mst_tquestionnarie  " +
                    " where categorytype_gid = '" + category_gid + "'  and questionnarie_gid not in(select questionnarie_gid from fnd_trn_tcampaigndtl where category_gid = '" + category_gid + "')";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcampaignsingleform_list = new List<campaignsingleform_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcampaignsingleform_list.Add(new campaignsingleform_list
                    {                    
                        questionnarie_answer = (dr_datarow["questionnarie_name"].ToString()),
                    });
                }
                values.campaignsingleform_list = getcampaignsingleform_list;
            }
            dt_datatable.Dispose();
        }




        public void DaGetSingleformEdit(MdlTrnCampaign values, string employee_gid, string campaign_gid)
        {


            msSQL = "select a.campaigndtl_gid,  a.questionnarie_gid, a.questionnarie_type, " +
                     " a.questionnarie_answer,a.questionnarie_name,a.display_order,a.form_type, " +
                    " b.categorytype_name from fnd_trn_tcampaigndtl a " +
                    " left join fnd_mst_tcategorytype b on a.categorytype_gid = b.categorytype_gid " +
                    " left join fnd_trn_tcampaign c on c.campaign_gid = a.campaign_gid " +
                    "where  a.form_type = 'S'" +
                    " and a.campaign_gid = '" + campaign_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var singleform_list = new List<singleform_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    singleform_list.Add(new singleform_list
                    {
                        questionnarie_gid = (dr_datarow["campaigndtl_gid"].ToString()),
                        questionnarie_type = (dr_datarow["categorytype_name"].ToString()),
                        questionnarie_answer = (dr_datarow["questionnarie_name"].ToString()),

                    });
                }
                values.singleform_list = singleform_list;
            }
            dt_datatable.Dispose();
        }


        public void DaDeleteSingleform(string campaigndtl_gid, MdlTrnCampaign values)
        {
            msSQL = "select campaign_gid  from fnd_trn_tcampaigndtl where campaigndtl_gid='" + campaigndtl_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.campaign_gid = objODBCDatareader["campaign_gid"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = "delete from fnd_trn_tcampaigndtl where campaigndtl_gid='" + campaigndtl_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "Single Form Questionnarie deleted successfully";
                values.status = true;
                values.campaign_gid = values.campaign_gid;
            }
            else
            {
                values.message = "Error Occured While Deleting the Single Form Questionnarie";
                values.status = false;

            }
        }
        public void DaDeleteMultipleform(string campaigndtl_gid, MdlTrnCampaign values)
        {
            msSQL = "select campaign_gid  from fnd_trn_tcampaigndtl where campaigndtl_gid='" + campaigndtl_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.campaign_gid = objODBCDatareader["campaign_gid"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = "delete from fnd_trn_tcampaigndtl where campaigndtl_gid='" + campaigndtl_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "Multiple Form Questionnarie deleted successfully";
                values.status = true;
                values.campaign_gid = values.campaign_gid;
            }
            else
            {
                values.message = "Error Occured While Deleting the Single Form Questionnarie";
                values.status = false;

            }
        }
        public bool DaPostMultipleformEdit(MdlTrnCampaign values, string employee_gid)
        {
            string importance = "";
            if (values.campaign_gid == null)
            {
                msSQL = "select campaigndtl_gid,campaign_gid from fnd_trn_tcampaigndtl where status = 'Y' and created_by = '" + employee_gid + "' ";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.campaign_gid = objODBCDatareader["campaign_gid"].ToString();

                }
                else
                {
                    values.campaign_gid = objcmnfunctions.GetMasterGID("FCCC");
                }
            }




            msSQL = "select questionnarie_type,questionnarie_answer,importance from fnd_mst_tquestionnarie where questionnarie_gid = '" + values.questionnarie_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.questionnarie_type = objODBCDatareader["questionnarie_type"].ToString();
                values.questionnarie_answer = objODBCDatareader["questionnarie_answer"].ToString();
                importance = objODBCDatareader["importance"].ToString();
            }

            objODBCDatareader.Close();
            msGetGid = objcmnfunctions.GetMasterGID("FCSF");
            msSQL = " insert into fnd_trn_tcampaigndtl(" +

                    " campaigndtl_gid," +
                    " campaign_gid," +
                    " questionnarie_gid," +
                    " questionnarie_name," +
                    " questionnarie_type," +
                    " questionnarie_answer, " +
                    " display_order," +
                    " status, " +
                     " form_type, " +
                    " created_by, " +
                    " categorytype_gid  ," +
                    " importance " +
                    " )" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.campaign_gid + "'," +
                    "'" + values.questionnarie_gid + "'," +
                    "'" + values.questionnarie_name + "'," +
                    "'" + values.questionnarie_type + "'," +
                    "'" + values.questionnarie_answer + "'," +
                    "'" + values.display_order + "'," +
                    "'N', " +
                      "'M', " +
                    "'" + employee_gid + "'," +
                    "'" + values.category_gid + "'," +
                     "'" + importance + "'" +
                    " )";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                values.status = true;
                values.message = "Multiple form Questionnarie Added Successfully";
                values.campaign_gid = values.campaign_gid;
                objdbconn.CloseConn();
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured While Adding Multiple form";
                objdbconn.CloseConn();
                return false;
            }
        }
        public bool DaPostMultipleform(MdlTrnCampaign values, string employee_gid)
        {
            string importance = "";
            if (values.campaign_gid == null)
            {
                msSQL = "select campaigndtl_gid,campaign_gid from fnd_trn_tcampaigndtl where status = 'Y' and created_by = '" + employee_gid + "' ";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.campaign_gid = objODBCDatareader["campaign_gid"].ToString();

                }
                else
                {
                    values.campaign_gid = objcmnfunctions.GetMasterGID("FCCC");
                }
            }




            msSQL = "select questionnarie_type,questionnarie_answer,importance from fnd_mst_tquestionnarie where questionnarie_gid = '" + values.questionnarie_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.questionnarie_type = objODBCDatareader["questionnarie_type"].ToString();
                values.questionnarie_answer = objODBCDatareader["questionnarie_answer"].ToString();
                importance = objODBCDatareader["importance"].ToString();
            }

            objODBCDatareader.Close();
            msGetGid = objcmnfunctions.GetMasterGID("FCSF");
            msSQL = " insert into fnd_trn_tcampaigndtl(" +

                    " campaigndtl_gid," +
                    " campaign_gid," +
                    " questionnarie_gid," +
                    " questionnarie_name," +
                    " questionnarie_type," +
                    " questionnarie_answer, " +
                    " display_order," +
                    " status, " +
                     " form_type, " +
                    " created_by, " +
                    " categorytype_gid  ," +
                    " importance " +
                    " )" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.campaign_gid + "'," +
                    "'" + values.questionnarie_gid + "'," +
                    "'" + values.questionnarie_name + "'," +
                    "'" + values.questionnarie_type + "'," +
                    "'" + values.questionnarie_answer + "'," +
                    "'" + values.display_order + "'," +
                    "'Y', " +
                      "'M', " +
                    "'" + employee_gid + "'," +
                    "'" + values.category_gid + "'," +
                     "'" + importance + "'" +
                    " )";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                values.status = true;
                values.message = "Multiple form Questionnarie Added Successfully";
                values.campaign_gid = values.campaign_gid;
                objdbconn.CloseConn();
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured While Adding Multiple form";
                objdbconn.CloseConn();
                return false;
            }
        }
        public bool DaCampaignSave(string employee_gid, MdlTrnCampaign values)
        {
            string msGetcampaignapprovingemployee_gid;
            if (values.campaign_gid != null)
            {
                msGetGid = values.campaign_gid;


            }
            else
            {
                msGetGid = objcmnfunctions.GetMasterGID("FCCC");
            }
            //msGetGid = objcmnfunctions.GetMasterGID("FCCC");
            msGetGidREF = objcmnfunctions.GetMasterGID("FDCC_");

            msSQL = " insert into fnd_trn_tcampaign ( " +
                  " campaign_gid, campaign_code," +
                  " campaign_name,campaigntype_gid," +
                  " contact_name, contact_mobile," +
                  " contact_email, campaign_manager," +
                  " campaign_approver,campaign_cost, " +
                  " start_date, end_date," +
                  " assesment_date," +
                  " os_assesment_date, loan_availed," +
                  " status_flag, created_by," +
                  " created_date ,customer_gid" +
                  "  )" +
                  " values(" +
                  "'" + msGetGid + "'," +
                  "'" + msGetGidREF + "'," +
                  "'" + values.campaign_name + "'," +
                  "'" + values.campaign_type + "'," +
                  "'" + values.contactperson_fn + "'," +
                  "'" + values.contactperson_mobile + "'," +
                  "'" + values.contactperson_email + "'," +
                  "'" + values.campaign_mgr + "'," +
                  "'" + values.campaign_apr + "'," +
                  "'" + values.campaign_cost + "',";


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
            if ((values.assesment_date == null) || (values.assesment_date == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.assesment_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }

            msSQL += "'" + values.osAssesment_date + "'," +
                    "'" + values.loan_availed + "'," +
                    "'N'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                    "'" + values.customer_gid + "')";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            //if (values.assignee == null )
            //{

            //    for (var i = 0; i == values.assignee.Count;)

            //        values.assignee[i].employee_gid = "";

            //}
            //else if (values.assignee == null)
            //{
            //    for (var i = 0; i == values.assignee.Count;)
            //        values.assignee[i].employee_name = "";
            //}

            //else
            //{

            if (values.assignee == null)
            {
                msSQL += "null,";
            }
            else
            {

                for (var i = 0; i < values.assignee.Count; i++)
                {
                    msGetcampaignapprovingemployee_gid = objcmnfunctions.GetMasterGID("FD2E");
                    msSQL = "Insert into fnd_mst_tcampaignapproving2employee( " +
                           " campaignapproving2employee_gid, " +
                           " campaign_gid," +
                           " employee_gid," +
                           " employee_name," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetcampaignapprovingemployee_gid + "'," +
                           "'" + msGetGid + "'," +
                           "'" + values.assignee[i].employee_gid + "'," +
                           "'" + values.assignee[i].employee_name + "'," +
                                "'" + employee_gid + "'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

            }

            if (mnResult != 0)
            {


                values.status = true;
                values.message = "Campaign Saved Successfully";

                msSQL = "update fnd_trn_tcampaigndtl set status = 'N' where created_by='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured While Saving Campaign";
                return false;
            }

        }
        public void DaCampaignSubmit(string employee_gid, MdlTrnCampaign values)
        {
            string msGetcampaignapprovingemployee_gid;
            if (values.campaign_gid != null)
            {
                msGetGid = values.campaign_gid;
            }
            else
            {
                msGetGid = objcmnfunctions.GetMasterGID("FCCC");
            }

            msGetGidREF = objcmnfunctions.GetMasterGID("FDCC_");

            msSQL = " insert into fnd_trn_tcampaign ( " +
                   " campaign_gid, campaign_code," +
                   " campaign_name,campaigntype_gid," +
                   " contact_name, contact_mobile," +
                   " contact_email, campaign_manager," +
                   " campaign_approver,campaign_cost, " +
                   " start_date, end_date," +
                   " assesment_date," +
                   " os_assesment_date, loan_availed," +
                   " status_flag, created_by," +
                   " created_date ,customer_gid" +
                   "  )" +
                   " values(" +
                   "'" + msGetGid + "'," +
                   "'" + msGetGidREF + "'," +
                   "'" + values.campaign_name + "'," +
                   "'" + values.campaign_type + "'," +
                   "'" + values.contactperson_fn + "'," +
                   "'" + values.contactperson_mobile + "'," +
                   "'" + values.contactperson_email + "'," +
                   "'" + values.campaign_mgr + "'," +
                   "'" + values.campaign_apr + "'," +
                   "'" + values.campaign_cost + "',";


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
            if ((values.assesment_date == null) || (values.assesment_date == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.assesment_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }

            msSQL += "'" + values.osAssesment_date + "'," +
                    "'" + values.loan_availed + "'," +
                    "'Completed'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                    "'" + values.customer_gid + "')";

            lscreated_by = employee_gid;
            //lscampaign_type = values.campaign_type;
            lscampaign_name = values.campaign_name;
            lsTo2members = values.campaign_apr;

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " update fnd_trn_tcampaign set campaign_status ='Pending'  where campaign_gid='" + values.campaign_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);



            for (var i = 0; i < values.assignee.Count; i++)
            {
                msGetcampaignapprovingemployee_gid = objcmnfunctions.GetMasterGID("FD2E");
                msSQL = "Insert into fnd_mst_tcampaignapproving2employee( " +
                       " campaignapproving2employee_gid, " +
                       " campaign_gid," +
                       " employee_gid," +
                       " employee_name," +
                       " created_by," +
                       " created_date)" +
                       " values(" +
                       "'" + msGetcampaignapprovingemployee_gid + "'," +
                       "'" + msGetGid + "'," +
                       "'" + values.assignee[i].employee_gid + "'," +
                       "'" + values.assignee[i].employee_name + "'," +
                       "'" + employee_gid + "'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }



            if (mnResult != 0)
            {


                values.status = true;
                values.message = "Campaign Created Successfully";
                try
                {
                    k = 1;

                    msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        ls_server = objODBCDatareader["pop_server"].ToString();
                        ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                        ls_username = objODBCDatareader["pop_username"].ToString();
                        ls_password = objODBCDatareader["pop_password"].ToString();
                    }
                    objODBCDatareader.Close();

                    msSQL = " select  group_concat(distinct a.employee_gid)  as CC2members, h.campaigntype_name, concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as campaign_apr, d.created_date, concat(i.user_firstname,' ',i.user_lastname,' / ',i.user_code) as employee_name  from fnd_mst_tcampaignapproving2employee a" +
                            " left join fnd_trn_tcampaign d on a.campaign_gid = d.campaign_gid" +
                            " left join fnd_mst_tcampaigntype h on h.campaigntype_gid = d.campaigntype_gid" +
                            " left join hrm_mst_temployee b on d.campaign_approver = b.employee_gid" +
                            " left join hrm_mst_temployee f on a.created_by = f.employee_gid" +
                            " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                            " left join adm_mst_tuser i on i.user_gid = f.user_gid  " +
                        " where a.campaign_gid ='" + values.campaign_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lsname = objODBCDatareader["employee_name"].ToString();
                        lsemployee_name = objODBCDatareader["campaign_apr"].ToString();
                        lscampaign_type = objODBCDatareader["campaigntype_name"].ToString();
                        lscc2members = objODBCDatareader["CC2members"].ToString();
                        //lsTo2members = objODBCDatareader["To2members"].ToString();
                        lscreated_date = objODBCDatareader["created_date"].ToString();
                    }
                    objODBCDatareader.Close();

                    string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
                    sToken = "";
                    int Length = 100;
                    for (int j = 0; j < Length; j++)
                    {
                        string sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                        sToken += sTempChars;
                    }

                    k = k + 1;


                    msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee " +
                            " where employee_gid in ('" + lsTo2members.Replace(",", "', '") + "')";
                    lsto_mail = objdbconn.GetExecuteScalar(msSQL);


                    msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                    cc_mailid = objdbconn.GetExecuteScalar(msSQL);



                    sub = "RE: Campaign Creation ";
                    body = "Dear All,<br />";
                    body = body + "<br />";
                    body = body + "Greetings,  <br />";
                    body = body + "<br />";
                    body = body + "A Campaign has been created for the Approval. The details are as follows, <br />";
                    body = body + "<br />";
                    body = body + "<b> Campaign Name :</b> " + HttpUtility.HtmlEncode(lscampaign_name) + "<br />";
                    body = body + "<br />";
                    body = body + "<b> Campaign Type :</b> " + HttpUtility.HtmlEncode(lscampaign_type) + "<br />";
                    //body = body + "<br />";
                    //body = body + "<b>Audit Department :</b> " + lsauditdepartment_name + "<br />";
                    //body = body + "<br />";
                    //body = body + "<b>Checkpoint Group :</b> " + lscheckpointgroup_name + "<br />";
                    body = body + "<br />";
                    body = body + "Kindly log into systems to view more details.";
                    body = body + "<br />";
                    body = body + "<br />";
                    body = body + "Thanks & Regards, ";
                    body = body + "<br />";
                    body = body + HttpUtility.HtmlEncode(lsname);
                    body = body + "<br />";
                    body = body + "<br />";
                    body = body + "<br />";
                    body = body + " **This is an automated e-mail. Please do not reply to this mailbox**";


                    MailMessage message = new MailMessage();
                    SmtpClient smtp = new SmtpClient();
                    message.From = new MailAddress(ls_username);
                    //message.To.Add(new MailAddress(lsto_mail));


                    lsBccmail_id = ConfigurationManager.AppSettings["Foundationbcc"].ToString();

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

                    msSQL = "update fnd_trn_tcampaigndtl set status = 'N' where created_by='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (values.status == true)
                    {
                        msSQL = "Insert into fnd_trn_tfoundationmailcount( " +
                        " campaign_gid," +
                        " from_mail," +
                        " to_mail," +
                        " cc_mail," +
                        " mail_status," +
                        " mail_senddate, " +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + values.campaign_gid + "'," +
                        "'" + lsname + "'," +
                        "'" + lsto_mail + "'," +
                        "'" + cc_mailid + "'," +
                        "'Campaign Created Successfully'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
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
                values.status = false;
                values.message = "Error Occured While Creating Campaign";
               
            }

        }

        public void DaGetCampaignApprovalpending(string employee_gid, MdlTrnCampaign values)
        {
            msSQL = " select a.campaign_gid,a.campaign_code,a.campaign_status,a.campaign_name," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                    " a.status_flag,b.customer_name ," +
                    " concat(d.user_firstname, ' ', d.user_lastname, ' / ', d.user_code) as created_by " +
                    " from fnd_trn_tcampaign a left join fnd_mst_tcustomer b on a.customer_gid = b.customer_gid " +
                  " left join hrm_mst_temployee c on a.created_by = c.employee_gid" +
                    " left join adm_mst_tuser d on c.user_gid = d.user_gid " +
                    " where a.campaign_approver='" + employee_gid + "' and a.campaign_status= 'Pending' order by a.created_date desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcampaign_list = new List<campaign_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcampaign_list.Add(new campaign_list
                    {
                        campaign_gid = (dr_datarow["campaign_gid"].ToString()),
                        campaign_code = (dr_datarow["campaign_code"].ToString()),
                        campaign_name = (dr_datarow["campaign_name"].ToString()),
                        customer_name = (dr_datarow["customer_name"].ToString()),
                        campaign_status = (dr_datarow["campaign_status"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString())

                    });
                }
                values.campaign_list = getcampaign_list;
            }
            dt_datatable.Dispose();
        }
        public void DaGetCampaignApproval(string employee_gid, MdlTrnCampaign values)
        {
            msSQL = " select a.campaign_gid,a.campaign_code,a.campaign_status,a.campaign_name," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                    " a.status_flag,b.customer_name ," +
                    " concat(d.user_firstname, ' ', d.user_lastname, ' / ', d.user_code) as created_by " +
                    " from fnd_trn_tcampaign a left join fnd_mst_tcustomer b on a.customer_gid = b.customer_gid " +
                    " left join hrm_mst_temployee c on a.created_by = c.employee_gid" +
                    " left join adm_mst_tuser d on c.user_gid = d.user_gid " +
                    " where a.campaign_approver='" + employee_gid + "' and ( a.campaign_status= 'Approved' or a.campaign_status='WIP')  order by a.created_date desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcampaign_list = new List<campaign_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcampaign_list.Add(new campaign_list
                    {
                        campaign_gid = (dr_datarow["campaign_gid"].ToString()),
                        campaign_code = (dr_datarow["campaign_code"].ToString()),
                        campaign_name = (dr_datarow["campaign_name"].ToString()),
                        customer_name = (dr_datarow["customer_name"].ToString()),
                        campaign_status = (dr_datarow["campaign_status"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString())

                    });
                }
                values.campaign_list = getcampaign_list;
            }
            dt_datatable.Dispose();
        }
        public void DaGetCampaignRejected(string employee_gid, MdlTrnCampaign values)
        {
            msSQL = " select a.campaign_gid,a.campaign_code,a.campaign_status,a.campaign_name," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                    " a.status_flag,b.customer_name ," +
                    " concat(d.user_firstname, ' ', d.user_lastname, ' / ', d.user_code) as created_by " +
                    " from fnd_trn_tcampaign a left join fnd_mst_tcustomer b on a.customer_gid = b.customer_gid " +
                    " left join hrm_mst_temployee c on a.created_by = c.employee_gid" +
                    " left join adm_mst_tuser d on c.user_gid = d.user_gid " +
                    " where a.campaign_approver='" + employee_gid + "' and a.campaign_status= 'rejected' order by created_date desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcampaign_list = new List<campaign_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcampaign_list.Add(new campaign_list
                    {
                        campaign_gid = (dr_datarow["campaign_gid"].ToString()),
                        campaign_code = (dr_datarow["campaign_code"].ToString()),
                        campaign_name = (dr_datarow["campaign_name"].ToString()),
                        customer_name = (dr_datarow["customer_name"].ToString()),
                        campaign_status = (dr_datarow["campaign_status"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString())

                    });
                }
                values.campaign_list = getcampaign_list;
            }
            dt_datatable.Dispose();
        }

        public bool DacampaignEditUpdate(MdlTrnCampaign values, string employee_gid)
        {
            string msGetcampaignapprovingemployee_gid;



            msSQL = " select date_format(start_date,'%d-%m-%Y') as start_date, " +
                    " date_format(end_date,'%d-%m-%Y') as end_date," +
                    " date_format(assesment_date, '%d-%m-%Y') as assesment_date" +
                    " from fnd_trn_tcampaign where campaign_gid='" + values.campaign_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsstartdate = objODBCDatareader["start_date"].ToString();
                lsenddate = objODBCDatareader["end_date"].ToString();
                lsassesmentdate = objODBCDatareader["assesment_date"].ToString();

            }


            msSQL = " update fnd_trn_tcampaign set " +
                    " campaign_name='" + values.campagin_name.Replace("'", "") + "'," +
                    " contact_name='" + values.contact_name + "'," +
                    " customer_gid='" + values.customer_gid + "'," +
                    " campaigntype_gid='" + values.campaigntype_gid + "'," +
                    "campaign_approver='" + values.campaign_approver + "'," +
                    "contact_mobile='" + values.contact_mobile + "'," +
                    "campaign_cost='" + values.campaign_cost.Replace("'", "") + "'," +
                    "contact_email='" + values.contact_email + "',";

            if (lsstartdate == values.start_date)
            {
            }
            else
            {
                msSQL += " start_date='" + Convert.ToDateTime(values.start_date).ToString("yyyy-MM-dd") + "',";
            }
            if (lsenddate == values.end_date)
            {
            }
            else
            {
                msSQL += " end_date='" + Convert.ToDateTime(values.end_date).ToString("yyyy-MM-dd") + "',";
            }
            if (lsassesmentdate == values.assesment_date)
            {
            }
            else
            {
                msSQL += " assesment_date='" + Convert.ToDateTime(values.assesment_date).ToString("yyyy-MM-dd") + "',";
            }

            msSQL += " os_assesment_date='" + values.osAssesment_date + "'," +
                    " loan_availed='" + values.loan_availed + "'," +
                    " status_flag='Completed'," +
                    " updated_by='" + employee_gid + "'," +
                    " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    " where campaign_gid='" + values.campaign_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " delete from fnd_mst_tcampaignapproving2employee where campaign_gid='" + values.campaign_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            for (var i = 0; i < values.assignee.Count; i++)
            {
                msGetcampaignapprovingemployee_gid = objcmnfunctions.GetMasterGID("FD2E");
                msSQL = "Insert into fnd_mst_tcampaignapproving2employee( " +
                       " campaignapproving2employee_gid, " +
                       " campaign_gid," +
                       " employee_gid," +
                       " employee_name," +
                       " created_by," +
                       " created_date)" +
                       " values(" +
                       "'" + msGetcampaignapprovingemployee_gid + "'," +
                       "'" + values.campaign_gid + "'," +
                       "'" + values.assignee[i].employee_gid + "'," +
                       "'" + values.assignee[i].employee_name + "'," +
                       "'" + employee_gid + "'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            }


            if (mnResult != 0)
            {

                msSQL = "update fnd_trn_tcampaigndtl set status = 'N' where created_by='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update fnd_trn_tcampaign set campaign_status ='Pending'  where campaign_gid='" + values.campaign_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Campaign Updated Successfully";


                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured While Creating Campaign";
                return false;
            }
        }




        public void DaPostCampaignApproved(MdlTrnCampaign values, string employee_gid)
        {
            string msGetcampaignapprovingemployee_gid;

            msSQL = " select count(campaignraisequery_gid) as openquery from fnd_trn_tcampaignraisequery where campaign_gid = '" + values.campaign_gid + "'" +
                 " and raisequery_status = 'Query Raised'";
            values.openquerycount = objdbconn.GetExecuteScalar(msSQL);
            if (values.openquerycount == "0")
            {

                msSQL = " select date_format(start_date,'%d-%m-%Y') as start_date, " +
                    " date_format(end_date,'%d-%m-%Y') as end_date," +
                    " date_format(assesment_date, '%d-%m-%Y') as assesment_date" +
                    " from fnd_trn_tcampaign where campaign_gid='" + values.campaign_gid + "' ";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lsstartdate = objODBCDatareader["start_date"].ToString();
                    lsenddate = objODBCDatareader["end_date"].ToString();
                    lsassesmentdate = objODBCDatareader["assesment_date"].ToString();

                }
                msSQL = " update fnd_trn_tcampaign set " +
                                   " campaign_name='" + values.campaign_name.Replace("'", "") + "'," +
                                   " contact_name='" + values.contact_name + "'," +
                                   " customer_gid='" + values.customer_gid + "'," +
                                   " campaigntype_gid='" + values.campaigntype_gid + "'," +
                                   "campaign_approver='" + values.campaign_approver + "'," +
                                   "contact_mobile='" + values.contact_mobile + "'," +
                                   "campaign_cost='" + values.campaign_cost.Replace("'", "") + "'," +
                                    " campaignapproval_remarks = '" + values.campaignapproval_remarks + "'," +
                                   "contact_email='" + values.contact_email + "',";

                if (lsstartdate == values.start_date)
                {
                }
                else
                {
                    msSQL += " start_date='" + Convert.ToDateTime(values.start_date).ToString("yyyy-MM-dd") + "',";
                }
                if (lsenddate == values.end_date)
                {
                }
                else
                {
                    msSQL += " end_date='" + Convert.ToDateTime(values.end_date).ToString("yyyy-MM-dd") + "',";
                }
                if (lsassesmentdate == values.assesment_date)
                {
                }
                else
                {
                    msSQL += " assesment_date='" + Convert.ToDateTime(values.assesment_date).ToString("yyyy-MM-dd") + "',";
                }

                msSQL += " os_assesment_date='" + values.osAssesment_date + "'," +
                        " loan_availed='" + values.loan_availed + "'," +
                        " status_flag='Completed'," +
                        " updated_by='" + employee_gid + "'," +
                        " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                        " where campaign_gid='" + values.campaign_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                msSQL = " delete from fnd_mst_tcampaignapproving2employee where campaign_gid='" + values.campaign_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                lscampaign_type = values.campaign_type;
                lscampaign_name = values.campaign_name;
                lscampaign_remarks = values.campaignapproval_remarks;

                for (var i = 0; i < values.assignee.Count; i++)
                {
                    msGetcampaignapprovingemployee_gid = objcmnfunctions.GetMasterGID("FD2E");
                    msSQL = "Insert into fnd_mst_tcampaignapproving2employee( " +
                           " campaignapproving2employee_gid, " +
                           " campaign_gid," +
                           " employee_gid," +
                           " employee_name," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetcampaignapprovingemployee_gid + "'," +
                           "'" + values.campaign_gid + "'," +
                           "'" + values.assignee[i].employee_gid + "'," +
                           "'" + values.assignee[i].employee_name + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                }

                msSQL = "update fnd_trn_tcampaign set campaign_status ='Approved' where campaign_gid='" + values.campaign_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Campaign Approved Successfully";

                try { 

                k = 1;

                msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    ls_server = objODBCDatareader["pop_server"].ToString();
                    ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                    ls_username = objODBCDatareader["pop_username"].ToString();
                    ls_password = objODBCDatareader["pop_password"].ToString();
                }
                objODBCDatareader.Close();

                msSQL =
                       " select  group_concat(distinct a.employee_gid)  as CC2members, d.campaign_status, d.created_by, h.campaigntype_name, concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as campaign_apr, d.created_date, concat(i.user_firstname,' ',i.user_lastname,' / ',i.user_code) as employee_name  from fnd_mst_tcampaignapproving2employee a" +
                            " left join fnd_trn_tcampaign d on a.campaign_gid = d.campaign_gid" +
                            " left join fnd_mst_tcampaigntype h on h.campaigntype_gid = d.campaigntype_gid" +
                            " left join hrm_mst_temployee b on d.campaign_approver = b.employee_gid" +
                            " left join hrm_mst_temployee f on a.created_by = f.employee_gid" +
                            " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                            " left join adm_mst_tuser i on i.user_gid = f.user_gid  " +
                        " where a.campaign_gid ='" + values.campaign_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {

                    lsemployee_name = objODBCDatareader["employee_name"].ToString();
                    lsname = objODBCDatareader["campaign_apr"].ToString();
                    lcampaign_status = objODBCDatareader["campaign_status"].ToString();
                    lscc2members = objODBCDatareader["CC2members"].ToString();
                    lscampaign_type = objODBCDatareader["campaigntype_name"].ToString();
                    lsTo2members = objODBCDatareader["created_by"].ToString();
                    lscreated_date = objODBCDatareader["created_date"].ToString();
                }
                objODBCDatareader.Close();

                string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
                sToken = "";
                int Length = 100;
                for (int j = 0; j < Length; j++)
                {
                    string sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                    sToken += sTempChars;
                }

                k = k + 1;


                msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee " +
                        " where employee_gid in ('" + lsTo2members.Replace(",", "', '") + "')";
                lsto_mail = objdbconn.GetExecuteScalar(msSQL);


                msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                cc_mailid = objdbconn.GetExecuteScalar(msSQL);



                sub = "RE: Campaign Approval ";
                body = "Dear All,<br />";
                body = body + "<br />";
                body = body + "Greetings,  <br />";
                body = body + "<br />";
                body = body + "A Campaign has been Approved. The details are as follows, <br />";
                body = body + "<br />";
                body = body + "<b> Campaign Name :</b> " + HttpUtility.HtmlEncode(lscampaign_name) + "<br />";
                body = body + "<br />";
                body = body + "<b> Campaign Type :</b> " + HttpUtility.HtmlEncode(lscampaign_type) + "<br />";
                body = body + "<br />";
                body = body + "<b>Campaign Approval Remarks :</b> " + HttpUtility.HtmlEncode(lscampaign_remarks) + "<br />";
                body = body + "<br />";
                body = body + "<b>Campaign Status :</b> " + HttpUtility.HtmlEncode(lcampaign_status) + "<br />";
                body = body + "<br />";
                body = body + "Kindly log into systems to view more details.";
                body = body + "<br />";
                body = body + "<br />";
                body = body + "Thanks & Regards, ";
                body = body + "<br />";
                body = body + HttpUtility.HtmlEncode(lsname);
                body = body + "<br />";
                body = body + "<br />";
                body = body + "<br />";
                body = body + " **This is an automated e-mail. Please do not reply to this mailbox**";


                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(ls_username);
                //message.To.Add(new MailAddress(lsto_mail));


                lsBccmail_id = ConfigurationManager.AppSettings["Foundationbcc"].ToString();

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

                    if (values.status == true)
                    {
                        msSQL = "Insert into fnd_trn_tfoundationmailcount( " +
                        " campaign_gid," +
                        " from_mail," +
                        " to_mail," +
                        " cc_mail," +
                        " mail_status," +
                        " mail_senddate, " +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + values.campaign_gid + "'," +
                        "'" + lsname + "'," +
                        "'" + lsto_mail + "'," +
                        "'" + cc_mailid + "'," +
                        "'Campaign Approved Successfully'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
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
                values.status = false;
                values.message = "Approval Can't be done,the query is still open";
               
            }


        }


        public void DaPostCampaignRejected(MdlTrnCampaign values, string employee_gid)
        {
            msSQL = " select count(campaignraisequery_gid) as openquery from fnd_trn_tcampaignraisequery where campaign_gid = '" + values.campaign_gid + "'" +
               " and raisequery_status = 'Query Raised'";
            values.openquerycount = objdbconn.GetExecuteScalar(msSQL);
            if (values.openquerycount == "0")
            {
                msSQL = " select a.campaign_gid,a.campaign_code,a.campaign_name,concat(d.user_firstname, ' ', d.user_lastname, ' / ', d.user_code) as campaign_approver," +
                         " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,a.created_by,b.customer_name,b.customer_gid,e.campaigntype_gid,e.campaigntype_name," +
                         " a.status_flag,b.customer_name ,a.contact_name,a.contact_mobile,a.contact_email,a.campaign_cost,a.start_date,a.end_date,a.assesment_date,a.os_assesment_date,a.loan_availed," +
                         " concat(d.user_firstname, ' ', d.user_lastname, ' / ', d.user_code) as created_by " +
                         " from fnd_trn_tcampaign a left join fnd_mst_tcustomer b on a.customer_gid = b.customer_gid " +
                         " left join fnd_mst_tcampaigntype e on e.campaigntype_gid = a.campaigntype_gid " +
                         " left join hrm_mst_temployee c on a.created_by = c.employee_gid" +
                         " left join adm_mst_tuser d on c.user_gid = d.user_gid " +
                         " where a.campaign_gid='" + values.campaign_gid + "' order by campaign_gid desc";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.campaign_code = objODBCDatareader["campaign_code"].ToString();
                    values.campaign_name = objODBCDatareader["campaign_name"].ToString();
                    values.created_by = objODBCDatareader["created_by"].ToString();
                    values.campaigntype_name = objODBCDatareader["campaigntype_name"].ToString();
                    values.customer_gid = objODBCDatareader["customer_gid"].ToString();
                    values.campaigntype_gid = objODBCDatareader["campaigntype_gid"].ToString();
                    values.customer_name = objODBCDatareader["customer_name"].ToString();
                    values.contact_name = objODBCDatareader["contact_name"].ToString();
                    values.campaign_approver = objODBCDatareader["campaign_approver"].ToString();
                    values.contact_mobile = objODBCDatareader["contact_mobile"].ToString();
                    values.contact_email = objODBCDatareader["contact_email"].ToString();
                    values.campaign_cost = objODBCDatareader["campaign_cost"].ToString();

                    if (objODBCDatareader["start_date"].ToString() == "")
                    {
                    }
                    else
                    {
                        values.start_date = Convert.ToDateTime(objODBCDatareader["start_date"]).ToString("dd-MM-yyyy");
                    }


                    if (objODBCDatareader["end_date"].ToString() == "")
                    {
                    }
                    else
                    {
                        values.end_date = Convert.ToDateTime(objODBCDatareader["end_date"]).ToString("dd-MM-yyyy");
                    }

                    if (objODBCDatareader["assesment_date"].ToString() == "")
                    {
                    }
                    else
                    {
                        values.assesment_date = Convert.ToDateTime(objODBCDatareader["assesment_date"]).ToString("dd-MM-yyyy");
                    }
                    values.os_assesment_date = objODBCDatareader["os_assesment_date"].ToString();
                    values.loan_availed = objODBCDatareader["loan_availed"].ToString();


                }

                objODBCDatareader.Close();
                msSQL = " update fnd_trn_tcampaign set " +
                        " campaign_code='" + values.campaign_code + "'," +
                        " campaign_name='" + values.campaign_name + "'," +
                         " contact_name='" + values.contact_name + "'," +
                          " customer_gid='" + values.customer_gid + "'," +
                         " campaigntype_gid='" + values.campaigntype_gid + "'," +
                         "campaign_approver='" + values.campaign_approver + "'," +
                        "contact_mobile='" + values.contact_mobile + "'," +
                        "campaign_cost='" + values.campaign_cost + "'," +
                         " campaignapproval_remarks = '" + values.campaignapproval_remarks + "'," +
                        "contact_email='" + values.contact_email + "',";

                if (Convert.ToDateTime(values.editstart_date).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
                {

                }
                else
                {
                    msSQL += " start_date='" + Convert.ToDateTime(values.editstart_date).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "',";
                }
                if (Convert.ToDateTime(values.editend_date).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
                {

                }
                else
                {
                    msSQL += " end_date='" + Convert.ToDateTime(values.editend_date).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "',";
                }
                if (Convert.ToDateTime(values.editassesment_date).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
                {

                }
                else
                {
                    msSQL += " assesment_date='" + Convert.ToDateTime(values.editassesment_date).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "',";
                }
                msSQL += " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where campaign_gid='" + values.campaign_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                msSQL = "update fnd_trn_tcampaign set campaign_status ='Rejected' where campaign_gid='" + values.campaign_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                values.status = true;

                try {  

                k = 1;

                msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    ls_server = objODBCDatareader["pop_server"].ToString();
                    ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                    ls_username = objODBCDatareader["pop_username"].ToString();
                    ls_password = objODBCDatareader["pop_password"].ToString();
                }
                objODBCDatareader.Close();

                msSQL =
                       " select  group_concat(distinct a.employee_gid)  as CC2members, d.campaign_status,d.campaign_name,d.campaignapproval_remarks, d.created_by, h.campaigntype_name, concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as campaign_apr, d.created_date, concat(i.user_firstname,' ',i.user_lastname,' / ',i.user_code) as employee_name  from fnd_mst_tcampaignapproving2employee a" +
                            " left join fnd_trn_tcampaign d on a.campaign_gid = d.campaign_gid" +
                            " left join fnd_mst_tcampaigntype h on h.campaigntype_gid = d.campaigntype_gid" +
                            " left join hrm_mst_temployee b on d.campaign_approver = b.employee_gid" +
                            " left join hrm_mst_temployee f on a.created_by = f.employee_gid" +
                            " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                            " left join adm_mst_tuser i on i.user_gid = f.user_gid  " +
                        " where a.campaign_gid ='" + values.campaign_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {

                    lsemployee_name = objODBCDatareader["employee_name"].ToString();
                    //lsname = objODBCDatareader["campaign_apr"].ToString();
                    lcampaign_status = objODBCDatareader["campaign_status"].ToString();
                    lscampaign_name = objODBCDatareader["campaign_name"].ToString();
                    lscampaign_remarks = objODBCDatareader["campaignapproval_remarks"].ToString();
                    lscc2members = objODBCDatareader["CC2members"].ToString();
                    lscampaign_type = objODBCDatareader["campaigntype_name"].ToString();
                    lsTo2members = objODBCDatareader["created_by"].ToString();
                    lscreated_date = objODBCDatareader["created_date"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = "select concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as employee_name from hrm_mst_temployee a " +
                        " left join adm_mst_tuser c on a.user_gid = c.user_gid " +
                        " where employee_gid = '" + employee_gid + "'";
                string employee_name = objdbconn.GetExecuteScalar(msSQL);

                string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
                sToken = "";
                int Length = 100;
                for (int j = 0; j < Length; j++)
                {
                    string sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                    sToken += sTempChars;
                }

                k = k + 1;


                msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee " +
                        " where employee_gid in ('" + lsTo2members.Replace(",", "', '") + "')";
                lsto_mail = objdbconn.GetExecuteScalar(msSQL);


                msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                cc_mailid = objdbconn.GetExecuteScalar(msSQL);



                sub = "RE: Campaign Rejected ";
                body = "Dear All,<br />";
                body = body + "<br />";
                body = body + "Greetings,  <br />";
                body = body + "<br />";
                body = body + "A Campaign has been Rejected. The details are as follows, <br />";
                body = body + "<br />";
                body = body + "<b> Campaign Name :</b> " + HttpUtility.HtmlEncode(lscampaign_name) + "<br />";
                body = body + "<br />";
                body = body + "<b> Campaign Type :</b> " + HttpUtility.HtmlEncode(lscampaign_type) + "<br />";
                body = body + "<br />";
                body = body + "<b>Campaign Approval Remarks :</b> " + HttpUtility.HtmlEncode(lscampaign_remarks) + "<br />";
                body = body + "<br />";
                body = body + "<b>Campaign Status :</b> " + HttpUtility.HtmlEncode(lcampaign_status) + "<br />";
                body = body + "<br />";
                body = body + "Kindly log into systems to view more details.";
                body = body + "<br />";
                body = body + "<br />";
                body = body + "Thanks & Regards, ";
                body = body + "<br />";
                body = body + HttpUtility.HtmlEncode(employee_name);
                body = body + "<br />";
                body = body + "<br />";
                body = body + "<br />";
                body = body + " **This is an automated e-mail. Please do not reply to this mailbox**";


                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(ls_username);
                //message.To.Add(new MailAddress(lsto_mail));


                lsBccmail_id = ConfigurationManager.AppSettings["Foundationbcc"].ToString();

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
                if (values.status == true)
                {
                    msSQL = "Insert into fnd_trn_tfoundationmailcount( " +
                    " campaign_gid," +
                    " from_mail," +
                    " to_mail," +
                    " cc_mail," +
                    " mail_status," +
                    " mail_senddate, " +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + values.campaign_gid + "'," +
                    "'" + employee_name + "'," +
                    "'" + lsto_mail + "'," +
                    "'" + cc_mailid + "'," +
                    "'Campaign Rejected Successfully'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
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
                values.status = false;
                values.message = "Reject Can't be done,the query is still open";
              
            }
        }


        public void DaGetMultipleform(MdlTrnCampaign values, string employee_gid, string campaign_gid)
        {
            msSQL = "select a.campaigndtl_gid,  a.questionnarie_gid, a.questionnarie_type, " +
            " a.questionnarie_answer,a.questionnarie_name,a.display_order,a.form_type, " +
           " b.categorytype_name from fnd_trn_tcampaigndtl a " +
           " left join fnd_mst_tcategorytype b on a.categorytype_gid = b.categorytype_gid " +
           "where a.status = 'Y' and a.form_type = 'M'" +
           " and a.campaign_gid = '" + campaign_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var multipleform_list = new List<multipleform_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    multipleform_list.Add(new multipleform_list
                    {
                        questionnarie_gid = (dr_datarow["campaigndtl_gid"].ToString()),
                        questionnarie_type = (dr_datarow["categorytype_name"].ToString()),
                        questionnarie_answer = (dr_datarow["questionnarie_name"].ToString()),

                    });
                }
                values.multipleform_list = multipleform_list;
            }
            dt_datatable.Dispose();
        }




        public void DaGetMultipleformEdit(MdlTrnCampaign values, string employee_gid, string campaign_gid)
        {
            msSQL = "select a.campaigndtl_gid,  a.questionnarie_gid, a.questionnarie_type, " +
            " a.questionnarie_answer,a.questionnarie_name,a.display_order,a.form_type, " +
           " b.categorytype_name from fnd_trn_tcampaigndtl a " +
           " left join fnd_mst_tcategorytype b on a.categorytype_gid = b.categorytype_gid " +
           " left join fnd_trn_tcampaign c on c.campaign_gid = a.campaign_gid " +
           "where  a.form_type = 'M'" +
           " and a.campaign_gid = '" + campaign_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var multipleform_list = new List<multipleform_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    multipleform_list.Add(new multipleform_list
                    {
                        questionnarie_gid = (dr_datarow["campaigndtl_gid"].ToString()),
                        questionnarie_type = (dr_datarow["categorytype_name"].ToString()),
                        questionnarie_answer = (dr_datarow["questionnarie_name"].ToString()),

                    });
                }
                values.multipleform_list = multipleform_list;
            }
            dt_datatable.Dispose();
        }
        public void DaDeleteCampaign(string campaign_gid, string employee_gid, MdlFndMstCampaignTypeMaster values)
        {
            msSQL = "select campaign_gid from fnd_trn_tcampaign where campaign_gid='" + campaign_gid + "' and (campaign_status = 'pending' or status_flag = 'pending 'or status_flag = 'N')";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = " Can't be deleted, this Campaign is already assigned";
            }
            else
            {

                string lscampaign_value;
                msSQL = " select campaign_name from fnd_trn_tcampaign where campaign_gid='" + campaign_gid + "'";
                lscampaign_value = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " delete from fnd_trn_tcampaign where campaign_gid='" + campaign_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Campaign Deleted Successfully..!";
                    msGetGid = objcmnfunctions.GetMasterGID("FDDL");
                    msSQL = " insert into fnd_mst_tcampaigntypedeletelog(" +
                             "campaigntypedeletelog_gid, " +
                             "campaigntype_gid, " +
                             "master_name, " +
                             "master_value, " +
                             "deleted_by, " +
                             "deleted_date) " +
                             " values(" +
                             "'" + msGetGid + "'," +
                             "'" + campaign_gid + "', " +
                             "'Campaign'," +
                             "'" + lscampaign_value + "'," +
                             "'" + employee_gid + "'," +
                             "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occured..!";
                }
            }


        }

        public void DaPostRaiseQuery(MdlTrnCampaign values, string employee_gid)
        {


            msGetGid = objcmnfunctions.GetMasterGID("CPRQ");
            msSQL = "Insert into fnd_trn_tcampaignraisequery( " +
                   " campaignraisequery_gid, " +
                   " campaign_gid," +
                   " query_title, " +
                   " query_description," +
                   " raisequery_status, " +
                   " created_by," +
                   " created_date)" +
                   " values(" +
                   "'" + msGetGid + "'," +
                   "'" + values.campaign_gid + "', " +
                   "'" + values.query_title.Replace("'", "") + "'," +
                    "'" + values.query_description.Replace("'", "") + "'," +
                   "'Query Raised'," +
                   "'" + employee_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Query Raised  Successfully";
                try
                {
                
                k = 1;

                msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    ls_server = objODBCDatareader["pop_server"].ToString();
                    ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                    ls_username = objODBCDatareader["pop_username"].ToString();
                    ls_password = objODBCDatareader["pop_password"].ToString();
                }
                objODBCDatareader.Close();             
                msSQL =
                       " select  group_concat(distinct d.employee_gid)  as CC2members,a.campaign_name, a.created_by, h.campaigntype_name, concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as campaign_apr, " + 
                       " a.created_date, concat(i.user_firstname,' ',i.user_lastname,' / ',i.user_code) as employee_name  from fnd_trn_tcampaign a" +
                            " left join fnd_mst_tcampaignapproving2employee d on a.campaign_gid = d.campaign_gid" +
                            " left join fnd_mst_tcampaigntype h on h.campaigntype_gid = a.campaigntype_gid" +
                            " left join hrm_mst_temployee b on a.campaign_approver = b.employee_gid" +
                            " left join hrm_mst_temployee f on a.created_by = f.employee_gid" +
                            " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                            " left join adm_mst_tuser i on i.user_gid = f.user_gid  " +
                        " where a.campaign_gid ='" + values.campaign_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {

                    lsemployee_name = objODBCDatareader["employee_name"].ToString();
                    //lsname = objODBCDatareader["campaign_apr"].ToString();    
                    lscampaign_name = objODBCDatareader["campaign_name"].ToString();                 
                    lscc2members = objODBCDatareader["CC2members"].ToString();
                    lscampaign_type = objODBCDatareader["campaigntype_name"].ToString();
                    lsTo2members = objODBCDatareader["created_by"].ToString();
                    lscreated_date = objODBCDatareader["created_date"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = "select concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as employee_name from hrm_mst_temployee a " +
                        " left join adm_mst_tuser c on a.user_gid = c.user_gid " +
                        " where employee_gid = '" + employee_gid + "'";
                string employee_name = objdbconn.GetExecuteScalar(msSQL);

                string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
                sToken = "";
                int Length = 100;
                for (int j = 0; j < Length; j++)
                {
                    string sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                    sToken += sTempChars;
                }

                k = k + 1;


                msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee " +
                        " where employee_gid in ('" + lsTo2members.Replace(",", "', '") + "')";
                lsto_mail = objdbconn.GetExecuteScalar(msSQL);


                msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                cc_mailid = objdbconn.GetExecuteScalar(msSQL);



                sub = "RE: Query Raised ";
                body = "Dear Sir,<br />";
                body = body + "<br />";
                body = body + "Greetings,  <br />";
                body = body + "<br />";
                body = body + "Query has been Raised. The details are as follows, <br />";
                body = body + "<br />";
                body = body + "<b> Campaign Name :</b> " + HttpUtility.HtmlEncode(lscampaign_name) + "<br />";
                body = body + "<br />";
                body = body + "<b> Campaign Type :</b> " + HttpUtility.HtmlEncode(lscampaign_type) + "<br />";
                body = body + "<br />";              
                body = body + "Kindly log into systems to view more details.";
                body = body + "<br />";
                body = body + "<br />";
                body = body + "Thanks & Regards, ";
                body = body + "<br />";
                body = body + HttpUtility.HtmlEncode(employee_name);
                body = body + "<br />";
                body = body + "<br />";
                body = body + "<br />";
                body = body + " **This is an automated e-mail. Please do not reply to this mailbox**";


                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(ls_username);
                //message.To.Add(new MailAddress(lsto_mail));


                lsBccmail_id = ConfigurationManager.AppSettings["Foundationbcc"].ToString();

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
                    if (values.status == true)
                    {
                        msSQL = "Insert into fnd_trn_tfoundationmailcount( " +
                        " campaign_gid," +
                        " from_mail," +
                        " to_mail," +
                        " cc_mail," +
                        " mail_status," +
                          " raisequery_gid, " +
                        " mail_senddate, " +                      
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + values.campaign_gid + "'," +
                        "'" + employee_name + "'," +
                        "'" + lsto_mail + "'," +
                        "'" + cc_mailid + "'," +
                        "'Query Raised Successfully'," +
                           "'" + msGetGid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
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
                values.message = "Error Occured While Adding";
                values.status = false;
            }
        }
        public void DaGetCampaignRaiseQuery(string campaign_gid, MdlTrnCampaign values)
        {
            msSQL = " select a.campaign_gid,a.campaignraisequery_gid,a.query_title,a.query_description,a.raisequery_status,a.queryresponse_remarks," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                    " concat(d.user_firstname, ' ', d.user_lastname, ' / ', d.user_code) as created_by, " +
                     " concat(f.user_firstname, ' ', f.user_lastname, ' / ', f.user_code) as query_responseby " +
                    " from fnd_trn_tcampaignraisequery a " +
                    " left join hrm_mst_temployee c on a.created_by = c.employee_gid" +
                     " left join hrm_mst_temployee e on a.query_responseby = e.employee_gid" +
                    " left join adm_mst_tuser d on c.user_gid = d.user_gid " +
                    " left join adm_mst_tuser f on e.user_gid = f.user_gid " +
                    " where a.campaign_gid = '" + campaign_gid + "'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcampiagnraisequery_list = new List<campiagnraisequery_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcampiagnraisequery_list.Add(new campiagnraisequery_list
                    {
                        campaign_gid = (dr_datarow["campaign_gid"].ToString()),
                        campaignraisequery_gid = (dr_datarow["campaignraisequery_gid"].ToString()),
                        query_title = (dr_datarow["query_title"].ToString()),
                        query_description = (dr_datarow["query_description"].ToString()),
                        queryresponse_remarks = (dr_datarow["queryresponse_remarks"].ToString()),
                        query_responseby = (dr_datarow["query_responseby"].ToString()),
                        raisequery_status = (dr_datarow["raisequery_status"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString())

                    });
                }
                values.campiagnraisequery_list = getcampiagnraisequery_list;
            }
            dt_datatable.Dispose();
        }
        public void DaPostCampaignresponsequery(MdlTrnCampaign values, string employee_gid)
        {

            msSQL = " update fnd_trn_tcampaignraisequery set queryresponse_remarks ='" + values.queryresponse_remarks.Replace("'", "") + "'," +
                   " query_responseby='" + employee_gid + "'," +
                   " query_responsedate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                   " raisequery_status='Closed' " +
                   " where campaignraisequery_gid='" + values.campaignraisequery_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
           

            if (mnResult == 1)
            {
                values.status = true;
                values.message = "Query Closed Successfully..!";
                try
                {
               
                k = 1;

                msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    ls_server = objODBCDatareader["pop_server"].ToString();
                    ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                    ls_username = objODBCDatareader["pop_username"].ToString();
                    ls_password = objODBCDatareader["pop_password"].ToString();
                }
                objODBCDatareader.Close();

                msSQL =
                       " select  group_concat(distinct d.employee_gid)  as CC2members,a.campaign_name,a.campaign_approver, a.created_by, h.campaigntype_name, concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as campaign_apr, " +
                       " a.created_date, concat(i.user_firstname,' ',i.user_lastname,' / ',i.user_code) as employee_name  from fnd_trn_tcampaign a" +
                            " left join fnd_mst_tcampaignapproving2employee d on a.campaign_gid = d.campaign_gid" +
                            " left join fnd_mst_tcampaigntype h on h.campaigntype_gid = a.campaigntype_gid" +
                            " left join hrm_mst_temployee b on a.campaign_approver = b.employee_gid" +
                            " left join hrm_mst_temployee f on a.created_by = f.employee_gid" +
                            " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                            " left join adm_mst_tuser i on i.user_gid = f.user_gid  " +
                        " where a.campaign_gid ='" + values.campaign_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {

                    lsemployee_name = objODBCDatareader["employee_name"].ToString();
                    //lsname = objODBCDatareader["campaign_apr"].ToString();    
                    lscampaign_name = objODBCDatareader["campaign_name"].ToString();
                    lscc2members = objODBCDatareader["CC2members"].ToString();
                    lscampaign_type = objODBCDatareader["campaigntype_name"].ToString();
                    lsTo2members = objODBCDatareader["campaign_approver"].ToString();
                    lscreated_date = objODBCDatareader["created_date"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = "select query_title from fnd_trn_tcampaignraisequery  " +
                      " where campaign_gid = '" + values.campaign_gid + "'";
                string lsquery_title = objdbconn.GetExecuteScalar(msSQL);


                msSQL = "select concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as employee_name from hrm_mst_temployee a " +
                        " left join adm_mst_tuser c on a.user_gid = c.user_gid " +
                        " where employee_gid = '" + employee_gid + "'";
                string employee_name = objdbconn.GetExecuteScalar(msSQL);

                string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
                sToken = "";
                int Length = 100;
                for (int j = 0; j < Length; j++)
                {
                    string sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                    sToken += sTempChars;
                }

                k = k + 1;


                msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee " +
                        " where employee_gid in ('" + lsTo2members.Replace(",", "', '") + "')";
                lsto_mail = objdbconn.GetExecuteScalar(msSQL);


                msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                cc_mailid = objdbconn.GetExecuteScalar(msSQL);



                sub = "RE: Query Response ";
                body = "Dear Sir,<br />";
                body = body + "<br />";
                body = body + "Greetings,  <br />";
                body = body + "<br />";
                body = body + "Query has been Response. The details are as follows, <br />";
                body = body + "<br />";
                body = body + "<b> Campaign Name :</b> " + HttpUtility.HtmlEncode(lscampaign_name) + "<br />";
                body = body + "<br />";
                body = body + "<b> Campaign Type :</b> " + HttpUtility.HtmlEncode(lscampaign_type) + "<br />";
                body = body + "<br />";
                body = body + "<b> Query Title :</b> " + HttpUtility.HtmlEncode(lsquery_title) + "<br />";
                body = body + "<br />";
                body = body + "Kindly log into systems to view more details.";
                body = body + "<br />";
                body = body + "<br />";
                body = body + "Thanks & Regards, ";
                body = body + "<br />";
                body = body + HttpUtility.HtmlEncode(employee_name);
                body = body + "<br />";
                body = body + "<br />";
                body = body + "<br />";
                body = body + " **This is an automated e-mail. Please do not reply to this mailbox**";


                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(ls_username);
                //message.To.Add(new MailAddress(lsto_mail));


                lsBccmail_id = ConfigurationManager.AppSettings["Foundationbcc"].ToString();

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

                    if (values.status == true)
                    {
                        msSQL = "Insert into fnd_trn_tfoundationmailcount( " +
                        " campaign_gid," +
                        " from_mail," +
                        " to_mail," +
                        " cc_mail," +
                        " mail_status," +
                          " raisequery_gid, " +
                        " mail_senddate, " +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + values.campaign_gid + "'," +
                        "'" + employee_name + "'," +
                        "'" + lsto_mail + "'," +
                        "'" + cc_mailid + "'," +
                        "'Query Close Successfully'," +
                           "'" + values.campaignraisequery_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
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
                values.status = false;
                values.message = "Error Occured..!";
            }
        }

    }
}