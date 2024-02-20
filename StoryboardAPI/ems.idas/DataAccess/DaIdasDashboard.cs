using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Odbc;
using ems.utilities.Functions;
using ems.idas.Models;
using System;

namespace ems.idas.DataAccess
{
    public class DaIdasDashboard
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        DataTable dt_levelthree;
        string msSQL;
        DataTable dt_datatable;
        string lscount, lstotalcount;
        int lscustomercreatecount, lscustomerupdatecount, lscustomertotalcount;

        public void DaGetIdasPrivilege(string user_gid,string module_gid, idasUserPrivilege values)
        {

            msSQL = " SELECT a.module_gid FROM adm_mst_tprivilege a" +
                     " INNER JOIN adm_mst_tmodule b ON a.module_gid = b.module_gid"+
                     " WHERE user_gid = '" + user_gid + "' AND menu_level = 4" +
                     " AND b.module_gid like '"+ module_gid + "%' ORDER BY b.display_order ";
            dt_levelthree = objdbconn.GetDataTable(msSQL);
            if (dt_levelthree.Rows.Count != 0)
            {
                values.idasUserPrivilege_List = dt_levelthree.AsEnumerable().Select(row => new idasUserPrivilege_List
                {
                    idasUserPrivilege = row["module_gid"].ToString()
                }).ToList();
                values.status = true;
                values.message = "Success";
            }
            else
            {
                values.status = false;
                values.message = "No Privilege";
            }
            dt_levelthree.Dispose();

        }
       
        public void DaGetCadDashboardSummary(string caddropdown, string from_date, string to_date, MdlCadDashboard values)
        {
            if (caddropdown == "employee") {
                msSQL = " SELECT a.user_firstname,b.employee_gid,concat(a.user_firstname, ' ', a.user_lastname, ' / ', a.user_code) as employee_name, " +
                        " b.employee_gid from adm_mst_tuser a " +
                        " left join hrm_mst_temployee b ON a.user_gid = b.user_gid " +
                        " left join hrm_mst_tdepartment c on c.department_gid = b.department_gid " +
                        " where user_status<> 'N' and c.department_name like '%Credit Administration%' order by a.user_firstname asc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcaddashboard_list = new List<caddashboard_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getcaddashboard_list.Add(new caddashboard_list
                        {
                            employee_gid = dt["employee_gid"].ToString(),
                            employee_name = dt["employee_name"].ToString(),
                        });

                    }
                }
                values.caddashboard_list = getcaddashboard_list;
                dt_datatable.Dispose();
            }
            else if (caddropdown == "Customer master updation")
            {
                if ((from_date == "undefined"|| from_date == null)|| (to_date == "undefined"|| to_date == null))
                {
                    msSQL = " SELECT a.user_firstname,b.employee_gid,concat(a.user_firstname, ' ', a.user_lastname, ' / ', a.user_code) as employee_name, " +
                        " (count(distinct(d.customer_gid)) + count(distinct(e.customer_gid))) as customer_count" +
                        " from adm_mst_tuser a " +
                        " left join hrm_mst_temployee b ON a.user_gid = b.user_gid " +
                        " left join hrm_mst_tdepartment c on c.department_gid = b.department_gid " +
                        " left join ocs_mst_tcustomer d on d.created_by = b.employee_gid " +
                        " left join ocs_mst_tcustomer e on e.updated_by = b.employee_gid" +
                        " where user_status<> 'N' and c.department_name like '%Credit Administration%' group by b.employee_gid order by a.user_firstname asc";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getcustomerupdation_list = new List<customerupdation_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            getcustomerupdation_list.Add(new customerupdation_list
                            {
                                customer_count = dt["customer_count"].ToString(),
                                employee_gid = dt["employee_gid"].ToString(),
                                employee_name = dt["employee_name"].ToString(),
                            });

                        }
                    }
                    values.customerupdation_list = getcustomerupdation_list;
                    dt_datatable.Dispose();
                }
                else
                {
                    msSQL = " SELECT a.user_firstname,b.employee_gid,concat(a.user_firstname, ' ', a.user_lastname, ' / ', a.user_code) as employee_name " +
                      " from adm_mst_tuser a " +
                      " left join hrm_mst_temployee b ON a.user_gid = b.user_gid " +
                      " left join hrm_mst_tdepartment c on c.department_gid = b.department_gid " +
                      " where user_status<> 'N' and c.department_name like '%Credit Administration%' group by b.employee_gid order by a.user_firstname asc";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getcustomerupdation_list = new List<customerupdation_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            msSQL = " SELECT (count(distinct(customer_gid))) as customer_count from ocs_mst_tcustomer" +
                                   " where created_by='" + dt["employee_gid"].ToString() + "' and date(created_date)>='" + from_date + "'"+
                                   " and date(created_date)<='" + to_date + "'";
                            lscount = objdbconn.GetExecuteScalar(msSQL);
                            if (lscount == "")
                            {
                                lscustomercreatecount = Convert.ToInt32("0");
                            }
                            else
                            {
                                lscustomercreatecount = Convert.ToInt32(lscount);
                            }
                            msSQL = " SELECT (count(distinct(customer_gid))) as customer_count from ocs_mst_tcustomer" +
                                    " where updated_by='" + dt["employee_gid"].ToString() + "' and date(updated_date)>='" + from_date + "'" +
                                    " and date(updated_date)<='" + to_date + "'"; 
                            lscount = objdbconn.GetExecuteScalar(msSQL);
                            if (lscount == "")
                            {
                                lscustomerupdatecount = Convert.ToInt32("0");
                            }
                            else
                            {
                                lscustomerupdatecount = Convert.ToInt32(lscount);
                            }
                            lscustomertotalcount = lscustomercreatecount + lscustomerupdatecount;
                            getcustomerupdation_list.Add(new customerupdation_list
                            {
                                customer_count = lscustomertotalcount.ToString(),
                                employee_gid = dt["employee_gid"].ToString(),
                                employee_name = dt["employee_name"].ToString(),
                            });

                        }
                    }
                    values.customerupdation_list = getcustomerupdation_list;
                    dt_datatable.Dispose();
                }
            }
            else if (caddropdown == "Sanction Ref.No")
            {
                if ((from_date == "undefined"|| from_date == null)|| (to_date == "undefined"|| to_date == null))
                {
                    msSQL = " SELECT a.user_firstname,b.employee_gid,concat(a.user_firstname, ' ', a.user_lastname, ' / ', a.user_code) as employee_name," +
                     "  count(d.customer2sanction_gid) as sanctioncreation_count from adm_mst_tuser a " +
                     " left join hrm_mst_temployee b ON a.user_gid = b.user_gid " +
                     " left join hrm_mst_tdepartment c on c.department_gid = b.department_gid " +
                     " left join ocs_mst_tcustomer2sanction d on d.created_by = b.employee_gid " +
                     " where user_status<> 'N' and c.department_name like '%Credit Administration%' group by b.employee_gid order by a.user_firstname asc";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getsanction_list = new List<sanction_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            getsanction_list.Add(new sanction_list
                            {
                                sanctioncreation_count = dt["sanctioncreation_count"].ToString(),
                                employee_gid = dt["employee_gid"].ToString(),
                                employee_name = dt["employee_name"].ToString(),
                            });
                        }
                    }
                    values.sanction_list = getsanction_list;
                    dt_datatable.Dispose();
                }
                else
                { 
                    msSQL = " SELECT a.user_firstname,b.employee_gid,concat(a.user_firstname, ' ', a.user_lastname, ' / ', a.user_code) as employee_name" +
                      " from adm_mst_tuser a " +
                      " left join hrm_mst_temployee b ON a.user_gid = b.user_gid " +
                      " left join hrm_mst_tdepartment c on c.department_gid = b.department_gid " +
                      " where user_status<> 'N' and c.department_name like '%Credit Administration%' group by b.employee_gid order by a.user_firstname asc";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getsanction_list = new List<sanction_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            msSQL = " SELECT count(customer2sanction_gid) as sanctioncreation_count from ocs_mst_tcustomer2sanction " +
                                    " where created_by='" + dt["employee_gid"].ToString() + "' and date(created_date)>='" + from_date + "'" +
                                   " and date(created_date)<='" + to_date + "'"; 
                            lscount = objdbconn.GetExecuteScalar(msSQL);
                            if(lscount == "")
                            {
                                lstotalcount = "0";
                            }
                            else
                            {
                                lstotalcount = lscount;
                            }
                            getsanction_list.Add(new sanction_list
                            {
                                employee_gid = dt["employee_gid"].ToString(),
                                employee_name = dt["employee_name"].ToString(),
                                sanctioncreation_count = lstotalcount,
                            });
                        }
                    }
                    values.sanction_list = getsanction_list;
                    dt_datatable.Dispose();
                }
            }
            else if (caddropdown == "Sanction Master updation")
            {
                if ((from_date == "undefined"|| from_date == null)|| (to_date == "undefined"|| to_date == null))
                {
                    msSQL = " SELECT a.user_firstname, concat(a.user_firstname, ' ', a.user_lastname, ' / ', a.user_code) as employee_name, " +
                     " count(d.customer2sanction_gid) as sanctionupdation_count, b.employee_gid from adm_mst_tuser a " +
                     " left join hrm_mst_temployee b ON a.user_gid = b.user_gid " +
                     " left join hrm_mst_tdepartment c on c.department_gid = b.department_gid " +
                     " left join ocs_mst_tcustomer2sanction d on d.updated_by = b.employee_gid " +
                     " where user_status<> 'N' and c.department_name like '%Credit Administration%' group by b.employee_gid order by a.user_firstname asc";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getsanctionupdation_list = new List<sanctionupdation_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            getsanctionupdation_list.Add(new sanctionupdation_list
                            {
                                sanctionupdation_count = dt["sanctionupdation_count"].ToString(),
                                employee_gid = dt["employee_gid"].ToString(),
                                employee_name = dt["employee_name"].ToString(),
                            });

                        }
                    }
                    values.sanctionupdation_list = getsanctionupdation_list;
                    dt_datatable.Dispose();
                }
                else
                {
                    msSQL = " SELECT a.user_firstname, concat(a.user_firstname, ' ', a.user_lastname, ' / ', a.user_code) as employee_name, b.employee_gid "+
                          " from adm_mst_tuser a " +
                          " left join hrm_mst_temployee b ON a.user_gid = b.user_gid " +
                          " left join hrm_mst_tdepartment c on c.department_gid = b.department_gid " +
                          " where user_status<> 'N' and c.department_name like '%Credit Administration%' group by b.employee_gid order by a.user_firstname asc";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getsanctionupdation_list = new List<sanctionupdation_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            msSQL = " SELECT count(customer2sanction_gid) as customer2sanction_gid from ocs_mst_tcustomer2sanction " +
                                   " where updated_by='" + dt["employee_gid"].ToString() + "' and date(updated_date)>='" + from_date + "'" +
                                   " and date(updated_date)<='" + to_date + "'";  
                            lscount = objdbconn.GetExecuteScalar(msSQL);
                            if (lscount == "")
                            {
                                lstotalcount = "0";
                            }
                            else
                            {
                                lstotalcount = lscount;
                            }
                            getsanctionupdation_list.Add(new sanctionupdation_list
                            {
                                sanctionupdation_count = lstotalcount,
                                employee_gid = dt["employee_gid"].ToString(),
                                employee_name = dt["employee_name"].ToString(),
                            });

                        }
                    }
                    values.sanctionupdation_list = getsanctionupdation_list;
                    dt_datatable.Dispose();
                }
            }
            else if (caddropdown == "LSA Maker")
            {
                if ((from_date == "undefined"|| from_date == null)|| (from_date == "undefined"|| from_date == null))
                {
                    msSQL = " SELECT a.user_firstname,concat(a.user_firstname, ' ', a.user_lastname, ' / ', a.user_code) as employee_name, count(d.lsacreate_gid) as lsamaker_count," +
                 " b.employee_gid from adm_mst_tuser a " +
                 " left join hrm_mst_temployee b ON a.user_gid = b.user_gid " +
                 " left join hrm_mst_tdepartment c on c.department_gid = b.department_gid " +
                 " left join ids_trn_tlsa d on d.created_by = b.employee_gid " +
                 " where user_status<> 'N' and c.department_name like '%Credit Administration%' group by b.employee_gid order by a.user_firstname asc";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getlsamaker_list = new List<lsamaker_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            getlsamaker_list.Add(new lsamaker_list
                            {
                                lsamaker_count = dt["lsamaker_count"].ToString(),
                                employee_gid = dt["employee_gid"].ToString(),
                                employee_name = dt["employee_name"].ToString(),
                            });

                        }
                    }
                    values.lsamaker_list = getlsamaker_list;
                    dt_datatable.Dispose();
                }
                else
                { 
                    msSQL = " SELECT a.user_firstname,concat(a.user_firstname, ' ', a.user_lastname, ' / ', a.user_code) as employee_name," +
                  " b.employee_gid from adm_mst_tuser a " +
                  " left join hrm_mst_temployee b ON a.user_gid = b.user_gid " +
                  " left join hrm_mst_tdepartment c on c.department_gid = b.department_gid " +
                  " where user_status<> 'N' and c.department_name like '%Credit Administration%' group by b.employee_gid order by a.user_firstname asc";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getlsamaker_list = new List<lsamaker_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            msSQL = " SELECT count(lsacreate_gid) as lsamaker_count from ids_trn_tlsa " +
                                  " where created_by='" + dt["employee_gid"].ToString() + "' and date(created_date)>='" + from_date + "'" +
                                   " and date(created_date)<='" + to_date + "'";  
                            lscount = objdbconn.GetExecuteScalar(msSQL);
                            if (lscount == "")
                            {
                                lstotalcount = "0";
                            }
                            else
                            {
                                lstotalcount = lscount;
                            }
                            getlsamaker_list.Add(new lsamaker_list
                            {
                                lsamaker_count = lstotalcount,
                                employee_gid = dt["employee_gid"].ToString(),
                                employee_name = dt["employee_name"].ToString(),
                            });
                        }
                    }
                    values.lsamaker_list = getlsamaker_list;
                    dt_datatable.Dispose();
                }
            }
            else if (caddropdown == "LSA Checker")
            {
                if ((from_date == "undefined"|| from_date == null)|| (to_date == "undefined"|| to_date == null))
                {
                    msSQL = "SELECT a.user_firstname,concat(a.user_firstname, ' ', a.user_lastname, ' / ', a.user_code) as employee_name, count(d.lsacreate_gid) as lsachecker_count," +
                 " b.employee_gid from adm_mst_tuser a " +
                 " left join hrm_mst_temployee b ON a.user_gid = b.user_gid " +
                 " left join hrm_mst_tdepartment c on c.department_gid = b.department_gid " +
                 " left join ids_trn_tlsa d on d.approvalupdated_by = b.employee_gid " +
                 " where user_status<> 'N' and c.department_name like '%Credit Administration%' group by b.employee_gid order by a.user_firstname asc";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getlsachecker_list = new List<lsachecker_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            getlsachecker_list.Add(new lsachecker_list
                            {
                                lsachecker_count = dt["lsachecker_count"].ToString(),
                                employee_gid = dt["employee_gid"].ToString(),
                                employee_name = dt["employee_name"].ToString(),
                            });

                        }
                    }
                    values.lsachecker_list = getlsachecker_list;
                    dt_datatable.Dispose();
                }
                else
                {
                    msSQL = "SELECT a.user_firstname,concat(a.user_firstname, ' ', a.user_lastname, ' / ', a.user_code) as employee_name," +
                  " b.employee_gid from adm_mst_tuser a " +
                  " left join hrm_mst_temployee b ON a.user_gid = b.user_gid " +
                  " left join hrm_mst_tdepartment c on c.department_gid = b.department_gid " +
                  " where user_status<> 'N' and c.department_name like '%Credit Administration%' group by b.employee_gid order by a.user_firstname asc";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getlsachecker_list = new List<lsachecker_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            msSQL = " SELECT count(lsacreate_gid) as lsachecker_count from ids_trn_tlsa " +
                                 " where approvalupdated_by='" + dt["employee_gid"].ToString() + "' and date(approvalupdated_date)>='" + from_date + "'" +
                                   " and date(approvalupdated_date)<='" + to_date + "'"; 
                            lscount = objdbconn.GetExecuteScalar(msSQL);
                            if (lscount == "")
                            {
                                lstotalcount = "0";
                            }
                            else
                            {
                                lstotalcount = lscount;
                            }
                            getlsachecker_list.Add(new lsachecker_list
                            {
                                lsachecker_count = lstotalcount,
                                employee_gid = dt["employee_gid"].ToString(),
                                employee_name = dt["employee_name"].ToString(),
                            });

                        }
                    }
                    values.lsachecker_list = getlsachecker_list;
                    dt_datatable.Dispose();
                }
            }
            else if (caddropdown == "Deferrals Created")
            {
                if ((from_date == "undefined"|| from_date == null)|| (to_date == "undefined"|| to_date == null))
                {
                    msSQL = " SELECT a.user_firstname,concat(a.user_firstname, ' ', a.user_lastname, ' / ', a.user_code) as employee_name, count(d.deferral_gid) as deferralcreate_count," +
                 " b.employee_gid from adm_mst_tuser a " +
                 " left join hrm_mst_temployee b ON a.user_gid = b.user_gid " +
                 " left join hrm_mst_tdepartment c on c.department_gid = b.department_gid " +
                 " left join ocs_trn_tdeferral d on d.created_by = b.employee_gid " +
                 " where user_status<> 'N' and c.department_name like '%Credit Administration%' group by b.employee_gid order by a.user_firstname asc";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getdeferralcreate_list = new List<deferralcreate_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            getdeferralcreate_list.Add(new deferralcreate_list
                            {
                                deferralcreate_count = dt["deferralcreate_count"].ToString(),
                                employee_gid = dt["employee_gid"].ToString(),
                                employee_name = dt["employee_name"].ToString(),
                            });
                        }
                    }
                    values.deferralcreate_list = getdeferralcreate_list;
                    dt_datatable.Dispose();
                }
                else
                {
                    msSQL = " SELECT a.user_firstname,concat(a.user_firstname, ' ', a.user_lastname, ' / ', a.user_code) as employee_name," +
                 " b.employee_gid from adm_mst_tuser a " +
                 " left join hrm_mst_temployee b ON a.user_gid = b.user_gid " +
                 " left join hrm_mst_tdepartment c on c.department_gid = b.department_gid " +
                 " where user_status<> 'N' and c.department_name like '%Credit Administration%' group by b.employee_gid order by a.user_firstname asc";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getdeferralcreate_list = new List<deferralcreate_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            msSQL = " SELECT count(deferral_gid) as deferralcreate_count from ocs_trn_tdeferral " +
                                    " where created_by='" + dt["employee_gid"].ToString() + "' and date(created_date)>='" + from_date + "'" +
                                    " and date(created_date)<='" + to_date + "'";  
                            lscount = objdbconn.GetExecuteScalar(msSQL);
                            if (lscount == "")
                            {
                                lstotalcount = "0";
                            }
                            else
                            {
                                lstotalcount = lscount;
                            }
                            getdeferralcreate_list.Add(new deferralcreate_list
                            {
                                deferralcreate_count = lstotalcount,
                                employee_gid = dt["employee_gid"].ToString(),
                                employee_name = dt["employee_name"].ToString(),
                            });

                        }
                    }
                    values.deferralcreate_list = getdeferralcreate_list;
                    dt_datatable.Dispose();
                }
            }
            else if (caddropdown == "Deferral Stage updated")
            {
                if ((from_date == "undefined"|| from_date == null)|| (to_date == "undefined"|| to_date == null))
                {
                    msSQL = " SELECT a.user_firstname,concat(a.user_firstname, ' ', a.user_lastname, ' / ', a.user_code) as employee_name, count(d.deferral_gid) as deferralstage_count," +
                 " b.employee_gid from adm_mst_tuser a " +
                 " left join hrm_mst_temployee b ON a.user_gid = b.user_gid " +
                 " left join hrm_mst_tdepartment c on c.department_gid = b.department_gid " +
                 " left join ocs_trn_tdeferral d on d.updated_by = b.employee_gid " +
                 " where user_status<> 'N' and c.department_name like '%Credit Administration%' group by b.employee_gid order by a.user_firstname asc";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getdeferralstage_list = new List<deferralstage_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            getdeferralstage_list.Add(new deferralstage_list
                            {
                                deferralstage_count = dt["deferralstage_count"].ToString(),
                                employee_gid = dt["employee_gid"].ToString(),
                                employee_name = dt["employee_name"].ToString(),
                            });

                        }
                    }
                    values.deferralstage_list = getdeferralstage_list;
                    dt_datatable.Dispose();
                }
                else
                {
                    msSQL = " SELECT a.user_firstname,concat(a.user_firstname, ' ', a.user_lastname, ' / ', a.user_code) as employee_name," +
                  " b.employee_gid from adm_mst_tuser a " +
                  " left join hrm_mst_temployee b ON a.user_gid = b.user_gid " +
                  " left join hrm_mst_tdepartment c on c.department_gid = b.department_gid " +
                  " where user_status<> 'N' and c.department_name like '%Credit Administration%' group by b.employee_gid order by a.user_firstname asc";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getdeferralstage_list = new List<deferralstage_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            msSQL = " SELECT count(deferral_gid) as deferralstage_count from ocs_trn_tdeferral " +
                               " where updated_by='" + dt["employee_gid"].ToString() + "' and date(updated_date)>='" + from_date + "'" +
                                    " and date(updated_date)<='" + to_date + "'"; 
                            lscount = objdbconn.GetExecuteScalar(msSQL);
                            if (lscount == "")
                            {
                                lstotalcount = "0";
                            }
                            else
                            {
                                lstotalcount = lscount;
                            }
                            getdeferralstage_list.Add(new deferralstage_list
                            {
                                deferralstage_count = lstotalcount,
                                employee_gid = dt["employee_gid"].ToString(),
                                employee_name = dt["employee_name"].ToString(),
                            });

                        }
                    }
                    values.deferralstage_list = getdeferralstage_list;
                    dt_datatable.Dispose();
                }
            }
            else if (caddropdown == "Deferral Checker Approval")
            {
                if ((from_date == "undefined"|| from_date == null)|| (to_date == "undefined"|| to_date == null))
                {
                    msSQL = " SELECT a.user_firstname,concat(a.user_firstname, ' ', a.user_lastname, ' / ', a.user_code) as employee_name, count(d.deferralapproval_gid) as deferralapproval_count," +
                 " b.employee_gid from adm_mst_tuser a " +
                 " left join hrm_mst_temployee b ON a.user_gid = b.user_gid " +
                 " left join hrm_mst_tdepartment c on c.department_gid = b.department_gid " +
                 " left join ocs_trn_tdeferralapproval d on d.checked_by = b.employee_gid " +
                 " where user_status<> 'N' and c.department_name like '%Credit Administration%' group by b.employee_gid order by a.user_firstname asc";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getdeferralapproval_list = new List<deferralapproval_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            getdeferralapproval_list.Add(new deferralapproval_list
                            {
                                deferralapproval_count = dt["deferralapproval_count"].ToString(),
                                employee_gid = dt["employee_gid"].ToString(),
                                employee_name = dt["employee_name"].ToString(),
                            });

                        }
                    }
                    values.deferralapproval_list = getdeferralapproval_list;
                    dt_datatable.Dispose();
                }
                else
                { 
                    msSQL = " SELECT a.user_firstname,concat(a.user_firstname, ' ', a.user_lastname, ' / ', a.user_code) as employee_name, " +
                  " b.employee_gid from adm_mst_tuser a " +
                  " left join hrm_mst_temployee b ON a.user_gid = b.user_gid " +
                  " left join hrm_mst_tdepartment c on c.department_gid = b.department_gid " +
                  " where user_status<> 'N' and c.department_name like '%Credit Administration%' group by b.employee_gid order by a.user_firstname asc";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getdeferralapproval_list = new List<deferralapproval_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            msSQL = " SELECT count(deferralapproval_gid) as deferralapproval_count from ocs_trn_tdeferralapproval " +
                               " where checked_by='" + dt["employee_gid"].ToString() + "' and date(checked_date)>='" + from_date + "'" +
                                    " and date(checked_date)<='" + to_date + "'"; 
                            lscount = objdbconn.GetExecuteScalar(msSQL);
                            if (lscount == "")
                            {
                                lstotalcount = "0";
                            }
                            else
                            {
                                lstotalcount = lscount;
                            }
                            getdeferralapproval_list.Add(new deferralapproval_list
                            {
                                deferralapproval_count = lstotalcount,
                                employee_gid = dt["employee_gid"].ToString(),
                                employee_name = dt["employee_name"].ToString(),
                            });

                        }
                    }
                    values.deferralapproval_list = getdeferralapproval_list;
                    dt_datatable.Dispose();
                }
            }
            else if (caddropdown == "CAD Compliance Certificate generated")
            {
                if ((from_date == "undefined" || from_date == null) || (to_date == "undefined" || to_date == null))
                {
                    msSQL = " SELECT a.user_firstname,concat(a.user_firstname, ' ', a.user_lastname, ' / ', a.user_code) as employee_name, count(d.batch_gid) as cadcompliance_count," +
                                    " b.employee_gid from adm_mst_tuser a " +
                                    " left join hrm_mst_temployee b ON a.user_gid = b.user_gid " +
                                    " left join hrm_mst_tdepartment c on c.department_gid = b.department_gid " +
                                    " left join ids_trn_tbatch d on d.created_by = b.user_gid " +
                                    " where user_status<> 'N' and c.department_name like '%Credit Administration%' group by b.employee_gid order by a.user_firstname asc";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getcadcompliance_list = new List<cadcompliance_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            getcadcompliance_list.Add(new cadcompliance_list
                            {
                                cadcompliance_count = dt["cadcompliance_count"].ToString(),
                                employee_gid = dt["employee_gid"].ToString(),
                                employee_name = dt["employee_name"].ToString(),
                            });

                        }
                    }
                    values.cadcompliance_list = getcadcompliance_list;
                    dt_datatable.Dispose();
                }
                else
                {
                    msSQL = " SELECT a.user_firstname,concat(a.user_firstname, ' ', a.user_lastname, ' / ', a.user_code) as employee_name," +
                                " b.employee_gid, b.user_gid from adm_mst_tuser a " +
                                " left join hrm_mst_temployee b ON a.user_gid = b.user_gid " +
                                " left join hrm_mst_tdepartment c on c.department_gid = b.department_gid " +
                                " where user_status<> 'N' and c.department_name like '%Credit Administration%' group by b.employee_gid order by a.user_firstname asc";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getcadcompliance_list = new List<cadcompliance_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            msSQL = " SELECT count(batch_gid) as cadcompliance_count from ids_trn_tbatch " +
                              " where created_by='" + dt["user_gid"].ToString() + "' and date(created_date)>='" + from_date + "'" +
                                    " and date(created_date)<='" + to_date + "'";  
                            lscount = objdbconn.GetExecuteScalar(msSQL);
                            if (lscount == "")
                            {
                                lstotalcount = "0";
                            }
                            else
                            {
                                lstotalcount = lscount;
                            }
                            getcadcompliance_list.Add(new cadcompliance_list
                            {
                                cadcompliance_count = lstotalcount,
                                employee_gid = dt["employee_gid"].ToString(),
                                employee_name = dt["employee_name"].ToString(),
                            });

                        }
                    }
                    values.cadcompliance_list = getcadcompliance_list;
                    dt_datatable.Dispose();
                }
            }
            else if (caddropdown == "Document Tagged")
            {
                if ((from_date == "undefined"|| from_date == null)|| (to_date == "undefined"|| to_date == null))
                {
                    msSQL = " SELECT a.user_firstname,concat(a.user_firstname, ' ', a.user_lastname, ' / ', a.user_code) as employee_name, count(d.customerfileupload_gid) as doctagged_count," +
                        " b.employee_gid from adm_mst_tuser a " +
                        " left join hrm_mst_temployee b ON a.user_gid = b.user_gid " +
                        " left join hrm_mst_tdepartment c on c.department_gid = b.department_gid " +
                        " left join ids_trn_tcustomerfileupload d on d.created_by = b.user_gid " +
                        " where user_status<> 'N' and c.department_name like '%Credit Administration%' group by b.employee_gid order by a.user_firstname asc";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getdoctagged_list = new List<doctagged_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            getdoctagged_list.Add(new doctagged_list
                            {
                                doctagged_count = dt["doctagged_count"].ToString(),
                                employee_gid = dt["employee_gid"].ToString(),
                                employee_name = dt["employee_name"].ToString(),
                            });

                        }
                    }
                    values.doctagged_list = getdoctagged_list;
                    dt_datatable.Dispose();
                }
                else
                {
                    msSQL = " SELECT a.user_firstname,concat(a.user_firstname, ' ', a.user_lastname, ' / ', a.user_code) as employee_name," +
                      " b.employee_gid, b.user_gid from adm_mst_tuser a " +
                      " left join hrm_mst_temployee b ON a.user_gid = b.user_gid " +
                      " left join hrm_mst_tdepartment c on c.department_gid = b.department_gid " +
                      " where user_status<> 'N' and c.department_name like '%Credit Administration%' group by b.employee_gid order by a.user_firstname asc";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getdoctagged_list = new List<doctagged_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            msSQL = " SELECT count(customerfileupload_gid) as doctagged_count from ids_trn_tcustomerfileupload " +
                             " where created_by='" + dt["user_gid"].ToString() + "' and date(created_date)>='" + from_date + "'" +
                                    " and date(created_date)<='" + to_date + "'";  
                            lscount = objdbconn.GetExecuteScalar(msSQL);
                            if (lscount == "")
                            {
                                lstotalcount = "0";
                            }
                            else
                            {
                                lstotalcount = lscount;
                            }
                            getdoctagged_list.Add(new doctagged_list
                            {
                                doctagged_count = lstotalcount,
                                employee_gid = dt["employee_gid"].ToString(),
                                employee_name = dt["employee_name"].ToString(),
                            });

                        }
                    }
                    values.doctagged_list = getdoctagged_list;
                    dt_datatable.Dispose();
                }
            }
            else if (caddropdown == "Document Vetting Maker")
            {
                if ((from_date == "undefined"|| from_date == null)|| (to_date == "undefined"|| to_date == null))
                {
                    msSQL = " SELECT a.user_firstname,concat(a.user_firstname, ' ', a.user_lastname, ' / ', a.user_code) as employee_name, count(distinct(d.sanction_gid)) as docvettingmaker_count," +
                        " b.employee_gid from adm_mst_tuser a " +
                        " left join hrm_mst_temployee b ON a.user_gid = b.user_gid " +
                        " left join hrm_mst_tdepartment c on c.department_gid = b.department_gid " +
                        " left join ids_trn_tsanctiondocumentdtls d on d.maker_gid = b.user_gid" +
                        " where user_status<> 'N' and c.department_name like '%Credit Administration%' group by b.employee_gid order by a.user_firstname asc";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getdocvettingmaker_list = new List<docvettingmaker_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            getdocvettingmaker_list.Add(new docvettingmaker_list
                            {
                                docvettingmaker_count = dt["docvettingmaker_count"].ToString(),
                                employee_gid = dt["employee_gid"].ToString(),
                                employee_name = dt["employee_name"].ToString(),
                            });

                        }
                    }
                    values.docvettingmaker_list = getdocvettingmaker_list;
                    dt_datatable.Dispose();
                }
                else
                { 
                    msSQL = " SELECT a.user_firstname,concat(a.user_firstname, ' ', a.user_lastname, ' / ', a.user_code) as employee_name, " +
                        " b.employee_gid, b.user_gid from adm_mst_tuser a " +
                        " left join hrm_mst_temployee b ON a.user_gid = b.user_gid " +
                        " left join hrm_mst_tdepartment c on c.department_gid = b.department_gid " +
                        " where user_status<> 'N' and c.department_name like '%Credit Administration%' group by b.employee_gid order by a.user_firstname asc";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getdocvettingmaker_list = new List<docvettingmaker_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            msSQL = " SELECT count(distinct(sanction_gid)) as docvettingmaker_count from ids_trn_tsanctiondocumentdtls " +
                            " where maker_gid='" + dt["user_gid"].ToString() + "' and date(maked_on)>='" + from_date + "'" +
                                    " and date(maked_on)<='" + to_date + "'";  
                            lscount = objdbconn.GetExecuteScalar(msSQL);
                            if (lscount == "")
                            {
                                lstotalcount = "0";
                            }
                            else
                            {
                                lstotalcount = lscount;
                            }
                            getdocvettingmaker_list.Add(new docvettingmaker_list
                            {
                                docvettingmaker_count = lstotalcount,
                                employee_gid = dt["employee_gid"].ToString(),
                                employee_name = dt["employee_name"].ToString(),
                            });

                        }
                    }
                    values.docvettingmaker_list = getdocvettingmaker_list;
                    dt_datatable.Dispose();
                }
            }
            else if (caddropdown == "Collateral Updation")
            {
                if ((from_date == "undefined"|| from_date == null)|| (to_date == "undefined"|| to_date == null))
                {
                    msSQL = " SELECT a.user_firstname,concat(a.user_firstname, ' ', a.user_lastname, ' / ', a.user_code) as employee_name, " +
                    " count(distinct(e.lsacreate_gid)) as collateralupdation_count," +
                    " b.employee_gid from adm_mst_tuser a " +
                    " left join hrm_mst_temployee b ON a.user_gid = b.user_gid " +
                    " left join hrm_mst_tdepartment c on c.department_gid = b.department_gid " +
                    " left join ocs_trn_tcustomercollateral d on d.created_by = b.employee_gid " +
                    " left join ids_trn_tlsa e on e.lsacreate_gid = d.lsacreate_gid " +
                    " where user_status<> 'N' and c.department_name like '%Credit Administration%' group by b.employee_gid order by a.user_firstname asc";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getcollateralUpdation_list = new List<collateralUpdation_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            getcollateralUpdation_list.Add(new collateralUpdation_list
                            {
                                collateralupdation_count = dt["collateralupdation_count"].ToString(),
                                employee_gid = dt["employee_gid"].ToString(),
                                employee_name = dt["employee_name"].ToString(),
                            });

                        }
                    }
                    values.collateralUpdation_list = getcollateralUpdation_list;
                    dt_datatable.Dispose();
                }
                else
               { 
                msSQL = " SELECT a.user_firstname,concat(a.user_firstname, ' ', a.user_lastname, ' / ', a.user_code) as employee_name, " +
                    " b.employee_gid from adm_mst_tuser a " +
                    " left join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                    " left join hrm_mst_tdepartment c on c.department_gid = b.department_gid " +
                    " where user_status<> 'N' and c.department_name like '%Credit Administration%' group by b.employee_gid order by a.user_firstname asc";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getcollateralUpdation_list = new List<collateralUpdation_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            msSQL = " SELECT count(distinct(b.lsacreate_gid)) as collateralupdation_count from ocs_trn_tcustomercollateral a" +
                           " left join ids_trn_tlsa b on b.lsacreate_gid = a.lsacreate_gid " +
                           " where a.created_by='" + dt["employee_gid"].ToString() + "' and date(a.created_date)>='" + from_date + "'" +
                                    " and date(a.created_date)<='" + to_date + "'";  
                            lscount = objdbconn.GetExecuteScalar(msSQL);
                            if (lscount == "")
                            {
                                lstotalcount = "0";
                            }
                            else
                            {
                                lstotalcount = lscount;
                            }
                            getcollateralUpdation_list.Add(new collateralUpdation_list
                            {
                                collateralupdation_count = lstotalcount,
                                employee_gid = dt["employee_gid"].ToString(),
                                employee_name = dt["employee_name"].ToString(),
                            });

                        }
                    }
                    values.collateralUpdation_list = getcollateralUpdation_list;
                    dt_datatable.Dispose();
                }
            }
            else
            {
                if ((from_date == "undefined"|| from_date == null)|| (to_date == "undefined"|| to_date == null))
                {
                    msSQL = " SELECT a.user_firstname,concat(a.user_firstname, ' ', a.user_lastname, ' / ', a.user_code) as employee_name, count(distinct(d.sanction_gid)) as docvettingchecker_count," +
                        " b.employee_gid from adm_mst_tuser a " +
                        " left join hrm_mst_temployee b ON a.user_gid = b.user_gid " +
                        " left join hrm_mst_tdepartment c on c.department_gid = b.department_gid " +
                        " left join ids_trn_tsanctiondocumentdtls d on d.checker_gid = b.user_gid" +
                        " where user_status<> 'N' and c.department_name like '%Credit Administration%' group by b.employee_gid order by a.user_firstname asc";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getdocvettingchecker_list = new List<docvettingchecker_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            getdocvettingchecker_list.Add(new docvettingchecker_list
                            {
                                docvettingchecker_count = dt["docvettingchecker_count"].ToString(),
                                employee_gid = dt["employee_gid"].ToString(),
                                employee_name = dt["employee_name"].ToString(),
                            });

                        }
                    }
                    values.docvettingchecker_list = getdocvettingchecker_list;
                    dt_datatable.Dispose();
                }
                else
                {
                    msSQL = " SELECT a.user_firstname,concat(a.user_firstname, ' ', a.user_lastname, ' / ', a.user_code) as employee_name," +
                        " b.employee_gid, b.user_gid from adm_mst_tuser a " +
                        " left join hrm_mst_temployee b ON a.user_gid = b.user_gid " +
                        " left join hrm_mst_tdepartment c on c.department_gid = b.department_gid " +
                        " where user_status<> 'N' and c.department_name like '%Credit Administration%' group by b.employee_gid order by a.user_firstname asc";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getdocvettingchecker_list = new List<docvettingchecker_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            msSQL = " SELECT count(distinct(sanction_gid)) as docvettingchecker_count from ids_trn_tsanctiondocumentdtls " +
                          " where checker_gid='" + dt["user_gid"].ToString() + "' and  date(checked_on)>='" + from_date + "'" +
                                    " and date(checked_on)<='" + to_date + "'";
                            lscount = objdbconn.GetExecuteScalar(msSQL);
                            if (lscount == "")
                            {
                                lstotalcount = "0";
                            }
                            else
                            {
                                lstotalcount = lscount;
                            }
                            getdocvettingchecker_list.Add(new docvettingchecker_list
                            {
                                docvettingchecker_count = lstotalcount,
                                employee_gid = dt["employee_gid"].ToString(),
                                employee_name = dt["employee_name"].ToString(),
                            });

                        }
                    }
                    values.docvettingchecker_list = getdocvettingchecker_list;
                    dt_datatable.Dispose();
                }
            }

        }

    }
}