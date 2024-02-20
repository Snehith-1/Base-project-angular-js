using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Odbc;
using ems.utilities.Functions;
using ems.master.Models;
/// <summary>
/// (It's used for pages in CAD Group Master page)CADGroupAssignment DataAccess Class accessed by API methods from related Controller class and is returning relevant response to client.
/// </summary>
/// <remarks>Written by Sumala,Logapriya and Abilash</remarks>
namespace ems.master.DataAccess
{
    public class DaCADGroupAssignment
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        DataTable dt_datatable;
        OdbcDataReader objODBCDatareader;
        string msSQL, msGetGid, msGetmaker_gid, msGetapprover_gid, msGetAPICode;
        int mnResult, msResult;
        Random rand = new Random();
        string msGetchecker_gid;

        //Add
        public void DaPostCADGroupAssign(MdlMstCADGroupAssignment values, string employee_gid)
        {
            msSQL = "select cadgroupassign_gid from ocs_mst_tcadgroupassignment where cadgroup_name = '" + values.cadgroup_name.Replace("'", "\\'") + "'";
            string lsdocumentgid = objdbconn.GetExecuteScalar(msSQL);
            if (lsdocumentgid != "")
            {
                //if (lsdocumentgid != values.cadgroupassign_gid)
                //{
                values.message = " This CAD Group Assign Already Exists";
                    values.status = false;
                    return;
                //}
            }
            msGetAPICode = objcmnfunctions.GetApiMasterGID("GASC");
            msGetGid = objcmnfunctions.GetMasterGID("CGAG");
            msSQL = " insert into ocs_mst_tcadgroupassignment(" +
                    " cadgroupassign_gid ," +
                    " api_code ," +
                    " vertical_gid ," +
                    " vertical_name ," +
                    " program_name ," +
                    " program_gid ," +
                    " cadgroup_name ," +
                    " cadgroup_gid ," +
                    //" menu_name ," +
                    //" menu_gid ," +
                    " created_by , " +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + msGetAPICode + "'," +
                    "'" + values.vertical_gid + "'," +
                    "'" + values.vertical_name.Replace("'", "") + "'," +
                    "'" + values.program_name.Replace("'", "") + "'," +
                    "'" + values.program_gid + "'," +
                    "'" + values.cadgroup_name.Replace("'", "") + "'," +
                    "'" + values.cadgroup_gid + "'," +
                    //"'" + values.menu_name.Replace("'", "") + "'," +
                    //"'" + values.menu_gid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            //for (var i = 0; i < values.maker.Count; i++)
            //{
            //    msGetmaker_gid = objcmnfunctions.GetMasterGID("CGAM");
            //    msSQL = "Insert into ocs_mst_tcadgroupmaker( " +
            //           " cadgroupmaker_gid, " +
            //           " cadgroupassign_gid," +
            //           " employee_gid," +
            //           " employee_name," +
            //           " created_by," +
            //           " created_date)" +
            //           " values(" +
            //           "'" + msGetmaker_gid + "'," +
            //           "'" + msGetGid + "'," +
            //           "'" + values.maker[i].employee_gid + "'," +
            //           "'" + values.maker[i].employee_name + "'," +
            //           "'" + employee_gid + "'," +
            //           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            //    msResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            //}

            //for (var i = 0; i < values.checker.Count; i++)
            //{
            //    msGetchecker_gid = objcmnfunctions.GetMasterGID("CGAC");
            //    msSQL = "Insert into ocs_mst_tcadgroupchecker( " +
            //           " cadgroupchecker_gid, " +
            //           " cadgroupassign_gid," +
            //           " employee_gid," +
            //           " employee_name," +
            //           " created_by," +
            //           " created_date)" +
            //           " values(" +
            //           "'" + msGetchecker_gid + "'," +
            //           "'" + msGetGid + "'," +
            //           "'" + values.checker[i].employee_gid + "'," +
            //           "'" + values.checker[i].employee_name + "'," +
            //           "'" + employee_gid + "'," +
            //           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            //    msResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            //}
            //for (var i = 0; i < values.approver.Count; i++)
            //{
            //    msGetapprover_gid = objcmnfunctions.GetMasterGID("CGAA");
            //    msSQL = "Insert into ocs_mst_tcadgroupapprover( " +
            //           " cadgroupapprover_gid, " +
            //           " cadgroupassign_gid," +
            //           " employee_gid," +
            //           " employee_name," +
            //           " created_by," +
            //           " created_date)" +
            //           " values(" +
            //           "'" + msGetapprover_gid + "'," +
            //           "'" + msGetGid + "'," +
            //           "'" + values.approver[i].employee_gid + "'," +
            //           "'" + values.approver[i].employee_name + "'," +
            //           "'" + employee_gid + "'," +
            //           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            //    msResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            //}

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "CAD Group Assignment Added successfully";
            }
            else
            {
                values.message = "Error Occured while Adding";
                values.status = false;
            }
        }
        //Menu
        public void DaGetMenu(MdlMstCADGroupAssignment objmenu)
        {
            try
            {
                msSQL = "select module_code as menu_gid,module_name as menu_name from adm_mst_tmodule where module_gid like '%CADMGT%' and menu_level = '3'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var menu_list = new List<MdlCADmenulist>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        menu_list.Add(new MdlCADmenulist
                        {
                            menu_gid = (dr_datarow["menu_gid"].ToString()),
                            menu_name = (dr_datarow["menu_name"].ToString()),
                        });
                    }
                    objmenu.menu_list= menu_list;
                }
                dt_datatable.Dispose();
                objmenu.status = true;
            }
            catch (Exception ex)
            {
                objmenu.status = false;
            }
        }
        //Summary
        public void DaGetCADGroupAssignmentSummary(MdlMstCADGroupAssignment objmaster)
        {
            try
            {
                msSQL = " SELECT a.cadgroupassign_gid, a.vertical_name ,a.program_name, a.cadgroup_name, a.menu_name, a.approver_name, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, api_code " +
                        " FROM ocs_mst_tcadgroupassignment a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid where delete_flag <> 'Y' order by a.created_date desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcadgroup_list = new List<MdlCADGroupAssignment>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcadgroup_list.Add(new MdlCADGroupAssignment
                        {
                            cadgroupassign_gid = (dr_datarow["cadgroupassign_gid"].ToString()),
                            vertical_name = (dr_datarow["vertical_name"].ToString()),
                            program_name = (dr_datarow["program_name"].ToString()),
                            menu_name = (dr_datarow["menu_name"].ToString()),
                            cadgroup_name = (dr_datarow["cadgroup_name"].ToString()),
                            approver_name = (dr_datarow["approver_name"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            api_code = (dr_datarow["api_code"].ToString()),
                        });
                    }
                    objmaster.group_list = getcadgroup_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch (Exception ex)
            {
                objmaster.status = false;
            }
        }
        //Edit
        public void DaGetCADGroupAssignmentEdit(string cadgroupassign_gid, MdlMstCADGroupAssignment objmaster)
        {
            msSQL = "SELECT cadgroupassign_gid, vertical_gid, program_gid, cadgroup_gid, menu_gid " +
                    " FROM ocs_mst_tcadgroupassignment " + 
                    " where cadgroupassign_gid='" + cadgroupassign_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objmaster.cadgroupassign_gid = objODBCDatareader["cadgroupassign_gid"].ToString();
                objmaster.vertical_gid = objODBCDatareader["vertical_gid"].ToString();
                objmaster.program_gid = objODBCDatareader["program_gid"].ToString();
                objmaster.cadgroup_gid = objODBCDatareader["cadgroup_gid"].ToString();
                //objmaster.menu_gid = objODBCDatareader["menu_gid"].ToString();
                //objmaster.approver_gid = objODBCDatareader["approver_gid"].ToString();

            }
            objODBCDatareader.Close();
            //msSQL = " select cadgroupmaker_gid,employee_gid,employee_name from ocs_mst_tcadgroupmaker " +
            //      " where cadgroupassign_gid='" + cadgroupassign_gid + "'";
            //dt_datatable = objdbconn.GetDataTable(msSQL);
            //var getcadmakerList = new List<Cadmaker>();
            //if (dt_datatable.Rows.Count != 0)
            //{
            //    foreach (DataRow dt in dt_datatable.Rows)
            //    {
            //        getcadmakerList.Add(new Cadmaker
            //        {
            //            cadgroupmaker_gid = dt["cadgroupmaker_gid"].ToString(),
            //            employee_gid = dt["employee_gid"].ToString(),
            //            employee_name = dt["employee_name"].ToString(),
            //        });
            //        objmaster.maker = getcadmakerList;
            //    }
            //}
            //dt_datatable.Dispose();
            //msSQL = " select cadgroupchecker_gid,employee_gid,employee_name from ocs_mst_tcadgroupchecker " +
            //      " where cadgroupassign_gid='" + cadgroupassign_gid + "'";
            //dt_datatable = objdbconn.GetDataTable(msSQL);
            //var getcadcheckerList = new List<Cadchecker>();
            //if (dt_datatable.Rows.Count != 0)
            //{
            //    foreach (DataRow dt in dt_datatable.Rows)
            //    {
            //        getcadcheckerList.Add(new Cadchecker
            //        {
            //            cadgroupchecker_gid = dt["cadgroupchecker_gid"].ToString(),
            //            employee_gid = dt["employee_gid"].ToString(),
            //            employee_name = dt["employee_name"].ToString(),
            //        });
            //        objmaster.checker = getcadcheckerList;
            //    }
            //}
            //dt_datatable.Dispose();
            //msSQL = " select cadgroupapprover_gid,employee_gid,employee_name from ocs_mst_tcadgroupapprover " +
            //     " where cadgroupassign_gid='" + cadgroupassign_gid + "'";
            //dt_datatable = objdbconn.GetDataTable(msSQL);
            //var getcadapproverList = new List<cadapprover>();
            //if (dt_datatable.Rows.Count != 0)
            //{
            //    foreach (DataRow dt in dt_datatable.Rows)
            //    {
            //        getcadapproverList.Add(new cadapprover
            //        {
            //            cadgroupapprover_gid = dt["cadgroupapprover_gid"].ToString(),
            //            employee_gid = dt["employee_gid"].ToString(),
            //            employee_name = dt["employee_name"].ToString(),
            //        });
            //        objmaster.approver = getcadapproverList;
            //    }
            //}
            //dt_datatable.Dispose();

        }
        //Update
        public void DaCADGroupAssignedUpdate(string employee_gid, MdlMstCADGroupAssignment values)
        {
            msSQL = "select cadgroupassign_gid from ocs_mst_tcadgroupassignment where cadgroup_name = '" + values.cadgroup_name.Replace("'", "\\'") + "'";
            string lsdocumentgid = objdbconn.GetExecuteScalar(msSQL);
            if (lsdocumentgid != "")
            {
                if (lsdocumentgid != values.cadgroupassign_gid)
                {
                    values.message = " This CAD Group Assign Already Exists";
                    values.status = false;
                    return;
                }
            }

            msSQL = "select vertical_gid,vertical_name,program_name,program_gid,cadgroup_name, cadgroup_gid, menu_name,menu_gid, " +
                " updated_by,updated_date from ocs_mst_tcadgroupassignment " + 
                " where  cadgroupassign_gid  ='" + values.cadgroupassign_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                string lsUpdatedBy = objODBCDatareader["updated_by"].ToString();
                string lsUpdatedDate = objODBCDatareader["updated_date"].ToString();

                if (!(String.IsNullOrEmpty(lsUpdatedBy)) && !(String.IsNullOrEmpty(lsUpdatedDate)))
                {

                    msGetGid = objcmnfunctions.GetMasterGID("CGAU");
                    msSQL = " insert into ocs_mst_tcadgroupassignlog(" +
                            " cadgroupassignlog_gid," +
                            " cadgroupassign_gid ," +
                            " vertical_gid ," +
                            " vertical_name ," +
                            " program_name ," +
                            " program_gid ," +
                            " cadgroup_name ," +
                            " cadgroup_gid ," +
                            //" menu_name ," +
                            //" menu_gid ," +
                            " created_by , " +
                            " created_date)" +
                            " values(" +
                            "'" + msGetGid + "'," +
                            "'" + values.cadgroupassign_gid + "'," +
                            "'" + objODBCDatareader["vertical_gid"].ToString().Replace("'", "") + "'," +
                            "'" + objODBCDatareader["vertical_name"].ToString().Replace("'", "") + "'," +
                            "'" + objODBCDatareader["program_name"].ToString().Replace("'", "") + "'," +
                            "'" + objODBCDatareader["program_gid"].ToString().Replace("'", "") + "'," +
                            "'" + objODBCDatareader["cadgroup_name"].ToString().Replace("'", "") + "'," +
                            "'" + objODBCDatareader["cadgroup_gid"].ToString().Replace("'", "") + "'," +
                            //"'" + objODBCDatareader["menu_name"].ToString().Replace("'", "") + "'," +
                            //"'" + objODBCDatareader["menu_gid"].ToString().Replace("'", "") + "'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            objODBCDatareader.Close();

            msSQL = "update ocs_mst_tcadgroupassignment set vertical_name='" + values.vertical_name + "'," +
                     " vertical_gid='" + values.vertical_gid + "'," +
                     " program_name='" + values.program_name + "'," +
                     " program_gid='" + values.program_gid + "'," +
                     " cadgroup_name='" + values.cadgroup_name + "'," + 
                     " cadgroup_gid='" + values.cadgroup_gid + "'," +
                     //" menu_name='" + values.menu_name + "'," +
                     //" menu_gid='" + values.menu_gid + "'," +
                     //" approver_name='" + values.approver_name + "'," +
                     //" approver_gid='" + values.approver_gid + "'," +
                     " updated_by='" + employee_gid + "'," +
                     " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                     " where cadgroup_gid='" + values.cadgroup_gid + "' ";
            msResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            //msSQL = " delete from ocs_mst_tcadgroupmaker where cadgroupassign_gid ='" + values.cadgroupassign_gid + "'";
            //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            //if (mnResult != 0)
            //{
            //    for (var i = 0; i < values.maker.Count; i++)
            //    {
            //        msGetmaker_gid = objcmnfunctions.GetMasterGID("CGAM");
            //        msSQL = "Insert into ocs_mst_tcadgroupmaker( " +
            //               " cadgroupmaker_gid, " +
            //               " cadgroupassign_gid," +
            //               " employee_gid," +
            //               " employee_name," +
            //               " created_by," +
            //               " created_date)" +
            //               " values(" +
            //               "'" + msGetmaker_gid + "'," +
            //               "'" + values.cadgroupassign_gid + "'," +
            //               "'" + values.maker[i].employee_gid + "'," +
            //               "'" + values.maker[i].employee_name + "'," +
            //               "'" + employee_gid + "'," +
            //               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            //        msResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            //    }
            //}
            //msSQL = " delete from ocs_mst_tcadgroupchecker where cadgroupassign_gid ='" + values.cadgroupassign_gid + "'";
            //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            //if (mnResult != 0)
            //{
            //    for (var i = 0; i < values.checker.Count; i++)
            //    {
            //        msGetchecker_gid = objcmnfunctions.GetMasterGID("CGAC");
            //        msSQL = "Insert into ocs_mst_tcadgroupchecker( " +
            //               " cadgroupchecker_gid, " +
            //               " cadgroupassign_gid," +
            //               " employee_gid," +
            //               " employee_name," +
            //               " created_by," +
            //               " created_date)" +
            //               " values(" +
            //               "'" + msGetchecker_gid + "'," +
            //               "'" + values.cadgroupassign_gid + "'," +
            //               "'" + values.checker[i].employee_gid + "'," +
            //               "'" + values.checker[i].employee_name + "'," +
            //               "'" + employee_gid + "'," +
            //               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            //        msResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            //    }
            //    msSQL = " delete from ocs_mst_tcadgroupapprover where cadgroupassign_gid ='" + values.cadgroupassign_gid + "'";
            //    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            //    if (mnResult != 0)
            //    {
            //        for (var i = 0; i < values.approver.Count; i++)
            //        {
            //            msGetapprover_gid = objcmnfunctions.GetMasterGID("CGAA");
            //            msSQL = "Insert into ocs_mst_tcadgroupapprover( " +
            //                   " cadgroupapprover_gid, " +
            //                   " cadgroupassign_gid," +
            //                   " employee_gid," +
            //                   " employee_name," +
            //                   " created_by," +
            //                   " created_date)" +
            //                   " values(" +
            //                   "'" + msGetapprover_gid + "'," +
            //                   "'" + msGetGid + "'," +
            //                   "'" + values.approver[i].employee_gid + "'," +
            //                   "'" + values.approver[i].employee_name + "'," +
            //                   "'" + employee_gid + "'," +
            //                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            //            msResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            //        }
            //    }
            //}
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "CAD Group Assignment Updated successfully";
                //msSQL = " delete from ocs_mst_tcadgroupmaker where cadgroupassign_gid ='" + values.cadgroupassign_gid + "'";
                //msSQL = " delete from ocs_mst_tcadgroupapprover where cadgroupassign_gid ='" + values.cadgroupassign_gid + "'";
                //msSQL = " delete from ocs_mst_tcadgroupchecker where cadgroupassign_gid ='" + values.cadgroupassign_gid + "'";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while updating";
            }
        }
        //Popup
        public void DaGetCADGroupMaker(string cadgroupassign_gid, Cadmaker values)
        {
            msSQL = " select group_concat(employee_name) as employee_name  from ocs_mst_tcadgroupmaker " +
                  " where cadgroupassign_gid='" + cadgroupassign_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.cadgroupmaker = objODBCDatareader["employee_name"].ToString();
            }
            objODBCDatareader.Close();
        }

        public void DaGetCADGroupChecker(string cadgroupassign_gid, Cadchecker values)
        {
            msSQL = " select group_concat(employee_name) as employee_name  from ocs_mst_tcadgroupchecker " +
                  " where cadgroupassign_gid='" + cadgroupassign_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.cadgroupchecker = objODBCDatareader["employee_name"].ToString();
            }
            objODBCDatareader.Close();
        }

        public void DaGetCADGroupApprover(string cadgroupassign_gid, cadapprover values)
        {
            msSQL = " select group_concat(employee_name) as employee_name  from ocs_mst_tcadgroupapprover " +
                  " where cadgroupassign_gid='" + cadgroupassign_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.cadgroupapprover = objODBCDatareader["employee_name"].ToString();
            }
            objODBCDatareader.Close();
        }

        //Delete
        public void DaDeleteCADGroupAssignment(string cadgroupassign_gid, string employee_gid, MdlMstCADGroupAssignment values)
        {
            msSQL = "update ocs_mst_tcadgroupassignment set delete_flag='Y'," +
                     " deleted_by='" + employee_gid + "'," +
                     " deleted_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                     " where cadgroupassign_gid='" + cadgroupassign_gid + "' ";
            
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if(mnResult == 1) { 
                values.status = true;
                values.message = "CAD Group Assignment Deleted Successfully..!";

            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
        }
    }
}