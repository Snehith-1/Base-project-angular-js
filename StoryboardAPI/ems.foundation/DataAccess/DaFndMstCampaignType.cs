using ems.foundation.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;


namespace ems.foundation.DataAccess
{
    public class DaFndMstCampaignTypeMaster
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        DataTable dt_datatable;
        OdbcDataReader objODBCDatareader, objODBCDatareader1, objODBCDatareader2;
        string msSQL, msGetGid, lscampaigntype_value, lslms_code, lsbureau_code, lsremarks, lscampaigntype_code;
        int mnResult;
        public void DaGetCampaignType(MdlFndMstCampaignTypeMaster values)
        {
            try
            {
                msSQL = " SELECT a.campaigntype_gid,a.campaigntype_name, a.campaigntype_code, a.lms_code, a.bureau_code,a.remarks, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                        " FROM fnd_mst_tcampaigntype a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid order by a.campaigntype_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcampaigntype_list = new List<campaigntype_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcampaigntype_list.Add(new campaigntype_list
                        {
                            campaigntype_gid = (dr_datarow["campaigntype_gid"].ToString()),
                            campaigntype_name = (dr_datarow["campaigntype_name"].ToString()),

                            campaigntype_code = (dr_datarow["campaigntype_code"].ToString()),
                            lms_code = (dr_datarow["lms_code"].ToString()),
                            bureau_code = (dr_datarow["bureau_code"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            status = (dr_datarow["status"].ToString()),
                        });
                    }
                    values.campaigntype_list = getcampaigntype_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaCreateCampaignType(MdlFndMstCampaignTypeMaster values, string employee_gid)
        {
            msSQL = "select campaigntype_name from fnd_mst_tcampaigntype where campaigntype_name = '" + values.campaigntype_name.Replace("'", "\\'") + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.status = false;
                values.message = "Campaign Type Already Exist";
            }
            else
            {

                if (values.lms_code == null || values.lms_code == "")
                {
                    lslms_code = "";
                }
                else
                {
                    lslms_code = values.lms_code.Replace("'", "");
                }
                if (values.bureau_code == null || values.bureau_code == "")
                {
                    lsbureau_code = "";
                }
                else
                {
                    lsbureau_code = values.bureau_code.Replace("'", "");
                }
                if (values.remarks == null || values.remarks == "")
                {
                    lsremarks = "";
                }
                else
                {
                    lsremarks = values.remarks.Replace("'", "");
                }



                msGetGid = objcmnfunctions.GetMasterGID("FCTL");
                lscampaigntype_code = objcmnfunctions.GetMasterGID("ICATY");

                msSQL = " insert into fnd_mst_tcampaigntype(" +
                        " campaigntype_gid," +
                        " campaigntype_name," +
                        " campaigntype_code," +
                        " lms_code," +
                        " bureau_code," +
                        " remarks," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + values.campaigntype_name.Replace("'", "") + "'," +
                        "'" + lscampaigntype_code + "'," +
                        "'" + lslms_code + "'," +
                        "'" + lsbureau_code + "'," +
                        "'" + lsremarks + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Campaign Type Added Successfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occurred While Adding";
                }
            }
        }

        public void DaEditCampaignType(string campaigntype_gid, MdlFndMstCampaignTypeMaster values)
        {
            try
            {
                msSQL = " SELECT campaigntype_gid,campaigntype_name,campaigntype_code,lms_code, bureau_code,remarks, status as Status FROM fnd_mst_tcampaigntype where campaigntype_gid='" + campaigntype_gid + "' ";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.campaigntype_gid = objODBCDatareader["campaigntype_gid"].ToString();
                    values.campaigntype_name = objODBCDatareader["campaigntype_name"].ToString();

                    values.campaigntype_code = objODBCDatareader["campaigntype_code"].ToString();
                    values.lms_code = objODBCDatareader["lms_code"].ToString();
                    values.bureau_code = objODBCDatareader["bureau_code"].ToString();
                    values.remarks = objODBCDatareader["remarks"].ToString();
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

        public void DaUpdateCampaignType(string employee_gid, MdlFndMstCampaignTypeMaster values)
        {


            msSQL = " update fnd_mst_tcampaigntype set " +
                 " campaigntype_name='" + values.campaigntype_name.Replace("'", "") + "'," +

                 " campaigntype_code='" + values.campaigntype_code + "'," +
                 " lms_code='" + values.lms_code + "'," +
                 " bureau_code='" + values.bureau_code + "'," +
                 " remarks='" + values.remarks + "'," +

                 " updated_by='" + employee_gid + "'," +
                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where campaigntype_gid='" + values.campaigntype_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("FDLO");

                msSQL = " insert into fnd_mst_tcampaigntypelog (" +
                       " campaigntypelog_gid, " +
                       " campaigntype_gid, " +
                       " campaigntype_name," +
                       " updated_by," +
                       " updated_date) " +
                       " values (" +
                       " '" + msGetGid + "'," +
                       " '" + values.campaigntype_gid + "'," +
                       " '" + values.campaigntype_name.Replace("'", "") + "'," +
                       " '" + employee_gid + "'," +
                       " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Campaign Type Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Updating";
            }
        }

        public void DaInactiveCampaignType(MdlFndMstCampaignTypeMaster values, string employee_gid)
        {
            msSQL = " update fnd_mst_tcampaigntype set status='" + values.rbo_status + "'" +
                    " where campaigntype_gid='" + values.campaigntype_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("FDIL");

                msSQL = " insert into fnd_mst_tcampaigntypeinactivelog (" +
                      " campaigntypeinactivelog_gid, " +
                      " campaigntype_gid," +
                      " campaigntype_name," +
                      " status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.campaigntype_gid + "'," +
                      " '" + values.campaigntype_name + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", "") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "Campaign Type Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Campaign Type Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        }

        public void DaDeleteCampaignType(string campaigntype_gid, string employee_gid, MdlFndMstCampaignTypeMaster values)
        {


            msSQL = " select campaigntype_name from fnd_mst_tcampaigntype where campaigntype_gid='" + campaigntype_gid + "'";
            lscampaigntype_value = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " delete from fnd_mst_tcampaigntype where campaigntype_gid='" + campaigntype_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Campaign Type Deleted Successfully..!";
                msGetGid = objcmnfunctions.GetMasterGID("FDDL");
                msSQL = " insert into fnd_mst_tcampaigntypedeletelog(" +
                         "campaigntypedeletelog_gid, " +
                         "campaigntype_gid, " +
                         "master_name, " +
                         "master_value, " +
                         "deleted_by, " +
                         "deleted_date) " +
                         " values(" +
                         "'" + msGetGid + "'," +
                         "'" + campaigntype_gid + "', " +
                         "'Campaigntype Type'," +
                         "'" + lscampaigntype_value + "'," +
                         "'" + employee_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
        }


        public void DaCampaignTypeInactiveLogview(string campaigntype_gid, MdlFndMstCampaignTypeMaster values)
        {
            try
            {
                msSQL = " SELECT a.campaigntype_gid,date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as Status, a.remarks" +
                        " FROM fnd_mst_tcampaigntypeinactivelog a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where a.campaigntype_gid ='" + campaigntype_gid + "' order by a.campaigntypeinactivelog_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcampaigntype_list = new List<campaigntype_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcampaigntype_list.Add(new campaigntype_list
                        {
                            campaigntype_gid = (dr_datarow["campaigntype_gid"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            status = (dr_datarow["Status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                        });
                    }
                    values.campaigntype_list = getcampaigntype_list;
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