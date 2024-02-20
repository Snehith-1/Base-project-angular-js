using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.VisualBasic;
using System.Data;
using System.Data.Odbc;
using ems.hrm.Models;
using ems.utilities.Functions;
namespace ems.hrm.DataAccess
{
    public class daAttendance
    {
        OdbcDataReader objODBCDatareader;
        string strClientIP;
        String msSQL;
        DataTable dt_datatable;
        TimeSpan time1 = new TimeSpan();
        TimeSpan time2 = new TimeSpan();
        TimeSpan lslunch = new TimeSpan();
        TimeSpan lstotal = new TimeSpan();
        int mnResult;
        string msGetGID;
        string lstime;
        string lsholiday;
        DataTable objTblRQ;
        DataTable table;
        Double lscount;
        string employee;
        string attendancelogintmp_gid, attendancelogouttmp_gid;
        DataTable objTblemployee;
        string lsshift_gid;
        string lsflag;
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        public bool iAttendancelogin_da(string employee_gid, mdliAttendance values)
        {
            strClientIP = "";
            

            msSQL = " select a.* " +
                    " from hrm_trn_tattendance a left join hrm_mst_temployee b on a.employee_gid = b.employee_gid  " +
                    "  where b.employee_gid ='" + employee_gid + "' and" +
                    " a.attendance_date ='" + DateTime.Now.ToString("yyyy-MM-dd") + "%'  and b.attendance_flag='Y'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow objtblrow in dt_datatable.Rows)
                {
                    msSQL = " update hrm_trn_tattendance set " +
                            " login_time='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                            " login_time_audit='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                            " employee_attendance='Present'," +
                            " attendance_source='Manual'," +
                            " login_ip='" + strClientIP + "'," +
                            " attendance_type='P'," +
                            " update_flag='N'," +
                            " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
                            " where employee_gid='" + employee_gid + "' and attendance_date='" + DateTime.Now.ToString("yyyy-MM-dd") + "' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            else
            {
                msGetGID = objcmnfunctions.GetMasterGID("HATP");
                msSQL = "Insert Into hrm_trn_tattendance" +
                    "(attendance_gid," +
                    " employee_gid," +
                    " attendance_date," +
                    " attendance_source," +
                    " login_time," +
                    " login_time_audit," +
                    " employee_attendance," +
                    " login_ip, " +
                    " location," +
                    " created_date," +
                    " attendance_type)" +
                    " VALUES ( " +
                    "'" + msGetGID + "', " +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                    "'Manual'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                    "'Present'," +
                    "'" + strClientIP + "'," +
                    "'" + values.location + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                    "'P')";
           mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            
            return true;
        }

        public bool iAttendancelogout_da(string employee_gid, mdliAttendance values)
        {
            strClientIP = "";
            

            msSQL = " update hrm_trn_tattendance set " +
                " logout_time = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                " logout_ip='" + strClientIP + "', " +
                " logout_time_audit = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', " +
                " logout_location='" + values.location + "'," +
                " update_flag='N'" +
                " where employee_gid = '" + employee_gid + "' and " +
                " attendance_date like '" + DateTime.Now.ToString("yyyy-MM-dd") + "%'";
          mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "  Select (time(login_time)) as logintime,(time(logout_time)) as logouttime, " +
                    " (time(lunch_out_scheduled)) as lunch_out_scheduled from hrm_trn_tattendance " +
                    " where employee_gid = '" + employee_gid + "' and " +
                    " attendance_date like '" + DateTime.Now.ToString("yyyy-MM-dd") + "%'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            objODBCDatareader.Read();
            if (objODBCDatareader.HasRows == true)
            {
                time1 = TimeSpan.Parse(objODBCDatareader["logintime"].ToString());
                time2 = TimeSpan.Parse(objODBCDatareader["logouttime"].ToString());
                lslunch = TimeSpan.Parse(objODBCDatareader["lunch_out_scheduled"].ToString());
                lstime = objODBCDatareader["logouttime"].ToString();

                if (time2 > TimeSpan.Parse("14:00:00"))
                {
                    lstotal = time2 - time1;
                }
                else
                {
                    lstotal = time2 - time1;
                    lstotal = lstotal - lslunch;
                }
           
            }
            msSQL = " update hrm_trn_tattendance set " +
                    " total_duration = '" + DateTime.Now.ToString("yyyy-MM-dd") + " " + lstotal.ToString() + "',"+
                    " update_flag='N'" +
                    " where employee_gid = '" + employee_gid + "' and " +
                    " attendance_date like '" + DateTime.Now.ToString("yyyy-MM-dd")  + "%'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            
            return true;
        }

        public bool Attendancelogin_da(string employee_gid, string user_gid,mdlloginreq values)
        {
            

            msSQL = " Select a.holiday_name,b.employee_gid from hrm_mst_tholiday a " +
                    " inner join hrm_mst_tholiday2employee b on a.holiday_gid=b.holiday_gid " +
                    " where a.holiday_date='" + values.loginreq_date.ToString("yyyy-MM-dd") + "'" +
                    " and b.employee_gid='" + employee_gid + "' ";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            objODBCDatareader.Read();
            if (objODBCDatareader.HasRows == true)
            {
                lsholiday = "Y";
            }
            else
            {
                lsholiday = "N";
            }

            msSQL = " select attendance_gid from hrm_trn_tattendance " +
                    " where employee_gid='" + employee_gid + "' and attendance_date='" + values.loginreq_date.ToString("yyyy-MM-dd") + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            objODBCDatareader.Read();
            if (objODBCDatareader.HasRows == true)
            {
               
            }
            else
            {
                values.message = " No records found in Attendance";
                return false;
            }
            msSQL = " select attendancelogintmp_gid from hrm_tmp_tattendancelogin " +
                    " where employee_gid='" + employee_gid + "' and attendance_date='" + values.loginreq_date.ToString("yyyy-MM-dd") + "'";
                
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            objODBCDatareader.Read();
            if (objODBCDatareader.HasRows == true)
            {
                values.message = "Login Time Requisition Already Added";
            }
            else
            {
                msGetGID = "";
                msGetGID = objcmnfunctions.GetMasterGID("HRLR");
                attendancelogintmp_gid = msGetGID;
                msSQL = " Insert into hrm_tmp_tattendancelogin" +
                            " (attendancelogintmp_gid, " +
                            " employee_gid," +
                            " attendance_date," +
                            " login_time, " +
                            " created_by," +
                            " created_date, " +
                            " remarks," +
                            "status) Values  " +
                            " ('" + msGetGID + "', " +
                            " '" + employee_gid + "'," +
                            " '" + values.login_date.ToString("yyyy-MM-dd") + "', " +
                            " '" + values.loginreq_date.ToString("yyyy-MM-dd HH:mm:ss") + " '," +
                            " '" + user_gid + " '," +
                            " '" + DateTime.Now.ToString("yyyy-MM-dd") + " '," +
                            " '" + values.loginreq_reason + "'," +
                            " 'Pending')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                

                objcmnfunctions.PopSummary(employee_gid, user_gid, lscount);
                objTblRQ = objcmnfunctions.foundRow(table);
                lscount = objcmnfunctions.foundcount(lscount);
                
                if (lscount > 0)
                {
                    foreach (DataRow objRow1 in objTblRQ.Rows)
                    {
                        employee = objRow1["employee_gid"].ToString();
                        if ((lscount == 1.0) && (employee == employee_gid))
                        {
                            msSQL = " update hrm_tmp_tattendancelogin set " +
                                " status='Approved' " +
                                " where attendancelogintmp_gid = '" + attendancelogintmp_gid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            if (mnResult != 0)
                            {
                                msSQL = " select attendance_gid from hrm_trn_tattendance " +
                                        " where employee_gid='" + employee_gid + "' and " +
                                        " attendance_date='" + values.login_date.ToString("yyyy-MM-dd") + "'";
                                objTblemployee = objdbconn.GetDataTable(msSQL);
                                if (objTblemployee.Rows.Count != 0)
                                {
                                    foreach (DataRow objtblrow in objTblemployee.Rows)
                                    {
                                        msSQL = " update hrm_trn_tattendance set" +
                                                  " login_time='" + values.login_date.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                                  " login_time_audit='" + values.login_date.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                                  " attendance_source='Requisition'," +
                                                  " approved_by='" + employee_gid + "'," +
                                                  " employee_attendance='Present'," +
                                                  " attendance_type='P'," +
                                                  " update_flag='N' " +
                                                  " where employee_gid='" + employee_gid + "' and " +
                                                  " attendance_date='" + values.login_date.ToString("yyyy-MM-dd") + "'";
                                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                    }
                                }
                                else
                                {
                                    msSQL = " select employee2shifttypedtl_gid from hrm_trn_temployee2shifttypedtl " +
                                            " where employee_gid='" + employee_gid + "' " +
                                            " and employee2shifttype_name='" + values.login_date.DayOfWeek + "' " +
                                            " and shift_status='Y' ";
                                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                    objODBCDatareader.Read();
                                    if (objODBCDatareader.HasRows == true)
                                    {
                                        lsshift_gid = objODBCDatareader["employee2shifttypedtl_gid"].ToString();

                                        msGetGID = objcmnfunctions.GetMasterGID("HATP");
                                        msSQL = "Insert Into hrm_trn_tattendance" +
                                                "(attendance_gid," +
                                                " employee_gid," +
                                                " attendance_date," +
                                                " attendance_source," +
                                                " login_time," +
                                                " login_time_audit, " +
                                                " employee_attendance," +
                                                " shifttype_gid," +
                                                " attendance_type)" +
                                                " VALUES ( " +
                                                "'" + msGetGID + "', " +
                                                "'" + employee_gid + "'," +
                                                "'" + values.login_date.ToString("yyyy-MM-dd") + "'," +
                                                "'Requisition'," +
                                                "'" + values.login_date.ToString("yyyy-MM-dd HH:mms:ss") + "'," +
                                                "'" + values.login_date.ToString("yyyy-MM-dd HH:mms:ss") + "'," +
                                                "'Present'," +
                                                "'" + lsshift_gid + "'," +
                                                "'P')";
                                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                    }

                                }
                            }
                        }
                        else
                        {
                            if (employee != employee_gid)
                            {
                                msGetGID = objcmnfunctions.GetMasterGID("HRLA");
                                msSQL = "insert into hrm_trn_tloginapproval ( " +
                                        " approval_gid, " +
                                        " approved_by, " +
                                        " approved_date, " +
                                        " submodule_gid, " +
                                        " loginapproval_gid " +
                                        " ) values ( " +
                                        " '" + msGetGID + "'," +
                                        " '" + employee + "'," +
                                        " '" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                                        "'HRMATNLTA'," +
                                        "'" + attendancelogintmp_gid + "') ";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);



                                msSQL = " select approved_by from hrm_trn_tloginapproval " +
                                        " where loginapproval_gid='" + attendancelogintmp_gid + "'" +
                                        " and approved_by='" + employee_gid + "'";
                                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                objODBCDatareader.Read();
                                if (objODBCDatareader.HasRows == true)
                                {
                                    lsflag = objODBCDatareader["approved_by"].ToString();
                                }
                                objODBCDatareader.Close();
                                if (lsflag == employee_gid)
                                {
                                    msSQL = "update hrm_trn_tloginapproval set " +
                                    "approval_flag='Y' " +
                                    "where approved_by='" + lsflag + "' and loginapproval_gid = '" + attendancelogintmp_gid + "'";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                    if (mnResult != 0)
                                    {
                                        msSQL = " select attendance_gid from hrm_trn_tattendance " +
                                                " where employee_gid='" + employee_gid + "' and " +
                                                " attendance_date='" + values.login_date.ToString("yyyy-MM-dd") + "'";
                                        dt_datatable = objdbconn.GetDataTable(msSQL);
                                        if (dt_datatable.Rows.Count != 0)
                                        {
                                            foreach (DataRow objtblrow in dt_datatable.Rows)
                                            {
                                                msSQL = " update hrm_trn_tattendance set" +
                                                          " login_time='" + values.login_date.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                                          " login_time_audit='" + values.login_date.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                                          " attendance_source='Requisition'," +
                                                          " approved_by='" + employee_gid + "'," +
                                                          " employee_attendance='Present'," +
                                                          " attendance_type='P'," +
                                                          " update_flag='N' " +
                                                          " where employee_gid='" + employee_gid + "' and " +
                                                          " attendance_date='" + values.login_date.ToString("yyyy-MM-dd") + "'";
                                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                            }
                                        }
                                        else
                                        {
                                            msGetGID = objcmnfunctions.GetMasterGID("HATP");
                                            msSQL = "Insert Into hrm_trn_tattendance" +
                                                    "(attendance_gid," +
                                                    " employee_gid," +
                                                    " attendance_date," +
                                                    " attendance_source," +
                                                    " login_time," +
                                                    " login_time_audit, " +
                                                    " employee_attendance," +
                                                    " shifttype_gid," +
                                                    " attendance_type)" +
                                                    " VALUES ( " +
                                                    "'" + msGetGID + "', " +
                                                    "'" + employee_gid + "'," +
                                                    "'" + values.login_date.ToString("yyyy-MM-dd") + "'," +
                                                    "'Requisition'," +
                                                    "'" + values.login_date.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                                    "'" + values.login_date.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                                    "'Present'," +
                                                    "'" + lsshift_gid + "'," +
                                                    "'P')";
                                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                        }
                                    }
                                    msSQL = " SELECT employeereporting_to FROM adm_mst_tmodule2employee " +
                                " where employee_gid   = '" + employee_gid + "' and module_gid ='HRM' " +
                                " and employeereporting_to='EM1006040001' ";
                                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                    objODBCDatareader.Read();
                                    if (objODBCDatareader.HasRows == true)
                                    {
                                        msSQL = "update hrm_tmp_tattendancelogin set " +
                                            "status='Approved' " +
                                            "where attendancelogintmp_gid = '" + attendancelogintmp_gid + "'";
                                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                    }

                                }
                            }
                        }
                    }
                }
                else
                {
                    msSQL = "update hrm_tmp_tattendancelogin set " +
                                      "status='Approved' " +
                                      "where attendancelogintmp_gid = '" + attendancelogintmp_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnResult != 0)
                    {
                        msSQL = " select attendance_gid from hrm_trn_tattendance " +
                                     " where employee_gid='" + employee_gid + "'" +
                                     " and attendance_date='" + values.login_date.ToString("yyyy-MM-dd") + "'";
                        dt_datatable = objdbconn.GetDataTable(msSQL);
                        if (dt_datatable.Rows.Count != 0)
                        {
                            foreach (DataRow objtblrow in dt_datatable.Rows)
                            {
                                msSQL = " update hrm_trn_tattendance set" +
                                          " login_time='" + values.login_date.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                          " login_time_audit='" + values.login_date.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                          " attendance_source='Requisition'," +
                                          " approved_by='" + employee_gid + "'," +
                                          " employee_attendance='Present'," +
                                          " update_flag='N', " +
                                          " attendance_type='P'" +
                                          " where employee_gid='" + employee_gid + "' and " +
                                          " attendance_date='" + values.login_date.ToString("yyyy-MM-dd") + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }
                        }

                        else
                        {
                            msGetGID = objcmnfunctions.GetMasterGID("HATP");
                            msSQL = "Insert Into hrm_trn_tattendance" +
                                    "(attendance_gid," +
                                    " employee_gid," +
                                    " attendance_date," +
                                    " attendance_source," +
                                    " login_time," +
                                    " login_time_audit, " +
                                    " employee_attendance," +
                                    " attendance_type)" +
                                    " VALUES ( " +
                                    "'" + msGetGID + "', " +
                                    "'" + employee_gid + "'," +
                                    "'" + values.login_date.ToString("yyyy-MM-dd") + "'," +
                                    "'Requisition'," +
                                    "'" + values.login_date.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                    "'" + values.login_date.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                    "'Present'," +
                                    "'P')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                        }
                    }

                }
            return true;

        }

        public bool Attendancelogout_da(string employee_gid, string user_gid, mdllogoutreq values)
        {
            
            msSQL = " select attendancetmp_gid from hrm_tmp_tattendance " +
                    " where employee_gid='" + employee_gid + "'" +
                    " and attendance_date='" + values.logout_date.ToString("yyyy-MM-dd") + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            objODBCDatareader.Read();
            if (objODBCDatareader.HasRows == true)
            {
                values.message = "Logout Time Requisition Already Applied for this date";
                objODBCDatareader.Close();
                
                return false;
            }

            msSQL = " select attendance_gid,time(login_time) as login from hrm_trn_tattendance " +
                     " where attendance_date='" + values.logout_date.ToString("yyyy-MM-dd") + "'" +
                     " and employee_gid='" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            objODBCDatareader.Read();
            if (objODBCDatareader.HasRows == true)
            {
                msGetGID = objcmnfunctions.GetMasterGID("HLGP");
                attendancelogouttmp_gid = msGetGID;
                msSQL = " Insert into hrm_tmp_tattendance" +
                       " (attendancetmp_gid, " +
                       " employee_gid," +
                       " attendance_date," +
                       " logout_time, " +
                       " created_by," +
                       " remarks," +
                       " created_date, " +
                       "status) Values  " +
                       " ('" + msGetGID + "', " +
                       " '" + employee_gid + "'," +
                       " '" + values.logout_date.ToString("yyyy-MM-dd") + "', " +
                       " '" + values.logout_date.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                       " '" + user_gid + "'," +
                       " '" + values.logoutreq_reason + "'," +
                       " '" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                       " 'Pending')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                objcmnfunctions.PopSummary(employee_gid, user_gid, lscount);
                objTblRQ = objcmnfunctions.foundRow(table);
                lscount = objcmnfunctions.foundcount(lscount);

                if (lscount > 0)
                {
                    foreach (DataRow objRow1 in objTblRQ.Rows)
                    {
                        employee = objRow1["employee_gid"].ToString();
                        if ((lscount == 1.0) && (employee == employee_gid))
                        {
                            msSQL = "update hrm_tmp_tattendance set " +
                                         "status='Approved' " +
                                         "where attendancetmp_gid = '" + attendancelogouttmp_gid + "'";

                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            if (mnResult != 0)
                            {
                                msSQL = " update hrm_trn_tattendance set " +
                                     " logout_time = '" + values.logout_date.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                     " logout_time_audit = '" + values.logout_date.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                     " logout_ip = '" + strClientIP + "'," +
                                     " update_flag='N' " +
                                     " where attendance_date='" + values.logout_date.ToString("yyyy-MM-dd") + "'" +
                                     " and employee_gid='" + employee_gid + "'" +
                                     " and login_time is not null";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            }
                        }
                        else
                        {
                            if (employee != employee_gid)
                            {
                                msGetGID = objcmnfunctions.GetMasterGID("HAGP");
                                msSQL = "insert into hrm_trn_tlogoutapproval ( " +
                                        " approval_gid, " +
                                        " approved_by, " +
                                        " approved_date, " +
                                        " submodule_gid, " +
                                        " logoutapproval_gid " +
                                        " ) values ( " +
                                        " '" + msGetGID + "'," +
                                        " '" + employee + "'," +
                                        " '" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                                        "'HRMATNALT'," +
                                        "'" + attendancelogouttmp_gid + "') ";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }
                            msSQL = " select approved_by from hrm_trn_tlogoutapproval " +
                                    " where logoutapproval_gid='" + attendancelogouttmp_gid + "'" +
                                    " and approved_by='" + employee + "'";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            objODBCDatareader.Read();
                            if (objODBCDatareader.HasRows == true)
                            {
                                lsflag = objODBCDatareader["approved_by"].ToString();
                            }
                            objODBCDatareader.Close();
                            if (lsflag == employee_gid)
                            {
                                msSQL = "update hrm_trn_tlogoutapproval set " +
                                        "approval_flag='Y' " +
                                        "where approved_by='" + lsflag + "'" +
                                        " and logoutapproval_gid = '" + attendancelogouttmp_gid + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                if (mnResult != 0)
                                {
                                    msSQL = " update hrm_trn_tattendance set " +
                                         " logout_time = '" + values.logout_date.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                         " logout_time_audit = '" + values.logout_date.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                         " logout_ip = '" + strClientIP + "'," +
                                         " update_flag='N' " +
                                         " where attendance_date='" + values.logout_date.ToString("yyyy-MM-dd") + "'" +
                                         " and employee_gid='" + employee_gid + "'" +
                                         " and login_time is not null";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }
                            msSQL = " SELECT employeereporting_to FROM adm_mst_tmodule2employee " +
                                   " where employee_gid   = '" + employee_gid + "'" +
                                   " and module_gid ='HRM' and employeereporting_to='EM1006040001' ";
                                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                objODBCDatareader.Read();
                                if (objODBCDatareader.HasRows == true)
                                {
                                    msSQL = "update hrm_tmp_tattendance set " +
                                                "status='Approved' " +
                                                "where attendancetmp_gid = '" + attendancelogouttmp_gid + "'";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                }
                                objODBCDatareader.Close();
                             }
                        }
                    }
                }
                else
                {
                    msSQL = "update hrm_tmp_tattendance set " +
                                           "status='Approved' " +
                                           "where attendancetmp_gid = '" + attendancelogouttmp_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnResult != 0)
                    {
                        msSQL = " update hrm_trn_tattendance set " +
                             " logout_time = '" + values.logout_date.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                             " logout_time_audit = '" + values.logout_date.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                             " logout_ip = '" + strClientIP + "', " +
                             " update_flag='N' " +
                             " where attendance_date='" + values.logout_date.ToString("yyyy-MM-dd") + "'" +
                             " and employee_gid='" + employee_gid + "'" +
                             " and login_time is not null";
                          mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
            }
                
            return true;
        }

        public bool loginSummary_da(string employee_gid, mdlloginsummary objloginsummary)
        {
            
            msSQL = " SELECT date_format(created_date,'%d-%m-%Y') as applydate, date_format(attendance_date,'%d-%m-%Y') as attendancedate," +
                    " time_format(login_time, '%H:%i %p') as login,status,remarks from hrm_tmp_tattendancelogin " +
                    " where employee_gid = '" + employee_gid + "'";
         
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getloginsummary = new List<loginsummary_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getloginsummary.Add(new loginsummary_list
                    {

                        applyDate = (dr_datarow["applydate"].ToString()),
                        attendanceDate = (dr_datarow["attendancedate"].ToString()),
                        login_Time = (dr_datarow["login"].ToString()),
                        login_status = (dr_datarow["status"].ToString()),
                        remarks = (dr_datarow["remarks"].ToString())

                    });
                }
                objloginsummary.loginsummary_list = getloginsummary;
            }
            dt_datatable.Dispose();

            
            return true;
        }

        public bool logoutSummary_da(string employee_gid, mdllogoutsummary objlogoutsummary)
        {
            
            msSQL = " SELECT date_format(created_date,'%d-%m-%Y') as applydate, date_format(attendance_date,'%d-%m-%Y') as attendancedate," +
                    " time_format(logout_time, '%H:%i %p') as logout,status,remarks from hrm_tmp_tattendance " +
                    " where employee_gid = '" + employee_gid + "'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getloginsummary = new List<logoutsummary_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getloginsummary.Add(new logoutsummary_list
                    {

                        applyDate = (dr_datarow["applydate"].ToString()),
                        attendanceDate = (dr_datarow["attendancedate"].ToString()),
                        logout_Time = (dr_datarow["logout"].ToString()),
                        logout_status = dr_datarow["status"].ToString(),
                        remarks = dr_datarow["remarks"].ToString()

                    });
                }
                objlogoutsummary.logoutsummary_list = getloginsummary;
            }
            dt_datatable.Dispose();
            
            return true;
        }
    }
           
}
