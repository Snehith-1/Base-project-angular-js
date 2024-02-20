using ems.mastersamagro.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.IO;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;


namespace ems.mastersamagro.DataAccess
{
    /// <summary>
    /// This DataAccess will provide access to fetch data from credit mapping master in custopedia.
    /// </summary>
    /// <remarks>Written by Abilash.A</remarks>
    public class DaAgrMstSuprCreditMapping
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        DataTable dt_datatable;
        OdbcDataReader objODBCDatareader, objODBCDatareader1;
        string msSQL, msGetGid, lsgroupid, msGetcredit2credithead_gid, msGetcredit2nationalmanager_gid, msGetcreditr2credithead_gid, msGetcredit2creditmanager_gid, lsmappinggid;
        int mnResult;
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
        public string lsclusterhead;
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
        private string lscreditmanager_name, lscreditregionalmanager_name, lscreditnationalmanager_name, lscredithead_name, lsremarks;

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
                lsgroupid = objcmnfunctions.GetMasterGID("CRGP");
                msSQL = " insert into ocs_mst_tcreditmapping(" +
                        " creditmapping_gid ," +
                        " creditgroup_id," +
                        " creditgroup_name," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
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
                msSQL = " SELECT a.creditgroup_id,a.creditmapping_gid ,a.creditgroup_name, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
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
                              " updated_by, " +
                              " updated_date) " +
                              " values(" +
                              "'" + msGetGid + "'," +
                              "'" + values.creditmapping_gid + "'," +
                              "'" + objODBCDatareader["creditgroup_name"].ToString() + "'," +
                              "'" + employee_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            objODBCDatareader.Close();

            msSQL = "update ocs_mst_tcreditmapping set creditgroup_name='" + values.creditgroup_name.Replace("'", "") + "'," +
                     " updated_by='" + employee_gid + "'," +
                     " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                     " where creditmapping_gid='" + values.creditmapping_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update agr_mst_tsuprapplication set creditgroup_name='" + values.creditgroup_name.Replace("'", "") + "'" +
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
            msSQL = "update agr_mst_tsuprapplication set " +
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
                    " creditgroup_status = 'Assigned'," +
                    " remarks ='" + values.remarks + "'," +
                    " creditassigned_by='" + employee_gid + "'," +
                    " creditassigned_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                    " ccsubmitted_by = '" + employee_gid + "'," +
                    " ccsubmitted_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
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
                msSQL = "Insert into agr_trn_tsuprAppcreditapproval( " +
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
                        msSQL = "select application_no from agr_mst_tsuprapplication where application_gid='" + values.application_gid + "'";
                        application_no = objdbconn.GetExecuteScalar(msSQL);
                        msSQL = "select customerref_name from agr_mst_tsuprapplication where application_gid='" + values.application_gid + "'";
                        customer_name = objdbconn.GetExecuteScalar(msSQL);
                        msSQL = "select approved_date from agr_trn_tapplicationapproval a left join agr_mst_tsuprapplication b on b.application_gid = a.application_gid and hierary_level = '5' where b.application_gid='" + values.application_gid + "'";
                        finalapproved_time = objdbconn.GetExecuteScalar(msSQL);
                        msSQL = "select clustermanager_name,zonalhead_name,relationshipmanager_name,creditassigned_date from agr_mst_tsuprapplication where application_gid = '" + values.application_gid + "'";
                        objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader1.HasRows == true)
                        {
                            cluster_head = objODBCDatareader1["clustermanager_name"].ToString();
                            zonal_head = objODBCDatareader1["zonalhead_name"].ToString();
                            relationshipmanager_name = objODBCDatareader1["relationshipmanager_name"].ToString();
                            creditassigned_date = objODBCDatareader1["creditassigned_date"].ToString();
                        }
                        objODBCDatareader1.Close();
                        msSQL = "select b.employee_emailid from agr_mst_tsuprapplication a left join hrm_mst_temployee b on b.employee_gid = a.creditmanager_gid  where application_gid = '" + values.application_gid + "'";
                        tomail_id = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = "select b.employee_emailid from agr_mst_tsuprapplication a left join hrm_mst_temployee b on b.employee_gid = a.creditnationalmanager_gid  where application_gid = '" + values.application_gid + "'";
                        nationalmanager_mailid = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = "select b.employee_emailid from agr_mst_tsuprapplication a left join hrm_mst_temployee b on b.employee_gid = a.creditregionalmanager_gid  where application_gid = '" + values.application_gid + "'";
                        regionalmanager_mailid = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = "select b.employee_emailid from agr_mst_tsuprapplication a left join hrm_mst_temployee b on b.employee_gid = a.credithead_gid  where application_gid = '" + values.application_gid + "'";
                        credithead_mailid = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = " select b.employee_emailid from agr_mst_tsuprapplication a left join hrm_mst_temployee b on b.employee_gid = a.relationshipmanager_gid where a.application_gid='" + values.application_gid + "'";
                        relationshipmanager_mailid = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = "select concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as allocated_by from agr_mst_tsuprapplication a left join hrm_mst_temployee b on b.employee_gid = a.creditassigned_by left join adm_mst_tuser c on c.user_gid = b.user_gid where a.application_gid='" + values.application_gid + "'";
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
                        //lssource = ConfigurationManager.AppSettings["img_path"];
                        objODBCDatareader.Close();
                        sub = " Application allocation : " + application_no + " ";
                        body = "<style>table, th, td {border: 1px solid black;border-collapse: collapse;}</style>";
                        body = body + "<table style='border-right: 1px solid black;border-top: 1px solid black;border-bottom: 1px solid black;'><tr><td style='border-right-color:white;align:center;'>";
                        //body = body + "<br />";
                        //body = body + "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp <img style='height:150px; width:380px;' src='" + lssource + "'><br />";
                        //body = body + "<br />";
                        body = body + " &nbsp &nbsp Dear Sir / Madam, <br />";
                        body = body + "<br />";
                        body = body + "&nbsp &nbsp Greetings! <br />";
                        body = body + "<br />";
                        body = body + "&nbsp &nbsp The below application has been allocated to you. <br />";
                        body = body + "<br />";
                        body = body + "&nbsp &nbsp <b> Application Number : </b> " + application_no + "  <br />";
                        body = body + "<br />";
                        body = body + "&nbsp &nbsp <b> Customer Name : </b> " + HttpUtility.HtmlEncode(customer_name)+ "  <br />";
                        body = body + "<br />";
                        body = body + "&nbsp &nbsp <b> RM Name : </b> " + HttpUtility.HtmlEncode(relationshipmanager_name)+ "  <br />";
                        body = body + "<br />";
                        body = body + "&nbsp &nbsp <b> Cluster Head Name : </b> " + HttpUtility.HtmlEncode(cluster_head )+ "  <br />";
                        body = body + "<br />";
                        body = body + "&nbsp &nbsp <b> Zonal Head Name : </b> " + HttpUtility.HtmlEncode(zonal_head )+ "  <br />";
                        body = body + "<br />";
                        body = body + "&nbsp &nbsp <b> Allocated By :</b>  " + HttpUtility.HtmlEncode(allocated_by)+ "  <br />";
                        body = body + "<br />";
                        body = body + "&nbsp &nbsp <b> Allocation Time :</b>  " + creditassigned_date + "  <br />";
                        body = body + "<br />";
                        body = body + "&nbsp &nbsp Login to " + ConfigurationManager.AppSettings["livedomain_url"].ToString() + " and complete the necessary actions.";
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
                    " creditnationalmanager_gid,creditnationalmanager_name,credithead_gid,credithead_name,remarks " +
                    " from agr_mst_tsuprapplication where application_gid='" + values.application_gid + "'";
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
            }
            objODBCDatareader.Close();

            msSQL = " Insert into agr_trn_tsuprAppcreditapprovalreassignlog( " +
                    " application_gid, " +
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

            msSQL += "'" + lsremarks + "'," +
                    "'" + values.remarks + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                msSQL = "update agr_mst_tsuprapplication set " +
                        " credithead_gid='" + values.credithead_gid + "'," +
                        " credithead_name='" + values.credithead_name + "'," +
                        " creditnationalmanager_gid='" + values.nationalcredit_gid + "'," +
                        " creditnationalmanager_name ='" + values.nationalcredit_name + "'," +
                        " creditregionalmanager_gid='" + values.regionalcredit_gid + "'," +
                        " creditregionalmanager_name='" + values.regionalcredit_name + "'," +
                        " creditmanager_gid='" + values.creditmanager_gid + "'," +
                        " creditmanager_name='" + values.creditmanager_name + "'," +
                        " remarks ='" + values.remarks + "'," +
                        " creditassigned_by='" + employee_gid + "'," +
                        " creditassigned_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                        " where application_gid='" + values.application_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {

                    msSQL = "update agr_trn_tsuprAppcreditapproval set approval_gid='" + values.credithead_gid + "',approval_name='" + values.credithead_name + "'" +
                           " where application_gid='" + values.application_gid + "' and hierary_level='3' and approval_status!='Approved'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_trn_tsuprAppcreditapproval set approval_gid='" + values.nationalcredit_gid + "',approval_name='" + values.nationalcredit_name + "'" +
                           " where application_gid='" + values.application_gid + "' and hierary_level='2' and approval_status!='Approved'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_trn_tsuprAppcreditapproval set approval_gid='" + values.regionalcredit_gid + "',approval_name='" + values.regionalcredit_name + "'" +
                           " where application_gid='" + values.application_gid + "' and hierary_level='1' and approval_status!='Approved'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_trn_tsuprAppcreditapproval set approval_gid='" + values.creditmanager_gid + "',approval_name='" + values.creditmanager_name + "'" +
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
                      " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by " +
                      " FROM agr_trn_tsuprAppcreditapprovalreassignlog a" +
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
                msSQL = " SELECT creditgroup_name,creditmapping_gid from ocs_mst_tcreditmapping ";
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

    }
}