using ems.lgl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Odbc;
using ems.utilities.Functions;
using System.IO;
using System.Configuration;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;
using ems.storage.Functions;
namespace ems.lgl.DataAccess
{
    public class DaLglDashboard
    {

        ems.utilities.Functions.dbconn objdbconn = new ems.utilities.Functions.dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        DataTable dt_datatable;
        string msSQL;
        string lspath;
        string lsname;

        public bool DaGetGID(mdllglDashboard values, string user_gid)
        {

            msSQL = "select a.module_gid from adm_mst_tmodule a" +
            " left join adm_mst_tprivilege b on b.module_gid = a.module_gid where" +
            " a.module_gid_parent = 'LGLLER' and" +
            " b.user_gid = '" + user_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getGID = new List<lawyer_empanelment>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getGID.Add(new lawyer_empanelment
                    {
                        privilege_gid = dr_datarow["module_gid"].ToString()
                    });
                }
                values.lawyer_empanelment = getGID;
            }
            dt_datatable.Dispose();

            msSQL = "select a.module_gid from adm_mst_tmodule a" +
           " left join adm_mst_tprivilege b on b.module_gid = a.module_gid where" +
           " a.module_gid_parent = 'LGLDCM' and" +
           " b.user_gid = '" + user_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getlegal_services = new List<legal_services>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getlegal_services.Add(new legal_services
                    {
                        legalservices_gid = dr_datarow["module_gid"].ToString()
                    });
                }
                values.legal_services = getlegal_services;
            }
            dt_datatable.Dispose();
            msSQL = "select a.module_gid from adm_mst_tmodule a" +
          " left join adm_mst_tprivilege b on b.module_gid = a.module_gid where" +
          " a.module_gid_parent = 'LGLLCM' and" +
          " b.user_gid = '" + user_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getlegal_compliance = new List<legal_compliance>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getlegal_compliance.Add(new legal_compliance
                    {
                        legalcompliance_gid = dr_datarow["module_gid"].ToString()
                    });
                }
                values.legal_compliance = getlegal_compliance;
            }
            dt_datatable.Dispose();
            values.status = true;
            return true;
        }
        //public bool DaGetdntrackerReport(string date, MdlLglReport values)
        //{

        //    msSQL = "(select b.customername,a.urn,vertical_code,case " +
        //           " when dn1generated_date like '" + date + "%' then 'DN1 Generated'" +
        //           " when dn2generated_date like '" + date + "%' then 'DN2 Generated'" +
        //           " when dn3generated_date like '" + date + "%' then 'DN3 Generated' end as dnstatus,  " +
        //           " case when dn1generated_date like '" + date + "%' then concat(n.user_firstname, ' ', n.user_lastname, '/', n.user_code)  " +
        //           " when dn2generated_date like '" + date + "%' then concat(o.user_firstname, ' ', o.user_lastname, '/', o.user_code)  " +
        //           " when dn3generated_date like '" + date + "%' then concat(p.user_firstname, ' ', p.user_lastname, '/', p.user_code) end as dngenerated_by,  " +
        //           " '---' as dnsent_by,'---' as courierref_no,'---' as courier_center,'---' as courier_date,'---' as courier_status, '---' as delivered_date,'---' as returned_date, " +
        //           " case when dn1generated_date like '" + date + "%' then date_format(dn1sanction_date,'%d-%m-%Y')  " +
        //           " when dn2generated_date like '" + date + "%' then date_format(dn2sanction_date,'%d-%m-%Y')  " +
        //           " when dn3generated_date like '" + date + "%' then date_format(dn3sanction_date,'%d-%m-%Y')end as sanction_date,  " +
        //           " case when dn1generated_date like '" + date + "%' then dn1sanction_amount" +
        //           " when dn2generated_date like '" + date + "%' then dn2sanction_amount" +
        //           " when dn3generated_date like '" + date + "%' then dn3sanction_amount end as sanction_amount,  " +
        //           " case when dn1generate_date like '" + date + "%' then date_format(dn1generate_date,'%d-%m-%Y') " +
        //           " when dn2generate_date like '" + date + "%' then date_format(dn1generate_date,'%d-%m-%Y')" +
        //           " when dn3generate_date like '" + date + "%' then date_format(dn3generate_date,'%d-%m-%Y')end as dngenerate_date ,'---' as dnsent_date" +
        //           " from lgl_tmp_tdnformat x" +
        //           " left join lgl_trn_tdncases a on a.tempdn1format_gid = x.tempdn1format_gid" +
        //           " left join ocs_mst_tcustomer b on x.customer_urn = b.customer_urn" +
        //           " left join hrm_mst_temployee c on c.employee_gid = a.dn1status_created_by" +
        //           " left join hrm_mst_temployee d on d.employee_gid = a.dn2status_updated_by" +
        //           " left join hrm_mst_temployee e on e.employee_gid = a.dn3status_updated_by" +
        //           " left join adm_mst_tuser f on f.user_gid = c.user_gid" +
        //           " left join adm_mst_tuser g on g.user_gid = d.user_gid" + 
        //           " left join adm_mst_tuser h on h.user_gid = e.user_gid" +
        //           " left join lgl_trn_tsanctiondtl i on x.customer_urn = i.customer_urn" +
        //           " left join lgl_trn_tcourierdetails j on a.dncase_gid = j.dncase_gid" +
        //           " left join hrm_mst_temployee l on l.employee_gid = x.dn1generated_by" +
        //           " left join hrm_mst_temployee k on k.employee_gid = x.dn2generated_by" +
        //           " left join hrm_mst_temployee m on m.employee_gid = x.dn3generated_by" +
        //           " left join adm_mst_tuser n on n.user_gid = l.user_gid" +
        //           " left join adm_mst_tuser o on o.user_gid = k.user_gid" +
        //           " left join adm_mst_tuser p on p.user_gid = m.user_gid" +
        //           " where (dn3generated_date like '" + date + "%' or dn1generated_date like '" + date + "%' or" +
        //           " dn2generated_date like '" + date + "%') and x.customer_urn not in (select urn from lgl_trn_tdncases where status <> 'Closed') group by x.tempdn1format_gid) UNION" +
        //           " (select b.customername, a.urn, vertical_code,case when dn1status_created_date like '" + date + "%' then dn1status" +
        //           " when dn2status_updated_date like '" + date + "%' then dn2status" +
        //           " when dn3status_updated_date like '" + date + "%' then dn3status end as dnstatus," +
        //           " case when x.dn1generated_date like '" + date + "%' then concat(n.user_firstname, ' ', n.user_lastname, '/', n.user_code)" +
        //           " when x.dn2generated_date like '" + date + "%' then concat(o.user_firstname, ' ', o.user_lastname, '/', o.user_code)" +
        //           " when x.dn3generated_date like '" + date + "%' then concat(p.user_firstname, ' ', p.user_lastname, '/', p.user_code) end as dngenerated_by," +
        //           " case when dn1status_created_date like '" + date + "%' then concat(f.user_firstname, ' ', f.user_lastname, '/', f.user_code)" +
        //           " when dn2status_updated_date like '" + date + "%' then concat(g.user_firstname, ' ', g.user_lastname, '/', g.user_code)" +
        //           " when dn3status_updated_date like '" + date + "%' then concat(h.user_firstname, ' ', h.user_lastname, '/', h.user_code) end as dnsent_by," +
        //           " case when dn1status_created_date like '" + date + "%' then courier_refno" +
        //           " when dn2status_updated_date like '" + date + "%' then dn2courier_refno" +
        //           " when dn3status_updated_date like '" + date + "%' then dn3courier_refno end as courierref_no," +
        //           " case when dn1status_created_date like '" + date + "%' then courier_center" +
        //           " when dn2status_updated_date like '" + date + "%' then dn2courier_center" +
        //           " when dn3status_updated_date like '" + date + "%' then dn3courier_center end as courier_center," +
        //           " case when dn1status_created_date like '" + date + "%' then date_format(courier_date, '%d-%m-%Y')" +
        //           " when dn2status_updated_date like '" + date + "%' then date_format(dn2courier_date, '%d-%m-%Y')" +
        //           " when dn3status_updated_date like '" + date + "%' then date_format(dn3courier_date, '%d-%m-%Y') end as courier_date," +
        //           " case when dn1status_created_date like '" + date + "%' then j.status" +
        //           " when dn2status_updated_date like '" + date + "%' then dn2courier_status" +
        //           " when dn3status_updated_date like '" + date + "%' then dn3courier_status end as courier_status," +
        //           " case when dn1status_created_date like '" + date + "%' then delivered_date" +
        //           " when dn2status_updated_date like '" + date + "%' then dn2delivered_date" +
        //           " when dn3status_updated_date like '" + date + "%' then dn3delivered_date end as delivered_date," +
        //           " case when dn1status_created_date like '" + date + "%' then returened_date" +
        //           " when dn2status_updated_date like '" + date + "%' then dn2returened_date" +
        //           " when dn3status_updated_date like '" + date + "%' then dn3returened_date end as returned_date," +
        //           " case when dn1status_created_date like '" + date + "%' then date_format(dn1sanction_date, '%d-%m-%Y')" +
        //           " when dn2status_updated_date like '" + date + "%' then date_format(dn2sanction_date, '%d-%m-%Y')" +
        //           " when dn3status_updated_date like '" + date + "%' then date_format(dn3sanction_date, '%d-%m-%Y') end as sanction_date," +
        //           " case when dn1status_created_date like '" + date + "%' then dn1sanction_amount" +
        //           " when dn1status_created_date like '" + date + "%' then dn2sanction_amount" +
        //           " when dn3status_updated_date like '" + date + "%' then dn3sanction_amount end as sanction_amount,"+
        //          " case when dn1status_created_date like '" + date + "%' then date_format(dn1generate_date,'%d-%m-%Y') " +
        //           " when dn2status_updated_date like '" + date + "%' then date_format(dn1generate_date,'%d-%m-%Y')" +
        //           " when dn3status_updated_date like '" + date + "%' then date_format(dn3generate_date,'%d-%m-%Y') end as dngenerate_date," +
        //           " case when dn1status_created_date like '" + date + "%' then date_format(dn1status_created_date, '%d-%m-%Y')" +
        //           " when dn2status_updated_date like '" + date + "%' then date_format(dn2status_updated_date, '%d-%m-%Y')" +
        //           " when dn2status_updated_date like '" + date + "%' then date_format(dn2status_updated_date, '%d-%m-%Y') end as dnsent_date" +
        //           " from lgl_trn_tdncases a" +
        //           " left join lgl_tmp_tdnformat x on a.tempdn1format_gid = x.tempdn1format_gid" +
        //           " left join ocs_mst_tcustomer b on a.urn = b.customer_urn" +
        //           " left join hrm_mst_temployee c on c.employee_gid = a.dn1status_created_by" +
        //           " left join hrm_mst_temployee d on d.employee_gid = a.dn2status_updated_by" +
        //           " left join hrm_mst_temployee e on e.employee_gid = a.dn3status_updated_by" +
        //           " left join adm_mst_tuser f on f.user_gid = c.user_gid" +
        //           " left join adm_mst_tuser g on g.user_gid = d.user_gid" +
        //           " left join adm_mst_tuser h on h.user_gid = e.user_gid" +
        //           " left join lgl_trn_tsanctiondtl i on a.urn = i.customer_urn" +
        //            " left join lgl_trn_tcourierdetails j on a.dncase_gid = j.dncase_gid" +
        //            " left join hrm_mst_temployee l on l.employee_gid = x.dn1generated_by" +
        //            " left join hrm_mst_temployee k on k.employee_gid = x.dn2generated_by" +
        //            " left join hrm_mst_temployee m on m.employee_gid = x.dn3generated_by" +
        //            " left join adm_mst_tuser n on n.user_gid = l.user_gid" +
        //            " left join adm_mst_tuser o on o.user_gid = k.user_gid" +
        //            " left join adm_mst_tuser p on p.user_gid = m.user_gid" +
        //            " where dn1status_created_date like '" + date + "%' or dn2status_updated_date like '" + date + "%' or" +
        //            " dn3status_updated_date like '" + date + "%' group by a.urn)";

        //    dt_datatable = objdbconn.GetDataTable(msSQL);
        //    var getdn_list = new List<dn_list>();
        //    if (dt_datatable.Rows.Count != 0)
        //    {
        //        foreach (DataRow dr_datarow in dt_datatable.Rows)
        //        {
        //            getdn_list.Add(new dn_list
        //            {
        //                customer_name = (dr_datarow["customername"].ToString()),
        //                urn = (dr_datarow["urn"].ToString()),
        //                vertical = (dr_datarow["vertical_code"].ToString()),
        //                status = dr_datarow["dnstatus"].ToString(),
        //                dngenerated_by = dr_datarow["dngenerated_by"].ToString(),
        //                dnsent_by = dr_datarow["dnsent_by"].ToString(),
        //                courierref_no = (dr_datarow["courierref_no"].ToString()),
        //                courier_center = (dr_datarow["courier_center"].ToString()),
        //                courier_date = (dr_datarow["courier_date"].ToString()),
        //                courier_status = dr_datarow["courier_status"].ToString(),
        //                delivered_date = dr_datarow["delivered_date"].ToString(),
        //                returned_date = dr_datarow["returned_date"].ToString(),
        //                sanction_amount = dr_datarow["sanction_amount"].ToString(),
        //                sanction_date = dr_datarow["sanction_date"].ToString(),
        //                dngenerate_date = dr_datarow["dngenerate_date"].ToString(),
        //                dnsent_date= dr_datarow["dnsent_date"].ToString(),
        //            });
        //        }
        //        values.dn_list = getdn_list;
        //    }
        //    dt_datatable.Dispose();


        //    msSQL = "select count(*) as dn1send_count from lgl_trn_tdncases where dn1status_created_date like'" + date + "%' and dn1status='DN1 Sent'";
        //    values.dn1send_count = objdbconn.GetExecuteScalar(msSQL);

        //    msSQL = "select count(*) as dn2send_count from lgl_trn_tdncases where dn2status_updated_date like '" + date + "%' and dn2status='DN2 Sent'";
        //    values.dn2send_count = objdbconn.GetExecuteScalar(msSQL);

        //    msSQL = "select count(*) as dn3send_count from lgl_trn_tdncases where dn3status_updated_date like'" + date + "%' and dn3status='DN3 Sent'";
        //    values.dn3send_count = objdbconn.GetExecuteScalar(msSQL);

        //    msSQL = "select count(*) as dn1send_count from lgl_trn_tdncases where dn1status_created_date like'" + date + "%' and dn1status='DN1 Skip'";
        //    values.dn1skip_count = objdbconn.GetExecuteScalar(msSQL);

        //    msSQL = "select count(*) as dn2send_count from lgl_trn_tdncases where dn2status_updated_date like'" + date + "%' and dn2status='DN2 Skip'";
        //    values.dn2skip_count = objdbconn.GetExecuteScalar(msSQL);

        //    msSQL = "select count(*) as dn3send_count from lgl_trn_tdncases where dn3status_updated_date like'" + date + "%' and dn3status='DN3 Skip'";
        //    values.dn3skip_count = objdbconn.GetExecuteScalar(msSQL);

        //    values.status = true;
        //    return true;
        //}
        public bool DaGetdntrackerReport(string date, MdlLglReport values)
        {
            msSQL = " select a.customer_urn,x.customername,x.vertical_code,case when o.dn1sanctionref_no is null then '---' else o.dn1sanctionref_no end as dn1sanctionref_no," +
                   " case when o.dn2sanctionref_no is null then '---' else o.dn2sanctionref_no end as dn2sanctionref_no," +
                   " case when o.dn3sanctionref_no is null then '---' else o.dn3sanctionref_no end as dn3sanctionref_no," +
                   " case when o.dn1sanction_date is null  then '---' else date_format(o.dn1sanction_date,'%d-%m-%Y') end as dn1sanction_date," +
                   " case when o.dn2sanction_date is null  then '---' else date_format(o.dn2sanction_date, '%d-%m-%Y') end as dn2sanction_date," +
                   " case when o.dn3sanction_date is null  then '---' else date_format(o.dn3sanction_date, '%d-%m-%Y') end as dn3sanction_date," +
                   " case when o.dn1sanction_amount is null  then '---' else format(o.dn1sanction_amount, 2) end as dn1sanction_amount," +
                   " case when o.dn2sanction_amount is null  then '---' else  format(o.dn2sanction_amount, 2) end as dn2sanction_amount," +
                   " case when o.dn3sanction_amount is null  then '---' else format(o.dn3sanction_amount, 2) end  as dn3sanction_amount," +
                   " case when dn1generated_date is null  then '---' else date_format(dn1generated_date, '%d-%m-%Y') end as dn1generated_date," +
                   " case when dn2generated_date is null  then '---' else date_format(dn2generated_date, '%d-%m-%Y') end as dn2generated_date," +
                   " case when dn3generated_date is null  then '---' else date_format(dn3generated_date, '%d-%m-%Y') end as dn3generated_date, " +
                   " case when b.dn1status_created_by is null  then '---' else concat(f.user_firstname, ' ', f.user_lastname, '/', f.user_code) end as dn1sent_by," +
                   " case when b.dn2status_updated_by is null  then '---' else concat(g.user_firstname, ' ', g.user_lastname, '/', g.user_code) end as dn2sent_by," +
                   " case when b.dn3status_updated_by is null  then '---' else concat(h.user_firstname, ' ', h.user_lastname, '/', h.user_code) end as dn3sent_by," +
                   " case when dn1status_created_date is null  then '---' else date_format(dn1status_created_date, '%d-%m-%Y') end as dn1sent_date," +
                   " case when dn2status_updated_date is null  then '---' else date_format(dn2status_updated_date, '%d-%m-%Y') end as dn2sent_date," +
                   " case when dn2status_updated_date is null  then '---' else date_format(dn3status_updated_date, '%d-%m-%Y') end as dn3sent_date," +
                   " case when a.dn1generated_by is null  then '---' else concat(l.user_firstname, ' ', l.user_lastname, '/', l.user_code) end as dn1generated_by," +
                   " case when a.dn2generated_by is null  then '---' else concat(m.user_firstname, ' ', m.user_lastname, '/', m.user_code) end as dn2generated_by," +
                   " case when a.dn3generated_by is null  then '---' else concat(n.user_firstname, ' ', n.user_lastname, '/', n.user_code) end as dn3generated_by," +
                   " case when courier_refno is null  then '---' else courier_refno end as dn1courier_refno," +
                   " case when dn2courier_refno is null  then '---' else dn2courier_refno end as dn2courier_refno," +
                   " case when dn3courier_refno is null  then '---' else dn3courier_refno end as dn3courier_refno ," +
                   " case when courier_center is null  then '---' else courier_center end as dn1courier_center," +
                   " case when dn2courier_center is null  then '---' else dn2courier_center end as dn2courier_center," +
                   " case when dn3courier_center is null  then '---' else dn3courier_center end as dn3courier_center," +
                   " case when courier_date is null  then '---' else date_format(courier_date, '%d-%m-%Y') end as dn1courier_date," +
                   " case when dn2courier_date is null  then '---' else date_format(dn2courier_date, '%d-%m-%Y') end as dn2courier_date," +
                   " case when dn3courier_date is null  then '---' else date_format(dn3courier_date, '%d-%m-%Y') end as dn3courier_date," +
                   " case when delivered_date is null  then '---' else date_format(delivered_date, '%d-%m-%Y') end as dn1delivered_date, " +
                   " case when dn2delivered_date is null  then '---' else date_format(dn2delivered_date, '%d-%m-%Y') end as dn2delivered_date, " +
                   " case when dn3delivered_date is null  then '---' else date_format(dn3delivered_date, '%d-%m-%Y') end as dn3delivered_date, " +
                   " case when returened_date is null  then '---' else date_format(returened_date, '%d-%m-%Y') end as dn1returened_date, " +
                   " case when dn2returened_date is null  then '---' else date_format(dn2returened_date, '%d-%m-%Y') end as dn2returened_date, " +
                   " case when dn3returened_date is null  then '---' else date_format(dn3returened_date, '%d-%m-%Y') end as dn3returened_date, " +
                   " couriered_by as dn1couriered_by, dn2couriered_by,dn3couriered_by,p.status as dn1courier_status,dn2courier_status,dn3courier_status, " +
                   " case when dn3status_updated_date is not null then dn3status" +
                   " when dn2status_updated_date  is not null then dn2status" +
                   " when dn1status_created_date  is not null then dn1status" +
                   " when dn3generated_date  is not null then 'DN3 Generated'" +
                   " when dn2generated_date  is not null then 'DN2 Generated'" +
                   " when dn1generated_date  is not null then 'DN1 Generated' end as dnstatus" +
                   " from lgl_tmp_tdnformat a" +
                   " left join lgl_trn_tdncases b on a.tempdn1format_gid = b.tempdn1format_gid" +
                   " left join ocs_mst_tcustomer x on a.customer_urn = x.customer_urn" +
                   " left join hrm_mst_temployee c on c.employee_gid = b.dn1status_created_by" +
                    " left join hrm_mst_temployee d on d.employee_gid = b.dn2status_updated_by" +
                    " left join hrm_mst_temployee e on e.employee_gid = b.dn3status_updated_by" +
                    " left join adm_mst_tuser f on f.user_gid = c.user_gid" +
                    " left join adm_mst_tuser g on g.user_gid = d.user_gid" +
                    " left join adm_mst_tuser h on h.user_gid = e.user_gid" +
                    " left join hrm_mst_temployee i on i.employee_gid = a.dn1generated_by" +
                    " left join hrm_mst_temployee j on j.employee_gid = a.dn2generated_by" +
                    " left join hrm_mst_temployee k on k.employee_gid = a.dn3generated_by" +
                    " left join adm_mst_tuser l on l.user_gid = i.user_gid" +
                    " left join adm_mst_tuser m on m.user_gid = j.user_gid" +
                    " left join adm_mst_tuser n on n.user_gid = k.user_gid" +
                    " left join lgl_trn_tsanctiondtl o on a.customer_urn = o.customer_urn" +
                    " left join lgl_trn_tcourierdetails p on b.dncase_gid = p.dncase_gid" +
                    "  group by a.tempdn1format_gid";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdn_list = new List<dn_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getdn_list.Add(new dn_list
                    {
                        customer_name = (dr_datarow["customername"].ToString()),
                        customer_urn = (dr_datarow["customer_urn"].ToString()),
                        vertical = (dr_datarow["vertical_code"].ToString()),
                        dn1sanctionref_no = dr_datarow["dn1sanctionref_no"].ToString(),
                        dn2sanctionref_no = dr_datarow["dn2sanctionref_no"].ToString(),
                        dn3sanctionref_no = dr_datarow["dn3sanctionref_no"].ToString(),
                        dn1sanction_date = (dr_datarow["dn1sanction_date"].ToString()),
                        dn2sanction_date = (dr_datarow["dn2sanction_date"].ToString()),
                        dn3sanction_date = (dr_datarow["dn3sanction_date"].ToString()),
                        dn1sanction_amount = dr_datarow["dn1sanction_amount"].ToString(),
                        dn2sanction_amount = dr_datarow["dn2sanction_amount"].ToString(),
                        dn3sanction_amount = dr_datarow["dn3sanction_amount"].ToString(),
                        dn1generated_date = dr_datarow["dn1generated_date"].ToString(),
                        dn2generated_date = dr_datarow["dn2generated_date"].ToString(),
                        dn3generated_date = dr_datarow["dn3generated_date"].ToString(),
                        dn1sent_by = dr_datarow["dn1sent_by"].ToString(),
                        dn2sent_by = dr_datarow["dn2sent_by"].ToString(),
                        dn3sent_by = dr_datarow["dn3sent_by"].ToString(),
                        dn1sent_date = dr_datarow["dn1sent_date"].ToString(),
                        dn2sent_date = dr_datarow["dn2sent_date"].ToString(),
                        dn3sent_date = dr_datarow["dn3sent_date"].ToString(),
                        dn1generated_by = dr_datarow["dn1generated_by"].ToString(),
                        dn2generated_by = dr_datarow["dn2generated_by"].ToString(),
                        dn3generated_by = dr_datarow["dn3generated_by"].ToString(),
                        dn1courier_refno = dr_datarow["dn1courier_refno"].ToString(),
                        dn2courier_refno = dr_datarow["dn2courier_refno"].ToString(),
                        dn3courier_refno = dr_datarow["dn3courier_refno"].ToString(),
                        dn1courier_center = dr_datarow["dn1courier_center"].ToString(),
                        dn2courier_center = dr_datarow["dn2courier_center"].ToString(),
                        dn3courier_center = dr_datarow["dn3courier_center"].ToString(),
                        dn1courier_date = dr_datarow["dn1courier_date"].ToString(),
                        dn2courier_date = dr_datarow["dn2courier_date"].ToString(),
                        dn3courier_date = dr_datarow["dn3courier_date"].ToString(),
                        dn1delivered_date = dr_datarow["dn1delivered_date"].ToString(),
                        dn2delivered_date = dr_datarow["dn2delivered_date"].ToString(),
                        dn3delivered_date = dr_datarow["dn3delivered_date"].ToString(),
                        dn1returened_date = dr_datarow["dn1returened_date"].ToString(),
                        dn2returened_date = dr_datarow["dn2returened_date"].ToString(),
                        dn3returened_date = dr_datarow["dn3returened_date"].ToString(),
                        dn1couriered_by = dr_datarow["dn1couriered_by"].ToString(),
                        dn2couriered_by = dr_datarow["dn2couriered_by"].ToString(),
                        dn3couriered_by = dr_datarow["dn3couriered_by"].ToString(),
                        dn1courier_status = dr_datarow["dn1courier_status"].ToString(),
                        dn2courier_status = dr_datarow["dn2courier_status"].ToString(),
                        dn3courier_status = dr_datarow["dn3courier_status"].ToString(),
                        dnstatus = dr_datarow["dnstatus"].ToString()
                    });
                }
                values.dn_list = getdn_list;
            }
            dt_datatable.Dispose();
            values.status = true;
            return true;
        }

        public bool DaGetdntrackerReport_IST(string date, MdlLglReport values)
        {
            string formattedDate;
            if (date == "null")
            {
                formattedDate = "null";
            }
            else
            {
                formattedDate = date;
            }

            msSQL = "(select b.customername,a.urn,vertical_code,case " +
                   " when dn1generated_date like '" + formattedDate + "%' then 'DN1 Generated'" +
                   " when dn2generated_date like '" + formattedDate + "%' then 'DN2 Generated'" +
                   " when dn3generated_date like '" + formattedDate + "%' then 'DN3 Generated' end as dnstatus,  " +
                   " case when dn1generated_date like '" + formattedDate + "%' then concat(n.user_firstname, ' ', n.user_lastname, '/', n.user_code)  " +
                   " when dn2generated_date like '" + formattedDate + "%' then concat(o.user_firstname, ' ', o.user_lastname, '/', o.user_code)  " +
                   " when dn3generated_date like '" + formattedDate + "%' then concat(p.user_firstname, ' ', p.user_lastname, '/', p.user_code) end as dngenerated_by,  " +
                   " '---' as dnsent_by,'---' as courierref_no,'---' as courier_center,'---' as courier_date,'---' as courier_status, '---' as delivered_date,'---' as returned_date, " +
                   " case when dn1generated_date like '" + formattedDate + "%' then date_format(dn1sanction_date,'%d-%m-%Y')  " +
                   " when dn2generated_date like '" + formattedDate + "%' then date_format(dn2sanction_date,'%d-%m-%Y')  " +
                   " when dn3generated_date like '" + formattedDate + "%' then date_format(dn3sanction_date,'%d-%m-%Y')end as sanction_date,  " +
                   " case when dn1generated_date like '" + formattedDate + "%' then dn1sanction_amount" +
                   " when dn2generated_date like '" + formattedDate + "%' then dn2sanction_amount" +
                   " when dn3generated_date like '" + formattedDate + "%' then dn3sanction_amount end as sanction_amount,  " +
                   " case when dn1generate_date like '" + formattedDate + "%' then date_format(dn1generate_date,'%d-%m-%Y') " +
                   " when dn2generate_date like '" + formattedDate + "%' then date_format(dn1generate_date,'%d-%m-%Y')" +
                   " when dn3generate_date like '" + formattedDate + "%' then date_format(dn3generate_date,'%d-%m-%Y')end as dngenerate_date ,'---' as dnsent_date" +
                   " from lgl_tmp_tdnformat x" +
                   " left join lgl_trn_tdncases a on a.tempdn1format_gid = x.tempdn1format_gid" +
                   " left join ocs_mst_tcustomer b on x.customer_urn = b.customer_urn" +
                   " left join hrm_mst_temployee c on c.employee_gid = a.dn1status_created_by" +
                   " left join hrm_mst_temployee d on d.employee_gid = a.dn2status_updated_by" +
                   " left join hrm_mst_temployee e on e.employee_gid = a.dn3status_updated_by" +
                   " left join adm_mst_tuser f on f.user_gid = c.user_gid" +
                   " left join adm_mst_tuser g on g.user_gid = d.user_gid" +
                   " left join adm_mst_tuser h on h.user_gid = e.user_gid" +
                   " left join lgl_trn_tsanctiondtl i on x.customer_urn = i.customer_urn" +
                   " left join lgl_trn_tcourierdetails j on a.dncase_gid = j.dncase_gid" +
                   " left join hrm_mst_temployee l on l.employee_gid = x.dn1generated_by" +
                   " left join hrm_mst_temployee k on k.employee_gid = x.dn2generated_by" +
                   " left join hrm_mst_temployee m on m.employee_gid = x.dn3generated_by" +
                   " left join adm_mst_tuser n on n.user_gid = l.user_gid" +
                   " left join adm_mst_tuser o on o.user_gid = k.user_gid" +
                   " left join adm_mst_tuser p on p.user_gid = m.user_gid" +
                   " where (dn3generated_date like '" + formattedDate + "%' or dn1generated_date like '" + formattedDate + "%' or" +
                   " dn2generated_date like '" + formattedDate + "%') and x.customer_urn not in (select urn from lgl_trn_tdncases where status <> 'Closed') group by x.tempdn1format_gid) UNION" +
                   " (select b.customername, a.urn, vertical_code,case when dn1status_created_date like '" + formattedDate + "%' then dn1status" +
                   " when dn2status_updated_date like '" + formattedDate + "%' then dn2status" +
                   " when dn3status_updated_date like '" + formattedDate + "%' then dn3status end as dnstatus," +
                   " case when dn1generated_date like '" + formattedDate + "%' then concat(n.user_firstname, ' ', n.user_lastname, '/', n.user_code)" +
                   " when dn2generated_date like '" + formattedDate + "%' then concat(o.user_firstname, ' ', o.user_lastname, '/', o.user_code)" +
                   " when dn3generated_date like '" + formattedDate + "%' then concat(p.user_firstname, ' ', p.user_lastname, '/', p.user_code) end as dngenerated_by," +
                   " case when dn1status_created_date like '" + formattedDate + "%' then concat(f.user_firstname, ' ', f.user_lastname, '/', f.user_code)" +
                   " when dn2status_updated_date like '" + formattedDate + "%' then concat(g.user_firstname, ' ', g.user_lastname, '/', g.user_code)" +
                   " when dn3status_updated_date like '" + formattedDate + "%' then concat(h.user_firstname, ' ', h.user_lastname, '/', h.user_code) end as dnsent_by," +
                   " case when dn1status_created_date like '" + formattedDate + "%' then courier_refno" +
                   " when dn2status_updated_date like '" + formattedDate + "%' then dn2courier_refno" +
                   " when dn3status_updated_date like '" + formattedDate + "%' then dn3courier_refno end as courierref_no," +
                   " case when dn1status_created_date like '" + formattedDate + "%' then courier_center" +
                   " when dn2status_updated_date like '" + formattedDate + "%' then dn2courier_center" +
                   " when dn3status_updated_date like '" + formattedDate + "%' then dn3courier_center end as courier_center," +
                   " case when dn1status_created_date like '" + formattedDate + "%' then date_format(courier_date, '%d-%m-%Y')" +
                   " when dn2status_updated_date like '" + formattedDate + "%' then date_format(dn2courier_date, '%d-%m-%Y')" +
                   " when dn3status_updated_date like '" + formattedDate + "%' then date_format(dn3courier_date, '%d-%m-%Y') end as courier_date," +
                   " case when dn1status_created_date like '" + formattedDate + "%' then j.status" +
                   " when dn2status_updated_date like '" + formattedDate + "%' then dn2courier_status" +
                   " when dn3status_updated_date like '" + formattedDate + "%' then dn3courier_status end as courier_status," +
                   " case when dn1status_created_date like '" + formattedDate + "%' then delivered_date" +
                   " when dn2status_updated_date like '" + formattedDate + "%' then dn2delivered_date" +
                   " when dn3status_updated_date like '" + formattedDate + "%' then dn3delivered_date end as delivered_date," +
                   " case when dn1status_created_date like '" + formattedDate + "%' then returened_date" +
                   " when dn2status_updated_date like '" + formattedDate + "%' then dn2returened_date" +
                   " when dn3status_updated_date like '" + formattedDate + "%' then dn3returened_date end as returned_date," +
                   " case when dn1status_created_date like '" + formattedDate + "%' then date_format(dn1sanction_date, '%d-%m-%Y')" +
                   " when dn2status_updated_date like '" + formattedDate + "%' then date_format(dn2sanction_date, '%d-%m-%Y')" +
                   " when dn3status_updated_date like '" + formattedDate + "%' then date_format(dn3sanction_date, '%d-%m-%Y') end as sanction_date," +
                   " case when dn1status_created_date like '" + formattedDate + "%' then dn1sanction_amount" +
                   " when dn2status_updated_date like '" + formattedDate + "%' then dn2sanction_amount" +
                   " when dn3status_updated_date like '" + formattedDate + "%' then dn3sanction_amount end as sanction_amount," +
                   " case when dn1status_created_date like '" + formattedDate + "%' then date_format(dn1generate_date,'%d-%m-%Y') " +
                   " when dn2status_updated_date like '" + formattedDate + "%' then date_format(dn1generate_date,'%d-%m-%Y')" +
                   " when dn3status_updated_date like '" + formattedDate + "%' then date_format(dn3generate_date,'%d-%m-%Y') end as dngenerate_date," +
                   " case when dn1status_created_date like '" + formattedDate + "%' then date_format(dn1status_created_date, '%d-%m-%Y')" +
                   " when dn2status_updated_date like '" + formattedDate + "%' then date_format(dn2status_updated_date, '%d-%m-%Y')" +
                   " when dn2status_updated_date like '" + formattedDate + "%' then date_format(dn2status_updated_date, '%d-%m-%Y') end as dnsent_date" +
                   " from lgl_trn_tdncases a" +
                   " left join lgl_tmp_tdnformat x on a.tempdn1format_gid = x.tempdn1format_gid" +
                   " left join ocs_mst_tcustomer b on a.urn = b.customer_urn" +
                   " left join hrm_mst_temployee c on c.employee_gid = a.dn1status_created_by" +
                   " left join hrm_mst_temployee d on d.employee_gid = a.dn2status_updated_by" +
                   " left join hrm_mst_temployee e on e.employee_gid = a.dn3status_updated_by" +
                   " left join adm_mst_tuser f on f.user_gid = c.user_gid" +
                   " left join adm_mst_tuser g on g.user_gid = d.user_gid" +
                   " left join adm_mst_tuser h on h.user_gid = e.user_gid" +
                   " left join lgl_trn_tsanctiondtl i on a.urn = i.customer_urn" +
                    " left join lgl_trn_tcourierdetails j on a.dncase_gid = j.dncase_gid" +
                    " left join hrm_mst_temployee l on l.employee_gid = x.dn1generated_by" +
                    " left join hrm_mst_temployee k on k.employee_gid = x.dn2generated_by" +
                    " left join hrm_mst_temployee m on m.employee_gid = x.dn3generated_by" +
                    " left join adm_mst_tuser n on n.user_gid = l.user_gid" +
                    " left join adm_mst_tuser o on o.user_gid = k.user_gid" +
                    " left join adm_mst_tuser p on p.user_gid = m.user_gid" +
                    " where dn1status_created_date like '" + formattedDate + "%' or dn2status_updated_date like '" + formattedDate + "%' or" +
                    " dn3status_updated_date like '" + formattedDate + "%' group by a.urn)";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdn_list = new List<dn_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getdn_list.Add(new dn_list
                    {
                        customer_name = (dr_datarow["customername"].ToString()),
                        urn = (dr_datarow["urn"].ToString()),
                        vertical = (dr_datarow["vertical_code"].ToString()),
                        //status = dr_datarow["dnstatus"].ToString(),
                        dngenerated_by = dr_datarow["dngenerated_by"].ToString(),
                        dnsent_by = dr_datarow["dnsent_by"].ToString(),
                        courierref_no = (dr_datarow["courierref_no"].ToString()),
                        courier_center = (dr_datarow["courier_center"].ToString()),
                        courier_date = (dr_datarow["courier_date"].ToString()),
                        courier_status = dr_datarow["courier_status"].ToString(),
                        delivered_date = dr_datarow["delivered_date"].ToString(),
                        returned_date = dr_datarow["returned_date"].ToString(),
                        sanction_amount = dr_datarow["sanction_amount"].ToString(),
                        sanction_date = dr_datarow["sanction_date"].ToString(),
                        dngenerate_date = dr_datarow["dngenerate_date"].ToString(),
                        dnsent_date = dr_datarow["dnsent_date"].ToString(),
                    });
                }
                values.dn_list = getdn_list;
            }
            dt_datatable.Dispose();

            msSQL = "select count(*) as dn1send_count from lgl_trn_tdncases where dn1status_created_date like'" + formattedDate + "%' and dn1status='DN1 Sent'";
            values.dn1send_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select count(*) as dn2send_count from lgl_trn_tdncases where dn2status_updated_date like'" + formattedDate + "%' and dn2status='DN2 Sent'";
            values.dn2send_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select count(*) as dn3send_count from lgl_trn_tdncases where dn3status_updated_date like'" + formattedDate + "%' and dn3status='DN3 Sent'";
            values.dn3send_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select count(*) as dn1send_count from lgl_trn_tdncases where dn1status_created_date like'" + formattedDate + "%' and dn1status='DN1 Skip'";
            values.dn1skip_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select count(*) as dn2send_count from lgl_trn_tdncases where dn2status_updated_date like'" + formattedDate + "%' and dn2status='DN2 Skip'";
            values.dn2skip_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select count(*) as dn3send_count from lgl_trn_tdncases where dn3status_updated_date like'" + formattedDate + "%' and dn3status='DN3 Skip'";
            values.dn3skip_count = objdbconn.GetExecuteScalar(msSQL);

            values.status = true;
            return true;
        }

        public bool DaGetdnTAT(MdlLglReport values)
        {
            msSQL = " select date_format(dn_date,'%d-%m-%Y') as dn_date,dntotal_cases,dneligiblecases_today,dngenerated,dnsent,dnhold,dnskip,dnreverted,dnpending" +
                " from lgl_rpt_tdntat  limit 30";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdn_list = new List<dnTAT_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getdn_list.Add(new dnTAT_list
                    {
                        dn_date = (dr_datarow["dn_date"].ToString()),
                        dntotal_count = (dr_datarow["dntotal_cases"].ToString()),
                        dneligiblecase_today = (dr_datarow["dneligiblecases_today"].ToString()),
                        dnsent = dr_datarow["dnsent"].ToString(),
                        dnskip = (dr_datarow["dnskip"].ToString()),
                        dngenerated = (dr_datarow["dngenerated"].ToString()),
                        dnreverted = (dr_datarow["dnreverted"].ToString()),
                        dnhold = dr_datarow["dnhold"].ToString(),
                        dnpending = dr_datarow["dnpending"].ToString()
                    });
                }
                values.dnTAT_list = getdn_list;
            }
            dt_datatable.Dispose();
            values.status = true;
            return true;
        }

        public bool DaGetDNexport(dn_list objrmDeferral, string employee_gid)
        {
            msSQL = " select a.customer_urn as `Customer URN`,x.customername as `Customer Name`,x.vertical_code as `Vertical`,   " +
                    " case when dn1generated_date is not null then date_format(dn1sanction_date, '%d-%m-%Y')" +
                    " when dn1generated_date is not null then date_format(dn1sanction_date, '%d-%m-%Y')" +
                    " when dn1generated_date is not null then date_format(dn1sanction_date, '%d-%m-%Y') end as `Sanction date`," +
                    " case when dn1generated_date is not null then dn1sanctionref_no" +
                    " when dn1generated_date is not null then dn1sanctionref_no" +
                    " when dn1generated_date is not null then dn1sanctionref_no end as `Sanction Ref No`," +
                    " case when dn1generated_date is not null then format(o.dn1sanction_amount, 2)" +
                    " when dn1generated_date is not null then format(o.dn2sanction_amount, 2)" +
                    " when dn1generated_date is not null then format(o.dn3sanction_amount, 2) end as `Sanction Amount`," +
                    "  case when dn3status_updated_date is not null then dn3status" +
                    " when dn2status_updated_date  is not null then dn2status" +
                    " when dn1status_created_date  is not null then dn1status" +
                    " when dn3generated_date  is not null then 'DN3 Generated'" +
                    " when dn2generated_date  is not null then 'DN2 Generated'" +
                    " when dn1generated_date  is not null then 'DN1 Generated' end as `DN Status`," +
                    " case when dn1generated_date is null  then '---' else date_format(dn1generated_date, '%d-%m-%Y') end as `DN1 Generated Date`," +
                    " case when a.dn1generated_by is null  then '---' else concat(l.user_firstname, ' ', l.user_lastname, '/', l.user_code) end as `DN1 Generated By`," +
                    " case when dn2generated_date is null  then '---' else date_format(dn2generated_date, '%d-%m-%Y') end as `DN2 Generated Date`," +
                    " case when a.dn2generated_by is null  then '---' else concat(m.user_firstname, ' ', m.user_lastname, '/', m.user_code) end as  `DN2 Generated By`," +
                    " case when dn3generated_date is null  then '---' else date_format(dn3generated_date, '%d-%m-%Y') end as `DN3 Generated Date`, " +
                    " case when a.dn3generated_by is null  then '---' else concat(n.user_firstname, ' ', n.user_lastname, '/', n.user_code) end as  `DN3 Generated By`," +
                    " case when dn1status_created_date is null  then '---' else date_format(dn1status_created_date, '%d-%m-%Y') end as `DN1 Sent Date`," +
                    " case when b.dn1status_created_by is null  then '---' else concat(f.user_firstname, ' ', f.user_lastname, '/', f.user_code) end as `DN1 Sent By`," +
                    " case when dn2status_updated_date is null  then '---' else date_format(dn2status_updated_date, '%d-%m-%Y') end as `DN2 Sent Date`," +
                    " case when b.dn2status_updated_by is null  then '---' else concat(g.user_firstname, ' ', g.user_lastname, '/', g.user_code) end as `DN2 Sent By`," +
                    " case when dn2status_updated_date is null  then '---' else date_format(dn3status_updated_date, '%d-%m-%Y') end as `DN3 Sent Date`," +
                    " case when b.dn3status_updated_by is null  then '---' else concat(h.user_firstname, ' ', h.user_lastname, '/', h.user_code) end as `DN3 Sent By`," +
                    " case when courier_refno is null  then '---' else courier_refno end as `DN1 Courier Ref No`," +
                    " case when courier_date is null  then '---' else date_format(courier_date, '%d-%m-%Y') end as `DN1 Courier Date`," +
                    " case when courier_center is null  then '---' else courier_center end as `DN1 Courier Center` ,couriered_by as `DN1 Courier By`,p.status as `DN1 Courier Status`," +
                   " case when dn2courier_refno is null  then '---' else dn2courier_refno end as `DN2 Courier Ref No`," +
                   " case when dn2courier_date is null  then '---' else date_format(dn2courier_date, '%d-%m-%Y') end as `DN2 Courier Date`," +
                   " case when dn2courier_center is null  then '---' else dn2courier_center end as `DN1 Courier Center`, dn2couriered_by as `DN2 Courier By`,dn2courier_status as `DN2 Courier Status`," +
                   " case when dn3courier_refno is null  then '---' else dn3courier_refno end as `DN3 Courier Ref No` ," +
                   " case when dn3courier_date is null  then '---' else date_format(dn3courier_date, '%d-%m-%Y') end as `DN3 Courier Date`," +
                   " case when dn3courier_center is null  then '---' else dn3courier_center end as `DN3 Courier Center`,dn3couriered_by as `DN2 Courier By`,dn3courier_status as `DN3 Courier Status`," +
                   " case when delivered_date is null  then '---' else date_format(delivered_date, '%d-%m-%Y') end as `DN1 Delivered Date`, " +
                   " case when dn2delivered_date is null  then '---' else date_format(dn2delivered_date, '%d-%m-%Y') end as `DN2 Delivered Date`, " +
                   " case when dn3delivered_date is null  then '---' else date_format(dn3delivered_date, '%d-%m-%Y') end as `DN3 Delivered Date`, " +
                   " case when returened_date is null  then '---' else date_format(returened_date, '%d-%m-%Y') end as `DN1 Returned Date`, " +
                   " case when dn2returened_date is null  then '---' else date_format(dn2returened_date, '%d-%m-%Y') end as `DN2 Returned Date`, " +
                   " case when dn3returened_date is null  then '---' else date_format(dn3returened_date, '%d-%m-%Y') end as `DN3 Returned Date`" +
                   " from lgl_tmp_tdnformat a" +
                   " left join lgl_trn_tdncases b on a.tempdn1format_gid = b.tempdn1format_gid" +
                   " left join ocs_mst_tcustomer x on a.customer_urn = x.customer_urn" +
                   " left join hrm_mst_temployee c on c.employee_gid = b.dn1status_created_by" +
                    " left join hrm_mst_temployee d on d.employee_gid = b.dn2status_updated_by" +
                    " left join hrm_mst_temployee e on e.employee_gid = b.dn3status_updated_by" +
                    " left join adm_mst_tuser f on f.user_gid = c.user_gid" +
                    " left join adm_mst_tuser g on g.user_gid = d.user_gid" +
                    " left join adm_mst_tuser h on h.user_gid = e.user_gid" +
                    " left join hrm_mst_temployee i on i.employee_gid = a.dn1generated_by" +
                    " left join hrm_mst_temployee j on j.employee_gid = a.dn2generated_by" +
                    " left join hrm_mst_temployee k on k.employee_gid = a.dn3generated_by" +
                    " left join adm_mst_tuser l on l.user_gid = i.user_gid" +
                    " left join adm_mst_tuser m on m.user_gid = j.user_gid" +
                    " left join adm_mst_tuser n on n.user_gid = k.user_gid" +
                    " left join lgl_trn_tsanctiondtl o on a.customer_urn = o.customer_urn" +
                    " left join lgl_trn_tcourierdetails p on b.dncase_gid = p.dncase_gid" +
                    "  group by a.tempdn1format_gid";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            string lscompany_code = string.Empty;
            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            var workSheet = excel.Workbook.Worksheets.Add("DN Tracker");
            try
            {
                //msSQL = " SELECT a.company_code FROM adm_mst_ttoken a " +
                //        " LEFT JOIN hrm_mst_temployee b ON a.employee_gid = b.employee_gid " +
                //        " LEFT JOIN  adm_mst_tuser c ON b.user_gid = c.user_gid " +
                //        " WHERE a.employee_gid = '" + employee_gid + "'";
                msSQL = " select company_code from adm_mst_tcompany";
                lscompany_code = objdbconn.GetExecuteScalar(msSQL);


                objrmDeferral.lsname = "Report.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "LGL/DNTracker/DNTrackerReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                objrmDeferral.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "LGL/DNTracker/DNTrackerReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objrmDeferral.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(objrmDeferral.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 40])  //Address "A1:A18"
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);
                }
                excel.SaveAs(ms);
                objrmDeferral.lspath = lscompany_code + "/" + "LGL/DNTracker/DNTrackerReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objrmDeferral.lsname;
                dt_datatable.Dispose();
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", objrmDeferral.lspath, ms);
                ms.Close();

            }

            catch (Exception ex)
            {
                objrmDeferral.status = false;
                return false;
            }
            objrmDeferral.lspath = objcmnstorage.EncryptData(objrmDeferral.lspath);
            objrmDeferral.status = true;
            return true;
        }

        public bool DaGetlegalsrreport(mdllglDashboard values, string user_gid)
        {
            msSQL = " select a.customer_gid, a.raiselegalSR_gid,a.auth_status,a.approval_status," +
                    " a.srref_no,a.account_name,a.constitution,a.financed_by,date_format(a.auth_date,'%d-%m-%Y %h:%i %p')  as auth_date,a.auth_remarks," +
                    " concat(c.user_firstname, ' ', c.user_lastname, '/', c.user_code) as raised_by," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, d.customer_urn,d.customername,a.urn," +
                    " e.department_name as raised_by_department" +
                    " from lgl_trn_traiselegalSR a" +
                    " left join hrm_mst_temployee b on  a.created_by = b.employee_gid" +
                    " left join adm_mst_tuser c on b.user_gid=c.user_gid" +
                    " left join hrm_mst_tdepartment e on b.department_gid=e.department_gid" +
                    " left join ocs_mst_tcustomer d on a.customer_gid=d.customer_gid " +
                    " where 1=1 order by a.created_date desc , d.customername asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getlegalSR = new List<legalSR_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getlegalSR.Add(new legalSR_list
                    {
                        legalsr_gid = dr_datarow["raiselegalSR_gid"].ToString(),
                        srref_no = dr_datarow["srref_no"].ToString(),
                        customer_name = (dr_datarow["account_name"].ToString()),
                        constitution = (dr_datarow["constitution"].ToString()),
                        financed_by = (dr_datarow["financed_by"].ToString()),
                        raised_by = (dr_datarow["raised_by"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        raised_by_department = (dr_datarow["raised_by_department"].ToString()),
                        customer_urn = (dr_datarow["customer_urn"].ToString()),
                        auth_status = (dr_datarow["auth_status"].ToString()),
                        approval_status = (dr_datarow["approval_status"].ToString()),
                        auth_date = (dr_datarow["auth_date"].ToString()),
                        auth_remarks = (dr_datarow["auth_remarks"].ToString()),
                        customer_gid = (dr_datarow["customer_gid"].ToString())
                    });
                }
                values.legalSR_list = getlegalSR;
            }
            dt_datatable.Dispose();
            values.status = true;
            return true;
        }
        public bool DaGetSRexport(legalSR_list objlegalsr, string employee_gid)
        {

            msSQL = " select  a.srref_no as `Ref no`, concat(c.user_firstname, ' ', c.user_lastname, '/', c.user_code) as `Initiated By`," +
                     "date_format(a.created_date,'%d-%m-%Y %h:%i %p') as `Initiated Date`,e.department_name as `Department`," +
                     "  d.customer_urn as `Customer URN`,d.customername as `Customer Name`, " +
                     " a.auth_status as `Authentication Status`,date_format(a.auth_date,'%d-%m-%Y %h:%i %p')  as `Authentication Date`," +
                     " a.auth_remarks as `Authentication Remarks`,a.approval_status as `ODC Approval Status`" +
                     " from lgl_trn_traiselegalSR a" +
                     " left join hrm_mst_temployee b on  a.created_by = b.employee_gid" +
                     " left join adm_mst_tuser c on b.user_gid=c.user_gid" +
                     " left join hrm_mst_tdepartment e on b.department_gid=e.department_gid" +
                     " left join ocs_mst_tcustomer d on a.customer_gid=d.customer_gid " +
                     " where 1=1 order by a.created_date desc , d.customername asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            string lscompany_code = string.Empty;
            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            var workSheet = excel.Workbook.Worksheets.Add("LEGAL SR");
            try
            {
                //msSQL = " SELECT a.company_code FROM adm_mst_ttoken a " +
                //        " LEFT JOIN hrm_mst_temployee b ON a.employee_gid = b.employee_gid " +
                //        " LEFT JOIN  adm_mst_tuser c ON b.user_gid = c.user_gid " +
                //        " WHERE a.employee_gid = '" + employee_gid + "'";
                msSQL = " select company_code from adm_mst_tcompany";
                lscompany_code = objdbconn.GetExecuteScalar(msSQL);


                objlegalsr.lsname = "Legal SR Report.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "LGL/LEGALSRReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                objlegalsr.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "LGL/LEGALSRReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objlegalsr.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(objlegalsr.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 40])  //Address "A1:A18"
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);
                }
                excel.SaveAs(ms);
                objlegalsr.lspath =lscompany_code + "/" + "LGL/LEGALSRReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objlegalsr.lsname;
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", objlegalsr.lspath, ms);
                ms.Close();

                dt_datatable.Dispose();
            }

            catch (Exception ex)
            {
                objlegalsr.status = false;
                return false;
            }
            objlegalsr.lspath = objcmnstorage.EncryptData(objlegalsr.lspath);
            objlegalsr.status = true;
            return true;
        }
        public bool DaGetCustomerName(string customername, mdllglDashboard values)
        {
            //try
            //{

            msSQL = " select a.customer_gid, a.raiselegalSR_gid,a.auth_status,a.approval_status," +
                             " a.srref_no,a.account_name,a.constitution,a.financed_by,date_format(a.auth_date,'%d-%m-%Y %h:%i %p')  as auth_date,a.auth_remarks," +
                             " concat(c.user_firstname, ' ', c.user_lastname, '/', c.user_code) as raised_by," +
                             " a.created_date, d.customer_urn,d.customername,a.urn," +
                             " e.department_name as raised_by_department" +
                             " from lgl_trn_traiselegalSR a" +
                             " left join hrm_mst_temployee b on  a.created_by = b.employee_gid" +
                             " left join adm_mst_tuser c on b.user_gid=c.user_gid" +
                             " left join hrm_mst_tdepartment e on b.department_gid=e.department_gid" +
                             " left join ocs_mst_tcustomer d on a.customer_gid=d.customer_gid " +
                             "  where  customername LIKE '" + customername + "%'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcustomer = new List<customers_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcustomer.Add(new customers_list
                    {

                        legalsr_gid = dr_datarow["raiselegalSR_gid"].ToString(),
                        srref_no = dr_datarow["srref_no"].ToString(),
                        customer_name = (dr_datarow["account_name"].ToString()),
                        constitution = (dr_datarow["constitution"].ToString()),
                        financed_by = (dr_datarow["financed_by"].ToString()),
                        raised_by = (dr_datarow["raised_by"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        raised_by_department = (dr_datarow["raised_by_department"].ToString()),
                        customer_urn = (dr_datarow["customer_urn"].ToString()),
                        auth_status = (dr_datarow["auth_status"].ToString()),
                        approval_status = (dr_datarow["approval_status"].ToString()),
                        auth_date = (dr_datarow["auth_date"].ToString()),
                        auth_remarks = (dr_datarow["auth_remarks"].ToString()),
                        customer_gid = (dr_datarow["customer_gid"].ToString())

                    });
                }
                values.customers_list = getcustomer;
            }
            dt_datatable.Dispose();
            values.status = true;
            return true;


        }

        public bool DaGetLegalreportsummary( mdllglDashboard values)
        {
            //try
            //{

            msSQL = " select a.customer_gid, a.raiselegalSR_gid,a.auth_status,a.approval_status," +
                             " a.srref_no,a.account_name,a.constitution,a.financed_by,date_format(a.auth_date,'%d-%m-%Y %h:%i %p')  as auth_date,a.auth_remarks," +
                             " concat(c.user_firstname, ' ', c.user_lastname, '/', c.user_code) as raised_by," +
                             " a.created_date, d.customer_urn,d.customername,a.urn," +
                             " e.department_name as raised_by_department" +
                             " from lgl_trn_traiselegalSR a" +
                             " left join hrm_mst_temployee b on  a.created_by = b.employee_gid" +
                             " left join adm_mst_tuser c on b.user_gid=c.user_gid" +
                             " left join hrm_mst_tdepartment e on b.department_gid=e.department_gid" +
                             " left join ocs_mst_tcustomer d on a.customer_gid=d.customer_gid " +
                             "  where 1=1 and  ";
            if (values.customername == null || values.customername == "")
            {
                msSQL += "1=1 ";
            }
            else
            {

                msSQL += " customername LIKE '" + values.customername + "%'";
            }
            if (values.month_date == null || values.month_date == "")
            {
                msSQL += " and 1=1 ";
            }
            else
            {

                msSQL += "and MONTH(a.created_date) = '" + values.month_date + "'";
            }
            if (values.year_date == null || values.year_date == "")
            {
                msSQL += " and 1=1 ";
            }
            else
            {

                msSQL += "and YEAR(a.created_date) = '" + values.year_date + "'";
            }
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getsummary = new List<Legalreportsummary>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getsummary.Add(new Legalreportsummary
                    {

                        legalsr_gid = dr_datarow["raiselegalSR_gid"].ToString(),
                        srref_no = dr_datarow["srref_no"].ToString(),
                        customername = (dr_datarow["customername"].ToString()),
                        constitution = (dr_datarow["constitution"].ToString()),
                        financed_by = (dr_datarow["financed_by"].ToString()),
                        raised_by = (dr_datarow["raised_by"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        raised_by_department = (dr_datarow["raised_by_department"].ToString()),
                        customer_urn = (dr_datarow["customer_urn"].ToString()),
                        auth_status = (dr_datarow["auth_status"].ToString()),
                        approval_status = (dr_datarow["approval_status"].ToString()),
                        auth_date = (dr_datarow["auth_date"].ToString()),
                        auth_remarks = (dr_datarow["auth_remarks"].ToString()),
                        customer_gid = (dr_datarow["customer_gid"].ToString())

                    });
                }
                values.Legalreportsummary = getsummary;
            }
            dt_datatable.Dispose();
            values.status = true;
            return true;


        }


    }
}