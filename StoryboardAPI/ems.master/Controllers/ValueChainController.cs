using ems.master.DataAccess;
using ems.master.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ems.master.Controllers
{
    [RoutePrefix("api/ValueChain")]
    [Authorize]

    public class ValueChainController : ApiController
    {
        DaValueChain objDaValueChain = new DaValueChain();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        [ActionName("GetValueChain")]
        [HttpGet]
        public HttpResponseMessage getValueChain()
        {
            MdlValueChain objMdlValueChain = new MdlValueChain();
            objDaValueChain.DaGetValueChain(objMdlValueChain);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlValueChain);
        }

        [ActionName("CreateValueChain")]
        [HttpPost]
        public HttpResponseMessage CreateValueChain(valuechain values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaValueChain.DaCreateValueChain(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditValueChain")]
        [HttpGet]
        public HttpResponseMessage editvaluechain(string valuechain_gid)
        {
            valuechain values = new valuechain();
            objDaValueChain.DaEditValueChain(valuechain_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateValueChain")]
        [HttpPost]
        public HttpResponseMessage updatevaluechain(valuechain values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaValueChain.DaUpdateValueChain(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteValueChain")]
        [HttpGet]
        public HttpResponseMessage deleteValueChain(string valuechain_gid)
        {
            valuechain values = new valuechain();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaValueChain.DaDeleteValueChain(valuechain_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //-----Get Value chain -order by ASC--------//
        [ActionName("GetValueChainASC")]
        [HttpGet]
        public HttpResponseMessage getValueChainASC()
        {
            MdlValueChain objMdlValueChain = new MdlValueChain();
            objDaValueChain.DaGetValueChainASC(objMdlValueChain);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlValueChain);
        }
        [ActionName("ValueChainStatusUpdate")]
        [HttpPost]
        public HttpResponseMessage ValueChainStatusUpdate(valuechain values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaValueChain.DaValueChainStatusUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetActiveLog")]
        [HttpGet]
        public HttpResponseMessage GetActiveLog(string valuechain_gid)
        {
            MdlValueChain values = new MdlValueChain();
            objDaValueChain.DaGetActiveLog(valuechain_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}