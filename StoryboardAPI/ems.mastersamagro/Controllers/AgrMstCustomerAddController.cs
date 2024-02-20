using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.utilities.Models;
using ems.utilities.Functions;
using System.Web;
using ems.mastersamagro.Models;
using ems.mastersamagro.DataAccess;

namespace ems.mastersamagro.Controllers
{

    /// <summary>
    /// This Controllers will provide access to fetch data from Customer master in custopedia.
    /// </summary>
    /// <remarks>Written by Abilash.A</remarks>

    [RoutePrefix("api/AgrMstCustomerAdd")]
    [Authorize]
    public class AgrMstCustomerAddController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaAgrMstCustomerAdd objDaAgrMstCustomerAdd = new DaAgrMstCustomerAdd();

        //----------- Submit Address Type----------//
        [ActionName("postaddresstype")]
        [HttpPost]
        public HttpResponseMessage postaddresstype(MdlMstaddresstype values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstCustomerAdd.Dapostaddresstype(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //----------- Submit ID Proof----------//
        [ActionName("postidproof")]
        [HttpPost]
        public HttpResponseMessage postidproof(MdlID_proof values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstCustomerAdd.Dapostidproof(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //----------- Submit Mobile No----------//
        [ActionName("postmobileno")]
        [HttpPost]
        public HttpResponseMessage postmobileno(Mdlmobile_no values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstCustomerAdd.Dapostmobileno(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //----------- Submit Member---------//
        [ActionName("postmember")]
        [HttpPost]
        public HttpResponseMessage postmember(MdlMember values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstCustomerAdd.Dapostmember(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetMobileNoList")]
        [HttpGet]
        public HttpResponseMessage getMobileNo()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            Mdlmobile_no values = new Mdlmobile_no();
            objDaAgrMstCustomerAdd.DaGetMobileNo(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAddressList")]
        [HttpGet]
        public HttpResponseMessage getAddress()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstaddresstype values = new MdlMstaddresstype();
            objDaAgrMstCustomerAdd.DaGetAddress(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetidproofList")]
        [HttpGet]
        public HttpResponseMessage getidprooflist()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlID_proof values = new MdlID_proof();
            objDaAgrMstCustomerAdd.DaGetidproofList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetMemberList")]
        [HttpGet]
        public HttpResponseMessage getmemberlist()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMember values = new MdlMember();
            objDaAgrMstCustomerAdd.DaGetMemberList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //---------Upload Photo-----//
        [ActionName("Uploadphoto")]
        [HttpPost]
        public HttpResponseMessage postuploadphoto()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            UploadDocumentname documentname = new UploadDocumentname();
            objDaAgrMstCustomerAdd.DaPostUploadPhoto(httpRequest, documentname, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }
        //----------Individual Submit------------//
        [ActionName("PostIndividualSubmit")]
        [HttpPost]
        public HttpResponseMessage postindividualsubmit(mdlcustomer2userdtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstCustomerAdd.DaPostIndividualSubmit(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //----------Individual Submit------------//
        [ActionName("EditIndividualSubmit")]
        [HttpPost]
        public HttpResponseMessage editindividualsubmit(mdlcustomer2userdtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstCustomerAdd.DaEditIndividualSubmit(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //--------Get Customer 2 user Details--------//
        [ActionName("GetCustomer2UserDtl")]
        [HttpGet]
        public HttpResponseMessage getcustomer2userdtl()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mdlcustomer2userdtl values = new mdlcustomer2userdtl();
            objDaAgrMstCustomerAdd.DaGetCustomer2UserDtl(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //--------Get Customer 2 user Details--------//
        [ActionName("EditCustomer2UserDtl")]
        [HttpGet]
        public HttpResponseMessage EditCustomer2UserDtl(string customer_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mdlcustomer2userdtl values = new mdlcustomer2userdtl();
            objDaAgrMstCustomerAdd.DaEditCustomer2UserDtl(customer_gid,getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //----------Institution Submit------------//
        [ActionName("PostInstitutionSubmit")]
        [HttpPost]
        public HttpResponseMessage postinstitutionsubmit(mdlcustomer2userdtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstCustomerAdd.DaPostInstitutionSubmit(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //----------Institution Submit------------//
        [ActionName("EditInstitutionSubmit")]
        [HttpPost]
        public HttpResponseMessage editinstitutionsubmit(mdlcustomer2userdtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstCustomerAdd.DaEditInstitutionSubmit(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //----------Customer Submit------------//
        [ActionName("PostCustomerSubmit")]
        [HttpPost]
        public HttpResponseMessage postcustomersubmit(mdlcreatecustomer values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstCustomerAdd.DaPostCustomerSubmit(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //----------- Delete Mobile No----------//
        [ActionName("DeleteMobileNo")]
        [HttpGet]
        public HttpResponseMessage deleteMmobileno(string customer2mobileno_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mdlcreatecustomer values = new mdlcreatecustomer();
            objDaAgrMstCustomerAdd.DaDeleteMobileNo(customer2mobileno_gid,getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //----------- Delete Mobile No----------//
        [ActionName("DeleteAddress")]
        [HttpGet]
        public HttpResponseMessage deleteaddress(string customer2address_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstaddresstype values = new MdlMstaddresstype();
            objDaAgrMstCustomerAdd.DaDeleteAddress(customer2address_gid,getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //----------- Delete ID Proof----------//
        [ActionName("DeleteIDProof")]
        [HttpGet]
        public HttpResponseMessage deleteidproof(string customer2identityproof_gid )
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlID_proof values = new MdlID_proof();
            objDaAgrMstCustomerAdd.DeleteIDProof(customer2identityproof_gid,getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //----------- Delete ID Proof----------//
        [ActionName("DeleteMember")]
        [HttpGet]
        public HttpResponseMessage deletemember(string customer2member_gid )
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMember values = new MdlMember();
            objDaAgrMstCustomerAdd.DeleteMember(customer2member_gid,getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //-----Get Age--------//
        [ActionName("GetAge")]
        [HttpGet]
        public HttpResponseMessage getage(string dob)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            Mdlmobile_no values = new Mdlmobile_no();
            objDaAgrMstCustomerAdd.DaGetAge(dob,getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetTempClear")]
        [HttpGet]
        public HttpResponseMessage gettempclear()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result values = new result();
            objDaAgrMstCustomerAdd.DaGetTempClear(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("OverallTempClear")]
        [HttpGet]
        public HttpResponseMessage getoveralltempclear()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result values = new result();
            objDaAgrMstCustomerAdd.DaOverallTempClear(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //-----Get Information--------//
        [ActionName("GetCustomer2UserInfo")]
        [HttpGet]
        public HttpResponseMessage getcustomer2userinfo(string customer2usertype_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mdlcustomer2userdtl values = new mdlcustomer2userdtl();
            objDaAgrMstCustomerAdd.DaGetCustomer2UserInfo(customer2usertype_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //----------View Information-----------//
        [ActionName("GetViewCustomer2UserDtl")]
        [HttpGet]
        public HttpResponseMessage getviewcustomer2userdtl(string customer_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mdlcustomer2userdtl values = new mdlcustomer2userdtl();
            objDaAgrMstCustomerAdd.DaGetViewCustomer2UserDtl(customer_gid,getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //----------View Information-----------//
        [ActionName("GetIndividualInformation")]
        [HttpGet]
        public HttpResponseMessage getindividualinformation(string aadhar_no)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mdlcustomer2userdtl values = new mdlcustomer2userdtl();
            objDaAgrMstCustomerAdd.DaGetIndividualInformation(aadhar_no,getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //----------View Institution Information-----------//
        [ActionName("GetInstitutionInformation")]
        [HttpGet]
        public HttpResponseMessage getinstitutionnformation(string gst_no)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mdlcustomer2userdtl values = new mdlcustomer2userdtl();
            objDaAgrMstCustomerAdd.DaGetInstitutionInformation(gst_no, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //----------Get Edit Information-----------//
        [ActionName("GetEditCustomer2UserDtl")]
        [HttpGet]
        public HttpResponseMessage geteditcustomer2userdtl(string customer_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mdlcreatecustomer values = new mdlcreatecustomer();
            objDaAgrMstCustomerAdd.DaGetEditCustomer2UserDtl(customer_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //----------Individual Update------------//
        [ActionName("PostIndividualUpdate")]
        [HttpPost]
        public HttpResponseMessage postindividualupdate(mdlcustomer2userdtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstCustomerAdd.DaPostIndividualUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //----------Institution Update------------//
        [ActionName("PostInstitutionUpdate")]
        [HttpPost]
        public HttpResponseMessage postinstitutionupdate(mdlcustomer2userdtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstCustomerAdd.DaPostInstitutionUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //----------Customer Update------------//
        [ActionName("PostCustomerUpdate")]
        [HttpPost]
        public HttpResponseMessage postcustomerupdate(mdlcreatecustomer values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstCustomerAdd.DaPostCustomerUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //--------Get Customer 2 user Details--------//
        [ActionName("EditUserDtl")]
        [HttpGet]
        public HttpResponseMessage edituserdtl(string customer_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mdlcustomer2userdtl values = new mdlcustomer2userdtl();
            objDaAgrMstCustomerAdd.DaEdit2UserDtl(getsessionvalues.employee_gid, values,customer_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetTempCustomerdetail")]
        [HttpGet]
        public HttpResponseMessage GetTempCustomerdetail()
        {
            MdlCustomer objMdlCustomer = new MdlCustomer();
            objDaAgrMstCustomerAdd.DaGetTempCustomerdetail(objMdlCustomer);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlCustomer);
        }
        //----------Tag Customer------------//
      
        //----------URN Validation-----------//
        [ActionName("GetURNInfo")]
        [HttpGet]
        public HttpResponseMessage GetURNInfo(string urn)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mdlcustomer2userdtl values = new mdlcustomer2userdtl();
            objDaAgrMstCustomerAdd.DaGetURNInfo(urn, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("customerdetail")]
        [HttpGet]
        public HttpResponseMessage Customerdetail()
        {
            MdlCustomer objMdlCustomer = new MdlCustomer();
            objDaAgrMstCustomerAdd.DaGetCustomerdetail(objMdlCustomer);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlCustomer);
        }
        [ActionName("GetMyTeamCustomer")]
        [HttpGet]
        public HttpResponseMessage GetMyTeamCustomer()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCustomer objMdlCustomer = new MdlCustomer();
            objDaAgrMstCustomerAdd.DaGetMyTeamCustomer(objMdlCustomer, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlCustomer);
        }
        [ActionName("GetMyCustomer")]
        [HttpGet]
        public HttpResponseMessage GetMyCustomer()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCustomer objMdlCustomer = new MdlCustomer();
            objDaAgrMstCustomerAdd.DaGetMyCustomer(objMdlCustomer, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlCustomer);
        }
    }
}
