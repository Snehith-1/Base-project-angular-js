using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Odbc;
using ems.utilities.Functions;
using ems.master.Models;
using System.Configuration;
using System.IO;
using System.Text;
using System.Net;
using System.Net.Mail;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using System.Reflection;
using org.matheval;
using System.Text.RegularExpressions;
using ems.storage.Functions;
using static OfficeOpenXml.ExcelErrorValue;

namespace ems.master.DataAccess
{
    public class DaMstCreditMapping
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        DaCustomerMailTrigger objcmt = new DaCustomerMailTrigger();
        DataTable dt_datatable;
        OdbcDataReader objODBCDatareader, objODBCDatareader1;
        string msSQL, msGetGid, lsgroupid, msGetcredit2credithead_gid, msGetcreditgrouplog_gid, msGetcreditr2regionalmanager_gid, msGetcredit2nationalmanager_gid, msGetcreditr2credithead_gid, msGetcredit2creditmanager_gid, lsmappinggid;
        int mnResult, GetApiMasterGID;
        string sToken = string.Empty;
        Random rand = new Random();
        public string ls_username;
        public string ls_password;
        public string ls_server;
        public int ls_port;
        public string body;
        public string sub;
        public string cc_mailid, lsBccmail_id;
        public string tomail_id;
        public string lssource;
        public string lsclusterhead, msGetAPICode;
        private IEnumerable<string> lsCCReceipients, lstoReceipients, lsBCCReceipients;
        private string customer_name;
        private string application_no;
        private string cluster_head;
        private string zonal_head;
        private string creditmanager_mailid;
        private string relationshipmanager_name;
        private string relationshipmanager_mailid;
        private string finalapproved_time;
        private string regional_head_mailid;
        private string business_head_mailid;
        private string zonalhead_mailid;
        private string creater_mailid;
        private string cluster_head_name;
        private string business_head_gid;
        private string regional_head_gid;
        private string zonal_head_gid;
        private string content;
        private string cluster_head_gid;
        private string cluster_head_mailid;
        private string business_head_name;
        private string regional_head_name;
        private string nationalmanager_mailid;
        private string credithead_mailid;
        private string regionalmanager_mailid;
        private string creditassigned_date;
        private string allocated_by;
        private string lscreditmanger_gid, lscreditregionalmanager_gid, lscreditnationalmanager_gid, lscredithead_gid;
        private string lscreditmanager_name, lscreditregionalmanager_name, lscreditnationalmanager_name, lscredithead_name, lsremarks, lscreditgroup_name, lscreditgroup_gid;

        public void DaPostCreditgroupaddAdd(MdlCreditGroup values, string employee_gid)
        {
            msSQL = "select creditgroup_name from ocs_mst_tcreditmapping where creditgroup_name = '" + values.creditgroup_name.Replace("'", "\\'") + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.status = false;
                values.message = "Credit Group Name Already Exist";
            }
            else
            {
                msGetGid = objcmnfunctions.GetMasterGID("CRMP");
                msGetAPICode = objcmnfunctions.GetApiMasterGID("CGMP");
                lsgroupid = objcmnfunctions.GetMasterGID("CRGP");
                msSQL = " insert into ocs_mst_tcreditmapping(" +
                        " creditmapping_gid ," +
                         " api_code," +
                        " creditgroup_id," +
                        " creditgroup_name," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + msGetAPICode + "'," +
                         "'" + lsgroupid + "'," +
                        "'" + values.creditgroup_name.Replace("'", "") + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                for (var i = 0; i < values.Credithead.Count; i++)
                {
                    msGetcredit2credithead_gid = objcmnfunctions.GetMasterGID("CRHD");

                    msSQL = "Insert into ocs_mst_tcredit2credithead( " +
                           " credit2credithead_gid, " +
                           " creditmapping_gid," +
                           " employee_gid," +
                           " employee_name," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetcredit2credithead_gid + "'," +
                           "'" + msGetGid + "'," +
                           "'" + values.Credithead[i].employee_gid + "'," +
                           "'" + values.Credithead[i].employee_name + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                for (var i = 0; i < values.Creditnationalmanager.Count; i++)
                {
                    msGetcredit2nationalmanager_gid = objcmnfunctions.GetMasterGID("CRNM");

                    msSQL = "Insert into ocs_mst_tcredit2nationalmanager( " +
                           " credit2nationalmanager_gid, " +
                           " creditmapping_gid," +
                           " employee_gid," +
                           " employee_name," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetcredit2nationalmanager_gid + "'," +
                           "'" + msGetGid + "'," +
                           "'" + values.Creditnationalmanager[i].employee_gid + "'," +
                           "'" + values.Creditnationalmanager[i].employee_name + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                for (var i = 0; i < values.Creditregionalmanager.Count; i++)
                {
                    msGetcreditr2credithead_gid = objcmnfunctions.GetMasterGID("CRRM");

                    msSQL = "Insert into ocs_mst_tcreditr2regionalmanager ( " +
                           " creditr2regionalmanager_gid, " +
                           " creditmapping_gid," +
                           " employee_gid," +
                           " employee_name," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetcreditr2credithead_gid + "'," +
                           "'" + msGetGid + "'," +
                           "'" + values.Creditregionalmanager[i].employee_gid + "'," +
                           "'" + values.Creditregionalmanager[i].employee_name + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                for (var i = 0; i < values.CreditManager.Count; i++)
                {
                    msGetcredit2creditmanager_gid = objcmnfunctions.GetMasterGID("CRMN");

                    msSQL = "Insert into ocs_mst_tcredit2creditmanager( " +
                           " credit2creditmanager_gid, " +
                           " creditmapping_gid," +
                           " employee_gid," +
                           " employee_name," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetcredit2creditmanager_gid + "'," +
                           "'" + msGetGid + "'," +
                           "'" + values.CreditManager[i].employee_gid + "'," +
                           "'" + values.CreditManager[i].employee_name + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Credit Group Added successfully";
                }
                else
                {
                    values.message = "Error Occured while Adding";
                    values.status = false;
                }
            }
            objODBCDatareader.Close();
        }

        public void DaGetCreditGroupSummary(MdlCreditGroup objmaster)
        {
            try
            {
                msSQL = " SELECT a.creditgroup_id,a.creditmapping_gid ,a.api_code,a.creditgroup_name, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                        " FROM ocs_mst_tcreditmapping a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid order by a.created_date desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcreditgroup_list = new List<CreditGroup>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcreditgroup_list.Add(new CreditGroup
                        {
                            creditgroup_id = (dr_datarow["creditgroup_id"].ToString()),
                            creditmapping_gid = (dr_datarow["creditmapping_gid"].ToString()),
                            api_code = (dr_datarow["api_code"].ToString()),
                            creditgroup_name = (dr_datarow["creditgroup_name"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            creditgroup_status = (dr_datarow["status"].ToString())
                        });
                    }
                    objmaster.CreditGroup = getcreditgroup_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch (Exception ex)
            {
                objmaster.status = false;
            }
        }

        public void DaGetCreditGroupEdit(string creditmapping_gid, MdlCreditGroup objmaster)
        {
            msSQL = " select creditmapping_gid,creditgroup_name,creditgroup_id, status as creditgroup_status from ocs_mst_tcreditmapping " +
                    " where creditmapping_gid='" + creditmapping_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objmaster.creditmapping_gid = objODBCDatareader["creditmapping_gid"].ToString();
                objmaster.creditgroup_name = objODBCDatareader["creditgroup_name"].ToString();
                objmaster.creditgroup_status = objODBCDatareader["creditgroup_status"].ToString();
                objmaster.creditgroup_id = objODBCDatareader["creditgroup_id"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " select credit2credithead_gid,employee_gid,employee_name from ocs_mst_tcredit2credithead " +
                  " where creditmapping_gid='" + creditmapping_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcreditheadList = new List<Credithead>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getcreditheadList.Add(new Credithead
                    {
                        credit2credithead_gid = dt["credit2credithead_gid"].ToString(),
                        employee_gid = dt["employee_gid"].ToString(),
                        employee_name = dt["employee_name"].ToString(),
                    });
                    objmaster.Credithead = getcreditheadList;
                }
            }
            dt_datatable.Dispose();
            msSQL = " select credit2nationalmanager_gid,employee_gid,employee_name from ocs_mst_tcredit2nationalmanager " +
                " where creditmapping_gid='" + creditmapping_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getCreditnationalmanagerList = new List<Creditnationalmanager>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getCreditnationalmanagerList.Add(new Creditnationalmanager
                    {
                        credit2nationalmanager_gid = dt["credit2nationalmanager_gid"].ToString(),
                        employee_gid = dt["employee_gid"].ToString(),
                        employee_name = dt["employee_name"].ToString(),
                    });
                    objmaster.Creditnationalmanager = getCreditnationalmanagerList;
                }
            }
            dt_datatable.Dispose();
            msSQL = " select creditr2regionalmanager_gid,employee_gid,employee_name from ocs_mst_tcreditr2regionalmanager " +
                " where creditmapping_gid='" + creditmapping_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getCreditregionalmanagerList = new List<Creditregionalmanager>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getCreditregionalmanagerList.Add(new Creditregionalmanager
                    {
                        creditr2regionalmanager_gid = dt["creditr2regionalmanager_gid"].ToString(),
                        employee_gid = dt["employee_gid"].ToString(),
                        employee_name = dt["employee_name"].ToString(),
                    });
                    objmaster.Creditregionalmanager = getCreditregionalmanagerList;
                }
            }
            dt_datatable.Dispose();
            msSQL = " select credit2creditmanager_gid,employee_gid,employee_name from ocs_mst_tcredit2creditmanager " +
                " where creditmapping_gid='" + creditmapping_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getCreditManagerList = new List<CreditManager>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getCreditManagerList.Add(new CreditManager
                    {
                        credit2creditmanager_gid = dt["credit2creditmanager_gid"].ToString(),
                        employee_gid = dt["employee_gid"].ToString(),
                        employee_name = dt["employee_name"].ToString(),
                    });
                    objmaster.CreditManager = getCreditManagerList;
                }
            }
            dt_datatable.Dispose();

            msSQL = " SELECT a.user_firstname,a.user_gid ,concat(a.user_firstname,' ',a.user_lastname,' / ',a.user_code) as employee_name,b.employee_gid from adm_mst_tuser a " +
               " LEFT JOIN hrm_mst_temployee b ON a.user_gid=b.user_gid " +
               " where user_status<>'N' order by a.user_firstname asc";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_creditheademployee = new List<Creditheadem_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                objmaster.Creditheadem_list = dt_datatable.AsEnumerable().Select(row =>
                  new Creditheadem_list
                  {
                      employee_gid = row["employee_gid"].ToString(),
                      employee_name = row["employee_name"].ToString()
                  }
                ).ToList();
            }
            dt_datatable.Dispose();

            msSQL = " SELECT a.user_firstname,a.user_gid ,concat(a.user_firstname,' ',a.user_lastname,' / ',a.user_code) as employee_name,b.employee_gid from adm_mst_tuser a " +
               " LEFT JOIN hrm_mst_temployee b ON a.user_gid=b.user_gid " +
               " where user_status<>'N' order by a.user_firstname asc";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_nationalemployee = new List<Creditnationalmanagerem_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                objmaster.Creditnationalmanagerem_list = dt_datatable.AsEnumerable().Select(row =>
                  new Creditnationalmanagerem_list
                  {
                      employee_gid = row["employee_gid"].ToString(),
                      employee_name = row["employee_name"].ToString()
                  }
                ).ToList();
            }
            dt_datatable.Dispose();

            msSQL = " SELECT a.user_firstname,a.user_gid ,concat(a.user_firstname,' ',a.user_lastname,' / ',a.user_code) as employee_name,b.employee_gid from adm_mst_tuser a " +
               " LEFT JOIN hrm_mst_temployee b ON a.user_gid=b.user_gid " +
               " where user_status<>'N' order by a.user_firstname asc";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_regionalemployee = new List<Creditregionalmanagerem_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                objmaster.Creditregionalmanagerem_list = dt_datatable.AsEnumerable().Select(row =>
                  new Creditregionalmanagerem_list
                  {
                      employee_gid = row["employee_gid"].ToString(),
                      employee_name = row["employee_name"].ToString()
                  }
                ).ToList();
            }
            dt_datatable.Dispose();

            msSQL = " SELECT a.user_firstname,a.user_gid ,concat(a.user_firstname,' ',a.user_lastname,' / ',a.user_code) as employee_name,b.employee_gid from adm_mst_tuser a " +
                 " LEFT JOIN hrm_mst_temployee b ON a.user_gid=b.user_gid " +
                 " where user_status<>'N' order by a.user_firstname asc";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_manageremployee = new List<CreditManagerem_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                objmaster.CreditManagerem_list = dt_datatable.AsEnumerable().Select(row =>
                  new CreditManagerem_list
                  {
                      employee_gid = row["employee_gid"].ToString(),
                      employee_name = row["employee_name"].ToString()
                  }
                ).ToList();
            }
            dt_datatable.Dispose();

        }

        public bool DaPostCreditGroupUpdate(string employee_gid, MdlCreditGroup values)
        {
            msSQL = "select creditmapping_gid from ocs_mst_tcreditmapping where creditgroup_name = '" + values.creditgroup_name.Replace("'", "\\'") + "'";
            lsmappinggid = objdbconn.GetExecuteScalar(msSQL);
            if (lsmappinggid != "")
            {
                if (lsmappinggid != values.creditmapping_gid)
                {
                    values.status = false;
                    values.message = "Credit Group Name Already Exist";
                    return false;
                }
            }

            msSQL = "select updated_by, updated_date,creditgroup_name from ocs_mst_tcreditmapping where creditmapping_gid ='" + values.creditmapping_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                string lsUpdatedBy = objODBCDatareader["updated_by"].ToString();
                string lsUpdatedDate = objODBCDatareader["updated_date"].ToString();

                if (!(String.IsNullOrEmpty(lsUpdatedBy)) && !(String.IsNullOrEmpty(lsUpdatedDate)))
                {
                    msGetGid = objcmnfunctions.GetMasterGID("RGNL");
                    msSQL = " insert into ocs_mst_tcreditmappinglog(" +
                              " creditmappinglog_gid," +
                              " creditmapping_gid," +
                              " creditgroup_name , " +
                               " old_creditgroup_name , " +
                              " updated_by, " +
                              " updated_date) " +
                              " values(" +
                              "'" + msGetGid + "'," +
                              "'" + values.creditmapping_gid + "',";
                    //"'" + objODBCDatareader["creditgroup_name"].ToString() + "'," +
                    if (objODBCDatareader["creditgroup_name"].ToString() == values.creditgroup_name)
                    {
                        msSQL += "''," +
                                 "'',";
                    }
                    else
                    {
                        msSQL += "'" + values.creditgroup_name + "'," +
                            "'" + objODBCDatareader["creditgroup_name"].ToString() + "',";

                    }
                    msSQL += "'" + employee_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }



            msSQL = "  select GROUP_CONCAT(DISTINCT (a.employee_gid)) as credithead_gid ,GROUP_CONCAT(DISTINCT (a.employee_name)) as credithead_name, GROUP_CONCAT(DISTINCT (a.credit2credithead_gid)) as credit2credithead_gid, " +
                    "  GROUP_CONCAT(DISTINCT (b.employee_gid)) as nationalmanager_gid ,GROUP_CONCAT(DISTINCT (b.employee_name)) as nationalmanager_name, GROUP_CONCAT(DISTINCT (b.credit2nationalmanager_gid)) as credit2nationalmanager_gid, " +
                    "  GROUP_CONCAT(DISTINCT (c.employee_gid)) as regionalmanager_gid ,GROUP_CONCAT(DISTINCT (c.employee_name)) as regionalmanager_name, GROUP_CONCAT(DISTINCT (c.creditr2regionalmanager_gid)) as creditr2regionalmanager_gid, " +
                    "  GROUP_CONCAT(DISTINCT (d.employee_gid)) as creditmanager_gid ,GROUP_CONCAT(DISTINCT (d.employee_name)) as creditmanager_name, GROUP_CONCAT(DISTINCT (d.credit2creditmanager_gid)) as credit2creditmanager_gid " +
                    "  from ocs_mst_tcredit2credithead  a " +
                    " LEFT JOIN ocs_mst_tcredit2nationalmanager b ON a.creditmapping_gid=b.creditmapping_gid " +
                    " LEFT JOIN ocs_mst_tcreditr2regionalmanager c ON c.creditmapping_gid=a.creditmapping_gid" +
                    " LEFT JOIN ocs_mst_tcredit2creditmanager d ON a.creditmapping_gid=d.creditmapping_gid  " +
                    " where a.creditmapping_gid='" + values.creditmapping_gid + "' group by a.creditmapping_gid";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt6 in dt_datatable.Rows)
                {
                    msGetcreditgrouplog_gid = objcmnfunctions.GetMasterGID("C2HL");
                    msSQL = "Insert into ocs_mst_tcreditgrouplog( " +
                         " creditgrouplog_gid, " +
                          " creditgroup_name , " +
                         " old_creditgroup_name , " +
                         " credit2credithead_gid, " +
                         " credit2nationalmanager_gid, " +
                         " creditr2regionalmanager_gid, " +
                         " credit2creditmanager_gid, " +
                         " creditmapping_gid," +
                         " credithead_gid," +
                         " credithead_name," +
                         " nationalmanager_gid," +
                         " nationalmanager_name," +
                         " regionalmanager_gid," +
                         " regionalmanager_name," +
                         " creditmanager_gid," +
                         " creditmanager_name," +
                         " created_by," +
                         " created_date)" +
                         " values(" +
                         "'" + msGetcreditgrouplog_gid + "',";
                    if (objODBCDatareader["creditgroup_name"].ToString() == values.creditgroup_name)
                    {
                        msSQL += "''," +
                                 "'',";
                    }
                    else
                    {
                        msSQL += "'" + values.creditgroup_name + "'," +
                            "'" + objODBCDatareader["creditgroup_name"].ToString() + "',";

                    }
                    msSQL += "'" + (dt6["credit2credithead_gid"].ToString()) + "'," +
                         "'" + (dt6["credit2nationalmanager_gid"].ToString()) + "'," +
                         "'" + (dt6["creditr2regionalmanager_gid"].ToString()) + "'," +
                         "'" + (dt6["credit2creditmanager_gid"].ToString()) + "'," +
                         "'" + values.creditmapping_gid + "'," +
                         "'" + (dt6["credithead_gid"].ToString()) + "'," +
                         "'" + (dt6["credithead_name"].ToString()) + "'," +
                         "'" + (dt6["nationalmanager_gid"].ToString()) + "'," +
                         "'" + (dt6["nationalmanager_name"].ToString()) + "'," +
                         "'" + (dt6["regionalmanager_gid"].ToString()) + "'," +
                         "'" + (dt6["regionalmanager_name"].ToString()) + "'," +
                         "'" + (dt6["creditmanager_gid"].ToString()) + "'," +
                         "'" + (dt6["creditmanager_name"].ToString()) + "'," +
                         "'" + employee_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            objODBCDatareader.Close();

            //msSQL = " select GROUP_CONCAT(employee_gid) as employee_gid ,GROUP_CONCAT(employee_name) as employee_name, GROUP_CONCAT(credit2credithead_gid) as credit2credithead_gid from ocs_mst_tcredit2credithead " +
            //     " where creditmapping_gid='" + values.creditmapping_gid + "' group by creditmapping_gid";
            //dt_datatable = objdbconn.GetDataTable(msSQL);
            //if (dt_datatable.Rows.Count != 0)
            //{
            //    foreach (DataRow dt6 in dt_datatable.Rows)
            //    {
            //        msGetcredit2credithead_gid = objcmnfunctions.GetMasterGID("C2HL");
            //        msSQL = "Insert into ocs_mst_tcredit2creditheadlog( " +
            //             " credit2creditheadlog_gid, " +
            //              " credit2credithead_gid, " +
            //             " creditmapping_gid," +
            //             " employee_gid," +
            //             " employee_name," +
            //             " created_by," +
            //             " created_date)" +
            //             " values(" +
            //             "'" + msGetcredit2credithead_gid + "'," +
            //              "'" + (dt6["credit2credithead_gid"].ToString()) + "'," +
            //              "'" + values.creditmapping_gid + "'," +
            //             "'" + (dt6["employee_gid"].ToString()) + "'," +
            //             "'" + (dt6["employee_name"].ToString()) + "'," +
            //             "'" + employee_gid + "'," +
            //             "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            //        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            //    }
            //}


            //msSQL = " select credit2nationalmanager_gid,employee_gid,employee_name from ocs_mst_tcredit2nationalmanager " +
            //    " where creditmapping_gid='" + values.creditmapping_gid + "'";

            //msSQL = " select GROUP_CONCAT(employee_gid) as employee_gid ,GROUP_CONCAT(employee_name) as employee_name, GROUP_CONCAT(credit2nationalmanager_gid) as credit2nationalmanager_gid from ocs_mst_tcredit2nationalmanager " +
            //" where creditmapping_gid='" + values.creditmapping_gid + "' group by creditmapping_gid";
            //dt_datatable = objdbconn.GetDataTable(msSQL);
            //if (dt_datatable.Rows.Count != 0)
            //{
            //    foreach (DataRow dt6 in dt_datatable.Rows)
            //    {
            //        msGetcredit2nationalmanager_gid = objcmnfunctions.GetMasterGID("C2NL");

            //        msSQL = "Insert into ocs_mst_tcredit2nationalmanagerlog( " +
            //               " credit2nationalmanagerlog_gid, " +
            //               " credit2nationalmanager_gid, " +
            //               " creditmapping_gid," +
            //               " employee_gid," +
            //               " employee_name," +
            //               " created_by," +
            //               " created_date)" +
            //               " values(" +
            //               "'" + msGetcredit2nationalmanager_gid + "'," +
            //                 "'" + (dt6["credit2nationalmanager_gid"].ToString()) + "'," +
            //              "'" + values.creditmapping_gid + "'," +
            //              "'" + (dt6["employee_gid"].ToString()) + "'," +
            //             "'" + (dt6["employee_name"].ToString()) + "'," +
            //               "'" + employee_gid + "'," +
            //               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            //        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            //    }
            //}

            //msSQL = " select creditr2regionalmanager_gid,employee_gid,employee_name from ocs_mst_tcreditr2regionalmanager " +
            //     " where creditmapping_gid='" + values.creditmapping_gid + "'";


            // msSQL = " select GROUP_CONCAT(employee_gid) as employee_gid ,GROUP_CONCAT(employee_name) as employee_name, GROUP_CONCAT(creditr2regionalmanager_gid) as creditr2regionalmanager_gid from ocs_mst_tcreditr2regionalmanager " +
            //" where creditmapping_gid='" + values.creditmapping_gid + "' group by creditmapping_gid";
            // dt_datatable = objdbconn.GetDataTable(msSQL);
            // if (dt_datatable.Rows.Count != 0)
            // {
            //     foreach (DataRow dt6 in dt_datatable.Rows)
            //     {
            //         msGetcreditr2regionalmanager_gid = objcmnfunctions.GetMasterGID("C2RL");

            //         msSQL = "Insert into ocs_mst_tcreditr2regionalmanagerlog ( " +
            //                " creditr2regionalmanagerlog_gid, " +
            //                  " creditr2regionalmanager_gid, " +
            //                " creditmapping_gid," +
            //                " employee_gid," +
            //                " employee_name," +
            //                " created_by," +
            //                " created_date)" +
            //                " values(" +
            //                "'" + msGetcreditr2regionalmanager_gid + "'," +
            //                 "'" + (dt6["creditr2regionalmanager_gid"].ToString()) + "'," +
            //               "'" + values.creditmapping_gid + "'," +
            //              "'" + (dt6["employee_gid"].ToString()) + "'," +
            //              "'" + (dt6["employee_name"].ToString()) + "'," +
            //                "'" + employee_gid + "'," +
            //                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            //         mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            //     }
            // }

            //msSQL = " select credit2creditmanager_gid,employee_gid,employee_name from ocs_mst_tcredit2creditmanager " +
            //     " where creditmapping_gid='" + values.creditmapping_gid + "'";


            //  msSQL = " select GROUP_CONCAT(employee_gid) as employee_gid ,GROUP_CONCAT(employee_name) as employee_name, GROUP_CONCAT(credit2creditmanager_gid) as credit2creditmanager_gid from ocs_mst_tcredit2creditmanager " +
            //" where creditmapping_gid='" + values.creditmapping_gid + "' group by creditmapping_gid";
            //  dt_datatable = objdbconn.GetDataTable(msSQL);
            //  if (dt_datatable.Rows.Count != 0)
            //  {
            //      foreach (DataRow dt6 in dt_datatable.Rows)
            //      {
            //          msGetcredit2creditmanager_gid = objcmnfunctions.GetMasterGID("C2ML");

            //          msSQL = "Insert into ocs_mst_tcredit2creditmanagerlog( " +
            //                " credit2creditmanagerlog_gid, " +
            //                 " credit2creditmanager_gid, " +
            //                " creditmapping_gid," +
            //                " employee_gid," +
            //                " employee_name," +
            //                " created_by," +
            //                " created_date)" +
            //                " values(" +
            //                "'" + msGetcredit2creditmanager_gid + "'," +
            //                 "'" + (dt6["credit2creditmanager_gid"].ToString()) + "'," +
            //               "'" + values.creditmapping_gid + "'," +
            //                "'" + (dt6["employee_gid"].ToString()) + "'," +
            //               "'" + (dt6["employee_name"].ToString()) + "'," +
            //                "'" + employee_gid + "'," +
            //                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            //          mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            //      }
            //  }

            msSQL = "update ocs_mst_tcreditmapping set creditgroup_name='" + values.creditgroup_name.Replace("'", "") + "'," +
                     " updated_by='" + employee_gid + "'," +
                     " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                     " where creditmapping_gid='" + values.creditmapping_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update ocs_mst_tapplication set creditgroup_name='" + values.creditgroup_name.Replace("'", "") + "'" +
                   " where creditgroup_gid='" + values.creditmapping_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " delete from ocs_mst_tcredit2credithead where creditmapping_gid ='" + values.creditmapping_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                for (var i = 0; i < values.Credithead.Count; i++)
                {
                    msGetcredit2credithead_gid = objcmnfunctions.GetMasterGID("CRHD");

                    msSQL = "Insert into ocs_mst_tcredit2credithead( " +
                           " credit2credithead_gid, " +
                           " creditmapping_gid," +
                           " employee_gid," +
                           " employee_name," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetcredit2credithead_gid + "'," +
                           "'" + values.creditmapping_gid + "'," +
                           "'" + values.Credithead[i].employee_gid + "'," +
                           "'" + values.Credithead[i].employee_name + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            msSQL = " delete from ocs_mst_tcredit2nationalmanager where creditmapping_gid ='" + values.creditmapping_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                for (var i = 0; i < values.Creditnationalmanager.Count; i++)
                {
                    msGetcredit2nationalmanager_gid = objcmnfunctions.GetMasterGID("CRNM");

                    msSQL = "Insert into ocs_mst_tcredit2nationalmanager( " +
                           " credit2nationalmanager_gid, " +
                           " creditmapping_gid," +
                           " employee_gid," +
                           " employee_name," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetcredit2nationalmanager_gid + "'," +
                           "'" + values.creditmapping_gid + "'," +
                           "'" + values.Creditnationalmanager[i].employee_gid + "'," +
                           "'" + values.Creditnationalmanager[i].employee_name + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            msSQL = " delete from ocs_mst_tcreditr2regionalmanager where creditmapping_gid ='" + values.creditmapping_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                for (var i = 0; i < values.Creditregionalmanager.Count; i++)
                {
                    msGetcreditr2credithead_gid = objcmnfunctions.GetMasterGID("CRRM");

                    msSQL = "Insert into ocs_mst_tcreditr2regionalmanager ( " +
                           " creditr2regionalmanager_gid, " +
                           " creditmapping_gid," +
                           " employee_gid," +
                           " employee_name," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetcreditr2credithead_gid + "'," +
                           "'" + values.creditmapping_gid + "'," +
                           "'" + values.Creditregionalmanager[i].employee_gid + "'," +
                           "'" + values.Creditregionalmanager[i].employee_name + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            msSQL = " delete from ocs_mst_tcredit2creditmanager where creditmapping_gid ='" + values.creditmapping_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                for (var i = 0; i < values.CreditManager.Count; i++)
                {
                    msGetcredit2creditmanager_gid = objcmnfunctions.GetMasterGID("CRMN");

                    msSQL = "Insert into ocs_mst_tcredit2creditmanager( " +
                           " credit2creditmanager_gid, " +
                           " creditmapping_gid," +
                           " employee_gid," +
                           " employee_name," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetcredit2creditmanager_gid + "'," +
                           "'" + values.creditmapping_gid + "'," +
                           "'" + values.CreditManager[i].employee_gid + "'," +
                           "'" + values.CreditManager[i].employee_name + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }


            msSQL = "  select GROUP_CONCAT(DISTINCT (a.employee_gid)) as new_credithead_gid ,GROUP_CONCAT(DISTINCT (a.employee_name)) as new_credithead_name, GROUP_CONCAT(DISTINCT (a.credit2credithead_gid)) as new_credit2credithead_gid, " +
                  "  GROUP_CONCAT(DISTINCT (b.employee_gid)) as new_nationalmanager_gid ,GROUP_CONCAT(DISTINCT (b.employee_name)) as new_nationalmanager_name, GROUP_CONCAT(DISTINCT (b.credit2nationalmanager_gid)) as new_credit2nationalmanager_gid, " +
                  "  GROUP_CONCAT(DISTINCT (c.employee_gid)) as new_regionalmanager_gid ,GROUP_CONCAT(DISTINCT (c.employee_name)) as new_regionalmanager_name, GROUP_CONCAT(DISTINCT (c.creditr2regionalmanager_gid)) as new_creditr2regionalmanager_gid, " +
                  "  GROUP_CONCAT(DISTINCT (d.employee_gid)) as new_creditmanager_gid ,GROUP_CONCAT(DISTINCT (d.employee_name)) as new_creditmanager_name, GROUP_CONCAT(DISTINCT (d.credit2creditmanager_gid)) as new_credit2creditmanager_gid " +
                  "  from ocs_mst_tcredit2credithead  a " +
                  " LEFT JOIN ocs_mst_tcredit2nationalmanager b ON a.creditmapping_gid=b.creditmapping_gid " +
                  " LEFT JOIN ocs_mst_tcreditr2regionalmanager c ON c.creditmapping_gid=a.creditmapping_gid" +
                  " LEFT JOIN ocs_mst_tcredit2creditmanager d ON a.creditmapping_gid=d.creditmapping_gid  " +
                  " where a.creditmapping_gid='" + values.creditmapping_gid + "' group by a.creditmapping_gid";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt6 in dt_datatable.Rows)
                {
                    msSQL = " update ocs_mst_tcreditgrouplog set " +
                        " new_credit2credithead_gid ='" + (dt6["new_credit2credithead_gid"].ToString()) + "'," +
                        " new_credit2nationalmanager_gid ='" + (dt6["new_credit2nationalmanager_gid"].ToString()) + "'," +
                        " new_creditr2regionalmanager_gid ='" + (dt6["new_creditr2regionalmanager_gid"].ToString()) + "'," +
                        " new_credit2creditmanager_gid ='" + (dt6["new_credit2creditmanager_gid"].ToString()) + "'," +
                        " new_credithead_gid ='" + (dt6["new_credithead_gid"].ToString()) + "'," +
                        " new_credithead_name ='" + (dt6["new_credithead_name"].ToString()) + "'," +
                        " new_nationalmanager_gid ='" + (dt6["new_nationalmanager_gid"].ToString()) + "'," +
                        " new_nationalmanager_name ='" + (dt6["new_nationalmanager_name"].ToString()) + "'," +
                        " new_regionalmanager_gid ='" + (dt6["new_regionalmanager_gid"].ToString()) + "'," +
                        " new_regionalmanager_name ='" + (dt6["new_regionalmanager_name"].ToString()) + "'," +
                        " new_creditmanager_gid ='" + (dt6["new_creditmanager_gid"].ToString()) + "'," +
                         " new_creditmanager_name ='" + (dt6["new_creditmanager_name"].ToString()) + "'," +
                        " updated_by='" + employee_gid + "'," +
                        " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                        " where creditgrouplog_gid='" + msGetcreditgrouplog_gid + "' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                    //msSQL = "Insert into ocs_mst_tcreditgrouplog( " +
                    //     " creditgrouplog_gid, " +
                    //     " newcredit2credithead_gid, " +
                    //     " new_credit2nationalmanager_gid, " +
                    //     " new_creditr2regionalmanager_gid, " +
                    //     " new_credit2creditmanager_gid, " +
                    //     " creditmapping_gid," +
                    //     " new_credithead_gid," +
                    //     " new_credithead_name," +
                    //     " new_nationalmanage_gid," +
                    //     " new_nationalmanage_name," +
                    //     " new_regionalmanager_gid," +
                    //     " new_regionalmanager_name," +
                    //     " new_creditmanager_gid," +
                    //     " new_creditmanager_name," +
                    //     " created_by," +
                    //     " created_date)" +
                    //     " values(" +
                    //     "'" + msGetcredit2credithead_gid + "'," +
                    //     "'" + (dt6["new_credit2credithead_gid"].ToString()) + "'," +
                    //     "'" + (dt6["new_credit2nationalmanager_gid"].ToString()) + "'," +
                    //     "'" + (dt6["new_creditr2regionalmanager_gid"].ToString()) + "'," +
                    //     "'" + (dt6["new_credit2creditmanager_gid"].ToString()) + "'," +
                    //     "'" + values.creditmapping_gid + "'," +
                    //     "'" + (dt6["new_credithead_gid"].ToString()) + "'," +
                    //     "'" + (dt6["new_credithead_name"].ToString()) + "'," +
                    //     "'" + (dt6["new_nationalmanage_gid"].ToString()) + "'," +
                    //     "'" + (dt6["new_nationalmanage_name"].ToString()) + "'," +
                    //     "'" + (dt6["new_regionalmanager_gid"].ToString()) + "'," +
                    //     "'" + (dt6["new_regionalmanager_name"].ToString()) + "'," +
                    //     "'" + (dt6["new_creditmanager_gid"].ToString()) + "'," +
                    //     "'" + (dt6["new_creditmanager_name"].ToString()) + "'," +
                    //     "'" + employee_gid + "'," +
                    //     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Credit Group updated successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while updating Credit Group";
                return false;
            }
        }

        public void DaGetCredit2Heads(string creditmapping_gid, MdlCreditGroup objmaster)
        {
            msSQL = " select credit2credithead_gid,employee_gid,employee_name, creditmapping_gid from ocs_mst_tcredit2credithead " +
                  " where creditmapping_gid='" + creditmapping_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getCredithead = new List<Credithead>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getCredithead.Add(new Credithead
                    {
                        credit2credithead_gid = dt["credit2credithead_gid"].ToString(),
                        employee_gid = dt["employee_gid"].ToString(),
                        employee_name = dt["employee_name"].ToString(),
                    });
                    objmaster.Credithead = getCredithead;
                }
            }
            dt_datatable.Dispose();

            msSQL = " select credit2nationalmanager_gid,employee_gid,employee_name, creditmapping_gid from ocs_mst_tcredit2nationalmanager " +
                " where creditmapping_gid='" + creditmapping_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getCreditnationalmanager = new List<Creditnationalmanager>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getCreditnationalmanager.Add(new Creditnationalmanager
                    {
                        credit2nationalmanager_gid = dt["credit2nationalmanager_gid"].ToString(),
                        employee_gid = dt["employee_gid"].ToString(),
                        employee_name = dt["employee_name"].ToString(),
                    });
                    objmaster.Creditnationalmanager = getCreditnationalmanager;
                }
            }
            dt_datatable.Dispose();

            msSQL = " select creditr2regionalmanager_gid,employee_gid,employee_name, creditmapping_gid from ocs_mst_tcreditr2regionalmanager " +
                " where creditmapping_gid='" + creditmapping_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getCreditregionalmanager = new List<Creditregionalmanager>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getCreditregionalmanager.Add(new Creditregionalmanager
                    {
                        creditr2regionalmanager_gid = dt["creditr2regionalmanager_gid"].ToString(),
                        employee_gid = dt["employee_gid"].ToString(),
                        employee_name = dt["employee_name"].ToString(),
                    });
                    objmaster.Creditregionalmanager = getCreditregionalmanager;
                }
            }
            dt_datatable.Dispose();

            msSQL = " select credit2creditmanager_gid,employee_gid,employee_name, creditmapping_gid from ocs_mst_tcredit2creditmanager " +
                " where creditmapping_gid='" + creditmapping_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getCreditManager = new List<CreditManager>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getCreditManager.Add(new CreditManager
                    {
                        credit2creditmanager_gid = dt["credit2creditmanager_gid"].ToString(),
                        employee_gid = dt["employee_gid"].ToString(),
                        employee_name = dt["employee_name"].ToString(),
                    });
                    objmaster.CreditManager = getCreditManager;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetCreditgroupHeads(string creditmapping_gid, creditheads values)
        {
            msSQL = " select group_concat(employee_name) as employee_name  from ocs_mst_tcredit2credithead " +
                  " where creditmapping_gid='" + creditmapping_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.credithead = objODBCDatareader["employee_name"].ToString();
            }
            objODBCDatareader.Close();
            msSQL = " select group_concat(employee_name) as employee_name from ocs_mst_tcredit2nationalmanager " +
                " where creditmapping_gid='" + creditmapping_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.creditnational_manager = objODBCDatareader["employee_name"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " select group_concat(employee_name) as employee_name  from ocs_mst_tcreditr2regionalmanager " +
                " where creditmapping_gid='" + creditmapping_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.creditregional_manager = objODBCDatareader["employee_name"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " select group_concat(employee_name) as employee_name  from ocs_mst_tcredit2creditmanager " +
                " where creditmapping_gid='" + creditmapping_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.creditmanager = objODBCDatareader["employee_name"].ToString();
            }
            objODBCDatareader.Close();
        }

        public void DaPostCreditgroupInactive(CreditGroup values, string employee_gid)
        {
            msSQL = " update ocs_mst_tcreditmapping set status ='" + values.rbo_status + "'," +
                    " remarks='" + values.remarks.Replace("'", "") + "'" +
                    " where creditmapping_gid='" + values.creditmapping_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("CRMA");

                msSQL = " insert into ocs_mst_tcreditmappinginactivelog (" +
                      " creditmappinginactivelog_gid , " +
                      " creditmapping_gid," +
                      " creditgroup_name ," +
                      " status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.creditmapping_gid + "'," +
                      " '" + values.creditgroup_name + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", "") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "Credit Group Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Credit Group Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        }

        public void DaGetCreditgroupInactiveLogview(string creditmapping_gid, MdlCreditGroup values)
        {
            try
            {
                msSQL = " SELECT creditmapping_gid,date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as Status, a.remarks" +
                        " FROM ocs_mst_tcreditmappinginactivelog a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where creditmapping_gid ='" + creditmapping_gid + "' order by a.creditmappinginactivelog_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getCreditlog = new List<Creditlog>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getCreditlog.Add(new Creditlog
                        {
                            creditmapping_gid = (dr_datarow["creditmapping_gid"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            status = (dr_datarow["Status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                        });
                    }
                    values.Creditlog = getCreditlog;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public bool DaPostCreditassignUpdate(string employee_gid, string user_gid, MdlCreditheadassign values)
        {
            msSQL = "update ocs_mst_tapplication set " +
                    " creditgroup_gid='" + values.creditgroup_gid + "'," +
                    " creditgroup_name='" + values.creditgroup_name + "'," +
                    " credithead_gid='" + values.credithead_gid + "'," +
                    " credithead_name='" + values.credithead_name + "'," +
                    " creditnationalmanager_gid='" + values.nationalcredit_gid + "'," +
                    " creditnationalmanager_name ='" + values.nationalcredit_name + "'," +
                    " creditregionalmanager_gid='" + values.regionalcredit_gid + "'," +
                    " creditregionalmanager_name='" + values.regionalcredit_name + "'," +
                    " creditmanager_gid='" + values.creditmanager_gid + "'," +
                    " creditmanager_name='" + values.creditmanager_name + "'," +
                    " creditgroup_status = 'Assigned',";                 
                    if (values.remarks == "" || values.remarks == null || values.remarks == "undefined")
                    {
                        msSQL += " remarks='', ";
                    }
                    else
                    {
                        msSQL += " remarks='" + values.remarks.Replace("'", "") + "', ";
                    }
            msSQL += " creditassigned_by='" + employee_gid + "'," +
                    " creditassigned_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +                    
                    " where application_gid='" + values.application_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                int k;
                k = 0;
                string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
                sToken = "";
                int Length = 100;
                for (int j = 0; j < Length; j++)
                {
                    string sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                    sToken += sTempChars;
                }

                msGetGid = objcmnfunctions.GetMasterGID("CRAP");
                msSQL = "Insert into ocs_trn_tAppcreditapproval( " +
                       " appcreditapproval_gid, " +
                       " application_gid," +
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
                       "'" + values.application_gid + "'," +
                       "'" + values.creditmanager_gid + "'," +
                       "'" + values.creditmanager_name + "'," +
                       "'sequence'," +
                       "'" + k + "'," +
                       "'" + sToken + "'," +
                       "'Y'," +
                       "'" + user_gid + "'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    try
                    {
                        msSQL = "select application_no from ocs_mst_tapplication where application_gid='" + values.application_gid + "'";
                        application_no = objdbconn.GetExecuteScalar(msSQL);
                        msSQL = "select customerref_name from ocs_mst_tapplication where application_gid='" + values.application_gid + "'";
                        customer_name = objdbconn.GetExecuteScalar(msSQL);
                        msSQL = "select approved_date from ocs_trn_tapplicationapproval a left join ocs_mst_tapplication b on b.application_gid = a.application_gid and hierary_level = '5' where b.application_gid='" + values.application_gid + "'";
                        finalapproved_time = objdbconn.GetExecuteScalar(msSQL);
                        msSQL = "select clustermanager_name,zonalhead_name,relationshipmanager_name,creditassigned_date from ocs_mst_tapplication where application_gid = '" + values.application_gid + "'";
                        objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader1.HasRows == true)
                        {
                            cluster_head = objODBCDatareader1["clustermanager_name"].ToString();
                            zonal_head = objODBCDatareader1["zonalhead_name"].ToString();
                            relationshipmanager_name = objODBCDatareader1["relationshipmanager_name"].ToString();
                            creditassigned_date = objODBCDatareader1["creditassigned_date"].ToString();
                        }
                        objODBCDatareader1.Close();
                        msSQL = "select b.employee_emailid from ocs_mst_tapplication a left join hrm_mst_temployee b on b.employee_gid = a.creditmanager_gid  where application_gid = '" + values.application_gid + "'";
                        tomail_id = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = "select b.employee_emailid from ocs_mst_tapplication a left join hrm_mst_temployee b on b.employee_gid = a.creditnationalmanager_gid  where application_gid = '" + values.application_gid + "'";
                        nationalmanager_mailid = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = "select b.employee_emailid from ocs_mst_tapplication a left join hrm_mst_temployee b on b.employee_gid = a.creditregionalmanager_gid  where application_gid = '" + values.application_gid + "'";
                        regionalmanager_mailid = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = "select b.employee_emailid from ocs_mst_tapplication a left join hrm_mst_temployee b on b.employee_gid = a.credithead_gid  where application_gid = '" + values.application_gid + "'";
                        credithead_mailid = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = " select b.employee_emailid from ocs_mst_tapplication a left join hrm_mst_temployee b on b.employee_gid = a.relationshipmanager_gid where a.application_gid='" + values.application_gid + "'";
                        relationshipmanager_mailid = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = "select concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as allocated_by from ocs_mst_tapplication a left join hrm_mst_temployee b on b.employee_gid = a.creditassigned_by left join adm_mst_tuser c on c.user_gid = b.user_gid where a.application_gid='" + values.application_gid + "'";
                        allocated_by = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = " SELECT pop_server, pop_port, pop_username, pop_password" +
                                " FROM adm_mst_tcompany";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            ls_server = objODBCDatareader["pop_server"].ToString();
                            ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                            ls_username = objODBCDatareader["pop_username"].ToString();
                            ls_password = objODBCDatareader["pop_password"].ToString();
                        }
                        lssource = ConfigurationManager.AppSettings["img_path"];
                        objODBCDatareader.Close();
                        sub = " Application allocation : " + application_no + " ";
                        body = "<style>table, th, td {border: 1px solid black;border-collapse: collapse;}</style>";
                        body = body + "<table style='border-right: 1px solid black;border-top: 1px solid black;border-bottom: 1px solid black;'><tr><td style='border-right-color:white;align:center;'>";
                        body = body + "<br />";
                        body = body + "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp <img style='height:150px; width:380px;' src='" + lssource + "'><br />";
                        body = body + "<br />";
                        body = body + " &nbsp &nbsp Dear Sir / Madam, <br />";
                        body = body + "<br />";
                        body = body + "&nbsp &nbsp Greetings! <br />";
                        body = body + "<br />";
                        body = body + "&nbsp &nbsp The below application has been allocated to you. <br />";
                        body = body + "<br />";
                        body = body + "&nbsp &nbsp <b> Application Number : </b> " + application_no + "  <br />";
                        body = body + "<br />";
                        body = body + "&nbsp &nbsp <b> Customer Name : </b> " + HttpUtility.HtmlEncode(customer_name) + "  <br />";
                        body = body + "<br />";
                        body = body + "&nbsp &nbsp <b> RM Name : </b> " + HttpUtility.HtmlEncode(relationshipmanager_name) + "  <br />";
                        body = body + "<br />";
                        body = body + "&nbsp &nbsp <b> Cluster Head Name : </b> " + HttpUtility.HtmlEncode(cluster_head) + "  <br />";
                        body = body + "<br />";
                        body = body + "&nbsp &nbsp <b> Zonal Head Name : </b> " + HttpUtility.HtmlEncode(zonal_head) + "  <br />";
                        body = body + "<br />";
                        body = body + "&nbsp &nbsp <b> Allocated By :</b>  " + HttpUtility.HtmlEncode(allocated_by) + "  <br />";
                        body = body + "<br />";
                        body = body + "&nbsp &nbsp <b> Allocation Time :</b>  " + creditassigned_date + "  <br />";
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


                        cc_mailid = "";
                        MailMessage message = new MailMessage();
                        SmtpClient smtp = new SmtpClient();
                        message.From = new MailAddress(ls_username);
                        message.To.Add(new MailAddress(tomail_id));
                        lsBccmail_id = ConfigurationManager.AppSettings["ApprovalBccMail"].ToString();

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
                        cc_mailid = "" + nationalmanager_mailid + "," + regionalmanager_mailid + " , " + credithead_mailid + ", " + relationshipmanager_mailid + "";

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
                values.message = "Credit Group Assigned successfully";
                bool credit_cus_mail = objcmt.DaAllocatedtocreditmail(values.application_gid);
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while Assigning Credit Group";
                return false;
            }
        }

        public bool DaGetCreditReassignUpdate(string employee_gid, string user_gid, MdlCreditheadassign values)
        {

            msSQL = "select creditmanager_gid,creditmanager_name,creditregionalmanager_gid,creditregionalmanager_name, " +
                    " creditnationalmanager_gid,creditnationalmanager_name,credithead_gid,credithead_name,remarks,creditgroup_gid,creditgroup_name " +
                    " from ocs_mst_tapplication where application_gid='" + values.application_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lscreditmanger_gid = objODBCDatareader["creditmanager_gid"].ToString();
                lscreditmanager_name = objODBCDatareader["creditmanager_name"].ToString();
                lscreditregionalmanager_gid = objODBCDatareader["creditregionalmanager_gid"].ToString();
                lscreditregionalmanager_name = objODBCDatareader["creditregionalmanager_name"].ToString();
                lscreditnationalmanager_gid = objODBCDatareader["creditnationalmanager_gid"].ToString();
                lscreditnationalmanager_name = objODBCDatareader["creditnationalmanager_name"].ToString();
                lscredithead_gid = objODBCDatareader["credithead_gid"].ToString();
                lscredithead_name = objODBCDatareader["credithead_name"].ToString();
                lsremarks = objODBCDatareader["remarks"].ToString();
                lscreditgroup_gid = objODBCDatareader["creditgroup_gid"].ToString();
                lscreditgroup_name = objODBCDatareader["creditgroup_name"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " Insert into ocs_trn_tAppcreditapprovalreassignlog( " +
                    " application_gid, " +
                    " creditgroup_gid," +
                    " creditgroup_name," +
                    " reassignto_creditgroupgid," +
                    " reassignto_creditgroupname," +
                    " creditmanger_gid," +
                    " creditmanger_name," +
                    " reassignto_creditmanger_gid," +
                    " reassignto_creditmanger_name," +
                    " creditregionalmanager_gid," +
                    " creditregionalmanager_name," +
                    " reassignto_creditregionalmanager_gid," +
                    " reassignto_creditregionalmanager_name," +
                    " creditnationalmanager_gid," +
                    " creditnationalmanager_name," +
                    " reassignto_creditnationalmanager_gid," +
                    " reassignto_creditnationalmanager_name," +
                    " credithead_gid," +
                    " credithead_name," +
                    " reassignto_credithead_gid," +
                    " reassignto_credithead_name," +
                    " remarks, " +
                    " reassign_remarks," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + values.application_gid + "',";
            if (lscreditgroup_gid == values.creditgroup_gid)
            {
                msSQL += "''," +
                         "''," +
                         "''," +
                         "'',";
            }
            else
            {
                msSQL += "'" + lscreditgroup_gid + "'," +
                         "'" + lscreditgroup_name + "'," +
                         "'" + values.creditgroup_gid + "'," +
                         "'" + values.creditgroup_name + "',";
            }
            if (lscreditmanger_gid == values.creditmanager_gid)
            {
                msSQL += "''," +
                         "''," +
                         "''," +
                         "'',";
            }
            else
            {
                msSQL += "'" + lscreditmanger_gid + "'," +
                         "'" + lscreditmanager_name + "'," +
                         "'" + values.creditmanager_gid + "'," +
                         "'" + values.creditmanager_name + "',";
            }
            if (lscreditregionalmanager_gid == values.regionalcredit_gid)
            {
                msSQL += "''," +
                        "''," +
                        "''," +
                        "'',";
            }
            else
            {
                msSQL += "'" + lscreditregionalmanager_gid + "'," +
                         "'" + lscreditregionalmanager_name + "'," +
                         "'" + values.regionalcredit_gid + "'," +
                         "'" + values.regionalcredit_name + "',";
            }
            if (lscreditnationalmanager_gid == values.nationalcredit_gid)
            {
                msSQL += "''," +
                        "''," +
                        "''," +
                        "'',";
            }
            else
            {
                msSQL += "'" + lscreditnationalmanager_gid + "'," +
                         "'" + lscreditnationalmanager_name + "'," +
                         "'" + values.nationalcredit_gid + "'," +
                         "'" + values.nationalcredit_name + "',";
            }
            if (lscredithead_gid == values.credithead_gid)
            {
                msSQL += "''," +
                        "''," +
                        "''," +
                        "'',";
            }
            else
            {
                msSQL += "'" + lscredithead_gid + "'," +
                            "'" + lscredithead_name + "'," +
                            "'" + values.credithead_gid + "'," +
                            "'" + values.credithead_name + "',";
            }

            msSQL += "'" + lsremarks.Replace("'", "") + "'," +
                    "'" + values.remarks.Replace("'", "") + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                msSQL = "update ocs_mst_tapplication set " +
                        " creditgroup_gid='" + values.creditgroup_gid + "'," +
                        " creditgroup_name='" + values.creditgroup_name + "'," +
                        " credithead_gid='" + values.credithead_gid + "'," +
                        " credithead_name='" + values.credithead_name + "'," +
                        " creditnationalmanager_gid='" + values.nationalcredit_gid + "'," +
                        " creditnationalmanager_name ='" + values.nationalcredit_name + "'," +
                        " creditregionalmanager_gid='" + values.regionalcredit_gid + "'," +
                        " creditregionalmanager_name='" + values.regionalcredit_name + "'," +
                        " creditmanager_gid='" + values.creditmanager_gid + "'," +
                        " creditmanager_name='" + values.creditmanager_name + "'," +
                        " remarks ='" + values.remarks.Replace("'", "") + "'," +
                        " creditreassigned_by='" + employee_gid + "'," +
                        " creditreassigned_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                        " where application_gid='" + values.application_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {

                    msSQL = "update ocs_trn_tAppcreditapproval set approval_gid='" + values.credithead_gid + "',approval_name='" + values.credithead_name + "'" +
                           " where application_gid='" + values.application_gid + "' and hierary_level='3' and approval_status!='Approved'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update ocs_trn_tAppcreditapproval set approval_gid='" + values.nationalcredit_gid + "',approval_name='" + values.nationalcredit_name + "'" +
                           " where application_gid='" + values.application_gid + "' and hierary_level='2' and approval_status!='Approved'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update ocs_trn_tAppcreditapproval set approval_gid='" + values.regionalcredit_gid + "',approval_name='" + values.regionalcredit_name + "'" +
                           " where application_gid='" + values.application_gid + "' and hierary_level='1' and approval_status!='Approved'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update ocs_trn_tAppcreditapproval set approval_gid='" + values.creditmanager_gid + "',approval_name='" + values.creditmanager_name + "'" +
                           " where application_gid='" + values.application_gid + "' and hierary_level='0' and approval_status!='Approved'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                values.status = true;
                values.message = "Credit Approval Reassigning successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while Reassigning Credit Approval";
                return false;
            }
        }

        public void DaGetReassignedLog(string application_gid, MdlreassignedlogInfo values)
        {
            msSQL = " SELECT a.application_gid,a.creditmanger_gid ,a.creditmanger_name,a.reassignto_creditmanger_gid,a.reassignto_creditmanger_name, " +
                      " a.creditregionalmanager_gid,a.creditregionalmanager_name,a.reassignto_creditregionalmanager_gid,a.reassignto_creditregionalmanager_name , " +
                      " a.creditnationalmanager_gid,a.creditnationalmanager_name,a.reassignto_creditnationalmanager_gid,a.reassignto_creditnationalmanager_name , " +
                      " a.credithead_gid,a.credithead_name,a.reassignto_credithead_gid,a.reassignto_credithead_name , " +
                      " a.remarks,a.reassign_remarks,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                      " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,creditgroup_gid, " + 
                      " creditgroup_name,reassignto_creditgroupgid,reassignto_creditgroupname " +
                      " FROM ocs_trn_tAppcreditapprovalreassignlog a" +
                      " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                      " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                      " where application_gid = '" + application_gid + "' order by a.created_date asc ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getreassignedlog = new List<reassignedloglist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getreassignedlog.Add(new reassignedloglist
                    {
                        application_gid = (dr_datarow["application_gid"].ToString()),
                        creditmanger_gid = (dr_datarow["creditmanger_gid"].ToString()),
                        creditmanger_name = (dr_datarow["creditmanger_name"].ToString()),
                        reassignto_creditmanger_gid = (dr_datarow["reassignto_creditmanger_gid"].ToString()),
                        reassignto_creditmanger_name = (dr_datarow["reassignto_creditmanger_name"].ToString()),

                        creditregionalmanager_gid = (dr_datarow["creditregionalmanager_gid"].ToString()),
                        creditregionalmanager_name = (dr_datarow["creditregionalmanager_name"].ToString()),
                        reassignto_creditregionalmanager_gid = (dr_datarow["reassignto_creditregionalmanager_gid"].ToString()),
                        reassignto_creditregionalmanager_name = (dr_datarow["reassignto_creditregionalmanager_name"].ToString()),

                        creditnationalmanager_gid = (dr_datarow["creditnationalmanager_gid"].ToString()),
                        creditnationalmanager_name = (dr_datarow["creditnationalmanager_name"].ToString()),
                        reassignto_creditnationalmanager_gid = (dr_datarow["reassignto_creditnationalmanager_gid"].ToString()),
                        reassignto_creditnationalmanager_name = (dr_datarow["reassignto_creditnationalmanager_name"].ToString()),

                        credithead_gid = (dr_datarow["credithead_gid"].ToString()),
                        credithead_name = (dr_datarow["credithead_name"].ToString()),
                        reassignto_credithead_gid = (dr_datarow["reassignto_credithead_gid"].ToString()),
                        reassignto_credithead_name = (dr_datarow["reassignto_credithead_name"].ToString()),

                        remarks = (dr_datarow["remarks"].ToString()),
                        reassign_remarks = (dr_datarow["reassign_remarks"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),

                        creditgroup_gid = (dr_datarow["creditgroup_gid"].ToString()),
                        creditgroup_name = (dr_datarow["creditgroup_name"].ToString()),
                        reassignto_creditgroup_gid = (dr_datarow["reassignto_creditgroupgid"].ToString()),
                        reassignto_creditgroup_name = (dr_datarow["reassignto_creditgroupname"].ToString()),
                    });
                }
                values.reassignedloglist = getreassignedlog;
            }
            dt_datatable.Dispose();
            values.status = true;
        }
        public void DaGetCreditgroupname(string creditmapping_gid, MdlCreditGroup values)
        {
            try
            {
                values.creditgroup_gid = objdbconn.GetExecuteScalar("select creditmapping_gid from ocs_mst_tcreditmapping where creditmapping_gid ='" + creditmapping_gid + "'");
                values.creditgroup_name = objdbconn.GetExecuteScalar("select creditgroup_name from ocs_mst_tcreditmapping where creditmapping_gid ='" + creditmapping_gid + "'");
                msSQL = " SELECT creditgroup_name,creditmapping_gid from ocs_mst_tcreditmapping where status='Y' ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcreditgoupname = new List<creditgoupname>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcreditgoupname.Add(new creditgoupname
                        {
                            creditgroup_gid = (dr_datarow["creditmapping_gid"].ToString()),
                            creditgroup_name = (dr_datarow["creditgroup_name"].ToString()),

                        });
                    }
                    values.creditgoupname = getcreditgoupname;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

      
        public void DaGetCreditMappingLog(string creditmapping_gid, MdlCreditMappingLogInfo values)
        {
            msSQL = " SELECT  a.credithead_gid ,a.credithead_name,a.nationalmanager_gid,a.nationalmanager_name, " +
                      " a.regionalmanager_gid,a.regionalmanager_name,a.creditmanager_gid,a.creditmanager_name, " +
                      " a.new_credithead_gid ,a.new_credithead_name,a.new_nationalmanager_gid,a.new_nationalmanager_name, " +
                      " a.new_regionalmanager_gid,a.new_regionalmanager_name,a.new_creditmanager_gid,a.new_creditmanager_name, a.creditgroup_name, a.old_creditgroup_name," +
                      " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                      " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by " +
                      " FROM ocs_mst_tcreditgrouplog a" +
                      " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                      " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                      //" left join ocs_mst_tcreditmappinglog d on a.creditmapping_gid = d.creditmapping_gid" +
                      " where a.creditmapping_gid = '" + creditmapping_gid + "'group by a.creditgrouplog_gid order by a.created_date desc ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcreditmappingloglist = new List<creditmappingloglist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcreditmappingloglist.Add(new creditmappingloglist
                    {
                        credithead_gid = (dr_datarow["credithead_gid"].ToString()),
                        credithead_name = (dr_datarow["credithead_name"].ToString()),
                        nationalmanager_gid = (dr_datarow["nationalmanager_gid"].ToString()),
                        nationalmanager_name = (dr_datarow["nationalmanager_name"].ToString()),
                        regionalmanager_gid = (dr_datarow["regionalmanager_gid"].ToString()),

                        regionalmanager_name = (dr_datarow["regionalmanager_name"].ToString()),
                        creditmanager_gid = (dr_datarow["creditmanager_gid"].ToString()),
                        creditmanager_name = (dr_datarow["creditmanager_name"].ToString()),

                        new_credithead_gid = (dr_datarow["new_credithead_gid"].ToString()),
                        new_credithead_name = (dr_datarow["new_credithead_name"].ToString()),
                        new_nationalmanager_gid = (dr_datarow["new_nationalmanager_gid"].ToString()),
                        new_nationalmanager_name = (dr_datarow["new_nationalmanager_name"].ToString()),
                        new_regionalmanager_gid = (dr_datarow["new_regionalmanager_gid"].ToString()),

                        new_regionalmanager_name = (dr_datarow["new_regionalmanager_name"].ToString()),
                        new_creditmanager_gid = (dr_datarow["new_creditmanager_gid"].ToString()),
                        new_creditmanager_name = (dr_datarow["new_creditmanager_name"].ToString()),

                        created_by = (dr_datarow["created_by"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),


                        creditgroup_name = (dr_datarow["creditgroup_name"].ToString()),
                        old_creditgroup_name = (dr_datarow["old_creditgroup_name"].ToString()),
                    });
                }
                values.creditmappingloglist = getcreditmappingloglist;
            }
            dt_datatable.Dispose();
            values.status = true;
        }

        public void DaExportMstCreditMapping(MstApplicationReport objMstApplicationReport)
        {
            msSQL = " select api_code,creditgroup_id as 'Credit Group ID', creditgroup_name as 'Credit Group Name', " +
                    " GROUP_CONCAT(DISTINCT(e.employee_name)) as 'Credit Head',  " +
                    " GROUP_CONCAT(DISTINCT(b.employee_name)) as 'National Credit Manager', " +
                    " GROUP_CONCAT(DISTINCT(c.employee_name)) as 'Regional Credit Manager',  " +
                    " GROUP_CONCAT(DISTINCT(d.employee_name)) as 'Credit Manager' ,  " +
                    " concat(f.user_firstname, ' ', f.user_lastname, ' / ', f.user_code) as 'Created By', " +
                    " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as 'Created Date'," +
                    " concat(i.user_firstname, ' ', i.user_lastname, ' / ', i.user_code) as 'Updated By',  " +
                    " date_format(a.updated_date, '%d-%m-%Y %h:%i %p') as 'Updated Date'" +
                    " from ocs_mst_tcreditmapping a " +
                    " LEFT JOIN ocs_mst_tcredit2credithead e ON a.creditmapping_gid = e.creditmapping_gid " +
                    " LEFT JOIN ocs_mst_tcredit2nationalmanager b ON a.creditmapping_gid = b.creditmapping_gid " +
                    " LEFT JOIN ocs_mst_tcreditr2regionalmanager c ON c.creditmapping_gid = a.creditmapping_gid " +
                    " LEFT JOIN ocs_mst_tcredit2creditmanager d ON a.creditmapping_gid = d.creditmapping_gid " +
                    " left join hrm_mst_temployee g on a.created_by = g.employee_gid " +
                    " left join adm_mst_tuser f on f.user_gid = g.user_gid " +
                    " left join hrm_mst_temployee h on a.updated_by = h.employee_gid " +
                    " left join adm_mst_tuser i on i.user_gid = h.user_gid " +
                    " group by a.creditmapping_gid ";


            dt_datatable = objdbconn.GetDataTable(msSQL);

            string lscompany_code = string.Empty;

            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            var workSheet = excel.Workbook.Worksheets.Add("CreditMapping Report");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                objMstApplicationReport.lsname = "CreditMapping Report.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/CreditMapping Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                objMstApplicationReport.lscloudpath =  lscompany_code + "/" + "Master/CreditMapping Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstApplicationReport.lsname;
                objMstApplicationReport.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/CreditMapping Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstApplicationReport.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(objMstApplicationReport.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 11])  //Address "A1:X1"

                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);

                }
                excel.SaveAs(ms);
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", objMstApplicationReport.lscloudpath, ms);
                ms.Close();
            }
            catch (Exception ex)
            {
                objMstApplicationReport.status = false;
                objMstApplicationReport.message = "Failure";
            }
            objMstApplicationReport.lscloudpath = objcmnstorage.EncryptData(objMstApplicationReport.lscloudpath);
            objMstApplicationReport.lspath = objcmnstorage.EncryptData(objMstApplicationReport.lspath);
            objMstApplicationReport.status = true;
            objMstApplicationReport.message = "Success";
        }


        public void DaGetCreditGroupTitleList(string creditmapping_gid, CreditGroupTitle_list objmaster)
        {
            try
            {
                msSQL = " select creditmapping_gid,creditgroup_name,creditgroup_id, status as creditgroup_status from ocs_mst_tcreditmapping " +
                        " where creditmapping_gid='" + creditmapping_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objmaster.creditgroup_name = objODBCDatareader["creditgroup_name"].ToString();
                    objmaster.creditgroup_id = objODBCDatareader["creditgroup_id"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = " SELECT a.grouptitle_name,a.grouptitle_gid FROM ocs_mst_tgrouptitle a where status='Y' order by a.grouptitle_name asc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcreditgroup_list = new List<GroupTitle_dtl>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcreditgroup_list.Add(new GroupTitle_dtl
                        {
                            grouptitle_name = (dr_datarow["grouptitle_name"].ToString()),
                            grouptitle_gid = (dr_datarow["grouptitle_gid"].ToString()),
                        });
                    }
                    objmaster.GroupTitle_dtl = getcreditgroup_list;
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
            }
        }

        public void DaPostCreateCreditRule(MdlGroupTitleQuestion values, string employee_gid)
        {
            string lsquestion_option = "";
            msGetGid = objcmnfunctions.GetMasterGID("CQRG");
            if (values.listarray != null)
            {
                lsquestion_option = string.Join(",", values.listarray.Select(item => "" + item.list_name + ""));
            }
            if (values.answer_type!= "Calculation")
            {
                values.simplify_formula = "";
            }

            msSQL = " insert into ocs_mst_tcreditquestionrule(" +
                        " creditquestionrule_gid," +
                        " creditmapping_gid," +
                        " grouptitle_gid," +
                        " grouptitle_name," +
                        " question," +
                        " answer_type," +
                        " question_option, " +
                        " number_score, " +
                        " calculation_formula, " +
                        " simplify_formula, " +
                        " addfinal_score, " +
                        " hidden_question, " +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + values.creditmapping_gid + "'," +
                        "'" + values.grouptitle_gid + "'," +
                        "'" + values.grouptitle_name.Replace("'", "\\'") + "'," +
                        "'" + values.question + "'," +
                        "'" + values.answer_type + "'," +
                        "'" + lsquestion_option + "'," +
                        "'" + values.number_score + "'," +
                        "'" + values.calculation_formula + "'," +
                        "'" + values.simplify_formula + "'," +
                        "'" + values.addfinal_score + "'," +
                        "'" + values.hidden_question + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            if (mnResult != 0)
            {
                if (values.listarray != null)
                {
                    foreach (var data in values.listarray)
                    {
                        string msGetOptionGid = objcmnfunctions.GetMasterGID("QLOG");

                        msSQL = " insert into ocs_mst_tquestionlistoption(" +
                            " questionlistoption_gid, " +
                           " creditquestionrule_gid," +
                           " list_name," +
                           " score," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetOptionGid + "'," +
                           "'" + msGetGid + "'," +
                           "'" + data.list_name.Replace("'", "\\'") + "'," +
                           "'" + data.Score + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }

                if (values.calculation_dtl != null)
                {
                    foreach (var data in values.calculation_dtl)
                    {
                        string msGetCalcGid = objcmnfunctions.GetMasterGID("QCDG");

                        msSQL = " insert into ocs_mst_tquestioncalculationdtl(" +
                           " questioncalculationdtl_gid," +
                           " creditquestionrule_gid, " +
                           " question_gid," +
                           " question," +
                           " grouptitle_gid," +
                           " grouptitle_name," +
                           " field_type," +
                           " constant_value, " +
                           " operations," +
                           " simplify_key, " +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetCalcGid + "'," +
                           "'" + msGetGid + "'," +
                           "'" + data.question_gid + "'," +
                           "'" + data.question.Replace("'", "\\'") + "'," +
                           "'" + data.grouptitle_gid + "'," +
                           "'" + data.grouptitle_name.Replace("'", "\\'") + "'," +
                           "'" + data.field_type + "'," +
                           "'" + data.constantvalue + "'," +
                           "'" + data.operations + "'," +
                           "'" + data.simplify_key + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }

                values.status = true;
                values.message = "Credit Mapping - Rule Added Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Adding";

            }
        }

        public void DaGetCreditquestionsummary(string creditmapping_gid, MdlGroupTitleQuestion_list objmaster)
        {
            try
            {
                msSQL = " SELECT a.creditquestionrule_gid,a.grouptitle_gid,a.grouptitle_name,question,answer_type,hidden_question, " +
                        " addfinal_score, calculation_formula,simplify_formula,group_order,question_order " +
                        " FROM ocs_mst_tcreditquestionrule a  " +
                        " where creditmapping_gid='" + creditmapping_gid + "' " +
                        " ORDER BY CAST(group_order AS UNSIGNED ) asc, " +
                        " CAST(question_order AS UNSIGNED) asc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcreditgroup_list = new List<MdlGroupTitleQuestion>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcreditgroup_list.Add(new MdlGroupTitleQuestion
                        {
                            creditquestionrule_gid = (dr_datarow["creditquestionrule_gid"].ToString()),
                            grouptitle_name = (dr_datarow["grouptitle_name"].ToString()),
                            question = (dr_datarow["question"].ToString()),
                            answer_type = (dr_datarow["answer_type"].ToString()),
                            calculation_formula = (dr_datarow["calculation_formula"].ToString()),
                            grouptitle_gid = (dr_datarow["grouptitle_gid"].ToString()),
                            simplify_formula = (dr_datarow["simplify_formula"].ToString()),
                            addfinal_score = (dr_datarow["addfinal_score"].ToString()),
                            hidden_question = (dr_datarow["hidden_question"].ToString()),
                            group_order = (dr_datarow["group_order"].ToString()),
                            question_order = (dr_datarow["question_order"].ToString()),
                        });
                    }
                    objmaster.MdlGroupTitleQuestion = getcreditgroup_list;
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
            }
        }

        public void DaGetquestionlistsummary(string creditquestionrule_gid, MdlGroupTitleQuestion values)
        {
            try
            {
                msSQL = " select questionlistoption_gid,list_name,score from ocs_mst_tquestionlistoption where creditquestionrule_gid='" + creditquestionrule_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcreditgroup_list = new List<listarray>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcreditgroup_list.Add(new listarray
                        {
                            questionlistoption_gid = (dr_datarow["questionlistoption_gid"].ToString()),
                            list_name = (dr_datarow["list_name"].ToString()),
                            Score = (dr_datarow["score"].ToString()),
                        });
                    }
                    values.listarray = getcreditgroup_list;
                }
                dt_datatable.Dispose();

                msSQL = " select simplify_key,grouptitle_name,question,field_type,constant_value,operations from ocs_mst_tquestioncalculationdtl " +
                        " where creditquestionrule_gid='" + creditquestionrule_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcalculation_dtl = new List<calculation_dtl>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcalculation_dtl.Add(new calculation_dtl
                        {
                            simplify_key = (dr_datarow["simplify_key"].ToString()),
                            question = (dr_datarow["question"].ToString()),
                            field_type = (dr_datarow["field_type"].ToString()),
                            constantvalue = (dr_datarow["constant_value"].ToString()),
                            operations = (dr_datarow["operations"].ToString()),
                            grouptitle_name = (dr_datarow["grouptitle_name"].ToString()),
                        });
                    }
                    values.calculation_dtl = getcalculation_dtl;
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
            }
        }


        public void DaGetCreditScorecarddtl(string application_gid, MdlCreditGroupQuestiondtl values)
        {
            msSQL = " select creditgroup_gid from ocs_mst_tapplication where application_gid='" + application_gid + "'";
            string lscreditgroup_gid = objdbconn.GetExecuteScalar(msSQL);

            if (lscreditgroup_gid != "")
            {
                try
                {
                    msSQL = " select grouptitle_gid, grouptitle_name from ocs_mst_tcreditquestionrule " +
                            " where creditmapping_gid='" + lscreditgroup_gid + "'  group by grouptitle_gid " +
                            " order by CAST(group_order AS UNSIGNED ) asc, CAST(question_order AS UNSIGNED ) asc ";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getcreditgroup_list = new List<GroupTitle_dtl>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dr_datarow in dt_datatable.Rows)
                        {
                            var footer_flag = "N";
                            if (dr_datarow["grouptitle_gid"].ToString() == "TRS006")
                                footer_flag = "Y";

                            getcreditgroup_list.Add(new GroupTitle_dtl
                            {
                                grouptitle_name = (dr_datarow["grouptitle_name"].ToString()),
                                grouptitle_gid = (dr_datarow["grouptitle_gid"].ToString()),
                                footer_flag = footer_flag
                            });
                        }
                        values.GroupTitle_dtl = getcreditgroup_list;
                    }

                    msSQL = " SELECT a.creditquestionrule_gid,a.grouptitle_gid,a.grouptitle_name,question,answer_type, " +
                            " addfinal_score,hidden_question, calculation_formula  FROM ocs_mst_tcreditquestionrule a  " +
                            " where creditmapping_gid='" + lscreditgroup_gid + "' " +
                            " order by CAST(group_order AS UNSIGNED ) asc, CAST(question_order AS UNSIGNED ) asc ";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getcreditgroupques_list = new List<MdlCreditGroupTitleQuestion>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dr_datarow in dt_datatable.Rows)
                        {
                            getcreditgroupques_list.Add(new MdlCreditGroupTitleQuestion
                            {
                                creditquestionrule_gid = (dr_datarow["creditquestionrule_gid"].ToString()),
                                question = (dr_datarow["question"].ToString()),
                                answer_type = (dr_datarow["answer_type"].ToString()),
                                calculation_formula = (dr_datarow["calculation_formula"].ToString()),
                                grouptitle_gid = (dr_datarow["grouptitle_gid"].ToString()),
                                addfinal_score = (dr_datarow["addfinal_score"].ToString()),
                                hidden_question = (dr_datarow["hidden_question"].ToString())
                            });
                        }
                        values.MdlCreditGroupTitleQuestion = getcreditgroupques_list;
                    }
                    dt_datatable.Dispose();

                    msSQL = " select a.creditquestionrule_gid, questionlistoption_gid,list_name,score from ocs_mst_tquestionlistoption a " +
                            " left join ocs_mst_tcreditquestionrule b on a.creditquestionrule_gid = b.creditquestionrule_gid " +
                            " where b.creditmapping_gid='" + lscreditgroup_gid + "' group by a.questionlistoption_gid";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getcreditgrouparray_list = new List<listarray>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dr_datarow in dt_datatable.Rows)
                        {
                            getcreditgrouparray_list.Add(new listarray
                            {
                                creditquestionrule_gid = (dr_datarow["creditquestionrule_gid"].ToString()),
                                questionlistoption_gid = (dr_datarow["questionlistoption_gid"].ToString()),
                                list_name = (dr_datarow["list_name"].ToString()),
                                Score = (dr_datarow["score"].ToString()),
                            });
                        }
                        values.listarray = getcreditgrouparray_list;
                    }
                    dt_datatable.Dispose();
                }
                catch (Exception ex)
                {
                }
            }
        }

        public void DaGetCreditQuestionScore(List<MdlCreditGroupScore> values, MdlCreditGroupScoredtl data, string lscreditquestionrule_gid)
        {
            List<calculation_dtl> objmaster = new List<calculation_dtl>();
            List<arraycalculation> objcalculation = new List<arraycalculation>();
            //string question_gid = "", field_type = "", operations = "", constant_value = "";
            string lsquestion_gid, lsfield_type, lsconstant_value = "", lsvalue = "";
            //int ActualScore = 0, CalculateScore = 0, Constant = 0;
            object CalculateScore = "0";
            string simplify_key = "";
            string lscalculation_formula = "";
            List<calculationinfo> calculationdtl = new List<calculationinfo>();
            List<calculationinfo> calculationdtl1 = new List<calculationinfo>();
            List<GroupQuestion_list> getcalculationques = new List<GroupQuestion_list>();
            try
            {
                msSQL = " select a.creditquestionrule_gid,a.question_gid,b.grouptitle_gid,answer_type from ocs_mst_tquestioncalculationdtl a  " +
                        " left join ocs_mst_tcreditquestionrule b on a.creditquestionrule_gid = b.creditquestionrule_gid " +
                        " where a.creditquestionrule_gid  in ('" + lscreditquestionrule_gid + "') or a.question_gid in ('" + lscreditquestionrule_gid + "') " +
                        " or a.question_gid in (select creditquestionrule_gid " +
                        " from ocs_mst_tquestioncalculationdtl where question_gid in ('" + lscreditquestionrule_gid + "'))";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    calculationdtl = dt_datatable.AsEnumerable().Select(row =>
                   new calculationinfo
                   {
                       creditquestionrule_gid = row["creditquestionrule_gid"].ToString(),
                       question_gid = row["question_gid"].ToString(),
                       grouptitle_gid = row["grouptitle_gid"].ToString()
                   }).ToList();
                }
                dt_datatable.Dispose();
                string lscreditquestiongid = string.Join(",", calculationdtl.Select(item => "'" + item.creditquestionrule_gid + "'"));

                var count = 1;
                for (int i = 0; count != 0; i++)
                { 
                    msSQL = " select a.creditquestionrule_gid,a.question_gid,b.grouptitle_gid from ocs_mst_tquestioncalculationdtl a  " +
                       " left join ocs_mst_tcreditquestionrule b on a.creditquestionrule_gid = b.creditquestionrule_gid " +
                       " where a.question_gid in (select creditquestionrule_gid " +
                       " from ocs_mst_tquestioncalculationdtl where question_gid in (" + lscreditquestiongid + "))";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable.Rows.Count != 0)
                    {
                        calculationdtl1 = dt_datatable.AsEnumerable().Select(row =>
                       new calculationinfo
                       {
                           creditquestionrule_gid = row["creditquestionrule_gid"].ToString(),
                           question_gid = row["question_gid"].ToString(),
                           grouptitle_gid = row["grouptitle_gid"].ToString()
                       }).ToList();
                    }
                    dt_datatable.Dispose();
                    count = dt_datatable.Rows.Count;
                    lscreditquestiongid = string.Join(",", calculationdtl1.Select(item => "'" + item.creditquestionrule_gid + "'"));
                    if (count != 0)
                        calculationdtl.AddRange(calculationdtl1);
                    else if (count == 0)
                        break;

                }
                foreach (var k in calculationdtl)
                {
                    msSQL = " select simplify_formula from ocs_mst_tcreditquestionrule where creditquestionrule_gid='" + k.creditquestionrule_gid + "'";
                    lscalculation_formula = objdbconn.GetExecuteScalar(msSQL);
                    msSQL = " select group_concat(simplify_key) from ocs_mst_tquestioncalculationdtl where creditquestionrule_gid='" + k.creditquestionrule_gid + "'";
                    var lskey = objdbconn.GetExecuteScalar(msSQL);
                    var tok = lskey.Split(',').Reverse().ToList<string>();
                    //string ls_formula = lscalculation_formula.Replace("FLOOR", string.Empty).Replace("MIN", string.Empty).Replace("MAX", string.Empty).Replace("OR", string.Empty).Replace("ISBLANK", string.Empty).Replace("AND", string.Empty).Replace("IF", string.Empty).Trim();
                    //var tok = Regex.Split(ls_formula, @"(?<=[=><,+*/-])|(?=[=><,+*/-])");
                    //string addition = "+", subtraction = "-", multiplication = "*", division = "/", Comma = ",", greater = ">", lesser = "<", equalto = "="; 
                    //tok = tok.Where(e => e != addition).ToArray();
                    //tok = tok.Where(e => e != subtraction).ToArray();
                    //tok = tok.Where(e => e != multiplication).ToArray();
                    //tok = tok.Where(e => e != division).ToArray();
                    //tok = tok.Where(e => e != Comma).ToArray();
                    //tok = tok.Where(e => e != greater).ToArray();
                    //tok = tok.Where(e => e != lesser).ToArray();
                    //tok = tok.Where(e => e != equalto).ToArray(); 

                    foreach (string match in tok)
                    {
                        simplify_key = match.Replace(")", string.Empty).Replace("(", string.Empty).Replace(",", string.Empty).Trim();

                        msSQL = " select question_gid,field_type,constant_value from ocs_mst_tquestioncalculationdtl where simplify_key ='" + simplify_key + "'" +
                                " and creditquestionrule_gid='" + k.creditquestionrule_gid + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsvalue = "0";
                            lsquestion_gid = objODBCDatareader["question_gid"].ToString();
                            lsfield_type = objODBCDatareader["field_type"].ToString();
                            lsconstant_value = objODBCDatareader["constant_value"].ToString();
                            if (lsfield_type == "Score" || lsfield_type == "Actual")
                            {
                                var getactualvalue1 = calculationdtl.Where(a => a.creditquestionrule_gid == lsquestion_gid).ToList();
                                if (getactualvalue1.Count != 0 && getactualvalue1[0].final_score != null)
                                    lsvalue = getactualvalue1[0].final_score;
                                else
                                {
                                    foreach (var j in data.GroupTitle_list)
                                    {
                                        GroupQuestion_list getactualvalue = j.GroupQuestion_list.Where(a => a.creditquestionrule_gid == lsquestion_gid).FirstOrDefault();
                                        if (getactualvalue != null)
                                        {
                                            lsvalue = getactualvalue.final_score;
                                            if (getactualvalue.final_score == "" || getactualvalue.final_score == null)
                                            {
                                                lsvalue = getactualvalue.final_scoredisplay;
                                            }
                                        }
                                    }
                                }

                            }
                            else if (lsfield_type == "Constant")
                            {
                                lsvalue = lsconstant_value;
                            }
                            if (lsvalue == "")
                                lsvalue = "0";

                            objcalculation.Add(new arraycalculation
                            {
                                key = simplify_key,
                                value = Convert.ToDecimal(lsvalue),
                            });

                        }
                        objODBCDatareader.Close();
                    }
                    if (objcalculation.Count != 0)
                    {
                        try
                        {
                            Expression expr = new Expression(lscalculation_formula);
                            foreach (var j in objcalculation)
                            {
                                expr.Bind(j.key, j.value);
                            }
                            Object value = expr.Eval();
                            CalculateScore = value;
                            CalculateScore = Math.Round(Convert.ToDouble(CalculateScore), 2);
                            k.final_score = Convert.ToString(CalculateScore);
                            values.Add(new MdlCreditGroupScore
                            {
                                creditquestionrule_gid = k.creditquestionrule_gid,
                                question_score = CalculateScore
                            });
                            objcalculation.Clear();
                        }
                        catch (Exception ex)
                        {
                            objcalculation.Clear();
                        }
                    }

                }

            }
            catch (Exception ex)
            {
            }
        }


        public void DaGetDeleteQuestionList(string creditquestionrule_gid, result objvalues)
        {
            try
            {
                msSQL = " select questioncalculationdtl_gid from ocs_mst_tquestioncalculationdtl a " +
                        " left join ocs_mst_tcreditquestionrule b on a.creditquestionrule_gid =  b.creditquestionrule_gid and a.grouptitle_gid = b.grouptitle_gid " +
                        " where a.question_gid = '" + creditquestionrule_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objODBCDatareader.Close();
                    objvalues.status = false;
                    objvalues.message = "You cannot delete. This question already mapped to the Calculation.";
                }
                else
                {
                    objODBCDatareader.Close();
                    msSQL = " delete from ocs_mst_tcreditquestionrule where creditquestionrule_gid ='" + creditquestionrule_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnResult == 1)
                    {
                        msSQL = " delete from ocs_mst_tquestionlistoption where creditquestionrule_gid ='" + creditquestionrule_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " delete from ocs_mst_tquestioncalculationdtl where creditquestionrule_gid ='" + creditquestionrule_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        objvalues.status = true;
                        objvalues.message = "Question details are deleted successfully..!";
                    }
                    else
                    {
                        objvalues.status = true;
                        objvalues.message = "Error occured..!";
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        public void DaGetCreditGroupList(string creditmapping_gid, MdlCreditGroupdtl values)
        {
            msSQL = " select grouptitle_gid,grouptitle_name,group_order from ocs_mst_tcreditquestionrule  " +
                    " where creditmapping_gid = '" + creditmapping_gid + "' group by grouptitle_gid order by group_order asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcreditgroup_list = new List<GroupTitle_dtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcreditgroup_list.Add(new GroupTitle_dtl
                    {
                        grouptitle_name = (dr_datarow["grouptitle_name"].ToString()),
                        grouptitle_gid = (dr_datarow["grouptitle_gid"].ToString()),
                        group_order = (dr_datarow["group_order"].ToString()),
                    });
                }
                values.GroupTitle_dtl = getcreditgroup_list;
            }

        }

        public void DaUpdateGroupOrder(MdlCreditGroupdtl values, string employee_gid)
        {
            if (values.GroupTitle_dtl != null)
            {
                foreach (var data in values.GroupTitle_dtl)
                {
                    msSQL = " update ocs_mst_tcreditquestionrule set group_order='" + data.group_order + "', " +
                            " updated_by='" + employee_gid + "'," +
                            " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                            " where grouptitle_gid ='" + data.grouptitle_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Group order updated successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";

            }
        }

        public void DaUpdateGroupQuestionOrder(MdlGroupTitleQuestion_list values, string employee_gid)
        {
            if (values.MdlGroupTitleQuestion != null)
            {
                foreach (var data in values.MdlGroupTitleQuestion)
                {
                    msSQL = " update ocs_mst_tcreditquestionrule set question_order='" + data.question_order + "', " +
                            " updated_by='" + employee_gid + "'," +
                            " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                            " where creditquestionrule_gid ='" + data.creditquestionrule_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Group question order updated successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";

            }
        }

        public void DaSubmitScoreCard(MdlCreditGroupScoredtl values, string employee_gid)
        {

            msSQL = " select creditgroup_gid from ocs_mst_tapplication where application_gid='" + values.application_gid + "'";
            string lscreditgroup_gid = objdbconn.GetExecuteScalar(msSQL);

            if (lscreditgroup_gid != "")
            {
                msSQL = " Insert into ocs_trn_tscorecardgrouptitle (application_gid,creditmapping_gid, grouptitle_gid,grouptitle_name,  " +
                       " created_by, created_date) " +
                       " select '" + values.application_gid + "','" + lscreditgroup_gid + "', grouptitle_gid, grouptitle_name, " +
                       " '" + employee_gid + "', now()  " +
                       " from ocs_mst_tcreditquestionrule " +
                       " where creditmapping_gid='" + lscreditgroup_gid + "'  group by grouptitle_gid " +
                       " order by CAST(group_order AS UNSIGNED ) asc, CAST(question_order AS UNSIGNED ) asc ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " insert into ocs_trn_tcreditquestionrule (application_gid,creditquestionrule_gid,creditmapping_gid,grouptitle_gid , " +
                        " grouptitle_name, question, answer_type, question_option, number_score, calculation_formula, simplify_formula, " +
                        " addfinal_score, hidden_question, group_order, question_order, " +
                        " created_by, created_date) " +
                        " select '" + values.application_gid + "',creditquestionrule_gid,creditmapping_gid,grouptitle_gid, " +
                        " grouptitle_name, question, answer_type,question_option,number_score, calculation_formula,simplify_formula, " +
                          " addfinal_score, hidden_question, group_order, question_order, " +
                        " '" + employee_gid + "', now()  " +
                        " from ocs_mst_tcreditquestionrule " +
                        " where creditmapping_gid= '" + lscreditgroup_gid + "'"; 
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " insert into ocs_trn_tquestioncalculationdtl (application_gid,questioncalculationdtl_gid,creditquestionrule_gid,question_gid , " +
                       " question, field_type, constant_value, operations, grouptitle_gid, grouptitle_name, simplify_key, " + 
                       " created_by, created_date) " +
                       " select '" + values.application_gid + "',questioncalculationdtl_gid,creditquestionrule_gid,question_gid , " +
                       " question, field_type, constant_value, operations, grouptitle_gid, grouptitle_name, simplify_key, " +
                       " '" + employee_gid + "', now()  " +
                       " from ocs_mst_tquestioncalculationdtl " +
                       " where creditquestionrule_gid in (select creditquestionrule_gid from ocs_mst_tcreditquestionrule where creditmapping_gid= '" + lscreditgroup_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " insert into ocs_trn_tquestionlistoption (application_gid,questionlistoption_gid,creditquestionrule_gid,list_name , " +
                        " score, created_by, created_date) " +
                        " select '" + values.application_gid + "',questionlistoption_gid,creditquestionrule_gid,list_name, " +
                        " score, '" + employee_gid + "', now()  " +
                        " from ocs_mst_tquestionlistoption " +
                        " where creditquestionrule_gid in (select creditquestionrule_gid from ocs_mst_tcreditquestionrule where creditmapping_gid= '" + lscreditgroup_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (values.GroupTitle_list != null)
                {
                    foreach (var data in values.GroupTitle_list)
                    {
                        msSQL = " update ocs_trn_tscorecardgrouptitle set final_score='" + data.final_scoredisplay + "'  " + 
                                " where application_gid ='" + values.application_gid +"' and " +
                                " grouptitle_gid ='" + data.grouptitle_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        foreach (var i in data.GroupQuestion_list)
                        {
                            string lsactualvalue_gid = "", lsactual_value = "";
                            if (i.DropdownListArray != null)
                            {
                                lsactualvalue_gid = i.DropdownListArray[0].questionlistoption_gid;
                                lsactual_value = i.DropdownListArray[0].list_name;
                            }
                            else
                            {
                                lsactual_value = i.field_number;
                            }
                            msSQL = " update ocs_trn_tcreditquestionrule set final_score='" + i.final_scoredisplay + "', " +
                                    " actualvalue_gid ='" + lsactualvalue_gid + "'," +
                                    " actual_value='" + lsactual_value + "'," +
                                    " actual_score='" + i.Score + "'," +
                                    " actual_number='" + i.actual_number + "'" +
                                    " where application_gid ='" + values.application_gid + "' and " +
                                    " creditquestionrule_gid ='" + i.creditquestionrule_gid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }

                   
                }
            } 

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Score Card Submitted successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";

            }
        }

        public void DaGetCreditScorecardViewdtl(string application_gid, MdlCreditGroupQuestiondtl values)
        { 
            try
            {
                msSQL = " select grouptitle_gid, grouptitle_name,final_score from ocs_trn_tscorecardgrouptitle " +
                        " where application_gid='" + application_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcreditgroup_list = new List<GroupTitle_dtl>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    { 
                        getcreditgroup_list.Add(new GroupTitle_dtl
                        {
                            grouptitle_name = (dr_datarow["grouptitle_name"].ToString()),
                            grouptitle_gid = (dr_datarow["grouptitle_gid"].ToString()),
                            final_scoredisplay = (dr_datarow["final_score"].ToString())
                        });
                    }
                    values.GroupTitle_dtl = getcreditgroup_list;
                }

                msSQL = " SELECT a.creditquestionrule_gid,a.grouptitle_gid,a.grouptitle_name,question,answer_type, " +
                        " addfinal_score,hidden_question, calculation_formula,actual_value,actualvalue_gid,actual_score,final_score, " +
                        " actual_number FROM ocs_trn_tcreditquestionrule a  " +
                        " where application_gid='" + application_gid + "' " +
                        " order by CAST(group_order AS UNSIGNED ) asc, CAST(question_order AS UNSIGNED ) asc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcreditgroupques_list = new List<MdlCreditGroupTitleQuestion>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcreditgroupques_list.Add(new MdlCreditGroupTitleQuestion
                        {
                            creditquestionrule_gid = (dr_datarow["creditquestionrule_gid"].ToString()),
                            question = (dr_datarow["question"].ToString()),
                            answer_type = (dr_datarow["answer_type"].ToString()),
                            calculation_formula = (dr_datarow["calculation_formula"].ToString()),
                            grouptitle_gid = (dr_datarow["grouptitle_gid"].ToString()),
                            addfinal_score = (dr_datarow["addfinal_score"].ToString()),
                            hidden_question = (dr_datarow["hidden_question"].ToString()),
                            actual_score = (dr_datarow["actual_score"].ToString()),
                            actual_value = (dr_datarow["actual_value"].ToString()),
                            actual_number = (dr_datarow["actual_number"].ToString()),
                            final_scoredisplay = (dr_datarow["final_score"].ToString()),
                            actualvalue_gid = (dr_datarow["actualvalue_gid"].ToString()),
                        });
                    }
                    values.MdlCreditGroupTitleQuestion = getcreditgroupques_list;
                }
                dt_datatable.Dispose();

                msSQL = " select a.creditquestionrule_gid, questionlistoption_gid,list_name,score from ocs_trn_tquestionlistoption a " +
                        " left join ocs_trn_tcreditquestionrule b on a.creditquestionrule_gid = b.creditquestionrule_gid " +
                        " where b.application_gid='" + application_gid + "' group by a.questionlistoption_gid";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcreditgrouparray_list = new List<listarray>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcreditgrouparray_list.Add(new listarray
                        {
                            creditquestionrule_gid = (dr_datarow["creditquestionrule_gid"].ToString()),
                            questionlistoption_gid = (dr_datarow["questionlistoption_gid"].ToString()),
                            list_name = (dr_datarow["list_name"].ToString()),
                            Score = (dr_datarow["score"].ToString()),
                        });
                    }
                    values.listarray = getcreditgrouparray_list;
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
            }
        }

        public void DaSaveScoreCard(MdlCreditGroupScoredtl values, string employee_gid)
        {
            if (values.GroupTitle_list != null)
            {
                foreach (var data in values.GroupTitle_list)
                {
                    msSQL = " update ocs_trn_tscorecardgrouptitle set final_score='" + data.final_scoredisplay + "'  " +
                            " where application_gid ='" + values.application_gid + "' and " +
                            " grouptitle_gid ='" + data.grouptitle_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    foreach (var i in data.GroupQuestion_list)
                    {
                        string lsactualvalue_gid = "", lsactual_value = "";
                        if (i.DropdownListArray != null)
                        {
                            lsactualvalue_gid = i.DropdownListArray[0].questionlistoption_gid;
                            lsactual_value = i.DropdownListArray[0].list_name;
                        }
                        else
                        {
                            lsactual_value = i.field_number;
                        }
                        msSQL = " update ocs_trn_tcreditquestionrule set final_score='" + i.final_scoredisplay + "', " +
                                " actualvalue_gid ='" + lsactualvalue_gid + "'," +
                                " actual_value='" + lsactual_value + "'," +
                                " actual_score='" + i.Score + "'," +
                                " actual_number='" + i.actual_number + "'" +
                                " where application_gid ='" + values.application_gid + "' and " +
                                " creditquestionrule_gid ='" + i.creditquestionrule_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }  
            }

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Score Card Details are Updated successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";

            }
        }

        public void DaGetVerticalGroupTitleList(string vertical_gid, VerticalTitle_list objmaster)
        {
            try
            {
                msSQL = " select vertical_gid,vertical_name,vertical_code from ocs_mst_tvertical " +
                        " where vertical_gid='" + vertical_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objmaster.vertical_name = objODBCDatareader["vertical_name"].ToString();
                    objmaster.vertical_code = objODBCDatareader["vertical_code"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = " SELECT a.grouptitle_name,a.grouptitle_gid FROM ocs_mst_tgrouptitle a where status='Y' order by a.grouptitle_name asc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getverticalgroup_list = new List<GroupVerticalTitle_dtl>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getverticalgroup_list.Add(new GroupVerticalTitle_dtl
                        {
                            grouptitle_name = (dr_datarow["grouptitle_name"].ToString()),
                            grouptitle_gid = (dr_datarow["grouptitle_gid"].ToString()),
                        });
                    }
                    objmaster.GroupTitle_dtl = getverticalgroup_list;
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
            }
        }

        public void DaGetVerticalquestionsummary(string vertical_gid, string applicant_type, MdlVerticalGroupTitleQuestion_list objmaster)
        {
            try
            {
                msSQL = " SELECT a.verticalquestionrule_gid,a.grouptitle_gid,a.grouptitle_name,question,answer_type,hidden_question, " +
                        " addfinal_score, calculation_formula,simplify_formula,group_order,question_order,applicant_type " +
                        " FROM ocs_mst_tverticalquestionrule a  " +
                        " where vertical_gid='" + vertical_gid + "' and applicant_type='" + applicant_type + "' " +
                        " ORDER BY CAST(group_order AS UNSIGNED ) asc, " +
                        " CAST(question_order AS UNSIGNED) asc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getverticalgroup_list = new List<MdlVerticalGroupTitleQuestion>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getverticalgroup_list.Add(new MdlVerticalGroupTitleQuestion
                        {
                            verticalquestionrule_gid = (dr_datarow["verticalquestionrule_gid"].ToString()),
                            grouptitle_name = (dr_datarow["grouptitle_name"].ToString()),
                            question = (dr_datarow["question"].ToString()),
                            answer_type = (dr_datarow["answer_type"].ToString()),
                            calculation_formula = (dr_datarow["calculation_formula"].ToString()),
                            grouptitle_gid = (dr_datarow["grouptitle_gid"].ToString()),
                            simplify_formula = (dr_datarow["simplify_formula"].ToString()),
                            addfinal_score = (dr_datarow["addfinal_score"].ToString()),
                            hidden_question = (dr_datarow["hidden_question"].ToString()),
                            group_order = (dr_datarow["group_order"].ToString()),
                            question_order = (dr_datarow["question_order"].ToString()),
                            applicant_type = (dr_datarow["applicant_type"].ToString()),
                        });
                    }
                    objmaster.MdlVerticalGroupTitleQuestion = getverticalgroup_list;
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
            }
        }

        public void DaPostCreateVerticalRule(MdlVerticalTitleQuestion values, string employee_gid)
        {
            string lsquestion_option = "";
            msGetGid = objcmnfunctions.GetMasterGID("VCQR");
            if (values.listarray != null)
            {
                lsquestion_option = string.Join(",", values.listarray.Select(item => "" + item.list_name + ""));
            }
            if (values.answer_type != "Calculation")
            {
                values.simplify_formula = "";
            }
            msSQL = " select group_order from ocs_mst_tverticalquestionrule " +
                    " where vertical_gid='" + values.vertical_gid + "' and grouptitle_gid='" + values.grouptitle_gid + "'  limit 0,1";
            string lsgroup_order = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " insert into ocs_mst_tverticalquestionrule(" +
                        " verticalquestionrule_gid," +
                        " vertical_gid," +
                        " grouptitle_gid," +
                        " grouptitle_name," +
                        " question," +
                        " answer_type," +
                        " question_option, " +
                        " number_score, " +
                        " calculation_formula, " +
                        " simplify_formula, " +
                        " addfinal_score, " +
                        " hidden_question, " +
                        //" applicant_typegid, " +
                        " applicant_type, " +
                        " verticalapplicanttyperule_gid, " +
                        " group_order, " +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + values.vertical_gid + "'," +
                        "'" + values.grouptitle_gid + "'," +
                        "'" + values.grouptitle_name.Replace("'", "\\'") + "'," +
                        "'" + values.question.Replace("'", "\\'") + "'," +
                        "'" + values.answer_type + "'," +
                        "'" + lsquestion_option + "'," +
                        "'" + values.number_score + "'," +
                        "'" + values.calculation_formula + "'," +
                        "'" + values.simplify_formula + "'," +
                        "'" + values.addfinal_score + "'," +
                        "'" + values.hidden_question + "'," +
                        //"'" + values.applicant_typegid + "'," +
                        "'" + values.applicant_type + "'," +
                        "'" + values.verticalapplicanttyperule_gid + "'," +
                        "'" + lsgroup_order + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            if (mnResult != 0)
            {
                if (values.listarray != null)
                {
                    foreach (var data in values.listarray)
                    {
                        string msGetOptionGid = objcmnfunctions.GetMasterGID("VQLO");

                        msSQL = " insert into ocs_mst_tverticalquestionlistoption(" +
                            " verticalquestionlistoption_gid, " +
                           " verticalquestionrule_gid," +
                           " list_name," +
                           " score," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetOptionGid + "'," +
                           "'" + msGetGid + "'," +
                           "'" + data.list_name.Replace("'", "\\'") + "'," +
                           "'" + data.Score + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }

                if (values.calculation_dtl != null)
                {
                    foreach (var data in values.calculation_dtl)
                    {
                        string msGetCalcGid = objcmnfunctions.GetMasterGID("VQCD");

                        msSQL = " insert into ocs_mst_tverticalquestioncalculationdtl(" +
                           " verticalquestioncalculationdtl_gid," +
                           " verticalquestionrule_gid, " +
                           " question_gid," +
                           " question," +
                           " grouptitle_gid," +
                           " grouptitle_name," +
                           " field_type," +
                           " constant_value, " +
                           " operations," +
                           " simplify_key, " +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetCalcGid + "'," +
                           "'" + msGetGid + "'," +
                           "'" + data.question_gid + "'," +
                           "'" + data.question.Replace("'", "\\'") + "'," +
                           "'" + data.grouptitle_gid + "'," +
                           "'" + data.grouptitle_name.Replace("'", "\\'") + "'," +
                           "'" + data.field_type + "'," +
                           "'" + data.constantvalue + "'," +
                           "'" + data.operations + "'," +
                           "'" + data.simplify_key + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }

                values.status = true;
                values.message = "vertical Mapping - Rule Added Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Adding";

            }
        }

        public void DaGetVerticalquestionlistsummary(string verticalquestionrule_gid, MdlVerticalTitleQuestion values)
        {
            try
            {
                msSQL = " select verticalquestionlistoption_gid,list_name,score from ocs_mst_tverticalquestionlistoption " +
                        " where verticalquestionrule_gid='" + verticalquestionrule_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcreditgroup_list = new List<listarray>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcreditgroup_list.Add(new listarray
                        {
                            questionlistoption_gid = (dr_datarow["verticalquestionlistoption_gid"].ToString()),
                            list_name = (dr_datarow["list_name"].ToString()),
                            Score = (dr_datarow["score"].ToString()),
                        });
                    }
                    values.listarray = getcreditgroup_list;
                }
                dt_datatable.Dispose();

                msSQL = " select simplify_key,grouptitle_name,question,field_type,constant_value,operations from ocs_mst_tverticalquestioncalculationdtl " +
                        " where verticalquestionrule_gid='" + verticalquestionrule_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcalculation_dtl = new List<calculation_dtl>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcalculation_dtl.Add(new calculation_dtl
                        {
                            simplify_key = (dr_datarow["simplify_key"].ToString()),
                            question = (dr_datarow["question"].ToString()),
                            field_type = (dr_datarow["field_type"].ToString()),
                            constantvalue = (dr_datarow["constant_value"].ToString()),
                            operations = (dr_datarow["operations"].ToString()),
                            grouptitle_name = (dr_datarow["grouptitle_name"].ToString()),
                        });
                    }
                    values.calculation_dtl = getcalculation_dtl;
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
            }
        }


        public void DaGetDeleteVerticalQuestionList(string verticalquestionrule_gid, result objvalues)
        {
            try
            {
                msSQL = " select verticalquestioncalculationdtl_gid from ocs_mst_tverticalquestioncalculationdtl a " +
                        " left join ocs_mst_tverticalquestionrule b on a.verticalquestionrule_gid =  b.verticalquestionrule_gid and a.grouptitle_gid = b.grouptitle_gid " +
                        " where a.question_gid = '" + verticalquestionrule_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objODBCDatareader.Close();
                    objvalues.status = false;
                    objvalues.message = "You cannot delete. This question already mapped to the Calculation.";
                }
                else
                {
                    objODBCDatareader.Close();
                    msSQL = " delete from ocs_mst_tverticalquestionrule where verticalquestionrule_gid ='" + verticalquestionrule_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnResult == 1)
                    {
                        msSQL = " delete from ocs_mst_tverticalquestionlistoption where verticalquestionrule_gid ='" + verticalquestionrule_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " delete from ocs_mst_tverticalquestioncalculationdtl where verticalquestionrule_gid ='" + verticalquestionrule_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        objvalues.status = true;
                        objvalues.message = "Question details are deleted successfully..!";
                    }
                    else
                    {
                        objvalues.status = true;
                        objvalues.message = "Error occured..!";
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        public void DaUpdateVerticalGroupQuestionOrder(MdlVerticalGroupTitleQuestion_list values, string employee_gid)
        {
            if (values.MdlVerticalGroupTitleQuestion != null)
            {
                foreach (var data in values.MdlVerticalGroupTitleQuestion)
                {
                    msSQL = " update ocs_mst_tverticalquestionrule set question_order='" + data.question_order + "', " +
                            " updated_by='" + employee_gid + "'," +
                            " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                            " where verticalquestionrule_gid ='" + data.verticalquestionrule_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Group question order updated successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";

            }
        }

        public void DaGetVerticalGroupList(string vertical_gid, string applicant_type, MdlVerticaldtl values)
        {
            msSQL = " select grouptitle_gid,grouptitle_name,group_order,applicant_type from ocs_mst_tverticalquestionrule  " +
                    " where vertical_gid = '" + vertical_gid + "' and applicant_type = '" + applicant_type + "' group by grouptitle_gid order by applicant_type asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcreditgroup_list = new List<GroupVerticalTitle_dtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcreditgroup_list.Add(new GroupVerticalTitle_dtl
                    {
                        grouptitle_name = (dr_datarow["grouptitle_name"].ToString()),
                        grouptitle_gid = (dr_datarow["grouptitle_gid"].ToString()),
                        group_order = (dr_datarow["group_order"].ToString()),
                        applicant_type = (dr_datarow["applicant_type"].ToString()),
                    });
                }
                values.GroupTitle_dtl = getcreditgroup_list;
            }

        }

        public void DaUpdateVerticalGroupOrder(MdlVerticaldtl values, string employee_gid)
        {
            if (values.GroupTitle_dtl != null)
            {
                foreach (var data in values.GroupTitle_dtl)
                {
                    msSQL = " update ocs_mst_tverticalquestionrule set group_order='" + data.group_order + "', " +
                            " updated_by='" + employee_gid + "'," +
                            " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                            " where grouptitle_gid ='" + data.grouptitle_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Group order updated successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";

            }
        }

        // Vertical Transaction
        public void DaGetVerticalBasicView(string vertical_gid, string applicanttype, string application_gid, MdlTrnVertical values)
        {
            // "'and applicant_type='" + applicanttype + // string applicanttype,
            try
            {
                msSQL = " select a.vertical_gid,a.vertical_name from ocs_mst_tvertical a " +
                        " left join ocs_mst_tverticalquestionrule b on b.vertical_gid = a.vertical_gid " +
                       " where a.vertical_gid='" + vertical_gid +  "' group by a.vertical_gid "; // "'and b.applicant_type='" + applicanttype +
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.vertical_gid = objODBCDatareader["vertical_gid"].ToString();
                    values.vertical_name = objODBCDatareader["vertical_name"].ToString();
                }
                values.status = true;
                values.message = "success";
                objODBCDatareader.Close();

                msSQL = "select count(*) from ocs_trn_tverticalscorecardgrouptitle a " +
                       // " left join ocs_mst_tverticalquestionrule b on b.vertical_gid = a.vertical_gid " +
                        " where a.vertical_gid='" + vertical_gid + "'and a.applicant_type='" + applicanttype + "'and a.application_gid='" + application_gid + "'";
                values.scorecard_submit = objdbconn.GetExecuteScalar(msSQL);
            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }
        }

        public void DaGetVerticalScorecarddtl(string vertical_gid, string applicanttype, MdlVerticalGroupQuestiondtl values)
        {
            msSQL = " select a.vertical_gid from ocs_mst_tvertical a left join ocs_mst_tverticalquestionrule b on b.vertical_gid = a.vertical_gid " + 
                    " where a.vertical_gid='" + vertical_gid + "'and b.applicant_type='" + applicanttype + "' group by a.vertical_gid";
            string lsverticalgroup_gid = objdbconn.GetExecuteScalar(msSQL);

            if (lsverticalgroup_gid != "")
            {
                try
                {
                    msSQL = " select a.grouptitle_gid, a.grouptitle_name from ocs_mst_tverticalquestionrule a " +
                            " where a.vertical_gid='" + lsverticalgroup_gid + "'and a.applicant_type='" + applicanttype + "'  group by a.grouptitle_gid " +
                            " order by case when (group_order is null or group_order = '') then CAST('0' AS unsigned) else CAST(group_order AS unsigned) end asc, " +
                            "case when (question_order is null or question_order = '') then CAST('0' AS unsigned) else CAST(question_order AS unsigned) end asc ";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getVerticalgroup_list = new List<Vertical_GroupTitle_dtl>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dr_datarow in dt_datatable.Rows)
                        {
                            var footer_flag = "N";
                            if (dr_datarow["grouptitle_gid"].ToString() == "TRS004")
                                footer_flag = "Y";

                            getVerticalgroup_list.Add(new Vertical_GroupTitle_dtl
                            {
                                grouptitle_name = (dr_datarow["grouptitle_name"].ToString()),
                                grouptitle_gid = (dr_datarow["grouptitle_gid"].ToString()),
                                footer_flag = footer_flag
                            });
                        }
                        values.Vertical_GroupTitle_dtl = getVerticalgroup_list;
                    }

                    msSQL = " SELECT a.verticalquestionrule_gid,a.grouptitle_gid,a.grouptitle_name,a.question,a.answer_type, " +
                            " a.addfinal_score,a.hidden_question, a.calculation_formula  FROM ocs_mst_tverticalquestionrule a  " +
                            // " left join ocs_mst_tverticalquestionrule b on b.vertical_gid = a.vertical_gid " +
                            " where a.vertical_gid='" + lsverticalgroup_gid + "'and a.applicant_type='" + applicanttype + "' " +
                            " group by a.verticalquestionrule_gid order by case when (a.group_order is null or a.group_order = '') then CAST('0' AS unsigned) else CAST(a.group_order AS unsigned) end asc, " +
                            "case when (a.question_order is null or a.question_order = '') then CAST('0' AS unsigned) else CAST(a.question_order AS unsigned) end asc ";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getVerticalgroupques_list = new List<MdlTrnVerticalGroupTitleQuestion>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dr_datarow in dt_datatable.Rows)
                        {
                            getVerticalgroupques_list.Add(new MdlTrnVerticalGroupTitleQuestion
                            {
                                verticalquestionrule_gid = (dr_datarow["verticalquestionrule_gid"].ToString()),
                                question = (dr_datarow["question"].ToString()),
                                answer_type = (dr_datarow["answer_type"].ToString()),
                                calculation_formula = (dr_datarow["calculation_formula"].ToString()),
                                grouptitle_gid = (dr_datarow["grouptitle_gid"].ToString()),
                                addfinal_score = (dr_datarow["addfinal_score"].ToString()),
                                hidden_question = (dr_datarow["hidden_question"].ToString())
                            });
                        }
                        values.MdlTrnVerticalGroupTitleQuestion = getVerticalgroupques_list;
                    }
                    dt_datatable.Dispose();

                    msSQL = " select a.verticalquestionrule_gid, a.verticalquestionlistoption_gid,a.list_name,a.score from ocs_mst_tverticalquestionlistoption a " +
                            " left join ocs_mst_tverticalquestionrule b on a.verticalquestionrule_gid = b.verticalquestionrule_gid " +
                            " where b.vertical_gid='" + lsverticalgroup_gid + "'and b.applicant_type='" + applicanttype + "' group by a.verticalquestionlistoption_gid"; // "'and b.applicant_type='" + applicanttype +
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getVerticalgrouparray_list = new List<Vertical_listarray>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dr_datarow in dt_datatable.Rows)
                        {
                            getVerticalgrouparray_list.Add(new Vertical_listarray
                            {
                                verticalquestionrule_gid = (dr_datarow["verticalquestionrule_gid"].ToString()),
                                verticalquestionlistoption_gid = (dr_datarow["verticalquestionlistoption_gid"].ToString()),
                                list_name = (dr_datarow["list_name"].ToString()),
                                Score = (dr_datarow["score"].ToString()),
                            });
                        }
                        values.Vertical_listarray = getVerticalgrouparray_list;
                    }
                    dt_datatable.Dispose();
                }
                catch (Exception ex)
                {
                }
            }
        }
        public void DaGetVerticalScorecardViewdtl(string vertical_gid, string applicanttype, string application_gid, MdlVerticalGroupQuestiondtl values)
        {
            try
            {
                msSQL = " select a.grouptitle_gid, a.grouptitle_name, a.final_score from ocs_trn_tverticalscorecardgrouptitle a " +
                        //" left join ocs_mst_tverticalquestionrule b on b.vertical_gid = a.vertical_gid " +
                        " where a.vertical_gid='" + vertical_gid + "'and a.applicant_type='" + applicanttype + "'and a.application_gid='" + application_gid + "'"; //"'and b.applicant_type='" + applicanttype + // group by a.grouptitle_gid
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getVerticalgroup_list = new List<Vertical_GroupTitle_dtl>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getVerticalgroup_list.Add(new Vertical_GroupTitle_dtl
                        {
                            grouptitle_name = (dr_datarow["grouptitle_name"].ToString()),
                            grouptitle_gid = (dr_datarow["grouptitle_gid"].ToString()),
                            final_scoredisplay = (dr_datarow["final_score"].ToString())
                        });
                    }
                    values.Vertical_GroupTitle_dtl = getVerticalgroup_list;
                }

                msSQL = " SELECT a.verticalquestionrule_gid,a.grouptitle_gid,a.grouptitle_name,a.question,a.answer_type, " +
                        " a.addfinal_score, a.hidden_question, a.calculation_formula,a.actual_value,a.actualvalue_gid,a.actual_score,a.final_score, " +
                        " a.actual_number FROM ocs_trn_tverticalquestionrule a  " +
                        //" left join ocs_mst_tverticalquestionrule b on b.verticalquestionrule_gid = a.verticalquestionrule_gid " +
                        " where a.vertical_gid='" + vertical_gid + "'and a.applicant_type='" + applicanttype + "'and a.application_gid='" + application_gid + "' " + // 
                        " group by a.verticalquestionrule_gid order by case when (a.group_order is null or a.group_order = '') then CAST('0' AS unsigned) else CAST(a.group_order AS unsigned) end asc," +
                        "case when (a.question_order is null or a.question_order = '') then CAST('0' AS unsigned) else CAST(a.question_order AS unsigned) end asc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getVerticalgroupques_list = new List<MdlTrnVerticalGroupTitleQuestion>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getVerticalgroupques_list.Add(new MdlTrnVerticalGroupTitleQuestion
                        {
                            verticalquestionrule_gid = (dr_datarow["verticalquestionrule_gid"].ToString()),
                            question = (dr_datarow["question"].ToString()),
                            answer_type = (dr_datarow["answer_type"].ToString()),
                            calculation_formula = (dr_datarow["calculation_formula"].ToString()),
                            grouptitle_gid = (dr_datarow["grouptitle_gid"].ToString()),
                            addfinal_score = (dr_datarow["addfinal_score"].ToString()),
                            hidden_question = (dr_datarow["hidden_question"].ToString()),
                            actual_score = (dr_datarow["actual_score"].ToString()),
                            actual_value = (dr_datarow["actual_value"].ToString()),
                            actual_number = (dr_datarow["actual_number"].ToString()),
                            final_scoredisplay = (dr_datarow["final_score"].ToString()),
                            actualvalue_gid = (dr_datarow["actualvalue_gid"].ToString()),
                        });
                    }
                    values.MdlTrnVerticalGroupTitleQuestion = getVerticalgroupques_list;
                }
                dt_datatable.Dispose();

                msSQL = " select a.verticalquestionrule_gid, a.verticalquestionlistoption_gid,a.list_name,a.score from ocs_mst_tverticalquestionlistoption a " +
                        " left join ocs_trn_tverticalquestionrule b on a.verticalquestionrule_gid = b.verticalquestionrule_gid " +
                        " left join ocs_mst_tverticalquestionrule c on c.vertical_gid = b.vertical_gid " +
                        " where b.vertical_gid='" + vertical_gid + "'and c.applicant_type='" + applicanttype + "' group by a.verticalquestionlistoption_gid"; //"'and b.applicant_type='" + applicanttype + 
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getVerticalgrouparray_list = new List<Vertical_listarray>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getVerticalgrouparray_list.Add(new Vertical_listarray
                        {
                            verticalquestionrule_gid = (dr_datarow["verticalquestionrule_gid"].ToString()),
                            verticalquestionlistoption_gid = (dr_datarow["verticalquestionlistoption_gid"].ToString()),
                            list_name = (dr_datarow["list_name"].ToString()),
                            Score = (dr_datarow["score"].ToString()),
                        });
                    }
                    values.Vertical_listarray = getVerticalgrouparray_list;
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
            }
        }
        public void DaGetVerticalQuestionScore(List<MdlVerticalGroupScore> values, MdlVerticalGroupScoredtl data, string lsverticalquestionrule_gid)
        {
            List<Vertical_calculation_dtl> objmaster = new List<Vertical_calculation_dtl>();
            List<Arraycalculation1> objcalculation = new List<Arraycalculation1>();
            string lsquestion_gid, lsfield_type, lsconstant_value = "", lsvalue = "";
            object CalculateScore = "0";
            string simplify_key = "";
            string lscalculation_formula = "";
            List<Calculationinfo1> calculationdtl2 = new List<Calculationinfo1>();
            List<Calculationinfo1> calculationdtl3 = new List<Calculationinfo1>();
            List<GroupQuestion_list1> getcalculationques = new List<GroupQuestion_list1>();
            try
            {
                msSQL = " select a.verticalquestionrule_gid,a.question_gid,b.grouptitle_gid,b.answer_type from ocs_mst_tverticalquestioncalculationdtl a  " +
                        " left join ocs_mst_tverticalquestionrule b on a.verticalquestionrule_gid = b.verticalquestionrule_gid " +
                        " where a.verticalquestionrule_gid  in ('" + lsverticalquestionrule_gid + "') or a.question_gid in ('" + lsverticalquestionrule_gid + "') " +
                        " or a.question_gid in (select verticalquestionrule_gid " +
                        " from ocs_mst_tverticalquestioncalculationdtl where question_gid in ('" + lsverticalquestionrule_gid + "'))"; // and b.applicant_type='" + data.applicanttype + "' group by a.verticalquestionrule_gid 
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    calculationdtl2 = dt_datatable.AsEnumerable().Select(row =>
                   new Calculationinfo1
                   {
                       verticalquestionrule_gid = row["verticalquestionrule_gid"].ToString(),
                       question_gid = row["question_gid"].ToString(),
                       grouptitle_gid = row["grouptitle_gid"].ToString()
                   }).ToList();
                }
                dt_datatable.Dispose();
                string lsverticalquestiongid = string.Join(",", calculationdtl2.Select(item => "'" + item.verticalquestionrule_gid + "'"));

                var count = 1;
                for (int i = 0; count != 0; i++)
                {
                    msSQL = " select a.verticalquestionrule_gid, a.question_gid, a.grouptitle_gid from ocs_mst_tverticalquestioncalculationdtl a  " +
                       " left join ocs_mst_tverticalquestionrule b on a.verticalquestionrule_gid = b.verticalquestionrule_gid " +
                       " where a.question_gid in (select verticalquestionrule_gid " +
                       " from ocs_mst_tverticalquestioncalculationdtl where question_gid in (" + lsverticalquestiongid + "))"; //and b.applicant_type='" + data.applicanttype + "'
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable.Rows.Count != 0)
                    {
                        calculationdtl3 = dt_datatable.AsEnumerable().Select(row =>
                       new Calculationinfo1
                       {
                           verticalquestionrule_gid = row["verticalquestionrule_gid"].ToString(),
                           question_gid = row["question_gid"].ToString(),
                           grouptitle_gid = row["grouptitle_gid"].ToString()
                       }).ToList();
                    }
                    dt_datatable.Dispose();
                    count = dt_datatable.Rows.Count;
                    lsverticalquestiongid = string.Join(",", calculationdtl3.Select(item => "'" + item.verticalquestionrule_gid + "'"));
                    if (count != 0)
                        calculationdtl2.AddRange(calculationdtl3);
                    else if (count == 0)
                        break;

                }
                foreach (var k in calculationdtl2)
                {
                    msSQL = " select simplify_formula from ocs_mst_tverticalquestionrule where verticalquestionrule_gid='" + k.verticalquestionrule_gid + "'"; //"' and applicant_type = '" + data.applicanttype + 
                    lscalculation_formula = objdbconn.GetExecuteScalar(msSQL);
                    msSQL = " select group_concat(a.simplify_key) from ocs_mst_tverticalquestioncalculationdtl a " +
                            //" left join ocs_mst_tverticalquestionrule b on a.verticalquestionrule_gid = b.verticalquestionrule_gid " +
                            "where a.verticalquestionrule_gid='" + k.verticalquestionrule_gid + "'"; //"' and b.applicant_type = '" + data.applicanttype +
                    var lskey = objdbconn.GetExecuteScalar(msSQL);
                    var tok = lskey.Split(',').Reverse().ToList<string>();


                    foreach (string match in tok)
                    {
                        simplify_key = match.Replace(")", string.Empty).Replace("(", string.Empty).Replace(",", string.Empty).Trim();

                        msSQL = " select a.question_gid,a.field_type,a.constant_value from ocs_mst_tverticalquestioncalculationdtl a " +
                                //" left join ocs_mst_tverticalquestionrule b on a.verticalquestionrule_gid = b.verticalquestionrule_gid " +
                                " where a.simplify_key ='" + simplify_key + "'" +
                                " and a.verticalquestionrule_gid='" + k.verticalquestionrule_gid + "'"; //"' and b.applicant_type = '" + data.applicanttype + 
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsvalue = "0";
                            lsquestion_gid = objODBCDatareader["question_gid"].ToString();
                            lsfield_type = objODBCDatareader["field_type"].ToString();
                            lsconstant_value = objODBCDatareader["constant_value"].ToString();
                            if (lsfield_type == "Score" || lsfield_type == "Actual")
                            {
                                var getactualvalue1 = calculationdtl2.Where(a => a.verticalquestionrule_gid == lsquestion_gid).ToList();
                                if (getactualvalue1.Count != 0 && getactualvalue1[0].final_score != null)
                                    lsvalue = getactualvalue1[0].final_score;
                                else
                                {
                                    foreach (var j in data.GroupTitle_list1)
                                    {
                                        GroupQuestion_list1 getactualvalue = j.GroupQuestion_list1.Where(a => a.verticalquestionrule_gid == lsquestion_gid).FirstOrDefault();
                                        if (getactualvalue != null)
                                        {
                                            lsvalue = getactualvalue.final_score;
                                            if (getactualvalue.final_score == "" || getactualvalue.final_score == null)
                                            {
                                                lsvalue = getactualvalue.final_scoredisplay;
                                            }
                                        }
                                    }
                                }

                            }
                            else if (lsfield_type == "Constant")
                            {
                                lsvalue = lsconstant_value;
                            }
                            if (lsvalue == "")
                                lsvalue = "0";

                            objcalculation.Add(new Arraycalculation1
                            {
                                key = simplify_key,
                                value = Convert.ToDecimal(lsvalue),
                            });

                        }
                        objODBCDatareader.Close();
                    }
                    if (objcalculation.Count != 0)
                    {
                        try
                        {
                            Expression expr = new Expression(lscalculation_formula);
                            foreach (var j in objcalculation)
                            {
                                expr.Bind(j.key, j.value);
                            }
                            Object value = expr.Eval();
                            CalculateScore = value;
                            CalculateScore = Math.Round(Convert.ToDouble(CalculateScore), 2);
                            k.final_score = Convert.ToString(CalculateScore);
                            values.Add(new MdlVerticalGroupScore
                            {
                                verticalquestionrule_gid = k.verticalquestionrule_gid,
                                question_score = CalculateScore
                            });
                            objcalculation.Clear();
                        }
                        catch (Exception ex)
                        {
                            objcalculation.Clear();
                        }
                    }

                }

            }
            catch (Exception ex)
            {
            }
        }

        public void DaVerticalSaveScoreCard(MdlVerticalGroupScoredtl values, string employee_gid)
        {
            if (values.GroupTitle_list1 != null)
            {
                foreach (var data in values.GroupTitle_list1)
                {
                    msSQL = " update ocs_trn_tverticalscorecardgrouptitle set final_score='" + data.final_scoredisplay + "'  " +
                            " where vertical_gid ='" + values.vertical_gid + "' and " +
                            " grouptitle_gid ='" + data.grouptitle_gid + "' and applicant_type = '" + values.applicanttype + "'and application_gid='" + values.application_gid + "'"; // "' and applicant_type = '" + values.applicanttype + 
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    foreach (var i in data.GroupQuestion_list1)
                    {
                        string lsactualvalue_gid = "", lsactual_value = "";
                        if (i.DropdownListArray1 != null)
                        {
                            lsactualvalue_gid = i.DropdownListArray1[0].verticalquestionlistoption_gid;
                            lsactual_value = i.DropdownListArray1[0].list_name;
                        }
                        else
                        {
                            lsactual_value = i.field_number;
                        }
                        msSQL = " update ocs_trn_tverticalquestionrule set final_score='" + i.final_scoredisplay + "', " +
                                " actualvalue_gid ='" + lsactualvalue_gid + "'," +
                                " actual_value='" + lsactual_value + "'," +
                                " actual_score='" + i.Score + "'," +
                                " actual_number='" + i.actual_number + "'" +
                                " where vertical_gid ='" + values.vertical_gid + "' and " +
                                " verticalquestionrule_gid ='" + i.verticalquestionrule_gid + "' and applicant_type = '" + values.applicanttype + "'and application_gid='" + values.application_gid + "'"; // "' and applicant_type = '" + values.applicanttype + 
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
            }

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Score Card Details are Updated successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";

            }
        }

        public void DaVerticalSubmitScoreCard(MdlVerticalGroupScoredtl values, string employee_gid)
        {

            msSQL = " select a.vertical_gid from ocs_mst_tvertical a " +
                    " left join ocs_mst_tverticalquestionrule b on a.vertical_gid = b.vertical_gid " +
                    " where a.vertical_gid='" + values.vertical_gid + "' and b.applicant_type = '" + values.applicanttype + "'";
            string lsverticalgroup_gid = objdbconn.GetExecuteScalar(msSQL);

            if (lsverticalgroup_gid != "")
            {
                msSQL = " Insert into ocs_trn_tverticalscorecardgrouptitle (application_gid, editruletype_gid, vertical_gid, applicant_type, grouptitle_gid,grouptitle_name, bre_status, " +
                       " created_by, created_date) " +
                       " select '" + values.application_gid + "','" + values.editruletype_gid + "','" + lsverticalgroup_gid + "','" + values.applicanttype + "', grouptitle_gid, grouptitle_name, 'BRE Activated', " +
                       " '" + employee_gid + "', now()  " +
                       " from ocs_mst_tverticalquestionrule " +
                       " where vertical_gid='" + lsverticalgroup_gid + "' and applicant_type = '" + values.applicanttype + "'  group by grouptitle_gid " +
                       " order by case when (group_order is null or group_order = '') then CAST('0' AS unsigned) else CAST(group_order AS unsigned) end asc, case when (question_order is null or question_order = '') then CAST('0' AS unsigned) else CAST(question_order AS unsigned) end asc ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " insert into ocs_trn_tverticalquestionrule (application_gid, editruletype_gid, vertical_gid, verticalquestionrule_gid, applicant_type, grouptitle_gid , " +
                        " grouptitle_name, question, answer_type, question_option, number_score, calculation_formula, simplify_formula, " +
                        " addfinal_score, hidden_question, group_order, question_order, " +
                        " created_by, created_date) " +
                        " select '" + values.application_gid + "','" + values.editruletype_gid + "','" + lsverticalgroup_gid + "',verticalquestionrule_gid, '" + values.applicanttype + "',grouptitle_gid, " +
                        " grouptitle_name, question, answer_type,question_option,number_score, calculation_formula,simplify_formula, " +
                        " addfinal_score, hidden_question, group_order, question_order, " +
                        " '" + employee_gid + "', now()  " +
                        " from ocs_mst_tverticalquestionrule " +
                        " where vertical_gid= '" + lsverticalgroup_gid + "' and applicant_type = '" + values.applicanttype + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " insert into ocs_trn_tverticalquestioncalculationdtl (application_gid, editruletype_gid,vertical_gid,verticalquestioncalculationdtl_gid,verticalquestionrule_gid,applicant_type,question_gid , " +
                       " question, field_type, constant_value, operations, grouptitle_gid, grouptitle_name, simplify_key, " +
                       " created_by, created_date) " +
                       " select '" + values.application_gid + "','" + values.editruletype_gid + "','" + lsverticalgroup_gid + "',verticalquestioncalculationdtl_gid,verticalquestionrule_gid,'" + values.applicanttype + "', question_gid , " +
                       " question, field_type, constant_value, operations, grouptitle_gid, grouptitle_name, simplify_key, " +
                       " '" + employee_gid + "', now()  " +
                       " from ocs_mst_tverticalquestioncalculationdtl " +
                       " where verticalquestionrule_gid in (select verticalquestionrule_gid from ocs_mst_tverticalquestionrule where vertical_gid= '" + lsverticalgroup_gid + "' and applicant_type = '" + values.applicanttype + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " insert into ocs_trn_tverticalquestionlistoption (application_gid, editruletype_gid, vertical_gid,verticalquestionlistoption_gid,verticalquestionrule_gid,applicant_type,list_name , " +
                        " score, created_by, created_date) " +
                        " select '" + values.application_gid + "','" + values.editruletype_gid + "','" + lsverticalgroup_gid + "',verticalquestionlistoption_gid,verticalquestionrule_gid,'" + values.applicanttype + "',list_name, " +
                        " score, '" + employee_gid + "', now()  " +
                        " from ocs_mst_tverticalquestionlistoption " +
                        " where verticalquestionrule_gid in (select verticalquestionrule_gid from ocs_mst_tverticalquestionrule where vertical_gid= '" + lsverticalgroup_gid + "' and applicant_type = '" + values.applicanttype + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (values.GroupTitle_list1 != null)
                {
                    foreach (var data in values.GroupTitle_list1)
                    {
                        msSQL = " update ocs_trn_tverticalscorecardgrouptitle set final_score='" + data.final_scoredisplay + "'" +
                                " where vertical_gid ='" + values.vertical_gid + "' and " +
                                " grouptitle_gid ='" + data.grouptitle_gid + "' and applicant_type = '" + values.applicanttype + "'and application_gid='" + values.application_gid + "'"; // "' and applicant_type = '" + values.applicanttype +
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        foreach (var i in data.GroupQuestion_list1)
                        {
                            string lsactualvalue_gid = "", lsactual_value = "";
                            if (i.DropdownListArray1 != null)
                            {
                                lsactualvalue_gid = i.DropdownListArray1[0].verticalquestionlistoption_gid;
                                lsactual_value = i.DropdownListArray1[0].list_name;
                            }
                            else
                            {
                                lsactual_value = i.field_number;
                            }
                            msSQL = " update ocs_trn_tverticalquestionrule set final_score='" + i.final_scoredisplay + "', " +
                                    " actualvalue_gid ='" + lsactualvalue_gid + "'," +
                                    " actual_value='" + lsactual_value + "'," +
                                    " actual_score='" + i.Score + "'," +
                                    " actual_number='" + i.actual_number + "'" +
                                    " where vertical_gid ='" + values.vertical_gid + "' and " +
                                    " verticalquestionrule_gid ='" + i.verticalquestionrule_gid + "' and applicant_type = '" + values.applicanttype + "'and application_gid='" + values.application_gid + "'"; // "' and applicant_type = '" + values.applicanttype +
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }


                }
            }

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Score Card Submitted successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";

            }
        }

        // Vertical Applicant Type Master 
        public void DaGetVerticalGroupTitleListAppType(string vertical_gid, VerticalTitle_list objmaster)
        {
            try
            {
                msSQL = " select vertical_gid,vertical_name,vertical_code from ocs_mst_tvertical " +
                        " where vertical_gid='" + vertical_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objmaster.vertical_name = objODBCDatareader["vertical_name"].ToString();
                    objmaster.vertical_code = objODBCDatareader["vertical_code"].ToString();
                }
                objODBCDatareader.Close();
            }

            catch (Exception ex)
            {
            }
        }

        public void DaGetVerticalquestionsummaryAppType(string vertical_gid, MdlVerticalGroupTitleQuestion_list objmaster)
        {
            try
            {
                msSQL = " SELECT verticalapplicanttyperule_gid,vertical_gid,vertical_name,applicant_typegid,applicant_type " +
                        " FROM ocs_trn_tverticalapplicanttyperule  " +
                        " where vertical_gid='" + vertical_gid +  "' " +
                        "order by verticalapplicanttyperule_gid desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getverticalgroup_list = new List<MdlVerticalGroupTitleQuestion>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getverticalgroup_list.Add(new MdlVerticalGroupTitleQuestion
                        {
                            verticalapplicanttyperule_gid = (dr_datarow["verticalapplicanttyperule_gid"].ToString()),
                            vertical_gid = (dr_datarow["vertical_gid"].ToString()),
                            applicant_typegid = (dr_datarow["applicant_typegid"].ToString()),
                            applicant_type = (dr_datarow["applicant_type"].ToString()),
                        });
                    }
                    objmaster.MdlVerticalGroupTitleQuestion = getverticalgroup_list;
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
            }
        }

        public void DaPostCreateVerticalRuleAppType(MdlVerticalTitleQuestion values, string employee_gid)
        {
            try
            {
                msGetGid = objcmnfunctions.GetMasterGID("VATA");

                msSQL = " insert into ocs_trn_tverticalapplicanttyperule(" +
                            " verticalapplicanttyperule_gid," +
                            " vertical_gid," +
                            " vertical_name," +
                            " vertical_code," +
                            " applicant_typegid," +
                            " applicant_type," +
                            " created_by," +
                            " created_date)" +
                            " values(" +
                            "'" + msGetGid + "'," +
                            "'" + values.vertical_gid + "'," +
                            "'" + values.vertical_name.Replace("'", "\\'") + "'," +
                            "'" + values.vertical_code.Replace("'", "\\'") + "'," +
                            "'" + values.applicant_typegid + "'," +
                            "'" + values.applicant_type + "'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Vertical Applicant Type Added Successfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occurred While Adding";
                }
            }
            catch (Exception ex) { }

        }

        public void DaGetDeleteVerticalQuestionListAppType(string verticalapplicanttyperule_gid, result objvalues)
        {
            try
            {
                msSQL = " select verticalapplicanttyperule_gid from ocs_mst_tverticalquestionrule " +
                        //" left join ocs_mst_tverticalquestionrule b on a.verticalquestionrule_gid =  b.verticalquestionrule_gid and a.grouptitle_gid = b.grouptitle_gid " +
                        " where verticalapplicanttyperule_gid = '" + verticalapplicanttyperule_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objODBCDatareader.Close();
                    objvalues.status = false;
                    objvalues.message = "You cannot delete. This Applicant Type already Mapped to the Question";
                }
                else
                {
                    objODBCDatareader.Close();
                    msSQL = " delete from ocs_trn_tverticalapplicanttyperule where verticalapplicanttyperule_gid ='" + verticalapplicanttyperule_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnResult == 1)
                    {
                        objvalues.status = true;
                        objvalues.message = "Applicant Type details are deleted successfully..!";
                    }
                    else
                    {
                        objvalues.status = true;
                        objvalues.message = "Error occured..!";
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        public void DaGetVerticalGroupTitleListAppTypeEdit(string verticalapplicanttyperule_gid, VerticalTitle_list objmaster)
        {
            try
            {
                msSQL = " select vertical_gid,vertical_name,vertical_code,applicant_type from ocs_trn_tverticalapplicanttyperule " +
                        " where verticalapplicanttyperule_gid='" + verticalapplicanttyperule_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objmaster.vertical_name = objODBCDatareader["vertical_name"].ToString();
                    objmaster.vertical_code = objODBCDatareader["vertical_code"].ToString();
                    objmaster.applicant_type = objODBCDatareader["applicant_type"].ToString();
                }
                objODBCDatareader.Close();
            }

            catch (Exception ex)
            {
            }
        }
    
    }
}