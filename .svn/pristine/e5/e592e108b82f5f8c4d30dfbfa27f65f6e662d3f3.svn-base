using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.utilities.Models;
using ems.utilities.Functions;
using System.Web;
using ems.hrm.Models;
using ems.hrm.DataAccess;
namespace StoryboardAPI.Controllers.ems.hrm
{
    [RoutePrefix("api/holidayCalender")]
    [Authorize]

    public class holidayCalenderController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaHolidayCalender objDaHolidayCalender = new DaHolidayCalender();
        [ActionName("holidaycalender")]
        [HttpGet]
        public HttpResponseMessage GetHolidayCalender()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            holidaycalender objholidaycalender = new holidaycalender();
            objDaHolidayCalender.DaGetHoliday(objholidaycalender, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objholidaycalender);
        }

        [ActionName("event")]
        [HttpGet]
        public HttpResponseMessage GetEvent()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            eventdetail objeventdetail = new eventdetail();
            objDaHolidayCalender.DaGetEvent(objeventdetail, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objeventdetail);
        }

        [ActionName("todayactivity")]
        [HttpGet]
        public HttpResponseMessage GetTodayActivity()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            eventdetail objtodayactivity = new eventdetail();
            objDaHolidayCalender.gettodayactivity_da(objtodayactivity, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objtodayactivity);
        }

        [ActionName("createevent")]
        [HttpPost]
        public HttpResponseMessage getcreateevent(createevent values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaHolidayCalender.DaGetCreateEvent(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}
