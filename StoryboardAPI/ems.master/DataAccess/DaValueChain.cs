using ems.master.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;

namespace ems.master.DataAccess
{
    public class DaValueChain
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        DataTable dt_datatable;
        OdbcDataReader objODBCDatareader;
        string msSQL, msGetGid, msValueCode, msGetAPICode;
        int mnResult, GetApiMasterGID;
        string lsmaster_value;

        public void DaGetValueChain(MdlValueChain objMdlValueChain)
        {
            try
            {
                msSQL = " SELECT valuechain_gid,api_code,valuechain_name,lms_code,bureau_code,status_log, " +
                    " date_format(a.created_date,'%d-%m-%Y || %h:%i %p') as created_date,concat(c.user_firstname,' ' ,c.user_lastname,'||',c.user_code) as created_by " +
                    " from ocs_mst_tvaluechain a" +
                    " left join hrm_mst_temployee b on a.created_by=b.employee_gid" +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid order by valuechain_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getvaluechain = new List<valuechain_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getvaluechain.Add(new valuechain_list
                        {
                            valuechain_gid = (dr_datarow["valuechain_gid"].ToString()),
                            api_code = (dr_datarow["api_code"].ToString()),
                            lms_code = (dr_datarow["lms_code"].ToString()),
                            valuechain_name = (dr_datarow["valuechain_name"].ToString()),
                            bureau_code = (dr_datarow["bureau_code"].ToString()),
                            status_log = (dr_datarow["status_log"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                        });
                    }
                    objMdlValueChain.valuechain_list = getvaluechain;
                }
                dt_datatable.Dispose();
                objMdlValueChain.status = true;
            }
            catch
            {
                objMdlValueChain.status = false;
            }
        }

        public void DaCreateValueChain(valuechain values, string employee_gid)
        {
            string bureau_code, valuechain_name, lms_code, lslms_code, lsbureau_code;

            if (values.lms_code == null || values.lms_code == "")
            {
                lslms_code = "";
            }
            else
            {
                lslms_code = values.lms_code.Replace("'", "\\'");
            }
            if (values.bureau_code == null || values.bureau_code == "")
            {
                lsbureau_code = "";
            }
            else
            {
                lsbureau_code = values.bureau_code.Replace("'", "\\'");
            }

            msSQL = "select valuechain_gid from ocs_mst_tvaluechain where valuechain_name = '" + values.valuechain_name.Replace("'", "\\'") + "'";
            string lsdocumentgid = objdbconn.GetExecuteScalar(msSQL);
            if (lsdocumentgid != "")
            {
                //if (lsdocumentgid != values.valuechain_gid)
                //{
                    values.message = "This ValueChain Already Exists";
                    values.status = false;
                    return;
                //}
            }
                        
            msGetGid = objcmnfunctions.GetMasterGID("MSVC");
            msGetAPICode = objcmnfunctions.GetApiMasterGID("VALC");
            msValueCode = objcmnfunctions.GetMasterGID("VC");
            msSQL = " insert into ocs_mst_tvaluechain(" +
                    " valuechain_gid," +
                     " api_code," +
                    " valuechain_code," +
                    " valuechain_name," +
                    " lms_code," +
                    " bureau_code," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + msGetAPICode + "'," +
                    "'" + msValueCode + "'," +
                    "'" + values.valuechain_name.Replace("'", "\\'") + "'," +
                    "'" + lslms_code + "'," +
                    "'" + lsbureau_code + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "ValueChain Added Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured While Adding'";
            }
        }

        public void DaEditValueChain(string valuechain_gid, valuechain values)
        {
            try
            {
                msSQL = " select valuechain_gid,valuechain_name,lms_code,bureau_code,status_log"+
                    " from ocs_mst_tvaluechain where valuechain_gid='" + valuechain_gid + "' ";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.valuechain_gid = objODBCDatareader["valuechain_gid"].ToString();
                    values.lms_code = objODBCDatareader["lms_code"].ToString();
                    values.valuechain_name = objODBCDatareader["valuechain_name"].ToString();
                    values.bureau_code = objODBCDatareader["bureau_code"].ToString();
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

        public void DaUpdateValueChain(string employee_gid, valuechain values)
        {
            msSQL = "select valuechain_gid from ocs_mst_tvaluechain where valuechain_name = '" + values.valuechain_name.Replace("'", "\\'") + "'";
            string lsdocumentgid = objdbconn.GetExecuteScalar(msSQL);
            if (lsdocumentgid != "")
            {
                if (lsdocumentgid != values.valuechain_gid)
                {
                    values.message = "This ValueChain Already Exists";
                    values.status = false;
                    return;
                }
            }

            string lslms_code, lsbureau_code;

            if (values.lms_code == null || values.lms_code == "")
            {
                lslms_code = "";
            }
            else
            {
                lslms_code = values.lms_code.Replace("'", "\\'");
            }
            if (values.bureau_code == null || values.bureau_code == "")
            {
                lsbureau_code = "";
            }
            else
            {
                lsbureau_code = values.bureau_code.Replace("'", "\\'");
            }

                        
            msSQL = "select updated_by, updated_date,valuechain_name from ocs_mst_tvaluechain where valuechain_gid = '" + values.valuechain_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                string lsUpdatedBy = objODBCDatareader["updated_by"].ToString();
                string lsUpdatedDate = objODBCDatareader["updated_date"].ToString();

                if (!(String.IsNullOrEmpty(lsUpdatedBy)) && !(String.IsNullOrEmpty(lsUpdatedDate)))
                {
                    msGetGid = objcmnfunctions.GetMasterGID("VCOG");
                    msSQL = " insert into ocs_trn_tauditvaluechainlog(" +
                              " auditvaluechainlog_gid," +
                              " valuechain_gid," +
                              " valuechain_name, " +
                              " created_by, " +
                              " created_date) " +
                              " values(" +
                              "'" + msGetGid + "'," +
                              "'" + values.valuechain_gid + "'," +
                              "'" + objODBCDatareader["valuechain_name"].ToString() + "'," +
                              "'" + employee_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            objODBCDatareader.Close();
            msSQL = " update ocs_mst_tvaluechain set " +
                " valuechain_name='" + values.valuechain_name.Replace("'", "\\'") + "'," +
                " lms_code='" + lslms_code + "'," +
                " bureau_code='" + lsbureau_code + "'," +
                 " updated_by='" + employee_gid + "'," +
                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where valuechain_gid='" + values.valuechain_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "ValueChain Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured While Updating'";
            }
            
        }

        public void DaDeleteValueChain(string valuechain_gid,string employee_gid, valuechain values)
        {
            msSQL = " select application2secondaryvaluechain_gid from ocs_mst_tapplication2secondaryvaluechain where secondaryvaluechain_gid='" + valuechain_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objODBCDatareader.Close();
                values.message = "Can't able to delete Category, Because it is tagged to Application Creation";
                values.status = false;
                return;
            }
            else
            {
                objODBCDatareader.Close();
                msSQL = " select application2primaryvaluechain_gid from ocs_mst_tapplication2primaryvaluechain where primaryvaluechain_gid='" + valuechain_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objODBCDatareader.Close();
                    values.message = "Can't able to delete Category, Because it is tagged to Application Creation";
                    values.status = false;
                    return;
                }
                else
                {
                    objODBCDatareader.Close();
                    msSQL = " select valuechain_name from ocs_mst_tvaluechain where valuechain_gid='" + valuechain_gid + "'";
                    lsmaster_value = objdbconn.GetExecuteScalar(msSQL);
                    msSQL = " delete from ocs_mst_tvaluechain where valuechain_gid='" + valuechain_gid + "'";
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
                                 "'Value Chain'," +
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
        //----Get Value chain - order by ASC-----//
        public void DaGetValueChainASC(MdlValueChain objMdlValueChain)
        {
            try
            {
                msSQL = " SELECT valuechain_gid,valuechain_name FROM ocs_mst_tvaluechain order by valuechain_gid asc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getvaluechain = new List<valuechain_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getvaluechain.Add(new valuechain_list
                        {
                            valuechain_gid = (dr_datarow["valuechain_gid"].ToString()),
                             valuechain_name = (dr_datarow["valuechain_name"].ToString()),
                        });
                    }
                    objMdlValueChain.valuechain_list = getvaluechain;
                }
                dt_datatable.Dispose();
                objMdlValueChain.status = true;
            }
            catch
            {
                objMdlValueChain.status = false;
            }
        }
        public void DaValueChainStatusUpdate(string employee_gid, valuechain values)
        {

            msSQL = " update ocs_mst_tvaluechain set status_log='" + values.status_log + "'," +
                " remarks='" + values.remarks.Replace("'", " ") + "'," +
                " updated_by='" + employee_gid + "'," +
                " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                " where valuechain_gid='" + values.valuechain_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("VCOG");
                msSQL = " insert into ocs_trn_tvaluechainstatuslog(" +
                          " valuechainstatuslog_gid," +
                          " valuechain_gid," +
                          " status_log, " +
                          " remarks, " +
                          " created_by, " +
                          " created_date) " +
                          " values(" +
                          "'" + msGetGid + "'," +
                          "'" + values.valuechain_gid + "'," +
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
                values.message = "Error Occured While Updating Status";
                values.status = false;
            }
        }
        //Get Active Status Log
        public void DaGetActiveLog(string valuechain_gid, MdlValueChain objgetsegment)
        {
            try
            {
                msSQL = " SELECT d.valuechain_name,a.status_log,a.remarks, " +
                    " date_format(a.created_date,'%d-%m-%Y || %h:%i %p') as created_date,concat(c.user_firstname,' ' ,c.user_lastname,'||',c.user_code) as created_by" +
                    " FROM ocs_trn_tvaluechainstatuslog a" +
                    " left join hrm_mst_temployee b on a.created_by=b.employee_gid" +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                    "  left join ocs_mst_tvaluechain d on a.valuechain_gid=d.valuechain_gid where a.valuechain_gid='" + valuechain_gid + "' order by a.valuechainstatuslog_gid desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getSegment = new List<valuechain_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getSegment.Add(new valuechain_list
                        {
                            valuechain_name = (dr_datarow["valuechain_name"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                            status_log = (dr_datarow["status_log"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                        });
                    }
                    objgetsegment.valuechain_list = getSegment;
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