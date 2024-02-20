using ems.hrloan.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.IO;
using System.Linq;
using System.Web;
using ems.storage.Functions;

namespace ems.hrloan.DataAccess
{
    public class DaMstHRLoanTenure
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        DataTable dt_datatable;
        OdbcDataReader objODBCDatareader;
        string msSQL, msGetGid;
        int mnResult;
        string lshrloantenure_gid, lstenureinmonths, lswitheffectfrom;

        public void DaGetHRLoanTenure(MdlMstHRLoanTenure hrloantenure)
        {
            try
            {
                msSQL = " SELECT hrloantypeoffinancialassistance_gid,hrloantypeoffinancialassistance_name,hrloantenure_gid,hrloantenure_name, " +
                        " date_format(a.hrloantenurestart_date,'%d-%m-%Y') as hrloantenurestart_date,lms_code, bureau_code, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                        " FROM hrl_mst_thrloantenure a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid order by a.hrloantenure_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var gethrloantenure_list = new List<hrloantenure_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        gethrloantenure_list.Add(new hrloantenure_list
                        {                            
                            hrloantypeoffinancialassistance_gid = (dr_datarow["hrloantypeoffinancialassistance_gid"].ToString()),
                            hrloantypeoffinancialassistance_name = (dr_datarow["hrloantypeoffinancialassistance_name"].ToString()),
                            hrloantenure_gid = (dr_datarow["hrloantenure_gid"].ToString()),
                            hrloantenure_name = (dr_datarow["hrloantenure_name"].ToString()),
                            hrloantenurestart_date = (dr_datarow["hrloantenurestart_date"].ToString()),
                            //hrloantenureend_date = (dr_datarow["hrloantenureend_date"].ToString()),
                            lms_code = (dr_datarow["lms_code"].ToString()),
                            bureau_code = (dr_datarow["bureau_code"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            status = (dr_datarow["status"].ToString()),
                        });
                    }
                    hrloantenure.hrloantenure_list = gethrloantenure_list;
                }
                dt_datatable.Dispose();
                hrloantenure.status = true;
            }
            catch
            {
                hrloantenure.status = false;
            }
        }

        public void DaCreateHRLoanTenure(tenure values, string employee_gid)
        {
            msSQL = "select hrloantypeoffinancialassistance_name from hrl_mst_thrloantenure where hrloantypeoffinancialassistance_name = '" + values.hrloantypeoffinancialassistance_name.Replace("'", @"\'") + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.status = false;
                values.message = "Tenure already exist";
            }
            else
            {
                msGetGid = objcmnfunctions.GetMasterGID("HRTN");

                msSQL = " insert into hrl_mst_thrloantenure(" +
                        " hrloantenure_gid," +
                        " lms_code," +
                        " bureau_code," +
                        " created_by," +
                        " hrloantypeoffinancialassistance_gid," +
                        " hrloantypeoffinancialassistance_name," +
                        " created_date)" +
                        " values(" +
                       "'" + msGetGid + "',";

                if (values.lms_code == "" || values.lms_code == null)
                {
                    msSQL += "'',";
                }
                else
                {
                    msSQL += "'" + values.lms_code.Replace("'", @"\'") + "',";
                }
                if (values.bureau_code == "" || values.bureau_code == null)
                {
                    msSQL += "'',";
                }
                else
                {
                    msSQL += "'" + values.bureau_code.Replace("'", @"\'") + "',";
                }

                msSQL +=
                         "'" + employee_gid + "'," +
                        "'" + values.hrloantypeoffinancialassistance_gid.Replace("'", "") + "'," +
                         "'" + values.hrloantypeoffinancialassistance_name.Replace("'", @"\'") + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {

                    values.status = true;
                    values.message = "Tenure added successfully";
                }
                else
                {
                    values.message = "Error occured while adding";
                    values.status = false;
                }
            }
           
        }
        public bool DaCreateHRLoanTenureUpdate(tenure values, string employee_gid)
        {
            msGetGid = objcmnfunctions.GetMasterGID("HTLG");
            msSQL = " insert into hrl_mst_thrltenure (" +
                      " hrltenure_gid," +
                      " hrloantenure_gid," +
                      " tenure_in_months," +
                      " with_effect_from," +                      
                      " updated_by," +
                      " updated_date) " +
                      " values(" +
                      "'" + msGetGid + "'," +
                      "'" + values.hrloantenure_gid + "'," +
                      "'" + values.hrloantenure_name + "'," +
                      "'" + Convert.ToDateTime(values.hrloantenurestart_date).ToString("yyyy-MM-dd") + "'," +                    
                      "'" + employee_gid + "'," +
                      "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " select hrloantenure_gid,tenure_in_months,date_format(with_effect_from, '%Y-%m-%d') as with_effect_from " +
                    " from hrl_mst_thrltenure  WHERE with_effect_from >= CURRENT_DATE() and hrloantenure_gid = '" + values.hrloantenure_gid + "'" +
                    " order by with_effect_from asc limit 1";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {

                lshrloantenure_gid = objODBCDatareader["hrloantenure_gid"].ToString();
                lstenureinmonths = objODBCDatareader["tenure_in_months"].ToString();
                lswitheffectfrom = objODBCDatareader["with_effect_from"].ToString();
            }
            objODBCDatareader.Close();
            string lscurrentdate = DateTime.Now.ToString("yyyy-MM-dd");
            if (lswitheffectfrom == lscurrentdate)
            {

                msSQL = " update hrl_mst_thrloantenure set " +
                         " hrloantenure_name='" + lstenureinmonths + "'," +
                         " hrloantenurestart_date='" + Convert.ToDateTime(lswitheffectfrom).ToString("yyyy-MM-dd") + "'" +
                         " where hrloantenure_gid='" + values.hrloantenure_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else
            {
                msSQL = " update hrl_mst_thrloantenure set " +
                        " hrloantenure_name='" + values.hrloantenure_name + "'," +
                        " hrloantenurestart_date='" + Convert.ToDateTime(values.hrloantenurestart_date).ToString("yyyy-MM-dd") + "'" +
                        " where hrloantenure_gid='" + values.hrloantenure_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            values.status = true;
            values.message = "Tenure added successfully";

            return true;

        }

        public void DaGetHRLoanTenureDropDown(string employee_gid, MdlMstHRLoanTenure values)
        {

            msSQL = " SELECT hrloantypeoffinancialassistance_gid, hrloantypeoffinancialassistance_name " +
                    " FROM hrl_mst_thrloantypeoffinancialassistance  where status='Y' order by hrloantypeoffinancialassistance_gid desc ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var gettypeofdocument = new List<typeofdocument_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    gettypeofdocument.Add(new typeofdocument_list
                    {
                        hrloantypeoffinancialassistance_gid = (dr_datarow["hrloantypeoffinancialassistance_gid"].ToString()),
                        hrloantypeoffinancialassistance_name = (dr_datarow["hrloantypeoffinancialassistance_name"].ToString()),

                    });
                }
                values.typeofdocument_list = gettypeofdocument;
            }
            dt_datatable.Dispose();            
        }

        public void DaEditHRLoanTenure(string hrloantenure_gid, tenure values)
        {
            try
            {
                msSQL = " SELECT hrloantenure_gid,hrloantypeoffinancialassistance_gid,hrloantypeoffinancialassistance_name, " +
                        " lms_code, bureau_code, status as Status " +
                        " FROM hrl_mst_thrloantenure a where hrloantenure_gid='" + hrloantenure_gid + "' ";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.hrloantenure_gid = objODBCDatareader["hrloantenure_gid"].ToString();                    
                    values.hrloantypeoffinancialassistance_gid = objODBCDatareader["hrloantypeoffinancialassistance_gid"].ToString();
                    values.hrloantypeoffinancialassistance_name = objODBCDatareader["hrloantypeoffinancialassistance_name"].ToString();
                    values.lms_code = objODBCDatareader["lms_code"].ToString();
                    values.bureau_code = objODBCDatareader["bureau_code"].ToString();
                    values.Status = objODBCDatareader["Status"].ToString();
                }
                objODBCDatareader.Close();
                values.status = true;

            }
            catch
            {
                values.status = false;
            }
        }

        public void DaUpdateHRLoanTenure(string employee_gid, tenure values)
        {
            msSQL = "select updated_by, date_format(updated_date,'%Y-%m-%d %h:%i:%s') as updated_date,hrloantypeoffinancialassistance_gid,hrloantypeoffinancialassistance_name,lms_code, bureau_code, status as Status from hrl_mst_thrloantenure where hrloantenure_gid ='" + values.hrloantenure_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                string lsUpdatedBy = objODBCDatareader["updated_by"].ToString();
                string lsUpdatedDate = objODBCDatareader["updated_date"].ToString();

                if (!(String.IsNullOrEmpty(lsUpdatedBy)) && !(String.IsNullOrEmpty(lsUpdatedDate)))
                {
                    msGetGid = objcmnfunctions.GetMasterGID("HRLT");
                    msSQL = " insert into hrl_mst_thrloantenurelog (" +
                              " hrloantenurelog_gid," +
                              " hrloantenure_gid," +                              
                              " hrloantypeoffinancialassistance_gid," +
                              " hrloantypeoffinancialassistance_name," +                              
                              " lms_code," +
                              " bureau_code," +
                              " Status," +
                              " updated_by," +
                              " updated_date) " +
                              " values(" +
                              "'" + msGetGid + "'," +
                              "'" + values.hrloantenure_gid + "'," +                             
                              "'" + values.hrloantypeoffinancialassistance_gid + "'," +
                              "'" + values.hrloantypeoffinancialassistance_name.Replace("'", @"\'") + "'," +                             
                              "'" + values.lms_code.Replace("'", @"\'") + "'," +
                              "'" + values.bureau_code.Replace("'", @"\'") + "'," +
                              "'" + values.Status.Replace("'", @"\'") + "'," +
                              "'" + employee_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            objODBCDatareader.Close();

            msSQL = " update hrl_mst_thrloantenure set " +
                     " hrloantenure_gid='" + values.hrloantenure_gid + "'," +                     
                     " hrloantypeoffinancialassistance_gid='" + values.hrloantypeoffinancialassistance_gid + "'," +
                     " hrloantypeoffinancialassistance_name='" + values.hrloantypeoffinancialassistance_name.Replace("'", @"\'") + "'," +                    
                     " lms_code='" + values.lms_code.Replace("'", @"\'") + "'," +
                     " bureau_code='" + values.bureau_code.Replace("'", @"\'") + "'," +
                     " Status='" + values.Status.Replace("'", @"\'") + "'," +
                     " updated_by='" + employee_gid + "'," +
                     " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                     " where hrloantenure_gid='" + values.hrloantenure_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Tenure updated successfully";    
            }
            else
            {
                values.status = false;
                values.message = "Error occurred while updating";                
            }
        }

        public void DaInactiveHRLoanTenure(hrloantenure values, string employee_gid)
        {
            msSQL = " update hrl_mst_thrloantenure set status='" + values.rbo_status + "'," +
                    " remarks='" + values.remarks.Replace("'", @"\'") + "'" +
                    " where hrloantenure_gid='" + values.hrloantenure_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("HRTI");

                msSQL = " insert into hrl_mst_thrloantenureinactivelog (" +
                      " hrloantenureinactivelog_gid, " +
                      " hrloantenure_gid," +
                      " status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.hrloantenure_gid + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", @"\'") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "Tenure inactivated successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Tenure activated successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error occurred";
            }
        }

        public void DaInactiveHRLoanTenureHistory(HRLoanTenureInactiveHistory objtenurehistory, string hrloantenure_gid)
        {
            try
            {
                msSQL = " select a.remarks, date_format(updated_date,'%Y-%m-%d %h:%i:%s') as updated_date, " +
                " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                " from hrl_mst_thrloantenureinactivelog a " +
                " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                " left join adm_mst_tuser c on b.user_gid = c.user_gid " +
                " where a.hrloantenure_gid='" + hrloantenure_gid + "' order by a.hrloantenureinactivelog_gid desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var gettenureinactivehistory_list = new List<tenureinactivehistory_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        gettenureinactivehistory_list.Add(new tenureinactivehistory_list
                        {
                            status = (dr_datarow["status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString())
                        });
                    }
                    objtenurehistory.tenureinactivehistory_list = gettenureinactivehistory_list;
                }
                dt_datatable.Dispose();
                objtenurehistory.status = true;
            }
            catch
            {
                objtenurehistory.status = false;
            }

        }

        public void DaDeleteHRLoanTenure(string hrloantenure_gid,string hrloantypeoffinancialassistance_gid, string employee_gid, result values)
        {
            msSQL = " select fintype_gid from hrl_trn_trequest where fintype_gid ='" + hrloantypeoffinancialassistance_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                values.message = "This Tenure is mapped, so..you can't delete it..!";
                values.status = false;
                objODBCDatareader.Close();
            }
            else
            {
                msSQL = " delete from hrl_mst_thrloantenure where hrloantenure_gid='" + hrloantenure_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Tenure deleted successfully..!";
                }
                else
                {
                    values.status = false;
                    values.message = "Error occured..!";
                }
            }
        }
        public void DaGettenurelog(MdlMstHRLoanTenure hrloantenurelog,string hrloantenure_gid)
        {
            try
            {
                msSQL = " SELECT a.hrloantypeoffinancialassistance_gid,a.hrloantypeoffinancialassistance_name, " +
                        " a.hrloantenure_gid,d.tenure_in_months,d.with_effect_from, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by FROM hrl_mst_thrloantenure a " +
                        " left join hrl_mst_thrltenure d on d.hrloantenure_gid = a.hrloantenure_gid " +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid "+
                        " where d.hrloantenure_gid = '" + hrloantenure_gid + "' order by d.with_effect_from desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var gettenurelog_list = new List<hrloantenure_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        gettenurelog_list.Add(new hrloantenure_list
                        {
                            hrloantypeoffinancialassistance_gid = (dr_datarow["hrloantypeoffinancialassistance_gid"].ToString()),
                            hrloantypeoffinancialassistance_name = (dr_datarow["hrloantypeoffinancialassistance_name"].ToString()),
                            hrloantenure_gid = (dr_datarow["hrloantenure_gid"].ToString()),
                            tenure_in_months = (dr_datarow["tenure_in_months"].ToString()),
                            with_effect_from = (dr_datarow["with_effect_from"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                        });
                    }
                    hrloantenurelog.hrloantenure_list = gettenurelog_list;
                }
                dt_datatable.Dispose();
                hrloantenurelog.status = true;
            }
            catch
            {
                hrloantenurelog.status = false;
            }
        }

    }
}