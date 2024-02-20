using ems.storage.Functions;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Web;
using ems.brs.Models;
using System.Data;
using System.Web.Http;

namespace ems.brs.Dataacess
{
    public class DaBRSMaster
    {
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        DataTable dt_datatable;
        string msSQL, msGetGid;
        OdbcDataReader objODBCDatareader;
        dbconn objdbconn = new dbconn();
        int mnResult;
        string lslms_code, lsbureau_code;

        /// <summary>
        /// The purpose of data access is for creating BRS Master pages related data access or methods
        /// </summary>
        /// <param name="objMdlBRSActivity"> Created by #VCX300 [KALAIYARASAN PERUMAL] </param>


        // BRS Avtivity
        public void DaGetBRSActivity(MdlBRSMaster objMdlBRSMaster)
        {
            try
            {
                msSQL = " SELECT brsactivity_gid,brsactivity_name,lms_code,bureau_code,status_log, " +
                    " date_format(a.created_date,'%d-%b-%Y || %h:%i %p') as created_date,concat(c.user_firstname,' ' ,c.user_lastname,' || ',c.user_code) as created_by " +
                    " from brs_mst_tbrsactivity a" +
                    " left join hrm_mst_temployee b on a.created_by=b.employee_gid" +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid order by brsactivity_gid desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getbrsactivity_list = new List<BRSActivity_List>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getbrsactivity_list.Add(new BRSActivity_List
                        {
                            brsactivity_gid = (dr_datarow["brsactivity_gid"].ToString()),
                            brsactivity_name = (dr_datarow["brsactivity_name"].ToString()),
                            lms_code = (dr_datarow["lms_code"].ToString()),
                            bureau_code = (dr_datarow["bureau_code"].ToString()),
                            status_log = (dr_datarow["status_log"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                        });
                    }
                    objMdlBRSMaster.BRSActivity_List = getbrsactivity_list;
                }
                dt_datatable.Dispose();
                objMdlBRSMaster.status = true;
            }
            catch
            {
                objMdlBRSMaster.status = false;
            }
        }

        public void DaCreateBRSActivity(BRSActivity values, string employee_gid)
        {
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

            msGetGid = objcmnfunctions.GetMasterGID("BRAA");
            //msGetAPICode = objcmnfunctions.GetMasterGID("LONT");
            msSQL = " insert into brs_mst_tbrsactivity(" +
                    " brsactivity_gid," +
                    " brsactivity_name," +
                    " lms_code," +
                    " bureau_code," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.brsactivity_name.Replace("'", "\\'") + "'," +
                    "'" + lslms_code + "'," +
                    "'" + lsbureau_code + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.message = "BRS Activity Added Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured while adding";
                values.status = false;
            }
        }

        public void DaEditBRSActivity(string brsactivity_gid, BRSActivity values)
        {
            try
            {
                msSQL = " select brsactivity_gid,lms_code,bureau_code,status_log ,brsactivity_name from brs_mst_tbrsactivity where brsactivity_gid='" + brsactivity_gid + "' ";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.lms_code = objODBCDatareader["lms_code"].ToString();
                    values.bureau_code = objODBCDatareader["bureau_code"].ToString();
                    values.status_log = objODBCDatareader["status_log"].ToString();
                    values.brsactivity_name = objODBCDatareader["brsactivity_name"].ToString();
                    values.brsactivity_gid = objODBCDatareader["brsactivity_gid"].ToString();
                }
                objODBCDatareader.Close();
                values.status = true;

            }
            catch
            {
                values.status = false;
            }
        }

        public void DaUpdateBRSActivity(string employee_gid, BRSActivity values)
        {
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

            msSQL = "select updated_by, updated_date,brsactivity_name from brs_mst_tbrsactivity where brsactivity_gid = '" + values.brsactivity_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                
                    msGetGid = objcmnfunctions.GetMasterGID("BACU");
                    msSQL = " insert into brs_mst_tbrsactivitylog(" +
                              " brsactivitylog_gid," +
                              " brsactivity_gid," +
                              " brsactivity_name, " +
                              " lms_code, " +
                              " bureau_code, " +
                              " created_by, " +
                              " created_date) " +
                              " values(" +
                              "'" + msGetGid + "'," +
                              "'" + values.brsactivity_gid + "'," +
                              "'" + values.brsactivity_name.Replace("'", "\\'") + "'," +
                              "'" + lslms_code + "'," +
                              "'" + lsbureau_code + "'," +
                              "'" + employee_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                
            }
            objODBCDatareader.Close();
            msSQL = " update brs_mst_tbrsactivity set " +
                " brsactivity_name='" + values.brsactivity_name.Replace("'", "\\'") + "'," +
                " lms_code='" + lslms_code + "'," +
                " bureau_code='" + lsbureau_code + "'," +
                 " updated_by='" + employee_gid + "'," +
                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where brsactivity_gid='" + values.brsactivity_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "BRS Activity Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Updating";
            }
        }

        //public void DaBRSActivityDelete(string brsactivity_gid, string employee_gid, BRSActivity values)
        //{
        //    msSQL = " select application2loan_gid from brs_mst_tapplication2loan where brsactivity_gid='" + brsactivity_gid + "'";
        //    objODBCDatareader = objdbconn.GetDataReader(msSQL);
        //    if (objODBCDatareader.HasRows == true)
        //    {
        //        objODBCDatareader.Close();
        //        values.message = "Can't able to Delete BRS Activity, Because it is tagged to Application Creation";
        //        values.status = false;
        //        return;
        //    }
        //    else
        //    {
        //        objODBCDatareader.Close();
        //        msSQL = " select brsactivity_name from brs_mst_tbrsactivity where brsactivity_gid='" + brsactivity_gid + "'";
        //        string lsmaster_value = objdbconn.GetExecuteScalar(msSQL);
        //        msSQL = " delete from brs_mst_tbrsactivity where brsactivity_gid='" + brsactivity_gid + "'";
        //        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

        //        if (mnResult != 0)
        //        {
        //            values.status = true;
        //            values.message = "BRS Activity Deleted Successfully..!";
        //            msGetGid = objcmnfunctions.GetMasterGID("MSTD");
        //            msSQL = " insert into brs_mst_tmasterdeletelog(" +
        //                     "master_gid, " +
        //                     "master_name, " +
        //                     "master_value, " +
        //                     "deleted_by, " +
        //                     "deleted_date) " +
        //                     " values(" +
        //                     "'" + msGetGid + "'," +
        //                     "'BRS Activity'," +
        //                     "'" + lsmaster_value + "'," +
        //                     "'" + employee_gid + "'," +
        //                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

        //            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
        //        }
        //        else
        //        {
        //            values.status = false;
        //        }
        //    }
        //}
        
        public void DaBRSActivityStatusUpdate(string employee_gid, BRSActivity values)
        {

            msSQL = " update brs_mst_tbrsactivity set status_log='" + values.status_log + "'," +
                " remarks='" + values.remarks.Replace("'", "\\'") + "'," +
                " updated_by='" + employee_gid + "'," +
                " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                " where brsactivity_gid='" + values.brsactivity_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("BRIL");
                msSQL = " insert into brs_mst_tbrsactivityinactivelog(" +
                          " brsactivityinactivelog_gid," +
                          " brsactivity_gid," +
                          " status_log, " +
                          " remarks, " +
                          " created_by, " +
                          " created_date) " +
                          " values(" +
                          "'" + msGetGid + "'," +
                          "'" + values.brsactivity_gid + "'," +
                          "'" + values.status_log + "'," +
                          "'" + values.remarks.Replace("'", "\\'") + "'," +
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
        public void DaGetBRSActivityActiveLog(string brsactivity_gid, MdlBRSMaster objgetsegment)
        {
            try
            {
                msSQL = " SELECT d.brsactivity_name,a.status_log,a.remarks, " +
                    " date_format(a.created_date,'%d-%b-%Y || %h:%i %p') as created_date,concat(c.user_firstname,' ' ,c.user_lastname,' || ',c.user_code) as created_by" +
                    " FROM brs_mst_tbrsactivityinactivelog a" +
                    " left join hrm_mst_temployee b on a.created_by=b.employee_gid" +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                    "  left join brs_mst_tbrsactivity d on a.brsactivity_gid=d.brsactivity_gid where a.brsactivity_gid='" + brsactivity_gid + "'" +
                    " order by a.brsactivityinactivelog_gid desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getSegment = new List<BRSActivity_List>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getSegment.Add(new BRSActivity_List
                        {
                            brsactivity_name = (dr_datarow["brsactivity_name"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                            status_log = (dr_datarow["status_log"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                        });
                    }
                    objgetsegment.BRSActivity_List = getSegment;
                }
                dt_datatable.Dispose();
                objgetsegment.status = true;

            }
            catch
            {
                objgetsegment.status = false;
            }
        }
        public void DaGetBRSActivityStatus(MdlBRSMaster objgetsegment)
        {
            try
            {
                msSQL = " SELECT brsactivity_name,brsactivity_gid " +
                    " FROM brs_mst_tbrsactivity  where status_log='Y'" +              
                    " order by brsactivity_gid desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getSegment = new List<BRSActivity_List>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getSegment.Add(new BRSActivity_List
                        {
                            brsactivity_name = (dr_datarow["brsactivity_name"].ToString()),
                            brsactivity_gid = (dr_datarow["brsactivity_gid"].ToString()),
                        });
                    }
                    objgetsegment.BRSActivity_List = getSegment;
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