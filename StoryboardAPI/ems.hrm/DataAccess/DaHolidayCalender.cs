using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Data;
using ems.hrm.Models;
using System.Data.Odbc;
using ems.utilities.Functions;


namespace ems.hrm.DataAccess
{
    public class DaHolidayCalender
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msSQL;
        string msGetGID;
        string lsmobilenum;
        int mnResult;
        TimeSpan time;
        DateTime lsevent_time;


        public bool DaGetHoliday(holidaycalender values, string employee_gid)
        {
            try
            {
                msSQL = " select  b.holiday_gid,employee_gid,date_format(b.holiday_date,'%d-%m-%Y')as holiday_date,holiday_name, " +
                    " cast(dayname(b.holiday_date) as char) as holiday_dayname from hrm_mst_tholiday2employee a " +
                    " left join hrm_mst_tholiday b on a.holiday_gid = b.holiday_gid where employee_gid = '" + employee_gid + "' and " +
                    " year(b.holiday_date) >= '" + DateTime.Now.ToString("yyyy") + " ' order by DATE(b.holiday_date) asc, b.holiday_date desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_holiday = new List<holidaycalender_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        get_holiday.Add(new holidaycalender_list
                        {
                            holiday_date = dt["holiday_date"].ToString(),
                            holiday_dayname = dt["holiday_dayname"].ToString(),
                            holiday_name = dt["holiday_name"].ToString()
                        });
                    }
                    values.holidaycalender_list = get_holiday;
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

        public bool DaGetEvent(eventdetail values, string employee_gid)
        {
            try
            {
                msSQL = " select date_format(event_date,'%Y-%m-%d') as event_date,event_title,event_time " +
                   " from hrm_trn_treminder where created_by='" + employee_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_event = new List<createevent>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        if (dt["event_time"].ToString() == "")
                        {
                            lsevent_time = Convert.ToDateTime("00:00".ToString());
                        }
                        else
                        {
                            lsevent_time = Convert.ToDateTime(dt["event_time"].ToString());
                        }
                        get_event.Add(new createevent
                        {
                            event_time = lsevent_time,
                            event_date = Convert.ToDateTime(dt["event_date"].ToString()),
                            event_title = dt["event_title"].ToString()
                        });
                    }
                    values.createevent = get_event;
                }
                dt_datatable.Dispose();
                values.status = true;
                values.message = "Event Created Successfully";
                return true;
            }
            catch
            {
                values.status = false;
                values.message = "Error Occured While Creating";
                return false;
            }
        }

        public bool gettodayactivity_da(eventdetail values, string employee_gid)
        {
            try
            {
                msSQL = " select date_format(event_date,'%d-%m-%Y') as event_date,event_title,event_time " +
                    " from hrm_trn_treminder where created_by='" + employee_gid + "' " +
                    " and event_date like '%" + DateTime.Now.ToString("yyyy-MM-dd") + "%'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_event = new List<createevent>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        if (dt["event_time"].ToString() == "")
                        {
                            time = TimeSpan.Parse("00:00".ToString());
                        }
                        else
                        {
                            time = TimeSpan.Parse(dt["event_time"].ToString());
                        }
                        get_event.Add(new createevent
                        {
                            time = time,
                            today_event = dt["event_date"].ToString(),
                            event_title = dt["event_title"].ToString()
                        });
                    }
                    values.createevent = get_event;
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

        public bool DaGetCreateEvent(string employee_gid, createevent values)
        {
            
            try
            {
                msGetGID = objcmnfunctions.GetMasterGID("HRRM");
                msSQL = " insert into hrm_trn_treminder(" +
                   " reminder_gid, " +
                   " event_date, " +
                   " reminder_startdate, " +
                   " event_title, " +
                   " event_time, " +
                   " event_description, " +
                   " event_status, " +
                   " created_date, " +
                   " created_by) " +
                   " values ( " +
                   " '" + msGetGID + "', " +
                   " '" + values.event_date.ToString("yyyy-MM-dd") + "', " +
                   " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', " +
                   " '" + values.event_title.Replace("'", " ") + "', " +
                   " '" + values.event_time.TimeOfDay + "'," +
                   " '', " +
                   " 'pending', " +
                   " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', " +
                   " '" + employee_gid + "') ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult !=0)
                {
                    values.message = "Event Created Successfully";
                    values.status = true;
                    return true;
                    
                }
                values.status = false;
                return false;

            }
            catch(Exception ex)
            {
                values.status = false;
                values.message = ex.Message;
                return false;
            }
         
           

        }


    }
}
