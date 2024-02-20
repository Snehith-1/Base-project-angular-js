using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ems.vp.Models;
using System.Data;
using System.Data.Odbc;
using ems.utilities.Functions;

namespace ems.vp.DataAccess
{
    public class DaReleaseManagement
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        DataTable dt_datatable, dt_date;
        OdbcDataReader objODBCDatareader;
        string msSQL, msGetGid;
        int mnResult;
        string changedetails_flag;
        int i = 0;

        public bool DaGetReleaseDetails(ReleaseManagement objReleaseManagemnet, string user_gid)
        {

            List<ReleaseDetails> getdata = null;
            msSQL = " select a.release_gid,a.ref_no,date_format(a.release_date,'%d-%m-%Y') as release_date,count(e.issuetracker_gid) as issue_releasecount,a.application_gid," +
                   " a.vendor_gid,a.application,a.changedetails_flag,a.vendor,a.release_remarks,a.release_status,a.approval_status,a.done_by,date_format(a.created_date,'%d-%m-%Y') as created_date" +
                   " from its_trn_trelease a" +
                   " left join its_mst_tapplicationmaster b on a.application_gid = b.applicationmaster_gid" +
                   " left join adm_mst_tvendoruser c on b.application_code = c.vendoruser_code" +
                   " left join its_trn_tissuetracker e on e.release_gid = a.release_gid " +
                   " where c.vendoruser_gid='" + user_gid + "' group by a.release_gid order by release_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {

                getdata = dt_datatable.AsEnumerable().Select(row =>
                  new ReleaseDetails
                  {
                      release_gid = row["release_gid"].ToString(),
                      releaseref_no = row["ref_no"].ToString(),
                      release_date = row["release_date"].ToString(),
                      application_gid = row["application_gid"].ToString(),
                      application_name = row["application"].ToString(),
                      vendor_name = row["vendor"].ToString(),
                      release_remarks = row["release_remarks"].ToString(),
                      release_status = row["release_status"].ToString(),
                      approval_status = row["approval_status"].ToString(),
                      issue_releasecount = row["issue_releasecount"].ToString(),
                      vendor_gid = row["vendor_gid"].ToString(),
                      done_by = row["done_by"].ToString(),
                      done_on = row["created_date"].ToString(),
                      changedetails_flag=row["changedetails_flag"].ToString()
                  }).ToList();
                objReleaseManagemnet.ReleaseDetails = getdata;

            }

            objReleaseManagemnet.status = true;
            objReleaseManagemnet.message = "Success";

            dt_datatable.Dispose();

            return true;
        }

        public bool DaPostStatusCompleted(statusUpdate values, string user_gid)
        {
            try
            {
                msSQL = " update its_trn_trelease set release_status='Completed'," +
                        " releasesub_status='Completed'" + 
                        " where release_gid='" + values.release_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " select issuetracker_gid from its_trn_tissue2release where release_gid='" + values.release_gid + "'";
                dt_date = objdbconn.GetDataTable(msSQL);
                if (dt_date.Rows.Count != 0)
                {
                    foreach (DataRow dr_date in dt_date.Rows)
                    {
                        msSQL = " Update its_trn_tissuetracker SET issue_status='Completed'," +
                                " issuetracker_status='Completed'," +
                                " remarks='" + values.StatusRemarks + "'" +
                                " WHERE issuetracker_gid='" + dr_date["issuetracker_gid"] + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msGetGid = objcmnfunctions.GetMasterGID("ISRS");
                        msSQL = " insert into its_trn_tissuestatuslog (" +
                                 " issuestatuslog_gid, " +
                                 " issue_status," +
                                 " remarks," +
                                 " created_date," +
                                 " created_by," +
                                 " done_by," +
                                 " issuetracker_gid)" +
                                 "Values(" +
                                 "'" + msGetGid + "'," +
                                 "'Completed'," +
                                 "'" + values.StatusRemarks + "'," +
                                 "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                 "'" + user_gid + "'," +
                                 "'" + values.DoneBy + "'," +
                                 "'" + dr_date["issuetracker_gid"] + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                dt_date.Dispose();

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "success";
                    return true;
                }
                else
                {
                    values.status = false;
                    values.message = "failure";
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool DaGetViewIssue2Release(string release_gid, releaseData objIssue2Release)
        {
            List<tabledata> IssueDetails = null;
            try
            {
                msSQL = " select date_format(a.release_date,'%d-%m-%Y') as release_date,a.changedetails_flag,a.release_remarks,a.done_by,a.release_status,b.change_description," +
                        " b.impacted_module,b.impacted_system,b.reason_change,b.alternative_way,b.resources " +
                        " from its_trn_trelease a " +
                        " left join its_trn_tchangedetails b on a.release_gid=b.release_gid " + 
                        " where a.release_gid='" + release_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows==true)
                {
                    objIssue2Release.release_date = objODBCDatareader["release_date"].ToString();
                    objIssue2Release.release_remarks = objODBCDatareader["release_remarks"].ToString();
                    objIssue2Release.release_status = objODBCDatareader["release_status"].ToString();
                    objIssue2Release.done_by = objODBCDatareader["done_by"].ToString();
                    objIssue2Release.change_description = objODBCDatareader["change_description"].ToString();
                    objIssue2Release.impacted_module = objODBCDatareader["impacted_module"].ToString();
                    objIssue2Release.impacted_system = objODBCDatareader["impacted_system"].ToString();
                    objIssue2Release.reason_change = objODBCDatareader["reason_change"].ToString();
                    objIssue2Release.alternative_way = objODBCDatareader["alternative_way"].ToString();
                    objIssue2Release.resources = objODBCDatareader["resources"].ToString();
                    changedetails_flag = objODBCDatareader["changedetails_flag"].ToString();
                }
                objODBCDatareader.Close();
                if (changedetails_flag=="Y")
                {
                    msSQL = " update its_trn_trelease set changedetails_flag='N' where release_gid='" + release_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                msSQL = "select uatdocument_gid,document_name,document_path from its_trn_uatdocument where release_gid='" + release_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var uatdocument = new List<uatdocument_list>();
                if (dt_datatable.Rows.Count !=0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        uatdocument.Add(new uatdocument_list
                        {
                            uatdocument_gid = dt["uatdocument_gid"].ToString(),
                            document_name = dt["document_name"].ToString(),
                            document_path = (dt["document_path"].ToString()),
                        });
                    }
                }
                objIssue2Release.uatdocument_list = uatdocument;
                dt_datatable.Dispose();

                msSQL = " select a.issuetracker_gid,a.issue_refno,date_format(a.issue_date, '%d-%m-%Y') as issue_date,a.issue_type, a.Severity,a.priority,a.issue_status," +
                        " a.issue_title,a.issue_remarks,a.issue_status,b.approval_status from its_trn_tissuetracker a " +
                        " left join its_trn_tissue2release b on a.issuetracker_gid = b.issuetracker_gid " +
                        " where a.release_gid = '" + release_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    IssueDetails = dt_datatable.AsEnumerable().Select(row =>
                    new tabledata
                    {
                        issuetracker_gid = row["issuetracker_gid"].ToString(),
                        issue_refno = row["issue_refno"].ToString(),
                        issue_date = row["issue_date"].ToString(),
                        Severity = row["Severity"].ToString(),
                        priority = row["priority"].ToString(),
                        issue_status = row["issue_status"].ToString(),
                        approval_status = row["approval_status"].ToString(),
                        issue_title = row["issue_title"].ToString(),
                        issue_type = row["issue_type"].ToString(),
                        issue_remarks = row["issue_remarks"].ToString()
                    }
                    ).ToList();
                    objIssue2Release.tabledata = IssueDetails;
                    dt_datatable.Dispose();

                }
                objIssue2Release.status = true;
                objIssue2Release.message = "success";
                return true;
            }
            catch (Exception ex)
            {
                objIssue2Release.status = false;
                objIssue2Release.message = ex.Message.ToString();
                return false;
            }

        }
    }
}