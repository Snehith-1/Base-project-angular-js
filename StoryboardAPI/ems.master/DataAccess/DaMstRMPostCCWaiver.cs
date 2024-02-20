﻿using ems.master.Models;
using ems.utilities.Functions;
using System;
using System.Web;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Data.Odbc;
using System.Configuration;
//using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using System.Globalization;
using OfficeOpenXml;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Net;
using System.Linq;
using ems.storage.Functions;

namespace ems.master.DataAccess
{
    public class DaMstRMPostCCWaiver
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        OdbcDataReader objODBCDatareader, objODBCDatareader1;
        HttpPostedFile httpPostedFile;
        DataTable dt_datatable;
        string msSQL, msGetGid, lspath;
        string gssanctionwaiver_gid, gssanctionwaiver_name, gslanwaiver_gid, gslanwaiver_name, gsgroupwaiver_gid, gsgroupwaiver_name, gslan_list;
        int mnResult;
        string lsrmpostccwaiver_gid, lswaiver_category, lsapplication_gid, lssanction_refno, lslan, lsurn, lssanctionwaiver_gid, lssanctionwaiver_name,
                              lslanwaiver_gid, lslanwaiver_name, lscustomer_info, lswaiver_title, lswaiver_description, lswaiver_amount,
                              lswaivergroup_gid, lswaivergroup_name, lsapproval_type, lsapproval_remarks;
        string gidHolder, lshierarchy_level;

        public void DaGetApplicationWaiverDetail(string application_gid, MdlApplicationWaiverDetail values)
        {
            //Sanction Ref No
            msSQL = " SELECT sanction_refno" +
                    " FROM ocs_trn_tapplication2sanction a  where application_gid='" + application_gid + "'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getsanctionrefno_list = new List<string>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getsanctionrefno_list.Add(dr_datarow["sanction_refno"].ToString());
                }
                values.sanctionrefno_list = getsanctionrefno_list;
            }
            dt_datatable.Dispose();

            //URN
            var geturn_list = new List<string>();

            msSQL = " SELECT urn" +
                    " FROM ocs_mst_tinstitution a  where application_gid='" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    if (dr_datarow["urn"].ToString() != "")
                    {
                        geturn_list.Add(dr_datarow["urn"].ToString());
                    }
                }
            }
            dt_datatable.Dispose();

            msSQL = " SELECT urn" +
                    " FROM ocs_mst_tcontact a  where application_gid='" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    if (dr_datarow["urn"].ToString() != "")
                    {
                        geturn_list.Add(dr_datarow["urn"].ToString());
                    }
                }
            }
            dt_datatable.Dispose();

            msSQL = " SELECT group_urn" +
                    " FROM ocs_mst_tgroup a  where application_gid='" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    if (dr_datarow["group_urn"].ToString() != "")
                    {
                        geturn_list.Add(dr_datarow["group_urn"].ToString());
                    }
                }
            }
            dt_datatable.Dispose();

            values.urn_list = geturn_list;
        }

        public void DaGetCustomerAndLANFromURN(string urn, MdlApplicationWaiverDetail values)
        {
            msSQL = " select company_name" +
                    " from ocs_mst_tinstitution a" +
                    " where urn='" + urn + "'";


            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.customer_info = objODBCDatareader["company_name"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " select concat(first_name,' ',middle_name,' ',last_name) as individual_name" +
                   " from ocs_mst_tcontact a" +
                   " where urn='" + urn + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.customer_info = objODBCDatareader["individual_name"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " select group_name" +
                   " from ocs_mst_tgroup a" +
                   " where group_urn='" + urn + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.customer_info = objODBCDatareader["group_name"].ToString();
            }
            objODBCDatareader.Close();

            //LAN
            msSQL = " SELECT account_no" +
                    " FROM lgl_tmp_tmisdata where urn='" + urn + "' and ac_status='0'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getlan_list = new List<string>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getlan_list.Add(dr_datarow["account_no"].ToString());
                }
                values.lan_list = getlan_list;
            }
            dt_datatable.Dispose();

        }

        public void DaGetApplicationWaiverMaster(MdlApplicationWaiverMaster values)
        {
            //Sanction Waiver
            msSQL = " SELECT sanctionwaiver_gid,sanctionwaiver_name" +
                    " FROM ocs_mst_tsanctionwaiver where status='Y' order by sanctionwaiver_gid desc ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getsanctionwaiver_list = new List<sanctionwaiver_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getsanctionwaiver_list.Add(new sanctionwaiver_list
                    {
                        sanctionwaiver_gid = (dr_datarow["sanctionwaiver_gid"].ToString()),
                        sanctionwaiver_name = (dr_datarow["sanctionwaiver_name"].ToString()),
                    });
                }
                values.sanctionwaiver_list = getsanctionwaiver_list;
            }
            dt_datatable.Dispose();

            //LAN Waiver
            msSQL = " SELECT lanwaiver_gid,lanwaiver_name" +
                        " FROM ocs_mst_tlanwaiver  where status='Y' order by lanwaiver_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getlanwaiver_list = new List<lanwaiver_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getlanwaiver_list.Add(new lanwaiver_list
                    {
                        lanwaiver_gid = (dr_datarow["lanwaiver_gid"].ToString()),
                        lanwaiver_name = (dr_datarow["lanwaiver_name"].ToString()),
                    });
                }
                values.lanwaiver_list = getlanwaiver_list;
            }
            dt_datatable.Dispose();
            //Waiver Group
            msSQL = " SELECT groupwaiver_gid,groupwaiver_name" +
                    " from ocs_mst_tgroupwaiver where status='Y' order by groupwaiver_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getwaivergroup_list = new List<waivergroup_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getwaivergroup_list.Add(new waivergroup_list
                    {
                        groupwaiver_gid = (dr_datarow["groupwaiver_gid"].ToString()),
                        groupwaiver_name = (dr_datarow["groupwaiver_name"].ToString()),
                    });
                }
                values.waivergroup_list = getwaivergroup_list;
            }
            dt_datatable.Dispose();

        }

        public bool DaWaiverDocumentUpload(HttpRequest httpRequest, uploaddocument objfilename, string employee_gid)
        {
            upload_list objdocumentmodel = new upload_list();
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string lsdocument_title = httpRequest.Form["document_title"].ToString();
            string project_flag = httpRequest.Form["project_flag"].ToString();
            String path = lspath;

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/WaiverDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
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
                        // Check Document validation;

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.message = "File format is not supported";
                            return false;
                        }

                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Master/WaiverDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "Master/WaiverDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        msGetGid = objcmnfunctions.GetMasterGID("W2DO");

                        msSQL = " insert into ocs_mst_trmpostccwaiver2document( " +
                                    " rmpostccwaiver2document_gid ," +
                                    " rmpostccwaiver_gid ," +
                                    " document_title ," +
                                    " document_name ," +
                                    " document_path," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + employee_gid + "'," +
                                  "'" + lsdocument_title.Replace("'", "") + "'," +
                                  "'" + httpPostedFile.FileName.Replace("'", " ") + "'," +
                                  "'" + lspath + msdocument_gid + FileExtension.Replace("'", " ") + "'," +
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
                    }
                }
            }
            catch (Exception ex)
            {
                objfilename.message = ex.ToString();
            }
            return true;
        }

        public void DaGetWaiverDocList(string employee_gid, MdlWaiverDocument values)
        {
            msSQL = " select rmpostccwaiver2document_gid,document_name,document_path,document_title from ocs_mst_trmpostccwaiver2document " +
                                 " where rmpostccwaiver_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdocumentdtlList = new List<uploadwaiverdoc_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getdocumentdtlList.Add(new uploadwaiverdoc_list
                    {
                        document_name = dt["document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                        rmpostccwaiver2document_gid = dt["rmpostccwaiver2document_gid"].ToString(),
                        document_title = dt["document_title"].ToString(),
                    });
                    values.uploadwaiverdoc_list = getdocumentdtlList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaWaiverDocDelete(string rmpostccwaiver2document_gid, MdlWaiverDocument values)
        {
            msSQL = "delete from ocs_mst_trmpostccwaiver2document where rmpostccwaiver2document_gid='" + rmpostccwaiver2document_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.message = "Document Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }

        public bool DaPostApprovalMember(string employee_gid, MdlWaiverApprovalMember values)
        {
            msSQL = "select rmpostccwaiver2approvalmember_gid from ocs_mst_trmpostccwaiver2approvalmember where approvalmember_gid='" + values.approvalmember_gid + "' " +
                " and rmpostccwaiver_gid='" + employee_gid + "' or rmpostccwaiver_gid='" + values.rmpostccwaiver_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Already This Approval Member Added";
                return false;
            }
            objODBCDatareader.Close();

            if (values.approval_type == "Sequence")
            {
                msSQL = "select rmpostccwaiver2approvalmember_gid, hierarchy_level from ocs_mst_trmpostccwaiver2approvalmember where rmpostccwaiver_gid='" + employee_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);

                var gethierarchylevel_list = new List<int>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        gethierarchylevel_list.Add(Convert.ToInt16(dr_datarow["hierarchy_level"].ToString()));
                    }
                    values.hierarchy_level = (gethierarchylevel_list.Max() + 1).ToString();
                }
                else
                {
                    values.hierarchy_level = "1";
                }
                dt_datatable.Dispose();
            }
            else
            {
                values.hierarchy_level = "NA";
            }


            values.sequence_flag = values.approval_type == "Sequence" ? "Y" : "N";

            values.approval_token = ComputeApprovalToken();

            msGetGid = objcmnfunctions.GetMasterGID("W2AM");
            msSQL = " insert into ocs_mst_trmpostccwaiver2approvalmember(" +
                    " rmpostccwaiver2approvalmember_gid," +
                    " rmpostccwaiver_gid," +
                    " approval_type," +
                    " sequence_flag," +
                    " approvalmember_gid," +
                    " approvalmember_name," +
                    " hierarchy_level," +
                    " approval_token," +
                    " assign_flag," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + values.approval_type + "'," +
                    "'" + values.sequence_flag + "'," +
                    "'" + values.approvalmember_gid + "'," +
                    "'" + values.approvalmember_name + "'," +
                    "'" + values.hierarchy_level + "'," +
                    "'" + values.approval_token + "',";

            msSQL += (values.hierarchy_level == "1" || values.hierarchy_level == "NA") ? "'Y'," : "'N',";

            msSQL += "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Approval Member Added Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
                return false;
            }
        }

        public void DaGetApprovalMemberList(string employee_gid, MdlWaiverApprovalMember values)
        {
            msSQL = "select rmpostccwaiver2approvalmember_gid,approvalmember_name from ocs_mst_trmpostccwaiver2approvalmember where " +
              " rmpostccwaiver_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getwaiverapprovalmember_list = new List<waiverapprovalmember_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getwaiverapprovalmember_list.Add(new waiverapprovalmember_list
                    {
                        rmpostccwaiver2approvalmember_gid = (dr_datarow["rmpostccwaiver2approvalmember_gid"].ToString()),
                        approvalmember_name = (dr_datarow["approvalmember_name"].ToString()),
                    });
                }
            }
            values.waiverapprovalmember_list = getwaiverapprovalmember_list;
            dt_datatable.Dispose();
        }

        public void DaApprovalMemberDelete(string rmpostccwaiver2approvalmember_gid, MdlWaiverApprovalMember values)
        {
            msSQL = "delete from ocs_mst_trmpostccwaiver2approvalmember where rmpostccwaiver2approvalmember_gid='" + rmpostccwaiver2approvalmember_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "Approval Member Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;
            }
        }

        public bool DaSubmitRMPostCCWaiver(MdlRMPostCCWaiver values, string employee_gid)
        {
            msSQL = "select rmpostccwaiver2document_gid" + " from ocs_mst_trmpostccwaiver2document where rmpostccwaiver_gid='" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Atleast One Document";
                return false;
            }
            objODBCDatareader.Close();

            msSQL = "select rmpostccwaiver2approvalmember_gid" + " from ocs_mst_trmpostccwaiver2approvalmember where rmpostccwaiver_gid='" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Atleast One Approval Member";
                return false;
            }
            objODBCDatareader.Close();



            if (values.sanctionwaiver_list != null)
            {
                for (var i = 0; i < values.sanctionwaiver_list.Count; i++)
                {
                    gssanctionwaiver_gid += values.sanctionwaiver_list[i].sanctionwaiver_gid + ",";
                    gssanctionwaiver_name += values.sanctionwaiver_list[i].sanctionwaiver_name + ",";
                }
                gssanctionwaiver_gid = gssanctionwaiver_gid.TrimEnd(',');
                gssanctionwaiver_name = gssanctionwaiver_name.TrimEnd(',');
            }

            if (values.lanwaiver_list != null)
            {
                for (var i = 0; i < values.lanwaiver_list.Count; i++)
                {
                    gslanwaiver_gid += values.lanwaiver_list[i].lanwaiver_gid + ",";
                    gslanwaiver_name += values.lanwaiver_list[i].lanwaiver_name + ",";
                }
                gslanwaiver_gid = gslanwaiver_gid.TrimEnd(',');
                gslanwaiver_name = gslanwaiver_name.TrimEnd(',');
            }

            if (values.waivergroup_list != null)
            {
                for (var i = 0; i < values.waivergroup_list.Count; i++)
                {
                    gsgroupwaiver_gid += values.waivergroup_list[i].groupwaiver_gid + ",";
                    gsgroupwaiver_name += values.waivergroup_list[i].groupwaiver_name + ",";
                }
                gsgroupwaiver_gid = gsgroupwaiver_gid.TrimEnd(',');
                gsgroupwaiver_name = gsgroupwaiver_name.TrimEnd(',');
            }

            if (values.waiver_category == "LAN Waiver")
            {
                if (values.lan_list != null)
                {
                    for (var i = 0; i < values.lan_list.Count; i++)
                    {
                        gslan_list += values.lan_list[i] + ",";
                    }
                    gslan_list = gslan_list.TrimEnd(',');
                }
            }


            msGetGid = objcmnfunctions.GetMasterGID("RPCW");
            msSQL = " insert into ocs_mst_trmpostccwaiver(" +
                " rmpostccwaiver_gid," +
                " application_gid," +
                " waiver_category,";
            if (values.waiver_category == "Sanction Waiver")
            {
                msSQL += "sanction_refno,";
            }
            else
            {
                msSQL += "lan,";
            }

            msSQL += " urn," +
                   " sanctionwaiver_gid," +
                   " sanctionwaiver_name," +
                   " lanwaiver_gid," +
                   " lanwaiver_name," +
                   " customer_info," +
                   " waiver_title," +
                   " waiver_description," +
                   " waiver_amount," +
                   " waivergroup_gid," +
                   " waivergroup_name," +
                   " approval_type," +
                   " approval_remarks," +
                   " created_by," +
                   " created_date) values(" +
                   "'" + msGetGid + "'," +
                   "'" + values.application_gid + "'," +
                   "'" + values.waiver_category + "',";

            if (values.waiver_category == "Sanction Waiver")
            {
                msSQL += "'" + values.sanction_refno + "',";
            }
            else
            {
                msSQL += "'" + gslan_list + "',";
            }

            msSQL += "'" + values.urn + "'," +
                     "'" + gssanctionwaiver_gid + "'," +
                     "'" + gssanctionwaiver_name + "'," +
                     "'" + gslanwaiver_gid + "'," +
                     "'" + gslanwaiver_name + "'," +
                     "'" + values.customer_info + "'," +
                     "'" + values.waiver_title + "'," +
                     "'" + values.waiver_description + "'," +
                     "'" + values.waiver_amount + "'," +
                     "'" + gsgroupwaiver_gid + "'," +
                     "'" + gsgroupwaiver_name + "'," +
                     "'" + values.approval_type + "'," +
                     "'" + values.approval_remarks + "'," +
                     "'" + employee_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "update ocs_mst_trmpostccwaiver2document set rmpostccwaiver_gid='" + msGetGid + "' where rmpostccwaiver_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_trmpostccwaiver2approvalmember set rmpostccwaiver_gid='" + msGetGid + "' where rmpostccwaiver_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.message = "Waiver Information Submitted Successfully";
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

        public void DaGetSanctionWaiverSummary(string application_gid, MdlRMPostCCWaiver values)
        {
            msSQL = " select a.rmpostccwaiver_gid,a.sanction_refno,a.waiver_title," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,a.approval_type" +
                    " from ocs_mst_trmpostccwaiver a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid" +
                    " where application_gid='" + application_gid + "' and waiver_category='Sanction Waiver' order by rmpostccwaiver_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getrmpostccwaiver_list = new List<rmpostccwaiver_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getrmpostccwaiver_list.Add(new rmpostccwaiver_list
                    {
                        rmpostccwaiver_gid = dt["rmpostccwaiver_gid"].ToString(),
                        sanction_refno = dt["sanction_refno"].ToString(),
                        waiver_title = dt["waiver_title"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        approval_type = dt["approval_type"].ToString(),
                        approval_status = ComputeApprovalStatus(dt["rmpostccwaiver_gid"].ToString())
                    });

                }
            }
            values.rmpostccwaiver_list = getrmpostccwaiver_list;
            dt_datatable.Dispose();
        }

        public void DaGetWaiverTempClear(string employee_gid, result values)
        {
            msSQL = "delete from ocs_mst_trmpostccwaiver2document where rmpostccwaiver_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from ocs_mst_trmpostccwaiver2approvalmember where rmpostccwaiver_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            values.status = true;
        }

        public void DaEditRMPostCCWaiver(string rmpostccwaiver_gid, MdlRMPostCCWaiver values)
        {
            try
            {
                msSQL = " select waiver_category,sanction_refno,lan,urn,sanctionwaiver_gid,sanctionwaiver_name," +
                        " lanwaiver_gid,lanwaiver_name,customer_info,waiver_title,waiver_description," +
                        " waiver_amount,waivergroup_gid,waivergroup_name,approval_type,approval_remarks" +
                        " from ocs_mst_trmpostccwaiver where rmpostccwaiver_gid='" + rmpostccwaiver_gid + "'";


                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.waiver_category = objODBCDatareader["waiver_category"].ToString();
                    values.sanction_refno = objODBCDatareader["sanction_refno"].ToString();
                    values.lan = objODBCDatareader["lan"].ToString();
                    values.urn = objODBCDatareader["urn"].ToString();
                    values.sanctionwaiver_gid = objODBCDatareader["sanctionwaiver_gid"].ToString();
                    values.sanctionwaiver_name = objODBCDatareader["sanctionwaiver_name"].ToString();
                    values.lanwaiver_gid = objODBCDatareader["lanwaiver_gid"].ToString();
                    values.lanwaiver_name = objODBCDatareader["lanwaiver_name"].ToString();
                    values.customer_info = objODBCDatareader["customer_info"].ToString();
                    values.waiver_title = objODBCDatareader["waiver_title"].ToString();
                    values.waiver_description = objODBCDatareader["waiver_description"].ToString();
                    values.waiver_amount = objODBCDatareader["waiver_amount"].ToString();
                    values.groupwaiver_gid = objODBCDatareader["waivergroup_gid"].ToString();
                    values.groupwaiver_name = objODBCDatareader["waivergroup_name"].ToString();
                    values.approval_type = objODBCDatareader["approval_type"].ToString();
                    values.approval_remarks = objODBCDatareader["approval_remarks"].ToString();

                    String[] sancwaivgid_list = objODBCDatareader["sanctionwaiver_gid"].ToString().Split(',');
                    String[] sancwaivname_list = objODBCDatareader["sanctionwaiver_name"].ToString().Split(',');
                    var getsanctionwaiver_list = new List<sanctionwaiver_list>();
                    for (var i = 0; i < sancwaivgid_list.Length; i++)
                    {
                        getsanctionwaiver_list.Add(new sanctionwaiver_list
                        {
                            sanctionwaiver_gid = sancwaivgid_list[i],
                            sanctionwaiver_name = sancwaivname_list[i],
                        });
                    }
                    values.sanctionwaiver_list = getsanctionwaiver_list;

                    String[] lanwaivgid_list = objODBCDatareader["lanwaiver_gid"].ToString().Split(',');
                    String[] lanwaivname_list = objODBCDatareader["lanwaiver_name"].ToString().Split(',');
                    var getlanwaiver_list = new List<lanwaiver_list>();
                    for (var i = 0; i < lanwaivgid_list.Length; i++)
                    {
                        getlanwaiver_list.Add(new lanwaiver_list
                        {
                            lanwaiver_gid = lanwaivgid_list[i],
                            lanwaiver_name = lanwaivname_list[i],
                        });
                    }
                    values.lanwaiver_list = getlanwaiver_list;

                    String[] groupwaivgid_list = objODBCDatareader["waivergroup_gid"].ToString().Split(',');
                    String[] groupwaivname_list = objODBCDatareader["waivergroup_name"].ToString().Split(',');
                    var getwaivergroup_list = new List<waivergroup_list>();
                    for (var i = 0; i < groupwaivgid_list.Length; i++)
                    {
                        getwaivergroup_list.Add(new waivergroup_list
                        {
                            groupwaiver_gid = groupwaivgid_list[i],
                            groupwaiver_name = groupwaivname_list[i],
                        });
                    }
                    values.waivergroup_list = getwaivergroup_list;

                    String[] lanval_list = objODBCDatareader["lan"].ToString().Split(',');
                    var getlan_list = new List<string>();
                    for (var i = 0; i < lanval_list.Length; i++)
                    {
                        getlan_list.Add(lanval_list[i]);
                    }
                    values.lan_list = getlan_list;
                    //Sanction Waiver
                    msSQL = " SELECT sanctionwaiver_gid,sanctionwaiver_name FROM ocs_mst_tsanctionwaiver" +
                         " where status='Y' order by sanctionwaiver_gid desc ";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getsanctionwaivergen_list = new List<sanctionwaiver_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dr_datarow in dt_datatable.Rows)
                        {
                            getsanctionwaivergen_list.Add(new sanctionwaiver_list
                            {
                                sanctionwaiver_gid = (dr_datarow["sanctionwaiver_gid"].ToString()),
                                sanctionwaiver_name = (dr_datarow["sanctionwaiver_name"].ToString()),
                            });
                        }
                        values.sanctionwaivergen_list = getsanctionwaivergen_list;
                    }
                    dt_datatable.Dispose();
                    //LAN Waiver
                    msSQL = " SELECT lanwaiver_gid,lanwaiver_name FROM ocs_mst_tlanwaiver" +
                         " where status='Y' order by lanwaiver_gid desc ";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getlanwaivergen_list = new List<lanwaiver_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dr_datarow in dt_datatable.Rows)
                        {
                            getlanwaivergen_list.Add(new lanwaiver_list
                            {
                                lanwaiver_gid = (dr_datarow["lanwaiver_gid"].ToString()),
                                lanwaiver_name = (dr_datarow["lanwaiver_name"].ToString()),
                            });
                        }
                        values.lanwaivergen_list = getlanwaivergen_list;
                    }
                    dt_datatable.Dispose();
                    //Group Waiver
                    msSQL = " SELECT groupwaiver_gid,groupwaiver_name FROM ocs_mst_tgroupwaiver" +
                         " where status='Y' order by groupwaiver_gid desc ";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getwaivergroupgen_list = new List<waivergroup_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dr_datarow in dt_datatable.Rows)
                        {
                            getwaivergroupgen_list.Add(new waivergroup_list
                            {
                                groupwaiver_gid = (dr_datarow["groupwaiver_gid"].ToString()),
                                groupwaiver_name = (dr_datarow["groupwaiver_name"].ToString()),
                            });
                        }
                        values.waivergroupgen_list = getwaivergroupgen_list;
                    }
                    dt_datatable.Dispose();
                    //LAN
                    msSQL = " SELECT account_no" +
                            " FROM lgl_tmp_tmisdata where urn='" + values.urn + "' and ac_status='0'";

                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getlangen_list = new List<string>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dr_datarow in dt_datatable.Rows)
                        {
                            getlangen_list.Add(dr_datarow["account_no"].ToString());
                        }
                        values.langen_list = getlangen_list;
                    }
                    dt_datatable.Dispose();
                }


                objODBCDatareader.Close();
                values.status = true;
                values.message = "success";
            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }

        }

        public void DaWaiverDocList(string rmpostccwaiver_gid, MdlWaiverDocument values)
        {
            msSQL = " select rmpostccwaiver2document_gid,document_name,document_path,document_title from ocs_mst_trmpostccwaiver2document " +
                                 " where rmpostccwaiver_gid='" + rmpostccwaiver_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdocumentdtlList = new List<uploadwaiverdoc_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getdocumentdtlList.Add(new uploadwaiverdoc_list
                    {
                        document_name = dt["document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                        rmpostccwaiver2document_gid = dt["rmpostccwaiver2document_gid"].ToString(),
                        document_title = dt["document_title"].ToString(),
                    });
                    values.uploadwaiverdoc_list = getdocumentdtlList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaApprovalMemberList(string rmpostccwaiver_gid, MdlWaiverApprovalMember values)
        {
            msSQL = "select rmpostccwaiver2approvalmember_gid,approvalmember_name from ocs_mst_trmpostccwaiver2approvalmember where " +
              " rmpostccwaiver_gid='" + rmpostccwaiver_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getwaiverapprovalmember_list = new List<waiverapprovalmember_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getwaiverapprovalmember_list.Add(new waiverapprovalmember_list
                    {
                        rmpostccwaiver2approvalmember_gid = (dr_datarow["rmpostccwaiver2approvalmember_gid"].ToString()),
                        approvalmember_name = (dr_datarow["approvalmember_name"].ToString()),
                    });
                }
            }
            values.waiverapprovalmember_list = getwaiverapprovalmember_list;
            dt_datatable.Dispose();
        }

        public void DaWaiverDocTempList(string rmpostccwaiver_gid, string employee_gid, MdlWaiverDocument values)
        {
            msSQL = " select rmpostccwaiver2document_gid,document_name,document_path,document_title from ocs_mst_trmpostccwaiver2document " +
                                 " where rmpostccwaiver_gid='" + rmpostccwaiver_gid + "' or rmpostccwaiver_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdocumentdtlList = new List<uploadwaiverdoc_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getdocumentdtlList.Add(new uploadwaiverdoc_list
                    {
                        document_name = dt["document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                        rmpostccwaiver2document_gid = dt["rmpostccwaiver2document_gid"].ToString(),
                        document_title = dt["document_title"].ToString(),
                    });
                    values.uploadwaiverdoc_list = getdocumentdtlList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaApprovalMemberTempList(string rmpostccwaiver_gid, string employee_gid, MdlWaiverApprovalMember values)
        {
            msSQL = "select rmpostccwaiver2approvalmember_gid,approvalmember_name from ocs_mst_trmpostccwaiver2approvalmember where " +
              " rmpostccwaiver_gid='" + rmpostccwaiver_gid + "' or rmpostccwaiver_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getwaiverapprovalmember_list = new List<waiverapprovalmember_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getwaiverapprovalmember_list.Add(new waiverapprovalmember_list
                    {
                        rmpostccwaiver2approvalmember_gid = (dr_datarow["rmpostccwaiver2approvalmember_gid"].ToString()),
                        approvalmember_name = (dr_datarow["approvalmember_name"].ToString()),
                    });
                }
            }
            values.waiverapprovalmember_list = getwaiverapprovalmember_list;
            dt_datatable.Dispose();
        }

        public bool DaUpdateRMPostCCWaiver(string employee_gid, MdlRMPostCCWaiver values)
        {
            msSQL = "select rmpostccwaiver2document_gid" + " from ocs_mst_trmpostccwaiver2document where rmpostccwaiver_gid='" + employee_gid + "' or rmpostccwaiver_gid='" + values.rmpostccwaiver_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Atleast One Document";
                return false;
            }
            objODBCDatareader.Close();

            msSQL = "select rmpostccwaiver2approvalmember_gid" + " from ocs_mst_trmpostccwaiver2approvalmember where rmpostccwaiver_gid='" + employee_gid + "' or rmpostccwaiver_gid='" + values.rmpostccwaiver_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Atleast One Approval Member";
                return false;
            }
            objODBCDatareader.Close();

            if (values.sanctionwaiver_list != null)
            {
                for (var i = 0; i < values.sanctionwaiver_list.Count; i++)
                {
                    gssanctionwaiver_gid += values.sanctionwaiver_list[i].sanctionwaiver_gid + ",";
                    gssanctionwaiver_name += values.sanctionwaiver_list[i].sanctionwaiver_name + ",";
                }
                gssanctionwaiver_gid = gssanctionwaiver_gid.TrimEnd(',');
                gssanctionwaiver_name = gssanctionwaiver_name.TrimEnd(',');
            }

            if (values.lanwaiver_list != null)
            {
                for (var i = 0; i < values.lanwaiver_list.Count; i++)
                {
                    gslanwaiver_gid += values.lanwaiver_list[i].lanwaiver_gid + ",";
                    gslanwaiver_name += values.lanwaiver_list[i].lanwaiver_name + ",";
                }
                gslanwaiver_gid = gslanwaiver_gid.TrimEnd(',');
                gslanwaiver_name = gslanwaiver_name.TrimEnd(',');
            }

            if (values.waivergroup_list != null)
            {
                for (var i = 0; i < values.waivergroup_list.Count; i++)
                {
                    gsgroupwaiver_gid += values.waivergroup_list[i].groupwaiver_gid + ",";
                    gsgroupwaiver_name += values.waivergroup_list[i].groupwaiver_name + ",";
                }
                gsgroupwaiver_gid = gsgroupwaiver_gid.TrimEnd(',');
                gsgroupwaiver_name = gsgroupwaiver_name.TrimEnd(',');
            }

            if (values.waiver_category == "LAN Waiver")
            {
                if (values.lan_list != null)
                {
                    for (var i = 0; i < values.lan_list.Count; i++)
                    {
                        gslan_list += values.lan_list[i] + ",";
                    }
                    gslan_list = gslan_list.TrimEnd(',');
                }
            }


            msSQL = " select rmpostccwaiver_gid,application_gid,waiver_category,sanction_refno,lan,urn,sanctionwaiver_gid,sanctionwaiver_name," +
                             " lanwaiver_gid,lanwaiver_name,customer_info,waiver_title,waiver_description,waiver_amount," +
                             " waivergroup_gid,waivergroup_name,approval_type,approval_remarks" +
                             " from ocs_mst_trmpostccwaiver " +
                             " where rmpostccwaiver_gid='" + values.rmpostccwaiver_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsrmpostccwaiver_gid = objODBCDatareader["rmpostccwaiver_gid"].ToString();
                lsapplication_gid = objODBCDatareader["application_gid"].ToString();
                lswaiver_category = objODBCDatareader["waiver_category"].ToString();
                lssanction_refno = objODBCDatareader["sanction_refno"].ToString();
                lslan = objODBCDatareader["lan"].ToString();
                lsurn = objODBCDatareader["urn"].ToString();
                lssanctionwaiver_gid = objODBCDatareader["sanctionwaiver_gid"].ToString();
                lssanctionwaiver_name = objODBCDatareader["sanctionwaiver_name"].ToString();
                lslanwaiver_gid = objODBCDatareader["lanwaiver_gid"].ToString();
                lslanwaiver_name = objODBCDatareader["lanwaiver_name"].ToString();
                lscustomer_info = objODBCDatareader["customer_info"].ToString();
                lswaiver_title = objODBCDatareader["waiver_title"].ToString();
                lswaiver_description = objODBCDatareader["waiver_description"].ToString();
                lswaiver_amount = objODBCDatareader["waiver_amount"].ToString();
                lswaivergroup_gid = objODBCDatareader["waivergroup_gid"].ToString();
                lswaivergroup_name = objODBCDatareader["waivergroup_name"].ToString();
                lsapproval_type = objODBCDatareader["approval_type"].ToString();
                lsapproval_remarks = objODBCDatareader["approval_remarks"].ToString();
            }
            objODBCDatareader.Close();
            try
            {
                msSQL = " update ocs_mst_trmpostccwaiver set " +
                    " waiver_category='" + values.waiver_category + "'," +
                    " application_gid='" + values.application_gid + "',";

                if (values.waiver_category == "Sanction Waiver")
                {
                    msSQL += " sanction_refno='" + values.sanction_refno + "',";
                }
                else
                {
                    msSQL += " lan='" + gslan_list + "',";
                }
                msSQL += " urn='" + values.urn + "'," +
                             " sanctionwaiver_gid='" + gssanctionwaiver_gid + "'," +
                             " sanctionwaiver_name='" + gssanctionwaiver_name + "'," +
                             " lanwaiver_gid='" + gslanwaiver_gid + "'," +
                             " lanwaiver_name='" + gslanwaiver_name + "'," +
                             " customer_info='" + values.customer_info + "'," +
                             " waiver_title='" + values.waiver_title + "'," +
                             " waiver_description='" + values.waiver_description + "'," +
                             " waiver_amount='" + values.waiver_amount + "'," +
                             " waivergroup_gid='" + gsgroupwaiver_gid + "'," +
                             " waivergroup_name='" + gsgroupwaiver_name + "'," +
                             " approval_type='" + values.approval_type + "'," +
                             " approval_remarks='" + values.approval_remarks + "'," +
                             " updated_by='" + employee_gid + "'," +
                             " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                             " where rmpostccwaiver_gid='" + values.rmpostccwaiver_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("RWUL");

                    msSQL = " insert into ocs_mst_trmpostccwaiverupdatelog(" +
                            " rmpostccwaiverupdatelog_gid," +
                            " rmpostccwaiver_gid," +
                            " application_gid," +
                            " waiver_category,";
                    if (values.waiver_category == "Sanction Waiver")
                    {
                        msSQL += "sanction_refno,";
                    }
                    else
                    {
                        msSQL += "lan,";
                    }

                    msSQL += " urn," +
                           " sanctionwaiver_gid," +
                           " sanctionwaiver_name," +
                           " lanwaiver_gid," +
                           " lanwaiver_name," +
                           " customer_info," +
                           " waiver_title," +
                           " waiver_description," +
                           " waiver_amount," +
                           " waivergroup_gid," +
                           " waivergroup_name," +
                           " approval_type," +
                           " approval_remarks," +
                           " created_by," +
                           " created_date) values(" +
                           "'" + msGetGid + "'," +
                           "'" + lsrmpostccwaiver_gid + "'," +
                           "'" + lsapplication_gid + "'," +
                           "'" + lswaiver_category + "',";

                    if (values.waiver_category == "Sanction Waiver")
                    {
                        msSQL += "'" + lssanction_refno + "',";
                    }
                    else
                    {
                        msSQL += "'" + lslan + "',";
                    }

                    msSQL += "'" + lsurn + "'," +
                             "'" + lssanctionwaiver_gid + "'," +
                             "'" + lssanctionwaiver_name + "'," +
                             "'" + lslanwaiver_gid + "'," +
                             "'" + lslanwaiver_name + "'," +
                             "'" + lscustomer_info + "'," +
                             "'" + lswaiver_title + "'," +
                             "'" + lswaiver_description + "'," +
                             "'" + lswaiver_amount + "'," +
                             "'" + lswaivergroup_gid + "'," +
                             "'" + lswaivergroup_name + "'," +
                             "'" + lsapproval_type + "'," +
                             "'" + lsapproval_remarks + "'," +
                             "'" + employee_gid + "'," +
                             "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    //Updates

                    msSQL = "update ocs_mst_trmpostccwaiver2document set rmpostccwaiver_gid='" + values.rmpostccwaiver_gid + "' where rmpostccwaiver_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update ocs_mst_trmpostccwaiver2approvalmember set rmpostccwaiver_gid='" + values.rmpostccwaiver_gid + "' where rmpostccwaiver_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);



                    values.status = true;
                    values.message = "Waiver Information Updated Successfully";
                    return true;
                }
                return true;
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Error Occured..";
                return false;
            }
        }

        public void DaDeleteRMPostCCWaiver(string rmpostccwaiver_gid, MdlRMPostCCWaiver values)
        {
            msSQL = "delete from ocs_mst_trmpostccwaiver where rmpostccwaiver_gid='" + rmpostccwaiver_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.message = "Waiver Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;
            }
        }


        public void DaGetLANWaiverSummary(string application_gid, MdlRMPostCCWaiver values)
        {


            msSQL = " select a.rmpostccwaiver_gid,a.urn,a.lan,a.waiver_title," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,a.approval_type" +
                    " from ocs_mst_trmpostccwaiver a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid" +
                    " where application_gid='" + application_gid + "' and waiver_category='LAN Waiver' order by rmpostccwaiver_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getrmpostccwaiver_list = new List<rmpostccwaiver_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getrmpostccwaiver_list.Add(new rmpostccwaiver_list
                    {
                        rmpostccwaiver_gid = dt["rmpostccwaiver_gid"].ToString(),
                        urn = dt["urn"].ToString(),
                        lan = dt["lan"].ToString(),
                        waiver_title = dt["waiver_title"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        approval_type = dt["approval_type"].ToString(),
                        approval_status = ComputeApprovalStatus(dt["rmpostccwaiver_gid"].ToString())
                    });

                }
            }
            values.rmpostccwaiver_list = getrmpostccwaiver_list;
            dt_datatable.Dispose();
        }

        public void DaGetWaiverApprovalSummary(string employee_gid, MdlRMPostCCWaiver values)
        {
            msSQL = " select a.rmpostccwaiver_gid,a.application_gid,a.waiver_title,a.waiver_category,a.urn," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,a.approval_type,d.approval_status" +
                    " from ocs_mst_trmpostccwaiver a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid" +
                    " left join ocs_mst_trmpostccwaiver2approvalmember d on d.rmpostccwaiver_gid=a.rmpostccwaiver_gid " +
                    " where d.approvalmember_gid='" + employee_gid + "' and d.assign_flag='Y' and d.approval_status='Pending' order by a.rmpostccwaiver_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getrmpostccwaiver_list = new List<rmpostccwaiver_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getrmpostccwaiver_list.Add(new rmpostccwaiver_list
                    {
                        rmpostccwaiver_gid = dt["rmpostccwaiver_gid"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        waiver_title = dt["waiver_title"].ToString(),
                        waiver_category = dt["waiver_category"].ToString(),
                        urn = dt["urn"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        approval_type = dt["approval_type"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                    });

                }
            }
            values.rmpostccwaiver_list = getrmpostccwaiver_list;
            dt_datatable.Dispose();
        }

        public void DaWaiverApprovalDetailList(string rmpostccwaiver_gid, MdlWaiverApprovalDetail values)
        {

            msSQL = "select a.rmpostccwaiver2approvalmember_gid,a.approvalmember_name,a.approval_type,a.approval_status,a.approval_token," +
                " case when a.approval_status = 'Approved' then date_format(a.approved_date,'%d-%m-%Y %h:%i %p')" +
                " when a.approval_status = 'Rejected' then date_format(a.rejected_date,'%d-%m-%Y %h:%i %p') else '-' end as apprej_date," +
                " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as initiated_by," +
                " case when (approval_status = 'Approved' or approval_status = 'Rejected') then a.approval_remarks else '-' end as approval_remarks" +
                " from ocs_mst_trmpostccwaiver2approvalmember a" +
                " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                " left join adm_mst_tuser c on c.user_gid=b.user_gid" +
                " where rmpostccwaiver_gid='" + rmpostccwaiver_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getwaiverapprovaldetail_list = new List<waiverapprovaldetail_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getwaiverapprovaldetail_list.Add(new waiverapprovaldetail_list
                    {
                        rmpostccwaiver2approvalmember_gid = (dr_datarow["rmpostccwaiver2approvalmember_gid"].ToString()),
                        approvalmember_name = (dr_datarow["approvalmember_name"].ToString()),
                        approval_type = (dr_datarow["approval_type"].ToString()),
                        approval_status = (dr_datarow["approval_status"].ToString()),
                        approval_token = (dr_datarow["approval_token"].ToString()),
                        apprej_date = (dr_datarow["apprej_date"].ToString()),
                        initiated_by = (dr_datarow["initiated_by"].ToString()),
                        approval_remarks = (dr_datarow["approval_remarks"].ToString()),

                    });
                }
            }
            values.waiverapprovaldetail_list = getwaiverapprovaldetail_list;
            dt_datatable.Dispose();
        }


        public void DaPostWaiverApproved(string employee_gid, waiverapproval values)
        {
            msSQL = " update ocs_mst_trmpostccwaiver2approvalmember set approval_status='Approved', approval_flag='Y'," +
                    " approval_remarks='" + values.approval_remarks.Replace("'", "\\'") + "'," +
                    " approved_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    " where rmpostccwaiver_gid='" + values.rmpostccwaiver_gid + "' and approvalmember_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                msSQL = " select hierarchy_level from ocs_mst_trmpostccwaiver2approvalmember" +
                        " where rmpostccwaiver_gid='" + values.rmpostccwaiver_gid + "' and approvalmember_gid='" + employee_gid + "'";
                lshierarchy_level = objdbconn.GetExecuteScalar(msSQL);
                if (lshierarchy_level != "NA")
                {
                    int lihierarchy_level = Convert.ToInt16(lshierarchy_level) + 1;

                    msSQL = " update ocs_mst_trmpostccwaiver2approvalmember set assign_flag='Y'" +
                            " where rmpostccwaiver_gid='" + values.rmpostccwaiver_gid + "' and hierarchy_level='" + lihierarchy_level.ToString() + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                }

                values.status = true;
                values.message = "Waiver Approved Successfully..!";

            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
        }

        public void DaPostWaiverRejected(string employee_gid, waiverapproval values)
        {
            msSQL = " update ocs_mst_trmpostccwaiver2approvalmember set approval_status='Rejected'," +
                    " approval_remarks='" + values.approval_remarks.Replace("'", "\\'") + "'," +
                    " rejected_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    " where rmpostccwaiver_gid='" + values.rmpostccwaiver_gid + "' and approvalmember_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {     
                values.status = true;
                values.message = "Waiver Rejected Successfully..!";

            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
        }

        public void DaGetWaiverApprovalHistorySummary(string employee_gid, MdlRMPostCCWaiver values)
        {


            msSQL = " select a.rmpostccwaiver_gid,a.application_gid,a.waiver_title,a.waiver_category,a.urn," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,a.approval_type,d.approval_status" +
                    " from ocs_mst_trmpostccwaiver a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid" +
                    " left join ocs_mst_trmpostccwaiver2approvalmember d on d.rmpostccwaiver_gid=a.rmpostccwaiver_gid " +
                    " where d.approvalmember_gid='" + employee_gid + "' and d.assign_flag='Y' and (d.approval_status='Approved' or d.approval_status='Rejected') order by a.rmpostccwaiver_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getrmpostccwaiver_list = new List<rmpostccwaiver_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getrmpostccwaiver_list.Add(new rmpostccwaiver_list
                    {
                        rmpostccwaiver_gid = dt["rmpostccwaiver_gid"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        waiver_title = dt["waiver_title"].ToString(),
                        waiver_category = dt["waiver_category"].ToString(),
                        urn = dt["urn"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        approval_type = dt["approval_type"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                    });

                }
            }
            values.rmpostccwaiver_list = getrmpostccwaiver_list;
            dt_datatable.Dispose();
        }

        public void DaWaiverApprovalHistoryList(MdlRMPostCCWaiver values)
        {
            msSQL = " select a.rmpostccwaiver_gid,a.waiver_category," +
                    " case when a.waiver_category = 'Sanction Waiver' then a.sanction_refno else '-' end as sanction_refno," +
                    " case when a.waiver_category = 'LAN Waiver' then a.lan else '-' end as lan," +
                    " a.urn,a.customer_info,a.waiver_title,a.waiver_amount,a.waivergroup_name" +
                    " from ocs_mst_trmpostccwaiver a" +
                    " where a.application_gid='" + values.application_gid + "' and a.rmpostccwaiver_gid!='" + values.rmpostccwaiver_gid + "' order by a.rmpostccwaiver_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getrmpostccwaiver_list = new List<rmpostccwaiver_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getrmpostccwaiver_list.Add(new rmpostccwaiver_list
                    {
                        rmpostccwaiver_gid = dt["rmpostccwaiver_gid"].ToString(),
                        waiver_category = dt["waiver_category"].ToString(),
                        sanction_refno = dt["sanction_refno"].ToString(),
                        lan = dt["lan"].ToString(),
                        urn = dt["urn"].ToString(),
                        customer_info = dt["customer_info"].ToString(),
                        waiver_title = dt["waiver_title"].ToString(),
                        waiver_amount = dt["waiver_amount"].ToString(),
                        waivergroup_name = dt["waivergroup_name"].ToString(),
                        approval_status = ComputeApprovalStatus(dt["rmpostccwaiver_gid"].ToString())
                    });

                }
            }
            values.rmpostccwaiver_list = getrmpostccwaiver_list;
            dt_datatable.Dispose();
        }

        public void DaGetDescDocAppPersons(string rmpostccwaiver_gid, MdlDescDocAppPersons values)
        {
            msSQL = " SELECT waiver_description FROM ocs_mst_trmpostccwaiver" +
                    " where rmpostccwaiver_gid='" + rmpostccwaiver_gid + "'";
            values.waiver_description = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select rmpostccwaiver2document_gid,document_name,document_path,document_title from ocs_mst_trmpostccwaiver2document " +
                                 " where rmpostccwaiver_gid='" + rmpostccwaiver_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdocumentdtlList = new List<uploadwaiverdoc_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getdocumentdtlList.Add(new uploadwaiverdoc_list
                    {
                        document_name = dt["document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                        rmpostccwaiver2document_gid = dt["rmpostccwaiver2document_gid"].ToString(),
                        document_title = dt["document_title"].ToString(),
                    });
                    values.uploadwaiverdoc_list = getdocumentdtlList;
                }
            }
            dt_datatable.Dispose();

            msSQL = "select rmpostccwaiver2approvalmember_gid,approvalmember_name from ocs_mst_trmpostccwaiver2approvalmember where " +
              " rmpostccwaiver_gid='" + rmpostccwaiver_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getwaiverapprovalmember_list = new List<waiverapprovalmember_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getwaiverapprovalmember_list.Add(new waiverapprovalmember_list
                    {
                        rmpostccwaiver2approvalmember_gid = (dr_datarow["rmpostccwaiver2approvalmember_gid"].ToString()),
                        approvalmember_name = (dr_datarow["approvalmember_name"].ToString()),
                    });
                }
            }
            values.waiverapprovalmember_list = getwaiverapprovalmember_list;
            dt_datatable.Dispose();

        }


        public static string ComputeApprovalToken()
        {
            Random rand = new Random();
            string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
            string sToken = "";
            int Length = 100;
            for (int j = 0; j < Length; j++)
            {
                string sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                sToken += sTempChars;
            }
            return sToken;
        }

        public static string ComputeApprovalStatus(string rmpostccwaiver_gid)
        {
            dbconn objappdbconn = new dbconn();  
            DataTable dt_appdatatable;
            int approval_count = 0, rejected_count = 0;
            string msSQL, approval_status = "Pending";
            msSQL = " select approval_status" +
                           " from ocs_mst_trmpostccwaiver2approvalmember" +
                           " where rmpostccwaiver_gid='" + rmpostccwaiver_gid + "'";
            dt_appdatatable = objappdbconn.GetDataTable(msSQL);
            var approvalstatus_list = new List<string>();
            if (dt_appdatatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_appdatatable.Rows)
                {
                    approvalstatus_list.Add(dt["approval_status"].ToString());
                }
            }
            for(int i = 0; i < approvalstatus_list.Count; i++)
            {
                if(approvalstatus_list[i] == "Approved")
                {
                    approval_count++;
                } 
                else if(approvalstatus_list[i] == "Rejected")
                {
                    rejected_count++;
                    break;
                }
                else
                {

                }
            }
            if((approval_count == approvalstatus_list.Count) && (rejected_count == 0))
            {
                approval_status = "Approved";
            } else if(rejected_count == 1)
            {
                approval_status = "Rejected";
            } else
            {
                
            }

            return approval_status;
        }

    }
}