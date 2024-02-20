using ems.masterng.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.Hosting;


namespace ems.masterng.DataAccess
{
    public class DaMstNgApplicationAdd
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        OdbcDataReader objODBCDatareader, objODBCDatareader1, objODBCDataReader;
        DataTable dt_datatable;
        int mnResult;
        string msSQL, msGetGid, msGetGidpan, lscustomer_name, lsurn, lsstakeholder_type, lsregion;
        private string lsemployee_name;
        private string lsapplication_gid;
        private string lsdrm_gid;
        private string lsdrm_name;

        public string application_gid, stackholder_gid, stakeholder_type, name, program, doc_count;
        string document_count;

        private string lsheadoffice_status;


        public void DaGetApplicationNewSummary(string employee_gid, MdlMstApplicationAdd values)
        {
            msSQL = " select a.application_gid,a.application_no,a.customerref_name as customer_name,a.customer_urn,a.vertical_name,a.overalllimit_amount,a.program_name,j.overalllimit_amount as existinglimit_amount," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,a.approval_status,a.applicant_type,a.created_by as createdby," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,date_format(a.submitted_date,'%d-%m-%Y %h:%i %p') as updated_date," +
                    " case when a.headapproval_status='Pending' then 'Pending' when a.headapproval_status='Comment Raised' then 'Comment Raised' else 'Level Approved' end as headapproval_status," +
                    " concat(FLOOR((DATEDIFF(a.submitted_date, a.created_date))), ' days ', MOD(HOUR(TIMEDIFF(a.submitted_date, a.created_date)), 24), ' Hrs ', MINUTE(TIMEDIFF(a.submitted_date, a.created_date)), 'Mins') as aging," +
                    " a.productcharge_flag, a.economical_flag,a.creditheadapproval_status,a.renewal_flag,a.enhancement_flag " +
                    " from ocs_mst_tapplication a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                    " left join ocs_mst_tapplication j on j.application_gid = a.refapplication_gid" +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid where a.created_by='" + employee_gid + "' and " +
                    " (a.application_gid not in (select application_gid from ocs_mst_tapplication where approval_status ='Rejected By Credit' or  approval_status ='Hold By Credit' " +
                    " or approval_status = 'Rejected By Business' or approval_status = 'Hold By Business' or approval_status = 'CC Rejected' or approval_status = 'Rejected by Credit Manager')) and " +
                    " (a.headapproval_status='Pending' or a.headapproval_status='Comment Raised' or a.headapproval_status like '%Approved' or a.creditheadapproval_status like '%Approved' or " +
                    " a.approval_status='Submitted to CC'  or a.approval_status='Submitted to Underwriting' or a.approval_status='Submitted to Credit Approval'  or a.approval_status='Submitted to Approval') and a.renewal_flag = '" + values.renewal_flag + "' and a.enhancement_flag = '" + values.enhancement_flag + "'and " +
                    " (a.application_no like '%" + values.searchText + "%' or a.customerref_name like '%" + values.searchText + "%' or a.vertical_name like '%" + values.searchText + "%'  or a.program_name like '%" + values.searchText + "%' or a.overalllimit_amount like '%" + values.searchText + "%'or a.approval_status like '%" + values.searchText + "%') group by a.application_gid  order by application_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);

            //msSQL = "call ocs_trn_spngapplicationnewsummary ('" + employee_gid + "', '" + values.searchText + "' , '" + values.renewal_flag + "' , '" + values.enhancement_flag +  "' )";
            //dt_datatable = objdbconn.GetDataTable(msSQL);

            var getapplicationadd_list = new List<applicationadd_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    //msSQL = " select appcreditquery_gid from ocs_trn_tapplicationcreditquery where query_status = 'Open' and application_gid = '" + dt["application_gid"].ToString() + "' " +
                    //        " and queryraised_to = 'RM'";
                    //objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    //if (objODBCDatareader.HasRows == false)
                    //{
                    //    lsrmquery_flag = "Y";
                    //}
                    //else
                    //{
                    //    lsrmquery_flag = "N";
                    //}
                    //objODBCDatareader.Close();
                    getapplicationadd_list.Add(new applicationadd_list
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical_name = dt["vertical_name"].ToString(),
                        overalllimit_amount = dt["overalllimit_amount"].ToString(),
                        program_name = dt["program_name"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        economical_flag = dt["economical_flag"].ToString(),
                        productcharge_flag = dt["productcharge_flag"].ToString(),
                        application_status = dt["approval_status"].ToString(),
                        applicant_type = dt["applicant_type"].ToString(),
                        updated_date = dt["updated_date"].ToString(),
                        createdby = dt["createdby"].ToString(),
                        headapproval_status = dt["headapproval_status"].ToString(),
                        creditheadapproval_status = dt["creditheadapproval_status"].ToString(),
                        renewal_flag = dt["renewal_flag"].ToString(),
                        //rmquery_flag = lsrmquery_flag,
                        //rmquery_flag = dt["rmquery_flag"].ToString(),
                        enhancement_flag = dt["enhancement_flag"].ToString(),
                        existinglimit_amount = dt["existinglimit_amount"].ToString(),
                        aging = dt["aging"].ToString(),
                    });

                }
            }
            values.applicationadd_list = getapplicationadd_list;
            dt_datatable.Dispose();
        }

        public void DaGetApplicationRejectedSummary(string employee_gid, MdlMstApplicationAdd values)
        {
            if (values.approval_status.Contains('|'))
            {
                string[] arrapprovalstatus = values.approval_status.Split('|');
                int arrapprovalstatuslength = arrapprovalstatus.Length;
                string approvalstatus2 = "";
                string approval_status = arrapprovalstatus[0].Trim();
                if (arrapprovalstatuslength > 1)
                {
                    approvalstatus2 = arrapprovalstatus[1].Trim();
                }
                values.approval_status = approval_status + ',' + approvalstatus2;
                int indexOfFirst = values.approval_status.IndexOf('\'');
                string temp = values.approval_status.Remove(indexOfFirst, 1);
                int indexOfLast = temp.LastIndexOf('\'');
                values.approval_status = temp.Remove(indexOfLast, 1);
            }
            var getapplicationadd_list = new List<applicationadd_list>();

            msSQL = " select a.application_gid, a.application_no, a.customerref_name as customer_name,a.customer_urn,a.vertical_name,a.headapproval_status,a.overalllimit_amount,a.program_name,d.rejected_date as buisnessrejecteddate,j.overalllimit_amount as existinglimit_amount," +
                 " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as created_by,a.approval_status,a.applicant_type,a.created_by as createdby," +
                 " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date,date_format(a.submitted_date, '%d-%m-%Y %h:%i %p') as updated_date," +
                 " a.productcharge_flag, a.economical_flag,a.renewal_flag,a.enhancement_flag from ocs_mst_tapplication a" +
                 " left join hrm_mst_temployee b on b.employee_gid = a.created_by" +
                 " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                 " left join ocs_mst_tapplication j on j.application_gid = a.refapplication_gid" +
                 " left join ocs_trn_tapplicationapproval d on a.application_gid = d.application_gid" +
                 " where a.created_by = '" + employee_gid + "' and a.approval_status in ('" + values.approval_status + "') and a.renewal_flag = '" + values.renewal_flag + "' and a.enhancement_flag = '" + values.enhancement_flag + "'and " +
                 " (a.application_no like concat('%" + values.searchText + "%') or a.customerref_name like concat('%" + values.searchText + "%') or a.vertical_name like concat('%" + values.searchText + "%') or a.program_name like concat('%" + values.searchText + "%')" +
                 " or a.overalllimit_amount like concat('%" + values.searchText + "%') or a.approval_status like concat('%" + values.searchText + "%')) group by a.application_gid order by application_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);


            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getapplicationadd_list.Add(new applicationadd_list
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical_name = dt["vertical_name"].ToString(),
                        overalllimit_amount = dt["overalllimit_amount"].ToString(),
                        program_name = dt["program_name"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        economical_flag = dt["economical_flag"].ToString(),
                        productcharge_flag = dt["productcharge_flag"].ToString(),
                        application_status = dt["approval_status"].ToString(),
                        applicant_type = dt["applicant_type"].ToString(),
                        updated_date = dt["updated_date"].ToString(),
                        createdby = dt["createdby"].ToString(),
                        headapproval_status = dt["headapproval_status"].ToString(),
                        renewal_flag = dt["renewal_flag"].ToString(),
                        enhancement_flag = dt["enhancement_flag"].ToString(),
                        rejected_date = Getrejecteddate(dt["application_gid"].ToString(), values.approval_status.ToString(), employee_gid.ToString()),
                        existinglimit_amount = dt["existinglimit_amount"].ToString(),
                    });

                }
            }
            values.applicationadd_list = getapplicationadd_list;
            dt_datatable.Dispose();
        }

        public string Getrejecteddate(string application_gid, string approval_status, string employee_gid)
        {
            string rejected_date = String.Empty;
            if (approval_status == "Rejected By Business")
            {
                msSQL = " select d.rejected_date " +
                        " from ocs_mst_tapplication a" +
                      " left join hrm_mst_temployee b on b.employee_gid = a.created_by" +
                      " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                      " left join ocs_trn_tapplicationapproval d on a.application_gid = d.application_gid" +
                      " where a.created_by = '" + employee_gid + "' and a.application_gid = '" + application_gid + "'";
                rejected_date = objdbconn.GetExecuteScalar(msSQL);
                return rejected_date;
            }
            else if (approval_status == "CC Rejected")
            {
                msSQL = " select d.approved_date " +
                " from ocs_mst_tapplication a" +
              " left join hrm_mst_temployee b on b.employee_gid = a.created_by" +
              " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
              " left join ocs_mst_tccmeeting2members d on a.application_gid = d.application_gid" +
              " where a.created_by = '" + employee_gid + "' and a.application_gid = '" + application_gid + "'";
                rejected_date = objdbconn.GetExecuteScalar(msSQL);
                return rejected_date;

            }
            else
            {
                msSQL = " select d.rejected_date " +
                   " from ocs_mst_tapplication a" +
                 " left join hrm_mst_temployee b on b.employee_gid = a.created_by" +
                 " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                 " left join ocs_trn_tAppcreditapproval d on a.application_gid = d.application_gid" +
                 " where a.created_by = '" + employee_gid + "' and a.application_gid = '" + application_gid + "'";
                rejected_date = objdbconn.GetExecuteScalar(msSQL);
                return rejected_date;
            }
        }

        public void DaGetApplicationHoldSummary(string employee_gid, MdlMstApplicationAdd values)
        {

            if (values.approval_status.Contains('|'))
            {
                string[] arrapprovalstatus = values.approval_status.Split('|');
                int arrapprovalstatuslength = arrapprovalstatus.Length;
                string approvalstatus2 = "";
                string approval_status = arrapprovalstatus[0].Trim();
                if (arrapprovalstatuslength > 1)
                {
                    approvalstatus2 = arrapprovalstatus[1].Trim();
                }
                values.approval_status = approval_status + ',' + approvalstatus2;
                int indexOfFirst = values.approval_status.IndexOf('\'');
                string temp = values.approval_status.Remove(indexOfFirst, 1);
                int indexOfLast = temp.LastIndexOf('\'');
                values.approval_status = temp.Remove(indexOfLast, 1);
            }
            msSQL = " select a.application_gid, a.application_no, a.customerref_name as customer_name,a.customer_urn,a.vertical_name,a.headapproval_status,a.overalllimit_amount,a.program_name,d.hold_date,j.overalllimit_amount as existinglimit_amount," +
                    " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as created_by,a.approval_status,a.applicant_type,a.created_by as createdby," +
                    " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date,date_format(a.submitted_date, '%d-%m-%Y %h:%i %p') as updated_date," +
                    " a.productcharge_flag, a.economical_flag,a.renewal_flag,a.enhancement_flag from ocs_mst_tapplication a" +
                    " left join hrm_mst_temployee b on b.employee_gid = a.created_by" +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                    " left join ocs_trn_tAppcreditapproval d on a.application_gid = d.application_gid" +
                    " left join ocs_mst_tapplication j on j.application_gid = a.refapplication_gid" +
                    " where a.created_by = '" + employee_gid + "' and a.approval_status in ('" + values.approval_status + "') and a.renewal_flag = '" + values.renewal_flag + "' and a.enhancement_flag = '" + values.enhancement_flag + "'and " +
                    " (a.application_no like concat('%" + values.searchText + "%') or a.customerref_name like concat('%" + values.searchText + "%') or a.vertical_name like concat('%" + values.searchText + "%') or a.program_name like concat('%" + values.searchText + "%')" +
                    " or a.overalllimit_amount like concat('%" + values.searchText + "%') or a.approval_status like concat('%" + values.searchText + "%')) group by a.application_gid order by a.application_gid desc";

            dt_datatable = objdbconn.GetDataTable(msSQL);

            //msSQL = "call ocs_trn_spngapplicationholdsummary ('" + employee_gid + "','" + values.searchText + "','" + values.approval_status + "')";
            //dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<applicationadd_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getapplicationadd_list.Add(new applicationadd_list
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical_name = dt["vertical_name"].ToString(),
                        overalllimit_amount = dt["overalllimit_amount"].ToString(),
                        program_name = dt["program_name"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        economical_flag = dt["economical_flag"].ToString(),
                        productcharge_flag = dt["productcharge_flag"].ToString(),
                        application_status = dt["approval_status"].ToString(),
                        applicant_type = dt["applicant_type"].ToString(),
                        updated_date = dt["updated_date"].ToString(),
                        createdby = dt["createdby"].ToString(),
                        headapproval_status = dt["headapproval_status"].ToString(),
                        renewal_flag = dt["renewal_flag"].ToString(),
                        enhancement_flag = dt["enhancement_flag"].ToString(),
                        hold_date = Getholddate(dt["application_gid"].ToString(), values.approval_status.ToString(), employee_gid.ToString()),
                        existinglimit_amount = dt["existinglimit_amount"].ToString(),
                    });

                }
            }
            values.applicationadd_list = getapplicationadd_list;
            dt_datatable.Dispose();
        }
        public string Getholddate(string application_gid, string approval_status, string employee_gid)
        {
            string hold_date = String.Empty;
            if (approval_status == "Hold By Business")
            {
                msSQL = " select d.hold_date " +
                        " from ocs_mst_tapplication a" +
                      " left join hrm_mst_temployee b on b.employee_gid = a.created_by" +
                      " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                      " left join ocs_trn_tapplicationapproval d on a.application_gid = d.application_gid" +
                      " where a.created_by = '" + employee_gid + "' and a.application_gid = '" + application_gid + "'";
                hold_date = objdbconn.GetExecuteScalar(msSQL);
                return hold_date;
            }
            else if (approval_status == "Hold By Credit")
            {
                msSQL = " select d.hold_date " +
                " from ocs_mst_tapplication a" +
              " left join hrm_mst_temployee b on b.employee_gid = a.created_by" +
              " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
              " left join ocs_trn_tAppcreditapproval d on a.application_gid = d.application_gid" +
              " where a.created_by = '" + employee_gid + "' and a.application_gid = '" + application_gid + "'";
                hold_date = objdbconn.GetExecuteScalar(msSQL);
                return hold_date;

            }
            else
            {
                msSQL = " select d.approved_date " +
                   " from ocs_mst_tapplication a" +
                 " left join hrm_mst_temployee b on b.employee_gid = a.created_by" +
                 " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                 " left join ocs_mst_tccmeeting2members d on a.application_gid = d.application_gid" +
                 " where a.created_by = '" + employee_gid + "' and a.application_gid = '" + application_gid + "'";
                hold_date = objdbconn.GetExecuteScalar(msSQL);
                return hold_date;
            }
        }

        public void DaGetApplicationApprovedSummary(string employee_gid, MdlMstApplicationAdd values)
        {
            if (values.approval_status.Contains('|'))
            {
                string[] arrapprovalstatus = values.approval_status.Split('|');
                string approval_status = "";
                if (arrapprovalstatus.Length > 0)
                {
                    for (int i = 0; i < arrapprovalstatus.Length; i++)
                    {
                        approval_status += arrapprovalstatus[i] + ',';
                    }
                    values.approval_status = approval_status.Trim(',');

                }
                int indexOfFirst = values.approval_status.IndexOf('\'');
                string temp = values.approval_status.Remove(indexOfFirst, 1);
                int indexOfLast = temp.LastIndexOf('\'');
                values.approval_status = temp.Remove(indexOfLast, 1);
            }
            msSQL = " select a.application_gid, a.application_no, a.customerref_name as customer_name,a.customer_urn,a.vertical_name,a.headapproval_status,a.overalllimit_amount,a.program_name,j.overalllimit_amount as existinglimit_amount," +
                    " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as created_by,a.approval_status,a.applicant_type,a.created_by as createdby," +
                    " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date,date_format(a.submitted_date, '%d-%m-%Y %h:%i %p') as updated_date," +
                    " a.productcharge_flag, a.economical_flag,a.renewal_flag,a.enhancement_flag from ocs_mst_tapplication a" +
                    " left join hrm_mst_temployee b on b.employee_gid = a.created_by" +
                    " left join ocs_mst_tapplication j on j.application_gid = a.refapplication_gid" +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid where a.created_by = '" + employee_gid + "' and a.approval_status in ('" + values.approval_status + "') and a.renewal_flag = '" + values.renewal_flag + "' and a.enhancement_flag = '" + values.enhancement_flag + "'and " +
                    " (a.application_no like concat('%" + values.searchText + "%') or a.customerref_name like concat('%" + values.searchText + "%') or a.vertical_name like concat('%" + values.searchText + "%') or a.program_name like concat('%" + values.searchText + "%')" +
                    " or a.overalllimit_amount like concat('%" + values.searchText + "%') or a.approval_status like concat('%" + values.searchText + "%')) group by a.application_gid order by application_gid desc";
            //dt_datatable = objdbconn.GetDataTable(msSQL);
            //msSQL = "call ocs_trn_spngapplicationapprovedsummary ('" + employee_gid + "' , '" + values.searchText + "','" + values.approval_status +  "', '"+values.renewal_flag + "','"+values.renewal_flag +"')";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<applicationadd_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getapplicationadd_list.Add(new applicationadd_list
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical_name = dt["vertical_name"].ToString(),
                        overalllimit_amount = dt["overalllimit_amount"].ToString(),
                        program_name = dt["program_name"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        economical_flag = dt["economical_flag"].ToString(),
                        productcharge_flag = dt["productcharge_flag"].ToString(),
                        application_status = dt["approval_status"].ToString(),
                        applicant_type = dt["applicant_type"].ToString(),
                        updated_date = dt["updated_date"].ToString(),
                        createdby = dt["createdby"].ToString(),
                        headapproval_status = dt["headapproval_status"].ToString(),
                        renewal_flag = dt["renewal_flag"].ToString(),
                        enhancement_flag = dt["enhancement_flag"].ToString(),
                        existinglimit_amount = dt["existinglimit_amount"].ToString(),
                        aging = Agingdate(dt["application_gid"].ToString(), values.approval_status.ToString(), employee_gid.ToString()),
                    });

                }


            }
            values.applicationadd_list = getapplicationadd_list;
            dt_datatable.Dispose();
        }

        public string Agingdate(string application_gid, string approval_status, string employee_gid)
        {
            string aging = String.Empty;
            if (approval_status == "Submitted to Approval")
            {

                msSQL = " select concat(FLOOR((DATEDIFF(d.approved_date , a.submitted_date))), ' days ', MOD(HOUR(TIMEDIFF(d.approved_date , a.submitted_date)), 24), ' Hrs ', MINUTE(TIMEDIFF(d.approved_date , a.submitted_date)), 'Mins') as buisnessaging " +
                      " from ocs_mst_tapplication a" +
                      " left join hrm_mst_temployee b on b.employee_gid = a.created_by" +
                      " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                      " left join ocs_trn_tapplicationapproval d on d.application_gid = a.application_gid and d.hierary_level = '5' " +
                      " where a.created_by = '" + employee_gid + "' and d.application_gid = '" + application_gid + "' and d.hierary_level = '5'";
                aging = objdbconn.GetExecuteScalar(msSQL);
                return aging;
            }
            else if (approval_status == "Submitted to Credit Approval','Submitted to Underwriting','Sent Back to Credit")
            {
                msSQL = " select concat(FLOOR((DATEDIFF(d.approved_date, a.creditassigned_date))), ' days ', MOD(HOUR(TIMEDIFF(d.approved_date, a.creditassigned_date)), 24), ' Hrs ', MINUTE(TIMEDIFF(d.approved_date, a.creditassigned_date)), 'Mins') as creditaging " +
                " from ocs_mst_tapplication a" +
              " left join hrm_mst_temployee b on b.employee_gid = a.created_by" +
              " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
              " left join ocs_trn_tappcreditapproval d on a.application_gid = d.application_gid and d.hierary_level = '3' " +
              " where a.created_by = '" + employee_gid + "' and a.application_gid = '" + application_gid + "' and d.hierary_level = '3'";
                aging = objdbconn.GetExecuteScalar(msSQL);
                return aging;

            }
            else
            {
                msSQL = " select concat(FLOOR((DATEDIFF(a.cccompleted_date,a.ccsubmitted_date))), ' days ', MOD(HOUR(TIMEDIFF(a.ccsubmitted_date, a.cccompleted_date)), 24), ' Hrs ', MINUTE(TIMEDIFF(a.ccsubmitted_date, a.cccompleted_date)), 'Mins') as ccaging " +
                   " from ocs_mst_tapplication a" +
                 " left join hrm_mst_temployee b on b.employee_gid = a.created_by" +
                 " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                 " where a.created_by = '" + employee_gid + "' and a.application_gid = '" + application_gid + "'";
                aging = objdbconn.GetExecuteScalar(msSQL);
                return aging;
            }
        }
        public void DaApplicationCount(string user_gid, string employee_gid, ApplicationCount values)
        {
            msSQL = " select count(application_gid) as newapplication_count from ocs_mst_tapplication a where a.created_by='" + employee_gid + "' and " +
                    " (application_gid not in (select application_gid from ocs_mst_tapplication where approval_status ='Rejected By Credit' or  approval_status ='Hold By Credit' " +
                   " or approval_status = 'Rejected By Business' or approval_status = 'Hold By Business' or approval_status = 'CC Approved'  or approval_status = 'CC Rejected')) and" +
                    " ( a.headapproval_status='Pending' or a.headapproval_status='Comment Raised' or a.headapproval_status like '%Approved' or a.creditheadapproval_status like '%Approved'  or " +
                    " a.approval_status='Submitted to CC'  or a.approval_status='Submitted to Underwriting' or a.approval_status='Submitted to Credit Approval'  or a.approval_status='Submitted to Approval')  ";
            values.newapplication_count = objdbconn.GetExecuteScalar(msSQL);
            int newapplication_count = Convert.ToInt16(values.newapplication_count);

            msSQL = "select count(application_gid) as activetab_count from ocs_mst_tapplication a " +
                   "left join  hrm_mst_temployee b on b.employee_gid = a.created_by " +
                   "left join  adm_mst_tuser c on c.user_gid = b.user_gid where a.created_by = '" + employee_gid + "' and a.approval_status in ('Submitted to Approval','Submitted to Underwriting','Submitted to Credit Approval','Sent Back to Credit','Submitted to CC','CC Approved') ";
            values.activetab_count = objdbconn.GetExecuteScalar(msSQL);
            int activetab_count = Convert.ToInt16(values.activetab_count);

            msSQL = "select count(application_gid) as activebusiness_count from ocs_mst_tapplication a " +
                   "left join  hrm_mst_temployee b on b.employee_gid = a.created_by " +
                   "left join  adm_mst_tuser c on c.user_gid = b.user_gid where a.created_by = '" + employee_gid + "' and a.approval_status = 'Submitted to Approval'" + "";
            values.activebusiness_count = objdbconn.GetExecuteScalar(msSQL);
            int activebusiness_count = Convert.ToInt16(values.activebusiness_count);

            msSQL = "select count(application_gid) as activecredit_count from ocs_mst_tapplication a " +
                   "left join  hrm_mst_temployee b on b.employee_gid = a.created_by " +
                   "left join  adm_mst_tuser c on c.user_gid = b.user_gid where a.created_by = '" + employee_gid + "' and a.approval_status in ('Submitted to Underwriting','Submitted to Credit Approval','Sent Back to Credit') ";
            values.activecredit_count = objdbconn.GetExecuteScalar(msSQL);
            int activecredit_count = Convert.ToInt16(values.activecredit_count);

            msSQL = "select count(application_gid) as activecc_count from ocs_mst_tapplication a " +
                   "left join  hrm_mst_temployee b on b.employee_gid = a.created_by " +
                   "left join  adm_mst_tuser c on c.user_gid = b.user_gid where a.created_by = '" + employee_gid + "' and a.approval_status in ('CC Approved','Submitted to CC')";
            values.activecc_count = objdbconn.GetExecuteScalar(msSQL);
            int activecc_count = Convert.ToInt16(values.activecc_count);

            msSQL = "select count(application_gid) as holdtab_count from ocs_mst_tapplication a " +
                   "left join  hrm_mst_temployee b on b.employee_gid = a.created_by " +
                   "left join  adm_mst_tuser c on c.user_gid = b.user_gid where a.created_by = '" + employee_gid + "' and a.approval_status in ('Hold By Business','Hold By Credit','Hold By CC') ";
            values.holdtab_count = objdbconn.GetExecuteScalar(msSQL);
            int holdtab_count = Convert.ToInt16(values.holdtab_count);

            msSQL = "select count(application_gid) as holdbusiness_count from ocs_mst_tapplication a " +
                   "left join  hrm_mst_temployee b on b.employee_gid = a.created_by " +
                   "left join  adm_mst_tuser c on c.user_gid = b.user_gid where a.created_by = '" + employee_gid + "' and a.approval_status = 'Hold By Business'" + "";
            values.holdbusiness_count = objdbconn.GetExecuteScalar(msSQL);
            int holdbusiness_count = Convert.ToInt16(values.holdbusiness_count);

            msSQL = "select count(application_gid) as holdcredit_count from ocs_mst_tapplication a " +
                   "left join  hrm_mst_temployee b on b.employee_gid = a.created_by " +
                   "left join  adm_mst_tuser c on c.user_gid = b.user_gid where a.created_by = '" + employee_gid + "' and a.approval_status = 'Hold By Credit'";
            values.holdcredit_count = objdbconn.GetExecuteScalar(msSQL);
            int holdcredit_count = Convert.ToInt16(values.holdcredit_count);

            msSQL = "select count(application_gid) as holdcc_count from ocs_mst_tapplication a " +
                   "left join  hrm_mst_temployee b on b.employee_gid = a.created_by " +
                   "left join  adm_mst_tuser c on c.user_gid = b.user_gid where a.created_by = '" + employee_gid + "' and a.approval_status = 'Hold By CC'";
            values.holdcc_count = objdbconn.GetExecuteScalar(msSQL);
            int holdcc_count = Convert.ToInt16(values.holdcc_count);

            msSQL = "select count(application_gid) as rejectedtab_count from ocs_mst_tapplication a " +
                   "left join  hrm_mst_temployee b on b.employee_gid = a.created_by " +
                   "left join  adm_mst_tuser c on c.user_gid = b.user_gid where a.created_by = '" + employee_gid + "' and a.approval_status in ('Rejected By Business','Rejected By Credit','Rejected by Credit Manager','CC Rejected') ";
            values.rejectedtab_count = objdbconn.GetExecuteScalar(msSQL);
            int rejectedtab_count = Convert.ToInt16(values.rejectedtab_count);

            msSQL = "select count(application_gid) as rejectedbusiness_count from ocs_mst_tapplication a " +
                   "left join  hrm_mst_temployee b on b.employee_gid = a.created_by " +
                   "left join  adm_mst_tuser c on c.user_gid = b.user_gid where a.created_by = '" + employee_gid + "' and a.approval_status = 'Rejected By Business'" + "";
            values.rejectedbusiness_count = objdbconn.GetExecuteScalar(msSQL);
            int rejectedbusiness_count = Convert.ToInt16(values.rejectedbusiness_count);

            msSQL = "select count(application_gid) as rejectedcredit_count from ocs_mst_tapplication a " +
                   "left join  hrm_mst_temployee b on b.employee_gid = a.created_by " +
                   "left join  adm_mst_tuser c on c.user_gid = b.user_gid where a.created_by = '" + employee_gid + "' and a.approval_status in ('Rejected By Credit','Rejected by Credit Manager') ";
            values.rejectedcredit_count = objdbconn.GetExecuteScalar(msSQL);
            int rejectedcredit_count = Convert.ToInt16(values.rejectedcredit_count);

            msSQL = "select count(application_gid) as rejectedcc_count from ocs_mst_tapplication a " +
                   "left join  hrm_mst_temployee b on b.employee_gid = a.created_by " +
                   "left join  adm_mst_tuser c on c.user_gid = b.user_gid where a.created_by = '" + employee_gid + "' and a.approval_status = 'CC Rejected'";
            values.rejectedcc_count = objdbconn.GetExecuteScalar(msSQL);
            int rejectedcc_count = Convert.ToInt16(values.rejectedcc_count);

        }

        public void DaGetInstitutionGSTList(string employee_gid, MdlMstGST values)
        {

            msSQL = " select institution2branch_gid,apifetch_flag,gst_state,gst_no, gst_registered,headoffice_status " +
                    " from ocs_mst_tinstitution2branch where institution_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstgst_list = new List<mstgst_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstgst_list.Add(new mstgst_list
                    {
                        institution2branch_gid = (dr_datarow["institution2branch_gid"].ToString()),
                        gstsource = (dr_datarow["apifetch_flag"].ToString()),
                        gst_state = (dr_datarow["gst_state"].ToString()),
                        gst_no = (dr_datarow["gst_no"].ToString()),
                        gst_registered = (dr_datarow["gst_registered"].ToString()),
                        headoffice_status = (dr_datarow["headoffice_status"].ToString())
                    });
                }
                values.mstgst_list = getmstgst_list;
            }
            dt_datatable.Dispose();
        }
        public bool DaPostProductDetailAdd(string employee_gid, MdlMstProductDetailAdd values)
        {
            string variety_gid = string.Empty;
            string variety_name = string.Empty;

            if (values.variety_list != null)
            {
                for (var i = 0; i < values.variety_list.Length; i++)
                {
                    variety_gid += values.variety_list[i].variety_gid + ",";
                    variety_name += values.variety_list[i].variety_name + ",";

                }

                variety_gid = variety_gid.TrimEnd(',');
                variety_name = variety_name.TrimEnd(',');
            }
            msGetGid = objcmnfunctions.GetMasterGID("AP2P");
            msSQL = " insert into ocs_mst_tapplication2product (" +
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
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "null," +
                    "'" + employee_gid + "'," +
                    "'" + values.product_gid + "'," +
                    "'" + values.product_name + "'," +
                    "'" + variety_gid + "'," +
                    "'" + variety_name + "'," +
                    "'" + values.sector_name + "'," +
                    "'" + values.category_name + "'," +
                    "'" + values.botanical_name + "'," +
                    "'" + values.alternative_name + "'," +
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
                values.status = false;
                values.message = "Error Occured";
                return false;
            }
        }

        public void DaGetProductDetailList(string employee_gid, MdlMstProductDetailList values)
        {
            msSQL = " select application2product_gid,product_gid,product_name,variety_gid,variety_name,sector_name,category_name," +
                    " botanical_name,alternative_name from ocs_mst_tapplication2product where application_gid='" + employee_gid + "'";
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
                        alternative_name = (dr_datarow["alternative_name"].ToString())
                    });
                }
                values.mstproduct_list = getmstproduct_list;
            }
            dt_datatable.Dispose();
        }

        public void DaDeleteProductDetail(string application2product_gid, MdlMstProductDetailAdd values, string employee_gid)
        {
            msSQL = "delete from ocs_mst_tapplication2product where application2product_gid='" + application2product_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.message = "Product Details Deleted Successfully";
                values.status = true;
                msSQL = " select application2product_gid,product_gid,product_name,variety_gid,variety_name,sector_name,category_name," +
                        " botanical_name,alternative_name,application2loan_gid from ocs_mst_tapplication2product where application2loan_gid='" + employee_gid + "'";
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
                            application2loan_gid = (dr_datarow["application2loan_gid"].ToString())
                        });
                    }
                    values.mstproduct_list = getmstproduct_list;
                }
                dt_datatable.Dispose();
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }
        public void DaSubmitGeneralDtl(MdlMstApplicationAdd values, string employee_gid)
        {

            msSQL = "select application_gid from tmp_application where employee_gid='" + employee_gid + "'";
            lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);
            if (lsapplication_gid == "" || lsapplication_gid == null)
            {
                lsapplication_gid = values.application_gid;
            }
            msSQL = "select vertical_refno from ocs_mst_tvertical where vertical_gid='" + values.vertical_gid + "'";
            string lsvertical_refno = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select entity_gid from ocs_mst_tvertical where vertical_gid='" + values.vertical_gid + "'";
            string lsentity_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select entity_code from adm_mst_tentity where entity_gid='" + lsentity_gid + "'";
            string lsentity_code = objdbconn.GetExecuteScalar(msSQL);

            string lsapp_refno = "ARN" + lsentity_code + lsvertical_refno + DateTime.Now.ToString("ddMMyyyy");

            string msGETRef = objcmnfunctions.GetMasterGID("APP");
            msGETRef = msGETRef.Replace("APP", "");

            lsapp_refno = lsapp_refno + msGETRef + "IN01";

            msGetGid = objcmnfunctions.GetMasterGID("APPC");
            values.application_gid = msGetGid;
            string gsvernacularlanguage_gid = string.Empty;
            string gsvernacular_language = string.Empty;
            if (values.vernacularlanguage_list != null)
            {
                for (var i = 0; i < values.vernacularlanguage_list.Length; i++)
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


            msSQL = " select concat( g.user_firstname, ' ', g.user_lastname, ' / ', g.user_code) as level_one ,a.employeereporting_to from adm_mst_tmodule2employee a " +
                    " left join hrm_mst_temployee f on f.employee_gid = a.employeereporting_to " +
                    " left join adm_mst_tuser g on g.user_gid = f.user_gid " +
                    " where  a.module_gid in  (select module_gid_parent from adm_mst_tmodule where module_gid in  " +
                    " (select modulereportingto_gid from adm_mst_tcompany)) and g.user_status = 'Y' and a.employee_gid ='" + employee_gid + "' " +
                    "  group by a.employee_gid ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsdrm_gid = objODBCDatareader["employeereporting_to"].ToString();
                lsdrm_name = objODBCDatareader["level_one"].ToString();
            }
            objODBCDatareader.Close();

            if (lsapplication_gid == "" || lsapplication_gid == null)
            {
                string lsclusterhead, lsregionalhead, lszonalhead, lsbusinesshead;
                string lsclusterheadgid, lsregionalheadgid, lszonalheadgid, lsbusinessheadgid;
                string lsbaselocationgid, lsclustergid, lsregiongid, lszonalgid;
                string lsbaselocationname, lsclustername, lsregionname, lszonalname;
                msSQL = " select distinct b.baselocation_gid,b.baselocation_name,c.cluster_gid,c.cluster_name,c.employee_name as clusterhead,c.vertical_name as clustervertical," +
                        " e.region_gid,e.region_name,e.employee_name as regionhead,g.zonal_gid,g.zonal_name,g.employee_name as zonalhead ," +
                        " c.employee_gid as clusterhead_gid,e.employee_gid as regionhead_gid, " +
                        " g.employee_gid as zonalhead_gid,h.employee_gid as businesshead_gid, " +
                        " h.employee_name as businesshead from hrm_mst_temployee a" +
                        " left join sys_mst_tcluster2baselocation b on b.baselocation_gid = a.baselocation_gid" +
                        " left  join sys_mst_tclusterhead c on b.cluster_gid = c.cluster_gid" +
                        " left join sys_mst_tregion2cluster d on c.cluster_gid = d.cluster_gid" +
                        " left join sys_mst_tregionhead e on d.region_gid = e.region_gid" +
                        " left join sys_mst_tzone2region f on f.region_gid = d.region_gid" +
                        " left join sys_mst_tzonalhead g on f.zone_gid = g.zonal_gid" +
                        " left join sys_mst_tbusinesshead h on h.zone_gid = g.zonal_gid where a.employee_gid = '" + employee_gid + "' and" +
                        " c.vertical_gid = '" + values.vertical_gid + "'" +
                        " and e.vertical_gid = '" + values.vertical_gid + "' and " +
                        " g.vertical_gid = '" + values.vertical_gid + "' and h.vertical_gid = '" + values.vertical_gid + "'" +
                        " and c.program_gid = '" + values.program_gid + "' and e.program_gid = '" + values.program_gid + "' and " +
                        " g.program_gid = '" + values.program_gid + "' and h.program_gid = '" + values.program_gid + "' " +
                        " and e.status='Y' and g.status='Y' and h.status='Y' and c.status='Y'";
                objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader1.HasRows == true)
                {
                    lsclusterhead = objODBCDatareader1["clusterhead"].ToString();
                    lsregionalhead = objODBCDatareader1["regionhead"].ToString();
                    lszonalhead = objODBCDatareader1["zonalhead"].ToString();
                    lsbusinesshead = objODBCDatareader1["businesshead"].ToString();
                    lsclusterheadgid = objODBCDatareader1["clusterhead_gid"].ToString();
                    lsregionalheadgid = objODBCDatareader1["regionhead_gid"].ToString();
                    lszonalheadgid = objODBCDatareader1["zonalhead_gid"].ToString();
                    lsbusinessheadgid = objODBCDatareader1["businesshead_gid"].ToString();
                    lsbaselocationgid = objODBCDatareader1["baselocation_gid"].ToString();
                    lsbaselocationname = objODBCDatareader1["baselocation_name"].ToString();
                    lsclustergid = objODBCDatareader1["cluster_gid"].ToString();
                    lsclustername = objODBCDatareader1["cluster_name"].ToString();
                    lsregiongid = objODBCDatareader1["region_gid"].ToString();
                    lsregionname = objODBCDatareader1["region_name"].ToString();
                    lszonalgid = objODBCDatareader1["zonal_gid"].ToString();
                    lszonalname = objODBCDatareader1["zonal_name"].ToString();
                    msSQL = " insert into ocs_mst_tapplication(" +
                    " application_gid," +
                    " application_no," +

                    " customerref_name," +
                    " vertical_gid," +
                    " vertical_name," +
                    " constitution_gid," +
                    " constitution_name," +
                    " sa_status," +
                    " saname_gid," +
                    " sa_name," +
                    " relationshipmanager_name," +
                    " relationshipmanager_gid," +
                    " drm_gid," +
                    " drm_name," +
                    " vernacular_language," +
                    " vernacularlanguage_gid," +
                    " contactpersonfirst_name," +
                    " contactpersonmiddle_name," +
                    " contactpersonlast_name," +
                    " designation_gid," +
                    " designation_type," +
                    " landline_no," +
                    " baselocation_gid," +
                    " baselocation_name," +
                    " cluster_gid," +
                    " cluster_name," +
                    " region_gid," +
                    " region_name," +
                    " zone_gid," +
                    " zone_name," +
                    " clustermanager_gid," +
                    " clustermanager_name," +
                    " zonalhead_name," +
                    " zonalhead_gid," +
                    " regionalhead_name," +
                    " regionalhead_gid," +
                    " businesshead_name," +
                    " businesshead_gid," +
                    " creditgroup_gid," +
                    " creditgroup_name," +
                    " program_gid," +
                    " program_name," +
                    " product_gid," +
                    " product_name," +
                    " variety_gid," +
                    " variety_name," +
                    " sector_name," +
                    " category_name," +
                    " botanical_name," +
                    " alternative_name," +
                    " refapplication_gid, " +
                    " business_activities, " +
                    " status," +
                    " created_by," +
                    " created_date) values(" +
                        "'" + msGetGid + "'," +
                        "'" + lsapp_refno + "'," +

                        "'" + values.customer_name + "'," +
                        "'" + values.vertical_gid + "'," +
                        "'" + values.vertical_name + "'," +
                        "'" + values.constitution_gid + "'," +
                        "'" + values.constitution_name + "'," +
                        "'" + values.sa_status + "'," +
                        "'" + values.saname_gid + "'," +
                        "'" + values.sa_name + "'," +
                        "'" + lsemployee_name + "'," +
                        "'" + employee_gid + "'," +
                        "'" + lsdrm_gid + "'," +
                        "'" + lsdrm_name + "'," +
                        "'" + gsvernacular_language + "'," +
                        "'" + gsvernacularlanguage_gid + "'," +
                        "'" + values.contactpersonfirst_name + "'," +
                        "'" + values.contactpersonmiddle_name + "'," +
                        "'" + values.contactpersonlast_name + "'," +
                        "'" + values.designation_gid + "'," +
                        "'" + values.designation_type + "'," +
                        "'" + values.landline_no + "'," +
                        "'" + lsbaselocationgid + "'," +
                        "'" + lsbaselocationname + "'," +
                        "'" + lsclustergid + "'," +
                        "'" + lsclustername + "'," +
                        "'" + lsregiongid + "'," +
                        "'" + lsregionname + "'," +
                        "'" + lszonalgid + "'," +
                        "'" + lszonalname + "'," +
                        "'" + lsclusterheadgid + "'," +
                        "'" + lsclusterhead + "'," +
                        "'" + lszonalhead + "'," +
                        "'" + lszonalheadgid + "'," +
                        "'" + lsregionalhead + "'," +
                        "'" + lsregionalheadgid + "'," +
                        "'" + lsbusinesshead + "'," +
                        "'" + lsbusinessheadgid + "'," +
                        "'" + values.creditgroup_gid + "'," +
                        "'" + values.creditgroup_name + "'," +
                        "'" + values.program_gid + "'," +
                        "'" + values.program_name + "'," +
                        "'" + values.product_gid + "'," +
                        "'" + values.product_name + "'," +
                        "'" + values.variety_gid + "'," +
                        "'" + values.variety_name + "'," +
                        "'" + values.sector_name + "'," +
                        "'" + values.category_name + "'," +
                        "'" + values.botanical_name + "'," +
                        "'" + values.alternative_name + "'," +
                        "''," +
                        "'" + values.business_activities + "'," +
                        "'Completed'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    objODBCDatareader1.Close();
                }
                else
                {
                    objODBCDatareader1.Close();
                    values.message = "Location / Vertical not Assign for Business Approval";
                    values.status = false;
                    return;
                }

                if (mnResult != 0)
                {
                    msSQL = "update ocs_mst_tapplication2product set application_gid='" + msGetGid + "' where application_gid='" + employee_gid + "'";
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
                    values.message = "Location / Vertical not Assign for Business Approval";
                    values.status = false;
                    return;
                }
            }
            else
            {
                string lsclusterhead, lsregionalhead, lszonalhead, lsbusinesshead;
                string lsclusterheadgid, lsregionalheadgid, lszonalheadgid, lsbusinessheadgid;
                string lsbaselocationgid, lsclustergid, lsregiongid, lszonalgid;
                string lsbaselocationname, lsclustername, lsregionname, lszonalname;
                msSQL = " select distinct b.baselocation_gid,b.baselocation_name,c.cluster_gid, c.cluster_name,c.employee_name as clusterhead,c.vertical_name as clustervertical," +
                    " e.region_gid,e.region_name,e.employee_name as regionhead,g.zonal_gid,g.zonal_name,g.employee_name as zonalhead ," +
                    " c.employee_gid as clusterhead_gid,e.employee_gid as regionhead_gid, " +
                    " g.employee_gid as zonalhead_gid,h.employee_gid as businesshead_gid, " +
                    " h.employee_name as businesshead from hrm_mst_temployee a" +
                    " left join sys_mst_tcluster2baselocation b on b.baselocation_gid = a.baselocation_gid" +
                    " left  join sys_mst_tclusterhead c on b.cluster_gid = c.cluster_gid" +
                    " left join sys_mst_tregion2cluster d on c.cluster_gid = d.cluster_gid" +
                    " left join sys_mst_tregionhead e on d.region_gid = e.region_gid" +
                    " left join sys_mst_tzone2region f on f.region_gid = d.region_gid" +
                    " left join sys_mst_tzonalhead g on f.zone_gid = g.zonal_gid" +
                    " left join sys_mst_tbusinesshead h on h.zone_gid = g.zonal_gid where a.employee_gid = '" + employee_gid + "' and" +
                    " c.vertical_gid = '" + values.vertical_gid + "'" +
                    " and e.vertical_gid = '" + values.vertical_gid + "' and " +
                    " g.vertical_gid = '" + values.vertical_gid + "' and h.vertical_gid = '" + values.vertical_gid + "'" +
                    " and c.program_gid = '" + values.program_gid + "' and e.program_gid = '" + values.program_gid + "' and " +
                    " g.program_gid = '" + values.program_gid + "' and h.program_gid = '" + values.program_gid + "' " +
                    " and e.status='Y' and g.status='Y' and h.status='Y' and c.status='Y'";
                objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader1.HasRows == true)
                {
                    lsclusterhead = objODBCDatareader1["clusterhead"].ToString();
                    lsregionalhead = objODBCDatareader1["regionhead"].ToString();
                    lszonalhead = objODBCDatareader1["zonalhead"].ToString();
                    lsbusinesshead = objODBCDatareader1["businesshead"].ToString();
                    lsclusterheadgid = objODBCDatareader1["clusterhead_gid"].ToString();
                    lsregionalheadgid = objODBCDatareader1["regionhead_gid"].ToString();
                    lszonalheadgid = objODBCDatareader1["zonalhead_gid"].ToString();
                    lsbusinessheadgid = objODBCDatareader1["businesshead_gid"].ToString();
                    lsbaselocationgid = objODBCDatareader1["baselocation_gid"].ToString();
                    lsbaselocationname = objODBCDatareader1["baselocation_name"].ToString();
                    lsclustergid = objODBCDatareader1["cluster_gid"].ToString();
                    lsclustername = objODBCDatareader1["cluster_name"].ToString();
                    lsregiongid = objODBCDatareader1["region_gid"].ToString();
                    lsregionname = objODBCDatareader1["region_name"].ToString();
                    lszonalgid = objODBCDatareader1["zonal_gid"].ToString();
                    lszonalname = objODBCDatareader1["zonal_name"].ToString();
                    msSQL = " update ocs_mst_tapplication set " +
                        " application_no='" + lsapp_refno + "'," +
                        " customer_urn='" + values.customer_urn + "'," +
                        " customerref_name='" + values.customer_name + "'," +
                        " vertical_gid='" + values.vertical_gid + "'," +
                        " vertical_name='" + values.vertical_name + "'," +
                        " constitution_gid='" + values.constitution_gid + "'," +
                        " constitution_name='" + values.constitution_name + "'," +
                        " sa_status='" + values.sa_status + "'," +
                        " sa_name='" + values.sa_name + "'," +
                        " saname_gid='" + values.saname_gid + "'," +
                        " vernacularlanguage_gid='" + gsvernacularlanguage_gid + "'," +
                        " vernacular_language='" + gsvernacular_language + "'," +
                        " contactpersonfirst_name='" + values.contactpersonfirst_name + "'," +
                        " contactpersonmiddle_name='" + values.contactpersonmiddle_name + "'," +
                        " contactpersonlast_name='" + values.contactpersonlast_name + "'," +
                        " designation_gid='" + values.designation_gid + "'," +
                        " designation_type='" + values.designation_type + "'," +
                        " landline_no='" + values.landline_no + "'," +
                        " cluster_gid='" + lsclustergid + "'," +
                        " cluster_name='" + lsclustername + "'," +
                        " region_gid='" + lsregiongid + "'," +
                        " region_name='" + lsregionname + "'," +
                        " zone_gid='" + lszonalgid + "'," +
                        " zone_name='" + lszonalname + "'," +
                        " drm_gid='" + lsdrm_gid + "'," +
                        " drm_name='" + lsdrm_name + "'," +
                        " clustermanager_gid='" + lsclusterheadgid + "'," +
                        " clustermanager_name='" + lsclusterhead + "'," +
                        " zonalhead_name='" + lszonalhead + "'," +
                        " zonalhead_gid='" + lszonalheadgid + "'," +
                        " regionalhead_name='" + lsregionalhead + "'," +
                        " regionalhead_gid='" + lsregionalheadgid + "'," +
                        " businesshead_name='" + lsbusinesshead + "'," +
                        " businesshead_gid='" + lsbusinessheadgid + "'," +
                        " creditgroup_gid= '" + values.creditgroup_gid + "'," +
                        " creditgroup_name= '" + values.creditgroup_name + "'," +
                        " product_gid= '" + values.product_gid + "'," +
                        " product_name='" + values.product_name + "'," +
                        " variety_gid= '" + values.variety_gid + "'," +
                        " variety_name='" + values.variety_name + "'," +
                        " sector_name= '" + values.sector_name + "'," +
                        " category_name='" + values.category_name + "'," +
                        " botanical_name= '" + values.botanical_name + "'," +
                        " alternative_name='" + values.alternative_name + "'," +
                        " status = 'Completed'," +
                        " updated_by='" + employee_gid + "'," +
                        " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                        " where application_gid='" + lsapplication_gid + "' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    objODBCDatareader1.Close();
                }
                else
                {
                    objODBCDatareader1.Close();
                    values.message = "Location / Vertical not Assign for Business Approval";
                    values.status = false;
                    return;
                }
                if (mnResult != 0)
                {
                    msSQL = "update ocs_mst_tapplication2product set application_gid='" + lsapplication_gid + "' where application_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    values.message = "General Information Submitted Successfully";
                    values.status = true;
                }
                else
                {
                    values.message = "Error Occured While Submitting Information";
                    values.status = false;
                }
            }
        }

        public void GetTempApp(string employee_gid, result values)
        {

            msSQL = "delete from ocs_mst_tapplication2product where application_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            values.status = true;
        }



        public bool DaSubmitInstitutionDtl(MdlNgMstInstitutionAdd values, string employee_gid)
        {



            msSQL = "select application_gid from ocs_mst_tapplication where employee_gid='" + employee_gid + "'";
            lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);




            msGetGid = objcmnfunctions.GetMasterGID("APIN");
            values.institution_gid = msGetGid;
            msSQL = " insert into ocs_mst_tinstitution(" +
                    " institution_gid," +
                    " application_gid," +
                    " company_name," +
                    " lei_no," +
                    " renewaldue_date," +
                    " cin_date," +
                    " kin_no," +
                    " businessstart_date," +
                    " companypan_no," +
                    " cin_no," +
                    " amlcategory_gid," +
                    " amlcategory_name," +
                    " businesscategory_gid," +
                    " businesscategory_name," +
                    " urn_status," +
                    " urn," +
                    " institution_status," +
                    " nearsamunnatiabranch_gid," +
                    " nearsamunnatiabranch_name," +
                    " udhayam_registration," +
                    " tan_number," +
                    " tanstate_gid," +
                    " tanstate_name," +
                    " created_by," +
                    " created_date) values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.application_gid + "'," +
                    "'" + values.company_name.Replace("'", "\\'") + "'," +
                    "'" + values.lei_no + "',";
            if ((values.renewaldue_date == null) || (values.renewaldue_date == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.renewaldue_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            msSQL += "'" + values.cin_date + "'," +
                    "'" + values.kin_no + "',";
            if ((values.businessstartdate == null) || (values.businessstartdate == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.businessstartdate).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            msSQL += "'" + values.companypan_no + "'," +
                    "'" + values.cin_no + "'," +
                    "'" + values.amlcategory_gid + "'," +
                    "'" + values.amlcategory_name + "'," +
                    "'" + values.businesscategory_gid + "'," +
                    "'" + values.businesscategory_name + "'," +
                    "'" + values.urn_status + "'," +
                    "'" + values.urn + "'," +
                    "'Completed'," +
                    "'" + values.nearsamunnatiabranch_gid + "'," +
                    "'" + values.nearsamunnatiabranch_name + "'," +
                    "'" + values.udhayam_registration + "'," +
                    "'" + values.tan_number + "'," +
                    "'" + values.tanstate_gid + "'," +
                    "'" + values.tanstate_name + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                //msSQL = "update ocs_mst_tinstitution2branch set institution_gid='" + msGetGid + "' where institution_gid='" + employee_gid + "'";
                //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);




                msSQL = "update ocs_mst_tkycgstsbpan set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tkycudyamauthentication set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);



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

        public bool DaPostInstitutionGST(string employee_gid, MdlMstGST values)
        {
            string lsapifetch_flag;
            if (values.apifetch_flag == "GST Portal")
            {
                lsapifetch_flag = "Y";
            }
            else
            {
                lsapifetch_flag = "N";
            }
            if (values.gst_no.Length > 2)
            {
                msSQL = "select institution2branch_gid from ocs_mst_tinstitution2branch where gst_no='" + values.gst_no + "' and institution_gid='" + values.institution_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objODBCDatareader.Close();
                    values.status = false;
                    values.message = "Already Added";
                    return false;
                }
                objODBCDatareader.Close();
                msGetGid = objcmnfunctions.GetMasterGID("ITGS");
                msSQL = " insert into ocs_mst_tinstitution2branch(" +
                        " institution2branch_gid," +
                        " institution_gid," +
                        " gst_state," +
                        " gst_no," +
                        " gst_registered," +
                        " headoffice_status, " +
                        " apifetch_flag," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + employee_gid + "'," +
                        "'" + values.gst_state + "'," +
                        "'" + values.gst_no + "'," +
                        "'" + values.gst_registered + "'," +
                        "'" + values.headoffice_status + "'," +
                        "'" + lsapifetch_flag + "'," +
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
            else
            {
                values.status = false;
                values.message = "Kindly fill valid gst";
                return false;
            }
        }
    


        public void DaPostProduct(string employee_gid, MdlMstProductDetailAdd values)
        {
            string variety_gid = string.Empty;
            string variety_name = string.Empty; 

            if (values.variety_list != null)
            {
                for (var i = 0; i < values.variety_list.Length; i++)
                {
                    variety_gid += values.variety_list[i].variety_gid + ",";
                    variety_name += values.variety_list[i].variety_name + ",";

                }

                variety_gid = variety_gid.TrimEnd(',');
                variety_name = variety_name.TrimEnd(',');
            }
            msGetGid = objcmnfunctions.GetMasterGID("AP2P");
            msSQL = " insert into ocs_mst_tapplication2product (" +
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
                    " csacommodity_average," +
                    " csapercentageoftotal_limit," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "null," +
                    "'" + values.product_gid + "'," +
                    "'" + values.product_name + "'," +
                    "'" + variety_gid + "'," +
                    "'" + variety_name + "'," +
                    "'" + values.sector_name + "'," +
                    "'" + values.category_name + "'," +
                    "'" + values.botanical_name + "'," +
                    "'" + values.alternative_name + "'," +
                    "'" + values.csacommodity_average + "'," +
                    "'" + values.csapercentageoftotal_limit + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Product Details Added successfully";

            }
            else
            {
                values.status = false;
                values.message = "Error Occured while Adding Product";
            }
        }
        public void DaGetProductDetails(string employee_gid, MdlMstProductDetailList values)
        {
            msSQL = " select application2product_gid,product_gid,product_name,variety_gid,variety_name,sector_name,category_name," +
                    " botanical_name,alternative_name from ocs_mst_tapplication2product where application2loan_gid='" + employee_gid + "'";
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
                        alternative_name = (dr_datarow["alternative_name"].ToString())
                    });
                }
                values.mstproduct_list = getmstproduct_list;
            }
            dt_datatable.Dispose();
        }
        public void GetTempProduct(string employee_gid, result values)
        {

            msSQL = "delete from ocs_mst_tapplication2product where application2loan_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            values.status = true;
        }
        public void DaIndividualSubmit(string employee_gid, MdlMstContact values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("CTCT");
            msSQL = " insert into ocs_mst_tcontact(" +
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
                              " nearsamunnatiabranch_gid," +
                              " nearsamunnatiabranch_name," +
                              " physicalstatus_gid," +
                              " physicalstatus_name," +
                              " internalrating_gid," +
                              " internalrating_name," +
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
            msSQL += " null," +
                      "null," +
                     "null," +
                    "null," +
                      "null," +
                     "null," +
                     "null," +
                     "null," +
                     "'Completed'," +
                     "'" + values.nearsamunnatiabranch_gid + "'," +
                     "'" + values.nearsamunnatiabranch_name + "'," +
                     "'" + values.physicalstatus_gid + "'," +
                     "'" + values.physicalstatus_name + "'," +
                      " null," +
                      "null," +
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
                        msSQL = " INSERT INTO ocs_mst_tcontact2panabsencereason(" +
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


                msSQL = "update ocs_mst_tcontact2panform60 set contact_gid ='" + msGetGid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " select concat(first_name,middle_name,last_name) as customer_name,contact_gid  from ocs_mst_tcontact where" +
                               " contact_gid ='" + msGetGid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lscustomer_name = objODBCDatareader["customer_name"].ToString();




                    //Region


                    msSQL = " update ocs_mst_tapplication set customer_name='" + lscustomer_name.Replace("'", "\\'") + "'," +
                            " region='" + lsregion + "'," +
                            " customer_urn='" + values.urn + "'," +
                            " applicant_type='Individual'," +
                            " updated_by='" + employee_gid + "'," +
                            " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                            " where application_gid='" + lsapplication_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    values.status = true;
                    values.message = "Individual Details Submitted Successfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Individual Details Submitted Successfully";
                }


            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
            }

        }
        public void Dastakeholderindividuallistsummary(string application_gid, MdlMstProductDetailList values)
        {
            msSQL = " select contact_gid,pan_no,concat(first_name,middle_name,last_name) as customer_name ,designation_name" +
                    " from ocs_mst_tcontact where application_gid='" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmststakecontat_list = new List<mststakecontact_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmststakecontat_list.Add(new mststakecontact_list
                    {
                        pan_no = (dr_datarow["pan_no"].ToString()),
                        customer_name = (dr_datarow["customer_name"].ToString()),
                        designation_name = (dr_datarow["designation_name"].ToString()),
                        contact_gid = (dr_datarow["contact_gid"].ToString()),

                    });
                }
                values.mststakecontact_list = getmststakecontat_list;
            }
            dt_datatable.Dispose();
        }


        public void DaUpdateGSTHeadOffice(string employee_gid, MdlGSTHeadOffice values)
        {
            
            msSQL = " update ocs_mst_tinstitution2branch set headoffice_status='No' " +
                      " where institution_gid='" + employee_gid + "'";
               mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

            
                values.status = true;
                values.message = "Head Office Confirmed Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
            }

        }

        public bool DaSubmitInstitutionAddressDtl(MdlNgMstInstitutionAdd values, string employee_gid)
        {

            msSQL = "select institution2address_gid from ocs_mst_tinstitution2address where institution_gid='" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {

                msSQL = "update ocs_mst_tinstitution2address set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {


                    values.message = "Institution Address Information Submitted Successfully";
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
            else 
            { 
                values.status = false;
                values.message = "Add Atleast One Address Detail";
                return false;
            }
            objODBCDatareader.Close();

        }

        public bool SubmitGSTDtl(MdlNgMstInstitutionAdd values, string employee_gid)
        {

            msSQL = "select institution2address_gid from ocs_mst_tinstitution2address where institution_gid='" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {

                msSQL = "update ocs_mst_tinstitution2branch set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {


                    values.message = "GST Information Submitted Successfully";
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
            else
            {
                values.status = false;
                values.message = "Add Atleast One Address Detail";
                return false;
            }
            objODBCDatareader.Close();

        }

        public void DaGetInstitutionAddressList(string employee_gid, MdlMstAddressDetails values)
        {
            msSQL = "  select institution2address_gid,addresstype_name,primary_status, addressline1, addressline2, taluka, district, state, country, landmark, latitude, longitude," +
                    " postal_code from ocs_mst_tinstitution2address where institution_gid='" + employee_gid + "' and apifetch_flag='N'";
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
                        landmark = (dr_datarow["landmark"].ToString()),
                        latitude = (dr_datarow["latitude"].ToString()),
                        longitude = (dr_datarow["longitude"].ToString()),
                    });
                }
                values.mstaddress_list = getmstaddress_list;
            }
            dt_datatable.Dispose();
        }


        public void DaGetGeneticCodeList(string employee_gid, MdlGeneticCode values)
        {
            msSQL = " SELECT geneticcode_gid,geneticcodebusiness_name FROM ocs_mst_tgeneticcode order by geneticcode_gid asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplication_list = new List<geneticbuisness_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getapplication_list.Add(new geneticbuisness_list
                    {
                        geneticcode_gid = (dr_datarow["geneticcode_gid"].ToString()),
                        geneticcodebusiness_name = (dr_datarow["geneticcodebusiness_name"].ToString()),
                    });
                }
                values.geneticbuisness_list = getapplication_list;
            }
            dt_datatable.Dispose();
            values.status = true;
        }
        public void DaPostGeneticCode(string employee_gid, MdlMstBuisnessGeneticCode values)
        {
            try
            {
                if (values.GeneticCodes_list.Count > 0)
                {

                    for (int i = 0; i < values.GeneticCodes_list.Count; i++)
                    {
                        msSQL = "select geneticcode_gid from ocs_mst_tapplication2geneticcode where application_gid='" + employee_gid + "' and geneticcode_gid='" + values.GeneticCodes_list[i].geneticcode_gid + "'";
                        string lsgenetic_code = objdbconn.GetExecuteScalar(msSQL);
                        if (lsgenetic_code == (values.GeneticCodes_list[i].geneticcode_gid))
                        {

                            values.status = false;
                            values.message = "Already Genetic Code Added";
                            return;

                        }

                        msGetGid = objcmnfunctions.GetMasterGID("A2GC");
                        msSQL = " insert into ocs_mst_tapplication2geneticcode(" +
                               " application2geneticcode_gid," +
                               " application_gid," +
                               " geneticcode_gid," +
                               " geneticcodebusiness_name," +
                               " genetic_status," +
                               " genetic_remarks," +
                               " created_by," +
                               " created_date)" +
                               " values(" +
                               "'" + msGetGid + "'," +
                               "'" + employee_gid + "'," +
                               "'" + values.GeneticCodes_list[i].geneticcode_gid + "'," +
                               "'" + values.GeneticCodes_list[i].geneticcodebusiness_name.Replace("'", "\\'") + "'," +
                               "'" + values.GeneticCodes_list[i].status + "'," +
                               "'" + values.GeneticCodes_list[i].observation.Replace("'", "\\'") + "'," +
                               "'" + employee_gid + "'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    values.status = true;
                    values.message = "Genetic Code Added Successfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Genetic Code Added unsuccessfully";
                }

            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Exception:" + ex.ToString();
            }

        }

        public void DaGetDocumentListSummary(string application_gid, string type,string stackholder_gid, applicationlistdocument values)
        {
            msSQL = "select program_gid from ocs_mst_tapplication where application_gid='" + application_gid + "'";
            program = objdbconn.GetExecuteScalar(msSQL);

            if (type == "individual")
            {
                msSQL = " select a.documenttypes_gid,a.documenttype_name,b.program_gid,a.individualdocument_gid,a.individualdocument_name from ocs_mst_tindividualdocument a " +
                    " left join ocs_mst_tindividualdocumentprogram b on a.individualdocument_gid = b.individualdocument_gid " +
                    " where status='Y' and b.program_gid in ('" + program + "') order by documenttypes_gid";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getdocument_list = new List<applicationlistdocument_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getdocument_list.Add(new applicationlistdocument_list
                        {
                            documenttypes_gid = (dr_datarow["documenttypes_gid"].ToString()),
                            documenttype_name = (dr_datarow["documenttype_name"].ToString()),
                            document_gid = (dr_datarow["individualdocument_gid"].ToString()),
                            document_name = (dr_datarow["individualdocument_name"].ToString()),
                            document_count = Document_count_check(type, stackholder_gid, (dr_datarow["documenttypes_gid"].ToString()), (dr_datarow["individualdocument_gid"].ToString())),
                        });
                    }
                    values.applicationlistdocument_list = getdocument_list;
                    values.status = true;
                }
                dt_datatable.Dispose();
            }
            else if (type == "institution")
            {
                msSQL = " select a.documenttypes_gid,a.documenttype_name,b.program_gid,a.companydocument_gid,a.companydocument_name from ocs_mst_tcompanydocument a " +
                        " left join ocs_mst_tcompanydocumentprogram b on a.companydocument_gid = b.companydocument_gid " +
                        " where status='Y' and b.program_gid in ('" + program + "') order by documenttypes_gid";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getdocument_list = new List<applicationlistdocument_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getdocument_list.Add(new applicationlistdocument_list
                        {
                            documenttypes_gid = (dr_datarow["documenttypes_gid"].ToString()),
                            documenttype_name = (dr_datarow["documenttype_name"].ToString()),
                            document_gid = (dr_datarow["companydocument_gid"].ToString()),
                            document_name = (dr_datarow["companydocument_name"].ToString()),
                            document_count = Document_count_check(type, stackholder_gid, (dr_datarow["documenttypes_gid"].ToString()), (dr_datarow["companydocument_gid"].ToString())),
                        });
                    }
                    values.applicationlistdocument_list = getdocument_list;
                    values.status = true;
                }
                dt_datatable.Dispose();
            }
        }
        public string Document_count_check(string type, string stackholder_gid, string documenttypes_gid, string document_gid)
        {
            string count= null;
            if (type == "institution")
            {
                msSQL = " select count(*) from ocs_mst_tinstitution2documentupload where institution_gid = '" + stackholder_gid + "' " +
                        " and documenttype_gid = '" + documenttypes_gid + "' and companydocument_gid = '" + document_gid + "'";
                count = objdbconn.GetExecuteScalar(msSQL);
            }
            else if (type == "individual")
            {
                msSQL = " select count(*) from ocs_mst_tcontact2document where contact_gid = '" + stackholder_gid + "' " +
                        " and documenttype_gid = '" + documenttypes_gid + "' and individualdocument_gid = '" + document_gid + "'";
                count = objdbconn.GetExecuteScalar(msSQL);
            }
            return count;
        }
        public string DaDocumentUpload(HttpResponseMessage request)
        {
            string filestatus = null;
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    var filePath = HttpContext.Current.Server.MapPath("~/UploadFile/" + postedFile.FileName);
                    postedFile.SaveAs(filePath);
                }
            }
            filestatus = "true";
            return filestatus;
        }

        public void DaGetDocumentSummary(string application_gid, applicationdocument values)
        {
            msSQL = " (select application_gid, institution_gid as stackholder_gid, stakeholder_type, 'institution' as type, "+
                    " company_name as name from ocs_mst_tinstitution where application_gid = '" + application_gid + "')"+
                    " union " + 
                    " (select application_gid, contact_gid as stackholder_gid, stakeholder_type, 'individual' as type,"+
                    " concat(first_name, ' ', middle_name, ' ', last_name) as name from ocs_mst_tcontact  where application_gid = '"+ application_gid + "')";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdocument_list = new List<applicationdocument_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getdocument_list.Add(new applicationdocument_list
                    {
                        application_gid = (dr_datarow["application_gid"].ToString()),
                        stackholder_gid = (dr_datarow["stackholder_gid"].ToString()),
                        stakeholder_type = (dr_datarow["stakeholder_type"].ToString()),
                        type = (dr_datarow["type"].ToString()),
                        name = (dr_datarow["name"].ToString()),
                        document_count = documentcount((dr_datarow["application_gid"].ToString()), (dr_datarow["type"].ToString()))
                    });
                }
                values.applicationdocument_list = getdocument_list;
                values.status = true;
            }
            dt_datatable.Dispose();
        }

public string documentcount(string application_gid, string type) 
        {
            msSQL = "select program_gid from ocs_mst_tapplication where application_gid='" + application_gid + "'";
            program = objdbconn.GetExecuteScalar(msSQL);

            if(type == "institution") 
            { 
                msSQL = "SELECT count(*) FROM ocs_mst_tcompanydocument a left join ocs_mst_tcompanydocumentprogram b on a.companydocument_gid = b.companydocument_gid " +
                        " where status='Y' and b.program_gid in ('" + program + "')";
                document_count = objdbconn.GetExecuteScalar(msSQL);
            }
            else if(type == "individual")
            {
                msSQL = "SELECT count(*) FROM ocs_mst_tindividualdocument a left join ocs_mst_tindividualdocumentprogram b on a.individualdocument_gid = b.individualdocument_gid " +
                        "where status = 'Y' and b.program_gid in ('" + program + "')";
                document_count = objdbconn.GetExecuteScalar(msSQL);
            }
            return document_count;
        }


        //Bureau 
        public void DaGetapplicationBureauList(string application_gid, MdlContactBureau values)
        {
            msSQL = "(select   a.application_gid,a.institution_gid as stackeholder_gid,stakeholder_type,'institution' as type," +
           "a.company_name as name,b.institution2bureau_gid as bureau_gid,b.bureauname_name,b.bureau_score,date_format" +
           "(b.bureauscore_date, '%d-%m-%Y') as bureauscore_date from ocs_mst_tinstitution a " +
           "left join ocs_mst_tinstitution2bureau  b on a.institution_gid = b.institution_gid  where a.application_gid='" + application_gid + "')" +
           "union" +
           "(select a.application_gid,a.contact_gid as stackeholder_gid, stakeholder_type,'individual'as type,concat(a.first_name,'',a.last_name,'',a.middle_name) as name," +
           "b.contact2bureau_gid as bureau_gid,b.bureauname_name,b.bureau_score,date_format(b.bureauscore_date, '%d-%m-%Y') as bureauscore_date from ocs_mst_tcontact  a " +
           "left join ocs_mst_tcontact2bureau b on a.contact_gid = b.contact_gid where a.application_gid='" + application_gid + "')";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getBureau_list = new List<Bureau_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getBureau_list.Add(new Bureau_list
                    {
                        application_gid = (dr_datarow["application_gid"].ToString()),
                        stackholder_gid = (dr_datarow["stackeholder_gid"].ToString()),
                        stakeholder_type = (dr_datarow["stakeholder_type"].ToString()),
                        type = (dr_datarow["type"].ToString()),
                        name = (dr_datarow["name"].ToString()),

                    });
                }
            }
            values.Bureau_list = getBureau_list;
            values.status = true;
            dt_datatable.Dispose();
        }
 public bool DaSubmitIndividualAddressDtl(MdlContactAddress values, string employee_gid)
        {

            msSQL = "select contact2address_gid from ocs_mst_tcontact2address where contact2address_gid='" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {

                msSQL = "update ocs_mst_tcontact2address set contact2address_gid='" + values.contact2address_gid + "' where contact2address_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {


                    values.message = "Individual Address Information Submitted Successfully";
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
            else
            {
                values.status = false;
                values.message = "Add Atleast One Address Detail";
                return false;
            }
            objODBCDatareader.Close();

        }


//Summary Borrower Details --> Individual --->Address Form -->Address List Show FunctionLity

        public void DaGetAddressList(string employee_gid, MdlContactAddress values)
        {
            msSQL = " select contact2address_gid,addresstype_name,primary_status, addressline1, addressline2, taluka, district, state, country, latitude, longitude,landmark," +
                    " postal_code from ocs_mst_tcontact2address where contact_gid='" + employee_gid + "'";
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
                        latitude = (dr_datarow["latitude"].ToString()),
                        longitude = (dr_datarow["longitude"].ToString()),
                        postal_code = (dr_datarow["postal_code"].ToString()),
                        landmark = (dr_datarow["landmark"].ToString())
                    });
                }
                values.contactaddress_list = getcontactaddress_list;
            }
            dt_datatable.Dispose();

        }

    }

}
