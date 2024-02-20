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
using ems.storage.Functions;

namespace ems.rsk.DataAccess
{
    public class DaTierMeeting
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string lsvisitreport_generateGid;
        string msSQL, msGetGid, msGetDocumentGid;
        string lspath, lstier2preparation_gid;
        HttpPostedFile httpPostedFile;
        int mnResult;
        string lschangereason;

        // Tier 1 Details...//

        public void DaGetVertical(string month, string tier2,string zonalmapping_gid, tierverticallist objgetsegment)
        {
            try
            {
                msSQL = " select zonalrisk_managerGid from rsk_mst_tzonalmapping where zonalmapping_gid='" + zonalmapping_gid + "'";
                string lszonalrm_GID = objdbconn.GetExecuteScalar(msSQL);
                if (tier2 == "Y")
                {
                    msSQL = " select vertical_gid, vertical from rsk_trn_ttier1format where tier1format_gid not in " +
                            " (select tier1format_gid from rsk_trn_ttierallocationdtl) and zonal_riskmanagergid = '" + lszonalrm_GID + "' " +
                            " and tier1_approvalstatus = 'Approved' group by vertical_gid ";

                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getvertical = new List<tiervertical>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dr_datarow in dt_datatable.Rows)
                        {
                            getvertical.Add(new tiervertical
                            {
                                vertical_gid = (dr_datarow["vertical_gid"].ToString()),
                                vertical_code = (dr_datarow["vertical"].ToString()),
                            });
                        }
                        objgetsegment.tiervertical = getvertical;
                    }
                    dt_datatable.Dispose();
                }
                else
                {
                    msSQL = " SELECT vertical_gid,vertical_name,vertical_code FROM ocs_mst_tvertical " +
                      " where vertical_gid not in (select vertical_gid from rsk_trn_ttier3preparation " +
                      " where tier3_month ='" + month + "') " +
                      " order by vertical_gid desc";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getvertical = new List<tiervertical>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dr_datarow in dt_datatable.Rows)
                        {
                            getvertical.Add(new tiervertical
                            {
                                vertical_gid = (dr_datarow["vertical_gid"].ToString()),
                                vertical_name = (dr_datarow["vertical_name"].ToString()),
                                vertical_code = (dr_datarow["vertical_code"].ToString()),
                            });
                        }
                        objgetsegment.tiervertical = getvertical;
                    }
                    dt_datatable.Dispose();
                }

                objgetsegment.status = true;
            }
            catch
            {
                objgetsegment.status = false;
            }
        }

        public void DaGetVerticalAllocationdtl(string vertical_gid, string month, string tier2_flag, tierallocationdtllist objvalues, string employee_gid)
        {
            try
            {
                if (tier2_flag == "Y")
                {
                    msSQL = " select a.tier1format_gid,vertical,date_format(tier1_approved_date, '%d-%m-%Y') as tier1_approveddate, " +
                            " a.allocationdtl_gid,a.tier1_code,a.customer_name,a.customer_urn,assignedRM_name as riskmanager,c.tier2_code  " +
                            " from rsk_trn_ttier1format a " +
                            " left join rsk_trn_tallocationdtl b on a.allocationdtl_gid = b.allocationdtl_gid " +
                            " left join rsk_tmp_ttier2codechange c on a.tier1format_gid=c.tier1format_gid " +
                            " where a.tier1format_gid not in (select tier1format_gid from rsk_trn_ttierallocationdtl ) and tier1_approvalstatus = 'Approved' " +
                            " and vertical_gid ='" + vertical_gid + "' and zonal_riskmanagergid = '" + employee_gid + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var gettierallocationdtl = new List<tierallocationdtl>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dr_datarow in dt_datatable.Rows)
                        {
                            gettierallocationdtl.Add(new tierallocationdtl
                            {
                                vertical = (dr_datarow["vertical"].ToString()),
                                tier1_approveddate = (dr_datarow["tier1_approveddate"].ToString()),
                                allocationdtl_gid = (dr_datarow["allocationdtl_gid"].ToString()),
                                customer_name = (dr_datarow["customer_name"].ToString()),
                                customer_urn = (dr_datarow["customer_urn"].ToString()),
                                riskmanager = dr_datarow["riskmanager"].ToString(),
                                tier1_code = dr_datarow["tier1_code"].ToString(),
                                tier1format_gid = dr_datarow["tier1format_gid"].ToString(),
                                tier2_code = dr_datarow["tier2_code"].ToString(),
                            });
                        }
                        objvalues.tierallocationdtl = gettierallocationdtl;
                    }
                    dt_datatable.Dispose();
                }
                else
                {
                    msSQL = " select c.tier3_code,a.tier2preparation_gid,a.zonal_name,a.vertical,date_format(tier2_approveddate, '%d-%m-%Y') as tier2_approveddate," +
                            " concat(b.user_firstname, ' ', b.user_lastname, ' / ', b.user_code) as ZRM_name,customer_name,customer_urn, " +
                            " riskmanager,tier2_code,c.allocationdtl_gid,c.tierallocation_gid from rsk_trn_ttier2preparation a " +
                            " left join adm_mst_tuser b on a.created_by = b.user_gid " +
                            " left join rsk_trn_ttierallocationdtl c on a.tier2preparation_gid = c.tier2preparation_gid " +
                            " where a.vertical_gid = '" + vertical_gid + "' and a.tier2_month = '" + month + "'" +
                            " and a.headRMD_gid = '" + employee_gid + "' and a.tier2_approval_status = 'Approved'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var gettierallocationdtl = new List<tierallocationdtl>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dr_datarow in dt_datatable.Rows)
                        {
                            gettierallocationdtl.Add(new tierallocationdtl
                            {
                                vertical = (dr_datarow["vertical"].ToString()),
                                zonal_name = (dr_datarow["zonal_name"].ToString()),
                                tier2_approveddate = (dr_datarow["tier2_approveddate"].ToString()),
                                ZRM_name = dr_datarow["ZRM_name"].ToString(),
                                customer_name = (dr_datarow["customer_name"].ToString()),
                                customer_urn = (dr_datarow["customer_urn"].ToString()),
                                allocationdtl_gid = (dr_datarow["allocationdtl_gid"].ToString()),
                                riskmanager = dr_datarow["riskmanager"].ToString(),
                                tier2preparation_gid = dr_datarow["tier2preparation_gid"].ToString(),
                                tier2_code = dr_datarow["tier2_code"].ToString(),
                                tierallocation_gid = dr_datarow["tierallocation_gid"].ToString(),
                                tier3_code = dr_datarow["tier3_code"].ToString(),
                            });
                        }
                        objvalues.tierallocationdtl = gettierallocationdtl;
                    }
                    dt_datatable.Dispose();
                }

                objvalues.status = true;
            }
            catch (Exception ex)
            {
                objvalues.message = ex.ToString();
                objvalues.status = false;
            }
        }

        public bool DaGetTier1formatlist(string employee_gid, tier1formatlist values)
        {
            msSQL = " select tier1format_gid,observation_reportgid,customer_name,customer_urn,allocationdtl_gid, " +
                   " tier1_approvalstatus,vertical,date_format(tier1_approved_date,'%d-%m-%Y') as tier1_approved_date, " +
                   " concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as created_by," +
                   " date_format(a.created_date, '%d-%m-%Y') as created_date " +
                   " from rsk_trn_ttier1format a " +
                   " left join adm_mst_tuser b on a.created_by=b.user_gid " +
                   " where zonal_riskmanagergid = '" + employee_gid + "' order by tier1_approvalstatus desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_tier1format = new List<tier1format>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    get_tier1format.Add(new tier1format
                    {
                        tier1format_gid = dt["tier1format_gid"].ToString(),
                        observation_reportgid = dt["observation_reportgid"].ToString(),
                        allocationdtl_gid = dt["allocationdtl_gid"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical = dt["vertical"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        tier1_approvalstatus = dt["tier1_approvalstatus"].ToString(),
                        tier1_approveddate = dt["tier1_approved_date"].ToString(),
                    });
                }
                values.tier1format = get_tier1format;
            }
            dt_datatable.Dispose();

            msSQL = " select a.count_pending , b.count_approved,c.count_rejected from " +
                   " (select count(*) as count_pending from rsk_trn_ttier1format " +
                   " where tier1_approvalstatus = 'Pending' and zonal_riskmanagergid = '" + employee_gid + "') as a, " +
                   " (select count(*) as count_approved from rsk_trn_ttier1format " +
                   " where tier1_approvalstatus = 'Approved' and zonal_riskmanagergid = '" + employee_gid + "')  as b, " +
                   " (select count(*) as count_rejected from rsk_trn_ttier1format " +
                   " where tier1_approvalstatus = 'Rejected' and zonal_riskmanagergid = '" + employee_gid + "') as c";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.count_pending = objODBCDatareader["count_pending"].ToString();
                values.count_approved = objODBCDatareader["count_approved"].ToString();
                values.count_rejected = objODBCDatareader["count_rejected"].ToString();
            }
            objODBCDatareader.Close();
            return true;
        }

        public bool DaGetTier1ApprovalDtl(string tier1format_gid, tier1format values)
        {

            msSQL = " select tier1format_gid,tier3_status,observation_reportgid,customer_name,customer_urn,tier1_observations, " +
                   " tier1_code,tier1_justification,tier1_managementcomments,approval_remarks,tier1_approvalstatus, " +
                   " tier1_processgap,tier1_processrecommendation,tier1_reverts_actionplan,tier1code_changereason, " +
                   " date_format(tier1_atrdate, '%d-%m-%Y') as tier1_atrdate,tier1code_changeflag," +
                   " date_format(created_date, '%d-%m-%Y') as created_date " +
                   " from rsk_trn_ttier1format where tier1format_gid = '" + tier1format_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.customer_name = objODBCDatareader["customer_name"].ToString();
                values.customer_urn = objODBCDatareader["customer_urn"].ToString();
                values.tier1format_gid = objODBCDatareader["tier1format_gid"].ToString();
                values.observation_reportgid = objODBCDatareader["observation_reportgid"].ToString();
                values.tier1_observations = objODBCDatareader["tier1_observations"].ToString();
                values.tier1_code = objODBCDatareader["tier1_code"].ToString();
                values.tier1_justification = objODBCDatareader["tier1_justification"].ToString();
                values.tier1_processgap = objODBCDatareader["tier1_processgap"].ToString();
                values.tier1_processrecommendation = objODBCDatareader["tier1_processrecommendation"].ToString();
                values.tier1_reverts_actionplan = objODBCDatareader["tier1_reverts_actionplan"].ToString();
                values.tier1_atrdate = objODBCDatareader["tier1_atrdate"].ToString();
                values.created_date = objODBCDatareader["created_date"].ToString();
                values.tier1_managementcomments = objODBCDatareader["tier1_managementcomments"].ToString();
                values.tier1_approvalstatus = objODBCDatareader["tier1_approvalstatus"].ToString();
                values.tier3_status = objODBCDatareader["tier3_status"].ToString();
                values.tier1code_changereason = objODBCDatareader["tier1code_changereason"].ToString();
                values.tier1code_changeflag = objODBCDatareader["tier1code_changeflag"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " select approval_status,approval_remarks,concat(b.user_firstname, ' ' ,b.user_lastname,' / ',b.user_code) as created_by, " +
                    " date_format(a.created_date, '%d-%m-%Y') as created_date from rsk_trn_ttier1approvallog a " +
                    " left join adm_mst_tuser b on a.created_by=b.user_gid " +
                    " where tier1format_gid='" + tier1format_gid + "' order by tier1approval_loggid asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_tier1approvallog = new List<tier1approvallog>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    get_tier1approvallog.Add(new tier1approvallog
                    {
                        approval_status = dt["approval_status"].ToString(),
                        approval_remarks = dt["approval_remarks"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                    });
                }
                values.tier1approvallog = get_tier1approvallog;
            }
            dt_datatable.Dispose();

            msSQL = " select reject_remarks,concat(b.user_firstname, ' ' ,b.user_lastname,' / ',b.user_code) as created_by, " +
                 " date_format(a.created_date, '%d-%m-%Y') as created_date from rsk_trn_ttier1RMRejectlog a " +
                 " left join adm_mst_tuser b on a.created_by=b.user_gid " +
                 " where tier1format_gid='" + values.tier1format_gid + "' order by tier1rmreject_loggid asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_tier1rejectlog = new List<tier1rejectlog>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    get_tier1rejectlog.Add(new tier1rejectlog
                    {
                        reject_remarks = dt["reject_remarks"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                    });
                }
                values.tier1rejectlog = get_tier1rejectlog;
            }
            dt_datatable.Dispose();

            msSQL = " select tier1document_gid,document_title,document_name,document_path,date_format(a.created_date, '%d-%m-%Y') as created_date, " +
                   " concat(b.user_firstname, ' ', b.user_lastname, '/', b.user_code) as created_by from rsk_trn_ttier1document a " +
                   " left join adm_mst_tuser b on a.created_by = b.user_gid " +
                   " where tier1format_gid='" + values.tier1format_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_tier1document = new List<tier1doc>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    get_tier1document.Add(new tier1doc
                    {
                        tier1document_gid = dt["tier1document_gid"].ToString(),
                        document_name = dt["document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData((dt["document_path"].ToString())),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        document_title = dt["document_title"].ToString(),
                    });
                }
                values.tier1doc = get_tier1document;
            }
            dt_datatable.Dispose();

            return true;
        }

        public void DaPostTier1Approved(tier1approval values, string user_gid)
        {
            msSQL = " update rsk_trn_ttier1format set " +
                    " tier1_approved_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                    " tier1_approvalstatus='Approved'," +
                    " tier1_approvalflag='N'" +
                    " where tier1format_gid='" + values.tier1format_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msGetGid = objcmnfunctions.GetMasterGID("TIAP");

            msSQL = " insert into rsk_trn_ttier1approvallog(" +
                   " tier1approval_loggid," +
                   " tier1format_gid, " +
                   " approval_status," +
                   " approval_remarks," +
                   " created_by," +
                   " created_date)" +
                   " values(" +
                   "'" + msGetGid + "'," +
                   "'" + values.tier1format_gid + "', " +
                   "'Approved', " +
                   "'" + values.approval_remarks.Replace("'", "\\'") + "'," +
                   "'" + user_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Tier 1 Approved Successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
        }

        public void DaPostTier1Rejected(tier1approval values, string user_gid)
        {
            msSQL = " update rsk_trn_ttier1format set " +
                   " tier1_approvalstatus='Rejected'," +
                   " tier1_approvalflag='N'" +
                   " where tier1format_gid='" + values.tier1format_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msGetGid = objcmnfunctions.GetMasterGID("TIAP");

            msSQL = " insert into rsk_trn_ttier1approvallog(" +
                  " tier1approval_loggid," +
                  " tier1format_gid, " +
                  " approval_status," +
                  " approval_remarks," +
                  " created_by," +
                  " created_date)" +
                  " values(" +
                  "'" + msGetGid + "'," +
                  "'" + values.tier1format_gid + "', " +
                  "'Rejected', " +
                  "'" + values.approval_remarks.Replace("'", "\\'") + "'," +
                  "'" + user_gid + "'," +
                  "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Tier 1 Rejected Successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
        }

        // Tier 2 Details...//

        public void DaGetTier2Monthdtl(tier2zonaldtl values, string employee_gid, string user_gid)
        {

            msSQL = " SELECT DATE_FORMAT(curdate(), '%m') month " +
                    " union SELECT DATE_FORMAT(date_add(curdate(), interval - 1 month), '%m') month " +
                    " union SELECT DATE_FORMAT(date_add(curdate(), interval - 2 month), '%m') month " +
                    " union SELECT DATE_FORMAT(date_add(curdate(), interval - 3 month), '%m') month";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_monthname = new List<monthname>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    int month = Convert.ToInt16(dt["month"].ToString());
                    string lsmonthname = System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat.GetMonthName(month);
                    get_monthname.Add(new monthname
                    {
                        month = dt["month"].ToString(),
                        month_name = lsmonthname,
                    });
                }
                values.monthname = get_monthname;
            }
            dt_datatable.Dispose();


            msSQL = "select zonalmapping_gid,zonal_name from rsk_mst_tzonalmapping where zonalrisk_managerGid='" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.zonal_name = objODBCDatareader["zonal_name"].ToString();
                values.zonalmapping_gid = objODBCDatareader["zonalmapping_gid"].ToString();
            }
            objODBCDatareader.Close();
            msSQL = "delete from rsk_tmp_ttier2document where created_by='" + user_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
        }

        public void DaGetTier2Summary(tier2preparationlist values, string user_gid)
        {
            msSQL = " select tier2preparation_gid,zonal_name,monthname(str_to_date(tier2_month, '%m')) as MonthName, " +
                    " vertical,headRMD_name,tier2_approval_status,date_format(created_date, '%d-%m-%Y') as created_date " +
                    " from rsk_trn_ttier2preparation where created_by='" + user_gid + "' order by tier2preparation_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_tier2preparation = new List<tier2preparation>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {

                    get_tier2preparation.Add(new tier2preparation
                    {
                        tier2preparation_gid = dt["tier2preparation_gid"].ToString(),
                        zonal_name = dt["zonal_name"].ToString(),
                        tier2_month = dt["MonthName"].ToString(),
                        vertical = dt["vertical"].ToString(),
                        headRMD_name = dt["headRMD_name"].ToString(),
                        tier2_approval_status = dt["tier2_approval_status"].ToString(),
                        created_date = dt["created_date"].ToString(),
                    });
                }
                values.tier2preparation = get_tier2preparation;
            }
            dt_datatable.Dispose();

            msSQL = " select a.count_pending , b.count_approved,c.count_rejected from  " +
                " (select count(*) as count_pending from rsk_trn_ttier2preparation where " +
                " tier2_approval_status = 'Pending' and created_by = '" + user_gid + "') as a, " +
                " (select count(*) as count_approved from rsk_trn_ttier2preparation where " +
                " tier2_approval_status = 'Approved' and created_by = '" + user_gid + "')  as b, " +
                " (select count(*) as count_rejected from rsk_trn_ttier2preparation " +
                " where tier2_approval_status = 'Rejected' and created_by = '" + user_gid + "') as c";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.count_pending = objODBCDatareader["count_pending"].ToString();
                values.count_approved = objODBCDatareader["count_approved"].ToString();
                values.count_rejected = objODBCDatareader["count_rejected"].ToString();
            }
            objODBCDatareader.Close();
        }

        public bool DaPostTier2Upload(HttpRequest httpRequest, tier2documentlist objfilename, string user_gid)
        {
            upload_list objdocumentmodel = new upload_list();
            HttpFileCollection httpFileCollection;

            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            //MemoryStream ms = new MemoryStream();
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string pdfFilName = string.Empty;
            Stream ls_readStream;
            string lsdocumenttype_gid = string.Empty;
            string lsdocument_title = httpRequest.Form["document_title"].ToString();
            string project_flag = httpRequest.Form["project_flag"].ToString();
            String path = lspath;

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);

            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "RSK/Tier2Document/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
            {
                if ((!System.IO.Directory.Exists(path)))
                    System.IO.Directory.CreateDirectory(path);
            }
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
                        ls_readStream = httpPostedFile.InputStream;
                        MemoryStream ms = new MemoryStream();
                        ls_readStream.CopyTo(ms);

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.message = "File format is not supported";
                            return false;
                        }

                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "RSK/Tier2Document/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "RSK/Tier2Document/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension;
                         
                        msSQL = " insert into rsk_tmp_ttier2document( " +
                                    " document_title," +
                                    " document_name ," +
                                    " document_path," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + lsdocument_title + "'," +
                                    "'" + httpPostedFile.FileName + "'," +
                                    "'" + lspath + "'," +
                                    "'" + user_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult == 1)
                        {
                            objfilename.status = true;
                        }
                        else
                        {
                            objfilename.status = false;
                        }
                    }
                }
                msSQL = "select tmptier2document_gid,document_title,document_name,document_path,created_date " +
                        " from rsk_tmp_ttier2document where created_by='" + user_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_filename = new List<tier2document>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_filename.Add(new tier2document
                        {

                            tmp_documentGid = (dr_datarow["tmptier2document_gid"].ToString()),
                            document_name = dr_datarow["document_name"].ToString(),
                            document_path = objcmnstorage.EncryptData((dr_datarow["document_path"].ToString())),
                            created_date = dr_datarow["created_date"].ToString(),
                            document_title = dr_datarow["document_title"].ToString(),
                        });
                    }
                    objfilename.tier2document = get_filename;
                }
                dt_datatable.Dispose();
            }
            catch
            {

            }
            return true;
        }

        public bool DaGetTier2UploadCancel(string tmp_documentGid, uploaddocument objfilename, string user_gid)
        {
            msSQL = "delete from rsk_tmp_ttier2document where tmptier2document_gid='" + tmp_documentGid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "select tmptier2document_gid,document_title,document_name,document_path,created_date " +
                       " from rsk_tmp_ttier2document where created_by='" + user_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_filename = new List<upload_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    get_filename.Add(new upload_list
                    {
                        document_title = dr_datarow["document_title"].ToString(),
                        tmp_documentGid = (dr_datarow["tmptier2document_gid"].ToString()),
                        document_name = dr_datarow["document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData((dr_datarow["document_path"].ToString())),
                        created_date = dr_datarow["created_date"].ToString(),
                    });
                }
                objfilename.upload_list = get_filename;
            }
            dt_datatable.Dispose();

            if (mnResult != 0)
            {
                objfilename.status = true;
                objfilename.message = "Document Deleted Successfully..!";
                return true;
            }
            else
            {
                objfilename.status = false;
                objfilename.message = "Error Occured..!";
                return false;
            }

        }

        public void DaPostTier2Preparation(tier2preparation values, string user_gid, string employee_gid)
        {

            msGetGid = objcmnfunctions.GetMasterGID("TI2P");

            msSQL = " insert into rsk_trn_ttier2preparation(" +
                   " tier2preparation_gid," +
                   " zonalmapping_gid, " +
                   " zonal_name," +
                   " tier2_month," +
                   " vertical_gid," +
                   " vertical," +
                   " headRMD_gid," +
                   " headRMD_name," +
                   " tier2_remarks," +
                   " tier2_approval_status ," +
                   " tier2_approvalflag," +
                   " created_by," +
                   " created_date)" +
                   " values(" +
                   "'" + msGetGid + "'," +
                   "'" + values.zonalmapping_gid + "', " +
                   "'" + values.zonal_name + "'," +
                   "'" + values.tier2_month + "'," +
                   "'" + values.vertical_gid + "'," +
                   "'" + values.vertical + "'," +
                   "'" + values.headRMD_gid + "'," +
                   "'" + values.headRMD_name + "'," +
                   "'" + values.tier2_remarks.Replace("'", "\\'") + "'," +
                   "'Pending'," +
                   "'Y'," +
                   "'" + user_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "select tmptier2document_gid,document_title,document_name,document_path from rsk_tmp_ttier2document where created_by='" + user_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msGetDocumentGid = objcmnfunctions.GetMasterGID("TI2D");

                    msSQL = " Insert into rsk_trn_ttier2document( " +
                              " tier2document_gid," +
                              " tier2preparation_gid," +
                              " document_title," +
                              " document_name," +
                              " document_path," +
                              " created_by," +
                              " created_date)" +
                              " values(" +
                              "'" + msGetDocumentGid + "', " +
                              "'" + msGetGid + "'," +
                              "'" + dt["document_title"].ToString() + "'," +
                              "'" + dt["document_name"].ToString() + "'," +
                              "'" + dt["document_path"].ToString() + "'," +
                              "'" + user_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult == 1)
                    {
                        msSQL = "delete from rsk_tmp_ttier2document where tmptier2document_gid ='" + dt["tmptier2document_gid"].ToString() + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }

            }
            dt_datatable.Dispose();

            msSQL = " select a.tier1format_gid,vertical,a.vertical_gid,date_format(tier1_approved_date,'%Y-%m-%d %H:%m') as tier1_approved_date, " +
                            " a.allocationdtl_gid,a.customer_name,a.customer_urn,assignedRM_name as riskmanager,a.tier1_code  " +
                            " from rsk_trn_ttier1format a " +
                            " left join rsk_trn_tallocationdtl b on a.allocationdtl_gid = b.allocationdtl_gid " +
                            " where tier1_approvalstatus = 'Approved' and a.tier1format_gid not in (select tier1format_gid from rsk_trn_ttierallocationdtl )" +
                            " and vertical_gid ='" + values.vertical_gid + "' and zonal_riskmanagergid = '" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msSQL = "select tmptier2_codechange,tier2_code,tier2code_changereason from rsk_tmp_ttier2codechange where tier1format_gid='" + dt["tier1format_gid"].ToString() + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        string msGetTier_gid = objcmnfunctions.GetMasterGID("TIAL");

                        msSQL = " Insert into rsk_trn_ttierallocationdtl( " +
                                 " tierallocation_gid," +
                                 " tier2preparation_gid," +
                                 " tier1format_gid," +
                                 " allocationdtl_gid," +
                                 " tier1_approveddate," +
                                 " vertical_gid," +
                                 " customer_name," +
                                 " customer_urn," +
                                 " tier2_code," +
                                 " tier2code_changereason," +
                                 " tier2code_changeflag," +
                                 " riskmanager," +
                                 " created_by," +
                                 " created_date)" +
                                 " values(" +
                                 "'" + msGetTier_gid + "', " +
                                 "'" + msGetGid + "'," +
                                 "'" + dt["tier1format_gid"].ToString() + "'," +
                                 "'" + dt["allocationdtl_gid"].ToString() + "'," +
                                 "'" + dt["tier1_approved_date"].ToString() + "'," +
                                 "'" + dt["vertical_gid"].ToString() + "'," +
                                 "'" + dt["customer_name"].ToString() + "'," +
                                 "'" + dt["customer_urn"].ToString() + "'," +
                                 "'" + objODBCDatareader["tier2_code"].ToString() + "'," +
                                 "'" + objODBCDatareader["tier2code_changereason"].ToString() + "'," +
                                 "'Y'," +
                                 "'" + dt["riskmanager"].ToString() + "'," +
                                 "'" + user_gid + "'," +
                                 "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult == 1)
                        {
                            msSQL = "delete from rsk_tmp_ttier2codechange where tmptier2_codechange='" + objODBCDatareader["tmptier2_codechange"].ToString() + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                        objODBCDatareader.Close();
                    }
                    else
                    {
                        objODBCDatareader.Close();
                        string msGetTier_gid = objcmnfunctions.GetMasterGID("TIAL");

                        msSQL = " Insert into rsk_trn_ttierallocationdtl( " +
                                 " tierallocation_gid," +
                                 " tier2preparation_gid," +
                                 " tier1format_gid," +
                                 " allocationdtl_gid," +
                                 " tier1_approveddate," +
                                 " vertical_gid," +
                                 " customer_name," +
                                 " customer_urn," +
                                 " tier2_code," +
                                 " riskmanager," +
                                 " created_by," +
                                 " created_date)" +
                                 " values(" +
                                 "'" + msGetTier_gid + "', " +
                                 "'" + msGetGid + "'," +
                                 "'" + dt["tier1format_gid"].ToString() + "'," +
                                 "'" + dt["allocationdtl_gid"].ToString() + "'," +
                                 "'" + dt["tier1_approved_date"].ToString() + "'," +
                                 "'" + dt["vertical_gid"].ToString() + "'," +
                                 "'" + dt["customer_name"].ToString() + "'," +
                                 "'" + dt["customer_urn"].ToString() + "'," +
                                 "'" + dt["tier1_code"].ToString() + "'," +
                                 "'" + dt["riskmanager"].ToString() + "'," +
                                 "'" + user_gid + "'," +
                                 "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    }
                }
            }
            dt_datatable.Dispose();
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Tier 2 Created Successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
        }

        public bool DaGetTier2ApprovalSummary(string employee_gid, tier2preparationlist values)
        {
            msSQL = " select tier2preparation_gid,zonal_name,tier2_month, " +
                     " vertical,headRMD_name,tier2_approval_status,date_format(a.created_date, '%d-%m-%Y') as created_date, " +
                     " concat(b.user_firstname,' ',b.user_lastname,'/',b.user_code) as created_by " +
                     " from rsk_trn_ttier2preparation a " +
                     " left join adm_mst_tuser b on a.created_by=b.user_gid" +
                     " where headRMD_gid='" + employee_gid + "' order by tier2_approval_status desc,tier2preparation_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_tier2preparation = new List<tier2preparation>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    int month = Convert.ToInt16(dt["tier2_month"].ToString());
                    string lsmonthname = System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat.GetMonthName(month);

                    get_tier2preparation.Add(new tier2preparation
                    {
                        tier2preparation_gid = dt["tier2preparation_gid"].ToString(),
                        zonal_name = dt["zonal_name"].ToString(),
                        tier2_month = lsmonthname,
                        vertical = dt["vertical"].ToString(),
                        headRMD_name = dt["headRMD_name"].ToString(),
                        tier2_approval_status = dt["tier2_approval_status"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                    });
                }
                values.tier2preparation = get_tier2preparation;
            }
            dt_datatable.Dispose();

            msSQL = " select a.count_pending , b.count_approved,c.count_rejected from  " +
                   " (select count(*) as count_pending from rsk_trn_ttier2preparation where " +
                   " tier2_approval_status = 'Pending' and headRMD_gid = '" + employee_gid + "') as a, " +
                   " (select count(*) as count_approved from rsk_trn_ttier2preparation where " +
                   " tier2_approval_status = 'Approved' and headRMD_gid = '" + employee_gid + "')  as b, " +
                   " (select count(*) as count_rejected from rsk_trn_ttier2preparation " +
                   " where tier2_approval_status = 'Rejected' and headRMD_gid = '" + employee_gid + "') as c";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.count_pending = objODBCDatareader["count_pending"].ToString();
                values.count_approved = objODBCDatareader["count_approved"].ToString();
                values.count_rejected = objODBCDatareader["count_rejected"].ToString();
            }
            objODBCDatareader.Close();
            return true;
        }

        public bool DaGetTier2ViewDtl(string tier2preparation_gid, tier2viewdtl values, string employee_gid)
        {

            msSQL = " select tier2preparation_gid,zonalmapping_gid,zonal_name,tier3_status, " +
                    "  vertical,vertical_gid,tier2_month, " +
                    " headRMD_name,headRMD_gid,tier2_remarks,tier2_approval_status, " +
                    " date_format(a.created_date, '%d-%m-%Y') as created_date, " +
                    " concat(b.user_firstname, ' ', b.user_lastname, '/', b.user_code) as created_by " +
                    " from rsk_trn_ttier2preparation a " +
                    " left join adm_mst_tuser b on a.created_by = b.user_gid" +
                    " where tier2preparation_gid='" + tier2preparation_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.tier2preparation_gid = objODBCDatareader["tier2preparation_gid"].ToString();
                values.zonal_name = objODBCDatareader["zonal_name"].ToString();
                values.tier2_month = objODBCDatareader["tier2_month"].ToString();
                values.vertical = objODBCDatareader["vertical"].ToString();
                values.vertical_gid = objODBCDatareader["vertical_gid"].ToString();
                values.headRMD_name = objODBCDatareader["headRMD_name"].ToString();
                values.headRMD_gid = objODBCDatareader["headRMD_gid"].ToString();
                values.tier2_remarks = objODBCDatareader["tier2_remarks"].ToString();
                values.tier2_approval_status = objODBCDatareader["tier2_approval_status"].ToString();
                values.created_date = objODBCDatareader["created_date"].ToString();
                values.created_by = objODBCDatareader["created_by"].ToString();
                values.tier3_status = objODBCDatareader["tier3_status"].ToString();
                values.zonalmapping_gid = objODBCDatareader["zonalmapping_gid"].ToString();
            }
            objODBCDatareader.Close();

            int month = Convert.ToInt16(values.tier2_month);
            values.tier2_monthname = System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat.GetMonthName(month);

            msSQL = " select tier2document_gid,document_title,document_name,document_path,date_format(a.created_date, '%d-%m-%Y') as created_date, " +
                    " concat(b.user_firstname, ' ', b.user_lastname, '/', b.user_code) as created_by from rsk_trn_ttier2document a " +
                    " left join adm_mst_tuser b on a.created_by = b.user_gid " +
                    " where tier2preparation_gid='" + tier2preparation_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_tier2document = new List<tier2document>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    get_tier2document.Add(new tier2document
                    {
                        tier2document_gid = dt["tier2document_gid"].ToString(),
                        document_name = dt["document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData((dt["document_path"].ToString())),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        document_title = dt["document_title"].ToString(),
                    });
                }
                values.tier2document = get_tier2document;
            }
            dt_datatable.Dispose();

            msSQL = " select approval_status,approval_remarks,concat(b.user_firstname, ' ' ,b.user_lastname,' / ',b.user_code) as approved_by, " +
                   " date_format(a.created_date, '%d-%m-%Y') as approved_date from rsk_trn_ttier2approvallog a " +
                   " left join adm_mst_tuser b on a.created_by=b.user_gid " +
                   " where tier2preparation_gid='" + tier2preparation_gid + "' order by tier2approval_loggid asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_tier2approvallog = new List<tier2approvallog>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    get_tier2approvallog.Add(new tier2approvallog
                    {
                        approval_status = dt["approval_status"].ToString(),
                        approval_remarks = dt["approval_remarks"].ToString(),
                        approved_by = dt["approved_by"].ToString(),
                        approved_date = dt["approved_date"].ToString(),
                    });
                }
                values.tier2approvallog = get_tier2approvallog;
            }
            dt_datatable.Dispose();

            msSQL = " select a.tierallocation_gid,a.allocationdtl_gid,a.tier2_code,date_format(tier1_approveddate,'%d-%m-%Y') as tier1_approveddate, " +
                   " a.tier1format_gid,customer_urn,customer_name,riskmanager,concat(b.user_firstname,' ',b.user_lastname,'/',b.user_code) as ZRM_name,  " +
                   " c.tier2_code as tier2_codechange,a.tier2preparation_gid " +
                   " from rsk_trn_ttierallocationdtl a " +
                   " left join rsk_tmp_ttier2codechange c on a.tierallocation_gid=c.tierallocation_gid " +
                   " left join adm_mst_tuser b on a.created_by=b.user_gid " +
                   " where a.tier2preparation_gid='" + tier2preparation_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var gettierallocationdtl = new List<tierallocationdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    gettierallocationdtl.Add(new tierallocationdtl
                    {
                        tier1_approveddate = (dr_datarow["tier1_approveddate"].ToString()),
                        customer_name = (dr_datarow["customer_name"].ToString()),
                        customer_urn = (dr_datarow["customer_urn"].ToString()),
                        riskmanager = dr_datarow["riskmanager"].ToString(),
                        ZRM_name = (dr_datarow["ZRM_name"].ToString()),
                        tier2_code = (dr_datarow["tier2_code"].ToString()),
                        tier2_codechange = (dr_datarow["tier2_codechange"].ToString()),
                        allocationdtl_gid = (dr_datarow["allocationdtl_gid"].ToString()),
                        tierallocation_gid = (dr_datarow["tierallocation_gid"].ToString()),
                        tier2preparation_gid = (dr_datarow["tier2preparation_gid"].ToString()),
                        tier1format_gid=(dr_datarow["tier1format_gid"].ToString()),
                    });
                }
                values.tierallocationdtl = gettierallocationdtl;
            }
            dt_datatable.Dispose();

            //msSQL = " select a.tier1format_gid,vertical,a.vertical_gid,date_format(tier1_approved_date,'%Y-%m-%d %H:%m') as tier1_approved_date, " +
            //   " a.allocationdtl_gid,a.customer_name,a.customer_urn,assignedRM_name as riskmanager  " +
            //   " from rsk_trn_ttier1format a " +
            //   " left join rsk_trn_tallocationdtl b on a.allocationdtl_gid = b.allocationdtl_gid " +
            //   " where tier1_approvalstatus = 'Approved' and month(tier1_approved_date) = '" + values.tier2_month + "'" +
            //   " and vertical_gid ='" + values.vertical_gid + "' and zonal_riskmanagergid = '" + employee_gid + "' and tier2_preparationflag!='Y'";
            //dt_datatable = objdbconn.GetDataTable(msSQL);
            //var gettierallocationlatestdtl = new List<tierallocationdtlLatest>();
            //if (dt_datatable.Rows.Count != 0)
            //{
            //    foreach (DataRow dr_datarow in dt_datatable.Rows)
            //    {
            //        gettierallocationlatestdtl.Add(new tierallocationdtlLatest
            //        {
            //            tier1_approveddate = (dr_datarow["tier1_approveddate"].ToString()),
            //            customer_name = (dr_datarow["customer_name"].ToString()),
            //            customer_urn = (dr_datarow["customer_urn"].ToString()),
            //            riskmanager = dr_datarow["riskmanager"].ToString(),
            //            ZRM_name = (dr_datarow["ZRM_name"].ToString()),
            //        });
            //    }
            //    values.tierallocationdtlLatest = gettierallocationlatestdtl;
            //}
            //dt_datatable.Dispose();

            return true;
        }

        public bool DaPostTrnTier2Upload(HttpRequest httpRequest, tier2documentlist values, string user_gid)
        {
            upload_list objdocumentmodel = new upload_list();
            HttpFileCollection httpFileCollection;

            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            //MemoryStream ms = new MemoryStream();
            MemoryStream ms_strea= new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string pdfFilName = string.Empty;
            Stream ls_readStream;
            string lsdocumenttype_gid = string.Empty;
            string lsdocument_title = httpRequest.Form["document_title"].ToString();
            string lstier2preparartion_gid = httpRequest.Form["tier2preparartion_gid"].ToString();
            string project_flag = httpRequest.Form["project_flag"].ToString();
            String path = lspath;

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);

            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "RSK/Tier2Document/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
            {
                if ((!System.IO.Directory.Exists(path)))
                    System.IO.Directory.CreateDirectory(path);
            }
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
                        ls_readStream = httpPostedFile.InputStream;
                        MemoryStream ms = new MemoryStream();
                        ls_readStream.CopyTo(ms);

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            values.message = "File format is not supported";
                            return false;
                        }

                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "RSK/Tier2Document/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "RSK/Tier2Document/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension;
                          
                        msGetDocumentGid = objcmnfunctions.GetMasterGID("TI2D");
                        msSQL = " insert into rsk_trn_ttier2document( " +
                                    " tier2document_gid," +
                                    " tier2preparation_gid, " +
                                    " document_title," +
                                    " document_name ," +
                                    " document_path," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + msGetDocumentGid + "'," +
                                    "'" + lstier2preparartion_gid + "'," +
                                    "'" + lsdocument_title + "'," +
                                    "'" + httpPostedFile.FileName + "'," +
                                    "'" + lspath + "'," +
                                    "'" + user_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult == 1)
                        {
                            values.message = "Document Uploaded Successfully..!";
                            values.status = true;
                        }
                        else
                        {
                            values.message = "Error Occured..!";
                            values.status = false;
                        }
                    }
                }
                msSQL = "select tier2document_gid,document_title,document_name,document_path,created_date " +
                        " from rsk_trn_ttier2document where tier2preparation_gid='" + lstier2preparartion_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_filename = new List<tier2document>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_filename.Add(new tier2document
                        {

                            tier2document_gid = (dr_datarow["tier2document_gid"].ToString()),
                            document_name = dr_datarow["document_name"].ToString(),
                            document_path = objcmnstorage.EncryptData((dr_datarow["document_path"].ToString())),
                            created_date = dr_datarow["created_date"].ToString(),
                            document_title = dr_datarow["document_title"].ToString(),
                        });
                    }
                    values.tier2document = get_filename;
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = ex.ToString();
                values.status = false;
            }
            return true;
        }

        public bool DaGetTier2TrnUploadCancel(string tier2document_gid, string tier2preparation_gid, tier2documentlist objfilename)
        {
            msSQL = "delete from rsk_trn_ttier2document where tier2document_gid='" + tier2document_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "select tier2document_gid,document_name,document_title,document_path,created_date " +
                       " from rsk_trn_ttier2document where tier2preparation_gid='" + tier2preparation_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_filename = new List<tier2document>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    get_filename.Add(new tier2document
                    {
                        document_title = (dr_datarow["document_title"].ToString()),
                        tier2document_gid = (dr_datarow["tier2document_gid"].ToString()),
                        document_name = dr_datarow["document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData((dr_datarow["document_path"].ToString())),
                        created_date = dr_datarow["created_date"].ToString(),
                    });
                }
                objfilename.tier2document = get_filename;
            }
            dt_datatable.Dispose();

            if (mnResult != 0)
            {
                objfilename.status = true;
                objfilename.message = "Document Deleted Successfully..!";
                return true;
            }
            else
            {
                objfilename.status = false;
                objfilename.message = "Error Occured..!";
                return false;
            }

        }

        public bool DaPostUpdateTier2(string user_gid, tier2preparation values, string employee_gid)
        {
            msSQL = "select tier2_approval_status from rsk_trn_ttier2preparation where tier2preparation_gid='" + values.tier2preparation_gid + "'";
            string lsapproval_status = objdbconn.GetExecuteScalar(msSQL);

            msGetGid = objcmnfunctions.GetMasterGID("T2HY");
            msSQL = " insert into rsk_trn_ttier2history(historytier2_gid,tier2preparation_gid,zonalmapping_gid, " +
                    " tier2_month,vertical_gid,vertical,headRMD_gid,headRMD_name, " +
                    " tier2_remarks,tier2_approval_status,created_by,created_date) " +
                    " (select '" + msGetGid + "',tier2preparation_gid,zonalmapping_gid, " +
                    " tier2_month,vertical_gid,vertical,headRMD_gid,headRMD_name, " +
                    " tier2_remarks,tier2_approval_status,'" + user_gid + "',curdate() " +
                    " from rsk_trn_ttier2preparation where tier2preparation_gid= '" + values.tier2preparation_gid + "') ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " update rsk_trn_ttier2preparation set " +
          " tier2_month='" + values.tier2_month + "'," +
          " vertical_gid='" + values.vertical_gid + "'," +
          " vertical='" + values.vertical + "'," +
          " headRMD_gid='" + values.headRMD_gid + "'," +
          " headRMD_name='" + values.headRMD_name + "'," +
          " tier2_remarks='" + values.tier2_remarks.Replace("'", "\\'") + "'," +
          " tier2_approval_status='Pending'," +
          " updated_by='" + user_gid + "'," +
          " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
          " where tier2preparation_gid='" + values.tier2preparation_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            //msSQL = " select tierallocation_gid,tmptier2_codechange,tier2_code,tier2code_changereason from rsk_tmp_ttier2codechange " +
            //        " where tier2preparation_gid='" + values.tier2preparation_gid + "'";
            //dt_datatable = objdbconn.GetDataTable(msSQL);
            //objODBCDatareader = objdbconn.GetDataReader(msSQL);
            //if (dt_datatable.Rows.Count != 0)
            //{
            //    foreach (DataRow dt in dt_datatable.Rows)
            //    {
            //        msSQL = " update rsk_trn_ttierallocationdtl set tier2_code='" + dt["tier2_code"].ToString() + "'," +
            //                " tier2code_changereason='" + dt["tier2code_changereason"].ToString() + "', " +
            //                " tier2code_changeflag='Y'" +
            //                " where tierallocation_gid='" + dt["tierallocation_gid"].ToString() + "'";
            //        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            //    }
            //}
            //dt_datatable.Dispose();

            //msSQL = "delete from rsk_trn_ttierallocationdtl where tier2preparation_gid='" + values.tier2preparation_gid + "'";
            //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            //msSQL = " select a.tier1format_gid,vertical,a.vertical_gid,date_format(tier1_approved_date,'%Y-%m-%d %H:%m') as tier1_approved_date, " +
            //      " a.allocationdtl_gid,a.customer_name,a.customer_urn,assignedRM_name as riskmanager  " +
            //      " from rsk_trn_ttier1format a " +
            //      " left join rsk_trn_tallocationdtl b on a.allocationdtl_gid = b.allocationdtl_gid " +
            //      " where tier1_approvalstatus = 'Approved' and month(tier1_approved_date) = '" + values.tier2_month + "'" +
            //      " and vertical_gid ='" + values.vertical_gid + "' and zonal_riskmanagergid = '" + employee_gid + "'";
            //dt_datatable = objdbconn.GetDataTable(msSQL);
            //if (dt_datatable.Rows.Count != 0)
            //{
            //    foreach (DataRow dt in dt_datatable.Rows)
            //    {
            //        string msGetTier_gid = objcmnfunctions.GetMasterGID("TIAL");

            //        msSQL = " Insert into rsk_trn_ttierallocationdtl( " +
            //                 " tierallocation_gid," +
            //                 " tier2preparation_gid," +
            //                 " tier1format_gid," +
            //                 " allocationdtl_gid," +
            //                 " tier1_approveddate," +
            //                 " vertical_gid," +
            //                 " customer_name," +
            //                 " customer_urn," +
            //                 " riskmanager," +
            //                 " created_by," +
            //                 " created_date)" +
            //                 " values(" +
            //                 "'" + msGetTier_gid + "', " +
            //                 "'" + msGetGid + "'," +
            //                 "'" + dt["tier1format_gid"].ToString() + "'," +
            //                 "'" + dt["allocationdtl_gid"].ToString() + "'," +
            //                 "'" + dt["tier1_approved_date"].ToString() + "'," +
            //                 "'" + dt["vertical_gid"].ToString() + "'," +
            //                 "'" + dt["customer_name"].ToString() + "'," +
            //                 "'" + dt["customer_urn"].ToString() + "'," +
            //                 "'" + dt["riskmanager"].ToString() + "'," +
            //                 "'" + user_gid + "'," +
            //                 "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            //        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            //    }
            //}
            //dt_datatable.Dispose();

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Tier 2 Submitted Successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
            return true;
        }

        public void DaPostTier2Approved(tier2approval values, string user_gid)
        {
            msSQL = " update rsk_trn_ttier2preparation set " +
                    " tier2_approveddate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                    " tier2_approval_status='Approved'," +
                    " tier2_approvalflag='N'" +
                    " where tier2preparation_gid='" + values.tier2preparation_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " update rsk_trn_ttierallocationdtl set " +
                   " tier2_approvedflag='Y'" +
                   " where tier2preparation_gid='" + values.tier2preparation_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msGetGid = objcmnfunctions.GetMasterGID("T2AP");

            msSQL = " insert into rsk_trn_ttier2approvallog(" +
                   " tier2approval_loggid," +
                   " tier2preparation_gid, " +
                   " approval_status," +
                   " approval_remarks," +
                   " created_by," +
                   " created_date)" +
                   " values(" +
                   "'" + msGetGid + "'," +
                   "'" + values.tier2preparation_gid + "', " +
                   "'Approved', " +
                   "'" + values.approval_remarks.Replace("'", "\\'") + "'," +
                   "'" + user_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Tier 2 Approved Successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
        }

        public void DaPostTier2Rejected(tier2approval values, string user_gid)
        {
            msSQL = " update rsk_trn_ttier2preparation set " +
                   " tier2_approval_status='Rejected'," +
                   " tier2_approvalflag='Y'" +
                   " where tier2preparation_gid='" + values.tier2preparation_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msGetGid = objcmnfunctions.GetMasterGID("T2AP");

            msSQL = " insert into rsk_trn_ttier2approvallog(" +
                   " tier2approval_loggid," +
                   " tier2preparation_gid, " +
                   " approval_status," +
                   " approval_remarks," +
                   " created_by," +
                   " created_date)" +
                   " values(" +
                   "'" + msGetGid + "'," +
                   "'" + values.tier2preparation_gid + "', " +
                   "'Rejected', " +
                   "'" + values.approval_remarks.Replace("'", "\\'") + "'," +
                   "'" + user_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Tier 2 Rejected Successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
        }

        // Tier 3 Details...//

        public void DaGetTier3Monthdtl(tier2zonaldtl values, string user_gid)
        {

            msSQL = " SELECT DATE_FORMAT(curdate(), '%m') month " +
                    " union SELECT DATE_FORMAT(date_add(curdate(), interval - 1 month), '%m') month " +
                    " union SELECT DATE_FORMAT(date_add(curdate(), interval - 2 month), '%m') month " +
                    " union SELECT DATE_FORMAT(date_add(curdate(), interval - 3 month), '%m') month";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_monthname = new List<monthname>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    int month = Convert.ToInt16(dt["month"].ToString());
                    string lsmonthname = System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat.GetMonthName(month);
                    get_monthname.Add(new monthname
                    {
                        month = dt["month"].ToString(),
                        month_name = lsmonthname,
                    });
                }
                values.monthname = get_monthname;
            }
            dt_datatable.Dispose();

            msSQL = "delete from rsk_tmp_ttier3document where created_by='" + user_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
        }

        public void DaPostTier3Preparation(tier3preparation values, string user_gid, string employee_gid)
        {

            msGetGid = objcmnfunctions.GetMasterGID("TI3P");

            msSQL = " insert into rsk_trn_ttier3preparation(" +
                   " tier3preparation_gid," +
                   " MLRC_date," +
                   " tier3_month," +
                   " vertical_gid," +
                   " vertical," +
                   " follow_up," +
                   " tier3_status ," +
                   " created_by," +
                   " created_date)" +
                   " values(" +
                   "'" + msGetGid + "'," +
                   "'" + Convert.ToDateTime(values.MLRC_date).ToString("yyyy-MM-dd") + "'," +
                   "'" + values.tier3_month + "', " +
                   "'" + values.vertical_gid + "'," +
                   "'" + values.vertical + "'," +
                   "'" + values.follow_up.Replace("'", "\\'") + "'," +
                   "'Pending'," +
                   "'" + user_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "select tmptier3document_gid,document_title,document_name,document_path from rsk_tmp_ttier3document where created_by='" + user_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msGetDocumentGid = objcmnfunctions.GetMasterGID("TI3D");

                    msSQL = " Insert into rsk_trn_ttier3document( " +
                              " tier3document_gid," +
                              " tier3preparation_gid," +
                              " document_title," +
                              " document_name," +
                              " document_path," +
                              " created_by," +
                              " created_date)" +
                              " values(" +
                              "'" + msGetDocumentGid + "', " +
                              "'" + msGetGid + "'," +
                              "'" + dt["document_title"].ToString() + "'," +
                              "'" + dt["document_name"].ToString() + "'," +
                              "'" + dt["document_path"].ToString() + "'," +
                              "'" + user_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult == 1)
                    {
                        msSQL = "delete from rsk_tmp_ttier3document where tmptier3document_gid ='" + dt["tmptier3document_gid"].ToString() + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }

            }
            dt_datatable.Dispose();

            msSQL = " select a.tier2preparation_gid from rsk_trn_ttier2preparation a " +
                    " left join rsk_trn_ttierallocationdtl c on a.tier2preparation_gid = c.tier2preparation_gid " +
                    " where a.vertical_gid = '" + values.vertical_gid + "' and a.tier2_month = '" + values.tier3_month + "'" +
                    " and a.headRMD_gid = '" + employee_gid + "' and a.tier2_approval_status = 'Approved'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lstier2preparation_gid = objODBCDatareader["tier2preparation_gid"].ToString();
            }
            objODBCDatareader.Close();

            if (lstier2preparation_gid != "")
            {
                msSQL = " update rsk_trn_ttierallocationdtl set tier3_flag='N' where tier2preparation_gid='" + lstier2preparation_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                string msGetCompletedGid = objcmnfunctions.GetMasterGID("TICO");

                msSQL = " Insert into rsk_trn_ttier3completion( " +
                            " tier3completion_gid," +
                            " tier3preparation_gid," +
                            " tier2preparation_gid," +
                            " created_by," +
                            " created_date)" +
                            " values(" +
                            "'" + msGetCompletedGid + "', " +
                            "'" + msGetGid + "'," +
                            "'" + lstier2preparation_gid + "'," +
                            "'" + user_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            msSQL = " select tierallocation_gid,tier2_code,tier3_code,tier3code_changereason from rsk_trn_ttierallocationdtl where tier2preparation_gid= '" + lstier2preparation_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    if (dt["tier3_code"].ToString() != "")
                    {
                        msSQL = " update rsk_trn_ttierallocationdtl set tier3_code='" + dt["tier3_code"].ToString() + "'," +
                           " tier3code_changereason='" + dt["tier3code_changereason"].ToString() + "', " +
                           " tier3code_changeflag='Y'," +
                           " tier3code_updatedby='" + user_gid + "'," +
                           " tier3code_updateddate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                           " where tierallocation_gid='" + dt["tierallocation_gid"].ToString() + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    else
                    {
                        msSQL = " update rsk_trn_ttierallocationdtl set tier3_code='" + dt["tier2_code"].ToString() + "'," +
                          " tier3code_updatedby='" + user_gid + "'," +
                          " tier3code_updateddate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                          " where tierallocation_gid='" + dt["tierallocation_gid"].ToString() + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }

                }
            }

            dt_datatable.Dispose();

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Tier 3 Created Successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
        }

        public void DaPostTier3Complete(tier3completedtl values, string user_gid)
        {
            msSQL = " update rsk_trn_ttier3preparation set " +
                    " completed_remarks='" + values.completed_remarks.Replace("'", "\\'") + "'," +
                    " tier3_status='Completed'," +
                    " completed_flag='Y'," +
                    " completed_by='" + user_gid + "'," +
                    " completed_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    " where tier3preparation_gid='" + values.tier3preparation_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " update rsk_trn_ttier3completion set " +
                    " completed_flag='Y'" +
                    " where tier3preparation_gid='" + values.tier3preparation_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " select tier2preparation_gid from rsk_trn_ttier3completion " +
                   "  where tier3preparation_gid='" + values.tier3preparation_gid + "' and completed_flag='Y'";
            string lstier2preparation_gid = objdbconn.GetExecuteScalar(msSQL);

            if (lstier2preparation_gid != "")
            {
                msSQL = " update rsk_trn_ttier2preparation set tier3_status = 'Completed' " +
                        " where tier2preparation_gid='" + lstier2preparation_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update rsk_trn_ttierallocationdtl set tier3_flag='Y' " +
                       " where tier2preparation_gid='" + lstier2preparation_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " select tier1format_gid,allocationdtl_gid from rsk_trn_ttierallocationdtl " +
                       " where tier2preparation_gid = '" + lstier2preparation_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        msSQL = " update rsk_trn_ttier1format set tier3_status = 'Completed' " +
                            " where tier1format_gid = '" + dt["tier1format_gid"].ToString() + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " update rsk_trn_tallocationdtl set tier3_status = 'Completed' " +
                               " where allocationdtl_gid = '" + dt["allocationdtl_gid"].ToString() + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                dt_datatable.Dispose();
            }

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Tier 3 Completed Successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
        }

        public bool DaPostTier3Upload(HttpRequest httpRequest, tier3documentlist objfilename, string user_gid)
        {
            upload_list objdocumentmodel = new upload_list();
            HttpFileCollection httpFileCollection;

            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            //MemoryStream ms = new MemoryStream();
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string pdfFilName = string.Empty;
            Stream ls_readStream;
            string lsdocumenttype_gid = string.Empty;
            string lsdocument_title = httpRequest.Form["document_title"].ToString();
            string project_flag = httpRequest.Form["project_flag"].ToString();
            String path = lspath;

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);

            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "RSK/Tier3Document/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
            {
                if ((!System.IO.Directory.Exists(path)))
                    System.IO.Directory.CreateDirectory(path);
            }
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
                        ls_readStream = httpPostedFile.InputStream;
                        MemoryStream ms = new MemoryStream();
                        ls_readStream.CopyTo(ms);

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.message = "File format is not supported";
                            return false;
                        }

                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "RSK/Tier3Document/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "RSK/Tier3Document/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension;
                         
                        msSQL = " insert into rsk_tmp_ttier3document( " +
                                    " document_title," +
                                    " document_name ," +
                                    " document_path," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + lsdocument_title + "'," +
                                    "'" + httpPostedFile.FileName + "'," +
                                    "'" + lspath + "'," +
                                    "'" + user_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult == 1)
                        {
                            objfilename.status = true;
                        }
                        else
                        {
                            objfilename.status = false;
                        }
                    }
                }
                msSQL = "select tmptier3document_gid,document_title,document_name,document_path,created_date " +
                        " from rsk_tmp_ttier3document where created_by='" + user_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_filename = new List<tier3document>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_filename.Add(new tier3document
                        {

                            tmp_documentGid = (dr_datarow["tmptier3document_gid"].ToString()),
                            document_name = dr_datarow["document_name"].ToString(),
                            document_path = objcmnstorage.EncryptData((dr_datarow["document_path"].ToString())),
                            created_date = dr_datarow["created_date"].ToString(),
                            document_title = dr_datarow["document_title"].ToString(),
                        });
                    }
                    objfilename.tier3document = get_filename;
                }
                dt_datatable.Dispose();
            }
            catch
            {

            }
            return true;
        }

        public bool DaGetTier3UploadCancel(string tmp_documentGid, uploaddocument objfilename, string user_gid)
        {
            msSQL = "delete from rsk_tmp_ttier3document where tmptier3document_gid='" + tmp_documentGid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "select tmptier3document_gid,document_title,document_name,document_path,created_date " +
                       " from rsk_tmp_ttier3document where created_by='" + user_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_filename = new List<upload_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    get_filename.Add(new upload_list
                    {
                        document_title = (dr_datarow["document_title"].ToString()),
                        tmp_documentGid = (dr_datarow["tmptier3document_gid"].ToString()),
                        document_name = dr_datarow["document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData((dr_datarow["document_path"].ToString())),
                        created_date = dr_datarow["created_date"].ToString(),
                    });
                }
                objfilename.upload_list = get_filename;
            }
            dt_datatable.Dispose();

            if (mnResult != 0)
            {
                objfilename.status = true;
                objfilename.message = "Document Deleted Successfully..!";
                return true;
            }
            else
            {
                objfilename.status = false;
                objfilename.message = "Error Occured..!";
                return false;
            }

        }

        public void DaGetTier3Summary(tier3preparationlist values, string user_gid)
        {
            msSQL = " select tier3preparation_gid,tier3_month, " +
                    " date_format(MLRC_date, '%d-%m-%Y') as MLRC_date,completed_flag," +
                    " date_format(completed_date, '%d-%m-%Y') as completed_date," +
                    " vertical,tier3_status,date_format(created_date, '%d-%m-%Y') as created_date " +
                    " from rsk_trn_ttier3preparation where created_by='" + user_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_tier3preparation = new List<tier3preparation>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    int month = Convert.ToInt16(dt["tier3_month"].ToString());
                    string lsmonthname = System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat.GetMonthName(month);
                    get_tier3preparation.Add(new tier3preparation
                    {
                        tier3preparation_gid = dt["tier3preparation_gid"].ToString(),
                        MLRC_date = dt["MLRC_date"].ToString(),
                        tier3_monthname = lsmonthname,
                        vertical = dt["vertical"].ToString(),
                        tier3_status = dt["tier3_status"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        completed_date = dt["completed_date"].ToString(),
                        completed_flag = dt["completed_flag"].ToString(),
                    });
                }
                values.tier3preparation = get_tier3preparation;
            }
            dt_datatable.Dispose();

            msSQL = " select a.count_pending , b.count_completed from  " +
                " (select count(*) as count_pending from rsk_trn_ttier3preparation where " +
                " tier3_status = 'Pending' and created_by = '" + user_gid + "') as a, " +
                " (select count(*) as count_completed from rsk_trn_ttier3preparation " +
                " where tier3_status = 'Completed' and created_by = '" + user_gid + "') as b";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.count_pending = objODBCDatareader["count_pending"].ToString();
                values.count_completed = objODBCDatareader["count_completed"].ToString();
            }
            objODBCDatareader.Close();
        }

        public void DaGetTier3CompletedSummary(tier3preparationlist values)
        {
            msSQL = " select tier3preparation_gid,monthname(str_to_date(tier3_month, '%m')) as MonthName, " +
                    " date_format(MLRC_date, '%d-%m-%Y') as MLRC_date," +
                    " date_format(completed_date, '%d-%m-%Y') as completed_date," +
                    " vertical,tier3_status,date_format(created_date, '%d-%m-%Y') as created_date " +
                    " from rsk_trn_ttier3preparation where tier3_status='Completed' and completed_flag='Y' order by tier3preparation_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_tier3preparation = new List<tier3preparation>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    get_tier3preparation.Add(new tier3preparation
                    {
                        tier3preparation_gid = dt["tier3preparation_gid"].ToString(),
                        MLRC_date = dt["MLRC_date"].ToString(),
                        tier3_monthname = dt["MonthName"].ToString(),
                        vertical = dt["vertical"].ToString(),
                        tier3_status = dt["tier3_status"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        completed_date = dt["completed_date"].ToString(),
                    });
                }
                values.tier3preparation = get_tier3preparation;
            }
            dt_datatable.Dispose();
        }

        public bool DaGetTier3ViewDtl(string tier3preparation_gid, tier3viewdtl values)
        {

            msSQL = " select tier3preparation_gid,date_format(a.MLRC_date, '%d-%m-%Y') as MLRC_date,tier3_month, " +
                    " monthname(str_to_date(tier3_month, '%m')) as tier3_monthname,a.MLRC_date as MLRC_Date, " +
                    "  vertical,vertical_gid, follow_up,tier3_status,completed_flag, " +
                    " date_format(a.completed_date, '%d-%m-%Y') as completed_date,completed_remarks, " +
                    " date_format(a.created_date, '%d-%m-%Y') as created_date, " +
                    " concat(c.user_firstname, ' ', c.user_lastname, '/', c.user_code) as completed_by," +
                    " concat(b.user_firstname, ' ', b.user_lastname, '/', b.user_code) as created_by " +
                    " from rsk_trn_ttier3preparation a " +
                    " left join adm_mst_tuser b on a.created_by = b.user_gid" +
                    " left join adm_mst_tuser c on a.completed_by = c.user_gid" +
                    " where tier3preparation_gid='" + tier3preparation_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.tier3preparation_gid = objODBCDatareader["tier3preparation_gid"].ToString();
                values.MLRC_date = objODBCDatareader["MLRC_date"].ToString();
                values.tier3_month = objODBCDatareader["tier3_month"].ToString();
                values.tier3_monthname = objODBCDatareader["tier3_monthname"].ToString();
                values.vertical = objODBCDatareader["vertical"].ToString();
                values.vertical_gid = objODBCDatareader["vertical_gid"].ToString();
                values.follow_up = objODBCDatareader["follow_up"].ToString();
                values.tier3_status = objODBCDatareader["tier3_status"].ToString();
                values.created_date = objODBCDatareader["created_date"].ToString();
                values.created_by = objODBCDatareader["created_by"].ToString();
                values.completed_flag = objODBCDatareader["completed_flag"].ToString();
                values.completed_date = objODBCDatareader["completed_date"].ToString();
                values.completed_by = objODBCDatareader["completed_by"].ToString();
                values.completed_remarks = objODBCDatareader["completed_remarks"].ToString();
                if (objODBCDatareader["MLRC_Date"].ToString() != "")
                {
                    values.MLRC_Date = Convert.ToDateTime(objODBCDatareader["MLRC_Date"].ToString());
                }
            }
            objODBCDatareader.Close();

            int month = Convert.ToInt16(values.tier3_month);
            values.tier3_monthname = System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat.GetMonthName(month);

            msSQL = " select tier3document_gid,document_title,document_name,document_path,date_format(a.created_date, '%d-%m-%Y') as created_date, " +
                    " concat(b.user_firstname, ' ', b.user_lastname, '/', b.user_code) as created_by from rsk_trn_ttier3document a " +
                    " left join adm_mst_tuser b on a.created_by = b.user_gid " +
                    " where tier3preparation_gid='" + tier3preparation_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_tier3document = new List<tier3document>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    get_tier3document.Add(new tier3document
                    {
                        tier3document_gid = dt["tier3document_gid"].ToString(),
                        document_name = dt["document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData((dt["document_path"].ToString())),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        document_title = dt["document_title"].ToString(),
                    });
                }
                values.tier3document = get_tier3document;
            }
            dt_datatable.Dispose();

            msSQL = " select a.tierallocation_gid,zonal_name,vertical,date_format(tier2_approveddate, '%d-%m-%Y') as tier2_approveddate,tier3_code,allocationdtl_gid, " +
                    " concat(b.user_firstname, ' ', b.user_lastname, ' / ', b.user_code) as ZRM_name,customer_name,customer_urn, " +
                    " a.tier1format_gid,riskmanager from rsk_trn_ttierallocationdtl a " +
                    " left join adm_mst_tuser b on a.created_by = b.user_gid " +
                    " left join rsk_trn_ttier2preparation c on a.tier2preparation_gid = c.tier2preparation_gid " +
                    " left join rsk_trn_ttier3completion d on a.tier2preparation_gid = d.tier2preparation_gid " +
                    " where d.tier3preparation_gid = '" + values.tier3preparation_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var gettierallocationdtl = new List<tierallocationdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    gettierallocationdtl.Add(new tierallocationdtl
                    {
                        vertical = (dr_datarow["vertical"].ToString()),
                        zonal_name = (dr_datarow["zonal_name"].ToString()),
                        tier2_approveddate = (dr_datarow["tier2_approveddate"].ToString()),
                        ZRM_name = dr_datarow["ZRM_name"].ToString(),
                        customer_name = (dr_datarow["customer_name"].ToString()),
                        customer_urn = (dr_datarow["customer_urn"].ToString()),
                        riskmanager = dr_datarow["riskmanager"].ToString(),
                        tier3_code = dr_datarow["tier3_code"].ToString(),
                        allocationdtl_gid = dr_datarow["allocationdtl_gid"].ToString(),
                        tierallocation_gid = dr_datarow["tierallocation_gid"].ToString(),
                        tier1format_gid = dr_datarow["tier1format_gid"].ToString(),
                    });
                }
                values.tierallocationdtl = gettierallocationdtl;
            }
            dt_datatable.Dispose();

            return true;
        }

        public bool DaPostTrnTier3Upload(HttpRequest httpRequest, tier3documentlist values, string user_gid)
        {
            upload_list objdocumentmodel = new upload_list();
            HttpFileCollection httpFileCollection;

            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            //MemoryStream ms = new MemoryStream();
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string pdfFilName = string.Empty;
            Stream ls_readStream;
            string lsdocumenttype_gid = string.Empty;
            string lsdocument_title = httpRequest.Form["document_title"].ToString();
            string lstier3preparartion_gid = httpRequest.Form["tier3preparation_gid"].ToString();
            string project_flag = httpRequest.Form["project_flag"].ToString();
            String path = lspath;

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);

            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "RSK/Tier3Document/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
            {
                if ((!System.IO.Directory.Exists(path)))
                    System.IO.Directory.CreateDirectory(path);
            }
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
                        ls_readStream = httpPostedFile.InputStream;
                        MemoryStream ms = new MemoryStream();
                        ls_readStream.CopyTo(ms);

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            values.message = "File format is not supported";
                            return false;
                        }

                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "RSK/Tier3Document/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "RSK/Tier3Document/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension;
                         
                        msGetDocumentGid = objcmnfunctions.GetMasterGID("TI3D");
                        msSQL = " insert into rsk_trn_ttier3document( " +
                                    " tier3document_gid," +
                                    " tier3preparation_gid, " +
                                    " document_title," +
                                    " document_name ," +
                                    " document_path," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + msGetDocumentGid + "'," +
                                    "'" + lstier3preparartion_gid + "'," +
                                    "'" + lsdocument_title + "'," +
                                    "'" + httpPostedFile.FileName + "'," +
                                    "'" + lspath + "'," +
                                    "'" + user_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult == 1)
                        {
                            values.message = "Document Uploaded Successfully..!";
                            values.status = true;
                        }
                        else
                        {
                            values.message = "Error Occured..!";
                            values.status = false;
                        }
                    }
                }
                msSQL = "select tier3document_gid,document_title,document_name,document_path,created_date " +
                        " from rsk_trn_ttier3document where tier3preparation_gid='" + lstier3preparartion_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_filename = new List<tier3document>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_filename.Add(new tier3document
                        {

                            tier3document_gid = (dr_datarow["tier3document_gid"].ToString()),
                            document_name = dr_datarow["document_name"].ToString(),
                            document_path = objcmnstorage.EncryptData((dr_datarow["document_path"].ToString())),
                            created_date = dr_datarow["created_date"].ToString(),
                            document_title = dr_datarow["document_title"].ToString(),
                        });
                    }
                    values.tier3document = get_filename;
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = ex.ToString();
                values.status = false;
            }
            return true;
        }

        public bool DaGetTier3TrnUploadCancel(string tier3document_gid, string tier3preparation_gid, tier3documentlist objfilename)
        {
            msSQL = "delete from rsk_trn_ttier3document where tier3document_gid='" + tier3document_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "select tier3document_gid,document_name,document_title,document_path,created_date " +
                       " from rsk_trn_ttier3document where tier3preparation_gid='" + tier3preparation_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_filename = new List<tier3document>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    get_filename.Add(new tier3document
                    {
                        document_title = (dr_datarow["document_title"].ToString()),
                        tier3document_gid = (dr_datarow["tier3document_gid"].ToString()),
                        document_name = dr_datarow["document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData((dr_datarow["document_path"].ToString())),
                        created_date = dr_datarow["created_date"].ToString(),
                    });
                }
                objfilename.tier3document = get_filename;
            }
            dt_datatable.Dispose();

            if (mnResult != 0)
            {
                objfilename.status = true;
                objfilename.message = "Document Deleted Successfully..!";
                return true;
            }
            else
            {
                objfilename.status = false;
                objfilename.message = "Error Occured..!";
                return false;
            }

        }

        public bool DaPostUpdateTier3(string user_gid, tier3preparation values, string employee_gid)
        {
            msSQL = " update rsk_trn_ttier3preparation set " +
          " MLRC_date='" + Convert.ToDateTime(values.MLRC_date).ToString("yyyy-MM-dd") + "'," +
          " tier3_month='" + values.tier3_month + "'," +
          " vertical_gid='" + values.vertical_gid + "'," +
          " vertical='" + values.vertical + "'," +
          " follow_up='" + values.follow_up.Replace("'", "\\'") + "'," +
          " updated_by='" + user_gid + "'," +
          " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
          " where tier3preparation_gid='" + values.tier3preparation_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " select a.tier2preparation_gid from rsk_trn_ttier2preparation a " +
                 " left join rsk_trn_ttierallocationdtl c on a.tier2preparation_gid = c.tier2preparation_gid " +
                 " where a.vertical_gid = '" + values.vertical_gid + "' and a.tier2_month = '" + values.tier3_month + "'" +
                 " and a.headRMD_gid = '" + employee_gid + "' and a.tier2_approval_status = 'Approved'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                msSQL = " update rsk_trn_ttierallocationdtl set tier3_approvedflag = 'Y' " +
                        " where tier2preparation_gid = '" + objODBCDatareader["tier2preparation_gid"].ToString() + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            objODBCDatareader.Close();

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Tier 3 Details are Updated Successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
            return true;
        }

        public void DaPostTier2codeChange(tier2code values, string user_gid, result objvalues)
        {
            if (values.tier2code_Trnchange == "Y")
            {
                msSQL = " insert into rsk_tmp_ttier2codechange(" +
                                 " tierallocation_gid, " +
                                 " tier2preparation_gid ," +
                                 " allocationdtl_gid," +
                                 " tier2_code," +
                                 " tier2code_changereason ," +
                                 " created_by," +
                                 " created_date)" +
                                 " values(" +
                                 "'" + values.tierallocation_gid + "'," +
                                 "'" + values.tier2preparation_gid + "'," +
                                 "'" + values.allocationdtl_gid + "', " +
                                 "'" + values.tier2_code + "', " +
                                 "'" + values.tier2code_changereason.Replace("'", "\\'") + "'," +
                                 "'" + user_gid + "'," +
                                 "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            }
            else
            {
                msSQL = " insert into rsk_tmp_ttier2codechange(" +
                                  " tier1format_gid, " +
                                  " allocationdtl_gid," +
                                  " tier2_code," +
                                  " tier2code_changereason ," +
                                  " created_by," +
                                  " created_date)" +
                                  " values(" +
                                  "'" + values.tier1format_gid + "'," +
                                  "'" + values.allocationdtl_gid + "', " +
                                  "'" + values.tier2_code + "', " +
                                  "'" + values.tier2code_changereason.Replace("'", "\\'") + "'," +
                                  "'" + user_gid + "'," +
                                  "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            msSQL = " insert into rsk_trn_tcodehistory( " +
                    " allocationdtl_gid ," +
                    " tier_code ," +
                    " tiercode_changereason ," +
                    " codechange_stage ," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + values.allocationdtl_gid + "', " +
                    "'" + values.tier2_code + "'," +
                    "'" + values.tier2code_changereason.Replace("'", "\\'") + "', " +
                    "'Tier 2', " +
                    "'" + user_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                objvalues.status = true;
            }
            else
            {
                objvalues.status = false;
            }
        }

        public void DaGetTier2ColorDetails(string allocationdtl_gid, tiercodedtllist values)
        {
            var get_tier3preparation = new List<tiercodedtl>();

            msSQL = "select risk_code as visit_code from rsk_trn_tvisitreportgenerate where allocationdtl_gid='" + allocationdtl_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                get_tier3preparation.Add(new tiercodedtl
                {
                    tier_stage = "At Visit Stage",
                    tier_code = objODBCDatareader["visit_code"].ToString(),
                    tiercode_changereason = "No Change",
                    delete_flag = "N",
                });
                values.tiercodedtl = get_tier3preparation;
            }
            objODBCDatareader.Close();

            msSQL = "select tier1_code as tier1_code,tier1code_changereason  from rsk_trn_ttier1format  where allocationdtl_gid='" + allocationdtl_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                if (objODBCDatareader["tier1code_changereason"].ToString() == "")
                {
                    lschangereason = "No Change";
                }
                else
                {
                    lschangereason = objODBCDatareader["tier1code_changereason"].ToString();
                }
                get_tier3preparation.Add(new tiercodedtl
                {
                    tier_stage = "At Tier 1 Stage",
                    tier_code = objODBCDatareader["tier1_code"].ToString(),
                    tiercode_changereason = lschangereason,
                    delete_flag = "N",
                });
                values.tiercodedtl = get_tier3preparation;
            }
            objODBCDatareader.Close();

            msSQL = "select tmptier2_codechange,tier2_code,tier2code_changereason from rsk_tmp_ttier2codechange where allocationdtl_gid='" + allocationdtl_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.tier_stage = "At Tier 2 Stage";
                values.tier_code = objODBCDatareader["tier2_code"].ToString();
                values.tiercode_changereason = objODBCDatareader["tier2code_changereason"].ToString();
                values.tmptier2_codechange = objODBCDatareader["tmptier2_codechange"].ToString();
            }
            objODBCDatareader.Close();
        }

        public void DaGetTier2ColorDelete(string tmptier2_codechange, result values)
        {
            msSQL = " delete from rsk_tmp_ttier2codechange where tmptier2_codechange='" + tmptier2_codechange + "'";
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


        public void DaGetTierColorDetails(string allocationdtl_gid, tiercodedtllist values)
        {

            var get_tier3preparation = new List<tiercodedtl>();

            msSQL = "select risk_code as visit_code from rsk_trn_tvisitreportgenerate where allocationdtl_gid='" + allocationdtl_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                get_tier3preparation.Add(new tiercodedtl
                {
                    tier_stage = "At Visit Stage",
                    tier_code = objODBCDatareader["visit_code"].ToString(),
                    tiercode_changereason = "No Change",
                });
                values.tiercodedtl = get_tier3preparation;
            }
            objODBCDatareader.Close();

            msSQL = "select tier1_code as tier1_code,tier1code_changereason  from rsk_trn_ttier1format  where allocationdtl_gid='" + allocationdtl_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                if (objODBCDatareader["tier1code_changereason"].ToString() == "")
                {
                    lschangereason = "No Change";
                }
                else
                {
                    lschangereason = objODBCDatareader["tier1code_changereason"].ToString();
                }
                get_tier3preparation.Add(new tiercodedtl
                {
                    tier_stage = "At Tier 1 Stage",
                    tier_code = objODBCDatareader["tier1_code"].ToString(),
                    tiercode_changereason = lschangereason,
                });
                values.tiercodedtl = get_tier3preparation;
            }
            objODBCDatareader.Close();

            msSQL = "select tier2_code,tier2code_changereason from rsk_trn_ttierallocationdtl where allocationdtl_gid='" + allocationdtl_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                if (objODBCDatareader["tier2code_changereason"].ToString() == "")
                {
                    lschangereason = "No Change";
                }
                else
                {
                    lschangereason = objODBCDatareader["tier2code_changereason"].ToString();
                }
                get_tier3preparation.Add(new tiercodedtl
                {
                    tier_stage = "At Tier 2 Stage",
                    tier_code = objODBCDatareader["tier2_code"].ToString(),
                    tiercode_changereason = lschangereason,
                });
                values.tiercodedtl = get_tier3preparation;
            }
            objODBCDatareader.Close();

            msSQL = "select tier3_code,tier3code_changereason from rsk_trn_ttierallocationdtl where allocationdtl_gid='" + allocationdtl_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                if (objODBCDatareader["tier3_code"].ToString() != "")
                {
                    if (objODBCDatareader["tier3code_changereason"].ToString() == "")
                    {
                        lschangereason = "No Change";
                    }
                    else
                    {
                        lschangereason = objODBCDatareader["tier3code_changereason"].ToString();
                    }
                    get_tier3preparation.Add(new tiercodedtl
                    {
                        tier_stage = "At Tier 3 Stage",
                        tier_code = objODBCDatareader["tier3_code"].ToString(),
                        tiercode_changereason = lschangereason,
                    });
                    values.tiercodedtl = get_tier3preparation;
                }
            }
            objODBCDatareader.Close();

            msSQL = "select tmptier3_codechangegid,tier3_code,tier3code_changereason from rsk_tmp_ttier3codechange where allocationdtl_gid='" + allocationdtl_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.tier_stage = "At Tier 3 Stage";
                values.tier_code = objODBCDatareader["tier3_code"].ToString();
                values.tiercode_changereason = objODBCDatareader["tier3code_changereason"].ToString();
                values.tmptier2_codechange = objODBCDatareader["tmptier3_codechangegid"].ToString();
            }
            objODBCDatareader.Close();
        }

        public void DaPostTier3codeChange(tier3code values, string user_gid, result objvalues)
        {
            msSQL = " insert into rsk_tmp_ttier3codechange(" +
                    " tierallocation_gid, " +
                    " allocationdtl_gid," +
                    " tier3_code," +
                    " tier3code_changereason ," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + values.tierallocation_gid + "'," +
                    "'" + values.allocationdtl_gid + "', " +
                    "'" + values.tier3_code + "', " +
                    "'" + values.tier3code_changereason.Replace("'", "\\'") + "'," +
                    "'" + user_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                objvalues.status = true;
            }
            else
            {
                objvalues.status = false;
            }
        }

        public void DaGetTier3ColorDelete(string tmptier3_codechangegid, result values)
        {
            msSQL = " delete from rsk_tmp_ttier3codechange where tmptier3_codechangegid='" + tmptier3_codechangegid + "'";
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

        public void DaPostTierColorUpdate(tiercodechange values, result objvalues, string user_gid)
        {
            if (values.tier3_flag == "Y")
            {
                msSQL = " update rsk_trn_ttierallocationdtl set tier3_code='" + values.tier_code + "'," +
                        " tier3code_changereason='" + values.tiercode_changereason + "', " +
                        " tier3code_changeflag='Y'," +
                        " tier3code_updatedby='" + user_gid + "'," +
                        " tier3code_updateddate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                        " where tierallocation_gid='" + values.tierallocation_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "select allocationdtl_gid from rsk_trn_ttierallocationdtl where tierallocation_gid='" + values.tierallocation_gid + "'";
                string lsallocationdtl_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " insert into rsk_trn_tcodehistory( " +
                 " allocationdtl_gid ," +
                 " tierallocation_gid ," +
                 " tier_code ," +
                 " tiercode_changereason ," +
                 " codechange_stage ," +
                 " created_by," +
                 " created_date)" +
                 " values(" +
                 "'" + lsallocationdtl_gid + "', " +
                 "'" + values.tierallocation_gid + "', " +
                 "'" + values.tier_code + "'," +
                 "'" + values.tiercode_changereason.Replace("'", "\\'") + "', " +
                 "'Tier 3', " +
                 "'" + user_gid + "'," +
                 "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else
            {
                msSQL = " update rsk_trn_ttierallocationdtl set tier2_code='" + values.tier_code + "'," +
                        " tier2code_changereason='" + values.tiercode_changereason + "', " +
                        " tier2code_changeflag='Y'," +
                        " tier2code_updatedby='" + user_gid + "'," +
                        " tier2code_updateddate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                        " where tierallocation_gid='" + values.tierallocation_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "select allocationdtl_gid from rsk_trn_ttierallocationdtl where tierallocation_gid='" + values.tierallocation_gid + "'";
                string lsallocationdtl_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " insert into rsk_trn_tcodehistory( " +
                 " allocationdtl_gid ," +
                 " tierallocation_gid ," +
                 " tier_code ," +
                 " tiercode_changereason ," +
                 " codechange_stage ," +
                 " created_by," +
                 " created_date)" +
                 " values(" +
                 "'" + lsallocationdtl_gid + "', " +
                 "'" + values.tierallocation_gid + "', " +
                 "'" + values.tier_code + "'," +
                 "'" + values.tiercode_changereason.Replace("'", "\\'") + "', " +
                 "'Tier 2', " +
                 "'" + user_gid + "'," +
                 "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            if (mnResult != 0)
            {
                objvalues.status = true;
                objvalues.message = "Tier Code Updated Successfully..!";
            }
            else
            {
                objvalues.status = false;
                objvalues.message = "Error Occured..!";
            }
        }

        public bool DaGetViewTierObservationdtl(string allocationdtl_gid, observationTierdtl values)
        {
            msSQL = " select observation_reportgid,visitreport_generategid,allocationdtl_gid,customer_name,customer_urn,risk_code, " +
                    " date_format(dateof_RMDvisit, '%d-%m-%Y') as dateof_RMDvisit,report_pertainingto, " +
                    " vertical,format(disbursement_amount,2) as disbursement_amount,approving_authority," +
                    " date_format(loansanction_date, '%d-%m-%Y') as loansanction_date ,observation_flag," +
                    " relationship_manager_gid,relationship_manager_name,date_format(relationshipmanager_updateddate,'%d-%m-%Y') as atr_completiondate, " +
                    " PPA_name,RMDvisit_officialname, date_format(loandisbursement_date, '%d-%m-%Y') as loandisbursement_date , " +
                    " people_accompaniedRMD,format(sanction_amount,2) as sanction_amount,format(outstanding_amount,2) as outstanding_amount, " +
                    " current_DPD,contact_details1,contact_details2, " +
                    " concat(b.user_firstname, ' / ', b.user_lastname) as created_by,date_format(a.created_date, '%d-%m-%Y') as created_date " +
                    " from rsk_trn_tobservationreport a " +
                    " left join adm_mst_tuser b on a.created_by = b.user_gid" +
                    " where allocationdtl_gid='" + allocationdtl_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.observation_reportgid = objODBCDatareader["observation_reportgid"].ToString();
                values.visitreport_generategid = objODBCDatareader["visitreport_generategid"].ToString();
                values.allocationdtl_gid = objODBCDatareader["allocationdtl_gid"].ToString();
                values.customer_urn = objODBCDatareader["customer_urn"].ToString();
                values.customer_name = objODBCDatareader["customer_name"].ToString();
                values.dateof_RMDvisit = objODBCDatareader["dateof_RMDvisit"].ToString();
                values.report_pertainingto = objODBCDatareader["report_pertainingto"].ToString();
                values.vertical = objODBCDatareader["vertical"].ToString();
                values.disbursement_amount = objODBCDatareader["disbursement_amount"].ToString();
                values.approving_authority = objODBCDatareader["approving_authority"].ToString();
                values.loansanction_date = objODBCDatareader["loansanction_date"].ToString();
                values.relationship_manager_name = objODBCDatareader["relationship_manager_name"].ToString();
                values.PPA_name = objODBCDatareader["PPA_name"].ToString();
                values.RMDvisit_officialname = objODBCDatareader["RMDvisit_officialname"].ToString();
                values.loandisbursement_date = objODBCDatareader["loandisbursement_date"].ToString();
                values.people_accompaniedRMD = objODBCDatareader["people_accompaniedRMD"].ToString();
                values.sanction_amount = objODBCDatareader["sanction_amount"].ToString();
                values.outstanding_amount = objODBCDatareader["outstanding_amount"].ToString();
                values.current_DPD = objODBCDatareader["current_DPD"].ToString();
                values.contact_details1 = objODBCDatareader["contact_details1"].ToString();
                values.contact_details2 = objODBCDatareader["contact_details2"].ToString();
                values.atr_completiondate = objODBCDatareader["atr_completiondate"].ToString();
                values.created_by = objODBCDatareader["created_by"].ToString();
                values.created_date = objODBCDatareader["created_date"].ToString();
                values.observation_flag = objODBCDatareader["observation_flag"].ToString();
                values.risk_code = objODBCDatareader["risk_code"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " select critical_observationgid,criteria, RMD_observations, actionable_recommended, relationship_manager_remarks, remarks_flag, " +
                " concat(b.user_firstname, ' / ', b.user_lastname) as remarks_updatedby,date_format(a.remarks_updateddate, '%d-%m-%Y') as remarks_updateddate, " +
                " concat(c.user_firstname, ' / ', c.user_lastname) as created_by" +
                " from rsk_trn_tcriticalobservation a " +
                " left join adm_mst_tuser b on a.remarks_updatedby = b.user_gid" +
                " left join adm_mst_tuser c on a.created_by = c.user_gid" +
                " where observation_reportgid='" + values.observation_reportgid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_criticalobservation = new List<criticalTierobservation>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    get_criticalobservation.Add(new criticalTierobservation
                    {
                        criteria = dt["criteria"].ToString(),
                        RMD_observations = dt["RMD_observations"].ToString(),
                        actionable_recommended = dt["actionable_recommended"].ToString(),
                        relationship_manager_remarks = dt["relationship_manager_remarks"].ToString(),
                        remarks_flag = dt["remarks_flag"].ToString(),
                        remarks_updatedby = dt["remarks_updatedby"].ToString(),
                        remarks_updateddate = dt["remarks_updateddate"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        critical_observationgid = dt["critical_observationgid"].ToString(),
                    });
                }
                values.criticalTierobservation = get_criticalobservation;
            }
            dt_datatable.Dispose();


            var get_tier3preparation = new List<tierReportdtl>();

            msSQL = " select tier1_code as tier1_code,tier1code_changereason,zonal_riskmanagername,tier1_approvalstatus, " +
                    " date_format(tier1_approved_date, '%d-%m-%Y') as tier1_approveddate  from rsk_trn_ttier1format " +
                    " where allocationdtl_gid='" + allocationdtl_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                if (objODBCDatareader["tier1code_changereason"].ToString() == "")
                {
                    lschangereason = "No Change";
                }
                else
                {
                    lschangereason = objODBCDatareader["tier1code_changereason"].ToString();
                }
                get_tier3preparation.Add(new tierReportdtl
                {
                    tier_stage = "At Tier 1 Stage",
                    tier_code = objODBCDatareader["tier1_code"].ToString(),
                    tiercode_changereason = lschangereason,
                    tier_approvedby = objODBCDatareader["zonal_riskmanagername"].ToString(),
                    tier_approveddate = objODBCDatareader["tier1_approveddate"].ToString(),
                    tier_approvalstatus = objODBCDatareader["tier1_approvalstatus"].ToString(),
                });
                values.tierReportdtl = get_tier3preparation;
            }
            objODBCDatareader.Close();

            msSQL = " select b.tier2_approval_status,date_format(b.tier2_approveddate, '%d-%m-%Y') as tier2_approveddate,headRMD_name, " +
                   " a.tier2_code,a.tier2code_changereason " +
                   " from rsk_trn_ttierallocationdtl a " +
                   " left join rsk_trn_ttier2preparation b on a.tier2preparation_gid = b.tier2preparation_gid " +
                   " where allocationdtl_gid='" + allocationdtl_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                if (objODBCDatareader["tier2code_changereason"].ToString() == "")
                {
                    lschangereason = "No Change";
                }
                else
                {
                    lschangereason = objODBCDatareader["tier2code_changereason"].ToString();
                }
                get_tier3preparation.Add(new tierReportdtl
                {
                    tier_stage = "At Tier 2 Stage",
                    tier_code = objODBCDatareader["tier2_code"].ToString(),
                    tiercode_changereason = lschangereason,
                    tier_approvalstatus = objODBCDatareader["tier2_approval_status"].ToString(),
                    tier_approvedby = objODBCDatareader["headRMD_name"].ToString(),
                    tier_approveddate = objODBCDatareader["tier2_approveddate"].ToString(),
                });
                values.tierReportdtl = get_tier3preparation;
            }
            objODBCDatareader.Close();

            msSQL = " select c.tier3_status,c.completed_by,tier3_code,tier3code_changereason, " +
                   " date_format(completed_date, '%d-%m-%Y') as completed_date,completed_by " +
                   " from rsk_trn_ttierallocationdtl a " +
                   " left join rsk_trn_ttier3completion b on a.tier2preparation_gid = b.tier2preparation_gid " +
                   " left join rsk_trn_ttier3preparation c on b.tier3preparation_gid = c.tier3preparation_gid" +
                   " where allocationdtl_gid='" + allocationdtl_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                if (objODBCDatareader["tier3_code"].ToString() != "")
                {
                    if (objODBCDatareader["tier3code_changereason"].ToString() == "")
                    {
                        lschangereason = "No Change";
                    }
                    else
                    {
                        lschangereason = objODBCDatareader["tier3code_changereason"].ToString();
                    }
                    get_tier3preparation.Add(new tierReportdtl
                    {
                        tier_stage = "At Tier 3 Stage",
                        tier_code = objODBCDatareader["tier3_code"].ToString(),
                        tiercode_changereason = lschangereason,
                        tier_approvalstatus = objODBCDatareader["tier3_status"].ToString(),
                        tier_approvedby = objODBCDatareader["completed_by"].ToString(),
                        tier_approveddate = objODBCDatareader["completed_date"].ToString(),
                    });
                    values.tierReportdtl = get_tier3preparation;
                }
            }
            objODBCDatareader.Close();
            return true;
        }

        public bool DaGetTier1Format360Dtl(string tier1format_gid, tier1format values)
        {

            msSQL = " select tier1format_gid,observation_reportgid,customer_name,customer_urn,tier1_observations, " +
                   " tier1_code,tier1_justification,tier1_managementcomments,tier1_approvalstatus, " +
                   " tier1_processgap,tier1_processrecommendation,tier1_reverts_actionplan,tier1code_changeflag, " +
                   " date_format(tier1_atrdate, '%d-%m-%Y') as tier1_atrdate,tier1code_changereason, " +
                   " date_format(created_date, '%d-%m-%Y') as created_date " +
                   " from rsk_trn_ttier1format where tier1format_gid = '" + tier1format_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.customer_name = objODBCDatareader["customer_name"].ToString();
                values.customer_urn = objODBCDatareader["customer_urn"].ToString();
                values.tier1format_gid = objODBCDatareader["tier1format_gid"].ToString();
                values.observation_reportgid = objODBCDatareader["observation_reportgid"].ToString();
                values.tier1_observations = objODBCDatareader["tier1_observations"].ToString();
                values.tier1_code = objODBCDatareader["tier1_code"].ToString();
                values.tier1_justification = objODBCDatareader["tier1_justification"].ToString();
                values.tier1_processgap = objODBCDatareader["tier1_processgap"].ToString();
                values.tier1_processrecommendation = objODBCDatareader["tier1_processrecommendation"].ToString();
                values.tier1_reverts_actionplan = objODBCDatareader["tier1_reverts_actionplan"].ToString();
                values.tier1_atrdate = objODBCDatareader["tier1_atrdate"].ToString();
                values.created_date = objODBCDatareader["created_date"].ToString();
                values.tier1_managementcomments = objODBCDatareader["tier1_managementcomments"].ToString();
                values.tier1_approvalstatus = objODBCDatareader["tier1_approvalstatus"].ToString();
                values.tier1code_changereason = objODBCDatareader["tier1code_changereason"].ToString();
                values.tier1code_changeflag = objODBCDatareader["tier1code_changeflag"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " select approval_status,approval_remarks,concat(b.user_firstname, ' ' ,b.user_lastname,' / ',b.user_code) as created_by, " +
                    " date_format(a.created_date, '%d-%m-%Y') as created_date from rsk_trn_ttier1approvallog a " +
                    " left join adm_mst_tuser b on a.created_by=b.user_gid " +
                    " where tier1format_gid='" + tier1format_gid + "' order by tier1approval_loggid asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_tier1approvallog = new List<tier1approvallog>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    get_tier1approvallog.Add(new tier1approvallog
                    {
                        approval_status = dt["approval_status"].ToString(),
                        approval_remarks = dt["approval_remarks"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                    });
                }
                values.tier1approvallog = get_tier1approvallog;
            }
            dt_datatable.Dispose();

            msSQL = " select reject_remarks,concat(b.user_firstname, ' ' ,b.user_lastname,' / ',b.user_code) as created_by, " +
                " date_format(a.created_date, '%d-%m-%Y') as created_date from rsk_trn_ttier1RMRejectlog a " +
                " left join adm_mst_tuser b on a.created_by=b.user_gid " +
                " where tier1format_gid='" + values.tier1format_gid + "' order by tier1rmreject_loggid asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_tier1rejectlog = new List<tier1rejectlog>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    get_tier1rejectlog.Add(new tier1rejectlog
                    {
                        reject_remarks = dt["reject_remarks"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                    });
                }
                values.tier1rejectlog = get_tier1rejectlog;
            }
            dt_datatable.Dispose();

            msSQL = " select tier1document_gid,document_title,document_name,document_path,date_format(a.created_date, '%d-%m-%Y') as created_date, " +
                  " concat(b.user_firstname, ' ', b.user_lastname, '/', b.user_code) as created_by from rsk_trn_ttier1document a " +
                  " left join adm_mst_tuser b on a.created_by = b.user_gid " +
                  " where tier1format_gid='" + values.tier1format_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_tier1document = new List<tier1doc>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    get_tier1document.Add(new tier1doc
                    {
                        tier1document_gid = dt["tier1document_gid"].ToString(),
                        document_name = dt["document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData((dt["document_path"].ToString())),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        document_title = dt["document_title"].ToString(),
                    });
                }
                values.tier1doc = get_tier1document;
            }
            dt_datatable.Dispose();

            return true;
        }

        public bool DaGetTier2Report360Dtl(string allocationdtl_gid, tier2Reportviewdtl values)
        {
            msSQL = " select a.tier2preparation_gid,a.zonal_name,a.vertical,a.tier2_month, " +
                    " monthname(str_to_date(tier2_month, '%m')) as tier2_monthname ," +
                    " concat(c.user_firstname,' ',c.user_lastname,'/',c.user_code) as created_by, " +
                    " date_format(a.created_date, '%d-%m-%Y') as tier2_submissiondate,tier2_approval_status, " +
                    " tier2_remarks,headRMD_name,date_format(tier2_approveddate, '%d-%m-%Y') as tier2_approveddate " +
                    " from rsk_trn_ttier2preparation a " +
                    " left join rsk_trn_ttierallocationdtl b on a.tier2preparation_gid = b.tier2preparation_gid " +
                    " left join adm_mst_tuser c on a.created_by=c.user_gid " +
                    " where b.allocationdtl_gid = '" + allocationdtl_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.tier2preparation_gid = objODBCDatareader["tier2preparation_gid"].ToString();
                values.zonal_name = objODBCDatareader["zonal_name"].ToString();
                values.tier2_month = objODBCDatareader["tier2_month"].ToString();
                values.vertical = objODBCDatareader["vertical"].ToString();
                values.headRMD_name = objODBCDatareader["headRMD_name"].ToString();
                values.tier2_remarks = objODBCDatareader["tier2_remarks"].ToString();
                values.tier2_approval_status = objODBCDatareader["tier2_approval_status"].ToString();
                values.created_date = objODBCDatareader["tier2_submissiondate"].ToString();
                values.created_by = objODBCDatareader["created_by"].ToString();
                values.tier2_approveddate = objODBCDatareader["tier2_approveddate"].ToString();
                values.tier2_monthname = objODBCDatareader["tier2_monthname"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " select tier2document_gid,document_title,document_name,document_path,date_format(a.created_date, '%d-%m-%Y') as created_date, " +
                    " concat(b.user_firstname, ' ', b.user_lastname, '/', b.user_code) as created_by from rsk_trn_ttier2document a " +
                    " left join adm_mst_tuser b on a.created_by = b.user_gid " +
                    " where tier2preparation_gid='" + values.tier2preparation_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_tier2document = new List<tier2document>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    get_tier2document.Add(new tier2document
                    {
                        tier2document_gid = dt["tier2document_gid"].ToString(),
                        document_name = dt["document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData((dt["document_path"].ToString())),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        document_title = dt["document_title"].ToString(),
                    });
                }
                values.tier2document = get_tier2document;
            }
            dt_datatable.Dispose();

            msSQL = " select approval_status,approval_remarks,concat(b.user_firstname, ' ' ,b.user_lastname,' / ',b.user_code) as approved_by, " +
                   " date_format(a.created_date, '%d-%m-%Y') as approved_date from rsk_trn_ttier2approvallog a " +
                   " left join adm_mst_tuser b on a.created_by=b.user_gid " +
                   " where tier2preparation_gid='" + values.tier2preparation_gid + "' order by tier2approval_loggid asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_tier2approvallog = new List<tier2approvallog>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    get_tier2approvallog.Add(new tier2approvallog
                    {
                        approval_status = dt["approval_status"].ToString(),
                        approval_remarks = dt["approval_remarks"].ToString(),
                        approved_by = dt["approved_by"].ToString(),
                        approved_date = dt["approved_date"].ToString(),
                    });
                }
                values.tier2approvallog = get_tier2approvallog;
            }
            dt_datatable.Dispose();
            return true;
        }

        public bool DaGetTier3Report360Dtl(string allocationdtl_gid, tier3viewdtl values)
        {
           msSQL= " select a.tier3preparation_gid,date_format(a.MLRC_date, '%d-%m-%Y') as MLRC_date,  " +
                  " monthname(str_to_date(tier3_month, '%m')) as tier3_monthname ,a.completed_flag, " +
			      " vertical, follow_up,tier3_status,  date_format(a.completed_date, '%d-%m-%Y') as completed_date, " +
                  " completed_remarks, date_format(a.created_date, '%d-%m-%Y') as created_date, " +
                  " concat(c.user_firstname, ' ', c.user_lastname, '/', c.user_code) as completed_by, " +
                  " concat(b.user_firstname, ' ', b.user_lastname, '/', b.user_code) as created_by " +
                  " from rsk_trn_ttier3preparation a " +
                  " left join adm_mst_tuser b on a.created_by = b.user_gid " +
                  " left join adm_mst_tuser c on a.completed_by = c.user_gid " +
                  " left join rsk_trn_ttier3completion d on a.tier3preparation_gid = d.tier3preparation_gid " +
                  " left join rsk_trn_ttierallocationdtl e on e.tier2preparation_gid = d.tier2preparation_gid " +
                  " where allocationdtl_gid = '" + allocationdtl_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.tier3preparation_gid = objODBCDatareader["tier3preparation_gid"].ToString();
                values.MLRC_date = objODBCDatareader["MLRC_date"].ToString();
                values.tier3_month = objODBCDatareader["tier3_monthname"].ToString();
                values.vertical = objODBCDatareader["vertical"].ToString();
                values.follow_up = objODBCDatareader["follow_up"].ToString();
                values.tier3_status = objODBCDatareader["tier3_status"].ToString();
                values.created_date = objODBCDatareader["created_date"].ToString();
                values.created_by = objODBCDatareader["created_by"].ToString();
                values.completed_flag = objODBCDatareader["completed_flag"].ToString();
                values.completed_date = objODBCDatareader["completed_date"].ToString();
                values.completed_by = objODBCDatareader["completed_by"].ToString();
                values.completed_remarks = objODBCDatareader["completed_remarks"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " select tier3document_gid,document_title,document_name,document_path,date_format(a.created_date, '%d-%m-%Y') as created_date, " +
                    " concat(b.user_firstname, ' ', b.user_lastname, '/', b.user_code) as created_by from rsk_trn_ttier3document a " +
                    " left join adm_mst_tuser b on a.created_by = b.user_gid " +
                    " where tier3preparation_gid='" + values.tier3preparation_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_tier3document = new List<tier3document>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    get_tier3document.Add(new tier3document
                    {
                        tier3document_gid = dt["tier3document_gid"].ToString(),
                        document_name = dt["document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData((dt["document_path"].ToString())),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        document_title = dt["document_title"].ToString(),
                    });
                }
                values.tier3document = get_tier3document;
            }
            dt_datatable.Dispose();

            msSQL = " select a.tierallocation_gid,zonal_name,vertical,date_format(tier2_approveddate, '%d-%m-%Y') as tier2_approveddate,tier3_code,allocationdtl_gid, " +
                    " concat(b.user_firstname, ' ', b.user_lastname, ' / ', b.user_code) as ZRM_name,customer_name,customer_urn, " +
                    " riskmanager from rsk_trn_ttierallocationdtl a " +
                    " left join adm_mst_tuser b on a.created_by = b.user_gid " +
                    " left join rsk_trn_ttier2preparation c on a.tier2preparation_gid = c.tier2preparation_gid " +
                    " left join rsk_trn_ttier3completion d on a.tier2preparation_gid = d.tier2preparation_gid " +
                    " where d.tier3preparation_gid = '" + values.tier3preparation_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var gettierallocationdtl = new List<tierallocationdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    gettierallocationdtl.Add(new tierallocationdtl
                    {
                        vertical = (dr_datarow["vertical"].ToString()),
                        zonal_name = (dr_datarow["zonal_name"].ToString()),
                        tier2_approveddate = (dr_datarow["tier2_approveddate"].ToString()),
                        ZRM_name = dr_datarow["ZRM_name"].ToString(),
                        customer_name = (dr_datarow["customer_name"].ToString()),
                        customer_urn = (dr_datarow["customer_urn"].ToString()),
                        riskmanager = dr_datarow["riskmanager"].ToString(),
                        tier3_code = dr_datarow["tier3_code"].ToString(),
                        allocationdtl_gid = dr_datarow["allocationdtl_gid"].ToString(),
                        tierallocation_gid = dr_datarow["tierallocation_gid"].ToString(),
                    });
                }
                values.tierallocationdtl = gettierallocationdtl;
            }
            dt_datatable.Dispose();
            return true;
        }

        public bool DaPostTier1Upload(HttpRequest httpRequest, tier1documentlist objfilename, string user_gid)
        {
            upload_list objdocumentmodel = new upload_list();
            HttpFileCollection httpFileCollection;

            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            //MemoryStream ms = new MemoryStream();
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string pdfFilName = string.Empty;
            Stream ls_readStream;
            string lsdocumenttype_gid = string.Empty;
            string lsdocument_title = httpRequest.Form["document_title"].ToString();
            string project_flag = httpRequest.Form["project_flag"].ToString();
            String path = lspath;

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);

            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "RSK/Tier1Document/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
            {
                if ((!System.IO.Directory.Exists(path)))
                    System.IO.Directory.CreateDirectory(path);
            }
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
                        ls_readStream = httpPostedFile.InputStream;
                        MemoryStream ms = new MemoryStream();
                        ls_readStream.CopyTo(ms);

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.message = "File format is not supported";
                            return false;
                        }

                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "RSK/Tier1Document/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "RSK/Tier1Document/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension;
                         
                        msSQL = " insert into rsk_tmp_ttier1document( " +
                                    " document_title," +
                                    " document_name ," +
                                    " document_path," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + lsdocument_title + "'," +
                                    "'" + httpPostedFile.FileName + "'," +
                                    "'" + lspath + "'," +
                                    "'" + user_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult == 1)
                        {
                            objfilename.status = true;
                        }
                        else
                        {
                            objfilename.status = false;
                        }
                    }
                }
                msSQL = "select tmptier1document_gid,document_title,document_name,document_path,created_date " +
                        " from rsk_tmp_ttier1document where created_by='" + user_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_filename = new List<tier1document>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_filename.Add(new tier1document
                        {
                            tmp_documentGid = (dr_datarow["tmptier1document_gid"].ToString()),
                            document_name = dr_datarow["document_name"].ToString(),
                            document_path = objcmnstorage.EncryptData((dr_datarow["document_path"].ToString())),
                            created_date = dr_datarow["created_date"].ToString(),
                            document_title = dr_datarow["document_title"].ToString(),
                        });
                    }
                    objfilename.tier1document = get_filename;
                }
                dt_datatable.Dispose();
            }
            catch
            {

            }
            return true;
        }

        public bool DaGetTier1UploadCancel(string tmp_documentGid, uploaddocument objfilename, string user_gid)
        {
            msSQL = "delete from rsk_tmp_ttier1document where tmptier1document_gid='" + tmp_documentGid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "select tmptier1document_gid,document_title,document_name,document_path,created_date " +
                       " from rsk_tmp_ttier1document where created_by='" + user_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_filename = new List<upload_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    get_filename.Add(new upload_list
                    {
                        document_title = (dr_datarow["document_title"].ToString()),
                        tmp_documentGid = (dr_datarow["tmptier1document_gid"].ToString()),
                        document_name = dr_datarow["document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData((dr_datarow["document_path"].ToString())),
                        created_date = dr_datarow["created_date"].ToString(),
                    });
                }
                objfilename.upload_list = get_filename;
            }
            dt_datatable.Dispose();

            if (mnResult != 0)
            {
                objfilename.status = true;
                objfilename.message = "Document Deleted Successfully..!";
                return true;
            }
            else
            {
                objfilename.status = false;
                objfilename.message = "Error Occured..!";
                return false;
            }

        }
    }
}