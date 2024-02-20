using ems.mastersamagro.DataAccess;
using ems.mastersamagro.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;

namespace ems.mastersamagro.Controllers
{

    /// <summary>
    /// This Controllers will provide access to various functionalities for uom master 
    /// </summary>
    /// <remarks>Written by Sherin Augusta</remarks>

    [RoutePrefix("api/AgrMstUom")]
    [Authorize]
    public class AgrMstUomController : ApiController
    {
        DaAgrMstUom objDaAgrMstUom = new DaAgrMstUom();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        [ActionName("Getuom")]
        [HttpGet]
        public HttpResponseMessage Getuom()
        {
            UomList objvalues = new UomList();
            objDaAgrMstUom.DaGetuom(objvalues);
            return Request.CreateResponse(HttpStatusCode.OK, objvalues);
        }

        [ActionName("Createuom")]
        [HttpPost]
        public HttpResponseMessage Createuom(Uomdtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstUom.DaCreateuom(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Edituom")]
        [HttpGet]
        public HttpResponseMessage Edituom(string uom_gid)
        {
            Uomdtl values = new Uomdtl();
            objDaAgrMstUom.DaEdituom(uom_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Updateuom")]
        [HttpPost]
        public HttpResponseMessage Updateuom(Uomdtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstUom.DaUpdateuom(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Inactiveuom")]
        [HttpPost]
        public HttpResponseMessage Inactiveuom(Uomdtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstUom.DaInactiveuom(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Deleteuom")]
        [HttpGet]
        public HttpResponseMessage Deleteuom(string uom_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstUom.DaDeleteuom(uom_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("uomInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage uomInactiveLogview(string uom_gid)
        {
            UomList values = new UomList();
            objDaAgrMstUom.DauomInactiveLogview(uom_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetuomList")]
        [HttpGet]
        public HttpResponseMessage GetuomList()
        {
            UomList objvalues = new UomList();
            objDaAgrMstUom.DaGetuomList(objvalues);
            return Request.CreateResponse(HttpStatusCode.OK, objvalues);
        }
    }
}