﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Data;
using ems.hrm.Models;
using System.Data.Odbc;
using ems.utilities.Functions;
using System.Web.Script.Serialization;
using System.Text;

namespace ems.hrm.DataAccess
{
    public class DaHrmDashboard
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        OdbcConnection objODBCconnection;
        OdbcDataReader objODBCDatareader, objODBCDatareader1, objODBCDatareader2;
        DataTable dt_datatable;
        string msSQL;
        string msGetGID, lsdate, msGetdtlGID, gid;
        int mnResult;
        string strClientIP;
        TimeSpan time1 = new TimeSpan();
        TimeSpan time2 = new TimeSpan();
        TimeSpan lslunch = new TimeSpan();
        TimeSpan lstotal = new TimeSpan();
        string lstime;
        string lsholiday, lsemployee, hierary_level;
        DataSet ds_tPR = new DataSet();
        string lsgid = "";
        int i = 1, MailFlag;
        string lsEmployee_Gid_list;
        Double lscount;
        DataTable objTblRQ;
        DataTable table;
        string employee;
        string attendancelogintmp_gid, attendancelogouttmp_gid;
        DataTable objTblemployee;
        string lsshift_gid;
        string lsflag;
        DateTime lsfromtime, lstotime;
        TimeSpan lstotaltime;
        string lblEmployeeGID_whatsapp, lsmobile_no, lsmessage, lsapproved_by;
        string lsname, lsupdate_flag;
        string lswhatsappflag, lsemployeeGID, supportmail, emailpassword;
        string employee_mailid, employeename, fromhours, attendance_date;
        string message, lsapprovedby, applied_by;
        TimeSpan lstotalpermissiontime;
        TimeSpan lspermissiontime;
        String totalhr, totalmin, msGetGIDpermission;

        public bool DaIAttendencePunchOut(string employee_gid, mdliAttendance values)
        {
            try
            {
                msSQL = "select login_time_audit,update_flag from hrm_trn_tattendance a " +
                   "where a.employee_gid = '" + employee_gid + "' and a.attendance_date = '" + DateTime.Now.ToString("yyyy-MM-dd") + "%'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.login_time_audit = objODBCDatareader["login_time_audit"].ToString();
                    lsupdate_flag = objODBCDatareader["update_flag"].ToString();
                    if (lsupdate_flag == "")
                    {
                        values.update_flag = "Y";
                    }
                    else
                    {
                        values.update_flag = "N";
                    }
                }
                else
                {

                    values.message = "true";
                }
                objODBCDatareader.Close();

                msSQL = "select iattendance_flag from adm_mst_tcompany where 1=1";
                values.iattendence_privilege = objdbconn.GetExecuteScalar(msSQL);

                values.status = true;
                return true;
            }
            catch
            {
                values.status = false;
                return false;
            }
        }

        public bool DaPostIAttendanceLogin(string employee_gid, mdliAttendance values)
        {
            try
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


                if (mnResult != 0)
                {
                    values.status = true;
                    return true;
                }
                else
                {
                    values.status = false;
                    return false;
                }
            }
            catch
            {
                values.status = false;
                return false;
            }
        }

        public bool DaPostIAttendanceLogout(string employee_gid, mdliAttendance values)
        {
           try
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
                objODBCDatareader.Close();
                msSQL = " update hrm_trn_tattendance set " +
                        " total_duration = '" + DateTime.Now.ToString("yyyy-MM-dd") + " " + lstotal.ToString() + "'," +
                        " update_flag='N'" +
                        " where employee_gid = '" + employee_gid + "' and " +
                        " attendance_date like '" + DateTime.Now.ToString("yyyy-MM-dd") + "%'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    values.status = true;
                    return true;
                }
                else
                {
                    values.status = false;
                    return false;
                }
            }
            catch
            {
                values.status = false;
                return false;
            }
        }

        public bool DaPostAttendanceLogin(string employee_gid, string user_gid, mdlloginreq values)
        {
            try
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
                objODBCDatareader.Close();
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
                objODBCDatareader.Close();
                msSQL = " select attendancelogintmp_gid from hrm_tmp_tattendancelogin " +
                        " where employee_gid='" + employee_gid + "' and attendance_date='" + values.loginreq_date.ToString("yyyy-MM-dd") + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                objODBCDatareader.Read();
                if (objODBCDatareader.HasRows == true)
                {
                    values.message = "Login Time Requisition Already Added";
                    return false;
                }
                else
                {
                    objODBCDatareader.Close();
                    msGetGID = objcmnfunctions.GetMasterGID("HRLR");
                    attendancelogintmp_gid = msGetGID;
                    string lstotalhours = values.login_date.ToString("yyyy-MM-dd") + " " + values.logintime.ToString("HH:mm:ss");
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
                                " '" + lstotalhours + " '," +
                                " '" + user_gid + " '," +
                                " '" + DateTime.Now.ToString("yyyy-MM-dd") + " '," +
                                " '" + values.loginreq_reason.Replace("'", "") + "'," +
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
                            hierary_level = objRow1["hierarchy_level"].ToString();
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
                                            objODBCDatareader.Close();
                                            msSQL = " update hrm_trn_tattendance set" +
                                                      " login_time='" + values.logintime.ToString("yyyy-MM-dd HH.mm.ss") + "'," +
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
                                        objODBCDatareader.Close();
                                        msSQL = " select employee2shifttypedtl_gid from hrm_trn_temployee2shifttypedtl " +
                                                " where employee_gid='" + employee_gid + "' " +
                                                " and employee2shifttype_name='" + values.login_date.DayOfWeek + "' " +
                                                " and shift_status='Y' ";
                                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                        objODBCDatareader.Read();
                                        if (objODBCDatareader.HasRows == true)
                                        {
                                            lsshift_gid = objODBCDatareader["employee2shifttypedtl_gid"].ToString();
                                            objODBCDatareader.Read();
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
                                                    "'" + values.logintime.ToString("yyyy-MM-dd HH.mm.ss") + "'," +
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
                                            " seqhierarchy_view, " +
                                            " hierary_level, " +
                                            " submodule_gid, " +
                                            " loginapproval_gid " +
                                            " ) values ( " +
                                            " '" + msGetGID + "'," +
                                            " '" + employee + "'," +
                                            " '" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                                            " 'N', " +
                                            " '" + hierary_level + "' , " +
                                            "'HRMATNLTA'," +
                                            "'" + attendancelogintmp_gid + "') ";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                                    objODBCDatareader.Close();
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
                                                    objODBCDatareader.Close();
                                                    msSQL = " update hrm_trn_tattendance set" +
                                                              " login_time='" + values.logintime.ToString("yyyy-MM-dd HH.mm.ss") + "'," +
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
                                                objODBCDatareader.Close();
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
                                                        "'" + values.logintime.ToString("yyyy-MM-dd HH.mm.ss") + "'," +
                                                        "'" + values.login_date.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                                        "'Present'," +
                                                        "'" + lsshift_gid + "'," +
                                                        "'P')";
                                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                            }
                                        }
                                        objODBCDatareader.Close();
                                        msSQL = " SELECT employeereporting_to FROM adm_mst_tmodule2employee " +
                                    " where employee_gid   = '" + employee_gid + "' and module_gid ='HRM' " +
                                    " and employeereporting_to='EM1006040001' ";
                                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                        objODBCDatareader.Read();
                                        if (objODBCDatareader.HasRows == true)
                                        {
                                            objODBCDatareader.Close();
                                            msSQL = "update hrm_tmp_tattendancelogin set " +
                                                "status='Approved' " +
                                                "where attendancelogintmp_gid = '" + attendancelogintmp_gid + "'";
                                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                        }

                                    }
                                }
                            }
                        }

                        msSQL = "update hrm_trn_tloginapproval set " +
                     "seqhierarchy_view='Y' " +
                     "where approval_flag='N'and approved_by <> '" + employee_gid + "' " +
                     "and loginapproval_gid = '" + attendancelogintmp_gid + "'" +
                     "order by hierary_level desc limit 1";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    else
                    {
                        objODBCDatareader.Close();
                        msSQL = "update hrm_tmp_tattendancelogin set " +
                                          "status='Approved' " +
                                          "where attendancelogintmp_gid = '" + attendancelogintmp_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult != 0)
                        {
                            objODBCDatareader.Close();
                            msSQL = " select attendance_gid from hrm_trn_tattendance " +
                                         " where employee_gid='" + employee_gid + "'" +
                                         " and attendance_date='" + values.login_date.ToString("yyyy-MM-dd") + "'";
                            dt_datatable = objdbconn.GetDataTable(msSQL);
                            if (dt_datatable.Rows.Count != 0)
                            {
                                foreach (DataRow objtblrow in dt_datatable.Rows)
                                {
                                    objODBCDatareader.Close();
                                    msSQL = " update hrm_trn_tattendance set" +
                                              " login_time='" + values.logintime.ToString("yyyy-MM-dd HH.mm.ss") + "'," +
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
                                objODBCDatareader.Close();
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
                                        "'" + values.logintime.ToString("yyyy-MM-dd HH.mm.ss") + "'," +
                                        "'" + values.login_date.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                        "'Present'," +
                                        "'P')";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }
                        }
                    }
                }
                if (mnResult == 1)
                {
                    msSQL = "Select approved_by from hrm_trn_tloginapproval where loginapproval_gid= '" + attendancelogintmp_gid + "' and approval_flag<>'Y'";
                    objTblRQ = objdbconn.GetDataTable(msSQL);
                    foreach (DataRow dt in objTblRQ.Rows)
                    {
                        lsapprovedby = dt["approved_by"].ToString();

                        msSQL = "select pop_username,pop_password from adm_mst_tcompany";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            supportmail = objODBCDatareader["pop_username"].ToString();
                            emailpassword = objODBCDatareader["pop_password"].ToString();
                        }
                        objODBCDatareader.Close();
                        if (supportmail != "")
                        {
                            msSQL = " select b.employee_emailid, date_format(a.login_time,'%H:%i %p') as login_time," +
                                    " concat(c.user_firstname,' ',c.user_lastname) as username," +
                                    " date_format(a.attendance_date,'%d-%m-%Y') as attendance_date from hrm_tmp_tattendancelogin a" +
                                    " left join hrm_trn_tloginapproval d on a.attendancelogintmp_gid=d.loginapproval_gid" +
                                    " left join hrm_mst_temployee b on d.approved_by=b.employee_gid " +
                                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                                    " where a.attendancelogintmp_gid='" + attendancelogintmp_gid + "' and b.employee_gid='" + lsapprovedby + "'";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                employee_mailid = objODBCDatareader["employee_emailid"].ToString();
                                employeename = objODBCDatareader["username"].ToString();
                                fromhours = objODBCDatareader["login_time"].ToString();
                                attendance_date = objODBCDatareader["attendance_date"].ToString();
                            }
                            objODBCDatareader.Close();
                            if (employee_mailid != "")
                            {
                                msSQL = " select a.created_by,Concat(c.user_firstname,' ',c.user_lastname) as username " +
                                        " from hrm_tmp_tattendancelogin a " +
                                        " left join adm_mst_tuser c on a.created_by=c.user_gid " +
                                        " where a.attendancelogintmp_gid='" + attendancelogintmp_gid + "'";
                                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                if (objODBCDatareader.HasRows == true)
                                {
                                    applied_by = objODBCDatareader["username"].ToString();
                                }
                                objODBCDatareader.Close();

                                message = "Hi Sir/Madam,  <br />";
                                message = message + "<br />";
                                message = message + "I have applied for Login Time Requisition on " + attendance_date + "<br />";
                                message = message + "<br />";
                                message = message + "<b>Actual Login Time:</b> " + fromhours + "<br />";
                                message = message + "<br />";
                                message = message + " Thanks and Regards  <br />";
                                message = message + " " + applied_by + " <br />";

                                try
                                {
                                    MailFlag = objcmnfunctions.SendSMTP2(supportmail, emailpassword, employee_mailid, "" + applied_by + " applied for Login Time Requisition", message, "", "", "");
                                }

                                catch (Exception ex)
                                {
                                    objTblRQ.Dispose();
                                    values.message = ex.Message;
                                    return false;
                                }
                                objTblRQ.Dispose();
                            }
                        }
                    }
                    objODBCDatareader.Close();
                    values.status = true;
                    values.message = "Login Time Requisition Applied Successfully";
                    return true;
                }

                else
                {
                    values.status = false;
                    values.message = "Error Occurred While Updating Login Time Requisition";
                    return false;
                }

            }
            catch
            {
                values.status = false;
                values.message = "Error Occurred While Updating Login Time Requisition";
                return false;
            }
        }

        public bool DaPostAttendanceLogout(string employee_gid, string user_gid, mdllogoutreq values)
        {
            try
            {
                msSQL = " select attendancetmp_gid from hrm_tmp_tattendance " +
                   " where employee_gid='" + employee_gid + "'" +
                   " and attendance_date='" + values.logoutattendence_date.ToString("yyyy-MM-dd") + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                objODBCDatareader.Read();
                if (objODBCDatareader.HasRows == true)
                {
                    values.message = "Logout Time Requisition Already Applied for this date";
                    objODBCDatareader.Close();

                    return false;
                }
                objODBCDatareader.Close();
                msSQL = " select attendance_gid,time(login_time) as login from hrm_trn_tattendance " +
                         " where attendance_date='" + values.logoutattendence_date.ToString("yyyy-MM-dd") + "'" +
                         " and employee_gid='" + employee_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                objODBCDatareader.Read();
                if (objODBCDatareader.HasRows == true)
                {
                    string lstotalhours = values.logoutattendence_date.ToString("yyyy-MM-dd") + " " + values.logouttime.ToString("HH:mm:ss");
                    objODBCDatareader.Close();
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
                           " '" + values.logoutattendence_date.ToString("yyyy-MM-dd") + "', " +
                           " '" + lstotalhours + "'," +
                           " '" + user_gid + "'," +
                           " '" + values.logouttime_reason.Replace("'", "") + "'," +
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
                            hierary_level = objRow1["hierarchy_level"].ToString();
                            if ((lscount == 1.0) && (employee == employee_gid))
                            {
                                msSQL = "update hrm_tmp_tattendance set " +
                                             "status='Approved' " +
                                             "where attendancetmp_gid = '" + attendancelogouttmp_gid + "'";

                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                if (mnResult != 0)
                                {
                                    msSQL = " update hrm_trn_tattendance set " +
                                         " logout_time = '" + values.logouttime.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                         " logout_time_audit = '" + values.logouttime.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                         " logout_ip = '" + strClientIP + "'," +
                                         " update_flag='N' " +
                                         " where attendance_date='" + values.logoutattendence_date.ToString("yyyy-MM-dd") + "'" +
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
                                            " seqhierarchy_view, " +
                                            " hierary_level, " +
                                            " submodule_gid, " +
                                            " logoutapproval_gid " +
                                            " ) values ( " +
                                            " '" + msGetGID + "'," +
                                            " '" + employee + "'," +
                                            " '" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                                            " 'N', " +
                                            " '" + hierary_level + "' , " +
                                            "'HRMATNALT'," +
                                            "'" + attendancelogouttmp_gid + "') ";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                }
                                objODBCDatareader.Close();
                                msSQL = " select approved_by from hrm_trn_tlogoutapproval " +
                                        " where logoutapproval_gid='" + attendancelogouttmp_gid + "'" +
                                        " and approved_by='" + employee + "'";
                                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                objODBCDatareader.Read();
                                if (objODBCDatareader.HasRows == true)
                                {
                                    lsflag = objODBCDatareader["approved_by"].ToString();
                                    objODBCDatareader.Close();
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
                                             " logout_time = '" + values.logouttime.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                             " logout_time_audit = '" + values.logouttime.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                             " logout_ip = '" + strClientIP + "'," +
                                             " update_flag='N' " +
                                             " where attendance_date='" + values.logoutattendence_date.ToString("yyyy-MM-dd") + "'" +
                                             " and employee_gid='" + employee_gid + "'" +
                                             " and login_time is not null";
                                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                    }
                                    objODBCDatareader.Close();
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

                        msSQL = "update hrm_trn_tlogoutapproval set " +
                       "seqhierarchy_view='Y' " +
                       "where approval_flag='N'and approved_by <> '" + employee_gid + "' " +
                       "and logoutapproval_gid = '" + attendancelogouttmp_gid + "'" +
                       "order by hierary_level desc limit 1";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    else
                    {
                        objODBCDatareader.Close();
                        msSQL = "update hrm_tmp_tattendance set " +
                                               "status='Approved' " +
                                               "where attendancetmp_gid = '" + attendancelogouttmp_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult != 0)
                        {
                            objODBCDatareader.Close();
                            msSQL = " update hrm_trn_tattendance set " +
                                 " logout_time = '" + values.logouttime.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                 " logout_time_audit = '" + values.logouttime.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                 " logout_ip = '" + strClientIP + "', " +
                                 " update_flag='N' " +
                                 " where attendance_date='" + values.logouttime.ToString("yyyy-MM-dd") + "'" +
                                 " and employee_gid='" + employee_gid + "'" +
                                 " and login_time is not null";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    //
                    if (mnResult == 1)
                    {
                        msSQL = "Select approved_by from hrm_trn_tlogoutapproval where logoutapproval_gid= '" + attendancelogouttmp_gid + "' and approval_flag<>'Y'";
                        objTblRQ = objdbconn.GetDataTable(msSQL);
                        foreach (DataRow dt in objTblRQ.Rows)
                        {
                            lsapprovedby = dt["approved_by"].ToString();

                            msSQL = "select pop_username,pop_password from adm_mst_tcompany";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                supportmail = objODBCDatareader["pop_username"].ToString();
                                emailpassword = objODBCDatareader["pop_password"].ToString();
                            }
                            objODBCDatareader.Close();
                            if (supportmail != "")
                            {
                                msSQL = " select b.employee_emailid," +
                                            " date_format(a.logout_time,'%H:%i %p') as logout_time," +
                                            " concat(c.user_firstname,' ',c.user_lastname) as username," +
                                            " date_format(a.attendance_date,'%d-%m-%Y') as attendance_date from hrm_tmp_tattendance a" +
                                            " left join hrm_trn_tlogoutapproval d on a.attendancetmp_gid=d.logoutapproval_gid" +
                                            " left join hrm_mst_temployee b on d.approved_by=b.employee_gid " +
                                            " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                                            " where a.attendancetmp_gid='" + attendancelogouttmp_gid + "' and b.employee_gid='" + lsapprovedby + "'";
                                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                if (objODBCDatareader.HasRows == true)
                                {
                                    employee_mailid = objODBCDatareader["employee_emailid"].ToString();
                                    employeename = objODBCDatareader["username"].ToString();
                                    fromhours = objODBCDatareader["logout_time"].ToString();
                                    attendance_date = objODBCDatareader["attendance_date"].ToString();
                                }
                                objODBCDatareader.Close();
                                if (employee_mailid != "")
                                {

                                    msSQL = " select a.created_by, Concat(c.user_firstname,' ',c.user_lastname) as username " +
                                            " from hrm_tmp_tattendance a " +
                                            " left join adm_mst_tuser c on a.created_by=c.user_gid " +
                                            " where a.attendancetmp_gid='" + attendancelogouttmp_gid + "'";
                                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                    if (objODBCDatareader.HasRows == true)
                                    {
                                        applied_by = objODBCDatareader["username"].ToString();
                                    }
                                    objODBCDatareader.Close();

                                    message = "Hi Sir/Madam,  <br />";
                                    message = message + "<br />";
                                    message = message + "I have applied for Logout Time Requisition on " + attendance_date + "<br />";
                                    message = message + "<br />";
                                    message = message + "<b>Actual Logout Time:</b> " + fromhours + "<br />";
                                    message = message + "<br />";
                                    message = message + " Thanks and Regards  <br />";
                                    message = message + " " + applied_by + " <br />";

                                    try
                                    {
                                        MailFlag = objcmnfunctions.SendSMTP2(supportmail, emailpassword, employee_mailid, "" + applied_by + " applied for Logout Time Requisition", message, "", "", "");
                                    }

                                    catch (Exception ex)
                                    {
                                        values.message = ex.Message;
                                        objTblRQ.Dispose();
                                        return false;
                                    }
                                }
                            }
                       
                        }
                        objTblRQ.Dispose();
                        values.status = true;
                        values.message = "Logout Time Requisition Applied Successfully";
                        return true;
                    }

                    else
                    {
                        values.status = false;
                        values.message = "Error Occurred While Updating Logout Time Requisition";
                        return false;

                    }
                }
                else
                {
                    values.status = false;
                    values.message = "You have not been login yet";
                    return false;
                }

            }
            catch
            {
                values.status = false;
                values.message = "Error Occurred While Updating Logout Time Requisition";
                return false;
            }
        }

        public bool DaGetLoginSummary(string employee_gid, mdlloginsummary objloginsummary)
        {
           try
            {
                msSQL = " SELECT attendancelogintmp_gid,date_format(created_date,'%d-%m-%Y') as applydate, date_format(attendance_date,'%d-%m-%Y') as attendancedate," +
                   " time_format(login_time, '%H:%i %p') as login,status,remarks from hrm_tmp_tattendancelogin " +
                   " where employee_gid = '" + employee_gid + "' order by attendancelogintmp_gid desc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getloginsummary = new List<loginsummary_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getloginsummary.Add(new loginsummary_list
                        {
                            attendancelogintmp_gid = (dr_datarow["attendancelogintmp_gid"].ToString()),
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
                objloginsummary.status = true;
                return true;
            }
            catch
            {
                objloginsummary.status = false;
                return false;
            }
        }

        public bool DaGetLogoutSummary(string employee_gid, mdllogoutsummary objlogoutsummary)
        {
          try
            {
                msSQL = " SELECT attendancetmp_gid,date_format(created_date,'%d-%m-%Y') as applydate, date_format(attendance_date,'%d-%m-%Y') as attendancedate," +
                  " time_format(logout_time, '%H:%i %p') as logout,status,remarks from hrm_tmp_tattendance " +
                  " where employee_gid = '" + employee_gid + "' order by attendancetmp_gid desc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getloginsummary = new List<logoutsummary_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getloginsummary.Add(new logoutsummary_list
                        {
                            attendancetmp_gid = (dr_datarow["attendancetmp_gid"].ToString()),
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
                objlogoutsummary.status = true;
                return true;
            }
            catch
            {
                objlogoutsummary.status = false;
                return false;
            }
        }
        public bool DaPostApplyOnduty(string employee_gid, string user_gid, applyondutydetails values)
        {
            try
            {
                string ls_session;
                lsfromtime = Convert.ToDateTime(values.od_fromhr + ":" + values.od_frommin);
                lstotime = Convert.ToDateTime(values.od_tohr + ":" + values.od_tomin);
                TimeSpan lstotaltime = lstotime.Subtract(lsfromtime);

                msGetGID = objcmnfunctions.GetMasterGID("HODP");
                gid = msGetGID;
                if (msGetGID == "E")
                {
                    return false;
                }
                if (values.od_session == "FN")
                {
                    ls_session = "DX";
                }
                else if (values.od_session == "AN")
                {
                    ls_session = "XD";
                }
                else
                {
                    ls_session = "OD";
                }
                if (values.onduty_period == "Full")
                {
                    msSQL = " Insert into hrm_trn_tondutytracker" +
                  " (ondutytracker_gid, " +
                  " employee_gid," +
                  " onduty_fromtime," +
                  " onduty_totime, " +
                  " from_minutes, " +
                  " to_minutes, " +
                  " onduty_duration," +
                  " onduty_reason," +
                  " ondutytracker_date , " +
                  " ondutytracker_status," +
                  " half_day, " +
                  " half_session, " +
                  " onduty_count, " +
                  " created_by," +
                  " created_date)" +
                  "  Values  (" +
                  "'" + msGetGID + "'," +
                  "'" + employee_gid + "'," +
                  "'" + values.od_fromhr + "'," +
                  "'" + values.od_tohr + "'," +
                  "'" + values.od_frommin + "'," +
                  "'" + values.od_tomin + "'," +
                  "'" + lstotaltime + "'," +
                  "'" + values.od_reason + "'," +
                  "'" + values.od_date.ToString("yyyy-MM-dd") + "'," +
                  "'Pending'," +
                  "'N'," +
                  "'" + ls_session + "'," +
                  "'1'," +
                  "'" + user_gid + "'," +
                  "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";

                }
                else
                {
                    msSQL = " Insert into hrm_trn_tondutytracker" +
                  " (ondutytracker_gid, " +
                  " employee_gid," +
                  " onduty_fromtime," +
                  " onduty_totime, " +
                  " from_minutes, " +
                  " to_minutes, " +
                  " onduty_duration," +
                  " onduty_reason," +
                  " ondutytracker_date , " +
                  " ondutytracker_status," +
                  " half_day, " +
                  " half_session, " +
                  " onduty_count, " +
                  " created_by," +
                  " created_date)" +
                  "  Values  (" +
                  "'" + msGetGID + "'," +
                  "'" + employee_gid + "'," +
                  "'" + values.od_fromhr + "'," +
                  "'" + values.od_tohr + "'," +
                  "'" + values.od_frommin + "'," +
                  "'" + values.od_tomin + "'," +
                  "'" + lstotaltime + "'," +
                  "'" + values.od_reason.Replace("'", "") + "'," +
                  "'" + values.od_date.ToString("yyyy-MM-dd") + "'," +
                  "'Pending'," +
                  "'Y'," +
                  "'" + ls_session + "'," +
                  "' 0.5'," +
                  "'" + user_gid + "'," +
                  "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";


                }
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                msSQL = "Select employee_gid from hrm_trn_tattendance " +
                                      "where attendance_date='" + values.od_date.ToString("yyyy-MM-dd") + "' and employee_gid='" + employee_gid + "'";
                objTblRQ = objdbconn.GetDataTable(msSQL);
                if (objTblRQ.Rows.Count > 0)
                {
                    msSQL = "update hrm_trn_tattendance set " +
                        "update_flag='N'" +
                        "where attendance_date='" + values.od_date.ToString("yyyy-MM-dd") + "' and employee_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                else
                {
                    msGetGID = objcmnfunctions.GetMasterGID("HATP");
                    msSQL = "Insert Into hrm_trn_tattendance" +
                            "(attendance_gid," +
                            " employee_gid," +
                            " attendance_date," +
                            " employee_attendance," +
                            " attendance_type)" +
                            " VALUES ( " +
                            "'" + msGetGID + "', " +
                            "'" + employee_gid + "'," +
                            "'" + values.od_date.ToString("yyyy-MM-dd") + "'," +
                            "'Absent'," +
                            "'A')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                }

                objcmnfunctions.PopSummary(employee_gid, user_gid, lscount);
                objTblRQ = objcmnfunctions.foundRow(table);
                lscount = objcmnfunctions.foundcount(lscount);

                foreach (DataRow objRow1 in objTblRQ.Rows)
                {
                    lsemployee = objRow1["employee_gid"].ToString();
                    hierary_level = objRow1["hierarchy_level"].ToString();
                    msGetGID = objcmnfunctions.GetMasterGID("PODC");
                    msSQL = "insert into hrm_trn_tapproval ( " +
                            " approval_gid, " +
                            " approved_by, " +
                            " approved_date, " +
                            " seqhierarchy_view, " +
                            " hierary_level, " +
                            " submodule_gid, " +
                            " ondutyapproval_gid " +
                            " ) values ( " +
                            "'" + msGetGID + "'," +
                            " '" + lsemployee + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                            " 'N', " +
                            " '" + hierary_level + "' , " +
                            "'HRMLEVARL'," +
                            "'" + gid + "') ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                msSQL = "update hrm_trn_tapproval set " +
                       "seqhierarchy_view='Y' " +
                       "where approval_flag='N'and approved_by <> '" + employee_gid + "' " +
                       "and ondutyapproval_gid = '" + gid + "'" +
                       "order by hierary_level desc limit 1";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                string lsflag;
                msSQL = "select approval_flag from hrm_trn_tapproval where ondutyapproval_gid='" + gid + "' and submodule_gid='HRMLEVARL'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                objODBCDatareader.Read();
                if (objODBCDatareader.HasRows == false)
                {
                    objODBCDatareader.Close();
                    msSQL = " update hrm_trn_tondutytracker set " +
                " ondutytracker_status='Approved' " +
                " where ondutytracker_gid = '" + gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                else
                {
                    objODBCDatareader.Close();
                    msSQL = "select approved_by from hrm_trn_tapproval where ondutyapproval_gid='" + gid + "' and submodule_gid='HRMLEVARL' ";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    objODBCDatareader.Read();
                    if (objODBCDatareader.RecordsAffected == 1)
                    {
                        if (objODBCDatareader["approved_by"].ToString() == employee_gid)
                        {
                            objODBCDatareader.Close();
                            msSQL = "update hrm_trn_tapproval set " +
                        "approval_flag='Y' " +
                        "where approved_by='" + employee_gid + "' and ondutyapproval_gid = '" + gid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            msSQL = " update hrm_trn_tondutytracker set " +
                                " ondutytracker_status = 'Approved' " +
                                " where ondutytracker_gid = '" + gid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            msSQL = " SELECT employeereporting_to FROM adm_mst_tmodule2employee " +
                            " where employee_gid   = '" + employee_gid + "' and module_gid ='HRM' and employeereporting_to='EM1006040001' ";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            objODBCDatareader.Read();
                            if (objODBCDatareader.HasRows == true)
                            {
                                objODBCDatareader.Close();
                                msSQL = " update hrm_trn_tondutytracker set " +
                                  " ondutytracker_status = 'Approved' " +
                                  " where ondutytracker_gid = '" + gid + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }
                        }


                    }
                    else if (objODBCDatareader.RecordsAffected > 1)
                    {
                        objODBCDatareader.Close();
                        msSQL = " update hrm_trn_tapproval set " +
                            " approval_flag='Y' " +
                            " where approved_by='" + employee_gid + "' and ondutyapproval_gid = '" + gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        msSQL = " update hrm_trn_tondutytracker set " +
                                " ondutytracker_status = 'Pending' " +
                                " where ondutytracker_gid = '" + gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                ds_tPR.Dispose();
                objTblRQ.Dispose();

                if (mnResult == 1)
                {
                    // Whatsapp starts here
                    DateTime lsondutydate;
                    string lswhatsappflag;
                    msSQL = "select whatsapp_flag from adm_mst_tcompany";
                    lswhatsappflag = objdbconn.GetExecuteScalar(msSQL);
                    if (lswhatsappflag == "Y")
                    {
                        Whatsapp_reportingto(employee_gid);
                        if (lsEmployee_Gid_list != "")
                        {
                            lsEmployee_Gid_list = lsEmployee_Gid_list.Remove(lsEmployee_Gid_list.Length - 1, 1);

                        }
                        string lsmobile_no;
                        msSQL = "select employee_mobileno from hrm_mst_temployee where employee_gid in (" + lsEmployee_Gid_list + ")";
                        objTblRQ = objdbconn.GetDataTable(msSQL);
                        if (objTblRQ.Rows.Count > 0)
                        {
                            foreach (DataRow objTblRow in objTblRQ.Rows)
                            {
                                string lsname, lsmessage;
                                lsmobile_no = objTblRow["employee_mobileno"].ToString();

                                lsondutydate = values.od_date;

                                msSQL = " select concat(user_firstname,user_lastname) As Name from adm_mst_tuser  " +
                                          " left join hrm_mst_temployee using(user_gid) where employee_gid='" + employee_gid + "'";
                                lsname = objdbconn.GetExecuteScalar(msSQL);
                                lsmessage = " " + lsname + "  Applied the Onduty  on " + lsondutydate + "  for about " + lstotaltime + "  hours ";
                                sendMessage(lsmobile_no, lsmessage);
                            }
                        }
                    }

                    //Mailfunction starts here
                    string lsapprovedby;
                    string message;
                    string employee_mailid = null;
                    string employeename = null;
                    string applied_by = null;
                    string supportmail = null;
                    string pwd = null;
                    int MailFlag;
                    string reason = null;
                    string days = null;
                    string fromdate = null;
                    string todate = null;
                    string emailpassword = null;
                    string trace_comment;
                    string onduty_date = null;

                    msSQL = "Select approved_by from hrm_trn_tapproval where ondutyapproval_gid= '" + gid + "' and approval_flag<>'Y'";
                    objTblRQ = objdbconn.GetDataTable(msSQL);
                    if (objTblRQ.Rows.Count > 0)
                    {
                        foreach (DataRow objTblRow in objTblRQ.Rows)
                        {
                            lsapprovedby = objTblRow["approved_by"].ToString();
                            msSQL = "select pop_username,pop_password from adm_mst_tcompany";
                            objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader1.HasRows == true)
                            {
                                objODBCDatareader1.Read();
                                supportmail = objODBCDatareader1["pop_username"].ToString();
                                emailpassword = objODBCDatareader1["pop_password"].ToString();
                            }
                            objODBCDatareader1.Close();
                            if (supportmail != "")
                            {
                                msSQL = " select b.employee_emailid,a.onduty_duration,a.onduty_fromtime, a.onduty_totime, date_format(a.ondutytracker_date,'%d-%m-%Y') as ondutytracker_date, " +
                        " Concat(c.user_firstname,' ',c.user_lastname) as username,a.onduty_reason,a.ondutytracker_date" +
                        " from hrm_trn_tondutytracker a" +
                        " left join hrm_trn_tapproval d on a.ondutytracker_gid=d.ondutyapproval_gid" +
                        " left join hrm_mst_temployee b on d.approved_by=b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid=b.user_gid" +
                        " where a.ondutytracker_gid='" + gid + "' and b.employee_gid='" + lsapprovedby + "'";

                                objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                                if (objODBCDatareader1.HasRows == true)
                                {
                                    objODBCDatareader1.Read();
                                    employee_mailid = objODBCDatareader1["employee_emailid"].ToString();
                                    employeename = objODBCDatareader1["username"].ToString();
                                    reason = objODBCDatareader1["onduty_reason"].ToString();
                                    days = objODBCDatareader1["onduty_duration"].ToString();
                                    fromdate = objODBCDatareader1["onduty_fromtime"].ToString();
                                    todate = objODBCDatareader1["onduty_totime"].ToString();
                                    onduty_date = objODBCDatareader1["ondutytracker_date"].ToString();
                                }
                                objODBCDatareader1.Close();
                                if (employee_mailid != "")
                                {
                                    msSQL = " select a.created_by,concat(b.user_firstname,'-',b.user_lastname) as username," +
                                        " b.user_gid from hrm_trn_tondutytracker a" +
                                        " left join adm_mst_tuser b on b.user_gid=a.created_by" +
                                        " left join hrm_mst_temployee c on b.user_gid=c.user_gid" +
                                        " where a.ondutytracker_gid='" + gid + "'";
                                    objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                                    if (objODBCDatareader1.HasRows == true)
                                    {
                                        objODBCDatareader1.Read();
                                        applied_by = objODBCDatareader1["username"].ToString();
                                    }
                                    objODBCDatareader1.Close();

                                    message = "Hi Sir/Madam,  <br />";
                                    message = message + "<br />";
                                    message = message + "I have applied for On-Duty on " + onduty_date + " <br />";
                                    message = message + "<br />";
                                    message = message + "<b>Reason :</b> " + reason + "<br />";
                                    message = message + "<br />";
                                    message = message + "<b>From Hours :</b> " + fromdate + " &nbsp; &nbsp; <b>To Hours :</b> " + todate + "<br />";
                                    message = message + "<br />";
                                    message = message + "<b>Total No of Hours :</b> " + days + " <br />";
                                    message = message + "<br />";
                                    message = message + " Thanks and Regards  <br />";
                                    message = message + " " + applied_by + " <br />";

                                    try
                                    {
                                        MailFlag = objcmnfunctions.SendSMTP2(supportmail, emailpassword, employee_mailid, "" + applied_by + " applied for On Duty", message, "", "", "");
                                    }
                                    catch
                                    {

                                    }
                                }
                            }
                        

                        }
                    }

                    trace_comment = "Applied Onduty on  " + " " + fromdate;
                    Tracelog(lsgid, user_gid, trace_comment, "apply_onduty");
                    values.status = true;
                    values.message = "OD Applied Successfully";
                    return true;
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occurred";
                    return false;
                }

            }
            catch
            {
                values.status = false;
                values.message = "Error Occurred";
                return false;
            }
        }

        public bool DaGetOnDutySummary(string employee_gid, onduty_detail_list objonduty_details)
        {
          try
            {

                msSQL = " select a.ondutytracker_gid,a.employee_gid,concat(onduty_fromtime, ':', from_minutes) as onduty_from,onduty_reason," +
                        " concat(onduty_totime, ':', to_minutes) as onduty_to," +
                        " date_format(ondutytracker_date,'%d-%m-%Y') as ondutytracker_date,onduty_duration,ondutytracker_status," +
                        " concat(c.user_firstname, ' ', c.user_lastname) as onduty_approveby,onduty_approvedate" +
                        " from hrm_trn_tondutytracker a" +
                        " left join hrm_mst_temployee b on b.employee_gid = a.onduty_approveby" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                        " where a.employee_gid = '" + employee_gid + "' order by ondutytracker_gid desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var onduty_details = new List<onduty_details>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        onduty_details.Add(new onduty_details
                        {
                            ondutytracker_gid = (dr_datarow["ondutytracker_gid"].ToString()),
                            onduty_from = dr_datarow["onduty_from"].ToString(),
                            onduty_to = dr_datarow["onduty_to"].ToString(),
                            onduty_date = (dr_datarow["ondutytracker_date"].ToString()),
                            onduty_duration = dr_datarow["onduty_duration"].ToString(),
                            ondutytracker_status = dr_datarow["ondutytracker_status"].ToString(),
                            approved_by = dr_datarow["onduty_approveby"].ToString(),
                            approved_date = dr_datarow["onduty_approvedate"].ToString(),
                            onduty_reason = dr_datarow["onduty_reason"].ToString()

                        });
                    }
                    objonduty_details.onduty_details = onduty_details;
                }
                dt_datatable.Dispose();

                //objonduty_details.status = true;
                return true;
            }
            catch
            {
                //objonduty_details.status = false;
                return false;
            }
        }
        public bool DaPostApplyPermission(string employee_gid, string user_gid, permission_details values)
        {
            try
            {
                lsfromtime = Convert.ToDateTime(values.permission_fromhr + ":" + values.permission_frommin);
                lstotime = Convert.ToDateTime(values.permission_tohr + ":" + values.permission_tomin);
                TimeSpan lstotaltime = lstotime.Subtract(lsfromtime);
              
                //msSQL = " select permission_gid from  hrm_trn_tpermission where  employee_gid='" + employee_gid + "'" +
                //        " and permission_date='" + values.permission_date.ToString("yyyy-MM-dd") + "'";
                //objODBCDatareader = objdbconn.GetDataReader(msSQL);
                //if (objODBCDatareader.HasRows == true)
                //{
                //    values.message = "Permission has already applied on this date";
                //    return false;

                //}
                //objODBCDatareader.Close();

                //msSQL = " select * from hrm_trn_tpermission " +
                //        " where employee_gid='" + employee_gid + "'  and permission_status='Pending' and " +
                //         " month(permission_date)='" + (values.permission_date).Month + "'" +
                //         " and year(permission_date)='" + (values.permission_date).Year + "'";
                //objODBCDatareader = objdbconn.GetDataReader(msSQL);
                //if (objODBCDatareader.HasRows == true)
                //{
                //    values.message = "Your Previous Permission is Pending ,you Cannot apply  !!! Kindly Close your Previous Permission ";
                //    return false;

                //}
                //objODBCDatareader.Close();
              
                msSQL = " Select a.holiday_name,b.employee_gid from hrm_mst_tholiday a " +
                        " inner join hrm_mst_tholiday2employee b on a.holiday_gid=b.holiday_gid " +
                        " where a.holiday_date='" + values.permission_date.ToString("yyyy-MM-dd") + "' and b.employee_gid='" + employee_gid + "' ";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.message = "It is a holiday,You can't apply Permission on that day";
                    return false;
                }
                objODBCDatareader.Close();
                TimeSpan permission_duration;
                TimeSpan permission_durationperday;
               
                msSQL = " select cast(ifnull(sec_to_time(sum(time_to_sec(cast(concat(case when length(permission_totalhours)=1 then concat(0,permission_totalhours) " +
                  " else permission_totalhours end,':',case when length(total_mins)=1 then concat(0,total_mins) " +
                  " else total_mins end,':00') as time)))),'00:00:00')as time)as permission_hours from(hrm_trn_tpermission) " +
                  " where employee_gid='" + employee_gid + "'  and permission_status='Approved' and " +
                  " month(permission_date)='" + (values.permission_date).Month + "'" +
                   " and year(permission_date)='" + (values.permission_date).Year + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    permission_duration = TimeSpan.Parse(objODBCDatareader["permission_hours"].ToString()) + lstotaltime;
                }
                else
                {
                    permission_duration = TimeSpan.Parse("00:00:00") + lstotaltime;
                }

              
                objODBCDatareader.Close();

                msSQL = " select cast(ifnull(sec_to_time(sum(time_to_sec(cast(concat(case when length(permission_totalhours)=1 then concat(0,permission_totalhours) " +
                " else permission_totalhours end,':',case when length(total_mins)=1 then concat(0,total_mins) " +
                " else total_mins end,':00') as time)))),'00:00:00')as time)as permission_hours from(hrm_trn_tpermission) " +
                " where employee_gid='" + employee_gid + "'  and permission_status='Approved' and " +
                " month(permission_date)='" + (values.permission_date).Month + "'" +
                 " and year(permission_date)='" + (values.permission_date).Year + "'" +
                   " and permission_date ='" + values.permission_date.ToString("yyyy-MM-dd") + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    permission_durationperday = TimeSpan.Parse(objODBCDatareader["permission_hours"].ToString()) + lstotaltime;
                }
                else
                {
                    permission_durationperday = TimeSpan.Parse("00:00:00") + lstotaltime;
                }


                objODBCDatareader.Close();

                string lspermission;
                msSQL = " select ifnull(permission_month,'00:00:00') as permission from hrm_mst_tmastersettings a " +
                    " inner join hrm_mst_tbranch b on a.branch_gid=b.branch_gid " +
                    " inner join hrm_mst_temployee c on b.branch_gid=c.branch_gid" +
                    " where (cast('" + values.permission_date.ToString("yyyy-MM-dd") + "' as date)  " +
                    " between attendance_startdate  and attendance_enddate ) " +
                    " and c.employee_gid='" + employee_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lspermission = objODBCDatareader["permission"].ToString();

                    if (lspermission == "")
                    {
                        lspermission = "00:00:00";
                        values.message = "Kindly inform your HR to assign the permission limit for the Month.";
                        return false;
                    }
                    else if (lspermission == "00:00:00")
                    {
                        values.message = "Kindly inform your HR to assign the permission limit for the Month.";
                        return false;
                    }
                    else
                    {
                        lspermission = objODBCDatareader["permission"].ToString();
                        if (permission_duration > TimeSpan.Parse(lspermission))
                        {
                            values.message = "permission Hour Should not Exceed " + lspermission + " for this month";
                            return false;
                        }
                        else if (lstotaltime > TimeSpan.Parse(lspermission))
                        {
                            values.message = "permission Hour Should not Exceed  " + lspermission + " for this month";
                            return false;
                        }
                    }

                }
                objODBCDatareader.Close();

                //perday

               
                msSQL = " select ifnull(permission_day,'00:00:00') as permission from hrm_mst_tmastersettings a " +
                    " inner join hrm_mst_tbranch b on a.branch_gid=b.branch_gid " +
                    " inner join hrm_mst_temployee c on b.branch_gid=c.branch_gid" +
                    " where (cast('" + values.permission_date.ToString("yyyy-MM-dd") + "' as date)  " +
                    " between attendance_startdate  and attendance_enddate ) " +
                    " and c.employee_gid='" + employee_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lspermission = objODBCDatareader["permission"].ToString();

                    if (lspermission == "")
                    {
                        lspermission = "00:00:00";
                        values.message = "Kindly inform your HR to assign the permission limit for the Month.";
                        return false;
                    }
                    else if (lspermission == "00:00:00")
                    {
                        values.message = "Kindly inform your HR to assign the permission limit for the Month.";
                        return false;
                    }
                    else
                    {
                        lspermission = objODBCDatareader["permission"].ToString();
                        if (permission_durationperday > TimeSpan.Parse(lspermission))
                        {
                            values.message = "permission Hour Should not Exceed " + lspermission + " for per day";
                            return false;
                        }
                        else if (lstotaltime > TimeSpan.Parse(lspermission))
                        {
                            values.message = "permission Hour Should not Exceed  " + lspermission + " for per day";
                            return false;
                        }
                    }

                }
                objODBCDatareader.Close();
                //perday

                //main table
                string totalhr, totalmin, permission_fromhrs, permission_tohrs, permission_frommins, permission_tomins;
                string hrlength = Convert.ToString(lstotaltime.Hours);
                Double lshrlength = hrlength.Length;
                if (Convert.ToString(lshrlength) == "2")
                {
                    totalhr = Convert.ToString(lstotaltime.Hours);
                }
                else
                {
                    totalhr = "0" + lstotaltime.Hours;
                }

                string minlength = Convert.ToString(lstotaltime.Minutes);
                Double lsminlength = minlength.Length;
                if (Convert.ToString(lsminlength) == "2")
                {
                    totalmin = Convert.ToString(lstotaltime.Minutes);
                }
                else
                {
                    totalmin = "0" + lstotaltime.Minutes;
                }

                msSQL = " select permission_gid from  hrm_trn_tpermission where  employee_gid='" + employee_gid + "'" +
                        " and permission_date='" + values.permission_date.ToString("yyyy-MM-dd") + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);

                if (objODBCDatareader.HasRows == false)
                {
                    msGetGIDpermission = objcmnfunctions.GetMasterGID("HPNP");
                    if (msGetGID == "E")
                    {
                        return false;
                    }

                    msSQL = " Insert into hrm_trn_tpermission" +
                             " (permission_gid, " +
                             " employee_gid," +
                             " permission_applydate," +
                             " permission_date," +
                             " created_by," +
                             " created_date," +
                             " halfday_flag) Values  " +
                             " ('" + msGetGIDpermission + "', " +
                             " '" + employee_gid + "'," +
                             "'" + DateTime.Now.ToString("yyyy-MM-dd") + "', " +
                             " '" + values.permission_date.ToString("yyyy-MM-dd") + "'," +
                              " '" + employee_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd") + "', " +
                                " ' ') ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                else
                {
                  

                    msSQL = " select permission_gid,employee_gid,permission_totalhours,total_mins, " +
                    " concat(permission_totalhours,':',total_mins,':00') as totaltime " +
                    " from  hrm_trn_tpermissiondtl where  employee_gid='" + employee_gid + "'" +
                    " and permission_date='" + values.permission_date.ToString("yyyy-MM-dd") + "' and permission_status='Approved'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);

                    if (objODBCDatareader.HasRows == true)
                    {
                        while (objODBCDatareader.Read())
                        {
                            lspermissiontime = TimeSpan.Parse(objODBCDatareader["totaltime"].ToString());
                            lstotalpermissiontime = lstotalpermissiontime.Add(lspermissiontime);
                        }
                        string totalpermissionhr;
                        string totalpermissionmin;
                        string lstotalpermissionhours = Convert.ToString(lstotalpermissiontime.Hours);
                        Double lshrperlength = lstotalpermissionhours.Length;
                        if (Convert.ToString(lshrperlength) == "2")
                        {
                            totalpermissionhr = Convert.ToString(lstotalpermissiontime.Hours);
                        }
                        else
                        {
                            totalpermissionhr = "0" + lstotalpermissiontime.Hours;
                        }

                        string lstotalpermissionmin = Convert.ToString(lstotalpermissiontime.Minutes);
                        Double lshrperminlength = lstotalpermissionmin.Length;
                        if (Convert.ToString(lshrperminlength) == "2")
                        {
                            totalpermissionmin = Convert.ToString(lstotalpermissiontime.Minutes);
                        }
                        else
                        {
                            totalpermissionmin = "0" + lstotalpermissiontime.Minutes;
                        }

                        msSQL = " Update hrm_trn_tpermission set " +
                        " permission_totalhours= '" + totalpermissionhr + "'," +
                        " total_mins= '" + totalpermissionmin + "'," +
                        " permission_status= 'Approved'" +
                        " where  employee_gid='" + employee_gid + "'" +
                        " and permission_date='" + values.permission_date.ToString("yyyy-MM-dd") +  "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    }
                       
                }

             
                    //main table
             
              
                //permission from
                string fromhrspermission = Convert.ToString(values.permission_fromhr);
                Double lspermissionfrom = fromhrspermission.Length;
                if (Convert.ToString(lspermissionfrom) == "2")
                {
                    permission_fromhrs = values.permission_fromhr;
                }
                else
                {
                    permission_fromhrs = "0" + values.permission_fromhr;
                }
                //permission to
                string tohrspermission = Convert.ToString(values.permission_tohr);
                Double lspermissionto = tohrspermission.Length;
                if (Convert.ToString(lspermissionto) == "2")
                {
                    permission_tohrs = values.permission_tohr;
                }
                else
                {
                    permission_tohrs = "0" + values.permission_tohr;
                }
                //permission from mins
                string fromminspermission = Convert.ToString(values.permission_frommin);
                Double lspermissionfrommins = fromminspermission.Length;
                if (Convert.ToString(lspermissionfrommins) == "2")
                {
                    permission_frommins = values.permission_frommin;
                }
                else
                {
                    permission_frommins = "0" + values.permission_frommin;
                }

                //permission to mins

                string tominspermission = Convert.ToString(values.permission_tomin);
                Double lstominspermission = tominspermission.Length;
                if (Convert.ToString(lstominspermission) == "2")
                {
                    permission_tomins = values.permission_tomin;
                }
                else
                {
                    permission_tomins = "0" + values.permission_tomin;
                }
                msGetGID = objcmnfunctions.GetMasterGID("HRPE");
                if (msGetGID == "E")
                {
                    return false;
                }
                if (msGetGIDpermission == null)
                {
                    msSQL = " Select permission_gid from hrm_trn_tpermission " +
                      " where employee_gid = '" + employee_gid + "'" +
                      " and permission_date='" + values.permission_date.ToString("yyyy-MM-dd") + "'";
                    msGetGIDpermission = objdbconn.GetExecuteScalar(msSQL);
                }
              

                msSQL = " Insert into hrm_trn_tpermissiondtl " +
                                " (permissiondtl_gid, " +
                                " permission_gid," +
                                " employee_gid," +
                                " permission_applydate," +
                                " permission_totalhours, " +
                                " total_mins, " +
                                " permission_reason," +
                                " permission_date," +
                                " permission_fromhours, " +
                                " permission_tohours, " +
                                " permission_status," +
                                " permission_frommins, " +
                                " permission_tomins, " +
                                " created_by," +
                                " created_date," +
                                " halfday_flag) Values  " +
                                " ('" + msGetGID + "', " +
                                " '" + msGetGIDpermission + "'," +
                                " '" + employee_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd") + "', " +
                                " '" + totalhr + "'," +
                                " '" + totalmin + "'," +
                                " '" + values.permission_reason.Replace("'", "") + "'," +
                                " '" + values.permission_date.ToString("yyyy-MM-dd") + "'," +
                                " '" + permission_fromhrs + "', " +
                                "'" + permission_tohrs + "', " +
                                " 'Pending'," +
                                "'" + permission_frommins + "', " +
                                "'" + permission_tomins + "', " +
                                 " '" + employee_gid + "'," +
                                 "'" + DateTime.Now.ToString("yyyy-MM-dd") + "', " +
                                   " ' ') ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                lsgid = msGetGID;
                objcmnfunctions.PopSummary(employee_gid, user_gid, lscount);
                objTblRQ = objcmnfunctions.foundRow(table);
                lscount = objcmnfunctions.foundcount(lscount);
                foreach (DataRow objRow1 in objTblRQ.Rows)
                {
                    lsemployee = objRow1["employee_gid"].ToString();
                    hierary_level = objRow1["hierarchy_level"].ToString();

                    msGetGID = objcmnfunctions.GetMasterGID("PODC");
                    msSQL = "insert into hrm_trn_tapproval (" +
                             " approval_gid, " +
                             " approved_by, " +
                             " approved_date, " +
                             " seqhierarchy_view, " +
                             " hierary_level, " +
                             " submodule_gid, " +
                             " permissionapproval_gid "
                             + " ) values ( " +
                             "'" + msGetGID + "'," +
                             " '" + lsemployee + "'," +
                             "'" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                             " 'N', " +
                             " '" + hierary_level + "' , " +
                             "'HRMLEVARL'," +
                             "'" + lsgid + "') ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                msSQL = "update hrm_trn_tapproval set " +
                     "seqhierarchy_view='Y' " +
                     "where approval_flag='N'and approved_by <> '" + employee_gid + "' " +
                     "and permissionapproval_gid = '" + lsgid + "'" +
                     "order by hierary_level desc limit 1";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                string lsflag;

                msSQL = "select approval_flag from hrm_trn_tapproval where permissionapproval_gid='" + lsgid + "' " +
                         " and submodule_gid='HRMLEVARL'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                
                if (objODBCDatareader.HasRows == false)
                {
                    objODBCDatareader.Close();
                    msSQL = " update hrm_trn_tpermissiondtl set " +
                        " permission_status='Approved' " +
                        " where permissiondtl_gid = '" + lsgid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                else
                {
                    objODBCDatareader.Close();
                    msSQL = "select approved_by from hrm_trn_tapproval where permissionapproval_gid='" + lsgid + "'" +
                             " and submodule_gid='HRMLEVARL' ";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lsapproved_by = objODBCDatareader["approved_by"].ToString();

                    }
                    if (objODBCDatareader.RecordsAffected == 1)
                    {

                        if (lsapproved_by == employee_gid)
                        {
                            objODBCDatareader.Close();
                            msSQL = " update hrm_trn_tapproval set " +
                                " approval_flag='Y' " +
                                " where approved_by='" + employee_gid + "' and permissionapproval_gid = '" + lsgid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            msSQL = " update hrm_trn_tpermissiondtl set " +
                                        " permission_status = 'Approved' " +
                                        " where permissiondtl_gid = '" + lsgid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            msSQL = " SELECT employeereporting_to FROM adm_mst_tmodule2employee " +
                                " where employee_gid   = '" + employee_gid + "' and " +
                                " module_gid ='HRM' and employeereporting_to='EM1006040001' ";
                            objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader1.HasRows == true)
                            {
                                msSQL = " update hrm_trn_tpermissiondtl set " +
                                         " permission_status = 'Approved' " +
                                         " where permissiondtl_gid = '" + lsgid + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }

                        }
                    }
                    else if (objODBCDatareader.RecordsAffected > 1)
                    {
                        objODBCDatareader.Close();
                        msSQL = " update hrm_trn_tapproval set " +
                            " approval_flag='Y' " +
                            " where approved_by='" + employee_gid + "' and permissionapproval_gid = '" + lsgid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        msSQL = " update hrm_trn_tpermissiondtl set " +
                                " permission_status = 'Pending' " +
                                " where permissiondtl_gid = '" + lsgid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }

                }

                ds_tPR.Dispose();
                objTblRQ.Dispose();
                if (mnResult == 1)
                {
                    // Whatsapp starts here
                    DateTime lspermissiondate;
                    string lswhatsappflag;
                    msSQL = "select whatsapp_flag from adm_mst_tcompany";
                    lswhatsappflag = objdbconn.GetExecuteScalar(msSQL);
                    if (lswhatsappflag == "Y")
                    {
                        Whatsapp_reportingto(employee_gid);
                        if (lsEmployee_Gid_list != "")
                        {
                            lsEmployee_Gid_list = lsEmployee_Gid_list.Remove(lsEmployee_Gid_list.Length - 1, 1);

                        }
                        string lsmobile_no;
                        msSQL = "select employee_mobileno from hrm_mst_temployee where employee_gid in (" + lsEmployee_Gid_list + ")";
                        objTblRQ = objdbconn.GetDataTable(msSQL);
                        if (objTblRQ.Rows.Count > 0)
                        {
                            foreach (DataRow objTblRow in objTblRQ.Rows)
                            {
                                string lsname, lsmessage;
                                lsmobile_no = objTblRow["employee_mobileno"].ToString();

                                lspermissiondate = values.permission_date;

                                string lsptimeto = " " + values.permission_tohr + ":" + values.permission_tomin + ":00";
                                string lsptimefrom = " " + values.permission_fromhr + ":" + values.permission_frommin + ":00";

                                msSQL = " select concat(user_firstname,user_lastname) As Name from adm_mst_tuser  " +
                                          " left join hrm_mst_temployee using(user_gid) where employee_gid='" + employee_gid + "'";
                                lsname = objdbconn.GetExecuteScalar(msSQL);
                                lsmessage = " " + lsname + "  applied the permission  on " + lspermissiondate + "  from " + lsfromtime + " to " + lstotime;
                                sendMessage(lsmobile_no, lsmessage);
                            }
                        }

                    }

                    //Mailfunction starts here
                    string lsapprovedby;
                    string message;
                    string employee_mailid = null;
                    string employeename = null;
                    string applied_by = null;
                    string supportmail = null;
                    string pwd = null;
                    int MailFlag;
                    string reason = null;
                    string days = null;
                    string fromhours = null;
                    string tohours = null;
                    string emailpassword = null;
                    string trace_comment;
                    string permission_date = null;
                    msSQL = "Select approved_by from hrm_trn_tapproval where permissionapproval_gid= '" + lsgid + "' and approval_flag<>'Y'";
                    objTblRQ = objdbconn.GetDataTable(msSQL);
                    if (objTblRQ.Rows.Count > 0)
                    {
                        foreach (DataRow objTblRow in objTblRQ.Rows)
                        {
                            lsapprovedby = objTblRow["approved_by"].ToString();
                            msSQL = "select pop_username,pop_password from adm_mst_tcompany";
                            objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader1.HasRows == true)
                            {
                                objODBCDatareader1.Read();
                                supportmail = objODBCDatareader1["pop_username"].ToString();
                                emailpassword = objODBCDatareader1["pop_password"].ToString();
                            }
                            objODBCDatareader1.Close();
                            if (supportmail != "")
                            {
                                msSQL = " select b.employee_emailid,a.permission_totalhours,total_mins," +
                                    " concat(a.permission_fromhours,':',a.permission_frommins)as fromhours," +
                                    " concat(a.permission_tohours,':',a.permission_tomins)as tohours," +
                                    " concat(c.user_firstname,' ',c.user_lastname) as username,a.permission_reason ," +
                                    " date_format(a.permission_date,'%d-%m-%Y') as permission_date from hrm_trn_tpermissiondtl a" +
                                    " left join hrm_trn_tapproval d on a.permissiondtl_gid=d.permissionapproval_gid" +
                                    " left join hrm_mst_temployee b on d.approved_by=b.employee_gid " +
                                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                                    " where a.permissiondtl_gid='" + lsgid + "' and b.employee_gid='" + lsapprovedby + "'";
                                objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                                if (objODBCDatareader1.HasRows == true)
                                {
                                    objODBCDatareader1.Read();
                                    employee_mailid = objODBCDatareader1["employee_emailid"].ToString();
                                    employeename = objODBCDatareader1["username"].ToString();
                                    reason = objODBCDatareader1["permission_reason"].ToString();
                                    days = objODBCDatareader1["permission_totalhours"].ToString();
                                    days += ":" + objODBCDatareader1["total_mins"].ToString();
                                    fromhours = objODBCDatareader1["fromhours"].ToString();
                                    tohours = objODBCDatareader1["tohours"].ToString();
                                    permission_date = objODBCDatareader1["permission_date"].ToString();
                                }
                                objODBCDatareader1.Close();
                                if (employee_mailid != "")
                                {
                                    msSQL = " select a.created_by,concat(c.user_firstname,'-',c.user_lastname) as username," +
                                        " b.user_gid from hrm_trn_tpermissiondtl a" +
                                        " left join hrm_mst_temployee b on b.employee_gid=a.created_by" +
                                        " left join adm_mst_tuser c on b.user_gid=c.user_gid" +
                                        " where a.permissiondtl_gid='" + lsgid + "'";
                                    objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                                    if (objODBCDatareader1.HasRows == true)
                                    {
                                        objODBCDatareader1.Read();
                                        applied_by = objODBCDatareader1["username"].ToString();
                                    }
                                    objODBCDatareader1.Close();

                                    message = "Hi Sir/Madam,  <br />";
                                    message = message + "<br />";
                                    message = message + "I have applied for Permission on " + permission_date + " <br />";
                                    message = message + "<br />";
                                    message = message + "<b>Reason :</b> " + reason + "<br />";
                                    message = message + "<br />";
                                    message = message + "<b>From Hours :</b> " + fromhours + " &nbsp; &nbsp; <b>To Hours :</b> " + tohours + "<br />";
                                    message = message + "<br />";
                                    message = message + "<b>Total No of Hours :</b> " + days + " <br />";
                                    message = message + "<br />";
                                    message = message + " Thanks and Regards  <br />";
                                    message = message + " " + applied_by + " <br />";

                                    msSQL = " select a.employee_emailid from hrm_mst_temployee a  where a.employee_gid in " +
                                        " (select distinct employee_gid from prj_trn_tmanagerprivilege where " +
                                        " project_gid in (select distinct project_gid from prj_trn_ttask where assigned_to='" + employee_gid + "'))  ";
                                    objODBCDatareader2 = objdbconn.GetDataReader(msSQL);
                                    if (objODBCDatareader2.HasRows == true)
                                    {
                                        while (objODBCDatareader2.Read())
                                        {
                                            if (employee_mailid == objODBCDatareader2["employee_emailid"].ToString())
                                            {
                                                msSQL = " select a.task_gid,date_format(a.created_date,'%d/%m/%Y')As created_date,a.project_gid,b.customer_name,c.project_name," +
                                             " concat(d.category_name,' / ',e.subcategory_name)as category_subcategory_name,a.severity," +
                                             " a.task_name,concat(f.user_firstname,'-',f.user_lastname) as created_by," +
                                             " concat(h.user_firstname,'-',h.user_lastname) as assigned_to,a.task_status,a.assigned_flag ," +
                                             " concat(l.user_firstname,'-',l.user_lastname) as assigned_by " +
                                             " from prj_trn_ttask a" +
                                             " left join crm_mst_tcustomer b on a.customer_gid = b.customer_gid" +
                                             " left join prj_mst_tproject c on a.project_gid = c.project_gid" +
                                             " left join prj_mst_tcategory d on a.category_gid=d.category_gid" +
                                             " left join prj_mst_tsubcategory e on a.subcategory_gid=e.subcategory_gid" +
                                             " left join adm_mst_tuser f on a.created_by = f.user_gid" +
                                             " left join hrm_mst_temployee g on a.assigned_to = g.employee_gid " +
                                             " left join adm_mst_tuser h on g.user_gid = h.user_gid " +
                                             " left join adm_mst_tuser l on a.assigned_by=l.user_gid " +
                                             " where a.task_status not in ('Close','Completed') and a.assigned_flag='Y' " +
                                             " and a.assigned_to='" + employee_gid + "' " +
                                             " ORDER BY date(a.created_date) desc,a.created_date asc,a.task_gid desc";

                                                objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                                                message += ("<table align='Center'><tr><td style='font-weight:bold;color:Blue' colspan='5' align='Center'> Task Pending report  &nbsp; : &nbsp; " + DateTime.Now.ToString() + " </td></tr></table>");
                                                message += ("<table border='1' cellpadding='0' width='100%' align='Center'>");
                                                message += ("<tr><td width='8%' style='font-weight:bold' align='Center'> Created Date</td><td width='8%' style='font-weight:bold' align='Center'> Customer Name </td><td width='8%' style='font-weight:bold' align='Center'> Project Name </td><td width='8%' align='Center' style='font-weight:bold'> Category/Subcategory </td><td width='30%' style='font-weight:bold' align='Center'> Task Name </td><td width='8%' style='font-weight:bold' align='Center'> Created By </td><td width='8%' style='font-weight:bold' align='Center'> Assign To </td><td width='8%' style='font-weight:bold' align='Center'> Assigned By </td><td width='8%' style='font-weight:bold' align='Center'> Priority </td><td width='8%' style='font-weight:bold' align='Center'> Task Status </td></tr>");
                                                if (objODBCDatareader1.HasRows == true)
                                                {
                                                    while (objODBCDatareader1.Read())
                                                    {
                                                        message += ("<tr><td  align='Center'> " + objODBCDatareader1["created_date"].ToString() + "</td>");
                                                        message += ("<td align='Center'> " + objODBCDatareader1["customer_name"].ToString() + "</td>");
                                                        message += ("<td> " + objODBCDatareader1["project_name"].ToString() + "</td>");
                                                        message += ("<td> " + objODBCDatareader1["category_subcategory_name"].ToString() + "</td>");
                                                        message += ("<td  align='Center' style='color:Green'> " + objODBCDatareader1["task_name"].ToString() + "</td>");
                                                        message += ("<td align='Center'> " + objODBCDatareader1["created_by"].ToString() + "</td>");
                                                        message += ("<td  align='Center'> " + objODBCDatareader1["assigned_to"].ToString() + "</td>");
                                                        message += ("<td  align='Center'> " + objODBCDatareader1["assigned_by"].ToString() + "</td>");
                                                        message += ("<td  align='Center'> " + objODBCDatareader1["severity"].ToString() + "</td>");
                                                        message += ("<td align='Center'> " + objODBCDatareader1["task_status"].ToString() + "</td></tr>");
                                                    }
                                                    message += ("</table>");
                                                }
                                                objODBCDatareader1.Close();
                                            }
                                        }
                                    }
                                    objODBCDatareader2.Close();
                                    try
                                    {
                                        //
                                        MailFlag = objcmnfunctions.SendSMTP2(supportmail, emailpassword, employee_mailid, "" + applied_by + " applied for Permission", message, "", "", "");
                                    }
                                    catch
                                    {

                                    }
                                }
                            }


                        }
                    }


                    trace_comment = "Applied Permission on  " + " " + DateTime.Now.ToString();
                    Tracelog(lsgid, user_gid, trace_comment, "apply_permission");
                    values.status = true;
                    values.message = "Permission Applied successfully";
                    return true;
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occurred";
                    return false;
                }
            }
            catch
            {
                values.status = false;
                values.message = "Error Occurred";
                return false;
            }

        }
        public bool DaPostCompoff(string employee_gid, string user_gid, mdlcompffreq values)
        {
            try
            {
                //objODBCDatareader.Close();
                DateTime lsleavefromdate;
                DateTime lsleavetodate;
                string lsleave;
                // Check If The employee has already compoff Applied or not 


                lsleavefromdate = values.compoff_date;
                lsleavetodate = values.compoff_date;
                //For Me.i = 0 To DateDiff(DateInterval.Day, lsleavefromdate, lsleavetodate)
                System.TimeSpan diffe = lsleavetodate.Subtract(lsleavefromdate);
                lsleave = Convert.ToDateTime(lsleavefromdate).ToString("yyyy-MM-dd HH:mm:ss");


                msSQL = " select a.actualworking_fromdate,a.actualworking_todate from hrm_trn_tcompensatoryoff a left join " +
                        "  hrm_mst_temployee c on a.employee_gid = c.employee_gid  where a.compensatoryoff_status = 'Approved' and" +
                        "  a.employee_gid =  '" + employee_gid + "' and " +
                        "  a.actualworking_fromdate = '" + values.actualworking_date.ToString("yyyy-MM-dd") + "'  ";
                        

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    while (objODBCDatareader.Read())
                    {
                        objODBCDatareader.Close();

                        values.message = "Already Comp Off has been taken for that day";
                        return false;
                    }
                }
                objODBCDatareader.Close();

                msSQL = " select a.actualworking_fromdate,a.actualworking_todate from hrm_trn_tcompensatoryoff a left join " +
                       "  hrm_mst_temployee c on a.employee_gid = c.employee_gid  where a.compensatoryoff_status = 'Approved' and" +
                       "  a.employee_gid =  '" + employee_gid + "' and " +
                       "  a.compensatoryoff_applydate = '" + lsleavefromdate.ToString("yyyy-MM-dd") + "'";
                    

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    while (objODBCDatareader.Read())
                    {
                        objODBCDatareader.Close();

                        values.message = "Already applied Comp Off !!";
                        return false;
                    }
                }
                objODBCDatareader.Close();

                // lsleavefromdate = lsleavefromdate.AddDays(1);


                msGetGID = objcmnfunctions.GetMasterGID("HCFP");
                if (msGetGID == "E")
                {

                    //radnote.Text = objcmnfunctions.GetErrMsg("HRM_ERR_016");
                    //radnote.Show();
                    return false;
                }
                string gid;
                gid = msGetGID;
                System.TimeSpan diff = lsleavetodate.Subtract(lsleavefromdate);
                int x = diff.Days;
                msSQL = " Insert into hrm_trn_tcompensatoryoff" + " (compensatoryoff_gid, " +
                        " employee_gid," +
                         " actualworking_fromdate," +
                         " actualworking_todate, " +
                         " compensatoryoff_status , " +
                    " compensatoryoff_reason , " +
                    " compensatoryoff_applydate , " +
                    " compoff_noofdays , " +
                    " created_by," + " created_date)" +
                    "  Values  " +
                    " ('" + msGetGID + "', " +
                    " '" + employee_gid + "'," +
                    "'" + values.actualworking_date.ToString("yyyy-MM-dd") +
                    "', " + " '" + values.actualworking_date.ToString("yyyy-MM-dd") +
                    " '," + " 'Pending'," +
                    " '" + values.compoff_reason.Replace("'", "") + "'," +
                    " '" + lsleavefromdate.ToString("yyyy-MM-dd HH:mm:ss") +
                    "'," + " '1'," +
                    "'" + employee_gid +
                    "'," + "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult == 1)
                {
                    msSQL = "insert into hrm_trn_tcompensatoryoffdtl (" + " compensatoryoffdtl_gid, " + " compensatoryoff_gid," + " compensatoryoff_date, " + " employee_gid, " + " created_by, " + " created_date " + " )values";
                    for (var j = 0; j <= x; j++)
                    {
                        lsleave = Convert.ToDateTime(lsleavefromdate).ToString("yyyy-MM-dd HH:mm:ss");
                        msGetdtlGID = objcmnfunctions.GetMasterGID("HCFC");
                        msSQL += " ('" + msGetdtlGID + "'," + " '" + msGetGID + "'," + "'" + lsleave + "'," + " '" + employee_gid + "'," + " '" + employee_gid + "'," + " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'),";
                        // lsleavefromdate = lsleavefromdate.AddDays(1);
                    }
                    // msSQL = msSQL.Substring(0, Len(msSQL) - 1);
                    msSQL = msSQL.TrimEnd(',');
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                DataTable objTblRQ = new DataTable();

                DataSet ds_tPR = new DataSet();
                string employee;

                i = 1;

                objcmnfunctions.PopSummary(employee_gid, user_gid, lscount);
                objTblRQ = objcmnfunctions.foundRow(table);
                lscount = objcmnfunctions.foundcount(lscount);
                foreach (DataRow objRow1 in objTblRQ.Rows)
                {
                    employee = objRow1["employee_gid"].ToString();
                    hierary_level = objRow1["hierarchy_level"].ToString();
                    msGetGID = objcmnfunctions.GetMasterGID("PODC");
                    msSQL = "insert into hrm_trn_tapproval ( " + " approval_gid, " + " approved_by, " + " approved_date, " + " seqhierarchy_view, " + " hierary_level, " + " submodule_gid, " + " compensatoryoff_gid " + " ) values ( " + "'" + msGetGID + "'," + " '" + employee + "'," + "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + "'N'," + "'" + hierary_level + "'," + "'HRMLEVARL'," + "'" + gid + "') ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                msSQL = "update hrm_trn_tapproval set " +
                     "seqhierarchy_view='Y' " +
                     "where approval_flag='N'and approved_by <> '" + employee_gid + "' " +
                     "and compensatoryoff_gid = '" + gid + "'" +
                     "order by hierary_level desc limit 1";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                string lsflag = null;

                msSQL = "select approval_flag from hrm_trn_tapproval where compensatoryoff_gid='" + gid + "' and submodule_gid='HRMLEVARL'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == false)
                {
                    objODBCDatareader.Close();
                    msSQL = " update hrm_trn_tcompensatoryoff set " + " compensatoryoff_status='Approved' " + " where compensatoryoff_gid = '" + gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                else
                {
                    objODBCDatareader.Close();
                    msSQL = "select approved_by from hrm_trn_tapproval where compensatoryoff_gid='" + gid + "' and submodule_gid='HRMLEVARL' ";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.RecordsAffected == 1)
                    {
                        objODBCDatareader.Read();
                        if (objODBCDatareader["approved_by"].ToString() == employee_gid)
                        {
                            objODBCDatareader.Close();
                            msSQL = "update hrm_trn_tapproval set " + "approval_flag='Y' " + "where approved_by='" + employee_gid + "' and compensatoryoff_gid = '" + gid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            objODBCDatareader.Close();
                            msSQL = " update hrm_trn_tcompensatoryoff set " + " compensatoryoff_status='Approved' " + " where compensatoryoff_gid = '" + gid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            objODBCDatareader.Close();
                            msSQL = " SELECT employeereporting_to FROM adm_mst_tmodule2employee " + " where employee_gid   = '" + employee_gid + "' and module_gid ='HRM' and employeereporting_to='EM1006040001' ";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                objODBCDatareader.Close();
                                msSQL = " update hrm_trn_tcompensatoryoff set " + " compensatoryoff_status='Approved' " + " where compensatoryoff_gid = '" + gid + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }
                        }
                    }
                    else if (objODBCDatareader.RecordsAffected > 1)
                    {
                        objODBCDatareader.Close();
                        msSQL = " update hrm_trn_tapproval set " + " approval_flag='Y' " + " where approved_by='" + employee_gid + "' and compensatoryoff_gid = '" + gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        objODBCDatareader.Close();
                        msSQL = " update hrm_trn_tcompensatoryoff set " + " compensatoryoff_status = 'Pending' " + " where compensatoryoff_gid = '" + gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                objODBCDatareader.Close();

                ds_tPR.Dispose();
                objTblRQ.Dispose();
                if (mnResult == 1)
                {

                    // -----------------------------------Whatsapp starts here-------------------
                    DateTime lscompfromdate, lsactual_working;
                    double lscompday;
                    msSQL = "select whatsapp_flag from adm_mst_tcompany";
                    lswhatsappflag = objdbconn.GetExecuteScalar(msSQL);
                    if (lswhatsappflag == "Y")
                    {
                        Whatsapp_reportingto("employee_gid");
                        if (lsemployeeGID != "")
                        {
                            lsemployeeGID = lsemployeeGID.TrimEnd(',');
                            lblEmployeeGID_whatsapp = lsemployeeGID;
                        }

                        msSQL = "select employee_mobileno from hrm_mst_temployee where employee_gid in (" + lblEmployeeGID_whatsapp + ")";
                        objTblRQ = objdbconn.GetDataTable(msSQL);
                        if (objTblRQ.Rows.Count > 0)
                        {
                            foreach (DataRow objTblrows in objTblRQ.Rows)
                            {
                                lsmobile_no = objTblrows["employee_mobileno"].ToString();
                                msSQL = " select concat(user_firstname,user_lastname) As Name from adm_mst_tuser  " + " left join hrm_mst_temployee using(user_gid) where employee_gid='" + employee_gid + "'";
                                lsname = objdbconn.GetExecuteScalar(msSQL);
                                lscompfromdate = values.compoff_date;
                                lsactual_working = values.actualworking_date;
                                lscompday = 1;
                                if (lscompday == 1)
                                    lsmessage = " " + lsname + "  applied the comp off  on " + lscompfromdate + "   ";
                                else
                                    lsmessage = " " + lsname + "  applied the comp off   on  " + lscompfromdate + "  since he/she have worked on " + lsactual_working + "";

                                sendMessage(lsmobile_no, lsmessage);
                            }


                        }
                    }

                    // -----------------------------------Mailfunction starts here-------------------
                    string lsapprovedby;
                    string message;
                    string employee_mailid = null;
                    string employeename = null;
                    string applied_by = null;
                    string supportmail = null;
                    string pwd = null;
                    bool Mail_flag = true;
                    int MailFlag;
                    string reason = null;
                    string compoff_date = null;
                    string compoffmail_date = null;
                    string actual_working = null;
                    string days = null;
                    string trace_comment;
                    string emailpassword = null;

                    msSQL = "Select approved_by from hrm_trn_tapproval where compensatoryoff_gid= '" + gid + "' and approval_flag<>'Y'";
                    objTblRQ = objdbconn.GetDataTable(msSQL);
                    if (objTblRQ.Rows.Count > 0)
                    {
                        foreach (DataRow objTblRow in objTblRQ.Rows)
                        {

                            lsapprovedby = objTblRow["approved_by"].ToString();
                            msSQL = "select pop_username,pop_password from adm_mst_tcompany";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                objODBCDatareader.Read();
                                supportmail = objODBCDatareader["pop_username"].ToString();
                                emailpassword = objODBCDatareader["pop_password"].ToString();
                            }
                            objODBCDatareader.Close();
                            if (supportmail != "")
                            {
                                msSQL = " select b.employee_emailid,(date_format(a.actualworking_fromdate,'%d/%m/%y')) as actualworking_fromdate," + 
                                    " (date_format(a.actualworking_todate,'%d/%m/%y')) as actualworking_todate," +
                                    " (date_format(a.compensatoryoff_applydate,'%d/%m/%y')) as compensatoryoff_applydate," +
                                    " Concat(c.user_firstname,' ',c.user_lastname) as username,a.compoff_noofdays,a.compensatoryoff_reason " + 
                                    " from hrm_trn_tcompensatoryoff a " + " left join hrm_trn_tapproval d on a.compensatoryoff_gid=d.compensatoryoff_gid " + 
                                    " left join hrm_mst_temployee b on d.approved_by=b.employee_gid " + " left join adm_mst_tuser c on c.user_gid=b.user_gid " + 
                                    " where a.compensatoryoff_gid='" + gid + "' and b.employee_gid='" + lsapprovedby + "'";
                                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                if (objODBCDatareader.HasRows == true)
                                {
                                    objODBCDatareader.Read();
                                    employee_mailid = objODBCDatareader["employee_emailid"].ToString();
                                    employeename = objODBCDatareader["username"].ToString();
                                    reason = objODBCDatareader["compensatoryoff_reason"].ToString();
                                    compoffmail_date = objODBCDatareader["compensatoryoff_applydate"].ToString();
                                    actual_working = objODBCDatareader["actualworking_fromdate"].ToString();
                                    days = objODBCDatareader["compoff_noofdays"].ToString();
                                }
                                objODBCDatareader.Close();
                                if (employee_mailid != "")
                                {
                                    msSQL = " select a.created_by," + "Concat(c.user_firstname,' ',c.user_lastname) as username " + " from hrm_trn_tcompensatoryoff a " + " left join hrm_mst_temployee b on a.created_by=b.employee_gid " + " left join adm_mst_tuser c on b.user_gid=c.user_gid " + " where a.compensatoryoff_gid='" + gid + "'";
                                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                    if (objODBCDatareader.HasRows == true)
                                    {
                                        objODBCDatareader.Read();
                                        applied_by = objODBCDatareader["username"].ToString();
                                    }
                                    objODBCDatareader.Close();

                                    message = "Hi Sir/Madam,  <br />";
                                    message = message + "<br />";
                                    message = message + "I have applied for Compensatory Off on " + compoffmail_date + " <br />";
                                    message = message + "<br />";
                                    message = message + "<b>Reason :</b> " + reason + "<br />";
                                    message = message + "<br />";
                                    message = message + "<b>Actual Working Date :</b> " + actual_working + "<br />";
                                   
                                    message = message + "<br />";
                                    message = message + " Thanks and Regards  <br />";
                                    message = message + " " + applied_by + " <br />";

                                    msSQL = " select a.employee_emailid from hrm_mst_temployee a  where a.employee_gid in " + " (select distinct employee_gid from prj_trn_tmanagerprivilege where " + " project_gid in (select distinct project_gid from prj_trn_ttask where assigned_to='" + employee_gid + "'))  ";
                                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                    while (objODBCDatareader.Read())
                                    {
                                        if (employee_mailid == objODBCDatareader["employee_emailid"].ToString())
                                        {
                                            objODBCDatareader.Close();
                                            msSQL = " select a.task_gid,date(a.created_date)As created_date,a.project_gid,b.customer_name,c.project_name," + " concat(d.category_name,' / ',e.subcategory_name)as category_subcategory_name,a.severity," + " a.task_name,concat(f.user_firstname,'-',f.user_lastname) as created_by," + " concat(h.user_firstname,'-',h.user_lastname) as assigned_to,a.task_status,a.assigned_flag ," + " concat(l.user_firstname,'-',l.user_lastname) as assigned_by " + " from prj_trn_ttask a" + " left join crm_mst_tcustomer b on a.customer_gid = b.customer_gid" + " left join prj_mst_tproject c on a.project_gid = c.project_gid" + " left join prj_mst_tcategory d on a.category_gid=d.category_gid" + " left join prj_mst_tsubcategory e on a.subcategory_gid=e.subcategory_gid" + " left join adm_mst_tuser f on a.created_by = f.user_gid" + " left join hrm_mst_temployee g on a.assigned_to = g.employee_gid " + " left join adm_mst_tuser h on g.user_gid = h.user_gid " + " left join adm_mst_tuser l on a.assigned_by=l.user_gid " + " where a.task_status not in ('Close','Completed') and a.assigned_flag='Y' " + " and a.assigned_to='" + employee_gid + "' " + " ORDER BY date(a.created_date) desc,a.created_date asc,a.task_gid desc";
                                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                            message += ("<table align='Center'><tr><td style='font-weight:bold;color:Blue' colspan='5' align='Center'> Task Pending report  &nbsp; : &nbsp; " + DateTime.Today.ToString("dd-MM-yyyy") + " </td></tr></table>");
                                            message += ("<table border='1' cellpadding='0' width='100%' align='Center'>");
                                            message += ("<tr><td width='8%' style='font-weight:bold' align='Center'> Created Date</td><td width='8%' style='font-weight:bold' align='Center'> Customer Name </td><td width='8%' style='font-weight:bold' align='Center'> Project Name </td><td width='8%' align='Center' style='font-weight:bold'> Category/Subcategory </td><td width='30%' style='font-weight:bold' align='Center'> Task Name </td><td width='8%' style='font-weight:bold' align='Center'> Created By </td><td width='8%' style='font-weight:bold' align='Center'> Assign To </td><td width='8%' style='font-weight:bold' align='Center'> Assigned By </td><td width='8%' style='font-weight:bold' align='Center'> Priority </td><td width='8%' style='font-weight:bold' align='Center'> Task Status </td></tr>");
                                            if (objODBCDatareader.HasRows == true)
                                            {
                                                while (objODBCDatareader.Read())
                                                {
                                                    message += ("<tr><td  align='Center'> " + objODBCDatareader["created_date"].ToString() + "</td>");
                                                    message += ("<td align='Center'> " + objODBCDatareader["customer_name"].ToString() + "</td>");
                                                    message += ("<td> " + objODBCDatareader["project_name"].ToString() + "</td>");
                                                    message += ("<td> " + objODBCDatareader["category_subcategory_name"].ToString() + "</td>");
                                                    message += ("<td  align='Center' style='color:Green'> " + objODBCDatareader["task_name"].ToString() + "</td>");
                                                    message += ("<td align='Center'> " + objODBCDatareader["created_by"].ToString() + "</td>");
                                                    message += ("<td  align='Center'> " + objODBCDatareader["assigned_to"].ToString() + "</td>");
                                                    message += ("<td  align='Center'> " + objODBCDatareader["assigned_by"].ToString() + "</td>");
                                                    message += ("<td  align='Center'> " + objODBCDatareader["severity"].ToString() + "</td>");
                                                    message += ("<td align='Center'> " + objODBCDatareader["task_status"].ToString() + "</td></tr>");
                                                }
                                                message += ("</table>");
                                            }
                                        }
                                    }
                                    objODBCDatareader.Close();
                                    try
                                    {
                                        MailFlag = objcmnfunctions.SendSMTP2(supportmail, emailpassword, employee_mailid, "" + applied_by + " Applied for Comp-Off", message, "", "", "");
                                    }
                                    catch
                                    {
                                    }
                                }
                            }
                        }
                    }

                    trace_comment = "Applied compoff on  " + " " + compoff_date;
                    Tracelog(gid, user_gid, trace_comment, "apply_compoff");
                    values.status = true;
                    values.message = "Comp off Applied Successfully";
                    return true;
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occurred while Applying Comp off";
                    return false;
                }
            }
            catch
            {
                values.status = false;
                values.message = "Error Occurred while Applying Comp off";
                return false;
            }
        }
        public bool DaGetPermissionSummary(string employee_gid, permission_details_list objpermission_details)
        {
           try
            {

                msSQL = " select a.permissiondtl_gid,a.permission_gid,date_format(a.permission_date,'%d-%m-%Y') as applydate,a.permission_applydate, " +
                        " concat(a.permission_fromhours,':',a.permission_frommins) as permission_fromhours , " +
                        " concat(a.permission_tohours,':',a.permission_tomins) as permission_tohours, " +
                        " concat(a.permission_totalhours,':',a.total_mins) as permission_totalhours, " +
                     " a.permission_reason,a.permission_status,concat(c.user_firstname,' ',c.user_lastname) as approvedby,a.permission_approveddate from hrm_trn_tpermissiondtl a " +
                    " left join hrm_mst_temployee b on b.employee_gid =a.permission_approvedby " +
                    "left join adm_mst_tuser c on c.user_gid= b.user_gid where a.employee_gid ='" + employee_gid + "' order by a.permissiondtl_gid desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var permission_details = new List<permissionSummary_details>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        permission_details.Add(new permissionSummary_details
                        {
                            permission_gid = (dr_datarow["permission_gid"].ToString()),
                            permissiondtl_gid = (dr_datarow["permissiondtl_gid"].ToString()),
                            permission_date = (dr_datarow["applydate"].ToString()),
                            permission_applydate = (dr_datarow["permission_applydate"].ToString()),
                            permission_from = (dr_datarow["permission_fromhours"].ToString()),
                            permission_to = dr_datarow["permission_tohours"].ToString(),
                            permission_total = dr_datarow["permission_totalhours"].ToString(),
                            permission_reason = dr_datarow["permission_reason"].ToString(),
                            permission_status = dr_datarow["permission_status"].ToString(),
                            approved_by = dr_datarow["approvedby"].ToString(),
                            approved_date = dr_datarow["permission_approveddate"].ToString()

                        });
                    }
                    objpermission_details.permissionSummary_details = permission_details;
                }
                dt_datatable.Dispose();


                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool DaGetCompOffSummary(string employee_gid, compoff_list objcompoff_details)
        {
           try
            {

                msSQL = " select a.compensatoryoff_gid,date_format(a.actualworking_fromdate,'%d-%m-%Y') as actualworking_fromdate, " +
                        " date_format(a.actualworking_fromdate,'%d-%m-%Y') as actualworking_fromdate, " +
                        " date_format(a.compensatoryoff_applydate,'%d-%m-%Y') as compensatoryoff_applydate, " +
                        " a.compoff_noofdays, " +
                        " a.compensatoryoff_reason,a.compensatoryoff_status from hrm_trn_tcompensatoryoff a " +
                      " where a.employee_gid ='" + employee_gid + "' order by created_date desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var compoff_details = new List<compoffSummary_details>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        compoff_details.Add(new compoffSummary_details
                        {
                            compensatoryoff_gid = (dr_datarow["compensatoryoff_gid"].ToString()),
                            compoff_date = (dr_datarow["compensatoryoff_applydate"].ToString()),
                            atual_working = (dr_datarow["actualworking_fromdate"].ToString()),
                           
                            compoff_reason = dr_datarow["compensatoryoff_reason"].ToString(),
                            compoff_status = dr_datarow["compensatoryoff_status"].ToString()

                        });
                    }
                    objcompoff_details.compoffSummary_details = compoff_details;
                }
                dt_datatable.Dispose();

              //objcompoff_details.status = true;
                return true;
            }
            catch
            {
             // objcompoff_details.status = false;
                return false;
            }
        }
        public void Whatsapp_reportingto(string whats_employeegid)
        {
            msSQL = " select a.employeereporting_to, concat(b.user_firstname, '-', b.user_code) as user " +
               " from adm_mst_tmodule2employee a " +
               " inner join hrm_mst_temployee c on a.employee_gid = c.employee_gid " +
               " inner join adm_mst_tuser b on c.user_gid = b.user_gid " +
               " where a.module_gid = 'HRM' " +
               " and a.employee_gid = '" + whats_employeegid + "'" +
               " and a.hierarchy_level <> '-1' ";
            objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader1.HasRows == true)
            {
                string lsfnemployee = "";
                msSQL = " select a.employee_gid, a.hierarchy_level, concat(b.user_firstname, '-', b.user_code) as user" +
                         " from adm_mst_tsubmodule a  " +
                    " inner join hrm_mst_temployee c on a.employee_gid = c.employee_gid  " +
                    " inner join adm_mst_tuser b on c.user_gid = b.user_gid  " +
                    " where a.module_gid = 'HRM' and a.submodule_id='HRMLEVARL'" +
                    " and a.employee_gid = '" + objODBCDatareader1["employeereporting_to"].ToString() + "' ";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lsEmployee_Gid_list = lsEmployee_Gid_list + objODBCDatareader["employee_gid"].ToString() + ",";
                    lsfnemployee = objODBCDatareader["employee_gid"].ToString();
                }
                Whatsapp_reportingto(objODBCDatareader1["employeereporting_to"].ToString());
            }
        }

        public void sendMessage(string number, string message)
        {
            bool success = true;
            WebClient webClient = new WebClient();
            string mssql;
            dbconn objdbconn = new dbconn();
            OdbcDataReader objODBCDataReader;
            string INSTANCE_ID;
            string CLIENT_ID;
            string CLIENT_SECRET, API_URL;
            msSQL = " select whatsapp_client_id,whatsapp_instance_id,whatsapp_client_secret from adm_mst_tcompany ";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                INSTANCE_ID = objODBCDataReader["whatsapp_instance_id"].ToString();
                CLIENT_ID = objODBCDataReader["whatsapp_client_id"].ToString();
                CLIENT_SECRET = objODBCDataReader["whatsapp_client_secret"].ToString();
                API_URL = "http://enterprise.whatsmate.net/v3/whatsapp/single/text/message/" + INSTANCE_ID;


                try
                {
                    Payload payloadObj = new Payload(number, message);
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    string postData = serializer.Serialize(payloadObj);

                    webClient.Headers["content-type"] = "application/json";
                    webClient.Headers["X-WM-CLIENT-ID"] = CLIENT_ID;
                    webClient.Headers["X-WM-CLIENT-SECRET"] = CLIENT_SECRET;

                    webClient.Encoding = Encoding.UTF8;
                    string response = webClient.UploadString(API_URL, postData);
                    Console.WriteLine(response);
                }
                catch
                {

                }
            }


        }

        public void Tracelog(string reference_gid, string user_gid, string tracecomment, string tracefrom)
        {
            
            msSQL = " insert into hrm_trn_ttracelog ( " +
                     " reference_gid," +
                     " tracelog_date," +
                     " user_gid," +
                     " trace_comment," +
                     " trace_from" +
                     " ) values(" +
                     " '" + reference_gid + "'," +
                     " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                     " '" + user_gid + "'," +
                     " '" + tracecomment + "', " +
                     " '" + tracefrom + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
           
            
        }
        private class Payload
        {
            public string number;
            public string message;

            public Payload(string num, string msg)
            {
                number = num;
                message = msg;
            }
        }

        public bool DaGetMonthlyAttendence(monthlyAttendence values, string employee_gid)
        {
            try
            {

                values.monthyear = DateTime.Now.ToString("MMMM yyyy");
                lsdate = DateTime.Now.ToString("MM");

                msSQL = " select concat(c.user_code,'<br>',c.user_firstname,' ' ,c.user_lastname) as 'Employee'  , " +
                       " cast((select count(attendance_gid) from hrm_trn_tattendance x  where (x.employee_gid = '" + employee_gid + "') " +
                       " and MONTH(x.attendance_date) = '" + lsdate + "' and year(x.attendance_date) = '" + DateTime.Now.ToString("yyyy") + "' group by a.employee_gid) as char) as 'totaldays', " +
                       " cast((select count(attendance_type) from hrm_trn_tattendance x  where (x.employee_gid = '" + employee_gid + "') " +
                       " and attendance_type = 'P' and MONTH(x.attendance_date) = '" + lsdate + "'  and year(x.attendance_date) = '" + DateTime.Now.ToString("yyyy") + "' group by a.employee_gid) as char) as Present, " +
                       " cast((select count(attendance_type) from hrm_trn_tattendance x " +
                       " where (x.employee_gid = '" + employee_gid + "') and attendance_type = 'A' and MONTH(x.attendance_date) = '" + lsdate + "'  and year(x.attendance_date) = '" + DateTime.Now.ToString("yyyy") + "' " +
                       " group by a.employee_gid) as char) as 'Absent', " +
                       " cast((select ifnull(SUM(if (x.day_session = 'NA','1','0.5')),0) as count from hrm_trn_tattendance x " +
                       " where (x.employee_gid = '" + employee_gid + "') and employee_attendance = 'Leave' and MONTH(x.attendance_date) = '" + lsdate + "' " +
                       "  and year(x.attendance_date) = '" + DateTime.Now.ToString("yyyy") + "' group by a.employee_gid ) as char) as 'Leave', " +
                       " cast((select count(attendance_type) from hrm_trn_tattendance x " +
                       "  where (x.employee_gid = '" + employee_gid + "') and employee_attendance = 'Holiday' and  MONTH(x.attendance_date) = '" + lsdate + "'" +
                       "  and year(x.attendance_date) = '" + DateTime.Now.ToString("yyyy") + "' group by a.employee_gid) as char) as 'Holiday', " +
                       " cast((select count(attendance_type) from hrm_trn_tattendance x " +
                       " where(a.employee_gid = x.employee_gid) and attendance_type = 'WH' and MONTH(x.attendance_date) = '" + lsdate + "'" +
                       "  and year(x.attendance_date) = '" + DateTime.Now.ToString("yyyy") + "' group by a.employee_gid ) as char) as 'Weekoff' " +
                       " from hrm_mst_temployee a " +
                       " inner join adm_mst_tuser c on a.user_gid = c.user_gid " +
                       " left join hrm_trn_temployeetypedtl h on a.employee_gid = h.employee_gid " +
                       " where a.attendance_flag = 'Y' and a.employee_gid = '" + employee_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    if (objODBCDatareader["totaldays"].ToString() == "")
                    {
                        values.totalDays = "0";
                    }
                    else
                    {
                        values.totalDays = objODBCDatareader["totaldays"].ToString();
                    }

                    if (objODBCDatareader["Present"].ToString() == "")
                    {
                        values.countPresent = "0";
                    }
                    else
                    {
                        values.countPresent = objODBCDatareader["Present"].ToString();
                    }
                    if (objODBCDatareader["Absent"].ToString() == "")
                    {
                        values.countAbsent = "0";
                    }
                    else
                    {
                        values.countAbsent = objODBCDatareader["Absent"].ToString();
                    }
                    if (objODBCDatareader["Leave"].ToString() == "")
                    {
                        values.countLeave = "0";
                    }
                    else
                    {
                        values.countLeave = objODBCDatareader["Leave"].ToString();
                    }
                    if (objODBCDatareader["Holiday"].ToString() == "")
                    {
                        values.countholiday = "0";
                    }
                    else
                    {
                        values.countholiday = objODBCDatareader["Holiday"].ToString();
                    }
                    if (objODBCDatareader["Weekoff"].ToString() == "")
                    {
                        values.countWeekOff = "0";
                    }
                    else
                    {
                        values.countWeekOff = objODBCDatareader["Weekoff"].ToString();
                    }

                }
                objODBCDatareader.Close();

                var getdata = new List<last6MonthAttendence_list>();
                msSQL = " select concat(CAST(monthname(attendance_date) AS char),' ',year(attendance_date)) as monthname,count(attendance_gid) as total, " +
                       " (select count(attendance_gid) from hrm_trn_tattendance b where monthname(a.attendance_date) = monthname(b.attendance_date) " +
                       " and year(a.attendance_date) = year(b.attendance_date) and  b.employee_gid = '" + employee_gid + "' and attendance_type = 'P' group by monthname(attendance_date)) AS present, " +
                       " (select count(attendance_gid) from hrm_trn_tattendance b where monthname(a.attendance_date) = monthname(b.attendance_date) " +
                       " and year(a.attendance_date) = year(b.attendance_date) and  b.employee_gid = '" + employee_gid + "' and attendance_type = 'A'  group by monthname(attendance_date)) AS absent " +
                       " from hrm_trn_tattendance a where employee_gid = '" + employee_gid + "' " +
                       " and  year(attendance_date) = '" + DateTime.Now.ToString("yyyy") + "' " +
                       " group by monthname(attendance_date) ORDER BY MONTH(attendance_date) desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    if (dt_datatable.Rows.Count < 5)
                    {
                        int count = 5 - dt_datatable.Rows.Count;
                        for (int i = 0; i < count; i++)
                        {
                            getdata.Add(new last6MonthAttendence_list
                            {
                                monthname = "",
                                countPresent = "",
                                countAbsent = ""
                            });
                        }
                    }
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getdata.Add(new last6MonthAttendence_list
                        {
                            monthname = dr_datarow["monthname"].ToString(),
                            countPresent = dr_datarow["present"].ToString(),
                            countAbsent = dr_datarow["absent"].ToString()
                        });
                    }


                    values.last6MonthAttendence_list = getdata;
                }
                dt_datatable.Dispose();
                values.status = true;
                return true;
            }
            catch
            {
                values.status = false;
                return false;
            }
        }
        // Delete Pending .....//

        public void DaPostLoginPendingDelete(string attendancelogintmp_gid, loginsummary_list values)
        {
            try
            {

                msSQL = " Delete from hrm_tmp_tattendancelogin where attendancelogintmp_gid = '" + attendancelogintmp_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Applied Login Deleted Successfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occurred";
                }
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaPostLogoutPendingDelete(string attendancetmp_gid, logoutsummary_list values)
        {
           try
            {
                msSQL = " Delete from hrm_tmp_tattendance where attendancetmp_gid= '" + attendancetmp_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Applied LogOut Deleted Successfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occurred";
                }
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaPostODpendingDelete(string ondutytracker_gid, onduty_details values)
        {
           try
            {

                msSQL = " Delete from hrm_trn_tondutytracker where ondutytracker_gid='" + ondutytracker_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                if (mnResult != 0)
                {
                    values.message = "Applied OD is Deleted Successfully";
                    values.status = true;
                    
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occured";
             
                }
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaPostCompoffPendingDelete(string compensatoryoff_gid, compoffSummary_details values)
        {
            try
            {
                msSQL = " delete hrm_trn_tcompensatoryoff, hrm_trn_tcompensatoryoffdtl from hrm_trn_tcompensatoryoff" +
                   " inner join hrm_trn_tcompensatoryoffdtl using(compensatoryoff_gid)  where compensatoryoff_gid = '" + compensatoryoff_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Applied Compensatoryoff Deleted Successfully";

                }
                else
                {
                    values.status = false;
                    values.message = "Error Occurred";
           
                }

            }
            catch
            {
                values.status = false;

            }
        }
        public void DaPostPermissionDelete(string permissiondtl_gid, permissionSummary_details values)
        {
           try
            {

                msSQL = " Delete from hrm_trn_tpermissiondtl where permissiondtl_gid = '" + permissiondtl_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Applied Permission Deleted Successfully";
                   
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occurred";
                    
                }
            }
            catch
            {
                values.status = false;
            }
        }

        public bool DamonthlyAttendenceReport(string employee_gid, monthlyAttendenceReport values)
        {
            try
            {

                //msSQL = " select a.shift_date,a.attendance_date,b.shifttype_name, " +
                //        " case when a.login_time is null then '-' " +
                //        " else date_format(a.login_time, '%h:%m:%s') end as login_time, " +
                //        " case when a.logout_time is null then '-' " +
                //        " else date_format(a.logout_time, '%h:%m:%s') end as logout_time," +
                //        " case when a.attendance_type = 'A' then 'Absent' " +
                //        " when a.attendance_type = 'P' then 'Present' " +
                //        " when a.attendance_type = 'WH' then 'Weekoff' " +
                //        " Else a.attendance_type end as attendance_type " +
                //        " from hrm_trn_tattendance a " +
                //        " left join hrm_mst_tshifttype b on a.shifttype_gid = b.shifttype_gid " +
                //        "where employee_gid = '" + employee_gid + "'  order by attendance_date desc Limit 30";

                msSQL = " select date_format(a.shift_date,'%d-%m-%Y') as shift_date,a.attendance_date,b.shifttype_name, " +
                      " a.login_time,a.logout_time, " +
                      " case when a.attendance_type = 'A' then 'Absent' " +
                      " when a.attendance_type = 'P' then 'Present' " +
                      " when a.attendance_type = 'WH' then 'Weekoff' " +
                      " Else a.attendance_type end as attendance_type " +
                      " from hrm_trn_tattendance a " +
                      " left join hrm_mst_tshifttype b on a.shifttype_gid = b.shifttype_gid " +
                      "where employee_gid = '" + employee_gid + "'  order by attendance_date desc Limit 30";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_report = new List<monthlyAttendenceReport_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_report.Add(new monthlyAttendenceReport_list
                        {
                            attendance_date = (dr_datarow["shift_date"].ToString()),
                            shift_name = (dr_datarow["shifttype_name"].ToString()),
                            Login_time = (dr_datarow["login_time"].ToString()),
                            logout_time = (dr_datarow["logout_time"].ToString()),
                            attendance_type = dr_datarow["attendance_type"].ToString()

                        });
                    }
                    values.monthlyAttendenceReport_list = get_report;
                }
                dt_datatable.Dispose();

                //objcompoff_details.status = true;
                return true;
            }
            catch (Exception ex)
            {
                // objcompoff_details.status = false;
                return false;
            }
        }

        public void DaGetAnnouncement(string employee_gid, mdlannouncement values)
        {
            try
            {
                msSQL = "select announce_details from hrm_trn_tannouncements where status='Activated'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    if (objODBCDatareader["announce_details"].ToString() == "")
                    {
                        values.Announcement = "N";
                    }
                    else
                    {

                        values.Announcement = objODBCDatareader["announce_details"].ToString();
                        values.status = true;
                    }
                }
                }
            catch
            {
                values.status = false;
            }
        }
        }
}