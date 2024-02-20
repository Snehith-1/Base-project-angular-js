using ems.mastersamagro.DataAccess;
using ems.mastersamagro.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ems.mastersamagro.Controllers
{

    /// <summary>
    /// This Controllers will provide access to Address Type master
    /// </summary>
    /// <remarks>Written by Abilash.A </remarks>

    [RoutePrefix("api/AgrMstAddressType")]
    [Authorize]
    public class AgrMstAddressTypeController : ApiController
    {
        DaAgrMstAddressType objDaAddress = new DaAgrMstAddressType();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        [ActionName("GetAddressType")]
        [HttpGet]
        public HttpResponseMessage getAddressType()
        {
            MdlAddressType objMdlAddressType = new MdlAddressType();
            objDaAddress.DaGetAddressType(objMdlAddressType);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlAddressType);
        }

        [ActionName("CreateAddressType")]
        [HttpPost]
        public HttpResponseMessage createaddresstype(addresstype values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAddress.DaCreateAddressType(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditAddressType")]
        [HttpGet]
        public HttpResponseMessage editaddresstype(string address_gid)
        {
            addresstype values = new addresstype();
            objDaAddress.DaEditAddressType(address_gid,values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateAddressType")]
        [HttpPost]
        public HttpResponseMessage updateaddresstype(addresstype values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAddress.DaUpdateAddressType(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("AddressTypeDelete")]
        [HttpGet]
        public HttpResponseMessage deleteaddresstype(string address_gid)
        {
            addresstype values = new addresstype();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAddress.DaDeleteAddressType(address_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //--- Get Address Type - Order by ASC----------//
        [ActionName("GetAddressTypeASC")]
        [HttpGet]
        public HttpResponseMessage getAddressTypeASC()
        {
            MdlAddressType objMdlAddressType = new MdlAddressType();
            objDaAddress.DaGetAddressTypeASC(objMdlAddressType);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlAddressType);
        }
        [ActionName("AddressTypeStatusUpdate")]
        [HttpPost]
        public HttpResponseMessage AddressTypeStatusUpdate(addresstype values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAddress.DaAddressTypeStatusUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetActiveLog")]
        [HttpGet]
        public HttpResponseMessage GetActiveLog(string address_gid)
        {
            MdlAddressType values = new MdlAddressType();
            objDaAddress.DaGetActiveLog(address_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}