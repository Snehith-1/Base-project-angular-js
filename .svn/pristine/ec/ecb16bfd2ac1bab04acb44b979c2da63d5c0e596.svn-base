using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data;
using System.Data.Odbc;
using ems.utilities.Functions;
using ems.marketing.Models;

namespace ems.marketing.DataAccess
{
    public class DaMarketingDashboard
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        DataTable dt_datatable;
        string msSQL;
        int mnResult;
        string msGetGID, lsemployeeGID;
        OdbcDataReader objODBCDataReader;
        string lspath = string.Empty;
        string lscompany_code = string.Empty;
        DataTable dt_table;
        OdbcDataReader objreader;


        public string DaGetNewTabDtl(string employee_gid, MdlNewTab values)
        {
            try
            {
                msSQL = "select employeereporting_to from adm_mst_tmodule2employee where employeereporting_to='" + employee_gid + "' and module_gid='MKT'";
                lsemployeeGID = objdbconn.GetExecuteScalar(msSQL);
                if (lsemployeeGID != "")
                {
                    lsemployeeGID = objcmnfunctions.childloop(employee_gid);
                }
                else {
                    lsemployeeGID = "'E2'";
                        }
                msSQL = " Select  b.leadbank_name,K.campaign_title," +
              " concat(g.leadbankcontact_name,' / ',g.mobile,' / ',g.email) as contact_details," +
              " concat(d.region_name,'/',b.leadbank_city,'/',b.leadbank_state,'/',h.source_name) as region_name," +
              " z.leadstage_name, concat(f.user_firstname,'/',f.user_lastname,'/',f.user_code) as assign_to," +
              " Case when a.internal_notes is not null then a.internal_notes" +
              " when a.internal_notes is null then b.remarks  end as internal_notes," +
              " z.leadstage_name," +
              " a.lead2campaign_gid, a.leadbank_gid, a.campaign_gid, g.leadbankcontact_gid" +
              " From crm_trn_tlead2campaign a" +
              " left join crm_trn_tleadbank b on a.leadbank_gid = b.leadbank_gid        " +
              " left join crm_mst_tregion d on b.leadbank_region=d.region_gid           " +
              " left join crm_trn_tleadbankcontact g on b.leadbank_gid = g.leadbank_gid " +
              " left join crm_mst_tsource h on b.source_gid=h.source_gid                " +
              " left join crm_trn_tcampaign k on a.campaign_gid=k.campaign_gid          " +
              " left join crm_mst_tleadstage z on a.leadstage_gid=z.leadstage_gid" +
              " left join hrm_mst_temployee e on e.employee_gid=a.assign_to" +
             " left join adm_mst_tuser f on f.user_gid=e.user_gid" +
              " where a.assign_to in ('"+employee_gid +"',"+ lsemployeeGID.TrimEnd(',') +") and a.pending_call is null " +
              " and (a.leadstage_gid ='1' or a.leadstage_gid is null)" +
              " and g.status='Y' and g.main_contact='Y' order by b.leadbank_name asc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getnewtabdetails = new List<newtab_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getnewtabdetails.Add(new newtab_list
                        {
                            team_name = (dr_datarow["campaign_title"].ToString()),
                            customer_name = (dr_datarow["leadbank_name"].ToString()),
                            region_name = (dr_datarow["region_name"].ToString()),
                            internal_notes = (dr_datarow["internal_notes"].ToString()),
                            contact_details = (dr_datarow["contact_details"].ToString()),
                            assign_to = (dr_datarow["assign_to"].ToString()),
                        });
                    }
                    values.newtab_list = getnewtabdetails;
                }
                dt_datatable.Dispose();
                values.status = true;
                values.message = "Data Fetched";
            }
            catch(Exception ex)
            {
                values.status = false;
                values.message = ex.Message;
            }
             return lsemployeeGID;
        }
        public void DaGetFollowUpTabDtl(string employee_gid, MdlFollowUpTab values)
        {
            try
            {
                msSQL = "select employeereporting_to from adm_mst_tmodule2employee where employeereporting_to='" + employee_gid + "' and module_gid='MKT'";
                lsemployeeGID = objdbconn.GetExecuteScalar(msSQL);
                if (lsemployeeGID != "")
                {
                    lsemployeeGID = objcmnfunctions.childloop(employee_gid);
                }
                else
                {
                    lsemployeeGID = "'E2'";
                }
                msSQL = " Select  b.leadbank_name,k.campaign_title,m.calllog_gid,m.call_response,g.email,g.leadbankcontact_name,g.mobile," +
              " concat(g.leadbankcontact_name,' / ',g.mobile,' / ',g.email) as contact_details," +
              " concat(d.region_name,'/',b.leadbank_city,'/',b.leadbank_state,'/',h.source_name) as region_name," +
              " Case when a.internal_notes is not null then a.internal_notes" +
              " when a.internal_notes is null then b.remarks  end as internal_notes," +
               " z.leadstage_name, concat(f.user_firstname,'/',f.user_lastname,'/',f.user_code) as assign_to," +
              " z.leadstage_name," +
              " a.lead2campaign_gid, a.leadbank_gid, a.campaign_gid, g.leadbankcontact_gid" +
              " From crm_trn_tlead2campaign a" +
              " left join crm_trn_tleadbank b on a.leadbank_gid = b.leadbank_gid        " +
              " left join crm_mst_tregion d on b.leadbank_region=d.region_gid           " +
              " left join crm_trn_tleadbankcontact g on b.leadbank_gid = g.leadbank_gid " +
              " left join crm_mst_tsource h on b.source_gid=h.source_gid                " +
              " left join crm_trn_tcampaign k on a.campaign_gid=k.campaign_gid          " +
              " left join crm_mst_tleadstage z on a.leadstage_gid=z.leadstage_gid" +
              " left join crm_trn_tcalllog m on m.leadbank_gid=a.leadbank_gid " +
              " left join hrm_mst_temployee e on e.employee_gid=a.assign_to" +
             " left join adm_mst_tuser f on f.user_gid=e.user_gid" +
              " where a.assign_to in ('" + employee_gid + "'," + lsemployeeGID.TrimEnd(',') + ")" +
              " and (a.leadstage_gid ='2')" +
              " and g.status='Y' and g.main_contact='Y' order by b.leadbank_name asc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getnewtabdetails = new List<FollowUptab_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getnewtabdetails.Add(new FollowUptab_list
                        {
                            team_name = (dr_datarow["campaign_title"].ToString()),
                            customer_name = (dr_datarow["leadbank_name"].ToString()),
                            region_name = (dr_datarow["region_name"].ToString()),
                            internal_notes = (dr_datarow["internal_notes"].ToString()),
                            contact_details = (dr_datarow["contact_details"].ToString()),
                            assign_to = (dr_datarow["assign_to"].ToString()),
                            call_response = (dr_datarow["call_response"].ToString()),
                        });
                    }
                    values.FollowUptab_list = getnewtabdetails;
                }
                dt_datatable.Dispose();
                values.status = true;
                values.message = "Data Fetched";
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = ex.Message;
            }
        }
        public void DaGetProspectTabDtl(string employee_gid, MdlProspectTab values)
        {
            try
            {
                msSQL = "select employeereporting_to from adm_mst_tmodule2employee where employeereporting_to='" + employee_gid + "' and module_gid='MKT'";
                lsemployeeGID = objdbconn.GetExecuteScalar(msSQL);
                if (lsemployeeGID != "")
                {
                    lsemployeeGID = objcmnfunctions.childloop(employee_gid);
                }
                else
                {
                    lsemployeeGID = "'E2'";
                }
                msSQL = " Select  b.leadbank_name,K.campaign_title,g.email,g.leadbankcontact_name,g.mobile," +
              " concat(g.leadbankcontact_name,' / ',g.mobile,' / ',g.email) as contact_details," +
              " concat(f.user_firstname,'/',f.user_lastname,'/',f.user_code) as assign_to," +
              " concat(d.region_name,'/',b.leadbank_city,'/',b.leadbank_state,'/',h.source_name) as region_name," +
              " Case when a.internal_notes is not null then a.internal_notes" +
              " when a.internal_notes is null then b.remarks  end as internal_notes," +
              " z.leadstage_name,k.campaign_title," +
              " a.lead2campaign_gid, a.leadbank_gid, a.campaign_gid, g.leadbankcontact_gid" +
              " From crm_trn_tlead2campaign a" +
              " left join crm_trn_tleadbank b on a.leadbank_gid = b.leadbank_gid        " +
              " left join crm_mst_tregion d on b.leadbank_region=d.region_gid           " +
              " left join crm_trn_tleadbankcontact g on b.leadbank_gid = g.leadbank_gid " +
              " left join crm_mst_tsource h on b.source_gid=h.source_gid                " +
              " left join crm_trn_tcampaign k on a.campaign_gid=k.campaign_gid          " +
              " left join crm_mst_tleadstage z on a.leadstage_gid=z.leadstage_gid" +
              " left join hrm_mst_temployee e on e.employee_gid=a.assign_to" +
              " left join adm_mst_tuser f on f.user_gid=e.user_gid" +
              " where a.assign_to in ('" + employee_gid + "'," + lsemployeeGID.TrimEnd(',') + ") " +
              " and (a.leadstage_gid ='3')" +
              " and g.status='Y' and g.main_contact='Y' order by b.leadbank_name asc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getnewtabdetails = new List<Prospecttab_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getnewtabdetails.Add(new Prospecttab_list
                        {
                            team_name = (dr_datarow["campaign_title"].ToString()),
                            customer_name = (dr_datarow["leadbank_name"].ToString()),
                            region_name = (dr_datarow["region_name"].ToString()),
                            internal_notes = (dr_datarow["internal_notes"].ToString()),
                            contact_details = (dr_datarow["contact_details"].ToString()),
                            assign_to = (dr_datarow["assign_to"].ToString()),
                        });
                    }
                    values.Prospecttab_list = getnewtabdetails;
                }
                dt_datatable.Dispose();
                values.status = true;
                values.message = "Data Fetched";
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = ex.Message;
            }
        }
        public void DaGetPotentialTabDtl(string employee_gid, MdlPotentialTab values)
        {
            try
            {
                msSQL = "select employeereporting_to from adm_mst_tmodule2employee where employeereporting_to='" + employee_gid + "' and module_gid='MKT'";
                lsemployeeGID = objdbconn.GetExecuteScalar(msSQL);
                if (lsemployeeGID != "")
                {
                    lsemployeeGID = objcmnfunctions.childloop(employee_gid);
                }
                else
                {
                    lsemployeeGID = "'E2'";
                }
                msSQL = " Select  b.leadbank_name,K.campaign_title,g.email,g.leadbankcontact_name,g.mobile," +
               " concat(g.leadbankcontact_name,' / ',g.mobile,' / ',g.email) as contact_details," +
              " concat(f.user_firstname,'/',f.user_lastname,'/',f.user_code) as assign_to," +
              " concat(d.region_name,'/',b.leadbank_city,'/',b.leadbank_state,'/',h.source_name) as region_name," +
              " Case when a.internal_notes is not null then a.internal_notes" +
              " when a.internal_notes is null then b.remarks  end as internal_notes," +
              " z.leadstage_name," +
              " a.lead2campaign_gid, a.leadbank_gid, a.campaign_gid, g.leadbankcontact_gid" +
              " From crm_trn_tlead2campaign a" +
              " left join crm_trn_tleadbank b on a.leadbank_gid = b.leadbank_gid        " +
              " left join crm_mst_tregion d on b.leadbank_region=d.region_gid           " +
              " left join crm_trn_tleadbankcontact g on b.leadbank_gid = g.leadbank_gid " +
              " left join crm_mst_tsource h on b.source_gid=h.source_gid                " +
              " left join crm_trn_tcampaign k on a.campaign_gid=k.campaign_gid          " +
              " left join crm_mst_tleadstage z on a.leadstage_gid=z.leadstage_gid" +
              " left join hrm_mst_temployee e on e.employee_gid=a.assign_to" +
              " left join adm_mst_tuser f on f.user_gid=e.user_gid" +
              " where a.assign_to in ('" + employee_gid + "'," + lsemployeeGID.TrimEnd(',') + ")  " +
              " and (a.leadstage_gid ='6')" +
              " and g.status='Y' and g.main_contact='Y' order by b.leadbank_name asc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getnewtabdetails = new List<Potentialtab_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getnewtabdetails.Add(new Potentialtab_list
                        {
                            team_name = (dr_datarow["campaign_title"].ToString()),
                            customer_name = (dr_datarow["leadbank_name"].ToString()),
                            region_name = (dr_datarow["region_name"].ToString()),
                            internal_notes = (dr_datarow["internal_notes"].ToString()),
                            contact_details = (dr_datarow["contact_details"].ToString()),
                            assign_to = (dr_datarow["assign_to"].ToString()),
                        });
                    }
                    values.Potentialtab_list = getnewtabdetails;
                }
                dt_datatable.Dispose();
                values.status = true;
                values.message = "Data Fetched";
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = ex.Message;
            }
        }
        public void DaGetDropTabDtl(string employee_gid, MdlDropTab values)
        {
            try
            {
                msSQL = "select employeereporting_to from adm_mst_tmodule2employee where employeereporting_to='" + employee_gid + "' and module_gid='MKT'";
                lsemployeeGID = objdbconn.GetExecuteScalar(msSQL);
                if (lsemployeeGID != "")
                {
                    lsemployeeGID = objcmnfunctions.childloop(employee_gid);
                }
                else
                {
                    lsemployeeGID = "'E2'";
                }
                msSQL = " Select  b.leadbank_name,k.campaign_title,g.email,g.leadbankcontact_name,g.mobile," +
               " concat(g.leadbankcontact_name,' / ',g.mobile,' / ',g.email) as contact_details," +
              " concat(f.user_firstname,'/',f.user_lastname,'/',f.user_code) as assign_to," +
              " concat(d.region_name,'/',b.leadbank_city,'/',b.leadbank_state,'/',h.source_name) as region_name," +
              " Case when a.internal_notes is not null then a.internal_notes" +
              " when a.internal_notes is null then b.remarks  end as internal_notes," +
              " z.leadstage_name," +
              " a.lead2campaign_gid, a.leadbank_gid, a.campaign_gid, g.leadbankcontact_gid" +
              " From crm_trn_tlead2campaign a" +
              " left join crm_trn_tleadbank b on a.leadbank_gid = b.leadbank_gid        " +
              " left join crm_mst_tregion d on b.leadbank_region=d.region_gid           " +
              " left join crm_trn_tleadbankcontact g on b.leadbank_gid = g.leadbank_gid " +
              " left join crm_mst_tsource h on b.source_gid=h.source_gid                " +
              " left join crm_trn_tcampaign k on a.campaign_gid=k.campaign_gid          " +
              " left join crm_mst_tleadstage z on a.leadstage_gid=z.leadstage_gid" +
              " left join hrm_mst_temployee e on e.employee_gid=a.assign_to" +
              " left join adm_mst_tuser f on f.user_gid=e.user_gid" +
               " where a.assign_to in ('" + employee_gid + "'," + lsemployeeGID.TrimEnd(',') + ")  " +
              " and (a.leadstage_gid ='5')" +
              " and g.status='Y' and g.main_contact='Y' order by b.leadbank_name asc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getnewtabdetails = new List<Droptab_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getnewtabdetails.Add(new Droptab_list
                        {
                            team_name = (dr_datarow["campaign_title"].ToString()),
                            customer_name = (dr_datarow["leadbank_name"].ToString()),
                            region_name = (dr_datarow["region_name"].ToString()),
                            internal_notes = (dr_datarow["internal_notes"].ToString()),
                            contact_details = (dr_datarow["contact_details"].ToString()),
                            assign_to = (dr_datarow["assign_to"].ToString()),
                        });
                    }
                    values.Droptab_list = getnewtabdetails;
                }
                dt_datatable.Dispose();
                values.status = true;
                values.message = "Data Fetched";
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = ex.Message;
            }
        }
        public void DaGetAllTabDtl(string employee_gid, MdlAllTab values)
        {
            try
            {
                msSQL = "select employeereporting_to from adm_mst_tmodule2employee where employeereporting_to='" + employee_gid + "' and module_gid='MKT'";
                lsemployeeGID = objdbconn.GetExecuteScalar(msSQL);
                if (lsemployeeGID != "")
                {
                    lsemployeeGID = objcmnfunctions.childloop(employee_gid);
                }
                else
                {
                    lsemployeeGID = "'E2'";
                }
                msSQL = " Select  b.leadbank_name,k.campaign_title," +
              " concat(g.leadbankcontact_name,' / ',g.mobile,' / ',g.email) as contact_details," +
              " concat(f.user_firstname,'/',f.user_lastname,'/',f.user_code) as assign_to," +
              " concat(d.region_name,'/',b.leadbank_city,'/',b.leadbank_state,'/',h.source_name) as region_name," +
              " Case when a.internal_notes is not null then a.internal_notes" +
              " when a.internal_notes is null then b.remarks  end as internal_notes," +
              " z.leadstage_name," +
              " a.lead2campaign_gid, a.leadbank_gid, a.campaign_gid, g.leadbankcontact_gid" +
              " From crm_trn_tlead2campaign a" +
              " left join crm_trn_tleadbank b on a.leadbank_gid = b.leadbank_gid        " +
              " left join crm_mst_tregion d on b.leadbank_region=d.region_gid           " +
              " left join crm_trn_tleadbankcontact g on b.leadbank_gid = g.leadbank_gid " +
              " left join crm_mst_tsource h on b.source_gid=h.source_gid                " +
              " left join crm_trn_tcampaign k on a.campaign_gid=k.campaign_gid          " +
              " left join crm_mst_tleadstage z on a.leadstage_gid=z.leadstage_gid" +
              " left join hrm_mst_temployee e on e.employee_gid=a.assign_to" +
              " left join adm_mst_tuser f on f.user_gid=e.user_gid" +
              " where a.assign_to in ('" + employee_gid + "'," + lsemployeeGID.TrimEnd(',') + ") " +
              " and g.status='Y' and g.main_contact='Y' order by b.leadbank_name asc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getnewtabdetails = new List<Alltab_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getnewtabdetails.Add(new Alltab_list
                        {
                            team_name = (dr_datarow["campaign_title"].ToString()),
                            customer_name = (dr_datarow["leadbank_name"].ToString()),
                            region_name = (dr_datarow["region_name"].ToString()),
                            internal_notes = (dr_datarow["internal_notes"].ToString()),
                            contact_details = (dr_datarow["contact_details"].ToString()),
                            assign_to = (dr_datarow["assign_to"].ToString()),
                            leadstage_name = (dr_datarow["leadstage_name"].ToString()),
                        });
                    }
                    values.Alltab_list = getnewtabdetails;
                }
                dt_datatable.Dispose();
                values.status = true;
                values.message = "Data Fetched";
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = ex.Message;
            }
        }

        public void DaGetDashboardCount(string employee_gid, MdlDashboardCount values)
        {
            try
            {
                msSQL = " SELECT a.campaign_gid, a.campaign_title, a.campaign_location,c.branch_name, " +
              " date_format(a.campaign_startdate,'%d-%m-%Y') as campaign_startdate," +
              " case when a.campaign_enddate = '0000-00-00' then 'Infinite' else date_format(a.campaign_enddate,'%d-%m-%Y') end as campaign_enddate, " +
              " b.product_name, " +
              " (select count(employee_gid) as employeecount from crm_trn_tcampaign2employee d" +
              " where d.campaign_gid=a.campaign_gid) as employeecount, " +
              " (SELECT count(lead2campaign_gid) as total FROM crm_trn_tlead2campaign e" +
              " where e.campaign_gid = a.campaign_gid) as total, " +
              " (SELECT count(log_gid) as total_call FROM crm_trn_tlog z" +
              " left join crm_trn_tlead2campaign y on z.reference_gid = y.lead2campaign_gid" +
              " left join crm_trn_tcampaign x on y.campaign_gid=x.campaign_gid" +
              " where y.campaign_gid=a.campaign_gid and log_type = 'Call Log' and z.log_from='Marketing') as total_call," +
              " (Select distinct count(w.customer_gid) as total_customer" +
              " from crm_mst_tcustomer w" +
              " left join crm_trn_tleadbank v on w.customer_gid = v.customer_gid" +
              " left join crm_trn_tlead2campaign u on v.leadbank_gid = u.leadbank_gid " +
              " left join adm_mst_tuser t on t.user_gid=w.created_by" +
              " where u.campaign_gid = a.campaign_gid) as total_customer" +
              " FROM crm_trn_tcampaign a " +
              " left join crm_trn_tcampaign2employee f on f.campaign_gid=a.campaign_gid" +
              " Left Join pmr_mst_tproduct b on a.product_gid = b.product_gid " +
              " left join hrm_mst_tbranch c on a.campaign_location = c.branch_gid" +
              " where f.employee_gid='" + employee_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getnewtabdetails = new List<Dashboardcount_list>();
            
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {

                        getnewtabdetails.Add(new Dashboardcount_list
                        {
                            campaign_gid = (dr_datarow["campaign_gid"].ToString()),
                            campaign_title = (dr_datarow["campaign_title"].ToString()),
                            campaign_location = (dr_datarow["campaign_location"].ToString()),
                            branch_name = (dr_datarow["branch_name"].ToString()),
                            campaign_startdate = (dr_datarow["campaign_startdate"].ToString()),
                            campaign_enddate = (dr_datarow["campaign_enddate"].ToString()),
                            product_name = (dr_datarow["product_name"].ToString()),
                            employeecount = (dr_datarow["employeecount"].ToString()),
                            total_lead = (dr_datarow["total"].ToString()),
                            total_call = (dr_datarow["total_call"].ToString()),
                            total_customer = (dr_datarow["total_customer"].ToString()),
                        });

                    }
                    values.Dashboardcount_list = getnewtabdetails;
                   
                }
                dt_datatable.Dispose();
                values.status = true;
                values.message = "Data Fetched";
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = ex.Message;
            }
        }

        public void DaGetOverAllLeadCount(string employee_gid, MdlDashboardCount objvalues)
        {
            msSQL = "select employeereporting_to from adm_mst_tmodule2employee where employeereporting_to='" + employee_gid + "' and module_gid='MKT'";
            lsemployeeGID = objdbconn.GetExecuteScalar(msSQL);
            if (lsemployeeGID != "")
            {
                lsemployeeGID = objcmnfunctions.childloop(employee_gid);
            }
            else
            {
                lsemployeeGID = "'E2'";
            }

            msSQL = " select count(*) as count_new from crm_trn_tlead2campaign a  left join crm_trn_tleadbank b on a.leadbank_gid = b.leadbank_gid " +
                 " left join crm_trn_tleadbankcontact g on g.leadbank_gid = b.leadbank_gid where a.assign_to in ('" + employee_gid + "'," + lsemployeeGID.TrimEnd(',') + ") and a.pending_call is null " +
                 " and (a.leadstage_gid ='1' or a.leadstage_gid is null) and g.status='Y' and g.main_contact='Y'";
            objvalues.count_new = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select count(*) as count_followup from crm_trn_tlead2campaign a  left join crm_trn_tleadbank b on a.leadbank_gid = b.leadbank_gid " +
                " left join crm_trn_tleadbankcontact g on g.leadbank_gid = b.leadbank_gid where a.assign_to in ('" + employee_gid + "'," + lsemployeeGID.TrimEnd(',') + ") " +
                " and a.leadstage_gid ='2' and g.status='Y' and g.main_contact='Y'";
            objvalues.count_followup = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select count(*) as count_prospect from crm_trn_tlead2campaign a  left join crm_trn_tleadbank b on a.leadbank_gid = b.leadbank_gid " +
                " left join crm_trn_tleadbankcontact g on g.leadbank_gid = b.leadbank_gid where a.assign_to in ('" + employee_gid + "'," + lsemployeeGID.TrimEnd(',') + ") " +
                " and a.leadstage_gid ='3' and g.status='Y' and g.main_contact='Y'";
            objvalues.count_prospect = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select count(*) as count_potential from crm_trn_tlead2campaign a  left join crm_trn_tleadbank b on a.leadbank_gid = b.leadbank_gid " +
                " left join crm_trn_tleadbankcontact g on g.leadbank_gid = b.leadbank_gid where a.assign_to in ('" + employee_gid + "'," + lsemployeeGID.TrimEnd(',') + ")" +
                " and a.leadstage_gid ='6' and g.status='Y' and g.main_contact='Y'";
            objvalues.count_potential = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select count(*) as count_close from crm_trn_tlead2campaign a  left join crm_trn_tleadbank b on a.leadbank_gid = b.leadbank_gid " +
                " left join crm_trn_tleadbankcontact g on g.leadbank_gid = b.leadbank_gid where a.assign_to in ('" + employee_gid + "'," + lsemployeeGID.TrimEnd(',') + ")" +
                " and a.leadstage_gid ='5' and g.status='Y' and g.main_contact='Y'";
            objvalues.count_close = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select count(*) as count_drop from crm_trn_tlead2campaign a  left join crm_trn_tleadbank b on a.leadbank_gid = b.leadbank_gid " +
                " left join crm_trn_tleadbankcontact g on g.leadbank_gid = b.leadbank_gid where a.assign_to in ('" + employee_gid + "'," + lsemployeeGID.TrimEnd(',') + ") " +
                " and a.leadstage_gid ='5' and g.status='Y' and g.main_contact='Y'";
            objvalues.count_drop = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select count(*) as count_all from crm_trn_tlead2campaign a left join crm_trn_tleadbank b on a.leadbank_gid = b.leadbank_gid " +
                " left join crm_trn_tleadbankcontact g on g.leadbank_gid = b.leadbank_gid where a.assign_to in ('" + employee_gid + "'," + lsemployeeGID.TrimEnd(',') + ")" +
                " and g.status='Y' and g.main_contact='Y'";
            objvalues.count_all = objdbconn.GetExecuteScalar(msSQL);
        }

        public string DaGetOverAllCount(string employee_gid, MdlNewTab values)
        {
            try
            {
                msSQL = "select employeereporting_to from adm_mst_tmodule2employee where employeereporting_to='" + employee_gid + "' and module_gid='MKT'";
                lsemployeeGID = objdbconn.GetExecuteScalar(msSQL);
                if (lsemployeeGID != "")
                {
                    lsemployeeGID = objcmnfunctions.childloop(employee_gid);
                }
                else
                {
                    lsemployeeGID = "'E2'";
                }
                msSQL = " Select  b.leadbank_name,K.campaign_title," +
              " concat(g.leadbankcontact_name,' / ',g.mobile,' / ',g.email) as contact_details," +
              " concat(d.region_name,'/',b.leadbank_city,'/',b.leadbank_state,'/',h.source_name) as region_name," +
              " z.leadstage_name, concat(f.user_firstname,'/',f.user_lastname,'/',f.user_code) as assign_to," +
              " Case when a.internal_notes is not null then a.internal_notes" +
              " when a.internal_notes is null then b.remarks  end as internal_notes," +
              " z.leadstage_name," +
              " a.lead2campaign_gid, a.leadbank_gid, a.campaign_gid, g.leadbankcontact_gid" +
              " From crm_trn_tlead2campaign a" +
              " left join crm_trn_tleadbank b on a.leadbank_gid = b.leadbank_gid        " +
              " left join crm_mst_tregion d on b.leadbank_region=d.region_gid           " +
              " left join crm_trn_tleadbankcontact g on b.leadbank_gid = g.leadbank_gid " +
              " left join crm_mst_tsource h on b.source_gid=h.source_gid                " +
              " left join crm_trn_tcampaign k on a.campaign_gid=k.campaign_gid          " +
              " left join crm_mst_tleadstage z on a.leadstage_gid=z.leadstage_gid" +
              " left join hrm_mst_temployee e on e.employee_gid=a.assign_to" +
             " left join adm_mst_tuser f on f.user_gid=e.user_gid" +
              " where a.assign_to in ('" + employee_gid + "'," + lsemployeeGID.TrimEnd(',') + ") and a.pending_call is null " +
              //" and (a.leadstage_gid ='1' or a.leadstage_gid is null)" +
              " and g.status='Y' and g.main_contact='Y' order by b.leadbank_name asc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getnewtabdetails = new List<alltabcount_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getnewtabdetails.Add(new alltabcount_list
                        {
                            team_name = (dr_datarow["campaign_title"].ToString()),
                            customer_name = (dr_datarow["leadbank_name"].ToString()),
                            region_name = (dr_datarow["region_name"].ToString()),
                            internal_notes = (dr_datarow["internal_notes"].ToString()),
                            contact_details = (dr_datarow["contact_details"].ToString()),
                            assign_to = (dr_datarow["assign_to"].ToString()),
                        });
                    }
                    values.alltabcount_list = getnewtabdetails;
                }
                dt_datatable.Dispose();
                values.status = true;
                values.message = "Data Fetched";
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = ex.Message;
            }
            return lsemployeeGID;
        }

        public string childloop(string employee)
        {
            msSQL = " select a.*, concat(b.user_firstname, '-', b.user_code) as user  from adm_mst_tmodule2employee a  " +
                " inner join hrm_mst_temployee c on a.employee_gid = c.employee_gid  " +
                " inner join adm_mst_tuser b on c.user_gid = b.user_gid  " +
                " where a.module_gid = 'MKT'  " +
                " and a.employeereporting_to = '" + employee + "'";
            dt_table = objdbconn.GetDataTable(msSQL);
            foreach (DataRow dr_datarow in dt_table.Rows)
            {
                msSQL = " select a.*, b.user_gid  from adm_mst_tmodule2employee a  " +
                    " inner join hrm_mst_temployee c on a.employee_gid = c.employee_gid  " +
                    " inner join adm_mst_tuser b on c.user_gid = b.user_gid  " +
                    " where a.module_gid = 'MKT' ";
                msSQL += " and a.employee_gid = '" + dr_datarow["employee_gid"].ToString() + "'";
                objreader = objdbconn.GetDataReader(msSQL);
                if (objreader.HasRows == true)
                {
                    objreader.Read();
                    lsemployeeGID = lsemployeeGID + "'" + objreader["employee_gid"].ToString() + "',";
                }
                objreader.Close();
                childloop(dr_datarow["employee_gid"].ToString());
            }

            dt_table.Dispose();
            return lsemployeeGID;
        }
    }
}