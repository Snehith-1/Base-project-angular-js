using ems.mastersamagro.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;

namespace ems.mastersamagro.DataAccess
{
    /// <summary>
    /// This DataAccess will provide access to Address Type master
    /// </summary>
    /// <remarks>Written by Abilash.A </remarks>
    public class DaAgrMstAddressType
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        OdbcDataReader objODBCDatareader;
        string lsmaster_value;

        DataTable dt_datatable;
        string msSQL, msGetGid;
        int mnResult;

        public void DaGetAddressType(MdlAddressType objMdlAddressType)
        {
            try
            {
                msSQL = " SELECT address_gid,address_type,lms_code,bureau_code,status_log, " +
                    " date_format(a.created_date,'%d-%m-%Y || %h:%i %p') as created_date,concat(c.user_firstname,' ' ,c.user_lastname,'||',c.user_code) as created_by " +
                    " from ocs_mst_taddresstype a" +
                    " left join hrm_mst_temployee b on a.created_by=b.employee_gid" +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid order by address_gid desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getaddresstype = new List<addresstype_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getaddresstype.Add(new addresstype_list
                        {
                            address_gid = (dr_datarow["address_gid"].ToString()),
                            address_type = (dr_datarow["address_type"].ToString()),
                            lms_code = (dr_datarow["lms_code"].ToString()),
                            bureau_code = (dr_datarow["bureau_code"].ToString()),
                            status_log = (dr_datarow["status_log"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                        });
                    }
                    objMdlAddressType.addresstype_list = getaddresstype;
                }
                dt_datatable.Dispose();
                objMdlAddressType.status = true;
            }
            catch
            {
                objMdlAddressType.status = false;
            }
        }

        public void DaCreateAddressType(addresstype values,string employee_gid)
        {
            msGetGid = objcmnfunctions.GetMasterGID("MSAD");
            msSQL = " insert into ocs_mst_taddresstype(" +
                    " address_gid," +
                    " lms_code," +
                    " bureau_code," +
                    " address_type," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                           "'" + msGetGid + "',";
            if (values.lms_code == "" || values.lms_code == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.lms_code.Replace("'", "") + "',";
            }
            if (values.bureau_code == "" || values.bureau_code == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.bureau_code.Replace("'", "") + "',";
            }

            msSQL += "'" + values.address_type.Replace("'", "") + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
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

        public void DaEditAddressType(string address_gid, addresstype values)
        {
            try
            {
                msSQL = " select address_gid,lms_code,bureau_code,status_log ,address_type from ocs_mst_taddresstype where address_gid='" + address_gid + "' ";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.lms_code = objODBCDatareader["lms_code"].ToString();
                    values.bureau_code = objODBCDatareader["bureau_code"].ToString();
                    values.status_log = objODBCDatareader["status_log"].ToString();
                    values.address_type = objODBCDatareader["address_type"].ToString();
                    values.address_gid = objODBCDatareader["address_gid"].ToString();
                }
                objODBCDatareader.Close();
                values.status = true;

            }
            catch
            {
                values.status = false;
            }
        }

        public void DaUpdateAddressType(string employee_gid, addresstype values)
        {
            msSQL = "select updated_by, updated_date,address_type from ocs_mst_taddresstype where address_gid = '" + values.address_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                string lsUpdatedBy = objODBCDatareader["updated_by"].ToString();
                string lsUpdatedDate = objODBCDatareader["updated_date"].ToString();

                if (!(String.IsNullOrEmpty(lsUpdatedBy)) && !(String.IsNullOrEmpty(lsUpdatedDate)))
                {
                    msGetGid = objcmnfunctions.GetMasterGID("ADLG");
                    msSQL = " insert into ocs_trn_tauditaddresstypelog(" +
                              " auditaddresstypelog_gid," +
                              " address_gid," +
                              " address_type, " +
                              " lastupdated_by, " +
                              " lastupdated_date, " +
                              " created_by, " +
                              " created_date) " +
                              " values(" +
                              "'" + msGetGid + "'," +
                              "'" + values.address_gid + "'," +
                              "'" + objODBCDatareader["address_type"].ToString() + "'," +
                              "'" + objODBCDatareader["updated_by"].ToString() + "'," +
                              "'" + objODBCDatareader["updated_date"].ToString() + "'," +
                              "'" + employee_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            objODBCDatareader.Close();
            msSQL = " update ocs_mst_taddresstype set ";
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

            msSQL += " address_type='" + values.address_type + "'," +
                 " updated_by='" + employee_gid + "'," +
                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where address_gid='" + values.address_gid + "' ";
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

        public void DaDeleteAddressType(string address_gid, string employee_gid, addresstype values)
        {
            msSQL = " select addresstype_gid from ocs_mst_tcontact2address where addresstype_gid='" + address_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objODBCDatareader.Close();
                values.message = "Can't able to delete Address Type, Because it is tagged to Application Creation";
                values.status = false;
                return;
            }
            else
            {
                objODBCDatareader.Close();
                msSQL = " select addresstype_gid from ocs_mst_tinstitution2address where addresstype_gid='" + address_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objODBCDatareader.Close();
                    values.message = "Can't able to Delete Address Type, Because it is tagged to Application Creation";
                    values.status = false;
                    return;
                }
                else
                {
                    objODBCDatareader.Close();
                    msSQL = " select address_type from ocs_mst_taddresstype where address_gid='" + address_gid + "'";
                    lsmaster_value = objdbconn.GetExecuteScalar(msSQL);
                    msSQL = " delete from ocs_mst_taddresstype where address_gid='" + address_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult != 0)
                    {
                        values.status = true;
                        values.message = "Address Type Deleted Successfully..!";
                        msGetGid = objcmnfunctions.GetMasterGID("MSTD");
                        msSQL = " insert into ocs_mst_tmasterdeletelog(" +
                                 "master_gid, " +
                                 "master_name, " +
                                 "master_value, " +
                                 "deleted_by, " +
                                 "deleted_date) " +
                                 " values(" +
                                 "'" + msGetGid + "'," +
                                 "'Address Type'," +
                                 "'" + lsmaster_value + "'," +
                                 "'" + employee_gid + "'," +
                                 "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error Occured while Deleting..";
                    }
                }
            }
        }
        //-----------Get Address Type order by ASC--------//
        public void DaGetAddressTypeASC(MdlAddressType objMdlAddressType)
        {
            try
            {
                msSQL = " SELECT address_gid,address_type FROM ocs_mst_taddresstype order by address_gid asc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getaddresstype = new List<addresstype_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getaddresstype.Add(new addresstype_list
                        {
                            address_gid = (dr_datarow["address_gid"].ToString()),
                            address_type = (dr_datarow["address_type"].ToString())
                        });
                    }
                    objMdlAddressType.addresstype_list = getaddresstype;
                }
                dt_datatable.Dispose();
                objMdlAddressType.status = true;
            }
            catch
            {
                objMdlAddressType.status = false;
            }
        }
        public void DaAddressTypeStatusUpdate(string employee_gid, addresstype values)
        {

            msSQL = " update ocs_mst_taddresstype set status_log='" + values.status_log + "'," +
                " remarks='" + values.remarks.Replace("'", " ") + "'," +
                " updated_by='" + employee_gid + "'," +
                " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                " where address_gid='" + values.address_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("ADLG");
                msSQL = " insert into ocs_trn_taddresstypestatuslog(" +
                          " addresstypestatuslog_gid," +
                          " address_gid," +
                          " status_log, " +
                          " remarks, " +
                          " created_by, " +
                          " created_date) " +
                          " values(" +
                          "'" + msGetGid + "'," +
                          "'" + values.address_gid + "'," +
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
        public void DaGetActiveLog(string address_gid, MdlAddressType objgetsegment)
        {
            try
            {
                msSQL = " SELECT d.address_type,a.status_log,a.remarks, " +
                    " date_format(a.created_date,'%d-%m-%Y || %h:%i %p') as created_date,concat(c.user_firstname,' ' ,c.user_lastname,'||',c.user_code) as created_by" +
                    " FROM ocs_trn_taddresstypestatuslog a" +
                    " left join hrm_mst_temployee b on a.created_by=b.employee_gid" +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                    "  left join ocs_mst_taddresstype d on a.address_gid=d.address_gid where a.address_gid='" + address_gid + "' order by a.addresstypestatuslog_gid desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getSegment = new List<addresstype_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getSegment.Add(new addresstype_list
                        {
                            address_type = (dr_datarow["address_type"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                            status_log = (dr_datarow["status_log"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                        });
                    }
                    objgetsegment.addresstype_list = getSegment;
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