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
    public class DaZonalAllocation
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        DataTable dt_datatable;
        string msSQL;
        string lsdisbursement_date, lsdaypassed_visit;
        OdbcDataReader objODBCDatareader;
        string lszonalmapping_gid;
        int lsallocation_pendingcount, mnResult;
        string lscount_status, lscount_fresh, lscount_revisit;

        public bool DaGetZonalQualifiedAllocation(string employee_gid, qualifiedallocationlist values)
        {

            msSQL = " select a.customer_urn,case when(DATEDIFF(CURDATE(), lastvisit_date) > 180 and c.last_disb_date > lastvisit_date) then 'Re-Visit' " +
                    " when(DATEDIFF(CURDATE(), lastvisit_date) > 180 and c.last_disb_date < lastvisit_date) then 'Re-Visit'" +
                    " else 'Fresh' end as qualified_status ,a.customername,format(a.total_sanction, 2) as total_sanction," +
                    " case when(b.lastvisit_date is null) then DATEDIFF(CURDATE(), c.first_disb_date) else" +
                    " DATEDIFF(CURDATE(), c.last_disb_date) end as daypassed_disbursement," +
                    " DATEDIFF(CURDATE(), b.lastvisit_date) as daypassed_visit," +
                    " case when(DATEDIFF(CURDATE(), lastvisit_date) > 180) then date_format(c.last_disb_date, '%d-%m-%Y')" +
                    " else date_format(c.first_disb_date, '%d-%m-%Y') end as disbursement_date," +
                    " date_format(lastvisit_date, '%d-%m-%Y') as lastvisit_date,ac_status" +
                    " from ocs_mst_tcustomer a" +
                    " left join rsk_trn_tcustomervisit b on a.customer_urn = b.customer_urn" +
                    " left join rsk_trn_tcustomerdisbursement c on a.customer_urn = c.customer_urn" +
                    " where zonal_gid in (select zonalmapping_gid from rsk_mst_tzonalmapping where zonalrisk_managerGid = '" + employee_gid + "')" +
                    " and(((DATEDIFF(CURDATE(), first_disb_date)) > 31 and c.allocate_flag = 'N' and c.exclusion_flag='N'  and lastvisit_date is null and ac_status = '0') or" +
                    " ((DATEDIFF(CURDATE(), lastvisit_date)) > 180 and ac_status = '0' and c.allocate_flag = 'N' and c.exclusion_flag='N'))" +
                    " and total_sanction >= 5000000 order by daypassed_visit desc, daypassed_disbursement asc limit 500";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_mappingdtl = new List<qualifiedallocation>();
            if (dt_datatable.Rows.Count != 0)
            {
                values.qualifiedallocation = dt_datatable.AsEnumerable().Select(row => new qualifiedallocation
                {
                    customer_urn = row["customer_urn"].ToString(),
                    customer_name = row["customername"].ToString(),
                    disbursement_date = row["disbursement_date"].ToString(),
                    daypassed_disbursement = row["daypassed_disbursement"].ToString(),
                    lastvisit_date = row["lastvisit_date"].ToString(),
                    qualified_status = row["qualified_status"].ToString(),
                    daypassed_visit = row["daypassed_visit"].ToString(),
                    total_sanction = row["total_sanction"].ToString(),
                }).ToList();
            }
            dt_datatable.Dispose();
            return true;
        }

        public bool DaGetZoanlFreshAllocation(string employee_gid, qualifiedallocationlist values)
        {
            msSQL = " select a.customer_urn,case when(DATEDIFF(CURDATE(), lastvisit_date) > 180 and c.last_disb_date > lastvisit_date) then 'Re-Visit' " +
                    " else 'Fresh' end as qualified_status,a.customername,format(a.total_sanction, 2) as total_sanction, " +
                    "  case when(b.lastvisit_date is null) then DATEDIFF(CURDATE(), c.first_disb_date) else " +
                    "  DATEDIFF(CURDATE(), c.last_disb_date) end as daypassed_disbursement, " +
                    "  DATEDIFF(CURDATE(), b.lastvisit_date) as daypassed_visit, " +
                    "  case when(DATEDIFF(CURDATE(), lastvisit_date) > 180) then date_format(c.last_disb_date, '%d-%m-%Y') " +
                    "  else date_format(c.first_disb_date, '%d-%m-%Y') end as disbursement_date, " +
                    "  date_format(lastvisit_date, '%d-%m-%Y') as lastvisit_date,ac_status " +
                    "  from ocs_mst_tcustomer a " +
                    "  left join rsk_trn_tcustomervisit b on a.customer_urn = b.customer_urn " +
                    "  left join rsk_trn_tcustomerdisbursement c on a.customer_urn = c.customer_urn " +
                    "  where zonal_gid in (select zonalmapping_gid from rsk_mst_tzonalmapping where zonalrisk_managerGid = '" + employee_gid + "') and " +
                    "  (((DATEDIFF(CURDATE(), first_disb_date)) > 31 and c.allocate_flag = 'N' and c.exclusion_flag='N' and lastvisit_date is null and ac_status = '0')) " +
                    "  and total_sanction >= 5000000 order by daypassed_visit desc, daypassed_disbursement asc limit 500";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_mappingdtl = new List<qualifiedallocation>();
            if (dt_datatable.Rows.Count != 0)
            {
                values.qualifiedallocation = dt_datatable.AsEnumerable().Select(row => new qualifiedallocation
                {
                    customer_urn = row["customer_urn"].ToString(),
                    customer_name = row["customername"].ToString(),
                    disbursement_date = row["disbursement_date"].ToString(),
                    daypassed_disbursement = row["daypassed_disbursement"].ToString(),
                    lastvisit_date = row["lastvisit_date"].ToString(),
                    qualified_status = row["qualified_status"].ToString(),
                    daypassed_visit = row["daypassed_visit"].ToString(),
                    total_sanction = row["total_sanction"].ToString(),

                }).ToList();
            }
            dt_datatable.Dispose();
            return true;
        }

        public bool DaGetZoanlReVisitAllocation(string employee_gid, qualifiedallocationlist values)
        {
            msSQL = " select a.customer_urn,a.customername,format(a.total_sanction, 2) as total_sanction, " +
                    " case when(b.lastvisit_date is null) then DATEDIFF(CURDATE(), c.first_disb_date) else " +
                    " DATEDIFF(CURDATE(), c.last_disb_date) end as daypassed_disbursement," +
                    " DATEDIFF(CURDATE(), b.lastvisit_date) as daypassed_visit, " +
                    " case when(DATEDIFF(CURDATE(), lastvisit_date) > 180) then date_format(c.last_disb_date, '%d-%m-%Y') " +
                    " else date_format(c.first_disb_date, '%d-%m-%Y') end as disbursement_date, " +
                    " date_format(lastvisit_date, '%d-%m-%Y') as lastvisit_date, " +
                    " case when(DATEDIFF(CURDATE(), lastvisit_date) > 180 and c.last_disb_date > lastvisit_date) then 'Re-Visit' " +
                    " when(DATEDIFF(CURDATE(), lastvisit_date) > 180 and c.last_disb_date < lastvisit_date) then 'Re-Visit' " +
                    " else 'Fresh' end as qualified_status from ocs_mst_tcustomer a " +
                    " left join rsk_trn_tcustomervisit b on a.customer_urn = b.customer_urn " +
                    " left join rsk_trn_tcustomerdisbursement c on a.customer_urn = c.customer_urn " +
                    " where zonal_gid in (select zonalmapping_gid from rsk_mst_tzonalmapping where zonalrisk_managerGid = '" + employee_gid + "')" +
                    " and((DATEDIFF(CURDATE(), lastvisit_date)) > 180 and ac_status = '0' and c.allocate_flag = 'N' and c.exclusion_flag='N')" +
                    " and total_sanction >= 5000000 order by daypassed_visit desc, daypassed_disbursement asc limit 500";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_mappingdtl = new List<qualifiedallocation>();
            if (dt_datatable.Rows.Count != 0)
            {
                values.qualifiedallocation = dt_datatable.AsEnumerable().Select(row => new qualifiedallocation
                {
                    customer_urn = row["customer_urn"].ToString(),
                    customer_name = row["customername"].ToString(),
                    disbursement_date = row["disbursement_date"].ToString(),
                    daypassed_disbursement = row["daypassed_disbursement"].ToString(),
                    lastvisit_date = row["lastvisit_date"].ToString(),
                    qualified_status = row["qualified_status"].ToString(),
                    daypassed_visit = row["daypassed_visit"].ToString(),
                    total_sanction = row["total_sanction"].ToString(),
                }).ToList();
            }
            dt_datatable.Dispose();

            return true;
        }
        public bool DaGetZonalCurrentAllocation(string assignedZonalRM_gid, allocationlist values)
        {

            msSQL = " select a.customer_gid,f.customer_urn,format(a.sanction_amount, 2) as sanction_amount,f.vertical_code,f.customername, " +
                      " b.allocationdtl_gid,b.allocation_status,b.allocate_external,date_format(b.created_date, '%d-%m-%Y') as allocated_date, " +
                      " concat(e.zonal_name, ' / ', b.state_name, ' / ', b.district_name) as allocatedLocation,b.allocation_flag, " +
                      " cast(monthname(visit_allocated_date) as char) as visitallocatemonth," +
                      " date_format(d.lastvisit_date, '%d-%m-%Y') as lastvisit_date,DATEDIFF(CURDATE(), d.lastvisit_date) as count_visit," +
                      " case when(d.lastvisit_date is null) then date_format(c.first_disb_date, '%d-%m-%Y') else" +
                      " date_format(c.last_disb_date, '%d-%m-%Y') end as disbursement_date," +
                      "  case when(d.lastvisit_date is null) then DATEDIFF(CURDATE(), c.first_disb_date) else" +
                      " DATEDIFF(CURDATE(), c.last_disb_date) end as daypassed_disbursement," +
                      "  case when(d.lastvisit_date is null) then date_format(c.first_disb_date, '%Y-%m-%d') else" +
                      "  date_format(c.last_disb_date, '%Y-%m-%d') end as disbursementDate," +
                      "  date_format(d.lastvisit_date, '%Y-%m-%d') as lastvisitDate," +
                      " b.assignedRM_name,b.zonalRM_name,date_format(cast(date_add(date_format(b.created_date,'%Y-%m-%d'),INTERVAL 60 DAY) as date), '%d-%m-%Y')as cutoff_date " +
                      " from rsk_trn_tallocation a" +
                      " left join rsk_trn_tallocationdtl b on a.allocation_gid = b.allocation_gid " +
                      " left join rsk_trn_tcustomervisit d on d.customer_gid = a.customer_gid" +
                      " left join rsk_mst_tzonalmapping e on b.zonal_gid = e.zonalmapping_gid" +
                      " left join ocs_mst_tcustomer f on f.customer_gid = a.customer_gid" +
                      " left join rsk_trn_tcustomerdisbursement c on f.customer_urn = c.customer_urn" +
                      " where b.allocation_status = 'Allocated' and b.visit_allocated_date <= NOW() " +
                      " and a.zonal_gid in (select zonalmapping_gid from rsk_mst_tzonalmapping where zonalrisk_managerGid='" + assignedZonalRM_gid + "') " +
                      "group by a.customer_gid ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_mappingdtl = new List<allocationdtl>();
            values.count_currentallo = dt_datatable.Rows.Count;
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    if (dt["disbursement_date"].ToString() != "" && dt["lastvisit_date"].ToString() != "")
                    {
                        if (Convert.ToDateTime(dt["disbursementDate"]) > Convert.ToDateTime(dt["lastvisitDate"]))
                        {
                            lsdisbursement_date = dt["disbursement_date"].ToString();
                            lsdaypassed_visit = dt["daypassed_disbursement"].ToString();
                        }
                        else
                        {
                            lsdisbursement_date = lsdaypassed_visit = "-";
                        }
                    }
                    else
                    {
                        lsdisbursement_date = dt["disbursement_date"].ToString();
                        lsdaypassed_visit = dt["daypassed_disbursement"].ToString();
                    }

                    get_mappingdtl.Add(new allocationdtl
                    {
                        allocationdtl_gid = dt["allocationdtl_gid"].ToString(),
                        allocated_date = dt["allocated_date"].ToString(),
                        customer_gid = dt["customer_gid"].ToString(),
                        customername = dt["customername"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical = dt["vertical_code"].ToString(),
                        location = dt["allocatedLocation"].ToString(),
                        assigned_RM = dt["assignedRM_name"].ToString(),
                        ZonalRMname = dt["zonalRM_name"].ToString(),
                        allocation_flag = dt["allocation_flag"].ToString(),
                        allocation_status = dt["allocation_status"].ToString(),
                        allocate_external = dt["allocate_external"].ToString(),
                        lastvisit_date = dt["lastvisit_date"].ToString(),
                        count_lastvisit = dt["count_visit"].ToString(),
                        disbursement_date = lsdisbursement_date,
                        daypassed_disbursement = lsdaypassed_visit,
                        visit_allocatemonth = dt["visitallocatemonth"].ToString(),
                        sanction_amount = dt["sanction_amount"].ToString(),
                        cutoff_date = dt["cutoff_date"].ToString(),
                    });
                }
                values.allocationdtl = get_mappingdtl;
            }
            dt_datatable.Dispose();
            return true;
        }

        public bool DaGetZonalUpcomingAllocation(string assignedZonalRM_gid, allocationlist values)
        {

            msSQL = " select a.customer_gid,f.customer_urn,format(a.sanction_amount, 2) as sanction_amount,f.vertical_code,f.customername, " +
                      " b.allocationdtl_gid,b.allocation_status,b.allocate_external,date_format(b.created_date, '%d-%m-%Y') as allocated_date, " +
                      " concat(e.zonal_name, ' / ', b.state_name, ' / ', b.district_name) as allocatedLocation,b.allocation_flag," +
                      " date_format(d.lastvisit_date, '%d-%m-%Y') as lastvisit_date,DATEDIFF(CURDATE(), d.lastvisit_date) as count_visit," +
                      " case when(d.lastvisit_date is null) then date_format(c.first_disb_date, '%d-%m-%Y') else" +
                      " date_format(c.last_disb_date, '%d-%m-%Y') end as disbursement_date,cast(monthname(visit_allocated_date) as char) as visitallocatemonth," +
                      "  case when(d.lastvisit_date is null) then DATEDIFF(CURDATE(), c.first_disb_date) else" +
                      " DATEDIFF(CURDATE(), c.last_disb_date) end as daypassed_disbursement," +
                      "  case when(d.lastvisit_date is null) then date_format(c.first_disb_date, '%Y-%m-%d') else" +
                      "  date_format(c.last_disb_date, '%Y-%m-%d') end as disbursementDate," +
                      "  date_format(d.lastvisit_date, '%Y-%m-%d') as lastvisitDate," +
                      " b.assignedRM_name,b.zonalRM_name from rsk_trn_tallocation a" +
                      " left join rsk_trn_tallocationdtl b on a.allocation_gid = b.allocation_gid " +
                      " left join rsk_trn_tcustomervisit d on d.customer_gid = a.customer_gid" +
                      " left join rsk_mst_tzonalmapping e on b.zonal_gid = e.zonalmapping_gid" +
                      " left join ocs_mst_tcustomer f on f.customer_gid = a.customer_gid" +
                      " left join rsk_trn_tcustomerdisbursement c on f.customer_urn = c.customer_urn" +
                      " where b.allocation_status = 'Allocated' and b.visit_allocated_date > NOW() " +
                      " and a.zonal_gid in (select zonalmapping_gid from rsk_mst_tzonalmapping where zonalrisk_managerGid='" + assignedZonalRM_gid + "') " +
                      "group by a.customer_gid ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_mappingdtl = new List<allocationdtl>();
            values.count_upcoming = dt_datatable.Rows.Count;
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    if (dt["disbursement_date"].ToString() != "" && dt["lastvisit_date"].ToString() != "")
                    {
                        if (Convert.ToDateTime(dt["disbursementDate"]) > Convert.ToDateTime(dt["lastvisitDate"]))
                        {
                            lsdisbursement_date = dt["disbursement_date"].ToString();
                            lsdaypassed_visit = dt["daypassed_disbursement"].ToString();
                        }
                        else
                        {
                            lsdisbursement_date = lsdaypassed_visit = "-";
                        }
                    }
                    else
                    {
                        lsdisbursement_date = dt["disbursement_date"].ToString();
                        lsdaypassed_visit = dt["daypassed_disbursement"].ToString();
                    }

                    get_mappingdtl.Add(new allocationdtl
                    {
                        allocationdtl_gid = dt["allocationdtl_gid"].ToString(),
                        allocated_date = dt["allocated_date"].ToString(),
                        customer_gid = dt["customer_gid"].ToString(),
                        customername = dt["customername"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical = dt["vertical_code"].ToString(),
                        location = dt["allocatedLocation"].ToString(),
                        assigned_RM = dt["assignedRM_name"].ToString(),
                        ZonalRMname = dt["zonalRM_name"].ToString(),
                        allocation_flag = dt["allocation_flag"].ToString(),
                        allocation_status = dt["allocation_status"].ToString(),
                        allocate_external = dt["allocate_external"].ToString(),
                        lastvisit_date = dt["lastvisit_date"].ToString(),
                        count_lastvisit = dt["count_visit"].ToString(),
                        visit_allocatemonth = dt["visitallocatemonth"].ToString(),
                        disbursement_date = lsdisbursement_date,
                        daypassed_disbursement = lsdaypassed_visit,
                        sanction_amount = dt["sanction_amount"].ToString(),
                    });
                }
                values.allocationdtl = get_mappingdtl;
            }
            dt_datatable.Dispose();
            return true;
        }



        public bool DaGetBreachedAllocationSummary(breachedlist values, string employee_gid)
        {

            msSQL = " select a.customer_gid,f.customer_urn,format(a.sanction_amount, 2) as sanction_amount,f.vertical_code,f.customername, " +
                   " b.allocationdtl_gid,b.allocation_status,b.allocate_external,date_format(b.created_date, '%d-%m-%Y') as allocated_date, " +
                   " concat(e.zonal_name, ' / ', b.state_name, ' / ', b.district_name) as allocatedLocation,b.allocation_flag," +
                   " date_format(d.lastvisit_date, '%d-%m-%Y') as lastvisit_date,DATEDIFF(CURDATE(), d.lastvisit_date) as count_visit," +
                   " case when(d.lastvisit_date is null) then date_format(c.first_disb_date, '%d-%m-%Y') else" +
                   " date_format(c.last_disb_date, '%d-%m-%Y') end as disbursement_date,cast(monthname(visit_allocated_date) as char) as visitallocatemonth," +
                   "  case when(d.lastvisit_date is null) then DATEDIFF(CURDATE(), c.first_disb_date) else" +
                   " DATEDIFF(CURDATE(), c.last_disb_date) end as daypassed_disbursement," +
                   "  case when(d.lastvisit_date is null) then date_format(c.first_disb_date, '%Y-%m-%d') else" +
                   "  date_format(c.last_disb_date, '%Y-%m-%d') end as disbursementDate," +
                   "  date_format(d.lastvisit_date, '%Y-%m-%d') as lastvisitDate," +
                   " b.assignedRM_name,b.zonalRM_name from rsk_trn_tallocation a" +
                   " left join rsk_trn_tallocationdtl b on a.allocation_gid = b.allocation_gid " +
                   " left join rsk_trn_tcustomervisit d on d.customer_gid = a.customer_gid" +
                   " left join rsk_mst_tzonalmapping e on b.zonal_gid = e.zonalmapping_gid" +
                   " left join ocs_mst_tcustomer f on f.customer_gid = a.customer_gid" +
                   " left join rsk_trn_tcustomerdisbursement c on f.customer_urn = c.customer_urn" +
                   " where DATEDIFF(CURDATE(), visit_allocated_date)>90 and b.lastvisit_date is null " +
                   " and a.zonal_gid in (select zonalmapping_gid from rsk_mst_tzonalmapping where zonalrisk_managerGid='" + employee_gid + "') " +
                   " group by a.customer_gid ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_mappingdtl = new List<breacheddtl>();
            values.count_breached = dt_datatable.Rows.Count;
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    if (dt["disbursement_date"].ToString() != "" && dt["lastvisit_date"].ToString() != "")
                    {
                        if (Convert.ToDateTime(dt["disbursementDate"]) > Convert.ToDateTime(dt["lastvisitDate"]))
                        {
                            lsdisbursement_date = dt["disbursement_date"].ToString();
                            lsdaypassed_visit = dt["daypassed_disbursement"].ToString();
                        }
                        else
                        {
                            lsdisbursement_date = lsdaypassed_visit = "-";
                        }
                    }
                    else
                    {
                        lsdisbursement_date = dt["disbursement_date"].ToString();
                        lsdaypassed_visit = dt["daypassed_disbursement"].ToString();
                    }

                    get_mappingdtl.Add(new breacheddtl
                    {
                        allocationdtl_gid = dt["allocationdtl_gid"].ToString(),
                        allocated_date = dt["allocated_date"].ToString(),
                        customer_gid = dt["customer_gid"].ToString(),
                        customername = dt["customername"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical = dt["vertical_code"].ToString(),
                        location = dt["allocatedLocation"].ToString(),
                        assigned_RM = dt["assignedRM_name"].ToString(),
                        ZonalRMname = dt["zonalRM_name"].ToString(),
                        allocation_flag = dt["allocation_flag"].ToString(),
                        allocation_status = dt["allocation_status"].ToString(),
                        lastvisit_date = dt["lastvisit_date"].ToString(),
                        count_lastvisit = dt["count_visit"].ToString(),
                        disbursement_date = lsdisbursement_date,
                        daypassed_disbursement = lsdaypassed_visit,
                        sanction_amount = dt["sanction_amount"].ToString(),
                        visit_allocatemonth = dt["visitallocatemonth"].ToString(),

                    });
                }
                values.breacheddtl = get_mappingdtl;
            }
            dt_datatable.Dispose();

            return true;
        }


        public bool DaGetZonalcompletedAlloSummary(allocationlist values, string employee_gid)
        {

            msSQL = " select a.customer_gid,f.customer_urn,format(a.sanction_amount, 2) as sanction_amount,f.vertical_code,f.customername, " +
                   " b.allocationdtl_gid,b.allocation_status,b.allocate_external,date_format(b.created_date, '%d-%m-%Y') as allocated_date, " +
                   " cast(monthname(visit_allocated_date) as char) as visitallocatemonth," +
                   " concat(e.zonal_name, ' / ', b.state_name, ' / ', b.district_name) as allocatedLocation,b.allocation_flag," +
                   " date_format(d.lastvisit_date, '%d-%m-%Y') as lastvisit_date,DATEDIFF(CURDATE(), d.lastvisit_date) as count_visit," +
                   " case when(d.lastvisit_date is null) then date_format(c.first_disb_date, '%d-%m-%Y') else" +
                   " date_format(c.last_disb_date, '%d-%m-%Y') end as disbursement_date," +
                   "  case when(d.lastvisit_date is null) then DATEDIFF(CURDATE(), c.first_disb_date) else" +
                   " DATEDIFF(CURDATE(), c.last_disb_date) end as daypassed_disbursement," +
                   "  case when(d.lastvisit_date is null) then date_format(c.first_disb_date, '%Y-%m-%d') else" +
                   "  date_format(c.last_disb_date, '%Y-%m-%d') end as disbursementDate," +
                   "  date_format(d.lastvisit_date, '%Y-%m-%d') as lastvisitDate," +
                   " b.assignedRM_name,b.zonalRM_name from rsk_trn_tallocation a" +
                   " left join rsk_trn_tallocationdtl b on a.allocation_gid = b.allocation_gid " +
                   " left join rsk_trn_tcustomervisit d on d.customer_gid = a.customer_gid" +
                   " left join rsk_mst_tzonalmapping e on b.zonal_gid = e.zonalmapping_gid" +
                   " left join ocs_mst_tcustomer f on f.customer_gid = a.customer_gid" +
                   " left join rsk_trn_tcustomerdisbursement c on f.customer_urn = c.customer_urn" +
                   " where a.status = 'Completed' " +
                   " and a.zonal_gid in (select zonalmapping_gid from rsk_mst_tzonalmapping where zonalrisk_managerGid='" + employee_gid + "') " +
                   " group by a.customer_gid ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_mappingdtl = new List<allocationdtl>();
            values.count_completedallo = dt_datatable.Rows.Count;
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    if (dt["disbursement_date"].ToString() != "" && dt["lastvisit_date"].ToString() != "")
                    {
                        if (Convert.ToDateTime(dt["disbursementDate"]) > Convert.ToDateTime(dt["lastvisitDate"]))
                        {
                            lsdisbursement_date = dt["disbursement_date"].ToString();
                            lsdaypassed_visit = dt["daypassed_disbursement"].ToString();
                        }
                        else
                        {
                            lsdisbursement_date = lsdaypassed_visit = "-";
                        }
                    }
                    else
                    {
                        lsdisbursement_date = dt["disbursement_date"].ToString();
                        lsdaypassed_visit = dt["daypassed_disbursement"].ToString();
                    }

                    get_mappingdtl.Add(new allocationdtl
                    {
                        allocationdtl_gid = dt["allocationdtl_gid"].ToString(),
                        allocated_date = dt["allocated_date"].ToString(),
                        customer_gid = dt["customer_gid"].ToString(),
                        customername = dt["customername"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical = dt["vertical_code"].ToString(),
                        location = dt["allocatedLocation"].ToString(),
                        assigned_RM = dt["assignedRM_name"].ToString(),
                        ZonalRMname = dt["zonalRM_name"].ToString(),
                        allocation_flag = dt["allocation_flag"].ToString(),
                        allocation_status = dt["allocation_status"].ToString(),
                        allocate_external = dt["allocate_external"].ToString(),
                        lastvisit_date = dt["lastvisit_date"].ToString(),
                        count_lastvisit = dt["count_visit"].ToString(),
                        disbursement_date = lsdisbursement_date,
                        daypassed_disbursement = lsdaypassed_visit,
                        sanction_amount = dt["sanction_amount"].ToString(),
                        visit_allocatemonth = dt["visitallocatemonth"].ToString(),
                    });
                }
                values.allocationdtl = get_mappingdtl;
            }
            dt_datatable.Dispose();

            return true;
        }

        public bool DaGetZonalExternalAllocation(allocationlist values, string employee_gid)
        {
            msSQL = " select a.customer_gid,f.customer_urn,format(a.sanction_amount, 2) as sanction_amount,f.vertical_code,f.customername, " +
                   " b.allocationdtl_gid,b.allocation_status,b.allocate_external,date_format(b.target_date, '%d-%m-%Y') as target_date, " +
                   " date_format(b.created_date, '%d-%m-%Y') as allocated_date,cast(monthname(visit_allocated_date) as char) as visitallocatemonth," +
                   " concat(e.zonal_name, ' / ', b.state_name, ' / ', b.district_name) as allocatedLocation,b.allocation_flag," +
                   " date_format(d.lastvisit_date, '%d-%m-%Y') as lastvisit_date,DATEDIFF(CURDATE(), d.lastvisit_date) as count_visit," +
                   " case when(d.lastvisit_date is null) then date_format(c.first_disb_date, '%d-%m-%Y') else" +
                   " date_format(c.last_disb_date, '%d-%m-%Y') end as disbursement_date," +
                   "  case when(d.lastvisit_date is null) then DATEDIFF(CURDATE(), c.first_disb_date) else" +
                   " DATEDIFF(CURDATE(), c.last_disb_date) end as daypassed_disbursement," +
                   "  case when(d.lastvisit_date is null) then date_format(c.first_disb_date, '%Y-%m-%d') else" +
                   "  date_format(c.last_disb_date, '%Y-%m-%d') end as disbursementDate," +
                   "  date_format(d.lastvisit_date, '%Y-%m-%d') as lastvisitDate," +
                   " b.allocate_externalname from rsk_trn_tallocation a" +
                   " left join rsk_trn_tallocationdtl b on a.allocation_gid = b.allocation_gid " +
                   " left join rsk_trn_tcustomervisit d on d.customer_gid = a.customer_gid" +
                   " left join rsk_mst_tzonalmapping e on b.zonal_gid = e.zonalmapping_gid" +
                   " left join ocs_mst_tcustomer f on f.customer_gid = a.customer_gid" +
                   " left join rsk_trn_tcustomerdisbursement c on f.customer_urn = c.customer_urn" +
                   " where b.allocation_status = 'External' " +
                   " and a.zonal_gid in (select zonalmapping_gid from rsk_mst_tzonalmapping where zonalrisk_managerGid='" + employee_gid + "') " +
                   " group by a.customer_gid ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_mappingdtl = new List<allocationdtl>();
            values.count_external = dt_datatable.Rows.Count;
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    if (dt["disbursement_date"].ToString() != "" && dt["lastvisit_date"].ToString() != "")
                    {
                        if (Convert.ToDateTime(dt["disbursementDate"]) > Convert.ToDateTime(dt["lastvisitDate"]))
                        {
                            lsdisbursement_date = dt["disbursement_date"].ToString();
                            lsdaypassed_visit = dt["daypassed_disbursement"].ToString();
                        }
                        else
                        {
                            lsdisbursement_date = lsdaypassed_visit = "-";
                        }
                    }
                    else
                    {
                        lsdisbursement_date = dt["disbursement_date"].ToString();
                        lsdaypassed_visit = dt["daypassed_disbursement"].ToString();
                    }

                    get_mappingdtl.Add(new allocationdtl
                    {
                        allocationdtl_gid = dt["allocationdtl_gid"].ToString(),
                        customer_gid = dt["customer_gid"].ToString(),
                        customername = dt["customername"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical = dt["vertical_code"].ToString(),
                        location = dt["allocatedLocation"].ToString(),
                        allocate_externalname = dt["allocate_externalname"].ToString(),
                        allocation_flag = dt["allocation_flag"].ToString(),
                        allocation_status = dt["allocation_status"].ToString(),
                        allocate_external = dt["allocate_external"].ToString(),
                        lastvisit_date = dt["lastvisit_date"].ToString(),
                        count_lastvisit = dt["count_visit"].ToString(),
                        disbursement_date = lsdisbursement_date,
                        daypassed_disbursement = lsdaypassed_visit,
                        sanction_amount = dt["sanction_amount"].ToString(),
                        target_date = dt["target_date"].ToString(),
                        allocated_date = dt["allocated_date"].ToString(),
                        visit_allocatemonth = dt["visitallocatemonth"].ToString(),
                    });
                }
                values.allocationdtl = get_mappingdtl;
            }
            dt_datatable.Dispose();

            return true;
        }

        public bool DaGetZonalCustomerAllocation(customerList values, string employee_gid)
        {

            msSQL = " select customer_gid,customername from ocs_mst_tcustomer a where a.total_sanction >= 5000000 and " +
                   "  a.customer_gid not in (select customer_gid from rsk_trn_tallocationdtl " +
                   " where (allocation_flag = 'Y' or allocation_flag = 'P')) and a.zonal_riskmanager='" + employee_gid + "' order by customername asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_customerdtl = new List<customerdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    get_customerdtl.Add(new customerdtl
                    {
                        customer_gid = dt["customer_gid"].ToString(),
                        customer_name = dt["customername"].ToString()
                    });
                }

                get_customerdtl.Add(new customerdtl
                {
                    customer_gid = "",
                    customer_name = ""
                });
                values.customerdtl = get_customerdtl;
            }
            dt_datatable.Dispose();

            return true;
        }

        public bool DaGetVisitCancelChanges(allocationlist values, string employee_gid)
        {
            msSQL = " select a.customer_gid,f.customer_urn,format(a.sanction_amount, 2) as sanction_amount,f.vertical_code,f.customername, " +
         " b.allocationdtl_gid,b.allocation_status,b.allocate_external,date_format(b.created_date, '%d-%m-%Y') as allocated_date, " +
         " concat(e.zonal_name, ' / ', b.state_name, ' / ', b.district_name) as allocatedLocation,b.allocation_flag," +
         " date_format(d.lastvisit_date, '%d-%m-%Y') as lastvisit_date,DATEDIFF(CURDATE(), d.lastvisit_date) as count_visit," +
         " case when(d.lastvisit_date is null) then date_format(c.first_disb_date, '%d-%m-%Y') else" +
         " date_format(c.last_disb_date, '%d-%m-%Y') end as disbursement_date,cast(monthname(visit_allocated_date) as char) as visitallocatemonth," +
         "  case when(d.lastvisit_date is null) then DATEDIFF(CURDATE(), c.first_disb_date) else" +
         " DATEDIFF(CURDATE(), c.last_disb_date) end as daypassed_disbursement," +
         "  case when(d.lastvisit_date is null) then date_format(c.first_disb_date, '%Y-%m-%d') else" +
         "  date_format(c.last_disb_date, '%Y-%m-%d') end as disbursementDate," +
         "  date_format(d.lastvisit_date, '%Y-%m-%d') as lastvisitDate," +
         " b.assignedRM_name,b.zonalRM_name from rsk_trn_tallocation a" +
          " left join rsk_trn_tallocationdtl b on a.allocation_gid = b.allocation_gid " +
         " left join rsk_trn_tcustomervisit d on d.customer_gid = a.customer_gid" +
         " left join rsk_mst_tzonalmapping e on b.zonal_gid = e.zonalmapping_gid" +
         " left join ocs_mst_tcustomer f on f.customer_gid = a.customer_gid" +
         " left join rsk_trn_tcustomerdisbursement c on f.customer_urn = c.customer_urn" +
         " where a.reportcancel_flag = 'Y' " +
         " and a.zonal_gid in (select zonalmapping_gid from rsk_mst_tzonalmapping where zonalrisk_managerGid='" + employee_gid + "') " +
         " group by a.customer_gid ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_mappingdtl = new List<allocationdtl>();
            values.count_reportcancel = dt_datatable.Rows.Count;
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    if (dt["disbursement_date"].ToString() != "" && dt["lastvisit_date"].ToString() != "")
                    {
                        if (Convert.ToDateTime(dt["disbursementDate"]) > Convert.ToDateTime(dt["lastvisitDate"]))
                        {
                            lsdisbursement_date = dt["disbursement_date"].ToString();
                            lsdaypassed_visit = dt["daypassed_disbursement"].ToString();
                        }
                        else
                        {
                            lsdisbursement_date = lsdaypassed_visit = "-";
                        }
                    }
                    else
                    {
                        lsdisbursement_date = dt["disbursement_date"].ToString();
                        lsdaypassed_visit = dt["daypassed_disbursement"].ToString();
                    }

                    get_mappingdtl.Add(new allocationdtl
                    {
                        allocationdtl_gid = dt["allocationdtl_gid"].ToString(),
                        allocated_date = dt["allocated_date"].ToString(),
                        customer_gid = dt["customer_gid"].ToString(),
                        customername = dt["customername"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical = dt["vertical_code"].ToString(),
                        location = dt["allocatedLocation"].ToString(),
                        assigned_RM = dt["assignedRM_name"].ToString(),
                        ZonalRMname = dt["zonalRM_name"].ToString(),
                        allocation_flag = dt["allocation_flag"].ToString(),
                        allocation_status = dt["allocation_status"].ToString(),
                        allocate_external = dt["allocate_external"].ToString(),
                        lastvisit_date = dt["lastvisit_date"].ToString(),
                        count_lastvisit = dt["count_visit"].ToString(),
                        disbursement_date = lsdisbursement_date,
                        daypassed_disbursement = lsdaypassed_visit,
                        sanction_amount = dt["sanction_amount"].ToString(),
                        visit_allocatemonth = dt["visitallocatemonth"].ToString(),

                    });
                }
                values.allocationdtl = get_mappingdtl;
            }
            dt_datatable.Dispose();

            return true;
        }

        public bool DaGetZonalAllocationLogDetail(string employee_gid, todayactivityList values)
        {
            msSQL = " select a.allocationdtl_gid,concat(e.user_firstname,' ',e.user_lastname, ' / ', e.user_code) as VisitRM, " +
                   " concat(customer_urn, ' / ', customername) as customer_name,schedulelog_gid, " +
                   " date_format(appointment_date, '%d-%m-%Y') as appointmentdate, " +
                   " appointment_time,appointment_status,appointment_remarks" +
                   " from rsk_trn_tschedulelog a " +
                   " left join ocs_mst_tcustomer b on b.customer_gid = a.customer_gid " +
                   " left join rsk_trn_tallocationdtl c on c.allocationdtl_gid = a.allocationdtl_gid " +
                   " left join hrm_mst_temployee d on a.created_by = d.employee_gid " +
                   " left join adm_mst_tuser e on d.user_gid = e.user_gid " +
                   " where appointment_date = '" + DateTime.Now.ToString("yyyy-MM-dd") + "' and c.allocation_zonalRM = '" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getschedulelistdtl = new List<todayactivity>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getschedulelistdtl.Add(new todayactivity
                    {
                        scheduled_date = (dr_datarow["appointmentdate"].ToString()),
                        scheduled_time = (dr_datarow["appointment_time"].ToString()),
                        remarks = dr_datarow["appointment_remarks"].ToString(),
                        customer_name = dr_datarow["customer_name"].ToString(),
                        VisitRM = dr_datarow["VisitRM"].ToString(),
                    });
                }
                values.todayactivity = getschedulelistdtl;
            }

            msSQL = "select zonalmapping_gid,zonal_name from rsk_mst_tzonalmapping where zonalrisk_managerGid='" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lszonalmapping_gid = objODBCDatareader["zonalmapping_gid"].ToString();
                values.zonal_name = objODBCDatareader["zonal_name"].ToString();
            }
            objODBCDatareader.Close();
            msSQL = " select assigned_RM,concat(c.user_firstname,' ',c.user_lastname, ' / ', c.user_code) as riskmanager_name from  rsk_mst_trmmapping a " +
                    " left join hrm_mst_temployee b on a.assigned_RM = b.employee_gid " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " where zonalmapping_gid = '" + lszonalmapping_gid + "' and assigned_RM!='' group by assigned_RM";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getzonalallocationcount = new List<zonalallocationcount>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    msSQL = " select a.count_fresh,b.count_revisit,c.count_overall from " +
                          " (select count(*) as count_fresh from rsk_trn_tallocationdtl " +
                          " where qualified_status = 'Fresh' and zonal_gid = '" + lszonalmapping_gid + "' " +
                          " and allocation_assignedRM = '" + dr_datarow["assigned_RM"].ToString() + "' and allocation_status = 'Allocated') as a, " +
                          " (select count(*) as count_revisit from rsk_trn_tallocationdtl " +
                          " where qualified_status = 'Re-Visit'  and zonal_gid = '" + lszonalmapping_gid + "' " +
                          " and allocation_assignedRM = '" + dr_datarow["assigned_RM"].ToString() + "' and allocation_status = 'Allocated') as b, " +
                          " (select count(*) as count_overall from rsk_trn_tallocationdtl where zonal_gid = '" + lszonalmapping_gid + "' " +
                          " and allocation_assignedRM = '" + dr_datarow["assigned_RM"].ToString() + "' and allocation_status = 'Allocated') as c";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lsallocation_pendingcount = Convert.ToInt16(objODBCDatareader["count_overall"].ToString());
                        lscount_fresh = objODBCDatareader["count_fresh"].ToString();
                        lscount_revisit = objODBCDatareader["count_revisit"].ToString();
                    }
                    objODBCDatareader.Close();

                    if (lsallocation_pendingcount > 12)
                    {
                        lscount_status = "Y";
                    }
                    else
                    {
                        lscount_status = "N";
                    }
                    getzonalallocationcount.Add(new zonalallocationcount
                    {
                        riskmanager_name = dr_datarow["riskmanager_name"].ToString(),
                        assigned_RM = dr_datarow["assigned_RM"].ToString(),
                        pending_count = lsallocation_pendingcount,
                        count_status = lscount_status,
                        count_fresh = lscount_fresh,
                        count_revisit = lscount_revisit,
                    });
                }
                values.zonalallocationcount = getzonalallocationcount;
            }
            dt_datatable.Dispose();

            msSQL = " select a.allocationdtl_gid,concat(e.user_firstname,' ', e.user_lastname, ' / ', e.user_code) as VisitRM, " +
                  " concat(customer_urn, ' / ', customername) as customer_name,schedulelog_gid, " +
                  " date_format(appointment_date, '%d-%m-%Y') as appointmentdate, " +
                  " appointment_time,appointment_status,appointment_remarks " +
                  " from rsk_trn_tschedulelog a " +
                  " left join ocs_mst_tcustomer b on b.customer_gid = a.customer_gid " +
                  " left join rsk_trn_tallocationdtl c on c.allocationdtl_gid = a.allocationdtl_gid " +
                  " left join hrm_mst_temployee d on a.created_by = d.employee_gid " +
                  " left join adm_mst_tuser e on d.user_gid = e.user_gid " +
                  " where month(appointment_date) = month(curdate()) and c.allocation_zonalRM = '" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmonthlylist = new List<monthlyactivity>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmonthlylist.Add(new monthlyactivity
                    {
                        scheduled_date = (dr_datarow["appointmentdate"].ToString()),
                        scheduled_time = (dr_datarow["appointment_time"].ToString()),
                        remarks = dr_datarow["appointment_remarks"].ToString(),
                        customer_name = dr_datarow["customer_name"].ToString(),
                        VisitRM = dr_datarow["VisitRM"].ToString(),

                    });
                }
                values.monthlyactivity = getmonthlylist;
            }
            dt_datatable.Dispose();

            msSQL = " select a.count_current,b.count_upcoming,g.count_breached,c.count_completed,d.count_external,f.count_reportchanges,k.count_qualified from " +
                   " (select count(*) as count_current from rsk_trn_tallocationdtl where allocation_status = 'Allocated' and visit_allocated_date <= NOW()" +
                  " and zonal_gid = (select zonalmapping_gid from rsk_mst_tzonalmapping where zonalrisk_managerGid = '" + employee_gid + "'))as a," +
                  " (select count(*) as count_upcoming from rsk_trn_tallocationdtl where allocation_status = 'Allocated' and visit_allocated_date > NOW()" +
                  " and zonal_gid = (select zonalmapping_gid from rsk_mst_tzonalmapping where zonalrisk_managerGid = '" + employee_gid + "'))as b," +
                  " (select count(*) as count_completed from rsk_trn_tallocation where status = 'Completed'" +
                  " and zonal_gid = (select zonalmapping_gid from rsk_mst_tzonalmapping where zonalrisk_managerGid = '" + employee_gid + "'))as c," +
                  " (select count(*) as count_external from rsk_trn_tallocationdtl where allocation_status = 'External'" +
                  " and zonal_gid = (select zonalmapping_gid from rsk_mst_tzonalmapping where zonalrisk_managerGid = '" + employee_gid + "'))as d," +
                  " (select count(*) as count_pending from rsk_trn_tallocation where status = 'Pending'" +
                  " and zonal_gid = (select zonalmapping_gid from rsk_mst_tzonalmapping where zonalrisk_managerGid = '" + employee_gid + "'))as e ," +
                  " (select count(*) as count_reportchanges from rsk_trn_tallocation where reportcancel_flag = 'Y'" +
                  " and zonal_gid = (select zonalmapping_gid from rsk_mst_tzonalmapping where zonalrisk_managerGid = '" + employee_gid + "'))as f," +
                  " (select count(*) as count_breached from rsk_trn_tallocationdtl where DATEDIFF(CURDATE(), visit_allocated_date) > 90 and lastvisit_date is null" +
                  " and zonal_gid = (select zonalmapping_gid from rsk_mst_tzonalmapping where zonalrisk_managerGid = '" + employee_gid + "'))as g," +
                  " (select count(h.customer_urn) as count_qualified from ocs_mst_tcustomer h " +
                  " left join rsk_trn_tcustomervisit i on h.customer_urn = i.customer_urn " +
                  " left join rsk_trn_tcustomerdisbursement c on h.customer_urn = c.customer_urn " +
                  " where zonal_gid in (select zonalmapping_gid from rsk_mst_tzonalmapping where zonalrisk_managerGid = '" + employee_gid + "') and " +
                  " (((DATEDIFF(CURDATE(), first_disb_date)) > 31 and c.allocate_flag = 'N' and c.exclusion_flag='N' and lastvisit_date is null  and ac_status = '0') or " +
                  " ((DATEDIFF(CURDATE(), lastvisit_date)) > 180 and ac_status = '0' and c.allocate_flag = 'N' and c.exclusion_flag='N')) " +
                  " and total_sanction >= 5000000) as k ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.count_current = objODBCDatareader["count_current"].ToString();
                values.count_upcoming = objODBCDatareader["count_upcoming"].ToString();
                values.count_completed = objODBCDatareader["count_completed"].ToString();
                values.count_external = objODBCDatareader["count_external"].ToString();
                values.count_breached = objODBCDatareader["count_breached"].ToString();
                values.count_reportchanges = objODBCDatareader["count_reportchanges"].ToString();
                values.count_qualified = objODBCDatareader["count_qualified"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " select date_format(process_date,'%d-%m-%Y') as process_date, " +
                   " concat(c.user_firstname, ' ', c.user_lastname, '/', c.user_code) as employee_name " +
                   " from lgl_trn_tmisdocumentimport a " +
                   " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                   " left join adm_mst_tuser c on b.user_gid = c.user_gid " +
                   " order by misdocumentimport_gid desc limit 1 ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.ADM_updatedby = objODBCDatareader["employee_name"].ToString();
                values.ADM_updateddate = objODBCDatareader["process_date"].ToString();
            }
            objODBCDatareader.Close();
            return true;
        }


        public bool DaGetZRMCalenderDtl(calendarevent values, string employee_gid)
        {
            msSQL = " select concat(e.user_firstname,' ',e.user_lastname, ' / ', e.user_code) as VisitRM, " +
                   " concat(customername, ' / ', customer_urn) as customer_name, " +
                   " date_format(appointment_date, '%Y-%m-%d') as appointmentdate, " +
                   " appointment_time  " +
                   " from rsk_trn_tschedulelog a " +
                   " left join ocs_mst_tcustomer b on b.customer_gid = a.customer_gid " +
                   " left join rsk_trn_tallocationdtl c on c.allocationdtl_gid = a.allocationdtl_gid " +
                   " left join hrm_mst_temployee d on a.created_by = d.employee_gid " +
                   " left join adm_mst_tuser e on d.user_gid = e.user_gid " +
                   " where c.allocation_zonalRM = '" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var geteventlist = new List<createevent>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    geteventlist.Add(new createevent
                    {
                        event_date = Convert.ToDateTime(dr_datarow["appointmentdate"].ToString()),
                        event_time = (Convert.ToDateTime(dr_datarow["appointment_time"].ToString())),
                        event_title = dr_datarow["customer_name"].ToString(),
                    });
                }
                values.createevent = geteventlist;
            }
            dt_datatable.Dispose();
            return true;
        }

        public bool DaGetExclusionCustomer(string customer_urn, string exclusion_reason, string user_gid, result values)
        {

            msSQL = " select customerdisb_gid,customer_name from rsk_trn_tcustomerdisbursement where customer_urn='" + customer_urn + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                string lscustomerdisb_gid = objODBCDatareader["customerdisb_gid"].ToString();
                string lscustomer_name = objODBCDatareader["customer_name"].ToString();
                objODBCDatareader.Close();
                string msGet_Gid = objcmnfunctions.GetMasterGID("EXCH");

                msSQL = "insert into rsk_trn_texclusionhistory(" +
                       " exclusion_historygid ," +
                       " customerdisb_gid," +
                       " customer_urn," +
                       " customer_name," +
                       " excluded_status," +
                       " excluded_stage ," +
                       " exclusion_reason," +
                       " created_by," +
                       " created_date)" +
                       " values(" +
                       "'" + msGet_Gid + "'," +
                       "'" + lscustomerdisb_gid + "'," +
                       "'" + customer_urn + "'," +
                       "'" + lscustomer_name + "'," +
                       "'Excluded'," +
                       "'Qualified Case'," +
                       "'" + exclusion_reason.Replace("'", "\\'") + "'," +
                       "'" + user_gid + "'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update rsk_trn_tcustomerdisbursement" +
                        " set exclusion_flag ='Y', " +
                        " exclusion_updatedby='" + user_gid + "'," +
                        " exclusion_updateddate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
                        " where customerdisb_gid = '" + lscustomerdisb_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            }
            else
            {
                objODBCDatareader.Close();
                values.message = "No Records Found..!";
                values.status = false;
                return false;
            }

            if (mnResult != 0)
            {
                values.message = "Customer Excluded Successfully..!";
                values.status = true;
                return true;
            }
            else
            {
                values.message = "Error Occured..!";
                values.status = false;
                return false;
            }

        }


        public bool DaGetExclusionZonalSummary(string employee_gid, exclusioncustomerlist values)
        {
            msSQL = " select a.customer_urn,case when(DATEDIFF(CURDATE(), lastvisit_date) > 180 and c.last_disb_date > lastvisit_date) then 'Re-Visit' " +
                      " when(DATEDIFF(CURDATE(), lastvisit_date) > 180 and c.last_disb_date < lastvisit_date) then 'Re-Visit'" +
                      " else 'Fresh' end as qualified_status ,a.customername,format(a.total_sanction, 2) as total_sanction," +
                      " case when(b.lastvisit_date is null) then DATEDIFF(CURDATE(), c.first_disb_date) else" +
                      " DATEDIFF(CURDATE(), c.last_disb_date) end as daypassed_disbursement," +
                      " DATEDIFF(CURDATE(), b.lastvisit_date) as daypassed_visit," +
                      " case when(DATEDIFF(CURDATE(), lastvisit_date) > 180) then date_format(c.last_disb_date, '%d-%m-%Y')" +
                      " else date_format(c.first_disb_date, '%d-%m-%Y') end as disbursement_date," +
                      " date_format(lastvisit_date, '%d-%m-%Y') as lastvisit_date, " +
                      " date_format(exclusion_updateddate, '%d-%m-%Y') as exclusion_updateddate, " +
                      " concat(d.user_firstname,' ',d.user_lastname,'/',d.user_code) as excludedby " +
                      " from ocs_mst_tcustomer a" +
                      " left join rsk_trn_tcustomervisit b on a.customer_urn = b.customer_urn" +
                      " left join rsk_trn_tcustomerdisbursement c on a.customer_urn = c.customer_urn" +
                      " left join adm_mst_tuser d on c.exclusion_updatedby=d.user_gid " +
                      " where zonal_gid in (select zonalmapping_gid from rsk_mst_tzonalmapping where zonalrisk_managerGid = '" + employee_gid + "')" +
                      " and c.exclusion_flag='Y'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_mappingdtl = new List<exclusioncustomer>();
            if (dt_datatable.Rows.Count != 0)
            {
                values.exclusioncustomer = dt_datatable.AsEnumerable().Select(row => new exclusioncustomer
                {
                    customer_urn = row["customer_urn"].ToString(),
                    customer_name = row["customername"].ToString(),
                    excluded_date = row["exclusion_updateddate"].ToString(),
                    excluded_by = row["excludedby"].ToString(),
                    qualified_status = row["qualified_status"].ToString(),
                    total_sanction = row["total_sanction"].ToString(),
                    disbursement_date = row["disbursement_date"].ToString(),
                    daypassed_disbursement = row["daypassed_disbursement"].ToString(),
                    lastvisit_date = row["lastvisit_date"].ToString(),
                    daypassed_visit = row["daypassed_visit"].ToString(),
                }).ToList();
            }
            dt_datatable.Dispose();
            return true;
        }

        public bool DaGetExclusionCustomerHistory(string customer_urn, exclusionhistorylist values)
        {
            msSQL = " select date_format(a.created_date, '%d-%m-%Y') as exclusion_updateddate,excluded_status,excluded_stage, " +
                     " concat(b.user_firstname,' ',b.user_lastname,'/',b.user_code) as excludedby,exclusion_reason " +
                     " from rsk_trn_texclusionhistory a" +
                     " left join adm_mst_tuser b on a.created_by=b.user_gid " +
                     " where a.customer_urn='" + customer_urn + "'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_mappingdtl = new List<exclusionhistory>();
            if (dt_datatable.Rows.Count != 0)
            {
                values.exclusionhistory = dt_datatable.AsEnumerable().Select(row => new exclusionhistory
                {
                    excluded_date = row["exclusion_updateddate"].ToString(),
                    excluded_by = row["excludedby"].ToString(),
                    excluded_status = row["excluded_status"].ToString(),
                    excluded_stage = row["excluded_stage"].ToString(),
                    exclusion_reason = row["exclusion_reason"].ToString(),

                }).ToList();
            }
            dt_datatable.Dispose();
            return true;
        }


        public bool DaGetActivationCustomer(string customer_urn, string exclusion_reason, string user_gid, result values)
        {

            msSQL = " select customerdisb_gid,customer_name from rsk_trn_tcustomerdisbursement where customer_urn='" + customer_urn + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                string lscustomerdisb_gid = objODBCDatareader["customerdisb_gid"].ToString();
                string lscustomer_name = objODBCDatareader["customer_name"].ToString();
                objODBCDatareader.Close();
                string msGet_Gid = objcmnfunctions.GetMasterGID("EXCH");

                msSQL = "insert into rsk_trn_texclusionhistory(" +
                       " exclusion_historygid ," +
                       " customerdisb_gid," +
                       " customer_urn," +
                       " customer_name," +
                       " excluded_status," +
                       " exclusion_reason," +
                       " created_by," +
                       " created_date)" +
                       " values(" +
                       "'" + msGet_Gid + "'," +
                       "'" + lscustomerdisb_gid + "'," +
                       "'" + customer_urn + "'," +
                       "'" + lscustomer_name + "'," +
                       "'Activated'," +
                       "'" + exclusion_reason.Replace("'", "\\'") + "'," +
                       "'" + user_gid + "'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update rsk_trn_tcustomerdisbursement" +
                        " set exclusion_flag ='N', " +
                        " exclusion_updatedby='" + user_gid + "'," +
                        " exclusion_updateddate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
                        " where customerdisb_gid = '" + lscustomerdisb_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            }
            else
            {
                objODBCDatareader.Close();
                values.message = "No Records Found..!";
                values.status = false;
                return false;
            }

            if (mnResult != 0)
            {
                values.message = "Customer Activated Successfully..!";
                values.status = true;
                return true;
            }
            else
            {
                values.message = "Error Occured..!";
                values.status = false;
                return false;
            }

        }
    }
}