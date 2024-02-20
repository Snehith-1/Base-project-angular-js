using ems.master.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Text.RegularExpressions;
using ems.storage.Functions;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace ems.master.DataAccess
{
    public class DaMstLSA
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        DataTable dt_datatable, dt_datatable1, dt_datatable2;
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        string msSQL, msGetGid, msGetdocGid, msGetcmckGid, msGetacdtlGid, generatelsa_gid, msGetprstypGid;
        int mnResult;
        OdbcDataReader objODBCDataReader, objODBCDatareader1;
        string lspath, lscloudpath, lsname;
        HttpPostedFile httpPostedFile;
        double dbldocumented_limit, dblexisting_limit, dbllimit_released ,dbllsadocumented_limit,dbllsaexisting_limit,dblreleased_amount, dbllsatotalreleased_amount, dbllsareleased_amount, dbllsatotalreleased_approved;
        string lsreinitiate_eligibleflag, lsatotdoclimit_flag, lslimitproductinfodtl_gid;

        string lsapplication_gid, lsapplication2sanction_gid, lsprodreinitiate_flag;

        public void DaGetLSAMakerSummary(string employee_gid, MdlLSAMakerSummaryList values)
        {
            msSQL = " select a.application_gid,e.application2sanction_gid,a.application_no,a.customer_urn,a.customer_name as customer_name, " +
                     " date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as ccapproved_date,e.sanction_refno, " +
                     " date_format(e.sanction_date, '%d-%m-%Y') as sanction_date, " +
                     " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as cadaccepted_by, " +
                     " date_format(a.processupdated_date, '%d-%m-%Y %h:%i %p') as cadaccepted_date,a.renewal_flag,a.enhancement_flag from ocs_trn_tcadapplication a " +
                     " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by " +
                     " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                     " left join ocs_trn_tprocesstype_assign d on d.application_gid = a.application_gid " +
                     " left join ocs_trn_tapplication2sanction e on a.application_gid= e.application_gid " +
                     " where a.process_type = 'Accept' and d.menu_gid='" + getMenuClass.LSA + "' and a.sanction_approvalflag='Y' and e.accepted_status ='Y' and  " +
                     " d.maker_gid = '" + employee_gid + "' and d.maker_approvalflag ='N' order by e.application2sanction_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getMdlLSAMakerSummary_list = new List<MdlLSAMakerSummary>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getMdlLSAMakerSummary_list.Add(new MdlLSAMakerSummary
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        ccapproved_date = dt["ccapproved_date"].ToString(),
                        cadaccepted_date = dt["cadaccepted_date"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        cadaccepted_by = dt["cadaccepted_by"].ToString(),
                        sanction_refno = dt["sanction_refno"].ToString(),
                        sanction_date = dt["sanction_date"].ToString(),
                        application2sanction_gid = dt["application2sanction_gid"].ToString(),
                        renewal_flag = dt["renewal_flag"].ToString(),
                        enhancement_flag = dt["enhancement_flag"].ToString()
                    });
                }
            }
            values.MdlLSAMakerSummary = getMdlLSAMakerSummary_list;
            dt_datatable.Dispose();
        }

        public void DaGetLSAFollowupMakerSummary(string employee_gid, MdlLSAMakerSummaryList values)
        {
            msSQL = " select a.application_gid,e.application2sanction_gid,a.application_no,a.customer_urn,a.customer_name as customer_name, " +
                     " date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as ccapproved_date,e.sanction_refno, " +
                     " date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as ccapproved_date, " +
                     " date_format(e.sanction_date, '%d-%m-%Y') as sanction_date, b.overall_lsastatus,a.renewal_flag,a.enhancement_flag  from ocs_trn_tcadapplication a " +
                     " left join ocs_trn_tgeneratelsa b on b.application_gid = a.application_gid " +
                     " left join ocs_trn_tprocesstype_assign d on d.application_gid = a.application_gid " +
                     " left join ocs_trn_tapplication2sanction e on a.application_gid= e.application_gid " +
                     " where a.process_type = 'Accept' and d.menu_gid='" + getMenuClass.LSA + "' and a.sanction_approvalflag='Y' and   e.accepted_status ='Y' and d.approver_approvalflag='N' and" +
                     " d.maker_gid = '" + employee_gid + "' and d.maker_approvalflag ='Y' and b.overall_lsastatus in ('Checker Approval Pending','Final Approval Pending') order by e.application2sanction_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getMdlLSAMakerSummary_list = new List<MdlLSAMakerSummary>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getMdlLSAMakerSummary_list.Add(new MdlLSAMakerSummary
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        ccapproved_date = dt["ccapproved_date"].ToString(),
                        sanction_refno = dt["sanction_refno"].ToString(),
                        sanction_date = dt["sanction_date"].ToString(),
                        application2sanction_gid = dt["application2sanction_gid"].ToString(),
                        overall_lsastatus = dt["overall_lsastatus"].ToString(),
                        renewal_flag = dt["renewal_flag"].ToString(),
                        enhancement_flag = dt["enhancement_flag"].ToString()
                    });
                }
            }
            values.MdlLSAMakerSummary = getMdlLSAMakerSummary_list;
            dt_datatable.Dispose();
        }

        public void DaGetLSAPendingCheckerSummary(string employee_gid, MdlLSACheckerSummaryList values)
        {
            msSQL = " select a.application_gid,e.application2sanction_gid,a.application_no,a.customer_urn,a.customer_name as customer_name, " +
                     " date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as ccapproved_date,e.sanction_refno, " +
                     " date_format(e.sanction_date, '%d-%m-%Y') as sanction_date, b.overall_lsastatus,a.renewal_flag,a.enhancement_flag  from ocs_trn_tcadapplication a " +
                     " left join ocs_trn_tgeneratelsa b on b.application_gid = a.application_gid " +
                     " left join ocs_trn_tprocesstype_assign d on d.application_gid = a.application_gid " +
                     " left join ocs_trn_tapplication2sanction e on a.application_gid= e.application_gid " +
                     " where a.process_type = 'Accept' and d.menu_gid='" + getMenuClass.LSA + "' and e.accepted_status ='Y' and " +
                     " d.checker_gid = '" + employee_gid + "' and d.maker_approvalflag ='Y' and d.checker_approvalflag='N' " +
                     " and d.approver_approvalflag='N' and b.overall_lsastatus not in ('Approved') order by e.application2sanction_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getMdlLSAMakerSummary_list = new List<MdlLSACheckerSummary>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getMdlLSAMakerSummary_list.Add(new MdlLSACheckerSummary
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        ccapproved_date = dt["ccapproved_date"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        sanction_refno = dt["sanction_refno"].ToString(),
                        sanction_date = dt["sanction_date"].ToString(),
                        application2sanction_gid = dt["application2sanction_gid"].ToString(),
                        overall_lsastatus = dt["overall_lsastatus"].ToString(),
                        renewal_flag = dt["renewal_flag"].ToString(),
                        enhancement_flag = dt["enhancement_flag"].ToString()
                    });
                }
            }
            values.MdlLSACheckerSummary = getMdlLSAMakerSummary_list;
            dt_datatable.Dispose();
        }

        public void DaGetLSAFollowupCheckerSummary(string employee_gid, MdlLSACheckerSummaryList values)
        {
            msSQL = " select a.application_gid,e.application2sanction_gid,a.application_no,a.customer_urn,a.customer_name as customer_name, " +
                     " date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as ccapproved_date,e.sanction_refno, " +
                     " date_format(e.sanction_date, '%d-%m-%Y') as sanction_date, b.overall_lsastatus,a.renewal_flag,a.enhancement_flag  from ocs_trn_tcadapplication a " +
                     " left join ocs_trn_tgeneratelsa b on b.application_gid = a.application_gid " +
                     " left join ocs_trn_tprocesstype_assign d on d.application_gid = a.application_gid " +
                     " left join ocs_trn_tapplication2sanction e on a.application_gid= e.application_gid " +
                     " where a.process_type = 'Accept' and d.menu_gid='" + getMenuClass.LSA + "' and e.accepted_status ='Y' and " +
                     " d.checker_gid = '" + employee_gid + "' and d.maker_approvalflag ='Y' and d.checker_approvalflag='Y' " +
                     " and d.approver_approvalflag='N' and b.overall_lsastatus in ('Final Approval Pending') order by e.application2sanction_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getMdlLSAMakerSummary_list = new List<MdlLSACheckerSummary>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getMdlLSAMakerSummary_list.Add(new MdlLSACheckerSummary
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        ccapproved_date = dt["ccapproved_date"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        sanction_refno = dt["sanction_refno"].ToString(),
                        sanction_date = dt["sanction_date"].ToString(),
                        application2sanction_gid = dt["application2sanction_gid"].ToString(),
                        overall_lsastatus = dt["overall_lsastatus"].ToString(),
                        renewal_flag = dt["renewal_flag"].ToString(),
                        enhancement_flag = dt["enhancement_flag"].ToString()
                    });
                }
            }
            values.MdlLSACheckerSummary = getMdlLSAMakerSummary_list;
            dt_datatable.Dispose();
        }

        public void DaGetLSAPendingApproverSummary(string employee_gid, MdlLSAApproverSummaryList values)
        {
            msSQL = " select a.application_gid,e.application2sanction_gid,a.application_no,a.customer_urn,a.customer_name as customer_name, " +
                     " date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as ccapproved_date,e.sanction_refno, " +
                     " date_format(e.sanction_date, '%d-%m-%Y') as sanction_date,b.overall_lsastatus,a.renewal_flag,a.enhancement_flag  from ocs_trn_tcadapplication a " +
                     " left join ocs_trn_tgeneratelsa b on b.application_gid = a.application_gid " +
                     " left join ocs_trn_tprocesstype_assign d on d.application_gid = a.application_gid " +
                     " left join ocs_trn_tapplication2sanction e on a.application_gid= e.application_gid " +
                     " where a.process_type = 'Accept' and d.menu_gid='" + getMenuClass.LSA + "' and " +
                     " d.approver_gid = '" + employee_gid + "' and d.maker_approvalflag ='Y' and d.checker_approvalflag='Y' and e.accepted_status ='Y' " +
                     " and d.approver_approvalflag='N' and b.overall_lsastatus not in ('Approved') order by e.application2sanction_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getMdlLSAMakerSummary_list = new List<MdlLSAApproverSummary>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getMdlLSAMakerSummary_list.Add(new MdlLSAApproverSummary
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        ccapproved_date = dt["ccapproved_date"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        sanction_refno = dt["sanction_refno"].ToString(),
                        sanction_date = dt["sanction_date"].ToString(),
                        application2sanction_gid = dt["application2sanction_gid"].ToString(),
                        overall_lsastatus = dt["overall_lsastatus"].ToString(),
                        renewal_flag = dt["renewal_flag"].ToString(),
                        enhancement_flag = dt["enhancement_flag"].ToString()
                    });
                }
            }
            values.MdlLSAApproverSummary = getMdlLSAMakerSummary_list;
            dt_datatable.Dispose();
        }

        public void DaGetLSACompletedSummary(string employee_gid, MdlLSAApproverSummaryList values)
        {
            msSQL =  " select a.application_gid,e.application2sanction_gid,b.generatelsa_gid,a.application_no," +
                     " a.customer_urn,a.customer_name as customer_name, " +
                     " date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as ccapproved_date,e.sanction_refno,b.lsa_refno, " +
                     " date_format(e.sanction_date, '%d-%m-%Y') as sanction_date,b.overall_lsastatus,a.renewal_flag,a.enhancement_flag," +
                     " b.reinitiate_eligibleflag from ocs_trn_tcadapplication a " +
                     " left join ocs_trn_tgeneratelsa b on b.application_gid = a.application_gid " +
                     " left join ocs_trn_tprocesstype_assign d on d.application_gid = a.application_gid " +
                     " left join ocs_trn_tapplication2sanction e on a.application_gid= e.application_gid " +
                     " where d.menu_gid='" + getMenuClass.LSA + "' and d.maker_approvalflag ='Y' and d.checker_approvalflag='Y' " +
                     "and b.overall_lsastatus='Approved' and e.accepted_status ='Y' " +
                     " and d.approver_approvalflag='Y' and e.lsatotdoclimit_flag='N' group by b.generatelsa_gid " +
                     " order by e.application2sanction_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getMdlLSAMakerSummary_list = new List<MdlLSAApproverSummary>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getMdlLSAMakerSummary_list.Add(new MdlLSAApproverSummary
                    {
                        application_no = dt["application_no"].ToString(),
                        generatelsa_gid = dt["generatelsa_gid"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        ccapproved_date = dt["ccapproved_date"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        sanction_refno = dt["sanction_refno"].ToString(),
                        sanction_date = dt["sanction_date"].ToString(),
                        application2sanction_gid = dt["application2sanction_gid"].ToString(),
                        overall_lsastatus = dt["overall_lsastatus"].ToString(),
                        renewal_flag = dt["renewal_flag"].ToString(),
                        enhancement_flag = dt["enhancement_flag"].ToString(),
                        reinitiate_eligibleflag = dt["reinitiate_eligibleflag"].ToString(),
                        lsa_refno = dt["lsa_refno"].ToString()
                    });
                }
            }
            values.MdlLSAApproverSummary = getMdlLSAMakerSummary_list;
            dt_datatable.Dispose();
        }

        //LSA Reinitiate Eligible
        public void DaGetLSAReinitiateEligible(string employee_gid, MdlLSAApproverSummaryList values)
        {
            msSQL =  " select a.application_gid,e.application2sanction_gid,b.generatelsa_gid,a.application_no," +
                     " a.customer_urn,a.customer_name as customer_name, " +
                     " date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as ccapproved_date,e.sanction_refno, " +
                     " date_format(e.sanction_date, '%d-%m-%Y') as sanction_date,b.overall_lsastatus,a.renewal_flag,a.enhancement_flag,b.reinitiate_eligibleflag from ocs_trn_tcadapplication a " +
                     " left join ocs_trn_tgeneratelsa b on b.application_gid = a.application_gid " +
                     " left join ocs_trn_tprocesstype_assign d on d.application_gid = a.application_gid " +
                     " left join ocs_trn_tapplication2sanction e on a.application_gid= e.application_gid " +
                     " where d.menu_gid='" + getMenuClass.LSA + "' and d.maker_approvalflag ='Y' and " +
                     " d.checker_approvalflag='Y' and b.overall_lsastatus='Approved' " +
                     " and d.approver_approvalflag='Y' and e.lsatotdoclimit_flag='Y' group by b.generatelsa_gid " +
                     "order by e.application2sanction_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getMdlLSAMakerSummary_list = new List<MdlLSAApproverSummary>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getMdlLSAMakerSummary_list.Add(new MdlLSAApproverSummary
                    {
                        application_no = dt["application_no"].ToString(),
                        generatelsa_gid = dt["generatelsa_gid"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        ccapproved_date = dt["ccapproved_date"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        sanction_refno = dt["sanction_refno"].ToString(),
                        sanction_date = dt["sanction_date"].ToString(),
                        application2sanction_gid = dt["application2sanction_gid"].ToString(),
                        overall_lsastatus = dt["overall_lsastatus"].ToString(),
                        renewal_flag = dt["renewal_flag"].ToString(),
                        enhancement_flag = dt["enhancement_flag"].ToString(),
                        reinitiate_eligibleflag = dt["reinitiate_eligibleflag"].ToString(),
                    });
                }
            }
            values.MdlLSAApproverSummary = getMdlLSAMakerSummary_list;
            dt_datatable.Dispose();
        }
        public void DaCADLSASummaryCount(string employee_gid, CadLSACount values)
        {

            msSQL = " select(select count(*) from ocs_trn_tprocesstype_assign a  " +
                   " left join ocs_trn_tcadapplication b on a.application_gid = b.application_gid " +
                   " left join ocs_trn_tapplication2sanction e on a.application_gid= e.application_gid " +
                   " where maker_approvalflag = 'N' and b.sanction_approvalflag = 'Y' and e.accepted_status ='Y' " +
                   " and maker_gid = '" + employee_gid + "' and menu_gid = '" + getMenuClass.LSA + "') as MakerPendingCount, " +
              "  (select count(distinct b.generatelsa_gid) from ocs_trn_tcadapplication a " +
                     " left join ocs_trn_tgeneratelsa b on b.application_gid = a.application_gid " +
                     " left join ocs_trn_tprocesstype_assign d on d.application_gid = a.application_gid " +
                     " left join ocs_trn_tapplication2sanction e on a.application_gid= e.application_gid " +
                     " where a.process_type = 'Accept' and d.menu_gid='" + getMenuClass.LSA + "' and a.sanction_approvalflag='Y' and   e.accepted_status ='Y' and d.approver_approvalflag='N' and" +
                     " d.maker_gid = '" + employee_gid + "' and d.maker_approvalflag ='Y' and b.overall_lsastatus in ('Checker Approval Pending','Final Approval Pending') order by e.application2sanction_gid desc) as MakerFollowUpCount, " +
                   "  (select count(*) from ocs_trn_tprocesstype_assign a " +
                   "  left join ocs_trn_tcadapplication b on a.application_gid = b.application_gid " +
                     " left join ocs_trn_tapplication2sanction e on a.application_gid= e.application_gid " +
                   "  where maker_approvalflag = 'Y' and checker_approvalflag = 'N' and e.accepted_status ='Y'" +
                   "  and checker_gid = '" + employee_gid + "' and menu_gid = '" + getMenuClass.LSA + "') as CheckerPendingCount,   " +
                   "  (select count(*) from ocs_trn_tprocesstype_assign a" +
                     " left join ocs_trn_tapplication2sanction e on a.application_gid= e.application_gid " +
                     " left join ocs_trn_tcadapplication g on a.application_gid= g.application_gid " +
                     " left join ocs_trn_tgeneratelsa f on a.application_gid= f.application_gid " +
                   "  where f.overall_lsastatus in ('Final Approval Pending') and g.process_type = 'Accept' and checker_approvalflag = 'Y' and maker_approvalflag = 'Y'  and  approver_approvalflag = 'N' and e.accepted_status ='Y'" +
                   "  and checker_gid = '" + employee_gid + "' and menu_gid = '" + getMenuClass.LSA + "') as CheckerFollowUpCount,  " +
                   "  (select count(distinct b.generatelsa_gid) from ocs_trn_tcadapplication a " +
                     " left join ocs_trn_tgeneratelsa b on b.application_gid = a.application_gid " +
                     " left join ocs_trn_tprocesstype_assign d on d.application_gid = a.application_gid " +
                     " left join ocs_trn_tapplication2sanction e on a.application_gid= e.application_gid " +
                     " where a.process_type = 'Accept' and d.menu_gid='" + getMenuClass.LSA + "' and " +
                     " d.approver_gid = '" + employee_gid + "' and d.maker_approvalflag ='Y' and d.checker_approvalflag='Y' and e.accepted_status ='Y' " +
                     " and d.approver_approvalflag='N' and b.overall_lsastatus not in ('Approved') order by e.application2sanction_gid desc)  as ApproverPendingCount,  " +
                   "  (select count(*) from ocs_trn_tprocesstype_assign a" +
                     " left join ocs_trn_tapplication2sanction e on a.application_gid= e.application_gid " +
                   "  where approver_approvalflag = 'Y' and checker_approvalflag = 'Y'  and menu_gid = '" + getMenuClass.LSA + "' and e.accepted_status ='Y'" +
                   "  and(maker_gid = '" + employee_gid + "' or checker_gid = '" + employee_gid + "' or approver_gid = '" + employee_gid + "')) as CompletedCount; ";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {

                values.MakerPendingCount = objODBCDataReader["MakerPendingCount"].ToString();
                values.MakerFollowUpCount = objODBCDataReader["MakerFollowUpCount"].ToString();
                values.CheckerPendingCount = objODBCDataReader["CheckerPendingCount"].ToString();
                values.CheckerFollowUpCount = objODBCDataReader["CheckerFollowUpCount"].ToString();
                values.ApproverPendingCount = objODBCDataReader["ApproverPendingCount"].ToString();
                values.CompletedCount = objODBCDataReader["CompletedCount"].ToString();

            }
            objODBCDataReader.Close();

        }


        public void DaGetGenerateLSAMakerSummary(string application_gid,MdlGenerateLSAMakerSummaryList values)
        {
                msSQL = " select a.application_gid,a.generatelsa_gid,a.limitproduct_filled, e.sanction_refno, date_format(e.sanction_date, '%d-%m-%Y') as sanction_date, " +
                     " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as created_by,a.overall_lsastatus, " +
                     " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date from ocs_trn_tgeneratelsa a " +
                     " left join hrm_mst_temployee b on b.employee_gid = a.created_by " +
                     " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                     " left join ocs_trn_tcadapplication d on d.application_gid = a.application_gid " +
                     " left join ocs_trn_tapplication2sanction e on a.application_gid= e.application_gid " +
                     " where a.application_gid = '" + application_gid + "' order by a.generatelsa_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getMdlLSAMakerSummary_list = new List<MdlGenerateLSAMakerSummary>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getMdlLSAMakerSummary_list.Add(new MdlGenerateLSAMakerSummary
                        {
                            generatelsa_gid = dt["generatelsa_gid"].ToString(),
                            sanction_refno = dt["sanction_refno"].ToString(),
                            sanction_date = dt["sanction_date"].ToString(),
                            created_by = dt["created_by"].ToString(),
                            created_date = dt["created_date"].ToString(),
                            limitproduct_filled = dt["limitproduct_filled"].ToString(),
                            overall_lsastatus = dt["overall_lsastatus"].ToString(),
                        });
                    }
                }
                values.MdlGenerateLSAMakerSummary = getMdlLSAMakerSummary_list;
                dt_datatable.Dispose();                      
        }

        public void DaGetlsaGeneratevalidation(string application_gid, string generatelsa_gid, result values)
        {
            string lsproductcount = "", lsloancount = "";
            msSQL = " select (select count(*) from ocs_trn_tlimitproductinfo  " +
                    " where generatelsa_gid = '" + generatelsa_gid + "') as productcount, " +
                    " (select count(*)  from ocs_mst_tapplication2loan where application_gid = '" + application_gid + "') as loancount";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                lsproductcount = objODBCDataReader["productcount"].ToString();
                lsloancount = objODBCDataReader["loancount"].ToString();
            }
            objODBCDataReader.Close();

            if (lsproductcount == lsloancount)
                values.status = true;
            else
                values.status = false;
        }

        // -------------------Limit & Products -------------------------------------------//

        public bool DaPostLimitInfo(string employee_gid, limitandproducts values)
        {
            int odlim_amount = Convert.ToInt32(values.odlim_amount.Replace(",", "").Replace(".00", ""));
            //int limit_validation = Convert.ToInt32(values.limit_validation.Replace(",", "").Replace(".00", ""));
            // int document_limit_validation = Convert.ToInt32(values.document_limit.Replace(",", "").Replace(".00", ""));
            int limit_released_amount = Convert.ToInt32(values.limit_released.Replace(",", "").Replace(".00", ""));
            int exiting_limit_amount = Convert.ToInt32(values.existing_limit.Replace(",", "").Replace(".00", ""));

            //if ((odlim_validation - limit_validation) < document_limit_validation)
            //{
            //    values.message = "Document Limit Exceeded From ODLIM";
            //    values.status = false;
            //    return false;
            //}
            //else
            //{
            //if ((limit_released_validation) > (document_limit_validation - exiting_limit_validation))

            //{
            //    values.message = "Limit To be Released Exceeded From Document Limit";

            //    values.status = false;
            //    return false;
            //}
            //else
            //{ 

            msSQL = "select application2sanction_gid from ocs_trn_tapplication2sanction where application_gid='" + values.application_gid + "'";
            values.application2sanction_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select generatelsa_gid from ocs_trn_tgeneratelsa where application2sanction_gid='" + values.application2sanction_gid + "' " +
                    " and application_gid='" + values.application_gid + "'";
            string generatelsa_gid = objdbconn.GetExecuteScalar(msSQL);
            if (generatelsa_gid == "")
            {
                generatelsa_gid = objcmnfunctions.GetMasterGID("LSAG");
                msSQL = " Insert into ocs_trn_tgeneratelsa (" +
                          " generatelsa_gid," +
                          " application2sanction_gid," +
                          " application_gid," +
                          " created_by, " +
                          " created_date)" +
                          " values(" +
                          "'" + generatelsa_gid + "'," +
                          "'" + values.application2sanction_gid + "'," +
                          "'" + values.application_gid + "'," +
                          "'" + employee_gid + "'," +
                          "current_timestamp)";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);



            }
            values.generatelsa_gid = generatelsa_gid;
            msSQL = " select producttype_gid,product_type,productsubtype_gid,productsub_type,loanfacility_amount " +
                    " from ocs_trn_tcadapplication2loan where application2loan_gid = '" + values.application2loan_gid + "'";
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
                        " generatelsa_gid," +
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
                        " existing_limit," +
                        " limit_released," +
                        " limitinfo_remarks," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetLimitGid + "'," +
                        "'" + generatelsa_gid + "'," +
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
                msSQL += "'" + values.report_structure.Replace("'", "") + "',";
            msSQL += "'" + values.odlim_amount.Replace(",", "") + "'," +
                       "'" + values.odlim_condition + "'," +
                       "'" + values.existing_limit.Replace(",", "") + "'," +
                       "'" + values.limit_released.Replace(",", "") + "',";
            if (values.limitinfo_remarks == null || values.limitinfo_remarks == "")
                msSQL += "'',";
            else
                msSQL += "'" + values.limitinfo_remarks.Replace("'", "") + "',";
            msSQL += "'" + employee_gid + "'," +
                     "current_timestamp)";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "update ocs_trn_tcadapplication2loan set limit_product='Y' where application2loan_gid ='" + values.application2loan_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.message = "Product Details are Added Successfully";
                values.status = true;
                return true;
            }
            else
            {
                values.message = "Error Occured while adding Product Details";
                values.status = false;
                return false;
            }
            // }
            //}

        }

        public void DaGetLSAApplicationLimitInfo(limitandproductslist values, string application_gid, string application2sanction_gid)
        {
            try
            {
                //msSQL = " select a.limitproductinfodtl_gid,a.interchangeability,a.report_structure_gid,a.product_type,a.productsub_type, " +
                //       " a.report_structure,format(a.odlim_amount,2,'en_IN') as odlim_amount,a.odlim_condition,format(a.existing_limit,2,'en_IN') as existing_limit , " +
                //       " format(a.limit_released,2,'en_IN') as limit_released,format(a.documented_limit,2,'en_IN') as documented_limit, " +
                //       " date_format(a.dateof_Expiry,'%d-%m-%Y') as dateof_Expiry, " +
                //       " a.limitinfo_remarks,a.created_by,a.created_date,format(b.lsatotalreleased_amount,2,'en_IN') as lsatotalreleased_amount from ocs_trn_tlimitproductinfo a " +
                //       " left join ocs_trn_tapplication2sanction b on a.application2sanction_gid=b.application2sanction_gid " +
                //       " where a.application_gid='" + application_gid + "' and a.application2sanction_gid='" + application2sanction_gid + "'";

                msSQL = " select limitproductinfodtl_gid,interchangeability,report_structure_gid,product_type,productsub_type, " +
                        " report_structure,format(odlim_amount,2,'en_IN') as odlim_amount,odlim_condition,format(existing_limit,2,'en_IN') as existing_limit , " +
                        " format(limit_released,2,'en_IN') as limit_released,format(documented_limit,2,'en_IN') as documented_limit, " +
                        " date_format(dateof_Expiry,'%d-%m-%Y') as dateof_Expiry,format(lsatotalreleased_approved,2,'en_IN') as lsatotalreleased_approved," +
                        " limitinfo_remarks,created_by,created_date from ocs_trn_tlimitproductinfo " +
                        " where application_gid='" + application_gid + "' and application2sanction_gid='" + application2sanction_gid + "'";

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
                            lsatotalreleased_approved = (dr_datarow["lsatotalreleased_approved"].ToString()),
                        });
                    }
                    values.limitandproducts = getlimit_info;
                    dt_datatable.Dispose();
                }

                msSQL = " select format(sum(limit_released),2,'en_IN') from ocs_trn_tlimitproductinfo  a " +
                        " where application2sanction_gid='" + application2sanction_gid + "'";
                values.total_limitreleased = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select format(sum(existing_limit),2,'en_IN') from ocs_trn_tlimitproductinfo  a " +
                       " where application2sanction_gid='" + application2sanction_gid + "'";
                values.total_existinglimit = objdbconn.GetExecuteScalar(msSQL);
                values.status = true;
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = ex.ToString();
            }
        }

        public void DaGetLimitInfoDtl(limitandproductslist values, string generatelsa_gid)
        {
            try
            {
                msSQL = " select limitproductinfodtl_gid,generatelsa_gid,interchangeability,report_structure_gid,product_type,productsub_type, " +
                       " report_structure,format(odlim_amount,2,'en_IN') as odlim_amount,odlim_condition,format(existing_limit,2,'en_IN') as existing_limit , " +
                       " format(limit_released,2,'en_IN') as limit_released,format(documented_limit,2,'en_IN') as documented_limit, date_format(dateof_Expiry,'%d-%m-%Y') as dateof_Expiry, " +
                       " limitinfo_remarks,created_by,created_date,format(lsatotalreleased_approved,2,'en_IN') as lsatotalreleased_approved from ocs_trn_tlimitproductinfo " +
                       " where generatelsa_gid='" + generatelsa_gid + "'";
                
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
                            generatelsa_gid = (dr_datarow["generatelsa_gid"].ToString()),
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
                            lsatotalreleased_approved = (dr_datarow["lsatotalreleased_approved"].ToString()),
                        });
                    }
                    values.limitandproducts = getlimit_info;
                    dt_datatable.Dispose();
                }

                msSQL = " select format(sum(limit_released),2,'en_IN') from ocs_trn_tlimitproductinfo  a " +
                        " where generatelsa_gid='" + generatelsa_gid + "'";
                values.total_limitreleased = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select format(sum(existing_limit),2,'en_IN') from ocs_trn_tlimitproductinfo  a " +
                       " where generatelsa_gid='" + generatelsa_gid + "'";
                values.total_existinglimit = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select maker_name from ocs_trn_tprocesstype_assign a " +
                        " left join ocs_trn_tgeneratelsa b on a.application_gid = b.application_gid " +
                        " where a.menu_gid='" + getMenuClass.LSA + "' and b.generatelsa_gid='" + generatelsa_gid + "'";
                values.maker_name = objdbconn.GetExecuteScalar(msSQL);
                values.status = true;
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = ex.ToString();
            }
        }

        public void DaGetLimitProductInfoDtl(limitandproducts values, string limitproductinfodtl_gid)
        {
            try
            {
                msSQL = " select limitproductinfodtl_gid,application2sanction_gid, generatelsa_gid,interchangeability,report_structure_gid,product_type,productsub_type, " +
                       " report_structure,format(odlim_amount,2,'en_IN') as odlim_amount,odlim_condition,format(existing_limit,2,'en_IN') as existing_limit , " +
                       " format(limit_released,2,'en_IN') as limit_released,format(documented_limit,2,'en_IN') as documented_limit, " +
                       " date_format(dateof_Expiry,'%d-%m-%Y') as dateof_Expiry, dateof_Expiry as dateofExpiry, " +
                       " limitinfo_remarks,created_by,created_date from ocs_trn_tlimitproductinfo " +
                       " where limitproductinfodtl_gid='" + limitproductinfodtl_gid + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows == true)
                {
                    values.limitproductinfodtl_gid = objODBCDataReader["limitproductinfodtl_gid"].ToString();
                    values.existing_limit = (objODBCDataReader["existing_limit"].ToString());
                    values.generatelsa_gid = (objODBCDataReader["generatelsa_gid"].ToString());
                    values.limit_released = (objODBCDataReader["limit_released"].ToString());
                    values.interchangeability = (objODBCDataReader["interchangeability"].ToString());
                    values.report_structuregid = (objODBCDataReader["report_structure_gid"].ToString());
                    values.report_structure = (objODBCDataReader["report_structure"].ToString());
                    values.odlim_amount = (objODBCDataReader["odlim_amount"].ToString());
                    values.odlim_condition = (objODBCDataReader["odlim_condition"].ToString());
                    values.limitinfo_remarks = (objODBCDataReader["limitinfo_remarks"].ToString());
                    values.product_type = (objODBCDataReader["product_type"].ToString());
                    values.productsub_type = (objODBCDataReader["productsub_type"].ToString());
                    values.documented_limit = (objODBCDataReader["documented_limit"].ToString());
                    values.dateof_Expiry = (objODBCDataReader["dateof_Expiry"].ToString());
                    values.application2sanction_gid = (objODBCDataReader["application2sanction_gid"].ToString());
                    if (objODBCDataReader["dateofExpiry"].ToString() != "")
                    {
                        values.dateofExpiry = Convert.ToDateTime(objODBCDataReader["dateofExpiry"].ToString());
                    }
                }
                objODBCDataReader.Close();

                //msSQL = " select count(*) as lsaupdatedcount from ocs_trn_tlimitproductinfo where application2sanction_gid = '" + values.application2sanction_gid + "'" +
                //        " and limitproduct_flag ='Y'";
                //string lscount = objdbconn.GetExecuteScalar(msSQL);

                //string lsexisting_limit;
                //if (values.existing_limit != "0.00" && lscount != "0")
                //{
                //    if (values.interchangeability == "No")
                //    {
                //        if (values.odlim_condition == "Not Applicable")
                //        {

                //            msSQL = "select (existing_limit+limit_released)  from ocs_trn_tlimitproductinfo  a " +
                //                   " where generatelsa_gid ='" + values.generatelsa_gid + "'";
                //            lsexisting_limit = objdbconn.GetExecuteScalar(msSQL);
                //        }
                //        else
                //        {
                //            msSQL = " select (sum(existing_limit)+sum(limit_released)) from ocs_trn_tlimitproductinfo  a " +
                //                    " where generatelsa_gid ='" + values.generatelsa_gid + "'";
                //            lsexisting_limit = objdbconn.GetExecuteScalar(msSQL);
                //        }
                //    }
                //    else
                //    {
                //        msSQL = " select (sum(existing_limit)+sum(limit_released)) from ocs_trn_tlimitproductinfo  a " +
                //                " where generatelsa_gid ='" + values.generatelsa_gid + "'";
                //        lsexisting_limit = objdbconn.GetExecuteScalar(msSQL);
                //    }
                //    if ((lsexisting_limit == "") || (lsexisting_limit == null))
                //    {
                //        values.existing_limit = "0,0";
                //    }
                //    else
                //    { 
                //        decimal parsed4 = decimal.Parse(lsexisting_limit, System.Globalization.CultureInfo.InvariantCulture);
                //        System.Globalization.CultureInfo indian_format4 = new System.Globalization.CultureInfo("hi-IN");
                //        string text4 = string.Format(indian_format4, "{0:c}", parsed4);

                //        msSQL = "select substring('" + text4 + "',2,20)";
                //        string existing_limit = objdbconn.GetExecuteScalar(msSQL);

                //        values.existing_limit = existing_limit;
                //    }
                //}
                //else
                //{
                //    values.existing_limit = "0,0";
                //}

                values.status = true;
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = ex.ToString();
            }
        }

        public bool DaUpdatelimitproduct(limitandproducts values, string employee_gid)
        {

            msSQL = "select application2sanction_gid from ocs_trn_tapplication2sanction where application_gid='" + values.application_gid + "'";
            values.application2sanction_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select generatelsa_gid from ocs_trn_tgeneratelsa where application2sanction_gid='" + values.application2sanction_gid + "' " +
                    " and application_gid='" + values.application_gid + "' order by created_date desc limit 1 ";
            string generatelsa_gid = objdbconn.GetExecuteScalar(msSQL);
            if (generatelsa_gid == "")
            {
                generatelsa_gid = objcmnfunctions.GetMasterGID("LSAG");
                msSQL = " Insert into ocs_trn_tgeneratelsa (" +
                          " generatelsa_gid," +
                          " application2sanction_gid," +
                          " application_gid," +
                          " created_by, " +
                          " created_date)" +
                          " values(" +
                          "'" + generatelsa_gid + "'," +
                          "'" + values.application2sanction_gid + "'," +
                          "'" + values.application_gid + "'," +
                          "'" + employee_gid + "'," +
                          "current_timestamp)";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update ocs_trn_tlimitproductinfo set generatelsa_gid='" + generatelsa_gid + "'" +
                        " where application2sanction_gid ='" + values.application2sanction_gid + "' and application_gid='" + values.application_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update ocs_trn_tlsafeescharge set generatelsa_gid='" + generatelsa_gid + "'" +
                        " where generatelsa_gid ='" + values.application_gid + "' and application_gid='" + values.application_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    // Service Charges Details
                    msSQL = " select application_gid, application2servicecharge_gid, processing_fee, processing_collectiontype, doc_charges," +
                      " doccharge_collectiontype,fieldvisit_charges,fieldvisit_charges_collectiontype,adhoc_fee,adhoc_collectiontype," +
                      " life_insurance,lifeinsurance_collectiontype,acct_insurance,total_collect,total_deduct,product_type," +
                      " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                      " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, acctinsurance_collectiontype " +
                      " from ocs_trn_tcadapplicationservicecharge a " +
                      " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                      " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                        " where a.application_gid = '" + values.application_gid + "' order by application2servicecharge_gid desc";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getproductcharges_list = new List<lsaFeecharges_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dr_datarow in dt_datatable.Rows)
                        {
                            getproductcharges_list.Add(new lsaFeecharges_list
                            {
                                application2servicecharge_gid = (dr_datarow["application2servicecharge_gid"].ToString()),
                                processing_fee = (dr_datarow["processing_fee"].ToString()),
                                processing_collectiontype = (dr_datarow["processing_collectiontype"].ToString()),
                                doc_charges = (dr_datarow["doc_charges"].ToString()),
                                doccharge_collectiontype = (dr_datarow["doccharge_collectiontype"].ToString()),
                                fieldvisit_charge = (dr_datarow["fieldvisit_charges"].ToString()),
                                fieldvisit_collectiontype = (dr_datarow["fieldvisit_charges_collectiontype"].ToString()),
                                adhoc_fee = (dr_datarow["adhoc_fee"].ToString()),
                                adhoc_collectiontype = (dr_datarow["adhoc_collectiontype"].ToString()),
                                life_insurance = (dr_datarow["life_insurance"].ToString()),
                                lifeinsurance_collectiontype = (dr_datarow["lifeinsurance_collectiontype"].ToString()),
                                acct_insurance = (dr_datarow["acct_insurance"].ToString()),
                                total_collect = (dr_datarow["total_collect"].ToString()),
                                total_deduct = (dr_datarow["total_deduct"].ToString()),
                                product_type = (dr_datarow["product_type"].ToString()),
                                created_by = (dr_datarow["created_by"].ToString()),
                                created_date = (dr_datarow["created_date"].ToString()),
                                acctinsurance_collectiontype = (dr_datarow["acctinsurance_collectiontype"].ToString()),
                            });
                        }
                        dt_datatable.Dispose();
                        foreach (var i in getproductcharges_list)
                        {
                            msSQL = " select application2servicecharge_gid from ocs_trn_tlsafeescharge where application2servicecharge_gid = '" + i.application2servicecharge_gid + "' ";
                            objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader1.HasRows == false)
                            {
                                string msgetfeechargeGid = objcmnfunctions.GetMasterGID("LFCG");

                            msSQL = " insert into ocs_trn_tlsafeescharge(" +
                                    " lsafeescharge_gid, " +
                                    " application2servicecharge_gid," +
                                    " application_gid," +
                                    " generatelsa_gid, " +
                                    " processing_fee," +
                                    " processing_collectiontype," +
                                    " doc_charges," +
                                    " doccharge_collectiontype," +
                                    " fieldvisit_charges," +
                                    " fieldvisit_charges_collectiontype," +
                                    " adhoc_fee," +
                                    " adhoc_collectiontype," +
                                    " life_insurance," +
                                    " lifeinsurance_collectiontype," +
                                    " acct_insurance," +
                                    " acctinsurance_collectiontype," +
                                    " total_collect," +
                                    " total_deduct," +
                                    " product_type," +
                                    " created_by," +
                                    " created_date) values(" +
                                    "'" + msgetfeechargeGid + "'," +
                                    "'" + i.application2servicecharge_gid + "'," +
                                    "'" + values.application_gid + "'," +
                                    "'" + generatelsa_gid + "'," +
                                    "'" + i.processing_fee + "'," +
                                    "'" + i.processing_collectiontype + "'," +
                                    "'" + i.doc_charges + "'," +
                                    "'" + i.doccharge_collectiontype + "'," +
                                    "'" + i.fieldvisit_charge + "'," +
                                    "'" + i.fieldvisit_collectiontype + "'," +
                                    "'" + i.adhoc_fee + "'," +
                                    "'" + i.adhoc_collectiontype + "'," +
                                    "'" + i.life_insurance + "'," +
                                    "'" + i.lifeinsurance_collectiontype + "'," +
                                    "'" + i.acct_insurance + "'," +
                                    "'" + i.acctinsurance_collectiontype + "'," +
                                    "'" + i.total_collect + "'," +
                                    "'" + i.total_deduct + "'," +
                                    "'" + i.product_type + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            }
                        }
                    }
                    dt_datatable.Dispose();
                }
            }
            values.generatelsa_gid = generatelsa_gid;

            msSQL = "select documented_limit,existing_limit,lsatotalreleased_approved from ocs_trn_tlimitproductinfo where limitproductinfodtl_gid ='" + values.limitproductinfodtl_gid + "' and generatelsa_gid='" + values.generatelsa_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                dbldocumented_limit = Convert.ToDouble(objODBCDataReader["documented_limit"].ToString());
                dblexisting_limit = Convert.ToDouble(objODBCDataReader["existing_limit"].ToString());
                //dbllimit_released = Convert.ToDouble(objODBCDataReader["limit_released"].ToString());
                dbllsatotalreleased_approved = Convert.ToDouble(objODBCDataReader["lsatotalreleased_approved"].ToString());
            }
            objODBCDataReader.Close();

            dbllimit_released = Convert.ToDouble(values.limit_released.Replace(",", ""));

            dbllsatotalreleased_amount = dbllimit_released;

            //dblexisting_limit = dbldocumented_limit - dbllsatotalreleased_amount;

            //dbllsatotalreleased_amount = dbllsatotalreleased_amount + dbllimit_released;

            dblexisting_limit = (dbldocumented_limit - dbllsatotalreleased_approved) -(dbllimit_released);


            msSQL = " update ocs_trn_tlimitproductinfo set " +
                    //" interchangeability ='" + values.interchangeability + "'," +
                    //" report_structure_gid ='" + values.report_structuregid + "', " +
                    //" report_structure ='" + values.report_structure + "'," +
                    " limitproduct_flag ='Y'," +
                    //" existing_limit ='" + values.existing_limit.Replace(",", "") + "'," +
                    " existing_limit ='" + dblexisting_limit + "'," +
                    " limit_released ='" + values.limit_released.Replace(",", "") + "'," +
                    " limitinfo_remarks='" + values.limitinfo_remarks.Replace("'", "") + "'," +
                    " lsatotalreleased_amount ='" + dbllsatotalreleased_amount + "'," +
                    //" lsprodreinitiate_flag ='" + lsprodreinitiate_flag + "'," +
                    " updated_by='" + employee_gid + "'," +
                    " updated_date=current_timestamp " +
                    " where limitproductinfodtl_gid ='" + values.limitproductinfodtl_gid + "' and generatelsa_gid='" + values.generatelsa_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                
                msSQL = " select (select count(*) from ocs_trn_tlimitproductinfo where application2sanction_gid='" + values.application2sanction_gid + "') as totalloanproduct, " +
                        " (select count(*) from ocs_trn_tlimitproductinfo where application2sanction_gid = '" + values.application2sanction_gid + "' and limitproduct_flag ='Y' ) " +
                        " as lsaupdatedproduct ";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows == true)
                {
                    int totalloanproduct = Convert.ToInt16(objODBCDataReader["totalloanproduct"].ToString());
                    int lsaupdatedproduct = Convert.ToInt16(objODBCDataReader["lsaupdatedproduct"].ToString());
                    if (totalloanproduct == lsaupdatedproduct)
                    {
                        msSQL = "update ocs_trn_tgeneratelsa set limitproduct_filled='Y' where generatelsa_gid='" + values.generatelsa_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        values.lsfilledlimitproduct = "Y";
                    }
                    else
                        values.lsfilledlimitproduct = "N";
                }
                objODBCDataReader.Close();
                values.status = true;
                values.generatelsa_gid = generatelsa_gid;
                values.message = "Product Details are updated Successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
            return true;
        }

        // -------------------Bank Account Details ---------------------------------------//

        public bool DaGetApplicationNameDetails(string application_gid, bankapplicationNameinfolist values)
        {
            msSQL = " select company_name as holder_name,stakeholder_type,institution_gid as credit_gid from ocs_trn_tcadinstitution where application_gid='" + application_gid + "' " +
                    " union " +
                    " select group_name as holder_name,'Group',group_gid from ocs_trn_tcadgroup  where application_gid = '" + application_gid + "' " +
                    " union " +
                    " select concat(first_name, ' ', middle_name, ' ', last_name) as holder_name, stakeholder_type, contact_gid " +
                    " from ocs_trn_tcadcontact where application_gid = '" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getlimit_info = new List<bankapplicationNameinfo>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getlimit_info.Add(new bankapplicationNameinfo
                    {
                        holder_name = dr_datarow["holder_name"].ToString(),
                        stakeholder_type = (dr_datarow["stakeholder_type"].ToString()),
                        credit_gid = (dr_datarow["credit_gid"].ToString()),
                    });
                }
                values.bankapplicationNameinfo = getlimit_info;
                dt_datatable.Dispose();
            }
            return true;
        }

        public bool DaPostBankAccountDetails(string employee_gid, MdlBankAccount values)
        {
            msSQL = " select application2sanction_gid from ocs_trn_tgeneratelsa where generatelsa_gid ='" + values.generatelsa_gid + "'";
            values.application2sanction_gid = objdbconn.GetExecuteScalar(msSQL);

            if (values.bank_name == null)
                values.bank_name = "";
            if (values.branch_name == null)
                values.branch_name = "";
            if (values.bank_address == null)
                values.bank_address = "";
            if (values.micr_code == null)
                values.micr_code = "";
            msGetGid = objcmnfunctions.GetMasterGID("LBAD");
            msSQL = " insert into ocs_trn_tlsabankaccountdtl(" +
                    " lsabankaccdtl_gid," +
                    " generatelsa_gid, " +
                    " application2sanction_gid," +
                    " application_gid," +
                    " name, " +
                    " credit_gid, " +
                    " stakeholder_type, " +
                    " bank_name," +
                    " branch_name," +
                    " branch_address," +
                    " micr_code," +
                    " ifsc_code," +
                    " accountholder_name," +
                    " accounttype_gid," +
                    " accounttype_name," +
                    " bankaccount_number," +
                    " confirmbankaccount_number," +
                    " joint_account," +
                    " jointaccountholder_name," +
                    " chequebookfacility_available," +
                    " accountopen_date," +
                    " disbursement_accountstatus, " +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.generatelsa_gid + "'," +
                    "'" + values.application2sanction_gid + "'," +
                    "'" + values.application_gid + "'," +
                    "'" + values.name + "'," +
                    "'" + values.credit_gid + "'," +
                    "'" + values.stakeholder_type + "'," +
                    "'" + values.bank_name.Replace("'", "") + "'," +
                    "'" + values.branch_name.Replace("'", "") + "'," +
                    "'" + values.bank_address.Replace("'", "") + "'," +
                    "'" + values.micr_code + "'," +
                    "'" + values.ifsc_code + "'," +
                    "'" + values.accountholder_name.Replace("'", "") + "'," +
                    "'" + values.bankaccounttype_gid + "'," +
                    "'" + values.bankaccounttype_name + "'," +
                    "'" + values.bankaccount_number + "'," +
                    "'" + values.confirmbankaccountnumber + "'," +
                    "'" + values.joint_account + "'," +
                    "'" + values.jointaccountholder_name.Replace("'", "") + "'," +
                    "'" + values.chequebookfacility_available + "',";
            if (values.accountopen_date1 == null || values.accountopen_date1 == "")
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.accountopen_date1).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            //if (values.accountopen_date == null || values.accountopen_date == "")
            //{
            //    msSQL += "null,";
            //}
            //else
            //{
            //    msSQL += "'" + Convert.ToDateTime(values.accountopen_date).ToString("yyyy-MM-dd") + "',";
            //}
            //if (Convert.ToDateTime(values.accountopen_date).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
            //{

            //}
            //else
            //{
            //    msSQL += " accountopen_date='" + Convert.ToDateTime(values.accountopen_date).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "',";
            //}
            msSQL += "'" + values.disbursement_accountstatus + "'," +
                     "'" + employee_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msSQL = " update ocs_trn_tlsachequeleafdocument set lsabankaccdtl_gid='" + msGetGid + "' " +
                        " where lsabankaccdtl_gid='" + employee_gid + "' and generatelsa_gid='" + values.generatelsa_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Bank Account Details Added Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured While Adding Bank Account Details";
                return false;
            }

        }

        public bool DaPostUpdateBankAccountDetails(string employee_gid, MdlBankAccount values)
        {

            if (values.accountopen_date == null || values.accountopen_date == "")
            {
                values.accountopen_date = null;
            }
            else
            {
                values.accountopen_date = Convert.ToDateTime(values.accountopen_date).ToString("yyyy-MM-dd HH:mm:ss");
            }
            if (values.bank_name == null)
                values.bank_name = "";
            if (values.branch_name == null)
                values.branch_name = "";
            if (values.bank_address == null)
                values.bank_address = "";
            if (values.micr_code == null)
                values.micr_code = "";

            if (values.branch_address == null)
                values.branch_address = "";
            if (values.bankaccounttype_name == null)
                values.bankaccounttype_name = "";
            if (values.jointaccountholder_name == null)
                values.jointaccountholder_name = "";


            msSQL = " update ocs_trn_tlsabankaccountdtl set name='" + values.name + "'," +
                    " credit_gid ='" + values.credit_gid + "'," +
                    " stakeholder_type='" + values.stakeholder_type + "'," +
                    " bank_name='" + values.bank_name.Replace("'", "") + "'," +
                    " branch_name='" + values.branch_name.Replace("'", "") + "'," +
                    " branch_address='" + values.branch_address.Replace("'", "") + "'," +
                    " micr_code='" + values.micr_code.Replace("'", "") + "'," +
                    " ifsc_code='" + values.ifsc_code + "'," +
                    " accountholder_name='" + values.accountholder_name.Replace("'", "") + "'," +
                    " accounttype_gid='" + values.bankaccounttype_gid + "'," +
                    " accounttype_name='" + values.bankaccounttype_name + "'," +
                    " bankaccount_number='" + values.bankaccount_number + "'," +
                    " confirmbankaccount_number='" + values.confirmbankaccountnumber + "'," +
                    " joint_account='" + values.joint_account + "'," +
                     " accountopen_date='" + values.accountopen_date + "'," +
                    " jointaccountholder_name='" + values.jointaccountholder_name.Replace("'", "") + "'," +
                    " chequebookfacility_available='" + values.chequebookfacility_available + "',";
            //if (values.accountopen_date == null || values.accountopen_date == "")
            //    msSQL += "accountopen_date = null,";
            //else
            //    msSQL += "accountopen_date = '" + Convert.ToDateTime(values.accountopen_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            msSQL += " disbursement_accountstatus='" + values.disbursement_accountstatus + "'," +
                     " updated_by ='" + employee_gid + "'," +
                     " updated_date ='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                     " where lsabankaccdtl_gid='" + values.lsabankaccdtl_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = " update ocs_trn_tlsachequeleafdocument set lsabankaccdtl_gid='" + values.lsabankaccdtl_gid + "' " +
                        " where lsabankaccdtl_gid='" + employee_gid + "' and generatelsa_gid='" + values.generatelsa_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Bank Account Details are updated Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
                return false;
            }

        }

        public void DaGetDeleteLSABankAccountdtl(string lsabankaccdtl_gid, result values)
        {
            msSQL = "delete from ocs_trn_tlsabankaccountdtl where lsabankaccdtl_gid='" + lsabankaccdtl_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                msSQL = "delete from ocs_trn_tlsachequeleafdocument where lsabankaccdtl_gid='" + lsabankaccdtl_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Bank Account Details deleted Successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
        }

        public void DaGetDeleteLSAchequeleafDocument(string lsachequeleafdocument_gid, result values)
        {
            msSQL = "delete from ocs_trn_tlsachequeleafdocument where lsachequeleafdocument_gid='" + lsachequeleafdocument_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Cheque leaf Document deleted Successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
        }

        public void DaGetTmpClearchequeleafDocument(string employee_gid, result values)
        {
            msSQL = "delete from ocs_trn_tlsachequeleafdocument where lsabankaccdtl_gid='" + employee_gid + "'";
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

        public void DaGetLSAChequeLeafTmpdoc(string employee_gid, lsauploaddocument values)
        {
            msSQL = " select lsachequeleafdocument_gid,lsabankaccdtl_gid,document_name,document_path,document_title from " +
                              " ocs_trn_tlsachequeleafdocument where lsabankaccdtl_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdocumentdtlList = new List<lsauploaddocument_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getdocumentdtlList.Add(new lsauploaddocument_list
                    {
                        document_name = dt["document_name"].ToString(),
                        document_title = dt["document_title"].ToString(),
                        document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                        lsachequeleafdocument_gid = dt["lsachequeleafdocument_gid"].ToString(),
                        lsabankaccdtl_gid = dt["lsabankaccdtl_gid"].ToString(),

                    });
                    values.lsauploaddocument_list = getdocumentdtlList;
                }
            }
            dt_datatable.Dispose();
        }

        public bool DaGetLSAchequeleafDocument(string lsabankaccdtl_gid, lsauploaddocument values)
        {
            msSQL = " select lsachequeleafdocument_gid,lsabankaccdtl_gid,document_name,document_path,document_title from " +
                    " ocs_trn_tlsachequeleafdocument where lsabankaccdtl_gid='" + lsabankaccdtl_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcreditbankacc_list = new List<lsauploaddocument_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getcreditbankacc_list.Add(new lsauploaddocument_list
                    {
                        document_name = dt["document_name"].ToString(),
                        document_title = dt["document_title"].ToString(),
                        document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                        lsachequeleafdocument_gid = dt["lsachequeleafdocument_gid"].ToString(),
                        lsabankaccdtl_gid = dt["lsabankaccdtl_gid"].ToString(),
                    });
                    values.lsauploaddocument_list = getcreditbankacc_list;
                }
            }
            dt_datatable.Dispose();
            return true;
        }

public bool DaGetLSABankAccountSummary(string lsgeneratelsa_gid, MdlBankAccountlist values, string employee_gid)
        {
            msSQL = " select generatelsa_gid,disbursementamount_flag,lsabankaccdtl_gid, name, stakeholder_type, bank_name, branch_name, branch_address, micr_code, ifsc_code, accountholder_name,disbursementaccount_status, " +
                    " accounttype_gid, accounttype_name, bankaccount_number, confirmbankaccount_number, joint_account, " +
                    " jointaccountholder_name, chequebookfacility_available, accountopen_date, disbursement_accountstatus, credit_gid " +
                    " from ocs_trn_tlsabankaccountdtl where generatelsa_gid='" + lsgeneratelsa_gid + "' and generatelsa_gid <> '' and generatelsa_gid is not null";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getLSABankAccountSummary_list = new List<MdlBankAccount>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {


                    string lsgeneratelsa_flag = ""; string lsdisbursement_flag = "N"; string lsoveralldisbursement_flag = "N";

                    //msSQL = " select generatelsa_gid from ocs_trn_tcadcreditbankdtl where generatelsa_gid='" + dt["generatelsa_gid"].ToString() + "' and disbursementaccount_status = 'Yes' and disbursementamount_flag = 'Y'";
                    //lsgeneratelsa_flag = objdbconn.GetExecuteScalar(msSQL);
                    //if (lsgeneratelsa_flag != "")
                    //{
                    //    lsdisbursement_flag = "Y";
                    //}
                    //else
                    //{
                    msSQL = " select disbursementaccount_status from ocs_trn_tbankaccountstatus  where credit_gid = '" + dt["lsabankaccdtl_gid"].ToString() + "' and  rmdisbursementrequest_gid='" + employee_gid + "' and disbursementaccount_status = 'Yes' ";
                    lsgeneratelsa_flag = objdbconn.GetExecuteScalar(msSQL);
                    if (lsgeneratelsa_flag != "")
                    {
                        lsdisbursement_flag = "Y";
                    }
                    else
                    {
                        lsgeneratelsa_flag = "No";
                    }
                    //}

                    //if (lsdisbursement_flag == "N")
                    //{
                    //    lsoveralldisbursement_flag = "Y";
                    //}
                    getLSABankAccountSummary_list.Add(new MdlBankAccount
                    {
                        lsdisbursement_flag = lsdisbursement_flag,
                        lsgeneratelsa_flag = lsgeneratelsa_flag,

                        disbursementaccount_status = dt["disbursementaccount_status"].ToString(),
                        disbursementamount_flag = dt["disbursementamount_flag"].ToString(),
                        name = dt["name"].ToString(),
                        stakeholder_type = dt["stakeholder_type"].ToString(),
                        bank_name = dt["bank_name"].ToString(),
                        branch_name = dt["branch_name"].ToString(),
                        branch_address = dt["branch_address"].ToString(),
                        micr_code = dt["micr_code"].ToString(),
                        ifsc_code = dt["ifsc_code"].ToString(),
                        accountholder_name = dt["accountholder_name"].ToString(),
                        bankaccounttype_gid = dt["accounttype_gid"].ToString(),
                        bankaccounttype_name = dt["accounttype_name"].ToString(),
                        bankaccount_number = dt["bankaccount_number"].ToString(),
                        confirmbankaccountnumber = dt["confirmbankaccount_number"].ToString(),
                        joint_account = dt["joint_account"].ToString(),
                        jointaccountholder_name = dt["jointaccountholder_name"].ToString(),
                        chequebookfacility_available = dt["chequebookfacility_available"].ToString(),
                        accountopen_date = dt["accountopen_date"].ToString(),
                        disbursement_accountstatus = dt["disbursement_accountstatus"].ToString(),
                        lsabankaccdtl_gid = dt["lsabankaccdtl_gid"].ToString(),
                        credit_gid = dt["credit_gid"].ToString(),
                        generatelsa_gid = dt["generatelsa_gid"].ToString(),
                    });
                    values.MdlBankAccount = getLSABankAccountSummary_list;
                }
            }
            dt_datatable.Dispose();
            return true;
        }

        public bool DaGetLSABankAccountDisSummary(string lsgeneratelsa_gid, string rmdisbursementrequest_gid, MdlBankAccountlist values, string employee_gid)
        {
            msSQL = " select generatelsa_gid,disbursementamount_flag,lsabankaccdtl_gid, name, stakeholder_type, bank_name, branch_name, branch_address, micr_code, ifsc_code, accountholder_name,disbursementaccount_status, " +
                    " accounttype_gid, accounttype_name, bankaccount_number, confirmbankaccount_number, joint_account, " +
                    " jointaccountholder_name, chequebookfacility_available, accountopen_date, disbursement_accountstatus, credit_gid " +
                    " from ocs_trn_tlsabankaccountdtl where generatelsa_gid='" + lsgeneratelsa_gid + "' and generatelsa_gid <> '' and generatelsa_gid is not null";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getLSABankAccountSummary_list = new List<MdlBankAccount>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {


                    string lsgeneratelsa_flag = ""; string lsdisbursement_flag = "N"; string lsoveralldisbursement_flag = "N";

                    //msSQL = " select generatelsa_gid from ocs_trn_tcadcreditbankdtl where generatelsa_gid='" + dt["generatelsa_gid"].ToString() + "' and disbursementaccount_status = 'Yes' and disbursementamount_flag = 'Y'";
                    //lsgeneratelsa_flag = objdbconn.GetExecuteScalar(msSQL);
                    //if (lsgeneratelsa_flag != "")
                    //{
                    //    lsdisbursement_flag = "Y";
                    //}
                    //else
                    //{
                    msSQL = " select disbursementaccount_status from ocs_trn_tbankaccountstatus  where credit_gid = '" + dt["lsabankaccdtl_gid"].ToString() + "' and  (rmdisbursementrequest_gid='" + rmdisbursementrequest_gid + "' or " +
                    " rmdisbursementrequest_gid='" + employee_gid + "') and disbursementaccount_status = 'Yes' ";
                    lsgeneratelsa_flag = objdbconn.GetExecuteScalar(msSQL);
                    if (lsgeneratelsa_flag != "")
                    {
                        lsdisbursement_flag = "Y";
                    }
                    else
                    {
                        lsgeneratelsa_flag = "No";
                    }
                    //}

                    //if (lsdisbursement_flag == "N")
                    //{
                    //    lsoveralldisbursement_flag = "Y";
                    //}
                    getLSABankAccountSummary_list.Add(new MdlBankAccount
                    {
                        lsdisbursement_flag = lsdisbursement_flag,
                        lsgeneratelsa_flag = lsgeneratelsa_flag,

                        disbursementaccount_status = dt["disbursementaccount_status"].ToString(),
                        disbursementamount_flag = dt["disbursementamount_flag"].ToString(),
                        name = dt["name"].ToString(),
                        stakeholder_type = dt["stakeholder_type"].ToString(),
                        bank_name = dt["bank_name"].ToString(),
                        branch_name = dt["branch_name"].ToString(),
                        branch_address = dt["branch_address"].ToString(),
                        micr_code = dt["micr_code"].ToString(),
                        ifsc_code = dt["ifsc_code"].ToString(),
                        accountholder_name = dt["accountholder_name"].ToString(),
                        bankaccounttype_gid = dt["accounttype_gid"].ToString(),
                        bankaccounttype_name = dt["accounttype_name"].ToString(),
                        bankaccount_number = dt["bankaccount_number"].ToString(),
                        confirmbankaccountnumber = dt["confirmbankaccount_number"].ToString(),
                        joint_account = dt["joint_account"].ToString(),
                        jointaccountholder_name = dt["jointaccountholder_name"].ToString(),
                        chequebookfacility_available = dt["chequebookfacility_available"].ToString(),
                        accountopen_date = dt["accountopen_date"].ToString(),
                        disbursement_accountstatus = dt["disbursement_accountstatus"].ToString(),
                        lsabankaccdtl_gid = dt["lsabankaccdtl_gid"].ToString(),
                        credit_gid = dt["credit_gid"].ToString(),
                        generatelsa_gid = dt["generatelsa_gid"].ToString(),
                    });
                    values.MdlBankAccount = getLSABankAccountSummary_list;
                }
            }
            dt_datatable.Dispose();
            return true;
        }

        public bool DalsachequeleafdocumentUpload(HttpRequest httpRequest, lsauploaddocument objfilename, string employee_gid)
        {

            upload_list objdocumentmodel = new upload_list();
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string lsdocument_title = httpRequest.Form["document_title"].ToString();
            string lsgeneratelsa_gid = httpRequest.Form["generatelsa_gid"].ToString();
            string lsabankaccdtl_gid = httpRequest.Form["lsabankaccdtl_gid"].ToString();
            string project_flag = httpRequest.Form["project_flag"].ToString();
            String path = lspath;

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/LSAChequeLeafDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
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
                        //lspath = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "Master/LSAChequeLeafDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        //FileStream file = new FileStream(lspath + lsfile_gid, FileMode.Create, FileAccess.Write);
                        //ms.WriteTo(file);
                        //file.Close();
                        //ms.Close();


                        // Check Document validation;

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.message = "File format is not supported";
                            return false;
                        }
                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Master/LSAChequeLeafDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "Master/LSAChequeLeafDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";


                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "Master/LSAChequeLeafDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        msGetGid = objcmnfunctions.GetMasterGID("LCLG");
                        msSQL = " insert into ocs_trn_tlsachequeleafdocument( " +
                                " lsachequeleafdocument_gid," +
                                " generatelsa_gid, " +
                                " lsabankaccdtl_gid, " +
                                " document_title, " +
                                " document_name, " +
                                " document_path, " +
                                " created_by, " +
                                " created_date" +
                                " )values(" +
                                "'" + msGetGid + "'," +
                                "'" + lsgeneratelsa_gid + "'," +
                                "'" + employee_gid + "'," +
                                "'" + lsdocument_title.Replace("'", "") + "'," +
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

                        msSQL = " select lsachequeleafdocument_gid,lsabankaccdtl_gid,document_name,document_path,document_title from " +
                                " ocs_trn_tlsachequeleafdocument where (lsabankaccdtl_gid='" + employee_gid + "' or lsabankaccdtl_gid='" + lsabankaccdtl_gid + "')";
                        dt_datatable = objdbconn.GetDataTable(msSQL);
                        var getdocumentdtlList = new List<lsauploaddocument_list>();
                        if (dt_datatable.Rows.Count != 0)
                        {
                            foreach (DataRow dt in dt_datatable.Rows)
                            {
                                getdocumentdtlList.Add(new lsauploaddocument_list
                                {
                                    document_name = dt["document_name"].ToString(),
                                    document_title = dt["document_title"].ToString(),
                                    document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                                    lsachequeleafdocument_gid = dt["lsachequeleafdocument_gid"].ToString(),
                                    lsabankaccdtl_gid = dt["lsabankaccdtl_gid"].ToString(),

                                });
                                objfilename.lsauploaddocument_list = getdocumentdtlList;
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

    public void DaGetCreditBankAccountSummary(string application_gid, MdlBankAccountlist values, string employee_gid)
        {
            msSQL = " select group_gid, group_name as holder_name,'Group' as stakeholder_type from ocs_trn_tcadgroup " +
                    " where application_gid = '" + application_gid + "'  union" +
                    " select institution_gid,company_name as holder_name,stakeholder_type from ocs_trn_tcadinstitution " +
                    " where application_gid = '" + application_gid + "'  union" +
                    " select contact_gid, concat(first_name, ' ', middle_name, ' ', last_name) as holder_name, stakeholder_type from ocs_trn_tcadcontact " +
                    " where application_gid = '" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getlimit_info = new List<bankapplicationNameinfo>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getlimit_info.Add(new bankapplicationNameinfo
                    {
                        holder_gid = dr_datarow["group_gid"].ToString(),
                        holder_name = dr_datarow["holder_name"].ToString(),
                        stakeholder_type = (dr_datarow["stakeholder_type"].ToString()),
                    });
                }
            }
            dt_datatable.Dispose();
            string Name = "", stakeholder_type = "";
            msSQL = " select application_gid,disbursementamount_flag,creditbankdtl_gid,credit_gid , bank_name,branch_name,bank_address,micr_code,ifsc_code,bankaccount_number,disbursementaccount_status, " +
                    "  bankaccount_name,bankaccounttype_gid,bankaccounttype_name,confirmbankaccountnumber,accountopen_date,disbursement_accountstatus, " +
                    " joinaccount_status,joinaccount_name,chequebook_status , date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                    " date_format(a.updated_date, '%d-%m-%Y %h:%i %p') as updated_date, " +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, " +
                    " concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as updated_by " +
                    " from ocs_trn_tcadcreditbankdtl a " +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " left join hrm_mst_temployee d on a.updated_by = d.employee_gid " +
                    " left join adm_mst_tuser e on e.user_gid = d.user_gid " +
                    " where application_gid='" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcreditbankacc_list = new List<MdlBankAccount>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    var getnameinfo = getlimit_info.Where(x => x.holder_gid == dt["credit_gid"].ToString()).FirstOrDefault();
                    if (getnameinfo != null)
                    {
                        Name = getnameinfo.holder_name;
                        stakeholder_type = getnameinfo.stakeholder_type;
                    }
                    else
                    {
                        Name = "";
                        stakeholder_type = "";
                    }
                    string lsgeneratelsa_flag = ""; string lsdisbursement_flag = "N"; string lsoveralldisbursement_flag = "N";
                    //msSQL = " select generatelsa_gid from ocs_trn_tlsabankaccountdtl  where application_gid='" + dt["application_gid"].ToString() + "' and disbursementaccount_status = 'Yes' and disbursementamount_flag = 'Y'";
                    //lsgeneratelsa_flag = objdbconn.GetExecuteScalar(msSQL);
                    //if (lsgeneratelsa_flag != "")
                    //{
                    //    lsdisbursement_flag = "Y";
                    //}
                    //else
                    //{
                    msSQL = " select disbursementaccount_status from ocs_trn_tbankaccountstatus where credit_gid = '" + dt["creditbankdtl_gid"].ToString() + "' and rmdisbursementrequest_gid='" + employee_gid + "' and disbursementaccount_status = 'Yes'";
                    lsgeneratelsa_flag = objdbconn.GetExecuteScalar(msSQL);
                    if (lsgeneratelsa_flag != "")
                    {
                        lsdisbursement_flag = "Y";
                    }
                    else
                    {
                        lsgeneratelsa_flag = "No";
                    }
                    //}

                    //if (lsdisbursement_flag == "N")
                    //{
                    //    lsoveralldisbursement_flag = "Y";
                    //}
                    getcreditbankacc_list.Add(new MdlBankAccount
                    {
                        name = Name,
                        lsdisbursement_flag = lsdisbursement_flag,
                        lsgeneratelsa_flag = lsgeneratelsa_flag,
                        stakeholder_type = stakeholder_type,
                        disbursementaccount_status = dt["disbursementaccount_status"].ToString(),
                        disbursementamount_flag = dt["disbursementamount_flag"].ToString(),                     
                        bank_name = dt["bank_name"].ToString(),
                        branch_name = dt["branch_name"].ToString(),
                        bank_address = dt["bank_address"].ToString(),
                        micr_code = dt["micr_code"].ToString(),
                        ifsc_code = dt["ifsc_code"].ToString(),
                        accountholder_name = dt["bankaccount_name"].ToString(),
                        bankaccounttype_gid = dt["bankaccounttype_gid"].ToString(),
                        bankaccounttype_name = dt["bankaccounttype_name"].ToString(),
                        bankaccount_number = dt["bankaccount_number"].ToString(),
                        confirmbankaccountnumber = dt["confirmbankaccountnumber"].ToString(),
                        joint_account = dt["joinaccount_status"].ToString(),
                        jointaccountholder_name = dt["joinaccount_name"].ToString(),
                        chequebookfacility_available = dt["chequebook_status"].ToString(),
                        accountopen_date = dt["accountopen_date"].ToString(),
                        disbursement_accountstatus = dt["disbursement_accountstatus"].ToString(),
                        creditbankdtl_gid = dt["creditbankdtl_gid"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                    });
                    values.MdlBankAccount = getcreditbankacc_list;
                }
            }
            dt_datatable.Dispose();
        }


        public void DaGetCreditBankAccountDisSummary(string application_gid, string rmdisbursementrequest_gid, MdlBankAccountlist values, string employee_gid)
        {
            msSQL = " select group_gid, group_name as holder_name,'Group' as stakeholder_type from ocs_trn_tcadgroup " +
                    " where application_gid = '" + application_gid + "'  union" +
                    " select institution_gid,company_name as holder_name,stakeholder_type from ocs_trn_tcadinstitution " +
                    " where application_gid = '" + application_gid + "'  union" +
                    " select contact_gid, concat(first_name, ' ', middle_name, ' ', last_name) as holder_name, stakeholder_type from ocs_trn_tcadcontact " +
                    " where application_gid = '" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getlimit_info = new List<bankapplicationNameinfo>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getlimit_info.Add(new bankapplicationNameinfo
                    {
                        holder_gid = dr_datarow["group_gid"].ToString(),
                        holder_name = dr_datarow["holder_name"].ToString(),
                        stakeholder_type = (dr_datarow["stakeholder_type"].ToString()),
                    });
                }
            }
            dt_datatable.Dispose();
            string Name = "", stakeholder_type = "";
            msSQL = " select application_gid,disbursementamount_flag,creditbankdtl_gid,credit_gid , bank_name,branch_name,bank_address,micr_code,ifsc_code,bankaccount_number,disbursementaccount_status, " +
                    "  bankaccount_name,bankaccounttype_gid,bankaccounttype_name,confirmbankaccountnumber,accountopen_date,disbursement_accountstatus, " +
                    " joinaccount_status,joinaccount_name,chequebook_status , date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                    " date_format(a.updated_date, '%d-%m-%Y %h:%i %p') as updated_date, " +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, " +
                    " concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as updated_by " +
                    " from ocs_trn_tcadcreditbankdtl a " +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " left join hrm_mst_temployee d on a.updated_by = d.employee_gid " +
                    " left join adm_mst_tuser e on e.user_gid = d.user_gid " +
                    " where application_gid='" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcreditbankacc_list = new List<MdlBankAccount>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    var getnameinfo = getlimit_info.Where(x => x.holder_gid == dt["credit_gid"].ToString()).FirstOrDefault();
                    if (getnameinfo != null)
                    {
                        Name = getnameinfo.holder_name;
                        stakeholder_type = getnameinfo.stakeholder_type;
                    }
                    else
                    {
                        Name = "";
                        stakeholder_type = "";
                    }
                    string lsgeneratelsa_flag = ""; string lsdisbursement_flag = "N"; string lsoveralldisbursement_flag = "N";
                    //msSQL = " select generatelsa_gid from ocs_trn_tlsabankaccountdtl  where application_gid='" + dt["application_gid"].ToString() + "' and disbursementaccount_status = 'Yes' and disbursementamount_flag = 'Y'";
                    //lsgeneratelsa_flag = objdbconn.GetExecuteScalar(msSQL);
                    //if (lsgeneratelsa_flag != "")
                    //{
                    //    lsdisbursement_flag = "Y";
                    //}
                    //else
                    //{
                    msSQL = " select disbursementaccount_status from ocs_trn_tbankaccountstatus where credit_gid = '" + dt["creditbankdtl_gid"].ToString() + "' and (rmdisbursementrequest_gid='" + rmdisbursementrequest_gid + "' or " +
                    " rmdisbursementrequest_gid='" + employee_gid + "') and disbursementaccount_status = 'Yes'";
                    lsgeneratelsa_flag = objdbconn.GetExecuteScalar(msSQL);
                    if (lsgeneratelsa_flag != "")
                    {
                        lsdisbursement_flag = "Y";
                    }
                    else
                    {
                        lsgeneratelsa_flag = "No";
                    }
                    //}

                    //if (lsdisbursement_flag == "N")
                    //{
                    //    lsoveralldisbursement_flag = "Y";
                    //}
                    getcreditbankacc_list.Add(new MdlBankAccount
                    {
                        name = Name,
                        lsdisbursement_flag = lsdisbursement_flag,
                        lsgeneratelsa_flag = lsgeneratelsa_flag,
                        stakeholder_type = stakeholder_type,
                        disbursementaccount_status = dt["disbursementaccount_status"].ToString(),
                        disbursementamount_flag = dt["disbursementamount_flag"].ToString(),                     
                        bank_name = dt["bank_name"].ToString(),
                        branch_name = dt["branch_name"].ToString(),
                        bank_address = dt["bank_address"].ToString(),
                        micr_code = dt["micr_code"].ToString(),
                        ifsc_code = dt["ifsc_code"].ToString(),
                        accountholder_name = dt["bankaccount_name"].ToString(),
                        bankaccounttype_gid = dt["bankaccounttype_gid"].ToString(),
                        bankaccounttype_name = dt["bankaccounttype_name"].ToString(),
                        bankaccount_number = dt["bankaccount_number"].ToString(),
                        confirmbankaccountnumber = dt["confirmbankaccountnumber"].ToString(),
                        joint_account = dt["joinaccount_status"].ToString(),
                        jointaccountholder_name = dt["joinaccount_name"].ToString(),
                        chequebookfacility_available = dt["chequebook_status"].ToString(),
                        accountopen_date = dt["accountopen_date"].ToString(),
                        disbursement_accountstatus = dt["disbursement_accountstatus"].ToString(),
                        creditbankdtl_gid = dt["creditbankdtl_gid"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                    });
                    values.MdlBankAccount = getcreditbankacc_list;
                }
            }
            dt_datatable.Dispose();
        }


        public void DaupdateDisbursementStatus(string employee_gid, MdlBankAccount values)
        {
            if (values.creditbankdtl_gid != "")
            {
                msSQL = " update ocs_trn_tcadcreditbankdtl set disbursement_accountstatus='" + values.disbursement_accountstatus + "' " +
                        " where creditbankdtl_gid='" + values.creditbankdtl_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else
            {
                msSQL = " update ocs_trn_tlsabankaccountdtl set disbursement_accountstatus='" + values.disbursement_accountstatus + "' " +
                        " where lsabankaccdtl_gid='" + values.lsabankaccdtl_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            if (mnResult == 1)
            {
                values.status = true;
                values.message = "Disbursement Account Status are updated Successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
        }

        public void DaGetLSABankAccountdetail(string lsabankaccdtl_gid, MdlBankAccount values)
        {
            msSQL = " select lsabankaccdtl_gid, name, stakeholder_type, bank_name, branch_name, branch_address, micr_code, ifsc_code, accountholder_name, " +
                    " accounttype_gid, accounttype_name, bankaccount_number, confirmbankaccount_number, joint_account,credit_gid, " +
                    " date_format(accountopen_date,'%d-%m-%Y') as accountopen_date, accountopen_date as accountopenDate, " +
                    " jointaccountholder_name, chequebookfacility_available, disbursement_accountstatus " +
                    " from ocs_trn_tlsabankaccountdtl where lsabankaccdtl_gid='" + lsabankaccdtl_gid + "'";

            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                values.name = objODBCDataReader["name"].ToString();
                values.stakeholder_type = objODBCDataReader["stakeholder_type"].ToString();
                values.bank_name = objODBCDataReader["bank_name"].ToString();
                values.branch_name = objODBCDataReader["branch_name"].ToString();
                values.bank_address = objODBCDataReader["branch_address"].ToString();
                values.micr_code = objODBCDataReader["micr_code"].ToString();
                values.ifsc_code = objODBCDataReader["ifsc_code"].ToString();
                values.accountholder_name = objODBCDataReader["accountholder_name"].ToString();
                values.bankaccounttype_gid = objODBCDataReader["accounttype_gid"].ToString();
                values.bankaccounttype_name = objODBCDataReader["accounttype_name"].ToString();
                values.bankaccount_number = objODBCDataReader["bankaccount_number"].ToString();
                values.confirmbankaccountnumber = objODBCDataReader["confirmbankaccount_number"].ToString();
                values.joint_account = objODBCDataReader["joint_account"].ToString();
                values.jointaccountholder_name = objODBCDataReader["jointaccountholder_name"].ToString();
                values.chequebookfacility_available = objODBCDataReader["chequebookfacility_available"].ToString();
                values.accountopen_date = objODBCDataReader["accountopen_date"].ToString();
                values.disbursement_accountstatus = objODBCDataReader["disbursement_accountstatus"].ToString();
                if (objODBCDataReader["accountopenDate"].ToString() != "")
                {
                    values.accountopendate = Convert.ToDateTime(objODBCDataReader["accountopenDate"].ToString());
                }
                values.credit_gid = objODBCDataReader["credit_gid"].ToString();
            }
            objODBCDataReader.Close();

            msSQL = " select lsachequeleafdocument_gid,lsabankaccdtl_gid,document_name,document_path,document_title from " +
                              " ocs_trn_tlsachequeleafdocument where lsabankaccdtl_gid='" + lsabankaccdtl_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdocumentdtlList = new List<credituploaddocument_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getdocumentdtlList.Add(new credituploaddocument_list
                    {
                        chequeleaf_name = dt["document_name"].ToString(),
                        document_title = dt["document_title"].ToString(),
                        //chequeleaf_path = HttpContext.Current.Server.MapPath(dt["document_path"].ToString()),
                        chequeleaf_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),

                    });
                    values.credituploaddocument_list = getdocumentdtlList;
                }
            }
            dt_datatable.Dispose();

            values.status = true;
        }

        public void DaGetCreditBankAccountdetail(string creditbankdtl_gid, MdlBankAccount values)
        {

            msSQL = " select creditbankdtl_gid,credit_gid, application_gid, bank_name,branch_name,bank_address,micr_code,ifsc_code,bankaccount_number, " +
                   "  bankaccount_name,bankaccounttype_gid,bankaccounttype_name,confirmbankaccountnumber,disbursement_accountstatus, " +
                   "  date_format(a.accountopen_date,'%d-%m-%Y') as accountopen_date, " +
                   " joinaccount_status,joinaccount_name,chequebook_status , date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                   " date_format(a.updated_date, '%d-%m-%Y %h:%i %p') as updated_date, " +
                   " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, " +
                   " concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as updated_by " +
                   " from ocs_trn_tcadcreditbankdtl a " +
                   " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                   " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                   " left join hrm_mst_temployee d on a.updated_by = d.employee_gid " +
                   " left join adm_mst_tuser e on e.user_gid = d.user_gid " +
                   " where creditbankdtl_gid='" + creditbankdtl_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                values.application_gid = objODBCDataReader["application_gid"].ToString();
                values.credit_gid = objODBCDataReader["credit_gid"].ToString();
                values.bank_name = objODBCDataReader["bank_name"].ToString();
                values.branch_name = objODBCDataReader["branch_name"].ToString();
                values.bank_address = objODBCDataReader["bank_address"].ToString();
                values.micr_code = objODBCDataReader["micr_code"].ToString();
                values.ifsc_code = objODBCDataReader["ifsc_code"].ToString();
                values.accountholder_name = objODBCDataReader["bankaccount_name"].ToString();
                values.bankaccounttype_gid = objODBCDataReader["bankaccounttype_gid"].ToString();
                values.bankaccounttype_name = objODBCDataReader["bankaccounttype_name"].ToString();
                values.bankaccount_number = objODBCDataReader["bankaccount_number"].ToString();
                values.confirmbankaccountnumber = objODBCDataReader["confirmbankaccountnumber"].ToString();
                values.joint_account = objODBCDataReader["joinaccount_status"].ToString();
                values.jointaccountholder_name = objODBCDataReader["joinaccount_name"].ToString();
                values.chequebookfacility_available = objODBCDataReader["chequebook_status"].ToString();
                values.accountopen_date = objODBCDataReader["accountopen_date"].ToString();
                values.disbursement_accountstatus = objODBCDataReader["disbursement_accountstatus"].ToString();
            }
            objODBCDataReader.Close();

            msSQL = " select group_gid, group_name as holder_name,'Group' as stakeholder_type from ocs_trn_tcadgroup " +
                   " where group_gid = '" + values.credit_gid + "' and application_gid='" + values.application_gid + "'  union" +
                   " select institution_gid,company_name as holder_name,stakeholder_type from ocs_trn_tcadinstitution " +
                   " where institution_gid = '" + values.credit_gid + "' and application_gid='" + values.application_gid + "'  union" +
                   " select contact_gid, concat(first_name, ' ', middle_name, ' ', last_name) as holder_name, stakeholder_type from ocs_trn_tcadcontact " +
                   " where contact_gid = '" + values.credit_gid + "' and application_gid='" + values.application_gid + "' ";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                values.name = objODBCDataReader["holder_name"].ToString();
                values.stakeholder_type = objODBCDataReader["stakeholder_type"].ToString();
            }
            objODBCDataReader.Close();

            msSQL = " select creditbankdtl2cheque_gid,creditbankdtl_gid,chequeleaf_name,chequeleaf_path,document_title from " +
                    " ocs_trn_tcadcreditbankdtl2cheque where creditbankdtl_gid='" + creditbankdtl_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdocumentdtlList = new List<credituploaddocument_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getdocumentdtlList.Add(new credituploaddocument_list
                    {
                        chequeleaf_name = dt["chequeleaf_name"].ToString(),
                        document_title = dt["document_title"].ToString(),
                        chequeleaf_path = objcmnstorage.EncryptData(dt["chequeleaf_path"].ToString()),
                        creditbankdtl_gid = dt["creditbankdtl_gid"].ToString(),
                        creditbankdtl2cheque_gid = dt["creditbankdtl2cheque_gid"].ToString(),

                    });
                    values.credituploaddocument_list = getdocumentdtlList;
                }
            }
            dt_datatable.Dispose();

            values.status = true;
        }

        public void DaGetlsaFeeschargesDetail(string generatelsa_gid, lsaFeecharges values)
        {
            msSQL = " select b.lsachargestype_gid,b.charge_typename,a.lsafeescharge_gid,application_gid, application2servicecharge_gid, processing_fee, processing_collectiontype, doc_charges," +
                   " doccharge_collectiontype,a.fieldvisit_charges,a.fieldvisit_charges_collectiontype,adhoc_fee,adhoc_collectiontype," +
                   " life_insurance,lifeinsurance_collectiontype,acct_insurance,total_collect,total_deduct,product_type, acctinsurance_collectiontype, " +
                   " processingfees_flag,documentcharges_flag,fieldvisitcharges_flag,adhocfee_flag,termlifeinsurance_flag,personalaccidentinsurance " +
                   " from ocs_trn_tlsafeescharge a " +
                   " left join ocs_trn_tlsachargestype b on b.lsafeescharge_gid = a.lsafeescharge_gid " +
                   " where a.generatelsa_gid = '" + generatelsa_gid + "'  group by a.lsafeescharge_gid " +
                   " order by application2servicecharge_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getproductcharges_list = new List<lsaFeecharges_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getproductcharges_list.Add(new lsaFeecharges_list
                    {
                        lsafeescharge_gid = (dr_datarow["lsafeescharge_gid"].ToString()),
                        processing_fee = (dr_datarow["processing_fee"].ToString()),
                        processing_collectiontype = (dr_datarow["processing_collectiontype"].ToString()),
                        doc_charges = (dr_datarow["doc_charges"].ToString()),
                        doccharge_collectiontype = (dr_datarow["doccharge_collectiontype"].ToString()),
                        fieldvisit_charge = (dr_datarow["fieldvisit_charges"].ToString()),
                        fieldvisit_collectiontype = (dr_datarow["fieldvisit_charges_collectiontype"].ToString()),
                        adhoc_fee = (dr_datarow["adhoc_fee"].ToString()),
                        adhoc_collectiontype = (dr_datarow["adhoc_collectiontype"].ToString()),
                        life_insurance = (dr_datarow["life_insurance"].ToString()),
                        lifeinsurance_collectiontype = (dr_datarow["lifeinsurance_collectiontype"].ToString()),
                        acct_insurance = (dr_datarow["acct_insurance"].ToString()),
                        total_collect = (dr_datarow["total_collect"].ToString()),
                        total_deduct = (dr_datarow["total_deduct"].ToString()),
                        product_type = (dr_datarow["product_type"].ToString()),
                        acctinsurance_collectiontype = (dr_datarow["acctinsurance_collectiontype"].ToString()),
                        processingfees_flag = (dr_datarow["processingfees_flag"].ToString()),
                        documentcharges_flag = (dr_datarow["documentcharges_flag"].ToString()),
                        fieldvisitcharges_flag = (dr_datarow["fieldvisitcharges_flag"].ToString()),
                        adhocfee_flag = (dr_datarow["adhocfee_flag"].ToString()),
                        termlifeinsurance_flag = (dr_datarow["termlifeinsurance_flag"].ToString()),
                        personalaccidentinsurance = (dr_datarow["personalaccidentinsurance"].ToString()),
                        lsachargestype_gid = (dr_datarow["personalaccidentinsurance"].ToString()),
                        charge_typename = (dr_datarow["charge_typename"].ToString())
                    });
                }
                values.lsaFeecharges_list = getproductcharges_list;
            }
            dt_datatable.Dispose();

            values.status = true;
        }

        public void DaGetBankName(MdlBankNamelist values)
        {
            try
            {
                msSQL = " SELECT bankname_gid,bankname_name FROM ocs_mst_tbankname a " +
                        " where a.status='Y' order by a.bankname_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getbankname_list = new List<MdlBankName>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getbankname_list.Add(new MdlBankName
                        {
                            bankname_gid = (dr_datarow["bankname_gid"].ToString()),
                            bank_name = (dr_datarow["bankname_name"].ToString()),
                        });
                    }
                    values.MdlBankName = getbankname_list;
                }
                dt_datatable.Dispose();
            }
            catch
            {
            }
        }

        public bool DaPostDocumentCharge(string user_gid, lsadocumentationcharge values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("LDCG");

            if (values.charge_remarks == null)
                values.charge_remarks = "";
            if (values.alreadycol_recoveredamount == null || values.alreadycol_recoveredamount == "")
                values.alreadycol_recoveredamount = "0";
            if (values.alreadycol_gstinpercentage == null)
                values.alreadycol_gstinpercentage = "";
            if (values.alreadycol_gst == null)
                values.alreadycol_gst = "";
            if (values.alreadycol_totalamount == null || values.alreadycol_totalamount == "")
                values.alreadycol_totalamount = "0";
            if (values.tobecol_gstinpercentage == null)
                values.tobecol_gstinpercentage = "";
            if (values.tobecol_gst == null)
                values.tobecol_gst = "";
            if (values.tobecol_totalamount == null || values.tobecol_totalamount == "")
                values.tobecol_totalamount = "0";
            if (values.tobecol_recoveredamount == null || values.tobecol_recoveredamount == "")
                values.tobecol_recoveredamount = "0";
            if (values.alreadycol_remainingamountcollected == null)
                values.alreadycol_remainingamountcollected = "";
            if (values.tobecol_remainingamountcollected == null)
                values.tobecol_remainingamountcollected = "";

            msSQL = " insert into ocs_trn_tlsachargestype(" +
                " lsachargestype_gid," +
                " lsafeescharge_gid," +
                " generatelsa_gid, " +
                " charge_typeid, " +
                " charge_typename, " +
                " charge_applicable," +
                " charge_amount, " +
                " charge_remarks," +
                " alreadycol_recoveredamount," +
                " alreadycol_gstinpercentage," +
                " alreadycol_gst," +
                " alreadycol_totalamount," +
                " tobecol_gstinpercentage," +
                " tobecol_gst," +
                " tobecol_totalamount," +
                " tobecol_recoveredamount," +
                //" alreadycol_remainingamountcollected, " +
                //" alreadycol_Chequenodetails," +
                //" alreadycol_Cheque_date," +
                //" alreadycol_banknamegid," +
                //" alreadycol_bankname," +
                //" alreadycol_accountnumber, " +
                " tobecol_remainingamountcollected, " +
                " tobecol_Chequenodetails," +
                " tobecol_Cheque_date," +
                " tobecol_banknamegid," +
                " tobecol_bankname," +
                " tobecol_accountnumber, " +
                " created_by," +
                " created_date)" +
                " values(" +
                "'" + msGetGid + "'," +
                "'" + values.lsafeescharge_gid + "'," +
                "'" + values.generatelsa_gid + "'," +
                "'" + values.charge_typeid + "'," +
                "'" + values.charge_typename + "'," +
                "'" + values.charge_applicable + "'," ;
            if (values.charge_amount == null || values.charge_amount == "")
            {
                msSQL += "'0.00',";
            }
            else
            {
                msSQL += "'" + values.charge_amount + "'," ;
            }
            msSQL += "'" + values.charge_remarks.Replace("'", "") + "'," +
                "'" + values.alreadycol_recoveredamount.Replace(",", "") + "'," +
                "'" + values.alreadycol_gstinpercentage + "'," +
                "'" + values.alreadycol_gst + "'," +
                "'" + values.alreadycol_totalamount.Replace(",", "") + "'," +
                "'" + values.tobecol_gstinpercentage + "'," +
                "'" + values.tobecol_gst + "'," +
                "'" + values.tobecol_totalamount.Replace(",", "") + "'," +
                "'" + values.tobecol_recoveredamount.Replace(",", "") + "',";
                //"'" + values.alreadycol_remainingamountcollected + "',";
            //if (values.alreadycol_remainingamountcollected == "Collect")
            //{
            //    msSQL += "'" + values.alreadycol_Chequenodetails + "'," +
            //    "'" + Convert.ToDateTime(values.alreadycol_Cheque_date).ToString("yyyy-MM-dd") + "'," +
            //    "'" + values.alreadycol_banknamegid + "'," +
            //    "'" + values.alreadycol_bankname + "'," +
            //    "'" + values.alreadycol_accountnumber + "',";
            //}
            //else
            //{
            //    msSQL += "null," +
            //   "null," +
            //   "null," +
            //   "null," +
            //   "null,";
            //}
            msSQL += "'" + values.tobecol_remainingamountcollected + "',";
            if (values.tobecol_remainingamountcollected == "Collect")
            {
                msSQL += "'" + values.tobecol_Chequenodetails + "'," +
                "'" + Convert.ToDateTime(values.tobecol_Cheque_date).ToString("yyyy-MM-dd") + "'," +
                "'" + values.tobecol_banknamegid + "'," +
                "'" + values.tobecol_bankname + "'," +
                "'" + values.tobecol_accountnumber + "',";
            }
            else
            {
                msSQL += "null," +
               "null," +
               "null," +
               "null," +
               "null,";
            }
            msSQL += "'" + user_gid + "'," +
                "current_timestamp)";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "update ocs_trn_tlsafeescharge set ";
                if (values.charge_typeid == getChargeType.ProcessingFees)
                    msSQL += " processingfees_flag ='Y' ";
                if (values.charge_typeid == getChargeType.DocumentCharges)
                    msSQL += " documentcharges_flag ='Y' ";
                if (values.charge_typeid == getChargeType.FieldVisitCharges)
                    msSQL += " fieldvisitcharges_flag ='Y' ";
                if (values.charge_typeid == getChargeType.AdhocFee)
                    msSQL += " adhocfee_flag ='Y' ";
                if (values.charge_typeid == getChargeType.TermLifeInsurance)
                    msSQL += " termlifeinsurance_flag ='Y' ";
                if (values.charge_typeid == getChargeType.PersonalAccidentInsurance)
                    msSQL += " personalaccidentinsurance ='Y' ";
                msSQL += " where lsafeescharge_gid = '" + values.lsafeescharge_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.message = "Charges Details are Added Successfully";
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

        public void DaGetlsachargesDetail(string lsafeescharge_gid, string charge_type, lsadocumentationcharge values)
        {
            try
            {
                msSQL = " select lsachargestype_gid,lsafeescharge_gid, charge_typeid,charge_typename,charge_applicable,charge_remarks, " +
                   " alreadycol_recoveredamount,alreadycol_gstinpercentage,alreadycol_gst,alreadycol_totalamount,tobecol_recoveredamount, " +
                   " tobecol_gstinpercentage,tobecol_gst,tobecol_totalamount ,charge_amount,alreadycol_accountnumber,tobecol_accountnumber, " +
                   " alreadycol_remainingamountcollected,tobecol_remainingamountcollected,alreadycol_Chequenodetails,tobecol_Chequenodetails, " +
                   " date_format(alreadycol_Cheque_date, '%d-%m-%Y %h:%i %p') as alreadycol_Cheque_date,alreadycol_banknamegid,alreadycol_bankname, " +
                    " date_format(tobecol_Cheque_date, '%d-%m-%Y %h:%i %p') as tobecol_Cheque_date,  tobecol_Cheque_date as tobecol_ChequeDate," +
                   " fieldvisit_charges_collectiontype, alreadycol_Cheque_date as alreadycol_ChequeDate, tobecol_banknamegid, tobecol_bankname, created_by " +
                   " from ocs_trn_tlsachargestype where lsafeescharge_gid = '" + lsafeescharge_gid + "' and charge_typeid='" + charge_type + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows == true)
                {
                    values.lsachargestype_gid = (objODBCDataReader["lsachargestype_gid"].ToString());
                    values.lsafeescharge_gid = (objODBCDataReader["lsafeescharge_gid"].ToString());
                    values.charge_typeid = (objODBCDataReader["charge_typeid"].ToString());
                    values.charge_typename = (objODBCDataReader["charge_typename"].ToString());
                    values.charge_applicable = (objODBCDataReader["charge_applicable"].ToString());
                    values.charge_remarks = (objODBCDataReader["charge_remarks"].ToString());
                    values.charge_amount = (objODBCDataReader["charge_amount"].ToString());
                    values.alreadycol_recoveredamount = (objODBCDataReader["alreadycol_recoveredamount"].ToString());
                    values.alreadycol_gstinpercentage = (objODBCDataReader["alreadycol_gstinpercentage"].ToString());
                    values.alreadycol_gst = (objODBCDataReader["alreadycol_gst"].ToString());
                    values.alreadycol_totalamount = (objODBCDataReader["alreadycol_totalamount"].ToString());
                    values.alreadycol_remainingamountcollected = (objODBCDataReader["alreadycol_remainingamountcollected"].ToString());
                    values.tobecol_remainingamountcollected = (objODBCDataReader["tobecol_remainingamountcollected"].ToString());
                    values.fieldvisit_charges_collectiontype = (objODBCDataReader["fieldvisit_charges_collectiontype"].ToString());
                    values.alreadycol_Chequenodetails = (objODBCDataReader["alreadycol_Chequenodetails"].ToString());
                    values.alreadycol_Cheque_date = (objODBCDataReader["alreadycol_Cheque_date"].ToString());
                    if (objODBCDataReader["alreadycol_ChequeDate"].ToString() != "")
                    {
                        values.alreadycol_ChequeDate = Convert.ToDateTime(objODBCDataReader["alreadycol_ChequeDate"].ToString());
                    }
                    values.alreadycol_banknamegid = (objODBCDataReader["alreadycol_banknamegid"].ToString());
                    values.alreadycol_bankname = (objODBCDataReader["alreadycol_bankname"].ToString());
                    values.tobecol_recoveredamount = (objODBCDataReader["tobecol_recoveredamount"].ToString());
                    values.tobecol_gstinpercentage = (objODBCDataReader["tobecol_gstinpercentage"].ToString());
                    values.tobecol_gst = (objODBCDataReader["tobecol_gst"].ToString());
                    values.tobecol_totalamount = (objODBCDataReader["tobecol_totalamount"].ToString());
                    values.tobecol_Chequenodetails = (objODBCDataReader["tobecol_Chequenodetails"].ToString());
                    values.tobecol_Cheque_date = (objODBCDataReader["tobecol_Cheque_date"].ToString());
                    if (objODBCDataReader["tobecol_ChequeDate"].ToString() != "")
                    {
                        values.tobecol_ChequeDate = Convert.ToDateTime(objODBCDataReader["tobecol_ChequeDate"].ToString());
                    }
                    values.tobecol_banknamegid = (objODBCDataReader["tobecol_banknamegid"].ToString());
                    values.tobecol_bankname = (objODBCDataReader["tobecol_bankname"].ToString());
                    values.alreadycol_accountnumber = (objODBCDataReader["alreadycol_accountnumber"].ToString());
                    values.tobecol_accountnumber = (objODBCDataReader["tobecol_accountnumber"].ToString());
                }
                objODBCDataReader.Close();
                values.status = true;
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = ex.ToString();
            }
        }

        public bool DaPostUpdateChargeDetail(string user_gid, lsadocumentationcharge values)
        {
            msSQL = " update ocs_trn_tlsachargestype set " +
                //" charge_typeid ='" + values.charge_typeid + "', " +
                //" charge_typename ='" + values.charge_typename + "', " +
                " charge_applicable ='" + values.charge_applicable + "', " +
                " charge_remarks ='" + values.charge_remarks + "', " +
                " alreadycol_recoveredamount ='" + values.alreadycol_recoveredamount.Replace(",", "") + "', " +
                " alreadycol_gstinpercentage ='" + values.alreadycol_gstinpercentage + "', " +
                " alreadycol_gst ='" + values.alreadycol_gst + "', " +
                " alreadycol_totalamount ='" + values.alreadycol_totalamount.Replace(",", "") + "', " +
                " alreadycol_remainingamountcollected ='" + values.alreadycol_remainingamountcollected + "', " +
                " tobecol_recoveredamount ='" + values.tobecol_recoveredamount.Replace(",", "") + "', " +
                " fieldvisit_charges_collectiontype ='" + values.fieldvisit_charges_collectiontype + "', " +
                " alreadycol_Chequenodetails ='" + values.alreadycol_Chequenodetails + "', ";
            if (values.alreadycol_Cheque_date == null || values.alreadycol_Cheque_date == "")
                msSQL += "alreadycol_Cheque_date = null,";
            else
                msSQL += "alreadycol_Cheque_date = '" + Convert.ToDateTime(values.alreadycol_Cheque_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            msSQL += " alreadycol_banknamegid ='" + values.alreadycol_banknamegid + "', " +
                " alreadycol_bankname ='" + values.alreadycol_bankname + "', " +
                " tobecol_gstinpercentage ='" + values.tobecol_gstinpercentage + "', " +
                " tobecol_gst ='" + values.tobecol_gst + "', " +
                " tobecol_totalamount ='" + values.tobecol_totalamount + "', " +
                " tobecol_remainingamountcollected ='" + values.tobecol_remainingamountcollected + "', " +
                " tobecol_Chequenodetails ='" + values.tobecol_Chequenodetails + "', ";
            if (values.tobecol_Cheque_date == null || values.tobecol_Cheque_date == "")
                msSQL += "tobecol_Cheque_date = null,";
            else
                msSQL += "tobecol_Cheque_date = '" + Convert.ToDateTime(values.tobecol_Cheque_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            msSQL += " tobecol_banknamegid ='" + values.tobecol_banknamegid + "', " +
                " tobecol_bankname ='" + values.tobecol_bankname + "', " +
                " updated_by ='" + user_gid + "', " +
                " updated_date = current_timestamp " +
                " where lsachargestype_gid ='" + values.lsachargestype_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.message = "Charges Details are Updated Successfully";
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

        public bool DaPostProcessingFee(string user_gid, lsaprocessingfees values)
        {
            string msGetGIDPS = objcmnfunctions.GetMasterGID("LPFG");
            msSQL = " insert into ocs_trn_tlsaprocessingfees(" +
                      " lsaprocessingfees_gid," +
                      " lsafeescharge_gid," +
                      " generatelsa_gid," +
                      " recovered_status," +
                      " recovered_amount," +
                      " Chequeno_details," +
                      " Cheque_date," +
                      " processingfees_remarks," +
                      " bank_namegid," +
                      " bank_name," +
                      " created_by," +
                      " created_date)" +
                      " values(" +
                      "'" + msGetGIDPS + "'," +
                       "'" + values.lsafeescharge_gid + "'," +
                       "'" + values.generatelsa_gid + "'," +
                       "'" + values.recovered_status + "'," +
                       "'" + values.recovered_amount + "'," +
                       "'" + values.Chequeno_details + "'," +
                       "'" + values.Cheque_date + "'," +
                       "'" + values.processingfees_remarks.Replace("'", "") + "'," +
                       "'" + values.bank_namegid + "'," +
                       "'" + values.bank_name + "'," +
                       "'" + user_gid + "'," +
                       "current_timestamp)";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.message = "Processing Fees are Added Successfully";
                values.status = true;
                return true;
            }
            else
            {
                values.message = "Error Occured while adding Processing Fees ";
                values.status = false;
                return false;
            }
        }

        public void DaPostcollateraldetails(string employee_gid, lsacollateraldetails values)
        {
            msSQL = " select product_type, producttype_gid, productsubtype_gid, productsub_type " +
                    " from ocs_mst_tapplication2loan where application2loan_gid = '" + values.application2loan_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                values.product_type = objODBCDataReader["product_type"].ToString();
                values.producttype_gid = objODBCDataReader["producttype_gid"].ToString();
                values.productsubtype_gid = objODBCDataReader["productsubtype_gid"].ToString();
                values.product_subtype = objODBCDataReader["productsub_type"].ToString();
            }
            objODBCDataReader.Close();
            msSQL = " SELECT loantype_gid FROM ocs_mst_tloantype a where status_log = 'Y' and loan_type = 'Secured'";
            values.loantype_gid = objdbconn.GetExecuteScalar(msSQL);
            values.loan_type = "Secured";

            msSQL = " update ocs_trn_tcadapplication2loan set " +
                    " product_type='" + values.product_type + "'," +
                    " producttype_gid='" + values.producttype_gid + "'," +
                    " productsub_type='" + values.product_subtype + "'," +
                    " productsubtype_gid='" + values.productsubtype_gid + "'," +
                    " loantype_gid='" + values.loantype_gid + "'," +
                    " loan_type='" + values.loan_type + "'," +
                    " source_type='" + values.source_type + "',";
            if (values.guideline_assessedon == null || values.guideline_assessedon == "")
            {
                msSQL += " guideline_value=null,";
            }
            else
            {
                msSQL += " guideline_value='" + values.guideline_value + "',";
            }
            if (values.guideline_assessedon == null || values.guideline_assessedon == "")
            {
                msSQL += " guideline_date=null,";
            }
            else
            {
                msSQL += " guideline_date='" + Convert.ToDateTime(values.guideline_assessedon).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            if (values.marketvalue_assessedon == null || values.marketvalue_assessedon == "")
            {
                msSQL += " marketvalue_date=null,";
            }
            else
            {
                msSQL += " marketvalue_date='" + Convert.ToDateTime(values.marketvalue_assessedon).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            if (values.market_value == null || values.market_value == "")
            {
                msSQL += " market_value='0.00',";
            }
            else
            {
                msSQL += " market_value='" + values.market_value.Replace(",", "") + "',";
            }
            if (values.forcedsource_value == null || values.forcedsource_value == "")
            {
                msSQL += " forcedsource_value='0.00',";
            }
            else
            {
                msSQL += " forcedsource_value='" + values.forcedsource_value.Replace(",", "") + "',";
            }
            if (values.collateralFSV_value == null || values.collateralFSV_value == "")
            {
                msSQL += " collateralSSV_value='0.00',";
            }
            else
            {
                msSQL += " collateralSSV_value='" + values.collateralFSV_value.Replace(",", "") + "',";
            }
            if (values.forcedvalue_assessedon == null || values.forcedvalue_assessedon == "")
            {
                msSQL += " forcedvalueassessed_on=null,";
            }
            else
            {
                msSQL += " forcedvalueassessed_on='" + Convert.ToDateTime(values.forcedvalue_assessedon).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            if (values.collateralobservation_summary == null)
            {
                msSQL += " collateralobservation_summary=null,";
            }
            else
            {
                msSQL += " collateralobservation_summary='" + values.collateralobservation_summary.Replace("'", " ") + "',";
            }

            msSQL += " updated_by='" + employee_gid + "'," +
                      " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                      " where application2loan_gid='" + values.application2loan_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msSQL = " update ocs_trn_tcaduploadcollateraldocument  set application2loan_gid='" + values.application2loan_gid + "' " +
                      "  where application2loan_gid='" + employee_gid + "' and generatelsa_gid='" + values.generatelsa_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                string msGetGIDPS = objcmnfunctions.GetMasterGID("LCDG");
                msSQL = " insert into ocs_trn_tlsacollateraldetailslog(" +
                          " lsacollateraldetails_gid," +
                          " application_gid," +
                          " generatelsa_gid," +
                          " application2loan_gid," +
                          " product_type," +
                          " producttype_gid," +
                          " product_subtype," +
                          " productsubtype_gid," +
                          " loantype_gid, " +
                          " loan_type, " +
                          " source_type," +
                          " guideline_value, " +
                          " guideline_assessedon," +
                          " marketvalue_assessedon," +
                          " market_value," +
                          " forcedsource_value," +
                          " collateralFSV_value," +
                          " forcedsource_assessedon," +
                          " collateralobservation_summary," +
                          " created_by," +
                          " created_date)" +
                          " values(" +
                          "'" + msGetGIDPS + "'," +
                           "'" + values.application_gid + "'," +
                           "'" + values.generatelsa_gid + "'," +
                           "'" + values.application2loan_gid + "'," +
                           "'" + values.product_type + "'," +
                           "'" + values.producttype_gid + "'," +
                           "'" + values.product_subtype + "'," +
                           "'" + values.productsubtype_gid + "'," +
                           "'" + values.loantype_gid + "'," +
                           "'" + values.loan_type + "'," +
                           "'" + values.source_type + "',";
                if (values.guideline_value == null || values.guideline_value == "")
                {
                    msSQL += "'0.00',";
                }
                else
                {
                    msSQL += "'" + values.guideline_value.Replace(",", "") + "',";
                }              
                if (values.guideline_assessedon == null || values.guideline_assessedon == "")
                {
                    msSQL += "null,";
                }
                else
                {
                    msSQL += "'" + Convert.ToDateTime(values.guideline_assessedon).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                }
                if (values.marketvalue_assessedon == null || values.marketvalue_assessedon == "")
                {
                    msSQL += "null,";
                }
                else
                {
                    msSQL += "'" + Convert.ToDateTime(values.marketvalue_assessedon).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                }
                if (values.market_value == null || values.market_value == "")
                {
                    msSQL += "'0.00',";
                }
                else
                {
                    msSQL += "'" + values.market_value.Replace(",", "") + "',";
                }
                if (values.forcedsource_value == null || values.forcedsource_value == "")
                {
                    msSQL += "'0.00',";
                }
                else
                {
                    msSQL += "'" + values.forcedsource_value.Replace(",", "") + "',";
                }
                if (values.collateralFSV_value == null || values.collateralFSV_value == "")
                {
                    msSQL += "'0.00',";
                }
                else
                {
                    msSQL += "'" + values.collateralFSV_value.Replace(",", "") + "',";
                }
                if (values.forcedvalue_assessedon == null || values.forcedvalue_assessedon == "")
                {
                    msSQL += "null,";
                }
                else
                {
                    msSQL += "'" + Convert.ToDateTime(values.forcedvalue_assessedon).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                }
                if (values.collateralobservation_summary == null)
                {
                    msSQL += "null,";
                }
                else
                {
                    msSQL += "'" + values.collateralobservation_summary.Replace("'", " ") + "',";
                }
                msSQL += "'" + employee_gid + "'," +
                         "current_timestamp)";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                values.status = true;
                values.message = "Collateral Details are Added Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
            }
        }

        public void DaGetCollateralTmpClear(string generatelsa_gid, string employee_gid)
        {
            msSQL = "delete from ocs_trn_tcaduploadcollateraldocument where generatelsa_gid='" + generatelsa_gid + "' and application2loan_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
        }

        public bool DapostLSAcollateraldocument(HttpRequest httpRequest, Documentname objfilename, string employee_gid)
        {
            DocumentList objdocumentmodel = new DocumentList();
            HttpFileCollection httpFileCollection;
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


            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";

            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/LSACollateralDoc/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
            {
                if ((!System.IO.Directory.Exists(path)))
                    System.IO.Directory.CreateDirectory(path);
            }

            string document_title = httpRequest.Form["document_title"].ToString();
            string generatelsa_gid = httpRequest.Form["generatelsa_gid"].ToString();
            string application2loan_gid = httpRequest.Form["application2loan_gid"].ToString();
            if (httpRequest.Files.Count > 0)
            {
                string lsfirstdocument_filepath = string.Empty;
                httpFileCollection = httpRequest.Files;
                for (int i = 0; i < httpFileCollection.Count; i++)
                {
                    string msdocument_gid = objcmnfunctions.GetMasterGID("UPLF");
                    httpPostedFile = httpFileCollection[i];
                    string FileExtension = httpPostedFile.FileName;
                    //string lsfile_gid = msdocument_gid + FileExtension;
                    string lsfile_gid = msdocument_gid;
                    FileExtension = Path.GetExtension(FileExtension).ToLower();
                    lsfile_gid = lsfile_gid + FileExtension;
                    if ((FileExtension == ".xls") || (FileExtension == ".xlsx") || (FileExtension == ".doc") || (FileExtension == ".docx") || (FileExtension == ".pdf") || (FileExtension == ".zip") || (FileExtension == ".msg") || (FileExtension == ".oft") || (FileExtension == ".txt") || (FileExtension == ".txtx"))
                    {
                        MemoryStream ms = new MemoryStream();
                        ls_readStream = httpPostedFile.InputStream;
                        ls_readStream.CopyTo(ms);
                        //lspath = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "Master/LSACollateralDoc/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        //objcmnfunctions.uploadFile(lspath, lsfile_gid);
                        //lspath = "../../erp_documents" + "/" + lscompany_code + "/" + "Master/LSACollateralDoc/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        // Check Document validation;

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.message = "File format is not supported";
                            return false;
                        }

                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Master/LSACollateralDoc/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "Master/LSACollateralDoc/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";


                        msGetGid = objcmnfunctions.GetMasterGID("CAMD");
                        msSQL = " insert into ocs_trn_tcaduploadcollateraldocument( " +
                                     " collateraldocument_gid," +
                                     " document_name, " +
                                     " document_title," +
                                     " document_path, " +
                                     " application2loan_gid," +
                                     " generatelsa_gid, " +
                                     " from_lsa," +
                                     " created_by ," +
                                     " created_date " +
                                     " )values(" +
                                     "'" + msGetGid + "'," +
                                     "'" + httpPostedFile.FileName.Replace("'", "") + "'," +
                                     "'" + document_title.Replace("'", "") + "'," +
                                     "'" + lspath + msdocument_gid + FileExtension.Replace("'", "") + "'," +
                                     "'" + employee_gid + "'," +
                                     "'" + generatelsa_gid + "'," +
                                     "'Y'," +
                                     "'" + employee_gid + "'," +
                                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        if (mnResult != 0)
                        {
                            msSQL = " select application2loan_gid,generatelsa_gid,collateraldocument_gid,document_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,document_path, " +
                               " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as uploaded_by,a.document_title,a.migration_flag " +
                               " from ocs_trn_tcaduploadcollateraldocument a,hrm_mst_temployee b, adm_mst_tuser c where a.created_by=b.employee_gid" +
                               " and b.user_gid = c.user_gid and (application2loan_gid='" + employee_gid + "' or application2loan_gid ='" + application2loan_gid + "') " +
                               " and generatelsa_gid='" + generatelsa_gid + "'";

                            dt_datatable = objdbconn.GetDataTable(msSQL);
                            var get_filename = new List<DocumentList>();
                            if (dt_datatable.Rows.Count != 0)
                            {
                                foreach (DataRow dr_datarow in dt_datatable.Rows)
                                {
                                    get_filename.Add(new DocumentList
                                    {
                                        document_path = objcmnstorage.EncryptData(dr_datarow["document_path"].ToString()),
                                        document_name = (dr_datarow["document_name"].ToString()),
                                        document_gid = (dr_datarow["collateraldocument_gid"].ToString()),
                                        uploaded_by = dr_datarow["uploaded_by"].ToString(),
                                        updated_date = dr_datarow["uploaded_date"].ToString(),
                                        document_type = dr_datarow["document_title"].ToString(),
                                        generatelsa_gid = dr_datarow["generatelsa_gid"].ToString(),
                                        application2loan_gid = dr_datarow["application2loan_gid"].ToString(),
                                        migration_flag = dr_datarow["migration_flag"].ToString()
                                    });
                                }
                                objfilename.DocumentList = get_filename;
                            }
                            dt_datatable.Dispose();

                            objfilename.status = true;
                            objfilename.message = "Collateral Document uploaded successfully";
                        }
                        else
                        {
                            objfilename.status = false;
                            objfilename.message = "Error Occured while uploading Collateral document";
                        }
                    }
                    else
                    {
                        objfilename.status = false;
                        objfilename.message = "File format is not supported";
                    }
                }
            }
            return true;
        }

        public void Dadeletelsacollateraldoc(string document_gid, Documentname values, string employee_gid)
        {
            msSQL = "delete from ocs_trn_tcaduploadcollateraldocument where collateraldocument_gid='" + document_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.message = "Document deleted successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured while deleting document";
                values.status = false;
            }
        }

        public void DaGetlsaProductname(string application_gid, LsaProductnamelist values)
        {
            try
            {
                msSQL = " SELECT application2loan_gid,concat(product_type,' / ',productsub_type) as product_type FROM ocs_mst_tapplication2loan a " +
                        " where application_gid = '" + application_gid + "' and loan_type != 'Secured'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getbankname_list = new List<LsaProductname>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getbankname_list.Add(new LsaProductname
                        {
                            application2loan_gid = (dr_datarow["application2loan_gid"].ToString()),
                            product_type = (dr_datarow["product_type"].ToString()),
                        });
                    }
                    values.LsaProductname = getbankname_list;
                }
                dt_datatable.Dispose();
            }
            catch
            {
            }
        }

        public void DaPostUpdateCollateralDetails(string employee_gid, lsacollateraldetails values)
        {
            msSQL = " update ocs_trn_tcadapplication2loan set " +
                     " product_type='" + values.product_type + "'," +
                     " producttype_gid='" + values.producttype_gid + "'," +
                     " productsub_type='" + values.product_subtype + "'," +
                     " productsubtype_gid='" + values.productsubtype_gid + "'," +
                     " loantype_gid='" + values.loantype_gid + "'," +
                     " loan_type='" + values.loan_type + "'," +
                     " source_type='" + values.source_type + "'," +
                     " guideline_value='" + values.guideline_value + "',";
            if (values.guideline_assessedon == null || values.guideline_assessedon == "")
            {
                msSQL += " guideline_date=null,";
            }
            else
            {
                msSQL += " guideline_date='" + Convert.ToDateTime(values.guideline_assessedon).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            if (values.marketvalue_assessedon == null || values.marketvalue_assessedon == "")
            {
                msSQL += " marketvalue_date=null,";
            }
            else
            {
                msSQL += " marketvalue_date='" + Convert.ToDateTime(values.marketvalue_assessedon).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            if (values.market_value == null || values.market_value == "")
            {
                msSQL += " market_value='0.00',";
            }
            else
            {
                msSQL += " market_value='" + values.market_value.Replace(",", "") + "',";
            }
            if (values.forcedsource_value == null || values.forcedsource_value == "")
            {
                msSQL += " forcedsource_value='0.00',";
            }
            else
            {
                msSQL += " forcedsource_value='" + values.forcedsource_value.Replace(",", "") + "',";
            }
            if (values.collateralFSV_value == null || values.collateralFSV_value == "")
            {
                msSQL += " collateralSSV_value='0.00',";
            }
            else
            {
                msSQL += " collateralSSV_value='" + values.collateralFSV_value.Replace(",", "") + "',";
            }
            if (values.forcedvalue_assessedon == null || values.forcedvalue_assessedon == "")
            {
                msSQL += " forcedvalueassessed_on=null,";
            }
            else
            {
                msSQL += " forcedvalueassessed_on='" + Convert.ToDateTime(values.forcedvalue_assessedon).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            if (values.collateralobservation_summary == null)
            {
                msSQL += " collateralobservation_summary=null,";
            }
            else
            {
                msSQL += " collateralobservation_summary='" + values.collateralobservation_summary.Replace("'", " ") + "',";
            }

            msSQL += " updated_by='" + employee_gid + "'," +
                      " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                      " where application2loan_gid='" + values.application2loan_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msSQL = " update ocs_trn_tcaduploadcollateraldocument  set application2loan_gid='" + values.application2loan_gid + "' " +
                        " generatelsa_gid='" + values.generatelsa_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                values.status = true;
                values.message = "Collateral Details are updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
            }
        }


        public void DaGetlsaCollateralDetail(string application2loan_gid, lsacollateraldetails values)
        {
            msSQL = " select application2loan_gid,product_type,producttype_gid,productsub_type,productsubtype_gid,loantype_gid,loan_type, " +
                    " source_type, guideline_value, date_format(guideline_date, '%d-%m-%Y') as guideline_assessedon,guideline_date, " +
                    " date_format(marketvalue_date, '%d-%m-%Y') as marketvalue_date, market_value, forcedsource_value,marketvalue_date as marketvaluedate," +
                    " collateralSSV_value, date_format(forcedvalueassessed_on, '%d-%m-%Y') as forcedvalueassessed_on,forcedvalueassessed_on as forcedvalueassessedDate, " +
                    " collateralobservation_summary,product_type,productsub_type from ocs_trn_tcadapplication2loan  " +
                    " where application2loan_gid='" + application2loan_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                values.product_type = objODBCDataReader["product_type"].ToString();
                values.producttype_gid = objODBCDataReader["producttype_gid"].ToString();
                values.product_subtype = objODBCDataReader["productsub_type"].ToString();
                values.productsubtype_gid = objODBCDataReader["productsubtype_gid"].ToString();
                values.loantype_gid = objODBCDataReader["loantype_gid"].ToString();
                values.loan_type = objODBCDataReader["loan_type"].ToString();
                values.source_type = objODBCDataReader["source_type"].ToString();
                values.guideline_value = objODBCDataReader["guideline_value"].ToString();
                values.guideline_assessedon = objODBCDataReader["guideline_assessedon"].ToString();
                values.marketvalue_assessedon = objODBCDataReader["marketvalue_date"].ToString();
                values.market_value = objODBCDataReader["market_value"].ToString();
                values.forcedsource_value = objODBCDataReader["forcedsource_value"].ToString();
                values.collateralFSV_value = objODBCDataReader["collateralSSV_value"].ToString();
                values.forcedvalue_assessedon = objODBCDataReader["forcedvalueassessed_on"].ToString();
                values.collateralobservation_summary = objODBCDataReader["collateralobservation_summary"].ToString();
                values.application2loan_gid = objODBCDataReader["application2loan_gid"].ToString();
                if (objODBCDataReader["marketvaluedate"].ToString() != "")
                {
                    values.marketvalue_date = Convert.ToDateTime(objODBCDataReader["marketvaluedate"].ToString());
                }
                if (objODBCDataReader["guideline_date"].ToString() != "")
                {
                    values.guideline_date = Convert.ToDateTime(objODBCDataReader["guideline_date"].ToString());
                }
                if (objODBCDataReader["forcedvalueassessedDate"].ToString() != "")
                {
                    values.forcedvalue_date = Convert.ToDateTime(objODBCDataReader["forcedvalueassessedDate"].ToString());
                }
            }
            objODBCDataReader.Close();
        }

        public void DaGetLSACollateraldocument(UploadLSADocumentname values, string application2loan_gid)
        {
            msSQL = " select application2loan_gid,generatelsa_gid,collateraldocument_gid,document_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,document_path, " +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as uploaded_by,a.document_title, a.migration_flag " +
                    " from ocs_trn_tcaduploadcollateraldocument a,hrm_mst_temployee b, adm_mst_tuser c where a.created_by=b.employee_gid" +
                    " and b.user_gid = c.user_gid and application2loan_gid='" + application2loan_gid + "'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_filename = new List<UploadLSADocumentList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    get_filename.Add(new UploadLSADocumentList
                    {
                        document_path = objcmnstorage.EncryptData(dr_datarow["document_path"].ToString()),
                        document_name = (dr_datarow["document_name"].ToString()),
                        document_gid = (dr_datarow["collateraldocument_gid"].ToString()),
                        uploaded_by = dr_datarow["uploaded_by"].ToString(),
                        created_date = dr_datarow["uploaded_date"].ToString(),
                        document_type = dr_datarow["document_title"].ToString(),
                        generatelsa_gid = dr_datarow["generatelsa_gid"].ToString(),
                        application2loan_gid = dr_datarow["application2loan_gid"].ToString(),
                        migration_flag = dr_datarow["migration_flag"].ToString()

                    });
                }
                values.UploadLSADocumentList = get_filename;
            }
            dt_datatable.Dispose();
        }


        public bool DaPostlsaGeneralUploaddocument(HttpRequest httpRequest, UploadLSADocumentname objfilename, string employee_gid, string user_gid)
        {
            UploadDocumentList objdocumentmodel = new UploadDocumentList();
            HttpFileCollection httpFileCollection;

            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;

            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string pdfFilName = string.Empty;
            Stream ls_readStream;
            string lsdocumenttype_gid = string.Empty;
            string lsdocument_type = string.Empty;
            string project_flag = httpRequest.Form["project_flag"].ToString();
            String path = lspath;

            string document_type = httpRequest.Form["document_type"].ToString();
            string generatelsa_gid = httpRequest.Form["generatelsa_gid"].ToString();

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";

            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/LSAGeneralDocuments/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
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
                        MemoryStream ms = new MemoryStream();
                        ls_readStream = httpPostedFile.InputStream;
                        ls_readStream.CopyTo(ms);

                        // Check Document validation;

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.message = "File format is not supported";
                            return false;
                        }

                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Master/LSAGeneralDocuments/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "Master/LSAGeneralDocuments/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        //lspath = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "Master/LSAGeneralDocuments/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        //objcmnfunctions.uploadFile(lspath, lsfile_gid);
                        if (document_type == "undefined")
                            lsdocument_type = "";
                        else
                            lsdocument_type = document_type;

                        //lspath = "../../erp_documents" + "/" + lscompany_code + "/" + "Master/LSAGeneralDocuments/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "Master/LSAGeneralDocuments/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        msGetGid = objcmnfunctions.GetMasterGID("LUDG");
                        msSQL = " insert into ocs_trn_tlsauploaddocument( " +
                                    " lsauploaddocument_gid," +
                                    " generatelsa_gid ," +
                                    " document_name," +
                                    " document_path," +
                                    " document_type ," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + generatelsa_gid + "'," +
                                    "'" + httpPostedFile.FileName.Replace("'", "") + "'," +
                                    "'" + lspath + msdocument_gid + FileExtension.Replace("'", "") + "'," +
                                    "'" + lsdocument_type.Replace("'", "") + "'," +
                                    "'" + user_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                msSQL = " select lsauploaddocument_gid,document_name,date_format(a.created_date,'%d-%m-%Y %H:%i %p') as created_date, " +
                        " document_path,document_type, " +
                        " concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as uploaded_by" +
                        " from ocs_trn_tlsauploaddocument a, adm_mst_tuser b where a.created_by=b.user_gid and generatelsa_gid='" + generatelsa_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_filename = new List<UploadLSADocumentList>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_filename.Add(new UploadLSADocumentList
                        {
                            document_path = objcmnstorage.EncryptData(dr_datarow["document_path"].ToString()),
                            document_name = (dr_datarow["document_name"].ToString()),
                            document_gid = (dr_datarow["lsauploaddocument_gid"].ToString()),
                            document_type = dr_datarow["document_type"].ToString(),
                            uploaded_by = dr_datarow["uploaded_by"].ToString(),
                            created_date = dr_datarow["created_date"].ToString()
                        });
                    }
                    objfilename.UploadLSADocumentList = get_filename;
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                objfilename.status = false;
                objfilename.message = ex.ToString();
            }
            if (mnResult == 1)
            {
                objfilename.status = true;
                objfilename.message = "Document Uploaded Successfully";
                return true;
            }
            else
            {
                objfilename.status = false;
                objfilename.message = "Error Occured while uploading document";
                return false;
            }
        }

        public void DaGetLSAGeneraldocument(UploadLSADocumentname values, string generatelsa_gid)
        {
            msSQL = " select lsauploaddocument_gid,document_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as created_date,document_path,document_type, " +
                    " concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as uploaded_by" +
                    " from ocs_trn_tlsauploaddocument a, adm_mst_tuser b where a.created_by=b.user_gid and generatelsa_gid='" + generatelsa_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_filename = new List<UploadLSADocumentList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    get_filename.Add(new UploadLSADocumentList
                    {
                        document_name = (dr_datarow["document_name"].ToString()),
                        document_path = objcmnstorage.EncryptData((dr_datarow["document_path"].ToString())),
                        document_gid = (dr_datarow["lsauploaddocument_gid"].ToString()),
                        document_type = dr_datarow["document_type"].ToString(),
                        uploaded_by = dr_datarow["uploaded_by"].ToString(),
                        created_date = dr_datarow["created_date"].ToString()
                    });
                }
                values.UploadLSADocumentList = get_filename;
            }
            dt_datatable.Dispose();


        }

        public bool DaGetDeleteLSAuploadeddocument(result values, string lsauploaddocument_gid)
        {
            msSQL = "delete from ocs_trn_tlsauploaddocument where lsauploaddocument_gid='" + lsauploaddocument_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.message = "Document Deleted Successfully";
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

        public bool DaGetcompliancecheckinfo(lsacompliancecheck values, string generatelsa_gid)
        {
            msSQL = "select lsacompliancecheckdetail_gid,generatelsa_gid,nachmandateform_held,nachmandateform_heldremarks,signmatching_nachform, " +
                    " signmatching_nachformremarks, namesign_kycmatching,namesign_kycmatchingremarks,escrowaccount_opened,escrowaccount_openedremarks, " +
                    " appropriate_stamping,appropriate_stampingremarks,rocfiling_initiated,rocfiling_initiatedremarks,cersai_initiated,cersai_initiatedremarks, " +
                    " created_by, created_date,alldeferralcovenant_captured,allpredisbursement_stipulated,maker_signaturename from ocs_trn_tlsacompliancecheckdetail " +
                     " where generatelsa_gid='" + generatelsa_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                values.lsacompliancecheckdetail_gid = objODBCDataReader["lsacompliancecheckdetail_gid"].ToString();
                values.generatelsa_gid = objODBCDataReader["generatelsa_gid"].ToString();
                values.nachmandateform_held = objODBCDataReader["nachmandateform_held"].ToString();
                values.nachmandateform_heldremarks = objODBCDataReader["nachmandateform_heldremarks"].ToString();
                values.signmatching_nachform = objODBCDataReader["signmatching_nachform"].ToString();
                values.signmatching_nachformremarks = objODBCDataReader["signmatching_nachformremarks"].ToString();
                values.namesign_kycmatching = objODBCDataReader["namesign_kycmatching"].ToString();
                values.namesign_kycmatchingremarks = objODBCDataReader["namesign_kycmatchingremarks"].ToString();
                values.escrowaccount_opened = objODBCDataReader["escrowaccount_opened"].ToString();
                values.escrowaccount_openedremarks = objODBCDataReader["escrowaccount_openedremarks"].ToString();
                values.appropriate_stamping = objODBCDataReader["appropriate_stamping"].ToString();
                values.appropriate_stampingremarks = objODBCDataReader["appropriate_stampingremarks"].ToString();
                values.rocfiling_initiated = objODBCDataReader["rocfiling_initiated"].ToString();
                values.rocfiling_initiatedremarks = objODBCDataReader["rocfiling_initiatedremarks"].ToString();
                values.cersai_initiated = objODBCDataReader["cersai_initiated"].ToString();
                values.cersai_initiatedremarks = objODBCDataReader["cersai_initiatedremarks"].ToString();
                values.alldeferralcovenant_captured = objODBCDataReader["alldeferralcovenant_captured"].ToString();
                values.allpredisbursement_stipulated = objODBCDataReader["allpredisbursement_stipulated"].ToString();
                values.maker_signaturename = objODBCDataReader["maker_signaturename"].ToString();
                values.created_by = objODBCDataReader["created_by"].ToString();
                values.created_date = objODBCDataReader["created_date"].ToString();
                values.status = true;
            }
            else
            {
                values.status = false;
            }

            objODBCDataReader.Close();


            return true;
        }

        public bool DaPostProceedtoChecker(string user_gid, lsacompliancecheck values)
        {
            if (values.nachmandateform_heldremarks == null)
                values.nachmandateform_heldremarks = "";
            if (values.signmatching_nachformremarks == null)
                values.signmatching_nachformremarks = "";
            if (values.namesign_kycmatchingremarks == null)
                values.namesign_kycmatchingremarks = "";
            if (values.escrowaccount_openedremarks == null)
                values.escrowaccount_openedremarks = "";
            if (values.appropriate_stampingremarks == null)
                values.appropriate_stampingremarks = "";
            if (values.rocfiling_initiatedremarks == null)
                values.rocfiling_initiatedremarks = "";
            if (values.cersai_initiatedremarks == null)
                values.cersai_initiatedremarks = "";
            msGetGid = objcmnfunctions.GetMasterGID("LCCG");

            msSQL = " insert into ocs_trn_tlsacompliancecheckdetail(" +
                " lsacompliancecheckdetail_gid," +
                " generatelsa_gid," +
                " nachmandateform_held, " +
                " nachmandateform_heldremarks, " +
                " signmatching_nachform, " +
                " signmatching_nachformremarks," +
                " namesign_kycmatching," +
                " namesign_kycmatchingremarks," +
                " escrowaccount_opened," +
                " escrowaccount_openedremarks," +
                " appropriate_stamping," +
                " appropriate_stampingremarks," +
                " rocfiling_initiated," +
                " rocfiling_initiatedremarks," +
                " cersai_initiated," +
                " cersai_initiatedremarks," +
                " alldeferralcovenant_captured, " +
                " allpredisbursement_stipulated, " +
                " maker_signaturename, " +
                " created_by," +
                " created_date)" +
                " values(" +
                "'" + msGetGid + "'," +
                "'" + values.generatelsa_gid + "'," +
                "'" + values.nachmandateform_held + "'," +
                "'" + values.nachmandateform_heldremarks.Replace("'", "") + "'," +
                "'" + values.signmatching_nachform + "'," +
                "'" + values.signmatching_nachformremarks.Replace("'", "") + "'," +
                "'" + values.namesign_kycmatching + "'," +
                "'" + values.namesign_kycmatchingremarks.Replace(",", "") + "'," +
                "'" + values.escrowaccount_opened + "'," +
                "'" + values.escrowaccount_openedremarks.Replace("'", "") + "'," +
                "'" + values.appropriate_stamping + "'," +
                "'" + values.appropriate_stampingremarks.Replace(",", "") + "'," +
                "'" + values.rocfiling_initiated + "'," +
                "'" + values.rocfiling_initiatedremarks.Replace("'", "") + "'," +
                "'" + values.cersai_initiated + "'," +
                "'" + values.cersai_initiatedremarks.Replace("'", "") + "'," +
                "'" + values.alldeferralcovenant_captured + "'," +
                "'" + values.allpredisbursement_stipulated + "'," +
                "'" + values.maker_signaturename + "'," +
                "'" + user_gid + "'," +
                "current_timestamp)";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msSQL = " select  processtypeassign_gid from ocs_trn_tprocesstype_assign a " +
                        " left join ocs_trn_tgeneratelsa b on a.application_gid = b.application_gid " +
                        " where generatelsa_gid = '" + values.generatelsa_gid + "' and a.menu_gid ='" + getMenuClass.LSA + "' and maker_approvalflag='N' ";
                string lsprocesstypeassign_gid = objdbconn.GetExecuteScalar(msSQL);

                if (lsprocesstypeassign_gid != "")
                {
                    msSQL = " update ocs_trn_tprocesstype_assign set maker_approvalflag='Y', " +
                            " maker_approveddate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                            " overall_approvalstatus='Proceed to Checker'  where processtypeassign_gid='" + lsprocesstypeassign_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                msSQL = "update ocs_trn_tgeneratelsa set overall_lsastatus='Checker Approval Pending' where generatelsa_gid = '" + values.generatelsa_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            }

            if (mnResult != 0)
            {
                values.message = "Proceeded to Checker Successfully";
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

        public bool DaPostProceedtoApprover(string user_gid, lsacompliancecheck values)
        {
            if (values.nachmandateform_heldremarks == null)
                values.nachmandateform_heldremarks = "";
            if (values.signmatching_nachformremarks == null)
                values.signmatching_nachformremarks = "";
            if (values.namesign_kycmatchingremarks == null)
                values.namesign_kycmatchingremarks = "";
            if (values.escrowaccount_openedremarks == null)
                values.escrowaccount_openedremarks = "";
            if (values.appropriate_stampingremarks == null)
                values.appropriate_stampingremarks = "";
            if (values.rocfiling_initiatedremarks == null)
                values.rocfiling_initiatedremarks = "";
            if (values.cersai_initiatedremarks == null)
                values.cersai_initiatedremarks = "";

            msSQL = " update ocs_trn_tlsacompliancecheckdetail set " +
                " nachmandateform_held ='" + values.nachmandateform_held + "', " +
                " nachmandateform_heldremarks  ='" + values.nachmandateform_heldremarks.Replace("'", "") + "', " +
                " signmatching_nachform ='" + values.signmatching_nachform + "', " +
                " signmatching_nachformremarks  ='" + values.signmatching_nachformremarks.Replace("'", "") + "', " +
                " namesign_kycmatching ='" + values.namesign_kycmatching + "', " +
                " namesign_kycmatchingremarks  ='" + values.namesign_kycmatchingremarks.Replace(",", "") + "', " +
                " escrowaccount_opened ='" + values.escrowaccount_opened + "', " +
                " escrowaccount_openedremarks ='" + values.escrowaccount_openedremarks.Replace("'", "") + "', " +
                " appropriate_stamping ='" + values.appropriate_stamping + "', " +
                " appropriate_stampingremarks ='" + values.appropriate_stampingremarks.Replace(",", "") + "', " +
                " rocfiling_initiated ='" + values.rocfiling_initiated + "', " +
                " rocfiling_initiatedremarks ='" + values.rocfiling_initiatedremarks.Replace("'", "") + "', " +
                " cersai_initiated ='" + values.cersai_initiated + "', " +
                " cersai_initiatedremarks ='" + values.cersai_initiatedremarks.Replace("'", "") + "', " +
                " alldeferralcovenant_captured ='" + values.alldeferralcovenant_captured + "', " +
                " allpredisbursement_stipulated ='" + values.allpredisbursement_stipulated + "', " +
                " maker_signaturename ='" + values.maker_signaturename + "', " +
                " updated_by ='" + user_gid + "', " +
                " updated_date=current_timestamp where lsacompliancecheckdetail_gid='" + values.lsacompliancecheckdetail_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msSQL = " select  processtypeassign_gid from ocs_trn_tprocesstype_assign a " +
                        " left join ocs_trn_tgeneratelsa b on a.application_gid = b.application_gid " +
                        " where generatelsa_gid = '" + values.generatelsa_gid + "' and a.menu_gid ='" + getMenuClass.LSA + "' order by a.created_date desc limit 1";
                string lsprocesstypeassign_gid = objdbconn.GetExecuteScalar(msSQL);

                if (lsprocesstypeassign_gid != "")
                {
                    msSQL = " update ocs_trn_tprocesstype_assign set checker_approvalflag='Y', " +
                            " checker_approveddate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                            " overall_approvalstatus='Proceed To Approval'  where processtypeassign_gid='" + lsprocesstypeassign_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                msSQL = "update ocs_trn_tgeneratelsa set overall_lsastatus='Final Approval Pending' where generatelsa_gid = '" + values.generatelsa_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            if (mnResult != 0)
            {
                values.message = "Proceeded to Approver Successfully";
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

        public bool DaPostLSAApproved(string user_gid, string generatelsa_gid, result values)
        {

            msSQL = " select  processtypeassign_gid from ocs_trn_tprocesstype_assign a " +
                       " left join ocs_trn_tgeneratelsa b on a.application_gid = b.application_gid " +
                       " where generatelsa_gid = '" + generatelsa_gid + "' and a.menu_gid ='" + getMenuClass.LSA + "' order by a.created_date desc limit 1 ";
            string lsprocesstypeassign_gid = objdbconn.GetExecuteScalar(msSQL);

            if (lsprocesstypeassign_gid != "")
            {
                msSQL = " update ocs_trn_tprocesstype_assign set approver_approvalflag='Y', " +
                     " approver_approveddate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                     " overall_approvalstatus='Approved'  where processtypeassign_gid='" + lsprocesstypeassign_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            string msGETLSAGID = objcmnfunctions.GetMasterGID("CLSA");
            msGETLSAGID = Regex.Replace(msGETLSAGID, "[^0-9.]", "");

            msSQL = " select b.vertical_refno from ocs_mst_tapplication a " +
                   " left join ocs_mst_tvertical b on a.vertical_gid = b.vertical_gid " +
                   " where a.application_gid in (select application_gid from ocs_trn_tgeneratelsa where generatelsa_gid = '" + generatelsa_gid + "')";
            string vertical_refno = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select count(*) from ocs_trn_tgeneratelsa where month(lsa_approveddate)=month(curdate()) and year(lsa_approveddate)=year(curdate())";
            string lsmonthwise = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select count(*) from ocs_trn_tgeneratelsa where day(lsa_approveddate)=day(curdate()) and month(lsa_approveddate)=month(curdate()) and year(lsa_approveddate)=year(curdate())";
            string lsdaywise = objdbconn.GetExecuteScalar(msSQL);

            int lsmonthcount = Int32.Parse(lsmonthwise) + 1;
            int lsdaycount = Int16.Parse(lsdaywise) + 1;
            int digits = lsmonthcount.ToString().Length;
            string lsmonth_count = lsmonthcount.ToString();
            string lsday_count = lsdaycount.ToString();
            if (digits < 3)
            {
                digits = 3 - digits;
                lsmonth_count = digits.ToString().PadLeft((digits + 1), '0');
            }
            int daydigits = lsdaycount.ToString().Length;
            if (daydigits < 3)
            {
                daydigits = 3 - daydigits;
                lsday_count = daydigits.ToString().PadLeft((daydigits + 1), '0');
            }

            string lssequencecode = "LSA" + vertical_refno + msGETLSAGID + lsmonth_count + lsday_count;

            msSQL = " update ocs_trn_tgeneratelsa set overall_lsastatus='Approved',lsa_refno='" + lssequencecode + "'," +
                    " lsa_approveddate ='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
                    " where generatelsa_gid = '" + generatelsa_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                msSQL = " select limitproductinfodtl_gid,documented_limit,lsatotalreleased_amount,lsatotalreleased_approved " +
                       " from ocs_trn_tlimitproductinfo where generatelsa_gid='" + generatelsa_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        lslimitproductinfodtl_gid = (dr_datarow["limitproductinfodtl_gid"].ToString());
                        dbldocumented_limit = Convert.ToDouble(dr_datarow["documented_limit"].ToString());
                        dbllsatotalreleased_amount = Convert.ToDouble(dr_datarow["lsatotalreleased_amount"].ToString());
                        dbllsatotalreleased_approved = Convert.ToDouble(dr_datarow["lsatotalreleased_approved"].ToString());

                        dbllsatotalreleased_approved = dbllsatotalreleased_amount + dbllsatotalreleased_approved;

                        if (!(dbllsatotalreleased_approved.Equals(dbldocumented_limit)))
                        {
                            lsprodreinitiate_flag = "Y";
                        }
                        else
                        {
                            lsprodreinitiate_flag = "N";
                        }

                        msSQL = " update ocs_trn_tlimitproductinfo set lsatotalreleased_approved='" + dbllsatotalreleased_approved + "'," +
                                " lsprodreinitiate_flag='" + lsprodreinitiate_flag + "'" +
                                " where limitproductinfodtl_gid = '" + lslimitproductinfodtl_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        
                    }
                }
                dt_datatable.Dispose();

                //Reinitiate

                msSQL = "select lsprodreinitiate_flag from ocs_trn_tlimitproductinfo where generatelsa_gid='" + generatelsa_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);

                List<string> lsprodreinitiateflag_list = new List<string>();

                lsprodreinitiateflag_list = dt_datatable.AsEnumerable().Select(p => p.Field<string>("lsprodreinitiate_flag")).ToList();

                if (lsprodreinitiateflag_list.Contains("Y") == true)
                {
                    lsatotdoclimit_flag = "Y";
                    lsreinitiate_eligibleflag = "Y";
                }
                else
                {
                    lsatotdoclimit_flag = "N";
                    lsreinitiate_eligibleflag = "N";
                }

                msSQL = " update ocs_trn_tgeneratelsa set " +
                        " reinitiate_eligibleflag ='" + lsreinitiate_eligibleflag + "'" +
                        " where generatelsa_gid ='" + generatelsa_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                msSQL = " select application2sanction_gid from ocs_trn_tgeneratelsa " +                       
                        " where generatelsa_gid = '" + generatelsa_gid + "'";
                string application2sanction_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " update ocs_trn_tapplication2sanction set " +
                        " lsatotdoclimit_flag ='" + lsatotdoclimit_flag + "'" +
                        " where application2sanction_gid = '" + application2sanction_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                //Reinitiate

                values.message = "Final Approval Approved Successfully";
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

        public void DaGetBranchdtl(branchlistdtl objbranch)
        {
            try
            {
                msSQL = " select branch_gid,branch_name from hrm_mst_tbranch ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getbranch = new List<branch_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getbranch.Add(new branch_list
                        {
                            branch_gid = (dr_datarow["branch_gid"].ToString()),
                            branch_name = (dr_datarow["branch_name"].ToString()),
                        });
                    }
                    objbranch.branch_list = getbranch;
                }
                dt_datatable.Dispose();
                objbranch.status = true;
            }
            catch
            {
                objbranch.status = false;
            }

        }

        public void DaPostServiceCharges(string employee_gid, MdlProductCharges values)
        {
            string producttypegid = string.Empty;
            string producttype = string.Empty;
            if (values.producttypelist != null)
            {
                for (var i = 0; i < values.producttypelist.Count; i++)
                {
                    producttypegid += values.producttypelist[i].producttype_gid + ",";
                    producttype += values.producttypelist[i].product_type + ",";

                }
                producttypegid = producttypegid.TrimEnd(',');
                producttype = producttype.TrimEnd(',');
            }
            string msgetfeechargeGid = objcmnfunctions.GetMasterGID("LFCG");
            msSQL = " insert into ocs_trn_tlsafeescharge(" +
                         " lsafeescharge_gid, " +
                         " application_gid," +
                         " generatelsa_gid, " +
                         " processing_fee," +
                         " processing_collectiontype," +
                         " doc_charges," +
                         " doccharge_collectiontype," +
                         " fieldvisit_charges," +
                         " fieldvisit_charges_collectiontype," +
                         " adhoc_fee," +
                         " adhoc_collectiontype," +
                         " life_insurance," +
                         " lifeinsurance_collectiontype," +
                         " acct_insurance," +
                         " acctinsurance_collectiontype," +
                         " total_collect," +
                         " total_deduct," +
                         " product_type," +
                         " created_by," +
                         " created_date) values(" +
                         "'" + msgetfeechargeGid + "'," +
                         "'" + values.application_gid + "'," +
                         "'" + values.generatelsa_gid + "'," +
                         "'" + values.processing_fee + "'," +
                         "'" + values.processing_collectiontype + "'," +
                         "'" + values.doc_charges + "'," +
                         "'" + values.doccharge_collectiontype + "'," +
                         "'" + values.fieldvisit_charge + "'," +
                         "'" + values.fieldvisit_collectiontype + "'," +
                         "'" + values.adhoc_fee + "'," +
                         "'" + values.adhoc_collectiontype + "'," +
                         "'" + values.life_insurance + "'," +
                         "'" + values.lifeinsurance_collectiontype + "'," +
                         "'" + values.acct_insurance + "'," +
                         "'" + values.acctinsurance_collectiontype + "'," +
                         "'" + values.total_collect + "'," +
                         "'" + values.total_deduct + "'," +
                         "'" + producttype + "'," +
                         "'" + employee_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Service Details are added Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
            }
        }

        public void DaGetBankAccountTempClear(string employee_gid, result values)
        {
            msSQL = "delete from ocs_trn_tlsachequeleafdocument where lsabankaccdtl_gid='" + employee_gid + "' or lsabankaccdtl_gid= ''";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            values.status = true;
        }
        public bool DapostLSAcollateraldocumentAdd(HttpRequest httpRequest, Documentname objfilename, string employee_gid)
        {
            DocumentList objdocumentmodel = new DocumentList();
            HttpFileCollection httpFileCollection;
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


            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";

            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/LSACollateralDoc/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
            {
                if ((!System.IO.Directory.Exists(path)))
                    System.IO.Directory.CreateDirectory(path);
            }

            string document_title = httpRequest.Form["document_title"].ToString();
            string generatelsa_gid = httpRequest.Form["generatelsa_gid"].ToString();
            string application2loan_gid = httpRequest.Form["application2loan_gid"].ToString();
            if (httpRequest.Files.Count > 0)
            {
                string lsfirstdocument_filepath = string.Empty;
                httpFileCollection = httpRequest.Files;
                for (int i = 0; i < httpFileCollection.Count; i++)
                {
                    string msdocument_gid = objcmnfunctions.GetMasterGID("UPLF");
                    httpPostedFile = httpFileCollection[i];
                    string FileExtension = httpPostedFile.FileName;
                    //string lsfile_gid = msdocument_gid + FileExtension;
                    string lsfile_gid = msdocument_gid;
                    FileExtension = Path.GetExtension(FileExtension).ToLower();
                    lsfile_gid = lsfile_gid + FileExtension;                    
                        MemoryStream ms = new MemoryStream();
                        ls_readStream = httpPostedFile.InputStream;
                        ls_readStream.CopyTo(ms);
                        //lspath = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "Master/LSACollateralDoc/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        //objcmnfunctions.uploadFile(lspath, lsfile_gid);
                        //lspath = "../../erp_documents" + "/" + lscompany_code + "/" + "Master/LSACollateralDoc/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        // Check Document validation;

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.message = "File format is not supported";
                            return false;
                        }

                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Master/LSACollateralDoc/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "Master/LSACollateralDoc/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";


                        msGetGid = objcmnfunctions.GetMasterGID("CAMD");
                        msSQL = " insert into ocs_trn_tcaduploadcollateraldocument( " +
                                     " collateraldocument_gid," +
                                     " document_name, " +
                                     " document_title," +
                                     " document_path, " +
                                     " application2loan_gid," +
                                     " generatelsa_gid, " +
                                     " from_lsa," +
                                     " created_by ," +
                                     " created_date " +
                                     " )values(" +
                                     "'" + msGetGid + "'," +
                                     "'" + httpPostedFile.FileName.Replace("'", "") + "'," +
                                     "'" + document_title.Replace("'", "") + "'," +
                                     "'" + lspath + msdocument_gid + FileExtension.Replace("'", "") + "'," +
                                     "'" + employee_gid + "'," +
                                     "'" + generatelsa_gid + "'," +
                                     "'Y'," +
                                     "'" + employee_gid + "'," +
                                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        if (mnResult != 0)
                        {
                            msSQL = " select collateraldocument_gid,document_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,document_path, " +
                               " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as uploaded_by,a.document_title, a.migration_flag " +
                               " from ocs_trn_tcaduploadcollateraldocument a,hrm_mst_temployee b, adm_mst_tuser c where a.created_by=b.employee_gid" +
                               " and b.user_gid = c.user_gid and (application2loan_gid='" + employee_gid + "') " +
                               " and generatelsa_gid='" + generatelsa_gid + "'";

                            dt_datatable = objdbconn.GetDataTable(msSQL);
                            var get_filename = new List<DocumentList>();
                            if (dt_datatable.Rows.Count != 0)
                            {
                                foreach (DataRow dr_datarow in dt_datatable.Rows)
                                {
                                    get_filename.Add(new DocumentList
                                    {
                                        document_path = objcmnstorage.EncryptData(dr_datarow["document_path"].ToString()),
                                        document_name = (dr_datarow["document_name"].ToString()),
                                        document_gid = (dr_datarow["collateraldocument_gid"].ToString()),
                                        uploaded_by = dr_datarow["uploaded_by"].ToString(),
                                        updated_date = dr_datarow["uploaded_date"].ToString(),
                                        document_type = dr_datarow["document_title"].ToString(),
                                        migration_flag = dr_datarow["migration_flag"].ToString()
                                    });
                                }
                                objfilename.DocumentList = get_filename;
                            }
                            dt_datatable.Dispose();

                            objfilename.status = true;
                            objfilename.message = "Collateral Document uploaded successfully";
                        }
                        else
                        {
                            objfilename.status = false;
                            objfilename.message = "Error Occured while uploading Collateral document";
                        }                    
                }
            }
            return true;
        }

        public void DaGetLSAChequeLeafTmpdocedit(string lsabankaccdtl_gid, string employee_gid, lsauploaddocument values)
        {
            msSQL = " select lsachequeleafdocument_gid,lsabankaccdtl_gid,document_name,document_path,document_title from " +
                                " ocs_trn_tlsachequeleafdocument where (lsabankaccdtl_gid='" + employee_gid + "' or lsabankaccdtl_gid='" + lsabankaccdtl_gid + "')";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdocumentdtlList = new List<lsauploaddocument_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getdocumentdtlList.Add(new lsauploaddocument_list
                    {
                        document_name = dt["document_name"].ToString(),
                        document_title = dt["document_title"].ToString(),
                        document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                        lsachequeleafdocument_gid = dt["lsachequeleafdocument_gid"].ToString(),
                        lsabankaccdtl_gid = dt["lsabankaccdtl_gid"].ToString(),

                    });
                    values.lsauploaddocument_list = getdocumentdtlList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetLSACollateraldocumentEdit(UploadLSADocumentname values, string generatelsa_gid, string application2loan_gid, string employee_gid)
        {
            //msSQL = " select collateraldocument_gid,document_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,document_path, " +
            //        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as uploaded_by,a.document_title " +
            //        " from ocs_trn_tcaduploadcollateraldocument a,hrm_mst_temployee b, adm_mst_tuser c where a.created_by=b.employee_gid" +
            //        " and b.user_gid = c.user_gid and application2loan_gid='" + application2loan_gid + "'";


            msSQL = " select application2loan_gid,generatelsa_gid,collateraldocument_gid,document_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,document_path, " +
                                " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as uploaded_by,a.document_title " +
                                " from ocs_trn_tcaduploadcollateraldocument a,hrm_mst_temployee b, adm_mst_tuser c where a.created_by=b.employee_gid" +
                                " and b.user_gid = c.user_gid and (application2loan_gid='" + employee_gid + "' or application2loan_gid ='" + application2loan_gid + "') " +
                                " and generatelsa_gid='" + generatelsa_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_filename = new List<UploadLSADocumentList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    get_filename.Add(new UploadLSADocumentList
                    {
                        document_path = objcmnstorage.EncryptData(dr_datarow["document_path"].ToString()),
                        document_name = (dr_datarow["document_name"].ToString()),
                        document_gid = (dr_datarow["collateraldocument_gid"].ToString()),
                        uploaded_by = dr_datarow["uploaded_by"].ToString(),
                        created_date = dr_datarow["uploaded_date"].ToString(),
                        document_type = dr_datarow["document_title"].ToString(),
                        generatelsa_gid = dr_datarow["generatelsa_gid"].ToString(),
                        application2loan_gid = dr_datarow["application2loan_gid"].ToString()
                    });
                }
                values.UploadLSADocumentList = get_filename;
            }
            dt_datatable.Dispose();
        }
        public void DaSubmitLSAReinitiate(string employee_gid, MdlLSAReinitiate values)
        {

            msGetGid = objcmnfunctions.GetMasterGID("LSAR");
            msSQL = " insert into ocs_trn_tlsareinitiatelog(" +
                    " lsareinitiatelog_gid," +
                    " ref_generatelsagid," +
                    " reinitatelsa_remarks," +
                    " reinitiated_by," +
                    " reinitiated_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.generatelsa_gid + "'," +
                    "'" + values.reinitatelsa_remarks.Replace("'", "") + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msSQL = " update ocs_trn_tgeneratelsa set reinitiate_eligibleflag='N' " +
                        " where generatelsa_gid = '" + values.generatelsa_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

               

                msSQL = " select application2sanction_gid,application_gid " +
                        " from ocs_trn_tgeneratelsa where generatelsa_gid='" + values.generatelsa_gid + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows == true)
                {
                    lsapplication2sanction_gid = objODBCDataReader["application2sanction_gid"].ToString();
                    lsapplication_gid = objODBCDataReader["application_gid"].ToString();
                }
                objODBCDataReader.Close();


                generatelsa_gid = objcmnfunctions.GetMasterGID("LSAG");
                msSQL = " insert into ocs_trn_tgeneratelsa(" +
                            " generatelsa_gid," +
                            " application2sanction_gid," +
                            " application_gid, " +
                            " created_by," +
                            " created_date)" +
                            " values(" +
                            "'" + generatelsa_gid + "'," +
                            "'" + lsapplication2sanction_gid + "'," +
                            "'" + lsapplication_gid + "'," +
                            "'" + employee_gid + "'," +
                            "current_timestamp)";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {

                    msGetprstypGid = objcmnfunctions.GetMasterGID("PRTA");
                    msSQL = " insert into ocs_trn_tprocesstype_assign(" +
                               " processtypeassign_gid," +
                               " application_gid," +
                               " customer_urn, " +
                               " processtype_name, " +
                               " cadgroup_gid, " +
                               " cadgroup_name," +
                               " menu_gid," +
                               " menu_name, " +
                               " maker_gid," +
                               " maker_name," +
                               " checker_gid," +
                               " checker_name," +
                               " approver_gid, " +
                               " approver_name, " +
                               " created_by," +
                               " created_date, " +
                               " assign_type," +
                               " lsareinitiate_flag," +
                               " lsareinitiatelog_gid)" +
                               " select @processtypeassign_gid := '" + msGetprstypGid + "',application_gid,customer_urn,processtype_name,cadgroup_gid,cadgroup_name," +
                               " menu_gid,menu_name,maker_gid,maker_name,checker_gid,checker_name,approver_gid,approver_name,'" + employee_gid + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',assign_type,'Y','" + msGetGid + "' " +
                               " from ocs_trn_tprocesstype_assign where application_gid='" + lsapplication_gid + "' and menu_name='LSA' limit 1 ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " select limitproductinfodtl_gid " +
                            " from ocs_trn_tlimitproductinfo where generatelsa_gid='" + values.generatelsa_gid + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            string msGetLimitGid = objcmnfunctions.GetMasterGID("LPIG");
                            msSQL = " insert into ocs_trn_tlimitproductinfo(" +
                                " limitproductinfodtl_gid," +
                                " application_gid," +
                                " application2sanction_gid, " +
                                " generatelsa_gid, " +
                                " application2loan_gid, " +
                                " interchangeability," +
                                " report_structure_gid," +
                                " report_structure, " +
                                " odlim_amount," +
                                " odlim_condition, " +
                                " existing_limit," +
                                //" limit_released," +
                                " limitinfo_remarks," +
                                " created_by," +
                                " created_date," +
                                " producttype_gid," +
                                " product_type," +
                                " productsubtype_gid," +
                                " productsub_type," +
                                " loanfacility_amount," +
                                " updated_by," +
                                " updated_date," +
                                " documented_limit," +
                                " dateof_Expiry," +
                                " limitproduct_flag," +
                                //" lsatotalreleased_amount," +
                                " lsatotalreleased_approved," +
                                " lsprodreinitiate_flag)" +
                                " select @limitproductinfodtl_gid := '" + msGetLimitGid + "',application_gid,application2sanction_gid,'" + generatelsa_gid + "',application2loan_gid,interchangeability,report_structure_gid,  " +
                                " report_structure,odlim_amount,odlim_condition,existing_limit,limitinfo_remarks,  " +
                                " created_by,created_date,producttype_gid,product_type,productsubtype_gid,productsub_type,  " +
                                " loanfacility_amount,updated_by,updated_date,documented_limit,dateof_Expiry,limitproduct_flag,lsatotalreleased_approved,lsprodreinitiate_flag " +
                                " from ocs_trn_tlimitproductinfo where generatelsa_gid='" + values.generatelsa_gid + "' and limitproductinfodtl_gid='" + dt["limitproductinfodtl_gid"].ToString() + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                        dt_datatable.Dispose();
                    }



                    msSQL = " select lsauploaddocument_gid " +
                            " from ocs_trn_tlsauploaddocument where generatelsa_gid='" + values.generatelsa_gid + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            msGetdocGid = objcmnfunctions.GetMasterGID("LUDG");
                            msSQL = " insert into ocs_trn_tlsauploaddocument(" +
                                        " lsauploaddocument_gid," +
                                        " generatelsa_gid," +
                                        " document_name, " +
                                        " document_path, " +
                                        " document_type, " +
                                        " created_by," +
                                        " created_date)" +
                                        " select @lsauploaddocument_gid := '" + msGetdocGid + "','" + generatelsa_gid + "',document_name,document_path,document_type,created_by,created_date " +
                                        " from ocs_trn_tlsauploaddocument where generatelsa_gid='" + values.generatelsa_gid + "' and lsauploaddocument_gid='" + dt["lsauploaddocument_gid"] + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    dt_datatable.Dispose();

                    msSQL = " select lsafeescharge_gid " +
                            " from ocs_trn_tlsafeescharge where generatelsa_gid='" + values.generatelsa_gid + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            string msgetfeechargeGid = objcmnfunctions.GetMasterGID("LFCG");
                            msSQL = " insert into ocs_trn_tlsafeescharge(" +
                                        " lsafeescharge_gid," +
                                        " application_gid," +
                                        " generatelsa_gid, " +
                                        " processing_fee, " +
                                        " processing_collectiontype, " +
                                        " doc_charges," +
                                        " doccharge_collectiontype," +
                                        " fieldvisit_charges, " +
                                        " fieldvisit_charges_collectiontype," +
                                        " adhoc_fee, " +
                                        " adhoc_collectiontype," +
                                        " life_insurance," +
                                        " lifeinsurance_collectiontype," +
                                        " acct_insurance," +
                                        " total_collect," +
                                        " total_deduct," +
                                        " product_type," +
                                        " producttype_gid," +
                                        " created_by," +
                                        " created_date," +
                                        " updated_date," +
                                        " updated_by," +
                                        " acctinsurance_collectiontype," +
                                        " application2servicecharge_gid," +
                                        " termlifeinsurance_flag," +
                                        " personalaccidentinsurance," +
                                        " adhocfee_flag," +
                                        " processingfees_flag," +
                                        " documentcharges_flag," +
                                        " fieldvisitcharges_flag)" +
                                        " select @lsafeescharge_gid := '" + msgetfeechargeGid + "',application_gid,'" + generatelsa_gid + "',processing_fee,processing_collectiontype,doc_charges,doccharge_collectiontype,  " +
                                        " fieldvisit_charges,fieldvisit_charges_collectiontype,adhoc_fee,adhoc_collectiontype,life_insurance,lifeinsurance_collectiontype,  " +
                                        " acct_insurance,total_collect,total_deduct,product_type,producttype_gid,created_by,  " +
                                        " created_date,updated_date,updated_by,acctinsurance_collectiontype,application2servicecharge_gid,termlifeinsurance_flag,  " +
                                        " personalaccidentinsurance,adhocfee_flag,processingfees_flag,documentcharges_flag,fieldvisitcharges_flag " +
                                        " from ocs_trn_tlsafeescharge where generatelsa_gid='" + values.generatelsa_gid + "' and lsafeescharge_gid='" + dt["lsafeescharge_gid"] + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }

                    }
                    dt_datatable.Dispose();

                    msGetcmckGid = objcmnfunctions.GetMasterGID("LCCG");
                    msSQL = " insert into ocs_trn_tlsacompliancecheckdetail(" +
                            " lsacompliancecheckdetail_gid," +
                            " generatelsa_gid, " +
                            " nachmandateform_held, " +
                            " nachmandateform_heldremarks, " +
                            " signmatching_nachform," +
                            " signmatching_nachformremarks," +
                            " namesign_kycmatching, " +
                            " namesign_kycmatchingremarks," +
                            " escrowaccount_opened, " +
                            " escrowaccount_openedremarks," +
                            " appropriate_stamping," +
                            " appropriate_stampingremarks," +
                            " rocfiling_initiated," +
                            " rocfiling_initiatedremarks," +
                            " cersai_initiated," +
                            " cersai_initiatedremarks," +
                            " alldeferralcovenant_captured," +
                            " allpredisbursement_stipulated," +
                            " maker_signaturename," +
                            " headcredit_admin," +
                            " created_by," +
                            " created_date," +
                            " updated_date," +
                            " updated_by)" +
                            " select @lsacompliancecheckdetail_gid := '" + msGetcmckGid + "','" + generatelsa_gid + "',nachmandateform_held,nachmandateform_heldremarks,signmatching_nachform,signmatching_nachformremarks,namesign_kycmatching,  " +
                       " namesign_kycmatchingremarks,escrowaccount_opened,escrowaccount_openedremarks,appropriate_stamping,appropriate_stampingremarks,rocfiling_initiated,  " +
                       " rocfiling_initiatedremarks,cersai_initiated,cersai_initiatedremarks,alldeferralcovenant_captured,allpredisbursement_stipulated,maker_signaturename,  " +
                       " headcredit_admin,created_by,created_date,updated_date,updated_by " +
                       " from ocs_trn_tlsacompliancecheckdetail where generatelsa_gid='" + values.generatelsa_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " select lsabankaccdtl_gid " +
                            " from ocs_trn_tlsabankaccountdtl where generatelsa_gid='" + values.generatelsa_gid + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            msGetacdtlGid = objcmnfunctions.GetMasterGID("LBAD");
                            msSQL = " insert into ocs_trn_tlsabankaccountdtl(" +
                                " lsabankaccdtl_gid," +
                                " generatelsa_gid, " +
                                " application2sanction_gid, " +
                                " application_gid, " +
                                " ifsc_code," +
                                " bank_name," +
                                " branch_name, " +
                                " branch_address," +
                                " micr_code, " +
                                " bankaccount_number," +
                                " confirmbankaccount_number," +
                                " accountholder_name," +
                                " accounttype_gid," +
                                " accounttype_name," +
                                " joint_account," +
                                " jointaccountholder_name," +
                                " chequebookfacility_available," +
                                " accountopen_date," +
                                " disbursement_accountstatus," +
                                " created_by," +
                                " created_date," +
                                " updated_by," +
                                " updated_date," +
                                " name," +
                                " stakeholder_type," +
                                " credit_gid)" +
                                " select @lsabankaccdtl_gid := '" + msGetacdtlGid + "','" + generatelsa_gid + "',application2sanction_gid,application_gid,ifsc_code,bank_name,branch_name,  " +
                               " branch_address,micr_code,bankaccount_number,confirmbankaccount_number,accountholder_name,accounttype_gid,  " +
                               " accounttype_name,joint_account,jointaccountholder_name,chequebookfacility_available,accountopen_date,disbursement_accountstatus,  " +
                               " created_by,created_date,updated_by,updated_date,name,stakeholder_type,credit_gid " +
                               " from ocs_trn_tlsabankaccountdtl where generatelsa_gid='" + values.generatelsa_gid + "' and lsabankaccdtl_gid='" + dt["lsabankaccdtl_gid"] + "'";

                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    dt_datatable.Dispose();

                    values.status = true;
                    values.message = "LSA Reinitiated Successfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occurred";
                }
            }

        }
        public bool DaGetBankAccountStatus(string application_gid,string rmdisbursementrequest_gid, MdlBankAccountlist values, string employee_gid)
        {

            string lsgeneratelsa_flag = ""; string disbursement_flag = "N";
            msSQL = " select bankaccountstatus_gid from ocs_trn_tbankaccountstatus  where (rmdisbursementrequest_gid='" + rmdisbursementrequest_gid + "' or " +
                    " rmdisbursementrequest_gid='" + employee_gid + "') and disbursementaccount_status = 'Yes'";
            lsgeneratelsa_flag = objdbconn.GetExecuteScalar(msSQL);
            if (lsgeneratelsa_flag != "")
            {
                disbursement_flag = "Y";
            }
            if (disbursement_flag == "N")
            {
                values.lsoveralldisbursement_flag = "Y";

            }
            else
            {
                values.lsoveralldisbursement_flag = "N";

            }



            string lsdisbapplicantbankdtl_gid = ""; string lsbankaccount_status = "N";
            msSQL = " select disbapplicantbankdtl_gid from ocs_trn_tdisbapplicantbankdtl  where (rmdisbursementrequest_gid='" + rmdisbursementrequest_gid + "'" +
                    " or rmdisbursementrequest_gid='" + employee_gid + "') and disbursementaccount_status = 'Yes'";
            lsdisbapplicantbankdtl_gid = objdbconn.GetExecuteScalar(msSQL);
            if (lsdisbapplicantbankdtl_gid != "")
            {
                values.lsbankaccount_status = "Y";
                values.lsoveralldisbursement_flag = "N";

            }

            return true;
        }

        //LSA Report Summary
        public void DaGetLSAReportSummary(MdlLSAReportSummaryList values)
        {
             msSQL = " select b.lsa_refno,a.application_gid,e.application2sanction_gid,b.generatelsa_gid,a.application_no,a.customer_urn,a.customer_name as customer_name, " +
                     " date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as ccapproved_date,e.sanction_refno, " +
                     " date_format(e.sanction_date, '%d-%m-%Y') as sanction_date,b.overall_lsastatus,a.renewal_flag,a.enhancement_flag,b.reinitiate_eligibleflag from ocs_trn_tcadapplication a " +
                     " left join ocs_trn_tgeneratelsa b on b.application_gid = a.application_gid " +
                     " left join ocs_trn_tprocesstype_assign d on d.application_gid = a.application_gid " +
                     " left join ocs_trn_tapplication2sanction e on a.application_gid= e.application_gid " +
                     " where d.menu_gid='" + getMenuClass.LSA + "' and d.maker_approvalflag ='Y' and d.checker_approvalflag='Y' and b.overall_lsastatus='Approved' and e.accepted_status ='Y' " +
                     " and d.approver_approvalflag='Y' " +
                     " group by b.generatelsa_gid order by e.application2sanction_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getMdlLSAReportSummary_list = new List<MdlLSAReportSummary>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getMdlLSAReportSummary_list.Add(new MdlLSAReportSummary
                    {
                        lsa_refno = dt["lsa_refno"].ToString(),
                        application_no = dt["application_no"].ToString(),
                        generatelsa_gid = dt["generatelsa_gid"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        ccapproved_date = dt["ccapproved_date"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        sanction_refno = dt["sanction_refno"].ToString(),
                        sanction_date = dt["sanction_date"].ToString(),
                        application2sanction_gid = dt["application2sanction_gid"].ToString(),
                        overall_lsastatus = dt["overall_lsastatus"].ToString(),
                        renewal_flag = dt["renewal_flag"].ToString(),
                        enhancement_flag = dt["enhancement_flag"].ToString(),
                        reinitiate_eligibleflag = dt["reinitiate_eligibleflag"].ToString(),
                    });
                }
            }
            values.MdlLSAReportSummary = getMdlLSAReportSummary_list;
            dt_datatable.Dispose();
        }

        //LSA Report Excel Export
        public void DaGetLSAReportExcelExport(MdlLSAReportExcel values)
        {
            //msSQL = " select b.application_no as 'Application Number',a.lsa_refno as 'LSA Reference Number', " +
            //        " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as 'LSA Created Date', " +
            //        " date_format(a.lsa_approveddate, '%d-%m-%Y %h:%i %p') as 'LSA Approved Date', " +
            //        " b.customer_name as 'Customer Name', b.customer_urn as 'Customer URN', " +
            //         " e.sanction_refno as 'Sanction Reference Number', " +
            //         " date_format(e.sanction_date, '%d-%m-%Y %h:%i %p') as 'Sanction Date', d.maker_name as 'Maker Name', d.checker_name as 'Checker Name'," +
            //         " (select group_concat(product_type separator ' | ') from ocs_trn_tlimitproductinfo where generatelsa_gid = a.generatelsa_gid) as 'Product Type', " +
            //         " (select group_concat(productsub_type separator ' | ') from ocs_trn_tlimitproductinfo where generatelsa_gid = a.generatelsa_gid) as 'Productsub Type', " +
            //         " (select group_concat(odlim_amount separator ' | ') from ocs_trn_tlimitproductinfo where generatelsa_gid = a.generatelsa_gid) as 'ODLIM Amount', " +
            //         " (select group_concat(interchangeability separator ' | ') from ocs_trn_tlimitproductinfo where generatelsa_gid = a.generatelsa_gid) as 'Interchangeability', " +
            //         " (select group_concat(report_structure separator ' | ') from ocs_trn_tlimitproductinfo where generatelsa_gid = a.generatelsa_gid) as 'Report Structure', " +
            //         " (select group_concat(odlim_condition separator ' | ') from ocs_trn_tlimitproductinfo where generatelsa_gid = a.generatelsa_gid) as 'ODLIM Condition', " +
            //        " (select group_concat(date_format(dateof_Expiry, '%d-%m-%Y %h:%i %p') separator ' | ') from ocs_trn_tlimitproductinfo where generatelsa_gid = a.generatelsa_gid) as 'Date of Expiry', " +
            //         " (select group_concat(documented_limit separator ' | ') from ocs_trn_tlimitproductinfo where generatelsa_gid = a.generatelsa_gid) as 'Documented Limit', " +
            //         " (select group_concat(existing_limit separator ' | ') from ocs_trn_tlimitproductinfo where generatelsa_gid = a.generatelsa_gid) as 'Existing Limit', " +
            //         " (select group_concat(limit_released separator ' | ') from ocs_trn_tlimitproductinfo where generatelsa_gid = a.generatelsa_gid) as 'Limit Released', " +
            //         " (select group_concat(limitinfo_remarks separator ' | ') from ocs_trn_tlimitproductinfo where generatelsa_gid = a.generatelsa_gid) as 'Limitinfo Remarks', " +
            //         " (select group_concat(stakeholder_type separator ' | ') from ocs_trn_tlsabankaccountdtl where generatelsa_gid = a.generatelsa_gid) as 'Stakeholder Type', " +
            //         " (select group_concat(name separator ' | ') from ocs_trn_tlsabankaccountdtl where generatelsa_gid = a.generatelsa_gid) as 'Name', " +
            //         " (select group_concat(bank_name separator ' | ') from ocs_trn_tlsabankaccountdtl where generatelsa_gid = a.generatelsa_gid) as 'Bank Name', " +
            //         " (select group_concat(ifsc_code separator ' | ') from ocs_trn_tlsabankaccountdtl where generatelsa_gid = a.generatelsa_gid) as 'IFSC Code', " +
            //         " (select group_concat(bankaccount_number separator ' | ') from ocs_trn_tlsabankaccountdtl where generatelsa_gid = a.generatelsa_gid) as 'Bankaccount Number', " +
            //         " (select group_concat(processing_fee separator ' | ') from ocs_trn_tlsafeescharge where generatelsa_gid = a.generatelsa_gid) as 'Processing Fees'," +
            //         " (select group_concat(doc_charges separator ' | ') from ocs_trn_tlsafeescharge where generatelsa_gid = a.generatelsa_gid) as 'Documentation Charges' " +
            //         " from ocs_trn_tgeneratelsa a " +
            //        " left join ocs_trn_tcadapplication b on b.application_gid = a.application_gid " +
            //         " left join ocs_trn_tprocesstype_assign d on d.application_gid = a.application_gid and d.menu_gid ='CADMGTLSA' " +
            //         " left join ocs_trn_tapplication2sanction e on a.application_gid = e.application_gid " +
            //         " where d.menu_gid = 'CADMGTLSA' and d.maker_approvalflag = 'Y' and d.checker_approvalflag = 'Y' and a.overall_lsastatus = 'Approved' and e.accepted_status = 'Y' " +
            //         " and d.approver_approvalflag = 'Y' group by a.generatelsa_gid order by e.application2sanction_gid desc ";


            msSQL = "call ocs_trn_tsplsareportfirstquery ()";
            dt_datatable1 = objdbconn.GetDataTable(msSQL);
            msSQL = "call ocs_trn_tsplsareportsecondquery ()";
            dt_datatable2 = objdbconn.GetDataTable(msSQL);
            string lscompany_code = string.Empty;
            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);

            var workSheet1 = excel.Workbook.Worksheets.Add("Application_List");
            var workSheet2 = excel.Workbook.Worksheets.Add("Other_Details");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";
                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                values.lsname = "LSA Report.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/LSA Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/LSA Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;

                values.lscloudpath = lscompany_code + "/" + "Master/LSA Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }

                workSheet1.Cells[1, 1].LoadFromDataTable(dt_datatable1, true);
                FileInfo file = new FileInfo(values.lspath);
                using (var range = workSheet1.Cells[1, 1, 1, 22])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);
                }
                workSheet2.Cells[1, 1].LoadFromDataTable(dt_datatable2, true);
                //FileInfo file = new FileInfo(values.lspath);
                using (var range = workSheet2.Cells[1, 1, 1, 19])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);
                }
                //excel.SaveAs(file);
                excel.SaveAs(ms);
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Master/LSA Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname, ms);
                ms.Close();

            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Failure";
                return;
            }
            values.lscloudpath = objcmnstorage.EncryptData(values.lscloudpath);
            values.lspath = objcmnstorage.EncryptData(values.lspath);
            values.status = true;
            values.message = "Success";
        }
    }
}
