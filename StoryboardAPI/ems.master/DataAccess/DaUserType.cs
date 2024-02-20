using ems.master.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Web;
using System.Runtime.Serialization;

namespace ems.master.DataAccess
{
    public class DaUserType
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        OdbcDataReader objODBCDatareader;
        string lsmaster_value;

        DataTable dt_datatable;
        string msSQL, msGetGid, msGetAPICode;
        int mnResult, GetApiMasterGID;
        public string address;
        public string external_ip;
        public string session_id;
        public string user_code;
        private object login_time;

        public ipandlogintimemodel DaGetipandlogintime(ipandlogintimemodel objipandlogintimemodel, string employee_gid, string token, string user_gid)
        {

            try
            {
                address = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                string[] _strarrvalues = address.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                external_ip = _strarrvalues[0];
                int colonIndex = external_ip.IndexOf(':');
                if (colonIndex > 0)
                    external_ip = external_ip.Remove(colonIndex);
            }
            catch (Exception ex)
            {
                //Auditlog("ExternalIP", "Failure", ex.StackTrace.ToString(), "Common", objipandlogintimemodel);
                address = "";
            }
            if (address == "")
            {
                address = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                external_ip = address;
            }
            objipandlogintimemodel.ip = external_ip;
          

            string ip1 = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(ip1))
            {
                ip1 = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            objipandlogintimemodel.ip = ip1;
            objipandlogintimemodel.login_time = DateTime.Now.ToString("HH:mm");

            //msSQL = "select user_code from adm_mst_tuser where user_gid = '" + user_gid + "'";
            //user_code = objdbconn.GetExecuteScalar(msSQL);
            ////msSQL = "select token from storyboarddb.adm_trn_tconsumertoken where user_code = '" + user_code + "'";
            //msSQL = "select token from adm_mst_ttoken where user_gid = '" + user_gid + "'";
            //session_id = objdbconn.GetExecuteScalar(msSQL);
            msSQL = "select login_time from ocs_mst_tlogindetailslog where session_id = '" + token +"'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                objODBCDatareader.Close();
                msSQL = "select date_format(login_time,'%d/%m/%Y %h:%i %p') from ocs_mst_tlogindetailslog where user_gid = '" + user_gid + "' order by login_time desc limit 1";
                objipandlogintimemodel.login_time = objdbconn.GetExecuteScalar(msSQL);
            }
             else
            {
                objODBCDatareader.Close();
                msSQL = "select date_format(login_time,'%d-%m-%Y %h:%i %p') from ocs_mst_tlogindetailslog where user_gid = '" + user_gid + "' order by login_time desc limit 1";
                objipandlogintimemodel.login_time = objdbconn.GetExecuteScalar(msSQL);
                msGetGid = objcmnfunctions.GetMasterGID("LOG");
                msSQL = " insert into ocs_mst_tlogindetailslog(" +
                        " login_gid," +
                        " login_id," +
                        " login_time," +
                        " session_id," +
                        " user_gid," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + ip1 + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "'," +
                        "'" + token + "'," +
                        "'" + user_gid + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                
            }
            //if(objipandlogintimemodel.login_time == "undefined")
            //{
            //    objipandlogintimemodel.login_time = " ";
            //}
            return objipandlogintimemodel;
        }


        public void DaGetUserType(MdlUserType objMdlUserType)
        {
            try
            {
                msSQL = " select usertype_gid,api_code,user_type,lms_code,bureau_code,status_log, " +
                    " date_format(a.created_date,'%d-%m-%Y || %h:%i %p') as created_date,concat(c.user_firstname,' ' ,c.user_lastname,'||',c.user_code) as created_by " +
                    " from ocs_mst_tusertype a" +
                    " left join hrm_mst_temployee b on a.created_by=b.employee_gid" +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid order by usertype_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getusertype = new List<usertype_list>();
                if(dt_datatable.Rows.Count != 0)
                {
                    foreach(DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getusertype.Add(new usertype_list
                        {
                            usertype_gid = (dr_datarow["usertype_gid"].ToString()),
                            api_code = (dr_datarow["api_code"].ToString()),
                            lms_code = (dr_datarow["lms_code"].ToString()),
                            user_type = (dr_datarow["user_type"].ToString()),
                            bureau_code = (dr_datarow["bureau_code"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            status_log = (dr_datarow["status_log"].ToString()),
                        });
                    }
                    objMdlUserType.usertype_list = getusertype;
                }
                dt_datatable.Dispose();
                objMdlUserType.status = true;
            }
            catch
            {
                objMdlUserType.status = false;
            }
        }

        public void DaCreateUserType(usertype values, string employee_gid)
        {

            msSQL = "select user_type from ocs_mst_tusertype where user_type = '" + values.user_type.Replace("'", "\\'") + "'";
            string lsdocumentgid = objdbconn.GetExecuteScalar(msSQL);
            if (lsdocumentgid != "")
            {
                //if (lsdocumentgid != values.companydocument_gid)
                //{
                    values.message = "This User Type Already Exists";
                    values.status = false;
                    return ;
                //}
            }
            msGetGid = objcmnfunctions.GetMasterGID("MSUT");
            msGetAPICode = objcmnfunctions.GetApiMasterGID("USER");
            msSQL = " insert into ocs_mst_tusertype(" +
                    " usertype_gid," +
                    " api_code," +
                    " user_type," +
                    " lms_code," +
                    " bureau_code," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + msGetAPICode + "'," +
                    "'" + values.user_type.Replace("'", "") + "',";
            if (values.lms_code == "" || values.lms_code == null)
            {
                msSQL += " '',";
            }
            else
            {
                msSQL += "'" + values.lms_code + "',";
            }
            if (values.bureau_code == "" || values.bureau_code == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.bureau_code + "',";
            }

            msSQL += "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.message = "Stackholder Type Added Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error occured while adding";
                values.status = false;
            }
        }

        public void DaEditUserType(string usertype_gid, usertype values)
        {
            try
            {
                msSQL = " select usertype_gid,user_type,lms_code,bureau_code,status_log, "+
                    " date_format(a.created_date,'%d-%m-%Y || %h:%i %p') as created_date,concat(c.user_firstname,' ' ,c.user_lastname,'||',c.user_code) as created_by "+
                    " from ocs_mst_tusertype a" +
                    " left join hrm_mst_temployee b on a.created_by=b.employee_gid" +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid"+
                    " where usertype_gid='" + usertype_gid + "' ";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.lms_code = objODBCDatareader["lms_code"].ToString();
                    values.user_type = objODBCDatareader["user_type"].ToString();
                    values.usertype_gid = objODBCDatareader["usertype_gid"].ToString();
                    values.bureau_code = objODBCDatareader["bureau_code"].ToString();
                    values.created_date = objODBCDatareader["created_date"].ToString();
                    values.created_by = objODBCDatareader["created_by"].ToString();
                    values.status_log = objODBCDatareader["status_log"].ToString();
                }
                objODBCDatareader.Close();
                values.status = true;

            }
            catch
            {
                values.status = false;
            }
        }

        public void DaUpdateUserType(string employee_gid, usertype values)
        {
            msSQL = "select usertype_gid from ocs_mst_tusertype where user_type = '" + values.user_type.Replace("'", "\\'") + "'";
            string lsdocumentgid = objdbconn.GetExecuteScalar(msSQL);
            if (lsdocumentgid != "")
            {
                if (lsdocumentgid != values.usertype_gid)
                {
                    values.message = "This User Type Already Exists";
                    values.status = false;
                    return;
                }
            }

            msSQL = "select updated_by, updated_date,user_type from ocs_mst_tusertype where usertype_gid = '" + values.usertype_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                string lsUpdatedBy = objODBCDatareader["updated_by"].ToString();
                string lsUpdatedDate = objODBCDatareader["updated_date"].ToString();

                if (!(String.IsNullOrEmpty(lsUpdatedBy)) && !(String.IsNullOrEmpty(lsUpdatedDate)))
                {
                    msGetGid = objcmnfunctions.GetMasterGID("ULOG");
                    msSQL = " insert into ocs_trn_tauditusertypelog(" +
                              " auditusertypelog_gid," +
                              " usertype_gid," +
                              " user_type, " +
                              " created_by, " +
                              " created_date) " +
                              " values(" +
                              "'" + msGetGid + "'," +
                              "'" + values.usertype_gid + "'," +
                              "'" + objODBCDatareader["user_type"].ToString() + "'," +
                              "'" + employee_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            objODBCDatareader.Close();
            msSQL = " update ocs_mst_tusertype set ";
            if (values.lms_code == "" || values.lms_code == null)
            {
                msSQL += " lms_code='',";
            }
            else
            {
                msSQL += " lms_code='" + values.lms_code + "',";
            }
            if (values.bureau_code == "" || values.bureau_code == null)
            {
                msSQL += " bureau_code='',";
            }
            else
            {
                msSQL += " bureau_code='" + values.bureau_code + "',";
            }

            msSQL += " user_type='" + values.user_type + "'," +
                 " updated_by='" + employee_gid + "'," +
                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where usertype_gid='" + values.usertype_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.message = "Stackholder Type updated Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured while updating";
                values.status = false;
            }
        }

        public void DaDeleteUserType(string usertype_gid, string employee_gid,usertype values)
        {

            msSQL = " select contact_gid from ocs_mst_tcontact where stakeholdertype_gid='" + usertype_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objODBCDatareader.Close();
                values.message = "Can't able to delete Stackholder Type, Because it is tagged to Application Creation";
                values.status = false;
                return;
            }
            else
            {
                objODBCDatareader.Close();
                msSQL = " select institution_gid from ocs_mst_tinstitution where stakeholdertype_gid='" + usertype_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objODBCDatareader.Close();
                    values.message = "Can't able to delete Stackholder Type, Because it is tagged to Application Creation";
                    values.status = false;
                    return;
                }
                else
                {
                    objODBCDatareader.Close();
                    msSQL = " select user_type from ocs_mst_tusertype where usertype_gid='" + usertype_gid + "'";
                    lsmaster_value = objdbconn.GetExecuteScalar(msSQL);
                    msSQL = " delete from ocs_mst_tusertype where usertype_gid='" + usertype_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult != 0)
                    {
                        values.status = true;
                        msGetGid = objcmnfunctions.GetMasterGID("MSTD");
                        msSQL = " insert into ocs_mst_tmasterdeletelog(" +
                                 "master_gid, " +
                                 "master_name, " +
                                 "master_value, " +
                                 "deleted_by, " +
                                 "deleted_date) " +
                                 " values(" +
                                 "'" + msGetGid + "'," +
                                 "'Stackholder Type'," +
                                 "'" + lsmaster_value + "'," +
                                 "'" + employee_gid + "'," +
                                 "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    else
                    {
                        values.status = false;
                    }
                }
            }
        }
        public void DaGetUserTypeASC(MdlUserType objMdlUserType)
        {
            try
            {
                msSQL = " SELECT usertype_gid,user_type FROM ocs_mst_tusertype order by usertype_gid asc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getusertype = new List<usertype_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getusertype.Add(new usertype_list
                        {
                            usertype_gid = (dr_datarow["usertype_gid"].ToString()),
                            user_type = (dr_datarow["user_type"].ToString()),
                        });
                    }
                    objMdlUserType.usertype_list = getusertype;
                }
                dt_datatable.Dispose();
                objMdlUserType.status = true;
            }
            catch
            {
                objMdlUserType.status = false;
            }
        }

        public void DaUserTypeStatusUpdate(string employee_gid, usertype values)
        {

            msSQL = " update ocs_mst_tusertype set status_log='" + values.status_log + "'," +
                " remarks='" + values.remarks.Replace("'", " ") + "'," +
                " updated_by='" + employee_gid + "'," +
                " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                " where usertype_gid='" + values.usertype_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("ULOG");
                msSQL = " insert into ocs_trn_tusertypestatuslog(" +
                          " usertypestatuslog_gid," +
                          " usertype_gid," +
                          " status_log, " +
                          " remarks, " +
                          " created_by, " +
                          " created_date) " +
                          " values(" +
                          "'" + msGetGid + "'," +
                          "'" + values.usertype_gid + "'," +
                          "'" + values.status_log + "'," +
                          "'" + values.remarks.Replace("'", " ") + "'," +
                          "'" + employee_gid + "'," +
                          "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                values.message = "Status Updated Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured while updating Status";
                values.status = false;
            }
        }
        //Get Active Status Log
        public void DaGetActiveLog(string usertype_gid, MdlUserType objgetsegment)
        {
            try
            {
                msSQL = " SELECT d.user_type,a.status_log,a.remarks, " +
                    " date_format(a.created_date,'%d-%m-%Y || %h:%i %p') as created_date,concat(c.user_firstname,' ' ,c.user_lastname,'||',c.user_code) as created_by" +
                    " FROM ocs_trn_tusertypestatuslog a" +
                    " left join hrm_mst_temployee b on a.created_by=b.employee_gid" +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                    "  left join ocs_mst_tusertype d on a.usertype_gid=d.usertype_gid where a.usertype_gid='" + usertype_gid + "' order by a.usertypestatuslog_gid desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getSegment = new List<usertype_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getSegment.Add(new usertype_list
                        {
                            user_type = (dr_datarow["user_type"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                            status_log = (dr_datarow["status_log"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                        });
                    }
                    objgetsegment.usertype_list = getSegment;
                }
                dt_datatable.Dispose();
                objgetsegment.status = true;

            }
            catch
            {
                objgetsegment.status = false;
            }
        }
    }
}