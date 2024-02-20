using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace ems.hrm.Models
{
 

    public class holidaycalender : result
    {
        public List<holidaycalender_list> holidaycalender_list { get; set; }
    }

    public class holidaycalender_list
    {
        public string holiday_date { get; set; }
        public string holiday_name { get; set; }
        public string holiday_dayname { get; set; }
    }

    public class eventdetail : result
    {
        public List<createevent> createevent { get; set; }
    }

    public class createevent : result
    {
        public DateTime event_date { get; set; }
        public DateTime event_time { get; set; }
        public TimeSpan time { get; set; }
        public string today_event { get; set; }
        public string event_title { get; set; }
    }

}
